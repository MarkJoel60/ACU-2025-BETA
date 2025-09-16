// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.PM.Services.ProjectTaskTypeUsageValidationServiceBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CN.Common.Extensions;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.PM.Services;

public abstract class ProjectTaskTypeUsageValidationServiceBase : ProjectTaskValidationServiceBase
{
  public void ValidateProjectTaskType(PXCache cache, PMTask projectTask)
  {
    if (cache.GetStatus((object) projectTask) != 1)
      return;
    if (projectTask.Type == "Cost" && this.IsTaskUsedInRevenueDocument(projectTask.ProjectID, projectTask.TaskID))
      cache.RaiseException<PMTask.type>((object) projectTask, $"Task type cannot be changed. The Task is already used in at least one {"Revenue"} related document.", (object) projectTask.Type);
    if (!(projectTask.Type == "Rev") || !this.IsTaskUsedInCostDocument(projectTask.ProjectID, projectTask.TaskID))
      return;
    cache.RaiseException<PMTask.type>((object) projectTask, $"Task type cannot be changed. The Task is already used in at least one {"Cost"} related document.", (object) projectTask.Type);
  }

  protected override bool IsTaskUsedInRevenueDocument(int? projectID, int? taskId)
  {
    return this.IsTaskUsed<PMBudget, PMBudget.projectID, PMBudget.projectTaskID, PMBudget.type>(projectID, taskId, "I") || this.IsTaskUsed<PMChangeOrderBudget, PMChangeOrderBudget.projectID, PMChangeOrderBudget.projectTaskID, PMChangeOrderBudget.type>(projectID, taskId, "I") || this.IsTaskUsed<ARTran, ARTran.projectID, ARTran.taskID>(projectID, taskId) || this.IsTaskUsed<PMProformaProgressLine, PMProformaLine.projectID, PMProformaLine.taskID>(projectID, taskId) || this.IsTaskUsed<PMProformaTransactLine, PMProformaLine.projectID, PMProformaLine.taskID>(projectID, taskId);
  }
}
