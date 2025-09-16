// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.Projections.INLocationStatusByCostLayerType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN.DAC.Projections;

/// <exclude />
[PXCacheName("IN Location Status by Cost Layer Type")]
[INLocationStatusByCostLayerTypeProjection]
public class INLocationStatusByCostLayerType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Inventory(IsKey = true, BqlField = typeof (INLocationStatusByCostCenter.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID { get; set; }

  [SubItem(IsKey = true, BqlField = typeof (INLocationStatusByCostCenter.subItemID))]
  public virtual int? SubItemID { get; set; }

  [Site(IsKey = true, BqlField = typeof (INLocationStatusByCostCenter.siteID))]
  public virtual int? SiteID { get; set; }

  [Location(typeof (INLocationStatusByCostLayerType.siteID), IsKey = true, BqlField = typeof (INLocationStatusByCostCenter.locationID))]
  public virtual int? LocationID { get; set; }

  [PXDBCalced(typeof (IsNull<INCostCenter.costLayerType, PX.Objects.IN.CostLayerType.normal>), typeof (string))]
  [PXString(1, IsKey = true)]
  [PX.Objects.IN.CostLayerType.List]
  [PXUIField(DisplayName = "Cost Layer Type", FieldClass = "CostLayerType")]
  public virtual 
  #nullable disable
  string CostLayerType { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyOnHand))]
  public virtual Decimal? QtyOnHand { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyAvail))]
  [PXUIField(DisplayName = "Qty. Available")]
  public virtual Decimal? QtyAvail { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyHardAvail))]
  [PXUIField(DisplayName = "Qty. Hard Available")]
  public virtual Decimal? QtyHardAvail { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyActual))]
  [PXUIField(DisplayName = "Qty. Available for Issue")]
  public virtual Decimal? QtyActual { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyInTransit))]
  [PXUIField(DisplayName = "Qty. In-Transit")]
  public virtual Decimal? QtyInTransit { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyInTransitToSO))]
  [PXUIField(DisplayName = "Qty. In Transit to SO")]
  public virtual Decimal? QtyInTransitToSO { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyPOPrepared))]
  [PXUIField(DisplayName = "Qty. PO Prepared")]
  public virtual Decimal? QtyPOPrepared { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyPOOrders))]
  [PXUIField(DisplayName = "Qty. Purchase Orders")]
  public virtual Decimal? QtyPOOrders { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyPOReceipts))]
  [PXUIField(DisplayName = "Qty. Purchase Receipts")]
  public virtual Decimal? QtyPOReceipts { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyFSSrvOrdBooked))]
  [PXUIField(DisplayName = "Qty. FS Booked", FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFSSrvOrdBooked { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyFSSrvOrdAllocated))]
  [PXUIField(DisplayName = "Qty. FS Allocated", FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFSSrvOrdAllocated { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyFSSrvOrdPrepared))]
  [PXUIField(DisplayName = "Qty. FS Prepared", FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFSSrvOrdPrepared { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtySOBackOrdered))]
  [PXUIField(DisplayName = "Qty. SO Backordered")]
  public virtual Decimal? QtySOBackOrdered { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtySOPrepared))]
  [PXUIField(DisplayName = "Qty. SO Prepared")]
  public virtual Decimal? QtySOPrepared { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtySOBooked))]
  [PXUIField(DisplayName = "Qty. SO Booked")]
  public virtual Decimal? QtySOBooked { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtySOShipped))]
  [PXUIField(DisplayName = "Qty. SO Shipped")]
  public virtual Decimal? QtySOShipped { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtySOShipping))]
  [PXUIField(DisplayName = "Qty. SO Shipping")]
  public virtual Decimal? QtySOShipping { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyINIssues))]
  [PXUIField(DisplayName = "Qty On Inventory Issues")]
  public virtual Decimal? QtyINIssues { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyINReceipts))]
  [PXUIField(DisplayName = "Qty On Inventory Receipts")]
  public virtual Decimal? QtyINReceipts { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyINAssemblyDemand))]
  [PXUIField(DisplayName = "Qty Demanded by Kit Assembly")]
  public virtual Decimal? QtyINAssemblyDemand { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyINAssemblySupply))]
  [PXUIField(DisplayName = "Qty On Kit Assembly")]
  public virtual Decimal? QtyINAssemblySupply { get; set; }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.inventoryID>
  {
  }

  public abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLocationStatusByCostLayerType.siteID>
  {
  }

  public abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.locationID>
  {
  }

  public abstract class costLayerType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.costLayerType>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtyOnHand>
  {
  }

  public abstract class qtyAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtyAvail>
  {
  }

  public abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtyHardAvail>
  {
  }

  public abstract class qtyActual : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtyActual>
  {
  }

  public abstract class qtyInTransit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtyInTransit>
  {
  }

  public abstract class qtyInTransitToSO : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtyInTransitToSO>
  {
  }

  public abstract class qtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtyPOPrepared>
  {
  }

  public abstract class qtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtyPOOrders>
  {
  }

  public abstract class qtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtyPOReceipts>
  {
  }

  public abstract class qtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtyFSSrvOrdBooked>
  {
  }

  public abstract class qtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtyFSSrvOrdAllocated>
  {
  }

  public abstract class qtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtyFSSrvOrdPrepared>
  {
  }

  public abstract class qtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtySOBackOrdered>
  {
  }

  public abstract class qtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtySOPrepared>
  {
  }

  public abstract class qtySOBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtySOBooked>
  {
  }

  public abstract class qtySOShipped : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtySOShipped>
  {
  }

  public abstract class qtySOShipping : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtySOShipping>
  {
  }

  public abstract class qtyINIssues : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtyINIssues>
  {
  }

  public abstract class qtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtyINReceipts>
  {
  }

  public abstract class qtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtyINAssemblyDemand>
  {
  }

  public abstract class qtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusByCostLayerType.qtyINAssemblySupply>
  {
  }
}
