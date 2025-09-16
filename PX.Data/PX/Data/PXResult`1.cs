// Decompiled with JetBrains decompiler
// Type: PX.Data.PXResult`1
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
public class PXResult<T0> : PXResult where T0 : class, IBqlTable, new()
{
  public T0 Record { get; set; }

  protected PXResult()
  {
  }

  public PXResult(T0 i0)
  {
    this.Items = new object[1]{ (object) i0 };
  }

  public static implicit operator T0(PXResult<T0> r) => r == null ? default (T0) : (T0) r.Items[0];

  public PXResult<T0> CreateCopy()
  {
    this.Items[0] = (object) PXCache<T0>.CreateCopy((T0) this.Items[0]);
    return this;
  }

  public override object this[System.Type t] => t == typeof (T0) ? this.Items[0] : (object) null;

  public override object this[string s] => s == typeof (T0).Name ? this.Items[0] : (object) null;

  public override System.Type GetItemType(int i) => i == 0 ? typeof (T0) : (System.Type) null;

  public override System.Type GetItemType(string s)
  {
    return s == typeof (T0).Name ? typeof (T0) : (System.Type) null;
  }

  public static PXResult<T0> Current
  {
    get
    {
      Dictionary<System.Type, PXResult> slot = PXContext.GetSlot<Dictionary<System.Type, PXResult>>();
      if (slot != null)
      {
        PXResult current = (PXResult) null;
        if (slot.TryGetValue(typeof (PXResult<T0>), out current))
          return (PXResult<T0>) current;
        foreach (System.Type key in slot.Keys)
        {
          int i = Array.IndexOf<System.Type>(key.GetGenericArguments(), typeof (T0));
          if (i != -1)
            return new PXResult<T0>((T0) slot[key][i]);
        }
      }
      return (PXResult<T0>) null;
    }
  }

  public void Deconstruct(out T0 item0) => item0 = (T0) this;
}
