// Decompiled with JetBrains decompiler
// Type: PX.Data.PXResultset`3
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
public class PXResultset<T0, T1, T2> : 
  PXResultset<T0, T1>,
  IEnumerable,
  IPXResultset,
  IPXResultsetBase
  where T0 : class, IBqlTable, new()
  where T1 : class, IBqlTable, new()
  where T2 : class, IBqlTable, new()
{
  public static implicit operator T0(PXResultset<T0, T1, T2> l)
  {
    return l != null && l.Count > 0 ? (T0) (PXResult<T0, T1, T2>) l[0] : default (T0);
  }

  public static implicit operator T1(PXResultset<T0, T1, T2> l)
  {
    return l != null && l.Count > 0 ? (T1) (PXResult<T0, T1, T2>) l[0] : default (T1);
  }

  public static implicit operator T2(PXResultset<T0, T1, T2> l)
  {
    return l != null && l.Count > 0 ? (T2) (PXResult<T0, T1, T2>) l[0] : default (T2);
  }

  public static implicit operator PXResult<T0, T1, T2>(PXResultset<T0, T1, T2> l)
  {
    return l != null && l.Count > 0 ? (PXResult<T0, T1, T2>) l[0] : (PXResult<T0, T1, T2>) null;
  }

  System.Type IPXResultset.GetItemType(int i)
  {
    switch (i)
    {
      case 0:
        return typeof (T0);
      case 1:
        return typeof (T1);
      case 2:
        return typeof (T2);
      default:
        return (System.Type) null;
    }
  }

  object IPXResultset.GetItem(int rowNbr, int i)
  {
    return rowNbr < 0 || rowNbr >= this.Count || i != 0 && i != 1 && i != 2 ? (object) null : this[rowNbr][i];
  }

  int IPXResultset.GetTableCount() => 3;

  int IPXResultset.GetRowCount() => this.Count;
}
