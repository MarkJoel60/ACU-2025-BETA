// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ChangeOrderEntryCostBudgetCostCodeValidator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.PM;

public class ChangeOrderEntryCostBudgetCostCodeValidator : 
  CostCodeInBudgetValidator<ChangeOrderEntry, PMChangeOrderCostBudget, PMChangeOrderCostBudget.costCodeID>
{
  public static bool IsActive()
  {
    return CostCodeInBudgetValidator<ChangeOrderEntry, PMChangeOrderCostBudget, PMChangeOrderCostBudget.costCodeID>.IsExtensionEnabled();
  }

  protected override CostCodeInBudgetValidator<ChangeOrderEntry, PMChangeOrderCostBudget, PMChangeOrderCostBudget.costCodeID>.CostCodeContext GetCostCodeContext(
    PMChangeOrderCostBudget row)
  {
    return new CostCodeInBudgetValidator<ChangeOrderEntry, PMChangeOrderCostBudget, PMChangeOrderCostBudget.costCodeID>.CostCodeContext()
    {
      CostCodeID = row.CostCodeID,
      TaskID = row.ProjectTaskID,
      AccountGroupID = row.AccountGroupID,
      IsRowReleased = row.Released
    };
  }

  protected override string BudgetType => "E";
}
