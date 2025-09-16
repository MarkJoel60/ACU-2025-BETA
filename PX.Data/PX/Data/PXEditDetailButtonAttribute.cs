// Decompiled with JetBrains decompiler
// Type: PX.Data.PXEditDetailButtonAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXEditDetailButtonAttribute : PXButtonAttribute
{
  public PXEditDetailButtonAttribute()
  {
    this.ImageKey = "RecordEdit";
    this.Tooltip = "Edit Record";
    this.SpecialType = PXSpecialButtonType.EditDetail;
  }
}
