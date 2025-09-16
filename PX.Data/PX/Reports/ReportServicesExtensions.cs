// Decompiled with JetBrains decompiler
// Type: PX.Reports.ReportServicesExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Common.Mail;
using PX.CS;
using PX.Data;
using PX.Data.Reports;
using PX.Reports.ARm;
using PX.Reports.ARm.Data;
using PX.Reports.Controls;
using PX.Reports.Data;
using PX.Reports.Mail;
using System.Collections;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace PX.Reports;

/// <summary>A collection of extensions for report services.</summary>
[PXInternalUseOnly]
public static class ReportServicesExtensions
{
  /// <exception cref="T:PX.Data.PXException">When user has no rights to the screen</exception>
  internal static Report LoadReportAndProcessParameters(
    this IReportLoaderService reportLoaderService,
    string screenID,
    string locale)
  {
    using (new PXLocaleScope(locale))
      return reportLoaderService.LoadReportAndProcessParameters(screenID);
  }

  /// <exception cref="T:PX.Data.PXException">When user has no rights to the screen</exception>
  [PXInternalUseOnly]
  public static Report LoadReportAndProcessParameters(
    this IReportLoaderService reportLoaderService,
    string screenID)
  {
    Report report = reportLoaderService.LoadReport(screenID, (IPXResultset) null);
    report.Parameters.Process(report.DataSource as IDataNavigator, report);
    return report;
  }

  [PXInternalUseOnly]
  public static void InitReportParameters(
    this IReportLoaderService reportLoaderService,
    Report report,
    string screenID,
    ScreenUtils.WebReportSettings settings,
    FilterExpCollection baseFilters)
  {
    reportLoaderService.InitReportParameters(report, (IDictionary<string, string>) new Dictionary<string, string>(), new PXReportSettings(screenID, settings.CommonSettings, settings.ReportMailSettings, settings.Parameters, settings.Sorting, settings.Filters, baseFilters));
    report.MailSettings.Bcc = settings.ReportMailSettings.Bcc;
    report.MailSettings.Cc = settings.ReportMailSettings.Cc;
    report.MailSettings.To = settings.ReportMailSettings.EMail;
    report.MailSettings.Format = settings.ReportMailSettings.Format;
    report.MailSettings.Subject = settings.ReportMailSettings.Subject;
  }

  [PXInternalUseOnly]
  public static Report RenderUniversalReport(
    this IReportLoaderService reportLoaderService,
    Report report,
    string screenID)
  {
    PXReportSettings settings = new PXReportSettings(screenID, report.CommonSettings, ScreenUtils.ConvertMailSettings(report.MailSettings), report.Parameters, new SortExpCollection(((List<SortExp>) report.DynamicSorting).ToArray()), new FilterExpCollection(((List<FilterExp>) report.DynamicFilters).ToArray()), (FilterExpCollection) null);
    return reportLoaderService.RenderUniversalReport(report, settings);
  }

  public static Report RenderUniversalReport(
    this IReportLoaderService reportLoader,
    Report report,
    PXReportSettings settings)
  {
    ExceptionExtensions.ThrowOnNull<IReportLoaderService>(reportLoader, nameof (reportLoader), (string) null);
    string schemaUrl = report.SchemaUrl;
    if (string.IsNullOrEmpty(ReportFileManager.GetReportPathByName(schemaUrl)) && string.IsNullOrEmpty(ReportDbStorage.ReportGetXmlFromDb(schemaUrl)))
    {
      report = reportLoader.RenderARMReport(report, settings);
    }
    else
    {
      if (report.DataSource is IDataNavigator dataSource)
        report.Parameters.Process(dataSource, report);
      reportLoader.InitReportParameters(report, (IDictionary<string, string>) new Dictionary<string, string>(), settings);
    }
    return report;
  }

