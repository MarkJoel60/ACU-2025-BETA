// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.PM.Services.ProjectTaskTypeUsageInConstructionValidationService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.EP;
using PX.Objects.IN;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.PM.Services;

public class ProjectTaskTypeUsageInConstructionValidationService : 
  ProjectTaskTypeUsageValidationServiceBase
{
  protected override bool IsTaskUsedInCostDocument(int? projectID, int? taskId)
  {
    return this.IsTaskUsed<PMBudget, PMBudget.projectID, PMBudget.projectTaskID, PMBudget.type>(projectID, taskId, "E") || this.IsTaskUsed<PX.Objects.PO.POLine, PX.Objects.PO.POLine.projectID, PX.Objects.PO.POLine.taskID>(projectID, taskId) || this.IsTaskUsed<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.projectID, PX.Objects.PO.POReceiptLine.taskID>(projectID, taskId) || this.IsTaskUsed<INTran, INTran.projectID, INTran.taskID>(projectID, taskId) || this.IsTaskUsed<PX.Objects.AP.APTran, PX.Objects.AP.APTran.projectID, PX.Objects.AP.APTran.taskID>(projectID, taskId) || this.IsTaskUsed<EPEquipmentSummary, EPEquipmentSummary.projectID, EPEquipmentSummary.projectTaskID>(projectID, taskId) || this.IsTaskUsed<EPEquipmentDetail, EPEquipmentDetail.projectID, EPEquipmentDetail.projectTaskID>(projectID, taskId) || this.IsTaskUsed<EPActivityApprove, EPActivityApprove.projectID, EPActivityApprove.projectTaskID>(projectID, taskId) || this.IsTaskUsed<EPTimeCardSummary, EPTimeCardSummary.projectID, EPTimeCardSummary.projectTaskID>(projectID, taskId) || this.IsTaskUsed<TimeCardMaint.EPTimecardDetail, TimeCardMaint.EPTimecardDetail.projectID, TimeCardMaint.EPTimecardDetail.projectTaskID>(projectID, taskId) || this.IsTaskUsed<EPTimeCardItem, EPTimeCardItem.projectID, EPTimeCardItem.taskID>(projectID, taskId) || this.IsTaskUsed<EPExpenseClaimDetails, EPExpenseClaimDetails.contractID, EPExpenseClaimDetails.taskID>(projectID, taskId) || this.IsTaskUsed<PMChangeOrderBudget, PMChangeOrderBudget.projectID, PMChangeOrderBudget.projectTaskID, PMChangeOrderBudget.type>(projectID, taskId, "E") || this.IsTaskUsed<PMChangeOrderLine, PMChangeOrderLine.projectID, PMChangeOrderLine.taskID>(projectID, taskId);
  }
}
