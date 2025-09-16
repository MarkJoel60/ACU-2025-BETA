// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SiteStatusLookup.ServiceOrderSiteStatusLookupExt
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS.SiteStatusLookup;

public class ServiceOrderSiteStatusLookupExt : 
  FSSiteStatusLookupExt<ServiceOrderEntry, FSServiceOrder, FSSODet>
{
  protected override FSSODet CreateNewLine(FSSiteStatusSelected line)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) line.InventoryID
    }));
    FSSODet fssoDet = new FSSODet()
    {
      LineType = !inventoryItem.StkItem.GetValueOrDefault() ? (inventoryItem.ItemType == "S" ? "SERVI" : "NSTKI") : "SLPRO"
    };
    fssoDet.SiteID = line.SiteID ?? fssoDet.SiteID;
    fssoDet.InventoryID = line.InventoryID;
    fssoDet.SubItemID = line.SubItemID;
    fssoDet.UOM = line.SalesUnit;
    FSSODet copy = PXCache<FSSODet>.CreateCopy(((PXSelectBase<FSSODet>) this.Base.ServiceOrderDetails).Update(fssoDet));
    if (line.BillingRule == "TIME")
      copy.EstimatedDuration = line.DurationSelected;
    else
      copy.Qty = line.QtySelected;
    return ((PXSelectBase<FSSODet>) this.Base.ServiceOrderDetails).Update(copy);
  }
}
