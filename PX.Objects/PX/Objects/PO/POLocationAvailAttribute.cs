// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLocationAvailAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.PO;

public class POLocationAvailAttribute(
  Type InventoryType,
  Type SubItemType,
  Type costCenterType,
  Type SiteIDType,
  Type TranType,
  Type InvtMultType) : LocationAvailAttribute(InventoryType, SubItemType, costCenterType, SiteIDType, TranType, InvtMultType)
{
  public override void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is POReceiptLine row))
      return;
    int? nullable;
    if (POLineType.IsStock(row.LineType) && row.POType != null && row.PONbr != null)
    {
      nullable = row.POLineNbr;
      if (nullable.HasValue && row.POType != "PD")
      {
        POLine poLine = PXResultset<POLine>.op_Implicit(PXSelectBase<POLine, PXSelect<POLine, Where<POLine.orderType, Equal<Required<POLine.orderType>>, And<POLine.orderNbr, Equal<Required<POLine.orderNbr>>, And<POLine.lineNbr, Equal<Required<POLine.lineNbr>>>>>>.Config>.Select(sender.Graph, new object[3]
        {
          (object) row.POType,
          (object) row.PONbr,
          (object) row.POLineNbr
        }));
        if (poLine != null)
        {
          nullable = poLine.TaskID;
          if (nullable.HasValue)
          {
            INLocation inLocation = PXResultset<INLocation>.op_Implicit(PXSelectBase<INLocation, PXSelect<INLocation, Where<INLocation.siteID, Equal<Required<INLocation.siteID>>, And<INLocation.taskID, Equal<Required<INLocation.taskID>>>>>.Config>.Select(sender.Graph, new object[2]
            {
              (object) row.SiteID,
              (object) poLine.TaskID
            }));
            if (inLocation != null)
            {
              e.NewValue = (object) inLocation.LocationID;
              return;
            }
            PMProject pmProject = PMProject.PK.Find(sender.Graph, poLine.ProjectID);
            if (pmProject != null && !pmProject.NonProject.GetValueOrDefault() && pmProject.AccountingMode == "L")
            {
              e.NewValue = (object) null;
              return;
            }
            goto label_16;
          }
          goto label_16;
        }
        goto label_16;
      }
    }
    if (POLineType.IsNonStockNonServiceNonDropShip(row.LineType))
    {
      nullable = row.SiteID;
      if (nullable.HasValue)
      {
        INSite inSite = INSite.PK.Find(sender.Graph, row.SiteID);
        nullable = inSite.ReceiptLocationID;
        if (nullable.HasValue)
        {
          e.NewValue = (object) inSite.ReceiptLocationID;
          return;
        }
        if (!PXAccess.FeatureInstalled<FeaturesSet.advancedFulfillment>())
          return;
        e.NewValue = (object) inSite.NonStockPickingLocationID;
        return;
      }
    }
label_16:
    base.FieldDefaulting(sender, e);
  }
}
