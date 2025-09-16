// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGuidState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Provides data to set up the presentation of the input control for the <tt>Guid</tt> DAC field.</summary>
public class PXGuidState : PXFieldState
{
  protected PXGuidState(object value)
    : base(value)
  {
    this._DataType = typeof (Guid);
  }

  /// <summary>Configures a guid field state from the provided parameters.</summary>
  /// <param name="value">The value that is stored in the field.</param>
  /// <param name="fieldName">The name of the field.</param>
  /// <param name="isKey">The value that indicates (if set to <see langword="true" />) that the field is marked as a key field.</param>
  /// <param name="required">The value that indicates (if set to <see langword="true" />) that the value of the field is required.</param>
  public static PXFieldState CreateInstance(
    object value,
    string fieldName,
    bool? isKey,
    int? required)
  {
    switch (value)
    {
      case PXGuidState instance1:
label_6:
        instance1._DataType = typeof (Guid);
        if (fieldName != null)
          instance1._FieldName = fieldName;
        if (isKey.HasValue)
          instance1._IsKey = isKey.Value;
        if (required.HasValue)
          instance1._Required += required.Value;
        return (PXFieldState) instance1;
      case PXFieldState instance2:
        if (instance2.DataType != typeof (object) && instance2.DataType != typeof (Guid))
        {
          if (isKey.HasValue)
            instance2._IsKey = isKey.Value;
          return instance2;
        }
        goto default;
      default:
        instance1 = new PXGuidState(value);
        goto label_6;
    }
  }
}
