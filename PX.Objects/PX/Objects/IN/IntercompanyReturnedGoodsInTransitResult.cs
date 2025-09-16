// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.IntercompanyReturnedGoodsInTransitResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.Objects.GL.Standalone;
using PX.Objects.PO;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.IN;

/// <summary>
/// The DAC is used as a result in Intercompany Returned Goods in Transit Generic Inquiry
/// </summary>
[PXCacheName("Intercompany Returned Goods in Transit Result")]
[PXProjection(typeof (Select2<PX.Objects.PO.POReceiptLine, InnerJoin<InventoryItem, On<PX.Objects.PO.POReceiptLine.FK.InventoryItem>, InnerJoin<PX.Objects.PO.POReceipt, On2<PX.Objects.PO.POReceiptLine.FK.Receipt, And<PX.Objects.PO.POReceipt.receiptType, Equal<POReceiptType.poreturn>, And<PX.Objects.PO.POReceipt.isIntercompany, Equal<True>>>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.PO.POReceipt.FK.Branch>, InnerJoin<Branch2, On<Branch2.bAccountID, Equal<PX.Objects.PO.POReceipt.vendorID>>, LeftJoin<PX.Objects.SO.SOOrder, On<BqlOperand<PX.Objects.SO.SOOrder.intercompanyPOReturnNbr, IBqlString>.IsEqual<PX.Objects.PO.POReceipt.receiptNbr>>, LeftJoin<PX.Objects.SO.SOLine, On<Where2<PX.Objects.SO.SOLine.FK.Order, And<PX.Objects.SO.SOLine.intercompanyPOLineNbr, Equal<PX.Objects.PO.POReceiptLine.lineNbr>>>>, LeftJoin<SOShipLine, On<Where2<SOShipLine.FK.OrderLine, And<SOShipLine.confirmed, Equal<False>>>>, LeftJoin<PX.Objects.SO.SOShipment, On<SOShipLine.FK.Shipment>>>>>>>>>>), Persistent = false)]
public class IntercompanyReturnedGoodsInTransitResult : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage
{
  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.PO.POReceiptLine.receiptNbr))]
  [POReceiptType.RefNbr(typeof (Search<PX.Objects.PO.POReceipt.receiptNbr, Where<PX.Objects.PO.POReceipt.receiptType, Equal<POReceiptType.poreturn>>>), Filterable = true)]
  [PXUIField(DisplayName = "Return Nbr.")]
  public virtual 
  #nullable disable
  string POReturnNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.PO.POReceiptLine.lineNbr))]
  public virtual int? LineNbr { get; set; }

  [Branch(typeof (AccessInfo.branchID), null, true, true, true, DisplayName = "Purchasing Company", Required = false, BqlField = typeof (PX.Objects.PO.POReceipt.branchID))]
  public virtual int? PurchasingBranchID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.GL.Branch.bAccountID))]
  public virtual int? PurchasingBranchBAccountID { get; set; }

  [Site(DisplayName = "Purchasing Warehouse", DescriptionField = typeof (INSite.descr), BqlField = typeof (PX.Objects.PO.POReceiptLine.siteID))]
  public virtual int? PurchasingSiteID { get; set; }

  [Inventory(BqlField = typeof (PX.Objects.PO.POReceiptLine.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (PX.Objects.PO.POReceiptLine.tranDesc))]
  [PXUIField(DisplayName = "Description")]
  public virtual string TranDesc { get; set; }

  [INUnit(typeof (IntercompanyReturnedGoodsInTransitResult.inventoryID), DisplayName = "UOM", BqlField = typeof (PX.Objects.PO.POReceiptLine.uOM))]
  public virtual string UOM { get; set; }

  [PXDBDecimal(BqlField = typeof (PX.Objects.PO.POReceiptLine.receiptQty))]
  [PXUIField(DisplayName = "In-Transit Qty.")]
  public virtual Decimal? ReturnedQty { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (PX.Objects.PO.POReceiptLine.tranCost))]
  [PXUIField(DisplayName = "Total Cost")]
  public virtual Decimal? ExtCost { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.PO.POReceipt.receiptDate))]
  [PXUIField(DisplayName = "Return Date")]
  public virtual DateTime? ReturnDate { get; set; }

  [PXInt]
  [PXDBCalced(typeof (DateDiff<PX.Objects.PO.POReceipt.receiptDate, IntercompanyGoodsInTransitResult.businessDate, DateDiff.day>), typeof (int))]
  [PXUIField(DisplayName = "Days in Transit")]
  public virtual int? DaysInTransit { get; set; }

  [Branch(null, null, true, true, true, DisplayName = "Selling Company", Required = false, BqlField = typeof (Branch2.branchID))]
  public virtual int? SellingBranchID { get; set; }

  [Vendor(DisplayName = "Selling Company", BqlField = typeof (PX.Objects.PO.POReceipt.vendorID))]
  public virtual int? SellingBranchBAccountID { get; set; }

  [Site(DisplayName = "Selling Warehouse", DescriptionField = typeof (INSite.descr), BqlField = typeof (PX.Objects.SO.SOLine.siteID))]
  public virtual int? SellingSiteID { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLine.orderType))]
  [PXUIField(DisplayName = "SO Type")]
  public virtual string SOType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOLine.orderNbr))]
  [PXUIField(DisplayName = "SO Nbr.")]
  [PX.Objects.SO.SO.RefNbr(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<IntercompanyReturnedGoodsInTransitResult.sOType>>>>))]
  public virtual string SONbr { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.lineNbr))]
  public virtual int? SOLineNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLine.behavior))]
  [PX.Objects.SO.SOBehavior.List]
  public virtual string SOBehavior { get; set; }

  [PXDBBool(BqlField = typeof (InventoryItem.stkItem))]
  [PXUIField(DisplayName = "Stock Item")]
  public virtual bool? StkItem { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.PO.POReceipt.released))]
  [PXUIField(DisplayName = "Released")]
  public virtual bool? ReturnReleased { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (SOShipLine.shipmentNbr))]
  [PXSelector(typeof (Search<PX.Objects.SO.SOShipment.shipmentNbr>))]
  [PXUIField(DisplayName = "Shipment Nbr.", Enabled = false)]
  public virtual string ShipmentNbr { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOShipment.status))]
  [PXUIField(DisplayName = "Shipment Status", Enabled = false, Visible = false)]
  [SOShipmentStatus.List]
  public virtual string ShipmentStatus { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.SO.SOShipment.shipDate))]
  [PXUIField(DisplayName = "Shipment Date", Visible = false)]
  public virtual DateTime? ShipmentDate { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.PO.POReceipt.excludeFromIntercompanyProc))]
  [PXUIField(DisplayName = "Exclude from Intercompany Processing")]
  public virtual bool? ExcludeFromIntercompanyProc { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.PO.POReceiptLine.origReceiptType))]
  public virtual string OrigReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.PO.POReceiptLine.origReceiptNbr))]
  public virtual string OrigReceiptNbr { get; set; }

  [PXNote(BqlField = typeof (PX.Objects.PO.POReceiptLine.noteID))]
  public virtual Guid? NoteID { get; set; }

  public abstract class pOReturnNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.pOReturnNbr>
  {
  }

  public abstract class lineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.lineNbr>
  {
  }

  public abstract class purchasingBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.purchasingBranchID>
  {
  }

  public abstract class purchasingBranchBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.purchasingBranchBAccountID>
  {
  }

  public abstract class purchasingSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.purchasingSiteID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.inventoryID>
  {
  }

  public abstract class tranDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.tranDesc>
  {
  }

  public abstract class uOM : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.uOM>
  {
  }

  public abstract class returnedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.returnedQty>
  {
  }

  public abstract class extCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.extCost>
  {
  }

  public abstract class returnDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.returnDate>
  {
  }

  public abstract class daysInTransit : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.daysInTransit>
  {
  }

  public abstract class sellingBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.sellingBranchID>
  {
  }

  public abstract class sellingBranchBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.sellingBranchBAccountID>
  {
  }

  public abstract class sellingSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.sellingSiteID>
  {
  }

  public abstract class sOType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.sOType>
  {
  }

  public abstract class sONbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.sONbr>
  {
  }

  public abstract class sOLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.sOLineNbr>
  {
  }

  public abstract class sOBehavior : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.sOBehavior>
  {
  }

  public abstract class stkItem : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.stkItem>
  {
  }

  public abstract class returnReleased : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.returnReleased>
  {
  }

  public abstract class shipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.shipmentNbr>
  {
  }

  public abstract class shipmentStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.shipmentStatus>
  {
  }

  public abstract class shipmentDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.shipmentDate>
  {
  }

  public abstract class excludeFromIntercompanyProc : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.excludeFromIntercompanyProc>
  {
  }

  public abstract class origReceiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.origReceiptType>
  {
  }

  public abstract class origReceiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.origReceiptNbr>
  {
  }

  public abstract class noteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    IntercompanyReturnedGoodsInTransitResult.noteID>
  {
  }
}
