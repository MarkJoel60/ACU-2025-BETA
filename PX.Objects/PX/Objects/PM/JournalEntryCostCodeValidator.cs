// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.JournalEntryCostCodeValidator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.GL;

#nullable disable
namespace PX.Objects.PM;

public class JournalEntryCostCodeValidator : 
  CostCodeInBudgetValidator<JournalEntry, GLTran, GLTran.costCodeID>
{
  public static bool IsActive()
  {
    return CostCodeInBudgetValidator<JournalEntry, GLTran, GLTran.costCodeID>.IsExtensionEnabled();
  }

  protected override CostCodeInBudgetValidator<JournalEntry, GLTran, GLTran.costCodeID>.CostCodeContext GetCostCodeContext(
    GLTran row)
  {
    return new CostCodeInBudgetValidator<JournalEntry, GLTran, GLTran.costCodeID>.CostCodeContext()
    {
      CostCodeID = row.CostCodeID,
      TaskID = row.TaskID,
      AccountID = row.AccountID,
      IsRowReleased = row.Released
    };
  }
}
