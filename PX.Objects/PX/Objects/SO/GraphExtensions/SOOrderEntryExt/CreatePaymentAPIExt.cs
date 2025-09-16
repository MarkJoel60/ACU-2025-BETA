// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.CreatePaymentAPIExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.GraphExtensions;
using PX.Objects.Extensions.PaymentTransaction;
using PX.Objects.SO.GraphExtensions.ARPaymentEntryExt;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class CreatePaymentAPIExt : PXGraphExtension<CreatePaymentExt, SOOrderEntry>
{
  public PXFilter<InputCCTransactionSO> apiInputCCTran;
  private int _nesting;

  public virtual ARPaymentEntry CreatePaymentAPI(
    SOAdjust soAdjust,
    PX.Objects.SO.SOOrder order,
    string paymentType)
  {
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    List<InputCCTransactionSO> source = new List<InputCCTransactionSO>();
    foreach (InputCCTransactionSO inputCcTransactionSo in ((PXSelectBase) this.apiInputCCTran).Cache.Inserted)
    {
      int? recordId1 = inputCcTransactionSo.RecordID;
      int? recordId2 = soAdjust.RecordID;
      if (recordId1.GetValueOrDefault() == recordId2.GetValueOrDefault() & recordId1.HasValue == recordId2.HasValue)
        source.Add(inputCcTransactionSo);
    }
    if (source.Count<InputCCTransactionSO>() != 0)
    {
      ((PXGraph) instance).IsContractBasedAPI = true;
    }
    else
    {
      bool? nullable = soAdjust.Capture;
      if (!nullable.GetValueOrDefault())
      {
        nullable = soAdjust.Authorize;
        if (!nullable.GetValueOrDefault())
          goto label_13;
      }
      ((PXSelectBase<ARSetup>) instance.arsetup).Current.HoldEntry = new bool?(false);
    }
label_13:
    PXSelectJoin<ARPayment, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<ARPayment.customerID>>>, Where<ARPayment.docType, Equal<Optional<ARPayment.docType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> document = instance.Document;
    ARPayment arPayment1 = new ARPayment();
    arPayment1.DocType = paymentType;
    ARPayment arpayment = ((PXSelectBase<ARPayment>) document).Insert(arPayment1);
    this.FillARPaymentAPI(instance, arpayment, soAdjust, order);
    ARPayment arPayment2 = ((PXSelectBase<ARPayment>) instance.Document).Update(arpayment);
    PXNoteAttribute.CopyNoteAndFiles(((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (SOAdjust)], (object) soAdjust, ((PXGraph) instance).Caches[typeof (ARPayment)], (object) arPayment2, (PXNoteAttribute.IPXCopySettings) null);
    ARPaymentEntryImportTransaction extension = ((PXGraph) instance).GetExtension<ARPaymentEntryImportTransaction>();
    foreach (InputCCTransaction inputCcTransaction in source)
      ((PXSelectBase<InputCCTransaction>) extension.apiInputCCTran).Insert(inputCcTransaction);
    SOAdjust soAdjust1 = new SOAdjust()
    {
      AdjdOrderType = order.OrderType,
      AdjdOrderNbr = order.OrderNbr
    };
    if (soAdjust.CuryAdjdAmt.HasValue)
      soAdjust1.CuryAdjgAmt = soAdjust.CuryAdjdAmt;
    if (arPayment2.DocType == "REF" && soAdjust.ValidateCCRefundOrigTransaction.HasValue)
      soAdjust1.ValidateCCRefundOrigTransaction = soAdjust.ValidateCCRefundOrigTransaction;
    OrdersToApplyTab applyTabExtension = instance.GetOrdersToApplyTabExtension(true);
    PXResultset<PX.Objects.SO.SOOrder> pxResultset = ((PXSelectBase<PX.Objects.SO.SOOrder>) applyTabExtension.SOOrder_CustomerID_OrderType_RefNbr).Select(new object[3]
    {
      (object) soAdjust.CustomerID,
      (object) soAdjust.AdjdOrderType,
      (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current.OrderNbr
    });
    if (pxResultset.Count != 1)
      throw new PXException("The import of the sales order failed.");
    PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(pxResultset).ExternalTaxesImportInProgress = order.ExternalTaxesImportInProgress;
    SOAdjust soAdjust2 = ((PXSelectBase<SOAdjust>) applyTabExtension.SOAdjustments).Insert(soAdjust1);
    PXNoteAttribute.CopyNoteAndFiles(((PXGraph) instance).Caches[typeof (ARPayment)], (object) arPayment2, ((PXGraph) instance).Caches[typeof (SOAdjust)], (object) soAdjust2, (PXNoteAttribute.IPXCopySettings) null);
    return instance;
  }

  public virtual void FillARPaymentAPI(
    ARPaymentEntry paymentEntry,
    ARPayment arpayment,
    SOAdjust soAdjust,
    PX.Objects.SO.SOOrder order)
  {
    if (soAdjust.AdjgDocDate.HasValue)
      arpayment.AdjDate = soAdjust.AdjgDocDate;
    arpayment.ExternalRef = soAdjust.ExternalRef;
    arpayment.CustomerID = order.CustomerID;
    arpayment.CustomerLocationID = order.CustomerLocationID;
    if (soAdjust.PaymentMethodID != null)
      ((PXSelectBase) paymentEntry.Document).Cache.SetValueExt<ARPayment.paymentMethodID>((object) arpayment, (object) soAdjust.PaymentMethodID);
    this.FillARRefundAPI(paymentEntry, arpayment, soAdjust, order);
    int? nullable = soAdjust.PMInstanceID;
    if (nullable.HasValue)
      ((PXSelectBase) paymentEntry.Document).Cache.SetValueExt<ARPayment.pMInstanceID>((object) arpayment, (object) soAdjust.PMInstanceID);
    nullable = soAdjust.CashAccountID;
    if (nullable.HasValue)
      ((PXSelectBase) paymentEntry.Document).Cache.SetValueExt<ARPayment.cashAccountID>((object) arpayment, (object) soAdjust.CashAccountID);
    if (soAdjust.ProcessingCenterID != null)
      ((PXSelectBase) paymentEntry.Document).Cache.SetValueExt<ARPayment.processingCenterID>((object) arpayment, (object) soAdjust.ProcessingCenterID);
    if (soAdjust.DocDesc != null)
      arpayment.DocDesc = soAdjust.DocDesc;
    if (soAdjust.ExtRefNbr != null)
      arpayment.ExtRefNbr = soAdjust.ExtRefNbr;
    if (soAdjust.ExternalRef != null)
      arpayment.ExternalRef = soAdjust.ExternalRef;
    if (!soAdjust.NewCard.HasValue)
    {
      if (soAdjust.SaveCard.HasValue)
        arpayment.SaveCard = soAdjust.SaveCard;
    }
    else
    {
      if (soAdjust.NewCard.GetValueOrDefault())
      {
        nullable = soAdjust.PMInstanceID;
        if (nullable.HasValue)
        {
          arpayment = (ARPayment) ((PXSelectBase) paymentEntry.Document).Cache.Update((object) arpayment);
          ((PXSelectBase) paymentEntry.Document).Cache.SetValueExt<ARPayment.pMInstanceID>((object) arpayment, (object) null);
          arpayment = (ARPayment) ((PXSelectBase) paymentEntry.Document).Cache.Update((object) arpayment);
        }
      }
      ((PXSelectBase) paymentEntry.Document).Cache.SetValueExt<ARPayment.newCard>((object) arpayment, (object) soAdjust.NewCard);
      switch (this.Base1.GetSavePaymentProfileCode(soAdjust.NewCard, arpayment.ProcessingCenterID))
      {
        case "A":
          arpayment.SaveCard = soAdjust.SaveCard;
          break;
        case "F":
          arpayment.SaveCard = new bool?(true);
          break;
      }
    }
    if (soAdjust.Hold.HasValue && !soAdjust.Capture.GetValueOrDefault() && !soAdjust.Authorize.GetValueOrDefault() && !soAdjust.Refund.GetValueOrDefault())
      arpayment.Hold = soAdjust.Hold;
    arpayment.CuryOrigDocAmt = soAdjust.CuryOrigDocAmt;
  }

  public virtual void FillARRefundAPI(
    ARPaymentEntry paymentEntry,
    ARPayment arpayment,
    SOAdjust soAdjust,
    PX.Objects.SO.SOOrder order)
  {
    if (arpayment.DocType != "REF")
      return;
    if (soAdjust.RefTranExtNbr != null)
    {
      ((PXSelectBase) paymentEntry.Document).Cache.SetValueExt<ARPayment.cCTransactionRefund>((object) arpayment, (object) true);
      ((PXSelectBase) paymentEntry.Document).Cache.SetValueExt<ARPayment.refTranExtNbr>((object) arpayment, (object) soAdjust.RefTranExtNbr);
    }
    else
      ((PXSelectBase) paymentEntry.Document).Cache.SetValueExt<ARPayment.cCTransactionRefund>((object) arpayment, (object) false);
  }

  protected virtual void CreatePaymentAPI(SOAdjust soAdjust, string paymentType)
  {
    ARPaymentEntry paymentApi = this.CreatePaymentAPI(soAdjust, ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current, paymentType);
    ((PXAction) paymentApi.Save).Press();
    ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Adjustments).Cache.Remove((object) soAdjust);
    try
    {
      bool? nullable = soAdjust.Capture;
      if (nullable.GetValueOrDefault())
      {
        this.Base1.PressButtonIfEnabled((PXGraph) paymentApi, "captureCCPayment");
      }
      else
      {
        nullable = soAdjust.Authorize;
        if (nullable.GetValueOrDefault())
        {
          this.Base1.PressButtonIfEnabled((PXGraph) paymentApi, "authorizeCCPayment");
        }
        else
        {
          nullable = soAdjust.Refund;
          if (!nullable.GetValueOrDefault())
            return;
          nullable = ((PXSelectBase<ARPayment>) paymentApi.Document).Current.Hold;
          bool flag = false;
          if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
            return;
          this.Base1.PressButtonIfEnabled((PXGraph) paymentApi, "creditCCPayment");
        }
      }
    }
    catch (PXBaseRedirectException ex)
    {
      throw;
    }
    catch (Exception ex)
    {
      this.Base1.RedirectToNewGraph(paymentApi, ex);
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<SOAdjust> e)
  {
    SOAdjust row = e.Row;
    if (row == null || !((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<SOAdjust>>) e).Cache.Graph.IsContractBasedAPI || row.AdjgRefNbr != null)
      return;
    e.Cancel = true;
  }

  /// Overrides <see cref="M:PX.Objects.SO.SOOrderEntry.Persist" />
  [PXOverride]
  public virtual void Persist(CreatePaymentAPIExt.PersistDelegate baseMethod)
  {
    if (((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Adjustments).Cache.Graph.IsContractBasedAPI && ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Adjustments).Cache.Inserted.Count() != 0L)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CreatePaymentAPIExt.\u003C\u003Ec__DisplayClass8_0 cDisplayClass80 = new CreatePaymentAPIExt.\u003C\u003Ec__DisplayClass8_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass80.createPayments = (Action) (() =>
      {
        foreach (SOAdjust soAdjust in ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Adjustments).Cache.Inserted)
        {
          if (soAdjust.AdjgRefNbr == null)
            this.CreatePaymentAPI(soAdjust, soAdjust.AdjgDocType ?? "PMT");
        }
        ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Adjustments).Cache.ClearQueryCache();
      });
      if (((IEnumerable<SOAdjust>) ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Adjustments).Cache.Inserted).Any<SOAdjust>((Func<SOAdjust, bool>) (a => a.Authorize.GetValueOrDefault() || a.Capture.GetValueOrDefault())))
      {
        try
        {
          ++this._nesting;
          try
          {
            ((PXGraphExtension<SOOrderEntry>) this).Base.RecalculateExternalTaxesSync = true;
            baseMethod();
          }
          finally
          {
            ((PXGraphExtension<SOOrderEntry>) this).Base.RecalculateExternalTaxesSync = false;
          }
          if (this._nesting != 1)
            return;
          // ISSUE: method pointer
          PXLongOperation.StartOperation((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new PXToggleAsyncDelegate((object) cDisplayClass80, __methodptr(\u003CPersist\u003Eb__2)));
        }
        finally
        {
          --this._nesting;
        }
      }
      else
      {
        PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current;
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          if (current != null && ((PXGraphExtension<SOOrderEntry>) this).Base.IsExternalTax(current.TaxZoneID) && !current.ExternalTaxesImportInProgress.GetValueOrDefault())
          {
            try
            {
              ((PXGraphExtension<SOOrderEntry>) this).Base.RecalculateExternalTaxesSync = true;
              baseMethod();
            }
            finally
            {
              ((PXGraphExtension<SOOrderEntry>) this).Base.RecalculateExternalTaxesSync = false;
            }
          }
          else
            baseMethod();
          // ISSUE: reference to a compiler-generated field
          cDisplayClass80.createPayments();
          transactionScope.Complete();
        }
      }
    }
    else
      baseMethod();
  }

  public delegate void PersistDelegate();
}
