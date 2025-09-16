// Decompiled with JetBrains decompiler
// Type: PX.Data.PXReportRequiredExceptionExt
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Reports;
using PX.Reports.Controls;
using PX.Reports.Web;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

internal static class PXReportRequiredExceptionExt
{
  private static Report PrepareReport(string reportId, PXReportRedirectParameters pars)
  {
    IReportLoaderService instance = ServiceLocator.Current.GetInstance<IReportLoaderService>();
    Report report = instance.LoadReport(reportId, (IPXResultset) null);
    PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(reportId);
    if ((screenIdUnsecure == null ? 0 : (PXSiteMap.IsARmReport(screenIdUnsecure.Url) ? 1 : 0)) != 0)
    {
      ReportCommonSettings reportCommonSettings = new ReportCommonSettings()
      {
        PrintAllPages = true
      };
      PXReportSettings settings = new PXReportSettings(reportId, reportCommonSettings, (ReportMailSettings) null);
      report = instance.RenderARMReport(report, settings, (IDictionary<string, string>) pars.ReportParameters);
    }
    else
      instance.InitDefaultReportParameters(report, (IDictionary<string, string>) pars.ReportParameters);
    return report;
  }

  internal static PXRedirectToUrlException ConvertToDirectUrl(this PXReportRequiredException rrex)
  {
    PXReportsRedirectList reportsRedirectList = rrex.Value as PXReportsRedirectList;
    Report report1 = (Report) null;
    foreach (KeyValuePair<string, object> keyValuePair in (List<KeyValuePair<string, object>>) reportsRedirectList)
    {
      if (report1 == null)
      {
        report1 = PXReportRequiredExceptionExt.PrepareReport(rrex.ReportID, keyValuePair.Value as PXReportRedirectParameters);
      }
      else
      {
        Report report2 = PXReportRequiredExceptionExt.PrepareReport(keyValuePair.Key, keyValuePair.Value as PXReportRedirectParameters);
        report1.SiblingReports.Add(report2);
      }
    }
    string str = Guid.NewGuid().ToString("N");
    WebReport webReport = new WebReport(report1, str);
    ServiceLocator.Current.GetInstance<IReportFacade>().SetReport(str, webReport);
    return new PXRedirectToUrlException($"~/Frames/PX.ReportViewer.axd?InstanceID={str}&OpType=PdfOneshot&Refresh=True", PXBaseRedirectException.WindowMode.New, true, rrex.ReportID);
  }
}
