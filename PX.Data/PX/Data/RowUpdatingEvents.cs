// Decompiled with JetBrains decompiler
// Type: PX.Data.RowUpdatingEvents
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
public abstract class RowUpdatingEvents(PXGraph graph) : 
  RowEventsBase<PXRowUpdatingEventArgs, PXRowUpdating>(graph)
{
  protected override PXRowUpdating? Get(PXCache cache) => cache._EventsRow.RowUpdating;

  protected override void Add(PXCache cache, PXRowUpdating handler) => cache.RowUpdating += handler;

  protected override void Remove(PXCache cache, PXRowUpdating handler)
  {
    cache.RowUpdating -= handler;
  }

  private protected override void SetProxy(
    PXCache cache,
    EventsBase<PXRowUpdatingEventArgs, PXRowUpdating, List<PXRowUpdating>>.Proxy proxy)
  {
    cache._EventsRow.RowUpdating = new PXRowUpdating(proxy.Intercept);
    cache._EventsRow._RowUpdatingList = (List<PXRowUpdating>) null;
  }

  private protected override void SetProxy(
    List<PXRowUpdating> delayed,
    EventsBase<PXRowUpdatingEventArgs, PXRowUpdating, List<PXRowUpdating>>.Proxy proxy)
  {
    delayed.Clear();
    delayed.Add(new PXRowUpdating(proxy.Intercept));
  }

  protected override void Add(List<PXRowUpdating> delayed, PXRowUpdating handler)
  {
    delayed.Insert(0, handler);
  }

  protected override void Remove(List<PXRowUpdating> delayed, PXRowUpdating handler)
  {
    delayed.Remove(handler);
  }

  protected override void ApplyDelayed(PXCache cache, List<PXRowUpdating> delayed)
  {
    if (cache._EventsRow._RowUpdatingList != null)
    {
      cache._EventsRow._RowUpdatingList.InsertRange(0, (IEnumerable<PXRowUpdating>) delayed);
    }
    else
    {
      if (cache._EventsRow.RowUpdating != null)
        delayed.Add(cache._EventsRow.RowUpdating);
      cache._EventsRow.RowUpdating = (PXRowUpdating) Delegate.Combine((Delegate[]) delayed.ToArray());
    }
  }

  public void AddAbstractHandler<TTable>(
    System.Action<AbstractEvents.IRowUpdating<TTable>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowUpdating classicHandler = new RowUpdatingEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveAbstractHandler<TTable>(
    System.Action<AbstractEvents.IRowUpdating<TTable>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowUpdating classicHandler = new RowUpdatingEvents.GenericAbstractStrict<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  public void AddAbstractHandler(
    System.Type table,
    System.Action<AbstractEvents.IRowUpdating<object>> handler)
  {
    PXRowUpdating classicHandler = new RowUpdatingEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(table, classicHandler);
  }

  public void RemoveAbstractHandler(
    System.Type table,
    System.Action<AbstractEvents.IRowUpdating<object>> handler)
  {
    PXRowUpdating classicHandler = new RowUpdatingEvents.GenericAbstractRelaxed(this, table).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(table, classicHandler);
  }

  private protected void AddHandler<TTable>(
    Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowUpdating classicHandler = new RowUpdatingEvents.GenericAuto<TTable>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TTable>(classicHandler);
  }

  public void RemoveHandler<TTable>(
    Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<TTable>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
  {
    PXRowUpdating classicHandler = new RowUpdatingEvents.GenericAuto<TTable>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TTable>(classicHandler);
  }

  private class GenericAbstractStrict<TTable>(RowUpdatingEvents parent) : 
    RowEventsBase<PXRowUpdatingEventArgs, PXRowUpdating>.GenericTable<AbstractEvents.IRowUpdating<TTable>, System.Action<AbstractEvents.IRowUpdating<TTable>>>((RowEventsBase<PXRowUpdatingEventArgs, PXRowUpdating>) parent, typeof (TTable), (Func<System.Action<AbstractEvents.IRowUpdating<TTable>>, IGenericEventAdapter<PXRowUpdatingEventArgs, System.Action<AbstractEvents.IRowUpdating<TTable>>>>) (handler => (IGenericEventAdapter<PXRowUpdatingEventArgs, System.Action<AbstractEvents.IRowUpdating<TTable>>>) new EventsBase<PXRowUpdatingEventArgs, PXRowUpdating, List<PXRowUpdating>>.Generic<AbstractEvents.IRowUpdating<TTable>, System.Action<AbstractEvents.IRowUpdating<TTable>>>.AbstractAdapter<Events.RowUpdating<TTable>>(handler)), (Func<IGenericEventAdapter<PXRowUpdatingEventArgs, System.Action<AbstractEvents.IRowUpdating<TTable>>>, PXRowUpdating>) (adapter => new PXRowUpdating(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }

  private class GenericAbstractRelaxed(RowUpdatingEvents parent, System.Type table) : 
    RowEventsBase<PXRowUpdatingEventArgs, PXRowUpdating>.GenericTable<AbstractEvents.IRowUpdating<object>, System.Action<AbstractEvents.IRowUpdating<object>>>((RowEventsBase<PXRowUpdatingEventArgs, PXRowUpdating>) parent, table, (Func<System.Action<AbstractEvents.IRowUpdating<object>>, IGenericEventAdapter<PXRowUpdatingEventArgs, System.Action<AbstractEvents.IRowUpdating<object>>>>) (handler => (IGenericEventAdapter<PXRowUpdatingEventArgs, System.Action<AbstractEvents.IRowUpdating<object>>>) new EventsBase<PXRowUpdatingEventArgs, PXRowUpdating, List<PXRowUpdating>>.Generic<AbstractEvents.IRowUpdating<object>, System.Action<AbstractEvents.IRowUpdating<object>>>.AbstractAdapter<AbstractEvents.RelaxedRowUpdating>(handler)), (Func<IGenericEventAdapter<PXRowUpdatingEventArgs, System.Action<AbstractEvents.IRowUpdating<object>>>, PXRowUpdating>) (adapter => new PXRowUpdating(adapter.Execute)))
  {
  }

  private class GenericAuto<TTable>(RowUpdatingEvents parent) : 
    RowEventsBase<PXRowUpdatingEventArgs, PXRowUpdating>.GenericTable<Events.RowUpdating<TTable>, Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<TTable>>.EventDelegate>((RowEventsBase<PXRowUpdatingEventArgs, PXRowUpdating>) parent, typeof (TTable), (Func<Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<TTable>>.EventDelegate, IGenericEventAdapter<PXRowUpdatingEventArgs, Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<TTable>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXRowUpdatingEventArgs, Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<TTable>>.EventDelegate>) new Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<TTable>>.Adapter(handler)), (Func<IGenericEventAdapter<PXRowUpdatingEventArgs, Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<TTable>>.EventDelegate>, PXRowUpdating>) (adapter => new PXRowUpdating(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }
}
