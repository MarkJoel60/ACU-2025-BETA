// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ContactMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.EP;
using PX.Objects.AR;
using PX.Objects.CR.ContactMaint_Extensions;
using PX.Objects.CR.Extensions;
using PX.Objects.CR.Extensions.CRCreateActions;
using PX.Objects.CR.Extensions.CRDuplicateEntities;
using PX.Objects.CR.Extensions.Relational;
using PX.Objects.CR.Extensions.SideBySideComparison;
using PX.Objects.CR.Extensions.SideBySideComparison.Link;
using PX.Objects.CR.Workflows;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.CR;

public class ContactMaint : PXGraph<
#nullable disable
ContactMaint, PX.Objects.CR.Contact, PX.Objects.CR.Contact.displayName>, ICaptionable
{
  [PXHidden]
  public PXSelect<BAccount> bAccountBasic;
  [PXHidden]
  public PXSetup<Company> company;
  [PXHidden]
  public PXSetupOptional<CRSetup> Setup;
  [PXViewName("Contact")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (PX.Objects.CR.Contact.duplicateStatus), typeof (PX.Objects.CR.Contact.duplicateFound)})]
  public SelectContactEmailSyncBase<SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<BAccount>.On<BqlOperand<
  #nullable enable
  BAccount.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.CR.Contact.bAccountID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.CR.Contact.contactType, 
  #nullable disable
  Equal<ContactTypesAttribute.person>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  BAccount.bAccountID, 
  #nullable disable
  IsNull>>>>.Or<Match<BAccount, Current<AccessInfo.userName>>>>>> Contact;
  public PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Current<PX.Objects.CR.Contact.contactID>>>> ContactCurrent;
  public PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Current<PX.Objects.CR.Contact.contactID>>>> ContactCurrent2;
  [PXViewName("Leads")]
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (PX.Objects.CR.Contact))]
  public PXSelectJoin<CRLead, InnerJoin<Address, On<Address.addressID, Equal<PX.Objects.CR.Contact.defAddressID>>, LeftJoin<CRActivityStatistics, On<CRActivityStatistics.noteID, Equal<CRLead.noteID>>>>, Where<CRLead.refContactID, Equal<Current<PX.Objects.CR.Contact.contactID>>>, OrderBy<Desc<CRLead.createdDateTime>>> Leads;
  [PXCopyPasteHiddenView]
  public PXSelect<CRActivityStatistics, Where<CRActivityStatistics.noteID, Equal<Current<PX.Objects.CR.Contact.noteID>>>> ContactActivityStatistics;
  [PXViewName("Address")]
  public FbqlSelect<SelectFromBase<Address, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  Address.addressID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.CR.Contact.defAddressID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  Address>.View AddressCurrent;
  [PXCopyPasteHiddenView]
  public PXSelectUsers<PX.Objects.CR.Contact, Where<Users.pKID, Equal<Current<PX.Objects.CR.Contact.userID>>>> User;
  [PXCopyPasteHiddenView]
  public PXSelectUsersInRoles UserRoles;
  [PXCopyPasteHiddenView]
  public PXSelectAllowedRoles Roles;
  [PXViewName("Answers")]
  public CRAttributeList<PX.Objects.CR.Contact> Answers;
  public PXSelectJoin<EMailSyncAccount, InnerJoin<BAccount, On<BAccount.bAccountID, Equal<EMailSyncAccount.employeeID>>>, Where<BAccount.defContactID, Equal<Optional<PX.Objects.CR.Contact.contactID>>>> SyncAccount;
  public PXSelect<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Optional<EMailSyncAccount.emailAccountID>>>> EMailAccounts;
  [PXHidden]
  public PXSelect<PX.Objects.CR.CROpportunityClass> CROpportunityClass;
  [PXViewName("Opportunities")]
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (PX.Objects.CR.Contact))]
  public PXSelectReadonly2<CROpportunity, LeftJoin<CROpportunityProbability, On<CROpportunityProbability.stageCode, Equal<CROpportunity.stageID>>, LeftJoin<PX.Objects.CR.CROpportunityClass, On<PX.Objects.CR.CROpportunityClass.cROpportunityClassID, Equal<CROpportunity.classID>>>>, Where<CROpportunity.contactID, Equal<Current<PX.Objects.CR.Contact.contactID>>>> Opportunities;
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (PX.Objects.CR.Contact))]
  public PXSelectReadonly<CRCase, Where<CRCase.contactID, Equal<Current<PX.Objects.CR.Contact.contactID>>, And<Where<Current<PX.Objects.CR.Contact.bAccountID>, IsNull, Or<CRCase.customerID, Equal<Current<PX.Objects.CR.Contact.bAccountID>>>>>>> Cases;
  [PXCopyPasteHiddenView]
  [PXViewName("Campaign Members")]
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (PX.Objects.CR.Contact), typeof (Select<CRCampaign, Where<CRCampaign.campaignID, Equal<Current<CRCampaignMembers.campaignID>>>>))]
  [PXViewDetailsButton(typeof (CRCampaign), typeof (Select<CRCampaign, Where<CRCampaign.campaignID, Equal<Current<CRCampaignMembers.campaignID>>>>))]
  public FbqlSelect<SelectFromBase<CRCampaignMembers, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CRCampaign>.On<BqlOperand<
  #nullable enable
  CRCampaign.campaignID, IBqlString>.IsEqual<
  #nullable disable
  CRCampaignMembers.campaignID>>>, FbqlJoins.Left<CRMarketingList>.On<BqlOperand<
  #nullable enable
  CRMarketingList.marketingListID, IBqlInt>.IsEqual<
  #nullable disable
  CRCampaignMembers.marketingListID>>>, FbqlJoins.Left<PX.Objects.CR.Contact>.On<BqlOperand<
  #nullable enable
  PX.Objects.CR.Contact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  CRCampaignMembers.contactID>>>>.Where<BqlOperand<
  #nullable enable
  CRCampaignMembers.contactID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.CR.Contact.contactID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  CRCampaignMembers>.View Members;
  [PXHidden]
  public PXSelect<CRMarketingListMember> Subscriptions_stub;
  [PXViewName("Notifications")]
  public PXSelectJoin<ContactNotification, InnerJoin<NotificationSetup, On<NotificationSetup.setupID, Equal<ContactNotification.setupID>>>, Where<ContactNotification.contactID, Equal<Optional<PX.Objects.CR.Contact.contactID>>>> NWatchers;
  [PXHidden]
  public PXSelect<CRQuote> SalesQuoteLink;
  public PXMenuAction<PX.Objects.CR.Contact> Action;
  public PXDBAction<PX.Objects.CR.Contact> addOpportunity;
  public PXDBAction<PX.Objects.CR.Contact> addCase;
  public PXAction<PX.Objects.CR.Contact> copyBAccountContactInfo;

  public ContactMaint()
  {
    PXUIFieldAttribute.SetRequired<PX.Objects.CR.Contact.lastName>(((PXSelectBase) this.Contact).Cache, true);
    PXUIFieldAttribute.SetDisplayName<BAccount.acctCD>(((PXGraph) this).Caches[typeof (BAccount)], "Business Account");
    PXUIFieldAttribute.SetDisplayName<BAccount.acctName>(((PXGraph) this).Caches[typeof (BAccount)], "Business Account Name");
    PXUIFieldAttribute.SetDisplayName<BAccountR.acctCD>(((PXGraph) this).Caches[typeof (BAccountR)], "Business Account");
    PXUIFieldAttribute.SetDisplayName<BAccountR.acctName>(((PXGraph) this).Caches[typeof (BAccountR)], "Business Account Name");
    PXUIFieldAttribute.SetEnabled<EPLoginTypeAllowsRole.rolename>(((PXSelectBase) this.Roles).Cache, (object) null, false);
    ((PXSelectBase) this.Roles).Cache.AllowInsert = false;
    ((PXSelectBase) this.Roles).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetVisible<PX.Objects.CR.Contact.languageID>(((PXSelectBase) this.ContactCurrent).Cache, (object) null, PXDBLocalizableStringAttribute.HasMultipleLocales);
    ((PXSelectBase) this.Opportunities).Cache.Fields.Add("_Contact_DisplayName");
    // ISSUE: method pointer
    ((PXGraph) this).FieldSelecting.AddHandler(typeof (CROpportunity), "_Contact_DisplayName", new PXFieldSelecting((object) this, __methodptr(\u003C\u002Ector\u003Eb__23_0)));
  }

  public virtual void InitCacheMapping(Dictionary<System.Type, System.Type> map)
  {
    ((PXGraph) this).InitCacheMapping(map);
    ((PXGraph) this).Caches.AddCacheMappingsWithInheritance((PXGraph) this, typeof (BAccount));
  }

  public string Caption()
  {
    PX.Objects.CR.Contact current = ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current;
    if (current == null)
      return "";
    return !string.IsNullOrEmpty(current.FullName) ? $"{current.DisplayName} - {current.FullName}" : current.DisplayName ?? "";
  }

  [PXUIField(DisplayName = "Create Opportunity", FieldClass = "CRM")]
  [PXButton]
  public virtual void AddOpportunity()
  {
    PX.Objects.CR.Contact current = ((PXSelectBase<PX.Objects.CR.Contact>) this.ContactCurrent).Current;
    if (current == null || !current.ContactID.HasValue)
      return;
    OpportunityMaint instance = PXGraph.CreateInstance<OpportunityMaint>();
    CROpportunity crOpportunity = ((PXSelectBase<CROpportunity>) instance.Opportunity).Insert();
    crOpportunity.BAccountID = current.BAccountID;
    crOpportunity.OverrideSalesTerritory = current.OverrideSalesTerritory;
    bool? overrideSalesTerritory = crOpportunity.OverrideSalesTerritory;
    if (overrideSalesTerritory.HasValue && overrideSalesTerritory.GetValueOrDefault())
      crOpportunity.SalesTerritoryID = current.SalesTerritoryID;
    crOpportunity.Source = current.Source;
    CRContactClass crContactClass = PXResultset<CRContactClass>.op_Implicit(PXSelectBase<CRContactClass, PXSelect<CRContactClass, Where<CRContactClass.classID, Equal<Required<PX.Objects.CR.Contact.classID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) current.ClassID
    }));
    crOpportunity.ClassID = crContactClass == null || crContactClass.TargetOpportunityClassID == null ? ((PXSelectBase<CRSetup>) this.Setup).Current?.DefaultOpportunityClassID : crContactClass.TargetOpportunityClassID;
    if (crContactClass != null && crContactClass.TargetOpportunityStage != null)
      crOpportunity.StageID = crContactClass.TargetOpportunityStage;
    if (PXResultset<PX.Objects.CR.CROpportunityClass>.op_Implicit(PXSelectBase<PX.Objects.CR.CROpportunityClass, PXSelect<PX.Objects.CR.CROpportunityClass, Where<PX.Objects.CR.CROpportunityClass.cROpportunityClassID, Equal<Current<CROpportunity.classID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) crOpportunity
    }, Array.Empty<object>()))?.DefaultOwner == "S")
    {
      crOpportunity.WorkgroupID = current.WorkgroupID;
      crOpportunity.OwnerID = current.OwnerID;
    }
    crOpportunity.ContactID = current.ContactID;
    UDFHelper.CopyAttributes(((PXSelectBase) this.ContactCurrent).Cache, (object) current, ((PXSelectBase) instance.Opportunity).Cache, (object) ((PXSelectBase<CROpportunity>) instance.Opportunity).Current, crOpportunity.ClassID);
    ((PXSelectBase<CROpportunity>) instance.Opportunity).Update(crOpportunity);
    if (!((PXGraph) this).IsContractBasedAPI)
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
    ((PXAction) instance.Save).Press();
  }

  [PXUIField(DisplayName = "Create Case", FieldClass = "CRM")]
  [PXButton]
  public virtual void AddCase()
  {
    PX.Objects.CR.Contact current = ((PXSelectBase<PX.Objects.CR.Contact>) this.ContactCurrent).Current;
    if (current == null || !current.ContactID.HasValue)
      return;
    CRCaseMaint instance1 = PXGraph.CreateInstance<CRCaseMaint>();
    CRCase instance2 = (CRCase) ((PXSelectBase) instance1.Case).Cache.CreateInstance();
    CRCase copy = PXCache<CRCase>.CreateCopy(((PXSelectBase<CRCase>) instance1.Case).Insert(instance2));
    UDFHelper.CopyAttributes(((PXSelectBase) this.ContactCurrent).Cache, (object) current, ((PXSelectBase) instance1.Case).Cache, (object) ((PXSelectBase<CRCase>) instance1.Case).Current, copy.CaseClassID);
    copy.CustomerID = current.BAccountID;
    copy.ContactID = current.ContactID;
    try
    {
      ((PXSelectBase<CRCase>) instance1.Case).Update(copy);
    }
    catch
    {
    }
    if (!((PXGraph) this).IsContractBasedAPI)
      PXRedirectHelper.TryRedirect((PXGraph) instance1, (PXRedirectHelper.WindowMode) 3);
    ((PXAction) instance1.Save).Press();
  }

  [PXButton(ImageKey = "ArrowDown", Tooltip = "Copy from Company", DisplayOnMainToolbar = false)]
  [PXUIField(DisplayName = "Copy from Company")]
  public virtual void CopyBAccountContactInfo()
  {
    PX.Objects.CR.Contact current = ((PXSelectBase<PX.Objects.CR.Contact>) this.ContactCurrent).Current;
    if (current == null)
      return;
    int? nullable = current.BAccountID;
    if (!nullable.HasValue)
      return;
    BAccount baccount = PXResultset<BAccount>.op_Implicit(PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) current.BAccountID
    }));
    if (baccount != null)
    {
      nullable = baccount.DefContactID;
      if (nullable.HasValue)
      {
        PX.Objects.CR.Contact src = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) baccount.DefContactID
        }));
        if (src != null)
          this.CopyContactInfo(current, src);
        ((PXSelectBase<PX.Objects.CR.Contact>) this.ContactCurrent).Update(current);
      }
    }
    if (!((PXGraph) this).IsContractBasedAPI)
      return;
    ((PXAction) this.Save).Press();
  }

  [PXUIField(DisplayName = "Contact")]
  [ContactSelector(true, new System.Type[] {typeof (ContactTypesAttribute.person)})]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.contactID> e)
  {
  }

  [PXUIField(DisplayName = "Contact Class")]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.classID> e)
  {
  }

  [ContactSynchronize]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.synchronize> e)
  {
  }

  [CRMBAccount(new System.Type[] {typeof (BAccountType.prospectType), typeof (BAccountType.customerType), typeof (BAccountType.combinedType), typeof (BAccountType.vendorType)}, null, null, null)]
  [PopupMessage]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.bAccountID> e)
  {
  }

  [PXUIField]
  [CRLeadFullName(typeof (PX.Objects.CR.Contact.bAccountID))]
  [PXRemoveBaseAttribute(typeof (PXUIFieldAttribute))]
  [PXRemoveBaseAttribute(typeof (PXDBStringAttribute))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.fullName> e)
  {
  }

  [CRMParentBAccount(typeof (PX.Objects.CR.Contact.bAccountID), null, null, null, null)]
  [PXFormula(typeof (Selector<PX.Objects.CR.Contact.bAccountID, BAccount.parentBAccountID>))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.parentBAccountID> e)
  {
  }

  [PXDefault("A")]
  [PXUIRequired(typeof (Where<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.person>>))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.status> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Enabled", true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.isActive> e)
  {
  }

  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXParent(typeof (Select<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.userID, Equal<Current<Users.pKID>>>>))]
  [PXMergeAttributes]
  public virtual void Users_PKID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(64 /*0x40*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Login")]
  [PXUIRequired(typeof (Where<Users.loginTypeID, IsNotNull, And<EntryStatus, Equal<EntryStatus.inserted>>>))]
  [PXMergeAttributes]
  public virtual void Users_Username_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search2<PX.Objects.CR.Contact.contactID, LeftJoin<Users, On<PX.Objects.CR.Contact.userID, Equal<Users.pKID>>, LeftJoin<BAccount, On<BAccount.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>, Where<Current<Users.guest>, Equal<True>, And<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.person>, Or<Current<Users.guest>, NotEqual<True>, And<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.employee>, And<BAccount.bAccountID, IsNotNull>>>>>>), new System.Type[] {typeof (PX.Objects.CR.Contact.displayName), typeof (PX.Objects.CR.Contact.salutation), typeof (PX.Objects.CR.Contact.fullName), typeof (PX.Objects.CR.Contact.eMail), typeof (Users.username)}, DescriptionField = typeof (PX.Objects.CR.Contact.displayName))]
  [PXRestrictor(typeof (Where<PX.Objects.CR.Contact.userID, IsNull, Or<PX.Objects.CR.Contact.userID, Equal<Current<Users.pKID>>>>), "Contact {0} already associated with another user.", new System.Type[] {typeof (PX.Objects.CR.Contact.displayName)})]
  [PXDBScalar(typeof (Search<PX.Objects.CR.Contact.contactID, Where<PX.Objects.CR.Contact.userID, Equal<Users.pKID>>>))]
  protected virtual void Users_ContactID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "User Type")]
  [PXRestrictor(typeof (Where<EPLoginType.entity, Equal<EPLoginType.entity.contact>>), "Incorrect User Type '{0}', linked entity must be 'Contact'", new System.Type[] {typeof (EPLoginType.loginTypeName)})]
  [PXSelector(typeof (Search5<EPLoginType.loginTypeID, LeftJoin<EPManagedLoginType, On<EPLoginType.loginTypeID, Equal<EPManagedLoginType.loginTypeID>>, LeftJoin<Users, On<EPManagedLoginType.parentLoginTypeID, Equal<Users.loginTypeID>>, LeftJoin<ContactMaint.CurrentUser, On<ContactMaint.CurrentUser.pKID, Equal<Current<AccessInfo.userID>>>>>>, Where<EPLoginType.allowThisTypeForContacts, Equal<True>, And<Users.pKID, Equal<ContactMaint.CurrentUser.pKID>, And<ContactMaint.CurrentUser.guest, Equal<True>, Or<ContactMaint.CurrentUser.guest, NotEqual<True>, And<EPLoginType.allowThisTypeForContacts, Equal<True>>>>>>, Aggregate<GroupBy<EPLoginType.loginTypeID, GroupBy<EPLoginType.loginTypeName, GroupBy<EPLoginType.requireLoginActivation, GroupBy<EPLoginType.resetPasswordOnLogin>>>>>>), SubstituteKey = typeof (EPLoginType.loginTypeName))]
  [PXDefault]
  protected virtual void Users_LoginTypeID_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "Guest Account")]
  [PXFormula(typeof (Switch<Case<Where<Selector<Users.loginTypeID, EPLoginType.entity>, Equal<EPLoginType.entity.contact>>, True>, False>))]
  [PXMergeAttributes]
  protected virtual void Users_Guest_CacheAttached(PXCache sender)
  {
  }

  [PXFormula(typeof (Selector<Users.loginTypeID, EPLoginType.requireLoginActivation>))]
  [PXMergeAttributes]
  protected virtual void Users_IsPendingActivation_CacheAttached(PXCache sender)
  {
  }

  [PXFormula(typeof (Switch<Case<Where<Selector<Users.loginTypeID, EPLoginType.resetPasswordOnLogin>, Equal<True>>, True>, False>))]
  [PXMergeAttributes]
  protected virtual void Users_PasswordChangeOnNextLogin_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(true)]
  [PXMergeAttributes]
  protected virtual void Users_GeneratePassword_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(256 /*0x0100*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault(typeof (Users.username))]
  [PXParent(typeof (Select<Users, Where<Users.username, Equal<Current<UsersInRoles.username>>>>))]
  [PXMergeAttributes]
  protected virtual void UsersInRoles_Username_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Assigned")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<EPLoginTypeAllowsRole.selected> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Campaign")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CRCampaignMembers.campaignID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Marketing List")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CRMarketingList.mailListCode> e)
  {
  }

  protected virtual void Users_State_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.ReturnValue != null || e.Row != null && sender.GetStatus(e.Row) != 2)
      return;
    e.ReturnValue = (object) "N";
  }

  protected virtual void Users_LoginTypeID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase) this.UserRoles).Cache.Clear();
    if (((Users) e.Row).LoginTypeID.HasValue)
      return;
    ((PXSelectBase) this.User).Cache.Clear();
    ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current.UserID = new Guid?();
  }

  protected virtual void Users_Username_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    Users row = (Users) e.Row;
    if (e.OldValue == null || row.Username == null || !(e.OldValue.ToString() != row.Username))
      return;
    ((PXSelectBase) this.User).Cache.RaiseExceptionHandling<Users.username>((object) ((PXSelectBase<Users>) this.User).Current, (object) ((PXSelectBase<Users>) this.User).Current.Username, (Exception) new PXSetPropertyException("The login cannot be modified."));
  }

  protected virtual void Users_Username_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    Guid? guidFromDeletedUser = Access.GetGuidFromDeletedUser((string) e.NewValue);
    if (!guidFromDeletedUser.HasValue)
      return;
    ((Users) e.Row).PKID = guidFromDeletedUser;
  }

  protected virtual void _(PX.Data.Events.RowSelected<Users> e)
  {
    Users row = e.Row;
    if (row == null)
      return;
    bool? generatePassword = row.GeneratePassword;
    bool flag = generatePassword.HasValue && !generatePassword.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<Users.password>(((PXSelectBase) this.User).Cache, (object) row, flag);
    PXUIFieldAttribute.SetRequired<Users.password>(((PXSelectBase) this.User).Cache, flag);
  }

  protected virtual void Users_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    Users row = (Users) e.Row;
    if (row == null || ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current == null || !((Users) e.Row).LoginTypeID.HasValue)
      return;
    if (((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current == null)
      ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current = PXResultset<PX.Objects.CR.Contact>.op_Implicit(((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Select(Array.Empty<object>()));
    ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current.UserID = row.PKID;
    GraphHelper.MarkUpdated(((PXSelectBase) this.Contact).Cache, (object) ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current);
    if (!((PXGraph) this).IsContractBasedAPI)
      return;
    ((PXSelectBase<EPLoginTypeAllowsRole>) this.Roles).Select(Array.Empty<object>());
  }

  protected virtual void Users_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    Users row = (Users) e.Row;
    EPLoginType epLoginType = PXResultset<EPLoginType>.op_Implicit(PXSelectBase<EPLoginType, PXSelect<EPLoginType, Where<EPLoginType.loginTypeID, Equal<Current<Users.loginTypeID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    if (epLoginType != null && epLoginType.EmailAsLogin.GetValueOrDefault() && string.IsNullOrEmpty(row.Username))
      row.Username = ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current.EMail;
    Guid? guidFromDeletedUser = Access.GetGuidFromDeletedUser(row.Username);
    if (guidFromDeletedUser.HasValue)
      row.PKID = guidFromDeletedUser;
    if (((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current != null && !((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current.UserID.HasValue)
    {
      ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current.UserID = row.PKID;
    }
    else
    {
      ((PXSelectBase) this.User).Cache.Clear();
      ((PXSelectBase) this.UserRoles).Cache.Clear();
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.CR.Contact> e)
  {
    PX.Objects.CR.Contact row = e.Row;
    if (row == null)
      return;
    bool flag1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.CR.Contact>>) e).Cache.GetStatus((object) row) != 2;
    ((PXSelectBase) this.Opportunities).Cache.AllowInsert = flag1;
    ((PXSelectBase) this.Cases).Cache.AllowInsert = flag1;
    ((PXSelectBase) this.Members).Cache.AllowInsert = flag1;
    ((PXSelectBase) this.NWatchers).Cache.AllowInsert = flag1;
    ((PXSelectBase) this.User).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.Roles).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.UserRoles).Cache.ClearQueryCacheObsolete();
    BAccount baccount = row.BAccountID.With<int?, BAccount>((Func<int?, BAccount>) (_ => PXResultset<BAccount>.op_Implicit(PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) _
    }))));
    bool flag2 = baccount == null || baccount.Type == "CU" || baccount.Type == "PR" || baccount.Type == "VC";
    bool flag3 = baccount == null || baccount.Type == "VE";
    ((PXAction) this.addOpportunity).SetEnabled(flag1 & flag2);
    ((PXAction) this.addCase).SetEnabled(flag1 && flag2 | flag3);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Contact.contactID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.CR.Contact>>) e).Cache, (object) row, true);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Contact.bAccountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.CR.Contact>>) e).Cache, (object) row, true);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Contact.classID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.CR.Contact>>) e).Cache, (object) row, true);
    bool flag4 = !row.UserID.HasValue || ((PXSelectBase) this.User).Cache.GetStatus((object) ((PXSelectBase<Users>) this.User).Current) == 2;
    bool flag5 = flag4 && ((PXSelectBase<Users>) this.User).Current != null && ((PXSelectBase<Users>) this.User).Current.LoginTypeID.HasValue;
    PXUIFieldAttribute.SetEnabled<Users.loginTypeID>(((PXSelectBase) this.User).Cache, (object) ((PXSelectBase<Users>) this.User).Current, flag4 && row.IsActive.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<Users.username>(((PXSelectBase) this.User).Cache, (object) ((PXSelectBase<Users>) this.User).Current, ((PXGraph) this).IsContractBasedAPI | flag5);
    PXUIFieldAttribute.SetEnabled<Users.generatePassword>(((PXSelectBase) this.User).Cache, (object) ((PXSelectBase<Users>) this.User).Current, ((PXGraph) this).IsContractBasedAPI | flag5);
    PXUIFieldAttribute.SetEnabled<Users.password>(((PXSelectBase) this.User).Cache, (object) ((PXSelectBase<Users>) this.User).Current, ((PXGraph) this).IsContractBasedAPI || flag5 && !((PXSelectBase<Users>) this.User).Current.GeneratePassword.GetValueOrDefault());
    bool flag6 = row.ContactType == "EP" && ((PXSelectBase<Users>) this.User).Current != null;
    PXDefaultAttribute.SetPersistingCheck<PX.Objects.CR.Contact.eMail>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.CR.Contact>>) e).Cache, (object) row, flag6 || flag5 && ((PXSelectBase<Users>) this.User).Current.Username != null ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetRequired<PX.Objects.CR.Contact.eMail>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.CR.Contact>>) e).Cache, flag6 || flag5 && ((PXSelectBase<Users>) this.User).Current.Username != null);
    ((PXSelectBase<Users>) this.User).Current = (Users) ((PXSelectBase) this.User).View.SelectSingleBound((object[]) new PX.Objects.CR.Contact[1]
    {
      e.Row
    }, Array.Empty<object>());
    PXUIFieldAttribute.SetEnabled<Address.isValidated>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.CR.Contact>>) e).Cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Contact.duplicateStatus>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.CR.Contact>>) e).Cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CRActivityStatistics.lastIncomingActivityDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.CR.Contact>>) e).Cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CRActivityStatistics.lastOutgoingActivityDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.CR.Contact>>) e).Cache, (object) row, false);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.CR.Contact> e)
  {
    PX.Objects.CR.Contact row = e.Row;
    if (row != null && row.ContactType == "EP")
      throw new PXSetPropertyException("Can not delete Contact of the Employee.");
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.CR.Contact> e)
  {
    PX.Objects.CR.Contact row = e.Row;
    if (row == null || !(row.ContactType == "EP") || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.CR.Contact>>) e).Cache.ObjectsEqual<PX.Objects.CR.Contact.displayName>((object) e.Row, (object) e.OldRow))
      return;
    BAccount baccount = PXResultset<BAccount>.op_Implicit(PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.parentBAccountID, Equal<Current<PX.Objects.CR.Contact.bAccountID>>, And<BAccount.defContactID, Equal<Current<PX.Objects.CR.Contact.contactID>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    if (baccount == null)
      return;
    BAccount copy = (BAccount) ((PXSelectBase) this.bAccountBasic).Cache.CreateCopy((object) baccount);
    ((PXSelectBase) this.bAccountBasic).Cache.SetValueExt<PX.Objects.EP.EPEmployee.acctName>((object) copy, (object) row.DisplayName);
    ((PXSelectBase<BAccount>) this.bAccountBasic).Update(copy);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.CR.Contact, PX.Objects.CR.Contact.eMail> e)
  {
    PX.Objects.CR.Contact row = e.Row;
    foreach (EMailSyncAccount emailSyncAccount in GraphHelper.RowCast<EMailSyncAccount>((IEnumerable) ((PXSelectBase<EMailSyncAccount>) this.SyncAccount).Select(new object[1]
    {
      (object) row.ContactID
    })).Select<EMailSyncAccount, EMailSyncAccount>((Func<EMailSyncAccount, EMailSyncAccount>) (account => (EMailSyncAccount) ((PXSelectBase) this.SyncAccount).Cache.CreateCopy((object) account))))
    {
      emailSyncAccount.Address = row.EMail;
      emailSyncAccount.ContactsExportDate = new DateTime?();
      emailSyncAccount.ContactsImportDate = new DateTime?();
      emailSyncAccount.EmailsExportDate = new DateTime?();
      emailSyncAccount.EmailsImportDate = new DateTime?();
      emailSyncAccount.TasksExportDate = new DateTime?();
      emailSyncAccount.TasksImportDate = new DateTime?();
      emailSyncAccount.EventsExportDate = new DateTime?();
      emailSyncAccount.EventsImportDate = new DateTime?();
      EMailAccount emailAccount = PXResultset<EMailAccount>.op_Implicit(((PXSelectBase<EMailAccount>) this.EMailAccounts).Select(new object[1]
      {
        (object) emailSyncAccount.EmailAccountID
      }));
      emailAccount.Address = emailSyncAccount.Address;
      ((PXSelectBase<EMailAccount>) this.EMailAccounts).Update(emailAccount);
      ((PXSelectBase<EMailSyncAccount>) this.SyncAccount).Update(emailSyncAccount);
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.CR.Contact, PX.Objects.CR.Contact.isActive> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.CR.Contact row = e.Row;
    bool? isActive = e.Row.IsActive;
    string str = !isActive.HasValue || !isActive.GetValueOrDefault() ? "I" : "A";
    row.Status = str;
    this.SetLinkedUserStatus(e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.CR.Contact, PX.Objects.CR.Contact.status> e)
  {
    if (e.Row == null || (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.CR.Contact, PX.Objects.CR.Contact.status>, PX.Objects.CR.Contact, object>) e).OldValue == (string) e.NewValue)
      return;
    PX.Objects.CR.Contact row = e.Row;
    bool flag = row.Status == "I";
    row.IsActive = new bool?(!flag);
    this.SetLinkedUserStatus(row);
    BAccount baccount = BAccount.PK.Find((PXGraph) this, row.BAccountID);
    if (baccount == null || !flag)
      return;
    int? primaryContactId = baccount.PrimaryContactID;
    int? contactId = row.ContactID;
    if (!(primaryContactId.GetValueOrDefault() == contactId.GetValueOrDefault() & primaryContactId.HasValue == contactId.HasValue))
      return;
    baccount.PrimaryContactID = new int?();
    ((PXSelectBase<BAccount>) this.bAccountBasic).Update(baccount);
  }

  private void SetLinkedUserStatus(PX.Objects.CR.Contact contact)
  {
    if (contact == null)
      return;
    Users users1 = Users.PK.Find((PXGraph) this, contact.UserID, (PKFindOptions) 0);
    if (users1 == null)
      return;
    Users users2 = users1;
    bool? isActive = contact.IsActive;
    bool? nullable = new bool?(isActive.HasValue && isActive.GetValueOrDefault());
    users2.IsApproved = nullable;
    ((PXSelectBase<Users>) this.User).Update(users1);
  }

  public virtual void Persist()
  {
    AccessUsers accessUsers = (AccessUsers) null;
    Users users1 = ((PXSelectBase<Users>) this.User).SelectSingle(Array.Empty<object>());
    if (users1 != null)
    {
      Users copy = PXCache<Users>.CreateCopy(users1);
      accessUsers = PXGraph.CreateInstance<AccessUsers>();
      ((PXSelectBase<Users>) accessUsers.UserList).Current = copy;
      Users users2;
      if (((PXSelectBase) this.User).Cache.GetStatus((object) users1) == 2)
      {
        copy.OldPassword = ((PXSelectBase<Users>) this.User).Current.Password;
        copy.NewPassword = ((PXSelectBase<Users>) this.User).Current.Password;
        copy.ConfirmPassword = ((PXSelectBase<Users>) this.User).Current.Password;
        copy.FirstName = ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current.FirstName;
        copy.LastName = ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current.LastName;
        copy.Email = ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current.EMail;
        copy.IsAssigned = new bool?(true);
        Users users3 = ((PXSelectBase<Users>) this.User).Update(copy);
        users2 = ((PXSelectBase<Users>) accessUsers.UserList).Insert(users3);
      }
      else
        users2 = ((PXSelectBase<Users>) accessUsers.UserList).Update(copy);
      ((PXSelectBase<Users>) accessUsers.UserList).Current = users2;
    }
    ((PXGraph) this).Persist();
    if (((PXSelectBase<Users>) this.User).Current != null && !((PXSelectBase<Users>) this.User).Current.ContactID.HasValue && ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current != null)
      ((PXSelectBase<Users>) this.User).Current.ContactID = ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current.ContactID;
    if (accessUsers == null)
      return;
    (string, string, string, bool?) valueTuple1 = (users1.OldPassword, users1.NewPassword, users1.ConfirmPassword, users1.GeneratePassword);
    ((PXAction) accessUsers.Cancel).Press();
    ((PXSelectBase<Users>) accessUsers.UserList).UpdateCurrent();
    ((PXAction) accessUsers.Save).Press();
    Users users4 = ((PXSelectBase<Users>) this.User).SelectSingle(Array.Empty<object>());
    if (users4 == null)
      return;
    Users users5 = users4;
    Users users6 = users4;
    Users users7 = users4;
    Users users8 = users4;
    (string, string, string, bool?) valueTuple2 = valueTuple1;
    string str = valueTuple2.Item1;
    users5.OldPassword = str;
    users6.NewPassword = valueTuple2.Item2;
    users7.ConfirmPassword = valueTuple2.Item3;
    users8.GeneratePassword = valueTuple2.Item4;
  }

  [PXUIField]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRLead.memberName> e)
  {
  }

  [PXDBDefault(typeof (PX.Objects.CR.Contact.contactID))]
  [PXMergeAttributes]
  protected virtual void CRCampaignMembers_ContactID_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "Class Description")]
  [PXMergeAttributes]
  protected virtual void CROpportunityClass_Description_CacheAttached(PXCache sender)
  {
  }

  protected void CopyContactInfo(PX.Objects.CR.Contact dest, PX.Objects.CR.Contact src)
  {
    if (!string.IsNullOrEmpty(src.FaxType))
      dest.FaxType = src.FaxType;
    if (!string.IsNullOrEmpty(src.Phone1Type))
      dest.Phone1Type = src.Phone1Type;
    if (!string.IsNullOrEmpty(src.Phone2Type))
      dest.Phone2Type = src.Phone2Type;
    if (!string.IsNullOrEmpty(src.Phone3Type))
      dest.Phone3Type = src.Phone3Type;
    dest.Fax = src.Fax;
    dest.Phone1 = src.Phone1;
    dest.Phone2 = src.Phone2;
    dest.Phone3 = src.Phone3;
    dest.WebSite = src.WebSite;
    dest.EMail = src.EMail;
  }

  [PXHidden]
  [Serializable]
  public class CurrentUser : Users
  {
    public abstract class pKID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ContactMaint.CurrentUser.pKID>
    {
    }

    public abstract class guest : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ContactMaint.CurrentUser.guest>
    {
    }
  }

  /// <exclude />
  public class PrimaryContactGraphExt : PXGraphExtension<ContactMaint>
  {
    protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.CR.Contact> e)
    {
      PX.Objects.CR.Contact row = e.Row;
      if (row == null || !(PXSelectorAttribute.Select<PX.Objects.CR.Contact.bAccountID>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.CR.Contact>>) e).Cache, (object) row, (object) row.BAccountID) is BAccount baccount))
        return;
      int? primaryContactId = baccount.PrimaryContactID;
      int? contactId = row.ContactID;
      if (!(primaryContactId.GetValueOrDefault() == contactId.GetValueOrDefault() & primaryContactId.HasValue == contactId.HasValue))
        return;
      GraphHelper.MarkUpdated((PXCache) GraphHelper.Caches<BAccount>((PXGraph) this.Base), (object) baccount);
    }

    protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.CR.Contact.bAccountID> e)
    {
      if (!(e.Row is PX.Objects.CR.Contact row) || !(PXSelectorAttribute.Select<PX.Objects.CR.Contact.bAccountID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.CR.Contact.bAccountID>>) e).Cache, (object) row, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.CR.Contact.bAccountID>, object, object>) e).OldValue) is BAccount baccount))
        return;
      int? primaryContactId = baccount.PrimaryContactID;
      int? contactId = row.ContactID;
      if (primaryContactId.GetValueOrDefault() == contactId.GetValueOrDefault() & primaryContactId.HasValue == contactId.HasValue)
        PXUIFieldAttribute.SetWarning<PX.Objects.CR.Contact.bAccountID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.CR.Contact.bAccountID>>) e).Cache, (object) row, "The current contact was the primary contact of the previously selected business account. Consider changing the primary contact for the previously selected business account before assigning the current contact to a different business account.");
      else
        PXUIFieldAttribute.SetWarning<PX.Objects.CR.Contact.bAccountID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.CR.Contact.bAccountID>>) e).Cache, (object) row, (string) null);
    }

    protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.CR.Contact> e)
    {
      PX.Objects.CR.Contact row = e.Row;
      if (row == null || e.TranStatus != null || !EnumerableExtensions.IsIn<PXDBOperation>(e.Operation, (PXDBOperation) 1, (PXDBOperation) 3) || !(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Contact>>) e).Cache.GetValueOriginal<PX.Objects.CR.Contact.bAccountID>((object) row) is int valueOriginal))
        return;
      int? nullable;
      if (e.Operation == 1)
      {
        nullable = row.BAccountID;
        int num = valueOriginal;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
          return;
      }
      BAccount baccount = BAccount.PK.Find((PXGraph) this.Base, new int?(valueOriginal));
      if (baccount == null)
        return;
      nullable = baccount.PrimaryContactID;
      int? contactId = row.ContactID;
      if (!(nullable.GetValueOrDefault() == contactId.GetValueOrDefault() & nullable.HasValue == contactId.HasValue))
        return;
      PXDatabase.Update<BAccount>(new PXDataFieldParam[2]
      {
        (PXDataFieldParam) new PXDataFieldAssign<BAccount.primaryContactID>((object) null),
        (PXDataFieldParam) new PXDataFieldRestrict<BAccount.bAccountID>((object) valueOriginal)
      });
    }
  }

  /// <exclude />
  public class DefaultContactOwnerGraphExt : 
    CRDefaultDocumentOwner<ContactMaint, PX.Objects.CR.Contact, PX.Objects.CR.Contact.classID, PX.Objects.CR.Contact.ownerID, PX.Objects.CR.Contact.workgroupID>
  {
  }

  /// <exclude />
  public class CRDuplicateEntitiesForContactGraphExt : PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<ContactMaint, PX.Objects.CR.Contact>
  {
    public override System.Type AdditionalConditions
    {
      get
      {
        return typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<DuplicateDocument.bAccountID>, IsNotNull>>>, And<Brackets<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<DuplicateContact.contactType, Equal<ContactTypesAttribute.bAccountProperty>>>>, And<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsNotEqual<BqlField<DuplicateDocument.bAccountID, IBqlInt>.FromCurrent>>>>.Or<BqlOperand<DuplicateContact.contactType, IBqlString>.IsNotEqual<ContactTypesAttribute.bAccountProperty>>>>>>.Or<BqlOperand<Current<DuplicateDocument.bAccountID>, IBqlInt>.IsNull>>>, And<Brackets<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<DuplicateDocument.bAccountID>, IsNotNull>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<DuplicateContact.bAccountID, Equal<BqlField<DuplicateDocument.bAccountID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<DuplicateContact.contactType, IBqlString>.IsEqual<ContactTypesAttribute.lead>>>, Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<DuplicateContact.bAccountID, IsNull>>>>.And<BqlOperand<DuplicateContact.contactType, IBqlString>.IsEqual<ContactTypesAttribute.lead>>>>>.Or<BqlOperand<DuplicateContact.contactType, IBqlString>.IsIn<ContactTypesAttribute.person, ContactTypesAttribute.bAccountProperty>>>>>>.Or<BqlOperand<Current<DuplicateDocument.bAccountID>, IBqlInt>.IsNull>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Standalone.CRLead.refContactID, NotEqual<BqlField<DuplicateDocument.contactID, IBqlInt>.FromCurrent>>>>>.Or<BqlOperand<PX.Objects.CR.Standalone.CRLead.refContactID, IBqlInt>.IsNull>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<DuplicateContact.isActive, Equal<True>>>>, And<BqlOperand<DuplicateContact.contactType, IBqlString>.IsNotEqual<ContactTypesAttribute.bAccountProperty>>>>.Or<BqlOperand<BAccountR.status, IBqlString>.IsNotEqual<CustomerStatus.inactive>>>);
      }
    }

    public override string WarningMessage => "This contact probably has duplicates";

    public static bool IsActive() => PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<ContactMaint, PX.Objects.CR.Contact>.IsExtensionActive();

    public override void Initialize()
    {
      base.Initialize();
      this.DuplicateDocuments = new PXSelectExtension<DuplicateDocument>((PXSelectBase) this.Base.ContactCurrent);
    }

    protected override PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<ContactMaint, PX.Objects.CR.Contact>.DocumentMapping GetDocumentMapping()
    {
      return new PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<ContactMaint, PX.Objects.CR.Contact>.DocumentMapping(typeof (PX.Objects.CR.Contact))
      {
        Key = typeof (PX.Objects.CR.Contact.contactID)
      };
    }

    protected override PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<ContactMaint, PX.Objects.CR.Contact>.DuplicateDocumentMapping GetDuplicateDocumentMapping()
    {
      return new PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<ContactMaint, PX.Objects.CR.Contact>.DuplicateDocumentMapping(typeof (PX.Objects.CR.Contact))
      {
        Email = typeof (PX.Objects.CR.Contact.eMail)
      };
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<PX.Objects.CR.Contact, PX.Objects.CR.Contact.duplicateStatus> e)
    {
      PX.Objects.CR.Contact row = e.Row;
      if (e.Row == null || (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.CR.Contact, PX.Objects.CR.Contact.duplicateStatus>, PX.Objects.CR.Contact, object>) e).OldValue == (string) e.NewValue || !(row.DuplicateStatus == "DD"))
        return;
      row.Status = "I";
    }

    protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.CR.Contact, PX.Objects.CR.Contact.isActive> e)
    {
      PX.Objects.CR.Contact row = e.Row;
      if (e.Row == null || !row.IsActive.GetValueOrDefault())
        return;
      bool? isActive = row.IsActive;
      bool? oldValue = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.CR.Contact, PX.Objects.CR.Contact.isActive>, PX.Objects.CR.Contact, object>) e).OldValue;
      if (isActive.GetValueOrDefault() == oldValue.GetValueOrDefault() & isActive.HasValue == oldValue.HasValue)
        return;
      row.DuplicateStatus = "NV";
    }

    protected override void _(PX.Data.Events.RowSelected<CRDuplicateRecord> e)
    {
      base._(e);
      CRDuplicateRecord row = e.Row;
      if (row == null || row.CanBeMerged.GetValueOrDefault())
        return;
      ((PXSelectBase) this.DuplicatesForMerging).Cache.RaiseExceptionHandling<CRDuplicateRecord.canBeMerged>((object) row, (object) row.CanBeMerged, (Exception) new PXSetPropertyException("The duplicate records cannot be merged because they are associated with different records.", (PXErrorLevel) 3));
    }

    public override PX.Objects.CR.Contact GetTargetEntity(int targetID)
    {
      return PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) targetID
      }));
    }

    public override PX.Objects.CR.Contact GetTargetContact(PX.Objects.CR.Contact targetEntity)
    {
      return targetEntity;
    }

    public override Address GetTargetAddress(PX.Objects.CR.Contact targetEntity)
    {
      return PXResultset<Address>.op_Implicit(PXSelectBase<Address, PXSelect<Address, Where<Address.addressID, Equal<Required<Address.addressID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) targetEntity.DefAddressID
      }));
    }

    protected override bool WhereMergingMet(CRDuplicateResult result)
    {
      DuplicateDocument current = ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).Current;
      CRDuplicateRecord crDuplicateRecord = ((PXResult) result).GetItem<CRDuplicateRecord>();
      return crDuplicateRecord != null && crDuplicateRecord.DuplicateContactType == "PN";
    }

    protected override bool CanBeMerged(CRDuplicateResult result)
    {
      DuplicateDocument current = ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).Current;
      CRDuplicateRecord crDuplicateRecord = ((PXResult) result).GetItem<CRDuplicateRecord>();
      if (crDuplicateRecord != null)
      {
        int? nullable1 = current.RefContactID;
        int? nullable2;
        if (nullable1.HasValue)
        {
          nullable1 = crDuplicateRecord.DuplicateRefContactID;
          if (nullable1.HasValue)
          {
            nullable1 = crDuplicateRecord.DuplicateRefContactID;
            nullable2 = current.RefContactID;
            if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
              goto label_8;
          }
        }
        nullable2 = current.BAccountID;
        if (nullable2.HasValue)
        {
          nullable2 = crDuplicateRecord.DuplicateBAccountID;
          if (nullable2.HasValue)
          {
            nullable2 = crDuplicateRecord.DuplicateBAccountID;
            nullable1 = current.BAccountID;
            return nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue;
          }
        }
        return true;
      }
label_8:
      return false;
    }

    public override void DoDuplicateAttach(DuplicateDocument duplicateDocument)
    {
      if (!(((PXSelectBase) this.DuplicatesForLinking).Cache.Current is CRDuplicateRecord current))
        return;
      PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Current<CRDuplicateRecord.duplicateContactID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
      {
        (object) current
      }, Array.Empty<object>()));
      if (contact == null)
        return;
      if (contact.ContactType == "LD")
      {
        CRLead crLead = CRLead.PK.Find((PXGraph) this.Base, current.DuplicateContactID);
        crLead.BAccountID = new int?();
        ((PXSelectBase) this.Base.Leads).Cache.SetValueExt<CRLead.refContactID>((object) crLead, (object) duplicateDocument.ContactID);
        ((PXSelectBase) this.Base.Leads).Cache.Update((object) crLead);
      }
      else
      {
        duplicateDocument.BAccountID = contact.ContactType == "AP" ? contact.BAccountID : throw new PXException("Select the lead or the business account that you want to link with the contact.");
        ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).Update(duplicateDocument);
      }
    }

    public override PXResult<PX.Objects.CR.Contact> GetGramContext(
      DuplicateDocument duplicateDocument)
    {
      return (PXResult<PX.Objects.CR.Contact>) new PXResult<PX.Objects.CR.Contact, Address>(duplicateDocument.Base as PX.Objects.CR.Contact, ((PXSelectBase<Address>) this.Base.AddressCurrent).SelectSingle(Array.Empty<object>()));
    }

    public class Workflow : 
      PXGraphExtension<ContactMaint.CRDuplicateEntitiesForContactGraphExt, ContactWorkflow, ContactMaint>
    {
      public static bool IsActive()
      {
        return PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<ContactMaint, PX.Objects.CR.Contact>.IsExtensionActive();
      }

      public virtual void Configure(PXScreenConfiguration configuration)
      {
        ContactMaint.CRDuplicateEntitiesForContactGraphExt.Workflow.Configure(configuration.GetScreenConfigurationContext<ContactMaint, PX.Objects.CR.Contact>());
      }

      protected static void Configure(WorkflowContext<ContactMaint, PX.Objects.CR.Contact> context)
      {
        BoundedTo<ContactMaint, PX.Objects.CR.Contact>.ActionCategory.IConfigured categoryValidation = context.Categories.Get("Validation");
        context.UpdateScreenConfigurationFor((Func<BoundedTo<ContactMaint, PX.Objects.CR.Contact>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ContactMaint, PX.Objects.CR.Contact>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((System.Action<BoundedTo<ContactMaint, PX.Objects.CR.Contact>.ActionDefinition.ContainerAdjusterActions>) (actions =>
        {
          actions.Add<ContactMaint.CRDuplicateEntitiesForContactGraphExt>((Expression<Func<ContactMaint.CRDuplicateEntitiesForContactGraphExt, PXAction<PX.Objects.CR.Contact>>>) (e => e.CheckForDuplicates), (Func<BoundedTo<ContactMaint, PX.Objects.CR.Contact>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContactMaint, PX.Objects.CR.Contact>.ActionDefinition.IConfigured>) (a => (BoundedTo<ContactMaint, PX.Objects.CR.Contact>.ActionDefinition.IConfigured) a.WithCategory(categoryValidation)));
          actions.Add<ContactMaint.CRDuplicateEntitiesForContactGraphExt>((Expression<Func<ContactMaint.CRDuplicateEntitiesForContactGraphExt, PXAction<PX.Objects.CR.Contact>>>) (e => e.MarkAsValidated), (Func<BoundedTo<ContactMaint, PX.Objects.CR.Contact>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContactMaint, PX.Objects.CR.Contact>.ActionDefinition.IConfigured>) (a => (BoundedTo<ContactMaint, PX.Objects.CR.Contact>.ActionDefinition.IConfigured) a.WithCategory(categoryValidation)));
          actions.Add<ContactMaint.CRDuplicateEntitiesForContactGraphExt>((Expression<Func<ContactMaint.CRDuplicateEntitiesForContactGraphExt, PXAction<PX.Objects.CR.Contact>>>) (e => e.CloseAsDuplicate), (Func<BoundedTo<ContactMaint, PX.Objects.CR.Contact>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContactMaint, PX.Objects.CR.Contact>.ActionDefinition.IConfigured>) (a => (BoundedTo<ContactMaint, PX.Objects.CR.Contact>.ActionDefinition.IConfigured) a.WithCategory(categoryValidation)));
          actions.Add<ContactMaint.ContactAddressActions>((Expression<Func<ContactMaint.ContactAddressActions, PXAction<PX.Objects.CR.Contact>>>) (e => e.ValidateAddress), (Func<BoundedTo<ContactMaint, PX.Objects.CR.Contact>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContactMaint, PX.Objects.CR.Contact>.ActionDefinition.IConfigured>) (a => (BoundedTo<ContactMaint, PX.Objects.CR.Contact>.ActionDefinition.IConfigured) a.WithCategory(categoryValidation)));
        }))));
      }
    }
  }

  /// <exclude />
  public class ContactBAccountSharedAddressOverrideGraphExt : 
    SharedChildOverrideGraphExt<ContactMaint, ContactMaint.ContactBAccountSharedAddressOverrideGraphExt>
  {
    protected override CRParentChild<ContactMaint, ContactMaint.ContactBAccountSharedAddressOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<ContactMaint, ContactMaint.ContactBAccountSharedAddressOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Contact))
      {
        RelatedID = typeof (PX.Objects.CR.Contact.bAccountID),
        ChildID = typeof (PX.Objects.CR.Contact.defAddressID),
        IsOverrideRelated = typeof (PX.Objects.CR.Contact.overrideAddress)
      };
    }

    protected override SharedChildOverrideGraphExt<ContactMaint, ContactMaint.ContactBAccountSharedAddressOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<ContactMaint, ContactMaint.ContactBAccountSharedAddressOverrideGraphExt>.RelatedMapping(typeof (BAccount))
      {
        RelatedID = typeof (BAccount.bAccountID),
        ChildID = typeof (BAccount.defAddressID)
      };
    }

    protected override CRParentChild<ContactMaint, ContactMaint.ContactBAccountSharedAddressOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<ContactMaint, ContactMaint.ContactBAccountSharedAddressOverrideGraphExt>.ChildMapping(typeof (Address))
      {
        ChildID = typeof (Address.addressID),
        RelatedID = typeof (Address.bAccountID)
      };
    }

    protected override void _(
      PX.Data.Events.RowSelected<CRParentChild<ContactMaint, ContactMaint.ContactBAccountSharedAddressOverrideGraphExt>.Child> e)
    {
      if (e.Row == null)
        return;
      PX.Objects.CR.Contact current = ((PXSelectBase<PX.Objects.CR.Contact>) this.Base.Contact).Current;
      if (current == null)
        return;
      BAccount baccount = PXSelectorAttribute.Select<PX.Objects.CR.Contact.bAccountID>(((PXSelectBase) this.Base.Contact).Cache, (object) current) as BAccount;
      PXUIFieldAttribute.SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRParentChild<ContactMaint, ContactMaint.ContactBAccountSharedAddressOverrideGraphExt>.Child>>) e).Cache, (object) e.Row, current.OverrideAddress.GetValueOrDefault() || baccount == null || baccount.Type == "PR");
    }
  }

  /// <exclude />
  public class ContactAddressActions : CRAddressActions<ContactMaint, PX.Objects.CR.Contact>
  {
    protected override CRParentChild<ContactMaint, CRAddressActions<ContactMaint, PX.Objects.CR.Contact>>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<ContactMaint, CRAddressActions<ContactMaint, PX.Objects.CR.Contact>>.ChildMapping(typeof (Address))
      {
        ChildID = typeof (Address.addressID),
        RelatedID = typeof (Address.bAccountID)
      };
    }

    protected override CRParentChild<ContactMaint, CRAddressActions<ContactMaint, PX.Objects.CR.Contact>>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<ContactMaint, CRAddressActions<ContactMaint, PX.Objects.CR.Contact>>.DocumentMapping(typeof (PX.Objects.CR.Contact))
      {
        RelatedID = typeof (PX.Objects.CR.Contact.bAccountID),
        ChildID = typeof (PX.Objects.CR.Contact.defAddressID)
      };
    }
  }

  public class LinkLeadFromContactExt : 
    LinkEntitiesExt_EventBased<ContactMaint, PX.Objects.CR.Contact, LinkFilter, CRLead, CRLead.refContactID>
  {
    public PXFilter<LinkFilter> SelectContactForLink;

    public override string LeftValueDescription => "Lead";

    public override string RightValueDescription => "Contact";

    public override CRLead UpdatingEntityCurrent
    {
      get
      {
        if (((PXCache) GraphHelper.Caches<CRDuplicateRecordForLinking>((PXGraph) this.Base)).Current is CRDuplicateRecordForLinking current1)
        {
          int? duplicateContactId = current1.DuplicateContactID;
          if (duplicateContactId.HasValue)
          {
            int valueOrDefault = duplicateContactId.GetValueOrDefault();
            if (this.UpdatingEntityCache.Current is CRLead current)
            {
              int? contactId = current.ContactID;
              int num = valueOrDefault;
              if (contactId.GetValueOrDefault() == num & contactId.HasValue)
                return current;
            }
            return CRLead.PK.Find((PXGraph) this.Base, new int?(valueOrDefault));
          }
        }
        return (CRLead) null;
      }
    }

    [PXMergeAttributes]
    [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Sync with Lead")]
    protected virtual void _(PX.Data.Events.CacheAttached<LinkFilter.processLink> e)
    {
    }

    public override EntitiesContext GetLeftEntitiesContext()
    {
      CRLead updatingEntityCurrent = this.UpdatingEntityCurrent;
      Address address = Address.PK.Find((PXGraph) this.Base, updatingEntityCurrent.DefAddressID);
      return new EntitiesContext((PXGraph) this.Base, new EntityEntry(typeof (PX.Objects.CR.Contact), this.UpdatingEntityCache, new IBqlTable[1]
      {
        (IBqlTable) updatingEntityCurrent
      }), new EntityEntry[1]
      {
        new EntityEntry(typeof (Address), ((PXSelectBase) this.Base.AddressCurrent).Cache, new IBqlTable[1]
        {
          (IBqlTable) address
        })
      });
    }

    public override EntitiesContext GetRightEntitiesContext()
    {
      return new EntitiesContext((PXGraph) this.Base, new EntityEntry(((PXSelectBase) this.Base.Contact).Cache, new IBqlTable[1]
      {
        (IBqlTable) ((PXSelectBase<PX.Objects.CR.Contact>) this.Base.Contact).Current
      }), new EntityEntry[1]
      {
        new EntityEntry(((PXSelectBase) this.Base.AddressCurrent).Cache, new IBqlTable[1]
        {
          (IBqlTable) ((PXSelectBase<Address>) this.Base.AddressCurrent).SelectSingle(Array.Empty<object>())
        })
      });
    }

    public override void UpdateMainAfterProcess()
    {
      this.UpdatingEntityCurrent.OverrideRefContact = new bool?(!((PXSelectBase<LinkFilter>) this.Filter).Current.ProcessLink.GetValueOrDefault());
      base.UpdateMainAfterProcess();
    }

    public override void UpdateRightEntitiesContext(
      EntitiesContext context,
      IEnumerable<LinkComparisonRow> result)
    {
    }

    protected override object GetSelectedEntityID()
    {
      return (object) ((PXSelectBase<PX.Objects.CR.Contact>) this.Base.Contact).Current.ContactID;
    }
  }

  /// <exclude />
  public class UpdateRelatedContactInfoFromContactGraphExt : 
    CRUpdateRelatedContactInfoGraphExt<ContactMaint>
  {
    public FbqlSelect<SelectFromBase<Address, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CR.Contact>.On<BqlOperand<
    #nullable enable
    PX.Objects.CR.Contact.defAddressID, IBqlInt>.IsEqual<
    #nullable disable
    Address.addressID>>>, FbqlJoins.Left<PX.Objects.CR.Standalone.CRLead>.On<BqlOperand<
    #nullable enable
    PX.Objects.CR.Standalone.CRLead.contactID, IBqlInt>.IsEqual<
    #nullable disable
    PX.Objects.CR.Contact.contactID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PX.Objects.CR.Standalone.CRLead.refContactID, 
    #nullable disable
    Equal<P.AsInt>>>>>.And<BqlOperand<
    #nullable enable
    PX.Objects.CR.Standalone.CRLead.overrideRefContact, IBqlBool>.IsEqual<
    #nullable disable
    False>>>, Address>.View ContactRelatedAddresses;

    protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.CR.Contact> e)
    {
      if (e.Row == null)
        return;
      this.SetUpdateRelatedInfo<PX.Objects.CR.Contact>(e, this.GetFields_ContactInfoExt(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.CR.Contact>>) e).Cache, (object) e.Row).Union<string>((IEnumerable<string>) new string[1]
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

    protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.CR.Contact> e)
    {
      PX.Objects.CR.Contact row = e.Row;
      if (row == null || !this.UpdateRelatedInfo.GetValueOrDefault() || e.TranStatus != null || PXDBOperationExt.Command(e.Operation) != 1)
        return;
      this.UpdateContact<FbqlSelect<SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.CR.Standalone.CRLead>.On<BqlOperand<PX.Objects.CR.Standalone.CRLead.contactID, IBqlInt>.IsEqual<PX.Objects.CR.Contact.contactID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Standalone.CRLead.refContactID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.CR.Standalone.CRLead.overrideRefContact, IBqlBool>.IsEqual<False>>>, PX.Objects.CR.Contact>.View>(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Contact>>) e).Cache, (object) row, new FbqlSelect<SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.CR.Standalone.CRLead>.On<BqlOperand<PX.Objects.CR.Standalone.CRLead.contactID, IBqlInt>.IsEqual<PX.Objects.CR.Contact.contactID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Standalone.CRLead.refContactID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.CR.Standalone.CRLead.overrideRefContact, IBqlBool>.IsEqual<False>>>, PX.Objects.CR.Contact>.View((PXGraph) this.Base), (object) row.ContactID);
    }

    protected virtual void _(PX.Data.Events.RowPersisted<Address> e)
    {
      Address row = e.Row;
      if (row == null || !this.UpdateRelatedInfo.GetValueOrDefault() || e.TranStatus != null || EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 1, (PXDBOperation) 2))
        return;
      PX.Objects.CR.Contact contact1 = ((PXSelectBase<PX.Objects.CR.Contact>) this.Base.Contact).Current;
      if (contact1 == null)
        contact1 = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.defAddressID, Equal<P.AsInt>, And<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.person>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) row.AddressID
        }));
      PX.Objects.CR.Contact contact2 = contact1;
      if (contact2 == null)
        return;
      this.UpdateAddress<FbqlSelect<SelectFromBase<Address, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CR.Contact>.On<BqlOperand<PX.Objects.CR.Contact.defAddressID, IBqlInt>.IsEqual<Address.addressID>>>, FbqlJoins.Left<PX.Objects.CR.Standalone.CRLead>.On<BqlOperand<PX.Objects.CR.Standalone.CRLead.contactID, IBqlInt>.IsEqual<PX.Objects.CR.Contact.contactID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Standalone.CRLead.refContactID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.CR.Standalone.CRLead.overrideRefContact, IBqlBool>.IsEqual<False>>>, Address>.View>(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<Address>>) e).Cache, (object) row, this.ContactRelatedAddresses, (object) contact2.ContactID);
    }
  }

  /// <exclude />
  public class CreateLeadFromContactGraphExt : CRCreateLeadAction<ContactMaint, PX.Objects.CR.Contact>
  {
    public virtual void Initialize()
    {
      ((PXGraphExtension) this).Initialize();
      this.Addresses = new PXSelectExtension<DocumentAddress>((PXSelectBase) this.Base.AddressCurrent);
      this.Contacts = new PXSelectExtension<DocumentContact>((PXSelectBase) this.Base.ContactCurrent);
    }

    protected override CRCreateActionBaseInit<ContactMaint, PX.Objects.CR.Contact>.DocumentContactMapping GetDocumentContactMapping()
    {
      return new CRCreateActionBaseInit<ContactMaint, PX.Objects.CR.Contact>.DocumentContactMapping(typeof (PX.Objects.CR.Contact))
      {
        Email = typeof (PX.Objects.CR.Contact.eMail)
      };
    }

    protected override CRCreateActionBaseInit<ContactMaint, PX.Objects.CR.Contact>.DocumentAddressMapping GetDocumentAddressMapping()
    {
      return new CRCreateActionBaseInit<ContactMaint, PX.Objects.CR.Contact>.DocumentAddressMapping(typeof (Address));
    }

    public override PX.Objects.CR.Contact GetCurrentMain(params object[] pars)
    {
      return PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, pars)) ?? ((PXSelectBase<PX.Objects.CR.Contact>) this.Base.ContactCurrent).Current;
    }
  }

  /// <exclude />
  public class CreateAccountFromContactGraphExt : CRCreateAccountAction<ContactMaint, PX.Objects.CR.Contact>
  {
    protected override string TargetType => "PX.Objects.CR.Contact";

    protected override PXSelectBase<CRPMTimeActivity> Activities
    {
      get
      {
        return (PXSelectBase<CRPMTimeActivity>) ((PXGraph) this.Base).GetExtension<ContactMaint_ActivityDetailsExt>().Activities;
      }
    }

    public override void Initialize()
    {
      base.Initialize();
      this.Addresses = new PXSelectExtension<DocumentAddress>((PXSelectBase) this.Base.AddressCurrent);
      this.Contacts = new PXSelectExtension<DocumentContact>((PXSelectBase) this.Base.ContactCurrent);
    }

    protected override CRCreateActionBaseInit<ContactMaint, PX.Objects.CR.Contact>.DocumentContactMapping GetDocumentContactMapping()
    {
      return new CRCreateActionBaseInit<ContactMaint, PX.Objects.CR.Contact>.DocumentContactMapping(typeof (PX.Objects.CR.Contact))
      {
        Email = typeof (PX.Objects.CR.Contact.eMail)
      };
    }

    protected override CRCreateActionBaseInit<ContactMaint, PX.Objects.CR.Contact>.DocumentAddressMapping GetDocumentAddressMapping()
    {
      return new CRCreateActionBaseInit<ContactMaint, PX.Objects.CR.Contact>.DocumentAddressMapping(typeof (Address));
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass> e)
    {
      BAccount baccount = ((PXSelectBase<BAccount>) this.ExistingAccount).SelectSingle(Array.Empty<object>());
      if (baccount != null)
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass>, AccountsFilter, object>) e).NewValue = (object) baccount.ClassID;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass>>) e).Cancel = true;
      }
      else
      {
        PX.Objects.CR.Contact current = ((PXSelectBase<PX.Objects.CR.Contact>) this.Base.Contact).Current;
        if (current == null)
          return;
        CRContactClass crContactClass = PXResultset<CRContactClass>.op_Implicit(PXSelectBase<CRContactClass, PXSelect<CRContactClass, Where<CRContactClass.classID, Equal<Required<PX.Objects.CR.Contact.classID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) current.ClassID
        }));
        if (crContactClass != null && crContactClass.TargetBAccountClassID != null)
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass>, AccountsFilter, object>) e).NewValue = (object) crContactClass.TargetBAccountClassID;
        else
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass>, AccountsFilter, object>) e).NewValue = (object) ((PXSelectBase<CRSetup>) this.Base.Setup).Current?.DefaultCustomerClassID;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass>>) e).Cancel = true;
      }
    }

    protected override void _(PX.Data.Events.RowSelected<AccountsFilter> e)
    {
      base._(e);
      PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AccountsFilter>>) e).Cache, (object) e.Row).For<AccountsFilter.linkContactToAccount>((System.Action<PXUIFieldAttribute>) (_ => _.Visible = false));
    }

    protected override void MapAddress(
      DocumentAddress docAddress,
      BAccount account,
      ref Address address)
    {
      ref Address local = ref address;
      if (!(((PXSelectBase) this.Base.AddressCurrent).View.SelectSingle(Array.Empty<object>()) is Address address1))
        address1 = address;
      local = address1;
      base.MapAddress(docAddress, account, ref address);
      account.DefAddressID = address.AddressID;
      address.BAccountID = account.BAccountID;
    }
  }

  /// <exclude />
  public class ContactMaintAddressLookupExtension : 
    AddressLookupExtension<ContactMaint, PX.Objects.CR.Contact, Address>
  {
    protected override string AddressView => "AddressCurrent";
  }
}
