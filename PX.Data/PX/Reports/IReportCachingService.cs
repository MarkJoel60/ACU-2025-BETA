// Decompiled with JetBrains decompiler
// Type: PX.Reports.IReportCachingService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Reports;
using System.Collections.Generic;

#nullable disable
namespace PX.Reports;

/// <summary>
/// Creates and caches the <see cref="!:PXSiteMap.ReportInfo" /> or gets it from the cache.
/// </summary>
/// <remarks>
/// The cache is never cleared, it is stored in a static dictionary.<para />
/// AC-271557: In <see cref="P:PX.Reports.Data.ReportSelectArguments.Relations" /> the following properties will be <see langword="null" />:<para />
/// <see cref="P:PX.Reports.ReportRelation.Report" />, <see cref="P:PX.Reports.ReportRelation.ChildTable" />, and <see cref="P:PX.Reports.ReportRelation.ParentTable" />.
/// </remarks>
internal interface IReportCachingService
{
  bool ProcessAndCacheReport(
    string reportID,
    IDictionary<string, string> reportParams,
    IPXResultset resultSet,
    bool selectData,
    bool loadArguments,
    out SoapNavigator soapNavigator,
    out ReportInfo reportInfo);
}
