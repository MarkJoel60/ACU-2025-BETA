// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.PXReportDataProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Reports.Controls;
using System;

#nullable disable
namespace PX.Data.Reports;

[Serializable]
public class PXReportDataProvider : IPXReportDataProvider
{
  public string GetReportPathByName(string reportName) => throw new PXException("Deprecated !!!");

  public string GetReportDataByName(string reportName)
  {
    return ReportDbStorage.ReportGetXml(reportName.ToLowerInvariant());
  }

  public string GetReportDataByName(string reportName, int ver)
  {
    return ReportDbStorage.GetVersion(reportName.ToLowerInvariant(), ver);
  }
}
