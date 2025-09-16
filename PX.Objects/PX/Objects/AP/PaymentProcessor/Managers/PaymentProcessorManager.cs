// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.Managers.PaymentProcessorManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Webhooks.DAC;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP.PaymentProcessor.DAC;
using PX.PaymentProcessor.Data;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.AP.PaymentProcessor.Managers;

[PXInternalUseOnly]
public abstract class PaymentProcessorManager
{
  public abstract bool IsCurrencySupported(string currencyId);

  public abstract bool IsCountrySupported(string countryId);

  public abstract Task<ExternalWebhook> SubscribeWebhookAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    WebHook webHook,
    OrganizationUserData organizationUser);

  public abstract Task<ExternalWebhook> UnsubscribeWebhookAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    OrganizationUserData organizationUser);

  public abstract Task<ExternalVendor> CreateVendorAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    PX.Objects.CR.Location location,
    OrganizationUserData organizationUser);

  public abstract Task<ExternalVendor> GetVendorDetailsAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    APPaymentProcessorVendor vendor,
    OrganizationUserData organizationUser);

  public abstract Task<ExternalVendor> UpdateVendorAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    APPaymentProcessorVendor vendor,
    OrganizationUserData organizationUser);

  public abstract Task<ExternalOrganization> CreateOrganizationAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    PX.Objects.GL.DAC.Organization organization);

  public abstract Task OnboardOrganizationAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    PX.Objects.GL.DAC.Organization organization);

  public abstract Task ManageFundingAccountsAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    OrganizationUserData oganizationUser);

  public abstract Task ShowMfaAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    OrganizationUserData organizationUser,
    string initialAction);

  public abstract Task CreateUserAsync(
    APExternalPaymentProcessor extPaymentProcessor,
    PX.Objects.GL.DAC.Organization organization,
    Users user);

  public abstract Task ArchiveUserAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    OrganizationUserData organizationUser);

  public abstract Task RestoreUserAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    OrganizationUserData organizationUser);

  public abstract Task<IEnumerable<ExternalBankAccountUser>> GetUserBankAccountsAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    APPaymentProcessorAccount externalAccount,
    OrganizationUserData organizationUser);

  public abstract Task RefreshFundingAccountsAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    OrganizationUserData organizationUser);

  public abstract Task RefreshFundingAccountUsersAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    OrganizationUserData organizationUser);

  public abstract Task<MfaResponse> GetSessionDetailsAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    OrganizationUserData organizationUser);

  public abstract Task<ExternalBankAccount> GetBankAccountDetailsAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    APPaymentProcessorAccount externalAccount,
    OrganizationUserData organizationUser);

  public abstract Task ArchiveBankAccountAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    APPaymentProcessorAccount account,
    OrganizationUserData organizationUser);

  public abstract Task ArchiveBankAccountUserAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    APPaymentProcessorAccountUser accountUser,
    OrganizationUserData organizationUser);

  public abstract Task<ExternalPayment> ProcessPaymentAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    APPayment payment,
    OrganizationUserData organizationUser);

  public abstract Task<ExternalPayment> CancelPaymentAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    APPayment payment,
    OrganizationUserData organizationUser);

  public abstract Task<ExternalPayment> VoidPaymentAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    APPayment payment,
    OrganizationUserData organizationUser);

  public abstract Task UpdatePaymentDetailsAsync(
    APExternalPaymentProcessor externalPaymentProcessor,
    APPayment payment,
    OrganizationUserData organizationUser);

  public abstract Task ProcessOnboardResponseAsync(
    string responseStr,
    APExternalPaymentProcessor externalPaymentProcessor);

  public abstract Task ProcessFundingAccountsResponseAsync(
    string responseStr,
    APExternalPaymentProcessor externalPaymentProcessor);

  public abstract MfaResponse ProcessMfaResponse(
    string responseStr,
    APExternalPaymentProcessor externalPaymentProcessor);

  protected virtual APExternalPaymentProcessorMaint GetPaymentProcessorGraph()
  {
    return this.GetAPExternalPaymentProcessorGraph((APExternalPaymentProcessor) null);
  }

  protected virtual APExternalPaymentProcessorMaint GetAPExternalPaymentProcessorGraph(
    APExternalPaymentProcessor externalPaymentProcessor,
    int? organizationID = null)
  {
    APExternalPaymentProcessorMaint instance = PXGraph.CreateInstance<APExternalPaymentProcessorMaint>();
    if (externalPaymentProcessor != null && externalPaymentProcessor.ExternalPaymentProcessorID != null)
      instance.ExternalPaymentProcessor.Current = (APExternalPaymentProcessor) PXSelectBase<APExternalPaymentProcessor, PXViewOf<APExternalPaymentProcessor>.BasedOn<SelectFromBase<APExternalPaymentProcessor, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<APExternalPaymentProcessor.externalPaymentProcessorID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) instance, (object) externalPaymentProcessor.ExternalPaymentProcessorID);
    if (organizationID.HasValue)
      instance.PaymentProcessorOrganizations.Current = (APPaymentProcessorOrganization) PXSelectBase<APPaymentProcessorOrganization, PXViewOf<APPaymentProcessorOrganization>.BasedOn<SelectFromBase<APPaymentProcessorOrganization, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorOrganization.externalPaymentProcessorID, Equal<P.AsString>>>>>.And<BqlOperand<APPaymentProcessorOrganization.organizationID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) instance, (object) externalPaymentProcessor.ExternalPaymentProcessorID, (object) organizationID);
    return instance;
  }

  protected virtual T ParseResponse<T>(string responseStr)
  {
    try
    {
      return JsonSerializer.Deserialize<T>(responseStr, (JsonSerializerOptions) null);
    }
    catch (Exception ex)
    {
      throw new PXException("Could not parse the response from the hosted form. Please contact your Acumatica support provider.", ex);
    }
  }
}
