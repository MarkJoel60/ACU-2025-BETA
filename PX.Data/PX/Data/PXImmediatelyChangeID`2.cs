// Decompiled with JetBrains decompiler
// Type: PX.Data.PXImmediatelyChangeID`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXImmediatelyChangeID<Table, Field> : PXChangeID<Table, Field>
  where Table : class, IBqlTable, new()
  where Field : class, IBqlField
{
  public PXImmediatelyChangeID(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXImmediatelyChangeID(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Change ID", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected override IEnumerable Handler(PXAdapter adapter)
  {
    IEnumerable enumerable = base.Handler(adapter);
    PXCache cach = this.Graph.Caches[typeof (ChangeIDParam)];
    if (!this.HasError)
    {
      this.Graph.Actions.PressSave();
      cach.Clear();
      this.Graph.SelectTimeStamp();
      enumerable = (IEnumerable) new object[1]
      {
        this.Graph.Caches<Table>().Current
      };
    }
    return enumerable;
  }
}
