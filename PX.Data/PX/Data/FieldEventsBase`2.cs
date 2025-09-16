// Decompiled with JetBrains decompiler
// Type: PX.Data.FieldEventsBase`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

#nullable enable
namespace PX.Data;

/// <exclude />
public abstract class FieldEventsBase<TClassicEventArgs, TClassicDelegate> : 
  EventsBase<TClassicEventArgs, TClassicDelegate, PXCache.EventDictionary<TClassicDelegate>>
  where TClassicEventArgs : EventArgs
  where TClassicDelegate : Delegate
{
  private protected FieldEventsBase(PXGraph graph)
    : base(graph)
  {
  }

  protected abstract TClassicDelegate? Get(PXCache cache, string field);

  protected abstract void Add(PXCache cache, string field, TClassicDelegate handler);

  protected abstract void Remove(PXCache cache, string field, TClassicDelegate handler);

  public void AddHandler(System.Type table, string field, TClassicDelegate handler)
  {
    if (string.IsNullOrEmpty(field))
      return;
    string lower = field.ToLower();
    PXCache cache;
    if (this.Graph.Caches.TryGetValue(table, out cache))
    {
      this.Add(cache, lower, handler);
    }
    else
    {
      PXCache.EventDictionary<TClassicDelegate> delayed;
      if (!this.Delayed.Get(table, out delayed, true))
        return;
      this.Add(delayed, lower, handler);
    }
  }

  public void RemoveHandler(System.Type table, string field, TClassicDelegate handler)
  {
    if (string.IsNullOrEmpty(field))
      return;
    string lower = field.ToLower();
    PXCache cache;
    if (this.Graph.Caches.TryGetValue(table, out cache))
    {
      this.Remove(cache, lower, handler);
    }
    else
    {
      PXCache.EventDictionary<TClassicDelegate> delayed;
      if (!this.Delayed.Get(table, out delayed) || !delayed.ContainsKey(lower))
        return;
      this.Remove(delayed, lower, handler);
    }
  }

  public void AddHandler(string view, string field, TClassicDelegate handler)
  {
    this.AddHandler(this.Graph.Views[view].GetItemType(), field, handler);
  }

  public void RemoveHandler(string view, string field, TClassicDelegate handler)
  {
    this.RemoveHandler(this.Graph.Views[view].GetItemType(), field, handler);
  }

  public void AddHandler<TField>(TClassicDelegate handler)
  {
    this.AddHandler(BqlCommand.GetItemType(typeof (TField)), typeof (TField).Name, handler);
  }

  public void RemoveHandler<TField>(TClassicDelegate handler)
  {
    this.RemoveHandler(BqlCommand.GetItemType(typeof (TField)), typeof (TField).Name, handler);
  }

  internal void AddHandler(
    System.Type table,
    string field,
    EventsBase<TClassicEventArgs, TClassicDelegate, PXCache.EventDictionary<TClassicDelegate>>.Interceptor interceptor)
  {
    if (string.IsNullOrEmpty(field))
      return;
    string lower = field.ToLower();
    PXCache cache;
    if (this.Graph.Caches.TryGetValue(table, out cache))
    {
      this.SetProxy(cache, lower, new EventsBase<TClassicEventArgs, TClassicDelegate, PXCache.EventDictionary<TClassicDelegate>>.Proxy(interceptor, this.Get(cache, lower)));
    }
    else
    {
      PXCache.EventDictionary<TClassicDelegate> delayed;
      if (!this.Delayed.Get(table, out delayed, true))
        return;
      this.SetProxy(delayed, lower, new EventsBase<TClassicEventArgs, TClassicDelegate, PXCache.EventDictionary<TClassicDelegate>>.Proxy(interceptor, delayed[lower]));
    }
  }

  private protected abstract void SetProxy(
    PXCache cache,
    string field,
    EventsBase<TClassicEventArgs, TClassicDelegate, PXCache.EventDictionary<TClassicDelegate>>.Proxy proxy);

  private protected abstract void SetProxy(
    PXCache.EventDictionary<TClassicDelegate> delayed,
    string field,
    EventsBase<TClassicEventArgs, TClassicDelegate, PXCache.EventDictionary<TClassicDelegate>>.Proxy proxy);

  protected abstract void Add(
    PXCache.EventDictionary<TClassicDelegate> delayed,
    string field,
    TClassicDelegate handler);

  protected abstract void Remove(
    PXCache.EventDictionary<TClassicDelegate> delayed,
    string field,
    TClassicDelegate handler);

  protected override void ApplyDelayed(
    PXCache cache,
    PXCache.EventDictionary<TClassicDelegate> delayed)
  {
    foreach (KeyValuePair<string, TClassicDelegate> keyValuePair in (Dictionary<string, TClassicDelegate>) delayed)
    {
      string str;
      TClassicDelegate classicDelegate;
      EnumerableExtensions.Deconstruct<string, TClassicDelegate>(keyValuePair, ref str, ref classicDelegate);
      string field = str;
      TClassicDelegate handler = classicDelegate;
      this.Add(cache, field, handler);
    }
  }

  private protected abstract class GenericField<TGenericEventArgs, TGenericDelegate> : 
    EventsBase<TClassicEventArgs, TClassicDelegate, PXCache.EventDictionary<TClassicDelegate>>.Generic<TGenericEventArgs, TGenericDelegate>
    where TGenericEventArgs : class, IGenericEventWith<TClassicEventArgs>
    where TGenericDelegate : Delegate
  {
    private readonly FieldEventsBase<TClassicEventArgs, TClassicDelegate> _parent;

    public System.Type Table { get; }

    public string FieldName { get; }

    public GenericField(
      FieldEventsBase<TClassicEventArgs, TClassicDelegate> parent,
      System.Type table,
      string fieldName,
      Func<TGenericDelegate, IGenericEventAdapter<TClassicEventArgs, TGenericDelegate>> handlerToAdapter,
      Func<IGenericEventAdapter<TClassicEventArgs, TGenericDelegate>, TClassicDelegate> adapterToHandler)
      : base(handlerToAdapter, adapterToHandler)
    {
      this._parent = parent;
      this.Table = table;
      this.FieldName = fieldName;
    }

    protected override bool TryGetExistingHandlers(
      [NotNullWhen(true)] out IEnumerable<TClassicDelegate>? existingHandlers)
    {
      ref IEnumerable<TClassicDelegate> local = ref existingHandlers;
      PXCache cache;
      IEnumerable<TClassicDelegate> classicDelegates;
      if (this._parent.Graph.Caches.TryGetValue(this.Table, out cache))
      {
        TClassicDelegate classicDelegate = this._parent.Get(cache, this.FieldName);
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
      PXCache.EventDictionary<TClassicDelegate> delayed;
      TClassicDelegate classicDelegate;
      delayedHandlers = !this._parent.Delayed.Get(this.Table, out delayed) || !delayed.TryGetValue(this.FieldName, out classicDelegate) ? (IEnumerable<TClassicDelegate>) null : classicDelegate.GetInvocationList().OfType<TClassicDelegate>();
      return delayedHandlers != null;
    }
  }
}
