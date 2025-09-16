// Decompiled with JetBrains decompiler
// Type: PX.Objects.SM.ChartSettingsMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Dashboards.Widgets;
using PX.Data;
using PX.Olap;
using System;

#nullable disable
namespace PX.Objects.SM;

public class ChartSettingsMaintExt : PXGraphExtension<ChartSettingsMaint>
{
  [PXOverride]
  public virtual SortType DetermineSortType(string field, Func<string, SortType> del)
  {
    SortType sortType;
    return PivotMaintExt.TryDetermineSortType(((InquiryBasedWidgetMaint<ChartSettingsMaint, ChartSettings>) this.Base).DataScreen, field, out sortType) ? sortType : del(field);
  }

  public virtual void ChartSettings_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!e.ExternalCall)
      return;
    ChartSettings row = (ChartSettings) e.Row;
    ChartSettings oldRow = (ChartSettings) e.OldRow;
    if (row.CategoryField != oldRow.CategoryField && PivotMaintExt.IsFinPeriod(((InquiryBasedWidgetMaint<ChartSettingsMaint, ChartSettings>) this.Base).DataScreen, row.CategoryField))
    {
      sender.SetValue<ChartSettings.categorySortType>((object) row, (object) 0);
      sender.SetValue<ChartSettings.categorySortOrder>((object) row, (object) 0);
    }
    if (!(row.SeriesField != oldRow.SeriesField) || !PivotMaintExt.IsFinPeriod(((InquiryBasedWidgetMaint<ChartSettingsMaint, ChartSettings>) this.Base).DataScreen, row.SeriesField))
      return;
    sender.SetValue<ChartSettings.seriesSortType>((object) row, (object) 0);
    sender.SetValue<ChartSettings.seriesSortOrder>((object) row, (object) 0);
  }
}
