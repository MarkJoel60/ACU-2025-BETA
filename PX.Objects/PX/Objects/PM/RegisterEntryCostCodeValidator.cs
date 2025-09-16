// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.RegisterEntryCostCodeValidator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.PM;

public class RegisterEntryCostCodeValidator : 
  CostCodeInBudgetValidator<RegisterEntry, PMTran, PMTran.costCodeID>
{
  public static bool IsActive()
  {
    return CostCodeInBudgetValidator<RegisterEntry, PMTran, PMTran.costCodeID>.IsExtensionEnabled();
  }

  protected override CostCodeInBudgetValidator<RegisterEntry, PMTran, PMTran.costCodeID>.CostCodeContext GetCostCodeContext(
    PMTran row)
  {
    return new CostCodeInBudgetValidator<RegisterEntry, PMTran, PMTran.costCodeID>.CostCodeContext()
    {
      CostCodeID = row.CostCodeID,
      TaskID = row.TaskID,
      AccountGroupID = row.AccountGroupID,
      IsRowReleased = row.Released
    };
  }

  protected override string BudgetType => "E";
}
