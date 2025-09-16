// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSRouteContractSchedule
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.PM;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXPrimaryGraph(typeof (RouteServiceContractScheduleEntry))]
[Serializable]
public class FSRouteContractSchedule : FSSchedule
{
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Service Contract ID")]
  [PXSelector(typeof (Search2<FSServiceContract.serviceContractID, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceContract.customerID>>>, Where<FSServiceContract.recordType, Equal<ListField_RecordType_ContractSchedule.RouteServiceContract>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>), SubstituteKey = typeof (FSServiceContract.refNbr), CacheGlobal = true)]
  public override int? EntityID { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXDefault]
  [PXSelector(typeof (Search<FSRouteContractSchedule.refNbr, Where<FSRouteContractSchedule.entityID, Equal<Current<FSRouteContractSchedule.entityID>>, And<FSRouteContractSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>>>, OrderBy<Desc<FSRouteContractSchedule.refNbr>>>))]
  [AutoNumber(typeof (Search<FSSetup.scheduleNumberingID>), typeof (AccessInfo.businessDate))]
  public override 
  #nullable disable
  string RefNbr { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField]
  [PXFormula(typeof (Selector<FSContractSchedule.entityID, FSServiceContract.customerID>))]
  [FSSelectorContractScheduleCustomer(typeof (Where<FSServiceContract.recordType, Equal<ListField_RecordType_ContractSchedule.RouteServiceContract>>), CacheGlobal = true)]
  [PXRestrictor(typeof (Where<PX.Objects.AR.Customer.status, IsNull, Or<PX.Objects.AR.Customer.status, Equal<CustomerStatus.active>, Or<PX.Objects.AR.Customer.status, Equal<CustomerStatus.oneTime>>>>), "The customer status is '{0}'.", new System.Type[] {typeof (PX.Objects.AR.Customer.status)})]
  public override int? CustomerID { get; set; }

