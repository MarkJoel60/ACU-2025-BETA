// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRScheduleDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.DR;

/// <summary>
/// A revenue or expense recognition component of a <see cref="T:PX.Objects.DR.DRSchedule">deferral
/// schedule</see>. This entity encapsulates the <see cref="T:PX.Objects.IN.INComponent">inventory item
/// component</see> level information of the deferral schedule.
/// Usually, deferral schedules contain a single component corresponding to the
/// <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> specified in the source document
/// line. However, multiple components can be present in case the inventory item
/// associated with the document line is a multiple deliverable arrangement (MDA),
/// or when custom deferral components are added by the user.
/// The entities of this type are edited on the Deferral Schedule (DR201500) form,
/// which corresponds to the <see cref="T:PX.Objects.DR.DraftScheduleMaint" /> graph.
/// </summary>
[DebuggerDisplay("SheduleID={ScheduleID} ComponentID={ComponentID} TotalAmt={TotalAmt} DefAmt={DefAmt}")]
[PXCacheName("Deferral Schedule Component")]
[PXPrimaryGraph(new System.Type[] {typeof (DraftScheduleMaint)}, new System.Type[] {typeof (Search<DRSchedule.scheduleID, Where<DRSchedule.scheduleID, Equal<Current<DRScheduleDetail.scheduleID>>>>)})]
[Serializable]
public class DRScheduleDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const int EmptyComponentID = 0;
  public const int EmptyBAccountID = 0;
  protected int? _ScheduleID;
  protected int? _ComponentID;
  protected 
  #nullable disable
  string _Module;
  protected string _DocType;
  protected string _RefNbr;
  protected int? _LineNbr;
  protected string _Status;
  protected Decimal? _TotalAmt;
  protected Decimal? _DefAmt;
  protected string _DefCode;
  protected int? _DefAcctID;
  protected int? _DefSubID;
  protected int? _AccountID;
  protected int? _SubID;
  protected bool? _IsOpen;
  protected Decimal? _DefTotal;
  protected int? _LineCntr;
  protected DateTime? _DocDate;
  protected string _LastRecFinPeriodID;
  protected string _CloseFinPeriodID;
  protected int? _CreditLineNbr;
  protected int? _BAccountID;
  protected bool? _IsCustom;
  protected string _DocumentType;
  protected string _BAccountType;
  protected string _DefCodeType;
  protected bool? _Selected = new bool?(false);
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The unique identifier of the parent <see cref="T:PX.Objects.DR.DRSchedule">
  /// deferral schedule</see>. This field is a part of the
  /// compound key of the record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.DR.DRSchedule.ScheduleID" /> field.
  /// </value>
  [PXDBDefault(typeof (DRSchedule.scheduleID))]
  [PXSelector(typeof (DRSchedule.scheduleID), SubstituteKey = typeof (DRSchedule.scheduleNbr), DirtyRead = true)]
  [PXParent(typeof (Select<DRSchedule, Where<DRSchedule.scheduleID, Equal<Current<DRScheduleDetail.scheduleID>>>>))]
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Schedule Number")]
  public virtual int? ScheduleID
  {
    get => this._ScheduleID;
    set => this._ScheduleID = value;
  }

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// The unique identifier of the <see cref="T:PX.Objects.IN.InventoryItem">
  /// inventory item</see> (or one of its components, in case
  /// of a multiple deliverable arrangement) associated
  /// with the schedule component. This field is a part of the
  /// compound key of the record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" />
  /// field. If the schedule component originates from a
  /// document line with no inventory item specified, the value
  /// of this field is equal to <see cref="F:PX.Objects.DR.DRScheduleDetail.EmptyComponentID" />.
  /// </value>
  /// <remarks>
  /// Within a <see cref="T:PX.Objects.DR.DRSchedule">deferral schedule</see>, there
  /// can be only one schedule component with <see cref="P:PX.Objects.DR.DRScheduleDetail.ComponentID" />
  /// equal to <see cref="F:PX.Objects.DR.DRScheduleDetail.EmptyComponentID" />.
  /// </remarks>
  [PXDBInt(IsKey = true)]
  [PXUIField]
  [DRComponentSelector(SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
  public virtual int? ComponentID
  {
    get => this._ComponentID;
    set => this._ComponentID = value;
  }

  [PXInt]
  public virtual int? ParentInventoryID { get; set; }

  /// <summary>
  /// The line number of the component.
  /// This field is defaulted from the current value of
  /// the <see cref="P:PX.Objects.DR.DRSchedule.DetailLineCntr" /> field
  /// of the parent deferred schedule.
  /// </summary>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (DRSchedule.detailLineCntr), DecrementOnDelete = true)]
  [PXUIField(DisplayName = "Detail Line Nbr.", Enabled = false)]
  public virtual int? DetailLineNbr { get; set; }

  /// <summary>
  /// The module associated with the schedule component.
  /// This field defaults to <see cref="P:PX.Objects.DR.DRSchedule.Module" />.
  /// </summary>
  /// <value>
  /// This field can have one of the following values:
  /// <c>"AP"</c>: Accounts Payable,
  /// <c>"AR"</c>: Accounts Receivable.
  /// </value>
  /// <remarks>
  /// Any changes made to this field also affect the value
  /// of the <see cref="P:PX.Objects.DR.DRScheduleDetail.DefCodeType" /> field.
  /// </remarks>
  [PXDefault(typeof (DRSchedule.module))]
  [PXDBString(2, IsFixed = true)]
  [PXStringList(new string[] {"AR", "AP"}, new string[] {"Revenue", "Expense"})]
  [PXUIField]
  public virtual string Module
  {
    get => this._Module;
    set
    {
      this._Module = value;
      if (value == "AP")
      {
        this._BAccountType = "VE";
        this.DefCodeType = "E";
      }
      else
      {
        this._BAccountType = "CU";
        this.DefCodeType = "I";
      }
    }
  }

  /// <summary>
  /// The type of the document from which the schedule component
  /// has been created. The value defaults to <see cref="P:PX.Objects.DR.DRSchedule.DocType" />.
  /// </summary>
  [PXDefault("INV")]
  [PXDBString(3, IsFixed = true)]
  [PXUIField]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  /// <summary>
  /// The reference number of the document from which the
  /// schedule component has been created. The value defaults to
  /// <see cref="P:PX.Objects.DR.DRSchedule.RefNbr" />.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [DRDocumentSelector(typeof (DRScheduleDetail.module), typeof (DRScheduleDetail.docType), null)]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  /// <summary>
  /// The document line number associated with the
  /// parent <see cref="T:PX.Objects.DR.DRSchedule">deferral schedule</see>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.DR.DRSchedule.LineNbr" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Line Nbr.", Enabled = false, Visible = false)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (DRSchedule.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>The status of the schedule component.</summary>
  /// <value>
  /// This field can have one of the values defined by
  /// <see cref="T:PX.Objects.DR.DRScheduleStatus.ListAttribute" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("O")]
  [DRScheduleStatus.List]
  [PXUIField]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  /// <summary>
  /// The total amount of expense or revenue that is associated
  /// with the component and should be posted to the Accounts
  /// Receivable or Accounts Payable account.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (DRSchedule.origLineAmt))]
  [PXUIField(DisplayName = "Total Amount")]
  public virtual Decimal? TotalAmt
  {
    get => this._TotalAmt;
    set => this._TotalAmt = value;
  }

  [PXDBCurrency(typeof (DRScheduleDetail.curyInfoID), typeof (DRScheduleDetail.totalAmt))]
  public virtual Decimal? CuryTotalAmt { get; set; }

  [PXDBCurrency(typeof (DRScheduleDetail.curyInfoID), typeof (DRScheduleDetail.defAmt))]
  public virtual Decimal? CuryDefAmt { get; set; }

  /// <summary>
  /// The remaining deferred revenue or expense amount to be recognized
  /// upon the release of <see cref="T:PX.Objects.DR.DRScheduleTran">deferral
  /// transactions</see> associated with the component.
  /// </summary>
  /// <value>
  /// For normal components, the value of this field is initially
  /// equal to <see cref="P:PX.Objects.DR.DRScheduleDetail.TotalAmt" />, and is decreased during the
  /// /// revenue or expense recognition process (which is performed on
  /// the Run Recognition (DR501000) form). For residual components of
  /// multiple deliverable arrangement items (for which the
  /// <see cref="P:PX.Objects.DR.DRScheduleDetail.IsResidual" /> flag is set to <c>true</c>), this value
  /// is zero because the amount is directly posted to the AP or AR account
  /// without deferral.
  /// </value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? DefAmt
  {
    get => this._DefAmt;
    set => this._DefAmt = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.DR.DRDeferredCode">
  /// deferral code</see> associated with the component.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.DR.DRDeferredCode.DeferredCodeID" />
  /// field.
  /// </value>
  [PXSelector(typeof (Search<DRDeferredCode.deferredCodeID, Where<DRDeferredCode.accountType, Equal<Current<DRScheduleDetail.defCodeType>>>>))]
  [PXRestrictor(typeof (Where<DRDeferredCode.active, Equal<True>>), "The {0} deferral code is deactivated on the Deferral Codes (DR202000) form.", new System.Type[] {typeof (DRDeferredCode.deferredCodeID)})]
  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXUIEnabled(typeof (Where<Current<DRScheduleDetail.isCustom>, Equal<True>>))]
  [PXForeignReference(typeof (Field<DRScheduleDetail.defCode>.IsRelatedTo<DRDeferredCode.deferredCodeID>))]
  public virtual string DefCode
  {
    get => this._DefCode;
    set => this._DefCode = value;
  }

  /// <summary>
  /// The identifier of the deferred revenue or deferred expense
  /// account associated with the component.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [PXUIVerify]
  [PXUIVerify]
  [Account]
  public virtual int? DefAcctID
  {
    get => this._DefAcctID;
    set => this._DefAcctID = value;
  }

  /// <summary>
  /// The identifier of the deferred revenue or deferred expense
  /// subaccount associated with the component.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (DRScheduleDetail.defAcctID))]
  public virtual int? DefSubID
  {
    get => this._DefSubID;
    set => this._DefSubID = value;
  }

  [PXString(15, IsUnicode = true)]
  [PXFormula(typeof (Search<APTran.receiptNbr, Where<APTran.tranType, Equal<Current<DRSchedule.docType>>, And<APTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<APTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>>>>>))]
  [PXDefault(typeof (Search<APTran.receiptNbr, Where<APTran.tranType, Equal<Current<DRSchedule.docType>>, And<APTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<APTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>>>>>))]
  public virtual string ReceiptNbr { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXFormula(typeof (Search<APTran.pONbr, Where<APTran.tranType, Equal<Current<DRSchedule.docType>>, And<APTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<APTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>>>>>))]
  [PXDefault(typeof (Search<APTran.pONbr, Where<APTran.tranType, Equal<Current<DRSchedule.docType>>, And<APTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<APTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>>>>>))]
  public virtual string PONbr { get; set; }

  [PXString]
  [PXFormula(typeof (Switch<Case<Where<DRScheduleDetail.receiptNbr, IsNotNull, Or<DRScheduleDetail.pONbr, IsNotNull>>, ControlAccountModule.pO>, Empty>))]
  public virtual string AllowControlAccountForModule { get; set; }

  /// <summary>
  /// The identifier of the income or expense account associated
  /// with the component.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  /// <summary>
  /// The identifier of the income or expense subaccount
  /// associated with the component.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (DRScheduleDetail.accountID))]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the component
  /// is open, which means that there are unrecognized
  /// <see cref="T:PX.Objects.DR.DRScheduleTran">deferral transactions</see>
  /// associated with the component.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "IsOpen")]
  public virtual bool? IsOpen
  {
    get => this._IsOpen;
    set => this._IsOpen = value;
  }

  /// <summary>
  /// The total deferred revenue or expense amount to be recognized
  /// upon the release of <see cref="T:PX.Objects.DR.DRScheduleTran">deferral
  /// transactions</see> associated with the component.
  /// </summary>
  [PXBaseCury]
  [PXUIField(DisplayName = "Line Total", Enabled = false)]
  public virtual Decimal? DefTotal
  {
    get => this._DefTotal;
    set => this._DefTotal = value;
  }

  /// <summary>
  /// The number of <see cref="T:PX.Objects.DR.DRScheduleTran">deferral transactions
  /// </see> associated with the component. When a deferral transaction
  /// is added to the component, this field provides the value for
  /// <see cref="P:PX.Objects.DR.DRScheduleTran.LineNbr" />, being incremented after
  /// each added transaction.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

  /// <summary>
  /// The date of the parent <see cref="T:PX.Objects.DR.DRSchedule">
  /// deferral schedule</see>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.DR.DRSchedule.DocDate" /> field.
  /// </value>
  [PXDBDate]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  [PXUIField(DisplayName = "Date", Enabled = false)]
  public virtual DateTime? DocDate
  {
    get => this._DocDate;
    set => this._DocDate = value;
  }

  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID { get; set; }

  /// <summary>
  /// The financial period ID of the parent <see cref="T:PX.Objects.DR.DRSchedule">
  /// deferral schedule</see>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.DR.DRSchedule.FinPeriodID" /> field.
  /// </value>
  [PXDefault]
  [APAROpenPeriod(typeof (DRScheduleDetail.module), typeof (DRSchedule.docDate), typeof (DRScheduleDetail.branchID), null, null, null, null, true, typeof (DRScheduleDetail.tranPeriodID), true)]
  [PXUIField(DisplayName = "Post Period", Enabled = false)]
  public virtual string FinPeriodID { get; set; }

  /// <summary>
  /// The financial period of the latest recognition of a
  /// <see cref="T:PX.Objects.DR.DRScheduleTran">deferral transaction</see>
  /// associated with the component.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:OrganizationFinPeriod.FinPeriodID" /> field.
  /// </value>
  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXUIField(DisplayName = "Last Recognition Period", Enabled = false)]
  public virtual string LastRecFinPeriodID
  {
    get => this._LastRecFinPeriodID;
    set => this._LastRecFinPeriodID = value;
  }

  /// <summary>
  /// The financial period in which all <see cref="T:PX.Objects.DR.DRScheduleTran">
  /// deferral transactions</see> associated with the component
  /// have been recognized and <see cref="P:PX.Objects.DR.DRScheduleDetail.DefAmt" /> has become zero.
  /// </summary>
  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXUIField(DisplayName = "Close Period", Enabled = false)]
  public virtual string CloseFinPeriodID
  {
    get => this._CloseFinPeriodID;
    set => this._CloseFinPeriodID = value;
  }

  /// <summary>
  /// The integer line number of the <see cref="T:PX.Objects.DR.DRScheduleTran">
  /// deferral transaction</see>, which posts the <see cref="P:PX.Objects.DR.DRScheduleDetail.TotalAmt">total
  /// component amount</see> to the deferral account specified by the
  /// <see cref="P:PX.Objects.DR.DRScheduleDetail.DefAcctID" /> field. The value defaults to zero.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  [PXUIField]
  public virtual int? CreditLineNbr
  {
    get => this._CreditLineNbr;
    set => this._CreditLineNbr = value;
  }

  /// <summary>
  /// The unique identifier of the <see cref="T:PX.Objects.CR.BAccount">
  /// business account</see> associated with the component.
  /// </summary>
  /// <value>
  /// The value of this field is always equal to the
  /// value of the <see cref="P:PX.Objects.DR.DRSchedule.BAccountID" />
  /// field in the parent schedule.
  /// </value>
  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Business Account", Enabled = false)]
  [PXSelector(typeof (Search<BAccountR.bAccountID, Where<BAccountR.type, Equal<Current<DRScheduleDetail.bAccountType>>, Or<BAccountR.type, Equal<PX.Objects.CR.BAccountType.combinedType>>>>), new System.Type[] {typeof (BAccountR.acctCD), typeof (BAccountR.acctName), typeof (BAccountR.type)}, SubstituteKey = typeof (BAccountR.acctCD), DescriptionField = typeof (BAccountR.acctName))]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the
  /// schedule component has been manually added to
  /// the parent <see cref="T:PX.Objects.DR.DRSchedule">deferral schedule
  /// </see> by the user.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Custom", Enabled = false)]
  public virtual bool? IsCustom
  {
    get => this._IsCustom;
    set => this._IsCustom = value;
  }

  /// <summary>
  /// The type of the document to which the parent
  /// <see cref="T:PX.Objects.DR.DRSchedule">deferral schedule</see>
  /// corresponds.
  /// </summary>
  /// <value>
  /// The value of this field is always equal to the
  /// value of the <see cref="P:PX.Objects.DR.DRSchedule.DocumentType" />
  /// field in the parent deferral schedule.
  /// </value>
  [PXString(3, IsFixed = true)]
  [DRScheduleDocumentType.List]
  [PXUIField]
  public virtual string DocumentType
  {
    get => this._DocumentType;
    set => this._DocumentType = value;
  }

  /// <summary>
  /// The type of the <see cref="T:PX.Objects.CR.BAccount">business account</see> associated
  /// with the current schedule component.
  /// </summary>
  [PXDefault("CU")]
  [PXString(2, IsFixed = true)]
  [PXStringList(new string[] {"VE", "CU"}, new string[] {"Vendor", "Customer"})]
  public virtual string BAccountType
  {
    get => this._BAccountType;
    set => this._BAccountType = value;
  }

  /// <summary>
  /// The type of the deferral account for the schedule component.
  /// </summary>
  /// <value>
  /// This field can have one of the values defined in the
  /// <see cref="T:PX.Objects.DR.DeferredAccountType" /> class.
  /// </value>
  /// <remarks>
  /// The value of this field changes automatically upon changing
  /// the <see cref="P:PX.Objects.DR.DRScheduleDetail.Module" /> field.
  /// </remarks>
  [PXString(1)]
  [PXDefault("I")]
  [LabelList(typeof (DeferredAccountType))]
  public virtual string DefCodeType
  {
    get => this._DefCodeType;
    set => this._DefCodeType = value;
  }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the record
  /// has been selected for processing in the UI.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  /// <summary>
  /// The unique identifier of the <see cref="T:PX.Objects.PM.PMProject">
  /// project</see> specified in the document line (if any)
  /// associated with the parent <see cref="T:PX.Objects.DR.DRSchedule">deferral schedule</see>.
  /// </summary>
  [PXDBInt]
  [PXDefault(typeof (Search<PX.Objects.AR.ARTran.projectID, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<DRSchedule.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<PX.Objects.AR.ARTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>>>>>))]
  [PXDimensionSelector("CONTRACT", typeof (Search<PX.Objects.CT.Contract.contractID>), typeof (PX.Objects.CT.Contract.contractCD))]
  [PXUIField]
  public virtual int? ProjectID { get; set; }

  [PXDefault(typeof (Search<PX.Objects.AR.ARTran.taskID, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<DRSchedule.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<PX.Objects.AR.ARTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>>>>>))]
  [ProjectTask(typeof (DRSchedule.projectID), DisplayName = "Project Task", AllowNullIfContract = true, Enabled = false, FieldClass = "ASC606")]
  public virtual int? TaskID { get; set; }

  /// <summary>
  /// If set to <c>true</c>, indicates that the deferral schedule line
  /// represents a residual component whose amount should be directly
  /// posted to the AP or AR account upon component release (without
  /// generating or recognizing <see cref="T:PX.Objects.DR.DRScheduleTran">deferral
  /// transactions</see>). For details, see <see cref="P:PX.Objects.IN.INComponent.AmtOption" />.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public bool? IsResidual { get; set; }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.salesUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<DRScheduleDetail.componentID>>>>))]
  [INUnit(typeof (DRScheduleDetail.componentID))]
  public virtual string UOM { get; set; }

  [PXDBDecimal(6, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Qty.", Visible = false, Enabled = false)]
  public virtual Decimal? BaseQty { get; set; }

  [PXDBQuantity(typeof (DRScheduleDetail.uOM), typeof (DRScheduleDetail.baseQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty { get; set; }

  [PXDBPriceCost]
  [PXUIField(DisplayName = "Fair Value Price")]
  public virtual Decimal? FairValuePrice { get; set; }

  [PXDBDecimal(6, MinValue = -100.0, MaxValue = 100.0)]
  [PXUIField(DisplayName = "Discount Percent", Visible = false, IsReadOnly = true)]
  public virtual Decimal? DiscountPercent { get; set; }

  [PXDBDecimal(5)]
  [PXUIField(DisplayName = "Share")]
  public virtual Decimal? Percentage { get; set; }

  [PXDBDecimal(6, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CoTerm Rate", Visible = false, Enabled = false)]
  public virtual Decimal? CoTermRate { get; set; }

  [PXDBPriceCost]
  [PXUIField(DisplayName = "Effective Fair Value Price")]
  public virtual Decimal? EffectiveFairValuePrice { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Fair Value Currency", Enabled = false)]
  public virtual string FairValueCuryID { get; set; }

  [PXDecimal(18)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Allocation Weight Residual", Visible = false, Enabled = false)]
  public Decimal? AllocationWeightResidual { get; set; }

  [PXDBDate]
  [PXDefault(typeof (Coalesce<Search<PX.Objects.AR.ARTran.dRTermStartDate, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<DRScheduleDetail.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<DRScheduleDetail.refNbr>>, And<PX.Objects.AR.ARTran.lineNbr, Equal<Current<DRScheduleDetail.lineNbr>>, And<Current<DRSchedule.module>, Equal<BatchModule.moduleAR>>>>>>, Search<APTran.dRTermStartDate, Where<APTran.tranType, Equal<Current<DRScheduleDetail.docType>>, And<APTran.refNbr, Equal<Current<DRScheduleDetail.refNbr>>, And<APTran.lineNbr, Equal<Current<DRScheduleDetail.lineNbr>>, And<Current<DRSchedule.module>, Equal<BatchModule.moduleAP>>>>>>>))]
  [PXUIField(DisplayName = "Term Start Date", Enabled = false, FieldClass = "ASC606")]
  public virtual DateTime? TermStartDate { get; set; }

  [PXDBDate]
  [PXDefault(typeof (Coalesce<Search<PX.Objects.AR.ARTran.dRTermEndDate, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<DRScheduleDetail.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<DRScheduleDetail.refNbr>>, And<PX.Objects.AR.ARTran.lineNbr, Equal<Current<DRScheduleDetail.lineNbr>>, And<Current<DRSchedule.module>, Equal<BatchModule.moduleAR>>>>>>, Search<APTran.dRTermEndDate, Where<APTran.tranType, Equal<Current<DRScheduleDetail.docType>>, And<APTran.refNbr, Equal<Current<DRScheduleDetail.refNbr>>, And<APTran.lineNbr, Equal<Current<DRScheduleDetail.lineNbr>>, And<Current<DRSchedule.module>, Equal<BatchModule.moduleAP>>>>>>>))]
  [PXUIField(DisplayName = "Term End Date", Enabled = false, FieldClass = "ASC606")]
  public virtual DateTime? TermEndDate { get; set; }

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

  public class PK : 
    PrimaryKeyOf<DRScheduleDetail>.By<DRScheduleDetail.scheduleID, DRScheduleDetail.componentID, DRScheduleDetail.detailLineNbr>
  {
    public static DRScheduleDetail Find(
      PXGraph graph,
      int? scheduleID,
      int? componentID,
      int? detailLineNbr,
      PKFindOptions options = 0)
    {
      return (DRScheduleDetail) PrimaryKeyOf<DRScheduleDetail>.By<DRScheduleDetail.scheduleID, DRScheduleDetail.componentID, DRScheduleDetail.detailLineNbr>.FindBy(graph, (object) scheduleID, (object) componentID, (object) detailLineNbr, options);
    }
  }

  public static class FK
  {
    public class Schedule : 
      PrimaryKeyOf<DRSchedule>.By<DRSchedule.scheduleID>.ForeignKeyOf<DRScheduleDetail>.By<DRScheduleDetail.scheduleID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<DRScheduleDetail>.By<DRScheduleDetail.branchID>
    {
    }

    public class Component : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<DRScheduleDetail>.By<DRScheduleDetail.componentID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<DRScheduleDetail>.By<DRScheduleDetail.curyInfoID>
    {
    }

    public class DeferralCode : 
      PrimaryKeyOf<DRDeferredCode>.By<DRDeferredCode.deferredCodeID>.ForeignKeyOf<DRScheduleDetail>.By<DRScheduleDetail.defCode>
    {
    }

    public class DeferralAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<DRScheduleDetail>.By<DRScheduleDetail.defAcctID>
    {
    }

    public class DeferralSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<DRScheduleDetail>.By<DRScheduleDetail.defSubID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<DRScheduleDetail>.By<DRScheduleDetail.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<DRScheduleDetail>.By<DRScheduleDetail.subID>
    {
    }

    public class Contract : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<DRScheduleDetail>.By<DRScheduleDetail.projectID>
    {
    }

    public class ProjectTask : 
      PrimaryKeyOf<ContractTask>.By<ContractTask.contractID, ContractTask.taskID>.ForeignKeyOf<DRScheduleDetail>.By<DRScheduleDetail.projectID, DRScheduleDetail.taskID>
    {
    }
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleDetail.scheduleID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleDetail.branchID>
  {
  }

  public abstract class componentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleDetail.componentID>
  {
  }

  public abstract class parentInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DRScheduleDetail.parentInventoryID>
  {
  }

  public abstract class detailLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleDetail.detailLineNbr>
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRScheduleDetail.module>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRScheduleDetail.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRScheduleDetail.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleDetail.lineNbr>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  DRScheduleDetail.curyInfoID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRScheduleDetail.status>
  {
  }

  public abstract class totalAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DRScheduleDetail.totalAmt>
  {
  }

  public abstract class curyTotalAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRScheduleDetail.curyTotalAmt>
  {
  }

  public abstract class curyDefAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DRScheduleDetail.curyDefAmt>
  {
  }

  public abstract class defAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DRScheduleDetail.defAmt>
  {
  }

  public abstract class defCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRScheduleDetail.defCode>
  {
  }

  public abstract class defAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleDetail.defAcctID>
  {
  }

  public abstract class defSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleDetail.defSubID>
  {
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRScheduleDetail.receiptNbr>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRScheduleDetail.pONbr>
  {
  }

  public abstract class allowControlAccountForModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRScheduleDetail.allowControlAccountForModule>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleDetail.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleDetail.subID>
  {
  }

  public abstract class isOpen : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DRScheduleDetail.isOpen>
  {
  }

  public abstract class defTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DRScheduleDetail.defTotal>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleDetail.lineCntr>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  DRScheduleDetail.docDate>
  {
  }

  public abstract class tranPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRScheduleDetail.finPeriodID>
  {
  }

  public abstract class lastRecFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRScheduleDetail.lastRecFinPeriodID>
  {
  }

  public abstract class closeFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRScheduleDetail.closeFinPeriodID>
  {
  }

  public abstract class creditLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleDetail.creditLineNbr>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleDetail.bAccountID>
  {
  }

  public abstract class bAccountID_BAccount_acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRScheduleDetail.bAccountID_BAccount_acctName>
  {
  }

  public abstract class isCustom : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DRScheduleDetail.isCustom>
  {
  }

  public abstract class documentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRScheduleDetail.documentType>
  {
  }

  public abstract class bAccountType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRScheduleDetail.bAccountType>
  {
  }

  public abstract class defCodeType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRScheduleDetail.defCodeType>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DRScheduleDetail.selected>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleDetail.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleDetail.taskID>
  {
  }

  public abstract class isResidual : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DRScheduleDetail.isResidual>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRScheduleDetail.uOM>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DRScheduleDetail.baseQty>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DRScheduleDetail.qty>
  {
  }

  public abstract class fairValuePrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRScheduleDetail.fairValuePrice>
  {
  }

  public abstract class discountPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRScheduleDetail.discountPercent>
  {
  }

  public abstract class percentage : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DRScheduleDetail.percentage>
  {
    public const int SharePrecision = 5;
  }

  public abstract class coTermRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DRScheduleDetail.coTermRate>
  {
  }

  public abstract class effectiveFairValuePrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRScheduleDetail.effectiveFairValuePrice>
  {
  }

  public abstract class fairValueCuryID : IBqlField, IBqlOperand
  {
  }

  public abstract class allocationWeightResidual : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRScheduleDetail.allocationWeightResidual>
  {
  }

  public abstract class termStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DRScheduleDetail.termStartDate>
  {
  }

  public abstract class termEndDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DRScheduleDetail.termEndDate>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  DRScheduleDetail.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  DRScheduleDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRScheduleDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DRScheduleDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    DRScheduleDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRScheduleDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DRScheduleDetail.lastModifiedDateTime>
  {
  }
}
