// Decompiled with JetBrains decompiler
// Type: PX.SM.AccessUsers
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.Access.ActiveDirectory;
using PX.Data.MultiFactorAuth;
using PX.EP;
using PX.Licensing;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.SM;

[PXPrimaryGraph(typeof (Users))]
public class AccessUsers : PX.SM.Access
{
  public PXCancel<Users> Cancel;
  public PXSave<Users> Save;
  public PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Current<Users.contactID>>>> contact;
  public PXSelect<PreferencesSecurity> Preferences;
  public PXSelectAllowedRoles AllowedRoles;
  [PXHidden]
  public PXSelectJoin<PX.Objects.EP.EPEmployee, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.EP.EPEmployee.defContactID, Equal<PX.Objects.CR.Contact.contactID>, And<PX.Objects.EP.EPEmployee.parentBAccountID, Equal<PX.Objects.CR.Contact.bAccountID>>>>, Where<PX.Objects.CR.Contact.contactID, Equal<Optional<PX.Objects.CR.Contact.contactID>>>> Employee;
  [PXHidden]
  public PXSelect<EPCompanyTreeMember, Where<EPCompanyTreeMember.contactID, Equal<Optional<PX.Objects.CR.Contact.contactID>>>> Members;
  public PXSelect<EPLoginType, Where<EPLoginType.loginTypeID, Equal<Current<Users.loginTypeID>>>> LoginType;
  public PXFilter<ADUserFilter> ADUser;
  public PXSelectJoin<EMailAccount, LeftJoin<EMailAccountStatistics, On<EMailAccountStatistics.emailAccountID, Equal<EMailAccount.emailAccountID>>>, Where<EMailAccount.userID, Equal<Current<Users.pKID>>>> EMailAccounts;
  public PXAction<Users> AddADUser;
  public PXAction<Users> AddADUserOK;
  public PXAction<Users> ReloadADUsers;
  public PXAction<Users> GenerateOneTimeCodes;
  public PXAction<Users> ViewEMailAccount;
  public PXAction<Users> AddEMailAccount;

  [InjectDependency]
  public IAdvancedAuthenticationRestrictor AdvancedAuthenticationRestrictor { get; set; }

  [InjectDependency]
  protected IMultiFactorService _multiFactorService { get; set; }

  [InjectDependency]
  private ILicensing _licensing { get; set; }

  [PXInternalUseOnly]
  public virtual void Initialize()
  {
    base.Initialize();
    ((PXAction) this.Cancel).SetVisible(false);
    ((PXAction) this.Save).SetVisible(false);
    if (!Extensions.IsEnabled(this.ActiveDirectoryProvider))
      ((PXAction) this.AddADUser).SetVisible(false);
    bool flag = this.ShouldCacheADUsers();
    ((PXAction) this.ReloadADUsers).SetVisible(flag);
    foreach (PXEventSubscriberAttribute attribute in ((PXGraph) this).GetAttributes("ADUser", "Username"))
    {
      if (attribute is PXADUsersSelectorAttribute)
        ((PXADUsersSelectorAttribute) attribute).UseCached = flag;
    }
  }

  protected virtual IEnumerable identities()
  {
    return (IEnumerable) base.identities().OfType<UserIdentity>().Where<UserIdentity>((Func<UserIdentity, bool>) (i => this.AdvancedAuthenticationRestrictor.IsAllowedProviderName(i.ProviderName)));
  }

