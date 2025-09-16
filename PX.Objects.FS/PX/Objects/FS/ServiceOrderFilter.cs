// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceOrderFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class ServiceOrderFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  [PXUIField(DisplayName = "Customer")]
  [FSSelectorCustomer]
  public virtual int? CustomerID { get; set; }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<ServiceOrderFilter.customerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Customer Location", DirtyRead = true)]
  public virtual int? CustomerLocationID { get; set; }

  [PXInt]
  [PXUIField]
  [Service(null)]
  public virtual int? ServiceID { get; set; }

  [PXString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type")]
  [FSSelectorActiveSrvOrdType]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "From Date")]
  public virtual DateTime? FromDate { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "To Date")]
  public virtual DateTime? ToDate { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Reported")]
  public virtual bool? ReportedFlag { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Checked Out")]
  public virtual bool? CheckOutFlag { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Signed Off")]
  public virtual bool? SignOffFlag { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Deadline - SLA")]
  public virtual bool? SLAFlag { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Completed")]
  public virtual bool? CompletedOrder { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Not Signed Off")]
  public virtual bool? NotSignedOff { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Signed Off")]
  public virtual bool? SignedOff { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Not Checked Out")]
  public virtual bool? NotCheckedOut { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Checked Out")]
  public virtual bool? CheckedOut { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Posted")]
  public virtual bool? Posted { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Not Posted")]
  public virtual bool? NotPosted { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Workflow Stage ID")]
  [PXSelector(typeof (Search2<FSWFStage.wFStageID, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdTypeID, Equal<FSWFStage.wFID>>>, Where<FSSrvOrdType.srvOrdType, Equal<Current<ServiceOrderFilter.srvOrdType>>>, OrderBy<Asc<FSWFStage.parentWFStageID, Asc<FSWFStage.sortOrder>>>>), SubstituteKey = typeof (FSWFStage.wFStageCD))]
  public virtual int? WFStageID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Service Contract ID")]
  [PXSelector(typeof (Search<FSServiceContract.serviceContractID, Where2<Where<FSServiceContract.recordType, Equal<ListField_RecordType_ContractSchedule.ServiceContract>, Or<FSServiceContract.recordType, Equal<ListField_RecordType_ContractSchedule.RouteServiceContract>>>, And<Where<Current<ServiceOrderFilter.customerID>, IsNull, Or<FSServiceContract.customerID, Equal<Current<ServiceOrderFilter.customerID>>>>>>>), SubstituteKey = typeof (FSServiceContract.refNbr))]
  public virtual int? ServiceContractID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Schedule ID")]
  [PXSelector(typeof (Search<FSSchedule.scheduleID, Where<FSSchedule.entityID, Equal<Current<ServiceOrderFilter.serviceContractID>>, And<FSSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>>>, OrderBy<Desc<FSSchedule.refNbr>>>), SubstituteKey = typeof (FSSchedule.refNbr))]
  public virtual int? ScheduleID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Billing Cycle ID")]
  [PXSelector(typeof (FSBillingCycle.billingCycleID), SubstituteKey = typeof (FSBillingCycle.billingCycleCD))]
  public virtual int? BillingCycleID { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "No Billing Cycle")]
  public virtual bool? BillingCycleNone { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Branch")]
  [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public virtual int? BranchID { get; set; }

  [PXInt]
  [PXDefault(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<ServiceContractFilter.branchID>>>>))]
  [PXUIField(DisplayName = "Branch Location")]
  [FSSelectorBranchLocation]
  public virtual int? BranchLocationID { get; set; }

  [PXString(2, IsFixed = true)]
  [ListField_ServiceOrder_Action_Filter.ListAtrribute]
  [PXUIField(DisplayName = "Action")]
  [PXDefault("UD")]
  public virtual string SOAction { get; set; }

  [PXString(1, IsFixed = true)]
  [PXUIField]
  [ListField.ServiceOrderStatus.List]
  public virtual string Status { get; set; }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceOrderFilter.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ServiceOrderFilter.customerLocationID>
  {
  }

  public abstract class serviceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceOrderFilter.serviceID>
  {
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ServiceOrderFilter.srvOrdType>
  {
  }

  public abstract class fromDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ServiceOrderFilter.fromDate>
  {
  }

  public abstract class toDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ServiceOrderFilter.toDate>
  {
  }

  public abstract class reportedFlag : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ServiceOrderFilter.reportedFlag>
  {
  }

  public abstract class checkOutFlag : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ServiceOrderFilter.checkOutFlag>
  {
  }

  public abstract class signOffFlag : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ServiceOrderFilter.signOffFlag>
  {
  }

  public abstract class sLAFlag : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ServiceOrderFilter.sLAFlag>
  {
  }

  public abstract class completedOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ServiceOrderFilter.completedOrder>
  {
  }

  public abstract class notSignedOff : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ServiceOrderFilter.notSignedOff>
  {
  }

  public abstract class signedOff : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ServiceOrderFilter.signedOff>
  {
  }

  public abstract class notCheckedOut : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ServiceOrderFilter.notCheckedOut>
  {
  }

  public abstract class checkedOut : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ServiceOrderFilter.checkedOut>
  {
  }

  public abstract class posted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ServiceOrderFilter.posted>
  {
  }

  public abstract class notPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ServiceOrderFilter.notPosted>
  {
  }

  public abstract class wFStageID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceOrderFilter.wFStageID>
  {
  }

  public abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ServiceOrderFilter.serviceContractID>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceOrderFilter.scheduleID>
  {
  }

  public abstract class billingCycleID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ServiceOrderFilter.billingCycleID>
  {
  }

  public abstract class billingCycleNone : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ServiceOrderFilter.billingCycleNone>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceOrderFilter.branchID>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ServiceOrderFilter.branchLocationID>
  {
  }

  public abstract class soAction : ListField_ServiceOrder_Action_Filter
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ServiceOrderFilter.status>
  {
    public abstract class Values : ListField.ServiceOrderStatus
    {
    }
  }
}
