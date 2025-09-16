// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BackwardCompatibility.CRCampaignMembersActivityList`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.BackwardCompatibility;

/// <exclude />
[Obsolete]
public class CRCampaignMembersActivityList<TPrimaryView> : 
  CRActivityListBase<TPrimaryView, CRPMTimeActivity>
  where TPrimaryView : class, IBqlTable, INotable, new()
{
  protected internal const string _NEW_CAMPAIGNMEMBER_ACTIVITY_COMMAND = "NewCampaignMemberActivity";
  protected internal const string _NEW_CAMPAIGNMEMBER_TASK_COMMAND = "NewCampaignMemberTask";
  protected internal const string _NEW_CAMPAIGNMEMBER_EVENT_COMMAND = "NewCampaignMemberEvent";
  protected internal const string _NEW_CAMPAIGNMEMBER_MAILACTIVITY_COMMAND = "NewCampaignMemberMailActivity";

  public CRCampaignMembersActivityList(PXGraph graph)
    : base(graph)
  {
    this.AddCampginMembersActivityQuickActionsAsMenu(graph);
  }

  protected override void SetCommandCondition(Delegate handler = null)
  {
    SelectFromBase<CRPMTimeActivity, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<CRReminder>.On<BqlOperand<CRReminder.refNoteID, IBqlGuid>.IsEqual<CRPMTimeActivity.noteID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRPMTimeActivity.documentNoteID, Equal<BqlField<CRCampaign.noteID, IBqlGuid>.FromCurrent>>>>>.Or<BqlOperand<CRPMTimeActivity.refNoteID, IBqlGuid>.IsEqual<BqlField<CRCampaign.noteID, IBqlGuid>.FromCurrent>>>.OrderBy<BqlField<CRPMTimeActivity.createdDateTime, IBqlDateTime>.Desc> orderBy = new SelectFromBase<CRPMTimeActivity, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<CRReminder>.On<BqlOperand<CRReminder.refNoteID, IBqlGuid>.IsEqual<CRPMTimeActivity.noteID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRPMTimeActivity.documentNoteID, Equal<BqlField<CRCampaign.noteID, IBqlGuid>.FromCurrent>>>>>.Or<BqlOperand<CRPMTimeActivity.refNoteID, IBqlGuid>.IsEqual<BqlField<CRCampaign.noteID, IBqlGuid>.FromCurrent>>>.OrderBy<BqlField<CRPMTimeActivity.createdDateTime, IBqlDateTime>.Desc>();
    if ((object) handler == null)
      ((PXSelectBase) this).View = new PXView(((PXSelectBase) this).View.Graph, ((PXSelectBase) this).View.IsReadOnly, (BqlCommand) orderBy);
    else
      ((PXSelectBase) this).View = new PXView(((PXSelectBase) this).View.Graph, ((PXSelectBase) this).View.IsReadOnly, (BqlCommand) orderBy, handler);
  }

  private void AddCampginMembersActivityQuickActionsAsMenu(PXGraph graph)
  {
    List<ActivityService.IActivityType> iactivityTypeList = (List<ActivityService.IActivityType>) null;
    try
    {
      iactivityTypeList = ServiceLocator.Current.GetInstance<IActivityService>().GetActivityTypes().ToList<ActivityService.IActivityType>();
    }
    catch (Exception ex)
    {
    }
    PXButtonAttribute pxButtonAttribute = new PXButtonAttribute()
    {
      OnClosingPopup = (PXSpecialButtonType) 4,
      DisplayOnMainToolbar = false
    };
    PXGraph graph1 = graph;
    string displayName = PXMessages.LocalizeNoPrefix("Add Activity");
    int num = iactivityTypeList == null ? 0 : (iactivityTypeList.Count > 0 ? 1 : 0);
    CRCampaignMembersActivityList<TPrimaryView> membersActivityList = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate handler = new PXButtonDelegate((object) membersActivityList, __vmethodptr(membersActivityList, NewCampaignMemberActivity));
    PXEventSubscriberAttribute[] subscriberAttributeArray = new PXEventSubscriberAttribute[1]
    {
      (PXEventSubscriberAttribute) pxButtonAttribute
    };
    PXAction pxAction = this.AddAction(graph1, "NewCampaignMemberActivity", displayName, num != 0, handler, subscriberAttributeArray);
    if (iactivityTypeList == null || iactivityTypeList.Count <= 0)
      return;
    List<ButtonMenu> buttonMenuList = new List<ButtonMenu>(iactivityTypeList.Count);
    foreach (ActivityService.IActivityType iactivityType in iactivityTypeList)
    {
      ButtonMenu buttonMenu = new ButtonMenu(iactivityType.Type, PXMessages.LocalizeFormatNoPrefix("Add {0}", new object[1]
      {
        (object) iactivityType.Description
      }), (string) null);
      if (iactivityType.IsDefault.GetValueOrDefault())
        buttonMenuList.Insert(0, buttonMenu);
      else
        buttonMenuList.Add(buttonMenu);
    }
    ButtonMenu buttonMenu1 = new ButtonMenu("NewCampaignMemberTask", "Add Task", (string) null);
    buttonMenuList.Add(buttonMenu1);
    ButtonMenu buttonMenu2 = new ButtonMenu("NewCampaignMemberEvent", "Add Event", (string) null);
    buttonMenuList.Add(buttonMenu2);
    ButtonMenu buttonMenu3 = new ButtonMenu("NewCampaignMemberMailActivity", "Add Email", (string) null);
    buttonMenuList.Add(buttonMenu3);
    pxAction.SetMenu(buttonMenuList.ToArray());
  }

  [PXButton]
  [PXShortCut(true, false, false, new char[] {'A', 'C'})]
  public virtual IEnumerable NewCampaignMemberActivity(PXAdapter adapter)
  {
    string type = (string) null;
    int classId = 2;
    switch (adapter.Menu)
    {
      case "NewCampaignMemberTask":
        classId = 0;
        break;
      case "NewCampaignMemberEvent":
        classId = 1;
        break;
      case "NewCampaignMemberMailActivity":
        classId = 4;
        break;
      default:
        type = adapter.Menu;
        break;
    }
    this.CreateCampaignMemberActivity(classId, type);
    return adapter.Get();
  }

  private void CreateCampaignMemberActivity(int classId, string type)
  {
    PXCache<CRCampaignMembers> pxCache1 = GraphHelper.Caches<CRCampaignMembers>(((PXSelectBase) this)._Graph);
    if (!(((PXCache) pxCache1).Current is CRCampaignMembers current1))
      return;
    PXGraph newActivity = this.CreateNewActivity(classId, type);
    if (newActivity == null)
      return;
    PXCache pxCache2 = classId != 4 ? (PXCache) GraphHelper.Caches<CRActivity>(newActivity) : (PXCache) GraphHelper.Caches<CRSMEmail>(newActivity);
    PXResultset<Contact, PX.Objects.CR.Standalone.CRLead> pxResultset = PXSelectBase<Contact, PXViewOf<Contact>.BasedOn<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.CR.Standalone.CRLead>.On<BqlOperand<PX.Objects.CR.Standalone.CRLead.contactID, IBqlInt>.IsEqual<Contact.contactID>>>>.Where<BqlOperand<Contact.contactID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select<PXResultset<Contact, PX.Objects.CR.Standalone.CRLead>>(((PXSelectBase) this)._Graph, new object[1]
    {
      (object) current1.ContactID
    });
    Contact contact = PXResultset<Contact, PX.Objects.CR.Standalone.CRLead>.op_Implicit(pxResultset);
    PX.Objects.CR.Standalone.CRLead crLead = PXResultset<Contact, PX.Objects.CR.Standalone.CRLead>.op_Implicit(pxResultset);
    if (crLead != null && crLead.ContactID.HasValue)
    {
      pxCache2.SetValue<CRActivity.refNoteID>(pxCache2.Current, (object) contact.NoteID);
      pxCache2.SetValue<CRActivity.contactID>(pxCache2.Current, (object) crLead.RefContactID);
    }
    else if (contact.ContactType == "PN")
      pxCache2.SetValue<CRActivity.contactID>(pxCache2.Current, (object) contact.ContactID);
    TPrimaryView current2 = ((PXSelectBase) this)._Graph.Caches[typeof (TPrimaryView)].Current as TPrimaryView;
    pxCache2.SetValue<CRActivity.documentNoteID>(pxCache2.Current, (object) (Guid?) current2?.NoteID);
    pxCache2.SetValue<CRActivity.bAccountID>(pxCache2.Current, (object) contact.BAccountID);
    pxCache2.SetValue<CRSMEmail.mailTo>(pxCache2.Current, (object) contact.EMail);
    pxCache2.SetValue<CRSMEmail.mailReply>(pxCache2.Current, (object) contact.EMail);
    ((PXCache) pxCache1).ClearQueryCacheObsolete();
    ((PXCache) pxCache1).Clear();
    PXRedirectHelper.TryRedirect(newActivity, (PXRedirectHelper.WindowMode) 3);
  }
}
