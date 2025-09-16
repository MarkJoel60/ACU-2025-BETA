// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.AppointmentInq
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.FS;

[TableAndChartDashboardType]
public class AppointmentInq : PXGraph<
#nullable disable
AppointmentInq>
{
  public PXFilter<AppointmentInq.AppointmentInqFilter> Filter;
  [PXHidden]
  public PXSelect<BAccountSelectorBase> DummyView_CustomerRecords;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoinGroupBy<FSAppointmentFSServiceOrder, CrossJoin<FSSetup, LeftJoin<FSAppointmentEmployee, On<FSAppointmentEmployee.appointmentID, Equal<FSAppointmentFSServiceOrder.appointmentID>>, LeftJoin<FSAppointmentResource, On<FSAppointmentResource.appointmentID, Equal<FSAppointmentFSServiceOrder.appointmentID>>, LeftJoin<FSCustomerBillingSetup, On<FSCustomerBillingSetup.customerID, Equal<FSAppointmentFSServiceOrder.billCustomerID>, And<Where2<Where<FSSetup.customerMultipleBillingOptions, Equal<True>, And<FSCustomerBillingSetup.srvOrdType, Equal<FSAppointmentFSServiceOrder.srvOrdType>>>, Or<Where<FSSetup.customerMultipleBillingOptions, Equal<False>, And<FSCustomerBillingSetup.srvOrdType, IsNull>>>>>>, LeftJoin<FSGeoZonePostalCode, On<FSGeoZonePostalCode.postalCode, Equal<FSAppointmentFSServiceOrder.postalCode>>, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSAppointmentFSServiceOrder.customerID>>>>>>>>, Where2<Where<Current<AppointmentInq.AppointmentInqFilter.branchID>, IsNull, Or<Current<AppointmentInq.AppointmentInqFilter.branchID>, Equal<FSAppointmentFSServiceOrder.branchID>>>, And2<Where<Current<AppointmentInq.AppointmentInqFilter.branchLocationID>, IsNull, Or<Current<AppointmentInq.AppointmentInqFilter.branchLocationID>, Equal<FSAppointmentFSServiceOrder.branchLocationID>>>, And2<Where<Current<AppointmentInq.AppointmentInqFilter.customerID>, IsNull, Or<Current<AppointmentInq.AppointmentInqFilter.customerID>, Equal<FSAppointmentFSServiceOrder.customerID>>>, And2<Where<Current<AppointmentInq.AppointmentInqFilter.customerLocationID>, IsNull, Or<Current<AppointmentInq.AppointmentInqFilter.customerLocationID>, Equal<FSAppointmentFSServiceOrder.locationID>>>, And2<Where<Current<AppointmentInq.AppointmentInqFilter.sORefNbr>, IsNull, Or<Current<AppointmentInq.AppointmentInqFilter.sORefNbr>, Equal<FSAppointmentFSServiceOrder.soRefNbr>>>, And2<Where<Current<AppointmentInq.AppointmentInqFilter.serviceContractID>, IsNull, Or<Where<Current<AppointmentInq.AppointmentInqFilter.serviceContractID>, Equal<FSAppointmentFSServiceOrder.serviceContractID>, Or<Current<AppointmentInq.AppointmentInqFilter.serviceContractID>, Equal<FSAppointmentFSServiceOrder.billServiceContractID>>>>>, And2<Where<Current<AppointmentInq.AppointmentInqFilter.scheduleID>, IsNull, Or<Current<AppointmentInq.AppointmentInqFilter.scheduleID>, Equal<FSAppointmentFSServiceOrder.scheduleID>>>, And2<Where<Current<AppointmentInq.AppointmentInqFilter.staffMemberID>, IsNull, Or<Current<AppointmentInq.AppointmentInqFilter.staffMemberID>, Equal<FSAppointmentEmployee.employeeID>>>, And2<Where<Current<AppointmentInq.AppointmentInqFilter.SMequipmentID>, IsNull, Or<Current<AppointmentInq.AppointmentInqFilter.SMequipmentID>, Equal<FSAppointmentResource.SMequipmentID>>>, And2<Where2<Where<Current<AppointmentInq.AppointmentInqFilter.fromScheduledDate>, IsNull, Or<FSAppointmentFSServiceOrder.scheduledDateTimeEnd, GreaterEqual<Current<AppointmentInq.AppointmentInqFilter.fromScheduledDate>>>>, And2<Where<Current<AppointmentInq.AppointmentInqFilter.toScheduledDate>, IsNull, Or<FSAppointmentFSServiceOrder.scheduledDateTimeBegin, LessEqual<Current<AppointmentInq.AppointmentInqFilter.toScheduledDate>>>>, And<Current<AppointmentInq.AppointmentInqFilter.fromScheduledDate>, LessEqual<Current<AppointmentInq.AppointmentInqFilter.toScheduledDate>>>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>>>>>>>>>, Aggregate<GroupBy<FSAppointmentFSServiceOrder.appointmentID, GroupBy<FSAppointmentFSServiceOrder.scheduledDateTimeBegin>>>, OrderBy<Asc<FSAppointmentFSServiceOrder.scheduledDateTimeBegin, Desc<FSAppointmentFSServiceOrder.srvOrdType, Desc<FSAppointmentFSServiceOrder.refNbr>>>>> Appointments;
  public PXCancel<AppointmentInq.AppointmentInqFilter> Cancel;
  public PXAction<AppointmentInq.AppointmentInqFilter> CreateNew;
  public PXAction<AppointmentInq.AppointmentInqFilter> EditDetail;

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Project", Visible = false, FieldClass = "PROJECT")]
  protected virtual void FSAppointmentFSServiceOrder_ProjectID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Task", Visible = false, FieldClass = "PROJECT")]
  protected virtual void FSAppointmentFSServiceOrder_DfltProjectTaskID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Branch", Visible = true)]
  protected virtual void FSAppointmentFSServiceOrder_BranchID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Scheduled End Date", Visible = false)]
  protected virtual void FSAppointmentFSServiceOrder_ScheduledDateTimeEnd_CacheAttached(
    PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Actual End Date", Visible = false)]
  protected virtual void FSAppointmentFSServiceOrder_ActualDateTimeEnd_CacheAttached(PXCache sender)
  {
  }

  public AppointmentInq()
  {
    ((PXSelectBase) this.Appointments).Cache.AllowDelete = false;
    ((PXSelectBase) this.Appointments).Cache.AllowInsert = false;
    ((PXSelectBase) this.Appointments).Cache.AllowUpdate = false;
    ((PXAction) this.CreateNew).SetEnabled(AppointmentEntry.IsReadyToBeUsed((PXGraph) this, ((PXGraph) this).Accessinfo.ScreenID));
  }

  public virtual bool IsDirty => false;

  [PXUIField]
  [PXInsertButton]
  protected virtual IEnumerable createNew(PXAdapter adapter)
  {
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Insert((FSAppointment) ((PXSelectBase) instance.AppointmentRecords).Cache.CreateInstance());
    ((PXSelectBase) instance.AppointmentRecords).Cache.IsDirty = false;
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
    throw requiredException;
  }

  [PXUIField]
  [PXEditDetailButton]
  protected virtual IEnumerable editDetail(PXAdapter adapter)
  {
    if (((PXSelectBase<FSAppointmentFSServiceOrder>) this.Appointments).Current == null)
      return (IEnumerable) ((PXSelectBase<AppointmentInq.AppointmentInqFilter>) this.Filter).Select(Array.Empty<object>());
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.refNbr>((object) ((PXSelectBase<FSAppointmentFSServiceOrder>) this.Appointments).Current.RefNbr, new object[1]
    {
      (object) ((PXSelectBase<FSAppointmentFSServiceOrder>) this.Appointments).Current.SrvOrdType
    }));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<AppointmentInq.AppointmentInqFilter, AppointmentInq.AppointmentInqFilter.fromScheduledDate> e)
  {
    if (e.Row == null)
      return;
    DateTime dateTime1 = ((PXGraph) this).Accessinfo.BusinessDate.Value;
    DateTime dateTime2 = ((PXGraph) this).Accessinfo.BusinessDate.Value;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AppointmentInq.AppointmentInqFilter, AppointmentInq.AppointmentInqFilter.fromScheduledDate>, AppointmentInq.AppointmentInqFilter, object>) e).NewValue = (object) new DateTime(dateTime2.Year, dateTime2.Month, 1);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<AppointmentInq.AppointmentInqFilter, AppointmentInq.AppointmentInqFilter.toScheduledDate> e)
  {
    if (e.Row == null)
      return;
    DateTime dateTime1 = ((PXGraph) this).Accessinfo.BusinessDate.Value;
    DateTime dateTime2 = ((PXGraph) this).Accessinfo.BusinessDate.Value;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AppointmentInq.AppointmentInqFilter, AppointmentInq.AppointmentInqFilter.toScheduledDate>, AppointmentInq.AppointmentInqFilter, object>) e).NewValue = (object) new DateTime(dateTime2.Year, dateTime2.Month, 1).AddMonths(1).AddDays(-1.0);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AppointmentInq.AppointmentInqFilter, AppointmentInq.AppointmentInqFilter.toScheduledDate>, AppointmentInq.AppointmentInqFilter, object>) e).NewValue = (object) new DateHandler((DateTime?) ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AppointmentInq.AppointmentInqFilter, AppointmentInq.AppointmentInqFilter.toScheduledDate>, AppointmentInq.AppointmentInqFilter, object>) e).NewValue).EndOfDay();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<AppointmentInq.AppointmentInqFilter, AppointmentInq.AppointmentInqFilter.staffMemberID> e)
  {
    if (e.Row == null)
      return;
    int? currentEmployeeId = SharedFunctions.GetCurrentEmployeeID(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<AppointmentInq.AppointmentInqFilter, AppointmentInq.AppointmentInqFilter.staffMemberID>>) e).Cache);
    if (!currentEmployeeId.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AppointmentInq.AppointmentInqFilter, AppointmentInq.AppointmentInqFilter.staffMemberID>, AppointmentInq.AppointmentInqFilter, object>) e).NewValue = (object) currentEmployeeId;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AppointmentInq.AppointmentInqFilter, AppointmentInq.AppointmentInqFilter.staffMemberID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<AppointmentInq.AppointmentInqFilter, AppointmentInq.AppointmentInqFilter.toScheduledDate> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<AppointmentInq.AppointmentInqFilter, AppointmentInq.AppointmentInqFilter.toScheduledDate>>) e).NewValue == null)
      return;
    if (((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<AppointmentInq.AppointmentInqFilter, AppointmentInq.AppointmentInqFilter.toScheduledDate>>) e).NewValue.GetType().Equals(typeof (string)))
      ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<AppointmentInq.AppointmentInqFilter, AppointmentInq.AppointmentInqFilter.toScheduledDate>>) e).NewValue = (object) new DateHandler(DateTime.Parse(((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<AppointmentInq.AppointmentInqFilter, AppointmentInq.AppointmentInqFilter.toScheduledDate>>) e).NewValue.ToString())).EndOfDay();
    else
      ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<AppointmentInq.AppointmentInqFilter, AppointmentInq.AppointmentInqFilter.toScheduledDate>>) e).NewValue = (object) new DateHandler((DateTime?) ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<AppointmentInq.AppointmentInqFilter, AppointmentInq.AppointmentInqFilter.toScheduledDate>>) e).NewValue).EndOfDay();
  }

  protected virtual void _(
    PX.Data.Events.RowSelecting<AppointmentInq.AppointmentInqFilter> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<AppointmentInq.AppointmentInqFilter> e)
  {
    if (e.Row == null || !((PXGraph) this).IsMobile)
      return;
    AppointmentInq.AppointmentInqFilter row = e.Row;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AppointmentInq.AppointmentInqFilter>>) e).Cache.SetValue<AppointmentInq.AppointmentInqFilter.toScheduledDate>((object) row, (object) new DateHandler(row.ToScheduledDate).EndOfDay());
  }

  protected virtual void _(
    PX.Data.Events.RowInserting<AppointmentInq.AppointmentInqFilter> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowInserted<AppointmentInq.AppointmentInqFilter> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowUpdating<AppointmentInq.AppointmentInqFilter> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowUpdated<AppointmentInq.AppointmentInqFilter> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowDeleting<AppointmentInq.AppointmentInqFilter> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowDeleted<AppointmentInq.AppointmentInqFilter> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowPersisting<AppointmentInq.AppointmentInqFilter> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowPersisted<AppointmentInq.AppointmentInqFilter> e)
  {
  }

  [Serializable]
  public class AppointmentInqFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [Branch(null, null, true, true, true)]
    [PXRestrictor(typeof (Where<True, Equal<True>>), "Branch is inactive.", new System.Type[] {}, ReplaceInherited = true)]
    public virtual int? BranchID { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "Branch Location")]
    [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<AppointmentInq.AppointmentInqFilter.branchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
    [PXFormula(typeof (Default<AppointmentInq.AppointmentInqFilter.branchID>))]
    public virtual int? BranchLocationID { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "Customer")]
    [FSSelectorCustomer]
    public virtual int? CustomerID { get; set; }

    [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<AppointmentInq.AppointmentInqFilter.customerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Location", DirtyRead = true)]
    [PXFormula(typeof (Default<AppointmentInq.AppointmentInqFilter.customerID>))]
    public virtual int? CustomerLocationID { get; set; }

    [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
    [PXUIField]
    [PXSelector(typeof (Search3<FSServiceOrder.refNbr, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<FSServiceOrder.locationID>>>>, OrderBy<Desc<FSServiceOrder.refNbr, Asc<FSServiceOrder.srvOrdType>>>>), new System.Type[] {typeof (FSServiceOrder.refNbr), typeof (FSServiceOrder.srvOrdType), typeof (PX.Objects.AR.Customer.acctCD), typeof (PX.Objects.AR.Customer.acctName), typeof (PX.Objects.CR.Location.locationCD), typeof (FSServiceOrder.docDesc), typeof (FSServiceOrder.status)})]
    public virtual string SORefNbr { get; set; }

    [PXInt]
    [PXSelector(typeof (Search<FSServiceContract.serviceContractID, Where<FSServiceContract.customerID, Equal<Current<AppointmentInq.AppointmentInqFilter.customerID>>>>), SubstituteKey = typeof (FSServiceContract.refNbr))]
    [PXUIField(DisplayName = "Service Contract ID", FieldClass = "FSCONTRACT")]
    [PXFormula(typeof (Default<AppointmentInq.AppointmentInqFilter.customerID>))]
    public virtual int? ServiceContractID { get; set; }

    [PXInt]
    [PXSelector(typeof (Search<FSSchedule.scheduleID, Where<FSSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>, And<FSSchedule.entityID, Equal<Current<AppointmentInq.AppointmentInqFilter.serviceContractID>>>>, OrderBy<Desc<FSSchedule.refNbr>>>), SubstituteKey = typeof (FSSchedule.refNbr))]
    [PXUIField(DisplayName = "Schedule ID")]
    [PXFormula(typeof (Default<AppointmentInq.AppointmentInqFilter.serviceContractID>))]
    public virtual int? ScheduleID { get; set; }

    [PXInt]
    [FSSelector_StaffMember_ServiceOrderProjectID]
    [PXUIField(DisplayName = "Staff Member", TabOrder = 0)]
    public virtual int? StaffMemberID { get; set; }

    [PXInt]
    [PXSelector(typeof (Search<FSEquipment.SMequipmentID, Where<FSEquipment.resourceEquipment, Equal<True>>>), SubstituteKey = typeof (FSEquipment.refNbr))]
    [PXUIField(DisplayName = "Resource Equipment")]
    public virtual int? SMEquipmentID { get; set; }

    [PXDateAndTime(UseTimeZone = true)]
    [PXUIField(DisplayName = "From Scheduled Date")]
    public virtual DateTime? FromScheduledDate { get; set; }

    [PXDateAndTime(UseTimeZone = true)]
    [PXUIField(DisplayName = "To Scheduled Date")]
    public virtual DateTime? ToScheduledDate { get; set; }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AppointmentInq.AppointmentInqFilter.branchID>
    {
    }

    public abstract class branchLocationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AppointmentInq.AppointmentInqFilter.branchLocationID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AppointmentInq.AppointmentInqFilter.customerID>
    {
    }

    public abstract class customerLocationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AppointmentInq.AppointmentInqFilter.customerLocationID>
    {
    }

    public abstract class sORefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AppointmentInq.AppointmentInqFilter.sORefNbr>
    {
    }

    public abstract class serviceContractID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AppointmentInq.AppointmentInqFilter.serviceContractID>
    {
    }

    public abstract class scheduleID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AppointmentInq.AppointmentInqFilter.scheduleID>
    {
    }

    public abstract class staffMemberID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AppointmentInq.AppointmentInqFilter.staffMemberID>
    {
    }

    public abstract class SMequipmentID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AppointmentInq.AppointmentInqFilter.SMequipmentID>
    {
    }

    public abstract class fromScheduledDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      AppointmentInq.AppointmentInqFilter.fromScheduledDate>
    {
    }

    public abstract class toScheduledDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      AppointmentInq.AppointmentInqFilter.toScheduledDate>
    {
    }
  }
}
