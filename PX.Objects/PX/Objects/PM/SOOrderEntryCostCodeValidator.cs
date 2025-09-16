// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.SOOrderEntryCostCodeValidator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.SO;

#nullable disable
namespace PX.Objects.PM;

public class SOOrderEntryCostCodeValidator : 
  CostCodeInBudgetValidator<SOOrderEntry, SOLine, SOLine.costCodeID>
{
  public static bool IsActive()
  {
    return CostCodeInBudgetValidator<SOOrderEntry, SOLine, SOLine.costCodeID>.IsExtensionEnabled();
  }

  protected override CostCodeInBudgetValidator<SOOrderEntry, SOLine, SOLine.costCodeID>.CostCodeContext GetCostCodeContext(
    SOLine row)
  {
    return new CostCodeInBudgetValidator<SOOrderEntry, SOLine, SOLine.costCodeID>.CostCodeContext()
    {
      CostCodeID = row.CostCodeID,
      TaskID = row.TaskID,
      AccountGroupID = row.SalesAcctID
    };
  }

  protected override string BudgetType => "I";
}
