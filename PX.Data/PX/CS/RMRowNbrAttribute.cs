// Decompiled with JetBrains decompiler
// Type: PX.CS.RMRowNbrAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Linq;

#nullable disable
namespace PX.CS;

public class RMRowNbrAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldDefaultingSubscriber,
  IPXRowDeletedSubscriber
{
  void IPXRowDeletedSubscriber.RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    RMRow row1 = (RMRow) e.Row;
    RMRowSet row2 = PXParentAttribute.SelectParent<RMRowSet>(sender, (object) row1);
    if (row1 == null)
      return;
    short? nullable1 = row1.RowNbr;
    if (!nullable1.HasValue || row2 == null)
      return;
    nullable1 = row2.RowCntr;
    int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    nullable1 = row1.RowNbr;
    int? nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    if (!(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue))
      return;
    RMRowSet rmRowSet = row2;
    nullable1 = rmRowSet.RowCntr;
    short? nullable4 = nullable1;
    rmRowSet.RowCntr = nullable4.HasValue ? new short?((short) ((int) nullable4.GetValueOrDefault() - 1)) : new short?();
    sender.Graph.Caches[typeof (RMRowSet)].MarkUpdated((object) row2);
  }

  void IPXFieldDefaultingSubscriber.FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    RMRow row1 = (RMRow) e.Row;
    RMRowSet row2 = PXParentAttribute.SelectParent<RMRowSet>(sender, (object) row1);
    if (row1 == null || row2 == null)
      return;
    short? rowCntr = row2.RowCntr;
    if (!rowCntr.HasValue)
      return;
    RMRow rmRow1 = new RMRow();
    rmRow1.RowSetCode = row2.RowSetCode;
    rowCntr = row2.RowCntr;
    rmRow1.RowNbr = rowCntr.HasValue ? new short?((short) ((int) rowCntr.GetValueOrDefault() + 1)) : new short?();
    RMRow rmRow2 = rmRow1;
    if (sender.Locate((object) rmRow2) != null)
      row2.RowCntr = sender.Cached.Cast<RMRow>().OrderByDescending<RMRow, short?>((Func<RMRow, short?>) (c => c.RowNbr)).First<RMRow>().RowNbr;
    RMRowSet rmRowSet = row2;
    rowCntr = rmRowSet.RowCntr;
    short? nullable = rowCntr;
    rmRowSet.RowCntr = nullable.HasValue ? new short?((short) ((int) nullable.GetValueOrDefault() + 1)) : new short?();
    e.NewValue = (object) row2.RowCntr;
    sender.Graph.Caches[typeof (RMRowSet)].MarkUpdated((object) row2);
  }
}
