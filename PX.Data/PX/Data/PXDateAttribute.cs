// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDateAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>Indicates a DAC field of <tt>DateTime?</tt> type that is not mapped to a database column.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field. The field is not bound to a table column.</remarks>
/// <example>
///   <code title="" description="" lang="CS">
/// [PXDate()]
/// public virtual DateTime? NextEffDate { get; set; }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
[PXAttributeFamily(typeof (PXFieldState))]
public class PXDateAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXCommandPreparingSubscriber
{
  protected string _InputMask;
  protected string _DisplayMask;
  protected System.DateTime? _MinValue;
  protected System.DateTime? _MaxValue;
  protected bool _IsKey;

  /// <summary>Gets or sets the value that indicates whether the field is a
  /// key field.</summary>
  public virtual bool IsKey
  {
    get => this._IsKey;
    set => this._IsKey = value;
  }

  /// <summary>Gets or sets the format string that defines how a field value
  /// inputted by a user should be formatted.</summary>
  /// <value>If the property is set to a one-character string, the corresponding
  /// <see href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings">
  /// standard date and time format string</see>
  /// is used. If the property value is longer, it is treated as a
  /// <see href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings">
  /// custom date and time format string</see>.
  /// A particular pattern depends on the culture set by the application.</value>
  public string InputMask
  {
    get => this._InputMask;
    set => this._InputMask = value;
  }

  /// <summary>Gets or sets the format string that defines how a field value is displayed in the input control.</summary>
  /// <value>If the property is set to a one-character string, the corresponding
  /// <see href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings">
  /// standard date and time format string</see> is used. If the property value is longer, it is treated as a
  /// <see href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings">
  /// custom date and time format string</see>. A particular pattern depends on the culture set by the application.</value>
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
        this._MinValue = new System.DateTime?(System.DateTime.Parse(value));
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
        this._MaxValue = new System.DateTime?(System.DateTime.Parse(value));
      else
        this._MaxValue = new System.DateTime?();
    }
  }

  /// <summary>Gets or sets the value that indicates whether the attribute
  /// should convert the time to UTC, using the local time zone. If
  /// <see langword="true" />, the time is converted.</summary>
  public bool UseTimeZone { get; set; }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!(e.NewValue is string))
      return;
    System.DateTime result;
    if (System.DateTime.TryParse((string) e.NewValue, (IFormatProvider) sender.Graph.Culture, DateTimeStyles.None, out result))
      e.NewValue = (object) result;
    else
      e.NewValue = (object) null;
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXDateState.CreateInstance(e.ReturnState, this._FieldName, new bool?(this._IsKey), new int?(-1), this._InputMask, this._DisplayMask, this._MinValue, this._MaxValue);
  }

  /// <exclude />
  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Select)
      return;
    if (e.Value != null)
    {
      e.BqlTable = this._BqlTable;
      if (e.Expr == null)
        e.Expr = (SQLExpression) new Column(this._FieldName, e.BqlTable);
      if (this.UseTimeZone)
      {
        System.DateTime utc = PXTimeZoneInfo.ConvertTimeToUtc((System.DateTime) e.Value, LocaleInfo.GetTimeZone());
        e.DataValue = (object) utc;
      }
      else
        e.DataValue = e.Value;
      e.DataLength = new int?(4);
    }
    e.DataType = PXDbType.DateTime;
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    if (this.IsKey)
      sender.Keys.Add(this._FieldName);
    if (!this._MinValue.HasValue)
      this._MinValue = new System.DateTime?(new System.DateTime(1900, 1, 1));
    if (this._MaxValue.HasValue)
      return;
    this._MaxValue = new System.DateTime?(new System.DateTime(9999, 12, 31 /*0x1F*/));
  }
}
