// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.CostCodeInBudgetValidator`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.PM;

public abstract class CostCodeInBudgetValidator<TGraph, TRow, TCostCodeField> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TRow : class, IBqlTable, new()
  where TCostCodeField : class, IBqlField
{
  public static bool IsExtensionEnabled() => PXAccess.FeatureInstalled<FeaturesSet.costCodes>();

  protected abstract CostCodeInBudgetValidator<TGraph, TRow, TCostCodeField>.CostCodeContext GetCostCodeContext(
    TRow row);

  protected virtual bool SkipVerificationForDefaultCostCode => false;

  protected virtual string BudgetType => (string) null;

  protected virtual void _(PX.Data.Events.RowSelected<TRow> e)
  {
    TRow row = e.Row;
    if ((object) row == null)
      return;
    this.ValidateCostCode(row, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<TRow>>) e).Cache, new int?(), true);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<TRow, TCostCodeField> e)
  {
    TRow row = e.Row;
    if ((object) row == null)
      return;
    this.ValidateCostCode(row, ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<TRow, TCostCodeField>>) e).Cache, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TRow, TCostCodeField>, TRow, object>) e).NewValue, false);
  }

  protected virtual void ValidateCostCode(
    TRow row,
    PXCache cache,
    int? newCostCodeID,
    bool skipReleased)
  {
    CostCodeInBudgetValidator<TGraph, TRow, TCostCodeField>.CostCodeContext costCodeContext = this.GetCostCodeContext(row);
    if (newCostCodeID.HasValue)
      costCodeContext.CostCodeID = newCostCodeID;
    int? nullable1 = costCodeContext.CostCodeID;
    if (!nullable1.HasValue)
      return;
    nullable1 = costCodeContext.TaskID;
    if (!nullable1.HasValue)
      return;
    nullable1 = costCodeContext.AccountID;
    if (!nullable1.HasValue)
    {
      nullable1 = costCodeContext.AccountGroupID;
      if (!nullable1.HasValue)
        return;
    }
    bool? nullable2 = costCodeContext.IsRowReleased;
    if (nullable2.GetValueOrDefault() & skipReleased)
      return;
    PMCostCode costCode = PMCostCode.PK.Find((PXGraph) this.Base, costCodeContext.CostCodeID);
    int num;
    if (costCode == null)
    {
      num = 1;
    }
    else
    {
      nullable2 = costCode.IsActive;
      num = !nullable2.GetValueOrDefault() ? 1 : 0;
    }
    if (num != 0)
      return;
    if (this.SkipVerificationForDefaultCostCode)
    {
      nullable2 = costCode.IsDefault;
      if (nullable2.GetValueOrDefault())
        return;
    }
    PMAccountGroup accountGroup = this.GetAccountGroup(costCodeContext);
    PMTask task = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXViewOf<PMTask>.BasedOn<SelectFromBase<PMTask, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMTask.taskID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) costCodeContext.TaskID
    }));
    // ISSUE: variable of a boxed type
    __Boxed<TGraph> graph1 = (object) this.Base;
    int? projectID;
    if (task == null)
    {
      nullable1 = new int?();
      projectID = nullable1;
    }
    else
      projectID = task.ProjectID;
    if (!this.HasCostCodeInProjectBudgetLevel(PMProject.PK.Find((PXGraph) graph1, projectID), accountGroup))
      return;
    // ISSUE: variable of a boxed type
    __Boxed<TGraph> graph2 = (object) this.Base;
    int? taskId = costCodeContext.TaskID;
    int? accountGroupID;
    if (accountGroup == null)
    {
      nullable1 = new int?();
      accountGroupID = nullable1;
    }
    else
      accountGroupID = accountGroup.GroupID;
    string budgetType = this.BudgetType;
    int? costCodeId = costCodeContext.CostCodeID;
    if (PXResultset<PMBudgetedCostCode>.op_Implicit(CostCodeSelectorAttribute.GetProjectSpecificRecords((PXGraph) graph2, taskId, accountGroupID, budgetType, costCodeId)) != null)
      return;
    string validationMessage = this.GetValidationMessage(costCode, task, accountGroup, cache, row);
    this.ShowValidationMessage(cache, row, costCodeContext.CostCodeID, validationMessage);
  }

  protected virtual PMAccountGroup GetAccountGroup(
    CostCodeInBudgetValidator<TGraph, TRow, TCostCodeField>.CostCodeContext costCodeContext)
  {
    return PMAccountGroup.PK.Find((PXGraph) this.Base, costCodeContext.AccountGroupID) ?? PMAccountGroup.PK.Find((PXGraph) this.Base, (int?) PX.Objects.GL.Account.PK.Find((PXGraph) this.Base, costCodeContext.AccountID)?.AccountGroupID);
  }

  protected virtual bool HasCostCodeInProjectBudgetLevel(
    PMProject project,
    PMAccountGroup accountGroup)
  {
    return accountGroup?.Type == "I" ? project?.BudgetLevel == "C" || project?.BudgetLevel == "D" : project?.CostBudgetLevel == "C" || project?.CostBudgetLevel == "D";
  }

  protected virtual string GetValidationMessage(
    PMCostCode costCode,
    PMTask task,
    PMAccountGroup accountGroup,
    PXCache cache,
    TRow row)
  {
    return $"The {CostCodeSelectorAttribute.FormatValueByMask<TCostCodeField>(cache, (object) row, costCode.CostCodeCD)} cost code is not present in the project budget with the combination of the {task.TaskCD.Trim()} project task and the {accountGroup?.GroupCD.Trim()} account group.";
  }

  protected virtual void ShowValidationMessage(
    PXCache cache,
    TRow row,
    int? costCodeID,
    string message)
  {
    cache.RaiseExceptionHandling<TCostCodeField>((object) row, (object) costCodeID, (Exception) new PXSetPropertyException(message, (PXErrorLevel) 2));
  }

  public class CostCodeContext
  {
    public int? CostCodeID { get; set; }

    public int? TaskID { get; set; }

    public int? AccountID { get; set; }

    public int? AccountGroupID { get; set; }

    public bool? IsRowReleased { get; set; }
  }
}
