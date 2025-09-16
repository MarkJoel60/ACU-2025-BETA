// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SiteStatusLookup.AppointmentSiteStatusLookupExt
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS.SiteStatusLookup;

public class AppointmentSiteStatusLookupExt : 
  FSSiteStatusLookupExt<AppointmentEntry, FSAppointment, FSAppointmentDet>
{
  protected override FSAppointmentDet CreateNewLine(FSSiteStatusSelected line)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) line.InventoryID
    }));
    FSAppointmentDet copy1 = PXCache<FSAppointmentDet>.CreateCopy(((PXSelectBase<FSAppointmentDet>) this.Base.AppointmentDetails).Insert(new FSAppointmentDet()));
    copy1.LineType = !inventoryItem.StkItem.GetValueOrDefault() ? (inventoryItem.ItemType == "S" ? "SERVI" : "NSTKI") : "SLPRO";
    copy1.SiteID = line.SiteID ?? copy1.SiteID;
    copy1.InventoryID = line.InventoryID;
    copy1.SubItemID = line.SubItemID;
    copy1.UOM = line.SalesUnit;
    FSAppointmentDet copy2 = PXCache<FSAppointmentDet>.CreateCopy(((PXSelectBase<FSAppointmentDet>) this.Base.AppointmentDetails).Update(copy1));
    if (line.BillingRule == "TIME")
    {
      copy2.EstimatedDuration = ((PXGraph) this.Base).IsMobile ? line.EstimatedDuration : line.DurationSelected;
    }
    else
    {
      copy2.EstimatedQty = line.QtySelected;
      if (((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current.AreActualFieldsActive.GetValueOrDefault())
        copy2.ActualQty = line.QtySelected;
    }
    return ((PXSelectBase<FSAppointmentDet>) this.Base.AppointmentDetails).Update(copy2);
  }
}
