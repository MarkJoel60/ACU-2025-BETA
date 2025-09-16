// Decompiled with JetBrains decompiler
// Type: PX.Data.PXAction`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Automation;
using PX.Data.BusinessProcess;
using PX.Data.Descriptor.Action;
using PX.Data.WorkflowAPI;
using PX.PushNotifications;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <summary>An action of the graph.</summary>
/// <typeparam name="TNode"></typeparam>
public class PXAction<TNode> : PXAction where TNode : class, IBqlTable, new()
{
  protected Delegate _Handler;
  protected string _Name;
  protected PXFieldVerifying _ButtonTogglingEvents;
  private bool preventLoop;

  protected PXAction(PXGraph graph)
    : base(graph)
  {
  }

  public PXAction(PXGraph graph, string name)
    : base(graph)
  {
    this.SetHandler((Delegate) new PXButtonDelegate(this.Handler), name);
  }

  public PXAction(PXGraph graph, Delegate handler)
    : this(graph, handler, (string) null)
  {
  }

  public PXAction(PXGraph graph, Delegate handler, string name)
    : base(graph)
  {
    if ((object) handler == null)
      throw new PXArgumentException(nameof (handler), "The argument cannot be null.");
    if (string.IsNullOrEmpty(name))
      name = handler.Method.Name;
    this._Handler = handler;
    if (handler is System.Action)
    {
      System.Action action = (System.Action) handler;
      this._Handler = (Delegate) (adapter =>
      {
        action();
        return adapter.Get();
      });
    }
    this.SetHandler(handler, name);
  }

  protected void SetHandler(Delegate handler, string name)
  {
    this.AppendAttributes(name, handler.Method.GetCustomAttributes(typeof (PXEventSubscriberAttribute), false));
    this._Name = name;
  }

  public override bool GetEnabled()
  {
    bool enabled = true;
    if (!(this._Graph.Actions[this._Name] is PXAction<TNode> pxAction))
      pxAction = this;
    foreach (PXEventSubscriberAttribute attribute in pxAction._Attributes)
    {
      if (attribute is IPXInterfaceField)
        enabled &= ((IPXInterfaceField) attribute).Enabled;
    }
    return enabled;
  }

  public override void SetEnabled(bool isEnabled)
  {
    if (!(this._Graph.Actions[this._Name] is PXAction<TNode> pxAction1))
      pxAction1 = this;
    PXAction<TNode> pxAction2 = pxAction1;
    foreach (PXEventSubscriberAttribute attribute in pxAction2._Attributes)
    {
      if (attribute is IPXInterfaceField)
      {
        ((IPXInterfaceField) attribute).Enabled = isEnabled;
        pxAction2._Altered = true;
      }
    }
  }

  public override bool GetVisible()
  {
    bool visible = true;
    if (!(this._Graph.Actions[this._Name] is PXAction<TNode> pxAction))
      pxAction = this;
    foreach (PXEventSubscriberAttribute attribute in pxAction._Attributes)
    {
      if (attribute is IPXInterfaceField)
        visible &= ((IPXInterfaceField) attribute).Visible;
    }
    return visible;
  }

  public override void SetVisible(bool isVisible)
  {
    if (!(this._Graph.Actions[this._Name] is PXAction<TNode> pxAction))
      pxAction = this;
    foreach (PXEventSubscriberAttribute attribute in pxAction._Attributes)
    {
      if (attribute is IPXInterfaceField)
      {
        ((IPXInterfaceField) attribute).Visible = isVisible;
        this._DynamicVisibility = true;
      }
    }
  }

  public override void SetDynamicText(bool isDynamicText)
  {
    if (!(this._Graph.Actions[this._Name] is PXAction<TNode> pxAction))
      pxAction = this;
    foreach (PXEventSubscriberAttribute attribute in pxAction._Attributes)
    {
      if (attribute is PXButtonAttribute)
        ((PXButtonAttribute) attribute).DynamicText = isDynamicText;
    }
  }

  public override void SetCaption(string caption)
  {
    if (!(this._Graph.Actions[this._Name] is PXAction<TNode> pxAction))
      pxAction = this;
    foreach (PXEventSubscriberAttribute attribute in pxAction._Attributes)
    {
      if (attribute is IPXInterfaceField)
        ((IPXInterfaceField) attribute).DisplayName = PXMessages.Localize(caption, out string _);
    }
  }

  public override void SetTooltip(string tooltip)
  {
    if (!(this._Graph.Actions[this._Name] is PXAction<TNode> pxAction))
      pxAction = this;
    foreach (PXEventSubscriberAttribute attribute in pxAction._Attributes)
    {
      if (attribute is PXButtonAttribute)
        ((PXButtonAttribute) attribute).Tooltip = PXMessages.Localize(tooltip, out string _);
    }
  }

  public override void SetConfirmationMessage(string message)
  {
    if (!(this._Graph.Actions[this._Name] is PXAction<TNode> pxAction))
      pxAction = this;
    PXButtonAttribute pxButtonAttribute = pxAction.Attributes.OfType<PXButtonAttribute>().FirstOrDefault<PXButtonAttribute>();
    if (pxButtonAttribute == null)
      return;
    pxButtonAttribute.ConfirmationMessage = PXMessages.Localize(message, out string _);
  }

