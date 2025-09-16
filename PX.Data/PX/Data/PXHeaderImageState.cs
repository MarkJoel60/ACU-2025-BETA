// Decompiled with JetBrains decompiler
// Type: PX.Data.PXHeaderImageState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXHeaderImageState : PXFieldState
{
  private string _headerImage;

  protected PXHeaderImageState(object value)
    : base(value)
  {
  }

  public string HeaderImage => this._headerImage;

  public static PXFieldState CreateInstance(object value, string fieldName, string headerImage)
  {
    if (!(value is PXHeaderImageState instance))
      instance = new PXHeaderImageState(value);
    if (headerImage != null)
      instance._headerImage = headerImage;
    if (fieldName != null)
      instance._FieldName = fieldName;
    return (PXFieldState) instance;
  }
}
