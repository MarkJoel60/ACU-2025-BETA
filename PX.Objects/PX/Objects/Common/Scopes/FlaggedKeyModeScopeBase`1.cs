// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Scopes.FlaggedKeyModeScopeBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Objects.Common.Scopes;

public abstract class FlaggedKeyModeScopeBase<FlagType> : IDisposable
{
  private static readonly string scopeKey = typeof (FlagType).FullName;
  private readonly string _key;

  public FlaggedKeyModeScopeBase(string key)
  {
    this._key = FlaggedKeyModeScopeBase<FlagType>.scopeKey + key;
    PXContext.SetSlot<bool>(this._key, true);
  }

  void IDisposable.Dispose() => PXContext.SetSlot<bool>(this._key, false);

  public static bool IsActive(string key = "")
  {
    return PXContext.GetSlot<bool>(FlaggedKeyModeScopeBase<FlagType>.scopeKey + key);
  }
}
