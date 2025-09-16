// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentLog
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.FS.Attributes;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Log")]
[Serializable]
public class FSAppointmentLog : FSLog
{
  [PXDBIdentity]
  public override int? LogID { get; set; }

  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type", Visible = false, Enabled = false)]
  [PXDefault(typeof (FSAppointment.srvOrdType))]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType>), CacheGlobal = true)]
  public override 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Appointment Nbr.", Visible = false, Enabled = false)]
  [PXDBDefault(typeof (FSAppointment.refNbr), DefaultForUpdate = false)]
  [PXParent(typeof (Select<FSAppointment, Where<FSAppointment.srvOrdType, Equal<Current<FSAppointmentLog.docType>>, And<FSAppointment.refNbr, Equal<Current<FSAppointmentLog.docRefNbr>>>>>))]
  public override string DocRefNbr { get; set; }

  [PXDBInt]
  [PXDBDefault(typeof (FSAppointment.appointmentID))]
  [PXUIField(DisplayName = "Appointment Ref. Nbr.", Visible = false, Enabled = false)]
  public override int? DocID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (FSAppointment.logLineCntr))]
  public override int? LineNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Staff Member")]
  [FSSelector_StaffMember_ServiceOrderProjectID]
  [PXForeignReference(typeof (Field<FSAppointmentLog.bAccountID>.IsRelatedTo<PX.Objects.EP.EPEmployee.bAccountID>))]
  public override int? BAccountID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField]
  [PX.Objects.CR.BAccountType.List]
  public override string BAccountType { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXDefault]
  [PXUIRequired(typeof (Where<FSAppointmentLog.bAccountID, IsNull>))]
  [FSSelectorAppointmentSODetID(typeof (Where2<Where<Current<FSAppointment.notStarted>, Equal<False>, Or<Where<Current<FSAppointment.startActionRunning>, Equal<True>, And<Current<FSAppointment.notStarted>, Equal<True>>>>>, Or<FSAppointmentDet.isTravelItem, Equal<True>>>))]
  [PXRestrictor(typeof (Where<FSAppointmentDet.status, NotEqual<FSAppointmentDet.ListField_Status_AppointmentDet.Canceled>, And<FSAppointmentDet.status, NotEqual<FSAppointmentDet.ListField_Status_AppointmentDet.NotFinished>, And<FSAppointmentDet.status, NotEqual<FSAppointmentDet.ListField_Status_AppointmentDet.NotPerformed>, And<FSAppointmentDet.status, NotEqual<FSAppointmentDet.ListField_Status_AppointmentDet.waitingForPO>, And<FSAppointmentDet.status, NotEqual<FSAppointmentDet.ListField_Status_AppointmentDet.requestForPO>>>>>>), "The {0} line cannot be used because it has the {1} status.", new System.Type[] {typeof (FSAppointmentDet.lineRef), typeof (FSAppointmentDet.status)})]
  [PXUIField(DisplayName = "Detail Ref. Nbr.")]
  public override string DetLineRef { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Log Type", Enabled = false)]
  [ListField_Log_ItemType.List]
  [PXDefault(typeof (Switch<Case<Where<Current<FSAppointment.notStarted>, Equal<True>, And<Current<FSAppointment.startActionRunning>, Equal<False>>>, ListField_Log_ItemType.travel>, ListField_Log_ItemType.staff>))]
  public override string ItemType { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_Status_Log.ListAtrribute]
  [PXDefault(typeof (Switch<Case<Where<Current<FSAppointment.completed>, Equal<True>, And<FSAppointmentLog.itemType, NotEqual<ListField_Log_ItemType.travel>>>, ListField_Status_Log.Completed>, ListField_Status_Log.InProcess>))]
  [PXUIField(DisplayName = "Log Line Status")]
  public override string Status { get; set; }

  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<FSAppointmentLog.itemType, Equal<ListField_Log_ItemType.travel>, Or<Current<FSAppointment.hold>, Equal<True>>>, True>, False>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<FSAppointmentLog.travel, Equal<True>, And<Where<FSAppointmentLog.status, Equal<ListField_Status_Log.InProcess>, Or<FSAppointmentLog.status, Equal<ListField_Status_Log.Paused>>>>>, int1>, int0>), typeof (MaxCalc<FSAppointment.intTravelInProcess>))]
  [PXUIField(DisplayName = "Travel")]
  public virtual bool? Travel { get; set; }

  [PXDefault]
  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Start Date", DisplayNameTime = "Start Time")]
  [PXUIField(DisplayName = "Start Date")]
  public override DateTime? DateTimeBegin { get; set; }

  [PXDefault]
  [PXUIVerify]
  [PXUIRequired(typeof (Where<FSAppointmentLog.timeDuration, GreaterEqual<Zero>, And<Where<FSAppointmentLog.status, Equal<ListField_Status_Log.Completed>, Or<FSAppointmentLog.status, Equal<ListField_Status_Log.Paused>>>>>))]
  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "End Date", DisplayNameTime = "End Time")]
  [PXUIField(DisplayName = "End Date")]
  public override DateTime? DateTimeEnd { get; set; }

  [FSDBTimeSpanLongAllowNegative]
  [PXFormula(typeof (Switch<Case<Where<FSAppointmentLog.dateTimeBegin, IsNotNull, And<FSAppointmentLog.dateTimeEnd, IsNotNull, And<FSAppointmentLog.dateTimeEnd, GreaterEqual<FSAppointmentLog.dateTimeBegin>>>>, DateDiff<FSAppointmentLog.dateTimeBegin, FSAppointmentLog.dateTimeEnd, DateDiff.minute>>, Zero>))]
  [PXUIField(DisplayName = "Duration")]
  public override int? TimeDuration { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Approved", Visible = false, Enabled = false)]
  [PXUIVisible(typeof (Where<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>>))]
  public override bool? ApprovedTime { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (FSAppointment.curyInfoID))]
  public override long? CuryInfoID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Description")]
  public override string Descr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [FSDBEarningTypeDefault]
  [PXSelector(typeof (EPEarningType.typeCD))]
  [PXUIField(DisplayName = "Earning Type")]
  [PXUIVisible(typeof (Where<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>>))]
  [PXUIEnabled(typeof (Where<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>>))]
  [PXRestrictor(typeof (Where<EPEarningType.isActive, Equal<True>>), "The earning type {0} selected on the Time & Expenses Preferences (EP101000) form is inactive. Inactive earning types are not available for data entry in new activities and time entries.", new System.Type[] {typeof (EPEarningType.typeCD)})]
  public override string EarningType { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (Where<Current<FSSrvOrdType.allowManualLogTimeEdition>, Equal<True>>))]
  [PXUIVisible(typeof (Where<Current<FSSrvOrdType.allowManualLogTimeEdition>, Equal<True>>))]
  [PXUIField(DisplayName = "Manage Time Manually")]
  public override bool? KeepDateTimes { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Labor Item ID")]
  [PXDimensionSelector("INVENTORY", typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.laborItem>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<Match<Current<AccessInfo.userName>>>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXDefault(typeof (Coalesce<Search<FSAppointmentEmployee.laborItemID, Where<FSAppointmentEmployee.appointmentID, Equal<Current<FSAppointmentLog.docID>>, And<FSAppointmentEmployee.employeeID, Equal<Current<FSAppointmentLog.bAccountID>>, And<Where2<Where<FSAppointmentEmployee.serviceLineRef, IsNull, And<Current<FSAppointmentLog.detLineRef>, IsNull>>, Or<FSAppointmentEmployee.serviceLineRef, Equal<Current<FSAppointmentLog.detLineRef>>>>>>>>, Search<PX.Objects.EP.EPEmployee.labourItemID, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<FSAppointmentLog.bAccountID>>>>>))]
  [PXFormula(typeof (Default<FSAppointmentLog.bAccountID, FSAppointmentLog.detLineRef>))]
  [PXForeignReference(typeof (FSAppointmentLog.FK.LaborItem))]
  public override int? LaborItemID { get; set; }

  [FSDBLineRef(typeof (FSAppointmentLog.lineNbr))]
  [PXDBString(3, IsFixed = true)]
  [PXUIField]
  public override string LineRef { get; set; }

  [PXDefault(typeof (Coalesce<Search<FSAppointmentEmployee.dfltProjectID, Where<FSAppointmentEmployee.appointmentID, Equal<Current<FSAppointmentLog.docID>>, And<FSAppointmentEmployee.employeeID, Equal<Current<FSAppointmentLog.bAccountID>>, And<Where2<Where<FSAppointmentEmployee.serviceLineRef, IsNull, And<Current<FSAppointmentLog.detLineRef>, IsNull>>, Or<FSAppointmentEmployee.serviceLineRef, Equal<Current<FSAppointmentLog.detLineRef>>>>>>>>, Search<FSAppointmentDet.projectID, Where<FSAppointmentDet.appointmentID, Equal<Current<FSAppointmentLog.docID>>, And<FSAppointmentDet.lineRef, Equal<Current<FSAppointmentLog.detLineRef>>>>>>))]
  [PXFormula(typeof (Default<FSAppointmentLog.bAccountID, FSAppointmentLog.detLineRef>))]
  [ProjectBase(typeof (FSServiceOrder.billCustomerID), Visible = false)]
  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.isCancelled, Equal<False>>), "The {0} project or contract is canceled.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXForeignReference(typeof (FSAppointmentLog.FK.Project))]
  public override int? ProjectID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Project Task", FieldClass = "PROJECT")]
  [FSSelectorActive_AR_SO_ProjectTask(typeof (Where<PMTask.projectID, Equal<Current<FSAppointmentLog.projectID>>>))]
  [PXDefault(typeof (Coalesce<Search<FSAppointmentEmployee.dfltProjectTaskID, Where<FSAppointmentEmployee.appointmentID, Equal<Current<FSAppointmentLog.docID>>, And<FSAppointmentEmployee.employeeID, Equal<Current<FSAppointmentLog.bAccountID>>, And<Where2<Where<FSAppointmentEmployee.serviceLineRef, IsNull, And<Current<FSAppointmentLog.detLineRef>, IsNull>>, Or<FSAppointmentEmployee.serviceLineRef, Equal<Current<FSAppointmentLog.detLineRef>>>>>>>>, Search<FSAppointmentDet.projectTaskID, Where<FSAppointmentDet.appointmentID, Equal<Current<FSAppointmentLog.docID>>, And<FSAppointmentDet.lineRef, Equal<Current<FSAppointmentLog.detLineRef>>>>>, Search<FSAppointment.dfltProjectTaskID, Where<FSAppointment.appointmentID, Equal<Current<FSAppointmentLog.docID>>>>>))]
  [PXFormula(typeof (Default<FSAppointmentLog.bAccountID, FSAppointmentLog.detLineRef>))]
  [PXForeignReference(typeof (FSAppointmentLog.FK.Task))]
  public override int? ProjectTaskID { get; set; }

  [SMCostCode(typeof (FSAppointmentLog.skipCostCodeValidation), null, typeof (FSAppointmentLog.projectTaskID))]
  [PXForeignReference(typeof (FSAppointmentLog.FK.CostCode))]
  [PXDefault(typeof (Coalesce<Search<FSAppointmentEmployee.costCodeID, Where<FSAppointmentEmployee.appointmentID, Equal<Current<FSAppointmentLog.docID>>, And<FSAppointmentEmployee.employeeID, Equal<Current<FSAppointmentLog.bAccountID>>, And<Where2<Where<FSAppointmentEmployee.serviceLineRef, IsNull, And<Current<FSAppointmentLog.detLineRef>, IsNull>>, Or<FSAppointmentEmployee.serviceLineRef, Equal<Current<FSAppointmentLog.detLineRef>>>>>>>>, Search<FSAppointmentDet.costCodeID, Where<FSAppointmentDet.appointmentID, Equal<Current<FSAppointmentLog.docID>>, And<FSAppointmentDet.lineRef, Equal<Current<FSAppointmentLog.detLineRef>>>>>, Search2<FSSrvOrdType.dfltCostCodeID, LeftJoin<PMProject, On<PMProject.nonProject, Equal<True>, And<PMProject.contractID, Equal<Current<FSAppointmentLog.projectID>>>>>, Where<FSSrvOrdType.srvOrdType, Equal<Current<FSSrvOrdType.srvOrdType>>, And<PMProject.contractID, IsNull, And<Current<FSAppointmentLog.projectID>, IsNotNull>>>>>))]
  [PXFormula(typeof (Default<FSAppointmentLog.bAccountID, FSAppointmentLog.detLineRef>))]
  public override int? CostCodeID { get; set; }

  [PXDBBool]
  [PXDefault(false, typeof (Coalesce<Search<FSAppointmentEmployee.trackTime, Where<FSAppointmentEmployee.appointmentID, Equal<Current<FSAppointmentLog.docID>>, And<FSAppointmentEmployee.employeeID, Equal<Current<FSAppointmentLog.bAccountID>>, And<Where2<Where<FSAppointmentEmployee.serviceLineRef, IsNull, And<Current<FSAppointmentLog.detLineRef>, IsNull>>, Or<FSAppointmentEmployee.serviceLineRef, Equal<Current<FSAppointmentLog.detLineRef>>>>>>>>, Search<FSSrvOrdType.createTimeActivitiesFromAppointment, Where<FSSrvOrdType.srvOrdType, Equal<Current<FSAppointmentLog.docType>>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>, And<Current<FSAppointmentLog.bAccountID>, IsNotNull, And<Current<FSAppointmentLog.bAccountType>, Equal<PX.Objects.CR.BAccountType.employeeType>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>>>>>>))]
  [PXUIField(DisplayName = "Track Time")]
  [PXUIVisible(typeof (Where<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>>))]
  [PXUIEnabled(typeof (Where<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>>))]
  public override bool? TrackTime { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Add to Actual Duration")]
  [PXFormula(typeof (Default<FSAppointmentLog.bAccountID, FSAppointmentLog.travel, FSAppointmentLog.detLineRef>))]
  [PXUIEnabled(typeof (Where<FSAppointmentLog.detLineRef, IsNotNull>))]
  public override bool? TrackOnService { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCC")]
  [PXUIField(DisplayName = "Time Card Ref. Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<EPTimeCard.timeCardCD>), new System.Type[] {typeof (EPTimeCard.timeCardCD), typeof (EPTimeCard.employeeID), typeof (EPTimeCard.weekDescription), typeof (EPTimeCard.status)})]
  [PXUIVisible(typeof (Where<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>>))]
  public override string TimeCardCD { get; set; }

  [PXDBCurrency(typeof (FSAppointmentLog.curyInfoID), typeof (FSAppointmentLog.unitCost))]
  [PXUIField(Visible = false, Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Default<FSAppointmentLog.laborItemID>))]
  public override Decimal? CuryUnitCost { get; set; }

  [PXDBCurrency(typeof (FSAppointmentLog.curyInfoID), typeof (FSAppointmentLog.extCost))]
  [PXUIField(Enabled = false, Visible = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Default<FSAppointmentLog.curyUnitCost, FSAppointmentLog.timeDuration>), typeof (SumCalc<FSAppointment.curyCostTotal>))]
  public override Decimal? CuryExtCost { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? UnitCost { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Default<FSAppointmentLog.unitCost, FSAppointmentLog.timeDuration>))]
  public override Decimal? ExtCost { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Workgroup")]
  [PXWorkgroupSelector]
  [PXDefault(typeof (Search<PX.Objects.EP.EPEmployee.defaultWorkgroupID, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<FSAppointmentLog.bAccountID>>>>))]
  [PXFormula(typeof (Default<FSAppointmentLog.bAccountID>))]
  public override int? WorkgroupID { get; set; }

  [PXDBBool]
  [PXDefault(typeof (Switch<Case<Where2<Where<Current<FSSrvOrdType.postTo>, Equal<FSPostTo.Projects>, And<Current<FSSrvOrdType.billingType>, Equal<ListField_SrvOrdType_BillingType.CostAsCost>, And<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>, And<FSAppointmentLog.trackTime, Equal<True>>>>>>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>, True>, False>))]
  [PXUIField(DisplayName = "Billable Labor")]
  [PXFormula(typeof (Default<FSAppointmentLog.trackTime>))]
  [PXUIEnabled(typeof (Where2<Where<Current<FSSrvOrdType.postTo>, Equal<FSPostTo.Projects>, And<Current<FSSrvOrdType.billingType>, Equal<ListField_SrvOrdType_BillingType.CostAsCost>, And<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>, And<FSAppointmentLog.trackTime, Equal<True>>>>>>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>))]
  [PXUIVisible(typeof (Where2<Where<Current<FSSrvOrdType.postTo>, Equal<FSPostTo.Projects>, And<Current<FSSrvOrdType.billingType>, Equal<ListField_SrvOrdType_BillingType.CostAsCost>, And<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>>>>>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>))]
  public override bool? IsBillable { get; set; }

  [FSDBTimeSpanLong]
  [PXUIField(DisplayName = "Billable Time")]
  [PXDefault(typeof (Switch<Case<Where<FSAppointmentLog.isBillable, Equal<True>>, FSAppointmentLog.timeDuration>, SharedClasses.int_0>))]
  [PXUIEnabled(typeof (Where<FSAppointmentLog.isBillable, Equal<True>>))]
  [PXFormula(typeof (Default<FSAppointmentLog.timeDuration, FSAppointmentLog.isBillable>))]
  [PXUIVisible(typeof (Where2<Where<Current<FSSrvOrdType.postTo>, Equal<FSPostTo.Projects>, And<Current<FSSrvOrdType.billingType>, Equal<ListField_SrvOrdType_BillingType.CostAsCost>, And<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>>>>>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>))]
  public override int? BillableTimeDuration { get; set; }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Billable Quantity", Enabled = false)]
  [PXFormula(typeof (Default<FSAppointmentLog.billableTimeDuration>))]
  [PXUIEnabled(typeof (Where<FSAppointmentLog.isBillable, Equal<True>>))]
  [PXUIVisible(typeof (Where2<Where<Current<FSSrvOrdType.postTo>, Equal<FSPostTo.Projects>, And<Current<FSSrvOrdType.billingType>, Equal<ListField_SrvOrdType_BillingType.CostAsCost>, And<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>>>>>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>))]
  public override Decimal? BillableQty { get; set; }

  [PXDBCurrency(typeof (FSAppointmentLog.curyInfoID), typeof (FSAppointmentLog.billableTranAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Mult<FSAppointmentLog.curyUnitCost, FSAppointmentLog.billableQty>), typeof (SumCalc<FSAppointment.curyLogBillableTranAmountTotal>))]
  [PXUIField(DisplayName = "Billable Amount", Enabled = false)]
  [PXUIVisible(typeof (Where2<Where<Current<FSSrvOrdType.postTo>, Equal<FSPostTo.Projects>, And<Current<FSSrvOrdType.billingType>, Equal<ListField_SrvOrdType_BillingType.CostAsCost>, And<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>>>>>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>))]
  public override Decimal? CuryBillableTranAmount { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Billable Amount", Enabled = false)]
  public override Decimal? BillableTranAmount { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "CreatedByID")]
  public override Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public override string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "CreatedDateTime")]
  public override DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "LastModifiedByID")]
  public override Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  public override string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "LastModifiedDateTime")]
  public override DateTime? LastModifiedDateTime { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public override Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public override byte[] tstamp { get; set; }

  [PXBool]
  [PXFormula(typeof (IIf<Where<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>>>, False, True>))]
  public override bool? SkipCostCodeValidation { get; set; }

  [PXDateAndTime(UseTimeZone = true)]
  [PXUIField]
  [PXFormula(null, typeof (MinCalc<FSAppointment.minLogTimeBegin>))]
  public virtual DateTime? ServiceDateTimeBegin
  {
    [PXDependsOnFields(new System.Type[] {typeof (FSAppointmentLog.dateTimeBegin)})] get
    {
      return this.ItemType != "TR" ? this.DateTimeBegin : new DateTime?();
    }
  }

  [PXDateAndTime(UseTimeZone = true)]
  [PXFormula(null, typeof (MaxCalc<FSAppointment.maxLogTimeEnd>))]
  [PXUIField]
  public virtual DateTime? ServiceDateTimeEnd
  {
    [PXDependsOnFields(new System.Type[] {typeof (FSAppointmentLog.dateTimeEnd)})] get
    {
      return this.ItemType != "TR" ? this.DateTimeEnd : new DateTime?();
    }
  }

  public new class PK : 
    PrimaryKeyOf<FSAppointmentLog>.By<FSAppointmentLog.docType, FSAppointmentLog.docRefNbr, FSAppointmentLog.lineNbr>
  {
    public static FSAppointmentLog Find(
      PXGraph graph,
      string docType,
      string docRefNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (FSAppointmentLog) PrimaryKeyOf<FSAppointmentLog>.By<FSAppointmentLog.docType, FSAppointmentLog.docRefNbr, FSAppointmentLog.lineNbr>.FindBy(graph, (object) docType, (object) docRefNbr, (object) lineNbr, options);
    }
  }

  public class UK : PrimaryKeyOf<FSAppointmentLog>.By<FSAppointmentLog.logID>
  {
    public static FSAppointmentLog Find(PXGraph graph, int? logID, PKFindOptions options = 0)
    {
      return (FSAppointmentLog) PrimaryKeyOf<FSAppointmentLog>.By<FSAppointmentLog.logID>.FindBy(graph, (object) logID, options);
    }
  }

  public new static class FK
  {
    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSAppointmentLog>.By<FSAppointmentLog.docType>
    {
    }

    public class Appointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.refNbr>.ForeignKeyOf<FSAppointmentLog>.By<FSAppointmentLog.docType, FSAppointmentLog.docRefNbr>
    {
    }

    public class Staff : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<FSAppointmentLog>.By<FSAppointmentLog.bAccountID>
    {
    }

    public class EarningType : 
      PrimaryKeyOf<EPEarningType>.By<EPEarningType.typeCD>.ForeignKeyOf<FSAppointmentLog>.By<FSAppointmentLog.earningType>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<FSAppointmentLog>.By<FSAppointmentLog.costCodeID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<FSAppointmentLog>.By<FSAppointmentLog.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<FSAppointmentLog>.By<FSAppointmentLog.projectID, FSAppointmentLog.projectTaskID>
    {
    }

    public class LaborItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSAppointmentLog>.By<FSAppointmentLog.laborItemID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<FSAppointmentLog>.By<FSAppointmentLog.curyInfoID>
    {
    }

    public class WorkGorupID : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<FSAppointmentLog>.By<FSAppointmentLog.workgroupID>
    {
    }

    public class TimeCard : 
      PrimaryKeyOf<EPTimeCard>.By<EPTimeCard.timeCardCD>.ForeignKeyOf<FSAppointmentLog>.By<FSAppointmentLog.timeCardCD>
    {
    }
  }

  public new abstract class logID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentLog.logID>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentLog.docType>
  {
  }

  public new abstract class docRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentLog.docRefNbr>
  {
  }

  public new abstract class docID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentLog.docID>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentLog.lineNbr>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentLog.bAccountID>
  {
  }

  public new abstract class bAccountType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentLog.bAccountType>
  {
  }

  public new abstract class detLineRef : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentLog.detLineRef>
  {
  }

  public new abstract class itemType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentLog.itemType>
  {
    public abstract class Values : ListField_Log_ItemType
    {
    }
  }

  public new abstract class status : ListField_Status_Log
  {
  }

  public abstract class travel : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentLog.travel>
  {
  }

  public new abstract class dateTimeBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentLog.dateTimeBegin>
  {
  }

  public new abstract class dateTimeEnd : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentLog.dateTimeEnd>
  {
  }

  public new abstract class timeDuration : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentLog.timeDuration>
  {
  }

  public new abstract class approvedTime : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentLog.approvedTime>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSAppointmentLog.curyInfoID>
  {
  }

  public new abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentLog.descr>
  {
  }

  public new abstract class earningType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentLog.earningType>
  {
  }

  public new abstract class keepDateTimes : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentLog.keepDateTimes>
  {
  }

  public new abstract class laborItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentLog.laborItemID>
  {
  }

  public new abstract class lineRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentLog.lineRef>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentLog.projectID>
  {
  }

  public new abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentLog.projectTaskID>
  {
  }

  public new abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentLog.costCodeID>
  {
  }

  public new abstract class trackTime : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentLog.trackTime>
  {
  }

  public new abstract class trackOnService : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentLog.trackOnService>
  {
  }

  public new abstract class timeCardCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentLog.timeCardCD>
  {
  }

  public new abstract class curyUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentLog.curyUnitCost>
  {
  }

  public new abstract class curyExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentLog.curyExtCost>
  {
  }

  public new abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointmentLog.unitCost>
  {
  }

  public new abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointmentLog.extCost>
  {
  }

  public new abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentLog.workgroupID>
  {
  }

  public new abstract class isBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentLog.isBillable>
  {
  }

  public new abstract class billableTimeDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentLog.billableTimeDuration>
  {
  }

  public new abstract class billableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentLog.billableQty>
  {
  }

  public new abstract class curyBillableTranAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentLog.curyBillableTranAmount>
  {
  }

  public new abstract class billableTranAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentLog.billableTranAmount>
  {
  }

  public new abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSAppointmentLog.createdByID>
  {
  }

  public new abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentLog.createdByScreenID>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentLog.createdDateTime>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSAppointmentLog.lastModifiedByID>
  {
  }

  public new abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentLog.lastModifiedByScreenID>
  {
  }

  public new abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentLog.lastModifiedDateTime>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSAppointmentLog.noteID>
  {
  }

  public new abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSAppointmentLog.Tstamp>
  {
  }

  public new abstract class skipCostCodeValidation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentLog.skipCostCodeValidation>
  {
  }

  public new abstract class timeActivityStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentLog.timeActivityStatus>
  {
  }

  public abstract class serviceDateTimeBegin : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentLog.serviceDateTimeBegin>
  {
  }

  public abstract class serviceDateTimeEnd : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentLog.serviceDateTimeEnd>
  {
  }
}
