// Decompiled with JetBrains decompiler
// Type: PX.Data.PXResultset`15
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
public class PXResultset<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : 
  PXResultset<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>,
  IEnumerable,
  IPXResultset,
  IPXResultsetBase
  where T0 : class, IBqlTable, new()
  where T1 : class, IBqlTable, new()
  where T2 : class, IBqlTable, new()
  where T3 : class, IBqlTable, new()
  where T4 : class, IBqlTable, new()
  where T5 : class, IBqlTable, new()
  where T6 : class, IBqlTable, new()
  where T7 : class, IBqlTable, new()
  where T8 : class, IBqlTable, new()
  where T9 : class, IBqlTable, new()
  where T10 : class, IBqlTable, new()
  where T11 : class, IBqlTable, new()
  where T12 : class, IBqlTable, new()
  where T13 : class, IBqlTable, new()
  where T14 : class, IBqlTable, new()
{
  public static implicit operator T0(
    PXResultset<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> l)
  {
    return l != null && l.Count > 0 ? (T0) (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>) l[0] : default (T0);
  }

  public static implicit operator T1(
    PXResultset<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> l)
  {
    return l != null && l.Count > 0 ? (T1) (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>) l[0] : default (T1);
  }

  public static implicit operator T2(
    PXResultset<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> l)
  {
    return l != null && l.Count > 0 ? (T2) (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>) l[0] : default (T2);
  }

  public static implicit operator T3(
    PXResultset<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> l)
  {
    return l != null && l.Count > 0 ? (T3) (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>) l[0] : default (T3);
  }

  public static implicit operator T4(
    PXResultset<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> l)
  {
    return l != null && l.Count > 0 ? (T4) (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>) l[0] : default (T4);
  }

  public static implicit operator T5(
    PXResultset<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> l)
  {
    return l != null && l.Count > 0 ? (T5) (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>) l[0] : default (T5);
  }

  public static implicit operator T6(
    PXResultset<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> l)
  {
    return l != null && l.Count > 0 ? (T6) (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>) l[0] : default (T6);
  }

  public static implicit operator T7(
    PXResultset<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> l)
  {
    return l != null && l.Count > 0 ? (T7) (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>) l[0] : default (T7);
  }

  public static implicit operator T8(
    PXResultset<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> l)
  {
    return l != null && l.Count > 0 ? (T8) (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>) l[0] : default (T8);
  }

  public static implicit operator T9(
    PXResultset<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> l)
  {
    return l != null && l.Count > 0 ? (T9) (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>) l[0] : default (T9);
  }

  public static implicit operator T10(
    PXResultset<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> l)
  {
    return l != null && l.Count > 0 ? (T10) (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>) l[0] : default (T10);
  }

  public static implicit operator T11(
    PXResultset<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> l)
  {
    return l != null && l.Count > 0 ? (T11) (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>) l[0] : default (T11);
  }

  public static implicit operator T12(
    PXResultset<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> l)
  {
    return l != null && l.Count > 0 ? (T12) (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>) l[0] : default (T12);
  }

  public static implicit operator T13(
    PXResultset<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> l)
  {
    return l != null && l.Count > 0 ? (T13) (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>) l[0] : default (T13);
  }

  public static implicit operator T14(
    PXResultset<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> l)
  {
    return l != null && l.Count > 0 ? (T14) (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>) l[0] : default (T14);
  }

  public static implicit operator PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
    PXResultset<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> l)
  {
    return l != null && l.Count > 0 ? (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>) l[0] : (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>) null;
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
      case 6:
        return typeof (T6);
      case 7:
        return typeof (T7);
      case 8:
        return typeof (T8);
      case 9:
        return typeof (T9);
      case 10:
        return typeof (T10);
      case 11:
        return typeof (T11);
      case 12:
        return typeof (T12);
      case 13:
        return typeof (T13);
      case 14:
        return typeof (T14);
      default:
        return (System.Type) null;
    }
  }

  object IPXResultset.GetItem(int rowNbr, int i)
  {
    return rowNbr < 0 || rowNbr >= this.Count || i != 0 && i != 1 && i != 2 && i != 3 && i != 4 && i != 5 && i != 6 && i != 7 && i != 8 && i != 9 && i != 10 && i != 11 && i != 12 && i != 13 && i != 14 ? (object) null : this[rowNbr][i];
  }

  int IPXResultset.GetTableCount() => 14;

  int IPXResultset.GetRowCount() => this.Count;
}
