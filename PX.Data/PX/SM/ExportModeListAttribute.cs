// Decompiled with JetBrains decompiler
// Type: PX.SM.ExportModeListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Update;

#nullable disable
namespace PX.SM;

public class ExportModeListAttribute : PXStringListAttribute
{
  public ExportModeListAttribute()
    : base(PXSnapshotBase.GetExportModes(), PXSnapshotBase.GetExportModes(true))
  {
  }
}
