// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDateState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Provides data to set up the presentation of the input control for the <tt>DateTime</tt> DAC field.</summary>
public class PXDateState : PXFieldState
{
  protected string _InputMask;
  protected string _DisplayMask;
  protected System.DateTime _MinValue = new System.DateTime(1900, 1, 1);
  protected System.DateTime _MaxValue = new System.DateTime(9999, 12, 31 /*0x1F*/);

  protected PXDateState(object value)
    : base(value)
  {
  }

  /// <summary>A value that specifies how users enter the date.</summary>
  public virtual string InputMask
  {
    get => this._InputMask;
    set => this._InputMask = value;
  }

  /// <summary>A value that specifies how the date is displayed.</summary>
  public virtual string DisplayMask
  {
    get => this._DisplayMask;
    set => this._DisplayMask = value;
  }

  /// <summary>The minimum value that can be set in the field input control.</summary>
  public virtual System.DateTime MinValue
  {
    get => this._MinValue;
    set => this._MinValue = value;
  }

  /// <summary>The maximum value that can be set in the field input control.</summary>
  public virtual System.DateTime MaxValue
  {
    get => this._MaxValue;
    set => this._MaxValue = value;
  }

  /// <summary>Configures a date field state from the provided parameters.</summary>
  /// <param name="minValue">The minimum value that can be set in the field input control.</param>
  /// <param name="maxValue">The maximum value that can be set in the field input control.</param>
  /// <param name="inputMask">The value that specifies how users enter the date.</param>
  /// <param name="displayMask">The value that specifies how the date is displayed.</param>
  /// <param name="required">The value that indicates (if set to <see langword="true" />) that the value of the field is required.</param>
  /// <param name="value">The value that is stored in the field.</param>
  /// <param name="isKey">The value that indicates (if set to <see langword="true" />) that the field is marked as a key field.</param>
  /// <param name="fieldName">The name of the field.</param>
  public static PXFieldState CreateInstance(
    object value,
    string fieldName,
    bool? isKey,
    int? required,
    string inputMask,
    string displayMask,
    System.DateTime? minValue,
    System.DateTime? maxValue)
  {
    switch (value)
    {
      case PXDateState instance1:
label_6:
        instance1._DataType = typeof (System.DateTime);
        if (fieldName != null)
          instance1._FieldName = fieldName;
        if (isKey.HasValue)
          instance1._IsKey = isKey.Value;
        if (required.HasValue)
          instance1._Required += required.Value;
        if (inputMask != null)
          instance1._InputMask = inputMask;
        if (displayMask != null)
          instance1._DisplayMask = displayMask;
        if (minValue.HasValue)
          instance1._MinValue = minValue.Value;
        if (maxValue.HasValue)
          instance1._MaxValue = maxValue.Value;
        return (PXFieldState) instance1;
      case PXFieldState instance2:
        if (instance2.DataType != typeof (object) && instance2.DataType != typeof (System.DateTime))
        {
          if (isKey.HasValue)
            instance2._IsKey = isKey.Value;
          return instance2;
        }
        goto default;
      default:
        instance1 = new PXDateState(value);
        goto label_6;
    }
  }
}
