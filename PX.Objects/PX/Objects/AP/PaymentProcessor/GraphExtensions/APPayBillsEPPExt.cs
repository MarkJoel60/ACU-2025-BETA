// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.GraphExtensions.APPayBillsEPPExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP.PaymentProcessor.DAC;
using PX.Objects.AP.PaymentProcessor.Managers;
using System;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor.GraphExtensions;

public class APPayBillsEPPExt : PXGraphExtension<APPayBills>
{
  [InjectDependency]
  internal Func<string, PaymentProcessorManager> PaymentProcessorManagerProvider { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentProcessor>();

  [InjectDependency]
  internal ICurrentUserInformationProvider CurrentUserInformationProvider { get; set; }

  protected virtual void _(PX.Data.Events.FieldVerifying<PayBillsFilter.payTypeID> e)
  {
    if (!(e.Row is PayBillsFilter))
      return;
    string newValue = (string) e.NewValue;
    if (!(e.Row is PayBillsFilter row) || newValue == null)
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
    PX.Data.Events.FieldVerifying<PayBillsFilter.payAccountID> e)
  {
    if (!(e.Row is PayBillsFilter))
      return;
    int? newValue = (int?) e.NewValue;
    if (!(e.Row is PayBillsFilter row) || !newValue.HasValue)
      return;
    string message = (string) null;
    PX.Objects.CA.PaymentMethod paymentMethod = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) this.Base, row?.PayTypeID);
    if (row == null || row.PayTypeID == null || !(paymentMethod?.PaymentType == "EPP"))
      return;
    APExternalPaymentProcessor externalProcessor = APExternalPaymentProcessor.PK.Find((PXGraph) this.Base, paymentMethod?.ExternalPaymentProcessorID);
    PX.Objects.CA.CashAccount cashAccount = PX.Objects.CA.CashAccount.PK.Find((PXGraph) this.Base, newValue);
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, (int?) cashAccount?.BranchID);
    PX.Objects.GL.DAC.Organization.PK.Find((PXGraph) this.Base, (int?) branch?.OrganizationID);
    APPaymentProcessorAccount processorAccount = this.Base.FindImplementation<ExternalPaymentProcessorHelper<APPayBills>>().GetExternalProcessorAccount(cashAccount, branch, externalProcessor);
    if (processorAccount == null)
      message = PXMessages.LocalizeFormatNoPrefix("Payments cannot be processed because the {0} cash account is not mapped to an active funding account in the external payment processor. To process payments, make sure that the cash account is mapped on the Funding Accounts tab of the External Payment Processor (AP205500) form.", (object) cashAccount?.CashAccountCD);
    else if (processorAccount?.Status == "P")
      e.Cache.RaiseExceptionHandling<PayBillsFilter.payAccountID>((object) row, (object) cashAccount?.CashAccountCD, (Exception) new PXSetPropertyException((IBqlTable) row, PXMessages.LocalizeFormatNoPrefix("Payments cannot be processed because the {0} cash account is mapped to a funding account pending activation in the external payment processor. To process payments, make sure that the funding account is active on the Funding Accounts tab of the External Payment Processor (AP205500) form.", (object) cashAccount?.CashAccountCD), PXErrorLevel.Warning));
    else if (processorAccount?.Status != "A" && processorAccount?.Status != "P")
      message = PXMessages.LocalizeFormatNoPrefix("Payments cannot be processed because the cash account {0} is mapped to a funding account that is not active in the external payment processor. To process payments, make sure that the funding account is active on the Funding Accounts tab of the External Payment Processor (AP205500) form.", (object) cashAccount?.CashAccountCD);
    if (!string.IsNullOrEmpty(message))
    {
      e.NewValue = (object) cashAccount?.CashAccountCD;
      throw new PXSetPropertyException((IBqlTable) row, message, PXErrorLevel.Error);
    }
  }

  public class APPayBillsExternalPaymentProcessorHelper : ExternalPaymentProcessorHelper<APPayBills>
  {
  }
}
