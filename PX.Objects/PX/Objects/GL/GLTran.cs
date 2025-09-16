// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.DAC.Abstract;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// A journal transaction.
/// The transactions are grouped in <see cref="T:PX.Objects.GL.Batch">batches</see>
/// and edited through the Journal Transactions (GL301000) form
/// (which corresponds to the <see cref="T:PX.Objects.GL.JournalEntry" /> graph).
/// </summary>
[PXCacheName("GL Transaction")]
[PXPrimaryGraph(typeof (JournalEntry))]
[Serializable]
public class GLTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAccountable
{
  protected int? _BranchID;
  protected 
  #nullable disable
  string _Module;
  protected string _BatchNbr;
  protected int? _LineNbr;
  protected int? _LedgerID;
  protected int? _AccountID;
  protected int? _SubID;
  protected int? _ProjectID;
  protected int? _TaskID;
  protected int? _CostCodeID;
  protected string _RefNbr;
  protected int? _InventoryID;
  protected string _UOM;
  protected Decimal? _Qty;
  protected Decimal? _DebitAmt;
  protected Decimal? _CreditAmt;
  protected long? _CuryInfoID;
  protected Decimal? _CuryDebitAmt;
  protected Decimal? _CuryCreditAmt;
  protected bool? _Released;
  protected bool? _Posted;
  protected bool? _NonBillable;
  protected bool? _IsInterCompany;
  protected bool? _SummPost;
  protected string _OrigModule;
  protected string _OrigBatchNbr;
  protected int? _OrigLineNbr;
  protected int? _OrigAccountID;
  protected int? _OrigSubID;
  protected int? _TranID;
  protected string _TranType;
  protected string _TranClass;
  protected string _TranDesc;
  protected DateTime? _TranDate;
  protected int? _TranLineNbr;
  protected int? _ReferenceID;
  protected string _FinPeriodID;
  protected string _TranPeriodID;
  protected long? _CATranID;
  protected long? _PMTranID;
  protected long? _OrigPMTranID;
  protected string _LedgerBalanceType;
  protected bool? _AccountRequireUnits;
  protected string _TaxID;
  protected string _TaxCategoryID;
  protected Guid? _NoteID;
  protected bool? _ReclassificationProhibited;
  protected string _ReclassBatchModule;
  protected string _ReclassBatchNbr;
  protected bool? _IsReclassReverse;
  protected string _ReclassSourceTranModule;
  protected string _ReclassSourceTranBatchNbr;
  protected int? _ReclassSourceTranLineNbr;
  protected int? _ReclassSeqNbr;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>Used for selection on screens.</summary>
  [PXBool]
  [PXUIField(DisplayName = "Selected", Visible = false)]
  public virtual bool? Selected { get; set; }

