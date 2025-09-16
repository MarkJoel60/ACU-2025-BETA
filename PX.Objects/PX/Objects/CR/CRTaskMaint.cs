// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRTaskMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.EP;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.CR;

public class CRTaskMaint : CRBaseActivityMaint<CRTaskMaint, CRActivity>, ICaptionable
{
  [PXHidden]
  public PXSelect<PX.Objects.CT.Contract> BaseContract;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (CRActivity.noteID), typeof (CRActivity.completedDate), typeof (CRActivity.percentCompletion), typeof (CRActivity.endDate.endDate_date), typeof (CRActivity.startDate.startDate_date), typeof (CRActivity.uistatus)})]
  public PXSelect<CRActivity, Where<CRActivity.classID, Equal<CRActivityClass.task>>> Tasks;
  [PXHidden]
  public PXSelect<CRActivity, Where<CRActivity.noteID, Equal<Current<CRActivity.noteID>>>> CurrentTask;
  [PXHidden]
  public PXSelect<PMTimeActivity> TimeActivitiesOld;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (PMTimeActivity.timeSpent), typeof (PMTimeActivity.overtimeSpent), typeof (PMTimeActivity.timeBillable), typeof (PMTimeActivity.overtimeBillable)})]
  public PMTimeActivityList<CRActivity> TimeActivity;
  [PXCopyPasteHiddenView]
  public CRReminderList<CRActivity> Reminder;
  public PXCopyPasteAction<CRActivity> CopyPaste;
  public PXDelete<CRActivity> Delete;
  public PXAction<CRActivity> Complete;
  public PXAction<CRActivity> CompleteAndFollowUp;
  public PXAction<CRActivity> CancelActivity;

  public CRTaskMaint()
  {
    ActivityStatusListAttribute.SetRestictedMode<CRActivity.uistatus>(((PXSelectBase) this.Tasks).Cache, true);
  }

  public string Caption()
  {
    CRActivity current = ((PXSelectBase<CRActivity>) this.Tasks).Current;
    return current == null || current.Subject == null ? "" : current.Subject ?? "";
  }

  [PXUIField]
  [PXButton]
  protected virtual void complete()
  {
    CRActivity current = ((PXSelectBase<CRActivity>) this.Tasks).Current;
    if (current == null)
      return;
    this.CompleteTask(current);
  }

  [PXUIField]
  [PXButton]
  protected virtual void completeAndFollowUp()
  {
    CRActivity current1 = ((PXSelectBase<CRActivity>) this.Tasks).Current;
    if (current1 == null)
      return;
    this.CompleteTask(current1);
    CRTaskMaint instance = PXGraph.CreateInstance<CRTaskMaint>();
    CRActivity copy = (CRActivity) ((PXSelectBase) instance.Tasks).Cache.CreateCopy((object) current1);
    copy.NoteID = new Guid?();
    copy.ParentNoteID = current1.NoteID;
    copy.UIStatus = (string) null;
    copy.PercentCompletion = new int?();
    CRActivity destData = (CRActivity) ((PXSelectBase) instance.Tasks).Cache.Insert((object) copy);
    UDFHelper.CopyAttributes((PXCache) GraphHelper.Caches<CRActivity>((PXGraph) this), (object) current1, ((PXSelectBase) instance.Tasks).Cache, (object) destData, (string) null);
    CRReminder current2 = this.Reminder.Current;
    CRReminder current3 = instance.Reminder.Current;
    if (current2 != null && current3 != null)
    {
      current3.ReminderDate = current2.ReminderDate;
      current3.RefNoteID = destData.NoteID;
      instance.Reminder.Cache.Update((object) current3);
    }
    if (!((PXGraph) this).IsContractBasedAPI)
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
    ((PXAction) instance.Save).Press();
  }

  [PXUIField]
  [PXButton(Tooltip = "Cancel")]
  protected virtual void cancelActivity()
  {
    CRActivity current = ((PXSelectBase<CRActivity>) this.Tasks).Current;
    if (current == null)
      return;
    this.CancelTask(current);
  }

  [PXRemoveBaseAttribute(typeof (PXNavigateSelectorAttribute))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRActivity.subject> e)
  {
  }

  [TaskStatus]
  [PXMergeAttributes]
  protected virtual void CRActivity_UIStatus_CacheAttached(PXCache cache)
  {
  }

  [PXUIField(DisplayName = "Start Date")]
  [EPStartDate(DisplayName = "Start Date", DisplayNameDate = "Start Date", DisplayNameTime = "Start Time", PreserveTime = false, BqlField = typeof (CRActivity.startDate), AllDayField = typeof (CRActivity.allDay))]
  [PXMergeAttributes]
  protected virtual void CRActivity_StartDate_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "Due Date")]
  [EPEndDate(typeof (CRActivity.classID), typeof (CRActivity.startDate), null, DisplayNameDate = "Due Date", DisplayNameTime = "Due Time", PreserveTime = false, BqlField = typeof (CRActivity.endDate), AllDayField = typeof (CRActivity.allDay))]
  [PXMergeAttributes]
  protected virtual void CRActivity_EndDate_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "Track Time", Visible = false)]
  [PXDefault(false)]
  [PXMergeAttributes]
  protected virtual void PMTimeActivity_TrackTime_CacheAttached(PXCache cache)
  {
  }

  [PXUIField(DisplayName = "Billable", Visible = false)]
  [PXMergeAttributes]
  protected virtual void PMTimeActivity_IsBillable_CacheAttached(PXCache cache)
  {
  }

  [PXFormula(typeof (False))]
  [PXMergeAttributes]
  protected virtual void PMTimeActivity_NeedToBeDeleted_CacheAttached(PXCache cache)
  {
  }

  [PXDefault(0)]
  [PXMergeAttributes]
  protected virtual void CRActivity_ClassID_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes]
  [ProjectTask(typeof (PMTimeActivity.projectID), "TA", DisplayName = "Project Task", DefaultNotClosedTask = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMTimeActivity.projectTaskID> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CRActivity, CRActivity.uistatus> e)
  {
    PMTimeActivity pmTimeActivity = this.TimeActivity.SelectSingle();
    if (pmTimeActivity == null)
      return;
    GraphHelper.MarkUpdated(((PXGraph) this).Caches[typeof (PMTimeActivity)], (object) pmTimeActivity);
  }

  protected virtual void CRActivity_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    CRActivity row = (CRActivity) e.Row;
    if (row == null || !(row.UIStatus == "CD"))
      return;
    row.PercentCompletion = new int?(100);
  }

  protected virtual void CRActivity_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    CRActivity row = e.Row as CRActivity;
    CRActivity oldRow = (CRActivity) e.OldRow;
    if (row == null || oldRow == null || !(row.UIStatus == "CD"))
      return;
    row.PercentCompletion = new int?(100);
    if (object.Equals(sender.GetValueOriginal<CRActivity.uistatus>((object) row), (object) "CD"))
      return;
    row.CompletedDate = new DateTime?(PXTimeZoneInfo.Now);
  }

  protected virtual void CRActivity_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is CRActivity row))
      return;
    bool flag = this.IsTaskEditable(row);
    PXUIFieldAttribute.SetEnabled(cache, (object) row, flag);
    ((PXAction) this.Complete).SetEnabled(row.UIStatus == "OP" || row.UIStatus == "IP");
    if (row.UIStatus == "OP" || row.UIStatus == "IP")
      ((PXAction) this.Complete).SetConnotation((ActionConnotation) 3);
    else
      ((PXAction) this.Complete).SetConnotation((ActionConnotation) 0);
    ((PXAction) this.CompleteAndFollowUp).SetEnabled(row.UIStatus == "OP" || row.UIStatus == "IP");
    ((PXAction) this.CancelActivity).SetEnabled(row.UIStatus == "OP" || row.UIStatus == "DR" || row.UIStatus == "IP");
    PXUIFieldAttribute.SetEnabled<CRActivity.noteID>(cache, (object) row);
    PXUIFieldAttribute.SetEnabled<CRActivity.uistatus>(cache, (object) row);
    PXUIFieldAttribute.SetEnabled<CRActivity.createdByID>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CRActivity.completedDate>(cache, (object) row, false);
    PMTimeActivity pmTimeActivity = this.TimeActivity.SelectSingle();
    PXCache cache1 = ((PXSelectBase) this.TimeActivity).Cache;
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.projectID>(cache1, (object) pmTimeActivity, flag);
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.projectTaskID>(cache1, (object) pmTimeActivity, flag);
    PXUIFieldAttribute.SetEnabled<CRReminder.isReminderOn>(this.Reminder.Cache, this.Reminder.SelectSingle(), flag);
    PXUIFieldAttribute.SetEnabled<CRActivity.parentNoteID>(cache, (object) row, flag);
    this.MarkAs(cache, row, ((PXGraph) this).Accessinfo.ContactID, 1);
    PXUIFieldAttribute.SetEnabled<CRActivity.refNoteID>(cache, (object) row, cache.GetValue<CRActivity.refNoteIDType>((object) row) != null || ((PXGraph) this).IsContractBasedAPI);
    PXUIFieldAttribute.SetEnabled<CRActivity.responseActivityNoteID>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CRActivity.responseDueDateTime>(cache, (object) row, false);
  }

  protected virtual void CRActivity_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is CRActivity row) || e.Operation != 2 && e.Operation != 1)
      return;
    int? nullable = row.OwnerID;
    if (nullable.HasValue)
      return;
    nullable = row.WorkgroupID;
    if (nullable.HasValue)
      return;
    string displayName = PXUIFieldAttribute.GetDisplayName<CRActivity.ownerID>(((PXSelectBase) this.Tasks).Cache);
    PXSetPropertyException propertyException = new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) displayName
    });
    if (((PXSelectBase) this.Tasks).Cache.RaiseExceptionHandling<CRActivity.ownerID>((object) row, (object) null, (Exception) propertyException))
      throw new PXRowPersistingException(typeof (CRActivity.ownerID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) displayName
      });
  }

  [PXSelector(typeof (Search<CRActivity.noteID, Where<CRActivity.classID, Equal<CRActivityClass.task>>>), new System.Type[] {typeof (CRActivity.subject), typeof (CRActivity.uistatus), typeof (CRActivity.startDate), typeof (CRActivity.endDate), typeof (CRActivity.ownerID), typeof (CRActivity.priority), typeof (CRActivity.refNoteID), typeof (CRActivity.source)})]
  [PXMergeAttributes]
  protected virtual void CRActivity_ParentNoteID_CacheAttached(PXCache cache)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.costCodeID> e)
  {
    if (!CostCodeAttribute.UseCostCode())
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.costCodeID>, PMTimeActivity, object>) e).NewValue = (object) CostCodeAttribute.DefaultCostCode;
  }

  protected virtual void _(PX.Data.Events.RowSelected<CRReminder> e)
  {
    if (e.Row == null)
      return;
    bool flag = true.Equals((object) e.Row.IsReminderOn);
    PXUIFieldAttribute.SetVisible<CRReminder.reminderDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRReminder>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetEnabled<CRReminder.reminderDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRReminder>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetRequired<CRReminder.reminderDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRReminder>>) e).Cache, flag);
  }

  private void CompleteTask(CRActivity row)
  {
    switch ((string) ((PXSelectBase) this.Tasks).Cache.GetValueOriginal<CRActivity.uistatus>((object) row) ?? "OP")
    {
      case "CD":
        break;
      case "CL":
        break;
      default:
        CRActivity copy = (CRActivity) ((PXSelectBase) this.Tasks).Cache.CreateCopy((object) row);
        copy.UIStatus = "CD";
        ((PXSelectBase) this.Tasks).Cache.Update((object) copy);
        ((PXGraph) this).Actions.PressSave();
        break;
    }
  }

  private void CancelTask(CRActivity row)
  {
    switch ((string) ((PXSelectBase) this.Tasks).Cache.GetValueOriginal<CRActivity.uistatus>((object) row) ?? "OP")
    {
      case "CD":
        break;
      case "CL":
        break;
      default:
        CRActivity copy = (CRActivity) ((PXSelectBase) this.Tasks).Cache.CreateCopy((object) row);
        copy.UIStatus = "CL";
        ((PXSelectBase) this.Tasks).Cache.Update((object) copy);
        ((PXGraph) this).Actions.PressSave();
        break;
    }
  }

  public override void CompleteRow(CRActivity row)
  {
    if (row == null)
      return;
    this.CompleteTask(row);
  }

  public override void CancelRow(CRActivity row)
  {
    if (row == null)
      return;
    this.CancelTask(row);
  }

  public virtual bool IsTaskEditable(CRActivity row)
  {
    string str = (string) ((PXSelectBase) this.Tasks).Cache.GetValueOriginal<CRActivity.uistatus>((object) row) ?? "OP";
    return str == "OP" || str == "DR" || str == "IP";
  }

  public class EmbeddedImagesExtractor : 
    EmbeddedImagesExtractorExtension<CRTaskMaint, CRActivity, CRActivity.body>
  {
  }
}
