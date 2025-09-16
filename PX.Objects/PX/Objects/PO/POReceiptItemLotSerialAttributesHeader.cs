// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptItemLotSerialAttributesHeader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using PX.Objects.IN.DAC;
using System;

#nullable enable
namespace PX.Objects.PO;

/// <exclude />
[PXCacheName("POReceiptItemLotSerialAttributesHeader")]
public class POReceiptItemLotSerialAttributesHeader : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ILotSerialAttributesHeader
{
  [PXUIField(DisplayName = "Type", Enabled = false)]
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault(typeof (POReceipt.receiptType))]
  [INDocType.List]
  public virtual 
  #nullable disable
  string ReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (POReceipt.receiptNbr))]
  [PXParent(typeof (POReceiptItemLotSerialAttributesHeader.FK.POReceipt))]
  [PXUIField(DisplayName = "Receipt Nbr.", Enabled = false)]
  public virtual string ReceiptNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (POReceiptLineSplit.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PX.Objects.IN.LotSerialNbr(IsKey = true)]
  [PXDefault(typeof (POReceiptLineSplit.lotSerialNbr))]
  public virtual string LotSerialNbr { get; set; }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Manufacturer Lot/Serial Nbr.")]
  public virtual string MfgLotSerialNbr { get; set; }

  [PXNote]
  [PXReplaceUDFConfiguration(typeof (PX.Objects.IN.DAC.INItemLotSerialAttributesHeader))]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<POReceiptItemLotSerialAttributesHeader>.By<POReceiptItemLotSerialAttributesHeader.receiptType, POReceiptItemLotSerialAttributesHeader.receiptNbr, POReceiptItemLotSerialAttributesHeader.inventoryID, POReceiptItemLotSerialAttributesHeader.lotSerialNbr>
  {
    public static POReceiptItemLotSerialAttributesHeader Find(
      PXGraph graph,
      string receiptType,
      string receiptNbr,
      int? inventoryID,
      string lotSerialNbr,
      PKFindOptions options = 0)
    {
      return (POReceiptItemLotSerialAttributesHeader) PrimaryKeyOf<POReceiptItemLotSerialAttributesHeader>.By<POReceiptItemLotSerialAttributesHeader.receiptType, POReceiptItemLotSerialAttributesHeader.receiptNbr, POReceiptItemLotSerialAttributesHeader.inventoryID, POReceiptItemLotSerialAttributesHeader.lotSerialNbr>.FindBy(graph, (object) receiptType, (object) receiptNbr, (object) inventoryID, (object) lotSerialNbr, options);
    }
  }

  public static class FK
  {
    public class POReceipt : 
      PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.ForeignKeyOf<POReceiptItemLotSerialAttributesHeader>.By<POReceiptItemLotSerialAttributesHeader.receiptType, POReceiptItemLotSerialAttributesHeader.receiptNbr>
    {
    }

    public class INItemLotSerialAttributesHeader : 
      PrimaryKeyOf<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.By<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.inventoryID, PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.lotSerialNbr>.ForeignKeyOf<INRegisterItemLotSerialAttributesHeader>.By<POReceiptItemLotSerialAttributesHeader.inventoryID, POReceiptItemLotSerialAttributesHeader.lotSerialNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<POReceiptItemLotSerialAttributesHeader>.By<POReceiptItemLotSerialAttributesHeader.inventoryID>
    {
    }
  }

  public abstract class receiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptItemLotSerialAttributesHeader.receiptType>
  {
  }

  public abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptItemLotSerialAttributesHeader.receiptNbr>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptItemLotSerialAttributesHeader.inventoryID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptItemLotSerialAttributesHeader.lotSerialNbr>
  {
  }

  public abstract class mfgLotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptItemLotSerialAttributesHeader.mfgLotSerialNbr>
  {
  }

  public abstract class noteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POReceiptItemLotSerialAttributesHeader.noteID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POReceiptItemLotSerialAttributesHeader.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptItemLotSerialAttributesHeader.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptItemLotSerialAttributesHeader.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POReceiptItemLotSerialAttributesHeader.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptItemLotSerialAttributesHeader.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptItemLotSerialAttributesHeader.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    POReceiptItemLotSerialAttributesHeader.Tstamp>
  {
  }
}
