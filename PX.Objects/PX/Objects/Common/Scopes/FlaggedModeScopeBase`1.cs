// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Scopes.FlaggedModeScopeBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Objects.Common.Scopes;

public abstract class FlaggedModeScopeBase<FlagType> : IDisposable
{
  private static readonly string scopeKey = typeof (FlagType).FullName;

  public FlaggedModeScopeBase()
  {
    PXContext.SetSlot<bool>(FlaggedModeScopeBase<FlagType>.scopeKey, true);
  }

  void IDisposable.Dispose()
  {
    PXContext.SetSlot<bool>(FlaggedModeScopeBase<FlagType>.scopeKey, false);
  }

  public static bool IsActive => PXContext.GetSlot<bool>(FlaggedModeScopeBase<FlagType>.scopeKey);
}
