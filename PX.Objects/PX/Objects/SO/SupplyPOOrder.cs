// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SupplyPOOrder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXHidden]
[PXProjection(typeof (Select<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.isLegacyDropShip, Equal<boolFalse>>>), Persistent = true)]
public class SupplyPOOrder : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDefault]
  [POOrderType.List]
  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.PO.POOrder.orderType))]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXDefault]
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (PX.Objects.PO.POOrder.orderNbr))]
  public virtual string OrderNbr { get; set; }

  [PXDefault]
  [PXUIField]
  [POOrderStatus.List]
  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.PO.POOrder.status))]
  public virtual string Status { get; set; }

  [PXDefault]
  [PXDBBool(BqlField = typeof (PX.Objects.PO.POOrder.hold))]
  public virtual bool? Hold { get; set; }

  [PXDefault]
  [PXDBBool(BqlField = typeof (PX.Objects.PO.POOrder.approved))]
  public virtual bool? Approved { get; set; }

  [PXDefault]
  [PXDBBool(BqlField = typeof (PX.Objects.PO.POOrder.cancelled))]
  public virtual bool? Cancelled { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POOrder.vendorID))]
  public virtual int? VendorID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POOrder.vendorLocationID))]
  public virtual int? VendorLocationID { get; set; }

  [PXDBString(40, IsUnicode = true, BqlField = typeof (PX.Objects.PO.POOrder.vendorRefNbr))]
  public virtual string VendorRefNbr { get; set; }

  [PXParent(typeof (SupplyPOOrder.FK.DemandOrder), LeaveChildren = true)]
  [PXDBString(2, IsFixed = true, InputMask = ">aa", BqlField = typeof (PX.Objects.PO.POOrder.sOOrderType))]
  public virtual string SOOrderType { get; set; }

  [PXDBDefault(typeof (SOOrder.orderNbr))]
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (PX.Objects.PO.POOrder.sOOrderNbr))]
  public virtual string SOOrderNbr { get; set; }

  [POShippingDestination.List]
  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.PO.POOrder.shipDestType))]
  public virtual string ShipDestType { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POOrder.shipToBAccountID))]
  public virtual int? ShipToBAccountID { get; set; }

  [PXDefault]
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  [PXDBInt(BqlField = typeof (PX.Objects.PO.POOrder.dropShipLinesCount))]
  public virtual int? DropShipLinesCount { get; set; }

  [PXDefault]
  [PXDBInt(BqlField = typeof (PX.Objects.PO.POOrder.dropShipLinkedLinesCount))]
  public virtual int? DropShipLinkedLinesCount { get; set; }

  [PXDefault]
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  [PXDBInt(BqlField = typeof (PX.Objects.PO.POOrder.dropShipActiveLinksCount))]
  public virtual int? DropShipActiveLinksCount { get; set; }

  [PXDefault]
  [PXDBInt(BqlField = typeof (PX.Objects.PO.POOrder.dropShipNotLinkedLinesCntr))]
  public virtual int? DropShipNotLinkedLinesCntr { get; set; }

  [PXDefault]
  [PXDBBool(BqlField = typeof (PX.Objects.PO.POOrder.isLegacyDropShip))]
  public virtual bool? IsLegacyDropShip { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POOrder.specialLineCntr))]
  public virtual int? SpecialLineCntr { get; set; }

  [PXNote(BqlField = typeof (PX.Objects.PO.POOrder.noteID))]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<SupplyPOOrder>.By<SupplyPOOrder.orderType, SupplyPOOrder.orderNbr>
  {
    public static SupplyPOOrder Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      PKFindOptions options = 0)
    {
      return (SupplyPOOrder) PrimaryKeyOf<SupplyPOOrder>.By<SupplyPOOrder.orderType, SupplyPOOrder.orderNbr>.FindBy(graph, (object) orderType, (object) orderNbr, options);
    }

    public static SupplyPOOrder FindDirty(PXGraph graph, string orderType, string orderNbr)
    {
      return PXResultset<SupplyPOOrder>.op_Implicit(PXSelectBase<SupplyPOOrder, PXSelect<SupplyPOOrder, Where<SupplyPOOrder.orderType, Equal<Required<SupplyPOOrder.orderType>>, And<SupplyPOOrder.orderNbr, Equal<Required<SupplyPOOrder.orderNbr>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
      {
        (object) orderType,
        (object) orderNbr
      }));
    }
  }

  public static class FK
  {
    public class DemandOrder : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SupplyPOOrder>.By<SupplyPOOrder.sOOrderType, SupplyPOOrder.sOOrderNbr>
    {
    }
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SupplyPOOrder.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SupplyPOOrder.orderNbr>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SupplyPOOrder.status>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SupplyPOOrder.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SupplyPOOrder.approved>
  {
  }

  public abstract class cancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SupplyPOOrder.cancelled>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SupplyPOOrder.vendorID>
  {
  }

  public abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SupplyPOOrder.vendorLocationID>
  {
  }

  public abstract class vendorRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SupplyPOOrder.vendorRefNbr>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SupplyPOOrder.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SupplyPOOrder.sOOrderNbr>
  {
  }

  public abstract class shipDestType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SupplyPOOrder.shipDestType>
  {
  }

  public abstract class shipToBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SupplyPOOrder.shipToBAccountID>
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  public abstract class dropShipLinesCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SupplyPOOrder.dropShipLinesCount>
  {
  }

  public abstract class dropShipLinkedLinesCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SupplyPOOrder.dropShipLinkedLinesCount>
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  public abstract class dropShipActiveLinksCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SupplyPOOrder.dropShipActiveLinksCount>
  {
  }

  public abstract class dropShipNotLinkedLinesCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SupplyPOOrder.dropShipNotLinkedLinesCntr>
  {
  }

  public abstract class isLegacyDropShip : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SupplyPOOrder.isLegacyDropShip>
  {
  }

  public abstract class specialLineCntr : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SupplyPOOrder.specialLineCntr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SupplyPOOrder.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SupplyPOOrder.Tstamp>
  {
  }
}
