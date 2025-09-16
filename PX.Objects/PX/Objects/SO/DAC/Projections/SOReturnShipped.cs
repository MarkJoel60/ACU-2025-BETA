// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.Projections.SOReturnShipped
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using PX.Objects.SO.DAC.Unbound;
using System;

#nullable enable
namespace PX.Objects.SO.DAC.Projections;

[PXHidden]
[PXProjection(typeof (Select5<PX.Objects.SO.SOLine, InnerJoin<PX.Objects.SO.SOOrderType, On<PX.Objects.SO.SOLine.FK.OrderType>, LeftJoin<SOShipLine, On<SOShipLine.FK.OrderLine>, LeftJoin<PX.Objects.AR.ARTran, On<PX.Objects.AR.ARTran.FK.SOOrderLine>, LeftJoin<PX.Objects.AR.ARRegister, On<PX.Objects.AR.ARTran.FK.Document>, LeftJoin<PX.Objects.PO.POReceiptLine, On<PX.Objects.PO.POReceiptLine.FK.SOLine>, LeftJoin<PX.Objects.AP.APTran, On<PX.Objects.AP.APTran.FK.POReceiptLine>, LeftJoin<PX.Objects.SO.SOOrderShipment, On<PX.Objects.SO.SOOrderShipment.orderType, Equal<PX.Objects.SO.SOLine.orderType>, And<PX.Objects.SO.SOOrderShipment.orderNbr, Equal<PX.Objects.SO.SOLine.orderNbr>, And<Where<SOShipLine.shipmentNbr, IsNull, And2<Where<PX.Objects.SO.SOOrderType.requireShipping, Equal<False>, Or<PX.Objects.SO.SOLine.lineType, Equal<SOLineType.miscCharge>>>, And<PX.Objects.SO.SOOrderShipment.shipmentType, Equal<PX.Objects.AR.ARTran.sOShipmentType>, And<PX.Objects.SO.SOOrderShipment.shipmentNbr, Equal<PX.Objects.AR.ARTran.sOShipmentNbr>, Or<PX.Objects.SO.SOOrderShipment.shipmentType, Equal<SOShipLine.shipmentType>, And<PX.Objects.SO.SOOrderShipment.shipmentNbr, Equal<SOShipLine.shipmentNbr>, Or<PX.Objects.SO.SOOrderShipment.shipmentType, Equal<INDocType.dropShip>, And<PX.Objects.SO.SOOrderShipment.shipmentNbr, Equal<PX.Objects.PO.POReceiptLine.receiptNbr>>>>>>>>>>>>>>>>>>>, Where<PX.Objects.SO.SOLine.origOrderType, Equal<CurrentValue<SOOrderRelatedReturnsSPFilter.orderType>>, And<PX.Objects.SO.SOLine.origOrderNbr, Equal<CurrentValue<SOOrderRelatedReturnsSPFilter.orderNbr>>>>, Aggregate<GroupBy<PX.Objects.SO.SOLine.orderType, GroupBy<PX.Objects.SO.SOLine.orderNbr, GroupBy<PX.Objects.SO.SOOrderShipment.shippingRefNoteID, GroupBy<PX.Objects.AR.ARRegister.docType, GroupBy<PX.Objects.AR.ARRegister.refNbr, GroupBy<PX.Objects.AP.APTran.tranType, GroupBy<PX.Objects.AP.APTran.refNbr>>>>>>>>>), Persistent = false)]
public class SOReturnShipped : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLine.orderType))]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.SO.SOLine.orderNbr))]
  public virtual string OrderNbr { get; set; }

  [PXDBGuid(false, IsKey = true, BqlField = typeof (PX.Objects.SO.SOOrderShipment.shippingRefNoteID))]
  public virtual Guid? ShippingRefNoteID { get; set; }

  [PXDBString(3, IsFixed = true, BqlField = typeof (PX.Objects.AR.ARRegister.docType))]
  public virtual string ARDocType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.AR.ARRegister.refNbr))]
  public virtual string ARRefNbr { get; set; }

  [PXDBGuid(false, BqlField = typeof (PX.Objects.AR.ARRegister.noteID))]
  public virtual Guid? ARNoteID { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLine.origOrderType))]
  public virtual string OrigOrderType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOLine.origOrderNbr))]
  public virtual string OrigOrderNbr { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOOrderShipment.shipmentType))]
  public virtual string ShipmentType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOOrderShipment.shipmentNbr))]
  public virtual string ShipmentNbr { get; set; }

  [PXDBString(3, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOOrderShipment.invoiceType))]
  public virtual string InvoiceType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOOrderShipment.invoiceNbr))]
  public virtual string InvoiceNbr { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOOrderShipment.invoiceReleased))]
  public virtual bool? InvoiceReleased { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOOrderShipment.invtDocType))]
  public virtual string InvtDocType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOOrderShipment.invtRefNbr))]
  public virtual string InvtRefNbr { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.AP.APTran.tranType))]
  public virtual string APDocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.AP.APTran.refNbr))]
  public virtual string APRefNbr { get; set; }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOReturnShipped.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOReturnShipped.orderNbr>
  {
  }

  public abstract class shippingRefNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOReturnShipped.shippingRefNoteID>
  {
  }

  public abstract class aRDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOReturnShipped.aRDocType>
  {
  }

  public abstract class aRRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOReturnShipped.aRRefNbr>
  {
  }

  public abstract class aRNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOReturnShipped.aRNoteID>
  {
  }

  public abstract class origOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOReturnShipped.origOrderType>
  {
  }

  public abstract class origOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOReturnShipped.origOrderNbr>
  {
  }

  public abstract class shipmentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOReturnShipped.shipmentType>
  {
  }

  public abstract class shipmentNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOReturnShipped.shipmentNbr>
  {
  }

  public abstract class invoiceType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOReturnShipped.invoiceType>
  {
  }

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOReturnShipped.invoiceNbr>
  {
  }

  public abstract class invoiceReleased : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOReturnShipped.invoiceReleased>
  {
  }

  public abstract class invtDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOReturnShipped.invtDocType>
  {
  }

  public abstract class invtRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOReturnShipped.invtRefNbr>
  {
  }

  public abstract class aPDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOReturnShipped.aPDocType>
  {
  }

  public abstract class aPRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOReturnShipped.aPRefNbr>
  {
  }
}
