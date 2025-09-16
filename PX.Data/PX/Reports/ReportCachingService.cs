// Decompiled with JetBrains decompiler
// Type: PX.Reports.ReportCachingService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Reports;
using PX.Reports.Controls;
using PX.Reports.Data;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Reports;

internal class ReportCachingService : IFullTrustReportCachingService, IReportCachingService
{
  private readonly IFullTrustReportLoaderService _reportLoaderService;
  private readonly IReportDataBinder _reportDataBinder;

  public ReportCachingService(
    IFullTrustReportLoaderService reportLoaderService,
    IReportDataBinder reportDataBinder)
  {
    this._reportLoaderService = reportLoaderService;
    this._reportDataBinder = reportDataBinder;
  }

  private bool ProcessAndCacheReport(
    string reportID,
    IDictionary<string, string> reportParams,
    IPXResultset resultSet,
    bool selectData,
    bool loadArguments,
    bool securityTrimming,
    out SoapNavigator soapNavigator,
    out ReportInfo reportInfo)
  {
    if (!ReportStorageHelper.ReportDescriptors.ContainsKey(reportID))
    {
      Report report = securityTrimming ? this._reportLoaderService.LoadReportUnsecure(reportID, resultSet) : this._reportLoaderService.LoadReport(reportID, resultSet);
      this._reportLoaderService.InitDefaultReportParameters(report, reportParams ?? (IDictionary<string, string>) new Dictionary<string, string>());
      if (!selectData && report.DataSource is SoapNavigator dataSource)
        dataSource.SelectData = false;
      ReportNode reportNode = this._reportDataBinder.ProcessReportDataBinding(report);
      soapNavigator = (SoapNavigator) reportNode.DataSource;
      lock (((ICollection) ReportStorageHelper.ReportDescriptors).SyncRoot)
      {
        ReportSelectArguments selectArguments;
        if (loadArguments)
        {
          selectArguments = new ReportSelectArguments();
          selectArguments.Load(soapNavigator.SelectArguments);
        }
        else
          selectArguments = soapNavigator?.SelectArguments;
        reportInfo = new ReportInfo(selectArguments);
        foreach (ReportRelation reportRelation in (CollectionBase) (reportInfo.SelectArguments?.Relations ?? new ReportRelationCollection(report)))
          reportRelation.Report = (Report) null;
        ReportStorageHelper.ReportDescriptors[reportID] = reportInfo;
      }
      return true;
    }
    soapNavigator = (SoapNavigator) null;
    reportInfo = ReportStorageHelper.ReportDescriptors[reportID];
    return false;
  }

  bool IReportCachingService.ProcessAndCacheReport(
    string reportID,
    IDictionary<string, string> reportParams,
    IPXResultset resultSet,
    bool selectData,
    bool loadArguments,
    out SoapNavigator soapNavigator,
    out ReportInfo reportInfo)
  {
    return this.ProcessAndCacheReport(reportID, reportParams, resultSet, selectData, loadArguments, false, out soapNavigator, out reportInfo);
  }

  bool IFullTrustReportCachingService.ProcessAndCacheReportUnsecure(
    string reportID,
    IDictionary<string, string> reportParams,
    IPXResultset resultSet,
    bool selectData,
    bool loadArguments,
    out SoapNavigator soapNavigator,
    out ReportInfo reportInfo)
  {
    return this.ProcessAndCacheReport(reportID, reportParams, resultSet, selectData, loadArguments, true, out soapNavigator, out reportInfo);
  }
}
