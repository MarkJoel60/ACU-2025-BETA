// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPApprovalList`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.EP;
using PX.Objects.CS;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

public class EPApprovalList<SourceAssign, Approved, Rejected> : 
  EPDependNoteList<EPApproval, EPApproval.refNoteID, SourceAssign>
  where SourceAssign : class, IAssign, IBqlTable, new()
  where Approved : class, IBqlField
  where Rejected : class, IBqlField
{
  protected PXView _Pending;
  protected PXView _Find;
  protected PXView _FindOwner;
  protected PXView _FindWithWorkgroup;
  protected PXView _FindOwnerWithoutWorkgroup;
  protected PXView _ApprovedByOwnerOrApprover;
  protected PXView _ApprovedByWorkgroup;
  protected PXView _Rejected;
  protected PXView _RelatedApprovalsToDelete;
  protected PXView _Except;
  protected PXAction _Activity;
  protected PXFilter<ReasonApproveRejectFilter> ReasonApproveRejectParams;
  protected PXFilter<PX.Objects.EP.ReassignApprovalFilter> ReassignApprovalFilter;
  public bool SuppressApproval;
  private bool _suppressInFull;

  public bool SuppressInFull
  {
    get => this._suppressInFull;
    set
    {
      this._suppressInFull = value;
      if (!value)
        return;
      this.SuppressApproval = true;
    }
  }

  public EPApprovalList(PXGraph graph, Delegate @delegate)
    : base(graph, @delegate)
  {
  }

  public EPApprovalList(PXGraph graph)
    : base(graph)
  {
  }

  protected override void Init(PXGraph graph)
  {
    this._Activity = graph.Actions["RegisterActivity"];
    string str = $"EPApprovalList_{graph.GetType().FullName}_{typeof (SourceAssign).FullName}_{typeof (Approved).FullName}_{typeof (Rejected).FullName}";
    ((PXSelectBase) this).View = this.CreateView(str + "_View", graph, BqlCommand.Compose(new Type[9]
    {
      typeof (Select2<,,>),
      typeof (EPApproval),
      typeof (LeftJoin<ApproverEmployee, On<ApproverEmployee.defContactID, Equal<EPApproval.ownerID>>, LeftJoin<ApprovedByEmployee, On<ApprovedByEmployee.defContactID, Equal<EPApproval.approvedByID>>>>),
      typeof (Where<,,>),
      typeof (EPApproval.refNoteID),
      typeof (Equal<>),
      typeof (Current<>),
      this.SourceNoteID,
      typeof (And<EPApproval.isPreApproved, Equal<False>>)
    }));
    this._Pending = this.CreateView(str + "_Pending", graph, BqlCommand.Compose(new Type[8]
    {
      typeof (Select<,>),
      typeof (EPApproval),
      typeof (Where<,,>),
      typeof (EPApproval.refNoteID),
      typeof (Equal<>),
      typeof (Required<>),
      this.SourceNoteID,
      typeof (And<EPApproval.status, Equal<EPApprovalStatus.pending>>)
    }));
    this._Find = this.CreateView(str + "_Find", graph, BqlCommand.Compose(new Type[9]
    {
      typeof (Select2<,,>),
      typeof (EPApproval),
      typeof (LeftJoin<EPRule, On<EPRule.ruleID, Equal<EPApproval.ruleID>>>),
      typeof (Where<,,>),
      typeof (EPApproval.refNoteID),
      typeof (Equal<>),
      typeof (Required<>),
      this.SourceNoteID,
      typeof (And<EPApproval.assignmentMapID, Equal<Required<EPApproval.assignmentMapID>>, And<EPApproval.workgroupID, Equal<Required<EPApproval.workgroupID>>, And<EPApproval.stepID, Equal<Required<EPApproval.stepID>>>>>)
    }));
    this._FindOwner = this.CreateView(str + "_FindOwner", graph, BqlCommand.Compose(new Type[9]
    {
      typeof (Select2<,,>),
      typeof (EPApproval),
      typeof (LeftJoin<EPRule, On<EPRule.ruleID, Equal<EPApproval.ruleID>>>),
      typeof (Where<,,>),
      typeof (EPApproval.refNoteID),
      typeof (Equal<>),
      typeof (Required<>),
      this.SourceNoteID,
      typeof (And<EPApproval.assignmentMapID, Equal<Required<EPApproval.assignmentMapID>>, And<EPApproval.origOwnerID, Equal<Required<EPApproval.origOwnerID>>, And<EPApproval.stepID, Equal<Required<EPApproval.stepID>>>>>)
    }));
    this._FindWithWorkgroup = this.CreateView(str + "_FindWithWorkgroup", graph, BqlCommand.Compose(new Type[9]
    {
      typeof (Select2<,,>),
      typeof (EPApproval),
      typeof (LeftJoin<EPRule, On<EPRule.ruleID, Equal<EPApproval.ruleID>>>),
      typeof (Where<,,>),
      typeof (EPApproval.refNoteID),
      typeof (Equal<>),
      typeof (Required<>),
      this.SourceNoteID,
      typeof (And<EPApproval.assignmentMapID, Equal<Required<EPApproval.assignmentMapID>>, And<EPApproval.workgroupID, Equal<Required<EPApproval.workgroupID>>>>)
    }));
    this._FindOwnerWithoutWorkgroup = this.CreateView(str + "_FindOwnerWithoutWorkgroup", graph, BqlCommand.Compose(new Type[9]
    {
      typeof (Select2<,,>),
      typeof (EPApproval),
      typeof (LeftJoin<EPRule, On<EPRule.ruleID, Equal<EPApproval.ruleID>>>),
      typeof (Where<,,>),
      typeof (EPApproval.refNoteID),
      typeof (Equal<>),
      typeof (Required<>),
      this.SourceNoteID,
      typeof (And<EPApproval.assignmentMapID, Equal<Required<EPApproval.assignmentMapID>>, And<EPApproval.workgroupID, IsNull, And<EPApproval.origOwnerID, Equal<Required<EPApproval.origOwnerID>>>>>)
    }));
    this._ApprovedByOwnerOrApprover = this.CreateView(str + "_ApprovedByOwner", graph, BqlCommand.Compose(new Type[8]
    {
      typeof (Select<,>),
      typeof (EPApproval),
      typeof (Where<,,>),
      typeof (EPApproval.refNoteID),
      typeof (Equal<>),
      typeof (Required<>),
      this.SourceNoteID,
      typeof (And<EPApproval.status, Equal<EPApprovalStatus.approved>, And<Where<EPApproval.ownerID, Equal<Required<EPApproval.ownerID>>, Or<EPApproval.approvedByID, Equal<Required<EPApproval.ownerID>>, Or<EPApproval.origOwnerID, Equal<Required<EPApproval.origOwnerID>>, Or<EPApproval.approvedByID, Equal<Required<EPApproval.origOwnerID>>>>>>>>)
    }));
    this._ApprovedByWorkgroup = this.CreateView(str + "_ApprovedByOwner", graph, BqlCommand.Compose(new Type[8]
    {
      typeof (Select<,>),
      typeof (EPApproval),
      typeof (Where<,,>),
      typeof (EPApproval.refNoteID),
      typeof (Equal<>),
      typeof (Required<>),
      this.SourceNoteID,
      typeof (And<EPApproval.status, Equal<EPApprovalStatus.approved>, And<EPApproval.workgroupID, Equal<Required<EPApproval.workgroupID>>>>)
    }));
    this._Rejected = this.CreateView(str + "_Rejected", graph, BqlCommand.Compose(new Type[8]
    {
      typeof (Select<,>),
      typeof (EPApproval),
      typeof (Where<,,>),
      typeof (EPApproval.refNoteID),
      typeof (Equal<>),
      typeof (Required<>),
      this.SourceNoteID,
      typeof (And<EPApproval.status, Equal<EPApprovalStatus.rejected>>)
    }));
    this._RelatedApprovalsToDelete = this.CreateView(str + "_RelatedApprovalsToDelete", graph, BqlCommand.Compose(new Type[8]
    {
      typeof (Select<,>),
      typeof (EPApproval),
      typeof (Where<,,>),
      typeof (EPApproval.refNoteID),
      typeof (Equal<>),
      typeof (Required<>),
      this.SourceNoteID,
      typeof (And<Where<EPApproval.status, Equal<EPApprovalStatus.approved>, Or<EPApproval.status, Equal<EPApprovalStatus.pending>, Or<Where<EPApproval.status, Equal<EPApprovalStatus.rejected>, And<EPApproval.reason, IsNull>>>>>>)
    }));
    this._Except = this.CreateView(str + "_Except", graph, BqlCommand.Compose(new Type[8]
    {
      typeof (Select<,>),
      typeof (EPApproval),
      typeof (Where<,,>),
      typeof (EPApproval.refNoteID),
      typeof (Equal<>),
      typeof (Required<>),
      this.SourceNoteID,
      typeof (And<EPApproval.ruleID, NotEqual<Required<EPApproval.ruleID>>, And<EPApproval.status, NotEqual<EPApprovalStatus.approved>>>)
    }));
    PXGraph.RowPersistedEvents rowPersisted = graph.RowPersisted;
    EPApprovalList<SourceAssign, Approved, Rejected> epApprovalList1 = this;
    // ISSUE: virtual method pointer
    PXRowPersisted pxRowPersisted = new PXRowPersisted((object) epApprovalList1, __vmethodptr(epApprovalList1, OnPersisted));
    rowPersisted.AddHandler<SourceAssign>(pxRowPersisted);
    this.ReasonApproveRejectParams = new PXFilter<ReasonApproveRejectFilter>(graph);
    graph.ViewNames[((PXSelectBase) this.ReasonApproveRejectParams).View] = "ReasonApproveRejectParams";
    graph.Views.Add("ReasonApproveRejectParams", ((PXSelectBase) this.ReasonApproveRejectParams).View);
    this.ReassignApprovalFilter = new PXFilter<PX.Objects.EP.ReassignApprovalFilter>(graph);
    graph.ViewNames[((PXSelectBase) this.ReassignApprovalFilter).View] = "ReassignApprovalFilter";
    graph.Views.Add("ReassignApprovalFilter", ((PXSelectBase) this.ReassignApprovalFilter).View);
    PXGraph graph1 = graph;
    EPApprovalList<SourceAssign, Approved, Rejected> epApprovalList2 = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate handler = new PXButtonDelegate((object) epApprovalList2, __vmethodptr(epApprovalList2, ReassignApproval));
    PXEventSubscriberAttribute[] subscriberAttributeArray = Array.Empty<PXEventSubscriberAttribute>();
    this.AddAction(graph1, "ReassignApproval", "Reassign", true, handler, subscriberAttributeArray);
    base.Init(graph);
  }

  internal PXAction AddAction(
    PXGraph graph,
    string name,
    string displayName,
    bool visible,
    PXButtonDelegate handler,
    params PXEventSubscriberAttribute[] attrs)
  {
    PXUIFieldAttribute pxuiFieldAttribute = new PXUIFieldAttribute()
    {
      DisplayName = PXMessages.LocalizeNoPrefix(displayName),
      MapEnableRights = (PXCacheRights) 1
    };
    if (!visible)
      pxuiFieldAttribute.Visible = false;
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>()
    {
      (PXEventSubscriberAttribute) pxuiFieldAttribute
    };
    if (attrs != null)
      subscriberAttributeList.AddRange(((IEnumerable<PXEventSubscriberAttribute>) attrs).Where<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (attr => attr != null)));
    PXNamedAction<SourceAssign> pxNamedAction = new PXNamedAction<SourceAssign>(graph, name, handler, subscriberAttributeList.ToArray());
    graph.Actions[name] = (PXAction) pxNamedAction;
    return (PXAction) pxNamedAction;
  }

  [PXButton]
  public virtual IEnumerable ReassignApproval(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.EP.ReassignApprovalFilter>) this.ReassignApprovalFilter).AskExt() != 1 || !this.ReassignApprovalFilter.VerifyRequired())
      return adapter.Get();
    PX.Objects.EP.ReassignApprovalFilter current1 = ((PXSelectBase<PX.Objects.EP.ReassignApprovalFilter>) this.ReassignApprovalFilter).Current;
    object current2 = ((PXSelectBase) this)._Graph.Caches[typeof (SourceAssign)].Current;
    bool flag1 = false;
    bool flag2 = false;
    Dictionary<EPApproval, string> dictionary = new Dictionary<EPApproval, string>();
    PXView pending = this._Pending;
    object[] objArray = new object[1]
    {
      (object) this.GetSourceNoteID(current2)
    };
    foreach (EPApproval epApproval in pending.SelectMulti(objArray))
    {
      flag1 = true;
      if (!this.ValidateApprovalAccess(epApproval))
      {
        this._Pending.Cache.RaiseExceptionHandling<EPApproval.approvalID>((object) epApproval, (object) epApproval.ApprovalID, (Exception) new PXSetPropertyException<EPApproval.approvalID>("You are not an authorized approver for this document.", (PXErrorLevel) 5));
      }
      else
      {
        try
        {
          flag2 = true;
          EPApprovalHelper.ReassignToContact(((PXSelectBase) this)._Graph, epApproval, current1.NewApprover, current1.IgnoreApproversDelegations);
          ((PXSelectBase) this)._Graph.Caches[typeof (EPApproval)].Update((object) epApproval);
        }
        catch (Exception ex)
        {
          dictionary[epApproval] = ex.Message;
        }
      }
    }
    if (flag1 && !flag2)
      throw new PXSetPropertyException("You are not an authorized approver for this document.");
    if (flag2)
    {
      ((PXSelectBase) this)._Graph.Persist();
      ((PXSelectBase) this.ReassignApprovalFilter).Cache.Clear();
      ((PXSelectBase<PX.Objects.EP.ReassignApprovalFilter>) this.ReassignApprovalFilter).ClearDialog();
    }
    foreach (KeyValuePair<EPApproval, string> keyValuePair in dictionary)
      this._Pending.Cache.RaiseExceptionHandling<EPApproval.approvalID>((object) keyValuePair.Key, (object) keyValuePair.Key.ApprovalID, (Exception) new PXSetPropertyException<EPApproval.approvalID>(keyValuePair.Value, (PXErrorLevel) 5));
    return adapter.Get();
  }

  public void Assign(SourceAssign source, IEnumerable<ApprovalMap> maps)
  {
    if (this.SuppressInFull || (object) source == null)
      return;
    this.Reset(source);
    bool flag1 = false;
    bool flag2 = false;
    if (!this.SuppressApproval)
    {
      foreach (ApprovalMap map in maps)
      {
        PXResultset<EPAssignmentMap> pxResultset = PXSelectBase<EPAssignmentMap, PXSelectReadonly<EPAssignmentMap, Where<EPAssignmentMap.assignmentMapID, Equal<Required<EPAssignmentMap.assignmentMapID>>>>.Config>.SelectSingleBound(((PXSelectBase) this)._Graph, (object[]) null, new object[1]
        {
          (object) map.ID
        });
        try
        {
          this.DoMapReExecution(source, (EPApproval) null, PXResultset<EPAssignmentMap>.op_Implicit(pxResultset), map.NotificationID, true);
        }
        catch (RequestApproveException ex)
        {
          flag1 = true;
        }
        catch (RequestRejectException ex)
        {
          flag2 = true;
        }
      }
    }
    if (((PXSelectBase) this).Cache.Inserted.Count() != 0L)
      return;
    if (flag2)
    {
      ((PXSelectBase) this)._Graph.Caches[source.GetType()].SetValue<Approved>((object) source, (object) false);
      ((PXSelectBase) this)._Graph.Caches[source.GetType()].SetValue<Rejected>((object) source, (object) true);
    }
    else
    {
      if (!flag1 && !this.SuppressApproval)
        return;
      ((PXSelectBase) this)._Graph.Caches[source.GetType()].SetValue<Approved>((object) source, (object) true);
      ((PXSelectBase) this)._Graph.Caches[source.GetType()].SetValue<Rejected>((object) source, (object) false);
    }
  }

  public void Assign(SourceAssign source, int? assignmentMapID, int? notification)
  {
    if (this.SuppressInFull)
      return;
    this.Reset(source);
    PXResultset<EPAssignmentMap> pxResultset = PXSelectBase<EPAssignmentMap, PXSelectReadonly<EPAssignmentMap, Where<EPAssignmentMap.assignmentMapID, Equal<Required<EPAssignmentMap.assignmentMapID>>>>.Config>.SelectSingleBound(((PXSelectBase) this)._Graph, (object[]) null, new object[1]
    {
      (object) assignmentMapID
    });
    this.DoMapReExecution(source, (EPApproval) null, PXResultset<EPAssignmentMap>.op_Implicit(pxResultset), notification);
  }

  public virtual void Reset(SourceAssign source)
  {
    foreach (EPApproval epApproval in this._History.SelectMulti(new object[1]
    {
      (object) this.GetSourceNoteID((object) source)
    }))
      ((PXSelectBase) this).Cache.Delete((object) epApproval);
  }

  public virtual bool Approve(SourceAssign source)
  {
    if (this.SuppressInFull)
      return false;
    if ((EPApproval) this._Rejected.SelectSingle(new object[1]
    {
      (object) this.GetSourceNoteID((object) source)
    }) != null)
      throw new PXException("Cannot approve rejected document.");
    if (!this.UpdateApproval(source, "A"))
      return false;
    this.RegisterActivity((object) source, nameof (Approved));
    return true;
  }

  public virtual bool Reject(SourceAssign source)
  {
    if (this.SuppressInFull || !this.UpdateApproval(source, "R"))
      return false;
    this.RegisterActivity((object) source, nameof (Rejected));
    return true;
  }

  [Obsolete]
  public virtual void ClearPendingApproval(SourceAssign source)
  {
    if (this.SuppressInFull)
      return;
    foreach (EPApproval epApproval in this._Pending.SelectMulti(new object[1]
    {
      (object) this.GetSourceNoteID((object) source)
    }))
      ((PXSelectBase) this).Cache.Delete((object) epApproval);
  }

  public virtual void ClearRelatedApprovals(SourceAssign doc)
  {
    foreach (EPApproval epApproval in this._RelatedApprovalsToDelete.SelectMulti(new object[1]
    {
      (object) this.GetSourceNoteID((object) doc)
    }))
      ((PXSelectBase) this).Cache.Delete((object) epApproval);
  }

  public virtual bool IsApprover(SourceAssign source)
  {
    bool flag = true;
    PXView pending = this._Pending;
    object[] objArray = new object[1]
    {
      (object) this.GetSourceNoteID((object) source)
    };
    foreach (EPApproval epApproval in pending.SelectMulti(objArray))
    {
      flag = false;
      if (this.ValidateApprovalAccess(epApproval))
        return true;
    }
    return flag;
  }

  public virtual bool ApprovedByCurrentUser(SourceAssign source)
  {
    int? contactId = PXAccess.GetContactID();
    return this._ApprovedByOwnerOrApprover.SelectSingle(new object[5]
    {
      (object) this.GetSourceNoteID((object) source),
      (object) contactId,
      (object) contactId,
      (object) contactId,
      (object) contactId
    }) != null;
  }

  public virtual bool IsApproved(SourceAssign source)
  {
    this._Pending.Cache.ClearQueryCacheObsolete();
    this._Pending.Clear();
    Guid? sourceNoteId = this.GetSourceNoteID((object) source);
    if ((EPApproval) this._Pending.SelectSingle(new object[1]
    {
      (object) sourceNoteId
    }) != null)
      return false;
    return (EPApproval) this._Rejected.SelectSingle(new object[1]
    {
      (object) sourceNoteId
    }) == null;
  }

  public virtual bool IsRejected(SourceAssign source)
  {
    this._Rejected.Cache.ClearQueryCacheObsolete();
    this._Rejected.Clear();
    return (EPApproval) this._Rejected.SelectSingle(new object[1]
    {
      (object) this.GetSourceNoteID((object) source)
    }) != null;
  }

  public virtual ApprovalResult GetResult(SourceAssign source)
  {
    PXCache cach = ((PXSelectBase) this)._Graph.Caches[typeof (SourceAssign)];
    bool valueOrDefault1 = ((bool?) cach.GetValue<Approved>((object) source)).GetValueOrDefault();
    bool valueOrDefault2 = ((bool?) cach.GetValue<Rejected>((object) source)).GetValueOrDefault();
    if (valueOrDefault1)
      return ApprovalResult.Approved;
    return valueOrDefault2 ? ApprovalResult.Rejected : ApprovalResult.PendingApproval;
  }

  private bool UpdateApproval(SourceAssign source, string status)
  {
    bool flag = false;
    List<EPApproval> epApprovalList = new List<EPApproval>();
    PXView pending = this._Pending;
    object[] objArray = new object[1]
    {
      (object) this.GetSourceNoteID((object) source)
    };
    foreach (EPApproval approval in pending.SelectMulti(objArray))
    {
      if (this.ValidateApprovalAccess(approval))
      {
        EPRule epRule = PXResultset<EPRule>.op_Implicit(PXSelectBase<EPRule, PXSelectReadonly<EPRule, Where<EPRule.ruleID, Equal<Required<EPApproval.ruleID>>>>.Config>.SelectSingleBound(((PXSelectBase) this)._Graph, (object[]) null, new object[1]
        {
          (object) approval.RuleID
        }));
        if (epRule != null)
        {
          string str = status.Equals("A") ? epRule.ReasonForApprove : epRule.ReasonForReject;
          switch (str)
          {
            case "N":
              goto label_18;
            case "R":
              if (((PXSelectBase) this)._Graph.UnattendedMode)
                throw new PXException(status.Equals("A") ? "The document cannot be processed because a comment must be entered to approve the document. On the corresponding data entry form, click Approve on the form toolbar and enter a comment." : "The document cannot be processed because a comment must be entered to reject the document. On the corresponding data entry form, click Reject on the form toolbar and enter a comment.");
              break;
          }
          if (!((PXSelectBase) this)._Graph.UnattendedMode)
          {
            PXCache cache = ((PXSelectBase) this.ReasonApproveRejectParams).Cache;
            ReasonApproveRejectFilter current = ((PXSelectBase<ReasonApproveRejectFilter>) this.ReasonApproveRejectParams).Current;
            if (this.ReasonApproveRejectParams != null && current != null)
            {
              if (((PXSelectBase) this.ReasonApproveRejectParams).View.Answer == null || ((PXSelectBase) this.ReasonApproveRejectParams).View.Answer == 7)
                this.ReasonApproveRejectParams.Reset();
              PXDefaultAttribute defaultAttribute = cache != null ? cache.GetAttributes<ReasonApproveRejectFilter.reason>().OfType<PXDefaultAttribute>().FirstOrDefault<PXDefaultAttribute>() : (PXDefaultAttribute) null;
              if (defaultAttribute != null)
              {
                defaultAttribute.PersistingCheck = str.Equals("R") ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2;
                if (defaultAttribute.PersistingCheck == null && string.IsNullOrWhiteSpace(current.Reason))
                  PXUIFieldAttribute.SetError(cache, (object) current, "Reason", PXMessages.LocalizeFormat("The {0} approval rule requires that you enter a comment to complete this action for the selected document.", new object[1]
                  {
                    (object) epRule.Name
                  }));
                else
                  PXUIFieldAttribute.SetError(cache, (object) current, "Reason", (string) null, (string) null, false, (PXErrorLevel) 0);
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                // ISSUE: method pointer
                if (!this.ReasonApproveRejectParams.AskExtFullyValid(EPApprovalList<SourceAssign, Approved, Rejected>.\u003C\u003Ec.\u003C\u003E9__35_1 ?? (EPApprovalList<SourceAssign, Approved, Rejected>.\u003C\u003Ec.\u003C\u003E9__35_1 = new PXView.InitializePanel((object) EPApprovalList<SourceAssign, Approved, Rejected>.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CUpdateApproval\u003Eb__35_1))), (DialogAnswerType) 1, true))
                  throw new ReasonRejectedException();
                approval.Reason = current.Reason;
                this.ReasonApproveRejectParams.Reset();
              }
            }
          }
        }
label_18:
        approval.ApprovedByID = PXAccess.GetContactID();
        approval.ApproveDate = new DateTime?(PXTimeZoneInfo.Now);
        approval.Status = status;
        if (((PXSelectBase) this).Cache.Update((object) approval) != null)
        {
          flag = true;
          if (status == "A")
            this.OnApprovalApproved(source, approval, approval.AssignmentMapID, approval.NotificationID);
        }
      }
      if (approval.Status == "P")
        epApprovalList.Add(approval);
    }
    if (flag && status == "R")
    {
      epApprovalList.ForEach((Action<EPApproval>) (item => ((PXSelectBase) this).Cache.Delete((object) item)));
      ((PXSelectBase) this)._Graph.Caches[source.GetType()].SetValue<Approved>((object) source, (object) false);
      ((PXSelectBase) this)._Graph.Caches[source.GetType()].SetValue<Rejected>((object) source, (object) true);
    }
    return flag;
  }

  protected void OnApprovalApproved(
    SourceAssign source,
    EPApproval approval,
    int? assignmentMapID,
    int? notification)
  {
    EPRule epRule = PXResultset<EPRule>.op_Implicit(PXSelectBase<EPRule, PXSelectReadonly<EPRule, Where<EPRule.ruleID, Equal<Required<EPApproval.ruleID>>>>.Config>.SelectSingleBound(((PXSelectBase) this)._Graph, (object[]) null, new object[1]
    {
      (object) approval.RuleID
    }));
    switch (epRule?.ApproveType ?? "W")
    {
      case "A":
        if (PXSelectBase<EPApproval, PXSelectJoin<EPApproval, InnerJoin<EPRule, On<EPRule.ruleID, Equal<EPApproval.ruleID>, And<EPApproval.status, NotEqual<EPApprovalStatus.approved>, And<EPApproval.refNoteID, Equal<Required<EPApproval.refNoteID>>, And<EPApproval.ruleID, Equal<Required<EPRule.ruleID>>>>>>>>.Config>.SelectSingleBound(((PXSelectBase) this)._Graph, (object[]) null, new object[2]
        {
          (object) this.GetSourceNoteID((object) source),
          (object) approval.RuleID
        }).Count != 0)
          break;
        this._Except.SelectMulti(new object[2]
        {
          (object) this.GetSourceNoteID((object) source),
          (object) epRule.RuleID
        }).ForEach((Action<object>) (item => ((PXSelectBase) this).Cache.Delete(item)));
        ((PXSelectBase) this)._Graph.Caches[source.GetType()].SetValue<Approved>((object) source, (object) true);
        ((PXSelectBase) this)._Graph.Caches[source.GetType()].SetValue<Rejected>((object) source, (object) false);
        break;
      case "C":
        EPAssignmentMap map1 = PXResultset<EPAssignmentMap>.op_Implicit(PXSelectBase<EPAssignmentMap, PXSelectReadonly<EPAssignmentMap, Where<EPAssignmentMap.assignmentMapID, Equal<Required<EPApproval.assignmentMapID>>>>.Config>.SelectSingleBound(((PXSelectBase) this)._Graph, (object[]) null, new object[1]
        {
          (object) assignmentMapID
        }));
        if (PXSelectBase<EPApproval, PXSelectJoin<EPApproval, InnerJoin<EPRule, On<EPRule.ruleID, Equal<EPApproval.ruleID>, And<EPApproval.status, NotEqual<EPApprovalStatus.approved>, And<EPApproval.refNoteID, Equal<Required<EPApproval.refNoteID>>, And<EPApproval.ruleID, Equal<Required<EPRule.ruleID>>>>>>>>.Config>.SelectSingleBound(((PXSelectBase) this)._Graph, (object[]) null, new object[2]
        {
          (object) this.GetSourceNoteID((object) source),
          (object) approval.RuleID
        }).Count == 0)
          this._Except.SelectMulti(new object[2]
          {
            (object) this.GetSourceNoteID((object) source),
            (object) epRule.RuleID
          }).ForEach((Action<object>) (item => ((PXSelectBase) this).Cache.Delete(item)));
        this.DoMapReExecution(source, approval, map1, notification);
        break;
      default:
        EPAssignmentMap map2 = PXResultset<EPAssignmentMap>.op_Implicit(PXSelectBase<EPAssignmentMap, PXSelectReadonly<EPAssignmentMap, Where<EPAssignmentMap.assignmentMapID, Equal<Required<EPApproval.assignmentMapID>>>>.Config>.SelectSingleBound(((PXSelectBase) this)._Graph, (object[]) null, new object[1]
        {
          (object) assignmentMapID
        }));
        this.DoMapReExecution(source, approval, map2, notification);
        break;
    }
  }

  protected bool OnApprovalApprovedWithoutReExecution(
    SourceAssign source,
    EPApproval approval,
    int? assignmentMapID,
    int? notification)
  {
    EPRule epRule = PXResultset<EPRule>.op_Implicit(PXSelectBase<EPRule, PXSelectReadonly<EPRule, Where<EPRule.ruleID, Equal<Required<EPApproval.ruleID>>>>.Config>.SelectSingleBound(((PXSelectBase) this)._Graph, (object[]) null, new object[1]
    {
      (object) approval.RuleID
    }));
    bool flag = PXSelectBase<EPApproval, PXSelectJoin<EPApproval, InnerJoin<EPRule, On<EPRule.ruleID, Equal<EPApproval.ruleID>, And<EPApproval.status, NotEqual<EPApprovalStatus.approved>, And<EPApproval.refNoteID, Equal<Required<EPApproval.refNoteID>>, And<EPApproval.ruleID, Equal<Required<EPRule.ruleID>>>>>>>>.Config>.SelectSingleBound(((PXSelectBase) this)._Graph, (object[]) null, new object[2]
    {
      (object) this.GetSourceNoteID((object) source),
      (object) approval.RuleID
    }).Count == 0;
    switch (epRule?.ApproveType ?? "W")
    {
      case "A":
        if (flag)
        {
          this._Except.SelectMulti(new object[2]
          {
            (object) this.GetSourceNoteID((object) source),
            (object) epRule.RuleID
          }).ForEach((Action<object>) (item => ((PXSelectBase) this).Cache.Delete(item)));
          ((PXSelectBase) this)._Graph.Caches[source.GetType()].SetValue<Approved>((object) source, (object) true);
          ((PXSelectBase) this)._Graph.Caches[source.GetType()].SetValue<Rejected>((object) source, (object) false);
        }
        return false;
      case "C":
        if (flag)
          this._Except.SelectMulti(new object[2]
          {
            (object) this.GetSourceNoteID((object) source),
            (object) epRule.RuleID
          }).ForEach((Action<object>) (item => ((PXSelectBase) this).Cache.Delete(item)));
        return true;
      default:
        return true;
    }
  }

  protected void DoMapReExecution(
    SourceAssign source,
    EPApproval approved,
    EPAssignmentMap map,
    int? notification,
    bool isMultimap = false)
  {
    try
    {
      int num = 0;
      do
        ;
      while (this.MapReExecutionInternal(source, approved, map, notification, num++));
      if (this.IsApproved(source))
        throw new RequestApproveException();
    }
    catch (RequestApproveException ex)
    {
      if (isMultimap)
        throw;
      ((PXSelectBase) this)._Graph.Caches[source.GetType()].SetValue<Approved>((object) source, (object) true);
      ((PXSelectBase) this)._Graph.Caches[source.GetType()].SetValue<Rejected>((object) source, (object) false);
    }
    catch (RequestRejectException ex)
    {
      if (isMultimap)
        throw;
      ((PXSelectBase) this)._Graph.Caches[source.GetType()].SetValue<Approved>((object) source, (object) false);
      ((PXSelectBase) this)._Graph.Caches[source.GetType()].SetValue<Rejected>((object) source, (object) true);
    }
  }

  protected bool MapReExecutionInternal(
    SourceAssign source,
    EPApproval approved,
    EPAssignmentMap map,
    int? notification,
    int currentStepSequence)
  {
    bool flag1 = true;
    EPRule epRule1 = (EPRule) null;
    if (map != null)
    {
      epRule1 = PXResultset<EPRule>.op_Implicit(PXSelectBase<EPRule, PXSelect<EPRule, Where<EPRule.assignmentMapID, Equal<Required<EPAssignmentMap.assignmentMapID>>, And<EPRule.sequence, Greater<Required<EPRule.sequence>>, And<EPRule.isActive, Equal<boolTrue>, And<EPRule.stepID, IsNull>>>>, OrderBy<Asc<EPRule.sequence>>>.Config>.SelectSingleBound(((PXSelectBase) this)._Graph, (object[]) null, new object[2]
      {
        (object) map.AssignmentMapID,
        (object) currentStepSequence
      }));
      if (epRule1?.ExecuteStep == "O")
      {
        if (PXSelectBase<EPApproval, PXSelect<EPApproval, Where<EPApproval.refNoteID, Equal<Required<EPApproval.refNoteID>>>>.Config>.SelectSingleBound(((PXSelectBase) this)._Graph, (object[]) null, new object[1]
        {
          (object) this.GetSourceNoteID((object) source)
        }).Count != 0)
          return flag1;
      }
    }
    using (new CRAssigmentScope((IAssign) source))
    {
      int? nullable1;
      if (map != null)
      {
        nullable1 = map.MapType;
        int num = 0;
        if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
        {
          // ISSUE: variable of a boxed type
          __Boxed<SourceAssign> local1 = (object) source;
          int? nullable2;
          if (approved == null)
          {
            nullable1 = new int?();
            nullable2 = nullable1;
          }
          else
            nullable2 = approved.WorkgroupID;
          local1.WorkgroupID = nullable2;
          // ISSUE: variable of a boxed type
          __Boxed<SourceAssign> local2 = (object) source;
          int? nullable3;
          if (approved == null)
          {
            nullable1 = new int?();
            nullable3 = nullable1;
          }
          else
            nullable3 = approved.OwnerID;
          local2.OwnerID = nullable3;
        }
      }
      foreach (ApproveInfo approveInfo in this.GetApproversFromNextStep(source, map, new int?(currentStepSequence)))
      {
        PXResult<EPApproval, EPRule> pxResult = (PXResult<EPApproval, EPRule>) null;
        if (map != null)
        {
          nullable1 = map.MapType;
          int num = 0;
          if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
          {
            if (!approveInfo.WorkgroupID.HasValue)
              pxResult = (PXResult<EPApproval, EPRule>) this._FindOwnerWithoutWorkgroup.SelectSingle(new object[3]
              {
                (object) this.GetSourceNoteID((object) source),
                (object) map.AssignmentMapID,
                (object) approveInfo.OwnerID
              });
            else
              pxResult = (PXResult<EPApproval, EPRule>) this._FindWithWorkgroup.SelectSingle(new object[3]
              {
                (object) this.GetSourceNoteID((object) source),
                (object) map.AssignmentMapID,
                (object) approveInfo.WorkgroupID
              });
          }
          else if (!approveInfo.WorkgroupID.HasValue)
            pxResult = (PXResult<EPApproval, EPRule>) this._FindOwner.SelectSingle(new object[4]
            {
              (object) this.GetSourceNoteID((object) source),
              (object) map.AssignmentMapID,
              (object) approveInfo.OwnerID,
              (object) approveInfo.StepID
            });
          else
            pxResult = (PXResult<EPApproval, EPRule>) this._Find.SelectSingle(new object[4]
            {
              (object) this.GetSourceNoteID((object) source),
              (object) map.AssignmentMapID,
              (object) approveInfo.WorkgroupID,
              (object) approveInfo.StepID
            });
        }
        else if (approved != null)
          break;
        EPApproval epApproval1 = PXResult<EPApproval, EPRule>.op_Implicit(pxResult);
        EPRule epRule2 = PXResult<EPApproval, EPRule>.op_Implicit(pxResult);
        if (epApproval1 == null)
        {
          EPApproval epApproval2 = new EPApproval();
          epApproval2.RefNoteID = this.GetSourceNoteID((object) source);
          epApproval2.WorkgroupID = approveInfo.WorkgroupID;
          epApproval2.OrigOwnerID = approveInfo.OwnerID;
          epApproval2.OwnerID = approveInfo.OwnerID;
          epApproval2.RuleID = approveInfo.RuleID;
          epApproval2.StepID = approveInfo.StepID;
          epApproval2.WaitTime = approveInfo.WaitTime;
          epApproval2.Status = "P";
          int? nullable4;
          if (map == null)
          {
            nullable1 = new int?();
            nullable4 = nullable1;
          }
          else
            nullable4 = map.AssignmentMapID;
          epApproval2.AssignmentMapID = nullable4;
          epApproval2.NotificationID = notification;
          EPApproval approval1 = epApproval2;
          EPRule.PK.Find(((PXSelectBase) this)._Graph, approval1.RuleID);
          int? delegationRecordID = new int?();
          approval1.OwnerID = EPApprovalHelper.GetTodayApproverContactID(((PXSelectBase) this)._Graph, approval1.OrigOwnerID, ref delegationRecordID);
          nullable1 = approval1.OwnerID;
          int? nullable5 = approval1.OrigOwnerID;
          if (!(nullable1.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable1.HasValue == nullable5.HasValue))
            PXTrace.WriteInformation("Reassigned: {0} - {1}", new object[2]
            {
              (object) approval1.OrigOwnerID,
              (object) approval1.OwnerID
            });
          approval1.DelegationRecordID = delegationRecordID;
          nullable5 = approval1.OwnerID;
          EPApproval epApproval3;
          if (!nullable5.HasValue)
          {
            nullable5 = approval1.WorkgroupID;
            if (!nullable5.HasValue)
              epApproval3 = (EPApproval) null;
            else
              epApproval3 = this._ApprovedByWorkgroup.SelectSingle(new object[2]
              {
                (object) approval1.RefNoteID,
                (object) approval1.WorkgroupID
              }) as EPApproval;
          }
          else
            epApproval3 = this._ApprovedByOwnerOrApprover.SelectSingle(new object[5]
            {
              (object) approval1.RefNoteID,
              (object) approval1.OwnerID,
              (object) approval1.OwnerID,
              (object) approval1.OrigOwnerID,
              (object) approval1.OrigOwnerID
            }) as EPApproval;
          EPApproval epApproval4 = epApproval3;
          if (epApproval4 != null)
          {
            approval1.ApprovedByID = epApproval4.ApprovedByID;
            approval1.ApproveDate = epApproval4.ApproveDate;
            approval1.Status = "A";
            approval1.IsPreApproved = new bool?(true);
            flag1 = this.OnApprovalApprovedWithoutReExecution(source, approval1, approval1.AssignmentMapID, approval1.NotificationID);
          }
          else
            flag1 = false;
          EPApproval approval2 = (EPApproval) ((PXSelectBase) this).Cache.Insert((object) approval1);
          if (approval2 != null && epApproval4 == null)
          {
            PXTrace.WriteInformation("Assign: {0} - {1}", new object[2]
            {
              (((PXSelectBase) this).Cache.GetStateExt<EPApproval.ownerID>((object) approval2) as PXFieldState)?.Value ?? (object) approval2.OwnerID,
              (object) approval2.WorkgroupID
            });
            if (approved != null && this.ValidateApprovalAccess(approval2))
            {
              approval2.ApprovedByID = PXAccess.GetContactID();
              approval2.ApproveDate = new DateTime?(PXTimeZoneInfo.Now);
              approval2.Status = "A";
              if (((PXSelectBase) this).Cache.Update((object) approval2) != null)
                flag1 = this.OnApprovalApprovedWithoutReExecution(source, approval2, approval2.AssignmentMapID, approval2.NotificationID);
            }
          }
          if (map != null)
          {
            nullable5 = map.MapType;
            int num = 0;
            if (nullable5.GetValueOrDefault() == num & nullable5.HasValue)
              return false;
          }
        }
        else if (epApproval1.Status == "A" && epRule2.ApproveType == "C")
        {
          if (PXSelectBase<EPApproval, PXSelectJoin<EPApproval, InnerJoin<EPRule, On<EPRule.ruleID, Equal<EPApproval.ruleID>, And<EPApproval.status, NotEqual<EPApprovalStatus.approved>, And<EPApproval.refNoteID, Equal<Required<EPApproval.refNoteID>>, And<EPApproval.ruleID, Equal<Required<EPRule.ruleID>>>>>>>>.Config>.SelectSingleBound(((PXSelectBase) this)._Graph, (object[]) null, new object[2]
          {
            (object) this.GetSourceNoteID((object) source),
            (object) epApproval1.RuleID
          }).Count == 0)
          {
            this._Except.SelectMulti(new object[2]
            {
              (object) this.GetSourceNoteID((object) source),
              (object) epApproval1.RuleID
            }).ForEach((Action<object>) (item => ((PXSelectBase) this).Cache.Delete(item)));
            return true;
          }
        }
        else if (approved != null)
          flag1 = PXSelectBase<EPApproval, PXSelect<EPApproval, Where<EPApproval.status, NotEqual<EPApprovalStatus.approved>, And<EPApproval.refNoteID, Equal<Required<EPApproval.refNoteID>>, And<EPApproval.stepID, Equal<Required<EPApproval.stepID>>>>>>.Config>.SelectSingleBound(((PXSelectBase) this)._Graph, (object[]) null, new object[2]
          {
            (object) this.GetSourceNoteID((object) source),
            (object) approveInfo.StepID
          }).Count == 0;
      }
    }
    if (map == null || epRule1 == null)
      return false;
    int num1 = PXSelectBase<EPApproval, PXSelectJoin<EPApproval, InnerJoin<EPRule, On<EPApproval.ruleID, Equal<EPRule.ruleID>, And<EPApproval.refNoteID, Equal<Required<EPApproval.refNoteID>>>>>, Where<EPRule.stepID, Equal<Required<EPRule.stepID>>>>.Config>.SelectSingleBound(((PXSelectBase) this)._Graph, (object[]) null, new object[2]
    {
      (object) this.GetSourceNoteID((object) source),
      (object) epRule1.RuleID
    }).Count != 0 ? 1 : 0;
    bool flag2 = ((PXSelectBase) this).Cache.Inserted.Count() != 0L;
    if (num1 == 0 && !flag2)
    {
      switch (epRule1.EmptyStepType)
      {
        case "A":
          throw new RequestApproveException();
        case "R":
          throw new RequestRejectException();
        default:
          flag1 = true;
          break;
      }
    }
    return flag1;
  }

  public bool ValidateAccess(SourceAssign source)
  {
    if (this.SuppressInFull)
      return false;
    PXView pending = this._Pending;
    object[] objArray = new object[1]
    {
      (object) this.GetSourceNoteID((object) source)
    };
    foreach (EPApproval epApproval in pending.SelectMulti(objArray))
    {
      if (this.ValidateApprovalAccess(epApproval))
        return true;
    }
    return false;
  }

  protected virtual IEnumerable<ApproveInfo> GetApproversFromNextStep(
    SourceAssign source,
    EPAssignmentMap map,
    int? currentStepSequence)
  {
    EPApprovalList<SourceAssign, Approved, Rejected> epApprovalList = this;
    if (map != null)
    {
      foreach (ApproveInfo approveInfo in new EPAssignmentProcessor<SourceAssign>(((PXSelectBase) epApprovalList)._Graph).Approve(source, map, currentStepSequence))
        yield return approveInfo;
    }
  }

  public virtual bool ValidateAccess(int? workgroup, int? ownerID)
  {
    if (this.SuppressInFull)
      return false;
    if (!workgroup.HasValue && !ownerID.HasValue || (ownerID.HasValue ? (ownerID.GetValueOrDefault().Equals((object) PXAccess.GetContactID()) ? 1 : 0) : 0) != 0)
      return true;
    return PXResultset<EPCompanyTree>.op_Implicit(PXSelectBase<EPCompanyTree, PXSelect<EPCompanyTree, Where<EPCompanyTree.workGroupID, Equal<Required<EPCompanyTree.workGroupID>>, And<EPCompanyTree.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>>.Config>.SelectWindowed(((PXSelectBase) this)._Graph, 0, 1, new object[1]
    {
      (object) workgroup
    })) != null;
  }

  public virtual bool ValidateApprovalAccess(EPApproval item)
  {
    if (item != null)
    {
      int? origOwnerId = item.OrigOwnerID;
      ref int? local = ref origOwnerId;
      if ((local.HasValue ? new bool?(local.GetValueOrDefault().Equals((object) PXAccess.GetContactID())) : new bool?()).GetValueOrDefault())
        return true;
    }
    return this.ValidateAccess((int?) item?.WorkgroupID, (int?) item?.OwnerID);
  }

  protected void RegisterActivity(object item, string summary)
  {
    if (this._Activity == null)
      return;
    PXGraph graph = ((PXSelectBase) this)._Graph;
    Select<SourceAssign> select = new Select<SourceAssign>();
    foreach (object obj in this._Activity.Press(new PXAdapter((PXView) new PXView.Dummy(graph, (BqlCommand) select, new List<object>()
    {
      item
    }))
    {
      Parameters = new object[1]{ (object) summary }
    }))
      ;
  }

  protected virtual void OnPersisted(PXCache cache, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != 1 || e.Operation == 3)
      return;
    foreach (EPApproval epApproval in this._Pending.Cache.Inserted.Cast<EPApproval>().GroupBy<EPApproval, int?>((Func<EPApproval, int?>) (_ => _.OwnerID)).Select<IGrouping<int?, EPApproval>, EPApproval>((Func<IGrouping<int?, EPApproval>, EPApproval>) (_ => _.First<EPApproval>())))
    {
      if (epApproval.NotificationID.HasValue)
      {
        if (epApproval.Status == "P")
        {
          try
          {
            this._Pending.Cache.Current = (object) epApproval;
            PXGraph graph = ((PXSelectBase) this)._Graph;
            object row = e.Row;
            int? nullable = epApproval.NotificationID;
            int notificationId = nullable.Value;
            TemplateNotificationGenerator notificationGenerator = TemplateNotificationGenerator.Create(graph, row, notificationId);
            nullable = new int?();
            notificationGenerator.Owner = nullable;
            notificationGenerator.LinkToEntity = false;
            notificationGenerator.MassProcessMode = false;
            notificationGenerator.Send();
          }
          catch (Exception ex)
          {
            this._Pending.Cache.RaiseExceptionHandling<EPApproval.status>((object) epApproval, (object) epApproval.Status, (Exception) new PXSetPropertyException("Unable to process approval notification. {0}", (PXErrorLevel) 2, new object[1]
            {
              (object) ex.Message
            }));
          }
        }
      }
    }
  }
}
