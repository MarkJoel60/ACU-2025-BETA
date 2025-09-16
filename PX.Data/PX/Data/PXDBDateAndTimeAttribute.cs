// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBDateAndTimeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using PX.Translation;
using System;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field of <tt>DateTime?</tt> type to the database
/// column of <tt>datetime</tt> or <tt>smalldatetime</tt> type. Defines
/// the DAC field that is represented in the UI by two input controls: one
/// for date, the other for time.</summary>
/// <remarks>
/// <para>The attribute is added to the value declaration of a DAC field.
/// The field becomes bound to the database column with the same
/// name.</para>
/// <para>Unlike the <see cref="T:PX.Data.PXDBDateAttribute">PXDBDate</see>
/// attribute, this attribute defines the field that is represented in the
/// UI by two input controls to specify date and time values
/// separately.</para>
/// </remarks>
/// <example>
/// <code>
/// [PXDBDateAndTime]
/// [PXUIField(DisplayName = "Start Time")]
/// public virtual DateTime? StartDate { get; set; }
/// </code>
/// </example>
public class PXDBDateAndTimeAttribute : PXDBDateAttribute
{
  /// <exclude />
  public const string DATE_FIELD_POSTFIX = "_Date";
  /// <exclude />
  public const string TIME_FIELD_POSTFIX = "_Time";
  protected string _TimeInputMask = "t";
  protected string _TimeDisplayMask = "t";
  protected string _DateInputMask = "d";
  protected string _DateDisplayMask = "d";
  private string _displayNameDate;
  private string _displayNameTime;
  private bool _isEnabledDate = true;
  private bool _isEnabledTime = true;
  private bool _isVisibleDate = true;
  private bool _isVisibleTime = true;
  private string _neutralDisplayNameDate;
  private string _neutralDisplayNameTime;

  /// <summary>
  /// Gets the localized additional label for the date part
  /// of the field.
  /// </summary>
  public string DateDisplayNamePostfix
  {
    get => $" ({PXLocalizer.Localize("Date", typeof (PX.SM.Messages).FullName)})";
  }

  /// <summary>
  /// Gets the localized additional label for the time part
  /// of the field.
  /// </summary>
  public string TimeDisplayNamePostfix
  {
    get => $" ({PXLocalizer.Localize("Time", typeof (PX.SM.Messages).FullName)})";
  }

  /// <summary>Initializes a new instance of the attribute with default
  /// parameters.</summary>
  public PXDBDateAndTimeAttribute() => this.PreserveTime = true;

  /// <summary>Gets or sets the value that indicates whether the display
  /// names of the input controls for date and time are appended with
  /// <i>(Date)</i> and <i>(Time)</i>, respectively.</summary>
  public virtual bool WithoutDisplayNames { get; set; }

  /// <summary>Gets or sets the display name for the input control that
  /// represents date.</summary>
  public string DisplayNameDate
  {
    get => this._displayNameDate;
    set => this._displayNameDate = value;
  }

  public string NeutralDisplayNameDate
  {
    get
    {
      if (this._neutralDisplayNameDate == null && this._displayNameDate != null)
        this._neutralDisplayNameDate = this._displayNameDate;
      return this._neutralDisplayNameDate;
    }
  }

  /// <summary>Gets or sets the display name for the input control that
  /// represents time.</summary>
  public string DisplayNameTime
  {
    get => this._displayNameTime;
    set => this._displayNameTime = value;
  }

