// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProformaEntryTransactCostCodeValidator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.PM;

public class ProformaEntryTransactCostCodeValidator : 
  CostCodeInBudgetValidator<ProformaEntry, PMProformaTransactLine, PMProformaLine.costCodeID>
{
  public static bool IsActive()
  {
    return CostCodeInBudgetValidator<ProformaEntry, PMProformaTransactLine, PMProformaLine.costCodeID>.IsExtensionEnabled();
  }

  protected override CostCodeInBudgetValidator<ProformaEntry, PMProformaTransactLine, PMProformaLine.costCodeID>.CostCodeContext GetCostCodeContext(
    PMProformaTransactLine row)
  {
    return new CostCodeInBudgetValidator<ProformaEntry, PMProformaTransactLine, PMProformaLine.costCodeID>.CostCodeContext()
    {
      CostCodeID = row.CostCodeID,
      TaskID = row.TaskID,
      AccountID = row.AccountID,
      IsRowReleased = row.Released
    };
  }

  protected override string BudgetType => "I";
}
