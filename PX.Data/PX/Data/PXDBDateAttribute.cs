// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBDateAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field of <tt>DateTime?</tt> type to the database column of <tt>datetime</tt> or <tt>smalldatetime</tt> type, depending on the
/// <tt>UseSmallDateTime</tt> flag.</summary>
/// <remarks>
///   <para>The attribute is added to the value declaration of a DAC field. The field becomes bound to the database column with the same name.</para>
///   <para>The attribute defines a field represented by a single input control in the user interface.</para>
/// </remarks>
/// <example><para>The attribute below binds the field to the database column and sets the minimum and maximum values for a field value.</para>
/// <code title="Example" lang="CS">
/// [PXDBDate(MinValue = "01/01/1900")]
/// public virtual DateTime? OrderDate { get; set; }</code>
/// <code title="Example2" description="The attribute below binds the field to the database column and sets the input and display masks. A field value will be displayed using the long date pattern. That is, for en-US culture the 6/15/2009 1:45:30 PM value will be converted to Monday, June 15, 2009." groupname="Example" lang="CS">
/// [PXDBDate(InputMask = "d", DisplayMask = "d")]
/// public virtual DateTime? StartDate { get; set; }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBDateAttribute : 
  PXDBFieldAttribute,
  IPXRowSelectingSubscriber,
  IPXCommandPreparingSubscriber,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber
{
  protected static readonly System.DateTime _MIN_VALUE = new System.DateTime(1900, 1, 1);
  protected string _InputMask;
  protected string _DisplayMask;
  protected System.DateTime? _MinValue;
  protected System.DateTime? _MaxValue;
  protected bool _PreserveTime;
  protected bool _UseSmallDateTime = true;
  private bool _useTimeZone = true;
  private static IConstant<string>[] dateParts = new IConstant<string>[4]
  {
    (IConstant<string>) new DatePart.month(),
    (IConstant<string>) new DatePart.day(),
    (IConstant<string>) new DatePart.hour(),
    (IConstant<string>) new DatePart.quarter()
  };
  private static string[] datePartsNames = new string[4]
  {
    "_Month",
    "_Day",
    "_Hour",
    "_Quarter"
  };

  /// <summary>Gets or sets the format string that defines how a field value
  /// inputted by a user should be formatted.</summary>
  /// <value>If the property is set to a one-character string, the corresponding <see href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings">standard date and time
  /// format string</see> is used. If the property value is longer, it is treated as a <see href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings">custom date and time format
  /// string</see>. A particular pattern depends on the culture set by the application.</value>
  public string InputMask
  {
    get => this._InputMask;
    set => this._InputMask = value;
  }

  /// <summary>Gets or sets the format string that defines how a field value is displayed in the input control.</summary>
  /// <value>If the property is set to a one-character string, the corresponding <see href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings">standard date and time
  /// format string</see> is used. If the property value is longer, it is treated as a <see href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings">custom date and time format
  /// string</see>. A particular pattern depends on the culture set by the application.</value>
  /// <example>
  /// The attribute below binds the field to the database column and sets the input and display
  /// masks. A field value will be displayed using the long date pattern. That is, for en-US
  /// culture the <i>6/15/2009 1:45:30 PM</i> value will be converted to <i>Monday, June 15,
  /// 2009</i>.
  /// <code title="" description="" lang="CS">
  /// [PXDBDate(InputMask = "d", DisplayMask = "d")]
  /// public virtual DateTime? StartDate { get; set; }</code></example>
  public string DisplayMask
  {
    get => this._DisplayMask;
    set => this._DisplayMask = value;
  }

  /// <summary>Gets or sets the minimum value for the field.</summary>
  public string MinValue
  {
    get => this._MinValue.HasValue ? this._MinValue.ToString() : (string) null;
    set
    {
      if (value != null)
        this._MinValue = new System.DateTime?(System.DateTime.Parse(value, (IFormatProvider) CultureInfo.InvariantCulture));
      else
        this._MinValue = new System.DateTime?();
    }
  }

  /// <summary>Gets or sets the maximum value for the field.</summary>
  public string MaxValue
  {
    get => this._MaxValue.HasValue ? this._MaxValue.ToString() : (string) null;
    set
    {
      if (value != null)
        this._MaxValue = new System.DateTime?(System.DateTime.Parse(value, (IFormatProvider) CultureInfo.InvariantCulture));
      else
        this._MaxValue = new System.DateTime?();
    }
  }

  /// <summary>Gets or sets the value that indicates whether the time part
  /// of a field value is preserved. If <tt>false</tt>, the time part is
  /// removed.</summary>
  public virtual bool PreserveTime
  {
    get => this._PreserveTime;
    set
    {
      this._PreserveTime = value;
      this._DisplayMask = !value || this._DisplayMask != null ? this._DisplayMask : "g";
    }
  }

  /// <summary>
  ///   <para>Gets or sets the value that indicates the database column data type.</para>
  /// </summary>
  /// <remarks>
  ///   <para>The following table shows the difference in using the property for MS SQL and MySQL.</para>
  /// 	<table>
  /// 		<tbody>
  /// 			<tr>
  /// 				<td>
  /// 					<para align="center">
  /// 						<strong>Value</strong>
  /// 					</para>
  /// 				</td>
  /// 				<td>
  /// 					<para align="center">
  /// 						<strong>MS SQL</strong>
  /// 					</para>
  /// 				</td>
  /// 				<td>
  /// 					<para align="center">
  /// 						<strong>MySQL</strong>
  /// 					</para>
  /// 				</td>
  /// 			</tr>
  /// 			<tr>
  /// 				<td>false</td>
  /// 				<td>datetime</td>
  /// 				<td>datetime(6)</td>
  /// 			</tr>
  /// 			<tr>
  /// 				<td>true</td>
  /// 				<td>datetime2(0)</td>
  /// 				<td>datetime(0)</td>
  /// 			</tr>
  /// 		</tbody>
  /// 	</table>
  /// </remarks>
  /// <value>
  /// By default, a value is set to <code>true.</code>
  /// </value>
  public bool UseSmallDateTime
  {
    get => this._UseSmallDateTime;
    set => this._UseSmallDateTime = value;
  }

  /// <summary>Gets or sets the value that indicates whether the attribute
  /// should convert the time to UTC, using the local time zone. If
  /// <tt>true</tt>, the time is converted. By default,
  /// <tt>true</tt>.</summary>
  public virtual bool UseTimeZone
  {
    get => this._useTimeZone;
    set => this._useTimeZone = value;
  }

  internal static void SetUseTimeZone(PXCache cache, string field, bool useTimeZone)
  {
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(field))
    {
      if (attribute is PXDBDateAttribute)
      {
        ((PXDBDateAttribute) attribute).UseTimeZone = useTimeZone;
        break;
      }
    }
  }

  /// <exclude />
  protected override void PrepareCommandImpl(string dbFieldName, PXCommandPreparingEventArgs e)
  {
    base.PrepareCommandImpl(dbFieldName, e);
    e.DataType = this.UseSmallDateTime ? PXDbType.SmallDateTime : PXDbType.DateTime;
    if (e.Value != null)
    {
      if (this.UseTimeZone && this._PreserveTime)
      {
        System.DateTime utc = PXTimeZoneInfo.ConvertTimeToUtc((System.DateTime) e.Value, this.GetTimeZone());
        e.DataValue = (object) utc;
      }
      else
        e.DataValue = (object) (System.DateTime) e.Value;
    }
    if (e.DataValue != null && this.UseSmallDateTime)
    {
      System.DateTime? dataValue1 = e.DataValue as System.DateTime?;
      System.DateTime? maxValue = this._MaxValue;
      if ((dataValue1.HasValue & maxValue.HasValue ? (dataValue1.GetValueOrDefault() > maxValue.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        e.DataValue = (object) this._MaxValue;
      System.DateTime? dataValue2 = e.DataValue as System.DateTime?;
      System.DateTime? minValue = this._MinValue;
      if ((dataValue2.HasValue & minValue.HasValue ? (dataValue2.GetValueOrDefault() < minValue.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        e.DataValue = (object) this._MinValue;
    }
    e.DataLength = new int?(4);
  }

  /// <exclude />
  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
    {
      System.DateTime? nullable;
      try
      {
        nullable = e.Record.GetDateTime(e.Position);
      }
      catch (ArgumentOutOfRangeException ex)
      {
        nullable = new System.DateTime?();
      }
      if (nullable.HasValue)
      {
        if (this._PreserveTime)
        {
          if (this.UseTimeZone)
            nullable = new System.DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(nullable.Value, this.GetTimeZone()));
        }
        else
        {
          ref System.DateTime? local = ref nullable;
          System.DateTime dateTime1 = nullable.Value;
          int year = dateTime1.Year;
          dateTime1 = nullable.Value;
          int month = dateTime1.Month;
          int day = nullable.Value.Day;
          System.DateTime dateTime2 = new System.DateTime(year, month, day);
          local = new System.DateTime?(dateTime2);
        }
      }
      sender.SetValue(e.Row, this._FieldOrdinal, (object) nullable);
    }
    ++e.Position;
  }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue is string)
    {
      System.DateTime result;
      if (System.DateTime.TryParse((string) e.NewValue, (IFormatProvider) sender.Graph.Culture, DateTimeStyles.None, out result))
      {
        if (!this._PreserveTime)
          result = new System.DateTime(result.Year, result.Month, result.Day);
        e.NewValue = (object) result;
      }
      else
        e.NewValue = (object) null;
    }
    else
    {
      if (this._PreserveTime || e.NewValue == null)
        return;
      System.DateTime newValue = (System.DateTime) e.NewValue;
      e.NewValue = (object) new System.DateTime(newValue.Year, newValue.Month, newValue.Day);
    }
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXDateState.CreateInstance(e.ReturnState, this._FieldName, new bool?(this._IsKey), new int?(), this._InputMask, this._DisplayMask, this._MinValue, this._MaxValue);
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!this._MinValue.HasValue)
      this._MinValue = new System.DateTime?(PXDBDateAttribute._MIN_VALUE);
    if (!this._MaxValue.HasValue)
      this._MaxValue = new System.DateTime?(new System.DateTime(9999, 12, 31 /*0x1F*/));
    this.AddFieldsForDateParts(sender);
  }

  protected virtual PXTimeZoneInfo GetTimeZone() => LocaleInfo.GetTimeZone();

  /// <exclude />
  protected static IEnumerable<PXDBDateAndTimeAttribute> GetAttribute(
    PXCache cache,
    object data,
    string name)
  {
    return cache.GetAttributes(data, name).OfType<PXDBDateAndTimeAttribute>();
  }

  private void AddFieldsForDateParts(PXCache cache)
  {
    for (int index = 0; index < PXDBDateAttribute.dateParts.Length; ++index)
      this.AddFieldForDatePart(cache, PXDBDateAttribute.dateParts[index], PXDBDateAttribute.datePartsNames[index]);
  }

  private void AddFieldForDatePart(PXCache cache, IConstant<string> datePart, string datePartName)
  {
    string partFieldName = this._FieldName + datePartName;
    if (cache.Fields.Contains(partFieldName))
      return;
    cache.Fields.Add(partFieldName);
    PXDBIntAttribute pxdbIntAttribute = new PXDBIntAttribute();
    pxdbIntAttribute.BqlTable = this.BqlTable;
    pxdbIntAttribute.FieldName = this._FieldName;
    PXDBIntAttribute attr = pxdbIntAttribute;
    cache.FieldSelectingEvents.Add(partFieldName, (PXFieldSelecting) ((sender, e) =>
    {
      string empty = string.Empty;
      object obj = sender.GetValue(e.Row, this._FieldName);
      if (e.Row != null && obj != null)
      {
        empty = obj.ToString();
        e.ReturnValue = (object) empty;
      }
      PXFieldState instance = PXStringState.CreateInstance((object) empty, new int?(), new bool?(true), partFieldName, new bool?(false), new int?(0), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null);
      instance.Visible = false;
      instance.Enabled = false;
      instance.Visibility = PXUIVisibility.Invisible;
      e.ReturnState = (object) instance;
    }));
    cache.FieldUpdatingEvents.Add(partFieldName, (PXFieldUpdating) ((sender, e) =>
    {
      if (e.Row == null)
        return;
      object obj = sender.GetValue(e.Row, this._FieldName);
      if (obj == null)
        return;
      e.NewValue = (object) Convert.ToInt32(obj);
    }));
    cache.CommandPreparingEvents.Add(partFieldName, (PXCommandPreparing) ((sender, e) =>
    {
      if (!e.IsSelect() || (e.Operation & PXDBOperation.Option) != PXDBOperation.External)
        return;
      attr.CommandPreparing(sender, e);
      string fieldName = this.FieldName;
      bool flag = this.UseTimeZone && this._PreserveTime;
      e.Expr = SQLExpression.DatePartByTimeZone(datePart, (SQLExpression) new Column(fieldName, e.Table), flag ? this.GetTimeZone() : (PXTimeZoneInfo) null);
    }));
  }
}
