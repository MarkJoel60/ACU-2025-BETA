// Decompiled with JetBrains decompiler
// Type: PX.Data.RowInsertingEvents
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Data;

/// <exclude />
/// <exclude />
public abstract class RowInsertingEvents(PXGraph graph) : 
  RowEventsBase<PXRowInsertingEventArgs, PXRowInserting>(graph)
{
  protected override PXRowInserting? Get(PXCache cache) => cache._EventsRow.RowInserting;

  protected override void Add(PXCache cache, PXRowInserting handler)
  {
    cache.RowInserting += handler;
  }

  protected override void Remove(PXCache cache, PXRowInserting handler)
  {
    cache.RowInserting -= handler;
  }

  private protected override void SetProxy(
    PXCache cache,
    EventsBase<PXRowInsertingEventArgs, PXRowInserting, List<PXRowInserting>>.Proxy proxy)
  {
    cache._EventsRow.RowInserting = new PXRowInserting(proxy.Intercept);
    cache._EventsRow._RowInsertingList = (List<PXRowInserting>) null;
  }

  private protected override void SetProxy(
    List<PXRowInserting> delayed,
    EventsBase<PXRowInsertingEventArgs, PXRowInserting, List<PXRowInserting>>.Proxy proxy)
  {
    delayed.Clear();
    delayed.Add(new PXRowInserting(proxy.Intercept));
  }

  protected override void Add(List<PXRowInserting> delayed, PXRowInserting handler)
  {
    delayed.Insert(0, handler);
  }

  protected override void Remove(List<PXRowInserting> delayed, PXRowInserting handler)
  {
    delayed.Remove(handler);
  }

  protected override void ApplyDelayed(PXCache cache, List<PXRowInserting> delayed)
  {
    if (cache._EventsRow._RowInsertingList != null)
    {
      cache._EventsRow._RowInsertingList.InsertRange(0, (IEnumerable<PXRowInserting>) delayed);
    }
    else
    {
      if (cache._EventsRow.RowInserting != null)
        delayed.Add(cache._EventsRow.RowInserting);
      cache._EventsRow.RowInserting = (PXRowInserting) Delegate.Combine((Delegate[]) delayed.ToArray());
    }
  }

  public void AddAbstractHandler<TTable>(
    System.Action<AbstractEvents.IRowInserting<TTable>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowInserting classicHandler = new RowInsertingEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveAbstractHandler<TTable>(
    System.Action<AbstractEvents.IRowInserting<TTable>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowInserting classicHandler = new RowInsertingEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  public void AddAbstractHandler(
    System.Type table,
    System.Action<AbstractEvents.IRowInserting<object>> handler)
  {
    PXRowInserting classicHandler = new RowInsertingEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(table, classicHandler);
  }

  public void RemoveAbstractHandler(
    System.Type table,
    System.Action<AbstractEvents.IRowInserting<object>> handler)
  {
    PXRowInserting classicHandler = new RowInsertingEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(table, classicHandler);
  }

  private protected void AddHandler<TTable>(
    Events.Event<PXRowInsertingEventArgs, Events.RowInserting<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowInserting classicHandler = new RowInsertingEvents.GenericAuto<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveHandler<TTable>(
    Events.Event<PXRowInsertingEventArgs, Events.RowInserting<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowInserting classicHandler = new RowInsertingEvents.GenericAuto<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  private class GenericAbstractStrict<TTable>(RowInsertingEvents parent) : 
    RowEventsBase<PXRowInsertingEventArgs, PXRowInserting>.GenericTable<AbstractEvents.IRowInserting<TTable>, System.Action<AbstractEvents.IRowInserting<TTable>>>((RowEventsBase<PXRowInsertingEventArgs, PXRowInserting>) parent, typeof (TTable), (Func<System.Action<AbstractEvents.IRowInserting<TTable>>, IGenericEventAdapter<PXRowInsertingEventArgs, System.Action<AbstractEvents.IRowInserting<TTable>>>>) (handler => (IGenericEventAdapter<PXRowInsertingEventArgs, System.Action<AbstractEvents.IRowInserting<TTable>>>) new EventsBase<PXRowInsertingEventArgs, PXRowInserting, List<PXRowInserting>>.Generic<AbstractEvents.IRowInserting<TTable>, System.Action<AbstractEvents.IRowInserting<TTable>>>.AbstractAdapter<Events.RowInserting<TTable>>(handler)), (Func<IGenericEventAdapter<PXRowInsertingEventArgs, System.Action<AbstractEvents.IRowInserting<TTable>>>, PXRowInserting>) (adapter => new PXRowInserting(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }

  private class GenericAbstractRelaxed(RowInsertingEvents parent, System.Type table) : 
    RowEventsBase<PXRowInsertingEventArgs, PXRowInserting>.GenericTable<AbstractEvents.IRowInserting<object>, System.Action<AbstractEvents.IRowInserting<object>>>((RowEventsBase<PXRowInsertingEventArgs, PXRowInserting>) parent, table, (Func<System.Action<AbstractEvents.IRowInserting<object>>, IGenericEventAdapter<PXRowInsertingEventArgs, System.Action<AbstractEvents.IRowInserting<object>>>>) (handler => (IGenericEventAdapter<PXRowInsertingEventArgs, System.Action<AbstractEvents.IRowInserting<object>>>) new EventsBase<PXRowInsertingEventArgs, PXRowInserting, List<PXRowInserting>>.Generic<AbstractEvents.IRowInserting<object>, System.Action<AbstractEvents.IRowInserting<object>>>.AbstractAdapter<AbstractEvents.RelaxedRowInserting>(handler)), (Func<IGenericEventAdapter<PXRowInsertingEventArgs, System.Action<AbstractEvents.IRowInserting<object>>>, PXRowInserting>) (adapter => new PXRowInserting(adapter.Execute)))
  {
  }

  private class GenericAuto<TTable>(RowInsertingEvents parent) : 
    RowEventsBase<PXRowInsertingEventArgs, PXRowInserting>.GenericTable<Events.RowInserting<TTable>, Events.Event<PXRowInsertingEventArgs, Events.RowInserting<TTable>>.EventDelegate>((RowEventsBase<PXRowInsertingEventArgs, PXRowInserting>) parent, typeof (TTable), (Func<Events.Event<PXRowInsertingEventArgs, Events.RowInserting<TTable>>.EventDelegate, IGenericEventAdapter<PXRowInsertingEventArgs, Events.Event<PXRowInsertingEventArgs, Events.RowInserting<TTable>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXRowInsertingEventArgs, Events.Event<PXRowInsertingEventArgs, Events.RowInserting<TTable>>.EventDelegate>) new Events.Event<PXRowInsertingEventArgs, Events.RowInserting<TTable>>.Adapter(handler)), (Func<IGenericEventAdapter<PXRowInsertingEventArgs, Events.Event<PXRowInsertingEventArgs, Events.RowInserting<TTable>>.EventDelegate>, PXRowInserting>) (adapter => new PXRowInserting(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }
}
