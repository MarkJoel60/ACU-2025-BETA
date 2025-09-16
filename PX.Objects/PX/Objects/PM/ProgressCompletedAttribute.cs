// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProgressCompletedAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.PM;

public class ProgressCompletedAttribute : PXEventSubscriberAttribute, IPXFieldSelectingSubscriber
{
  public void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is PMProformaLine row))
      return;
    PMRevenueBudget pmRevenueBudget = PMRevenueBudget.PK.Find(sender.Graph, row.ProjectID, row.TaskID, row.AccountGroupID, row.CostCodeID, row.InventoryID);
    if (pmRevenueBudget == null)
      return;
    Decimal num1 = 0.0M;
    Decimal num2 = 0.0M;
    if (pmRevenueBudget.RevisedQty.GetValueOrDefault() != 0M)
    {
      PXGraph graph = sender.Graph;
      string uom1 = row.UOM;
      string uom2 = pmRevenueBudget.UOM;
      Decimal? nullable = row.Qty;
      Decimal valueOrDefault = nullable.GetValueOrDefault();
      ref Decimal local = ref num1;
      if (INUnitAttribute.TryConvertGlobalUnits(graph, uom1, uom2, valueOrDefault, INPrecision.QUANTITY, out local))
      {
        Decimal num3 = num1;
        nullable = pmRevenueBudget.RevisedQty;
        Decimal num4 = nullable.Value;
        num2 = Math.Round(num3 / num4, 2);
      }
    }
    e.ReturnValue = (object) num2;
  }
}
