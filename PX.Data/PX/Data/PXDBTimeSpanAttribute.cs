// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBTimeSpanAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;
using System.Globalization;

#nullable enable
namespace PX.Data;

/// <summary>Maps a DAC field of <tt>int?</tt> type to the <tt>int</tt>
/// database column. The field value represents a date as a number of
/// minutes passed from 01/01/1900.</summary>
/// <remarks>
/// <para>The attribute is added to the value declaration of a DAC field.
/// The field becomes bound to the database column with the same
/// name.</para>
/// <para>The field value stores a date as a number of minutes. In the UI,
/// the string is typically represented by a control allowing a selection
/// from the list of time values with half-hour interval.</para>
/// </remarks>
/// <example>
/// <code>
/// [PXDBTimeSpan]
/// [PXUIField(DisplayName = "Run Time")]
/// public virtual int? RunTime { get; set; ]
/// </code>
/// </example>
public class PXDBTimeSpanAttribute : PXDBIntAttribute
{
  protected 
  #nullable disable
  string _InputMask = "HH:mm";
  protected string _DisplayMask = "HH:mm";
  protected System.DateTime? _MinValue;
  protected System.DateTime? _MaxValue;
  /// <summary>The "00:00" constant.</summary>
  public const string Zero = "00:00";

  /// <summary>Gets or sets the input mask for date and time values that can
  /// be entered as value of the current field. By default, the proprty
  /// equals <i>HH:mm</i>.</summary>
  public string InputMask
  {
    get => this._InputMask;
    set => this._InputMask = value;
  }

  /// <summary>Gets or sets the display mask for date and time values that
  /// can be entered as value of the current field. By default, the proprty
  /// equals <i>HH:mm</i>.</summary>
  public string DisplayMask
  {
    get => this._DisplayMask;
    set => this._DisplayMask = value;
  }

  /// <summary>Gets or sets the minimum value for the field. The value
  /// should be a valid string representation of a date.</summary>
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

  /// <summary>Gets or sets the maximum value for the field. The value
  /// should be a valid string representation of a date.</summary>
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
    if (e.ReturnValue == null || !(e.ReturnValue is int) && !(e.ReturnValue is int?))
      return;
    TimeSpan timeSpan = new TimeSpan(0, 0, (int) e.ReturnValue, 0);
    e.ReturnValue = (object) new System.DateTime(1900, 1, 1).Add(timeSpan);
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

  /// <summary>Returns the date obtained by adding the specified number of
  /// minutes to 01/01/1900.</summary>
  /// <param name="minutes">The minutes to add to the default date.</param>
  public static System.DateTime FromMinutes(int minutes)
  {
    return new System.DateTime(1900, 1, 1).Add(new TimeSpan(0, 0, minutes, 0));
  }

  /// <summary>The BQL constant representing string "00:00".</summary>
  public sealed class zero : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PXDBTimeSpanAttribute.zero>
  {
    public zero()
      : base("00:00")
    {
    }
  }
}
