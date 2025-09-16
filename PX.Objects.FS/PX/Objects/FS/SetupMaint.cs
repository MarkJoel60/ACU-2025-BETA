// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SetupMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using System;

#nullable disable
namespace PX.Objects.FS;

public class SetupMaint : PXGraph<SetupMaint>
{
  public PXSave<FSSetup> Save;
  public PXCancel<FSSetup> Cancel;
  public PXSelect<FSSetup> SetupRecord;
  public CRNotificationSetupList<FSNotification> Notifications;
  public PXSelect<NotificationSetupRecipient, Where<NotificationSetupRecipient.setupID, Equal<Current<FSNotification.setupID>>>> Recipients;
  public PXSelect<FSSrvOrdType, Where<FSSrvOrdType.requireRoom, Equal<True>>> SrvOrdTypeRequireRoomRecords;

  public SetupMaint()
  {
    PXGraph.FieldUpdatingEvents fieldUpdating1 = ((PXGraph) this).FieldUpdating;
    System.Type type1 = typeof (FSSetup);
    string str1 = typeof (FSSetup.dfltCalendarStartTime).Name + "_Time";
    SetupMaint setupMaint1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating1 = new PXFieldUpdating((object) setupMaint1, __vmethodptr(setupMaint1, FSSetup_DfltCalendarStartTime_Time_FieldUpdating));
    fieldUpdating1.AddHandler(type1, str1, pxFieldUpdating1);
    PXGraph.FieldUpdatingEvents fieldUpdating2 = ((PXGraph) this).FieldUpdating;
    System.Type type2 = typeof (FSSetup);
    string str2 = typeof (FSSetup.dfltCalendarEndTime).Name + "_Time";
    SetupMaint setupMaint2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating2 = new PXFieldUpdating((object) setupMaint2, __vmethodptr(setupMaint2, FSSetup_DfltCalendarEndTime_Time_FieldUpdating));
    fieldUpdating2.AddHandler(type2, str2, pxFieldUpdating2);
    PXGraph.FieldUpdatingEvents fieldUpdating3 = ((PXGraph) this).FieldUpdating;
    System.Type type3 = typeof (FSSetup);
    string str3 = typeof (FSSetup.rOLunchBreakStartTimeFrame).Name + "_Time";
    SetupMaint setupMaint3 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating3 = new PXFieldUpdating((object) setupMaint3, __vmethodptr(setupMaint3, FSSetup_ROLunchBreakStartTimeFrame_Time_FieldUpdating));
    fieldUpdating3.AddHandler(type3, str3, pxFieldUpdating3);
    PXGraph.FieldUpdatingEvents fieldUpdating4 = ((PXGraph) this).FieldUpdating;
    System.Type type4 = typeof (FSSetup);
    string str4 = typeof (FSSetup.rOLunchBreakEndTimeFrame).Name + "_Time";
    SetupMaint setupMaint4 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating4 = new PXFieldUpdating((object) setupMaint4, __vmethodptr(setupMaint4, FSSetup_ROLunchBreakEndTimeFrame_Time_FieldUpdating));
    fieldUpdating4.AddHandler(type4, str4, pxFieldUpdating4);
  }

  [PXDBString(10)]
  [PXDefault]
  [ApptContactType.ClassList]
  [PXUIField(DisplayName = "Contact Type")]
  [PXCheckUnique(new System.Type[] {typeof (NotificationSetupRecipient.contactID)}, Where = typeof (Where<NotificationSetupRecipient.setupID, Equal<Current<NotificationSetupRecipient.setupID>>>))]
  public virtual void NotificationSetupRecipient_ContactType_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Contact ID")]
  [PXNotificationContactSelector(typeof (NotificationSetupRecipient.contactType), typeof (Search2<PX.Objects.CR.Contact.contactID, LeftJoin<EPEmployee, On<EPEmployee.parentBAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<EPEmployee.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>, Where<Current<NotificationSetupRecipient.contactType>, Equal<NotificationContactType.employee>, And<EPEmployee.acctCD, IsNotNull>>>))]
  public virtual void NotificationSetupRecipient_ContactID_CacheAttached(PXCache sender)
  {
  }

