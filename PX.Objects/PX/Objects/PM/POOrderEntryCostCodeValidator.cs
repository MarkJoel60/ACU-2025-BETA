// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.POOrderEntryCostCodeValidator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.PO;

#nullable disable
namespace PX.Objects.PM;

public class POOrderEntryCostCodeValidator : 
  CostCodeInBudgetValidator<POOrderEntry, POLine, POLine.costCodeID>
{
  public static bool IsActive()
  {
    return CostCodeInBudgetValidator<POOrderEntry, POLine, POLine.costCodeID>.IsExtensionEnabled();
  }

  protected override CostCodeInBudgetValidator<POOrderEntry, POLine, POLine.costCodeID>.CostCodeContext GetCostCodeContext(
    POLine row)
  {
    return new CostCodeInBudgetValidator<POOrderEntry, POLine, POLine.costCodeID>.CostCodeContext()
    {
      CostCodeID = row.CostCodeID,
      TaskID = row.TaskID,
      AccountID = row.ExpenseAcctID
    };
  }

  protected override string BudgetType => "E";
}
