// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Standalone.APInvoice
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AP.Standalone;

[PXHidden(ServiceVisible = false)]
[Serializable]
public class APInvoice : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DocType;
  protected string _RefNbr;
  protected string _TermsID;
  protected System.DateTime? _DueDate;
  protected System.DateTime? _DiscDate;
  protected string _InvoiceNbr;
  protected System.DateTime? _InvoiceDate;
  protected string _TaxZoneID;
  protected string _MasterRefNbr;
  protected short? _InstallmentNbr;
  protected Decimal? _CuryTaxTotal;
  protected Decimal? _TaxTotal;
  protected Decimal? _CuryVatTaxableTotal;
  protected Decimal? _VatTaxableTotal;
  protected Decimal? _CuryVatExemptTotal;
  protected Decimal? _VatExemptTotal;
  protected Decimal? _CuryLineTotal;
  protected Decimal? _LineTotal;
  protected bool? _SeparateCheck;
  protected bool? _PaySel = new bool?(false);
  protected System.DateTime? _PayDate;
  protected string _PayTypeID;
  protected int? _PayAccountID;
  protected int? _PayLocationID;
  protected int? _PrebookAcctID;
  protected int? _PrebookSubID;
  protected bool? _DisableAutomaticDiscountCalculation;

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBString(10, IsUnicode = true)]
  public virtual string TermsID
  {
    get => this._TermsID;
    set => this._TermsID = value;
  }

  [PXDBDate]
  public virtual System.DateTime? DueDate
  {
    get => this._DueDate;
    set => this._DueDate = value;
  }

  [PXDBDate]
  public virtual System.DateTime? DiscDate
  {
    get => this._DiscDate;
    set => this._DiscDate = value;
  }

  [PXDBString(40, IsUnicode = true)]
  public virtual string InvoiceNbr
  {
    get => this._InvoiceNbr;
    set => this._InvoiceNbr = value;
  }

  [PXDBDate]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  [PXUIField(DisplayName = "Vendor Ref. Date", Visibility = PXUIVisibility.Invisible)]
  public virtual System.DateTime? InvoiceDate
  {
    get => this._InvoiceDate;
    set => this._InvoiceDate = value;
  }

  [PXDBString(10, IsUnicode = true)]
  public virtual string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

  [PXDBString(15, IsUnicode = true)]
  public virtual string MasterRefNbr
  {
    get => this._MasterRefNbr;
    set => this._MasterRefNbr = value;
  }

  [PXDBShort]
  public virtual short? InstallmentNbr
  {
    get => this._InstallmentNbr;
    set => this._InstallmentNbr = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxTotal
  {
    get => this._CuryTaxTotal;
    set => this._CuryTaxTotal = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTotal
  {
    get => this._TaxTotal;
    set => this._TaxTotal = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatTaxableTotal
  {
    get => this._CuryVatTaxableTotal;
    set => this._CuryVatTaxableTotal = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatTaxableTotal
  {
    get => this._VatTaxableTotal;
    set => this._VatTaxableTotal = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatExemptTotal
  {
    get => this._CuryVatExemptTotal;
    set => this._CuryVatExemptTotal = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatExemptTotal
  {
    get => this._VatExemptTotal;
    set => this._VatExemptTotal = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineTotal
  {
    get => this._CuryLineTotal;
    set => this._CuryLineTotal = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineTotal
  {
    get => this._LineTotal;
    set => this._LineTotal = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? SeparateCheck
  {
    get => this._SeparateCheck;
    set => this._SeparateCheck = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public bool? PaySel
  {
    get => this._PaySel;
    set => this._PaySel = value;
  }

  [PXDBDate]
  public virtual System.DateTime? PayDate
  {
    get => this._PayDate;
    set => this._PayDate = value;
  }

  [PXDBString(10, IsUnicode = true)]
  public virtual string PayTypeID
  {
    get => this._PayTypeID;
    set => this._PayTypeID = value;
  }

  [PXDBInt]
  public virtual int? PayAccountID
  {
    get => this._PayAccountID;
    set => this._PayAccountID = value;
  }

  [PXDBInt]
  public virtual int? PayLocationID
  {
    get => this._PayLocationID;
    set => this._PayLocationID = value;
  }

  [PXDBInt]
  public virtual int? PrebookAcctID
  {
    get => this._PrebookAcctID;
    set => this._PrebookAcctID = value;
  }

  [PXDBInt]
  public virtual int? PrebookSubID
  {
    get => this._PrebookSubID;
    set => this._PrebookSubID = value;
  }

  [PXDBInt]
  public virtual int? SuppliedByVendorID { get; set; }

  [PXDBInt]
  public virtual int? SuppliedByVendorLocationID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Disable Automatic Discount Update")]
  public virtual bool? DisableAutomaticDiscountCalculation
  {
    get => this._DisableAutomaticDiscountCalculation;
    set => this._DisableAutomaticDiscountCalculation = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Joint Payees", FieldClass = "Construction")]
  public bool? IsJointPayees { get; set; }

  [PXDBString(1, IsFixed = true)]
  public virtual string EntityUsageType { get; set; }

  [PXDBString(30, IsUnicode = true)]
  public virtual string ExternalTaxExemptionNumber { get; set; }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.refNbr>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.termsID>
  {
  }

  public abstract class dueDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APInvoice.dueDate>
  {
  }

  public abstract class discDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APInvoice.discDate>
  {
  }

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.invoiceNbr>
  {
  }

  public abstract class invoiceDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APInvoice.invoiceDate>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.taxZoneID>
  {
  }

  public abstract class masterRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.masterRefNbr>
  {
  }

  public abstract class installmentNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  APInvoice.installmentNbr>
  {
  }

  public abstract class curyTaxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.curyTaxTotal>
  {
  }

  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.taxTotal>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.taxAmt>
  {
  }

  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.curyVatTaxableTotal>
  {
  }

  public abstract class vatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.vatTaxableTotal>
  {
  }

  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.curyVatExemptTotal>
  {
  }

  public abstract class vatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.vatExemptTotal>
  {
  }

  public abstract class curyLineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.curyLineTotal>
  {
  }

  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.lineTotal>
  {
  }

  public abstract class separateCheck : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.separateCheck>
  {
  }

  public abstract class paySel : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.paySel>
  {
  }

  public abstract class payDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APInvoice.payDate>
  {
  }

  public abstract class payTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.payTypeID>
  {
  }

  public abstract class payAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoice.payAccountID>
  {
  }

  public abstract class payLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoice.payLocationID>
  {
  }

  public abstract class prebookAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoice.prebookAcctID>
  {
  }

  public abstract class prebookSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoice.prebookSubID>
  {
  }

  public abstract class taxCalcMode : IBqlField, IBqlOperand
  {
  }

  public abstract class suppliedByVendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APInvoice.suppliedByVendorID>
  {
  }

  public abstract class suppliedByVendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APInvoice.suppliedByVendorLocationID>
  {
  }

  public abstract class disableAutomaticDiscountCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APInvoice.disableAutomaticDiscountCalculation>
  {
  }

  public abstract class isJointPayees : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.isJointPayees>
  {
  }

  public abstract class entityUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoice.entityUsageType>
  {
  }

  public abstract class externalTaxExemptionNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoice.externalTaxExemptionNumber>
  {
  }
}
