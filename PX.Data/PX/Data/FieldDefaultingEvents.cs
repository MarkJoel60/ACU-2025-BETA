// Decompiled with JetBrains decompiler
// Type: PX.Data.FieldDefaultingEvents
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
public abstract class FieldDefaultingEvents(PXGraph graph) : 
  FieldEventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting>(graph)
{
  protected override PXFieldDefaulting? Get(PXCache cache, string field)
  {
    PXFieldDefaulting pxFieldDefaulting;
    return !cache.FieldDefaultingEvents.TryGetValue(field, out pxFieldDefaulting) ? (PXFieldDefaulting) null : pxFieldDefaulting;
  }

  protected override void Add(PXCache cache, string field, PXFieldDefaulting handler)
  {
    cache.FieldDefaultingEvents[field] = handler + cache.FieldDefaultingEvents[field];
  }

  protected override void Remove(PXCache cache, string field, PXFieldDefaulting handler)
  {
    cache.FieldDefaultingEvents[field] -= handler;
  }

  private protected override void SetProxy(
    PXCache cache,
    string field,
    EventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting, PXCache.EventDictionary<PXFieldDefaulting>>.Proxy proxy)
  {
    cache.FieldDefaultingEvents[field] = new PXFieldDefaulting(proxy.Intercept);
  }

  private protected override void SetProxy(
    PXCache.EventDictionary<PXFieldDefaulting> delayed,
    string field,
    EventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting, PXCache.EventDictionary<PXFieldDefaulting>>.Proxy proxy)
  {
    delayed[field] = new PXFieldDefaulting(proxy.Intercept);
  }

  protected override void Add(
    PXCache.EventDictionary<PXFieldDefaulting> delayed,
    string field,
    PXFieldDefaulting handler)
  {
    delayed[field] = handler + delayed[field];
  }

  protected override void Remove(
    PXCache.EventDictionary<PXFieldDefaulting> delayed,
    string field,
    PXFieldDefaulting handler)
  {
    delayed[field] -= handler;
  }

  public void AddAbstractHandler<TTable, TField, TValue>(
    System.Action<AbstractEvents.IFieldDefaulting<TTable, TField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : class
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAbstractFullSpecifiedTyped<TTable, TField, TValue>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TField, TValue>(
    System.Action<AbstractEvents.IFieldDefaulting<TTable, TField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : class
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAbstractFullSpecifiedTyped<TTable, TField, TValue>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void AddAbstractHandler<TTable, TField, TValue>(
    System.Action<AbstractEvents.IFieldDefaulting<TTable, TField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : struct
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAbstractFullSpecifiedTyped<TTable, TField, TValue?>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TField, TValue>(
    System.Action<AbstractEvents.IFieldDefaulting<TTable, TField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : struct
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAbstractFullSpecifiedTyped<TTable, TField, TValue?>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void AddAbstractHandler<TTable, TField>(
    System.Action<AbstractEvents.IFieldDefaulting<TTable, TField, object?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAbstractFullSpecifiedTypeless<TTable, TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TField>(
    System.Action<AbstractEvents.IFieldDefaulting<TTable, TField, object?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAbstractFullSpecifiedTypeless<TTable, TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void AddAbstractHandler<TTable, TValue>(
    string fieldName,
    System.Action<AbstractEvents.IFieldDefaulting<TTable, IBqlField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TValue : class
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAbstractFieldAgnosticTyped<TTable, TValue>(this, fieldName).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TValue>(
    string fieldName,
    System.Action<AbstractEvents.IFieldDefaulting<TTable, IBqlField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TValue : class
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAbstractFieldAgnosticTyped<TTable, TValue>(this, fieldName).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void AddAbstractHandler<TTable, TValue>(
    string fieldName,
    System.Action<AbstractEvents.IFieldDefaulting<TTable, IBqlField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TValue : struct
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAbstractFieldAgnosticTyped<TTable, TValue?>(this, fieldName).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TValue>(
    string fieldName,
    System.Action<AbstractEvents.IFieldDefaulting<TTable, IBqlField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TValue : struct
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAbstractFieldAgnosticTyped<TTable, TValue?>(this, fieldName).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void AddAbstractHandler<TTable>(
    string fieldName,
    System.Action<AbstractEvents.IFieldDefaulting<TTable, IBqlField, object?>> handler)
    where TTable : class, IBqlTable, new()
  {
    this.AddAbstractHandler<TTable, object>(fieldName, handler);
  }

  public void RemoveAbstractHandler<TTable>(
    string fieldName,
    System.Action<AbstractEvents.IFieldDefaulting<TTable, IBqlField, object?>> handler)
    where TTable : class, IBqlTable, new()
  {
    this.RemoveAbstractHandler<TTable, object>(fieldName, handler);
  }

  public void AddAbstractHandler<TField, TValue>(
    System.Action<AbstractEvents.IFieldDefaulting<object, TField, TValue?>> handler)
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : class
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAbstractFieldOnlyTyped<TField, TValue>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveAbstractHandler<TField, TValue>(
    System.Action<AbstractEvents.IFieldDefaulting<object, TField, TValue?>> handler)
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : class
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAbstractFieldOnlyTyped<TField, TValue>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  public void AddAbstractHandler<TField, TValue>(
    System.Action<AbstractEvents.IFieldDefaulting<object, TField, TValue?>> handler)
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : struct
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAbstractFieldOnlyTyped<TField, TValue?>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveAbstractHandler<TField, TValue>(
    System.Action<AbstractEvents.IFieldDefaulting<object, TField, TValue?>> handler)
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : struct
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAbstractFieldOnlyTyped<TField, TValue?>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  public void AddAbstractHandler<TField>(
    System.Action<AbstractEvents.IFieldDefaulting<object, TField, object?>> handler)
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAbstractFieldOnlyTypeless<TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveAbstractHandler<TField>(
    System.Action<AbstractEvents.IFieldDefaulting<object, TField, object?>> handler)
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAbstractFieldOnlyTypeless<TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  public void AddAbstractHandler(
    System.Type table,
    string fieldName,
    System.Action<AbstractEvents.IFieldDefaulting<object, IBqlField, object?>> handler)
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAbstractFullAgnostic(this, table, fieldName).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(table, fieldName, classicHandler);
  }

  public void RemoveAbstractHandler(
    System.Type table,
    string fieldName,
    System.Action<AbstractEvents.IFieldDefaulting<object, IBqlField, object?>> handler)
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAbstractFullAgnostic(this, table, fieldName).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(table, fieldName, classicHandler);
  }

  public void RemoveHandler<TTable, TField, TValue>(
    Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TTable, TField, TValue>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAutoFullSpecified<TTable, TField, TValue>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveHandler<TTable, TField>(
    Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TTable, TField>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAutoTypeAgnostic<TTable, TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  private protected void AddHandler<TField>(
    Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TField>>.EventDelegate handler)
    where TField : class, IBqlField
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAutoFieldOnly<TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveHandler<TField>(
    Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TField>>.EventDelegate handler)
    where TField : class, IBqlField
  {
    PXFieldDefaulting classicHandler = new FieldDefaultingEvents.GenericAutoFieldOnly<TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  private class GenericAbstractFullSpecifiedTyped<TTable, TField, TValue>(
    FieldDefaultingEvents parent) : 
    FieldEventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting>.GenericField<AbstractEvents.IFieldDefaulting<TTable, TField, TValue>, System.Action<AbstractEvents.IFieldDefaulting<TTable, TField, TValue>>>((FieldEventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting>) parent, typeof (TTable), typeof (TField).Name, (Func<System.Action<AbstractEvents.IFieldDefaulting<TTable, TField, TValue>>, IGenericEventAdapter<PXFieldDefaultingEventArgs, System.Action<AbstractEvents.IFieldDefaulting<TTable, TField, TValue>>>>) (handler => (IGenericEventAdapter<PXFieldDefaultingEventArgs, System.Action<AbstractEvents.IFieldDefaulting<TTable, TField, TValue>>>) new EventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting, PXCache.EventDictionary<PXFieldDefaulting>>.Generic<AbstractEvents.IFieldDefaulting<TTable, TField, TValue>, System.Action<AbstractEvents.IFieldDefaulting<TTable, TField, TValue>>>.AbstractAdapter<Events.FieldDefaulting<TTable, TField, TValue>>(handler)), (Func<IGenericEventAdapter<PXFieldDefaultingEventArgs, System.Action<AbstractEvents.IFieldDefaulting<TTable, TField, TValue>>>, PXFieldDefaulting>) (adapter => new PXFieldDefaulting(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
  }

  private class GenericAbstractFullSpecifiedTypeless<TTable, TField>(FieldDefaultingEvents parent) : 
    FieldEventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting>.GenericField<AbstractEvents.IFieldDefaulting<TTable, TField, object?>, System.Action<AbstractEvents.IFieldDefaulting<TTable, TField, object?>>>((FieldEventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting>) parent, typeof (TTable), typeof (TField).Name, (Func<System.Action<AbstractEvents.IFieldDefaulting<TTable, TField, object>>, IGenericEventAdapter<PXFieldDefaultingEventArgs, System.Action<AbstractEvents.IFieldDefaulting<TTable, TField, object>>>>) (handler => (IGenericEventAdapter<PXFieldDefaultingEventArgs, System.Action<AbstractEvents.IFieldDefaulting<TTable, TField, object>>>) new EventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting, PXCache.EventDictionary<PXFieldDefaulting>>.Generic<AbstractEvents.IFieldDefaulting<TTable, TField, object>, System.Action<AbstractEvents.IFieldDefaulting<TTable, TField, object>>>.AbstractAdapter<Events.FieldDefaulting<TTable, TField>>(handler)), (Func<IGenericEventAdapter<PXFieldDefaultingEventArgs, System.Action<AbstractEvents.IFieldDefaulting<TTable, TField, object>>>, PXFieldDefaulting>) (adapter => new PXFieldDefaulting(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAbstractFieldAgnosticTyped<TTable, TValue>(
    FieldDefaultingEvents parent,
    string fieldName) : 
    FieldEventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting>.GenericField<AbstractEvents.IFieldDefaulting<TTable, IBqlField, TValue>, System.Action<AbstractEvents.IFieldDefaulting<TTable, IBqlField, TValue>>>((FieldEventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting>) parent, typeof (TTable), fieldName, (Func<System.Action<AbstractEvents.IFieldDefaulting<TTable, IBqlField, TValue>>, IGenericEventAdapter<PXFieldDefaultingEventArgs, System.Action<AbstractEvents.IFieldDefaulting<TTable, IBqlField, TValue>>>>) (handler => (IGenericEventAdapter<PXFieldDefaultingEventArgs, System.Action<AbstractEvents.IFieldDefaulting<TTable, IBqlField, TValue>>>) new EventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting, PXCache.EventDictionary<PXFieldDefaulting>>.Generic<AbstractEvents.IFieldDefaulting<TTable, IBqlField, TValue>, System.Action<AbstractEvents.IFieldDefaulting<TTable, IBqlField, TValue>>>.AbstractAdapter<Events.FieldDefaulting<TTable, AbstractEvents.AnonymousBqlFieldOf<TValue>, TValue>>(handler)), (Func<IGenericEventAdapter<PXFieldDefaultingEventArgs, System.Action<AbstractEvents.IFieldDefaulting<TTable, IBqlField, TValue>>>, PXFieldDefaulting>) (adapter => new PXFieldDefaulting(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }

  private class GenericAbstractFieldOnlyTyped<TField, TValue>(FieldDefaultingEvents parent) : 
    FieldEventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting>.GenericField<AbstractEvents.IFieldDefaulting<object, TField, TValue>, System.Action<AbstractEvents.IFieldDefaulting<object, TField, TValue>>>((FieldEventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting>) parent, BqlCommand.GetItemType<TField>(), typeof (TField).Name, (Func<System.Action<AbstractEvents.IFieldDefaulting<object, TField, TValue>>, IGenericEventAdapter<PXFieldDefaultingEventArgs, System.Action<AbstractEvents.IFieldDefaulting<object, TField, TValue>>>>) (handler => (IGenericEventAdapter<PXFieldDefaultingEventArgs, System.Action<AbstractEvents.IFieldDefaulting<object, TField, TValue>>>) new EventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting, PXCache.EventDictionary<PXFieldDefaulting>>.Generic<AbstractEvents.IFieldDefaulting<object, TField, TValue>, System.Action<AbstractEvents.IFieldDefaulting<object, TField, TValue>>>.AbstractAdapter<AbstractEvents.RelaxedFieldDefaulting<TField, TValue>>(handler)), (Func<IGenericEventAdapter<PXFieldDefaultingEventArgs, System.Action<AbstractEvents.IFieldDefaulting<object, TField, TValue>>>, PXFieldDefaulting>) (adapter => new PXFieldDefaulting(adapter.Execute)))
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
  }

  private class GenericAbstractFieldOnlyTypeless<TField>(FieldDefaultingEvents parent) : 
    FieldEventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting>.GenericField<AbstractEvents.IFieldDefaulting<object, TField, object?>, System.Action<AbstractEvents.IFieldDefaulting<object, TField, object?>>>((FieldEventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting>) parent, BqlCommand.GetItemType<TField>(), typeof (TField).Name, (Func<System.Action<AbstractEvents.IFieldDefaulting<object, TField, object>>, IGenericEventAdapter<PXFieldDefaultingEventArgs, System.Action<AbstractEvents.IFieldDefaulting<object, TField, object>>>>) (handler => (IGenericEventAdapter<PXFieldDefaultingEventArgs, System.Action<AbstractEvents.IFieldDefaulting<object, TField, object>>>) new EventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting, PXCache.EventDictionary<PXFieldDefaulting>>.Generic<AbstractEvents.IFieldDefaulting<object, TField, object>, System.Action<AbstractEvents.IFieldDefaulting<object, TField, object>>>.AbstractAdapter<Events.FieldDefaulting<TField>>(handler)), (Func<IGenericEventAdapter<PXFieldDefaultingEventArgs, System.Action<AbstractEvents.IFieldDefaulting<object, TField, object>>>, PXFieldDefaulting>) (adapter => new PXFieldDefaulting(adapter.Execute)))
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAbstractFullAgnostic(
    FieldDefaultingEvents parent,
    System.Type table,
    string fieldName) : 
    FieldEventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting>.GenericField<AbstractEvents.IFieldDefaulting<object, IBqlField, object?>, System.Action<AbstractEvents.IFieldDefaulting<object, IBqlField, object?>>>((FieldEventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting>) parent, table, fieldName, (Func<System.Action<AbstractEvents.IFieldDefaulting<object, IBqlField, object>>, IGenericEventAdapter<PXFieldDefaultingEventArgs, System.Action<AbstractEvents.IFieldDefaulting<object, IBqlField, object>>>>) (handler => (IGenericEventAdapter<PXFieldDefaultingEventArgs, System.Action<AbstractEvents.IFieldDefaulting<object, IBqlField, object>>>) new EventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting, PXCache.EventDictionary<PXFieldDefaulting>>.Generic<AbstractEvents.IFieldDefaulting<object, IBqlField, object>, System.Action<AbstractEvents.IFieldDefaulting<object, IBqlField, object>>>.AbstractAdapter<Events.FieldDefaulting<AbstractEvents.IFakeBqlField>>(handler)), (Func<IGenericEventAdapter<PXFieldDefaultingEventArgs, System.Action<AbstractEvents.IFieldDefaulting<object, IBqlField, object>>>, PXFieldDefaulting>) (adapter => new PXFieldDefaulting(adapter.Execute)))
  {
  }

  private class GenericAutoFullSpecified<TTable, TField, TValue>(FieldDefaultingEvents parent) : 
    FieldEventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting>.GenericField<Events.FieldDefaulting<TTable, TField, TValue>, Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TTable, TField, TValue>>.EventDelegate>((FieldEventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting>) parent, BqlCommand.GetItemType<TField>(), typeof (TField).Name, (Func<Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TTable, TField, TValue>>.EventDelegate, IGenericEventAdapter<PXFieldDefaultingEventArgs, Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TTable, TField, TValue>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXFieldDefaultingEventArgs, Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TTable, TField, TValue>>.EventDelegate>) new Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TTable, TField, TValue>>.Adapter(handler)), (Func<IGenericEventAdapter<PXFieldDefaultingEventArgs, Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TTable, TField, TValue>>.EventDelegate>, PXFieldDefaulting>) (adapter => new PXFieldDefaulting(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
  }

  private class GenericAutoTypeAgnostic<TTable, TField>(FieldDefaultingEvents parent) : 
    FieldEventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting>.GenericField<Events.FieldDefaulting<TTable, TField>, Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TTable, TField>>.EventDelegate>((FieldEventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting>) parent, typeof (TTable), typeof (TField).Name, (Func<Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TTable, TField>>.EventDelegate, IGenericEventAdapter<PXFieldDefaultingEventArgs, Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TTable, TField>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXFieldDefaultingEventArgs, Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TTable, TField>>.EventDelegate>) new Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TTable, TField>>.Adapter(handler)), (Func<IGenericEventAdapter<PXFieldDefaultingEventArgs, Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TTable, TField>>.EventDelegate>, PXFieldDefaulting>) (adapter => new PXFieldDefaulting(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAutoFieldOnly<TField>(FieldDefaultingEvents parent) : 
    FieldEventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting>.GenericField<Events.FieldDefaulting<TField>, Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TField>>.EventDelegate>((FieldEventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting>) parent, BqlCommand.GetItemType<TField>(), typeof (TField).Name, (Func<Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TField>>.EventDelegate, IGenericEventAdapter<PXFieldDefaultingEventArgs, Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TField>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXFieldDefaultingEventArgs, Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TField>>.EventDelegate>) new Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TField>>.Adapter(handler)), (Func<IGenericEventAdapter<PXFieldDefaultingEventArgs, Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<TField>>.EventDelegate>, PXFieldDefaulting>) (adapter => new PXFieldDefaulting(adapter.Execute)))
    where TField : class, IBqlField
  {
  }
}
