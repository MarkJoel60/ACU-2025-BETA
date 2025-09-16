// Decompiled with JetBrains decompiler
// Type: PX.Data.ProjectDefinition.Workflow.ActionSequencesContext
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.ProjectDefinition.Workflow;

internal class ActionSequencesContext : IDisposable
{
  public const string ActionSequencesContextKey = "ActionSequencesContextKey";
  private readonly bool _isOuterContext = !ActionSequencesContext.InContext();

  public static bool InContext()
  {
    object forCurrentThread = PXLongOperation.GetCustomInfoForCurrentThread("ActionSequencesContextKey");
    return forCurrentThread != null && (bool) forCurrentThread;
  }

  public ActionSequencesContext()
  {
    if (!this._isOuterContext)
      return;
    PXLongOperation.SetCustomInfoInternal(PXLongOperation.GetOperationKey(), nameof (ActionSequencesContextKey), (object) true);
  }

  public void Dispose()
  {
    if (!this._isOuterContext)
      return;
    PXLongOperation.SetCustomInfoInternal(PXLongOperation.GetOperationKey(), "ActionSequencesContextKey", (object) false);
  }
}
