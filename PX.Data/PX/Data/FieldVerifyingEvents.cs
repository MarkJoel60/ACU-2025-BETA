// Decompiled with JetBrains decompiler
// Type: PX.Data.FieldVerifyingEvents
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
public abstract class FieldVerifyingEvents(PXGraph graph) : 
  FieldEventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying>(graph)
{
  protected override PXFieldVerifying? Get(PXCache cache, string field)
  {
    PXFieldVerifying pxFieldVerifying;
    return !cache.FieldVerifyingEvents.TryGetValue(field, out pxFieldVerifying) ? (PXFieldVerifying) null : pxFieldVerifying;
  }

  protected override void Add(PXCache cache, string field, PXFieldVerifying handler)
  {
    cache.FieldVerifyingEvents[field] = handler + cache.FieldVerifyingEvents[field];
  }

  protected override void Remove(PXCache cache, string field, PXFieldVerifying handler)
  {
    cache.FieldVerifyingEvents[field] -= handler;
  }

  private protected override void SetProxy(
    PXCache cache,
    string field,
    EventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying, PXCache.EventDictionary<PXFieldVerifying>>.Proxy proxy)
  {
    cache.FieldVerifyingEvents[field] = new PXFieldVerifying(proxy.Intercept);
  }

  private protected override void SetProxy(
    PXCache.EventDictionary<PXFieldVerifying> delayed,
    string field,
    EventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying, PXCache.EventDictionary<PXFieldVerifying>>.Proxy proxy)
  {
    delayed[field] = new PXFieldVerifying(proxy.Intercept);
  }

  protected override void Add(
    PXCache.EventDictionary<PXFieldVerifying> delayed,
    string field,
    PXFieldVerifying handler)
  {
    delayed[field] = handler + delayed[field];
  }

  protected override void Remove(
    PXCache.EventDictionary<PXFieldVerifying> delayed,
    string field,
    PXFieldVerifying handler)
  {
    delayed[field] -= handler;
  }

  public void AddAbstractHandler<TTable, TField, TValue>(
    System.Action<AbstractEvents.IFieldVerifying<TTable, TField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : class
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAbstractFullSpecifiedTyped<TTable, TField, TValue>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TField, TValue>(
    System.Action<AbstractEvents.IFieldVerifying<TTable, TField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : class
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAbstractFullSpecifiedTyped<TTable, TField, TValue>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void AddAbstractHandler<TTable, TField, TValue>(
    System.Action<AbstractEvents.IFieldVerifying<TTable, TField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : struct
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAbstractFullSpecifiedTyped<TTable, TField, TValue?>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TField, TValue>(
    System.Action<AbstractEvents.IFieldVerifying<TTable, TField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : struct
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAbstractFullSpecifiedTyped<TTable, TField, TValue?>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void AddAbstractHandler<TTable, TField>(
    System.Action<AbstractEvents.IFieldVerifying<TTable, TField, object?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAbstractFullSpecifiedTypeless<TTable, TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TField>(
    System.Action<AbstractEvents.IFieldVerifying<TTable, TField, object?>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAbstractFullSpecifiedTypeless<TTable, TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void AddAbstractHandler<TTable, TValue>(
    string fieldName,
    System.Action<AbstractEvents.IFieldVerifying<TTable, IBqlField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TValue : class
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAbstractFieldAgnosticTyped<TTable, TValue>(this, fieldName).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TValue>(
    string fieldName,
    System.Action<AbstractEvents.IFieldVerifying<TTable, IBqlField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TValue : class
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAbstractFieldAgnosticTyped<TTable, TValue>(this, fieldName).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void AddAbstractHandler<TTable, TValue>(
    string fieldName,
    System.Action<AbstractEvents.IFieldVerifying<TTable, IBqlField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TValue : struct
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAbstractFieldAgnosticTyped<TTable, TValue?>(this, fieldName).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TValue>(
    string fieldName,
    System.Action<AbstractEvents.IFieldVerifying<TTable, IBqlField, TValue?>> handler)
    where TTable : class, IBqlTable, new()
    where TValue : struct
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAbstractFieldAgnosticTyped<TTable, TValue?>(this, fieldName).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void AddAbstractHandler<TTable>(
    string fieldName,
    System.Action<AbstractEvents.IFieldVerifying<TTable, IBqlField, object?>> handler)
    where TTable : class, IBqlTable, new()
  {
    this.AddAbstractHandler<TTable, object>(fieldName, handler);
  }

  public void RemoveAbstractHandler<TTable>(
    string fieldName,
    System.Action<AbstractEvents.IFieldVerifying<TTable, IBqlField, object?>> handler)
    where TTable : class, IBqlTable, new()
  {
    this.RemoveAbstractHandler<TTable, object>(fieldName, handler);
  }

  public void AddAbstractHandler<TField, TValue>(
    System.Action<AbstractEvents.IFieldVerifying<object, TField, TValue?>> handler)
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : class
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAbstractFieldOnlyTyped<TField, TValue>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveAbstractHandler<TField, TValue>(
    System.Action<AbstractEvents.IFieldVerifying<object, TField, TValue?>> handler)
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : class
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAbstractFieldOnlyTyped<TField, TValue>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  public void AddAbstractHandler<TField, TValue>(
    System.Action<AbstractEvents.IFieldVerifying<object, TField, TValue?>> handler)
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : struct
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAbstractFieldOnlyTyped<TField, TValue?>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveAbstractHandler<TField, TValue>(
    System.Action<AbstractEvents.IFieldVerifying<object, TField, TValue?>> handler)
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
    where TValue : struct
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAbstractFieldOnlyTyped<TField, TValue?>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  public void AddAbstractHandler<TField>(
    System.Action<AbstractEvents.IFieldVerifying<object, TField, object?>> handler)
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAbstractFieldOnlyTypeless<TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveAbstractHandler<TField>(
    System.Action<AbstractEvents.IFieldVerifying<object, TField, object?>> handler)
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAbstractFieldOnlyTypeless<TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  public void AddAbstractHandler(
    System.Type table,
    string fieldName,
    System.Action<AbstractEvents.IFieldVerifying<object, IBqlField, object?>> handler)
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAbstractFullAgnostic(this, table, fieldName).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(table, fieldName, classicHandler);
  }

  public void RemoveAbstractHandler(
    System.Type table,
    string fieldName,
    System.Action<AbstractEvents.IFieldVerifying<object, IBqlField, object?>> handler)
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAbstractFullAgnostic(this, table, fieldName).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(table, fieldName, classicHandler);
  }

  public void RemoveHandler<TTable, TField, TValue>(
    Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TTable, TField, TValue>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAutoFullSpecified<TTable, TField, TValue>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveHandler<TTable, TField>(
    Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TTable, TField>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAutoTypeAgnostic<TTable, TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  private protected void AddHandler<TField>(
    Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TField>>.EventDelegate handler)
    where TField : class, IBqlField
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAutoFieldOnly<TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveHandler<TField>(
    Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TField>>.EventDelegate handler)
    where TField : class, IBqlField
  {
    PXFieldVerifying classicHandler = new FieldVerifyingEvents.GenericAutoFieldOnly<TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  private class GenericAbstractFullSpecifiedTyped<TTable, TField, TValue>(
    FieldVerifyingEvents parent) : 
    FieldEventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying>.GenericField<AbstractEvents.IFieldVerifying<TTable, TField, TValue>, System.Action<AbstractEvents.IFieldVerifying<TTable, TField, TValue>>>((FieldEventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying>) parent, typeof (TTable), typeof (TField).Name, (Func<System.Action<AbstractEvents.IFieldVerifying<TTable, TField, TValue>>, IGenericEventAdapter<PXFieldVerifyingEventArgs, System.Action<AbstractEvents.IFieldVerifying<TTable, TField, TValue>>>>) (handler => (IGenericEventAdapter<PXFieldVerifyingEventArgs, System.Action<AbstractEvents.IFieldVerifying<TTable, TField, TValue>>>) new EventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying, PXCache.EventDictionary<PXFieldVerifying>>.Generic<AbstractEvents.IFieldVerifying<TTable, TField, TValue>, System.Action<AbstractEvents.IFieldVerifying<TTable, TField, TValue>>>.AbstractAdapter<Events.FieldVerifying<TTable, TField, TValue>>(handler)), (Func<IGenericEventAdapter<PXFieldVerifyingEventArgs, System.Action<AbstractEvents.IFieldVerifying<TTable, TField, TValue>>>, PXFieldVerifying>) (adapter => new PXFieldVerifying(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
  }

  private class GenericAbstractFullSpecifiedTypeless<TTable, TField>(FieldVerifyingEvents parent) : 
    FieldEventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying>.GenericField<AbstractEvents.IFieldVerifying<TTable, TField, object?>, System.Action<AbstractEvents.IFieldVerifying<TTable, TField, object?>>>((FieldEventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying>) parent, typeof (TTable), typeof (TField).Name, (Func<System.Action<AbstractEvents.IFieldVerifying<TTable, TField, object>>, IGenericEventAdapter<PXFieldVerifyingEventArgs, System.Action<AbstractEvents.IFieldVerifying<TTable, TField, object>>>>) (handler => (IGenericEventAdapter<PXFieldVerifyingEventArgs, System.Action<AbstractEvents.IFieldVerifying<TTable, TField, object>>>) new EventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying, PXCache.EventDictionary<PXFieldVerifying>>.Generic<AbstractEvents.IFieldVerifying<TTable, TField, object>, System.Action<AbstractEvents.IFieldVerifying<TTable, TField, object>>>.AbstractAdapter<Events.FieldVerifying<TTable, TField>>(handler)), (Func<IGenericEventAdapter<PXFieldVerifyingEventArgs, System.Action<AbstractEvents.IFieldVerifying<TTable, TField, object>>>, PXFieldVerifying>) (adapter => new PXFieldVerifying(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAbstractFieldAgnosticTyped<TTable, TValue>(
    FieldVerifyingEvents parent,
    string fieldName) : 
    FieldEventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying>.GenericField<AbstractEvents.IFieldVerifying<TTable, IBqlField, TValue>, System.Action<AbstractEvents.IFieldVerifying<TTable, IBqlField, TValue>>>((FieldEventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying>) parent, typeof (TTable), fieldName, (Func<System.Action<AbstractEvents.IFieldVerifying<TTable, IBqlField, TValue>>, IGenericEventAdapter<PXFieldVerifyingEventArgs, System.Action<AbstractEvents.IFieldVerifying<TTable, IBqlField, TValue>>>>) (handler => (IGenericEventAdapter<PXFieldVerifyingEventArgs, System.Action<AbstractEvents.IFieldVerifying<TTable, IBqlField, TValue>>>) new EventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying, PXCache.EventDictionary<PXFieldVerifying>>.Generic<AbstractEvents.IFieldVerifying<TTable, IBqlField, TValue>, System.Action<AbstractEvents.IFieldVerifying<TTable, IBqlField, TValue>>>.AbstractAdapter<Events.FieldVerifying<TTable, AbstractEvents.AnonymousBqlFieldOf<TValue>, TValue>>(handler)), (Func<IGenericEventAdapter<PXFieldVerifyingEventArgs, System.Action<AbstractEvents.IFieldVerifying<TTable, IBqlField, TValue>>>, PXFieldVerifying>) (adapter => new PXFieldVerifying(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }

  private class GenericAbstractFieldOnlyTyped<TField, TValue>(FieldVerifyingEvents parent) : 
    FieldEventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying>.GenericField<AbstractEvents.IFieldVerifying<object, TField, TValue>, System.Action<AbstractEvents.IFieldVerifying<object, TField, TValue>>>((FieldEventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying>) parent, BqlCommand.GetItemType<TField>(), typeof (TField).Name, (Func<System.Action<AbstractEvents.IFieldVerifying<object, TField, TValue>>, IGenericEventAdapter<PXFieldVerifyingEventArgs, System.Action<AbstractEvents.IFieldVerifying<object, TField, TValue>>>>) (handler => (IGenericEventAdapter<PXFieldVerifyingEventArgs, System.Action<AbstractEvents.IFieldVerifying<object, TField, TValue>>>) new EventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying, PXCache.EventDictionary<PXFieldVerifying>>.Generic<AbstractEvents.IFieldVerifying<object, TField, TValue>, System.Action<AbstractEvents.IFieldVerifying<object, TField, TValue>>>.AbstractAdapter<AbstractEvents.RelaxedFieldVerifying<TField, TValue>>(handler)), (Func<IGenericEventAdapter<PXFieldVerifyingEventArgs, System.Action<AbstractEvents.IFieldVerifying<object, TField, TValue>>>, PXFieldVerifying>) (adapter => new PXFieldVerifying(adapter.Execute)))
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
  }

  private class GenericAbstractFieldOnlyTypeless<TField>(FieldVerifyingEvents parent) : 
    FieldEventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying>.GenericField<AbstractEvents.IFieldVerifying<object, TField, object?>, System.Action<AbstractEvents.IFieldVerifying<object, TField, object?>>>((FieldEventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying>) parent, BqlCommand.GetItemType<TField>(), typeof (TField).Name, (Func<System.Action<AbstractEvents.IFieldVerifying<object, TField, object>>, IGenericEventAdapter<PXFieldVerifyingEventArgs, System.Action<AbstractEvents.IFieldVerifying<object, TField, object>>>>) (handler => (IGenericEventAdapter<PXFieldVerifyingEventArgs, System.Action<AbstractEvents.IFieldVerifying<object, TField, object>>>) new EventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying, PXCache.EventDictionary<PXFieldVerifying>>.Generic<AbstractEvents.IFieldVerifying<object, TField, object>, System.Action<AbstractEvents.IFieldVerifying<object, TField, object>>>.AbstractAdapter<Events.FieldVerifying<TField>>(handler)), (Func<IGenericEventAdapter<PXFieldVerifyingEventArgs, System.Action<AbstractEvents.IFieldVerifying<object, TField, object>>>, PXFieldVerifying>) (adapter => new PXFieldVerifying(adapter.Execute)))
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAbstractFullAgnostic(
    FieldVerifyingEvents parent,
    System.Type table,
    string fieldName) : 
    FieldEventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying>.GenericField<AbstractEvents.IFieldVerifying<object, IBqlField, object?>, System.Action<AbstractEvents.IFieldVerifying<object, IBqlField, object?>>>((FieldEventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying>) parent, table, fieldName, (Func<System.Action<AbstractEvents.IFieldVerifying<object, IBqlField, object>>, IGenericEventAdapter<PXFieldVerifyingEventArgs, System.Action<AbstractEvents.IFieldVerifying<object, IBqlField, object>>>>) (handler => (IGenericEventAdapter<PXFieldVerifyingEventArgs, System.Action<AbstractEvents.IFieldVerifying<object, IBqlField, object>>>) new EventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying, PXCache.EventDictionary<PXFieldVerifying>>.Generic<AbstractEvents.IFieldVerifying<object, IBqlField, object>, System.Action<AbstractEvents.IFieldVerifying<object, IBqlField, object>>>.AbstractAdapter<Events.FieldVerifying<AbstractEvents.IFakeBqlField>>(handler)), (Func<IGenericEventAdapter<PXFieldVerifyingEventArgs, System.Action<AbstractEvents.IFieldVerifying<object, IBqlField, object>>>, PXFieldVerifying>) (adapter => new PXFieldVerifying(adapter.Execute)))
  {
  }

  private class GenericAutoFullSpecified<TTable, TField, TValue>(FieldVerifyingEvents parent) : 
    FieldEventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying>.GenericField<Events.FieldVerifying<TTable, TField, TValue>, Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TTable, TField, TValue>>.EventDelegate>((FieldEventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying>) parent, typeof (TTable), typeof (TField).Name, (Func<Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TTable, TField, TValue>>.EventDelegate, IGenericEventAdapter<PXFieldVerifyingEventArgs, Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TTable, TField, TValue>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXFieldVerifyingEventArgs, Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TTable, TField, TValue>>.EventDelegate>) new Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TTable, TField, TValue>>.Adapter(handler)), (Func<IGenericEventAdapter<PXFieldVerifyingEventArgs, Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TTable, TField, TValue>>.EventDelegate>, PXFieldVerifying>) (adapter => new PXFieldVerifying(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
  }

  private class GenericAutoTypeAgnostic<TTable, TField>(FieldVerifyingEvents parent) : 
    FieldEventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying>.GenericField<Events.FieldVerifying<TTable, TField>, Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TTable, TField>>.EventDelegate>((FieldEventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying>) parent, typeof (TTable), typeof (TField).Name, (Func<Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TTable, TField>>.EventDelegate, IGenericEventAdapter<PXFieldVerifyingEventArgs, Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TTable, TField>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXFieldVerifyingEventArgs, Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TTable, TField>>.EventDelegate>) new Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TTable, TField>>.Adapter(handler)), (Func<IGenericEventAdapter<PXFieldVerifyingEventArgs, Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TTable, TField>>.EventDelegate>, PXFieldVerifying>) (adapter => new PXFieldVerifying(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAutoFieldOnly<TField>(FieldVerifyingEvents parent) : 
    FieldEventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying>.GenericField<Events.FieldVerifying<TField>, Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TField>>.EventDelegate>((FieldEventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying>) parent, BqlCommand.GetItemType<TField>(), typeof (TField).Name, (Func<Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TField>>.EventDelegate, IGenericEventAdapter<PXFieldVerifyingEventArgs, Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TField>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXFieldVerifyingEventArgs, Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TField>>.EventDelegate>) new Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TField>>.Adapter(handler)), (Func<IGenericEventAdapter<PXFieldVerifyingEventArgs, Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<TField>>.EventDelegate>, PXFieldVerifying>) (adapter => new PXFieldVerifying(adapter.Execute)))
    where TField : class, IBqlField
  {
  }
}
