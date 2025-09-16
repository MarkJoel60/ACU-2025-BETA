// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ChangeOrderEntryRevenueBudgetCostCodeValidator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.PM;

public class ChangeOrderEntryRevenueBudgetCostCodeValidator : 
  CostCodeInBudgetValidator<ChangeOrderEntry, PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.costCodeID>
{
  public static bool IsActive()
  {
    return CostCodeInBudgetValidator<ChangeOrderEntry, PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.costCodeID>.IsExtensionEnabled();
  }

  protected override CostCodeInBudgetValidator<ChangeOrderEntry, PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.costCodeID>.CostCodeContext GetCostCodeContext(
    PMChangeOrderRevenueBudget row)
  {
    return new CostCodeInBudgetValidator<ChangeOrderEntry, PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.costCodeID>.CostCodeContext()
    {
      CostCodeID = row.CostCodeID,
      TaskID = row.ProjectTaskID,
      AccountGroupID = row.AccountGroupID,
      IsRowReleased = row.Released
    };
  }

  protected override string BudgetType => "I";
}
