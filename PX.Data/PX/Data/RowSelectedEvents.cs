// Decompiled with JetBrains decompiler
// Type: PX.Data.RowSelectedEvents
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
public abstract class RowSelectedEvents(PXGraph graph) : 
  RowEventsBase<PXRowSelectedEventArgs, PXRowSelected>(graph)
{
  protected override PXRowSelected? Get(PXCache cache) => cache._EventsRow.RowSelected;

  protected override void Add(PXCache cache, PXRowSelected handler) => cache.RowSelected += handler;

  protected override void Remove(PXCache cache, PXRowSelected handler)
  {
    cache.RowSelected -= handler;
  }

  private protected override void SetProxy(
    PXCache cache,
    EventsBase<PXRowSelectedEventArgs, PXRowSelected, List<PXRowSelected>>.Proxy proxy)
  {
    cache._EventsRow.RowSelected = new PXRowSelected(proxy.Intercept);
    cache._EventsRow._RowSelectedList = (List<PXRowSelected>) null;
  }

  private protected override void SetProxy(
    List<PXRowSelected> delayed,
    EventsBase<PXRowSelectedEventArgs, PXRowSelected, List<PXRowSelected>>.Proxy proxy)
  {
    delayed.Clear();
    delayed.Add(new PXRowSelected(proxy.Intercept));
  }

  protected override void Add(List<PXRowSelected> delayed, PXRowSelected handler)
  {
    delayed.Add(handler);
  }

  protected override void Remove(List<PXRowSelected> delayed, PXRowSelected handler)
  {
    delayed.Remove(handler);
  }

  protected override void ApplyDelayed(PXCache cache, List<PXRowSelected> delayed)
  {
    if (cache._EventsRow._RowSelectedList != null)
    {
      cache._EventsRow._RowSelectedList.AddRange((IEnumerable<PXRowSelected>) delayed);
    }
    else
    {
      if (cache._EventsRow.RowSelected != null)
        delayed.Insert(0, cache._EventsRow.RowSelected);
      cache._EventsRow.RowSelected = (PXRowSelected) Delegate.Combine((Delegate[]) delayed.ToArray());
    }
  }

  public void AddAbstractHandler<TTable>(
    System.Action<AbstractEvents.IRowSelected<TTable>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowSelected classicHandler = new RowSelectedEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveAbstractHandler<TTable>(
    System.Action<AbstractEvents.IRowSelected<TTable>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowSelected classicHandler = new RowSelectedEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  public void AddAbstractHandler(
    System.Type table,
    System.Action<AbstractEvents.IRowSelected<object>> handler)
  {
    PXRowSelected classicHandler = new RowSelectedEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(table, classicHandler);
  }

  public void RemoveAbstractHandler(
    System.Type table,
    System.Action<AbstractEvents.IRowSelected<object>> handler)
  {
    PXRowSelected classicHandler = new RowSelectedEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(table, classicHandler);
  }

  private protected void AddHandler<TTable>(
    Events.Event<PXRowSelectedEventArgs, Events.RowSelected<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowSelected classicHandler = new RowSelectedEvents.GenericAuto<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveHandler<TTable>(
    Events.Event<PXRowSelectedEventArgs, Events.RowSelected<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowSelected classicHandler = new RowSelectedEvents.GenericAuto<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  private class GenericAbstractStrict<TTable>(RowSelectedEvents parent) : 
    RowEventsBase<PXRowSelectedEventArgs, PXRowSelected>.GenericTable<AbstractEvents.IRowSelected<TTable>, System.Action<AbstractEvents.IRowSelected<TTable>>>((RowEventsBase<PXRowSelectedEventArgs, PXRowSelected>) parent, typeof (TTable), (Func<System.Action<AbstractEvents.IRowSelected<TTable>>, IGenericEventAdapter<PXRowSelectedEventArgs, System.Action<AbstractEvents.IRowSelected<TTable>>>>) (handler => (IGenericEventAdapter<PXRowSelectedEventArgs, System.Action<AbstractEvents.IRowSelected<TTable>>>) new EventsBase<PXRowSelectedEventArgs, PXRowSelected, List<PXRowSelected>>.Generic<AbstractEvents.IRowSelected<TTable>, System.Action<AbstractEvents.IRowSelected<TTable>>>.AbstractAdapter<Events.RowSelected<TTable>>(handler)), (Func<IGenericEventAdapter<PXRowSelectedEventArgs, System.Action<AbstractEvents.IRowSelected<TTable>>>, PXRowSelected>) (adapter => new PXRowSelected(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }

  private class GenericAbstractRelaxed(RowSelectedEvents parent, System.Type table) : 
    RowEventsBase<PXRowSelectedEventArgs, PXRowSelected>.GenericTable<AbstractEvents.IRowSelected<object>, System.Action<AbstractEvents.IRowSelected<object>>>((RowEventsBase<PXRowSelectedEventArgs, PXRowSelected>) parent, table, (Func<System.Action<AbstractEvents.IRowSelected<object>>, IGenericEventAdapter<PXRowSelectedEventArgs, System.Action<AbstractEvents.IRowSelected<object>>>>) (handler => (IGenericEventAdapter<PXRowSelectedEventArgs, System.Action<AbstractEvents.IRowSelected<object>>>) new EventsBase<PXRowSelectedEventArgs, PXRowSelected, List<PXRowSelected>>.Generic<AbstractEvents.IRowSelected<object>, System.Action<AbstractEvents.IRowSelected<object>>>.AbstractAdapter<AbstractEvents.RelaxedRowSelected>(handler)), (Func<IGenericEventAdapter<PXRowSelectedEventArgs, System.Action<AbstractEvents.IRowSelected<object>>>, PXRowSelected>) (adapter => new PXRowSelected(adapter.Execute)))
  {
  }

  private class GenericAuto<TTable>(RowSelectedEvents parent) : 
    RowEventsBase<PXRowSelectedEventArgs, PXRowSelected>.GenericTable<Events.RowSelected<TTable>, Events.Event<PXRowSelectedEventArgs, Events.RowSelected<TTable>>.EventDelegate>((RowEventsBase<PXRowSelectedEventArgs, PXRowSelected>) parent, typeof (TTable), (Func<Events.Event<PXRowSelectedEventArgs, Events.RowSelected<TTable>>.EventDelegate, IGenericEventAdapter<PXRowSelectedEventArgs, Events.Event<PXRowSelectedEventArgs, Events.RowSelected<TTable>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXRowSelectedEventArgs, Events.Event<PXRowSelectedEventArgs, Events.RowSelected<TTable>>.EventDelegate>) new Events.Event<PXRowSelectedEventArgs, Events.RowSelected<TTable>>.Adapter(handler)), (Func<IGenericEventAdapter<PXRowSelectedEventArgs, Events.Event<PXRowSelectedEventArgs, Events.RowSelected<TTable>>.EventDelegate>, PXRowSelected>) (adapter => new PXRowSelected(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }
}
