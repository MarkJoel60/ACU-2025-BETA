// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Batch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.CM;
using PX.Objects.Common.Attributes;
using PX.Objects.GL.DAC;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// A batch of <see cref="T:PX.Objects.GL.GLTran">journal transactions</see>.
/// The records of this type are edited through the Journal Transactions (GL301000) form
/// (which corresponds to the <see cref="T:PX.Objects.GL.JournalEntry" /> graph).
/// GL batches are also created whenever a document that needs posting to GL is released in any other functional area.
/// </summary>
[PXCacheName("GL Batch")]
[PXPrimaryGraph(typeof (JournalEntry))]
[Serializable]
public class Batch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IApprovable, IAssign
{
  protected bool? _Selected = new bool?(false);
  protected int? _BranchID;
  protected 
  #nullable disable
  string _Module;
  protected string _BatchNbr;
  protected int? _LedgerID;
  protected DateTime? _DateEntered;
  protected string _FinPeriodID;
  protected string _BatchType;
  protected string _NumberCode;
  protected string _RefNbr;
  protected string _Status;
  protected Decimal? _CuryDebitTotal;
  protected Decimal? _CuryCreditTotal;
  protected Decimal? _CuryControlTotal;
  protected Decimal? _DebitTotal;
  protected Decimal? _CreditTotal;
  protected Decimal? _ControlTotal;
  protected long? _CuryInfoID;
  protected bool? _AutoReverse;
  protected bool? _AutoReverseCopy;
  protected string _OrigModule;
  protected string _OrigBatchNbr;
  protected bool? _Released;
  protected bool? _Posted;
  protected bool? _Draft;
  protected string _TranPeriodID;
  protected int? _LineCntr;
  protected string _CuryID;
  protected string _ScheduleID;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected bool? _Hold;
  protected bool? _Scheduled;
  protected bool? _Voided;
  protected string _Description;
  protected bool? _CreateTaxTrans;
  private int? _ReverseCount;

