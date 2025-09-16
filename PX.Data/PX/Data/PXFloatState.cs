// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFloatState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Provides data to set up the presentation of the input control for the <tt>float</tt> DAC field.</summary>
public class PXFloatState : PXFieldState
{
  protected float _MinValue = float.MinValue;
  protected float _MaxValue = float.MaxValue;

  protected PXFloatState(object value)
    : base(value)
  {
    if (!(value is PXFloatState pxFloatState))
      return;
    this._Precision = pxFloatState._Precision;
  }

  /// <summary>The minimum value that could be set in the field input control.</summary>
  public virtual float MinValue
  {
    get => this._MinValue;
    set => this._MinValue = value;
  }

  /// <summary>The maximum value that could be set in the field input control.</summary>
  public virtual float MaxValue
  {
    get => this._MaxValue;
    set => this._MaxValue = value;
  }

  /// <summary>Configures a float field state from the provided parameters.</summary>
  /// <param name="value">The value that is stored in the field.</param>
  /// <param name="fieldName">The name of the field.</param>
  /// <param name="precision">The maximum number of digits used to represent a numeric value stored in the field.</param>
  /// <param name="isKey">The value that indicates (if set to <see langword="true" />) that the field is marked as a key field.</param>
  /// <param name="required">The value that indicates (if set to <see langword="true" />) that the value of the field is required.</param>
  /// <param name="minValue">The minimum value that can be set in the field input control.</param>
  /// <param name="maxValue">The maximum value that can be set in the field input control.</param>
  public static PXFieldState CreateInstance(
    object value,
    int? precision,
    string fieldName,
    bool? isKey,
    int? required,
    float? minValue,
    float? maxValue)
  {
    switch (value)
    {
      case PXFloatState instance1:
label_6:
        instance1._DataType = typeof (float);
        if (required.HasValue)
          instance1._Required += required.Value;
        if (precision.HasValue)
          instance1._Precision = precision.Value;
        if (fieldName != null)
          instance1._FieldName = fieldName;
        if (isKey.HasValue)
          instance1._IsKey = isKey.Value;
        if (minValue.HasValue)
          instance1._MinValue = minValue.Value;
        if (maxValue.HasValue)
          instance1._MaxValue = maxValue.Value;
        return (PXFieldState) instance1;
      case PXFieldState instance2:
        if (instance2.DataType != typeof (object) && instance2.DataType != typeof (float))
        {
          if (isKey.HasValue)
            instance2._IsKey = isKey.Value;
          return instance2;
        }
        goto default;
      default:
        instance1 = new PXFloatState(value);
        goto label_6;
    }
  }
}
