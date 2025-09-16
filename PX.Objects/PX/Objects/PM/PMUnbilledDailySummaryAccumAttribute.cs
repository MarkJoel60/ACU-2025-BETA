// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMUnbilledDailySummaryAccumAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

public class PMUnbilledDailySummaryAccumAttribute : PXAccumulatorAttribute
{
  public PMUnbilledDailySummaryAccumAttribute() => this._SingleRecord = true;

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.RowPersistingEvents rowPersisting = sender.Graph.RowPersisting;
    Type itemType = sender.GetItemType();
    PMUnbilledDailySummaryAccumAttribute summaryAccumAttribute = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) summaryAccumAttribute, __vmethodptr(summaryAccumAttribute, RowPersisting));
    rowPersisting.AddHandler(itemType, pxRowPersisting);
  }

  protected virtual bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
  {
    if (!base.PrepareInsert(sender, row, columns))
      return false;
    PMUnbilledDailySummaryAccum dailySummaryAccum = (PMUnbilledDailySummaryAccum) row;
    columns.Update<PMUnbilledDailySummaryAccum.billable>((object) dailySummaryAccum.Billable, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMUnbilledDailySummaryAccum.nonBillable>((object) dailySummaryAccum.NonBillable, (PXDataFieldAssign.AssignBehavior) 1);
    return true;
  }

  protected virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    PMUnbilledDailySummaryAccum row = e.Row as PMUnbilledDailySummaryAccum;
    if (row.Date.Value.TimeOfDay != TimeSpan.Zero)
    {
      string str1 = "Date";
      string str2 = "PMUnbilledDailySummaryAccum";
      throw new PXRowPersistingException(str1, (object) row.Date, "The {0} value of the {1} table is incorrect because it contains a time component. The format must be yyyy-mm-dd.", new object[2]
      {
        (object) str1,
        (object) str2
      });
    }
  }
}
