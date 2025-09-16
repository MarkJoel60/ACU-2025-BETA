// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CampaignMaint_MarketingMemberImport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

public class CampaignMaint_MarketingMemberImport : 
  MarketingMemberImport<CampaignMaint, CRCampaign, CRCampaignMembers>
{
  [PXOverride]
  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    object[] parameters,
    ExecuteUpdateDelegate del)
  {
    return viewName == "CampaignMembers" && ((PXGraph) this.Base).IsImportFromExcel ? del("ImportEntity", keys, values, parameters) : del(viewName, keys, values, parameters);
  }

  public override void GenerateDummyRowsForProcessingResultsGrid(
    List<CRCampaign> list,
    List<CRMarketingMemberForImport> membersToCreate)
  {
    list.Add(((PXSelectBase<CRCampaign>) this.Base.Campaign).Current);
    for (int index = 0; index < membersToCreate.Count - 1; ++index)
      list.Add(new CRCampaign()
      {
        Selected = new bool?(true),
        CampaignID = index.ToString("D12").Replace('0', 'Y')
      });
  }

  public override void InsertContactMarketingInfoIfNotExists(
    ContactMaint graph,
    Contact contact,
    CRMarketingMemberForImport newMember)
  {
    if (CRCampaignMembers.PK.Find((PXGraph) graph, newMember.LinkCampaignID, contact.ContactID) != null)
      return;
    ((PXSelectBase<CRCampaignMembers>) graph.Members).Insert(new CRCampaignMembers()
    {
      CampaignID = newMember.LinkCampaignID,
      ContactID = contact.ContactID
    });
  }

  public override void InsertLeadMarketingInfoIfNotExtsis(
    LeadMaint graph,
    CRLead lead,
    CRMarketingMemberForImport newMember)
  {
    if (CRCampaignMembers.PK.Find((PXGraph) graph, newMember.LinkCampaignID, lead.ContactID) != null)
      return;
    ((PXSelectBase<CRCampaignMembers>) graph.Members).Insert(new CRCampaignMembers()
    {
      CampaignID = newMember.LinkCampaignID,
      ContactID = lead.ContactID
    });
  }

  public override void InsertBAMarketingInfoIfNotExists(
    BusinessAccountMaint graph,
    int? contactID,
    CRMarketingMemberForImport newMember)
  {
    if (CRCampaignMembers.PK.Find((PXGraph) graph, newMember.LinkCampaignID, contactID) != null)
      return;
    CRCampaignMembers crCampaignMembers = ((PXSelectBase<CRCampaignMembers>) graph.Members).Insert(new CRCampaignMembers()
    {
      CampaignID = newMember.LinkCampaignID,
      ContactID = contactID
    });
    PXDBDefaultAttribute.SetDefaultForInsert<CRCampaignMembers.contactID>(((PXSelectBase) graph.Members).Cache, (object) crCampaignMembers, false);
  }

  public override bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (string.Compare(viewName, "CampaignMembers", StringComparison.OrdinalIgnoreCase) != 0)
      return true;
    values[(object) "LinkCampaignID"] = (object) ((PXSelectBase<CRCampaign>) this.Base.Campaign).Current?.CampaignID;
    return base.PrepareImportRow(viewName, keys, values);
  }
}
