// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProjectBudgetHistoryAccumAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.PM;

public class PMProjectBudgetHistoryAccumAttribute : PXAccumulatorAttribute
{
  public PMProjectBudgetHistoryAccumAttribute() => this._SingleRecord = true;

  protected virtual bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
  {
    if (!base.PrepareInsert(sender, row, columns))
      return false;
    PMProjectBudgetHistory projectBudgetHistory = (PMProjectBudgetHistory) row;
    columns.Update<PMProjectBudgetHistory.revisedBudgetQty>((object) projectBudgetHistory.RevisedBudgetQty, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMProjectBudgetHistory.curyRevisedBudgetAmt>((object) projectBudgetHistory.CuryRevisedBudgetAmt, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMProjectBudgetHistory.revisedBudgetAmt>((object) projectBudgetHistory.RevisedBudgetAmt, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMProjectBudgetHistory.type>((object) projectBudgetHistory.Type, (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<PMProjectBudgetHistory.curyInfoID>((object) projectBudgetHistory.CuryInfoID, (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<PMProjectBudgetHistory.uOM>((object) projectBudgetHistory.UOM, (PXDataFieldAssign.AssignBehavior) 0);
    return true;
  }
}
