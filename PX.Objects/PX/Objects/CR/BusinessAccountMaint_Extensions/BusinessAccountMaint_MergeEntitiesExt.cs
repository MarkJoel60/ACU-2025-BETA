// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BusinessAccountMaint_Extensions.BusinessAccountMaint_MergeEntitiesExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR.Extensions.SideBySideComparison;
using PX.Objects.CR.Extensions.SideBySideComparison.Merge;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.BusinessAccountMaint_Extensions;

public class BusinessAccountMaint_MergeEntitiesExt : MergeEntitiesExt<BusinessAccountMaint, BAccount>
{
  public virtual BAccount GetSelectedBAccount()
  {
    int result;
    if (!int.TryParse(((PXSelectBase<MergeEntitiesFilter>) this.Filter).Current.MergeEntityID, out result))
      throw new PXException("The business account ID {0} is invalid and cannot be parsed to the integer.", new object[1]
      {
        (object) result
      });
    return BAccount.PK.Find((PXGraph) this.Base, new int?(result)) ?? throw new PXException("The business account with the ID {0} cannot be found.", new object[1]
    {
      (object) result
    });
  }

  public override EntitiesContext GetLeftEntitiesContext()
  {
    ((IQueryable<PXResult<BAccount>>) ((PXSelectBase<BAccount>) this.Base.CurrentBAccount).Select(Array.Empty<object>())).FirstOrDefault<PXResult<BAccount>>();
    BAccount current = ((PXSelectBase<BAccount>) this.Base.BAccount).Current;
    BusinessAccountMaint.DefContactAddressExt extension = ((PXGraph) this.Base).GetExtension<BusinessAccountMaint.DefContactAddressExt>();
    Contact contact = ((PXSelectBase<Contact>) extension.DefContact).SelectSingle(Array.Empty<object>());
    Address address = ((PXSelectBase<Address>) extension.DefAddress).SelectSingle(Array.Empty<object>());
    return new EntitiesContext((PXGraph) this.Base, new EntityEntry(((PXSelectBase) this.Base.BAccount).Cache, new IBqlTable[1]
    {
      (IBqlTable) current
    }), new EntityEntry[3]
    {
      new EntityEntry(typeof (Contact), ((PXSelectBase) extension.DefContact).Cache, new IBqlTable[1]
      {
        (IBqlTable) contact
      }),
      new EntityEntry(typeof (Address), ((PXSelectBase) extension.DefAddress).Cache, new IBqlTable[1]
      {
        (IBqlTable) address
      }),
      new EntityEntry(((PXSelectBase) this.Base.Answers).Cache, (IBqlTable[]) this.Base.Answers.SelectMain(Array.Empty<object>()))
    });
  }

  public override EntitiesContext GetRightEntitiesContext()
  {
    BAccount selectedBaccount = this.GetSelectedBAccount();
    Contact contact = Contact.PK.Find((PXGraph) this.Base, selectedBaccount.DefContactID);
    if (contact == null)
      throw new PXException("The contact with the ID {0} cannot be found.", new object[1]
      {
        (object) selectedBaccount.DefContactID
      });
    Address address = Address.PK.Find((PXGraph) this.Base, selectedBaccount.DefAddressID);
    if (address == null)
      throw new PXException("The contact's address with the ID {0} cannot be found.", new object[1]
      {
        (object) selectedBaccount.DefAddressID
      });
    IEnumerable<CSAnswers> items = this.Base.Answers.SelectInternal((object) selectedBaccount);
    return new EntitiesContext((PXGraph) this.Base, new EntityEntry(((PXSelectBase) this.Base.BAccount).Cache, new IBqlTable[1]
    {
      (IBqlTable) selectedBaccount
    }), new EntityEntry[3]
    {
      new EntityEntry(typeof (Contact), ((PXSelectBase) this.Base.ContactDummy).Cache, new IBqlTable[1]
      {
        (IBqlTable) contact
      }),
      new EntityEntry(typeof (Address), ((PXSelectBase) this.Base.AddressDummy).Cache, new IBqlTable[1]
      {
        (IBqlTable) address
      }),
      new EntityEntry(((PXSelectBase) this.Base.Answers).Cache, (IEnumerable<IBqlTable>) items)
    });
  }

