// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CampaignMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR.CRMarketingListMaint_Extensions;
using PX.Objects.CR.Extensions;
using PX.Objects.PM;
using PX.SM;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CR;

public class CampaignMaint : PXGraph<
#nullable disable
CampaignMaint, CRCampaign>
{
  [PXHidden]
  public PXSetup<CRSetup> crSetup;
  [PXHidden]
  public PXSelect<Contact> BaseContacts;
  [PXHidden]
  public PXSelect<APInvoice> APInvoicies;
  [PXHidden]
  public PXSelect<ARInvoice> ARInvoicies;
  public PXSelect<Contact, Where<Contact.contactID, Equal<Required<Contact.contactID>>>> ContactByContactId;
  [PXViewName("Campaign")]
  public PXSelect<CRCampaign> Campaign;
  [PXHidden]
  public PXSelect<PX.Objects.CR.CROpportunityClass> CROpportunityClass;
  [PXViewName("Opportunities")]
  public PXSelectJoin<CROpportunity, LeftJoin<CROpportunityProbability, On<CROpportunity.stageID, Equal<CROpportunityProbability.stageCode>>, LeftJoin<PX.Objects.CR.CROpportunityClass, On<PX.Objects.CR.CROpportunityClass.cROpportunityClassID, Equal<CROpportunity.classID>>>>, Where<CROpportunity.campaignSourceID, Equal<Current<CRCampaign.campaignID>>>> Opportunities;
  [PXHidden]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (CRCampaign.projectID), typeof (CRCampaign.projectTaskID)})]
  public PXSelect<CRCampaign, Where<CRCampaign.campaignID, Equal<Current<CRCampaign.campaignID>>>> CampaignCurrent;
  [PXHidden]
  public PXSelect<PX.Objects.CR.DAC.Standalone.CRCampaign, Where<PX.Objects.CR.DAC.Standalone.CRCampaign.campaignID, Equal<Current<CRCampaign.campaignID>>>> CalcCampaignCurrent;
  [PXViewName("Campaign Members")]
  [PXImportSubstitute(typeof (CRCampaign), typeof (CRMarketingMemberForImport))]
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (Contact), typeof (SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Contact.contactID, IBqlInt>.IsEqual<BqlField<CRCampaignMembers.contactID, IBqlInt>.FromCurrent>>))]
  [PXViewDetailsButton(typeof (BAccount), typeof (SelectFromBase<BAccount, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<Contact>.On<BqlOperand<Contact.bAccountID, IBqlInt>.IsEqual<BAccount.bAccountID>>>>.Where<BqlOperand<Contact.contactID, IBqlInt>.IsEqual<BqlField<CRCampaignMembers.contactID, IBqlInt>.FromCurrent>>))]
  [PXViewDetailsButton(typeof (CRMarketingList), typeof (SelectFromBase<CRMarketingList, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRMarketingList.marketingListID, IBqlInt>.IsEqual<BqlField<CRCampaignMembers.marketingListID, IBqlInt>.FromCurrent>>))]
  [PXViewDetailsButton(typeof (CRContactClass), typeof (SelectFromBase<CRContactClass, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<Contact>.On<BqlOperand<Contact.classID, IBqlString>.IsEqual<CRContactClass.classID>>>>.Where<BqlOperand<Contact.contactID, IBqlInt>.IsEqual<BqlField<CRCampaignMembers.contactID, IBqlInt>.FromCurrent>>))]
  public FbqlSelect<SelectFromBase<CRCampaignMembers, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<Contact>.On<BqlOperand<
  #nullable enable
  CRCampaignMembers.contactID, IBqlInt>.IsEqual<
  #nullable disable
  Contact.contactID>>>, FbqlJoins.Left<Address>.On<BqlOperand<
  #nullable enable
  Address.addressID, IBqlInt>.IsEqual<
  #nullable disable
  Contact.defAddressID>>>, FbqlJoins.Left<BAccount>.On<BqlOperand<
  #nullable enable
  BAccount.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  Contact.bAccountID>>>, FbqlJoins.Left<Address2>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  Address2.addressID, 
  #nullable disable
  Equal<BAccount.defAddressID>>>>>.And<BqlOperand<
  #nullable enable
  Address2.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BAccount.bAccountID>>>>, FbqlJoins.Left<CRMarketingList>.On<BqlOperand<
  #nullable enable
  CRCampaignMembers.marketingListID, IBqlInt>.IsEqual<
  #nullable disable
  CRMarketingList.marketingListID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlOperand<
  #nullable enable
  Contact.contactType, IBqlString>.IsIn<
  #nullable disable
  ContactTypesAttribute.lead, ContactTypesAttribute.person, ContactTypesAttribute.bAccountProperty, ContactTypesAttribute.employee>>>>.And<BqlOperand<
  #nullable enable
  CRCampaignMembers.campaignID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  CRCampaign.campaignID, IBqlString>.FromCurrent>>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  CRCampaignMembers.marketingListID, IBqlInt>.Asc>>, 
  #nullable disable
  CRCampaignMembers>.View CampaignMembers;
  [PXHidden]
  public PXSelect<CRCampaignMembers> CampaignMembersHidden;
  [PXViewName("Answers")]
  public CRAttributeList<CRCampaign> Answers;
  [PXViewName("Leads")]
  [PXFilterable(new System.Type[] {})]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<CRLead, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CRLead.bAccountID>>, LeftJoin<Address, On<Address.addressID, Equal<CRLead.defAddressID>>>>, Where<CRLead.campaignID, Equal<Current<CRCampaign.campaignID>>>, OrderBy<Asc<CRLead.displayName, Asc<CRLead.contactID>>>> Leads;
  [PXHidden]
  public FbqlSelect<SelectFromBase<CRCampaignToCRMarketingListLink, TypeArrayOf<IFbqlJoin>.Empty>, CRCampaignToCRMarketingListLink>.View CRCampaignToCRMarketingListLinkDummy;
  [PXViewName("Campaign Marketing Lists")]
  [PXViewDetailsButton(typeof (CRMarketingList))]
  [PXFilterable(new System.Type[] {})]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<CRMarketingListWithLinkToCRCampaign, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  CRCampaign.campaignID>, 
  #nullable disable
  IsNotNull>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CRMarketingListWithLinkToCRCampaign.campaignID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CRCampaign.campaignID, IBqlString>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<
  #nullable enable
  CRMarketingListWithLinkToCRCampaign.campaignID, IBqlString>.IsNull>>>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  CRMarketingListWithLinkToCRCampaign.selectedForCampaign, IBqlBool>.Desc>>, 
  #nullable disable
  CRMarketingListWithLinkToCRCampaign>.View CampaignMarketingLists;
  private ILogger _logger;
  public PXAction<CRCampaign> UpdateListMembers;
  public PXAction<CRCampaign> ClearMembers;
  public PXDBAction<CRCampaign> addOpportunity;
  public PXDBAction<CRCampaign> addContact;
  public PXAction<CROpportunity> addNewProjectTask;
  public PXAction<CRMarketingList> deleteAction;

  public CampaignMaint()
  {
    PXDBAttributeAttribute.Activate(((PXGraph) this).Caches[typeof (Contact)]);
    PXDBAttributeAttribute.Activate(((PXSelectBase) this.Opportunities).Cache);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.DAC.Standalone.CRCampaign.leadsGenerated>(((PXSelectBase) this.CampaignCurrent).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.DAC.Standalone.CRCampaign.leadsConverted>(((PXSelectBase) this.CampaignCurrent).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.DAC.Standalone.CRCampaign.contacts>(((PXSelectBase) this.CampaignCurrent).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.DAC.Standalone.CRCampaign.opportunities>(((PXSelectBase) this.CampaignCurrent).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.DAC.Standalone.CRCampaign.closedOpportunities>(((PXSelectBase) this.CampaignCurrent).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.DAC.Standalone.CRCampaign.opportunitiesValue>(((PXSelectBase) this.CampaignCurrent).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.DAC.Standalone.CRCampaign.closedOpportunitiesValue>(((PXSelectBase) this.CampaignCurrent).Cache, (object) null, false);
    PXUIFieldAttribute.SetRequired<CRCampaign.startDate>(((PXSelectBase) this.CampaignCurrent).Cache, true);
    PXUIFieldAttribute.SetRequired<CRCampaign.status>(((PXSelectBase) this.CampaignCurrent).Cache, true);
    PXCache cach1 = ((PXGraph) this).Caches[typeof (Contact)];
    PXDBAttributeAttribute.Activate(cach1);
    PXUIFieldAttribute.SetVisible<Contact.title>(cach1, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.workgroupID>(cach1, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.firstName>(cach1, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.midName>(cach1, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.lastName>(cach1, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.phone2>(cach1, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.phone3>(cach1, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.fax>(cach1, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.webSite>(cach1, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.dateOfBirth>(cach1, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.createdByID>(cach1, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.createdDateTime>(cach1, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.lastModifiedByID>(cach1, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.lastModifiedDateTime>(cach1, (object) null, false);
    PXUIFieldAttribute.SetVisible<Address.addressLine1>(((PXGraph) this).Caches[typeof (Address)], (object) null, false);
    PXUIFieldAttribute.SetVisible<Address.addressLine2>(((PXGraph) this).Caches[typeof (Address)], (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.classID>(cach1, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.source>(cach1, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.status>(cach1, (object) null, false);
    PXUIFieldAttribute.SetVisibility<Contact.contactPriority>(cach1, (object) null, (PXUIVisibility) 1);
    PXUIFieldAttribute.SetDisplayName<BAccount.acctName>(((PXGraph) this).Caches[typeof (BAccount)], "Customer Name");
    PXCache cach2 = ((PXGraph) this).Caches[typeof (CRLead)];
    PXUIFieldAttribute.SetVisible<Contact.title>(cach2, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.firstName>(cach2, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.midName>(cach2, (object) null, false);
    PXUIFieldAttribute.SetVisible<CRLead.lastName>(cach2, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.phone1>(cach2, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.phone2>(cach2, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.phone3>(cach2, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.fax>(cach2, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.eMail>(cach2, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.webSite>(cach2, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.dateOfBirth>(cach2, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.createdByID>(cach2, (object) null, false);
    PXUIFieldAttribute.SetVisible<CRLead.createdDateTime>(cach2, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.lastModifiedByID>(cach2, (object) null, false);
    PXUIFieldAttribute.SetVisible<Contact.lastModifiedDateTime>(cach2, (object) null, false);
    PXUIFieldAttribute.SetEnabled(((PXGraph) this).Caches[typeof (Contact)], (object) null, (string) null, false);
    PXUIFieldAttribute.SetEnabled<Contact.selected>(((PXGraph) this).Caches[typeof (Contact)], (object) null, true);
    ((PXSelectBase) this.CampaignMarketingLists).AllowInsert = ((PXSelectBase) this.CampaignMarketingLists).AllowInsert = false;
    PXCache cach3 = ((PXGraph) this).Caches[typeof (CRMarketingListWithLinkToCRCampaign)];
    PXDBAttributeAttribute.Activate(cach3);
    PXUIFieldAttribute.SetEnabled<CRMarketingListWithLinkToCRCampaign.campaignID>(cach3, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CRMarketingList.marketingListID>(cach3, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CRMarketingList.mailListCode>(cach3, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CRMarketingList.name>(cach3, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CRMarketingList.description>(cach3, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CRMarketingList.status>(cach3, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CRMarketingList.workgroupID>(cach3, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CRMarketingList.ownerID>(cach3, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CRMarketingList.method>(cach3, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CRMarketingList.type>(cach3, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CRMarketingList.gIDesignID>(cach3, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CRMarketingList.sharedGIFilter>(cach3, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CRMarketingList.noteID>(cach3, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CRMarketingList.createdByScreenID>(cach3, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CRMarketingList.createdByID>(cach3, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CRMarketingList.createdDateTime>(cach3, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CRMarketingList.lastModifiedByID>(cach3, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CRMarketingList.lastModifiedByScreenID>(cach3, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CRMarketingList.lastModifiedDateTime>(cach3, (object) null, false);
    PXUIFieldAttribute.SetDisplayName<Address2.addressLine1>((PXCache) GraphHelper.Caches<Address2>((PXGraph) this), "Business Account Address Line 1");
    PXUIFieldAttribute.SetDisplayName<Address2.addressLine2>((PXCache) GraphHelper.Caches<Address2>((PXGraph) this), "Business Account Address Line 2");
    PXUIFieldAttribute.SetDisplayName<Address2.city>((PXCache) GraphHelper.Caches<Address2>((PXGraph) this), "Business Account City");
    PXUIFieldAttribute.SetDisplayName<Address2.state>((PXCache) GraphHelper.Caches<Address2>((PXGraph) this), "Business Account State");
    PXUIFieldAttribute.SetDisplayName<Address2.postalCode>((PXCache) GraphHelper.Caches<Address2>((PXGraph) this), "Business Account Postal Code");
    PXUIFieldAttribute.SetDisplayName<Address2.countryID>((PXCache) GraphHelper.Caches<Address2>((PXGraph) this), "Business Account Country");
    PXUIFieldAttribute.SetDisplayName<CRMarketingList.mailListCode>(((PXGraph) this).Caches[typeof (CRMarketingListWithLinkToCRCampaign)], "Marketing List");
  }

  [InjectDependency]
  public ICRMarketingListMemberRepository MemberRepository { get; private set; }

  public virtual void Clear(PXClearOption option)
  {
    if (((Dictionary<System.Type, PXCache>) ((PXGraph) this).Caches).ContainsKey(typeof (CRCampaignMembers)))
      ((PXGraph) this).Caches[typeof (CRCampaignMembers)].ClearQueryCache();
    ((PXGraph) this).Clear(option);
  }

  [InjectDependency]
  public ILogger Logger
  {
    get => this._logger;
    set => this._logger = value?.ForContext<CampaignMaint>();
  }

  [PXUIField]
  [PXButton(Tooltip = "Replace campaign members added from marketing lists with those in the lists on the Marketing Lists tab")]
  public virtual IEnumerable updateListMembers(PXAdapter adapter)
  {
    bool flag;
    using (PXDataRecord pxDataRecord = ((PXGraph) this).ProviderSelectSingle<CRCampaignMembers>(new PXDataField[3]
    {
      (PXDataField) new PXDataField<CRCampaignMembers.campaignID>(),
      (PXDataField) new PXDataFieldValue<CRCampaignMembers.campaignID>((object) ((PXSelectBase<CRCampaign>) this.CampaignCurrent).Current.CampaignID),
      (PXDataField) new PXDataFieldValue<CRCampaignMembers.marketingListID>((object) null, (PXComp) 7)
    }))
      flag = pxDataRecord != null;
    if (!flag || ((PXSelectBase) this.CampaignMembers).View.Ask("All campaign members from marketing lists will be updated. Do you want to continue?", (MessageButtons) 1) == 1)
    {
      ((PXSelectBase) this.CampaignMembers).View.Answer = (WebDialogResult) 7;
      this.FixLastUpdateDate();
      ((PXGraph) this).Actions.PressSave();
      this.CampaignUpdateListMembersLongOperation();
      ((PXGraph) this).Actions.PressCancel();
    }
    return adapter.Get();
  }

  public virtual void CampaignUpdateListMembersLongOperation()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) new CampaignMaint.\u003C\u003Ec__DisplayClass28_0()
    {
      selectedItems = GraphHelper.RowCast<CRMarketingList>((IEnumerable) ((IEnumerable<CRMarketingListWithLinkToCRCampaign>) ((PXSelectBase<CRMarketingListWithLinkToCRCampaign>) this.CampaignMarketingLists).SelectMain(Array.Empty<object>())).Where<CRMarketingListWithLinkToCRCampaign>((Func<CRMarketingListWithLinkToCRCampaign, bool>) (i => i.SelectedForCampaign.GetValueOrDefault()))).ToList<CRMarketingList>(),
      graph = this.CloneGraphState<CampaignMaint>()
    }, __methodptr(\u003CCampaignUpdateListMembersLongOperation\u003Eb__1)));
  }

  public virtual void CampaignUpdateListMembers(
    CRCampaign cRCampaign,
    IEnumerable<CRMarketingList> maketingLists)
  {
    Guid userId = ((PXGraph) this).Accessinfo.UserID;
    string normalizedScreenId = ((PXGraph) this).Accessinfo.GetNormalizedScreenID();
    ((PXGraph) this).ProviderDelete<CRCampaignMembers>(new PXDataFieldRestrict[2]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<CRCampaignMembers.campaignID>((object) cRCampaign.CampaignID),
      (PXDataFieldRestrict) new PXDataFieldRestrict<CRCampaignMembers.marketingListID>((PXDbType) 8, new int?(4), (object) null, (PXComp) 7)
    });
    HashSet<int?> nullableSet = new HashSet<int?>();
    foreach (PXResult<CRMarketingListMember, Contact, Address> member in this.MemberRepository.GetMembers(maketingLists))
    {
      CRMarketingListMember marketingListMember = PXResult<CRMarketingListMember, Contact, Address>.op_Implicit(member);
      if (nullableSet.Add(marketingListMember.ContactID))
      {
        DateTime utcNow = PXTimeZoneInfo.UtcNow;
        try
        {
          ((PXGraph) this).ProviderInsert<CRCampaignMembers>(new PXDataFieldAssign[9]
          {
            (PXDataFieldAssign) new PXDataFieldAssign<CRCampaignMembers.campaignID>((object) cRCampaign.CampaignID),
            (PXDataFieldAssign) new PXDataFieldAssign<CRCampaignMembers.contactID>((object) marketingListMember.ContactID),
            (PXDataFieldAssign) new PXDataFieldAssign<CRCampaignMembers.marketingListID>((object) marketingListMember.MarketingListID),
            (PXDataFieldAssign) new PXDataFieldAssign<CRCampaignMembers.createdByID>((object) userId),
            (PXDataFieldAssign) new PXDataFieldAssign<CRCampaignMembers.lastModifiedByID>((object) userId),
            (PXDataFieldAssign) new PXDataFieldAssign<CRCampaignMembers.createdByScreenID>((object) normalizedScreenId),
            (PXDataFieldAssign) new PXDataFieldAssign<CRCampaignMembers.lastModifiedByScreenID>((object) normalizedScreenId),
            (PXDataFieldAssign) new PXDataFieldAssign<CRCampaignMembers.createdDateTime>((object) utcNow),
            (PXDataFieldAssign) new PXDataFieldAssign<CRCampaignMembers.lastModifiedDateTime>((object) utcNow)
          });
        }
        catch (PXDatabaseException ex) when (ex.ErrorCode == 4)
        {
          this.Logger.Verbose<int?, string>((Exception) ex, "Campaign member {ContactID} for Campaign {CampaignID} already exists", marketingListMember.ContactID, cRCampaign.CampaignID);
        }
      }
    }
  }

  [PXUIField]
  [PXButton(Tooltip = "Remove members currently shown on all pages from the list")]
  protected virtual IEnumerable clearMembers(PXAdapter adapter)
  {
    if (((PXSelectBase<CRCampaign>) this.CampaignCurrent)?.Current?.CampaignID != null && ((PXSelectBase) this.CampaignCurrent).View.Ask((string) null, "All currently displayed members will be removed from the list. Do you want to proceed?", (MessageButtons) 1) == 1)
      this.ClearCampaignMembersLongOperation();
    return adapter.Get();
  }

  public virtual void ClearCampaignMembersLongOperation()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) new CampaignMaint.\u003C\u003Ec__DisplayClass32_0()
    {
      graph = this.CloneGraphState<CampaignMaint>(),
      externalFilter = ((PXSelectBase) this.CampaignMembers).View.GetExternalFilters()
    }, __methodptr(\u003CClearCampaignMembersLongOperation\u003Eb__0)));
  }

  [PXUIField(DisplayName = "Add Opportunity", FieldClass = "CRM")]
  [PXButton]
  public virtual void AddOpportunity()
  {
    CRCampaign current = ((PXSelectBase<CRCampaign>) this.CampaignCurrent).Current;
    if (current == null || current.CampaignID == null)
      return;
    OpportunityMaint instance = PXGraph.CreateInstance<OpportunityMaint>();
    CROpportunity crOpportunity = ((PXSelectBase<CROpportunity>) instance.Opportunity).Insert();
    crOpportunity.CampaignSourceID = current.CampaignID;
    if (current.ProjectID.HasValue)
      crOpportunity.ProjectID = current.ProjectID;
    if (PXResultset<PX.Objects.CR.CROpportunityClass>.op_Implicit(PXSelectBase<PX.Objects.CR.CROpportunityClass, PXSelect<PX.Objects.CR.CROpportunityClass, Where<PX.Objects.CR.CROpportunityClass.cROpportunityClassID, Equal<Current<CROpportunity.classID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) crOpportunity
    }, Array.Empty<object>()))?.DefaultOwner == "S")
    {
      crOpportunity.WorkgroupID = current.WorkgroupID;
      crOpportunity.OwnerID = current.OwnerID;
    }
    ((PXSelectBase<CROpportunity>) instance.Opportunity).Update(crOpportunity);
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
  }

  [PXUIField(DisplayName = "Create Lead", FieldClass = "CRM")]
  [PXButton]
  public virtual void AddContact()
  {
    CRCampaign current = ((PXSelectBase<CRCampaign>) this.CampaignCurrent).Current;
    if (current == null || current.CampaignID == null)
      return;
    LeadMaint instance = PXGraph.CreateInstance<LeadMaint>();
    CRLead crLead = ((PXSelectBase<CRLead>) instance.Lead).Insert();
    crLead.CampaignID = current.CampaignID;
    if (PXResultset<CRLeadClass>.op_Implicit(PXSelectBase<CRLeadClass, PXSelect<CRLeadClass, Where<CRLeadClass.classID, Equal<Current<Contact.classID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) crLead
    }, Array.Empty<object>()))?.DefaultOwner == "S")
    {
      crLead.WorkgroupID = current.WorkgroupID;
      crLead.OwnerID = current.OwnerID;
    }
    ((PXSelectBase<CRLead>) instance.Lead).Update(crLead);
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
  }

  [PXUIField(Visible = false)]
  [PXButton]
  public virtual IEnumerable AddNewProjectTask(PXAdapter adapter)
  {
    CRCampaign current = ((PXSelectBase<CRCampaign>) this.CampaignCurrent).Current;
    if (current != null && current.ProjectID.HasValue)
    {
      ProjectTaskEntry instance = PXGraph.CreateInstance<ProjectTaskEntry>();
      ((PXGraph) instance).Clear();
      PMTask pmTask = new PMTask();
      ((PXSelectBase) instance.Task).Cache.SetValue<PMTask.projectID>((object) pmTask, (object) current.ProjectID);
      object campaignId = (object) current.CampaignID;
      ((PXSelectBase) instance.Task).Cache.RaiseFieldUpdating<PMTask.taskCD>((object) pmTask, ref campaignId);
      ((PXSelectBase) instance.Task).Cache.SetValue<PMTask.taskCD>((object) pmTask, campaignId);
      PMTask copy = (PMTask) ((PXSelectBase) instance.Task).Cache.CreateCopy((object) ((PXSelectBase<PMTask>) instance.Task).Insert(pmTask));
      ((PXSelectBase) instance.Task).Cache.SetValue<PMTask.description>((object) copy, (object) current.CampaignName);
      ((PXSelectBase) instance.Task).Cache.SetValue<PMTask.plannedStartDate>((object) copy, (object) current.StartDate);
      ((PXSelectBase) instance.Task).Cache.SetValue<PMTask.startDate>((object) copy, (object) current.StartDate);
      ((PXSelectBase) instance.Task).Cache.SetValue<PMTask.plannedEndDate>((object) copy, (object) current.EndDate);
      ((PXSelectBase) instance.Task).Cache.SetValue<PMTask.endDate>((object) copy, (object) current.EndDate);
      ((PXSelectBase<PMTask>) instance.Task).Update(copy);
      PXRedirectHelper.TryRedirect((PXGraph) instance, (object) copy, (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  [PXButton(ImageKey = "RecordDel", ImageSet = "main")]
  [PXUIField(DisplayName = "Delete Selected")]
  protected IEnumerable DeleteAction(PXAdapter adapter)
  {
    CRCampaign current = ((PXSelectBase<CRCampaign>) this.Campaign).Current;
    if (current == null || current == null || current.CampaignID == null || ((PXSelectBase) this.Campaign).Cache.GetStatus((object) current) == 2)
      return adapter.Get();
    List<CRCampaignMembers> source = new List<CRCampaignMembers>();
    PXCache cach = ((PXGraph) this).Caches[typeof (CRCampaignMembers)];
    foreach (CRCampaignMembers crCampaignMembers in ((IEnumerable<CRCampaignMembers>) cach.Updated).Concat<CRCampaignMembers>((IEnumerable<CRCampaignMembers>) cach.Inserted))
    {
      if (crCampaignMembers.Selected.GetValueOrDefault())
        source.Add(crCampaignMembers);
    }
    if (!source.Any<CRCampaignMembers>() && cach.Current != null)
      source.Add((CRCampaignMembers) cach.Current);
    foreach (CRCampaignMembers crCampaignMembers in source)
      cach.Delete((object) crCampaignMembers);
    return adapter.Get();
  }

  [PXRemoveBaseAttribute(typeof (PXNavigateSelectorAttribute))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRCampaign.campaignName> e)
  {
  }

  [PXMergeAttributes]
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDBDefault(typeof (CRCampaign.campaignID))]
  [PXUIField(DisplayName = "Campaign ID")]
  [PXParent(typeof (Select<CRCampaign, Where<CRCampaign.campaignID, Equal<Current<CRCampaignMembers.campaignID>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CRCampaignMembers.campaignID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Member Since", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CRCampaignMembers.createdDateTime> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<Contact.displayName> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault("")]
  protected virtual void _(PX.Data.Events.CacheAttached<Contact.contactType> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  [PXSelector(typeof (BAccount.bAccountID), SubstituteKey = typeof (BAccount.acctCD), DescriptionField = typeof (BAccount.acctCD), DirtyRead = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<Contact.bAccountID> e)
  {
  }

  [PXUIField(DisplayName = "Class Description")]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.CR.CROpportunityClass.description> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Business Account Workgroup")]
  protected virtual void _(PX.Data.Events.CacheAttached<BAccount.workgroupID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Business Account Owner")]
  protected virtual void _(PX.Data.Events.CacheAttached<BAccount.ownerID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Business Account Parent Account")]
  protected virtual void _(PX.Data.Events.CacheAttached<BAccount.parentBAccountID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Business Account Source Campaign")]
  protected virtual void _(PX.Data.Events.CacheAttached<BAccount.campaignSourceID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<CRCampaign> e)
  {
    CRCampaign row = e.Row;
    PX.Objects.CR.DAC.Standalone.CRCampaign dacCampaign = PXResultset<PX.Objects.CR.DAC.Standalone.CRCampaign>.op_Implicit(((PXSelectBase<PX.Objects.CR.DAC.Standalone.CRCampaign>) this.CalcCampaignCurrent).Select(Array.Empty<object>()));
    if (row != null && dacCampaign != null && !this.CanBeDeleted(row, dacCampaign))
    {
      e.Cancel = true;
      throw new PXException("The campaign cannot be deleted because one or multiple documents refer to this campaign. Remove all the references and try again.");
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CRCampaign, CRCampaign.status> e)
  {
    CRCampaign row = e.Row;
    PXStringState stateExt = (PXStringState) ((PXCache) GraphHelper.Caches<CRCampaign>((PXGraph) this)).GetStateExt<CRCampaign.status>((object) row);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CRCampaign, CRCampaign.status>, CRCampaign, object>) e).NewValue = (object) ((IEnumerable<string>) stateExt.AllowedValues).FirstOrDefault<string>();
  }

  protected virtual bool CanBeDeleted(CRCampaign campaign, PX.Objects.CR.DAC.Standalone.CRCampaign dacCampaign)
  {
    string[] strArray1 = new string[1]{ "mailsSent" };
    foreach (string str in strArray1)
    {
      object stateExt = ((PXSelectBase) this.CampaignCurrent).Cache.GetStateExt((object) campaign, str);
      if (((PXFieldState) stateExt).Value != null && (int) ((PXFieldState) stateExt).Value > 0)
        return false;
    }
    string[] strArray2 = new string[5]
    {
      "closedOpportunities",
      "contacts",
      "leadsConverted",
      "leadsGenerated",
      "opportunities"
    };
    foreach (string str in strArray2)
    {
      object stateExt = ((PXSelectBase) this.CalcCampaignCurrent).Cache.GetStateExt((object) dacCampaign, str);
      if (((PXFieldState) stateExt).Value != null && (int) ((PXFieldState) stateExt).Value > 0)
        return false;
    }
    int? rowCount = PXSelectBase<PMTask, PXSelectGroupBy<PMTask, Where<PMTask.projectID, Equal<Current<CRCampaign.projectID>>, And<PMTask.taskID, Equal<Current<CRCampaign.projectTaskID>>>>, Aggregate<Count>>.Config>.Select((PXGraph) this, Array.Empty<object>()).RowCount;
    int num = 0;
    return !(rowCount.GetValueOrDefault() > num & rowCount.HasValue);
  }

  protected virtual void _(PX.Data.Events.RowSelected<CRCampaign> e)
  {
    CRCampaign row = e.Row;
    if (row == null)
      return;
    ((PXAction) this.addOpportunity).SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRCampaign>>) e).Cache.GetStatus((object) row) != 2);
    PXCache cache1 = ((PXSelectBase) this.CampaignCurrent).Cache;
    CRCampaign crCampaign = row;
    int? projectId = row.ProjectID;
    int num1 = projectId.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CRCampaign.projectTaskID>(cache1, (object) crCampaign, num1 != 0);
    PXCache cache2 = ((PXSelectBase) this.CampaignCurrent).Cache;
    projectId = row.ProjectID;
    int num2 = projectId.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetRequired<CRCampaign.projectTaskID>(cache2, num2 != 0);
    PXImportAttribute.SetEnabled((PXGraph) this, "CampaignMembers", ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRCampaign>>) e).Cache.GetOriginal((object) e.Row) != null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CRCampaign, CRCampaign.projectID> e)
  {
    CRCampaign row = e.Row;
    if (row == null)
      return;
    ((PXSelectBase) this.CampaignCurrent).Cache.SetValue<CRCampaign.projectTaskID>((object) row, (object) null);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CRCampaign> e)
  {
    CRCampaign row = e.Row;
    if (row == null)
      return;
    if (!row.StartDate.HasValue)
    {
      if (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CRCampaign>>) e).Cache.RaiseExceptionHandling<CRCampaign.startDate>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[startDate]"
      })))
        throw new PXRowPersistingException(typeof (CRCampaign.startDate).Name, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) "startDate"
        });
    }
    if (row.ProjectID.HasValue && !row.ProjectTaskID.HasValue)
    {
      if (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CRCampaign>>) e).Cache.RaiseExceptionHandling<CRCampaign.projectTaskID>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[projectTaskID]"
      })))
        throw new PXRowPersistingException(typeof (CRCampaign.projectTaskID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) "projectTaskID"
        });
    }
    if (!row.ProjectTaskID.HasValue)
      return;
    int? rowCount = PXSelectBase<CRCampaign, PXSelectGroupBy<CRCampaign, Where<CRCampaign.projectID, Equal<Required<CRCampaign.projectID>>, And<CRCampaign.projectTaskID, Equal<Required<CRCampaign.projectTaskID>>, And<CRCampaign.campaignID, NotEqual<Required<CRCampaign.campaignID>>>>>, Aggregate<Count>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) row.ProjectID,
      (object) row.ProjectTaskID,
      (object) row.CampaignID
    }).RowCount;
    int num = 0;
    if (rowCount.GetValueOrDefault() > num & rowCount.HasValue)
      throw new PXRowPersistingException(typeof (CRCampaign.projectTaskID).Name, (object) row.ProjectTaskID, "Task {0} has already been linked to another campaign.", new object[1]
      {
        (object) typeof (CRCampaign.projectTaskID).Name
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CRMarketingListWithLinkToCRCampaign, CRMarketingListWithLinkToCRCampaign.selectedForCampaign> e)
  {
    CRMarketingListWithLinkToCRCampaign row = e.Row;
    if (row == null)
      return;
    PXResult<CRCampaignToCRMarketingListLink> pxResult = ((IQueryable<PXResult<CRCampaignToCRMarketingListLink>>) PXSelectBase<CRCampaignToCRMarketingListLink, PXViewOf<CRCampaignToCRMarketingListLink>.BasedOn<SelectFromBase<CRCampaignToCRMarketingListLink, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRCampaignToCRMarketingListLink.campaignID, Equal<BqlField<CRCampaign.campaignID, IBqlString>.FromCurrent>>>>>.And<BqlOperand<CRCampaignToCRMarketingListLink.marketingListID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.MarketingListID
    })).FirstOrDefault<PXResult<CRCampaignToCRMarketingListLink>>();
    CRCampaignToCRMarketingListLink marketingListLink = pxResult != null ? PXResult<CRCampaignToCRMarketingListLink>.op_Implicit(pxResult) : GraphHelper.InitNewRow<CRCampaignToCRMarketingListLink>(GraphHelper.Caches<CRCampaignToCRMarketingListLink>((PXGraph) this), (CRCampaignToCRMarketingListLink) null);
    marketingListLink.MarketingListID = row.MarketingListID;
    marketingListLink.SelectedForCampaign = e.NewValue as bool?;
    GraphHelper.Caches<CRCampaignToCRMarketingListLink>((PXGraph) this).Update(marketingListLink);
  }

  public virtual void Persist()
  {
    if (((PXCache) GraphHelper.Caches<CRCampaignToCRMarketingListLink>((PXGraph) this)).IsInsertedUpdatedDeleted)
    {
      if (EnumerableExtensions.IsIn<WebDialogResult>(((PXSelectBase) this.CampaignMembers).View.Ask((object) ((PXSelectBase<CRCampaignMembers>) this.CampaignMembers).Current, "Confirmation", "You have updated the set of marketing lists for the campaign. You can update the campaign members or keep the current members.", (MessageButtons) 3, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
      {
        [(WebDialogResult) 6] = "Update",
        [(WebDialogResult) 7] = "Keep",
        [(WebDialogResult) 2] = "Cancel"
      }, (MessageIcon) 0), (WebDialogResult) 6, (WebDialogResult) 7))
      {
        if (((PXSelectBase) this.CampaignMembers).View.Answer == 6)
        {
          this.FixLastUpdateDate();
          this.CampaignUpdateListMembersLongOperation();
        }
      }
      else
      {
        ((PXSelectBase) this.CampaignMembers).View.Answer = (WebDialogResult) 0;
        return;
      }
    }
    ((PXGraph) this).Persist();
  }

  protected virtual void FixLastUpdateDate()
  {
    DateTime now = PXTimeZoneInfo.Now;
    PXCache<CRCampaignToCRMarketingListLink> pxCache = GraphHelper.Caches<CRCampaignToCRMarketingListLink>((PXGraph) this);
    foreach (PXResult pxResult in PXSelectBase<CRCampaignToCRMarketingListLink, PXViewOf<CRCampaignToCRMarketingListLink>.BasedOn<SelectFromBase<CRCampaignToCRMarketingListLink, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRCampaignToCRMarketingListLink.campaignID, IBqlString>.IsEqual<BqlField<CRCampaign.campaignID, IBqlString>.FromCurrent>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      CRCampaignToCRMarketingListLink marketingListLink = pxResult.GetItem<CRCampaignToCRMarketingListLink>();
      bool? selectedForCampaign = (bool?) marketingListLink?.SelectedForCampaign;
      if (selectedForCampaign.HasValue && selectedForCampaign.GetValueOrDefault())
      {
        marketingListLink.LastUpdateDate = new DateTime?(now);
        pxCache.Update(marketingListLink);
      }
      else if (marketingListLink != null && marketingListLink.LastUpdateDate.HasValue)
        pxCache.Delete(marketingListLink);
    }
  }
}
