// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.GraphExtensions.APPaymentEntryEPPExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AP.PaymentProcessor.DAC;
using PX.Objects.AP.PaymentProcessor.Managers;
using PX.Objects.PO;
using PX.PaymentProcessor.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor.GraphExtensions;

public class APPaymentEntryEPPExt : PXGraphExtension<APPaymentEntry>
{
  public PXSelectOrderBy<APPaymentProcessorPaymentTran, PX.Data.OrderBy<Desc<APPaymentProcessorPaymentTran.tranNbr>>> ExternalPaymentTrans;
  public PXAction<PX.Objects.AP.APPayment> validatePayment;

  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentProcessor>();

  [InjectDependency]
  internal Func<string, PaymentProcessorManager> PaymentProcessorManagerProvider { get; set; }

  [InjectDependency]
  internal ICurrentUserInformationProvider CurrentUserInformationProvider { get; set; }

  public IEnumerable externalPaymentTrans()
  {
    APPaymentEntryEPPExt paymentEntryEppExt = this;
    PX.Objects.AP.APPayment current = paymentEntryEppExt.Base.CurrentDocument.Current;
    if (current != null)
    {
      APPaymentEntry graph = paymentEntryEppExt.Base;
      object[] objArray = new object[2]
      {
        (object) current.DocType,
        (object) current.RefNbr
      };
      foreach (PXResult<APPaymentProcessorPaymentTran> pxResult in PXSelectBase<APPaymentProcessorPaymentTran, PXViewOf<APPaymentProcessorPaymentTran>.BasedOn<SelectFromBase<APPaymentProcessorPaymentTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorPaymentTran.docType, Equal<P.AsString>>>>>.And<BqlOperand<APPaymentProcessorPaymentTran.refNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) graph, objArray))
        yield return (object) (APPaymentProcessorPaymentTran) pxResult;
    }
    else
      Array.Empty<APPaymentProcessorPaymentTran>();
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.AP.APPayment.paymentMethodID> e)
  {
    if (!(e.Row is PX.Objects.AP.APPayment))
      return;
    string newValue = (string) e.NewValue;
    if (!(e.Row is PX.Objects.AP.APPayment row) || newValue == null)
      return;
    PX.Objects.CA.PaymentMethod paymentMethod = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) this.Base, newValue);
    if (paymentMethod == null || !(paymentMethod?.PaymentType == "EPP"))
      return;
    bool? isActive = APExternalPaymentProcessor.PK.Find((PXGraph) this.Base, paymentMethod?.ExternalPaymentProcessorID).IsActive;
    bool flag = false;
    if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
      throw new PXSetPropertyException((IBqlTable) row, PXMessages.LocalizeNoPrefix("The external payment processor is inactive. To process payments, activate the payment processor on the External Payment Processor (AP205500) form."), PXErrorLevel.Error);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.AP.APPayment.cashAccountID> e)
  {
    int? newValue = (int?) e.NewValue;
    if (!(e.Row is PX.Objects.AP.APPayment row) || !newValue.HasValue)
      return;
    string message = (string) null;
    PX.Objects.CA.PaymentMethod paymentMethod = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) this.Base, row?.PaymentMethodID);
    if (row == null || row.PaymentMethodID == null || !(paymentMethod?.PaymentType == "EPP"))
      return;
    APExternalPaymentProcessor externalProcessor = APExternalPaymentProcessor.PK.Find((PXGraph) this.Base, paymentMethod?.ExternalPaymentProcessorID);
    PX.Objects.CA.CashAccount cashAccount = PX.Objects.CA.CashAccount.PK.Find((PXGraph) this.Base, newValue);
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, (int?) cashAccount?.BranchID);
    APPaymentProcessorAccount processorAccount = this.Base.FindImplementation<ExternalPaymentProcessorHelper<APPaymentEntry>>().GetExternalProcessorAccount(cashAccount, branch, externalProcessor);
    if (processorAccount == null)
      message = PXMessages.LocalizeFormatNoPrefix("Payments cannot be processed because the {0} cash account is not mapped to an active funding account in the external payment processor. To process payments, make sure that the cash account is mapped on the Funding Accounts tab of the External Payment Processor (AP205500) form.", (object) cashAccount?.CashAccountCD);
    else if (processorAccount?.Status != "A" && processorAccount?.Status != "P")
      message = PXMessages.LocalizeFormatNoPrefix("Payments cannot be processed because the cash account {0} is mapped to a funding account that is not active in the external payment processor. To process payments, make sure that the funding account is active on the Funding Accounts tab of the External Payment Processor (AP205500) form.", (object) cashAccount?.CashAccountCD);
    else if (processorAccount?.Status == "P")
      e.Cache.RaiseExceptionHandling<PX.Objects.AP.APPayment.cashAccountID>((object) row, (object) cashAccount?.CashAccountCD, (Exception) new PXSetPropertyException((IBqlTable) row, PXMessages.LocalizeFormatNoPrefix("Payments cannot be processed because the {0} cash account is mapped to a funding account pending activation in the external payment processor. To process payments, make sure that the funding account is active on the Funding Accounts tab of the External Payment Processor (AP205500) form.", (object) cashAccount?.CashAccountCD), PXErrorLevel.Warning));
    if (!string.IsNullOrEmpty(message))
    {
      e.NewValue = (object) cashAccount?.CashAccountCD;
      throw new PXSetPropertyException((IBqlTable) row, message, PXErrorLevel.Error);
    }
  }

  protected void _(PX.Data.Events.FieldUpdated<PX.Objects.AP.APPayment.paymentMethodID> e, PXFieldUpdated baseHandler)
  {
    if (baseHandler != null)
      baseHandler(e.Cache, e.Args);
    PX.Objects.AP.APPayment row = (PX.Objects.AP.APPayment) e.Row;
    if (row == null)
      return;
    PX.Objects.CA.PaymentMethod current = this.Base.paymenttype.Current;
    if (current == null)
      return;
    APExternalPaymentProcessor.PK.Find((PXGraph) this.Base, current.ExternalPaymentProcessorID);
    if (current.PaymentType == "EPP" && current.ExternalPaymentProcessorID != null)
    {
      if (row.ExternalPaymentStatus == null)
      {
        e.Cache.SetValue<PX.Objects.AP.APRegister.pendingProcessing>((object) row, (object) (row?.DocType != "VCK"));
        PXEntityEventBase<PX.Objects.AP.APPayment>.Container<PX.Objects.AP.APPayment.Events>.Select((Expression<Func<PX.Objects.AP.APPayment.Events, PXEntityEvent<PX.Objects.AP.APPayment>>>) (g => g.ProcessDocument)).FireOn((PXGraph) this.Base, row);
        e.Cache.SetValue<PX.Objects.AP.APPayment.clearDate>((object) row, (object) null);
        e.Cache.SetValue<PX.Objects.AP.APPayment.cleared>((object) row, (object) false);
        e.Cache.SetValue<PX.Objects.AP.APPayment.extRefNbr>((object) row, (object) null);
      }
      row.Printed = new bool?(false);
    }
    else
    {
      e.Cache.SetValue<PX.Objects.AP.APRegister.pendingProcessing>((object) row, (object) false);
      PXEntityEventBase<PX.Objects.AP.APPayment>.Container<PX.Objects.AP.APPayment.Events>.Select((Expression<Func<PX.Objects.AP.APPayment.Events, PXEntityEvent<PX.Objects.AP.APPayment>>>) (g => g.ProcessDocument)).FireOn((PXGraph) this.Base, row);
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AP.APPayment> e, PXRowSelected baseHandler)
  {
    if (baseHandler != null)
      baseHandler(e.Cache, e.Args);
    PX.Objects.AP.APPayment row = e.Row;
    if (row == null)
      return;
    bool flag1 = row?.DocType == "VCK";
    bool flag2 = this.Base.paymenttype.Current?.PaymentType == "EPP";
    bool flag3 = flag2 && !string.IsNullOrEmpty(row?.ExternalPaymentStatus);
    this.ExternalPaymentTrans.Cache.AllowSelect = flag2 && !flag1;
    this.validatePayment.SetVisible(flag2 && !flag1);
    this.validatePayment.SetEnabled(flag3 && (row == null || !row.Voided.GetValueOrDefault()));
    if (row != null && row.ExternalPaymentID != null && EnumerableExtensions.IsIn<string>(row.Status, "W", "B"))
    {
      e.Cache.AllowUpdate = false;
      e.Cache.AllowDelete = false;
      this.Base.Adjustments.Cache.AllowInsert = false;
      this.Base.Adjustments.Cache.AllowDelete = false;
      this.Base.Adjustments.Cache.AllowUpdate = false;
    }
    if (row != null && row.ExternalPaymentID != null)
    {
      bool flag4 = PaymentStatus.CanVoid(row.ExternalPaymentStatus) || PaymentStatus.CanCancel(row.ExternalPaymentStatus);
      PXAction<PX.Objects.AP.APPayment> voidCheck = this.Base.voidCheck;
      bool? nullable = row.ExternalPaymentCanceled;
      int num1;
      if (!nullable.GetValueOrDefault())
      {
        nullable = row.ExternalPaymentVoided;
        num1 = !nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num1 = 0;
      int num2 = flag4 ? 1 : 0;
      int num3 = num1 & num2;
      voidCheck.SetEnabled(num3 != 0);
      if (PaymentStatus.RequestVoiding(row.ExternalPaymentStatus))
      {
        nullable = row.ExternalPaymentVoided;
        if (!nullable.GetValueOrDefault())
          PXUIFieldAttribute.SetWarning<PX.Objects.AP.APPayment.externalPaymentStatus>(e.Cache, (object) e.Row, "Disbursement has failed in the external payment processor. Please void this payment.");
      }
    }
    if (!flag2)
      return;
    PXUIFieldAttribute.SetRequired<PX.Objects.AP.APPayment.extRefNbr>(e.Cache, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APPayment.extRefNbr>(e.Cache, (object) row, false);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.AP.APPayment> e, PXRowPersisting baseHandler)
  {
    if (baseHandler != null)
      baseHandler(e.Cache, e.Args);
    PX.Objects.AP.APPayment row = e.Row;
    if (row == null)
      return;
    PX.Objects.CA.PaymentMethod current = this.Base.paymenttype.Current;
    if (current?.PaymentType == "EPP" && current != null && current.ExternalPaymentProcessorID != null && EnumerableExtensions.IsNotIn<string>(row.DocType, "CHK", "PPM", "VCK"))
      throw new PXRowPersistingException(typeof (PX.Objects.AP.APPayment.docType).Name, (object) row.DocType, "External Payment Processing is not available for {0} Documents", new object[1]
      {
        (object) APDocType.GetDisplayName(row.DocType)
      });
  }

  [PXUIField(DisplayName = "Synchronize Payment", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = false)]
  [PXButton]
  public virtual IEnumerable ValidatePayment(PXAdapter adapter)
  {
    PX.Objects.AP.APPayment current = this.Base.CurrentDocument.Current;
    if (current != null)
      this.UpdatePaymentDetails(current);
    return adapter.Get();
  }

  public virtual void UpdatePaymentDetails(PX.Objects.AP.APPayment payment)
  {
    this.Base.CurrentDocument.Current = payment;
    if (!payment.CashAccountID.HasValue)
      throw new PXException("{0} cannot be empty.", new object[1]
      {
        (object) "CashAccount"
      });
    if (payment.ExternalPaymentID != null)
    {
      APPaymentProcessorOrganization organizationById = this.GetExternalOrganizationById(payment?.ExternalOrganizationID);
      APExternalPaymentProcessor externalProcessor = APExternalPaymentProcessor.PK.Find((PXGraph) this.Base, organizationById?.ExternalPaymentProcessorID);
      if (externalProcessor == null)
        return;
      PaymentProcessorManager manager = this.GetPaymentProcessorManager(externalProcessor.Type);
      OrganizationUserData organizationUser = this.GetOrganizationUserData(organizationById.OrganizationID, new Guid?());
      PXLongOperation.StartOperation<APPaymentEntry>((PXGraphExtension<APPaymentEntry>) this, (PXToggleAsyncDelegate) (() => manager.UpdatePaymentDetailsAsync(externalProcessor, payment, organizationUser).GetAwaiter().GetResult()));
    }
    else
    {
      if (payment.PaymentMethodID == null)
        return;
      PX.Objects.CA.PaymentMethod paymentMethod = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) this.Base, payment.PaymentMethodID);
      if (!(paymentMethod.PaymentType == "EPP"))
        return;
      APPaymentProcessorPaymentTran transactionForPayment = this.GetLatestTransactionForPayment(paymentMethod.ExternalPaymentProcessorID, payment);
      if (transactionForPayment != null && transactionForPayment.TransactionState == "O")
      {
        APExternalPaymentProcessor externalProcessor = APExternalPaymentProcessor.PK.Find((PXGraph) this.Base, paymentMethod.ExternalPaymentProcessorID);
        PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, (int?) PX.Objects.CA.CashAccount.PK.Find((PXGraph) this.Base, payment.CashAccountID)?.BranchID);
        PaymentProcessorManager manager = this.GetPaymentProcessorManager(externalProcessor.Type);
        OrganizationUserData organizationUser = this.GetOrganizationUserData(branch.OrganizationID, new Guid?());
        PXLongOperation.StartOperation<APPaymentEntry>((PXGraphExtension<APPaymentEntry>) this, (PXToggleAsyncDelegate) (() => manager.UpdatePaymentDetailsAsync(externalProcessor, payment, organizationUser).GetAwaiter().GetResult()));
      }
      else
      {
        payment.ExternalPaymentStatus = (string) null;
        this.Base.Document.Update(payment);
        this.Base.Save.Press();
      }
    }
  }

  public virtual void CancelPaymentInEPP(PX.Objects.AP.APPayment payment)
  {
    this.Base.CurrentDocument.Current = payment;
    if (payment?.ExternalPaymentID == null)
      return;
    APPaymentProcessorOrganization organizationById = this.GetExternalOrganizationById(payment?.ExternalOrganizationID);
    APExternalPaymentProcessor externalProcessor = APExternalPaymentProcessor.PK.Find((PXGraph) this.Base, organizationById?.ExternalPaymentProcessorID);
    if (externalProcessor == null)
      return;
    PaymentProcessorManager manager = this.GetPaymentProcessorManager(externalProcessor.Type);
    OrganizationUserData organizationUser = this.GetOrganizationUserData(organizationById.OrganizationID, new Guid?());
    PXLongOperation.StartOperation<APPaymentEntry>((PXGraphExtension<APPaymentEntry>) this, (PXToggleAsyncDelegate) (() => manager.CancelPaymentAsync(externalProcessor, payment, organizationUser).GetAwaiter().GetResult()));
  }

  public virtual void VoidPaymentInEPP(PX.Objects.AP.APPayment payment, Guid? userID = null)
  {
    this.Base.CurrentDocument.Current = payment;
    if (payment?.ExternalPaymentID == null)
      return;
    APPaymentProcessorOrganization organizationById = this.GetExternalOrganizationById(payment?.ExternalOrganizationID);
    APExternalPaymentProcessor externalProcessor = APExternalPaymentProcessor.PK.Find((PXGraph) this.Base, organizationById?.ExternalPaymentProcessorID);
    if (externalProcessor == null)
      return;
    PaymentProcessorManager manager = this.GetPaymentProcessorManager(externalProcessor.Type);
    OrganizationUserData organizationUser = this.GetOrganizationUserData((int?) organizationById?.OrganizationID, userID);
    PXLongOperation.StartOperation<APPaymentEntry>((PXGraphExtension<APPaymentEntry>) this, (PXToggleAsyncDelegate) (() => manager.VoidPaymentAsync(externalProcessor, payment, organizationUser).GetAwaiter().GetResult()));
  }

  [PXOverride]
  public virtual IEnumerable VoidCheck(
    PXAdapter adapter,
    APPaymentEntryEPPExt.VoidCheckDelegate baseMethod)
  {
    if (baseMethod == null || this.Base.Document.Current == null)
      return adapter.Get();
    PX.Objects.AP.APPayment current1 = this.Base.Document.Current;
    PX.Objects.CA.PaymentMethod current2 = this.Base.paymenttype.Current;
    APExternalPaymentProcessor paymentProcessor = APExternalPaymentProcessor.PK.Find((PXGraph) this.Base, current2?.ExternalPaymentProcessorID);
    if (!(current2?.PaymentType == "EPP") || current2 == null || current2.ExternalPaymentProcessorID == null || paymentProcessor == null || !paymentProcessor.IsActive.GetValueOrDefault() || PaymentStatus.IsVoided(current1.ExternalPaymentStatus))
      return baseMethod(adapter);
    bool? nullable = current1.ExternalPaymentVoided;
    bool flag1 = false;
    if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
    {
      nullable = current1.ExternalPaymentCanceled;
      bool flag2 = false;
      if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
      {
        if (PaymentStatus.CanCancel(current1.ExternalPaymentStatus))
          this.CancelPaymentInEPP(current1);
        else if (PaymentStatus.CanVoid(current1.ExternalPaymentStatus))
          this.VoidPaymentInEPP(current1);
        return adapter.Get();
      }
    }
    return baseMethod(adapter);
  }

  public virtual void ProcessPaymentInEPP(
    APExternalPaymentProcessor processor,
    PX.Objects.AP.APPayment payment,
    int? organizationId,
    Guid? userId = null)
  {
    if (payment == null)
      return;
    try
    {
      PaymentProcessorManager processorManager = this.GetPaymentProcessorManager(processor.Type);
      OrganizationUserData organizationUserData = this.GetOrganizationUserData(organizationId, userId);
      APExternalPaymentProcessor externalPaymentProcessor = processor;
      PX.Objects.AP.APPayment payment1 = payment;
      OrganizationUserData organizationUser = organizationUserData;
      processorManager.ProcessPaymentAsync(externalPaymentProcessor, payment1, organizationUser).GetAwaiter().GetResult();
    }
    catch (Exception ex)
    {
      throw new PXException(ex.Message.ToString());
    }
  }

  public virtual APPaymentProcessorPaymentTran UpdatePayment(
    APPaymentProcessorOrganization extOrganization,
    PX.Objects.AP.APPayment apPayment,
    ExternalPayment paymentInfo,
    APPaymentProcessorPaymentTran? tran = null)
  {
    apPayment.ExternalPaymentStatus = paymentInfo?.SingleStatus;
    apPayment.ExternalPaymentDisbursementType = paymentInfo?.DisbursementType;
    apPayment.ExternalPaymentBatchNbr = paymentInfo?.DisbursementInfo?.BatchNumber ?? apPayment.ExternalPaymentBatchNbr;
    apPayment.ExternalPaymentTraceNbr = paymentInfo?.DisbursementInfo?.TraceNumber ?? apPayment.ExternalPaymentTraceNbr;
    apPayment.ExternalPaymentSentDate = (System.DateTime?) paymentInfo?.DisbursementInfo?.SentDate ?? apPayment.ExternalPaymentSentDate;
    apPayment.ExternalPaymentUpdateTime = (System.DateTime?) paymentInfo?.UpdatedTime ?? apPayment.ExternalPaymentUpdateTime;
    apPayment.ExternalPaymentCheckNbr = paymentInfo?.DisbursementInfo?.CheckNumber ?? apPayment.ExternalPaymentCheckNbr;
    apPayment.ExternalPaymentCardNbr = paymentInfo?.DisbursementInfo?.CardNumber ?? apPayment.ExternalPaymentCardNbr;
    if (string.IsNullOrEmpty(apPayment.ExtRefNbr) && !string.IsNullOrEmpty(paymentInfo?.ConfirmationNumber))
      apPayment.ExtRefNbr = paymentInfo?.ConfirmationNumber;
    APPaymentProcessorPaymentTran paymentTran = this.ExternalPaymentTrans.Update(this.ConvertPaymentResponse(extOrganization, apPayment, paymentInfo, tran));
    this.ClearPayment(apPayment, paymentTran);
    this.Base.Document.Current = (PX.Objects.AP.APPayment) this.Base.Document.Search<PX.Objects.AP.APPayment.refNbr>((object) apPayment.RefNbr, (object) apPayment.DocType);
    this.Base.Document.Update(apPayment);
    this.Base.Save.Press();
    return paymentTran;
  }

  public virtual void UpdatePaymentForCreatePayment(
    APPaymentProcessorOrganization extOrganization,
    PX.Objects.AP.APPayment payment,
    APPaymentProcessorPaymentTran tran,
    ExternalPayment paymentInfo)
  {
    payment.ExternalPaymentID = paymentInfo.Id;
    payment.ExternalOrganizationID = extOrganization.ExternalOrganizationID;
    this.UpdatePayment(extOrganization, payment, paymentInfo, tran);
  }

  public virtual void UpdatePaymentForCancelRequest(
    APPaymentProcessorOrganization extOrganization,
    PX.Objects.AP.APPayment payment,
    ExternalPayment paymentInfo)
  {
    payment.ExternalPaymentCanceled = new bool?(true);
    this.UpdatePayment(extOrganization, payment, paymentInfo);
  }

  public virtual void UpdatePaymentForVoidRequest(
    APPaymentProcessorOrganization extOrganization,
    PX.Objects.AP.APPayment payment,
    ExternalPayment paymentInfo)
  {
    payment.ExternalPaymentVoided = paymentInfo.IsVoided;
    this.UpdatePayment(extOrganization, payment, paymentInfo);
  }

  public virtual void UpdatePaymentForPaymentDetail(
    APPaymentProcessorOrganization extOrganization,
    PX.Objects.AP.APPayment payment,
    ExternalPayment response)
  {
    APPaymentProcessorPaymentTran transactionForPayment = this.GetLatestTransactionForPayment(extOrganization.ExternalPaymentProcessorID, payment);
    if (response != null)
    {
      if (payment.ExternalPaymentID == null)
        payment.ExternalPaymentID = response.Id;
      if (payment.ExternalOrganizationID == null)
        payment.ExternalOrganizationID = extOrganization.ExternalOrganizationID;
      this.CheckAndChangePaymentStatus(this.Base.Document.Current, transactionForPayment == null || !(transactionForPayment.TransactionState == "O") ? this.UpdatePayment(extOrganization, payment, response) : this.UpdatePayment(extOrganization, payment, response, transactionForPayment));
    }
    else
    {
      transactionForPayment.TransactionState = "E";
      this.FinalizeUnsuccessfulTransaction(payment, transactionForPayment, "The payment does not exist in the external payment processor.");
    }
  }

  public virtual APPaymentProcessorPaymentTran? StartNewTransaction(
    APExternalPaymentProcessor externalPaymentProcessor,
    PX.Objects.AP.APPayment apPayment)
  {
    if (apPayment == null || externalPaymentProcessor == null)
      return (APPaymentProcessorPaymentTran) null;
    APPaymentProcessorPaymentTran processorPaymentTran = new APPaymentProcessorPaymentTran();
    processorPaymentTran.TransactionState = "O";
    processorPaymentTran.ExternalPaymentProcessorID = externalPaymentProcessor.ExternalPaymentProcessorID;
    processorPaymentTran.RefNbr = apPayment.RefNbr;
    processorPaymentTran.DocType = apPayment.DocType;
    Guid? noteId = apPayment.NoteID;
    ref Guid? local = ref noteId;
    processorPaymentTran.TransactionNumber = local.HasValue ? local.GetValueOrDefault().ToString("N") : (string) null;
    APPaymentProcessorPaymentTran paymentTran = this.ExternalPaymentTrans.Insert(processorPaymentTran);
    apPayment.ExternalPaymentStatus = "UNDEFINED";
    this.Base.Document.Current = (PX.Objects.AP.APPayment) this.Base.Document.Search<PX.Objects.AP.APPayment.refNbr>((object) apPayment.RefNbr, (object) apPayment.DocType);
    this.Base.Document.Update(apPayment);
    this.Base.Save.Press();
    this.CheckAndChangePaymentStatus(this.Base.Document.Current, paymentTran);
    return paymentTran;
  }

  public virtual void FinalizeUnsuccessfulTransaction(
    PX.Objects.AP.APPayment apPayment,
    APPaymentProcessorPaymentTran paymentTran,
    string? comment)
  {
    apPayment.ExternalPaymentStatus = !(paymentTran.TransactionState == "E") || apPayment.ExternalPaymentID != null ? "UNDEFINED" : (string) null;
    paymentTran.ExternalComment = comment;
    this.ExternalPaymentTrans.Update(paymentTran);
    this.Base.Document.Current = (PX.Objects.AP.APPayment) this.Base.Document.Search<PX.Objects.AP.APPayment.refNbr>((object) apPayment.RefNbr, (object) apPayment.DocType);
    this.Base.Document.Update(apPayment);
    this.Base.Save.Press();
    this.CheckAndChangePaymentStatus(this.Base.Document.Current, paymentTran);
  }

  public virtual void UpdatePaymentFromWebhook(
    PX.Objects.AP.APPayment apPayment,
    APPaymentProcessorPaymentTran paymentTran)
  {
    this.ExternalPaymentTrans.Insert(paymentTran);
    apPayment.ExternalPaymentStatus = paymentTran?.Status ?? apPayment.ExternalPaymentStatus;
    apPayment.ExternalPaymentUpdateTime = (System.DateTime?) paymentTran?.ExternalUpdatedDateTime ?? apPayment.ExternalPaymentUpdateTime;
    apPayment.ExternalPaymentDisbursementType = paymentTran?.DisbursementType ?? apPayment.ExternalPaymentDisbursementType;
    this.ClearPayment(apPayment, paymentTran);
    this.Base.Document.Current = (PX.Objects.AP.APPayment) this.Base.Document.Search<PX.Objects.AP.APPayment.refNbr>((object) apPayment.RefNbr, (object) apPayment.DocType);
    this.Base.Document.Update(apPayment);
    this.Base.Save.Press();
    this.CheckAndChangePaymentStatus(this.Base.Document.Current, paymentTran);
  }

  public virtual void ClearPayment(PX.Objects.AP.APPayment apPayment, APPaymentProcessorPaymentTran? paymentTran)
  {
    if (PaymentStatus.FundsMoved(paymentTran?.Status))
    {
      bool? cleared = apPayment.Cleared;
      bool flag = false;
      if (!(cleared.GetValueOrDefault() == flag & cleared.HasValue))
        return;
      apPayment.Cleared = new bool?(true);
      apPayment.ClearDate = (System.DateTime?) ((System.DateTime?) paymentTran?.ProcessDate ?? apPayment?.AdjDate);
    }
    else
    {
      apPayment.Cleared = new bool?(false);
      apPayment.ClearDate = new System.DateTime?();
    }
  }

  public virtual void CheckAndChangePaymentStatus(
    PX.Objects.AP.APPayment apPayment,
    APPaymentProcessorPaymentTran? paymentTran)
  {
    if (paymentTran == null || apPayment == null)
      return;
    bool flag1 = PaymentStatus.FundsMoved(paymentTran.Status);
    bool flag2 = PaymentStatus.IsDisbursed(paymentTran.Status);
    bool flag3 = PaymentStatus.IsVoided(paymentTran.Status);
    bool flag4 = PaymentStatus.IsCanceled(paymentTran.Status);
    PaymentStatus.RequestVoiding(paymentTran.Status);
    bool? nullable = apPayment.Released;
    bool flag5 = false;
    if (nullable.GetValueOrDefault() == flag5 & nullable.HasValue)
    {
      if (flag1 | flag2)
      {
        nullable = apPayment.PendingProcessing;
        if (nullable.GetValueOrDefault())
        {
          this.Base.Document.Update(apPayment);
          PXEntityEventBase<PX.Objects.AP.APPayment>.Container<PX.Objects.AP.APPayment.Events>.Select((Expression<Func<PX.Objects.AP.APPayment.Events, PXEntityEvent<PX.Objects.AP.APPayment>>>) (g => g.ProcessDocument)).FireOn((PXGraph) this.Base, apPayment);
          this.Base.Document.Cache.Update((object) apPayment);
          this.Base.Save.Press();
        }
        this.ReleasePayment(apPayment);
      }
      else if (flag3 | flag4)
      {
        apPayment.ExternalPaymentVoided = new bool?(flag3);
        apPayment.ExternalPaymentCanceled = new bool?(flag4);
        this.VoidUnreleasedPayment(apPayment);
      }
    }
    nullable = apPayment.Released;
    if (!(nullable.GetValueOrDefault() & flag3))
      return;
    this.VoidPayment(apPayment);
  }

  protected virtual void VoidUnreleasedPayment(PX.Objects.AP.APPayment apPayment)
  {
    apPayment.Voided = new bool?(true);
    apPayment.OpenDoc = new bool?(false);
    apPayment.Hold = new bool?(false);
    apPayment.CuryDocBal = new Decimal?(0M);
    apPayment.DocBal = new Decimal?(0M);
    foreach (PXResult<APAdjust> pxResult in this.Base.Adjustments_Raw.Select())
    {
      APAdjust apAdjust = (APAdjust) pxResult;
      apAdjust.Voided = new bool?(true);
      this.Base.Adjustments_Raw.Update(apAdjust);
    }
    this.Base.Document.Update(apPayment);
    PXEntityEventBase<PX.Objects.AP.APPayment>.Container<PX.Objects.AP.APPayment.Events>.Select((Expression<Func<PX.Objects.AP.APPayment.Events, PXEntityEvent<PX.Objects.AP.APPayment>>>) (g => g.VoidDocument)).FireOn((PXGraph) this.Base, apPayment);
    this.Base.Document.Cache.Update((object) apPayment);
    this.Base.Save.Press();
  }

  protected virtual void ReleasePayment(PX.Objects.AP.APPayment apPayment)
  {
    this.Base.Document.Current = (PX.Objects.AP.APPayment) this.Base.Document.Search<PX.Objects.AP.APPayment.refNbr>((object) apPayment.RefNbr, (object) apPayment.DocType);
    this.Base.Document.Update(apPayment);
    this.Base.Save.Press();
    APDocumentRelease.ReleaseDoc(new List<PX.Objects.AP.APRegister>()
    {
      (PX.Objects.AP.APRegister) apPayment
    }, false);
  }

  protected virtual void VoidPayment(PX.Objects.AP.APPayment apPayment)
  {
    PXAdapter adapterWithDummyView = APPaymentEntryEPPExt.CreateAdapterWithDummyView(this.Base, apPayment);
    this.Base.Document.Current = (PX.Objects.AP.APPayment) this.Base.Document.Search<PX.Objects.AP.APPayment.refNbr>((object) apPayment.RefNbr, (object) apPayment.DocType);
    this.Base.Save.Press();
    this.Base.VoidCheck(adapterWithDummyView).RowCast<PX.Objects.AP.APPayment>().FirstOrDefault<PX.Objects.AP.APPayment>();
    this.Base.Save.Press();
  }

  protected virtual void VoidPaymentInEpp(
    PX.Objects.AP.APPayment apPayment,
    bool voided,
    bool canVoided,
    APPaymentProcessorPaymentTran extTran)
  {
    bool? externalPaymentVoided = apPayment.ExternalPaymentVoided;
    bool flag = false;
    if (!(externalPaymentVoided.GetValueOrDefault() == flag & externalPaymentVoided.HasValue & canVoided))
      return;
    this.VoidPaymentInEPP(apPayment);
  }

  public static PXAdapter CreateAdapterWithDummyView(APPaymentEntry graph, PX.Objects.AP.APPayment doc)
  {
    return new PXAdapter((PXView) new PXView.Dummy((PXGraph) graph, graph.Document.View.BqlSelect, new List<object>()
    {
      (object) doc
    }));
  }

  public virtual APPaymentProcessorPaymentTran ConvertPaymentResponse(
    APPaymentProcessorOrganization extOrganization,
    PX.Objects.AP.APPayment payment,
    ExternalPayment? response,
    APPaymentProcessorPaymentTran? tran = null)
  {
    APPaymentProcessorPaymentTran processorPaymentTran = tran ?? new APPaymentProcessorPaymentTran();
    processorPaymentTran.ExternalPaymentProcessorID = extOrganization.ExternalPaymentProcessorID;
    processorPaymentTran.DocType = payment.DocType;
    processorPaymentTran.RefNbr = payment.RefNbr;
    processorPaymentTran.ExternalPaymentID = response?.Id;
    processorPaymentTran.Status = response?.SingleStatus;
    processorPaymentTran.ExternalUserID = response?.CreatedBy;
    processorPaymentTran.ProcessDate = (System.DateTime?) response?.ProcessDate;
    processorPaymentTran.FundingAcctID = response?.FundingAccountId;
    processorPaymentTran.DisbursementType = response?.DisbursementType;
    processorPaymentTran.DisbursementAmount = (Decimal?) response?.Amount;
    processorPaymentTran.ExternalVendorID = response?.VendorId;
    processorPaymentTran.TransactionNumber = response?.TransactionNumber;
    processorPaymentTran.ExternalUpdatedDateTime = (System.DateTime?) response?.UpdatedTime;
    processorPaymentTran.DisbursementArriveDate = (System.DateTime?) response?.DisbursementInfo?.ArrivesBy;
    processorPaymentTran.ExternalComment = response?.ConfirmationNumber;
    if (payment.ExternalPaymentID != null)
      processorPaymentTran.TransactionState = "S";
    return processorPaymentTran;
  }

  public virtual List<APAdjust> GetPaymentAdjustments(PX.Objects.AP.APPayment payment)
  {
    return PXSelectBase<APAdjust, PXSelectGroupBy<APAdjust, Where<APAdjust.adjgDocType, Equal<Required<APAdjust.adjgDocType>>, And<APAdjust.adjgRefNbr, Equal<Required<APAdjust.adjgRefNbr>>>>, PX.Data.Aggregate<GroupBy<APAdjust.adjdDocType, GroupBy<APAdjust.adjdRefNbr, Sum<APAdjust.curyAdjgAmt>>>>>.Config>.Select((PXGraph) this.Base, (object) payment.DocType, (object) payment.RefNbr).RowCast<APAdjust>().ToList<APAdjust>();
  }

  public virtual List<POAdjust> GetPOAdjustments(PX.Objects.AP.APPayment payment)
  {
    return PXSelectBase<POAdjust, PXSelectGroupBy<POAdjust, Where<POAdjust.adjgDocType, Equal<Required<POAdjust.adjgDocType>>, And<POAdjust.adjgRefNbr, Equal<Required<POAdjust.adjgRefNbr>>>>, PX.Data.Aggregate<GroupBy<POAdjust.adjdOrderType, GroupBy<POAdjust.adjdOrderNbr, Sum<POAdjust.curyAdjgAmt>>>>>.Config>.Select((PXGraph) this.Base, (object) payment.DocType, (object) payment.RefNbr).RowCast<POAdjust>().ToList<POAdjust>();
  }

  public virtual List<PX.Objects.AP.APTran> GetCashPurchaseAdjustments(PX.Objects.AP.APPayment payment)
  {
    return PXSelectBase<PX.Objects.AP.APTran, PXSelectGroupBy<PX.Objects.AP.APTran, Where<PX.Objects.AP.APTran.tranType, Equal<Required<PX.Objects.AP.APTran.tranType>>, And<PX.Objects.AP.APTran.refNbr, Equal<Required<PX.Objects.AP.APTran.refNbr>>>>, PX.Data.Aggregate<GroupBy<PX.Objects.AP.APTran.tranType, GroupBy<PX.Objects.AP.APTran.refNbr, Sum<PX.Objects.AP.APTran.lineAmt>>>>>.Config>.Select((PXGraph) this.Base, (object) payment.DocType, (object) payment.RefNbr).RowCast<PX.Objects.AP.APTran>().ToList<PX.Objects.AP.APTran>();
  }

  public virtual APPaymentProcessorBill GetExternalPaymentProcessorBill(
    APExternalPaymentProcessor ePP,
    int? organizationID,
    string docType,
    string refNbr)
  {
    return (APPaymentProcessorBill) PXSelectBase<APPaymentProcessorBill, PXViewOf<APPaymentProcessorBill>.BasedOn<SelectFromBase<APPaymentProcessorBill, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorBill.externalPaymentProcessorID, Equal<P.AsString>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorBill.organizationID, Equal<P.AsInt>>>>, PX.Data.And<BqlOperand<PX.Data.RTrim<APPaymentProcessorBill.docType>, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<APPaymentProcessorBill.refNbr, IBqlString>.IsEqual<P.AsString>>>>>.Config>.Select((PXGraph) this.Base, (object) ePP?.ExternalPaymentProcessorID, (object) organizationID, (object) docType, (object) refNbr);
  }

  public virtual APPaymentProcessorUser GetExternalOrganizationUser(
    int? organizationID,
    string eppID,
    string externalUserID)
  {
    return (APPaymentProcessorUser) PXSelectBase<APPaymentProcessorUser, PXViewOf<APPaymentProcessorUser>.BasedOn<SelectFromBase<APPaymentProcessorUser, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorUser.externalPaymentProcessorID, Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorUser.organizationID, Equal<P.AsInt>>>>>.And<BqlOperand<APPaymentProcessorUser.externalUserID, IBqlString>.IsEqual<P.AsString>>>>>.Config>.Select((PXGraph) this.Base, (object) eppID, (object) organizationID, (object) externalUserID);
  }

  public virtual APPaymentProcessorOrganization GetExternalOrganizationById(string? externalOrgId)
  {
    return (APPaymentProcessorOrganization) PXSelectBase<APPaymentProcessorOrganization, PXViewOf<APPaymentProcessorOrganization>.BasedOn<SelectFromBase<APPaymentProcessorOrganization, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<APPaymentProcessorOrganization.externalOrganizationID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this.Base, (object) externalOrgId).FirstOrDefault<PXResult<APPaymentProcessorOrganization>>();
  }

  public virtual APPaymentProcessorPaymentTran GetLatestTransactionForPayment(
    string? externalPaymentProcessorId,
    PX.Objects.AP.APPayment payment)
  {
    return PXSelectBase<APPaymentProcessorPaymentTran, PXViewOf<APPaymentProcessorPaymentTran>.BasedOn<SelectFromBase<APPaymentProcessorPaymentTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorPaymentTran.externalPaymentProcessorID, Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorPaymentTran.refNbr, Equal<P.AsString>>>>>.And<BqlOperand<APPaymentProcessorPaymentTran.docType, IBqlString>.IsEqual<P.AsString>>>>.Order<By<BqlField<APPaymentProcessorPaymentTran.tranNbr, IBqlInt>.Desc>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, (object) externalPaymentProcessorId, (object) payment.RefNbr, (object) payment.DocType).RowCast<APPaymentProcessorPaymentTran>().FirstOrDefault<APPaymentProcessorPaymentTran>();
  }

  protected virtual PaymentProcessorManager GetPaymentProcessorManager(string type)
  {
    return this.PaymentProcessorManagerProvider(type);
  }

  private OrganizationUserData GetOrganizationUserData(int? organizationId, Guid? userId)
  {
    return new OrganizationUserData()
    {
      OrganizationId = organizationId,
      UserId = userId
    };
  }

  public class APPaymentEntryExternalPaymentProcessorHelper : 
    ExternalPaymentProcessorHelper<APPaymentEntry>
  {
  }

  public delegate IEnumerable VoidCheckDelegate(PXAdapter adapter);
}
