// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CROpportunityDiscountDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Discount;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXCacheName("Opportunity Discount")]
[Serializable]
public class CROpportunityDiscountDetail : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IDiscountDetail
{
  protected int? _RecordID;
  protected ushort? _LineNbr;
  protected bool? _SkipDiscount;
  protected 
  #nullable disable
  string _DiscountID;
  protected string _DiscountSequenceID;
  protected string _Type;
  protected long? _CuryInfoID;
  protected Decimal? _DiscountableAmt;
  protected Decimal? _CuryDiscountableAmt;
  protected Decimal? _DiscountableQty;
  protected Decimal? _DiscountAmt;
  protected Decimal? _CuryDiscountAmt;
  protected Decimal? _DiscountPct;
  protected int? _FreeItemID;
  protected Decimal? _FreeItemQty;
  protected bool? _IsManual;
  protected bool? _IsOrigDocDiscount;
  protected string _ExtDiscCode;
  protected string _Description;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBGuid(false, IsKey = true)]
  [PXDBDefault(typeof (CROpportunity.quoteNoteID))]
  [PXParent(typeof (Select<CROpportunity, Where<CROpportunity.quoteNoteID, Equal<Current<CROpportunityDiscountDetail.quoteID>>>>))]
  public virtual Guid? QuoteID { get; set; }

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [PXDBUShort]
  [PXLineNbr(typeof (CROpportunity))]
  public virtual ushort? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (Where<CROpportunityDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>, And<CROpportunityDiscountDetail.discountID, IsNotNull>>))]
  [PXUIField(DisplayName = "Skip Discount", Enabled = true)]
  public virtual bool? SkipDiscount
  {
    get => this._SkipDiscount;
    set => this._SkipDiscount = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXUIEnabled(typeof (Where<CROpportunityDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>>))]
  [PXUIField(DisplayName = "Discount ID")]
  [PXForeignReference(typeof (CROpportunityDiscountDetail.FK.DiscountSequence))]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXUIEnabled(typeof (Where<CROpportunityDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>>))]
  [PXUIField(DisplayName = "Sequence")]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  [PXDBString(1, IsKey = true)]
  [PXDefault]
  [DiscountType.List]
  [PXUIField(DisplayName = "Type")]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (CROpportunity.curyInfoID))]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBDecimal(4)]
  public virtual Decimal? DiscountableAmt
  {
    get => this._DiscountableAmt;
    set => this._DiscountableAmt = value;
  }

  [PXDBCurrency(typeof (CROpportunityDiscountDetail.curyInfoID), typeof (CROpportunityDiscountDetail.discountableAmt))]
  [PXUIField(DisplayName = "Discountable Amt.")]
  public virtual Decimal? CuryDiscountableAmt
  {
    get => this._CuryDiscountableAmt;
    set => this._CuryDiscountableAmt = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Discountable Qty.")]
  public virtual Decimal? DiscountableQty
  {
    get => this._DiscountableQty;
    set => this._DiscountableQty = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscountAmt
  {
    get => this._DiscountAmt;
    set => this._DiscountAmt = value;
  }

  [PXDBCurrency(typeof (CROpportunityDiscountDetail.curyInfoID), typeof (CROpportunityDiscountDetail.discountAmt))]
  [PXUIEnabled(typeof (Where<CROpportunityDiscountDetail.type, Equal<DiscountType.DocumentDiscount>, Or<CROpportunityDiscountDetail.type, Equal<DiscountType.ExternalDocumentDiscount>>>))]
  [PXUIField(DisplayName = "Discount Amt.")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscountAmt
  {
    get => this._CuryDiscountAmt;
    set => this._CuryDiscountAmt = value;
  }

  [PXDBDecimal(6)]
  [PXUIEnabled(typeof (Where<CROpportunityDiscountDetail.type, Equal<DiscountType.DocumentDiscount>, Or<CROpportunityDiscountDetail.type, Equal<DiscountType.ExternalDocumentDiscount>>>))]
  [PXUIField(DisplayName = "Discount Percent")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscountPct
  {
    get => this._DiscountPct;
    set => this._DiscountPct = value;
  }

  [Inventory(DisplayName = "Free Item")]
  [PXForeignReference(typeof (Field<CROpportunityDiscountDetail.freeItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? FreeItemID
  {
    get => this._FreeItemID;
    set => this._FreeItemID = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Free Item Qty.")]
  public virtual Decimal? FreeItemQty
  {
    get => this._FreeItemQty;
    set => this._FreeItemQty = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Discount")]
  public virtual bool? IsManual
  {
    get => this._IsManual;
    set => this._IsManual = value;
  }

  [PXBool]
  [PXFormula(typeof (False))]
  public virtual bool? IsOrigDocDiscount
  {
    get => this._IsOrigDocDiscount;
    set => this._IsOrigDocDiscount = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "External Discount Code")]
  public virtual string ExtDiscCode
  {
    get => this._ExtDiscCode;
    set => this._ExtDiscCode = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  [PXDefault(typeof (Search<PX.Objects.AR.DiscountSequence.description, Where<PX.Objects.AR.DiscountSequence.discountID, Equal<Current<CROpportunityDiscountDetail.discountID>>, And<PX.Objects.AR.DiscountSequence.discountSequenceID, Equal<Current<CROpportunityDiscountDetail.discountSequenceID>>>>>))]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
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
  public virtual DateTime? CreatedDateTime
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
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public static class FK
  {
    public class DiscountSequence : 
      PrimaryKeyOf<PX.Objects.AR.DiscountSequence>.By<PX.Objects.AR.DiscountSequence.discountID, PX.Objects.AR.DiscountSequence.discountSequenceID>.ForeignKeyOf<CROpportunityDiscountDetail>.By<CROpportunityDiscountDetail.discountID, CROpportunityDiscountDetail.discountSequenceID>
    {
    }
  }

  public abstract class quoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CROpportunityDiscountDetail.quoteID>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityDiscountDetail.recordID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityDiscountDetail.lineNbr>
  {
  }

  public abstract class skipDiscount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CROpportunityDiscountDetail.skipDiscount>
  {
  }

  public abstract class discountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityDiscountDetail.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityDiscountDetail.discountSequenceID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunityDiscountDetail.type>
  {
  }

  public abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    CROpportunityDiscountDetail.curyInfoID>
  {
  }

  public abstract class discountableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityDiscountDetail.discountableAmt>
  {
  }

  public abstract class curyDiscountableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityDiscountDetail.curyDiscountableAmt>
  {
  }

  public abstract class discountableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityDiscountDetail.discountableQty>
  {
  }

  public abstract class discountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityDiscountDetail.discountAmt>
  {
  }

  public abstract class curyDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityDiscountDetail.curyDiscountAmt>
  {
  }

  public abstract class discountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityDiscountDetail.discountPct>
  {
  }

  public abstract class freeItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunityDiscountDetail.freeItemID>
  {
  }

  public abstract class freeItemQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityDiscountDetail.freeItemQty>
  {
  }

  public abstract class isManual : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CROpportunityDiscountDetail.isManual>
  {
  }

  public abstract class isOrigDocDiscount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CROpportunityDiscountDetail.isOrigDocDiscount>
  {
  }

  public abstract class extDiscCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityDiscountDetail.extDiscCode>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityDiscountDetail.description>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    CROpportunityDiscountDetail.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CROpportunityDiscountDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityDiscountDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunityDiscountDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CROpportunityDiscountDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityDiscountDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunityDiscountDetail.lastModifiedDateTime>
  {
  }
}
