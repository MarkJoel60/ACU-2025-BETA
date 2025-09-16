// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APVendorPrice
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXCacheName("AP Vendor Price")]
[Serializable]
public class APVendorPrice : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _RecordID;
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _AlternateID;
  protected string _CuryID;
  protected string _UOM;
  protected bool? _IsPromotionalPrice;
  protected System.DateTime? _EffectiveDate;
  protected Decimal? _BreakQty;
  protected int? _SiteID;
  protected System.DateTime? _ExpirationDate;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [Vendor]
  [PXDefault]
  [PXParent(typeof (Select<Vendor, Where<Vendor.bAccountID, Equal<Current<APVendorPrice.vendorID>>>>))]
  public virtual int? VendorID { get; set; }

  [APCrossItem(BAccountField = typeof (APVendorPrice.vendorID), WarningOnNonUniqueSubstitution = true, AllowTemplateItems = true)]
  [PXParent(typeof (Select<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<APVendorPrice.inventoryID>>>>))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXUIField(DisplayName = "Alternate ID", Visible = false, Visibility = PXUIVisibility.Dynamic)]
  [PXDBString(50, IsUnicode = true, InputMask = "")]
  public virtual string AlternateID
  {
    get => this._AlternateID;
    set => this._AlternateID = value;
  }

  [PXDBString(5)]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID), CacheGlobal = true)]
  [PXUIField(DisplayName = "Currency", Required = false)]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDefault]
  [INUnit(typeof (APVendorPrice.inventoryID))]
  [PXFormula(typeof (Selector<APVendorPrice.inventoryID, PX.Objects.IN.InventoryItem.purchaseUnit>))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Promotional")]
  public virtual bool? IsPromotionalPrice
  {
    get => this._IsPromotionalPrice;
    set => this._IsPromotionalPrice = value;
  }

  [PXDefault(typeof (AccessInfo.businessDate), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXDBDate]
  [PXUIField(DisplayName = "Effective Date", Visibility = PXUIVisibility.Visible)]
  public virtual System.DateTime? EffectiveDate
  {
    get => this._EffectiveDate;
    set => this._EffectiveDate = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBPriceCost]
  [PXUIField(DisplayName = "Price", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? SalesPrice { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Break Qty", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? BreakQty
  {
    get => this._BreakQty;
    set => this._BreakQty = value;
  }

  [NullableSite]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Expiration Date", Visibility = PXUIVisibility.Visible)]
  public virtual System.DateTime? ExpirationDate
  {
    get => this._ExpirationDate;
    set => this._ExpirationDate = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : PrimaryKeyOf<APVendorPrice>.By<APVendorPrice.recordID>
  {
    public static APVendorPrice Find(PXGraph graph, int? recordID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APVendorPrice>.By<APVendorPrice.recordID>.FindBy(graph, (object) recordID, options);
    }
  }

  public static class FK
  {
    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APVendorPrice>.By<APVendorPrice.vendorID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<APVendorPrice>.By<APVendorPrice.inventoryID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<APVendorPrice>.By<APVendorPrice.curyID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<APVendorPrice>.By<APVendorPrice.siteID>
    {
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APVendorPrice.recordID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APVendorPrice.vendorID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APVendorPrice.inventoryID>
  {
  }

  public abstract class alternateID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APVendorPrice.alternateID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APVendorPrice.curyID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APVendorPrice.uOM>
  {
  }

  public abstract class isPromotionalPrice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APVendorPrice.isPromotionalPrice>
  {
  }

  public abstract class effectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APVendorPrice.effectiveDate>
  {
  }

  public abstract class salesPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APVendorPrice.salesPrice>
  {
  }

  public abstract class breakQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APVendorPrice.breakQty>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APVendorPrice.siteID>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APVendorPrice.expirationDate>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APVendorPrice.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APVendorPrice.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APVendorPrice.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APVendorPrice.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APVendorPrice.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APVendorPrice.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APVendorPrice.lastModifiedDateTime>
  {
  }
}
