// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ContactMaint_Extensions.ContactMaint_MergeEntitiesExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions.SideBySideComparison;
using PX.Objects.CR.Extensions.SideBySideComparison.Merge;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.ContactMaint_Extensions;

public class ContactMaint_MergeEntitiesExt : MergeEntitiesExt<ContactMaint, Contact>
{
  public virtual Contact GetSelectedContact()
  {
    int result;
    if (!int.TryParse(((PXSelectBase<MergeEntitiesFilter>) this.Filter).Current.MergeEntityID, out result))
      throw new PXException("The contact ID {0} is invalid and cannot be parsed to the integer.", new object[1]
      {
        (object) result
      });
    return Contact.PK.Find((PXGraph) this.Base, new int?(result)) ?? throw new PXException("The contact with the ID {0} cannot be found.", new object[1]
    {
      (object) result
    });
  }

  public override EntitiesContext GetLeftEntitiesContext()
  {
    return new EntitiesContext((PXGraph) this.Base, new EntityEntry(typeof (Contact), ((PXSelectBase) this.Base.Contact).Cache, new IBqlTable[1]
    {
      (IBqlTable) ((PXSelectBase<Contact>) this.Base.Contact).Current
    }), new EntityEntry[2]
    {
      new EntityEntry(typeof (Address), ((PXSelectBase) this.Base.AddressCurrent).Cache, new IBqlTable[1]
      {
        (IBqlTable) ((PXSelectBase<Address>) this.Base.AddressCurrent).SelectSingle(Array.Empty<object>())
      }),
      new EntityEntry(((PXSelectBase) this.Base.Answers).Cache, (IBqlTable[]) this.Base.Answers.SelectMain(Array.Empty<object>()))
    });
  }

  public override EntitiesContext GetRightEntitiesContext()
  {
    Contact selectedContact = this.GetSelectedContact();
    Address address = Address.PK.Find((PXGraph) this.Base, selectedContact.DefAddressID);
    if (address == null)
      throw new PXException("The contact's address with the ID {0} cannot be found.", new object[1]
      {
        (object) selectedContact.DefAddressID
      });
    IEnumerable<CSAnswers> items = this.Base.Answers.SelectInternal((object) selectedContact);
    return new EntitiesContext((PXGraph) this.Base, new EntityEntry(typeof (Contact), ((PXSelectBase) this.Base.Contact).Cache, new IBqlTable[1]
    {
      (IBqlTable) selectedContact
    }), new EntityEntry[2]
    {
      new EntityEntry(typeof (Address), ((PXSelectBase) this.Base.AddressCurrent).Cache, new IBqlTable[1]
      {
        (IBqlTable) address
      }),
      new EntityEntry(((PXSelectBase) this.Base.Answers).Cache, (IEnumerable<IBqlTable>) items)
    });
  }

