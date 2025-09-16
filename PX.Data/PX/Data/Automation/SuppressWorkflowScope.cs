// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.SuppressWorkflowScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.Automation;

[PXInternalUseOnly]
public class SuppressWorkflowScope : IDisposable
{
  private bool _previousIsScoped;

  public static bool IsScoped
  {
    get => PXContext.GetSlot<bool>("SuppressWorkflowScope.IsScoped");
    set => PXContext.SetSlot<bool>("SuppressWorkflowScope.IsScoped", value);
  }

  public SuppressWorkflowScope()
  {
    this._previousIsScoped = SuppressWorkflowScope.IsScoped;
    SuppressWorkflowScope.IsScoped = true;
  }

  public void Dispose() => SuppressWorkflowScope.IsScoped = this._previousIsScoped;
}
