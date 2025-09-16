// Decompiled with JetBrains decompiler
// Type: PX.Data.RowPersistedEvents
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
public abstract class RowPersistedEvents(PXGraph graph) : 
  RowEventsBase<PXRowPersistedEventArgs, PXRowPersisted>(graph)
{
  protected override PXRowPersisted? Get(PXCache cache) => cache._EventsRow.RowPersisted;

  protected override void Add(PXCache cache, PXRowPersisted handler)
  {
    cache.RowPersisted += handler;
  }

  protected override void Remove(PXCache cache, PXRowPersisted handler)
  {
    cache.RowPersisted -= handler;
  }

  private protected override void SetProxy(
    PXCache cache,
    EventsBase<PXRowPersistedEventArgs, PXRowPersisted, List<PXRowPersisted>>.Proxy proxy)
  {
    cache._EventsRow.RowPersisted = new PXRowPersisted(proxy.Intercept);
    cache._EventsRow._RowPersistedList = (List<PXRowPersisted>) null;
  }

  private protected override void SetProxy(
    List<PXRowPersisted> delayed,
    EventsBase<PXRowPersistedEventArgs, PXRowPersisted, List<PXRowPersisted>>.Proxy proxy)
  {
    delayed.Clear();
    delayed.Add(new PXRowPersisted(proxy.Intercept));
  }

  protected override void Add(List<PXRowPersisted> delayed, PXRowPersisted handler)
  {
    delayed.Add(handler);
  }

  protected override void Remove(List<PXRowPersisted> delayed, PXRowPersisted handler)
  {
    delayed.Remove(handler);
  }

  protected override void ApplyDelayed(PXCache cache, List<PXRowPersisted> delayed)
  {
    if (cache._EventsRow._RowPersistedList != null)
    {
      cache._EventsRow._RowPersistedList.AddRange((IEnumerable<PXRowPersisted>) delayed);
    }
    else
    {
      if (cache._EventsRow.RowPersisted != null)
        delayed.Insert(0, cache._EventsRow.RowPersisted);
      cache._EventsRow.RowPersisted = (PXRowPersisted) Delegate.Combine((Delegate[]) delayed.ToArray());
    }
  }

  public void AddAbstractHandler<TTable>(
    System.Action<AbstractEvents.IRowPersisted<TTable>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowPersisted classicHandler = new RowPersistedEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveAbstractHandler<TTable>(
    System.Action<AbstractEvents.IRowPersisted<TTable>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowPersisted classicHandler = new RowPersistedEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  public void AddAbstractHandler(
    System.Type table,
    System.Action<AbstractEvents.IRowPersisted<object>> handler)
  {
    PXRowPersisted classicHandler = new RowPersistedEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(table, classicHandler);
  }

  public void RemoveAbstractHandler(
    System.Type table,
    System.Action<AbstractEvents.IRowPersisted<object>> handler)
  {
    PXRowPersisted classicHandler = new RowPersistedEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(table, classicHandler);
  }

  private protected void AddHandler<TTable>(
    Events.Event<PXRowPersistedEventArgs, Events.RowPersisted<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowPersisted classicHandler = new RowPersistedEvents.GenericAuto<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveHandler<TTable>(
    Events.Event<PXRowPersistedEventArgs, Events.RowPersisted<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowPersisted classicHandler = new RowPersistedEvents.GenericAuto<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  private class GenericAbstractStrict<TTable>(RowPersistedEvents parent) : 
    RowEventsBase<PXRowPersistedEventArgs, PXRowPersisted>.GenericTable<AbstractEvents.IRowPersisted<TTable>, System.Action<AbstractEvents.IRowPersisted<TTable>>>((RowEventsBase<PXRowPersistedEventArgs, PXRowPersisted>) parent, typeof (TTable), (Func<System.Action<AbstractEvents.IRowPersisted<TTable>>, IGenericEventAdapter<PXRowPersistedEventArgs, System.Action<AbstractEvents.IRowPersisted<TTable>>>>) (handler => (IGenericEventAdapter<PXRowPersistedEventArgs, System.Action<AbstractEvents.IRowPersisted<TTable>>>) new EventsBase<PXRowPersistedEventArgs, PXRowPersisted, List<PXRowPersisted>>.Generic<AbstractEvents.IRowPersisted<TTable>, System.Action<AbstractEvents.IRowPersisted<TTable>>>.AbstractAdapter<Events.RowPersisted<TTable>>(handler)), (Func<IGenericEventAdapter<PXRowPersistedEventArgs, System.Action<AbstractEvents.IRowPersisted<TTable>>>, PXRowPersisted>) (adapter => new PXRowPersisted(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }

  private class GenericAbstractRelaxed(RowPersistedEvents parent, System.Type table) : 
    RowEventsBase<PXRowPersistedEventArgs, PXRowPersisted>.GenericTable<AbstractEvents.IRowPersisted<object>, System.Action<AbstractEvents.IRowPersisted<object>>>((RowEventsBase<PXRowPersistedEventArgs, PXRowPersisted>) parent, table, (Func<System.Action<AbstractEvents.IRowPersisted<object>>, IGenericEventAdapter<PXRowPersistedEventArgs, System.Action<AbstractEvents.IRowPersisted<object>>>>) (handler => (IGenericEventAdapter<PXRowPersistedEventArgs, System.Action<AbstractEvents.IRowPersisted<object>>>) new EventsBase<PXRowPersistedEventArgs, PXRowPersisted, List<PXRowPersisted>>.Generic<AbstractEvents.IRowPersisted<object>, System.Action<AbstractEvents.IRowPersisted<object>>>.AbstractAdapter<AbstractEvents.RelaxedRowPersisted>(handler)), (Func<IGenericEventAdapter<PXRowPersistedEventArgs, System.Action<AbstractEvents.IRowPersisted<object>>>, PXRowPersisted>) (adapter => new PXRowPersisted(adapter.Execute)))
  {
  }

  private class GenericAuto<TTable>(RowPersistedEvents parent) : 
    RowEventsBase<PXRowPersistedEventArgs, PXRowPersisted>.GenericTable<Events.RowPersisted<TTable>, Events.Event<PXRowPersistedEventArgs, Events.RowPersisted<TTable>>.EventDelegate>((RowEventsBase<PXRowPersistedEventArgs, PXRowPersisted>) parent, typeof (TTable), (Func<Events.Event<PXRowPersistedEventArgs, Events.RowPersisted<TTable>>.EventDelegate, IGenericEventAdapter<PXRowPersistedEventArgs, Events.Event<PXRowPersistedEventArgs, Events.RowPersisted<TTable>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXRowPersistedEventArgs, Events.Event<PXRowPersistedEventArgs, Events.RowPersisted<TTable>>.EventDelegate>) new Events.Event<PXRowPersistedEventArgs, Events.RowPersisted<TTable>>.Adapter(handler)), (Func<IGenericEventAdapter<PXRowPersistedEventArgs, Events.Event<PXRowPersistedEventArgs, Events.RowPersisted<TTable>>.EventDelegate>, PXRowPersisted>) (adapter => new PXRowPersisted(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }
}
