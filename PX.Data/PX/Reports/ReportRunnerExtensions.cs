// Decompiled with JetBrains decompiler
// Type: PX.Reports.ReportRunnerExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Reports.Controls;
using PX.Reports.Data;
using System;

#nullable enable
namespace PX.Reports;

internal static class ReportRunnerExtensions
{
  internal static Guid Start(this IReportRunner reportRunner, Report report, string screenId)
  {
    Guid instanceId = Guid.NewGuid();
    reportRunner.Start((object) instanceId, report, screenId);
    return instanceId;
  }

  internal static ReportNode? PopResult(this IReportRunner reportRunner, object instanceId)
  {
    ReportNode result = reportRunner.GetResult(instanceId);
    reportRunner.Clear(instanceId);
    return result;
  }
}
