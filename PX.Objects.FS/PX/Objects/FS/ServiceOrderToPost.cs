// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceOrderToPost
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXProjection(typeof (Select2<FSServiceOrder, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSServiceOrder.srvOrdType>>, CrossJoin<FSSetup, InnerJoin<FSCustomerBillingSetup, On<FSCustomerBillingSetup.customerID, Equal<FSServiceOrder.billCustomerID>, And<Where2<Where<FSSetup.customerMultipleBillingOptions, Equal<True>, And<FSCustomerBillingSetup.srvOrdType, Equal<FSServiceOrder.srvOrdType>>>, Or<Where<FSSetup.customerMultipleBillingOptions, Equal<False>, And<FSCustomerBillingSetup.srvOrdType, IsNull>>>>>>, InnerJoin<FSBillingCycle, On<FSBillingCycle.billingCycleID, Equal<FSCustomerBillingSetup.billingCycleID>>, LeftJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSServiceOrder.billServiceContractID>>>>>>>, Where2<Where<FSServiceOrder.closed, Equal<True>, Or<FSServiceOrder.completed, Equal<True>, Or<Where<FSBillingCycle.invoiceOnlyCompletedServiceOrder, Equal<False>, And<FSServiceOrder.openDoc, Equal<True>>>>>>, And<FSServiceOrder.allowInvoice, Equal<True>, And<Where2<Where<FSServiceOrder.billServiceContractID, IsNull, And<FSServiceOrder.billContractPeriodID, IsNull>>, Or<FSServiceContract.isFixedRateContract, Equal<True>>>>>>>))]
[PXGroupMask(typeof (InnerJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<ServiceOrderToPost.billCustomerID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>))]
[Serializable]
public class ServiceOrderToPost : FSServiceOrder, IPostLine
{
  [PXDBString(2, BqlField = typeof (FSSrvOrdType.postTo))]
  [FSPostTo.List]
  [PXUIField(DisplayName = "Post To")]
  public virtual 
  #nullable disable
  string PostTo { get; set; }

  [PXDBString(2, BqlField = typeof (FSSrvOrdType.postOrderType))]
  public virtual string PostOrderType { get; set; }

  [PXDBString(2, BqlField = typeof (FSSrvOrdType.postOrderTypeNegativeBalance))]
  public virtual string PostOrderTypeNegativeBalance { get; set; }

