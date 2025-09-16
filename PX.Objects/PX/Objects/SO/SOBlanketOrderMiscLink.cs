// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOBlanketOrderMiscLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXProjection(typeof (Select5<SOBlanketOrderLink, InnerJoin<SOOrder, On<SOBlanketOrderLink.FK.ChildOrder>, InnerJoin<PX.Objects.AR.ARTran, On<PX.Objects.AR.ARTran.FK.BlanketOrderLink>, InnerJoin<SOOrderShipment, On<PX.Objects.AR.ARTran.FK.SOOrderShipment>, InnerJoin<PX.Objects.AR.ARRegister, On<SOOrderShipment.FK.ARRegister>>>>>, Where<BqlOperand<SOOrderShipment.shipmentNoteID, IBqlGuid>.IsNull>, Aggregate<GroupBy<SOBlanketOrderLink.blanketType, GroupBy<SOBlanketOrderLink.blanketNbr, GroupBy<SOBlanketOrderLink.orderType, GroupBy<SOBlanketOrderLink.orderNbr, GroupBy<SOOrderShipment.shippingRefNoteID>>>>>>>), Persistent = false)]
[PXHidden]
public class SOBlanketOrderMiscLink : SOBlanketOrderDisplayLink
{
  [PXString(1, IsFixed = true, InputMask = ">a")]
  [PXUIField(DisplayName = "Operation", Enabled = false)]
  [SOOperation.List]
  public override 
  #nullable disable
  string Operation { get; set; }

  [PXDate]
  [PXUIField]
  public override DateTime? ShipmentDate { get; set; }

  [PXString(1, IsFixed = true)]
  [PXUIField]
  [SOShipmentStatus.List]
  public override string ShipmentStatus { get; set; }

  [PXQuantity]
  [PXUIField(DisplayName = "Shipped Qty.", Enabled = false)]
  public override Decimal? ShippedQty
  {
    get => base.ShippedQty;
    set => base.ShippedQty = value;
  }

  public new abstract class blanketType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.blanketType>
  {
  }

  public new abstract class blanketNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.blanketNbr>
  {
  }

  public new abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.orderType>
  {
  }

  public new abstract class orderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.orderNbr>
  {
  }

  public new abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.customerLocationID>
  {
  }

  public new abstract class orderDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.orderDate>
  {
  }

  public new abstract class orderStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.orderStatus>
  {
  }

  public new abstract class orderedQty : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.orderedQty>
  {
  }

  public new abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.curyInfoID>
  {
  }

  public new abstract class curyOrderedAmt : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.curyOrderedAmt>
  {
  }

  public new abstract class orderedAmt : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.orderedAmt>
  {
  }

  public new abstract class operation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.operation>
  {
  }

  public new abstract class shippingRefNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.shippingRefNoteID>
  {
  }

  public new abstract class displayShippingRefNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.displayShippingRefNoteID>
  {
  }

  public new abstract class shipmentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.shipmentType>
  {
  }

  public new abstract class shipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.shipmentNbr>
  {
  }

  public new abstract class shipmentDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.shipmentDate>
  {
  }

  public new abstract class shipmentStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.shipmentStatus>
  {
  }

  public new abstract class shippedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.shippedQty>
  {
  }

  public new abstract class invoiceType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.invoiceType>
  {
  }

  public new abstract class invoiceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.invoiceNbr>
  {
  }

  public new abstract class invoiceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.invoiceDate>
  {
  }

  public new abstract class invoiceStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.invoiceStatus>
  {
  }

  public new abstract class invtDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.invtDocType>
  {
  }

  public new abstract class invtRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderMiscLink.invtRefNbr>
  {
  }

  public new abstract class Tstamp : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOBlanketOrderMiscLink.Tstamp>
  {
  }
}
