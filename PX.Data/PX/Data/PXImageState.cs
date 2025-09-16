// Decompiled with JetBrains decompiler
// Type: PX.Data.PXImageState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXImageState : PXStringState
{
  private string _headerImage;

  protected PXImageState(object value)
    : base(value)
  {
  }

  public string HeaderImage => this._headerImage;

  public static PXFieldState CreateInstance(
    object value,
    int? length,
    bool? isUnicode,
    string fieldName,
    bool? isKey,
    int? required,
    string inputMask,
    string[] allowedValues,
    string[] allowedLabels,
    bool? exclusiveValues,
    string defaultValue,
    string headerImage)
  {
    switch (value)
    {
      case PXImageState instance1:
label_4:
        instance1._DataType = typeof (string);
        if (length.HasValue)
          instance1._Length = length.Value;
        if (isUnicode.HasValue)
          instance1._IsUnicode = isUnicode.Value;
        if (fieldName != null)
          instance1._FieldName = fieldName;
        if (isKey.HasValue)
          instance1._IsKey = isKey.Value;
        if (required.HasValue)
          instance1._Required += required.Value;
        if (defaultValue != null)
          instance1._DefaultValue = (object) defaultValue;
        if (headerImage != null)
          instance1._headerImage = headerImage;
        return (PXFieldState) instance1;
      case PXFieldState instance2:
        if (instance2.DataType != typeof (object) && instance2.DataType != typeof (string))
          return instance2;
        goto default;
      default:
        instance1 = new PXImageState(value);
        goto label_4;
    }
  }
}
