// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.AppointmentToPost
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.PM;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXProjection(typeof (Select2<FSAppointment, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSAppointment.srvOrdType>>, CrossJoin<FSSetup, InnerJoin<FSCustomerBillingSetup, On<FSCustomerBillingSetup.customerID, Equal<FSServiceOrder.billCustomerID>, And<Where2<Where<FSSetup.customerMultipleBillingOptions, Equal<True>, And<FSCustomerBillingSetup.srvOrdType, Equal<FSServiceOrder.srvOrdType>>>, Or<Where<FSSetup.customerMultipleBillingOptions, Equal<False>, And<FSCustomerBillingSetup.srvOrdType, IsNull>>>>>>, InnerJoin<FSBillingCycle, On<FSBillingCycle.billingCycleID, Equal<FSCustomerBillingSetup.billingCycleID>>, LeftJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSAppointment.billServiceContractID>>>>>>>>, Where2<Where2<Where<FSAppointment.pendingAPARSOPost, Equal<True>, And<Where2<Where<FSAppointment.billServiceContractID, IsNull, And<FSAppointment.billContractPeriodID, IsNull>>, Or<FSServiceContract.isFixedRateContract, Equal<True>>>>>, And<FSAppointment.closed, Equal<True>, Or<Where<FSSrvOrdType.allowInvoiceOnlyClosedAppointment, Equal<False>, And<FSAppointment.completed, Equal<True>, And<FSAppointment.timeRegistered, Equal<True>>>>>>>, And<Where<FSServiceOrder.closed, Equal<True>, Or<FSServiceOrder.completed, Equal<True>, Or<Where<FSBillingCycle.invoiceOnlyCompletedServiceOrder, Equal<False>, And<FSServiceOrder.openDoc, Equal<True>>>>>>>>>))]
[PXGroupMask(typeof (InnerJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<AppointmentToPost.billCustomerID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>))]
[Serializable]
public class AppointmentToPost : FSAppointment, IPostLine
{
  [PXDBString(4, IsFixed = true, IsKey = true, InputMask = ">AAAA", BqlField = typeof (FSAppointment.srvOrdType))]
  [PXUIField(DisplayName = "Service Order Type")]
  [FSSelectorSrvOrdTypeNOTQuote]
  [PXFieldDescription]
  public override 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "CCCCCCCCCCCCCCCCCCCC", BqlField = typeof (FSAppointment.refNbr))]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search2<FSAppointment.refNbr, LeftJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<FSServiceOrder.customerID>, And<PX.Objects.CR.Location.locationID, Equal<FSServiceOrder.locationID>>>>>>, Where2<Where<FSAppointment.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>, OrderBy<Desc<FSAppointment.refNbr>>>), new System.Type[] {typeof (FSAppointment.refNbr), typeof (PX.Objects.AR.Customer.acctCD), typeof (PX.Objects.AR.Customer.acctName), typeof (PX.Objects.CR.Location.locationCD), typeof (FSAppointment.docDesc), typeof (FSAppointment.status), typeof (FSAppointment.scheduledDateTimeBegin)})]
  public override string RefNbr { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointment.appointmentID))]
  public override int? AppointmentID { get; set; }

  [PXDefault]
  [PXDBString(15, IsUnicode = true, BqlField = typeof (FSAppointment.soRefNbr))]
  [PXUIField(DisplayName = "Service Order Nbr.")]
  [FSSelectorSORefNbr_Appointment]
  public override string SORefNbr { get; set; }

  [PXDBString(2, BqlField = typeof (FSSrvOrdType.postTo))]
  [FSPostTo.List]
  [PXUIField(DisplayName = "Post To")]
  public virtual string PostTo { get; set; }

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

  [PXString(2, IsFixed = true)]
  [ListField_Billing_By.ListAtrribute]
  [PXDBCalced(typeof (IsNull<FSServiceOrder.billingBy, FSBillingCycle.billingBy>), typeof (string))]
  public virtual string BillingBy { get; set; }

  [PXDBBool(BqlField = typeof (FSBillingCycle.groupBillByLocations))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Create Separate Invoices for Customer Locations")]
  public virtual bool? GroupBillByLocations { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.billCustomerID))]
  [PXUIField(DisplayName = "Billing Customer")]
  [FSSelectorCustomer]
  public override int? BillCustomerID { get; set; }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<AppointmentToPost.billCustomerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), BqlField = typeof (FSServiceOrder.billLocationID), DisplayName = "Billing Location", DirtyRead = true)]
  public virtual int? BillLocationID { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.branchLocationID))]
  [PXDefault(typeof (Search<FSxUserPreferences.dfltBranchLocationID, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>, And<UserPreferences.defBranchID, Equal<Current<FSServiceOrder.branchID>>>>>))]
  [PXUIField(DisplayName = "Branch Location")]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<FSServiceOrder.branchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  [PXFormula(typeof (Default<FSServiceOrder.branchID>))]
  public virtual int? BranchLocationID { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (FSServiceOrder.status))]
  [PXUIField(DisplayName = "Status")]
  [ListField.ServiceOrderStatus.List]
  public virtual string ServiceOrderStatus { get; set; }

  [PXDBString(40, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC", BqlField = typeof (FSServiceOrder.custWorkOrderRefNbr))]
  [PXUIField(DisplayName = "Customer Work Order Ref. Nbr.")]
  public virtual string CustWorkOrderRefNbr { get; set; }

  [PXDBString(40, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC", BqlField = typeof (FSServiceOrder.custPORefNbr))]
  [PXUIField(DisplayName = "Customer Purchase Order Ref. Nbr.")]
  public virtual string CustPORefNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (FSServiceOrder.postedBy))]
  public virtual string PostedBy { get; set; }

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

  [PXDBInt(BqlField = typeof (FSServiceOrder.projectID))]
  [PXUIField(DisplayName = "Project ID")]
  public override int? ProjectID { get; set; }

  [PXDBString(4, BqlField = typeof (FSSrvOrdType.srvOrdType))]
  public virtual string DocType { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (FSAppointment.taxZoneID))]
  [PXUIField(DisplayName = "Customer Tax Zone")]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  public override string TaxZoneID { get; set; }

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

  public string EntityType => "AP";

  public new class PK : 
    PrimaryKeyOf<AppointmentToPost>.By<AppointmentToPost.srvOrdType, AppointmentToPost.refNbr>
  {
    public static AppointmentToPost Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (AppointmentToPost) PrimaryKeyOf<AppointmentToPost>.By<AppointmentToPost.srvOrdType, AppointmentToPost.refNbr>.FindBy(graph, (object) srvOrdType, (object) refNbr, options);
    }
  }

  public new class UK : PrimaryKeyOf<AppointmentToPost>.By<AppointmentToPost.appointmentID>
  {
    public static AppointmentToPost Find(PXGraph graph, int? appointmentID, PKFindOptions options = 0)
    {
      return (AppointmentToPost) PrimaryKeyOf<AppointmentToPost>.By<AppointmentToPost.appointmentID>.FindBy(graph, (object) appointmentID, options);
    }
  }

  public new static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.customerID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.branchID>
    {
    }

    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.srvOrdType>
    {
    }

    public class ServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.srvOrdType, AppointmentToPost.soRefNbr>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.projectID, AppointmentToPost.dfltProjectTaskID>
    {
    }

    public class WorkFlowStage : 
      PrimaryKeyOf<FSWFStage>.By<FSWFStage.wFStageID>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.wFStageID>
    {
    }

    public class ServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.serviceContractID>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<FSSchedule>.By<FSSchedule.scheduleID>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.scheduleID>
    {
    }

    public class BillServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.billServiceContractID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.taxZoneID>
    {
    }

    public class Route : 
      PrimaryKeyOf<FSRoute>.By<FSRoute.routeID>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.routeID>
    {
    }

    public class RouteDocument : 
      PrimaryKeyOf<FSRouteDocument>.By<FSRouteDocument.routeDocumentID>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.routeDocumentID>
    {
    }

    public class Vehicle : 
      PrimaryKeyOf<FSVehicle>.By<FSVehicle.SMequipmentID>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.vehicleID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<AppointmentToPost>.By<FSAppointment.curyID>
    {
    }

    public class SalesPerson : 
      PrimaryKeyOf<PX.Objects.AR.SalesPerson>.By<PX.Objects.AR.SalesPerson.salesPersonID>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.salesPersonID>
    {
    }

    public class PostSOOrder : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.postOrderType>
    {
    }

    public class PostOrderNegativeBalance : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.postOrderTypeNegativeBalance>
    {
    }

    public class DefaultTermsARSO : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.dfltTermIDARSO>
    {
    }

    public class BillingCycle : 
      PrimaryKeyOf<FSBillingCycle>.By<FSBillingCycle.billingCycleID>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.billingCycleID>
    {
    }

    public class BillCustomer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.billCustomerID>
    {
    }

    public class BillCustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.billCustomerID, AppointmentToPost.billLocationID>
    {
    }

    public class PostBatch : 
      PrimaryKeyOf<FSPostBatch>.By<FSPostBatch.batchNbr>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.batchID>
    {
    }

    public class BranchLocation : 
      PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationID>.ForeignKeyOf<AppointmentToPost>.By<AppointmentToPost.branchLocationID>
    {
    }
  }

  public new abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AppointmentToPost.srvOrdType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AppointmentToPost.refNbr>
  {
  }

  public new abstract class appointmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AppointmentToPost.appointmentID>
  {
  }

  public new abstract class soRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AppointmentToPost.soRefNbr>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AppointmentToPost.branchID>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AppointmentToPost.customerID>
  {
  }

  public abstract class postTo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AppointmentToPost.postTo>
  {
  }

  public abstract class postOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AppointmentToPost.postOrderType>
  {
  }

  public abstract class postOrderTypeNegativeBalance : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AppointmentToPost.postOrderTypeNegativeBalance>
  {
  }

  public abstract class postNegBalanceToAP : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AppointmentToPost.postNegBalanceToAP>
  {
  }

  public abstract class dfltTermIDARSO : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AppointmentToPost.dfltTermIDARSO>
  {
  }

  public abstract class billingCycleID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AppointmentToPost.billingCycleID>
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
    AppointmentToPost.monthlyFrequency>
  {
  }

  public abstract class sendInvoicesTo : ListField_Send_Invoices_To
  {
  }

  public abstract class billingBy : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AppointmentToPost.billingBy>
  {
  }

  public abstract class groupBillByLocations : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AppointmentToPost.groupBillByLocations>
  {
  }

  public new abstract class billCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AppointmentToPost.billCustomerID>
  {
  }

  public abstract class billLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AppointmentToPost.billLocationID>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AppointmentToPost.branchLocationID>
  {
  }

  public abstract class serviceOrderStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AppointmentToPost.serviceOrderStatus>
  {
    public abstract class Values : ListField.ServiceOrderStatus
    {
    }
  }

  public abstract class custWorkOrderRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AppointmentToPost.custWorkOrderRefNbr>
  {
  }

  public abstract class custPORefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AppointmentToPost.custPORefNbr>
  {
  }

  public abstract class postedBy : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AppointmentToPost.postedBy>
  {
  }

  public abstract class billingCycleCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AppointmentToPost.billingCycleCD>
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
    AppointmentToPost.invoiceOnlyCompletedServiceOrder>
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
    AppointmentToPost.timeCycleDayOfMonth>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AppointmentToPost.projectID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AppointmentToPost.docType>
  {
  }

  public new abstract class taxZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AppointmentToPost.taxZoneID>
  {
  }

  public new abstract class dfltProjectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AppointmentToPost.dfltProjectTaskID>
  {
  }

  public new abstract class wFStageID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AppointmentToPost.wFStageID>
  {
  }

  public new abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AppointmentToPost.serviceContractID>
  {
  }

  public new abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AppointmentToPost.scheduleID>
  {
  }

  public new abstract class billServiceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AppointmentToPost.billServiceContractID>
  {
  }

  public new abstract class routeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AppointmentToPost.routeID>
  {
  }

  public new abstract class routeDocumentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AppointmentToPost.routeDocumentID>
  {
  }

  public new abstract class vehicleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AppointmentToPost.vehicleID>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  AppointmentToPost.curyInfoID>
  {
  }

  public new abstract class salesPersonID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AppointmentToPost.salesPersonID>
  {
  }

  public abstract class rowIndex : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AppointmentToPost.rowIndex>
  {
  }

  public abstract class groupKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AppointmentToPost.groupKey>
  {
  }

  public abstract class batchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AppointmentToPost.batchID>
  {
  }

  public abstract class errorFlag : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AppointmentToPost.errorFlag>
  {
  }
}
