// Decompiled with JetBrains decompiler
// Type: PX.Data.RowDeletingEvents
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
public abstract class RowDeletingEvents(PXGraph graph) : 
  RowEventsBase<PXRowDeletingEventArgs, PXRowDeleting>(graph)
{
  protected override PXRowDeleting? Get(PXCache cache) => cache._EventsRow.RowDeleting;

  protected override void Add(PXCache cache, PXRowDeleting handler) => cache.RowDeleting += handler;

  protected override void Remove(PXCache cache, PXRowDeleting handler)
  {
    cache.RowDeleting -= handler;
  }

  private protected override void SetProxy(
    PXCache cache,
    EventsBase<PXRowDeletingEventArgs, PXRowDeleting, List<PXRowDeleting>>.Proxy proxy)
  {
    cache._EventsRow.RowDeleting = new PXRowDeleting(proxy.Intercept);
    cache._EventsRow._RowDeletingList = (List<PXRowDeleting>) null;
  }

  private protected override void SetProxy(
    List<PXRowDeleting> delayed,
    EventsBase<PXRowDeletingEventArgs, PXRowDeleting, List<PXRowDeleting>>.Proxy proxy)
  {
    delayed.Clear();
    delayed.Add(new PXRowDeleting(proxy.Intercept));
  }

  protected override void Add(List<PXRowDeleting> delayed, PXRowDeleting handler)
  {
    delayed.Insert(0, handler);
  }

  protected override void Remove(List<PXRowDeleting> delayed, PXRowDeleting handler)
  {
    delayed.Remove(handler);
  }

  protected override void ApplyDelayed(PXCache cache, List<PXRowDeleting> delayed)
  {
    if (cache._EventsRow._RowDeletingList != null)
    {
      cache._EventsRow._RowDeletingList.InsertRange(0, (IEnumerable<PXRowDeleting>) delayed);
    }
    else
    {
      if (cache._EventsRow.RowDeleting != null)
        delayed.Add(cache._EventsRow.RowDeleting);
      cache._EventsRow.RowDeleting = (PXRowDeleting) Delegate.Combine((Delegate[]) delayed.ToArray());
    }
  }

  public void AddAbstractHandler<TTable>(
    System.Action<AbstractEvents.IRowDeleting<TTable>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowDeleting classicHandler = new RowDeletingEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveAbstractHandler<TTable>(
    System.Action<AbstractEvents.IRowDeleting<TTable>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowDeleting classicHandler = new RowDeletingEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  public void AddAbstractHandler(
    System.Type table,
    System.Action<AbstractEvents.IRowDeleting<object>> handler)
  {
    PXRowDeleting classicHandler = new RowDeletingEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(table, classicHandler);
  }

  public void RemoveAbstractHandler(
    System.Type table,
    System.Action<AbstractEvents.IRowDeleting<object>> handler)
  {
    PXRowDeleting classicHandler = new RowDeletingEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(table, classicHandler);
  }

  private protected void AddHandler<TTable>(
    Events.Event<PXRowDeletingEventArgs, Events.RowDeleting<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowDeleting classicHandler = new RowDeletingEvents.GenericAuto<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveHandler<TTable>(
    Events.Event<PXRowDeletingEventArgs, Events.RowDeleting<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowDeleting classicHandler = new RowDeletingEvents.GenericAuto<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  private class GenericAbstractStrict<TTable>(RowDeletingEvents parent) : 
    RowEventsBase<PXRowDeletingEventArgs, PXRowDeleting>.GenericTable<AbstractEvents.IRowDeleting<TTable>, System.Action<AbstractEvents.IRowDeleting<TTable>>>((RowEventsBase<PXRowDeletingEventArgs, PXRowDeleting>) parent, typeof (TTable), (Func<System.Action<AbstractEvents.IRowDeleting<TTable>>, IGenericEventAdapter<PXRowDeletingEventArgs, System.Action<AbstractEvents.IRowDeleting<TTable>>>>) (handler => (IGenericEventAdapter<PXRowDeletingEventArgs, System.Action<AbstractEvents.IRowDeleting<TTable>>>) new EventsBase<PXRowDeletingEventArgs, PXRowDeleting, List<PXRowDeleting>>.Generic<AbstractEvents.IRowDeleting<TTable>, System.Action<AbstractEvents.IRowDeleting<TTable>>>.AbstractAdapter<Events.RowDeleting<TTable>>(handler)), (Func<IGenericEventAdapter<PXRowDeletingEventArgs, System.Action<AbstractEvents.IRowDeleting<TTable>>>, PXRowDeleting>) (adapter => new PXRowDeleting(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }

  private class GenericAbstractRelaxed(RowDeletingEvents parent, System.Type table) : 
    RowEventsBase<PXRowDeletingEventArgs, PXRowDeleting>.GenericTable<AbstractEvents.IRowDeleting<object>, System.Action<AbstractEvents.IRowDeleting<object>>>((RowEventsBase<PXRowDeletingEventArgs, PXRowDeleting>) parent, table, (Func<System.Action<AbstractEvents.IRowDeleting<object>>, IGenericEventAdapter<PXRowDeletingEventArgs, System.Action<AbstractEvents.IRowDeleting<object>>>>) (handler => (IGenericEventAdapter<PXRowDeletingEventArgs, System.Action<AbstractEvents.IRowDeleting<object>>>) new EventsBase<PXRowDeletingEventArgs, PXRowDeleting, List<PXRowDeleting>>.Generic<AbstractEvents.IRowDeleting<object>, System.Action<AbstractEvents.IRowDeleting<object>>>.AbstractAdapter<AbstractEvents.RelaxedRowDeleting>(handler)), (Func<IGenericEventAdapter<PXRowDeletingEventArgs, System.Action<AbstractEvents.IRowDeleting<object>>>, PXRowDeleting>) (adapter => new PXRowDeleting(adapter.Execute)))
  {
  }

  private class GenericAuto<TTable>(RowDeletingEvents parent) : 
    RowEventsBase<PXRowDeletingEventArgs, PXRowDeleting>.GenericTable<Events.RowDeleting<TTable>, Events.Event<PXRowDeletingEventArgs, Events.RowDeleting<TTable>>.EventDelegate>((RowEventsBase<PXRowDeletingEventArgs, PXRowDeleting>) parent, typeof (TTable), (Func<Events.Event<PXRowDeletingEventArgs, Events.RowDeleting<TTable>>.EventDelegate, IGenericEventAdapter<PXRowDeletingEventArgs, Events.Event<PXRowDeletingEventArgs, Events.RowDeleting<TTable>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXRowDeletingEventArgs, Events.Event<PXRowDeletingEventArgs, Events.RowDeleting<TTable>>.EventDelegate>) new Events.Event<PXRowDeletingEventArgs, Events.RowDeleting<TTable>>.Adapter(handler)), (Func<IGenericEventAdapter<PXRowDeletingEventArgs, Events.Event<PXRowDeletingEventArgs, Events.RowDeleting<TTable>>.EventDelegate>, PXRowDeleting>) (adapter => new PXRowDeleting(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }
}
