// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointment
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
using System.Collections.Generic;
using System.Text;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Appointment")]
[PXPrimaryGraph(typeof (AppointmentEntry))]
[PXGroupMask(typeof (InnerJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSAppointment.customerID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>))]
[Serializable]
public class FSAppointment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _ScheduledDateTimeBegin;
  protected DateTime? _ScheduledDateTimeEnd;
  protected DateTime? _ActualDateTimeBegin;
  protected DateTime? _ActualDateTimeEnd;
  public int? _ServiceContractID;
  protected Decimal? _CuryDiscTot;
  protected Decimal? _DocDisc;
  protected Decimal? _CuryDocDisc;
  protected DateTime? _MinLogTimeBegin;
  protected DateTime? _MaxLogTimeEnd;
  protected int? _MaxLineNbr;

  [PXDBString(4, IsFixed = true, IsKey = true, InputMask = ">AAAA")]
  [PXDefault(typeof (Coalesce<Search<FSxUserPreferences.dfltSrvOrdType, Where<BqlOperand<UserPreferences.userID, IBqlGuid>.IsEqual<BqlField<AccessInfo.userID, IBqlGuid>.FromCurrent>>>, Search<FSSetup.dfltSrvOrdType>>))]
  [PXUIField]
  [PXRestrictor(typeof (Where<FSSrvOrdType.active, Equal<True>>), null, new System.Type[] {})]
  [FSSelectorSrvOrdTypeNOTQuote]
  [PXUIVerify]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXString]
  [PXFormula(typeof (FSAppointment.srvOrdType))]
  [PXFieldDescription]
  public virtual string SrvOrdTypeCode { get; set; }

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "CCCCCCCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search2<FSAppointment.refNbr, LeftJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<FSServiceOrder.customerID>, And<PX.Objects.CR.Location.locationID, Equal<FSServiceOrder.locationID>>>>>>, Where2<Where<FSAppointment.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>, OrderBy<Desc<FSAppointment.refNbr>>>), new System.Type[] {typeof (FSAppointment.refNbr), typeof (PX.Objects.AR.Customer.acctCD), typeof (PX.Objects.AR.Customer.acctName), typeof (PX.Objects.CR.Location.locationCD), typeof (FSAppointment.docDesc), typeof (FSAppointment.status), typeof (FSAppointment.scheduledDateTimeBegin)})]
  [AppointmentAutoNumber(typeof (Search<FSSrvOrdType.srvOrdNumberingID, Where<FSSrvOrdType.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>>>), typeof (AccessInfo.businessDate))]
  [PXFieldDescription]
  public virtual string RefNbr { get; set; }

  [PXDBIdentity]
  public virtual int? AppointmentID { get; set; }

  [PXDBString(2, IsUnicode = false)]
  [PXDefault(typeof (Selector<FSAppointment.srvOrdType, FSSrvOrdType.appointmentWorkflowTypeID>))]
  [PXFormula(typeof (Default<FSAppointment.srvOrdType>))]
  [ListField.ServiceOrderWorkflowTypes.List]
  [PXUIField(DisplayName = "Workflow Type")]
  public virtual string WorkflowTypeID { get; set; }

  [PXDefault]
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Service Order Nbr.")]
  [FSSelectorSORefNbr_Appointment]
  public virtual string SORefNbr { get; set; }

  [PXDBInt]
  public virtual int? SOID { get; set; }

  /// <summary>
  /// A service field, which is necessary for the <see cref="T:PX.Objects.CS.CSAnswers">dynamically
  /// added attributes</see> defined at the <see cref="T:PX.Objects.FS.FSSrvOrdType">customer
  /// class</see> level to function correctly.
  /// </summary>
  [CRAttributesField(typeof (FSAppointment.srvOrdType), typeof (FSAppointment.noteID))]
  public virtual string[] Attributes { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Branch", Enabled = false, Visible = false, TabOrder = 0)]
  [PXDefault(typeof (AccessInfo.branchID))]
  [PXSelector(typeof (Search<PX.Objects.GL.Branch.branchID>), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public virtual int? BranchID { get; set; }

  [PXDBInt]
  [PopupMessage]
  [PXDefault]
  [PXUIField]
  [PXRestrictor(typeof (Where<PX.Objects.AR.Customer.status, IsNull, Or<PX.Objects.AR.Customer.status, Equal<CustomerStatus.active>, Or<PX.Objects.AR.Customer.status, Equal<CustomerStatus.oneTime>>>>), "The customer status is '{0}'.", new System.Type[] {typeof (PX.Objects.AR.Customer.status)})]
  [FSSelectorBAccountCustomerOrCombined]
  [PXForeignReference(typeof (FSAppointment.FK.Customer))]
  public virtual int? CustomerID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Billing Customer")]
  public virtual int? BillCustomerID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string DocDesc { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Scheduled Start Date", DisplayNameTime = "Scheduled Start Time")]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? ScheduledDateTimeBegin
  {
    get => this._ScheduledDateTimeBegin;
    set
    {
      this.ScheduledDateTimeBeginUTC = value;
      this._ScheduledDateTimeBegin = value;
    }
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Handle Manually")]
  public virtual bool? HandleManuallyScheduleTime { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Scheduled End Date", DisplayNameTime = "Scheduled End Time")]
  [PXDefault]
  [PXUIEnabled(typeof (FSAppointment.handleManuallyScheduleTime))]
  [PXUIField(DisplayName = "Scheduled End Date")]
  public virtual DateTime? ScheduledDateTimeEnd
  {
    get => this._ScheduledDateTimeEnd;
    set
    {
      this.ScheduledDateTimeEndUTC = value;
      this._ScheduledDateTimeEnd = value;
    }
  }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Actual Start Date", Enabled = false)]
  public virtual DateTime? ExecutionDate { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Actual Start Date", DisplayNameTime = "Actual Start Time")]
  [PXUIField]
  public virtual DateTime? ActualDateTimeBegin
  {
    get => this._ActualDateTimeBegin;
    set
    {
      this._ActualDateTimeBegin = value;
      this.ActualDateTimeBeginUTC = value;
      if (!this._ActualDateTimeBegin.HasValue)
        return;
      DateTime dateTime1;
      ref DateTime local = ref dateTime1;
      DateTime dateTime2 = this._ActualDateTimeBegin.Value;
      int year = dateTime2.Year;
      dateTime2 = this._ActualDateTimeBegin.Value;
      int month = dateTime2.Month;
      dateTime2 = this._ActualDateTimeBegin.Value;
      int day = dateTime2.Day;
      local = new DateTime(year, month, day);
      DateTime? executionDate = this.ExecutionDate;
      dateTime2 = dateTime1;
      if ((executionDate.HasValue ? (executionDate.GetValueOrDefault() != dateTime2 ? 1 : 0) : 1) == 0)
        return;
      this.ExecutionDate = new DateTime?(dateTime1);
    }
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Handle Manually")]
  public virtual bool? HandleManuallyActualTime { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Actual End Date", DisplayNameTime = "Actual End Time")]
  [PXUIEnabled(typeof (FSAppointment.handleManuallyActualTime))]
  [PXUIField]
  public virtual DateTime? ActualDateTimeEnd
  {
    get => this._ActualDateTimeEnd;
    set
    {
      this.ActualDateTimeEndUTC = value;
      this._ActualDateTimeEnd = value;
    }
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  [PXUIField]
  public virtual string CuryID { get; set; }

  [PXDBLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Service Description", Visible = true, Enabled = false)]
  public virtual string AutoDocDesc { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Confirmed")]
  public virtual bool? Confirmed { get; set; }

  [PXDBString(2147483647 /*0x7FFFFFFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Delivery Notes")]
  public virtual string DeliveryNotes { get; set; }

  [ProjectDefault]
  [PXUIEnabled(typeof (Where<Current<FSServiceOrder.sOID>, Less<Zero>, And<Current<FSServiceContract.billingType>, NotEqual<ListField.ServiceContractBillingType.standardizedBillings>>>))]
  [ProjectBase(typeof (FSServiceOrder.billCustomerID))]
  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.isCancelled, Equal<False>>), "The {0} project or contract is canceled.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXForeignReference(typeof (FSAppointment.FK.Project))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt]
  [PXFormula(typeof (Default<FSAppointment.projectID>))]
  [PXUIField]
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<FSAppointment.projectID>>, And<PMTask.isDefault, Equal<True>, And<PMTask.isCompleted, Equal<False>, And<PMTask.isCancelled, Equal<False>>>>>>))]
  [FSSelectorActive_AR_SO_ProjectTask(typeof (Where<PMTask.projectID, Equal<Current<FSAppointment.projectID>>>))]
  [PXForeignReference(typeof (FSAppointment.FK.DefaultTask))]
  public virtual int? DfltProjectTaskID { get; set; }

  [PXDBString(2147483647 /*0x7FFFFFFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string LongDescr { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Not Started", Enabled = false)]
  public virtual bool? NotStarted { get; set; }

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
  [PXUIField(DisplayName = "In Process", Enabled = false)]
  public virtual bool? InProcess { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Paused", Enabled = false)]
  public virtual bool? Paused { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Completed", Enabled = false)]
  public virtual bool? Completed { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Closed", Enabled = false)]
  public virtual bool? Closed { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Canceled", Enabled = false)]
  public virtual bool? Canceled { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Billed", Enabled = false)]
  public virtual bool? Billed { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Generated by Contract", Enabled = false)]
  public virtual bool? GeneratedByContract { get; set; }

  [PXBool]
  public virtual bool? UserConfirmedUnclosing { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? StartActionRunning { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? PauseActionRunning { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? ResumeActionRunning { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? CompleteActionRunning { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? CloseActionRunning { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? UnCloseActionRunning { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? CancelActionRunning { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? ReopenActionRunning { get; set; }

  [PXUnboundDefault(false)]
  [PXBool]
  public virtual bool? ReloadServiceOrderRelated { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [ListField.AppointmentStatus.List]
  [PXUIField]
  public virtual string Status { get; set; }

  [PXBool]
  [PXUnboundDefault(typeof (Switch<Case<Where<FSAppointment.reopenActionRunning, Equal<True>>, False, Case<Where<FSAppointment.startActionRunning, Equal<True>, Or<FSAppointment.inProcess, Equal<True>, Or<FSAppointment.paused, Equal<True>, Or<FSAppointment.completed, Equal<True>>>>>, True>>, False>))]
  public virtual bool? AreActualFieldsActive { get; set; }

  [PXDate(UseTimeZone = true)]
  [PXUIField(DisplayName = "Effective Document Date", Enabled = false)]
  [PXFormula(typeof (IIf<Where<FSAppointment.areActualFieldsActive, Equal<False>>, FSAppointment.scheduledDateTimeBegin, IsNull<FSAppointment.actualDateTimeBegin, FSAppointment.scheduledDateTimeBegin>>))]
  public virtual DateTime? EffDocDate { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? SplitLineCntr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LogLineCntr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? EmployeeLineCntr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? PendingPOLineCntr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? PendingApptPOLineCntr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? APBillLineCntr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? StaffCntr { get; set; }

  [PXDefault]
  [PXUIField(DisplayName = "NoteID")]
  [PXSearchable(8192 /*0x2000*/, "SM {0}: {1}", new System.Type[] {typeof (FSAppointment.srvOrdType), typeof (FSAppointment.refNbr)}, new System.Type[] {typeof (PX.Objects.AR.Customer.acctCD), typeof (FSAppointment.srvOrdType), typeof (FSAppointment.refNbr), typeof (FSAppointment.soRefNbr), typeof (FSAppointment.docDesc)}, NumberFields = new System.Type[] {typeof (FSAppointment.refNbr)}, Line1Format = "{0:d}{1}{2}", Line1Fields = new System.Type[] {typeof (FSAppointment.scheduledDateTimeBegin), typeof (FSAppointment.status), typeof (FSAppointment.soRefNbr)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (FSAppointment.docDesc)}, MatchWithJoin = typeof (InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSServiceOrder.srvOrdType>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>>>), WhereConstraint = typeof (Where<FSServiceOrder.customerID, IsNotNull, Or<Where<FSSrvOrdType.behavior, Equal<ListField.ServiceOrderTypeBehavior.internalAppointment>>>>), SelectForFastIndexing = typeof (Select2<FSAppointment, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>>>))]
  [PXNote(ShowInReferenceSelector = true)]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "LastModifiedByID")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "tstamp")]
  public virtual byte[] tstamp { get; set; }

  [PXDBTimeSpanLong]
  [PXUIField(DisplayName = "Estimated Duration", Enabled = false)]
  [PXDefault(0)]
  public virtual int? EstimatedDurationTotal { get; set; }

  [FSDBTimeSpanLongAllowNegative]
  [PXUIField(DisplayName = "Actual Duration", Enabled = false)]
  [PXDefault(0)]
  public virtual int? ActualDurationTotal { get; set; }

  [PXDBInt(MinValue = 0)]
  [PXUIField(DisplayName = "Driving Time")]
  public virtual int? DriveTime { get; set; }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Latitude", Enabled = false)]
  public virtual Decimal? MapLatitude { get; set; }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Longitude", Enabled = false)]
  public virtual Decimal? MapLongitude { get; set; }

  [PXDBInt(MinValue = 1)]
  [PXUIField(DisplayName = "Route Position")]
  public virtual int? RoutePosition { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Time Locked")]
  public virtual bool? TimeLocked { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<FSServiceContract.serviceContractID, Where<FSServiceContract.customerID, Equal<Current<FSServiceOrder.customerID>>>>), SubstituteKey = typeof (FSServiceContract.refNbr))]
  [PXUIField(DisplayName = "Source Service Contract ID", Enabled = false, FieldClass = "FSCONTRACT")]
  public virtual int? ServiceContractID
  {
    get => this._ServiceContractID;
    set
    {
      this._ServiceContractID = value;
      this.GeneratedByContract = new bool?(this._ServiceContractID.HasValue);
    }
  }

  [PXDBInt]
  [PXSelector(typeof (Search<FSSchedule.scheduleID, Where<FSSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>, And<FSSchedule.entityID, Equal<Current<FSServiceOrder.serviceContractID>>>>>), SubstituteKey = typeof (FSSchedule.refNbr))]
  [PXUIField(DisplayName = "Source Schedule ID", Enabled = false, FieldClass = "FSCONTRACT")]
  public virtual int? ScheduleID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Original Appointment ID", Enabled = false)]
  public virtual int? OriginalAppointmentID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Unreached Customer")]
  public virtual bool? UnreachedCustomer { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Route ID", Enabled = true)]
  [FSSelectorRouteID]
  public virtual int? RouteID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<FSRouteDocument.routeDocumentID>), SubstituteKey = typeof (FSRouteDocument.refNbr))]
  [PXUIField(DisplayName = "Route Nbr.")]
  public virtual int? RouteDocumentID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Validated by Dispatcher")]
  public virtual bool? ValidatedByDispatcher { get; set; }

  [PXDBInt]
  [FSSelectorVehicle]
  [PXRestrictor(typeof (Where<FSEquipment.status, Equal<ID.Equipment_Status.Equipment_StatusActive>>), "Vehicle is {0}.", new System.Type[] {typeof (FSEquipment.status)})]
  [PXUIField(DisplayName = "Vehicle ID", FieldClass = "ROUTEMANAGEMENT")]
  public virtual int? VehicleID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Generation ID")]
  public virtual int? GenerationID { get; set; }

  [PXDBString(6, IsFixed = true)]
  [PXUIField(DisplayName = "Post Period")]
  public virtual string FinPeriodID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Workflow Stage")]
  [FSSelectorWorkflowStage(typeof (FSAppointment.srvOrdType))]
  [PXDefault(typeof (Search2<FSWFStage.wFStageID, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdTypeID, Equal<FSWFStage.wFID>>>, Where<FSSrvOrdType.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>>, OrderBy<Asc<FSWFStage.parentWFStageID, Asc<FSWFStage.sortOrder>>>>))]
  [PXUIVisible(typeof (BqlOperand<Current<FSSetup.showWorkflowStageField>, IBqlBool>.IsEqual<True>))]
  public virtual int? WFStageID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Approved Staff Times", Enabled = false, Visible = false)]
  public virtual bool? TimeRegistered { get; set; }

  [PXDBString(IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Customer Signature")]
  public virtual string customerSignaturePath { get; set; }

  [PXUIField(DisplayName = "Signed Report ID")]
  [PXDBGuid(false)]
  public virtual Guid? CustomerSignedReport { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Full Name")]
  public virtual string FullNameSignature { get; set; }

  [SalesPerson(DisplayName = "Salesperson")]
  [PXDefault(typeof (Coalesce<Search<FSServiceOrder.salesPersonID, Where<FSServiceOrder.sOID, Equal<Current<FSServiceOrder.sOID>>>>, Search<FSSrvOrdType.salesPersonID, Where<FSSrvOrdType.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>>>>))]
  [PXForeignReference(typeof (FSAppointment.FK.SalesPerson))]
  public virtual int? SalesPersonID { get; set; }

  [PXDBBool]
  [PXDefault(typeof (Coalesce<Search<FSServiceOrder.commissionable, Where<FSServiceOrder.sOID, Equal<Current<FSServiceOrder.sOID>>>>, Search<FSSrvOrdType.commissionable, Where<FSSrvOrdType.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>>>>))]
  [PXUIField(DisplayName = "Commissionable")]
  public virtual bool? Commissionable { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? PendingAPARSOPost { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? PendingINPost { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("NP")]
  [PXUIField(Visible = false)]
  public virtual string PostingStatusAPARSO { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("NP")]
  [PXUIField(Visible = false)]
  public virtual string PostingStatusIN { get; set; }

  /// <summary>Non-used field</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Cut-Off Date")]
  [PXDefault]
  public virtual DateTime? CutOffDate { get; set; }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Latitude", Enabled = false)]
  public virtual Decimal? GPSLatitudeStart { get; set; }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Longitude", Enabled = false)]
  public virtual Decimal? GPSLongitudeStart { get; set; }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Latitude", Enabled = false)]
  public virtual Decimal? GPSLatitudeComplete { get; set; }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Longitude", Enabled = false)]
  public virtual Decimal? GPSLongitudeComplete { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Estimated Total", Enabled = false)]
  public virtual Decimal? EstimatedLineTotal { get; set; }

  [PXDBCurrency(typeof (FSAppointment.curyInfoID), typeof (FSAppointment.estimatedLineTotal))]
  [PXUIField(DisplayName = "Estimated Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryEstimatedLineTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual Decimal? EstimatedCostTotal { get; set; }

  [PXDBCurrency(typeof (FSAppointment.curyInfoID), typeof (FSAppointment.estimatedCostTotal))]
  [PXUIField(DisplayName = "Estimated Cost Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryEstimatedCostTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Ext. Price Total", Enabled = false)]
  public virtual Decimal? LineTotal { get; set; }

  [PXDBCurrency(typeof (FSAppointment.curyInfoID), typeof (FSAppointment.lineTotal))]
  [PXUIField(DisplayName = "Ext. Price Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Billable Labor Total", Enabled = false)]
  public virtual Decimal? LogBillableTranAmountTotal { get; set; }

  [PXDBCurrency(typeof (FSAppointment.curyInfoID), typeof (FSAppointment.logBillableTranAmountTotal))]
  [PXUIField(DisplayName = "Billable Labor Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIVisible(typeof (Where2<Where<Current<FSSrvOrdType.postTo>, Equal<FSPostTo.Projects>, And<Current<FSSrvOrdType.billingType>, Equal<ListField_SrvOrdType_BillingType.CostAsCost>, And<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>>>>>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>))]
  public virtual Decimal? CuryLogBillableTranAmountTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Billable Total", Enabled = false)]
  public virtual Decimal? BillableLineTotal { get; set; }

  [PXDBCurrency(typeof (FSAppointment.curyInfoID), typeof (FSAppointment.billableLineTotal))]
  [PXUIField(DisplayName = "Actual Billable Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryBillableLineTotal { get; set; }

  [PXDBInt]
  [FSSelectorPPFRServiceContract(typeof (FSServiceOrder.customerID), typeof (FSServiceOrder.locationID))]
  [PXUIField(DisplayName = "Service Contract", FieldClass = "FSCONTRACT", IsReadOnly = true)]
  public virtual int? BillServiceContractID { get; set; }

  [PXDBInt]
  [FSSelectorContractBillingPeriod]
  [PXFormula(typeof (Default<FSServiceOrder.billCustomerID, FSAppointment.scheduledDateTimeEnd>))]
  [PXUIField(DisplayName = "Contract Period", Enabled = false)]
  public virtual int? BillContractPeriodID { get; set; }

  [PXDBCurrency(typeof (FSAppointment.curyInfoID), typeof (FSAppointment.costTotal))]
  [PXUIField(DisplayName = "Cost Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryCostTotal { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(Enabled = false)]
  public virtual Decimal? CostTotal { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Profit Markup (%)", Enabled = false)]
  [PXFormula(typeof (Switch<Case<Where<FSAppointment.curyCostTotal, Equal<decimal0>>, Null>, Mult<Div<Sub<FSAppointment.curyActualBillableTotal, FSAppointment.curyCostTotal>, FSAppointment.curyCostTotal>, decimal100>>))]
  public virtual Decimal? ProfitPercent { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Profit Margin (%)", Enabled = false)]
  [PXFormula(typeof (Switch<Case<Where<FSAppointment.curyActualBillableTotal, Equal<decimal0>>, Null>, Mult<Div<Sub<FSAppointment.curyActualBillableTotal, FSAppointment.curyCostTotal>, FSAppointment.curyActualBillableTotal>, decimal100>>))]
  public virtual Decimal? ProfitMarginPercent { get; set; }

  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.vATReporting>))]
  [PXDBCurrency(typeof (FSAppointment.curyInfoID), typeof (FSAppointment.vatExemptTotal))]
  [PXUIField(DisplayName = "VAT Exempt Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatExemptTotal { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatExemptTotal { get; set; }

  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.vATReporting>))]
  [PXDBCurrency(typeof (FSAppointment.curyInfoID), typeof (FSAppointment.vatTaxableTotal))]
  [PXUIField(DisplayName = "VAT Taxable Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatTaxableTotal { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatTaxableTotal { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIEnabled(typeof (Where<Current<FSSrvOrdType.behavior>, NotEqual<ListField.ServiceOrderTypeBehavior.internalAppointment>>))]
  [PXUIField(DisplayName = "Customer Tax Zone")]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  [PXFormula(typeof (Default<FSAppointment.branchID>))]
  [PXFormula(typeof (Default<FSServiceOrder.billLocationID>))]
  public virtual string TaxZoneID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("T", typeof (Search<PX.Objects.CR.Location.cTaxCalcMode, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSServiceOrder.billCustomerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<FSServiceOrder.billLocationID>>>>>))]
  [PXFormula(typeof (Default<FSServiceOrder.billCustomerID>))]
  [PXFormula(typeof (Default<FSServiceOrder.billLocationID>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTotal { get; set; }

  [PXDBCurrency(typeof (FSAppointment.curyInfoID), typeof (FSAppointment.taxTotal))]
  [PXUIField(DisplayName = "Actual Tax Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Discount Total")]
  public virtual Decimal? DiscTot { get; set; }

  [PXDBCurrency(typeof (FSAppointment.curyInfoID), typeof (FSAppointment.discTot))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Discount Total", Enabled = false)]
  public virtual Decimal? CuryDiscTot
  {
    get => this._CuryDiscTot;
    set => this._CuryDiscTot = value;
  }

  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Base Order Total", Enabled = false)]
  public virtual Decimal? DocTotal { get; set; }

  [PXDependsOnFields(new System.Type[] {typeof (FSAppointment.curyBillableLineTotal), typeof (FSAppointment.curyDiscTot), typeof (FSAppointment.curyTaxTotal)})]
  [PXDBCurrency(typeof (FSAppointment.curyInfoID), typeof (FSAppointment.docTotal))]
  [PXUIField(DisplayName = "Invoice Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDocTotal { get; set; }

  [PXCurrency(typeof (FSAppointment.curyInfoID))]
  [PXUIField(Enabled = false)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineDocDiscountTotal { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DocDisc
  {
    get => this._DocDisc;
    set => this._DocDisc = value;
  }

  [PXCurrency(typeof (FSAppointment.curyInfoID), typeof (FSAppointment.docDisc))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Document Discount", Enabled = true)]
  public virtual Decimal? CuryDocDisc
  {
    get => this._CuryDocDisc;
    set => this._CuryDocDisc = value;
  }

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Skip External Tax Calculation", Enabled = false)]
  public virtual bool? SkipExternalTaxCalculation { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Is Up to Date", Enabled = false)]
  public virtual bool? IsTaxValid { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Min Log Date Begin", DisplayNameTime = "Min Log Time Begin")]
  [PXUIField(DisplayName = "Min Log Time Begin")]
  public virtual DateTime? MinLogTimeBegin
  {
    get => this._MinLogTimeBegin;
    set => this._MinLogTimeBegin = value;
  }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Max Log Date End", DisplayNameTime = "Max Log Time End")]
  [PXUIField(DisplayName = "Max Log Time End")]
  public virtual DateTime? MaxLogTimeEnd
  {
    get => this._MaxLogTimeEnd;
    set => this._MaxLogTimeEnd = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXFormula(typeof (IIf<Where<FSAppointment.pendingPOLineCntr, Greater<int0>>, True, False>))]
  [PXUIVisible(typeof (Where<Current<FSSrvOrdType.postTo>, Equal<FSPostTo.Sales_Order_Invoice>, Or<Current<FSSrvOrdType.postTo>, Equal<FSPostTo.Sales_Order_Module>, Or<Current<FSSrvOrdType.postTo>, Equal<FSPostTo.Projects>>>>))]
  [PXUIField(DisplayName = "Waiting for Purchased Items", Enabled = false, FieldClass = "DISTINV")]
  public virtual bool? WaitingForParts { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Finished")]
  public virtual bool? Finished { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Appointment Billable Total", Enabled = false)]
  [PXFormula(typeof (Switch<Case<Where<FSAppointment.inProcess, Equal<True>, Or<FSAppointment.completed, Equal<True>, Or<FSAppointment.closed, Equal<True>>>>, FSAppointment.curyDocTotal>, Null>))]
  public virtual Decimal? AppCompletedBillableTotal { get; set; }

  [PXInt]
  [PXDependsOnFields(new System.Type[] {typeof (FSAppointment.travelInProcess)})]
  public virtual int? IntTravelInProcess
  {
    get
    {
      bool? travelInProcess = this.TravelInProcess;
      return !travelInProcess.HasValue ? new int?() : (travelInProcess.GetValueOrDefault() ? new int?(1) : new int?(0));
    }
    set
    {
      int? nullable = value;
      int num = 0;
      this.TravelInProcess = new bool?(nullable.GetValueOrDefault() > num & nullable.HasValue);
    }
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Travel in Process", Enabled = false)]
  public virtual bool? TravelInProcess { get; set; }

  [PXDBInt]
  [FSSelector_StaffMember_ServiceOrderProjectID]
  [PXUIField(DisplayName = "Primary Driver", FieldClass = "ROUTEOPTIMIZER")]
  public virtual int? PrimaryDriver { get; set; }

  [PXDBString(2, IsFixed = true)]
  [ListField_Status_ROOptimization.ListAtrribute]
  [PXFormula(typeof (Default<FSAppointment.primaryDriver, FSAppointment.scheduledDateTimeBegin, FSAppointment.scheduledDateTimeEnd>))]
  [PXDefault(typeof (ListField_Status_ROOptimization.NotOptimized))]
  [PXUIField(DisplayName = "Optimization Result", FieldClass = "ROUTEOPTIMIZER", Enabled = false)]
  public virtual string ROOptimizationStatus { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Nbr. in Original Sequence", Enabled = false, FieldClass = "ROUTEOPTIMIZER")]
  public virtual int? ROOriginalSortOrder { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Nbr. in Optimized Sequence", Enabled = false, FieldClass = "ROUTEOPTIMIZER")]
  public virtual int? ROSortOrder { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? MaxLineNbr
  {
    get => this._MaxLineNbr;
    set => this._MaxLineNbr = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXDate]
  public virtual DateTime? Mem_InvoiceDate { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Invoice Doc Type", Enabled = false)]
  public virtual string Mem_InvoiceDocType { get; set; }

  [PXString(15, IsFixed = true)]
  [PXUIField(DisplayName = "Batch Nbr.", Enabled = false)]
  public virtual string Mem_BatchNbr { get; set; }

  [PXString(15)]
  [PXUIField(DisplayName = "Invoice Ref. Nbr.", Enabled = false)]
  public virtual string Mem_InvoiceRef { get; set; }

  [PXDecimal(2)]
  [PXUIField(DisplayName = "Scheduled Hours", Enabled = false)]
  public virtual Decimal? Mem_ScheduledHours { get; set; }

  [PXDecimal(2)]
  [PXUIField(DisplayName = "Appointment Hours", Enabled = false)]
  public virtual Decimal? Mem_AppointmentHours { get; set; }

  [PXDecimal(2)]
  [PXUIField(DisplayName = "Idle Rate (%)", Enabled = false)]
  public virtual Decimal? Mem_IdleRate { get; set; }

  [PXDecimal(2)]
  [PXUIField(DisplayName = "Occupational Rate (%)", Enabled = false)]
  public virtual Decimal? Mem_OccupationalRate { get; set; }

  [PXDefault(false)]
  public virtual bool? isBeingCloned { get; set; }

  [PXInt]
  public virtual int? Mem_ReturnValueID { get; set; }

  [PXInt]
  public virtual int? Mem_LastRouteDocumentID { get; set; }

  [PXDateAndTime]
  public virtual DateTime? Mem_BusinessDateTime => new DateTime?(PXTimeZoneInfo.Now);

  [PXTimeSpanLong]
  [PXFormula(typeof (DateDiff<FSAppointment.scheduledDateTimeBegin, FSAppointment.scheduledDateTimeEnd, DateDiff.minute>))]
  [PXUIField(DisplayName = "Scheduled Duration", Enabled = false)]
  public virtual int? ScheduledDuration { get; set; }

  [PXTimeSpanLong]
  [PXDefault(typeof (IIf<Where<DateDiff<FSAppointment.actualDateTimeBegin, FSAppointment.actualDateTimeEnd, DateDiff.minute>, Greater<SharedClasses.int_0>>, DateDiff<FSAppointment.actualDateTimeBegin, FSAppointment.actualDateTimeEnd, DateDiff.minute>, SharedClasses.int_0>))]
  [PXFormula(typeof (IIf<Where<DateDiff<FSAppointment.actualDateTimeBegin, FSAppointment.actualDateTimeEnd, DateDiff.minute>, Greater<SharedClasses.int_0>>, DateDiff<FSAppointment.actualDateTimeBegin, FSAppointment.actualDateTimeEnd, DateDiff.minute>, SharedClasses.int_0>))]
  [PXUIField(DisplayName = "Actual Duration", Enabled = false)]
  public virtual int? ActualDuration { get; set; }

  [PXTimeList(1, 1440)]
  [PXInt]
  [PXUIField(DisplayName = "Actual Start Time")]
  public virtual int? Mem_ActualDateTimeBegin_Time
  {
    get
    {
      if (!this.ActualDateTimeBegin.HasValue)
        return new int?();
      DateTime dateTime = this.ActualDateTimeBegin.Value;
      return new int?((int) this.ActualDateTimeBegin.Value.TimeOfDay.TotalMinutes);
    }
  }

  [PXTimeList(1, 1440)]
  [PXInt]
  [PXUIField(DisplayName = "Actual End Time")]
  public virtual int? Mem_ActualDateTimeEnd_Time
  {
    get
    {
      if (!this.ActualDateTimeEnd.HasValue)
        return new int?();
      DateTime dateTime = this.ActualDateTimeEnd.Value;
      return new int?((int) this.ActualDateTimeEnd.Value.TimeOfDay.TotalMinutes);
    }
  }

  [PXString]
  [PXUIField(DisplayName = "Assigned employees list", Enabled = false)]
  public virtual string WildCard_AssignedEmployeesList { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Assigned employees cells list", Enabled = false)]
  public virtual string WildCard_AssignedEmployeesCellPhoneList { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Customer primary contact", Enabled = false)]
  public virtual string WildCard_CustomerPrimaryContact { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Customer primary contact cell", Enabled = false)]
  public virtual string WildCard_CustomerPrimaryContactCell { get; set; }

  [PXDateAndTime(UseTimeZone = true, DisplayMask = "t")]
  public virtual DateTime? Mem_ScheduledTimeBegin => this.ScheduledDateTimeBegin;

  [PXDate]
  public virtual DateTime? ScheduledDateBegin => this.ScheduledDateTimeBegin;

  [PXString]
  [ListField_Month.ListAtrribute]
  [PXDefault("JAN")]
  [PXUIField(DisplayName = "Month")]
  public virtual string Mem_ActualDateTime_Month
  {
    get
    {
      if (!this.ScheduledDateTimeBegin.HasValue)
        return (string) null;
      DateTime dateTime = this.ScheduledDateTimeBegin.Value;
      return SharedFunctions.GetMonthOfYearInStringByID(this.ScheduledDateTimeBegin.Value.Month);
    }
  }

  [PXInt]
  [PXUIField(DisplayName = "Year")]
  public virtual int? Mem_ActualDateTime_Year
  {
    get
    {
      if (!this.ScheduledDateTimeBegin.HasValue)
        return new int?();
      DateTime dateTime = this.ScheduledDateTimeBegin.Value;
      return new int?(this.ScheduledDateTimeBegin.Value.Year);
    }
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(Visible = false)]
  public virtual bool? IsRouteAppoinment { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(Visible = false)]
  public virtual bool? IsPrepaymentEnable { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? IsReassigned { get; set; }

  [PXInt]
  public virtual int? Mem_SMequipmentID { get; set; }

  [PXInt]
  [PXFormula(typeof (FSAppointment.actualDurationTotal))]
  public virtual int? ActualDurationTotalReport { get; set; }

  [PXInt]
  [PXSelector(typeof (Search<FSAppointment.refNbr, Where<FSAppointment.soRefNbr, Equal<Optional<FSAppointment.soRefNbr>>>>), SubstituteKey = typeof (FSAppointment.refNbr), DescriptionField = typeof (FSAppointment.refNbr))]
  public virtual int? AppointmentRefReport { get; set; }

  [PXString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "GPS Latitude Longitude", Enabled = false)]
  public virtual string Mem_GPSLatitudeLongitude { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Actual Date Time Begin", DisplayNameTime = "Actual Start Time")]
  [PXUIField(DisplayName = "Actual Date")]
  public virtual DateTime? ActualDateTimeBeginUTC { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Actual Date Time End", DisplayNameTime = "Actual End Time")]
  [PXUIField]
  public virtual DateTime? ActualDateTimeEndUTC { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Scheduled Date", DisplayNameTime = "Scheduled Start Time")]
  [PXUIField(DisplayName = "Scheduled Date")]
  public virtual DateTime? ScheduledDateTimeBeginUTC { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Scheduled End Date", DisplayNameTime = "Scheduled End Time")]
  [PXUIField]
  public virtual DateTime? ScheduledDateTimeEndUTC { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? IsCalledFromQuickProcess { get; set; }

  [PXBool]
  [PXDefault]
  [PXFormula(typeof (Switch<Case<Where<FSAppointment.postingStatusAPARSO, NotEqual<ListField_Status_Posting.Posted>, And<FSAppointment.postingStatusIN, NotEqual<ListField_Status_Posting.Posted>>>, False>, True>))]
  public virtual bool? IsPosted { get; set; }

  [PXBool]
  [PXDefault]
  [PXUIField]
  [PXFormula(typeof (Switch<Case<Where<FSAppointment.closed, NotEqual<True>, And<FSAppointment.canceled, NotEqual<True>, And<FSAppointment.hold, NotEqual<True>>>>, True>, False>))]
  public virtual bool? TravelCanBeStarted { get; set; }

  [PXBool]
  [PXDefault]
  [PXUIField]
  [PXFormula(typeof (Switch<Case<Where<FSAppointment.travelCanBeStarted, Equal<True>, And<FSAppointment.travelInProcess, Equal<True>>>, True>, False>))]
  public virtual bool? TravelCanBeCompleted { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? MustUpdateServiceOrder { get; set; }

  public virtual bool IsBilledOrClosed
  {
    get => this.IsPosted.GetValueOrDefault() || this.Closed.GetValueOrDefault();
  }

  [PXString]
  public string FormCaptionDescription { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool IsINReleaseProcess { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that there is inconsistency with the trackTime flags in log records.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(Visible = false)]
  public virtual bool? TrackTimeChanged { get; set; }

  [PXCurrency(typeof (FSAppointment.curyInfoID), typeof (FSAppointment.actualBillableTotal))]
  [PXUIField(DisplayName = "Actual Billable Total", Enabled = false)]
  public virtual Decimal? CuryActualBillableTotal { get; set; }

  [PXDecimal(4)]
  [PXUIField(DisplayName = "Actual Billable Total", Enabled = false)]
  public virtual Decimal? ActualBillableTotal { get; set; }

  /// <summary>
  /// The tax exemption number for reporting purposes. The field is used if the system is integrated with External Tax Calculation
  /// and the <see cref="P:PX.Objects.CS.FeaturesSet.AvalaraTax">External Tax Calculation Integration</see> feature is enabled.
  /// </summary>
  [PXDefault(typeof (Search<PX.Objects.CR.Location.cAvalaraExemptionNumber, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSAppointment.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<FSServiceOrder.locationID>>>>>))]
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
  [PXDefault("0", typeof (Search<PX.Objects.CR.Location.cAvalaraCustomerUsageType, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSAppointment.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<FSServiceOrder.locationID>>>>>))]
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Tax Exemption Type")]
  [TXAvalaraCustomerUsageType.List]
  public virtual string EntityUsageType { get; set; }

  /// <summary>
  /// Check whether appointment edit action button click is executed.
  /// </summary>
  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? EditActionRunning { get; set; }

  /// <summary>Returns the set of contacts of a customer.</summary>
  private static PXResultset<PX.Objects.CR.Contact> ReturnsContactList(
    PXGraph graph,
    int? appointmentID)
  {
    FSServiceOrder fsServiceOrder = PXResultset<FSServiceOrder>.op_Implicit(PXSelectBase<FSServiceOrder, PXSelectJoin<FSServiceOrder, InnerJoin<FSAppointment, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>, And<FSServiceOrder.srvOrdType, Equal<FSAppointment.srvOrdType>>>>, Where<FSAppointment.appointmentID, Equal<Required<FSAppointment.appointmentID>>>>.Config>.Select(graph, new object[1]
    {
      (object) appointmentID
    }));
    if (fsServiceOrder == null || !fsServiceOrder.CustomerID.HasValue)
      return (PXResultset<PX.Objects.CR.Contact>) null;
    return PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.bAccountID, Equal<Required<PX.Objects.CR.Contact.bAccountID>>, And<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.person>, And<PX.Objects.CR.Contact.isActive, Equal<True>>>>>.Config>.Select(graph, new object[1]
    {
      (object) fsServiceOrder.CustomerID
    });
  }

  private static StringBuilder ConcatenatesContactInfo(
    PXResultset<PX.Objects.CR.Contact> bqlResultSet,
    bool concatenateNames,
    bool concatenateCells)
  {
    StringBuilder stringBuilder = new StringBuilder();
    int num = 0;
    if (bqlResultSet.Count > 0)
    {
      foreach (PXResult<PX.Objects.CR.Contact> bqlResult in bqlResultSet)
      {
        PX.Objects.CR.Contact contact = PXResult<PX.Objects.CR.Contact>.op_Implicit(bqlResult);
        ++num;
        if (bqlResultSet.Count > 1 && num == bqlResultSet.Count)
          stringBuilder.Append(PXMessages.LocalizeFormatNoPrefix(" and ", Array.Empty<object>()));
        else if (stringBuilder.Length != 0)
          stringBuilder.Append(", ");
        if (!string.IsNullOrEmpty(contact.FirstName) && concatenateNames)
          stringBuilder.Append(contact.FirstName.Trim());
        if (!string.IsNullOrEmpty(contact.LastName) && concatenateNames)
        {
          stringBuilder.Append(' ');
          stringBuilder.Append(contact.LastName.Trim());
        }
        if (!string.IsNullOrEmpty(contact.Phone1) && concatenateCells)
          stringBuilder.Append(contact.Phone1.Trim());
        else if (concatenateCells)
          stringBuilder.Append("CONTACT CELL MISSING");
      }
    }
    else if (concatenateNames)
      stringBuilder.Append("CONTACT NAME MISSING");
    else if (concatenateCells)
      stringBuilder.Append("CONTACT CELL MISSING");
    return stringBuilder;
  }

  /// <summary>Gets the employees contact info separated by a coma.</summary>
  /// <param name="graph">Graph to use.</param>
  /// <param name="concatenateNames">Boolean that if true, returns the Staff name(s).</param>
  /// <param name="concatenateCells">Boolean that if true, returns the Staff cell phone(s).</param>
  /// <param name="appointmentID">Appointment ID.</param>
  private static StringBuilder GetsEmployeesContactInfo(
    PXGraph graph,
    bool concatenateNames,
    bool concatenateCells,
    int? appointmentID)
  {
    StringBuilder stringBuilder = new StringBuilder();
    int num = 0;
    BqlCommand bqlCommand = (BqlCommand) new Select5<FSAppointmentEmployee, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<FSAppointmentEmployee.employeeID>>, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.BAccount.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>, Where<FSAppointmentEmployee.appointmentID, Equal<Required<FSAppointmentEmployee.appointmentID>>>, Aggregate<GroupBy<FSAppointmentEmployee.employeeID>>, OrderBy<Asc<FSAppointmentEmployee.employeeID>>>();
    List<object> objectList = new PXView(graph, true, bqlCommand).SelectMulti(new object[1]
    {
      (object) appointmentID
    });
    if (objectList.Count > 0)
    {
      foreach (PXResult<FSAppointmentEmployee, PX.Objects.CR.BAccount, PX.Objects.CR.Contact> pxResult in objectList)
      {
        FSAppointmentEmployee appointmentEmployee = PXResult<FSAppointmentEmployee, PX.Objects.CR.BAccount, PX.Objects.CR.Contact>.op_Implicit(pxResult);
        PXResult<FSAppointmentEmployee, PX.Objects.CR.BAccount, PX.Objects.CR.Contact>.op_Implicit(pxResult);
        PX.Objects.CR.Contact contact = PXResult<FSAppointmentEmployee, PX.Objects.CR.BAccount, PX.Objects.CR.Contact>.op_Implicit(pxResult);
        ++num;
        if (objectList.Count > 1 && num == objectList.Count)
          stringBuilder.Append(PXMessages.LocalizeFormatNoPrefix(" and ", Array.Empty<object>()));
        else if (stringBuilder.Length != 0)
          stringBuilder.Append(", ");
        if (appointmentEmployee.Type == "EP")
        {
          if (!string.IsNullOrEmpty(contact.FirstName) && concatenateNames)
            stringBuilder.Append(contact.FirstName.Trim());
          if (!string.IsNullOrEmpty(contact.LastName) && concatenateNames)
          {
            stringBuilder.Append(' ');
            stringBuilder.Append(contact.LastName.Trim());
          }
        }
        else if (appointmentEmployee.Type == "VE" && !string.IsNullOrEmpty(contact.FullName) && concatenateNames)
          stringBuilder.Append(contact.FullName.Trim());
        if (!string.IsNullOrEmpty(contact.Phone1) && concatenateCells)
          stringBuilder.Append(contact.Phone1.Trim());
        else if (concatenateCells)
          stringBuilder.Append("STAFF CONTACT CELL MISSING");
      }
    }
    else
      stringBuilder.Append("THERE IS NO STAFF ASSIGNED FOR THIS APPOINTMENT");
    return stringBuilder;
  }

  /// <summary>Gets the value of a WildCard field.</summary>
  /// <returns>Returns the string value of the field.</returns>
  private static string GetWildCardFieldValue(PXGraph graph, object objectRow, string fieldName)
  {
    if (fieldName.ToUpper() == typeof (FSAppointment.wildCard_AssignedEmployeesList).Name.ToUpper())
    {
      StringBuilder stringBuilder = new StringBuilder();
      return FSAppointment.GetsEmployeesContactInfo(graph, true, false, ((FSAppointment) objectRow).AppointmentID).ToString();
    }
    if (fieldName.ToUpper() == typeof (FSAppointment.wildCard_AssignedEmployeesCellPhoneList).Name.ToUpper())
    {
      StringBuilder stringBuilder = new StringBuilder();
      return FSAppointment.GetsEmployeesContactInfo(graph, false, true, ((FSAppointment) objectRow).AppointmentID).ToString();
    }
    if (fieldName.ToUpper() == typeof (FSAppointment.wildCard_CustomerPrimaryContact).Name.ToUpper())
    {
      StringBuilder stringBuilder = new StringBuilder();
      PXResultset<PX.Objects.CR.Contact> bqlResultSet = FSAppointment.ReturnsContactList(graph, ((FSAppointment) objectRow).AppointmentID);
      return bqlResultSet == null ? stringBuilder.ToString() : FSAppointment.ConcatenatesContactInfo(bqlResultSet, true, false).ToString();
    }
    if (!(fieldName.ToUpper() == typeof (FSAppointment.wildCard_CustomerPrimaryContactCell).Name.ToUpper()))
      return (string) null;
    StringBuilder stringBuilder1 = new StringBuilder();
    PXResultset<PX.Objects.CR.Contact> bqlResultSet1 = FSAppointment.ReturnsContactList(graph, ((FSAppointment) objectRow).AppointmentID);
    return bqlResultSet1 == null ? stringBuilder1.ToString() : FSAppointment.ConcatenatesContactInfo(bqlResultSet1, false, true).ToString();
  }

  /// <summary>
  /// Replace WildCards existing inside the "body" string. It assumes that all WildCards are between "((" and "))".
  /// </summary>
  public static void ReplaceWildCards(PXGraph graph, ref string body, object objectRow)
  {
    if (objectRow == null || string.IsNullOrEmpty(body))
      return;
    StringBuilder stringBuilder = new StringBuilder(body);
    int num1 = stringBuilder.ToString().IndexOf("((");
    for (int index = stringBuilder.ToString().IndexOf("))"); num1 != -1 && index != -1; index = stringBuilder.ToString().IndexOf("))", index + "))".Length))
    {
      string str = stringBuilder.ToString(num1 + "((".Length, index - num1 - "))".Length);
      int num2 = str.IndexOf(".");
      string fieldName = str.Substring(num2 + 1, str.Length - num2 - 1);
      string wildCardFieldValue = FSAppointment.GetWildCardFieldValue(graph, objectRow, fieldName);
      if (wildCardFieldValue != null)
      {
        stringBuilder = stringBuilder.Remove(num1, index + "))".Length - num1).Insert(num1, wildCardFieldValue);
        index = num1;
      }
      num1 = stringBuilder.ToString().IndexOf("((", num1 + "((".Length);
    }
    body = stringBuilder.ToString();
  }

  public class PK : PrimaryKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.refNbr>
  {
    public static FSAppointment Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (FSAppointment) PrimaryKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.refNbr>.FindBy(graph, (object) srvOrdType, (object) refNbr, options);
    }
  }

  public class UK : PrimaryKeyOf<FSAppointment>.By<FSAppointment.appointmentID>
  {
    public static FSAppointment Find(PXGraph graph, int? appointmentID, PKFindOptions options = 0)
    {
      return (FSAppointment) PrimaryKeyOf<FSAppointment>.By<FSAppointment.appointmentID>.FindBy(graph, (object) appointmentID, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSAppointment>.By<FSAppointment.customerID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FSAppointment>.By<FSAppointment.branchID>
    {
    }

    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSAppointment>.By<FSAppointment.srvOrdType>
    {
    }

    public class ServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>.ForeignKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.soRefNbr>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<FSAppointment>.By<FSAppointment.projectID>
    {
    }

    public class DefaultTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<FSAppointment>.By<FSAppointment.projectID, FSAppointment.dfltProjectTaskID>
    {
    }

    public class WorkFlowStage : 
      PrimaryKeyOf<FSWFStage>.By<FSWFStage.wFStageID>.ForeignKeyOf<FSAppointment>.By<FSAppointment.wFStageID>
    {
    }

    public class ServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<FSAppointment>.By<FSAppointment.serviceContractID>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<FSSchedule>.By<FSSchedule.scheduleID>.ForeignKeyOf<FSAppointment>.By<FSAppointment.scheduleID>
    {
    }

    public class BillServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<FSAppointment>.By<FSAppointment.billServiceContractID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<FSAppointment>.By<FSAppointment.taxZoneID>
    {
    }

    public class Route : 
      PrimaryKeyOf<FSRoute>.By<FSRoute.routeID>.ForeignKeyOf<FSAppointment>.By<FSAppointment.routeID>
    {
    }

    public class RouteDocument : 
      PrimaryKeyOf<FSRouteDocument>.By<FSRouteDocument.routeDocumentID>.ForeignKeyOf<FSAppointment>.By<FSAppointment.routeDocumentID>
    {
    }

    public class Vehicle : 
      PrimaryKeyOf<FSVehicle>.By<FSVehicle.SMequipmentID>.ForeignKeyOf<FSAppointment>.By<FSAppointment.vehicleID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<FSAppointment>.By<FSAppointment.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<FSAppointment>.By<FSAppointment.curyID>
    {
    }

    public class SalesPerson : 
      PrimaryKeyOf<PX.Objects.AR.SalesPerson>.By<PX.Objects.AR.SalesPerson.salesPersonID>.ForeignKeyOf<FSAppointment>.By<FSAppointment.salesPersonID>
    {
    }
  }

  public class Events : PXEntityEventBase<FSAppointment>.Container<FSAppointment.Events>
  {
    public PXEntityEvent<FSAppointment> ServiceContractCleared;
    public PXEntityEvent<FSAppointment> ServiceContractPeriodAssigned;
    public PXEntityEvent<FSAppointment> ServiceContractPeriodCleared;
    public PXEntityEvent<FSAppointment> RequiredServiceContractPeriodCleared;
    public PXEntityEvent<FSAppointment> AppointmentUnposted;
    public PXEntityEvent<FSAppointment> AppointmentPosted;
    public PXEntityEvent<FSAppointment> AppointmentStatusChanged;
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointment.srvOrdType>
  {
  }

  /// <summary>
  /// TODO: AC-233462 Code Refactoring - Removing FSAppointment.SrvOrdTypeCode
  /// </summary>
  public abstract class srvOrdTypeCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointment.srvOrdTypeCode>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointment.refNbr>
  {
  }

  public abstract class appointmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.appointmentID>
  {
  }

  public abstract class workflowTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointment.workflowTypeID>
  {
    public abstract class Values : ListField.ServiceOrderWorkflowTypes
    {
    }
  }

  public abstract class soRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointment.soRefNbr>
  {
  }

  public abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.sOID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.branchID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.customerID>
  {
  }

  public abstract class billCustomerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.billCustomerID>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointment.docDesc>
  {
  }

  public abstract class scheduledDateTimeBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointment.scheduledDateTimeBegin>
  {
  }

  public abstract class handleManuallyScheduleTime : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.handleManuallyScheduleTime>
  {
  }

  public abstract class scheduledDateTimeEnd : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointment.scheduledDateTimeEnd>
  {
  }

  public abstract class executionDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointment.executionDate>
  {
  }

  public abstract class actualDateTimeBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointment.actualDateTimeBegin>
  {
  }

  public abstract class handleManuallyActualTime : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.handleManuallyActualTime>
  {
  }

  public abstract class actualDateTimeEnd : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointment.actualDateTimeEnd>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointment.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSAppointment.curyInfoID>
  {
  }

  public abstract class autoDocDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointment.autoDocDesc>
  {
  }

  public abstract class confirmed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointment.confirmed>
  {
  }

  public abstract class deliveryNotes : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointment.deliveryNotes>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.projectID>
  {
  }

  public abstract class dfltProjectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointment.dfltProjectTaskID>
  {
  }

  public abstract class longDescr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointment.longDescr>
  {
  }

  public abstract class notStarted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointment.notStarted>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointment.hold>
  {
  }

  public abstract class awaiting : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointment.awaiting>
  {
  }

  public abstract class inProcess : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointment.inProcess>
  {
  }

  public abstract class paused : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointment.paused>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointment.completed>
  {
  }

  public abstract class closed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointment.closed>
  {
  }

  public abstract class canceled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointment.canceled>
  {
  }

  public abstract class billed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointment.billed>
  {
  }

  public abstract class generatedByContract : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.generatedByContract>
  {
  }

  public abstract class userConfirmedUnclosing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.userConfirmedUnclosing>
  {
  }

  public abstract class startActionRunning : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.startActionRunning>
  {
  }

  public abstract class pauseActionRunning : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.pauseActionRunning>
  {
  }

  public abstract class resumeActionRunning : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.resumeActionRunning>
  {
  }

  public abstract class completeActionRunning : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.completeActionRunning>
  {
  }

  public abstract class closeActionRunning : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.closeActionRunning>
  {
  }

  public abstract class unCloseActionRunning : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.unCloseActionRunning>
  {
  }

  public abstract class cancelActionRunning : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.cancelActionRunning>
  {
  }

  public abstract class reopenActionRunning : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.reopenActionRunning>
  {
  }

  public abstract class reloadServiceOrderRelated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.reloadServiceOrderRelated>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointment.status>
  {
    public abstract class Values : ListField.AppointmentStatus
    {
    }
  }

  public abstract class areActualFieldsActive : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.areActualFieldsActive>
  {
  }

  public abstract class effDocDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSAppointment.effDocDate>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.lineCntr>
  {
  }

  public abstract class splitLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.splitLineCntr>
  {
  }

  public abstract class logLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.logLineCntr>
  {
  }

  public abstract class employeeLineCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointment.employeeLineCntr>
  {
  }

  public abstract class pendingPOLineCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointment.pendingPOLineCntr>
  {
  }

  public abstract class pendingApptPOLineCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointment.pendingApptPOLineCntr>
  {
  }

  public abstract class apBillLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.apBillLineCntr>
  {
  }

  public abstract class staffCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.staffCntr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSAppointment.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSAppointment.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointment.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointment.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSAppointment.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointment.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointment.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSAppointment.Tstamp>
  {
  }

  public abstract class estimatedDurationTotal : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointment.estimatedDurationTotal>
  {
  }

  public abstract class actualDurationTotal : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointment.actualDurationTotal>
  {
  }

  public abstract class driveTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.driveTime>
  {
  }

  public abstract class mapLatitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointment.mapLatitude>
  {
  }

  public abstract class mapLongitude : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.mapLongitude>
  {
  }

  public abstract class routePosition : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.routePosition>
  {
  }

  public abstract class timeLocked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointment.timeLocked>
  {
  }

  public abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointment.serviceContractID>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.scheduleID>
  {
  }

  public abstract class originalAppointmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointment.originalAppointmentID>
  {
  }

  public abstract class unreachedCustomer : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.unreachedCustomer>
  {
  }

  public abstract class routeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.routeID>
  {
  }

  public abstract class routeDocumentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.routeDocumentID>
  {
  }

  public abstract class validatedByDispatcher : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.validatedByDispatcher>
  {
  }

  public abstract class vehicleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.vehicleID>
  {
  }

  public abstract class generationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.generationID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointment.finPeriodID>
  {
  }

  public abstract class wFStageID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.wFStageID>
  {
  }

  public abstract class timeRegistered : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointment.timeRegistered>
  {
  }

  public abstract class CustomerSignaturePath : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointment.CustomerSignaturePath>
  {
  }

  public abstract class customerSignedReport : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSAppointment.customerSignedReport>
  {
  }

  public abstract class fullNameSignature : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointment.fullNameSignature>
  {
  }

  public abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.salesPersonID>
  {
  }

  public abstract class commissionable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointment.commissionable>
  {
  }

  public abstract class pendingAPARSOPost : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.pendingAPARSOPost>
  {
  }

  public abstract class pendingINPost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointment.pendingINPost>
  {
  }

  public abstract class postingStatusAPARSO : ListField_Status_Posting
  {
  }

  public abstract class postingStatusIN : ListField_Status_Posting
  {
  }

  public abstract class cutOffDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSAppointment.cutOffDate>
  {
  }

  public abstract class gPSLatitudeStart : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.gPSLatitudeStart>
  {
  }

  public abstract class gPSLongitudeStart : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.gPSLongitudeStart>
  {
  }

  public abstract class gPSLatitudeComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.gPSLatitudeComplete>
  {
  }

  public abstract class gPSLongitudeComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.gPSLongitudeComplete>
  {
  }

  public abstract class estimatedLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.estimatedLineTotal>
  {
  }

  public abstract class curyEstimatedLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.curyEstimatedLineTotal>
  {
  }

  public abstract class estimatedCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.estimatedCostTotal>
  {
  }

  public abstract class curyEstimatedCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.curyEstimatedCostTotal>
  {
  }

  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointment.lineTotal>
  {
  }

  public abstract class curyLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.curyLineTotal>
  {
  }

  public abstract class logBillableTranAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.logBillableTranAmountTotal>
  {
  }

  public abstract class curyLogBillableTranAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.curyLogBillableTranAmountTotal>
  {
  }

  public abstract class billableLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.billableLineTotal>
  {
  }

  public abstract class curyBillableLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.curyBillableLineTotal>
  {
  }

  public abstract class billServiceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointment.billServiceContractID>
  {
  }

  public abstract class billContractPeriodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointment.billContractPeriodID>
  {
  }

  public abstract class curyCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.curyCostTotal>
  {
  }

  public abstract class costTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointment.costTotal>
  {
  }

  public abstract class profitPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.profitPercent>
  {
  }

  public abstract class profitMarginPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.profitMarginPercent>
  {
  }

  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.curyVatExemptTotal>
  {
  }

  public abstract class vatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.vatExemptTotal>
  {
  }

  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.curyVatTaxableTotal>
  {
  }

  public abstract class vatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.vatTaxableTotal>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointment.taxZoneID>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointment.taxCalcMode>
  {
  }

  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointment.taxTotal>
  {
  }

  public abstract class curyTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.curyTaxTotal>
  {
  }

  public abstract class discTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointment.discTot>
  {
  }

  public abstract class curyDiscTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointment.curyDiscTot>
  {
  }

  public abstract class docTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointment.docTotal>
  {
  }

  public abstract class curyDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.curyDocTotal>
  {
  }

  public abstract class curyLineDocDiscountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.curyLineDocDiscountTotal>
  {
  }

  public abstract class docDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointment.docDisc>
  {
  }

  public abstract class curyDocDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointment.curyDocDisc>
  {
  }

  public abstract class skipExternalTaxCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.skipExternalTaxCalculation>
  {
  }

  public abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointment.isTaxValid>
  {
  }

  public abstract class minLogTimeBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointment.minLogTimeBegin>
  {
  }

  public abstract class maxLogTimeEnd : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointment.maxLogTimeEnd>
  {
  }

  public abstract class waitingForParts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.waitingForParts>
  {
  }

  public abstract class finished : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointment.finished>
  {
  }

  public abstract class appCompletedBillableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.appCompletedBillableTotal>
  {
  }

  public abstract class intTravelInProcess : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointment.intTravelInProcess>
  {
  }

  public abstract class travelInProcess : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.travelInProcess>
  {
  }

  public abstract class primaryDriver : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.primaryDriver>
  {
  }

  public abstract class rOOptimizationStatus : ListField_Status_ROOptimization
  {
  }

  public abstract class rOOriginalSortOrder : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointment.rOOriginalSortOrder>
  {
  }

  public abstract class rOSortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.rOSortOrder>
  {
  }

  public abstract class maxLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.maxLineNbr>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointment.selected>
  {
  }

  public abstract class mem_InvoiceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointment.mem_InvoiceDate>
  {
  }

  public abstract class mem_InvoiceDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointment.mem_InvoiceDocType>
  {
  }

  public abstract class mem_BatchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointment.mem_BatchNbr>
  {
  }

  public abstract class mem_InvoiceRef : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointment.mem_InvoiceRef>
  {
  }

  public abstract class mem_ScheduledHours : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.mem_ScheduledHours>
  {
  }

  public abstract class mem_AppointmentHours : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.mem_AppointmentHours>
  {
  }

  public abstract class mem_IdleRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.mem_IdleRate>
  {
  }

  public abstract class mem_OccupationalRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.mem_OccupationalRate>
  {
  }

  public abstract class mem_BusinessDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointment.mem_BusinessDateTime>
  {
  }

  public abstract class scheduledDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointment.scheduledDuration>
  {
  }

  public abstract class actualDuration : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointment.actualDuration>
  {
  }

  public abstract class mem_ActualDateTimeBegin_Time : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointment.mem_ActualDateTimeBegin_Time>
  {
  }

  public abstract class mem_ActualDateTimeEnd_Time : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointment.mem_ActualDateTimeEnd_Time>
  {
  }

  public abstract class wildCard_AssignedEmployeesList : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointment.wildCard_AssignedEmployeesList>
  {
  }

  public abstract class wildCard_AssignedEmployeesCellPhoneList : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointment.wildCard_AssignedEmployeesCellPhoneList>
  {
  }

  /// <summary>
  /// This memory field is used to store the names from the contact(s) associated to a given customer.
  /// </summary>
  public abstract class wildCard_CustomerPrimaryContact : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointment.wildCard_CustomerPrimaryContact>
  {
  }

  /// <summary>
  /// This memory field is used to store the cellphones from the contact(s) associated to a given customer.
  /// </summary>
  public abstract class wildCard_CustomerPrimaryContactCell : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointment.wildCard_CustomerPrimaryContactCell>
  {
  }

  public abstract class mem_ScheduledTimeBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointment.mem_ScheduledTimeBegin>
  {
  }

  public abstract class scheduledDateBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointment.scheduledDateBegin>
  {
  }

  public abstract class mem_ActualDateTime_Month : ListField_Month
  {
  }

  public abstract class mem_ActualDateTime_Year : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointment.mem_ActualDateTime_Year>
  {
  }

  public abstract class isRouteAppoinment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.isRouteAppoinment>
  {
  }

  public abstract class isPrepaymentEnable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.isPrepaymentEnable>
  {
  }

  public abstract class isReassigned : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointment.isReassigned>
  {
  }

  public abstract class mem_SMequipmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointment.mem_SMequipmentID>
  {
  }

  public abstract class actualDurationTotalReport : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointment.actualDurationTotalReport>
  {
  }

  public abstract class appointmentRefReport : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointment.appointmentRefReport>
  {
  }

  public abstract class mem_GPSLatitudeLongitude : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointment.mem_GPSLatitudeLongitude>
  {
  }

  public abstract class actualDateTimeBeginUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointment.actualDateTimeBeginUTC>
  {
  }

  public abstract class actualDateTimeEndUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointment.actualDateTimeEndUTC>
  {
  }

  public abstract class scheduledDateTimeBeginUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointment.scheduledDateTimeBeginUTC>
  {
  }

  public abstract class scheduledDateTimeEndUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointment.scheduledDateTimeEndUTC>
  {
  }

  public abstract class isCalledFromQuickProcess : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.isCalledFromQuickProcess>
  {
  }

  public abstract class isPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointment.isPosted>
  {
  }

  public abstract class travelCanBeStarted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.travelCanBeStarted>
  {
  }

  public abstract class travelCanBeCompleted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.travelCanBeCompleted>
  {
  }

  public abstract class mustUpdateServiceOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.mustUpdateServiceOrder>
  {
  }

  public abstract class trackTimeChanged : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.trackTimeChanged>
  {
  }

  public abstract class curyActualBillableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.curyActualBillableTotal>
  {
  }

  public abstract class actualBillableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointment.actualBillableTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.FS.FSAppointment.ExternalTaxExemptionNumber" />
  public abstract class externalTaxExemptionNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointment.externalTaxExemptionNumber>
  {
  }

  /// <inheritdoc cref="!:AvalaraCustomerUsageType" />
  public abstract class entityUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointment.entityUsageType>
  {
  }

  public abstract class editActionRunning : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointment.editActionRunning>
  {
  }
}
