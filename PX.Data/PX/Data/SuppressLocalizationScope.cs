// Decompiled with JetBrains decompiler
// Type: PX.Data.SuppressLocalizationScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public class SuppressLocalizationScope : IDisposable
{
  private bool _previousIsScoped;

  public static bool IsScoped
  {
    get => PXContext.GetSlot<bool>("SuppressLocalizationScope.IsScoped");
    set => PXContext.SetSlot<bool>("SuppressLocalizationScope.IsScoped", value);
  }

  public SuppressLocalizationScope()
  {
    this._previousIsScoped = SuppressLocalizationScope.IsScoped;
    SuppressLocalizationScope.IsScoped = true;
  }

  public void Dispose() => SuppressLocalizationScope.IsScoped = this._previousIsScoped;
}
