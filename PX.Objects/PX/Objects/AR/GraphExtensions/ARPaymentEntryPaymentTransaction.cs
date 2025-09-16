// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.GraphExtensions.ARPaymentEntryPaymentTransaction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.CA;
using PX.Objects.CM.TemporaryHelpers;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.Extensions.PaymentTransaction;
using PX.Objects.GL;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR.GraphExtensions;

public class ARPaymentEntryPaymentTransaction : 
  PaymentTransactionAcceptFormGraph<ARPaymentEntry, PX.Objects.AR.ARPayment>
{
  public PXFilter<InputPaymentInfo> ccPaymentInfo;
  public PXSelect<PX.Objects.CC.DefaultTerminal, Where<PX.Objects.CC.DefaultTerminal.userID, Equal<Current<AccessInfo.userID>>, And<PX.Objects.CC.DefaultTerminal.branchID, Equal<Current<AccessInfo.branchID>>, And<PX.Objects.CC.DefaultTerminal.processingCenterID, Equal<Current<PX.Objects.AR.ARPayment.processingCenterID>>>>>> DefaultTerminal;
  public PXAction<PX.Objects.AR.ARPayment> voidCCPaymentForReAuthorization;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>();
  }

  public bool RaisedVoidForReAuthorization { get; set; }

  [PXOverride]
  public virtual void Persist(System.Action persist)
  {
    PX.Objects.AR.ARPayment current = ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current;
    if (PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Select(Array.Empty<object>()))?.PaymentType == "POS" && !string.IsNullOrEmpty(current.ProcessingCenterID) && !string.IsNullOrEmpty(current?.TerminalID))
    {
      PX.Objects.CC.DefaultTerminal defaultTerminal = ((PXSelectBase<PX.Objects.CC.DefaultTerminal>) this.DefaultTerminal).SelectSingle(Array.Empty<object>());
      if (defaultTerminal == null)
        ((PXSelectBase<PX.Objects.CC.DefaultTerminal>) this.DefaultTerminal).Insert(new PX.Objects.CC.DefaultTerminal()
        {
          BranchID = PXContext.GetBranchID(),
          UserID = this.Base.CurrentUserInformationProvider.GetUserId(),
          ProcessingCenterID = current.ProcessingCenterID,
          TerminalID = current.TerminalID
        });
      else if (current.TerminalID != defaultTerminal.TerminalID)
      {
        defaultTerminal.TerminalID = current.TerminalID;
        ((PXSelectBase<PX.Objects.CC.DefaultTerminal>) this.DefaultTerminal).Update(defaultTerminal);
      }
    }
    persist();
  }

  protected override void RowPersisting(PX.Data.Events.RowPersisting<PX.Objects.AR.ARPayment> e)
  {
    PX.Objects.AR.ARPayment row = e.Row;
    this.CheckSyncLock(row);
    this.CheckProcessingCenter(((PXSelectBase) this.Base.Document).Cache, ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current);
    if (row.CCTransactionRefund.GetValueOrDefault() && !CCProcessingHelper.PaymentMethodSupportsIntegratedProcessing(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current) && row.DocType != "REF" && row.DocType != "VRF")
      throw new PXRowPersistingException("CCTransactionRefund", (object) row.CCTransactionRefund, "The document does not support refunds of credit card transactions.");
    PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Select(Array.Empty<object>()));
    if (row.SaveCard.GetValueOrDefault())
    {
      int? pmInstanceId = row.PMInstanceID;
      int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
      if (!(pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue) || paymentMethod?.PaymentType != "CCD" && paymentMethod?.PaymentType != "EFT" || paymentMethod?.PaymentType == "POS")
        row.SaveCard = new bool?(false);
    }
    base.RowPersisting(e);
  }

  private void CheckBranchID(PX.Objects.AR.ARPayment payment, PXCache cache)
  {
    if (payment == null || !EnumerableExtensions.IsIn<string>(payment.DocType, "PMT", "PPM", "REF") || !payment.IsCCPayment.GetValueOrDefault() || !this.BranchIsProhibited(payment))
      return;
    PX.Objects.CA.CashAccount current = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.Base.cashaccount).Current;
    string branchCd1 = PXAccess.GetBranchCD((int?) current?.BranchID);
    string branchCd2 = PXAccess.GetBranchCD(payment.BranchID);
    PXSetPropertyException propertyException = new PXSetPropertyException("The {0} cash account is related to the {1} branch and the document is related to the {2} branch, but the Inter-Branch Transactions feature is disabled. Select another cash account or change the document branch to {1}.", new object[4]
    {
      (object) current?.CashAccountCD,
      (object) branchCd1,
      (object) branchCd2,
      (object) (PXErrorLevel) 4
    });
    cache.RaiseExceptionHandling<PX.Objects.AR.ARPayment.branchID>((object) payment, (object) branchCd2, (Exception) propertyException);
  }

  protected virtual void RowUpdated(PX.Data.Events.RowUpdated<PX.Objects.AR.ARPayment> e)
  {
    PX.Objects.AR.ARPayment row = e.Row;
    PX.Objects.AR.ARPayment oldRow = e.OldRow;
    if (row == null)
      return;
    this.CheckBranchID(row, ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARPayment>>) e).Cache);
    this.CheckProcCenterAndCashAccountCurrency(row);
    this.UpdateUserAttentionFlagIfNeeded(e);
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARPayment>>) e).Cache.GetStatus((object) row) != 2 || ((PXGraph) this.Base).IsContractBasedAPI || row.ProcessingCenterID == null)
      return;
    int? pmInstanceId = row.PMInstanceID;
    int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
    if (!(pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue))
      return;
    bool? saveCard = row.SaveCard;
    bool flag = false;
    if (saveCard.GetValueOrDefault() == flag & saveCard.HasValue && this.ForceSaveCard(row))
    {
      ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARPayment>>) e).Cache.SetValueExt<PX.Objects.AR.ARPayment.saveCard>((object) row, (object) true);
    }
    else
    {
      saveCard = row.SaveCard;
      if (!saveCard.GetValueOrDefault() || !this.ProhibitSaveCard(row))
        return;
      ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARPayment>>) e).Cache.SetValueExt<PX.Objects.AR.ARPayment.saveCard>((object) row, (object) false);
    }
  }

  public virtual void CheckProcCenterAndCashAccountCurrency(PX.Objects.AR.ARPayment doc)
  {
    if (!doc.IsCCPayment.GetValueOrDefault())
      return;
    PX.Objects.CA.CashAccount docCashAcc = PX.Objects.CA.CashAccount.PK.Find((PXGraph) this.Base, doc.CashAccountID);
    PX.Objects.CA.CashAccount procCenterCashAcc = PX.Objects.CA.CashAccount.PK.Find((PXGraph) this.Base, (int?) this.GetCCProcessingCenterByProcCenterOrPMInstance(doc)?.CashAccountID);
    if ((docCashAcc == null || procCenterCashAcc == null ? 0 : (docCashAcc.CuryID != procCenterCashAcc.CuryID ? 1 : 0)) != 0)
    {
      PXSetPropertyException currecyException = this.GetDiffCurrecyException(doc, docCashAcc, procCenterCashAcc);
      ((PXSelectBase) this.Base.Document).Cache.RaiseExceptionHandling<PX.Objects.AR.ARPayment.cashAccountID>((object) doc, (object) docCashAcc?.CashAccountCD, (Exception) currecyException);
    }
    else
      ((PXSelectBase) this.Base.Document).Cache.RaiseExceptionHandling<PX.Objects.AR.ARPayment.cashAccountID>((object) doc, (object) docCashAcc?.CashAccountCD, (Exception) null);
  }

  public virtual PXSetPropertyException GetDiffCurrecyException(
    PX.Objects.AR.ARPayment doc,
    PX.Objects.CA.CashAccount docCashAcc,
    PX.Objects.CA.CashAccount procCenterCashAcc)
  {
    PX.Objects.AR.CustomerPaymentMethod paymentMethodById = this.GetCustomerPaymentMethodById(doc.PMInstanceID);
    PXSetPropertyException currecyException;
    if (paymentMethodById == null)
      currecyException = new PXSetPropertyException("The currency of the {0} processing center ({1}) differs from the currency of the {2} cash account ({3}). Select a cash account denominated in {1} to process transactions with the {0} processing center.", new object[5]
      {
        (object) doc.ProcessingCenterID,
        (object) procCenterCashAcc.CuryID,
        (object) docCashAcc.CashAccountCD,
        (object) docCashAcc.CuryID,
        (object) (PXErrorLevel) 4
      });
    else
      currecyException = new PXSetPropertyException("The currency of the {0} cash account ({1}) differs from the currency of the {2} card transactions ({3}). Select a cash account denominated in {3} to process transactions with the {2} card.", new object[5]
      {
        (object) docCashAcc.CashAccountCD,
        (object) docCashAcc.CuryID,
        (object) paymentMethodById.Descr,
        (object) procCenterCashAcc.CuryID,
        (object) (PXErrorLevel) 4
      });
    return currecyException;
  }

  protected virtual CCProcessingCenter GetCCProcessingCenterByProcCenterOrPMInstance(PX.Objects.AR.ARPayment doc)
  {
    CCProcessingCenter centerOrPmInstance = (CCProcessingCenter) null;
    if (doc.ProcessingCenterID != null)
      centerOrPmInstance = this.GetProcessingCenterById(doc.ProcessingCenterID);
    else if (doc.PMInstanceID.HasValue)
      centerOrPmInstance = this.GetProcessingCenterById(this.GetCustomerPaymentMethodById(doc.PMInstanceID).CCProcessingCenterID);
    return centerOrPmInstance;
  }

  protected override void RowSelected(PX.Data.Events.RowSelected<PX.Objects.AR.ARPayment> e)
  {
    base.RowSelected(e);
    PX.Objects.AR.ARPayment row = e.Row;
    if (row == null)
      return;
    this.TranHeldwarnMsg = "The transaction is held for review by the processing center. Use the processing center interface to approve or reject the transaction. After the transaction is processed by the processing center, use the Validate Card Payment action to update the processing status.";
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARPayment>>) e).Cache;
    bool valueOrDefault1 = row.Hold.GetValueOrDefault();
    bool valueOrDefault2 = row.OpenDoc.GetValueOrDefault();
    bool valueOrDefault3 = row.Released.GetValueOrDefault();
    bool flag1 = this.EnableCCProcess(row);
    bool flag2 = row.DocType == "CRM" || row.DocType == "SMB" || row.DocType == "PPI";
    bool flag3 = row.DocType == "REF";
    bool isIncorrect = CCProcessingHelper.PaymentMethodSupportsIntegratedProcessing(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current);
    bool flag4 = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current?.PaymentType == "POS";
    CCProcessingCenter current = ((PXSelectBase<CCProcessingCenter>) this.Base.processingCenter).Current;
    bool? nullable1;
    int num1;
    if (current == null)
    {
      num1 = 0;
    }
    else
    {
      nullable1 = current.IsExternalAuthorizationOnly;
      num1 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    bool flag5 = num1 != 0;
    int num2;
    if (EnumerableExtensions.IsIn<string>(row.DocType, "PMT", "PPM", "REF"))
    {
      nullable1 = row.IsCCPayment;
      num2 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    bool flag6 = num2 != 0;
    bool flag7 = AutoNumberAttribute.IsViewOnlyRecord<PX.Objects.AR.ARPayment.refNbr>(cache1, (object) row);
    nullable1 = row.Voided;
    int num3;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = row.VoidAppl;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = row.Released;
        num3 = nullable1.GetValueOrDefault() ? 1 : 0;
        goto label_11;
      }
    }
    num3 = 1;
