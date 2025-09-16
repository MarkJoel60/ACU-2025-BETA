// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.MainAppointmentFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

/// <summary>Main filter for Scheduler Board</summary>
[PXCacheName("Calendar Board Filter")]
[Serializable]
public class MainAppointmentFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>Service Order type</summary>
  [PXString(4, IsFixed = true)]
  [PXUIField]
  [FSSelectorSrvOrdTypeNOTQuote]
  [PXRestrictor(typeof (Where<FSSrvOrdType.active, Equal<True>>), null, new System.Type[] {})]
  [PXUIVerify]
  [PXUnboundDefault(typeof (Coalesce<Search<FSxUserPreferences.dfltSrvOrdType, Where<BqlOperand<UserPreferences.userID, IBqlGuid>.IsEqual<BqlField<AccessInfo.userID, IBqlGuid>.FromCurrent>>>, Search<FSSetup.dfltSrvOrdType>>))]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  /// <summary>Service Order reference number</summary>
  [PXString]
  [PXUIField]
  [PXFormula(typeof (Default<MainAppointmentFilter.srvOrdType>))]
  [PXSelector(typeof (Search<FSServiceOrder.refNbr, Where2<Where<FSServiceOrder.srvOrdType, Equal<Current<MainAppointmentFilter.srvOrdType>>, Or<Current<MainAppointmentFilter.srvOrdType>, IsNull>>, And<FSServiceOrder.status, Equal<ListField.ServiceOrderStatus.open>>>, OrderBy<Desc<FSServiceOrder.refNbr>>>), DescriptionField = typeof (FSServiceOrder.docDesc))]
  public virtual string SORefNbr { get; set; }

  /// <summary>Appointment reference number</summary>
  [PXString]
  [PXUIField]
  [PXFormula(typeof (Default<MainAppointmentFilter.srvOrdType>))]
  [PXFieldDescription]
  public virtual string RefNbr { get; set; }

  /// <summary>Service Order ID</summary>
  [PXInt]
  public virtual int? SOID { get; set; }

  /// <summary>Service Order Status</summary>
  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [ListField.ServiceOrderStatus.List]
  [PXUnboundDefault(typeof (Search<FSServiceOrder.status, Where2<Where<FSServiceOrder.sOID, Equal<Current<MainAppointmentFilter.sOID>>, And<MainAppointmentFilter.sORefNbr, IsNull>>, Or<Where<FSServiceOrder.srvOrdType, Equal<Current<MainAppointmentFilter.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Current<MainAppointmentFilter.sORefNbr>>>>>>>))]
  public virtual string Status { get; set; }

  /// <summary>Service Order Description</summary>
  [PXString]
  [PXUIField(DisplayName = "Description", Required = true)]
  [PXUnboundDefault("")]
  public virtual string Description { get; set; }

  /// <summary>Customer ID</summary>
  [PXInt]
  [PXUIField]
  [PXUnboundDefault(typeof (Coalesce<Search<FSServiceOrder.customerID, Where2<Where<FSServiceOrder.sOID, Equal<Current<MainAppointmentFilter.sOID>>, And<MainAppointmentFilter.sORefNbr, IsNull>>, Or<Where<FSServiceOrder.srvOrdType, Equal<Current<MainAppointmentFilter.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Current<MainAppointmentFilter.sORefNbr>>>>>>>, Search<PX.Objects.AR.Customer.bAccountID, Where<PX.Objects.AR.Customer.bAccountID, Equal<CurrentValue<MainAppointmentFilter.customerID>>>>>))]
  [FSSelectorBusinessAccount_CU_PR_VC]
  public virtual int? CustomerID { get; set; }

  /// <summary>Customer Location ID</summary>
  [PXUnboundDefault(typeof (Coalesce<Search<FSServiceOrder.locationID, Where2<Where<FSServiceOrder.sOID, Equal<Current<MainAppointmentFilter.sOID>>, And<MainAppointmentFilter.sORefNbr, IsNull>>, Or<Where<FSServiceOrder.srvOrdType, Equal<Current<MainAppointmentFilter.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Current<MainAppointmentFilter.sORefNbr>>>>>>>, Search<PX.Objects.AR.Customer.defLocationID, Where<PX.Objects.AR.Customer.bAccountID, Equal<CurrentValue<MainAppointmentFilter.customerID>>>>>))]
  [PXUIField(DisplayName = "Customer Location")]
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<MainAppointmentFilter.customerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), DirtyRead = true)]
  public virtual int? LocationID { get; set; }

  /// <summary>Branch Location ID</summary>
  [PXUnboundDefault(typeof (Coalesce<Search<FSServiceOrder.branchLocationID, Where2<Where<FSServiceOrder.sOID, Equal<Current<MainAppointmentFilter.sOID>>, And<MainAppointmentFilter.sORefNbr, IsNull>>, Or<Where<FSServiceOrder.srvOrdType, Equal<Current<MainAppointmentFilter.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Current<MainAppointmentFilter.sORefNbr>>>>>>>, Search<FSxUserPreferences.dfltBranchLocationID, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>>>>))]
  [PXSelector(typeof (FSBranchLocation.branchLocationID), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  [PXUIField(DisplayName = "Branch Location")]
  public virtual int? BranchLocationID { get; set; }

  /// <summary>Contact ID</summary>
  [PXUIField(DisplayName = "Contact", Required = false)]
  [FSSelectorContact(typeof (MainAppointmentFilter.customerID))]
  [PXUnboundDefault(typeof (Coalesce<Search<FSServiceOrder.contactID, Where2<Where<FSServiceOrder.sOID, Equal<Current<MainAppointmentFilter.sOID>>, And<MainAppointmentFilter.sORefNbr, IsNull>>, Or<Where<FSServiceOrder.srvOrdType, Equal<Current<MainAppointmentFilter.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Current<MainAppointmentFilter.sORefNbr>>>>>>>, Search<PX.Objects.AR.Customer.defContactID, Where<PX.Objects.AR.Customer.bAccountID, Equal<CurrentValue<MainAppointmentFilter.customerID>>>>>))]
  public virtual int? ContactID { get; set; }

  /// <summary>Scheduled Start date and time</summary>
  [PXDateAndTime(UseTimeZone = true)]
  [PXUIField(DisplayName = "Scheduled Start", Required = true)]
  public virtual DateTime? ScheduledDateTimeBegin { get; set; }

  /// <summary>Scheduled Duration</summary>
  [PXTimeSpanLong]
  [PXUIField(DisplayName = "Duration", Required = true)]
  [PXUnboundDefault(typeof (Current<FSAppointment.scheduledDuration>))]
  public virtual int? Duration { get; set; }

  /// <summary>Long Description ("Other")</summary>
  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Details")]
  public virtual string LongDescr { get; set; }

  /// <summary>Employees for Service Order</summary>
  [PXString]
  public virtual string Resources { get; set; }

  /// <summary>Service Order Detail ID</summary>
  [PXInt]
  public virtual int? SODetID { get; set; }

  /// <summary>
  /// Specifies if the appointment to be creates in On Hold state
  /// </summary>
  [PXBool]
  public virtual bool? OnHold { get; set; }

  /// <summary>Reference number of an appointment to schedule</summary>
  [PXString]
  public virtual string InitialRefNbr { get; set; }

  /// <summary>Reference number of a SO to schedule</summary>
  [PXString]
  public virtual string InitialSORefNbr { get; set; }

  /// <summary>Customer ID to filter SOs with</summary>
  [PXString]
  [PXSelector(typeof (FSServiceOrder.customerID), SubstituteKey = typeof (FSServiceOrder.customerDisplayName))]
  public virtual string InitialCustomerID { get; set; }

  /// <summary>Specifies if App. Entry is to be opened</summary>
  [PXBool]
  public virtual bool? OpenEditor { get; set; }

  /// <summary>
  /// Set by server to true, if client needs to switch to "All Records" in order to show the search results
  /// </summary>
  [PXBool]
  public virtual bool? ResetFilters { get; set; }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MainAppointmentFilter.srvOrdType>
  {
  }

  public abstract class sORefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MainAppointmentFilter.sORefNbr>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MainAppointmentFilter.refNbr>
  {
  }

  public abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  MainAppointmentFilter.sOID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MainAppointmentFilter.status>
  {
    public abstract class Values : ListField.ServiceOrderStatus
    {
    }
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MainAppointmentFilter.description>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  MainAppointmentFilter.customerID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  MainAppointmentFilter.locationID>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MainAppointmentFilter.branchLocationID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  MainAppointmentFilter.contactID>
  {
  }

  public abstract class scheduledDateTimeBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    MainAppointmentFilter.scheduledDateTimeBegin>
  {
  }

  public abstract class duration : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  MainAppointmentFilter.duration>
  {
  }

  public abstract class details : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MainAppointmentFilter.details>
  {
  }

  public abstract class resources : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MainAppointmentFilter.resources>
  {
  }

  public abstract class sODetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  MainAppointmentFilter.sODetID>
  {
  }

  public abstract class onHold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  MainAppointmentFilter.onHold>
  {
  }

  public abstract class initialRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MainAppointmentFilter.initialRefNbr>
  {
  }

  public abstract class initialSORefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MainAppointmentFilter.initialSORefNbr>
  {
  }

  public abstract class initialCustomerID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MainAppointmentFilter.initialCustomerID>
  {
  }

  public abstract class openEditor : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  MainAppointmentFilter.openEditor>
  {
  }

  public abstract class resetFilters : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    MainAppointmentFilter.resetFilters>
  {
  }
}
