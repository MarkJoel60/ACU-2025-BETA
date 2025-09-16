// Decompiled with JetBrains decompiler
// Type: PX.Data.FieldUpdatedEvents
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
public abstract class FieldUpdatedEvents(PXGraph graph) : 
  FieldEventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated>(graph)
{
  protected override PXFieldUpdated? Get(PXCache cache, string field)
  {
    PXFieldUpdated pxFieldUpdated;
    return !cache.FieldUpdatedEvents.TryGetValue(field, out pxFieldUpdated) ? (PXFieldUpdated) null : pxFieldUpdated;
  }

  protected override void Add(PXCache cache, string field, PXFieldUpdated handler)
  {
    cache.FieldUpdatedEvents[field] += handler;
  }

  protected override void Remove(PXCache cache, string field, PXFieldUpdated handler)
  {
    cache.FieldUpdatedEvents[field] -= handler;
  }

  private protected override void SetProxy(
    PXCache cache,
    string field,
    EventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated, PXCache.EventDictionary<PXFieldUpdated>>.Proxy proxy)
  {
    cache.FieldUpdatedEvents[field] = new PXFieldUpdated(proxy.Intercept);
  }

  private protected override void SetProxy(
    PXCache.EventDictionary<PXFieldUpdated> delayed,
    string field,
    EventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated, PXCache.EventDictionary<PXFieldUpdated>>.Proxy proxy)
  {
    delayed[field] = new PXFieldUpdated(proxy.Intercept);
  }

  protected override void Add(
    PXCache.EventDictionary<PXFieldUpdated> delayed,
    string field,
    PXFieldUpdated handler)
  {
    delayed[field] += handler;
  }

  protected override void Remove(
    PXCache.EventDictionary<PXFieldUpdated> delayed,
    string field,
    PXFieldUpdated handler)
  {
    delayed[field] -= handler;
  }

  public void AddAbstractHandler<TTable, TField, TValue>(
    System.Action<AbstractEvents.IFieldUpdated<TTable, TField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : class
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAbstractFullSpecifiedTyped<TTable, TField, TValue>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TField, TValue>(
    System.Action<AbstractEvents.IFieldUpdated<TTable, TField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : class
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAbstractFullSpecifiedTyped<TTable, TField, TValue>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void AddAbstractHandler<TTable, TField, TValue>(
    System.Action<AbstractEvents.IFieldUpdated<TTable, TField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : struct
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAbstractFullSpecifiedTyped<TTable, TField, TValue?>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TField, TValue>(
    System.Action<AbstractEvents.IFieldUpdated<TTable, TField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : struct
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAbstractFullSpecifiedTyped<TTable, TField, TValue?>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void AddAbstractHandler<TTable, TField>(
    System.Action<AbstractEvents.IFieldUpdated<TTable, TField, object?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAbstractFullSpecifiedTypeless<TTable, TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TField>(
    System.Action<AbstractEvents.IFieldUpdated<TTable, TField, object?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAbstractFullSpecifiedTypeless<TTable, TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void AddAbstractHandler<TTable, TValue>(
    string fieldName,
    System.Action<AbstractEvents.IFieldUpdated<TTable, IBqlField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TValue : class
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAbstractFieldAgnostic<TTable, TValue>(this, fieldName).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TValue>(
    string fieldName,
    System.Action<AbstractEvents.IFieldUpdated<TTable, IBqlField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TValue : class
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAbstractFieldAgnostic<TTable, TValue>(this, fieldName).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void AddAbstractHandler<TTable, TValue>(
    string fieldName,
    System.Action<AbstractEvents.IFieldUpdated<TTable, IBqlField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TValue : struct
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAbstractFieldAgnostic<TTable, TValue?>(this, fieldName).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TValue>(
    string fieldName,
    System.Action<AbstractEvents.IFieldUpdated<TTable, IBqlField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TValue : struct
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAbstractFieldAgnostic<TTable, TValue?>(this, fieldName).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void AddAbstractHandler<TTable>(
    string fieldName,
    System.Action<AbstractEvents.IFieldUpdated<TTable, IBqlField, object?>> handler)
    where TTable : class, IBqlTable, new()
  {
    this.AddAbstractHandler<TTable, object>(fieldName, handler);
  }

  public void RemoveAbstractHandler<TTable>(
    string fieldName,
    System.Action<AbstractEvents.IFieldUpdated<TTable, IBqlField, object?>> handler)
    where TTable : class, IBqlTable, new()
  {
    this.RemoveAbstractHandler<TTable, object>(fieldName, handler);
  }

  public void AddAbstractHandler<TField, TValue>(
    System.Action<AbstractEvents.IFieldUpdated<object, TField, TValue?>> handler)
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : class
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAbstractFieldOnlyTyped<TField, TValue>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveAbstractHandler<TField, TValue>(
    System.Action<AbstractEvents.IFieldUpdated<object, TField, TValue?>> handler)
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : class
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAbstractFieldOnlyTyped<TField, TValue>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  public void AddAbstractHandler<TField, TValue>(
    System.Action<AbstractEvents.IFieldUpdated<object, TField, TValue?>> handler)
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : struct
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAbstractFieldOnlyTyped<TField, TValue?>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveAbstractHandler<TField, TValue>(
    System.Action<AbstractEvents.IFieldUpdated<object, TField, TValue?>> handler)
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : struct
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAbstractFieldOnlyTyped<TField, TValue?>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  public void AddAbstractHandler<TField>(
    System.Action<AbstractEvents.IFieldUpdated<object, TField, object?>> handler)
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAbstractFieldOnlyTypeless<TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveAbstractHandler<TField>(
    System.Action<AbstractEvents.IFieldUpdated<object, TField, object?>> handler)
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAbstractFieldOnlyTypeless<TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  public void AddAbstractHandler(
    System.Type table,
    string fieldName,
    System.Action<AbstractEvents.IFieldUpdated<object, IBqlField, object?>> handler)
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAbstractFullAgnostic(this, table, fieldName).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(table, fieldName, classicHandler);
  }

  public void RemoveAbstractHandler(
    System.Type table,
    string fieldName,
    System.Action<AbstractEvents.IFieldUpdated<object, IBqlField, object?>> handler)
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAbstractFullAgnostic(this, table, fieldName).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(table, fieldName, classicHandler);
  }

  public void RemoveHandler<TTable, TField, TValue>(
    Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TTable, TField, TValue>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAutoFullSpecified<TTable, TField, TValue>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveHandler<TTable, TField>(
    Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TTable, TField>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAutoTypeAgnostic<TTable, TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  private protected void AddHandler<TField>(
    Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TField>>.EventDelegate handler)
    where TField : class, IBqlField
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAutoFieldOnly<TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveHandler<TField>(
    Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TField>>.EventDelegate handler)
    where TField : class, IBqlField
  {
    PXFieldUpdated classicHandler = new FieldUpdatedEvents.GenericAutoFieldOnly<TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  private class GenericAbstractFullSpecifiedTyped<TTable, TField, TValue>(FieldUpdatedEvents parent) : 
    FieldEventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated>.GenericField<AbstractEvents.IFieldUpdated<TTable, TField, TValue>, System.Action<AbstractEvents.IFieldUpdated<TTable, TField, TValue>>>((FieldEventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated>) parent, typeof (TTable), typeof (TField).Name, (Func<System.Action<AbstractEvents.IFieldUpdated<TTable, TField, TValue>>, IGenericEventAdapter<PXFieldUpdatedEventArgs, System.Action<AbstractEvents.IFieldUpdated<TTable, TField, TValue>>>>) (handler => (IGenericEventAdapter<PXFieldUpdatedEventArgs, System.Action<AbstractEvents.IFieldUpdated<TTable, TField, TValue>>>) new EventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated, PXCache.EventDictionary<PXFieldUpdated>>.Generic<AbstractEvents.IFieldUpdated<TTable, TField, TValue>, System.Action<AbstractEvents.IFieldUpdated<TTable, TField, TValue>>>.AbstractAdapter<Events.FieldUpdated<TTable, TField, TValue>>(handler)), (Func<IGenericEventAdapter<PXFieldUpdatedEventArgs, System.Action<AbstractEvents.IFieldUpdated<TTable, TField, TValue>>>, PXFieldUpdated>) (adapter => new PXFieldUpdated(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
  }

  private class GenericAbstractFullSpecifiedTypeless<TTable, TField>(FieldUpdatedEvents parent) : 
    FieldEventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated>.GenericField<AbstractEvents.IFieldUpdated<TTable, TField, object?>, System.Action<AbstractEvents.IFieldUpdated<TTable, TField, object?>>>((FieldEventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated>) parent, typeof (TTable), typeof (TField).Name, (Func<System.Action<AbstractEvents.IFieldUpdated<TTable, TField, object>>, IGenericEventAdapter<PXFieldUpdatedEventArgs, System.Action<AbstractEvents.IFieldUpdated<TTable, TField, object>>>>) (handler => (IGenericEventAdapter<PXFieldUpdatedEventArgs, System.Action<AbstractEvents.IFieldUpdated<TTable, TField, object>>>) new EventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated, PXCache.EventDictionary<PXFieldUpdated>>.Generic<AbstractEvents.IFieldUpdated<TTable, TField, object>, System.Action<AbstractEvents.IFieldUpdated<TTable, TField, object>>>.AbstractAdapter<Events.FieldUpdated<TTable, TField>>(handler)), (Func<IGenericEventAdapter<PXFieldUpdatedEventArgs, System.Action<AbstractEvents.IFieldUpdated<TTable, TField, object>>>, PXFieldUpdated>) (adapter => new PXFieldUpdated(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAbstractFieldAgnostic<TTable, TValue>(
    FieldUpdatedEvents parent,
    string fieldName) : 
    FieldEventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated>.GenericField<AbstractEvents.IFieldUpdated<TTable, IBqlField, TValue>, System.Action<AbstractEvents.IFieldUpdated<TTable, IBqlField, TValue>>>((FieldEventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated>) parent, typeof (TTable), fieldName, (Func<System.Action<AbstractEvents.IFieldUpdated<TTable, IBqlField, TValue>>, IGenericEventAdapter<PXFieldUpdatedEventArgs, System.Action<AbstractEvents.IFieldUpdated<TTable, IBqlField, TValue>>>>) (handler => (IGenericEventAdapter<PXFieldUpdatedEventArgs, System.Action<AbstractEvents.IFieldUpdated<TTable, IBqlField, TValue>>>) new EventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated, PXCache.EventDictionary<PXFieldUpdated>>.Generic<AbstractEvents.IFieldUpdated<TTable, IBqlField, TValue>, System.Action<AbstractEvents.IFieldUpdated<TTable, IBqlField, TValue>>>.AbstractAdapter<Events.FieldUpdated<TTable, AbstractEvents.AnonymousBqlFieldOf<TValue>, TValue>>(handler)), (Func<IGenericEventAdapter<PXFieldUpdatedEventArgs, System.Action<AbstractEvents.IFieldUpdated<TTable, IBqlField, TValue>>>, PXFieldUpdated>) (adapter => new PXFieldUpdated(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }

  private class GenericAbstractFieldOnlyTyped<TField, TValue>(FieldUpdatedEvents parent) : 
    FieldEventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated>.GenericField<AbstractEvents.IFieldUpdated<object, TField, TValue>, System.Action<AbstractEvents.IFieldUpdated<object, TField, TValue>>>((FieldEventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated>) parent, BqlCommand.GetItemType<TField>(), typeof (TField).Name, (Func<System.Action<AbstractEvents.IFieldUpdated<object, TField, TValue>>, IGenericEventAdapter<PXFieldUpdatedEventArgs, System.Action<AbstractEvents.IFieldUpdated<object, TField, TValue>>>>) (handler => (IGenericEventAdapter<PXFieldUpdatedEventArgs, System.Action<AbstractEvents.IFieldUpdated<object, TField, TValue>>>) new EventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated, PXCache.EventDictionary<PXFieldUpdated>>.Generic<AbstractEvents.IFieldUpdated<object, TField, TValue>, System.Action<AbstractEvents.IFieldUpdated<object, TField, TValue>>>.AbstractAdapter<AbstractEvents.RelaxedFieldUpdated<TField, TValue>>(handler)), (Func<IGenericEventAdapter<PXFieldUpdatedEventArgs, System.Action<AbstractEvents.IFieldUpdated<object, TField, TValue>>>, PXFieldUpdated>) (adapter => new PXFieldUpdated(adapter.Execute)))
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
  }

  private class GenericAbstractFieldOnlyTypeless<TField>(FieldUpdatedEvents parent) : 
    FieldEventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated>.GenericField<AbstractEvents.IFieldUpdated<object, TField, object?>, System.Action<AbstractEvents.IFieldUpdated<object, TField, object?>>>((FieldEventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated>) parent, BqlCommand.GetItemType<TField>(), typeof (TField).Name, (Func<System.Action<AbstractEvents.IFieldUpdated<object, TField, object>>, IGenericEventAdapter<PXFieldUpdatedEventArgs, System.Action<AbstractEvents.IFieldUpdated<object, TField, object>>>>) (handler => (IGenericEventAdapter<PXFieldUpdatedEventArgs, System.Action<AbstractEvents.IFieldUpdated<object, TField, object>>>) new EventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated, PXCache.EventDictionary<PXFieldUpdated>>.Generic<AbstractEvents.IFieldUpdated<object, TField, object>, System.Action<AbstractEvents.IFieldUpdated<object, TField, object>>>.AbstractAdapter<Events.FieldUpdated<TField>>(handler)), (Func<IGenericEventAdapter<PXFieldUpdatedEventArgs, System.Action<AbstractEvents.IFieldUpdated<object, TField, object>>>, PXFieldUpdated>) (adapter => new PXFieldUpdated(adapter.Execute)))
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAbstractFullAgnostic(
    FieldUpdatedEvents parent,
    System.Type table,
    string fieldName) : 
    FieldEventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated>.GenericField<AbstractEvents.IFieldUpdated<object, IBqlField, object?>, System.Action<AbstractEvents.IFieldUpdated<object, IBqlField, object?>>>((FieldEventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated>) parent, table, fieldName, (Func<System.Action<AbstractEvents.IFieldUpdated<object, IBqlField, object>>, IGenericEventAdapter<PXFieldUpdatedEventArgs, System.Action<AbstractEvents.IFieldUpdated<object, IBqlField, object>>>>) (handler => (IGenericEventAdapter<PXFieldUpdatedEventArgs, System.Action<AbstractEvents.IFieldUpdated<object, IBqlField, object>>>) new EventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated, PXCache.EventDictionary<PXFieldUpdated>>.Generic<AbstractEvents.IFieldUpdated<object, IBqlField, object>, System.Action<AbstractEvents.IFieldUpdated<object, IBqlField, object>>>.AbstractAdapter<Events.FieldUpdated<AbstractEvents.IFakeBqlField>>(handler)), (Func<IGenericEventAdapter<PXFieldUpdatedEventArgs, System.Action<AbstractEvents.IFieldUpdated<object, IBqlField, object>>>, PXFieldUpdated>) (adapter => new PXFieldUpdated(adapter.Execute)))
  {
  }

  private class GenericAutoFullSpecified<TTable, TField, TValue>(FieldUpdatedEvents parent) : 
    FieldEventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated>.GenericField<Events.FieldUpdated<TTable, TField, TValue>, Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TTable, TField, TValue>>.EventDelegate>((FieldEventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated>) parent, typeof (TTable), typeof (TField).Name, (Func<Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TTable, TField, TValue>>.EventDelegate, IGenericEventAdapter<PXFieldUpdatedEventArgs, Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TTable, TField, TValue>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXFieldUpdatedEventArgs, Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TTable, TField, TValue>>.EventDelegate>) new Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TTable, TField, TValue>>.Adapter(handler)), (Func<IGenericEventAdapter<PXFieldUpdatedEventArgs, Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TTable, TField, TValue>>.EventDelegate>, PXFieldUpdated>) (adapter => new PXFieldUpdated(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
  }

  private class GenericAutoTypeAgnostic<TTable, TField>(FieldUpdatedEvents parent) : 
    FieldEventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated>.GenericField<Events.FieldUpdated<TTable, TField>, Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TTable, TField>>.EventDelegate>((FieldEventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated>) parent, typeof (TTable), typeof (TField).Name, (Func<Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TTable, TField>>.EventDelegate, IGenericEventAdapter<PXFieldUpdatedEventArgs, Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TTable, TField>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXFieldUpdatedEventArgs, Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TTable, TField>>.EventDelegate>) new Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TTable, TField>>.Adapter(handler)), (Func<IGenericEventAdapter<PXFieldUpdatedEventArgs, Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TTable, TField>>.EventDelegate>, PXFieldUpdated>) (adapter => new PXFieldUpdated(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAutoFieldOnly<TField>(FieldUpdatedEvents parent) : 
    FieldEventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated>.GenericField<Events.FieldUpdated<TField>, Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TField>>.EventDelegate>((FieldEventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated>) parent, BqlCommand.GetItemType<TField>(), typeof (TField).Name, (Func<Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TField>>.EventDelegate, IGenericEventAdapter<PXFieldUpdatedEventArgs, Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TField>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXFieldUpdatedEventArgs, Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TField>>.EventDelegate>) new Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TField>>.Adapter(handler)), (Func<IGenericEventAdapter<PXFieldUpdatedEventArgs, Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TField>>.EventDelegate>, PXFieldUpdated>) (adapter => new PXFieldUpdated(adapter.Execute)))
    where TField : class, IBqlField
  {
  }
}
