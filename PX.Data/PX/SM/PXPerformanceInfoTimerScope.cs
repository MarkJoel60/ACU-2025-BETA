// Decompiled with JetBrains decompiler
// Type: PX.SM.PXPerformanceInfoTimerScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Diagnostics;

#nullable disable
namespace PX.SM;

public class PXPerformanceInfoTimerScope : IDisposable
{
  private readonly Stopwatch _stopwatch;
  private readonly PXPerformanceInfoTimerScope _parentScope;
  [ThreadStatic]
  private static PXPerformanceInfoTimerScope _currentScope;

  public PXPerformanceInfoTimerScope(Func<PXPerformanceInfo, Stopwatch> stopwatchGetter)
  {
    if (!PXPerformanceMonitor.IsTimersScopesEnabled)
      return;
    PXPerformanceInfo slot = PXContext.GetSlot<PXPerformanceInfo>();
    if (slot == null)
      return;
    this._stopwatch = stopwatchGetter(slot);
    if (PXPerformanceInfoTimerScope._currentScope != null && PXPerformanceInfoTimerScope._currentScope._stopwatch == this._stopwatch)
      return;
    this._parentScope = PXPerformanceInfoTimerScope._currentScope;
    PXPerformanceInfoTimerScope._currentScope = this;
    this._parentScope?._stopwatch.Stop();
    this._stopwatch.Start();
  }

  public void Dispose()
  {
    if (!PXPerformanceMonitor.IsTimersScopesEnabled)
      return;
    if (this._stopwatch != null && this._stopwatch.IsRunning)
    {
      this._stopwatch.Stop();
      PXPerformanceInfoTimerScope._currentScope = this._parentScope;
    }
    this._parentScope?._stopwatch.Start();
  }
}
