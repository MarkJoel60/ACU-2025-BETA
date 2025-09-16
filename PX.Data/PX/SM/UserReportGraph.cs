// Decompiled with JetBrains decompiler
// Type: PX.SM.UserReportGraph
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Reports;
using PX.Metadata;
using PX.Reports;

#nullable disable
namespace PX.SM;

public class UserReportGraph : PXGraph<UserReportGraph, UserReport>
{
  public PXSelect<UserReport, Where<UserReport.reportFileName, Equal<Required<UserReport.reportFileName>>>> ViewReports;
  private bool _manualDeactivation;

  [InjectDependency]
  protected IScreenInfoCacheControl ScreenInfoCacheControl { get; set; }

  protected void EventHandler(Events.RowDeleting<UserReport> e)
  {
    UserReport row = e.Row;
    if (row.IsActive.Value && string.IsNullOrEmpty(ReportFileManager.GetReportPathByName(row.ReportFileName)))
      throw new PXException("The active version cannot be deleted because there's no report file found on disk.");
  }

  protected void EventHandler(Events.FieldVerifying<UserReport.isActive> e)
  {
    if (this._manualDeactivation)
      return;
    UserReport row = e.Row as UserReport;
    if (row.IsActive.Value && !(bool) e.NewValue && string.IsNullOrEmpty(ReportFileManager.GetReportPathByName(row.ReportFileName)))
      throw new PXException("The active version cannot be disabled because there's no report file found on disk.");
  }

  protected void EventHandler(Events.FieldUpdated<UserReport.isActive> e)
  {
    UserReport row = (UserReport) e.Row;
    if (row == null)
      return;
    bool? isActive = row.IsActive;
    bool flag1 = true;
    if (!(isActive.GetValueOrDefault() == flag1 & isActive.HasValue))
      return;
    try
    {
      this._manualDeactivation = true;
      foreach (PXResult<UserReport> pxResult in this.ViewReports.Select((object) row.ReportFileName))
      {
        UserReport userReport = (UserReport) pxResult;
        int? version1 = userReport.Version;
        int? version2 = row.Version;
        if (!(version1.GetValueOrDefault() == version2.GetValueOrDefault() & version1.HasValue == version2.HasValue))
        {
          isActive = userReport.IsActive;
          bool flag2 = true;
          if (isActive.GetValueOrDefault() == flag2 & isActive.HasValue)
          {
            UserReport copy = (UserReport) this.ViewReports.Cache.CreateCopy((object) userReport);
            copy.IsActive = new bool?(false);
            this.ViewReports.Update(copy);
          }
        }
      }
    }
    finally
    {
      this._manualDeactivation = false;
    }
    string screenId = ReportStorageHelper.GetScreenId(row.ReportFileName);
    if (!string.IsNullOrEmpty(screenId))
      this.ScreenInfoCacheControl.InvalidateCache(screenId);
    this.ViewReports.View.RequestRefresh();
  }
}
