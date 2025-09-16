// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLTranDoc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common.Abstractions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// Represents a line of a <see cref="T:PX.Objects.GL.GLDocBatch">GL document batch</see> and contains all information
/// required to create the corresponding document or transaction.
/// Records of this type appear in the details grid of the Journal Vouchers (GL304000) form.
/// </summary>
[PXCacheName("Journal Voucher")]
[Serializable]
public class GLTranDoc : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IInvoice,
  PX.Objects.CM.IRegister,
  IDocumentKey
{
  protected int? _CostCodeID;

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch" />, to which the parent <see cref="T:PX.Objects.GL.GLTranDoc">document batch</see> belongs.
  /// </summary>
  /// <value>
  /// The value of this field is obtained from the <see cref="P:PX.Objects.GL.GLDocBatch.BranchID" /> field of the parent batch.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(typeof (GLDocBatch.branchID), null, true, true, true)]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// Key field.
  /// The code of the module, to which the parent <see cref="T:PX.Objects.GL.GLDocBatch">batch</see> belongs.
  /// </summary>
  /// <value>
  /// Defaults to the value of the <see cref="P:PX.Objects.GL.GLDocBatch.Module" /> field of the parent batch.
  /// This field is not supposed to be changed directly.
  /// Possible values are:
  /// "GL", "AP", "AR", "CM", "CA", "IN", "DR", "FA", "PM", "TX", "SO", "PO".
  /// </value>
  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (GLDocBatch))]
  [PXUIField]
  public virtual 
  #nullable disable
  string Module { get; set; }

  /// <summary>
  /// Key field.
  /// Auto-generated unique number of the parent <see cref="T:PX.Objects.GL.GLDocBatch">document batch</see>.
  /// </summary>
  /// <value>
  /// The number is obtained from the <see cref="P:PX.Objects.GL.GLDocBatch.BatchNbr" /> field of the parent batch.
  /// </value>
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (GLDocBatch))]
  [PXParent(typeof (Select<GLDocBatch, Where<GLDocBatch.module, Equal<Current<GLTranDoc.module>>, And<GLDocBatch.batchNbr, Equal<Current<GLTranDoc.batchNbr>>>>>))]
  [PXUIField]
  public virtual string BatchNbr { get; set; }

  /// <summary>
  /// Key field.
  /// The number of the document/transaction line inside the <see cref="T:PX.Objects.GL.GLDocBatch">document batch</see>.
  /// </summary>
  /// <value>
  /// Note that the sequence of line numbers of the transactions belonging to a single document may include gaps.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXLineNbr(typeof (GLDocBatch.lineCntr))]
  public virtual int? LineNbr { get; set; }

  [PXString(15, IsUnicode = true)]
  public virtual string ImportRefNbr { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Ledger" />, to which the parent <see cref="T:PX.Objects.GL.GLDocBatch">document batch</see> belongs.
  /// </summary>
  /// <value>
  /// The value of this field is obtained from the <see cref="P:PX.Objects.GL.GLDocBatch.LedgerID" /> field of the parent batch.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Ledger.LedgerID" /> field.
  /// </value>
  [PXDBInt]
  [PXDBDefault(typeof (GLDocBatch))]
  public virtual int? LedgerID { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.GL.GLTranDoc.LineNbr">line number</see> of the line, which represents the parent document for this line of the batch.
  /// This field is used to define the details (lines) of a document included in the <see cref="T:PX.Objects.GL.GLDocBatch" />.
  /// For more information see <see cref="P:PX.Objects.GL.GLTranDoc.Split" /> field and the documentation for the Journal Vouchers (GL304000) form.
  /// </summary>
  [PXDBInt]
  [PXParent(typeof (Select<GLTranDoc, Where<GLTranDoc.lineNbr, Equal<Current<GLTranDoc.parentLineNbr>>, And<GLTranDoc.module, Equal<Current<GLTranDoc.module>>, And<GLTranDoc.batchNbr, Equal<Current<GLTranDoc.batchNbr>>>>>>))]
  [PXUIField]
  public virtual int? ParentLineNbr { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the document or transaction created from this line should include
  /// the details defined by other <see cref="T:PX.Objects.GL.GLTranDoc">lines</see> of the batch.
  /// See also the <see cref="P:PX.Objects.GL.GLTranDoc.ParentLineNbr" /> field.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Split")]
  public bool? Split { get; set; }

  [CostCode(ReleasedField = typeof (GLTranDoc.released))]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.Currency" /> of the document or transaction that the system creates from the line of the batch.
  /// </summary>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Visible = true, Enabled = false)]
  [PXDefault(typeof (GLDocBatch.curyID))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID { get; set; }

  /// <summary>
  /// Entry code that defines the <see cref="P:PX.Objects.GL.GLTranCode.Module">Module</see>
  /// and the <see cref="P:PX.Objects.GL.GLTranCode.TranType">Type</see> of the document or transaction that the system creates from this line of the batch.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.GLTranCode.TranCode" /> field.
  /// </value>
  [PXString(5, IsUnicode = true, InputMask = ">aaaaa")]
  [PXDBScalar(typeof (Search<GLTranCode.tranCode, Where<GLTranCode.module, Equal<GLTranDoc.tranModule>, And<GLTranCode.tranType, Equal<GLTranDoc.tranType>>>>))]
  [PXSelector(typeof (Search<GLTranCode.tranCode, Where<GLTranCode.active, Equal<True>>>), new System.Type[] {typeof (GLTranCode.tranCode), typeof (GLTranCode.module), typeof (GLTranCode.tranType), typeof (GLTranCode.descr)})]
  [PXUIField(DisplayName = "Tran. Code", Required = true)]
  public virtual string TranCode { get; set; }

  /// <summary>
  /// The module of the transaction or document that the system creates from this line of the batch.
  /// </summary>
  /// <value>
  /// The value of this field is defined by the <see cref="T:PX.Objects.GL.GLTranCode">Entry Code</see> specified in the <see cref="P:PX.Objects.GL.GLTranDoc.TranCode" /> field.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXDefault(typeof (Search<GLTranCode.module, Where<GLTranCode.tranCode, Equal<Current<GLTranDoc.tranCode>>>>))]
  [PXUIField]
  public virtual string TranModule { get; set; }

  /// <summary>
  /// The type of the transaction or document that the system creates from this line of the batch.
  /// </summary>
  /// <value>
  /// The value of this field is defined by the <see cref="T:PX.Objects.GL.GLTranCode">Entry Code</see> specified in the <see cref="P:PX.Objects.GL.GLTranDoc.TranCode" /> field.
  /// </value>
  [PXDBString(3, IsFixed = true)]
  [PXDefault(typeof (Search<GLTranCode.tranType, Where<GLTranCode.tranCode, Equal<Current<GLTranDoc.tranCode>>>>))]
  [PXUIField]
  public virtual string TranType { get; set; }

  /// <summary>
  /// The date of the transaction or document that the system creates from this line of the batch.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.GLDocBatch.DateEntered">date of the batch</see>.
  /// </value>
  [PXDBDate]
  [PXDefault(typeof (GLDocBatch.dateEntered))]
  [PXUIField(DisplayName = "Transaction Date")]
  public virtual DateTime? TranDate { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.AP.Vendor">Vendor</see> or <see cref="T:PX.Objects.AR.Customer">Customer</see>,
  /// for whom the system will create a document from this line.
  /// </summary>
  /// <value>
  /// The field corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field and is relevant only for the documents of AP and AR modules.
  /// </value>
  [PXDBInt]
  [PXVendorCustomerSelector(typeof (GLTranDoc.tranModule), typeof (GLTranDoc.curyID), CacheGlobal = true)]
  [PXUIField(DisplayName = "Customer/Vendor", Enabled = true, Visible = true)]
  public virtual int? BAccountID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CR.Location">Location</see> of the <see cref="P:PX.Objects.GL.GLTranDoc.BAccountID">Vendor or
  /// Customer</see> associated with this line of the batch.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Location.LocationID" /> field.
  /// Defaults to vendor's or customer's <see cref="!:BAccountR.DefLocationID">default location</see>.
  /// </value>
  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<GLTranDoc.bAccountID>>>), DisplayName = "Location", DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXDefault(typeof (Search<BAccountR.defLocationID, Where<BAccountR.bAccountID, Equal<Current<GLTranDoc.bAccountID>>>>))]
  public virtual int? LocationID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.PM.PMProject">Project</see> associated with the document or transaction that the system creates from the line,
  /// or the <see cref="P:PX.Objects.PM.PMSetup.NonProjectCode">non-project code</see> indicating that the document or transaction is not associated with any particular project.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMProject.ProjectID" /> field.
  /// </value>
  [ProjectDefault(null)]
  [ActiveProjectOrContractForGL]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.PM.PMTask">Task</see> associated with the document or transaction that the system creates from the line.
  /// The field is relevant only if the Projects module has been activated and integrated with the module of the document or transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMTask.TaskID" /> field.
  /// </value>
  [ActiveProjectTask(typeof (GLTranDoc.projectID), typeof (GLTranDoc.tranModule), NeedTaskValidationField = typeof (GLTranDoc.needTaskValidation), DisplayName = "Project Task")]
  public virtual int? TaskID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CA.CAEntryType">Entry Type</see> of the CA transaction
  /// that the system creates from this line of the batch.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:CAEntryType.EntryTypeID" /> field.
  /// This field is relevant only for the lines defining the transactions of the CA module.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXSelector(typeof (Search<CAEntryType.entryTypeId, Where<CAEntryType.module, Equal<Current<GLTranDoc.tranModule>>, And<CAEntryType.useToReclassifyPayments, NotEqual<True>>>>), DescriptionField = typeof (CAEntryType.descr))]
  [PXUIField(DisplayName = "Entry Type ID")]
  public virtual string EntryTypeID { get; set; }

  /// <summary>
  /// Indicates whether the CA transaction that the system will create from this line is Receipt or Disbursement.
  /// </summary>
  /// <value>
  /// The value of this field is determined by the <see cref="!:EntryType" /> of the line.
  /// Possible values are: <c>"D"</c> for Receipt and <c>"C"</c> for Disbursement.
  /// </value>
  [PXString(1, IsFixed = true)]
  [PXDefault(typeof (Search<CAEntryType.drCr, Where<Current<GLTranDoc.tranModule>, Equal<BatchModule.moduleCA>, And<CAEntryType.entryTypeId, Equal<Current<GLTranDoc.entryTypeID>>>>>))]
  [PXDBScalar(typeof (Search<CAEntryType.drCr, Where<CAEntryType.entryTypeId, Equal<GLTranDoc.entryTypeID>, And<GLTranDoc.tranModule, Equal<BatchModule.moduleCA>>>>))]
  public string CADrCr { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CA.PaymentMethod">Payment Method</see> used for the document created from the line.
  /// </summary>
  /// <value>
  /// This field is relevant only for the lines defining documents of AP and AR modules.
  /// Corresponds to the <see cref="P:PX.Objects.CA.PaymentMethod.PaymentMethodID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.isActive, Equal<True>, And<Where2<Where<Current<GLTranDoc.tranModule>, Equal<BatchModule.moduleAP>, And<PX.Objects.CA.PaymentMethod.useForAP, Equal<True>, And<Where2<Where<PX.Objects.CA.PaymentMethod.aPPrintChecks, Equal<False>, And<PX.Objects.CA.PaymentMethod.aPCreateBatchPayment, Equal<False>>>, Or<Current<GLTranDoc.tranType>, Equal<APDocType.invoice>, Or<Current<GLTranDoc.tranType>, Equal<APDocType.debitAdj>>>>>>>, Or<Where<Current<GLTranDoc.tranModule>, Equal<BatchModule.moduleAR>, And<PX.Objects.CA.PaymentMethod.useForAR, Equal<True>, And<Where<PX.Objects.CA.PaymentMethod.aRIsProcessingRequired, Equal<False>, Or<Current<GLTranDoc.tranType>, Equal<ARDocType.invoice>, Or<Current<GLTranDoc.tranType>, Equal<ARDocType.debitMemo>>>>>>>>>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  [PXUIField(DisplayName = "Payment Method", Visible = true)]
  public virtual string PaymentMethodID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.TX.TaxCategory">Tax Category</see> associated with the document or transaction defined by the line.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.TX.TaxCategory.TaxCategoryID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [GLTax(typeof (GLTranDoc), typeof (GLTax), typeof (GLTaxTran))]
  [PXDefault]
  public virtual string TaxCategoryID { get; set; }

  /// <summary>
  /// Identifier of the debit <see cref="T:PX.Objects.GL.Account" /> associated with the line of the batch.
  /// </summary>
  /// <value>
  /// For the lines defining GL transactions the value of this field is entered by user.
  /// For the lines used to create AP or AR documents the account is selected automatically once the <see cref="P:PX.Objects.GL.GLTranDoc.BAccountID">vendor or customer</see> is chosen.
  /// For CA transactions the account is either entered by user (for Receipts) or selected automatically
  /// based on the <see cref="P:PX.Objects.GL.GLTranDoc.EntryTypeID">Entry Type</see> (for Disbursements).
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(typeof (GLTranDoc.branchID), typeof (Search2<Account.accountID, LeftJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.accountID, Equal<Account.accountID>, And<PX.Objects.CA.CashAccount.curyID, Equal<Optional<GLTranDoc.curyID>>, And2<Match<Current<AccessInfo.userName>>, And<Where<PX.Objects.CA.CashAccount.branchID, Equal<Current<GLTranDoc.branchID>>, Or<PX.Objects.CA.CashAccount.restrictVisibilityWithBranch, NotEqual<True>>>>>>>, LeftJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<Optional<GLTranDoc.paymentMethodID>>>>, LeftJoin<CAEntryType, On<CAEntryType.entryTypeId, Equal<Optional<GLTranDoc.entryTypeID>>>, LeftJoin<CashAccountETDetail, On<CashAccountETDetail.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>>>>>, Where2<Where<Optional<GLTranDoc.isARCustomerCashAccount>, NotEqual<True>, And<Where2<Where<Optional<GLTranDoc.tranModule>, Equal<BatchModule.moduleCA>, And<CAEntryType.entryTypeId, IsNotNull, And<Where<CAEntryType.drCr, Equal<PX.Objects.CA.CADrCr.cACredit>, Or<CashAccountETDetail.cashAccountID, IsNotNull>>>>>, Or<Where<Optional<GLTranDoc.tranModule>, NotEqual<BatchModule.moduleCA>, And<Where<Optional<GLTranDoc.needsDebitCashAccount>, Equal<False>, Or<PX.Objects.CA.CashAccount.accountID, IsNotNull>>>>>>>>, Or<Where<Optional<GLTranDoc.tranModule>, Equal<BatchModule.moduleAR>, And<PaymentMethodAccount.useForAR, Equal<True>, And<Optional<GLTranDoc.paymentMethodID>, IsNotNull, And<Optional<GLTranDoc.isARCustomerCashAccount>, Equal<True>>>>>>>>), LedgerID = typeof (GLTranDoc.ledgerID), DescriptionField = typeof (Account.description), DisplayName = "Debit Account")]
  [PXDefault]
  public virtual int? DebitAccountID { get; set; }

  /// <summary>
  /// Identifier of the debit <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the line of the batch.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// For the information related to defaulting of this field see <see cref="P:PX.Objects.GL.GLTranDoc.DebitAccountID" />.
  /// </value>
  [SubAccount(typeof (GLTranDoc.debitAccountID), typeof (GLTranDoc.branchID), false, DisplayName = "Debit Subaccount")]
  [PXDefault]
  public virtual int? DebitSubID { get; set; }

  /// <summary>
  /// Identifier of the credit <see cref="T:PX.Objects.GL.Account" /> associated with the line of the batch.
  /// </summary>
  /// <value>
  /// For the lines defining GL transactions the value of this fields is entered by user.
  /// For the lines used to create AP or AR documents the account is selected automatically once the <see cref="P:PX.Objects.GL.GLTranDoc.BAccountID">vendor or customer</see> is chosen.
  /// For CA transactions the account is either entered by user (for Disbursements) or selected automatically
  /// based on the <see cref="P:PX.Objects.GL.GLTranDoc.EntryTypeID">Entry Type</see> (for Receipts).
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(typeof (GLTranDoc.branchID), typeof (Search2<Account.accountID, LeftJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.accountID, Equal<Account.accountID>, And<PX.Objects.CA.CashAccount.branchID, Equal<Current<GLTranDoc.branchID>>, And<PX.Objects.CA.CashAccount.curyID, Equal<Optional<GLTranDoc.curyID>>>>>, LeftJoin<CAEntryType, On<CAEntryType.entryTypeId, Equal<Optional<GLTranDoc.entryTypeID>>>, LeftJoin<CashAccountETDetail, On<CashAccountETDetail.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>>>>, Where<Where2<Where<Optional<GLTranDoc.tranModule>, Equal<BatchModule.moduleCA>, And<CAEntryType.entryTypeId, IsNotNull, And<Where<CAEntryType.drCr, Equal<PX.Objects.CA.CADrCr.cADebit>, Or<CashAccountETDetail.cashAccountID, IsNotNull>>>>>, Or<Where<Optional<GLTranDoc.tranModule>, NotEqual<BatchModule.moduleCA>, And<Where<Optional<GLTranDoc.needsCreditCashAccount>, Equal<False>, Or<PX.Objects.CA.CashAccount.accountID, IsNotNull>>>>>>>>), LedgerID = typeof (GLTranDoc.ledgerID), DescriptionField = typeof (Account.description), DisplayName = "Credit Account")]
  [PXDefault]
  public virtual int? CreditAccountID { get; set; }

  /// <summary>
  /// Identifier of the credit <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the line of the batch.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// For the information related to defaulting of this field see <see cref="P:PX.Objects.GL.GLTranDoc.CreditAccountID" />.
  /// </value>
  [SubAccount(typeof (GLTranDoc.creditAccountID), typeof (GLTranDoc.branchID), false, DisplayName = "Credit Subaccount")]
  [PXDefault]
  public virtual int? CreditSubID { get; set; }

  /// <summary>
  /// Reference number of the document or transaction created from the line.
  /// </summary>
  /// <value>
  /// For the lines defining GL transactions the field corresponds to the <see cref="P:PX.Objects.GL.Batch.BatchNbr" /> field.
  /// For the lines used to create documents in AP and AR modules the field corresponds to the
  /// <see cref="P:PX.Objects.AP.APRegister.RefNbr" /> and <see cref="P:PX.Objects.AR.ARRegister.RefNbr" /> fields, respectively.
  /// For the lines defining CA transactions the field corresponds to the <see cref="P:PX.Objects.CA.CAAdj.AdjRefNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Ref. Number", Visible = true)]
  public virtual string RefNbr { get; set; }

  /// <summary>
  /// When <c>true</c>, indicates that the document or transaction defined by the line has been created.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Doc. Created", Enabled = false)]
  public virtual bool? DocCreated { get; set; }

  /// <summary>
  /// The reference number of the original vendor or customer document.
  /// </summary>
  [PXDBString(40, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string ExtRefNbr { get; set; }

  /// <summary>
  /// The sum of the document or transaction details or lines. Does not include tax amount.
  /// For the batch lines representing details of the document to be created (see <see cref="P:PX.Objects.GL.GLTranDoc.Split" /> field),
  /// the value of this field defines the line amount.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// See also <see cref="P:PX.Objects.GL.GLTranDoc.CuryTranAmt" />.
  /// </summary>
  [PXDBBaseCury(typeof (GLTranDoc.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranAmt { get; set; }

  /// <summary>
  /// The amount of the document or transaction, including the total tax amount.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// See also <see cref="!:CuryTotalAmt" />.
  /// </summary>
  [PXDBBaseCury(typeof (GLTranDoc.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranTotal { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the line.
  /// </summary>
  /// <value>
  /// Aut-generated. Corresponds to the <see cref="!:PX.Objects.CM.CurrencyInfo.CurrencyInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo(typeof (GLDocBatch.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>
  /// The amount of the document or transaction, including the total tax amount.
  /// Given in the <see cref="P:PX.Objects.GL.GLTranDoc.CuryID">currency</see> of the line.
  /// See also <see cref="!:TotalAmt" />.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXDBCurrency(typeof (GLTranDoc.curyInfoID), typeof (GLTranDoc.tranTotal))]
  public virtual Decimal? CuryTranTotal { get; set; }

  /// <summary>
  /// The sum of the document or transaction details or lines. Does not include tax amount.
  /// For the batch lines representing details of the document to be created (see <see cref="P:PX.Objects.GL.GLTranDoc.Split" /> field),
  /// the value of this field defines the line amount.
  /// Given in the <see cref="P:PX.Objects.GL.GLTranDoc.CuryID">currency</see> of the line.
  /// See also <see cref="P:PX.Objects.GL.GLTranDoc.TranAmt" />.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXDBCurrency(typeof (GLTranDoc.curyInfoID), typeof (GLTranDoc.tranAmt))]
  public virtual Decimal? CuryTranAmt { get; set; }

  /// <summary>
  /// Indicates whether the document defined by the line has been released.
  /// The system releases the documents once they have been generated.
  /// </summary>
  /// <value>
  /// <c>true</c> if released, otherwise <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released", Enabled = false)]
  public virtual bool? Released { get; set; }

  /// <summary>
  /// Reserved for internal use.
  /// The class of the document or transaction defined by the line.
  /// This field affects posting of documents and transactions to GL.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  public virtual string TranClass { get; set; }

  /// <summary>
  /// The description of the document or transaction defined by the line.
  /// </summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string TranDesc { get; set; }

  /// <summary>Reserved for internal use.</summary>
  [PXDBInt]
  public virtual int? TranLineNbr { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.AR.CustomerPaymentMethod">Customer Payment Method</see> (card or account number) associated with the line.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.CustomerPaymentMethod.PMInstanceID" /> field.
  /// Relevant only for the lines representing AR documents.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Card/Account Nbr.")]
  [PXSelector(typeof (Search<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current<GLTranDoc.bAccountID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current<GLTranDoc.paymentMethodID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>>>>>), DescriptionField = typeof (PX.Objects.AR.CustomerPaymentMethod.descr))]
  [PXDefault]
  public virtual int? PMInstanceID { get; set; }

  /// <summary>
  /// <see cref="T:PX.Objects.GL.FinPeriods.OrganizationFinPeriod">Financial Period</see> of the document or transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.GLDocBatch.TranPeriodID">period of the batch</see>,
  /// which is defined by its <see cref="P:PX.Objects.GL.GLDocBatch.DateEntered">date</see> and can't be changed by user.
  /// </value>
  [PeriodID(null, null, null, true)]
  [PXDBDefault(typeof (GLDocBatch.tranPeriodID))]
  public virtual string TranPeriodID { get; set; }

  /// <summary>
  /// <see cref="T:PX.Objects.GL.FinPeriods.OrganizationFinPeriod">Financial Period</see> of the document or transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.GLDocBatch.FinPeriodID">period of the batch</see>,
  /// which is defined by its <see cref="P:PX.Objects.GL.GLDocBatch.DateEntered">date</see>, but can be overriden by user.
  /// </value>
  [CAAPAROpenPeriod(typeof (GLTranDoc.tranModule), typeof (GLTranDoc.tranDate), typeof (GLTranDoc.branchID), null, null, null, null, true, typeof (GLTranDoc.tranPeriodID), false)]
  [PXDBDefault(typeof (GLDocBatch.finPeriodID))]
  public virtual string FinPeriodID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.PM.PMTran">Project Transactions</see> assoicated with the document or transactions, represented by the line.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMTran.TranID" /> field.
  /// </value>
  [PXDBChildIdentity(typeof (PMTran.tranID))]
  [PXDBLong]
  public virtual long? PMTranID { get; set; }

  /// <summary>
  /// The type of balance of the <see cref="T:PX.Objects.GL.Ledger" />, associated with the line.
  /// </summary>
  /// <value>
  /// Possible values are:
  /// <c>"A"</c> - Actual,
  /// <c>"R"</c> - Reporting,
  /// <c>"S"</c> - Statistical,
  /// <c>"B"</c> - Budget.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Ledger.BalanceType" /> field.
  /// </value>
  [PXString(1, IsFixed = true, InputMask = "")]
  public virtual string LedgerBalanceType { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CS.Terms">Credit Terms</see> record associated with the line.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.Terms.TermsID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>, Or2<Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.customer>, And<Current<GLTranDoc.tranModule>, Equal<BatchModule.moduleAR>>>, Or<Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.vendor>, And<Current<GLTranDoc.tranModule>, Equal<BatchModule.moduleAP>>>>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
  [Terms(typeof (GLTranDoc.tranDate), typeof (GLTranDoc.dueDate), typeof (GLTranDoc.discDate), typeof (GLTranDoc.curyTranTotal), typeof (GLTranDoc.curyDiscAmt), typeof (GLTranDoc.curyTaxTotal), typeof (GLTranDoc.branchID))]
  public virtual string TermsID { get; set; }

  /// <summary>
  /// The due date of the document defined by the line, if applicable.
  /// </summary>
  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? DueDate { get; set; }

  /// <summary>
  /// The date of the cash discount for the document, if applicable.
  /// </summary>
  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? DiscDate { get; set; }

  /// <summary>
  /// The amount of the cash discount for the document (if applicable),
  /// given in the <see cref="P:PX.Objects.GL.GLTranDoc.CuryID">currency</see> of the line.
  /// See also <see cref="P:PX.Objects.GL.GLTranDoc.DiscAmt" />.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (GLTranDoc.curyInfoID), typeof (GLTranDoc.discAmt))]
  [PXUIField]
  public virtual Decimal? CuryDiscAmt { get; set; }

  /// <summary>
  /// The amount of the cash discount for the document (if applicable),
  /// given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// See also <see cref="P:PX.Objects.GL.GLTranDoc.DiscAmt" />.
  /// </summary>
  [PXDBBaseCury(null, null)]
  public virtual Decimal? DiscAmt { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

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

  /// <summary>
  /// The open balance of the parent document (represented by another line of the batch) for the moment when
  /// the child line is inserted.
  /// This field is used to determine the default value of the <see cref="P:PX.Objects.GL.GLTranDoc.CuryTranAmt" /> for the lines,
  /// representing details of a document to be created from another line of the batch.
  /// Given in the <see cref="P:PX.Objects.GL.GLTranDoc.CuryID">currency</see> of the line.
  /// </summary>
  [PXDecimal(4)]
  [PXUIField]
  public virtual Decimal? CuryBalanceAmt { get; set; }

  /// <summary>Read-only identifier of the group of batch lines.</summary>
  /// <value>
  /// Batch lines are grouped by document or transaction, so that for the lines describing different documents
  /// the values of this field are different, and for the lines defining header or lines of the same document
  /// (see the <see cref="P:PX.Objects.GL.GLTranDoc.Split" /> field) the GroupTranID value is the same.
  /// Depends only on the <see cref="P:PX.Objects.GL.GLTranDoc.LineNbr" /> and <see cref="P:PX.Objects.GL.GLTranDoc.ParentLineNbr" /> fields.
  /// </value>
  [PXInt]
  [PXUIField(FieldName = "Group Tran ID", Visible = false, Enabled = false)]
  public virtual int? GroupTranID
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranDoc.parentLineNbr), typeof (GLTranDoc.lineNbr)})] get
    {
      int? nullable = this.ParentLineNbr.HasValue ? new int?(this.ParentLineNbr.Value) : this.LineNbr;
      return !nullable.HasValue ? new int?() : new int?(nullable.GetValueOrDefault() * 10000);
    }
    set
    {
    }
  }

  /// <summary>
  /// Read-only identifier of the <see cref="T:PX.Objects.CA.CashAccount">Cash Account</see> associated with the document or transaction defined by the line.
  /// </summary>
  /// <value>
  /// Depending on the <see cref="P:PX.Objects.GL.GLTranDoc.TranModule" /> and <see cref="P:PX.Objects.GL.GLTranDoc.TranType" /> returns either <see cref="P:PX.Objects.GL.GLTranDoc.DebitCashAccountID" /> or <see cref="P:PX.Objects.GL.GLTranDoc.CreditCashAccountID" />.
  /// </value>
  [PXInt]
  public virtual int? CashAccountID
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranDoc.tranModule), typeof (GLTranDoc.tranType), typeof (GLTranDoc.creditCashAccountID), typeof (GLTranDoc.debitCashAccountID), typeof (GLTranDoc.cADrCr)})] get
    {
      int? cashAccountId = new int?();
      if (this.TranModule == "AP")
      {
        if (this.TranType == "CHK" || this.TranType == "PPM" || this.TranType == "QCK")
          cashAccountId = this.CreditCashAccountID;
        if (this.TranType == "REF" || this.TranType == "VCK" || this.TranType == "VQC")
          cashAccountId = this.DebitCashAccountID;
      }
      if (this.TranModule == "AR")
      {
        if (this.TranType == "PMT" || this.TranType == "PPM" || this.TranType == "CSL")
          cashAccountId = this.DebitCashAccountID;
        if (this.TranType == "REF" || this.TranType == "RPM" || this.TranType == "RCS")
          cashAccountId = this.CreditCashAccountID;
      }
      if (this.TranModule == "CA" && !this.IsChildTran && !string.IsNullOrEmpty(this.CADrCr))
        cashAccountId = this.CADrCr == "D" ? this.DebitCashAccountID : this.CreditCashAccountID;
      return cashAccountId;
    }
    set
    {
    }
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.TX.TaxZone">Tax Zone</see> associated with the document, if applicable.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.TX.TaxZone.TaxZoneID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  public virtual string TaxZoneID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.TX.Tax" /> associated with the document, if applicable.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.TX.Tax.TaxID" /> field.
  /// </value>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax ID", Visible = false)]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr))]
  public virtual string TaxID { get; set; }

  /// <summary>The tax rate for the document, if applicable.</summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxRate { get; set; }

  /// <summary>
  /// The amount that is subjected to the tax for the line.
  /// Given in the <see cref="P:PX.Objects.GL.GLTranDoc.CuryID">currency</see> of the line.
  /// See also <see cref="P:PX.Objects.GL.GLTranDoc.TaxableAmt" />.
  /// </summary>
  [PXDBCurrency(typeof (GLTranDoc.curyInfoID), typeof (GLTranDoc.taxableAmt))]
  [PXUIField]
  public virtual Decimal? CuryTaxableAmt { get; set; }

  /// <summary>
  /// The amount that is subjected to the tax for the line.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// See also <see cref="P:PX.Objects.GL.GLTranDoc.CuryTaxableAmt" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXUIField]
  public virtual Decimal? TaxableAmt { get; set; }

  /// <summary>
  /// The resulting amount of the tax associated with the line.
  /// Given in the <see cref="P:PX.Objects.GL.GLTranDoc.CuryID">currency</see> of the line.
  /// See also <see cref="P:PX.Objects.GL.GLTranDoc.TaxAmt" />.
  /// </summary>
  [PXDBCurrency(typeof (GLTranDoc.curyInfoID), typeof (GLTranDoc.taxAmt))]
  [PXUIField]
  public virtual Decimal? CuryTaxAmt { get; set; }

  /// <summary>
  /// The resulting amount of the tax associated with the line.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// See also <see cref="P:PX.Objects.GL.GLTranDoc.CuryTaxAmt" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXUIField]
  public virtual Decimal? TaxAmt { get; set; }

  /// <summary>
  /// The amount of tax that is included in the <see cref="P:PX.Objects.GL.GLTranDoc.CuryTranTotal">document total</see>.
  /// Given in the <see cref="P:PX.Objects.GL.GLTranDoc.CuryID">currency</see> of the line.
  /// See also <see cref="P:PX.Objects.GL.GLTranDoc.InclTaxAmt" />.
  /// </summary>
  [PXDBCurrency(typeof (GLTranDoc.curyInfoID), typeof (GLTranDoc.inclTaxAmt))]
  [PXUIField]
  public virtual Decimal? CuryInclTaxAmt { get; set; }

  /// <summary>
  /// The amount of tax that is included in the <see cref="P:PX.Objects.GL.GLTranDoc.TranTotal">document total</see>.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// See also <see cref="P:PX.Objects.GL.GLTranDoc.CuryInclTaxAmt" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXUIField]
  public virtual Decimal? InclTaxAmt { get; set; }

  /// <summary>
  /// The tax amount withheld on the document.
  /// Given in the <see cref="P:PX.Objects.GL.GLTranDoc.CuryID">currency</see> of the line.
  /// See also <see cref="P:PX.Objects.GL.GLTranDoc.OrigWhTaxAmt" />.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (GLTranDoc.curyInfoID), typeof (GLTranDoc.origWhTaxAmt))]
  [PXUIField]
  public virtual Decimal? CuryOrigWhTaxAmt { get; set; }

  /// <summary>
  /// The tax amount withheld on the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// See also <see cref="P:PX.Objects.GL.GLTranDoc.OrigWhTaxAmt" />.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigWhTaxAmt { get; set; }

  /// <summary>
  /// Read-only document total.
  /// Given in the <see cref="P:PX.Objects.GL.GLTranDoc.CuryID">currency</see> of the line.
  /// </summary>
  /// <value>
  /// For the lines representing document headers gives the total amount of the document, including taxes amount.
  /// For the lines representing details of a document returns <c>null</c>.
  /// Calculated from <see cref="P:PX.Objects.GL.GLTranDoc.CuryTaxAmt" />, <see cref="P:PX.Objects.GL.GLTranDoc.CuryTranAmt" /> and <see cref="P:PX.Objects.GL.GLTranDoc.CuryInclTaxAmt" /> fields.
  /// </value>
  [PXCurrency(typeof (GLTranDoc.curyInfoID), typeof (GLTranDoc.docTotal))]
  [PXUIField]
  public virtual Decimal? CuryDocTotal
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranDoc.curyTaxAmt), typeof (GLTranDoc.curyTranAmt), typeof (GLTranDoc.curyInclTaxAmt)})] get
    {
      if (this.IsChildTran)
        return new Decimal?();
      Decimal? curyDocTotal = this.CuryTranAmt;
      Decimal? nullable1 = this.CuryTaxAmt;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      Decimal? nullable2;
      if (!curyDocTotal.HasValue)
      {
        nullable1 = new Decimal?();
        nullable2 = nullable1;
      }
      else
        nullable2 = new Decimal?(curyDocTotal.GetValueOrDefault() + valueOrDefault1);
      Decimal? nullable3 = nullable2;
      curyDocTotal = this.CuryInclTaxAmt;
      Decimal valueOrDefault2 = curyDocTotal.GetValueOrDefault();
      if (nullable3.HasValue)
        return new Decimal?(nullable3.GetValueOrDefault() - valueOrDefault2);
      curyDocTotal = new Decimal?();
      return curyDocTotal;
    }
    set
    {
    }
  }

  /// <summary>
  /// Read-only document total.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// </summary>
  /// <value>
  /// For the lines representing document headers gives the total amount of the document, including taxes amount.
  /// For the lines representing details of a document returns <c>null</c>.
  /// Calculated from <see cref="P:PX.Objects.GL.GLTranDoc.TaxAmt" />, <see cref="P:PX.Objects.GL.GLTranDoc.TranAmt" /> and <see cref="P:PX.Objects.GL.GLTranDoc.InclTaxAmt" /> fields.
  /// </value>
  public virtual Decimal? DocTotal
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranDoc.taxAmt), typeof (GLTranDoc.tranAmt), typeof (GLTranDoc.inclTaxAmt)})] get
    {
      if (this.IsChildTran)
        return new Decimal?();
      Decimal? tranAmt = this.TranAmt;
      Decimal? docTotal = this.TaxAmt;
      Decimal? nullable = tranAmt.HasValue & docTotal.HasValue ? new Decimal?(tranAmt.GetValueOrDefault() + docTotal.GetValueOrDefault()) : new Decimal?();
      Decimal? inclTaxAmt = this.InclTaxAmt;
      if (nullable.HasValue & inclTaxAmt.HasValue)
        return new Decimal?(nullable.GetValueOrDefault() - inclTaxAmt.GetValueOrDefault());
      docTotal = new Decimal?();
      return docTotal;
    }
    set
    {
    }
  }

  /// <summary>
  /// Read-only total amount of tax associated with the document or transaction.
  /// Given in the <see cref="P:PX.Objects.GL.GLTranDoc.CuryID">currency</see> of the line.
  /// </summary>
  /// <value>
  /// Calculated from the <see cref="P:PX.Objects.GL.GLTranDoc.CuryTaxAmt" /> and <see cref="P:PX.Objects.GL.GLTranDoc.CuryInclTaxAmt" /> fields.
  /// </value>
  [PXCury(typeof (GLTranDoc.curyID))]
  [PXUIField]
  public virtual Decimal? CuryTaxTotal
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranDoc.curyTaxAmt), typeof (GLTranDoc.curyInclTaxAmt)})] get
    {
      Decimal? curyTaxAmt = this.CuryTaxAmt;
      Decimal? curyInclTaxAmt = this.CuryInclTaxAmt;
      return !(curyTaxAmt.HasValue & curyInclTaxAmt.HasValue) ? new Decimal?() : new Decimal?(curyTaxAmt.GetValueOrDefault() - curyInclTaxAmt.GetValueOrDefault());
    }
    set
    {
    }
  }

  /// <summary>
  /// Read-only total amount of tax associated with the document or transaction.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// </summary>
  /// <value>
  /// Calculated from the <see cref="P:PX.Objects.GL.GLTranDoc.TaxAmt" /> and <see cref="P:PX.Objects.GL.GLTranDoc.InclTaxAmt" /> fields.
  /// </value>
  public virtual Decimal? TaxTotal
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranDoc.taxAmt), typeof (GLTranDoc.inclTaxAmt)})] get
    {
      Decimal? taxAmt = this.TaxAmt;
      Decimal? inclTaxAmt = this.InclTaxAmt;
      return !(taxAmt.HasValue & inclTaxAmt.HasValue) ? new Decimal?() : new Decimal?(taxAmt.GetValueOrDefault() - inclTaxAmt.GetValueOrDefault());
    }
    set
    {
    }
  }

  /// <summary>
  /// The amount of the application.
  /// Given in the <see cref="P:PX.Objects.GL.GLTranDoc.CuryID">currency</see> of the line.
  /// See also <see cref="P:PX.Objects.GL.GLTranDoc.ApplAmt" />.
  /// </summary>
  /// <value>
  /// The value of this field should be set if a line describes an <see cref="T:PX.Objects.AP.APPayment">APPayment</see> or
  /// an <see cref="T:PX.Objects.AR.ARPayment">ARPayment</see>, which is applied to existing documents.
  /// The Journal Vouchers (GL304000) form allows you to enter application information through the AP Payment Applications
  /// and AR Payment Applications tabs.
  /// </value>
  [PXDBCurrency(typeof (GLTranDoc.curyInfoID), typeof (GLTranDoc.applAmt))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryApplAmt { get; set; }

  /// <summary>
  /// The amount of the application.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// For more infor see <see cref="P:PX.Objects.GL.GLTranDoc.CuryApplAmt" />.
  /// </summary>
  [PXDBDecimal(4)]
  public virtual Decimal? ApplAmt { get; set; }

  /// <summary>
  /// The amount of the cash discount taken on the document.
  /// Given in the <see cref="P:PX.Objects.GL.GLTranDoc.CuryID">currency</see> of the line.
  /// See also <see cref="P:PX.Objects.GL.GLTranDoc.DiscTaken" />.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (GLTranDoc.curyInfoID), typeof (GLTranDoc.discTaken))]
  public virtual Decimal? CuryDiscTaken { get; set; }

  /// <summary>
  /// The amount of the cash discount taken on the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// See also <see cref="P:PX.Objects.GL.GLTranDoc.CuryDiscTaken" />.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscTaken { get; set; }

  /// <summary>
  /// The actual amount of tax withheld on the applications of the document.
  /// Given in the <see cref="P:PX.Objects.GL.GLTranDoc.CuryID">currency</see> of the line.
  /// See also <see cref="P:PX.Objects.GL.GLTranDoc.TaxWheld" />.
  /// </summary>
  /// <value>
  /// The value of this field is calculated from the values of the <see cref="P:PX.Objects.AP.APAdjust.CuryAdjdWhTaxAmt">APAdjust.CuryAdjdWhTaxAmt</see> or
  /// <see cref="P:PX.Objects.AR.ARAdjust.CuryAdjdWhTaxAmt">ARAdjust.CuryAdjdWhTaxAmt</see> fields of the applications assoicated with the line.
  /// </value>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (GLTranDoc.curyInfoID), typeof (GLTranDoc.taxWheld))]
  public virtual Decimal? CuryTaxWheld { get; set; }

  /// <summary>
  /// The actual amount of tax withheld on the applications of the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// See also <see cref="P:PX.Objects.GL.GLTranDoc.CuryTaxWheld" />.
  /// </summary>
  /// <value>
  /// The value of this field is calculated from the values of the <see cref="P:PX.Objects.AP.APAdjust.CuryAdjdWhTaxAmt">APAdjust.CuryAdjdWhTaxAmt</see> or
  /// <see cref="P:PX.Objects.AR.ARAdjust.CuryAdjdWhTaxAmt">ARAdjust.CuryAdjdWhTaxAmt</see> fields of the applications assoicated with the line.
  /// </value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxWheld { get; set; }

  /// <summary>
  /// The number of applications specified for the document.
  /// The applications are represented by <see cref="T:PX.Objects.AP.APAdjust">APAdjust</see> or
  /// <see cref="T:PX.Objects.AR.ARAdjust">ARAdjust</see> records.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? ApplCount { get; set; }

  /// <summary>
  /// Read-only unapplied balance of the document.
  /// Given in the <see cref="P:PX.Objects.GL.GLTranDoc.CuryID">currency</see> of the line.
  /// See also <see cref="!:UnappliedBalance" />.
  /// </summary>
  /// <value>
  /// The value of this field is calculated from the <see cref="P:PX.Objects.GL.GLTranDoc.CuryTranTotal" /> and <see cref="P:PX.Objects.GL.GLTranDoc.CuryApplAmt" /> fields.
  /// </value>
  [PXCurrency(typeof (GLTranDoc.curyInfoID), typeof (GLTranDoc.unappliedBal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnappliedBal
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranDoc.curyTranTotal), typeof (GLTranDoc.curyApplAmt)})] get
    {
      Decimal? nullable = this.CuryTranTotal;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryApplAmt;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
    set
    {
    }
  }

  /// <summary>
  /// Read-only unapplied balance of the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// See also <see cref="!:CuryUnappliedBalance" />.
  /// </summary>
  /// <value>
  /// The value of this field is calculated from the <see cref="P:PX.Objects.GL.GLTranDoc.TranTotal" /> and <see cref="P:PX.Objects.GL.GLTranDoc.ApplAmt" /> fields.
  /// </value>
  [PXDecimal(4)]
  public virtual Decimal? UnappliedBal
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranDoc.tranTotal), typeof (GLTranDoc.applAmt)})] get
    {
      Decimal? nullable = this.TranTotal;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.ApplAmt;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
    set
    {
    }
  }

  /// <summary>
  /// The read-only discount balance of the document - the amount of the cash discount that has not been used.
  /// Given in the <see cref="P:PX.Objects.GL.GLTranDoc.CuryID">currency</see> of the line.
  /// See also <see cref="P:PX.Objects.GL.GLTranDoc.DiscBal" />.
  /// </summary>
  /// <value>
  /// The value of this field is calculated from the <see cref="P:PX.Objects.GL.GLTranDoc.CuryDiscAmt" /> and <see cref="P:PX.Objects.GL.GLTranDoc.CuryDiscTaken" /> fields.
  /// </value>
  [PXCury(typeof (GLTranDoc.curyID))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscBal
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranDoc.curyDiscAmt), typeof (GLTranDoc.curyDiscTaken)})] get
    {
      Decimal? nullable = this.CuryDiscAmt;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryDiscTaken;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
    set
    {
    }
  }

  /// <summary>
  /// The read-only discount balance of the document - the amount of the cash discount that has not been used.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// See also <see cref="P:PX.Objects.GL.GLTranDoc.CuryDiscBal" />.
  /// </summary>
  /// <value>
  /// The value of this field is calculated from the <see cref="P:PX.Objects.GL.GLTranDoc.DiscAmt" /> and <see cref="P:PX.Objects.GL.GLTranDoc.DiscTaken" /> fields.
  /// </value>
  [PXDecimal(4)]
  public virtual Decimal? DiscBal
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranDoc.discAmt), typeof (GLTranDoc.discTaken)})] get
    {
      Decimal? nullable = this.DiscAmt;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.DiscTaken;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
    set
    {
    }
  }

  /// <summary>
  /// The read-only balance of the tax withheld on the document.
  /// Given in the <see cref="P:PX.Objects.GL.GLTranDoc.CuryID">currency</see> of the line.
  /// See also the <see cref="P:PX.Objects.GL.GLTranDoc.WhTaxBal" /> field.
  /// </summary>
  /// <value>
  /// The value of this field is calculated from the <see cref="P:PX.Objects.GL.GLTranDoc.CuryOrigWhTaxAmt" /> and <see cref="P:PX.Objects.GL.GLTranDoc.CuryTaxWheld" /> fields.
  /// </value>
  [PXCury(typeof (GLTranDoc.curyID))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryWhTaxBal
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranDoc.curyOrigWhTaxAmt), typeof (GLTranDoc.curyTaxWheld)})] get
    {
      Decimal? nullable = this.CuryOrigWhTaxAmt;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryTaxWheld;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
    set
    {
    }
  }

  /// <summary>
  /// The read-only balance of the tax withheld on the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// See also the <see cref="P:PX.Objects.GL.GLTranDoc.CuryWhTaxBal" /> field.
  /// </summary>
  /// <value>
  /// The value of this field is calculated from the <see cref="P:PX.Objects.GL.GLTranDoc.OrigWhTaxAmt" /> and <see cref="P:PX.Objects.GL.GLTranDoc.TaxWheld" /> fields.
  /// </value>
  [PXDecimal(4)]
  public virtual Decimal? WhTaxBal
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranDoc.origWhTaxAmt), typeof (GLTranDoc.taxWheld)})] get
    {
      Decimal? nullable = this.OrigWhTaxAmt;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.TaxWheld;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
    set
    {
    }
  }

  /// <summary>
  /// The read-only field, indicating whether the <see cref="!:DebitCashAccount">Debit Cash Account</see>
  /// is required to define the document described by the batch line.
  /// </summary>
  /// <value>
  /// The value of this field is determined by the <see cref="P:PX.Objects.GL.GLTranDoc.TranModule" />, <see cref="P:PX.Objects.GL.GLTranDoc.TranType" />,
  /// <see cref="P:PX.Objects.GL.GLTranDoc.EntryTypeID" /> and <see cref="P:PX.Objects.GL.GLTranDoc.CADrCr" /> fields.
  /// </value>
  [PXBool]
  [PXDefault(false)]
  public virtual bool? NeedsDebitCashAccount
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranDoc.tranModule), typeof (GLTranDoc.tranType), typeof (GLTranDoc.entryTypeID), typeof (GLTranDoc.cADrCr)})] get
    {
      bool? debitCashAccount = new bool?(false);
      if (this.TranModule == "AP" && (this.TranType == "REF" || this.TranType == "VCK" || this.TranType == "VQC"))
        debitCashAccount = new bool?(true);
      if (this.TranModule == "AR" && (this.TranType == "PMT" || this.TranType == "PPM" || this.TranType == "CSL"))
        debitCashAccount = new bool?(true);
      if (this.TranModule == "CA" && !string.IsNullOrEmpty(this.EntryTypeID))
        debitCashAccount = new bool?(this.CADrCr == "D");
      return debitCashAccount;
    }
    set
    {
    }
  }

  /// <summary>
  /// The read-only virtual field, indicating whether the <see cref="!:DebitCashAccount">Debit Cash Account</see>
  /// is used in AR documents by customers.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  public virtual bool? IsARCustomerCashAccount
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranDoc.tranModule), typeof (GLTranDoc.tranType)})] get
    {
      bool? customerCashAccount = new bool?(false);
      if (this.TranModule == "AR" && (this.TranType == "PMT" || this.TranType == "PPM" || this.TranType == "CSL"))
        customerCashAccount = new bool?(true);
      return customerCashAccount;
    }
    set
    {
    }
  }

  /// <summary>
  /// The read-only field, indicating whether the <see cref="!:CreditCashAccount">Credit Cash Account</see>
  /// is required to define the document described by the batch line.
  /// </summary>
  /// <value>
  /// The value of this field is determined by the <see cref="P:PX.Objects.GL.GLTranDoc.TranModule" />, <see cref="P:PX.Objects.GL.GLTranDoc.TranType" />,
  /// <see cref="P:PX.Objects.GL.GLTranDoc.EntryTypeID" /> and <see cref="P:PX.Objects.GL.GLTranDoc.CADrCr" /> fields.
  /// </value>
  [PXBool]
  [PXDefault(false)]
  public virtual bool? NeedsCreditCashAccount
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranDoc.tranModule), typeof (GLTranDoc.tranType), typeof (GLTranDoc.entryTypeID), typeof (GLTranDoc.cADrCr)})] get
    {
      bool? creditCashAccount = new bool?(false);
      if (this.TranModule == "AP" && (this.TranType == "CHK" || this.TranType == "PPM" || this.TranType == "QCK"))
        creditCashAccount = new bool?(true);
      if (this.TranModule == "AR" && (this.TranType == "REF" || this.TranType == "RPM" || this.TranType == "RCS"))
        creditCashAccount = new bool?(true);
      if (this.TranModule == "CA" && !string.IsNullOrEmpty(this.EntryTypeID))
        creditCashAccount = new bool?(this.CADrCr == "C");
      return creditCashAccount;
    }
    set
    {
    }
  }

  /// <summary>
  /// Indicates whether validation for the presence of the correct <see cref="P:PX.Objects.GL.GLTranDoc.TaskID" /> must be performed for the line before it is persisted to the database.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  public virtual bool? NeedTaskValidation
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranDoc.tranModule), typeof (GLTranDoc.split), typeof (GLTranDoc.parentLineNbr)})] get
    {
      return this.Split.GetValueOrDefault() && !this.IsChildTran ? new bool?(false) : new bool?(true);
    }
    set
    {
    }
  }

  internal bool IsBalanced
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranDoc.creditAccountID), typeof (GLTranDoc.debitAccountID)})] get
    {
      return this.CreditAccountID.HasValue && this.DebitAccountID.HasValue;
    }
  }

  internal bool IsChildTran
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranDoc.parentLineNbr)})] get
    {
      return this.ParentLineNbr.HasValue;
    }
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CA.CashAccount">Debit Cash Account</see> associated with the document.
  /// Debit Cash Account is required for some types of the documents, that can be included in a <see cref="T:PX.Objects.GL.GLDocBatch">document batch</see>
  /// - see <see cref="P:PX.Objects.GL.GLTranDoc.NeedsDebitCashAccount" />.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.CashAccount.CashAccountID" /> field.
  /// </value>
  [PXDBInt]
  public virtual int? DebitCashAccountID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CA.CashAccount">Credit Cash Account</see> associated with the document.
  /// Credit Cash Account is required for some types of the documents, that can be included in a <see cref="T:PX.Objects.GL.GLDocBatch">document batch</see>
  /// - see <see cref="P:PX.Objects.GL.GLTranDoc.NeedsCreditCashAccount" />.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.CashAccount.CashAccountID" /> field.
  /// </value>
  [PXDBInt]
  public virtual int? CreditCashAccountID { get; set; }

  Decimal? IInvoice.CuryDocBal
  {
    get => this.CuryUnappliedBal;
    set => this.CuryUnappliedBal = value;
  }

  Decimal? IInvoice.DocBal
  {
    get => this.UnappliedBal;
    set => this.UnappliedBal = value;
  }

  Decimal? IInvoice.CuryDiscBal
  {
    get => this.CuryDiscBal;
    set => throw new NotImplementedException();
  }

  Decimal? IInvoice.DiscBal
  {
    get => this.DiscBal;
    set => throw new NotImplementedException();
  }

  Decimal? IInvoice.CuryWhTaxBal
  {
    get => this.CuryWhTaxBal;
    set => throw new NotImplementedException();
  }

  Decimal? IInvoice.WhTaxBal
  {
    get => this.WhTaxBal;
    set => throw new NotImplementedException();
  }

  long? PX.Objects.CM.IRegister.CuryInfoID
  {
    get => this.CuryInfoID;
    set
    {
    }
  }

  DateTime? IInvoice.DiscDate
  {
    get => this.DiscDate;
    set
    {
    }
  }

  public string DocType
  {
    get => this.TranType;
    set
    {
    }
  }

  public string OrigModule
  {
    get => this.Module;
    set
    {
    }
  }

  public DateTime? DocDate
  {
    get => new DateTime?();
    set
    {
    }
  }

  public string DocDesc
  {
    get => (string) null;
    set
    {
    }
  }

  public Decimal? CuryOrigDocAmt
  {
    get => new Decimal?();
    set
    {
    }
  }

  public Decimal? OrigDocAmt
  {
    get => new Decimal?();
    set
    {
    }
  }

  public class PK : 
    PrimaryKeyOf<GLTranDoc>.By<GLTranDoc.module, GLTranDoc.batchNbr, GLTranDoc.lineNbr>
  {
    public static GLTranDoc Find(
      PXGraph graph,
      string module,
      string batchNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (GLTranDoc) PrimaryKeyOf<GLTranDoc>.By<GLTranDoc.module, GLTranDoc.batchNbr, GLTranDoc.lineNbr>.FindBy(graph, (object) module, (object) batchNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.branchID>
    {
    }

    public class Ledger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.ledgerID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.curyInfoID>
    {
    }

    public class JournalVoucher : 
      PrimaryKeyOf<GLDocBatch>.By<GLDocBatch.module, GLDocBatch.batchNbr>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.module, GLTranDoc.batchNbr>
    {
    }

    public class BusinessAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.bAccountID>
    {
    }

    public class Location : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.bAccountID, GLTranDoc.locationID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.paymentMethodID>
    {
    }

    public class CustomerPaymentMethod : 
      PrimaryKeyOf<PX.Objects.AR.CustomerPaymentMethod>.By<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.pMInstanceID>
    {
    }

    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.taxCategoryID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.taxZoneID>
    {
    }

    public class Tax : PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.taxID>
    {
    }

    public class CashAccountEntryType : 
      PrimaryKeyOf<CAEntryType>.By<CAEntryType.entryTypeId>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.entryTypeID>
    {
    }

    public class JournalVoucherCode : 
      PrimaryKeyOf<GLTranCode>.By<GLTranCode.module, GLTranCode.tranType>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.tranModule, GLTranDoc.tranType>
    {
    }

    public class DebitAccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.debitAccountID>
    {
    }

    public class CreditAccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.creditAccountID>
    {
    }

    public class DebitSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.debitSubID>
    {
    }

    public class CreditSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.creditSubID>
    {
    }

    public class Terms : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.termsID>
    {
    }

    public class DebitCashAccount : 
      PrimaryKeyOf<PX.Objects.CA.CashAccount>.By<PX.Objects.CA.CashAccount.cashAccountID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.debitCashAccountID>
    {
    }

    public class CreditCashAccount : 
      PrimaryKeyOf<PX.Objects.CA.CashAccount>.By<PX.Objects.CA.CashAccount.cashAccountID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.creditCashAccountID>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.costCodeID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.projectID, GLTranDoc.taskID>
    {
    }

    public class ProjectTransaction : 
      PrimaryKeyOf<PMTran>.By<PMTran.tranID>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.pMTranID>
    {
    }

    public class ARInvoice : 
      PrimaryKeyOf<PX.Objects.AR.ARInvoice>.By<PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.refNbr>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.tranType, GLTranDoc.refNbr>
    {
    }

    public class APBill : 
      PrimaryKeyOf<PX.Objects.AP.APInvoice>.By<PX.Objects.AP.APInvoice.docType, PX.Objects.AP.APInvoice.refNbr>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.tranType, GLTranDoc.refNbr>
    {
    }

    public class ARPayment : 
      PrimaryKeyOf<PX.Objects.AR.ARPayment>.By<PX.Objects.AR.ARPayment.docType, PX.Objects.AR.ARPayment.refNbr>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.tranType, GLTranDoc.refNbr>
    {
    }

    public class APPayment : 
      PrimaryKeyOf<PX.Objects.AP.APPayment>.By<PX.Objects.AP.APPayment.docType, PX.Objects.AP.APPayment.refNbr>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.tranType, GLTranDoc.refNbr>
    {
    }

    public class CashTransaction : 
      PrimaryKeyOf<CAAdj>.By<CAAdj.adjTranType, CAAdj.adjRefNbr>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.tranType, GLTranDoc.refNbr>
    {
    }

    public class Batch : 
      PrimaryKeyOf<Batch>.By<Batch.module, Batch.batchNbr>.ForeignKeyOf<GLTranDoc>.By<GLTranDoc.tranModule, GLTranDoc.refNbr>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranDoc.branchID>
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranDoc.module>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranDoc.batchNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranDoc.lineNbr>
  {
  }

  public abstract class importRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranDoc.importRefNbr>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranDoc.ledgerID>
  {
  }

  public abstract class parentLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranDoc.parentLineNbr>
  {
  }

  public abstract class split : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranDoc.split>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranDoc.costCodeID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranDoc.curyID>
  {
  }

  public abstract class tranCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranDoc.tranCode>
  {
  }

  public abstract class tranModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranDoc.tranModule>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranDoc.tranType>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  GLTranDoc.tranDate>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranDoc.bAccountID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranDoc.locationID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranDoc.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranDoc.taskID>
  {
  }

  public abstract class entryTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranDoc.entryTypeID>
  {
  }

  public abstract class cADrCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranDoc.cADrCr>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranDoc.paymentMethodID>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranDoc.taxCategoryID>
  {
  }

  public abstract class debitAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranDoc.debitAccountID>
  {
  }

  public abstract class debitSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranDoc.debitSubID>
  {
  }

  public abstract class creditAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranDoc.creditAccountID>
  {
  }

  public abstract class creditSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranDoc.creditSubID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranDoc.refNbr>
  {
  }

  public abstract class docCreated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranDoc.docCreated>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranDoc.extRefNbr>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.tranAmt>
  {
  }

  public abstract class tranTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.tranTotal>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLTranDoc.curyInfoID>
  {
  }

  public abstract class curyTranTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.curyTranTotal>
  {
  }

  public abstract class curyTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.curyTranAmt>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranDoc.released>
  {
  }

  public abstract class tranClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranDoc.tranClass>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranDoc.tranDesc>
  {
  }

  public abstract class tranLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranDoc.tranLineNbr>
  {
  }

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranDoc.pMInstanceID>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranDoc.tranPeriodID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranDoc.finPeriodID>
  {
  }

  public abstract class pMTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLTranDoc.pMTranID>
  {
  }

  public abstract class ledgerBalanceType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranDoc.ledgerBalanceType>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranDoc.termsID>
  {
  }

  public abstract class dueDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  GLTranDoc.dueDate>
  {
  }

  public abstract class discDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  GLTranDoc.discDate>
  {
  }

  public abstract class curyDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.curyDiscAmt>
  {
  }

  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.discAmt>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLTranDoc.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  GLTranDoc.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLTranDoc.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranDoc.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLTranDoc.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLTranDoc.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranDoc.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLTranDoc.lastModifiedDateTime>
  {
  }

  public abstract class groupTranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranDoc.groupTranID>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranDoc.cashAccountID>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranDoc.taxZoneID>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranDoc.taxID>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.taxRate>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranDoc.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.taxableAmt>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.taxAmt>
  {
  }

  public abstract class curyInclTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranDoc.curyInclTaxAmt>
  {
  }

  public abstract class inclTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.inclTaxAmt>
  {
  }

  public abstract class curyOrigWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranDoc.curyOrigWhTaxAmt>
  {
  }

  public abstract class origWhTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.origWhTaxAmt>
  {
  }

  public abstract class curyDocTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.curyDocTotal>
  {
  }

  public abstract class docTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.docTotal>
  {
  }

  public abstract class curyTaxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.curyTaxTotal>
  {
  }

  public abstract class curyApplAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.curyApplAmt>
  {
  }

  public abstract class applAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.applAmt>
  {
  }

  public abstract class curyDiscTaken : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.curyDiscTaken>
  {
  }

  public abstract class discTaken : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.discTaken>
  {
  }

  public abstract class curyTaxWheld : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.curyTaxWheld>
  {
  }

  public abstract class taxWheld : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.taxWheld>
  {
  }

  public abstract class applCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranDoc.applCount>
  {
  }

  public abstract class curyUnappliedBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranDoc.curyUnappliedBal>
  {
  }

  public abstract class unappliedBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.unappliedBal>
  {
  }

  public abstract class curyDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.curyDiscBal>
  {
  }

  public abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.discBal>
  {
  }

  public abstract class curyWhTaxBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.curyWhTaxBal>
  {
  }

  public abstract class whTaxBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranDoc.whTaxBal>
  {
  }

  public abstract class needsDebitCashAccount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTranDoc.needsDebitCashAccount>
  {
  }

  public abstract class isARCustomerCashAccount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTranDoc.isARCustomerCashAccount>
  {
  }

  public abstract class needsCreditCashAccount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTranDoc.needsCreditCashAccount>
  {
  }

  public abstract class needTaskValidation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTranDoc.needTaskValidation>
  {
  }

  public abstract class debitCashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTranDoc.debitCashAccountID>
  {
  }

  public abstract class creditCashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTranDoc.creditCashAccountID>
  {
  }
}
