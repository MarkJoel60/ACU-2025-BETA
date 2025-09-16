// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EmployeeSummaryApprove
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

public class EmployeeSummaryApprove : PXGraph<EmployeeSummaryApprove>
{
  public PXFilter<EPSummaryFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<EPSummaryApprove, LeftJoin<EPEarningType, On<EPEarningType.typeCD, Equal<EPSummaryApprove.earningType>>>, Where2<Where<EPSummaryApprove.taskApproverID, Equal<Current<EPSummaryFilter.approverID>>, Or<Where<EPSummaryApprove.taskApproverID, IsNull, And<EPSummaryApprove.approverID, Equal<Current<EPSummaryFilter.approverID>>>>>>, And2<Where<EPSummaryApprove.weekId, GreaterEqual<Current<EPSummaryFilter.fromWeek>>, Or<Current<EPSummaryFilter.fromWeek>, IsNull>>, And2<Where<EPSummaryApprove.weekId, LessEqual<Current<EPSummaryFilter.tillWeek>>, Or<Current<EPSummaryFilter.tillWeek>, IsNull>>, And2<Where<EPSummaryApprove.projectID, Equal<Current<EPSummaryFilter.projectID>>, Or<Current<EPSummaryFilter.projectID>, IsNull>>, And2<Where<EPSummaryApprove.projectTaskID, Equal<Current<EPSummaryFilter.projectTaskID>>, Or<Current<EPSummaryFilter.projectTaskID>, IsNull>>, And<Where<EPSummaryApprove.employeeID, Equal<Current<EPSummaryFilter.employeeID>>, Or<Current<EPSummaryFilter.employeeID>, IsNull>>>>>>>>> Summary;
  public PXSelect<PMTimeActivity> Activity;
  public PXSetup<EPSetup> Setup;
  public PXSave<EPSummaryFilter> Save;
  public PXCancel<EPSummaryFilter> Cancel;
  public PXAction<EPSummaryFilter> viewDetails;
  public PXAction<EPSummaryFilter> approveAll;
  public PXAction<EPSummaryFilter> rejectAll;

  [PXUIField(DisplayName = "")]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDetails(PXAdapter adapter)
  {
    EPSummaryApprove current = ((PXSelectBase<EPSummaryApprove>) this.Summary).Current;
    if (current != null)
      PXRedirectHelper.TryRedirect((PXGraph) this, PXSelectorAttribute.Select<EPSummaryApprove.timeCardCD>(((PXSelectBase) this.Summary).Cache, (object) current), (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Approve All")]
  [PXButton]
  public virtual IEnumerable ApproveAll(PXAdapter adapter)
  {
    if (((PXSelectBase<EPSummaryApprove>) this.Summary).Current == null || ((PXSelectBase) this.Filter).View.Ask("Are you sure you want to approve all the listed records?", (MessageButtons) 4) != 6)
      return adapter.Get();
    foreach (PXResult<EPSummaryApprove> pxResult in ((PXSelectBase<EPSummaryApprove>) this.Summary).Select(Array.Empty<object>()))
    {
      EPSummaryApprove epSummaryApprove = PXResult<EPSummaryApprove>.op_Implicit(pxResult);
      epSummaryApprove.IsApprove = new bool?(true);
      epSummaryApprove.IsReject = new bool?(false);
      ((PXSelectBase) this.Summary).Cache.Update((object) epSummaryApprove);
    }
    ((PXGraph) this).Persist();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Reject All")]
  [PXButton]
  public virtual IEnumerable RejectAll(PXAdapter adapter)
  {
    if (((PXSelectBase<EPSummaryApprove>) this.Summary).Current == null || ((PXSelectBase) this.Filter).View.Ask("Are you sure you want to reject all the listed records?", (MessageButtons) 4) != 6)
      return adapter.Get();
    foreach (PXResult<EPSummaryApprove> pxResult in ((PXSelectBase<EPSummaryApprove>) this.Summary).Select(Array.Empty<object>()))
    {
      EPSummaryApprove epSummaryApprove = PXResult<EPSummaryApprove>.op_Implicit(pxResult);
      epSummaryApprove.IsApprove = new bool?(false);
      epSummaryApprove.IsReject = new bool?(true);
      ((PXSelectBase) this.Summary).Cache.Update((object) epSummaryApprove);
    }
    ((PXGraph) this).Persist();
    return adapter.Get();
  }

  protected virtual void EPSummaryFilter_FromWeek_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    EPSummaryFilter row = (EPSummaryFilter) e.Row;
    if (row == null || !e.ExternalCall)
      return;
    int? fromWeek = row.FromWeek;
    int? tillWeek = row.TillWeek;
    if (!(fromWeek.GetValueOrDefault() > tillWeek.GetValueOrDefault() & fromWeek.HasValue & tillWeek.HasValue))
      return;
    row.TillWeek = row.FromWeek;
  }

  protected virtual void EPSummaryFilter_TillWeek_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    EPSummaryFilter row = (EPSummaryFilter) e.Row;
    if (row == null || !e.ExternalCall)
      return;
    int? fromWeek = row.FromWeek;
    if (!fromWeek.HasValue)
      return;
    fromWeek = row.FromWeek;
    int? tillWeek = row.TillWeek;
    if (!(fromWeek.GetValueOrDefault() > tillWeek.GetValueOrDefault() & fromWeek.HasValue & tillWeek.HasValue))
      return;
    row.FromWeek = row.TillWeek;
  }

  protected virtual void EPSummaryFilter_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (((PXSelectBase) this.Summary).Cache.IsDirty && ((PXSelectBase) this.Filter).View.Ask("Any unsaved changes will be discarded.", (MessageButtons) 4) != 6)
      ((CancelEventArgs) e).Cancel = true;
    else
      ((PXSelectBase) this.Summary).Cache.Clear();
  }

  protected virtual void EPSummaryApprove_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    EPSummaryApprove row = (EPSummaryApprove) e.Row;
    if (row == null)
      return;
    PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
    if (row.HasComplite != null || row.HasReject != null || row.HasApprove != null)
    {
      PXUIFieldAttribute.SetEnabled<EPSummaryApprove.isApprove>(sender, (object) row, true);
      PXUIFieldAttribute.SetEnabled<EPSummaryApprove.isReject>(sender, (object) row, true);
    }
    if (row.HasOpen == null)
      return;
    sender.RaiseExceptionHandling<EPSummaryApprove.weekId>((object) row, (object) null, (Exception) new PXSetPropertyException("The time card includes one or multiple time activities with the Open status. All time activities must be completed before the time card may be submitted for approval.", (PXErrorLevel) 3));
  }

