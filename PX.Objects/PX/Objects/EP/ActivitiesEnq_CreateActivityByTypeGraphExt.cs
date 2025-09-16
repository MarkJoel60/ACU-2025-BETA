// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ActivitiesEnq_CreateActivityByTypeGraphExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.EP;
using PX.Objects.CR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

public class ActivitiesEnq_CreateActivityByTypeGraphExt : PXGraphExtension<ActivitiesEnq>
{
  public PXAction<CRPMTimeActivity> createActivity;

  [InjectDependency]
  protected IActivityService ActivityService { get; private set; }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.AddActivityQuickActionsAsMenu();
  }

  public virtual void AddActivityQuickActionsAsMenu()
  {
    foreach (PX.Data.EP.ActivityService.IActivityType iactivityType in this.ActivityService.GetActivityTypes().ToList<PX.Data.EP.ActivityService.IActivityType>())
    {
      ActivitiesEnq graph = this.Base;
      string actionName = iactivityType.Type?.Trim();
      string description = iactivityType.Description;
      ActivitiesEnq_CreateActivityByTypeGraphExt activityByTypeGraphExt = this;
      // ISSUE: virtual method pointer
      PXButtonDelegate handler = new PXButtonDelegate((object) activityByTypeGraphExt, __vmethodptr(activityByTypeGraphExt, CreateActivity));
      PXEventSubscriberAttribute[] subscriberAttributeArray = new PXEventSubscriberAttribute[1]
      {
        (PXEventSubscriberAttribute) new PXButtonAttribute()
        {
          CommitChanges = true,
          DisplayOnMainToolbar = false,
          OnClosingPopup = (PXSpecialButtonType) 4
        }
      };
      ((PXAction) this.createActivity).AddMenuAction(this.AddAction((PXGraph) graph, actionName, description, true, handler, subscriberAttributeArray));
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
    return (PXAction) new PXNamedAction<CRActivity>(graph, actionName, handler, subscriberAttributeList.ToArray());
  }

  [PXUIField(DisplayName = "")]
  [PXInsertButton(MenuAutoOpen = true, Tooltip = "Create Activity", DisplayOnMainToolbar = true)]
  protected virtual IEnumerable CreateActivity(PXAdapter adapter)
  {
    new PX.Objects.EP.ActivityService().CreateActivity((object) null, adapter.Menu, (PXRedirectHelper.WindowMode) 4);
    return adapter.Get();
  }
}
