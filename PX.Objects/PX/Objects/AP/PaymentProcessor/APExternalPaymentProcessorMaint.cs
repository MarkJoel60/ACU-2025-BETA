// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.APExternalPaymentProcessorMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Webhooks.DAC;
using PX.Api.Webhooks.Graph;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP.PaymentProcessor.Common;
using PX.Objects.AP.PaymentProcessor.DAC;
using PX.Objects.AP.PaymentProcessor.Managers;
using PX.Objects.Common.Scopes;
using PX.PaymentProcessor.Data;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor;

public class APExternalPaymentProcessorMaint : 
  PXGraph<APExternalPaymentProcessorMaint, APExternalPaymentProcessor>
{
  public FbqlSelect<
  #nullable disable
  SelectFromBase<
  #nullable enable
  APExternalPaymentProcessor, 
  #nullable disable
  TypeArrayOf<IFbqlJoin>.Empty>, 
  #nullable enable
  APExternalPaymentProcessor>.View ExternalPaymentProcessor;
  public FbqlSelect<
  #nullable disable
  SelectFromBase<
  #nullable enable
  APExternalPaymentProcessor, 
  #nullable disable
  TypeArrayOf<IFbqlJoin>.Empty>.Where<
  #nullable enable
  BqlOperand<APExternalPaymentProcessor.externalPaymentProcessorID, IBqlString>.IsEqual<BqlField<APExternalPaymentProcessor.externalPaymentProcessorID, IBqlString>.FromCurrent>>, APExternalPaymentProcessor>.View CurrentExternalPaymentProcessor;
  public FbqlSelect<
  #nullable disable
  SelectFromBase<
  #nullable enable
  APPaymentProcessorOrganization, 
  #nullable disable
  TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<
  #nullable enable
  PX.Objects.GL.DAC.Organization>.On<BqlOperand<PX.Objects.GL.DAC.Organization.organizationID, IBqlInt>.IsEqual<APPaymentProcessorOrganization.organizationID>>>>.Where<BqlChainableConditionBase<
  #nullable disable
  TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  APPaymentProcessorOrganization.externalPaymentProcessorID, 
  #nullable disable
  Equal<
  #nullable enable
  BqlField<APExternalPaymentProcessor.externalPaymentProcessorID, IBqlString>.FromCurrent>>>>>.And<MatchWithOrganization<PX.Objects.GL.DAC.Organization.organizationID>>>, APPaymentProcessorOrganization>.View PaymentProcessorOrganizations;
  public FbqlSelect<
  #nullable disable
  SelectFromBase<
  #nullable enable
  APPaymentProcessorUser, 
  #nullable disable
  TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<
  #nullable enable
  Users>.On<BqlOperand<Users.pKID, IBqlGuid>.IsEqual<APPaymentProcessorUser.userID>>>>.Where<BqlChainableConditionBase<
  #nullable disable
  TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  APPaymentProcessorUser.externalPaymentProcessorID, 
  #nullable disable
  Equal<
  #nullable enable
  BqlField<APExternalPaymentProcessor.externalPaymentProcessorID, IBqlString>.FromCurrent>>>>>.And<BqlOperand<APPaymentProcessorUser.organizationID, IBqlInt>.IsEqual<BqlField<APPaymentProcessorOrganization.organizationID, IBqlInt>.FromCurrent>>>, APPaymentProcessorUser>.View PaymentProcessorUsers;
  public FbqlSelect<
  #nullable disable
  SelectFromBase<
  #nullable enable
  APPaymentProcessorAccount, 
  #nullable disable
  TypeArrayOf<IFbqlJoin>.Empty>.Where<
  #nullable enable
  BqlChainableConditionBase<
  #nullable disable
  TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  APPaymentProcessorAccount.externalPaymentProcessorID, 
  #nullable disable
  Equal<
  #nullable enable
  BqlField<APExternalPaymentProcessor.externalPaymentProcessorID, IBqlString>.FromCurrent>>>>>.And<BqlOperand<APPaymentProcessorAccount.organizationID, IBqlInt>.IsEqual<BqlField<APPaymentProcessorOrganization.organizationID, IBqlInt>.FromCurrent>>>.Order<
  #nullable disable
  By<
  #nullable enable
  BqlField<APPaymentProcessorAccount.externalAccountBank, IBqlString>.Asc, BqlField<APPaymentProcessorAccount.externalAccountType, IBqlString>.Asc, BqlField<APPaymentProcessorAccount.externalAccountName, IBqlString>.Asc, BqlField<APPaymentProcessorAccount.externalAccountRoutingNumber, IBqlString>.Asc, BqlField<APPaymentProcessorAccount.externalAccountNumber, IBqlString>.Asc>>, APPaymentProcessorAccount>.View PaymentProcessorAccounts;
  public FbqlSelect<
  #nullable disable
  SelectFromBase<
  #nullable enable
  APPaymentProcessorAccountUser, 
  #nullable disable
  TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<
  #nullable enable
  APPaymentProcessorAccount>.On<BqlChainableConditionBase<
  #nullable disable
  TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  APPaymentProcessorAccountUser.externalAccountID, 
  #nullable disable
  Equal<
  #nullable enable
  APPaymentProcessorAccount.externalAccountID>>>>>.And<BqlOperand<APPaymentProcessorAccountUser.externalPaymentProcessorID, IBqlString>.IsEqual<APPaymentProcessorAccount.externalPaymentProcessorID>>>>, 
  #nullable disable
  FbqlJoins.Left<
  #nullable enable
  APPaymentProcessorUser>.On<BqlChainableConditionBase<
  #nullable disable
  TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  APPaymentProcessorAccountUser.externalUserID, 
  #nullable disable
  Equal<
  #nullable enable
  APPaymentProcessorUser.externalUserID>>>>>.And<BqlOperand<APPaymentProcessorAccountUser.externalPaymentProcessorID, IBqlString>.IsEqual<APPaymentProcessorUser.externalPaymentProcessorID>>>>, 
  #nullable disable
  FbqlJoins.Left<
  #nullable enable
  Users>.On<BqlOperand<Users.pKID, IBqlGuid>.IsEqual<APPaymentProcessorUser.userID>>>>.Where<BqlChainableConditionBase<
  #nullable disable
  TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  APPaymentProcessorAccountUser.externalPaymentProcessorID, 
  #nullable disable
  Equal<
  #nullable enable
  BqlField<APExternalPaymentProcessor.externalPaymentProcessorID, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<
  #nullable disable
  TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  APPaymentProcessorAccountUser.organizationID, 
  #nullable disable
  Equal<
  #nullable enable
  BqlField<APPaymentProcessorOrganization.organizationID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<APPaymentProcessorAccount.status, IBqlString>.IsIn<AccountStatus.pending, AccountStatus.active>>>>.Order<
  #nullable disable
  By<
  #nullable enable
  BqlField<Users.username, IBqlString>.Asc, BqlField<APPaymentProcessorAccount.externalAccountBank, IBqlString>.Asc, BqlField<APPaymentProcessorAccount.externalAccountType, IBqlString>.Asc, BqlField<APPaymentProcessorAccount.externalAccountNumber, IBqlString>.Asc>>, APPaymentProcessorAccountUser>.View PaymentProcessorAccountUsers;
  public PXSelect<APPaymentProcessorVendor, Where<APPaymentProcessorVendor.externalPaymentProcessorID, Equal<Current<APExternalPaymentProcessor.externalPaymentProcessorID>>>> PaymentProcessorVendors;
  public PXSelect<APPaymentProcessorBill, Where<APPaymentProcessorBill.externalPaymentProcessorID, Equal<Current<APExternalPaymentProcessor.externalPaymentProcessorID>>>> PaymentProcessorBills;
  private const string EppWebHookHandler = "PX.DataSync.ExternalPaymentProcessor.EPPWebhookHandler";
  public PXAction<APExternalPaymentProcessor> connectOrganization;
  public PXAction<APExternalPaymentProcessor> subscribeWebhook;
  public PXAction<APExternalPaymentProcessor> unSubscribeWebhook;
  public PXAction<APExternalPaymentProcessor> syncConnectOrganization;
  public PXAction<APExternalPaymentProcessor> onboardUser;
  public PXAction<APExternalPaymentProcessor> enableUser;
  public PXAction<APExternalPaymentProcessor> disableUser;
  public PXAction<APExternalPaymentProcessor> disableAccount;
  public PXAction<APExternalPaymentProcessor> disableAccountUser;
  public PXAction<APExternalPaymentProcessor> syncManageFundingAccounts;
  public PXAction<APExternalPaymentProcessor> refreshFundingAccounts;
  public PXAction<APExternalPaymentProcessor> refreshFundingAccountUsers;
  public PXAction<APExternalPaymentProcessor> manageFundingAccounts;

  [InjectDependency]
  internal Func<string, PaymentProcessorManager> PaymentProcessorManagerProvider { get; set; }

  [InjectDependency]
  internal ICurrentUserInformationProvider CurrentUserInformationProvider { get; set; }

  [PXSelector(typeof (Users.pKID), SubstituteKey = typeof (Users.fullName), DescriptionField = typeof (Users.fullName), CacheGlobal = true, DirtyRead = true)]
  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By", Enabled = false, Visible = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<APPaymentProcessorAccount.createdByID> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Name", Enabled = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<Users.displayName> e)
  {
  }

  public APExternalPaymentProcessorMaint()
  {
    this.PaymentProcessorAccounts.Cache.AllowInsert = false;
    this.PaymentProcessorAccounts.Cache.AllowDelete = false;
    this.PaymentProcessorAccountUsers.Cache.AllowInsert = false;
    this.PaymentProcessorAccountUsers.Cache.AllowDelete = false;
  }

  [PXUIField(DisplayName = "Onboard")]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable ConnectOrganization(PXAdapter adapter)
  {
    APExternalPaymentProcessor current1 = this.ExternalPaymentProcessor.Current;
    if (current1 == null)
      return adapter.Get();
    PaymentProcessorManager manager = this.GetPaymentProcessorManager(current1.Type);
    APPaymentProcessorOrganization current2 = this.PaymentProcessorOrganizations.Current;
    if (current2 == null)
      return adapter.Get();
    this.Save.Press();
    PX.Objects.GL.DAC.Organization organization = PX.Objects.GL.DAC.Organization.PK.Find((PXGraph) this, current2.OrganizationID);
    APExternalPaymentProcessor copy = this.CopyPaymentProcessor(current1);
    PaymentProcessorSessionHelper.SetUserSessionStore(copy);
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => manager.OnboardOrganizationAsync(copy, organization).GetAwaiter().GetResult()));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Create Webhook")]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable SubscribeWebhook(PXAdapter adapter)
  {
    APExternalPaymentProcessor current1 = this.ExternalPaymentProcessor.Current;
    if (current1 == null)
      return adapter.Get();
    PaymentProcessorManager manager = this.GetPaymentProcessorManager(current1.Type);
    APPaymentProcessorOrganization current2 = this.PaymentProcessorOrganizations.Current;
    if (current2 == null)
      return adapter.Get();
    APPaymentProcessorUser current3 = this.PaymentProcessorUsers.Current;
    if (current3 == null)
      return adapter.Get();
    if (!APExternalPaymentProcessorMaint.IsSecureConnection())
      throw new PXException("Webhooks work only if your Acumatica ERP instance is hosted over HTTPS.");
    this.Save.Press();
    PX.Objects.GL.DAC.Organization organization = PX.Objects.GL.DAC.Organization.PK.Find((PXGraph) this, current2.OrganizationID);
    APExternalPaymentProcessor copy = this.CopyPaymentProcessor(current1);
    PaymentProcessorSessionHelper.SetUserSessionStore(copy);
    string str = $"{current1.ExternalPaymentProcessorID}_{organization.OrganizationCD}".Trim();
    WebhookMaint instance = PXGraph.CreateInstance<WebhookMaint>();
    WebHook webhook = PXSelectBase<WebHook, PXViewOf<WebHook>.BasedOn<SelectFromBase<WebHook, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<WebHook.name, IBqlString>.IsEqual<P.AsString>>>.ReadOnly.Config>.Select((PXGraph) instance, (object) str).TopFirst;
    if (webhook == null)
    {
      webhook = new WebHook()
      {
        Name = str,
        Handler = "PX.DataSync.ExternalPaymentProcessor.EPPWebhookHandler",
        IsSystem = new bool?(true),
        RequestLogLevel = new byte?((byte) 2),
        RequestRetainCount = new short?((short) 1000),
        IsActive = new bool?(true)
      };
      webhook = instance.Webhook.Insert(webhook);
      ((PXGraph<WebhookMaint, WebHook>) instance).Save.Press();
    }
    instance.Webhook.Locate(webhook);
    instance.Webhook.GetValueExt<WebHook.url>(webhook);
    OrganizationUserData orgUserData = this.GetOrganizationUserData(organization.OrganizationID, current3.UserID);
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => manager.SubscribeWebhookAsync(copy, webhook, orgUserData).GetAwaiter().GetResult()));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Delete Webhook")]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable UnSubscribeWebhook(PXAdapter adapter)
  {
    APExternalPaymentProcessor current1 = this.ExternalPaymentProcessor.Current;
    if (current1 == null)
      return adapter.Get();
    PaymentProcessorManager manager = this.GetPaymentProcessorManager(current1.Type);
    APPaymentProcessorOrganization current2 = this.PaymentProcessorOrganizations.Current;
    if (current2 == null || !current2.WebhookID.HasValue)
      return adapter.Get();
    this.Save.Press();
    PX.Objects.GL.DAC.Organization organization = PX.Objects.GL.DAC.Organization.PK.Find((PXGraph) this, current2.OrganizationID);
    APPaymentProcessorUser current3 = this.PaymentProcessorUsers.Current;
    if (current3 == null)
      return adapter.Get();
    APExternalPaymentProcessor copy = this.CopyPaymentProcessor(current1);
    PaymentProcessorSessionHelper.SetUserSessionStore(copy);
    OrganizationUserData orgUserData = this.GetOrganizationUserData(organization.OrganizationID, current3.UserID);
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => manager.UnsubscribeWebhookAsync(copy, orgUserData).GetAwaiter().GetResult()));
    return adapter.Get();
  }

  private static bool IsSecureConnection()
  {
    return HttpContext.Current.Request.IsSecureConnection || string.Equals(HttpContext.Current.Request.Headers["X-Forwarded-Proto"], "https", StringComparison.InvariantCultureIgnoreCase);
  }

  [PXUIField(DisplayName = "", Visible = false)]
  [PXButton]
  public virtual IEnumerable SyncConnectOrganization(PXAdapter adapter)
  {
    string responseStr = adapter.CommandArguments;
    APExternalPaymentProcessor current = this.ExternalPaymentProcessor.Current;
    if (current == null)
      return adapter.Get();
    PaymentProcessorManager manager = this.GetPaymentProcessorManager(current.Type);
    APExternalPaymentProcessor copy = this.CopyPaymentProcessor(current);
    PaymentProcessorSessionHelper.SetUserSessionStore(copy);
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => manager.ProcessOnboardResponseAsync(responseStr, copy).GetAwaiter().GetResult()));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Onboard")]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable OnboardUser(PXAdapter adapter)
  {
    APExternalPaymentProcessor current1 = this.ExternalPaymentProcessor.Current;
    if (current1 == null)
      return adapter.Get();
    this.Save.Press();
    PaymentProcessorManager manager = this.GetPaymentProcessorManager(current1.Type);
    APPaymentProcessorUser current2 = this.PaymentProcessorUsers.Current;
    APPaymentProcessorOrganization current3 = this.PaymentProcessorOrganizations.Current;
    if (current2 == null || current3 == null)
      return adapter.Get();
    APExternalPaymentProcessor copy = this.CopyPaymentProcessor(current1);
    Users user = Users.PK.Find((PXGraph) this, current2.UserID);
    PX.Objects.GL.DAC.Organization organization = PX.Objects.GL.DAC.Organization.PK.Find((PXGraph) this, current3.OrganizationID);
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => manager.CreateUserAsync(copy, organization, user).GetAwaiter().GetResult()));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Activate")]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable EnableUser(PXAdapter adapter)
  {
    APExternalPaymentProcessor externalPaymentProcessor = this.ExternalPaymentProcessor.Current;
    if (externalPaymentProcessor == null)
      return adapter.Get();
    this.Save.Press();
    PaymentProcessorManager manager = this.GetPaymentProcessorManager(externalPaymentProcessor.Type);
    APPaymentProcessorUser current = this.PaymentProcessorUsers.Current;
    if (current == null)
      return adapter.Get();
    OrganizationUserData organizationUser = this.GetOrganizationUserData(current.OrganizationID, current.UserID);
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => manager.RestoreUserAsync(externalPaymentProcessor, organizationUser).GetAwaiter().GetResult()));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Deactivate")]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable DisableUser(PXAdapter adapter)
  {
    APExternalPaymentProcessor externalPaymentProcessor = this.ExternalPaymentProcessor.Current;
    if (externalPaymentProcessor == null)
      return adapter.Get();
    this.Save.Press();
    PaymentProcessorManager manager = this.GetPaymentProcessorManager(externalPaymentProcessor.Type);
    APPaymentProcessorUser current = this.PaymentProcessorUsers.Current;
    if (current == null)
      return adapter.Get();
    OrganizationUserData organizationUser = this.GetOrganizationUserData(current.OrganizationID, current.UserID);
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => manager.ArchiveUserAsync(externalPaymentProcessor, organizationUser).GetAwaiter().GetResult()));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Disable")]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable DisableAccount(PXAdapter adapter)
  {
    APExternalPaymentProcessor externalPaymentProcessor = this.ExternalPaymentProcessor.Current;
    if (externalPaymentProcessor == null)
      return adapter.Get();
    this.Save.Press();
    PaymentProcessorManager manager = this.GetPaymentProcessorManager(externalPaymentProcessor.Type);
    APPaymentProcessorAccount account = this.PaymentProcessorAccounts.Current;
    if (account == null)
      return adapter.Get();
    if (this.ExternalPaymentProcessor.View.Ask((object) null, "Disable Funding Account", PXMessages.LocalizeFormatNoPrefix("The selected funding account will be permanently disabled in the {0} company. Once disabled, it cannot be restored, and the related unprocessed payments will be canceled. To proceed, click Disable.", (object) PX.Objects.GL.DAC.Organization.PK.Find((PXGraph) this, account.OrganizationID).OrganizationCD), MessageButtons.YesNo, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
    {
      {
        WebDialogResult.Yes,
        "Disable"
      },
      {
        WebDialogResult.No,
        "Cancel"
      }
    }, MessageIcon.None) != WebDialogResult.Yes)
      return adapter.Get();
    OrganizationUserData organizationUser = this.GetOrganizationUserData(account.OrganizationID);
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => manager.ArchiveBankAccountAsync(externalPaymentProcessor, account, organizationUser).GetAwaiter().GetResult()));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Disable")]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable DisableAccountUser(PXAdapter adapter)
  {
    APExternalPaymentProcessor externalPaymentProcessor = this.ExternalPaymentProcessor.Current;
    if (externalPaymentProcessor == null)
      return adapter.Get();
    APPaymentProcessorOrganization current1 = this.PaymentProcessorOrganizations.Current;
    if (current1 == null)
      return adapter.Get();
    this.Save.Press();
    PaymentProcessorManager manager = this.GetPaymentProcessorManager(externalPaymentProcessor.Type);
    APPaymentProcessorAccountUser accountUser = this.PaymentProcessorAccountUsers.Current;
    if (accountUser == null)
      return adapter.Get();
    APPaymentProcessorAccount current2 = this.PaymentProcessorAccounts.Current;
    if (current2 == null)
      return adapter.Get();
    APPaymentProcessorUser paymentProcessorUser = this.GeUserByExtUserID(accountUser.ExternalUserID, externalPaymentProcessor.ExternalPaymentProcessorID, current1.OrganizationID);
    if (paymentProcessorUser == null)
      return adapter.Get();
    if (this.ExternalPaymentProcessor.View.Ask((object) null, "Disable Access to Funding Account", PXMessages.LocalizeFormatNoPrefix("Access to the selected funding account will be permanently disabled for the {0} user. The operation is irreversible. To proceed, click Disable.", (object) Users.PK.Find((PXGraph) this, paymentProcessorUser.UserID)?.DisplayName), MessageButtons.YesNo, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
    {
      {
        WebDialogResult.Yes,
        "Disable"
      },
      {
        WebDialogResult.No,
        "Cancel"
      }
    }, MessageIcon.None) != WebDialogResult.Yes)
      return adapter.Get();
    OrganizationUserData organizationUser = this.GetOrganizationUserData(current2.OrganizationID);
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => manager.ArchiveBankAccountUserAsync(externalPaymentProcessor, accountUser, organizationUser).GetAwaiter().GetResult()));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "", Visible = false)]
  [PXButton]
  public virtual IEnumerable SyncManageFundingAccounts(PXAdapter adapter)
  {
    string responseStr = adapter.CommandArguments;
    APExternalPaymentProcessor current = this.ExternalPaymentProcessor.Current;
    if (current == null)
      return adapter.Get();
    PaymentProcessorManager manager = this.GetPaymentProcessorManager(current.Type);
    APExternalPaymentProcessor copy = this.CopyPaymentProcessor(current);
    PaymentProcessorSessionHelper.SetUserSessionStore(copy);
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => manager.ProcessFundingAccountsResponseAsync(responseStr, copy).GetAwaiter().GetResult()));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Refresh Accounts")]
  [PXButton]
  public virtual IEnumerable RefreshFundingAccounts(PXAdapter adapter)
  {
    APExternalPaymentProcessor current1 = this.ExternalPaymentProcessor.Current;
    if (current1 == null)
      return adapter.Get();
    APPaymentProcessorOrganization current2 = this.PaymentProcessorOrganizations.Current;
    this.Save.Press();
    PaymentProcessorManager manager = this.GetPaymentProcessorManager(current1.Type);
    APExternalPaymentProcessor copy = this.CopyPaymentProcessor(current1);
    PaymentProcessorSessionHelper.SetUserSessionStore(copy);
    PX.Objects.GL.DAC.Organization organization = PX.Objects.GL.DAC.Organization.PK.Find((PXGraph) this, current2.OrganizationID);
    if (organization != null)
    {
      string organizationName = organization.OrganizationName;
    }
    OrganizationUserData organizationUser = this.GetOrganizationUserData(current2.OrganizationID);
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => manager.RefreshFundingAccountsAsync(copy, organizationUser).GetAwaiter().GetResult()));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Refresh Accounts")]
  [PXButton]
  public virtual IEnumerable RefreshFundingAccountUsers(PXAdapter adapter)
  {
    APExternalPaymentProcessor current1 = this.ExternalPaymentProcessor.Current;
    if (current1 == null)
      return adapter.Get();
    APPaymentProcessorOrganization current2 = this.PaymentProcessorOrganizations.Current;
    this.Save.Press();
    PaymentProcessorManager manager = this.GetPaymentProcessorManager(current1.Type);
    APExternalPaymentProcessor copy = this.CopyPaymentProcessor(current1);
    this.CopyOrganization(current2);
    PaymentProcessorSessionHelper.SetUserSessionStore(copy);
    PX.Objects.GL.DAC.Organization organization = PX.Objects.GL.DAC.Organization.PK.Find((PXGraph) this, current2.OrganizationID);
    if (organization != null)
    {
      string organizationName = organization.OrganizationName;
    }
    OrganizationUserData organizationUser = this.GetOrganizationUserData(current2.OrganizationID);
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => manager.RefreshFundingAccountUsersAsync(copy, organizationUser).GetAwaiter().GetResult()));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Manage Accounts")]
  [PXButton]
  public virtual IEnumerable ManageFundingAccounts(PXAdapter adapter)
  {
    APExternalPaymentProcessor current1 = this.ExternalPaymentProcessor.Current;
    if (current1 == null)
      return adapter.Get();
    APPaymentProcessorOrganization current2 = this.PaymentProcessorOrganizations.Current;
    this.Save.Press();
    PaymentProcessorManager manager = this.GetPaymentProcessorManager(current1.Type);
    APExternalPaymentProcessor copy = this.CopyPaymentProcessor(current1);
    OrganizationUserData userData = this.GetOrganizationUserData(current2.OrganizationID);
    PaymentProcessorSessionHelper.SetUserSessionStore(copy);
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => manager.ManageFundingAccountsAsync(copy, userData).GetAwaiter().GetResult()));
    return adapter.Get();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<APPaymentProcessorAccount.cashAccountID> e)
  {
    APPaymentProcessorAccount row = (APPaymentProcessorAccount) e.Row;
    if (row == null)
      return;
    int? newValue = (int?) e.NewValue;
    if (!newValue.HasValue)
    {
      PXUIFieldAttribute.SetWarning<APPaymentProcessorAccount.status>(e.Cache, (object) row, (string) null);
    }
    else
    {
      PX.Objects.CA.CashAccount cashAccount = PX.Objects.CA.CashAccount.PK.Find((PXGraph) this, newValue);
      if (!SupportedCurrencies.IsSupported(cashAccount.CuryID))
      {
        e.NewValue = (object) cashAccount?.CashAccountCD;
        throw new PXSetPropertyException<APPaymentProcessorAccount.cashAccountID>("Only USD funding accounts are supported by the external payment processor.");
      }
      APPaymentProcessorAccount processorAccount = (APPaymentProcessorAccount) PXSelectBase<APPaymentProcessorAccount, PXViewOf<APPaymentProcessorAccount>.BasedOn<SelectFromBase<APPaymentProcessorAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorAccount.externalPaymentProcessorID, Equal<BqlField<APExternalPaymentProcessor.externalPaymentProcessorID, IBqlString>.FromCurrent>>>>>.And<BqlOperand<APPaymentProcessorAccount.cashAccountID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) newValue);
      if (processorAccount != null)
      {
        if (!(processorAccount.ExternalAccountID != row.ExternalAccountID))
        {
          int? organizationId1 = processorAccount.OrganizationID;
          int? organizationId2 = row.OrganizationID;
          if (organizationId1.GetValueOrDefault() == organizationId2.GetValueOrDefault() & organizationId1.HasValue == organizationId2.HasValue)
            goto label_10;
        }
        PX.Objects.GL.DAC.Organization organization = PX.Objects.GL.DAC.Organization.PK.Find((PXGraph) this, row.OrganizationID);
        e.NewValue = (object) cashAccount?.CashAccountCD;
        throw new PXSetPropertyException<APPaymentProcessorAccount.cashAccountID>("The selected cash account has already been mapped to the {0} funding account of the {1} company.", new object[2]
        {
          (object) processorAccount.ExternalAccountNumber,
          (object) organization.OrganizationCD
        });
      }