  public override void MergeRelatedDocuments(BAccount targetEntity, BAccount duplicateEntity)
  {
    base.MergeRelatedDocuments(targetEntity, duplicateEntity);
    PXCache Activities = ((PXGraph) this.Base).Caches[typeof (CRPMTimeActivity)];
    foreach (CRPMTimeActivity crpmTimeActivity in GraphHelper.RowCast<CRPMTimeActivity>((IEnumerable) PXSelectBase<CRPMTimeActivity, PXSelect<CRPMTimeActivity, Where<CRPMTimeActivity.bAccountID, Equal<Required<BAccount.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) duplicateEntity.BAccountID
    })).Select<CRPMTimeActivity, CRPMTimeActivity>((Func<CRPMTimeActivity, CRPMTimeActivity>) (cas => (CRPMTimeActivity) Activities.CreateCopy((object) cas))))
    {
      int? baccountId1 = crpmTimeActivity.BAccountID;
      int? baccountId2 = duplicateEntity.BAccountID;
      if (baccountId1.GetValueOrDefault() == baccountId2.GetValueOrDefault() & baccountId1.HasValue == baccountId2.HasValue)
        crpmTimeActivity.BAccountID = targetEntity.BAccountID;
      crpmTimeActivity.BAccountID = targetEntity.BAccountID;
      Activities.Update((object) crpmTimeActivity);
    }
    foreach (CRPMTimeActivity crpmTimeActivity in GraphHelper.RowCast<CRPMTimeActivity>((IEnumerable) PXSelectBase<CRPMTimeActivity, PXSelect<CRPMTimeActivity, Where<CRPMTimeActivity.refNoteID, Equal<Current<BAccount.noteID>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
    {
      (object) duplicateEntity
    }, Array.Empty<object>())).Select<CRPMTimeActivity, CRPMTimeActivity>((Func<CRPMTimeActivity, CRPMTimeActivity>) (cas => (CRPMTimeActivity) Activities.CreateCopy((object) cas))))
    {
      crpmTimeActivity.RefNoteID = targetEntity.NoteID;
      Activities.Update((object) crpmTimeActivity);
    }
    PXCache Cases = ((PXGraph) this.Base).Caches[typeof (CRCase)];
    foreach (CRCase crCase in GraphHelper.RowCast<CRCase>((IEnumerable) PXSelectBase<CRCase, PXSelect<CRCase, Where<CRCase.customerID, Equal<Required<BAccount.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) duplicateEntity.BAccountID
    })).Select<CRCase, CRCase>((Func<CRCase, CRCase>) (cas => (CRCase) Cases.CreateCopy((object) cas))))
    {
      crCase.CustomerID = targetEntity.BAccountID;
      Cases.Update((object) crCase);
    }
    PXCache Opportunities = ((PXGraph) this.Base).Caches[typeof (CROpportunity)];
    foreach (CROpportunity crOpportunity in GraphHelper.RowCast<CROpportunity>((IEnumerable) PXSelectBase<CROpportunity, PXSelect<CROpportunity, Where<CROpportunity.bAccountID, Equal<Required<BAccount.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) duplicateEntity.BAccountID
    })).Select<CROpportunity, CROpportunity>((Func<CROpportunity, CROpportunity>) (opp => (CROpportunity) Opportunities.CreateCopy((object) opp))))
    {
      crOpportunity.BAccountID = targetEntity.BAccountID;
      crOpportunity.LocationID = targetEntity.DefLocationID;
      Opportunities.Update((object) crOpportunity);
    }
    PXCache Relations = ((PXGraph) this.Base).Caches[typeof (CRRelation)];
    foreach (CRRelation crRelation in GraphHelper.RowCast<CRRelation>((IEnumerable) PXSelectBase<CRRelation, PXSelectJoin<CRRelation, LeftJoin<CRRelation2, On<CRRelation.entityID, Equal<CRRelation2.entityID>, And<CRRelation.role, Equal<CRRelation2.role>, And<CRRelation2.refNoteID, Equal<Required<BAccount.noteID>>>>>>, Where<CRRelation2.entityID, IsNull, And<CRRelation.refNoteID, Equal<Required<BAccount.noteID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
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
    int? defContactID = duplicateEntity.DefContactID;
    PXCache Contacts = ((PXGraph) this.Base).Caches[typeof (Contact)];
    foreach (Contact contact in GraphHelper.RowCast<Contact>((IEnumerable) PXSelectBase<Contact, PXViewOf<Contact>.BasedOn<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact.bAccountID, Equal<P.AsInt>>>>>.And<BqlOperand<Contact.contactType, IBqlString>.IsNotEqual<ContactTypesAttribute.lead>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) duplicateEntity.BAccountID
    })).Where<Contact>((Func<Contact, bool>) (c =>
    {
      int? contactId = c.ContactID;
      int? nullable = defContactID;
      return !(contactId.GetValueOrDefault() == nullable.GetValueOrDefault() & contactId.HasValue == nullable.HasValue);
    })).Select<Contact, Contact>((Func<Contact, Contact>) (c => (Contact) Contacts.CreateCopy((object) c))))
    {
      contact.BAccountID = targetEntity.BAccountID;
      Contacts.Update((object) contact);
    }
    PXCache Leads = ((PXGraph) this.Base).Caches[typeof (CRLead)];
    foreach (CRLead crLead in GraphHelper.RowCast<CRLead>((IEnumerable) PXSelectBase<CRLead, PXSelect<CRLead, Where<CRLead.bAccountID, Equal<Required<BAccount.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) duplicateEntity.BAccountID
    })).Select<CRLead, CRLead>((Func<CRLead, CRLead>) (lead => (CRLead) Leads.CreateCopy((object) lead))))
    {
      crLead.BAccountID = targetEntity.BAccountID;
      Leads.Update((object) crLead);
    }
    PXCache cach = ((PXGraph) this.Base).Caches[typeof (BAccount)];
    duplicateEntity.Status = "I";
    BAccount baccount = duplicateEntity;
    cach.Update((object) baccount);
  }

  public override IEnumerable<string> GetFieldsForComparison(
    System.Type itemType,
    PXCache leftCache,
    PXCache rightCache)
  {
    IEnumerable<string> source = base.GetFieldsForComparison(itemType, leftCache, rightCache);
    if (itemType == typeof (BAccount))
      source = source.Append<string>("PrimaryContactID");
    return source;
  }

  public override (EntitiesContext LeftContext, EntitiesContext RightContext) ProcessComparisons(
    IReadOnlyCollection<MergeComparisonRow> comparisons)
  {
    (EntitiesContext LeftContext, EntitiesContext RightContext) tuple = base.ProcessComparisons(comparisons);
    EntitiesContext duplicate = this.DefineTargetAndDuplicateContexts(tuple.LeftContext, tuple.RightContext).duplicate;
    using (duplicate.PreserveCurrentsScope())
    {
      EntityEntry entry = duplicate.Entries[typeof (BAccount)];
      BAccount baccount = entry.Single<BAccount>();
      baccount.PrimaryContactID = new int?();
      entry.Cache.Update((object) baccount);
      return tuple;
    }
  }

  public override MergeEntitiesFilter CreateNewFilter(object mergeEntityID)
  {
    int int32 = Convert.ToInt32(mergeEntityID);
    Contact contact = Contact.PK.Find((PXGraph) this.Base, new int?(int32));
    if (contact == null)
      throw new PXException("The contact with the ID {0} cannot be found.", new object[1]
      {
        (object) int32
      });
    BAccount baccount = BAccount.PK.Find((PXGraph) this.Base, contact.BAccountID);
    MergeEntitiesFilter newFilter = baccount != null ? base.CreateNewFilter((object) baccount.BAccountID) : throw new PXException("The business account with the ID {0} cannot be found.", new object[1]
    {
      (object) contact.BAccountID
    });
    int num = ((PXSelectBase<BAccount>) this.Base.BAccount).Current.Status == "I" || baccount.Type != "PR" ? 1 : 0;
    ((PXSelectBase) this.Filter).Cache.SetValueExt<MergeEntitiesFilter.targetRecord>((object) newFilter, (object) num);
    return newFilter;
  }

  public override MergeComparisonRow CreateComparisonRow(
    string fieldName,
    System.Type itemType,
    ref int order,
    (PXCache Cache, IBqlTable Item, string Value, PXFieldState State) left,
    (PXCache Cache, IBqlTable Item, string Value, PXFieldState State) right)
  {
    MergeComparisonRow comparisonRow = base.CreateComparisonRow(fieldName, itemType, ref order, left, right);
    if (comparisonRow.FieldName == "PrimaryContactID")
    {
      comparisonRow.FieldDisplayName = PXMessages.LocalizeNoPrefix("Primary Contact");
      comparisonRow.LeftFieldState.SelectorMode = (PXSelectorMode) 16 /*0x10*/;
      comparisonRow.RightFieldState.SelectorMode = (PXSelectorMode) 16 /*0x10*/;
      int result1;
      if (string.IsNullOrEmpty(comparisonRow.LeftValue_description) && int.TryParse(comparisonRow.LeftValue, out result1) && result1 < 0)
        comparisonRow.LeftFieldState.Value = (object) (comparisonRow.LeftValue = (string) null);
      int result2;
      if (string.IsNullOrEmpty(comparisonRow.RightValue_description) && int.TryParse(comparisonRow.RightValue, out result2) && result2 < 0)
        comparisonRow.RightFieldState.Value = (object) (comparisonRow.RightValue = (string) null);
    }
    return comparisonRow;
  }

  public virtual void _(
    Events.FieldVerifying<MergeEntitiesFilter, MergeEntitiesFilter.targetRecord> e)
  {
    BAccount baccount1 = ((PXSelectBase<BAccount>) this.Base.BAccount).Current;
    BAccount baccount2 = this.GetSelectedBAccount();
    if (((Events.FieldVerifyingBase<Events.FieldVerifying<MergeEntitiesFilter, MergeEntitiesFilter.targetRecord>, MergeEntitiesFilter, object>) e).NewValue is 1)
    {
      BAccount baccount3 = baccount2;
      baccount2 = baccount1;
      baccount1 = baccount3;
    }
    if (baccount1.Status == "I")
      PXUIFieldAttribute.SetWarning<MergeEntitiesFilter.targetRecord>(((Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<MergeEntitiesFilter, MergeEntitiesFilter.targetRecord>>) e).Cache, (object) e.Row, "The target record has the Inactive status.");
    if (!(baccount2.Type != "PR"))
      return;
    PXUIFieldAttribute.SetError<MergeEntitiesFilter.targetRecord>(((Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<MergeEntitiesFilter, MergeEntitiesFilter.targetRecord>>) e).Cache, (object) e.Row, "A customer or vendor account cannot be used as a source business account; it can be used only as the target.");
  }
}
