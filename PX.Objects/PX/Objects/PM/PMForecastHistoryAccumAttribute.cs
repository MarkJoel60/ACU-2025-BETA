// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMForecastHistoryAccumAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.PM;

public class PMForecastHistoryAccumAttribute : PXAccumulatorAttribute
{
  public PMForecastHistoryAccumAttribute() => this._SingleRecord = true;

  protected virtual bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
  {
    if (!base.PrepareInsert(sender, row, columns))
      return false;
    PMForecastHistory pmForecastHistory = (PMForecastHistory) row;
    columns.Update<PMForecastHistory.actualQty>((object) pmForecastHistory.ActualQty, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMForecastHistory.curyActualAmount>((object) pmForecastHistory.CuryActualAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMForecastHistory.actualAmount>((object) pmForecastHistory.ActualAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMForecastHistory.changeOrderQty>((object) pmForecastHistory.ChangeOrderQty, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMForecastHistory.curyChangeOrderAmount>((object) pmForecastHistory.CuryChangeOrderAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMForecastHistory.draftChangeOrderQty>((object) pmForecastHistory.DraftChangeOrderQty, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMForecastHistory.curyDraftChangeOrderAmount>((object) pmForecastHistory.CuryDraftChangeOrderAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMForecastHistory.curyArAmount>((object) pmForecastHistory.CuryArAmount, (PXDataFieldAssign.AssignBehavior) 1);
    return true;
  }
}
