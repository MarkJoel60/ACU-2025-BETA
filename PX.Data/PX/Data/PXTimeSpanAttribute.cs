// Decompiled with JetBrains decompiler
// Type: PX.Data.PXTimeSpanAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>Indicates a DAC field of <tt>int?</tt> type that represents a date value as minutes passed from 01/01/1900 and that is not mapped to a database column.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field is not bound to a table column.</remarks>
/// <example>
/// The code below shows a full definition of a DAC field not bound to any database columns.
/// <code title="" description="" lang="CS">
/// public abstract class timeOnly : PX.Data.BQL.BqlInt.Field&lt;timeOnly&gt; { }
/// [PXUIField(DisplayName = "Time")]
/// [PXTimeSpan]
/// public virtual int? TimeOnly
/// {
///     get
///     {
///         return (int?)Date.Value.TimeOfDay.TotalMinutes;
///     }
/// }</code></example>
public class PXTimeSpanAttribute : PXIntAttribute
{
  protected string _InputMask = "HH:mm";
  protected string _DisplayMask = "HH:mm";
  protected System.DateTime? _MinValue;
  protected System.DateTime? _MaxValue;

  /// <summary>Gets or sets the pattern that indicates the allowed
  /// characters in a field value. The user interface will not allow the
  /// user to enter other characters in the input control associated with
  /// the field.</summary>
  public string InputMask
  {
    get => this._InputMask;
    set => this._InputMask = value;
  }

  /// <summary>Get, set.</summary>
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

  /// <exclude />
  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel == PXAttributeLevel.Item || e.IsAltered)
      e.ReturnState = (object) PXDateState.CreateInstance(e.ReturnState, this._FieldName, new bool?(this._IsKey), new int?(), this._InputMask, this._DisplayMask, this._MinValue, this._MaxValue);
    if (e.ReturnValue == null)
      return;
    TimeSpan timeSpan = new TimeSpan(0, 0, (int) e.ReturnValue, 0);
    e.ReturnValue = (object) new System.DateTime(1900, 1, 1, timeSpan.Hours, timeSpan.Minutes, 0);
  }

  /// <exclude />
  public override void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null || e.NewValue is int)
      return;
    if (e.NewValue is string)
    {
      System.DateTime result;
      if (System.DateTime.TryParse((string) e.NewValue, (IFormatProvider) sender.Graph.Culture, DateTimeStyles.None, out result))
      {
        TimeSpan timeSpan = new TimeSpan(result.Hour, result.Minute, 0);
        e.NewValue = (object) (int) timeSpan.TotalMinutes;
      }
      else
        e.NewValue = (object) null;
    }
    else
    {
      if (!(e.NewValue is System.DateTime))
        return;
      System.DateTime newValue = (System.DateTime) e.NewValue;
      TimeSpan timeSpan = new TimeSpan(newValue.Hour, newValue.Minute, 0);
      e.NewValue = (object) (int) timeSpan.TotalMinutes;
    }
  }
}
