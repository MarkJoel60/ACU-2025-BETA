// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.GraphExtensions.RQRequisitionEntryExt.RQRequisitionSiteStatusLookupExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.PO;

#nullable disable
namespace PX.Objects.RQ.GraphExtensions.RQRequisitionEntryExt;

public class RQRequisitionSiteStatusLookupExt : 
  RQSiteStatusLookupBaseExt<RQRequisitionEntry, RQRequisition, RQRequisitionLine>
{
  protected override RQRequisitionLine CreateNewLine(RQSiteStatusSelected line)
  {
    return ((PXSelectBase<RQRequisitionLine>) this.Base.Lines).Insert(new RQRequisitionLine()
    {
      SiteID = line.SiteID,
      InventoryID = line.InventoryID,
      SubItemID = line.SubItemID,
      UOM = line.PurchaseUnit,
      OrderQty = line.QtySelected
    });
  }

  protected virtual void _(PX.Data.Events.RowInserted<POSiteStatusFilter> e)
  {
    if (e.Row == null || ((PXSelectBase<RQRequisition>) this.Base.Document).Current == null)
      return;
    e.Row.OnlyAvailable = new bool?(((PXSelectBase<RQRequisition>) this.Base.Document).Current.VendorID.HasValue);
    e.Row.VendorID = ((PXSelectBase<RQRequisition>) this.Base.Document).Current.VendorID;
  }

  protected override void _(PX.Data.Events.RowSelected<POSiteStatusFilter> e)
  {
    base._(e);
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<POSiteStatusFilter.onlyAvailable>(((PXSelectBase) this.ItemFilter).Cache, (object) e.Row, ((PXSelectBase<RQRequisition>) this.Base.Document).Current.VendorID.HasValue);
  }

  protected virtual void _(PX.Data.Events.RowSelected<RQRequisition> e)
  {
    if (e.Row == null)
      return;
    PXAction<RQRequisition> showItems = this.showItems;
    bool? nullable = e.Row.Hold;
    int num;
    if (nullable.GetValueOrDefault())
    {
      nullable = e.Row.Released;
      num = !nullable.Value ? 1 : 0;
    }
    else
      num = 0;
    ((PXAction) showItems).SetEnabled(num != 0);
  }
}