  [Obsolete("This field is obsolete and will be removed in the future versions. Use SetDisplayOnMainToolbar instead.")]
  public override void SetVisibleOnDataSource(bool isVisibleOnDataSource)
  {
    if (!(this._Graph.Actions[this._Name] is PXAction<TNode> pxAction))
      pxAction = this;
    foreach (PXEventSubscriberAttribute attribute in pxAction._Attributes)
    {
      if (attribute is PXButtonAttribute)
        ((PXButtonAttribute) attribute).VisibleOnDataSource = isVisibleOnDataSource;
    }
  }

  public override void SetDisplayOnMainToolbar(bool visible)
  {
    if (!(this._Graph.Actions[this._Name] is PXAction<TNode> pxAction))
      pxAction = this;
    foreach (PXEventSubscriberAttribute attribute in pxAction._Attributes)
    {
      if (attribute is PXButtonAttribute)
        ((PXButtonAttribute) attribute).DisplayOnMainToolbar = visible;
    }
  }

  public override void SetCategory(string category)
  {
    if (!(this._Graph.Actions[this._Name] is PXAction<TNode> pxAction))
      pxAction = this;
    foreach (PXEventSubscriberAttribute attribute in pxAction._Attributes)
    {
      if (attribute is PXButtonAttribute)
        ((PXButtonAttribute) attribute).Category = category;
    }
  }

  public override void SetIsLockedOnToolbar(bool locked)
  {
    if (!(this._Graph.Actions[this._Name] is PXAction<TNode> pxAction))
      pxAction = this;
    foreach (PXEventSubscriberAttribute attribute in pxAction._Attributes)
    {
      if (attribute is PXButtonAttribute)
        ((PXButtonAttribute) attribute).IsLockedOnToolbar = locked;
    }
  }

  public override void SetConnotation(ActionConnotation connotation)
  {
    if (!(this._Graph.Actions[this._Name] is PXAction<TNode> pxAction))
      pxAction = this;
    foreach (PXEventSubscriberAttribute attribute in pxAction._Attributes)
    {
      if (attribute is PXButtonAttribute)
        ((PXButtonAttribute) attribute).Connotation = connotation;
    }
  }

  public override void SetCommitChanges(bool commitChanges)
  {
    if (!(this._Graph.Actions[this._Name] is PXAction<TNode> pxAction))
      pxAction = this;
    foreach (PXEventSubscriberAttribute attribute in pxAction._Attributes)
    {
      if (attribute is PXButtonAttribute)
        ((PXButtonAttribute) attribute).CommitChanges = commitChanges;
    }
  }

  public override void SetMapEnableRights(PXCacheRights mapping)
  {
    if (!(this._Graph.Actions[this._Name] is PXAction<TNode> pxAction))
      pxAction = this;
    foreach (PXEventSubscriberAttribute attribute in pxAction._Attributes)
    {
      if (attribute is IPXInterfaceField)
        ((IPXInterfaceField) attribute).MapEnableRights = mapping;
    }
  }

  public override void SetMapViewRights(PXCacheRights mapping)
  {
    if (!(this._Graph.Actions[this._Name] is PXAction<TNode> pxAction))
      pxAction = this;
    foreach (PXEventSubscriberAttribute attribute in pxAction._Attributes)
    {
      if (attribute is IPXInterfaceField)
        ((IPXInterfaceField) attribute).MapViewRights = mapping;
    }
  }

  protected void AppendAttributes(string name, params object[] attributes)
  {
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>();
    if (this._Attributes != null)
      subscriberAttributeList.AddRange((IEnumerable<PXEventSubscriberAttribute>) this._Attributes);
    PXCache cach = this._Graph.Caches[typeof (TNode)];
    foreach (object attribute in attributes)
    {
      if (attribute is PXEventSubscriberAttribute subscriberAttribute1)
      {
        subscriberAttribute1.BqlTable = typeof (TNode);
        subscriberAttribute1.FieldName = name;
        PXEventSubscriberAttribute subscriberAttribute = subscriberAttribute1.Clone(PXAttributeLevel.Item);
        List<IPXFieldSelectingSubscriber> sel = new List<IPXFieldSelectingSubscriber>();
        subscriberAttribute.GetSubscriber<IPXFieldSelectingSubscriber>(sel);
        if (sel.Count > 0)
          this._StateSelectingEvents = this._StateSelectingEvents + (PXFieldSelecting) ((sender, e) =>
          {
            for (int index = 0; index < sel.Count; ++index)
              sel[index].FieldSelecting(sender, e);
          });
        List<IPXFieldVerifyingSubscriber> ver = new List<IPXFieldVerifyingSubscriber>();
        subscriberAttribute.GetSubscriber<IPXFieldVerifyingSubscriber>(ver);
        if (ver.Count > 0)
          this._ButtonTogglingEvents += (PXFieldVerifying) ((sender, e) =>
          {
            for (int index = 0; index < ver.Count; ++index)
              ver[index].FieldVerifying(sender, e);
          });
        subscriberAttribute.InvokeCacheAttached(cach);
        if (subscriberAttribute is PXUIFieldAttribute)
          ((PXUIFieldAttribute) subscriberAttribute).DisplayName = PXMessages.LocalizeNoPrefix(((PXUIFieldAttribute) subscriberAttribute).DisplayName);
        if (subscriberAttribute is PXButtonAttribute && ((PXButtonAttribute) subscriberAttribute).MenuAutoOpen)
          this.MenuAutoOpen = true;
        subscriberAttributeList.Add(subscriberAttribute);
      }
    }
    this._Attributes = subscriberAttributeList.ToArray();
  }

