// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLongState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Provides data to set up the presentation of the input control for the <see langword="long" /> DAC field.</summary>
public class PXLongState : PXFieldState
{
  protected long _MinValue = long.MinValue;
  protected long _MaxValue = long.MaxValue;

  protected PXLongState(object value)
    : base(value)
  {
    this._DataType = typeof (long);
  }

  /// <summary>The minimum value that could be set in the field input control.</summary>
  public virtual long MinValue
  {
    get => this._MinValue;
    set => this._MinValue = value;
  }

  /// <summary>The maximum value that could be set in the field input control.</summary>
  public virtual long MaxValue
  {
    get => this._MaxValue;
    set => this._MaxValue = value;
  }

  /// <exclude />
  public static PXFieldState CreateInstance(
    object value,
    string fieldName,
    bool? isKey,
    int? required,
    long? minValue,
    long? maxValue,
    System.Type dataType)
  {
    switch (value)
    {
      case PXLongState instance1:
label_6:
        if (dataType != (System.Type) null)
          instance1._DataType = dataType;
        if (fieldName != null)
          instance1._FieldName = fieldName;
        if (isKey.HasValue)
          instance1._IsKey = isKey.Value;
        if (required.HasValue)
          instance1._Required += required.Value;
        if (minValue.HasValue)
          instance1._MinValue = minValue.Value;
        if (maxValue.HasValue)
          instance1._MaxValue = maxValue.Value;
        return (PXFieldState) instance1;
      case PXFieldState instance2:
        if (instance2.DataType != typeof (object) && instance2.DataType != dataType)
        {
          if (isKey.HasValue)
            instance2._IsKey = isKey.Value;
          return instance2;
        }
        goto default;
      default:
        instance1 = new PXLongState(value);
        goto label_6;
    }
  }
}
