// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.GraphExtensions.APPrintChecksEPPExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP.PaymentProcessor.Common;
using PX.Objects.AP.PaymentProcessor.DAC;
using PX.Objects.AP.PaymentProcessor.Managers;
using PX.PaymentProcessor.Data;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor.GraphExtensions;

public class APPrintChecksEPPExt : PXGraphExtension<APPrintChecks>
{
  public FbqlSelect<
  #nullable disable
  SelectFromBase<
  #nullable enable
  APExternalPaymentProcessor, 
  #nullable disable
  TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<
  #nullable enable
  PX.Objects.CA.PaymentMethod>.On<BqlOperand<PX.Objects.CA.PaymentMethod.externalPaymentProcessorID, IBqlString>.IsEqual<APExternalPaymentProcessor.externalPaymentProcessorID>>>>.Where<BqlOperand<PX.Objects.CA.PaymentMethod.paymentMethodID, IBqlString>.IsEqual<BqlField<PrintChecksFilter.payTypeID, IBqlString>.FromCurrent>>, APExternalPaymentProcessor>.View CurrentExternalPaymentProcessor;
  public PXAction<PrintChecksFilter> verifyFundingAccounts;
  public PXAction<APExternalPaymentProcessor> syncManageFundingAccounts;
  public PXAction<PrintChecksFilter> syncShowMfa;

  [InjectDependency]
  internal Func<string, PaymentProcessorManager> PaymentProcessorManagerProvider { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentProcessor>();

  [InjectDependency]
  internal ICurrentUserInformationProvider CurrentUserInformationProvider { get; set; }

  [PXButton]
  [PXUIField(DisplayName = "Verify Funding Account")]
  public virtual IEnumerable VerifyFundingAccounts(PXAdapter adapter)
  {
    PrintChecksFilter current = this.Base.Filter.Current;
    IEnumerable enumerable = adapter.Get();
    if (current == null)
      return enumerable;
    int? payAccountId = current.PayAccountID;
    if (!payAccountId.HasValue)
      return enumerable;
    PX.Objects.CA.PaymentMethod paymentMethod = this.Base.paymenttype.SelectSingle();
    if (paymentMethod.ExternalPaymentProcessorID == null)
      return enumerable;
    string paymentProcessorId = paymentMethod.ExternalPaymentProcessorID;
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, PX.Objects.CA.CashAccount.PK.Find((PXGraph) this.Base, payAccountId).BranchID);
    int? organizationId1 = branch.OrganizationID;
    if (!organizationId1.HasValue)
      return enumerable;
    ExternalPaymentProcessorHelper<APPrintChecks> implementation = this.Base.FindImplementation<ExternalPaymentProcessorHelper<APPrintChecks>>();
    string externalProcessorId = paymentProcessorId;
    organizationId1 = branch.OrganizationID;
    int organizationId2 = organizationId1.Value;
    APExternalPaymentProcessor extProcessor = implementation.GetExternalProcessorWithOrganization(externalProcessorId, organizationId2).Item1;
    PaymentProcessorManager manager = this.PaymentProcessorManagerProvider(extProcessor.Type);
    PaymentProcessorSessionHelper.SetUserSessionStore(extProcessor);
    PXLongOperation.StartOperation<APPrintChecks>((PXGraphExtension<APPrintChecks>) this, (PXToggleAsyncDelegate) (() =>
    {
      try
      {
        manager.ManageFundingAccountsAsync(extProcessor, new OrganizationUserData()
        {
          OrganizationId = branch.OrganizationID
        }).GetAwaiter().GetResult();
      }
      catch (PXPluginRedirectException ex)
      {
        PXLongOperation.SetCustomInfo((object) ex);
      }
    }));
    PXLongOperation.WaitCompletion(this.Base.UID);
    if (PXLongOperation.GetCustomInfo(this.Base.UID) is PXPluginRedirectException customInfo)
      throw customInfo;
    return enumerable;
  }

