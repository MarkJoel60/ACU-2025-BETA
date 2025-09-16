// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.KeyValuesComparer`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

[PXInternalUseOnly]
public class KeyValuesComparer<TEntity> : IEqualityComparer<TEntity> where TEntity : class, IBqlTable
{
  private readonly int[] _keyOrdinals;
  private readonly PXCache _cache;

  public KeyValuesComparer(PXCache cache, IEnumerable<Type> keyFields)
  {
    this._cache = cache;
    this._keyOrdinals = keyFields.Select<Type, int>((Func<Type, int>) (f => this._cache.GetFieldOrdinal(f.Name))).ToArray<int>();
  }

  public bool Equals(TEntity x, TEntity y)
  {
    if ((object) x == (object) y)
      return true;
    foreach (int keyOrdinal in this._keyOrdinals)
    {
      if (!object.Equals(this._cache.GetValue((object) x, keyOrdinal), this._cache.GetValue((object) y, keyOrdinal)))
        return false;
    }
    return true;
  }

  public int GetHashCode(TEntity entity)
  {
    if ((object) entity == null)
      return 0;
    int hashCode = 13;
    foreach (int keyOrdinal in this._keyOrdinals)
      hashCode = hashCode * 37 + (this._cache.GetValue((object) entity, keyOrdinal) ?? (object) 0).GetHashCode();
    return hashCode;
  }
}
