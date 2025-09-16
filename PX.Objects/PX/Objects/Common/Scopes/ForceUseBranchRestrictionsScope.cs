// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Scopes.ForceUseBranchRestrictionsScope
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.Scopes;

public sealed class ForceUseBranchRestrictionsScope : IDisposable
{
  private static string _SLOT_KEY = "ForceUseBranchRestrictionsScope_Running";

  public ForceUseBranchRestrictionsScope()
  {
    PXDatabase.GetSlot<ForceUseBranchRestrictionsScope.BoolWrapper>(ForceUseBranchRestrictionsScope._SLOT_KEY, Array.Empty<Type>()).Value = true;
  }

  public void Dispose()
  {
    PXDatabase.ResetSlot<ForceUseBranchRestrictionsScope.BoolWrapper>(ForceUseBranchRestrictionsScope._SLOT_KEY, Array.Empty<Type>());
  }

  public static bool IsRunning
  {
    get
    {
      return PXDatabase.GetSlot<ForceUseBranchRestrictionsScope.BoolWrapper>(ForceUseBranchRestrictionsScope._SLOT_KEY, Array.Empty<Type>()).Value;
    }
  }

  private class BoolWrapper
  {
    public bool Value { get; set; }

    public BoolWrapper() => this.Value = false;
  }
}
