// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.GraphExtensions.APInvoiceEntryEPPExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP.PaymentProcessor.DAC;
using PX.PaymentProcessor.Data;
using System;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor.GraphExtensions;

public class APInvoiceEntryEPPExt : PXGraphExtension<APInvoiceEntry>
{
  public PXSelect<APPaymentProcessorBill, Where<APPaymentProcessorBill.docType, Equal<Current<PX.Objects.AP.APRegister.docType>>, And<APPaymentProcessorBill.refNbr, Equal<Current<PX.Objects.AP.APRegister.refNbr>>>>> PaymentProcessorBills;

  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentProcessor>();

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.AP.APInvoice.payTypeID> e)
  {
    if (!(e.Row is PX.Objects.AP.APInvoice))
      return;
    string newValue = (string) e.NewValue;
    if (!(e.Row is PX.Objects.AP.APInvoice row) || newValue == null)
      return;
    PX.Objects.CA.PaymentMethod paymentMethod = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) this.Base, newValue);
    if (paymentMethod == null || !(paymentMethod?.PaymentType == "EPP"))
      return;
    bool? isActive = APExternalPaymentProcessor.PK.Find((PXGraph) this.Base, paymentMethod?.ExternalPaymentProcessorID).IsActive;
    bool flag = false;
    if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
      throw new PXSetPropertyException((IBqlTable) row, PXMessages.LocalizeNoPrefix("The external payment processor is inactive. To process payments, activate the payment processor on the External Payment Processor (AP205500) form."), PXErrorLevel.Error);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.AP.APInvoice.payAccountID> e)
  {
    if (!(e.Row is PX.Objects.AP.APInvoice))
      return;
    int? newValue = (int?) e.NewValue;
    if (!(e.Row is PX.Objects.AP.APInvoice row) || !newValue.HasValue)
      return;
    string message = (string) null;
    PX.Objects.CA.PaymentMethod paymentMethod = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) this.Base, row?.PayTypeID);
    if (row == null || row.PayTypeID == null || !(paymentMethod?.PaymentType == "EPP"))
      return;
    APExternalPaymentProcessor externalProcessor = APExternalPaymentProcessor.PK.Find((PXGraph) this.Base, paymentMethod?.ExternalPaymentProcessorID);
    PX.Objects.CA.CashAccount cashAccount = PX.Objects.CA.CashAccount.PK.Find((PXGraph) this.Base, newValue);
    if (externalProcessor != null)
    {
      bool? isActive = externalProcessor.IsActive;
      bool flag = false;
      if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
      {
        message = PXMessages.LocalizeNoPrefix("The external payment processor is inactive. To process payments, activate the payment processor on the External Payment Processor (AP205500) form.");
        goto label_13;
      }
    }
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, (int?) cashAccount?.BranchID);
    PX.Objects.GL.DAC.Organization.PK.Find((PXGraph) this.Base, (int?) branch?.OrganizationID);
    APPaymentProcessorAccount processorAccount = this.Base.FindImplementation<ExternalPaymentProcessorHelper<APInvoiceEntry>>().GetExternalProcessorAccount(cashAccount, branch, externalProcessor);
    if (processorAccount == null)
      message = PXMessages.LocalizeFormatNoPrefix("Payments cannot be processed because the {0} cash account is not mapped to an active funding account in the external payment processor. To process payments, make sure that the cash account is mapped on the Funding Accounts tab of the External Payment Processor (AP205500) form.", (object) cashAccount?.CashAccountCD);
    else if (processorAccount?.Status == "P")
      e.Cache.RaiseExceptionHandling<PX.Objects.AP.APInvoice.payAccountID>((object) row, (object) cashAccount?.CashAccountCD, (Exception) new PXSetPropertyException((IBqlTable) row, PXMessages.LocalizeFormatNoPrefix("Payments cannot be processed because the {0} cash account is mapped to a funding account pending activation in the external payment processor. To process payments, make sure that the funding account is active on the Funding Accounts tab of the External Payment Processor (AP205500) form.", (object) cashAccount?.CashAccountCD), PXErrorLevel.Warning));
    else if (processorAccount?.Status != "A" && processorAccount?.Status != "P")
      message = PXMessages.LocalizeFormatNoPrefix("Payments cannot be processed because the cash account {0} is mapped to a funding account that is not active in the external payment processor. To process payments, make sure that the funding account is active on the Funding Accounts tab of the External Payment Processor (AP205500) form.", (object) cashAccount?.CashAccountCD);
label_13:
    if (!string.IsNullOrEmpty(message))
    {
      e.NewValue = (object) cashAccount?.CashAccountCD;
      throw new PXSetPropertyException((IBqlTable) row, message, PXErrorLevel.Error);
    }
  }

  public virtual void AddExternalPaymentProcessorBill(
    APPaymentProcessorOrganization extOrganization,
    ExternalBill response,
    string doctype,
    string refNbr)
  {
    int? organizationId = extOrganization.OrganizationID;
    string paymentProcessorId = extOrganization.ExternalPaymentProcessorID;
    this.PaymentProcessorBills.Insert(new APPaymentProcessorBill()
    {
      OrganizationID = organizationId,
      ExternalPaymentProcessorID = paymentProcessorId,
      DocType = doctype,
      RefNbr = refNbr,
      ExternalBillID = response.Id
    });
    this.Base.Save.Press();
  }

  public class APInvoiceEntryExternalPaymentProcessorHelper : 
    ExternalPaymentProcessorHelper<APInvoiceEntry>
  {
  }
}
