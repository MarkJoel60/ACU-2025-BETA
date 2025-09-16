// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.APPaymentProcessingWithWidget
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
using PX.Objects.CS;
using PX.PaymentProcessor.Data;
using System;
using System.Collections;
using System.Linq;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor;

public class APPaymentProcessingWithWidget : 
  PXFilteredProcessingJoin<PX.Objects.AP.APPayment, PrintChecksFilter, InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.AP.APPayment.vendorID>>>, Where<boolTrue, Equal<boolTrue>>, PX.Data.OrderBy<Asc<PX.Objects.AP.Vendor.acctName, Asc<PX.Objects.AP.APPayment.refNbr>>>>
{
  private const string ProcessAction = "Process";
  private const string ProcessAllAction = "ProcessAll";

  [InjectDependency]
  public Func<string, PaymentProcessorManager> PaymentProcessorManagerProvider { get; set; }

  public APPaymentProcessingWithWidget(PXGraph graph)
    : base(graph)
  {
  }

  public APPaymentProcessingWithWidget(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  protected virtual void ShowWidget(
    APExternalPaymentProcessor extProcessor,
    PX.Objects.GL.Branch branch,
    PrintChecksFilter filter,
    string actionName)
  {
    PaymentProcessorManager manager = this.PaymentProcessorManagerProvider(extProcessor.Type);
    HelperGraph instance = PXGraph.CreateInstance<HelperGraph>();
    OrganizationUserData orgUser = new OrganizationUserData()
    {
      OrganizationId = branch.OrganizationID
    };
    PXLongOperation.StartOperation((PXGraph) instance, (PXToggleAsyncDelegate) (() =>
    {
      try
      {
        manager.ShowMfaAsync(extProcessor, orgUser, actionName).GetAwaiter().GetResult();
      }
      catch (PXPluginRedirectException ex)
      {
        PXLongOperation.SetCustomInfo((object) ex);
      }
    }));
    PXLongOperation.WaitCompletion(instance.UID);
    if (PXLongOperation.GetCustomInfo(instance.UID) is PXPluginRedirectException customInfo)
      throw customInfo;
  }

  protected virtual ExternalBankAccount VerifyFundingAccount(
    APExternalPaymentProcessor extProcessor,
    APPaymentProcessorAccount extenalAccount,
    PX.Objects.GL.Branch branch)
  {
    ExternalBankAccount response = (ExternalBankAccount) null;
    PaymentProcessorManager manager = this.PaymentProcessorManagerProvider(extProcessor.Type);
    HelperGraph instance = PXGraph.CreateInstance<HelperGraph>();
    PXLongOperation.StartOperation((PXGraph) instance, (PXToggleAsyncDelegate) (() =>
    {
      try
      {
        response = manager.GetBankAccountDetailsAsync(extProcessor, extenalAccount, new OrganizationUserData()
        {
          OrganizationId = branch.OrganizationID
        }).GetAwaiter().GetResult();
      }
      catch (Exception ex)
      {
        PXLongOperation.SetCustomInfo((object) ex);
      }
    }));
    PXLongOperation.WaitCompletion(instance.UID);
    return response;
  }

  protected virtual MfaResponse GetMfaSessionDetail(
    APExternalPaymentProcessor extProcessor,
    PX.Objects.GL.Branch branch)
  {
    MfaResponse response = (MfaResponse) null;
    PaymentProcessorManager manager = this.PaymentProcessorManagerProvider(extProcessor.Type);
    HelperGraph instance = PXGraph.CreateInstance<HelperGraph>();
    PXLongOperation.StartOperation((PXGraph) instance, (PXToggleAsyncDelegate) (() =>
    {
      try
      {
        response = manager.GetSessionDetailsAsync(extProcessor, new OrganizationUserData()
        {
          OrganizationId = branch.OrganizationID
        }).GetAwaiter().GetResult();
      }
      catch (Exception ex)
      {
        PXLongOperation.SetCustomInfo((object) ex);
      }
    }));
    PXLongOperation.WaitCompletion(instance.UID);
    return response;
  }

  protected virtual void VerifyFundingAccountUser(
    APExternalPaymentProcessor extProcessor,
    PX.Objects.GL.Branch branch,
    APPaymentProcessorAccount extBankAccount)
  {
    ExternalBankAccountUser userStatus = (ExternalBankAccountUser) null;
    PaymentProcessorManager manager = this.PaymentProcessorManagerProvider(extProcessor.Type);
    HelperGraph instance = PXGraph.CreateInstance<HelperGraph>();
    PXLongOperation.StartOperation((PXGraph) instance, (PXToggleAsyncDelegate) (() =>
    {
      try
      {
        userStatus = manager.GetUserBankAccountsAsync(extProcessor, extBankAccount, new OrganizationUserData()
        {
          OrganizationId = branch.OrganizationID
        }).GetAwaiter().GetResult().ToList<ExternalBankAccountUser>().FirstOrDefault<ExternalBankAccountUser>();
      }
      catch (Exception ex)
      {
        PXLongOperation.SetCustomInfo((object) ex);
      }
    }));
    PXLongOperation.WaitCompletion(instance.UID);
    this.VerifyAccoutUserResponse(userStatus, extProcessor, branch);
  }

  protected virtual void VerifyAccoutUserResponse(
    ExternalBankAccountUser userStatus,
    APExternalPaymentProcessor externalProcessor,
    PX.Objects.GL.Branch branch)
  {
    PX.Objects.GL.DAC.Organization organization = PX.Objects.GL.DAC.Organization.PK.Find(this._Graph, (int?) branch?.OrganizationID);
    if (userStatus == null)
      throw new PXException("Payments cannot be processed because your user is not nominated for the funding account in the {0} company in the external payment processor. To process payments, select a cash account that is mapped to a funding account you are allowed to use.", new object[1]
      {
        (object) organization.OrganizationCD
      });
    if (userStatus.VerificationStatus == 3)
      throw new PXException("Payments cannot be processed because your user was disabled for the funding account in the {0} company in the external payment processor. To process payments, select a cash account that is mapped to a funding account you are allowed to use.", new object[1]
      {
        (object) organization?.OrganizationCD
      });
    if (userStatus.VerificationStatus != 2)
      throw new PXException("Payments cannot be processed because your user is not verified for the funding account in the {0} company in the external payment processor. To process payments, click Verify Funding Account and verify yourself for the funding account.", new object[1]
      {
        (object) organization?.OrganizationCD
      });
  }

  protected virtual void VerifyBankAccoutRespnse(
    APExternalPaymentProcessor externalProcessor,
    PX.Objects.CA.CashAccount cashAccount,
    PX.Objects.GL.Branch branch,
    ExternalBankAccount response)
  {
    PX.Objects.GL.DAC.Organization organization = PX.Objects.GL.DAC.Organization.PK.Find(this._Graph, (int?) branch?.OrganizationID);
    if (response == null || response != null && response.Archived)
      throw new PXException("Payments cannot be processed because the {0} cash account is mapped to a funding account that is not active in the external payment processor. To process payments, select a cash account that is mapped to an active funding account on the Funding Accounts tab of the External Payment Processor (AP205500) form.", new object[1]
      {
        (object) cashAccount.CashAccountCD
      });
    if ((response != null ? (!response.Verified ? 1 : 0) : 1) != 0)
      throw new PXException("You cannot process payments because the Cash Account {0} is mapped to an unverified Funding Account of the {1} company of the {2}. You can access the External Payment Processor form to verify it on the Funding Accounts tab.", new object[3]
      {
        (object) cashAccount.CashAccountCD,
        (object) organization.OrganizationCD,
        (object) externalProcessor.ExternalPaymentProcessorID
      });
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "Process", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected override IEnumerable Process(PXAdapter adapter)
  {
    if (!(this._Filter.SelectSingle() is PrintChecksFilter filter))
      return base.Process(adapter);
    PX.Objects.CA.PaymentMethod paymentMethod = PX.Objects.CA.PaymentMethod.PK.Find(this._Graph, filter.PayTypeID);
    if (paymentMethod == null || paymentMethod.ExternalPaymentProcessorID == null)
      return base.Process(adapter);
    APExternalPaymentProcessor externalProcessor = APExternalPaymentProcessor.PK.Find(this._Graph, paymentMethod.ExternalPaymentProcessorID);
    if (paymentMethod?.PaymentType == "EPP" && paymentMethod != null && paymentMethod.ExternalPaymentProcessorID != null)
      this.PerformVerificationBeforeProcess(filter, externalProcessor, nameof (Process));
    return base.Process(adapter);
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "Process All", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected override IEnumerable ProcessAll(PXAdapter adapter)
  {
    PrintChecksFilter filter = this._Filter.SelectSingle() as PrintChecksFilter;
    PX.Objects.CA.PaymentMethod paymentMethod = PX.Objects.CA.PaymentMethod.PK.Find(this._Graph, filter.PayTypeID);
    APExternalPaymentProcessor externalProcessor = APExternalPaymentProcessor.PK.Find(this._Graph, paymentMethod?.ExternalPaymentProcessorID);
    if (paymentMethod?.PaymentType == "EPP" && paymentMethod != null && paymentMethod.ExternalPaymentProcessorID != null)
      this.PerformVerificationBeforeProcess(filter, externalProcessor, nameof (ProcessAll));
    return base.ProcessAll(adapter);
  }

  protected virtual void PerformVerificationBeforeProcess(
    PrintChecksFilter filter,
    APExternalPaymentProcessor externalProcessor,
    string actionName)
  {
    if (externalProcessor != null)
    {
      bool? isActive = externalProcessor.IsActive;
      bool flag = false;
      if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
        throw new PXException("The external payment processor is inactive. To process payments, activate the payment processor on the External Payment Processor (AP205500) form.");
    }
    PX.Objects.CA.CashAccount cashAccount = PX.Objects.CA.CashAccount.PK.Find(this._Graph, filter.PayAccountID);
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find(this._Graph, (int?) cashAccount?.BranchID);
    APPaymentProcessorAccount processorAccount = this.GetExternalProcessorAccount(cashAccount, branch, externalProcessor);
    if (processorAccount == null)
    {
      PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) filter, PXMessages.LocalizeFormatNoPrefix("Payments cannot be processed because the {0} cash account is mapped to a funding account that is not active in the external payment processor. To process payments, select a cash account that is mapped to an active funding account on the Funding Accounts tab of the External Payment Processor (AP205500) form.", (object) cashAccount?.CashAccountCD), PXErrorLevel.Error);
      this._Filter.Cache.RaiseExceptionHandling<PrintChecksFilter.payAccountID>((object) filter, (object) cashAccount?.CashAccountCD, (Exception) propertyException);
      throw propertyException;
    }
    if (branch == null || processorAccount == null)
      return;
    if (filter.MfaResult == null)
    {
      PaymentProcessorSessionHelper.SetUserSessionStore(externalProcessor);
      ExternalBankAccount response = this.VerifyFundingAccount(externalProcessor, processorAccount, branch);
      this.VerifyBankAccoutRespnse(externalProcessor, cashAccount, branch, response);
      this.VerifyFundingAccountUser(externalProcessor, branch, processorAccount);
      MfaResponse mfaSessionDetail = this.GetMfaSessionDetail(externalProcessor, branch);
      if ((mfaSessionDetail != null ? (mfaSessionDetail.Status > 0 ? 1 : 0) : 1) != 0)
      {
        this.ShowWidget(externalProcessor, branch, filter, actionName);
      }
      else
      {
        filter.MfaResult = mfaSessionDetail;
        this.DoNotCheckPrevOperation = true;
      }
    }
    else
      this.DoNotCheckPrevOperation = true;
  }

  protected virtual APPaymentProcessorAccount GetExternalProcessorAccount(
    PX.Objects.CA.CashAccount cashAccount,
    PX.Objects.GL.Branch branch,
    APExternalPaymentProcessor externalProcessor)
  {
    return (APPaymentProcessorAccount) PXSelectBase<APPaymentProcessorAccount, PXViewOf<APPaymentProcessorAccount>.BasedOn<SelectFromBase<APPaymentProcessorAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorAccount.externalPaymentProcessorID, Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorAccount.organizationID, Equal<P.AsInt>>>>>.And<BqlOperand<APPaymentProcessorAccount.cashAccountID, IBqlInt>.IsEqual<P.AsInt>>>>>.Config>.Select(this._Graph, (object) externalProcessor?.ExternalPaymentProcessorID, (object) (int?) branch?.OrganizationID, (object) (int?) cashAccount?.CashAccountID);
  }
}
