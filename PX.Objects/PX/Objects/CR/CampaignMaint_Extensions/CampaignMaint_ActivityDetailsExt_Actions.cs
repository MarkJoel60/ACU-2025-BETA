// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CampaignMaint_Extensions.CampaignMaint_ActivityDetailsExt_Actions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR.Extensions;
using PX.Objects.EP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.CampaignMaint_Extensions;

public class CampaignMaint_ActivityDetailsExt_Actions : 
  ActivityDetailsExt_Actions<CampaignMaint_ActivityDetailsExt, CampaignMaint, CRCampaign, CRCampaign.noteID>
{
  protected internal const string _NEW_CAMPAIGNMEMBER_TASK_COMMAND = "NewCampaignMemberTask";
  protected internal const string _NEW_CAMPAIGNMEMBER_EVENT_COMMAND = "NewCampaignMemberEvent";
  protected internal const string _NEW_CAMPAIGNMEMBER_MAILACTIVITY_COMMAND = "NewCampaignMemberMailActivity";
  public PXAction<CRCampaign> NewCampaignMemberActivity;

  public override void Initialize()
  {
    base.Initialize();
    this.AddCampginMembersActivityQuickActionsAsMenu();
  }

  [PXButton]
  [PXUIField(DisplayName = "Add Activity")]
  public virtual IEnumerable newCampaignMemberActivity(PXAdapter adapter, string type)
  {
    int classId = 2;
    switch (type)
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
    }
    this.CreateCampaignMemberActivity(classId, type);
    return adapter.Get();
  }

  public virtual void AddCampginMembersActivityQuickActionsAsMenu()
  {
    List<PX.Data.EP.ActivityService.IActivityType> second = (List<PX.Data.EP.ActivityService.IActivityType>) null;
    try
    {
      second = this.ActivityService.GetActivityTypes().ToList<PX.Data.EP.ActivityService.IActivityType>();
    }
    catch (Exception ex)
    {
    }
    if (second == null || second.Count <= 0)
      return;
    EPActivityType[] first = new EPActivityType[3]
    {
      new EPActivityType()
      {
        Description = "Task",
        Type = "NewCampaignMemberTask",
        IsDefault = new bool?(false)
      },
      new EPActivityType()
      {
        Description = "Event",
        Type = "NewCampaignMemberEvent",
        IsDefault = new bool?(false)
      },
      new EPActivityType()
      {
        Description = "Email",
        Type = "NewCampaignMemberMailActivity",
        IsDefault = new bool?(false)
      }
    };
    foreach (PX.Data.EP.ActivityService.IActivityType iactivityType in ((IEnumerable<PX.Data.EP.ActivityService.IActivityType>) first).Union<PX.Data.EP.ActivityService.IActivityType>((IEnumerable<PX.Data.EP.ActivityService.IActivityType>) second))
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CampaignMaint_ActivityDetailsExt_Actions.\u003C\u003Ec__DisplayClass6_0 cDisplayClass60 = new CampaignMaint_ActivityDetailsExt_Actions.\u003C\u003Ec__DisplayClass6_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass60.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass60.type = iactivityType;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((PXAction) this.NewCampaignMemberActivity).AddMenuAction(this.AddAction((PXGraph) this.Base, "Add" + cDisplayClass60.type.Description?.Replace(" ", ""), PXMessages.LocalizeFormatNoPrefix("Add {0}", new object[1]
      {
        (object) cDisplayClass60.type.Description
      }), true, new PXButtonDelegate((object) cDisplayClass60, __methodptr(\u003CAddCampginMembersActivityQuickActionsAsMenu\u003Eb__0)), (PXEventSubscriberAttribute) new PXButtonAttribute()
      {
        CommitChanges = true,
        DisplayOnMainToolbar = false,
        OnClosingPopup = (PXSpecialButtonType) 4
      }));
    }
  }

  public virtual void CreateCampaignMemberActivity(int classId, string type)
  {
    PXCache<CRCampaignMembers> pxCache1 = GraphHelper.Caches<CRCampaignMembers>((PXGraph) this.Base);
    if (!(((PXCache) pxCache1).Current is CRCampaignMembers current1))
      return;
    PXGraph newActivity = this.ActivityDetailsExt.CreateNewActivity(classId, type);
    if (newActivity == null)
      return;
    PXCache pxCache2 = classId != 4 ? (PXCache) GraphHelper.Caches<CRActivity>(newActivity) : (PXCache) GraphHelper.Caches<CRSMEmail>(newActivity);
    PXResultset<PX.Objects.CR.Contact, CRLead, BAccount> pxResultset = PXSelectBase<PX.Objects.CR.Contact, PXViewOf<PX.Objects.CR.Contact>.BasedOn<SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<CRLead>.On<BqlOperand<CRLead.contactID, IBqlInt>.IsEqual<PX.Objects.CR.Contact.contactID>>>, FbqlJoins.Left<BAccount>.On<BqlOperand<BAccount.defContactID, IBqlInt>.IsEqual<PX.Objects.CR.Contact.contactID>>>>.Where<BqlOperand<PX.Objects.CR.Contact.contactID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select<PXResultset<PX.Objects.CR.Contact, CRLead, BAccount>>((PXGraph) this.Base, new object[1]
    {
      (object) current1.ContactID
    });
    PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact, CRLead, BAccount>.op_Implicit(pxResultset);
    CRLead crLead = PXResultset<PX.Objects.CR.Contact, CRLead, BAccount>.op_Implicit(pxResultset);
    BAccount baccount = PXResultset<PX.Objects.CR.Contact, CRLead, BAccount>.op_Implicit(pxResultset);
    if (crLead != null && crLead.ContactID.HasValue)
    {
      pxCache2.SetValue<CRActivity.refNoteIDType>(pxCache2.Current, (object) typeof (CRLead).FullName);
      pxCache2.SetValue<CRActivity.refNoteID>(pxCache2.Current, (object) PXNoteAttribute.GetNoteID(newActivity.Caches[typeof (CRLead)], (object) crLead, EntityHelper.GetNoteField(typeof (CRLead))));
      pxCache2.SetValue<CRActivity.contactID>(pxCache2.Current, (object) crLead.RefContactID);
    }
    else if (contact.ContactType == "PN")
    {
      pxCache2.SetValue<CRActivity.refNoteIDType>(pxCache2.Current, (object) typeof (PX.Objects.CR.Contact).FullName);
      pxCache2.SetValue<CRActivity.refNoteID>(pxCache2.Current, (object) PXNoteAttribute.GetNoteID(newActivity.Caches[typeof (PX.Objects.CR.Contact)], (object) contact, EntityHelper.GetNoteField(typeof (PX.Objects.CR.Contact))));
      pxCache2.SetValue<CRActivity.contactID>(pxCache2.Current, (object) contact.ContactID);
    }
    else if (contact.ContactType == "AP")
    {
      pxCache2.SetValue<CRActivity.refNoteIDType>(pxCache2.Current, (object) typeof (BAccount).FullName);
      pxCache2.SetValue<CRActivity.refNoteID>(pxCache2.Current, (object) PXNoteAttribute.GetNoteID(newActivity.Caches[typeof (BAccount)], (object) baccount, EntityHelper.GetNoteField(typeof (BAccount))));
      pxCache2.SetValue<CRActivity.contactID>(pxCache2.Current, (object) baccount.BAccountID);
    }
    CRCampaign current2 = ((PXGraph) this.Base).Caches[typeof (CRCampaign)].Current as CRCampaign;
    pxCache2.SetValue<CRActivity.documentNoteID>(pxCache2.Current, (object) (Guid?) current2?.NoteID);
    pxCache2.SetValue<CRActivity.bAccountID>(pxCache2.Current, (object) contact.BAccountID);
    pxCache2.SetValue<CRSMEmail.mailTo>(pxCache2.Current, (object) contact.EMail);
    pxCache2.SetValue<CRSMEmail.mailReply>(pxCache2.Current, (object) contact.EMail);
    ((PXCache) pxCache1).ClearQueryCacheObsolete();
    ((PXCache) pxCache1).Clear();
    PXRedirectHelper.TryRedirect(newActivity, (PXRedirectHelper.WindowMode) 3);
  }
}
