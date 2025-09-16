// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.Managers.BillComManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Webhooks.DAC;
using PX.Common;
using PX.Common.Extensions;
using PX.Data;
using PX.Objects.AP.PaymentProcessor.Common;
using PX.Objects.AP.PaymentProcessor.DAC;
using PX.Objects.AP.PaymentProcessor.GraphExtensions;
using PX.Objects.CS;
using PX.Objects.CS.DAC;
using PX.Objects.PO;
using PX.PaymentProcessor.Common;
using PX.PaymentProcessor.Data;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.AP.PaymentProcessor.Managers;

internal class BillComManager : PaymentProcessorManager
{
  private readonly IPaymentProcessorClient _client;
  private readonly ICurrentUserInformationProvider _currentUserInformationProvider;
  private const string VendorAcctType = "BUSINESS";
  private const string CheckingAcct = "CHECKING";
  private const string SavingAcct = "SAVINGS";
  private const string VoidPaymentReason = "Payment voiding requested by Acumatica user";
  private const string BankAccount = "BANK_ACCOUNT";

  public BillComManager(
    IPaymentProcessorClient client,
    ICurrentUserInformationProvider currentUserInformationProvider)
  {
    this._client = client;
    this._currentUserInformationProvider = currentUserInformationProvider;
  }

  public override bool IsCurrencySupported(string CurrencyID)
  {
    return SupportedCurrencies.IsSupported(CurrencyID);
  }

  public bool IsAdjustmentsCurrencySupported(PX.Objects.AP.APPayment payment)
  {
    APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
    APPaymentEntryEPPExt extension = instance.GetExtension<APPaymentEntryEPPExt>();
    foreach (APAdjust paymentAdjustment in extension.GetPaymentAdjustments(payment))
    {
      PX.Objects.AP.APInvoice apInvoice = PX.Objects.AP.APInvoice.PK.Find((PXGraph) instance, paymentAdjustment?.AdjdDocType, paymentAdjustment?.AdjdRefNbr);
      if (apInvoice != null && !SupportedCurrencies.IsSupported(apInvoice.CuryID))
        return false;
    }
    foreach (POAdjust poAdjustment in extension.GetPOAdjustments(payment))
    {
      PX.Objects.PO.POOrder poOrder = PX.Objects.PO.POOrder.PK.Find((PXGraph) instance, poAdjustment?.AdjdOrderType, poAdjustment?.AdjdOrderNbr);
      if (poOrder != null && !SupportedCurrencies.IsSupported(poOrder.CuryID))
        return false;
    }
    return true;
  }

  public override bool IsCountrySupported(string CountryID)
  {
    return SupportedCountries.IsSupported(CountryID);
  }

