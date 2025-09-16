// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.Projections.BlanketSOLineSplit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO.DAC.Projections;

[PXCacheName("Blanket SO Line Split")]
[PXProjection(typeof (Select<PX.Objects.SO.SOLineSplit, Where<PX.Objects.SO.SOLineSplit.behavior, Equal<SOBehavior.bL>>>), Persistent = true)]
public class BlanketSOLineSplit : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IItemPlanSource,
  IItemPlanMaster
{
  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.orderType))]
  [PXDefault]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.orderNbr))]
  [PXDefault]
  public virtual string OrderNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.lineNbr))]
  [PXDefault]
  [PXParent(typeof (BlanketSOLineSplit.FK.BlanketOrderLine))]
  public virtual int? LineNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.splitLineNbr))]
  [PXDefault]
  public virtual int? SplitLineNbr { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLineSplit.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.lineType))]
  [PXDefault]
  public virtual string LineType { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLineSplit.siteID))]
  [PXDefault]
  public virtual int? SiteID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLineSplit.locationID))]
  public virtual int? LocationID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLineSplit.subItemID))]
  [PXDefault]
  public virtual int? SubItemID { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.SO.SOLineSplit.shipDate))]
  public virtual DateTime? ShipDate { get; set; }

  [INUnit(typeof (BlanketSOLineSplit.inventoryID), BqlField = typeof (PX.Objects.SO.SOLineSplit.uOM))]
  [PXDefault]
  public virtual string UOM { get; set; }

  [PXDBQuantity(typeof (BlanketSOLineSplit.uOM), typeof (BlanketSOLineSplit.baseQty), BqlField = typeof (PX.Objects.SO.SOLineSplit.qty))]
  [PXDefault]
  public virtual Decimal? Qty { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, BqlField = typeof (PX.Objects.SO.SOLineSplit.baseQty))]
  [PXDefault]
  public virtual Decimal? BaseQty { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLineSplit.toSiteID))]
  [PXDefault]
  public virtual int? ToSiteID { get; set; }

  [PXDBString(100, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.lotSerialNbr))]
  public virtual string LotSerialNbr { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLineSplit.isAllocated))]
  [PXDefault]
  public virtual bool? IsAllocated { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLineSplit.pOCreate))]
  [PXDefault]
  public virtual bool? POCreate { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.pOType))]
  public virtual string POType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.pONbr))]
  public virtual string PONbr { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLineSplit.pOLineNbr))]
  public virtual int? POLineNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.pOReceiptType))]
  public virtual string POReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.pOReceiptNbr))]
  public virtual string POReceiptNbr { get; set; }

  [PXDBGuid(false, BqlField = typeof (PX.Objects.SO.SOLineSplit.refNoteID))]
  public virtual Guid? RefNoteID { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLineSplit.pOCompleted))]
  [PXDefault]
  public virtual bool? POCompleted { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLineSplit.pOCancelled))]
  [PXDefault(false)]
  public virtual bool? POCancelled { get; set; }

  [PXDBString(BqlField = typeof (PX.Objects.SO.SOLineSplit.pOSource))]
  public virtual string POSource { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLineSplit.vendorID))]
  public virtual int? VendorID { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLineSplit.completed))]
  [PXDefault]
  public virtual bool? Completed { get; set; }

  [PXDBLong(IsImmutable = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.planID))]
  public virtual long? PlanID { get; set; }

  [PXDBQuantity(typeof (BlanketSOLineSplit.uOM), typeof (BlanketSOLineSplit.baseQtyOnOrders), BqlField = typeof (PX.Objects.SO.SOLineSplit.qtyOnOrders))]
  [PXDefault]
  public virtual Decimal? QtyOnOrders { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, BqlField = typeof (PX.Objects.SO.SOLineSplit.baseQtyOnOrders))]
  [PXDefault]
  public virtual Decimal? BaseQtyOnOrders { get; set; }

  [PXDBString(40, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.customerOrderNbr))]
  public virtual string CustomerOrderNbr { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.SO.SOLineSplit.schedOrderDate))]
  public virtual DateTime? SchedOrderDate { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.SO.SOLineSplit.schedShipDate))]
  public virtual DateTime? SchedShipDate { get; set; }

  [PXQuantity]
  [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.SO.SOLineSplit.lineType, NotEqual<SOLineType.miscCharge>, And<PX.Objects.SO.SOLineSplit.completed, Equal<False>>>, Sub<PX.Objects.SO.SOLineSplit.qty, Add<PX.Objects.SO.SOLineSplit.qtyOnOrders, PX.Objects.SO.SOLineSplit.receivedQty>>>, decimal0>), typeof (Decimal))]
  [PXFormula(typeof (Switch<Case<Where<BlanketSOLineSplit.lineType, NotEqual<SOLineType.miscCharge>, And<BlanketSOLineSplit.completed, Equal<False>>>, Sub<BlanketSOLineSplit.qty, Add<BlanketSOLineSplit.qtyOnOrders, BlanketSOLineSplit.receivedQty>>>, decimal0>))]
  [PXDefault]
  public virtual Decimal? BlanketOpenQty { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLineSplit.childLineCntr))]
  [PXDefault]
  public virtual int? ChildLineCntr { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLineSplit.effectiveChildLineCntr))]
  [PXDefault]
  public virtual int? EffectiveChildLineCntr { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLineSplit.openChildLineCntr))]
  [PXDefault]
  public virtual int? OpenChildLineCntr { get; set; }

  [PXDBQuantity(typeof (BlanketSOLineSplit.uOM), typeof (BlanketSOLineSplit.baseShippedQty), BqlField = typeof (PX.Objects.SO.SOLineSplit.shippedQty))]
  [PXDefault]
  public virtual Decimal? ShippedQty { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, BqlField = typeof (PX.Objects.SO.SOLineSplit.baseShippedQty))]
  [PXDefault]
  public virtual Decimal? BaseShippedQty { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLineSplit.ClosedQty" />
  [PXDBQuantity(typeof (BlanketSOLineSplit.uOM), typeof (BlanketSOLineSplit.baseShippedQty), BqlField = typeof (PX.Objects.SO.SOLineSplit.closedQty))]
  [PXDefault]
  public virtual Decimal? ClosedQty { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLineSplit.BaseClosedQty" />
  [PXDBDecimal(6, MinValue = 0.0, BqlField = typeof (PX.Objects.SO.SOLineSplit.baseClosedQty))]
  [PXDefault]
  public virtual Decimal? BaseClosedQty { get; set; }

  [PXDBQuantity(typeof (BlanketSOLineSplit.uOM), typeof (BlanketSOLineSplit.baseReceivedQty), BqlField = typeof (PX.Objects.SO.SOLineSplit.receivedQty))]
  [PXDefault]
  public virtual Decimal? ReceivedQty { get; set; }

  [PXDBDecimal(6, BqlField = typeof (PX.Objects.SO.SOLineSplit.baseReceivedQty))]
  [PXDefault]
  public virtual Decimal? BaseReceivedQty { get; set; }

  [PXDecimal]
  [PXFormula(typeof (Sub<BlanketSOLineSplit.baseQty, BlanketSOLineSplit.baseReceivedQty>))]
  public virtual Decimal? BaseUnreceivedQty { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (PX.Objects.SO.SOLineSplit.lastModifiedByID))]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (PX.Objects.SO.SOLineSplit.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (PX.Objects.SO.SOLineSplit.lastModifiedDateTime))]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<BlanketSOLineSplit>.By<BlanketSOLineSplit.orderType, BlanketSOLineSplit.orderNbr, BlanketSOLineSplit.lineNbr, BlanketSOLineSplit.splitLineNbr>
  {
    public static BlanketSOLineSplit Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      int? lineNbr,
      int? splitLineNbr,
      PKFindOptions options = 0)
    {
      return (BlanketSOLineSplit) PrimaryKeyOf<BlanketSOLineSplit>.By<BlanketSOLineSplit.orderType, BlanketSOLineSplit.orderNbr, BlanketSOLineSplit.lineNbr, BlanketSOLineSplit.splitLineNbr>.FindBy(graph, (object) orderType, (object) orderNbr, (object) lineNbr, (object) splitLineNbr, options);
    }
  }

  public static class FK
  {
    public class Order : 
      PrimaryKeyOf<PX.Objects.SO.SOOrder>.By<PX.Objects.SO.SOOrder.orderType, PX.Objects.SO.SOOrder.orderNbr>.ForeignKeyOf<BlanketSOLineSplit>.By<BlanketSOLineSplit.orderType, BlanketSOLineSplit.orderNbr>
    {
    }

    public class BlanketOrder : 
      PrimaryKeyOf<BlanketSOOrder>.By<BlanketSOOrder.orderType, BlanketSOOrder.orderNbr>.ForeignKeyOf<BlanketSOLineSplit>.By<BlanketSOLineSplit.orderType, BlanketSOLineSplit.orderNbr>
    {
    }

    public class BlanketOrderLine : 
      PrimaryKeyOf<BlanketSOLine>.By<BlanketSOLine.orderType, BlanketSOLine.orderNbr, BlanketSOLine.lineNbr>.ForeignKeyOf<BlanketSOLineSplit>.By<BlanketSOLineSplit.orderType, BlanketSOLineSplit.orderNbr, BlanketSOLineSplit.lineNbr>
    {
    }
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLineSplit.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLineSplit.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLineSplit.lineNbr>
  {
  }

  public abstract class splitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLineSplit.splitLineNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLineSplit.inventoryID>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLineSplit.lineType>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLineSplit.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLineSplit.locationID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLineSplit.subItemID>
  {
  }

  public abstract class shipDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  BlanketSOLineSplit.shipDate>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLineSplit.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLineSplit.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLineSplit.baseQty>
  {
  }

  public abstract class toSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLineSplit.toSiteID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BlanketSOLineSplit.lotSerialNbr>
  {
  }

  public abstract class isAllocated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOLineSplit.isAllocated>
  {
  }

  public abstract class pOCreate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOLineSplit.pOCreate>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLineSplit.pOType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLineSplit.pONbr>
  {
  }

  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLineSplit.pOLineNbr>
  {
  }

  public abstract class pOReceiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BlanketSOLineSplit.pOReceiptType>
  {
  }

  public abstract class pOReceiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BlanketSOLineSplit.pOReceiptNbr>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  BlanketSOLineSplit.refNoteID>
  {
  }

  public abstract class pOCompleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOLineSplit.pOCompleted>
  {
  }

  public abstract class pOCancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOLineSplit.pOCancelled>
  {
  }

  public abstract class pOSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLineSplit.pOSource>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLineSplit.vendorID>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOLineSplit.completed>
  {
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  BlanketSOLineSplit.planID>
  {
  }

  public abstract class qtyOnOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLineSplit.qtyOnOrders>
  {
  }

  public abstract class baseQtyOnOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLineSplit.baseQtyOnOrders>
  {
  }

  public abstract class customerOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BlanketSOLineSplit.customerOrderNbr>
  {
  }

  public abstract class schedOrderDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BlanketSOLineSplit.schedOrderDate>
  {
  }

  public abstract class schedShipDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BlanketSOLineSplit.schedShipDate>
  {
  }

  public abstract class blanketOpenQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLineSplit.blanketOpenQty>
  {
  }

  public abstract class childLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLineSplit.childLineCntr>
  {
  }

  public abstract class effectiveChildLineCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BlanketSOLineSplit.effectiveChildLineCntr>
  {
  }

  public abstract class openChildLineCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BlanketSOLineSplit.openChildLineCntr>
  {
  }

  public abstract class shippedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLineSplit.shippedQty>
  {
  }

  public abstract class baseShippedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLineSplit.baseShippedQty>
  {
  }

  public abstract class closedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLineSplit.closedQty>
  {
  }

  public abstract class baseClosedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLineSplit.baseClosedQty>
  {
  }

  public abstract class receivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLineSplit.receivedQty>
  {
  }

  public abstract class baseReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLineSplit.baseReceivedQty>
  {
  }

  public abstract class baseUnreceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLineSplit.baseUnreceivedQty>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  BlanketSOLineSplit.Tstamp>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    BlanketSOLineSplit.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BlanketSOLineSplit.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BlanketSOLineSplit.lastModifiedDateTime>
  {
  }
}
