// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRSchedule
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.Objects.PM;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.DR;

/// <summary>
/// The information about a deferred revenue or deferred expense
/// recognition schedule produced by an accounts payable
/// or an accounts receivable document line.
/// The entities of this type are usually created automatically upon the release
/// of the document line (such as <see cref="T:PX.Objects.AR.ARTran" />) that has a
/// <see cref="T:PX.Objects.DR.DRDeferredCode">deferral code</see> specified. A user can also
/// create a custom deferral schedule (or edit an existing one) by using the
/// Deferral Schedule (DR201500) form, which corresponds to the
/// <see cref="T:PX.Objects.DR.DraftScheduleMaint" /> graph.
/// </summary>
[DebuggerDisplay("SheduleID={ScheduleID} DocType={DocType} RefNbr={RefNbr}")]
[PXPrimaryGraph(typeof (DraftScheduleMaint))]
[PXCacheName("Deferral Schedule")]
[Serializable]
public class DRSchedule : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DocType;
  protected string _Module;
  protected string _RefNbr;
  protected int? _LineNbr;
  protected DateTime? _DocDate;
  protected string _FinPeriodID;
  protected int? _BAccountID;
  protected int? _BAccountLocID;
  protected string _TranDesc;
  protected bool? _IsCustom;
  protected bool? _IsDraft;
  protected string _DocumentType;
  protected string _BAccountType;
  protected Decimal? _OrigLineAmt;
  protected DateTime? _TermStartDate;
  protected DateTime? _TermEndDate;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The unique integer identifier of the deferral schedule.
  /// </summary>
  [PXDBIdentity]
  [PXFieldDescription]
  public virtual int? ScheduleID { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Schedule Number")]
  [AutoNumber(typeof (DRSetup.scheduleNumberingID), typeof (DRSchedule.docDate))]
  [PXParent(typeof (Select<ARTran, Where<ARTran.tranType, Equal<Current<DRSchedule.docType>>, And<ARTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<ARTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>, And<BatchModule.moduleAR, Equal<Current<DRSchedule.module>>>>>>>))]
  [PXParent(typeof (Select<APTran, Where<APTran.tranType, Equal<Current<DRSchedule.docType>>, And<APTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<APTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>, And<BatchModule.moduleAP, Equal<Current<DRSchedule.module>>>>>>>))]
  [PXSelector(typeof (DRSchedule.scheduleNbr), new System.Type[] {typeof (DRSchedule.scheduleNbr), typeof (DRSchedule.documentTypeEx), typeof (DRSchedule.refNbr), typeof (DRSchedule.bAccountID)})]
  public virtual string ScheduleNbr { get; set; }

  /// <summary>
  /// The type of the document that the deferral
  /// schedule corresponds to.
  /// </summary>
  /// <value>
  /// Corresponds to either the <see cref="P:PX.Objects.AR.ARTran.TranType" />
  /// field or the <see cref="P:PX.Objects.AP.APTran.TranType" /> field.
  /// This field can have one of the values defined
  /// by the <see cref="T:PX.Objects.AR.ARDocType" /> or <see cref="T:PX.Objects.AP.APDocType" /> class.
  /// </value>
  [PXDefault("INV")]
  [PXDBString(3, IsFixed = true)]
  [PXUIField(DisplayName = "Doc. Type", Enabled = true)]
  [PXFieldDescription]
  [ARDocType.List]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  /// <summary>
  /// The module from which the deferral schedule originates.
  /// </summary>
  /// <value>
  /// This field can have one of the following values:
  /// <c>"AR"</c>: Accounts Receivable,
  /// <c>"AP"</c>: Accounts Payable.
  /// If the module specified is Accounts Payable,
  /// the record is a deferred expense recognition schedule.
  /// If the module specified is Accounts Receivable,
  /// the record is a deferred revenue recognition schedule.
  /// The value of this field depends on the value of the
  /// <see cref="P:PX.Objects.DR.DRSchedule.DocumentTypeEx" /> field.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXDefault("AR")]
  [PXUIField(DisplayName = "Module")]
  [PXFormula(typeof (Switch<Case<Where<DRSchedule.docType, In3<ARDocType.invoice, ARDocType.creditMemo, ARDocType.debitMemo, ARDocType.cashSale, ARDocType.cashReturn>>, BatchModule.moduleAR, Case<Where<DRSchedule.docType, In3<APDocType.invoice, APDocType.creditAdj, APDocType.debitAdj, APDocType.quickCheck, APDocType.voidQuickCheck>>, BatchModule.moduleAP>>, BatchModule.moduleAR>))]
  [PXFieldDescription]
  public virtual string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  /// <summary>
  /// The reference number of the document that
  /// the deferral schedule corresponds to.
  /// </summary>
  /// <value>
  /// Corresponds to either the <see cref="P:PX.Objects.AR.ARTran.RefNbr" />
  /// or the <see cref="P:PX.Objects.AP.APTran.RefNbr" /> field.
  /// This field can be empty for custom deferral
  /// schedules that are not attached to any document.
  /// </value>
  [PXFieldDescription]
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [DRDocumentSelector(typeof (DRSchedule.module), typeof (DRSchedule.docType), typeof (DRSchedule.bAccountID))]
  [PXFormula(typeof (Default<DRSchedule.documentType>))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  /// <summary>
  /// The number of the document line, which produced the deferral schedule.
  /// </summary>
  /// <value>
  /// Corresponds to either the <see cref="P:PX.Objects.AR.ARTran.LineNbr" />
  /// or the <see cref="P:PX.Objects.AP.APTran.LineNbr" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Line Nbr.")]
  [DRLineSelector(typeof (DRSchedule.module), typeof (DRSchedule.docType), typeof (DRSchedule.refNbr))]
  [PXUIRequired(typeof (Where<DRSchedule.refNbr, IsNotNull, And<Where<DRSchedule.module, Equal<BatchModule.moduleAP>, Or<Where<DRSchedule.module, Equal<BatchModule.moduleAR>, And<Not<FeatureInstalled<FeaturesSet.aSC606>>>>>>>>))]
  [PXDefault]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  /// <summary>
  /// The date of the document, which contains the document line
  /// from which the deferral schedule originates.
  /// </summary>
  /// <value>
  /// Corresponds to either the <see cref="P:PX.Objects.AR.ARRegister.DocDate" />
  /// or the <see cref="P:PX.Objects.AP.APRegister.DocDate" /> field.
  /// For custom deferral schedules, which are not attached to a document line,
  /// the value of the field is specified by the user manually and
  /// defaults to the <see cref="P:PX.Data.AccessInfo.BusinessDate">current
  /// business date</see>.
  /// </value>
  [PXDBDate]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  [PXUIField(DisplayName = "Date", Enabled = false)]
  public virtual DateTime? DocDate
  {
    get => this._DocDate;
    set => this._DocDate = value;
  }

  /// <summary>
  /// The financial period of the document, which contains the
  /// document line from which the deferral schedule originates.
  /// </summary>
  /// <value>
  /// Corresponds to either the <see cref="P:PX.Objects.AR.ARRegister.FinPeriodID" />
  /// or the <see cref="P:PX.Objects.AP.APRegister.FinPeriodID" /> field.
  /// For custom deferral schedules, the value of this field
  /// is determined by the <see cref="P:PX.Objects.DR.DRSchedule.DocDate" /> field.
  /// </value>
  [PXDefault]
  [AROpenPeriod(typeof (DRSchedule.docDate), null, null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, IsHeader = true)]
  [PXUIField(DisplayName = "Fin. Period", Enabled = false)]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  /// <summary>
  /// The unique identifier of the <see cref="T:PX.Objects.AR.Customer" />
  /// or <see cref="T:PX.Objects.AP.Vendor" /> record, which is associated with
  /// the document line from which the deferral schedule originates.
  /// </summary>
  /// <value>
  /// Corresponds to either the <see cref="P:PX.Objects.AR.ARTran.CustomerID" />
  /// or the <see cref="P:PX.Objects.AP.APTran.VendorID" /> field.
  /// For custom deferral schedules, the value of this
  /// field is specified by the user.
  /// </value>
  [PXDBInt]
  [PXSelector(typeof (Search<BAccountR.bAccountID, Where<BAccountR.type, Equal<Current<DRSchedule.bAccountType>>, Or<PX.Objects.CR.BAccount.type, Equal<PX.Objects.CR.BAccountType.combinedType>, Or<Where<Current<DRSchedule.bAccountType>, Equal<PX.Objects.CR.BAccountType.vendorType>, And<BAccountR.type, Equal<PX.Objects.CR.BAccountType.employeeType>>>>>>>), new System.Type[] {typeof (BAccountR.acctCD), typeof (BAccountR.acctName), typeof (BAccountR.type)}, SubstituteKey = typeof (BAccountR.acctCD), DescriptionField = typeof (BAccountR.acctName))]
  [PXUIField]
  [PXDefault]
  [PXFormula(typeof (Default<DRSchedule.documentType>))]
  [PXForeignReference(typeof (Field<DRSchedule.bAccountID>.IsRelatedTo<BAccountR.bAccountID>))]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  [PXDefault(typeof (Search<BAccountR.defLocationID, Where<BAccountR.bAccountID, Equal<Current<DRSchedule.bAccountID>>>>))]
  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<DRSchedule.bAccountID>>>))]
  public virtual int? BAccountLocID
  {
    get => this._BAccountLocID;
    set => this._BAccountLocID = value;
  }

  /// <summary>
  /// The base <see cref="T:PX.Objects.CM.Currency" /> of the Branch.
  /// </summary>
  /// <value>
  /// This unbound field corresponds to the <see cref="!:Organization.BaseCuryID" />.
  /// </value>
  [PXDBString(5, IsUnicode = true)]
  [PXSelector(typeof (Search<CurrencyList.curyID>))]
  [PXRestrictor(typeof (Where<CurrencyList.curyID, IsBaseCurrency>), "The currency cannot be selected as a schedule currency, because there is no branch with the same base currency in the system.", new System.Type[] {})]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXUIField(DisplayName = "Currency")]
  public virtual string BaseCuryID { get; set; }

  /// <summary>
  /// The description of the document line from which the
  /// deferral schedule originates.
  /// </summary>
  /// <value>
  /// Corresponds to either the <see cref="P:PX.Objects.AR.ARTran.TranDesc" />
  /// or <see cref="P:PX.Objects.AP.APTran.TranDesc" /> field.
  /// </value>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the deferral schedule
  /// has been created manually by the user.
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
  /// Indicates (if set to <c>true</c>) that the deferral schedule
  /// is in draft mode, which allows the user to add and edit
  /// <see cref="T:PX.Objects.DR.DRScheduleDetail">schedule components</see>.
  /// This flag is reset to <c>false</c> after the release of
  /// any schedule components.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Draft", Visible = false)]
  public virtual bool? IsDraft
  {
    get => this._IsDraft;
    set => this._IsDraft = value;
  }

  /// <summary>
  /// 
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override", Visible = false)]
  public virtual bool? IsOverridden { get; set; }

  /// <summary>
  /// The number of <see cref="T:PX.Objects.DR.DRScheduleDetail">components
  /// </see> associated with the deferral schedule. When a component
  /// is added to the deferral schedule, this field provides the value for
  /// <see cref="P:PX.Objects.DR.DRScheduleDetail.DetailLineNbr" />, being incremented after
  /// each added component.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? DetailLineCntr { get; set; }

  /// <summary>
  /// The unique identifier of the <see cref="T:PX.Objects.PM.PMProject">
  /// project</see> associated with the schedule.
  /// This field affects the way <see cref="P:PX.Objects.DR.DRScheduleDetail.SubID">
  /// deferral components' subaccounts</see> are calculated during
  /// schedule creation. For details, see <see cref="T:PX.Objects.DR.ScheduleCreator" />.
  /// </summary>
  [ProjectDefault]
  [PXRestrictor(typeof (Where<PMProject.status, NotEqual<ProjectStatus.closed>>), "The {0} project is closed.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXDimensionSelector("PROJECT", typeof (Search<PMProject.contractID, Where<PMProject.baseType, Equal<CTPRType.project>, Or<PMProject.baseType, Equal<CTPRType.contract>>>>), typeof (PMProject.contractCD), new System.Type[] {typeof (PMProject.contractCD), typeof (PMProject.customerID), typeof (PMProject.description), typeof (PMProject.status)}, DescriptionField = typeof (PMProject.description))]
  [PXDBInt]
  [PXUIField]
  [PXFormula(typeof (Default<DRSchedule.documentType>))]
  [PXUIVerify]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// The unique identifier of the <see cref="T:PX.Objects.PM.PMTask">project
  /// task</see> associated with the schedule.
  /// </summary>
  [ProjectTask(typeof (DRSchedule.projectID), DisplayName = "Project Task", AllowNullIfContract = true)]
  [PXFormula(typeof (Default<DRSchedule.documentType>))]
  public virtual int? TaskID { get; set; }

  /// <summary>
  /// The code of the <see cref="T:PX.Objects.CM.Currency" /> of the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:Currency.CuryID" /> field.
  /// </value>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXDefault(typeof (Search<ARRegister.curyID, Where<ARRegister.docType, Equal<Current<DRSchedule.docType>>, And<ARRegister.refNbr, Equal<Current<DRSchedule.refNbr>>>>>))]
  [PXUIField(DisplayName = "Doc. Currency", Enabled = false, Visible = false)]
  public virtual string CuryID { get; set; }

  [PXDBLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>
  /// An extension of the <see cref="P:PX.Objects.DR.DRSchedule.DocType" />
  /// field that is used to disambiguate between
  /// Accounts Payable bills and Accounts Receivable
  /// invoices, both of which have the <c>"INV"</c>
  /// document type value.
  /// </summary>
  /// <value>
  /// This field can take one of the values defined
  /// by <see cref="T:PX.Objects.DR.DRScheduleDocumentType.ListAttribute" />.
  /// </value>
  [PXString(3, IsFixed = true)]
  [DRScheduleDocumentType.List]
  [PXUIField(DisplayName = "Doc. Type", Enabled = false, Required = true)]
  public virtual string DocumentType
  {
    get => this._DocumentType;
    set => this._DocumentType = value;
  }

  /// <summary>
  /// The type of the <see cref="T:PX.Objects.CR.BAccount">business account</see>
  /// defined by the <see cref="P:PX.Objects.DR.DRSchedule.BAccountID" /> field.
  /// </summary>
  /// <value>
  /// This field can have one of the following values:
  /// <c>"VE": Vendor</c>,
  /// <c>"CU": Customer</c>.
  /// </value>
  [PXUIField(DisplayName = "Entity Type")]
  [PXUnboundDefault("CU")]
  [PXString(2, IsFixed = true)]
  [PXStringList(new string[] {"VE", "CU", "EP"}, new string[] {"Vendor", "Customer", "Employee"})]
  [PXFormula(typeof (IIf<Where<DRSchedule.module, Equal<BatchModule.moduleAR>>, PX.Objects.CR.BAccountType.customerType, PX.Objects.CR.BAccountType.vendorType>))]
  public virtual string BAccountType
  {
    get => this._BAccountType;
    set => this._BAccountType = value;
  }

  /// <summary>
  /// The original amount of the document line from which the
  /// deferral schedule originates.
  /// </summary>
  /// <value>
  /// Corresponds to either the <see cref="P:PX.Objects.AR.ARTran.TranAmt" />
  /// or the <see cref="P:PX.Objects.AP.APTran.TranAmt" /> field.
  /// For custom schedules, which do not have a reference
  /// to a document line, this field has the <c>null</c>
  /// value.
  /// </value>
  [PXBaseCury]
  [PXUIField(DisplayName = "Line Amount", Enabled = false)]
  public virtual Decimal? OrigLineAmt
  {
    get => this._OrigLineAmt;
    set => this._OrigLineAmt = value;
  }

  [PXCurrency(typeof (DRSchedule.curyInfoID), typeof (DRSchedule.netTranPrice))]
  [PXUIField(DisplayName = "Net Tran. Price", Enabled = false, Visible = false)]
  public virtual Decimal? CuryNetTranPrice { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Base Net Tran. Price", Enabled = false, Visible = false)]
  public virtual Decimal? NetTranPrice { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Comp. Total", Enabled = false, Visible = false)]
  public virtual Decimal? ComponentsTotal { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Comp.  Deferred", Enabled = false, Visible = false)]
  public virtual Decimal? DefTotal { get; set; }

  /// <summary>
  /// A human-readable representation of the source document type.
  /// </summary>
  [PXString(3, IsFixed = true)]
  [DRScheduleDocumentType.List]
  [PXUIField]
  public virtual string DocumentTypeEx
  {
    [PXDependsOnFields(new System.Type[] {typeof (DRSchedule.module), typeof (DRSchedule.docType)})] get
    {
      return DRScheduleDocumentType.BuildDocumentType(this.Module, this.DocType);
    }
  }

  /// <summary>
  /// Defines the term start date for <see cref="T:PX.Objects.DR.DRScheduleDetail">
  /// deferral components</see> that have a flexible deferral code
  /// specified.
  /// </summary>
  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Term Start Date")]
  public virtual DateTime? TermStartDate
  {
    get => this._TermStartDate;
    set => this._TermStartDate = value;
  }

  /// <summary>
  /// Defines the term end date for <see cref="T:PX.Objects.DR.DRScheduleDetail">
  /// deferral components</see> that have a flexible deferral code
  /// specified.
  /// </summary>
  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Term End Date")]
  public virtual DateTime? TermEndDate
  {
    get => this._TermEndDate;
    set => this._TermEndDate = value;
  }

  /// <summary>The status of the deferral schedule.</summary>
  /// <value>
  /// This field can have one of the values defined by
  /// <see cref="T:PX.Objects.DR.DRScheduleStatus.ListAttribute" />.
  /// </value>
  [PXString]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  [DRScheduleStatus.List]
  public string Status { get; set; }

  [PXBool]
  [PXFormula(typeof (IIf<Where2<FeatureInstalled<FeaturesSet.aSC606>, And<DRSchedule.module, Equal<BatchModule.moduleAR>>>, True, False>))]
  public bool? IsPoolVisible { get; set; }

  [PXBool]
  [PXDefault(false)]
  public bool? IsRecalculated { get; set; }

  [PXBool]
  [PXDefault(false)]
  public bool? IsSuspense { get; set; }

  [PXNote(DescriptionField = typeof (DRSchedule.scheduleID))]
  public virtual Guid? NoteID { get; set; }

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

  public class PK : PrimaryKeyOf<DRSchedule>.By<DRSchedule.scheduleID>
  {
    public static DRSchedule Find(PXGraph graph, int? scheduleID, PKFindOptions options = 0)
    {
      return (DRSchedule) PrimaryKeyOf<DRSchedule>.By<DRSchedule.scheduleID>.FindBy(graph, (object) scheduleID, options);
    }
  }

  public class UK : PrimaryKeyOf<DRSchedule>.By<DRSchedule.scheduleNbr>
  {
    public static DRSchedule Find(PXGraph graph, string scheduleNbr, PKFindOptions options = 0)
    {
      return (DRSchedule) PrimaryKeyOf<DRSchedule>.By<DRSchedule.scheduleNbr>.FindBy(graph, (object) scheduleNbr, options);
    }
  }

  public static class FK
  {
    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<DRSchedule>.By<DRSchedule.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<DRSchedule>.By<DRSchedule.curyID>
    {
    }

    public class BusinessAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<DRSchedule>.By<DRSchedule.bAccountID>
    {
    }

    public class Location : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<DRSchedule>.By<DRSchedule.bAccountID, DRSchedule.bAccountLocID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<DRSchedule>.By<DRSchedule.projectID>
    {
    }

    public class ProjectTask : 
      PrimaryKeyOf<ContractTask>.By<ContractTask.contractID, ContractTask.taskID>.ForeignKeyOf<DRSchedule>.By<DRSchedule.projectID, DRSchedule.taskID>
    {
    }
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRSchedule.scheduleID>
  {
  }

  public abstract class scheduleNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRSchedule.scheduleNbr>
  {
  }

  public abstract class docType : IBqlField, IBqlOperand
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRSchedule.module>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRSchedule.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRSchedule.lineNbr>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  DRSchedule.docDate>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRSchedule.finPeriodID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRSchedule.bAccountID>
  {
  }

  public abstract class bAccountLocID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRSchedule.bAccountLocID>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRSchedule.baseCuryID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRSchedule.tranDesc>
  {
  }

  public abstract class isCustom : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DRSchedule.isCustom>
  {
  }

  public abstract class isDraft : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DRSchedule.isDraft>
  {
  }

  public abstract class isOverridden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DRSchedule.isOverridden>
  {
  }

  public abstract class detailLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRSchedule.detailLineCntr>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRSchedule.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRSchedule.taskID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRSchedule.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  DRSchedule.curyInfoID>
  {
  }

  public abstract class documentType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRSchedule.documentType>
  {
  }

  public abstract class bAccountType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRSchedule.bAccountType>
  {
  }

  public abstract class origLineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DRSchedule.origLineAmt>
  {
  }

  public abstract class curyNetTranPrice : IBqlField, IBqlOperand
  {
  }

  public abstract class netTranPrice : IBqlField, IBqlOperand
  {
  }

  public abstract class componentsTotal : IBqlField, IBqlOperand
  {
  }

  public abstract class defTotal : IBqlField, IBqlOperand
  {
  }

  public abstract class documentTypeEx : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRSchedule.documentTypeEx>
  {
  }

  public abstract class termStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DRSchedule.termStartDate>
  {
  }

  public abstract class termEndDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  DRSchedule.termEndDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRSchedule.status>
  {
  }

  public abstract class isPoolVisible : IBqlField, IBqlOperand
  {
  }

  public abstract class isRecalculated : IBqlField, IBqlOperand
  {
  }

  public abstract class isSuspense : IBqlField, IBqlOperand
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  DRSchedule.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  DRSchedule.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  DRSchedule.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRSchedule.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DRSchedule.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  DRSchedule.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRSchedule.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DRSchedule.lastModifiedDateTime>
  {
  }
}
