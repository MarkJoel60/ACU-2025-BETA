// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.SM.SendRecurringNotifications.AUScheduleMaintExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Process;
using PX.Reports;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

#nullable enable
namespace PX.Data.Maintenance.SM.SendRecurringNotifications;

public class AUScheduleMaintExtension : PXGraphExtension<AUScheduleMaint>
{
  internal const string ScreenIdToMigrate = "SM205060";
  public PXSelectJoin<NotificationSchedule, InnerJoin<Notification, On<NotificationSchedule.notificationID, Equal<Notification.notificationID>>>, Where<NotificationSchedule.scheduleID, Equal<Current<AUSchedule.scheduleID>>>> EmailNotifications;
  public PXAction<AUSchedule> ViewNotification;
  public PXAction<AUSchedule> ScheduleMigration;

  [InjectDependency]
  protected SettingsProvider SettingsProvider { get; private set; }

  [InjectDependency]
  private IScheduledJobHandlerProvider ScheduledJobHandlerProvider { get; set; }

  [PXUIField(DisplayName = "View Notification", Visible = false)]
  [PXButton]
  public virtual void viewNotification()
  {
    if (this.EmailNotifications.Current != null)
    {
      SMNotificationMaint instance = PXGraph.CreateInstance<SMNotificationMaint>();
      instance.Notifications.Current = (Notification) instance.Notifications.Search<Notification.notificationID>((object) this.EmailNotifications.Current.NotificationID);
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, (string) null);
      requiredException.Mode = PXBaseRedirectException.WindowMode.New;
      throw requiredException;
    }
  }

  [PXUIField(DisplayName = "Migrate")]
  [PXButton]
  public virtual IEnumerable scheduleMigration(PXAdapter adapter)
  {
    AUSchedule schedule = this.Base.Schedule.Current;
    SettingsProvider settingsProvider = this.SettingsProvider;
    this.Base.LongOperationManager.StartOperation((System.Action<CancellationToken>) (cancellationToken => AUScheduleMaintExtension.ExecuteScheduleMigration(schedule, settingsProvider, cancellationToken)));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Email Template")]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void _(
    Events.CacheAttached<NotificationSchedule.notificationID> e)
  {
  }

  [PXUIField(DisplayName = "Screen Name")]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void _(Events.CacheAttached<PX.SM.SiteMap.title> e)
  {
  }

  [PXUIField(DisplayName = "Screen ID")]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void _(Events.CacheAttached<Notification.screenID> e)
  {
  }

  protected virtual void _(Events.RowSelected<AUSchedule> e, PXRowSelected baseHandler)
  {
    baseHandler(e.Cache, e.Args);
    AUSchedule row = e.Row;
    if (row == null)
      return;
    this.ResetScheduleInitialUIState(e.Cache, row);
    IScheduledJobHandler jobHandler;
    if (this.ScheduledJobHandlerProvider.TryGetHandler(row.Action, out jobHandler))
      jobHandler.ModifyAUScheduleScreen(this.Base, e.Cache, row);
    bool isVisible = "ProcessAll".Equals(row.Action, StringComparison.OrdinalIgnoreCase);
    PXUIFieldAttribute.SetVisible<AUSchedule.actionName>(e.Cache, (object) row, isVisible);
    if (isVisible)
    {
      PXDefaultAttribute.SetPersistingCheck<AUSchedule.screenID>(e.Cache, (object) row, PXPersistingCheck.Null);
      PXDefaultAttribute.SetPersistingCheck<AUSchedule.graphName>(e.Cache, (object) row, PXPersistingCheck.Null);
      PXDefaultAttribute.SetPersistingCheck<AUSchedule.viewName>(e.Cache, (object) row, PXPersistingCheck.Null);
      PXDefaultAttribute.SetPersistingCheck<AUSchedule.tableName>(e.Cache, (object) row, PXPersistingCheck.Null);
      PXDefaultAttribute.SetPersistingCheck<AUSchedule.actionName>(e.Cache, (object) row, PXPersistingCheck.Null);
      PXSiteMapNodeSelectorAttribute.SetRestriction<AUSchedule.screenID>(e.Cache, (object) null, (Func<PX.SM.SiteMap, bool>) (sm => !SiteMapRestrictionsTypes.IsProcessing(sm)));
    }
    else
      PXSiteMapNodeSelectorAttribute.SetRestriction<AUSchedule.screenID>(e.Cache, (object) null, (Func<PX.SM.SiteMap, bool>) (sm => !PXSiteMap.IsGenericInquiry(sm.Url)));
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if ("SM205060".Equals(row.ScreenID, StringComparison.OrdinalIgnoreCase))
    {
      bool? isMigrated = e.Cache.GetExtension<AUScheduleExtension>((object) e.Row).IsMigrated;
      bool flag = true;
      if (!(isMigrated.GetValueOrDefault() == flag & isMigrated.HasValue))
        propertyException = new PXSetPropertyException((IBqlTable) e.Row, "The Send Reports (SM205060) form will be deprecated in the next release. To avoid interruptions in report sending, use the Migrate button in the toolbar to migrate your schedule. This process will create an email template for each report template in the schedule and link it to a new schedule.", PXErrorLevel.RowWarning);
    }
    e.Cache.RaiseExceptionHandling<AUSchedule.scheduleID>((object) e.Row, (object) null, (Exception) propertyException);
    this.ScheduleMigration.SetVisible(propertyException != null);
  }

  private void ResetScheduleInitialUIState(PXCache scheduleCache, AUSchedule schedule)
  {
    scheduleCache.TrimItemAttributes((object) schedule);
    this.Base.viewScreen.SetVisible(true);
    schedule.ShowEmailNotificationsTabExpr = false;
    schedule.ShowConditionsTabExpr = true;
    int? scheduleId = schedule.ScheduleID;
    int num = 0;
    bool isEnabled = scheduleId.GetValueOrDefault() < num & scheduleId.HasValue;
    PXUIFieldAttribute.SetEnabled<AUSchedule.action>(scheduleCache, (object) schedule, isEnabled);
    PXUIFieldAttribute.SetVisible<AUSchedule.screenID>(scheduleCache, (object) schedule, true);
  }

  private static void ExecuteScheduleMigration(
    AUSchedule schedule,
    SettingsProvider settingsProvider,
    CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();
    AUScheduleMaint instance1 = PXGraph.CreateInstance<AUScheduleMaint>();
    instance1.Schedule.Current = schedule;
    IEnumerable<PXFilterRow> filters = AUScheduleMaintExtension.LoadFilters(instance1);
    AUReportProcess instance2 = PXGraph.CreateInstance<AUReportProcess>();
    AUScheduleMaintExtension.FillFilterValues(instance2, instance1);
    IEnumerable<AUReportProcess.ReportSettings> reportSettingses = AUScheduleMaintExtension.SelectReportTemplates(instance2, instance1, filters);
    SMNotificationMaint instance3 = PXGraph.CreateInstance<SMNotificationMaint>();
    List<Notification> notificationList = new List<Notification>();
    foreach (AUReportProcess.ReportSettings reportTemplate in reportSettingses)
    {
      Notification notification = AUScheduleMaintExtension.MigrateReportTemplate(reportTemplate, settingsProvider, instance3, cancellationToken);
      notificationList.Add(notification);
    }
    AUSchedule schedule1 = AUScheduleMaintExtension.MigrateSchedule(schedule, instance1, cancellationToken);
    AUScheduleMaintExtension.LinkNotificationsToSchedule(schedule1, (IEnumerable<Notification>) notificationList, instance1, cancellationToken);
    AUScheduleMaintExtension.FinishMigration(schedule, instance1, cancellationToken);
    instance1.Schedule.Current = schedule1;
    throw new PXRedirectRequiredException((PXGraph) instance1, true, (string) null);
  }

  private static IEnumerable<PXFilterRow> LoadFilters(AUScheduleMaint graph)
  {
    List<PXFilterRow> pxFilterRowList = new List<PXFilterRow>();
    foreach (AUScheduleFilter firstTableItem in graph.Filters.Select().FirstTableItems)
    {
      string str1 = RelativeDatesManager.IsRelativeDatesString(firstTableItem.Value) ? RelativeDatesManager.EvaluateAsString(firstTableItem.Value) : firstTableItem.Value;
      string str2 = RelativeDatesManager.IsRelativeDatesString(firstTableItem.Value2) ? RelativeDatesManager.EvaluateAsString(firstTableItem.Value2) : firstTableItem.Value2;
      string fieldName = firstTableItem.FieldName;
      int? condition1 = firstTableItem.Condition;
      int? nullable1;
      int? nullable2;
      if (!condition1.HasValue)
      {
        nullable1 = new int?();
        nullable2 = nullable1;
      }
      else
        nullable2 = new int?(condition1.GetValueOrDefault() - 1);
      nullable1 = nullable2;
      int condition2 = nullable1.Value;
      string str3 = str1;
      string str4 = str2;
      PXFilterRow pxFilterRow1 = new PXFilterRow(fieldName, (PXCondition) condition2, (object) str3, (object) str4);
      nullable1 = firstTableItem.OpenBrackets;
      pxFilterRow1.OpenBrackets = nullable1.Value;
      nullable1 = firstTableItem.CloseBrackets;
      pxFilterRow1.CloseBrackets = nullable1.Value;
      nullable1 = firstTableItem.Operator;
      int num = 1;
      pxFilterRow1.OrOperator = nullable1.GetValueOrDefault() == num & nullable1.HasValue;
      PXFilterRow pxFilterRow2 = pxFilterRow1;
      pxFilterRowList.Add(pxFilterRow2);
    }
    return (IEnumerable<PXFilterRow>) pxFilterRowList;
  }

  private static void FillFilterValues(
    AUReportProcess reportProcessGraph,
    AUScheduleMaint scheduleMaintGraph)
  {
    if (string.IsNullOrEmpty(scheduleMaintGraph.Schedule.Current.FilterName))
      return;
    PXCache cache = reportProcessGraph.Views[scheduleMaintGraph.Schedule.Current.FilterName].Cache;
    object copy = cache.CreateCopy(cache.Current);
    object current = cache.Current;
    foreach (AUScheduleFill firstTableItem in scheduleMaintGraph.Fills.Select().FirstTableItems)
    {
      object obj = !RelativeDatesManager.IsRelativeDatesString(firstTableItem.Value) ? (scheduleMaintGraph.Fills.Cache.GetStateExt<AUScheduleFill.value>((object) firstTableItem) is PXFieldState stateExt1 ? stateExt1.Value : (object) null) ?? (object) firstTableItem.Value : (object) RelativeDatesManager.EvaluateAsDateTime(firstTableItem.Value);
      if ((cache.GetStateExt(current, firstTableItem.FieldName) is PXFieldState stateExt2 ? (stateExt2.Enabled ? 1 : 0) : 0) != 0)
      {
        cache.SetValueExt(current, firstTableItem.FieldName, obj);
        cache.Update(current);
      }
    }
    cache.RaiseRowUpdated(current, copy);
    cache.RaiseRowSelected(current);
  }

  private static IEnumerable<AUReportProcess.ReportSettings> SelectReportTemplates(
    AUReportProcess reportProcessGraph,
    AUScheduleMaint scheduleMaintGraph,
    IEnumerable<PXFilterRow> filters)
  {
    if (!"Templates".Equals(scheduleMaintGraph.Schedule.Current.ViewName, StringComparison.Ordinal))
      return (IEnumerable<AUReportProcess.ReportSettings>) Array.Empty<AUReportProcess.ReportSettings>();
    reportProcessGraph.Templates.View.RequestFiltersReset();
    int startRow = 0;
    int totalRows = 0;
    return reportProcessGraph.Templates.View.Select((object[]) null, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, filters.ToArray<PXFilterRow>(), ref startRow, 0, ref totalRows).OfType<AUReportProcess.ReportSettings>();
  }

  private static Notification MigrateReportTemplate(
    AUReportProcess.ReportSettings reportTemplate,
    SettingsProvider settingsProvider,
    SMNotificationMaint emailTemplateGraph,
    CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();
    string field0 = string.Join("|", reportTemplate.ScreenID, reportTemplate.Username, reportTemplate.Name);
    Notification notification1 = (Notification) emailTemplateGraph.Notifications.Search<Notification.name>((object) field0);
    if (notification1 != null)
      return notification1;
    bool? isShared = reportTemplate.IsShared;
    bool flag = true;
    if (!(isShared.GetValueOrDefault() == flag & isShared.HasValue))
      PXDatabase.Update<AUReportProcess.ReportSettings>((PXDataFieldParam) new PXDataFieldRestrict<AUReportProcess.ReportSettings.settingsID>((object) reportTemplate.SettingsID), (PXDataFieldParam) new PXDataFieldAssign<AUReportProcess.ReportSettings.isShared>((object) true));
    ReportMailSettings mail = settingsProvider.Read(reportTemplate.SettingsID).Mail;
    Notification notification2 = emailTemplateGraph.Notifications.Insert();
    notification2.Name = field0;
    notification2.NTo = mail.EMail;
    notification2.NCc = mail.Cc;
    notification2.NBcc = mail.Bcc;
    notification2.Subject = mail.Subject;
    Notification notification3 = emailTemplateGraph.Notifications.Update(notification2);
    NotificationReport notificationReport = emailTemplateGraph.NotificationReports.Insert();
    notificationReport.ScreenID = reportTemplate.ScreenID;
    NotificationReportFormat result;
    notificationReport.Format = new byte?(Enum.TryParse<NotificationReportFormat>(mail.Format, out result) ? (byte) result : (byte) 0);
    notificationReport.ReportTemplateID = reportTemplate.SettingsID;
    emailTemplateGraph.NotificationReports.Update(notificationReport);
    emailTemplateGraph.Persist();
    return notification3;
  }

  private static AUSchedule MigrateSchedule(
    AUSchedule schedule,
    AUScheduleMaint scheduleMaintGraph,
    CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();
    AUSchedule auSchedule1 = PXDatabase.Select<AUSchedule>().Where<AUSchedule>((Expression<Func<AUSchedule, bool>>) (s => s.Description == schedule.Description && s.Action == "SendEmailNotification")).FirstOrDefault<AUSchedule>();
    if (auSchedule1 != null)
      return auSchedule1;
    AUSchedule auSchedule2 = scheduleMaintGraph.Schedule.Insert();
    auSchedule2.Action = "SendEmailNotification";
    auSchedule2.Description = schedule.Description;
    auSchedule2.IsActive = schedule.IsActive;
    auSchedule2.StartDate = schedule.StartDate;
    auSchedule2.EndDate = schedule.EndDate;
    auSchedule2.NoEndDate = schedule.NoEndDate;
    auSchedule2.RunLimit = schedule.RunLimit;
    auSchedule2.NoRunLimit = schedule.NoRunLimit;
    auSchedule2.HistoryRetainCount = schedule.HistoryRetainCount;
    auSchedule2.KeepFullHistory = schedule.KeepFullHistory;
    auSchedule2.BranchID = schedule.BranchID;
    auSchedule2.TimeZoneID = schedule.TimeZoneID;
    auSchedule2.ScheduleType = schedule.ScheduleType;
    auSchedule2.DailyFrequency = schedule.DailyFrequency;
    auSchedule2.WeeklyFrequency = schedule.WeeklyFrequency;
    auSchedule2.WeeklyOnDay1 = schedule.WeeklyOnDay1;
    auSchedule2.WeeklyOnDay2 = schedule.WeeklyOnDay2;
    auSchedule2.WeeklyOnDay3 = schedule.WeeklyOnDay3;
    auSchedule2.WeeklyOnDay4 = schedule.WeeklyOnDay4;
    auSchedule2.WeeklyOnDay5 = schedule.WeeklyOnDay5;
    auSchedule2.WeeklyOnDay6 = schedule.WeeklyOnDay6;
    auSchedule2.WeeklyOnDay7 = schedule.WeeklyOnDay7;
    auSchedule2.MonthlyFrequency = schedule.MonthlyFrequency;
    auSchedule2.MonthlyDaySel = schedule.MonthlyDaySel;
    auSchedule2.MonthlyOnDay = schedule.MonthlyOnDay;
    auSchedule2.MonthlyOnWeek = schedule.MonthlyOnWeek;
    auSchedule2.MonthlyOnDayOfWeek = schedule.MonthlyOnDayOfWeek;
    auSchedule2.PeriodFrequency = schedule.PeriodFrequency;
    auSchedule2.PeriodDateSel = schedule.PeriodDateSel;
    auSchedule2.PeriodFixedDay = schedule.PeriodFixedDay;
    auSchedule2.StartTime = schedule.StartTime;
    auSchedule2.EndTime = schedule.EndTime;
    auSchedule2.Interval = schedule.Interval;
    auSchedule2.NextRunDate = schedule.NextRunDate;
    auSchedule2.NextRunTime = schedule.NextRunTime;
    auSchedule2.ExactTime = schedule.ExactTime;
    AUSchedule auSchedule3 = scheduleMaintGraph.Schedule.Update(auSchedule2);
    scheduleMaintGraph.Persist();
    return auSchedule3;
  }

  private static void LinkNotificationsToSchedule(
    AUSchedule schedule,
    IEnumerable<Notification> notifications,
    AUScheduleMaint scheduleMaintGraph,
    CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();
    scheduleMaintGraph.Schedule.Current = schedule;
    PXCache cache = scheduleMaintGraph.GetExtension<AUScheduleMaintExtension>().EmailNotifications.Cache;
    foreach (Notification notification in notifications)
    {
      if (NotificationSchedule.PK.Find((PXGraph) scheduleMaintGraph, notification.NotificationID, schedule.ScheduleID) == null)
      {
        NotificationSchedule notificationSchedule = new NotificationSchedule()
        {
          ScheduleID = schedule.ScheduleID,
          NotificationID = notification.NotificationID
        };
        cache.Insert((object) notificationSchedule);
      }
    }
    scheduleMaintGraph.Persist();
  }

  private static void FinishMigration(
    AUSchedule schedule,
    AUScheduleMaint scheduleMaintGraph,
    CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();
    scheduleMaintGraph.Schedule.Current = schedule;
    string str = $"{PXLocalizer.Localize("Migrated")}|{schedule.Description}";
    schedule.Description = str;
    schedule.IsActive = new bool?(false);
    scheduleMaintGraph.Schedule.Cache.GetExtension<AUScheduleExtension>((object) schedule).IsMigrated = new bool?(true);
    scheduleMaintGraph.Schedule.Update(schedule);
    scheduleMaintGraph.Persist();
  }
}
