// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.Common.DataIntegrity;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// A record of the company-level accounts receivable preferences.
/// The parameters defined by this record include various numbering sequence
/// settings and document processing options. The single record of this type
/// is created and edited on the Accounts Receivable Preferences (AR101000) form,
/// which corresponds to the <see cref="T:PX.Objects.AR.ARSetupMaint" /> graph.
/// </summary>
[PXPrimaryGraph(typeof (ARSetupMaint))]
[PXCacheName("Account Receivable Preferences")]
[Serializable]
public class ARSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _BatchNumberingID;
  protected string _DfltCustomerClassID;
  protected short? _PerRetainTran;
  protected short? _PerRetainHist;
  protected string _InvoiceNumberingID;
  protected string _PaymentNumberingID;
  protected string _CreditAdjNumberingID;
  protected string _DebitAdjNumberingID;
  protected string _WriteOffNumberingID;
  protected string _PriceWSNumberingID;
  protected string _DefaultTranDesc;
  protected string _SalesSubMask;
  protected bool? _AutoPost;
  protected string _TransactionPosting;
  protected string _FinChargeNumberingID;
  protected bool? _FinChargeOnCharge;
  protected bool? _AgeCredits;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected bool? _HoldEntry;
  protected bool? _RequireControlTotal;
  protected bool? _RequireExtRef;
  protected bool? _CreditCheckError;
  protected string _SPCommnCalcType;
  protected string _SPCommnPeriodType;
  protected bool? _DefFinChargeFromCycle;
  protected bool? _FinChargeFirst;
  protected bool? _PrintBeforeRelease;
  protected bool? _EmailBeforeRelease;
  protected bool? _IntegratedCCProcessing;
  protected bool? _ConsolidatedStatement;
  protected int? _StatementBranchID;
  protected int? _DunningLetterBranchID;
  protected int? _DunningLetterProcessType;
  protected bool? _AutoReleaseDunningFee;
  protected bool? _AutoReleaseDunningLetter;
  protected bool? _IncludeNonOverdueDunning;
  protected bool? _AddOpenPaymentsAndCreditMemos;
  protected bool? _AddUnpaidPPI;
  protected int? _DunningFeeInventoryID;
  protected Decimal? _InvoicePrecision;
  protected string _InvoiceRounding;
  protected string _BalanceWriteOff;
  protected string _CreditWriteOff;
  protected string _DefaultRateTypeID;
  protected bool? _AlwaysFromBaseCury;
  protected string _LineDiscountTarget;
  protected string _ApplyQuantityDiscountBy;
  protected string _RetentionType;
  protected int? _NumberOfMonths;
  protected bool? _AutoReleasePPDCreditMemo;
  protected string _PPDCreditMemoDescr;
  protected Guid? _NoteID;

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("BATCH")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string BatchNumberingID
  {
    get => this._BatchNumberingID;
    set => this._BatchNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (CustomerClass.customerClassID), DescriptionField = typeof (CustomerClass.descr), CacheGlobal = true)]
  public virtual string DfltCustomerClassID
  {
    get => this._DfltCustomerClassID;
    set => this._DfltCustomerClassID = value;
  }

  [PXDBShort]
  [PXDefault(99)]
  [PXUIField]
  public virtual short? PerRetainTran
  {
    get => this._PerRetainTran;
    set => this._PerRetainTran = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField]
  public virtual short? PerRetainHist
  {
    get => this._PerRetainHist;
    set => this._PerRetainHist = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("ARINVOICE")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string InvoiceNumberingID
  {
    get => this._InvoiceNumberingID;
    set => this._InvoiceNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("ARPAYMENT")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string PaymentNumberingID
  {
    get => this._PaymentNumberingID;
    set => this._PaymentNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("ARINVOICE")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string CreditAdjNumberingID
  {
    get => this._CreditAdjNumberingID;
    set => this._CreditAdjNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("ARINVOICE")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string DebitAdjNumberingID
  {
    get => this._DebitAdjNumberingID;
    set => this._DebitAdjNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("ARINVOICE")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string WriteOffNumberingID
  {
    get => this._WriteOffNumberingID;
    set => this._WriteOffNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("ARINVOICE")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string PrepaymentInvoiceNumberingID { get; set; }

  [PXDefault("PMTRAN")]
  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField(DisplayName = "Usage Transaction Numbering Sequence")]
  public virtual string UsageNumberingID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("ARPRICEWS")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string PriceWSNumberingID
  {
    get => this._PriceWSNumberingID;
    set => this._PriceWSNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("ARINVOICE")]
  [PXRestrictor(typeof (Where<Numbering.userNumbering, Equal<False>>), "Manual Numbering is not allowed for generating fee documents.", new Type[] {})]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string DunningFeeNumberingID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("C")]
  [PXStringList(new string[] {"C", "I", "N", "U"}, new string[] {"Combination ID and Name", "Vendor ID", "Vendor Name", "User Entered Description"})]
  [PXUIField]
  public virtual string DefaultTranDesc
  {
    get => this._DefaultTranDesc;
    set => this._DefaultTranDesc = value;
  }

  [PXDefault]
  [SubAccountMask(DisplayName = "Combine Sales Sub. From")]
  public virtual string SalesSubMask
  {
    get => this._SalesSubMask;
    set => this._SalesSubMask = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? AutoPost
  {
    get => this._AutoPost;
    set => this._AutoPost = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("D")]
  [PXUIField]
  [PXStringList(new string[] {"S", "D"}, new string[] {"Summary", "Detail"})]
  public virtual string TransactionPosting
  {
    get => this._TransactionPosting;
    set => this._TransactionPosting = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("ARINVOICE")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string FinChargeNumberingID
  {
    get => this._FinChargeNumberingID;
    set => this._FinChargeNumberingID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? FinChargeOnCharge
  {
    get => this._FinChargeOnCharge;
    set => this._FinChargeOnCharge = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? AgeCredits
  {
    get => this._AgeCredits;
    set => this._AgeCredits = value;
  }

  [Obsolete("This field is not used anymore and will be removed in 2018R2. Use DataInconsistencyHandlingModeinstead.")]
  public virtual bool? ValidateDataConsistencyOnRelease { get; set; }

  [PXDBString(1)]
  [PXDefault("L")]
  [PXUIField(DisplayName = "Extra Data Validation")]
  [LabelList(typeof (InconsistencyHandlingMode))]
  public virtual string DataInconsistencyHandlingMode { get; set; }

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

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Hold Documents on Entry")]
  public virtual bool? HoldEntry
  {
    get => this._HoldEntry;
    set => this._HoldEntry = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Validate Document Totals on Entry")]
  public virtual bool? RequireControlTotal
  {
    get => this._RequireControlTotal;
    set => this._RequireControlTotal = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that users must fill Ext. Ref. Number box (<see cref="P:PX.Objects.GL.GLTranDoc.ExtRefNbr">GLTranDoc.ExtRefNbr</see>).
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Require Payment Reference on Entry")]
  public virtual bool? RequireExtRef
  {
    get => this._RequireExtRef;
    set => this._RequireExtRef = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? SummaryPost
  {
    get => new bool?(this._TransactionPosting == "S");
    set => this._TransactionPosting = value.GetValueOrDefault() ? "S" : "D";
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? CreditCheckError
  {
    get => this._CreditCheckError;
    set => this._CreditCheckError = value;
  }

  [PXDBString(1)]
  [PXDefault("I")]
  [PXUIField]
  [SPCommnCalcTypes.List]
  public virtual string SPCommnCalcType
  {
    get => this._SPCommnCalcType;
    set => this._SPCommnCalcType = value;
  }

  [PXDBString(1)]
  [PXDefault("M")]
  [PXUIField(DisplayName = "Commission Period Type")]
  [SPCommnPeriodTypes.List]
  public virtual string SPCommnPeriodType
  {
    get => this._SPCommnPeriodType;
    set => this._SPCommnPeriodType = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Set Default Overdue Charges by Statement Cycle")]
  public virtual bool? DefFinChargeFromCycle
  {
    get => this._DefFinChargeFromCycle;
    set => this._DefFinChargeFromCycle = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Apply Payments to Overdue Charges First")]
  public virtual bool? FinChargeFirst
  {
    get => this._FinChargeFirst;
    set => this._FinChargeFirst = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Invoice/Memo Printing Before Release")]
  public virtual bool? PrintBeforeRelease
  {
    get => this._PrintBeforeRelease;
    set => this._PrintBeforeRelease = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Invoice/Memo Emailing Before Release")]
  public virtual bool? EmailBeforeRelease
  {
    get => this._EmailBeforeRelease;
    set => this._EmailBeforeRelease = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? IntegratedCCProcessing
  {
    get => this._IntegratedCCProcessing;
    set => this._IntegratedCCProcessing = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Consolidate Statements for all Branches", FieldClass = "COMPANYBRANCH")]
  public virtual bool? ConsolidatedStatement
  {
    get => this._ConsolidatedStatement;
    set => this._ConsolidatedStatement = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("C")]
  [PXUIField(DisplayName = "Prepare Statements")]
  [ARSetup.prepareStatements.List]
  public virtual string PrepareStatements { get; set; }

  [Branch(null, null, true, true, true)]
  public virtual int? StatementBranchID
  {
    get => this._StatementBranchID;
    set => this._StatementBranchID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("C")]
  [PXUIField(DisplayName = "Prepare Dunning Letters")]
  [ARSetup.prepareDunningLetters.List]
  public virtual string PrepareDunningLetters { get; set; }

  [Branch(null, null, true, true, true)]
  public virtual int? DunningLetterBranchID
  {
    get => this._DunningLetterBranchID;
    set => this._DunningLetterBranchID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [DunningProcessType.List]
  [PXUIField]
  public virtual int? DunningLetterProcessType
  {
    get => this._DunningLetterProcessType;
    set => this._DunningLetterProcessType = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? AutoReleaseDunningFee
  {
    get => this._AutoReleaseDunningFee;
    set => this._AutoReleaseDunningFee = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? AutoReleaseDunningLetter
  {
    get => this._AutoReleaseDunningLetter;
    set => this._AutoReleaseDunningLetter = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? IncludeNonOverdueDunning
  {
    get => this._IncludeNonOverdueDunning;
    set => this._IncludeNonOverdueDunning = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? AddOpenPaymentsAndCreditMemos
  {
    get => this._AddOpenPaymentsAndCreditMemos;
    set => this._AddOpenPaymentsAndCreditMemos = value;
  }

  /// <summary>
  /// Specifies default behavior of Prepayment Invoices on Prepare Unning Letters. Specific for feature VAT Recognition on Prepayments.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? AddUnpaidPPI
  {
    get => this._AddUnpaidPPI;
    set => this._AddUnpaidPPI = value;
  }

  [PXDefault]
  [NonStockItem(DisplayName = "Dunning Fee Item")]
  [PXForeignReference(typeof (Field<ARSetup.dunningFeeInventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? DunningFeeInventoryID
  {
    get => this._DunningFeeInventoryID;
    set => this._DunningFeeInventoryID = value;
  }

  /// <summary>
  /// The identifier of the Dunning Fee <see cref="T:PX.Objects.CS.Terms" /> object that can be set for all dunning fees as default one.
  /// Optinal field that defaults the credit terms for dunning fee invoice.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [ARTermsSelector]
  public virtual string DunningFeeTermID { get; set; }

  [PXDBDecimalString(2)]
  [PX.Objects.CS.InvoicePrecision.List]
  [PXDefault(TypeCode.Decimal, "0.1")]
  [PXUIField(DisplayName = "Rounding Precision")]
  public virtual Decimal? InvoicePrecision
  {
    get => this._InvoicePrecision;
    set => this._InvoicePrecision = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Rounding Rule for Invoices")]
  [PX.Objects.CS.InvoiceRounding.List]
  public virtual string InvoiceRounding
  {
    get => this._InvoiceRounding;
    set => this._InvoiceRounding = value;
  }

  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.balanceWriteOff>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXUIField]
  [PXForeignReference(typeof (Field<ARSetup.balanceWriteOff>.IsRelatedTo<PX.Objects.CS.ReasonCode.reasonCodeID>))]
  public virtual string BalanceWriteOff
  {
    get => this._BalanceWriteOff;
    set => this._BalanceWriteOff = value;
  }

  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.creditWriteOff>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXUIField]
  [PXForeignReference(typeof (Field<ARSetup.creditWriteOff>.IsRelatedTo<PX.Objects.CS.ReasonCode.reasonCodeID>))]
  public virtual string CreditWriteOff
  {
    get => this._CreditWriteOff;
    set => this._CreditWriteOff = value;
  }

  [PXDBString(6, IsUnicode = true)]
  [PXSelector(typeof (PX.Objects.CM.CurrencyRateType.curyRateTypeID), DescriptionField = typeof (PX.Objects.CM.CurrencyRateType.descr))]
  [PXForeignReference(typeof (Field<ARSetup.defaultRateTypeID>.IsRelatedTo<PX.Objects.CM.CurrencyRateType.curyRateTypeID>))]
  [PXUIField(DisplayName = "Default Rate Type")]
  public virtual string DefaultRateTypeID
  {
    get => this._DefaultRateTypeID;
    set => this._DefaultRateTypeID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Always Calculate Price from Base Currency Price")]
  public virtual bool? AlwaysFromBaseCury
  {
    get => this._AlwaysFromBaseCury;
    set => this._AlwaysFromBaseCury = value;
  }

  /// <summary>
  /// When set to <c>true</c>, makes it possible to load
  /// <see cref="T:PX.Objects.AR.ARSalesPrice">Sales Prices</see> by
  /// alternate ID
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Load Sales Prices by Alternate ID")]
  public virtual bool? LoadSalesPricesUsingAlternateID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("E")]
  [LineDiscountTargetType.List]
  [PXUIField]
  public virtual string LineDiscountTarget
  {
    get => this._LineDiscountTarget;
    set => this._LineDiscountTarget = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("L")]
  [ApplyQuantityDiscountType.List]
  [PXUIField]
  public virtual string ApplyQuantityDiscountBy
  {
    get => this._ApplyQuantityDiscountBy;
    set => this._ApplyQuantityDiscountBy = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("L")]
  [RetentionTypeList.List]
  [PXUIField]
  public virtual string RetentionType
  {
    get => this._RetentionType;
    set => this._RetentionType = value;
  }

  [PXDBInt]
  [PXDefault(12)]
  [PXUIField]
  public virtual int? NumberOfMonths
  {
    get => this._NumberOfMonths;
    set => this._NumberOfMonths = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if it is <see langword="true" />) that the credit memo generated on the Generate AR Tax Adjustments (AR504500) form will be released automatically.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Automatically Release Tax Adjustments")]
  public virtual bool? AutoReleasePPDCreditMemo
  {
    get => this._AutoReleasePPDCreditMemo;
    set => this._AutoReleasePPDCreditMemo = value;
  }

  /// <summary>
  /// The description of the credit memo generated on the Generate AR Tax Adjustments (AR504500) form.
  /// </summary>
  [PXDBLocalizableString(150, IsUnicode = true)]
  [PXUIField]
  public virtual string PPDCreditMemoDescr
  {
    get => this._PPDCreditMemoDescr;
    set => this._PPDCreditMemoDescr = value;
  }

  /// <summary>
  /// Activates possibility to use Credit Terms in credit memos
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? TermsInCreditMemos { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that migration mode is activated for the AR module.
  /// In other words, this gives an ability to create the document with starting balance without any applications.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Activate Migration Mode")]
  public virtual bool? MigrationMode { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? RetainTaxes { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? RetainageInvoicesAutoRelease { get; set; }

  [PXDBShort]
  [PXDefault(100)]
  [PXUIField]
  public virtual short? AutoLoadMaxDocs { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("L")]
  [PXUIField(DisplayName = "Use Intercompany Sales Account From", FieldClass = "InterBranch")]
  [ARSetup.intercompanySalesAccountDefault.List]
  public virtual string IntercompanySalesAccountDefault { get; set; }

  public static class FK
  {
    public class DefaultCustomerClass : 
      PrimaryKeyOf<CustomerClass>.By<CustomerClass.customerClassID>.ForeignKeyOf<ARSetup>.By<ARSetup.dfltCustomerClassID>
    {
    }

    public class GLBatchNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<ARSetup>.By<ARSetup.batchNumberingID>
    {
    }

    public class InvoiceNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<ARSetup>.By<ARSetup.invoiceNumberingID>
    {
    }

    public class PaymentNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<ARSetup>.By<ARSetup.paymentNumberingID>
    {
    }

    public class DebitMemoNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<ARSetup>.By<ARSetup.debitAdjNumberingID>
    {
    }

    public class CreditMemoNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<ARSetup>.By<ARSetup.creditAdjNumberingID>
    {
    }

    public class WriteOffNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<ARSetup>.By<ARSetup.writeOffNumberingID>
    {
    }

    public class PrepaymentInvoiceNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<ARSetup>.By<ARSetup.prepaymentInvoiceNumberingID>
    {
    }

    public class OverdueChargeNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<ARSetup>.By<ARSetup.finChargeNumberingID>
    {
    }

    public class UsageTransactionNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<ARSetup>.By<ARSetup.usageNumberingID>
    {
    }

    public class PriceWorksheetNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<ARSetup>.By<ARSetup.priceWSNumberingID>
    {
    }

    public class DunningFeeNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<ARSetup>.By<ARSetup.dunningFeeNumberingID>
    {
    }

    public class StatementFromBranch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARSetup>.By<ARSetup.statementBranchID>
    {
    }

    public class DunningLetterFromBranch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARSetup>.By<ARSetup.dunningLetterBranchID>
    {
    }

    public class DunningFeeTerms : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<ARSetup>.By<ARSetup.dunningFeeTermID>
    {
    }
  }

  public abstract class batchNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.batchNumberingID>
  {
  }

  public abstract class dfltCustomerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.dfltCustomerClassID>
  {
  }

  public abstract class perRetainTran : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARSetup.perRetainTran>
  {
  }

  public abstract class perRetainHist : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARSetup.perRetainHist>
  {
  }

  public abstract class invoiceNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.invoiceNumberingID>
  {
  }

  public abstract class paymentNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.paymentNumberingID>
  {
  }

  public abstract class creditAdjNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.creditAdjNumberingID>
  {
  }

  public abstract class debitAdjNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.debitAdjNumberingID>
  {
  }

  public abstract class writeOffNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.writeOffNumberingID>
  {
  }

  public abstract class prepaymentInvoiceNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.prepaymentInvoiceNumberingID>
  {
  }

  public abstract class usageNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.usageNumberingID>
  {
  }

  public abstract class priceWSNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.priceWSNumberingID>
  {
  }

  public abstract class dunningFeeNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.dunningFeeNumberingID>
  {
  }

  public abstract class defaultTranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSetup.defaultTranDesc>
  {
  }

  public abstract class salesSubMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSetup.salesSubMask>
  {
  }

  public abstract class autoPost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSetup.autoPost>
  {
  }

  public abstract class transactionPosting : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.transactionPosting>
  {
  }

  public abstract class finChargeNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.finChargeNumberingID>
  {
  }

  public abstract class finChargeOnCharge : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSetup.finChargeOnCharge>
  {
  }

  public abstract class ageCredits : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSetup.ageCredits>
  {
  }

  [Obsolete("This field is not used anymore and will be removed in 2018R2. Use DataInconsistencyHandlingModeinstead.")]
  public abstract class validateDataConsistencyOnRelease : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARSetup.validateDataConsistencyOnRelease>
  {
  }

  public abstract class dataInconsistencyHandlingMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.dataInconsistencyHandlingMode>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARSetup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARSetup.lastModifiedDateTime>
  {
  }

  public abstract class holdEntry : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSetup.holdEntry>
  {
  }

  public abstract class requireControlTotal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARSetup.requireControlTotal>
  {
  }

  public abstract class requireExtRef : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSetup.requireExtRef>
  {
  }

  public abstract class summaryPost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSetup.summaryPost>
  {
  }

  public abstract class creditCheckError : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSetup.creditCheckError>
  {
  }

  public abstract class sPCommnCalcType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSetup.sPCommnCalcType>
  {
  }

  public abstract class sPCommnPeriodType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.sPCommnPeriodType>
  {
  }

  public abstract class defFinChargeFromCycle : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARSetup.defFinChargeFromCycle>
  {
  }

  public abstract class finChargeFirst : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSetup.finChargeFirst>
  {
  }

  public abstract class printBeforeRelease : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARSetup.printBeforeRelease>
  {
  }

  public abstract class emailBeforeRelease : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARSetup.emailBeforeRelease>
  {
  }

  public abstract class integratedCCProcessing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARSetup.integratedCCProcessing>
  {
  }

  public abstract class consolidatedStatement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARSetup.consolidatedStatement>
  {
  }

  public abstract class prepareStatements : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    ARSetup.prepareStatements>
  {
    public const string ForEachBranch = "B";
    public const string ConsolidatedForCompany = "C";
    public const string ConsolidatedForAllCompanies = "A";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[3]{ "B", "C", "A" }, new string[3]
        {
          "For Each Branch",
          "Consolidated for Company",
          "Consolidated for All Companies"
        })
      {
      }

      public virtual void CacheAttached(PXCache sender)
      {
        List<string> stringList1 = new List<string>();
        List<string> stringList2 = new List<string>();
        if (PXAccess.FeatureInstalled<FeaturesSet.branch>())
        {
          stringList1.Add("B");
          stringList2.Add("For Each Branch");
        }
        stringList1.Add("C");
        stringList2.Add("Consolidated for Company");
        if (PXAccess.FeatureInstalled<FeaturesSet.multiCompany>())
        {
          stringList1.Add("A");
          stringList2.Add("Consolidated for All Companies");
        }
        this._AllowedValues = stringList1.ToArray();
        this._AllowedLabels = stringList2.ToArray();
        this._NeutralAllowedLabels = (string[]) null;
        base.CacheAttached(sender);
      }
    }
  }

  public abstract class statementBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSetup.statementBranchID>
  {
  }

  public abstract class prepareDunningLetters : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    ARSetup.prepareDunningLetters>
  {
    public const string ForEachBranch = "B";
    public const string ConsolidatedForCompany = "C";
    public const string ConsolidatedForAllCompanies = "A";

    public class ListAttribute : ARSetup.prepareStatements.ListAttribute
    {
    }
  }

  public abstract class dunningLetterBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARSetup.dunningLetterBranchID>
  {
  }

  public abstract class dunningLetterProcessType : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARSetup.dunningLetterProcessType>
  {
  }

  public abstract class autoReleaseDunningFee : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARSetup.autoReleaseDunningFee>
  {
  }

  public abstract class autoReleaseDunningLetter : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARSetup.autoReleaseDunningLetter>
  {
  }

  public abstract class includeNonOverdueDunning : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARSetup.includeNonOverdueDunning>
  {
  }

  public abstract class addOpenPaymentsAndCreditMemos : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARSetup.addOpenPaymentsAndCreditMemos>
  {
  }

  public abstract class addUnpaidPPI : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSetup.addUnpaidPPI>
  {
  }

  public abstract class dunningFeeInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARSetup.dunningFeeInventoryID>
  {
  }

  public abstract class dunningFeeTermID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.dunningFeeTermID>
  {
  }

  public abstract class invoicePrecision : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARSetup.invoicePrecision>
  {
  }

  public abstract class invoiceRounding : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSetup.invoiceRounding>
  {
  }

  public abstract class balanceWriteOff : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSetup.balanceWriteOff>
  {
  }

  public abstract class creditWriteOff : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSetup.creditWriteOff>
  {
  }

  public abstract class defaultRateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.defaultRateTypeID>
  {
  }

  public abstract class alwaysFromBaseCury : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARSetup.alwaysFromBaseCury>
  {
  }

  public abstract class loadSalesPricesUsingAlternateID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARSetup.loadSalesPricesUsingAlternateID>
  {
  }

  public abstract class lineDiscountTarget : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.lineDiscountTarget>
  {
  }

  public abstract class applyQuantityDiscountBy : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.applyQuantityDiscountBy>
  {
  }

  public abstract class retentionType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSetup.retentionType>
  {
  }

  public abstract class numberOfMonths : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSetup.numberOfMonths>
  {
  }

  public abstract class autoReleasePPDCreditMemo : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARSetup.autoReleasePPDCreditMemo>
  {
  }

  public abstract class pPDCreditMemoDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetup.pPDCreditMemoDescr>
  {
  }

  public abstract class termsInCreditMemos : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARSetup.termsInCreditMemos>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARSetup.noteID>
  {
  }

  public abstract class migrationMode : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSetup.migrationMode>
  {
  }

  public abstract class retainTaxes : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSetup.retainTaxes>
  {
  }

  public abstract class retainageInvoicesAutoRelease : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARSetup.retainageInvoicesAutoRelease>
  {
  }

  public abstract class autoLoadMaxDocs : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARSetup.autoLoadMaxDocs>
  {
  }

  public abstract class intercompanySalesAccountDefault : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    ARSetup.intercompanySalesAccountDefault>
  {
    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "L", "I" }, new string[2]
        {
          ARSetup.intercompanySalesAccountDefault.ListAttribute.MaskCustomerLocationLabel,
          "Inventory Item"
        })
      {
      }

      public static string MaskCustomerLocationLabel
      {
        get
        {
          return !PXAccess.FeatureInstalled<FeaturesSet.accountLocations>() ? "Customer" : "Customer Location";
        }
      }
    }
  }
}