  public override void AddMenuAction(PXAction action, string prevAction, bool insertAfter)
  {
    PXButtonState astate = action.GetState((object) null) as PXButtonState;
    if (astate == null)
      return;
    if (this._Graph.Actions[this._Name].GetState((object) null) is PXButtonState state && state.Menus != null)
    {
      List<ButtonMenu> source = new List<ButtonMenu>((IEnumerable<ButtonMenu>) state.Menus);
      if (source.Any<ButtonMenu>((Func<ButtonMenu, bool>) (m => string.Equals(m.Command, astate.Name, StringComparison.OrdinalIgnoreCase))))
        return;
      ButtonMenu buttonMenu = new ButtonMenu(astate.Name, astate.DisplayName, astate.ImageUrl, astate.DisabledImageUrl);
      int index = source.FindIndex((Predicate<ButtonMenu>) (m => string.Equals(m.Command, prevAction, StringComparison.OrdinalIgnoreCase)));
      if (index >= 0)
        source.Insert(insertAfter ? index + 1 : index, buttonMenu);
      else
        source.Add(buttonMenu);
      this._Graph.Actions[this._Name].SetMenu(source.ToArray());
    }
    else
      this._Graph.Actions[this._Name].SetMenu(new ButtonMenu[1]
      {
        new ButtonMenu(astate.Name, astate.DisplayName, astate.ImageUrl, astate.DisabledImageUrl)
      });
    action.WorkflowHiddenOnMainToolbar = true;
  }

  internal override void RemoveMenuAction(PXAction action)
  {
    PXButtonState astate = action.GetState((object) null) as PXButtonState;
    if (astate == null || !(this._Graph.Actions[this._Name].GetState((object) null) is PXButtonState state) || state.Menus == null)
      return;
    List<ButtonMenu> source = new List<ButtonMenu>((IEnumerable<ButtonMenu>) state.Menus);
    ButtonMenu buttonMenu = source.FirstOrDefault<ButtonMenu>((Func<ButtonMenu, bool>) (m => string.Equals(m.Command, astate.Name, StringComparison.OrdinalIgnoreCase)));
    if (buttonMenu == null)
      return;
    source.Remove(buttonMenu);
    this._Graph.Actions[this._Name].SetMenu(source.ToArray());
  }

  public override void SetMenu(ButtonMenu[] menus)
  {
    this._Menus = menus;
    this._MenuAttributes = (PXEventSubscriberAttribute[]) null;
    if (this._Menus == null)
      return;
    foreach (PXEventSubscriberAttribute attribute in this._Attributes)
    {
      if (attribute is IPXInterfaceField)
      {
        this._MenuAttributes = new PXEventSubscriberAttribute[this._Menus.Length];
        for (int index = 0; index < this._Menus.Length; ++index)
        {
          PXEventSubscriberAttribute subscriberAttribute1 = attribute.Clone(PXAttributeLevel.Type);
          subscriberAttribute1.FieldName = $"{this._Menus[index].Command}@{attribute.FieldName}";
          PXEventSubscriberAttribute subscriberAttribute2 = subscriberAttribute1.Clone(PXAttributeLevel.Item);
          ((IPXInterfaceField) subscriberAttribute2).DisplayName = this._Menus[index].Text;
          if (subscriberAttribute2 is PXUIFieldAttribute pxuiFieldAttribute)
            pxuiFieldAttribute.ChangeNeutralDisplayName(this._Menus[index].Text);
          subscriberAttribute2.InvokeCacheAttached(this._Graph.Caches[typeof (TNode)]);
          this._Menus[index].Text = ((IPXInterfaceField) subscriberAttribute2).DisplayName;
          if (!((IPXInterfaceField) subscriberAttribute2).Visible)
          {
            this._Menus[index].Visible = false;
            this._Menus[index].Enabled = false;
          }
          ((IPXInterfaceField) subscriberAttribute2).Enabled = this._Menus[index].Enabled;
          this._Menus[index].Enabled = ((IPXInterfaceField) subscriberAttribute2).Enabled;
          this._MenuAttributes[index] = subscriberAttribute2;
        }
      }
    }
  }

  /// <exclude />
  public event PXFieldVerifying ButtonTogglingEvents
  {
    add => this._ButtonTogglingEvents = value + this._ButtonTogglingEvents;
    remove => this._ButtonTogglingEvents -= value;
  }

  protected bool OnButtonToggling(PXAdapter adapter)
  {
    if (this._ButtonTogglingEvents == null)
      return false;
    PXFieldVerifyingEventArgs args = new PXFieldVerifyingEventArgs((object) adapter, (object) !this._Pressed, true);
    this._ButtonTogglingEvents(this._Graph.Caches[typeof (TNode)], args);
    this._Pressed = (args.NewValue == null ? 0 : ((bool) args.NewValue ? 1 : 0)) == 0;
    return args.Cancel;
  }

