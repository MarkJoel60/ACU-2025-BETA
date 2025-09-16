// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPSetupExpenseClaimDetailApproval
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace PX.Objects.EP;

[PXHidden]
[PXBreakInheritance]
[ExcludeFromCodeCoverage]
public class EPSetupExpenseClaimDetailApproval : EPSetup, IAssignedMap
{
  int? IAssignedMap.AssignmentMapID
  {
    get => this.ClaimDetailsAssignmentMapID;
    set => this.ClaimDetailsAssignmentMapID = value;
  }

  int? IAssignedMap.AssignmentNotificationID
  {
    get => this.ClaimDetailsAssignmentNotificationID;
    set => this.ClaimDetailsAssignmentNotificationID = value;
  }

  bool? IAssignedMap.IsActive => new bool?(this.ClaimDetailsAssignmentMapID.HasValue);
}