  /// <summary>
  /// Updates <c>FSSrvOrdType.createTimeActivitiesFromAppointment</c> when the Time Card integration is enabled.
  /// </summary>
  /// <param name="graph">PXGraph instance.</param>
  /// <param name="enableEmpTimeCardIntegration">Flag that says whether the TimeCard integration is enabled or not.</param>
  public virtual void Update_SrvOrdType_TimeActivitiesFromAppointment(
    PXGraph graph,
    bool? enableEmpTimeCardIntegration)
  {
    if (!enableEmpTimeCardIntegration.GetValueOrDefault())
      return;
    PXUpdate<Set<FSSrvOrdType.createTimeActivitiesFromAppointment, True>, FSSrvOrdType>.Update(graph, Array.Empty<object>());
  }

  public virtual void EnableDisable_Document(PXCache cache, FSSetup fsSetupRow)
  {
    PXDefaultAttribute.SetPersistingCheck<FSSetup.contractPostOrderType>(cache, (object) fsSetupRow, (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<FSSetup.dfltContractTermIDARSO>(cache, (object) fsSetupRow, (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetEnabled<FSSetup.rOLunchBreakEndTimeFrame>(cache, (object) fsSetupRow, fsSetupRow.ROLunchBreakDuration.HasValue);
    PXUIFieldAttribute.SetEnabled<FSSetup.rOLunchBreakStartTimeFrame>(cache, (object) fsSetupRow, fsSetupRow.ROLunchBreakDuration.HasValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSetup, FSSetup.rOLunchBreakStartTimeFrame> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSetup, FSSetup.rOLunchBreakStartTimeFrame>, FSSetup, object>) e).NewValue = (object) new DateTime(1900, 1, 1, 12, 0, 0);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSetup, FSSetup.rOLunchBreakEndTimeFrame> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSetup, FSSetup.rOLunchBreakEndTimeFrame>, FSSetup, object>) e).NewValue = (object) new DateTime(1900, 1, 1, 14, 0, 0);
  }

  protected virtual void FSSetup_DfltCalendarStartTime_Time_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    FSSetup row = (FSSetup) e.Row;
    row.DfltCalendarStartTime = SharedFunctions.TryParseHandlingDateTime(cache, e.NewValue);
    DateTime? nullable;
    ref DateTime? local = ref nullable;
    int year = ((PXGraph) this).Accessinfo.BusinessDate.Value.Year;
    DateTime? businessDate = ((PXGraph) this).Accessinfo.BusinessDate;
    int month = businessDate.Value.Month;
    businessDate = ((PXGraph) this).Accessinfo.BusinessDate;
    int day = businessDate.Value.Day;
    DateTime dateTime = new DateTime(year, month, day, 0, 0, 0);
    local = new DateTime?(dateTime);
    row.DfltCalendarStartTime = PXDBDateAndTimeAttribute.CombineDateTime(nullable, row.DfltCalendarStartTime);
  }

