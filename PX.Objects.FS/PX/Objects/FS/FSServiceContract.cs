// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSServiceContract
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.PM;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Service Contract")]
[PXPrimaryGraph(new System.Type[] {typeof (ServiceContractEntry), typeof (RouteServiceContractEntry)}, new System.Type[] {typeof (Where<FSServiceContract.recordType, Equal<ListField_RecordType_ContractSchedule.ServiceContract>>), typeof (Where<FSServiceContract.recordType, Equal<ListField_RecordType_ContractSchedule.RouteServiceContract>>)})]
[PXGroupMask(typeof (InnerJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceContract.customerID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>))]
[Serializable]
public class FSServiceContract : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, INotable
{
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [AutoNumber(typeof (Search<FSSetup.serviceContractNumberingID>), typeof (AccessInfo.businessDate))]
  [FSSelectorContractRefNbrAttribute(typeof (ListField_RecordType_ContractSchedule.ServiceContract))]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [FSSelectorCustomerContractNbrAttribute(typeof (ListField_RecordType_ContractSchedule.ServiceContract), typeof (FSServiceContract.customerID))]
  [ServiceContractAutoNumber]
  public virtual string CustomerContractNbr { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField]
  [FSSelectorBAccountTypeCustomerOrCombined]
  [PXRestrictor(typeof (Where<PX.Objects.AR.Customer.status, IsNull, Or<PX.Objects.AR.Customer.status, Equal<CustomerStatus.active>, Or<PX.Objects.AR.Customer.status, Equal<CustomerStatus.oneTime>>>>), "The customer status is '{0}'.", new System.Type[] {typeof (PX.Objects.AR.Customer.status)})]
  [PXFieldDescription]
  public virtual int? CustomerID { get; set; }

  [PXDBIdentity]
  public virtual int? ServiceContractID { get; set; }

  /// <summary>
  /// A service field, which is necessary for the <see cref="T:PX.Objects.CS.CSAnswers">dynamically
  /// added attributes</see> defined at the <see cref="T:PX.Objects.FS.FSServiceContract">customer
  /// class</see> level to function correctly.
  /// </summary>
  [CRAttributesField(typeof (FSServiceContract.classID))]
  public virtual string[] Attributes { get; set; }

  [PXString(20)]
  public string ClassID => "Service Contract";

  [PXDBString(1, IsUnicode = false)]
  [ListField_Contract_BillingPeriod.ListAtrribute]
  [PXDefault("M")]
  [PXUIField(DisplayName = "Period")]
  public virtual string BillingPeriod { get; set; }

  [PXDBString(4, IsUnicode = false)]
  [PXDefault("APFB")]
  [ListField.ServiceContractBillingType.List]
  [PXUIField(DisplayName = "Billing Type")]
  public virtual string BillingType { get; set; }

