// Decompiled with JetBrains decompiler
// Type: PX.Data.PXWorkflowAction`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>The class that is rendered as a button on the page datasource.</summary>
/// <typeparam name="TNode">The action is executed when the primary view of the TNode type is selected.</typeparam>
public class PXWorkflowAction<TNode> : PXAction<TNode> where TNode : class, IBqlTable, new()
{
  protected PXAction _SubAction;

  public PXWorkflowAction(PXGraph graph, Delegate handler, string name)
    : base(graph)
  {
    this._Handler = handler;
    this.SetHandler(handler, name);
  }

  public PXWorkflowAction(PXGraph graph, PXAction action, string name)
    : base(graph)
  {
    this._SubAction = action;
    this._Attributes = action.Attributes.ToArray<PXEventSubscriberAttribute>();
    this._Name = name;
  }

  public PXWorkflowAction(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  [PXUIField(DisplayName = "Proxy Action")]
  [PXButton]
  protected override IEnumerable Handler(PXAdapter adapter)
  {
    return this._Menus != null && this._Menus.Length != 0 && !string.IsNullOrWhiteSpace(this._Menus[0].Command) ? this._Graph.Actions[this._Menus[0].Command].Press(adapter) : (IEnumerable) new object[0];
  }

  public override object GetState(object row)
  {
    PXButtonState state = base.GetState(row) as PXButtonState;
    state.PopupVisible = true;
    state.Menus = (ButtonMenu[]) null;
    return (object) state;
  }
}
