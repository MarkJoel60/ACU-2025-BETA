// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.SM.SendRecurringNotifications.RecurringNotificationsSender
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.EP;
using PX.Data.Localization;
using PX.Data.Process;
using PX.Data.Reports;
using PX.Data.Wiki.Parser;
using PX.Reports;
using PX.Reports.Controls;
using PX.Reports.Data;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

#nullable enable
namespace PX.Data.Maintenance.SM.SendRecurringNotifications;

[PXInternalUseOnly]
internal class RecurringNotificationsSender : ScheduledJobBase<Notification>, IScheduledJobHandler
{
  private readonly INotificationSender _notificationSender;
  private readonly ICurrentUserInformationProvider _currentUserInfoProvider;
  private readonly SettingsProvider _settingsProvider;
  private readonly IFullTrustReportLoaderService _reportLoaderService;
  private readonly IReportDataBinder _reportDataBinder;
  private readonly IReportRenderer _reportRenderer;
  internal const string TypeValue = "SendEmailNotification";

  public bool IsActive => true;

  public PXCache GetProcessingEntityCache(AUSchedule schedule, AUScheduleHistory historyItem)
  {
    return PXGraph.CreateInstance<SMNotificationMaint>().Notifications.Cache;
  }

  public void ModifyAUScheduleScreen(
    AUScheduleMaint screen,
    PXCache scheduleCache,
    AUSchedule schedule)
  {
    PXUIFieldAttribute.SetVisible<AUSchedule.screenID>(scheduleCache, (object) schedule, false);
    PXDefaultAttribute.SetPersistingCheck<AUSchedule.screenID>(scheduleCache, (object) schedule, PXPersistingCheck.Nothing);
    PXDefaultAttribute.SetPersistingCheck<AUSchedule.graphName>(scheduleCache, (object) schedule, PXPersistingCheck.Nothing);
    PXDefaultAttribute.SetPersistingCheck<AUSchedule.viewName>(scheduleCache, (object) schedule, PXPersistingCheck.Nothing);
    PXDefaultAttribute.SetPersistingCheck<AUSchedule.tableName>(scheduleCache, (object) schedule, PXPersistingCheck.Nothing);
    PXDefaultAttribute.SetPersistingCheck<AUSchedule.actionName>(scheduleCache, (object) schedule, PXPersistingCheck.Nothing);
    schedule.ShowEmailNotificationsTabExpr = true;
    schedule.ShowConditionsTabExpr = false;
    screen.viewScreen.SetVisible(false);
  }

  public string Type => "SendEmailNotification";

  public string Description => "Send Email Notification";

  public RecurringNotificationsSender(
    IScheduleAdjustmentRuleProvider adjustmentRuleProvider,
    INotificationSender notificationSender,
    ICurrentUserInformationProvider currentUserInfoProvider,
    SettingsProvider settingsProvider,
    IFullTrustReportLoaderService reportLoaderService,
    IReportDataBinder reportDataBinder,
    IReportRenderer reportRenderer)
    : base(adjustmentRuleProvider)
  {
    this._notificationSender = notificationSender;
    this._currentUserInfoProvider = currentUserInfoProvider;
    this._settingsProvider = settingsProvider;
    this._reportLoaderService = reportLoaderService;
    this._reportDataBinder = reportDataBinder;
    this._reportRenderer = reportRenderer;
  }

  protected override List<Notification> GetListToProcess(
    PXGraph screen,
    List<PXFilterRow> filters,
    string viewName,
    AUSchedule schedule,
    string? screenId)
  {
    return PXDatabase.Select<Notification>().Join((IEnumerable<NotificationSchedule>) PXDatabase.Select<NotificationSchedule>(), (Expression<Func<Notification, int?>>) (n => n.NotificationID), (Expression<Func<NotificationSchedule, int?>>) (s => s.NotificationID), (n, s) => new
    {
      Notification = n,
      NotificationSchedule = s
    }).Where(ns => ns.NotificationSchedule.ScheduleID == schedule.ScheduleID).Select(ns => ns.Notification).ToList<Notification>();
  }

  protected override System.Action<List<Notification>> GetProcessDelegate(
    PXGraph screenGraph,
    List<PXFilterRow> filters,
    string viewName,
    AUSchedule schedule,
    string? screenId,
    List<(Notification Row, PXProcessingMessage? processingResult)> eventResults)
  {
    return (System.Action<List<Notification>>) (notificationsToSend => this.ProcessNotifications(notificationsToSend, eventResults));
  }