  public override async System.Threading.Tasks.Task ShowMfaAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    OrganizationUserData organizationUser,
    string action)
  {
    BillComManager billComManager = this;
    APExternalPaymentProcessorMaint paymentProcessorGraph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = billComManager.GetOrganizationAndUser(paymentProcessorGraph, organizationUser);
    APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
    string externalUserID = organizationAndUser.eppUser.ExternalUserID;
    string externalOrganizationID = eppOrganization.ExternalOrganizationID;
    string idForWidgetAsync = await billComManager.GetUserSessionIdForWidgetAsync(externalPaymentProcessor, externalOrganizationID, externalUserID);
    WidgetData widgetData = new WidgetData()
    {
      SessionId = idForWidgetAsync,
      ExternalOrganizationId = externalOrganizationID,
      ExternalUserId = externalUserID,
      InitialAction = action
    };
    throw new PXPluginRedirectException<PXPluginRedirectOptions>(await billComManager._client.RedirectToMfaProcessWidgetAsync(widgetData));
  }

  public override async System.Threading.Tasks.Task ManageFundingAccountsAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    OrganizationUserData organizationUser)
  {
    BillComManager billComManager = this;
    int? organizationId = organizationUser.OrganizationId;
    APExternalPaymentProcessorMaint paymentProcessorGraph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor, organizationId);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) = billComManager.GetOrganizationAndUser(paymentProcessorGraph, organizationId);
    string extOrganizationId = eppOrganization.ExternalOrganizationID;
    string externalUserId = eppUser.ExternalUserID;
    string idForWidgetAsync = await billComManager.GetUserSessionIdForWidgetAsync(externalPaymentProcessor, extOrganizationId, externalUserId);
    WidgetData widgetData = new WidgetData()
    {
      SessionId = idForWidgetAsync,
      ExternalOrganizationId = extOrganizationId,
      ExternalUserId = eppUser.ExternalUserID
    };
    throw new PXPluginRedirectException<PXPluginRedirectOptions>(await billComManager._client.RedirectToFundingWidgetAsync(widgetData));
  }

  public override async System.Threading.Tasks.Task OnboardOrganizationAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    PX.Objects.GL.DAC.Organization organization)
  {
    BillComManager billComManager = this;
    if (organization == null)
      throw new PXException("The operation failed because the company does not exist in the external payment processor.");
    APExternalPaymentProcessorMaint graph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor, organization.OrganizationID);
    APPaymentProcessorOrganization eppOrganization = APPaymentProcessorOrganization.PK.Find((PXGraph) graph, externalPaymentProcessor?.ExternalPaymentProcessorID, organization.OrganizationID);
    if (eppOrganization == null)
      throw new PXException("The operation failed because the company does not exist in the external payment processor.");
    APPaymentProcessorUser eppUser;
    string externalOrganizationId;
    string externalUserId;
    if (!eppOrganization.IsOnboarded.GetValueOrDefault())
    {
      Guid? userId = billComManager._currentUserInformationProvider.GetUserId();
      if (!userId.HasValue)
        throw new PXException("The operation failed because the user does not exist in the external payment processor.");
      eppUser = APPaymentProcessorUser.PK.Find((PXGraph) graph, externalPaymentProcessor?.ExternalPaymentProcessorID, eppOrganization.OrganizationID, userId);
      if (eppUser == null)
      {
        using (new OnboardingProcessScope(eppOrganization.OrganizationID.Value))
          eppUser = graph.AddPaymentProcessorUser(eppOrganization.OrganizationID.Value, userId.Value);
      }
      externalOrganizationId = eppOrganization.ExternalOrganizationID;
      externalUserId = eppUser?.ExternalUserID;
      if (externalOrganizationId == null)
      {
        ExternalOrganization organizationAsync = await billComManager.CreateOrganizationAsync(externalPaymentProcessor, organization);
        externalOrganizationId = organizationAsync.Id;
        if (externalOrganizationId == null)
          throw new PXException("The operation failed because the {0} company does not exist in the external payment processor.", new object[1]
          {
            (object) organization.OrganizationCD
          });
        graph.RefreshProcessorOrganizationExternalId(organization, organizationAsync);
        externalUserId = (string) null;
      }
      if (externalUserId == null)
      {
        Users user = Users.PK.Find((PXGraph) graph, new Guid?(userId.Value));
        UserData userData = billComManager.BuildUserRequest(eppUser, user, externalOrganizationId);
        ExternalUser userAsync = await billComManager._client.CreateUserAsync(userData);
        externalUserId = userAsync.Id;
        graph.RefreshProcessorUserExternalId(eppOrganization, user, userAsync);
        user = (Users) null;
      }
      string idForWidgetAsync = await billComManager.GetUserSessionIdForWidgetAsync(externalPaymentProcessor, externalOrganizationId, externalUserId);
      WidgetData widgetData = new WidgetData()
      {
        SessionId = idForWidgetAsync,
        ExternalOrganizationId = externalOrganizationId,
        ExternalUserId = externalUserId
      };
      throw new PXPluginRedirectException<PXPluginRedirectOptions>(await billComManager._client.RedirectToOnboardingWidgetAsync(widgetData));
    }
    graph = (APExternalPaymentProcessorMaint) null;
    eppOrganization = (APPaymentProcessorOrganization) null;
    eppUser = (APPaymentProcessorUser) null;
    externalOrganizationId = (string) null;
    externalUserId = (string) null;
  }

  public override MfaResponse ProcessMfaResponse(
    string responseStr,
    APExternalPaymentProcessor externalPaymentProcessor)
  {
    MfaResponse response = this.ParseResponse<MfaResponse>(responseStr);
    if (externalPaymentProcessor.UserSessionStore.HasValue)
      PaymentProcessorSessionHelper.SaveUserSessionStore(PaymentProcessorSessionHelper.AddOrUpdateUserSessionIdInStore(externalPaymentProcessor.UserSessionStore.Value, response.OrganizationId, response.SessionId));
    return response;
  }

  public override async System.Threading.Tasks.Task ProcessOnboardResponseAsync(
    string responseStr,
    APExternalPaymentProcessor externalPaymentProcessor)
  {
    BillComManager billComManager = this;
    OnboardingResponse response = billComManager.ParseResponse<OnboardingResponse>(responseStr);
    if (externalPaymentProcessor.UserSessionStore.HasValue)
    {
      UserSessionStore store = PaymentProcessorSessionHelper.AddOrUpdateUserSessionIdInStore(externalPaymentProcessor.UserSessionStore.Value, response.OrganizationId, response.SessionId);
      externalPaymentProcessor.UserSessionStore = new UserSessionStore?(store);
      PaymentProcessorSessionHelper.SaveUserSessionStore(store);
    }
    if (response.ClosedManually)
      return;
    APExternalPaymentProcessorMaint graph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor);
    APPaymentProcessorOrganization extOrganizationId = graph.GetOrganizationByExtOrganizationId(response.OrganizationId);
    if (extOrganizationId != null)
    {
      graph.UpdateOrganizationStatus(extOrganizationId, true);
      OrganizationUserData organizationUser = new OrganizationUserData()
      {
        OrganizationId = extOrganizationId.OrganizationID
      };
      await billComManager.RefreshFundingAccountsAsync(graph, externalPaymentProcessor, organizationUser);
      await billComManager.RefreshFundingAccountUsersAsync(graph, externalPaymentProcessor, organizationUser);
      organizationUser = (OrganizationUserData) null;
    }
    graph = (APExternalPaymentProcessorMaint) null;
  }

  public override async System.Threading.Tasks.Task ProcessFundingAccountsResponseAsync(
    string responseStr,
    APExternalPaymentProcessor externalPaymentProcessor)
  {
    BillComManager billComManager = this;
    FundingAccountsResponse response = billComManager.ParseResponse<FundingAccountsResponse>(responseStr);
    APExternalPaymentProcessorMaint graph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor);
    APPaymentProcessorOrganization extOrganizationId = graph.GetOrganizationByExtOrganizationId(response.OrganizationId);
    graph.PaymentProcessorOrganizations.Current = extOrganizationId;
    if (externalPaymentProcessor.UserSessionStore.HasValue)
    {
      UserSessionStore store = PaymentProcessorSessionHelper.AddOrUpdateUserSessionIdInStore(externalPaymentProcessor.UserSessionStore.Value, response.OrganizationId, response.SessionId);
      externalPaymentProcessor.UserSessionStore = new UserSessionStore?(store);
      PaymentProcessorSessionHelper.SaveUserSessionStore(store);
    }
    if (extOrganizationId == null)
    {
      graph = (APExternalPaymentProcessorMaint) null;
    }
    else
    {
      OrganizationUserData organizationUser = new OrganizationUserData()
      {
        OrganizationId = extOrganizationId.OrganizationID
      };
      await billComManager.RefreshFundingAccountsAsync(graph, externalPaymentProcessor, organizationUser);
      await billComManager.RefreshFundingAccountUsersAsync(graph, externalPaymentProcessor, organizationUser);
      organizationUser = (OrganizationUserData) null;
      graph = (APExternalPaymentProcessorMaint) null;
    }
  }

  public override async System.Threading.Tasks.Task RefreshFundingAccountsAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    OrganizationUserData organizationUser)
  {
    BillComManager billComManager = this;
    APExternalPaymentProcessorMaint graph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor, organizationUser.OrganizationId);
    await billComManager.RefreshFundingAccountsAsync(graph, externalPaymentProcessor, organizationUser);
    await billComManager.RefreshFundingAccountUsersAsync(graph, externalPaymentProcessor, organizationUser);
    graph = (APExternalPaymentProcessorMaint) null;
  }

  public override async System.Threading.Tasks.Task RefreshFundingAccountUsersAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    OrganizationUserData organizationUser)
  {
    BillComManager billComManager = this;
    APExternalPaymentProcessorMaint paymentProcessorGraph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor, organizationUser.OrganizationId);
    await billComManager.RefreshFundingAccountUsersAsync(paymentProcessorGraph, externalPaymentProcessor, organizationUser);
  }

  public override async Task<ExternalOrganization> CreateOrganizationAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    PX.Objects.GL.DAC.Organization organization)
  {
    OrganizationMaint instance = PXGraph.CreateInstance<OrganizationMaint>();
    instance.OrganizationView.Current = organization;
    instance.BAccount.Current = (OrganizationBAccount) instance.BAccount.Search<OrganizationBAccount.bAccountID>((object) organization.BAccountID);
    return await this._client.CreateOrganizationAsync(this.BuildOrganizationRequest(instance));
  }

  public override async System.Threading.Tasks.Task CreateUserAsync(
    APExternalPaymentProcessor extPaymentProcessor,
    PX.Objects.GL.DAC.Organization organization,
    Users user)
  {
    BillComManager billComManager = this;
    int? organizationId = organization.OrganizationID;
    Guid? userId = user.PKID;
    if (!organizationId.HasValue)
      throw new PXException("The operation failed because the company does not exist in the external payment processor.");
    APExternalPaymentProcessorMaint extPaymentProcessorGraph = billComManager.GetAPExternalPaymentProcessorGraph(extPaymentProcessor, organizationId);
    (APPaymentProcessorOrganization processorOrganization, APPaymentProcessorUser paymentProcessorUser) = billComManager.GetOrganizationAndUser(extPaymentProcessorGraph, organizationId, userId, false);
    string externalOrganizationId = processorOrganization.ExternalOrganizationID;
    UserData userData = billComManager.BuildUserRequest(paymentProcessorUser, user, externalOrganizationId);
    ExternalUser userAsync = await billComManager._client.CreateUserAsync(userData);
    OrganizationUserData organizationUser = new OrganizationUserData();
    organizationUser.OrganizationId = organizationId;
    organizationUser.UserId = userId;
    extPaymentProcessorGraph.RefreshProcessorUserExternalId(processorOrganization, user, userAsync);
    await billComManager.RefreshFundingAccountsAsync(extPaymentProcessorGraph, extPaymentProcessor, organizationUser);
    await billComManager.RefreshFundingAccountUsersAsync(extPaymentProcessorGraph, extPaymentProcessor, organizationUser);
    extPaymentProcessorGraph = (APExternalPaymentProcessorMaint) null;
    processorOrganization = (APPaymentProcessorOrganization) null;
    organizationUser = (OrganizationUserData) null;
  }

  public override async System.Threading.Tasks.Task ArchiveUserAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    OrganizationUserData organizationUser)
  {
    BillComManager billComManager = this;
    if (!organizationUser.OrganizationId.HasValue)
      throw new PXException("The operation failed because the company does not exist in the external payment processor.");
    APExternalPaymentProcessorMaint graph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor, organizationUser.OrganizationId);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser paymentProcessorUser) = billComManager.GetOrganizationAndUser(graph, organizationUser);
    string externalOrganizationId = eppOrganization.ExternalOrganizationID;
    string externalUserId = paymentProcessorUser.ExternalUserID;
    UserData userData = new UserData()
    {
      ExternalUserId = externalUserId,
      ExternalOrganizationId = externalOrganizationId
    };
    graph.UpdateOrganizationUser(paymentProcessorUser, await billComManager._client.ArchiveUserAsync(userData));
    graph = (APExternalPaymentProcessorMaint) null;
    paymentProcessorUser = (APPaymentProcessorUser) null;
  }

  public override async System.Threading.Tasks.Task RestoreUserAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    OrganizationUserData organizationUser)
  {
    BillComManager billComManager = this;
    int? organizationId = organizationUser.OrganizationId;
    if (!organizationUser.OrganizationId.HasValue)
      throw new PXException("The operation failed because the company does not exist in the external payment processor.");
    APExternalPaymentProcessorMaint graph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor, organizationId);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser paymentProcessorUser) = billComManager.GetOrganizationAndUser(graph, organizationId, organizationUser.UserId, false);
    string externalOrganizationId = eppOrganization.ExternalOrganizationID;
    string externalUserId = paymentProcessorUser.ExternalUserID;
    UserData userData = new UserData()
    {
      ExternalOrganizationId = externalOrganizationId,
      ExternalUserId = externalUserId
    };
    graph.UpdateOrganizationUser(paymentProcessorUser, await billComManager._client.RestoreUserAsync(userData));
    graph = (APExternalPaymentProcessorMaint) null;
    paymentProcessorUser = (APPaymentProcessorUser) null;
  }

  public override async Task<IEnumerable<ExternalBankAccountUser>> GetUserBankAccountsAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    APPaymentProcessorAccount externalAccount,
    OrganizationUserData organizationUser)
  {
    BillComManager billComManager = this;
    APExternalPaymentProcessorMaint paymentProcessorGraph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = billComManager.GetOrganizationAndUser(paymentProcessorGraph, organizationUser);
    APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
    string eppUserID = organizationAndUser.eppUser.ExternalUserID;
    string eppOrganizationID = eppOrganization.ExternalOrganizationID;
    GetBankAccountParams bankAccountParams = new GetBankAccountParams();
    bankAccountParams.ExternalBankAccountId = externalAccount.ExternalAccountID;
    return await billComManager.PerformApiCallWithRetry<IEnumerable<ExternalBankAccountUser>>((Func<Task<IEnumerable<ExternalBankAccountUser>>>) (async () =>
    {
      string userSessionIdAsync = await this.GetUserSessionIdAsync(externalPaymentProcessor, eppOrganizationID, eppUserID);
      bankAccountParams.SessionId = userSessionIdAsync;
      return await this._client.GetUserBankAccountsAsync(bankAccountParams);
    }), externalPaymentProcessor);
  }

  public override async Task<MfaResponse> GetSessionDetailsAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    OrganizationUserData organizationUser)
  {
    BillComManager billComManager = this;
    APExternalPaymentProcessorMaint paymentProcessorGraph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = billComManager.GetOrganizationAndUser(paymentProcessorGraph, organizationUser);
    APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
    string eppUserID = organizationAndUser.eppUser.ExternalUserID;
    string eppOrganizationID = eppOrganization.ExternalOrganizationID;
    GetSessionParams sessionParams = new GetSessionParams();
    sessionParams.ExternalOrganizationId = eppOrganizationID;
    sessionParams.ExternalUserId = eppUserID;
    return await billComManager.PerformApiCallWithRetry<MfaResponse>((Func<Task<MfaResponse>>) (async () =>
    {
      string userSessionIdAsync = await this.GetUserSessionIdAsync(externalPaymentProcessor, eppOrganizationID, eppUserID);
      sessionParams.SessionId = userSessionIdAsync;
      return await this._client.GetSessionDetailsAsync(sessionParams);
    }), externalPaymentProcessor);
  }

  public override async Task<ExternalBankAccount> GetBankAccountDetailsAsync(
    APExternalPaymentProcessor extPaymentProcessor,
    APPaymentProcessorAccount extBankAccount,
    OrganizationUserData organizationUser)
  {
    BillComManager billComManager = this;
    APExternalPaymentProcessorMaint paymentProcessorGraph = billComManager.GetAPExternalPaymentProcessorGraph(extPaymentProcessor);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = billComManager.GetOrganizationAndUser(paymentProcessorGraph, organizationUser);
    APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
    string eppUserID = organizationAndUser.eppUser.ExternalUserID;
    string eppOrganizationID = eppOrganization.ExternalOrganizationID;
    GetBankAccountParams bankAccountParams = new GetBankAccountParams()
    {
      ExternalBankAccountId = extBankAccount.ExternalAccountID
    };
    return await billComManager.PerformApiCallWithRetry<ExternalBankAccount>((Func<Task<ExternalBankAccount>>) (async () =>
    {
      string userSessionIdAsync = await this.GetUserSessionIdAsync(extPaymentProcessor, eppOrganizationID, eppUserID);
      bankAccountParams.SessionId = userSessionIdAsync;
      return await this._client.GetBankAccountDetailsAsync(bankAccountParams);
    }), extPaymentProcessor);
  }

  public override async System.Threading.Tasks.Task ArchiveBankAccountAsync(
    APExternalPaymentProcessor extPaymentProcessor,
    APPaymentProcessorAccount extBankAccount,
    OrganizationUserData organizationUser)
  {
    BillComManager billComManager = this;
    int? organizationId = organizationUser.OrganizationId;
    APExternalPaymentProcessorMaint graph = billComManager.GetAPExternalPaymentProcessorGraph(extPaymentProcessor, organizationId);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = billComManager.GetOrganizationAndUser(graph, organizationUser);
    APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
    string eppUserID = organizationAndUser.eppUser.ExternalUserID;
    string eppOrganizationID = eppOrganization.ExternalOrganizationID;
    GetBankAccountParams bankAccountParams = new GetBankAccountParams()
    {
      ExternalBankAccountId = extBankAccount.ExternalAccountID
    };
    graph.UpdateBankAccount(extBankAccount, await billComManager.PerformApiCallWithRetry<ExternalBankAccount>((Func<Task<ExternalBankAccount>>) (async () =>
    {
      string userSessionIdAsync = await this.GetUserSessionIdAsync(extPaymentProcessor, eppOrganizationID, eppUserID);
      bankAccountParams.SessionId = userSessionIdAsync;
      return await this._client.ArchiveBankAccountAsync(bankAccountParams);
    }), extPaymentProcessor));
    graph = (APExternalPaymentProcessorMaint) null;
  }

  public override async System.Threading.Tasks.Task ArchiveBankAccountUserAsync(
    APExternalPaymentProcessor extPaymentProcessor,
    APPaymentProcessorAccountUser extBankAccountUser,
    OrganizationUserData organizationUser)
  {
    BillComManager billComManager = this;
    int? organizationId = organizationUser.OrganizationId;
    APExternalPaymentProcessorMaint graph = billComManager.GetAPExternalPaymentProcessorGraph(extPaymentProcessor, organizationUser.OrganizationId);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = billComManager.GetOrganizationAndUser(graph, organizationUser);
    APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
    string eppUserID = organizationAndUser.eppUser.ExternalUserID;
    string eppOrganizationID = eppOrganization.ExternalOrganizationID;
    GetBankAccountUserParams bankAccountUserParams = new GetBankAccountUserParams();
    bankAccountUserParams.ExternalBankAccountUserId = extBankAccountUser.ExternalID;
    graph.UpdateBankAccountUser(await billComManager.PerformApiCallWithRetry<ExternalBankAccountUser>((Func<Task<ExternalBankAccountUser>>) (async () =>
    {
      string userSessionIdAsync = await this.GetUserSessionIdAsync(extPaymentProcessor, eppOrganizationID, eppUserID);
      bankAccountUserParams.SessionId = userSessionIdAsync;
      return await this._client.ArchiveBankAccountUserAsync(bankAccountUserParams);
    }), extPaymentProcessor));
    graph = (APExternalPaymentProcessorMaint) null;
  }

  public override async Task<ExternalWebhook> SubscribeWebhookAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    WebHook webHook,
    OrganizationUserData orgUserData)
  {
    BillComManager billComManager = this;
    APExternalPaymentProcessorMaint graph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = billComManager.GetOrganizationAndUser(graph, orgUserData);
    APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
    string eppUserID = organizationAndUser.eppUser.ExternalUserID;
    string eppOrganizationID = eppOrganization.ExternalOrganizationID;
    WebhookData data = new WebhookData()
    {
      Name = $"{eppOrganization.ExternalPaymentProcessorID} {eppOrganization.OrganizationID.ToString()} Webhook",
      Url = webHook.Url
    };
    ExternalWebhook externalWebhook1 = await billComManager.PerformApiCallWithRetry<ExternalWebhook>((Func<Task<ExternalWebhook>>) (async () =>
    {
      string userSessionIdAsync = await this.GetUserSessionIdAsync(externalPaymentProcessor, eppOrganizationID, eppUserID);
      data.SessionId = userSessionIdAsync;
      return await this._client.SubscribeWebhookAsync(data);
    }), externalPaymentProcessor);
    graph.UpdateExternalWebhookID(eppOrganization, webHook, externalWebhook1);
    ExternalWebhook externalWebhook2 = externalWebhook1;
    graph = (APExternalPaymentProcessorMaint) null;
    eppOrganization = (APPaymentProcessorOrganization) null;
    return externalWebhook2;
  }

  public override async Task<ExternalWebhook> UnsubscribeWebhookAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    OrganizationUserData orgUserData)
  {
    BillComManager billComManager = this;
    APExternalPaymentProcessorMaint graph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = billComManager.GetOrganizationAndUser(graph, orgUserData);
    APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
    string eppUserID = organizationAndUser.eppUser.ExternalUserID;
    string eppOrganizationID = eppOrganization.ExternalOrganizationID;
    WebhookData webhookData = new WebhookData()
    {
      WebhookId = eppOrganization.ExternalWebhookID
    };
    ExternalWebhook externalWebhook1 = await billComManager.PerformApiCallWithRetry<ExternalWebhook>((Func<Task<ExternalWebhook>>) (async () =>
    {
      string userSessionIdAsync = await this.GetUserSessionIdAsync(externalPaymentProcessor, eppOrganizationID, eppUserID);
      webhookData.SessionId = userSessionIdAsync;
      return await this._client.UnsubscribeWebhookAsync(webhookData);
    }), externalPaymentProcessor);
    graph.DeleteWebHookSubscription(eppOrganization, externalWebhook1);
    ExternalWebhook externalWebhook2 = externalWebhook1;
    graph = (APExternalPaymentProcessorMaint) null;
    eppOrganization = (APPaymentProcessorOrganization) null;
    return externalWebhook2;
  }

  public override async Task<ExternalVendor> GetVendorDetailsAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    APPaymentProcessorVendor vendor,
    OrganizationUserData organizationUser)
  {
    BillComManager billComManager = this;
    if (vendor?.ExternalVendorID == null)
      return (ExternalVendor) null;
    APExternalPaymentProcessorMaint paymentProcessorGraph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = billComManager.GetOrganizationAndUser(paymentProcessorGraph, organizationUser);
    APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
    string eppUserID = organizationAndUser.eppUser.ExternalUserID;
    string eppOrganizationID = eppOrganization.ExternalOrganizationID;
    VendorLocationMaint instance = PXGraph.CreateInstance<VendorLocationMaint>();
    VendorLocationMaintEPPExt graphExt = instance.GetExtension<VendorLocationMaintEPPExt>();
    PX.Objects.CR.Location location = PX.Objects.CR.Location.PK.Find((PXGraph) instance, vendor.BAccountID, vendor.LocationID);
    instance.Location.Current = location;
    GetVendorParams vendorParams = new GetVendorParams();
    vendorParams.ExternalVendorId = vendor.ExternalVendorID;
    ExternalVendor response = await billComManager.PerformApiCallWithRetry<ExternalVendor>((Func<Task<ExternalVendor>>) (async () =>
    {
      string userSessionIdAsync = await this.GetUserSessionIdAsync(externalPaymentProcessor, eppOrganizationID, eppUserID);
      vendorParams.SessionId = userSessionIdAsync;
      return await this._client.GetVendorDetailsAsync(vendorParams);
    }), externalPaymentProcessor);
    if (vendor.NetworkStatus != response?.NetworkStatus || vendor.PayByType != response?.PayByType)
      graphExt.UpdateExternalPaymentProcessorVendor(vendor, location, response);
    return response;
  }

  public override async Task<ExternalVendor> CreateVendorAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    PX.Objects.CR.Location location,
    OrganizationUserData organizationUser)
  {
    BillComManager billComManager = this;
    APExternalPaymentProcessorMaint paymentProcessorGraph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = billComManager.GetOrganizationAndUser(paymentProcessorGraph, organizationUser);
    APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
    string eppUserID = organizationAndUser.eppUser.ExternalUserID;
    string eppOrganizationID = eppOrganization.ExternalOrganizationID;
    int? organizationId = organizationUser.OrganizationId;
    APPaymentProcessorVendor paymentProcessorVendor = APPaymentProcessorVendor.PK.Find((PXGraph) paymentProcessorGraph, externalPaymentProcessor?.ExternalPaymentProcessorID, organizationId, (int?) location?.BAccountID, (int?) location?.LocationID);
    if (paymentProcessorVendor != null)
      return new ExternalVendor()
      {
        Id = paymentProcessorVendor.ExternalVendorID
      };
    VendorLocationMaint instance = PXGraph.CreateInstance<VendorLocationMaint>();
    VendorLocationMaintEPPExt graphExt = instance.GetExtension<VendorLocationMaintEPPExt>();
    instance.Location.Current = location;
    VendorData request = billComManager.BuildVendorRequest(instance);
    ExternalVendor externalVendor = await billComManager.PerformApiCallWithRetry<ExternalVendor>((Func<Task<ExternalVendor>>) (async () =>
    {
      string userSessionIdAsync = await this.GetUserSessionIdAsync(externalPaymentProcessor, eppOrganizationID, eppUserID);
      request.SessionId = userSessionIdAsync;
      return await this._client.CreateVendorAsync(request);
    }), externalPaymentProcessor);
    graphExt.AddExternalPaymentProcessorVendor(eppOrganization, location, externalVendor);
    return externalVendor;
  }

  public override async Task<ExternalVendor> UpdateVendorAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    APPaymentProcessorVendor vendor,
    OrganizationUserData organizationUser)
  {
    BillComManager billComManager = this;
    if (vendor?.ExternalVendorID == null)
      return (ExternalVendor) null;
    APExternalPaymentProcessorMaint paymentProcessorGraph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = billComManager.GetOrganizationAndUser(paymentProcessorGraph, organizationUser);
    APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
    string extUserId = organizationAndUser.eppUser.ExternalUserID;
    string extOrganizationId = eppOrganization.ExternalOrganizationID;
    VendorLocationMaint instance = PXGraph.CreateInstance<VendorLocationMaint>();
    VendorLocationMaintEPPExt graphExt = instance.GetExtension<VendorLocationMaintEPPExt>();
    PX.Objects.CR.Location location = PX.Objects.CR.Location.PK.Find((PXGraph) instance, vendor.BAccountID, vendor.LocationID);
    instance.Location.Current = location;
    VendorData vendorData = billComManager.BuildVendorRequest(instance, false);
    vendorData.ExternalVendorId = vendor.ExternalVendorID;
    ExternalVendor response = await billComManager.PerformApiCallWithRetry<ExternalVendor>((Func<Task<ExternalVendor>>) (async () =>
    {
      string userSessionIdAsync = await this.GetUserSessionIdAsync(externalPaymentProcessor, extOrganizationId, extUserId);
      vendorData.SessionId = userSessionIdAsync;
      return await this._client.UpdateVendorAsync(vendorData);
    }), externalPaymentProcessor);
    if (vendor.NetworkStatus != response?.NetworkStatus || vendor.PayByType != response?.PayByType)
      graphExt.UpdateExternalPaymentProcessorVendor(vendor, location, response);
    return response;
  }

  private async Task<string> CreateBillAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    BillData billData,
    string docType,
    string refNbr,
    int? organizationID,
    Guid? userID = null)
  {
    BillComManager billComManager = this;
    APExternalPaymentProcessorMaint paymentProcessorGraph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = billComManager.GetOrganizationAndUser(paymentProcessorGraph, organizationID, userID);
    APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
    string eppUserID = organizationAndUser.eppUser.ExternalUserID;
    string eppOrganizationID = eppOrganization.ExternalOrganizationID;
    APInvoiceEntryEPPExt graphExt = PXGraph.CreateInstance<APInvoiceEntry>().GetExtension<APInvoiceEntryEPPExt>();
    ExternalBill response = await billComManager.PerformApiCallWithRetry<ExternalBill>((Func<Task<ExternalBill>>) (async () =>
    {
      string userSessionIdAsync = await this.GetUserSessionIdAsync(externalPaymentProcessor, eppOrganizationID, eppUserID);
      billData.SessionId = userSessionIdAsync;
      return await this._client.CreateBillAsync(billData);
    }), externalPaymentProcessor);
    graphExt.AddExternalPaymentProcessorBill(eppOrganization, response, docType, refNbr);
    string id = response.Id;
    eppOrganization = (APPaymentProcessorOrganization) null;
    graphExt = (APInvoiceEntryEPPExt) null;
    return id;
  }

  private async Task<string> CreateVendorCreditAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    BillData billData,
    string docType,
    string refNbr,
    int? organizationID,
    Guid? userID = null)
  {
    BillComManager billComManager = this;
    APExternalPaymentProcessorMaint paymentProcessorGraph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = billComManager.GetOrganizationAndUser(paymentProcessorGraph, organizationID, userID);
    APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
    string eppUserID = organizationAndUser.eppUser.ExternalUserID;
    string eppOrganizationID = eppOrganization.ExternalOrganizationID;
    APInvoiceEntryEPPExt graphExt = PXGraph.CreateInstance<APInvoiceEntry>().GetExtension<APInvoiceEntryEPPExt>();
    ExternalBill response = await billComManager.PerformApiCallWithRetry<ExternalBill>((Func<Task<ExternalBill>>) (async () =>
    {
      string userSessionIdAsync = await this.GetUserSessionIdAsync(externalPaymentProcessor, eppOrganizationID, eppUserID);
      billData.SessionId = userSessionIdAsync;
      return await this._client.CreateVendorCreditAsync(billData);
    }), externalPaymentProcessor);
    graphExt.AddExternalPaymentProcessorBill(eppOrganization, response, docType, refNbr);
    string id = response.Id;
    eppOrganization = (APPaymentProcessorOrganization) null;
    graphExt = (APInvoiceEntryEPPExt) null;
    return id;
  }

  public override async Task<ExternalPayment> ProcessPaymentAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    PX.Objects.AP.APPayment payment,
    OrganizationUserData organizationUser)
  {
    BillComManager billComManager = this;
    int? organizationId = organizationUser.OrganizationId;
    Guid? userId = organizationUser.UserId;
    if (!string.IsNullOrEmpty(payment.ExternalPaymentID))
      return new ExternalPayment()
      {
        Id = payment.ExternalPaymentID
      };
    if (payment.ExternalPaymentID != null)
      throw new PXException("The payment has already been created in the external payment processor.");
    if (payment.ExternalPaymentStatus == "UNDEFINED")
      throw new PXException("The payment was not processed successfully by the external payment processor. Click Synchronize Payment on the Checks and Payments (AP302000) form and try to process it again.");
    if (!billComManager.IsCurrencySupported(payment.CuryID))
      throw new PXException("Payments for non-USD documents are not supported.");
    if (!billComManager.IsAdjustmentsCurrencySupported(payment))
      throw new PXException("Payments for non-USD documents are not supported.");
    if (!organizationId.HasValue)
      throw new PXException("The operation failed because the company does not exist in the external payment processor.");
    APPaymentEntry apPaymentEntry = PXGraph.CreateInstance<APPaymentEntry>();
    PX.Objects.CR.Location location = PX.Objects.CR.Location.PK.Find((PXGraph) apPaymentEntry, payment.VendorID, payment.VendorLocationID);
    await billComManager.ProcessVendor(externalPaymentProcessor, organizationId, userId, (PXGraph) apPaymentEntry, location);
    APPaymentProcessorVendor eppVendor = APPaymentProcessorVendor.PK.Find((PXGraph) apPaymentEntry, externalPaymentProcessor?.ExternalPaymentProcessorID, organizationId, (int?) location?.BAccountID, (int?) location?.LocationID);
    await billComManager.CreateBills(externalPaymentProcessor, payment, organizationId, userId, apPaymentEntry, eppVendor);
    return await billComManager.CreatePaymentAsync(externalPaymentProcessor, payment, organizationId, userId);
  }

  private async Task<ExternalPayment> CreatePaymentAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    PX.Objects.AP.APPayment payment,
    int? organizationID,
    Guid? userID)
  {
    BillComManager billComManager = this;
    if (!string.IsNullOrEmpty(payment.ExternalPaymentID))
      return new ExternalPayment()
      {
        Id = payment.ExternalPaymentID
      };
    APExternalPaymentProcessorMaint paymentProcessorGraph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = billComManager.GetOrganizationAndUser(paymentProcessorGraph, organizationID, userID);
    APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
    string eppUserID = organizationAndUser.eppUser.ExternalUserID;
    string extOrganizationId = eppOrganization.ExternalOrganizationID;
    APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
    APPaymentEntryEPPExt graphExt = instance.GetExtension<APPaymentEntryEPPExt>();
    instance.Document.Current = payment;
    instance.Save.Press();
    PaymentData paymentRequest = billComManager.BuildPaymentRequest(instance, externalPaymentProcessor, payment, organizationID);
    if (paymentRequest.PaymentDetails.Count<AdjustmentDetail>() == 0)
      return (ExternalPayment) null;
    APPaymentProcessorPaymentTran paymentTran = graphExt.StartNewTransaction(externalPaymentProcessor, payment);
    paymentRequest.TransactionNumber = paymentTran.TransactionNumber;
    try
    {
      ExternalPayment paymentInfo = await billComManager.PerformApiCallWithRetry<ExternalPayment>((Func<Task<ExternalPayment>>) (async () =>
      {
        string userSessionIdAsync = await this.GetUserSessionIdAsync(externalPaymentProcessor, extOrganizationId, eppUserID);
        paymentRequest.SessionId = userSessionIdAsync;
        return await this._client.CreatePaymentAsync(paymentRequest);
      }), externalPaymentProcessor);
      paymentTran.TransactionState = "S";
      graphExt.UpdatePaymentForCreatePayment(eppOrganization, payment, paymentTran, paymentInfo);
      return paymentInfo;
    }
    catch (Exception ex)
    {
      paymentTran.TransactionState = ex is WebException || ex.InnerException is WebException ? "O" : "E";
      paymentTran.ExternalComment = StringExtensions.Truncate(ex.Message, 512 /*0x0200*/);
      graphExt.FinalizeUnsuccessfulTransaction(payment, paymentTran, ex.Message);
      throw;
    }
  }

  private async System.Threading.Tasks.Task ProcessVendor(
    APExternalPaymentProcessor externalPaymentProcessor,
    int? organizationId,
    Guid? userId,
    PXGraph graph,
    PX.Objects.CR.Location location)
  {
    BillComManager billComManager = this;
    APPaymentProcessorVendor eppVendor = APPaymentProcessorVendor.PK.Find(graph, externalPaymentProcessor?.ExternalPaymentProcessorID, organizationId, (int?) location?.BAccountID, (int?) location?.LocationID);
    OrganizationUserData organizationUser = new OrganizationUserData();
    organizationUser.OrganizationId = organizationId;
    organizationUser.UserId = userId;
    if (eppVendor == null)
    {
      ExternalVendor vendorAsync = await billComManager.CreateVendorAsync(externalPaymentProcessor, location, organizationUser);
      eppVendor = (APPaymentProcessorVendor) null;
      organizationUser = (OrganizationUserData) null;
    }
    else
    {
      ExternalVendor getExternalVendorResponse = await billComManager.GetVendorDetailsAsync(externalPaymentProcessor, eppVendor, organizationUser);
      eppVendor = APPaymentProcessorVendor.PK.Find(graph, externalPaymentProcessor?.ExternalPaymentProcessorID, organizationId, (int?) location?.BAccountID, (int?) location?.LocationID);
      if (EnumerableExtensions.IsIn<string>(eppVendor.NetworkStatus, "NOT_CONNECTED", "PENDING"))
      {
        eppVendor.IsRemittanceAddressChanged.GetValueOrDefault();
        int num = eppVendor.IsBankDetailsChanged.GetValueOrDefault() ? 1 : 0;
        VendorLocationMaint instance = PXGraph.CreateInstance<VendorLocationMaint>();
        instance.Location.Current = location;
        VendorLocationMaintEPPExt graphExt = instance.GetExtension<VendorLocationMaintEPPExt>();
        APExternalPaymentProcessorMaint paymentProcessorGraph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor);
        (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = billComManager.GetOrganizationAndUser(paymentProcessorGraph, organizationId, userId);
        APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
        string externalUserId = organizationAndUser.eppUser.ExternalUserID;
        string externalOrganizationId = eppOrganization.ExternalOrganizationID;
        if (num != 0)
        {
          VendorData vendorData1 = billComManager.BuildVendorRequest(instance);
          vendorData1.ExternalVendorId = eppVendor.ExternalVendorID;
          VendorData vendorData2 = vendorData1;
          vendorData2.SessionId = await billComManager.GetUserSessionIdAsync(externalPaymentProcessor, externalOrganizationId, externalUserId);
          vendorData2 = (VendorData) null;
          if (getExternalVendorResponse.bankAccount != null)
          {
            ExternalVendor externalVendor = await billComManager._client.DeleteVendorBankAccountAsync(vendorData1);
          }
          BankAccountData bankAccountData = await billComManager._client.AddVendorBankDetailsAsync(vendorData1);
          graphExt.MarkEppVendorBankDetailsUpdated(eppVendor);
          vendorData1 = (VendorData) null;
        }
        ExternalVendor externalVendor1 = await billComManager.UpdateVendorAsync(externalPaymentProcessor, eppVendor, organizationUser);
        graphExt.MarkEppVendorAddressUpdated(eppVendor);
        graphExt = (VendorLocationMaintEPPExt) null;
      }
      getExternalVendorResponse = (ExternalVendor) null;
      eppVendor = (APPaymentProcessorVendor) null;
      organizationUser = (OrganizationUserData) null;
    }
  }

  private async System.Threading.Tasks.Task CreateBills(
    APExternalPaymentProcessor externalPaymentProcessor,
    PX.Objects.AP.APPayment payment,
    int? organizationID,
    Guid? userID,
    APPaymentEntry apPaymentEntry,
    APPaymentProcessorVendor eppVendor)
  {
    APPaymentEntryEPPExt graphExt = apPaymentEntry.GetExtension<APPaymentEntryEPPExt>();
    foreach (APAdjust paymentAdjustment in graphExt.GetPaymentAdjustments(payment))
      await this.CreateAPBill(externalPaymentProcessor, organizationID, userID, apPaymentEntry, eppVendor, paymentAdjustment);
    if (payment.DocType == "PPM")
    {
      foreach (POAdjust poAdjustment in graphExt.GetPOAdjustments(payment))
        await this.CreatePOBill(externalPaymentProcessor, organizationID, userID, apPaymentEntry, eppVendor, poAdjustment);
      PX.Objects.AP.APPayment apPayment = payment;
      int num1;
      if (apPayment == null)
      {
        num1 = 0;
      }
      else
      {
        Decimal? curyUnappliedBal = apPayment.CuryUnappliedBal;
        Decimal num2 = 0M;
        num1 = curyUnappliedBal.GetValueOrDefault() > num2 & curyUnappliedBal.HasValue ? 1 : 0;
      }
      if (num1 != 0)
        await this.CreatePrepaymentBill(externalPaymentProcessor, organizationID, userID, apPaymentEntry, eppVendor, payment);
    }
    if (!(payment.DocType == "QCK"))
    {
      graphExt = (APPaymentEntryEPPExt) null;
    }
    else
    {
      foreach (PX.Objects.AP.APTran purchaseAdjustment in graphExt.GetCashPurchaseAdjustments(payment))
        await this.CreateCashPurchaseBill(externalPaymentProcessor, organizationID, userID, apPaymentEntry, eppVendor, purchaseAdjustment);
      graphExt = (APPaymentEntryEPPExt) null;
    }
  }

  private async System.Threading.Tasks.Task CreateAPBill(
    APExternalPaymentProcessor externalPaymentProcessor,
    int? organizationID,
    Guid? userID,
    APPaymentEntry apPaymentEntry,
    APPaymentProcessorVendor eppVendor,
    APAdjust res)
  {
    BillComManager billComManager = this;
    PX.Objects.AP.APInvoice invoice = PX.Objects.AP.APInvoice.PK.Find((PXGraph) apPaymentEntry, res?.AdjdDocType, res?.AdjdRefNbr);
    if (APPaymentProcessorBill.PK.Find((PXGraph) apPaymentEntry, externalPaymentProcessor?.ExternalPaymentProcessorID, organizationID, invoice?.DocType, invoice?.RefNbr) != null)
      return;
    if (!billComManager.IsCurrencySupported(invoice.CuryID))
      throw new PXException("Payments for non-USD documents are not supported.");
    BillData billData = billComManager.BuildAPBillRequest(invoice, eppVendor);
    if (invoice.DocType == "ADR")
    {
      string vendorCreditAsync = await billComManager.CreateVendorCreditAsync(externalPaymentProcessor, billData, invoice?.DocType, invoice?.RefNbr, organizationID, userID);
    }
    else
    {
      string billAsync = await billComManager.CreateBillAsync(externalPaymentProcessor, billData, invoice?.DocType, invoice?.RefNbr, organizationID, userID);
    }
  }

  private async System.Threading.Tasks.Task CreatePOBill(
    APExternalPaymentProcessor externalPaymentProcessor,
    int? organizationID,
    Guid? userID,
    APPaymentEntry apPaymentEntry,
    APPaymentProcessorVendor eppVendor,
    POAdjust res)
  {
    BillComManager billComManager = this;
    APPaymentEntryEPPExt extension = apPaymentEntry.GetExtension<APPaymentEntryEPPExt>();
    PX.Objects.PO.POOrder order = PX.Objects.PO.POOrder.PK.Find((PXGraph) apPaymentEntry, res?.AdjdOrderType, res?.AdjdOrderNbr);
    APExternalPaymentProcessor ePP = externalPaymentProcessor;
    int? organizationID1 = organizationID;
    string orderType = order?.OrderType;
    string orderNbr = order?.OrderNbr;
    if (extension.GetExternalPaymentProcessorBill(ePP, organizationID1, orderType, orderNbr) != null)
      return;
    if (!billComManager.IsCurrencySupported(order.CuryID))
      throw new PXException("Payments for non-USD documents are not supported.");
    BillData billData = billComManager.BuildPOBillRequest(order, eppVendor);
    string billAsync = await billComManager.CreateBillAsync(externalPaymentProcessor, billData, order?.OrderType, order?.OrderNbr, organizationID, userID);
  }

  private async System.Threading.Tasks.Task CreatePrepaymentBill(
    APExternalPaymentProcessor externalPaymentProcessor,
    int? organizationID,
    Guid? userID,
    APPaymentEntry apPaymentEntry,
    APPaymentProcessorVendor eppVendor,
    PX.Objects.AP.APPayment res)
  {
    BillComManager billComManager = this;
    if (APPaymentProcessorBill.PK.Find((PXGraph) apPaymentEntry, externalPaymentProcessor?.ExternalPaymentProcessorID, organizationID, res?.DocType, res?.RefNbr) != null)
      return;
    if (!billComManager.IsCurrencySupported(res.CuryID))
      throw new PXException("Payments for non-USD documents are not supported.");
    BillData billData = billComManager.BuildPrepaymentBillRequest(res, eppVendor);
    string billAsync = await billComManager.CreateBillAsync(externalPaymentProcessor, billData, res?.DocType, res?.RefNbr, organizationID, userID);
  }

  private async System.Threading.Tasks.Task CreateCashPurchaseBill(
    APExternalPaymentProcessor externalPaymentProcessor,
    int? organizationID,
    Guid? userID,
    APPaymentEntry apPaymentEntry,
    APPaymentProcessorVendor eppVendor,
    PX.Objects.AP.APTran res)
  {
    BillComManager billComManager = this;
    PX.Objects.AP.APInvoice invoice = PX.Objects.AP.APInvoice.PK.Find((PXGraph) apPaymentEntry, res?.TranType, res?.RefNbr);
    if (APPaymentProcessorBill.PK.Find((PXGraph) apPaymentEntry, externalPaymentProcessor?.ExternalPaymentProcessorID, organizationID, invoice?.DocType, invoice?.RefNbr) != null)
      return;
    if (!billComManager.IsCurrencySupported(invoice.CuryID))
      throw new PXException("Payments for non-USD documents are not supported.");
    BillData billData = billComManager.BuildAPBillRequest(invoice, eppVendor);
    string billAsync = await billComManager.CreateBillAsync(externalPaymentProcessor, billData, invoice?.DocType, invoice?.RefNbr, organizationID, userID);
  }

  public override async Task<ExternalPayment> CancelPaymentAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    PX.Objects.AP.APPayment payment,
    OrganizationUserData organizationUser)
  {
    BillComManager billComManager = this;
    if (string.IsNullOrEmpty(payment.ExternalPaymentID))
      return (ExternalPayment) null;
    bool cancelFailed = false;
    APExternalPaymentProcessorMaint paymentProcessorGraph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = billComManager.GetOrganizationAndUser(paymentProcessorGraph, organizationUser);
    APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
    string eppUserID = organizationAndUser.eppUser.ExternalUserID;
    string eppOrganizationID = eppOrganization.ExternalOrganizationID;
    APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
    APPaymentEntryEPPExt graphExt = instance.GetExtension<APPaymentEntryEPPExt>();
    instance.Document.Current = payment;
    PaymentData paymentData = new PaymentData()
    {
      ExternalPaymentId = payment.ExternalPaymentID
    };
    ExternalPayment extPayment = (ExternalPayment) null;
    try
    {
      extPayment = await billComManager.PerformApiCallWithRetry<ExternalPayment>((Func<Task<ExternalPayment>>) (async () =>
      {
        string userSessionIdAsync = await this.GetUserSessionIdAsync(externalPaymentProcessor, eppOrganizationID, eppUserID);
        paymentData.SessionId = userSessionIdAsync;
        return await this._client.CancelPaymentAsync(paymentData);
      }), externalPaymentProcessor);
      graphExt.UpdatePaymentForCancelRequest(eppOrganization, payment, extPayment);
    }
    catch (PaymentProcessorException ex) when (ex.Reason == 2)
    {
      cancelFailed = true;
    }
    if (cancelFailed)
      extPayment = await billComManager.VoidPaymentAsync(externalPaymentProcessor, payment, organizationUser);
    return extPayment;
  }

  public override async Task<ExternalPayment> VoidPaymentAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    PX.Objects.AP.APPayment payment,
    OrganizationUserData organizationUser)
  {
    BillComManager billComManager = this;
    int? organizationId = organizationUser.OrganizationId;
    if (string.IsNullOrEmpty(payment.ExternalPaymentID))
      return (ExternalPayment) null;
    APExternalPaymentProcessorMaint paymentProcessorGraph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor, organizationId);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = billComManager.GetOrganizationAndUser(paymentProcessorGraph, organizationUser);
    APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
    string eppUserID = organizationAndUser.eppUser.ExternalUserID;
    string eppOrganizationID = eppOrganization.ExternalOrganizationID;
    APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
    APPaymentEntryEPPExt graphExt = instance.GetExtension<APPaymentEntryEPPExt>();
    instance.Document.Current = payment;
    instance.Save.Press();
    PaymentData paymentData = new PaymentData()
    {
      ExternalPaymentId = payment.ExternalPaymentID,
      Description = "Payment voiding requested by Acumatica user"
    };
    ExternalPayment paymentInfo = await billComManager.PerformApiCallWithRetry<ExternalPayment>((Func<Task<ExternalPayment>>) (async () =>
    {
      string userSessionIdAsync = await this.GetUserSessionIdAsync(externalPaymentProcessor, eppOrganizationID, eppUserID);
      paymentData.SessionId = userSessionIdAsync;
      return await this._client.VoidPaymentAsync(paymentData);
    }), externalPaymentProcessor);
    graphExt.UpdatePaymentForVoidRequest(eppOrganization, payment, paymentInfo);
    return paymentInfo;
  }

  public override async System.Threading.Tasks.Task UpdatePaymentDetailsAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    PX.Objects.AP.APPayment payment,
    OrganizationUserData organizationUser)
  {
    BillComManager billComManager = this;
    APExternalPaymentProcessorMaint paymentProcessorGraph = billComManager.GetAPExternalPaymentProcessorGraph(externalPaymentProcessor);
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = billComManager.GetOrganizationAndUser(paymentProcessorGraph, organizationUser);
    APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
    string eppUserID = organizationAndUser.eppUser.ExternalUserID;
    string eppOrganizationID = eppOrganization.ExternalOrganizationID;
    APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
    APPaymentEntryEPPExt graphExt = instance.GetExtension<APPaymentEntryEPPExt>();
    instance.Document.Current = payment;
    instance.Save.Press();
    GetPaymentParams getPaymentParams1 = new GetPaymentParams();
    string externalPaymentId = payment.ExternalPaymentID;
    if (externalPaymentId == null)
    {
      Guid? noteId = payment.NoteID;
      ref Guid? local = ref noteId;
      externalPaymentId = local.HasValue ? local.GetValueOrDefault().ToString("N") : (string) null;
    }
    getPaymentParams1.ExternalPaymentId = externalPaymentId;
    GetPaymentParams getPaymentParams = getPaymentParams1;
    graphExt.UpdatePaymentForPaymentDetail(eppOrganization, payment, await billComManager.PerformApiCallWithRetry<ExternalPayment>((Func<Task<ExternalPayment>>) (async () =>
    {
      string userSessionIdAsync = await this.GetUserSessionIdAsync(externalPaymentProcessor, eppOrganizationID, eppUserID);
      getPaymentParams.SessionId = userSessionIdAsync;
      ExternalPayment externalPayment;
      if (!Str.IsNullOrEmpty(payment.ExternalPaymentID))
        externalPayment = await this._client.GetPaymentDetailsAsync(getPaymentParams);
      else
        externalPayment = (await this._client.GetPaymentsAsync(getPaymentParams)).FirstOrDefault<ExternalPayment>();
      return externalPayment;
    }), externalPaymentProcessor));
    eppOrganization = (APPaymentProcessorOrganization) null;
    graphExt = (APPaymentEntryEPPExt) null;
  }

  private async Task<string> GetUserSessionIdAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    string externalOrganizationID,
    string externalUserID)
  {
    GetSessionParams getSessionParams = new GetSessionParams()
    {
      ExternalUserId = externalUserID,
      ExternalOrganizationId = externalOrganizationID
    };
    string sessionId;
    if (externalPaymentProcessor.UserSessionStore.HasValue)
    {
      UserSessionStore sessionStore = externalPaymentProcessor.UserSessionStore.Value;
      Dictionary<string, string> sessionDict;
      if (((UserSessionStore) ref sessionStore).UserSessionIdForOrganization == null)
      {
        sessionDict = new Dictionary<string, string>();
        ((UserSessionStore) ref sessionStore).UserSessionIdForOrganization = sessionDict;
      }
      else
        sessionDict = ((UserSessionStore) ref sessionStore).UserSessionIdForOrganization;
      if (sessionDict.ContainsKey(externalOrganizationID) && sessionDict[externalOrganizationID] != null)
      {
        sessionId = sessionDict[externalOrganizationID];
      }
      else
      {
        sessionId = (await this._client.GetSessionAsync(getSessionParams)).SessionId;
        sessionDict.Add(externalOrganizationID, sessionId);
        externalPaymentProcessor.UserSessionStore = new UserSessionStore?(sessionStore);
        PaymentProcessorSessionHelper.SaveUserSessionStore(sessionStore);
      }
      sessionStore = new UserSessionStore();
      sessionDict = (Dictionary<string, string>) null;
    }
    else
      sessionId = (await this._client.GetSessionAsync(getSessionParams)).SessionId;
    return sessionId;
  }

  private string MakeUserExternalReferenceID(APPaymentProcessorUser eppUser)
  {
    Guid? noteId = eppUser.NoteID;
    ref Guid? local = ref noteId;
    return !local.HasValue ? (string) null : local.GetValueOrDefault().ToString("N");
  }

  private (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) GetOrganizationAndUser(
    APExternalPaymentProcessorMaint graph,
    OrganizationUserData orgUserData)
  {
    return this.GetOrganizationAndUser(graph, orgUserData.OrganizationId, orgUserData.UserId);
  }

  /// <summary>
  /// Get external organization and user from the current context.
  /// </summary>
  /// <param name="graph">External Payment Processor Maint</param>
  /// <param name="organizationID">Internal Organization ID or null if use current</param>
  /// <param name="userID">Internal User ID or null if use current</param>
  /// <returns>A pair of External Processor user and organization</returns>
  /// <exception cref="T:PX.Data.PXException"></exception>
  private (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) GetOrganizationAndUser(
    APExternalPaymentProcessorMaint graph,
    int? organizationID = null,
    Guid? userID = null,
    bool validateExtUser = true)
  {
    APExternalPaymentProcessor current = graph.ExternalPaymentProcessor.Current;
    if (current == null)
      throw new PXException("The operation failed because the external payment processor does not exist.");
    Guid? userID1 = userID ?? this._currentUserInformationProvider.GetUserId();
    if (!userID1.HasValue)
      throw new PXException("The operation failed because the user does not exist in the external payment processor.");
    int? organizationID1 = organizationID;
    if (!organizationID1.HasValue)
    {
      int? branchId = PXAccess.GetBranchID();
      if (!branchId.HasValue)
        throw new PXException("The operation failed because the company does not exist in the external payment processor.");
      organizationID1 = (int?) PXAccess.GetBranch(branchId)?.Organization?.OrganizationID;
      if (!organizationID1.HasValue)
        throw new PXException("The operation failed because the company does not exist in the external payment processor.");
    }
    APPaymentProcessorUser paymentProcessorUser = APPaymentProcessorUser.PK.Find((PXGraph) graph, current?.ExternalPaymentProcessorID, organizationID1, userID1);
    if (paymentProcessorUser == null)
      throw new PXException("The operation failed because the {0} user does not exist in the external payment processor.", new object[1]
      {
        (object) this._currentUserInformationProvider.GetUserName()
      });
    if (validateExtUser && (paymentProcessorUser.ExternalUserID == null || paymentProcessorUser.Status != "O"))
      throw new PXException("The logged-in user is not active in {0}. You cannot send requests to {0} until your user account is activated.");
    return (APPaymentProcessorOrganization.PK.Find((PXGraph) graph, current?.ExternalPaymentProcessorID, organizationID1) ?? throw new PXException("The operation failed because the company does not exist in the external payment processor."), paymentProcessorUser);
  }

  private async System.Threading.Tasks.Task RefreshFundingAccountsAsync(
    APExternalPaymentProcessorMaint graph,
    APExternalPaymentProcessor extPaymentProcessor,
    OrganizationUserData organizationUser)
  {
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = this.GetOrganizationAndUser(graph, organizationUser);
    APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
    string extUserID = organizationAndUser.eppUser.ExternalUserID;
    string extOrganizationID = eppOrganization.ExternalOrganizationID;
    graph.RefreshBankAccounts(await this.PerformApiCallWithRetry<IEnumerable<ExternalBankAccount>>((Func<Task<IEnumerable<ExternalBankAccount>>>) (async () =>
    {
      string userSessionIdAsync = await this.GetUserSessionIdAsync(extPaymentProcessor, extOrganizationID, extUserID);
      return await this._client.GetBankAccountsAsync(new GetBankAccountParams()
      {
        SessionId = userSessionIdAsync
      });
    }), extPaymentProcessor), eppOrganization);
    eppOrganization = (APPaymentProcessorOrganization) null;
  }

  private async System.Threading.Tasks.Task RefreshFundingAccountUsersAsync(
    APExternalPaymentProcessorMaint graph,
    APExternalPaymentProcessor extPaymentProcessor,
    OrganizationUserData organizationUser)
  {
    (APPaymentProcessorOrganization eppOrganization, APPaymentProcessorUser eppUser) organizationAndUser = this.GetOrganizationAndUser(graph, organizationUser);
    APPaymentProcessorOrganization eppOrganization = organizationAndUser.eppOrganization;
    string eppUserID = organizationAndUser.eppUser.ExternalUserID;
    string eppOrganizationID = eppOrganization.ExternalOrganizationID;
    graph.RefreshBankAccountUsers(await this.PerformApiCallWithRetry<IEnumerable<ExternalBankAccountUser>>((Func<Task<IEnumerable<ExternalBankAccountUser>>>) (async () =>
    {
      string userSessionIdAsync = await this.GetUserSessionIdAsync(extPaymentProcessor, eppOrganizationID, eppUserID);
      return await this._client.GetUserBankAccountsAsync(new GetBankAccountParams()
      {
        SessionId = userSessionIdAsync,
        CurrentUser = false
      });
    }), extPaymentProcessor), eppOrganization);
    eppOrganization = (APPaymentProcessorOrganization) null;
  }

  private void ClearUserSessionStore(APExternalPaymentProcessor extPaymentProcessor)
  {
    extPaymentProcessor.UserSessionStore = new UserSessionStore?(new UserSessionStore());
  }

  private async Task<T> PerformApiCallWithRetry<T>(
    Func<Task<T>> action,
    APExternalPaymentProcessor extPaymentProcessor)
    where T : class
  {
    PaymentProcessorException ex;
    int num;
    try
    {
      return await action();
    }
    catch (PaymentProcessorException ex1) when (
    {
      // ISSUE: unable to correctly present filter
      ex = ex1;
      if (ex.Reason == 1)
      {
        SuccessfulFiltering;
      }
      else
        throw;
    }
    )
    {
      num = 1;
    }
    catch (Exception ex2) when (ex2 is WebException || ex2.InnerException is WebException)
    {
      if (!(ex2 is WebException webException))
        webException = ex2.InnerException as WebException;
      WebException innerException = webException;
      throw new WebException(innerException?.Message ?? ex2.Message, (Exception) innerException);
    }
    if (num == 1)
    {
      UserSessionStore? userSessionStore = extPaymentProcessor.UserSessionStore;
      ref UserSessionStore? local = ref userSessionStore;
      Dictionary<string, string> dictionary;
      if (!local.HasValue)
      {
        dictionary = (Dictionary<string, string>) null;
      }
      else
      {
        UserSessionStore valueOrDefault = local.GetValueOrDefault();
        dictionary = ((UserSessionStore) ref valueOrDefault).UserSessionIdForOrganization;
      }
      if (dictionary != null)
      {
        this.ClearUserSessionStore(extPaymentProcessor);
        try
        {
          return await action();
        }
        catch (Exception ex3)
        {
          throw new PXException(PXMessages.LocalizeFormatNoPrefix("The user session has been updated. {0}", (object) ex3.Message), (Exception) ex);
        }
      }
      else
      {
        if (!((object) ex1 is Exception source))
          throw (object) ex1;
        ExceptionDispatchInfo.Capture(source).Throw();
      }
    }
    ex = (PaymentProcessorException) null;
    T obj;
    return obj;
  }

  private async Task<string> GetUserSessionIdForWidgetAsync(
    APExternalPaymentProcessor extPaymentProcessor,
    string extOrganizationId,
    string extUserId)
  {
    string sessionId = await this.GetUserSessionIdAsync(extPaymentProcessor, extOrganizationId, extUserId);
    GetSessionParams sessionParams = new GetSessionParams();
    sessionParams.SessionId = sessionId;
    sessionParams.ExternalOrganizationId = extOrganizationId;
    sessionParams.ExternalUserId = extUserId;
    try
    {
      MfaResponse sessionDetailsAsync = await this._client.GetSessionDetailsAsync(sessionParams);
    }
    catch (PaymentProcessorException ex) when (ex.Reason == 1)
    {
      sessionId = (await this._client.GetSessionAsync(sessionParams)).SessionId;
    }
    string idForWidgetAsync = sessionId;
    sessionId = (string) null;
    sessionParams = (GetSessionParams) null;
    return idForWidgetAsync;
  }

  private OrganizationData BuildOrganizationRequest(OrganizationMaint organizationMaint)
  {
    PX.Objects.GL.DAC.Organization current1 = organizationMaint.OrganizationView.Current;
    OrganizationBAccount current2 = organizationMaint.BAccount.Current;
    OrganizationMaint.DefContactAddressExt extension = organizationMaint.GetExtension<OrganizationMaint.DefContactAddressExt>();
    PX.Objects.CR.Address address = (PX.Objects.CR.Address) extension.DefAddress.Select();
    PX.Objects.CR.Contact contact = (PX.Objects.CR.Contact) extension.DefContact.Select();
    return new OrganizationData()
    {
      Name = current1.OrganizationName,
      Address = new AddressData()
      {
        Line1 = address.AddressLine1,
        Line2 = address.AddressLine2,
        City = address.City,
        StateOrProvince = address.State,
        ZipOrPostalCode = address.PostalCode,
        Country = address.CountryID
      },
      Phone = contact.Phone1
    };
  }

  private PaymentData BuildPaymentRequest(
    APPaymentEntry graph,
    APExternalPaymentProcessor ePP,
    PX.Objects.AP.APPayment payment,
    int? organizationID)
  {
    APPaymentEntryEPPExt extension = graph.GetExtension<APPaymentEntryEPPExt>();
    PX.Objects.CR.Location location = PX.Objects.CR.Location.PK.Find((PXGraph) graph, payment.VendorID, payment.VendorLocationID);
    PX.Objects.CA.CashAccount cashAccount = PX.Objects.CA.CashAccount.PK.Find((PXGraph) graph, payment.CashAccountID);
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) graph, (int?) cashAccount?.BranchID);
    APPaymentProcessorVendor paymentProcessorVendor = APPaymentProcessorVendor.PK.Find((PXGraph) graph, ePP?.ExternalPaymentProcessorID, organizationID, (int?) location?.BAccountID, (int?) location?.LocationID);
    APPaymentProcessorAccount processorAccount = graph.FindImplementation<ExternalPaymentProcessorHelper<APPaymentEntry>>().GetExternalProcessorAccount(cashAccount, branch, ePP);
    PaymentData paymentData1 = new PaymentData();
    paymentData1.VendorId = paymentProcessorVendor?.ExternalVendorID;
    paymentData1.Description = payment.DocDesc;
    paymentData1.PaymentDate = payment.AdjDate.Value;
    Decimal? nullable = payment.CuryOrigDocAmt;
    paymentData1.PaymentAmount = nullable.Value;
    paymentData1.FundingAccount = new FundingAccountData()
    {
      AccountType = "BANK_ACCOUNT",
      AccountID = processorAccount?.ExternalAccountID
    };
    paymentData1.PaymentProcessingOptions = new PaymentProcessingData()
    {
      RequestPayFaster = false,
      CreateBill = false
    };
    PaymentData paymentData2 = paymentData1;
    List<APAdjust> list = extension.GetPaymentAdjustments(payment).OrderByDescending<APAdjust, Decimal?>((Func<APAdjust, Decimal?>) (p => p.CuryAdjgAmt)).ToList<APAdjust>();
    List<DebitAdjustmentDetail> source = new List<DebitAdjustmentDetail>();
    nullable = list.Where<APAdjust>((Func<APAdjust, bool>) (d => d.AdjdDocType == "ADR")).Sum<APAdjust>((Func<APAdjust, Decimal?>) (p => p.CuryAdjgAmt));
    Decimal num1 = nullable.Value;
    foreach (APAdjust apAdjust in list)
    {
      PX.Objects.AP.APInvoice apInvoice = PX.Objects.AP.APInvoice.PK.Find((PXGraph) graph, apAdjust?.AdjdDocType, apAdjust?.AdjdRefNbr);
      APPaymentProcessorBill paymentProcessorBill = APPaymentProcessorBill.PK.Find((PXGraph) graph, ePP?.ExternalPaymentProcessorID, organizationID, apInvoice?.DocType, apInvoice?.RefNbr);
      if (paymentProcessorBill != null)
      {
        if (apInvoice.DocType != "ADR")
        {
          AdjustmentDetail adjustmentDetail1 = new AdjustmentDetail();
          nullable = apAdjust.CuryAdjgAmt;
          adjustmentDetail1.Amount = nullable.Value;
          adjustmentDetail1.BillID = paymentProcessorBill.ExternalBillID;
          AdjustmentDetail adjustmentDetail2 = adjustmentDetail1;
          paymentData2.PaymentDetails.Add(adjustmentDetail2);
        }
        else
        {
          DebitAdjustmentDetail adjustmentDetail3 = new DebitAdjustmentDetail();
          nullable = apAdjust.CuryAdjgAmt;
          adjustmentDetail3.Amount = nullable.Value;
          adjustmentDetail3.BillID = paymentProcessorBill.ExternalBillID;
          DebitAdjustmentDetail adjustmentDetail4 = adjustmentDetail3;
          source.Add(adjustmentDetail4);
        }
      }
    }
    if (source.Any<DebitAdjustmentDetail>() && paymentData2.PaymentDetails.Any<AdjustmentDetail>())
    {
      if (paymentData2.PaymentDetails[0].Amount <= num1)
        throw new PXException("The {0} payment cannot be processed because the total amount of debit adjustments applied to the payment is greater than the highest amount applied to the bill.", new object[1]
        {
          (object) payment.RefNbr
        });
      paymentData2.PaymentDetails[0].CreditToApply = num1;
      paymentData2.PaymentDetails[0].DebitAdjDetails = source;
    }
    if (payment.DocType == "PPM")
    {
      foreach (POAdjust poAdjustment in extension.GetPOAdjustments(payment))
      {
        PX.Objects.PO.POOrder poOrder = PX.Objects.PO.POOrder.PK.Find((PXGraph) graph, poAdjustment?.AdjdOrderType, poAdjustment?.AdjdOrderNbr);
        APPaymentProcessorBill paymentProcessorBill = extension.GetExternalPaymentProcessorBill(ePP, organizationID, poOrder?.OrderType, poOrder?.OrderNbr);
        if (paymentProcessorBill != null)
        {
          AdjustmentDetail adjustmentDetail = new AdjustmentDetail()
          {
            Amount = poAdjustment.CuryAdjgAmt.Value,
            BillID = paymentProcessorBill.ExternalBillID
          };
          paymentData2.PaymentDetails.Add(adjustmentDetail);
        }
      }
      Decimal? curyUnappliedBal = payment.CuryUnappliedBal;
      Decimal num2 = 0M;
      if (!(curyUnappliedBal.GetValueOrDefault() == num2 & curyUnappliedBal.HasValue))
      {
        APPaymentProcessorBill paymentProcessorBill = APPaymentProcessorBill.PK.Find((PXGraph) graph, ePP?.ExternalPaymentProcessorID, organizationID, payment?.DocType, payment?.RefNbr);
        AdjustmentDetail adjustmentDetail = new AdjustmentDetail()
        {
          Amount = payment.CuryUnappliedBal.Value,
          BillID = paymentProcessorBill.ExternalBillID
        };
        paymentData2.PaymentDetails.Add(adjustmentDetail);
      }
    }
    if (payment.DocType == "QCK")
    {
      foreach (PX.Objects.AP.APTran purchaseAdjustment in extension.GetCashPurchaseAdjustments(payment))
      {
        PX.Objects.AP.APInvoice apInvoice = PX.Objects.AP.APInvoice.PK.Find((PXGraph) graph, purchaseAdjustment?.TranType, purchaseAdjustment?.RefNbr);
        APPaymentProcessorBill paymentProcessorBill = APPaymentProcessorBill.PK.Find((PXGraph) graph, ePP?.ExternalPaymentProcessorID, organizationID, apInvoice?.DocType, apInvoice?.RefNbr);
        if (paymentProcessorBill != null)
        {
          AdjustmentDetail adjustmentDetail = new AdjustmentDetail()
          {
            Amount = purchaseAdjustment.LineAmt.Value,
            BillID = paymentProcessorBill.ExternalBillID
          };
          paymentData2.PaymentDetails.Add(adjustmentDetail);
        }
      }
    }
    return paymentData2;
  }

  private BillData BuildAPBillRequest(PX.Objects.AP.APInvoice invoice, APPaymentProcessorVendor eppVendor)
  {
    return new BillData()
    {
      ExternalVendorID = eppVendor.ExternalVendorID,
      Description = invoice.DocDesc,
      DueDate = (invoice.DueDate ?? invoice.DocDate).Value,
      invoiceDetail = new BillInvoiceDetail()
      {
        InvoiceNumber = invoice.InvoiceNbr ?? invoice.RefNbr,
        InvoiceDate = invoice.DocDate.Value
      },
      BillLineDetails = new List<BillLineDetail>()
      {
        new BillLineDetail()
        {
          Amount = invoice.CuryOrigDocAmt.Value,
          LineDesc = invoice.DocDesc
        }
      }
    };
  }

  private BillData BuildPOBillRequest(PX.Objects.PO.POOrder order, APPaymentProcessorVendor eppVendor)
  {
    return new BillData()
    {
      ExternalVendorID = eppVendor.ExternalVendorID,
      Description = order?.OrderDesc,
      DueDate = ((System.DateTime?) ((System.DateTime?) order?.ExpectedDate ?? order?.OrderDate)).Value,
      invoiceDetail = new BillInvoiceDetail()
      {
        InvoiceNumber = order?.VendorRefNbr ?? order.OrderNbr,
        InvoiceDate = ((System.DateTime?) order?.OrderDate).Value
      },
      BillLineDetails = new List<BillLineDetail>()
      {
        new BillLineDetail()
        {
          Amount = order.CuryOrderTotal.Value,
          LineDesc = order.OrderDesc
        }
      }
    };
  }

  private BillData BuildPrepaymentBillRequest(PX.Objects.AP.APPayment order, APPaymentProcessorVendor eppVendor)
  {
    return new BillData()
    {
      ExternalVendorID = eppVendor.ExternalVendorID,
      Description = order?.DocDesc,
      DueDate = ((System.DateTime?) order?.AdjDate).Value,
      invoiceDetail = new BillInvoiceDetail()
      {
        InvoiceNumber = order?.ExtRefNbr ?? order.RefNbr,
        InvoiceDate = ((System.DateTime?) order?.AdjDate).Value
      },
      BillLineDetails = new List<BillLineDetail>()
      {
        new BillLineDetail()
        {
          Amount = order.CuryUnappliedBal.Value,
          LineDesc = order.DocDesc
        }
      }
    };
  }

  private UserData BuildUserRequest(
    APPaymentProcessorUser extUser,
    Users user,
    string extOrganizationId)
  {
    return new UserData()
    {
      Email = user.Email,
      FirstName = user.FirstName,
      LastName = user.LastName,
      UserName = Guid.NewGuid().ToString(),
      ExternalReferenceId = this.MakeUserExternalReferenceID(extUser),
      ExternalOrganizationId = extOrganizationId
    };
  }

  private VendorData BuildVendorRequest(VendorLocationMaint vendorLocMaint, bool addBankAccount = true)
  {
    PX.Objects.CR.Location current = vendorLocMaint.Location.Current;
    PX.Objects.CR.Address address = (PX.Objects.CR.Address) vendorLocMaint.RemitAddress.Select();
    PX.Objects.CR.Contact contact = (PX.Objects.CR.Contact) vendorLocMaint.RemitContact.Select();
    PX.Objects.AP.VendorR vendorR = PX.Objects.AP.VendorR.PK.Find((PXGraph) vendorLocMaint, current.BAccountID);
    if (!this.IsCountrySupported(address?.CountryID))
      throw new PXException("Payments to vendors located outside the USA are not supported.");
    VendorData vendorData = new VendorData()
    {
      Name = vendorR.AcctName,
      ShortName = vendorR.AcctCD,
      Currency = vendorR.CuryID,
      VendorAccountType = "BUSINESS",
      Address = new AddressData()
      {
        Line1 = address?.AddressLine1,
        Line2 = address?.AddressLine2,
        City = address?.City,
        StateOrProvince = address?.State,
        ZipOrPostalCode = address?.PostalCode,
        Country = address?.CountryID
      },
      Email = contact?.EMail,
      Phone = contact?.Phone1
    };
    if (addBankAccount)
    {
      BankAccountData vendorPaymentDetails = this.GetVendorPaymentDetails(vendorLocMaint, current);
      if (!string.IsNullOrEmpty(vendorPaymentDetails.AccountNumber) && !string.IsNullOrEmpty(vendorPaymentDetails.RoutingNumber))
        vendorData.Payment = new VendorPaymentData()
        {
          BankAccount = vendorPaymentDetails,
          PayeeName = vendorR.AcctName,
          Email = contact?.EMail
        };
    }
    return vendorData;
  }

  private BankAccountData GetVendorPaymentDetails(
    VendorLocationMaint vendorLocMaint,
    PX.Objects.CR.Location location)
  {
    BankAccountData vendorPaymentDetails = new BankAccountData()
    {
      OwnerType = "BUSINESS"
    };
    foreach (PXResult<VendorPaymentMethodDetail> pxResult in vendorLocMaint.PaymentDetails.Select((object) location.BAccountID, (object) location.LocationID, (object) location.VPaymentMethodID))
    {
      VendorPaymentMethodDetail paymentMethodDetail = (VendorPaymentMethodDetail) pxResult;
      switch (paymentMethodDetail.DetailID)
      {
        case "1":
          vendorPaymentDetails.AccountNumber = paymentMethodDetail.DetailValue;
          continue;
        case "2":
          vendorPaymentDetails.NameOnAccount = paymentMethodDetail.DetailValue;
          continue;
        case "3":
          vendorPaymentDetails.RoutingNumber = paymentMethodDetail.DetailValue;
          continue;
        case "5":
          vendorPaymentDetails.Type = !(paymentMethodDetail.DetailValue == "22") ? "SAVINGS" : "CHECKING";
          continue;
        default:
          continue;
      }
    }
    return vendorPaymentDetails;
  }
}
