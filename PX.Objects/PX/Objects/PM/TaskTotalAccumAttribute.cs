// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TaskTotalAccumAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public class TaskTotalAccumAttribute : PXAccumulatorAttribute
{
  public TaskTotalAccumAttribute() => this._SingleRecord = true;

  protected virtual bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
  {
    if (!base.PrepareInsert(sender, row, columns))
      return false;
    PMTaskTotal pmTaskTotal = (PMTaskTotal) row;
    columns.Update<PMTaskTotal.curyAsset>((object) pmTaskTotal.CuryAsset, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMTaskTotal.asset>((object) pmTaskTotal.Asset, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMTaskTotal.curyLiability>((object) pmTaskTotal.CuryLiability, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMTaskTotal.liability>((object) pmTaskTotal.Liability, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMTaskTotal.curyIncome>((object) pmTaskTotal.CuryIncome, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMTaskTotal.income>((object) pmTaskTotal.Income, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMTaskTotal.curyExpense>((object) pmTaskTotal.CuryExpense, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMTaskTotal.expense>((object) pmTaskTotal.Expense, (PXDataFieldAssign.AssignBehavior) 1);
    return true;
  }
}