label_11:
    bool flag8 = num3 != 0;
    bool flag9 = !flag8 && this.Base.HasUnreleasedSOInvoice;
    bool flag10 = (flag6 && !valueOrDefault3 || !flag6 && !flag8 && !this.Base.HasUnreleasedSOInvoice) && !flag7;
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARPayment.branchID>(cache1, (object) row, flag10);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARRegister.aRAccountID>(cache1, (object) row, flag9 | flag10);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARRegister.aRSubID>(cache1, (object) row, flag9 | flag10);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARPayment.projectID>(cache1, (object) row, flag10);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARPayment.taskID>(cache1, (object) row, flag10);
    PXCache cache2 = cache1;
    int num4;
    if (row == null)
    {
      num4 = 0;
    }
    else
    {
      nullable1 = row.IsCCPayment;
      bool flag11 = false;
      num4 = nullable1.GetValueOrDefault() == flag11 & nullable1.HasValue ? 1 : 0;
    }
    PaymentRefAttribute.SetAllowAskUpdateLastRefNbr<PX.Objects.AR.ARPayment.extRefNbr>(cache2, num4 != 0);
    int? nullable2;
    int? nullable3;
    if (row.DocType == "REF" & isIncorrect)
    {
      this.SetVisibilityCreditCardControlsForRefund(cache1, row);
    }
    else
    {
      PX.Objects.AR.ARPayment arPayment = row;
      int num5;
      if (isIncorrect)
      {
        nullable2 = row.PMInstanceID;
        nullable3 = PaymentTranExtConstants.NewPaymentProfile;
        if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
        {
          num5 = !flag4 ? 1 : 0;
          goto label_20;
        }
      }
      num5 = 0;
label_20:
      bool? nullable4 = new bool?(num5 != 0);
      arPayment.NewCard = nullable4;
      nullable1 = row.NewCard;
      int num6 = nullable1.GetValueOrDefault() ? 1 : 0;
      bool flag12 = num6 == 0 && !flag4 && !flag2;
      bool flag13 = (num6 | (flag4 ? 1 : 0)) != 0 && !flag2;
      PXUIFieldAttribute.SetVisible<PX.Objects.AR.ARPayment.pMInstanceID>(cache1, (object) row, flag12);
      PXUIFieldAttribute.SetVisible<PX.Objects.AR.ARPayment.processingCenterID>(cache1, (object) row, flag13);
    }
    PXPersistingCheck pxPersistingCheck = (PXPersistingCheck) 0;
    if (!(flag2 | flag1))
    {
      nullable1 = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RequireExtRef;
      bool flag14 = false;
      if (!(nullable1.GetValueOrDefault() == flag14 & nullable1.HasValue) && !(row.DocType == "RPM"))
        goto label_24;
    }
    pxPersistingCheck = (PXPersistingCheck) 2;
label_24:
    ExternalTransactionState transactionState = this.GetActiveTransactionState();
    row.CCPaymentStateDescr = this.GetPaymentStateDescr(transactionState);
    IEnumerable<IExternalTransaction> extTrans = this.GetExtTrans();
    int num7;
    if (flag1 && row.DocType == "REF" && !transactionState.IsRefunded && (!transactionState.IsImportedUnknown || transactionState.SyncFailed))
    {
      nullable1 = row.CCTransactionRefund;
      if (nullable1.GetValueOrDefault() && !valueOrDefault3)
      {
        num7 = !this.RefundDocHasValidSharedTran(extTrans) ? 1 : 0;
        goto label_28;
      }
    }
    num7 = 0;