  public override void MergeRelatedDocuments(Contact targetEntity, Contact duplicateEntity)
  {
    base.MergeRelatedDocuments(targetEntity, duplicateEntity);
    PXCache Activities = ((PXGraph) this.Base).Caches[typeof (CRPMTimeActivity)];
    foreach (CRPMTimeActivity crpmTimeActivity in GraphHelper.RowCast<CRPMTimeActivity>((IEnumerable) PXSelectBase<CRPMTimeActivity, PXSelect<CRPMTimeActivity, Where<CRPMTimeActivity.contactID, Equal<Current<Contact.contactID>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
    {
      (object) duplicateEntity
    }, Array.Empty<object>())).Select<CRPMTimeActivity, CRPMTimeActivity>((Func<CRPMTimeActivity, CRPMTimeActivity>) (cas => (CRPMTimeActivity) Activities.CreateCopy((object) cas))))
    {
      crpmTimeActivity.ContactID = targetEntity.ContactID;
      crpmTimeActivity.BAccountID = targetEntity.BAccountID;
      Activities.Update((object) crpmTimeActivity);
    }
    foreach (CRPMTimeActivity crpmTimeActivity in GraphHelper.RowCast<CRPMTimeActivity>((IEnumerable) PXSelectBase<CRPMTimeActivity, PXSelect<CRPMTimeActivity, Where<CRPMTimeActivity.refNoteID, Equal<Current<Contact.noteID>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
    {
      (object) duplicateEntity
    }, Array.Empty<object>())).Select<CRPMTimeActivity, CRPMTimeActivity>((Func<CRPMTimeActivity, CRPMTimeActivity>) (cas => (CRPMTimeActivity) Activities.CreateCopy((object) cas))))
    {
      crpmTimeActivity.RefNoteID = targetEntity.NoteID;
      Activities.Update((object) crpmTimeActivity);
    }
    PXCache Cases = ((PXGraph) this.Base).Caches[typeof (CRCase)];
    foreach (CRCase crCase in GraphHelper.RowCast<CRCase>((IEnumerable) PXSelectBase<CRCase, PXSelect<CRCase, Where<CRCase.contactID, Equal<Current<Contact.contactID>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
    {
      (object) duplicateEntity
    }, Array.Empty<object>())).Select<CRCase, CRCase>((Func<CRCase, CRCase>) (cas => (CRCase) Cases.CreateCopy((object) cas))))
    {
      int? baccountId = targetEntity.BAccountID;
      int? customerId = crCase.CustomerID;
      if (!(baccountId.GetValueOrDefault() == customerId.GetValueOrDefault() & baccountId.HasValue == customerId.HasValue))
        throw new PXException("Contact '{0}' has case '{1}' for another business account.", new object[2]
        {
          (object) duplicateEntity.DisplayName,
          (object) crCase.CaseCD
        });
      crCase.ContactID = targetEntity.ContactID;
      Cases.Update((object) crCase);
    }
    PXCache Opportunities = ((PXGraph) this.Base).Caches[typeof (CROpportunity)];
    foreach (CROpportunity crOpportunity in GraphHelper.RowCast<CROpportunity>((IEnumerable) PXSelectBase<CROpportunity, PXSelect<CROpportunity, Where<CROpportunity.contactID, Equal<Current<Contact.contactID>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
    {
      (object) duplicateEntity
    }, Array.Empty<object>())).Select<CROpportunity, CROpportunity>((Func<CROpportunity, CROpportunity>) (opp => (CROpportunity) Opportunities.CreateCopy((object) opp))))
    {
      int? baccountId1 = targetEntity.BAccountID;
      int? baccountId2 = crOpportunity.BAccountID;
      if (!(baccountId1.GetValueOrDefault() == baccountId2.GetValueOrDefault() & baccountId1.HasValue == baccountId2.HasValue))
        throw new PXException("Contact '{0}' ({1}) has opportunity '{2}' for another business account.", new object[3]
        {
          (object) duplicateEntity.DisplayName,
          (object) duplicateEntity.ContactID,
          (object) crOpportunity.OpportunityID
        });
      crOpportunity.ContactID = targetEntity.ContactID;
      Opportunities.Update((object) crOpportunity);
    }
    PXCache Relations = ((PXGraph) this.Base).Caches[typeof (CRRelation)];
    foreach (CRRelation crRelation in GraphHelper.RowCast<CRRelation>((IEnumerable) PXSelectBase<CRRelation, PXSelectJoin<CRRelation, LeftJoin<CRRelation2, On<CRRelation.entityID, Equal<CRRelation2.entityID>, And<CRRelation.role, Equal<CRRelation2.role>, And<CRRelation2.refNoteID, Equal<Required<Contact.noteID>>>>>>, Where<CRRelation2.entityID, IsNull, And<CRRelation.refNoteID, Equal<Required<Contact.noteID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) targetEntity.NoteID,
      (object) duplicateEntity.NoteID
    })).Select<CRRelation, CRRelation>((Func<CRRelation, CRRelation>) (rel => (CRRelation) Relations.CreateCopy((object) rel))))
    {
      crRelation.RelationID = new int?();
      crRelation.RefNoteID = targetEntity.NoteID;
      crRelation.RefEntityType = ((object) targetEntity).GetType().FullName;
      Relations.Insert((object) crRelation);
    }
    PXCache Subscriptions = ((PXGraph) this.Base).Caches[typeof (CRMarketingListMember)];
    foreach (CRMarketingListMember marketingListMember in GraphHelper.RowCast<CRMarketingListMember>((IEnumerable) PXSelectBase<CRMarketingListMember, PXSelectJoin<CRMarketingListMember, LeftJoin<CRMarketingListMember2, On<CRMarketingListMember.marketingListID, Equal<CRMarketingListMember2.marketingListID>, And<CRMarketingListMember2.contactID, Equal<Required<Contact.contactID>>>>>, Where<CRMarketingListMember.contactID, Equal<Required<Contact.contactID>>, And<CRMarketingListMember2.marketingListID, IsNull>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) targetEntity.ContactID,
      (object) duplicateEntity.ContactID
    })).Select<CRMarketingListMember, CRMarketingListMember>((Func<CRMarketingListMember, CRMarketingListMember>) (mmember => (CRMarketingListMember) Subscriptions.CreateCopy((object) mmember))))
    {
      marketingListMember.ContactID = targetEntity.ContactID;
      Subscriptions.Insert((object) marketingListMember);
    }
    PXCache Members = ((PXGraph) this.Base).Caches[typeof (CRCampaignMembers)];
    foreach (CRCampaignMembers crCampaignMembers in GraphHelper.RowCast<CRCampaignMembers>((IEnumerable) PXSelectBase<CRCampaignMembers, PXSelectJoin<CRCampaignMembers, LeftJoin<CRCampaignMembers2, On<CRCampaignMembers.campaignID, Equal<CRCampaignMembers2.campaignID>, And<CRCampaignMembers2.contactID, Equal<Required<Contact.contactID>>>>>, Where<CRCampaignMembers2.campaignID, IsNull, And<CRCampaignMembers.contactID, Equal<Required<Contact.contactID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) targetEntity.ContactID,
      (object) duplicateEntity.ContactID
    })).Select<CRCampaignMembers, CRCampaignMembers>((Func<CRCampaignMembers, CRCampaignMembers>) (cmember => (CRCampaignMembers) Members.CreateCopy((object) cmember))))
    {
      crCampaignMembers.ContactID = targetEntity.ContactID;
      Members.Insert((object) crCampaignMembers);
    }
    PXCache NWatchers = ((PXGraph) this.Base).Caches[typeof (ContactNotification)];
    foreach (ContactNotification contactNotification in GraphHelper.RowCast<ContactNotification>((IEnumerable) PXSelectBase<ContactNotification, PXSelectJoin<ContactNotification, LeftJoin<ContactNotification2, On<ContactNotification.setupID, Equal<ContactNotification2.setupID>, And<ContactNotification2.contactID, Equal<Required<Contact.contactID>>>>>, Where<ContactNotification2.setupID, IsNull, And<ContactNotification.contactID, Equal<Required<Contact.contactID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) targetEntity.ContactID,
      (object) duplicateEntity.ContactID
    })).Select<ContactNotification, ContactNotification>((Func<ContactNotification, ContactNotification>) (watcher => (ContactNotification) NWatchers.CreateCopy((object) watcher))))
    {
      contactNotification.NotificationID = new int?();
      contactNotification.ContactID = targetEntity.ContactID;
      NWatchers.Insert((object) contactNotification);
    }
    ((PXSelectBase) ((PXGraph) this.Base).GetExtension<ContactMaint.LinkLeadFromContactExt>().Filter).View.Answer = (WebDialogResult) 5;
    PXCache Leads = ((PXGraph) this.Base).Caches[typeof (CRLead)];
    foreach (CRLead crLead in GraphHelper.RowCast<CRLead>((IEnumerable) PXSelectBase<CRLead, PXSelect<CRLead, Where<CRLead.refContactID, Equal<Required<Contact.contactID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) duplicateEntity.ContactID
    })).Select<CRLead, CRLead>((Func<CRLead, CRLead>) (lead => (CRLead) Leads.CreateCopy((object) lead))))
    {
      crLead.RefContactID = targetEntity.ContactID;
      crLead.BAccountID = targetEntity.BAccountID;
      Leads.Update((object) crLead);
    }
    if (!duplicateEntity.UserID.HasValue)
      return;
    Users users = PXResultset<Users>.op_Implicit(PXSelectBase<Users, PXSelect<Users, Where<Users.pKID, Equal<Required<Contact.userID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) duplicateEntity.UserID
    }));
    if (users == null)
      return;
    GraphHelper.EnsureCachePersistence((PXGraph) this.Base, typeof (Users));
    users.IsApproved = new bool?(false);
    ((PXGraph) this.Base).Caches[typeof (Users)].Update((object) users);
  }

  public override MergeEntitiesFilter CreateNewFilter(object mergeEntityID)
  {
    MergeEntitiesFilter newFilter = base.CreateNewFilter(mergeEntityID);
    if (((PXSelectBase<Contact>) this.Base.Contact).Current.Status == "I")
      ((PXSelectBase) this.Filter).Cache.SetValueExt<MergeEntitiesFilter.targetRecord>((object) newFilter, (object) 1);
    return newFilter;
  }

  public virtual void _(
    Events.FieldVerifying<MergeEntitiesFilter, MergeEntitiesFilter.targetRecord> e)
  {
    if (!((!(((Events.FieldVerifyingBase<Events.FieldVerifying<MergeEntitiesFilter, MergeEntitiesFilter.targetRecord>, MergeEntitiesFilter, object>) e).NewValue is 1) ? ((PXSelectBase<Contact>) this.Base.Contact).Current : this.GetSelectedContact()).Status == "I"))
      return;
    PXUIFieldAttribute.SetWarning<MergeEntitiesFilter.targetRecord>(((Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<MergeEntitiesFilter, MergeEntitiesFilter.targetRecord>>) e).Cache, (object) e.Row, "The target record has the Inactive status.");
  }
}