  public bool RaiseButtonToggling(PXAdapter adapter) => this.OnButtonToggling(adapter);

  protected bool OnStateSelecting(object row, ref object returnValue, out bool viewRights)
  {
    viewRights = true;
    PXCache cach = this._Graph.Caches[typeof (TNode)];
    foreach (PXEventSubscriberAttribute attribute in this._Attributes)
    {
      if (attribute is IPXInterfaceField)
      {
        viewRights = ((IPXInterfaceField) attribute).ViewRights;
        if (!this._Altered)
        {
          switch (((IPXInterfaceField) attribute).MapEnableRights)
          {
            case PXCacheRights.Denied:
              ((IPXInterfaceField) attribute).Enabled = !cach.AllowSelect;
              continue;
            case PXCacheRights.Select:
              ((IPXInterfaceField) attribute).Enabled = cach.AllowSelect;
              continue;
            case PXCacheRights.Update:
              ((IPXInterfaceField) attribute).Enabled = cach.AllowUpdate;
              continue;
            case PXCacheRights.Insert:
              ((IPXInterfaceField) attribute).Enabled = cach.AllowInsert;
              continue;
            case PXCacheRights.Delete:
              ((IPXInterfaceField) attribute).Enabled = cach.AllowDelete;
              continue;
            default:
              continue;
          }
        }
      }
    }
    if (this._StateSelectingEvents == null)
      return false;
    PXFieldSelectingEventArgs args = new PXFieldSelectingEventArgs(row, returnValue, true, true);
    this._StateSelectingEvents(this._Graph.Caches[typeof (TNode)], args);
    returnValue = args.ReturnState;
    return args.Cancel;
  }

  public bool RaiseStateSelecting(object row, ref object returnValue)
  {
    return this.OnStateSelecting(row, ref returnValue, out bool _);
  }

  public override object GetState(object row)
  {
    object handler = (object) this._Handler;
    bool viewRights;
    this.OnStateSelecting(row, ref handler, out viewRights);
    ButtonMenu[] buttonMenuArray;
    if (!this.preventLoop && this._Menus != null && this._Menus.Length != 0)
    {
      this.preventLoop = true;
      buttonMenuArray = (ButtonMenu[]) this._Menus.Clone();
      for (int index = 0; index < buttonMenuArray.Length; ++index)
      {
        if (buttonMenuArray[index].Command != null)
        {
          PXAction action = this._Graph.Actions[buttonMenuArray[index].Command];
          if (action != null && action.GetState(row) is PXButtonState state)
          {
            buttonMenuArray[index] = new ButtonMenu(buttonMenuArray[index].Command, buttonMenuArray[index].Icon)
            {
              Text = state.DisplayName,
              SyncText = true,
              Enabled = this._Menus[index].Enabled,
              ActionEnabled = state.Enabled
            };
            if (string.IsNullOrEmpty(buttonMenuArray[index].Icon))
            {
              buttonMenuArray[index].Icon = state.ImageUrl;
              if (!string.IsNullOrEmpty(state.DisabledImageUrl))
                buttonMenuArray[index].IconDisabled = state.DisabledImageUrl;
            }
            buttonMenuArray[index].Visible = state.ViewRights && this._Menus[index].Visible && !action.AutomationHidden;
            buttonMenuArray[index].OnClosingPopup = state.OnClosingPopup;
          }
        }
      }
      this.preventLoop = false;
    }
    else
      buttonMenuArray = this._Menus;
    PXButtonState instance = PXButtonState.CreateInstance(handler, (string) null, (string) null, (string) null, (string) null, (string) null, new bool?(this._Pressed), PXConfirmationType.Unspecified, (string) null, new char?(), new bool?(), new bool?(), buttonMenuArray, new bool?(), new bool?(), new bool?(), (string) null, (string) null, (PXShortCutAttribute) null, typeof (TNode));
    if (this.AutomationHidden || this.MenuAutoOpen && (buttonMenuArray == null || ((IEnumerable<ButtonMenu>) buttonMenuArray).All<ButtonMenu>((Func<ButtonMenu, bool>) (_ => !_.Visible))))
      instance.Visible = false;
    if (this.AutomationDisabled)
      instance.Enabled = false;
    PXSpecialButtonType? specialType = this.GetSpecialType();
    bool? isArchiveContext1 = this.Graph.IsArchiveContext;
    bool flag1 = true;
    if (isArchiveContext1.GetValueOrDefault() == flag1 & isArchiveContext1.HasValue)
    {
      bool? archiveDisabling = this.GetIgnoresArchiveDisabling();
      bool flag2 = true;
      if (!(archiveDisabling.GetValueOrDefault() == flag2 & archiveDisabling.HasValue))
        instance.Enabled = false;
      if (EnumerableExtensions.IsIn<PXSpecialButtonType?>(specialType, new PXSpecialButtonType?(PXSpecialButtonType.Save), new PXSpecialButtonType?(PXSpecialButtonType.SaveNotClose), new PXSpecialButtonType?(PXSpecialButtonType.Delete), new PXSpecialButtonType?(PXSpecialButtonType.Archive)) || EnumerableExtensions.IsIn<PXSpecialButtonType?>(specialType, new PXSpecialButtonType?(PXSpecialButtonType.First), new PXSpecialButtonType?(PXSpecialButtonType.Prev), new PXSpecialButtonType?(PXSpecialButtonType.Next), new PXSpecialButtonType?(PXSpecialButtonType.Last)))
      {
        instance.Visible = false;
        instance.Enabled = false;
      }
    }
    else
    {
      bool? isArchiveContext2 = this.Graph.IsArchiveContext;
      bool flag3 = false;
      if (isArchiveContext2.GetValueOrDefault() == flag3 & isArchiveContext2.HasValue)
      {
        PXSpecialButtonType? nullable = specialType;
        PXSpecialButtonType specialButtonType = PXSpecialButtonType.Extract;
        if (nullable.GetValueOrDefault() == specialButtonType & nullable.HasValue)
        {
          instance.Visible = false;
          instance.Enabled = false;
        }
      }
      else if (EnumerableExtensions.IsIn<PXSpecialButtonType?>(specialType, new PXSpecialButtonType?(PXSpecialButtonType.Archive), new PXSpecialButtonType?(PXSpecialButtonType.Extract)))
      {
        instance.Visible = false;
        instance.Enabled = false;
      }
    }
    instance.ViewRights = viewRights;
    if (this._DynamicVisibility)
      instance.DynamicVisibility = true;
    return (object) instance;
  }

