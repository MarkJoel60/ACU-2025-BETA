// Decompiled with JetBrains decompiler
// Type: PX.Data.EventHandlerEntityRemapper`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable enable
namespace PX.Data;

/// <exclude />
internal class EventHandlerEntityRemapper<TMap, TClassicEventArgs>
  where TMap : PXMappedCacheExtension, new()
  where TClassicEventArgs : EventArgs
{
  private readonly EventHandlerEntityRemapper<
  #nullable disable
  TMap, TClassicEventArgs>.ClassicDelegate _classicHandler;

  public EventHandlerEntityRemapper(
    #nullable enable
    EventHandlerEntityRemapper<
    #nullable disable
    TMap, TClassicEventArgs>.ClassicDelegate classicHandler)
  {
    this._classicHandler = classicHandler;
  }

  public void Execute(
  #nullable enable
  PXCache c, PXCommandPreparingEventArgs args)
  {
    PXCache cach = c.Graph.Caches[typeof (TMap)];
    PXCommandPreparingEventArgs args1 = new PXCommandPreparingEventArgs((object) cach.GetExtension<TMap>(args.Row), args.Value, args.Operation, args.Table, args.SqlDialect);
    this._classicHandler(cach, (TClassicEventArgs) args1);
    args.Cancel = args1.Cancel;
  }

  public void Execute(PXCache c, PXExceptionHandlingEventArgs args)
  {
    PXCache cach = c.Graph.Caches[typeof (TMap)];
    PXExceptionHandlingEventArgs args1 = new PXExceptionHandlingEventArgs((object) cach.GetExtension<TMap>(args.Row), args.NewValue, args.Exception);
    this._classicHandler(cach, (TClassicEventArgs) args1);
    args.Cancel = args1.Cancel;
    args.NewValue = args1.NewValue;
  }

  public void Execute(PXCache c, PXFieldDefaultingEventArgs args)
  {
    PXCache cach = c.Graph.Caches[typeof (TMap)];
    PXFieldDefaultingEventArgs args1 = new PXFieldDefaultingEventArgs((object) cach.GetExtension<TMap>(args.Row));
    this._classicHandler(cach, (TClassicEventArgs) args1);
    args.Cancel = args1.Cancel;
    args.NewValue = args1.NewValue;
  }

  public void Execute(PXCache c, PXFieldSelectingEventArgs args)
  {
    PXCache cach = c.Graph.Caches[typeof (TMap)];
    PXFieldSelectingEventArgs args1 = new PXFieldSelectingEventArgs((object) cach.GetExtension<TMap>(args.Row), args.ReturnValue, args.IsAltered, args.ExternalCall);
    this._classicHandler(cach, (TClassicEventArgs) args1);
    args.ReturnValue = args1.ReturnValue;
    args.ReturnState = args1.ReturnState;
  }

  public void Execute(PXCache c, PXFieldUpdatedEventArgs args)
  {
    PXCache cach = c.Graph.Caches[typeof (TMap)];
    PXFieldUpdatedEventArgs args1 = new PXFieldUpdatedEventArgs((object) cach.GetExtension<TMap>(args.Row), args.OldValue, args.ExternalCall);
    this._classicHandler(cach, (TClassicEventArgs) args1);
  }

  public void Execute(PXCache c, PXFieldUpdatingEventArgs args)
  {
    PXCache cach = c.Graph.Caches[typeof (TMap)];
    PXFieldUpdatingEventArgs args1 = new PXFieldUpdatingEventArgs((object) cach.GetExtension<TMap>(args.Row), args.NewValue);
    this._classicHandler(cach, (TClassicEventArgs) args1);
    args.Cancel = args1.Cancel;
    args.NewValue = args1.NewValue;
  }

  public void Execute(PXCache c, PXFieldVerifyingEventArgs args)
  {
    PXCache cach = c.Graph.Caches[typeof (TMap)];
    PXFieldVerifyingEventArgs args1 = new PXFieldVerifyingEventArgs((object) cach.GetExtension<TMap>(args.Row), args.NewValue, args.ExternalCall);
    this._classicHandler(cach, (TClassicEventArgs) args1);
    args.Cancel = args1.Cancel;
    args.NewValue = args1.NewValue;
  }

  public void Execute(PXCache c, PXRowDeletedEventArgs args)
  {
    PXCache cach = c.Graph.Caches[typeof (TMap)];
    PXRowDeletedEventArgs args1 = new PXRowDeletedEventArgs((object) cach.GetExtension<TMap>(args.Row), args.ExternalCall);
    this._classicHandler(cach, (TClassicEventArgs) args1);
  }

  public void Execute(PXCache c, PXRowDeletingEventArgs args)
  {
    PXCache cach = c.Graph.Caches[typeof (TMap)];
    PXRowDeletingEventArgs args1 = new PXRowDeletingEventArgs((object) cach.GetExtension<TMap>(args.Row), args.ExternalCall);
    this._classicHandler(cach, (TClassicEventArgs) args1);
    args.Cancel = args1.Cancel;
  }

  public void Execute(PXCache c, PXRowInsertedEventArgs args)
  {
    PXCache cach = c.Graph.Caches[typeof (TMap)];
    PXRowInsertedEventArgs args1 = new PXRowInsertedEventArgs((object) cach.GetExtension<TMap>(args.Row), args.ExternalCall);
    this._classicHandler(cach, (TClassicEventArgs) args1);
  }

  public void Execute(PXCache c, PXRowInsertingEventArgs args)
  {
    PXCache cach = c.Graph.Caches[typeof (TMap)];
    PXRowInsertingEventArgs args1 = new PXRowInsertingEventArgs((object) cach.GetExtension<TMap>(args.Row), args.ExternalCall);
    this._classicHandler(cach, (TClassicEventArgs) args1);
    args.Cancel = args1.Cancel;
  }

  public void Execute(PXCache c, PXRowPersistedEventArgs args)
  {
    PXCache cach = c.Graph.Caches[typeof (TMap)];
    PXRowPersistedEventArgs args1 = new PXRowPersistedEventArgs((object) cach.GetExtension<TMap>(args.Row), args.Operation, args.TranStatus, args.Exception);
    this._classicHandler(cach, (TClassicEventArgs) args1);
  }

  public void Execute(PXCache c, PXRowPersistingEventArgs args)
  {
    PXCache cach = c.Graph.Caches[typeof (TMap)];
    PXRowPersistingEventArgs args1 = new PXRowPersistingEventArgs(args.Operation, (object) cach.GetExtension<TMap>(args.Row));
    this._classicHandler(cach, (TClassicEventArgs) args1);
    args.Cancel = args1.Cancel;
  }

  public void Execute(PXCache c, PXRowSelectedEventArgs args)
  {
    PXCache cach = c.Graph.Caches[typeof (TMap)];
    PXRowSelectedEventArgs args1 = new PXRowSelectedEventArgs((object) cach.GetExtension<TMap>(args.Row));
    this._classicHandler(cach, (TClassicEventArgs) args1);
  }

  public void Execute(PXCache c, PXRowSelectingEventArgs args)
  {
    PXCache cach = c.Graph.Caches[typeof (TMap)];
    PXRowSelectingEventArgs args1 = new PXRowSelectingEventArgs((object) cach.GetExtension<TMap>(args.Row), args.Record, args.Position, args.IsReadOnly);
    this._classicHandler(cach, (TClassicEventArgs) args1);
  }

  public void Execute(PXCache c, PXRowUpdatedEventArgs args)
  {
    PXCache cach = c.Graph.Caches[typeof (TMap)];
    PXRowUpdatedEventArgs args1 = new PXRowUpdatedEventArgs((object) cach.GetExtension<TMap>(args.Row), (object) cach.GetExtension<TMap>(args.OldRow), args.ExternalCall);
    this._classicHandler(cach, (TClassicEventArgs) args1);
  }

  public void Execute(PXCache c, PXRowUpdatingEventArgs args)
  {
    PXCache cach = c.Graph.Caches[typeof (TMap)];
    PXRowUpdatingEventArgs args1 = new PXRowUpdatingEventArgs((object) cach.GetExtension<TMap>(args.Row), (object) cach.GetExtension<TMap>(args.NewRow), args.ExternalCall);
    this._classicHandler(cach, (TClassicEventArgs) args1);
    args.Cancel = args1.Cancel;
  }

  /// <exclude />
  public delegate void ClassicDelegate(PXCache cache, TClassicEventArgs args)
    where TMap : 
    #nullable disable
    PXMappedCacheExtension, new()
    where TClassicEventArgs : EventArgs;
}