label_28:
    bool flag15 = num7 != 0;
    bool flag16 = isIncorrect && row.DocType == "REF" && !transactionState.IsRefunded && this.HasProcCenterSupportingUnlinkedMode(cache1, row);
    nullable1 = row.CCTransactionRefund;
    bool valueOrDefault4 = nullable1.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARPayment.refTranExtNbr>(cache1, (object) row, flag15);
    PXCache pxCache1 = cache1;
    int num8;
    if (isIncorrect)
    {
      nullable3 = row.PMInstanceID;
      nullable2 = PaymentTranExtConstants.NewPaymentProfile;
      num8 = nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue ? 1 : 0;
    }
    else
      num8 = 0;
    PXUIFieldAttribute.SetRequired<PX.Objects.AR.ARPayment.processingCenterID>(pxCache1, num8 != 0);
    PXUIFieldAttribute.SetVisible<PX.Objects.AR.ARPayment.cCTransactionRefund>(cache1, (object) row, flag16);
    PXCache pxCache2 = cache1;
    PX.Objects.AR.ARPayment arPayment1 = row;
    nullable1 = row.IsCCPayment;
    int num9 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PX.Objects.AR.ARPayment.cCPaymentStateDescr>(pxCache2, (object) arPayment1, num9 != 0);
    PXUIFieldAttribute.SetVisible<PX.Objects.AR.ARPayment.refTranExtNbr>(cache1, (object) row, row.DocType == "REF" & flag1);
    PXUIFieldAttribute.SetRequired<PX.Objects.AR.ARPayment.refTranExtNbr>(cache1, flag3 & isIncorrect);
    PXUIFieldAttribute.SetVisible<PX.Objects.AR.ARPayment.terminalID>(cache1, (object) row, flag4 && (!flag3 || !valueOrDefault4));
    PXDefaultAttribute.SetPersistingCheck<PX.Objects.AR.ARPayment.extRefNbr>(cache1, (object) row, pxPersistingCheck);
    this.SetUsingAcceptHostedForm(row);
    bool flag17 = this.CanAuthorize(row, transactionState);
    bool flag18 = this.CanCapture(row, transactionState);
    bool flag19 = this.CanCaptureOnly(row);
    bool flag20 = this.CanCredit(row, transactionState);
    bool flag21 = this.CanValidate(row, transactionState);
    bool flag22 = this.CanVoid(row, transactionState);
    bool flag23 = this.CanVoidCheck(row);
    bool flag24 = this.CanVoidForReAuthorization(row, transactionState);
    bool flag25 = this.CanRecord(row, transactionState);
    ((PXAction) this.authorizeCCPayment).SetEnabled(flag17 && !flag5);
    ((PXAction) this.captureCCPayment).SetEnabled(flag18);
    ((PXAction) this.validateCCPayment).SetEnabled(flag21);
    ((PXAction) this.voidCCPayment).SetEnabled(flag22);
    ((PXAction) this.Base.voidCheck).SetEnabled(((PXAction) this.Base.voidCheck).GetEnabled() & flag23);
    ((PXAction) this.creditCCPayment).SetEnabled(flag20);
    PXAction<PX.Objects.AR.ARPayment> captureOnlyCcPayment = this.captureOnlyCCPayment;
    nullable2 = row.PMInstanceID;
    nullable3 = PaymentTranExtConstants.NewPaymentProfile;
    int num10 = !(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue) & flag17 & flag19 ? 1 : 0;
    ((PXAction) captureOnlyCcPayment).SetEnabled(num10 != 0);
    ((PXAction) this.recordCCPayment).SetEnabled(flag25);
    ((PXAction) this.voidCCPaymentForReAuthorization).SetEnabled(flag24);
    bool flag26 = !transactionState.IsCaptured && !transactionState.IsPreAuthorized;
    if (flag1)
    {
      nullable1 = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.IntegratedCCProcessing;
      if (nullable1.GetValueOrDefault() && !valueOrDefault3)
      {
        nullable1 = row.VoidAppl;
        if (!nullable1.Value)
        {
          bool flag27 = row.DocType == "REF" && this.RefundDocHasValidSharedTran(extTrans);
          int num11;
          if (!valueOrDefault1 & valueOrDefault2 && transactionState.IsSettlementDue | flag27)
          {
            nullable1 = row.PendingProcessing;
            bool flag28 = false;
            num11 = nullable1.GetValueOrDefault() == flag28 & nullable1.HasValue ? 1 : 0;
          }
          else
            num11 = 0;
          ((PXAction) this.Base.release).SetEnabled(num11 != 0);
        }
        else
          ((PXAction) this.Base.release).SetEnabled(!valueOrDefault1 & valueOrDefault2 && (flag26 || transactionState.IsPreAuthorized && transactionState.ProcessingStatus == ProcessingStatus.VoidFail));
      }
    }
    if (row.DocType == "VRF")
      UIState.RaiseOrHideErrorByErrorLevelPriority<PX.Objects.AR.ARPayment.paymentMethodID>(cache1, (object) e.Row, isIncorrect, "The system does not support integrated processing for voided refunds with the Credit Card and EFT payment methods. You need to void a refund transaction manually in the processing center, and then release the voided refund in the system.", (PXErrorLevel) 2);
    this.ShowWarningIfActualFinPeriodClosed(e, row);
    this.ShowWarningIfExternalAuthorizationOnly(e, row);
    this.ShowUnlinkedRefundWarnIfNeeded(e, transactionState);
    this.DenyDeletionVoidedPaymentDependingOnTran(cache1, row);
    this.ShowWarningOnProcessingCenterID(e, transactionState);
    this.ShowWarningOnNewAccount(e, row);
    this.ShowWarningFromLongOperation(e, row);
    this.SetActionCaptions();
  }

  private bool BranchIsProhibited(PX.Objects.AR.ARPayment payment)
  {
    PX.Objects.CA.CashAccount current = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.Base.cashaccount).Current;
    if (current == null || !payment.BranchID.HasValue || !current.BranchID.HasValue)
      return false;
    int? nullable1;
    int num;
    if (!PXAccess.FeatureInstalled<FeaturesSet.interBranch>())
    {
      nullable1 = payment.BranchID;
      int? branchId = current.BranchID;
      num = !(nullable1.GetValueOrDefault() == branchId.GetValueOrDefault() & nullable1.HasValue == branchId.HasValue) ? 1 : 0;
    }
    else
      num = 0;
    bool flag = num != 0;
    if (flag)
    {
      int? parentOrganizationId1 = PXAccess.GetParentOrganizationID(payment.BranchID);
      int? parentOrganizationId2 = PXAccess.GetParentOrganizationID(current.BranchID);
      if (parentOrganizationId1.HasValue)
      {
        int? nullable2 = parentOrganizationId1;
        nullable1 = parentOrganizationId2;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          PX.Objects.GL.DAC.Organization organization = PX.Objects.GL.DAC.Organization.PK.Find((PXGraph) this.Base, parentOrganizationId1);
          if (organization != null && organization.OrganizationType != "Balancing")
            flag = false;
        }
      }
    }
    return flag;
  }

  private void SetActionCaptions()
  {
    bool flag = this.IsEFT();
    ((PXAction) this.voidCCPayment).SetCaption(flag ? (this.IsRejection() ? "Record EFT Rejection" : "Void EFT Payment") : "Void Card Payment");
    ((PXAction) this.recordCCPayment).SetCaption(flag ? "Record EFT Payment" : "Record Card Payment");
    ((PXAction) this.creditCCPayment).SetCaption(flag ? "Refund EFT Payment" : "Refund Card Payment");
    ((PXAction) this.validateCCPayment).SetCaption(flag ? "Validate EFT Payment" : "Validate Card Payment");
  }

  private bool IsCCPaymentMethod(PX.Objects.AR.ARPayment doc)
  {
    if (string.IsNullOrEmpty(doc.PaymentMethodID))
      return false;
    PX.Objects.CA.PaymentMethod paymentMethod = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) this.Base, doc.PaymentMethodID);
    return paymentMethod != null && EnumerableExtensions.IsIn<string>(paymentMethod.PaymentType, "CCD", "EFT", "POS");
  }

  protected virtual void ShowWarningIfActualFinPeriodClosed(
    PX.Data.Events.RowSelected<PX.Objects.AR.ARPayment> e,
    PX.Objects.AR.ARPayment doc)
  {
    bool flag = this.IsCCPaymentMethod(doc);
    if (flag && this.IsActualFinPeriodClosedForBranch(PXContext.GetBranchID()) && string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PX.Objects.AR.ARPayment.paymentMethodID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARPayment>>) e).Cache, (object) doc)))
      ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARPayment>>) e).Cache.RaiseExceptionHandling<PX.Objects.AR.ARPayment.paymentMethodID>((object) doc, (object) doc.PaymentMethodID, (Exception) new PXSetPropertyException("The operation cannot be performed because the financial period of the current date is either closed, inactive, or does not exist for the {0} company. To process the payment with a credit card, EFT, or POS terminal, create or reopen the financial period.", (PXErrorLevel) 2, new object[1]
      {
        (object) ((PXAccess.Organization) PXAccess.GetBranch(PXContext.GetBranchID()).Organization).OrganizationCD
      }));
    if (!flag || !this.IsActualFinPeriodClosedForBranch(doc.BranchID) || !string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PX.Objects.AR.ARPayment.paymentMethodID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARPayment>>) e).Cache, (object) doc)))
      return;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARPayment>>) e).Cache.RaiseExceptionHandling<PX.Objects.AR.ARPayment.paymentMethodID>((object) doc, (object) doc.PaymentMethodID, (Exception) new PXSetPropertyException("The operation cannot be performed because the financial period of the current date is either closed, inactive, or does not exist for the {0} company. To process the payment with a credit card, EFT, or POS terminal, create or reopen the financial period.", (PXErrorLevel) 2, new object[1]
    {
      (object) ((PXAccess.Organization) PXAccess.GetBranch(doc.BranchID).Organization).OrganizationCD
    }));
  }

  protected virtual void ShowWarningIfExternalAuthorizationOnly(
    PX.Data.Events.RowSelected<PX.Objects.AR.ARPayment> e,
    PX.Objects.AR.ARPayment doc)
  {
    ExternalTransactionState transactionState = this.GetActiveTransactionState();
    CCProcessingCenter current = ((PXSelectBase<CCProcessingCenter>) this.Base.processingCenter).Current;
    bool flag = current != null && current.IsExternalAuthorizationOnly.GetValueOrDefault() && (!transactionState.IsActive || transactionState.IsExpired) && doc.Status == "W" && (doc.DocType == "PMT" || doc.DocType == "PPM");
    PX.Objects.AR.CustomerPaymentMethod paymentMethodById = this.GetCustomerPaymentMethodById(doc.PMInstanceID);
    UIState.RaiseOrHideErrorByErrorLevelPriority<PX.Objects.AR.ARPayment.pMInstanceID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARPayment>>) e).Cache, (object) e.Row, (flag ? 1 : 0) != 0, "The {0} card is associated with the {1} processing center that does not support the Authorize action. The Capture action is supported only for payments that were pre-authorized externally.", (PXErrorLevel) 2, (object) paymentMethodById?.Descr, (object) current?.ProcessingCenterID);
  }

  protected virtual void ShowWarningOnProcessingCenterID(
    PX.Data.Events.RowSelected<PX.Objects.AR.ARPayment> e,
    ExternalTransactionState state)
  {
    PX.Objects.AR.ARPayment row = e.Row;
    if (row == null || state != null && state.IsActive)
      return;
    CCProcessingCenter processingCenterById = this.GetProcessingCenterById(row.ProcessingCenterID);
    bool flag1 = row.DocType == "PMT" || row.DocType == "PPM";
    int num1 = ((processingCenterById != null ? (processingCenterById.IsExternalAuthorizationOnly.GetValueOrDefault() ? 1 : 0) : 0) & (flag1 ? 1 : 0)) == 0 ? 0 : (row.PendingProcessing.GetValueOrDefault() ? 1 : 0);
    bool? nullable;
    int num2;
    if (processingCenterById == null)
    {
      num2 = 0;
    }
    else
    {
      nullable = processingCenterById.UseAcceptPaymentForm;
      bool flag2 = false;
      num2 = nullable.GetValueOrDefault() == flag2 & nullable.HasValue ? 1 : 0;
    }
    int num3 = flag1 ? 1 : 0;
    int num4;
    if ((num2 & num3) != 0)
    {
      nullable = row.PendingProcessing;
      if (nullable.GetValueOrDefault())
      {
        nullable = row.NewCard;
        num4 = nullable.GetValueOrDefault() ? 1 : 0;
        goto label_8;
      }
    }
    num4 = 0;
label_8:
    bool flag3 = num4 != 0;
    string message = string.Empty;
    bool flag4 = false;
    if (num1 != 0)
    {
      message = "The {0} processing center does not support the Authorize action. The Capture action is supported only for payments that were pre-authorized externally.";
      flag4 = true;
    }
    else if (flag3)
    {
      message = "The Accept Payments from New Cards check box is cleared for the {0} processing center. Payments with the New Card check box selected cannot be processed with this processing center.";
      flag4 = true;
    }
    else if (((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current?.PaymentType == "POS")
    {
      int num5;
      if (processingCenterById == null)
      {
        num5 = 1;
      }
      else
      {
        nullable = processingCenterById.AcceptPOSPayments;
        num5 = !nullable.GetValueOrDefault() ? 1 : 0;
      }
      if (num5 != 0)
      {
        message = "Payments from POS Terminals are disabled for the {0} processing center. To enable them, on the Processing Centers (CA205000) form, select the Accept Payments from POS Terminals check box.";
        flag4 = true;
      }
    }
    UIState.RaiseOrHideErrorByErrorLevelPriority<PX.Objects.Extensions.PaymentTransaction.Payment.processingCenterID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARPayment>>) e).Cache, (object) e.Row, (flag4 ? 1 : 0) != 0, message, (PXErrorLevel) 2, (object) processingCenterById?.ProcessingCenterID);
  }

  private void ShowWarningOnNewAccount(PX.Data.Events.RowSelected<PX.Objects.AR.ARPayment> e, PX.Objects.AR.ARPayment payment)
  {
    if (!payment.NewAccount.GetValueOrDefault() || !this.IsEFT())
      return;
    UIState.RaiseOrHideError<PX.Objects.AR.ARPayment.newAccount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARPayment>>) e).Cache, (object) e.Row, true, "By continuing this operation, you confirm that the customer's bank account details were obtained legally and with the permission of the account holder. This payment is authorized by the account holder. If this is not the case, this operation must be terminated.", (PXErrorLevel) 2, (object) payment.NewAccount);
  }

  private void ShowWarningFromLongOperation(PX.Data.Events.RowSelected<PX.Objects.AR.ARPayment> e, PX.Objects.AR.ARPayment doc)
  {
    if (!(PXLongOperation.GetCustomInfoPersistent(((PXGraph) this.Base).UID, true) is PaymentTransactionGraph<ARPaymentEntry, PX.Objects.AR.ARPayment>.LongOperationWarning customInfoPersistent))
      return;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARPayment>>) e).Cache;
    if (customInfoPersistent.FieldName == "AdjDate")
    {
      bool? released = doc.Released;
      bool flag = false;
      if (released.GetValueOrDefault() == flag & released.HasValue)
        cache.RaiseExceptionHandling(customInfoPersistent.FieldName, (object) doc, (object) doc.AdjDate, (Exception) customInfoPersistent.Exception);
    }
    if (customInfoPersistent.FieldName == "CuryUnappliedBal")
      cache.RaiseExceptionHandling(customInfoPersistent.FieldName, (object) doc, (object) doc.CuryUnappliedBal, (Exception) customInfoPersistent.Exception);
    if (customInfoPersistent.FieldName == "PaymentMethodID")
      cache.RaiseExceptionHandling(customInfoPersistent.FieldName, (object) doc, (object) doc.PaymentMethodID, (Exception) customInfoPersistent.Exception);
    PXLongOperation.RemoveCustomInfoPersistent(((PXGraph) this.Base).UID);
  }

  public static bool IsDocTypePayment(PX.Objects.AR.ARPayment doc)
  {
    return doc.DocType == "PMT" || doc.DocType == "PPM";
  }

  private bool IsEFT()
  {
    return string.Equals(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current?.PaymentType, "EFT");
  }

  private bool IsRejection()
  {
    PX.Objects.AR.ARPayment current = ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current;
    if (current == null)
      return false;
    PX.Objects.Extensions.PaymentTransaction.Payment extension = PXCacheEx.GetExtension<PX.Objects.Extensions.PaymentTransaction.Payment>((IBqlTable) current);
    return extension != null && extension.IsRejection.GetValueOrDefault();
  }

  public bool EnableCCProcess(PX.Objects.AR.ARPayment doc)
  {
    bool flag1 = false;
    PX.Objects.CA.PaymentMethod current1 = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current;
    if (!doc.IsMigratedRecord.GetValueOrDefault() && CCProcessingHelper.PaymentMethodSupportsIntegratedProcessing(current1))
      flag1 = ARPaymentEntryPaymentTransaction.IsDocTypePayment(doc) || doc.DocType == "REF" || doc.DocType == "RPM";
    bool flag2 = flag1 & !doc.Voided.Value;
    bool flag3 = this.IsProcCenterDisabled(this.SelectedProcessingCenterType);
    CCProcessingCenter current2 = ((PXSelectBase<CCProcessingCenter>) this.Base.processingCenter).Current;
    int num1 = flag2 ? 1 : 0;
    int num2;
    if (!flag3)
    {
      if (current2 == null)
      {
        num2 = 1;
      }
      else
      {
        bool? isActive = current2.IsActive;
        bool flag4 = false;
        num2 = !(isActive.GetValueOrDefault() == flag4 & isActive.HasValue) ? 1 : 0;
      }
    }
    else
      num2 = 0;
    return (num1 & num2) != 0 && this.IsFinPeriodValid(PXContext.GetBranchID(), ((PXSelectBase<GLSetup>) this.Base.glsetup).Current.RestrictAccessToClosedPeriods) && this.IsFinPeriodValid(doc.BranchID, ((PXSelectBase<GLSetup>) this.Base.glsetup).Current.RestrictAccessToClosedPeriods);
  }

  public bool CanAuthorize()
  {
    return this.CanAuthorize(((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current, this.GetActiveTransactionState());
  }

  public bool CanCapture()
  {
    return this.CanCapture(((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current, this.GetActiveTransactionState());
  }

  public bool CanVoid()
  {
    return this.CanVoid(((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current, this.GetActiveTransactionState());
  }

  public bool CanCredit()
  {
    return this.CanCredit(((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current, this.GetActiveTransactionState());
  }

  public bool CanValidate()
  {
    return this.CanValidate(((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current, this.GetActiveTransactionState());
  }

  public bool CanVoidForReAuthorization()
  {
    return this.CanVoidForReAuthorization(((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current, this.GetActiveTransactionState());
  }

  private bool CanAuthorize(PX.Objects.AR.ARPayment doc, ExternalTransactionState state)
  {
    if (!this.EnableCCProcess(doc) || this.IsEFT())
      return false;
    PXCache cache = ((PXSelectBase) this.Base.Document).Cache;
    bool flag = !doc.Hold.GetValueOrDefault() && ARPaymentEntryPaymentTransaction.IsDocTypePayment(doc) && (!this.UseAcceptHostedForm || cache.GetStatus((object) doc) != 2);
    if (flag)
      flag = !state.IsPreAuthorized && !state.IsCaptured && !state.IsImportedUnknown;
    if (flag)
    {
      IEnumerable<IExternalTransaction> extTrans = this.GetExtTrans();
      flag = !ExternalTranHelper.HasImportedNeedSyncTran((PXGraph) this.Base, extTrans) && !this.RefundDocHasValidSharedTran(extTrans);
    }
    if (flag && ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current.PaymentType == "POS")
      flag = !string.IsNullOrEmpty(doc.TerminalID);
    return flag;
  }

  private bool CanCapture(PX.Objects.AR.ARPayment doc, ExternalTransactionState state)
  {
    if (!this.EnableCCProcess(doc))
      return false;
    PXCache cache = ((PXSelectBase) this.Base.Document).Cache;
    bool flag1 = !doc.Hold.GetValueOrDefault() && ARPaymentEntryPaymentTransaction.IsDocTypePayment(doc);
    if (flag1)
    {
      CCProcessingCenter current = ((PXSelectBase<CCProcessingCenter>) this.Base.processingCenter).Current;
      int num;
      if (current == null)
      {
        num = 0;
      }
      else
      {
        bool? authorizationOnly = current.IsExternalAuthorizationOnly;
        bool flag2 = false;
        num = authorizationOnly.GetValueOrDefault() == flag2 & authorizationOnly.HasValue ? 1 : 0;
      }
      bool flag3 = num != 0 || state.IsActive && !state.IsExpired;
      flag1 = ((state.IsCaptured || state.IsImportedUnknown ? 0 : (!state.IsOpenForReview ? 1 : 0)) & (flag3 ? 1 : 0)) != 0 && (!this.UseAcceptHostedForm || cache.GetStatus((object) doc) != 2 && !state.IsOpenForReview);
    }
    if (flag1)
    {
      IEnumerable<IExternalTransaction> extTrans = this.GetExtTrans();
      flag1 = !ExternalTranHelper.HasImportedNeedSyncTran((PXGraph) this.Base, extTrans) && !this.RefundDocHasValidSharedTran(extTrans);
    }
    if (flag1 && ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current.PaymentType == "POS")
      flag1 = !string.IsNullOrEmpty(doc.TerminalID);
    return flag1;
  }

  private bool CanRecord(PX.Objects.AR.ARPayment doc, ExternalTransactionState state)
  {
    if (!this.EnableCCProcess(doc))
      return false;
    bool flag = !doc.Hold.GetValueOrDefault() && (ARPaymentEntryPaymentTransaction.IsDocTypePayment(doc) && !state.IsPreAuthorized && !state.IsCaptured || doc.DocType == "REF" && !state.IsRefunded) && (!state.IsImportedUnknown || state.SyncFailed);
    if (flag)
    {
      IEnumerable<IExternalTransaction> extTrans = this.GetExtTrans();
      flag = !ExternalTranHelper.HasImportedNeedSyncTran((PXGraph) this.Base, extTrans) && !this.RefundDocHasValidSharedTran(extTrans);
    }
    return flag;
  }

  private bool CanCaptureOnly(PX.Objects.AR.ARPayment doc)
  {
    return !this.IsEFT() && CCProcessingFeatureHelper.IsFeatureSupported(((PXSelectBase<CCProcessingCenter>) this.Base.processingCenter).Current, CCProcessingFeature.CapturePreauthorization, false);
  }

  private bool CanVoid(PX.Objects.AR.ARPayment doc, ExternalTransactionState state)
  {
    if (!this.EnableCCProcess(doc))
      return false;
    bool? hold = doc.Hold;
    bool flag1 = false;
    bool flag2 = hold.GetValueOrDefault() == flag1 & hold.HasValue && doc.DocType == "RPM" && (state.IsCaptured || state.IsPreAuthorized) || state.IsPreAuthorized && ARPaymentEntryPaymentTransaction.IsDocTypePayment(doc);
    if (flag2)
      flag2 = (!state.IsOpenForReview || !this.GettingDetailsByTranSupported(doc)) && !ExternalTranHelper.HasImportedNeedSyncTran((PXGraph) this.Base, this.GetExtTrans());
    return flag2;
  }

  protected virtual bool CanVoidCheck(PX.Objects.AR.ARPayment doc)
  {
    PX.Objects.CA.PaymentMethod current = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current;
    return !(doc.DocType == "REF") || current == null || !EnumerableExtensions.IsIn<string>(current.PaymentType, "CCD", "EFT") || !((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.IntegratedCCProcessing.GetValueOrDefault();
  }

  private bool CanCredit(PX.Objects.AR.ARPayment doc, ExternalTransactionState state)
  {
    if (!this.EnableCCProcess(doc))
      return false;
    bool? hold = doc.Hold;
    bool flag1 = false;
    bool flag2 = hold.GetValueOrDefault() == flag1 & hold.HasValue && doc.DocType == "REF";
    if (flag2)
      flag2 = !state.IsRefunded && (!state.IsImportedUnknown || state.SyncFailed);
    if (flag2)
      flag2 = !this.RefundDocHasValidSharedTran(this.GetExtTrans());
    return flag2;
  }

  private bool CanValidate(PX.Objects.AR.ARPayment doc, ExternalTransactionState state)
  {
    if (!this.EnableCCProcess(doc))
      return false;
    PXCache cache = ((PXSelectBase) this.Base.Document).Cache;
    if (doc.Hold.GetValueOrDefault() || !ARPaymentEntryPaymentTransaction.IsDocTypePayment(doc) && !EnumerableExtensions.IsIn<string>(doc.DocType, "RPM", "REF") || cache.GetStatus((object) doc) == 2)
      return false;
    ExternalTransactionState transactionState = ExternalTranHelper.GetLastTransactionState((PXGraph) this.Base, this.GetExtTrans());
    int num;
    if (!this.CanCapture(doc, state) && !this.CanAuthorize(doc, state) && !state.IsOpenForReview && !ExternalTranHelper.HasImportedNeedSyncTran((PXGraph) this.Base, this.GetExtTrans()) && !state.NeedSync && !state.IsImportedUnknown)
    {
      int? pmInstanceId = doc.PMInstanceID;
      int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
      if (!(pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue) && (!(doc.DocType == "REF") || transactionState.IsRefunded || transactionState.IsDeclined))
      {
        num = !(doc.DocType == "RPM") ? 0 : (transactionState.HasErrors ? 1 : (transactionState.ProcessingStatus == ProcessingStatus.Unknown ? 1 : 0));
        goto label_8;
      }
    }
    num = 1;
label_8:
    bool flag = num != 0;
    if (flag && doc.DocType == "REF")
    {
      switch (ExternalTranHelper.GetSharedTranStatus((PXGraph) this.Base, this.GetExtTrans().FirstOrDefault<IExternalTransaction>()))
      {
        case ExternalTranHelper.SharedTranStatus.Synchronized:
        case ExternalTranHelper.SharedTranStatus.ClearState:
          flag = false;
          break;
      }
    }
    if (!flag)
    {
      AfterProcessingManager processingManager = this.GetAfterProcessingManager(this.Base);
      flag = processingManager != null && !processingManager.CheckDocStateConsistency((IBqlTable) doc);
    }
    return flag && this.GettingDetailsByTranSupported(doc);
  }

  private bool CanVoidForReAuthorization(PX.Objects.AR.ARPayment doc, ExternalTransactionState state)
  {
    if (!this.EnableCCProcess(doc) || !state.IsPreAuthorized || !doc.PMInstanceID.HasValue)
      return false;
    return !state.IsOpenForReview || !this.GettingDetailsByTranSupported(doc);
  }

  private void UpdateUserAttentionFlagIfNeeded(PX.Data.Events.RowUpdated<PX.Objects.AR.ARPayment> e)
  {
    PX.Objects.AR.ARPayment row = e.Row;
    PX.Objects.AR.ARPayment oldRow = e.OldRow;
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARPayment>>) e).Cache.ObjectsEqual<PX.Objects.AR.ARPayment.paymentMethodID, PX.Objects.AR.ARPayment.pMInstanceID>((object) row, (object) oldRow))
      return;
    if (CCProcessingHelper.PaymentMethodSupportsIntegratedProcessing(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current) && (row.DocType == "PMT" || row.DocType == "PPM"))
    {
      IEnumerable<IExternalTransaction> extTrans = this.GetExtTrans();
      bool flag1 = extTrans.Count<IExternalTransaction>() == 0;
      if (!flag1)
      {
        ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) this.Base, extTrans.First<IExternalTransaction>());
        flag1 = (transactionState.IsVoided || transactionState.IsExpired) && !transactionState.IsActive;
      }
      if (!flag1)
        return;
      int? pmInstanceId = row.PMInstanceID;
      int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
      bool flag2 = pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue;
      ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARPayment>>) e).Cache.SetValueExt<PX.Objects.AR.ARPayment.isCCUserAttention>((object) row, (object) flag2);
    }
    else
      ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARPayment>>) e).Cache.SetValueExt<PX.Objects.AR.ARPayment.isCCUserAttention>((object) row, (object) false);
  }

  private void DenyDeletionVoidedPaymentDependingOnTran(PXCache cache, PX.Objects.AR.ARPayment doc)
  {
    if (cache.GetStatus((object) doc) == 2)
      return;
    bool? released = doc.Released;
    bool flag = false;
    if (!(released.GetValueOrDefault() == flag & released.HasValue) || !(doc.DocType == "RPM"))
      return;
    PX.Objects.AR.ExternalTransaction extTran = ((PXSelectBase<PX.Objects.AR.ExternalTransaction>) this.Base.ExternalTran).SelectSingle(Array.Empty<object>());
    if (extTran == null)
      return;
    ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) this.Base, (IExternalTransaction) extTran);
    if (!transactionState.IsVoided && !transactionState.IsRefunded)
      return;
    cache.AllowDelete = false;
  }

  private void ShowUnlinkedRefundWarnIfNeeded(
    PX.Data.Events.RowSelected<PX.Objects.AR.ARPayment> e,
    ExternalTransactionState state)
  {
    PX.Objects.AR.ARPayment row = e.Row;
    if (!this.CanCredit(row, state))
      return;
    bool? transactionRefund = row.CCTransactionRefund;
    bool flag1 = false;
    if (!(transactionRefund.GetValueOrDefault() == flag1 & transactionRefund.HasValue))
      return;
    int? pmInstanceId = row.PMInstanceID;
    int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
    if (pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue)
      return;
    CCProcessingCenter current = ((PXSelectBase<CCProcessingCenter>) this.Base.processingCenter).Current;
    if (current != null)
    {
      bool? allowUnlinkedRefund = current.AllowUnlinkedRefund;
      bool flag2 = false;
      if (allowUnlinkedRefund.GetValueOrDefault() == flag2 & allowUnlinkedRefund.HasValue)
      {
        PX.Objects.AR.CustomerPaymentMethod paymentMethodById = this.GetCustomerPaymentMethodById(row.PMInstanceID);
        ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARPayment>>) e).Cache.RaiseExceptionHandling<PX.Objects.AR.ARPayment.pMInstanceID>((object) row, (object) row.PMInstanceID, (Exception) new PXSetPropertyException<PX.Objects.AR.ARPayment.pMInstanceID>("The {0} card is associated with the {1} processing center that does not allow processing unlinked refunds. Select another card to process the unlinked refund.", (PXErrorLevel) 2, new object[2]
        {
          (object) paymentMethodById?.Descr,
          (object) current.ProcessingCenterID
        }));
        return;
      }
    }
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARPayment>>) e).Cache.RaiseExceptionHandling<PX.Objects.AR.ARPayment.pMInstanceID>((object) row, (object) row.PMInstanceID, (Exception) null);
  }

  private bool ForceSaveCard(PX.Objects.AR.ARPayment payment)
  {
    bool flag = false;
    PX.Objects.CA.PaymentMethod current1 = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current;
    string savePaymentProfiles = ((PXSelectBase<CustomerClass>) this.Base.customerclass).Current?.SavePaymentProfiles;
    CCProcessingCenter current2 = ((PXSelectBase<CCProcessingCenter>) this.Base.processingCenter).Current;
    if (savePaymentProfiles == "F" && (current1?.PaymentType == "CCD" || current1?.PaymentType == "EFT") && current1 != null)
    {
      bool? nullable = current1.IsAccountNumberRequired;
      if (nullable.GetValueOrDefault() && (payment.DocType == "PMT" || payment.DocType == "PPM") && current2 != null)
      {
        nullable = current2.AllowSaveProfile;
        if (nullable.GetValueOrDefault())
          flag = true;
      }
    }
    return flag;
  }

  private bool ProhibitSaveCard(PX.Objects.AR.ARPayment payment)
  {
    bool flag1 = false;
    PX.Objects.CA.PaymentMethod current1 = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current;
    string savePaymentProfiles = ((PXSelectBase<CustomerClass>) this.Base.customerclass).Current?.SavePaymentProfiles;
    CCProcessingCenter current2 = ((PXSelectBase<CCProcessingCenter>) this.Base.processingCenter).Current;
    if (!(savePaymentProfiles == "P"))
    {
      if (current2 != null)
      {
        bool? allowSaveProfile = current2.AllowSaveProfile;
        bool flag2 = false;
        if (!(allowSaveProfile.GetValueOrDefault() == flag2 & allowSaveProfile.HasValue))
          goto label_5;
      }
      else
        goto label_5;
    }
    if ((current1?.PaymentType == "CCD" || current1?.PaymentType == "EFT") && (payment.DocType == "PMT" || payment.DocType == "PPM"))
      flag1 = true;
label_5:
    return flag1;
  }

  protected override void MapViews(ARPaymentEntry graph)
  {
    base.MapViews(graph);
    this.PaymentTransaction = new PXSelectExtension<PaymentTransactionDetail>((PXSelectBase) this.Base.ccProcTran);
    this.ExternalTransaction = new PXSelectExtension<ExternalTransactionDetail>((PXSelectBase) this.Base.ExternalTran);
  }

  [PXUIField]
  [PXProcessButton]
  public override IEnumerable AuthorizeCCPayment(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current == null || ((PXSelectBase<PX.Objects.Extensions.PaymentTransaction.Payment>) this.PaymentDoc).Current == null)
      return adapter.Get();
    this.CalcTax((PXGraph) this.Base, ((PXSelectBase<PX.Objects.Extensions.PaymentTransaction.Payment>) this.PaymentDoc).Current, false);
    return base.AuthorizeCCPayment(adapter);
  }

  [PXUIField]
  [PXProcessButton(DisplayOnMainToolbar = false)]
  public override IEnumerable IncreazeAuthorizedCCPayment(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current == null || ((PXSelectBase<PX.Objects.Extensions.PaymentTransaction.Payment>) this.PaymentDoc).Current == null)
      return adapter.Get();
    this.CalcTax((PXGraph) this.Base, ((PXSelectBase<PX.Objects.Extensions.PaymentTransaction.Payment>) this.PaymentDoc).Current, true);
    return base.IncreazeAuthorizedCCPayment(adapter);
  }

  [PXUIField]
  [PXProcessButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public override IEnumerable CaptureCCPayment(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current == null || ((PXSelectBase<PX.Objects.Extensions.PaymentTransaction.Payment>) this.PaymentDoc).Current == null)
      return adapter.Get();
    this.CalcTax((PXGraph) this.Base, ((PXSelectBase<PX.Objects.Extensions.PaymentTransaction.Payment>) this.PaymentDoc).Current, false);
    return base.CaptureCCPayment(adapter);
  }

  /// <summary>Calc amounts for L2 data.</summary>
  /// <param name="graph">Graph with payment and adjustments</param>
  /// <param name="payment">Payment for L2 calculation</param>
  /// <param name="isIncreasing">If false, all adjustments are retrieved from db, otherwise increased amounts are retrieved from the payment.</param>
  public virtual void CalcTax(PXGraph graph, PX.Objects.Extensions.PaymentTransaction.Payment payment, bool isIncreasing)
  {
    Decimal paymentTax = 0M;
    EnumerableExtensions.ForEach<PXResult<ARAdjust>>((IEnumerable<PXResult<ARAdjust>>) PXSelectBase<ARAdjust, PXSelectJoin<ARAdjust, InnerJoin<PX.Objects.AR.ARInvoice, On<PX.Objects.AR.ARInvoice.docType, Equal<ARAdjust.adjdDocType>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<ARAdjust.adjdRefNbr>>>>, Where<ARAdjust.adjgDocType, Equal<Required<PX.Objects.AR.ARPayment.docType>>, And<ARAdjust.adjgRefNbr, Equal<Required<PX.Objects.AR.ARPayment.refNbr>>, And<ARAdjust.adjdDocType, In3<ARDocType.invoice, ARDocType.finCharge, ARDocType.debitMemo, ARDocType.creditMemo>, And<ARAdjust.voided, NotEqual<True>>>>>>.Config>.Select(graph, new object[2]
    {
      (object) payment.DocType,
      (object) payment.RefNbr
    }), (Action<PXResult<ARAdjust>>) (arApplication =>
    {
      ARAdjust row = PXResult.Unwrap<ARAdjust>((object) arApplication);
      PX.Objects.AR.ARInvoice arInvoice = PXResult.Unwrap<PX.Objects.AR.ARInvoice>((object) arApplication);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = MultiCurrencyCalculator.GetCurrencyInfo<ARAdjust.adjdOrigCuryInfoID>(graph, (object) row);
      if (isIncreasing && arInvoice.DocType == payment.TransactionOrigDocType && arInvoice.RefNbr == payment.TransactionOrigDocRefNbr)
      {
        Decimal? nullable1 = payment.OrigDocAppliedAmount;
        if (currencyInfo.CuryID != payment.CuryID)
        {
          Decimal baseval = MultiCurrencyCalculator.GetCurrencyInfo<SOAdjust.adjgCuryInfoID>(graph, (object) row).CuryConvBase(payment.OrigDocAppliedAmount.GetValueOrDefault());
          nullable1 = new Decimal?(currencyInfo.CuryConvCury(baseval));
        }
        Decimal num = paymentTax;
        Decimal? adjdBalSign = row.AdjdBalSign;
        Decimal? nullable2 = arInvoice.CuryTaxTotal;
        Decimal? nullable3 = adjdBalSign.HasValue & nullable2.HasValue ? new Decimal?(adjdBalSign.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable4 = nullable1;
        Decimal? nullable5 = row.CuryAdjdDiscAmt;
        nullable2 = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable6 = row.CuryAdjdWOAmt;
        Decimal? nullable7;
        if (!(nullable2.HasValue & nullable6.HasValue))
        {
          nullable5 = new Decimal?();
          nullable7 = nullable5;
        }
        else
          nullable7 = new Decimal?(nullable2.GetValueOrDefault() + nullable6.GetValueOrDefault());
        Decimal? nullable8 = nullable7;
        Decimal? nullable9;
        if (!(nullable3.HasValue & nullable8.HasValue))
        {
          nullable6 = new Decimal?();
          nullable9 = nullable6;
        }
        else
          nullable9 = new Decimal?(nullable3.GetValueOrDefault() * nullable8.GetValueOrDefault());
        Decimal? nullable10 = nullable9;
        Decimal? curyOrigDocAmt = arInvoice.CuryOrigDocAmt;
        Decimal? nullable11;
        if (!(nullable10.HasValue & curyOrigDocAmt.HasValue))
        {
          nullable8 = new Decimal?();
          nullable11 = nullable8;
        }
        else
          nullable11 = new Decimal?(nullable10.GetValueOrDefault() / curyOrigDocAmt.GetValueOrDefault());
        nullable8 = nullable11;
        Decimal valueOrDefault = nullable8.GetValueOrDefault();
        paymentTax = num + valueOrDefault;
      }
      else
      {
        Decimal num = paymentTax;
        Decimal? adjdBalSign = row.AdjdBalSign;
        Decimal? nullable12 = arInvoice.CuryTaxTotal;
        Decimal? nullable13 = adjdBalSign.HasValue & nullable12.HasValue ? new Decimal?(adjdBalSign.GetValueOrDefault() * nullable12.GetValueOrDefault()) : new Decimal?();
        Decimal? curyAdjdAmt = row.CuryAdjdAmt;
        Decimal? nullable14 = row.CuryAdjdDiscAmt;
        nullable12 = curyAdjdAmt.HasValue & nullable14.HasValue ? new Decimal?(curyAdjdAmt.GetValueOrDefault() + nullable14.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable15 = row.CuryAdjdWOAmt;
        Decimal? nullable16;
        if (!(nullable12.HasValue & nullable15.HasValue))
        {
          nullable14 = new Decimal?();
          nullable16 = nullable14;
        }
        else
          nullable16 = new Decimal?(nullable12.GetValueOrDefault() + nullable15.GetValueOrDefault());
        Decimal? nullable17 = nullable16;
        Decimal? nullable18;
        if (!(nullable13.HasValue & nullable17.HasValue))
        {
          nullable15 = new Decimal?();
          nullable18 = nullable15;
        }
        else
          nullable18 = new Decimal?(nullable13.GetValueOrDefault() * nullable17.GetValueOrDefault());
        Decimal? nullable19 = nullable18;
        Decimal? curyOrigDocAmt = arInvoice.CuryOrigDocAmt;
        Decimal? nullable20;
        if (!(nullable19.HasValue & curyOrigDocAmt.HasValue))
        {
          nullable17 = new Decimal?();
          nullable20 = nullable17;
        }
        else
          nullable20 = new Decimal?(nullable19.GetValueOrDefault() / curyOrigDocAmt.GetValueOrDefault());
        nullable17 = nullable20;
        Decimal valueOrDefault = nullable17.GetValueOrDefault();
        paymentTax = num + valueOrDefault;
      }
      paymentTax *= currencyInfo.RecipRate ?? 1M;
    }));
    EnumerableExtensions.ForEach<PXResult<SOAdjust>>((IEnumerable<PXResult<SOAdjust>>) PXSelectBase<SOAdjust, PXSelectJoin<SOAdjust, InnerJoin<PX.Objects.SO.SOOrder, On<SOAdjust.FK.Order>>, Where<SOAdjust.adjgDocType, Equal<Required<PX.Objects.AR.ARPayment.docType>>, And<SOAdjust.adjgRefNbr, Equal<Required<PX.Objects.AR.ARPayment.refNbr>>, And<SOAdjust.voided, NotEqual<True>>>>>.Config>.Select(graph, new object[2]
    {
      (object) payment.DocType,
      (object) payment.RefNbr
    }), (Action<PXResult<SOAdjust>>) (soApplication =>
    {
      SOAdjust row = PXResult.Unwrap<SOAdjust>((object) soApplication);
      PX.Objects.SO.SOOrder soOrder = PXResult.Unwrap<PX.Objects.SO.SOOrder>((object) soApplication);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = MultiCurrencyCalculator.GetCurrencyInfo<SOAdjust.adjdOrigCuryInfoID>(graph, (object) row);
      if (isIncreasing && soOrder.OrderType == payment.TransactionOrigDocType && soOrder.OrderNbr == payment.TransactionOrigDocRefNbr)
      {
        Decimal? nullable21 = payment.OrigDocAppliedAmount;
        if (currencyInfo.CuryID != payment.CuryID)
        {
          Decimal baseval = MultiCurrencyCalculator.GetCurrencyInfo<SOAdjust.adjgCuryInfoID>(graph, (object) row).CuryConvBase(payment.OrigDocAppliedAmount.GetValueOrDefault());
          nullable21 = new Decimal?(currencyInfo.CuryConvCury(baseval));
        }
        Decimal num = paymentTax;
        Decimal? curyTaxTotal = soOrder.CuryTaxTotal;
        Decimal? nullable22 = nullable21;
        Decimal? nullable23 = curyTaxTotal.HasValue & nullable22.HasValue ? new Decimal?(curyTaxTotal.GetValueOrDefault() * nullable22.GetValueOrDefault()) : new Decimal?();
        Decimal? curyOrderTotal = soOrder.CuryOrderTotal;
        Decimal? nullable24;
        if (!(nullable23.HasValue & curyOrderTotal.HasValue))
        {
          nullable22 = new Decimal?();
          nullable24 = nullable22;
        }
        else
          nullable24 = new Decimal?(nullable23.GetValueOrDefault() / curyOrderTotal.GetValueOrDefault());
        nullable22 = nullable24;
        Decimal valueOrDefault = nullable22.GetValueOrDefault();
        paymentTax = num + valueOrDefault;
      }
      else
      {
        Decimal num = paymentTax;
        Decimal? curyTaxTotal = soOrder.CuryTaxTotal;
        Decimal? nullable25 = row.CuryAdjdAmt;
        Decimal? nullable26 = curyTaxTotal.HasValue & nullable25.HasValue ? new Decimal?(curyTaxTotal.GetValueOrDefault() * nullable25.GetValueOrDefault()) : new Decimal?();
        Decimal? curyOrderTotal = soOrder.CuryOrderTotal;
        Decimal? nullable27;
        if (!(nullable26.HasValue & curyOrderTotal.HasValue))
        {
          nullable25 = new Decimal?();
          nullable27 = nullable25;
        }
        else
          nullable27 = new Decimal?(nullable26.GetValueOrDefault() / curyOrderTotal.GetValueOrDefault());
        nullable25 = nullable27;
        Decimal valueOrDefault = nullable25.GetValueOrDefault();
        paymentTax = num + valueOrDefault;
      }
      paymentTax *= currencyInfo.RecipRate ?? 1M;
    }));
    Decimal? nullable28 = new Decimal?(Math.Round(paymentTax, 2, MidpointRounding.AwayFromZero));
    payment.Tax = nullable28;
    if (isIncreasing)
    {
      PX.Objects.Extensions.PaymentTransaction.Payment payment1 = payment;
      Decimal? curyDocBalIncrease = payment.CuryDocBalIncrease;
      Decimal? nullable29 = nullable28;
      Decimal? nullable30 = curyDocBalIncrease.HasValue & nullable29.HasValue ? new Decimal?(curyDocBalIncrease.GetValueOrDefault() - nullable29.GetValueOrDefault()) : new Decimal?();
      payment1.SubtotalAmount = nullable30;
    }
    else
    {
      PX.Objects.Extensions.PaymentTransaction.Payment payment2 = payment;
      Decimal? curyDocBal = payment.CuryDocBal;
      Decimal? nullable31 = nullable28;
      Decimal? nullable32 = curyDocBal.HasValue & nullable31.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() - nullable31.GetValueOrDefault()) : new Decimal?();
      payment2.SubtotalAmount = nullable32;
    }
  }

  [PXUIField]
  [PXProcessButton]
  public override IEnumerable CaptureOnlyCCPayment(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current.Released;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        nullable = ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current.IsCCPayment;
        if (nullable.GetValueOrDefault())
        {
          PXFilter<InputPaymentInfo> ccPaymentInfo = this.ccPaymentInfo;
          ARPaymentEntryPaymentTransaction paymentTransaction = this;
          // ISSUE: virtual method pointer
          PXView.InitializePanel initializePanel = new PXView.InitializePanel((object) paymentTransaction, __vmethodptr(paymentTransaction, initAuthCCInfo));
          if (((PXSelectBase<InputPaymentInfo>) ccPaymentInfo).AskExt(initializePanel) == 1)
          {
            this.CalcTax((PXGraph) this.Base, ((PXSelectBase<PX.Objects.Extensions.PaymentTransaction.Payment>) this.PaymentDoc).Current, false);
            return base.CaptureOnlyCCPayment(adapter);
          }
        }
      }
    }
    ((PXSelectBase) this.ccPaymentInfo).View.Clear();
    ((PXSelectBase) this.ccPaymentInfo).Cache.Clear();
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public override IEnumerable RecordCCPayment(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current != null && ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current.IsCCPayment.GetValueOrDefault())
    {
      WebDialogResult webDialogResult = ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).AskExt();
      if (webDialogResult == 1 || ((PXGraph) this.Base).IsContractBasedAPI && webDialogResult == 6)
        return base.RecordCCPayment(adapter);
    }
    ((PXSelectBase) this.InputPmtInfo).View.Clear();
    ((PXSelectBase) this.InputPmtInfo).Cache.Clear();
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable VoidCCPaymentForReAuthorization(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARPaymentEntryPaymentTransaction.\u003C\u003Ec__DisplayClass55_0 cDisplayClass550 = new ARPaymentEntryPaymentTransaction.\u003C\u003Ec__DisplayClass55_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass550.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass550.list = adapter.Get<PX.Objects.AR.ARPayment>().ToList<PX.Objects.AR.ARPayment>();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass550, __methodptr(\u003CVoidCCPaymentForReAuthorization\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass550.list;
  }

  protected virtual void CheckScheduledDateForReauth(ARPaymentEntry paymentGraph, PX.Objects.AR.ARPayment doc)
  {
    CCProcessingCenterPmntMethod centerPmntMethod = PXResultset<CCProcessingCenterPmntMethod>.op_Implicit(((PXSelectBase<CCProcessingCenterPmntMethod>) paymentGraph.ProcessingCenterPmntMethod).Select(Array.Empty<object>()));
    int? reauthDelay = centerPmntMethod.ReauthDelay;
    int num = 0;
    if (!(reauthDelay.GetValueOrDefault() > num & reauthDelay.HasValue))
      return;
    DateTime dateTime1 = PXTimeZoneInfo.Now.AddDays(1.0).AddHours((double) centerPmntMethod.ReauthDelay.Value);
    DateTime? expirationDate = this.GetCustomerPaymentMethodById(doc.PMInstanceID).ExpirationDate;
    DateTime dateTime2 = dateTime1;
    if ((expirationDate.HasValue ? (expirationDate.GetValueOrDefault() < dateTime2 ? 1 : 0) : 0) != 0)
      throw new PXException("The pre-authorization was not voided for reauthorization. The scheduled reauthorization date is later than the expiration date of the credit card.");
  }

  [PXOverride]
  public virtual void VoidCheckProc(PX.Objects.AR.ARPayment doc, Action<PX.Objects.AR.ARPayment> handler)
  {
    handler(doc);
    PX.Objects.AR.ARPayment current = ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current;
    current.TerminalID = (string) null;
    ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Update(current);
  }

  protected virtual bool RefundDocHasValidSharedTran(IEnumerable<IExternalTransaction> trans)
  {
    return ExternalTranHelper.GetSharedTranStatus((PXGraph) this.Base, trans.FirstOrDefault<IExternalTransaction>()) == ExternalTranHelper.SharedTranStatus.Synchronized;
  }

  protected override void BeforeCapturePayment(PX.Objects.AR.ARPayment doc)
  {
    base.BeforeCapturePayment(doc);
    ARPaymentEntry.CheckValidPeriodForCCTran((PXGraph) this.Base, doc);
    this.ReleaseDoc = !doc.Voided.GetValueOrDefault() ? this.NeedRelease(doc) : throw new PXException("The {0} payment has already been voided.", new object[1]
    {
      (object) (doc.DocType + doc.RefNbr)
    });
  }

  protected override void BeforeCreditPayment(PX.Objects.AR.ARPayment doc)
  {
    base.BeforeCapturePayment(doc);
    ARPaymentEntry.CheckValidPeriodForCCTran((PXGraph) this.Base, doc);
    if (((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current?.PaymentType == "POS")
    {
      bool? transactionRefund = doc.CCTransactionRefund;
      bool flag = false;
      if (transactionRefund.GetValueOrDefault() == flag & transactionRefund.HasValue && string.IsNullOrEmpty(doc.TerminalID))
      {
        PXSetPropertyException propertyException = new PXSetPropertyException("Terminal cannot be empty.", (PXErrorLevel) 4);
        ((PXSelectBase) this.Base.Document).Cache.RaiseExceptionHandling<PX.Objects.AR.ARPayment.terminalID>((object) doc, (object) doc.TerminalID, (Exception) propertyException);
        throw propertyException;
      }
    }
    this.ReleaseDoc = this.NeedRelease(doc);
  }

  protected override void BeforeCaptureOnlyPayment(PX.Objects.AR.ARPayment doc)
  {
    base.BeforeCaptureOnlyPayment(doc);
    this.ReleaseDoc = this.NeedRelease(doc);
  }

  protected override void BeforeVoidPayment(PX.Objects.AR.ARPayment doc)
  {
    base.BeforeVoidPayment(doc);
    ICCPayment paymentDoc = this.GetPaymentDoc(doc);
    this.ReleaseDoc = this.NeedRelease(doc) && ARPaymentType.VoidAppl(paymentDoc.DocType);
  }

  protected override AfterProcessingManager GetAfterProcessingManager()
  {
    return (AfterProcessingManager) this.GetARPaymentAfterProcessingManager();
  }

  protected override AfterProcessingManager GetAfterProcessingManager(ARPaymentEntry graph)
  {
    ARPaymentAfterProcessingManager processingManager = this.GetARPaymentAfterProcessingManager();
    processingManager.Graph = graph;
    return (AfterProcessingManager) processingManager;
  }

  protected override PX.Objects.AR.ARPayment SetCurrentDocument(ARPaymentEntry graph, PX.Objects.AR.ARPayment doc)
  {
    PXSelectJoin<PX.Objects.AR.ARPayment, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.AR.ARPayment.customerID>>>, Where<PX.Objects.AR.ARPayment.docType, Equal<Optional<PX.Objects.AR.ARPayment.docType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> document = graph.Document;
    ((PXSelectBase<PX.Objects.AR.ARPayment>) document).Current = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARPayment>) document).Search<PX.Objects.AR.ARPayment.refNbr>((object) doc.RefNbr, new object[1]
    {
      (object) doc.DocType
    }));
    return ((PXSelectBase<PX.Objects.AR.ARPayment>) document).Current;
  }

  protected override PaymentTransactionAcceptFormGraph<ARPaymentEntry, PX.Objects.AR.ARPayment> GetPaymentTransactionAcceptFormExt(
    ARPaymentEntry graph)
  {
    return (PaymentTransactionAcceptFormGraph<ARPaymentEntry, PX.Objects.AR.ARPayment>) ((PXGraph) graph).GetExtension<ARPaymentEntryPaymentTransaction>();
  }

  protected override PaymentTransactionGraph<ARPaymentEntry, PX.Objects.AR.ARPayment> GetPaymentTransactionExt(
    ARPaymentEntry graph)
  {
    return (PaymentTransactionGraph<ARPaymentEntry, PX.Objects.AR.ARPayment>) ((PXGraph) graph).GetExtension<ARPaymentEntryPaymentTransaction>();
  }

  private ARPaymentAfterProcessingManager GetARPaymentAfterProcessingManager()
  {
    return new ARPaymentAfterProcessingManager()
    {
      ReleaseDoc = true,
      RaisedVoidForReAuthorization = this.RaisedVoidForReAuthorization,
      NeedSyncContext = this.IsNeedSyncContext
    };
  }

  protected override PaymentTransactionGraph<ARPaymentEntry, PX.Objects.AR.ARPayment>.PaymentTransactionDetailMapping GetPaymentTransactionMapping()
  {
    return new PaymentTransactionGraph<ARPaymentEntry, PX.Objects.AR.ARPayment>.PaymentTransactionDetailMapping(typeof (CCProcTran));
  }

  protected override PaymentTransactionGraph<ARPaymentEntry, PX.Objects.AR.ARPayment>.PaymentMapping GetPaymentMapping()
  {
    return new PaymentTransactionGraph<ARPaymentEntry, PX.Objects.AR.ARPayment>.PaymentMapping(typeof (PX.Objects.AR.ARPayment));
  }

  protected override PaymentTransactionGraph<ARPaymentEntry, PX.Objects.AR.ARPayment>.ExternalTransactionDetailMapping GetExternalTransactionMapping()
  {
    return new PaymentTransactionGraph<ARPaymentEntry, PX.Objects.AR.ARPayment>.ExternalTransactionDetailMapping(typeof (PX.Objects.AR.ExternalTransaction));
  }

  protected override void SetSyncLock(PX.Objects.AR.ARPayment doc)
  {
    try
    {
      base.SetSyncLock(doc);
      if (doc.SyncLock.GetValueOrDefault())
        return;
      this.CheckSyncLockOnPersist = false;
      PXCache cach = ((PXGraph) this.Base).Caches[typeof (PX.Objects.AR.ARPayment)];
      cach.SetValue<PX.Objects.AR.ARPayment.syncLock>((object) doc, (object) true);
      cach.SetValue<PX.Objects.AR.ARPayment.syncLockReason>((object) doc, (object) "N");
      cach.Update((object) doc);
      ((PXGraph) this.Base).Actions.PressSave();
    }
    finally
    {
      this.CheckSyncLockOnPersist = true;
    }
  }

  protected override void RemoveSyncLock(PX.Objects.AR.ARPayment doc)
  {
    try
    {
      base.RemoveSyncLock(doc);
      if (!doc.SyncLock.GetValueOrDefault())
        return;
      this.CheckSyncLockOnPersist = false;
      PXCache cach = ((PXGraph) this.Base).Caches[typeof (PX.Objects.AR.ARPayment)];
      cach.SetValue<PX.Objects.AR.ARPayment.syncLock>((object) doc, (object) false);
      cach.SetValue<PX.Objects.AR.ARPayment.syncLockReason>((object) doc, (object) null);
      cach.Update((object) doc);
      ((PXGraph) this.Base).Actions.PressSave();
    }
    finally
    {
      this.CheckSyncLockOnPersist = true;
    }
  }

  protected override bool LockExists(PX.Objects.AR.ARPayment doc)
  {
    PX.Objects.AR.ARPayment arPayment = ((PXSelectBase<PX.Objects.AR.ARPayment>) new PXSelectReadonly<PX.Objects.AR.ARPayment, Where<PX.Objects.AR.ARPayment.noteID, Equal<Required<PX.Objects.AR.ARPayment.noteID>>>>((PXGraph) this.Base)).SelectSingle(new object[1]
    {
      (object) doc.NoteID
    });
    return arPayment != null && arPayment.SyncLock.GetValueOrDefault();
  }

  private bool HasProcCenterSupportingUnlinkedMode(PXCache cache, PX.Objects.AR.ARPayment doc)
  {
    return GraphHelper.RowCast<CCProcessingCenter>((IEnumerable) PXSelectorAttribute.SelectAll<PX.Objects.AR.ARPayment.processingCenterID>(cache, (object) doc)).FirstOrDefault<CCProcessingCenter>((Func<CCProcessingCenter, bool>) (i => i.AllowUnlinkedRefund.GetValueOrDefault())) != null;
  }

  private bool NeedRelease(PX.Objects.AR.ARPayment doc)
  {
    bool? released = doc.Released;
    bool flag = false;
    return released.GetValueOrDefault() == flag & released.HasValue && CCProcessingHelper.IntegratedProcessingActivated(((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current);
  }

  private void SetUsingAcceptHostedForm(PX.Objects.AR.ARPayment doc)
  {
    this.SelectedBAccount = (int?) ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current?.BAccountID;
    this.SelectedPaymentMethod = ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current?.PaymentMethodID;
    CCProcessingCenter processingCenter = ((PXSelectBase<CCProcessingCenter>) this.Base.processingCenter).SelectSingle(Array.Empty<object>());
    this.SelectedProcessingCenter = processingCenter?.ProcessingCenterID;
    this.SelectedProcessingCenterType = processingCenter?.ProcessingTypeName;
    this.DocNoteId = (Guid?) ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current?.NoteID;
    this.EnableMobileMode = ((PXGraph) this.Base).IsMobile;
    int? pmInstanceId = doc.PMInstanceID;
    int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
    this.UseAcceptHostedForm = pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue && !ExternalTranHelper.HasSuccessfulTrans((PXSelectBase<PX.Objects.AR.ExternalTransaction>) this.Base.ExternalTran) && string.IsNullOrEmpty(doc.TerminalID);
  }

  private bool GettingDetailsByTranSupported(PX.Objects.AR.ARPayment doc)
  {
    return CCProcessingFeatureHelper.IsFeatureSupported(this.GetProcessingCenterById(doc.ProcessingCenterID), CCProcessingFeature.TransactionGetter, false);
  }

  private void SetVisibilityCreditCardControlsForRefund(PXCache cache, PX.Objects.AR.ARPayment doc)
  {
    bool flag1 = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current?.PaymentType == "POS";
    PX.Objects.AR.ExternalTransaction storedTran = RefTranExtNbrAttribute.GetStoredTran(doc.RefTranExtNbr, (PXGraph) this.Base, cache);
    bool valueOrDefault = doc.CCTransactionRefund.GetValueOrDefault();
    if (storedTran != null)
    {
      int? pmInstanceId = storedTran.PMInstanceID;
      int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
      bool flag2 = pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue;
      PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARPayment.processingCenterID>(cache, (object) doc, false);
      PXUIFieldAttribute.SetVisible<PX.Objects.AR.ARPayment.processingCenterID>(cache, (object) doc, flag2);
      PXUIFieldAttribute.SetVisible<PX.Objects.AR.ARPayment.pMInstanceID>(cache, (object) doc, !flag2);
    }
    else
    {
      int? pmInstanceId = doc.PMInstanceID;
      int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
      bool flag3 = pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue;
      PXUIFieldAttribute.SetVisible<PX.Objects.AR.ARPayment.processingCenterID>(cache, (object) doc, flag3 & valueOrDefault);
      PXUIFieldAttribute.SetVisible<PX.Objects.AR.ARPayment.pMInstanceID>(cache, (object) doc, (!flag3 || !valueOrDefault) && !flag1);
    }
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARPayment.pMInstanceID>(cache, (object) doc, ((PXGraph) this.Base).IsContractBasedAPI || !valueOrDefault);
  }

  private string GetPaymentStateDescr(ExternalTransactionState state)
  {
    return this.GetLastTransactionDescription();
  }

  protected void SetPendingProcessingIfNeeded(PXCache sender, PX.Objects.AR.ARPayment document)
  {
    PX.Objects.CA.PaymentMethod pm = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) new PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>((PXGraph) this.Base)).SelectSingle(new object[1]
    {
      (object) document.PaymentMethodID
    });
    bool flag1 = false;
    if (CCProcessingHelper.PaymentMethodSupportsIntegratedProcessing(pm))
    {
      bool? released = document.Released;
      bool flag2 = false;
      if (released.GetValueOrDefault() == flag2 & released.HasValue)
      {
        if (document.DocType == "RPM")
        {
          IEnumerable<CCProcTran> trans = GraphHelper.RowCast<CCProcTran>((IEnumerable) ((PXSelectBase<CCProcTran>) this.Base.ccProcTran).Select(Array.Empty<object>()));
          IExternalTransaction processedExtTran = ExternalTranHelper.GetLastProcessedExtTran((IEnumerable<IExternalTransaction>) GraphHelper.RowCast<PX.Objects.AR.ExternalTransaction>((IEnumerable) ((PXSelectBase<PX.Objects.AR.ExternalTransaction>) this.Base.ExternalTran).Select(Array.Empty<object>())), (IEnumerable<ICCPaymentTransaction>) trans);
          if (processedExtTran != null && ExternalTranHelper.GetTransactionState((PXGraph) this.Base, processedExtTran).IsActive)
            flag1 = true;
        }
        else
          flag1 = true;
      }
    }
    sender.SetValue<PX.Objects.AR.ARRegister.pendingProcessing>((object) document, (object) flag1);
  }

  protected virtual void CheckProcessingCenter(PXCache cache, PX.Objects.AR.ARPayment doc)
  {
    if (doc == null)
      return;
    PXEntryStatus status = cache.GetStatus((object) doc);
    PX.Objects.CA.PaymentMethod current = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current;
    int? nullable1;
    if (doc != null)
    {
      int? pmInstanceId = doc.PMInstanceID;
      nullable1 = PaymentTranExtConstants.NewPaymentProfile;
      if (!(pmInstanceId.GetValueOrDefault() == nullable1.GetValueOrDefault() & pmInstanceId.HasValue == nullable1.HasValue))
      {
        PX.Objects.AR.CustomerPaymentMethod paymentMethodById = this.GetCustomerPaymentMethodById(doc.PMInstanceID);
        if (paymentMethodById != null && paymentMethodById.CCProcessingCenterID != null)
          doc.ProcessingCenterID = paymentMethodById.CCProcessingCenterID;
      }
    }
    int? nullable2;
    if (doc != null)
    {
      nullable1 = doc.PMInstanceID;
      nullable2 = PaymentTranExtConstants.NewPaymentProfile;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && doc.ProcessingCenterID != null && status == 2 && !GraphHelper.RowCast<CCProcessingCenter>((IEnumerable) PXSelectorAttribute.SelectAll<PX.Objects.AR.ARPayment.processingCenterID>(cache, (object) doc)).Any<CCProcessingCenter>((Func<CCProcessingCenter, bool>) (i => i.ProcessingCenterID == doc.ProcessingCenterID)))
        throw new PXException("'{0}' cannot be found in the system.", new object[1]
        {
          (object) "ProcessingCenterID"
        });
    }
    bool flag1 = doc.DocType == "CRM" || doc.DocType == "SMB";
    bool flag2 = (current?.PaymentType == "CCD" || current?.PaymentType == "EFT") && current != null && current.IsAccountNumberRequired.GetValueOrDefault();
    int num1;
    if (doc.DocType == "REF")
    {
      bool? transactionRefund = doc.CCTransactionRefund;
      bool flag3 = false;
      num1 = transactionRefund.GetValueOrDefault() == flag3 & transactionRefund.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    int num2 = flag2 ? 1 : 0;
    if ((num1 & num2) != 0)
    {
      PXDefaultAttribute.SetPersistingCheck<PX.Objects.AR.ARPayment.pMInstanceID>(cache, (object) doc, (PXPersistingCheck) 1);
    }
    else
    {
      nullable2 = doc.PMInstanceID;
      nullable1 = PaymentTranExtConstants.NewPaymentProfile;
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue & flag2 && !flag1)
      {
        PXDefaultAttribute.SetPersistingCheck<PX.Objects.AR.ARPayment.processingCenterID>(cache, (object) doc, (PXPersistingCheck) 1);
      }
      else
      {
        bool valueOrDefault = ((bool?) ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current?.IsAccountNumberRequired).GetValueOrDefault();
        PXDefaultAttribute.SetPersistingCheck<PX.Objects.AR.ARPayment.pMInstanceID>(cache, (object) doc, !flag1 & valueOrDefault ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
        PXDefaultAttribute.SetPersistingCheck<PX.Objects.AR.ARPayment.processingCenterID>(cache, (object) doc, (PXPersistingCheck) 2);
      }
    }
  }

  private PX.Objects.AR.CustomerPaymentMethod GetCustomerPaymentMethodById(int? pmInstanceId)
  {
    return PX.Objects.AR.CustomerPaymentMethod.PK.Find((PXGraph) this.Base, pmInstanceId);
  }

  private void CheckSyncLock(PX.Objects.AR.ARPayment payment)
  {
    bool flag = ((PXGraph) this.Base).IsContractBasedAPI && ((PXSelectBase) this.Base.Document).Cache.GetStatus((object) payment) == 2 && payment?.SyncLockReason == "V";
    if (!this.CheckSyncLockOnPersist || !payment.SyncLock.GetValueOrDefault() || flag)
      return;
    if (CCProcessingHelper.IntegratedProcessingActivated(((PXSelectBase<PX.Objects.AR.ARSetup>) this.Base.arsetup).Current))
      throw new PXException("The payment is locked for editing. Please wait for the external transaction result.\r\nIf the payment does not get unlocked, click Actions > Validate Card Payment to request the transaction result from the processing center.");
    if (((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Ask("The payment is locked for editing. Please wait for the external transaction result. If the payment does not get unlocked, click Actions > Validate Card Payment to request the transaction result from the processing center. Continue editing?", (MessageButtons) 4) != 6)
      throw new PXException("Operation cancelled.");
    payment.SyncLock = new bool?(false);
  }

  protected virtual void FieldUpdated(PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.paymentMethodID> e)
  {
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.paymentMethodID>>) e).Cache;
    if (!(e.Row is PX.Objects.AR.ARPayment row))
      return;
    cache.SetDefaultExt<PX.Objects.AR.ARPayment.cCTransactionRefund>((object) row);
    cache.SetValueExt<PX.Objects.AR.ARPayment.saveCard>((object) row, (object) false);
    cache.SetValueExt<PX.Objects.AR.ARPayment.processingCenterID>((object) row, (object) null);
    if (row.DocType == "REF")
    {
      cache.SetValueExt<PX.Objects.AR.ARPayment.refTranExtNbr>((object) row, (object) null);
    }
    else
    {
      object obj;
      cache.RaiseFieldDefaulting<PX.Objects.AR.ARPayment.pMInstanceID>((object) row, ref obj);
      if (obj == null)
      {
        PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod)?.Select(Array.Empty<object>()));
        CCProcessingCenter processingCenter = PXResultset<CCProcessingCenter>.op_Implicit(((PXSelectBase<CCProcessingCenter>) this.Base.processingCenter)?.Select(Array.Empty<object>()));
        if (row != null && paymentMethod != null && processingCenter != null && this.Base.ShowCardChck(row))
        {
          cache.SetDefaultExt<PX.Objects.AR.ARPayment.processingCenterID>((object) row);
          if (paymentMethod.PaymentType != "POS" && PXSelectorAttribute.SelectAll<PX.Objects.AR.ARPayment.pMInstanceID>(cache, (object) row).Count == 0)
            cache.SetValuePending<PX.Objects.AR.ARPayment.newCard>((object) row, (object) true);
        }
      }
      cache.SetDefaultExt<PX.Objects.AR.ARPayment.terminalID>((object) row);
    }
    this.SetPendingProcessingIfNeeded(cache, row);
  }

  protected virtual void FieldUpdated(
    PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.processingCenterID> e)
  {
    if (!(e.Row is PX.Objects.AR.ARPayment row))
      return;
    if (row.ProcessingCenterID != null && ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.processingCenterID>>) e).ExternalCall)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.processingCenterID>>) e).Cache.SetValueExt<PX.Objects.AR.ARPayment.pMInstanceID>((object) row, (object) PaymentTranExtConstants.NewPaymentProfile);
    if (!string.IsNullOrEmpty(row.RefTranExtNbr))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.processingCenterID>>) e).Cache.SetDefaultExt<PX.Objects.AR.ARPayment.terminalID>((object) row);
  }

  protected virtual void FieldUpdated(PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.terminalID> e)
  {
    if (!(e.Row is PX.Objects.AR.ARPayment row))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.terminalID>>) e).Cache.SetValue<PX.Objects.AR.ARPayment.cardPresent>((object) row, (object) !string.IsNullOrEmpty(row.TerminalID));
    if (string.IsNullOrEmpty(row.TerminalID) || string.IsNullOrEmpty(row.RefTranExtNbr))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.terminalID>>) e).Cache.SetValueExt<PX.Objects.AR.ARPayment.refTranExtNbr>((object) row, (object) null);
  }

  protected virtual void FieldUpdated(PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.pMInstanceID> e)
  {
    if (!(e.Row is PX.Objects.AR.ARPayment row) || !row.PMInstanceID.HasValue || !((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.pMInstanceID>>) e).ExternalCall)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.pMInstanceID>>) e).Cache.SetValueExt<PX.Objects.AR.ARPayment.processingCenterID>((object) row, (object) null);
  }

  protected virtual void FieldUpdated(PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.branchID> e)
  {
    if (!(e.Row is PX.Objects.AR.ARPayment row) || !row.IsCCPayment.GetValueOrDefault() || !((PXFieldState) ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.branchID>>) e).Cache.GetStateExt<PX.Objects.AR.ARPayment.pMInstanceID>((object) row)).Enabled)
      return;
    PX.Objects.AR.ARPayment copy = (PX.Objects.AR.ARPayment) ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.branchID>>) e).Cache.CreateCopy((object) row);
    copy.BranchID = (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.branchID>, object, object>) e).OldValue;
    object obj;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.branchID>>) e).Cache.RaiseFieldDefaulting<PX.Objects.AR.ARPayment.pMInstanceID>((object) copy, ref obj);
    int? nullable = (int?) obj;
    int? pmInstanceId = row.PMInstanceID;
    if (!(nullable.GetValueOrDefault() == pmInstanceId.GetValueOrDefault() & nullable.HasValue == pmInstanceId.HasValue))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.branchID>>) e).Cache.SetDefaultExt<PX.Objects.AR.ARPayment.processingCenterID>(e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.branchID>>) e).Cache.SetDefaultExt<PX.Objects.AR.ARPayment.pMInstanceID>(e.Row);
  }

  protected virtual void FieldUpdated(PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.newCard> e)
  {
    this.NewCardAccountFieldUpdated(e.Row as PX.Objects.AR.ARPayment, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.newCard>>) e).Cache, e.NewValue as bool?, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.newCard>>) e).ExternalCall);
  }

  protected virtual void FieldUpdated(PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.newAccount> e)
  {
    this.NewCardAccountFieldUpdated(e.Row as PX.Objects.AR.ARPayment, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.newAccount>>) e).Cache, e.NewValue as bool?, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.newAccount>>) e).ExternalCall);
  }

  private void NewCardAccountFieldUpdated(
    PX.Objects.AR.ARPayment payment,
    PXCache cache,
    bool? newValue,
    bool externalCall)
  {
    if (!(payment != null & externalCall))
      return;
    if (newValue.GetValueOrDefault())
    {
      PX.Objects.AR.ExternalTransaction storedTran = RefTranExtNbrAttribute.GetStoredTran(payment.RefTranExtNbr, (PXGraph) this.Base, cache);
      this.EnableNewCardMode(payment, storedTran, cache);
      if (!this.ForceSaveCard(payment))
        return;
      cache.SetValueExt<PX.Objects.AR.ARPayment.saveCard>((object) payment, (object) true);
    }
    else
      this.DisableNewCardMode(payment, cache);
  }

  protected virtual void FieldUpdated(PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.refTranExtNbr> e)
  {
    PX.Objects.AR.ARPayment row = e.Row as PX.Objects.AR.ARPayment;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.refTranExtNbr>>) e).Cache;
    if (row == null)
      return;
    if (!string.IsNullOrEmpty(e.NewValue as string))
    {
      PX.Objects.AR.ExternalTransaction storedTran = RefTranExtNbrAttribute.GetStoredTran(row.RefTranExtNbr, (PXGraph) this.Base, cache);
      if (storedTran != null)
      {
        int? pmInstanceId = storedTran.PMInstanceID;
        int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
        if (!(pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue))
        {
          this.DisableNewCardMode(row, cache);
          cache.SetValueExt<PX.Objects.AR.ARPayment.pMInstanceID>((object) row, (object) storedTran.PMInstanceID);
          goto label_7;
        }
      }
      this.EnableNewCardMode(row, storedTran, cache);
label_7:
      if (string.IsNullOrEmpty(row.TerminalID))
        return;
      cache.SetValueExt<PX.Objects.AR.ARPayment.terminalID>((object) row, (object) null);
    }
    else
      this.EnableNewCardMode(row, (PX.Objects.AR.ExternalTransaction) null, cache);
  }

  protected virtual void FieldUpdated(
    PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.cCTransactionRefund> e)
  {
    PX.Objects.AR.ARPayment row = e.Row as PX.Objects.AR.ARPayment;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARPayment.cCTransactionRefund>>) e).Cache;
    bool? newValue = e.NewValue as bool?;
    if (row == null || !(row.DocType == "REF"))
      return;
    if (newValue.GetValueOrDefault())
    {
      PX.Objects.AR.ExternalTransaction storedTran = RefTranExtNbrAttribute.GetStoredTran(row.RefTranExtNbr, (PXGraph) this.Base, cache);
      this.EnableNewCardMode(row, storedTran, cache);
    }
    else
    {
      cache.SetValueExt<PX.Objects.AR.ARPayment.refTranExtNbr>((object) row, (object) null);
      this.DisableNewCardMode(row, cache);
    }
  }

  protected virtual void FieldVerifying(PX.Data.Events.FieldVerifying<PX.Objects.AR.ARPayment.adjDate> e)
  {
    if (!(e.Row is PX.Objects.AR.ARPayment row) || !((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.AR.ARPayment.adjDate>>) e).ExternalCall)
      return;
    DateTime? adjDate = row.AdjDate;
    if (!adjDate.HasValue)
      return;
    bool? released = row.Released;
    bool flag = false;
    if (!(released.GetValueOrDefault() == flag & released.HasValue))
      return;
    adjDate = row.AdjDate;
    if (adjDate.Value.CompareTo(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.AR.ARPayment.adjDate>, object, object>) e).NewValue) == 0)
      return;
    IExternalTransaction extTran = (IExternalTransaction) ((PXSelectBase<PX.Objects.AR.ExternalTransaction>) this.Base.ExternalTran).SelectSingle(Array.Empty<object>());
    if (extTran == null)
      return;
    ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) this.Base, extTran);
    if (ARPaymentEntryPaymentTransaction.IsDocTypePayment(row) && transactionState.IsSettlementDue)
      throw new PXSetPropertyException("The date in the Application Date box must be the same as the date when the credit card transaction was captured.");
    if (row.DocType == "REF" && transactionState.IsSettlementDue)
      throw new PXSetPropertyException("The date in the Application Date box must be the same as the date when the credit card transaction was voided or refunded.");
    if (row.DocType == "RPM" && (transactionState.IsVoided || transactionState.IsRefunded))
      throw new PXSetPropertyException("The date in the Application Date box must be the same as the date when the credit card transaction was voided or refunded.");
  }

  private void DisableNewCardMode(PX.Objects.AR.ARPayment payment, PXCache cache)
  {
    cache.SetDefaultExt<PX.Objects.AR.ARPayment.pMInstanceID>((object) payment);
    cache.SetValueExt<PX.Objects.AR.ARPayment.saveCard>((object) payment, (object) false);
  }

  private void EnableNewCardMode(PX.Objects.AR.ARPayment payment, PX.Objects.AR.ExternalTransaction extTran, PXCache cache)
  {
    cache.SetValueExt<PX.Objects.AR.ARPayment.pMInstanceID>((object) payment, (object) PaymentTranExtConstants.NewPaymentProfile);
    if (extTran != null)
      cache.SetValueExt<PX.Objects.AR.ARPayment.processingCenterID>((object) payment, (object) extTran.ProcessingCenterID);
    else
      cache.SetDefaultExt<PX.Objects.AR.ARPayment.processingCenterID>((object) payment);
  }

  [PXDBDefault(typeof (PX.Objects.AR.ARRegister.docType))]
  [PXMergeAttributes]
  protected virtual void CCProcTran_DocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBDefault(typeof (PX.Objects.AR.ARRegister.refNbr))]
  [PXMergeAttributes]
  protected virtual void CCProcTran_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBDefault(typeof (PX.Objects.AR.ARRegister.docType))]
  [PXMergeAttributes]
  protected virtual void ExternalTransaction_DocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBDefault(typeof (PX.Objects.AR.ARRegister.refNbr))]
  [PXMergeAttributes]
  protected virtual void ExternalTransaction_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBDefault]
  [PXMergeAttributes]
  protected virtual void ExternalTransaction_VoidDocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBDefault]
  [PXMergeAttributes]
  protected virtual void ExternalTransaction_VoidRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true)]
  [PXDBDefault(typeof (PX.Objects.AR.ARRegister.refNbr))]
  [PXMergeAttributes]
  protected virtual void CCBatchTransaction_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBDefault(typeof (PX.Objects.AR.ExternalTransaction.transactionID))]
  [PXMergeAttributes]
  protected virtual void CCBatchTransaction_TransactionID_CacheAttached(PXCache sender)
  {
  }
}
