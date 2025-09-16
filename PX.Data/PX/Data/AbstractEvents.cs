// Decompiled with JetBrains decompiler
// Type: PX.Data.AbstractEvents
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data;

/// <summary>
/// A class that provides the interfaces that are used as parameters in generic event handlers.
/// Unlike a handler with a parameter from the <see cref="T:PX.Data.Events" /> class, a handler with a parameter from this class <b>does not get automatically subscribed</b> to the corresponding event.
/// In a handler with a parameter from this class, you can abstract from the concrete source of the event, such as table, or field, since the information of the concrete source exists only on the subscription side.
/// </summary>
public class AbstractEvents
{
  /// <summary>Cancels an event.</summary>
  public static void Cancel(ICancelEventArgs e) => e.Cancel = true;

  internal interface IFakeBqlField : IBqlField, IBqlOperand
  {
  }

  internal abstract class AnonymousBqlFieldOf<TCSharpType> : 
    AbstractEvents.IFakeBqlField,
    IBqlField,
    IBqlOperand,
    IImplement<IBqlCorrespondsTo<TCSharpType>>
  {
  }

  /// <summary>
  /// An interface for the <tt>CommandPreparing</tt> event. The interface is used in generic event handlers.
  /// A handler with such a parameter <b>does not get automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.Events.CommandPreparing`2" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC or the abstraction over the DAC that raised the event.</typeparam>
  /// <typeparam name="TField">The field of the DAC that raised the event. Use <see cref="T:PX.Data.IBqlField" /> as a placeholder if the exact field is not relevant.</typeparam>
  public interface ICommandPreparing<out TTable, out TField> : 
    IGenericEventWith<PXCommandPreparingEventArgs>,
    ICancelEventArgs
    where TTable : class
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.Row" />
    TTable Row { get; }

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.Operation" />
    PXDBOperation Operation { get; }

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.Table" />
    System.Type Table { get; }

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.Value" />
    object? Value { get; set; }

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.DataType" />
    PXDbType DataType { get; set; }

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.DataLength" />
    int? DataLength { get; set; }

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.DataValue" />
    object? DataValue { get; set; }

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.IsRestriction" />
    bool IsRestriction { get; set; }
  }

  /// <summary>
  /// An interface for the <tt>ExceptionHandling</tt> event. The interface is used in generic event handlers.
  /// A handler with such a parameter <b>does not get automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.Events.ExceptionHandling`3" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC or the abstraction over the DAC that raised the event.</typeparam>
  /// <typeparam name="TField">The field of the DAC that raised the event. Use <see cref="T:PX.Data.IBqlField" /> as a placeholder if the exact field is not relevant.</typeparam>
  /// <typeparam name="TValue">The type of the field of the DAC that raised the event.</typeparam>
  public interface IExceptionHandling<out TTable, out TField, TValue> : 
    IGenericEventWith<PXExceptionHandlingEventArgs>,
    ICancelEventArgs
    where TTable : class
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXExceptionHandlingEventArgs.Row" />
    TTable Row { get; }

    /// <inheritdoc cref="P:PX.Data.PXExceptionHandlingEventArgs.Exception" />
    Exception Exception { get; }

    /// <inheritdoc cref="P:PX.Data.PXExceptionHandlingEventArgs.NewValue" />
    TValue? NewValue { get; set; }
  }

  internal class RelaxedExceptionHandling<TField, TValue> : 
    Events.Event<PXExceptionHandlingEventArgs, AbstractEvents.RelaxedExceptionHandling<TField, TValue>>,
    AbstractEvents.IExceptionHandling<object, TField, TValue>,
    IGenericEventWith<PXExceptionHandlingEventArgs>,
    ICancelEventArgs
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
    public object Row => this._args.Row;

    public Exception Exception => this._args.Exception;

    public TValue? NewValue
    {
      get => (TValue) this._args.NewValue;
      set => this._args.NewValue = (object) value;
    }

    public bool Cancel
    {
      get => this._args.Cancel;
      set => this._args.Cancel = value;
    }
  }

  /// <summary>
  /// An interface for the <tt>FieldDefaulting</tt> event. The interface is used in generic event handlers.
  /// A handler with such a parameter <b>does not get automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.Events.FieldDefaulting`3" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC or the abstraction over the DAC that raised the event.</typeparam>
  /// <typeparam name="TField">The field of the DAC that raised the event. Use <see cref="T:PX.Data.IBqlField" /> as a placeholder if the exact field is not relevant.</typeparam>
  /// <typeparam name="TValue">The type of the field of the DAC that raised the event.</typeparam>
  public interface IFieldDefaulting<out TTable, out TField, TValue> : 
    IGenericEventWith<PXFieldDefaultingEventArgs>,
    ICancelEventArgs
    where TTable : class
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldDefaultingEventArgs.Row" />
    TTable Row { get; }

    /// <inheritdoc cref="P:PX.Data.PXFieldDefaultingEventArgs.NewValue" />
    TValue? NewValue { get; set; }
  }

  internal class RelaxedFieldDefaulting<TField, TValue> : 
    Events.Event<PXFieldDefaultingEventArgs, AbstractEvents.RelaxedFieldDefaulting<TField, TValue>>,
    AbstractEvents.IFieldDefaulting<object, TField, TValue>,
    IGenericEventWith<PXFieldDefaultingEventArgs>,
    ICancelEventArgs
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
    public object Row => this._args.Row;

    public TValue? NewValue
    {
      get => (TValue) this._args.NewValue;
      set => this._args.NewValue = (object) value;
    }

    public bool Cancel
    {
      get => this._args.Cancel;
      set => this._args.Cancel = value;
    }
  }

  /// <summary>
  /// An interface for the <tt>FieldSelecting</tt> event. The interface is used in generic event handlers.
  /// A handler with such a parameter <b>does not get automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.Events.FieldSelecting`2" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC or the abstraction over the DAC that raised the event.</typeparam>
  /// <typeparam name="TField">The field of the DAC that raised the event. Use <see cref="T:PX.Data.IBqlField" /> as a placeholder if the exact field is not relevant.</typeparam>
  public interface IFieldSelecting<out TTable, out TField> : 
    IGenericEventWith<PXFieldSelectingEventArgs>,
    ICancelEventArgs
    where TTable : class
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldSelectingEventArgs.Row" />
    TTable? Row { get; }

    /// <inheritdoc cref="P:PX.Data.PXFieldSelectingEventArgs.IsAltered" />
    bool IsAltered { get; set; }

    /// <inheritdoc cref="P:PX.Data.PXFieldSelectingEventArgs.ReturnValue" />
    object? ReturnValue { get; set; }

    /// <inheritdoc cref="P:PX.Data.PXFieldSelectingEventArgs.ReturnState" />
    object? ReturnState { get; set; }
  }

  /// <summary>
  /// An interface for the <tt>FieldUpdated</tt> event. The interface is used in generic event handlers.
  /// A handler with such a parameter <b>does not get automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.Events.FieldUpdated`3" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC or the abstraction over the DAC that raised the event.</typeparam>
  /// <typeparam name="TField">The field of the DAC that raised the event. Use <see cref="T:PX.Data.IBqlField" /> as a placeholder if the exact field is not relevant.</typeparam>
  /// <typeparam name="TValue">The type of the field of the DAC that raised the event.</typeparam>
  public interface IFieldUpdated<out TTable, out TField, out TValue> : 
    IGenericEventWith<PXFieldUpdatedEventArgs>
    where TTable : class
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldUpdatedEventArgs.Row" />
    TTable Row { get; }

    /// <inheritdoc cref="P:PX.Data.PXFieldUpdatedEventArgs.ExternalCall" />
    bool ExternalCall { get; }

    /// <inheritdoc cref="P:PX.Data.PXFieldUpdatedEventArgs.OldValue" />
    TValue? OldValue { get; }
  }

  internal class RelaxedFieldUpdated<TField, TValue> : 
    Events.Event<PXFieldUpdatedEventArgs, AbstractEvents.RelaxedFieldUpdated<TField, TValue>>,
    AbstractEvents.IFieldUpdated<object, TField, TValue>,
    IGenericEventWith<PXFieldUpdatedEventArgs>
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
    public object Row => this._args.Row;

    public bool ExternalCall => this._args.ExternalCall;

    public TValue? OldValue => (TValue) this._args.OldValue;
  }

  /// <summary>
  /// An interface for the <tt>FieldUpdating</tt> event. The interface is used in generic event handlers.
  /// A handler with such a parameter <b>does not get automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.Events.FieldUpdating`2" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC or the abstraction over the DAC that raised the event.</typeparam>
  /// <typeparam name="TField">The field of the DAC that raised the event. Use <see cref="T:PX.Data.IBqlField" /> as a placeholder if the exact field is not relevant.</typeparam>
  public interface IFieldUpdating<out TTable, out TField> : 
    IGenericEventWith<PXFieldUpdatingEventArgs>,
    ICancelEventArgs
    where TTable : class
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldUpdatingEventArgs.Row" />
    TTable Row { get; }

    /// <inheritdoc cref="P:PX.Data.PXFieldUpdatingEventArgs.NewValue" />
    object? NewValue { get; set; }
  }

  /// <summary>
  /// An interface for the <tt>FieldVerifying</tt> event. The interface is used in generic event handlers.
  /// A handler with such a parameter <b>does not get automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.Events.FieldVerifying`3" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC or the abstraction over the DAC that raised the event.</typeparam>
  /// <typeparam name="TField">The field of the DAC that raised the event. Use <see cref="T:PX.Data.IBqlField" /> as a placeholder if the exact field is not relevant.</typeparam>
  /// <typeparam name="TValue">The type of the field of the DAC that raised the event.</typeparam>
  public interface IFieldVerifying<out TTable, out TField, TValue> : 
    IGenericEventWith<PXFieldVerifyingEventArgs>,
    ICancelEventArgs
    where TTable : class
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldVerifyingEventArgs.Row" />
    TTable Row { get; }

    /// <inheritdoc cref="P:PX.Data.PXFieldVerifyingEventArgs.NewValue" />
    TValue? NewValue { get; set; }

    /// <inheritdoc cref="P:PX.Data.PXFieldVerifyingEventArgs.ExternalCall" />
    bool ExternalCall { get; }
  }

  internal class RelaxedFieldVerifying<TField, TValue> : 
    Events.Event<PXFieldVerifyingEventArgs, AbstractEvents.RelaxedFieldVerifying<TField, TValue>>,
    AbstractEvents.IFieldVerifying<object, TField, TValue>,
    IGenericEventWith<PXFieldVerifyingEventArgs>,
    ICancelEventArgs
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
    public object Row => this._args.Row;

    public bool ExternalCall => this._args.ExternalCall;

    public TValue? NewValue
    {
      get => (TValue) this._args.NewValue;
      set => this._args.NewValue = (object) value;
    }

    public bool Cancel
    {
      get => this._args.Cancel;
      set => this._args.Cancel = value;
    }
  }

  /// <summary>
  /// An interface for the <tt>RowDeleted</tt> event. The interface is used in generic event handlers.
  /// A handler with such a parameter <b>does not get automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.Events.RowDeleted`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC or the abstraction over the DAC that raised the event.</typeparam>
  public interface IRowDeleted<out TTable> : IGenericEventWith<PXRowDeletedEventArgs> where TTable : class
  {
    /// <inheritdoc cref="P:PX.Data.PXRowDeletedEventArgs.Row" />
    TTable Row { get; }

    /// <inheritdoc cref="P:PX.Data.PXRowDeletedEventArgs.ExternalCall" />
    bool ExternalCall { get; }
  }

  internal class RelaxedRowDeleted : 
    Events.Event<PXRowDeletedEventArgs, AbstractEvents.RelaxedRowDeleted>,
    AbstractEvents.IRowDeleted<object>,
    IGenericEventWith<PXRowDeletedEventArgs>
  {
    public object Row => this._args.Row;

    public bool ExternalCall => this._args.ExternalCall;
  }

  /// <summary>
  /// An interface for the <tt>RowDeleting</tt> event. The interface is used in generic event handlers.
  /// A handler with such a parameter <b>does not get automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.Events.RowDeleting`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC or the abstraction over the DAC that raised the event.</typeparam>
  public interface IRowDeleting<out TTable> : 
    IGenericEventWith<PXRowDeletingEventArgs>,
    ICancelEventArgs
    where TTable : class
  {
    /// <inheritdoc cref="P:PX.Data.PXRowDeletingEventArgs.Row" />
    TTable Row { get; }

    /// <inheritdoc cref="P:PX.Data.PXRowDeletingEventArgs.ExternalCall" />
    bool ExternalCall { get; }
  }

  internal class RelaxedRowDeleting : 
    Events.Event<PXRowDeletingEventArgs, AbstractEvents.RelaxedRowDeleting>,
    AbstractEvents.IRowDeleting<object>,
    IGenericEventWith<PXRowDeletingEventArgs>,
    ICancelEventArgs
  {
    public object Row => this._args.Row;

    public bool ExternalCall => this._args.ExternalCall;

    public bool Cancel
    {
      get => this._args.Cancel;
      set => this._args.Cancel = value;
    }
  }

  /// <summary>
  /// An interface for the <tt>RowInserted</tt> event. The interface is used in generic event handlers.
  /// A handler with such a parameter <b>does not get automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.Events.RowInserted`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC or the abstraction over the DAC that raised the event.</typeparam>
  public interface IRowInserted<out TTable> : IGenericEventWith<PXRowInsertedEventArgs> where TTable : class
  {
    /// <inheritdoc cref="P:PX.Data.PXRowInsertedEventArgs.Row" />
    TTable Row { get; }

    /// <inheritdoc cref="P:PX.Data.PXRowInsertedEventArgs.ExternalCall" />
    bool ExternalCall { get; }
  }

  internal class RelaxedRowInserted : 
    Events.Event<PXRowInsertedEventArgs, AbstractEvents.RelaxedRowInserted>,
    AbstractEvents.IRowInserted<object>,
    IGenericEventWith<PXRowInsertedEventArgs>
  {
    public object Row => this._args.Row;

    public bool ExternalCall => this._args.ExternalCall;
  }

  /// <summary>
  /// An interface for the <tt>RowInserting</tt> event. The interface is used in generic event handlers.
  /// A handler with such a parameter <b>does not get automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.Events.RowInserting`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC or the abstraction over the DAC that raised the event.</typeparam>
  public interface IRowInserting<out TTable> : 
    IGenericEventWith<PXRowInsertingEventArgs>,
    ICancelEventArgs
    where TTable : class
  {
    /// <inheritdoc cref="P:PX.Data.PXRowInsertingEventArgs.Row" />
    TTable Row { get; }

    /// <inheritdoc cref="P:PX.Data.PXRowInsertingEventArgs.ExternalCall" />
    bool ExternalCall { get; }
  }

  internal class RelaxedRowInserting : 
    Events.Event<PXRowInsertingEventArgs, AbstractEvents.RelaxedRowInserting>,
    AbstractEvents.IRowInserting<object>,
    IGenericEventWith<PXRowInsertingEventArgs>,
    ICancelEventArgs
  {
    public object Row => this._args.Row;

    public bool ExternalCall => this._args.ExternalCall;

    public bool Cancel
    {
      get => this._args.Cancel;
      set => this._args.Cancel = value;
    }
  }

  /// <summary>
  /// An interface for the <tt>RowPersisted</tt> event. The interface is used in generic event handlers.
  /// A handler with such a parameter <b>does not get automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.Events.RowPersisted`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC or the abstraction over the DAC that raised the event.</typeparam>
  public interface IRowPersisted<out TTable> : IGenericEventWith<PXRowPersistedEventArgs> where TTable : class
  {
    /// <inheritdoc cref="P:PX.Data.PXRowPersistedEventArgs.Row" />
    TTable Row { get; }

    /// <inheritdoc cref="P:PX.Data.PXRowPersistedEventArgs.Operation" />
    PXDBOperation Operation { get; }

    /// <inheritdoc cref="P:PX.Data.PXRowPersistedEventArgs.TranStatus" />
    PXTranStatus TranStatus { get; }

    /// <inheritdoc cref="P:PX.Data.PXRowPersistedEventArgs.Exception" />
    Exception Exception { get; }
  }

  internal class RelaxedRowPersisted : 
    Events.Event<PXRowPersistedEventArgs, AbstractEvents.RelaxedRowPersisted>,
    AbstractEvents.IRowPersisted<object>,
    IGenericEventWith<PXRowPersistedEventArgs>
  {
    public object Row => this._args.Row;

    public PXDBOperation Operation => this._args.Operation;

    public PXTranStatus TranStatus => this._args.TranStatus;

    public Exception Exception => this._args.Exception;
  }

  /// <summary>
  /// An interface for the <tt>RowPersisting</tt> event. The interface is used in generic event handlers.
  /// A handler with such a parameter <b>does not get automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.Events.RowPersisting`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC or the abstraction over the DAC that raised the event.</typeparam>
  public interface IRowPersisting<out TTable> : 
    IGenericEventWith<PXRowPersistingEventArgs>,
    ICancelEventArgs
    where TTable : class
  {
    /// <inheritdoc cref="P:PX.Data.PXRowPersistingEventArgs.Row" />
    TTable Row { get; }

    /// <inheritdoc cref="P:PX.Data.PXRowPersistingEventArgs.Operation" />
    PXDBOperation Operation { get; }
  }

  internal class RelaxedRowPersisting : 
    Events.Event<PXRowPersistingEventArgs, AbstractEvents.RelaxedRowPersisting>,
    AbstractEvents.IRowPersisting<object>,
    IGenericEventWith<PXRowPersistingEventArgs>,
    ICancelEventArgs
  {
    public object Row => this._args.Row;

    public PXDBOperation Operation => this._args.Operation;

    public bool Cancel
    {
      get => this._args.Cancel;
      set => this._args.Cancel = value;
    }
  }

  /// <summary>
  /// An interface for the <tt>RowSelected</tt> event. The interface is used in generic event handlers.
  /// A handler with such a parameter <b>does not get automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.Events.RowSelected`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC or the abstraction over the DAC that raised the event.</typeparam>
  public interface IRowSelected<out TTable> : IGenericEventWith<PXRowSelectedEventArgs> where TTable : class
  {
    /// <inheritdoc cref="P:PX.Data.PXRowSelectedEventArgs.Row" />
    TTable? Row { get; }
  }

  internal class RelaxedRowSelected : 
    Events.Event<PXRowSelectedEventArgs, AbstractEvents.RelaxedRowSelected>,
    AbstractEvents.IRowSelected<object>,
    IGenericEventWith<PXRowSelectedEventArgs>
  {
    public object? Row => this._args.Row;
  }

  /// <summary>
  /// An interface for the <tt>RowSelecting</tt> event. The interface is used in generic event handlers.
  /// A handler with such a parameter <b>does not get automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.Events.RowSelecting`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC or the abstraction over the DAC that raised the event.</typeparam>
  public interface IRowSelecting<out TTable> : 
    IGenericEventWith<PXRowSelectingEventArgs>,
    ICancelEventArgs
    where TTable : class
  {
    /// <inheritdoc cref="P:PX.Data.PXRowSelectingEventArgs.Row" />
    TTable Row { get; }

    /// <inheritdoc cref="P:PX.Data.PXRowSelectingEventArgs.Record" />
    PXDataRecord Record { get; }

    /// <inheritdoc cref="P:PX.Data.PXRowSelectingEventArgs.Position" />
    int Position { get; set; }
  }

  internal class RelaxedRowSelecting : 
    Events.Event<PXRowSelectingEventArgs, AbstractEvents.RelaxedRowSelecting>,
    AbstractEvents.IRowSelecting<object>,
    IGenericEventWith<PXRowSelectingEventArgs>,
    ICancelEventArgs
  {
    public object Row => this._args.Row;

    public PXDataRecord Record => this._args.Record;

    public int Position
    {
      get => this._args.Position;
      set => this._args.Position = value;
    }

    public bool Cancel
    {
      get => this._args.Cancel;
      set => this._args.Cancel = value;
    }
  }

  /// <summary>
  /// An interface for the <tt>RowUpdated</tt> event. The interface is used in generic event handlers.
  /// A handler with such a parameter <b>does not get automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.Events.RowUpdated`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC or the abstraction over the DAC that raised the event.</typeparam>
  public interface IRowUpdated<out TTable> : IGenericEventWith<PXRowUpdatedEventArgs> where TTable : class
  {
    /// <inheritdoc cref="P:PX.Data.PXRowUpdatedEventArgs.Row" />
    TTable Row { get; }

    /// <inheritdoc cref="P:PX.Data.PXRowUpdatedEventArgs.OldRow" />
    TTable OldRow { get; }

    /// <inheritdoc cref="P:PX.Data.PXRowUpdatedEventArgs.ExternalCall" />
    bool ExternalCall { get; }
  }

  internal class RelaxedRowUpdated : 
    Events.Event<PXRowUpdatedEventArgs, AbstractEvents.RelaxedRowUpdated>,
    AbstractEvents.IRowUpdated<object>,
    IGenericEventWith<PXRowUpdatedEventArgs>
  {
    public object Row => this._args.Row;

    public object OldRow => this._args.OldRow;

    public bool ExternalCall => this._args.ExternalCall;
  }

  /// <summary>
  /// An interface for the <tt>RowUpdating</tt> event. The interface is used in generic event handlers.
  /// A handler with such a parameter <b>does not get automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.Events.RowUpdating`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC or the abstraction over the DAC that raised the event.</typeparam>
  public interface IRowUpdating<out TTable> : 
    IGenericEventWith<PXRowUpdatingEventArgs>,
    ICancelEventArgs
    where TTable : class
  {
    /// <inheritdoc cref="P:PX.Data.PXRowUpdatingEventArgs.Row" />
    TTable Row { get; }

    /// <inheritdoc cref="P:PX.Data.PXRowUpdatingEventArgs.NewRow" />
    TTable NewRow { get; }

    /// <inheritdoc cref="P:PX.Data.PXRowUpdatingEventArgs.ExternalCall" />
    bool ExternalCall { get; }
  }

  internal class RelaxedRowUpdating : 
    Events.Event<PXRowUpdatingEventArgs, AbstractEvents.RelaxedRowUpdating>,
    AbstractEvents.IRowUpdating<object>,
    IGenericEventWith<PXRowUpdatingEventArgs>,
    ICancelEventArgs
  {
    public object Row => this._args.Row;

    public object NewRow => this._args.NewRow;

    public bool ExternalCall => this._args.ExternalCall;

    public bool Cancel
    {
      get => this._args.Cancel;
      set => this._args.Cancel = value;
    }
  }
}
