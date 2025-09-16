// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.SM.SendRecurringNotifications.SMNotificationMaintExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Data.Maintenance.SM.SendRecurringNotifications;

public class SMNotificationMaintExtension : PXGraphExtension<SMNotificationMaint>
{
  public PXSelectJoin<NotificationSchedule, InnerJoin<AUSchedule, On<NotificationSchedule.scheduleID, Equal<AUSchedule.scheduleID>>>, Where<NotificationSchedule.notificationID, Equal<Current<Notification.notificationID>>>> Schedules;
  public PXSelectJoin<AUScheduleHistory, InnerJoin<Notification, On<AUScheduleHistory.refNoteID, Equal<Notification.noteID>>>, Where<AUScheduleHistory.scheduleID, Equal<Current<NotificationSchedule.scheduleID>>>, OrderBy<Desc<AUScheduleHistory.executionDate>>> ScheduleHistory;
  public PXAction<Notification> CreateSchedule;
  public PXAction<Notification> ViewScheduleHistory;
  public PXAction<Notification> ViewSchedule;

  public IEnumerable schedules()
  {
    this.AssignScheduleIds();
    return this.Schedules.View.QuickSelect();
  }

  [PXUIField(DisplayName = "Create Schedule")]
  [PXButton(OnClosingPopup = PXSpecialButtonType.Refresh)]
  public virtual void createSchedule()
  {
    AUScheduleMaint instance = PXGraph.CreateInstance<AUScheduleMaint>();
    AUSchedule auSchedule1 = instance.Schedule.Insert();
    auSchedule1.Action = "SendEmailNotification";
    AUSchedule auSchedule2 = instance.Schedule.Update(auSchedule1);
    NotificationSchedule notificationSchedule = new NotificationSchedule()
    {
      NotificationID = this.Base.Notifications.Current.NotificationID,
      ScheduleID = auSchedule2.ScheduleID
    };
    if (this.Base.Notifications.Cache.GetStatus((object) this.Base.Notifications.Current) == PXEntryStatus.Inserted)
    {
      notificationSchedule.ScheduleNoteID = auSchedule2.NoteID;
      this.Schedules.Insert(notificationSchedule);
    }
    else
      instance.GetExtension<AUScheduleMaintExtension>().EmailNotifications.Insert(notificationSchedule);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, (string) null);
    requiredException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
    throw requiredException;
  }

  [PXUIField(DisplayName = "View Schedule History")]
  [PXButton]
  public virtual void viewScheduleHistory()
  {
    int num = (int) this.ScheduleHistory.AskExt(true);
  }

  [PXUIField(DisplayName = "View Schedule", Visible = false)]
  [PXButton]
  public virtual void viewSchedule()
  {
    if (this.Schedules.Current != null)
    {
      AUScheduleMaint instance = PXGraph.CreateInstance<AUScheduleMaint>();
      instance.Schedule.Current = (AUSchedule) instance.Schedule.Search<AUSchedule.scheduleID>((object) this.Schedules.Current.ScheduleID);
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, (string) null);
      requiredException.Mode = PXBaseRedirectException.WindowMode.New;
      throw requiredException;
    }
  }

  protected virtual void _(Events.RowSelected<NotificationReport> e)
  {
    if (e.Row == null || e.Cache.GetStatus((object) e.Row) == PXEntryStatus.Inserted)
      return;
    PXUIFieldAttribute.SetEnabled<NotificationReport.screenID>(e.Cache, (object) e.Row, false);
  }

  protected virtual void _(Events.RowSelected<Notification> e, PXRowSelected baseEvent)
  {
    baseEvent(e.Cache, e.Args);
    PXUIFieldAttribute.SetWarning<NotificationSchedule.isActive>(this.Schedules.Cache, (object) null, string.IsNullOrEmpty(e.Row?.ScreenID) || this.Schedules.Select().Count <= 0 ? (string) null : "If this email template is sent by schedules, the system will not be able to retrieve values for the screen fields and actions. As a result, the generated emails and attached reports will contain empty placeholders, if any. Consider reviewing the email content to avoid confusion due to missing placeholder values.");
  }

  private void AssignScheduleIds()
  {
    foreach (NotificationSchedule notificationSchedule in this.Schedules.Cache.Inserted.OfType<NotificationSchedule>().Where<NotificationSchedule>((Func<NotificationSchedule, bool>) (ns => ns.ScheduleNoteID.HasValue)).ToArray<NotificationSchedule>())
    {
      NotificationSchedule row = notificationSchedule;
      AUSchedule auSchedule = PXDatabase.Select<AUSchedule>().Where<AUSchedule>((Expression<Func<AUSchedule, bool>>) (s => s.NoteID == row.ScheduleNoteID)).FirstOrDefault<AUSchedule>();
      if (auSchedule != null)
      {
        this.Schedules.Cache.Remove((object) row);
        row.ScheduleID = auSchedule.ScheduleID;
        row.ScheduleNoteID = new Guid?();
        row.IsActive = auSchedule.IsActive;
        this.Schedules.Cache.Insert((object) row);
      }
    }
  }

  protected virtual void _(
    Events.FieldSelecting<AUScheduleHistory.scheduleID> e)
  {
    if (!(e.Row is AUScheduleHistory row))
      return;
    PXErrorLevel valueOrDefault = (PXErrorLevel) row.ErrorLevel.GetValueOrDefault();
    Events.FieldSelecting<AUScheduleHistory.scheduleID> fieldSelecting = e;
    object returnValue = e.ReturnValue;
    string executionResult = row.ExecutionResult;
    PXErrorLevel pxErrorLevel = valueOrDefault;
    bool? isKey = new bool?();
    bool? nullable = new bool?();
    int? required = new int?();
    int? precision = new int?();
    int? length = new int?();
    string error = executionResult;
    int errorLevel = (int) pxErrorLevel;
    bool? enabled = new bool?();
    bool? visible = new bool?();
    bool? readOnly = new bool?();
    PXFieldState instance = PXFieldState.CreateInstance(returnValue, (System.Type) null, isKey, nullable, required, precision, length, error: error, errorLevel: (PXErrorLevel) errorLevel, enabled: enabled, visible: visible, readOnly: readOnly);
    fieldSelecting.ReturnState = (object) instance;
  }

  protected virtual void _(Events.RowPersisting<Notification> e)
  {
    if (e.Row == null)
      return;
    NotificationExtension extension = e.Cache.GetExtension<NotificationExtension>((object) e.Row);
    int num;
    if (extension == null)
    {
      num = 1;
    }
    else
    {
      bool? createdFromReport = extension.CreatedFromReport;
      bool flag = true;
      num = !(createdFromReport.GetValueOrDefault() == flag & createdFromReport.HasValue) ? 1 : 0;
    }
    if (num == 0 && this.Schedules.SelectSingle() == null)
      throw new PXException("No automation schedule is linked to trigger this email notification. Create an automation schedule on the Send by Schedule tab.");
  }

  [PXOverride]
  public bool PrePersist(Func<bool> baseMethod)
  {
    if (!baseMethod())
      return false;
    this.RemoveStaleSchedules();
    return true;
  }

  private void RemoveStaleSchedules()
  {
    foreach (object obj in this.Schedules.Cache.Inserted.OfType<NotificationSchedule>().Where<NotificationSchedule>((Func<NotificationSchedule, bool>) (ns => ns.ScheduleNoteID.HasValue)).ToArray<NotificationSchedule>())
      this.Schedules.Cache.Remove(obj);
  }
}
