// Decompiled with JetBrains decompiler
// Type: PX.Data.PXResult`14
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
[Serializable]
public class PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : 
  PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
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
{
  protected PXResult()
  {
  }

  public PXResult(
    T0 i0,
    T1 i1,
    T2 i2,
    T3 i3,
    T4 i4,
    T5 i5,
    T6 i6,
    T7 i7,
    T8 i8,
    T9 i9,
    T10 i10,
    T11 i11,
    T12 i12,
    T13 i13)
  {
    this.Items = new object[14]
    {
      (object) i0,
      (object) i1,
      (object) i2,
      (object) i3,
      (object) i4,
      (object) i5,
      (object) i6,
      (object) i7,
      (object) i8,
      (object) i9,
      (object) i10,
      (object) i11,
      (object) i12,
      (object) i13
    };
  }

  public static implicit operator T0(
    PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> r)
  {
    return r == null ? default (T0) : (T0) r.Items[0];
  }

  public static implicit operator T1(
    PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> r)
  {
    return r == null ? default (T1) : (T1) r.Items[1];
  }

  public static implicit operator T2(
    PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> r)
  {
    return r == null ? default (T2) : (T2) r.Items[2];
  }

  public static implicit operator T3(
    PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> r)
  {
    return r == null ? default (T3) : (T3) r.Items[3];
  }

  public static implicit operator T4(
    PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> r)
  {
    return r == null ? default (T4) : (T4) r.Items[4];
  }

  public static implicit operator T5(
    PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> r)
  {
    return r == null ? default (T5) : (T5) r.Items[5];
  }

  public static implicit operator T6(
    PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> r)
  {
    return r == null ? default (T6) : (T6) r.Items[6];
  }

  public static implicit operator T7(
    PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> r)
  {
    return r == null ? default (T7) : (T7) r.Items[7];
  }

  public static implicit operator T8(
    PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> r)
  {
    return r == null ? default (T8) : (T8) r.Items[8];
  }

  public static implicit operator T9(
    PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> r)
  {
    return r == null ? default (T9) : (T9) r.Items[9];
  }

  public static implicit operator T10(
    PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> r)
  {
    return r == null ? default (T10) : (T10) r.Items[10];
  }

  public static implicit operator T11(
    PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> r)
  {
    return r == null ? default (T11) : (T11) r.Items[11];
  }

  public static implicit operator T12(
    PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> r)
  {
    return r == null ? default (T12) : (T12) r.Items[12];
  }

  public static implicit operator T13(
    PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> r)
  {
    return r == null ? default (T13) : (T13) r.Items[13];
  }

  public override object this[System.Type t] => t == typeof (T13) ? this.Items[13] : base[t];

  public override object this[string s] => s == typeof (T13).Name ? this.Items[13] : base[s];

  public override System.Type GetItemType(int i) => i == 13 ? typeof (T13) : base.GetItemType(i);

  public override System.Type GetItemType(string s)
  {
    return s == typeof (T13).Name ? typeof (T13) : base.GetItemType(s);
  }

  public static PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Current
  {
    get
    {
      Dictionary<System.Type, PXResult> slot = PXContext.GetSlot<Dictionary<System.Type, PXResult>>();
      if (slot != null)
      {
        PXResult current = (PXResult) null;
        if (slot.TryGetValue(typeof (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>), out current))
          return (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>) current;
      }
      return (PXResult<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>) null;
    }
  }

  public void Deconstruct(
    out T0 item0,
    out T1 item1,
    out T2 item2,
    out T3 item3,
    out T4 item4,
    out T5 item5,
    out T6 item6,
    out T7 item7,
    out T8 item8,
    out T9 item9,
    out T10 item10,
    out T11 item11,
    out T12 item12,
    out T13 item13)
  {
    (item0, item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12) = this;
    item13 = (T13) this;
  }
}
