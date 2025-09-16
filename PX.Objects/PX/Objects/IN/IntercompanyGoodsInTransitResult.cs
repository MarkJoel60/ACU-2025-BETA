// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.IntercompanyGoodsInTransitResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Standalone;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.IN;

/// <summary>
/// The DAC is used as a result in Intercompany Goods in Transit Generic Inquiry
/// </summary>
[PXCacheName("Intercompany Goods in Transit Result")]
[PXProjection(typeof (Select2<SOShipLine, InnerJoin<InventoryItem, On<SOShipLine.FK.InventoryItem>, InnerJoin<PX.Objects.SO.SOShipment, On<SOShipLine.FK.Shipment>, InnerJoin<PX.Objects.SO.SOOrder, On<SOShipLine.FK.Order>, InnerJoin<PX.Objects.SO.SOLine, On<SOShipLine.FK.OrderLine>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.SO.SOOrder.FK.Branch>, InnerJoin<Branch2, On<Branch2.bAccountID, Equal<PX.Objects.SO.SOOrder.customerID>>, LeftJoin<PX.Objects.PO.POReceipt, On<Where2<PX.Objects.PO.POReceipt.FK.IntercompanyShipment, And<PX.Objects.PO.POReceipt.canceled, Equal<False>>>>, LeftJoin<PX.Objects.PO.POReceiptLine, On<Where2<PX.Objects.PO.POReceiptLine.FK.Receipt, And<PX.Objects.PO.POReceiptLine.intercompanyShipmentLineNbr, Equal<SOShipLine.lineNbr>>>>>>>>>>>>, Where<PX.Objects.SO.SOShipment.isIntercompany, Equal<True>>>), Persistent = false)]
public class IntercompanyGoodsInTransitResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (SOShipLine.shipmentNbr))]
  [PXSelector(typeof (Search<PX.Objects.SO.SOShipment.shipmentNbr>))]
  [PXUIField(DisplayName = "Shipment Nbr.", Enabled = false)]
  public virtual 
  #nullable disable
  string ShipmentNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (SOShipLine.lineNbr))]
  [PXUIField(DisplayName = "Line Nbr.", Enabled = false)]
  public virtual int? LineNbr { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOShipment.operation))]
  [PXUIField(DisplayName = "Operation")]
  [SOOperation.List]
  public virtual string Operation { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOShipment.shipmentType))]
  [PXUIField(DisplayName = "Type")]
  [SOShipmentType.ShortList]
  public virtual string ShipmentType { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.SO.SOShipment.shipDate))]
  [PXUIField(DisplayName = "Shipment Date")]
  public virtual DateTime? ShipDate { get; set; }

  [Branch(typeof (AccessInfo.branchID), null, true, true, true, DisplayName = "Selling Company", Required = false, BqlField = typeof (PX.Objects.SO.SOOrder.branchID))]
  public virtual int? SellingBranchID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.GL.Branch.bAccountID))]
  public virtual int? SellingBranchBAccountID { get; set; }

  [Site(DisplayName = "Selling Warehouse", DescriptionField = typeof (INSite.descr), BqlField = typeof (PX.Objects.SO.SOShipment.siteID))]
  public virtual int? SellingSiteID { get; set; }

  [Inventory(BqlField = typeof (SOShipLine.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (SOShipLine.tranDesc))]
  [PXUIField(DisplayName = "Description")]
  public virtual string TranDesc { get; set; }

  [INUnit(typeof (IntercompanyGoodsInTransitResult.inventoryID), DisplayName = "UOM", BqlField = typeof (SOShipLine.uOM))]
  public virtual string UOM { get; set; }

  [PXDBDecimal(BqlField = typeof (SOShipLine.shippedQty))]
  [PXUIField(DisplayName = "In-Transit Qty.")]
  public virtual Decimal? ShippedQty { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.SO.SOLine.baseOrderQty, Equal<decimal0>>, PX.Objects.SO.SOLine.lineAmt>, Div<Mult<PX.Objects.SO.SOLine.lineAmt, SOShipLine.baseShippedQty>, PX.Objects.SO.SOLine.baseOrderQty>>), typeof (Decimal))]
  [PXUIField(DisplayName = "Total Cost")]
  public virtual Decimal? ExtCost { get; set; }

  [PXInt]
  [PXDBCalced(typeof (DateDiff<PX.Objects.SO.SOShipment.shipDate, IntercompanyGoodsInTransitResult.businessDate, DateDiff.day>), typeof (int))]
  [PXUIField(DisplayName = "Days in Transit")]
  public virtual int? DaysInTransit { get; set; }

  [PXInt]
  [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.SO.SOLine.requestDate, GreaterEqual<IntercompanyGoodsInTransitResult.businessDate>>, Null>, DateDiff<PX.Objects.SO.SOLine.requestDate, IntercompanyGoodsInTransitResult.businessDate, DateDiff.day>>), typeof (int))]
  [PXUIField(DisplayName = "Days Overdue")]
  public virtual int? DaysOverdue { get; set; }

  [Branch(null, null, true, true, true, DisplayName = "Purchasing Company", Required = false, BqlField = typeof (Branch2.branchID))]
  public virtual int? PurchasingBranchID { get; set; }

  [Customer(DisplayName = "Purchasing Company", BqlField = typeof (PX.Objects.SO.SOOrder.customerID))]
  public virtual int? PurchasingBranchBAccountID { get; set; }

  [Site(DisplayName = "Purchasing Warehouse", DescriptionField = typeof (INSite.descr), BqlField = typeof (PX.Objects.PO.POReceiptLine.siteID))]
  public virtual int? PurchasingSiteID { get; set; }

  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.PO.POReceipt.receiptType))]
  [PX.Objects.PO.POReceiptType.List]
  [PXUIField(DisplayName = "Receipt Type")]
  public virtual string POReceiptType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, BqlField = typeof (PX.Objects.PO.POReceipt.receiptNbr))]
  [PX.Objects.PO.POReceiptType.RefNbr(typeof (Search<PX.Objects.PO.POReceipt.receiptNbr, Where<BqlOperand<PX.Objects.PO.POReceipt.receiptType, IBqlString>.IsEqual<BqlField<IntercompanyGoodsInTransitResult.pOReceiptType, IBqlString>.FromCurrent>>>), Filterable = true)]
  [PXUIField(DisplayName = "Receipt Nbr.")]
  public virtual string POReceiptNbr { get; set; }

  [PXDBBool(BqlField = typeof (InventoryItem.stkItem))]
  [PXUIField(DisplayName = "Stock Item")]
  public virtual bool? StkItem { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOShipment.confirmed))]
  [PXUIField(DisplayName = "Confirmed")]
  public virtual bool? ShipmentConfirmed { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOShipment.status))]
  [PXUIField(DisplayName = "Shipment Status", Enabled = false, Visible = false)]
  [SOShipmentStatus.List]
  public virtual string ShipmentStatus { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.PO.POReceipt.released))]
  [PXUIField(DisplayName = "Released", Visible = false)]
  public virtual bool? ReceiptReleased { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.SO.SOLine.requestDate))]
  [PXUIField(DisplayName = "Requested On", Visible = false)]
  public virtual DateTime? RequestDate { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.PO.POReceipt.receiptDate))]
  [PXUIField(DisplayName = "Receipt Date", Visible = false)]
  public virtual DateTime? ReceiptDate { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOShipment.excludeFromIntercompanyProc))]
  [PXUIField(DisplayName = "Exclude from Intercompany Processing")]
  public virtual bool? ExcludeFromIntercompanyProc { get; set; }

  [PXNote(BqlField = typeof (SOShipLine.noteID))]
  public virtual Guid? NoteID { get; set; }

  public abstract class shipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.shipmentNbr>
  {
  }

  public abstract class lineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.lineNbr>
  {
  }

  public abstract class operation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.operation>
  {
  }

  public abstract class shipmentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.shipmentType>
  {
  }

  public abstract class shipDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.shipDate>
  {
  }

  public abstract class sellingBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.sellingBranchID>
  {
  }

  public abstract class sellingBranchBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.sellingBranchBAccountID>
  {
  }

  public abstract class sellingSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.sellingSiteID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.inventoryID>
  {
  }

  public abstract class tranDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.tranDesc>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  IntercompanyGoodsInTransitResult.uOM>
  {
  }

  public abstract class shippedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.shippedQty>
  {
  }

  public abstract class extCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.extCost>
  {
  }

  public abstract class daysInTransit : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.daysInTransit>
  {
  }

  public abstract class daysOverdue : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.daysOverdue>
  {
  }

  public abstract class purchasingBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.purchasingBranchID>
  {
  }

  public abstract class purchasingBranchBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.purchasingBranchBAccountID>
  {
  }

  public abstract class purchasingSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.purchasingSiteID>
  {
  }

  public abstract class pOReceiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.pOReceiptType>
  {
  }

  public abstract class pOReceiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.pOReceiptNbr>
  {
  }

  public abstract class stkItem : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.stkItem>
  {
  }

  public abstract class shipmentConfirmed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.shipmentConfirmed>
  {
  }

  public abstract class shipmentStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.shipmentStatus>
  {
  }

  public abstract class receiptReleased : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.receiptReleased>
  {
  }

  public abstract class requestDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.requestDate>
  {
  }

  public abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.receiptDate>
  {
  }

  public abstract class excludeFromIntercompanyProc : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.excludeFromIntercompanyProc>
  {
  }

  public abstract class noteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    IntercompanyGoodsInTransitResult.noteID>
  {
  }

  public class businessDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Constant<
    #nullable disable
    IntercompanyGoodsInTransitResult.businessDate>
  {
    public businessDate()
      : base(PXContext.GetBusinessDate() ?? PXTimeZoneInfo.Today)
    {
    }
  }
}
