// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.QtyAllocType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

public static class QtyAllocType
{
  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[1]
      {
        PXStringListAttribute.Pair("undefined", "undefined")
      })
    {
    }

    public virtual bool IsLocalizable => false;

    public virtual void CacheAttached(PXCache sender)
    {
      PXCache cach = sender.Graph.Caches[typeof (INPlanType)];
      List<string> stringList1 = new List<string>();
      List<string> stringList2 = new List<string>();
      foreach (string field in (List<string>) cach.Fields)
      {
        object stateExt = cach.GetStateExt((object) null, field);
        if (stateExt is PXIntState)
        {
          stringList1.Add(char.ToLower(field[0]).ToString() + field.Substring(1));
          stringList2.Add(((PXFieldState) stateExt).DisplayName);
        }
      }
      this._AllowedValues = stringList1.ToArray();
      this._AllowedLabels = stringList2.ToArray();
      this._NeutralAllowedLabels = this._AllowedLabels;
      base.CacheAttached(sender);
    }
  }
}