  public override void Press() => this.PressImpl();

  public void PressImpl(bool internalCall = true, bool externalCall = false)
  {
    BqlCommand bqlCommand;
    if (!string.IsNullOrEmpty(this._Graph.PrimaryView))
    {
      PXView view = this._Graph.Views[this._Graph.PrimaryView];
      bqlCommand = view.GetItemType() == typeof (TNode) || view.GetItemType().IsAssignableFrom(typeof (TNode)) ? view.BqlSelect : (BqlCommand) new Select<TNode>();
    }
    else
      bqlCommand = (BqlCommand) new Select<TNode>();
    PXGraph graph = this._Graph;
    BqlCommand command = bqlCommand;
    List<object> records;
    if (this._Graph.Caches[typeof (TNode)].Current == null)
    {
      records = new List<object>();
    }
    else
    {
      records = new List<object>();
      records.Add(this._Graph.Caches[typeof (TNode)].Current);
    }
    PXAdapter adapter = new PXAdapter((PXView) new PXView.Dummy(graph, command, records));
    adapter.InternalCall = internalCall;
    adapter.ExternalCall = externalCall;
    if (!PXCustomizedActionScope.IsScoped(this._Name))
    {
      foreach (object obj in this._Graph.Actions[this._Name].Press(adapter))
        ;
    }
    else
    {
      foreach (object obj in this.Press(adapter))
        ;
    }
  }

