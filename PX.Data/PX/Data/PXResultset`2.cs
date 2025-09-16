// Decompiled with JetBrains decompiler
// Type: PX.Data.PXResultset`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <exclude />
[Serializable]
public class PXResultset<T0, T1> : PXResultset<T0>, IEnumerable, IPXResultset, IPXResultsetBase
  where T0 : class, IBqlTable, new()
  where T1 : class, IBqlTable, new()
{
  public static implicit operator T0(PXResultset<T0, T1> l)
  {
    return l != null && l.Count > 0 ? (T0) (PXResult<T0, T1>) l[0] : default (T0);
  }

  public static implicit operator T1(PXResultset<T0, T1> l)
  {
    return l != null && l.Count > 0 ? (T1) (PXResult<T0, T1>) l[0] : default (T1);
  }

  public static implicit operator PXResult<T0, T1>(PXResultset<T0, T1> l)
  {
    return l != null && l.Count > 0 ? (PXResult<T0, T1>) l[0] : (PXResult<T0, T1>) null;
  }

  System.Type IPXResultset.GetItemType(int i)
  {
    if (i == 0)
      return typeof (T0);
    return i == 1 ? typeof (T1) : (System.Type) null;
  }

  object IPXResultset.GetItem(int rowNbr, int i)
  {
    return rowNbr < 0 || rowNbr >= this.Count || i != 0 && i != 1 ? (object) null : this[rowNbr][i];
  }

  int IPXResultset.GetTableCount() => 2;

  int IPXResultset.GetRowCount() => this.Count;
}
