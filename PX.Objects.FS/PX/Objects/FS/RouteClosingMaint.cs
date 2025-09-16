// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteClosingMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class RouteClosingMaint : PXGraph<RouteClosingMaint, FSRouteDocument>
{
  public bool AutomaticallyCloseRoute;
  public bool AutomaticallyUncloseRoute;
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> BAccount;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Contact> Contact;
  [PXHidden]
  public PXSelect<FSServiceOrder> ServiceOrder;
  [PXHidden]
  public PXSelect<FSAppointment> Appointment;
  public PXSetup<FSRouteSetup> RouteSetupRecord;
  public PXSelect<FSRouteDocument, Where<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>>>> RouteRecords;
  public PXSelect<FSRouteDocument, Where<FSRouteDocument.routeDocumentID, Equal<Current<FSRouteDocument.routeDocumentID>>>> RouteDocumentSelected;
  public PXSelectJoin<FSAppointment, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, InnerJoin<FSAddress, On<FSAddress.addressID, Equal<FSServiceOrder.serviceOrderAddressID>>, LeftJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSAppointment.serviceContractID>>>>>, Where<FSAppointment.routeDocumentID, Equal<Current<FSRouteDocument.routeDocumentID>>>, OrderBy<Asc<FSAppointment.routePosition>>> AppointmentsInRoute;
  public PXAction<FSRouteDocument> closeRoute;
  public PXAction<FSRouteDocument> uncloseRoute;
  public PXAction<FSRouteDocument> openAppointment;
  public PXDBAction<FSRouteDocument> openCustomerLocation;
  public PXAction<FSRouteDocument> OpenRouteSchedule;
  public PXAction<FSRouteDocument> OpenRouteContract;

  public RouteClosingMaint()
  {
    PXUIFieldAttribute.SetDisplayName<FSServiceOrder.locationID>(((PXSelectBase) this.ServiceOrder).Cache, "Customer Location");
    PXUIFieldAttribute.SetDisplayName<FSAppointment.estimatedDurationTotal>(((PXSelectBase) this.Appointment).Cache, "Estimated Duration");
    PXGraph.FieldUpdatingEvents fieldUpdating1 = ((PXGraph) this).FieldUpdating;
    System.Type type1 = typeof (FSRouteDocument);
    string str1 = typeof (FSRouteDocument.actualStartTime).Name + "_Time";
    RouteClosingMaint routeClosingMaint1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating1 = new PXFieldUpdating((object) routeClosingMaint1, __vmethodptr(routeClosingMaint1, FSRouteDocument_ActualStartTime_FieldUpdating));
    fieldUpdating1.AddHandler(type1, str1, pxFieldUpdating1);
    PXGraph.FieldUpdatingEvents fieldUpdating2 = ((PXGraph) this).FieldUpdating;
    System.Type type2 = typeof (FSRouteDocument);
    string str2 = typeof (FSRouteDocument.actualEndTime).Name + "_Time";
    RouteClosingMaint routeClosingMaint2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating2 = new PXFieldUpdating((object) routeClosingMaint2, __vmethodptr(routeClosingMaint2, FSRouteDocument_ActualEndTime_FieldUpdating));
    fieldUpdating2.AddHandler(type2, str2, pxFieldUpdating2);
    ((PXSelectBase) this.AppointmentsInRoute).Cache.AllowUpdate = false;
    ((PXAction) this.Insert).SetVisible(false);
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<FSRouteDocument.refNbr, Where<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>>>>))]
  protected virtual void FSRouteDocument_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXSelector(typeof (Search<FSServiceContract.serviceContractID, Where<FSServiceContract.customerID, Equal<Current<FSServiceOrder.customerID>>>>), SubstituteKey = typeof (FSServiceContract.refNbr))]
  [PXUIField(DisplayName = "Service Contract ID", Enabled = false)]
  protected virtual void FSAppointment_ServiceContractID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXSelector(typeof (Search<FSSchedule.scheduleID, Where<FSSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>, And<FSSchedule.entityID, Equal<Current<FSServiceOrder.serviceContractID>>>>>), SubstituteKey = typeof (FSSchedule.refNbr))]
  [PXUIField(DisplayName = "Schedule ID", Enabled = false)]
  protected virtual void FSAppointment_ScheduleID_CacheAttached(PXCache sender)
  {
  }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Actual Start Time")]
  [PXDefault]
  [PXUIField(DisplayName = "Actual Start Time")]
  protected virtual void FSRouteDocument_ActualStartTime_CacheAttached(PXCache sender)
  {
  }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Actual End Time")]
  [PXDefault]
  [PXUIField(DisplayName = "Actual End Time")]
  protected virtual void FSRouteDocument_ActualEndTime_CacheAttached(PXCache sender)
  {
  }

  [PXUIField]
  [PXButton(Category = "Processing")]
  public virtual IEnumerable CloseRoute(PXAdapter adapter)
  {
    FSRouteDocument current = ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Current;
    if (current != null && (this.AutomaticallyCloseRoute || 6 == ((PXSelectBase<FSRouteDocument>) this.RouteRecords).Ask("Confirm Route Closing", "The current route will be closed. Do you want to proceed?", (MessageButtons) 4)))
    {
      if (PXSelectBase<FSAppointment, PXSelectReadonly2<FSAppointment, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSAppointment.srvOrdType>>, InnerJoin<FSAppointmentDet, On<FSAppointmentDet.appointmentID, Equal<FSAppointment.appointmentID>>, LeftJoin<FSPostInfo, On<FSPostInfo.postID, Equal<FSAppointmentDet.postID>>>>>, Where<FSAppointmentDet.lineType, Equal<ListField_LineType_Pickup_Delivery.Pickup_Delivery>, And<FSAppointment.routeDocumentID, Equal<Current<FSRouteDocument.routeDocumentID>>, And<FSAppointment.canceled, Equal<False>, And<FSSrvOrdType.enableINPosting, Equal<True>, And<Where<FSPostInfo.postID, IsNull, Or<FSPostInfo.iNPosted, Equal<False>>>>>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) current.RouteDocumentID
      }).Count != 0)
        throw new PXException("The route execution cannot be closed because at least one appointment has not been posted to the Inventory module.");
      string empty = string.Empty;
      if (!this.CloseAppointmentsInRoute(ref empty))
        throw new PXException(empty);
      ((PXGraph) this).SelectTimeStamp();
      ((PXSelectBase) this.RouteRecords).Cache.AllowUpdate = true;
      current.Status = "Z";
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

  [PXButton]
  [PXUIField]
  public virtual void OpenAppointment()
  {
    if (((PXSelectBase<FSAppointment>) this.AppointmentsInRoute).Current != null && ((PXSelectBase<FSAppointment>) this.AppointmentsInRoute).Current.RouteDocumentID.HasValue)
    {
      AppointmentClosingMaint instance = PXGraph.CreateInstance<AppointmentClosingMaint>();
      ((PXSelectBase<FSAppointment>) instance.ClosingAppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.ClosingAppointmentRecords).Search<FSAppointment.appointmentID>((object) ((PXSelectBase<FSAppointment>) this.AppointmentsInRoute).Current.AppointmentID, new object[2]
      {
        (object) ((PXSelectBase<FSAppointment>) this.AppointmentsInRoute).Current.SrvOrdType,
        (object) ((PXSelectBase<FSAppointment>) this.AppointmentsInRoute).Current.RouteDocumentID
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  [PXUIField]
  public virtual void OpenCustomerLocation()
  {
    LocationHelper.OpenCustomerLocation((PXGraph) this, ((PXSelectBase<FSAppointment>) this.AppointmentsInRoute).Current.SOID);
  }

  [PXUIField]
  protected virtual void openRouteSchedule()
  {
    if (((PXSelectBase<FSAppointment>) this.AppointmentsInRoute).Current != null)
    {
      RouteServiceContractScheduleEntry instance = PXGraph.CreateInstance<RouteServiceContractScheduleEntry>();
      ((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Current = PXResultset<FSRouteContractSchedule>.op_Implicit(((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Search<FSSchedule.scheduleID>((object) ((PXSelectBase<FSAppointment>) this.AppointmentsInRoute).Current.ScheduleID, new object[1]
      {
        (object) ((PXSelectBase<FSAppointment>) this.AppointmentsInRoute).Current.ServiceContractID
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  [PXUIField]
  public virtual void openRouteContract()
  {
    if (((PXSelectBase<FSAppointment>) this.AppointmentsInRoute).Current != null)
    {
      RouteServiceContractEntry instance = PXGraph.CreateInstance<RouteServiceContractEntry>();
      FSServiceContract fsServiceContract = PXResultset<FSServiceContract>.op_Implicit(PXSelectBase<FSServiceContract, PXSelectJoin<FSServiceContract, InnerJoin<FSSchedule, On<FSSchedule.entityID, Equal<FSServiceContract.serviceContractID>, And<FSSchedule.customerID, Equal<FSServiceContract.customerID>>>>, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<FSAppointment>) this.AppointmentsInRoute).Current.ScheduleID
      }));
      ((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Current = fsServiceContract;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  /// <summary>
  /// Enables/Disables the actions defined for Route Closing.
  /// </summary>
  public virtual void EnableDisable_ActionButtons(PXCache cache, FSRouteDocument fsRouteDocumentRow)
  {
    ((PXAction) this.closeRoute).SetEnabled(fsRouteDocumentRow.Status != "Z");
    ((PXAction) this.uncloseRoute).SetEnabled(fsRouteDocumentRow.Status == "Z");
  }

  /// <summary>Enable/Disable additional info fields.</summary>
  public virtual void EnableDisable_AdditionalInfoFields(
    PXCache cache,
    FSRouteDocument fsRouteDocumentRow)
  {
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.miles>(cache, (object) fsRouteDocumentRow, fsRouteDocumentRow.Status != "Z");
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.weight>(cache, (object) fsRouteDocumentRow, fsRouteDocumentRow.Status != "Z");
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.fuelQty>(cache, (object) fsRouteDocumentRow, fsRouteDocumentRow.Status != "Z");
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.fuelType>(cache, (object) fsRouteDocumentRow, fsRouteDocumentRow.Status != "Z");
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.oil>(cache, (object) fsRouteDocumentRow, fsRouteDocumentRow.Status != "Z");
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.antiFreeze>(cache, (object) fsRouteDocumentRow, fsRouteDocumentRow.Status != "Z");
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.dEF>(cache, (object) fsRouteDocumentRow, fsRouteDocumentRow.Status != "Z");
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.propane>(cache, (object) fsRouteDocumentRow, fsRouteDocumentRow.Status != "Z");
  }

  /// <summary>
  /// Closes all appointments belonging to the current Route, in case an error occurs with any appointment,
  /// the route will not be closed and a message will be displayed alerting the user about the appointment's issue.
  /// The row of the appointment having problems is marked with its error.
  /// </summary>
  /// <param name="errorMessage">Error message to be displayed.</param>
  /// <returns>True in case all appointments are closed, otherwise False.</returns>
  public virtual bool CloseAppointmentsInRoute(ref string errorMessage)
  {
    bool flag = true;
    PXResultset<FSAppointment> bqlResultSet = ((PXSelectBase<FSAppointment>) this.AppointmentsInRoute).Select(Array.Empty<object>());
    if (bqlResultSet.Count > 0)
    {
      Dictionary<FSAppointment, string> dictionary = this.CloseAppointments(bqlResultSet);
      if (dictionary.Count > 0)
      {
        foreach (KeyValuePair<FSAppointment, string> keyValuePair in dictionary)
          ((PXSelectBase) this.AppointmentsInRoute).Cache.RaiseExceptionHandling<FSAppointment.refNbr>((object) keyValuePair.Key, (object) keyValuePair.Key.RefNbr, (Exception) new PXSetPropertyException(keyValuePair.Value, (PXErrorLevel) 5));
        flag = false;
        errorMessage = PXMessages.LocalizeFormatNoPrefix("The route execution cannot be closed because some appointments have issues. See details below.", Array.Empty<object>());
      }
    }
    return flag;
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

  public virtual Dictionary<FSAppointment, string> CloseAppointments(
    PXResultset<FSAppointment> bqlResultSet)
  {
    return ServiceOrderEntry.CloseAppointmentsInt(bqlResultSet);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSRouteDocument, FSRouteDocument.actualStartTime> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSRouteDocument, FSRouteDocument.actualStartTime>, FSRouteDocument, object>) e).NewValue = (object) PXDBDateAndTimeAttribute.CombineDateTime(new DateTime?(((PXGraph) this).Accessinfo.BusinessDate.Value), new DateTime?(PXTimeZoneInfo.Now));
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSRouteDocument, FSRouteDocument.actualEndTime> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSRouteDocument, FSRouteDocument.actualEndTime>, FSRouteDocument, object>) e).NewValue = (object) PXDBDateAndTimeAttribute.CombineDateTime(new DateTime?(((PXGraph) this).Accessinfo.BusinessDate.Value), new DateTime?(PXTimeZoneInfo.Now));
  }

  protected virtual void FSRouteDocument_ActualStartTime_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null)
      return;
    FSRouteDocument row = (FSRouteDocument) e.Row;
    DateTime? handlingDateTime = SharedFunctions.TryParseHandlingDateTime(cache, e.NewValue);
    if (!handlingDateTime.HasValue)
      return;
    row.ActualStartTime = PXDBDateAndTimeAttribute.CombineDateTime(((PXGraph) this).Accessinfo.BusinessDate, handlingDateTime);
  }

  protected virtual void FSRouteDocument_ActualEndTime_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null)
      return;
    FSRouteDocument row = (FSRouteDocument) e.Row;
    DateTime? handlingDateTime = SharedFunctions.TryParseHandlingDateTime(cache, e.NewValue);
    if (!handlingDateTime.HasValue)
      return;
    row.ActualEndTime = PXDBDateAndTimeAttribute.CombineDateTime(((PXGraph) this).Accessinfo.BusinessDate, handlingDateTime);
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
    this.EnableDisable_ActionButtons(cache, row);
    this.EnableDisable_AdditionalInfoFields(cache, row);
    SharedFunctions.CheckRouteActualDateTimes(cache, row, ((PXGraph) this).Accessinfo.BusinessDate);
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.routeID>(cache, (object) row, false);
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
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSRouteDocument> e)
  {
  }
}