  [PXDBString(1, IsUnicode = false)]
  [PXDefault("C")]
  [ListField_Contract_BillTo.ListAtrribute]
  [PXUIField(DisplayName = "Bill To")]
  public virtual string BillTo { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Billing Customer", Enabled = false)]
  [FSSelectorBAccountTypeCustomerOrCombined]
  [PXRestrictor(typeof (Where<PX.Objects.AR.Customer.status, IsNull, Or<PX.Objects.AR.Customer.status, Equal<CustomerStatus.active>, Or<PX.Objects.AR.Customer.status, Equal<CustomerStatus.oneTime>>>>), "The customer status is '{0}'.", new System.Type[] {typeof (PX.Objects.AR.Customer.status)})]
  public virtual int? BillCustomerID { get; set; }

  [PXDefault]
  [FSLocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSServiceContract.billCustomerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Billing Location", DirtyRead = true, Enabled = false)]
  [PXForeignReference(typeof (FSServiceContract.FK.BillCustomerLocation))]
  public virtual int? BillLocationID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (AccessInfo.branchID))]
  [PXUIField(DisplayName = "Branch", TabOrder = 0)]
  [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public virtual int? BranchID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Search<FSxUserPreferences.dfltBranchLocationID, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>, And<UserPreferences.defBranchID, Equal<Current<FSServiceContract.branchID>>>>>))]
  [PXUIField(DisplayName = "Branch Location")]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<FSServiceContract.branchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  public virtual int? BranchLocationID { get; set; }

  [PXDefault]
  [FSLocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSServiceContract.customerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Location", DirtyRead = true)]
  [PXForeignReference(typeof (FSServiceContract.FK.CustomerLocation))]
  public virtual int? CustomerLocationID { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string DocDesc { get; set; }

  [PXDBString(1, IsUnicode = false)]
  [PXDefault("U")]
  [ListField_Contract_ExpirationType.ListAtrribute]
  [PXUIField(DisplayName = "Expiration Type")]
  public virtual string ExpirationType { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Start Date")]
  public virtual DateTime? StartDate { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Duration Type")]
  [PXDefault("Y")]
  [SC_Duration_Type.ListAtrribute]
  public virtual string DurationType { get; set; }

  [PXUIField(DisplayName = "Renewal Date", Enabled = false)]
  [PXDBDate]
  [PXDefault]
  public virtual DateTime? RenewalDate { get; set; }

  [PXUIField(DisplayName = "Expiration Date", Enabled = false)]
  [PXDBDate]
  [PXDefault]
  [PXFormula(typeof (Default<FSServiceContract.durationType, FSServiceContract.expirationType, FSServiceContract.startDate>))]
  public virtual DateTime? EndDate { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Duration")]
  [PXDefault(1)]
  [PXFormula(typeof (Default<FSServiceContract.durationType, FSServiceContract.expirationType, FSServiceContract.startDate>))]
  public virtual int? Duration { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Last Billing Date", Enabled = false)]
  public virtual DateTime? LastBillingInvoiceDate { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Master Contract")]
  [PXSelector(typeof (Search<FSMasterContract.masterContractID, Where<FSMasterContract.customerID, Equal<Current<FSServiceContract.customerID>>>>), SubstituteKey = typeof (FSMasterContract.masterContractCD), DescriptionField = typeof (FSMasterContract.descr))]
  public virtual int? MasterContractID { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Next Billing Date", Enabled = false)]
  public virtual DateTime? NextBillingInvoiceDate { get; set; }

  [PXDBString(4, IsUnicode = false)]
  [PXDefault("NRSC")]
  [ListField_RecordType_ContractSchedule.ListAtrribute]
  public virtual string RecordType { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("D")]
  [ListField_Status_ServiceContract.ListAtrribute]
  [PXUIField]
  public virtual string Status { get; set; }

  [PXUIField(DisplayName = "Effective From Date", Enabled = false)]
  [PXDBDate]
  public virtual DateTime? StatusEffectiveFromDate { get; set; }

  [PXUIField(DisplayName = "Effective Until Date", Enabled = false)]
  [PXDBDate]
  public virtual DateTime? StatusEffectiveUntilDate { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_Status_ServiceContract.ListAtrribute]
  [PXUIField(DisplayName = "Upcoming Status", Enabled = false)]
  public virtual string UpcomingStatus { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Contact")]
  [FSSelectorContact(typeof (FSServiceContract.customerID))]
  [PXFormula(typeof (Default<FSServiceContract.customerID>))]
  public virtual int? CustomerContactID { get; set; }

  [PXDBInt]
  [PXUIField]
  [FSSelectorVendor]
  public virtual int? VendorID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("P")]
  [ListField_Contract_SourcePrice.ListAtrribute]
  [PXUIField]
  public virtual string SourcePrice { get; set; }

  [SalesPerson(DisplayName = "Salesperson ID")]
  [PXDefault(typeof (Search<CustDefSalesPeople.salesPersonID, Where<CustDefSalesPeople.bAccountID, Equal<Current<FSServiceContract.customerID>>, And<CustDefSalesPeople.locationID, Equal<Current<FSServiceContract.customerLocationID>>, And<CustDefSalesPeople.isDefault, Equal<True>>>>>))]
  [PXFormula(typeof (Default<FSServiceContract.customerID>))]
  [PXFormula(typeof (Default<FSServiceContract.customerLocationID>))]
  public virtual int? SalesPersonID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Commissionable")]
  public virtual bool? Commissionable { get; set; }

  [PXDBString(2, IsUnicode = false)]
  [ListField_ScheduleGenType_ContractSchedule.ListAtrribute]
  [PXUIField(DisplayName = "Schedule Generation Type")]
  [PXFormula(typeof (Default<FSServiceContract.billingType>))]
  public virtual string ScheduleGenType { get; set; }

  [ProjectDefault]
  [PXRestrictor(typeof (Where<PMProject.status, NotEqual<ProjectStatus.closed>>), "The {0} project is closed.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.isCancelled, Equal<False>>), "The {0} project or contract is canceled.", new System.Type[] {typeof (PMProject.contractCD)})]
  [ProjectBase(typeof (FSServiceContract.customerID), Filterable = true)]
  [PXForeignReference(typeof (FSServiceContract.FK.Project))]
  public virtual int? ProjectID { get; set; }

  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<FSServiceContract.projectID>>, And<PMTask.isDefault, Equal<True>, And<Where<PMTask.status, Equal<ProjectTaskStatus.active>, Or<PMTask.status, Equal<ProjectTaskStatus.planned>>>>>>>))]
  [ActiveOrInPlanningProjectTask(typeof (FSServiceContract.projectID), DisplayName = "Default Project Task", DescriptionField = typeof (PMTask.description), Enabled = false)]
  [PXForeignReference(typeof (FSServiceContract.FK.DefaultTask))]
  public virtual int? DfltProjectTaskID { get; set; }

  [PXDefault]
  [CostCode(DisplayName = "Default Cost Code", Filterable = false, SkipVerification = true)]
  [PXForeignReference(typeof (Field<FSServiceContract.dfltCostCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  public virtual int? DfltCostCodeID { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote(ShowInReferenceSelector = true)]
  [PXSearchable(8192 /*0x2000*/, "SM: {0} - {2}", new System.Type[] {typeof (FSServiceContract.refNbr), typeof (FSServiceContract.customerID), typeof (PX.Objects.AR.Customer.acctName)}, new System.Type[] {typeof (PX.Objects.AR.Customer.acctCD), typeof (FSServiceContract.docDesc)}, NumberFields = new System.Type[] {typeof (FSServiceContract.refNbr)}, Line1Format = "{0:d}{1}{2}", Line1Fields = new System.Type[] {typeof (FSServiceContract.startDate), typeof (FSServiceContract.status), typeof (FSServiceContract.branchID)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (FSServiceContract.docDesc)}, MatchWithJoin = typeof (InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceContract.customerID>>>), SelectForFastIndexing = typeof (Select2<FSServiceContract, InnerJoin<PX.Objects.AR.Customer, On<FSServiceContract.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>>))]
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
  [PXUIField(DisplayName = "Last Modified By")]
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

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXBool]
  [PXDependsOnFields(new System.Type[] {typeof (FSServiceContract.billingType)})]
  [PXFormula(typeof (IIf<Where<FSServiceContract.billingType, Equal<ListField.ServiceContractBillingType.fixedRateBillings>, Or<FSServiceContract.billingType, Equal<ListField.ServiceContractBillingType.fixedRateAsPerformedBillings>>>, True, False>))]
  [PXDBCalced(typeof (IIf<Where<FSServiceContract.billingType, Equal<ListField.ServiceContractBillingType.fixedRateBillings>, Or<FSServiceContract.billingType, Equal<ListField.ServiceContractBillingType.fixedRateAsPerformedBillings>>>, True, False>), typeof (bool))]
  public virtual bool? IsFixedRateContract { get; set; }

  [PXString]
  [PXSelector(typeof (Search<FSServiceContract.refNbr, Where<FSServiceContract.recordType, Equal<ListField_RecordType_ContractSchedule.ServiceContract>, And<FSServiceContract.customerID, Equal<Optional<FSServiceContract.customerID>>>>>), new System.Type[] {typeof (FSServiceContract.refNbr), typeof (FSServiceContract.customerContractNbr), typeof (FSServiceContract.customerID), typeof (FSServiceContract.status), typeof (FSServiceContract.customerLocationID)})]
  public virtual string ReportServiceContractID { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? HasSchedule { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? HasProcessedSchedule { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? HasForecast { get; set; }

  [PXBool]
  public virtual bool? Mem_ShowPriceTab => new bool?(this.BillingType == "APFB");

  [PXBool]
  public virtual bool? Mem_ShowScheduleTab => new bool?(this.ScheduleGenType == "NA");

  [PXBool]
  [PXDefault(false)]
  [PXUIField(Visible = false)]
  public virtual bool? ShowInvoicesTab { get; set; }

  [PXInt]
  [PXSelector(typeof (FSBillingCycle.billingCycleID), SubstituteKey = typeof (FSBillingCycle.billingCycleCD), DescriptionField = typeof (FSBillingCycle.descr))]
  [PXUIField(DisplayName = "Usage Billing Cycle", Enabled = false)]
  public virtual int? UsageBillingCycleID { get; set; }

  [PXInt]
  [PXDBScalar(typeof (Search<FSContractPeriod.contractPeriodID, Where<FSContractPeriod.serviceContractID, Equal<FSServiceContract.serviceContractID>, And<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Active>>>>))]
  public virtual int? ActivePeriodID { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<FSServiceContract.customerID, PX.Objects.AR.Customer.acctName>))]
  public string FormCaptionDescription { get; set; }

  [PXString(15)]
  [PXUIField(DisplayName = "Orig Service Contract ID", Enabled = false)]
  public virtual string OrigServiceContractRefNbr { get; set; }

  [PXString(30)]
  public virtual string EmailNotificationCD { get; set; }

  public static bool TryParse(object row, out FSServiceContract fsServiceContractRow)
  {
    fsServiceContractRow = (FSServiceContract) null;
    if (!(row is FSServiceContract))
      return false;
    fsServiceContractRow = (FSServiceContract) row;
    return true;
  }

  public bool isEditable() => this.Status == "D" || this.Status == "A";

  public class PK : PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>
  {
    public static FSServiceContract Find(
      PXGraph graph,
      int? serviceContractID,
      PKFindOptions options = 0)
    {
      return (FSServiceContract) PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.FindBy(graph, (object) serviceContractID, options);
    }
  }

  public class UK : PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.refNbr>
  {
    public static FSServiceContract Find(PXGraph graph, string refNbr, PKFindOptions options = 0)
    {
      return (FSServiceContract) PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.refNbr>.FindBy(graph, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FSServiceContract>.By<FSServiceContract.branchID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSServiceContract>.By<FSServiceContract.customerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<FSServiceContract>.By<FSServiceContract.customerID, FSServiceContract.customerLocationID>
    {
    }

    public class BillCustomer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSServiceContract>.By<FSServiceContract.billCustomerID>
    {
    }

    public class BillCustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<FSServiceContract>.By<FSServiceContract.billCustomerID, FSServiceContract.billLocationID>
    {
    }

    public class MasterContract : 
      PrimaryKeyOf<FSMasterContract>.By<FSMasterContract.masterContractID>.ForeignKeyOf<FSServiceContract>.By<FSServiceContract.masterContractID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<FSServiceContract>.By<FSServiceContract.vendorID>
    {
    }

    public class SalesPerson : 
      PrimaryKeyOf<PX.Objects.AR.SalesPerson>.By<PX.Objects.AR.SalesPerson.salesPersonID>.ForeignKeyOf<FSServiceContract>.By<FSServiceContract.salesPersonID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<FSServiceContract>.By<FSServiceContract.projectID>
    {
    }

    public class DefaultTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<FSServiceContract>.By<FSServiceContract.projectID, FSServiceContract.dfltProjectTaskID>
    {
    }
  }

  public abstract class refNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class customerContractNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class customerID : IBqlField, IBqlOperand
  {
  }

  public abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceContract.serviceContractID>
  {
  }

  public abstract class classID : IBqlField, IBqlOperand
  {
  }

  public abstract class billingPeriod : ListField_Contract_BillingPeriod
  {
  }

  public abstract class billingType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceContract.billingType>
  {
    public abstract class Values : ListField.ServiceContractBillingType
    {
    }
  }

  public abstract class billTo : ListField_Contract_BillTo
  {
  }

  public abstract class billCustomerID : IBqlField, IBqlOperand
  {
  }

  public abstract class billLocationID : IBqlField, IBqlOperand
  {
  }

  public abstract class branchID : IBqlField, IBqlOperand
  {
  }

  public abstract class branchLocationID : IBqlField, IBqlOperand
  {
  }

  public abstract class customerLocationID : IBqlField, IBqlOperand
  {
  }

  public abstract class docDesc : IBqlField, IBqlOperand
  {
  }

  public abstract class expirationType : ListField_Contract_ExpirationType
  {
  }

  public abstract class startDate : IBqlField, IBqlOperand
  {
  }

  public abstract class durationType : SC_Duration_Type
  {
  }

  public abstract class renewalDate : IBqlField, IBqlOperand
  {
  }

  public abstract class endDate : IBqlField, IBqlOperand
  {
  }

  public abstract class duration : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceContract.duration>
  {
  }

  public abstract class lastBillingInvoiceDate : IBqlField, IBqlOperand
  {
  }

  public abstract class masterContractID : IBqlField, IBqlOperand
  {
  }

  public abstract class nextBillingInvoiceDate : IBqlField, IBqlOperand
  {
  }

  public abstract class recordType : ListField_RecordType_ContractSchedule
  {
  }

  public abstract class status : ListField_Status_ServiceContract
  {
  }

  public abstract class statusEffectiveFromDate : IBqlField, IBqlOperand
  {
  }

  public abstract class statusEffectiveUntilDate : IBqlField, IBqlOperand
  {
  }

  public abstract class upcomingStatus : ListField_Status_ServiceContract
  {
  }

  public abstract class customerContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceContract.customerContactID>
  {
  }

  public abstract class vendorID : IBqlField, IBqlOperand
  {
  }

  public abstract class sourcePrice : ListField_Contract_SourcePrice
  {
  }

  public abstract class salesPersonID : IBqlField, IBqlOperand
  {
  }

  public abstract class commissionable : IBqlField, IBqlOperand
  {
  }

  public abstract class scheduleGenType : ListField_ScheduleGenType_ContractSchedule
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceContract.projectID>
  {
  }

  public abstract class dfltProjectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceContract.dfltProjectTaskID>
  {
  }

  public abstract class dfltCostCodeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceContract.dfltCostCodeID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSServiceContract.noteID>
  {
  }

  public abstract class createdByID : IBqlField, IBqlOperand
  {
  }

  public abstract class createdByScreenID : IBqlField, IBqlOperand
  {
  }

  public abstract class createdDateTime : IBqlField, IBqlOperand
  {
  }

  public abstract class lastModifiedByID : IBqlField, IBqlOperand
  {
  }

  public abstract class lastModifiedByScreenID : IBqlField, IBqlOperand
  {
  }

  public abstract class lastModifiedDateTime : IBqlField, IBqlOperand
  {
  }

  public abstract class Tstamp : IBqlField, IBqlOperand
  {
  }

  public abstract class selected : IBqlField, IBqlOperand
  {
  }

  public abstract class isFixedRateContract : IBqlField, IBqlOperand
  {
  }

  public abstract class reportServiceContractID : IBqlField, IBqlOperand
  {
  }

  public abstract class mem_ShowPriceTab : IBqlField, IBqlOperand
  {
  }

  public abstract class mem_ShowScheduleTab : IBqlField, IBqlOperand
  {
  }

  public abstract class showInvoicesTab : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceContract.showInvoicesTab>
  {
  }

  public abstract class usageBillingCycleID : IBqlField, IBqlOperand
  {
  }

  public abstract class activePeriodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceContract.activePeriodID>
  {
  }

  public abstract class origServiceContractRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceContract.origServiceContractRefNbr>
  {
  }

  public abstract class emailNotificationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceContract.emailNotificationCD>
  {
  }
}
