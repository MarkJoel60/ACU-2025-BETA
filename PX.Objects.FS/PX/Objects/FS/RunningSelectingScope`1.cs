// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RunningSelectingScope`1
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class RunningSelectingScope<DAC> : IDisposable where DAC : IBqlTable
{
  protected List<string> _GraphList;
  protected string _MyGraphSelecting;
  protected readonly RunningSelectingScope<DAC> _Previous;

  public RunningSelectingScope(PXGraph myGraph)
  {
    this._MyGraphSelecting = myGraph.GetType().FullName;
    this._Previous = PXContext.GetSlot<RunningSelectingScope<DAC>>();
    this._GraphList = this._Previous != null ? new List<string>((IEnumerable<string>) this._Previous._GraphList) : new List<string>();
    this._GraphList.Add(this._MyGraphSelecting);
    PXContext.SetSlot<RunningSelectingScope<DAC>>(this);
  }

  public void Dispose() => PXContext.SetSlot<RunningSelectingScope<DAC>>(this._Previous);

  public static bool IsRunningSelecting(PXGraph graph)
  {
    RunningSelectingScope<DAC> slot = PXContext.GetSlot<RunningSelectingScope<DAC>>();
    return slot != null && slot._GraphList.Exists((Predicate<string>) (e => e == graph.GetType().FullName));
  }
}
