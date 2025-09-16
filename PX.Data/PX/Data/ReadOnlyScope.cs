// Decompiled with JetBrains decompiler
// Type: PX.Data.ReadOnlyScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public class ReadOnlyScope : IDisposable
{
  private PXCache[] _caches;
  private bool[] _isDirty;

  public ReadOnlyScope(params PXCache[] caches)
  {
    this._caches = caches;
    this._isDirty = new bool[this._caches.Length];
    for (int index = 0; index < this._caches.Length; ++index)
      this._isDirty[index] = this._caches[index].IsDirty;
  }

  void IDisposable.Dispose()
  {
    for (int index = 0; index < this._caches.Length; ++index)
      this._caches[index].IsDirty = this._isDirty[index];
  }
}