  private void ProcessNotifications(
    List<Notification> notifications,
    List<(Notification Row, PXProcessingMessage? processingResult)> eventResults)
  {
    foreach (Notification notification in notifications)
    {
      PXProcessingMessage processingMessage = (PXProcessingMessage) null;
      try
      {
        this.SendNotification(notification);
      }
      catch (Exception ex)
      {
        PXTrace.WithStack().Information<int?>("Processing of email template {NotificationID} failed", notification.NotificationID);
        processingMessage = new PXProcessingMessage(PXErrorLevel.RowError, ex.Message);
      }
      eventResults.Add((notification, processingMessage));
    }
  }

  public void SendNotification(
  #nullable disable
  Notification notification)
  {
    using (new PXLocaleScope(notification.LocaleName))
    {
      if (!string.IsNullOrEmpty(notification.LocaleName))
        LocalesFormatProvider.ApplyLocaleFormat(CultureInfo.CurrentCulture, new Guid?(this._currentUserInfoProvider.GetUserIdOrDefault()));
      PXGraph graph = NotificationHelper.GetGraph();
      Dictionary<NotificationReport, IReadOnlyList<NotificationReportParameter>> reportsWithParameters = this.GetReportsWithParameters(graph, notification.NotificationID.Value);
      int? notificationId = notification.NotificationID;
      int? nfrom = notification.NFrom;
      string validAddresses1 = NotificationHelper.GetValidAddresses(notification.NTo, (Tuple<IDictionary<string, object>, IDictionary<string, object>>[]) null, (graph, (string) null), (Dictionary<string, AUWorkflowFormField[]>) null);
      string validAddresses2 = NotificationHelper.GetValidAddresses(notification.NCc, (Tuple<IDictionary<string, object>, IDictionary<string, object>>[]) null, (graph, (string) null), (Dictionary<string, AUWorkflowFormField[]>) null);
      string validAddresses3 = NotificationHelper.GetValidAddresses(notification.NBcc, (Tuple<IDictionary<string, object>, IDictionary<string, object>>[]) null, (graph, (string) null), (Dictionary<string, AUWorkflowFormField[]>) null);
      string str = PXTemplateContentParser.Instance.Process(notification.Subject, (Tuple<IDictionary<string, object>, IDictionary<string, object>>[]) null, graph, (string) null);
      string body = this.GetBody(notification, graph, reportsWithParameters);
      IReadOnlyCollection<Tuple<string, byte[]>> attachments = this.GetAttachments(notification.LocaleName, reportsWithParameters);
      Guid[] fileNotes = PXNoteAttribute.GetFileNotes(graph.Caches[typeof (Notification)], (object) notification);
      this._notificationSender.Notify(new EmailNotificationParameters()
      {
        NotificationID = notificationId,
        EmailAccountID = nfrom,
        To = validAddresses1,
        Cc = validAddresses2,
        Bcc = validAddresses3,
        Subject = str,
        Body = body,
        Attachments = attachments,
        AttachmentLinks = (IReadOnlyCollection<Guid>) fileNotes
      });
    }
  }

  private string GetBody(
    Notification notification,
    PXGraph graph,
    Dictionary<NotificationReport, IReadOnlyList<NotificationReportParameter>> reportsWithParameters)
  {
    return PXTemplateContentParser.ScriptInstance.Process(notification.Body, (Tuple<IDictionary<string, object>, IDictionary<string, object>>[]) null, graph, (string) null) + string.Join(" ", this.GetAttachmentsEmbedded(reportsWithParameters, notification.LocaleName).ToArray<string>());
  }

  private IList<string> GetAttachmentsEmbedded(
    Dictionary<NotificationReport, IReadOnlyList<NotificationReportParameter>> reportsWithParameters,
    string localeName)
  {
    List<string> attachmentsEmbedded = new List<string>();
    string str = "";
    Report report1 = (Report) null;
    string format = (string) null;
    foreach (NotificationReport notificationReport in reportsWithParameters.Keys.Where<NotificationReport>((Func<NotificationReport, bool>) (r =>
    {
      bool? embedded = r.Embedded;
      bool flag = true;
      return embedded.GetValueOrDefault() == flag & embedded.HasValue;
    })))
    {
      IReadOnlyList<NotificationReportParameter> notificationReportParameters = (IReadOnlyList<NotificationReportParameter>) ((object) reportsWithParameters[notificationReport] ?? (object) Array.Empty<NotificationReportParameter>());
      if (report1 == null)
      {
        report1 = this.RenderReport(notificationReport, notificationReportParameters, localeName);
        format = ((NotificationReportFormat) notificationReport.Format.Value).ToString();
      }
      else
      {
        Report report2 = this.RenderReport(notificationReport, notificationReportParameters, localeName);
        ((SoapNavigator) report2.DataSource).AddSibling(((ReportItem) report2).Name, ((SoapNavigator) report2.DataSource).Incoming);
        report1.SiblingReports.Add(report2);
      }
    }
    if (report1 != null)
    {
      StreamManager streamManager = this._reportRenderer.RenderReport(this._reportDataBinder.ProcessReportDataBinding(report1), format, embedStreams: true);
      str += Encoding.Default.GetString(streamManager.MainStream.GetBytes());
    }
    attachmentsEmbedded.Add(str);
    return (IList<string>) attachmentsEmbedded;
  }

