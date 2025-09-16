// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Standalone.ARInvoice
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR.Standalone;

[PXHidden(ServiceVisible = false)]
[Serializable]
public class ARInvoice : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DocType;
  protected string _RefNbr;
  protected int? _BillAddressID;
  protected int? _BillContactID;
  protected string _TermsID;
  protected DateTime? _DiscDate;
  protected string _InvoiceNbr;
  protected DateTime? _InvoiceDate;
  protected string _TaxZoneID;
  protected string _AvalaraCustomerUsageType;
  protected string _MasterRefNbr;
  protected short? _InstallmentNbr;
  protected Decimal? _CuryTaxTotal;
  protected Decimal? _TaxTotal;
  protected Decimal? _CuryLineTotal;
  protected Decimal? _LineTotal;
  protected Decimal? _CuryVatTaxableTotal;
  protected Decimal? _VatTaxableTotal;
  protected Decimal? _CuryVatExemptTotal;
  protected Decimal? _VatExemptTotal;
  protected Decimal? _CommnPct;
  protected Decimal? _CuryCommnAmt;
  protected Decimal? _CommnAmt;
  protected Decimal? _CuryCommnblAmt;
  protected Decimal? _CommnblAmt;
  protected bool? _CreditHold;
  protected bool? _ApprovedCredit;
  protected Decimal? _ApprovedCreditAmt;
  protected int? _WorkgroupID;
  protected int? _OwnerID;
  protected Guid? _RefNoteID;
  protected int? _ProjectID;
  protected bool? _Revoked;
  protected bool? _ApplyOverdueCharge;

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

  [PXDBInt]
  public virtual int? BillAddressID
  {
    get => this._BillAddressID;
    set => this._BillAddressID = value;
  }

  [PXDBInt]
  public virtual int? BillContactID
  {
    get => this._BillContactID;
    set => this._BillContactID = value;
  }

  [PXDBInt]
  public virtual int? ShipAddressID { get; set; }

  [PXDBInt]
  public virtual int? ShipContactID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string TermsID
  {
    get => this._TermsID;
    set => this._TermsID = value;
  }

  [PXDBDate]
  public virtual DateTime? DiscDate
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
  public virtual DateTime? InvoiceDate
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

  [PXDBString(30, IsUnicode = true)]
  public virtual string ExternalTaxExemptionNumber { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("0")]
  public virtual string AvalaraCustomerUsageType
  {
    get => this._AvalaraCustomerUsageType;
    set => this._AvalaraCustomerUsageType = value;
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

  /// <summary>
  /// The total <see cref="T:PX.Objects.AR.Standalone.ARInvoice.lineDiscTotal">discount of the document</see> (in the currency of the document),
  /// which is calculated as the sum of line discounts of the order.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Line Discounts", Enabled = false, Required = false)]
  public virtual Decimal? CuryLineDiscTotal { get; set; }

  /// <summary>
  /// The total line discount of the document, which is calculated as the sum of line discounts of the invoice.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Line Discounts", Enabled = false)]
  public virtual Decimal? LineDiscTotal { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.AR.Standalone.ARInvoice.goodsExtPriceTotal">total amount</see> on all lines of the document, except for Misc. Charges and null or empty Line Types
  /// (in the currency of the document).
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Goods")]
  public virtual Decimal? CuryGoodsExtPriceTotal { get; set; }

  /// <summary>
  /// The total amount on all lines of the document, except for Misc. Charges and null or empty Line Types
  /// (in base currency).
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? GoodsExtPriceTotal { get; set; }

  /// <summary>
  /// The <see cref="!:miscTot">total amount</see> calculated as the sum of the amounts in
  /// <see cref="T:PX.Objects.AR.ARTran.curyExtPrice">Ext. Price</see>
  /// (in the currency of the document).
  /// </summary>
  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Misc. Charges", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryMiscExtPriceTotal { get; set; }

  /// <summary>
  /// The total amount calculated as the sum of the amounts in <see cref="T:PX.Objects.AR.ARTran.curyExtPrice">Ext. Price</see>
  /// (in base currency).
  /// </summary>
  [PXDBDecimal(4)]
  public virtual Decimal? MiscExtPriceTotal { get; set; }

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

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CommnPct
  {
    get => this._CommnPct;
    set => this._CommnPct = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(4)]
  public virtual Decimal? CuryCommnAmt
  {
    get => this._CuryCommnAmt;
    set => this._CuryCommnAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CommnAmt
  {
    get => this._CommnAmt;
    set => this._CommnAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryCommnblAmt
  {
    get => this._CuryCommnblAmt;
    set => this._CuryCommnblAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CommnblAmt
  {
    get => this._CommnblAmt;
    set => this._CommnblAmt = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Credit Hold")]
  public virtual bool? CreditHold
  {
    get => this._CreditHold;
    set => this._CreditHold = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ApprovedCredit
  {
    get => this._ApprovedCredit;
    set => this._ApprovedCredit = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ApprovedCreditAmt
  {
    get => this._ApprovedCreditAmt;
    set => this._ApprovedCreditAmt = value;
  }

  [PXDBInt]
  public virtual int? WorkgroupID
  {
    get => this._WorkgroupID;
    set => this._WorkgroupID = value;
  }

  [PXDBInt]
  public virtual int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  [PXDBGuid(false)]
  public virtual Guid? RefNoteID
  {
    get => this._RefNoteID;
    set => this._RefNoteID = value;
  }

  [PXDBInt]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>
  /// A Boolean field that indicates whether the payments and prepayment applied to the related sales orders
  /// should be transferred to the invoice during the document creation.
  /// When set to <see langword="false" />, the payments and prepayments will not be transferred to the invoice
  /// during the document creation but will be transferred within the <b>Complete Processing</b> actions
  /// execution when all orders are already added to the invoice.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? IsPaymentsTransferred { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Revoked", Enabled = true, Visible = false)]
  public virtual bool? Revoked
  {
    get => this._Revoked;
    set => this._Revoked = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? ApplyOverdueCharge
  {
    get => this._ApplyOverdueCharge;
    set => this._ApplyOverdueCharge = value;
  }

  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryPaymentTotal { get; set; }

  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PaymentTotal { get; set; }

  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryBalanceWOTotal { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BalanceWOTotal { get; set; }

  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? CuryUnpaidBalance { get; set; }

  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? UnpaidBalance { get; set; }

  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscAppliedAmt { get; set; }

  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscAppliedAmt { get; set; }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.refNbr>
  {
  }

  public abstract class billAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.billAddressID>
  {
  }

  public abstract class billContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.billContactID>
  {
  }

  public abstract class shipAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.shipAddressID>
  {
  }

  public abstract class shipContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.shipContactID>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.termsID>
  {
  }

  public abstract class discDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARInvoice.discDate>
  {
  }

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.invoiceNbr>
  {
  }

  public abstract class invoiceDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARInvoice.invoiceDate>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.taxZoneID>
  {
  }

  public abstract class externalTaxExemptionNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoice.externalTaxExemptionNumber>
  {
  }

  public abstract class avalaraCustomerUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoice.avalaraCustomerUsageType>
  {
  }

  public abstract class masterRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.masterRefNbr>
  {
  }

  public abstract class installmentNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARInvoice.installmentNbr>
  {
  }

  public abstract class curyTaxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.curyTaxTotal>
  {
  }

  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.taxTotal>
  {
  }

  public abstract class curyLineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.curyLineTotal>
  {
  }

  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.lineTotal>
  {
  }

  public abstract class curyLineDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyLineDiscTotal>
  {
  }

  public abstract class lineDiscTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.lineDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.Standalone.ARInvoice.CuryGoodsExtPriceTotal" />
  public abstract class curyGoodsExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyGoodsExtPriceTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.Standalone.ARInvoice.CuryGoodsExtPriceTotal" />
  public abstract class goodsExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.goodsExtPriceTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.Standalone.ARInvoice.CuryMiscExtPriceTotal" />
  public abstract class curyMiscExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyMiscExtPriceTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.Standalone.ARInvoice.MiscExtPriceTotal" />
  public abstract class miscExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.miscExtPriceTotal>
  {
  }

  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyVatTaxableTotal>
  {
  }

  public abstract class vatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.vatTaxableTotal>
  {
  }

  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyVatExemptTotal>
  {
  }

  public abstract class vatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.vatExemptTotal>
  {
  }

  public abstract class commnPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.commnPct>
  {
  }

  public abstract class curyCommnAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.curyCommnAmt>
  {
  }

  public abstract class commnAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.commnAmt>
  {
  }

  public abstract class curyCommnblAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyCommnblAmt>
  {
  }

  public abstract class commnblAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.commnblAmt>
  {
  }

  public abstract class creditHold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.creditHold>
  {
  }

  public abstract class approvedCredit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.approvedCredit>
  {
  }

  public abstract class approvedCreditAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.approvedCreditAmt>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.ownerID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARInvoice.refNoteID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.projectID>
  {
  }

  public abstract class isPaymentsTransferred : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.isPaymentsTransferred>
  {
  }

  public abstract class revoked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.revoked>
  {
  }

  public abstract class applyOverdueCharge : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.applyOverdueCharge>
  {
  }

  public abstract class curyPaymentTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyPaymentTotal>
  {
  }

  public abstract class paymentTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.paymentTotal>
  {
  }

  public abstract class curyBalanceWOTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyBalanceWOTotal>
  {
  }

  public abstract class balanceWOTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.balanceWOTotal>
  {
  }

  public abstract class curyUnpaidBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyUnpaidBalance>
  {
  }

  public abstract class unpaidBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.unpaidBalance>
  {
  }

  public abstract class curyDiscAppliedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyDiscAppliedAmt>
  {
  }

  public abstract class discAppliedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.discAppliedAmt>
  {
  }
}
