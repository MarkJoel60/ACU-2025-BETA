// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ReclassifyTransactionsProcessCostCodeValidator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.GL.Reclassification.Common;
using PX.Objects.GL.Reclassification.UI;

#nullable disable
namespace PX.Objects.PM;

public class ReclassifyTransactionsProcessCostCodeValidator : 
  CostCodeInBudgetValidator<ReclassifyTransactionsProcess, GLTranForReclassification, GLTranForReclassification.newCostCodeID>
{
  public static bool IsActive()
  {
    return CostCodeInBudgetValidator<ReclassifyTransactionsProcess, GLTranForReclassification, GLTranForReclassification.newCostCodeID>.IsExtensionEnabled();
  }

  protected override CostCodeInBudgetValidator<ReclassifyTransactionsProcess, GLTranForReclassification, GLTranForReclassification.newCostCodeID>.CostCodeContext GetCostCodeContext(
    GLTranForReclassification row)
  {
    return new CostCodeInBudgetValidator<ReclassifyTransactionsProcess, GLTranForReclassification, GLTranForReclassification.newCostCodeID>.CostCodeContext()
    {
      CostCodeID = row.NewCostCodeID,
      TaskID = row.NewTaskID,
      AccountID = row.NewAccountID,
      IsRowReleased = row.Released
    };
  }
}