  /// <summary>
  /// Used for storing state of "Reclassification History" button in GL301000 and GL404000
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Included in Reclass. History", Enabled = false)]
  public virtual bool? IncludedInReclassHistory { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch" />, to which the transaction belongs.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Batch.BranchID">branch of the parent batch</see>.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(typeof (Batch.branchID), null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>
  /// Key field.
  /// The code of the module, to which the transaction belongs.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Batch.Module">module of the parent batch</see>.
  /// Possible values are:
  /// "GL", "AP", "AR", "CM", "CA", "IN", "DR", "FA", "PM", "TX", "SO", "PO".
  /// </value>
  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (Batch))]
  [PXUIField]
  [BatchModule.List]
  public virtual string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  /// <summary>
  /// Key field.
  /// The number of the <see cref="T:PX.Objects.GL.Batch" />, to which the transaction belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.BatchNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (Batch))]
  [PXParent(typeof (Select<Batch, Where<Batch.module, Equal<Current<GLTran.module>>, And<Batch.batchNbr, Equal<Current<GLTran.batchNbr>>>>>))]
  [PXUIField]
  public virtual string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  /// <summary>
  /// Key field. Auto-generated.
  /// The number of the transaction in the <see cref="T:PX.Objects.GL.Batch" />.
  /// </summary>
  /// <value>
  /// Note that the sequence of line numbers of the transactions belonging to a single batch may include gaps.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXLineNbr(typeof (Batch.lineCntr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Ledger" />, to which the transaction belongs.
  /// </summary>
  /// <value>
  /// If the <see cref="P:PX.Objects.GL.Ledger.BalanceType">Balance Type</see> of the <see cref="P:PX.Objects.GL.Batch.LedgerID">ledger of the batch</see> is Actual (<c>"A"</c>),
  /// defaults to the <see cref="P:PX.Objects.GL.Branch.LedgerID">ledger of the branch</see>. Otherwise defaults to the <see cref="P:PX.Objects.GL.Batch.LedgerID">ledger of the batch</see>.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Ledger.LedgerID" /> field.
  /// </value>
  [PXDBInt]
  [PXFormula(typeof (Switch<Case<Where<Selector<Current<Batch.ledgerID>, Ledger.balanceType>, Equal<PX.Objects.GL.LedgerBalanceType.actual>>, Selector<GLTran.branchID, Branch.ledgerID>>, Current<Batch.ledgerID>>))]
  [PXDefault]
  public virtual int? LedgerID
  {
    get => this._LedgerID;
    set => this._LedgerID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Account" /> of the transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(typeof (GLTran.branchID), LedgerID = typeof (GLTran.ledgerID), DescriptionField = typeof (Account.description))]
  [PXDefault]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Sub">Subaccount</see> of the transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (GLTran.accountID), typeof (GLTran.branchID), true)]
  [PXDefault]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.PM.PMProject">Project</see> associated with the transaction,
  /// or the <see cref="P:PX.Objects.PM.PMSetup.NonProjectCode">non-project code</see> indicating that the transaction is not related to any particular project.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMProject.ProjectID" /> field.
  /// </value>
  [GLProjectDefault(typeof (GLTran.ledgerID))]
  [ActiveProjectOrContractForGL(AccountFieldType = typeof (GLTran.accountID))]
  [PXForeignReference(typeof (Field<GLTran.projectID>.IsRelatedTo<PMProject.contractID>))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.PM.PMTask">Task</see> associated with the transaction.
  /// The field is relevant only if the Projects module has been activated.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMTask.TaskID" /> field.
  /// </value>
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<GLTran.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [PXForeignReference(typeof (CompositeKey<Field<GLTran.projectID>.IsRelatedTo<PMTask.projectID>, Field<GLTran.taskID>.IsRelatedTo<PMTask.taskID>>))]
  [BaseProjectTask(typeof (GLTran.projectID), "GL", DisplayName = "Project Task", AllowInactive = false)]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [CostCode(typeof (GLTran.accountID), typeof (GLTran.taskID), ReleasedField = typeof (GLTran.released), InventoryField = typeof (GLTran.inventoryID), ProjectField = typeof (GLTran.projectID), UseNewDefaulting = true)]
  [PXForeignReference(typeof (Field<GLTran.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsNonPM { get; set; }

  /// <summary>The reference number of the transaction.</summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Batch.RefNbr">reference number of the batch</see>.
  /// Can be overriden by user.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXDefault]
  [PXDBDefault(typeof (Batch.refNbr), DefaultForUpdate = false, DefaultForInsert = false)]
  [PXUIField]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.IN.InventoryItem">Inventory Item</see>, associated with the transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [Inventory(Enabled = false, Visible = false)]
  [PXForeignReference(typeof (Field<GLTran.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  /// <summary>
  /// The code of the <see cref="T:PX.Objects.IN.INUnit">Unit of Measure</see> for the <see cref="P:PX.Objects.GL.GLTran.Qty">qunatity</see> of the transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="T:PX.Objects.IN.INUnit.fromUnit" /> field.
  /// </value>
  [INUnit(typeof (GLTran.inventoryID), typeof (GLTran.accountID), typeof (GLTran.accountRequireUnits))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  /// <summary>The quantity of the transaction.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  /// <summary>
  /// The debit amount of the transaction.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// See also the <see cref="P:PX.Objects.GL.GLTran.CuryDebitAmt" /> field.
  /// </summary>
  [PXDBBaseCury(typeof (GLTran.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(null, typeof (SumCalc<Batch.debitTotal>))]
  public virtual Decimal? DebitAmt
  {
    get => this._DebitAmt;
    set => this._DebitAmt = value;
  }

  /// <summary>
  /// The credit amount of the transaction.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// See also the <see cref="P:PX.Objects.GL.GLTran.CuryCreditAmt" /> field.
  /// </summary>
  [PXDBBaseCury(typeof (GLTran.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(null, typeof (SumCalc<Batch.creditTotal>))]
  public virtual Decimal? CreditAmt
  {
    get => this._CreditAmt;
    set => this._CreditAmt = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the transaction.
  /// </summary>
  /// <value>
  /// Auto-generated. Corresponds to the <see cref="!:PX.Objects.CM.CurrencyInfo.CurrencyInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo(typeof (PX.Objects.CM.CurrencyInfo.curyInfoID), Required = true)]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  /// <summary>
  /// The debit amount of the transaction.
  /// Given in the <see cref="P:PX.Objects.GL.Batch.CuryID">currency</see> of the batch.
  /// See also the <see cref="P:PX.Objects.GL.GLTran.DebitAmt" /> field.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXFormula(null, typeof (SumCalc<Batch.curyDebitTotal>))]
  [PXDBCurrency(typeof (GLTran.curyInfoID), typeof (GLTran.debitAmt))]
  public virtual Decimal? CuryDebitAmt
  {
    get => this._CuryDebitAmt;
    set => this._CuryDebitAmt = value;
  }

  /// <summary>
  /// The credit amount of the transaction.
  /// Given in the <see cref="P:PX.Objects.GL.Batch.CuryID">currency</see> of the batch.
  /// See also the <see cref="P:PX.Objects.GL.GLTran.CreditAmt" /> field.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXFormula(null, typeof (SumCalc<Batch.curyCreditTotal>))]
  [PXDBCurrency(typeof (GLTran.curyInfoID), typeof (GLTran.creditAmt))]
  public virtual Decimal? CuryCreditAmt
  {
    get => this._CuryCreditAmt;
    set => this._CuryCreditAmt = value;
  }

  /// <summary>Indicates whether the transaction has been released.</summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  /// <summary>Indicates whether the transaction has been posted.</summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Posted
  {
    get => this._Posted;
    set => this._Posted = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the transaction is non-billable in the <see cref="P:PX.Objects.GL.GLTran.ProjectID">Project</see>
  /// This means that when releasing the batch the system will set the <see cref="P:PX.Objects.PM.PMTran.Billable" /> field of
  /// the <see cref="T:PX.Objects.PM.PMTran">project transaction</see> generated from this transaction to <c>false</c>.
  /// This field is relevant only if the Projects module has been activated and integrated with the General Ledger module.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Non Billable", FieldClass = "PROJECT")]
  public virtual bool? NonBillable
  {
    get => this._NonBillable;
    set => this._NonBillable = value;
  }

  /// <summary>
  /// When <c>true</c>, indicates that the transaction is an inter-branch transaction.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsInterCompany
  {
    get => this._IsInterCompany;
    set => this._IsInterCompany = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the system must summarize the transaction with other ones when posting.
  /// In this case all transactions being posted are grouped by <see cref="P:PX.Objects.GL.GLTran.AccountID">Account</see> and <see cref="P:PX.Objects.GL.GLTran.SubID">Subaccount</see>.
  /// Then, the amounts are summarized and a single transaction is created for each group.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? SummPost
  {
    get => this._SummPost;
    set => this._SummPost = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the system must post the transaction even if its amounts are equal to zero.
  /// </summary>
  [PXBool]
  public virtual bool? ZeroPost { get; set; }

  /// <summary>
  /// The module, to which the original batch (e.g. the one reversed by the parent batch of this transaction) belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.Module" /> field.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Orig. Module", Visible = false)]
  public virtual string OrigModule
  {
    get => this._OrigModule;
    set => this._OrigModule = value;
  }

  /// <summary>
  /// The number of the original batch (e.g. the one reversed by the parent batch of this transaction).
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.BatchNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Orig. Batch Nbr.", Visible = false)]
  public virtual string OrigBatchNbr
  {
    get => this._OrigBatchNbr;
    set => this._OrigBatchNbr = value;
  }

  /// <summary>
  /// The number of the corresponding transaction in the original batch (e.g. the one reversed by the parent batch of this transaction).
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.GLTran.LineNbr" />.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Orig. Line Nbr.", Visible = false)]
  public virtual int? OrigLineNbr
  {
    get => this._OrigLineNbr;
    set => this._OrigLineNbr = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Account" /> associated with the original document.
  /// This field is populated for the transactions updating the account, which is different from the account of the original document.
  /// For example, if upon release of an Accounts Receivable document an RGOL account must be updated, the system will generate a transaction
  /// with <see cref="P:PX.Objects.GL.GLTran.AccountID" /> set to the RGOL account and <see cref="P:PX.Objects.GL.GLTran.OrigAccountID" /> set to
  /// the Accounts Receivable account associated with the document being released.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  public virtual int? OrigAccountID
  {
    get => this._OrigAccountID;
    set => this._OrigAccountID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the original document.
  /// For more information see the <see cref="P:PX.Objects.GL.GLTran.OrigAccountID" /> field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (GLTran.origAccountID))]
  public virtual int? OrigSubID
  {
    get => this._OrigSubID;
    set => this._OrigSubID = value;
  }

  /// <summary>
  /// A unique identifier of the transaction reserved for internal use.
  /// </summary>
  [PXDBIdentity]
  public virtual int? TranID
  {
    get => this._TranID;
    set => this._TranID = value;
  }

  /// <summary>
  /// The type of the original document or transaction.
  /// This field is populated when a document is released in another module, such as Accounts Receivable or Accounts Payable.
  /// For example, when an <see cref="T:PX.Objects.AR.ARInvoice">ARInvoice</see> is released, the system will set this field for
  /// the resulting transactions to the document's <see cref="P:PX.Objects.AR.ARInvoice.DocType">DocType</see>.
  /// </summary>
  [PXDBString(3, IsFixed = true)]
  [PXDefault("")]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  /// <summary>
  /// Reserved for internal use.
  /// The class of the document or transaction defined by the line.
  /// This field affects posting of documents and transactions to GL.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  public virtual string TranClass
  {
    get => this._TranClass;
    set => this._TranClass = value;
  }

  /// <summary>The description of the transaction.</summary>
  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXUIField]
  public virtual string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  /// <summary>The date of the transaction.</summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Batch.DateEntered">date of the parent batch</see>.
  /// </value>
  [PXDBDate]
  [PXDefault(typeof (Batch.dateEntered))]
  [PXUIField]
  public virtual DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  /// <summary>
  /// Reserved for internal use.
  /// This field is populated on release of a document in another module with the number of the corresponding line in that document.
  /// This field is not populated when <see cref="P:PX.Objects.GL.GLTran.SummPost" /> is on and in some other cases.
  /// </summary>
  [PXDBInt]
  public virtual int? TranLineNbr
  {
    get => this._TranLineNbr;
    set => this._TranLineNbr = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.AR.Customer">Customer</see> or <see cref="T:PX.Objects.AP.Vendor">Vendor</see>
  /// associated with the transaction.
  /// This field is populated when a document is released in Accounts Receivable or Accounts Payable module.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:PX.Objects.AR.Customer.BAccountID">Customer.BAccountID</see> and
  /// <see cref="!:PX.Objects.AP.Vendor.BAccountID">Vendor.BAccountID</see> fields.
  /// </value>
  [PXDBInt]
  [PXSelector(typeof (Search<BAccountR.bAccountID>), SubstituteKey = typeof (BAccountR.acctCD))]
  [CustomerVendorRestrictor]
  [PXUIField(DisplayName = "Customer/Vendor", Enabled = false, Visible = false)]
  public virtual int? ReferenceID
  {
    get => this._ReferenceID;
    set => this._ReferenceID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see>, to which the transaction belongs.
  /// </summary>
  /// <value>
  /// Is equal to the <see cref="P:PX.Objects.GL.Batch.FinPeriodID" /> of the parent batch.
  /// For the explanation of the difference between this field and the <see cref="P:PX.Objects.GL.GLTran.TranPeriodID" />
  /// see the descriptions of the corresponding fields in the <see cref="T:PX.Objects.GL.Batch" /> class.
  /// </value>
  [PXDefault]
  [PX.Objects.GL.FinPeriodID(null, typeof (GLTran.branchID), null, null, null, null, true, false, null, typeof (GLTran.tranPeriodID), typeof (Batch.tranPeriodID), true, true)]
  [PXUIField(DisplayName = "Period ID", Enabled = false, Visible = false)]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see>, to which the transaction belongs.
  /// </summary>
  /// <value>
  /// Is equal to the <see cref="P:PX.Objects.GL.Batch.TranPeriodID" /> of the parent batch.
  /// For the explanation of the difference between this field and the <see cref="P:PX.Objects.GL.GLTran.FinPeriodID" />
  /// see the descriptions of the corresponding fields in the <see cref="T:PX.Objects.GL.Batch" /> class.
  /// </value>
  [PXDefault]
  [PeriodID(null, null, null, true)]
  [PXUIField(DisplayName = "Master Period ID", Enabled = false, Visible = false)]
  public virtual string TranPeriodID
  {
    get => this._TranPeriodID;
    set => this._TranPeriodID = value;
  }

  /// <summary>
  /// The read-only year, to which the transaction is posted.
  /// </summary>
  /// <value>
  /// The value of this field is determined from the <see cref="P:PX.Objects.GL.GLTran.FinPeriodID" />.
  /// </value>
  [PXString(4, IsFixed = true)]
  public virtual string PostYear
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTran.finPeriodID)})] get
    {
      return this._FinPeriodID != null ? FinPeriodUtils.FiscalYear(this._FinPeriodID) : (string) null;
    }
  }

  /// <summary>
  /// The read-only year, to which the transaction is posted.
  /// </summary>
  /// <value>
  /// The value of this field is determined from the <see cref="P:PX.Objects.GL.GLTran.TranPeriodID" />.
  /// </value>
  [PXString(4, IsFixed = true)]
  public virtual string TranYear
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTran.tranPeriodID)})] get
    {
      return this._TranPeriodID != null ? FinPeriodUtils.FiscalYear(this._TranPeriodID) : (string) null;
    }
  }

  /// <summary>
  /// The read-only field providing the year, following the <see cref="P:PX.Objects.GL.GLTran.PostYear" />.
  /// </summary>
  [PXString(6, IsFixed = true)]
  public virtual string NextPostYear
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTran.postYear)})] get
    {
      return this.PostYear != null ? AutoNumberAttribute.NextNumber(this.PostYear) + "00" : (string) null;
    }
  }

  /// <summary>
  /// The read-only field providing the year, following the <see cref="P:PX.Objects.GL.GLTran.TranYear" />.
  /// </summary>
  [PXString(6, IsFixed = true)]
  public virtual string NextTranYear
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTran.tranYear)})] get
    {
      return this.TranYear != null ? AutoNumberAttribute.NextNumber(this.TranYear) + "00" : (string) null;
    }
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CA.CATran">Cash Transaction</see> associated with this transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.CATran.TranID">CATran.TranID</see> field.
  /// </value>
  [PXDBLong]
  [GLCashTranID]
  public virtual long? CATranID
  {
    get => this._CATranID;
    set => this._CATranID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.PM.PMTran">Project Transaction</see> associated with this transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMTran.TranID" /> field.
  /// </value>
  [PXDBChildIdentity(typeof (PMTran.tranID))]
  [PXDBLong]
  [PXUIField(DisplayName = "PM Tran.", Visible = false, Enabled = false, FieldClass = "PROJECT")]
  public virtual long? PMTranID
  {
    get => this._PMTranID;
    set => this._PMTranID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.PM.PMTran">Project Transaction</see> associated with the original transaction
  /// (e.g. the transaction reversed by this one).
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMTran.TranID" /> field.
  /// </value>
  [PXDBLong]
  public virtual long? OrigPMTranID
  {
    get => this._OrigPMTranID;
    set => this._OrigPMTranID = value;
  }

  /// <summary>
  /// The type of balance of the <see cref="T:PX.Objects.GL.Ledger" />, associated with the transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Ledger.BalanceType" /> field.
  /// </value>
  [PXString(1, IsFixed = true, InputMask = "")]
  public virtual string LedgerBalanceType
  {
    get => this._LedgerBalanceType;
    set => this._LedgerBalanceType = value;
  }

  /// <summary>
  /// Indicates whether the account associated with the transaction requires <see cref="P:PX.Objects.GL.GLTran.UOM">Units of Measure</see> to be specified.
  /// </summary>
  [PXBool]
  public virtual bool? AccountRequireUnits
  {
    get => this._AccountRequireUnits;
    set => this._AccountRequireUnits = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.TX.Tax" /> associated with the transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.TX.Tax.TaxID" /> field.
  /// </value>
  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Tax ID")]
  [PXSelector(typeof (Search<PX.Objects.TX.Tax.taxID, Where2<Where<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.sales>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.vat>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.use>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.withholding>>>>>, And<PX.Objects.TX.Tax.deductibleVAT, Equal<False>, And<Where2<Where<PX.Objects.TX.Tax.purchTaxAcctID, Equal<Current<GLTran.accountID>>, And<PX.Objects.TX.Tax.purchTaxSubID, Equal<Current<GLTran.subID>>>>, Or<Where<PX.Objects.TX.Tax.salesTaxAcctID, Equal<Current<GLTran.accountID>>, And<PX.Objects.TX.Tax.salesTaxSubID, Equal<Current<GLTran.subID>>>>>>>>>>))]
  public virtual string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.TX.TaxCategory">Tax Category</see> associated with the transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.TX.TaxCategory.TaxCategoryID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [PXDefault(typeof (Search<Account.taxCategoryID, Where<Account.accountID, Equal<Current<GLTran.accountID>>>>))]
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <summary>Indicates that the transaction can't be reclassified.</summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ReclassificationProhibited
  {
    get => this._ReclassificationProhibited;
    set => this._ReclassificationProhibited = value;
  }

  /// <summary>
  /// The module of the <see cref="T:PX.Objects.GL.Batch" />, by which the transaction has been reclassified.
  /// </summary>
  [PXUIField(DisplayName = "Reclass. Batch Module", Enabled = false)]
  [PXDBString(2, IsFixed = true)]
  public virtual string ReclassBatchModule
  {
    get => this._ReclassBatchModule;
    set => this._ReclassBatchModule = value;
  }

  /// <summary>
  /// The number of the <see cref="T:PX.Objects.GL.Batch" />, by which the transaction has been reclassified.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Reclass. Batch Number", Enabled = false)]
  public virtual string ReclassBatchNbr
  {
    get => this._ReclassBatchNbr;
    set => this._ReclassBatchNbr = value;
  }

  /// <summary>
  /// This field distinguishes reversed transaction in the reclassification pair from transaction on destination account.
  /// When <c>true</c>, indicates that the transaction is a reversed of source transaction.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsReclassReverse
  {
    get => this._IsReclassReverse;
    set => this._IsReclassReverse = value;
  }

  [PXDBString(1)]
  [PXUIField]
  [PX.Objects.GL.ReclassType.List]
  public virtual string ReclassType { get; set; }

  [PXDBCurrency(typeof (GLTran.curyInfoID), typeof (GLTran.reclassRemainingAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Remaining Reclass. Amount", Enabled = false, Visible = false)]
  [ReclassRemaining]
  public virtual Decimal? CuryReclassRemainingAmt { get; set; }

  [PXDBBaseCury(typeof (GLTran.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ReclassRemainingAmt { get; set; }

  /// <summary>
  /// Indicates that the transaction has been reclassified.
  /// It is set on Reclass Batch Releasing. This fact is used.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Reclassified { get; set; }

  /// <summary>
  /// According to the <see cref="T:PX.Objects.GL.GLTran.tranDate" /> field of the original reclassified transaction.
  /// This field is filling on the <see cref="M:PX.Objects.GL.Reclassification.Processing.ReclassifyTransactionsProcessor.ProcessTransForReclassification(System.Collections.Generic.List{PX.Objects.GL.Reclassification.Common.GLTranForReclassification},PX.Objects.GL.Reclassification.Common.ReclassGraphState)" /> Reclassify Transactions (GL506000) screen.
  /// </summary>
  [PXDBDate]
  public virtual DateTime? ReclassOrigTranDate { get; set; }

  [PXDBString(2, IsFixed = true)]
  public virtual string ReclassSourceTranModule
  {
    get => this._ReclassSourceTranModule;
    set => this._ReclassSourceTranModule = value;
  }

  [PXDBString(15, IsUnicode = true)]
  public virtual string ReclassSourceTranBatchNbr
  {
    get => this._ReclassSourceTranBatchNbr;
    set => this._ReclassSourceTranBatchNbr = value;
  }

  [PXDBInt]
  public virtual int? ReclassSourceTranLineNbr
  {
    get => this._ReclassSourceTranLineNbr;
    set => this._ReclassSourceTranLineNbr = value;
  }

  /// <summary>
  /// Sequence number of reclassification pair in reclassification chain.
  /// </summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Reclass. Sequence Nbr.", Visible = false)]
  public virtual int? ReclassSeqNbr
  {
    get => this._ReclassSeqNbr;
    set => this._ReclassSeqNbr = value;
  }

  /// <summary>The count of reclassifying transactions.</summary>
  [PXDBInt]
  public virtual int? ReclassTotalCount { get; set; }

  /// <summary>The count of reclassifying transactions.</summary>
  [PXDBInt]
  public virtual int? ReclassReleasedCount { get; set; }

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

  /// <summary>
  /// Turn off amount normalization when transactions are posted to GL
  /// </summary>
  [PXBool]
  public virtual bool? SkipNormalizeAmounts { get; set; }

  public static string GetKeyImage(string module, string batchNbr, int? lineNbr)
  {
    return $"{typeof (GLTran.module).Name}:{module}, {typeof (GLTran.batchNbr).Name}:{batchNbr}, {typeof (GLTran.lineNbr).Name}:{lineNbr}";
  }

  public string GetKeyImage() => GLTran.GetKeyImage(this.Module, this.BatchNbr, this.LineNbr);

  public static string GetImage(string module, string batchNbr, int? lineNbr)
  {
    return $"{EntityHelper.GetFriendlyEntityName(typeof (GLTran))}[{GLTran.GetKeyImage(module, batchNbr, lineNbr)}]";
  }

  public virtual string ToString() => GLTran.GetImage(this.Module, this.BatchNbr, this.LineNbr);

  public class PK : PrimaryKeyOf<GLTran>.By<GLTran.module, GLTran.batchNbr, GLTran.lineNbr>
  {
    public static GLTran Find(
      PXGraph graph,
      string module,
      string batchNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (GLTran) PrimaryKeyOf<GLTran>.By<GLTran.module, GLTran.batchNbr, GLTran.lineNbr>.FindBy(graph, (object) module, (object) batchNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<GLTran>.By<GLTran.branchID>
    {
    }

    public class Ledger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<GLTran>.By<GLTran.ledgerID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<GLTran>.By<GLTran.curyInfoID>
    {
    }

    public class Batch : 
      PrimaryKeyOf<Batch>.By<Batch.module, Batch.batchNbr>.ForeignKeyOf<GLTran>.By<GLTran.module, GLTran.batchNbr>
    {
    }

    public class Account : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<GLTran>.By<GLTran.accountID>
    {
    }

    public class Subaccount : PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<GLTran>.By<GLTran.subID>
    {
    }

    public class OriginalTransaction : 
      PrimaryKeyOf<GLTran>.By<GLTran.module, GLTran.batchNbr, GLTran.lineNbr>.ForeignKeyOf<GLTran>.By<GLTran.origModule, GLTran.origBatchNbr, GLTran.origLineNbr>
    {
    }

    public class OriginalAccount : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<GLTran>.By<GLTran.origAccountID>
    {
    }

    public class OriginalSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<GLTran>.By<GLTran.origSubID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<GLTran>.By<GLTran.inventoryID>
    {
    }

    public class CashAccountTransaction : 
      PrimaryKeyOf<CATran>.By<CATran.tranID>.ForeignKeyOf<GLTran>.By<GLTran.cATranID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTran.selected>
  {
  }

  public abstract class includedInReclassHistory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTran.includedInReclassHistory>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTran.branchID>
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTran.module>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTran.batchNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTran.lineNbr>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTran.ledgerID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTran.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTran.subID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTran.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTran.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTran.costCodeID>
  {
  }

  public abstract class isNonPM : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTran.isNonPM>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTran.refNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTran.inventoryID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTran.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTran.qty>
  {
  }

  public abstract class debitAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTran.debitAmt>
  {
  }

  public abstract class creditAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTran.creditAmt>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLTran.curyInfoID>
  {
  }

  public abstract class curyDebitAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTran.curyDebitAmt>
  {
  }

  public abstract class curyCreditAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTran.curyCreditAmt>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTran.released>
  {
  }

  public abstract class posted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTran.posted>
  {
  }

  public abstract class nonBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTran.nonBillable>
  {
  }

  public abstract class isInterCompany : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTran.isInterCompany>
  {
  }

  public abstract class summPost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTran.summPost>
  {
  }

  public abstract class zeroPost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTran.zeroPost>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTran.origModule>
  {
  }

  public abstract class origBatchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTran.origBatchNbr>
  {
  }

  public abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTran.origLineNbr>
  {
  }

  public abstract class origAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTran.origAccountID>
  {
  }

  public abstract class origSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTran.origSubID>
  {
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTran.tranID>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTran.tranType>
  {
    public const string Consolidation = "CON";
  }

  public abstract class tranClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTran.tranClass>
  {
    public const string Normal = "N";
    public const string Charge = "U";
    public const string Discount = "D";
    public const string Payment = "P";
    public const string WriteOff = "B";
    public const string Tax = "T";
    public const string WithholdingTax = "W";
    public const string Consolidation = "C";
    public const string RealizedAndRoundingGOL = "R";
    public const string Rounding = "X";
    public const string UnrealizedAndRevaluationGOL = "G";
    public const string RetainageWithheld = "E";
    public const string RetainageReleased = "F";
    public const string ShippedNotInvoiced = "S";
    public const string Void = "V";
    public const string PrepaymentInvoice = "Y";
    public const string ZeroRecord = "Z";

    public class normal : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    GLTran.tranClass.normal>
    {
      public normal()
        : base("N")
      {
      }
    }

    public class charge : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    GLTran.tranClass.charge>
    {
      public charge()
        : base("U")
      {
      }
    }

    public class discount : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    GLTran.tranClass.discount>
    {
      public discount()
        : base("D")
      {
      }
    }

    public class payment : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    GLTran.tranClass.payment>
    {
      public payment()
        : base("P")
      {
      }
    }

    public class writeoff : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    GLTran.tranClass.writeoff>
    {
      public writeoff()
        : base("B")
      {
      }
    }

    public class tax : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    GLTran.tranClass.tax>
    {
      public tax()
        : base("T")
      {
      }
    }

    public class withholdingTax : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      GLTran.tranClass.withholdingTax>
    {
      public withholdingTax()
        : base("W")
      {
      }
    }

    public class consolidation : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    GLTran.tranClass.consolidation>
    {
      public consolidation()
        : base("C")
      {
      }
    }

    public class rgol : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    GLTran.tranClass.rgol>
    {
      public rgol()
        : base("R")
      {
      }
    }

    public class rounding : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    GLTran.tranClass.rounding>
    {
      public rounding()
        : base("X")
      {
      }
    }

    public class evaluation : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    GLTran.tranClass.evaluation>
    {
      public evaluation()
        : base("G")
      {
      }
    }

    public class retainageWithheld : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      GLTran.tranClass.retainageWithheld>
    {
      public retainageWithheld()
        : base("E")
      {
      }
    }

    public class retainageReleased : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      GLTran.tranClass.retainageReleased>
    {
      public retainageReleased()
        : base("F")
      {
      }
    }

    public class shippedNotInvoiced : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      GLTran.tranClass.shippedNotInvoiced>
    {
      public shippedNotInvoiced()
        : base("S")
      {
      }
    }

    public class @void : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    GLTran.tranClass.@void>
    {
      public @void()
        : base("V")
      {
      }
    }

    public class prepaymentInvoice : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      GLTran.tranClass.prepaymentInvoice>
    {
      public prepaymentInvoice()
        : base("Y")
      {
      }
    }

    public class zeroRecord : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    GLTran.tranClass.zeroRecord>
    {
      public zeroRecord()
        : base("Z")
      {
      }
    }
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTran.tranDesc>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  GLTran.tranDate>
  {
  }

  public abstract class tranLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTran.tranLineNbr>
  {
  }

  public abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTran.referenceID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTran.finPeriodID>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTran.tranPeriodID>
  {
  }

  public abstract class postYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTran.postYear>
  {
  }

  public abstract class tranYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTran.tranYear>
  {
  }

  public abstract class nextPostYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTran.nextPostYear>
  {
  }

  public abstract class nextTranYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTran.nextTranYear>
  {
  }

  public abstract class cATranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLTran.cATranID>
  {
  }

  public abstract class pMTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLTran.pMTranID>
  {
  }

  public abstract class origPMTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLTran.origPMTranID>
  {
  }

  public abstract class ledgerBalanceType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTran.ledgerBalanceType>
  {
  }

  public abstract class accountRequireUnits : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTran.accountRequireUnits>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTran.taxID>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTran.taxCategoryID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLTran.noteID>
  {
  }

  public abstract class reclassificationProhibited : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTran.reclassificationProhibited>
  {
  }

  public abstract class reclassBatchModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTran.reclassBatchModule>
  {
  }

  public abstract class reclassBatchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTran.reclassBatchNbr>
  {
  }

  public abstract class isReclassReverse : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTran.isReclassReverse>
  {
  }

  public abstract class reclassType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTran.reclassType>
  {
  }

  public abstract class curyReclassRemainingAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTran.curyReclassRemainingAmt>
  {
  }

  public abstract class reclassRemainingAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTran.reclassRemainingAmt>
  {
  }

  public abstract class reclassified : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTran.reclassified>
  {
  }

  public abstract class reclassOrigTranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLTran.reclassOrigTranDate>
  {
  }

  public abstract class reclassSourceTranModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTran.reclassSourceTranModule>
  {
  }

  public abstract class reclassSourceTranBatchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTran.reclassSourceTranBatchNbr>
  {
  }

  public abstract class reclassSourceTranLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTran.reclassSourceTranLineNbr>
  {
  }

  public abstract class reclassSeqNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTran.reclassSeqNbr>
  {
  }

  public abstract class reclassTotalCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTran.reclassTotalCount>
  {
  }

  public abstract class reclassReleasedCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTran.reclassReleasedCount>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  GLTran.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLTran.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTran.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLTran.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLTran.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTran.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLTran.lastModifiedDateTime>
  {
  }

  public abstract class skipNormalizeAmounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTran.skipNormalizeAmounts>
  {
  }
}
