// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMarketingListMaint_MarketingMemberImport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.BusinessAccountMaint_Extensions;
using PX.Objects.CR.ContactMaint_Extensions;
using PX.Objects.CR.Extensions;
using PX.Objects.CR.LeadMaint_Extensions;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

public class CRMarketingListMaint_MarketingMemberImport : 
  MarketingMemberImport<CRMarketingListMaint, CRMarketingList, CRMarketingListMember>
{
  [PXOverride]
  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    object[] parameters,
    ExecuteUpdateDelegate del)
  {
    return viewName == "ListMembers" && ((PXGraph) this.Base).IsImportFromExcel ? del("ImportEntity", keys, values, parameters) : del(viewName, keys, values, parameters);
  }

  public override void GenerateDummyRowsForProcessingResultsGrid(
    List<CRMarketingList> list,
    List<CRMarketingMemberForImport> membersToCreate)
  {
    list.Add(((PXSelectBase<CRMarketingList>) this.Base.MailLists).Current);
    for (int index = 0; index < membersToCreate.Count - 1; ++index)
      list.Add(new CRMarketingList()
      {
        Selected = new bool?(true),
        MarketingListID = new int?(index),
        MailListCode = index.ToString("D12").Replace('0', 'Y')
      });
  }

  public override void InsertContactMarketingInfoIfNotExists(
    ContactMaint graph,
    Contact contact,
    CRMarketingMemberForImport newMember)
  {
    if (CRMarketingListMember.PK.Find((PXGraph) graph, newMember.LinkMarketingListID, contact.ContactID) != null)
      return;
    ((PXSelectBase<CRMarketingListMember>) ((PXGraph) graph).GetExtension<ContactMaint_MarketingListDetailsExt>().Subscriptions).Insert(new CRMarketingListMember()
    {
      MarketingListID = newMember.LinkMarketingListID,
      ContactID = contact.ContactID,
      IsSubscribed = new bool?(true)
    });
  }

  public override void InsertLeadMarketingInfoIfNotExtsis(
    LeadMaint graph,
    CRLead lead,
    CRMarketingMemberForImport newMember)
  {
    if (CRMarketingListMember.PK.Find((PXGraph) graph, newMember.LinkMarketingListID, lead.ContactID) != null)
      return;
    ((PXSelectBase<CRMarketingListMember>) ((PXGraph) graph).GetExtension<LeadMaint_MarketingListDetailsExt>().Subscriptions).Insert(new CRMarketingListMember()
    {
      MarketingListID = newMember.LinkMarketingListID,
      ContactID = lead.ContactID,
      IsSubscribed = new bool?(true)
    });
  }

  public override void InsertBAMarketingInfoIfNotExists(
    BusinessAccountMaint graph,
    int? contactID,
    CRMarketingMemberForImport newMember)
  {
    if (CRMarketingListMember.PK.Find((PXGraph) graph, newMember.LinkMarketingListID, contactID) != null)
      return;
    BusinessAccountMaint_MarketingListDetailsExt extension = ((PXGraph) graph).GetExtension<BusinessAccountMaint_MarketingListDetailsExt>();
    PXDBDefaultAttribute.SetDefaultForInsert<CRMarketingListMember.contactID>(((PXSelectBase) extension.Subscriptions).Cache, (object) ((PXSelectBase<CRMarketingListMember>) extension.Subscriptions).Insert(new CRMarketingListMember()
    {
      MarketingListID = newMember.LinkMarketingListID,
      ContactID = contactID,
      IsSubscribed = new bool?(true)
    }), false);
  }

  public override bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (string.Compare(viewName, "ListMembers", StringComparison.OrdinalIgnoreCase) != 0)
      return true;
    values[(object) "LinkMarketingListID"] = (object) (int?) ((PXSelectBase<CRMarketingList>) this.Base.MailLists).Current?.MarketingListID;
    return base.PrepareImportRow(viewName, keys, values);
  }
}
