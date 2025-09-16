// Decompiled with JetBrains decompiler
// Type: PX.SM.MyProfileMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Outlook.Services;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.SM;

public class MyProfileMaint : SMAccessPersonalMaint
{
  private bool _needRefresh;
  public 
  #nullable disable
  PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.userID, Equal<Optional<Users.pKID>>>> Contact;
  [PXHidden]
  public PXSelect<EPEmployee, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>> Employee;
  public PXSelectJoin<EMailSyncAccount, InnerJoin<BAccount, On<BAccount.bAccountID, Equal<EMailSyncAccount.employeeID>>, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<BAccount.defContactID>, And<PX.Objects.CR.Contact.userID, Equal<Optional<Users.pKID>>>>>>> SyncAccount;
  public PXSelect<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Optional<EMailSyncAccount.emailAccountID>>>> EMailAccountsNew;
  public PXFilter<CustomerManagementFeature> CustomerModule;
  public FbqlSelect<SelectFromBase<NotificationSetupUserOverride, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  NotificationSetupUserOverride.userID, IBqlGuid>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  Users.pKID, IBqlGuid>.FromCurrent>>, 
  #nullable disable
  NotificationSetupUserOverride>.View Notifications;

  [InjectDependency]
  private IAdvancedAuthenticationRestrictor AdvancedAuthenticationRestrictor { get; set; }

  [InjectDependency]
  private IOutlookOidcProviderProvider OutlookOidcProviderProvider { get; set; }

  [PXDBString(50)]
  [PXUIField]
  [PhoneValidation]
  protected virtual void Users_Phone_CacheAttached(PXCache sender)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  protected virtual void _(PX.Data.Events.CacheAttached<Users.username> e)
  {
  }

