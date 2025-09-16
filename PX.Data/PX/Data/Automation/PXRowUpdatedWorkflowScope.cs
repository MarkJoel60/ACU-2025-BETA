// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.PXRowUpdatedWorkflowScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.Automation;

[PXInternalUseOnly]
public class PXRowUpdatedWorkflowScope : IDisposable
{
  private readonly bool _previousValue;
  private const string PreventRecursionInRowUpdated = "PreventRecursionInFieldUpdated";

  public PXRowUpdatedWorkflowScope()
  {
    this._previousValue = PXContext.GetSlot<bool>("PreventRecursionInFieldUpdated");
    PXContext.SetSlot<bool>("PreventRecursionInFieldUpdated", true);
  }

  public void Dispose()
  {
    PXContext.SetSlot<bool>("PreventRecursionInFieldUpdated", this._previousValue);
  }

  public static bool IsInScope() => PXContext.GetSlot<bool>("PreventRecursionInFieldUpdated");
}
