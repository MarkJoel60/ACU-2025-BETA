// Decompiled with JetBrains decompiler
// Type: PX.Data.PXResult`3
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
public class PXResult<T0, T1, T2> : PXResult<T0, T1>
  where T0 : class, IBqlTable, new()
  where T1 : class, IBqlTable, new()
  where T2 : class, IBqlTable, new()
{
  protected PXResult()
  {
  }

  public PXResult(T0 i0, T1 i1, T2 i2)
  {
    this.Items = new object[3]
    {
      (object) i0,
      (object) i1,
      (object) i2
    };
  }

  public static implicit operator T0(PXResult<T0, T1, T2> r)
  {
    return r == null ? default (T0) : (T0) r.Items[0];
  }

  public static implicit operator T1(PXResult<T0, T1, T2> r)
  {
    return r == null ? default (T1) : (T1) r.Items[1];
  }

  public static implicit operator T2(PXResult<T0, T1, T2> r)
  {
    return r == null ? default (T2) : (T2) r.Items[2];
  }

  public override object this[System.Type t] => t == typeof (T2) ? this.Items[2] : base[t];

  public override object this[string s] => s == typeof (T2).Name ? this.Items[2] : base[s];

  public override System.Type GetItemType(int i) => i == 2 ? typeof (T2) : base.GetItemType(i);

  public override System.Type GetItemType(string s)
  {
    return s == typeof (T2).Name ? typeof (T2) : base.GetItemType(s);
  }

  public static PXResult<T0, T1, T2> Current
  {
    get
    {
      Dictionary<System.Type, PXResult> slot = PXContext.GetSlot<Dictionary<System.Type, PXResult>>();
      if (slot != null)
      {
        PXResult current = (PXResult) null;
        if (slot.TryGetValue(typeof (PXResult<T0, T1, T2>), out current))
          return (PXResult<T0, T1, T2>) current;
      }
      return (PXResult<T0, T1, T2>) null;
    }
  }

  public void Deconstruct(out T0 item0, out T1 item1, out T2 item2)
  {
    (item0, item1) = this;
    item2 = (T2) this;
  }
}
