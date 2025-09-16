// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CCBatchMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.GraphExtensions;
using PX.Objects.AR.Repositories;
using PX.Objects.AR.Standalone;
using PX.Objects.BQLConstants;
using PX.Objects.CC.Common;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.Extensions.PaymentTransaction;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class CCBatchMaint : PXGraph<CCBatchMaint>
{
  public PXSelect<CCBatch> BatchView;
  public PXSelect<CCBatchStatistics, Where<CCBatchStatistics.batchID, Equal<Current<CCBatch.batchID>>>> CardTypeSummary;
  public PXSelectJoin<CCBatchTransaction, LeftJoin<PX.Objects.AR.ARPayment, On<CCBatchTransaction.docType, Equal<PX.Objects.AR.ARPayment.docType>, And<CCBatchTransaction.refNbr, Equal<PX.Objects.AR.ARPayment.refNbr>>>>, Where<CCBatchTransaction.batchID, Equal<Current<CCBatch.batchID>>>> Transactions;
  public PXSelect<CCBatchTransactionAlias1, Where<CCBatchTransactionAlias1.batchID, Equal<Current<CCBatch.batchID>>, And<CCBatchTransactionAlias1.processingStatus, Equal<CCBatchTranProcessingStatusCode.missing>>>> MissingTransactions;
  public PXSelectJoin<CCBatchTransactionAlias2, InnerJoin<PX.Objects.AR.ARPayment, On<PX.Objects.AR.ARPayment.docType, Equal<CCBatchTransactionAlias2.docType>, And<PX.Objects.AR.ARPayment.refNbr, Equal<CCBatchTransactionAlias2.refNbr>>>, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<Current<CCBatch.processingCenterID>>>>>, Where<CCBatchTransactionAlias2.batchID, Equal<Current<CCBatch.batchID>>, And<Where<PX.Objects.AR.ARPayment.depositAsBatch, Equal<boolFalse>, Or2<Where<PX.Objects.AR.ARPayment.deposited, Equal<boolTrue>, And<Where<PX.Objects.AR.ARPayment.depositNbr, IsNull, Or<IsNull<PX.Objects.AR.ARPayment.depositNbr, EmptyString>, NotEqual<IsNull<Current<CCBatch.depositNbr>, EmptyString>>>>>>, Or<NotExists<Select<CashAccountDeposit, Where<CashAccountDeposit.cashAccountID, Equal<CCProcessingCenter.depositAccountID>, And<CashAccountDeposit.depositAcctID, Equal<PX.Objects.AR.ARPayment.cashAccountID>, And<Where<CashAccountDeposit.paymentMethodID, Equal<PX.Objects.AR.ARPayment.paymentMethodID>, Or<CashAccountDeposit.paymentMethodID, Equal<EmptyString>>>>>>>>>>>>>> ExcludedFromDepositTransactions;
  public PXSelect<PX.Objects.AR.ExternalTransaction> ExternalTransactions;
  public PXSelect<PX.Objects.AR.ARPayment> Payment;
  public PXSelect<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Current<CCBatch.processingCenterID>>>> ProcessingCenter;
  public PXSave<CCBatch> Save;
  public PXCancel<CCBatch> Cancel;
  public PXFirst<CCBatch> First;
  public PXPrevious<CCBatch> Previous;
  public PXNext<CCBatch> Next;
  public PXLast<CCBatch> Last;
  public PXAction<CCBatch> createDeposit;
  public PXAction<CCBatch> record;
  public PXAction<CCBatch> refreshGraph;
  public PXAction<CCBatch> hide;
  public PXAction<CCBatch> unhide;
  public PXAction<CCBatch> repeatMatching;
  public PXAction<CCBatchTransaction> ViewPaymentAll;
  public PXAction<CCBatchTransaction> ViewPaymentExcl;
  private Lazy<ExternalTransactionRepository> _extTranRepo;

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CreateDeposit(PXAdapter adapter)
  {
    if (((PXSelectBase<CCBatch>) this.BatchView).Current != null)
    {
      CCProcessingCenter processingCenter = ((PXSelectBase<CCProcessingCenter>) this.ProcessingCenter).SelectSingle(Array.Empty<object>());
      if (!processingCenter.DepositAccountID.HasValue)
        throw new PXException(PXMessages.LocalizeFormatNoPrefix("To create a bank deposit, specify an active deposit account for the {0} processing center on the Processing Centers (CA205000) form.", new object[1]
        {
          (object) processingCenter.ProcessingCenterID
        }));
      CashAccount cashAccount = CashAccount.PK.Find((PXGraph) this, processingCenter.DepositAccountID);
      if ((cashAccount != null ? (!cashAccount.Active.GetValueOrDefault() ? 1 : 0) : 1) != 0)
        throw new PXException(PXMessages.LocalizeFormatNoPrefix("To create a bank deposit, specify an active deposit account for the {0} processing center on the Processing Centers (CA205000) form.", new object[1]
        {
          (object) processingCenter.ProcessingCenterID
        }));
      this.CreateDepositProc(adapter.MassProcess);
      ((PXAction) this.Save).Press();
    }
    return adapter.Get();
  }

  private List<CCBatchTransactionAlias1> SelectedMissingTransactions(
    int? batchID,
    out bool allSelected)
  {
    allSelected = true;
    List<CCBatchTransactionAlias1> transactionAlias1List = new List<CCBatchTransactionAlias1>();
    foreach (CCBatchTransactionAlias1 transactionAlias1 in ((PXSelectBase) this.MissingTransactions).Cache.Cached)
    {
      int? batchId = transactionAlias1.BatchID;
      int? nullable = batchID;
      if (batchId.GetValueOrDefault() == nullable.GetValueOrDefault() & batchId.HasValue == nullable.HasValue)
      {
        if (transactionAlias1.SelectedToHide.GetValueOrDefault())
          transactionAlias1List.Add(transactionAlias1);
        else
          allSelected = false;
      }
    }
    return transactionAlias1List;
  }

  [PXUIField]
  [PXButton(PopupCommand = "refreshGraph")]
  public virtual IEnumerable Record(PXAdapter adapter)
  {
    CCBatch current1 = ((PXSelectBase<CCBatch>) this.BatchView).Current;
    if (current1 == null)
      return adapter.Get();
    List<CCBatchTransactionAlias1> source = this.SelectedMissingTransactions(current1.BatchID, out bool _);
    if (source.Count<CCBatchTransactionAlias1>() > 1)
      throw new PXException("Please select only one transaction.");
    ((PXAction) this.Save).Press();
    CCBatchTransaction batchTransaction = (CCBatchTransaction) source.First<CCBatchTransactionAlias1>();
    switch (batchTransaction.SettlementStatus)
    {
      case "SSC":
        ARPaymentEntry instance1 = PXGraph.CreateInstance<ARPaymentEntry>();
        this.CreatePayment(instance1, current1, batchTransaction, "PMT");
        PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, "View Document");
        ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException1;
      case "RSS":
        ARPaymentEntry instance2 = PXGraph.CreateInstance<ARPaymentEntry>();
        this.CreatePayment(instance2, current1, batchTransaction, "REF");
        PXRedirectRequiredException requiredException2 = new PXRedirectRequiredException((PXGraph) instance2, "View Document");
        ((PXBaseRedirectException) requiredException2).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException2;
      case "VOI":
      case "REJ":
        string docType = batchTransaction.DocType;
        string refNbr = batchTransaction.RefNbr;
        if (docType == null || refNbr == null)
        {
          PX.Objects.AR.ExternalTransaction externalTransaction = this.GetExternalTransaction(current1.ProcessingCenterID, batchTransaction.PCTranNumber);
          docType = externalTransaction?.DocType;
          refNbr = externalTransaction?.RefNbr;
        }
        PXGraph pxGraph = docType != null && refNbr != null ? this.GetDocumentGraph(docType, refNbr) : throw new PXException("The rejection or voiding of the transaction cannot be recorded because the original payment does not exist in the system. Click Hide to hide the transaction.");
        switch (pxGraph)
        {
          case ARPaymentEntry arPaymentEntry:
            this.ValidateDocumentOnRecord((PX.Objects.AR.ARRegister) ((PXSelectBase<PX.Objects.AR.ARPayment>) arPaymentEntry.Document).Current, batchTransaction);
            using (new SettlementProcessScope(batchTransaction.SettlementStatus))
              arPaymentEntry.VoidCheck(adapter);
            PX.Objects.AR.ARPayment current2 = ((PXSelectBase<PX.Objects.AR.ARPayment>) arPaymentEntry.Document).Current;
            this.SetIsRejection(current2 != null ? PXCacheEx.GetExtension<PX.Objects.Extensions.PaymentTransaction.Payment>((IBqlTable) current2) : (PX.Objects.Extensions.PaymentTransaction.Payment) null, batchTransaction.SettlementStatus);
            break;
          case ARCashSaleEntry arCashSaleEntry:
            this.ValidateDocumentOnRecord((PX.Objects.AR.ARRegister) ((PXSelectBase<ARCashSale>) arCashSaleEntry.Document).Current, batchTransaction);
            using (new SettlementProcessScope(batchTransaction.SettlementStatus))
              arCashSaleEntry.VoidCheck(adapter);
            ARCashSale current3 = ((PXSelectBase<ARCashSale>) arCashSaleEntry.Document).Current;
            this.SetIsRejection(current3 != null ? PXCacheEx.GetExtension<PX.Objects.Extensions.PaymentTransaction.Payment>((IBqlTable) current3) : (PX.Objects.Extensions.PaymentTransaction.Payment) null, batchTransaction.SettlementStatus);
            break;
        }
        PXRedirectRequiredException requiredException3 = new PXRedirectRequiredException(pxGraph, "View Document");
        ((PXBaseRedirectException) requiredException3).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException3;
      default:
        return adapter.Get();
    }
  }

  private void SetIsRejection(PX.Objects.Extensions.PaymentTransaction.Payment payment, string code)
  {
    if (payment == null)
      return;
    payment.IsRejection = new bool?(code == "REJ");
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable RefreshGraph(PXAdapter adapter)
  {
    return ((PXAction) this.Cancel).Press(adapter);
  }

  private void CreatePayment(
    ARPaymentEntry te,
    CCBatch batch,
    CCBatchTransaction batchTran,
    string arDocType)
  {
    InputPaymentInfo current = ((PXSelectBase<InputPaymentInfo>) ((PXGraph) te).GetExtension<ARPaymentEntryPaymentTransaction>()?.InputPmtInfo)?.Current;
    if (current != null)
      current.PCTranNumber = batchTran.PCTranNumber;
    PXCache cach = ((PXGraph) te).Caches[typeof (PX.Objects.AR.ARPayment)];
    PX.Objects.AR.ARPayment arPayment1 = new PX.Objects.AR.ARPayment();
    arPayment1.DocType = arDocType;
    PX.Objects.AR.ARPayment arPayment2 = ((PXSelectBase<PX.Objects.AR.ARPayment>) te.Document).Insert(arPayment1);
    Tuple<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail> withProfileDetail = new CustomerPaymentMethodRepository((PXGraph) this).GetCustomerPaymentMethodWithProfileDetail(batch.ProcessingCenterID, batchTran.PCCustomerID, batchTran.PCPaymentProfileID);
    if (withProfileDetail != null)
    {
      PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod1;
      withProfileDetail.Deconstruct<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail>(out customerPaymentMethod1, out CustomerPaymentMethodDetail _);
      PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod2 = customerPaymentMethod1;
      cach.SetValueExt<PX.Objects.AR.ARPayment.customerID>((object) arPayment2, (object) customerPaymentMethod2.BAccountID);
      cach.SetValueExt<PX.Objects.AR.ARPayment.paymentMethodID>((object) arPayment2, (object) customerPaymentMethod2.PaymentMethodID);
      cach.SetValueExt<PX.Objects.AR.ARPayment.pMInstanceID>((object) arPayment2, (object) customerPaymentMethod2.PMInstanceID);
    }
    else
    {
      arPayment2.NewCard = new bool?(true);
      PX.Objects.AR.CustomerPaymentMethod cpm = this.GetCPM((PXGraph) this, batch.ProcessingCenterID, batchTran.PCCustomerID);
      cach.SetValueExt<PX.Objects.AR.ARPayment.customerID>((object) arPayment2, (object) (int?) cpm?.BAccountID);
    }
    cach.SetValueExt<PX.Objects.AR.ARPayment.processingCenterID>((object) arPayment2, (object) batch.ProcessingCenterID);
    cach.SetValueExt<PX.Objects.AR.ARPayment.curyOrigDocAmt>((object) arPayment2, (object) batchTran.Amount);
  }

  private PX.Objects.AR.CustomerPaymentMethod GetCPM(
    PXGraph graph,
    string processingCenterID,
    string pcCustomerID)
  {
    return ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) new PXSelectReadonly<PX.Objects.AR.CustomerPaymentMethod, Where<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>>, And<PX.Objects.AR.CustomerPaymentMethod.customerCCPID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.customerCCPID>>>>>(graph)).SelectSingle(new object[2]
    {
      (object) processingCenterID,
      (object) pcCustomerID
    });
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Hide(PXAdapter adapter)
  {
    CCBatch current = ((PXSelectBase<CCBatch>) this.BatchView).Current;
    if (current == null)
      return adapter.Get();
    bool allSelected;
    List<CCBatchTransactionAlias1> source = this.SelectedMissingTransactions(current.BatchID, out allSelected);
    if (source.Any<CCBatchTransactionAlias1>() && ((PXSelectBase<CCBatchTransactionAlias1>) this.MissingTransactions).Ask("This transaction will be excluded from further processing. Proceed?", (MessageButtons) 4) == 6)
    {
      foreach (CCBatchTransactionAlias1 transactionAlias1 in source)
      {
        transactionAlias1.ProcessingStatus = "HID";
        transactionAlias1.SelectedToHide = new bool?(false);
        ((PXSelectBase<CCBatchTransactionAlias1>) this.MissingTransactions).Update(transactionAlias1);
      }
      if (allSelected && current.Status == "PRV")
        current.Status = "PRD";
      ((PXGraph) this).Actions.PressSave();
    }
    ((PXSelectBase) this.Transactions).View.Cache.Clear();
    ((PXSelectBase) this.Transactions).View.Cache.ClearQueryCache();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Unhide(PXAdapter adapter)
  {
    CCBatch current = ((PXSelectBase<CCBatch>) this.BatchView).Current;
    if (current == null)
      return adapter.Get();
    List<CCBatchTransaction> source = new List<CCBatchTransaction>();
    foreach (CCBatchTransaction batchTransaction in ((PXSelectBase) this.Transactions).Cache.Cached)
    {
      int? batchId1 = batchTransaction.BatchID;
      int? batchId2 = current.BatchID;
      if (batchId1.GetValueOrDefault() == batchId2.GetValueOrDefault() & batchId1.HasValue == batchId2.HasValue && batchTransaction.SelectedToUnhide.GetValueOrDefault())
        source.Add(batchTransaction);
    }
    if (source.Any<CCBatchTransaction>())
    {
      foreach (CCBatchTransaction batchTransaction in source)
      {
        batchTransaction.ProcessingStatus = "MIS";
        batchTransaction.SelectedToUnhide = new bool?(false);
        ((PXSelectBase<CCBatchTransaction>) this.Transactions).Update(batchTransaction);
      }
      if (current.Status == "PRD")
        current.Status = "PRV";
      ((PXGraph) this).Actions.PressSave();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable RepeatMatching(PXAdapter adapter)
  {
    ((PXSelectBase) this.MissingTransactions).View.Cache.Clear();
    ((PXSelectBase) this.MissingTransactions).View.Cache.ClearQueryCache();
    CCBatch current = ((PXSelectBase<CCBatch>) this.BatchView).Current;
    if (current != null && this.MatchTransactions() && current.Status != "DPD")
    {
      current.Status = "PRD";
      ((PXSelectBase<CCBatch>) this.BatchView).UpdateCurrent();
      ((PXAction) this.Save).Press();
    }
    return adapter.Get();
  }

  private void CreateDepositProc(bool isMassProcess)
  {
    CCBatch current = ((PXSelectBase<CCBatch>) this.BatchView).Current;
    CADepositEntry instance = PXGraph.CreateInstance<CADepositEntry>();
    List<PaymentInfo> paymentInfoList = new List<PaymentInfo>();
    List<KeyValuePair<string, CCBatchTransaction>> source = new List<KeyValuePair<string, CCBatchTransaction>>();
    foreach (PXResult<PX.Objects.CA.Light.ARPayment, CADepositDetail, CADeposit, CashAccountDeposit, PaymentMethod, CCBatchTransaction, CCProcessingCenter, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location> pxResult in ((PXSelectBase<PX.Objects.CA.Light.ARPayment>) new PXSelectJoin<PX.Objects.CA.Light.ARPayment, LeftJoin<CADepositDetail, On<CADepositDetail.origDocType, Equal<PX.Objects.CA.Light.ARPayment.docType>, And<CADepositDetail.origRefNbr, Equal<PX.Objects.CA.Light.ARPayment.refNbr>, And<CADepositDetail.origModule, Equal<BatchModule.moduleAR>, And<CADepositDetail.tranType, Equal<CATranType.cADeposit>>>>>, LeftJoin<CADeposit, On<CADeposit.tranType, Equal<CADepositDetail.tranType>, And<CADeposit.refNbr, Equal<CADepositDetail.refNbr>>>, InnerJoin<CashAccountDeposit, On<CashAccountDeposit.depositAcctID, Equal<PX.Objects.CA.Light.ARPayment.cashAccountID>, And<Where<CashAccountDeposit.paymentMethodID, Equal<PX.Objects.CA.Light.ARPayment.paymentMethodID>, Or<CashAccountDeposit.paymentMethodID, Equal<EmptyString>>>>>, InnerJoin<PaymentMethod, On<PaymentMethod.paymentMethodID, Equal<PX.Objects.CA.Light.ARPayment.paymentMethodID>>, InnerJoin<CCBatchTransaction, On<CCBatchTransaction.docType, Equal<PX.Objects.CA.Light.ARPayment.docType>, And<CCBatchTransaction.refNbr, Equal<PX.Objects.CA.Light.ARPayment.refNbr>>>, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.depositAccountID, Equal<CashAccountDeposit.cashAccountID>>, InnerJoin<PX.Objects.CA.Light.BAccount, On<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.CA.Light.ARPayment.customerID>>, InnerJoin<PX.Objects.CA.Light.Location, On<PX.Objects.CA.Light.Location.locationID, Equal<PX.Objects.CA.Light.ARPayment.customerLocationID>>>>>>>>>>, Where<CCProcessingCenter.processingCenterID, Equal<Required<CCProcessingCenter.processingCenterID>>, And<CCBatchTransaction.batchID, Equal<Required<CCBatchTransaction.batchID>>, And<PX.Objects.CA.Light.ARPayment.depositAsBatch, Equal<boolTrue>, And<PX.Objects.CA.Light.ARPayment.deposited, NotEqual<boolTrue>, And<PX.Objects.CA.Light.ARPayment.depositNbr, IsNull, And<Where<CADepositDetail.refNbr, IsNull, Or<CADeposit.voided, Equal<boolTrue>>>>>>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) current.ProcessingCenterID,
      (object) current.BatchID
    }))
    {
      PX.Objects.CA.Light.ARPayment payment = PXResult<PX.Objects.CA.Light.ARPayment, CADepositDetail, CADeposit, CashAccountDeposit, PaymentMethod, CCBatchTransaction, CCProcessingCenter, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location>.op_Implicit(pxResult);
      CCBatchTransaction batchTran = PXResult<PX.Objects.CA.Light.ARPayment, CADepositDetail, CADeposit, CashAccountDeposit, PaymentMethod, CCBatchTransaction, CCProcessingCenter, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location>.op_Implicit(pxResult);
      ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) this, (IExternalTransaction) this.GetExternalTransaction(current.ProcessingCenterID, batchTran.PCTranNumber));
      if (this.DocumentRequiresDeposit(batchTran, transactionState.IsVoided, current.BatchType))
      {
        bool? released = payment.Released;
        bool flag = false;
        if (released.GetValueOrDefault() == flag & released.HasValue)
          throw new PXException(PXMessages.LocalizeNoPrefix("Unreleased documents cannot be included in a bank deposit. To create a bank deposit, release the documents with the Balanced status included in the settlement batch."));
        if (!paymentInfoList.Any<PaymentInfo>((Func<PaymentInfo, bool>) (p => p.DocType == payment.DocType && p.RefNbr == payment.RefNbr)))
        {
          PX.Objects.CA.Light.BAccount bAccount = PXResult<PX.Objects.CA.Light.ARPayment, CADepositDetail, CADeposit, CashAccountDeposit, PaymentMethod, CCBatchTransaction, CCProcessingCenter, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location>.op_Implicit(pxResult);
          PX.Objects.CA.Light.Location location = PXResult<PX.Objects.CA.Light.ARPayment, CADepositDetail, CADeposit, CashAccountDeposit, PaymentMethod, CCBatchTransaction, CCProcessingCenter, PX.Objects.CA.Light.BAccount, PX.Objects.CA.Light.Location>.op_Implicit(pxResult);
          PaymentInfo paymentInfo = instance.Copy(payment, bAccount, location, new PaymentInfo());
          paymentInfoList.Add(paymentInfo);
        }
      }
    }
    foreach (PXResult<CCBatchTransaction, CCProcessingCenterFeeType> pxResult in ((PXSelectBase<CCBatchTransaction>) new PXSelectJoin<CCBatchTransaction, LeftJoin<CCProcessingCenterFeeType, On<CCProcessingCenterFeeType.processingCenterID, Equal<Required<CCProcessingCenter.processingCenterID>>, And<CCProcessingCenterFeeType.feeType, Equal<CCBatchTransaction.feeType>>>>, Where<CCBatchTransaction.batchID, Equal<Required<CCBatchTransaction.batchID>>, And<CCBatchTransaction.totalFee, NotEqual<decimal0>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) current.ProcessingCenterID,
      (object) current.BatchID
    }))
    {
      CCBatchTransaction batchTransaction = PXResult<CCBatchTransaction, CCProcessingCenterFeeType>.op_Implicit(pxResult);
      CCProcessingCenterFeeType processingCenterFeeType = PXResult<CCBatchTransaction, CCProcessingCenterFeeType>.op_Implicit(pxResult);
      if (processingCenterFeeType.EntryTypeID == null)
        throw new PXException(PXMessages.LocalizeFormatNoPrefix("The {0} fee type is not linked to an entry type. Add the {0} fee type on the Fees tab of the Processing Centers (CA205000) form.", new object[1]
        {
          (object) batchTransaction.FeeType
        }));
      source.Add(new KeyValuePair<string, CCBatchTransaction>(processingCenterFeeType.EntryTypeID, batchTransaction));
    }
    if (!paymentInfoList.Any<PaymentInfo>() && !NonGenericIEnumerableExtensions.Any_((IEnumerable) source))
    {
      Dictionary<WebDialogResult, string> buttonNames = new Dictionary<WebDialogResult, string>()
      {
        {
          (WebDialogResult) 1,
          "Update Status"
        },
        {
          (WebDialogResult) 2,
          "Cancel"
        }
      };
      if (!isMassProcess && ((PXSelectBase) this.BatchView).Ask("Update Batch Status", "There are no payments that can be included in a deposit. Do you want to change the batch status to Deposited to complete the processing?", (MessageButtons) 1, (IReadOnlyDictionary<WebDialogResult, string>) buttonNames) != 1)
        return;
      current.Status = "DPD";
      ((PXSelectBase<CCBatch>) this.BatchView).Update(current);
    }
    else
    {
      CADeposit caDepositHeader = this.CreateCADepositHeader(instance);
      if (caDepositHeader == null)
        return;
      int num = 0;
      if (paymentInfoList.Any<PaymentInfo>())
        num = instance.AddPaymentInfoBatch((IEnumerable<PaymentInfo>) paymentInfoList);
      if (NonGenericIEnumerableExtensions.Any_((IEnumerable) source))
      {
        foreach (IGrouping<string, CCBatchTransaction> grouping in source.GroupBy<KeyValuePair<string, CCBatchTransaction>, string, CCBatchTransaction>((Func<KeyValuePair<string, CCBatchTransaction>, string>) (res => res.Key), (Func<KeyValuePair<string, CCBatchTransaction>, CCBatchTransaction>) (res => res.Value), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
        {
          string key = grouping.Key;
          CADepositCharge caDepositCharge1 = new CADepositCharge()
          {
            EntryTypeID = key,
            CuryChargeableAmt = new Decimal?(0M),
            CuryChargeAmt = new Decimal?(0M)
          };
          foreach (CCBatchTransaction batchTransaction in (IEnumerable<CCBatchTransaction>) grouping)
          {
            CADepositCharge caDepositCharge2 = caDepositCharge1;
            Decimal? nullable1 = caDepositCharge2.CuryChargeableAmt;
            Decimal? nullable2 = batchTransaction.Amount;
            caDepositCharge2.CuryChargeableAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
            CADepositCharge caDepositCharge3 = caDepositCharge1;
            nullable2 = caDepositCharge3.CuryChargeAmt;
            nullable1 = batchTransaction.TotalFee;
            caDepositCharge3.CuryChargeAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
          }
          ((PXSelectBase<CADepositCharge>) instance.Charges).Insert(caDepositCharge1);
        }
      }
      ((PXSelectBase<CADeposit>) instance.Document).SetValueExt<CADeposit.curyControlAmt>(caDepositHeader, (object) caDepositHeader.CuryTranAmt);
      ((PXSelectBase<CADeposit>) instance.Document).Update(caDepositHeader);
      ((PXAction) instance.Save).Press();
      current.DepositType = ((PXSelectBase<CADeposit>) instance.Document).Current.TranType;
      current.DepositNbr = ((PXSelectBase<CADeposit>) instance.Document).Current.RefNbr;
      if (paymentInfoList.Count == num)
        current.Status = "DPD";
      ((PXSelectBase<CCBatch>) this.BatchView).Update(current);
    }
  }

  private bool DocumentRequiresDeposit(
    CCBatchTransaction batchTran,
    bool isVoidedPayment,
    string batchType)
  {
    switch (batchTran.SettlementStatus)
    {
      case "SSC":
        return !(batchType == "ACS") || !isVoidedPayment;
      case "RSS":
        return true;
      case "REJ":
        return batchTran.DocType == "RCS";
      default:
        return false;
    }
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2024R2.", true)]
  public static PXGraph CreateGraph() => (PXGraph) PXGraph.CreateInstance<CCBatchMaint>();

  private CADeposit CreateCADepositHeader(CADepositEntry graph)
  {
    CCBatch current = ((PXSelectBase<CCBatch>) this.BatchView).Current;
    string processingCenterId1 = current.ProcessingCenterID;
    DateTime date1 = current.SettlementTime.Value;
    date1 = date1.Date;
    string shortDateString1 = date1.ToShortDateString();
    string str1 = $"{processingCenterId1}.{shortDateString1}";
    CCProcessingCenter processingCenter = ((PXSelectBase<CCProcessingCenter>) this.ProcessingCenter).SelectSingle(Array.Empty<object>());
    CADeposit caDeposit1 = new CADeposit();
    caDeposit1.TranType = "CDT";
    caDeposit1.CashAccountID = processingCenter.DepositAccountID;
    caDeposit1.TranDate = new DateTime?(current.SettlementTime.Value.Date);
    CADeposit caDeposit2 = caDeposit1;
    string processingCenterId2 = current.ProcessingCenterID;
    DateTime date2 = current.SettlementTime.Value;
    date2 = date2.Date;
    string shortDateString2 = date2.ToShortDateString();
    string str2 = $"{processingCenterId2}.{shortDateString2}";
    caDeposit2.ExtRefNbr = str2;
    caDeposit1.TranDesc = PXMessages.LocalizeFormatNoPrefix("Deposit of settlement batch from {0}", new object[1]
    {
      (object) str1
    });
    CADeposit caDeposit3 = caDeposit1;
    return ((PXSelectBase<CADeposit>) graph.Document).Insert(caDeposit3);
  }

  public bool MatchTransactions()
  {
    CCBatch current = ((PXSelectBase<CCBatch>) this.BatchView).Current;
    bool flag = true;
    Dictionary<string, Exception> source = new Dictionary<string, Exception>((IEqualityComparer<string>) StringComparer.Ordinal);
    foreach (PXResult<CCBatchTransaction> batchTransaction in this.GetBatchTransactions(current.BatchID, "MIS", "PPR"))
    {
      CCBatchTransaction batchTran = PXResult<CCBatchTransaction>.op_Implicit(batchTransaction);
      try
      {
        ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
        PX.Objects.AR.ARPayment payment = (PX.Objects.AR.ARPayment) null;
        PX.Objects.AR.ExternalTransaction externalTransaction1 = this.GetExternalTransaction(current.ProcessingCenterID, batchTran.PCTranNumber);
        if (externalTransaction1 != null)
        {
          payment = CCBatchMaint.FindARPayment((PXGraph) instance, externalTransaction1.DocType, externalTransaction1.RefNbr, batchTran.SettlementStatus);
        }
        else
        {
          TransactionData tranDetails = this.GetTranDetails(current.ProcessingCenterID, batchTran.PCTranNumber);
          externalTransaction1 = this.GetExternalTransactionByNoteID((Guid?) tranDetails?.TranUID);
          if (externalTransaction1 != null && string.IsNullOrEmpty(externalTransaction1.TranNumber))
          {
            externalTransaction1.TranNumber = batchTran.PCTranNumber;
            ((PXSelectBase<PX.Objects.AR.ExternalTransaction>) this.ExternalTransactions).Update(externalTransaction1);
            ((PXGraph) this).Actions.PressSave();
            payment = CCBatchMaint.FindARPayment((PXGraph) instance, externalTransaction1.DocType, externalTransaction1.RefNbr, batchTran.SettlementStatus);
            if (payment != null && payment.DocType != "REF" && (!(payment.DocType == "RPM") || !(batchTran.SettlementStatus == "RSS")))
            {
              ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current = payment;
              ARPaymentEntryPaymentTransaction extension = ((PXGraph) instance).GetExtension<ARPaymentEntryPaymentTransaction>();
              extension.CheckAndRecordTransaction(PXCacheEx.GetExtension<ExternalTransactionDetail>((IBqlTable) externalTransaction1), tranDetails);
              PX.Objects.AR.ExternalTransaction externalTransaction2 = GraphHelper.RowCast<ExternalTransactionDetail>((IEnumerable) ((PXSelectBase<ExternalTransactionDetail>) extension.ExternalTransaction).Select(Array.Empty<object>())).FirstOrDefault<ExternalTransactionDetail>((Func<ExternalTransactionDetail, bool>) (tran => tran.TranNumber == batchTran.PCTranNumber))?.Base as PX.Objects.AR.ExternalTransaction;
              flag &= this.HandleExternalUpdate(batchTran);
              continue;
            }
          }
          this.PickValuablesFromTransactionData(tranDetails, batchTran);
        }
        if (externalTransaction1 == null || payment == null)
        {
          flag &= this.ProcessTranWithMissingDocument(batchTran);
        }
        else
        {
          CCBatchMaint.StatusMatchingResult matchingResult = CCBatchMaint.MatchStatuses(batchTran.SettlementStatus, externalTransaction1, (PX.Objects.AR.ARRegister) payment, (PXGraph) instance);
          switch (matchingResult)
          {
            case CCBatchMaint.StatusMatchingResult.NoMatch:
              this.ValidateTransaction(externalTransaction1);
              flag &= this.HandleExternalUpdate(batchTran);
              current = ((PXSelectBase<CCBatch>) this.BatchView).Current;
              continue;
            case CCBatchMaint.StatusMatchingResult.VoidPaymentWithoutVoidTransaction:
              try
              {
                this.VoidCardPayment(instance, payment);
                break;
              }
              catch
              {
                matchingResult = CCBatchMaint.StatusMatchingResult.NoMatch;
                break;
              }
          }
          flag &= this.UpdateRelatedRecords(current, batchTran, externalTransaction1, payment, matchingResult, instance);
        }
      }
      catch (Exception ex)
      {
        source[batchTran.PCTranNumber] = ex;
      }
    }
    if (source.Any<KeyValuePair<string, Exception>>())
      throw new PXException(string.Join(Environment.NewLine, source.Select<KeyValuePair<string, Exception>, string>((Func<KeyValuePair<string, Exception>, string>) (e => $"{Prefix(e.Key)} {e.Value.Message}"))));
    return flag;

    static string Prefix(string id) => $"An error occurred while processing the {id} transaction.";
  }

  private bool HandleExternalUpdate(CCBatchTransaction batchTran)
  {
    int? batchId = ((PXSelectBase<CCBatch>) this.BatchView).Current.BatchID;
    ((PXSelectBase) this.BatchView).Cache.Clear();
    ((PXSelectBase) this.BatchView).Cache.ClearQueryCache();
    ((PXSelectBase<CCBatch>) this.BatchView).Current = CCBatch.PK.Find((PXGraph) this, batchId);
    ((PXGraph) this).SelectTimeStamp();
    if (!(((CCBatchTransaction) PrimaryKeyOf<CCBatchTransaction>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<CCBatchTransaction.batchID, CCBatchTransaction.pCTranNumber>>.Find((PXGraph) this, (TypeArrayOf<IBqlField>.IFilledWith<CCBatchTransaction.batchID, CCBatchTransaction.pCTranNumber>) batchTran, (PKFindOptions) 0))?.ProcessingStatus != "PRD"))
      return true;
    this.SetBatchTranStatus(batchTran, "MIS");
    return false;
  }

  internal static CCBatchMaint.StatusMatchingResult MatchStatuses(
    string batchTranSettlementStatus,
    PX.Objects.AR.ExternalTransaction extTran,
    PX.Objects.AR.ARRegister payment,
    PXGraph graph)
  {
    if (CCBatchMaint.IsMatchUnsupported(batchTranSettlementStatus))
      return CCBatchMaint.StatusMatchingResult.NoMatch;
    string procStatus = extTran.ProcStatus;
    if (batchTranSettlementStatus != null && batchTranSettlementStatus.Length == 3)
    {
      switch (batchTranSettlementStatus[2])
      {
        case 'C':
          switch (batchTranSettlementStatus)
          {
            case "SSC":
              if (procStatus == "CAS")
                return CCBatchMaint.StatusMatchingResult.SuccessMatch;
              if (EnumerableExtensions.IsIn<string>(procStatus, "VDF", "VDD"))
              {
                ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState(graph, (IExternalTransaction) extTran);
                if (transactionState.IsCaptured || transactionState.IsRefunded)
                  return CCBatchMaint.StatusMatchingResult.SuccessMatch;
                goto label_27;
              }
              goto label_27;
            case "DEC":
              if (EnumerableExtensions.IsIn<string>(procStatus, "AUD", "CAD", "CDD"))
                return CCBatchMaint.StatusMatchingResult.Match;
              goto label_27;
            default:
              goto label_27;
          }
        case 'I':
          if (batchTranSettlementStatus == "VOI")
            break;
          goto label_27;
        case 'J':
          if (batchTranSettlementStatus == "REJ")
            break;
          goto label_27;
        case 'P':
          if (batchTranSettlementStatus == "EXP" && EnumerableExtensions.IsIn<string>(procStatus, "AUE", "CAE"))
            return CCBatchMaint.StatusMatchingResult.Match;
          goto label_27;
        case 'R':
          if (batchTranSettlementStatus == "ERR" && EnumerableExtensions.IsIn<string>(procStatus, "AUF", "AIF", "CAF"))
            return CCBatchMaint.StatusMatchingResult.Match;
          goto label_27;
        case 'S':
          if (batchTranSettlementStatus == "RSS" && procStatus == "CDS")
            return CCBatchMaint.StatusMatchingResult.SuccessMatch;
          goto label_27;
        default:
          goto label_27;
      }
      ExternalTransactionState transactionState1 = ExternalTranHelper.GetTransactionState(graph, (IExternalTransaction) extTran);
      return EnumerableExtensions.IsIn<string>(payment.DocType, "RPM", "RCS") ? (!transactionState1.IsCaptured ? CCBatchMaint.StatusMatchingResult.Match : CCBatchMaint.StatusMatchingResult.VoidPaymentWithoutVoidTransaction) : (!transactionState1.IsCaptured || transactionState1.IsOpenForReview ? CCBatchMaint.StatusMatchingResult.Match : CCBatchMaint.StatusMatchingResult.NoMatchSkipValidation);
    }
label_27:
    return CCBatchMaint.StatusMatchingResult.NoMatch;
  }

  private static bool IsMatchUnsupported(string batchTranSettlementStatus)
  {
    return EnumerableExtensions.IsIn<string>(batchTranSettlementStatus, "RRJ", "RVO");
  }

  private static PX.Objects.AR.ARPayment FindARPayment(
    PXGraph graph,
    string docType,
    string refNbr,
    string settlementStatus)
  {
    if (EnumerableExtensions.IsIn<string>(settlementStatus, "VOI", "RSS", "REJ", "RRJ"))
    {
      PX.Objects.AR.Standalone.ARRegister arRegister = PXResultset<PX.Objects.AR.Standalone.ARRegister>.op_Implicit(PXSelectBase<PX.Objects.AR.Standalone.ARRegister, PXSelect<PX.Objects.AR.Standalone.ARRegister, Where<PX.Objects.AR.ARRegister.origDocType, Equal<Required<PX.Objects.AR.ARRegister.origDocType>>, And<PX.Objects.AR.ARRegister.origRefNbr, Equal<Required<PX.Objects.AR.ARRegister.origRefNbr>>, And<PX.Objects.AR.Standalone.ARRegister.docType, In3<ARDocType.voidPayment, ARDocType.cashReturn>>>>>.Config>.Select(graph, new object[2]
      {
        (object) docType,
        (object) refNbr
      }));
      if (arRegister != null)
        return PX.Objects.AR.ARPayment.PK.Find(graph, arRegister.DocType, arRegister.RefNbr);
    }
    return PX.Objects.AR.ARPayment.PK.Find(graph, docType, refNbr);
  }

  private void SetBatchTranStatus(CCBatchTransaction batchTran, string processingStatus)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      batchTran.ProcessingStatus = processingStatus;
      ((PXSelectBase<CCBatchTransaction>) this.Transactions).Update(batchTran);
      ((PXGraph) this).Actions.PressSave();
      transactionScope.Complete();
    }
  }

  private bool UpdateRelatedRecords(
    CCBatch ccBatch,
    CCBatchTransaction batchTran,
    PX.Objects.AR.ExternalTransaction extTran,
    PX.Objects.AR.ARPayment payment,
    CCBatchMaint.StatusMatchingResult matchingResult,
    ARPaymentEntry paymentGraph)
  {
    bool flag = true;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (matchingResult != CCBatchMaint.StatusMatchingResult.NoMatchSkipValidation)
      {
        batchTran.TransactionID = extTran.TransactionID;
        batchTran.OriginalStatus = extTran.ProcStatus;
        batchTran.DocType = payment.DocType;
        batchTran.RefNbr = payment.RefNbr;
      }
      if (matchingResult == CCBatchMaint.StatusMatchingResult.NoMatch || matchingResult == CCBatchMaint.StatusMatchingResult.NoMatchSkipValidation)
      {
        batchTran.ProcessingStatus = "MIS";
        flag = false;
      }
      else
      {
        batchTran.CurrentStatus = batchTran.OriginalStatus;
        batchTran.ProcessingStatus = "PRD";
        if (matchingResult == CCBatchMaint.StatusMatchingResult.SuccessMatch)
        {
          extTran.Settled = new bool?(true);
          ((PXSelectBase<PX.Objects.AR.ExternalTransaction>) this.ExternalTransactions).Update(extTran);
          this.MarkPaymentSettled(paymentGraph, payment, ccBatch);
        }
      }
      ((PXSelectBase<CCBatchTransaction>) this.Transactions).Update(batchTran);
      ((PXSelectBase<CCBatch>) this.BatchView).Update(ccBatch);
      ((PXGraph) this).Actions.PressSave();
      transactionScope.Complete();
    }
    return flag;
  }

  private void MarkPaymentSettled(ARPaymentEntry paymentGraph, PX.Objects.AR.ARPayment payment, CCBatch ccBatch)
  {
    if (new ARCashSaleType.ListAttribute().ValueLabelDic.ContainsKey(payment.DocType))
    {
      ARCashSaleEntry instance = PXGraph.CreateInstance<ARCashSaleEntry>();
      ARCashSale arCashSale = ARCashSale.PK.Find((PXGraph) instance, payment.DocType, payment.RefNbr);
      arCashSale.Settled = new bool?(true);
      arCashSale.Cleared = new bool?(true);
      arCashSale.ClearDate = ccBatch.SettlementTime;
      ((PXGraph) instance).Caches[typeof (ARCashSale)].Update((object) arCashSale);
      ((PXAction) instance.Save).Press();
    }
    else
    {
      payment = (PX.Objects.AR.ARPayment) PrimaryKeyOf<PX.Objects.AR.ARPayment>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.AR.ARPayment.docType, PX.Objects.AR.ARPayment.refNbr>>.Find((PXGraph) paymentGraph, (TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.AR.ARPayment.docType, PX.Objects.AR.ARPayment.refNbr>) payment, (PKFindOptions) 0);
      payment.Settled = new bool?(true);
      payment.Cleared = new bool?(true);
      payment.ClearDate = ccBatch.SettlementTime;
      ((PXGraph) paymentGraph).Caches[typeof (PX.Objects.AR.ARPayment)].Update((object) payment);
      ((PXAction) paymentGraph.Save).Press();
    }
  }

  private bool ProcessTranWithMissingDocument(CCBatchTransaction batchTran)
  {
    if (EnumerableExtensions.IsIn<string>(batchTran.SettlementStatus, "DEC", "EXP", "ERR"))
    {
      this.SetBatchTranStatus(batchTran, "PRD");
      return true;
    }
    this.SetBatchTranStatus(batchTran, "MIS");
    return false;
  }

  private void ValidateTransaction(PX.Objects.AR.ExternalTransaction transaction)
  {
    ExternalTransactionValidation.ValidateCCPayment((PXGraph) this, new List<IExternalTransaction>()
    {
      (IExternalTransaction) transaction
    }, false);
  }

  private void VoidCardPayment(ARPaymentEntry graph, PX.Objects.AR.ARPayment payment)
  {
    ((PXSelectBase<PX.Objects.AR.ARPayment>) graph.Document).Current = payment;
    ((PXAction) ((PXGraph) graph).GetExtension<ARPaymentEntryPaymentTransaction>().voidCCPayment).Press();
  }

  protected TransactionData GetTranDetails(string procCenterId, string transactionId)
  {
    return new PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing((PXGraph) this).GetTransactionById(transactionId, procCenterId);
  }

  private void PickValuablesFromTransactionData(
    TransactionData tranData,
    CCBatchTransaction batchTran)
  {
    if (tranData == null || batchTran.PCCustomerID != null)
      return;
    batchTran.PCCustomerID = tranData.CustomerId;
    batchTran.PCPaymentProfileID = tranData.PaymentId;
    ((PXSelectBase<CCBatchTransaction>) this.Transactions).Update(batchTran);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewPaymentAll(PXAdapter adapter)
  {
    return this.ViewPayment(adapter, ((PXSelectBase<CCBatchTransaction>) this.Transactions).Current);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewPaymentExcl(PXAdapter adapter)
  {
    return this.ViewPayment(adapter, (CCBatchTransaction) ((PXSelectBase<CCBatchTransactionAlias2>) this.ExcludedFromDepositTransactions).Current);
  }

  private IEnumerable ViewPayment(PXAdapter adapter, CCBatchTransaction batchTransaction)
  {
    if (batchTransaction != null)
    {
      PXGraph documentGraph = this.GetDocumentGraph(batchTransaction.DocType, batchTransaction.RefNbr);
      if (documentGraph != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException(documentGraph, true, "View Document");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  private void ValidateDocumentOnRecord(PX.Objects.AR.ARRegister doc, CCBatchTransaction tran)
  {
    if (tran.SettlementStatus == "REJ" && (doc?.DocType == "RPM" || doc?.DocType == "RCS") && doc.Status == "C")
      throw new PXException("The rejection of the transaction cannot be recorded because the {0} document with the {1} ref. number is already voided or refunded in the system. Click Hide to hide the transaction.", new object[2]
      {
        (object) ARDocType.GetDisplayName(doc.OrigDocType),
        (object) doc.OrigRefNbr
      });
  }

  private PXGraph GetDocumentGraph(string docType, string refNbr)
  {
    if (new ARPaymentType.ListAttribute().ValueLabelDic.ContainsKey(docType))
      return (PXGraph) this.GetARPaymentGraph(docType, refNbr);
    return new ARCashSaleType.ListAttribute().ValueLabelDic.ContainsKey(docType) ? (PXGraph) this.GetCashSaleGraph(docType, refNbr) : throw new PXException("The document cannot be processed because the document type is unknown.");
  }

  private ARCashSaleEntry GetCashSaleGraph(string docType, string refNbr)
  {
    ARCashSale arCashSale = ARCashSale.PK.Find((PXGraph) this, docType, refNbr);
    if (arCashSale == null)
      return (ARCashSaleEntry) null;
    ARCashSaleEntry instance = PXGraph.CreateInstance<ARCashSaleEntry>();
    ((PXSelectBase<ARCashSale>) instance.Document).Current = arCashSale;
    return instance;
  }

  private ARPaymentEntry GetARPaymentGraph(string docType, string refNbr)
  {
    PX.Objects.AR.ARPayment arPayment = PX.Objects.AR.ARPayment.PK.Find((PXGraph) this, docType, refNbr);
    if (arPayment == null)
      return (ARPaymentEntry) null;
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current = arPayment;
    return instance;
  }

  protected void _(PX.Data.Events.RowSelected<CCBatch> e)
  {
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CCBatch>>) e).Cache;
    CCBatch row = e.Row;
    if (row == null)
      return;
    ((PXAction) this.createDeposit).SetEnabled(row.Status == "PRD" && string.IsNullOrEmpty(row.DepositNbr));
    bool flag1 = row.Status == "DPD";
    bool flag2 = GraphHelper.RowCast<CCBatchTransactionAlias1>(((PXSelectBase) this.MissingTransactions).Cache.Cached).Any<CCBatchTransactionAlias1>((Func<CCBatchTransactionAlias1, bool>) (tran =>
    {
      int? batchId1 = (int?) tran?.BatchID;
      int? batchId2 = row.BatchID;
      return batchId1.GetValueOrDefault() == batchId2.GetValueOrDefault() & batchId1.HasValue == batchId2.HasValue && tran != null && tran.SelectedToHide.GetValueOrDefault();
    }));
    bool flag3 = flag2 && GraphHelper.RowCast<CCBatchTransactionAlias1>(((PXSelectBase) this.MissingTransactions).Cache.Cached).Any<CCBatchTransactionAlias1>((Func<CCBatchTransactionAlias1, bool>) (tran =>
    {
      int? batchId3 = (int?) tran?.BatchID;
      int? batchId4 = row.BatchID;
      return batchId3.GetValueOrDefault() == batchId4.GetValueOrDefault() & batchId3.HasValue == batchId4.HasValue && tran != null && tran.SelectedToHide.GetValueOrDefault() && CCBatchMaint.IsMatchUnsupported(tran.SettlementStatus);
    }));
    ((PXAction) this.hide).SetEnabled(!flag1 & flag2);
    ((PXAction) this.record).SetEnabled(!flag1 & flag2 && !flag3);
    bool flag4 = GraphHelper.RowCast<CCBatchTransaction>(((PXSelectBase) this.Transactions).Cache.Cached).Any<CCBatchTransaction>((Func<CCBatchTransaction, bool>) (tran =>
    {
      int? batchId5 = (int?) tran?.BatchID;
      int? batchId6 = row.BatchID;
      return batchId5.GetValueOrDefault() == batchId6.GetValueOrDefault() & batchId5.HasValue == batchId6.HasValue && tran != null && tran.SelectedToUnhide.GetValueOrDefault();
    }));
    ((PXAction) this.unhide).SetEnabled(!flag1 & flag4);
    ((PXAction) this.repeatMatching).SetEnabled(!flag1);
    bool flag5 = row.BatchType == "CCS";
    PXUIVisibility pxuiVisibility = flag5 ? (PXUIVisibility) 1 : (PXUIVisibility) 3;
    PXUIFieldAttribute.SetVisible<CCBatch.rejectedAmount>(cache, (object) row, !flag5);
    PXUIFieldAttribute.SetVisible<CCBatch.rejectedCount>(cache, (object) row, !flag5);
    PXUIFieldAttribute.SetVisible<CCBatchStatistics.rejectedAmount>(((PXSelectBase) this.CardTypeSummary).Cache, (object) null, !flag5);
    PXUIFieldAttribute.SetVisible<CCBatchStatistics.rejectedCount>(((PXSelectBase) this.CardTypeSummary).Cache, (object) null, !flag5);
    PXUIFieldAttribute.SetVisibility<CCBatchStatistics.rejectedAmount>(((PXSelectBase) this.CardTypeSummary).Cache, (object) null, pxuiVisibility);
    PXUIFieldAttribute.SetVisibility<CCBatchStatistics.rejectedCount>(((PXSelectBase) this.CardTypeSummary).Cache, (object) null, pxuiVisibility);
  }

  protected virtual void _(PX.Data.Events.FieldSelecting<CCBatch.excludedCount> e)
  {
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<CCBatch.excludedCount>>) e).ReturnValue = (object) ((PXSelectBase<CCBatchTransactionAlias2>) this.ExcludedFromDepositTransactions).Select(Array.Empty<object>()).Count;
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Imported Count")]
  protected void _(
    PX.Data.Events.CacheAttached<CCBatch.importedTransactionCount> _)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Missing Count")]
  protected void _(PX.Data.Events.CacheAttached<CCBatch.missingCount> _)
  {
  }

  protected void _(PX.Data.Events.RowSelected<CCBatchTransaction> e)
  {
    CCBatchTransaction row = e.Row;
    if (row == null)
      return;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CCBatchTransaction>>) e).Cache;
    PXUIFieldAttribute.SetEnabled<CCBatchTransaction.selectedToUnhide>(cache, (object) row, row.ProcessingStatus == "HID");
    PX.Objects.AR.ARPayment arPayment = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARPayment>) this.Payment).Search<PX.Objects.AR.ARPayment.docType, PX.Objects.AR.ARPayment.refNbr>((object) row.DocType, (object) row.RefNbr, Array.Empty<object>()));
    UIState.RaiseOrHideError<CCBatchTransaction.pCTranNumber>(cache, (object) row, arPayment?.Status == "B", "Unreleased documents cannot be included in a bank deposit. Release the document to create a bank deposit.", (PXErrorLevel) 3);
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Bank Deposit")]
  protected void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARPayment.depositNbr> _)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Doc. Status")]
  protected void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARPayment.status> _)
  {
  }

  private ExternalTransactionRepository ExtTranRepo => this._extTranRepo.Value;

  public CCBatchMaint()
  {
    this._extTranRepo = new Lazy<ExternalTransactionRepository>((Func<ExternalTransactionRepository>) (() => new ExternalTransactionRepository((PXGraph) this)));
    ((PXAction) this.Save).SetVisible(false);
    ((PXAction) this.Cancel).SetVisible(false);
    ((PXSelectBase) this.BatchView).Cache.AllowInsert = false;
    ((PXSelectBase) this.BatchView).Cache.AllowDelete = false;
  }

  private PXResultset<CCBatchTransaction> GetBatchTransactions(
    int? batchID,
    params string[] procStatuses)
  {
    return PXSelectBase<CCBatchTransaction, PXSelect<CCBatchTransaction, Where<CCBatchTransaction.batchID, Equal<Required<CCBatch.batchID>>, And<CCBatchTransaction.processingStatus, In<Required<CCBatchTransaction.processingStatus>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) batchID,
      (object) procStatuses
    });
  }

  private PX.Objects.AR.ExternalTransaction GetExternalTransaction(
    string cCProcessingCenterID,
    string tranNumber)
  {
    ((PXSelectBase) this.ExternalTransactions).Cache.Clear();
    ((PXSelectBase) this.ExternalTransactions).Cache.ClearQueryCache();
    foreach (PX.Objects.AR.ExternalTransaction externalTransaction in this.ExtTranRepo.GetExternalTransaction(cCProcessingCenterID, tranNumber))
    {
      if (!externalTransaction.NeedSync.GetValueOrDefault() && externalTransaction.SyncStatus != "E")
        return externalTransaction;
    }
    return (PX.Objects.AR.ExternalTransaction) null;
  }

  private PX.Objects.AR.ExternalTransaction GetExternalTransactionByNoteID(Guid? noteID)
  {
    ((PXSelectBase) this.ExternalTransactions).Cache.Clear();
    ((PXSelectBase) this.ExternalTransactions).Cache.ClearQueryCache();
    PX.Objects.AR.ExternalTransaction transactionByNoteId = this.ExtTranRepo.GetExternalTransactionByNoteID(noteID);
    return (transactionByNoteId != null ? (!transactionByNoteId.NeedSync.GetValueOrDefault() ? 1 : 0) : 1) != 0 && transactionByNoteId?.SyncStatus != "E" ? transactionByNoteId : (PX.Objects.AR.ExternalTransaction) null;
  }

  internal enum StatusMatchingResult
  {
    NoMatch,
    Match,
    SuccessMatch,
    NoMatchSkipValidation,
    VoidPaymentWithoutVoidTransaction,
  }
}
