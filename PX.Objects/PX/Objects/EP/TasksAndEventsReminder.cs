// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.TasksAndEventsReminder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.SQLTree;
using PX.Objects.CR;
using PX.Objects.IN;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

#nullable enable
namespace PX.Objects.EP;

[PXHidden]
public class TasksAndEventsReminder : PXGraph<
#nullable disable
TasksAndEventsReminder>
{
  private const string _REMINDER_SLOT_KEY_PREFIX = "ReminderStatistics@";
  private const string _ACTIVITY_SLOT_KEY_PREFIX = "ActivityStatistics@";
  private const string _REMINDERLIST_SLOT_KEY_PREFIX = "ReminderList@";
  [PXHidden]
  public PXFilter<TasksAndEventsReminder.DeferFilterInfo> DeferFilter;
  public PXSelectReadonly<PX.Objects.CR.CRActivity> ReminderListCurrent;
  public PXSelectReadonly2<PX.Objects.CR.CRActivity, LeftJoin<EPAttendee, On<EPAttendee.contactID, Equal<Current<AccessInfo.contactID>>, And<EPAttendee.eventNoteID, Equal<PX.Objects.CR.CRActivity.noteID>>>, LeftJoin<CRReminder, On<CRReminder.refNoteID, Equal<PX.Objects.CR.CRActivity.noteID>, And<CRReminder.owner, Equal<Current<AccessInfo.contactID>>>>>>, Where2<Where<PX.Objects.CR.CRActivity.classID, Equal<CRActivityClass.task>, Or<PX.Objects.CR.CRActivity.classID, Equal<CRActivityClass.events>>>, And2<Where<PX.Objects.CR.CRActivity.uistatus, NotEqual<ActivityStatusListAttribute.canceled>, And<PX.Objects.CR.CRActivity.uistatus, NotEqual<ActivityStatusListAttribute.completed>, And<PX.Objects.CR.CRActivity.uistatus, NotEqual<ActivityStatusListAttribute.released>>>>, And<CRReminder.reminderDate, PX.Data.IsNotNull, And2<Where<CRReminder.reminderDate, Less<Now>, And<CRReminder.dismiss, NotEqual<PX.Data.True>>>, PX.Data.And<Where<CRReminder.owner, Equal<Current<AccessInfo.contactID>>, Or<EPAttendee.contactID, PX.Data.IsNotNull>>>>>>>, OrderBy<Asc<CRReminder.reminderDate>>> ReminderList;
  public PXSelectJoin<CRReminder, InnerJoin<PX.Objects.CR.CRActivity, On<PX.Objects.CR.CRActivity.noteID, Equal<Required<PX.Objects.CR.CRActivity.noteID>>>>, Where<CRReminder.refNoteID, Equal<PX.Objects.CR.CRActivity.noteID>, And<CRReminder.owner, Equal<Current<AccessInfo.contactID>>>>> RemindInfo;
  public PXSelectReadonly<PX.Objects.CR.CRActivity, Where2<Where<PX.Objects.CR.CRActivity.classID, Equal<CRActivityClass.task>, Or<PX.Objects.CR.CRActivity.classID, Equal<CRActivityClass.events>, Or<PX.Objects.CR.CRActivity.classID, Equal<CRActivityClass.activity>, Or<PX.Objects.CR.CRActivity.classID, Equal<CRActivityClass.email>>>>>, PX.Data.And<Where<PX.Objects.CR.CRActivity.uistatus, NotEqual<ActivityStatusListAttribute.canceled>, And<PX.Objects.CR.CRActivity.uistatus, NotEqual<ActivityStatusListAttribute.released>>>>>, OrderBy<Asc<PX.Objects.CR.CRActivity.startDate>>> ActivityList;
  public PXSelectGroupBy<PX.Objects.CR.CRActivity, Where2<Where<PX.Objects.CR.CRActivity.classID, Equal<CRActivityClass.task>, Or<PX.Objects.CR.CRActivity.classID, Equal<CRActivityClass.events>, Or<PX.Objects.CR.CRActivity.classID, Equal<CRActivityClass.activity>, Or<PX.Objects.CR.CRActivity.classID, Equal<CRActivityClass.email>>>>>, PX.Data.And<Where<PX.Objects.CR.CRActivity.uistatus, NotEqual<ActivityStatusListAttribute.canceled>, And<PX.Objects.CR.CRActivity.uistatus, NotEqual<ActivityStatusListAttribute.released>>>>>, Aggregate<Count>> ActivityCount;
  public PXSelectReadonly2<PX.Objects.CR.CRActivity, LeftJoin<EPView, On<EPView.noteID, Equal<PX.Objects.CR.CRActivity.noteID>, And<EPView.contactID, Equal<Current<AccessInfo.contactID>>>>, LeftJoin<EPAttendee, On<EPAttendee.contactID, Equal<Current<AccessInfo.contactID>>, And<EPAttendee.eventNoteID, Equal<PX.Objects.CR.CRActivity.noteID>>>, LeftJoin<CRReminder, On<CRReminder.refNoteID, Equal<PX.Objects.CR.CRActivity.noteID>, And<CRReminder.owner, Equal<Current<AccessInfo.contactID>>>>>>>, Where2<Where<PX.Objects.CR.CRActivity.classID, Equal<CRActivityClass.task>, Or<PX.Objects.CR.CRActivity.classID, Equal<CRActivityClass.events>>>, And2<Where<PX.Objects.CR.CRActivity.uistatus, NotEqual<ActivityStatusListAttribute.canceled>, And<PX.Objects.CR.CRActivity.uistatus, NotEqual<ActivityStatusListAttribute.completed>, And<PX.Objects.CR.CRActivity.uistatus, NotEqual<ActivityStatusListAttribute.released>>>>, And<CRReminder.reminderDate, PX.Data.IsNotNull, And2<Where<CRReminder.reminderDate, Less<Now>, And<CRReminder.dismiss, NotEqual<PX.Data.True>>>, PX.Data.And<Where<CRReminder.owner, Equal<Current<AccessInfo.contactID>>, Or<EPAttendee.contactID, PX.Data.IsNotNull>>>>>>>, OrderBy<Asc<CRReminder.reminderDate>>> SlotView;
  public PXSelect<PX.Data.EP.ActivityService.Total> Counters;
  [PXHidden]
  public PXSetupOptional<EPSetup> Setup;
  public PXAction<PX.Objects.CR.CRActivity> navigate;
  public PXAction<PX.Objects.CR.CRActivity> viewActivity;
  public PXAction<PX.Objects.CR.CRActivity> dismiss;
  public PXAction<PX.Objects.CR.CRActivity> dismissAll;
  public PXAction<PX.Objects.CR.CRActivity> dismissCurrent;
  public PXAction<PX.Objects.CR.CRActivity> defer;
  public PXAction<PX.Objects.CR.CRActivity> deferCurrent;
  public PXAction<PX.Objects.CR.CRActivity> completeRow;
  public PXAction<PX.Objects.CR.CRActivity> cancelRow;
  [Obsolete]
  public PXAction<PX.Objects.CR.CRActivity> openInquiry;
  private EntityHelper _EntityHelper;

  public virtual IEnumerable counters()
  {
    TasksAndEventsReminder.ActivityStatistics.Source[] sourceArray1 = new TasksAndEventsReminder.ActivityStatistics.Source[4]
    {
      new TasksAndEventsReminder.ActivityStatistics.Source("EP404000", (string) null, "Task"),
      new TasksAndEventsReminder.ActivityStatistics.Source("EP4041PL", "Events", "Event"),
      new TasksAndEventsReminder.ActivityStatistics.Source("CO409000", (string) null, "MailReceive"),
      new TasksAndEventsReminder.ActivityStatistics.Source("EP503010", (string) null, "Roles", new System.Type[2]
      {
        typeof (EPApproval),
        typeof (UserPreferences)
      })
    };
    List<PX.Data.EP.ActivityService.Total> totalList = new List<PX.Data.EP.ActivityService.Total>();
    TasksAndEventsReminder.ActivityStatistics.Source[] sourceArray = sourceArray1;
    for (int index = 0; index < sourceArray.Length; ++index)
    {
      TasksAndEventsReminder.ActivityStatistics.Source source = sourceArray[index];
      TasksAndEventsReminder.ActivityStatistics fromSlot = TasksAndEventsReminder.ActivityStatistics.GetFromSlot(source);
      if (fromSlot != null && fromSlot.Count > 0)
      {
        PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(source.SrceenID);
        if (mapNodeByScreenId != null && PXAccess.VerifyRights(mapNodeByScreenId.ScreenID))
          yield return (object) new PX.Data.EP.ActivityService.Total()
          {
            ScreenID = source.SrceenID,
            ImageKey = source.ImageKey,
            ImageSet = source.ImageSet,
            Url = mapNodeByScreenId.Url,
            Title = mapNodeByScreenId.Title,
            Count = new int?(fromSlot.Count),
            NewCount = new int?(fromSlot.UnreadCount)
          };
      }
    }
    sourceArray = (TasksAndEventsReminder.ActivityStatistics.Source[]) null;
  }

  public virtual IEnumerable reminderListCurrent([PXGuid] Guid? noteID)
  {
    TasksAndEventsReminder graph = this;
    if (!noteID.HasValue)
    {
      if (graph.ReminderList.Current == null)
        yield break;
      noteID = graph.ReminderList.Current.NoteID;
    }
    yield return (object) (PX.Objects.CR.CRActivity) PXSelectBase<PX.Objects.CR.CRActivity, PXSelectReadonly<PX.Objects.CR.CRActivity>.Config>.Search<PX.Objects.CR.CRActivity.noteID>((PXGraph) graph, (object) noteID);
  }

  public virtual IEnumerable reminderList()
  {
    PXResult<PX.Objects.CR.CRActivity>[] pxResultArray = this.ActivityReminder.ReminderList;
    for (int index = 0; index < pxResultArray.Length; ++index)
    {
      PXResult<PX.Objects.CR.CRActivity, EPView, EPAttendee> pxResult = (PXResult<PX.Objects.CR.CRActivity, EPView, EPAttendee>) pxResultArray[index];
      EPView epView = (EPView) pxResult;
      yield return (object) new PXResult<PX.Objects.CR.CRActivity, EPAttendee>((PX.Objects.CR.CRActivity) pxResult, (EPAttendee) pxResult);
    }
    pxResultArray = (PXResult<PX.Objects.CR.CRActivity>[]) null;
  }

  [PXButton]
  public virtual IEnumerable Navigate(PXAdapter adapter)
  {
    this.NavigateToItem(this.ReminderList.Current);
    return adapter.Get();
  }

  public virtual void NavigateToItem(PX.Objects.CR.CRActivity current)
  {
    PXRedirectHelper.TryRedirect((PXGraph) this, (object) current, PXRedirectHelper.WindowMode.NewWindow);
  }

  [PXButton(OnClosingPopup = PXSpecialButtonType.Refresh)]
  [PXUIField(DisplayName = "View Details")]
  public virtual IEnumerable ViewActivity(PXAdapter adapter)
  {
    if (this.ReminderList.Current != null)
      PXRedirectHelper.TryOpenPopup(this.ReminderList.Cache, (object) this.ReminderList.Current, "Open");
    return adapter.Get();
  }

  [PXButton]
  public virtual IEnumerable Dismiss(PXAdapter adapter)
  {
    Guid? activityId = TasksAndEventsReminder.ExtractActivityID(adapter);
    if (activityId.HasValue)
      this.UpdateAcitivtyRemindInfo(activityId, (TasksAndEventsReminder.UpdateRemindInfo) (reminder => reminder.Dismiss = new bool?(true)));
    this.RefreshActivityReminder();
    return adapter.Get();
  }

  [PXButton(ClosePopup = true, OnClosingPopup = PXSpecialButtonType.Refresh)]
  [PXUIField(DisplayName = "Dismiss All")]
  public virtual IEnumerable DismissAll(PXAdapter adapter)
  {
    foreach (PXResult<PX.Objects.CR.CRActivity> pxResult in this.ReminderList.Select())
      this.UpdateAcitivtyRemindInfo(((PX.Objects.CR.CRActivity) pxResult).NoteID, (TasksAndEventsReminder.UpdateRemindInfo) (reminder => reminder.Dismiss = new bool?(true)));
    this.RefreshActivityReminder();
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Dismiss")]
  public virtual IEnumerable DismissCurrent(PXAdapter adapter)
  {
    if (this.ReminderList.Current != null)
      this.UpdateAcitivtyRemindInfo(this.ReminderList.Current.NoteID, (TasksAndEventsReminder.UpdateRemindInfo) (reminder => reminder.Dismiss = new bool?(true)));
    this.RefreshActivityReminder();
    if (this.ReminderList.Select().Count == 0)
      throw new PXClosePopupException("");
    return adapter.Get();
  }

  [PXButton]
  public virtual IEnumerable Defer(PXAdapter adapter)
  {
    Guid? activityId = TasksAndEventsReminder.ExtractActivityID(adapter);
    if (activityId.HasValue)
      this.UpdateAcitivtyRemindInfo(activityId, (TasksAndEventsReminder.UpdateRemindInfo) (reminder => reminder.ReminderDate = new System.DateTime?(PXTimeZoneInfo.Now.AddMinutes((double) Convert.ToInt32(adapter.Parameters[1])))));
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Snooze")]
  public virtual IEnumerable DeferCurrent(PXAdapter adapter)
  {
    if (this.ReminderList.Current != null)
    {
      int? type = this.DeferFilter.Current.Type;
      int num;
      if (type.HasValue)
      {
        type = this.DeferFilter.Current.Type;
        num = type.Value;
      }
      else
        num = 5;
      int minutes = num;
      this.UpdateAcitivtyRemindInfo(this.ReminderList.Current.NoteID, (TasksAndEventsReminder.UpdateRemindInfo) (reminder => reminder.ReminderDate = new System.DateTime?(PXTimeZoneInfo.Now.AddMinutes((double) minutes))));
      this.RefreshActivityReminder();
    }
    if (this.ReminderList.Select().Count == 0)
      throw new PXClosePopupException("");
    return adapter.Get();
  }

  [PXButton(Tooltip = "Marks current record as completed")]
  [PXUIField(DisplayName = "Complete")]
  public virtual IEnumerable CompleteRow(PXAdapter adapter)
  {
    PX.Objects.CR.CRActivity crActivity = this.ReadActivity((object) TasksAndEventsReminder.ExtractActivityID(adapter));
    System.Type type = crActivity.With<PX.Objects.CR.CRActivity, System.Type>((Func<PX.Objects.CR.CRActivity, System.Type>) (_ => this.EntityHelper.GetPrimaryGraphType((object) _, true)));
    if (type != (System.Type) null)
      (Activator.CreateInstance(type) as IActivityMaint).CompleteRow(crActivity);
    adapter.Parameters = new object[0];
    return adapter.Get();
  }

  [PXButton(Tooltip = "Marks current record as canceled")]
  [PXUIField(DisplayName = "Cancel")]
  public virtual IEnumerable CancelRow(PXAdapter adapter)
  {
    PX.Objects.CR.CRActivity crActivity = this.ReadActivity((object) TasksAndEventsReminder.ExtractActivityID(adapter));
    System.Type type = crActivity.With<PX.Objects.CR.CRActivity, System.Type>((Func<PX.Objects.CR.CRActivity, System.Type>) (_ => this.EntityHelper.GetPrimaryGraphType((object) _, true)));
    if (type != (System.Type) null)
      (Activator.CreateInstance(type) as IActivityMaint).CancelRow(crActivity);
    adapter.Parameters = new object[0];
    return adapter.Get();
  }

  [PXButton(Tooltip = "Marks current record as canceled")]
  [PXUIField(DisplayName = "Cancel")]
  [Obsolete]
  public virtual IEnumerable OpenInquiry(PXAdapter adapter)
  {
    if (adapter.Parameters.Length != 0 && adapter.Parameters[0] != null)
      this.OpenInquiryScreen((Guid) adapter.Parameters[0]);
    return adapter.Get();
  }

  [Obsolete]
  public virtual void OpenInquiryScreen(Guid refNoteID)
  {
    if (!(refNoteID != Guid.Empty))
      return;
    ActivitiesMaint instance = PXGraph.CreateInstance<ActivitiesMaint>();
    instance.Filter.Current.NoteID = new Guid?(refNoteID);
    PXRedirectHelper.TryRedirect((PXGraph) instance, PXRedirectHelper.WindowMode.NewWindow);
  }

  public virtual int GetListCount()
  {
    return PXSelectBase<PX.Objects.CR.CRActivity, PXSelectJoinGroupBy<PX.Objects.CR.CRActivity, LeftJoin<EPAttendee, On<EPAttendee.contactID, Equal<Current<AccessInfo.contactID>>, And<EPAttendee.eventNoteID, Equal<PX.Objects.CR.CRActivity.noteID>>>, LeftJoin<CRReminder, On<CRReminder.refNoteID, Equal<PX.Objects.CR.CRActivity.noteID>, And<CRReminder.owner, Equal<Current<AccessInfo.contactID>>>>>>, Where2<Where<PX.Objects.CR.CRActivity.classID, Equal<CRActivityClass.task>, Or<PX.Objects.CR.CRActivity.classID, Equal<CRActivityClass.events>>>, And2<Where<PX.Objects.CR.CRActivity.uistatus, NotEqual<ActivityStatusListAttribute.canceled>, And<PX.Objects.CR.CRActivity.uistatus, NotEqual<ActivityStatusListAttribute.completed>, And<PX.Objects.CR.CRActivity.uistatus, NotEqual<ActivityStatusListAttribute.released>>>>, And<CRReminder.reminderDate, PX.Data.IsNotNull, And2<Where<CRReminder.reminderDate, Less<Now>, And<CRReminder.dismiss, NotEqual<PX.Data.True>>>, PX.Data.And<Where<CRReminder.owner, Equal<Current<AccessInfo.contactID>>, Or<EPAttendee.contactID, PX.Data.IsNotNull>>>>>>>, Aggregate<Count>>.Config>.Select((PXGraph) this).RowCount.Value;
  }

  public EntityHelper EntityHelper
  {
    get
    {
      if (this._EntityHelper == null)
        this._EntityHelper = new EntityHelper((PXGraph) this);
      return this._EntityHelper;
    }
  }

  private EPSetup SetupCurrent
  {
    get
    {
      try
      {
        return this.Setup.Current;
      }
      catch (OutOfMemoryException ex)
      {
      }
      catch (OverflowException ex)
      {
      }
      catch (PXSetPropertyException ex)
      {
      }
      return (EPSetup) null;
    }
  }

  private void UpdateAcitivtyRemindInfo(Guid? id, TasksAndEventsReminder.UpdateRemindInfo handler)
  {
    CRReminder info = (CRReminder) this.RemindInfo.Select((object) id);
    if (info == null && (ValueType) id is Guid)
    {
      info = (CRReminder) this.RemindInfo.Cache.Insert();
      info.RefNoteID = new Guid?(id.Value);
      info.Owner = PXAccess.GetContactID();
      info.ReminderDate = new System.DateTime?(PXTimeZoneInfo.Now);
      this.RemindInfo.Cache.Normalize();
    }
    handler(info);
    this.RemindInfo.Update(info);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this.RemindInfo.Cache.Persist(PXDBOperation.Insert);
      this.RemindInfo.Cache.Persist(PXDBOperation.Update);
      transactionScope.Complete((PXGraph) this);
    }
    this.RemindInfo.Cache.Persisted(false);
    this.ActivityList.Cache.Clear();
    this.ActivityList.View.Clear();
    this.ActivityCount.Cache.Clear();
    this.ActivityCount.View.Clear();
    this.ReminderList.Cache.Clear();
    this.ReminderList.View.Clear();
    this.ReminderListCurrent.View.Clear();
  }

  private PX.Objects.CR.CRActivity ReadActivity(object noteID)
  {
    if (noteID == null)
      return (PX.Objects.CR.CRActivity) null;
    return (PX.Objects.CR.CRActivity) PXSelectBase<PX.Objects.CR.CRActivity, PXSelect<PX.Objects.CR.CRActivity, Where<PX.Objects.CR.CRActivity.noteID, Equal<Required<PX.Objects.CR.CRActivity.noteID>>>>.Config>.Select((PXGraph) this, noteID);
  }

  private static Guid? ExtractActivityID(PXAdapter adapter)
  {
    if (adapter == null || adapter.Parameters == null || adapter.Parameters.Length < 1)
      return new Guid?();
    object parameter = adapter.Parameters[0];
    return parameter is string[] strArray && strArray.Length != 0 ? new Guid?(Guid.Parse(strArray[0])) : new Guid?(Guid.Parse(parameter.ToString()));
  }

  private TasksAndEventsReminder.EPActivityReminder ActivityReminder
  {
    get
    {
      string key = "ReminderList@" + PXAccess.GetUserID().ToString();
      return PXContext.GetSlot<TasksAndEventsReminder.EPActivityReminder>(key) ?? PXContext.SetSlot<TasksAndEventsReminder.EPActivityReminder>(key, TasksAndEventsReminder.EPActivityReminder.GetFromSlot(key, this) ?? TasksAndEventsReminder.EPActivityReminder.Empty);
    }
  }

  private void RefreshActivityReminder()
  {
    this.ActivityReminder.Expire();
    string key = "ReminderList@" + PXAccess.GetUserID().ToString();
    PXContext.SetSlot<TasksAndEventsReminder.EPActivityReminder>(key, (TasksAndEventsReminder.EPActivityReminder) null);
    PXDatabase.ResetSlot<TasksAndEventsReminder.EPActivityReminder>(key, typeof (PX.Objects.CR.CRActivity), typeof (EPView), typeof (EPAttendee), typeof (CRReminder), typeof (UserPreferences));
  }

  private class ActivityStatistics : 
    IPrefetchable<TasksAndEventsReminder.ActivityStatistics.Source>,
    IPXCompanyDependent
  {
    public static readonly TasksAndEventsReminder.ActivityStatistics Empty = new TasksAndEventsReminder.ActivityStatistics();
    private System.DateTime _day;
    private int _count;
    private int _unreadCount;
    private int _readCount;

    protected System.DateTime Day => this._day;

    public int Count => this._count;

    public int ReadCount => this._readCount;

    public int UnreadCount => this._unreadCount;

    public void Prefetch(
      TasksAndEventsReminder.ActivityStatistics.Source source)
    {
      this._day = System.DateTime.Today;
      this._count = this.RefreshTotal(source.SrceenID, source.ViewName, out this._readCount, out this._unreadCount);
    }

    private int RefreshTotal(string screenID, string viewName, out int read, out int unread)
    {
      read = -1;
      unread = -1;
      string screenId = PXContext.GetScreenID();
      using (new PXPreserveScope())
      {
        try
        {
          PXContext.SetScreenID(screenID);
          PXGraph graph = TasksAndEventsReminder.ActivityStatistics.CreateGraph(screenID);
          if (graph == null || graph.PrimaryView == null)
            return -1;
          if (viewName == null)
            viewName = graph.PrimaryView;
          PXView view = graph.Views[viewName];
          PXFilterRow[] filters = (PXFilterRow[]) null;
          PXView pxView;
          if (graph.Views.TryGetValue(viewName + "$FilterHeader", out pxView))
          {
            foreach (FilterHeader filterHeader in pxView.SelectMulti())
            {
              if (filterHeader.IsDefault.GetValueOrDefault())
              {
                int startRow = 0;
                int totalRows = 0;
                if (graph.ExecuteSelect(viewName + "$FilterRow", new object[1]
                {
                  (object) filterHeader.FilterID
                }, (object[]) null, (string[]) null, (bool[]) null, new PXFilterRow[1]
                {
                  new PXFilterRow(typeof (FilterRow.isUsed).Name, PXCondition.EQ, (object) true, (object) null)
                }, ref startRow, 0, ref totalRows) is IList list)
                {
                  if (list.Count > 0)
                  {
                    filters = new PXFilterRow[list.Count];
                    PXCache cach = graph.Caches[typeof (FilterRow)];
                    for (int index = 0; index < list.Count; ++index)
                    {
                      FilterRow data = (FilterRow) list[index];
                      PXFilterRow pxFilterRow = new PXFilterRow(data.DataField, (PXCondition) data.Condition.Value, cach.GetValueExt((object) data, "ValueSt"), cach.GetValueExt((object) data, "ValueSt2"));
                      if (data.OpenBrackets.HasValue)
                        pxFilterRow.OpenBrackets = data.OpenBrackets.Value;
                      if (data.CloseBrackets.HasValue)
                        pxFilterRow.CloseBrackets = data.CloseBrackets.Value;
                      pxFilterRow.OrOperator = data.Operator.GetValueOrDefault() == 1;
                      filters[index] = pxFilterRow;
                    }
                    break;
                  }
                  break;
                }
                break;
              }
            }
          }
          int startRow1 = 0;
          int totalRows1 = 0;
          read = -1;
          unread = -1;
          using (new PXFieldScope(view, new System.Type[2]
          {
            typeof (PX.Objects.CR.CRActivity.noteID),
            typeof (EPView.status)
          }))
          {
            foreach (object obj in view.Select((object[]) null, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, filters, ref startRow1, 101, ref totalRows1))
            {
              if (obj is PXResult pxResult)
              {
                EPView epView = pxResult.GetItem<EPView>();
                if (epView != null)
                {
                  if (epView.Status.HasValue && epView.Status.GetValueOrDefault() == 1)
                    ++read;
                  else
                    ++unread;
                }
              }
            }
          }
          if (read != -1 || unread != -1)
          {
            ++read;
            ++unread;
          }
          return totalRows1;
        }
        finally
        {
          PXContext.SetScreenID(screenId);
        }
      }
    }

    private static PXGraph CreateGraph(string screenID)
    {
      PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenID);
      if (mapNodeByScreenId == null)
        return (PXGraph) null;
      string graphType1 = mapNodeByScreenId.GraphType;
      System.Type type1 = PXBuildManager.GetType(graphType1, false);
      if ((object) type1 == null)
        type1 = System.Type.GetType(graphType1);
      System.Type t = type1;
      if (t == (System.Type) null)
        return (PXGraph) null;
      System.Type type2 = PXBuildManager.GetType(CustomizedTypeManager.GetCustomizedTypeFullName(t), false);
      if ((object) type2 == null)
        type2 = t;
      System.Type graphType2 = type2;
      using (new PXPreserveScope())
      {
        try
        {
          return PXGraph.CreateInstance(graphType2);
        }
        catch (TargetInvocationException ex)
        {
          throw PXException.ExtractInner((Exception) ex);
        }
      }
    }

    public static TasksAndEventsReminder.ActivityStatistics GetFromSlot(
      TasksAndEventsReminder.ActivityStatistics.Source source)
    {
      System.Type[] typeArray1 = source.Tables;
      if (typeArray1 == null)
        typeArray1 = new System.Type[4]
        {
          typeof (PX.Objects.CR.CRActivity),
          typeof (EPAttendee),
          typeof (EPView),
          typeof (UserPreferences)
        };
      System.Type[] typeArray2 = typeArray1;
      string key = $"ActivityStatistics@{source.SrceenID}{PXAccess.GetUserID().ToString()}";
      TasksAndEventsReminder.ActivityStatistics slot = PXDatabase.GetSlot<TasksAndEventsReminder.ActivityStatistics, TasksAndEventsReminder.ActivityStatistics.Source>(key, source, typeArray2);
      if (slot != null && slot.Day != System.DateTime.Today)
      {
        PXDatabase.ResetSlot<TasksAndEventsReminder.ActivityStatistics>(key, typeArray2);
        slot = PXDatabase.GetSlot<TasksAndEventsReminder.ActivityStatistics, TasksAndEventsReminder.ActivityStatistics.Source>(key, source, typeArray2);
      }
      return slot;
    }

    public class Source
    {
      public readonly string SrceenID;
      public readonly string ViewName;
      public readonly string ImageKey;
      public readonly string ImageSet;
      public readonly System.Type[] Tables;

      public Source(string screenID, string viewName, string imageKey)
        : this(screenID, viewName, imageKey, (System.Type[]) null)
      {
      }

      public Source(string screenID, string viewName, string imageKey, System.Type[] tables)
      {
        this.SrceenID = screenID;
        this.ViewName = viewName;
        this.ImageKey = imageKey;
        this.ImageSet = "main";
        this.Tables = tables;
      }
    }
  }

  private sealed class EPActivityReminder : 
    IPrefetchable<TasksAndEventsReminder>,
    IPXCompanyDependent,
    IExpires
  {
    private bool _dbChanged;
    public static readonly TasksAndEventsReminder.EPActivityReminder Empty = new TasksAndEventsReminder.EPActivityReminder();
    private PXResult<PX.Objects.CR.CRActivity>[] _reminderList = new PXResult<PX.Objects.CR.CRActivity>[0];

    public bool DBChanged
    {
      get
      {
        if (System.DateTime.UtcNow >= this.ExpirationTime)
          this._dbChanged = true;
        return this._dbChanged;
      }
      set => this._dbChanged = value;
    }

    public System.DateTime ExpirationTime { get; set; }

    public PXResult<PX.Objects.CR.CRActivity>[] ReminderList => this._reminderList;

    public EPActivityReminder()
    {
      this.ExpirationTime = System.DateTime.UtcNow;
      this.ExpirationTime = this.ExpirationTime.AddSeconds((double) (60 - this.ExpirationTime.Second));
    }

    public void Prefetch(TasksAndEventsReminder graph)
    {
      graph.SlotView.View.Clear();
      this._reminderList = graph.SlotView.Select().ToArray<PXResult<PX.Objects.CR.CRActivity>>();
    }

    public void Expire()
    {
      this.DBChanged = true;
      this.ExpirationTime = System.DateTime.MinValue;
    }

    public static TasksAndEventsReminder.EPActivityReminder GetFromSlot(
      string key,
      TasksAndEventsReminder graph)
    {
      TasksAndEventsReminder.EPActivityReminder slot = PXDatabase.GetSlot<TasksAndEventsReminder.EPActivityReminder, TasksAndEventsReminder>(key, graph, typeof (PX.Objects.CR.CRActivity), typeof (EPView), typeof (EPAttendee), typeof (CRReminder), typeof (UserPreferences));
      if (slot.DBChanged)
      {
        PXContext.SetSlot<TasksAndEventsReminder.EPActivityReminder>(key, (TasksAndEventsReminder.EPActivityReminder) null);
        PXDatabase.ResetSlot<TasksAndEventsReminder.EPActivityReminder>(key, typeof (PX.Objects.CR.CRActivity), typeof (EPView), typeof (EPAttendee), typeof (CRReminder), typeof (UserPreferences));
        slot = PXDatabase.GetSlot<TasksAndEventsReminder.EPActivityReminder, TasksAndEventsReminder>(key, graph, typeof (PX.Objects.CR.CRActivity), typeof (EPView), typeof (EPAttendee), typeof (CRReminder), typeof (UserPreferences));
      }
      return slot;
    }
  }

  public class WrappedWhere<TWhere> : IBqlWhere, IBqlUnary, IBqlCreator, IBqlVerifier where TWhere : IBqlWhere, new()
  {
    private readonly TWhere _realWhere;

    public WrappedWhere() => this._realWhere = new TWhere();

    public bool AppendExpression(
      ref SQLExpression exp,
      PXGraph graph,
      BqlCommandInfo info,
      BqlCommand.Selection selection)
    {
      return this._realWhere.AppendExpression(ref exp, graph, info, selection);
    }

    public virtual void Verify(
      PXCache cache,
      object item,
      List<object> pars,
      ref bool? result,
      ref object value)
    {
      this._realWhere.Verify(cache, item, pars, ref result, ref value);
    }
  }

  [PXHidden]
  [Serializable]
  public class DeferFilterInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXInt]
    [PXDefault(5)]
    [PXIntList(new int[] {5, 10, 15, 30, 60, 120, 240 /*0xF0*/, 720, 1440}, new string[] {"5 minutes", "10 minutes", "15 minutes", "30 minutes", "1 hour", "2 hours", "4 hours", "0.5 days", "1 day"})]
    public virtual int? Type { get; set; }

    public abstract class type : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TasksAndEventsReminder.DeferFilterInfo.type>
    {
    }
  }

  private delegate void UpdateRemindInfo(CRReminder info);
}
