// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Scopes.FlaggedModeScopeBase`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Objects.Common.Scopes;

public abstract class FlaggedModeScopeBase<FlagType, ParameterType> : IDisposable
{
  private static readonly string scopeKey = typeof (FlagType).FullName;
  private static readonly string parametersKey = $"{FlaggedModeScopeBase<FlagType, ParameterType>.scopeKey}_Parameters";
  private const string parametersKeyFormat = "{0}_Parameters";

  public FlaggedModeScopeBase(ParameterType parameters)
  {
    PXContext.SetSlot<bool>(FlaggedModeScopeBase<FlagType, ParameterType>.scopeKey, true);
    PXContext.SetSlot<ParameterType>(FlaggedModeScopeBase<FlagType, ParameterType>.parametersKey, parameters);
  }

  void IDisposable.Dispose()
  {
    PXContext.SetSlot<bool>(FlaggedModeScopeBase<FlagType, ParameterType>.scopeKey, false);
  }

  public static bool IsActive
  {
    get => PXContext.GetSlot<bool>(FlaggedModeScopeBase<FlagType, ParameterType>.scopeKey);
  }

  public static ParameterType Parameters
  {
    get
    {
      return PXContext.GetSlot<ParameterType>(FlaggedModeScopeBase<FlagType, ParameterType>.parametersKey);
    }
  }
}
