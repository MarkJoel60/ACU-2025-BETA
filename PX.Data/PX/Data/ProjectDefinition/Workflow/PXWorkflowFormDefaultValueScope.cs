// Decompiled with JetBrains decompiler
// Type: PX.Data.ProjectDefinition.Workflow.PXWorkflowFormDefaultValueScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.ProjectDefinition.Workflow;

internal class PXWorkflowFormDefaultValueScope : IDisposable
{
  private readonly bool _previousValue;

  public PXWorkflowFormDefaultValueScope()
  {
    this._previousValue = PXContext.GetSlot<bool>(nameof (PXWorkflowFormDefaultValueScope));
    PXContext.SetSlot<bool>(nameof (PXWorkflowFormDefaultValueScope), true);
  }

  public void Dispose()
  {
    PXContext.SetSlot<bool>(nameof (PXWorkflowFormDefaultValueScope), this._previousValue);
  }

  public static bool IsWorkflowFormDefaultValueScope()
  {
    return PXContext.GetSlot<bool>(nameof (PXWorkflowFormDefaultValueScope));
  }
}
