// Decompiled with JetBrains decompiler
// Type: PX.Reports.IReportLoaderService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.Wiki.Parser;
using PX.Reports.Controls;
using System.Collections.Generic;

#nullable disable
namespace PX.Reports;

[PXInternalUseOnly]
public interface IReportLoaderService
{
  ISettings WikiSettings { get; }

  /// <exception cref="T:PX.Data.PXException">When user has no rights to the screen</exception>
  Report LoadReport(string reportID, IPXResultset incoming);

  void InitDefaultReportParameters(Report report, IDictionary<string, string> rParams);

  void InitReportParameters(
    Report report,
    IDictionary<string, string> rParams,
    PXReportSettings settings);

  void InitReportParameters(
    Report report,
    IDictionary<string, string> rParams,
    PXReportSettings settings,
    bool resetReport);
}
