// Decompiled with JetBrains decompiler
// Type: PX.Data.EventsBase`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Data;

/// <exclude />
public abstract class EventsBase<TClassicEventArgs, TClassicDelegate, TDelayedCollection>
  where TClassicEventArgs : EventArgs
  where TClassicDelegate : Delegate
  where TDelayedCollection : class, ICollection, new()
{
  protected PXGraph Graph;
  private protected EventsBase<
  #nullable disable
  TClassicEventArgs, TClassicDelegate, TDelayedCollection>.DelayedHandlers Delayed = new EventsBase<TClassicEventArgs, TClassicDelegate, TDelayedCollection>.DelayedHandlers();

  private protected EventsBase(
  #nullable enable
  PXGraph graph) => this.Graph = graph;

  internal void CacheAttached(System.Type table, PXCache cache)
  {
    TDelayedCollection delayed;
    if (!this.Delayed.Get(table, out delayed) || delayed.Count <= 0)
      return;
    this.ApplyDelayed(cache, delayed);
    this.Delayed.Remove(table);
  }

  protected abstract void ApplyDelayed(PXCache cache, TDelayedCollection delayedHandlers);

  internal delegate void Interceptor(
    PXCache cache,
    TClassicEventArgs args,
    TClassicDelegate? interceptedDelegate)
    where TClassicEventArgs : 
    #nullable disable
    EventArgs
    where TClassicDelegate : Delegate
    where TDelayedCollection : class, ICollection, new();

  private protected class Proxy(
    #nullable enable
    EventsBase<
    #nullable disable
    TClassicEventArgs, TClassicDelegate, TDelayedCollection>.Interceptor intercepting,
    #nullable enable
    TClassicDelegate? intercepted) : IEventInterceptorProxy
  {
    private readonly EventsBase<
    #nullable disable
    TClassicEventArgs, TClassicDelegate, TDelayedCollection>.Interceptor _interceptingDelegate = intercepting;
    private 
    #nullable enable
    TClassicDelegate? _interceptedDelegate = intercepted;

    Delegate IEventInterceptorProxy.InterceptingDelegate => (Delegate) this._interceptingDelegate;

    Delegate? IEventInterceptorProxy.InterceptedDelegate
    {
      get => (Delegate) this._interceptedDelegate;
      set => this._interceptedDelegate = (TClassicDelegate) value;
    }

    public void Intercept(PXCache cache, TClassicEventArgs args)
    {
      this._interceptingDelegate(cache, args, this._interceptedDelegate);
    }
  }

  private protected class DelayedHandlers
  {
    private Dictionary<System.Type, TDelayedCollection>? _handlersByTable;

    public bool Get(System.Type table, [NotNullWhen(true)] out TDelayedCollection? delayed, bool initialize = false)
    {
      delayed = default (TDelayedCollection);
      if (this._handlersByTable == null)
      {
        if (initialize)
        {
          this._handlersByTable = new Dictionary<System.Type, TDelayedCollection>();
          this._handlersByTable.Add(table, delayed = new TDelayedCollection());
        }
      }
      else if (!this._handlersByTable.TryGetValue(table, out delayed) && initialize)
        this._handlersByTable.Add(table, delayed = new TDelayedCollection());
      if (initialize)
        return (object) delayed != null;
      return (object) delayed != null && delayed.Count > 0;
    }

    public bool Remove(System.Type table)
    {
      Dictionary<System.Type, TDelayedCollection> handlersByTable = this._handlersByTable;
      // ISSUE: explicit non-virtual call
      return handlersByTable != null && __nonvirtual (handlersByTable.Remove(table));
    }
  }

  private protected abstract class Generic<TGenericEventArgs, TGenericDelegate>
    where TGenericEventArgs : class, IGenericEventWith<TClassicEventArgs>
    where TGenericDelegate : Delegate
  {
    private readonly Func<TGenericDelegate, IGenericEventAdapter<TClassicEventArgs, TGenericDelegate>> _handlerToAdapter;
    private readonly Func<IGenericEventAdapter<TClassicEventArgs, TGenericDelegate>, TClassicDelegate> _adapterToHandler;

    public Generic(
      Func<TGenericDelegate, IGenericEventAdapter<TClassicEventArgs, TGenericDelegate>> handlerToAdapter,
      Func<IGenericEventAdapter<TClassicEventArgs, TGenericDelegate>, TClassicDelegate> adapterToHandler)
    {
      this._handlerToAdapter = handlerToAdapter;
      this._adapterToHandler = adapterToHandler;
    }

    public TClassicDelegate? GetClassicHandler(TGenericDelegate handler, bool remove = false)
    {
      if ((Delegate) handler == (Delegate) null)
        return default (TClassicDelegate);
      IEnumerable<TClassicDelegate> existingHandlers;
      if (this.TryGetExistingHandlers(out existingHandlers))
      {
        TClassicDelegate classicHandler = this.SearchAdapted(handler, existingHandlers);
        if ((Delegate) classicHandler != (Delegate) null)
          return classicHandler;
      }
      else
      {
        IEnumerable<TClassicDelegate> delayedHandlers;
        if (this.TryGetDelayedHandlers(out delayedHandlers))
        {
          TClassicDelegate classicHandler = this.SearchAdapted(handler, delayedHandlers);
          if ((Delegate) classicHandler != (Delegate) null)
            return classicHandler;
        }
      }
      return !remove ? this._adapterToHandler(this._handlerToAdapter(handler)) : default (TClassicDelegate);
    }

    private TClassicDelegate? SearchAdapted(
      TGenericDelegate genericHandler,
      IEnumerable<TClassicDelegate> classicHandlers)
    {
      foreach (TClassicDelegate classicHandler in classicHandlers)
      {
        if (classicHandler.Target is IGenericEventAdapter<TClassicEventArgs, TGenericDelegate> target && (Delegate) target.GenericHandler == (Delegate) genericHandler)
          return this._adapterToHandler(target);
      }
      return default (TClassicDelegate);
    }

    protected abstract bool TryGetExistingHandlers(
      [NotNullWhen(true)] out IEnumerable<TClassicDelegate>? existingHandlers);

    protected abstract bool TryGetDelayedHandlers([NotNullWhen(true)] out IEnumerable<TClassicDelegate>? delayedHandlers);

    protected class AbstractAdapter<TConcreteGenericEventArgs> : 
      IGenericEventAdapter<TClassicEventArgs, System.Action<TGenericEventArgs>>
      where TConcreteGenericEventArgs : Events.Event<TClassicEventArgs, TConcreteGenericEventArgs>, TGenericEventArgs, new()
    {
      public System.Action<TGenericEventArgs> GenericHandler { get; }

      public AbstractAdapter(System.Action<TGenericEventArgs> genericHandler)
      {
        this.GenericHandler = genericHandler;
      }

      public void Execute(PXCache cache, TClassicEventArgs args)
      {
        System.Action<TGenericEventArgs> genericHandler = this.GenericHandler;
        TConcreteGenericEventArgs genericEventArgs1 = new TConcreteGenericEventArgs();
        genericEventArgs1.Cache = cache;
        genericEventArgs1.Args = args;
        TGenericEventArgs genericEventArgs2 = (TGenericEventArgs) genericEventArgs1;
        genericHandler(genericEventArgs2);
      }
    }
  }
}
