// Decompiled with JetBrains decompiler
// Type: PX.Data.PXWeakKeyComparer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
[Serializable]
internal class PXWeakKeyComparer : IEqualityComparer<IBqlTable>
{
  private static readonly PXWeakKeyComparer _comparer = new PXWeakKeyComparer();

  internal static PXWeakKeyComparer Comparer => PXWeakKeyComparer._comparer;

  bool IEqualityComparer<IBqlTable>.Equals(IBqlTable x, IBqlTable y)
  {
    if (x == y)
      return true;
    if (x == null || y == null || x.GetHashCode() != y.GetHashCode())
      return false;
    if (x is PXWeakReference pxWeakReference1)
    {
      if (!pxWeakReference1.IsAlive)
        return false;
      x = pxWeakReference1.Target as IBqlTable;
    }
    if (y is PXWeakReference pxWeakReference2)
    {
      if (!pxWeakReference2.IsAlive)
        return false;
      y = pxWeakReference2.Target as IBqlTable;
    }
    return x == y;
  }

  int IEqualityComparer<IBqlTable>.GetHashCode(IBqlTable obj) => obj.GetHashCode();
}
