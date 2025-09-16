// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.ActivityDetailsExt_Actions`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.SQLTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.Extensions;

public abstract class ActivityDetailsExt_Actions<TActivityDetailsExt, TGraph, TPrimaryEntity, TActivityEntity, TActivityEntity_NoteID> : 
  PXGraphExtension<TGraph>
  where TActivityDetailsExt : PXGraphExtension<TGraph>, IActivityDetailsExt
  where TGraph : PXGraph, new()
  where TPrimaryEntity : class, IBqlTable, new()
  where TActivityEntity : CRPMTimeActivity, new()
  where TActivityEntity_NoteID : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<IBqlGuid>>
{
  private TActivityDetailsExt _activityDetailsExt;
  public PXAction<TPrimaryEntity> NewTask;
  public PXAction<TPrimaryEntity> NewEvent;
  public PXAction<TPrimaryEntity> NewActivity;
  public PXAction<TPrimaryEntity> TogglePinActivity;
  public PXAction<TPrimaryEntity> RemoveActivity;

  [InjectDependency]
  protected IActivityService ActivityService { get; private set; }

  public TActivityDetailsExt ActivityDetailsExt
  {
    get
    {
      if ((object) this._activityDetailsExt == null)
        this._activityDetailsExt = this.Base.GetExtension<TActivityDetailsExt>();
      return this._activityDetailsExt;
    }
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.AddActivityQuickActionsAsMenu();
    this.AddPinFunctionality();
    PXUIFieldAttribute.SetVisible<CRActivityPinCacheExtension.isPinned>(this.ActivityDetailsExt.ActivitiesView.Cache, (object) null, this.IsPinActivityAvailable());
  }

  [PXUIField(DisplayName = "Create Task")]
  [PXButton(DisplayOnMainToolbar = false, PopupCommand = "RefreshActivities")]
  public virtual IEnumerable newTask(PXAdapter adapter)
  {
    this.ActivityDetailsExt.CreateNewActivityAndRedirect(0, (string) null);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Create Event")]
  [PXButton(DisplayOnMainToolbar = false, PopupCommand = "RefreshActivities")]
  public virtual IEnumerable newEvent(PXAdapter adapter)
  {
    this.ActivityDetailsExt.CreateNewActivityAndRedirect(1, (string) null);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Create Activity")]
  [PXButton(MenuAutoOpen = true, DisplayOnMainToolbar = false, PopupCommand = "RefreshActivities")]
  public virtual IEnumerable newActivity(PXAdapter adapter)
  {
    return this.NewActivityByType(adapter, adapter.Menu);
  }

  [PXSuppressActionValidation]
  [PXButton]
  public virtual IEnumerable NewActivityByType(PXAdapter adapter, string type)
  {
    this.ActivityDetailsExt.CreateNewActivityAndRedirect(2, type);
    return adapter.Get();
  }

  [PXUIField(Visible = false, DisplayName = "Pin/Unpin")]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable togglePinActivity(PXAdapter adapter)
  {
    PXCache cache = this.ActivityDetailsExt.ActivitiesView.Cache;
    if (!(cache?.Current is INotable current))
      return adapter.Get();
    int num = (cache.GetStateExt((object) current, "IsPinned") is PXFieldState stateExt ? stateExt.Value : (object) null) as string == CRActivityPinCacheExtension.isPinned.Pinned ? 1 : 0;
    string str = ScreenHelper.UnmaskScreenID(this.Base?.Accessinfo?.ScreenID);
    if (num != 0)
      this.Base.Caches[typeof (CRActivityPin)].Delete((object) new CRActivityPin()
      {
        NoteID = current.NoteID,
        CreatedByScreenID = str
      });
    else
      this.Base.Caches[typeof (CRActivityPin)].Insert((object) new CRActivityPin()
      {
        NoteID = current.NoteID
      });
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(Visible = false, DisplayName = "Remove")]
  protected void removeActivity()
  {
    PXCache cache = this.ActivityDetailsExt.ActivitiesView.Cache;
    CRActivity current = cache.Current as CRActivity;
    current.ParentNoteID = new Guid?();
    cache.Update((object) current);
  }

  protected virtual void _(Events.RowSelected<TPrimaryEntity> e)
  {
    TPrimaryEntity row = e.Row;
    if ((object) row == null)
      return;
    this.CorrectButtons((object) row, ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<TPrimaryEntity>>) e).Cache.GetStatus((object) row));
    ((PXAction) this.TogglePinActivity).SetVisible(this.IsPinActivityAvailable());
  }

  protected virtual void _(Events.RowSelected<TActivityEntity> e)
  {
    TActivityEntity row = e.Row;
    if ((object) row == null)
      return;
    int? classId = row.ClassID;
    int num = 0;
    if (!(classId.GetValueOrDefault() == num & classId.HasValue))
    {
      classId = row.ClassID;
      if (classId.GetValueOrDefault() != 1)
        return;
    }
    PXCacheEx.AdjustUI(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<TActivityEntity>>) e).Cache, (object) e.Row).For<CRActivityPinCacheExtension.isPinned>((Action<PXUIFieldAttribute>) (_ => _.Visible = this.IsPinActivityAvailable()));
  }

  protected virtual void IsPinnedFieldSelecting(
    PXView view,
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (view is PXPredefinedOrderedView<CRActivityPinCacheExtension.isPinned> predefinedOrderedView && predefinedOrderedView.IsCompare)
      return;
    Guid? selectingNoteId = sender.GetValue(e.Row, "NoteID") as Guid?;
    if (!selectingNoteId.HasValue)
      return;
    foreach (CRActivityPin crActivityPin in sender.Graph.Caches[typeof (CRActivityPin)].Dirty.Cast<CRActivityPin>().Where<CRActivityPin>((Func<CRActivityPin, bool>) (x =>
    {
      Guid? noteId = x.NoteID;
      Guid? nullable = selectingNoteId;
      if (noteId.HasValue != nullable.HasValue)
        return false;
      return !noteId.HasValue || noteId.GetValueOrDefault() == nullable.GetValueOrDefault();
    })))
      e.ReturnState = sender.Graph.Caches[typeof (CRActivityPin)].GetStatus((object) crActivityPin) == 3 ? (object) CRActivityPinCacheExtension.isPinned.Unpinned : (object) CRActivityPinCacheExtension.isPinned.Pinned;
  }

  protected virtual void IsPinnedCommandPreparing(
    System.Type itemType,
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    Query query = new Query();
    query.Field((SQLExpression) new SQLConst((object) true)).From((Table) new SimpleTable<CRActivityPin>((string) null)).Where(SQLExpressionExt.EQ(SQLExpressionExt.EQ((SQLExpression) new Column<CRActivityPin.noteID>((Table) null), (SQLExpression) new Column("NoteID", (Table) new SimpleTable(itemType.Name, (string) null), (PXDbType) 100)).And((SQLExpression) new Column<CRActivityPin.createdByScreenID>((Table) null)), (object) ScreenHelper.UnmaskScreenID(sender?.Graph?.Accessinfo?.ScreenID)));
    SQLSwitch sqlSwitch = new SQLSwitch().Case(new SubQuery(query).Exists(), (SQLExpression) new SQLConst((object) CRActivityPinCacheExtension.isPinned.Pinned)).Default((SQLExpression) new SQLConst((object) CRActivityPinCacheExtension.isPinned.Unpinned));
    e.Expr = (SQLExpression) sqlSwitch;
    e.BqlTable = itemType;
    ((CancelEventArgs) e).Cancel = true;
    e.DataType = (PXDbType) 2;
    e.DataLength = new int?(1);
    e.DataValue = e.Value;
  }

  public virtual void AddActivityQuickActionsAsMenu()
  {
    foreach (PX.Data.EP.ActivityService.IActivityType iactivityType in this.GetActivityTypesForMenu())
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ActivityDetailsExt_Actions<TActivityDetailsExt, TGraph, TPrimaryEntity, TActivityEntity, TActivityEntity_NoteID>.\u003C\u003Ec__DisplayClass25_0 cDisplayClass250 = new ActivityDetailsExt_Actions<TActivityDetailsExt, TGraph, TPrimaryEntity, TActivityEntity, TActivityEntity_NoteID>.\u003C\u003Ec__DisplayClass25_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass250.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass250.type = iactivityType;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((PXAction) this.NewActivity).AddMenuAction(this.AddAction((PXGraph) this.Base, "NewActivity" + cDisplayClass250.type.Type.TrimEnd(), PXMessages.LocalizeFormatNoPrefix("Create {0}", new object[1]
      {
        (object) cDisplayClass250.type.Description
      }), true, new PXButtonDelegate((object) cDisplayClass250, __methodptr(\u003CAddActivityQuickActionsAsMenu\u003Eb__0)), (PXEventSubscriberAttribute) new PXButtonAttribute()
      {
        CommitChanges = true,
        DisplayOnMainToolbar = false,
        PopupCommand = "RefreshActivities"
      }));
    }
  }

  public virtual IEnumerable<PX.Data.EP.ActivityService.IActivityType> GetActivityTypesForMenu()
  {
    IActivityService activityService = this.ActivityService;
    List<PX.Data.EP.ActivityService.IActivityType> iactivityTypeList;
    if (activityService == null)
    {
      iactivityTypeList = (List<PX.Data.EP.ActivityService.IActivityType>) null;
    }
    else
    {
      IEnumerable<PX.Data.EP.ActivityService.IActivityType> activityTypes = activityService.GetActivityTypes();
      iactivityTypeList = activityTypes != null ? activityTypes.ToList<PX.Data.EP.ActivityService.IActivityType>() : (List<PX.Data.EP.ActivityService.IActivityType>) null;
    }
    List<PX.Data.EP.ActivityService.IActivityType> source = iactivityTypeList;
    if (source == null || source.Count <= 0)
      return Enumerable.Empty<PX.Data.EP.ActivityService.IActivityType>();
    source.Where<PX.Data.EP.ActivityService.IActivityType>((Func<PX.Data.EP.ActivityService.IActivityType, bool>) (t => t.IsDefault.GetValueOrDefault())).Concat<PX.Data.EP.ActivityService.IActivityType>(source.Where<PX.Data.EP.ActivityService.IActivityType>((Func<PX.Data.EP.ActivityService.IActivityType, bool>) (t => !t.IsDefault.GetValueOrDefault())));
    return (IEnumerable<PX.Data.EP.ActivityService.IActivityType>) source.OrderBy<PX.Data.EP.ActivityService.IActivityType, bool?>((Func<PX.Data.EP.ActivityService.IActivityType, bool?>) (_ => _.IsDefault));
  }

  public virtual PXAction AddAction(
    PXGraph graph,
    string actionName,
    string displayName,
    bool visible,
    PXButtonDelegate handler,
    params PXEventSubscriberAttribute[] defaultAttributes)
  {
    PXUIFieldAttribute pxuiFieldAttribute = new PXUIFieldAttribute()
    {
      DisplayName = displayName,
      MapEnableRights = (PXCacheRights) 1
    };
    if (!visible)
      pxuiFieldAttribute.Visible = false;
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>()
    {
      (PXEventSubscriberAttribute) pxuiFieldAttribute
    };
    if (defaultAttributes != null)
      subscriberAttributeList.AddRange(((IEnumerable<PXEventSubscriberAttribute>) defaultAttributes).Where<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (attr => attr != null)));
    PXNamedAction<TPrimaryEntity> pxNamedAction1 = new PXNamedAction<TPrimaryEntity>(graph, actionName, handler, subscriberAttributeList.ToArray());
    graph.Actions[actionName] = (PXAction) pxNamedAction1;
    string str = actionName + "_Workflow";
    PXNamedAction<TPrimaryEntity> pxNamedAction2 = new PXNamedAction<TPrimaryEntity>(graph, str, handler, subscriberAttributeList.ToArray());
    graph.Actions[str] = (PXAction) pxNamedAction2;
    return (PXAction) pxNamedAction1;
  }

  public virtual void CorrectButtons(object row, PXEntryStatus status)
  {
    row = row ?? this.ActivityDetailsExt.ActivitiesView.Cache.Current;
    bool flag = (row == null ? 0 : (EnumerableExtensions.IsNotIn<PXEntryStatus>(status, (PXEntryStatus) 2, (PXEntryStatus) 4) ? 1 : 0)) != 0 && this.ActivityDetailsExt.ActivitiesView.Cache.AllowInsert;
    PXActionCollection actions = this.Base.Actions;
    actions["NewTask"].SetEnabled(flag);
    actions["NewEvent"].SetEnabled(flag);
    actions["NewMailActivity"].SetEnabled(flag);
    actions["NewActivity"].SetEnabled(flag);
    if (!(actions["NewActivity"].GetState(row) is PXButtonState state) || state.Menus == null)
      return;
    foreach (ButtonMenu menu in state.Menus)
    {
      actions[menu.Command].SetEnabled(flag);
      actions[menu.Command + "_Workflow"].SetEnabled(flag);
    }
  }

  public virtual void AddPinFunctionality()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ActivityDetailsExt_Actions<TActivityDetailsExt, TGraph, TPrimaryEntity, TActivityEntity, TActivityEntity_NoteID>.\u003C\u003Ec__DisplayClass29_0 cDisplayClass290 = new ActivityDetailsExt_Actions<TActivityDetailsExt, TGraph, TPrimaryEntity, TActivityEntity, TActivityEntity_NoteID>.\u003C\u003Ec__DisplayClass29_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass290.\u003C\u003E4__this = this;
    if (!this.IsPinActivityAvailable())
      return;
    this.Base.Views.Caches.Add(typeof (CRActivityPin));
    if ((object) this.ActivityDetailsExt.ActivitiesView.BqlDelegate == null)
      this.Base.Views[this.ActivityDetailsExt.ActivitiesView.Name] = (PXView) new PXPredefinedOrderedView<CRActivityPinCacheExtension.isPinned>((PXGraph) this.Base, this.ActivityDetailsExt.ActivitiesView.IsReadOnly, this.ActivityDetailsExt.ActivitiesView.BqlSelect);
    else
      this.Base.Views[this.ActivityDetailsExt.ActivitiesView.Name] = (PXView) new PXPredefinedOrderedView<CRActivityPinCacheExtension.isPinned>((PXGraph) this.Base, this.ActivityDetailsExt.ActivitiesView.IsReadOnly, this.ActivityDetailsExt.ActivitiesView.BqlSelect, this.ActivityDetailsExt.ActivitiesView.BqlDelegate);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass290.itemType = this.ActivityDetailsExt.ActivitiesView.Cache.GetItemType();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    this.Base.CommandPreparing.AddHandler(cDisplayClass290.itemType, "IsPinned", new PXCommandPreparing((object) cDisplayClass290, __methodptr(\u003CAddPinFunctionality\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    this.Base.FieldSelecting.AddHandler(cDisplayClass290.itemType, "IsPinned", new PXFieldSelecting((object) cDisplayClass290, __methodptr(\u003CAddPinFunctionality\u003Eb__1)));
    // ISSUE: reference to a compiler-generated method
    this.Base.OnAfterPersist += new Action<PXGraph>(cDisplayClass290.\u003CAddPinFunctionality\u003Eb__2);
  }

  public virtual bool IsPinActivityAvailable() => true;

  public static class ActivityTypes
  {
    public const string Appointment = "E";
    public const string Escalation = "ES";
    public const string Message = "M";
    public const string Note = "N";
    public const string PhoneCall = "P";
    public const string WorkItem = "W";
  }

  public static class ActionNames
  {
    public const string WorflowActionSuffix = "_Workflow";
    public const string NewTask_Workflow = "NewTask";
    public const string NewEvent_Workflow = "NewEvent";
    public const string NewMailActivity_Workflow = "newMailActivity";
    public const string NewActivity_Appointment_Workflow = "NewActivityE_Workflow";
    public const string NewActivity_Escalation_Workflow = "NewActivityES_Workflow";
    public const string NewActivity_Message_Workflow = "NewActivityM_Workflow";
    public const string NewActivity_Note_Workflow = "NewActivityN_Workflow";
    public const string NewActivity_Phonecall_Workflow = "NewActivityP_Workflow";
    public const string NewActivity_Workitem_Workflow = "NewActivityW_Workflow";
  }
}
