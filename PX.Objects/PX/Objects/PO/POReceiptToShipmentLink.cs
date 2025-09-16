// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptToShipmentLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.PO;

/// <summary>Purchase Receipt to Shipment Link</summary>
[PXCacheName("Purchase Receipt to Shipment Link")]
public class POReceiptToShipmentLink : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The type of the purchase receipt.
  /// This field is a part of the compound reference to the purchasing document (<see cref="T:PX.Objects.PO.POReceipt" />).
  /// The full reference contains the <see cref="T:PX.Objects.PO.POReceiptToShipmentLink.receiptType" /> and <see cref="T:PX.Objects.PO.POReceiptToShipmentLink.receiptNbr" /> fields.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PO.POReceiptType.ListAttribute" />.
  /// </value>
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault]
  [POReceiptType.List]
  [PXUIField(DisplayName = "Receipt Type")]
  public virtual string? ReceiptType { get; set; }

  /// <summary>
  /// The number of the purchase receipt.
  /// This field is a part of the compound reference to the purchasing document (<see cref="T:PX.Objects.PO.POReceipt" />).
  /// The full reference contains the <see cref="T:PX.Objects.PO.POReceiptToShipmentLink.receiptType" /> and <see cref="T:PX.Objects.PO.POReceiptToShipmentLink.receiptNbr" /> fields.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXParent(typeof (POReceiptToShipmentLink.FK.Receipt))]
  [PXUIField(DisplayName = "Receipt Nbr.")]
  public virtual string? ReceiptNbr { get; set; }

  /// <summary>
  /// The type of the shipment.
  /// This field is a part of the compound reference to the shipment document (<see cref="T:PX.Objects.SO.SOShipment" />).
  /// The full reference contains the <see cref="T:PX.Objects.PO.POReceiptToShipmentLink.sOShipmentType" /> and <see cref="T:PX.Objects.PO.POReceiptToShipmentLink.sOShipmentNbr" /> fields.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.SO.SOShipmentType.ListAttribute" />.
  /// </value>
  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault]
  [PX.Objects.SO.SOShipmentType.List]
  [PXUIField(DisplayName = "Shipment Type")]
  public virtual string? SOShipmentType { get; set; }

  /// <summary>
  /// The number of the shipment.
  /// This field is a part of the compound reference to the shipment document (<see cref="T:PX.Objects.SO.SOShipment" />).
  /// The full reference contains the <see cref="T:PX.Objects.PO.POReceiptToShipmentLink.sOShipmentType" /> and <see cref="T:PX.Objects.PO.POReceiptToShipmentLink.sOShipmentNbr" /> fields.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXParent(typeof (POReceiptToShipmentLink.FK.SOShipment), LeaveChildren = true)]
  [PXUIField(DisplayName = "Shipment Nbr.")]
  public virtual string? SOShipmentNbr { get; set; }

  /// <summary>
  /// The type of the sales order.
  /// This field is a part of the compound reference to the sales order document (<see cref="T:PX.Objects.SO.SOOrder" />).
  /// The full reference contains the <see cref="T:PX.Objects.PO.POReceiptToShipmentLink.sOOrderType" /> and <see cref="T:PX.Objects.PO.POReceiptToShipmentLink.sOOrderNbr" /> fields.
  /// </summary>
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Sales Order Type")]
  public virtual string? SOOrderType { get; set; }

  /// <summary>
  /// The number of the sales order.
  /// This field is a part of the compound reference to the sales order document (<see cref="T:PX.Objects.SO.SOOrder" />).
  /// The full reference contains the <see cref="T:PX.Objects.PO.POReceiptToShipmentLink.sOOrderType" /> and <see cref="T:PX.Objects.PO.POReceiptToShipmentLink.sOOrderNbr" /> fields.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXParent(typeof (POReceiptToShipmentLink.FK.SOOrder), LeaveChildren = true)]
  [PXUIField(DisplayName = "Sales Order Nbr.")]
  public virtual string? SOOrderNbr { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string? CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string? LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[]? tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<POReceiptToShipmentLink>.By<POReceiptToShipmentLink.receiptType, POReceiptToShipmentLink.receiptNbr, POReceiptToShipmentLink.sOShipmentType, POReceiptToShipmentLink.sOShipmentNbr, POReceiptToShipmentLink.sOOrderType, POReceiptToShipmentLink.sOOrderNbr>
  {
    public static POReceiptToShipmentLink Find(
      PXGraph graph,
      string receiptType,
      string receiptNbr,
      string shipmentType,
      string shipmentNbr,
      string sOOrderType,
      string sOOrderNbr,
      PKFindOptions options = 0)
    {
      return (POReceiptToShipmentLink) PrimaryKeyOf<POReceiptToShipmentLink>.By<POReceiptToShipmentLink.receiptType, POReceiptToShipmentLink.receiptNbr, POReceiptToShipmentLink.sOShipmentType, POReceiptToShipmentLink.sOShipmentNbr, POReceiptToShipmentLink.sOOrderType, POReceiptToShipmentLink.sOOrderNbr>.FindBy(graph, (object) receiptType, (object) receiptNbr, (object) shipmentType, (object) shipmentNbr, (object) sOOrderType, (object) sOOrderNbr, options);
    }
  }

  public static class FK
  {
    public class SOOrderShipment : 
      PrimaryKeyOf<
      #nullable disable
      PX.Objects.SO.SOOrderShipment>.By<PX.Objects.SO.SOOrderShipment.shipmentType, PX.Objects.SO.SOOrderShipment.shipmentNbr, PX.Objects.SO.SOOrderShipment.orderType, PX.Objects.SO.SOOrderShipment.orderNbr>.ForeignKeyOf<
      #nullable enable
      POReceiptToShipmentLink>.By<POReceiptToShipmentLink.sOShipmentType, POReceiptToShipmentLink.sOShipmentNbr, POReceiptToShipmentLink.sOOrderType, POReceiptToShipmentLink.sOOrderNbr>
    {
    }

    public class SOOrder : 
      PrimaryKeyOf<
      #nullable disable
      PX.Objects.SO.SOOrder>.By<PX.Objects.SO.SOOrder.orderType, PX.Objects.SO.SOOrder.orderNbr>.ForeignKeyOf<
      #nullable enable
      POReceiptToShipmentLink>.By<POReceiptToShipmentLink.sOOrderType, POReceiptToShipmentLink.sOOrderNbr>
    {
    }

    public class SOShipment : 
      PrimaryKeyOf<
      #nullable disable
      PX.Objects.SO.SOShipment>.By<PX.Objects.SO.SOShipment.shipmentType, PX.Objects.SO.SOShipment.shipmentNbr>.ForeignKeyOf<
      #nullable enable
      POReceiptToShipmentLink>.By<POReceiptToShipmentLink.sOShipmentType, POReceiptToShipmentLink.sOShipmentNbr>
    {
    }

    public class Receipt : 
      PrimaryKeyOf<
      #nullable disable
      POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.ForeignKeyOf<
      #nullable enable
      POReceiptToShipmentLink>.By<POReceiptToShipmentLink.receiptType, POReceiptToShipmentLink.receiptNbr>
    {
    }
  }

  public abstract class receiptType : 
    BqlType<IBqlString, string>.Field<POReceiptToShipmentLink.receiptType>
  {
  }

  public abstract class receiptNbr : 
    BqlType<IBqlString, string>.Field<POReceiptToShipmentLink.receiptNbr>
  {
  }

  public abstract class sOShipmentType : 
    BqlType<IBqlString, string>.Field<POReceiptToShipmentLink.sOShipmentType>
  {
  }

  public abstract class sOShipmentNbr : 
    BqlType<IBqlString, string>.Field<POReceiptToShipmentLink.sOShipmentNbr>
  {
  }

  public abstract class sOOrderType : 
    BqlType<IBqlString, string>.Field<POReceiptToShipmentLink.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : 
    BqlType<IBqlString, string>.Field<POReceiptToShipmentLink.sOOrderNbr>
  {
  }

  public abstract class createdByID : 
    BqlType<IBqlGuid, Guid>.Field<POReceiptToShipmentLink.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<IBqlString, string>.Field<POReceiptToShipmentLink.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<IBqlDateTime, DateTime>.Field<POReceiptToShipmentLink.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<IBqlGuid, Guid>.Field<POReceiptToShipmentLink.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<IBqlString, string>.Field<POReceiptToShipmentLink.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<IBqlDateTime, DateTime>.Field<POReceiptToShipmentLink.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<IBqlByteArray, byte[]>.Field<POReceiptToShipmentLink.Tstamp>
  {
  }
}