  [PXDBInt]
  [PXUIField(DisplayName = "User Type")]
  [PXSelector(typeof (Search<EPLoginType.loginTypeID>), SubstituteKey = typeof (EPLoginType.loginTypeName))]
  [PXDefault]
  protected virtual void Users_LoginTypeID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "First Name")]
  [PXFormula(typeof (Switch<Case<Where<Users.contactID, IsNotNull>, Selector<Users.contactID, PX.Objects.CR.Contact.firstName>>, Users.firstName>))]
  [PXPersonalDataField]
  protected virtual void Users_FirstName_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Last Name")]
  [PXFormula(typeof (Switch<Case<Where<Users.contactID, IsNotNull>, Selector<Users.contactID, PX.Objects.CR.Contact.lastName>>, Users.lastName>))]
  [PXPersonalDataField]
  protected virtual void Users_LastName_CacheAttached(PXCache sender)
  {
  }

  [PXDBEmail]
  [PXUIField]
  [PXFormula(typeof (Switch<Case<Where<Selector<Users.contactID, PX.Objects.CR.Contact.eMail>, IsNotNull>, Selector<Users.contactID, PX.Objects.CR.Contact.eMail>>, Users.email>))]
  [PXDefault]
  [PXUIRequired(typeof (Where<Users.source, NotEqual<PXUsersSourceListAttribute.activeDirectory>>))]
  [PXPersonalDataField]
  protected virtual void Users_Email_CacheAttached(PXCache sender)
  {
  }

  [PXDBBool]
  [PXFormula(typeof (Selector<Users.loginTypeID, EPLoginType.requireLoginActivation>))]
  protected virtual void Users_IsPendingActivation_CacheAttached(PXCache sender)
  {
  }

  [PXDBBool]
  [PXUIField]
  [PXFormula(typeof (IsNull<Selector<Users.loginTypeID, EPLoginType.isExternal>, False>))]
  protected virtual void Users_Guest_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search2<PX.Objects.CR.Contact.contactID, LeftJoin<Users, On<PX.Objects.CR.Contact.userID, Equal<Users.pKID>>, LeftJoin<BAccount, On<BAccount.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>, Where<Current<Users.guest>, Equal<True>, And<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.person>, Or<Current<Users.guest>, NotEqual<True>, And<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.employee>, And<BAccount.bAccountID, IsNotNull>>>>>>), new System.Type[] {typeof (PX.Objects.CR.Contact.displayName), typeof (PX.Objects.CR.Contact.salutation), typeof (PX.Objects.CR.Contact.fullName), typeof (PX.Objects.CR.Contact.eMail), typeof (Users.username)}, DescriptionField = typeof (PX.Objects.CR.Contact.displayName))]
  [PXRestrictor(typeof (Where<PX.Objects.CR.Contact.userID, IsNull, Or<PX.Objects.CR.Contact.userID, Equal<Current<Users.pKID>>>>), "Contact {0} already associated with another user.", new System.Type[] {typeof (PX.Objects.CR.Contact.displayName)})]
  protected virtual void Users_ContactID_CacheAttached(PXCache sender)
  {
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Force User to Change Password on Next Login")]
  [PXFormula(typeof (Switch<Case<Where<Selector<Users.loginTypeID, EPLoginType.resetPasswordOnLogin>, Equal<True>>, True>, False>))]
  protected virtual void Users_PasswordChangeOnNextLogin_CacheAttached(PXCache sender)
  {
  }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Generate Password")]
  protected virtual void Users_GeneratePassword_CacheAttached(PXCache sender)
  {
  }

  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (Users.pKID))]
  protected virtual void UserPreferences_UserID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void Contact_ContactType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    EPLoginType epLoginType = PXResultset<EPLoginType>.op_Implicit(PXSelectBase<EPLoginType, PXSelect<EPLoginType, Where<EPLoginType.loginTypeID, Equal<Current<Users.loginTypeID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (epLoginType == null)
      e.NewValue = (object) "EP";
    else if (epLoginType.Entity == "E")
      e.NewValue = (object) "EP";
    else
      e.NewValue = (object) "PN";
  }

  protected virtual void Users_ContactID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is Users row))
      return;
    PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<Users.contactID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    }));
    if (contact != null && string.IsNullOrEmpty(contact.EMail) && row.Source.GetValueOrDefault() != 1)
      throw new PXSetPropertyException("Contact '{0}' does not have an email address.", new object[1]
      {
        (object) contact.DisplayName
      });
  }

  [PXMergeAttributes]
  [PXIntList(new int[] {0, 3, 4}, new string[] {"Basic Authentication", "OAuth2", "Plug-In"})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<EMailAccount.authenticationMethod> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  [PXSelector(typeof (Search<Roles.rolename>), DescriptionField = typeof (Roles.descr))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<EPLoginTypeAllowsRole.rolename> e)
  {
  }

  protected virtual IEnumerable roleList()
  {
    yield break;
  }

  [PXUIField]
  [PXButton]
  public IEnumerable addADUser(PXAdapter adapter)
  {
    ((PXSelectBase<ADUserFilter>) this.ADUser).AskExt();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public IEnumerable addADUserOK(PXAdapter adapter)
  {
    this.ADUser.VerifyRequired();
    Users adUserByName = this.GetADUserByName(((PXSelectBase<ADUserFilter>) this.ADUser).Current.Username);
    if (adUserByName != null)
    {
      PXActiveDirectorySyncMembershipProvider.CheckAndRenameDeletedADUser(adUserByName.Username, adUserByName.ExtRef);
      if (adapter.ImportFlag)
      {
        ((PXSelectBase<Users>) this.UserList).Insert(adUserByName);
      }
      else
      {
        AccessUsers instance = PXGraph.CreateInstance<AccessUsers>();
        ((PXSelectBase<Users>) instance.UserList).Insert(adUserByName);
        throw new PXRedirectRequiredException((PXGraph) instance, "New AD User");
      }
    }
    return adapter.Get();
  }

  private Users GetADUserByName(string name)
  {
    if (name == null)
      return (Users) null;
    PX.Data.Access.ActiveDirectory.User userByLogin = this.ActiveDirectoryProvider.GetUserByLogin(name, false);
    if (userByLogin == null || string.CompareOrdinal(name, userByLogin.Name.DomainLogin) != 0)
      return (Users) null;
    Users instance = (Users) ((PXSelectBase) this.UserList).Cache.CreateInstance();
    instance.Fill(userByLogin);
    return instance;
  }

  [PXUIField]
  [PXButton]
  public IEnumerable reloadADUsers(PXAdapter adapter)
  {
    this.ActiveDirectoryProvider.Reset();
    this.ActiveDirectoryProvider.GetUsers(true);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public IEnumerable generateOneTimeCodes(PXAdapter adapter)
  {
    Users current = ((PXSelectBase<Users>) this.UserList).Current;
    if (current == null)
      return adapter.Get();
    MultifactorServiceHelper.GenerateCodesAndShowReport(this._multiFactorService, current.PKID.Value);
    return adapter.Get();
  }

  [PXUIField(Visible = false)]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable viewEMailAccount(PXAdapter adapter)
  {
    EMailAccount current = ((PXSelectBase<EMailAccount>) this.EMailAccounts).Current;
    if (current == null)
      return adapter.Get();
    PXRedirectHelper.TryRedirect(((PXGraph) this).Caches[typeof (EMailAccount)], (object) current, "", (PXRedirectHelper.WindowMode) 1);
    return adapter.Get();
  }

  [PXUIField(Enabled = true)]
  [PXButton]
  public virtual IEnumerable addEMailAccount(PXAdapter adapter)
  {
    EMailAccountMaint instance = PXGraph.CreateInstance<EMailAccountMaint>();
    Users current = ((PXSelectBase<Users>) this.UserList).Current;
    if (instance == null || current == null)
      return adapter.Get();
    EMailAccount emailAccount = ((PXSelectBase<EMailAccount>) instance.EMailAccounts).Insert();
    if (emailAccount == null)
      return adapter.Get();
    emailAccount.UserID = current.PKID;
    ((PXSelectBase<EMailAccount>) instance.EMailAccounts).Update(emailAccount);
    PXRedirectHelper.TryRedirect(((PXSelectBase) instance.EMailAccounts).Cache, (object) emailAccount, "", (PXRedirectHelper.WindowMode) 1);
    return adapter.Get();
  }

  protected virtual void Users_MultiFactorOverride_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if ((e.Row is Users row ? (row.MultiFactorOverride.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ((PXSelectBase<PreferencesSecurity>) this.Preferences).Update(((PXSelectBase<PreferencesSecurity>) this.Preferences).Select(Array.Empty<object>()).FirstTableItems.FirstOrDefault<PreferencesSecurity>());
  }

  protected virtual void Users_OverrideADRoles_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    Users row = (Users) e.Row;
    int num1 = row.OverrideADRoles.GetValueOrDefault() ? 1 : 0;
    bool flag = e.NewValue != null && Convert.ToBoolean(e.NewValue);
    int num2 = flag ? 1 : 0;
    if (num1 == num2 || flag || row.Source.GetValueOrDefault() != 1 || ((PXSelectBase<UsersInRoles>) this.RolesByUser).SelectSingle(Array.Empty<object>()) == null)
      return;
    if (((PXSelectBase<Users>) this.UserList).Ask("Confirmation", PXMessages.LocalizeFormatNoPrefixNLA("All Local Roles of User '{0}' will be deleted.", new object[1]
    {
      (object) row.Username
    }), (MessageButtons) 4, (MessageIcon) 3) != 6)
    {
      e.NewValue = (object) true;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      foreach (PXResult<UsersInRoles> pxResult in ((PXSelectBase<UsersInRoles>) this.RolesByUser).Select(Array.Empty<object>()))
        ((PXSelectBase<UsersInRoles>) this.RolesByUser).Delete(PXResult<UsersInRoles>.op_Implicit(pxResult));
    }
  }

  protected virtual void Users_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    Users row = (Users) e.Row;
    if (row == null)
      return;
    base.Users_RowSelected(sender, e);
    PXAction<Users> generateOneTimeCodes = this.GenerateOneTimeCodes;
    int? multiFactorType = row.MultiFactorType;
    int num1 = 0;
    int num2 = multiFactorType.GetValueOrDefault() > num1 & multiFactorType.HasValue ? 1 : 0;
    ((PXAction) generateOneTimeCodes).SetVisible(num2 != 0);
    ((PXSelectBase) this.AllowedRoles).Cache.AllowInsert = false;
    PXDefaultAttribute.SetPersistingCheck<Users.contactID>(sender, (object) row, !row.Guest.GetValueOrDefault() || Anonymous.IsAnonymous(row.Username) ? (PXPersistingCheck) 2 : (PXPersistingCheck) 0);
    bool? nullable1;
    if (row.Source.GetValueOrDefault() == 1)
    {
      string str = (string) null;
      nullable1 = row.OverrideADRoles;
      if (nullable1.GetValueOrDefault())
      {
        str = "Roles assigned to the Active Directory groups will be ignored.";
      }
      else
      {
        string[] mappedRolesBySid = Extensions.GetADMappedRolesBySID(this.ActiveDirectoryProvider, row.ExtRef);
        if (mappedRolesBySid == null || ((IEnumerable<string>) mappedRolesBySid).Count<string>() == 0)
          str = "No roles mapped to Active Directory groups have been found.";
      }
      PXUIFieldAttribute.SetWarning<Users.overrideADRoles>(sender, (object) row, str);
    }
    PXCache pxCache = sender;
    Users users1 = row;
    nullable1 = row.MultiFactorOverride;
    int num3 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<Users.multiFactorType>(pxCache, (object) users1, num3 != 0);
    PX.Objects.CR.Contact contact = GraphHelper.RowCast<PX.Objects.CR.Contact>((IEnumerable) PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.userID, Equal<Required<PX.Objects.CR.Contact.userID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) row.PKID
    })).FirstOrDefault<PX.Objects.CR.Contact>();
    int? nullable2 = row.ContactID;
    if (!nullable2.HasValue)
    {
      Users users2 = row;
      int? nullable3;
      if (contact == null)
      {
        nullable2 = new int?();
        nullable3 = nullable2;
      }
      else
        nullable3 = contact.ContactID;
      users2.ContactID = nullable3;
    }
    EPLoginType epLoginType = ((PXSelectBase<EPLoginType>) this.LoginType).SelectSingle(Array.Empty<object>());
    if (epLoginType != null)
    {
      nullable1 = epLoginType.DisableTwoFactorAuth;
      if (nullable1.GetValueOrDefault() || epLoginType.AllowedLoginType == "A")
      {
        PXUIFieldAttribute.SetEnabled<Users.multiFactorOverride>(sender, (object) row, false);
        PXUIFieldAttribute.SetEnabled<Users.multiFactorType>(sender, (object) row, false);
      }
      if (epLoginType.Entity == "C")
      {
        nullable1 = (bool?) contact?.IsActive;
        if (nullable1.HasValue && !nullable1.GetValueOrDefault())
        {
          ((PXGraph) this).Actions["EnableLogin"].SetEnabled(false);
          PXUIFieldAttribute.SetWarning<Users.contactID>(sender, (object) row, "The linked contact is inactive");
        }
      }
    }
    else
      PXUIFieldAttribute.SetEnabled<Users.multiFactorOverride>(sender, (object) row, true);
    ((PXAction) this.AddEMailAccount).SetEnabled(((PXSelectBase) this.UserList).Cache.GetStatus((object) row) != 2);
  }

  protected virtual void Users_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    Users row = e.Row as Users;
    if (!(e.NewRow is Users newRow) || row == null)
      return;
    int? contactId1 = row.ContactID;
    int? contactId2 = newRow.ContactID;
    if (!(contactId1.GetValueOrDefault() == contactId2.GetValueOrDefault() & contactId1.HasValue == contactId2.HasValue))
    {
      foreach (PXResult<PX.Objects.CR.Contact, PX.Objects.EP.EPEmployee> pxResult in PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, LeftJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.EP.EPEmployee.defContactID>, And<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.EP.EPEmployee.parentBAccountID>>>>, Where<PX.Objects.CR.Contact.userID, Equal<Current<Users.pKID>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
      {
        (object) row
      }, Array.Empty<object>()))
      {
        PX.Objects.CR.Contact contact1;
        PX.Objects.EP.EPEmployee epEmployee1;
        pxResult.Deconstruct(ref contact1, ref epEmployee1);
        PX.Objects.CR.Contact contact2 = contact1;
        PX.Objects.EP.EPEmployee epEmployee2 = epEmployee1;
        contact2.UserID = new Guid?();
        ((PXSelectBase<PX.Objects.CR.Contact>) this.contact).Update(contact2);
        if (epEmployee2 != null && epEmployee2.BAccountID.HasValue)
        {
          epEmployee2.UserID = new Guid?();
          ((PXSelectBase<PX.Objects.EP.EPEmployee>) this.Employee).Update(epEmployee2);
        }
      }
      PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Current<Users.contactID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) newRow
      }, Array.Empty<object>()));
      if (contact != null)
      {
        contact.UserID = newRow.PKID;
        ((PXSelectBase<PX.Objects.CR.Contact>) this.contact).Update(contact);
        PX.Objects.EP.EPEmployee epEmployee = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelectJoin<PX.Objects.EP.EPEmployee, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.EP.EPEmployee.defContactID>, And<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.EP.EPEmployee.parentBAccountID>>>>, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) contact.ContactID
        }));
        if (epEmployee != null)
        {
          PX.Objects.EP.EPEmployee copy = PXCache<PX.Objects.EP.EPEmployee>.CreateCopy(epEmployee);
          copy.UserID = newRow.PKID;
          ((PXSelectBase<PX.Objects.EP.EPEmployee>) this.Employee).Update(copy);
        }
      }
    }
    bool? guest = newRow.Guest;
    if (guest.GetValueOrDefault())
    {
      guest = row.Guest;
      if (!guest.GetValueOrDefault())
      {
        if (((PXSelectBase) this.contact).View.Ask("Unable to link employee with guest user. Do you want to proceed operation and remove link?", (MessageButtons) 4) == 6)
          return;
        ((CancelEventArgs) e).Cancel = true;
        return;
      }
    }
    guest = newRow.Guest;
    if (guest.GetValueOrDefault())
      return;
    guest = row.Guest;
    if (!guest.GetValueOrDefault() || ((PXSelectBase) this.contact).View.Ask("Unable to link contact with non guest user. Do you want to proceed operation and remove link?", (MessageButtons) 4) == 6)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void Users_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    Users oldRow = e.OldRow as Users;
    Users row = e.Row as Users;
    PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.userID, Equal<Current<Users.pKID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    if (row == null || oldRow == null)
      return;
    bool? guest1 = row.Guest;
    bool? guest2 = oldRow.Guest;
    if (guest1.GetValueOrDefault() == guest2.GetValueOrDefault() & guest1.HasValue == guest2.HasValue || contact == null)
      return;
    foreach (EPCompanyTreeMember companyTreeMember in ((PXSelectBase) this.Members).View.SelectMultiBound((object[]) new PX.Objects.CR.Contact[1]
    {
      contact
    }, Array.Empty<object>()))
      ((PXSelectBase<EPCompanyTreeMember>) this.Members).Delete(companyTreeMember);
  }

  protected virtual void Users_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    PX.Objects.CR.Contact contact = (PX.Objects.CR.Contact) ((PXSelectBase) this.contact).View.SelectSingleBound(new object[1]
    {
      e.Row
    }, Array.Empty<object>());
    if (contact != null)
    {
      PX.Objects.EP.EPEmployee epEmployee = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(((PXSelectBase<PX.Objects.EP.EPEmployee>) this.Employee).Select(new object[1]
      {
        (object) contact.ContactID
      }));
      if (epEmployee != null)
      {
        PX.Objects.EP.EPEmployee copy = PXCache<PX.Objects.EP.EPEmployee>.CreateCopy(epEmployee);
        copy.UserID = new Guid?();
        ((PXSelectBase<PX.Objects.EP.EPEmployee>) this.Employee).Update(copy);
      }
    }
    foreach (EPCompanyTreeMember companyTreeMember in ((PXSelectBase) this.Members).View.SelectMultiBound((object[]) new PX.Objects.CR.Contact[1]
    {
      contact
    }, Array.Empty<object>()))
      ((PXSelectBase<EPCompanyTreeMember>) this.Members).Delete(companyTreeMember);
  }

  protected virtual void Users_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    Users row = (Users) e.Row;
    if (row == null)
      return;
    using (IEnumerator<PXResult<EPAssignmentRoute>> enumerator = PXSelectBase<EPAssignmentRoute, PXSelectJoin<EPAssignmentRoute, InnerJoin<EPAssignmentMap, On<EPAssignmentRoute.assignmentMapID, Equal<EPAssignmentMap.assignmentMapID>>>, Where<EPAssignmentRoute.ownerID, Equal<Current<Users.contactID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXResult<EPAssignmentRoute, EPAssignmentMap> current = (PXResult<EPAssignmentRoute, EPAssignmentMap>) enumerator.Current;
        throw new PXSetPropertyException("User '{0}' participate in the Assignment and Approval Map '{1}'", new object[2]
        {
          (object) row.Username,
          (object) PXResult<EPAssignmentRoute, EPAssignmentMap>.op_Implicit(current).Name
        });
      }
    }
  }

  protected virtual void Users_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    Users row = (Users) e.Row;
    PX.Objects.CR.Contact contact = (PX.Objects.CR.Contact) ((PXSelectBase) this.contact).View.SelectSingleBound(new object[1]
    {
      (object) row
    }, Array.Empty<object>());
    if (row == null || contact == null)
      return;
    Guid? pkid = row.PKID;
    Guid? userId = contact.UserID;
    if ((pkid.HasValue == userId.HasValue ? (pkid.HasValue ? (pkid.GetValueOrDefault() == userId.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      return;
    PX.Objects.CR.Contact copy1 = (PX.Objects.CR.Contact) ((PXSelectBase) this.contact).Cache.CreateCopy((object) contact);
    copy1.UserID = row.PKID;
    ((PXSelectBase<PX.Objects.CR.Contact>) this.contact).Update(copy1);
    PX.Objects.EP.EPEmployee epEmployee = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(((PXSelectBase<PX.Objects.EP.EPEmployee>) this.Employee).Select(new object[1]
    {
      (object) copy1.ContactID
    }));
    if (epEmployee == null)
      return;
    PX.Objects.EP.EPEmployee copy2 = PXCache<PX.Objects.EP.EPEmployee>.CreateCopy(epEmployee);
    copy2.UserID = row.PKID;
    ((PXSelectBase<PX.Objects.EP.EPEmployee>) this.Employee).Update(copy2);
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<Users, Users.allowedSessions> e)
  {
    if (((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<Users, Users.allowedSessions>>) e).ReturnValue != null || e.Row == null || !e.Row.LoginTypeID.HasValue)
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<Users, Users.allowedSessions>>) e).ReturnValue = (object) (int?) ((PXSelectBase<EPLoginType>) this.LoginType).SelectSingle(Array.Empty<object>())?.AllowedSessions;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<Users, Users.allowedSessions> e)
  {
    if (((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<Users, Users.allowedSessions>>) e).NewValue == null || !e.Row.LoginTypeID.HasValue)
      return;
    int? allowedSessions = (int?) ((PXSelectBase<EPLoginType>) this.LoginType).SelectSingle(Array.Empty<object>())?.AllowedSessions;
    int newValue = (int) ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<Users, Users.allowedSessions>>) e).NewValue;
    if (!(allowedSessions.GetValueOrDefault() == newValue & allowedSessions.HasValue))
      return;
    ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<Users, Users.allowedSessions>>) e).NewValue = (object) null;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<Users, Users.loginTypeID> e)
  {
    if (e.NewValue == null || ((PXSelectBase<EPLoginType>) this.LoginType).SelectSingle(Array.Empty<object>()) == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<Users, Users.loginTypeID>>) e).Cache.SetValueExt<Users.allowedSessions>((object) e.Row, (object) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<Users, Users.allowedSessions> e)
  {
    if (!(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Users, Users.allowedSessions>, Users, object>) e).NewValue is int newValue))
      return;
    PXLicense license = this._licensing.License;
    int val1 = WebConfig.MaximumAllowedSessionsCount ?? int.MaxValue;
    int val2;
    switch (((PXSelectBase<EPLoginType>) this.LoginType).SelectSingle(Array.Empty<object>())?.AllowedLoginType)
    {
      case "U":
        val2 = license.UsersAllowed;
        break;
      case "A":
        val2 = license.MaxApiUsersAllowed;
        break;
      default:
        val2 = Math.Max(license.UsersAllowed, license.MaxApiUsersAllowed);
        break;
    }
    int num = Math.Max(3, Math.Min(val1, val2));
    if (newValue < 1 || newValue > num)
      throw new PXSetPropertyException("The value in the Max. Number of Concurrent Logins box should be between {1} and {0}.", new object[2]
      {
        (object) num,
        (object) 1
      });
  }

  protected virtual void SendUserNotification(int? accountId, Notification notification)
  {
    TemplateNotificationGenerator notificationGenerator = TemplateNotificationGenerator.Create((object) ((PXSelectBase<Users>) this.UserList).Current, notification);
    notificationGenerator.MailAccountId = accountId;
    notificationGenerator.To = ((PXSelectBase<Users>) this.UserList).Current.Email;
    notificationGenerator.Body = notificationGenerator.Body.Replace("((UserList.Password))", ((PXSelectBase<Users>) this.UserList).Current.Password);
    notificationGenerator.Body = notificationGenerator.Body.Replace("((UserList.RecoveryLink))", ((PXSelectBase<Users>) this.UserList).Current.RecoveryLink);
    notificationGenerator.LinkToEntity = true;
    notificationGenerator.Send();
  }

  public virtual void Persist()
  {
    foreach (Users users in ((PXSelectBase) this.UserList).Cache.Deleted)
    {
      PX.Objects.CR.Contact contact = (PX.Objects.CR.Contact) ((PXSelectBase) this.contact).View.SelectSingleBound(new object[1]
      {
        (object) users
      }, Array.Empty<object>());
      if (contact != null)
      {
        contact.UserID = new Guid?();
        ((PXSelectBase<PX.Objects.CR.Contact>) this.contact).Update(contact);
        PX.Objects.EP.EPEmployee epEmployee = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(((PXSelectBase<PX.Objects.EP.EPEmployee>) this.Employee).Select(new object[1]
        {
          (object) contact.ContactID
        }));
        if (epEmployee != null)
        {
          PX.Objects.EP.EPEmployee copy = PXCache<PX.Objects.EP.EPEmployee>.CreateCopy(epEmployee);
          copy.UserID = new Guid?();
          ((PXSelectBase<PX.Objects.EP.EPEmployee>) this.Employee).Update(copy);
        }
      }
    }
    bool? overrideAdRoles;
    int? source;
    if (((PXSelectBase<Users>) this.UserList).Current != null)
    {
      overrideAdRoles = ((PXSelectBase<Users>) this.UserList).Current.OverrideADRoles;
      if (!overrideAdRoles.GetValueOrDefault())
      {
        source = ((PXSelectBase<Users>) this.UserList).Current.Source;
        if (source.GetValueOrDefault() == 1)
        {
          foreach (PXResult<UsersInRoles> pxResult in ((PXSelectBase<UsersInRoles>) this.RoleList).Select(Array.Empty<object>()))
            ((PXSelectBase<UsersInRoles>) this.RoleList).Delete(PXResult<UsersInRoles>.op_Implicit(pxResult));
        }
      }
    }
    if (((PXSelectBase<Users>) this.UserList).Current != null)
    {
      overrideAdRoles = ((PXSelectBase<Users>) this.UserList).Current.OverrideADRoles;
      if (overrideAdRoles.GetValueOrDefault())
      {
        source = ((PXSelectBase<Users>) this.UserList).Current.Source;
        if (source.GetValueOrDefault() == 1 && ((PXSelectBase<UsersInRoles>) this.RolesByUser).SelectSingle(Array.Empty<object>()) == null)
          ((PXSelectBase<Users>) this.UserListCurrent).Current.OverrideADRoles = new bool?(false);
      }
    }
    base.Persist();
  }

  public virtual void ClearDependencies()
  {
    PXDatabase.SelectTimeStamp();
    PXPageCacheUtils.InvalidateCachedPages();
  }

  private bool ShouldCacheADUsers()
  {
    try
    {
      return this.ActiveDirectoryProvider.GetUsers(true).Count<PX.Data.Access.ActiveDirectory.User>() > this.Options.Value.ADGroupCacheLimit;
    }
    catch (Exception ex)
    {
      this._logger.Warning(ex, "Unable to get Active Directory users count");
      return false;
    }
  }
}
