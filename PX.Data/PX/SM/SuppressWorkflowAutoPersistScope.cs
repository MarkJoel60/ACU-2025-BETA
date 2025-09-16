// Decompiled with JetBrains decompiler
// Type: PX.SM.SuppressWorkflowAutoPersistScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Automation;
using System;

#nullable disable
namespace PX.SM;

public class SuppressWorkflowAutoPersistScope : IDisposable
{
  private readonly bool _previousValue;
  private readonly PXGraph _graph;

  public SuppressWorkflowAutoPersistScope(PXGraph graph)
  {
    this._graph = graph;
    this._previousValue = PXWorkflowService.PreventSaveOnAction[graph];
    PXWorkflowService.PreventSaveOnAction[graph] = true;
  }

  void IDisposable.Dispose()
  {
    PXWorkflowService.PreventSaveOnAction[this._graph] = this._previousValue;
  }
}
