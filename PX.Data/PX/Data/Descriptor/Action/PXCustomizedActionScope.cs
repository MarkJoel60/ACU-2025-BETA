// Decompiled with JetBrains decompiler
// Type: PX.Data.Descriptor.Action.PXCustomizedActionScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.Descriptor.Action;

internal class PXCustomizedActionScope : IDisposable
{
  private const string PXActionGettingCustomized = "PXActionGettingCustomized";
  private bool _isDisposed;

  public PXCustomizedActionScope(string actionName)
  {
    PXContext.SetSlot<string>(nameof (PXActionGettingCustomized), actionName);
  }

  public static bool IsScoped(string actionName)
  {
    return string.Equals(PXCustomizedActionScope.CurrentActionName, actionName, StringComparison.OrdinalIgnoreCase);
  }

  public static string CurrentActionName => PXContext.GetSlot<string>("PXActionGettingCustomized");

  public void Dispose()
  {
    if (this._isDisposed)
      return;
    PXContext.SetSlot<string>("PXActionGettingCustomized", (string) null);
    this._isDisposed = true;
  }
}
