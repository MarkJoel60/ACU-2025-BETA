// Decompiled with JetBrains decompiler
// Type: PX.Data.ExceptionHandlingEvents
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
public abstract class ExceptionHandlingEvents(PXGraph graph) : 
  FieldEventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling>(graph)
{
  protected override PXExceptionHandling? Get(PXCache cache, string field)
  {
    PXExceptionHandling exceptionHandling;
    return !cache.ExceptionHandlingEvents.TryGetValue(field, out exceptionHandling) ? (PXExceptionHandling) null : exceptionHandling;
  }

  protected override void Add(PXCache cache, string field, PXExceptionHandling handler)
  {
    cache.ExceptionHandlingEvents[field] = handler + cache.ExceptionHandlingEvents[field];
  }

  protected override void Remove(PXCache cache, string field, PXExceptionHandling handler)
  {
    cache.ExceptionHandlingEvents[field] -= handler;
  }

  private protected override void SetProxy(
    PXCache cache,
    string field,
    EventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling, PXCache.EventDictionary<PXExceptionHandling>>.Proxy proxy)
  {
    cache.ExceptionHandlingEvents[field] = new PXExceptionHandling(proxy.Intercept);
  }

  private protected override void SetProxy(
    PXCache.EventDictionary<PXExceptionHandling> delayed,
    string field,
    EventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling, PXCache.EventDictionary<PXExceptionHandling>>.Proxy proxy)
  {
    delayed[field] = new PXExceptionHandling(proxy.Intercept);
  }

  protected override void Add(
    PXCache.EventDictionary<PXExceptionHandling> delayed,
    string field,
    PXExceptionHandling handler)
  {
    delayed[field] = handler + delayed[field];
  }

  protected override void Remove(
    PXCache.EventDictionary<PXExceptionHandling> delayed,
    string field,
    PXExceptionHandling handler)
  {
    delayed[field] -= handler;
  }

  public void AddAbstractHandler<TTable, TField, TValue>(
    System.Action<AbstractEvents.IExceptionHandling<TTable, TField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : class
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAbstractFullSpecifiedTyped<TTable, TField, TValue>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TField, TValue>(
    System.Action<AbstractEvents.IExceptionHandling<TTable, TField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : class
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAbstractFullSpecifiedTyped<TTable, TField, TValue>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void AddAbstractHandler<TTable, TField, TValue>(
    System.Action<AbstractEvents.IExceptionHandling<TTable, TField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : struct
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAbstractFullSpecifiedTyped<TTable, TField, TValue?>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TField, TValue>(
    System.Action<AbstractEvents.IExceptionHandling<TTable, TField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : struct
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAbstractFullSpecifiedTyped<TTable, TField, TValue?>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  public void AddAbstractHandler<TTable, TField>(
    System.Action<AbstractEvents.IExceptionHandling<TTable, TField, object?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAbstractFullSpecifiedTypeless<TTable, TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TField>(
    System.Action<AbstractEvents.IExceptionHandling<TTable, TField, object?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAbstractFullSpecifiedTypeless<TTable, TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void AddAbstractHandler<TTable, TValue>(
    string fieldName,
    System.Action<AbstractEvents.IExceptionHandling<TTable, IBqlField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TValue : class
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAbstractFieldAgnosticTyped<TTable, TValue>(this, fieldName).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TValue>(
    string fieldName,
    System.Action<AbstractEvents.IExceptionHandling<TTable, IBqlField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TValue : class
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAbstractFieldAgnosticTyped<TTable, TValue>(this, fieldName).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void AddAbstractHandler<TTable, TValue>(
    string fieldName,
    System.Action<AbstractEvents.IExceptionHandling<TTable, IBqlField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TValue : struct
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAbstractFieldAgnosticTyped<TTable, TValue?>(this, fieldName).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TValue>(
    string fieldName,
    System.Action<AbstractEvents.IExceptionHandling<TTable, IBqlField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TValue : struct
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAbstractFieldAgnosticTyped<TTable, TValue?>(this, fieldName).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void AddAbstractHandler<TTable>(
    string fieldName,
    System.Action<AbstractEvents.IExceptionHandling<TTable, IBqlField, object?>> handler)
    where TTable : class, IBqlTable, new()
  {
    this.AddAbstractHandler<TTable, object>(fieldName, handler);
  }

  public void RemoveAbstractHandler<TTable>(
    string fieldName,
    System.Action<AbstractEvents.IExceptionHandling<TTable, IBqlField, object?>> handler)
    where TTable : class, IBqlTable, new()
  {
    this.RemoveAbstractHandler<TTable, object>(fieldName, handler);
  }

  public void AddAbstractHandler<TField, TValue>(
    System.Action<AbstractEvents.IExceptionHandling<object, TField, TValue?>> handler)
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : class
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAbstractFieldOnlyTyped<TField, TValue>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveAbstractHandler<TField, TValue>(
    System.Action<AbstractEvents.IExceptionHandling<object, TField, TValue?>> handler)
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : class
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAbstractFieldOnlyTyped<TField, TValue>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  public void AddAbstractHandler<TField, TValue>(
    System.Action<AbstractEvents.IExceptionHandling<object, TField, TValue?>> handler)
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : struct
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAbstractFieldOnlyTyped<TField, TValue?>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveAbstractHandler<TField, TValue>(
    System.Action<AbstractEvents.IExceptionHandling<object, TField, TValue?>> handler)
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : struct
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAbstractFieldOnlyTyped<TField, TValue?>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  public void AddAbstractHandler<TField>(
    System.Action<AbstractEvents.IExceptionHandling<object, TField, object?>> handler)
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAbstractFieldOnlyTypeless<TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveAbstractHandler<TField>(
    System.Action<AbstractEvents.IExceptionHandling<object, TField, object?>> handler)
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAbstractFieldOnlyTypeless<TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  public void AddAbstractHandler(
    System.Type table,
    string fieldName,
    System.Action<AbstractEvents.IExceptionHandling<object, IBqlField, object?>> handler)
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAbstractFullAgnostic(this, table, fieldName).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(table, fieldName, classicHandler);
  }

  public void RemoveAbstractHandler(
    System.Type table,
    string fieldName,
    System.Action<AbstractEvents.IExceptionHandling<object, IBqlField, object?>> handler)
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAbstractFullAgnostic(this, table, fieldName).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(table, fieldName, classicHandler);
  }

  public void RemoveHandler<TTable, TField, TValue>(
    Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TTable, TField, TValue>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAutoFullSpecified<TTable, TField, TValue>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveHandler<TTable, TField>(
    Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TTable, TField>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAutoTypeAgnostic<TTable, TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  private protected void AddHandler<TField>(
    Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TField>>.EventDelegate handler)
    where TField : class, IBqlField
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAutoFieldOnly<TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveHandler<TField>(
    Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TField>>.EventDelegate handler)
    where TField : class, IBqlField
  {
    PXExceptionHandling classicHandler = new ExceptionHandlingEvents.GenericAutoFieldOnly<TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  private class GenericAbstractFullSpecifiedTyped<TTable, TField, TValue>(
    ExceptionHandlingEvents parent) : 
    FieldEventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling>.GenericField<AbstractEvents.IExceptionHandling<TTable, TField, TValue>, System.Action<AbstractEvents.IExceptionHandling<TTable, TField, TValue>>>((FieldEventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling>) parent, typeof (TTable), typeof (TField).Name, (Func<System.Action<AbstractEvents.IExceptionHandling<TTable, TField, TValue>>, IGenericEventAdapter<PXExceptionHandlingEventArgs, System.Action<AbstractEvents.IExceptionHandling<TTable, TField, TValue>>>>) (handler => (IGenericEventAdapter<PXExceptionHandlingEventArgs, System.Action<AbstractEvents.IExceptionHandling<TTable, TField, TValue>>>) new EventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling, PXCache.EventDictionary<PXExceptionHandling>>.Generic<AbstractEvents.IExceptionHandling<TTable, TField, TValue>, System.Action<AbstractEvents.IExceptionHandling<TTable, TField, TValue>>>.AbstractAdapter<Events.ExceptionHandling<TTable, TField, TValue>>(handler)), (Func<IGenericEventAdapter<PXExceptionHandlingEventArgs, System.Action<AbstractEvents.IExceptionHandling<TTable, TField, TValue>>>, PXExceptionHandling>) (adapter => new PXExceptionHandling(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
  }

  private class GenericAbstractFullSpecifiedTypeless<TTable, TField>(ExceptionHandlingEvents parent) : 
    FieldEventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling>.GenericField<AbstractEvents.IExceptionHandling<TTable, TField, object?>, System.Action<AbstractEvents.IExceptionHandling<TTable, TField, object?>>>((FieldEventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling>) parent, typeof (TTable), typeof (TField).Name, (Func<System.Action<AbstractEvents.IExceptionHandling<TTable, TField, object>>, IGenericEventAdapter<PXExceptionHandlingEventArgs, System.Action<AbstractEvents.IExceptionHandling<TTable, TField, object>>>>) (handler => (IGenericEventAdapter<PXExceptionHandlingEventArgs, System.Action<AbstractEvents.IExceptionHandling<TTable, TField, object>>>) new EventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling, PXCache.EventDictionary<PXExceptionHandling>>.Generic<AbstractEvents.IExceptionHandling<TTable, TField, object>, System.Action<AbstractEvents.IExceptionHandling<TTable, TField, object>>>.AbstractAdapter<Events.ExceptionHandling<TTable, TField>>(handler)), (Func<IGenericEventAdapter<PXExceptionHandlingEventArgs, System.Action<AbstractEvents.IExceptionHandling<TTable, TField, object>>>, PXExceptionHandling>) (adapter => new PXExceptionHandling(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAbstractFieldAgnosticTyped<TTable, TValue>(
    ExceptionHandlingEvents parent,
    string fieldName) : 
    FieldEventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling>.GenericField<AbstractEvents.IExceptionHandling<TTable, IBqlField, TValue>, System.Action<AbstractEvents.IExceptionHandling<TTable, IBqlField, TValue>>>((FieldEventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling>) parent, typeof (TTable), fieldName, (Func<System.Action<AbstractEvents.IExceptionHandling<TTable, IBqlField, TValue>>, IGenericEventAdapter<PXExceptionHandlingEventArgs, System.Action<AbstractEvents.IExceptionHandling<TTable, IBqlField, TValue>>>>) (handler => (IGenericEventAdapter<PXExceptionHandlingEventArgs, System.Action<AbstractEvents.IExceptionHandling<TTable, IBqlField, TValue>>>) new EventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling, PXCache.EventDictionary<PXExceptionHandling>>.Generic<AbstractEvents.IExceptionHandling<TTable, IBqlField, TValue>, System.Action<AbstractEvents.IExceptionHandling<TTable, IBqlField, TValue>>>.AbstractAdapter<Events.ExceptionHandling<TTable, AbstractEvents.AnonymousBqlFieldOf<TValue>, TValue>>(handler)), (Func<IGenericEventAdapter<PXExceptionHandlingEventArgs, System.Action<AbstractEvents.IExceptionHandling<TTable, IBqlField, TValue>>>, PXExceptionHandling>) (adapter => new PXExceptionHandling(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }

  private class GenericAbstractFieldOnlyTyped<TField, TValue>(ExceptionHandlingEvents parent) : 
    FieldEventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling>.GenericField<AbstractEvents.IExceptionHandling<object, TField, TValue>, System.Action<AbstractEvents.IExceptionHandling<object, TField, TValue>>>((FieldEventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling>) parent, BqlCommand.GetItemType<TField>(), typeof (TField).Name, (Func<System.Action<AbstractEvents.IExceptionHandling<object, TField, TValue>>, IGenericEventAdapter<PXExceptionHandlingEventArgs, System.Action<AbstractEvents.IExceptionHandling<object, TField, TValue>>>>) (handler => (IGenericEventAdapter<PXExceptionHandlingEventArgs, System.Action<AbstractEvents.IExceptionHandling<object, TField, TValue>>>) new EventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling, PXCache.EventDictionary<PXExceptionHandling>>.Generic<AbstractEvents.IExceptionHandling<object, TField, TValue>, System.Action<AbstractEvents.IExceptionHandling<object, TField, TValue>>>.AbstractAdapter<AbstractEvents.RelaxedExceptionHandling<TField, TValue>>(handler)), (Func<IGenericEventAdapter<PXExceptionHandlingEventArgs, System.Action<AbstractEvents.IExceptionHandling<object, TField, TValue>>>, PXExceptionHandling>) (adapter => new PXExceptionHandling(adapter.Execute)))
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
  }

  private class GenericAbstractFieldOnlyTypeless<TField>(ExceptionHandlingEvents parent) : 
    FieldEventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling>.GenericField<AbstractEvents.IExceptionHandling<object, TField, object?>, System.Action<AbstractEvents.IExceptionHandling<object, TField, object?>>>((FieldEventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling>) parent, BqlCommand.GetItemType<TField>(), typeof (TField).Name, (Func<System.Action<AbstractEvents.IExceptionHandling<object, TField, object>>, IGenericEventAdapter<PXExceptionHandlingEventArgs, System.Action<AbstractEvents.IExceptionHandling<object, TField, object>>>>) (handler => (IGenericEventAdapter<PXExceptionHandlingEventArgs, System.Action<AbstractEvents.IExceptionHandling<object, TField, object>>>) new EventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling, PXCache.EventDictionary<PXExceptionHandling>>.Generic<AbstractEvents.IExceptionHandling<object, TField, object>, System.Action<AbstractEvents.IExceptionHandling<object, TField, object>>>.AbstractAdapter<Events.ExceptionHandling<TField>>(handler)), (Func<IGenericEventAdapter<PXExceptionHandlingEventArgs, System.Action<AbstractEvents.IExceptionHandling<object, TField, object>>>, PXExceptionHandling>) (adapter => new PXExceptionHandling(adapter.Execute)))
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAbstractFullAgnostic(
    ExceptionHandlingEvents parent,
    System.Type table,
    string fieldName) : 
    FieldEventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling>.GenericField<AbstractEvents.IExceptionHandling<object, IBqlField, object?>, System.Action<AbstractEvents.IExceptionHandling<object, IBqlField, object?>>>((FieldEventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling>) parent, table, fieldName, (Func<System.Action<AbstractEvents.IExceptionHandling<object, IBqlField, object>>, IGenericEventAdapter<PXExceptionHandlingEventArgs, System.Action<AbstractEvents.IExceptionHandling<object, IBqlField, object>>>>) (handler => (IGenericEventAdapter<PXExceptionHandlingEventArgs, System.Action<AbstractEvents.IExceptionHandling<object, IBqlField, object>>>) new EventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling, PXCache.EventDictionary<PXExceptionHandling>>.Generic<AbstractEvents.IExceptionHandling<object, IBqlField, object>, System.Action<AbstractEvents.IExceptionHandling<object, IBqlField, object>>>.AbstractAdapter<Events.ExceptionHandling<AbstractEvents.IFakeBqlField>>(handler)), (Func<IGenericEventAdapter<PXExceptionHandlingEventArgs, System.Action<AbstractEvents.IExceptionHandling<object, IBqlField, object>>>, PXExceptionHandling>) (adapter => new PXExceptionHandling(adapter.Execute)))
  {
  }

  private class GenericAutoFullSpecified<TTable, TField, TValue>(ExceptionHandlingEvents parent) : 
    FieldEventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling>.GenericField<Events.ExceptionHandling<TTable, TField, TValue>, Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TTable, TField, TValue>>.EventDelegate>((FieldEventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling>) parent, typeof (TTable), typeof (TField).Name, (Func<Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TTable, TField, TValue>>.EventDelegate, IGenericEventAdapter<PXExceptionHandlingEventArgs, Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TTable, TField, TValue>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXExceptionHandlingEventArgs, Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TTable, TField, TValue>>.EventDelegate>) new Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TTable, TField, TValue>>.Adapter(handler)), (Func<IGenericEventAdapter<PXExceptionHandlingEventArgs, Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TTable, TField, TValue>>.EventDelegate>, PXExceptionHandling>) (adapter => new PXExceptionHandling(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
  }

  private class GenericAutoTypeAgnostic<TTable, TField>(ExceptionHandlingEvents parent) : 
    FieldEventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling>.GenericField<Events.ExceptionHandling<TTable, TField>, Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TTable, TField>>.EventDelegate>((FieldEventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling>) parent, typeof (TTable), typeof (TField).Name, (Func<Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TTable, TField>>.EventDelegate, IGenericEventAdapter<PXExceptionHandlingEventArgs, Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TTable, TField>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXExceptionHandlingEventArgs, Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TTable, TField>>.EventDelegate>) new Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TTable, TField>>.Adapter(handler)), (Func<IGenericEventAdapter<PXExceptionHandlingEventArgs, Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TTable, TField>>.EventDelegate>, PXExceptionHandling>) (adapter => new PXExceptionHandling(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAutoFieldOnly<TField>(ExceptionHandlingEvents parent) : 
    FieldEventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling>.GenericField<Events.ExceptionHandling<TField>, Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TField>>.EventDelegate>((FieldEventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling>) parent, BqlCommand.GetItemType<TField>(), typeof (TField).Name, (Func<Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TField>>.EventDelegate, IGenericEventAdapter<PXExceptionHandlingEventArgs, Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TField>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXExceptionHandlingEventArgs, Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TField>>.EventDelegate>) new Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TField>>.Adapter(handler)), (Func<IGenericEventAdapter<PXExceptionHandlingEventArgs, Events.Event<PXExceptionHandlingEventArgs, Events.ExceptionHandling<TField>>.EventDelegate>, PXExceptionHandling>) (adapter => new PXExceptionHandling(adapter.Execute)))
    where TField : class, IBqlField
  {
  }
}
