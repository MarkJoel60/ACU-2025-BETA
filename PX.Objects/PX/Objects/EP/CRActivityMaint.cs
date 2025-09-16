// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.CRActivityMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.CR;
using PX.Objects.PM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.EP;

public class CRActivityMaint : CRBaseActivityMaint<
#nullable disable
CRActivityMaint, CRActivity>
{
  private static readonly EPSetup EmptyEpSetup = new EPSetup();
  [PXHidden]
  public PXSelect<PX.Objects.CT.Contract> BaseContract;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (CRActivity.body)})]
  public PXSelect<CRActivity, Where<CRActivity.classID, Equal<CRActivityClass.activity>>> Activities;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<CRActivity, Where<CRActivity.noteID, Equal<Current<CRActivity.noteID>>>> CurrentActivity;
  public PXSelect<PMTimeActivity, Where<PMTimeActivity.refNoteID, Equal<Current<CRActivity.noteID>>>> TimeActivitiesOld;
  public PMTimeActivityList<CRActivity> TimeActivity;
  public PXDelete<CRActivity> Delete;
  public PXAction<CRActivity> MarkAsCompleted;
  public PXAction<CRActivity> MarkAsCompletedAndFollowUp;

  public CRActivityMaint() => CRCaseActivityHelper.Attach((PXGraph) this);

  [PXUIField(DisplayName = "Complete")]
  [PXButton(Tooltip = "Marks current record as completed (Ctrl + K)", ClosePopup = true, ShortcutCtrl = true, ShortcutChar = 'K', OnClosingPopup = PXSpecialButtonType.Refresh)]
  public virtual void markAsCompleted()
  {
    CRActivity current = this.Activities.Current;
    if (current == null)
      return;
    if (this.IsDirty)
      this.Actions.PressSave();
    this.CompleteActivity(current);
  }

  [PXUIField(DisplayName = "Complete & Follow-Up")]
  [PXButton(Tooltip = "Marks current record as completed and creates new its copy (Ctrl + Shift + K)", ClosePopup = true, ShortcutCtrl = true, ShortcutShift = true, ShortcutChar = 'K')]
  public virtual void markAsCompletedAndFollowUp()
  {
    CRActivity current = this.Activities.Current;
    if (current == null)
      return;
    if (this.IsDirty)
      this.Actions.PressSave();
    this.CompleteActivity(current);
    CRActivityMaint instance = PXGraph.CreateInstance<CRActivityMaint>();
    CRActivity copy = (CRActivity) instance.Activities.Cache.CreateCopy((object) current);
    copy.NoteID = new Guid?();
    copy.ParentNoteID = current.ParentNoteID;
    copy.UIStatus = (string) null;
    copy.PercentCompletion = new int?();
    if (copy.StartDate.HasValue)
      copy.StartDate = new DateTime?(copy.StartDate.Value.AddDays(1.0));
    DateTime? endDate = copy.EndDate;
    if (endDate.HasValue)
    {
      CRActivity crActivity = copy;
      endDate = copy.EndDate;
      DateTime? nullable = new DateTime?(endDate.Value.AddDays(1.0));
      crActivity.EndDate = nullable;
    }
    instance.Activities.Insert(copy);
    PXRedirectHelper.TryRedirect((PXGraph) instance, PXRedirectHelper.WindowMode.NewWindow);
  }

  [PXRemoveBaseAttribute(typeof (PXNavigateSelectorAttribute))]
  [PXMergeAttributes(Method = MergeMethod.Append)]
  protected virtual void _(PX.Data.Events.CacheAttached<CRActivity.subject> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Started On")]
  [PXCustomizeBaseAttribute(typeof (EPStartDateAttribute), "DisplayNameDate", "Started On")]
  [PXMergeAttributes(Method = MergeMethod.Append)]
  protected virtual void _(PX.Data.Events.CacheAttached<CRActivity.startDate> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXSelectorAttribute), "SelectorMode", PXSelectorMode.DisplayModeText)]
  [PXMergeAttributes(Method = MergeMethod.Append)]
  protected virtual void _(PX.Data.Events.CacheAttached<CRActivity.type> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [ProjectTask(typeof (PMTimeActivity.projectID), "TA", DisplayName = "Project Task", DefaultNotClosedTask = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMTimeActivity.projectTaskID> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.trackTime> e)
  {
    PMTimeActivity row = e.Row;
    if (row == null || ProjectDefaultAttribute.IsNonProject(row.ProjectID))
      return;
    bool flag1 = !ProjectAttribute.IsPMVisible("TA");
    bool? trackTime = row.TrackTime;
    bool flag2 = false;
    if (!(trackTime.GetValueOrDefault() == flag2 & trackTime.HasValue | flag1))
      return;
    e.Cache.SetValueExt<PMTimeActivity.projectID>((object) row, (object) ProjectDefaultAttribute.NonProject());
  }

  protected virtual void CRActivity_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    CRActivity row = (CRActivity) e.Row;
    if (row == null)
      return;
    row.ClassID = new int?(2);
  }

  protected virtual void CRActivity_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    CRActivity row = (CRActivity) e.Row;
    CRActivity oldRow = (CRActivity) e.OldRow;
    if (row == null || oldRow == null)
      return;
    row.ClassID = new int?(2);
  }

  [Obsolete]
  protected virtual void CRActivity_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
  }

  protected override void _(PX.Data.Events.RowSelected<CRActivity> e)
  {
    CRActivity row = e.Row;
    if (row == null)
    {
      base._(e);
    }
    else
    {
      PMTimeActivity tAct = this.TimeActivity.SelectSingle(Array.Empty<object>());
      PXCache tActCache = this.TimeActivity.Cache;
      int num1;
      if (string.IsNullOrEmpty(tAct?.TimeCardCD))
      {
        PMTimeActivity pmTimeActivity = tAct;
        num1 = pmTimeActivity != null ? (pmTimeActivity.Billed.GetValueOrDefault() ? 1 : 0) : 0;
      }
      else
        num1 = 1;
      bool wasUsed = num1 != 0;
      bool isPmVisible = ProjectAttribute.IsPMVisible("TA");
      bool showMinutes = this.EPSetupCurrent.RequireTimes.GetValueOrDefault();
      PMTimeActivity pmTimeActivity1 = tAct;
      bool trackTime = (pmTimeActivity1 != null ? (pmTimeActivity1.TrackTime.GetValueOrDefault() ? 1 : 0) : 0) != 0;
      string originalStatus = GetOriginalStatus(GetOriginalTimeStatus());
      bool isOpenPM = originalStatus == "OP";
      bool isOpen = originalStatus == "OP";
      AdjustCRActivityUI();
      AdjustPMTimeActivityUI();
      AdjustButtonsUI();
      base._(e);

      void AdjustCRActivityUI()
      {
        PXDBDateAndTimeAttribute.SetTimeEnabled<CRActivity.startDate>(e.Cache, (object) row, showMinutes & trackTime);
        PXDBDateAndTimeAttribute.SetTimeVisible<CRActivity.startDate>(e.Cache, (object) row, showMinutes & trackTime);
        PXDBDateAndTimeAttribute.SetTimeVisible<CRActivity.endDate>(e.Cache, (object) row, showMinutes & trackTime);
        PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = e.Cache.AdjustUI((object) e.Row);
        attributeAdjuster = attributeAdjuster.ForAllFields((Action<PXUIFieldAttribute>) (ui => ui.Enabled = isOpen));
        PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = attributeAdjuster.For<CRActivity.responseActivityNoteID>((Action<PXUIFieldAttribute>) (ui =>
        {
          if (!isOpen)
            return;
          ui.Enabled = false;
        }));
        chained = chained.SameFor<CRActivity.responseDueDateTime>();
        chained = chained.For<CRActivity.noteID>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = true));
        chained = chained.SameFor<CRActivity.startDate>();
        chained = chained.For<CRActivity.completedDate>((Action<PXUIFieldAttribute>) (ui =>
        {
          ui.Visible = trackTime;
          ui.Enabled = true;
        }));
        chained = chained.For<CRActivity.refNoteID>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = e.Cache.GetValue<CRActivity.refNoteIDType>((object) row) != null || this.IsContractBasedAPI));
        chained = chained.For<CRActivity.uistatus>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = false));
        chained.For<CRActivity.uistatus>((Action<PXUIFieldAttribute>) (ui => ui.Visible = !trackTime));
        e.Cache.Adjust<PXDefaultAttribute>((object) e.Row).For<CRActivity.ownerID>((Action<PXDefaultAttribute>) (ui => ui.PersistingCheck = trackTime ? PXPersistingCheck.Null : PXPersistingCheck.Nothing)).For<CRActivity.type>((Action<PXDefaultAttribute>) (ui => ui.PersistingCheck = PXPersistingCheck.Null));
      }

      void AdjustPMTimeActivityUI()
      {
        PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = tActCache.AdjustUI().For<PMTimeActivity.timeSpent>((Action<PXUIFieldAttribute>) (ui => ui.Visible = trackTime));
        chained = chained.SameFor<PMTimeActivity.approvalStatus>();
        chained = chained.SameFor<PMTimeActivity.earningTypeID>();
        chained = chained.SameFor<PMTimeActivity.isBillable>();
        chained = chained.SameFor<PMTimeActivity.released>();
        chained = chained.SameFor<PMTimeActivity.approverID>();
        chained = chained.SameFor<PMTimeActivity.unionID>();
        chained = chained.SameFor<PMTimeActivity.workCodeID>();
        chained = chained.SameFor<PMTimeActivity.shiftID>();
        chained = chained.For<PMTimeActivity.timeBillable>((Action<PXUIFieldAttribute>) (ui =>
        {
          PXUIFieldAttribute pxuiFieldAttribute = ui;
          int num2;
          if (tAct != null)
          {
            bool? isBillable = tAct.IsBillable;
            if (isBillable.HasValue && isBillable.GetValueOrDefault())
            {
              bool? trackTime1 = tAct.TrackTime;
              if (trackTime1.HasValue)
              {
                num2 = trackTime1.GetValueOrDefault() ? 1 : 0;
                goto label_5;
              }
            }
          }
          num2 = 0;
label_5:
          pxuiFieldAttribute.Visible = num2 != 0;
        }));
        chained = chained.SameFor<PMTimeActivity.arRefNbr>();
        chained = chained.For<PMTimeActivity.projectID>((Action<PXUIFieldAttribute>) (ui => ui.Visible = trackTime & isPmVisible));
        chained = chained.SameFor<PMTimeActivity.certifiedJob>();
        chained = chained.SameFor<PMTimeActivity.projectTaskID>();
        chained = chained.SameFor<PMTimeActivity.costCodeID>();
        chained = chained.SameFor<PMTimeActivity.labourItemID>();
        chained = chained.For<PMTimeActivity.overtimeSpent>((Action<PXUIFieldAttribute>) (ui => ui.Visible = false));
        chained = chained.SameFor<PMTimeActivity.overtimeSpent>();
        chained = chained.SameFor<PMTimeActivity.overtimeBillable>();
        chained = chained.For<PMTimeActivity.projectTaskID>((Action<PXUIFieldAttribute>) (ui =>
        {
          if (tAct == null)
            return;
          PXUIFieldAttribute pxuiFieldAttribute = ui;
          int num3;
          if (tAct.ProjectID.HasValue)
          {
            int? projectId = tAct.ProjectID;
            int? nullable = ProjectDefaultAttribute.NonProject();
            num3 = !(projectId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId.HasValue == nullable.HasValue) ? 1 : 0;
          }
          else
            num3 = 0;
          pxuiFieldAttribute.Required = num3 != 0;
        }));
        chained = chained.ForAllFields((Action<PXUIFieldAttribute>) (ui => ui.Enabled = isOpenPM)).For<PMTimeActivity.timeBillable>((Action<PXUIFieldAttribute>) (ui =>
        {
          if (!isOpenPM)
            return;
          PXUIFieldAttribute pxuiFieldAttribute = ui;
          int num4;
          if (!wasUsed)
          {
            PMTimeActivity pmTimeActivity2 = tAct;
            num4 = pmTimeActivity2 != null ? (pmTimeActivity2.IsBillable.GetValueOrDefault() ? 1 : 0) : 0;
          }
          else
            num4 = 0;
          pxuiFieldAttribute.Enabled = num4 != 0;
        }));
        chained = chained.SameFor<PMTimeActivity.overtimeBillable>();
        chained = chained.For<PMTimeActivity.approvalStatus>((Action<PXUIFieldAttribute>) (ui =>
        {
          PXUIFieldAttribute pxuiFieldAttribute = ui;
          int num5;
          if (tAct != null)
          {
            bool? trackTime2 = tAct.TrackTime;
            if (trackTime2.HasValue && trackTime2.GetValueOrDefault())
            {
              num5 = !wasUsed ? 1 : 0;
              goto label_4;
            }
          }
          num5 = 0;
label_4:
          pxuiFieldAttribute.Enabled = num5 != 0;
        }));
        chained.For<PMTimeActivity.released>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = false));
      }

      void AdjustButtonsUI()
      {
        this.Delete.SetEnabled(isOpen && !wasUsed);
        this.MarkAsCompleted.SetEnabled(isOpen);
        this.MarkAsCompleted.SetVisible(isOpen & trackTime);
        this.MarkAsCompletedAndFollowUp.SetVisible(false);
      }

      string GetOriginalTimeStatus()
      {
        PMTimeActivity pmTimeActivity = tAct;
        return (pmTimeActivity != null ? (pmTimeActivity.Released.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? "CD" : (string) this.TimeActivity.Cache.GetValueOriginal<PMTimeActivity.approvalStatus>((object) tAct) ?? "OP";
      }

      string GetOriginalStatus(string originalTimeStatus)
      {
        string str = (string) this.Activities.Cache.GetValueOriginal<CRActivity.uistatus>((object) row) ?? originalTimeStatus;
        bool? nullable1;
        if (str == "CD")
        {
          bool? nullable2;
          ref bool? local = ref nullable2;
          nullable1 = (bool?) this.TimeActivity.Cache.GetValueOriginal<PMTimeActivity.trackTime>((object) tAct);
          int num = nullable1.GetValueOrDefault() ? 1 : 0;
          local = new bool?(num != 0);
          if (!nullable2.GetValueOrDefault())
            return "OP";
        }
        nullable1 = row.IsLocked;
        return nullable1.GetValueOrDefault() ? "CD" : str;
      }
    }
  }

  protected virtual void PMTimeActivity_ProjectID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if ((PMTimeActivity) e.Row == null || e.NewValue == null || !(e.NewValue is int))
      return;
    PMProject pmProject = (PMProject) PXSelectBase<PMProject, PXSelect<PMProject>.Config>.Search<PMProject.contractID>(sender.Graph, e.NewValue);
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
    PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.ownerID> e)
  {
    e.Cache.SetDefaultExt<PMTimeActivity.labourItemID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.costCodeID> e)
  {
    e.Cache.SetDefaultExt<PMTimeActivity.workCodeID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.projectID> e)
  {
    e.Cache.SetDefaultExt<PMTimeActivity.unionID>((object) e.Row);
    e.Cache.SetDefaultExt<PMTimeActivity.certifiedJob>((object) e.Row);
    e.Cache.SetDefaultExt<PMTimeActivity.labourItemID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.costCodeID> e)
  {
    if (!CostCodeAttribute.UseCostCode())
      return;
    e.NewValue = (object) CostCodeAttribute.DefaultCostCode;
  }

  protected virtual void _(PX.Data.Events.RowInserted<PMTimeActivity> e)
  {
    EmployeeActivitiesEntry.UpdateReportedInTimeZoneIDIfNeeded(e.Cache, e.Row, new DateTime?(), e.Row.Date);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMTimeActivity> e)
  {
    EmployeeActivitiesEntry.UpdateReportedInTimeZoneIDIfNeeded(e.Cache, e.Row, e.OldRow.Date, e.Row.Date);
  }

  public virtual int? GetDefaultLaborItem(int? employeeID, string earningType, int? projectID)
  {
    if (!employeeID.HasValue)
      return new int?();
    int? defaultLaborItem = new int?();
    if (ProjectDefaultAttribute.IsProject((PXGraph) this, projectID))
      defaultLaborItem = EPContractRate.GetProjectLaborClassID((PXGraph) this, projectID.Value, employeeID.Value, earningType);
    if (!defaultLaborItem.HasValue)
      defaultLaborItem = EPEmployeeClassLaborMatrix.GetLaborClassID((PXGraph) this, employeeID, earningType);
    if (!defaultLaborItem.HasValue)
    {
      EPEmployee epEmployee = (EPEmployee) PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<EPTimeCard.employeeID>>>>.Config>.Select((PXGraph) this);
      if (epEmployee != null)
        defaultLaborItem = epEmployee.LabourItemID;
    }
    return defaultLaborItem;
  }

  public override void CompleteRow(CRActivity row) => this.CompleteActivity(row);

  public static TimeSpan CalculateOvertime(
    PXGraph graph,
    PMTimeActivity act,
    DateTime start,
    DateTime end)
  {
    string calendarId = CRActivityMaint.GetCalendarID(graph, act);
    return calendarId != null ? CalendarHelper.CalculateOvertime(graph, start, end, calendarId) : new TimeSpan();
  }

  public static string GetCalendarID(PXGraph graph, PMTimeActivity act)
  {
    return act.ProjectID.With<int?, PX.Objects.CT.Contract>((Func<int?, PX.Objects.CT.Contract>) (_ => (PX.Objects.CT.Contract) PXSelectBase<PX.Objects.CT.Contract, PXSelect<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.contractID, Equal<Required<PX.Objects.CT.Contract.contractID>>>>.Config>.Select(graph, (object) _.Value))).With<PX.Objects.CT.Contract, string>((Func<PX.Objects.CT.Contract, string>) (_ => _.CalendarID)) ?? act.ProjectTaskID.With<int?, PXResult<PX.Objects.CR.Location, PMTask>>((Func<int?, PXResult<PX.Objects.CR.Location, PMTask>>) (_ => (PXResult<PX.Objects.CR.Location, PMTask>) (PXResult<PX.Objects.CR.Location>) PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, InnerJoin<PMTask, On<PMTask.customerID, Equal<PX.Objects.CR.Location.bAccountID>, And<PMTask.locationID, Equal<PX.Objects.CR.Location.locationID>>>>, Where<PMTask.taskID, Equal<Required<PMTask.taskID>>>>.Config>.Select(graph, (object) _.Value))).With<PXResult<PX.Objects.CR.Location, PMTask>, string>((Func<PXResult<PX.Objects.CR.Location, PMTask>, string>) (_ => ((PX.Objects.CR.Location) _).CCalendarID)) ?? act.RefNoteID.With<Guid?, PXResult<PX.Objects.CR.Location, CRCase>>((Func<Guid?, PXResult<PX.Objects.CR.Location, CRCase>>) (_ => (PXResult<PX.Objects.CR.Location, CRCase>) (PXResult<PX.Objects.CR.Location>) PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, InnerJoin<CRCase, On<CRCase.customerID, Equal<PX.Objects.CR.Location.bAccountID>, And<CRCase.locationID, Equal<PX.Objects.CR.Location.locationID>>>, InnerJoin<CRActivityLink, On<CRActivityLink.refNoteID, Equal<CRCase.noteID>>>>, Where<CRActivityLink.noteID, Equal<Required<PMTimeActivity.refNoteID>>>>.Config>.Select(graph, (object) _.Value))).With<PXResult<PX.Objects.CR.Location, CRCase>, string>((Func<PXResult<PX.Objects.CR.Location, CRCase>, string>) (_ => ((PX.Objects.CR.Location) _).CCalendarID)) ?? act.OwnerID.With<int?, EPEmployee>((Func<int?, EPEmployee>) (_ => (EPEmployee) PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.defContactID, Equal<Required<PMTimeActivity.ownerID>>>>.Config>.Select(graph, (object) _.Value))).With<EPEmployee, string>((Func<EPEmployee, string>) (_ => _.CalendarID)) ?? (string) null;
  }

  public static DateTime? GetNextActivityStartDate<Activity>(
    PXGraph graph,
    PXResultset<Activity> res,
    PMTimeActivity row,
    int? fromWeekId,
    int? tillWeekId,
    PXCache tempDataCache,
    System.Type tempDataField)
    where Activity : PMTimeActivity, new()
  {
    DateTime? activityStartDate;
    DateTime? nullable1;
    if (fromWeekId.HasValue || tillWeekId.HasValue)
    {
      activityStartDate = new DateTime?(PXWeekSelector2Attribute.GetWeekStartDate(graph, (fromWeekId ?? tillWeekId).Value));
    }
    else
    {
      ref DateTime? local = ref activityStartDate;
      nullable1 = graph.Accessinfo.BusinessDate;
      DateTime date = (nullable1 ?? DateTime.Now).Date;
      local = new DateTime?(date);
    }
    EPEmployee epEmployee = (EPEmployee) PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.defContactID, Equal<Required<PMTimeActivity.ownerID>>>>.Config>.Select(graph, (object) row.OwnerID);
    EPEmployeeClass epEmployeeClass = (EPEmployeeClass) PXSelectBase<EPEmployeeClass, PXSelect<EPEmployeeClass, Where<EPEmployeeClass.vendorClassID, Equal<Required<EPEmployee.vendorClassID>>>>.Config>.Select(graph, (object) epEmployee?.VendorClassID);
    string calendarId = CRActivityMaint.GetCalendarID(graph, row);
    if (epEmployeeClass != null && "LD" == epEmployeeClass.DefaultDateInActivity)
    {
      DateTime? nullable2 = tempDataCache.GetValue(tempDataCache.Current, tempDataField.Name) as DateTime?;
      if (nullable2.HasValue)
      {
        int weekId = PXWeekSelector2Attribute.GetWeekID(graph, nullable2.Value);
        if (fromWeekId.HasValue)
        {
          int num = weekId;
          int? nullable3 = fromWeekId;
          int valueOrDefault = nullable3.GetValueOrDefault();
          if (!(num >= valueOrDefault & nullable3.HasValue))
            goto label_26;
        }
        if (tillWeekId.HasValue)
        {
          int? nullable4 = tillWeekId;
          int num = weekId;
          if (!(nullable4.GetValueOrDefault() >= num & nullable4.HasValue))
            goto label_26;
        }
        activityStartDate = nullable2;
      }
    }
    else
    {
      DateTime date1 = activityStartDate.Value;
      DateTime? nullable5 = new DateTime?();
      List<PXResult<Activity>> list = res != null ? res.ToList<PXResult<Activity>>() : (List<PXResult<Activity>>) null;
      activityStartDate = list == null || list.Count <= 0 ? activityStartDate : list.Max<PXResult<Activity>, DateTime?>((Func<PXResult<Activity>, DateTime?>) (_ => ((Activity) _).Date));
      int weekId = PXWeekSelector2Attribute.GetWeekID(graph, date1);
      while (true)
      {
        if (tillWeekId.HasValue)
          goto label_25;
label_11:
        foreach (KeyValuePair<DayOfWeek, PXWeekSelector2Attribute.DayInfo> keyValuePair in (IEnumerable<KeyValuePair<DayOfWeek, PXWeekSelector2Attribute.DayInfo>>) PXWeekSelector2Attribute.GetWeekInfo(graph, PXWeekSelector2Attribute.GetWeekID(graph, date1)).Days.OrderBy<KeyValuePair<DayOfWeek, PXWeekSelector2Attribute.DayInfo>, DateTime?>((Func<KeyValuePair<DayOfWeek, PXWeekSelector2Attribute.DayInfo>, DateTime?>) (_ => _.Value.Date)))
        {
          nullable1 = keyValuePair.Value.Date;
          DateTime? nullable6 = activityStartDate;
          if ((nullable1.HasValue & nullable6.HasValue ? (nullable1.GetValueOrDefault() >= nullable6.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            PXGraph graph1 = graph;
            string calendarID = calendarId;
            nullable6 = keyValuePair.Value.Date;
            DateTime date2 = nullable6.Value;
            if (CalendarHelper.IsWorkDay(graph1, calendarID, date2) || string.IsNullOrEmpty(calendarId) && keyValuePair.Key != DayOfWeek.Saturday && keyValuePair.Key != DayOfWeek.Sunday)
            {
              nullable6 = keyValuePair.Value.Date;
              nullable5 = new DateTime?(nullable6.Value);
              break;
            }
          }
          date1 = date1.AddDays(1.0);
        }
        if (!nullable5.HasValue)
        {
          weekId = PXWeekSelector2Attribute.GetWeekID(graph, date1);
          continue;
        }
        break;
label_25:
        int num = weekId;
        int? nullable7 = tillWeekId;
        int valueOrDefault = nullable7.GetValueOrDefault();
        if (num <= valueOrDefault & nullable7.HasValue)
          goto label_11;
        goto label_26;
      }
      activityStartDate = new DateTime?(nullable5.Value.Date);
    }
label_26:
    if (!string.IsNullOrEmpty(calendarId) && activityStartDate.HasValue)
    {
      DateTime startDate;
      CalendarHelper.CalculateStartEndTime(graph, calendarId, activityStartDate.Value, out startDate, out DateTime _);
      activityStartDate = new DateTime?(startDate);
    }
    return activityStartDate;
  }

  private void CompleteActivity(CRActivity activity)
  {
    string str = (string) this.Activities.Cache.GetValueOriginal<CRActivity.uistatus>((object) activity) ?? "OP";
    PMTimeActivity current = this.TimeActivity.Current;
    PMActiveLaborItemAttribute.VerifyLaborItem<PMTimeActivity.labourItemID>(this.TimeActivity.Cache, (object) current);
    if (activity == null)
      return;
    switch (str)
    {
      case "CD":
        break;
      case "CL":
        break;
      default:
        activity.UIStatus = "CD";
        this.Activities.Cache.Update((object) activity);
        if (current != null)
        {
          current.ApprovalStatus = "CD";
          this.TimeActivity.Cache.Update((object) current);
        }
        this.Actions.PressSave();
        break;
    }
  }

  private EPSetup EPSetupCurrent
  {
    get
    {
      return (EPSetup) PXSelectBase<EPSetup, PXSelect<EPSetup>.Config>.SelectWindowed((PXGraph) this, 0, 1) ?? CRActivityMaint.EmptyEpSetup;
    }
  }

  public class EmbeddedImagesExtractor : 
    EmbeddedImagesExtractorExtension<CRActivityMaint, CRActivity, CRActivity.body>
  {
  }

  [PXHidden]
  [Serializable]
  public class EPTempData : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _LastEnteredDate;

    [PXDate]
    public virtual DateTime? LastEnteredDate
    {
      get => this._LastEnteredDate;
      set => this._LastEnteredDate = value;
    }

    public abstract class lastEnteredDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CRActivityMaint.EPTempData.lastEnteredDate>
    {
    }
  }
}
