// Decompiled with JetBrains decompiler
// Type: PX.Data.CommandPreparingEvents
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
public abstract class CommandPreparingEvents(PXGraph graph) : 
  FieldEventsBase<PXCommandPreparingEventArgs, PXCommandPreparing>(graph)
{
  protected override PXCommandPreparing? Get(PXCache cache, string field)
  {
    PXCommandPreparing commandPreparing;
    return !cache.CommandPreparingEvents.TryGetValue(field, out commandPreparing) ? (PXCommandPreparing) null : commandPreparing;
  }

  protected override void Add(PXCache cache, string field, PXCommandPreparing handler)
  {
    cache.CommandPreparingEvents[field] = handler + cache.CommandPreparingEvents[field];
  }

  protected override void Remove(PXCache cache, string field, PXCommandPreparing handler)
  {
    cache.CommandPreparingEvents[field] -= handler;
  }

  private protected override void SetProxy(
    PXCache cache,
    string field,
    EventsBase<PXCommandPreparingEventArgs, PXCommandPreparing, PXCache.EventDictionary<PXCommandPreparing>>.Proxy proxy)
  {
    cache.CommandPreparingEvents[field] = new PXCommandPreparing(proxy.Intercept);
  }

  private protected override void SetProxy(
    PXCache.EventDictionary<PXCommandPreparing> delayed,
    string field,
    EventsBase<PXCommandPreparingEventArgs, PXCommandPreparing, PXCache.EventDictionary<PXCommandPreparing>>.Proxy proxy)
  {
    delayed[field] = new PXCommandPreparing(proxy.Intercept);
  }

  protected override void Add(
    PXCache.EventDictionary<PXCommandPreparing> delayed,
    string field,
    PXCommandPreparing handler)
  {
    delayed[field] = handler + delayed[field];
  }

  protected override void Remove(
    PXCache.EventDictionary<PXCommandPreparing> delayed,
    string field,
    PXCommandPreparing handler)
  {
    delayed[field] -= handler;
  }

  public void AddAbstractHandler<TTable, TField>(
    System.Action<AbstractEvents.ICommandPreparing<TTable, TField>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXCommandPreparing classicHandler = new CommandPreparingEvents.GenericAbstractFullSpecified<TTable, TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void RemoveAbstractHandler<TTable, TField>(
    System.Action<AbstractEvents.ICommandPreparing<TTable, TField>> handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXCommandPreparing classicHandler = new CommandPreparingEvents.GenericAbstractFullSpecified<TTable, TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  public void AddAbstractHandler<TTable>(
    string fieldName,
    System.Action<AbstractEvents.ICommandPreparing<TTable, IBqlField>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXCommandPreparing classicHandler = new CommandPreparingEvents.GenericAbstractFieldAgnostic<TTable>(this, fieldName).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void RemoveAbstractHandler<TTable>(
    string fieldName,
    System.Action<AbstractEvents.ICommandPreparing<TTable, IBqlField>> handler)
    where TTable : class, IBqlTable, new()
  {
    PXCommandPreparing classicHandler = new CommandPreparingEvents.GenericAbstractFieldAgnostic<TTable>(this, fieldName).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), fieldName, classicHandler);
  }

  public void AddAbstractHandler<TField>(
    System.Action<AbstractEvents.ICommandPreparing<object, TField>> handler)
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXCommandPreparing classicHandler = new CommandPreparingEvents.GenericAbstractFieldOnly<TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveAbstractHandler<TField>(
    System.Action<AbstractEvents.ICommandPreparing<object, TField>> handler)
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXCommandPreparing classicHandler = new CommandPreparingEvents.GenericAbstractFieldOnly<TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  public void AddAbstractHandler(
    System.Type table,
    string fieldName,
    System.Action<AbstractEvents.ICommandPreparing<object, IBqlField>> handler)
  {
    PXCommandPreparing classicHandler = new CommandPreparingEvents.GenericAbstractFullAgnostic(this, table, fieldName).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler(table, fieldName, classicHandler);
  }

  public void RemoveAbstractHandler(
    System.Type table,
    string fieldName,
    System.Action<AbstractEvents.ICommandPreparing<object, IBqlField>> handler)
  {
    PXCommandPreparing classicHandler = new CommandPreparingEvents.GenericAbstractFullAgnostic(this, table, fieldName).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(table, fieldName, classicHandler);
  }

  public void RemoveHandler<TTable, TField>(
    Events.Event<PXCommandPreparingEventArgs, Events.CommandPreparing<TTable, TField>>.EventDelegate handler)
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
    PXCommandPreparing classicHandler = new CommandPreparingEvents.GenericAutoFullSpecified<TTable, TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler(typeof (TTable), typeof (TField).Name, classicHandler);
  }

  private protected void AddHandler<TField>(
    Events.Event<PXCommandPreparingEventArgs, Events.CommandPreparing<TField>>.EventDelegate handler)
    where TField : class, IBqlField
  {
    PXCommandPreparing classicHandler = new CommandPreparingEvents.GenericAutoFieldOnly<TField>(this).GetClassicHandler(handler);
    if (classicHandler == null)
      return;
    this.AddHandler<TField>(classicHandler);
  }

  public void RemoveHandler<TField>(
    Events.Event<PXCommandPreparingEventArgs, Events.CommandPreparing<TField>>.EventDelegate handler)
    where TField : class, IBqlField
  {
    PXCommandPreparing classicHandler = new CommandPreparingEvents.GenericAutoFieldOnly<TField>(this).GetClassicHandler(handler, true);
    if (classicHandler == null)
      return;
    this.RemoveHandler<TField>(classicHandler);
  }

  private class GenericAbstractFullSpecified<TTable, TField>(CommandPreparingEvents parent) : 
    FieldEventsBase<PXCommandPreparingEventArgs, PXCommandPreparing>.GenericField<AbstractEvents.ICommandPreparing<TTable, TField>, System.Action<AbstractEvents.ICommandPreparing<TTable, TField>>>((FieldEventsBase<PXCommandPreparingEventArgs, PXCommandPreparing>) parent, typeof (TTable), typeof (TField).Name, (Func<System.Action<AbstractEvents.ICommandPreparing<TTable, TField>>, IGenericEventAdapter<PXCommandPreparingEventArgs, System.Action<AbstractEvents.ICommandPreparing<TTable, TField>>>>) (handler => (IGenericEventAdapter<PXCommandPreparingEventArgs, System.Action<AbstractEvents.ICommandPreparing<TTable, TField>>>) new EventsBase<PXCommandPreparingEventArgs, PXCommandPreparing, PXCache.EventDictionary<PXCommandPreparing>>.Generic<AbstractEvents.ICommandPreparing<TTable, TField>, System.Action<AbstractEvents.ICommandPreparing<TTable, TField>>>.AbstractAdapter<Events.CommandPreparing<TTable, TField>>(handler)), (Func<IGenericEventAdapter<PXCommandPreparingEventArgs, System.Action<AbstractEvents.ICommandPreparing<TTable, TField>>>, PXCommandPreparing>) (adapter => new PXCommandPreparing(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAbstractFieldAgnostic<TTable>(
    CommandPreparingEvents parent,
    string fieldName) : 
    FieldEventsBase<PXCommandPreparingEventArgs, PXCommandPreparing>.GenericField<AbstractEvents.ICommandPreparing<TTable, IBqlField>, System.Action<AbstractEvents.ICommandPreparing<TTable, IBqlField>>>((FieldEventsBase<PXCommandPreparingEventArgs, PXCommandPreparing>) parent, typeof (TTable), fieldName, (Func<System.Action<AbstractEvents.ICommandPreparing<TTable, IBqlField>>, IGenericEventAdapter<PXCommandPreparingEventArgs, System.Action<AbstractEvents.ICommandPreparing<TTable, IBqlField>>>>) (handler => (IGenericEventAdapter<PXCommandPreparingEventArgs, System.Action<AbstractEvents.ICommandPreparing<TTable, IBqlField>>>) new EventsBase<PXCommandPreparingEventArgs, PXCommandPreparing, PXCache.EventDictionary<PXCommandPreparing>>.Generic<AbstractEvents.ICommandPreparing<TTable, IBqlField>, System.Action<AbstractEvents.ICommandPreparing<TTable, IBqlField>>>.AbstractAdapter<Events.CommandPreparing<TTable, AbstractEvents.IFakeBqlField>>(handler)), (Func<IGenericEventAdapter<PXCommandPreparingEventArgs, System.Action<AbstractEvents.ICommandPreparing<TTable, IBqlField>>>, PXCommandPreparing>) (adapter => new PXCommandPreparing(adapter.Execute)))
    where TTable : class, IBqlTable, new()
  {
  }

  private class GenericAbstractFieldOnly<TField>(CommandPreparingEvents parent) : 
    FieldEventsBase<PXCommandPreparingEventArgs, PXCommandPreparing>.GenericField<AbstractEvents.ICommandPreparing<object, TField>, System.Action<AbstractEvents.ICommandPreparing<object, TField>>>((FieldEventsBase<PXCommandPreparingEventArgs, PXCommandPreparing>) parent, BqlCommand.GetItemType<TField>(), typeof (TField).Name, (Func<System.Action<AbstractEvents.ICommandPreparing<object, TField>>, IGenericEventAdapter<PXCommandPreparingEventArgs, System.Action<AbstractEvents.ICommandPreparing<object, TField>>>>) (handler => (IGenericEventAdapter<PXCommandPreparingEventArgs, System.Action<AbstractEvents.ICommandPreparing<object, TField>>>) new EventsBase<PXCommandPreparingEventArgs, PXCommandPreparing, PXCache.EventDictionary<PXCommandPreparing>>.Generic<AbstractEvents.ICommandPreparing<object, TField>, System.Action<AbstractEvents.ICommandPreparing<object, TField>>>.AbstractAdapter<Events.CommandPreparing<TField>>(handler)), (Func<IGenericEventAdapter<PXCommandPreparingEventArgs, System.Action<AbstractEvents.ICommandPreparing<object, TField>>>, PXCommandPreparing>) (adapter => new PXCommandPreparing(adapter.Execute)))
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAbstractFullAgnostic(
    CommandPreparingEvents parent,
    System.Type table,
    string fieldName) : 
    FieldEventsBase<PXCommandPreparingEventArgs, PXCommandPreparing>.GenericField<AbstractEvents.ICommandPreparing<object, IBqlField>, System.Action<AbstractEvents.ICommandPreparing<object, IBqlField>>>((FieldEventsBase<PXCommandPreparingEventArgs, PXCommandPreparing>) parent, table, fieldName, (Func<System.Action<AbstractEvents.ICommandPreparing<object, IBqlField>>, IGenericEventAdapter<PXCommandPreparingEventArgs, System.Action<AbstractEvents.ICommandPreparing<object, IBqlField>>>>) (handler => (IGenericEventAdapter<PXCommandPreparingEventArgs, System.Action<AbstractEvents.ICommandPreparing<object, IBqlField>>>) new EventsBase<PXCommandPreparingEventArgs, PXCommandPreparing, PXCache.EventDictionary<PXCommandPreparing>>.Generic<AbstractEvents.ICommandPreparing<object, IBqlField>, System.Action<AbstractEvents.ICommandPreparing<object, IBqlField>>>.AbstractAdapter<Events.CommandPreparing<IBqlField>>(handler)), (Func<IGenericEventAdapter<PXCommandPreparingEventArgs, System.Action<AbstractEvents.ICommandPreparing<object, IBqlField>>>, PXCommandPreparing>) (adapter => new PXCommandPreparing(adapter.Execute)))
  {
  }

  private class GenericAutoFullSpecified<TTable, TField>(CommandPreparingEvents parent) : 
    FieldEventsBase<PXCommandPreparingEventArgs, PXCommandPreparing>.GenericField<Events.CommandPreparing<TTable, TField>, Events.Event<PXCommandPreparingEventArgs, Events.CommandPreparing<TTable, TField>>.EventDelegate>((FieldEventsBase<PXCommandPreparingEventArgs, PXCommandPreparing>) parent, typeof (TTable), typeof (TField).Name, (Func<Events.Event<PXCommandPreparingEventArgs, Events.CommandPreparing<TTable, TField>>.EventDelegate, IGenericEventAdapter<PXCommandPreparingEventArgs, Events.Event<PXCommandPreparingEventArgs, Events.CommandPreparing<TTable, TField>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXCommandPreparingEventArgs, Events.Event<PXCommandPreparingEventArgs, Events.CommandPreparing<TTable, TField>>.EventDelegate>) new Events.Event<PXCommandPreparingEventArgs, Events.CommandPreparing<TTable, TField>>.Adapter(handler)), (Func<IGenericEventAdapter<PXCommandPreparingEventArgs, Events.Event<PXCommandPreparingEventArgs, Events.CommandPreparing<TTable, TField>>.EventDelegate>, PXCommandPreparing>) (adapter => new PXCommandPreparing(adapter.Execute)))
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlDataType>
  {
  }

  private class GenericAutoFieldOnly<TField>(CommandPreparingEvents parent) : 
    FieldEventsBase<PXCommandPreparingEventArgs, PXCommandPreparing>.GenericField<Events.CommandPreparing<TField>, Events.Event<PXCommandPreparingEventArgs, Events.CommandPreparing<TField>>.EventDelegate>((FieldEventsBase<PXCommandPreparingEventArgs, PXCommandPreparing>) parent, BqlCommand.GetItemType<TField>(), typeof (TField).Name, (Func<Events.Event<PXCommandPreparingEventArgs, Events.CommandPreparing<TField>>.EventDelegate, IGenericEventAdapter<PXCommandPreparingEventArgs, Events.Event<PXCommandPreparingEventArgs, Events.CommandPreparing<TField>>.EventDelegate>>) (handler => (IGenericEventAdapter<PXCommandPreparingEventArgs, Events.Event<PXCommandPreparingEventArgs, Events.CommandPreparing<TField>>.EventDelegate>) new Events.Event<PXCommandPreparingEventArgs, Events.CommandPreparing<TField>>.Adapter(handler)), (Func<IGenericEventAdapter<PXCommandPreparingEventArgs, Events.Event<PXCommandPreparingEventArgs, Events.CommandPreparing<TField>>.EventDelegate>, PXCommandPreparing>) (adapter => new PXCommandPreparing(adapter.Execute)))
    where TField : class, IBqlField
  {
  }
}