  protected virtual void UserPreferences_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    base.UserPreferences_RowSelected(sender, e);
    ((PXAction) this.ResetTimeZone).SetVisible(true);
  }

  protected virtual void UserPreferences_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != 1 || !string.Equals(((PXSelectBase<Users>) this.UserProfile).Current.Username, PXAccess.GetUserName()))
      return;
    this._needRefresh = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CustomerManagementFeature, CustomerManagementFeature.isOutlookOidcEnabled> e)
  {
    PX.Data.Events.FieldDefaulting<CustomerManagementFeature, CustomerManagementFeature.isOutlookOidcEnabled> fieldDefaulting = e;
    CustomerManagementFeature row = e.Row;
    int num;
    if (row != null)
    {
      bool? integrationInstalled = row.IsOutlookIntegrationInstalled;
      if (integrationInstalled.HasValue && integrationInstalled.GetValueOrDefault())
      {
        bool? connectInstalled = row.IsOpenIDConnectInstalled;
        if (connectInstalled.HasValue && connectInstalled.GetValueOrDefault())
        {
          num = this.OutlookOidcProviderProvider.TryGetCurrentProvider() != null ? 1 : 0;
          goto label_5;
        }
      }
    }
    num = 0;
label_5:
    // ISSUE: variable of a boxed type
    __Boxed<bool> local = (ValueType) (bool) num;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CustomerManagementFeature, CustomerManagementFeature.isOutlookOidcEnabled>, CustomerManagementFeature, object>) fieldDefaulting).NewValue = (object) local;
  }

  public virtual void Persist()
  {
    base.Persist();
    if (this._needRefresh)
      throw new PXRefreshException();
  }

  public virtual string GetUserTimeZoneId(string username)
  {
    string userTimeZoneId = base.GetUserTimeZoneId(username);
    if (string.IsNullOrEmpty(userTimeZoneId))
    {
      PXResultset<CSCalendar> pxResultset = PXSelectBase<CSCalendar, PXSelectJoin<CSCalendar, InnerJoin<EPEmployee, On<EPEmployee.calendarID, Equal<CSCalendar.calendarID>>, InnerJoin<Users, On<Users.pKID, Equal<EPEmployee.userID>>>>, Where<Users.username, Equal<Required<Users.username>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) username
      });
      if (pxResultset != null && pxResultset.Count > 0)
        userTimeZoneId = ((CSCalendar) ((PXResult) pxResultset[0])[typeof (CSCalendar)]).TimeZone;
    }
    return userTimeZoneId;
  }

  protected virtual string GetDefaultUserTimeZoneId(string username)
  {
    CSCalendar csCalendar = PXResultset<CSCalendar>.op_Implicit(PXSelectBase<CSCalendar, PXSelectJoin<CSCalendar, InnerJoin<EPEmployee, On<EPEmployee.calendarID, Equal<CSCalendar.calendarID>>, InnerJoin<Users, On<Users.pKID, Equal<EPEmployee.userID>>>>, Where<Users.username, Equal<Required<Users.username>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) username
    }));
    return csCalendar != null && !string.IsNullOrEmpty(csCalendar.TimeZone) ? csCalendar.TimeZone : base.GetDefaultUserTimeZoneId(username);
  }

  [PXUIField]
  [PXButton]
  public virtual void changeEmail()
  {
    base.changeEmail();
    foreach (PX.Objects.CR.Contact contact in GraphHelper.RowCast<PX.Objects.CR.Contact>((IEnumerable) ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Select(new object[1]
    {
      (object) ((PXSelectBase<Users>) this.UserProfile).Current.PKID
    })).Select<PX.Objects.CR.Contact, PX.Objects.CR.Contact>((Func<PX.Objects.CR.Contact, PX.Objects.CR.Contact>) (contact => (PX.Objects.CR.Contact) ((PXSelectBase) this.Contact).Cache.CreateCopy((object) contact))))
    {
      contact.EMail = ((PXSelectBase<Users>) this.UserProfile).Current.Email;
      ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Update(contact);
    }
    foreach (EMailSyncAccount emailSyncAccount in GraphHelper.RowCast<EMailSyncAccount>((IEnumerable) ((PXSelectBase<EMailSyncAccount>) this.SyncAccount).Select(new object[1]
    {
      (object) ((PXSelectBase<Users>) this.UserProfile).Current.PKID
    })).Select<EMailSyncAccount, EMailSyncAccount>((Func<EMailSyncAccount, EMailSyncAccount>) (account => (EMailSyncAccount) ((PXSelectBase) this.SyncAccount).Cache.CreateCopy((object) account))))
    {
      emailSyncAccount.Address = ((PXSelectBase<Users>) this.UserProfile).Current.Email;
      emailSyncAccount.ContactsExportDate = new DateTime?();
      emailSyncAccount.ContactsImportDate = new DateTime?();
      emailSyncAccount.EmailsExportDate = new DateTime?();
      emailSyncAccount.EmailsImportDate = new DateTime?();
      emailSyncAccount.TasksExportDate = new DateTime?();
      emailSyncAccount.TasksImportDate = new DateTime?();
      emailSyncAccount.EventsExportDate = new DateTime?();
      emailSyncAccount.EventsImportDate = new DateTime?();
      EMailAccount emailAccount = PXResultset<EMailAccount>.op_Implicit(((PXSelectBase<EMailAccount>) this.EMailAccountsNew).Select(new object[1]
      {
        (object) emailSyncAccount.EmailAccountID
      }));
      emailAccount.Address = emailSyncAccount.Address;
      ((PXSelectBase<EMailAccount>) this.EMailAccountsNew).Update(emailAccount);
      ((PXSelectBase<EMailSyncAccount>) this.SyncAccount).Update(emailSyncAccount);
    }
    ((PXGraph) this).Actions.PressSave();
  }

  /// <exclude />
  public class MyProfileMaint_EPEmployeeDelegateExtension : 
    EPEmployeeDelegateExtension<MyProfileMaint>
  {
    public static bool IsActive()
    {
      return EPEmployeeDelegateExtension<MyProfileMaint>.IsExtensionActive();
    }

    public override void Initialize()
    {
      base.Initialize();
      ((PXSelectBase<EPWingman>) this.Delegates).Join<InnerJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<EPWingman.employeeID>>>>();
      ((PXSelectBase<EPWingman>) this.Delegates).WhereNew<Where2<Where<BqlOperand<EPEmployee.userID, IBqlGuid>.IsEqual<BqlField<AccessInfo.userID, IBqlGuid>.FromCurrent>>, And2<Where<EPWingman.delegationOf, NotEqual<EPDelegationOf.approvals>, Or<FeatureInstalled<FeaturesSet.approvalWorkflow>>>, And2<Where<EPWingman.delegationOf, NotEqual<EPDelegationOf.expenses>, Or<FeatureInstalled<FeaturesSet.expenseManagement>>>, And<Where<EPWingman.delegationOf, NotEqual<EPDelegationOf.timeEntries>, Or2<FeatureInstalled<FeaturesSet.financialAdvanced>, Or<FeatureInstalled<FeaturesSet.timeReportingModule>>>>>>>>>();
    }

    [PXMergeAttributes]
    [PXDBDefault(typeof (Search<EPEmployee.bAccountID, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>))]
    protected virtual void _(PX.Data.Events.CacheAttached<EPWingman.employeeID> e)
    {
    }

    [PXMergeAttributes]
    [PXRestrictor(typeof (Where<EPEmployee.userID, NotEqual<Current<AccessInfo.userID>>>), null, new System.Type[] {})]
    protected virtual void _(PX.Data.Events.CacheAttached<EPWingman.wingmanID> e)
    {
    }

    protected virtual void _(PX.Data.Events.RowSelected<Users> e)
    {
      if (e.Row == null)
        return;
      ((PXSelectBase) this.Delegates).Cache.AllowSelect = ((IEnumerable<PXResult<EPEmployee>>) ((PXSelectBase<EPEmployee>) this.Base.Employee).Select(Array.Empty<object>())).ToList<PXResult<EPEmployee>>().Any<PXResult<EPEmployee>>();
    }
  }
}
