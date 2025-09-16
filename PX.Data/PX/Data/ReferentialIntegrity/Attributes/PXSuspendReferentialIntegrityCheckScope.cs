// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.PXSuspendReferentialIntegrityCheckScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Attributes;

/// <summary>
/// Temporarily suspend referential integrity check for a cache
/// </summary>
public sealed class PXSuspendReferentialIntegrityCheckScope : IDisposable
{
  private readonly PXCache _cache;

  public PXSuspendReferentialIntegrityCheckScope(PXCache cache)
  {
    this._cache = cache;
    this._cache.Adjust<PXReferentialIntegrityCheckAttribute>().ForAllFields((System.Action<PXReferentialIntegrityCheckAttribute>) (ric => ric.IsSuspended = true));
  }

  public void Dispose()
  {
    this._cache.Adjust<PXReferentialIntegrityCheckAttribute>().ForAllFields((System.Action<PXReferentialIntegrityCheckAttribute>) (ric => ric.IsSuspended = false));
  }
}