  protected virtual void FSSetup_DfltCalendarEndTime_Time_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    FSSetup row = (FSSetup) e.Row;
    row.DfltCalendarEndTime = SharedFunctions.TryParseHandlingDateTime(cache, e.NewValue);
    DateTime? nullable;
    ref DateTime? local = ref nullable;
    int year = ((PXGraph) this).Accessinfo.BusinessDate.Value.Year;
    DateTime? businessDate = ((PXGraph) this).Accessinfo.BusinessDate;
    int month = businessDate.Value.Month;
    businessDate = ((PXGraph) this).Accessinfo.BusinessDate;
    int day = businessDate.Value.Day;
    DateTime dateTime = new DateTime(year, month, day, 0, 0, 0);
    local = new DateTime?(dateTime);
    row.DfltCalendarEndTime = PXDBDateAndTimeAttribute.CombineDateTime(nullable, row.DfltCalendarEndTime);
  }

  protected virtual void FSSetup_ROLunchBreakStartTimeFrame_Time_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    FSSetup row = (FSSetup) e.Row;
    row.ROLunchBreakStartTimeFrame = SharedFunctions.TryParseHandlingDateTime(cache, e.NewValue);
    DateTime? nullable = new DateTime?(new DateTime(1900, 1, 1, 0, 0, 0));
    row.ROLunchBreakStartTimeFrame = PXDBDateAndTimeAttribute.CombineDateTime(nullable, row.ROLunchBreakStartTimeFrame);
  }

  protected virtual void FSSetup_ROLunchBreakEndTimeFrame_Time_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    FSSetup row = (FSSetup) e.Row;
    row.ROLunchBreakEndTimeFrame = SharedFunctions.TryParseHandlingDateTime(cache, e.NewValue);
    DateTime? nullable = new DateTime?(new DateTime(1900, 1, 1, 0, 0, 0));
    row.ROLunchBreakEndTimeFrame = PXDBDateAndTimeAttribute.CombineDateTime(nullable, row.ROLunchBreakEndTimeFrame);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSSetup, FSSetup.rOLunchBreakDuration> e)
  {
    if (e.Row == null)
      return;
    FSSetup row = e.Row;
    DateTime? nullable = row.ROLunchBreakStartTimeFrame;
    if (!nullable.HasValue)
      return;
    nullable = row.ROLunchBreakEndTimeFrame;
    if (!nullable.HasValue)
      return;
    nullable = row.ROLunchBreakEndTimeFrame;
    DateTime dateTime = nullable.Value;
    TimeSpan timeOfDay = dateTime.TimeOfDay;
    double totalMinutes1 = timeOfDay.TotalMinutes;
    nullable = row.ROLunchBreakStartTimeFrame;
    dateTime = nullable.Value;
    timeOfDay = dateTime.TimeOfDay;
    double totalMinutes2 = timeOfDay.TotalMinutes;
    int num = (int) (totalMinutes1 - totalMinutes2);
    int? newValue = (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSSetup, FSSetup.rOLunchBreakDuration>, FSSetup, object>) e).NewValue;
    int valueOrDefault = newValue.GetValueOrDefault();
    if (!(num < valueOrDefault & newValue.HasValue))
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSSetup, FSSetup.rOLunchBreakDuration>>) e).Cache.RaiseExceptionHandling<FSSetup.rOLunchBreakDuration>((object) row, (object) null, (Exception) new PXSetPropertyException("Duration cannot be greater that difference between time frames", (PXErrorLevel) 4));
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSSetup, FSSetup.rOLunchBreakDuration>, FSSetup, object>) e).NewValue = (object) null;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSetup, FSSetup.appAutoConfirmGap> e)
  {
    if (e.Row == null)
      return;
    FSSetup row = e.Row;
    int? appAutoConfirmGap = row.AppAutoConfirmGap;
    int num = 0;
    if (!(appAutoConfirmGap.GetValueOrDefault() < num & appAutoConfirmGap.HasValue))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSetup, FSSetup.appAutoConfirmGap>>) e).Cache.RaiseExceptionHandling<FSSetup.appAutoConfirmGap>((object) row, (object) row.AppAutoConfirmGap, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefix("The minimum value allowed for this field is {0}", new object[1]
    {
      (object) " 00 h 00 m"
    }), (PXErrorLevel) 4));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSetup, FSSetup.manageRooms> e)
  {
    if (e.Row == null)
      return;
    FSSetup row = e.Row;
    bool? manageRooms = row.ManageRooms;
    bool flag = false;
    if (!(manageRooms.GetValueOrDefault() == flag & manageRooms.HasValue))
      return;
    PXResultset<FSSrvOrdType> pxResultset = ((PXSelectBase<FSSrvOrdType>) this.SrvOrdTypeRequireRoomRecords).Select(Array.Empty<object>());
    if (pxResultset == null || pxResultset.Count <= 0)
      return;
    if (((PXSelectBase<FSSrvOrdType>) this.SrvOrdTypeRequireRoomRecords).Ask("Confirm Manage Rooms change", "Currently there is at least one Service Order Type requiring rooms. Turning off this feature, will also disable the rooms requirement for the Service Order Types. Would you like to proceed with this change?", (MessageButtons) 4) == 6)
    {
      SvrOrdTypeMaint instance = PXGraph.CreateInstance<SvrOrdTypeMaint>();
      foreach (PXResult<FSSrvOrdType> pxResult in pxResultset)
      {
        FSSrvOrdType fsSrvOrdType = PXResult<FSSrvOrdType>.op_Implicit(pxResult);
        fsSrvOrdType.RequireRoom = new bool?(false);
        ((PXSelectBase<FSSrvOrdType>) instance.SvrOrdTypeRecords).Update(fsSrvOrdType);
        ((PXAction) instance.Save).Press();
        ((PXGraph) instance).Clear();
      }
    }
    else
      row.ManageRooms = new bool?(true);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSetup, FSSetup.rOLunchBreakStartTimeFrame> e)
  {
    if (e.Row == null)
      return;
    FSSetup row = e.Row;
    DateTime? nullable1 = row.ROLunchBreakStartTimeFrame;
    if (nullable1.HasValue)
    {
      nullable1 = row.ROLunchBreakEndTimeFrame;
      if (!nullable1.HasValue)
      {
        PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSetup, FSSetup.rOLunchBreakStartTimeFrame>>) e).Cache;
        FSSetup fsSetup = row;
        nullable1 = row.ROLunchBreakStartTimeFrame;
        // ISSUE: variable of a boxed type
        __Boxed<DateTime> local = (ValueType) nullable1.Value.AddMinutes((double) row.ROLunchBreakDuration.Value);
        cache.SetValueExt<FSSetup.rOLunchBreakEndTimeFrame>((object) fsSetup, (object) local);
        return;
      }
    }
    nullable1 = row.ROLunchBreakStartTimeFrame;
    if (!nullable1.HasValue)
      return;
    nullable1 = row.ROLunchBreakEndTimeFrame;
    if (!nullable1.HasValue)
      return;
    nullable1 = row.ROLunchBreakEndTimeFrame;
    DateTime dateTime = nullable1.Value;
    TimeSpan timeOfDay = dateTime.TimeOfDay;
    double totalMinutes1 = timeOfDay.TotalMinutes;
    nullable1 = row.ROLunchBreakStartTimeFrame;
    dateTime = nullable1.Value;
    timeOfDay = dateTime.TimeOfDay;
    double totalMinutes2 = timeOfDay.TotalMinutes;
    double num = totalMinutes1 - totalMinutes2;
    int? lunchBreakDuration = row.ROLunchBreakDuration;
    double? nullable2 = lunchBreakDuration.HasValue ? new double?((double) lunchBreakDuration.GetValueOrDefault()) : new double?();
    double valueOrDefault = nullable2.GetValueOrDefault();
    if (!(num < valueOrDefault & nullable2.HasValue))
      return;
    PXCache cache1 = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSetup, FSSetup.rOLunchBreakStartTimeFrame>>) e).Cache;
    FSSetup fsSetup1 = row;
    nullable1 = row.ROLunchBreakStartTimeFrame;
    dateTime = nullable1.Value;
    // ISSUE: variable of a boxed type
    __Boxed<DateTime> local1 = (ValueType) dateTime.AddMinutes((double) row.ROLunchBreakDuration.Value);
    cache1.SetValueExt<FSSetup.rOLunchBreakEndTimeFrame>((object) fsSetup1, (object) local1);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSetup, FSSetup.rOLunchBreakEndTimeFrame> e)
  {
    if (e.Row == null)
      return;
    FSSetup row = e.Row;
    DateTime? nullable1 = row.ROLunchBreakEndTimeFrame;
    if (nullable1.HasValue)
    {
      nullable1 = row.ROLunchBreakStartTimeFrame;
      if (!nullable1.HasValue)
      {
        PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSetup, FSSetup.rOLunchBreakEndTimeFrame>>) e).Cache;
        FSSetup fsSetup = row;
        nullable1 = row.ROLunchBreakEndTimeFrame;
        // ISSUE: variable of a boxed type
        __Boxed<DateTime> local = (ValueType) nullable1.Value.AddMinutes(-(double) row.ROLunchBreakDuration.Value);
        cache.SetValueExt<FSSetup.rOLunchBreakStartTimeFrame>((object) fsSetup, (object) local);
        return;
      }
    }
    nullable1 = row.ROLunchBreakStartTimeFrame;
    if (!nullable1.HasValue)
      return;
    nullable1 = row.ROLunchBreakEndTimeFrame;
    if (!nullable1.HasValue)
      return;
    nullable1 = row.ROLunchBreakEndTimeFrame;
    DateTime dateTime = nullable1.Value;
    TimeSpan timeOfDay = dateTime.TimeOfDay;
    double totalMinutes1 = timeOfDay.TotalMinutes;
    nullable1 = row.ROLunchBreakStartTimeFrame;
    dateTime = nullable1.Value;
    timeOfDay = dateTime.TimeOfDay;
    double totalMinutes2 = timeOfDay.TotalMinutes;
    double num = totalMinutes1 - totalMinutes2;
    int? lunchBreakDuration = row.ROLunchBreakDuration;
    double? nullable2 = lunchBreakDuration.HasValue ? new double?((double) lunchBreakDuration.GetValueOrDefault()) : new double?();
    double valueOrDefault = nullable2.GetValueOrDefault();
    if (!(num < valueOrDefault & nullable2.HasValue))
      return;
    PXCache cache1 = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSetup, FSSetup.rOLunchBreakEndTimeFrame>>) e).Cache;
    FSSetup fsSetup1 = row;
    nullable1 = row.ROLunchBreakEndTimeFrame;
    dateTime = nullable1.Value;
    // ISSUE: variable of a boxed type
    __Boxed<DateTime> local1 = (ValueType) dateTime.AddMinutes(-(double) row.ROLunchBreakDuration.Value);
    cache1.SetValueExt<FSSetup.rOLunchBreakStartTimeFrame>((object) fsSetup1, (object) local1);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSetup, FSSetup.rOLunchBreakDuration> e)
  {
    if (e.Row == null)
      return;
    FSSetup row = e.Row;
    int? lunchBreakDuration = row.ROLunchBreakDuration;
    if (!lunchBreakDuration.HasValue)
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSetup, FSSetup.rOLunchBreakDuration>>) e).Cache.SetValueExt<FSSetup.rOLunchBreakStartTimeFrame>((object) row, (object) null);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSetup, FSSetup.rOLunchBreakDuration>>) e).Cache.SetValueExt<FSSetup.rOLunchBreakEndTimeFrame>((object) row, (object) null);
    }
    else
    {
      lunchBreakDuration = row.ROLunchBreakDuration;
      if (!lunchBreakDuration.HasValue || ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSSetup, FSSetup.rOLunchBreakDuration>, FSSetup, object>) e).OldValue != null)
        return;
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSetup, FSSetup.rOLunchBreakDuration>>) e).Cache.SetDefaultExt<FSSetup.rOLunchBreakStartTimeFrame>((object) row);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSetup, FSSetup.customerMultipleBillingOptions> e)
  {
    if (e.Row == null || e.NewValue == ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSSetup, FSSetup.customerMultipleBillingOptions>, FSSetup, object>) e).OldValue)
      return;
    PXUIFieldAttribute.SetWarning<FSSetup.customerMultipleBillingOptions>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSetup, FSSetup.customerMultipleBillingOptions>>) e).Cache, (object) e.Row, "After modifying this setting, ensure that billing cycles are specified for all customers.");
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSSetup> e)
  {
    if (e.Row == null)
      return;
    FSSetup row = e.Row;
    this.EnableDisable_Document(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSetup>>) e).Cache, row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSSetup> e)
  {
    if (e.Row == null)
      return;
    FSSetup row = e.Row;
    DateTime? nullable = row.DfltCalendarStartTime;
    if (!nullable.HasValue)
      return;
    nullable = row.DfltCalendarEndTime;
    if (!nullable.HasValue)
      return;
    nullable = row.DfltCalendarStartTime;
    DateTime dateTime = nullable.Value;
    TimeSpan timeOfDay1 = dateTime.TimeOfDay;
    nullable = row.DfltCalendarEndTime;
    dateTime = nullable.Value;
    TimeSpan timeOfDay2 = dateTime.TimeOfDay;
    if (!(timeOfDay1 > timeOfDay2))
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException("The day end time cannot be earlier than the day start time. Update the value accordingly.", (PXErrorLevel) 5);
    ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSSetup>>) e).Cache.RaiseExceptionHandling<FSSetup.dfltCalendarEndTime>((object) row, (object) null, (Exception) propertyException);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSSetup> e)
  {
    if (e.Row == null)
      return;
    this.Update_SrvOrdType_TimeActivitiesFromAppointment((PXGraph) this, e.Row.EnableEmpTimeCardIntegration);
  }

  protected void _(
    PX.Data.Events.FieldVerifying<FSSetup.enableEmpTimeCardIntegration> e)
  {
    if (e.Row == null || (bool) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSSetup.enableEmpTimeCardIntegration>, object, object>) e).NewValue)
      return;
    FSSetup row = (FSSetup) e.Row;
    PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) row, "This functionality is currently in use. If you clear this check box, it may cause unexpected results.", (PXErrorLevel) 2);
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSSetup.enableEmpTimeCardIntegration>>) e).Cache.RaiseExceptionHandling<FSSetup.enableEmpTimeCardIntegration>((object) row, (object) null, (Exception) propertyException);
  }
}
