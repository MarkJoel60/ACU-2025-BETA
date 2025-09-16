// Decompiled with JetBrains decompiler
// Type: PX.Reports.ReportRunner
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Async;
using PX.Data;
using PX.Reports.Controls;
using PX.Reports.Data;
using System;

#nullable enable
namespace PX.Reports;

internal sealed class ReportRunner : IReportRunner
{
  private readonly ILongOperationManager _longOperationManager;
  private readonly IReportLoaderService _reportLoader;
  private readonly IReportDataBinder _reportDataBinder;

  public ReportRunner(
    ILongOperationManager longOperationManager,
    IReportLoaderService reportLoader,
    IReportDataBinder reportDataBinder)
  {
    this._longOperationManager = longOperationManager;
    this._reportLoader = reportLoader;
    this._reportDataBinder = reportDataBinder;
  }

  void IReportRunner.Start(object instanceId, Report report, string screenId)
  {
    this._longOperationManager.StartOperation(instanceId, (PXToggleAsyncDelegate) (() => this._longOperationManager.SetCustomInfo((object) this._reportDataBinder.ProcessReportDataBinding(this._reportLoader.RenderUniversalReport(report, screenId)))));
  }

  PXLongRunStatus IReportRunner.GetStatus(object instanceId, out Exception? error)
  {
    (PXLongRunStatus status1, TimeSpan _, Exception message, bool _) = this._longOperationManager.GetOperationDetails(instanceId);
    int status2 = (int) status1;
    error = message;
    return (PXLongRunStatus) status2;
  }

  ReportNode? IReportRunner.GetResult(object instanceId)
  {
    return this._longOperationManager.GetCustomInfoFor(instanceId, out object[] _) as ReportNode;
  }

  void IReportRunner.Clear(object instanceId) => this._longOperationManager.ClearStatus(instanceId);
}
