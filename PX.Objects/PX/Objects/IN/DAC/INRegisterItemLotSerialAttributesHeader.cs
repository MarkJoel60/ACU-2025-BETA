// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.INRegisterItemLotSerialAttributesHeader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN.DAC;

/// <exclude />
[PXCacheName("INRegisterItemLotSerialAttributesHeader")]
public class INRegisterItemLotSerialAttributesHeader : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ILotSerialAttributesHeader
{
  [PXUIField(DisplayName = "Document Type", Enabled = false)]
  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault(typeof (PX.Objects.IN.INRegister.docType))]
  [INDocType.List]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.IN.INRegister.refNbr))]
  [PXParent(typeof (INRegisterItemLotSerialAttributesHeader.FK.INRegister))]
  [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (INTranSplit.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PX.Objects.IN.LotSerialNbr(IsKey = true)]
  [PXDefault(typeof (INTranSplit.lotSerClassID))]
  public virtual string LotSerialNbr { get; set; }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Manufacturer Lot/Serial Nbr.")]
  public virtual string MfgLotSerialNbr { get; set; }

  [PXNote]
  [PXReplaceUDFConfiguration(typeof (INItemLotSerialAttributesHeader))]
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
    PrimaryKeyOf<INRegisterItemLotSerialAttributesHeader>.By<INRegisterItemLotSerialAttributesHeader.docType, INRegisterItemLotSerialAttributesHeader.refNbr, INRegisterItemLotSerialAttributesHeader.inventoryID, INRegisterItemLotSerialAttributesHeader.lotSerialNbr>
  {
    public static INRegisterItemLotSerialAttributesHeader Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? inventoryID,
      string lotSerialNbr,
      PKFindOptions options = 0)
    {
      return (INRegisterItemLotSerialAttributesHeader) PrimaryKeyOf<INRegisterItemLotSerialAttributesHeader>.By<INRegisterItemLotSerialAttributesHeader.docType, INRegisterItemLotSerialAttributesHeader.refNbr, INRegisterItemLotSerialAttributesHeader.inventoryID, INRegisterItemLotSerialAttributesHeader.lotSerialNbr>.FindBy(graph, (object) docType, (object) refNbr, (object) inventoryID, (object) lotSerialNbr, options);
    }
  }

  public static class FK
  {
    public class INRegister : 
      PrimaryKeyOf<PX.Objects.IN.INRegister>.By<PX.Objects.IN.INRegister.docType, PX.Objects.IN.INRegister.refNbr>.ForeignKeyOf<INRegisterItemLotSerialAttributesHeader>.By<INRegisterItemLotSerialAttributesHeader.docType, INRegisterItemLotSerialAttributesHeader.refNbr>
    {
    }

    public class INItemLotSerialAttributesHeader : 
      PrimaryKeyOf<INItemLotSerialAttributesHeader>.By<INItemLotSerialAttributesHeader.inventoryID, INItemLotSerialAttributesHeader.lotSerialNbr>.ForeignKeyOf<INRegisterItemLotSerialAttributesHeader>.By<INRegisterItemLotSerialAttributesHeader.inventoryID, INRegisterItemLotSerialAttributesHeader.lotSerialNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<INRegisterItemLotSerialAttributesHeader>.By<INRegisterItemLotSerialAttributesHeader.inventoryID>
    {
    }
  }

  public abstract class docType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRegisterItemLotSerialAttributesHeader.docType>
  {
  }

  public abstract class refNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRegisterItemLotSerialAttributesHeader.refNbr>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INRegisterItemLotSerialAttributesHeader.inventoryID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRegisterItemLotSerialAttributesHeader.lotSerialNbr>
  {
  }

  public abstract class mfgLotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRegisterItemLotSerialAttributesHeader.mfgLotSerialNbr>
  {
  }

  public abstract class noteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INRegisterItemLotSerialAttributesHeader.noteID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INRegisterItemLotSerialAttributesHeader.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRegisterItemLotSerialAttributesHeader.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INRegisterItemLotSerialAttributesHeader.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INRegisterItemLotSerialAttributesHeader.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRegisterItemLotSerialAttributesHeader.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INRegisterItemLotSerialAttributesHeader.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    INRegisterItemLotSerialAttributesHeader.Tstamp>
  {
  }
}
