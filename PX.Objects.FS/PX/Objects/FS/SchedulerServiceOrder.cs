// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SchedulerServiceOrder
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.FS;

/// <exclude />
[PXProjection(typeof (Select2<FSServiceOrder, InnerJoin<FSContact, On<BqlOperand<FSContact.contactID, IBqlInt>.IsEqual<FSServiceOrder.serviceOrderContactID>>, InnerJoin<FSAddress, On<BqlOperand<FSAddress.addressID, IBqlInt>.IsEqual<FSServiceOrder.serviceOrderAddressID>>, LeftJoin<PX.Objects.AR.Customer, On<BqlOperand<PX.Objects.AR.Customer.bAccountID, IBqlInt>.IsEqual<FSServiceOrder.customerID>>, LeftJoin<PX.Objects.CR.Location, On<BqlOperand<PX.Objects.CR.Location.locationID, IBqlInt>.IsEqual<FSServiceOrder.locationID>>, LeftJoin<PX.Objects.GL.Branch, On<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<FSServiceOrder.branchID>>, LeftJoin<PX.Objects.CR.BAccount, On<BqlOperand<PX.Objects.CR.BAccount.bAccountID, IBqlInt>.IsEqual<PX.Objects.GL.Branch.bAccountID>>, LeftJoin<FSBranchLocation, On<BqlOperand<FSBranchLocation.branchLocationID, IBqlInt>.IsEqual<FSServiceOrder.branchLocationID>>, LeftJoin<FSProblem, On<BqlOperand<FSProblem.problemID, IBqlInt>.IsEqual<FSServiceOrder.problemID>>, LeftJoin<FSServiceContract, On<BqlOperand<FSServiceContract.serviceContractID, IBqlInt>.IsEqual<FSServiceOrder.billServiceContractID>>>>>>>>>>>>))]
[PXCacheName("Service Order")]
[Serializable]
public class SchedulerServiceOrder : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(4, IsFixed = true, IsKey = true, IsUnicode = true, BqlField = typeof (FSServiceOrder.srvOrdType))]
  [PXUIField(DisplayName = "Service Order Type")]
  [FSSelectorSrvOrdTypeNOTQuote]
  [PXRestrictor(typeof (Where<FSSrvOrdType.active, Equal<True>>), null, new System.Type[] {})]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.sOID))]
  public virtual int? SOID { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (FSServiceOrder.refNbr))]
  [PXUIField(DisplayName = "Service Order Nbr.")]
  [FSSelectorSORefNbr]
  public virtual string RefNbr { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (FSServiceOrder.status))]
  [PXUIField(DisplayName = "Service Order Status", Enabled = false)]
  [ListField.ServiceOrderStatus.List]
  public virtual string Status { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (FSServiceOrder.roomID))]
  [PXSelector(typeof (FSRoom.roomID), CacheGlobal = true, DescriptionField = typeof (FSRoom.descr))]
  [PXUIField(DisplayName = "Room ID")]
  public virtual string RoomID { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.customerID))]
  [PXUIField(DisplayName = "Customer", Required = false)]
  [FSSelectorBusinessAccount_CU_PR_VC]
  public virtual int? CustomerID { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.contactID))]
  [FSSelectorContact(typeof (FSServiceOrder.customerID))]
  [PXUIField(DisplayName = "Contact")]
  public virtual int? ContactID { get; set; }

  [PXDBTimeSpanLong]
  [PXUIField(DisplayName = "Estimated Duration", Enabled = false)]
  public virtual int? EstimatedDurationTotal { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (FSServiceOrder.priority))]
  [PXUIField(DisplayName = "Priority")]
  [ListField_Priority_ServiceOrder.ListAtrribute]
  public virtual string Priority { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (FSServiceOrder.severity))]
  [PXUIField(DisplayName = "Severity")]
  [ListField_Severity_ServiceOrder.ListAtrribute]
  public virtual string Severity { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (FSServiceOrder.docDesc))]
  [PXUIField(DisplayName = "Description")]
  public virtual string DocDesc { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "SLA", BqlField = typeof (FSServiceOrder.sLAETA))]
  [PXUIField(DisplayName = "SLA")]
  public virtual DateTime? SLAETA { get; set; }

  [ProjectBase(typeof (FSServiceOrder.billCustomerID), BqlField = typeof (FSServiceOrder.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBString(40, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC", BqlField = typeof (FSServiceOrder.custPORefNbr))]
  [NormalizeWhiteSpace]
  [PXUIField(DisplayName = "Customer Order")]
  public virtual string CustPORefNbr { get; set; }

  [PXDBBool(BqlField = typeof (FSServiceOrder.waitingForParts))]
  [PXFormula(typeof (IIf<Where<SchedulerServiceOrder.pendingPOLineCntr, Greater<int0>>, True, False>))]
  [PXUIVisible(typeof (Where<Current<FSSrvOrdType.postTo>, Equal<FSPostTo.Sales_Order_Invoice>, Or<Current<FSSrvOrdType.postTo>, Equal<FSPostTo.Sales_Order_Module>, Or<Current<FSSrvOrdType.postTo>, Equal<FSPostTo.Projects>>>>))]
  [PXUIField(DisplayName = "Waiting for Purchased Items", Enabled = false, FieldClass = "DISTINV")]
  public virtual bool? WaitingForParts { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.pendingPOLineCntr))]
  public virtual int? PendingPOLineCntr { get; set; }

  [FSLocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSServiceOrder.customerID>>, And<MatchWithBranch<PX.Objects.CR.Location.cBranchID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Location", DirtyRead = true, BqlField = typeof (FSServiceOrder.locationID))]
  public virtual int? LocationID { get; set; }

  [PXDBDate(DisplayMask = "d", BqlField = typeof (FSServiceOrder.orderDate))]
  [PXUIField(DisplayName = "Date")]
  public virtual DateTime? OrderDate { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (FSServiceOrder.sourceType))]
  [PXUIField(DisplayName = "Document Type", Enabled = false)]
  [ListField_SourceType_ServiceOrder.ListAtrribute]
  public virtual string SourceType { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.assignedEmpID))]
  [PXUIField(DisplayName = "Supervisor")]
  public virtual int? AssignedEmpID { get; set; }

  [PXDBCreatedDateTime(BqlField = typeof (FSServiceOrder.createdDateTime))]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.createdDateTime))]
  public virtual int? ServiceOrderContactID { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.serviceOrderAddressID))]
  public virtual int? ServiceOrderAddressID { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.problemID))]
  public virtual int? ProblemID { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.serviceContractID))]
  public virtual int? ServiceContractID { get; set; }

  [PXDBBool(BqlField = typeof (FSServiceOrder.quote))]
  public virtual bool? Quote { get; set; }

  [PXDBBool(BqlField = typeof (FSServiceOrder.hold))]
  public virtual bool? Hold { get; set; }

  [PXDBBool(BqlField = typeof (FSServiceOrder.closed))]
  public virtual bool? Closed { get; set; }

  [PXDBBool(BqlField = typeof (FSServiceOrder.canceled))]
  public virtual bool? Canceled { get; set; }

  [PXDBBool(BqlField = typeof (FSServiceOrder.completed))]
  public virtual bool? Completed { get; set; }

  [PXDBBool(BqlField = typeof (FSServiceOrder.appointmentsNeeded))]
  public virtual bool? AppointmentsNeeded { get; set; }

  [CustomerRaw(IsKey = true, BqlField = typeof (PX.Objects.AR.Customer.acctCD))]
  [PXUIField]
  [PXFieldDescription]
  [PXPersonalDataWarning]
  public virtual string CustomerAcctCD { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (PX.Objects.AR.Customer.acctName))]
  [PXUIField]
  [PXFieldDescription]
  [PXPersonalDataField]
  public virtual string CustomerAcctName { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.AR.Customer.customerClassID))]
  [PXSelector(typeof (PX.Objects.AR.CustomerClass.customerClassID), CacheGlobal = true, DescriptionField = typeof (PX.Objects.AR.CustomerClass.descr))]
  [PXUIField(DisplayName = "Customer Class")]
  public virtual string CustomerClassID { get; set; }

  [PXDBString(50, BqlField = typeof (FSContact.phone1))]
  [PXUIField]
  [PXPersonalDataField]
  public virtual string Phone1 { get; set; }

  [PXUIField]
  [PXDependsOnFields(new System.Type[] {typeof (SchedulerServiceOrder.lastName), typeof (SchedulerServiceOrder.firstName), typeof (SchedulerServiceOrder.title)})]
  [PX.Objects.CR.ContactDisplayName(typeof (SchedulerServiceOrder.lastName), typeof (SchedulerServiceOrder.firstName), typeof (SchedulerServiceOrder.midName), typeof (SchedulerServiceOrder.title), true)]
  [PXFieldDescription]
  [PXPersonalDataField]
  public virtual string ContactDisplayName { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSContact.title))]
  public virtual string Title { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSContact.firstName))]
  [PXPersonalDataField]
  public virtual string FirstName { get; set; }

  [PXDBString(100, IsUnicode = true, BqlField = typeof (FSContact.lastName))]
  [PXPersonalDataField]
  public virtual string LastName { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSContact.midName))]
  [PXPersonalDataField]
  public virtual string MidName { get; set; }

  [PXDBEmail(BqlField = typeof (FSContact.email))]
  [PXUIField]
  [PXPersonalDataField]
  public virtual string Email { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSAddress.addressLine1))]
  [PXUIField]
  [PXPersonalDataField]
  public virtual string AddressLine1 { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSAddress.addressLine2))]
  [PXUIField(DisplayName = "Address Line 2")]
  [PXPersonalDataField]
  public virtual string AddressLine2 { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSAddress.city))]
  [PXUIField]
  [PXPersonalDataField]
  public virtual string City { get; set; }

  [PXDBString(100, BqlField = typeof (FSAddress.countryID))]
  [PXUIField(DisplayName = "Country")]
  public virtual string CountryID { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSAddress.state))]
  [PXUIField(DisplayName = "State")]
  public virtual string State { get; set; }

  [PXDBString(20, BqlField = typeof (FSAddress.postalCode))]
  [PXUIField(DisplayName = "Postal Code")]
  [PXPersonalDataField]
  public virtual string PostalCode { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Address", Enabled = false, Visible = true)]
  [PXFormula(typeof (SmartJoin<CommaSpace, SmartJoin<Space, SchedulerServiceOrder.addressLine1, SchedulerServiceOrder.addressLine2>, SmartJoin<Space, SchedulerServiceOrder.city, SchedulerServiceOrder.state, SchedulerServiceOrder.postalCode>>))]
  public virtual string FullAddress { get; set; }

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (PX.Objects.GL.Branch.branchCD))]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public virtual string BranchCD { get; set; }

  [PXDBString(30, IsUnicode = true, BqlField = typeof (PX.Objects.CR.BAccount.acctName))]
  [PXUIField]
  public virtual string BranchName { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true, BqlField = typeof (FSBranchLocation.branchLocationCD))]
  [PXSelector(typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  [PXUIField]
  [NormalizeWhiteSpace]
  public virtual string BranchLocationCD { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true, BqlField = typeof (FSBranchLocation.descr))]
  [PXUIField]
  public virtual string BranchLocationDescr { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true, BqlField = typeof (FSProblem.problemCD))]
  [NormalizeWhiteSpace]
  [PXSelector(typeof (FSProblem.problemCD), CacheGlobal = true, DescriptionField = typeof (FSProblem.descr))]
  [PXUIField]
  public virtual string ProblemCD { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true, BqlField = typeof (FSProblem.descr))]
  [PXUIField]
  public virtual string ProblemDescr { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (FSServiceContract.refNbr))]
  [PXUIField]
  [FSSelectorContractRefNbrAttribute(typeof (ListField_RecordType_ContractSchedule.ServiceContract))]
  public virtual string ServiceContractRefNbr { get; set; }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerServiceOrder.srvOrdType>
  {
  }

  public abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SchedulerServiceOrder.sOID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerServiceOrder.refNbr>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerServiceOrder.status>
  {
    public abstract class Values : ListField.ServiceOrderStatus
    {
    }
  }

  public abstract class roomID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerServiceOrder.roomID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SchedulerServiceOrder.customerID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SchedulerServiceOrder.contactID>
  {
  }

  public abstract class estimatedDurationTotal : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerServiceOrder.estimatedDurationTotal>
  {
  }

  public abstract class priority : ListField_Priority_ServiceOrder
  {
  }

  public abstract class severity : ListField_Severity_ServiceOrder
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerServiceOrder.docDesc>
  {
  }

  public abstract class sLAETA : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SchedulerServiceOrder.sLAETA>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SchedulerServiceOrder.projectID>
  {
  }

  public abstract class custPORefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerServiceOrder.custPORefNbr>
  {
  }

  public abstract class waitingForParts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SchedulerServiceOrder.waitingForParts>
  {
  }

  public abstract class pendingPOLineCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerServiceOrder.pendingPOLineCntr>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SchedulerServiceOrder.locationID>
  {
  }

  public abstract class orderDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SchedulerServiceOrder.orderDate>
  {
  }

  public abstract class sourceType : ListField_SourceType_ServiceOrder
  {
  }

  public abstract class assignedEmpID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerServiceOrder.assignedEmpID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SchedulerServiceOrder.createdDateTime>
  {
  }

  public abstract class serviceOrderContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerServiceOrder.serviceOrderContactID>
  {
  }

  public abstract class serviceOrderAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerServiceOrder.serviceOrderAddressID>
  {
  }

  public abstract class problemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SchedulerServiceOrder.problemID>
  {
  }

  public abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerServiceOrder.serviceContractID>
  {
  }

  public abstract class quote : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SchedulerServiceOrder.quote>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SchedulerServiceOrder.hold>
  {
  }

  public abstract class closed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SchedulerServiceOrder.closed>
  {
  }

  public abstract class canceled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SchedulerServiceOrder.canceled>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SchedulerServiceOrder.completed>
  {
  }

  public abstract class appointmentsNeeded : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SchedulerServiceOrder.appointmentsNeeded>
  {
  }

  public abstract class customerAcctCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerServiceOrder.customerAcctCD>
  {
  }

  public abstract class customerAcctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerServiceOrder.customerAcctName>
  {
  }

  public abstract class customerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerServiceOrder.customerClassID>
  {
  }

  public abstract class phone1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerServiceOrder.phone1>
  {
  }

  public abstract class contactDisplayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerServiceOrder.contactDisplayName>
  {
  }

  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerServiceOrder.title>
  {
  }

  public abstract class firstName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerServiceOrder.firstName>
  {
  }

  public abstract class lastName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerServiceOrder.lastName>
  {
  }

  public abstract class midName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerServiceOrder.midName>
  {
  }

  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerServiceOrder.email>
  {
  }

  public abstract class addressLine1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerServiceOrder.addressLine1>
  {
  }

  public abstract class addressLine2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerServiceOrder.addressLine2>
  {
  }

  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerServiceOrder.city>
  {
  }

  public abstract class countryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerServiceOrder.countryID>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerServiceOrder.state>
  {
  }

  public abstract class postalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerServiceOrder.postalCode>
  {
  }

  public abstract class fullAddress : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerServiceOrder.fullAddress>
  {
  }

  public abstract class branchCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerServiceOrder.branchCD>
  {
  }

  public abstract class branchName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerServiceOrder.branchName>
  {
  }

  public abstract class branchLocationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerServiceOrder.branchLocationCD>
  {
  }

  public abstract class branchLocationDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerServiceOrder.branchLocationDescr>
  {
  }

  public abstract class problemCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerServiceOrder.problemCD>
  {
  }

  public abstract class problemDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerServiceOrder.problemDescr>
  {
  }

  public abstract class serviceContractRefNbr : IBqlField, IBqlOperand
  {
  }
}
