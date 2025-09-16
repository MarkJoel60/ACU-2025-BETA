// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.GraphExtensions.ARCashSaleEntryPaymentTransaction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.Standalone;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.Extensions.PaymentTransaction;
using PX.Objects.GL;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR.GraphExtensions;

public class ARCashSaleEntryPaymentTransaction : PaymentTransactionGraph<ARCashSaleEntry, ARCashSale>
{
  public PXSelect<PX.Objects.AR.ExternalTransaction> externalTran;
  public PXSelect<PX.Objects.CC.DefaultTerminal, Where<PX.Objects.CC.DefaultTerminal.userID, Equal<Current<AccessInfo.userID>>, And<PX.Objects.CC.DefaultTerminal.branchID, Equal<Current<AccessInfo.branchID>>, And<PX.Objects.CC.DefaultTerminal.processingCenterID, Equal<Current<ARCashSale.processingCenterID>>>>>> DefaultTerminal;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>();
  }

  [PXOverride]
  public virtual void Persist(System.Action persist)
  {
    ARCashSale current = ((PXSelectBase<ARCashSale>) this.Base.Document).Current;
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

  protected override PaymentTransactionGraph<ARCashSaleEntry, ARCashSale>.PaymentTransactionDetailMapping GetPaymentTransactionMapping()
  {
    return new PaymentTransactionGraph<ARCashSaleEntry, ARCashSale>.PaymentTransactionDetailMapping(typeof (CCProcTran));
  }

  protected override PaymentTransactionGraph<ARCashSaleEntry, ARCashSale>.ExternalTransactionDetailMapping GetExternalTransactionMapping()
  {
    return new PaymentTransactionGraph<ARCashSaleEntry, ARCashSale>.ExternalTransactionDetailMapping(typeof (PX.Objects.AR.ExternalTransaction));
  }

  protected override PaymentTransactionGraph<ARCashSaleEntry, ARCashSale>.PaymentMapping GetPaymentMapping()
  {
    return new PaymentTransactionGraph<ARCashSaleEntry, ARCashSale>.PaymentMapping(typeof (ARCashSale));
  }

  protected override void MapViews(ARCashSaleEntry graph)
  {
    this.PaymentTransaction = new PXSelectExtension<PaymentTransactionDetail>((PXSelectBase) this.Base.ccProcTran);
    this.ExternalTransaction = new PXSelectExtension<ExternalTransactionDetail>((PXSelectBase) this.Base.ExternalTran);
  }

  protected override void BeforeVoidPayment(ARCashSale doc)
  {
    base.BeforeVoidPayment(doc);
    int num;
    if (doc.VoidAppl.GetValueOrDefault())
    {
      bool? nullable = doc.Released;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        nullable = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.IntegratedCCProcessing;
        num = nullable.GetValueOrDefault() ? 1 : 0;
        goto label_4;
      }
    }
    num = 0;
label_4:
    this.ReleaseDoc = num != 0;
  }

  protected override void BeforeCapturePayment(ARCashSale doc)
  {
    base.BeforeCapturePayment(doc);
    bool? released = doc.Released;
    bool flag = false;
    this.ReleaseDoc = released.GetValueOrDefault() == flag & released.HasValue && ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.IntegratedCCProcessing.GetValueOrDefault();
  }

  protected override void BeforeCreditPayment(ARCashSale doc)
  {
    base.BeforeCreditPayment(doc);
    if (((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current?.PaymentType == "POS" && string.IsNullOrEmpty(doc.RefTranExtNbr) && string.IsNullOrEmpty(doc.TerminalID))
    {
      PXSetPropertyException propertyException = new PXSetPropertyException("Either the original transaction or the terminal must be specified.", (PXErrorLevel) 4);
      ((PXSelectBase) this.Base.Document).Cache.RaiseExceptionHandling<ARCashSale.refTranExtNbr>((object) doc, (object) doc.RefTranExtNbr, (Exception) propertyException);
      throw propertyException;
    }
    bool? nullable = doc.Released;
    bool flag = false;
    int num;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
    {
      nullable = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.IntegratedCCProcessing;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    this.ReleaseDoc = num != 0;
  }

  protected override AfterProcessingManager GetAfterProcessingManager(ARCashSaleEntry graph)
  {
    ARCashSaleAfterProcessingManager processingManager = this.GetARCashSaleAfterProcessingManager();
    processingManager.Graph = graph;
    return (AfterProcessingManager) processingManager;
  }

  protected override AfterProcessingManager GetAfterProcessingManager()
  {
    return (AfterProcessingManager) this.GetARCashSaleAfterProcessingManager();
  }

  private ARCashSaleAfterProcessingManager GetARCashSaleAfterProcessingManager()
  {
    return new ARCashSaleAfterProcessingManager()
    {
      ReleaseDoc = true
    };
  }

  protected override void RowSelected(PX.Data.Events.RowSelected<ARCashSale> e)
  {
    base.RowSelected(e);
    ARCashSale row = e.Row;
    if (row == null)
      return;
    this.TranHeldwarnMsg = "The transaction is held for review by the processing center. Use the processing center interface to approve or reject the transaction. After the transaction is processed by the processing center, use the Validate Card Payment action to update the processing status.";
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARCashSale>>) e).Cache;
    bool isDocTypePayment = ARCashSaleEntryPaymentTransaction.IsDocTypePayment(row);
    bool valueOrDefault = row.Released.GetValueOrDefault();
    bool isPMInstanceRequired = false;
    if (!string.IsNullOrEmpty(row.PaymentMethodID))
      isPMInstanceRequired = ((bool?) ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current?.IsAccountNumberRequired).GetValueOrDefault();
    this.ProcessingCCSettings(row, cache1, isPMInstanceRequired);
    CCProcessingCenter processingCenter = CCProcessingCenter.PK.Find((PXGraph) this.Base, row.ProcessingCenterID);
    this.SelectedProcessingCenter = processingCenter?.ProcessingCenterID;
    this.SelectedProcessingCenterType = processingCenter?.ProcessingTypeName;
    ExternalTransactionState transactionState = this.GetActiveTransactionState();
    bool flag1 = this.CanAuthorize(row, transactionState, isDocTypePayment);
    bool? nullable1;
    int num1;
    if (processingCenter == null)
    {
      num1 = 0;
    }
    else
    {
      nullable1 = processingCenter.IsExternalAuthorizationOnly;
      bool flag2 = false;
      num1 = nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue ? 1 : 0;
    }
    bool flag3 = num1 != 0;
    ((PXAction) this.authorizeCCPayment).SetEnabled(flag1 & flag3);
    bool flag4 = this.CanCapture(row, transactionState, isDocTypePayment);
    int num2;
    if (!flag3)
    {
      if (processingCenter != null)
      {
        nullable1 = processingCenter.IsExternalAuthorizationOnly;
        if (nullable1.GetValueOrDefault())
        {
          num2 = transactionState.IsActive ? 1 : 0;
          goto label_12;
        }
      }
      num2 = 0;
    }
    else
      num2 = 1;
label_12:
    bool flag5 = num2 != 0;
    ((PXAction) this.captureCCPayment).SetEnabled(flag4 & flag5);
    nullable1 = row.Hold;
    bool flag6 = false;
    int num3 = !(nullable1.GetValueOrDefault() == flag6 & nullable1.HasValue) ? 0 : (row.DocType == "RCS" ? 1 : 0);
    bool flag7 = num3 != 0 && (transactionState.IsCaptured || transactionState.IsPreAuthorized) || transactionState.IsPreAuthorized & isDocTypePayment;
    bool flag8 = this.EnableCCProcess(row);
    ((PXAction) this.voidCCPayment).SetEnabled(flag8 & flag7);
    bool flag9 = num3 != 0 && !transactionState.IsRefunded && (transactionState.IsCaptured || transactionState.IsPreAuthorized || string.IsNullOrEmpty(row.OrigRefNbr));
    ((PXAction) this.creditCCPayment).SetEnabled(flag8 & flag9);
    row.CCPaymentStateDescr = this.GetPaymentStateDescr(transactionState);
    ((PXAction) this.validateCCPayment).SetEnabled(this.CanValidate(row));
    ((PXAction) this.recordCCPayment).SetEnabled(false);
    ((PXAction) this.recordCCPayment).SetVisible(false);
    ((PXAction) this.captureOnlyCCPayment).SetEnabled(false);
    ((PXAction) this.captureOnlyCCPayment).SetVisible(false);
    PXCache pxCache = cache1;
    int num4;
    if (!flag8)
    {
      nullable1 = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RequireExtRef;
      num4 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num4 = 1;
    PXUIFieldAttribute.SetRequired<ARCashSale.extRefNbr>(pxCache, num4 != 0);
    PXUIFieldAttribute.SetVisible<ARCashSale.cCPaymentStateDescr>(cache1, (object) row, flag8 && !string.IsNullOrEmpty(row.CCPaymentStateDescr));
    PXUIFieldAttribute.SetVisible<ARCashSale.refTranExtNbr>(cache1, (object) row, row.DocType == "RCS" & flag8);
    PXUIFieldAttribute.SetRequired<PX.Objects.AR.ARPayment.pMInstanceID>(cache1, isPMInstanceRequired);
    PXDefaultAttribute.SetPersistingCheck<PX.Objects.AR.ARPayment.pMInstanceID>(cache1, (object) row, isPMInstanceRequired ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    if (flag8 && !valueOrDefault && (transactionState.IsPreAuthorized || transactionState.IsCaptured || row.DocType == "RCS" && (transactionState.IsRefunded || this.CheckLastProcessedTranIsVoided(row))))
    {
      this.SetHeaderFields(cache1, row, false);
      if (row.Status != "D")
      {
        PXUIFieldAttribute.SetEnabled<ARCashSale.adjDate>(cache1, (object) row, true);
        PXUIFieldAttribute.SetEnabled<ARCashSale.adjFinPeriodID>(cache1, (object) row, true);
      }
      PXUIFieldAttribute.SetEnabled<ARCashSale.hold>(cache1, (object) row, true);
      PXDBCurrencyAttribute.SetBaseCalc<ARCashSale.curyDocBal>(cache1, (object) null, true);
      PXDBCurrencyAttribute.SetBaseCalc<ARCashSale.curyDiscBal>(cache1, (object) null, true);
      cache1.AllowDelete = row.DocType == "RCS" && !transactionState.IsRefunded && !this.CheckLastProcessedTranIsVoided(row);
      cache1.AllowUpdate = true;
      ((PXSelectBase) this.Base.Transactions).Cache.AllowDelete = true;
      ((PXSelectBase) this.Base.Transactions).Cache.AllowUpdate = true;
      PXCache cache2 = ((PXSelectBase) this.Base.Transactions).Cache;
      int? nullable2 = row.CustomerID;
      int num5;
      if (nullable2.HasValue)
      {
        nullable2 = row.CustomerLocationID;
        num5 = nullable2.HasValue ? 1 : 0;
      }
      else
        num5 = 0;
      cache2.AllowInsert = num5 != 0;
      PXAction<ARCashSale> release = this.Base.release;
      nullable1 = row.Hold;
      bool flag10 = false;
      int num6 = nullable1.GetValueOrDefault() == flag10 & nullable1.HasValue ? 1 : 0;
      ((PXAction) release).SetEnabled(num6 != 0);
      ((PXAction) this.Base.voidCheck).SetEnabled(false);
    }
    else
    {
      PXUIFieldAttribute.SetEnabled<ARCashSale.refTranExtNbr>(cache1, (object) row, flag8 && !valueOrDefault && row.DocType == "RCS" && !transactionState.IsRefunded);
      nullable1 = row.Released;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = row.Voided;
        if (!nullable1.GetValueOrDefault())
        {
          cache1.AllowDelete = !ExternalTranHelper.HasTransactions((PXSelectBase<PX.Objects.AR.ExternalTransaction>) this.Base.ExternalTran);
          goto label_26;
        }
      }
      cache1.AllowDelete = false;
    }
label_26:
    if (flag8 && CCProcessingHelper.IntegratedProcessingActivated(((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current))
    {
      nullable1 = row.Released;
      bool flag11 = false;
      if (nullable1.GetValueOrDefault() == flag11 & nullable1.HasValue)
      {
        nullable1 = row.Hold;
        bool flag12 = false;
        int num7;
        if (nullable1.GetValueOrDefault() == flag12 & nullable1.HasValue)
        {
          nullable1 = row.OpenDoc;
          if (nullable1.GetValueOrDefault())
          {
            num7 = row.DocType == "RCS" ? (transactionState.IsRefunded ? 1 : 0) : (transactionState.IsCaptured ? 1 : 0);
            goto label_32;
          }
        }
        num7 = 0;
label_32:
        ((PXAction) this.Base.release).SetEnabled(num7 != 0);
      }
    }
    PXUIFieldAttribute.SetEnabled<ARCashSale.docType>(cache1, (object) row, true);
    PXUIFieldAttribute.SetEnabled<ARCashSale.refNbr>(cache1, (object) row, true);
    this.ShowWarningIfActualFinPeriodClosed(e, row);
    this.ShowWarningIfExternalAuthorizationOnly(e, row);
    this.ShowWarningOnProcessingCenterID(e, row);
    this.SetActionCaptions();
    this.EnableDisableFieldsAndActions(row, transactionState, cache1);
  }

  protected virtual void EnableDisableFieldsAndActions(
    ARCashSale doc,
    ExternalTransactionState tranState,
    PXCache cache)
  {
    bool flag1 = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current?.PaymentType == "POS";
    PXUIFieldAttribute.SetVisible<ARCashSale.processingCenterID>(cache, (object) doc, flag1);
    PXUIFieldAttribute.SetVisible<ARCashSale.terminalID>(cache, (object) doc, flag1);
    PXUIFieldAttribute.SetVisible<ARCashSale.pMInstanceID>(cache, (object) doc, !flag1);
    bool? nullable = doc.Released;
    if (nullable.GetValueOrDefault())
      return;
    nullable = doc.IsCCPayment;
    bool flag2 = false;
    if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
      return;
    nullable = doc.PendingProcessing;
    bool flag3 = false;
    int num1;
    if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
    {
      nullable = doc.Voided;
      bool flag4 = false;
      if (nullable.GetValueOrDefault() == flag4 & nullable.HasValue)
      {
        nullable = doc.Hold;
        bool flag5 = false;
        num1 = nullable.GetValueOrDefault() == flag5 & nullable.HasValue ? 1 : 0;
        goto label_7;
      }
    }
    num1 = 0;
label_7:
    bool flag6 = num1 != 0;
    nullable = doc.Hold;
    int num2;
    if (!nullable.GetValueOrDefault())
    {
      nullable = doc.PendingProcessing;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 1;
    bool flag7 = num2 != 0;
    nullable = doc.Hold;
    bool flag8 = nullable.GetValueOrDefault() && tranState.IsCaptured;
    switch (doc.DocType)
    {
      case "CSL":
        this.SetAdditionalHeaderFields(cache, doc, flag7 && (!tranState.IsCaptured || tranState.IsPreAuthorized));
        this.SetDetailsTabFields(((PXSelectBase) this.Base.Transactions).Cache, !(flag8 | flag6));
        this.SetFinancialTabFields(cache, doc, ((!flag7 ? 0 : (!tranState.IsCaptured ? 1 : (tranState.IsPreAuthorized ? 1 : 0))) | (flag8 ? 1 : 0)) != 0);
        bool flag9 = ((!flag7 ? 0 : (!tranState.IsCaptured ? 1 : (tranState.IsPreAuthorized ? 1 : 0))) | (flag8 ? 1 : 0)) != 0;
        PXUIFieldAttribute.SetEnabled<ARCashSale.projectID>(cache, (object) doc, flag9);
        PXUIFieldAttribute.SetEnabled<ARCashSale.docDesc>(cache, (object) doc, flag9);
        PXUIFieldAttribute.SetEnabled<ARTran.qty>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, !(flag8 | flag6));
        bool flag10 = !(flag8 | flag6);
        PXSelect<ARTran, Where<ARTran.tranType, Equal<Current<ARCashSale.docType>>, And<ARTran.refNbr, Equal<Current<ARCashSale.refNbr>>>>, OrderBy<Asc<ARTran.tranType, Asc<ARTran.refNbr, Asc<ARTran.lineNbr>>>>> transactions1 = this.Base.Transactions;
        bool? allowInsert1 = new bool?(flag10);
        nullable = new bool?();
        bool? allowSelect1 = nullable;
        nullable = new bool?();
        bool? allowUpdate1 = nullable;
        bool? allowDelete1 = new bool?(flag10);
        this.SetTabPermissions((PXSelectBase) transactions1, allowInsert1, allowSelect1, allowUpdate1, allowDelete1);
        PXSelectJoin<ARTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<ARTaxTran.taxID>>>, Where<ARTaxTran.module, Equal<BatchModule.moduleAR>, And<ARTaxTran.tranType, Equal<Current<ARCashSale.docType>>, And<ARTaxTran.refNbr, Equal<Current<ARCashSale.refNbr>>>>>> taxes1 = this.Base.Taxes;
        bool? allowInsert2 = new bool?(flag10);
        nullable = new bool?();
        bool? allowSelect2 = nullable;
        bool? allowUpdate2 = new bool?(flag10);
        bool? allowDelete2 = new bool?(flag10);
        this.SetTabPermissions((PXSelectBase) taxes1, allowInsert2, allowSelect2, allowUpdate2, allowDelete2);
        PXSelect<ARSalesPerTran, Where<ARSalesPerTran.docType, Equal<Current<ARCashSale.docType>>, And<ARSalesPerTran.refNbr, Equal<Current<ARCashSale.refNbr>>, And<ARSalesPerTran.adjdDocType, Equal<ARDocType.undefined>, And2<Where<Current<PX.Objects.AR.ARSetup.sPCommnCalcType>, Equal<SPCommnCalcTypes.byInvoice>, Or<Current<ARCashSale.released>, Equal<boolFalse>>>, Or<ARSalesPerTran.adjdDocType, Equal<Current<ARCashSale.docType>>, And<ARSalesPerTran.adjdRefNbr, Equal<Current<ARCashSale.refNbr>>, And<Current<PX.Objects.AR.ARSetup.sPCommnCalcType>, Equal<SPCommnCalcTypes.byPayment>>>>>>>>> salesPerTrans = this.Base.salesPerTrans;
        bool? allowInsert3 = new bool?(!flag6);
        nullable = new bool?();
        bool? allowSelect3 = nullable;
        bool? allowUpdate3 = new bool?(!flag6);
        bool? allowDelete3 = new bool?(!flag6);
        this.SetTabPermissions((PXSelectBase) salesPerTrans, allowInsert3, allowSelect3, allowUpdate3, allowDelete3);
        break;
      case "RCS":
        bool flag11 = tranState.IsRefunded || tranState.IsVoided || this.CheckLastProcessedTranIsVoided(doc);
        bool flag12 = flag11 || tranState.IsCaptured;
        bool flag13 = PXSelectBase<EPAssignmentMap, PXSelectReadonly<EPAssignmentMap, Where<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeARCashSale>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()).Count != 0 && PXSelectBase<ARSetupApproval, PXSelectReadonly<ARSetupApproval, Where<ARSetupApproval.docType, Equal<ARDocType.cashReturn>, And<ARSetupApproval.isActive, Equal<True>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()).Count != 0;
        nullable = doc.Hold;
        bool flag14 = false;
        int num3;
        if (nullable.GetValueOrDefault() == flag14 & nullable.HasValue)
        {
          nullable = doc.Approved;
          bool flag15 = false;
          if (nullable.GetValueOrDefault() == flag15 & nullable.HasValue)
          {
            nullable = doc.DontApprove;
            bool flag16 = false;
            num3 = nullable.GetValueOrDefault() == flag16 & nullable.HasValue ? 1 : 0;
            goto label_16;
          }
        }
        num3 = 0;
label_16:
        bool flag17 = num3 != 0;
        int num4;
        if (flag13)
        {
          if (!flag17)
          {
            nullable = doc.Approved;
            num4 = nullable.GetValueOrDefault() ? 1 : 0;
          }
          else
            num4 = 1;
        }
        else
          num4 = 0;
        bool flag18 = num4 != 0;
        nullable = doc.PendingProcessing;
        int num5;
        if (!nullable.GetValueOrDefault())
        {
          nullable = doc.Voided;
          bool flag19 = false;
          if (nullable.GetValueOrDefault() == flag19 & nullable.HasValue)
          {
            nullable = doc.Hold;
            bool flag20 = false;
            num5 = nullable.GetValueOrDefault() == flag20 & nullable.HasValue ? 1 : 0;
          }
          else
            num5 = 0;
        }
        else
          num5 = 1;
        bool flag21 = num5 != 0;
        bool flag22 = !string.IsNullOrEmpty(doc.OrigRefNbr);
        int num6;
        if (flag21 & flag18)
        {
          nullable = doc.Hold;
          bool flag23 = false;
          if (nullable.GetValueOrDefault() == flag23 & nullable.HasValue)
          {
            num6 = 0;
            goto label_32;
          }
        }
        if (flag11)
        {
          nullable = doc.Hold;
          bool flag24 = false;
          num6 = !(nullable.GetValueOrDefault() == flag24 & nullable.HasValue) ? 1 : 0;
        }
        else
          num6 = 1;
label_32:
        bool flag25 = num6 != 0;
        PXUIFieldAttribute.SetEnabled<ARCashSale.projectID>(cache, (object) doc, flag25);
        PXUIFieldAttribute.SetEnabled<ARCashSale.docDesc>(cache, (object) doc, flag25);
        PXCache cache1 = cache;
        ARCashSale doc1 = doc;
        int num7;
        if (!(flag21 & flag18))
        {
          if (flag22)
          {
            nullable = doc.Hold;
            if (nullable.GetValueOrDefault())
              goto label_38;
          }
          if (flag22)
          {
            nullable = doc.Hold;
            bool flag26 = false;
            num7 = nullable.GetValueOrDefault() == flag26 & nullable.HasValue ? 1 : 0;
            goto label_39;
          }
          num7 = 0;
          goto label_39;
        }
label_38:
        num7 = 1;
label_39:
        int num8 = flag11 ? 1 : 0;
        int num9 = (num7 | num8) == 0 ? 1 : 0;
        this.SetAdditionalHeaderFields(cache1, doc1, num9 != 0);
        PXUIFieldAttribute.SetEnabled<ARCashSale.pMInstanceID>(cache, (object) doc, flag17 && !flag12);
        int num10;
        if (!flag7)
        {
          nullable = doc.Voided;
          bool flag27 = false;
          if (!(nullable.GetValueOrDefault() == flag27 & nullable.HasValue))
          {
            num10 = 1;
            goto label_47;
          }
        }
        if (!flag11)
        {
          if (flag13)
          {
            nullable = doc.Approved;
            num10 = nullable.GetValueOrDefault() ? 0 : (!tranState.IsCaptured ? 1 : 0);
          }
          else
            num10 = 1;
        }
        else
          num10 = 0;
label_47:
        bool flag28 = num10 != 0;
        this.SetDetailsTabFields(((PXSelectBase) this.Base.Transactions).Cache, flag28);
        PXSelect<ARTran, Where<ARTran.tranType, Equal<Current<ARCashSale.docType>>, And<ARTran.refNbr, Equal<Current<ARCashSale.refNbr>>>>, OrderBy<Asc<ARTran.tranType, Asc<ARTran.refNbr, Asc<ARTran.lineNbr>>>>> transactions2 = this.Base.Transactions;
        bool? allowInsert4 = new bool?(flag28);
        nullable = new bool?();
        bool? allowSelect4 = nullable;
        bool? allowUpdate4 = new bool?(flag28);
        bool? allowDelete4 = new bool?(flag28);
        this.SetTabPermissions((PXSelectBase) transactions2, allowInsert4, allowSelect4, allowUpdate4, allowDelete4);
        PXCache cache2 = ((PXSelectBase) this.Base.Transactions).Cache;
        nullable = doc.Hold;
        int num11;
        if (!nullable.GetValueOrDefault())
        {
          nullable = doc.PendingProcessing;
          if (!nullable.GetValueOrDefault())
          {
            nullable = doc.Voided;
            bool flag29 = false;
            num11 = nullable.GetValueOrDefault() == flag29 & nullable.HasValue ? 1 : 0;
            goto label_51;
          }
        }
        num11 = 1;
label_51:
        int num12 = flag11 ? 1 : 0;
        int num13 = (num11 & num12) == 0 ? 1 : 0;
        PXUIFieldAttribute.SetEnabled<ARTran.qty>(cache2, (object) null, num13 != 0);
        PXCache cache3 = cache;
        ARCashSale doc2 = doc;
        int num14;
        if (!flag7)
        {
          nullable = doc.Voided;
          bool flag30 = false;
          num14 = nullable.GetValueOrDefault() == flag30 & nullable.HasValue ? 1 : 0;
        }
        else
          num14 = 1;
        int num15 = flag11 ? 1 : 0;
        int num16 = (((num14 & num15 & (flag8 ? 1 : 0)) != 0 ? 1 : (!flag6 ? 0 : (tranState.IsRefunded ? 1 : 0))) | (flag18 ? 1 : 0)) == 0 ? 1 : 0;
        this.SetFinancialTabFields(cache3, doc2, num16 != 0);
        PXCache cache4 = ((PXSelectBase) this.Base.Transactions).Cache;
        int num17;
        if (!flag7)
        {
          nullable = doc.Voided;
          bool flag31 = false;
          num17 = nullable.GetValueOrDefault() == flag31 & nullable.HasValue ? 1 : 0;
        }
        else
          num17 = 1;
        int num18 = flag11 ? 1 : 0;
        int num19 = (num17 & num18) == 0 ? 1 : 0;
        this.SetTaxTabFields(cache4, num19 != 0);
        int num20;
        if (!flag7)
        {
          nullable = doc.Voided;
          bool flag32 = false;
          if (!(nullable.GetValueOrDefault() == flag32 & nullable.HasValue))
          {
            num20 = 1;
            goto label_65;
          }
        }
        if (!flag11)
        {
          if (flag13)
          {
            nullable = doc.Approved;
            num20 = nullable.GetValueOrDefault() ? 0 : (!tranState.IsCaptured ? 1 : 0);
          }
          else
            num20 = 1;
        }
        else
          num20 = 0;
label_65:
        bool flag33 = num20 != 0;
        PXSelectJoin<ARTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<ARTaxTran.taxID>>>, Where<ARTaxTran.module, Equal<BatchModule.moduleAR>, And<ARTaxTran.tranType, Equal<Current<ARCashSale.docType>>, And<ARTaxTran.refNbr, Equal<Current<ARCashSale.refNbr>>>>>> taxes2 = this.Base.Taxes;
        bool? allowInsert5 = new bool?(flag33);
        nullable = new bool?();
        bool? allowSelect5 = nullable;
        bool? allowUpdate5 = new bool?(flag33);
        bool? allowDelete5 = new bool?(flag33);
        this.SetTabPermissions((PXSelectBase) taxes2, allowInsert5, allowSelect5, allowUpdate5, allowDelete5);
        break;
    }
  }

  protected virtual void SetHeaderFields(PXCache cache, ARCashSale doc, bool value)
  {
    PXUIFieldAttribute.SetEnabled<ARCashSale.curyOrigDocAmt>(cache, (object) doc, value);
    PXUIFieldAttribute.SetEnabled<ARCashSale.curyOrigDiscAmt>(cache, (object) doc, value);
    PXUIFieldAttribute.SetEnabled<ARCashSale.refTranExtNbr>(cache, (object) doc, value);
    PXUIFieldAttribute.SetEnabled<ARCashSale.customerLocationID>(cache, (object) doc, value);
    PXUIFieldAttribute.SetEnabled<ARCashSale.paymentMethodID>(cache, (object) doc, value);
    PXUIFieldAttribute.SetEnabled<ARCashSale.pMInstanceID>(cache, (object) doc, value);
    PXUIFieldAttribute.SetEnabled<ARCashSale.cashAccountID>(cache, (object) doc, value);
    PXUIFieldAttribute.SetEnabled<ARCashSale.taxCalcMode>(cache, (object) doc, value);
    PXUIFieldAttribute.SetEnabled<ARCashSale.processingCenterID>(cache, (object) doc, value);
    PXUIFieldAttribute.SetEnabled<ARCashSale.terminalID>(cache, (object) doc, value);
  }

  protected virtual void SetAdditionalHeaderFields(PXCache cache, ARCashSale doc, bool value)
  {
    PXUIFieldAttribute.SetEnabled<ARCashSale.termsID>(cache, (object) doc, value);
    PXUIFieldAttribute.SetEnabled<ARCashSale.extRefNbr>(cache, (object) doc, value);
  }

  protected virtual void SetDetailsTabFields(PXCache cache, bool value)
  {
    PXUIFieldAttribute.SetEnabled<ARTran.curyUnitPrice>(cache, (object) null, value);
    PXUIFieldAttribute.SetEnabled<ARTran.discPct>(cache, (object) null, value);
    PXUIFieldAttribute.SetEnabled<ARTran.curyDiscAmt>(cache, (object) null, value);
    PXUIFieldAttribute.SetEnabled<ARTran.curyExtPrice>(cache, (object) null, value);
    PXUIFieldAttribute.SetEnabled<ARTran.manualDisc>(cache, (object) null, value);
    PXUIFieldAttribute.SetEnabled<ARTran.taxCategoryID>(cache, (object) null, value);
    PXUIFieldAttribute.SetEnabled<ARTran.inventoryID>(cache, (object) null, value);
    PXUIFieldAttribute.SetEnabled<ARTran.uOM>(cache, (object) null, value);
  }

  protected virtual void SetFinancialTabFields(PXCache cache, ARCashSale doc, bool value)
  {
    PXUIFieldAttribute.SetEnabled<ARCashSale.branchID>(cache, (object) doc, value);
    PXUIFieldAttribute.SetEnabled<ARCashSale.aRAccountID>(cache, (object) doc, value);
    PXUIFieldAttribute.SetEnabled<ARCashSale.aRSubID>(cache, (object) doc, value);
    PXUIFieldAttribute.SetEnabled<ARCashSale.taxZoneID>(cache, (object) doc, value);
    PXUIFieldAttribute.SetEnabled<ARCashSale.externalTaxExemptionNumber>(cache, (object) doc, value);
    PXUIFieldAttribute.SetEnabled<ARCashSale.avalaraCustomerUsageType>(cache, (object) doc, value);
    PXUIFieldAttribute.SetEnabled<ARCashSale.workgroupID>(cache, (object) doc, value);
    PXUIFieldAttribute.SetEnabled<ARCashSale.ownerID>(cache, (object) doc, value);
    PXUIFieldAttribute.SetEnabled<ARCashSale.dontPrint>(cache, (object) doc, value);
    PXUIFieldAttribute.SetEnabled<ARCashSale.dontEmail>(cache, (object) doc, value);
  }

  protected virtual void SetTaxTabFields(PXCache cache, bool value)
  {
    PXUIFieldAttribute.SetEnabled<ARTaxTran.taxID>(cache, (object) null, value);
    PXUIFieldAttribute.SetEnabled<TaxTran.taxRate>(cache, (object) null, value);
  }

  protected virtual void SetTabPermissions(
    PXSelectBase view,
    bool? allowInsert,
    bool? allowSelect,
    bool? allowUpdate,
    bool? allowDelete)
  {
    if (allowInsert.HasValue)
      view.AllowInsert = allowInsert.Value;
    if (allowSelect.HasValue)
      view.AllowSelect = allowSelect.Value;
    if (allowUpdate.HasValue)
      view.AllowUpdate = allowUpdate.Value;
    if (!allowDelete.HasValue)
      return;
    view.AllowDelete = allowDelete.Value;
  }

  protected virtual void ProcessingCCSettings(
    ARCashSale doc,
    PXCache cache,
    bool isPMInstanceRequired)
  {
    PXUIFieldAttribute.SetRequired<ARCashSale.pMInstanceID>(cache, isPMInstanceRequired);
    bool flag = doc.Released.GetValueOrDefault() || doc.Voided.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<ARCashSale.pMInstanceID>(cache, (object) doc, isPMInstanceRequired && !flag);
    UIState.RaiseOrHideErrorByErrorLevelPriority<PX.Objects.AR.ARPayment.status>(cache, (object) doc, false, "The document can be processed as a document with the Balanced status. Card processing actions are no longer available because the Integrated Card Processing feature has been disabled.", (PXErrorLevel) 2);
    this.Base.EnableVoidIfPossible(doc, cache, doc.Status == "W");
  }

  private void SetActionCaptions()
  {
    bool flag = this.IsEft();
    ((PXAction) this.voidCCPayment).SetCaption(flag ? (this.IsRejection() ? "Record EFT Rejection" : "Void EFT Payment") : "Void Card Payment");
    ((PXAction) this.creditCCPayment).SetCaption(flag ? "Refund EFT Payment" : "Refund Card Payment");
    ((PXAction) this.validateCCPayment).SetCaption(flag ? "Validate EFT Payment" : "Validate Card Payment");
  }

  private bool IsCCPaymentMethod(ARCashSale doc)
  {
    if (string.IsNullOrEmpty(doc.PaymentMethodID))
      return false;
    PX.Objects.CA.PaymentMethod paymentMethod = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) this.Base, doc.PaymentMethodID);
    return paymentMethod != null && EnumerableExtensions.IsIn<string>(paymentMethod.PaymentType, "CCD", "EFT", "POS");
  }

  protected virtual void ARCashSaleRowDeleting(PX.Data.Events.RowDeleting<ARCashSale> e, ARCashSale doc)
  {
    PX.Objects.CA.PaymentMethod current = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current;
    ExternalTransactionState transactionState = ExternalTranHelper.GetActiveTransactionState((PXGraph) this.Base, (PXSelectBase<PX.Objects.AR.ExternalTransaction>) this.Base.ExternalTran);
    bool flag = doc.DocType == "RCS" && !transactionState.IsRefunded && !transactionState.IsVoided;
    if (current?.PaymentType == "CCD" && current != null && current.ARIsProcessingRequired.GetValueOrDefault() && transactionState != null && transactionState.IsActive && !flag)
      throw new PXException("The document cannot be deleted because there are card transactions associated with it. Use the Void action to cancel the document.");
  }

  protected virtual void ShowWarningIfActualFinPeriodClosed(
    PX.Data.Events.RowSelected<ARCashSale> e,
    ARCashSale doc)
  {
    if (!this.IsCCPaymentMethod(doc) || !string.IsNullOrEmpty(PXUIFieldAttribute.GetError<ARCashSale.paymentMethodID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARCashSale>>) e).Cache, (object) doc)))
      return;
    this.RaiseWarning(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARCashSale>>) e).Cache, doc, PXContext.GetBranchID());
    this.RaiseWarning(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARCashSale>>) e).Cache, doc, doc.BranchID);
  }

  private void RaiseWarning(PXCache cache, ARCashSale doc, int? branchId)
  {
    if (!this.IsActualFinPeriodClosedForBranch(branchId))
      return;
    cache.RaiseExceptionHandling<ARCashSale.paymentMethodID>((object) doc, (object) doc.PaymentMethodID, (Exception) new PXSetPropertyException("The operation cannot be performed because the financial period of the current date is either closed, inactive, or does not exist for the {0} company. To process the payment with a credit card, EFT, or POS terminal, create or reopen the financial period.", (PXErrorLevel) 2, new object[1]
    {
      (object) ((PXAccess.Organization) PXAccess.GetBranch(branchId).Organization).OrganizationCD
    }));
  }

  protected virtual void ShowWarningIfExternalAuthorizationOnly(
    PX.Data.Events.RowSelected<ARCashSale> e,
    ARCashSale doc)
  {
    ExternalTransactionState transactionState = this.GetActiveTransactionState();
    CCProcessingCenter processingCenter = CCProcessingCenter.PK.Find((PXGraph) this.Base, doc.ProcessingCenterID);
    PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = PX.Objects.AR.CustomerPaymentMethod.PK.Find((PXGraph) this.Base, doc.PMInstanceID);
    bool flag = processingCenter != null && processingCenter.IsExternalAuthorizationOnly.GetValueOrDefault() && !transactionState.IsActive && doc.Status == "W" && doc.DocType == "CSL";
    UIState.RaiseOrHideErrorByErrorLevelPriority<ARCashSale.pMInstanceID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARCashSale>>) e).Cache, (object) e.Row, (flag ? 1 : 0) != 0, "The {0} card is associated with the {1} processing center that does not support the Authorize action. The Capture action is supported only for payments that were pre-authorized externally.", (PXErrorLevel) 2, (object) customerPaymentMethod?.Descr, (object) processingCenter?.ProcessingCenterID);
  }

  protected virtual void ShowWarningOnProcessingCenterID(
    PX.Data.Events.RowSelected<ARCashSale> e,
    ARCashSale doc)
  {
    CCProcessingCenter processingCenter = CCProcessingCenter.PK.Find((PXGraph) this.Base, doc.ProcessingCenterID);
    string message = string.Empty;
    bool flag = false;
    if (((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current?.PaymentType == "POS" && (processingCenter != null ? (!processingCenter.AcceptPOSPayments.GetValueOrDefault() ? 1 : 0) : 1) != 0)
    {
      message = "Payments from POS Terminals are disabled for the {0} processing center. To enable them, on the Processing Centers (CA205000) form, select the Accept Payments from POS Terminals check box.";
      flag = true;
    }
    UIState.RaiseOrHideErrorByErrorLevelPriority<ARCashSale.processingCenterID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARCashSale>>) e).Cache, (object) e.Row, (flag ? 1 : 0) != 0, message, (PXErrorLevel) 2, (object) processingCenter?.ProcessingCenterID);
  }

  protected virtual void FieldUpdated(PX.Data.Events.FieldUpdated<ARCashSale.paymentMethodID> e)
  {
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARCashSale.paymentMethodID>>) e).Cache;
    if (!(e.Row is ARCashSale row))
      return;
    this.SetPendingProcessingIfNeeded(cache, row);
    cache.SetDefaultExt<ARCashSale.terminalID>((object) row);
  }

  protected virtual void FieldUpdated(
    PX.Data.Events.FieldUpdated<ARCashSale.processingCenterID> e)
  {
    if (!(e.Row is ARCashSale row))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARCashSale.processingCenterID>>) e).Cache.SetDefaultExt<ARCashSale.terminalID>((object) row);
  }

  protected virtual void FieldUpdated(PX.Data.Events.FieldUpdated<ARCashSale.terminalID> e)
  {
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARCashSale.terminalID>>) e).Cache;
    if (!(e.Row is ARCashSale row))
      return;
    cache.SetValue<ARCashSale.cardPresent>((object) row, (object) !string.IsNullOrEmpty(row.TerminalID));
    if (string.IsNullOrEmpty(row.TerminalID) || string.IsNullOrEmpty(row.RefTranExtNbr))
      return;
    cache.SetValueExt<ARCashSale.refTranExtNbr>((object) row, (object) null);
  }

  protected virtual void FieldUpdated(PX.Data.Events.FieldUpdated<ARCashSale.refTranExtNbr> e)
  {
    if (!(e.Row is ARCashSale row) || string.IsNullOrEmpty(row.RefTranExtNbr) || string.IsNullOrEmpty(row.TerminalID))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARCashSale.refTranExtNbr>>) e).Cache.SetValueExt<ARCashSale.terminalID>((object) row, (object) null);
  }

  protected virtual void FieldUpdated(PX.Data.Events.FieldUpdated<ARCashSale.branchID> e)
  {
    if (!(e.Row is ARCashSale row) || !row.IsCCPayment.GetValueOrDefault() || !((PXFieldState) ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARCashSale.branchID>>) e).Cache.GetStateExt<PX.Objects.AR.ARPayment.pMInstanceID>((object) row)).Enabled)
      return;
    ARCashSale copy = (ARCashSale) ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARCashSale.branchID>>) e).Cache.CreateCopy((object) row);
    copy.BranchID = (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<ARCashSale.branchID>, object, object>) e).OldValue;
    object obj;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARCashSale.branchID>>) e).Cache.RaiseFieldDefaulting<ARCashSale.pMInstanceID>((object) copy, ref obj);
    int? nullable = (int?) obj;
    int? pmInstanceId = row.PMInstanceID;
    if (!(nullable.GetValueOrDefault() == pmInstanceId.GetValueOrDefault() & nullable.HasValue == pmInstanceId.HasValue))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARCashSale.branchID>>) e).Cache.SetDefaultExt<ARCashSale.processingCenterID>(e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARCashSale.branchID>>) e).Cache.SetDefaultExt<ARCashSale.pMInstanceID>(e.Row);
  }

  public static bool IsDocTypeSuitableForCC(ARCashSale doc)
  {
    return EnumerableExtensions.IsIn<string>(doc.DocType, "CSL", "RCS");
  }

  private bool IsEft()
  {
    return ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current != null && ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current.PaymentType == "EFT";
  }

  private bool IsRejection()
  {
    ARCashSale current = ((PXSelectBase<ARCashSale>) this.Base.Document).Current;
    if (current == null)
      return false;
    PX.Objects.Extensions.PaymentTransaction.Payment extension = PXCacheEx.GetExtension<PX.Objects.Extensions.PaymentTransaction.Payment>((IBqlTable) current);
    return extension != null && extension.IsRejection.GetValueOrDefault();
  }

  public static bool IsDocTypePayment(ARCashSale doc) => doc.DocType == "CSL";

  public bool EnableCCProcess(ARCashSale doc)
  {
    bool flag = false;
    if (!doc.IsMigratedRecord.GetValueOrDefault() && ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current != null && EnumerableExtensions.IsIn<string>(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current.PaymentType, "CCD", "EFT", "POS"))
      flag = ARCashSaleEntryPaymentTransaction.IsDocTypeSuitableForCC(doc);
    return flag & !doc.Voided.Value & !this.IsProcCenterDisabled(this.SelectedProcessingCenterType) && this.IsFinPeriodValid(PXContext.GetBranchID(), ((PXSelectBase<GLSetup>) this.Base.glsetup).Current.RestrictAccessToClosedPeriods) && this.IsFinPeriodValid(doc.BranchID, ((PXSelectBase<GLSetup>) this.Base.glsetup).Current.RestrictAccessToClosedPeriods);
  }

  private bool CanAuthorize(
    ARCashSale doc,
    ExternalTransactionState tranState,
    bool isDocTypePayment)
  {
    if (!this.EnableCCProcess(doc) || this.IsEft() || ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current.PaymentType == "POS" && string.IsNullOrEmpty(doc.TerminalID))
      return false;
    bool? hold = doc.Hold;
    bool flag = false;
    if (!(hold.GetValueOrDefault() == flag & hold.HasValue & isDocTypePayment) || tranState.IsPreAuthorized || tranState.IsCaptured)
      return false;
    Decimal? curyDocBal = doc.CuryDocBal;
    Decimal num = 0M;
    return curyDocBal.GetValueOrDefault() > num & curyDocBal.HasValue;
  }

  private bool CanCapture(
    ARCashSale doc,
    ExternalTransactionState tranState,
    bool isDocTypePayment)
  {
    if (!this.EnableCCProcess(doc) || ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current.PaymentType == "POS" && string.IsNullOrEmpty(doc.TerminalID))
      return false;
    bool? hold = doc.Hold;
    bool flag = false;
    if (!(hold.GetValueOrDefault() == flag & hold.HasValue & isDocTypePayment) || tranState.IsCaptured)
      return false;
    Decimal? curyDocBal = doc.CuryDocBal;
    Decimal num = 0M;
    return curyDocBal.GetValueOrDefault() > num & curyDocBal.HasValue;
  }

  public bool CanValidate(ARCashSale doc)
  {
    if (!this.EnableCCProcess(doc))
      return false;
    PXCache cache = ((PXSelectBase) this.Base.Document).Cache;
    bool isDocTypePayment = ARCashSaleEntryPaymentTransaction.IsDocTypePayment(doc);
    ExternalTransactionState transactionState = this.GetActiveTransactionState();
    bool? hold = doc.Hold;
    bool flag1 = false;
    if (!(hold.GetValueOrDefault() == flag1 & hold.HasValue & isDocTypePayment) || !transactionState.IsActive || cache.GetStatus((object) doc) == 2)
      return false;
    int num;
    if (!this.CanCapture(doc, transactionState, isDocTypePayment) && !this.CanAuthorize(doc, transactionState, isDocTypePayment) && !transactionState.IsOpenForReview && !ExternalTranHelper.HasImportedNeedSyncTran((PXGraph) this.Base, this.GetExtTrans()) && !transactionState.NeedSync && !transactionState.IsImportedUnknown)
    {
      int? pmInstanceId = doc.PMInstanceID;
      int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
      num = pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue ? 1 : 0;
    }
    else
      num = 1;
    bool flag2 = num != 0;
    if (flag2 && doc.DocType == "REF" && EnumerableExtensions.IsIn<ExternalTranHelper.SharedTranStatus>(ExternalTranHelper.GetSharedTranStatus((PXGraph) this.Base, this.GetExtTrans().FirstOrDefault<IExternalTransaction>()), ExternalTranHelper.SharedTranStatus.ClearState, ExternalTranHelper.SharedTranStatus.Synchronized))
      flag2 = false;
    if (!flag2)
    {
      AfterProcessingManager processingManager = this.GetAfterProcessingManager(this.Base);
      flag2 = processingManager != null && !processingManager.CheckDocStateConsistency((IBqlTable) doc);
    }
    return flag2 & this.GettingDetailsByTranSupported();
  }

  private string GetPaymentStateDescr(ExternalTransactionState state)
  {
    return this.GetLastTransactionDescription();
  }

  public override string GetTransactionStateDescription(IExternalTransaction targetTran)
  {
    foreach (IExternalTransaction extTran in this.GetExtTrans())
    {
      int? transactionId = extTran.TransactionID;
      int? parentTranId = targetTran.ParentTranID;
      if (transactionId.GetValueOrDefault() == parentTranId.GetValueOrDefault() & transactionId.HasValue == parentTranId.HasValue && extTran.ProcStatus == "VDS")
        return ExternalTranHelper.GetTransactionState((PXGraph) this.Base, extTran).Description;
    }
    return base.GetTransactionStateDescription(targetTran);
  }

  private bool GettingDetailsByTranSupported()
  {
    return CCProcessingFeatureHelper.IsFeatureSupported(((PXSelectBase<CCProcessingCenter>) this.Base.ProcessingCenter).SelectSingle(Array.Empty<object>()), CCProcessingFeature.TransactionGetter, false);
  }

  protected void SetPendingProcessingIfNeeded(PXCache sender, ARCashSale document)
  {
    int num;
    if (CCProcessingHelper.PaymentMethodSupportsIntegratedProcessing(((PXSelectBase<PX.Objects.CA.PaymentMethod>) new PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>((PXGraph) this.Base)).SelectSingle(new object[1]
    {
      (object) document.PaymentMethodID
    })))
    {
      bool? released = document.Released;
      bool flag = false;
      num = released.GetValueOrDefault() == flag & released.HasValue ? 1 : 0;
    }
    else
      num = 0;
    bool flag1 = num != 0;
    sender.SetValue<PX.Objects.AR.ARRegister.pendingProcessing>((object) document, (object) flag1);
  }

  protected override ARCashSale SetCurrentDocument(ARCashSaleEntry graph, ARCashSale doc)
  {
    PXSelectJoin<ARCashSale, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<ARCashSale.customerID>>>, Where<ARCashSale.docType, Equal<Optional<ARCashSale.docType>>, And2<Where<PX.Objects.AR.ARRegister.origModule, NotEqual<BatchModule.moduleSO>, Or<ARCashSale.released, Equal<boolTrue>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>> document = graph.Document;
    ((PXSelectBase<ARCashSale>) document).Current = PXResultset<ARCashSale>.op_Implicit(((PXSelectBase<ARCashSale>) document).Search<ARCashSale.refNbr>((object) doc.RefNbr, new object[1]
    {
      (object) doc.DocType
    }));
    return ((PXSelectBase<ARCashSale>) document).Current;
  }

  protected override PaymentTransactionGraph<ARCashSaleEntry, ARCashSale> GetPaymentTransactionExt(
    ARCashSaleEntry graph)
  {
    return (PaymentTransactionGraph<ARCashSaleEntry, ARCashSale>) ((PXGraph) graph).GetExtension<ARCashSaleEntryPaymentTransaction>();
  }

  private bool CheckLastProcessedTranIsVoided(ARCashSale cashSale)
  {
    IEnumerable<IExternalTransaction> extTrans = this.GetExtTrans();
    IExternalTransaction externalTran = ExternalTranHelper.GetLastProcessedExtTran(extTrans, this.GetProcTrans());
    IExternalTransaction extTran = extTrans.Where<IExternalTransaction>((Func<IExternalTransaction, bool>) (i =>
    {
      int? transactionId1 = i.TransactionID;
      int? transactionId2 = externalTran.TransactionID;
      return transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue;
    })).FirstOrDefault<IExternalTransaction>();
    return extTran != null && ExternalTranHelper.GetTransactionState((PXGraph) this.Base, extTran).IsVoided;
  }

  [PXUIField]
  [PXProcessButton]
  public override IEnumerable AuthorizeCCPayment(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.Extensions.PaymentTransaction.Payment>) this.PaymentDoc).Current == null)
      return adapter.Get();
    this.CalcTax(((PXSelectBase<PX.Objects.Extensions.PaymentTransaction.Payment>) this.PaymentDoc).Current);
    return base.AuthorizeCCPayment(adapter);
  }

  [PXUIField]
  [PXProcessButton]
  public override IEnumerable CaptureCCPayment(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.Extensions.PaymentTransaction.Payment>) this.PaymentDoc).Current == null)
      return adapter.Get();
    this.CalcTax(((PXSelectBase<PX.Objects.Extensions.PaymentTransaction.Payment>) this.PaymentDoc).Current);
    return base.CaptureCCPayment(adapter);
  }

  public virtual void CalcTax(PX.Objects.Extensions.PaymentTransaction.Payment payment)
  {
    ARCashSale current = ((PXSelectBase<ARCashSale>) this.Base.CurrentDocument).Current;
    payment.Tax = current.CuryTaxTotal;
    PX.Objects.Extensions.PaymentTransaction.Payment payment1 = payment;
    Decimal? curyOrigDocAmt = current.CuryOrigDocAmt;
    Decimal? tax = payment.Tax;
    Decimal? nullable = curyOrigDocAmt.HasValue & tax.HasValue ? new Decimal?(curyOrigDocAmt.GetValueOrDefault() - tax.GetValueOrDefault()) : new Decimal?();
    payment1.SubtotalAmount = nullable;
  }
}
