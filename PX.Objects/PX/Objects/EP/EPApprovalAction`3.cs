// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPApprovalAction`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.EP;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.EP;

public class EPApprovalAction<SourceAssign, Approved, Rejected> : 
  EPApprovalList<SourceAssign, Approved, Rejected>
  where SourceAssign : class, IAssign, IBqlTable, new()
  where Approved : class, IBqlField
  where Rejected : class, IBqlField
{
  public virtual EPApprovalAction<SourceAssign, Approved, Rejected>.StatusDelegate StatusHandler { get; set; }

  protected virtual bool Persistent => false;

  public EPApprovalAction(PXGraph graph, Delegate @delegate)
    : base(graph, @delegate)
  {
  }

  public EPApprovalAction(PXGraph graph)
    : base(graph)
  {
  }

  protected override void Init(PXGraph graph)
  {
    base.Init(graph);
    PXGraph graph1 = graph;
    EPApprovalAction<SourceAssign, Approved, Rejected> epApprovalAction1 = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate handler1 = new PXButtonDelegate((object) epApprovalAction1, __vmethodptr(epApprovalAction1, Approve));
    this.AddAction(graph1, "Approve", handler1);
    PXGraph graph2 = graph;
    EPApprovalAction<SourceAssign, Approved, Rejected> epApprovalAction2 = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate handler2 = new PXButtonDelegate((object) epApprovalAction2, __vmethodptr(epApprovalAction2, Reject));
    this.AddAction(graph2, "Reject", handler2);
  }

  [Obsolete("Will be removed in 2018R1 version")]
  public virtual void SetEnabledActions(PXCache sender, SourceAssign row, bool enable)
  {
    if (enable && !this.IsApprover(row))
      enable = false;
    sender.Graph.Actions["Approve"].SetEnabled(enable);
    sender.Graph.Actions["Reject"].SetEnabled(enable);
  }

  protected virtual void AddAction(PXGraph graph, string name, PXButtonDelegate handler)
  {
    graph.Actions[name] = (PXAction) Activator.CreateInstance(typeof (PXNamedAction<>).MakeGenericType(BqlCommand.GetItemType(this.SourceNoteID)), (object) graph, (object) name, (object) handler);
  }

  [PXUIField(DisplayName = "Approve")]
  [PXButton(Category = "Approval")]
  public virtual IEnumerable Approve(PXAdapter adapter)
  {
    EPApprovalAction<SourceAssign, Approved, Rejected> epApprovalAction = this;
    foreach (SourceAssign source in adapter.Get<SourceAssign>())
    {
      try
      {
        EPApprovalAction<SourceAssign, Approved, Rejected>.StatusDelegate statusDelegate = epApprovalAction.Approve(source) ? epApprovalAction.StatusHandler : throw new PXSetPropertyException("You are not an authorized approver for this document.");
        if (statusDelegate != null)
          statusDelegate(source);
        ((PXSelectBase) epApprovalAction)._Graph.Caches[typeof (SourceAssign)].Update((object) source);
        if (epApprovalAction.Persistent)
          ((PXSelectBase) epApprovalAction)._Graph.Persist();
      }
      catch (ReasonRejectedException ex)
      {
      }
      yield return (object) source;
    }
  }

  [PXUIField(DisplayName = "Reject")]
  [PXButton(Category = "Approval")]
  public virtual IEnumerable Reject(PXAdapter adapter)
  {
    EPApprovalAction<SourceAssign, Approved, Rejected> epApprovalAction = this;
    foreach (SourceAssign source in adapter.Get<SourceAssign>())
    {
      try
      {
        EPApprovalAction<SourceAssign, Approved, Rejected>.StatusDelegate statusDelegate = epApprovalAction.Reject(source) ? epApprovalAction.StatusHandler : throw new PXSetPropertyException("You are not an authorized approver for this document.");
        if (statusDelegate != null)
          statusDelegate(source);
        ((PXSelectBase) epApprovalAction)._Graph.Caches[typeof (SourceAssign)].Update((object) source);
        if (epApprovalAction.Persistent)
          ((PXSelectBase) epApprovalAction)._Graph.Persist();
      }
      catch (ReasonRejectedException ex)
      {
      }
      yield return (object) source;
    }
  }

  public delegate void StatusDelegate(SourceAssign item)
    where SourceAssign : class, IAssign, IBqlTable, new()
    where Approved : class, IBqlField
    where Rejected : class, IBqlField;
}
