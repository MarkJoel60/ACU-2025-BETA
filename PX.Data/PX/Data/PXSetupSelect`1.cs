// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSetupSelect`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXSetupSelect<TNode> : PXSelectReadonly<TNode>, IPXNonUpdateable where TNode : class, IBqlTable, new()
{
  private TNode _record;

  public PXSetupSelect(PXGraph graph)
    : base(graph)
  {
    graph.Defaults[typeof (TNode)] = new PXGraph.GetDefaultDelegate(this.GetRecord);
    this.View = new PXView(graph, true, (BqlCommand) new PX.Data.Select<TNode>(), (Delegate) new PXSelectDelegate(this.GetRecords));
  }

  protected virtual void FillDefaultValues(TNode record)
  {
  }

  private IEnumerable GetRecords()
  {
    PXSetupSelect<TNode> pxSetupSelect = this;
    TNode record = (TNode) PXSelectBase<TNode, PXSelectReadonly<TNode>.Config>.Select(pxSetupSelect._Graph);
    if ((object) record == null)
    {
      record = new TNode();
      pxSetupSelect.FillDefaultValues(record);
    }
    yield return (object) record;
  }

  public new static PXResultset<TNode> Select(PXGraph graph, params object[] pars)
  {
    return new PXSetupSelect<TNode>(graph).Select();
  }

  private object GetRecord()
  {
    if ((object) this._record == null)
      this._record = (TNode) this.Select();
    return (object) this._record;
  }
}
