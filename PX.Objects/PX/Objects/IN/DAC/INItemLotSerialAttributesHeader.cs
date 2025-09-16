// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.INItemLotSerialAttributesHeader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.IN.DAC;

/// <exclude />
[PXCacheName("INItemLotSerialAttributesHeader")]
public class INItemLotSerialAttributesHeader : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ILotSerialAttributesHeader
{
  [StockItem(IsKey = true)]
  [PXDefault]
  public virtual int? InventoryID { get; set; }

  [PXDefault]
  [PX.Objects.IN.LotSerialNbr(IsKey = true)]
  public virtual 
  #nullable disable
  string LotSerialNbr { get; set; }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Manufacturer Lot/Serial Nbr.")]
  public virtual string MfgLotSerialNbr { get; set; }

  /// <summary>The description of the particular lot\serial item.</summary>
  [PXDBLocalizableString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr { get; set; }

  /// <summary>
  /// The URL of the image associated with the particular lot\serial item.
  /// </summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Image")]
  [PXAttachedFile]
  public virtual string ImageUrl { get; set; }

  /// <summary>
  /// Rich text description of the particular lot\serial item.
  /// </summary>
  [PXDBLocalizableString(IsUnicode = true)]
  [PXUIField(DisplayName = "Content")]
  public virtual string Body { get; set; }

  [PXDBPriceCost(true)]
  [CurySymbol(null, null, null, null, null, null, null, true, true)]
  [PXUIField(DisplayName = "Sales Price")]
  public virtual Decimal? SalesPrice { get; set; }

  [PXDBPriceCost(true)]
  [CurySymbol(null, null, null, null, null, null, null, true, true)]
  [PXUIField(DisplayName = "MSRP")]
  public virtual Decimal? RecPrice { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<INItemLotSerialAttributesHeader>.By<INItemLotSerialAttributesHeader.inventoryID, INItemLotSerialAttributesHeader.lotSerialNbr>
  {
    public static INItemLotSerialAttributesHeader Find(
      PXGraph graph,
      int? inventoryID,
      string lotSerialNbr,
      PKFindOptions options = 0)
    {
      return (INItemLotSerialAttributesHeader) PrimaryKeyOf<INItemLotSerialAttributesHeader>.By<INItemLotSerialAttributesHeader.inventoryID, INItemLotSerialAttributesHeader.lotSerialNbr>.FindBy(graph, (object) inventoryID, (object) lotSerialNbr, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<INItemLotSerialAttributesHeader>.By<INItemLotSerialAttributesHeader.inventoryID>
    {
    }

    public class INItemLotSerial : 
      PrimaryKeyOf<PX.Objects.IN.INItemLotSerial>.By<PX.Objects.IN.INItemLotSerial.inventoryID, PX.Objects.IN.INItemLotSerial.lotSerialNbr>.ForeignKeyOf<INItemLotSerialAttributesHeader>.By<INItemLotSerialAttributesHeader.inventoryID, INItemLotSerialAttributesHeader.lotSerialNbr>
    {
    }
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemLotSerialAttributesHeader.inventoryID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemLotSerialAttributesHeader.lotSerialNbr>
  {
  }

  public abstract class mfgLotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemLotSerialAttributesHeader.mfgLotSerialNbr>
  {
  }

  public abstract class descr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemLotSerialAttributesHeader.descr>
  {
  }

  public abstract class imageUrl : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemLotSerialAttributesHeader.imageUrl>
  {
  }

  public abstract class body : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemLotSerialAttributesHeader.body>
  {
  }

  public abstract class salesPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemLotSerialAttributesHeader.salesPrice>
  {
  }

  public abstract class recPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemLotSerialAttributesHeader.recPrice>
  {
  }

  public abstract class noteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INItemLotSerialAttributesHeader.noteID>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    INItemLotSerialAttributesHeader.Tstamp>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemLotSerialAttributesHeader.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemLotSerialAttributesHeader.lastModifiedDateTime>
  {
  }
}
