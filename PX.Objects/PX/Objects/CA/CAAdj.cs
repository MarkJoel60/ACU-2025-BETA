// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAAdj
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Abstractions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.TX;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// The main properties of CA transactions.
/// CA transaction are edited on the Cash Transactions (CA304000) form
/// (which corresponds to the <see cref="T:PX.Objects.CA.CATranEntry" /> graph).
/// </summary>
[PXPrimaryGraph(typeof (CATranEntry))]
[PXCacheName("Cash Transactions")]
[Serializable]
public class CAAdj : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ICADocument,
  IAssign,
  PX.Objects.CM.IRegister,
  IDocumentKey,
  INotable
{
  protected bool? _DepositAsBatch;
  protected DateTime? _DepositAfter;
  protected bool? _Deposited;
  protected 
  #nullable disable
  string _DepositType;
  protected string _DepositNbr;

  /// <summary>
  /// The type of the cash transaction.
  /// Returns the value of the <see cref="P:PX.Objects.CA.CAAdj.AdjTranType" /> field.
  /// This field implements the ICADocument interface member.
  /// </summary>
  public string DocType
  {
    get => this.AdjTranType;
    set => this.AdjTranType = value;
  }

  /// <summary>
  /// The user-friendly unique identifier assigned to the cash transaction in accordance with the numbering sequence.
  /// Returns the value of the <see cref="P:PX.Objects.CA.CAAdj.AdjRefNbr" /> field.
  /// This field implements the ICADocument interface member.
  /// </summary>
  public string RefNbr
  {
    get => this.AdjRefNbr;
    set => this.AdjRefNbr = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  /// <summary>The branch of the cash transaction.</summary>
  [Branch(typeof (Search<CashAccount.branchID, Where<CashAccount.cashAccountID, Equal<Current<CAAdj.cashAccountID>>>>), null, true, true, true)]
  [PXFormula(typeof (Default<CAAdj.cashAccountID>))]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// The type of the cash transaction.
  /// On the Transactions (CA304000) form, transactions of the only Cash Entry type are created.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"CTE"</c>: Expense entry,
  /// <c>"CAE"</c>: Cash entry.
  /// </value>
  [PXDBString(3, IsFixed = true, IsKey = true)]
  [CATranType.List]
  [PXDefault("CAE")]
  [PXUIField]
  public virtual string AdjTranType { get; set; }

  /// <summary>
  /// The user-friendly unique identifier assigned to the cash transaction in accordance with the numbering sequence.
  /// This field is the key field.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<CAAdj.adjRefNbr, Where<CAAdj.draft, Equal<False>>>), new System.Type[] {typeof (CAAdj.adjRefNbr), typeof (CAAdj.extRefNbr), typeof (CAAdj.cashAccountID), typeof (CAAdj.tranDate), typeof (CAAdj.tranDesc), typeof (CAAdj.employeeID), typeof (CAAdj.entryTypeID), typeof (CAAdj.status), typeof (CAAdj.curyID), typeof (CAAdj.curyTranAmt)})]
  [AutoNumber(typeof (CASetup.registerNumberingID), typeof (CAAdj.tranDate))]
  public virtual string AdjRefNbr { get; set; }

  /// <summary>The unique identifier of the cash transfer.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CA.CATransfer.TransferNbr" /> field.
  /// The value of the field can be empty.
  /// If the value of the field is not empty, this cash transaction is an expense entry (<see cref="P:PX.Objects.CA.CAAdj.AdjTranType" /> is <c>"CTE"</c>), which was created on the Funds Transfers (CA301000) form.
  /// Then this cash transaction is an expense entry from the form Funds Transfer (CA301000).
  /// </value>
  [Obsolete("Will be removed in Acumatica 2019R2")]
  [PXDBString(15, IsUnicode = true)]
  public virtual string TransferNbr { get; set; }

  /// <summary>The reference number of the external document.</summary>
  [PXDBString(40, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string ExtRefNbr { get; set; }

  /// <summary>
  /// The cash account that is the source account for the transaction.
  /// </summary>
  [PXDefault]
  [CashAccount(null, typeof (Search<CashAccount.cashAccountID, Where<Match<Current<AccessInfo.userName>>>>), true)]
  public virtual int? CashAccountID { get; set; }

  /// <summary>The date of the transaction.</summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? TranDate { get; set; }

  /// <summary>
  /// The basic type of the transaction: Receipt or Disbursement.
  /// </summary>
  [PXDefault("D")]
  [PXDBString(1, IsFixed = true)]
  [CADrCr.List]
  [PXUIField(DisplayName = "Disbursement/Receipt", Enabled = false)]
  public virtual string DrCr { get; set; }

  /// <summary>The description of the transaction.</summary>
  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string TranDesc { get; set; }

  /// <summary>
  /// <see cref="T:PX.Objects.GL.FinPeriods.OrganizationFinPeriod">financial period</see> of the document.
  /// </summary>
  /// <value>
  /// Is determined by the <see cref="P:PX.Objects.CA.CAAdj.TranDate">date of the cash transaction</see>.
  /// Unlike <see cref="P:PX.Objects.CA.CAAdj.FinPeriodID" /> the value of this field can't be overridden by a user.
  /// </value>
  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID { get; set; }

  /// <summary>
  /// <see cref="T:PX.Objects.GL.FinPeriods.OrganizationFinPeriod">financial period</see> of the document.
  /// </summary>
  /// <value>
  /// Defaults to the period to which the <see cref="!:APRegister.DocDate" /> belongs, but can be overridden by a user.
  /// </value>
  [CAOpenPeriod(typeof (CAAdj.tranDate), typeof (CAAdj.cashAccountID), typeof (Selector<CAAdj.cashAccountID, CashAccount.branchID>), null, null, null, true, typeof (CAAdj.tranPeriodID), IsHeader = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Fin. Period")]
  public virtual string FinPeriodID { get; set; }

  /// <summary>The tax zone that applies to the transaction.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.TX.TaxZone.TaxZoneID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  [PXDefault(typeof (Search<CashAccountETDetail.taxZoneID, Where<CashAccountETDetail.cashAccountID, Equal<Current<CAAdj.cashAccountID>>, And<CashAccountETDetail.entryTypeID, Equal<Current<CAAdj.entryTypeID>>>>>))]
  public virtual string TaxZoneID { get; set; }

  /// <summary>
  /// The identifier of the exchange rate record for the deposit.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>The type of the original cash transaction.</summary>
  [PXDBString(3, IsFixed = true)]
  [PXUIField]
  [CATranType.List]
  public virtual string OrigAdjTranType { get; set; }

  /// <summary>The number of the original CA transaction.</summary>
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<CAAdj.adjRefNbr, Where<CAAdj.adjTranType, Equal<Current<CAAdj.origAdjTranType>>>>), new System.Type[] {typeof (CAAdj.adjRefNbr), typeof (CAAdj.extRefNbr), typeof (CAAdj.cashAccountID), typeof (CAAdj.tranDate), typeof (CAAdj.tranDesc), typeof (CAAdj.employeeID), typeof (CAAdj.entryTypeID), typeof (CAAdj.status), typeof (CAAdj.curyID), typeof (CAAdj.curyTranAmt)})]
  public virtual string OrigAdjRefNbr { get; set; }

  /// <summary>
  /// The read-only field, reflecting the number of transactions in the system, which reverse this transaction.
  /// </summary>
  /// <value>
  /// This field is populated only by the <see cref="T:PX.Objects.CA.CATranEntry" /> graph, which corresponds to the Cash Entry (CA304000) form.
  /// </value>
  [PXInt]
  [PXUIField(DisplayName = "Reversing Transactions", Visible = false, Enabled = false, IsReadOnly = true)]
  public int? ReverseCount { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the adjustment was created on the Journal Vouchers (GL304000) form
  /// and the parent <see cref="T:PX.Objects.GL.GLDocBatch" /> record of the adjustment has the <c>"On Hold"</c> status.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Draft")]
  public virtual bool? Draft { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the cash transaction is on hold,
  /// which means that it can be edited but cannot be released.
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (Search<CASetup.holdEntry>))]
  [PXUIField(DisplayName = "Hold")]
  public virtual bool? Hold { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the transaction has been approved by a responsible person.
  /// This field is displayed if the <see cref="!:CASetup.RequestApproval" /> field is set to <c>true</c>.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Approved { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the transaction has been rejected by a responsible person.
  /// When the transaction has been rejected, its status changes to On Hold.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public bool? Rejected { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the transaction was released.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the transaction's tax is validated by the external tax provider.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Is Up to Date", Enabled = false)]
  public virtual bool? IsTaxValid { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the transaction's tax is posted or committed to the external tax provider.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Is Posted/Committed to External Tax Engine (Avalara)", Enabled = false)]
  public virtual bool? IsTaxPosted { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the transaction's tax is saved in the external tax provider.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax has been saved in the external tax provider", Enabled = false)]
  public virtual bool? IsTaxSaved { get; set; }

  /// <summary>
  /// Get or set NonTaxable that mark current document does not impose sales taxes.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Non-Taxable", Enabled = false)]
  public virtual bool? NonTaxable { get; set; }

  /// <summary>
  /// The total amount of tax paid on the document in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CAAdj.curyInfoID), typeof (CAAdj.taxTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxTotal { get; set; }

  /// <summary>
  /// The total amount of tax paid on the document in the base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTotal { get; set; }

  /// <summary>
  /// The document total that is exempt from VAT in the selected currency.
  /// This total is calculated as the taxable amount for the tax
  /// with the <see cref="P:PX.Objects.TX.Tax.ExemptTax" /> field set to <c>true</c> (that is, the Include in VAT Exempt Total check box selected on the Taxes (TX205000) form).
  /// </summary>
  [PXDBCurrency(typeof (CAAdj.curyInfoID), typeof (CAAdj.vatExemptTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatExemptTotal { get; set; }

  /// <summary>
  /// The document total that is exempt from VAT in the base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatExemptTotal { get; set; }

  /// <summary>
  /// The document total that is subjected to VAT in the selected currency.
  /// The field is displayed only if
  /// the <see cref="P:PX.Objects.TX.Tax.IncludeInTaxable" /> field is set to <c>true</c> (that is, the Include in VAT Exempt Total check box is selected on the Taxes (TX205000) form).
  /// </summary>
  [PXDBCurrency(typeof (CAAdj.curyInfoID), typeof (CAAdj.vatTaxableTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatTaxableTotal { get; set; }

  /// <summary>
  /// The document total that is subjected to VAT in the base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatTaxableTotal { get; set; }

  /// <summary>
  /// The tax amount to be paid for the document in the selected currency.
  /// This field is enable and visible only if the <see cref="P:PX.Objects.CA.CASetup.RequireControlTaxTotal" /> field
  /// and the <see cref="P:PX.Objects.CS.FeaturesSet.NetGrossEntryMode" /> field are set to <c>true</c>.
  /// </summary>
  [PXDBCurrency(typeof (CAAdj.curyInfoID), typeof (CAAdj.taxAmt))]
  [PXUIField(DisplayName = "Tax Amount")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxAmt { get; set; }

  /// <summary>
  /// The tax amount to be paid for the document in the base currency.
  /// </summary>
  [PXDBBaseCury(BqlField = typeof (CAAdj.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxAmt { get; set; }

  /// <summary>
  /// The sum of amounts of all detail lines in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CAAdj.curyInfoID), typeof (CAAdj.splitTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CurySplitTotal { get; set; }

  /// <summary>
  /// The sum of amounts of all detail lines in the base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? SplitTotal { get; set; }

  /// <summary>
  /// The amount of the transaction in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CAAdj.curyInfoID), typeof (CAAdj.tranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount", Enabled = false)]
  public virtual Decimal? CuryTranAmt { get; set; }

  /// <summary>The amount of the transaction in the base currency.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tran. Amount", Enabled = false)]
  public virtual Decimal? TranAmt { get; set; }

  /// <summary>The currency of the cash transaction.</summary>
  /// <value>
  /// Corresponds to the currency of the <see cref="P:PX.Objects.CA.CashAccount.CuryID" /> cash account.
  /// </value>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  [PXDefault(typeof (Search<CashAccount.curyID, Where<CashAccount.cashAccountID, Equal<Current<CAAdj.cashAccountID>>>>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string CuryID { get; set; }

  /// <summary>
  /// The control total of the transaction in the selected currency.
  /// A user enters this amount manually.
  /// This amount should be equal to the <see cref="P:PX.Objects.CA.CAAdj.CurySplitTotal">sum of amounts of all detail lines</see> of the transaction.
  /// </summary>
  [PXDBCurrency(typeof (CAAdj.curyInfoID), typeof (CAAdj.controlAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Total")]
  public virtual Decimal? CuryControlAmt { get; set; }

  /// <summary>
  /// The control total of the transaction in the base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ControlAmt { get; set; }

  /// <summary>The user who created the cash transaction.</summary>
  [PXDBInt]
  [PXDefault(typeof (Search<PX.Objects.EP.EPEmployee.bAccountID, Where<PX.Objects.EP.EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>))]
  [PXSubordinateSelector]
  [PXUIField]
  public virtual int? EmployeeID { get; set; }

  /// <summary>
  /// The ID of the workgroup which was assigned to approve the transaction.
  /// </summary>
  [PXInt]
  [PXSelector(typeof (Search<EPCompanyTree.workGroupID>), SubstituteKey = typeof (EPCompanyTree.description))]
  [PXUIField(DisplayName = "Approval Workgroup ID", Enabled = false)]
  public virtual int? WorkgroupID { get; set; }

  /// <summary>
  /// The ID of the employee who was assigned to approve the transaction.
  /// </summary>
  [Owner(IsDBField = false, DisplayName = "Approver", Enabled = false)]
  public virtual int? OwnerID { get; set; }

  [PXSearchable(4, "{0} {1}", new System.Type[] {typeof (CAAdj.adjTranType), typeof (CAAdj.adjRefNbr)}, new System.Type[] {typeof (CAAdj.entryTypeID), typeof (CAAdj.tranDesc), typeof (CAAdj.extRefNbr)}, NumberFields = new System.Type[] {typeof (CAAdj.adjRefNbr)}, Line1Format = "{0:d}{1}{2}{4}", Line1Fields = new System.Type[] {typeof (CAAdj.tranDate), typeof (CAAdj.entryTypeID), typeof (CAAdj.extRefNbr), typeof (CAAdj.ownerID), typeof (PX.Objects.CR.BAccount.acctName)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (CAAdj.tranDesc)})]
  [PXNote(DescriptionField = typeof (CAAdj.adjRefNbr))]
  public virtual Guid? NoteID { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? CABankTranRefNoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  /// <summary>
  /// The counter of child <see cref="T:PX.Objects.CA.CASplit" /> records.
  /// <c>PXParentAttribute</c> of the <see cref="P:PX.Objects.CA.CASplit.LineNbr" /> field refers to this field.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  /// <summary>
  /// The identifier of the transaction that is recorded to the cash account.
  /// Corresponds to the <see cref="P:PX.Objects.CA.CATran.TranID" /> field.
  /// </summary>
  [PXDBLong]
  [AdjCashTranID]
  [PXSelector(typeof (Search<CATran.tranID>), DescriptionField = typeof (CATran.batchNbr))]
  public virtual long? TranID { get; set; }

  /// <summary>The status of the cash transaction.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.CA.CATransferStatus.ListAttribute" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("B")]
  [PXUIField]
  [CATransferStatus.List]
  [SetStatus]
  [PXDependsOnFields(new System.Type[] {typeof (CAAdj.hold), typeof (CAAdj.approved), typeof (CAAdj.rejected), typeof (CAAdj.released)})]
  public virtual string Status { get; set; }

  /// <summary>
  /// The user-defined transaction type.
  /// Selects the appropriate type from the <see cref="T:PX.Objects.CA.CAEntryType">list of entry types</see> defined for the selected cash account.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search2<CAEntryType.entryTypeId, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>, Where<CashAccountETDetail.cashAccountID, Equal<Current<CAAdj.cashAccountID>>, And<CAEntryType.module, Equal<BatchModule.moduleCA>>>>), DescriptionField = typeof (CAEntryType.descr))]
  [PXDefault(typeof (Search2<CAEntryType.entryTypeId, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>, Where<CashAccountETDetail.cashAccountID, Equal<Current<CAAdj.cashAccountID>>, And<CAEntryType.module, Equal<BatchModule.moduleCA>, And<CashAccountETDetail.isDefault, Equal<True>>>>>))]
  [PXUIField]
  public virtual string EntryTypeID { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that this transaction is used for payments reclassification.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CA.CAEntryType.UseToReclassifyPayments" /> field of the selected <see cref="T:PX.Objects.CA.CAEntryType">entry type</see>.
  /// </value>
  [PXBool]
  [PXDBScalar(typeof (Search<CAEntryType.useToReclassifyPayments, Where<CAEntryType.entryTypeId, Equal<CAAdj.entryTypeID>>>))]
  public virtual bool? PaymentsReclassification { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the transaction was cleared.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Cleared")]
  public virtual bool? Cleared { get; set; }

  /// <summary>The date when the transaction was cleared.</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Clear Date")]
  public virtual DateTime? ClearDate { get; set; }

  /// <summary>
  /// The tax calculation mode, which defines which amounts (tax-inclusive or tax-exclusive)
  /// should be entered in the detail lines of a document.
  /// This field is displayed only if the <see cref="P:PX.Objects.CS.FeaturesSet.NetGrossEntryMode" /> field is set to <c>true</c>.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"T"</c> (Tax Settings): The tax amount for the document is calculated according to the settings of the applicable tax or taxes.
  /// <c>"G"</c> (Gross): The amount in the document detail line includes a tax or taxes.
  /// <c>"N"</c> (Net): The amount in the document detail line does not include taxes.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("T", typeof (Search<CashAccountETDetail.taxCalcMode, Where<CashAccountETDetail.cashAccountID, Equal<Current<CAAdj.cashAccountID>>, And<CashAccountETDetail.entryTypeID, Equal<Current<CAAdj.entryTypeID>>>>>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  /// <summary>
  /// The difference between the original document amount and the rounded amount in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CAAdj.curyInfoID), typeof (CAAdj.taxRoundDiff), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public Decimal? CuryTaxRoundDiff { get; set; }

  /// <summary>
  /// The difference between the original document amount and the rounded amount in the base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? TaxRoundDiff { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that withholding taxes are applied to the document.
  /// This is a technical field, which is calculated on the fly and is used to restrict the values of the <see cref="P:PX.Objects.CA.CAAdj.TaxCalcMode" /> field.
  /// </summary>
  [PXBool]
  [RestrictWithholdingTaxCalcMode(typeof (CAAdj.taxZoneID), typeof (CAAdj.taxCalcMode))]
  public virtual bool? HasWithHoldTax { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that use taxes are applied to the document.
  /// This is a technical field, which is calculated on the fly and is used to restrict the values of the <see cref="P:PX.Objects.CA.CAAdj.TaxCalcMode" /> field.
  /// </summary>
  [PXBool]
  [RestrictUseTaxCalcMode(typeof (CAAdj.taxZoneID), typeof (CAAdj.taxCalcMode))]
  public virtual bool? HasUseTax { get; set; }

  /// <summary>
  /// Indicates that the current document should be excluded from the
  /// approval process.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Don't Approve", Visible = false, Enabled = false)]
  public virtual bool? DontApprove { get; set; }

  /// <summary>
  /// When set to <c>true</c> indicates that the transaction can be included in a deposit.
  /// </summary>
  [PXDBBool]
  [PXDefault(false, typeof (Search<CashAccount.clearingAccount, Where<CashAccount.cashAccountID, Equal<Current<CAAdj.cashAccountID>>>>))]
  [PXUIField(DisplayName = "Batch Deposit", Enabled = false)]
  public virtual bool? DepositAsBatch
  {
    get => this._DepositAsBatch;
    set => this._DepositAsBatch = value;
  }

  /// <summary>
  /// Informational date specified on the document, which is the source of the deposit.
  /// </summary>
  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Deposit After", Enabled = false, Visible = true)]
  public virtual DateTime? DepositAfter
  {
    get => this._DepositAfter;
    set => this._DepositAfter = value;
  }

  /// <summary>The date of deposit.</summary>
  [PXDate]
  [PXUIField(DisplayName = "Batch Deposit Date", Enabled = false, Visible = true)]
  public virtual DateTime? DepositDate { get; set; }

  /// <summary>
  /// When equal to <c>true</c> indicates that the transaction was deposited.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Deposited", Enabled = false)]
  public virtual bool? Deposited
  {
    get => this._Deposited;
    set => this._Deposited = value;
  }

  /// <summary>
  /// The type of the <see cref="T:PX.Objects.CA.CADeposit">deposit document</see>.
  /// </summary>
  [PXDBString(3, IsFixed = true)]
  public virtual string DepositType
  {
    get => this._DepositType;
    set => this._DepositType = value;
  }

  /// <summary>
  /// The reference number of the <see cref="T:PX.Objects.CA.CADeposit">deposit document</see>.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Batch Deposit Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<CADeposit.refNbr, Where<CADeposit.tranType, Equal<Current<CAAdj.depositType>>>>))]
  public virtual string DepositNbr
  {
    get => this._DepositNbr;
    set => this._DepositNbr = value;
  }

  public DateTime? DocDate { get; set; }

  public string DocDesc
  {
    get => this.TranDesc;
    set => this.TranDesc = value;
  }

  public string OrigModule
  {
    get => "CA";
    set
    {
    }
  }

  public Decimal? CuryOrigDocAmt
  {
    get => this.CuryTranAmt;
    set => this.CuryTranAmt = value;
  }

  public Decimal? OrigDocAmt
  {
    get => this.TranAmt;
    set => this.TranAmt = value;
  }

  [PXString]
  [PXFormula(typeof (SmartJoin<Space, Selector<CAAdj.cashAccountID, CashAccount.cashAccountCD>, Selector<CAAdj.cashAccountID, CashAccount.descr>>))]
  public string FormCaptionDescription { get; set; }

  /// <summary>
  /// The tax exemption number for reporting purposes. The field is used if the system is integrated with External Tax Calculation
  /// and the <see cref="P:PX.Objects.CS.FeaturesSet.AvalaraTax">External Tax Calculation Integration</see> feature is enabled.
  /// </summary>
  [PXDefault(typeof (Search2<PX.Objects.CR.Location.cAvalaraExemptionNumber, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<BAccountR.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current<CAAdj.branchID>>>>))]
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Exemption Number")]
  public virtual string ExternalTaxExemptionNumber { get; set; }

  /// <summary>
  /// The entity usage type for reporting purposes. The field is used if the system is integrated with External Tax Calculation
  /// and the <see cref="P:PX.Objects.CS.FeaturesSet.AvalaraTax">External Tax Calculation Integration</see> feature is enabled.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.TX.TXAvalaraCustomerUsageType.ListAttribute" />.
  /// Defaults to the <see cref="P:PX.Objects.CR.Location.CAvalaraCustomerUsageType">Tax Exemption Type</see>
  /// that is specified for the <see cref="P:PX.Objects.CA.CAAdj.BranchID">location of the branch</see>.
  /// </value>
  [PXDefault("0", typeof (Search2<PX.Objects.CR.Location.cAvalaraCustomerUsageType, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<BAccountR.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current<CAAdj.branchID>>>>))]
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Tax Exemption Type")]
  [TXAvalaraCustomerUsageType.List]
  public virtual string EntityUsageType { get; set; }

  public class Events : PXEntityEventBase<CAAdj>.Container<CAAdj.Events>
  {
    public PXEntityEvent<CAAdj> ReleaseDocument;
  }

  public class PK : PrimaryKeyOf<CAAdj>.By<CAAdj.adjTranType, CAAdj.adjRefNbr>
  {
    public static CAAdj Find(
      PXGraph graph,
      string adjTranType,
      string adjRefNbr,
      PKFindOptions options = 0)
    {
      return (CAAdj) PrimaryKeyOf<CAAdj>.By<CAAdj.adjTranType, CAAdj.adjRefNbr>.FindBy(graph, (object) adjTranType, (object) adjRefNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<CAAdj>.By<CAAdj.branchID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CAAdj>.By<CAAdj.cashAccountID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<CAAdj>.By<CAAdj.taxZoneID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CAAdj>.By<CAAdj.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CAAdj>.By<CAAdj.curyID>
    {
    }

    public class Owner : 
      PrimaryKeyOf<PX.Objects.EP.EPEmployee>.By<PX.Objects.EP.EPEmployee.bAccountID>.ForeignKeyOf<CAAdj>.By<CAAdj.employeeID>
    {
    }

    public class CashAccountTransaction : 
      PrimaryKeyOf<CATran>.By<CATran.cashAccountID, CATran.tranID>.ForeignKeyOf<CAAdj>.By<CAAdj.cashAccountID, CAAdj.tranID>
    {
    }

    public class EntryType : 
      PrimaryKeyOf<CAEntryType>.By<CAEntryType.entryTypeId>.ForeignKeyOf<CAAdj>.By<CAAdj.entryTypeID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAAdj.selected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAAdj.branchID>
  {
  }

  public abstract class adjTranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.adjTranType>
  {
  }

  public abstract class adjRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.adjRefNbr>
  {
  }

  [Obsolete("Will be removed in Acumatica 2019R2")]
  public abstract class transferNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.transferNbr>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.extRefNbr>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAAdj.cashAccountID>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CAAdj.tranDate>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.drCr>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.tranDesc>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.tranPeriodID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.finPeriodID>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.taxZoneID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CAAdj.curyInfoID>
  {
  }

  public abstract class origAdjTranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.origAdjTranType>
  {
  }

  public abstract class origAdjRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.origAdjRefNbr>
  {
  }

  public abstract class reverseCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAAdj.reverseCount>
  {
  }

  public abstract class draft : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAAdj.draft>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAAdj.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAAdj.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAAdj.rejected>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAAdj.released>
  {
  }

  public abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAAdj.isTaxValid>
  {
  }

  public abstract class isTaxPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAAdj.isTaxPosted>
  {
  }

  public abstract class isTaxSaved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAAdj.isTaxSaved>
  {
  }

  public abstract class nonTaxable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAAdj.nonTaxable>
  {
  }

  public abstract class curyTaxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAAdj.curyTaxTotal>
  {
  }

  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAAdj.taxTotal>
  {
  }

  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAAdj.curyVatExemptTotal>
  {
  }

  public abstract class vatExemptTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAAdj.vatExemptTotal>
  {
  }

  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAAdj.curyVatTaxableTotal>
  {
  }

  public abstract class vatTaxableTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAAdj.vatTaxableTotal>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAAdj.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAAdj.taxAmt>
  {
  }

  public abstract class curySplitTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAAdj.curySplitTotal>
  {
  }

  public abstract class splitTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAAdj.splitTotal>
  {
  }

  public abstract class curyTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAAdj.curyTranAmt>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAAdj.tranAmt>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.curyID>
  {
  }

  public abstract class curyControlAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAAdj.curyControlAmt>
  {
  }

  public abstract class controlAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAAdj.controlAmt>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAAdj.employeeID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAAdj.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAAdj.ownerID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CAAdj.noteID>
  {
  }

  public abstract class cABankTranRefNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CAAdj.cABankTranRefNoteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CAAdj.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CAAdj.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CAAdj.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CAAdj.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CAAdj.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CAAdj.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CAAdj.Tstamp>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAAdj.lineCntr>
  {
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CAAdj.tranID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.status>
  {
  }

  public abstract class entryTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.entryTypeID>
  {
  }

  public abstract class paymentsReclassification : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CAAdj.paymentsReclassification>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAAdj.cleared>
  {
  }

  public abstract class clearDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CAAdj.clearDate>
  {
  }

  /// <summary>
  /// The number of the batch generated to implement the cash transaction.
  /// The number appears automatically after the transaction is released.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="T:PX.Objects.CA.CATran.batchNbr" /> field.
  /// The value is filled in by the <see cref="M:PX.Objects.CA.CATranEntry.CAAdj_TranID_CATran_BatchNbr_FieldSelecting(PX.Data.PXCache,PX.Data.PXFieldSelectingEventArgs)" /> method.
  /// </value>
  public abstract class tranID_CATran_batchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CAAdj.tranID_CATran_batchNbr>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.taxCalcMode>
  {
  }

  public abstract class curyTaxRoundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAAdj.curyTaxRoundDiff>
  {
  }

  public abstract class taxRoundDiff : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAAdj.taxRoundDiff>
  {
  }

  public abstract class hasWithHoldTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAAdj.hasWithHoldTax>
  {
  }

  public abstract class hasUseTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAAdj.hasUseTax>
  {
  }

  public abstract class dontApprove : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAAdj.dontApprove>
  {
  }

  public abstract class depositAsBatch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAAdj.depositAsBatch>
  {
  }

  public abstract class depositAfter : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CAAdj.depositAfter>
  {
  }

  public abstract class depositDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CAAdj.depositDate>
  {
  }

  public abstract class deposited : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAAdj.deposited>
  {
  }

  public abstract class depositType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.depositType>
  {
  }

  public abstract class depositNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.depositNbr>
  {
  }

  public abstract class externalTaxExemptionNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CAAdj.externalTaxExemptionNumber>
  {
  }

  public abstract class entityUsageType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.entityUsageType>
  {
  }
}