  public override IEnumerable Press(PXAdapter adapter)
  {
    PXAction<TNode> caller = this;
    foreach (PXEventSubscriberAttribute attribute in caller.Attributes)
    {
      if (attribute is PXActionRestrictionAttribute restrictionAttribute1)
        restrictionAttribute1.Verify(adapter);
      else if (attribute is PXAggregateAttribute aggregateAttribute)
      {
        foreach (PXActionRestrictionAttribute restrictionAttribute in aggregateAttribute.GetAttributes().OfType<PXActionRestrictionAttribute>())
          restrictionAttribute.Verify(adapter);
      }
    }
    if ((adapter.ExternalCall || adapter.ForceButtonEnabledCheck) && caller.GetState((object) null) is PXButtonState state)
    {
      if (adapter.Menu == null)
      {
        if (!state.Enabled)
          throw new PXActionDisabledException(state.DisplayName ?? state.Name);
      }
      else if (state.Menus != null)
      {
        bool flag = state.Enabled;
        for (int index = 0; index < state.Menus.Length; ++index)
        {
          if (PXLocalesProvider.CollationComparer.Equals(state.Menus[index].Command, adapter.Menu))
          {
            if (!state.Menus[index].GetEnabled())
              throw new PXActionDisabledException(state.Menus[index].Text ?? state.Menus[index].Command);
            flag = true;
            break;
          }
        }
        if (!flag)
          throw new PXActionDisabledException(adapter.Menu);
      }
    }
    PXWorkflowService workflowService1 = caller.Graph.WorkflowService;
    bool flag1 = workflowService1 != null && workflowService1.IsWorkflowExists(caller._Graph);
    int num1;
    if (adapter.MassProcess)
    {
      PXWorkflowService workflowService2 = caller.Graph.WorkflowService;
      num1 = workflowService2 != null ? (workflowService2.IsWorkflowDefinitionDefined(caller._Graph) ? 1 : 0) : 0;
    }
    else
      num1 = 0;
    bool flag2 = num1 != 0;
    if (!caller.OnButtonToggling(adapter))
    {
      bool flag3 = false;
      Exception exception = (Exception) null;
      List<object> ret = new List<object>();
      bool flag4 = false;
      PXActionInterruptException actionInterruptException = (PXActionInterruptException) null;
      Dictionary<KeyWithAlias, object> formRow = new Dictionary<KeyWithAlias, object>();
      try
      {
        PXAction pxAction = (PXAction) null;
        if (adapter.Menu != null)
          pxAction = caller._Graph.Actions[adapter.Menu];
        if (pxAction != null)
        {
          flag4 = true;
          adapter.Menu = (string) null;
          foreach (object obj in pxAction.Press(adapter))
            ret.Add(obj);
        }
        else
        {
          caller._Graph.ExceptionRollback.OnActionStart();
          int valueOrDefault = PXContext.GetSlot<int?>("PXParallelProcessingOffset").GetValueOrDefault();
          PXProcessingInfo processingInfo = PXProcessing.GetProcessingInfo();
          foreach (object row1 in caller.RunHandler(adapter))
          {
            object row = PXResult.UnwrapFirst(row1);
            PXCache primaryCache = caller._Graph.GetPrimaryCache();
            int num2 = primaryCache.GetItemType().IsInstanceOfType(row) ? 1 : 0;
            if (num2 != 0)
            {
              if (EnumerableExtensions.IsIn<PXEntryStatus>(primaryCache.GetStatus(row), PXEntryStatus.Inserted, PXEntryStatus.InsertedDeleted, PXEntryStatus.Updated, PXEntryStatus.Deleted))
              {
                row = primaryCache.Locate(row);
              }
              else
              {
                object persistedRecord = PXTimeStampScope.GetPersistedRecord(adapter.View.Cache, row);
                if (persistedRecord != null && persistedRecord != row && adapter.View.Cache.GetItemType().IsAssignableFrom(persistedRecord.GetType()))
                  row = persistedRecord;
              }
            }
            adapter.View.Cache.Current = row;
            string str;
            if ((num2 & (flag1 ? 1 : 0) | (flag2 ? 1 : 0)) != 0 && (adapter.View is PXView.Dummy || caller._Graph.ViewNames.TryGetValue(adapter.View, out str) && str == caller._Graph.PrimaryView))
            {
              bool flag5 = false;
              if (adapter.MassProcess && processingInfo != null && processingInfo.Total > valueOrDefault + ret.Count)
              {
                PXProcessingMessage processingMessage = !PXContext.GetSlot<bool>("Workflow.NonBatchModeProcessing") ? processingInfo.Messages.Get<TNode>(ret.Count) : PXProcessing.GetItemMessage<TNode>();
                if (processingMessage != null && (processingMessage.ErrorLevel == PXErrorLevel.RowError || processingMessage.ErrorLevel == PXErrorLevel.Error))
                  flag5 = true;
              }
              Dictionary<IDictionary, bool> slot = PXContext.GetSlot<Dictionary<IDictionary, bool>>("Workflow.MassProcessingWorkflowObject");
              if (!flag5 && slot != null)
              {
                Dictionary<string, object> currentKeys = adapter.View.Cache.Keys.ToDictionary<string, string, object>((Func<string, string>) (cacheKey => cacheKey), (Func<string, object>) (cacheKey => adapter.View.Cache.GetValue(row, cacheKey)));
                if (slot.Any<KeyValuePair<IDictionary, bool>>((Func<KeyValuePair<IDictionary, bool>, bool>) (pair => DictionaryAreEquals(pair.Key, (IDictionary<string, object>) currentKeys) && pair.Value)))
                  flag5 = true;
              }
              if (!flag1)
              {
                if (flag2)
                {
                  PXWorkflowService workflowService3 = caller.Graph.WorkflowService;
                  if ((workflowService3 != null ? (workflowService3.IsWorkflowExists(caller._Graph, row) ? 1 : 0) : 0) == 0)
                    goto label_67;
                }
                else
                  goto label_67;
              }
              if (!flag5)
                caller.Graph.WorkflowService.ApplyTransition(caller._Graph, row, caller._Name);
label_67:
              caller.Graph.WorkflowService.ApplyWorkflowState(caller._Graph, row);
            }
            ret.Add(row1);
          }
          System.Action<PXAction, PXAdapter, Exception> afterRunHandler = caller.AfterRunHandler;
          if (afterRunHandler != null)
            afterRunHandler((PXAction) caller, adapter, (Exception) null);
        }
      }
      catch (PXActionInterruptException ex)
      {
        actionInterruptException = ex;
        PXAction.IsInterruptedByActionLogic = true;
      }
      catch (Exception ex)
      {
        exception = ex;
      }
      finally
      {
        if (!flag4)
          caller._Graph.ExceptionRollback.OnActionEnd();
        if (!adapter.InternalCall)
          caller.Graph.WorkflowService?.ClearActionData();
        if (exception != null)
        {
          if (!(exception is PXBaseRedirectException))
            PXWorkflowService.SaveAfterAction[caller._Graph] = false;
          if (flag3)
            caller._Graph.Actions.PressSave((PXAction) caller);
          else if (PXWorkflowService.SaveAfterAction[caller._Graph])
          {
            formRow = caller._Graph.BusinessProcessEventProcessor.GetFormFieldsAsRow(caller._Graph);
            PXWorkflowService.SaveAfterAction[caller._Graph] = false;
            caller.Graph.WorkflowService.ClearFormData(caller._Graph);
            caller.Graph.WorkflowService.PressSave((PXAction) caller);
            if (!caller._Graph.IsDirty)
              caller.ApplyWorkflowSteps((IEnumerable<object>) ret);
          }
          if (!flag4 && !flag3 && !(exception is PXBaseRedirectException) && !(exception is PXOuterException))
            caller._Graph.ExceptionRollback.ProcessActionException<Exception>(exception, caller._Name);
        }
        else if (flag3)
          caller._Graph.Actions.PressSave((PXAction) caller);
        else if (PXWorkflowService.SaveAfterAction[caller._Graph])
        {
          formRow = caller._Graph.BusinessProcessEventProcessor.GetFormFieldsAsRow(caller._Graph);
          PXWorkflowService.SaveAfterAction[caller._Graph] = false;
          caller.Graph.WorkflowService.ClearFormData(caller._Graph);
          caller.Graph.WorkflowService.PressSave((PXAction) caller);
          if (!caller._Graph.IsDirty)
            caller.ApplyWorkflowSteps((IEnumerable<object>) ret);
        }
      }
      foreach (object obj in ret)
        yield return obj;
      switch (exception)
      {
        case null:
          if (actionInterruptException != null)
          {
            caller._Graph.ExceptionRollback.ProcessActionException<PXActionInterruptException>(actionInterruptException, caller._Name);
          }
          else
          {
            PXAction.IsInterruptedByActionLogic = false;
            if (!adapter.InternalCall && caller.Graph.WorkflowService != null)
              caller.ApplyWorkflowSteps((IEnumerable<object>) ret);
            caller._Graph.BusinessProcessEventProcessor?.CollectAllFieldValuesAndTriggerActionEvents(caller._Graph, caller._Name, (IDictionary<KeyWithAlias, object>) formRow, ret);
          }
          exception = (Exception) null;
          ret = (List<object>) null;
          actionInterruptException = (PXActionInterruptException) null;
          formRow = (Dictionary<KeyWithAlias, object>) null;
          goto label_112;
        case PXOperationCompletedException _:
        case PXBaseRedirectException _ when !(exception is PXBaseUnhandledRedirectException):
          IBusinessProcessEventProcessor processEventProcessor = caller._Graph.BusinessProcessEventProcessor;
          if (processEventProcessor != null)
          {
            processEventProcessor.CollectAllFieldValuesAndTriggerActionEvents(caller._Graph, caller._Name, (IDictionary<KeyWithAlias, object>) formRow);
            break;
          }
          break;
      }
      throw PXException.PreserveStack(exception);
    }
label_112:
    if (caller.ClearAnswerAfterPress && !adapter.InternalCall)
      adapter.View.ClearDialog();

    static bool DictionaryAreEquals(IDictionary dic1, IDictionary<string, object> dic2)
    {
      return dic1.Keys.Count == dic2.Keys.Count && dic1.Keys.OfType<string>().All<string>((Func<string, bool>) (k => dic2.ContainsKey(k) && object.Equals(dic2[k], dic1[(object) k])));
    }
  }

