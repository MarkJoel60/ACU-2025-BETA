// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.PXUseSystemOnlyWorkflow
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.Automation;

[PXInternalUseOnly]
public class PXUseSystemOnlyWorkflow : IDisposable
{
  private readonly bool _previousValue;

  public PXUseSystemOnlyWorkflow()
  {
    this._previousValue = PXContext.GetSlot<bool>(nameof (PXUseSystemOnlyWorkflow));
    PXContext.SetSlot<bool>(nameof (PXUseSystemOnlyWorkflow), true);
  }

  public void Dispose()
  {
    PXContext.SetSlot<bool>(nameof (PXUseSystemOnlyWorkflow), this._previousValue);
  }

  public static bool UseSystemOnlyWorkflow()
  {
    return PXContext.GetSlot<bool>(nameof (PXUseSystemOnlyWorkflow));
  }
}
