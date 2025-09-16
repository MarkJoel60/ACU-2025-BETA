// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.DBoxDocSettings
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.PM;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class DBoxDocSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Destination Document", Visible = false)]
  [ListField_Billing_By.ListAtrribute]
  public virtual 
  #nullable disable
  string DestinationDocument { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Customer ID")]
  [FSSelectorBusinessAccount_CU_PR_VC]
  public virtual int? CustomerID { get; set; }

  [PXString(4, IsFixed = true, InputMask = ">AAAA")]
  [PXDefault(typeof (Coalesce<Search<FSxUserPreferences.dfltSrvOrdType, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>>>, Search<FSSetup.dfltSrvOrdType>>))]
  [PXUIField(DisplayName = "Service Order Type")]
  [FSSelectorActiveSrvOrdType]
  public virtual string SrvOrdType { get; set; }

  [PXInt]
  [PXDefault]
  [PXUIField(DisplayName = "Branch")]
  [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public virtual int? BranchID { get; set; }

  [PXInt]
  [PXDefault(typeof (Search<FSxUserPreferences.dfltBranchLocationID, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>, And<UserPreferences.defBranchID, Equal<Current<DBoxDocSettings.branchID>>>>>))]
  [PXFormula(typeof (Default<DBoxDocSettings.branchID>))]
  [PXUIField(DisplayName = "Branch Location")]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<DBoxDocSettings.branchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  public virtual int? BranchLocationID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description { get; set; }

  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Details")]
  public virtual string LongDescr { get; set; }

  [ProjectDefault]
  [ProjectBase(typeof (DBoxDocSettings.customerID))]
  public virtual int? ProjectID { get; set; }

  [PXInt]
  [PXFormula(typeof (Default<DBoxDocSettings.projectID>))]
  [PXUIField]
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<DBoxDocSettings.projectID>>, And<PMTask.isDefault, Equal<True>, And<PMTask.isCompleted, Equal<False>, And<PMTask.isCancelled, Equal<False>>>>>>))]
  [FSSelectorActive_AR_SO_ProjectTask(typeof (Where<PMTask.projectID, Equal<Current<DBoxDocSettings.projectID>>>), typeof (On<FSSrvOrdType.srvOrdType, Equal<Current<DBoxDocSettings.srvOrdType>>, Or<Current<DBoxDocSettings.srvOrdType>, IsNull>>))]
  public virtual int? ProjectTaskID { get; set; }

  [PXDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Order Date")]
  public virtual DateTime? OrderDate { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "SLA")]
  [PXUIField(DisplayName = "SLA")]
  public virtual DateTime? SLAETA { get; set; }

  [PXInt]
  [FSSelector_StaffMember_All(null)]
  [PXUIField(DisplayName = "Supervisor")]
  public virtual int? AssignedEmpID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Problem")]
  [PXSelector(typeof (Search2<FSProblem.problemID, InnerJoin<FSSrvOrdTypeProblem, On<FSProblem.problemID, Equal<FSSrvOrdTypeProblem.problemID>>, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSSrvOrdTypeProblem.srvOrdType>>>>, Where<FSSrvOrdType.srvOrdType, Equal<Current<DBoxDocSettings.srvOrdType>>>>), SubstituteKey = typeof (FSProblem.problemCD), DescriptionField = typeof (FSProblem.descr))]
  public virtual int? ProblemID { get; set; }

  /// <exclude />
  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Contact")]
  [FSSelectorContact(typeof (PMProject.customerID))]
  public virtual int? ContactID { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIVisible(typeof (BqlOperand<Current<DBoxDocSettings.destinationDocument>, IBqlString>.IsEqual<ListField_Billing_By.Appointment>))]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? HandleManuallyScheduleTime { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Scheduled Start Date", DisplayNameTime = "Scheduled Start Time")]
  [PXDefault]
  [PXUIVisible(typeof (BqlOperand<Current<DBoxDocSettings.destinationDocument>, IBqlString>.IsEqual<ListField_Billing_By.Appointment>))]
  [PXUIField]
  public virtual DateTime? ScheduledDateTimeBegin { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Scheduled End Date", DisplayNameTime = "Scheduled End Time")]
  [PXDefault(typeof (Switch<Case<Where<DBoxDocSettings.handleManuallyScheduleTime, Equal<True>>, DBoxDocSettings.scheduledDateTimeBegin>, Null>))]
  [PXFormula(typeof (Default<DBoxDocSettings.handleManuallyScheduleTime, DBoxDocSettings.scheduledDateTimeBegin>))]
  [PXUIVisible(typeof (BqlOperand<Current<DBoxDocSettings.destinationDocument>, IBqlString>.IsEqual<ListField_Billing_By.Appointment>))]
  [PXUIEnabled(typeof (DBoxDocSettings.handleManuallyScheduleTime))]
  [PXUIField(DisplayName = "Scheduled End Date")]
  public virtual DateTime? ScheduledDateTimeEnd { get; set; }

  public abstract class destinationDocument : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DBoxDocSettings.destinationDocument>
  {
    public abstract class Values : ListField_Billing_By
    {
    }
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DBoxDocSettings.customerID>
  {
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DBoxDocSettings.srvOrdType>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DBoxDocSettings.branchID>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DBoxDocSettings.branchLocationID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DBoxDocSettings.description>
  {
  }

  public abstract class details : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DBoxDocSettings.details>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DBoxDocSettings.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DBoxDocSettings.projectTaskID>
  {
  }

  public abstract class orderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  DBoxDocSettings.orderDate>
  {
  }

  public abstract class sLAETA : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  DBoxDocSettings.sLAETA>
  {
  }

  public abstract class assignedEmpID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DBoxDocSettings.assignedEmpID>
  {
  }

  public abstract class problemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DBoxDocSettings.problemID>
  {
  }

  /// <exclude />
  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DBoxDocSettings.contactID>
  {
  }

  public abstract class handleManuallyScheduleTime : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DBoxDocSettings.handleManuallyScheduleTime>
  {
  }

  public abstract class scheduledDateTimeBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DBoxDocSettings.scheduledDateTimeBegin>
  {
  }

  public abstract class scheduledDateTimeEnd : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DBoxDocSettings.scheduledDateTimeEnd>
  {
  }
}
