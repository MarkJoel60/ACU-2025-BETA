// Decompiled with JetBrains decompiler
// Type: PX.SM.AllowWorkflowExtendedComboBoxValuesScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.SM;

public class AllowWorkflowExtendedComboBoxValuesScope : IDisposable
{
  private readonly bool _previousValue;

  public AllowWorkflowExtendedComboBoxValuesScope()
  {
    this._previousValue = PXContext.GetSlot<bool>("AllowWorkflowExtendedComboBoxValues");
    PXContext.SetSlot<bool>("AllowWorkflowExtendedComboBoxValues", true);
  }

  public void Dispose()
  {
    PXContext.SetSlot<bool>("AllowWorkflowExtendedComboBoxValues", this._previousValue);
  }
}