  [PXDBBool(BqlField = typeof (FSSrvOrdType.postNegBalanceToAP))]
  [PXUIField(DisplayName = "Create a Bill Document in AP for Negative Balances")]
  public virtual bool? PostNegBalanceToAP { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (FSSrvOrdType.dfltTermIDARSO))]
  public virtual string DfltTermIDARSO { get; set; }

  [PXDBInt(BqlField = typeof (FSCustomerBillingSetup.billingCycleID))]
  [PXSelector(typeof (Search<FSBillingCycle.billingCycleID>), SubstituteKey = typeof (FSBillingCycle.billingCycleCD), DescriptionField = typeof (FSBillingCycle.descr))]
  [PXUIField(DisplayName = "Billing Cycle ID", Enabled = false)]
  public virtual int? BillingCycleID { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (FSCustomerBillingSetup.frequencyType))]
  [ListField_Frequency_Type.ListAtrribute]
  [PXUIField(DisplayName = "Frequency Type", Enabled = false)]
  public virtual string FrequencyType { get; set; }

  [PXDBInt(BqlField = typeof (FSCustomerBillingSetup.weeklyFrequency))]
  [PXUIField(DisplayName = "Frequency Week Day")]
  [ListField_WeekDaysNumber.ListAtrribute]
  [PXDefault]
  public virtual int? WeeklyFrequency { get; set; }

  [PXDBInt(BqlField = typeof (FSCustomerBillingSetup.monthlyFrequency))]
  [PXUIField(DisplayName = "Frequency Month Day")]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 /*0x10*/, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 /*0x1F*/}, new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})]
  [PXDefault]
  public virtual int? MonthlyFrequency { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (FSCustomerBillingSetup.sendInvoicesTo))]
  [ListField_Send_Invoices_To.ListAtrribute]
  [PXUIField(DisplayName = "Send Invoices to")]
  public virtual string SendInvoicesTo { get; set; }

  [PXDBBool(BqlField = typeof (FSBillingCycle.groupBillByLocations))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Create Separate Invoices for Customer Locations")]
  public virtual bool? GroupBillByLocations { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">AAAAAAAAAAAAAAA", BqlField = typeof (FSBillingCycle.billingCycleCD))]
  [PXUIField]
  [PXSelector(typeof (FSBillingCycle.billingCycleCD))]
  public virtual string BillingCycleCD { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (FSBillingCycle.billingCycleType))]
  [ListField_Billing_Cycle_Type.ListAtrribute]
  [PXDefault("TC")]
  public virtual string BillingCycleType { get; set; }

  [PXDBBool(BqlField = typeof (FSBillingCycle.invoiceOnlyCompletedServiceOrder))]
  [PXUIField(DisplayName = "Invoice only completed or closed Service Orders")]
  public virtual bool? InvoiceOnlyCompletedServiceOrder { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (FSBillingCycle.timeCycleType))]
  [PXDefault("MT")]
  [ListField_Time_Cycle_Type.ListAtrribute]
  [PXUIField(DisplayName = "Time Cycle Type")]
  public virtual string TimeCycleType { get; set; }

  [PXDBInt(BqlField = typeof (FSBillingCycle.timeCycleWeekDay))]
  [PXUIField(DisplayName = "Day of Week", Visible = false)]
  [ListField_WeekDaysNumber.ListAtrribute]
  public virtual int? TimeCycleWeekDay { get; set; }

  [PXDBInt(BqlField = typeof (FSBillingCycle.timeCycleDayOfMonth))]
  [PXUIField(DisplayName = "Day of Month")]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 /*0x10*/, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 /*0x1F*/}, new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})]
  public virtual int? TimeCycleDayOfMonth { get; set; }

  [PXDBString(4, BqlField = typeof (FSSrvOrdType.srvOrdType))]
  public virtual string DocType { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (FSServiceOrder.taxZoneID))]
  [PXUIField(DisplayName = "Customer Tax Zone")]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  public override string TaxZoneID { get; set; }

  [PXInt]
  public virtual int? AppointmentID { get; set; }

  [PXInt]
  public virtual int? RowIndex { get; set; }

  [PXString]
  public virtual string GroupKey { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Batch Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<FSPostBatch.batchID>), SubstituteKey = typeof (FSPostBatch.batchNbr))]
  public virtual int? BatchID { get; set; }

  [PXBool]
  public virtual bool? ErrorFlag { get; set; }

  public string EntityType => "SO";

  public new class PK : 
    PrimaryKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.srvOrdType, FSServiceOrder.refNbr>
  {
    public static ServiceOrderToPost Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (ServiceOrderToPost) PrimaryKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.srvOrdType, FSServiceOrder.refNbr>.FindBy(graph, (object) srvOrdType, (object) refNbr, options);
    }
  }

  public new static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.customerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.customerID, ServiceOrderToPost.locationID>
    {
    }

    public class BillCustomer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.billCustomerID>
    {
    }

    public class BillCustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.billCustomerID, ServiceOrderToPost.billLocationID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.branchID>
    {
    }

    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.srvOrdType>
    {
    }

    public class Address : 
      PrimaryKeyOf<FSAddress>.By<FSAddress.addressID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.serviceOrderAddressID>
    {
    }

    public class Contact : 
      PrimaryKeyOf<FSContact>.By<FSContact.contactID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.serviceOrderContactID>
    {
    }

    public class Contract : 
      PrimaryKeyOf<PX.Objects.CT.Contract>.By<PX.Objects.CT.Contract.contractID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.contractID>
    {
    }

    public class BranchLocation : 
      PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.branchLocationID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.projectID, ServiceOrderToPost.dfltProjectTaskID>
    {
    }

    public class WorkFlowStage : 
      PrimaryKeyOf<FSWFStage>.By<FSWFStage.wFStageID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.wFStageID>
    {
    }

    public class ServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.serviceContractID>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<FSSchedule>.By<FSSchedule.scheduleID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.scheduleID>
    {
    }

    public class BillServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.billServiceContractID>
    {
    }

    public class CustomerBillingSetup : 
      PrimaryKeyOf<FSCustomerBillingSetup>.By<FSCustomerBillingSetup.cBID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.cBID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.taxZoneID>
    {
    }

    public class Problem : 
      PrimaryKeyOf<FSProblem>.By<FSProblem.problemCD>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.problemID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.curyID>
    {
    }

    public class PostSOOrder : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.postOrderType>
    {
    }

    public class PostOrderNegativeBalance : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.postOrderTypeNegativeBalance>
    {
    }

    public class DfltTermIDARSO : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.dfltTermIDARSO>
    {
    }

    public class BillingCycle : 
      PrimaryKeyOf<FSBillingCycle>.By<FSBillingCycle.billingCycleID>.ForeignKeyOf<ServiceOrderToPost>.By<ServiceOrderToPost.billingCycleID>
    {
    }
  }

  public abstract class postTo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ServiceOrderToPost.postTo>
  {
  }

  public abstract class postOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ServiceOrderToPost.postOrderType>
  {
  }

  public abstract class postOrderTypeNegativeBalance : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ServiceOrderToPost.postOrderTypeNegativeBalance>
  {
  }

  public abstract class postNegBalanceToAP : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ServiceOrderToPost.postNegBalanceToAP>
  {
  }

  public abstract class dfltTermIDARSO : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ServiceOrderToPost.dfltTermIDARSO>
  {
  }

  public abstract class billingCycleID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ServiceOrderToPost.billingCycleID>
  {
  }

  public abstract class frequencyType : ListField_Frequency_Type
  {
  }

  public abstract class weeklyFrequency : ListField_WeekDaysNumber
  {
  }

  public abstract class monthlyFrequency : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ServiceOrderToPost.monthlyFrequency>
  {
  }

  public abstract class sendInvoicesTo : ListField_Send_Invoices_To
  {
  }

  public abstract class groupBillByLocations : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ServiceOrderToPost.groupBillByLocations>
  {
  }

  public abstract class billingCycleCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ServiceOrderToPost.billingCycleCD>
  {
  }

  public abstract class billingCycleType : ListField_Billing_Cycle_Type
  {
  }

  public abstract class invoiceOnlyCompletedServiceOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ServiceOrderToPost.invoiceOnlyCompletedServiceOrder>
  {
  }

  public abstract class timeCycleType : ListField_Time_Cycle_Type
  {
  }

  public abstract class timeCycleWeekDay : ListField_WeekDaysNumber
  {
  }

  public abstract class timeCycleDayOfMonth : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ServiceOrderToPost.timeCycleDayOfMonth>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ServiceOrderToPost.docType>
  {
  }

  public new abstract class taxZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ServiceOrderToPost.taxZoneID>
  {
  }

  public abstract class appointmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceOrderToPost.appointmentID>
  {
  }

  public abstract class rowIndex : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceOrderToPost.rowIndex>
  {
  }

  public abstract class groupKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ServiceOrderToPost.groupKey>
  {
  }

  public abstract class batchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceOrderToPost.batchID>
  {
  }

  public abstract class errorFlag : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ServiceOrderToPost.errorFlag>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceOrderToPost.customerID>
  {
  }

  public new abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceOrderToPost.locationID>
  {
  }

  public new abstract class billCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ServiceOrderToPost.billCustomerID>
  {
  }

  public new abstract class billLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ServiceOrderToPost.billLocationID>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceOrderToPost.branchID>
  {
  }

  public new abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ServiceOrderToPost.srvOrdType>
  {
  }

  public new abstract class billingBy : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ServiceOrderToPost.billingBy>
  {
  }

  public new abstract class serviceOrderAddressID : IBqlField, IBqlOperand
  {
  }

  public new abstract class serviceOrderContactID : IBqlField, IBqlOperand
  {
  }

  public new abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceOrderToPost.contractID>
  {
  }

  public new abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ServiceOrderToPost.branchLocationID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceOrderToPost.projectID>
  {
  }

  public new abstract class dfltProjectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ServiceOrderToPost.dfltProjectTaskID>
  {
  }

  public new abstract class wFStageID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceOrderToPost.wFStageID>
  {
  }

  public new abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ServiceOrderToPost.serviceContractID>
  {
  }

  public new abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceOrderToPost.scheduleID>
  {
  }

  public new abstract class billServiceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ServiceOrderToPost.billServiceContractID>
  {
  }

  public new abstract class cBID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceOrderToPost.cBID>
  {
  }

  public new abstract class problemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceOrderToPost.problemID>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ServiceOrderToPost.curyInfoID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ServiceOrderToPost.curyID>
  {
  }
}