  /// <summary>
  /// Indicates whether the record is selected for mass processing.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch" />, to which the batch belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(null, null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>
  /// Key field.
  /// The code of the module, to which the batch belongs.
  /// </summary>
  /// <value>
  /// Allowed values are:
  /// "GL", "AP", "AR", "CM", "CA", "IN", "DR", "FA", "PM", "TX", "SO", "PO".
  /// </value>
  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault]
  [PXUIField]
  [BatchModule.List]
  [PXFieldDescription]
  public virtual string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  /// <summary>
  /// Key field.
  /// Auto-generated unique number of the batch.
  /// </summary>
  /// <value>
  /// The number is generated from the <see cref="T:PX.Objects.CS.Numbering">Numbering Sequence</see> specified in the
  /// setup record of the <see cref="P:PX.Objects.GL.Batch.Module" />, to which the batch belongs.
  /// For example, the numbering sequence for the batches belonging to the General Ledger module
  /// is specified in the <see cref="P:PX.Objects.GL.GLSetup.BatchNumberingID" /> field. For other modules see corresponding setup DACs.
  /// </value>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXSelector(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<Current<Batch.module>>, And<Batch.draft, Equal<False>>>, OrderBy<Desc<Batch.batchNbr>>>), Filterable = true)]
  [PXUIField]
  [BatchModule.Numbering]
  [PXFieldDescription]
  public virtual string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Ledger" />, to which the batch belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Ledger.LedgerID" /> field.
  /// </value>
  [PXDBInt]
  [PXDefault(typeof (Search<Branch.ledgerID, Where<Branch.branchID, Equal<Current<Batch.branchID>>>>))]
  [PXUIField]
  [PXSelector(typeof (Search5<Ledger.ledgerID, LeftJoin<OrganizationLedgerLink, On<Ledger.ledgerID, Equal<OrganizationLedgerLink.ledgerID>>, LeftJoin<Branch, On<Branch.organizationID, Equal<OrganizationLedgerLink.organizationID>, And<Branch.branchID, Equal<Current2<Batch.branchID>>>>>>, Where<Ledger.balanceType, NotEqual<LedgerBalanceType.budget>>, Aggregate<GroupBy<Ledger.ledgerID>>>), SubstituteKey = typeof (Ledger.ledgerCD), DescriptionField = typeof (Ledger.descr), CacheGlobal = true)]
  [PXRestrictor(typeof (Where<Branch.branchID, Equal<Current2<Batch.branchID>>, Or<Current2<Batch.branchID>, IsNull>>), "The {0} ledger is not associated with the branch.", new Type[] {typeof (Ledger.ledgerCD)})]
  public virtual int? LedgerID
  {
    get => this._LedgerID;
    set => this._LedgerID = value;
  }

  /// <summary>The date of the batch, specified by user.</summary>
  /// <value>
  /// Defaults to the current <see cref="P:PX.Data.AccessInfo.BusinessDate">Business Date</see>.
  /// </value>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? DateEntered
  {
    get => this._DateEntered;
    set => this._DateEntered = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see>, to which the batch belongs.
  /// </summary>
  /// <value>
  /// By default the period is deducted from the <see cref="P:PX.Objects.GL.Batch.DateEntered">date of the batch</see>.
  /// Can be overriden by user.
  /// </value>
  [OpenPeriod(null, typeof (Batch.dateEntered), typeof (Batch.branchID), null, null, null, null, true, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (Batch.tranPeriodID), true, IsHeader = true)]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  /// <summary>The type of the batch.</summary>
  /// <value>
  /// Allowed values are:
  /// <c>"H"</c> - Normal,
  /// <c>"R"</c> - Recurring,
  /// <c>"C"</c> - Consolidation,
  /// <c>"T"</c> - Trial Balance,
  /// <c>"RCL"</c> - Reclassification,
  /// <c>"A"</c> - Allocation.
  /// Defaults to <c>"H"</c> - Normal.
  /// </value>
  [PXDBString(3)]
  [PXDefault("H")]
  [PXUIField(DisplayName = "Type")]
  [BatchTypeCode.List]
  public virtual string BatchType
  {
    get => this._BatchType;
    set => this._BatchType = value;
  }

  /// <summary>
  /// The identifier of the <see cref="!:GLNumberCode" /> record used to assign an auto-generated <see cref="P:PX.Objects.GL.Batch.RefNbr" /> to the batch.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:GLNumberCode.NumberCode" /> field.
  /// The number codes are assigned by the <see cref="T:PX.Objects.GL.AllocationProcess" /> and <see cref="T:PX.Objects.GL.ScheduleProcess" /> graphs,
  /// which allows to have separate numbering sequences for recurring and allocation batches.
  /// </value>
  [PXDBString(5, IsUnicode = true, InputMask = ">aaaaa")]
  [PXUIField(DisplayName = "Number. Code")]
  public virtual string NumberCode
  {
    get => this._NumberCode;
    set => this._NumberCode = value;
  }

  /// <summary>
  /// Auto-generated reference number assigned to the batch according to the <see cref="T:PX.Objects.CS.Numbering">Numbering Sequence</see> specified
  /// in the <see cref="!:GLNumberCode.NumberingID" /> field of the corresponding <see cref="!:GLNumberCode" /> record (see the <see cref="P:PX.Objects.GL.Batch.NumberCode" /> field).
  /// </summary>
  /// <value>
  /// The field will remain empty if <see cref="P:PX.Objects.GL.Batch.NumberCode" /> is not set for the batch.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(Visible = false)]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  /// <summary>The read-only status of the batch.</summary>
  /// <value>
  /// The value of this field is determined by the <see cref="P:PX.Objects.GL.Batch.Hold" />, <see cref="P:PX.Objects.GL.Batch.Released" />,
  /// <see cref="P:PX.Objects.GL.Batch.Posted" />, <see cref="P:PX.Objects.GL.Batch.Voided" /> and <see cref="P:PX.Objects.GL.Batch.Scheduled" /> fields.
  /// Possible values are:
  /// <c>"H"</c> - Hold
  /// <c>"B"</c> - Balanced
  /// <c>"U"</c> - Unposted
  /// <c>"P"</c> - Posted
  /// <c>"C"</c> - Completed
  /// <c>"V"</c> - Voided
  /// <c>"R"</c> - Released
  /// <c>"Q"</c> - Partially Released
  /// <c>"S"</c> - Scheduled
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  [PXUIField]
  [BatchStatus.List]
  public virtual string Status
  {
    [PXDependsOnFields(new Type[] {typeof (Batch.posted), typeof (Batch.voided), typeof (Batch.scheduled), typeof (Batch.released), typeof (Batch.hold)})] get
    {
      return this._Status;
    }
    set => this._Status = value;
  }

  /// <summary>
  /// The total debit amount of the batch in its <see cref="P:PX.Objects.GL.Batch.CuryID">currency</see>.
  /// </summary>
  /// <value>
  /// See also <see cref="P:PX.Objects.GL.Batch.DebitTotal" />.
  /// </value>
  [PXDBCurrency(typeof (Batch.curyInfoID), typeof (Batch.debitTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryDebitTotal
  {
    get => this._CuryDebitTotal;
    set => this._CuryDebitTotal = value;
  }

  /// <summary>
  /// The total credit amount of the batch in its <see cref="P:PX.Objects.GL.Batch.CuryID">currency</see>.
  /// </summary>
  /// <value>
  /// See also <see cref="P:PX.Objects.GL.Batch.CreditTotal" />.
  /// </value>
  [PXDBCurrency(typeof (Batch.curyInfoID), typeof (Batch.creditTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryCreditTotal
  {
    get => this._CuryCreditTotal;
    set => this._CuryCreditTotal = value;
  }

  /// <summary>
  /// The control total of the batch in its <see cref="P:PX.Objects.GL.Batch.CuryID">currency</see>.
  /// </summary>
  /// 
  ///             See also <see cref="P:PX.Objects.GL.Batch.ControlTotal" />
  /// .
  [PXDBCurrency(typeof (Batch.curyInfoID), typeof (Batch.controlTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Total")]
  public virtual Decimal? CuryControlTotal
  {
    get => this._CuryControlTotal;
    set => this._CuryControlTotal = value;
  }

  /// <summary>
  /// The total debit amount of the batch in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// </summary>
  /// <value>
  /// See also <see cref="P:PX.Objects.GL.Batch.CuryDebitTotal" />.
  /// </value>
  [PXDBBaseCury(typeof (Batch.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DebitTotal
  {
    get => this._DebitTotal;
    set => this._DebitTotal = value;
  }

  /// <summary>
  /// The total credit amount of the batch in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// </summary>
  /// <value>
  /// See also <see cref="P:PX.Objects.GL.Batch.CuryCreditTotal" />.
  /// </value>
  [PXDBBaseCury(typeof (Batch.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CreditTotal
  {
    get => this._CreditTotal;
    set => this._CreditTotal = value;
  }

  /// <summary>
  /// The control total of the batch in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// </summary>
  /// <value>
  /// See also <see cref="P:PX.Objects.GL.Batch.CuryControlTotal" />.
  /// </value>
  [PXDBBaseCury(typeof (Batch.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ControlTotal
  {
    get => this._ControlTotal;
    set => this._ControlTotal = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> record associated with the batch.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:PCurrencyInfo.CurrencyInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo(Required = true)]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the batch is auto-reversing.
  /// For a batch of this kind the system automatically generates a reversing batch in the next period.
  /// The reversing batch is generated either when the original batch is posted or when it is released,
  /// depending on the value of the <see cref="P:PX.Objects.GL.GLSetup.AutoRevOption" /> field of the GL preferences record.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? AutoReverse
  {
    get => this._AutoReverse;
    set => this._AutoReverse = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the batch is a reversing batch.
  /// See also the <see cref="P:PX.Objects.GL.Batch.OrigModule" /> and <see cref="P:PX.Objects.GL.Batch.OrigBatchNbr" /> fields.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? AutoReverseCopy
  {
    get => this._AutoReverseCopy;
    set => this._AutoReverseCopy = value;
  }

  /// <summary>
  /// The module, to which the original batch (e.g. the one reversed by this batch) belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.Module" /> field.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Orig. Module", Visible = false, Enabled = false)]
  public virtual string OrigModule
  {
    get => this._OrigModule;
    set => this._OrigModule = value;
  }

  /// <summary>
  /// The number of the original batch (e.g. the one reversed by this batch).
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.BatchNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<Current<Batch.origModule>>>>))]
  public virtual string OrigBatchNbr
  {
    get => this._OrigBatchNbr;
    set => this._OrigBatchNbr = value;
  }

  /// <summary>Indicates whether the batch has been released.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released")]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  /// <summary>Indicates whether the batch has been posted.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Posted")]
  public virtual bool? Posted
  {
    get => this._Posted;
    set => this._Posted = value;
  }

  /// <summary>Indicates whether the batch is required to be posted.</summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? RequirePost { get; set; }

  /// <summary>
  /// Indicates how many times system has failed while posting batch.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? PostErrorCount { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates the the batch is a draft.
  /// The drafts of batches are not displayed in the Journal Transactions (GL301000) form.
  /// </summary>
  /// <value>
  /// This field is set to <c>true</c> by the Journal Vouchers (GL304000) form (<see cref="T:PX.Objects.GL.JournalWithSubEntry" /> graph)
  /// when a new <see cref="T:PX.Objects.GL.GLTranDoc" /> defining a transactions batch is created.
  /// This allows to reserve a <see cref="P:PX.Objects.GL.Batch.BatchNbr">number</see> for the batch, while not showing it in the interface until
  /// the <see cref="T:PX.Objects.GL.GLTranDoc" /> is released and the batch is actually created.
  /// Outside Journal Vouchers defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Draft
  {
    get => this._Draft;
    set => this._Draft = value;
  }

  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID
  {
    get => this._TranPeriodID;
    set => this._TranPeriodID = value;
  }

  /// <summary>
  /// The counter of the document lines, used <i>internally</i> to assign consistent numbers to newly created lines.
  /// It is not recommended to rely on this field to determine the exact count of lines, because it might not reflect the latter under some conditions.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

  /// <summary>
  /// The code of the <see cref="T:PX.Objects.CM.Currency">Currency</see> of the batch.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </value>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Schedule" />, associated with the batch.
  /// </summary>
  /// <value>
  /// If <see cref="P:PX.Objects.GL.Batch.Scheduled" /> is <c>true</c> for the batch, then this field provides the identifier of the schedule,
  /// into which the batch is included as a template.
  /// Otherwise, the field is set to the identifier of the schedule that produced the batch.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Schedule.ScheduleID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  public virtual string ScheduleID
  {
    get => this._ScheduleID;
    set => this._ScheduleID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXSearchable(16 /*0x10*/, "{0} {1} - {2}", new Type[] {typeof (Batch.module), typeof (Batch.batchNbr), typeof (Batch.branchID)}, new Type[] {typeof (Batch.ledgerID), typeof (Batch.description)}, NumberFields = new Type[] {typeof (Batch.batchNbr)}, Line1Format = "{0}{1}{2:d}", Line1Fields = new Type[] {typeof (Batch.ledgerID), typeof (Batch.finPeriodID), typeof (Batch.dateEntered)}, Line2Format = "{0}", Line2Fields = new Type[] {typeof (Batch.description)})]
  [PXNote(DescriptionField = typeof (Batch.batchNbr), ShowInReferenceSelector = true)]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBTimestamp(RecordComesFirst = true)]
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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  /// <summary>Indicates whether the batch is on hold.</summary>
  /// <value>
  /// Defaults to <c>true</c>, if the <see cref="P:PX.Objects.GL.GLSetup.HoldEntry" /> flag is set in the preferences of the module,
  /// and to <c>false</c> otherwise.
  /// </value>
  [PXDBBool]
  [PXUIField]
  [PXDefault(true, typeof (Search<GLSetup.holdEntry>))]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  /// <summary>
  /// When <c>true</c>, indicates that the batch is included as a template into a <see cref="T:PX.Objects.GL.Schedule" /> pointed to by the <see cref="P:PX.Objects.GL.Batch.ScheduleID" /> field.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Scheduled
  {
    get => this._Scheduled;
    set => this._Scheduled = value;
  }

  /// <summary>Indicates whether the batch has been voided.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Voided")]
  public virtual bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  /// <summary>The description of the batch.</summary>
  [PXUIField]
  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the system must generate <see cref="T:PX.Objects.TX.TaxTran">tax transactions</see> for the batch.
  /// </summary>
  /// <value>
  /// This field is taken into account only if the <see cref="P:PX.Objects.CS.FeaturesSet.TaxEntryFromGL" /> feature is on.
  /// Affects only those batches, which belong to the General Ledger <see cref="P:PX.Objects.GL.Batch.Module" />.
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Create Tax Transactions")]
  public virtual bool? CreateTaxTrans
  {
    get => this._CreateTaxTrans;
    set => this._CreateTaxTrans = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the system should not validate the <see cref="T:PX.Objects.TX.TaxTran">tax transactions</see> associated with the batch.
  /// </summary>
  /// <value>
  /// The value of this field is relevant only if <see cref="P:PX.Objects.GL.Batch.CreateTaxTrans" /> is <c>true</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Skip Tax Amount Validation")]
  public virtual bool? SkipTaxValidation { get; set; }

  /// <summary>
  /// The read-only field, reflecting the number of batches in the system, which reverse this batch.
  /// </summary>
  /// <value>
  /// This field is populated only by the <see cref="T:PX.Objects.GL.JournalEntry" /> graph, which corresponds to the Journal Transactions (GL301000) form.
  /// </value>
  [PXInt]
  [PXUIField(DisplayName = "Reversing Batches", Visible = false, Enabled = false, IsReadOnly = true)]
  public int? ReverseCount
  {
    get => this._ReverseCount;
    set => this._ReverseCount = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the batch has a detail with the visible <see cref="P:PX.Objects.GL.GLTran.CuryReclassRemainingAmt" /> field.
  /// </summary>
  /// <value>
  /// This field is populated only by the <see cref="T:PX.Objects.GL.JournalEntry" /> graph, which corresponds to the Journal Transactions (GL301000) form.
  /// </value>
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2019R2.")]
  [PXBool]
  [PXUIField(Enabled = false, Visible = false)]
  public bool? HasRamainingAmount { get; set; }

  /// <summary>
  /// When set, on persist checks, that the document has the corresponded <see cref="P:PX.Objects.GL.Batch.Released" /> original value.
  /// When not set, on persist checks, that <see cref="P:PX.Objects.GL.Batch.Released" /> value is not changed.
  /// Throws an error otherwise.
  /// </summary>
  [PXDBRestrictionBool(typeof (Batch.released))]
  public virtual bool? ReleasedToVerify { get; set; }

  /// <summary>
  /// When set, on persist checks, that the document has the corresponded <see cref="P:PX.Objects.GL.Batch.Posted" /> original value.
  /// When not set, on persist checks, that <see cref="P:PX.Objects.GL.Batch.Posted" /> value is not changed.
  /// Throws an error otherwise.
  /// </summary>
  [PXDBRestrictionBool(typeof (Batch.posted))]
  public virtual bool? PostedToVerify { get; set; }

  [Owner(IsDBField = false)]
  public virtual int? ApproverID { get; set; }

  [PXInt]
  [PXCompanyTreeSelector]
  public virtual int? ApproverWorkgroupID { get; set; }

  int? IAssign.WorkgroupID
  {
    get => this.ApproverWorkgroupID;
    set => this.ApproverWorkgroupID = value;
  }

  int? IAssign.OwnerID
  {
    get => this.ApproverID;
    set => this.ApproverID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Approved { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Rejected { get; set; }

  /// <summary>
  /// Indicates that the current document should be excluded from the
  /// approval process. Maintenance of this property is on graph level.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DontApprove { get; set; }

  public class PK : PrimaryKeyOf<Batch>.By<Batch.module, Batch.batchNbr>
  {
    public static Batch Find(PXGraph graph, string module, string batchNbr, PKFindOptions options = 0)
    {
      return (Batch) PrimaryKeyOf<Batch>.By<Batch.module, Batch.batchNbr>.FindBy(graph, (object) module, (object) batchNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<Batch>.By<Batch.branchID>
    {
    }

    public class Ledger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<Batch>.By<Batch.ledgerID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<Batch>.By<Batch.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<Batch>.By<Batch.curyID>
    {
    }

    public class OriginalBatch : 
      PrimaryKeyOf<Batch>.By<Batch.module, Batch.batchNbr>.ForeignKeyOf<Batch>.By<Batch.origModule, Batch.origBatchNbr>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<Schedule>.By<Schedule.scheduleID>.ForeignKeyOf<Batch>.By<Batch.scheduleID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.selected>
  {
  }

  public class Events : PXEntityEventBase<Batch>.Container<Batch.Events>
  {
    public PXEntityEvent<Batch, Schedule> ConfirmSchedule;
    public PXEntityEvent<Batch, Schedule> VoidSchedule;
    public PXEntityEvent<Batch> ReleaseBatch;
    public PXEntityEvent<Batch> PostBatch;
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Batch.branchID>
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Batch.module>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Batch.batchNbr>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Batch.ledgerID>
  {
  }

  public abstract class dateEntered : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Batch.dateEntered>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Batch.finPeriodID>
  {
  }

  public abstract class batchType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Batch.batchType>
  {
  }

  public abstract class numberCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Batch.numberCode>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Batch.refNbr>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Batch.status>
  {
  }

  public abstract class curyDebitTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Batch.curyDebitTotal>
  {
  }

  public abstract class curyCreditTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Batch.curyCreditTotal>
  {
  }

  public abstract class curyControlTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Batch.curyControlTotal>
  {
  }

  public abstract class debitTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Batch.debitTotal>
  {
  }

  public abstract class creditTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Batch.creditTotal>
  {
  }

  public abstract class controlTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Batch.controlTotal>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  Batch.curyInfoID>
  {
  }

  public abstract class autoReverse : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.autoReverse>
  {
  }

  public abstract class autoReverseCopy : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.autoReverseCopy>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Batch.origModule>
  {
  }

  public abstract class origBatchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Batch.origBatchNbr>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.released>
  {
  }

  public abstract class posted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.posted>
  {
  }

  public abstract class requirePost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.requirePost>
  {
  }

  public class postErrorCountLimit : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  Batch.postErrorCountLimit>
  {
    public postErrorCountLimit()
      : base(5)
    {
    }
  }

  public abstract class postErrorCount : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.postErrorCount>
  {
  }

  public abstract class draft : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.draft>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Batch.tranPeriodID>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Batch.lineCntr>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Batch.curyID>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Batch.scheduleID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Batch.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Batch.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Batch.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Batch.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Batch.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Batch.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Batch.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Batch.lastModifiedDateTime>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.hold>
  {
  }

  public abstract class scheduled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.scheduled>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.voided>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Batch.description>
  {
  }

  public abstract class createTaxTrans : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.createTaxTrans>
  {
  }

  public abstract class skipTaxValidation : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.skipTaxValidation>
  {
  }

  public abstract class reverseCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Batch.reverseCount>
  {
  }

  public abstract class hasRamainingAmount : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.hasRamainingAmount>
  {
  }

  public abstract class releasedToVerify : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.releasedToVerify>
  {
  }

  public abstract class postedToVerify : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.postedToVerify>
  {
  }

  public abstract class approverID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Batch.approverID>
  {
  }

  public abstract class approverWorkgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Batch.approverWorkgroupID>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.rejected>
  {
  }

  public abstract class dontApprove : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.dontApprove>
  {
  }
}
