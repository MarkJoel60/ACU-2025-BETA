// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOBlanketOrderDisplayLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO.Attributes;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXProjection(typeof (Select5<SOBlanketOrderLink, InnerJoin<SOOrder, On<SOBlanketOrderLink.FK.ChildOrder>, LeftJoin<SOShipLine, On<SOShipLine.FK.BlanketOrderLink>, LeftJoin<SOOrderShipment, On<SOShipLine.FK.OrderShipment>, LeftJoin<SOShipment, On<SOOrderShipment.FK.Shipment>, LeftJoin<PX.Objects.AR.ARRegister, On<SOOrderShipment.FK.ARRegister>>>>>>, Aggregate<GroupBy<SOBlanketOrderLink.blanketType, GroupBy<SOBlanketOrderLink.blanketNbr, GroupBy<SOBlanketOrderLink.orderType, GroupBy<SOBlanketOrderLink.orderNbr, GroupBy<SOOrderShipment.shippingRefNoteID, Sum<SOShipLine.shippedQty>>>>>>>>), Persistent = false)]
[PXCacheName("Blanket Order Display Link")]
public class SOBlanketOrderDisplayLink : SOBlanketOrderLink
{
  protected int? _CustomerLocationID;
  protected 
  #nullable disable
  string _InvoiceType;
  protected string _InvoiceNbr;
  protected string _InvtDocType;
  protected string _InvtRefNbr;

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (SOBlanketOrderLink.orderNbr))]
  [PXSelector(typeof (Search<SOOrder.orderNbr, Where<SOOrder.orderType, Equal<Current<SOBlanketOrderDisplayLink.orderType>>>>))]
  [PXUIField(DisplayName = "Order Nbr.", Visible = true, Enabled = false)]
  public override string OrderNbr { get; set; }

  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<MatchWithBranch<PX.Objects.CR.Location.cBranchID>>>))]
  public virtual int? CustomerLocationID { get; set; }

  [PXDBDate(BqlField = typeof (SOOrder.orderDate))]
  [PXUIField]
  public virtual DateTime? OrderDate { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (SOOrder.status))]
  [PXUIField]
  [SOOrderStatus.List]
  public virtual string OrderStatus { get; set; }

  [PXDBString(1, IsFixed = true, InputMask = ">a", BqlField = typeof (SOOrderShipment.operation))]
  [PXUIField(DisplayName = "Operation", Enabled = false)]
  [SOOperation.List]
  public virtual string Operation { get; set; }

  [PXDBGuid(false, IsKey = true, BqlField = typeof (SOOrderShipment.shippingRefNoteID))]
  public virtual Guid? ShippingRefNoteID { get; set; }

  [ShippingRefNote]
  [PXFormula(typeof (SOBlanketOrderDisplayLink.shippingRefNoteID))]
  [PXUIField(DisplayName = "Document Nbr.", Enabled = false)]
  public virtual Guid? DisplayShippingRefNoteID { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (SOOrderShipment.shipmentType))]
  [SOShipmentType.List]
  [PXUIField(DisplayName = "Shipment Type", Enabled = false)]
  public virtual string ShipmentType { get; set; }

  [PXDBString(15, InputMask = "", IsUnicode = true, BqlField = typeof (SOOrderShipment.shipmentNbr))]
  [PXUIField(DisplayName = "Shipment Nbr.", Visible = false, Enabled = false)]
  public virtual string ShipmentNbr { get; set; }

  [PXDBDate(BqlField = typeof (SOShipment.shipDate))]
  [PXUIField]
  public virtual DateTime? ShipmentDate { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (SOShipment.status))]
  [PXUIField]
  [SOShipmentStatus.List]
  public virtual string ShipmentStatus { get; set; }

  [PXDBQuantity(BqlField = typeof (SOShipLine.shippedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Shipped Qty.", Enabled = false)]
  public virtual Decimal? ShippedQty { get; set; }

  [PXDBString(3, IsFixed = true, BqlField = typeof (SOOrderShipment.invoiceType))]
  [PXUIField(DisplayName = "Invoice Type", Enabled = false)]
  [ARDocType.List]
  public virtual string InvoiceType
  {
    get => this._InvoiceType;
    set => this._InvoiceType = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (SOOrderShipment.invoiceNbr))]
  [PXUIField(DisplayName = "Invoice Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<SOInvoice.refNbr, Where<SOInvoice.docType, Equal<Current<SOBlanketOrderDisplayLink.invoiceType>>>>), DirtyRead = true)]
  public virtual string InvoiceNbr
  {
    get => this._InvoiceNbr;
    set => this._InvoiceNbr = value;
  }

  [PXDBDate(BqlField = typeof (PX.Objects.AR.ARRegister.docDate))]
  [PXUIField]
  public virtual DateTime? InvoiceDate { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.AR.ARRegister.status))]
  [PXUIField]
  [ARDocStatus.List]
  public virtual string InvoiceStatus { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (SOOrderShipment.invtDocType))]
  [PXUIField(DisplayName = "Inventory Doc. Type", Enabled = false)]
  [INDocType.List]
  public virtual string InvtDocType
  {
    get => this._InvtDocType;
    set => this._InvtDocType = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = "", BqlField = typeof (SOOrderShipment.invtRefNbr))]
  [PXUIField(DisplayName = "Inventory Ref. Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.IN.INRegister.refNbr, Where<PX.Objects.IN.INRegister.docType, Equal<Current<SOOrderShipment.invtDocType>>>>))]
  public virtual string InvtRefNbr
  {
    get => this._InvtRefNbr;
    set => this._InvtRefNbr = value;
  }

  public new class PK : 
    PrimaryKeyOf<SOBlanketOrderDisplayLink>.By<SOBlanketOrderDisplayLink.blanketType, SOBlanketOrderDisplayLink.blanketNbr, SOBlanketOrderDisplayLink.orderType, SOBlanketOrderDisplayLink.orderNbr, SOBlanketOrderDisplayLink.shippingRefNoteID>
  {
    public static SOBlanketOrderDisplayLink Find(
      PXGraph graph,
      string blanketType,
      string blanketNbr,
      string orderType,
      string orderNbr,
      Guid? shippingRefNoteID,
      PKFindOptions options = 0)
    {
      return (SOBlanketOrderDisplayLink) PrimaryKeyOf<SOBlanketOrderDisplayLink>.By<SOBlanketOrderDisplayLink.blanketType, SOBlanketOrderDisplayLink.blanketNbr, SOBlanketOrderDisplayLink.orderType, SOBlanketOrderDisplayLink.orderNbr, SOBlanketOrderDisplayLink.shippingRefNoteID>.FindBy(graph, (object) blanketType, (object) blanketNbr, (object) orderType, (object) orderNbr, (object) shippingRefNoteID, options);
    }
  }

  public new abstract class blanketType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.blanketType>
  {
  }

  public new abstract class blanketNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.blanketNbr>
  {
  }

  public new abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.orderType>
  {
  }

  public new abstract class orderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.orderNbr>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.customerLocationID>
  {
  }

  public abstract class orderDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.orderDate>
  {
  }

  public abstract class orderStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.orderStatus>
  {
  }

  public new abstract class orderedQty : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.orderedQty>
  {
  }

  public new abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.curyInfoID>
  {
  }

  public new abstract class curyOrderedAmt : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.curyOrderedAmt>
  {
  }

  public new abstract class orderedAmt : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.orderedAmt>
  {
  }

  public abstract class operation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.operation>
  {
  }

  public abstract class shippingRefNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.shippingRefNoteID>
  {
  }

  public abstract class displayShippingRefNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.displayShippingRefNoteID>
  {
  }

  public abstract class shipmentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.shipmentType>
  {
  }

  public abstract class shipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.shipmentNbr>
  {
  }

  public abstract class shipmentDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.shipmentDate>
  {
  }

  public abstract class shipmentStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.shipmentStatus>
  {
  }

  public abstract class shippedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.shippedQty>
  {
  }

  public abstract class invoiceType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.invoiceType>
  {
  }

  public abstract class invoiceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.invoiceNbr>
  {
  }

  public abstract class invoiceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.invoiceDate>
  {
  }

  public abstract class invoiceStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.invoiceStatus>
  {
  }

  public abstract class invtDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.invtDocType>
  {
  }

  public abstract class invtRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.invtRefNbr>
  {
  }

  public new abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderDisplayLink.Tstamp>
  {
  }
}
