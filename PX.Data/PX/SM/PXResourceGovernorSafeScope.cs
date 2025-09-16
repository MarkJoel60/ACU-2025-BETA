// Decompiled with JetBrains decompiler
// Type: PX.SM.PXResourceGovernorSafeScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.SM;

internal class PXResourceGovernorSafeScope : IDisposable
{
  private readonly bool _previousValue;

  public PXResourceGovernorSafeScope()
  {
    this._previousValue = PXContext.GetSlot<bool>("PXPerformanceMonitor.PXResourceGovernorSafeScope");
    PXContext.SetSlot<bool>("PXPerformanceMonitor.PXResourceGovernorSafeScope", true);
  }

  public void Dispose()
  {
    PXContext.SetSlot<bool>("PXPerformanceMonitor.PXResourceGovernorSafeScope", this._previousValue);
  }
}
