// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDateAndTimeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using System;
using System.Globalization;

#nullable enable
namespace PX.Data;

/// <summary>Indicates a DAC field of <tt>DateTime?</tt> type that is not mapped to a database column and is represented in the user interface by two controls to input date
/// and time values separately.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field. The field is not bound to a table column.</remarks>
/// <example>
///   <code title="" description="" lang="CS">
/// [PXDateAndTime]
/// public virtual DateTime? StartDate { get; set; }</code>
/// </example>
public class PXDateAndTimeAttribute : PXDateAttribute
{
  /// <exclude />
  public const 
  #nullable disable
  string DATE_FIELD_POSTFIX = "_Date";
  /// <exclude />
  public const string TIME_FIELD_POSTFIX = "_Time";
  protected string _TimeInputMask = "t";
  protected string _TimeDisplayMask = "t";
  protected string _DateInputMask = "d";
  protected string _DateDisplayMask = "d";
  private bool? _isEnabledDate;
  private bool? _isEnabledTime;

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.Fields.Add(this._FieldName + "_Date");
    sender.Fields.Add(this._FieldName + "_Time");
    sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), this._FieldName + "_Date", new PXFieldSelecting(this.Date_FieldSelecting));
    sender.Graph.FieldUpdating.AddHandler(sender.GetItemType(), this._FieldName + "_Date", new PXFieldUpdating(this.Date_FieldUpdating));
    sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), this._FieldName + "_Time", new PXFieldSelecting(this.Time_FieldSelecting));
    sender.Graph.FieldUpdating.AddHandler(sender.GetItemType(), this._FieldName + "_Time", new PXFieldUpdating(this.Time_FieldUpdating));
  }

  /// <exclude />
  public void Date_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    object returnValue1 = e.ReturnValue = sender.GetValue(e.Row, this._FieldOrdinal);
    if (sender.HasAttributes(e.Row) || e.Row == null || e.IsAltered)
    {
      sender.RaiseFieldSelecting(this._FieldName, e.Row, ref returnValue1, true);
      PXFieldState instance = PXDateState.CreateInstance(returnValue1, this._FieldName + "_Date", new bool?(this._IsKey), new int?(), this._DateInputMask, this._DateDisplayMask, new System.DateTime?(), new System.DateTime?());
      PXDateAndTimeAttribute attribute = PXDateAndTimeAttribute.GetAttribute(sender, e.Row, this._FieldName);
      if (attribute != null && attribute._isEnabledDate.HasValue)
        instance.Enabled = attribute._isEnabledDate.Value;
      e.ReturnState = (object) instance;
    }
    if (e.ReturnValue == null)
      return;
    if (sender.Graph.IsMobile)
    {
      PXFieldSelectingEventArgs selectingEventArgs = e;
      int year = ((System.DateTime) e.ReturnValue).Year;
      System.DateTime returnValue2 = (System.DateTime) e.ReturnValue;
      int month = returnValue2.Month;
      returnValue2 = (System.DateTime) e.ReturnValue;
      int day = returnValue2.Day;
      returnValue2 = (System.DateTime) e.ReturnValue;
      int hour = returnValue2.Hour;
      returnValue2 = (System.DateTime) e.ReturnValue;
      int minute = returnValue2.Minute;
      // ISSUE: variable of a boxed type
      __Boxed<System.DateTime> local = (ValueType) new System.DateTime(year, month, day, hour, minute, 0);
      selectingEventArgs.ReturnValue = (object) local;
    }
    else
    {
      PXFieldSelectingEventArgs selectingEventArgs = e;
      int year = ((System.DateTime) e.ReturnValue).Year;
      System.DateTime returnValue3 = (System.DateTime) e.ReturnValue;
      int month = returnValue3.Month;
      returnValue3 = (System.DateTime) e.ReturnValue;
      int day = returnValue3.Day;
      // ISSUE: variable of a boxed type
      __Boxed<System.DateTime> local = (ValueType) new System.DateTime(year, month, day);
      selectingEventArgs.ReturnValue = (object) local;
    }
  }

  /// <exclude />
  public void Time_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    object returnValue1 = e.ReturnValue = sender.GetValue(e.Row, this._FieldOrdinal);
    if (sender.HasAttributes(e.Row) || e.Row == null || e.IsAltered)
    {
      sender.RaiseFieldSelecting(this._FieldName, e.Row, ref returnValue1, true);
      PXFieldState instance = PXDateState.CreateInstance(returnValue1, this._FieldName + "_Time", new bool?(this._IsKey), new int?(), this._TimeInputMask, this._TimeDisplayMask, new System.DateTime?(), new System.DateTime?());
      PXDateAndTimeAttribute attribute = PXDateAndTimeAttribute.GetAttribute(sender, e.Row, this._FieldName);
      if (attribute != null && attribute._isEnabledTime.HasValue)
        instance.Enabled = attribute._isEnabledTime.Value;
      e.ReturnState = (object) instance;
    }
    if (e.ReturnValue == null)
      return;
    if (sender.Graph.IsMobile)
    {
      PXFieldSelectingEventArgs selectingEventArgs = e;
      int year = ((System.DateTime) e.ReturnValue).Year;
      System.DateTime returnValue2 = (System.DateTime) e.ReturnValue;
      int month = returnValue2.Month;
      returnValue2 = (System.DateTime) e.ReturnValue;
      int day = returnValue2.Day;
      returnValue2 = (System.DateTime) e.ReturnValue;
      int hour = returnValue2.Hour;
      returnValue2 = (System.DateTime) e.ReturnValue;
      int minute = returnValue2.Minute;
      // ISSUE: variable of a boxed type
      __Boxed<System.DateTime> local = (ValueType) new System.DateTime(year, month, day, hour, minute, 0);
      selectingEventArgs.ReturnValue = (object) local;
    }
    else
      e.ReturnValue = (object) new System.DateTime(1900, 1, 1, ((System.DateTime) e.ReturnValue).Hour, ((System.DateTime) e.ReturnValue).Minute, 0);
  }

  /// <exclude />
  public virtual void Date_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue is string)
    {
      System.DateTime? nullable = (System.DateTime?) sender.GetValue(e.Row, this._FieldOrdinal);
      System.DateTime result;
      if (System.DateTime.TryParse((string) e.NewValue, (IFormatProvider) sender.Graph.Culture, DateTimeStyles.None, out result))
      {
        object obj = (object) (sender.Graph.IsMobile ? new System.DateTime?(result) : PXDateAndTimeAttribute.CombineDateTime(new System.DateTime?(result), nullable));
        sender.SetValue(e.Row, this._FieldOrdinal, obj);
        if (sender.GetValuePending(e.Row, this._FieldName + "_Time") == null)
          return;
        sender.RaiseFieldUpdated(this._FieldName, e.Row, (object) nullable);
      }
      else
        e.NewValue = (object) null;
    }
    else
    {
      if (!(e.NewValue is System.DateTime))
        return;
      System.DateTime? nullable = (System.DateTime?) sender.GetValue(e.Row, this._FieldOrdinal);
      object obj = sender.Graph.IsMobile ? e.NewValue : (object) PXDateAndTimeAttribute.CombineDateTime(new System.DateTime?((System.DateTime) e.NewValue), nullable);
      sender.SetValue(e.Row, this._FieldOrdinal, obj);
      if (sender.GetValuePending(e.Row, this._FieldName + "_Time") == null)
        return;
      sender.RaiseFieldUpdated(this._FieldName, e.Row, (object) nullable);
    }
  }

  /// <exclude />
  public void Time_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue is string)
    {
      System.DateTime? date = (System.DateTime?) sender.GetValue(e.Row, this._FieldOrdinal);
      System.DateTime result;
      if (date.HasValue && System.DateTime.TryParse((string) e.NewValue, (IFormatProvider) sender.Graph.Culture, DateTimeStyles.None, out result))
      {
        System.DateTime? nullable = sender.Graph.IsMobile ? new System.DateTime?(result) : PXDateAndTimeAttribute.CombineDateTime(date, new System.DateTime?(result));
        sender.SetValueExt(e.Row, this._FieldName, (object) nullable);
      }
      else
        e.NewValue = (object) null;
    }
    else
    {
      if (!(e.NewValue is System.DateTime))
        return;
      System.DateTime? date = (System.DateTime?) sender.GetValue(e.Row, this._FieldOrdinal);
      System.DateTime? nullable = sender.Graph.IsMobile ? new System.DateTime?((System.DateTime) e.NewValue) : PXDateAndTimeAttribute.CombineDateTime(date, new System.DateTime?((System.DateTime) e.NewValue));
      sender.SetValueExt(e.Row, this._FieldName, (object) nullable);
    }
  }

  /// <summary>
  /// Combines the provided data and time values in a single <tt>DateTime</tt>
  /// object.
  /// </summary>
  /// <param name="date">The date value.</param>
  /// <param name="time">The time value.</param>
  /// <returns>The combined <tt>DateTime</tt> value or <tt>null</tt> if the
  /// <tt>date</tt> value is <tt>null</tt>.</returns>
  /// <example>
  /// <code>
  /// ...
  /// return PXDateAndTimeAttribute.CombineDateTime(date, new DateTime(2008, 1, 1, 9, 0, 0));
  /// </code>
  /// </example>
  public static System.DateTime? CombineDateTime(System.DateTime? date, System.DateTime? time)
  {
    if (!date.HasValue)
      return new System.DateTime?();
    if (time.HasValue)
    {
      int year = date.Value.Year;
      System.DateTime dateTime = date.Value;
      int month = dateTime.Month;
      dateTime = date.Value;
      int day = dateTime.Day;
      dateTime = time.Value;
      int hour = dateTime.Hour;
      dateTime = time.Value;
      int minute = dateTime.Minute;
      return new System.DateTime?(new System.DateTime(year, month, day, hour, minute, 0));
    }
    int year1 = date.Value.Year;
    System.DateTime dateTime1 = date.Value;
    int month1 = dateTime1.Month;
    dateTime1 = date.Value;
    int day1 = dateTime1.Day;
    return new System.DateTime?(new System.DateTime(year1, month1, day1));
  }

  /// <summary>Sets the <see cref="!:Enabled" /> property that represents the
  /// date part of the field value. The field is specified as the type
  /// parameter.</summary>
  /// <param name="cache">The cache object to search for
  /// <tt>PXDateAndTime</tt> attributes.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="isEnabled">The value indicating whether the input control
  /// is enabled.</param>
  /// <typeparam name="Field">The field the attribute is attached to.</typeparam>
  public static void SetDateEnabled<Field>(PXCache cache, object data, bool isEnabled) where Field : IBqlField
  {
    PXDateAndTimeAttribute.SetDateEnabled(cache, data, cache.GetField(typeof (Field)), isEnabled);
  }

  /// <summary>Sets the <see cref="!:Enabled" /> property that represents the
  /// date part of the field value.</summary>
  /// <param name="cache">The cache object to search for
  /// <tt>PXDateAndTime</tt> attributes.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The name of the field the attribute is attached
  /// to.</param>
  /// <param name="isEnabled">The value indicating whether the input control
  /// is enabled.</param>
  public static void SetDateEnabled(PXCache cache, object data, string name, bool isEnabled)
  {
    PXDateAndTimeAttribute attribute = PXDateAndTimeAttribute.GetAttribute(cache, data, name);
    if (attribute == null)
      return;
    attribute._isEnabledDate = new bool?(isEnabled);
  }

  /// <summary>Sets the <see cref="!:Enabled" /> property that represents the
  /// time part of the field value. The field is specified as the type
  /// parameter.</summary>
  /// <param name="cache">The cache object to search for
  /// <tt>PXDateAndTime</tt> attributes.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="isEnabled">The value indicating whether the input control
  /// is enabled.</param>
  /// <typeparam name="Field">The field the attribute is attached to.</typeparam>
  public static void SetTimeEnabled<Field>(PXCache cache, object data, bool isEnabled) where Field : IBqlField
  {
    PXDateAndTimeAttribute.SetTimeEnabled(cache, data, cache.GetField(typeof (Field)), isEnabled);
  }

  /// <summary>Sets the <see cref="!:Enabled" /> property that represents the
  /// time part of the field value.</summary>
  /// <param name="cache">The cache object to search for
  /// <tt>PXDateAndTime</tt> attributes.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The name of the field the attribute is attached
  /// to.</param>
  /// <param name="isEnabled">The value indicating whether the input control
  /// is enabled.</param>
  public static void SetTimeEnabled(PXCache cache, object data, string name, bool isEnabled)
  {
    PXDateAndTimeAttribute attribute = PXDateAndTimeAttribute.GetAttribute(cache, data, name);
    if (attribute == null)
      return;
    attribute._isEnabledTime = new bool?(isEnabled);
  }

  /// <exclude />
  public static PXDateAndTimeAttribute GetAttribute(PXCache cache, object data, string name)
  {
    foreach (PXEventSubscriberAttribute attribute1 in cache.GetAttributes(data, name))
    {
      if (attribute1 is PXDateAndTimeAttribute attribute2)
        return attribute2;
    }
    return (PXDateAndTimeAttribute) null;
  }

  /// <summary>Represents the current local date and time in BQL.</summary>
  public class now : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Constant<
  #nullable disable
  PXDateAndTimeAttribute.now>
  {
    public now()
      : base(System.DateTime.MinValue)
    {
    }

    public override System.DateTime Value => PXTimeZoneInfo.Now;
  }
}
