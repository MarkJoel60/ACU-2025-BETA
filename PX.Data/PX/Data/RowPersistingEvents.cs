// Decompiled with JetBrains decompiler
// Type: PX.Data.RowPersistingEvents
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
public abstract class RowPersistingEvents(PXGraph graph) : 
  RowEventsBase<PXRowPersistingEventArgs, PXRowPersisting>(graph)
{
  protected override PXRowPersisting? Get(PXCache cache) => cache._EventsRow.RowPersisting;

  protected override void Add(PXCache cache, PXRowPersisting handler)
  {
    cache.RowPersisting += handler;
  }

  protected override void Remove(PXCache cache, PXRowPersisting handler)
  {
    cache.RowPersisting -= handler;
  }

  private protected override void SetProxy(
    PXCache cache,
    EventsBase<PXRowPersistingEventArgs, PXRowPersisting, List<PXRowPersisting>>.Proxy proxy)
  {
    cache._EventsRow.RowPersisting = new PXRowPersisting(proxy.Intercept);
    cache._EventsRow._RowPersistingList = (List<PXRowPersisting>) null;
  }

  private protected override void SetProxy(
    List<PXRowPersisting> delayed,
    EventsBase<PXRowPersistingEventArgs, PXRowPersisting, List<PXRowPersisting>>.Proxy proxy)
  {
    delayed.Clear();
    delayed.Add(new PXRowPersisting(proxy.Intercept));
  }

  protected override void Add(List<PXRowPersisting> delayed, PXRowPersisting handler)
  {
    delayed.Insert(0, handler);
  }

  protected override void Remove(List<PXRowPersisting> delayed, PXRowPersisting handler)
  {
    delayed.Remove(handler);
  }

  protected override void ApplyDelayed(PXCache cache, List<PXRowPersisting> delayed)
  {
    if (cache._EventsRow._RowPersistingList != null)
    {
      cache._EventsRow._RowPersistingList.InsertRange(0, (IEnumerable<PXRowPersisting>) delayed);
    }
    else
    {
      if (cache._EventsRow.RowPersisting != null)
        delayed.Add(cache._EventsRow.RowPersisting);
      cache._EventsRow.RowPersisting = (PXRowPersisting) Delegate.Combine((Delegate[]) delayed.ToArray());
    }
  }

  public void AddAbstractHandler<TTable>(
    System.Action<AbstractEvents.IRowPersisting<TTable>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowPersisting classicHandler = new RowPersistingEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveAbstractHandler<TTable>(
    System.Action<AbstractEvents.IRowPersisting<TTable>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowPersisting classicHandler = new RowPersistingEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  public void AddAbstractHandler(
    System.Type table,
    System.Action<AbstractEvents.IRowPersisting<object>> handler)
  {
    PXRowPersisting classicHandler = new RowPersistingEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(table, classicHandler);
  }

  public void RemoveAbstractHandler(
    System.Type table,
    System.Action<AbstractEvents.IRowPersisting<object>> handler)
  {
    PXRowPersisting classicHandler = new RowPersistingEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(table, classicHandler);
  }

  private protected void AddHandler<TTable>(
    Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowPersisting classicHandler = new RowPersistingEvents.GenericAuto<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveHandler<TTable>(
    Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowPersisting classicHandler = new RowPersistingEvents.GenericAuto<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  private class GenericAbstractStrict<TTable>(RowPersistingEvents parent) : 
    RowEventsBase<PXRowPersistingEventArgs, PXRowPersisting>.GenericTable<AbstractEvents.IRowPersisting<TTable>, System.Action<AbstractEvents.IRowPersisting<TTable>>>((RowEventsBase<PXRowPersistingEventArgs, PXRowPersisting>) parent, typeof (TTable), (Func<System.Action<AbstractEvents.IRowPersisting<TTable>>, IGenericEventAdapter<PXRowPersistingEventArgs, System.Action<AbstractEvents.IRowPersisting<TTable>>>>) (handler => (IGenericEventAdapter<PXRowPersistingEventArgs, System.Action<AbstractEvents.IRowPersisting<TTable>>>) new EventsBase<PXRowPersistingEventArgs, PXRowPersisting, List<PXRowPersisting>>.Generic<AbstractEvents.IRowPersisting<TTable>, System.Action<AbstractEvents.IRowPersisting<TTable>>>.AbstractAdapter<Events.RowPersisting<TTable>>(handler)), (Func<IGenericEventAdapter<PXRowPersistingEventArgs, System.Action<AbstractEvents.IRowPersisting<TTable>>>, PXRowPersisting>) (adapter => new PXRowPersisting(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }

  private class GenericAbstractRelaxed(RowPersistingEvents parent, System.Type table) : 
    RowEventsBase<PXRowPersistingEventArgs, PXRowPersisting>.GenericTable<AbstractEvents.IRowPersisting<object>, System.Action<AbstractEvents.IRowPersisting<object>>>((RowEventsBase<PXRowPersistingEventArgs, PXRowPersisting>) parent, table, (Func<System.Action<AbstractEvents.IRowPersisting<object>>, IGenericEventAdapter<PXRowPersistingEventArgs, System.Action<AbstractEvents.IRowPersisting<object>>>>) (handler => (IGenericEventAdapter<PXRowPersistingEventArgs, System.Action<AbstractEvents.IRowPersisting<object>>>) new EventsBase<PXRowPersistingEventArgs, PXRowPersisting, List<PXRowPersisting>>.Generic<AbstractEvents.IRowPersisting<object>, System.Action<AbstractEvents.IRowPersisting<object>>>.AbstractAdapter<AbstractEvents.RelaxedRowPersisting>(handler)), (Func<IGenericEventAdapter<PXRowPersistingEventArgs, System.Action<AbstractEvents.IRowPersisting<object>>>, PXRowPersisting>) (adapter => new PXRowPersisting(adapter.Execute)))
  {
  }

  private class GenericAuto<TTable>(RowPersistingEvents parent) : 
    RowEventsBase<PXRowPersistingEventArgs, PXRowPersisting>.GenericTable<Events.RowPersisting<TTable>, Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TTable>>.EventDelegate>((RowEventsBase<PXRowPersistingEventArgs, PXRowPersisting>) parent, typeof (TTable), (Func<Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TTable>>.EventDelegate, IGenericEventAdapter<PXRowPersistingEventArgs, Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TTable>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXRowPersistingEventArgs, Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TTable>>.EventDelegate>) new Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TTable>>.Adapter(handler)), (Func<IGenericEventAdapter<PXRowPersistingEventArgs, Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TTable>>.EventDelegate>, PXRowPersisting>) (adapter => new PXRowPersisting(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }
}
