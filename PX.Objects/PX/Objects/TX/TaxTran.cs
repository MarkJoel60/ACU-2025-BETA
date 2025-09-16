// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;

#nullable enable
namespace PX.Objects.TX;

/// <summary>
/// A tax transaction. The entity serves for the following purposes simultaneously:
/// <list>
/// <item><description>To report the tax amount, which is stored in the TaxTran record, to a tax period.
/// A tax report is built by TaxTran records. Reportable TaxTran records
/// belong to the <see cref="P:PX.Objects.TX.TaxTran.TaxPeriodID">specified tax period</see>
/// after the tax report is prepared.</description></item>
/// <item><description>To store the tax amount of the document for each applied tax.</description></item>
/// </list>
/// </summary>
[PXCacheName("Tax Transaction")]
[Serializable]
public class TaxTran : TaxDetail, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _Module;
  protected string _TranType;
  protected string _RefNbr;
  protected string _LineRefNbr;
  protected string _OrigTranType;
  protected string _OrigRefNbr;
  protected bool? _Released;
  protected bool? _Voided;
  protected int? _BranchID;
  protected string _FinPeriodID;
  protected string _TaxPeriodID;
  protected int? _RecordID;
  protected string _JurisType;
  protected string _JurisName;
  protected int? _VendorID;
  protected int? _RevisionID;
  protected int? _BAccountID;
  protected string _TaxZoneID;
  protected int? _AccountID;
  protected int? _SubID;
  protected DateTime? _TranDate;
  protected string _TaxInvoiceNbr;
  protected DateTime? _TaxInvoiceDate;
  /// <summary>
  /// The reporting type of the tax, as it has been recognized by the system.
  /// The value is used to calculate the sign of record amounts during preparation of the tax report.
  /// </summary>
  /// <value>
  /// <c>S</c>: Output (sales)
  /// <c>P</c>: Input (purchase)
  /// <c>A</c>: Suspended VAT output
  /// <c>B</c>: Suspended VAT input
  /// </value>
  protected string _TaxType;
  protected int? _TaxBucketID;
  protected Decimal? _CuryTaxableAmt;
  protected Decimal? _TaxableAmt;
  protected Decimal? _CuryTaxAmt;
  protected Decimal? _TaxAmt;
  protected string _CuryID;
  protected string _ReportCuryID;
  protected string _ReportCuryRateTypeID;
  protected DateTime? _ReportCuryEffDate;
  protected string _ReportCuryMultDiv;
  protected Decimal? _ReportCuryRate;
  protected Decimal? _ReportTaxableAmt;
  protected Decimal? _ReportTaxAmt;
  protected DateTime? _CuryEffDate;
  protected string _AdjdDocType;
  protected string _AdjdRefNbr;
  protected int? _AdjNbr;
  protected string _Description;
  protected byte[] _tstamp;
  protected Decimal? _CuryRetainedTaxableAmt;
  protected Decimal? _RetainedTaxableAmt;
  protected Decimal? _CuryRetainedTaxAmt;
  protected Decimal? _RetainedTaxAmt;

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the tax transaction is selected on a form.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  /// <summary>
  /// The source module of the record.
  /// The field is a part of the primary key.
  /// </summary>
  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault("GL")]
  [PXUIField(DisplayName = "Module")]
  public virtual string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  /// <summary>The type of the record.</summary>
  /// <value>
  /// If the record is related to a document, the field contains the type of the document.
  /// In other cases, the field contains one of the following values:
  /// <c>TFW</c>: GL tax record
  /// <c>TRV</c>: GL tax record
  /// <c>INT</c>: Output tax adjustment
  /// <c>RET</c>: Input tax adjustment
  /// <c>VTI</c>: Input VAT
  /// <c>VTO</c>: Output VAT
  /// <c>REI</c>: Reverse input VAT
  /// <c>REO</c>: Reverse output VAT
  /// </value>
  [PXDBString(3, IsFixed = true)]
  [PXDBDefault(typeof (TaxAdjustment.docType))]
  [PXParent(typeof (Select<TaxAdjustment, Where<TaxAdjustment.docType, Equal<Current<TaxTran.tranType>>, And<TaxAdjustment.refNbr, Equal<Current<TaxTran.refNbr>>>>>))]
  [TaxAdjustmentType.List]
  [PXUIField(DisplayName = "Tran. Type")]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  /// <summary>
  /// The reference number of the document to which the record is releated.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXDBDefault(typeof (TaxAdjustment.refNbr))]
  [PXUIField(DisplayName = "Ref. Nbr.")]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  /// <summary>
  /// The reference number of the transaction to which the record is related.
  /// The field is used for the records that are created from GL.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Line Ref. Number")]
  [PXDefault("")]
  public virtual string LineRefNbr
  {
    get => this._LineRefNbr;
    set => this._LineRefNbr = value;
  }

  /// <summary>
  /// The original document type for which the tax amount has been entered.
  /// The field is used for the records that are created on the Tax Bills and Adjustments (TX303000) form.
  /// </summary>
  [PXDBString(3, IsFixed = true)]
  [PXUIField(DisplayName = "Orig. Tran. Type")]
  [PXDefault("")]
  public virtual string OrigTranType
  {
    get => this._OrigTranType;
    set => this._OrigTranType = value;
  }

  /// <summary>
  /// The original document reference number for which the tax amount has been entered.
  /// The field is used for the records that are created on the Tax Bills and Adjustments (TX303000) form.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Orig. Doc. Number")]
  [PXDefault("")]
  public virtual string OrigRefNbr
  {
    get => this._OrigRefNbr;
    set => this._OrigRefNbr = value;
  }

  [PXDBInt]
  [PXDefault]
  public virtual int? LineNbr { get; set; }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the record has been released.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the record has been voided.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  /// <summary>
  /// The reference to the <see cref="T:PX.Objects.GL.Branch" /> record to which the record belongs.
  /// </summary>
  /// <value>The value is copied from the document from which the record is created.</value>
  [Branch(null, null, true, true, true, Enabled = false)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>
  /// The reference to the financial period of the document to which the record belongs.
  /// </summary>
  [PX.Objects.GL.FinPeriodID(null, typeof (TaxTran.branchID), null, null, null, null, true, false, null, null, typeof (TaxAdjustment.tranPeriodID), true, true)]
  [PXDefault]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  /// <summary>
  /// The last day (<see cref="!:PX.Objects.GL.Obsolete.FinPeriod.FinDate" />) of the financial period of the document to which the record belongs.
  /// </summary>
  [PXDBDate]
  [PXDBDefault(typeof (Search2<OrganizationFinPeriod.finDate, InnerJoin<PX.Objects.GL.Branch, On<OrganizationFinPeriod.organizationID, Equal<PX.Objects.GL.Branch.organizationID>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current2<TaxTran.branchID>>, And<OrganizationFinPeriod.finPeriodID, Equal<Current2<TaxTran.finPeriodID>>>>>))]
  public virtual DateTime? FinDate { get; set; }

  /// <summary>
  /// The key of the tax period to which the record has been reported.
  /// The field has the null value for the unreported records.
  /// </summary>
  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXDBDefault(typeof (TaxAdjustment.taxPeriod))]
  public virtual string TaxPeriodID
  {
    get => this._TaxPeriodID;
    set => this._TaxPeriodID = value;
  }

  /// <summary>
  /// The reference to the <see cref="T:PX.Objects.TX.Tax" /> record.
  /// </summary>
  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<Tax.taxID, Where<Tax.taxVendorID, Equal<Current<TaxAdjustment.vendorID>>>>), DirtyRead = true)]
  [PXForeignReference(typeof (Field<TaxTran.taxID>.IsRelatedTo<Tax.taxID>))]
  public override string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  /// <summary>
  /// This is an auto-numbered field, which is a part of the primary key.
  /// </summary>
  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  /// <summary>
  /// The tax jurisdiction type. The field is used for the taxes from Avalara.
  /// </summary>
  [PXDBString(9, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Jurisdiction Type")]
  public virtual string JurisType
  {
    get => this._JurisType;
    set => this._JurisType = value;
  }

  /// <summary>
  /// The tax jurisdiction name. The field is used for the taxes from Avalara.
  /// </summary>
  [PXDBString(200, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Jurisdiction Name")]
  public virtual string JurisName
  {
    get => this._JurisName;
    set => this._JurisName = value;
  }

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.AP.Vendor" />, which specifies the tax agency to which the record belongs.
  /// </summary>
  /// <value>
  /// When the record is created, the field is assigned the default value based on the document.
  /// The value of the field is updated during preparation of a tax report.
  /// </value>
  [PXDBInt]
  [PXDBDefault(typeof (TaxAdjustment.vendorID))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  /// <summary>
  /// The revision of the tax report to which the record was included.
  /// </summary>
  [PXDBInt]
  public virtual int? RevisionID
  {
    get => this._RevisionID;
    set => this._RevisionID = value;
  }

  /// <summary>
  /// The reference to the vendor record (<see cref="!:Vendor.BAccountID" />) or customer record (<see cref="!:Customer.BAccountID" />).
  /// The field is used for the records that have been created in the AP or AR module.
  /// </summary>
  [PXDBInt]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  /// <summary>
  /// The reference to the tax zone (<see cref="P:PX.Objects.TX.TaxZone.TaxZoneID" />). The value is assigned based on the document to which the record belongs.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Zone")]
  [PXSelector(typeof (Search<TaxZone.taxZoneID>))]
  [PXDefault]
  [PXForeignReference(typeof (Field<TaxTran.taxZoneID>.IsRelatedTo<TaxZone.taxZoneID>))]
  public virtual string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

  /// <summary>
  /// The reference to the account record (<see cref="P:PX.Objects.GL.Account.AccountID" />) of the related <see cref="T:PX.Objects.TX.Tax" /> record.
  /// </summary>
  [Account]
  [PXDefault]
  [PXReferentialIntegrityCheck]
  [PXForeignReference(typeof (Field<TaxTran.accountID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  /// <summary>
  /// The reference to the subaccount (<see cref="P:PX.Objects.GL.Account.AccountID" />) of the related <see cref="T:PX.Objects.TX.Tax" /> record.
  /// </summary>
  [SubAccount(typeof (TaxTran.accountID))]
  [PXDefault]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  /// <summary>The date of the tax record.</summary>
  /// <value>
  /// The value corresponds to the date of the document to which the record belongs.
  /// </value>
  [PXDBDate]
  [PXDBDefault(typeof (TaxAdjustment.docDate))]
  [PXUIField(DisplayName = "Tran. Date")]
  public virtual DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  /// <summary>
  /// The reference number of the tax invoice. The field is used for recognized SVAT records.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Invoice Nbr.")]
  public virtual string TaxInvoiceNbr
  {
    get => this._TaxInvoiceNbr;
    set => this._TaxInvoiceNbr = value;
  }

  /// <summary>
  /// The date of the tax invoice. The field is used for recognized SVAT records.
  /// </summary>
  [PXDBDate(InputMask = "d", DisplayMask = "d")]
  [PXUIField(DisplayName = "Tax Invoice Date")]
  public virtual DateTime? TaxInvoiceDate
  {
    get => this._TaxInvoiceDate;
    set => this._TaxInvoiceDate = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  public virtual string TaxType
  {
    get => this._TaxType;
    set => this._TaxType = value;
  }

  /// <summary>
  /// The reference to the reporting group (<see cref="P:PX.Objects.TX.TaxBucket.BucketID" />) for which the record should be reported.
  /// During record creation, the value of the field is assigned based on the document.
  /// During preparation of the tax report, the value is updated from the relevant<see cref="P:PX.Objects.TX.TaxRev.TaxBucketID" /> record.
  /// </summary>
  [PXDBInt]
  [PXDefault]
  public virtual int? TaxBucketID
  {
    get => this._TaxBucketID;
    set => this._TaxBucketID = value;
  }

  /// <summary>
  /// The tax rate of the relevant <see cref="T:PX.Objects.TX.Tax" /> record.
  /// </summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? TaxRate
  {
    get => this._TaxRate;
    set => this._TaxRate = value;
  }

  /// <summary>
  /// The reference to the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo" /> record that is related to the document.
  /// </summary>
  [PXDBLong]
  [CurrencyInfo(typeof (TaxAdjustment.curyInfoID))]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  /// <summary>
  /// The original taxable amount (before truncation by minimal or maximal value) in the record currency.
  /// </summary>
  [PXDBCurrency(typeof (TaxTran.curyInfoID), typeof (TaxTran.origTaxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Orig. Taxable Amount")]
  public virtual Decimal? CuryOrigTaxableAmt { get; set; }

  /// <summary>
  /// The original taxable amount (before truncation by minimal or maximal value) in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Orig. Taxable Amount")]
  public virtual Decimal? OrigTaxableAmt { get; set; }

  /// <summary>The taxable amount in the record currency.</summary>
  [PXDBCurrency(typeof (TaxTran.curyInfoID), typeof (TaxTran.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxableAmt
  {
    get => this._CuryTaxableAmt;
    set => this._CuryTaxableAmt = value;
  }

  /// <summary>The taxable amount in the base currency.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxableAmt
  {
    get => this._TaxableAmt;
    set => this._TaxableAmt = value;
  }

  /// <summary>The exempted amount in the record currency.</summary>
  [PXDBCurrency(typeof (TaxTran.curyInfoID), typeof (TaxTran.exemptedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryExemptedAmt { get; set; }

  /// <summary>The exempted amount in the base currency.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ExemptedAmt { get; set; }

  /// <summary>The tax amount in the record currency.</summary>
  [PXDBCurrency(typeof (TaxTran.curyInfoID), typeof (TaxTran.taxAmt))]
  [PXFormula(typeof (Mult<TaxTran.curyTaxableAmt, Div<TaxTran.taxRate, decimal100>>), typeof (SumCalc<TaxAdjustment.curyDocBal>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxAmt
  {
    get => this._CuryTaxAmt;
    set => this._CuryTaxAmt = value;
  }

  /// <summary>The tax amount in the base currency.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxAmt
  {
    get => this._TaxAmt;
    set => this._TaxAmt = value;
  }

  [PXDBCurrency(typeof (TaxTran.curyInfoID), typeof (TaxTran.taxAmtSumm))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxAmtSumm { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxAmtSumm { get; set; }

  /// <summary>
  /// The reference to the currency (<see cref="!:Currency.CuryID" />) of the document to which the record belongs.
  /// </summary>
  [SlaveCuryID(typeof (TaxTran.curyInfoID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  /// <summary>
  /// The reference to the currency (<see cref="!:Currency.CuryID" />) of the tax agency.
  /// </summary>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Report Currency")]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string ReportCuryID
  {
    get => this._ReportCuryID;
    set => this._ReportCuryID = value;
  }

  /// <summary>
  /// The reference to the currency rate type (<see cref="P:PX.Objects.CM.Extensions.CurrencyRateType.CuryRateTypeID" />),
  /// which is used during report preparation to obtain amounts in the tax agency currency.
  /// </summary>
  [PXDBString(6, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Extensions.CurrencyRateType.curyRateTypeID), DescriptionField = typeof (PX.Objects.CM.Extensions.CurrencyRateType.descr))]
  public virtual string ReportCuryRateTypeID
  {
    get => this._ReportCuryRateTypeID;
    set => this._ReportCuryRateTypeID = value;
  }

  /// <summary>
  /// The effective date of the currency rate (<see cref="P:PX.Objects.CM.Extensions.CurrencyRate.CuryEffDate" />),
  /// which is used during report preparation to obtain amounts in the tax agency currency.
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Report Effective Date")]
  public virtual DateTime? ReportCuryEffDate
  {
    get => this._ReportCuryEffDate;
    set => this._ReportCuryEffDate = value;
  }

  /// <summary>
  /// The conversion type of the currency rate (<see cref="P:PX.Objects.CM.Extensions.CurrencyRate.CuryMultDiv" />),
  /// which is used during report preparation to obtain amounts in the tax agency currency.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("M")]
  [PXUIField(DisplayName = "Report Mult Div")]
  public virtual string ReportCuryMultDiv
  {
    get => this._ReportCuryMultDiv;
    set => this._ReportCuryMultDiv = value;
  }

  /// <summary>
  /// The currency rate value (<see cref="P:PX.Objects.CM.Extensions.CurrencyRate.CuryRate" />) of the (<see cref="T:PX.Objects.CM.Extensions.CurrencyRate" />) record
  /// which is used on report prepare to obtaion amounts in the tax agency currency.
  /// </summary>
  [PXDBDecimal(8)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? ReportCuryRate
  {
    get => this._ReportCuryRate;
    set => this._ReportCuryRate = value;
  }

  /// <summary>The taxable amount in the tax agency currency.</summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal]
  [PXUIField]
  public virtual Decimal? ReportTaxableAmt
  {
    get => this._ReportTaxableAmt;
    set => this._ReportTaxableAmt = value;
  }

  /// <summary>The exempted amount in the tax agency currency.</summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal]
  [PXUIField]
  public virtual Decimal? ReportExemptedAmt { get; set; }

  /// <summary>The tax amount in the tax agency currency.</summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal]
  [PXUIField]
  public virtual Decimal? ReportTaxAmt
  {
    get => this._ReportTaxAmt;
    set => this._ReportTaxAmt = value;
  }

  /// <summary>
  /// The deductible tax rate from the related <see cref="T:PX.Objects.TX.TaxRev" /> record.
  /// </summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? NonDeductibleTaxRate { get; set; }

  /// <summary>The expense amount in the base currency.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? ExpenseAmt { get; set; }

  /// <summary>The expense amount in the record currency.</summary>
  [PXDBCurrency(typeof (TaxTran.curyInfoID), typeof (TaxTran.expenseAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryExpenseAmt { get; set; }

  /// <summary>
  /// The effective date of the suitable <see cref="T:PX.Objects.CM.Extensions.CurrencyRate" /> record.
  /// The field is filled in only during preparation of the tax report.
  /// </summary>
  [PXDate]
  public virtual DateTime? CuryEffDate
  {
    get => this._CuryEffDate;
    set => this._CuryEffDate = value;
  }

  /// <summary>
  /// Link to <see cref="T:PX.Objects.AP.APPayment" /> (Check) application. Used for withholding taxes.
  /// </summary>
  [PXDBString(3)]
  public virtual string AdjdDocType
  {
    get => this._AdjdDocType;
    set => this._AdjdDocType = value;
  }

  /// <summary>
  /// Link to <see cref="T:PX.Objects.AP.APPayment" /> (Check) application. Used for withholding taxes.
  /// </summary>
  [PXDBString(15)]
  public virtual string AdjdRefNbr
  {
    get => this._AdjdRefNbr;
    set => this._AdjdRefNbr = value;
  }

  /// <summary>
  /// Link to <see cref="T:PX.Objects.AP.APPayment" /> (Check) application. Used for withholding taxes.
  /// </summary>
  [PXDBInt]
  public virtual int? AdjNbr
  {
    get => this._AdjNbr;
    set => this._AdjNbr = value;
  }

  /// <summary>The description of the transaction.</summary>
  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXUIField]
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

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (TaxTran.curyInfoID), typeof (TaxTran.retainedTaxableAmt))]
  [PXUIField]
  public virtual Decimal? CuryRetainedTaxableAmt
  {
    get => this._CuryRetainedTaxableAmt;
    set => this._CuryRetainedTaxableAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(4)]
  [PXUIField]
  public virtual Decimal? RetainedTaxableAmt
  {
    get => this._RetainedTaxableAmt;
    set => this._RetainedTaxableAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (TaxTran.curyInfoID), typeof (TaxTran.retainedTaxAmt))]
  [PXUIField]
  public virtual Decimal? CuryRetainedTaxAmt
  {
    get => this._CuryRetainedTaxAmt;
    set => this._CuryRetainedTaxAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(4)]
  [PXUIField]
  public virtual Decimal? RetainedTaxAmt
  {
    get => this._RetainedTaxAmt;
    set => this._RetainedTaxAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (TaxTran.curyInfoID), typeof (TaxTran.retainedTaxAmtSumm))]
  public virtual Decimal? CuryRetainedTaxAmtSumm { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(4)]
  public virtual Decimal? RetainedTaxAmtSumm { get; set; }

  /// <summary>
  /// The Taxable Amount of the prepayment invoice document (PPI) reduced by a credit memo or a debit adjustment.
  /// when the feature <see cref="P:PX.Objects.CS.FeaturesSet.VATRecognitionOnPrepaymentsAR" /> or <see cref="P:PX.Objects.CS.FeaturesSet.VATRecognitionOnPrepaymentsAP" /> is activated.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARTaxTran.curyInfoID), typeof (TaxTran.adjustedTaxableAmt))]
  [PXUIField]
  public virtual Decimal? CuryAdjustedTaxableAmt { get; set; }

  /// <summary>
  /// The Taxable Amount of the prepayment invoice document (PPI) reduced by a credit memo or a debit adjustment.
  /// when the feature <see cref="P:PX.Objects.CS.FeaturesSet.VATRecognitionOnPrepaymentsAR" /> or <see cref="P:PX.Objects.CS.FeaturesSet.VATRecognitionOnPrepaymentsAP" /> is activated.
  /// In the base currency
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(4)]
  public virtual Decimal? AdjustedTaxableAmt { get; set; }

  /// <summary>
  /// The Tax Amount of the prepayment invoice document (PPI) reduced by a credit memo or a debit adjustment.
  /// when the feature <see cref="P:PX.Objects.CS.FeaturesSet.VATRecognitionOnPrepaymentsAR" /> or <see cref="P:PX.Objects.CS.FeaturesSet.VATRecognitionOnPrepaymentsAP" /> is activated.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARTaxTran.curyInfoID), typeof (TaxTran.adjustedTaxAmt))]
  [PXUIField]
  public virtual Decimal? CuryAdjustedTaxAmt { get; set; }

  /// <summary>
  /// The Tax Amount of the prepayment invoice document (PPI) reduced by a credit memo or a debit adjustment.
  /// when the feature <see cref="P:PX.Objects.CS.FeaturesSet.VATRecognitionOnPrepaymentsAR" /> or <see cref="P:PX.Objects.CS.FeaturesSet.VATRecognitionOnPrepaymentsAP" /> is activated.
  /// In the base currency
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(4)]
  public virtual Decimal? AdjustedTaxAmt { get; set; }

  /// <summary>
  /// A Boolean value that specifies that the tax transaction is tax inclusive or not
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Inclusive")]
  public virtual bool? IsTaxInclusive { get; set; }

  public static string GetKeyImage(string module, int? recordID, DateTime? tranDate)
  {
    return $"{typeof (TaxTran.module).Name}:{module}, {typeof (TaxTran.recordID).Name}:{recordID}, {typeof (TaxTran.tranDate).Name}:{tranDate}";
  }

  public string GetKeyImage() => TaxTran.GetKeyImage(this.Module, this.RecordID, this.TranDate);

  public static string GetImage(string module, int? recordID, DateTime? tranDate)
  {
    return $"{EntityHelper.GetFriendlyEntityName(typeof (TaxTran))}[{TaxTran.GetKeyImage(module, recordID, tranDate)}]";
  }

  public virtual string ToString() => TaxTran.GetImage(this.Module, this.RecordID, this.TranDate);

  public class PK : PrimaryKeyOf<TaxTran>.By<TaxTran.module, TaxTran.tranDate, TaxTran.recordID>
  {
    public static TaxTran Find(
      PXGraph graph,
      string module,
      DateTime? tranDate,
      int? recordID,
      PKFindOptions options = 0)
    {
      return (TaxTran) PrimaryKeyOf<TaxTran>.By<TaxTran.module, TaxTran.tranDate, TaxTran.recordID>.FindBy(graph, (object) module, (object) tranDate, (object) recordID, options);
    }
  }

  public static class FK
  {
    public class BAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<TaxTran>.By<TaxTran.bAccountID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<TaxTran>.By<TaxTran.branchID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<TaxTran>.By<TaxTran.vendorID>
    {
    }

    public class Tax : PrimaryKeyOf<Tax>.By<Tax.taxID>.ForeignKeyOf<TaxTran>.By<TaxTran.taxID>
    {
    }

    public class TaxReport : 
      PrimaryKeyOf<TaxReport>.By<TaxReport.vendorID, TaxReport.revisionID>.ForeignKeyOf<TaxTran>.By<TaxTran.vendorID, TaxTran.revisionID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<TaxZone>.By<TaxZone.taxZoneID>.ForeignKeyOf<TaxTran>.By<TaxTran.taxZoneID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<TaxTran>.By<TaxTran.accountID>
    {
    }

    public class Subaccount : PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<TaxTran>.By<TaxTran.subID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<TaxTran>.By<TaxTran.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<TaxTran>.By<TaxTran.curyID>
    {
    }

    public class ReportCurrency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<TaxTran>.By<TaxTran.reportCuryID>
    {
    }

    public class ReportCurrencyRateType : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyRateType>.By<PX.Objects.CM.CurrencyRateType.curyRateTypeID>.ForeignKeyOf<TaxTran>.By<TaxTran.reportCuryRateTypeID>
    {
    }

    public class TaxBucket : 
      PrimaryKeyOf<TaxBucket>.By<TaxBucket.vendorID, TaxBucket.bucketID>.ForeignKeyOf<TaxTran>.By<TaxTran.vendorID, TaxTran.taxBucketID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxTran.selected>
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTran.module>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTran.tranType>
  {
    public const string TranForward = "TFW";
    public const string TranReversed = "TRV";
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTran.refNbr>
  {
  }

  public abstract class lineRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTran.lineRefNbr>
  {
  }

  public abstract class origTranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTran.origTranType>
  {
  }

  public abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTran.origRefNbr>
  {
  }

  /// <summary>
  /// The line number of the document to which the record is releated.
  /// The field is used for documents which have the Line Number as a kay field.
  /// </summary>
  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTran.lineNbr>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxTran.released>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxTran.voided>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTran.branchID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTran.finPeriodID>
  {
  }

  public abstract class finDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxTran.finDate>
  {
  }

  public abstract class taxPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTran.taxPeriodID>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTran.taxID>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTran.recordID>
  {
  }

  public abstract class jurisType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTran.jurisType>
  {
  }

  public abstract class jurisName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTran.jurisName>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTran.vendorID>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTran.revisionID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTran.bAccountID>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTran.taxZoneID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTran.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTran.subID>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxTran.tranDate>
  {
  }

  public abstract class taxInvoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTran.taxInvoiceNbr>
  {
  }

  public abstract class taxInvoiceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxTran.taxInvoiceDate>
  {
  }

  public abstract class taxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTran.taxType>
  {
  }

  public abstract class taxBucketID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTran.taxBucketID>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTran.taxRate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  TaxTran.curyInfoID>
  {
  }

  public abstract class curyOrigTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTran.curyOrigTaxableAmt>
  {
  }

  public abstract class origTaxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTran.origTaxableAmt>
  {
  }

  public abstract class curyTaxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTran.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTran.taxableAmt>
  {
  }

  public abstract class curyExemptedAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class exemptedAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTran.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTran.taxAmt>
  {
  }

  public abstract class curyTaxAmtSumm : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTran.curyTaxAmtSumm>
  {
  }

  public abstract class taxAmtSumm : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTran.taxAmtSumm>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTran.curyID>
  {
  }

  public abstract class reportCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTran.reportCuryID>
  {
  }

  public abstract class reportCuryRateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxTran.reportCuryRateTypeID>
  {
  }

  public abstract class reportCuryEffDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxTran.reportCuryEffDate>
  {
  }

  public abstract class reportCuryMultDiv : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxTran.reportCuryMultDiv>
  {
  }

  public abstract class reportCuryRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTran.reportCuryRate>
  {
  }

  public abstract class reportTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTran.reportTaxableAmt>
  {
  }

  public abstract class reportExemptedAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class reportTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTran.reportTaxAmt>
  {
  }

  public abstract class nonDeductibleTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTran.nonDeductibleTaxRate>
  {
  }

  public abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTran.expenseAmt>
  {
  }

  public abstract class curyExpenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTran.curyExpenseAmt>
  {
  }

  public abstract class curyEffDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxTran.curyEffDate>
  {
  }

  public abstract class adjdDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTran.adjdDocType>
  {
  }

  public abstract class adjdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTran.adjdRefNbr>
  {
  }

  public abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTran.adjNbr>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTran.description>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TaxTran.Tstamp>
  {
  }

  public abstract class curyRetainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTran.curyRetainedTaxableAmt>
  {
  }

  public abstract class retainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTran.retainedTaxableAmt>
  {
  }

  public abstract class curyRetainedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTran.curyRetainedTaxAmt>
  {
  }

  public abstract class retainedTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTran.retainedTaxAmt>
  {
  }

  public abstract class curyRetainedTaxAmtSumm : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTran.curyRetainedTaxAmtSumm>
  {
  }

  public abstract class retainedTaxAmtSumm : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTran.retainedTaxAmtSumm>
  {
  }

  public abstract class curyAdjustedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTran.curyAdjustedTaxableAmt>
  {
  }

  public abstract class adjustedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTran.adjustedTaxableAmt>
  {
  }

  public abstract class curyAdjustedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTran.curyAdjustedTaxAmt>
  {
  }

  public abstract class adjustedTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTran.adjustedTaxAmt>
  {
  }

  public abstract class isTaxInclusive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxTran.isTaxInclusive>
  {
  }
}
