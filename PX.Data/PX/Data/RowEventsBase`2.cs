// Decompiled with JetBrains decompiler
// Type: PX.Data.RowEventsBase`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

#nullable enable
namespace PX.Data;

/// <exclude />
public abstract class RowEventsBase<TClassicEventArgs, TClassicDelegate> : 
  EventsBase<TClassicEventArgs, TClassicDelegate, List<TClassicDelegate>>
  where TClassicEventArgs : EventArgs
  where TClassicDelegate : Delegate
{
  private protected RowEventsBase(PXGraph graph)
    : base(graph)
  {
  }

  protected abstract TClassicDelegate? Get(PXCache cache);

  protected abstract void Add(PXCache cache, TClassicDelegate handler);

  protected abstract void Remove(PXCache cache, TClassicDelegate handler);

  public void AddHandler(System.Type table, TClassicDelegate handler)
  {
    PXCache cache;
    if (this.Graph.Caches.TryGetValue(table, out cache))
    {
      this.Add(cache, handler);
    }
    else
    {
      List<TClassicDelegate> delayed;
      if (!this.Delayed.Get(table, out delayed, true))
        return;
      this.Add(delayed, handler);
    }
  }

  public void RemoveHandler(System.Type table, TClassicDelegate handler)
  {
    PXCache cache;
    if (this.Graph.Caches.TryGetValue(table, out cache))
    {
      this.Remove(cache, handler);
    }
    else
    {
      List<TClassicDelegate> delayed;
      if (!this.Delayed.Get(table, out delayed))
        return;
      this.Remove(delayed, handler);
    }
  }

  public void AddHandler(string view, TClassicDelegate handler)
  {
    this.AddHandler(this.Graph.Views[view].GetItemType(), handler);
  }

  public void RemoveHandler(string view, TClassicDelegate handler)
  {
    this.RemoveHandler(this.Graph.Views[view].GetItemType(), handler);
  }

  public void AddHandler<TTable>(TClassicDelegate handler)
  {
    this.AddHandler(typeof (TTable), handler);
  }

  public void RemoveHandler<TTable>(TClassicDelegate handler)
  {
    this.RemoveHandler(typeof (TTable), handler);
  }

  internal void AddHandler(
    System.Type table,
    EventsBase<TClassicEventArgs, TClassicDelegate, List<TClassicDelegate>>.Interceptor interceptor)
  {
    PXCache cache;
    if (this.Graph.Caches.TryGetValue(table, out cache))
    {
      this.SetProxy(cache, new EventsBase<TClassicEventArgs, TClassicDelegate, List<TClassicDelegate>>.Proxy(interceptor, this.Get(cache)));
    }
    else
    {
      List<TClassicDelegate> delayed;
      if (!this.Delayed.Get(table, out delayed, true))
        return;
      this.SetProxy(delayed, new EventsBase<TClassicEventArgs, TClassicDelegate, List<TClassicDelegate>>.Proxy(interceptor, (TClassicDelegate) Delegate.Combine((Delegate[]) delayed.ToArray())));
    }
  }

  private protected abstract void SetProxy(
    PXCache cache,
    EventsBase<TClassicEventArgs, TClassicDelegate, List<TClassicDelegate>>.Proxy proxy);

  private protected abstract void SetProxy(
    List<TClassicDelegate> delayed,
    EventsBase<TClassicEventArgs, TClassicDelegate, List<TClassicDelegate>>.Proxy proxy);

  protected abstract void Add(List<TClassicDelegate> delayed, TClassicDelegate handler);

  protected abstract void Remove(List<TClassicDelegate> delayed, TClassicDelegate handler);

  private protected abstract class GenericTable<TGenericEventArgs, TGenericDelegate> : 
    EventsBase<TClassicEventArgs, TClassicDelegate, List<TClassicDelegate>>.Generic<TGenericEventArgs, TGenericDelegate>
    where TGenericEventArgs : class, IGenericEventWith<TClassicEventArgs>
    where TGenericDelegate : Delegate
  {
    private readonly RowEventsBase<TClassicEventArgs, TClassicDelegate> _parent;

    public System.Type Table { get; }

    public GenericTable(
      RowEventsBase<TClassicEventArgs, TClassicDelegate> parent,
      System.Type table,
      Func<TGenericDelegate, IGenericEventAdapter<TClassicEventArgs, TGenericDelegate>> handlerToAdapter,
      Func<IGenericEventAdapter<TClassicEventArgs, TGenericDelegate>, TClassicDelegate> adapterToHandler)
      : base(handlerToAdapter, adapterToHandler)
    {
      this._parent = parent;
      this.Table = table;
    }

    protected override bool TryGetExistingHandlers(
      [NotNullWhen(true)] out IEnumerable<TClassicDelegate>? existingHandlers)
    {
      ref IEnumerable<TClassicDelegate> local = ref existingHandlers;
      PXCache cache;
      IEnumerable<TClassicDelegate> classicDelegates;
      if (this._parent.Graph.Caches.TryGetValue(this.Table, out cache))
      {
        TClassicDelegate classicDelegate = this._parent.Get(cache);
        if ((object) classicDelegate != null)
        {
          classicDelegates = classicDelegate.GetInvocationList().OfType<TClassicDelegate>();
          goto label_4;
        }
      }
      classicDelegates = (IEnumerable<TClassicDelegate>) null;
label_4:
      local = classicDelegates;
      return existingHandlers != null;
    }

    protected override bool TryGetDelayedHandlers([NotNullWhen(true)] out IEnumerable<TClassicDelegate>? delayedHandlers)
    {
      List<TClassicDelegate> delayed;
      delayedHandlers = this._parent.Delayed.Get(this.Table, out delayed) ? (IEnumerable<TClassicDelegate>) delayed : (IEnumerable<TClassicDelegate>) null;
      return delayedHandlers != null;
    }
  }
}
