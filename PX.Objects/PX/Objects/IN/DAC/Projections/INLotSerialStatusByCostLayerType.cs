// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.Projections.INLotSerialStatusByCostLayerType
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
[PXCacheName("IN Lot/Serial Status by Cost Layer Type")]
[INLotSerialStatusByCostLayerTypeProjection]
public class INLotSerialStatusByCostLayerType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Inventory(IsKey = true, BqlField = typeof (INLotSerialStatusByCostCenter.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID { get; set; }

  [SubItem(IsKey = true, BqlField = typeof (INLotSerialStatusByCostCenter.subItemID))]
  public virtual int? SubItemID { get; set; }

  [Site(IsKey = true, BqlField = typeof (INLotSerialStatusByCostCenter.siteID))]
  public virtual int? SiteID { get; set; }

  [Location(typeof (INLotSerialStatusByCostLayerType.siteID), IsKey = true, BqlField = typeof (INLotSerialStatusByCostCenter.locationID))]
  public virtual int? LocationID { get; set; }

  [PX.Objects.IN.LotSerialNbr(IsKey = true, BqlField = typeof (INLotSerialStatusByCostCenter.lotSerialNbr))]
  public virtual 
  #nullable disable
  string LotSerialNbr { get; set; }

  [PXDBCalced(typeof (IsNull<INCostCenter.costLayerType, PX.Objects.IN.CostLayerType.normal>), typeof (string))]
  [PXString(1, IsKey = true)]
  [PX.Objects.IN.CostLayerType.List]
  [PXUIField(DisplayName = "Cost Layer Type", FieldClass = "CostLayerType")]
  public virtual string CostLayerType { get; set; }

  [PXDBDate(BqlField = typeof (INLotSerialStatusByCostCenter.expireDate))]
  [PXUIField(DisplayName = "Expiry Date")]
  public virtual DateTime? ExpireDate { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyOnHand))]
  public virtual Decimal? QtyOnHand { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyAvail))]
  [PXUIField(DisplayName = "Qty. Available")]
  public virtual Decimal? QtyAvail { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyHardAvail))]
  [PXUIField(DisplayName = "Qty. Hard Available")]
  public virtual Decimal? QtyHardAvail { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyActual))]
  [PXUIField(DisplayName = "Qty. Available for Issue")]
  public virtual Decimal? QtyActual { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyInTransit))]
  [PXUIField(DisplayName = "Qty. In-Transit")]
  public virtual Decimal? QtyInTransit { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyInTransitToSO))]
  [PXUIField(DisplayName = "Qty. In Transit to SO")]
  public virtual Decimal? QtyInTransitToSO { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyPOPrepared))]
  [PXUIField(DisplayName = "Qty. PO Prepared")]
  public virtual Decimal? QtyPOPrepared { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyPOOrders))]
  [PXUIField(DisplayName = "Qty. Purchase Orders")]
  public virtual Decimal? QtyPOOrders { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyPOReceipts))]
  [PXUIField(DisplayName = "Qty. Purchase Receipts")]
  public virtual Decimal? QtyPOReceipts { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyFSSrvOrdBooked))]
  [PXUIField(DisplayName = "Qty. FS Booked", FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFSSrvOrdBooked { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyFSSrvOrdAllocated))]
  [PXUIField(DisplayName = "Qty. FS Allocated", FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFSSrvOrdAllocated { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyFSSrvOrdPrepared))]
  [PXUIField(DisplayName = "Qty. FS Prepared", FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFSSrvOrdPrepared { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtySOBackOrdered))]
  [PXUIField(DisplayName = "Qty. SO Backordered")]
  public virtual Decimal? QtySOBackOrdered { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtySOPrepared))]
  [PXUIField(DisplayName = "Qty. SO Prepared")]
  public virtual Decimal? QtySOPrepared { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtySOBooked))]
  [PXUIField(DisplayName = "Qty. SO Booked")]
  public virtual Decimal? QtySOBooked { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtySOShipped))]
  [PXUIField(DisplayName = "Qty. SO Shipped")]
  public virtual Decimal? QtySOShipped { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtySOShipping))]
  [PXUIField(DisplayName = "Qty. SO Shipping")]
  public virtual Decimal? QtySOShipping { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyINIssues))]
  [PXUIField(DisplayName = "Qty On Inventory Issues")]
  public virtual Decimal? QtyINIssues { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyINReceipts))]
  [PXUIField(DisplayName = "Qty On Inventory Receipts")]
  public virtual Decimal? QtyINReceipts { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyINAssemblyDemand))]
  [PXUIField(DisplayName = "Qty Demanded by Kit Assembly")]
  public virtual Decimal? QtyINAssemblyDemand { get; set; }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyINAssemblySupply))]
  [PXUIField(DisplayName = "Qty On Kit Assembly")]
  public virtual Decimal? QtyINAssemblySupply { get; set; }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.inventoryID>
  {
  }

  public abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLotSerialStatusByCostLayerType.siteID>
  {
  }

  public abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.locationID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.lotSerialNbr>
  {
  }

  public abstract class costLayerType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.costLayerType>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.expireDate>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtyOnHand>
  {
  }

  public abstract class qtyAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtyAvail>
  {
  }

  public abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtyHardAvail>
  {
  }

  public abstract class qtyActual : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtyActual>
  {
  }

  public abstract class qtyInTransit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtyInTransit>
  {
  }

  public abstract class qtyInTransitToSO : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtyInTransitToSO>
  {
  }

  public abstract class qtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtyPOPrepared>
  {
  }

  public abstract class qtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtyPOOrders>
  {
  }

  public abstract class qtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtyPOReceipts>
  {
  }

  public abstract class qtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtyFSSrvOrdBooked>
  {
  }

  public abstract class qtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtyFSSrvOrdAllocated>
  {
  }

  public abstract class qtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtyFSSrvOrdPrepared>
  {
  }

  public abstract class qtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtySOBackOrdered>
  {
  }

  public abstract class qtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtySOPrepared>
  {
  }

  public abstract class qtySOBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtySOBooked>
  {
  }

  public abstract class qtySOShipped : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtySOShipped>
  {
  }

  public abstract class qtySOShipping : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtySOShipping>
  {
  }

  public abstract class qtyINIssues : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtyINIssues>
  {
  }

  public abstract class qtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtyINReceipts>
  {
  }

  public abstract class qtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtyINAssemblyDemand>
  {
  }

  public abstract class qtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostLayerType.qtyINAssemblySupply>
  {
  }
}
