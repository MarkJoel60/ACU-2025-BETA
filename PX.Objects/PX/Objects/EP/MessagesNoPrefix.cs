// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.MessagesNoPrefix
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.EP;

[PXLocalizable]
public static class MessagesNoPrefix
{
  public const string ApprovalRecordNotFound = "Record for approving not found.";
  public const string ReassignmentOfApprovalsNotSupported = "Reassignment of approvals is supported only for maps of the Approval Map type.";
  public const string ReassignmentNotAllowed = "The approval request cannot be reassigned because reassignment of approvals is not allowed in the approval map rule.";
  public const string ReassignmentApproverNotAvailable = "The selected approver or their delegates are not available for the specified period. Select another approver.";
  public const string ReassignmentDelegateNotAvailable = "The delegate or their delegates are not available. The approval was not delegated.";
  public const string DelegateStartsOnDateInThePast = "The Starts On date cannot be in the past.";
  public const string DelegateStartsExpiresOnDatesInsideExistingPeriod = "The date is within the period specified for one of the existing delegations. The delegation periods cannot intersect.";
  public const string DelegateExpiresOnDateBeforeStartsOn = "The Expires On date cannot be before the Starts On date.";
}