  public string NeutralDisplayNameTime
  {
    get
    {
      if (this._neutralDisplayNameTime == null && this._displayNameTime != null)
        this._neutralDisplayNameTime = this._displayNameTime;
      return this._neutralDisplayNameTime;
    }
  }

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
    this.TryLocalize(sender);
  }

  protected virtual void TryLocalize(PXCache sender)
  {
    if (ResourceCollectingManager.IsStringCollecting)
      PXPageRipper.RipDateAndTime(this, sender, CollectResourceSettings.Resource);
    else
      PXLocalizerRepository.DateTimeLocalizer.Localize(this, sender);
  }

  /// <exclude />
  public void Date_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    object returnValue1 = e.ReturnValue = sender.GetValue(e.Row, this._FieldOrdinal);
    if (sender.HasAttributes(e.Row) || e.Row == null || e.IsAltered)
    {
      sender.RaiseFieldSelecting(this._FieldName, e.Row, ref returnValue1, true);
      PXFieldState instance = PXDateState.CreateInstance(returnValue1, this._FieldName + "_Date", new bool?(this._IsKey), new int?(), this._DateInputMask, this._DateDisplayMask, new System.DateTime?(), new System.DateTime?());
      if (!this.WithoutDisplayNames)
        instance.DisplayName += this.DateDisplayNamePostfix;
      foreach (PXDBDateAndTimeAttribute andTimeAttribute in PXDBDateAttribute.GetAttribute(sender, e.Row, this._FieldName))
      {
        if (andTimeAttribute != null)
        {
          if (andTimeAttribute._displayNameDate != null)
            instance.DisplayName = PXLocalizer.Localize(andTimeAttribute._displayNameDate, sender.BqlTable.FullName);
          instance.Enabled = instance.Enabled && andTimeAttribute._isEnabledDate;
          instance.Visible = instance.Visible && andTimeAttribute._isVisibleDate;
        }
      }
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
  public virtual void Time_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    object returnValue1 = e.ReturnValue = sender.GetValue(e.Row, this._FieldOrdinal);
    if (sender.HasAttributes(e.Row) || e.Row == null || e.IsAltered)
    {
      sender.RaiseFieldSelecting(this._FieldName, e.Row, ref returnValue1, true);
      PXFieldState instance = PXDateState.CreateInstance(returnValue1, this._FieldName + "_Time", new bool?(this._IsKey), new int?(), this._TimeInputMask, this._TimeDisplayMask, new System.DateTime?(), new System.DateTime?());
      if (!this.WithoutDisplayNames)
        instance.DisplayName += this.TimeDisplayNamePostfix;
      foreach (PXDBDateAndTimeAttribute andTimeAttribute in PXDBDateAttribute.GetAttribute(sender, e.Row, this._FieldName))
      {
        if (andTimeAttribute != null)
        {
          if (andTimeAttribute._displayNameTime != null)
            instance.DisplayName = PXLocalizer.Localize(andTimeAttribute._displayNameTime, sender.BqlTable.FullName);
          instance.Enabled = instance.Enabled && andTimeAttribute._isEnabledTime;
          instance.Visible = instance.Visible && andTimeAttribute._isVisibleTime && this._PreserveTime;
        }
      }
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
    if (e.Cancel)
      return;
    if (e.NewValue is string)
    {
      System.DateTime? nullable = (System.DateTime?) sender.GetValue(e.Row, this._FieldOrdinal);
      System.DateTime result;
      if (System.DateTime.TryParse((string) e.NewValue, (IFormatProvider) sender.Graph.Culture, DateTimeStyles.None, out result))
      {
        object obj = (object) (sender.Graph.IsMobile ? new System.DateTime?(result) : PXDBDateAndTimeAttribute.CombineDateTime(new System.DateTime?(result), nullable));
        sender.SetValue(e.Row, this._FieldOrdinal, obj);
        sender.RaiseFieldUpdated(this._FieldName + "_Date", e.Row, (object) nullable);
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
      object newValue = sender.Graph.IsMobile ? e.NewValue : (object) PXDBDateAndTimeAttribute.CombineDateTime(new System.DateTime?((System.DateTime) e.NewValue), nullable);
      if (!sender.RaiseFieldVerifying(this.FieldName + "_Date", e.Row, ref newValue))
        return;
      sender.SetValue(e.Row, this._FieldOrdinal, newValue);
      sender.RaiseFieldUpdated(this._FieldName + "_Date", e.Row, (object) nullable);
      if (sender.GetValuePending(e.Row, this._FieldName + "_Time") == null)
        return;
      sender.RaiseFieldUpdated(this._FieldName, e.Row, (object) nullable);
    }
  }

  /// <exclude />
  public void Time_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.Cancel)
      return;
    if (e.NewValue is string)
    {
      System.DateTime? date = (System.DateTime?) sender.GetValue(e.Row, this._FieldOrdinal);
      System.DateTime result;
      if (date.HasValue && System.DateTime.TryParse((string) e.NewValue, (IFormatProvider) sender.Graph.Culture, DateTimeStyles.None, out result))
      {
        date = sender.Graph.IsMobile ? new System.DateTime?(result) : PXDBDateAndTimeAttribute.CombineDateTime(date, new System.DateTime?(result));
        sender.SetValueExt(e.Row, this._FieldName, (object) date);
      }
      else
        e.NewValue = (object) null;
    }
    else
    {
      if (!(e.NewValue is System.DateTime))
        return;
      System.DateTime? date = (System.DateTime?) sender.GetValue(e.Row, this._FieldOrdinal);
      System.DateTime? nullable = sender.Graph.IsMobile ? new System.DateTime?((System.DateTime) e.NewValue) : PXDBDateAndTimeAttribute.CombineDateTime(date, new System.DateTime?((System.DateTime) e.NewValue));
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
  /// switch (date.Value.DayOfWeek)
  /// {
  ///    case DayOfWeek.Monday: return PXDBDateAndTimeAttribute.CombineDateTime(date, cal.MonStartTime);
  ///    case DayOfWeek.Tuesday: return PXDBDateAndTimeAttribute.CombineDateTime(date, cal.TueStartTime);
  ///    case DayOfWeek.Wednesday: return PXDBDateAndTimeAttribute.CombineDateTime(date, cal.WedStartTime);
  ///    case DayOfWeek.Thursday: return PXDBDateAndTimeAttribute.CombineDateTime(date, cal.ThuStartTime);
  ///    case DayOfWeek.Friday: return PXDBDateAndTimeAttribute.CombineDateTime(date, cal.FriStartTime);
  ///    case DayOfWeek.Saturday: return PXDBDateAndTimeAttribute.CombineDateTime(date, cal.SatStartTime);
  ///    case DayOfWeek.Sunday: return PXDBDateAndTimeAttribute.CombineDateTime(date, cal.SunStartTime);
  /// }
  /// ...
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

  /// <summary>Enables or disables the input control that represents the
  /// date part of the field value. The field is specified as the type
  /// parameter.</summary>
  /// <param name="cache">The cache object to search for
  /// <tt>PXDBDateAndTime</tt> attributes.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="isEnabled">The value indicating whether the input control
  /// is enabled.</param>
  public static void SetDateEnabled<Field>(PXCache cache, object data, bool isEnabled) where Field : IBqlField
  {
    PXDBDateAndTimeAttribute.SetDateEnabled(cache, data, cache.GetField(typeof (Field)), isEnabled);
  }

  /// <summary>Enables or disables the input control that represents the
  /// date part of the field value.</summary>
  /// <param name="cache">The cache object to search for
  /// <tt>PXDBDateAndTime</tt> attributes.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The name of the field the attribute is attached
  /// to.</param>
  /// <param name="isEnabled">The value indicating whether the input control
  /// is enabled.</param>
  public static void SetDateEnabled(PXCache cache, object data, string name, bool isEnabled)
  {
    foreach (PXDBDateAndTimeAttribute andTimeAttribute in PXDBDateAttribute.GetAttribute(cache, data, name))
    {
      if (andTimeAttribute != null)
        andTimeAttribute._isEnabledDate = isEnabled;
    }
  }

  /// <summary>Enables or disables the input control that represents the
  /// time part of the field value. The field is specified as the type
  /// parameter.</summary>
  /// <param name="cache">The cache object to search for
  /// <tt>PXDBDateAndTime</tt> attributes.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="isEnabled">The value indicating whether the input control
  /// is enabled.</param>
  public static void SetTimeEnabled<Field>(PXCache cache, object data, bool isEnabled) where Field : IBqlField
  {
    PXDBDateAndTimeAttribute.SetTimeEnabled(cache, data, cache.GetField(typeof (Field)), isEnabled);
  }

  /// <summary>Enables or disables the input control that represents the
  /// time part of the field value.</summary>
  /// <param name="cache">The cache object to search for
  /// <tt>PXDBDateAndTime</tt> attributes.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The name of the field the attribute is attached
  /// to.</param>
  /// <param name="isEnabled">The value indicating whether the input control
  /// is enabled.</param>
  public static void SetTimeEnabled(PXCache cache, object data, string name, bool isEnabled)
  {
    foreach (PXDBDateAndTimeAttribute andTimeAttribute in PXDBDateAttribute.GetAttribute(cache, data, name))
    {
      if (andTimeAttribute != null)
        andTimeAttribute._isEnabledTime = isEnabled;
    }
  }

  /// <summary>Sets the display name of the input control that represents
  /// the date part of the field value. The field is specified as the type
  /// parameter.</summary>
  /// <typeparam name="Field">The field.</typeparam>
  /// <param name="cache">The cache object to search for
  /// <tt>PXDBDateAndTime</tt> attributes.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="displayName">The string to set as the display
  /// name.</param>
  /// <example>
  /// The code below shows the constructor of the <tt>CRTaskMaint</tt> graph,
  /// in which you set new display names for the input control representing
  /// the date values of <tt>StartDate</tt> and <tt>EndDate</tt>.
  /// <code>
  /// public CRTaskMaint()
  ///     : base()
  /// {
  ///     ...
  ///     PXDBDateAndTimeAttribute.SetDateDisplayName&lt;EPActivity.startDate&gt;(Tasks.Cache, null, "Start Date");
  ///     PXDBDateAndTimeAttribute.SetDateDisplayName&lt;EPActivity.endDate&gt;(Tasks.Cache, null, "Due Date");
  /// }
  /// </code>
  /// </example>
  public static void SetDateDisplayName<Field>(PXCache cache, object data, string displayName) where Field : IBqlField
  {
    PXDBDateAndTimeAttribute.SetDateDisplayName(cache, data, cache.GetField(typeof (Field)), displayName);
  }

  /// <summary>Sets the display name of the input control that represents
  /// the date part of the field value.</summary>
  /// <param name="cache">The cache object to search for
  /// <tt>PXDBDateAndTime</tt> attributes.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The name of the field the attribute is attached
  /// to.</param>
  /// <param name="displayName">The string to set as the display
  /// name.</param>
  /// <seealso cref="M:PX.Data.PXDBDateAndTimeAttribute.SetDateDisplayName``1(PX.Data.PXCache,System.Object,System.String)" />
  /// <example>
  /// <code>
  /// PXDBDateAndTimeAttribute.SetDateDisplayName(Tasks.Cache, null, "StartDate", "Start Date");
  /// </code>
  /// </example>
  public static void SetDateDisplayName(
    PXCache cache,
    object data,
    string name,
    string displayName)
  {
    foreach (PXDBDateAndTimeAttribute andTimeAttribute in PXDBDateAttribute.GetAttribute(cache, data, name))
    {
      if (andTimeAttribute != null)
        andTimeAttribute._displayNameDate = displayName;
    }
  }

  /// <summary>Sets the display name of the input control that represents
  /// the time part of the field value. The field is specified as the type
  /// parameter.</summary>
  /// <param name="cache">The cache object to search for
  /// <tt>PXDBDateAndTime</tt> attributes.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="displayName">The string to set as the display
  /// name.</param>
  public static void SetTimeDisplayName<Field>(PXCache cache, object data, string displayName) where Field : IBqlField
  {
    PXDBDateAndTimeAttribute.SetTimeDisplayName(cache, data, cache.GetField(typeof (Field)), displayName);
  }

  /// <summary>Sets the display name of the input control that represents
  /// the time part of the field value.</summary>
  /// <param name="cache">The cache object to search for
  /// <tt>PXDBDateAndTime</tt> attributes.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The name of the field the attribute is attached
  /// to.</param>
  /// <param name="displayName">The string to set as the display
  /// name.</param>
  public static void SetTimeDisplayName(
    PXCache cache,
    object data,
    string name,
    string displayName)
  {
    foreach (PXDBDateAndTimeAttribute andTimeAttribute in PXDBDateAttribute.GetAttribute(cache, data, name))
    {
      if (andTimeAttribute != null)
        andTimeAttribute._displayNameTime = displayName;
    }
  }

  /// <summary>Makes visible or hides the input control that represents the
  /// data part of the field value. The field is specified as the type
  /// parameter.</summary>
  /// <param name="cache">The cache object to search for
  /// <tt>PXDBDateAndTime</tt> attributes.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="isVisible">The value indicating whether the input control
  /// is visible on the user interface.</param>
  public static void SetDateVisible<Field>(PXCache cache, object data, bool isVisible) where Field : IBqlField
  {
    PXDBDateAndTimeAttribute.SetDateVisible(cache, data, cache.GetField(typeof (Field)), isVisible);
  }

  /// <summary>Makes visible or hides the input control that represents the
  /// data part of the field value.</summary>
  /// <param name="cache">The cache object to search for
  /// <tt>PXDBDateAndTime</tt> attributes.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The name of the field the attribute is attached
  /// to.</param>
  /// <param name="isVisible">The value indicating whether the input control
  /// is visible on the user interface.</param>
  public static void SetDateVisible(PXCache cache, object data, string name, bool isVisible)
  {
    foreach (PXDBDateAndTimeAttribute andTimeAttribute in PXDBDateAttribute.GetAttribute(cache, data, name))
    {
      if (andTimeAttribute != null)
        andTimeAttribute._isVisibleDate = isVisible;
    }
  }

  /// <summary>Makes visible or hides the input control that represents the
  /// data part of the field value. The field is specified as the type
  /// parameter.</summary>
  /// <param name="cache">The cache object to search for
  /// <tt>PXDBDateAndTime</tt> attributes.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="isVisible">The value indicating whether the input control
  /// is visible on the user interface.</param>
  /// <example>
  /// The code below shows the <tt>RowSelected</tt> event handler, in which you
  /// configure the visibility of the input controls that represent time value
  /// of the <tt>StartDate</tt> and <tt>EndDate</tt> fields.
  /// <code>
  /// protected virtual void EPActivity_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  /// {
  ///     ...
  ///     var showMinutes = EPSetupCurrent.RequireTimes == true;
  ///     PXDBDateAndTimeAttribute.SetTimeVisible&lt;EPActivity.startDate&gt;(cache, row, true);
  ///     PXDBDateAndTimeAttribute.SetTimeVisible&lt;EPActivity.endDate&gt;(cache, row, showMinutes &amp;&amp; row.TrackTime == true);
  ///     ...
  /// }
  /// </code>
  /// </example>
  public static void SetTimeVisible<Field>(PXCache cache, object data, bool isVisible) where Field : IBqlField
  {
    PXDBDateAndTimeAttribute.SetTimeVisible(cache, data, cache.GetField(typeof (Field)), isVisible);
  }

  /// <summary>Makes visible or hides the input control that represents the
  /// time part of the field value.</summary>
  /// <param name="cache">The cache object to search for
  /// <tt>PXDBDateAndTime</tt> attributes.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The name of the field the attribute is attached
  /// to.</param>
  /// <param name="isVisible">The value indicating whether the input control
  /// is visible on the user interface.</param>
  /// <seealso cref="M:PX.Data.PXDBDateAndTimeAttribute.SetTimeVisible``1(PX.Data.PXCache,System.Object,System.Boolean)" />
  /// <example>
  /// <code>
  /// PXDBDateAndTimeAttribute.SetTimeVisible(cache, row, "StartDate", true);
  /// </code>
  /// </example>
  public static void SetTimeVisible(PXCache cache, object data, string name, bool isVisible)
  {
    foreach (PXDBDateAndTimeAttribute andTimeAttribute in PXDBDateAttribute.GetAttribute(cache, data, name))
    {
      if (andTimeAttribute != null)
        andTimeAttribute._isVisibleTime = isVisible;
    }
  }
}
