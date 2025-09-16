// Decompiled with JetBrains decompiler
// Type: PX.SM.PXCancelCommandPreparingScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.SM;

/// <summary>Cancels execution of command preparing events for specified field of specified cache.</summary>
internal sealed class PXCancelCommandPreparingScope : IDisposable
{
  private readonly PXCache _cache;
  private readonly string _fieldName;

  /// <summary>Cancels execution of command preparing events for specified field of specified cache.</summary>
  /// <param name="cache">The cache object those command preparing events will be disabled.</param>
  /// <param name="fieldName">The name of the field for which events will be disabled.</param>
  public PXCancelCommandPreparingScope(PXCache cache, string fieldName)
  {
    this._cache = cache;
    this._fieldName = fieldName;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this._cache.CommandPreparingEvents[this._fieldName] += PXCancelCommandPreparingScope.\u003C\u003EO.\u003C0\u003E__CancelCommandPreparing ?? (PXCancelCommandPreparingScope.\u003C\u003EO.\u003C0\u003E__CancelCommandPreparing = new PXCommandPreparing(PXCancelCommandPreparingScope.CancelCommandPreparing));
  }

  public void Dispose()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this._cache.CommandPreparingEvents[this._fieldName] -= PXCancelCommandPreparingScope.\u003C\u003EO.\u003C0\u003E__CancelCommandPreparing ?? (PXCancelCommandPreparingScope.\u003C\u003EO.\u003C0\u003E__CancelCommandPreparing = new PXCommandPreparing(PXCancelCommandPreparingScope.CancelCommandPreparing));
  }

  private static void CancelCommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    e.Cancel = true;
  }
}