  internal static Report RenderARMReport(
    this IReportLoaderService reportLoader,
    Report report,
    PXReportSettings settings,
    IDictionary<string, string> reportParams = null,
    bool securityTrimming = false)
  {
    ExceptionExtensions.ThrowOnNull<IReportLoaderService>(reportLoader, nameof (reportLoader), (string) null);
    string schemaUrl = report.SchemaUrl;
    RMReportReader instance = PXGraph.CreateInstance<RMReportReader>();
    if (!PXSiteMap.IsARmReport(schemaUrl))
    {
      instance.ReportCode = string.IsNullOrEmpty(schemaUrl) || schemaUrl.LastIndexOf('.') == -1 ? schemaUrl : schemaUrl.Substring(0, schemaUrl.LastIndexOf('.'));
    }
    else
    {
      string str = ReportSchemaExtractor.ExtractSchemaFromUrl(schemaUrl);
      int length = str != null ? str.IndexOf(".rpx") : 0;
      if (length > 0)
        str = str.Substring(0, length);
      instance.ReportCode = str;
    }
    ARmReport report1 = instance.GetReport();
    if (report1 != null)
    {
      string name = ((ReportItem) report).Name;
      report = ARmProcessor.CreateReport(report1);
      SoapNavigator soapNavigator = new SoapNavigator(new PXGraph(), (IPXResultset) null, securityTrimming);
      report.DataSource = (object) soapNavigator;
      report.ApplyRules((ReportItem) report);
      if (settings != null || reportParams != null)
      {
        reportLoader.InitReportParameters(report, reportParams ?? (IDictionary<string, string>) new Dictionary<string, string>(), settings);
        ARmProcessor.CopyParameters(report, report1);
      }
      ARmReportNode armReportNode = ARmProcessor.ProcessReport((IARmDataSource) instance, report1);
      if (settings != null)
      {
        bool? printAllPages = settings.CommonSettings?.PrintAllPages;
        bool flag = true;
        if (printAllPages.GetValueOrDefault() == flag & printAllPages.HasValue)
          armReportNode.ActiveNode = armReportNode;
      }
      ARmProcessor.Render(armReportNode.ActiveNode, report);
      ((ReportItem) report).Name = name;
    }
    return report;
  }

  [PXInternalUseOnly]
  public static byte[] RenderReportToByteArray(
    this IReportRenderer reportRenderer,
    ReportNode reportNode,
    string format)
  {
    using (StreamManager streamManager = reportRenderer.RenderReport(reportNode, format))
      return streamManager.MainStream.GetBytes();
  }

  public static StreamManager RenderReport(
    this IReportRenderer reportRenderer,
    ReportNode reportNode,
    string format,
    Hashtable deviceInfo = null,
    bool embedStreams = false)
  {
    ExceptionExtensions.ThrowOnNull<IReportRenderer>(reportRenderer, nameof (reportRenderer), (string) null);
    if (deviceInfo == null)
    {
      deviceInfo = new Hashtable();
      deviceInfo[(object) "PageNumber"] = (object) -1;
      deviceInfo[(object) "StartPage"] = (object) -1;
      deviceInfo[(object) "EndPage"] = (object) -1;
    }
    StreamManager streamManager = new StreamManager();
    streamManager.EmbedStreams = embedStreams;
    string str1 = string.Empty;
    reportNode.SendMailMode = true;
    using (new Message.FilterMessage(reportNode, (MailSender.MailMessageT) null))
    {
      reportRenderer.Render(format, reportNode, deviceInfo, streamManager);
      str1 = ((Report) ((ItemNode) reportNode).Definition).ReportName ?? string.Empty;
    }
    ((Stream) streamManager.MainStream).Flush();
    ((Stream) streamManager.MainStream).Seek(0L, SeekOrigin.Begin);
    string str2 = ((ItemNode) reportNode).Name;
    if (string.IsNullOrEmpty(str2))
    {
      string str3 = str1;
      int length = str3.LastIndexOf('.');
      if (length > -1)
        str3 = str3.Substring(0, length);
      str2 = str3;
    }
    string str4 = $"{str2} - ({System.DateTime.Today.ToShortDateString()}).{streamManager.MainStream.Extension}";
    streamManager.MainStream.Name = str4;
    return streamManager;
  }

  internal static void Assign(this ReportParameterCollection parameters, string name, string value)
  {
    if (!parameters.Contains(name))
      ((List<ReportParameter>) parameters).Add(new ReportParameter(name));
    parameters[name].Value = (object) value;
  }
}
