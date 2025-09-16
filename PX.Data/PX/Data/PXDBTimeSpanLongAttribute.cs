// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBTimeSpanLongAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field of the <tt>int?</tt> type that represents the
/// duration (in minutes) to an <tt>int</tt> database column.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field becomes bound to the database column with the same
/// name.</remarks>
/// <example>
/// <code>
/// [PXDBTimeSpanLong(Format = TimeSpanFormatType.LongHoursMinutes)]
/// [PXUIField(DisplayName = "Estimation")]
/// public virtual Int32? TimeEstimated { get; set; }
/// </code>
/// </example>
public class PXDBTimeSpanLongAttribute : PXDBIntAttribute
{
  protected string[] _inputMasks = new string[5]
  {
    "### d ## h ## m",
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
  private string _inputMask;
  private int _maskLength;

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
    get => this._inputMask;
    set
    {
      this._inputMask = value;
      this._maskLength = 0;
      foreach (char ch in value)
      {
        switch (ch)
        {
          case '#':
          case '0':
            ++this._maskLength;
            break;
        }
      }
    }
  }

  /// <exclude />
  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    int format = (int) this._Format;
    if (this._AttributeLevel == PXAttributeLevel.Item || e.IsAltered)
    {
      string str = PXMessages.LocalizeNoPrefix(this._inputMask ?? this._inputMasks[format]);
      int num = this._inputMask != null ? this._maskLength : this._lengths[format];
      e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(num), new bool?(), this._FieldName, new bool?(this._IsKey), new int?(), string.IsNullOrEmpty(str) ? (string) null : str, (string[]) null, (string[]) null, new bool?(), (string) null);
    }
    if (e.ReturnState == null)
      return;
    int result;
    int? nullable = new int?(!(e.ReturnValue is int returnValue1) ? (!(e.ReturnValue is string returnValue2) || !int.TryParse(returnValue2, out result) ? 0 : result) : returnValue1);
    if (!nullable.HasValue)
      return;
    TimeSpan timeSpan = new TimeSpan(0, 0, nullable.Value, 0);
    int num1 = this._Format == TimeSpanFormatType.LongHoursMinutes ? timeSpan.Days * 24 + timeSpan.Hours : timeSpan.Hours;
    e.ReturnValue = (object) string.Format(this._outputFormats[format], (object) timeSpan.Days, (object) num1, (object) timeSpan.Minutes);
  }

  /// <exclude />
  public override void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue is string newValue)
    {
      int length1 = newValue.Length;
      int length2 = this._lengths[(int) this._Format];
      if (length1 < length2)
      {
        StringBuilder stringBuilder = new StringBuilder(length2);
        for (int index = length1; index < length2; ++index)
          stringBuilder.Append('0');
        stringBuilder.Append(newValue);
        e.NewValue = (object) (newValue = stringBuilder.ToString());
      }
      int result;
      if (!string.IsNullOrEmpty(newValue) && int.TryParse(newValue.Replace(" ", "0"), out result))
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
