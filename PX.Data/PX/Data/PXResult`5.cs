// Decompiled with JetBrains decompiler
// Type: PX.Data.PXResult`5
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
public class PXResult<T0, T1, T2, T3, T4> : PXResult<T0, T1, T2, T3>
  where T0 : class, IBqlTable, new()
  where T1 : class, IBqlTable, new()
  where T2 : class, IBqlTable, new()
  where T3 : class, IBqlTable, new()
  where T4 : class, IBqlTable, new()
{
  protected PXResult()
  {
  }

  public PXResult(T0 i0, T1 i1, T2 i2, T3 i3, T4 i4)
  {
    this.Items = new object[5]
    {
      (object) i0,
      (object) i1,
      (object) i2,
      (object) i3,
      (object) i4
    };
  }

  public static implicit operator T0(PXResult<T0, T1, T2, T3, T4> r)
  {
    return r == null ? default (T0) : (T0) r.Items[0];
  }

  public static implicit operator T1(PXResult<T0, T1, T2, T3, T4> r)
  {
    return r == null ? default (T1) : (T1) r.Items[1];
  }

  public static implicit operator T2(PXResult<T0, T1, T2, T3, T4> r)
  {
    return r == null ? default (T2) : (T2) r.Items[2];
  }

  public static implicit operator T3(PXResult<T0, T1, T2, T3, T4> r)
  {
    return r == null ? default (T3) : (T3) r.Items[3];
  }

  public static implicit operator T4(PXResult<T0, T1, T2, T3, T4> r)
  {
    return r == null ? default (T4) : (T4) r.Items[4];
  }

  public override object this[System.Type t] => t == typeof (T4) ? this.Items[4] : base[t];

  public override object this[string s] => s == typeof (T4).Name ? this.Items[4] : base[s];

  public override System.Type GetItemType(int i) => i == 4 ? typeof (T4) : base.GetItemType(i);

  public override System.Type GetItemType(string s)
  {
    return s == typeof (T4).Name ? typeof (T4) : base.GetItemType(s);
  }

  public static PXResult<T0, T1, T2, T3, T4> Current
  {
    get
    {
      Dictionary<System.Type, PXResult> slot = PXContext.GetSlot<Dictionary<System.Type, PXResult>>();
      if (slot != null)
      {
        PXResult current = (PXResult) null;
        if (slot.TryGetValue(typeof (PXResult<T0, T1, T2, T3, T4>), out current))
          return (PXResult<T0, T1, T2, T3, T4>) current;
      }
      return (PXResult<T0, T1, T2, T3, T4>) null;
    }
  }

  public void Deconstruct(out T0 item0, out T1 item1, out T2 item2, out T3 item3, out T4 item4)
  {
    (item0, item1, item2, item3) = this;
    item4 = (T4) this;
  }
}
