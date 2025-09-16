// Decompiled with JetBrains decompiler
// Type: PX.Data.PXTimeSpanLongAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data;

/// <summary>Indicates a DAC field of the <tt>int?</tt> type that represents the duration (in minutes)
/// and that is not mapped to a database column.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field. The field is not bound to a table column.</remarks>
/// <example>
///   <code title="" description="" lang="CS">
/// [PXTimeSpanLong(Format = TimeSpanFormatType.LongHoursMinutes)]
/// public virtual int? InitResponse { get; set; }</code>
/// </example>
public class PXTimeSpanLongAttribute : PXIntAttribute
{
  protected string[] _inputMasks = new string[5]
  {
    "### d\\ays ## hrs ## mins",
    "### d 00:00",
    "#### h ## m",
    "## h ## m",
    "00:00"
  };
  protected string[] _outputFormats = new string[5]
  {
    "{0,3}{1:00}{2:00}",
    "{0,3}{1:00}{2:00}",
    "{1,4}{2:00}",
    "{1,2}{2:00}",
    "{1:00}{2:00}"
  };
  protected int[] _lengths = new int[5]{ 7, 7, 6, 4, 4 };
  protected bool _NullIsZero;
  protected TimeSpanFormatType _Format;
  private string inputMask;
  private int _maskLenght;

  /// <summary>Gets or sets the data format type. Possible values are
  /// defined by the <see cref="T:PX.Data.TimeSpanFormatType">TimeSpanFormatType</see>
  /// enumeration.</summary>
  public TimeSpanFormatType Format
  {
    get => this._Format;
    set => this._Format = value;
  }

  /// <summary>Gets or sets the pattern that indicates the allowed
  /// characters in a field value. By default, the property is null, and the
  /// attribute determines the input mask by the <tt>Format</tt>
  /// value.</summary>
  public string InputMask
  {
    get => this.inputMask;
    set
    {
      this.inputMask = value;
      this._maskLenght = 0;
      foreach (char ch in value)
      {
        switch (ch)
        {
          case '#':
          case '0':
            ++this._maskLenght;
            break;
        }
      }
    }
  }

  /// <summary>Initializes a new instance with default parameters.</summary>
  public PXTimeSpanLongAttribute()
  {
    this.inputMask = (string) null;
    this._maskLenght = 0;
  }

  /// <exclude />
  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel == PXAttributeLevel.Item || e.IsAltered)
    {
      string strMessage = this.inputMask ?? this._inputMasks[(int) this._Format];
      int num = this.inputMask != null ? this._maskLenght : this._lengths[(int) this._Format];
      string str = PXMessages.LocalizeNoPrefix(strMessage);
      e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(num), new bool?(), this._FieldName, new bool?(this._IsKey), new int?(), string.IsNullOrEmpty(str) ? (string) null : str, (string[]) null, (string[]) null, new bool?(), (string) null);
    }
    if (e.ReturnValue == null)
      return;
    TimeSpan timeSpan = new TimeSpan(0, 0, (int) e.ReturnValue, 0);
    int num1 = this._Format == TimeSpanFormatType.LongHoursMinutes ? timeSpan.Days * 24 + timeSpan.Hours : timeSpan.Hours;
    string str1 = string.Format(this._outputFormats[(int) this._Format], (object) timeSpan.Days, (object) num1, (object) timeSpan.Minutes);
    e.ReturnValue = str1.Length < this._maskLenght ? (object) (new string(' ', this._maskLenght - str1.Length) + str1) : (object) str1;
  }

  /// <exclude />
  public override void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue is string)
    {
      int length1 = ((string) e.NewValue).Length;
      int length2 = this._lengths[(int) this._Format];
      if (length1 < length2)
      {
        StringBuilder stringBuilder = new StringBuilder(length2);
        for (int index = length1; index < length2; ++index)
          stringBuilder.Append('0');
        stringBuilder.Append((string) e.NewValue);
        e.NewValue = (object) stringBuilder.ToString();
      }
      int result = 0;
      if (!string.IsNullOrEmpty((string) e.NewValue) && int.TryParse(((string) e.NewValue).Replace(" ", "0"), out result))
      {
        int minutes = result % 100;
        int hours = (result - minutes) / 100 % 100;
        int days = ((result - minutes) / 100 - hours) / 100;
        if (this.Format == TimeSpanFormatType.LongHoursMinutes)
        {
          hours = (result - minutes) / 100;
          days = 0;
        }
        TimeSpan timeSpan = new TimeSpan(days, hours, minutes, 0);
        e.NewValue = (object) (int) timeSpan.TotalMinutes;
      }
      else
        e.NewValue = (object) null;
    }
    if (e.NewValue != null || !this._NullIsZero)
      return;
    e.NewValue = (object) 0;
  }
}
