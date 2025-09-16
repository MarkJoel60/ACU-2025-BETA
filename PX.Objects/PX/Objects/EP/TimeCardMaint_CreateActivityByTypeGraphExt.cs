// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.TimeCardMaint_CreateActivityByTypeGraphExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.EP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

public class TimeCardMaint_CreateActivityByTypeGraphExt : PXGraphExtension<TimeCardMaint>
{
  public PXAction<EPTimeCard> createActivity;

  [InjectDependency]
  protected IActivityService ActivityService { get; private set; }

  public override void Initialize()
  {
    base.Initialize();
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.timeReportingModule>())
    {
      this.createActivity.SetVisible(false);
    }
    else
    {
      this.createActivity.SetVisible(true);
      this.AddActivityQuickActionsAsMenu();
    }
  }

  public virtual void AddActivityQuickActionsAsMenu()
  {
    foreach (PX.Data.EP.ActivityService.IActivityType activityType in this.ActivityService.GetActivityTypes().ToList<PX.Data.EP.ActivityService.IActivityType>())
    {
      PX.Data.EP.ActivityService.IActivityType type = activityType;
      this.createActivity.AddMenuAction(this.AddAction((PXGraph) this.Base, type.Type?.Trim(), type.Description, true, (PXButtonDelegate) (adapter => this.CreateActivity(adapter, type.Type)), (PXEventSubscriberAttribute) new PXButtonAttribute()
      {
        CommitChanges = true,
        DisplayOnMainToolbar = false,
        OnClosingPopup = PXSpecialButtonType.Refresh
      }));
    }
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
      MapEnableRights = PXCacheRights.Select
    };
    if (!visible)
      pxuiFieldAttribute.Visible = false;
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>()
    {
      (PXEventSubscriberAttribute) pxuiFieldAttribute
    };
    if (defaultAttributes != null)
      subscriberAttributeList.AddRange(((IEnumerable<PXEventSubscriberAttribute>) defaultAttributes).Where<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (attr => attr != null)));
    PXNamedAction<PX.Objects.CR.CRActivity> pxNamedAction = new PXNamedAction<PX.Objects.CR.CRActivity>(graph, actionName, handler, subscriberAttributeList.ToArray());
    graph.Actions[actionName] = (PXAction) pxNamedAction;
    return (PXAction) pxNamedAction;
  }

  [PXUIField(DisplayName = "")]
  [PXInsertButton(MenuAutoOpen = true, Tooltip = "Create Activity")]
  protected virtual IEnumerable CreateActivity(PXAdapter adapter, string type)
  {
    if (this.Base.Document.Current == null)
      return adapter.Get();
    int? defContactId = (int?) this.Base.Employee.SelectSingle()?.DefContactID;
    new PX.Objects.EP.ActivityService().CreateActivity((object) null, type, defContactId);
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<EPTimeCard> e, PXRowSelected baseDel)
  {
    baseDel(e.Cache, e.Args);
    if (e.Row == null)
      return;
    this.createActivity.SetVisible(this.Base.Activities.Cache.AllowInsert);
    this.createActivity.SetEnabled(this.Base.Activities.Cache.AllowInsert && e.Cache.GetStatus((object) e.Row) != PXEntryStatus.Inserted);
    if ((this.createActivity.GetState((object) e.Row) is PXButtonState state ? state.Menus : (ButtonMenu[]) null) == null)
      return;
    foreach (ButtonMenu menu in state.Menus)
    {
      this.Base.Actions[menu.Command]?.SetVisible(this.createActivity.GetVisible());
      this.Base.Actions[menu.Command]?.SetEnabled(this.createActivity.GetEnabled());
    }
  }
}
