// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoiceDiscountDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.Common.Discount;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// A document-level or group-level discount that has been applied
/// to an accounts payable bill or adjustment. The records of this type
/// can be edited on the Bills and Adjustments (AP301000) form, which corresponds
/// to the <see cref="T:PX.Objects.AP.APInvoiceEntry" /> graph.
/// </summary>
/// <remarks>
/// Line-level discounts are specified in the document line itself,
/// see <see cref="P:PX.Objects.AP.APTran.DiscPct" />.
/// </remarks>
[PXCacheName("AP Invoice Discount Detail")]
[Serializable]
public class APInvoiceDiscountDetail : 
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
  string _DocType;
  protected string _RefNbr;
  protected string _DiscountID;
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
  protected string _OrderType;
  protected string _OrderNbr;
  protected string _ReceiptType;
  protected string _ReceiptNbr;
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

  [PXDBUShort]
  [PXLineNbr(typeof (APRegister))]
  public virtual ushort? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (Where<APInvoiceDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>, And<APInvoiceDiscountDetail.discountID, PX.Data.IsNotNull>>))]
  [PXUIField(DisplayName = "Skip Discount", Enabled = true)]
  public virtual bool? SkipDiscount
  {
    get => this._SkipDiscount;
    set => this._SkipDiscount = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (APRegister.docType))]
  [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = false, TabOrder = 0)]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (APRegister.refNbr))]
  [PXParent(typeof (Select<APRegister, Where<APRegister.docType, Equal<Current<APInvoiceDiscountDetail.docType>>, And<APRegister.refNbr, Equal<Current<APInvoiceDiscountDetail.refNbr>>>>>))]
  [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIEnabled(typeof (Where<APInvoiceDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>>))]
  [PXUIField(DisplayName = "Discount Code")]
  [PXSelector(typeof (Search<APDiscount.discountID, Where<APDiscount.type, NotEqual<DiscountType.LineDiscount>>>))]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIEnabled(typeof (Where<ARInvoiceDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>>))]
  [PXUIField(DisplayName = "Sequence ID")]
  [PXSelector(typeof (Search<VendorDiscountSequence.discountSequenceID, Where<VendorDiscountSequence.isActive, Equal<True>, And<VendorDiscountSequence.discountID, Equal<Current<APInvoiceDiscountDetail.discountID>>>>>))]
  [PXForeignReference(typeof (APInvoiceDiscountDetail.FK.VendorDiscountSequence))]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  [DiscountType.List]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [PXDBLong]
  [PX.Objects.CM.Extensions.CurrencyInfo(typeof (APInvoice.curyInfoID))]
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

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APInvoiceDiscountDetail.curyInfoID), typeof (APInvoiceDiscountDetail.discountableAmt))]
  [PXUIField(DisplayName = "Discountable Amt.", Enabled = false)]
  public virtual Decimal? CuryDiscountableAmt
  {
    get => this._CuryDiscountableAmt;
    set => this._CuryDiscountableAmt = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Discountable Qty.", Enabled = false)]
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

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APInvoiceDiscountDetail.curyInfoID), typeof (APInvoiceDiscountDetail.discountAmt))]
  [PXUIEnabled(typeof (Where<APInvoiceDiscountDetail.type, Equal<DiscountType.DocumentDiscount>, Or<APInvoiceDiscountDetail.type, Equal<DiscountType.ExternalDocumentDiscount>>>))]
  [PXUIField(DisplayName = "Discount Amt.")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscountAmt
  {
    get => this._CuryDiscountAmt;
    set => this._CuryDiscountAmt = value;
  }

  [PXDBDecimal(6)]
  [PXUIEnabled(typeof (Where<APInvoiceDiscountDetail.type, Equal<DiscountType.DocumentDiscount>, Or<APInvoiceDiscountDetail.type, Equal<DiscountType.ExternalDocumentDiscount>>>))]
  [PXUIField(DisplayName = "Discount Percent")]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? DiscountPct
  {
    get => this._DiscountPct;
    set => this._DiscountPct = value;
  }

  [Inventory(DisplayName = "Free Item", Enabled = false)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<APInvoiceDiscountDetail.freeItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? FreeItemID
  {
    get => this._FreeItemID;
    set => this._FreeItemID = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Free Item Qty.", Enabled = false)]
  public virtual Decimal? FreeItemQty
  {
    get => this._FreeItemQty;
    set => this._FreeItemQty = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Discount", Enabled = false)]
  public virtual bool? IsManual
  {
    get => this._IsManual;
    set => this._IsManual = value;
  }

  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<APInvoiceDiscountDetail.orderNbr, PX.Data.IsNotNull>, True>, False>))]
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
  [PXDefault(typeof (Search<VendorDiscountSequence.description, Where<VendorDiscountSequence.discountID, Equal<Current<APInvoiceDiscountDetail.discountID>>, And<VendorDiscountSequence.discountSequenceID, Equal<Current<APInvoiceDiscountDetail.discountSequenceID>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Order Type", Enabled = false)]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PO Order Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.PO.POOrder.orderNbr>))]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Receipt Type", Enabled = false)]
  public virtual string ReceiptType
  {
    get => this._ReceiptType;
    set => this._ReceiptType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PO Receipt Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.PO.POReceipt.receiptNbr, Where<PX.Objects.PO.POReceipt.receiptType, Equal<BqlField<APInvoiceDiscountDetail.receiptType, IBqlString>.FromCurrent>>>))]
  public virtual string ReceiptNbr
  {
    get => this._ReceiptNbr;
    set => this._ReceiptNbr = value;
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

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APInvoiceDiscountDetail.curyInfoID), typeof (APInvoiceDiscountDetail.retainedDiscountAmt))]
  [PXUIField(DisplayName = "Retained Discount", Enabled = false, FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? CuryRetainedDiscountAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? RetainedDiscountAmt { get; set; }

  public class PK : 
    PrimaryKeyOf<APInvoiceDiscountDetail>.By<APInvoiceDiscountDetail.docType, APInvoiceDiscountDetail.refNbr, APInvoiceDiscountDetail.recordID>
  {
    public static APInvoiceDiscountDetail Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? recordID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APInvoiceDiscountDetail>.By<APInvoiceDiscountDetail.docType, APInvoiceDiscountDetail.refNbr, APInvoiceDiscountDetail.recordID>.FindBy(graph, (object) docType, (object) refNbr, (object) recordID, options);
    }
  }

  public static class FK
  {
    public class Invoice : 
      PrimaryKeyOf<APRegister>.By<APRegister.docType, APRegister.refNbr>.ForeignKeyOf<APInvoiceDiscountDetail>.By<APInvoiceDiscountDetail.docType, APInvoiceDiscountDetail.refNbr>
    {
    }

    public class VendorDiscountSequence : 
      PrimaryKeyOf<VendorDiscountSequence>.By<VendorDiscountSequence.discountID, VendorDiscountSequence.discountSequenceID>.ForeignKeyOf<APInvoiceDiscountDetail>.By<APInvoiceDiscountDetail.discountID, APInvoiceDiscountDetail.discountSequenceID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<APInvoiceDiscountDetail>.By<APInvoiceDiscountDetail.curyInfoID>
    {
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoiceDiscountDetail.recordID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoiceDiscountDetail.lineNbr>
  {
  }

  public abstract class skipDiscount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APInvoiceDiscountDetail.skipDiscount>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoiceDiscountDetail.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoiceDiscountDetail.refNbr>
  {
  }

  public abstract class discountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoiceDiscountDetail.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoiceDiscountDetail.discountSequenceID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoiceDiscountDetail.type>
  {
  }

  public abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    APInvoiceDiscountDetail.curyInfoID>
  {
  }

  public abstract class discountableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceDiscountDetail.discountableAmt>
  {
  }

  public abstract class curyDiscountableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceDiscountDetail.curyDiscountableAmt>
  {
  }

  public abstract class discountableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceDiscountDetail.discountableQty>
  {
  }

  public abstract class discountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceDiscountDetail.discountAmt>
  {
  }

  public abstract class curyDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceDiscountDetail.curyDiscountAmt>
  {
  }

  public abstract class discountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceDiscountDetail.discountPct>
  {
  }

  public abstract class freeItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoiceDiscountDetail.freeItemID>
  {
  }

  public abstract class freeItemQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceDiscountDetail.freeItemQty>
  {
  }

  public abstract class isManual : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoiceDiscountDetail.isManual>
  {
  }

  public abstract class isOrigDocDiscount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APInvoiceDiscountDetail.isOrigDocDiscount>
  {
  }

  public abstract class extDiscCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoiceDiscountDetail.extDiscCode>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoiceDiscountDetail.description>
  {
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoiceDiscountDetail.orderType>
  {
  }

  public abstract class orderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoiceDiscountDetail.orderNbr>
  {
  }

  public abstract class receiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoiceDiscountDetail.receiptType>
  {
  }

  public abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoiceDiscountDetail.receiptNbr>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APInvoiceDiscountDetail.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APInvoiceDiscountDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoiceDiscountDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APInvoiceDiscountDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APInvoiceDiscountDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoiceDiscountDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APInvoiceDiscountDetail.lastModifiedDateTime>
  {
  }

  public abstract class curyRetainedDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceDiscountDetail.curyRetainedDiscountAmt>
  {
  }

  public abstract class retainedDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceDiscountDetail.retainedDiscountAmt>
  {
  }
}
