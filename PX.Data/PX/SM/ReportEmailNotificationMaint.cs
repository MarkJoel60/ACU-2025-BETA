// Decompiled with JetBrains decompiler
// Type: PX.SM.ReportEmailNotificationMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Reports;
using System;
using System.Collections;

#nullable disable
namespace PX.SM;

public class ReportEmailNotificationMaint : PXGraph<ReportEmailNotificationMaint>
{
  public PXSelectJoin<Notification, InnerJoin<NotificationReport, On<Notification.notificationID, Equal<NotificationReport.notificationID>>, LeftJoin<PX.Data.Reports.DAC.ReportSettings, On<PXSettingProvider.ReportSettings.settingsID, Equal<NotificationReport.reportTemplateID>>>>, Where<NotificationReport.screenID, Equal<Required<NotificationReport.screenID>>>> ReportNotifications;
  public PXAction<Notification> ViewNotification;

  [PXUIField(DisplayName = "View Notification")]
  [PXButton]
  public void viewNotification()
  {
    Notification current = this.ReportNotifications.Current;
    if (current != null)
    {
      SMNotificationMaint instance = PXGraph.CreateInstance<SMNotificationMaint>();
      instance.Notifications.Current = current;
      throw new PXRedirectRequiredException((PXGraph) instance, true, (string) null);
    }
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Email Template")]
  protected void _(Events.CacheAttached<Notification.name> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Recipients")]
  protected void _(Events.CacheAttached<Notification.nto> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Screen ID")]
  protected void _(Events.CacheAttached<Notification.screenID> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Report Template Owner")]
  protected void _(Events.CacheAttached<PX.Data.Reports.DAC.ReportSettings.username> e)
  {
  }

  public override IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    if (viewName.Equals("ReportNotifications", StringComparison.OrdinalIgnoreCase) && parameters == null)
      parameters = new object[1]
      {
        (object) PXSiteMap.CurrentScreenID
      };
    return base.ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }
}
