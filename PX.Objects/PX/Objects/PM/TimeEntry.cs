// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TimeEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.PM;

public class TimeEntry : PXGraph<TimeEntry, PMTimeActivity>
{
  public PXSelect<PMTimeActivity> Items;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<PMTimeActivity, Where<PMTimeActivity.noteID, Equal<Current<PMTimeActivity.noteID>>>> ItemProperties;
  public PXSetup<EPSetup> Setup;
  public PXAction<PMTimeActivity> MarkAsCompleted;
  public PXAction<PMTimeActivity> MarkAsOpen;
  protected EmployeeCostEngine costEngine;

  [PXMergeAttributes]
  [PXSelector(typeof (Search<PMTimeActivity.noteID>))]
  [PXUIField(DisplayName = "ID")]
  [PXDBGuid(true, IsKey = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTimeActivity.noteID> e)
  {
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Track Time")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTimeActivity.trackTime> e)
  {
  }

  [PXDefault(typeof (AccessInfo.contactID))]
  [SubordinateAndWingmenOwnerEmployee(DisplayName = "Employee")]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTimeActivity.ownerID> e)
  {
  }

  [PXSelector(typeof (Search<CRActivity.noteID, Where<CRActivity.noteID, NotEqual<Current<PMTimeActivity.noteID>>, And<CRActivity.ownerID, Equal<Current<PMTimeActivity.ownerID>>, And<CRActivity.classID, NotEqual<CRActivityClass.events>, And<Where<CRActivity.classID, Equal<CRActivityClass.task>, Or<CRActivity.classID, Equal<CRActivityClass.events>>>>>>>, OrderBy<Desc<CRActivity.createdDateTime>>>), new System.Type[] {typeof (CRActivity.subject), typeof (CRActivity.uistatus)}, DescriptionField = typeof (CRActivity.subject))]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMTimeActivity.parentTaskNoteID> e)
  {
  }

  [PXUIField]
  [PXMergeAttributes]
  public virtual void _(PX.Data.Events.CacheAttached<PMTimeActivity.timeCardCD> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault("OP")]
  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Approval Status")]
  public virtual void _(
    PX.Data.Events.CacheAttached<PMTimeActivity.approvalStatus> e)
  {
  }

  [PXUIField(DisplayName = "Complete")]
  [PXButton(Tooltip = "Marks current record as completed (Ctrl + K)", ShortcutCtrl = true, ShortcutChar = 'K')]
  public virtual void markAsCompleted()
  {
    PMTimeActivity current = ((PXSelectBase<PMTimeActivity>) this.Items).Current;
    if (current == null)
      return;
    this.CompleteActivity(current);
  }

  [PXUIField(DisplayName = "Open")]
  [PXButton(Tooltip = "Open")]
  public virtual void markAsOpen()
  {
    PMTimeActivity current = ((PXSelectBase<PMTimeActivity>) this.Items).Current;
    if (current == null)
      return;
    this.OpenActivity(current);
  }