  [FSLocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSSchedule.customerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  public override int? CustomerLocationID { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type")]
  [PXDefault(typeof (Coalesce<Search2<FSxUserPreferences.dfltSrvOrdType, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSxUserPreferences.dfltSrvOrdType>>>, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>, And<FSSrvOrdType.behavior, Equal<ListField.ServiceOrderTypeBehavior.routeAppointment>>>>, Search<FSRouteSetup.dfltSrvOrdType>>))]
  [PXRestrictor(typeof (Where<FSSrvOrdType.active, Equal<True>>), null, new System.Type[] {})]
  [FSSelectorRouteContractSrvOrdType]
  public override string SrvOrdType { get; set; }

  [PXDBString(2, IsUnicode = false)]
  [ListField_ScheduleGenType_ContractSchedule.ListAtrribute]
  [PXUIField(DisplayName = "Schedule Generation Type")]
  [PXDefault(typeof (Search<FSServiceContract.scheduleGenType, Where<FSServiceContract.customerID, Equal<Current<FSRouteContractSchedule.customerID>>, And<FSServiceContract.serviceContractID, Equal<Current<FSRouteContractSchedule.entityID>>>>>))]
  public override string ScheduleGenType { get; set; }

  [PXDefault(typeof (Search<FSServiceContract.projectID, Where<FSServiceContract.serviceContractID, Equal<Current<FSRouteContractSchedule.entityID>>, And<Current<FSRouteContractSchedule.entityType>, Equal<ListField_Schedule_EntityType.Contract>>>>))]
  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.isCancelled, Equal<False>>), "The {0} project or contract is canceled.", new System.Type[] {typeof (PMProject.contractCD)})]
  [ProjectBase(typeof (FSRouteContractSchedule.customerID), Enabled = false)]
  public override int? ProjectID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Default Project Task", Enabled = false, FieldClass = "PROJECT")]
  [PXDefault(typeof (Search2<FSServiceContract.dfltProjectTaskID, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<Current<FSRouteContractSchedule.srvOrdType>>>, InnerJoin<PMTask, On<PMTask.taskID, Equal<FSServiceContract.dfltProjectTaskID>, And<PMTask.projectID, Equal<Current<FSRouteContractSchedule.projectID>>>>>>, Where<FSServiceContract.serviceContractID, Equal<Current<FSRouteContractSchedule.entityID>>, And<Current<FSRouteContractSchedule.entityType>, Equal<ListField_Schedule_EntityType.Contract>, And2<Where<FSSrvOrdType.enableINPosting, Equal<False>, Or<PMTask.visibleInIN, Equal<True>>>, And<Where2<Where<FSSrvOrdType.postTo, Equal<FSPostTo.None>>, Or<Where2<Where<FSSrvOrdType.postTo, Equal<FSPostTo.Accounts_Receivable_Module>, And<Where<PMTask.visibleInAR, Equal<True>>>>, Or<Where2<Where<FSSrvOrdType.postTo, Equal<FSPostTo.Sales_Order_Module>, Or<FSSrvOrdType.postTo, Equal<FSPostTo.Sales_Order_Invoice>>>, And<Where<PMTask.visibleInSO, Equal<True>>>>>>>>>>>>>))]
  [FSSelectorActive_AR_SO_ProjectTask(typeof (Where<PMTask.projectID, Equal<Current<FSRouteContractSchedule.projectID>>>))]
  public override int? DfltProjectTaskID { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<FSRouteContractSchedule.customerID, PX.Objects.AR.Customer.acctName>))]
  public virtual string FormCaptionDescription { get; set; }

  public new class PK : 
    PrimaryKeyOf<FSRouteContractSchedule>.By<FSRouteContractSchedule.entityID, FSRouteContractSchedule.refNbr>
  {
    public static FSRouteContractSchedule Find(
      PXGraph graph,
      int? entityID,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (FSRouteContractSchedule) PrimaryKeyOf<FSRouteContractSchedule>.By<FSRouteContractSchedule.entityID, FSRouteContractSchedule.refNbr>.FindBy(graph, (object) entityID, (object) refNbr, options);
    }
  }

  public new static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FSRouteContractSchedule>.By<FSSchedule.branchID>
    {
    }

    public class BranchLocation : 
      PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationID>.ForeignKeyOf<FSRouteContractSchedule>.By<FSSchedule.branchLocationID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSRouteContractSchedule>.By<FSRouteContractSchedule.customerID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<FSRouteContractSchedule>.By<FSSchedule.vendorID>
    {
    }

    public class VehicleType : 
      PrimaryKeyOf<FSVehicleType>.By<FSVehicleType.vehicleTypeID>.ForeignKeyOf<FSRouteContractSchedule>.By<FSSchedule.vehicleTypeID>
    {
    }

    public class Employee : 
      PrimaryKeyOf<EPEmployee>.By<EPEmployee.bAccountID>.ForeignKeyOf<FSRouteContractSchedule>.By<FSSchedule.employeeID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<FSRouteContractSchedule>.By<FSRouteContractSchedule.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<FSRouteContractSchedule>.By<FSRouteContractSchedule.projectID, FSRouteContractSchedule.dfltProjectTaskID>
    {
    }
  }

  public new abstract class entityID : IBqlField, IBqlOperand
  {
  }

  public new abstract class refNbr : IBqlField, IBqlOperand
  {
  }

  public new abstract class customerID : IBqlField, IBqlOperand
  {
  }

  public new abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRouteContractSchedule.customerLocationID>
  {
  }

  public new abstract class srvOrdType : IBqlField, IBqlOperand
  {
  }

  public new abstract class scheduleGenType : ListField_ScheduleGenType_ContractSchedule
  {
  }

  public new abstract class entityType : ListField_Schedule_EntityType
  {
  }

  public new abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRouteContractSchedule.projectID>
  {
  }

  public new abstract class dfltProjectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRouteContractSchedule.dfltProjectTaskID>
  {
  }
}
