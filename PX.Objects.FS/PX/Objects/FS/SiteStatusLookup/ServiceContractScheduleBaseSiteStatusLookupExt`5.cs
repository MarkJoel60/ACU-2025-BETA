// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SiteStatusLookup.ServiceContractScheduleBaseSiteStatusLookupExt`5
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS.SiteStatusLookup;

public abstract class ServiceContractScheduleBaseSiteStatusLookupExt<TScheduleGraph, TPrimary, TScheduleID, TEntityID, TCustomerID> : 
  FSSiteStatusLookupExt<TScheduleGraph, FSSchedule, FSScheduleDet>
  where TScheduleGraph : ServiceContractScheduleEntryBase<TScheduleGraph, TPrimary, TScheduleID, TEntityID, TCustomerID>
  where TPrimary : class, IBqlTable, new()
  where TScheduleID : IBqlField
  where TEntityID : IBqlField
  where TCustomerID : IBqlField
{
  protected override FSScheduleDet CreateNewLine(FSSiteStatusSelected line)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) (object) this.Base, new object[1]
    {
      (object) line.InventoryID
    }));
    FSScheduleDet copy1 = PXCache<FSScheduleDet>.CreateCopy(((PXSelectBase<FSScheduleDet>) this.Base.ScheduleDetails).Insert(new FSScheduleDet()));
    copy1.LineType = !inventoryItem.StkItem.GetValueOrDefault() ? (inventoryItem.ItemType == "S" ? "SERVI" : "NSTKI") : "SLPRO";
    copy1.InventoryID = line.InventoryID;
    FSScheduleDet copy2 = PXCache<FSScheduleDet>.CreateCopy(((PXSelectBase<FSScheduleDet>) this.Base.ScheduleDetails).Update(copy1));
    if (line.BillingRule == "TIME")
      copy2.EstimatedDuration = line.DurationSelected;
    else
      copy2.Qty = line.QtySelected;
    return ((PXSelectBase<FSScheduleDet>) this.Base.ScheduleDetails).Update(copy2);
  }
}
