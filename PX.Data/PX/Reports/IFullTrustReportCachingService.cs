// Decompiled with JetBrains decompiler
// Type: PX.Reports.IFullTrustReportCachingService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Reports;
using System.Collections.Generic;

#nullable disable
namespace PX.Reports;

internal interface IFullTrustReportCachingService : IReportCachingService
{
  bool ProcessAndCacheReportUnsecure(
    string reportID,
    IDictionary<string, string> reportParams,
    IPXResultset resultSet,
    bool selectData,
    bool loadArguments,
    out SoapNavigator soapNavigator,
    out ReportInfo reportInfo);
}