  private IReadOnlyCollection<Tuple<string, byte[]>> GetAttachments(
    string localeName,
    Dictionary<NotificationReport, IReadOnlyList<NotificationReportParameter>> reportsWithParameters)
  {
    List<Tuple<string, byte[]>> attachments1 = new List<Tuple<string, byte[]>>();
    foreach (NotificationReport notificationReport in reportsWithParameters.Keys.Where<NotificationReport>((Func<NotificationReport, bool>) (r =>
    {
      bool? embedded = r.Embedded;
      bool flag = true;
      return !(embedded.GetValueOrDefault() == flag & embedded.HasValue);
    })))
    {
      IReadOnlyList<NotificationReportParameter> notificationReportParameters = (IReadOnlyList<NotificationReportParameter>) ((object) reportsWithParameters[notificationReport] ?? (object) Array.Empty<NotificationReportParameter>());
      Report report = this.RenderReport(notificationReport, notificationReportParameters, localeName);
      ReportNode reportNode = this._reportDataBinder.ProcessReportDataBinding(report);
      string format = ((NotificationReportFormat) notificationReport.Format.Value).ToString();
      IEnumerable<Tuple<string, byte[]>> attachments2 = NotificationHelper.GetAttachments(this._reportRenderer.RenderReport(reportNode, format), reportNode, report.ReportName);
      attachments1.AddRange(attachments2);
    }
    return (IReadOnlyCollection<Tuple<string, byte[]>>) attachments1;
  }

  private Report RenderReport(
    NotificationReport reportData,
    IReadOnlyList<NotificationReportParameter> notificationReportParameters,
    string localeName)
  {
    Dictionary<string, string> dictionary = notificationReportParameters.ToDictionary<NotificationReportParameter, string, string>((Func<NotificationReportParameter, string>) (c => c.Name), (Func<NotificationReportParameter, string>) (e => e.Value));
    string screenId = reportData.ScreenID;
    Guid? reportTemplateId = reportData.ReportTemplateID;
    PXReportSettings settings = !reportTemplateId.HasValue ? (PXReportSettings) null : this._settingsProvider.Read(reportTemplateId);
    PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenId);
    Report report = this._reportLoaderService.LoadReportUnsecure(screenId, (IPXResultset) null);
    report.Locale = localeName;
    report.Localizable = true;
    if ((screenIdUnsecure == null ? 0 : (PXSiteMap.IsARmReport(screenIdUnsecure.Url) ? 1 : 0)) != 0)
    {
      if (settings == null)
      {
        ReportCommonSettings reportCommonSettings = new ReportCommonSettings()
        {
          PrintAllPages = true
        };
        settings = new PXReportSettings(screenId, reportCommonSettings, (ReportMailSettings) null);
      }
      report = this._reportLoaderService.RenderARMReport(report, settings, (IDictionary<string, string>) dictionary, true);
    }
    else if (settings == null)
      this._reportLoaderService.InitDefaultReportParameters(report, (IDictionary<string, string>) dictionary);
    else
      this._reportLoaderService.InitReportParameters(report, (IDictionary<string, string>) dictionary, settings);
    return report;
  }

  private Dictionary<NotificationReport, IReadOnlyList<NotificationReportParameter>> GetReportsWithParameters(
    PXGraph graph,
    int notificationId)
  {
    IEnumerable<NotificationReport> firstTableItems = PXSelectBase<NotificationReport, PXSelect<NotificationReport, Where<NotificationReport.notificationID, Equal<Required<Notification.notificationID>>>>.Config>.Select(graph, (object) notificationId).FirstTableItems;
    PXSelect<NotificationReportParameter, Where<NotificationReportParameter.reportID, Equal<Required<NotificationReport.reportID>>>> pxSelect = new PXSelect<NotificationReportParameter, Where<NotificationReportParameter.reportID, Equal<Required<NotificationReport.reportID>>>>(graph);
    Dictionary<NotificationReport, IReadOnlyList<NotificationReportParameter>> reportsWithParameters = new Dictionary<NotificationReport, IReadOnlyList<NotificationReportParameter>>();
    foreach (NotificationReport key in firstTableItems)
    {
      List<NotificationReportParameter> list = pxSelect.Select((object) key.ReportID).FirstTableItems.ToList<NotificationReportParameter>();
      reportsWithParameters.Add(key, (IReadOnlyList<NotificationReportParameter>) list);
    }
    return reportsWithParameters;
  }
}
