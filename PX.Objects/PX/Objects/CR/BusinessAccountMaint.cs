// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BusinessAccountMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CR.BusinessAccountMaint_Extensions;
using PX.Objects.CR.DAC;
using PX.Objects.CR.Extensions;
using PX.Objects.CR.Extensions.CRCreateActions;
using PX.Objects.CR.Extensions.CRDuplicateEntities;
using PX.Objects.CR.Extensions.Relational;
using PX.Objects.CR.Standalone;
using PX.Objects.CR.Workflows;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.GDPR;
using PX.Objects.GL;
using PX.Objects.GraphExtensions.ExtendBAccount;
using PX.Objects.SO;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.CR;

public class BusinessAccountMaint : PXGraph<
#nullable disable
BusinessAccountMaint, PX.Objects.CR.BAccount>
{
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> BaseBAccounts;
  [PXHidden]
  public PXSelect<BAccountCRM> BaseBAccountsCRM;
  [PXHidden]
  public PXSetup<PX.Objects.GL.Branch> Branches;
  [PXHidden]
  [PXCheckCurrent]
  public CMSetupSelect CMSetup;
  [PXHidden]
  [PXCheckCurrent]
  public PXSetup<Company> cmpany;
  [PXHidden]
  [PXCheckCurrent]
  public PXSetup<CRSetup> Setup;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Standalone.Location> BaseLocations;
  [PXViewName("Customer")]
  public PXSelectJoin<PX.Objects.CR.BAccount, LeftJoin<Contact, On<Contact.contactID, Equal<PX.Objects.CR.BAccount.defContactID>>>, Where2<Match<Current<AccessInfo.userName>>, And<Where<PX.Objects.CR.BAccount.type, Equal<BAccountType.customerType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.prospectType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.combinedType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.vendorType>>>>>>>> BAccount;
  [PXHidden]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (PX.Objects.CR.BAccount.primaryContactID)})]
  public FbqlSelect<SelectFromBase<PX.Objects.CR.BAccount, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<Contact>.On<BqlOperand<
  #nullable enable
  Contact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.CR.BAccount.defContactID>>>>.Where<BqlOperand<
  #nullable enable
  PX.Objects.CR.BAccount.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.CR.BAccount.bAccountID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  PX.Objects.CR.BAccount>.View CurrentBAccount;
  [PXCopyPasteHiddenView]
  public PXSelect<CRActivityStatistics, Where<CRActivityStatistics.noteID, Equal<Current<PX.Objects.CR.BAccount.noteID>>>> BAccountActivityStatistics;
  [PXHidden]
  public PXSelect<Address> AddressDummy;
  [PXHidden]
  public PXSelect<Contact> ContactDummy;
  [PXViewName("Leads")]
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (Contact))]
  public PXSelectJoin<CRLead, InnerJoin<Address, On<Address.addressID, Equal<Contact.defAddressID>>, LeftJoin<CRActivityStatistics, On<CRActivityStatistics.noteID, Equal<CRLead.noteID>>>>, Where<CRLead.bAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>>, OrderBy<Desc<CRLead.createdDateTime>>> Leads;
  [PXViewName("Answers")]
  public CRAttributeList<PX.Objects.CR.BAccount> Answers;
  [PXHidden]
  public PXSelect<PX.Objects.CR.CROpportunityClass> CROpportunityClass;
  [PXViewName("Opportunities")]
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (PX.Objects.CR.BAccount))]
  [PXViewDetailsButton(typeof (PX.Objects.CR.BAccount), typeof (Select<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<CROpportunity.bAccountID>>>>))]
  [PXViewDetailsButton(typeof (PX.Objects.CR.BAccount), typeof (Select<Contact, Where<Contact.contactID, Equal<Current<CROpportunity.contactID>>>>))]
  public PXSelectJoin<CROpportunity, LeftJoin<Contact, On<Contact.contactID, Equal<CROpportunity.contactID>>, LeftJoin<CROpportunityProbability, On<CROpportunityProbability.stageCode, Equal<CROpportunity.stageID>>, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<CROpportunity.bAccountID>>, LeftJoin<PX.Objects.CR.CROpportunityClass, On<PX.Objects.CR.CROpportunityClass.cROpportunityClassID, Equal<CROpportunity.classID>>, LeftJoin<CRLead, On<CRLead.noteID, Equal<CROpportunity.leadID>>>>>>>, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>, Or<PX.Objects.CR.BAccount.parentBAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>>>> Opportunities;
  [PXHidden]
  public PXSelect<CROpportunity> OpportunityLink;
  [PXHidden]
  public PXSelect<CRQuote> SalesQuoteLink;
  [PXViewName("Cases")]
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (PX.Objects.CR.BAccount))]
  [PXViewDetailsButton(typeof (PX.Objects.CR.BAccount), typeof (Select<Contact, Where<Contact.contactID, Equal<Current<CRCase.contactID>>>>))]
  public PXSelectReadonly2<CRCase, LeftJoin<Contact, On<Contact.contactID, Equal<CRCase.contactID>>>, Where<CRCase.customerID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>>> Cases;
  [PXViewName("Contracts")]
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (PX.Objects.CR.BAccount))]
  [PXViewDetailsButton(typeof (PX.Objects.CR.BAccount), typeof (Select<Location, Where<Location.bAccountID, Equal<Current<PX.Objects.CT.Contract.customerID>>, And<Location.locationID, Equal<Current<PX.Objects.CT.Contract.locationID>>>>>))]
  [PXViewDetailsButton(typeof (PX.Objects.CR.BAccount), typeof (Select<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<PX.Objects.CT.Contract.customerID>>>>))]
  public PXSelectReadonly2<PX.Objects.CT.Contract, LeftJoin<ContractBillingSchedule, On<ContractBillingSchedule.contractID, Equal<PX.Objects.CT.Contract.contractID>>, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PX.Objects.CT.Contract.customerID>>>>, Where<PX.Objects.CT.Contract.baseType, Equal<CTPRType.contract>, And<Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>, Or<ContractBillingSchedule.accountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>>>>>> Contracts;
  [PXViewName("Orders")]
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (PX.Objects.CR.BAccount))]
  [PXViewDetailsButton(typeof (PX.Objects.CR.BAccount), typeof (Select<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Current<PX.Objects.SO.SOOrder.orderNbr>>>>>))]
  public PXSelectReadonly<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.customerID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>>> Orders;
  [PXCopyPasteHiddenView]
  [PXViewName("Campaign Members")]
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (PX.Objects.CR.BAccount), typeof (Select<CRCampaign, Where<CRCampaign.campaignID, Equal<Current<CRCampaignMembers.campaignID>>>>))]
  [PXViewDetailsButton(typeof (CRCampaign), typeof (Select<CRCampaign, Where<CRCampaign.campaignID, Equal<Current<CRCampaignMembers.campaignID>>>>))]
  public FbqlSelect<SelectFromBase<CRCampaignMembers, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CRCampaign>.On<BqlOperand<
  #nullable enable
  CRCampaign.campaignID, IBqlString>.IsEqual<
  #nullable disable
  CRCampaignMembers.campaignID>>>, FbqlJoins.Inner<Contact>.On<BqlOperand<
  #nullable enable
  Contact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  CRCampaignMembers.contactID>>>, FbqlJoins.Left<CRMarketingList>.On<BqlOperand<
  #nullable enable
  CRMarketingList.marketingListID, IBqlInt>.IsEqual<
  #nullable disable
  CRCampaignMembers.marketingListID>>>>.Where<BqlOperand<
  #nullable enable
  Contact.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.CR.BAccount.bAccountID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  CRCampaignMembers>.View Members;
  [PXHidden]
  public PXSelect<CRMarketingListMember> Subscriptions_stub;
  public CRNotificationSourceList<PX.Objects.CR.BAccount, PX.Objects.CR.BAccount.classID, CRNotificationSource.bAccount> NotificationSources;
  public CRNotificationRecipientList<PX.Objects.CR.BAccount, PX.Objects.CR.BAccount.classID> NotificationRecipients;
  public PXMenuAction<PX.Objects.CR.BAccount> Action;
  public PXDBAction<PX.Objects.CR.BAccount> addOpportunity;
  public PXDBAction<PX.Objects.CR.BAccount> addCase;
  public PXChangeBAccountID<PX.Objects.CR.BAccount, PX.Objects.CR.BAccount.acctCD> ChangeID;

  public BusinessAccountMaint()
  {
    if (!((PXSelectBase<PX.Objects.GL.Branch>) this.Branches).Current.BAccountID.HasValue)
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (PX.Objects.GL.Branch), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Company Branches")
      });
    PXUIFieldAttribute.SetDisplayName<PX.Objects.CS.Carrier.description>(((PXGraph) this).Caches[typeof (PX.Objects.CS.Carrier)], "Carrier Description");
    PXUIFieldAttribute.SetRequired<Contact.lastName>(((PXGraph) this).Caches[typeof (Contact)], true);
  }

  [PXUIField(DisplayName = "Create Opportunity")]
  [PXButton]
  public virtual void AddOpportunity()
  {
    PX.Objects.CR.BAccount current = ((PXSelectBase<PX.Objects.CR.BAccount>) this.CurrentBAccount).Current;
    if (current == null || !current.BAccountID.HasValue)
      return;
    OpportunityMaint instance = PXGraph.CreateInstance<OpportunityMaint>();
    CROpportunity destData = ((PXSelectBase<CROpportunity>) instance.Opportunity).Insert();
    destData.BAccountID = current.BAccountID;
    destData.LocationID = current.DefLocationID;
    destData.CuryID = current.CuryID ?? destData.CuryID;
    destData.OverrideSalesTerritory = current.OverrideSalesTerritory;
    bool? overrideSalesTerritory = destData.OverrideSalesTerritory;
    if (overrideSalesTerritory.HasValue && overrideSalesTerritory.GetValueOrDefault())
      destData.SalesTerritoryID = current.SalesTerritoryID;
    if (PXResultset<PX.Objects.CR.CROpportunityClass>.op_Implicit(PXSelectBase<PX.Objects.CR.CROpportunityClass, PXSelect<PX.Objects.CR.CROpportunityClass, Where<PX.Objects.CR.CROpportunityClass.cROpportunityClassID, Equal<Current<CROpportunity.classID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) destData
    }, Array.Empty<object>()))?.DefaultOwner == "S")
    {
      destData.WorkgroupID = current.WorkgroupID;
      destData.OwnerID = current.OwnerID;
    }
    UDFHelper.CopyAttributes(((PXSelectBase) this.CurrentBAccount).Cache, (object) current, ((PXSelectBase) instance.Opportunity).Cache, (object) destData, destData.ClassID);
    CROpportunity row = ((PXSelectBase<CROpportunity>) instance.Opportunity).Update(destData);
    ((PXSelectBase<CROpportunity>) instance.Opportunity).SetValueExt<CROpportunity.bAccountID>(row, (object) current.BAccountID);
    instance.Answers.CopyAllAttributes((object) row, (object) current);
    if (!((PXGraph) this).IsContractBasedAPI)
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
    ((PXAction) instance.Save).Press();
  }

  [PXUIField(DisplayName = "Add Case")]
  [PXButton]
  public virtual void AddCase()
  {
    PX.Objects.CR.BAccount current = ((PXSelectBase<PX.Objects.CR.BAccount>) this.CurrentBAccount).Current;
    if (current == null)
      return;
    int? nullable1 = current.BAccountID;
    if (!nullable1.HasValue)
      return;
    CRCaseMaint instance = PXGraph.CreateInstance<CRCaseMaint>();
    CRCase crCase1 = (CRCase) ((PXSelectBase) instance.Case).Cache.Insert();
    crCase1.CustomerID = current.BAccountID;
    crCase1.LocationID = current.DefLocationID;
    CRCase crCase2 = crCase1;
    nullable1 = current.PrimaryContactID;
    int num = 0;
    int? nullable2;
    if (!(nullable1.GetValueOrDefault() > num & nullable1.HasValue))
    {
      nullable1 = new int?();
      nullable2 = nullable1;
    }
    else
      nullable2 = current.PrimaryContactID;
    crCase2.ContactID = nullable2;
    UDFHelper.CopyAttributes(((PXSelectBase) this.CurrentBAccount).Cache, (object) current, ((PXSelectBase) instance.Case).Cache, ((PXSelectBase) instance.Case).Cache.Current, crCase1.CaseClassID);
    CRCase row = ((PXSelectBase<CRCase>) instance.Case).Update(crCase1);
    instance.Answers.CopyAllAttributes((object) row, (object) current);
    if (!((PXGraph) this).IsContractBasedAPI)
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
    ((PXAction) instance.Save).Press();
  }

  [PXSelector(typeof (Search<NotificationSetup.setupID, Where<NotificationSetup.sourceCD, Equal<CRNotificationSource.bAccount>>>))]
  [PXMergeAttributes]
  protected virtual void NotificationSource_SetupID_CacheAttached(PXCache sender)
  {
  }

  [PXCheckUnique(new System.Type[] {typeof (NotificationSource.setupID)}, IgnoreNulls = false, Where = typeof (Where<NotificationSource.refNoteID, Equal<Current<NotificationSource.refNoteID>>>))]
  [PXMergeAttributes]
  protected virtual void NotificationSource_NBranchID_CacheAttached(PXCache sender)
  {
  }

  [PXSelector(typeof (Search<SiteMap.screenID, Where<SiteMap.url, Like<urlReports>, And<SiteMap.screenID, Like<PXModule.cr_>>>, OrderBy<Asc<SiteMap.screenID>>>), new System.Type[] {typeof (SiteMap.screenID), typeof (SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (SiteMap.title))]
  [PXMergeAttributes]
  protected virtual void NotificationSource_ReportID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [CRMContactType.List]
  [PXCheckDistinct(new System.Type[] {typeof (NotificationRecipient.contactID)}, Where = typeof (Where<NotificationRecipient.sourceID, Equal<Current<NotificationRecipient.sourceID>>, And<NotificationRecipient.refNoteID, Equal<Current<PX.Objects.CR.BAccount.noteID>>>>))]
  [PXMergeAttributes]
  protected virtual void NotificationRecipient_ContactType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(10, IsUnicode = true)]
  [PXMergeAttributes]
  protected virtual void NotificationRecipient_ClassID_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "Class Description")]
  [PXMergeAttributes]
  protected virtual void CROpportunityClass_Description_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Marketing List")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CRMarketingList.mailListCode> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelecting<NotificationRecipient> e)
  {
    using (new PXConnectionScope())
      this.CalculateNotificationRecipientEmail(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<NotificationRecipient>>) e).Cache, e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<NotificationRecipient, NotificationRecipient.contactID> e)
  {
    this.CalculateNotificationRecipientEmail(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<NotificationRecipient, NotificationRecipient.contactID>>) e).Cache, e.Row);
  }

  protected virtual void CalculateNotificationRecipientEmail(
    PXCache cache,
    NotificationRecipient row)
  {
    if (row == null)
      return;
    if (!(PXSelectorAttribute.Select<NotificationRecipient.contactID>(cache, (object) row) is Contact contact))
    {
      switch (row.ContactType)
      {
        case "P":
          contact = PXResultset<Contact>.op_Implicit(((PXSelectBase<Contact>) ((PXGraph) this).GetExtension<BusinessAccountMaint.DefContactAddressExt>().DefContact).SelectWindowed(0, 1, Array.Empty<object>()));
          break;
        case "S":
          contact = ((PXSelectBase) ((PXGraph) this).GetExtension<BusinessAccountMaint.DefLocationExt>().DefLocationContact).View.SelectSingle(new object[1]
          {
            ((PXSelectBase) this.BAccount).Cache.Current
          }) as Contact;
          break;
      }
    }
    if (contact == null)
      return;
    row.Email = contact.EMail;
  }

  [SOOrderStatus.ListWithoutOrders]
  [PXMergeAttributes]
  protected virtual void SOOrder_Status_CacheAttached(PXCache sender)
  {
  }

  [PXDimensionSelector("BIZACCT", typeof (Search2<PX.Objects.CR.BAccount.acctCD, LeftJoin<Contact, On<Contact.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>, And<Contact.contactID, Equal<PX.Objects.CR.BAccount.defContactID>>>, LeftJoin<Address, On<Address.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>, And<Address.addressID, Equal<PX.Objects.CR.BAccount.defAddressID>>>>>, Where2<Where<PX.Objects.CR.BAccount.type, Equal<BAccountType.customerType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.prospectType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.combinedType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.vendorType>>>>>, And<Match<Current<AccessInfo.userName>>>>>), typeof (PX.Objects.CR.BAccount.acctCD), new System.Type[] {typeof (PX.Objects.CR.BAccount.acctCD), typeof (PX.Objects.CR.BAccount.acctName), typeof (PX.Objects.CR.BAccount.type), typeof (PX.Objects.CR.BAccount.classID), typeof (PX.Objects.CR.BAccount.status), typeof (Contact.phone1), typeof (Address.city), typeof (Address.countryID), typeof (Contact.eMail)}, DescriptionField = typeof (PX.Objects.CR.BAccount.acctName))]
  [PXUIField]
  [PXMergeAttributes]
  protected virtual void BAccount_AcctCD_CacheAttached(PXCache cache)
  {
  }

  [CRMParentBAccount(typeof (PX.Objects.CR.BAccount.bAccountID), null, null, null, null)]
  [PXMergeAttributes]
  public virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.BAccount.parentBAccountID> e)
  {
  }

  [PXUIField(DisplayName = "Currency")]
  [PXMergeAttributes]
  public virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.BAccount.curyID> e)
  {
  }

  [Obsolete]
  protected virtual void BAccount_ClassID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
  }

  protected virtual void BAccount_Type_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) "PR";
  }

  protected virtual void BAccount_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    PX.Objects.CR.BAccount row = (PX.Objects.CR.BAccount) e.Row;
    if (row == null)
      return;
    bool flag1 = cache.GetStatus((object) row) != 2;
    ((PXSelectBase) this.Opportunities).Cache.AllowInsert = flag1;
    ((PXSelectBase) this.Cases).Cache.AllowInsert = flag1;
    ((PXSelectBase) this.Members).Cache.AllowInsert = flag1;
    bool flag2 = row.Type == "CU" || row.Type == "VC";
    bool flag3 = row.Type == "VE" || row.Type == "VC";
    bool flag4 = (row.Type == "CU" ? 1 : (row.Type == "PR" ? 1 : 0)) != 0 || row.Type == "VC";
    ((PXAction) this.addOpportunity).SetEnabled(flag1 & flag4);
    ((PXAction) this.addCase).SetEnabled(flag1 && flag4 | flag3);
    ((PXAction) this.ChangeID).SetEnabled(!row.IsBranch.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.BAccount.parentBAccountID>(cache, (object) row, !flag2 || !PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>());
    PXStringListAttribute.SetList<PX.Objects.CR.BAccount.status>(cache, (object) row, flag2 ? (PXStringListAttribute) new CustomerStatus.ListAttribute() : (PXStringListAttribute) new CustomerStatus.BusinessAccountListAttribute());
  }

  protected virtual void BAccount_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.BAccount row) || !(row.Type == "CU") && !(row.Type == "VC"))
      return;
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXViewOf<PX.Objects.AR.Customer>.BasedOn<SelectFromBase<PX.Objects.AR.Customer, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.AR.Customer.acctCD, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.AcctCD
    }));
    CustomerMaint.VerifyParentBAccountID<PX.Objects.CR.BAccount.parentBAccountID>((PXGraph) this, cache, customer, row);
  }

  [PXDefault("AP")]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<Contact.contactType> e)
  {
  }

  [PXUIField]
  [PXDBDefault(typeof (PX.Objects.CR.BAccount.bAccountID))]
  [PXSelector(typeof (PX.Objects.CR.BAccount.bAccountID), SubstituteKey = typeof (PX.Objects.CR.BAccount.acctCD), DirtyRead = true)]
  [PXMergeAttributes]
  public virtual void _(PX.Data.Events.CacheAttached<CRLead.bAccountID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Source Lead Contact ID")]
  public virtual void _(PX.Data.Events.CacheAttached<CRLead.contactID> e)
  {
  }

  [PXDBInt(BqlField = typeof (CROpportunityRevision.contactID))]
  [PXUIField(DisplayName = "Main Contact ID", Visible = false)]
  public virtual void _(PX.Data.Events.CacheAttached<CROpportunity.contactID> e)
  {
  }

  [PXUIField]
  [PXDBDefault(typeof (PX.Objects.CR.BAccount.bAccountID))]
  [PXSelector(typeof (PX.Objects.CR.BAccount.bAccountID), SubstituteKey = typeof (PX.Objects.CR.BAccount.acctCD), DirtyRead = true)]
  [PXMergeAttributes]
  public virtual void _(PX.Data.Events.CacheAttached<Contact.bAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  public virtual void _(PX.Data.Events.CacheAttached<Contact.memberName> e)
  {
  }

  [PXDBInt(BqlField = typeof (CROpportunityRevision.bAccountID))]
  [PXUIField]
  [PXDBDefault(typeof (PX.Objects.CR.BAccount.bAccountID))]
  [PXSelector(typeof (PX.Objects.CR.BAccount.bAccountID), SubstituteKey = typeof (PX.Objects.CR.BAccount.acctCD), DirtyRead = true)]
  [PXMergeAttributes]
  public virtual void _(PX.Data.Events.CacheAttached<CROpportunity.bAccountID> e)
  {
  }

  [PXUIField(DisplayName = "Location")]
  [PXDBDefault(typeof (PX.Objects.CR.Standalone.Location.locationID))]
  [LocationActive(typeof (Where<Location.bAccountID, Equal<Current<CROpportunity.bAccountID>>>), DisplayName = "Location", DescriptionField = typeof (Location.descr), BqlField = typeof (CROpportunityRevision.locationID), ValidateValue = false)]
  [PXMergeAttributes]
  public virtual void _(PX.Data.Events.CacheAttached<CROpportunity.locationID> e)
  {
  }

  [PXDBInt(BqlField = typeof (CROpportunityRevision.bAccountID))]
  [PXUIField]
  [PXDBDefault(typeof (PX.Objects.CR.BAccount.bAccountID))]
  [PXSelector(typeof (PX.Objects.CR.BAccount.bAccountID), SubstituteKey = typeof (PX.Objects.CR.BAccount.acctCD), DirtyRead = true)]
  [PXMergeAttributes]
  public virtual void _(PX.Data.Events.CacheAttached<CRQuote.bAccountID> e)
  {
  }

  [PXUIField(DisplayName = "Location")]
  [PXDBDefault(typeof (PX.Objects.CR.Standalone.Location.locationID))]
  [LocationActive(typeof (Where<Location.bAccountID, Equal<Current<CRQuote.bAccountID>>>), DisplayName = "Location", DescriptionField = typeof (Location.descr), BqlField = typeof (CROpportunityRevision.locationID), ValidateValue = false)]
  [PXMergeAttributes]
  public virtual void _(PX.Data.Events.CacheAttached<CRQuote.locationID> e)
  {
  }

  [PXUIField]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRLead.memberName> e)
  {
  }

  [PXDBDefault(typeof (PX.Objects.CR.BAccount.bAccountID))]
  [PXParent(typeof (Select<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<Address.bAccountID>>>>))]
  [PXMergeAttributes]
  protected virtual void Address_BAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.CR.BAccount.defContactID))]
  [PXMergeAttributes]
  protected virtual void CRCampaignMembers_ContactID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCheckUnique(new System.Type[] {typeof (CRCampaignMembers.contactID)})]
  [PXUIField(DisplayName = "Campaign")]
  protected virtual void CRCampaignMembers_CampaignID_CacheAttached(PXCache sender)
  {
  }

  /// <exclude />
  public class LastNameOrCompanyNameRequiredGraphExt : PXGraphExtension<BusinessAccountMaint>
  {
  }

  /// <exclude />
  public class DefContactAddressExt : 
    PX.Objects.CR.Extensions.DefContactAddressExt<BusinessAccountMaint, PX.Objects.CR.BAccount, PX.Objects.CR.BAccount.acctName>
  {
  }

  /// <exclude />
  public class DefLocationExt : 
    PX.Objects.CR.Extensions.DefLocationExt<BusinessAccountMaint, BusinessAccountMaint.DefContactAddressExt, BusinessAccountMaint.LocationDetailsExt, PX.Objects.CR.BAccount, PX.Objects.CR.BAccount.bAccountID, PX.Objects.CR.BAccount.defLocationID>.WithUIExtension
  {
    [PXHidden]
    public PXSelectJoin<Location, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.defLocationID, Equal<Location.locationID>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>> BranchLocation;

    public override void Initialize() => base.Initialize();

    public override List<System.Type> InitLocationFields
    {
      get
      {
        return new List<System.Type>()
        {
          typeof (PX.Objects.CR.Standalone.Location.vTaxCalcMode),
          typeof (PX.Objects.CR.Standalone.Location.vRetainageAcctID),
          typeof (PX.Objects.CR.Standalone.Location.vRetainageSubID)
        };
      }
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cShipComplete> e)
    {
      PX.Objects.CR.BAccount current = ((PXSelectBase<PX.Objects.CR.BAccount>) this.Base.BAccount).Current;
      if (e.Row == null || current == null)
        return;
      if (current.IsBranch.GetValueOrDefault())
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cShipComplete>, PX.Objects.CR.Standalone.Location, object>) e).NewValue = (object) e.Row.CShipComplete;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cShipComplete>>) e).Cancel = true;
      }
      else
      {
        Location sourceObject = ((PXSelectBase<Location>) this.BranchLocation).SelectSingle(new object[1]
        {
          (object) ((PXGraph) this.Base).Accessinfo.BranchID
        });
        this.DefaultFrom<Location.cShipComplete>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cShipComplete>>) e).Args, ((PXSelectBase) this.BranchLocation).Cache, (object) sourceObject, nullValue: (object) "L");
      }
    }

    [PXMergeAttributes]
    [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
    protected virtual void _(PX.Data.Events.CacheAttached<Address.latitude> e)
    {
    }

    [PXMergeAttributes]
    [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
    protected virtual void _(PX.Data.Events.CacheAttached<Address.longitude> e)
    {
    }
  }

  /// <exclude />
  public class ContactDetailsExt : 
    BusinessAccountContactDetailsExt<BusinessAccountMaint, BusinessAccountMaint.CreateContactFromAccountGraphExt, PX.Objects.CR.BAccount, PX.Objects.CR.BAccount.bAccountID, PX.Objects.CR.BAccount.acctName>
  {
  }

  /// <exclude />
  public class LocationDetailsExt : 
    PX.Objects.CR.Extensions.LocationDetailsExt<BusinessAccountMaint, PX.Objects.CR.BAccount, PX.Objects.CR.BAccount.bAccountID>
  {
  }

  /// <exclude />
  public class PrimaryContactGraphExt : 
    CRPrimaryContactGraphExt<BusinessAccountMaint, BusinessAccountMaint.ContactDetailsExt, PX.Objects.CR.BAccount, PX.Objects.CR.BAccount.bAccountID, PX.Objects.CR.BAccount.primaryContactID>
  {
    protected override PXView ContactsView
    {
      get => ((PXSelectBase) this.ContactDetailsExtension.Contacts).View;
    }

    [PXUIField(DisplayName = "Name")]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.BAccount.primaryContactID> e)
    {
    }

    protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.CR.BAccount, PX.Objects.CR.BAccount.acctName> e)
    {
      PX.Objects.CR.BAccount row = e.Row;
      if (!row.PrimaryContactID.HasValue)
        return;
      Contact contact = ((PXSelectBase<Contact>) this.PrimaryContactCurrent).SelectSingle(Array.Empty<object>());
      if (contact == null || row.AcctName == null || row.AcctName.Equals(contact.FullName))
        return;
      contact.FullName = row.AcctName;
      ((PXSelectBase<Contact>) this.PrimaryContactCurrent).Update(contact);
    }
  }

  /// <exclude />
  public class BusinessAccountMaintAddressLookupExtension : 
    AddressLookupExtension<BusinessAccountMaint, PX.Objects.CR.BAccount, Address>
  {
    protected override string AddressView => "DefAddress";

    protected override string ViewOnMap => "ViewMainOnMap";
  }

  /// <exclude />
  public class BusinessAccountMaintDefLocationAddressLookupExtension : 
    AddressLookupExtension<BusinessAccountMaint, PX.Objects.CR.BAccount, Address>
  {
    protected override string AddressView => "DefLocationAddress";

    protected override string ViewOnMap => "ViewDefLocationAddressOnMap";
  }

  /// <exclude />
  public class ExtendToCustomer : ExtendToCustomerGraph<BusinessAccountMaint, PX.Objects.CR.BAccount>
  {
    protected override ExtendToCustomerGraph<BusinessAccountMaint, PX.Objects.CR.BAccount>.SourceAccountMapping GetSourceAccountMapping()
    {
      return new ExtendToCustomerGraph<BusinessAccountMaint, PX.Objects.CR.BAccount>.SourceAccountMapping(typeof (PX.Objects.CR.BAccount));
    }

    protected override void _(PX.Data.Events.RowSelected<PX.Objects.CR.BAccount> e)
    {
      base._(e);
      ((PXAction) this.viewCustomer).SetVisible(((PXAction) this.viewCustomer).GetEnabled());
      ((PXAction) this.extendToCustomer).SetVisible(((PXAction) this.extendToCustomer).GetEnabled());
    }
  }

  /// <exclude />
  public class ExtendToVendor : ExtendToVendorGraph<BusinessAccountMaint, PX.Objects.CR.BAccount>
  {
    protected override ExtendToVendorGraph<BusinessAccountMaint, PX.Objects.CR.BAccount>.SourceAccountMapping GetSourceAccountMapping()
    {
      return new ExtendToVendorGraph<BusinessAccountMaint, PX.Objects.CR.BAccount>.SourceAccountMapping(typeof (PX.Objects.CR.BAccount));
    }

    protected override void _(PX.Data.Events.RowSelected<PX.Objects.CR.BAccount> e)
    {
      base._(e);
      ((PXAction) this.viewVendor).SetVisible(((PXAction) this.viewVendor).GetEnabled());
      ((PXAction) this.extendToVendor).SetVisible(((PXAction) this.extendToVendor).GetEnabled());
    }
  }

  /// <exclude />
  public class DefaultAccountOwner : 
    CRDefaultDocumentOwner<BusinessAccountMaint, PX.Objects.CR.BAccount, PX.Objects.CR.BAccount.classID, PX.Objects.CR.BAccount.ownerID, PX.Objects.CR.BAccount.workgroupID>
  {
  }

  /// <exclude />
  public class CRDuplicateEntitiesForBAccountGraphExt : 
    PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<BusinessAccountMaint, PX.Objects.CR.BAccount>
  {
    public override System.Type AdditionalConditions
    {
      get
      {
        return typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<DuplicateContact.contactType, Equal<ContactTypesAttribute.bAccountProperty>>>>>.And<BqlOperand<BAccountR.status, IBqlString>.IsNotEqual<CustomerStatus.inactive>>);
      }
    }

    public override string WarningMessage => "This business account probably has duplicates";

    public static bool IsActive()
    {
      return PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<BusinessAccountMaint, PX.Objects.CR.BAccount>.IsExtensionActive();
    }

    public override void Initialize()
    {
      base.Initialize();
      this.DuplicateDocuments = new PXSelectExtension<DuplicateDocument>((PXSelectBase) ((PXGraph) this.Base).GetExtension<BusinessAccountMaint.DefContactAddressExt>().DefContact);
    }

    protected override PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<BusinessAccountMaint, PX.Objects.CR.BAccount>.DocumentMapping GetDocumentMapping()
    {
      return new PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<BusinessAccountMaint, PX.Objects.CR.BAccount>.DocumentMapping(typeof (PX.Objects.CR.BAccount))
      {
        Key = typeof (PX.Objects.CR.BAccount.bAccountID)
      };
    }

    protected override PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<BusinessAccountMaint, PX.Objects.CR.BAccount>.DuplicateDocumentMapping GetDuplicateDocumentMapping()
    {
      return new PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<BusinessAccountMaint, PX.Objects.CR.BAccount>.DuplicateDocumentMapping(typeof (Contact))
      {
        Email = typeof (Contact.eMail)
      };
    }

    protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.CR.BAccount, PX.Objects.CR.BAccount.status> e)
    {
      PX.Objects.CR.BAccount row = e.Row;
      if (e.Row == null || !(row.Status != "I") || !(row.Status != (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.CR.BAccount, PX.Objects.CR.BAccount.status>, PX.Objects.CR.BAccount, object>) e).OldValue))
        return;
      ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).SetValueExt<DuplicateDocument.duplicateStatus>(((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).Current, (object) "NV");
    }

    protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.CR.Extensions.CRDuplicateEntities.Document> e)
    {
      if (e.Row == null)
        return;
      ((PXAction) this.DuplicateAttach).SetVisible(false);
    }

    protected override void _(PX.Data.Events.RowSelected<CRDuplicateRecord> e)
    {
      base._(e);
      if (e.Row == null || e.Row.CanBeMerged.GetValueOrDefault())
        return;
      ((PXSelectBase) this.DuplicatesForMerging).Cache.RaiseExceptionHandling<CRDuplicateRecord.canBeMerged>((object) e.Row, (object) e.Row.CanBeMerged, (Exception) new PXSetPropertyException("The duplicate business accounts cannot be merged because they have the Customer type.", (PXErrorLevel) 3));
    }

    [PXUIField(DisplayName = "Mark as Validated")]
    [PXButton]
    public override IEnumerable markAsValidated(PXAdapter adapter)
    {
      base.markAsValidated(adapter);
      foreach (PXResult<PX.Objects.CR.BAccount, Contact> pxResult in adapter.Get())
      {
        PX.Objects.CR.BAccount baccount = PXResult<PX.Objects.CR.BAccount, Contact>.op_Implicit(pxResult);
        BusinessAccountMaint.DefContactAddressExt extension = ((PXGraph) this.Base).GetExtension<BusinessAccountMaint.DefContactAddressExt>();
        if (((PXSelectBase) extension.DefContact).View.SelectSingleBound(new object[1]
        {
          (object) baccount
        }, Array.Empty<object>()) is Contact contact)
        {
          Contact copy = (Contact) ((PXSelectBase) extension.DefContact).Cache.CreateCopy((object) contact);
          copy.DuplicateStatus = "VA";
          copy.DuplicateFound = new bool?(false);
          ((PXSelectBase<Contact>) extension.DefContact).Update(copy);
          if (((PXGraph) this.Base).IsContractBasedAPI)
            ((PXAction) this.Base.Save).Press();
        }
      }
      return adapter.Get();
    }

    public override PX.Objects.CR.BAccount GetTargetEntity(int targetID)
    {
      return PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) targetID
      }));
    }

    public override Contact GetTargetContact(PX.Objects.CR.BAccount targetEntity)
    {
      return PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelect<Contact, Where<Contact.contactID, Equal<Required<PX.Objects.CR.BAccount.defContactID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) targetEntity.DefContactID
      }));
    }

    public override Address GetTargetAddress(PX.Objects.CR.BAccount targetEntity)
    {
      return PXResultset<Address>.op_Implicit(PXSelectBase<Address, PXSelect<Address, Where<Address.addressID, Equal<Required<PX.Objects.CR.BAccount.defAddressID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) targetEntity.DefAddressID
      }));
    }

    public override bool CheckIsActive()
    {
      PX.Objects.CR.BAccount current = ((PXSelectBase<PX.Objects.CR.BAccount>) this.Base.BAccount).Current;
      return current != null && current.Status != "I";
    }

    protected override bool WhereMergingMet(CRDuplicateResult result) => true;

    protected override bool CanBeMerged(CRDuplicateResult result)
    {
      return ((PXSelectBase<PX.Objects.CR.BAccount>) this.Base.BAccount).Current.Type == "PR" || ((PXResult) result).GetItem<BAccountR>()?.Type == "PR";
    }

    public override void DoDuplicateAttach(DuplicateDocument duplicateDocument)
    {
    }

    public override PXResult<Contact> GetGramContext(DuplicateDocument duplicateDocument)
    {
      return (PXResult<Contact>) new PXResult<Contact, Address, PX.Objects.CR.Standalone.Location, PX.Objects.CR.BAccount>(duplicateDocument.Base as Contact, ((PXSelectBase<Address>) ((PXGraph) this.Base).GetExtension<BusinessAccountMaint.DefContactAddressExt>().DefAddress).SelectSingle(Array.Empty<object>()), ((PXSelectBase<PX.Objects.CR.Standalone.Location>) ((PXGraph) this.Base).GetExtension<BusinessAccountMaint.DefLocationExt>().DefLocation).SelectSingle(Array.Empty<object>()), ((PXSelectBase<PX.Objects.CR.BAccount>) this.Base.BAccount).Current);
    }

    [PXUIField]
    [PXButton(DisplayOnMainToolbar = false)]
    public override IEnumerable checkForDuplicates(PXAdapter adapter)
    {
      ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).Current = ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).SelectSingle(Array.Empty<object>());
      return base.checkForDuplicates(adapter);
    }

    public class Workflow : 
      PXGraphExtension<BusinessAccountMaint.CRDuplicateEntitiesForBAccountGraphExt, BusinessAccountWorkflow, BusinessAccountMaint>
    {
      public static bool IsActive()
      {
        return PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<BusinessAccountMaint, PX.Objects.CR.BAccount>.IsExtensionActive();
      }

      public virtual void Configure(PXScreenConfiguration configuration)
      {
        BusinessAccountMaint.CRDuplicateEntitiesForBAccountGraphExt.Workflow.Configure(configuration.GetScreenConfigurationContext<BusinessAccountMaint, PX.Objects.CR.BAccount>());
      }

      protected static void Configure(
        WorkflowContext<BusinessAccountMaint, PX.Objects.CR.BAccount> context)
      {
        var conditions = new
        {
          IsCloseAsDuplicateDisabled = Bql<BqlOperand<PX.Objects.AR.Customer.status, IBqlString>.IsNotIn<CustomerStatus.prospect, CustomerStatus.active, CustomerStatus.hold, CustomerStatus.creditHold, CustomerStatus.oneTime>>()
        }.AutoNameConditions();
        context.UpdateScreenConfigurationFor((Func<BoundedTo<BusinessAccountMaint, PX.Objects.CR.BAccount>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<BusinessAccountMaint, PX.Objects.CR.BAccount>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((System.Action<BoundedTo<BusinessAccountMaint, PX.Objects.CR.BAccount>.ActionDefinition.ContainerAdjusterActions>) (actions =>
        {
          actions.Add<BusinessAccountMaint.CRDuplicateEntitiesForBAccountGraphExt>((Expression<Func<BusinessAccountMaint.CRDuplicateEntitiesForBAccountGraphExt, PXAction<PX.Objects.CR.BAccount>>>) (e => e.CheckForDuplicates), (Func<BoundedTo<BusinessAccountMaint, PX.Objects.CR.BAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<BusinessAccountMaint, PX.Objects.CR.BAccount>.ActionDefinition.IConfigured>) (a => (BoundedTo<BusinessAccountMaint, PX.Objects.CR.BAccount>.ActionDefinition.IConfigured) a.WithCategory(context.Categories.Get("Validation"))));
          actions.Add<BusinessAccountMaint.CRDuplicateEntitiesForBAccountGraphExt>((Expression<Func<BusinessAccountMaint.CRDuplicateEntitiesForBAccountGraphExt, PXAction<PX.Objects.CR.BAccount>>>) (e => e.MarkAsValidated), (Func<BoundedTo<BusinessAccountMaint, PX.Objects.CR.BAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<BusinessAccountMaint, PX.Objects.CR.BAccount>.ActionDefinition.IConfigured>) (a => (BoundedTo<BusinessAccountMaint, PX.Objects.CR.BAccount>.ActionDefinition.IConfigured) a.WithCategory(context.Categories.Get("Validation"))));
          actions.Add<BusinessAccountMaint.CRDuplicateEntitiesForBAccountGraphExt>((Expression<Func<BusinessAccountMaint.CRDuplicateEntitiesForBAccountGraphExt, PXAction<PX.Objects.CR.BAccount>>>) (e => e.CloseAsDuplicate), (Func<BoundedTo<BusinessAccountMaint, PX.Objects.CR.BAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<BusinessAccountMaint, PX.Objects.CR.BAccount>.ActionDefinition.IConfigured>) (a => (BoundedTo<BusinessAccountMaint, PX.Objects.CR.BAccount>.ActionDefinition.IConfigured) a.WithCategory(context.Categories.Get("Validation")).IsDisabledWhen((BoundedTo<BusinessAccountMaint, PX.Objects.CR.BAccount>.ISharedCondition) conditions.IsCloseAsDuplicateDisabled)));
        }))));

        BoundedTo<BusinessAccountMaint, PX.Objects.CR.BAccount>.Condition Bql<T>() where T : IBqlUnary, new()
        {
          return context.Conditions.FromBql<T>();
        }
      }
    }
  }

  /// <exclude />
  public class BAccountLocationSharedContactOverrideGraphExt : 
    SharedChildOverrideGraphExt<BusinessAccountMaint, BusinessAccountMaint.BAccountLocationSharedContactOverrideGraphExt>
  {
    public override bool ViewHasADelegate => true;

    protected override CRParentChild<BusinessAccountMaint, BusinessAccountMaint.BAccountLocationSharedContactOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<BusinessAccountMaint, BusinessAccountMaint.BAccountLocationSharedContactOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Standalone.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Standalone.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Standalone.Location.defContactID),
        IsOverrideRelated = typeof (PX.Objects.CR.Standalone.Location.overrideContact)
      };
    }

    protected override SharedChildOverrideGraphExt<BusinessAccountMaint, BusinessAccountMaint.BAccountLocationSharedContactOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<BusinessAccountMaint, BusinessAccountMaint.BAccountLocationSharedContactOverrideGraphExt>.RelatedMapping(typeof (PX.Objects.CR.BAccount))
      {
        RelatedID = typeof (PX.Objects.CR.BAccount.bAccountID),
        ChildID = typeof (PX.Objects.CR.BAccount.defContactID)
      };
    }

    protected override CRParentChild<BusinessAccountMaint, BusinessAccountMaint.BAccountLocationSharedContactOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<BusinessAccountMaint, BusinessAccountMaint.BAccountLocationSharedContactOverrideGraphExt>.ChildMapping(typeof (Contact))
      {
        ChildID = typeof (Contact.contactID),
        RelatedID = typeof (Contact.bAccountID)
      };
    }
  }

  /// <exclude />
  public class BAccountLocationSharedAddressOverrideGraphExt : 
    SharedChildOverrideGraphExt<BusinessAccountMaint, BusinessAccountMaint.BAccountLocationSharedAddressOverrideGraphExt>
  {
    public override bool ViewHasADelegate => true;

    protected override CRParentChild<BusinessAccountMaint, BusinessAccountMaint.BAccountLocationSharedAddressOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<BusinessAccountMaint, BusinessAccountMaint.BAccountLocationSharedAddressOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Standalone.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Standalone.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Standalone.Location.defAddressID),
        IsOverrideRelated = typeof (PX.Objects.CR.Standalone.Location.overrideAddress)
      };
    }

    protected override SharedChildOverrideGraphExt<BusinessAccountMaint, BusinessAccountMaint.BAccountLocationSharedAddressOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<BusinessAccountMaint, BusinessAccountMaint.BAccountLocationSharedAddressOverrideGraphExt>.RelatedMapping(typeof (PX.Objects.CR.BAccount))
      {
        RelatedID = typeof (PX.Objects.CR.BAccount.bAccountID),
        ChildID = typeof (PX.Objects.CR.BAccount.defAddressID)
      };
    }

    protected override CRParentChild<BusinessAccountMaint, BusinessAccountMaint.BAccountLocationSharedAddressOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<BusinessAccountMaint, BusinessAccountMaint.BAccountLocationSharedAddressOverrideGraphExt>.ChildMapping(typeof (Address))
      {
        ChildID = typeof (Address.addressID),
        RelatedID = typeof (Address.bAccountID)
      };
    }
  }

  /// <exclude />
  public class CreateLeadFromAccountGraphExt : CRCreateLeadAction<BusinessAccountMaint, PX.Objects.CR.BAccount>
  {
    public virtual void Initialize()
    {
      ((PXGraphExtension) this).Initialize();
      this.Addresses = new PXSelectExtension<DocumentAddress>((PXSelectBase) ((PXGraph) this.Base).GetExtension<BusinessAccountMaint.DefContactAddressExt>().DefAddress);
      this.Contacts = new PXSelectExtension<DocumentContact>((PXSelectBase) ((PXGraph) this.Base).GetExtension<BusinessAccountMaint.PrimaryContactGraphExt>().PrimaryContactCurrent);
    }

    protected override CRCreateActionBaseInit<BusinessAccountMaint, PX.Objects.CR.BAccount>.DocumentMapping GetDocumentMapping()
    {
      return new CRCreateActionBaseInit<BusinessAccountMaint, PX.Objects.CR.BAccount>.DocumentMapping(typeof (PX.Objects.CR.BAccount))
      {
        ContactID = typeof (PX.Objects.CR.BAccount.primaryContactID)
      };
    }

    protected override CRCreateActionBaseInit<BusinessAccountMaint, PX.Objects.CR.BAccount>.DocumentContactMapping GetDocumentContactMapping()
    {
      return new CRCreateActionBaseInit<BusinessAccountMaint, PX.Objects.CR.BAccount>.DocumentContactMapping(typeof (Contact))
      {
        Email = typeof (Contact.eMail)
      };
    }

    protected override CRCreateActionBaseInit<BusinessAccountMaint, PX.Objects.CR.BAccount>.DocumentAddressMapping GetDocumentAddressMapping()
    {
      return new CRCreateActionBaseInit<BusinessAccountMaint, PX.Objects.CR.BAccount>.DocumentAddressMapping(typeof (Address));
    }

    [PXUIField]
    [PXButton]
    public override void createLead()
    {
      if (((PXGraph) this.Base).IsDirty)
        ((PXGraph) this.Base).Actions.PressSave();
      PX.Objects.CR.Extensions.CRCreateActions.Document current = ((PXSelectBase<PX.Objects.CR.Extensions.CRCreateActions.Document>) this.Documents).Current;
      if (current == null)
        return;
      LeadMaint instance = PXGraph.CreateInstance<LeadMaint>();
      CRLead destData = ((PXSelectBase<CRLead>) instance.Lead).Insert();
      destData.BAccountID = current.BAccountID;
      CRLead crLead1 = destData;
      CRLead crLead2 = destData;
      bool? nullable1 = new bool?(true);
      bool? nullable2 = nullable1;
      crLead2.OverrideAddress = nullable2;
      bool? nullable3 = nullable1;
      crLead1.OverrideRefContact = nullable3;
      destData.FullName = ((PXSelectBase<PX.Objects.CR.BAccount>) this.Base.CurrentBAccount).Current.AcctName;
      destData.OverrideSalesTerritory = current.OverrideSalesTerritory;
      bool? overrideSalesTerritory = destData.OverrideSalesTerritory;
      if (overrideSalesTerritory.HasValue && overrideSalesTerritory.GetValueOrDefault())
        destData.SalesTerritoryID = current.SalesTerritoryID;
      if (PXResultset<CRLeadClass>.op_Implicit(PXSelectBase<CRLeadClass, PXSelect<CRLeadClass, Where<CRLeadClass.classID, Equal<Current<CRLead.classID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
      {
        (object) destData
      }, Array.Empty<object>()))?.DefaultOwner == "S")
      {
        destData.WorkgroupID = current.WorkgroupID;
        destData.OwnerID = current.OwnerID;
      }
      UDFHelper.CopyAttributes(((PXSelectBase) this.Base.CurrentBAccount).Cache, (object) ((PXSelectBase<PX.Objects.CR.BAccount>) this.Base.CurrentBAccount).Current, ((PXSelectBase) instance.Lead).Cache, (object) destData, destData.ClassID);
      ((PXSelectBase<CRLead>) instance.Lead).Update(destData);
      if (!((PXGraph) this.Base).IsContractBasedAPI)
        PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
      ((PXAction) instance.Save).Press();
    }

    public virtual void _(PX.Data.Events.RowSelected<PX.Objects.CR.BAccount> e)
    {
      ((PXAction) this.CreateLead).SetEnabled(e.Row?.Type != null && EnumerableExtensions.IsIn<string>(e.Row.Type, "PR", "CU", "VC"));
    }
  }

  /// <exclude />
  public class CreateContactFromAccountGraphExt : 
    CRCreateContactActionBase<BusinessAccountMaint, PX.Objects.CR.BAccount>
  {
    protected override PXSelectBase<CRPMTimeActivity> Activities
    {
      get
      {
        return (PXSelectBase<CRPMTimeActivity>) ((PXGraph) this.Base).GetExtension<BusinessAccountMaint_ActivityDetailsExt>().Activities;
      }
    }

    public override void Initialize()
    {
      base.Initialize();
      this.Addresses = new PXSelectExtension<DocumentAddress>((PXSelectBase) ((PXGraph) this.Base).GetExtension<BusinessAccountMaint.DefContactAddressExt>().DefAddress);
      this.Contacts = new PXSelectExtension<DocumentContact>((PXSelectBase) ((PXGraph) this.Base).GetExtension<BusinessAccountMaint.DefContactAddressExt>().DefContact);
    }

    protected override CRCreateActionBaseInit<BusinessAccountMaint, PX.Objects.CR.BAccount>.DocumentContactMapping GetDocumentContactMapping()
    {
      return new CRCreateActionBaseInit<BusinessAccountMaint, PX.Objects.CR.BAccount>.DocumentContactMapping(typeof (Contact))
      {
        Email = typeof (Contact.eMail)
      };
    }

    protected override CRCreateActionBaseInit<BusinessAccountMaint, PX.Objects.CR.BAccount>.DocumentAddressMapping GetDocumentAddressMapping()
    {
      return new CRCreateActionBaseInit<BusinessAccountMaint, PX.Objects.CR.BAccount>.DocumentAddressMapping(typeof (Address));
    }

    public virtual void _(PX.Data.Events.RowSelected<ContactFilter> e)
    {
      PXUIFieldAttribute.SetReadOnly<ContactFilter.fullName>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ContactFilter>>) e).Cache, (object) e.Row, true);
    }

    public virtual void _(
      PX.Data.Events.FieldDefaulting<ContactFilter, ContactFilter.fullName> e)
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ContactFilter, ContactFilter.fullName>, ContactFilter, object>) e).NewValue = (object) ((PXSelectBase<DocumentContact>) this.Contacts).SelectSingle(Array.Empty<object>())?.FullName;
    }

    protected override void FillRelations(PXGraph graph, Contact target)
    {
    }

    protected override void FillNotesAndAttachments(
      PXGraph graph,
      object src_row,
      PXCache dst_cache,
      Contact dst_row)
    {
    }

    protected override IConsentable MapConsentable(DocumentContact source, IConsentable target)
    {
      return target;
    }
  }

  /// <exclude />
  public class UpdateRelatedContactInfoFromAccountGraphExt : 
    CRUpdateRelatedContactInfoGraphExt<BusinessAccountMaint>
  {
    public FbqlSelect<SelectFromBase<Address, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<Contact>.On<BqlOperand<
    #nullable enable
    Contact.defAddressID, IBqlInt>.IsEqual<
    #nullable disable
    Address.addressID>>>, FbqlJoins.Left<PX.Objects.CR.Standalone.CRLead>.On<BqlOperand<
    #nullable enable
    PX.Objects.CR.Standalone.CRLead.contactID, IBqlInt>.IsEqual<
    #nullable disable
    Contact.contactID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    Contact.bAccountID, 
    #nullable disable
    Equal<P.AsInt>>>>, And<BqlOperand<
    #nullable enable
    Contact.contactType, IBqlString>.IsNotEqual<
    #nullable disable
    ContactTypesAttribute.lead>>>>.And<BqlOperand<
    #nullable enable
    PX.Objects.CR.Standalone.CRLead.overrideRefContact, IBqlBool>.IsEqual<
    #nullable disable
    False>>>, Address>.View BAccountRelatedAddresses;

    protected virtual void _(PX.Data.Events.RowPersisting<Contact> e)
    {
      if (e.Row == null)
        return;
      this.SetUpdateRelatedInfo<Contact>(e, this.GetFields_ContactInfoExt(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<Contact>>) e).Cache, (object) e.Row).Union<string>((IEnumerable<string>) new string[1]
      {
        "DefAddressID"
      }));
    }

    protected virtual void _(PX.Data.Events.RowPersisting<Address> e)
    {
      if (e.Row == null)
        return;
      this.SetUpdateRelatedInfo<Address>(e, this.GetFields_ContactInfoExt(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<Address>>) e).Cache));
    }

    protected virtual void _(PX.Data.Events.RowPersisting<CRLead> e)
    {
      if (e.Row == null)
        return;
      this.SetUpdateRelatedInfo<CRLead, CRLead.refContactID>(e);
    }

    protected virtual void _(PX.Data.Events.RowPersisted<Contact> e)
    {
      Contact row = e.Row;
      if (row == null || !this.UpdateRelatedInfo.GetValueOrDefault() || e.TranStatus != null || PXDBOperationExt.Command(e.Operation) != 1)
        return;
      PX.Objects.CR.BAccount baccount1 = ((PXSelectBase<PX.Objects.CR.BAccount>) this.Base.BAccount).Current;
      if (baccount1 == null)
        baccount1 = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.BAccount.bAccountID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.CR.BAccount.defContactID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this.Base, new object[2]
        {
          (object) row.BAccountID,
          (object) row.ContactID
        }));
      PX.Objects.CR.BAccount baccount2 = baccount1;
      if (baccount2 == null)
        return;
      this.UpdateContact<FbqlSelect<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.CR.Standalone.CRLead>.On<BqlOperand<PX.Objects.CR.Standalone.CRLead.contactID, IBqlInt>.IsEqual<Contact.contactID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact.bAccountID, Equal<P.AsInt>>>>, And<BqlOperand<Contact.contactType, IBqlString>.IsNotEqual<ContactTypesAttribute.lead>>>>.And<BqlOperand<PX.Objects.CR.Standalone.CRLead.overrideRefContact, IBqlBool>.IsEqual<False>>>, Contact>.View>(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<Contact>>) e).Cache, (object) row, new FbqlSelect<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.CR.Standalone.CRLead>.On<BqlOperand<PX.Objects.CR.Standalone.CRLead.contactID, IBqlInt>.IsEqual<Contact.contactID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact.bAccountID, Equal<P.AsInt>>>>, And<BqlOperand<Contact.contactType, IBqlString>.IsNotEqual<ContactTypesAttribute.lead>>>>.And<BqlOperand<PX.Objects.CR.Standalone.CRLead.overrideRefContact, IBqlBool>.IsEqual<False>>>, Contact>.View((PXGraph) this.Base), (object) baccount2.BAccountID);
    }

    protected virtual void _(PX.Data.Events.RowPersisted<Address> e)
    {
      Address row = e.Row;
      if (row == null || !this.UpdateRelatedInfo.GetValueOrDefault() || e.TranStatus != null || PXDBOperationExt.Command(e.Operation) != 1)
        return;
      PX.Objects.CR.BAccount baccount1 = ((PXSelectBase<PX.Objects.CR.BAccount>) this.Base.BAccount).Current;
      if (baccount1 == null)
        baccount1 = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.BAccount.bAccountID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.CR.BAccount.defAddressID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this.Base, new object[2]
        {
          (object) row.BAccountID,
          (object) row.AddressID
        }));
      PX.Objects.CR.BAccount baccount2 = baccount1;
      if (baccount2 == null)
        return;
      int? addressId = row.AddressID;
      int? defAddressId = baccount2.DefAddressID;
      if (!(addressId.GetValueOrDefault() == defAddressId.GetValueOrDefault() & addressId.HasValue == defAddressId.HasValue))
        return;
      this.UpdateAddress<FbqlSelect<SelectFromBase<Address, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<Contact>.On<BqlOperand<Contact.defAddressID, IBqlInt>.IsEqual<Address.addressID>>>, FbqlJoins.Left<PX.Objects.CR.Standalone.CRLead>.On<BqlOperand<PX.Objects.CR.Standalone.CRLead.contactID, IBqlInt>.IsEqual<Contact.contactID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact.bAccountID, Equal<P.AsInt>>>>, And<BqlOperand<Contact.contactType, IBqlString>.IsNotEqual<ContactTypesAttribute.lead>>>>.And<BqlOperand<PX.Objects.CR.Standalone.CRLead.overrideRefContact, IBqlBool>.IsEqual<False>>>, Address>.View>(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<Address>>) e).Cache, (object) row, this.BAccountRelatedAddresses, (object) baccount2.BAccountID);
    }
  }

  /// <exclude />
  public class ExtensionSort : 
    SortExtensionsBy<TypeArrayOf<PXGraphExtension<BusinessAccountMaint>>.FilledWith<BusinessAccountMaint.DefContactAddressExt, BusinessAccountMaint.CreateLeadFromAccountGraphExt, BusinessAccountMaint.CreateContactFromAccountGraphExt>>
  {
  }
}
