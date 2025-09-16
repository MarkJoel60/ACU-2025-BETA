// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteDocumentMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.DependencyInjection;
using PX.LicensePolicy;
using PX.Objects.CR;
using PX.Objects.EP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.FS;

public class RouteDocumentMaint : 
  PXGraph<RouteDocumentMaint, FSRouteDocument>,
  IGraphWithInitialization
{
  public AppointmentEntry globalAppointmentGraph;
  private bool needAppointmentDateUpdate;
  private bool needAppointmentTimeBeginUpdate;
  private bool vehicleChanged;
  private int? oldDriverID;
  private int? oldAdditionalDriverID;
  private int minutesToAdd;
  public bool AutomaticallyUncloseRoute;
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> BAccount;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Contact> Contact;
  [PXHidden]
  public PXSelect<FSServiceOrder> ServiceOrder;
  [PXHidden]
  public PXSelect<FSAppointment> Appointment;
  public PXSetup<FSRouteSetup> RouteSetupRecord;
  public PXSelect<FSRouteDocument> RouteRecords;
  public PXSelect<FSRouteDocument, Where<FSRouteDocument.routeDocumentID, Equal<Current<FSRouteDocument.routeDocumentID>>>> RouteSelected;
  public PXSelect<FSAppointmentInRoute, Where<FSAppointmentInRoute.routeDocumentID, Equal<Current<FSRouteDocument.routeDocumentID>>>, OrderBy<Asc<FSAppointment.routePosition>>> AppointmentsInRoute;
  public PXSelect<FSAppointmentInRoute, Where<FSAppointmentInRoute.routeDocumentID, Equal<Current<FSRouteDocument.routeDocumentID>>, And<FSAppointment.completed, Equal<False>, And<FSAppointment.canceled, NotEqual<False>, And<FSAppointment.closed, NotEqual<False>>>>>, OrderBy<Asc<FSAppointment.routePosition>>> AppointmentsMobileValidation;
  public SharedClasses.RouteSelected_view VehicleRouteSelected;
  public SharedClasses.RouteSelected_view DriverRouteSelected;
  public VehicleSelectionHelper.VehicleRecords_View VehicleRecords;
  public DriverSelectionHelper.DriverRecords_View DriverRecords;
  [PXViewName("Answers")]
  public CRAttributeList<FSRouteDocument> Answers;
  public PXFilter<VehicleSelectionFilter> VehicleFilter;
  public PXFilter<DriverSelectionFilter> DriverFilter;
  public PXAction<FSRouteDocument> openVehicleSelector;
  public PXAction<FSRouteDocument> openDriverSelector;
  public RouteAppointmentAssignmentHelper.RouteRecords_View RouteAppAssignmentRecords;
  public PXFilter<RouteAppointmentAssignmentFilter> RouteAppAssignmentFilter;
  public PXAction<FSRouteDocument> reassignAppointment;
  public PXAction<FSRouteDocument> selectCurrentRoute;
  public PXFilter<RouteAppointmentAssignmentFilter> RouteAppUnassignmentFilter;
  public PXAction<FSRouteDocument> unassignAppointment;
  public PXAction<FSRouteDocument> openAppointment;
  [PXFilterable(new System.Type[] {})]
  public PXFilter<SrvOrderTypeRouteAux> ServiceOrderTypeSelector;
  public PXSelect<FSSrvOrdType, Where<FSSrvOrdType.behavior, Equal<ListField.ServiceOrderTypeBehavior.routeAppointment>>> ServiceOrderTypeRecords;
  public PXAction<FSRouteDocument> addAppointment;
  public PXAction<FSRouteDocument> up;
  public PXAction<FSRouteDocument> down;
  public PXDBAction<FSRouteDocument> openCustomerLocation;
  public PXAction<FSRouteDocument> startRoute;
  public PXAction<FSRouteDocument> completeRoute;
  public PXAction<FSRouteDocument> cancelRoute;
  public PXAction<FSRouteDocument> reopenRoute;
  public PXAction<FSRouteDocument> uncloseRoute;
  public PXAction<FSRouteDocument> DeleteRoute;
  public PXAction<FSRouteDocument> openRoutesOnMap;
  public PXAction<FSRouteDocument> openDriverCalendar;
  public PXAction<FSRouteDocument> calculateRouteStats;
  public PXAction<FSRouteDocument> OpenRouteSchedule;
  public PXAction<FSRouteDocument> viewStartGPSOnMap;
  public PXAction<FSRouteDocument> viewCompleteGPSOnMap;

  public virtual bool IsReadyToBeUsed(PXGraph graph)
  {
    return PXSelectBase<FSRouteSetup, PXSelect<FSRouteSetup, Where<FSRouteSetup.routeNumberingID, IsNotNull>>.Config>.Select(graph, Array.Empty<object>()).Count > 0;
  }

  public RouteDocumentMaint()
  {
    PXUIFieldAttribute.SetDisplayName<FSServiceOrder.locationID>(((PXSelectBase) this.ServiceOrder).Cache, "Customer Location");
    PXUIFieldAttribute.SetDisplayName<FSAppointment.estimatedDurationTotal>(((PXSelectBase) this.Appointment).Cache, "Estimated Duration");
    PXGraph.FieldUpdatingEvents fieldUpdating1 = ((PXGraph) this).FieldUpdating;
    System.Type type1 = typeof (FSRouteDocument);
    string str1 = typeof (FSRouteDocument.timeBegin).Name + "_Time";
    RouteDocumentMaint routeDocumentMaint1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating1 = new PXFieldUpdating((object) routeDocumentMaint1, __vmethodptr(routeDocumentMaint1, FSRouteDocument_TimeBegin_FieldUpdating));
    fieldUpdating1.AddHandler(type1, str1, pxFieldUpdating1);
    PXGraph.FieldUpdatingEvents fieldUpdating2 = ((PXGraph) this).FieldUpdating;
    System.Type type2 = typeof (FSRouteDocument);
    string str2 = typeof (FSRouteDocument.actualStartTime).Name + "_Time";
    RouteDocumentMaint routeDocumentMaint2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating2 = new PXFieldUpdating((object) routeDocumentMaint2, __vmethodptr(routeDocumentMaint2, FSRouteDocument_ActualStartTime_FieldUpdating));
    fieldUpdating2.AddHandler(type2, str2, pxFieldUpdating2);
    ((PXSelectBase) this.AppointmentsInRoute).Cache.AllowUpdate = false;
    this.globalAppointmentGraph = PXGraph.CreateInstance<AppointmentEntry>();
  }

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits == null)
      return;
    ((PXGraph) this).OnBeforeCommit += this._licenseLimits.GetCheckerDelegate<FSRouteDocument>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (FSAppointment), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[1]
      {
        (PXDataFieldValue) new PXDataFieldValue<FSAppointment.routeDocumentID>((object) (int?) ((PXSelectBase<FSRouteDocument>) ((RouteDocumentMaint) graph).RouteRecords).Current?.RouteDocumentID)
      }))
    });
  }

  [PXUIField(DisplayName = "Total Driving Duration [*]", Enabled = false)]
  [PXDBTimeSpanLong]
  protected virtual void FSRouteDocument_TotalDuration_CacheAttached(PXCache sender)
  {
  }

  [PXDBString]
  [PXUIField(DisplayName = "Total Distance [*]", Enabled = false)]
  protected virtual void FSRouteDocument_TotalDistanceFriendly_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "Total Route Duration [*]", Enabled = false)]
  [PXDBTimeSpanLong]
  protected virtual void FSRouteDocument_TotalTravelTime_CacheAttached(PXCache sender)
  {
  }

  protected virtual IEnumerable vehicleRecords()
  {
    return ((PXAction) this.openVehicleSelector).GetEnabled() ? VehicleSelectionHelper.VehicleRecordsDelegate((PXGraph) this, this.VehicleRouteSelected, this.VehicleFilter) : (IEnumerable) null;
  }

  [PXButton]
  [PXUIField]
  public virtual void OpenVehicleSelector()
  {
    if (((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current == null)
      return;
    ((PXSelectBase<VehicleSelectionFilter>) this.VehicleFilter).Current.RouteDocumentID = ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.RouteDocumentID;
    ((PXSelectBase<FSRouteDocument>) this.VehicleRouteSelected).Current = ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current;
    PXResultset<FSRoute>.op_Implicit(PXSelectBase<FSRoute, PXSelectJoin<FSRoute, InnerJoin<FSRouteDocument, On<FSRoute.routeID, Equal<FSRouteDocument.routeID>>>, Where<FSRouteDocument.routeDocumentID, Equal<Required<FSRouteDocument.routeDocumentID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.RouteDocumentID
    }));
    if (((PXSelectBase<FSRouteDocument>) this.VehicleRouteSelected).AskExt() != 1 || ((PXSelectBase<FSVehicle>) this.VehicleRecords).Current == null)
      return;
    int? vehicleId = ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.VehicleID;
    int? smEquipmentId = ((PXSelectBase<FSVehicle>) this.VehicleRecords).Current.SMEquipmentID;
    if (vehicleId.GetValueOrDefault() == smEquipmentId.GetValueOrDefault() & vehicleId.HasValue == smEquipmentId.HasValue)
      return;
    ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.VehicleID = ((PXSelectBase<FSVehicle>) this.VehicleRecords).Current.SMEquipmentID;
    ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Update(((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current);
  }

  protected virtual IEnumerable driverRecords()
  {
    return DriverSelectionHelper.DriverRecordsDelegate((PXGraph) this, this.DriverRouteSelected, this.DriverFilter);
  }

  [PXButton]
  [PXUIField]
  public virtual void OpenDriverSelector()
  {
    if (((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current == null)
      return;
    ((PXSelectBase<FSRouteDocument>) this.DriverRouteSelected).Current = ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current;
    if (((PXSelectBase<FSRouteDocument>) this.DriverRouteSelected).AskExt() != 1 || ((PXSelectBase<EPEmployee>) this.DriverRecords).Current == null)
      return;
    int? driverId = ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.DriverID;
    int? baccountId = ((PXSelectBase<EPEmployee>) this.DriverRecords).Current.BAccountID;
    if (driverId.GetValueOrDefault() == baccountId.GetValueOrDefault() & driverId.HasValue == baccountId.HasValue)
      return;
    ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current = PXResultset<FSRouteDocument>.op_Implicit(((PXSelectBase<FSRouteDocument>) this.RouteRecords).Search<FSRouteDocument.routeDocumentID>((object) ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.RouteDocumentID, Array.Empty<object>()));
    ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.DriverID = ((PXSelectBase<EPEmployee>) this.DriverRecords).Current.BAccountID;
    ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Update(((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current);
  }

  [PXUIField]
  [PXButton]
  public virtual void ReassignAppointment()
  {
    if (((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current == null || ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current == null)
      return;
    PXResult<FSAppointment, FSServiceOrder, FSAddress> pxResult = (PXResult<FSAppointment, FSServiceOrder, FSAddress>) PXResultset<FSAppointment>.op_Implicit(PXSelectBase<FSAppointment, PXSelectReadonly2<FSAppointment, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, InnerJoin<FSAddress, On<FSAddress.addressID, Equal<FSServiceOrder.serviceOrderAddressID>>>>, Where<FSAppointment.appointmentID, Equal<Required<FSAppointment.appointmentID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current.AppointmentID
    }));
    FSAppointment fsAppointment = PXResult<FSAppointment, FSServiceOrder, FSAddress>.op_Implicit(pxResult);
    FSServiceOrder fsServiceOrder = PXResult<FSAppointment, FSServiceOrder, FSAddress>.op_Implicit(pxResult);
    FSAddress fsAddress = PXResult<FSAppointment, FSServiceOrder, FSAddress>.op_Implicit(pxResult);
    ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.AppointmentID = ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current.AppointmentID;
    ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.SrvOrdType = ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current.SrvOrdType;
    ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.RefNbr = ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current.RefNbr;
    ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.RouteDocumentID = ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current.RouteDocumentID;
    ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.RouteDate = ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.Date;
    ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.AppRefNbr = fsAppointment.RefNbr;
    ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.AppSrvOrdType = fsAppointment.SrvOrdType;
    ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.EstimatedDurationTotal = fsAppointment.EstimatedDurationTotal;
    ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.ScheduledDateTimeBegin = fsAppointment.ScheduledDateTimeBegin;
    ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.AddressLine1 = fsAddress.AddressLine1;
    ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.AddressLine2 = fsAddress.AddressLine2;
    ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.City = fsAddress.City;
    ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.State = fsAddress.State;
    ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.CustomerID = fsServiceOrder.CustomerID;
    ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.LocationID = fsServiceOrder.LocationID;
    ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).AskExt();
  }

  protected virtual IEnumerable routeAppAssignmentRecords()
  {
    return new RouteAppointmentAssignmentHelper().RouteRecordsDelegate(this.RouteAppAssignmentFilter, (PXSelectBase<FSRouteDocument>) new RouteAppointmentAssignmentHelper.RouteRecords_View((PXGraph) this));
  }

  [PXUIField]
  [PXButton]
  public virtual void SelectCurrentRoute()
  {
    if (((PXSelectBase<FSRouteDocument>) this.RouteAppAssignmentRecords).Current == null || ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current == null)
      return;
    RouteAppointmentAssignmentHelper.ReassignAppointmentToRoute(((PXSelectBase<FSRouteDocument>) this.RouteAppAssignmentRecords).Current, ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.RefNbr, ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.SrvOrdType);
    ((PXGraph) this).Actions.PressCancel();
  }

  [PXUIField]
  [PXButton]
  public virtual void UnassignAppointment()
  {
    if (((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current == null)
      return;
    ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.SrvOrdType = ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current.SrvOrdType;
    ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.RefNbr = ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current.RefNbr;
    if (6 != ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppUnassignmentFilter).Ask("Confirm Unassign Appointment", "The selected appointment will be deleted from all the records. Are you sure?", (MessageButtons) 4))
      return;
    RouteAppointmentAssignmentHelper.DeleteAppointmentRoute(((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.RefNbr, ((PXSelectBase<RouteAppointmentAssignmentFilter>) this.RouteAppAssignmentFilter).Current.SrvOrdType);
    ((PXGraph) this).Actions.PressCancel();
  }

  [PXButton]
  [PXUIField]
  public virtual void OpenAppointment()
  {
    if (((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current != null && ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current.RouteDocumentID.HasValue)
    {
      AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
      ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.appointmentID>((object) ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current.AppointmentID, new object[1]
      {
        (object) ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current.SrvOrdType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  [PXButton]
  [PXUIField]
  public virtual void AddAppointment()
  {
    if (((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current == null || !((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.RouteDocumentID.HasValue)
      return;
    if (((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeRecords).Select(Array.Empty<object>()).Count == 0)
      throw new PXException("A new appointment cannot be created because a service order type of the Route behavior does not exist in the system. Create it on the Service Order Types (FS202300) form.");
    if (!((PXGraph) this).IsMobile)
    {
      if (((PXSelectBase<SrvOrderTypeRouteAux>) this.ServiceOrderTypeSelector).AskExt() != 1 || ((PXSelectBase<SrvOrderTypeRouteAux>) this.ServiceOrderTypeSelector).Current == null || ((PXSelectBase<SrvOrderTypeRouteAux>) this.ServiceOrderTypeSelector).Current.SrvOrdType == null)
        return;
      this.openAppointmentScreen(((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current, ((PXSelectBase<SrvOrderTypeRouteAux>) this.ServiceOrderTypeSelector).Current.SrvOrdType);
    }
    else
      this.openAppointmentScreen(((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current, (string) null);
  }

  [PXButton]
  [PXUIField]
  public virtual void Up()
  {
    if (((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current != null)
    {
      int? nullable = ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current.RouteDocumentID;
      if (nullable.HasValue)
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          FSRouteDocument current = ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current;
          int? routePosition = ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current.RoutePosition;
          nullable = ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current.RoutePosition;
          int? positionTo = nullable.HasValue ? new int?(nullable.GetValueOrDefault() - 1) : new int?();
          this.MoveAppointmentInRoute((PXGraph) this, current, routePosition, positionTo);
          transactionScope.Complete();
        }
      }
    }
    ((PXGraph) this).Actions.PressCancel();
    ((PXSelectBase<FSRouteDocument>) this.RouteSelected).Current.MustRecalculateStats = new bool?(true);
    ((PXSelectBase<FSRouteDocument>) this.RouteSelected).Update(((PXSelectBase<FSRouteDocument>) this.RouteSelected).Current);
  }

  [PXButton]
  [PXUIField]
  public virtual void Down()
  {
    if (((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current != null)
    {
      int? nullable = ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current.RouteDocumentID;
      if (nullable.HasValue)
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          FSRouteDocument current = ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current;
          int? routePosition = ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current.RoutePosition;
          nullable = ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current.RoutePosition;
          int? positionTo = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + 1) : new int?();
          this.MoveAppointmentInRoute((PXGraph) this, current, routePosition, positionTo);
          transactionScope.Complete();
        }
      }
    }
    ((PXGraph) this).Actions.PressCancel();
    ((PXSelectBase<FSRouteDocument>) this.RouteSelected).Current.MustRecalculateStats = new bool?(true);
    ((PXSelectBase<FSRouteDocument>) this.RouteSelected).Update(((PXSelectBase<FSRouteDocument>) this.RouteSelected).Current);
  }

  [PXUIField]
  public virtual void OpenCustomerLocation()
  {
    LocationHelper.OpenCustomerLocation((PXGraph) this, ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current.SOID);
  }

  [PXUIField]
  [PXButton(Category = "Processing")]
  public virtual IEnumerable StartRoute(PXAdapter adapter)
  {
    FSRouteDocument current = ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current;
    if (((PXSelectBase) this.RouteRecords).Cache.GetStatus((object) current) != 2)
    {
      string empty = string.Empty;
      if (!this.IsThisStatusTransitionAllowed(current, "P", ref empty))
        throw new PXException(empty);
      if (!this.AreThereAnyAppointmentsInThisRoute("P", ref empty))
        throw new PXException(empty);
      int? nullable1 = current.DriverID;
      if (nullable1.HasValue)
      {
        nullable1 = current.VehicleID;
        if (nullable1.HasValue)
          goto label_14;
      }
      string str = string.Empty;
      nullable1 = current.DriverID;
      if (!nullable1.HasValue)
      {
        str = "Driver ID";
        nullable1 = current.VehicleID;
        if (!nullable1.HasValue)
          str += " and Vehicle ID";
      }
      else
      {
        nullable1 = current.VehicleID;
        if (!nullable1.HasValue)
          str = "Vehicle ID";
      }
      if (((PXSelectBase<FSRouteDocument>) this.RouteRecords).Ask(PXMessages.LocalizeFormatNoPrefix("This route is missing a {0}. Do you want to proceed anyway?", new object[1]
      {
        (object) str
      }), (MessageButtons) 4) == 7)
        return adapter.Get();
label_14:
      DateTime? nullable2;
      ref DateTime? local = ref nullable2;
      int year = ((PXGraph) this).Accessinfo.BusinessDate.Value.Year;
      int month = ((PXGraph) this).Accessinfo.BusinessDate.Value.Month;
      DateTime dateTime1 = ((PXGraph) this).Accessinfo.BusinessDate.Value;
      int day = dateTime1.Day;
      dateTime1 = current.MemBusinessDateTime.Value;
      int hour = dateTime1.Hour;
      dateTime1 = current.MemBusinessDateTime.Value;
      int minute = dateTime1.Minute;
      dateTime1 = current.MemBusinessDateTime.Value;
      int second = dateTime1.Second;
      DateTime dateTime2 = new DateTime(year, month, day, hour, minute, second);
      local = new DateTime?(dateTime2);
      ((PXSelectBase) this.RouteRecords).Cache.SetValueExt<FSRouteDocument.actualStartTime>((object) current, (object) nullable2);
      current.Status = "P";
      ((PXSelectBase) this.RouteRecords).Cache.SetStatus((object) current, (PXEntryStatus) 1);
      if (((PXGraph) this).IsMobile && ((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Current != null && ((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Current.TrackRouteLocation.GetValueOrDefault() && !string.IsNullOrEmpty(current.GPSLatitudeLongitude))
      {
        string[] strArray = current.GPSLatitudeLongitude.Split(':');
        current.GPSLatitudeStart = new Decimal?(Decimal.Parse(strArray[0]));
        current.GPSLongitudeStart = new Decimal?(Decimal.Parse(strArray[1]));
      }
      ((PXAction) this.Save).Press();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Processing")]
  public virtual IEnumerable CompleteRoute(PXAdapter adapter)
  {
    FSRouteDocument current = ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current;
    if (((PXSelectBase) this.RouteRecords).Cache.GetStatus((object) current) != 2)
    {
      if (((PXGraph) this).IsMobile && ((IQueryable<PXResult<FSAppointmentInRoute>>) ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsMobileValidation).Select(Array.Empty<object>())).Count<PXResult<FSAppointmentInRoute>>() > 0)
        throw new PXException("The route cannot be completed because some of its appointments have to be completed, canceled, or closed.");
      string empty = string.Empty;
      if (!this.IsThisStatusTransitionAllowed(current, "C", ref empty))
        throw new PXException(empty);
      if (!this.AreThereAnyAppointmentsInThisRoute("C", ref empty))
        throw new PXException(empty);
      if (!this.CompleteAppointmentsInRoute(ref empty))
        throw new PXException(empty);
      DateTime? nullable1 = current.TimeBegin;
      if (!nullable1.HasValue)
      {
        ((PXSelectBase) this.RouteRecords).Cache.SetStatus((object) current, (PXEntryStatus) 1);
        PXDefaultAttribute.SetPersistingCheck<FSRouteDocument.timeBegin>(((PXSelectBase) this.RouteRecords).Cache, (object) current, (PXPersistingCheck) 1);
        ((PXAction) this.Save).Press();
      }
      else
      {
        nullable1 = ((PXGraph) this).Accessinfo.BusinessDate;
        DateTime? nullable2 = new DateTime?(nullable1.Value);
        nullable1 = current.MemBusinessDateTime;
        DateTime? nullable3 = new DateTime?(nullable1.Value);
        DateTime? nullable4 = PXDBDateAndTimeAttribute.CombineDateTime(nullable2, nullable3);
        if (((PXGraph) this).IsMobile)
        {
          nullable1 = current.ActualStartTime;
          DateTime? nullable5 = nullable4;
          if ((nullable1.HasValue & nullable5.HasValue ? (nullable1.GetValueOrDefault() < nullable5.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            ((PXSelectBase) this.RouteRecords).Cache.SetValueExt<FSRouteDocument.actualEndTime>((object) current, (object) nullable4);
        }
        if (((PXGraph) this).IsMobile && ((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Current != null && ((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Current.TrackRouteLocation.GetValueOrDefault() && !string.IsNullOrEmpty(current.GPSLatitudeLongitude))
        {
          string[] strArray = current.GPSLatitudeLongitude.Split(':');
          current.GPSLatitudeComplete = new Decimal?(Decimal.Parse(strArray[0]));
          current.GPSLongitudeComplete = new Decimal?(Decimal.Parse(strArray[1]));
        }
        current.Status = "C";
        ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Update(current);
        ((PXAction) this.Save).Press();
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Processing")]
  public virtual IEnumerable CancelRoute(PXAdapter adapter)
  {
    FSRouteDocument current = ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current;
    if (((PXSelectBase) this.RouteRecords).Cache.GetStatus((object) current) != 2)
    {
      string empty = string.Empty;
      if (!this.IsThisStatusTransitionAllowed(current, "X", ref empty))
        throw new PXException(empty);
      if (!this.CancelAppointmentsInRoute(ref empty))
        throw new PXException(empty);
      ((PXAction) this.Cancel).Press();
      current.Status = "X";
      ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Update(current);
      ((PXAction) this.Save).Press();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Corrections")]
  public virtual IEnumerable ReopenRoute(PXAdapter adapter)
  {
    FSRouteDocument current = ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current;
    if (((PXSelectBase) this.RouteRecords).Cache.GetStatus((object) current) != 2)
    {
      string empty = string.Empty;
      if (!this.IsThisStatusTransitionAllowed(current, "O", ref empty))
        throw new PXException(empty);
      ((PXSelectBase) this.RouteRecords).Cache.AllowUpdate = true;
      current.Status = "O";
      ((PXSelectBase) this.RouteRecords).Cache.SetStatus((object) current, (PXEntryStatus) 1);
      ((PXAction) this.Save).Press();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Corrections")]
  public virtual IEnumerable UncloseRoute(PXAdapter adapter)
  {
    FSRouteDocument current = ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current;
    if (current != null && (this.AutomaticallyUncloseRoute || 6 == ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Ask("Confirm Route Unclosing", "The current route will be unclosed. Do you want to proceed?", (MessageButtons) 4)))
      this.UncloseRouteAndSave(((PXSelectBase) this.RouteRecords).Cache, current);
    return adapter.Get();
  }

  [PXButton(Category = "Corrections", ImageSet = "main", ImageKey = "Remove")]
  [PXUIField]
  public virtual void deleteRoute()
  {
    FSRouteDocument current = ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current;
    if (current == null || 6 != ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Ask("Confirm Delete Current Route", "The Appointments and Service Orders will be also deleted with this action. Are you sure?", (MessageButtons) 4))
      return;
    if (PXSelectBase<FSAppointment, PXSelectJoin<FSAppointment, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>>, Where2<Where<FSAppointment.completed, Equal<True>, Or<FSAppointment.closed, Equal<True>, Or<FSServiceOrder.completed, Equal<True>, Or<FSServiceOrder.closed, Equal<True>>>>>, And<FSAppointment.routeDocumentID, Equal<Required<FSAppointment.routeDocumentID>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) current.RouteDocumentID
    }).Count > 0)
      throw new PXException("This record cannot be deleted because at least one appointment or service order of the Completed or Closed status is related to the route execution.");
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
      foreach (PXResult<FSAppointmentInRoute> pxResult in ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Select(Array.Empty<object>()))
      {
        FSAppointment fsAppointment = (FSAppointment) PXResult<FSAppointmentInRoute>.op_Implicit(pxResult);
        ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.appointmentID>((object) fsAppointment.AppointmentID, new object[1]
        {
          (object) fsAppointment.SrvOrdType
        }));
        instance.NeedRecalculateRouteStats = false;
        instance.CalculateGoogleStats = false;
        instance.AvoidCalculateRouteStats = true;
        ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Delete(fsAppointment);
        ((PXAction) instance.Save).Press();
      }
      ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Delete(current);
      ((PXAction) this.Save).Press();
      transactionScope.Complete();
    }
  }

  [PXButton(Category = "Other", DisplayOnMainToolbar = true)]
  [PXUIField]
  public virtual void OpenRoutesOnMap()
  {
    throw new PXRedirectToBoardRequiredException("Pages/fs/GoogleMaps/Routes/FS300900.aspx", new KeyValuePair<string, string>[3]
    {
      new KeyValuePair<string, string>("RefNbr", ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.RefNbr),
      new KeyValuePair<string, string>("Date", ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.Date.Value.ToString()),
      new KeyValuePair<string, string>("BranchID", ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.BranchID.Value.ToString())
    });
  }

  [PXButton(Category = "Other", DisplayOnMainToolbar = true)]
  [PXUIField]
  public virtual void OpenDriverCalendar()
  {
    throw new PXRedirectToBoardRequiredException("pages/fs/calendars/SingleEmpDispatch/FS300400.aspx", new KeyValuePair<string, string>[2]
    {
      new KeyValuePair<string, string>("EmployeeID", ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.DriverID.ToString()),
      new KeyValuePair<string, string>("Date", ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.Date.Value.ToString())
    });
  }

  [PXButton(Category = "Other")]
  [PXUIField]
  public virtual void CalculateRouteStats()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    RouteDocumentMaint.\u003C\u003Ec__DisplayClass80_0 cDisplayClass800 = new RouteDocumentMaint.\u003C\u003Ec__DisplayClass80_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass800.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass800.graphRouteDocumentMaint = PXGraph.CreateInstance<RouteDocumentMaint>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ((PXSelectBase<FSRouteDocument>) cDisplayClass800.graphRouteDocumentMaint.RouteRecords).Current = PXResultset<FSRouteDocument>.op_Implicit(((PXSelectBase<FSRouteDocument>) cDisplayClass800.graphRouteDocumentMaint.RouteRecords).Search<FSRouteDocument.routeDocumentID>((object) ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.RouteDocumentID, Array.Empty<object>()));
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass800, __methodptr(\u003CCalculateRouteStats\u003Eb__0)));
  }

  [PXUIField]
  protected virtual void openRouteSchedule()
  {
    if (((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current != null)
    {
      RouteServiceContractScheduleEntry instance = PXGraph.CreateInstance<RouteServiceContractScheduleEntry>();
      ((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Current = PXResultset<FSRouteContractSchedule>.op_Implicit(((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Search<FSSchedule.scheduleID>((object) ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current.ScheduleID, new object[1]
      {
        (object) ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Current.ServiceContractID
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  /// <summary>
  /// Open Appointment Screen with the given FSRouteDocument and Service Order Type.
  /// </summary>
  /// <param name="fsRouteDocumentRow"> Route Document.</param>
  /// <param name="srvOrdType"> Service Order Type.</param>
  public virtual void openAppointmentScreen(FSRouteDocument fsRouteDocumentRow, string srvOrdType)
  {
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    FSAppointment fsAppointment1 = new FSAppointment();
    fsAppointment1.RouteDocumentID = fsRouteDocumentRow.RouteDocumentID;
    fsAppointment1.RouteID = fsRouteDocumentRow.RouteID;
    if (!string.IsNullOrEmpty(srvOrdType))
      fsAppointment1.SrvOrdType = ((PXSelectBase<SrvOrderTypeRouteAux>) this.ServiceOrderTypeSelector).Current.SrvOrdType;
    FSAppointment fsAppointment2 = ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Insert(fsAppointment1);
    int? nullable1 = fsRouteDocumentRow.DriverID;
    if (nullable1.HasValue)
      ((PXSelectBase<FSAppointmentEmployee>) instance.AppointmentServiceEmployees).Insert(new FSAppointmentEmployee()
      {
        EmployeeID = fsRouteDocumentRow.DriverID,
        IsDriver = new bool?(true)
      });
    nullable1 = fsRouteDocumentRow.AdditionalDriverID;
    if (nullable1.HasValue)
      ((PXSelectBase<FSAppointmentEmployee>) instance.AppointmentServiceEmployees).Insert(new FSAppointmentEmployee()
      {
        EmployeeID = fsRouteDocumentRow.AdditionalDriverID,
        IsDriver = new bool?(true)
      });
    nullable1 = fsRouteDocumentRow.VehicleID;
    if (nullable1.HasValue)
    {
      fsAppointment2.VehicleID = fsRouteDocumentRow.VehicleID;
      fsAppointment2 = ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Update(fsAppointment2);
    }
    DateTime? nullable2 = (((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Current == null ? 0 : (((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Current.AutoCalculateRouteStats.GetValueOrDefault() ? 1 : 0)) != 0 ? fsAppointment2.ScheduledDateTimeBegin : fsRouteDocumentRow.TimeBegin;
    ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).SetValueExt<FSAppointment.scheduledDateTimeBegin>(fsAppointment2, (object) PXDBDateAndTimeAttribute.CombineDateTime(fsRouteDocumentRow.Date, nullable2));
    ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Update(fsAppointment2);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewStartGPSOnMap(PXAdapter adapter)
  {
    if (((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current != null && ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.RouteDocumentID.HasValue)
      new GoogleMapLatLongRedirector().ShowAddressByLocation(((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.GPSLatitudeStart, ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.GPSLongitudeStart);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewCompleteGPSOnMap(PXAdapter adapter)
  {
    if (((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current != null && ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.RouteDocumentID.HasValue)
      new GoogleMapLatLongRedirector().ShowAddressByLocation(((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.GPSLatitudeComplete, ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.GPSLongitudeComplete);
    return adapter.Get();
  }

  /// <summary>
  /// Completes all appointments belonging to the current Route, in case an error occurs with any appointment,
  /// the route will not be completed and a message will be displayed alerting the user about the appointment's issue.
  /// The row of the appointment having problems is marked with its error.
  /// </summary>
  /// <param name="errorMessage">Error message to be displayed.</param>
  /// <returns>True in case all appointments are completed, otherwise False.</returns>
  public virtual bool CompleteAppointmentsInRoute(ref string errorMessage)
  {
    bool flag = true;
    PXResultset<FSAppointmentInRoute> bqlResultSet = ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Select(Array.Empty<object>());
    if (bqlResultSet.Count > 0)
    {
      Dictionary<FSAppointment, string> dictionary = RouteDocumentMaint.CompleteAppointments(PXGraph.CreateInstance<ServiceOrderEntry>(), bqlResultSet);
      if (dictionary.Count > 0)
      {
        foreach (KeyValuePair<FSAppointment, string> keyValuePair in dictionary)
          ((PXSelectBase) this.AppointmentsInRoute).Cache.RaiseExceptionHandling<FSAppointment.refNbr>((object) keyValuePair.Key, (object) keyValuePair.Key.RefNbr, (Exception) new PXSetPropertyException(keyValuePair.Value, (PXErrorLevel) 5));
        flag = false;
        errorMessage = PXMessages.LocalizeFormatNoPrefix("The route execution cannot be completed. Some appointments have issues. See details below.", Array.Empty<object>());
      }
    }
    return flag;
  }

  private bool CancelAppointmentsInRoute(ref string errorMessage)
  {
    bool flag = true;
    List<PXResult<FSAppointmentInRoute>> list = ((IQueryable<PXResult<FSAppointmentInRoute>>) ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Select(Array.Empty<object>())).Where<PXResult<FSAppointmentInRoute>>((Expression<Func<PXResult<FSAppointmentInRoute>, bool>>) (x => ((FSAppointmentInRoute) x).Completed == (bool?) false && ((FSAppointmentInRoute) x).Closed == (bool?) false && ((FSAppointmentInRoute) x).Canceled == (bool?) false)).ToList<PXResult<FSAppointmentInRoute>>();
    if (list.Count<PXResult<FSAppointmentInRoute>>() > 0)
    {
      Dictionary<FSAppointment, string> dictionary = this.CancelAppointments((PXGraph) this, list);
      if (dictionary.Count > 0)
      {
        foreach (KeyValuePair<FSAppointment, string> keyValuePair in dictionary)
          ((PXSelectBase) this.AppointmentsInRoute).Cache.RaiseExceptionHandling<FSAppointment.refNbr>((object) keyValuePair.Key, (object) keyValuePair.Key.RefNbr, (Exception) new PXSetPropertyException(keyValuePair.Value, (PXErrorLevel) 5));
        flag = false;
        errorMessage = PXMessages.LocalizeFormatNoPrefix("The route execution cannot be canceled because some appointments have issues. See the Appointments tab.", Array.Empty<object>());
      }
    }
    return flag;
  }

  /// <summary>
  /// Enable/Disable the CalculateRouteStats button depending on FSRouteSetup.CalculateRouteStats flag.
  /// </summary>
  /// <param name="routeHasAppointments">Indicates if there are appointments in the grid.</param>
  /// <param name="fsRouteDocumentRow">Current Route Document instance.</param>
  public virtual void EnableDisableCalcRouteStatsButton(
    bool routeHasAppointments,
    FSRouteDocument fsRouteDocumentRow)
  {
    PXAction<FSRouteDocument> calculateRouteStats1 = this.calculateRouteStats;
    int num;
    if (routeHasAppointments && ((PXSelectBase) this.RouteRecords).Cache.AllowUpdate)
    {
      bool? calculateRouteStats2 = ((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Current.AutoCalculateRouteStats;
      bool flag1 = false;
      if (calculateRouteStats2.GetValueOrDefault() == flag1 & calculateRouteStats2.HasValue)
      {
        bool? routeStatsUpdated = fsRouteDocumentRow.RouteStatsUpdated;
        bool flag2 = false;
        if (routeStatsUpdated.GetValueOrDefault() == flag2 & routeStatsUpdated.HasValue)
        {
          num = 1;
          goto label_8;
        }
      }
      int? nullable = fsRouteDocumentRow.TotalTravelTime;
      if (nullable.HasValue && fsRouteDocumentRow.TotalDistance.HasValue && !string.IsNullOrEmpty(fsRouteDocumentRow.TotalDistanceFriendly))
      {
        nullable = fsRouteDocumentRow.TotalDuration;
        num = !nullable.HasValue ? 1 : 0;
      }
      else
        num = 1;
    }
    else
      num = 0;
label_8:
    ((PXAction) calculateRouteStats1).SetEnabled(num != 0);
  }

  /// <summary>Enable/Disable document fields and buttons.</summary>
  public virtual void EnableDisableDocument(PXCache cache, FSRouteDocument fsRouteDocumentRow)
  {
    int? routeDocumentId = fsRouteDocumentRow.RouteDocumentID;
    int num = 0;
    bool flag = routeDocumentId.GetValueOrDefault() > num & routeDocumentId.HasValue;
    bool routeHasAppointments = ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Select(Array.Empty<object>()).Count > 0;
    ((PXAction) this.openDriverCalendar).SetEnabled(flag && fsRouteDocumentRow.DriverID.HasValue);
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.branchID>(cache, (object) fsRouteDocumentRow, !routeHasAppointments && !flag);
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.routeID>(cache, (object) fsRouteDocumentRow, !flag);
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.actualStartTime>(cache, (object) fsRouteDocumentRow, fsRouteDocumentRow.Status != "O" && fsRouteDocumentRow.Status != "X" && fsRouteDocumentRow.Status != "Z");
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.actualEndTime>(cache, (object) fsRouteDocumentRow, fsRouteDocumentRow.Status != "O" && fsRouteDocumentRow.Status != "X" && fsRouteDocumentRow.Status != "Z");
    this.AllowUpdateRouteDocument(fsRouteDocumentRow);
    this.AllowDeleteRouteDocument(fsRouteDocumentRow);
    this.EnableDisableCalcRouteStatsButton(routeHasAppointments, fsRouteDocumentRow);
  }

  /// <summary>
  /// Enables/Disables the Update process in Route Document.
  /// </summary>
  public virtual void AllowUpdateRouteDocument(FSRouteDocument fsRouteDocumentRow)
  {
    ((PXSelectBase) this.RouteRecords).Cache.AllowUpdate = fsRouteDocumentRow.Status != "X" && fsRouteDocumentRow.Status != "Z";
  }

  /// <summary>
  /// Enables/Disables the Delete process in Route Document.
  /// </summary>
  public virtual void AllowDeleteRouteDocument(FSRouteDocument fsRouteDocumentRow)
  {
    bool flag = fsRouteDocumentRow.Status != "C" && fsRouteDocumentRow.Status != "X" && fsRouteDocumentRow.Status != "Z";
    ((PXSelectBase) this.RouteRecords).Cache.AllowDelete = flag;
    ((PXAction) this.DeleteRoute).SetEnabled(flag);
  }

  /// <summary>
  /// Enables/Disables the actions defined for ServiceContract
  /// It's called by RowSelected event of FSServiceContract.
  /// </summary>
  public virtual void EnableDisable_ActionButtons(PXCache cache, FSRouteDocument fsRouteDocumentRow)
  {
    bool flag1 = cache.GetStatus((object) fsRouteDocumentRow) != 2 && fsRouteDocumentRow.Status != "C" && fsRouteDocumentRow.Status != "X" && fsRouteDocumentRow.Status != "Z";
    bool flag2 = cache.GetStatus((object) fsRouteDocumentRow) != 2 && (fsRouteDocumentRow.Status == "O" || fsRouteDocumentRow.Status == "P");
    bool flag3 = ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Select(Array.Empty<object>()).Count > 1;
    ((PXAction) this.addAppointment).SetEnabled(flag1);
    ((PXAction) this.unassignAppointment).SetEnabled(flag1);
    ((PXAction) this.reassignAppointment).SetEnabled(flag1);
    ((PXAction) this.up).SetEnabled(flag1 & flag3);
    ((PXAction) this.down).SetEnabled(flag1 & flag3);
    ((PXAction) this.openRoutesOnMap).SetEnabled(((PXSelectBase) this.RouteRecords).Cache.GetStatus((object) fsRouteDocumentRow) != 2);
    ((PXAction) this.startRoute).SetEnabled(cache.GetStatus((object) fsRouteDocumentRow) != 2 && fsRouteDocumentRow.Status == "O");
    ((PXAction) this.completeRoute).SetEnabled(flag2);
    ((PXAction) this.cancelRoute).SetEnabled(flag2);
    ((PXAction) this.reopenRoute).SetEnabled(cache.GetStatus((object) fsRouteDocumentRow) != 2 && (fsRouteDocumentRow.Status == "C" || fsRouteDocumentRow.Status == "X" || fsRouteDocumentRow.Status == "P"));
    ((PXAction) this.uncloseRoute).SetEnabled(fsRouteDocumentRow.Status == "Z");
  }

  /// <summary>
  /// Sets (if it is defined) the TimeBegin of the Route Document depending on the execution day of the RouteID
  /// and the Day in which the Route is taking place.
  /// </summary>
  /// <param name="fsRouteDocumentRow">FSRouteDocument Row.</param>
  public virtual void SetRouteStartTimeByRouteID(FSRouteDocument fsRouteDocumentRow)
  {
    FSRoute fsRoute = FSRoute.PK.Find((PXGraph) this, fsRouteDocumentRow.RouteID);
    switch (fsRouteDocumentRow.Date.Value.DayOfWeek)
    {
      case DayOfWeek.Sunday:
        if (!fsRoute.ActiveOnSunday.GetValueOrDefault())
          break;
        fsRouteDocumentRow.TimeBegin = PXDBDateAndTimeAttribute.CombineDateTime(fsRouteDocumentRow.TimeBegin, fsRoute.BeginTimeOnSunday);
        break;
      case DayOfWeek.Monday:
        if (!fsRoute.ActiveOnMonday.GetValueOrDefault())
          break;
        fsRouteDocumentRow.TimeBegin = PXDBDateAndTimeAttribute.CombineDateTime(fsRouteDocumentRow.TimeBegin, fsRoute.BeginTimeOnMonday);
        break;
      case DayOfWeek.Tuesday:
        if (!fsRoute.ActiveOnTuesday.GetValueOrDefault())
          break;
        fsRouteDocumentRow.TimeBegin = PXDBDateAndTimeAttribute.CombineDateTime(fsRouteDocumentRow.TimeBegin, fsRoute.BeginTimeOnTuesday);
        break;
      case DayOfWeek.Wednesday:
        if (!fsRoute.ActiveOnWednesday.GetValueOrDefault())
          break;
        fsRouteDocumentRow.TimeBegin = PXDBDateAndTimeAttribute.CombineDateTime(fsRouteDocumentRow.TimeBegin, fsRoute.BeginTimeOnWednesday);
        break;
      case DayOfWeek.Thursday:
        if (!fsRoute.ActiveOnThursday.GetValueOrDefault())
          break;
        fsRouteDocumentRow.TimeBegin = PXDBDateAndTimeAttribute.CombineDateTime(fsRouteDocumentRow.TimeBegin, fsRoute.BeginTimeOnThursday);
        break;
      case DayOfWeek.Friday:
        if (!fsRoute.ActiveOnFriday.GetValueOrDefault())
          break;
        fsRouteDocumentRow.TimeBegin = PXDBDateAndTimeAttribute.CombineDateTime(fsRouteDocumentRow.TimeBegin, fsRoute.BeginTimeOnFriday);
        break;
      case DayOfWeek.Saturday:
        if (!fsRoute.ActiveOnSaturday.GetValueOrDefault())
          break;
        fsRouteDocumentRow.TimeBegin = PXDBDateAndTimeAttribute.CombineDateTime(fsRouteDocumentRow.TimeBegin, fsRoute.BeginTimeOnSaturday);
        break;
    }
  }

  /// <summary>
  /// Validates if there are any appointments assigned for the current route
  /// Return false if the route has no appointments and sets a [errorMessage] to display.
  /// </summary>
  public virtual bool AreThereAnyAppointmentsInThisRoute(
    string routeStatus,
    ref string errorMessage)
  {
    errorMessage = string.Empty;
    if (((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Select(Array.Empty<object>()).Count != 0)
      return true;
    switch (routeStatus)
    {
      case "P":
        errorMessage = PXMessages.LocalizeFormatNoPrefix("A route execution without appointments cannot be {0}. At least one appointment has to be added to the route execution.", new object[1]
        {
          (object) "started"
        });
        break;
      case "C":
        errorMessage = PXMessages.LocalizeFormatNoPrefix("A route execution without appointments cannot be {0}. At least one appointment has to be added to the route execution.", new object[1]
        {
          (object) "completed"
        });
        break;
    }
    return false;
  }

  /// <summary>
  /// Validates basics fields for an appointment address showing a warning if there are incomplete fields.
  /// </summary>
  public virtual void CheckAppointmentsAddress(PXCache cache, FSAppointment fsAppointmentRow)
  {
    FSAddress fsAddress = PXResultset<FSAddress>.op_Implicit(PXSelectBase<FSAddress, PXSelectJoin<FSAddress, InnerJoin<FSServiceOrder, On<FSServiceOrder.serviceOrderAddressID, Equal<FSAddress.addressID>>>, Where<FSServiceOrder.sOID, Equal<Required<FSAppointment.sOID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fsAppointmentRow.SOID
    }));
    if (fsAddress == null || (!string.IsNullOrEmpty(fsAddress.AddressLine1) || !string.IsNullOrEmpty(fsAddress.AddressLine2)) && !string.IsNullOrEmpty(fsAddress.City) && !string.IsNullOrEmpty(fsAddress.State) && !string.IsNullOrEmpty(fsAddress.PostalCode))
      return;
    cache.RaiseExceptionHandling<FSAppointment.refNbr>((object) fsAppointmentRow, (object) fsAppointmentRow.RefNbr, (Exception) new PXSetPropertyException("The address of the appointment is incomplete. This missing information can produce inconsistencies in statistics and routes.", (PXErrorLevel) 2));
  }

  /// <summary>
  /// Updates appointment's time after modify Route Document time begin.
  /// </summary>
  /// <param name="fsRouteDocumentRow">FSRouteDocument object.</param>
  public virtual void UpdateAppointmentsTimeBegin(FSRouteDocument fsRouteDocumentRow)
  {
    if (this.minutesToAdd == 0)
      return;
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    instance.CalculateGoogleStats = false;
    foreach (PXResult<FSAppointmentInRoute> pxResult in ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Select(Array.Empty<object>()))
    {
      FSAppointment fsAppointment = (FSAppointment) PXResult<FSAppointmentInRoute>.op_Implicit(pxResult);
      bool? nullable = fsAppointment.Closed;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = fsAppointment.Canceled;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
        {
          ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.appointmentID>((object) fsAppointment.AppointmentID, new object[1]
          {
            (object) fsAppointment.SrvOrdType
          }));
          fsAppointment.ScheduledDateTimeBegin = PXDBDateAndTimeAttribute.CombineDateTime(fsRouteDocumentRow.Date, new DateTime?(fsAppointment.ScheduledDateTimeBegin.Value.AddMinutes((double) this.minutesToAdd)));
          fsAppointment.ScheduledDateTimeEnd = PXDBDateAndTimeAttribute.CombineDateTime(fsRouteDocumentRow.Date, new DateTime?(fsAppointment.ScheduledDateTimeEnd.Value.AddMinutes((double) this.minutesToAdd)));
          ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current.ScheduledDateTimeBegin = fsAppointment.ScheduledDateTimeBegin;
          ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current.ScheduledDateTimeEnd = fsAppointment.ScheduledDateTimeEnd;
          ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Update(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current);
          ((PXAction) instance.Save).Press();
        }
      }
    }
    instance.CalculateGoogleStats = true;
    this.minutesToAdd = 0;
    this.needAppointmentDateUpdate = false;
    this.needAppointmentTimeBeginUpdate = false;
  }

  /// <summary>Check if the driver and additional driver are equal.</summary>
  /// <param name="cache">FSRouteDocument cache.</param>
  /// <param name="fsRouteDocumentRow">FSRouteDocument Row.</param>
  public virtual void ValidateDrivers(PXCache cache, FSRouteDocument fsRouteDocumentRow)
  {
    if (!fsRouteDocumentRow.DriverID.HasValue)
      return;
    int? driverId = fsRouteDocumentRow.DriverID;
    int? additionalDriverId = fsRouteDocumentRow.AdditionalDriverID;
    if (driverId.GetValueOrDefault() == additionalDriverId.GetValueOrDefault() & driverId.HasValue == additionalDriverId.HasValue)
    {
      cache.RaiseExceptionHandling<FSRouteDocument.additionalDriverID>((object) fsRouteDocumentRow, (object) fsRouteDocumentRow.AdditionalDriverID, (Exception) new PXSetPropertyException("The driver and additional driver cannot be the same employee.", (PXErrorLevel) 4));
      throw new PXException("The driver and additional driver cannot be the same employee.", new object[1]
      {
        (object) (PXErrorLevel) 4
      });
    }
  }

  /// <summary>Check if the vehicles given are not repeated.</summary>
  /// <param name="cache">FSRouteDocument cache.</param>
  /// <param name="fsRouteDocumentRow">FSRouteDocument Row.</param>
  public virtual void ValidateVehicles(PXCache cache, FSRouteDocument fsRouteDocumentRow)
  {
    bool flag = false;
    int? nullable;
    if (fsRouteDocumentRow.VehicleID.HasValue)
    {
      int? vehicleId = fsRouteDocumentRow.VehicleID;
      nullable = fsRouteDocumentRow.AdditionalVehicleID1;
      if (vehicleId.GetValueOrDefault() == nullable.GetValueOrDefault() & vehicleId.HasValue == nullable.HasValue)
      {
        cache.RaiseExceptionHandling<FSRouteDocument.additionalVehicleID1>((object) fsRouteDocumentRow, (object) fsRouteDocumentRow.AdditionalVehicleID1, (Exception) new PXSetPropertyException("The vehicles cannot be repeated.", (PXErrorLevel) 4));
        flag = true;
      }
    }
    nullable = fsRouteDocumentRow.VehicleID;
    if (nullable.HasValue)
    {
      nullable = fsRouteDocumentRow.VehicleID;
      int? additionalVehicleId2 = fsRouteDocumentRow.AdditionalVehicleID2;
      if (nullable.GetValueOrDefault() == additionalVehicleId2.GetValueOrDefault() & nullable.HasValue == additionalVehicleId2.HasValue)
      {
        cache.RaiseExceptionHandling<FSRouteDocument.additionalVehicleID2>((object) fsRouteDocumentRow, (object) fsRouteDocumentRow.AdditionalVehicleID2, (Exception) new PXSetPropertyException("The vehicles cannot be repeated.", (PXErrorLevel) 4));
        flag = true;
      }
    }
    if (flag)
      throw new PXException("The vehicles cannot be repeated.", new object[1]
      {
        (object) (PXErrorLevel) 4
      });
  }

  /// <summary>Validate if the Trip is Valid for the routeDate.</summary>
  /// <param name="fsRouteRow">FSRoute Row.</param>
  /// <returns>The name of the day if invalid, null otherwise.</returns>
  public virtual string InvalidDayTrip(FSRoute fsRouteRow)
  {
    string str = (string) null;
    int? nullable1 = new int?(PXSelectBase<FSRoute, PXSelectReadonly2<FSRoute, InnerJoin<FSRouteDocument, On<FSRoute.routeID, Equal<FSRouteDocument.routeID>>>, Where<FSRoute.routeID, Equal<Required<FSRoute.routeID>>, And<FSRouteDocument.date, Equal<Required<FSRouteDocument.date>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) fsRouteRow.RouteID,
      (object) ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.Date
    }).Count + 1);
    switch (((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.Date.Value.DayOfWeek)
    {
      case DayOfWeek.Sunday:
        int? nullable2 = nullable1;
        int? nbrTripOnSunday = fsRouteRow.NbrTripOnSunday;
        if (nullable2.GetValueOrDefault() > nbrTripOnSunday.GetValueOrDefault() & nullable2.HasValue & nbrTripOnSunday.HasValue)
        {
          str = "Sunday";
          break;
        }
        break;
      case DayOfWeek.Monday:
        int? nullable3 = nullable1;
        int? nbrTripOnMonday = fsRouteRow.NbrTripOnMonday;
        if (nullable3.GetValueOrDefault() > nbrTripOnMonday.GetValueOrDefault() & nullable3.HasValue & nbrTripOnMonday.HasValue)
        {
          str = "Monday";
          break;
        }
        break;
      case DayOfWeek.Tuesday:
        int? nullable4 = nullable1;
        int? nbrTripOnTuesday = fsRouteRow.NbrTripOnTuesday;
        if (nullable4.GetValueOrDefault() > nbrTripOnTuesday.GetValueOrDefault() & nullable4.HasValue & nbrTripOnTuesday.HasValue)
        {
          str = "Tuesday";
          break;
        }
        break;
      case DayOfWeek.Wednesday:
        int? nullable5 = nullable1;
        int? nbrTripOnWednesday = fsRouteRow.NbrTripOnWednesday;
        if (nullable5.GetValueOrDefault() > nbrTripOnWednesday.GetValueOrDefault() & nullable5.HasValue & nbrTripOnWednesday.HasValue)
        {
          str = "Wednesday";
          break;
        }
        break;
      case DayOfWeek.Thursday:
        int? nullable6 = nullable1;
        int? nbrTripOnThursday = fsRouteRow.NbrTripOnThursday;
        if (nullable6.GetValueOrDefault() > nbrTripOnThursday.GetValueOrDefault() & nullable6.HasValue & nbrTripOnThursday.HasValue)
        {
          str = "Thursday";
          break;
        }
        break;
      case DayOfWeek.Friday:
        int? nullable7 = nullable1;
        int? nbrTripOnFriday = fsRouteRow.NbrTripOnFriday;
        if (nullable7.GetValueOrDefault() > nbrTripOnFriday.GetValueOrDefault() & nullable7.HasValue & nbrTripOnFriday.HasValue)
        {
          str = "Friday";
          break;
        }
        break;
      case DayOfWeek.Saturday:
        int? nullable8 = nullable1;
        int? nbrTripOnSaturday = fsRouteRow.NbrTripOnSaturday;
        if (nullable8.GetValueOrDefault() > nbrTripOnSaturday.GetValueOrDefault() & nullable8.HasValue & nbrTripOnSaturday.HasValue)
        {
          str = "Saturday";
          break;
        }
        break;
    }
    return str;
  }

  /// <summary>Validate if the trip number already exist.</summary>
  /// <param name="fsRouteDocumentRow">Route Document Row.</param>
  /// <param name="tripNbr">Trip Number.</param>
  /// <returns>True is valid, false otherwise.</returns>
  public virtual bool TripIDAlreadyExist(FSRouteDocument fsRouteDocumentRow, int? tripNbr)
  {
    return PXResultset<FSRouteDocument>.op_Implicit(PXSelectBase<FSRouteDocument, PXSelectReadonly<FSRouteDocument, Where<FSRouteDocument.routeID, Equal<Required<FSRoute.routeID>>, And<FSRouteDocument.date, Equal<Required<FSRouteDocument.date>>, And<FSRouteDocument.tripNbr, Equal<Required<FSRouteDocument.tripNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) fsRouteDocumentRow.RouteID,
      (object) fsRouteDocumentRow.Date,
      (object) tripNbr
    })) != null;
  }

  /// <summary>
  /// Sets the FuelType to the <c>fsRouteDocumentRow</c> using the FuelType in the <c>vechicleID</c>
  /// </summary>
  public virtual void SetRouteFuelType(FSRouteDocument fsRouteDocumentRow, int? vechicleID)
  {
    FSVehicle fsVehicle = PXResultset<FSVehicle>.op_Implicit(PXSelectBase<FSVehicle, PXSelect<FSVehicle, Where<FSVehicle.SMequipmentID, Equal<Required<FSVehicle.SMequipmentID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) vechicleID
    }));
    if (fsVehicle.FuelType == null)
      return;
    fsRouteDocumentRow.FuelType = fsVehicle.FuelType;
  }

  public virtual void UpdateAppointmentInRoute(
    FSRouteDocument fsRouteDocumentRow,
    bool dateTimeChanged,
    bool vehicleChanged,
    bool driverChanged,
    bool additionalDriverChanged)
  {
    if (!dateTimeChanged && !vehicleChanged && !driverChanged && !additionalDriverChanged)
      return;
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    foreach (PXResult<FSAppointment> pxResult in PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.routeDocumentID, Equal<Required<FSAppointment.routeDocumentID>>, And<FSAppointment.canceled, Equal<False>, And<FSAppointment.closed, Equal<False>>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) fsRouteDocumentRow.RouteDocumentID
    }))
    {
      FSAppointment fsAppointment = PXResult<FSAppointment>.op_Implicit(pxResult);
      ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.refNbr>((object) fsAppointment.RefNbr, new object[1]
      {
        (object) fsAppointment.SrvOrdType
      }));
      if (dateTimeChanged)
      {
        FSAppointment current1 = ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current;
        DateTime? date1 = fsRouteDocumentRow.Date;
        DateTime? nullable1 = fsAppointment.ScheduledDateTimeBegin;
        DateTime? nullable2 = new DateTime?(nullable1.Value.AddMinutes((double) this.minutesToAdd));
        DateTime? nullable3 = PXDBDateAndTimeAttribute.CombineDateTime(date1, nullable2);
        current1.ScheduledDateTimeBegin = nullable3;
        FSAppointment current2 = ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current;
        DateTime? date2 = fsRouteDocumentRow.Date;
        nullable1 = fsAppointment.ScheduledDateTimeEnd;
        DateTime? nullable4 = new DateTime?(nullable1.Value.AddMinutes((double) this.minutesToAdd));
        DateTime? nullable5 = PXDBDateAndTimeAttribute.CombineDateTime(date2, nullable4);
        current2.ScheduledDateTimeEnd = nullable5;
      }
      if (vehicleChanged)
        ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current.VehicleID = fsRouteDocumentRow.VehicleID;
      if (dateTimeChanged || vehicleChanged)
        ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Update(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current);
      if (driverChanged)
        this.AssignDriverToAppointmentsInRoute(instance, ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current, fsRouteDocumentRow.DriverID, this.oldDriverID);
      if (additionalDriverChanged)
        this.AssignDriverToAppointmentsInRoute(instance, ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current, fsRouteDocumentRow.AdditionalDriverID, this.oldAdditionalDriverID);
      instance.AvoidCalculateRouteStats = true;
      instance.SkipServiceOrderUpdate = true;
      instance.CalculateGoogleStats = false;
      try
      {
        instance.RecalculateExternalTaxesSync = true;
        ((PXAction) instance.Save).Press();
      }
      finally
      {
        instance.RecalculateExternalTaxesSync = false;
      }
    }
  }

  /// <summary>
  /// Assigns driver of the route to the appointments in it.
  /// </summary>
  public virtual void AssignDriverToAppointmentsInRoute(
    AppointmentEntry graphAppointmentMaint,
    FSAppointment fsAppointmentRow,
    int? localNewDriverID,
    int? localOldDriverID)
  {
    PXResultset<FSAppointmentEmployee> pxResultset1 = ((PXSelectBase<FSAppointmentEmployee>) graphAppointmentMaint.AppointmentServiceEmployees).Search<FSAppointmentEmployee.employeeID, FSAppointmentEmployee.serviceLineRef>((object) localOldDriverID, (object) null, Array.Empty<object>());
    PXResultset<FSAppointmentEmployee> pxResultset2 = ((PXSelectBase<FSAppointmentEmployee>) graphAppointmentMaint.AppointmentServiceEmployees).Search<FSAppointmentEmployee.employeeID, FSAppointmentEmployee.serviceLineRef>((object) localNewDriverID, (object) null, Array.Empty<object>());
    if (localNewDriverID.HasValue && pxResultset2.Count > 0)
    {
      foreach (PXResult<FSAppointmentEmployee> pxResult in pxResultset2)
      {
        FSAppointmentEmployee appointmentEmployee = PXResult<FSAppointmentEmployee>.op_Implicit(pxResult);
        appointmentEmployee.IsDriver = new bool?(true);
        ((PXSelectBase<FSAppointmentEmployee>) graphAppointmentMaint.AppointmentServiceEmployees).Update(appointmentEmployee);
      }
    }
    if (localNewDriverID.HasValue && pxResultset2.Count == 0)
      ((PXSelectBase<FSAppointmentEmployee>) graphAppointmentMaint.AppointmentServiceEmployees).Insert(new FSAppointmentEmployee()
      {
        AppointmentID = fsAppointmentRow.AppointmentID,
        EmployeeID = localNewDriverID,
        IsDriver = new bool?(true)
      });
    if (!localOldDriverID.HasValue)
      return;
    int? nullable1 = localOldDriverID;
    int? nullable2 = localNewDriverID;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue || pxResultset1.Count <= 0)
      return;
    foreach (PXResult<FSAppointmentEmployee> pxResult in pxResultset1)
    {
      FSAppointmentEmployee appointmentEmployee = PXResult<FSAppointmentEmployee>.op_Implicit(pxResult);
      if (appointmentEmployee.IsDriver.GetValueOrDefault())
      {
        int? employeeId = appointmentEmployee.EmployeeID;
        int? nullable3 = localOldDriverID;
        if (employeeId.GetValueOrDefault() == nullable3.GetValueOrDefault() & employeeId.HasValue == nullable3.HasValue)
          ((PXSelectBase<FSAppointmentEmployee>) graphAppointmentMaint.AppointmentServiceEmployees).Delete(appointmentEmployee);
      }
    }
  }

  /// <summary>
  /// Returns true if a Route Document [fsRouteDocumentRow] can change it's status to [newRouteStatus] based on the current status of the Route Document [fsRouteDocumentRow]
  /// If an error is detected is going to be assigned to the [errorMessage].
  /// </summary>
  public virtual bool IsThisStatusTransitionAllowed(
    FSRouteDocument fsRouteDocumentRow,
    string newRouteStatus,
    ref string errorMessage)
  {
    errorMessage = string.Empty;
    if (fsRouteDocumentRow.Status == "O" && newRouteStatus == "P" || (fsRouteDocumentRow.Status == "O" || fsRouteDocumentRow.Status == "P") && newRouteStatus == "C" || (fsRouteDocumentRow.Status == "O" || fsRouteDocumentRow.Status == "P") && newRouteStatus == "X" || (fsRouteDocumentRow.Status == "C" || fsRouteDocumentRow.Status == "X" || fsRouteDocumentRow.Status == "P") && newRouteStatus == "O")
      return true;
    errorMessage = "The transition of the route status is invalid.";
    return false;
  }

  /// <summary>Normalize route position in the appointment list.</summary>
  public virtual void NormalizeAppointmentPosition()
  {
    PXResultset<FSAppointmentInRoute> pxResultset = ((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Select(Array.Empty<object>());
    int num = 1;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (PXResult<FSAppointmentInRoute> pxResult in pxResultset)
      {
        FSAppointment fsAppointment = (FSAppointment) PXResult<FSAppointmentInRoute>.op_Implicit(pxResult);
        PXUpdate<Set<FSAppointment.routePosition, Required<FSAppointment.routePosition>>, FSAppointment, Where<FSAppointment.appointmentID, Equal<Required<FSAppointment.appointmentID>>>>.Update((PXGraph) this, new object[2]
        {
          (object) num,
          (object) fsAppointment.AppointmentID
        });
        ++num;
      }
      transactionScope.Complete();
    }
  }

  public virtual void calculateSimpleRouteStatistic()
  {
    FSRouteDocument current = ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current;
    current.TotalNumAppointments = new int?(((PXSelectBase<FSAppointmentInRoute>) this.AppointmentsInRoute).Select(Array.Empty<object>()).Count);
    PXResultset<FSAppointmentDet> pxResultset = PXSelectBase<FSAppointmentDet, PXSelectJoin<FSAppointmentDet, InnerJoin<FSAppointment, On<FSAppointmentDet.appointmentID, Equal<FSAppointment.appointmentID>>, InnerJoin<FSSODet, On<FSSODet.sODetID, Equal<FSAppointmentDet.sODetID>>>>, Where<FSSODet.lineType, Equal<ListField_LineType_ALL.Service>, And<FSAppointment.routeDocumentID, Equal<Required<FSAppointment.routeDocumentID>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) current.RouteDocumentID
    });
    current.TotalServices = new int?(pxResultset.Count);
    current.TotalServicesDuration = new int?(0);
    foreach (PXResult<FSAppointmentDet> pxResult in pxResultset)
    {
      FSAppointmentDet fsAppointmentDet = PXResult<FSAppointmentDet>.op_Implicit(pxResult);
      FSRouteDocument fsRouteDocument = current;
      int? servicesDuration = fsRouteDocument.TotalServicesDuration;
      int? estimatedDuration = fsAppointmentDet.EstimatedDuration;
      fsRouteDocument.TotalServicesDuration = servicesDuration.HasValue & estimatedDuration.HasValue ? new int?(servicesDuration.GetValueOrDefault() + estimatedDuration.GetValueOrDefault()) : new int?();
    }
    ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Update(current);
    ((PXAction) this.Save).Press();
  }

  /// <summary>
  /// @TODO: This is a temporal solution. It must be improved
  /// AC-107854
  /// AC-142850
  /// </summary>
  public virtual void CalculateStats()
  {
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CCalculateStats\u003Eb__109_0)));
  }

  public virtual void UncloseRouteAndSave(PXCache viewCache, FSRouteDocument fsRouteDocumentRow)
  {
    fsRouteDocumentRow.Status = "C";
    this.ForceUpdateCacheAndSave(viewCache, (object) fsRouteDocumentRow);
  }

  public virtual void ForceUpdateCacheAndSave(PXCache cache, object row)
  {
    cache.AllowUpdate = true;
    cache.SetStatus(row, (PXEntryStatus) 1);
    ((PXGraph) this).GetSaveAction().Press();
  }

  /// <summary>
  /// Move appointment from original position to new position and recalculate route statistics.
  /// </summary>
  public virtual void MoveAppointmentInRoute(
    PXGraph graph,
    FSRouteDocument fsRouteDocumentRow,
    int? positionFrom,
    int? positionTo)
  {
    FSAppointment fsAppointment1 = PXResultset<FSAppointment>.op_Implicit(PXSelectBase<FSAppointment, PXSelectReadonly<FSAppointment, Where<FSAppointment.routePosition, Equal<Required<FSAppointment.routePosition>>, And<FSAppointment.routeDocumentID, Equal<Required<FSAppointment.routeDocumentID>>>>>.Config>.Select(graph, new object[2]
    {
      (object) positionFrom,
      (object) fsRouteDocumentRow.RouteDocumentID
    }));
    FSAppointment fsAppointment2 = PXResultset<FSAppointment>.op_Implicit(PXSelectBase<FSAppointment, PXSelectReadonly<FSAppointment, Where<FSAppointment.routePosition, Equal<Required<FSAppointment.routePosition>>, And<FSAppointment.routeDocumentID, Equal<Required<FSAppointment.routeDocumentID>>>>>.Config>.Select(graph, new object[2]
    {
      (object) positionTo,
      (object) fsRouteDocumentRow.RouteDocumentID
    }));
    if (fsAppointment1 == null || fsAppointment2 == null)
      return;
    PXCache<FSAppointment> pxCache = new PXCache<FSAppointment>(graph);
    fsAppointment1.RoutePosition = positionTo;
    ((PXCache) pxCache).Update((object) fsAppointment1);
    fsAppointment2.RoutePosition = positionFrom;
    ((PXCache) pxCache).Update((object) fsAppointment2);
    ((PXCache) pxCache).Persist((PXDBOperation) 1);
  }

  public virtual Dictionary<FSAppointment, string> CancelAppointments(
    PXGraph graph,
    List<PXResult<FSAppointmentInRoute>> appointments)
  {
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    instance.SkipCallSOAction = true;
    Dictionary<FSAppointment, string> dictionary = new Dictionary<FSAppointment, string>();
    foreach (PXResult<FSAppointmentInRoute> appointment in appointments)
    {
      FSAppointment key = (FSAppointment) PXResult<FSAppointmentInRoute>.op_Implicit(appointment);
      ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.appointmentID>((object) key.AppointmentID, new object[1]
      {
        (object) key.SrvOrdType
      }));
      try
      {
        if (((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current.InProcess.GetValueOrDefault())
          ((PXAction) instance.reopenAppointment).Press();
        ((PXAction) instance.cancelAppointment).Press();
      }
      catch (PXException ex)
      {
        dictionary.Add(key, ((Exception) ex).Message);
      }
    }
    return dictionary;
  }

  public static Dictionary<FSAppointment, string> CompleteAppointments(
    ServiceOrderEntry graph,
    PXResultset<FSAppointmentInRoute> bqlResultSet)
  {
    return ServiceOrderEntry.CompleteAppointmentsInt(graph, bqlResultSet);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSRouteDocument, FSRouteDocument.actualStartTime> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSRouteDocument, FSRouteDocument.timeBegin> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSRouteDocument, FSRouteDocument.timeBegin>, FSRouteDocument, object>) e).NewValue = (object) PXDBDateAndTimeAttribute.CombineDateTime(((PXGraph) this).Accessinfo.BusinessDate, new DateTime?(PXTimeZoneInfo.Now));
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSRouteDocument, FSRouteDocument.tripNbr> e)
  {
    if (((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current == null)
      return;
    FSRouteDocument fsRouteDocument = PXResultset<FSRouteDocument>.op_Implicit(PXSelectBase<FSRouteDocument, PXSelectReadonly2<FSRouteDocument, InnerJoin<FSRoute, On<FSRouteDocument.routeID, Equal<FSRoute.routeID>>>, Where<FSRouteDocument.routeID, Equal<Required<FSRouteDocument.routeID>>, And<FSRouteDocument.date, Equal<Required<FSRouteDocument.date>>>>, OrderBy<Desc<FSRouteDocument.tripNbr>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
    {
      (object) ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.RouteID,
      (object) ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.Date
    }));
    if (fsRouteDocument != null)
    {
      PX.Data.Events.FieldDefaulting<FSRouteDocument, FSRouteDocument.tripNbr> fieldDefaulting = e;
      int? tripNbr = fsRouteDocument.TripNbr;
      // ISSUE: variable of a boxed type
      __Boxed<int?> local = (ValueType) (tripNbr.HasValue ? new int?(tripNbr.GetValueOrDefault() + 1) : new int?());
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSRouteDocument, FSRouteDocument.tripNbr>, FSRouteDocument, object>) fieldDefaulting).NewValue = (object) local;
    }
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSRouteDocument, FSRouteDocument.tripNbr>, FSRouteDocument, object>) e).NewValue = (object) 1;
  }

  protected virtual void FSRouteDocument_TimeBegin_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null)
      return;
    DateTime? handlingDateTime = SharedFunctions.TryParseHandlingDateTime(cache, e.NewValue);
    FSRouteDocument row = (FSRouteDocument) e.Row;
    if (!handlingDateTime.HasValue)
      return;
    e.NewValue = (object) PXDBDateAndTimeAttribute.CombineDateTime(row.Date, handlingDateTime);
  }

  protected virtual void FSRouteDocument_ActualStartTime_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    FSRouteDocument row = (FSRouteDocument) e.Row;
    DateTime? handlingDateTime = SharedFunctions.TryParseHandlingDateTime(cache, e.NewValue);
    if (handlingDateTime.HasValue)
    {
      DateTime? nullable = PXDBDateAndTimeAttribute.CombineDateTime(row.ActualStartTime, handlingDateTime);
      e.NewValue = (object) nullable;
      cache.SetValuePending(e.Row, typeof (FSAppointment.actualDateTimeEnd).Name + "newTime", (object) nullable);
    }
    row.ActualStartTime = SharedFunctions.TryParseHandlingDateTime(cache, e.NewValue);
    if (row.ActualStartTime.HasValue)
      ((PXSelectBase) this.RouteRecords).Cache.SetValueExt<FSRouteDocument.actualEndTime>((object) row, (object) row.ActualStartTime.Value.AddHours(1.0));
    else
      ((PXSelectBase) this.RouteRecords).Cache.SetValueExt<FSRouteDocument.actualEndTime>((object) row, (object) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<FSRouteDocument, FSRouteDocument.tripNbr> e)
  {
    if (e.Row == null)
      return;
    FSRouteDocument row = e.Row;
    int? tripNbr = new int?(Convert.ToInt32(((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSRouteDocument, FSRouteDocument.tripNbr>>) e).NewValue));
    row.TripNbr = tripNbr;
    if (!this.TripIDAlreadyExist(row, tripNbr))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatingEventArgs, PX.Data.Events.FieldUpdating<FSRouteDocument, FSRouteDocument.tripNbr>>) e).Cache.RaiseExceptionHandling<FSRouteDocument.tripNbr>((object) row, (object) row.TripNbr, (Exception) new PXException("The trip number already exists. Define another number."));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRouteDocument, FSRouteDocument.vehicleID> e)
  {
    if (e.Row == null)
      return;
    FSRouteDocument row = e.Row;
    if (!row.VehicleID.HasValue)
      return;
    this.SetRouteFuelType(row, row.VehicleID);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRouteDocument, FSRouteDocument.additionalVehicleID1> e)
  {
    if (e.Row == null)
      return;
    FSRouteDocument row = e.Row;
    if (!row.AdditionalVehicleID1.HasValue)
      return;
    this.SetRouteFuelType(row, row.AdditionalVehicleID1);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRouteDocument, FSRouteDocument.additionalVehicleID2> e)
  {
    if (e.Row == null)
      return;
    FSRouteDocument row = e.Row;
    if (!row.AdditionalVehicleID2.HasValue)
      return;
    this.SetRouteFuelType(row, row.AdditionalVehicleID2);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRouteDocument, FSRouteDocument.routeID> e)
  {
    if (e.Row == null)
      return;
    FSRouteDocument row = e.Row;
    if (row.Date.HasValue)
      this.SetRouteStartTimeByRouteID(row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSRouteDocument, FSRouteDocument.routeID>>) e).Cache.SetDefaultExt<FSRouteDocument.routeCD>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRouteDocument, FSRouteDocument.date> e)
  {
    if (e.Row == null)
      return;
    FSRouteDocument row = e.Row;
    if (row.TimeBegin.HasValue)
    {
      row.TimeBegin = PXDBDateAndTimeAttribute.CombineDateTime(row.Date, row.TimeBegin);
    }
    else
    {
      FSRouteDocument fsRouteDocument = row;
      DateTime? date = row.Date;
      int year = DateTime.Now.Year;
      DateTime now = DateTime.Now;
      int month = now.Month;
      now = DateTime.Now;
      int day = now.Day;
      DateTime? nullable1 = new DateTime?(new DateTime(year, month, day, 0, 0, 0));
      DateTime? nullable2 = PXDBDateAndTimeAttribute.CombineDateTime(date, nullable1);
      fsRouteDocument.TimeBegin = nullable2;
    }
    if (!row.RouteID.HasValue)
      return;
    this.SetRouteStartTimeByRouteID(row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSRouteDocument> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSRouteDocument> e)
  {
    if (e.Row == null)
      return;
    FSRouteDocument row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSRouteDocument>>) e).Cache;
    this.EnableDisableDocument(cache, row);
    this.EnableDisable_ActionButtons(cache, row);
    SharedFunctions.CheckRouteActualDateTimes(cache, row, ((PXGraph) this).Accessinfo.BusinessDate);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSRouteDocument> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSRouteDocument> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSRouteDocument> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSRouteDocument> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSRouteDocument> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSRouteDocument> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSRouteDocument> e)
  {
    if (e.Row == null)
      return;
    FSRouteDocument row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSRouteDocument>>) e).Cache;
    if (e.Operation == 2 || e.Operation == 1)
    {
      this.ValidateDrivers(cache, row);
      this.ValidateVehicles(cache, row);
    }
    PXDBOperation operation = e.Operation;
    if (operation != 1)
    {
      if (operation != 2)
        return;
      FSRoute fsRouteRow1 = FSRoute.PK.Find((PXGraph) this, row.RouteID);
      if (fsRouteRow1 == null)
        return;
      DateTime? nullable = new DateTime?();
      FSRoute fsRouteRow2 = fsRouteRow1;
      DateTime? date1 = row.Date;
      DateTime dateTime = date1.Value;
      int dayOfWeek = (int) dateTime.DayOfWeek;
      ref DateTime? local = ref nullable;
      if (!SharedFunctions.EvaluateExecutionDay(fsRouteRow2, (DayOfWeek) dayOfWeek, ref local))
      {
        PXCache pxCache = cache;
        FSRouteDocument fsRouteDocument = row;
        // ISSUE: variable of a boxed type
        __Boxed<DateTime?> date2 = (ValueType) row.Date;
        object[] objArray = new object[2];
        date1 = row.Date;
        dateTime = date1.Value;
        objArray[0] = (object) dateTime.DayOfWeek;
        objArray[1] = (object) fsRouteRow1.RouteCD;
        PXException pxException = new PXException("{0} is not set as an execution day for the {1} route on the Routes (FS203700) form.", objArray);
        pxCache.RaiseExceptionHandling<FSRouteDocument.date>((object) fsRouteDocument, (object) date2, (Exception) pxException);
      }
      else if (fsRouteRow1.WeekCode != null && !SharedFunctions.WeekCodeIsValid(fsRouteRow1.WeekCode, row.Date, (PXGraph) this))
        cache.RaiseExceptionHandling<FSRouteDocument.date>((object) row, (object) row.Date, (Exception) new PXException("This date does not correspond to the {0} week code set for the {1} route.", new object[2]
        {
          (object) fsRouteRow1.WeekCode,
          (object) fsRouteRow1.RouteCD
        }));
      else if (this.InvalidDayTrip(fsRouteRow1) != null)
      {
        PXCache pxCache = cache;
        FSRouteDocument fsRouteDocument = row;
        // ISSUE: variable of a boxed type
        __Boxed<int?> tripNbr = (ValueType) row.TripNbr;
        object[] objArray = new object[1];
        date1 = row.Date;
        dateTime = date1.Value;
        objArray[0] = (object) dateTime.DayOfWeek;
        PXException pxException = new PXException("The route has reached the maximum number of trips on {0}.", objArray);
        pxCache.RaiseExceptionHandling<FSRouteDocument.tripNbr>((object) fsRouteDocument, (object) tripNbr, (Exception) pxException);
      }
      else
      {
        if (!this.TripIDAlreadyExist(((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current, ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current.TripNbr))
          return;
        cache.RaiseExceptionHandling<FSRouteDocument.tripNbr>((object) row, (object) row.TripNbr, (Exception) new PXException("The trip number already exists. Define another number."));
      }
    }
    else
    {
      this.oldDriverID = (int?) cache.GetValueOriginal<FSRouteDocument.driverID>((object) row);
      this.oldAdditionalDriverID = (int?) cache.GetValueOriginal<FSRouteDocument.additionalDriverID>((object) row);
      int? valueOriginal1 = (int?) cache.GetValueOriginal<FSRouteDocument.vehicleID>((object) row);
      int? vehicleId = row.VehicleID;
      int? nullable1 = valueOriginal1;
      int? nullable2 = vehicleId;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        this.vehicleChanged = true;
      DateTime? valueOriginal2 = (DateTime?) cache.GetValueOriginal<FSRouteDocument.date>((object) row);
      DateTime? date = row.Date;
      DateTime? nullable3 = valueOriginal2;
      DateTime? nullable4 = date;
      if ((nullable3.HasValue == nullable4.HasValue ? (nullable3.HasValue ? (nullable3.GetValueOrDefault() != nullable4.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        this.needAppointmentDateUpdate = true;
      DateTime? valueOriginal3 = (DateTime?) cache.GetValueOriginal<FSRouteDocument.timeBegin>((object) row);
      DateTime? timeBegin = row.TimeBegin;
      nullable4 = valueOriginal3;
      DateTime? nullable5 = timeBegin;
      if ((nullable4.HasValue == nullable5.HasValue ? (nullable4.HasValue ? (nullable4.GetValueOrDefault() != nullable5.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        this.needAppointmentTimeBeginUpdate = true;
        nullable5 = timeBegin;
        nullable4 = valueOriginal3;
        this.minutesToAdd = (int) (nullable5.HasValue & nullable4.HasValue ? new TimeSpan?(nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new TimeSpan?()).Value.TotalMinutes;
      }
      if ((((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Current == null || !((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Current.AutoCalculateRouteStats.GetValueOrDefault() ? 0 : (row != null ? (row.MustRecalculateStats.GetValueOrDefault() ? 1 : 0) : 0)) == 0)
        return;
      this.CalculateStats();
      row.MustRecalculateStats = new bool?(false);
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSRouteDocument> e)
  {
    if (e.Row == null)
      return;
    FSRouteDocument row = e.Row;
    if (e.Operation != 1 || e.TranStatus != null)
      return;
    FSRouteDocument fsRouteDocumentRow = row;
    int num1 = this.needAppointmentTimeBeginUpdate ? 1 : (this.needAppointmentDateUpdate ? 1 : 0);
    int num2 = this.vehicleChanged ? 1 : 0;
    int? nullable1 = row.DriverID;
    int? nullable2 = this.oldDriverID;
    int num3 = !(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) ? 1 : 0;
    nullable2 = row.AdditionalDriverID;
    nullable1 = this.oldAdditionalDriverID;
    int num4 = !(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue) ? 1 : 0;
    this.UpdateAppointmentInRoute(fsRouteDocumentRow, num1 != 0, num2 != 0, num3 != 0, num4 != 0);
    this.needAppointmentTimeBeginUpdate = false;
    this.needAppointmentDateUpdate = false;
    this.vehicleChanged = false;
    this.oldAdditionalDriverID = new int?();
    this.oldDriverID = new int?();
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSAppointment> e)
  {
    if (e.Row == null)
      return;
    FSAppointment row = e.Row;
    this.CheckAppointmentsAddress(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointment>>) e).Cache, row);
  }
}