  private void ApplyWorkflowSteps(IEnumerable<object> rows)
  {
    foreach (object row in rows)
      this._Graph.WorkflowService.ApplyWorkflowState(this._Graph, PXResult.UnwrapFirst(row));
  }

  private IEnumerable RunHandler(PXAdapter adapter)
  {
    System.Action<PXAction, PXAdapter> beforeRunHandler = this.BeforeRunHandler;
    if (beforeRunHandler != null)
      beforeRunHandler((PXAction) this, adapter);
    if ((object) this._Handler != null)
    {
      if (this._Handler is PXButtonDelegate)
        return ((PXButtonDelegate) this._Handler)(adapter);
      if (this._Handler.Method.ReturnType == typeof (IEnumerable))
      {
        ParameterInfo[] parameters = this._Handler.Method.GetParameters();
        object[] objArray = new object[parameters.Length];
        objArray[0] = (object) adapter;
        if (adapter.Arguments.Count > 0)
        {
          for (int index = 1; index < parameters.Length; ++index)
          {
            object obj;
            if (adapter.Arguments.TryGetValue(parameters[index].Name, out obj) && obj != null)
              objArray[index] = obj;
          }
        }
        try
        {
          return (IEnumerable) this._Handler.DynamicInvoke(objArray);
        }
        catch (TargetInvocationException ex)
        {
          throw PXException.ExtractInner((Exception) ex);
        }
      }
    }
    try
    {
      return this.Handler(adapter);
    }
    catch (Exception ex)
    {
      System.Action<PXAction, PXAdapter, Exception> afterRunHandler = this.AfterRunHandler;
      if (afterRunHandler != null)
        afterRunHandler((PXAction) this, adapter, ex);
      throw;
    }
  }

  protected virtual IEnumerable Handler(PXAdapter adapter) => (IEnumerable) new object[0];

