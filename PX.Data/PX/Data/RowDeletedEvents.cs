// Decompiled with JetBrains decompiler
// Type: PX.Data.RowDeletedEvents
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
public abstract class RowDeletedEvents(PXGraph graph) : 
  RowEventsBase<PXRowDeletedEventArgs, PXRowDeleted>(graph)
{
  protected override PXRowDeleted? Get(PXCache cache) => cache._EventsRow.RowDeleted;

  protected override void Add(PXCache cache, PXRowDeleted handler) => cache.RowDeleted += handler;

  protected override void Remove(PXCache cache, PXRowDeleted handler)
  {
    cache.RowDeleted -= handler;
  }

  private protected override void SetProxy(
    PXCache cache,
    EventsBase<PXRowDeletedEventArgs, PXRowDeleted, List<PXRowDeleted>>.Proxy proxy)
  {
    cache._EventsRow.RowDeleted = new PXRowDeleted(proxy.Intercept);
    cache._EventsRow._RowDeletedList = (List<PXRowDeleted>) null;
  }

  private protected override void SetProxy(
    List<PXRowDeleted> delayed,
    EventsBase<PXRowDeletedEventArgs, PXRowDeleted, List<PXRowDeleted>>.Proxy proxy)
  {
    delayed.Clear();
    delayed.Add(new PXRowDeleted(proxy.Intercept));
  }

  protected override void Add(List<PXRowDeleted> delayed, PXRowDeleted handler)
  {
    delayed.Add(handler);
  }

  protected override void Remove(List<PXRowDeleted> delayed, PXRowDeleted handler)
  {
    delayed.Remove(handler);
  }

  protected override void ApplyDelayed(PXCache cache, List<PXRowDeleted> delayed)
  {
    if (cache._EventsRow._RowDeletedList != null)
    {
      cache._EventsRow._RowDeletedList.AddRange((IEnumerable<PXRowDeleted>) delayed);
    }
    else
    {
      if (cache._EventsRow.RowDeleted != null)
        delayed.Insert(0, cache._EventsRow.RowDeleted);
      cache._EventsRow.RowDeleted = (PXRowDeleted) Delegate.Combine((Delegate[]) delayed.ToArray());
    }
  }

  public void AddAbstractHandler<TTable>(System.Action<AbstractEvents.IRowDeleted<TTable>> handler) where TTable : class, IBqlTable, new()
  {
    PXRowDeleted classicHandler = new RowDeletedEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveAbstractHandler<TTable>(System.Action<AbstractEvents.IRowDeleted<TTable>> handler) where TTable : class, IBqlTable, new()
  {
    PXRowDeleted classicHandler = new RowDeletedEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  public void AddAbstractHandler(System.Type table, System.Action<AbstractEvents.IRowDeleted<object>> handler)
  {
    PXRowDeleted classicHandler = new RowDeletedEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(table, classicHandler);
  }

  public void RemoveAbstractHandler(System.Type table, System.Action<AbstractEvents.IRowDeleted<object>> handler)
  {
    PXRowDeleted classicHandler = new RowDeletedEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(table, classicHandler);
  }

  private protected void AddHandler<TTable>(
    Events.Event<PXRowDeletedEventArgs, Events.RowDeleted<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowDeleted classicHandler = new RowDeletedEvents.GenericAuto<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveHandler<TTable>(
    Events.Event<PXRowDeletedEventArgs, Events.RowDeleted<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowDeleted classicHandler = new RowDeletedEvents.GenericAuto<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  private class GenericAbstractStrict<TTable>(RowDeletedEvents parent) : 
    RowEventsBase<PXRowDeletedEventArgs, PXRowDeleted>.GenericTable<AbstractEvents.IRowDeleted<TTable>, System.Action<AbstractEvents.IRowDeleted<TTable>>>((RowEventsBase<PXRowDeletedEventArgs, PXRowDeleted>) parent, typeof (TTable), (Func<System.Action<AbstractEvents.IRowDeleted<TTable>>, IGenericEventAdapter<PXRowDeletedEventArgs, System.Action<AbstractEvents.IRowDeleted<TTable>>>>) (handler => (IGenericEventAdapter<PXRowDeletedEventArgs, System.Action<AbstractEvents.IRowDeleted<TTable>>>) new EventsBase<PXRowDeletedEventArgs, PXRowDeleted, List<PXRowDeleted>>.Generic<AbstractEvents.IRowDeleted<TTable>, System.Action<AbstractEvents.IRowDeleted<TTable>>>.AbstractAdapter<Events.RowDeleted<TTable>>(handler)), (Func<IGenericEventAdapter<PXRowDeletedEventArgs, System.Action<AbstractEvents.IRowDeleted<TTable>>>, PXRowDeleted>) (adapter => new PXRowDeleted(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }

  private class GenericAbstractRelaxed(RowDeletedEvents parent, System.Type table) : 
    RowEventsBase<PXRowDeletedEventArgs, PXRowDeleted>.GenericTable<AbstractEvents.IRowDeleted<object>, System.Action<AbstractEvents.IRowDeleted<object>>>((RowEventsBase<PXRowDeletedEventArgs, PXRowDeleted>) parent, table, (Func<System.Action<AbstractEvents.IRowDeleted<object>>, IGenericEventAdapter<PXRowDeletedEventArgs, System.Action<AbstractEvents.IRowDeleted<object>>>>) (handler => (IGenericEventAdapter<PXRowDeletedEventArgs, System.Action<AbstractEvents.IRowDeleted<object>>>) new EventsBase<PXRowDeletedEventArgs, PXRowDeleted, List<PXRowDeleted>>.Generic<AbstractEvents.IRowDeleted<object>, System.Action<AbstractEvents.IRowDeleted<object>>>.AbstractAdapter<AbstractEvents.RelaxedRowDeleted>(handler)), (Func<IGenericEventAdapter<PXRowDeletedEventArgs, System.Action<AbstractEvents.IRowDeleted<object>>>, PXRowDeleted>) (adapter => new PXRowDeleted(adapter.Execute)))
  {
  }

  private class GenericAuto<TTable>(RowDeletedEvents parent) : 
    RowEventsBase<PXRowDeletedEventArgs, PXRowDeleted>.GenericTable<Events.RowDeleted<TTable>, Events.Event<PXRowDeletedEventArgs, Events.RowDeleted<TTable>>.EventDelegate>((RowEventsBase<PXRowDeletedEventArgs, PXRowDeleted>) parent, typeof (TTable), (Func<Events.Event<PXRowDeletedEventArgs, Events.RowDeleted<TTable>>.EventDelegate, IGenericEventAdapter<PXRowDeletedEventArgs, Events.Event<PXRowDeletedEventArgs, Events.RowDeleted<TTable>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXRowDeletedEventArgs, Events.Event<PXRowDeletedEventArgs, Events.RowDeleted<TTable>>.EventDelegate>) new Events.Event<PXRowDeletedEventArgs, Events.RowDeleted<TTable>>.Adapter(handler)), (Func<IGenericEventAdapter<PXRowDeletedEventArgs, Events.Event<PXRowDeletedEventArgs, Events.RowDeleted<TTable>>.EventDelegate>, PXRowDeleted>) (adapter => new PXRowDeleted(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }
}
