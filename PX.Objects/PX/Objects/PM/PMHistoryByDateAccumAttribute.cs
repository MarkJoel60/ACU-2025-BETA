// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMHistoryByDateAccumAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.PM;

public class PMHistoryByDateAccumAttribute : PXAccumulatorAttribute
{
  public PMHistoryByDateAccumAttribute() => this._SingleRecord = true;

  protected virtual bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
  {
    if (!base.PrepareInsert(sender, row, columns))
      return false;
    PMHistoryByDate pmHistoryByDate = (PMHistoryByDate) row;
    columns.Update<PMHistoryByDate.actualQty>((object) pmHistoryByDate.ActualQty, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMHistoryByDate.curyActualAmount>((object) pmHistoryByDate.CuryActualAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMHistoryByDate.actualAmount>((object) pmHistoryByDate.ActualAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMHistoryByDate.year>((object) pmHistoryByDate.Year, (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<PMHistoryByDate.quarter>((object) pmHistoryByDate.Quarter, (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<PMHistoryByDate.month>((object) pmHistoryByDate.Month, (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<PMHistoryByDate.week>((object) pmHistoryByDate.Week, (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<PMHistoryByDate.weekOfYear>((object) pmHistoryByDate.WeekOfYear, (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<PMHistoryByDate.day>((object) pmHistoryByDate.Day, (PXDataFieldAssign.AssignBehavior) 4);
    return true;
  }
}