  public override string[] GetParameterNames()
  {
    if ((object) this._Handler != null && !(this._Handler is PXButtonDelegate))
    {
      ParameterInfo[] parameters = this._Handler.Method.GetParameters();
      if (parameters.Length != 0)
      {
        string[] parameterNames = new string[parameters.Length - 1];
        for (int index = 1; index < parameters.Length; ++index)
          parameterNames[index - 1] = parameters[index].Name;
        return parameterNames;
      }
    }
    return new string[0];
  }

  public override PXFieldState GetParameterExt(string name)
  {
    if ((object) this._Handler != null && !(this._Handler is PXButtonDelegate))
    {
      ParameterInfo[] parameters = this._Handler.Method.GetParameters();
      for (int index1 = 1; index1 < parameters.Length; ++index1)
      {
        if (string.Equals(parameters[index1].Name, name))
        {
          List<IPXFieldSelectingSubscriber> subscribers = new List<IPXFieldSelectingSubscriber>();
          foreach (PXEventSubscriberAttribute customAttribute in parameters[index1].GetCustomAttributes(typeof (PXEventSubscriberAttribute), false))
            customAttribute.GetSubscriber<IPXFieldSelectingSubscriber>(subscribers);
          if (subscribers.Count > 0)
          {
            PXFieldSelectingEventArgs e = new PXFieldSelectingEventArgs((object) null, (object) null, true, false);
            for (int index2 = 0; index2 < subscribers.Count; ++index2)
              subscribers[index2].FieldSelecting(this._Graph.Caches[typeof (TNode)], e);
            return e.ReturnState as PXFieldState;
          }
        }
      }
    }
    return (PXFieldState) null;
  }

  public override object SetParameterExt(string name, object value)
  {
    if ((object) this._Handler != null && !(this._Handler is PXButtonDelegate))
    {
      ParameterInfo[] parameters = this._Handler.Method.GetParameters();
      for (int index1 = 1; index1 < parameters.Length; ++index1)
      {
        if (string.Equals(parameters[index1].Name, name))
        {
          List<IPXFieldUpdatingSubscriber> subscribers = new List<IPXFieldUpdatingSubscriber>();
          foreach (PXEventSubscriberAttribute customAttribute in parameters[index1].GetCustomAttributes(typeof (PXEventSubscriberAttribute), false))
            customAttribute.GetSubscriber<IPXFieldUpdatingSubscriber>(subscribers);
          if (subscribers.Count > 0)
          {
            PXFieldUpdatingEventArgs e = new PXFieldUpdatingEventArgs((object) null, value);
            for (int index2 = 0; index2 < subscribers.Count; ++index2)
              subscribers[index2].FieldUpdating(this._Graph.Caches[typeof (TNode)], e);
            return e.NewValue;
          }
        }
      }
    }
    return value;
  }

  public IEnumerable this[PXSelectBase indexer, params object[] pars]
  {
    get => this.Press(new PXAdapter(indexer, pars));
  }

  protected virtual void Insert(PXAdapter adapter)
  {
    Dictionary<string, object> values = new Dictionary<string, object>();
    if (adapter.Searches != null && adapter.View.Cache.Keys.Count > 1)
    {
      string key = adapter.View.Cache.Keys[adapter.View.Cache.Keys.Count - 1];
      for (int index = 0; index < adapter.Searches.Length && index < adapter.SortColumns.Length; ++index)
      {
        if (string.Compare(adapter.SortColumns[index], key, StringComparison.OrdinalIgnoreCase) != 0)
          values[adapter.SortColumns[index]] = adapter.Searches[index];
      }
    }
    if (adapter.View.Cache.Insert((IDictionary) values) != 1)
      return;
    if (adapter.SortColumns == null)
    {
      adapter.SortColumns = adapter.View.Cache.Keys.ToArray();
    }
    else
    {
      List<string> list = new List<string>((IEnumerable<string>) adapter.SortColumns);
      foreach (string key in (IEnumerable<string>) adapter.View.Cache.Keys)
      {
        if (!CompareIgnoreCase.IsInList(list, key))
          list.Add(key);
      }
      adapter.SortColumns = list.ToArray();
    }
    adapter.Searches = new object[adapter.SortColumns.Length];
    for (int index = 0; index < adapter.Searches.Length; ++index)
    {
      object stateOrValue;
      if (values.TryGetValue(adapter.SortColumns[index], out stateOrValue))
        adapter.Searches[index] = PXFieldState.UnwrapValue(stateOrValue);
    }
    adapter.StartRow = 0;
  }

  /// <summary>
  /// Calls PXAction.Insert(adapter) and then returns result of the adapter.Get() call.
  /// </summary>
  protected virtual IEnumerable InsertAndGet(PXAdapter adapter)
  {
    this.Insert(adapter);
    return adapter.Get();
  }

  public override System.Type GetRowType() => typeof (TNode);

  /// <inheritdoc cref="P:PX.Data.PXAction.IsMass" />
  public override bool IsMass
  {
    get
    {
      if (!this._isMass.HasValue)
        this._isMass = new bool?(this._Handler?.Method != (MethodInfo) null && Attribute.IsDefined((MemberInfo) this._Handler.Method, typeof (PXMassActionAttribute)));
      return this._isMass.Value;
    }
    set => this._isMass = new bool?(value);
  }

  internal override bool HasAdapterSupport => this._Handler is PXButtonDelegate;
}