label_10:
      if (!(row.Status == "P"))
        return;
      string error = $"Payments cannot be processed because the {cashAccount.CashAccountCD} cash account is mapped to a funding account pending activation in the external payment processor. To process payments, make sure that the funding account is active on the Funding Accounts tab of the External Payment Processor (AP205500) form.";
      PXUIFieldAttribute.SetWarning(e.Cache, (object) row, "status", error);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<APExternalPaymentProcessor.isActive> e)
  {
    if (!((bool?) e.NewValue).GetValueOrDefault())
      return;
    APExternalPaymentProcessor row = (APExternalPaymentProcessor) e.Row;
    if (row == null || !(row.Type == "BC"))
      return;
    APExternalPaymentProcessor paymentProcessor = (APExternalPaymentProcessor) PXSelectBase<APExternalPaymentProcessor, PXViewOf<APExternalPaymentProcessor>.BasedOn<SelectFromBase<APExternalPaymentProcessor, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APExternalPaymentProcessor.isActive, Equal<PX.Data.True>>>>>.And<BqlOperand<APExternalPaymentProcessor.type, IBqlString>.IsEqual<P.AsString>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) row.Type);
    if (paymentProcessor != null && paymentProcessor.ExternalPaymentProcessorID != row.ExternalPaymentProcessorID)
      throw new PXSetPropertyException<APExternalPaymentProcessor.isActive>("The record cannot be saved because another active payment processor with the {0} plug-in exists.", new object[1]
      {
        (object) APExternalPaymentPocessorType.GetLabel(row.Type)
      });
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<APPaymentProcessorOrganization> e)
  {
    APPaymentProcessorOrganization row = e.Row;
    if (row == null)
      return;
    bool valueOrDefault = row.IsOnboarded.GetValueOrDefault();
    this.manageFundingAccounts.SetEnabled(valueOrDefault);
    this.refreshFundingAccounts.SetEnabled(valueOrDefault);
    this.refreshFundingAccountUsers.SetEnabled(valueOrDefault);
    this.PaymentProcessorUsers.Cache.AllowInsert = valueOrDefault;
  }

  protected virtual void _(PX.Data.Events.RowSelected<APPaymentProcessorUser> e)
  {
    APPaymentProcessorUser row = e.Row;
    if (row == null)
      return;
    PXUIFieldAttribute.SetEnabled<APPaymentProcessorUser.userID>(e.Cache, (object) row, row.Status == "R");
  }

  protected virtual void _(PX.Data.Events.RowSelected<APPaymentProcessorAccount> e)
  {
    PXCache cache = e.Cache;
    APPaymentProcessorAccount row = e.Row;
    if (row == null || !row.CashAccountID.HasValue || !(cache.GetStateExt<APPaymentProcessorAccount.status>((object) row) is PXFieldState stateExt) || stateExt.ErrorLevel >= PXErrorLevel.Warning || !(row.Status == "P"))
      return;
    string error = $"Payments cannot be processed because the {PX.Objects.CA.CashAccount.PK.Find((PXGraph) this, row.CashAccountID).CashAccountCD} cash account is mapped to a funding account pending activation in the external payment processor. To process payments, make sure that the funding account is active on the Funding Accounts tab of the External Payment Processor (AP205500) form.";
    PXUIFieldAttribute.SetWarning(cache, (object) row, "status", error);
  }

  protected virtual void _(
    PX.Data.Events.RowInserting<APPaymentProcessorOrganization> e)
  {
    APPaymentProcessorOrganization row = e.Row;
    if (row == null)
      return;
    PX.Objects.GL.DAC.Organization organization = PX.Objects.GL.DAC.Organization.PK.Find((PXGraph) this, row.OrganizationID);
    if (organization == null)
      return;
    if (!SupportedCountries.IsSupported(((PX.Objects.CR.Address) PXSelectBase<PX.Objects.CR.Address, PXViewOf<PX.Objects.CR.Address>.BasedOn<SelectFromBase<PX.Objects.CR.Address, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CR.BAccount>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.CR.BAccount.bAccountID, Equal<PX.Objects.CR.Address.bAccountID>>>>>.And<BqlOperand<PX.Objects.CR.BAccount.defAddressID, IBqlInt>.IsEqual<PX.Objects.CR.Address.addressID>>>>>.Where<BqlOperand<PX.Objects.CR.Address.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, (object) organization.BAccountID)).CountryID))
    {
      string str = string.Join(", ", (IEnumerable<string>) SupportedCountries.GetAll().Select<string, string>((Func<string, string>) (x => PX.Objects.CS.Country.PK.Find((PXGraph) this, x)?.Description ?? (string) null)).Where<string>((Func<string, bool>) (x => x != null)).ToList<string>());
      e.Cache.RaiseExceptionHandling<APPaymentProcessorOrganization.organizationID>((object) e.Row, (object) organization.OrganizationCD, (Exception) new PXException("Cannot add companies that are not based in {0}", new object[1]
      {
        (object) str
      }));
      e.Cancel = true;
    }
    foreach (PXResult<APPaymentProcessorOrganization> pxResult in this.PaymentProcessorOrganizations.Select())
    {
      int? organizationId1 = ((APPaymentProcessorOrganization) pxResult).OrganizationID;
      int? organizationId2 = row.OrganizationID;
      if (organizationId1.GetValueOrDefault() == organizationId2.GetValueOrDefault() & organizationId1.HasValue == organizationId2.HasValue)
      {
        e.Cache.RaiseExceptionHandling<APPaymentProcessorOrganization.organizationID>((object) e.Row, (object) organization.OrganizationCD, (Exception) new PXException("The company has already been added to the payment processor."));
        e.Cancel = true;
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowInserting<APPaymentProcessorUser> e)
  {
    APPaymentProcessorUser row = e.Row;
    if (row == null)
      return;
    Users users = Users.PK.Find((PXGraph) this, row.UserID);
    APPaymentProcessorOrganization current = this.PaymentProcessorOrganizations.Current;
    if (current == null)
      return;
    if (FlaggedModeScopeBase<OnboardingProcessScope, int>.IsActive)
    {
      int parameters = FlaggedModeScopeBase<OnboardingProcessScope, int>.Parameters;
      int? organizationId = current.OrganizationID;
      int valueOrDefault = organizationId.GetValueOrDefault();
      if (parameters == valueOrDefault & organizationId.HasValue)
        return;
    }
    if (!current.IsOnboarded.GetValueOrDefault())
    {
      e.Cache.RaiseExceptionHandling<APPaymentProcessorUser.userID>((object) e.Row, (object) users.Username, (Exception) new PXException("Cannot add a user before the company is onboarded."));
      e.Cancel = true;
    }
    else
    {
      foreach (PXResult<APPaymentProcessorUser> pxResult in this.PaymentProcessorUsers.Select())
      {
        Guid? userId1 = ((APPaymentProcessorUser) pxResult).UserID;
        Guid? userId2 = row.UserID;
        if ((userId1.HasValue == userId2.HasValue ? (userId1.HasValue ? (userId1.GetValueOrDefault() == userId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        {
          e.Cache.RaiseExceptionHandling<APPaymentProcessorUser.userID>((object) e.Row, (object) users.Username, (Exception) new PXException("The user has already been added for the company."));
          e.Cancel = true;
        }
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleting<APPaymentProcessorUser> e)
  {
    APPaymentProcessorUser row = e.Row;
    if (row != null && row.ExternalUserID != null && row.Status != "R")
      throw new PXException("An onboarded user cannot be deleted.");
  }

  protected virtual void _(
    PX.Data.Events.RowDeleting<APPaymentProcessorOrganization> e)
  {
    APPaymentProcessorOrganization row = e.Row;
    if (row == null || row.ExternalOrganizationID == null)
      return;
    PXResultset<APPaymentProcessorAccount> source1 = PXSelectBase<APPaymentProcessorAccount, PXViewOf<APPaymentProcessorAccount>.BasedOn<SelectFromBase<APPaymentProcessorAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorAccount.organizationID, Equal<P.AsInt>>>>, PX.Data.And<BqlOperand<APPaymentProcessorAccount.externalPaymentProcessorID, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<APPaymentProcessorAccount.isActive, IBqlBool>.IsEqual<PX.Data.True>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) row.OrganizationID, (object) row.ExternalPaymentProcessorID);
    if ((source1 != null ? (source1.Any<PXResult<APPaymentProcessorAccount>>() ? 1 : 0) : 0) != 0)
      throw new PXException("The company has already been added to the payment processor.");
    PXResultset<PX.Objects.AP.APPayment> source2 = PXSelectBase<PX.Objects.AP.APPayment, PXViewOf<PX.Objects.AP.APPayment>.BasedOn<SelectFromBase<PX.Objects.AP.APPayment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.AP.APPayment.externalOrganizationID, IBqlString>.IsEqual<P.AsString>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) row.ExternalOrganizationID);
    if ((source2 != null ? (source2.Any<PXResult<PX.Objects.AP.APPayment>>() ? 1 : 0) : 0) != 0)
      throw new PXException("The company has already been added to the payment processor.");
  }

  internal virtual void RefreshProcessorOrganizationExternalId(
    PX.Objects.GL.DAC.Organization organization,
    ExternalOrganization extOrganization)
  {
    int? organizationId = organization.OrganizationID;
    if (!organizationId.HasValue)
      return;
    APPaymentProcessorOrganization processorOrganization = this.PaymentProcessorOrganizations.Select().RowCast<APPaymentProcessorOrganization>().Where<APPaymentProcessorOrganization>((Func<APPaymentProcessorOrganization, bool>) (i =>
    {
      int? organizationId1 = i.OrganizationID;
      int? nullable = organizationId;
      return organizationId1.GetValueOrDefault() == nullable.GetValueOrDefault() & organizationId1.HasValue == nullable.HasValue;
    })).FirstOrDefault<APPaymentProcessorOrganization>();
    processorOrganization.ExternalOrganizationID = extOrganization.Id;
    this.PaymentProcessorOrganizations.Update(processorOrganization);
    this.Save.Press();
  }

  internal virtual void UpdateOrganizationUser(
    APPaymentProcessorUser paymentProcUser,
    ExternalUser extUser)
  {
    int? organizationId = paymentProcUser.OrganizationID;
    if (!organizationId.HasValue)
      return;
    APPaymentProcessorUser paymentProcessorUser = this.PaymentProcessorUsers.Select().RowCast<APPaymentProcessorUser>().Where<APPaymentProcessorUser>((Func<APPaymentProcessorUser, bool>) (i =>
    {
      int? organizationId1 = i.OrganizationID;
      int? nullable = organizationId;
      return organizationId1.GetValueOrDefault() == nullable.GetValueOrDefault() & organizationId1.HasValue == nullable.HasValue && i.ExternalUserID == extUser.Id;
    })).FirstOrDefault<APPaymentProcessorUser>();
    if (paymentProcessorUser == null)
      return;
    paymentProcessorUser.Status = extUser.Archived.GetValueOrDefault() ? "D" : "O";
    this.PaymentProcessorUsers.Update(paymentProcessorUser);
    this.Save.Press();
  }

  internal virtual void RefreshProcessorUserExternalId(
    APPaymentProcessorOrganization extOrganization,
    Users users,
    ExternalUser extUser)
  {
    string id = extUser.Id;
    APExternalPaymentProcessor current = this.ExternalPaymentProcessor.Current;
    int? organizationId = extOrganization.OrganizationID;
    Guid? userId = users.PKID;
    if (!organizationId.HasValue || !userId.HasValue)
      return;
    APPaymentProcessorUser paymentProcessorUser = this.PaymentProcessorUsers.Select().RowCast<APPaymentProcessorUser>().Where<APPaymentProcessorUser>((Func<APPaymentProcessorUser, bool>) (i =>
    {
      int? organizationId1 = i.OrganizationID;
      int? nullable1 = organizationId;
      if (!(organizationId1.GetValueOrDefault() == nullable1.GetValueOrDefault() & organizationId1.HasValue == nullable1.HasValue))
        return false;
      Guid? userId1 = i.UserID;
      Guid? nullable2 = userId;
      if (userId1.HasValue != nullable2.HasValue)
        return false;
      return !userId1.HasValue || userId1.GetValueOrDefault() == nullable2.GetValueOrDefault();
    })).FirstOrDefault<APPaymentProcessorUser>();
    if (paymentProcessorUser == null && id == null)
      return;
    if (paymentProcessorUser == null)
    {
      this.PaymentProcessorUsers.Insert(new APPaymentProcessorUser()
      {
        ExternalPaymentProcessorID = current.ExternalPaymentProcessorID,
        UserID = userId,
        ExternalUserID = id,
        OrganizationID = organizationId,
        Status = id == null ? "R" : "O"
      });
    }
    else
    {
      paymentProcessorUser.ExternalUserID = id;
      paymentProcessorUser.Status = id == null ? "R" : "O";
      this.PaymentProcessorUsers.Update(paymentProcessorUser);
    }
    this.Save.Press();
  }

  internal virtual void RefreshBankAccounts(
    IEnumerable<ExternalBankAccount> externalBankAccounts,
    APPaymentProcessorOrganization extOrganization)
  {
    APExternalPaymentProcessor current = this.ExternalPaymentProcessor.Current;
    if (current == null || current.ExternalPaymentProcessorID == null || !extOrganization.OrganizationID.HasValue)
      return;
    int organizationId = extOrganization.OrganizationID.Value;
    if (externalBankAccounts != null && externalBankAccounts.Count<ExternalBankAccount>() > 0)
    {
      IEnumerable<APPaymentProcessorAccount> accountsByOrganization = this.GetAccountsByOrganization(organizationId, current.ExternalPaymentProcessorID);
      foreach (ExternalBankAccount externalBankAccount1 in externalBankAccounts)
      {
        ExternalBankAccount externalBankAccount = externalBankAccount1;
        APPaymentProcessorAccount processorAccount = accountsByOrganization.FirstOrDefault<APPaymentProcessorAccount>((Func<APPaymentProcessorAccount, bool>) (x =>
        {
          if (!(x.ExternalAccountID?.ToLower() == externalBankAccount.Id.ToLower()))
            return false;
          int? organizationId1 = x.OrganizationID;
          int num = organizationId;
          return organizationId1.GetValueOrDefault() == num & organizationId1.HasValue;
        }));
        int num1 = processorAccount == null ? 1 : 0;
        if (processorAccount == null)
          processorAccount = new APPaymentProcessorAccount()
          {
            OrganizationID = new int?(organizationId),
            ExternalAccountID = externalBankAccount.Id
          };
        processorAccount.Status = AccountStatus.GetStatusAsString(externalBankAccount.Status);
        processorAccount.ExternalAccountName = externalBankAccount.NameOnAccount;
        processorAccount.ExternalAccountNumber = externalBankAccount.AccountNumber;
        processorAccount.ExternalAccountRoutingNumber = externalBankAccount.RoutingNumber;
        processorAccount.ExternalAccountBank = externalBankAccount.BankName;
        processorAccount.ExternalAccountType = externalBankAccount.AccountType;
        processorAccount.IsActive = new bool?(!externalBankAccount.Archived);
        if (num1 != 0)
          this.Caches[typeof (APPaymentProcessorAccount)].Insert((object) processorAccount);
        else
          this.Caches[typeof (APPaymentProcessorAccount)].Update((object) processorAccount);
      }
    }
    this.Save.Press();
  }

  internal virtual void UpdateBankAccount(
    APPaymentProcessorAccount extAccount,
    ExternalBankAccount extBankAcct)
  {
    int? organizationId = extAccount.OrganizationID;
    if (!organizationId.HasValue)
      return;
    APPaymentProcessorAccount processorAccount = this.PaymentProcessorAccounts.Select().RowCast<APPaymentProcessorAccount>().Where<APPaymentProcessorAccount>((Func<APPaymentProcessorAccount, bool>) (i =>
    {
      int? organizationId1 = i.OrganizationID;
      int? nullable = organizationId;
      return organizationId1.GetValueOrDefault() == nullable.GetValueOrDefault() & organizationId1.HasValue == nullable.HasValue && i.ExternalAccountID == extBankAcct.Id;
    })).FirstOrDefault<APPaymentProcessorAccount>();
    if (processorAccount == null)
      return;
    processorAccount.Status = AccountStatus.GetStatusAsString(extBankAcct.Status);
    processorAccount.IsActive = new bool?(!extBankAcct.Archived);
    this.PaymentProcessorAccounts.Update(processorAccount);
    this.Save.Press();
  }

  internal virtual void UpdateBankAccountUser(ExternalBankAccountUser extBankAcct)
  {
    APPaymentProcessorAccountUser processorAccountUser = this.PaymentProcessorAccountUsers.Select().RowCast<APPaymentProcessorAccountUser>().Where<APPaymentProcessorAccountUser>((Func<APPaymentProcessorAccountUser, bool>) (i => i.ExternalAccountID == extBankAcct.BankAccountId && i.ExternalID == extBankAcct.Id)).FirstOrDefault<APPaymentProcessorAccountUser>();
    if (processorAccountUser == null)
      return;
    processorAccountUser.VerificationStatus = VerificationStatus.GetStatusAsString(extBankAcct.VerificationStatus);
    this.PaymentProcessorAccountUsers.Update(processorAccountUser);
    this.Save.Press();
  }

  internal virtual void RefreshBankAccountUsers(
    IEnumerable<ExternalBankAccountUser> externalBankAccountUsers,
    APPaymentProcessorOrganization extOrganization)
  {
    if (!extOrganization.OrganizationID.HasValue)
      return;
    int organizationId = extOrganization.OrganizationID.Value;
    APExternalPaymentProcessor current = this.ExternalPaymentProcessor.Current;
    if (current == null || current.ExternalPaymentProcessorID == null)
      return;
    if (externalBankAccountUsers != null && externalBankAccountUsers.Count<ExternalBankAccountUser>() > 0)
    {
      IEnumerable<APPaymentProcessorAccountUser> usersByOrganization = this.GetAccountUsersByOrganization(organizationId, current.ExternalPaymentProcessorID);
      foreach (ExternalBankAccountUser externalBankAccountUser1 in externalBankAccountUsers)
      {
        ExternalBankAccountUser externalBankAccountUser = externalBankAccountUser1;
        APPaymentProcessorAccountUser processorAccountUser = usersByOrganization.FirstOrDefault<APPaymentProcessorAccountUser>((Func<APPaymentProcessorAccountUser, bool>) (x => x.ExternalID?.ToLower() == externalBankAccountUser.Id.ToLower()));
        int num = processorAccountUser == null ? 1 : 0;
        if (processorAccountUser == null)
          processorAccountUser = new APPaymentProcessorAccountUser()
          {
            ExternalID = externalBankAccountUser.Id,
            OrganizationID = new int?(organizationId)
          };
        processorAccountUser.ExternalAccountID = externalBankAccountUser.BankAccountId;
        processorAccountUser.ExternalUserID = externalBankAccountUser.UserId;
        processorAccountUser.VerificationStatus = VerificationStatus.GetStatusAsString(externalBankAccountUser.VerificationStatus);
        if (num != 0)
          this.Caches[typeof (APPaymentProcessorAccountUser)].Insert((object) processorAccountUser);
        else
          this.Caches[typeof (APPaymentProcessorAccountUser)].Update((object) processorAccountUser);
      }
    }
    this.Save.Press();
  }

  internal virtual APPaymentProcessorUser AddPaymentProcessorUser(int organizationId, Guid userId)
  {
    APPaymentProcessorUser paymentProcessorUser = this.PaymentProcessorUsers.Insert(new APPaymentProcessorUser()
    {
      OrganizationID = new int?(organizationId),
      Status = "R",
      UserID = new Guid?(userId)
    });
    this.Save.Press();
    return paymentProcessorUser;
  }

  internal virtual void UpdateOrganizationStatus(
    APPaymentProcessorOrganization organization,
    bool isOnboarded)
  {
    if (organization == null)
      return;
    organization.IsOnboarded = new bool?(isOnboarded);
    this.PaymentProcessorOrganizations.Update(organization);
    this.Save.Press();
  }

  internal virtual void UpdateExternalWebhookID(
    APPaymentProcessorOrganization organization,
    WebHook webhook,
    ExternalWebhook externalWebhook)
  {
    if (organization == null)
      return;
    organization.ExternalWebhookID = externalWebhook.Id;
    organization.WebhookID = webhook.WebHookID;
    this.PaymentProcessorOrganizations.Update(organization);
    this.Save.Press();
  }

  internal virtual void DeleteWebHookSubscription(
    APPaymentProcessorOrganization organization,
    ExternalWebhook externalWebhook)
  {
    if (organization == null)
      return;
    Guid? webhookId = organization.WebhookID;
    WebhookMaint instance = PXGraph.CreateInstance<WebhookMaint>();
    WebHook topFirst = PXSelectBase<WebHook, PXViewOf<WebHook>.BasedOn<SelectFromBase<WebHook, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<WebHook.webHookID, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.Select((PXGraph) this, (object) webhookId).TopFirst;
    organization.ExternalWebhookID = (string) null;
    organization.WebhookID = new Guid?();
    this.PaymentProcessorOrganizations.Update(organization);
    instance.Webhook.Delete(topFirst);
    ((PXGraph<WebhookMaint, WebHook>) instance).Save.Press();
    this.Save.Press();
  }

  internal APPaymentProcessorOrganization? GetOrganizationByExtOrganizationId(string externalOrgId)
  {
    return (APPaymentProcessorOrganization) PXSelectBase<APPaymentProcessorOrganization, PXViewOf<APPaymentProcessorOrganization>.BasedOn<SelectFromBase<APPaymentProcessorOrganization, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<APPaymentProcessorOrganization.externalOrganizationID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, (object) externalOrgId).FirstOrDefault<PXResult<APPaymentProcessorOrganization>>();
  }

  internal APPaymentProcessorUser? GeUserByExtUserID(
    string? externalUserId,
    string? externalPaymentProcessorId,
    int? organizationId)
  {
    if (externalUserId == null || externalPaymentProcessorId == null || !organizationId.HasValue)
      return (APPaymentProcessorUser) null;
    return (APPaymentProcessorUser) PXSelectBase<APPaymentProcessorUser, PXViewOf<APPaymentProcessorUser>.BasedOn<SelectFromBase<APPaymentProcessorUser, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorUser.externalUserID, Equal<P.AsString>>>>, PX.Data.And<BqlOperand<APPaymentProcessorUser.externalPaymentProcessorID, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<APPaymentProcessorUser.organizationID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, (object) externalUserId, (object) externalPaymentProcessorId, (object) organizationId).FirstOrDefault<PXResult<APPaymentProcessorUser>>();
  }

  internal IEnumerable<APPaymentProcessorAccount> GetAccountsByOrganization(
    int organizationId,
    string externalPaymentProcessorId)
  {
    return PXSelectBase<APPaymentProcessorAccount, PXViewOf<APPaymentProcessorAccount>.BasedOn<SelectFromBase<APPaymentProcessorAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorAccount.organizationID, Equal<P.AsInt>>>>>.And<BqlOperand<APPaymentProcessorAccount.externalPaymentProcessorID, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, (object) organizationId, (object) externalPaymentProcessorId).RowCast<APPaymentProcessorAccount>();
  }

  internal IEnumerable<APPaymentProcessorAccountUser> GetAccountUsersByOrganization(
    int organizationId,
    string externalPaymentProcessorId)
  {
    return PXSelectBase<APPaymentProcessorAccountUser, PXViewOf<APPaymentProcessorAccountUser>.BasedOn<SelectFromBase<APPaymentProcessorAccountUser, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorAccountUser.organizationID, Equal<P.AsInt>>>>>.And<BqlOperand<APPaymentProcessorAccountUser.externalPaymentProcessorID, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, (object) organizationId, (object) externalPaymentProcessorId).RowCast<APPaymentProcessorAccountUser>();
  }

  protected virtual PaymentProcessorManager GetPaymentProcessorManager(string type)
  {
    return this.PaymentProcessorManagerProvider(type);
  }

  private APExternalPaymentProcessor CopyPaymentProcessor(APExternalPaymentProcessor processor)
  {
    return (APExternalPaymentProcessor) this.ExternalPaymentProcessor.Cache.CreateCopy((object) processor);
  }

  private APPaymentProcessorOrganization CopyOrganization(
    APPaymentProcessorOrganization organization)
  {
    return (APPaymentProcessorOrganization) this.PaymentProcessorOrganizations.Cache.CreateCopy((object) organization);
  }

  private OrganizationUserData GetOrganizationUserData(int? organizationId, Guid? userId)
  {
    return new OrganizationUserData()
    {
      OrganizationId = organizationId,
      UserId = userId
    };
  }

  private OrganizationUserData GetOrganizationUserData(int? organizationId)
  {
    return new OrganizationUserData()
    {
      OrganizationId = organizationId
    };
  }
}
