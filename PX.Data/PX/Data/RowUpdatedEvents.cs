// Decompiled with JetBrains decompiler
// Type: PX.Data.RowUpdatedEvents
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
public abstract class RowUpdatedEvents(PXGraph graph) : 
  RowEventsBase<PXRowUpdatedEventArgs, PXRowUpdated>(graph)
{
  protected override PXRowUpdated? Get(PXCache cache) => cache._EventsRow.RowUpdated;

  protected override void Add(PXCache cache, PXRowUpdated handler) => cache.RowUpdated += handler;

  protected override void Remove(PXCache cache, PXRowUpdated handler)
  {
    cache.RowUpdated -= handler;
  }

  private protected override void SetProxy(
    PXCache cache,
    EventsBase<PXRowUpdatedEventArgs, PXRowUpdated, List<PXRowUpdated>>.Proxy proxy)
  {
    cache._EventsRow.RowUpdated = new PXRowUpdated(proxy.Intercept);
    cache._EventsRow._RowUpdatedList = (List<PXRowUpdated>) null;
  }

  private protected override void SetProxy(
    List<PXRowUpdated> delayed,
    EventsBase<PXRowUpdatedEventArgs, PXRowUpdated, List<PXRowUpdated>>.Proxy proxy)
  {
    delayed.Clear();
    delayed.Add(new PXRowUpdated(proxy.Intercept));
  }

  protected override void Add(List<PXRowUpdated> delayed, PXRowUpdated handler)
  {
    delayed.Add(handler);
  }

  protected override void Remove(List<PXRowUpdated> delayed, PXRowUpdated handler)
  {
    delayed.Remove(handler);
  }

  protected override void ApplyDelayed(PXCache cache, List<PXRowUpdated> delayed)
  {
    if (cache._EventsRow._RowUpdatedList != null)
    {
      cache._EventsRow._RowUpdatedList.AddRange((IEnumerable<PXRowUpdated>) delayed);
    }
    else
    {
      if (cache._EventsRow.RowUpdated != null)
        delayed.Insert(0, cache._EventsRow.RowUpdated);
      cache._EventsRow.RowUpdated = (PXRowUpdated) Delegate.Combine((Delegate[]) delayed.ToArray());
    }
  }

  public void AddAbstractHandler<TTable>(System.Action<AbstractEvents.IRowUpdated<TTable>> handler) where TTable : class, IBqlTable, new()
  {
    PXRowUpdated classicHandler = new RowUpdatedEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveAbstractHandler<TTable>(System.Action<AbstractEvents.IRowUpdated<TTable>> handler) where TTable : class, IBqlTable, new()
  {
    PXRowUpdated classicHandler = new RowUpdatedEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  public void AddAbstractHandler(System.Type table, System.Action<AbstractEvents.IRowUpdated<object>> handler)
  {
    PXRowUpdated classicHandler = new RowUpdatedEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(table, classicHandler);
  }

  public void RemoveAbstractHandler(System.Type table, System.Action<AbstractEvents.IRowUpdated<object>> handler)
  {
    PXRowUpdated classicHandler = new RowUpdatedEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(table, classicHandler);
  }

  private protected void AddHandler<TTable>(
    Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowUpdated classicHandler = new RowUpdatedEvents.GenericAuto<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveHandler<TTable>(
    Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowUpdated classicHandler = new RowUpdatedEvents.GenericAuto<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  private class GenericAbstractStrict<TTable>(RowUpdatedEvents parent) : 
    RowEventsBase<PXRowUpdatedEventArgs, PXRowUpdated>.GenericTable<AbstractEvents.IRowUpdated<TTable>, System.Action<AbstractEvents.IRowUpdated<TTable>>>((RowEventsBase<PXRowUpdatedEventArgs, PXRowUpdated>) parent, typeof (TTable), (Func<System.Action<AbstractEvents.IRowUpdated<TTable>>, IGenericEventAdapter<PXRowUpdatedEventArgs, System.Action<AbstractEvents.IRowUpdated<TTable>>>>) (handler => (IGenericEventAdapter<PXRowUpdatedEventArgs, System.Action<AbstractEvents.IRowUpdated<TTable>>>) new EventsBase<PXRowUpdatedEventArgs, PXRowUpdated, List<PXRowUpdated>>.Generic<AbstractEvents.IRowUpdated<TTable>, System.Action<AbstractEvents.IRowUpdated<TTable>>>.AbstractAdapter<Events.RowUpdated<TTable>>(handler)), (Func<IGenericEventAdapter<PXRowUpdatedEventArgs, System.Action<AbstractEvents.IRowUpdated<TTable>>>, PXRowUpdated>) (adapter => new PXRowUpdated(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }

  private class GenericAbstractRelaxed(RowUpdatedEvents parent, System.Type table) : 
    RowEventsBase<PXRowUpdatedEventArgs, PXRowUpdated>.GenericTable<AbstractEvents.IRowUpdated<object>, System.Action<AbstractEvents.IRowUpdated<object>>>((RowEventsBase<PXRowUpdatedEventArgs, PXRowUpdated>) parent, table, (Func<System.Action<AbstractEvents.IRowUpdated<object>>, IGenericEventAdapter<PXRowUpdatedEventArgs, System.Action<AbstractEvents.IRowUpdated<object>>>>) (handler => (IGenericEventAdapter<PXRowUpdatedEventArgs, System.Action<AbstractEvents.IRowUpdated<object>>>) new EventsBase<PXRowUpdatedEventArgs, PXRowUpdated, List<PXRowUpdated>>.Generic<AbstractEvents.IRowUpdated<object>, System.Action<AbstractEvents.IRowUpdated<object>>>.AbstractAdapter<AbstractEvents.RelaxedRowUpdated>(handler)), (Func<IGenericEventAdapter<PXRowUpdatedEventArgs, System.Action<AbstractEvents.IRowUpdated<object>>>, PXRowUpdated>) (adapter => new PXRowUpdated(adapter.Execute)))
  {
  }

  private class GenericAuto<TTable>(RowUpdatedEvents parent) : 
    RowEventsBase<PXRowUpdatedEventArgs, PXRowUpdated>.GenericTable<Events.RowUpdated<TTable>, Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TTable>>.EventDelegate>((RowEventsBase<PXRowUpdatedEventArgs, PXRowUpdated>) parent, typeof (TTable), (Func<Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TTable>>.EventDelegate, IGenericEventAdapter<PXRowUpdatedEventArgs, Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TTable>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXRowUpdatedEventArgs, Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TTable>>.EventDelegate>) new Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TTable>>.Adapter(handler)), (Func<IGenericEventAdapter<PXRowUpdatedEventArgs, Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TTable>>.EventDelegate>, PXRowUpdated>) (adapter => new PXRowUpdated(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }
}
