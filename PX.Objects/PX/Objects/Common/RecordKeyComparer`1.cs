// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.RecordKeyComparer`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common;

/// <summary>
/// Compares two data records based on the equality of their keys.
/// The collection of keys is defined by the specified
/// <see cref="T:PX.Data.PXCache" /> object.
/// </summary>
public class RecordKeyComparer<TRecord> : IEqualityComparer<TRecord> where TRecord : class, IBqlTable, new()
{
  private PXCache _cache;

  public RecordKeyComparer(PXCache cache)
  {
    this._cache = cache != null ? cache : throw new ArgumentNullException(nameof (cache));
  }

  public bool Equals(TRecord first, TRecord second)
  {
    return this._cache.ObjectsEqual((object) first, (object) second);
  }

  public int GetHashCode(TRecord record) => this._cache.GetObjectHashCode((object) record);
}
