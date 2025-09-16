// Decompiled with JetBrains decompiler
// Type: PX.Data.FieldUpdatingEvents
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
public abstract class FieldUpdatingEvents(PXGraph graph) : 
  FieldEventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating>(graph)
{
  protected override PXFieldUpdating? Get(PXCache cache, string field)
  {
    PXFieldUpdating pxFieldUpdating;
    return !cache.FieldUpdatingEvents.TryGetValue(field, out pxFieldUpdating) ? (PXFieldUpdating) null : pxFieldUpdating;
  }

  protected override void Add(PXCache cache, string field, PXFieldUpdating handler)
  {
    cache.FieldUpdatingEvents[field] = handler + cache.FieldUpdatingEvents[field];
  }

  protected override void Remove(PXCache cache, string field, PXFieldUpdating handler)
  {
    cache.FieldUpdatingEvents[field] -= handler;
  }

  private protected override void SetProxy(
    PXCache cache,
    string field,
    EventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating, PXCache.EventDictionary<PXFieldUpdating>>.Proxy proxy)
  {
    cache.FieldUpdatingEvents[field] = new PXFieldUpdating(proxy.Intercept);
  }

  private protected override void SetProxy(
    PXCache.EventDictionary<PXFieldUpdating> delayed,
    string field,
    EventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating, PXCache.EventDictionary<PXFieldUpdating>>.Proxy proxy)
  {
    delayed[field] = new PXFieldUpdating(proxy.Intercept);
  }

  protected override void Add(
    PXCache.EventDictionary<PXFieldUpdating> delayed,
    string field,
    PXFieldUpdating handler)
  {
    delayed[field] = handler + delayed[field];
  }

  protected override void Remove(
    PXCache.EventDictionary<PXFieldUpdating> delayed,
    string field,
    PXFieldUpdating handler)
  {
    delayed[field] -= handler;
  }

  public void AddAbstractHandler<TTable, TField>(
    System.Action<AbstractEvents.IFieldUpdating<TTable, TField>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldUpdating classicHandler = new FieldUpdatingEvents.GenericAbstractFullSpecified<TTable, TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TField>(
    System.Action<AbstractEvents.IFieldUpdating<TTable, TField>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldUpdating classicHandler = new FieldUpdatingEvents.GenericAbstractFullSpecified<TTable, TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void AddAbstractHandler<TTable>(
    string fieldName,
    System.Action<AbstractEvents.IFieldUpdating<TTable, IBqlField>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXFieldUpdating classicHandler = new FieldUpdatingEvents.GenericAbstractFieldAgnostic<TTable>(this, fieldName).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void RemoveAbstractHandler<TTable>(
    string fieldName,
    System.Action<AbstractEvents.IFieldUpdating<TTable, IBqlField>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXFieldUpdating classicHandler = new FieldUpdatingEvents.GenericAbstractFieldAgnostic<TTable>(this, fieldName).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void AddAbstractHandler<TField>(
    System.Action<AbstractEvents.IFieldUpdating<object, TField>> handler)
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldUpdating classicHandler = new FieldUpdatingEvents.GenericAbstractFieldOnly<TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveAbstractHandler<TField>(
    System.Action<AbstractEvents.IFieldUpdating<object, TField>> handler)
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldUpdating classicHandler = new FieldUpdatingEvents.GenericAbstractFieldOnly<TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  public void AddAbstractHandler(
    System.Type table,
    string fieldName,
    System.Action<AbstractEvents.IFieldUpdating<object, IBqlField>> handler)
  {
    PXFieldUpdating classicHandler = new FieldUpdatingEvents.GenericAbstractFullAgnostic(this, table, fieldName).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(table, fieldName, classicHandler);
  }

  public void RemoveAbstractHandler(
    System.Type table,
    string fieldName,
    System.Action<AbstractEvents.IFieldUpdating<object, IBqlField>> handler)
  {
    PXFieldUpdating classicHandler = new FieldUpdatingEvents.GenericAbstractFullAgnostic(this, table, fieldName).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(table, fieldName, classicHandler);
  }

  public void RemoveHandler<TTable, TField, TValue>(
    Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TTable, TField, TValue>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
  {
    PXFieldUpdating classicHandler = new FieldUpdatingEvents.GenericAutoFullSpecified<TTable, TField, TValue>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveHandler<TTable, TField>(
    Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TTable, TField>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXFieldUpdating classicHandler = new FieldUpdatingEvents.GenericAutoTypeAgnostic<TTable, TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  private protected void AddHandler<TField>(
    Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TField>>.EventDelegate handler)
    where TField : class, IBqlField
  {
    PXFieldUpdating classicHandler = new FieldUpdatingEvents.GenericAutoFieldOnly<TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveHandler<TField>(
    Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TField>>.EventDelegate handler)
    where TField : class, IBqlField
  {
    PXFieldUpdating classicHandler = new FieldUpdatingEvents.GenericAutoFieldOnly<TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  private class GenericAbstractFullSpecified<TTable, TField>(FieldUpdatingEvents parent) : 
    FieldEventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating>.GenericField<AbstractEvents.IFieldUpdating<TTable, TField>, System.Action<AbstractEvents.IFieldUpdating<TTable, TField>>>((FieldEventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating>) parent, typeof (TTable), typeof (TField).Name, (Func<System.Action<AbstractEvents.IFieldUpdating<TTable, TField>>, IGenericEventAdapter<PXFieldUpdatingEventArgs, System.Action<AbstractEvents.IFieldUpdating<TTable, TField>>>>) (handler => (IGenericEventAdapter<PXFieldUpdatingEventArgs, System.Action<AbstractEvents.IFieldUpdating<TTable, TField>>>) new EventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating, PXCache.EventDictionary<PXFieldUpdating>>.Generic<AbstractEvents.IFieldUpdating<TTable, TField>, System.Action<AbstractEvents.IFieldUpdating<TTable, TField>>>.AbstractAdapter<Events.FieldUpdating<TTable, TField>>(handler)), (Func<IGenericEventAdapter<PXFieldUpdatingEventArgs, System.Action<AbstractEvents.IFieldUpdating<TTable, TField>>>, PXFieldUpdating>) (adapter => new PXFieldUpdating(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAbstractFieldAgnostic<TTable>(FieldUpdatingEvents parent, string fieldName) : 
    FieldEventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating>.GenericField<AbstractEvents.IFieldUpdating<TTable, IBqlField>, System.Action<AbstractEvents.IFieldUpdating<TTable, IBqlField>>>((FieldEventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating>) parent, typeof (TTable), fieldName, (Func<System.Action<AbstractEvents.IFieldUpdating<TTable, IBqlField>>, IGenericEventAdapter<PXFieldUpdatingEventArgs, System.Action<AbstractEvents.IFieldUpdating<TTable, IBqlField>>>>) (handler => (IGenericEventAdapter<PXFieldUpdatingEventArgs, System.Action<AbstractEvents.IFieldUpdating<TTable, IBqlField>>>) new EventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating, PXCache.EventDictionary<PXFieldUpdating>>.Generic<AbstractEvents.IFieldUpdating<TTable, IBqlField>, System.Action<AbstractEvents.IFieldUpdating<TTable, IBqlField>>>.AbstractAdapter<Events.FieldUpdating<TTable, AbstractEvents.IFakeBqlField>>(handler)), (Func<IGenericEventAdapter<PXFieldUpdatingEventArgs, System.Action<AbstractEvents.IFieldUpdating<TTable, IBqlField>>>, PXFieldUpdating>) (adapter => new PXFieldUpdating(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }

  private class GenericAbstractFieldOnly<TField>(FieldUpdatingEvents parent) : 
    FieldEventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating>.GenericField<AbstractEvents.IFieldUpdating<object, TField>, System.Action<AbstractEvents.IFieldUpdating<object, TField>>>((FieldEventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating>) parent, BqlCommand.GetItemType<TField>(), typeof (TField).Name, (Func<System.Action<AbstractEvents.IFieldUpdating<object, TField>>, IGenericEventAdapter<PXFieldUpdatingEventArgs, System.Action<AbstractEvents.IFieldUpdating<object, TField>>>>) (handler => (IGenericEventAdapter<PXFieldUpdatingEventArgs, System.Action<AbstractEvents.IFieldUpdating<object, TField>>>) new EventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating, PXCache.EventDictionary<PXFieldUpdating>>.Generic<AbstractEvents.IFieldUpdating<object, TField>, System.Action<AbstractEvents.IFieldUpdating<object, TField>>>.AbstractAdapter<Events.FieldUpdating<TField>>(handler)), (Func<IGenericEventAdapter<PXFieldUpdatingEventArgs, System.Action<AbstractEvents.IFieldUpdating<object, TField>>>, PXFieldUpdating>) (adapter => new PXFieldUpdating(adapter.Execute)))
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAbstractFullAgnostic(
    FieldUpdatingEvents parent,
    System.Type table,
    string fieldName) : 
    FieldEventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating>.GenericField<AbstractEvents.IFieldUpdating<object, IBqlField>, System.Action<AbstractEvents.IFieldUpdating<object, IBqlField>>>((FieldEventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating>) parent, table, fieldName, (Func<System.Action<AbstractEvents.IFieldUpdating<object, IBqlField>>, IGenericEventAdapter<PXFieldUpdatingEventArgs, System.Action<AbstractEvents.IFieldUpdating<object, IBqlField>>>>) (handler => (IGenericEventAdapter<PXFieldUpdatingEventArgs, System.Action<AbstractEvents.IFieldUpdating<object, IBqlField>>>) new EventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating, PXCache.EventDictionary<PXFieldUpdating>>.Generic<AbstractEvents.IFieldUpdating<object, IBqlField>, System.Action<AbstractEvents.IFieldUpdating<object, IBqlField>>>.AbstractAdapter<Events.FieldUpdating<AbstractEvents.IFakeBqlField>>(handler)), (Func<IGenericEventAdapter<PXFieldUpdatingEventArgs, System.Action<AbstractEvents.IFieldUpdating<object, IBqlField>>>, PXFieldUpdating>) (adapter => new PXFieldUpdating(adapter.Execute)))
  {
  }

  private class GenericAutoFullSpecified<TTable, TField, TValue>(FieldUpdatingEvents parent) : 
    FieldEventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating>.GenericField<Events.FieldUpdating<TTable, TField, TValue>, Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TTable, TField, TValue>>.EventDelegate>((FieldEventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating>) parent, typeof (TTable), typeof (TField).Name, (Func<Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TTable, TField, TValue>>.EventDelegate, IGenericEventAdapter<PXFieldUpdatingEventArgs, Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TTable, TField, TValue>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXFieldUpdatingEventArgs, Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TTable, TField, TValue>>.EventDelegate>) new Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TTable, TField, TValue>>.Adapter(handler)), (Func<IGenericEventAdapter<PXFieldUpdatingEventArgs, Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TTable, TField, TValue>>.EventDelegate>, PXFieldUpdating>) (adapter => new PXFieldUpdating(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
  {
  }

  private class GenericAutoTypeAgnostic<TTable, TField>(FieldUpdatingEvents parent) : 
    FieldEventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating>.GenericField<Events.FieldUpdating<TTable, TField>, Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TTable, TField>>.EventDelegate>((FieldEventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating>) parent, typeof (TTable), typeof (TField).Name, (Func<Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TTable, TField>>.EventDelegate, IGenericEventAdapter<PXFieldUpdatingEventArgs, Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TTable, TField>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXFieldUpdatingEventArgs, Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TTable, TField>>.EventDelegate>) new Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TTable, TField>>.Adapter(handler)), (Func<IGenericEventAdapter<PXFieldUpdatingEventArgs, Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TTable, TField>>.EventDelegate>, PXFieldUpdating>) (adapter => new PXFieldUpdating(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAutoFieldOnly<TField>(FieldUpdatingEvents parent) : 
    FieldEventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating>.GenericField<Events.FieldUpdating<TField>, Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TField>>.EventDelegate>((FieldEventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating>) parent, BqlCommand.GetItemType<TField>(), typeof (TField).Name, (Func<Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TField>>.EventDelegate, IGenericEventAdapter<PXFieldUpdatingEventArgs, Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TField>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXFieldUpdatingEventArgs, Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TField>>.EventDelegate>) new Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TField>>.Adapter(handler)), (Func<IGenericEventAdapter<PXFieldUpdatingEventArgs, Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TField>>.EventDelegate>, PXFieldUpdating>) (adapter => new PXFieldUpdating(adapter.Execute)))
    where TField : class, IBqlField
  {
  }
}
