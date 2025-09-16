// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSLogActionStartStaffFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

[PXVirtual]
[PXBreakInheritance]
[Serializable]
public class FSLogActionStartStaffFilter : FSLogActionFilter
{
  [PXString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Action", Enabled = false)]
  [PXUnboundDefault("ST")]
  [ListField_LogActions.StartList]
  public override string Action { get; set; }

  [PXString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Logging", Enabled = false)]
  [PXUnboundDefault("SA")]
  [FSLogTypeAction.List]
  public override string Type { get; set; }
}
