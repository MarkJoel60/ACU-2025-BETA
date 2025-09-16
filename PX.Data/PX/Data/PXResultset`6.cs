// Decompiled with JetBrains decompiler
// Type: PX.Data.PXResultset`6
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
public class PXResultset<T0, T1, T2, T3, T4, T5> : 
  PXResultset<T0, T1, T2, T3, T4>,
  IEnumerable,
  IPXResultset,
  IPXResultsetBase
  where T0 : class, IBqlTable, new()
  where T1 : class, IBqlTable, new()
  where T2 : class, IBqlTable, new()
  where T3 : class, IBqlTable, new()
  where T4 : class, IBqlTable, new()
  where T5 : class, IBqlTable, new()
{
  public static implicit operator T0(PXResultset<T0, T1, T2, T3, T4, T5> l)
  {
    return l != null && l.Count > 0 ? (T0) (PXResult<T0, T1, T2, T3, T4, T5>) l[0] : default (T0);
  }

  public static implicit operator T1(PXResultset<T0, T1, T2, T3, T4, T5> l)
  {
    return l != null && l.Count > 0 ? (T1) (PXResult<T0, T1, T2, T3, T4, T5>) l[0] : default (T1);
  }

  public static implicit operator T2(PXResultset<T0, T1, T2, T3, T4, T5> l)
  {
    return l != null && l.Count > 0 ? (T2) (PXResult<T0, T1, T2, T3, T4, T5>) l[0] : default (T2);
  }

  public static implicit operator T3(PXResultset<T0, T1, T2, T3, T4, T5> l)
  {
    return l != null && l.Count > 0 ? (T3) (PXResult<T0, T1, T2, T3, T4, T5>) l[0] : default (T3);
  }

  public static implicit operator T4(PXResultset<T0, T1, T2, T3, T4, T5> l)
  {
    return l != null && l.Count > 0 ? (T4) (PXResult<T0, T1, T2, T3, T4, T5>) l[0] : default (T4);
  }

  public static implicit operator T5(PXResultset<T0, T1, T2, T3, T4, T5> l)
  {
    return l != null && l.Count > 0 ? (T5) (PXResult<T0, T1, T2, T3, T4, T5>) l[0] : default (T5);
  }

  public static implicit operator PXResult<T0, T1, T2, T3, T4, T5>(
    PXResultset<T0, T1, T2, T3, T4, T5> l)
  {
    return l != null && l.Count > 0 ? (PXResult<T0, T1, T2, T3, T4, T5>) l[0] : (PXResult<T0, T1, T2, T3, T4, T5>) null;
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
      case 3:
        return typeof (T3);
      case 4:
        return typeof (T4);
      case 5:
        return typeof (T5);
      default:
        return (System.Type) null;
    }
  }

  object IPXResultset.GetItem(int rowNbr, int i)
  {
    return rowNbr < 0 || rowNbr >= this.Count || i != 0 && i != 1 && i != 2 && i != 3 && i != 4 && i != 5 ? (object) null : this[rowNbr][i];
  }

  int IPXResultset.GetTableCount() => 6;

  int IPXResultset.GetRowCount() => this.Count;
}
