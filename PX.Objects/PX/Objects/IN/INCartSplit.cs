// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INCartSplit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName]
public class INCartSplit : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Site(IsKey = true, Visible = false)]
  [PXParent(typeof (INCartSplit.FK.Site))]
  public int? SiteID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Cart ID")]
  [PXSelector(typeof (Search<INCart.cartID, Where<INCart.active, Equal<True>>>), SubstituteKey = typeof (INCart.cartCD), DescriptionField = typeof (INCart.descr))]
  [PXParent(typeof (INCartSplit.FK.Cart))]
  public int? CartID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (INCart))]
  [PXUIField(DisplayName = "Split Line Number")]
  public virtual int? SplitLineNbr { get; set; }

  [StockItem]
  [PXDefault]
  [PXForeignReference(typeof (INCartSplit.FK.InventoryItem))]
  public virtual int? InventoryID { get; set; }

  [SubItem(typeof (INCartSplit.inventoryID))]
  [PXDefault]
  [PXFormula(typeof (Default<INCartSplit.inventoryID>))]
  public virtual int? SubItemID { get; set; }

  [PX.Objects.IN.LotSerialNbr]
  public virtual 
  #nullable disable
  string LotSerialNbr { get; set; }

  [PXDBDate(InputMask = "d", DisplayMask = "d")]
  [PXUIField(DisplayName = "Expiration Date", FieldClass = "LotSerial")]
  public virtual DateTime? ExpireDate { get; set; }

  [INUnit(typeof (INCartSplit.inventoryID), DisplayName = "UOM")]
  [PXDefault]
  public virtual string UOM { get; set; }

  [PXDBQuantity(typeof (INCartSplit.uOM), typeof (INCartSplit.baseQty), MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseQty { get; set; }

  [Location(typeof (INCartSplit.siteID))]
  [PXDefault]
  public virtual int? FromLocationID { get; set; }

  [Location(typeof (INCartSplit.siteID))]
  public virtual int? ToLocationID { get; set; }

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
    PrimaryKeyOf<INCartSplit>.By<INCartSplit.siteID, INCartSplit.cartID, INCartSplit.splitLineNbr>
  {
    public static INCartSplit Find(
      PXGraph graph,
      int? siteID,
      int? cartID,
      int? splitLineNbr,
      PKFindOptions options = 0)
    {
      return (INCartSplit) PrimaryKeyOf<INCartSplit>.By<INCartSplit.siteID, INCartSplit.cartID, INCartSplit.splitLineNbr>.FindBy(graph, (object) siteID, (object) cartID, (object) splitLineNbr, options);
    }
  }

  public static class FK
  {
    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INCartSplit>.By<INCartSplit.siteID>
    {
    }

    public class Cart : 
      PrimaryKeyOf<INCart>.By<INCart.siteID, INCart.cartID>.ForeignKeyOf<INCartSplit>.By<INCartSplit.siteID, INCartSplit.cartID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INCartSplit>.By<INCartSplit.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INCartSplit>.By<INCartSplit.subItemID>
    {
    }

    public class FromLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INCartSplit>.By<INCartSplit.fromLocationID>
    {
    }

    public class ToLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INCartSplit>.By<INCartSplit.toLocationID>
    {
    }
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCartSplit.siteID>
  {
  }

  public abstract class cartID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCartSplit.cartID>
  {
  }

  public abstract class splitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCartSplit.splitLineNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCartSplit.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCartSplit.subItemID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INCartSplit.lotSerialNbr>
  {
  }

  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INCartSplit.expireDate>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INCartSplit.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INCartSplit.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INCartSplit.baseQty>
  {
  }

  public abstract class fromLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCartSplit.fromLocationID>
  {
  }

  public abstract class toLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCartSplit.toLocationID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INCartSplit.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INCartSplit.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INCartSplit.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INCartSplit.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INCartSplit.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INCartSplit.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INCartSplit.Tstamp>
  {
  }
}
