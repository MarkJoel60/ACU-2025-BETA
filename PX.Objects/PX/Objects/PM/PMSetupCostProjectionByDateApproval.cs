// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMSetupCostProjectionByDateApproval
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.EP;
using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace PX.Objects.PM;

[PXHidden]
[PXBreakInheritance]
[ExcludeFromCodeCoverage]
public class PMSetupCostProjectionByDateApproval : PMSetup, IAssignedMap
{
  int? IAssignedMap.AssignmentMapID
  {
    get => this.CostProjectionByDateApprovalMapID;
    set => this.CostProjectionByDateApprovalMapID = value;
  }

  int? IAssignedMap.AssignmentNotificationID
  {
    get => this.CostProjectionByDateApprovalNotificationID;
    set => this.CostProjectionByDateApprovalNotificationID = value;
  }

  bool? IAssignedMap.IsActive => new bool?(this.CostProjectionByDateApprovalMapID.HasValue);
}
