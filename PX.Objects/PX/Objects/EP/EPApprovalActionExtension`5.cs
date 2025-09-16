// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPApprovalActionExtension`5
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

public class EPApprovalActionExtension<SourceAssign, Approved, Rejected, ApprovalMapID, ApprovalNotificationID> : 
  EPApprovalAction<SourceAssign, Approved, Rejected>
  where SourceAssign : class, IAssign, IBqlTable, new()
  where Approved : class, IBqlField
  where Rejected : class, IBqlField
  where ApprovalMapID : class, IBqlField
  where ApprovalNotificationID : class, IBqlField
{
  public EPApprovalActionExtension(PXGraph graph, Delegate @delegate)
    : base(graph, @delegate)
  {
  }

  public EPApprovalActionExtension(PXGraph graph)
    : base(graph)
  {
  }

  protected override void Init(PXGraph graph)
  {
    base.Init(graph);
    PXGraph graph1 = graph;
    EPApprovalActionExtension<SourceAssign, Approved, Rejected, ApprovalMapID, ApprovalNotificationID> approvalActionExtension = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate handler = new PXButtonDelegate((object) approvalActionExtension, __vmethodptr(approvalActionExtension, Submit));
    this.AddAction(graph1, "Submit", handler);
    // ISSUE: method pointer
    graph.RowSelected.AddHandler<SourceAssign>(new PXRowSelected((object) this, __methodptr(RowSelected)));
  }

  public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    bool configuration = this.GetConfiguration(out int? _, out int? _);
    ((PXSelectBase) this)._Graph.Actions["Approve"].SetVisible(configuration);
    ((PXSelectBase) this)._Graph.Actions["Reject"].SetVisible(configuration);
  }

  [PXUIField(DisplayName = "Submit")]
  [PXButton]
  public virtual IEnumerable Submit(PXAdapter adapter)
  {
    EPApprovalActionExtension<SourceAssign, Approved, Rejected, ApprovalMapID, ApprovalNotificationID> approvalActionExtension = this;
    foreach (SourceAssign sourceAssign in adapter.Get<SourceAssign>())
    {
      approvalActionExtension.DoSubmit(sourceAssign);
      EPApprovalAction<SourceAssign, Approved, Rejected>.StatusDelegate statusHandler = approvalActionExtension.StatusHandler;
      if (statusHandler != null)
        statusHandler(sourceAssign);
      ((PXSelectBase) approvalActionExtension)._Graph.Caches[typeof (SourceAssign)].Update((object) sourceAssign);
      if (approvalActionExtension.Persistent)
        ((PXSelectBase) approvalActionExtension)._Graph.Persist();
      yield return (object) sourceAssign;
    }
  }

  protected void DoSubmit(SourceAssign item)
  {
    int? mapID;
    int? notificationID;
    if (!this.GetConfiguration(out mapID, out notificationID))
      return;
    this.Assign(item, mapID, notificationID);
  }

  public override ApprovalResult GetResult(SourceAssign source)
  {
    PXCache cach = ((PXSelectBase) this)._Graph.Caches[typeof (SourceAssign)];
    bool valueOrDefault1 = ((bool?) cach.GetValue<Approved>((object) source)).GetValueOrDefault();
    bool valueOrDefault2 = ((bool?) cach.GetValue<Rejected>((object) source)).GetValueOrDefault();
    if (valueOrDefault1)
      return ApprovalResult.Approved;
    if (valueOrDefault2)
      return ApprovalResult.Rejected;
    return this.GetConfiguration(out int? _, out int? _) ? ApprovalResult.PendingApproval : ApprovalResult.Submitted;
  }

  protected bool GetConfiguration(out int? mapID, out int? notificationID)
  {
    mapID = new int?();
    notificationID = new int?();
    PXCache cach = ((PXSelectBase) this)._Graph.Caches[typeof (ApprovalMapID).DeclaringType];
    object current = cach?.Current;
    if (current == null)
      return false;
    mapID = cach.GetValue<ApprovalMapID>(current) as int?;
    notificationID = cach.GetValue<ApprovalNotificationID>(current) as int?;
    return mapID.HasValue;
  }
}
