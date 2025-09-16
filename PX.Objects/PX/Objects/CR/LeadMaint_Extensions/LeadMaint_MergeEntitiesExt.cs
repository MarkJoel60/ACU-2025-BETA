// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.LeadMaint_Extensions.LeadMaint_MergeEntitiesExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions.SideBySideComparison;
using PX.Objects.CR.Extensions.SideBySideComparison.Merge;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.LeadMaint_Extensions;

public class LeadMaint_MergeEntitiesExt : MergeEntitiesExt<LeadMaint, CRLead>
{
  public override EntitiesContext GetLeftEntitiesContext()
  {
    return new EntitiesContext((PXGraph) this.Base, new EntityEntry(typeof (Contact), ((PXSelectBase) this.Base.Lead).Cache, new IBqlTable[1]
    {
      (IBqlTable) ((PXSelectBase<CRLead>) this.Base.Lead).Current
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
    int result;
    if (!int.TryParse(((PXSelectBase<MergeEntitiesFilter>) this.Filter).Current.MergeEntityID, out result))
      throw new PXException("The contact ID {0} is invalid and cannot be parsed to the integer.", new object[1]
      {
        (object) result
      });
    CRLead row = CRLead.PK.Find((PXGraph) this.Base, new int?(result));
    if (row == null)
      throw new PXException("The lead with the ID {0} cannot be found.", new object[1]
      {
        (object) result
      });
    Address address = Address.PK.Find((PXGraph) this.Base, row.DefAddressID);
    if (address == null)
      throw new PXException("The contact's address with the ID {0} cannot be found.", new object[1]
      {
        (object) row.DefAddressID
      });
    IEnumerable<CSAnswers> items = this.Base.Answers.SelectInternal((object) row);
    return new EntitiesContext((PXGraph) this.Base, new EntityEntry(typeof (Contact), ((PXSelectBase) this.Base.Lead).Cache, new IBqlTable[1]
    {
      (IBqlTable) row
    }), new EntityEntry[2]
    {
      new EntityEntry(typeof (Address), ((PXSelectBase) this.Base.AddressCurrent).Cache, new IBqlTable[1]
      {
        (IBqlTable) address
      }),
      new EntityEntry(((PXSelectBase) this.Base.Answers).Cache, (IEnumerable<IBqlTable>) items)
    });
  }

  public override void MergeRelatedDocuments(CRLead targetEntity, CRLead duplicateEntity)
  {
    base.MergeRelatedDocuments(targetEntity, duplicateEntity);
    PXCache Activities = ((PXGraph) this.Base).Caches[typeof (CRPMTimeActivity)];
    foreach (CRPMTimeActivity crpmTimeActivity in GraphHelper.RowCast<CRPMTimeActivity>((IEnumerable) PXSelectBase<CRPMTimeActivity, PXSelect<CRPMTimeActivity, Where<CRPMTimeActivity.refNoteID, Equal<Current<CRLead.noteID>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
    {
      (object) duplicateEntity
    }, Array.Empty<object>())).Select<CRPMTimeActivity, CRPMTimeActivity>((Func<CRPMTimeActivity, CRPMTimeActivity>) (cas => (CRPMTimeActivity) Activities.CreateCopy((object) cas))))
    {
      crpmTimeActivity.RefNoteID = targetEntity.NoteID;
      crpmTimeActivity.ContactID = targetEntity.RefContactID;
      crpmTimeActivity.BAccountID = targetEntity.BAccountID;
      Activities.Update((object) crpmTimeActivity);
    }
    PXCache Opportunities = ((PXGraph) this.Base).Caches[typeof (CROpportunity)];
    foreach (CROpportunity crOpportunity in GraphHelper.RowCast<CROpportunity>((IEnumerable) PXSelectBase<CROpportunity, PXSelect<CROpportunity, Where<CROpportunity.leadID, Equal<Current<CRLead.noteID>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
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
    foreach (CRRelation crRelation in GraphHelper.RowCast<CRRelation>((IEnumerable) PXSelectBase<CRRelation, PXSelectJoin<CRRelation, LeftJoin<CRRelation2, On<CRRelation.entityID, Equal<CRRelation2.entityID>, And<CRRelation.role, Equal<CRRelation2.role>, And<CRRelation2.refNoteID, Equal<Required<CRLead.noteID>>>>>>, Where<CRRelation2.entityID, IsNull, And<CRRelation.refNoteID, Equal<Required<CRLead.noteID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
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
    foreach (CRMarketingListMember marketingListMember in GraphHelper.RowCast<CRMarketingListMember>((IEnumerable) PXSelectBase<CRMarketingListMember, PXSelectJoin<CRMarketingListMember, LeftJoin<CRMarketingListMember2, On<CRMarketingListMember.marketingListID, Equal<CRMarketingListMember2.marketingListID>, And<CRMarketingListMember2.contactID, Equal<Required<CRLead.contactID>>>>>, Where<CRMarketingListMember.contactID, Equal<Required<CRLead.contactID>>, And<CRMarketingListMember2.marketingListID, IsNull>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) targetEntity.ContactID,
      (object) duplicateEntity.ContactID
    })).Select<CRMarketingListMember, CRMarketingListMember>((Func<CRMarketingListMember, CRMarketingListMember>) (mmember => (CRMarketingListMember) Subscriptions.CreateCopy((object) mmember))))
    {
      marketingListMember.ContactID = targetEntity.ContactID;
      Subscriptions.Insert((object) marketingListMember);
    }
    PXCache Members = ((PXGraph) this.Base).Caches[typeof (CRCampaignMembers)];
    foreach (CRCampaignMembers crCampaignMembers in GraphHelper.RowCast<CRCampaignMembers>((IEnumerable) PXSelectBase<CRCampaignMembers, PXSelectJoin<CRCampaignMembers, LeftJoin<CRCampaignMembers2, On<CRCampaignMembers.campaignID, Equal<CRCampaignMembers2.campaignID>, And<CRCampaignMembers2.contactID, Equal<Required<CRLead.contactID>>>>>, Where<CRCampaignMembers2.campaignID, IsNull, And<CRCampaignMembers.contactID, Equal<Required<CRLead.contactID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) targetEntity.ContactID,
      (object) duplicateEntity.ContactID
    })).Select<CRCampaignMembers, CRCampaignMembers>((Func<CRCampaignMembers, CRCampaignMembers>) (cmember => (CRCampaignMembers) Members.CreateCopy((object) cmember))))
    {
      crCampaignMembers.ContactID = targetEntity.ContactID;
      Members.Insert((object) crCampaignMembers);
    }
  }
}