  protected virtual void EPSummaryFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    EPSummaryFilter row = (EPSummaryFilter) e.Row;
    if (row == null)
      return;
    row.RegularTime = new int?(0);
    row.RegularOvertime = new int?(0);
    row.RegularTotal = new int?(0);
    foreach (PXResult<EPSummaryApprove, EPEarningType> pxResult in ((PXSelectBase<EPSummaryApprove>) this.Summary).Select(Array.Empty<object>()))
    {
      EPSummaryApprove epSummaryApprove = PXResult<EPSummaryApprove, EPEarningType>.op_Implicit(pxResult);
      int? nullable1;
      if (PXResult<EPSummaryApprove, EPEarningType>.op_Implicit(pxResult).IsOvertime.GetValueOrDefault())
      {
        EPSummaryFilter epSummaryFilter = row;
        int? regularOvertime = epSummaryFilter.RegularOvertime;
        nullable1 = epSummaryApprove.TimeSpent;
        int num = nullable1 ?? 0;
        int? nullable2;
        if (!regularOvertime.HasValue)
        {
          nullable1 = new int?();
          nullable2 = nullable1;
        }
        else
          nullable2 = new int?(regularOvertime.GetValueOrDefault() + num);
        epSummaryFilter.RegularOvertime = nullable2;
      }
      else
      {
        EPSummaryFilter epSummaryFilter = row;
        int? regularTime = epSummaryFilter.RegularTime;
        nullable1 = epSummaryApprove.TimeSpent;
        int num = nullable1 ?? 0;
        int? nullable3;
        if (!regularTime.HasValue)
        {
          nullable1 = new int?();
          nullable3 = nullable1;
        }
        else
          nullable3 = new int?(regularTime.GetValueOrDefault() + num);
        epSummaryFilter.RegularTime = nullable3;
      }
      EPSummaryFilter epSummaryFilter1 = row;
      int? regularTime1 = row.RegularTime;
      nullable1 = row.RegularOvertime;
      int? nullable4 = regularTime1.HasValue & nullable1.HasValue ? new int?(regularTime1.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new int?();
      epSummaryFilter1.RegularTotal = nullable4;
    }
  }

