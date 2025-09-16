// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSServiceOrder
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.TX;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Service Order")]
[PXPrimaryGraph(typeof (ServiceOrderEntry))]
[PXGroupMask(typeof (LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>), WhereRestriction = typeof (Where<PX.Objects.AR.Customer.bAccountID, IsNotNull, Or<FSServiceOrder.customerID, IsNull>>))]
[Serializable]
public class FSServiceOrder : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SrvOrdAddressID;
  protected int? _SrvOrdContactID;
  protected bool? _AllowOverrideContactAddress;
  private Decimal? _CuryBillableOrderTotal;
  protected DateTime? _SLAETA;
  protected int? _MaxLineNbr;
  protected Decimal? _CuryDocDisc;
  protected Decimal? _DocDisc;
  protected Decimal? _CuryDiscTot;
  protected Decimal? _DiscTot;

  [PXDBString(4, IsKey = true, IsFixed = true, InputMask = ">AAAA")]
  [PXUIField]
  [PXDefault(typeof (Coalesce<Search<FSxUserPreferences.dfltSrvOrdType, Where<BqlOperand<UserPreferences.userID, IBqlGuid>.IsEqual<BqlField<AccessInfo.userID, IBqlGuid>.FromCurrent>>>, Search<FSSetup.dfltSrvOrdType>>))]
  [FSSelectorSrvOrdType]
  [PXRestrictor(typeof (Where<FSSrvOrdType.active, Equal<True>>), null, new System.Type[] {})]
  [PXUIVerify]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [FSSelectorSORefNbr]
  [ServiceOrderAutoNumber(typeof (Search<FSSrvOrdType.srvOrdNumberingID, Where<FSSrvOrdType.srvOrdType, Equal<Optional<FSServiceOrder.srvOrdType>>>>), typeof (AccessInfo.businessDate))]
  [PXFieldDescription]
  public virtual string RefNbr { get; set; }

  [PXDBIdentity]
  public virtual int? SOID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault(typeof (Selector<FSServiceOrder.srvOrdType, FSSrvOrdType.serviceOrderWorkflowTypeID>))]
  [PXFormula(typeof (Default<FSServiceOrder.srvOrdType>))]
  [ListField.ServiceOrderWorkflowTypes.List]
  [PXUIField(DisplayName = "Workflow Type", Enabled = false)]
  public virtual string WorkflowTypeID { get; set; }

  /// <summary>
  /// A service field, which is necessary for the <see cref="T:PX.Objects.CS.CSAnswers">dynamically
  /// added attributes</see> defined at the <see cref="T:PX.Objects.FS.FSSrvOrdType">customer
  /// class</see> level to function correctly.
  /// </summary>
  [CRAttributesField(typeof (FSServiceOrder.srvOrdType), typeof (FSServiceOrder.noteID))]
  public virtual string[] Attributes { get; set; }

  [PXDBInt]
  [FSSrvOrdAddress(typeof (Select<PX.Objects.CR.Address, Where<True, Equal<False>>>))]
  public virtual int? ServiceOrderAddressID
  {
    get => this._SrvOrdAddressID;
    set => this._SrvOrdAddressID = value;
  }

  [PXDBInt]
  [FSSrvOrdContact(typeof (Select<PX.Objects.CR.Contact, Where<True, Equal<False>>>))]
  public virtual int? ServiceOrderContactID
  {
    get => this._SrvOrdContactID;
    set => this._SrvOrdContactID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? AllowOverrideContactAddress
  {
    get => this._AllowOverrideContactAddress;
    set => this._AllowOverrideContactAddress = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Billing", Enabled = false)]
  public virtual bool? AllowInvoice { get; set; }

  [PXDBInt]
  [FSSelector_StaffMember_All(null)]
  [PXUIField(DisplayName = "Supervisor")]
  public virtual int? AssignedEmpID { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Service description", Visible = true, Enabled = false)]
  public virtual string AutoDocDesc { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField]
  [PXRestrictor(typeof (Where<PX.Objects.CR.BAccount.status, IsNull, Or<PX.Objects.CR.BAccount.status, Equal<CustomerStatus.active>, Or<PX.Objects.CR.BAccount.status, Equal<CustomerStatus.prospect>, Or<PX.Objects.CR.BAccount.status, Equal<CustomerStatus.oneTime>>>>>), "The customer status is '{0}'.", new System.Type[] {typeof (PX.Objects.CR.BAccount.status)})]
  [FSSelectorBusinessAccount_CU_PR_VC]
  [PXForeignReference(typeof (FSServiceOrder.FK.Customer))]
  public virtual int? CustomerID { get; set; }

  [FSLocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSServiceOrder.customerID>>, And<MatchWithBranch<PX.Objects.CR.Location.cBranchID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Location", DirtyRead = true)]
  [PXDefault(typeof (Coalesce<Search2<BAccountR.defLocationID, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>>, Where<BAccountR.bAccountID, Equal<Current<FSServiceOrder.customerID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.cBranchID>>>>>, Search<PX.Objects.CR.Standalone.Location.locationID, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<FSServiceOrder.customerID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.cBranchID>>>>>>))]
  [PXForeignReference(typeof (FSServiceOrder.FK.CustomerLocation))]
  public virtual int? LocationID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Billing Customer")]
  [FSCustomer]
  [PXForeignReference(typeof (FSServiceOrder.FK.BillCustomer))]
  [PXUIEnabled(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<FSServiceOrder.bAccountRequired>, Equal<True>>>>>.And<BqlOperand<Current<FSServiceOrder.billServiceContractID>, IBqlInt>.IsNull>>))]
  public virtual int? BillCustomerID { get; set; }

  [FSLocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSServiceOrder.billCustomerID>>, And<MatchWithBranch<PX.Objects.CR.Location.cBranchID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Billing Location", DirtyRead = true)]
  [PXUIEnabled(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<FSServiceOrder.bAccountRequired>, Equal<True>>>>>.And<BqlOperand<Current<FSServiceOrder.billServiceContractID>, IBqlInt>.IsNull>>))]
  [PXForeignReference(typeof (FSServiceOrder.FK.BillCustomerLocation))]
  public virtual int? BillLocationID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string DocDesc { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Contact")]
  [FSSelectorContact(typeof (FSServiceOrder.customerID))]
  public virtual int? ContactID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Contract", Enabled = false)]
  [FSSelectorContract]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.status, Equal<ListField_Status_ServiceContract.Active>>), "Restrictor 1", new System.Type[] {})]
  [PXRestrictor(typeof (Where<Current<AccessInfo.businessDate>, LessEqual<PX.Objects.CT.Contract.graceDate>, Or<PX.Objects.CT.Contract.expireDate, IsNull>>), "Restrictor 2", new System.Type[] {})]
  [PXRestrictor(typeof (Where<Current<AccessInfo.businessDate>, GreaterEqual<PX.Objects.CT.Contract.startDate>>), "Restrictor 3", new System.Type[] {typeof (PX.Objects.CT.Contract.startDate)})]
  public virtual int? ContractID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (AccessInfo.branchID))]
  [PXUIField(DisplayName = "Branch")]
  [PXSelector(typeof (Search<PX.Objects.GL.Branch.branchID>), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public virtual int? BranchID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Search<FSxUserPreferences.dfltBranchLocationID, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>, And<UserPreferences.defBranchID, Equal<Current<FSServiceOrder.branchID>>>>>))]
  [PXUIField(DisplayName = "Branch Location")]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<FSServiceOrder.branchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  [PXFormula(typeof (Default<FSServiceOrder.branchID>))]
  public virtual int? BranchLocationID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Room")]
  [PXSelector(typeof (Search<FSRoom.roomID, Where<FSRoom.branchLocationID, Equal<Current<FSServiceOrder.branchLocationID>>>>), SubstituteKey = typeof (FSRoom.roomID), DescriptionField = typeof (FSRoom.descr))]
  public virtual string RoomID { get; set; }

  [PXDBDate(DisplayMask = "d")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? OrderDate { get; set; }

  [PXBool]
  public virtual bool? UserConfirmedClosing { get; set; }

  [PXBool]
  public virtual bool? UserConfirmedUnclosing { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copied", Enabled = false)]
  public virtual bool? Copied { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Confirmed", Enabled = false)]
  public virtual bool? Confirmed { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Open", Enabled = false)]
  public virtual bool? OpenDoc { get; set; }

  [PXBool]
  public virtual bool? ProcessReopenAction { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Hold", Enabled = false)]
  public virtual bool? Hold { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Awaiting", Enabled = false)]
  public virtual bool? Awaiting { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Completed", Enabled = false)]
  public virtual bool? Completed { get; set; }

  [PXBool]
  public virtual bool? ProcessCompleteAction { get; set; }

  [PXBool]
  public virtual bool? CompleteAppointments { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Closed", Enabled = false)]
  public virtual bool? Closed { get; set; }

  [PXBool]
  public virtual bool? ProcessCloseAction { get; set; }

  [PXBool]
  public virtual bool? CloseAppointments { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Canceled", Enabled = false)]
  public virtual bool? Canceled { get; set; }

  [PXBool]
  public virtual bool? ProcessCancelAction { get; set; }

  [PXBool]
  public virtual bool? CancelAppointments { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Billed", Enabled = false)]
  public virtual bool? Billed { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Billing By", Enabled = false)]
  [ListField_Billing_By.ListAtrribute]
  [PXDefault]
  public virtual string BillingBy { get; set; }

  /// <summary>Non-used field</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Bill Only Completed or Closed Service Orders")]
  public virtual bool? BillOnlyCompletedClosed { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? CompleteActionRunning { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? CancelActionRunning { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? ReopenActionRunning { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? CloseActionRunning { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? UnCloseActionRunning { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [ListField.ServiceOrderStatus.List]
  [PXDefault("O")]
  public virtual string Status { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Workflow Stage")]
  [FSSelectorWorkflowStage(typeof (FSServiceOrder.srvOrdType))]
  [PXDefault(typeof (Search2<FSWFStage.wFStageID, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdTypeID, Equal<FSWFStage.wFID>>>, Where<FSSrvOrdType.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>>, OrderBy<Asc<FSWFStage.parentWFStageID, Asc<FSWFStage.sortOrder>>>>))]
  [PXUIVisible(typeof (BqlOperand<Current<FSSetup.showWorkflowStageField>, IBqlBool>.IsEqual<True>))]
  public virtual int? WFStageID { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  [PXUIField]
  public virtual string CuryID { get; set; }

  [PXDBLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  [ProjectDefault]
  [ProjectBase(typeof (FSServiceOrder.billCustomerID))]
  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.isCancelled, Equal<False>>), "The {0} project or contract is canceled.", new System.Type[] {typeof (PMProject.contractCD)})]
  public virtual int? ProjectID { get; set; }

  [PXDBInt]
  [PXUIField]
  [PXFormula(typeof (Default<FSServiceOrder.projectID>))]
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<FSServiceOrder.projectID>>, And<PMTask.isDefault, Equal<True>, And<Where<PMTask.status, Equal<ProjectTaskStatus.active>, Or<PMTask.status, Equal<ProjectTaskStatus.planned>>>>>>>))]
  [FSSelectorActive_AR_SO_ProjectTask(typeof (Where<PMTask.projectID, Equal<Current<FSServiceOrder.projectID>>>))]
  public virtual int? DfltProjectTaskID { get; set; }

  [PXDBTimeSpanLong]
  [PXUIField(DisplayName = "Estimated Duration", Enabled = false)]
  [PXDefault(0)]
  public virtual int? EstimatedDurationTotal { get; set; }

  [PXDBString(2147483647 /*0x7FFFFFFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string LongDescr { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Ext. Price Total", Enabled = false)]
  public virtual Decimal? EstimatedOrderTotal { get; set; }

  [PXDBCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.estimatedOrderTotal))]
  [PXUIField(DisplayName = "Ext. Price Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryEstimatedOrderTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Estimated Billable Total", Enabled = false)]
  public virtual Decimal? BillableOrderTotal { get; set; }

  [PXDBCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.billableOrderTotal))]
  [PXUIField(DisplayName = "Estimated Billable Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryBillableOrderTotal
  {
    get => this._CuryBillableOrderTotal;
    set => this._CuryBillableOrderTotal = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("M")]
  [PXUIField]
  [ListField_Priority_ServiceOrder.ListAtrribute]
  public virtual string Priority { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Problem")]
  [PXSelector(typeof (Search2<FSProblem.problemID, InnerJoin<FSSrvOrdTypeProblem, On<FSProblem.problemID, Equal<FSSrvOrdTypeProblem.problemID>>, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSSrvOrdTypeProblem.srvOrdType>>>>, Where<FSSrvOrdType.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>>>), SubstituteKey = typeof (FSProblem.problemCD), DescriptionField = typeof (FSProblem.descr))]
  public virtual int? ProblemID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("M")]
  [PXUIField]
  [ListField_Severity_ServiceOrder.ListAtrribute]
  public virtual string Severity { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "SLA")]
  [PXUIField(DisplayName = "SLA")]
  public virtual DateTime? SLAETA
  {
    get => this._SLAETA;
    set
    {
      this.SLAETAUTC = value;
      this._SLAETA = value;
    }
  }

  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Source Document Type", Enabled = false)]
  public virtual string SourceDocType { get; set; }

  [PXDBInt]
  public virtual int? SourceID { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Source Ref. Nbr.", Enabled = false)]
  public virtual string SourceRefNbr { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("SD")]
  [PXUIField(DisplayName = "Document Type", Enabled = false)]
  [ListField_SourceType_ServiceOrder.ListAtrribute]
  public virtual string SourceType { get; set; }

  [PXNote(ShowInReferenceSelector = true)]
  [PXSearchable(8192 /*0x2000*/, "SM {0}: {1} - {3}", new System.Type[] {typeof (FSServiceOrder.srvOrdType), typeof (FSServiceOrder.refNbr), typeof (FSServiceOrder.customerID), typeof (PX.Objects.AR.Customer.acctName)}, new System.Type[] {typeof (PX.Objects.AR.Customer.acctCD), typeof (FSServiceOrder.srvOrdType), typeof (FSServiceOrder.custWorkOrderRefNbr), typeof (FSServiceOrder.docDesc)}, NumberFields = new System.Type[] {typeof (FSServiceOrder.refNbr)}, Line1Format = "{0:d}{1}{2}", Line1Fields = new System.Type[] {typeof (FSServiceOrder.orderDate), typeof (FSServiceOrder.status), typeof (FSServiceOrder.custWorkOrderRefNbr)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (FSServiceOrder.docDesc)}, MatchWithJoin = typeof (InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSServiceOrder.srvOrdType>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>>), WhereConstraint = typeof (Where<FSServiceOrder.customerID, IsNotNull, Or<Where<FSSrvOrdType.behavior, Equal<ListField.ServiceOrderTypeBehavior.internalAppointment>>>>), SelectForFastIndexing = typeof (Select2<FSServiceOrder, InnerJoin<PX.Objects.AR.Customer, On<FSServiceOrder.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>>))]
  public virtual Guid? NoteID { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? SplitLineCntr { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "Created By Screen ID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "Last Modified By Screen ID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBBool]
  [PXDefault(typeof (Selector<FSServiceOrder.srvOrdType, FSSrvOrdType.bAccountRequired>))]
  [PXUIField(DisplayName = "Customer Required", Enabled = false)]
  public virtual bool? BAccountRequired { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Quote", Enabled = false)]
  public virtual bool? Quote { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<FSServiceContract.serviceContractID, Where<FSServiceContract.customerID, Equal<Current<FSServiceOrder.customerID>>>>), SubstituteKey = typeof (FSServiceContract.refNbr))]
  [PXUIField(DisplayName = "Source Service Contract ID", Enabled = false, FieldClass = "FSCONTRACT")]
  public virtual int? ServiceContractID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<FSSchedule.scheduleID, Where<FSSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>, And<FSSchedule.entityID, Equal<Current<FSServiceOrder.serviceContractID>>>>>), SubstituteKey = typeof (FSSchedule.refNbr))]
  [PXUIField(DisplayName = "Source Schedule ID", Enabled = false, FieldClass = "FSCONTRACT")]
  public virtual int? ScheduleID { get; set; }

  [PXDBString(6, IsFixed = true)]
  [PXUIField(DisplayName = "Post Period")]
  public virtual string FinPeriodID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Generation ID")]
  public virtual int? GenerationID { get; set; }

  [PXDBString(40, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
  [NormalizeWhiteSpace]
  [PXUIField(DisplayName = "External Reference")]
  public virtual string CustWorkOrderRefNbr { get; set; }

  [PXDBString(40, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
  [NormalizeWhiteSpace]
  [PXUIField(DisplayName = "Customer Order")]
  public virtual string CustPORefNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Service Count", Enabled = false)]
  public virtual int? ServiceCount { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Scheduled Service Count", Enabled = false)]
  public virtual int? ScheduledServiceCount { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Complete Service Count", Enabled = false)]
  public virtual int? CompleteServiceCount { get; set; }

  [PXDBString(2, IsFixed = true)]
  public virtual string PostedBy { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? PendingAPARSOPost { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? PendingINPost { get; set; }

  /// <summary>Non-used field</summary>
  [PXDBInt]
  public virtual int? CBID { get; set; }

  [SalesPerson(DisplayName = "Salesperson")]
  [PXUIEnabled(typeof (Where<Current<FSSrvOrdType.behavior>, NotEqual<ListField.ServiceOrderTypeBehavior.internalAppointment>>))]
  [PXDefault(typeof (Search<CustDefSalesPeople.salesPersonID, Where<CustDefSalesPeople.bAccountID, Equal<Current<FSServiceOrder.customerID>>, And<CustDefSalesPeople.locationID, Equal<Current<FSServiceOrder.locationID>>, And<CustDefSalesPeople.isDefault, Equal<True>>>>>))]
  [PXFormula(typeof (Default<FSServiceOrder.customerID>))]
  [PXFormula(typeof (Default<FSServiceOrder.locationID>))]
  [PXForeignReference(typeof (FSServiceOrder.FK.SalesPerson))]
  public virtual int? SalesPersonID { get; set; }

  [PXDBBool]
  [PXUIEnabled(typeof (Where<Current<FSSrvOrdType.behavior>, NotEqual<ListField.ServiceOrderTypeBehavior.internalAppointment>>))]
  [PXDefault(typeof (Search<FSSrvOrdType.commissionable, Where<FSSrvOrdType.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>>>))]
  [PXUIField(DisplayName = "Commissionable")]
  public virtual bool? Commissionable { get; set; }

  /// <summary>Non-used field</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Cut-Off Date")]
  [PXDefault]
  public virtual DateTime? CutOffDate { get; set; }

  [PXDBTimeSpanLong]
  [PXUIField(DisplayName = "Appointment Duration", Enabled = false)]
  [PXDefault(0)]
  public virtual int? ApptDurationTotal { get; set; }

  [PXDBCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.apptOrderTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Billable Total", Enabled = false)]
  public virtual Decimal? CuryApptOrderTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Appointment Line Total", Enabled = false)]
  public virtual Decimal? ApptOrderTotal { get; set; }

  [PXCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.appointmentTaxTotal))]
  [PXUIField(DisplayName = "Actual Tax Total", Enabled = false)]
  public virtual Decimal? CuryAppointmentTaxTotal { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Base Appointment Tax Total", Enabled = false)]
  public virtual Decimal? AppointmentTaxTotal { get; set; }

  [PXCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.appointmentDocTotal))]
  [PXUIField(DisplayName = "Invoice Total", Enabled = false)]
  public virtual Decimal? CuryAppointmentDocTotal { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Base Invoice Total", Enabled = false)]
  public virtual Decimal? AppointmentDocTotal { get; set; }

  [PXDBInt]
  [PXDefault]
  [FSSelectorPPFRServiceContract(typeof (FSServiceOrder.customerID), typeof (FSServiceOrder.locationID))]
  [PXUIField(DisplayName = "Service Contract", FieldClass = "FSCONTRACT")]
  public virtual int? BillServiceContractID { get; set; }

  [PXDBInt]
  [FSSelectorContractBillingPeriod]
  [PXDefault(typeof (Search2<FSContractPeriod.contractPeriodID, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSContractPeriod.serviceContractID>>>, Where<FSContractPeriod.startPeriodDate, LessEqual<Current<FSServiceOrder.orderDate>>, And<FSContractPeriod.endPeriodDate, GreaterEqual<Current<FSServiceOrder.orderDate>>, And<FSContractPeriod.serviceContractID, Equal<Current<FSServiceOrder.billServiceContractID>>, And2<Where2<Where<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Active>, Or<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Pending>>>, Or<Where<FSServiceContract.isFixedRateContract, Equal<True>, And<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Invoiced>>>>>, And<Current<FSBillingCycle.billingBy>, Equal<ListField_Billing_By.ServiceOrder>>>>>>>))]
  [PXFormula(typeof (Default<FSServiceOrder.billCustomerID>))]
  [PXUIField(DisplayName = "Contract Period", Enabled = false)]
  public virtual int? BillContractPeriodID { get; set; }

  [PXCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.effectiveBillableLineTotal))]
  [PXUIField(DisplayName = "Line Total", Enabled = false)]
  public virtual Decimal? CuryEffectiveBillableLineTotal { get; set; }

  [PXDecimal]
  public virtual Decimal? EffectiveBillableLineTotal { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.effectiveLogBillableTranAmountTotal))]
  [PXUIField(DisplayName = "Billable Labor Total", Enabled = false)]
  public virtual Decimal? CuryEffectiveLogBillableTranAmountTotal { get; set; }

  [PXDecimal]
  public virtual Decimal? EffectiveLogBillableTranAmountTotal { get; set; }

  [PXCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.effectiveBillableTaxTotal))]
  [PXUIField(DisplayName = "Tax Total", Enabled = false)]
  public virtual Decimal? CuryEffectiveBillableTaxTotal { get; set; }

  [PXDecimal]
  public virtual Decimal? EffectiveBillableTaxTotal { get; set; }

  [PXCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.effectiveBillableDocTotal))]
  [PXUIField(DisplayName = "Invoice Total", Enabled = false)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryEffectiveBillableDocTotal { get; set; }

  [PXDecimal]
  public virtual Decimal? EffectiveBillableDocTotal { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Billable Total", Enabled = false)]
  public virtual Decimal? CuryShortLabelEffectiveBillableDocTotal
  {
    get => this.CuryEffectiveBillableDocTotal;
  }

  [PXDecimal]
  public virtual Decimal? ShortLabelEffectiveBillableDocTotal => this.EffectiveBillableDocTotal;

  [PXCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.effectiveCostTotal))]
  [PXUIField(DisplayName = "Cost Total", Enabled = false)]
  public virtual Decimal? CuryEffectiveCostTotal { get; set; }

  [PXDecimal]
  public virtual Decimal? EffectiveCostTotal { get; set; }

  [PXCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.sOUnpaidBalanace))]
  [PXUIField(DisplayName = "Service Order Unpaid Balance", Enabled = false)]
  public virtual Decimal? SOCuryUnpaidBalanace { get; set; }

  [PXDecimal]
  public virtual Decimal? SOUnpaidBalanace { get; set; }

  [PXCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.sOBillableUnpaidBalanace))]
  [PXUIField(DisplayName = "Service Order Billable Unpaid Balance", Enabled = false)]
  public virtual Decimal? SOCuryBillableUnpaidBalanace { get; set; }

  [PXDecimal]
  public virtual Decimal? SOBillableUnpaidBalanace { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Prepayment Received", Enabled = false)]
  public virtual Decimal? SOPrepaymentReceived { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Prepayment Remaining", Enabled = false)]
  public virtual Decimal? SOPrepaymentRemaining { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Prepayment Applied", Enabled = false)]
  public virtual Decimal? SOPrepaymentApplied { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXFormula(typeof (IIf<Where<FSServiceOrder.pendingPOLineCntr, Greater<int0>>, True, False>))]
  [PXUIVisible(typeof (Where<Current<FSSrvOrdType.postTo>, Equal<FSPostTo.Sales_Order_Invoice>, Or<Current<FSSrvOrdType.postTo>, Equal<FSPostTo.Sales_Order_Module>, Or<Current<FSSrvOrdType.postTo>, Equal<FSPostTo.Projects>>>>))]
  [PXUIField(DisplayName = "Waiting for Purchased Items", Enabled = false, FieldClass = "DISTINV")]
  public virtual bool? WaitingForParts { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Appointments Needed", Enabled = false)]
  public virtual bool? AppointmentsNeeded { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? MaxLineNbr
  {
    get => this._MaxLineNbr;
    set => this._MaxLineNbr = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? ApptNeededLineCntr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? PendingPOLineCntr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? APBillLineCntr { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXInt]
  [PXSelector(typeof (Search<PX.Objects.CR.Location.locationID, Where<PX.Objects.CR.Location.bAccountID, Equal<Optional<FSServiceOrder.customerID>>>>), SubstituteKey = typeof (PX.Objects.CR.Location.locationCD), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  public virtual int? ReportLocationID { get; set; }

  [PXInt]
  public virtual int? Mem_ReturnValueID { get; set; }

  [PXBool]
  [PXUIField(Enabled = false)]
  [PXDBScalar(typeof (Search2<FSContractPeriod.invoiced, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSContractPeriod.serviceContractID>>>, Where<FSContractPeriod.serviceContractID, Equal<FSServiceOrder.billServiceContractID>, And<FSContractPeriod.contractPeriodID, Equal<FSServiceOrder.billContractPeriodID>, And<FSServiceContract.billingType, Equal<ListField.ServiceContractBillingType.standardizedBillings>>>>>))]
  public virtual bool? InvoicedByContract { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Billed", Enabled = false)]
  public virtual bool? Mem_Invoiced
  {
    get
    {
      return new bool?(this.PostedBy != null && this.PostedBy == "SO" || this.BillServiceContractID.HasValue && this.InvoicedByContract.GetValueOrDefault());
    }
  }

  [PXInt]
  public virtual int? AppointmentsCompletedCntr { get; set; }

  [PXInt]
  public virtual int? AppointmentsCompletedOrClosedCntr { get; set; }

  [PXString(17, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCC")]
  public virtual string MemRefNbr { get; set; }

  [PXString(62, IsUnicode = true)]
  public virtual string MemAcctName { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(Visible = false)]
  public virtual bool? IsPrepaymentEnable { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(Visible = false)]
  public virtual bool? ShowInvoicesTab { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Reference Nbr.")]
  public virtual string SourceReferenceNbr
  {
    get
    {
      return (this.SourceDocType != null ? this.SourceDocType.Trim() + ", " : "") + (this.SourceRefNbr != null ? this.SourceRefNbr.Trim() : "");
    }
  }

  [PXString]
  [PXUIField(DisplayName = "Project ID", FieldClass = "PROJECT")]
  public virtual string ProjectCD { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Task ID", FieldClass = "PROJECT")]
  public virtual string TaskCD { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Project Description", FieldClass = "PROJECT")]
  public virtual string ProjectDescr { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Task Description", FieldClass = "PROJECT")]
  public virtual string ProjectTaskDescr { get; set; }

  [PXBool]
  public virtual bool? CanCreatePurchaseOrder
  {
    get
    {
      bool? canceled = this.Canceled;
      bool flag1 = false;
      int num;
      if (canceled.GetValueOrDefault() == flag1 & canceled.HasValue)
      {
        bool? closed = this.Closed;
        bool flag2 = false;
        if (closed.GetValueOrDefault() == flag2 & closed.HasValue)
        {
          num = this.WaitingForParts.GetValueOrDefault() ? 1 : 0;
          goto label_4;
        }
      }
      num = 0;
label_4:
      return new bool?(num != 0);
    }
  }

  [PXInt]
  public virtual int? SLARemaining { get; set; }

  [PXString]
  public virtual string CustomerDisplayName { get; set; }

  [PXString]
  public virtual string ContactName { get; set; }

  [PXString]
  public virtual string ContactPhone { get; set; }

  [PXString]
  public virtual string ContactEmail { get; set; }

  [PXString]
  public virtual string AssignedEmployeeDisplayName { get; set; }

  [PXInt]
  public virtual int? ServicesRemaining { get; set; }

  [PXInt]
  public virtual int? ServicesCount { get; set; }

  [PXInt]
  public virtual Array ServiceClassIDs { get; set; }

  [PXString]
  public virtual string BranchLocationDesc { get; set; }

  [PXInt]
  public virtual int? TreeID { get; set; }

  [PXString]
  public virtual string Text { get; set; }

  [PXBool]
  public virtual bool? Leaf { get; set; }

  public virtual object Rows { get; set; }

  [PXString]
  public virtual string CustomOrderDate
  {
    get => this.OrderDate.HasValue ? this.OrderDate.ToString() : string.Empty;
  }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Deadline - SLA Date", DisplayNameTime = "Deadline - SLA Time")]
  [PXUIField(DisplayName = "Deadline - SLA")]
  public virtual DateTime? SLAETAUTC { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIEnabled(typeof (Where<Current<FSSrvOrdType.behavior>, NotEqual<ListField.ServiceOrderTypeBehavior.internalAppointment>>))]
  [PXUIField(DisplayName = "Customer Tax Zone")]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  [PXFormula(typeof (Default<FSServiceOrder.branchID>))]
  [PXFormula(typeof (Default<FSServiceOrder.billLocationID>))]
  public virtual string TaxZoneID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("T", typeof (Search<PX.Objects.CR.Location.cTaxCalcMode, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSServiceOrder.billCustomerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<FSServiceOrder.billLocationID>>>>>))]
  [PXFormula(typeof (Default<FSServiceOrder.billCustomerID>))]
  [PXFormula(typeof (Default<FSServiceOrder.billLocationID>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.vATReporting>))]
  [PXDBCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.vatExemptTotal))]
  [PXUIField(DisplayName = "VAT Exempt Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatExemptTotal { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatExemptTotal { get; set; }

  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.vATReporting>))]
  [PXDBCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.vatTaxableTotal))]
  [PXUIField(DisplayName = "VAT Taxable Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatTaxableTotal { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatTaxableTotal { get; set; }

  [PXDBCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.taxTotal))]
  [PXUIField(DisplayName = "Estimated Tax Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxTotal { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTotal { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Skip External Tax Calculation", Enabled = false)]
  public virtual bool? SkipExternalTaxCalculation { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Is Up to Date", Enabled = false)]
  public virtual bool? IsTaxValid { get; set; }

  [PXCurrency(typeof (FSServiceOrder.curyInfoID))]
  [PXUIField(Enabled = false)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineDocDiscountTotal { get; set; }

  [PXCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.docDisc))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Document Discount", Enabled = true)]
  public virtual Decimal? CuryDocDisc
  {
    get => this._CuryDocDisc;
    set => this._CuryDocDisc = value;
  }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DocDisc
  {
    get => this._DocDisc;
    set => this._DocDisc = value;
  }

  [PXDBCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.discTot))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Discount Total")]
  public virtual Decimal? CuryDiscTot
  {
    get => this._CuryDiscTot;
    set => this._CuryDiscTot = value;
  }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Discount Total")]
  public virtual Decimal? DiscTot
  {
    get => this._DiscTot;
    set => this._DiscTot = value;
  }

  [PXDependsOnFields(new System.Type[] {typeof (FSServiceOrder.curyBillableOrderTotal), typeof (FSServiceOrder.curyDiscTot), typeof (FSServiceOrder.curyTaxTotal)})]
  [PXDBCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.docTotal))]
  [PXUIField(DisplayName = "Estimated Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDocTotal { get; set; }

  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Base Service Order Total", Enabled = false)]
  public virtual Decimal? DocTotal { get; set; }

  [PXDBCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.costTotal))]
  [PXUIField(DisplayName = "Cost Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryCostTotal { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CostTotal { get; set; }

  [PXDecimal]
  [PXDefault]
  [PXUIField(DisplayName = "Profit Markup (%)", Enabled = false)]
  public virtual Decimal? ProfitPercent { get; set; }

  [PXDecimal]
  [PXDefault]
  [PXUIField(DisplayName = "Profit Margin (%)", Enabled = false)]
  public virtual Decimal? ProfitMarginPercent { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? IsCalledFromQuickProcess { get; set; }

  public virtual bool IsBilledOrClosed
  {
    get => this.Billed.GetValueOrDefault() || this.Closed.GetValueOrDefault();
  }

  [PXString]
  public string FormCaptionDescription { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool IsINReleaseProcess { get; set; }

  [PXCurrency(typeof (FSServiceOrder.curyInfoID), typeof (FSServiceOrder.estimatedBillableTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Estimated Billable Total", Enabled = false)]
  public virtual Decimal? CuryEstimatedBillableTotal { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Estimated Billable Total", Enabled = false)]
  public virtual Decimal? EstimatedBillableTotal { get; set; }

  /// <summary>
  /// The tax exemption number for reporting purposes. The field is used if the system is integrated with External Tax Calculation
  /// and the <see cref="P:PX.Objects.CS.FeaturesSet.AvalaraTax">External Tax Calculation Integration</see> feature is enabled.
  /// </summary>
  [PXDefault(typeof (Search<PX.Objects.CR.Location.cAvalaraExemptionNumber, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSServiceOrder.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<FSServiceOrder.locationID>>>>>))]
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
  /// that is specified for the <see cref="T:PX.Objects.FS.FSServiceOrder.locationID">location of the customer</see>.
  /// </value>
  [PXDefault("0", typeof (Search<PX.Objects.CR.Location.cAvalaraCustomerUsageType, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSServiceOrder.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<FSServiceOrder.locationID>>>>>))]
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Tax Exemption Type")]
  [TXAvalaraCustomerUsageType.List]
  public virtual string EntityUsageType { get; set; }

  public class PK : PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>
  {
    public static FSServiceOrder Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (FSServiceOrder) PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>.FindBy(graph, (object) srvOrdType, (object) refNbr, options);
    }
  }

  public class UK : PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.sOID>
  {
    public static FSServiceOrder Find(PXGraph graph, int? sOID, PKFindOptions options = 0)
    {
      return (FSServiceOrder) PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.sOID>.FindBy(graph, (object) sOID, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.customerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.customerID, FSServiceOrder.locationID>
    {
    }

    public class BillCustomer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.billCustomerID>
    {
    }

    public class BillCustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.billCustomerID, FSServiceOrder.billLocationID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.branchID>
    {
    }

    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType>
    {
    }

    public class Address : 
      PrimaryKeyOf<FSAddress>.By<FSAddress.addressID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.serviceOrderAddressID>
    {
    }

    public class Contact : 
      PrimaryKeyOf<FSContact>.By<FSContact.contactID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.serviceOrderContactID>
    {
    }

    public class Contract : 
      PrimaryKeyOf<PX.Objects.CT.Contract>.By<PX.Objects.CT.Contract.contractID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.contractID>
    {
    }

    public class BranchLocation : 
      PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.branchLocationID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.projectID>
    {
    }

    public class ProjectTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.projectID, FSServiceOrder.dfltProjectTaskID>
    {
    }

    public class WorkFlowStage : 
      PrimaryKeyOf<FSWFStage>.By<FSWFStage.wFStageID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.wFStageID>
    {
    }

    public class ServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.serviceContractID>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<FSSchedule>.By<FSSchedule.scheduleID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.scheduleID>
    {
    }

    public class BillServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.billServiceContractID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.taxZoneID>
    {
    }

    public class Problem : 
      PrimaryKeyOf<FSProblem>.By<FSProblem.problemCD>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.problemID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.curyID>
    {
    }

    public class Room : 
      PrimaryKeyOf<FSRoom>.By<FSRoom.branchLocationID, FSRoom.roomID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.branchLocationID, FSServiceOrder.roomID>
    {
    }

    public class SalesPerson : 
      PrimaryKeyOf<PX.Objects.AR.SalesPerson>.By<PX.Objects.AR.SalesPerson.salesPersonID>.ForeignKeyOf<FSServiceOrder>.By<FSServiceOrder.salesPersonID>
    {
    }
  }

  public class Events : PXEntityEventBase<FSServiceOrder>.Container<FSServiceOrder.Events>
  {
    public PXEntityEvent<FSServiceOrder> ServiceOrderDeleted;
    public PXEntityEvent<FSServiceOrder> ServiceContractCleared;
    public PXEntityEvent<FSServiceOrder> ServiceContractPeriodAssigned;
    public PXEntityEvent<FSServiceOrder> ServiceContractPeriodCleared;
    public PXEntityEvent<FSServiceOrder> RequiredServiceContractPeriodCleared;
    public PXEntityEvent<FSServiceOrder> LastAppointmentCompleted;
    public PXEntityEvent<FSServiceOrder> LastAppointmentCanceled;
    public PXEntityEvent<FSServiceOrder> LastAppointmentClosed;
    public PXEntityEvent<FSServiceOrder> AppointmentReOpened;
    public PXEntityEvent<FSServiceOrder> AppointmentUnclosed;
    public PXEntityEvent<FSServiceOrder> AppointmentEdit;
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.srvOrdType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.refNbr>
  {
  }

  public abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.sOID>
  {
  }

  public abstract class workflowTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrder.workflowTypeID>
  {
    public abstract class Values : ListField.ServiceOrderWorkflowTypes
    {
    }
  }

  public abstract class serviceOrderAddressID : FSServiceOrder.serviceOrderContactID
  {
  }

  public abstract class serviceOrderContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceOrder.serviceOrderContactID>
  {
  }

  public abstract class allowOverrideContactAddress : IBqlField, IBqlOperand
  {
  }

  public abstract class allowInvoice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.allowInvoice>
  {
  }

  public abstract class assignedEmpID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.assignedEmpID>
  {
  }

  public abstract class autoDocDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.autoDocDesc>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.customerID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.locationID>
  {
  }

  public abstract class billCustomerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.billCustomerID>
  {
  }

  public abstract class billLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.billLocationID>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.docDesc>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.contactID>
  {
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.contractID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.branchID>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceOrder.branchLocationID>
  {
  }

  public abstract class roomID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.roomID>
  {
  }

  public abstract class orderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSServiceOrder.orderDate>
  {
  }

  public abstract class userConfirmedClosing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.userConfirmedClosing>
  {
  }

  public abstract class userConfirmedUnclosing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.userConfirmedUnclosing>
  {
  }

  public abstract class copied : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.copied>
  {
  }

  public abstract class confirmed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.confirmed>
  {
  }

  public abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.openDoc>
  {
  }

  public abstract class processReopenAction : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.processReopenAction>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.hold>
  {
  }

  public abstract class awaiting : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.awaiting>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.completed>
  {
  }

  public abstract class processCompleteAction : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.processCompleteAction>
  {
  }

  public abstract class completeAppointments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.completeAppointments>
  {
  }

  public abstract class closed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.closed>
  {
  }

  public abstract class processCloseAction : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.processCloseAction>
  {
  }

  public abstract class closeAppointments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.closeAppointments>
  {
  }

  public abstract class canceled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.canceled>
  {
  }

  public abstract class processCancelAction : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.processCancelAction>
  {
  }

  public abstract class cancelAppointments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.cancelAppointments>
  {
  }

  public abstract class billed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.billed>
  {
  }

  public abstract class billingBy : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.billingBy>
  {
  }

  public abstract class billOnlyCompletedClosed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.billOnlyCompletedClosed>
  {
  }

  public abstract class completeActionRunning : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.completeActionRunning>
  {
  }

  public abstract class cancelActionRunning : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.cancelActionRunning>
  {
  }

  public abstract class reopenActionRunning : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.reopenActionRunning>
  {
  }

  public abstract class closeActionRunning : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.closeActionRunning>
  {
  }

  public abstract class unCloseActionRunning : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.unCloseActionRunning>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.status>
  {
    public abstract class Values : ListField.ServiceOrderStatus
    {
    }
  }

  public abstract class wFStageID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.wFStageID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSServiceOrder.curyInfoID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.projectID>
  {
  }

  public abstract class dfltProjectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceOrder.dfltProjectTaskID>
  {
  }

  public abstract class estimatedDurationTotal : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceOrder.estimatedDurationTotal>
  {
  }

  public abstract class longDescr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.longDescr>
  {
  }

  public abstract class estimatedOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.estimatedOrderTotal>
  {
  }

  public abstract class curyEstimatedOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.curyEstimatedOrderTotal>
  {
  }

  public abstract class billableOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.billableOrderTotal>
  {
  }

  public abstract class curyBillableOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.curyBillableOrderTotal>
  {
  }

  public abstract class priority : ListField_Priority_ServiceOrder
  {
  }

  public abstract class problemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.problemID>
  {
  }

  public abstract class severity : ListField_Severity_ServiceOrder
  {
  }

  public abstract class sLAETA : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSServiceOrder.sLAETA>
  {
  }

  public abstract class sourceDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrder.sourceDocType>
  {
  }

  public abstract class sourceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.sourceID>
  {
  }

  public abstract class sourceRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.sourceRefNbr>
  {
  }

  public abstract class sourceType : ListField_SourceType_ServiceOrder
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSServiceOrder.noteID>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.lineCntr>
  {
  }

  public abstract class splitLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.splitLineCntr>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSServiceOrder.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrder.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSServiceOrder.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSServiceOrder.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrder.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSServiceOrder.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSServiceOrder.Tstamp>
  {
  }

  public abstract class bAccountRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.bAccountRequired>
  {
  }

  public abstract class quote : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.quote>
  {
  }

  public abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceOrder.serviceContractID>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.scheduleID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.finPeriodID>
  {
  }

  public abstract class generationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.generationID>
  {
  }

  public abstract class custWorkOrderRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrder.custWorkOrderRefNbr>
  {
  }

  public abstract class custPORefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.custPORefNbr>
  {
  }

  public abstract class serviceCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.serviceCount>
  {
  }

  public abstract class scheduledServiceCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceOrder.scheduledServiceCount>
  {
  }

  public abstract class completeServiceCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceOrder.completeServiceCount>
  {
  }

  public abstract class postedBy : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.postedBy>
  {
  }

  public abstract class pendingAPARSOPost : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.pendingAPARSOPost>
  {
  }

  public abstract class pendingINPost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.pendingINPost>
  {
  }

  public abstract class cBID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.cBID>
  {
  }

  public abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.salesPersonID>
  {
  }

  public abstract class commissionable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.commissionable>
  {
  }

  public abstract class cutOffDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSServiceOrder.cutOffDate>
  {
  }

  public abstract class apptDurationTotal : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceOrder.apptDurationTotal>
  {
  }

  public abstract class curyApptOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.curyApptOrderTotal>
  {
  }

  public abstract class apptOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.apptOrderTotal>
  {
  }

  public abstract class curyAppointmentTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.curyAppointmentTaxTotal>
  {
  }

  public abstract class appointmentTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.appointmentTaxTotal>
  {
  }

  public abstract class curyAppointmentDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.curyAppointmentDocTotal>
  {
  }

  public abstract class appointmentDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.appointmentDocTotal>
  {
  }

  public abstract class billServiceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceOrder.billServiceContractID>
  {
  }

  public abstract class billContractPeriodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceOrder.billContractPeriodID>
  {
  }

  public abstract class curyEffectiveBillableLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.curyEffectiveBillableLineTotal>
  {
  }

  public abstract class effectiveBillableLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.effectiveBillableLineTotal>
  {
  }

  public abstract class curyEffectiveLogBillableTranAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.curyEffectiveLogBillableTranAmountTotal>
  {
  }

  public abstract class effectiveLogBillableTranAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.effectiveLogBillableTranAmountTotal>
  {
  }

  public abstract class curyEffectiveBillableTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.curyEffectiveBillableTaxTotal>
  {
  }

  public abstract class effectiveBillableTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.effectiveBillableTaxTotal>
  {
  }

  public abstract class curyEffectiveBillableDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.curyEffectiveBillableDocTotal>
  {
  }

  public abstract class effectiveBillableDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.effectiveBillableDocTotal>
  {
  }

  public abstract class curyShortLabelEffectiveBillableDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.curyShortLabelEffectiveBillableDocTotal>
  {
  }

  public abstract class shortLabelEffectiveBillableDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.shortLabelEffectiveBillableDocTotal>
  {
  }

  public abstract class curyEffectiveCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.curyEffectiveCostTotal>
  {
  }

  public abstract class effectiveCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.effectiveCostTotal>
  {
  }

  public abstract class sOCuryUnpaidBalanace : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.sOCuryUnpaidBalanace>
  {
  }

  public abstract class sOUnpaidBalanace : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.sOUnpaidBalanace>
  {
  }

  public abstract class sOCuryBillableUnpaidBalanace : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.sOCuryBillableUnpaidBalanace>
  {
  }

  public abstract class sOBillableUnpaidBalanace : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.sOBillableUnpaidBalanace>
  {
  }

  public abstract class sOPrepaymentReceived : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.sOPrepaymentReceived>
  {
  }

  public abstract class sOPrepaymentRemaining : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.sOPrepaymentRemaining>
  {
  }

  public abstract class sOPrepaymentApplied : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.sOPrepaymentApplied>
  {
  }

  public abstract class waitingForParts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.waitingForParts>
  {
  }

  public abstract class appointmentsNeeded : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.appointmentsNeeded>
  {
  }

  public abstract class maxLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.maxLineNbr>
  {
  }

  public abstract class apptNeededLineCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceOrder.apptNeededLineCntr>
  {
  }

  public abstract class pendingPOLineCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceOrder.pendingPOLineCntr>
  {
  }

  public abstract class apBillLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.apBillLineCntr>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.selected>
  {
  }

  public abstract class reportLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceOrder.reportLocationID>
  {
  }

  public abstract class invoicedByContract : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.invoicedByContract>
  {
  }

  public abstract class mem_Invoiced : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.mem_Invoiced>
  {
  }

  public abstract class memRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.memRefNbr>
  {
  }

  public abstract class memAcctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.memAcctName>
  {
  }

  public abstract class isPrepaymentEnable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.isPrepaymentEnable>
  {
  }

  public abstract class showInvoicesTab : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.showInvoicesTab>
  {
  }

  public abstract class sourceReferenceNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class projectCD : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.projectCD>
  {
  }

  public abstract class taskCD : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.taskCD>
  {
  }

  public abstract class projectDescr : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.projectDescr>
  {
  }

  public abstract class projectTaskDescr : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.projectTaskDescr>
  {
  }

  public abstract class canCreatePurchaseOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.canCreatePurchaseOrder>
  {
  }

  public abstract class sLARemaining : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.sLARemaining>
  {
  }

  public abstract class customerDisplayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrder.customerDisplayName>
  {
  }

  public abstract class contactName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.contactName>
  {
  }

  public abstract class contactPhone : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.contactPhone>
  {
  }

  public abstract class contactEmail : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.contactEmail>
  {
  }

  public abstract class assignedEmployeeDisplayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrder.assignedEmployeeDisplayName>
  {
  }

  public abstract class servicesRemaining : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceOrder.servicesRemaining>
  {
  }

  public abstract class servicesCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.servicesCount>
  {
  }

  public abstract class serviceClassIDs : IBqlField, IBqlOperand
  {
  }

  public abstract class branchLocationDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrder.branchLocationDesc>
  {
  }

  public abstract class treeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrder.treeID>
  {
  }

  public abstract class text : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.text>
  {
  }

  public abstract class leaf : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.leaf>
  {
  }

  public abstract class rows : IBqlField, IBqlOperand
  {
  }

  public abstract class customOrderDate : IBqlField, IBqlOperand
  {
  }

  public abstract class sLAETAUTC : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSServiceOrder.sLAETAUTC>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.taxZoneID>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrder.taxCalcMode>
  {
  }

  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.curyVatExemptTotal>
  {
  }

  public abstract class vatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.vatExemptTotal>
  {
  }

  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.curyVatTaxableTotal>
  {
  }

  public abstract class vatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.vatTaxableTotal>
  {
  }

  public abstract class curyTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.curyTaxTotal>
  {
  }

  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSServiceOrder.taxTotal>
  {
  }

  public abstract class skipExternalTaxCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.skipExternalTaxCalculation>
  {
  }

  public abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSServiceOrder.isTaxValid>
  {
  }

  public abstract class curyLineDocDiscountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.curyLineDocDiscountTotal>
  {
  }

  public abstract class curyDocDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSServiceOrder.curyDocDisc>
  {
  }

  public abstract class docDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSServiceOrder.docDisc>
  {
  }

  public abstract class curyDiscTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSServiceOrder.curyDiscTot>
  {
  }

  public abstract class discTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSServiceOrder.discTot>
  {
  }

  public abstract class curyDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.curyDocTotal>
  {
  }

  public abstract class docTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSServiceOrder.docTotal>
  {
  }

  public abstract class curyCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.curyCostTotal>
  {
  }

  public abstract class costTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSServiceOrder.costTotal>
  {
  }

  public abstract class profitPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.profitPercent>
  {
  }

  public abstract class profitMarginPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.profitMarginPercent>
  {
  }

  public abstract class isCalledFromQuickProcess : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrder.isCalledFromQuickProcess>
  {
  }

  public abstract class curyEstimatedBillableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.curyEstimatedBillableTotal>
  {
  }

  public abstract class estimatedBillableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrder.estimatedBillableTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.FS.FSServiceOrder.ExternalTaxExemptionNumber" />
  public abstract class externalTaxExemptionNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrder.externalTaxExemptionNumber>
  {
  }

  /// <inheritdoc cref="!:AvalaraCustomerUsageType" />
  public abstract class entityUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrder.entityUsageType>
  {
  }
}
