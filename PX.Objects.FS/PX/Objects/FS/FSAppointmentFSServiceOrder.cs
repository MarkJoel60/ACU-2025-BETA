// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentFSServiceOrder
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.FS;

/// <exclude />
[PXBreakInheritance]
[PXPrimaryGraph(typeof (AppointmentEntry))]
[PXProjection(typeof (Select2<FSAppointment, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, InnerJoin<FSAddress, On<FSAddress.addressID, Equal<FSServiceOrder.serviceOrderAddressID>>, InnerJoin<FSContact, On<FSContact.contactID, Equal<FSServiceOrder.serviceOrderContactID>>>>>>))]
[Serializable]
public class FSAppointmentFSServiceOrder : FSAppointment
{
  [PXDBInt(BqlField = typeof (FSAppointment.sOID))]
  public override int? SOID { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointment.appointmentID))]
  public override int? AppointmentID { get; set; }

  [PXDBString(4, IsKey = true, IsFixed = true, BqlField = typeof (FSAppointment.srvOrdType))]
  [PXUIField(DisplayName = "Service Order Type")]
  [FSSelectorSrvOrdTypeNOTQuote]
  public override 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "CCCCCCCCCCCCCCCCCCCC", BqlField = typeof (FSAppointment.refNbr))]
  [PXUIField]
  [PXSelector(typeof (Search3<FSAppointment.refNbr, LeftJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<FSServiceOrder.locationID>>>>>, OrderBy<Desc<FSAppointment.refNbr, Asc<FSAppointment.srvOrdType>>>>), new System.Type[] {typeof (FSAppointment.refNbr), typeof (FSAppointment.srvOrdType), typeof (PX.Objects.AR.Customer.acctCD), typeof (PX.Objects.AR.Customer.acctName), typeof (PX.Objects.CR.Location.locationCD), typeof (FSAppointment.docDesc), typeof (FSAppointment.status), typeof (FSAppointment.scheduledDateTimeBegin)})]
  public override string RefNbr { get; set; }

  [PXDefault]
  [PXDBString(15, IsUnicode = true, BqlField = typeof (FSAppointment.soRefNbr))]
  [PXUIField(DisplayName = "Service Order Nbr.")]
  [PXSelector(typeof (Search2<FSServiceOrder.refNbr, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>, Where<FSServiceOrder.srvOrdType, Equal<Current<FSAppointmentFSServiceOrder.srvOrdType>>, And<Where2<Where<Current<FSAppointmentFSServiceOrder.appointmentID>, Greater<Zero>, Or<FSServiceOrder.openDoc, Equal<True>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Where<PX.Objects.AR.Customer.bAccountID, IsNotNull, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>>>>, OrderBy<Desc<FSServiceOrder.refNbr>>>), new System.Type[] {typeof (FSServiceOrder.refNbr), typeof (FSServiceOrder.srvOrdType), typeof (FSServiceOrder.customerID), typeof (FSServiceOrder.status), typeof (FSServiceOrder.priority), typeof (FSServiceOrder.severity), typeof (FSServiceOrder.orderDate), typeof (FSServiceOrder.sLAETA), typeof (FSServiceOrder.assignedEmpID), typeof (FSServiceOrder.sourceType), typeof (FSServiceOrder.sourceRefNbr)})]
  public override string SORefNbr { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointment.customerID))]
  [PXDefault]
  [PXUIField]
  [FSSelectorCustomer]
  public override int? CustomerID { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointment.serviceContractID))]
  [PXSelector(typeof (Search<FSServiceContract.serviceContractID, Where<FSServiceContract.customerID, Equal<Current<FSAppointmentFSServiceOrder.customerID>>>>), SubstituteKey = typeof (FSServiceContract.refNbr))]
  [PXUIField(DisplayName = "Source Service Contract ID", Enabled = false, Visible = false, FieldClass = "FSCONTRACT")]
  public override int? ServiceContractID { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointment.scheduleID))]
  [PXSelector(typeof (Search<FSSchedule.scheduleID, Where<FSSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>, And<FSSchedule.entityID, Equal<Current<FSServiceOrder.serviceContractID>>>>>), SubstituteKey = typeof (FSSchedule.refNbr))]
  [PXUIField(DisplayName = "Schedule ID", Enabled = false)]
  public override int? ScheduleID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<FSServiceContract.serviceContractID, Where<FSServiceContract.customerID, Equal<Current<FSAppointmentFSServiceOrder.customerID>>>>), SubstituteKey = typeof (FSServiceContract.refNbr))]
  [PXUIField(DisplayName = "Service Contract ID", Enabled = false, Visible = false)]
  public override int? BillServiceContractID { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Scheduled Start Date", DisplayNameTime = "Scheduled Start Time", BqlField = typeof (FSAppointment.scheduledDateTimeBegin))]
  [PXUIField]
  public override DateTime? ScheduledDateTimeBegin { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Scheduled End Date", DisplayNameTime = "Scheduled End Time", BqlField = typeof (FSAppointment.scheduledDateTimeEnd))]
  [PXUIField]
  public override DateTime? ScheduledDateTimeEnd { get; set; }

  [PXDBTimeSpanLong]
  [PXUIField(DisplayName = "Estimated Duration Total", Enabled = false, Visible = false)]
  public override int? EstimatedDurationTotal { get; set; }

  [PXDBTimeSpanLong]
  [PXUIField(DisplayName = "Actual Duration Total", Enabled = false, Visible = false)]
  public override int? ActualDurationTotal { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Waiting for Purchased Items", Visible = false, Enabled = false)]
  public override bool? WaitingForParts { get; set; }

  [ProjectBase(BqlField = typeof (FSAppointment.projectID))]
  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.isCancelled, Equal<False>>), "The {0} project or contract is canceled.", new System.Type[] {typeof (PMProject.contractCD)})]
  public override int? ProjectID { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSAddress.addressLine1))]
  [PXUIField(DisplayName = "Address Line 1")]
  public virtual string AddressLine1 { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSAddress.addressLine2))]
  [PXUIField(DisplayName = "Address Line 2")]
  public virtual string AddressLine2 { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSAddress.addressLine3))]
  [PXUIField(DisplayName = "Address Line 3", Visible = false, Enabled = false)]
  public virtual string AddressLine3 { get; set; }

  [PXDBBool(BqlField = typeof (FSAddress.isValidated))]
  [PXUIField(DisplayName = "Address Validated", Enabled = true)]
  public virtual bool? AddressValidated { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.assignedEmpID))]
  [FSSelector_StaffMember_All(null)]
  [PXUIField]
  public virtual int? AssignedEmpID { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.billCustomerID))]
  [PXUIField(DisplayName = "Billing Customer", Visible = false)]
  [FSSelectorCustomer]
  public override int? BillCustomerID { get; set; }

  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSServiceOrder.billCustomerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), BqlField = typeof (FSServiceOrder.billLocationID), DisplayName = "Billing Location", Visible = false, DirtyRead = true)]
  public virtual int? BillLocationID { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSAddress.city))]
  [PXUIField(DisplayName = "City", Visible = false)]
  public virtual string City { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.contactID))]
  [PXDefault]
  [PXUIField(DisplayName = "Contact")]
  [FSSelectorContact(typeof (FSServiceOrder.customerID))]
  public virtual int? ContactID { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.contractID))]
  [PXUIField(DisplayName = "Contract", Enabled = false)]
  [FSSelectorContract]
  public virtual int? ContractID { get; set; }

  [PXDBString(2, IsUnicode = true, BqlField = typeof (FSAddress.countryID))]
  [PXUIField(DisplayName = "Country")]
  [Country]
  public virtual string CountryID { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.branchLocationID))]
  [PXUIField(DisplayName = "Branch Location")]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<FSServiceOrder.branchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  public virtual int? BranchLocationID { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (FSServiceOrder.roomID))]
  [PXUIField(DisplayName = "Room", Visible = false)]
  [PXSelector(typeof (Search<FSRoom.roomID, Where<FSRoom.branchLocationID, Equal<Current<FSServiceOrder.branchLocationID>>>>), SubstituteKey = typeof (FSRoom.roomID), DescriptionField = typeof (FSRoom.descr))]
  public virtual string RoomID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (FSServiceOrder.docDesc))]
  [PXUIField(DisplayName = "Description")]
  public virtual string SODocDesc { get; set; }

  [PXDBEmail(BqlField = typeof (FSContact.email))]
  [PXUIField(DisplayName = "Email")]
  public virtual string EMail { get; set; }

  [PXDBString(50, BqlField = typeof (FSContact.fax))]
  [PXUIField(DisplayName = "Fax")]
  public virtual string Fax { get; set; }

  [PXDBBool(BqlField = typeof (FSServiceOrder.hold))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Hold")]
  public override bool? Hold { get; set; }

  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSServiceOrder.customerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), BqlField = typeof (FSServiceOrder.locationID), DisplayName = "Location", DirtyRead = true)]
  public virtual int? LocationID { get; set; }

  [PXDBString(50, BqlField = typeof (FSContact.phone1))]
  [PXUIField(DisplayName = "Phone 1")]
  public virtual string Phone1 { get; set; }

  [PXDBString(50, BqlField = typeof (FSContact.phone2))]
  [PXUIField(DisplayName = "Phone 2")]
  public virtual string Phone2 { get; set; }

  [PXDBString(50, BqlField = typeof (FSContact.phone3))]
  [PXUIField(DisplayName = "Phone 3", Visible = false, Enabled = false)]
  public virtual string Phone3 { get; set; }

  [PXDBString(20, BqlField = typeof (FSAddress.postalCode))]
  [PXUIField(DisplayName = "Postal Code")]
  public virtual string PostalCode { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (FSServiceOrder.curyEstimatedOrderTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Estimated Order Total", Enabled = false)]
  public virtual Decimal? CuryEstimatedOrderTotal { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (FSServiceOrder.priority))]
  [PXUIField]
  [ListField_Priority_ServiceOrder.ListAtrribute]
  public virtual string Priority { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.problemID))]
  [PXUIField(DisplayName = "Problem ID")]
  [PXSelector(typeof (Search2<FSProblem.problemID, InnerJoin<FSSrvOrdTypeProblem, On<FSProblem.problemID, Equal<FSSrvOrdTypeProblem.problemID>>, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSSrvOrdTypeProblem.srvOrdType>>>>, Where<FSSrvOrdType.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>>>), SubstituteKey = typeof (FSProblem.problemCD), DescriptionField = typeof (FSProblem.descr))]
  public virtual int? ProblemID { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (FSServiceOrder.severity))]
  [PXDefault("M")]
  [PXUIField]
  [ListField_Severity_ServiceOrder.ListAtrribute]
  public virtual string Severity { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Deadline - SLA Date", DisplayNameTime = "Deadline - SLA Time", BqlField = typeof (FSServiceOrder.sLAETA))]
  [PXUIField]
  public virtual DateTime? SLAETA { get; set; }

  [PXDBString(4, IsFixed = true, BqlField = typeof (FSServiceOrder.sourceDocType))]
  [PXUIField(DisplayName = "Source document type", Enabled = false)]
  public virtual string SourceDocType { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.sourceID))]
  public virtual int? SourceID { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (FSServiceOrder.sourceRefNbr))]
  [PXUIField]
  public virtual string SourceRefNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (FSServiceOrder.sourceType))]
  [PXUIField]
  [ListField_SourceType_ServiceOrder.ListAtrribute]
  public virtual string SourceType { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSAddress.state))]
  [PXUIField(DisplayName = "State", Visible = false)]
  public virtual string State { get; set; }

  [PXDBBool(BqlField = typeof (FSServiceOrder.bAccountRequired))]
  [PXUIField(DisplayName = "Customer Required", Enabled = false)]
  public virtual bool? BAccountRequired { get; set; }

  [PXDBBool(BqlField = typeof (FSServiceOrder.quote))]
  [PXUIField(DisplayName = "Quote", Enabled = false)]
  public virtual bool? Quote { get; set; }

  [PXDBString(40, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC", BqlField = typeof (FSServiceOrder.custWorkOrderRefNbr))]
  [PXUIField(DisplayName = "Customer Work Order Ref. Nbr.")]
  public virtual string CustWorkOrderRefNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (FSServiceOrder.postedBy))]
  public virtual string PostedBy { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Deadline - SLA Date", DisplayNameTime = "Deadline - SLA Time", BqlField = typeof (FSServiceOrder.sLAETAUTC))]
  [PXUIField]
  public virtual DateTime? SLAETAUTC { get; set; }

  [PXTimeSpanLong]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Scheduled Duration", Enabled = false)]
  public override int? ScheduledDuration
  {
    [PXDependsOnFields(new System.Type[] {typeof (FSAppointmentFSServiceOrder.scheduledDateTimeBegin), typeof (FSAppointmentFSServiceOrder.scheduledDateTimeEnd)})] get
    {
      return this.ScheduledDateTimeBegin.HasValue && this.ScheduledDateTimeEnd.HasValue ? new int?(Convert.ToInt32(SharedFunctions.GetTimeSpanDiff(this.ScheduledDateTimeBegin.Value, this.ScheduledDateTimeEnd.Value).TotalMinutes)) : new int?(0);
    }
  }

  public new class PK : 
    PrimaryKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.refNbr, FSAppointmentFSServiceOrder.srvOrdType>
  {
    public static FSAppointmentFSServiceOrder Find(
      PXGraph graph,
      string refNbr,
      string srvOrdType,
      PKFindOptions options = 0)
    {
      return (FSAppointmentFSServiceOrder) PrimaryKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.refNbr, FSAppointmentFSServiceOrder.srvOrdType>.FindBy(graph, (object) refNbr, (object) srvOrdType, options);
    }
  }

  public new class UK : 
    PrimaryKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.appointmentID>
  {
    public static FSAppointmentFSServiceOrder Find(
      PXGraph graph,
      int? appointmentID,
      PKFindOptions options = 0)
    {
      return (FSAppointmentFSServiceOrder) PrimaryKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.appointmentID>.FindBy(graph, (object) appointmentID, options);
    }
  }

  public new static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.customerID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.branchID>
    {
    }

    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.srvOrdType>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.projectID, FSAppointmentFSServiceOrder.dfltProjectTaskID>
    {
    }

    public class WorkFlowStage : 
      PrimaryKeyOf<FSWFStage>.By<FSWFStage.wFStageID>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.wFStageID>
    {
    }

    public class ServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.serviceContractID>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<FSSchedule>.By<FSSchedule.scheduleID>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.scheduleID>
    {
    }

    public class BillServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.billServiceContractID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.taxZoneID>
    {
    }

    public class Route : 
      PrimaryKeyOf<FSRoute>.By<FSRoute.routeID>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.routeID>
    {
    }

    public class RouteDocument : 
      PrimaryKeyOf<FSRouteDocument>.By<FSRouteDocument.routeDocumentID>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.routeDocumentID>
    {
    }

    public class Vehicle : 
      PrimaryKeyOf<FSVehicle>.By<FSVehicle.SMequipmentID>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.vehicleID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.curyID>
    {
    }

    public class SalesPerson : 
      PrimaryKeyOf<PX.Objects.AR.SalesPerson>.By<PX.Objects.AR.SalesPerson.salesPersonID>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.salesPersonID>
    {
    }

    public class BillCustomer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.billCustomerID>
    {
    }

    public class BranchLocation : 
      PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationID>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.branchLocationID>
    {
    }

    public class Contract : 
      PrimaryKeyOf<PX.Objects.CT.Contract>.By<PX.Objects.CT.Contract.contractID>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.contractID>
    {
    }

    public class Room : 
      PrimaryKeyOf<FSRoom>.By<FSRoom.branchLocationID, FSRoom.roomID>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.branchLocationID, FSAppointmentFSServiceOrder.roomID>
    {
    }

    public class ServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>.ForeignKeyOf<FSAppointmentFSServiceOrder>.By<FSAppointmentFSServiceOrder.srvOrdType, FSAppointmentFSServiceOrder.soRefNbr>
    {
    }
  }

  public new abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentFSServiceOrder.sOID>
  {
  }

  public new abstract class appointmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.appointmentID>
  {
  }

  public new abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.srvOrdType>
  {
  }

  public new abstract class refNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.refNbr>
  {
  }

  public new abstract class soRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.soRefNbr>
  {
  }

  public new abstract class customerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.customerID>
  {
  }

  public new abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.serviceContractID>
  {
  }

  public new abstract class scheduleID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.scheduleID>
  {
  }

  public new abstract class billServiceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.billServiceContractID>
  {
  }

  public new abstract class scheduledDateTimeBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.scheduledDateTimeBegin>
  {
  }

  public new abstract class scheduledDateTimeEnd : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.scheduledDateTimeEnd>
  {
  }

  public new abstract class estimatedDurationTotal : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.estimatedDurationTotal>
  {
  }

  public new abstract class actualDurationTotal : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.actualDurationTotal>
  {
  }

  public new abstract class waitingForParts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.waitingForParts>
  {
  }

  public new abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.projectID>
  {
  }

  public abstract class addressLine1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.addressLine1>
  {
  }

  public abstract class addressLine2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.addressLine2>
  {
  }

  public abstract class addressLine3 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.addressLine3>
  {
  }

  public abstract class addressValidated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.addressValidated>
  {
  }

  public abstract class assignedEmpID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.assignedEmpID>
  {
  }

  public new abstract class billCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.billCustomerID>
  {
  }

  public abstract class billLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.billLocationID>
  {
  }

  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentFSServiceOrder.city>
  {
  }

  public abstract class contactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.contactID>
  {
  }

  public abstract class contractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.contractID>
  {
  }

  public abstract class countryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.countryID>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.branchLocationID>
  {
  }

  public abstract class roomID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.roomID>
  {
  }

  public abstract class soDocDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.soDocDesc>
  {
  }

  public abstract class eMail : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentFSServiceOrder.eMail>
  {
  }

  public abstract class fax : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentFSServiceOrder.fax>
  {
  }

  public new abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentFSServiceOrder.hold>
  {
  }

  public abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.locationID>
  {
  }

  public abstract class phone1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.phone1>
  {
  }

  public abstract class phone2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.phone2>
  {
  }

  public abstract class phone3 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.phone3>
  {
  }

  public abstract class postalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.postalCode>
  {
  }

  public abstract class curyEstimatedOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.curyEstimatedOrderTotal>
  {
  }

  public abstract class priority : ListField_Priority_ServiceOrder
  {
  }

  public abstract class problemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.problemID>
  {
  }

  public abstract class severity : ListField_Severity_ServiceOrder
  {
  }

  public abstract class sLAETA : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.sLAETA>
  {
  }

  public abstract class sourceDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.sourceDocType>
  {
  }

  public abstract class sourceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentFSServiceOrder.sourceID>
  {
  }

  public abstract class sourceRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.sourceRefNbr>
  {
  }

  public abstract class sourceType : ListField_SourceType_ServiceOrder
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentFSServiceOrder.state>
  {
  }

  public abstract class bAccountRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.bAccountRequired>
  {
  }

  public abstract class quote : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentFSServiceOrder.quote>
  {
  }

  public abstract class custWorkOrderRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.custWorkOrderRefNbr>
  {
  }

  public abstract class postedBy : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.postedBy>
  {
  }

  public abstract class sLAETAUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.sLAETAUTC>
  {
  }

  public new abstract class branchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.branchID>
  {
  }

  public new abstract class taxZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.taxZoneID>
  {
  }

  public new abstract class dfltProjectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.dfltProjectTaskID>
  {
  }

  public new abstract class wFStageID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.wFStageID>
  {
  }

  public new abstract class routeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.routeID>
  {
  }

  public new abstract class routeDocumentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.routeDocumentID>
  {
  }

  public new abstract class vehicleID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.vehicleID>
  {
  }

  public new abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.curyInfoID>
  {
  }

  public new abstract class curyID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.curyID>
  {
  }

  public new abstract class salesPersonID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.salesPersonID>
  {
  }

  public new abstract class scheduledDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFSServiceOrder.scheduledDuration>
  {
  }
}
