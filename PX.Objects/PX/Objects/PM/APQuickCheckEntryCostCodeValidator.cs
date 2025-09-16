// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.APQuickCheckEntryCostCodeValidator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.AP;

#nullable disable
namespace PX.Objects.PM;

public class APQuickCheckEntryCostCodeValidator : 
  CostCodeInBudgetValidator<APQuickCheckEntry, APTran, APTran.costCodeID>
{
  public static bool IsActive()
  {
    return CostCodeInBudgetValidator<APQuickCheckEntry, APTran, APTran.costCodeID>.IsExtensionEnabled();
  }

  protected override CostCodeInBudgetValidator<APQuickCheckEntry, APTran, APTran.costCodeID>.CostCodeContext GetCostCodeContext(
    APTran row)
  {
    return new CostCodeInBudgetValidator<APQuickCheckEntry, APTran, APTran.costCodeID>.CostCodeContext()
    {
      CostCodeID = row.CostCodeID,
      TaskID = row.TaskID,
      AccountID = row.AccountID,
      IsRowReleased = row.Released
    };
  }

  protected override string BudgetType => "E";
}