  public EmployeeCostEngine CostEngine
  {
    get
    {
      if (this.costEngine == null)
        this.costEngine = this.CreateEmployeeCostEngine();
      return this.costEngine;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMTimeActivity> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.Released.GetValueOrDefault())
    {
      PXUIFieldAttribute.SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTimeActivity>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PMTimeActivity.noteID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTimeActivity>>) e).Cache, (object) e.Row, true);
    }
    else if (e.Row.ApprovalStatus == "OP")
    {
      PXUIFieldAttribute.SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTimeActivity>>) e).Cache, (object) e.Row, true);
      PXUIFieldAttribute.SetEnabled<PMTimeActivity.timeBillable>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTimeActivity>>) e).Cache, (object) e.Row);
      PMProject pmProject = (PMProject) PXSelectorAttribute.Select<PMTimeActivity.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTimeActivity>>) e).Cache, (object) e.Row);
      if (pmProject != null)
        PXUIFieldAttribute.SetEnabled<PMTimeActivity.projectTaskID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTimeActivity>>) e).Cache, (object) e.Row, pmProject.BaseType == "P");
      PXDBDateAndTimeAttribute.SetTimeEnabled<PMTimeActivity.date>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTimeActivity>>) e).Cache, (object) e.Row, true);
      PXDBDateAndTimeAttribute.SetDateEnabled<PMTimeActivity.date>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTimeActivity>>) e).Cache, (object) e.Row, true);
      PXUIFieldAttribute.SetEnabled<PMTimeActivity.approverID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTimeActivity>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PMTimeActivity.timeCardCD>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTimeActivity>>) e).Cache, (object) e.Row, false);
    }
    else
    {
      PXUIFieldAttribute.SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTimeActivity>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PMTimeActivity.noteID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTimeActivity>>) e).Cache, (object) e.Row, true);
      PXUIFieldAttribute.SetEnabled<PMTimeActivity.approvalStatus>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTimeActivity>>) e).Cache, (object) e.Row, true);
    }
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.employeeRate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTimeActivity>>) e).Cache, (object) e.Row, false);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PMTimeActivity> e)
  {
    if (e.Row.ApprovalStatus == "AP" || e.Row.Released.GetValueOrDefault())
    {
      ((PXSelectBase) this.Items).View.Ask(PXMessages.LocalizeFormatNoPrefix("Activity in status \"{0}\" cannot be deleted.", new object[1]
      {
        ((PX.Data.Events.Event<PXRowDeletingEventArgs, PX.Data.Events.RowDeleting<PMTimeActivity>>) e).Cache.GetValueExt<PMTimeActivity.approvalStatus>((object) e.Row)
      }), (MessageButtons) 0);
      e.Cancel = true;
    }
    else
    {
      if (e.Row.TimeCardCD == null)
        return;
      ((PXSelectBase) this.Items).View.Ask("This Activity assigned to the Time Card. You may do changes only in a Time Card screen.", (MessageButtons) 0);
      e.Cancel = true;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PMTimeActivity, PMTimeActivity.approvalStatus> e)
  {
    if (e.Row == null)
      return;
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    if (e.Row.Released.GetValueOrDefault())
    {
      stringList1.Add("RL");
      stringList2.Add(Messages.GetLocal("Released"));
    }
    else
    {
      stringList1.Add("OP");
      stringList2.Add(Messages.GetLocal("Open"));
      stringList1.Add("CD");
      stringList2.Add(Messages.GetLocal("Completed"));
    }
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMTimeActivity, PMTimeActivity.approvalStatus>>) e).ReturnState = (object) PXStringState.CreateInstance(((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMTimeActivity, PMTimeActivity.approvalStatus>>) e).ReturnState, new int?(1), new bool?(false), typeof (PMTimeActivity.approvalStatus).Name, new bool?(true), new int?(1), (string) null, stringList1.ToArray(), stringList2.ToArray(), new bool?(true), "CD", (string[]) null);
  }

  protected virtual void PMTimeActivity_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    PMTimeActivity row = (PMTimeActivity) e.Row;
    if (row == null || row.TimeCardCD == null)
      return;
    ((PXSelectBase) this.Items).View.Ask("This Activity assigned to the Time Card. You may do changes only in a Time Card screen.", (MessageButtons) 0);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.earningTypeID> e)
  {
    PX.Objects.EP.EPEmployee epEmployee = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee>.Config>.Search<PX.Objects.EP.EPEmployee.defContactID>((PXGraph) this, (object) e.Row.OwnerID, Array.Empty<object>()));
    if (epEmployee == null || !e.Row.Date.HasValue)
      return;
    if (CalendarHelper.IsWorkDay((PXGraph) this, epEmployee.CalendarID, e.Row.Date.Value))
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.earningTypeID>, PMTimeActivity, object>) e).NewValue = (object) ((PXSelectBase<EPSetup>) this.Setup).Current.RegularHoursType;
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.earningTypeID>, PMTimeActivity, object>) e).NewValue = (object) ((PXSelectBase<EPSetup>) this.Setup).Current.HolidaysType;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.projectID> e)
  {
    if (e.Row == null)
      return;
    EPEarningType epEarningType = PXResultset<EPEarningType>.op_Implicit(PXSelectBase<EPEarningType, PXSelect<EPEarningType, Where<EPEarningType.typeCD, Equal<Required<EPEarningType.typeCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.EarningTypeID
    }));
    if (epEarningType == null)
      return;
    int? projectId = epEarningType.ProjectID;
    if (!projectId.HasValue)
      return;
    projectId = e.Row.ProjectID;
    if (projectId.HasValue || PXSelectorAttribute.Select(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.projectID>>) e).Cache, (object) e.Row, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.projectID>>) e).Cache.GetField(typeof (PMTimeActivity.projectID)), (object) epEarningType.ProjectID) == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.projectID>, PMTimeActivity, object>) e).NewValue = (object) epEarningType.ProjectID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.projectID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.projectTaskID> e)
  {
    if (e.Row == null)
      return;
    int? projectId1;
    if (e.Row.ParentTaskNoteID.HasValue)
    {
      EPActivityApprove epActivityApprove = PXResultset<EPActivityApprove>.op_Implicit(PXSelectBase<EPActivityApprove, PXSelect<EPActivityApprove>.Config>.Search<EPActivityApprove.noteID>((PXGraph) this, (object) e.Row.ParentTaskNoteID, Array.Empty<object>()));
      if (epActivityApprove != null)
      {
        projectId1 = epActivityApprove.ProjectID;
        int? projectId2 = e.Row.ProjectID;
        if (projectId1.GetValueOrDefault() == projectId2.GetValueOrDefault() & projectId1.HasValue == projectId2.HasValue)
        {
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.projectTaskID>, PMTimeActivity, object>) e).NewValue = (object) epActivityApprove.ProjectTaskID;
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.projectTaskID>>) e).Cancel = true;
        }
      }
    }
    EPEarningType epEarningType = (EPEarningType) PXSelectorAttribute.Select<PMTimeActivity.earningTypeID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.projectTaskID>>) e).Cache, (object) e.Row);
    if (((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.projectTaskID>, PMTimeActivity, object>) e).NewValue != null || epEarningType == null)
      return;
    int? projectId3 = epEarningType.ProjectID;
    projectId1 = e.Row.ProjectID;
    if (!(projectId3.GetValueOrDefault() == projectId1.GetValueOrDefault() & projectId3.HasValue == projectId1.HasValue) || !(PXSelectorAttribute.Select(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.projectTaskID>>) e).Cache, (object) e.Row, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.projectTaskID>>) e).Cache.GetField(typeof (EPTimeCardSummary.projectTaskID)), (object) epEarningType.TaskID) is PMTask pmTask) || !pmTask.VisibleInTA.GetValueOrDefault())
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.projectTaskID>, PMTimeActivity, object>) e).NewValue = (object) epEarningType.TaskID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.projectTaskID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMTimeActivity, PMTimeActivity.projectID> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTimeActivity, PMTimeActivity.projectID>, PMTimeActivity, object>) e).NewValue == null || !(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTimeActivity, PMTimeActivity.projectID>, PMTimeActivity, object>) e).NewValue is int))
      return;
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject>.Config>.Search<PMProject.contractID>((PXGraph) this, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTimeActivity, PMTimeActivity.projectID>, PMTimeActivity, object>) e).NewValue, Array.Empty<object>()));
    if (pmProject == null)
      return;
    bool? nullable = pmProject.IsCompleted;
    nullable = !nullable.GetValueOrDefault() ? pmProject.IsCancelled : throw new PXSetPropertyException("Project is Completed and cannot be used for data entry.")
    {
      ErrorValue = (object) pmProject.ContractCD
    };
    if (nullable.GetValueOrDefault())
      throw new PXSetPropertyException("Project is Canceled and cannot be used for data entry.")
      {
        ErrorValue = (object) pmProject.ContractCD
      };
    if (pmProject.Status == "E")
      throw new PXSetPropertyException("Project is Suspended and cannot be used for data entry.")
      {
        ErrorValue = (object) pmProject.ContractCD
      };
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMTimeActivity, PMTimeActivity.projectTaskID> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTimeActivity, PMTimeActivity.projectTaskID>, PMTimeActivity, object>) e).NewValue == null || !(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTimeActivity, PMTimeActivity.projectTaskID>, PMTimeActivity, object>) e).NewValue is int))
      return;
    PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask>.Config>.Search<PMTask.projectID, PMTask.taskID>((PXGraph) this, (object) e.Row.ProjectID, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTimeActivity, PMTimeActivity.projectTaskID>, PMTimeActivity, object>) e).NewValue, Array.Empty<object>()));
    if (pmTask == null)
      return;
    bool? nullable = pmTask.IsCompleted;
    nullable = !nullable.GetValueOrDefault() ? pmTask.IsCancelled : throw new PXSetPropertyException("Task is Completed and cannot be used for data entry.")
    {
      ErrorValue = (object) pmTask.TaskCD
    };
    if (nullable.GetValueOrDefault())
      throw new PXSetPropertyException("Task is Canceled and cannot be used for data entry.")
      {
        ErrorValue = (object) pmTask.TaskCD
      };
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.ownerID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.ownerID>>) e).Cache.SetDefaultExt<PMTimeActivity.labourItemID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.costCodeID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.costCodeID>>) e).Cache.SetDefaultExt<PMTimeActivity.workCodeID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.projectID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.projectID>>) e).Cache.SetDefaultExt<PMTimeActivity.unionID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.projectID>>) e).Cache.SetDefaultExt<PMTimeActivity.certifiedJob>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.projectID>>) e).Cache.SetDefaultExt<PMTimeActivity.labourItemID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.isBillable> e)
  {
    e.Row.TimeBillable = this.GetTimeBillable(e.Row, new int?());
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMTimeActivity> e)
  {
    DateTime? date = e.Row.Date;
    DateTime valueOrDefault1 = date.GetValueOrDefault();
    date = e.OldRow.Date;
    DateTime valueOrDefault2 = date.GetValueOrDefault();
    if (!(valueOrDefault1 != valueOrDefault2) && !(e.Row.EarningTypeID != e.OldRow.EarningTypeID))
    {
      int? nullable1 = e.Row.ProjectID;
      int valueOrDefault3 = nullable1.GetValueOrDefault();
      nullable1 = e.OldRow.ProjectID;
      int valueOrDefault4 = nullable1.GetValueOrDefault();
      if (valueOrDefault3 == valueOrDefault4)
      {
        nullable1 = e.Row.ProjectTaskID;
        int valueOrDefault5 = nullable1.GetValueOrDefault();
        nullable1 = e.OldRow.ProjectTaskID;
        int valueOrDefault6 = nullable1.GetValueOrDefault();
        if (valueOrDefault5 == valueOrDefault6)
        {
          nullable1 = e.Row.CostCodeID;
          int? nullable2 = e.OldRow.CostCodeID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && !(e.Row.UnionID != e.OldRow.UnionID))
          {
            nullable2 = e.Row.LabourItemID;
            nullable1 = e.OldRow.LabourItemID;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
            {
              bool? certifiedJob = e.Row.CertifiedJob;
              int num1 = certifiedJob.GetValueOrDefault() ? 1 : 0;
              certifiedJob = e.OldRow.CertifiedJob;
              int num2 = certifiedJob.GetValueOrDefault() ? 1 : 0;
              if (num1 == num2)
              {
                nullable1 = e.Row.OwnerID;
                int valueOrDefault7 = nullable1.GetValueOrDefault();
                nullable1 = e.OldRow.OwnerID;
                int valueOrDefault8 = nullable1.GetValueOrDefault();
                if (valueOrDefault7 == valueOrDefault8)
                  return;
              }
            }
          }
        }
      }
    }
    this.RecalculateEmployeeCost(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserted<PMTimeActivity> e)
  {
    this.RecalculateEmployeeCost(e.Row);
  }

  public virtual void Persist()
  {
    GraphHelper.EnsureCachePersistence<Note>((PXGraph) this);
    ((PXGraph) this).Persist();
  }

  public virtual void RecalculateEmployeeCost(PMTimeActivity row)
  {
    if (row == null || !row.Date.HasValue)
      return;
    int? employeeID = new int?();
    if (row.OwnerID.HasValue)
    {
      PX.Objects.EP.EPEmployee epEmployee = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Required<PMTimeActivity.ownerID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.OwnerID
      }));
      if (epEmployee != null)
        employeeID = epEmployee.BAccountID;
    }
    EmployeeCostEngine.LaborCost employeeCost = this.CostEngine.CalculateEmployeeCost((string) null, row.EarningTypeID, row.LabourItemID, row.ProjectID, row.ProjectTaskID, row.CertifiedJob, row.UnionID, employeeID, row.Date.Value, row.ShiftID);
    row.EmployeeRate = (Decimal?) employeeCost?.Rate;
  }

  public virtual int ExecuteInsert(string viewName, IDictionary values, params object[] parameters)
  {
    return ((PXGraph) this).ExecuteInsert(viewName, values, parameters);
  }

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
  }

  protected virtual int? GetTimeBillable(PMTimeActivity row, int? OldTimeSpent)
  {
    if (row.TimeCardCD != null || row.Billed.GetValueOrDefault())
      return row.TimeBillable;
    if (!row.IsBillable.GetValueOrDefault())
      return new int?(0);
    if (OldTimeSpent.GetValueOrDefault() != 0)
    {
      int? nullable = OldTimeSpent;
      int? timeBillable = row.TimeBillable;
      if (!(nullable.GetValueOrDefault() == timeBillable.GetValueOrDefault() & nullable.HasValue == timeBillable.HasValue))
        return row.TimeBillable;
    }
    return row.TimeSpent;
  }

  private void CompleteActivity(PMTimeActivity activity)
  {
    if (activity != null)
    {
      activity.ApprovalStatus = "CD";
      ((PXSelectBase) this.Items).Cache.Update((object) activity);
    }
    ((PXGraph) this).Actions.PressSave();
  }

  private void OpenActivity(PMTimeActivity activity)
  {
    if (activity != null)
    {
      activity.ApprovalStatus = "OP";
      ((PXSelectBase) this.Items).Cache.Update((object) activity);
    }
    ((PXGraph) this).Actions.PressSave();
  }

  public virtual EmployeeCostEngine CreateEmployeeCostEngine()
  {
    return new EmployeeCostEngine((PXGraph) this);
  }
}
