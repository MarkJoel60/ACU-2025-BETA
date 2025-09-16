// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPApprovalAutomation`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.EP;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

public class EPApprovalAutomation<SourceAssign, Approved, Rejected, Hold, SetupApproval> : 
  EPApprovalList<SourceAssign, Approved, Rejected>
  where SourceAssign : class, IAssign, IBqlTable, new()
  where Approved : class, IBqlField
  where Rejected : class, IBqlField
  where Hold : class, IBqlField
  where SetupApproval : class, IBqlTable, new()
{
  public EPApprovalAutomation(PXGraph graph, Delegate @delegate)
    : base(graph, @delegate)
  {
    this.Initialize(graph);
  }

  public EPApprovalAutomation(PXGraph graph)
    : base(graph)
  {
    this.Initialize(graph);
  }

  private void Initialize(PXGraph graph)
  {
    PXGraph.FieldVerifyingEvents fieldVerifying1 = graph.FieldVerifying;
    Type itemType1 = BqlCommand.GetItemType(typeof (Approved));
    string name1 = typeof (Approved).Name;
    EPApprovalAutomation<SourceAssign, Approved, Rejected, Hold, SetupApproval> approvalAutomation1 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying1 = new PXFieldVerifying((object) approvalAutomation1, __vmethodptr(approvalAutomation1, Approved_FieldVerifying));
    fieldVerifying1.AddHandler(itemType1, name1, pxFieldVerifying1);
    PXGraph.FieldVerifyingEvents fieldVerifying2 = graph.FieldVerifying;
    Type itemType2 = BqlCommand.GetItemType(typeof (Rejected));
    string name2 = typeof (Rejected).Name;
    EPApprovalAutomation<SourceAssign, Approved, Rejected, Hold, SetupApproval> approvalAutomation2 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying2 = new PXFieldVerifying((object) approvalAutomation2, __vmethodptr(approvalAutomation2, Rejected_FieldVerifying));
    fieldVerifying2.AddHandler(itemType2, name2, pxFieldVerifying2);
    PXGraph.FieldUpdatedEvents fieldUpdated1 = graph.FieldUpdated;
    Type itemType3 = BqlCommand.GetItemType(typeof (Approved));
    string name3 = typeof (Approved).Name;
    EPApprovalAutomation<SourceAssign, Approved, Rejected, Hold, SetupApproval> approvalAutomation3 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) approvalAutomation3, __vmethodptr(approvalAutomation3, Approved_FieldUpdated));
    fieldUpdated1.AddHandler(itemType3, name3, pxFieldUpdated1);
    PXGraph.FieldUpdatedEvents fieldUpdated2 = graph.FieldUpdated;
    Type itemType4 = BqlCommand.GetItemType(typeof (Rejected));
    string name4 = typeof (Rejected).Name;
    EPApprovalAutomation<SourceAssign, Approved, Rejected, Hold, SetupApproval> approvalAutomation4 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) approvalAutomation4, __vmethodptr(approvalAutomation4, Rejected_FieldUpdated));
    fieldUpdated2.AddHandler(itemType4, name4, pxFieldUpdated2);
    PXGraph.FieldDefaultingEvents fieldDefaulting1 = graph.FieldDefaulting;
    Type itemType5 = BqlCommand.GetItemType(typeof (Hold));
    string name5 = typeof (Hold).Name;
    EPApprovalAutomation<SourceAssign, Approved, Rejected, Hold, SetupApproval> approvalAutomation5 = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting1 = new PXFieldDefaulting((object) approvalAutomation5, __vmethodptr(approvalAutomation5, Hold_FieldDefaulting));
    fieldDefaulting1.AddHandler(itemType5, name5, pxFieldDefaulting1);
    PXGraph.FieldDefaultingEvents fieldDefaulting2 = graph.FieldDefaulting;
    Type itemType6 = BqlCommand.GetItemType(typeof (Approved));
    string name6 = typeof (Approved).Name;
    EPApprovalAutomation<SourceAssign, Approved, Rejected, Hold, SetupApproval> approvalAutomation6 = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting2 = new PXFieldDefaulting((object) approvalAutomation6, __vmethodptr(approvalAutomation6, Approved_FieldDefaulting));
    fieldDefaulting2.AddHandler(itemType6, name6, pxFieldDefaulting2);
    // ISSUE: method pointer
    graph.Initialized += new PXGraphInitializedDelegate((object) this, __methodptr(InitLastEvents));
  }

  private void InitLastEvents(PXGraph graph)
  {
    PXGraph.RowUpdatedEvents rowUpdated = graph.RowUpdated;
    EPApprovalAutomation<SourceAssign, Approved, Rejected, Hold, SetupApproval> approvalAutomation = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) approvalAutomation, __vmethodptr(approvalAutomation, RowUpdated));
    rowUpdated.AddHandler<SourceAssign>(pxRowUpdated);
  }

  public virtual List<ApprovalMap> GetAssignedMaps(SourceAssign doc, PXCache cache)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>())
      return new List<ApprovalMap>();
    PXResultset<SetupApproval> pxResultset = PXSetup<SetupApproval>.SelectMultiBound(cache.Graph, new object[1]
    {
      (object) doc
    }, Array.Empty<object>());
    int count = pxResultset.Count;
    List<ApprovalMap> assignedMaps = new List<ApprovalMap>();
    for (int index = 0; index < count; ++index)
    {
      IAssignedMap assignedMap = (IAssignedMap) (object) PXResult<SetupApproval>.op_Implicit(pxResultset[index]);
      if (assignedMap.IsActive.GetValueOrDefault())
      {
        int? assignmentMapId = assignedMap.AssignmentMapID;
        if (assignmentMapId.HasValue)
        {
          List<ApprovalMap> approvalMapList = assignedMaps;
          assignmentMapId = assignedMap.AssignmentMapID;
          ApprovalMap approvalMap = new ApprovalMap(assignmentMapId.Value, assignedMap.AssignmentNotificationID);
          approvalMapList.Add(approvalMap);
        }
      }
    }
    return assignedMaps;
  }

  protected virtual void Approved_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (this.SuppressInFull || !(e.Row is SourceAssign row))
      return;
    if (((bool?) e.NewValue).GetValueOrDefault() && !this.IsApprover(row))
    {
      if (this.ApprovedByCurrentUser(row))
        throw new PXSetPropertyException((IBqlTable) row, "You have already approved the document.", (PXErrorLevel) 1);
      if (sender.GetAttributesReadonly<Approved>((object) row).OfType<PXUIFieldAttribute>().Any<PXUIFieldAttribute>((Func<PXUIFieldAttribute, bool>) (attribute => attribute.Visible)))
        PXUIFieldAttribute.SetError<Approved>(sender, (object) row, "You are not an authorized approver for this document.");
      throw new PXSetPropertyException("You are not an authorized approver for this document.");
    }
    PXUIFieldAttribute.SetError<Approved>(sender, (object) row, (string) null);
  }

  protected virtual void Rejected_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (this.SuppressInFull)
      return;
    if (((bool?) e.NewValue).GetValueOrDefault() && !this.IsApprover((SourceAssign) e.Row))
    {
      e.NewValue = (object) false;
      if (sender.GetAttributesReadonly<Approved>(e.Row).OfType<PXUIFieldAttribute>().Any<PXUIFieldAttribute>((Func<PXUIFieldAttribute, bool>) (attribute => attribute.Visible)))
        PXUIFieldAttribute.SetError<Approved>(sender, e.Row, "You are not an authorized approver for this document.");
      throw new PXSetPropertyException("You are not an authorized approver for this document.");
    }
    PXUIFieldAttribute.SetError<Approved>(sender, e.Row, (string) null);
  }

  protected virtual void Approved_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    if (this.SuppressInFull)
      return;
    SourceAssign row = (SourceAssign) e.Row;
    if (e.Row == null || !((bool?) cache.GetValue<Approved>((object) row)).GetValueOrDefault() || ((bool?) e.OldValue).GetValueOrDefault())
      return;
    cache.SetValue<Approved>((object) row, (object) false);
    try
    {
      if (!this.Approve(row))
        return;
      cache.SetValue<Approved>((object) row, (object) this.IsApproved(row));
    }
    catch (ReasonRejectedException ex)
    {
    }
  }

  protected virtual void Rejected_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    if (this.SuppressInFull)
      return;
    SourceAssign row = (SourceAssign) e.Row;
    if (e.Row == null || !((bool?) cache.GetValue<Rejected>((object) row)).GetValueOrDefault() || ((bool?) e.OldValue).GetValueOrDefault())
      return;
    cache.SetValue<Rejected>((object) row, (object) false);
    try
    {
      if (!this.Reject(row))
        return;
      cache.SetValue<Rejected>((object) row, (object) this.IsRejected(row));
    }
    catch (ReasonRejectedException ex)
    {
    }
  }

  protected virtual void RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (this.SuppressInFull)
      return;
    SourceAssign row = (SourceAssign) e.Row;
    SourceAssign oldRow = (SourceAssign) e.OldRow;
    if ((object) oldRow == null || (object) row == null)
      return;
    bool? nullable1 = (bool?) cache.GetValue<Approved>((object) row);
    bool? nullable2 = (bool?) cache.GetValue<Hold>((object) row);
    bool? nullable3 = (bool?) cache.GetValue<Hold>((object) oldRow);
    if (!nullable3.HasValue)
      return;
    if (nullable2.GetValueOrDefault())
    {
      bool? nullable4 = nullable3;
      bool flag = false;
      if (!(nullable4.GetValueOrDefault() == flag & nullable4.HasValue))
        return;
      this.ClearRelatedApprovals(row);
      cache.SetDefaultExt<Approved>((object) row);
      cache.SetDefaultExt<Rejected>((object) row);
    }
    else
    {
      if (nullable1.GetValueOrDefault() || !this.AllowAssign(cache, oldRow, row))
        return;
      this.AssignMaps(cache, row);
    }
  }

  protected virtual void AssignMaps(PXCache cache, SourceAssign doc)
  {
    List<ApprovalMap> assignedMaps = this.GetAssignedMaps(doc, cache);
    if (assignedMaps.Any<ApprovalMap>())
    {
      try
      {
        this.Assign(doc, (IEnumerable<ApprovalMap>) assignedMaps);
      }
      catch (PXSetPropertyException ex)
      {
        cache.SetValue<Hold>((object) doc, (object) true);
        throw new PXException("Unable to process the approval.\n{0}", new object[1]
        {
          (object) ((Exception) ((object) ((Exception) ex).InnerException ?? (object) ex)).Message
        });
      }
    }
    else
    {
      cache.SetValue<Approved>((object) doc, (object) true);
      cache.SetValue<Rejected>((object) doc, (object) false);
    }
  }

  protected virtual bool AllowAssign(PXCache cache, SourceAssign oldDoc, SourceAssign doc)
  {
    return ((bool?) cache.GetValue<Hold>((object) oldDoc)).GetValueOrDefault();
  }

  protected virtual void Hold_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    if (this.SuppressInFull)
      return;
    SourceAssign row = (SourceAssign) e.Row;
    if (!this.GetAssignedMaps(row, cache).Any<ApprovalMap>())
      return;
    cache.SetValue<Hold>((object) row, (object) true);
  }

  protected virtual void Approved_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    if (this.SuppressInFull)
      return;
    SourceAssign row = (SourceAssign) e.Row;
    if (((bool?) cache.GetValue<Hold>((object) row)).GetValueOrDefault() || this.GetAssignedMaps(row, cache).Any<ApprovalMap>())
      return;
    cache.SetValue<Approved>((object) row, (object) true);
  }
}
