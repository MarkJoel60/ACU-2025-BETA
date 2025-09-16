// Decompiled with JetBrains decompiler
// Type: PX.Reports.IReportRunner
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Reports.Controls;
using PX.Reports.Data;
using System;

#nullable enable
namespace PX.Reports;

internal interface IReportRunner
{
  void Start(object instanceId, Report report, string screenId);

  PXLongRunStatus GetStatus(object instanceId, out Exception? error);

  ReportNode? GetResult(object instanceId);

  void Clear(object instanceId);
}
