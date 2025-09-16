// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ExpenseClaimEntryCostCodeValidator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.EP;

#nullable disable
namespace PX.Objects.PM;

public class ExpenseClaimEntryCostCodeValidator : 
  CostCodeInBudgetValidator<ExpenseClaimEntry, EPExpenseClaimDetails, EPExpenseClaimDetails.costCodeID>
{
  public static bool IsActive()
  {
    return CostCodeInBudgetValidator<ExpenseClaimEntry, EPExpenseClaimDetails, EPExpenseClaimDetails.costCodeID>.IsExtensionEnabled();
  }

  protected override CostCodeInBudgetValidator<ExpenseClaimEntry, EPExpenseClaimDetails, EPExpenseClaimDetails.costCodeID>.CostCodeContext GetCostCodeContext(
    EPExpenseClaimDetails row)
  {
    return new CostCodeInBudgetValidator<ExpenseClaimEntry, EPExpenseClaimDetails, EPExpenseClaimDetails.costCodeID>.CostCodeContext()
    {
      CostCodeID = row.CostCodeID,
      TaskID = row.TaskID,
      AccountGroupID = row.ExpenseAccountID,
      IsRowReleased = row.Released
    };
  }

  protected override string BudgetType => "E";
}
