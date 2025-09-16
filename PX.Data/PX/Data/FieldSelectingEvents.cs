// Decompiled with JetBrains decompiler
// Type: PX.Data.FieldSelectingEvents
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data;

/// <exclude />
/// <exclude />
public abstract class FieldSelectingEvents(PXGraph graph) : 
  FieldEventsBase<PXFieldSelectingEventArgs, PXFieldSelecting>(graph)
{
  protected override PXFieldSelecting? Get(PXCache cache, string field)
  {
    PXFieldSelecting pxFieldSelecting;
    return !cache.FieldSelectingEvents.TryGetValue(field, out pxFieldSelecting) ? (PXFieldSelecting) null : pxFieldSelecting;
  }

  protected override void Add(PXCache cache, string field, PXFieldSelecting handler)
  {
    cache.SelectingFields.Add(field.ToLower());
    cache.FieldSelectingEvents[field] = handler + cache.FieldSelectingEvents[field];
  }

  protected override void Remove(PXCache cache, string field, PXFieldSelecting handler)
  {
    cache.FieldSelectingEvents[field] -= handler;
  }

  private protected override void SetProxy(
    PXCache cache,
    string field,
    EventsBase<PXFieldSelectingEventArgs, PXFieldSelecting, PXCache.EventDictionary<PXFieldSelecting>>.Proxy proxy)
  {
    cache.SelectingFields.Add(field);
    cache.FieldSelectingEvents[field] = new PXFieldSelecting(proxy.Intercept);
  }

  private protected override void SetProxy(
    PXCache.EventDictionary<PXFieldSelecting> delayed,
    string field,
    EventsBase<PXFieldSelectingEventArgs, PXFieldSelecting, PXCache.EventDictionary<PXFieldSelecting>>.Proxy proxy)
  {
    delayed[field] = new PXFieldSelecting(proxy.Intercept);
  }

  protected override void Add(
    PXCache.EventDictionary<PXFieldSelecting> delayed,
    string field,
    PXFieldSelecting handler)
  {
    delayed[field] = handler + delayed[field];
  }

  protected override void Remove(
    PXCache.EventDictionary<PXFieldSelecting> delayed,
    string field,
    PXFieldSelecting handler)
  {
    delayed[field] -= handler;
  }

  public void AddAbstractHandler<TTable, TField>(
    System.Action<AbstractEvents.IFieldSelecting<TTable, TField>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldSelecting classicHandler = new FieldSelectingEvents.GenericAbstractFullSpecified<TTable, TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TField>(
    System.Action<AbstractEvents.IFieldSelecting<TTable, TField>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldSelecting classicHandler = new FieldSelectingEvents.GenericAbstractFullSpecified<TTable, TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void AddAbstractHandler<TTable>(
    string fieldName,
    System.Action<AbstractEvents.IFieldSelecting<TTable, IBqlField>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXFieldSelecting classicHandler = new FieldSelectingEvents.GenericAbstractFieldAgnostic<TTable>(this, fieldName).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void RemoveAbstractHandler<TTable>(
    string fieldName,
    System.Action<AbstractEvents.IFieldSelecting<TTable, IBqlField>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXFieldSelecting classicHandler = new FieldSelectingEvents.GenericAbstractFieldAgnostic<TTable>(this, fieldName).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void AddAbstractHandler<TField>(
    System.Action<AbstractEvents.IFieldSelecting<object, TField>> handler)
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldSelecting classicHandler = new FieldSelectingEvents.GenericAbstractFieldOnly<TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveAbstractHandler<TField>(
    System.Action<AbstractEvents.IFieldSelecting<object, TField>> handler)
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldSelecting classicHandler = new FieldSelectingEvents.GenericAbstractFieldOnly<TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  public void AddAbstractHandler(
    System.Type table,
    string fieldName,
    System.Action<AbstractEvents.IFieldSelecting<object, IBqlField>> handler)
  {
    PXFieldSelecting classicHandler = new FieldSelectingEvents.GenericAbstractFullAgnostic(this, table, fieldName).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(table, fieldName, classicHandler);
  }

  public void RemoveAbstractHandler(
    System.Type table,
    string fieldName,
    System.Action<AbstractEvents.IFieldSelecting<object, IBqlField>> handler)
  {
    PXFieldSelecting classicHandler = new FieldSelectingEvents.GenericAbstractFullAgnostic(this, table, fieldName).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(table, fieldName, classicHandler);
  }

  public void RemoveHandler<TTable, TField, TValue>(
    Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<TTable, TField>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldSelecting classicHandler = new FieldSelectingEvents.GenericAutoFullSpecified<TTable, TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  private protected void AddHandler<TField>(
    Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<TField>>.EventDelegate handler)
    where TField : class, IBqlField
  {
    PXFieldSelecting classicHandler = new FieldSelectingEvents.GenericAutoFieldOnly<TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveHandler<TField>(
    Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<TField>>.EventDelegate handler)
    where TField : class, IBqlField
  {
    PXFieldSelecting classicHandler = new FieldSelectingEvents.GenericAutoFieldOnly<TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  private class GenericAbstractFullSpecified<TTable, TField>(FieldSelectingEvents parent) : 
    FieldEventsBase<PXFieldSelectingEventArgs, PXFieldSelecting>.GenericField<AbstractEvents.IFieldSelecting<TTable, TField>, System.Action<AbstractEvents.IFieldSelecting<TTable, TField>>>((FieldEventsBase<PXFieldSelectingEventArgs, PXFieldSelecting>) parent, typeof (TTable), typeof (TField).Name, (Func<System.Action<AbstractEvents.IFieldSelecting<TTable, TField>>, IGenericEventAdapter<PXFieldSelectingEventArgs, System.Action<AbstractEvents.IFieldSelecting<TTable, TField>>>>) (handler => (IGenericEventAdapter<PXFieldSelectingEventArgs, System.Action<AbstractEvents.IFieldSelecting<TTable, TField>>>) new EventsBase<PXFieldSelectingEventArgs, PXFieldSelecting, PXCache.EventDictionary<PXFieldSelecting>>.Generic<AbstractEvents.IFieldSelecting<TTable, TField>, System.Action<AbstractEvents.IFieldSelecting<TTable, TField>>>.AbstractAdapter<Events.FieldSelecting<TTable, TField>>(handler)), (Func<IGenericEventAdapter<PXFieldSelectingEventArgs, System.Action<AbstractEvents.IFieldSelecting<TTable, TField>>>, PXFieldSelecting>) (adapter => new PXFieldSelecting(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAbstractFieldAgnostic<TTable>(FieldSelectingEvents parent, string fieldName) : 
    FieldEventsBase<PXFieldSelectingEventArgs, PXFieldSelecting>.GenericField<AbstractEvents.IFieldSelecting<TTable, IBqlField>, System.Action<AbstractEvents.IFieldSelecting<TTable, IBqlField>>>((FieldEventsBase<PXFieldSelectingEventArgs, PXFieldSelecting>) parent, typeof (TTable), fieldName, (Func<System.Action<AbstractEvents.IFieldSelecting<TTable, IBqlField>>, IGenericEventAdapter<PXFieldSelectingEventArgs, System.Action<AbstractEvents.IFieldSelecting<TTable, IBqlField>>>>) (handler => (IGenericEventAdapter<PXFieldSelectingEventArgs, System.Action<AbstractEvents.IFieldSelecting<TTable, IBqlField>>>) new EventsBase<PXFieldSelectingEventArgs, PXFieldSelecting, PXCache.EventDictionary<PXFieldSelecting>>.Generic<AbstractEvents.IFieldSelecting<TTable, IBqlField>, System.Action<AbstractEvents.IFieldSelecting<TTable, IBqlField>>>.AbstractAdapter<Events.FieldSelecting<TTable, AbstractEvents.IFakeBqlField>>(handler)), (Func<IGenericEventAdapter<PXFieldSelectingEventArgs, System.Action<AbstractEvents.IFieldSelecting<TTable, IBqlField>>>, PXFieldSelecting>) (adapter => new PXFieldSelecting(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }

  private class GenericAbstractFieldOnly<TField>(FieldSelectingEvents parent) : 
    FieldEventsBase<PXFieldSelectingEventArgs, PXFieldSelecting>.GenericField<AbstractEvents.IFieldSelecting<object, TField>, System.Action<AbstractEvents.IFieldSelecting<object, TField>>>((FieldEventsBase<PXFieldSelectingEventArgs, PXFieldSelecting>) parent, BqlCommand.GetItemType<TField>(), typeof (TField).Name, (Func<System.Action<AbstractEvents.IFieldSelecting<object, TField>>, IGenericEventAdapter<PXFieldSelectingEventArgs, System.Action<AbstractEvents.IFieldSelecting<object, TField>>>>) (handler => (IGenericEventAdapter<PXFieldSelectingEventArgs, System.Action<AbstractEvents.IFieldSelecting<object, TField>>>) new EventsBase<PXFieldSelectingEventArgs, PXFieldSelecting, PXCache.EventDictionary<PXFieldSelecting>>.Generic<AbstractEvents.IFieldSelecting<object, TField>, System.Action<AbstractEvents.IFieldSelecting<object, TField>>>.AbstractAdapter<Events.FieldSelecting<TField>>(handler)), (Func<IGenericEventAdapter<PXFieldSelectingEventArgs, System.Action<AbstractEvents.IFieldSelecting<object, TField>>>, PXFieldSelecting>) (adapter => new PXFieldSelecting(adapter.Execute)))
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAbstractFullAgnostic(
    FieldSelectingEvents parent,
    System.Type table,
    string fieldName) : 
    FieldEventsBase<PXFieldSelectingEventArgs, PXFieldSelecting>.GenericField<AbstractEvents.IFieldSelecting<object, IBqlField>, System.Action<AbstractEvents.IFieldSelecting<object, IBqlField>>>((FieldEventsBase<PXFieldSelectingEventArgs, PXFieldSelecting>) parent, table, fieldName, (Func<System.Action<AbstractEvents.IFieldSelecting<object, IBqlField>>, IGenericEventAdapter<PXFieldSelectingEventArgs, System.Action<AbstractEvents.IFieldSelecting<object, IBqlField>>>>) (handler => (IGenericEventAdapter<PXFieldSelectingEventArgs, System.Action<AbstractEvents.IFieldSelecting<object, IBqlField>>>) new EventsBase<PXFieldSelectingEventArgs, PXFieldSelecting, PXCache.EventDictionary<PXFieldSelecting>>.Generic<AbstractEvents.IFieldSelecting<object, IBqlField>, System.Action<AbstractEvents.IFieldSelecting<object, IBqlField>>>.AbstractAdapter<Events.FieldSelecting<IBqlField>>(handler)), (Func<IGenericEventAdapter<PXFieldSelectingEventArgs, System.Action<AbstractEvents.IFieldSelecting<object, IBqlField>>>, PXFieldSelecting>) (adapter => new PXFieldSelecting(adapter.Execute)))
  {
  }

  private class GenericAutoFullSpecified<TTable, TField>(FieldSelectingEvents parent) : 
    FieldEventsBase<PXFieldSelectingEventArgs, PXFieldSelecting>.GenericField<Events.FieldSelecting<TTable, TField>, Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<TTable, TField>>.EventDelegate>((FieldEventsBase<PXFieldSelectingEventArgs, PXFieldSelecting>) parent, typeof (TTable), typeof (TField).Name, (Func<Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<TTable, TField>>.EventDelegate, IGenericEventAdapter<PXFieldSelectingEventArgs, Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<TTable, TField>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXFieldSelectingEventArgs, Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<TTable, TField>>.EventDelegate>) new Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<TTable, TField>>.Adapter(handler)), (Func<IGenericEventAdapter<PXFieldSelectingEventArgs, Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<TTable, TField>>.EventDelegate>, PXFieldSelecting>) (adapter => new PXFieldSelecting(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAutoFieldOnly<TField>(FieldSelectingEvents parent) : 
    FieldEventsBase<PXFieldSelectingEventArgs, PXFieldSelecting>.GenericField<Events.FieldSelecting<TField>, Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<TField>>.EventDelegate>((FieldEventsBase<PXFieldSelectingEventArgs, PXFieldSelecting>) parent, BqlCommand.GetItemType<TField>(), typeof (TField).Name, (Func<Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<TField>>.EventDelegate, IGenericEventAdapter<PXFieldSelectingEventArgs, Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<TField>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXFieldSelectingEventArgs, Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<TField>>.EventDelegate>) new Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<TField>>.Adapter(handler)), (Func<IGenericEventAdapter<PXFieldSelectingEventArgs, Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<TField>>.EventDelegate>, PXFieldSelecting>) (adapter => new PXFieldSelecting(adapter.Execute)))
    where TField : class, IBqlField
  {
  }
}