  [PXUIField(DisplayName = "", Visible = false)]
  [PXButton]
  public virtual IEnumerable SyncManageFundingAccounts(PXAdapter adapter)
  {
    string responseStr = adapter.CommandArguments;
    PrintChecksFilter current = this.Base.Filter.Current;
    IEnumerable enumerable = adapter.Get();
    if (current == null || !current.PayAccountID.HasValue)
      return enumerable;
    PX.Objects.CA.PaymentMethod paymentMethod = this.Base.paymenttype.SelectSingle();
    if (paymentMethod.ExternalPaymentProcessorID == null)
      return enumerable;
    APExternalPaymentProcessor extProcessor = APExternalPaymentProcessor.PK.Find((PXGraph) this.Base, paymentMethod.ExternalPaymentProcessorID);
    PaymentProcessorManager manager = this.PaymentProcessorManagerProvider(extProcessor.Type);
    PaymentProcessorSessionHelper.SetUserSessionStore(extProcessor);
    PXLongOperation.StartOperation<APPrintChecks>((PXGraphExtension<APPrintChecks>) this, (PXToggleAsyncDelegate) (() => manager.ProcessFundingAccountsResponseAsync(responseStr, extProcessor).GetAwaiter().GetResult()));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "", Visible = false)]
  [PXButton]
  public virtual IEnumerable SyncShowMfa(PXAdapter adapter)
  {
    PrintChecksFilter current = this.Base.Filter.Current;
    string commandArguments = adapter.CommandArguments;
    APExternalPaymentProcessor processor = APExternalPaymentProcessor.PK.Find((PXGraph) this.Base, this.Base.paymenttype.SelectSingle()?.ExternalPaymentProcessorID);
    PaymentProcessorManager processorManager = this.GetPaymentProcessorManager(processor.Type);
    PaymentProcessorSessionHelper.SetUserSessionStore(processor);
    string responseStr = commandArguments;
    APExternalPaymentProcessor externalPaymentProcessor = processor;
    MfaResponse mfaResponse1 = processorManager.ProcessMfaResponse(responseStr, externalPaymentProcessor);
    MfaResponse mfaResponse2 = mfaResponse1;
    current.MfaResult = mfaResponse2;
    this.Base.Actions[mfaResponse1.InitialAction].Press();
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PrintChecksFilter> e)
  {
    if (e.Row == null || this.Base.paymenttype.Current == null)
      return;
    PXUIFieldAttribute.SetVisible<APExternalPaymentProcessor.disclaimerMessage>(this.CurrentExternalPaymentProcessor.Cache, (object) this.CurrentExternalPaymentProcessor.Current, this.Base.paymenttype.Current.PaymentType == "EPP");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PrintChecksFilter.payTypeID> e)
  {
    string newValue = (string) e.NewValue;
    if (!(e.Row is PrintChecksFilter row) || newValue == null)
      return;
    PX.Objects.CA.PaymentMethod paymentMethod = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) this.Base, newValue);
    if (paymentMethod == null || !(paymentMethod?.PaymentType == "EPP"))
      return;
    bool? isActive = APExternalPaymentProcessor.PK.Find((PXGraph) this.Base, paymentMethod?.ExternalPaymentProcessorID).IsActive;
    bool flag = false;
    if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
      throw new PXSetPropertyException((IBqlTable) row, PXMessages.LocalizeNoPrefix("The external payment processor is inactive. To process payments, activate the payment processor on the External Payment Processor (AP205500) form."), PXErrorLevel.Error);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PrintChecksFilter.payAccountID> e)
  {
    int? newValue = (int?) e.NewValue;
    if (!(e.Row is PrintChecksFilter row) || !newValue.HasValue)
      return;
    string message = (string) null;
    PX.Objects.CA.PaymentMethod paymentMethod = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) this.Base, row?.PayTypeID);
    if (row == null || row.PayTypeID == null || !(paymentMethod?.PaymentType == "EPP"))
      return;
    APExternalPaymentProcessor externalProcessor = APExternalPaymentProcessor.PK.Find((PXGraph) this.Base, paymentMethod?.ExternalPaymentProcessorID);
    PX.Objects.CA.CashAccount cashAccount = PX.Objects.CA.CashAccount.PK.Find((PXGraph) this.Base, newValue);
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, (int?) cashAccount?.BranchID);
    PX.Objects.GL.DAC.Organization organization = PX.Objects.GL.DAC.Organization.PK.Find((PXGraph) this.Base, (int?) branch?.OrganizationID);
    APPaymentProcessorAccount processorAccount = this.Base.FindImplementation<ExternalPaymentProcessorHelper<APPrintChecks>>().GetExternalProcessorAccount(cashAccount, branch, externalProcessor);
    APPaymentProcessorUser paymentProcessorUser = APPaymentProcessorUser.PK.Find((PXGraph) this.Base, externalProcessor?.ExternalPaymentProcessorID, (int?) branch?.OrganizationID, this.CurrentUserInformationProvider.GetUserId());
    APPaymentProcessorAccountUser processorAccountUser = this.Base.FindImplementation<ExternalPaymentProcessorHelper<APPrintChecks>>().GetExternalProcessorAccountUser(externalProcessor, processorAccount?.ExternalAccountID, paymentProcessorUser?.ExternalUserID);
    string userName = this.CurrentUserInformationProvider.GetUserName();
    if (paymentProcessorUser == null)
      message = PXMessages.LocalizeFormatNoPrefix("Payments cannot be processed because your user is not created for the {0} company in the external payment processor. To process payments, make sure that the {1} user is added and onboarded on the External Payment Processor (AP205500) form.", (object) organization?.OrganizationCD, (object) userName);
    else if (paymentProcessorUser?.Status != "O")
      message = PXMessages.LocalizeFormatNoPrefix("Payments cannot be processed because your user is not active in the {0} company in the external payment processor. To process payments, make sure that the {1} user is activated on the External Payment Processor (AP205500) form.", (object) organization?.OrganizationCD, (object) userName);
    else if (processorAccount == null)
      message = PXMessages.LocalizeFormatNoPrefix("Payments cannot be processed because the {0} cash account is not mapped to an active funding account in the external payment processor. To process payments, make sure that the cash account is mapped on the Funding Accounts tab of the External Payment Processor (AP205500) form.", (object) cashAccount?.CashAccountCD);
    else if (processorAccount?.Status != "A" && processorAccount?.Status != "P")
      message = PXMessages.LocalizeFormatNoPrefix("Payments cannot be processed because the cash account {0} is mapped to a funding account that is not active in the external payment processor. To process payments, make sure that the funding account is active on the Funding Accounts tab of the External Payment Processor (AP205500) form.", (object) cashAccount?.CashAccountCD);
    else if (processorAccountUser == null)
      message = PXMessages.LocalizeFormatNoPrefix("Payments cannot be processed because your user is not nominated for the funding account in the {0} company in the external payment processor. To process payments, select a cash account that is mapped to a funding account you are allowed to use.", (object) organization?.OrganizationCD, (object) externalProcessor?.ExternalPaymentProcessorID);
    if (!string.IsNullOrEmpty(message))
    {
      e.NewValue = (object) cashAccount?.CashAccountCD;
      throw new PXSetPropertyException((IBqlTable) row, message, PXErrorLevel.Error);
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<PrintChecksFilter> e, PXRowSelected baseHandler)
  {
    if (baseHandler != null)
      baseHandler(e.Cache, e.Args);
    PrintChecksFilter row = e.Row;
    if (row == null)
      return;
    bool flag = !string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<PrintChecksFilter.payAccountID>(e.Cache, (object) row));
    if (flag)
    {
      this.Base.APPaymentList.SetProcessEnabled(false);
      this.Base.APPaymentList.SetProcessAllEnabled(false);
    }
    PX.Objects.CA.PaymentMethod paymentMethod = this.Base.paymenttype.SelectSingle();
    bool isVisible = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentProcessor>();
    this.verifyFundingAccounts.SetEnabled(paymentMethod != null && paymentMethod.PaymentType == "EPP" && paymentMethod.ExternalPaymentProcessorID != null && row.PayAccountID.HasValue && !flag);
    this.verifyFundingAccounts.SetVisible(isVisible);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.AP.APPrintChecks.PrintPayments(System.Collections.Generic.List{PX.Objects.AP.APPayment},PX.Objects.AP.PrintChecksFilter,PX.Objects.CA.PaymentMethod)" />
  /// </summary>
  [PXOverride]
  public void PrintPayments(
    List<PX.Objects.AP.APPayment> list,
    PrintChecksFilter filter,
    PX.Objects.CA.PaymentMethod paymentMethod,
    APPrintChecksEPPExt.PrintPaymentsDel baseInvoke)
  {
    MfaResponse mfaResult = filter.MfaResult;
    filter.MfaResult = (MfaResponse) null;
    bool flag1 = false;
    if (list.Count == 0)
      return;
    if (paymentMethod != null && paymentMethod.UseForAP.GetValueOrDefault())
    {
      APExternalPaymentProcessor paymentProcessor = APExternalPaymentProcessor.PK.Find((PXGraph) this.Base, paymentMethod?.ExternalPaymentProcessorID);
      bool? isActive;
      if (paymentProcessor != null)
      {
        isActive = paymentProcessor.IsActive;
        bool flag2 = false;
        if (isActive.GetValueOrDefault() == flag2 & isActive.HasValue)
          throw new PXException("The external payment processor is inactive. To process payments, activate the payment processor on the External Payment Processor (AP205500) form.");
      }
      if (mfaResult != null && paymentMethod?.PaymentType == "EPP" && paymentMethod != null && paymentMethod.ExternalPaymentProcessorID != null && paymentProcessor != null)
      {
        isActive = paymentProcessor.IsActive;
        if (isActive.GetValueOrDefault())
        {
          this.SetSessionStoreFromMfaResult(paymentProcessor, mfaResult);
          flag1 = true;
          PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, (int?) PX.Objects.CA.CashAccount.PK.Find((PXGraph) this.Base, filter.PayAccountID)?.BranchID);
          APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
          APPaymentEntryEPPExt extension = instance.GetExtension<APPaymentEntryEPPExt>();
          foreach (PX.Objects.AP.APPayment currentItem in list)
          {
            PXProcessing<PX.Objects.AP.APPayment>.SetCurrentItem((object) currentItem);
            try
            {
              PX.Objects.AP.APPayment payment = (PX.Objects.AP.APPayment) instance.Document.Search<PX.Objects.AP.APPayment.refNbr>((object) currentItem.RefNbr, (object) currentItem.DocType);
              if (payment != null)
              {
                extension.ProcessPaymentInEPP(paymentProcessor, payment, (int?) branch?.OrganizationID);
                PXProcessing<PX.Objects.AP.APPayment>.SetProcessed();
              }
            }
            catch (Exception ex)
            {
              PXProcessing<PX.Objects.AP.APPayment>.SetError(ex.Message);
            }
          }
        }
      }
    }
    if (flag1)
      return;
    baseInvoke(list, filter, paymentMethod);
  }

  protected virtual PaymentProcessorManager GetPaymentProcessorManager(string type)
  {
    return this.PaymentProcessorManagerProvider(type);
  }

  private void SetSessionStoreFromMfaResult(
    APExternalPaymentProcessor extProcessor,
    MfaResponse mfaResponse)
  {
    APExternalPaymentProcessor paymentProcessor = extProcessor;
    UserSessionStore userSessionStore = new UserSessionStore();
    ((UserSessionStore) ref userSessionStore).UserSessionIdForOrganization = new Dictionary<string, string>()
    {
      {
        mfaResponse.OrganizationId,
        mfaResponse.SessionId
      }
    };
    UserSessionStore? nullable = new UserSessionStore?(userSessionStore);
    paymentProcessor.UserSessionStore = nullable;
  }

  public class APPrintChecksExternalPaymentProcessorHelper : 
    ExternalPaymentProcessorHelper<APPrintChecks>
  {
  }

  public delegate void PrintPaymentsDel(
    List<PX.Objects.AP.APPayment> list,
    PrintChecksFilter filter,
    PX.Objects.CA.PaymentMethod paymentMethod);
}
