// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.Projections.SOLineForDirectInvoice
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO.DAC.Projections;

[PXProjection(typeof (Select2<PX.Objects.SO.SOLine, InnerJoin<PX.Objects.SO.SOOrder, On<PX.Objects.SO.SOOrder.orderType, Equal<PX.Objects.SO.SOLine.orderType>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<PX.Objects.SO.SOLine.orderNbr>>>, InnerJoin<PX.Objects.SO.SOOrderType, On<PX.Objects.SO.SOOrderType.orderType, Equal<PX.Objects.SO.SOLine.orderType>>>>, Where<PX.Objects.SO.SOOrderType.requireShipping, Equal<True>, And<PX.Objects.SO.SOOrderType.aRDocType, NotEqual<ARDocType.noUpdate>, And2<Where<PX.Objects.SO.SOOrder.isLegacyMiscBilling, Equal<True>, And<Sub<PX.Objects.SO.SOLine.baseOrderQty, PX.Objects.SO.SOLine.baseShippedQty>, Greater<decimal0>, Or<PX.Objects.SO.SOOrder.isLegacyMiscBilling, Equal<False>, And<Where<PX.Objects.SO.SOLine.operation, Equal<PX.Objects.SO.SOLine.defaultOperation>, And<PX.Objects.SO.SOLine.unbilledQty, Greater<decimal0>, Or<PX.Objects.SO.SOLine.operation, NotEqual<PX.Objects.SO.SOLine.defaultOperation>, And<PX.Objects.SO.SOLine.unbilledQty, Less<decimal0>>>>>>>>>, And<PX.Objects.SO.SOLine.pOCreate, Equal<False>, And<PX.Objects.SO.SOLine.completed, Equal<False>>>>>>, OrderBy<Desc<PX.Objects.SO.SOLine.orderNbr>>>), Persistent = false)]
[PXCacheName("Sales Order Line")]
[Serializable]
public class SOLineForDirectInvoice : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLine.orderType))]
  [PXUIField(DisplayName = "Order Type", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType>))]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.SO.SOLine.orderNbr))]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
  public virtual string OrderNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.SO.SOLine.lineNbr))]
  [PXUIField(DisplayName = "Line Nbr.", Enabled = false)]
  public virtual int? LineNbr { get; set; }

  [Customer(Enabled = false, BqlField = typeof (PX.Objects.SO.SOOrder.customerID))]
  public virtual int? CustomerID { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLine.operation))]
  [PXUIField(DisplayName = "Operation", Enabled = false)]
  [SOOperation.List]
  public virtual string Operation { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.SO.SOLine.shipDate))]
  [PXUIField(DisplayName = "Ship On", Enabled = false)]
  public virtual DateTime? ShipDate { get; set; }

  [SOLineInventoryItem(Enabled = false, BqlField = typeof (PX.Objects.SO.SOLine.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [INUnit(typeof (PX.Objects.SO.SOLine.inventoryID), DisplayName = "UOM", Enabled = false, BqlField = typeof (PX.Objects.SO.SOLine.uOM))]
  public virtual string UOM { get; set; }

  [PXDBQuantity(BqlField = typeof (PX.Objects.SO.SOLine.orderQty))]
  [PXUIField(DisplayName = "Order Qty.", Enabled = false)]
  public virtual Decimal? OrderQty { get; set; }

  [PXDBDecimal(6, BqlField = typeof (PX.Objects.SO.SOLine.baseOrderQty))]
  public virtual Decimal? BaseOrderQty { get; set; }

  [PXDBQuantity(BqlField = typeof (PX.Objects.SO.SOLine.shippedQty))]
  [PXUIField(DisplayName = "Qty. on Shipments", Enabled = false)]
  public virtual Decimal? ShippedQty { get; set; }

  [PXDBDecimal(6, BqlField = typeof (PX.Objects.SO.SOLine.baseShippedQty))]
  public virtual Decimal? BaseShippedQty { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLine.completed))]
  [PXUIField(DisplayName = "Completed", Enabled = false)]
  public virtual bool? Completed { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLineForDirectInvoice.selected>
  {
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLineForDirectInvoice.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineForDirectInvoice.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineForDirectInvoice.lineNbr>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineForDirectInvoice.customerID>
  {
  }

  public abstract class operation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLineForDirectInvoice.operation>
  {
  }

  public abstract class shipDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOLineForDirectInvoice.shipDate>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineForDirectInvoice.inventoryID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineForDirectInvoice.uOM>
  {
  }

  public abstract class orderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLineForDirectInvoice.orderQty>
  {
  }

  public abstract class baseOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLineForDirectInvoice.baseOrderQty>
  {
  }

  public abstract class shippedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLineForDirectInvoice.shippedQty>
  {
  }

  public abstract class baseShippedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLineForDirectInvoice.baseShippedQty>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLineForDirectInvoice.completed>
  {
  }
}
