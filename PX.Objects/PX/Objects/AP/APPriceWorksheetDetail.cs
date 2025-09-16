// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPriceWorksheetDetail
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

[PXCacheName("AP Price Worksheet Detail")]
[PXProjection(typeof (Select2<APPriceWorksheetDetail, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<APPriceWorksheetDetail.inventoryID>>>>), new System.Type[] {typeof (APPriceWorksheetDetail)})]
[Serializable]
public class APPriceWorksheetDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _RefNbr;
  protected int? _LineID;
  protected int? _VendorID;
  protected int? _InventoryID;
  protected string _AlternateID;
  protected int? _SubItemID;
  protected string _Description;
  protected string _UOM;
  protected Decimal? _BreakQty;
  protected Decimal? _CurrentPrice;
  protected int? _SiteID;
  protected string _TaxID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDBDefault(typeof (APPriceWorksheet.refNbr))]
  [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
  [PXParent(typeof (Select<APPriceWorksheet, Where<APPriceWorksheet.refNbr, Equal<Current<APPriceWorksheetDetail.refNbr>>>>))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBIdentity(IsKey = true)]
  public virtual int? LineID
  {
    get => this._LineID;
    set => this._LineID = value;
  }

  [Vendor]
  [PXDefault]
  [PXParent(typeof (Select<Vendor, Where<Vendor.bAccountID, Equal<Current<APPriceWorksheetDetail.vendorID>>>>))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [InventoryByAlternateID(typeof (APPriceWorksheetDetail.vendorID), typeof (APPriceWorksheetDetail.alternateID), typeof (INAlternateType.vPN), typeof (APPriceWorksheetDetail.restrictInventoryByAlternateID))]
  [PXParent(typeof (Select<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<APPriceWorksheetDetail.inventoryID>>>>))]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString(IsUnicode = true, BqlField = typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXFormula(typeof (Selector<APPriceWorksheetDetail.inventoryID, PX.Objects.IN.InventoryItem.inventoryCD>))]
  public virtual string InventoryCD { get; set; }

  [PriceWorksheetAlternateItem]
  public virtual string AlternateID
  {
    get => this._AlternateID;
    set => this._AlternateID = value;
  }

  [SubItem(typeof (APPriceWorksheetDetail.inventoryID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBLocalizableString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (PX.Objects.IN.InventoryItem.descr), IsProjection = true)]
  [PXFormula(typeof (Selector<APPriceWorksheetDetail.inventoryID, PX.Objects.IN.InventoryItem.descr>))]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDefault]
  [INUnit(typeof (APPriceWorksheetDetail.inventoryID))]
  [PXFormula(typeof (Switch<Case<Where<BqlOperand<APPriceWorksheetDetail.restrictInventoryByAlternateID, IBqlBool>.IsNotEqual<True>>, Selector<APPriceWorksheetDetail.inventoryID, PX.Objects.IN.InventoryItem.purchaseUnit>>, APPriceWorksheetDetail.uOM>))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Break Qty", Visibility = PXUIVisibility.Visible, Enabled = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BreakQty
  {
    get => this._BreakQty;
    set => this._BreakQty = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBPriceCost]
  [PXUIField(DisplayName = "Source Price", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual Decimal? CurrentPrice
  {
    get => this._CurrentPrice;
    set => this._CurrentPrice = value;
  }

  [PXDBPriceCost(true)]
  [PXUIField(DisplayName = "Pending Price", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? PendingPrice { get; set; }

  [PXDBString(5)]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID), CacheGlobal = true)]
  [PXUIField(DisplayName = "Currency")]
  public virtual string CuryID { get; set; }

  [NullableSite]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXUIField(DisplayName = "Tax", Visibility = PXUIVisibility.Visible, Enabled = true)]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr))]
  [PXDBString(60)]
  public virtual string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
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

  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? RestrictInventoryByAlternateID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the item.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote(BqlField = typeof (PX.Objects.IN.InventoryItem.noteID))]
  public virtual Guid? NoteID { get; set; }

  public class PK : 
    PrimaryKeyOf<APPriceWorksheetDetail>.By<APPriceWorksheetDetail.refNbr, APPriceWorksheetDetail.lineID>
  {
    public static APPriceWorksheetDetail Find(
      PXGraph graph,
      string refNbr,
      int? lineID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APPriceWorksheetDetail>.By<APPriceWorksheetDetail.refNbr, APPriceWorksheetDetail.lineID>.FindBy(graph, (object) refNbr, (object) lineID, options);
    }
  }

  public static class FK
  {
    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APPriceWorksheetDetail>.By<APPriceWorksheetDetail.vendorID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<APPriceWorksheetDetail>.By<APPriceWorksheetDetail.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<APPriceWorksheetDetail>.By<APPriceWorksheetDetail.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<APPriceWorksheetDetail>.By<APPriceWorksheetDetail.siteID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<APPriceWorksheetDetail>.By<APPriceWorksheetDetail.curyID>
    {
    }

    public class Tax : 
      PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<APPriceWorksheetDetail>.By<APPriceWorksheetDetail.taxID>
    {
    }
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPriceWorksheetDetail.refNbr>
  {
  }

  public abstract class lineID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPriceWorksheetDetail.lineID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPriceWorksheetDetail.vendorID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPriceWorksheetDetail.inventoryID>
  {
  }

  public abstract class inventoryCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPriceWorksheetDetail.inventoryCD>
  {
  }

  public abstract class alternateID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPriceWorksheetDetail.alternateID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPriceWorksheetDetail.subItemID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPriceWorksheetDetail.description>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPriceWorksheetDetail.uOM>
  {
  }

  public abstract class breakQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPriceWorksheetDetail.breakQty>
  {
  }

  public abstract class currentPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPriceWorksheetDetail.currentPrice>
  {
  }

  public abstract class pendingPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPriceWorksheetDetail.pendingPrice>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPriceWorksheetDetail.curyID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPriceWorksheetDetail.siteID>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPriceWorksheetDetail.taxID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APPriceWorksheetDetail.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APPriceWorksheetDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPriceWorksheetDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPriceWorksheetDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APPriceWorksheetDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPriceWorksheetDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPriceWorksheetDetail.lastModifiedDateTime>
  {
  }

  public abstract class restrictInventoryByAlternateID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APPriceWorksheetDetail.restrictInventoryByAlternateID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APPriceWorksheetDetail.noteID>
  {
  }
}
