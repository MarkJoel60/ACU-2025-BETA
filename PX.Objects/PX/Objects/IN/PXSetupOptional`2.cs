// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PXSetupOptional`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

public class PXSetupOptional<Table, Where> : PXSelectReadonly<Table, Where>
  where Table : class, IBqlTable, new()
  where Where : IBqlWhere, new()
{
  protected Table _Record;

  public PXSetupOptional(PXGraph graph)
    : base(graph)
  {
    // ISSUE: method pointer
    graph.Defaults[typeof (Table)] = new PXGraph.GetDefaultDelegate((object) this, __methodptr(getRecord));
  }

  private object getRecord()
  {
    if ((object) this._Record == null)
    {
      this._Record = PXResultset<Table>.op_Implicit(((PXSelectBase<Table>) this).Select(Array.Empty<object>()));
      if ((object) this._Record == null)
      {
        this._Record = new Table();
        PXCache cach = ((PXSelectBase) this)._Graph.Caches[typeof (Table)];
        foreach (Type bqlField in cach.BqlFields)
        {
          object obj;
          cach.RaiseFieldDefaulting(bqlField.Name.ToLower(), (object) this._Record, ref obj);
          cach.SetValue((object) this._Record, bqlField.Name.ToLower(), obj);
        }
        ((PXSelectBase<Table>) this).StoreCached(new PXCommandKey(new object[0]), new List<object>()
        {
          (object) this._Record
        });
      }
    }
    return (object) this._Record;
  }
}
