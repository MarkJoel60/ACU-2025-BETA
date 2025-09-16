// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Navigate.SOOrderShipment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.SO.Navigate;

/// <summary>
/// SOOrderShipment DAC for proper navigation purposes. Keys are overriden: OrderType, OrderNbr, ShipmentType, ShipmentNbr
/// </summary>
[PXPrimaryGraph(new Type[] {typeof (SOShipmentEntry), typeof (POReceiptEntry), typeof (SOInvoiceEntry)}, new Type[] {typeof (Select2<PX.Objects.SO.SOShipment, LeftJoin<PX.Objects.IN.INSite, On<PX.Objects.SO.SOShipment.FK.Site>>, Where<PX.Objects.SO.SOShipment.noteID, Equal<Current<SOOrderShipment.shippingRefNoteID>>, And<INDocType.dropShip, NotEqual<Current<SOOrderShipment.shipmentType>>, And<Where<PX.Objects.IN.INSite.siteID, IsNull, Or<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>>>>>), typeof (Select<PX.Objects.PO.POReceipt, Where<PX.Objects.PO.POReceipt.noteID, Equal<Current<SOOrderShipment.shippingRefNoteID>>, And<INDocType.dropShip, Equal<Current<SOOrderShipment.shipmentType>>>>>), typeof (Select<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.noteID, Equal<Current<SOOrderShipment.shippingRefNoteID>>, And<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleSO>>>>)}, UseParent = false)]
[PXHidden]
[Serializable]
public class SOOrderShipment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsKey = true, IsFixed = true)]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true)]
  public virtual string OrderNbr { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? ShippingRefNoteID { get; set; }

  [PXDBString(1, IsKey = true, IsFixed = true)]
  public virtual string ShipmentType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = "")]
  public virtual string ShipmentNbr { get; set; }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderShipment.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderShipment.orderNbr>
  {
  }

  public abstract class shippingRefNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOOrderShipment.shippingRefNoteID>
  {
  }

  public abstract class shipmentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderShipment.shipmentType>
  {
  }

  public abstract class shipmentNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderShipment.shipmentNbr>
  {
  }
}
