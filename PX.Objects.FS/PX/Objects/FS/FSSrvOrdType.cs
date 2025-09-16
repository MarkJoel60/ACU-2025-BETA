// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSrvOrdType
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
using PX.Objects.GL;
using PX.Objects.PM;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Order Type")]
[PXPrimaryGraph(typeof (SvrOrdTypeMaint))]
[Serializable]
public class FSSrvOrdType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  [PXUIField(Enabled = false)]
  public virtual int? SrvOrdTypeID { get; set; }

  [PXDBString(4, IsKey = true, InputMask = ">AAAA", IsFixed = true)]
  [PXUIField]
  [PXSelector(typeof (FSSrvOrdType.srvOrdType), DescriptionField = typeof (FSSrvOrdType.descr))]
  [PXDefault]
  [NormalizeWhiteSpace]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Partial Billing", Enabled = false)]
  public virtual bool? AllowPartialBilling { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Quick Process")]
  public virtual bool? AllowQuickProcess { get; set; }

  [PXDBString(2)]
  [ListField_SrvOrdType_AddressSource.ListAtrribute]
  [PXDefault("BA")]
  [PXUIField(DisplayName = "Take Address and Contact Information From")]
  public virtual string AppAddressSource { get; set; }

  [PXDBString(2)]
  [PXUIField(DisplayName = "Appointment Contact info source", Visible = false)]
  public virtual string AppContactInfoSource { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Business Account", Enabled = false)]
  public virtual bool? BAccountRequired { get; set; }

  [PXDBString(2, IsFixed = true)]
  [ListField.ServiceOrderTypeBehavior.List]
  [PXDefault("RE")]
  [PXUIField(DisplayName = "Behavior")]
  public virtual string Behavior { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Bill Separately", Enabled = false)]
  public virtual bool? BillSeparately { get; set; }

  [PXDefault]
  [SubAccountMask(DisplayName = "Combine Sales Sub. From")]
  public virtual string CombineSubFrom { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Complete Service Order When Its Appointments Are Completed")]
  public virtual bool? CompleteSrvOrdWhenSrvDone { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIEnabled(typeof (Where<Current<FSSrvOrdType.completeSrvOrdWhenSrvDone>, Equal<True>>))]
  [PXUIField(DisplayName = "Close Service Order When Its Appointments Are Closed")]
  public virtual bool? CloseSrvOrdWhenSrvDone { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.customer>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
  public virtual string DfltTermIDARSO { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.vendor>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
  public virtual string DfltTermIDAP { get; set; }

  [CostCode(Filterable = false, SkipVerification = true)]
  public virtual int? DfltCostCodeID { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXFormula(typeof (Default<FSSrvOrdType.postTo>))]
  [PXFormula(typeof (Default<FSSrvOrdType.behavior>))]
  [PXUIField(DisplayName = "Post Pickup/Delivery Items to Inventory")]
  public virtual bool? EnableINPosting { get; set; }

  [PXDBString(4)]
  [ListField_SrvOrdType_GenerateInvoiceBy.ListAtrribute]
  [PXDefault("SORD")]
  [PXUIField(DisplayName = "Generate Invoice By")]
  public virtual string GenerateInvoiceBy { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Bill Only Closed Appointments")]
  public virtual bool? AllowInvoiceOnlyClosedAppointment { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXFormula(typeof (Default<FSSrvOrdType.postTo>))]
  [PXUIField(DisplayName = "Create AP Bills for Negative Balances")]
  public virtual bool? PostNegBalanceToAP { get; set; }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType, Where<PX.Objects.SO.SOOrderType.active, Equal<True>, And<FSxSOOrderType.enableFSIntegration, Equal<True>>>>), DescriptionField = typeof (PX.Objects.SO.SOOrderType.descr))]
  public virtual string PostOrderType { get; set; }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType, Where<PX.Objects.SO.SOOrderType.active, Equal<True>, And<FSxSOOrderType.enableFSIntegration, Equal<True>, And<PX.Objects.SO.SOOrderType.aRDocType, Equal<ARDocType.creditMemo>>>>>), DescriptionField = typeof (PX.Objects.SO.SOOrderType.descr))]
  public virtual string PostOrderTypeNegativeBalance { get; set; }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXDefault(typeof (Search<PX.Objects.SO.SOOrderType.orderType, Where<PX.Objects.SO.SOOrderType.active, Equal<True>, And<PX.Objects.SO.SOOrderType.orderType, In3<SOOrderTypeConstants.salesOrder, SOOrderTypeConstants.transferOrder>>>>))]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType, Where<PX.Objects.SO.SOOrderType.active, Equal<True>, And<PX.Objects.SO.SOOrderType.behavior, In3<SOBehavior.sO, SOBehavior.tR>>>>), DescriptionField = typeof (PX.Objects.SO.SOOrderType.descr))]
  public virtual string AllocationOrderType { get; set; }

  [PXDBString(2)]
  [PXDefault]
  [FSPostTo.List]
  [PXUIField(DisplayName = "Generated Billing Documents")]
  public virtual string PostTo { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Appointment Confirmation Required", Visible = false)]
  public virtual bool? RequireAppConfirmation { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Address Validation")]
  public virtual bool? RequireAddressValidation { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Contact")]
  public virtual bool? RequireContact { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Customer Signature on Mobile App")]
  public virtual bool? RequireCustomerSignature { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Room")]
  public virtual bool? RequireRoom { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Route", Enabled = false)]
  public virtual bool? RequireRoute { get; set; }

  [PXDBString(2)]
  [ListField_SrvOrdType_SalesAcctSource.ListAtrribute]
  [PXDefault("CL")]
  [PXUIField(DisplayName = "Use Sales Account From")]
  public virtual string SalesAcctSource { get; set; }

  [PXDBString(10)]
  [PXDefault]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField(DisplayName = "Numbering Sequence")]
  public virtual string SrvOrdNumberingID { get; set; }

  [SubAccount]
  [PXUIField(DisplayName = "General Subaccount")]
  public virtual int? SubID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Time Approval to Close/Bill Appointments")]
  public virtual bool? RequireTimeApprovalToInvoice { get; set; }

  [PXDBBool]
  [PXDefault(typeof (Search<FSSetup.enableEmpTimeCardIntegration>))]
  [PXUIField(DisplayName = "Automatically Create Time Activities from Appointments")]
  [PXUIVisible(typeof (IIf<FeatureInstalled<FeaturesSet.timeReportingModule>, True, False>))]
  public virtual bool? CreateTimeActivitiesFromAppointment { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault(typeof (Search<EPSetup.regularHoursType>))]
  [PXSelector(typeof (EPEarningType.typeCD))]
  [PXUIField(DisplayName = "Default Earning Type")]
  public virtual string DfltEarningType { get; set; }

  [PXDefault]
  [PXForeignReference(typeof (FSSrvOrdType.FK.AccountGroup))]
  [PXUIRequired(typeof (Where<FSSrvOrdType.postTo, Equal<FSPostTo.Projects>>))]
  [PXUIVisible(typeof (Where<FSSrvOrdType.postTo, Equal<FSPostTo.Projects>>))]
  [AccountGroup]
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new Type[] {typeof (PMAccountGroup.groupCD)})]
  public virtual int? AccountGroupID { get; set; }

  [PXDBString(20, IsUnicode = true)]
  [PXDefault]
  [PXUIRequired(typeof (Where<FSSrvOrdType.postTo, Equal<FSPostTo.Projects>, And<FeatureInstalled<FeaturesSet.inventory>>>))]
  [PXUIVisible(typeof (Where<FSSrvOrdType.postTo, Equal<FSPostTo.Projects>>))]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.issue>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXForeignReference(typeof (FSSrvOrdType.FK.ReasonCode))]
  public virtual string ReasonCode { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIVisible(typeof (Where<FSSrvOrdType.postTo, Equal<FSPostTo.Projects>>))]
  [PXUIField(DisplayName = "Automatically Release Project Transactions")]
  public virtual bool? ReleaseProjectTransactionOnInvoice { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIVisible(typeof (Where<FSSrvOrdType.postTo, Equal<FSPostTo.Projects>>))]
  [PXUIEnabled(typeof (Where<FSSrvOrdType.billingType, NotEqual<ListField_SrvOrdType_BillingType.CostAsCost>>))]
  [PXUIField(DisplayName = "Automatically Release Issues", FieldClass = "DISTINV")]
  public virtual bool? ReleaseIssueOnInvoice { get; set; }

  [PXDBString(2, IsFixed = true)]
  [ListField_SrvOrdType_BillingType.ListAtrribute]
  [PXDefault("CC")]
  [PXUIVisible(typeof (Where<FSSrvOrdType.postTo, Equal<FSPostTo.Projects>>))]
  [PXUIField(DisplayName = "Billing Type")]
  public virtual string BillingType { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXFormula(typeof (Default<FSSrvOrdType.behavior>))]
  [PXDefault(typeof (Switch<Case<Where<FSSrvOrdType.behavior, Equal<ListField.ServiceOrderTypeBehavior.quote>>, ListField.ServiceOrderWorkflowTypes.quote>, ListField.ServiceOrderWorkflowTypes.simple>))]
  [ListField.ServiceOrderWorkflowTypes.List]
  [PXUIField]
  public virtual string ServiceOrderWorkflowTypeID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault(typeof (ListField.AppointmentWorkflowTypes.simple))]
  [ListField.AppointmentWorkflowTypes.List]
  [PXUIVisible(typeof (Where<FSSrvOrdType.behavior, NotEqual<ListField.ServiceOrderTypeBehavior.quote>>))]
  [PXUIField]
  public virtual string AppointmentWorkflowTypeID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Set Start Time in Appointment")]
  public virtual bool? OnStartApptSetStartTimeInHeader { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Set Not Started Items as In Process")]
  public virtual bool? OnStartApptSetNotStartItemInProcess { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Start Logging for Unassigned Staff")]
  public virtual bool? OnStartApptStartUnassignedStaff { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Start Logging for Services and Assigned Staff (If Any)")]
  public virtual bool? OnStartApptStartServiceAndStaff { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Set End Time in Appointment")]
  public virtual bool? OnCompleteApptSetEndTimeInHeader { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("CP")]
  [PXUIField(DisplayName = "Status to Set for In Process Items")]
  [ListField_OnCompleteApptSetInProcessItemsAs.List]
  public virtual string OnCompleteApptSetInProcessItemsAs { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("CP")]
  [PXUIField(DisplayName = "Status to Set for Not Started Items")]
  [ListField_OnCompleteApptSetNotStartedItemsAs.List]
  public virtual string OnCompleteApptSetNotStartedItemsAs { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Update Log Start Time When Appointment Start Time is Updated")]
  public virtual bool? OnStartTimeChangeUpdateLogStartTime { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Update Log End Time When Appointment End Time is Updated")]
  public virtual bool? OnEndTimeChangeUpdateLogEndTime { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Service Logs on Appointment Completion")]
  public virtual bool? OnCompleteApptRequireLog { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Start Appointment When Travel Is Completed")]
  public virtual bool? OnTravelCompleteStartAppt { get; set; }

  [Service(null, DisplayName = "Default Travel Item")]
  [PXRestrictor(typeof (Where<FSxService.isTravelItem, Equal<True>>), "This item cannot be selected as the default travel item. Make sure that the Is a Travel Item check box is selected on the Non-Stock Items (IN202000) form or select another item.", new Type[] {})]
  public virtual int? DfltBillableTravelItem { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Update Appointment Time Based on Logged Time")]
  [PXDefault(false)]
  [PXFormula(typeof (Default<FSSrvOrdType.onStartTimeChangeUpdateLogStartTime, FSSrvOrdType.onEndTimeChangeUpdateLogEndTime>))]
  [PXUIEnabled(typeof (Where<Current<FSSrvOrdType.onStartTimeChangeUpdateLogStartTime>, Equal<False>, And<Current<FSSrvOrdType.onEndTimeChangeUpdateLogEndTime>, Equal<False>>>))]
  public virtual bool? SetTimeInHeaderBasedOnLog { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manually Manage Time")]
  public virtual bool? AllowManualLogTimeEdition { get; set; }

  [PXUIVisible(typeof (Where<FSSrvOrdType.postTo, NotEqual<FSPostTo.Projects>>))]
  [SalesPerson(DisplayName = "Salesperson ID")]
  public virtual int? SalesPersonID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIVisible(typeof (Where<FSSrvOrdType.postTo, NotEqual<FSPostTo.Projects>>))]
  [PXUIField(DisplayName = "Commissionable")]
  public virtual bool? Commissionable { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Notes from Customer")]
  public virtual bool? CopyNotesFromCustomer { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Attachments from Customer")]
  public virtual bool? CopyAttachmentsFromCustomer { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Notes from Customer Location")]
  public virtual bool? CopyNotesFromCustomerLocation { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Attachments from Customer Location")]
  public virtual bool? CopyAttachmentsFromCustomerLocation { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Notes and Comments to Appointment")]
  public virtual bool? CopyNotesToAppoinment { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Attachments to Appointment")]
  public virtual bool? CopyAttachmentsToAppoinment { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Notes to Invoice")]
  public virtual bool? CopyNotesToInvoice { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Attachments to Invoice")]
  public virtual bool? CopyAttachmentsToInvoice { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Line Notes to Invoice")]
  public virtual bool? CopyLineNotesToInvoice { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Line Attachments to Invoice")]
  public virtual bool? CopyLineAttachmentsToInvoice { get; set; }

  [PXBool]
  public virtual bool? ShowQuickProcessTab
  {
    get
    {
      return new bool?(this.AllowQuickProcess.GetValueOrDefault() && this.PostTo != "NA" && this.PostTo != "AR");
    }
  }

  [PXBool]
  [PXDefault]
  [PXUIField]
  [PXFormula(typeof (Switch<Case<Where2<Where<FSSrvOrdType.postTo, Equal<FSPostTo.Sales_Order_Invoice>, Or<FSSrvOrdType.postTo, Equal<FSPostTo.Sales_Order_Module>, Or<FSSrvOrdType.postTo, Equal<FSPostTo.Projects>>>>, And<FSSrvOrdType.behavior, NotEqual<ListField.ServiceOrderTypeBehavior.quote>, And<FSSrvOrdType.behavior, NotEqual<ListField.ServiceOrderTypeBehavior.internalAppointment>>>>, True>, False>))]
  public virtual bool? PostToSOSIPM { get; set; }

  [PXBool]
  [PXDefault]
  [PXUIField]
  [PXFormula(typeof (Switch<Case<Where<FSSrvOrdType.postToSOSIPM, Equal<True>, Or<FSSrvOrdType.behavior, Equal<ListField.ServiceOrderTypeBehavior.quote>>>, True>, False>))]
  public virtual bool AllowInventoryItems { get; set; }

  [PXDBBool]
  [PXDefault(typeof (IsNull<FSSrvOrdType.postToSOSIPM, False>))]
  [PXUIField]
  [PXUIVisible(typeof (IsNull<FSSrvOrdType.postToSOSIPM, False>))]
  public virtual bool? SetLotSerialNbrInAppts { get; set; }

  public class PK : PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>
  {
    public static FSSrvOrdType Find(PXGraph graph, string srvOrdType, PKFindOptions options = 0)
    {
      return (FSSrvOrdType) PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.FindBy(graph, (object) srvOrdType, options);
    }
  }

  public class UK : PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdTypeID>
  {
    public static FSSrvOrdType Find(PXGraph graph, int? srvOrdTypeID, PKFindOptions options = 0)
    {
      return (FSSrvOrdType) PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdTypeID>.FindBy(graph, (object) srvOrdTypeID, options);
    }
  }

  public static class FK
  {
    public class DefaultTermsARSO : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<FSSrvOrdType>.By<FSSrvOrdType.dfltTermIDARSO>
    {
    }

    public class DefaultTermsAP : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<FSSrvOrdType>.By<FSSrvOrdType.dfltTermIDAP>
    {
    }

    public class DefaultCostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<FSSrvOrdType>.By<FSSrvOrdType.dfltCostCodeID>
    {
    }

    public class PostSOOrder : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<FSSrvOrdType>.By<FSSrvOrdType.postOrderType>
    {
    }

    public class PostOrderNegativeBalance : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<FSSrvOrdType>.By<FSSrvOrdType.postOrderTypeNegativeBalance>
    {
    }

    public class AllocationSOOrder : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<FSSrvOrdType>.By<FSSrvOrdType.allocationOrderType>
    {
    }

    public class ServiceOrderNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdNumberingID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<FSSrvOrdType>.By<FSSrvOrdType.subID>
    {
    }

    public class DefaultEarningType : 
      PrimaryKeyOf<EPEarningType>.By<EPEarningType.typeCD>.ForeignKeyOf<FSSrvOrdType>.By<FSSrvOrdType.dfltEarningType>
    {
    }

    public class AccountGroup : 
      PrimaryKeyOf<PMAccountGroup>.By<PMAccountGroup.groupID>.ForeignKeyOf<FSSrvOrdType>.By<FSSrvOrdType.accountGroupID>
    {
    }

    public class ReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<FSSrvOrdType>.By<FSSrvOrdType.reasonCode>
    {
    }

    public class DefaultBillableTravelItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSSrvOrdType>.By<FSSrvOrdType.dfltBillableTravelItem>
    {
    }

    public class SalesPerson : 
      PrimaryKeyOf<PX.Objects.AR.SalesPerson>.By<PX.Objects.AR.SalesPerson.salesPersonID>.ForeignKeyOf<FSSrvOrdType>.By<FSSrvOrdType.salesPersonID>
    {
    }
  }

  public abstract class srvOrdTypeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSrvOrdType.srvOrdTypeID>
  {
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSrvOrdType.srvOrdType>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSrvOrdType.active>
  {
  }

  public abstract class allowPartialBilling : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.allowPartialBilling>
  {
  }

  public abstract class allowQuickProcess : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.allowQuickProcess>
  {
  }

  public abstract class appAddressSource : ListField_SrvOrdType_AddressSource
  {
  }

  public abstract class appContactInfoSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSrvOrdType.appContactInfoSource>
  {
  }

  public abstract class bAccountRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.bAccountRequired>
  {
  }

  public abstract class behavior : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSrvOrdType.behavior>
  {
    public abstract class Values : ListField.ServiceOrderTypeBehavior
    {
    }
  }

  public abstract class billSeparately : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSrvOrdType.billSeparately>
  {
  }

  public abstract class combineSubFrom : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSrvOrdType.combineSubFrom>
  {
  }

  public abstract class completeSrvOrdWhenSrvDone : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.completeSrvOrdWhenSrvDone>
  {
  }

  public abstract class closeSrvOrdWhenSrvDone : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.closeSrvOrdWhenSrvDone>
  {
  }

  public abstract class dfltTermIDARSO : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSrvOrdType.dfltTermIDARSO>
  {
  }

  public abstract class dfltTermIDAP : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSrvOrdType.dfltTermIDAP>
  {
  }

  public abstract class dfltCostCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSrvOrdType.dfltCostCodeID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSrvOrdType.descr>
  {
  }

  public abstract class enableINPosting : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSrvOrdType.enableINPosting>
  {
  }

  public abstract class generateInvoiceBy : ListField_SrvOrdType_GenerateInvoiceBy
  {
  }

  public abstract class allowInvoiceOnlyClosedAppointment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.allowInvoiceOnlyClosedAppointment>
  {
  }

  public abstract class postNegBalanceToAP : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.postNegBalanceToAP>
  {
  }

  public abstract class postOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSrvOrdType.postOrderType>
  {
  }

  public abstract class postOrderTypeNegativeBalance : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSrvOrdType.postOrderTypeNegativeBalance>
  {
  }

  public abstract class allocationOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSrvOrdType.allocationOrderType>
  {
  }

  public abstract class postTo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSrvOrdType.postTo>
  {
  }

  public abstract class requireAppConfirmation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.requireAppConfirmation>
  {
  }

  public abstract class requireAddressValidation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.requireAddressValidation>
  {
  }

  public abstract class requireContact : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSrvOrdType.requireContact>
  {
  }

  public abstract class requireCustomerSignature : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.requireCustomerSignature>
  {
  }

  public abstract class requireRoom : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSrvOrdType.requireRoom>
  {
  }

  public abstract class requireRoute : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSrvOrdType.requireRoute>
  {
  }

  public abstract class salesAcctSource : ListField_SrvOrdType_SalesAcctSource
  {
  }

  public abstract class srvOrdNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSrvOrdType.srvOrdNumberingID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSrvOrdType.subID>
  {
  }

  public abstract class requireTimeApprovalToInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.requireTimeApprovalToInvoice>
  {
  }

  public abstract class createTimeActivitiesFromAppointment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.createTimeActivitiesFromAppointment>
  {
  }

  public abstract class dfltEarningType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSrvOrdType.dfltEarningType>
  {
  }

  public abstract class accountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSrvOrdType.accountGroupID>
  {
  }

  public abstract class reasonCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSrvOrdType.reasonCode>
  {
  }

  public abstract class releaseProjectTransactionOnInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.releaseProjectTransactionOnInvoice>
  {
  }

  public abstract class releaseIssueOnInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.releaseIssueOnInvoice>
  {
  }

  public abstract class billingType : ListField_SrvOrdType_BillingType
  {
  }

  public abstract class serviceOrderWorkflowTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSrvOrdType.serviceOrderWorkflowTypeID>
  {
    public abstract class Values : ListField.ServiceOrderWorkflowTypes
    {
    }
  }

  public abstract class appointmentWorkflowTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSrvOrdType.appointmentWorkflowTypeID>
  {
    public abstract class Values : ListField.AppointmentWorkflowTypes
    {
    }
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSrvOrdType.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSrvOrdType.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSrvOrdType.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSSrvOrdType.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSrvOrdType.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSrvOrdType.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSSrvOrdType.Tstamp>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSrvOrdType.noteID>
  {
  }

  public abstract class onStartApptSetStartTimeInHeader : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.onStartApptSetStartTimeInHeader>
  {
  }

  public abstract class onStartApptSetNotStartItemInProcess : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.onStartApptSetNotStartItemInProcess>
  {
  }

  public abstract class onStartApptStartUnassignedStaff : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.onStartApptStartUnassignedStaff>
  {
  }

  public abstract class onStartApptStartServiceAndStaff : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.onStartApptStartServiceAndStaff>
  {
  }

  public abstract class onCompleteApptSetEndTimeInHeader : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.onCompleteApptSetEndTimeInHeader>
  {
  }

  public abstract class onCompleteApptSetInProcessItemsAs : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSrvOrdType.onCompleteApptSetInProcessItemsAs>
  {
    public abstract class Values : ListField_OnCompleteApptSetInProcessItemsAs
    {
    }
  }

  public abstract class onCompleteApptSetNotStartedItemsAs : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSrvOrdType.onCompleteApptSetNotStartedItemsAs>
  {
    public abstract class Values : ListField_OnCompleteApptSetNotStartedItemsAs
    {
    }
  }

  public abstract class onStartTimeChangeUpdateLogStartTime : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.onStartTimeChangeUpdateLogStartTime>
  {
  }

  public abstract class onEndTimeChangeUpdateLogEndTime : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.onEndTimeChangeUpdateLogEndTime>
  {
  }

  public abstract class onCompleteApptRequireLog : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.onCompleteApptRequireLog>
  {
  }

  public abstract class onTravelCompleteStartAppt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.onTravelCompleteStartAppt>
  {
  }

  public abstract class dfltBillableTravelItem : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSSrvOrdType.dfltBillableTravelItem>
  {
  }

  public abstract class setTimeInHeaderBasedOnLog : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.setTimeInHeaderBasedOnLog>
  {
  }

  public abstract class allowManualLogTimeEdition : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.allowManualLogTimeEdition>
  {
  }

  public abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSrvOrdType.salesPersonID>
  {
  }

  public abstract class commissionable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSrvOrdType.commissionable>
  {
  }

  public abstract class copyNotesFromCustomer : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.copyNotesFromCustomer>
  {
  }

  public abstract class copyAttachmentsFromCustomer : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.copyAttachmentsFromCustomer>
  {
  }

  public abstract class copyNotesFromCustomerLocation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.copyNotesFromCustomerLocation>
  {
  }

  public abstract class copyAttachmentsFromCustomerLocation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.copyAttachmentsFromCustomerLocation>
  {
  }

  public abstract class copyNotesToAppoinment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.copyNotesToAppoinment>
  {
  }

  public abstract class copyAttachmentsToAppoinment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.copyAttachmentsToAppoinment>
  {
  }

  public abstract class copyNotesToInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.copyNotesToInvoice>
  {
  }

  public abstract class copyAttachmentsToInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.copyAttachmentsToInvoice>
  {
  }

  public abstract class copyLineNotesToInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.copyLineNotesToInvoice>
  {
  }

  public abstract class copyLineAttachmentsToInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.copyLineAttachmentsToInvoice>
  {
  }

  public abstract class showQuickProcessTab : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.showQuickProcessTab>
  {
  }

  public abstract class postToSOSIPM : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSrvOrdType.postToSOSIPM>
  {
  }

  public abstract class allowInventoryItems : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.allowInventoryItems>
  {
  }

  public abstract class setLotSerialNbrInAppts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdType.setLotSerialNbrInAppts>
  {
  }
}
