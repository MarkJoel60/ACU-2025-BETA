// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.VendorSearch.ContactRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition.VendorSearch;

internal class ContactRepository : IContactRepository
{
  public PX.Objects.CR.Contact GetAccountContact(PXGraph graph, int baccountId, int defContactId)
  {
    return (PX.Objects.CR.Contact) PXSelectBase<PX.Objects.CR.Contact, PXViewOf<PX.Objects.CR.Contact>.BasedOn<SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.CR.Contact.bAccountID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.CR.Contact.contactID, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly.Config>.Select(graph, (object) baccountId, (object) defContactId);
  }

  public PX.Objects.CR.Contact GetPrimaryContact(
    PXGraph graph,
    int baccountId,
    int primaryContactID)
  {
    return (PX.Objects.CR.Contact) PXSelectBase<PX.Objects.CR.Contact, PXViewOf<PX.Objects.CR.Contact>.BasedOn<SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.CR.Contact.bAccountID, Equal<P.AsInt>>>>, PX.Data.And<BqlOperand<PX.Objects.CR.Contact.contactType, IBqlString>.IsEqual<ContactTypesAttribute.person>>>>.And<BqlOperand<PX.Objects.CR.Contact.contactID, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly.Config>.Select(graph, (object) baccountId, (object) primaryContactID);
  }

  public List<string> GetOtherContactEmails(
    PXGraph graph,
    int baccountId,
    PX.Objects.CR.Contact accountContact,
    PX.Objects.CR.Contact primaryContact)
  {
    return PXSelectBase<PX.Objects.CR.Contact, PXViewOf<PX.Objects.CR.Contact>.BasedOn<SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.CR.Contact.bAccountID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.CR.Contact.contactType, IBqlString>.IsEqual<ContactTypesAttribute.person>>>>.ReadOnly.Config>.Select(graph, (object) baccountId).FirstTableItems.Where<PX.Objects.CR.Contact>((Func<PX.Objects.CR.Contact, bool>) (c =>
    {
      int? contactId1 = c.ContactID;
      int? contactId2 = (int?) accountContact?.ContactID;
      if (!(contactId1.GetValueOrDefault() == contactId2.GetValueOrDefault() & contactId1.HasValue == contactId2.HasValue))
      {
        int? contactId3 = c.ContactID;
        int? contactId4 = (int?) primaryContact?.ContactID;
        if (!(contactId3.GetValueOrDefault() == contactId4.GetValueOrDefault() & contactId3.HasValue == contactId4.HasValue))
          return !string.IsNullOrEmpty(c.EMail);
      }
      return false;
    })).Select<PX.Objects.CR.Contact, string>((Func<PX.Objects.CR.Contact, string>) (c => c.EMail)).ToList<string>();
  }
}
