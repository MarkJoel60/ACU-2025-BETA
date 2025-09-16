// Decompiled with JetBrains decompiler
// Type: PX.Data.RowSelectingEvents
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
public abstract class RowSelectingEvents(PXGraph graph) : 
  RowEventsBase<PXRowSelectingEventArgs, PXRowSelecting>(graph)
{
  protected override PXRowSelecting? Get(PXCache cache) => cache._EventsRow.RowSelecting;

  protected override void Add(PXCache cache, PXRowSelecting handler)
  {
    cache.RowSelecting += handler;
  }

  protected override void Remove(PXCache cache, PXRowSelecting handler)
  {
    cache.RowSelecting -= handler;
  }

  private protected override void SetProxy(
    PXCache cache,
    EventsBase<PXRowSelectingEventArgs, PXRowSelecting, List<PXRowSelecting>>.Proxy proxy)
  {
    cache._EventsRow.RowSelecting = new PXRowSelecting(proxy.Intercept);
    cache._EventsRow._RowSelectingList = (List<PXRowSelecting>) null;
  }

  private protected override void SetProxy(
    List<PXRowSelecting> delayed,
    EventsBase<PXRowSelectingEventArgs, PXRowSelecting, List<PXRowSelecting>>.Proxy proxy)
  {
    delayed.Clear();
    delayed.Add(new PXRowSelecting(proxy.Intercept));
  }

  protected override void Add(List<PXRowSelecting> delayed, PXRowSelecting handler)
  {
    delayed.Add(handler);
  }

  protected override void Remove(List<PXRowSelecting> delayed, PXRowSelecting handler)
  {
    delayed.Remove(handler);
  }

  protected override void ApplyDelayed(PXCache cache, List<PXRowSelecting> delayed)
  {
    if (cache._EventsRow._RowSelectingList != null)
    {
      cache._EventsRow._RowSelectingList.AddRange((IEnumerable<PXRowSelecting>) delayed);
    }
    else
    {
      if (cache._EventsRow.RowSelecting != null)
        delayed.Insert(0, cache._EventsRow.RowSelecting);
      cache._EventsRow.RowSelecting = (PXRowSelecting) Delegate.Combine((Delegate[]) delayed.ToArray());
    }
  }

  public void AddAbstractHandler<TTable>(
    System.Action<AbstractEvents.IRowSelecting<TTable>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowSelecting classicHandler = new RowSelectingEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveAbstractHandler<TTable>(
    System.Action<AbstractEvents.IRowSelecting<TTable>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowSelecting classicHandler = new RowSelectingEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  public void AddAbstractHandler(
    System.Type table,
    System.Action<AbstractEvents.IRowSelecting<object>> handler)
  {
    PXRowSelecting classicHandler = new RowSelectingEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(table, classicHandler);
  }

  public void RemoveAbstractHandler(
    System.Type table,
    System.Action<AbstractEvents.IRowSelecting<object>> handler)
  {
    PXRowSelecting classicHandler = new RowSelectingEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(table, classicHandler);
  }

  private protected void AddHandler<TTable>(
    Events.Event<PXRowSelectingEventArgs, Events.RowSelecting<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowSelecting classicHandler = new RowSelectingEvents.GenericAuto<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveHandler<TTable>(
    Events.Event<PXRowSelectingEventArgs, Events.RowSelecting<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowSelecting classicHandler = new RowSelectingEvents.GenericAuto<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  private class GenericAbstractStrict<TTable>(RowSelectingEvents parent) : 
    RowEventsBase<PXRowSelectingEventArgs, PXRowSelecting>.GenericTable<AbstractEvents.IRowSelecting<TTable>, System.Action<AbstractEvents.IRowSelecting<TTable>>>((RowEventsBase<PXRowSelectingEventArgs, PXRowSelecting>) parent, typeof (TTable), (Func<System.Action<AbstractEvents.IRowSelecting<TTable>>, IGenericEventAdapter<PXRowSelectingEventArgs, System.Action<AbstractEvents.IRowSelecting<TTable>>>>) (handler => (IGenericEventAdapter<PXRowSelectingEventArgs, System.Action<AbstractEvents.IRowSelecting<TTable>>>) new EventsBase<PXRowSelectingEventArgs, PXRowSelecting, List<PXRowSelecting>>.Generic<AbstractEvents.IRowSelecting<TTable>, System.Action<AbstractEvents.IRowSelecting<TTable>>>.AbstractAdapter<Events.RowSelecting<TTable>>(handler)), (Func<IGenericEventAdapter<PXRowSelectingEventArgs, System.Action<AbstractEvents.IRowSelecting<TTable>>>, PXRowSelecting>) (adapter => new PXRowSelecting(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }

  private class GenericAbstractRelaxed(RowSelectingEvents parent, System.Type table) : 
    RowEventsBase<PXRowSelectingEventArgs, PXRowSelecting>.GenericTable<AbstractEvents.IRowSelecting<object>, System.Action<AbstractEvents.IRowSelecting<object>>>((RowEventsBase<PXRowSelectingEventArgs, PXRowSelecting>) parent, table, (Func<System.Action<AbstractEvents.IRowSelecting<object>>, IGenericEventAdapter<PXRowSelectingEventArgs, System.Action<AbstractEvents.IRowSelecting<object>>>>) (handler => (IGenericEventAdapter<PXRowSelectingEventArgs, System.Action<AbstractEvents.IRowSelecting<object>>>) new EventsBase<PXRowSelectingEventArgs, PXRowSelecting, List<PXRowSelecting>>.Generic<AbstractEvents.IRowSelecting<object>, System.Action<AbstractEvents.IRowSelecting<object>>>.AbstractAdapter<AbstractEvents.RelaxedRowSelecting>(handler)), (Func<IGenericEventAdapter<PXRowSelectingEventArgs, System.Action<AbstractEvents.IRowSelecting<object>>>, PXRowSelecting>) (adapter => new PXRowSelecting(adapter.Execute)))
  {
  }

  private class GenericAuto<TTable>(RowSelectingEvents parent) : 
    RowEventsBase<PXRowSelectingEventArgs, PXRowSelecting>.GenericTable<Events.RowSelecting<TTable>, Events.Event<PXRowSelectingEventArgs, Events.RowSelecting<TTable>>.EventDelegate>((RowEventsBase<PXRowSelectingEventArgs, PXRowSelecting>) parent, typeof (TTable), (Func<Events.Event<PXRowSelectingEventArgs, Events.RowSelecting<TTable>>.EventDelegate, IGenericEventAdapter<PXRowSelectingEventArgs, Events.Event<PXRowSelectingEventArgs, Events.RowSelecting<TTable>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXRowSelectingEventArgs, Events.Event<PXRowSelectingEventArgs, Events.RowSelecting<TTable>>.EventDelegate>) new Events.Event<PXRowSelectingEventArgs, Events.RowSelecting<TTable>>.Adapter(handler)), (Func<IGenericEventAdapter<PXRowSelectingEventArgs, Events.Event<PXRowSelectingEventArgs, Events.RowSelecting<TTable>>.EventDelegate>, PXRowSelecting>) (adapter => new PXRowSelecting(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }
}