  protected virtual void EPSummaryApprove_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    EPSummaryApprove row = (EPSummaryApprove) e.Row;
    if (row == null || e.Operation == 3)
      return;
    bool? nullable = row.IsApprove;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.IsReject;
      if (nullable.GetValueOrDefault())
        return;
    }
    foreach (PXResult<PMTimeActivity> pxResult in PXSelectBase<PMTimeActivity, PXSelectJoin<PMTimeActivity, InnerJoin<EPTimeCard, On<Required<EPTimeCardSummary.timeCardCD>, Equal<EPTimeCard.timeCardCD>, And<EPTimeCard.weekId, Equal<PMTimeActivity.weekID>>>, InnerJoin<CREmployee, On<EPTimeCard.employeeID, Equal<CREmployee.bAccountID>, And<CREmployee.defContactID, Equal<PMTimeActivity.ownerID>>>>>, Where<Required<EPTimeCardSummary.earningType>, Equal<PMTimeActivity.earningTypeID>, And<Required<EPTimeCardSummary.projectID>, Equal<PMTimeActivity.projectID>, And<Required<EPTimeCardSummary.projectTaskID>, Equal<PMTimeActivity.projectTaskID>, And<Required<EPTimeCardSummary.isBillable>, Equal<PMTimeActivity.isBillable>, And2<Where<Required<EPTimeCardSummary.parentNoteID>, Equal<PMTimeActivity.parentTaskNoteID>, Or<Required<EPTimeCardSummary.parentNoteID>, IsNull, And<PMTimeActivity.parentTaskNoteID, IsNull>>>, And<PMTimeActivity.trackTime, Equal<True>, And<PMTimeActivity.released, Equal<False>, And<PMTimeActivity.trackTime, Equal<True>, And<PMTimeActivity.approverID, IsNotNull, And<Where<PMTimeActivity.approvalStatus, NotEqual<ActivityStatusListAttribute.canceled>, And<PMTimeActivity.approvalStatus, NotEqual<ActivityStatusListAttribute.open>>>>>>>>>>>>>>.Config>.Select((PXGraph) this, new object[7]
    {
      (object) row.TimeCardCD,
      (object) row.EarningType,
      (object) row.ProjectID,
      (object) row.ProjectTaskID,
      (object) row.IsBillable,
      (object) row.ParentNoteID,
      (object) row.ParentNoteID
    }))
    {
      PMTimeActivity pmTimeActivity = PXResult<PMTimeActivity>.op_Implicit(pxResult);
      nullable = row.IsApprove;
      if (nullable.GetValueOrDefault())
      {
        if (pmTimeActivity.ApprovalStatus != "AP")
        {
          ((PXSelectBase) this.Activity).Cache.SetValueExt<PMTimeActivity.approvalStatus>((object) pmTimeActivity, (object) "AP");
          ((PXSelectBase) this.Activity).Cache.SetValueExt<PMTimeActivity.approvedDate>((object) pmTimeActivity, (object) ((PXGraph) this).Accessinfo.BusinessDate);
        }
      }
      else
      {
        nullable = row.IsReject;
        if (nullable.GetValueOrDefault() && pmTimeActivity.ApprovalStatus != "RJ")
          ((PXSelectBase) this.Activity).Cache.SetValueExt<PMTimeActivity.approvalStatus>((object) pmTimeActivity, (object) "RJ");
      }
      ((PXSelectBase) this.Activity).Cache.Persist((object) pmTimeActivity, (PXDBOperation) 1);
    }
  }

  protected virtual void PMTimeActivity_UIStatus_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Week")]
  protected virtual void _(PX.Data.Events.CacheAttached<EPWeekRaw.shortDescription> e)
  {
  }

  public EmployeeSummaryApprove()
  {
    if (((PXSelectBase<EPSetup>) this.Setup).Current == null)
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (EPSetup), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Time & Expenses Preferences")
      });
  }

  public virtual void Persist()
  {
    List<IGrouping<string, EPSummaryApprove>> list = ((PXSelectBase) this.Summary).Cache.Updated.Cast<EPSummaryApprove>().Where<EPSummaryApprove>((Func<EPSummaryApprove, bool>) (a => a.IsReject.GetValueOrDefault())).GroupBy<EPSummaryApprove, string>((Func<EPSummaryApprove, string>) (a => a.TimeCardCD)).ToList<IGrouping<string, EPSummaryApprove>>();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (list.Count > 0)
      {
        TimeCardMaint instance = PXGraph.CreateInstance<TimeCardMaint>();
        ((PXSelectBase<EPTimeCard>) instance.Document).Join<InnerJoin<EPTimeCardSummary, On<EPTimeCardSummary.timeCardCD, Equal<EPTimeCard.timeCardCD>>>>();
        ((PXSelectBase<EPTimeCard>) instance.Document).Join<InnerJoin<PMTask, On<PMTask.taskID, Equal<EPTimeCardSummary.projectTaskID>>>>();
        ((PXSelectBase<EPTimeCard>) instance.Document).WhereOr<Where<PMTask.approverID, Equal<Current<EPSummaryFilter.approverID>>>>();
        foreach (IGrouping<string, EPSummaryApprove> grouping in list)
        {
          ((PXGraph) instance).Clear();
          ((PXSelectBase<EPTimeCard>) instance.Document).Current = PXResultset<EPTimeCard>.op_Implicit(((PXSelectBase<EPTimeCard>) instance.Document).Search<EPTimeCard.timeCardCD>((object) grouping.Key, Array.Empty<object>()));
          ((PXGraph) instance).Actions["Reject"].Press();
          ((PXGraph) instance).Persist();
        }
      }
      ((PXGraph) this).Persist();
      transactionScope.Complete();
    }
  }
}
