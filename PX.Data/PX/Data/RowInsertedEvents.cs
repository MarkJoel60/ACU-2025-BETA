// Decompiled with JetBrains decompiler
// Type: PX.Data.RowInsertedEvents
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
public abstract class RowInsertedEvents(PXGraph graph) : 
  RowEventsBase<PXRowInsertedEventArgs, PXRowInserted>(graph)
{
  protected override PXRowInserted? Get(PXCache cache) => cache._EventsRow.RowInserted;

  protected override void Add(PXCache cache, PXRowInserted handler) => cache.RowInserted += handler;

  protected override void Remove(PXCache cache, PXRowInserted handler)
  {
    cache.RowInserted -= handler;
  }

  private protected override void SetProxy(
    PXCache cache,
    EventsBase<PXRowInsertedEventArgs, PXRowInserted, List<PXRowInserted>>.Proxy proxy)
  {
    cache._EventsRow.RowInserted = new PXRowInserted(proxy.Intercept);
    cache._EventsRow._RowInsertedList = (List<PXRowInserted>) null;
  }

  private protected override void SetProxy(
    List<PXRowInserted> delayed,
    EventsBase<PXRowInsertedEventArgs, PXRowInserted, List<PXRowInserted>>.Proxy proxy)
  {
    delayed.Clear();
    delayed.Add(new PXRowInserted(proxy.Intercept));
  }

  protected override void Add(List<PXRowInserted> delayed, PXRowInserted handler)
  {
    delayed.Add(handler);
  }

  protected override void Remove(List<PXRowInserted> delayed, PXRowInserted handler)
  {
    delayed.Remove(handler);
  }

  protected override void ApplyDelayed(PXCache cache, List<PXRowInserted> delayed)
  {
    if (cache._EventsRow._RowInsertedList != null)
    {
      cache._EventsRow._RowInsertedList.AddRange((IEnumerable<PXRowInserted>) delayed);
    }
    else
    {
      if (cache._EventsRow.RowInserted != null)
        delayed.Insert(0, cache._EventsRow.RowInserted);
      cache._EventsRow.RowInserted = (PXRowInserted) Delegate.Combine((Delegate[]) delayed.ToArray());
    }
  }

  public void AddAbstractHandler<TTable>(
    System.Action<AbstractEvents.IRowInserted<TTable>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowInserted classicHandler = new RowInsertedEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveAbstractHandler<TTable>(
    System.Action<AbstractEvents.IRowInserted<TTable>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowInserted classicHandler = new RowInsertedEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  public void AddAbstractHandler(
    System.Type table,
    System.Action<AbstractEvents.IRowInserted<object>> handler)
  {
    PXRowInserted classicHandler = new RowInsertedEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(table, classicHandler);
  }

  public void RemoveAbstractHandler(
    System.Type table,
    System.Action<AbstractEvents.IRowInserted<object>> handler)
  {
    PXRowInserted classicHandler = new RowInsertedEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(table, classicHandler);
  }

  private protected void AddHandler<TTable>(
    Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowInserted classicHandler = new RowInsertedEvents.GenericAuto<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveHandler<TTable>(
    Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowInserted classicHandler = new RowInsertedEvents.GenericAuto<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  private class GenericAbstractStrict<TTable>(RowInsertedEvents parent) : 
    RowEventsBase<PXRowInsertedEventArgs, PXRowInserted>.GenericTable<AbstractEvents.IRowInserted<TTable>, System.Action<AbstractEvents.IRowInserted<TTable>>>((RowEventsBase<PXRowInsertedEventArgs, PXRowInserted>) parent, typeof (TTable), (Func<System.Action<AbstractEvents.IRowInserted<TTable>>, IGenericEventAdapter<PXRowInsertedEventArgs, System.Action<AbstractEvents.IRowInserted<TTable>>>>) (handler => (IGenericEventAdapter<PXRowInsertedEventArgs, System.Action<AbstractEvents.IRowInserted<TTable>>>) new EventsBase<PXRowInsertedEventArgs, PXRowInserted, List<PXRowInserted>>.Generic<AbstractEvents.IRowInserted<TTable>, System.Action<AbstractEvents.IRowInserted<TTable>>>.AbstractAdapter<Events.RowInserted<TTable>>(handler)), (Func<IGenericEventAdapter<PXRowInsertedEventArgs, System.Action<AbstractEvents.IRowInserted<TTable>>>, PXRowInserted>) (adapter => new PXRowInserted(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }

  private class GenericAbstractRelaxed(RowInsertedEvents parent, System.Type table) : 
    RowEventsBase<PXRowInsertedEventArgs, PXRowInserted>.GenericTable<AbstractEvents.IRowInserted<object>, System.Action<AbstractEvents.IRowInserted<object>>>((RowEventsBase<PXRowInsertedEventArgs, PXRowInserted>) parent, table, (Func<System.Action<AbstractEvents.IRowInserted<object>>, IGenericEventAdapter<PXRowInsertedEventArgs, System.Action<AbstractEvents.IRowInserted<object>>>>) (handler => (IGenericEventAdapter<PXRowInsertedEventArgs, System.Action<AbstractEvents.IRowInserted<object>>>) new EventsBase<PXRowInsertedEventArgs, PXRowInserted, List<PXRowInserted>>.Generic<AbstractEvents.IRowInserted<object>, System.Action<AbstractEvents.IRowInserted<object>>>.AbstractAdapter<AbstractEvents.RelaxedRowInserted>(handler)), (Func<IGenericEventAdapter<PXRowInsertedEventArgs, System.Action<AbstractEvents.IRowInserted<object>>>, PXRowInserted>) (adapter => new PXRowInserted(adapter.Execute)))
  {
  }

  private class GenericAuto<TTable>(RowInsertedEvents parent) : 
    RowEventsBase<PXRowInsertedEventArgs, PXRowInserted>.GenericTable<Events.RowInserted<TTable>, Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TTable>>.EventDelegate>((RowEventsBase<PXRowInsertedEventArgs, PXRowInserted>) parent, typeof (TTable), (Func<Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TTable>>.EventDelegate, IGenericEventAdapter<PXRowInsertedEventArgs, Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TTable>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXRowInsertedEventArgs, Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TTable>>.EventDelegate>) new Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TTable>>.Adapter(handler)), (Func<IGenericEventAdapter<PXRowInsertedEventArgs, Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TTable>>.EventDelegate>, PXRowInserted>) (adapter => new PXRowInserted(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }
}
