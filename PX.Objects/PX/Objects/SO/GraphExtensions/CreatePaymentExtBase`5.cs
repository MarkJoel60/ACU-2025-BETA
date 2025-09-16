// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.CreatePaymentExtBase`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase;
using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.GraphExtensions;
using PX.Objects.CA;
using PX.Objects.CC.PaymentProcessing;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Abstractions;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.Extensions.PaymentTransaction;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.SO.DAC;
using PX.Objects.SO.DAC.Unbound;
using PX.Objects.SO.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

#nullable disable
namespace PX.Objects.SO.GraphExtensions;

public abstract class CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph<TFirstGraph, TDocument>, new()
  where TFirstGraph : PXGraph
  where TDocument : class, IBqlTable, ICreatePaymentDocument, new()
  where TDocumentAdjust : class, IBqlTable, ICreatePaymentAdjust, new()
  where TPaymentAdjust : class, IBqlTable, new()
{
  [PXCopyPasteHiddenView]
  public PXFilter<SOQuickPayment> QuickPayment;
  [PXCopyPasteHiddenView]
  public PXFilter<SOImportExternalTran> ImportExternalTran;
  [PXCopyPasteHiddenView]
  public PXSelect<ARPaymentTotals> PaymentTotals;
  [PXCopyPasteHiddenView]
  public PXFilter<SOIncreaseAuthorizedAmountDialog> IncreaseAmount;
  public PXAction<TDocument> createDocumentPayment;
  public PXAction<TDocument> createDocumentRefund;
  public PXAction<TDocument> increaseAuthorizedOK;
  public PXAction<TDocument> increaseAndCaptureAuthorizedOK;
  public PXAction<TDocument> createPaymentOK;
  public PXAction<TDocument> createPaymentCapture;
  public PXAction<TDocument> createPaymentAuthorize;
  public PXAction<TDocument> createPaymentRefund;
  public PXAction<TDocument> createAndAuthorizePayment;
  public PXAction<TDocument> createAndCapturePayment;
  public PXAction<TDocument> syncPaymentTransaction;
  public PXAction<TDocument> captureDocumentPayment;
  public PXAction<TDocument> voidDocumentPayment;
  public PXAction<TDocument> viewPayment;
  public PXAction<TDocument> importDocumentPayment;
  public PXAction<TDocument> importDocumentPaymentCreate;
  public PXAction<TDocument> increaseAuthorizedAmount;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  internal PaymentConnectorCallbackService PaymentCallbackService { get; set; }

  protected virtual DAC GetCurrent<DAC>() where DAC : class, IBqlTable, new()
  {
    return (DAC) ((PXCache) GraphHelper.Caches<DAC>((PXGraph) (object) this.Base)).Current;
  }

  protected static CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust> CreateNewGraph(
    TDocument document)
  {
    CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust> implementation = ((PXGraph) (object) PXGraph.CreateInstance<TGraph>()).FindImplementation<CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>>();
    implementation.SetCurrentDocument(document);
    return implementation;
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXGraph) (object) this.Base).Views.Caches.Remove(((PXSelectBase<SOQuickPayment>) this.QuickPayment).GetItemType());
    ((PXGraph) (object) this.Base).Views.Caches.Remove(((PXSelectBase<SOImportExternalTran>) this.ImportExternalTran).GetItemType());
    ((PXGraph) (object) this.Base).Views.Caches.Remove(((PXSelectBase<SOIncreaseAuthorizedAmountDialog>) this.IncreaseAmount).GetItemType());
    if (!((PXSelectBase) this.GetAdjustView()).Cache.Fields.Contains("CanVoid"))
    {
      ((PXSelectBase) this.GetAdjustView()).Cache.Fields.Add("CanVoid");
      ((PXGraph) (object) this.Base).FieldSelecting.AddHandler(typeof (TDocumentAdjust), "CanVoid", this.CreateVoidCaptureFieldSelecting("CanVoid", new Func<TDocumentAdjust, PX.Objects.AR.ARPayment, bool>(this.CanVoid)));
    }
    if (!((PXSelectBase) this.GetAdjustView()).Cache.Fields.Contains("CanCapture"))
    {
      ((PXSelectBase) this.GetAdjustView()).Cache.Fields.Add("CanCapture");
      ((PXGraph) (object) this.Base).FieldSelecting.AddHandler(typeof (TDocumentAdjust), "CanCapture", this.CreateVoidCaptureFieldSelecting("CanCapture", new Func<TDocumentAdjust, PX.Objects.AR.ARPayment, bool>(this.CanCapture)));
    }
    if (!((PXSelectBase) this.GetAdjustView()).Cache.Fields.Contains("CanIncreaseAuthorizedAmount"))
    {
      ((PXSelectBase) this.GetAdjustView()).Cache.Fields.Add("CanIncreaseAuthorizedAmount");
      ((PXGraph) (object) this.Base).FieldSelecting.AddHandler(typeof (TDocumentAdjust), "CanIncreaseAuthorizedAmount", this.CreateVoidCaptureFieldSelecting("CanIncreaseAuthorizedAmount", new Func<TDocumentAdjust, PX.Objects.AR.ARPayment, bool>(this.CanIncreaseAuthorizedAmount)));
    }
    Type paymentMethodField = this.GetPaymentMethodField();
    PXGraph.FieldVerifyingEvents fieldVerifying = ((PXGraph) (object) this.Base).FieldVerifying;
    Type itemType = BqlCommand.GetItemType(paymentMethodField);
    string name = paymentMethodField.Name;
    CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust> createPaymentExtBase = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) createPaymentExtBase, __vmethodptr(createPaymentExtBase, PaymentMethodFieldVerifying));
    fieldVerifying.AddHandler(itemType, name, pxFieldVerifying);
  }

  protected virtual void InitializeQuickPaymentPanel(PXGraph graph, string viewName)
  {
    this.SetDefaultValues(((PXSelectBase<SOQuickPayment>) this.QuickPayment).Current, this.GetCurrent<TDocument>());
    ((PXSelectBase) this.QuickPayment).Cache.RaiseRowSelected((object) ((PXSelectBase<SOQuickPayment>) this.QuickPayment).Current);
  }

  public virtual void SetDefaultValues(SOQuickPayment payment, TDocument document)
  {
    this.ClearQuickPayment(payment);
    if ((object) document == null)
      return;
    PXCache cache = ((PXSelectBase) this.QuickPayment).Cache;
    cache.SetValueExt<SOQuickPayment.paymentMethodID>((object) payment, (object) document.PaymentMethodID);
    cache.SetValueExt<SOQuickPayment.cashAccountID>((object) payment, (object) document.CashAccountID);
    cache.SetValueExt<SOQuickPayment.docDesc>((object) payment, (object) this.GetDocumentDescr(document));
    if (document.PMInstanceID.HasValue)
    {
      this.VerifyHasActiveCustomerPaymentMethod(document);
      cache.SetValueExt<SOQuickPayment.pMInstanceID>((object) payment, (object) document.PMInstanceID);
    }
    cache.SetDefaultExt<SOQuickPayment.curyOrigDocAmt>((object) payment);
    cache.SetDefaultExt<SOQuickPayment.curyRefundAmt>((object) payment);
    cache.SetDefaultExt<SOQuickPayment.newCard>((object) payment);
    cache.SetDefaultExt<SOQuickPayment.saveCard>((object) payment);
    cache.SetDefaultExt<SOQuickPayment.refTranExtNbr>((object) payment);
  }

  protected virtual void ClearQuickPayment(SOQuickPayment quickPayment)
  {
    quickPayment.CashAccountID = new int?();
    quickPayment.CuryOrigDocAmt = new Decimal?();
    quickPayment.CuryRefundAmt = new Decimal?();
    quickPayment.DocDesc = (string) null;
    quickPayment.ExtRefNbr = (string) null;
    quickPayment.NewCard = new bool?();
    quickPayment.NewAccount = new bool?();
    quickPayment.SaveCard = new bool?();
    quickPayment.SaveAccount = new bool?();
    quickPayment.OrigDocAmt = new Decimal?();
    quickPayment.RefundAmt = new Decimal?();
    quickPayment.PaymentMethodID = (string) null;
    quickPayment.PaymentMethodProcCenterID = (string) null;
    quickPayment.RefTranExtNbr = (string) null;
    quickPayment.PMInstanceID = new int?();
    quickPayment.ProcessingCenterID = (string) null;
    quickPayment.UpdateNextNumber = new bool?();
    quickPayment.Authorize = new bool?();
    quickPayment.Capture = new bool?();
    quickPayment.Refund = new bool?();
    quickPayment.AuthorizeRemainder = new bool?();
    quickPayment.AdjgDocType = (string) null;
    quickPayment.AdjgRefNbr = (string) null;
    quickPayment.PreviousExternalTransactionID = (string) null;
    ((PXSelectBase<SOQuickPayment>) this.QuickPayment).Update(quickPayment);
  }

  protected virtual void InitializeImportPaymentPanel(PXGraph graph, string viewName)
  {
    this.SetDefaultValues(((PXSelectBase<SOImportExternalTran>) this.ImportExternalTran).Current, this.GetCurrent<TDocument>());
    ((PXSelectBase) this.ImportExternalTran).Cache.RaiseRowSelected((object) ((PXSelectBase<SOImportExternalTran>) this.ImportExternalTran).Current);
  }

  public virtual void SetDefaultValues(SOImportExternalTran panel, TDocument document)
  {
    this.ClearImportPayment(panel);
    if ((object) document == null)
      return;
    PXCache cache = ((PXSelectBase) this.ImportExternalTran).Cache;
    cache.SetValueExt<SOImportExternalTran.paymentMethodID>((object) panel, (object) document.PaymentMethodID);
    cache.SetDefaultExt<SOImportExternalTran.processingCenterID>((object) panel);
  }

  protected virtual void ClearImportPayment(SOImportExternalTran panel)
  {
    panel.PaymentMethodID = (string) null;
    panel.PMInstanceID = new int?();
    panel.TranNumber = (string) null;
    panel.ProcessingCenterID = (string) null;
  }

  protected virtual void InitializeIncreaseAuthorizedAmountPanel(PXGraph graph, string viewName)
  {
    TDocumentAdjust current1 = this.GetAdjustView().Current;
    TDocument current2 = this.GetCurrent<TDocument>();
    if ((object) current1 == null || (object) current2 == null)
      return;
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Search<PX.Objects.AR.ARPayment.refNbr>((object) current1.AdjgRefNbr, new object[1]
    {
      (object) current1.AdjgDocType
    }));
    PX.Objects.AR.ARPayment current3 = ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current;
    if (current3 == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.AR.ARPayment>((PXGraph) (object) this.Base), new object[2]
      {
        (object) current1.AdjgDocType,
        (object) current1.AdjgRefNbr
      });
    SOIncreaseAuthorizedAmountDialog current4 = ((PXSelectBase<SOIncreaseAuthorizedAmountDialog>) this.IncreaseAmount).Current;
    current4.CuryInfoID = current3.CuryInfoID;
    current4.CuryID = current3.CuryID;
    current4.InstanceType = current3.DocType;
    current4.ReferenceNumber = current3.RefNbr;
    current4.CuryAuthorizedAmt = current3.CuryOrigDocAmt;
    current4.WarningMessage = string.Empty;
    try
    {
      this.VerifyAdjustments(instance, "IncreaseAuthorizedAmount");
    }
    catch (PXException ex)
    {
      current4.WarningMessage = ex.MessageNoPrefix;
      ((PXAction) this.increaseAndCaptureAuthorizedOK).SetEnabled(false);
    }
    if (current3.CuryID != current2.CuryID)
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = ((PXGraph) instance).GetExtension<ARPaymentEntry.MultiCurrency>().GetCurrencyInfo(current3.CuryInfoID);
      Decimal valueOrDefault1 = current2.UnpaidBalance.GetValueOrDefault();
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = currencyInfo1;
      Decimal? nullable1 = current4.CuryAuthorizedAmt;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      Decimal num = currencyInfo2.CuryConvBase(valueOrDefault2);
      current4.AuthorizedAmtNew = new Decimal?(valueOrDefault1 + num);
      SOIncreaseAuthorizedAmountDialog authorizedAmountDialog = current4;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = currencyInfo1;
      nullable1 = current4.AuthorizedAmtNew;
      Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
      Decimal? nullable2 = new Decimal?(currencyInfo3.CuryConvCury(valueOrDefault3));
      authorizedAmountDialog.CuryAuthorizedAmtNew = nullable2;
    }
    else
    {
      SOIncreaseAuthorizedAmountDialog authorizedAmountDialog = current4;
      Decimal? curyAuthorizedAmt = current4.CuryAuthorizedAmt;
      Decimal? curyUnpaidBalance = current2.CuryUnpaidBalance;
      Decimal? nullable = curyAuthorizedAmt.HasValue & curyUnpaidBalance.HasValue ? new Decimal?(curyAuthorizedAmt.GetValueOrDefault() + curyUnpaidBalance.GetValueOrDefault()) : new Decimal?();
      authorizedAmountDialog.CuryAuthorizedAmtNew = nullable;
    }
    current4.CuryAuthorizedAmtNew = new Decimal?(PX.Objects.CM.PXCurrencyAttribute.Round(((PXGraph) instance).Caches[typeof (SOIncreaseAuthorizedAmountDialog)], (object) current4, current4.CuryAuthorizedAmtNew.GetValueOrDefault(), CMPrecision.BASECURY));
    current4.CuryOrigAdjAmt = new Decimal?(((Decimal?) current1?.CuryAdjgAmt).GetValueOrDefault());
    current4.CuryOrigAdjAmtNew = PXFormulaAttribute.Evaluate<SOIncreaseAuthorizedAmountDialog.curyOrigAdjAmtNew>(((PXSelectBase) this.IncreaseAmount).Cache, (object) current4) as Decimal?;
    current4.CuryAuthorizedAmtMax = current4.CuryAuthorizedAmtNew;
  }

  [PXUIField]
  [PXButton(ImageKey = "AddNew", Tooltip = "Create Payment", DisplayOnMainToolbar = false, PopupCommand = "syncPaymentTransaction")]
  protected virtual IEnumerable CreateDocumentPayment(PXAdapter adapter)
  {
    if (this.AskCreatePaymentDialog("Create Payment") == 1)
      this.CreatePayment(((PXSelectBase<SOQuickPayment>) this.QuickPayment).Current, "PMT", false);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "AddNew", Tooltip = "Create Refund", VisibleOnDataSource = false, PopupCommand = "syncPaymentTransaction")]
  protected virtual IEnumerable CreateDocumentRefund(PXAdapter adapter)
  {
    ((PXAction) this.Base.Save).Press();
    if (this.AskCreatePaymentDialog("Create Refund") == 1)
      this.CreatePayment(((PXSelectBase<SOQuickPayment>) this.QuickPayment).Current, "REF", false);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual IEnumerable IncreaseAuthorizedOK(PXAdapter adapter)
  {
    if (!this.CheckIncreaseAmount())
      throw new PXActionDisabledException("IncreazeAuthorizedCCPayment");
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual IEnumerable IncreaseAndCaptureAuthorizedOK(PXAdapter adapter)
  {
    if (!this.CheckIncreaseAmount())
      throw new PXActionDisabledException("IncreazeAuthorizedCCPayment");
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual IEnumerable CreatePaymentOK(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual IEnumerable CreatePaymentCapture(PXAdapter adapter)
  {
    this.AssignCapture();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual IEnumerable CreatePaymentAuthorize(PXAdapter adapter)
  {
    this.AssignAuthorize();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(VisibleOnDataSource = false)]
  protected virtual IEnumerable CreatePaymentRefund(PXAdapter adapter)
  {
    this.AssignRefund();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Create and Authorize", Visible = false, Enabled = false)]
  [PXButton]
  protected virtual IEnumerable CreateAndAuthorizePayment(PXAdapter adapter)
  {
    List<TDocument> list = adapter.Get<TDocument>().ToList<TDocument>();
    foreach (TDocument doc in list)
      this.CreateCCPayment(doc, new Action(this.AssignAuthorize));
    return (IEnumerable) list;
  }

  [PXUIField(DisplayName = "Create and Capture", Visible = false, Enabled = false)]
  [PXButton]
  protected virtual IEnumerable CreateAndCapturePayment(PXAdapter adapter)
  {
    List<TDocument> list = adapter.Get<TDocument>().ToList<TDocument>();
    foreach (TDocument doc in list)
      this.CreateCCPayment(doc, new Action(this.AssignCapture));
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable SyncPaymentTransaction(PXAdapter adapter)
  {
    if (!string.IsNullOrEmpty(((PXSelectBase<SOQuickPayment>) this.QuickPayment).Current.AdjgDocType) && !string.IsNullOrEmpty(((PXSelectBase<SOQuickPayment>) this.QuickPayment).Current.AdjgRefNbr))
    {
      bool? nullable = ((PXSelectBase<SOQuickPayment>) this.QuickPayment).Current.NewCard;
      if (nullable.GetValueOrDefault())
      {
        nullable = ((PXSelectBase<SOQuickPayment>) this.QuickPayment).Current.Authorize;
        if (!nullable.GetValueOrDefault())
        {
          nullable = ((PXSelectBase<SOQuickPayment>) this.QuickPayment).Current.Capture;
          if (!nullable.GetValueOrDefault())
            goto label_5;
        }
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.\u003C\u003Ec__DisplayClass49_0 cDisplayClass490 = new CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.\u003C\u003Ec__DisplayClass49_0();
        string commandArguments = adapter.CommandArguments;
        PaymentConnectorCallbackParams connectorCallbackParams = !string.IsNullOrEmpty(commandArguments) ? this.PaymentCallbackService.FromCommandArguments(commandArguments) : this.PaymentCallbackService.FromCCPaymentPanelCallback();
        CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.SyncPaymentTransactionParameter transactionParameter = new CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.SyncPaymentTransactionParameter();
        transactionParameter.Document = this.GetCurrent<TDocument>();
        transactionParameter.AdjgDocType = ((PXSelectBase<SOQuickPayment>) this.QuickPayment).Current.AdjgDocType;
        transactionParameter.AdjgRefNbr = ((PXSelectBase<SOQuickPayment>) this.QuickPayment).Current.AdjgRefNbr;
        nullable = connectorCallbackParams.IsCancelled;
        transactionParameter.CancelStr = nullable.GetValueOrDefault() ? true.ToString() : (string) null;
        transactionParameter.TranResponseStr = connectorCallbackParams.TransactionId;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass490.syncParameter = transactionParameter;
        // ISSUE: method pointer
        PXLongOperation.StartOperation((PXGraph) (object) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass490, __methodptr(\u003CSyncPaymentTransaction\u003Eb__0)));
        return adapter.Get();
      }
    }
label_5:
    return ((PXAction) this.Base.Cancel).Press(adapter);
  }

  protected virtual void ProcessSyncPaymentTransaction(
    CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.SyncPaymentTransactionParameter parameter)
  {
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    ARPaymentEntryPaymentTransaction extension = ((PXGraph) instance).GetExtension<ARPaymentEntryPaymentTransaction>();
    extension.SetContextString("__CLOSECCHFORM", parameter.CancelStr);
    extension.SetContextString("__TRANID", parameter.TranResponseStr);
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Search<PX.Objects.AR.ARPayment.refNbr>((object) parameter.AdjgRefNbr, new object[1]
    {
      (object) parameter.AdjgDocType
    }));
    try
    {
      this.PressButtonIfEnabled((PXGraph) instance, "syncPaymentTransaction");
    }
    catch (Exception ex)
    {
      this.RedirectToNewGraph(instance, ex);
    }
    finally
    {
      extension.SetContextString("__CLOSECCHFORM", (string) null);
      extension.SetContextString("__TRANID", (string) null);
    }
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable CaptureDocumentPayment(PXAdapter adapter)
  {
    // ISSUE: variable of a boxed type
    __Boxed<TDocumentAdjust> current = (object) this.GetCurrent<TDocumentAdjust>();
    int num1;
    if (current == null)
    {
      num1 = 0;
    }
    else
    {
      Decimal? curyAdjdAmt = current.CuryAdjdAmt;
      Decimal num2 = 0M;
      num1 = curyAdjdAmt.GetValueOrDefault() == num2 & curyAdjdAmt.HasValue ? 1 : 0;
    }
    if (num1 != 0)
      throw new PXException("Amount to capture must be greater than zero.");
    this.ExecutePaymentTransactionAction((Action<CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>, TDocumentAdjust, ARPaymentEntry, ARPaymentEntryPaymentTransaction>) ((createPaymentExt, adjustment, payment, transaction) => createPaymentExt.CapturePayment(adjustment, payment, transaction)));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable VoidDocumentPayment(PXAdapter adapter)
  {
    this.ExecutePaymentTransactionAction((Action<CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>, TDocumentAdjust, ARPaymentEntry, ARPaymentEntryPaymentTransaction>) ((createPaymentExt, adjustment, payment, transaction) => createPaymentExt.VoidPayment(adjustment, payment, transaction)));
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewPayment(PXAdapter adapter)
  {
    TDocumentAdjust current = this.GetCurrent<TDocumentAdjust>();
    if ((object) this.GetCurrent<TDocument>() == null || (object) current == null)
      return adapter.Get();
    if (current.AdjgDocType == "PPI")
    {
      ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) current.AdjgRefNbr, new object[1]
      {
        (object) current.AdjgDocType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Payment");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    ARPaymentEntry instance1 = PXGraph.CreateInstance<ARPaymentEntry>();
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance1.Document).Current = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARPayment>) instance1.Document).Search<PX.Objects.AR.ARPayment.refNbr>((object) current.AdjgRefNbr, new object[1]
    {
      (object) current.AdjgDocType
    }));
    PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, true, "Payment");
    ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException1;
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable ImportDocumentPayment(PXAdapter adapter)
  {
    PXFilter<SOImportExternalTran> importExternalTran = this.ImportExternalTran;
    CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust> createPaymentExtBase = this;
    // ISSUE: virtual method pointer
    PXView.InitializePanel initializePanel = new PXView.InitializePanel((object) createPaymentExtBase, __vmethodptr(createPaymentExtBase, InitializeImportPaymentPanel));
    if (((PXSelectBase<SOImportExternalTran>) importExternalTran).AskExt(initializePanel, true) == 1)
      this.ImportPayment(((PXSelectBase<SOImportExternalTran>) this.ImportExternalTran).Current);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual IEnumerable ImportDocumentPaymentCreate(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual IEnumerable IncreaseAuthorizedAmount(PXAdapter adapter)
  {
    TDocumentAdjust current1 = this.GetAdjustView().Current;
    TDocument current2 = this.GetCurrent<TDocument>();
    if ((object) current1 == null || (object) current2 == null)
      return adapter.Get();
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Search<PX.Objects.AR.ARPayment.refNbr>((object) current1.AdjgRefNbr, new object[1]
    {
      (object) current1.AdjgDocType
    }));
    PX.Objects.AR.ARPayment current3 = ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current;
    PX.Objects.Extensions.PaymentTransaction.Payment extension = PXCacheEx.GetExtension<PX.Objects.Extensions.PaymentTransaction.Payment>((IBqlTable) current3);
    string documentType = this.GetDocumentType(current2);
    string documentNbr = this.GetDocumentNbr(current2);
    this.CheckUnappliedBalance(instance, current1, current2);
    if (!this.CanIncreaseAuthorizedAmount(current1, current3))
      throw new PXActionDisabledException("IncreazeAuthorizedCCPayment");
    (WebDialogResult webDialogResult, bool capture) tuple = this.AskIncreaseAuthorizedAmountDialog("Increase Authorized Amount");
    if (tuple.webDialogResult == 1)
    {
      if (tuple.capture)
      {
        if (!this.CanIncreaseAndCapture)
          throw new PXActionDisabledException("IncreazeAuthorizedCCPayment");
        this.VerifyAdjustments(instance, nameof (IncreaseAuthorizedAmount));
      }
      if (!this.CheckIncreaseAmount())
        throw new PXActionDisabledException("IncreazeAuthorizedCCPayment");
      SOIncreaseAuthorizedAmountDialog current4 = ((PXSelectBase<SOIncreaseAuthorizedAmountDialog>) this.IncreaseAmount).Current;
      Decimal? authorizedAmtNew = current4.CuryAuthorizedAmtNew;
      Decimal? curyOrigAdjAmtNew = current4.CuryOrigAdjAmtNew;
      if (authorizedAmtNew.HasValue)
      {
        extension.CuryDocBalIncrease = authorizedAmtNew;
        extension.TransactionOrigDocType = documentType;
        extension.TransactionOrigDocRefNbr = documentNbr;
        extension.OrigDocAppliedAmount = curyOrigAdjAmtNew;
        ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Update(current3);
        ((PXAction) instance.Save).Press();
        this.IncreaseCCAuthorizedAmount(instance, tuple.capture);
      }
    }
    return adapter.Get();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.paymentMethodID> eventArgs)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.paymentMethodID>>) eventArgs).Cache.SetDefaultExt<SOQuickPayment.paymentMethodProcCenterID>((object) eventArgs.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.paymentMethodID>>) eventArgs).Cache.SetDefaultExt<SOQuickPayment.pMInstanceID>((object) eventArgs.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.paymentMethodID>>) eventArgs).Cache.SetDefaultExt<SOQuickPayment.cashAccountID>((object) eventArgs.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.paymentMethodID>>) eventArgs).Cache.SetDefaultExt<SOQuickPayment.extRefNbr>((object) eventArgs.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.paymentMethodID>>) eventArgs).Cache.SetDefaultExt<SOQuickPayment.refTranExtNbr>((object) eventArgs.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.paymentMethodID>>) eventArgs).Cache.SetDefaultExt<SOQuickPayment.newCard>((object) eventArgs.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.paymentMethodID>>) eventArgs).Cache.SetDefaultExt<SOQuickPayment.processingCenterID>((object) eventArgs.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.paymentMethodID>>) eventArgs).Cache.SetDefaultExt<SOQuickPayment.saveCard>((object) eventArgs.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.paymentMethodID>>) eventArgs).Cache.SetDefaultExt<SOQuickPayment.terminalID>((object) eventArgs.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.refTranExtNbr> eventArgs)
  {
    PX.Objects.AR.ExternalTransaction externalTransaction = (PX.Objects.AR.ExternalTransaction) PXSelectorAttribute.Select<SOQuickPayment.refTranExtNbr>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.refTranExtNbr>>) eventArgs).Cache, (object) eventArgs.Row);
    if (externalTransaction == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.refTranExtNbr>>) eventArgs).Cache.SetValueExt<SOQuickPayment.pMInstanceID>((object) eventArgs.Row, (object) externalTransaction.PMInstanceID);
    object curyRefundAmt = (object) eventArgs.Row.CuryRefundAmt;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.refTranExtNbr>>) eventArgs).Cache.RaiseFieldVerifying<SOQuickPayment.curyRefundAmt>((object) eventArgs.Row, ref curyRefundAmt);
  }

  protected virtual void _(PX.Data.Events.RowSelected<SOQuickPayment> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    PX.Objects.CA.PaymentMethod pm = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) (object) this.Base, eventArgs.Row.PaymentMethodID);
    bool flag1 = pm != null && EnumerableExtensions.IsIn<string>(pm.PaymentType, "CCD", "EFT", "POS");
    bool flag2 = pm?.PaymentType == "EFT";
    bool flag3 = pm?.PaymentType == "POS";
    bool? nullable;
    int num1;
    if (flag1)
    {
      nullable = pm.IsAccountNumberRequired;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag4 = num1 != 0;
    nullable = eventArgs.Row.NewCard;
    bool valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = eventArgs.Row.IsRefund;
    bool valueOrDefault2 = nullable.GetValueOrDefault();
    nullable = eventArgs.Row.AllowUnlinkedRefund;
    bool valueOrDefault3 = nullable.GetValueOrDefault();
    bool flag5 = !string.IsNullOrEmpty(eventArgs.Row.RefTranExtNbr);
    PXUIFieldAttribute.SetVisible<SOQuickPayment.curyOrigDocAmt>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, !valueOrDefault2);
    PXUIFieldAttribute.SetEnabled<SOQuickPayment.curyOrigDocAmt>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, !valueOrDefault2);
    PXUIFieldAttribute.SetRequired<SOQuickPayment.curyOrigDocAmt>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, !valueOrDefault2);
    PXUIFieldAttribute.SetVisible<SOQuickPayment.curyRefundAmt>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, valueOrDefault2);
    PXUIFieldAttribute.SetEnabled<SOQuickPayment.curyRefundAmt>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, valueOrDefault2);
    PXUIFieldAttribute.SetRequired<SOQuickPayment.curyRefundAmt>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, valueOrDefault2);
    PXUIFieldAttribute.SetVisible<SOQuickPayment.pMInstanceID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, flag4);
    PXUIFieldAttribute.SetEnabled<SOQuickPayment.pMInstanceID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, flag4 && !valueOrDefault1 && !flag5 && !valueOrDefault2 | valueOrDefault3);
    PXUIFieldAttribute.SetRequired<SOQuickPayment.pMInstanceID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, flag4 && !valueOrDefault1 && !flag5);
    PXUIFieldAttribute.SetVisible<SOQuickPayment.processingCenterID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, flag4 | flag3);
    PXUIFieldAttribute.SetEnabled<SOQuickPayment.processingCenterID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, flag4 & valueOrDefault1 | flag3);
    PXUIFieldAttribute.SetRequired<SOQuickPayment.processingCenterID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, flag4 | flag3);
    PXUIFieldAttribute.SetVisible<SOQuickPayment.terminalID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, flag3 && !valueOrDefault2);
    PXUIFieldAttribute.SetEnabled<SOQuickPayment.terminalID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, flag3 && !valueOrDefault2);
    PXUIFieldAttribute.SetRequired<SOQuickPayment.terminalID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, flag3 && !valueOrDefault2);
    PXUIFieldAttribute.SetVisible<SOQuickPayment.refTranExtNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, flag4 & valueOrDefault2);
    PXUIFieldAttribute.SetEnabled<SOQuickPayment.refTranExtNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, flag4 & valueOrDefault2);
    PXUIFieldAttribute.SetRequired<SOQuickPayment.refTranExtNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, flag4 & valueOrDefault2 && !valueOrDefault3);
    ARSetup arSetup = this.GetARSetup();
    PXUIFieldAttribute.SetVisible<SOQuickPayment.extRefNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, !flag4 && !flag3);
    PXUIFieldAttribute.SetEnabled<SOQuickPayment.extRefNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, !flag4 && !flag3);
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache;
    int num2;
    if (!flag4)
    {
      nullable = arSetup.RequireExtRef;
      if (nullable.GetValueOrDefault())
      {
        num2 = !flag3 ? 1 : 0;
        goto label_9;
      }
    }
    num2 = 0;
label_9:
    PXUIFieldAttribute.SetRequired<SOQuickPayment.extRefNbr>(cache, num2 != 0);
    ((PXAction) this.createPaymentOK).SetVisible(!CCProcessingHelper.PaymentMethodSupportsIntegratedProcessing(pm));
    ((PXAction) this.createPaymentCapture).SetVisible(CCProcessingHelper.PaymentMethodSupportsIntegratedProcessing(pm) && !valueOrDefault2);
    ((PXAction) this.createPaymentAuthorize).SetVisible(CCProcessingHelper.PaymentMethodSupportsIntegratedProcessing(pm) && this.AllowsAuthorization(eventArgs.Row));
    ((PXAction) this.createPaymentRefund).SetVisible(CCProcessingHelper.PaymentMethodSupportsIntegratedProcessing(pm) & valueOrDefault2);
    bool flag6 = this.PaymentHasError(eventArgs.Row);
    bool flag7 = this.IsActualFinPeriodClosed();
    CCProcessingCenter processingCenter = CCProcessingCenter.PK.Find((PXGraph) (object) this.Base, eventArgs.Row.ProcessingCenterID);
    int num3;
    if (processingCenter == null)
    {
      num3 = 0;
    }
    else
    {
      nullable = processingCenter.IsExternalAuthorizationOnly;
      num3 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    bool flag8 = num3 != 0;
    bool flag9 = CCPluginTypeHelper.IsProcCenterFeatureDisabled(processingCenter?.ProcessingTypeName);
    UIState.RaiseOrHideError<SOQuickPayment.processingCenterID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, (flag8 ? 1 : 0) != 0, "The {0} processing center does not support the Authorize action. The Capture action is supported only for payments that were pre-authorized externally.", (PXErrorLevel) 2, (object) eventArgs.Row.ProcessingCenterID);
    if (flag7 & flag1)
      ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache.RaiseExceptionHandling<SOQuickPayment.paymentMethodID>((object) eventArgs.Row, (object) eventArgs.Row.PaymentMethodID, (Exception) new PXSetPropertyException("The operation cannot be performed because the financial period of the current date is either closed, inactive, or does not exist for the {0} company. To process the payment with a credit card, EFT, or POS terminal, create or reopen the financial period.", (PXErrorLevel) 2, new object[1]
      {
        (object) ((PXAccess.Organization) PXAccess.GetBranch(PXContext.GetBranchID()).Organization).OrganizationCD
      }));
    ((PXAction) this.createPaymentOK).SetEnabled(!flag6);
    ((PXAction) this.createPaymentCapture).SetEnabled(!(flag6 | flag8 | flag7 | flag9));
    ((PXAction) this.createPaymentAuthorize).SetEnabled(!(flag6 | flag8 | flag7 | flag2 | flag9));
    ((PXAction) this.createPaymentRefund).SetEnabled(!(flag6 | flag8) | flag7 | flag9);
    bool flag10 = flag4 && !valueOrDefault2 && GraphHelper.RowCast<CCProcessingCenter>((IEnumerable) PXSelectorAttribute.SelectAll<SOQuickPayment.processingCenterID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row)).Where<CCProcessingCenter>((Func<CCProcessingCenter, bool>) (i => i.UseAcceptPaymentForm.GetValueOrDefault())).Any<CCProcessingCenter>();
    PXUIFieldAttribute.SetVisible<SOQuickPayment.newCard>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, flag10 && !flag2);
    PXUIFieldAttribute.SetEnabled<SOQuickPayment.newCard>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, flag10 && !flag2);
    PXUIFieldAttribute.SetVisible<SOQuickPayment.newAccount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, flag10 & flag2);
    PXUIFieldAttribute.SetEnabled<SOQuickPayment.newAccount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, flag10 & flag2);
    bool flag11 = flag10 && this.GetSavePaymentProfileCode(eventArgs.Row) == "A";
    PXUIFieldAttribute.SetVisible<SOQuickPayment.saveCard>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, flag11 && !flag2);
    PXUIFieldAttribute.SetEnabled<SOQuickPayment.saveCard>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, flag11 && !flag2);
    PXUIFieldAttribute.SetVisible<SOQuickPayment.saveAccount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, flag11 & flag2);
    PXUIFieldAttribute.SetEnabled<SOQuickPayment.saveAccount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, flag11 & flag2);
    nullable = eventArgs.Row.NewAccount;
    if (nullable.GetValueOrDefault() & flag2)
      UIState.RaiseOrHideError<SOQuickPayment.newAccount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, true, "By continuing this operation, you confirm that the customer's bank account details were obtained legally and with the permission of the account holder. This payment is authorized by the account holder. If this is not the case, this operation must be terminated.", (PXErrorLevel) 2, (object) eventArgs.Row.NewAccount);
    if (!flag3)
      return;
    int num4;
    if (processingCenter == null)
    {
      num4 = 1;
    }
    else
    {
      nullable = processingCenter.AcceptPOSPayments;
      num4 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num4 == 0)
      return;
    UIState.RaiseOrHideError<SOQuickPayment.processingCenterID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPayment>>) eventArgs).Cache, (object) eventArgs.Row, true, "Payments from POS Terminals are disabled for the {0} processing center. To enable them, on the Processing Centers (CA205000) form, select the Accept Payments from POS Terminals check box.", (PXErrorLevel) 2, (object) eventArgs.Row.ProcessingCenterID);
  }

  protected virtual bool IsActualFinPeriodClosed()
  {
    PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(PXContext.GetBranchID());
    if (branch != null)
    {
      FinPeriod finPeriodByDate = this.FinPeriodRepository.FindFinPeriodByDate(((PXGraph) (object) this.Base).Accessinfo.BusinessDate, ((PXAccess.Organization) branch.Organization).OrganizationID);
      if (finPeriodByDate == null || finPeriodByDate.Status == "Closed")
        return true;
    }
    return false;
  }

  protected virtual bool PaymentHasError(SOQuickPayment payment)
  {
    foreach (string field in (List<string>) ((PXSelectBase) this.QuickPayment).Cache.Fields)
    {
      PXUIFieldAttribute pxuiFieldAttribute = ((PXSelectBase) this.QuickPayment).Cache.GetAttributesReadonly((object) payment, field).OfType<PXUIFieldAttribute>().FirstOrDefault<PXUIFieldAttribute>();
      if (pxuiFieldAttribute != null && EnumerableExtensions.IsIn<PXErrorLevel>(pxuiFieldAttribute.ErrorLevel, (PXErrorLevel) 4, (PXErrorLevel) 5) || pxuiFieldAttribute != null && pxuiFieldAttribute.Required && EnumerableExtensions.IsIn<object>(((PXSelectBase) this.QuickPayment).Cache.GetValue((object) payment, field), (object) null, (object) 0M))
        return true;
    }
    return false;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.newCard> eventArgs)
  {
    this.NewCardAccountFieldUpdated(eventArgs.Row, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.newCard>>) eventArgs).Cache);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.newAccount> eventArgs)
  {
    this.NewCardAccountFieldUpdated(eventArgs.Row, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.newAccount>>) eventArgs).Cache);
  }

  private void NewCardAccountFieldUpdated(SOQuickPayment payment, PXCache cache)
  {
    if (payment == null)
      return;
    if (payment.NewCard.GetValueOrDefault())
      cache.SetValueExt<SOQuickPayment.pMInstanceID>((object) payment, (object) PaymentTranExtConstants.NewPaymentProfile);
    else
      cache.SetDefaultExt<SOQuickPayment.saveCard>((object) payment);
    cache.SetDefaultExt<SOQuickPayment.processingCenterID>((object) payment);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.cashAccountID> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.cashAccountID>>) eventArgs).Cache.SetDefaultExt<SOQuickPayment.extRefNbr>((object) eventArgs.Row);
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    string curyId1 = eventArgs.Row.CuryID;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.cashAccountID>>) eventArgs).Cache.SetDefaultExt<SOQuickPayment.curyID>((object) eventArgs.Row);
    PX.Objects.CA.CashAccount cashAccount = PX.Objects.CA.CashAccount.PK.Find((PXGraph) (object) this.Base, (int?) ((PXSelectBase<SOQuickPayment>) this.QuickPayment).Current?.CashAccountID);
    PXCache<PX.Objects.CM.CurrencyInfo> pxCache = GraphHelper.Caches<PX.Objects.CM.CurrencyInfo>((PXGraph) (object) this.Base);
    PX.Objects.CM.CurrencyInfo currencyInfo = pxCache.Locate(new PX.Objects.CM.CurrencyInfo()
    {
      CuryInfoID = eventArgs.Row.CuryInfoID
    });
    if (!string.IsNullOrEmpty(cashAccount?.CuryRateTypeID))
      ((PXCache) pxCache).SetValueExt<PX.Objects.CM.CurrencyInfo.curyRateTypeID>((object) currencyInfo, (object) cashAccount?.CuryRateTypeID);
    else
      ((PXCache) pxCache).SetDefaultExt<PX.Objects.CM.CurrencyInfo.curyRateTypeID>((object) currencyInfo);
    string curyId2 = eventArgs.Row.CuryID;
    if (curyId1 != curyId2)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.cashAccountID>>) eventArgs).Cache.SetDefaultExt<SOQuickPayment.curyOrigDocAmt>((object) eventArgs.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.cashAccountID>>) eventArgs).Cache.SetDefaultExt<SOQuickPayment.curyRefundAmt>((object) eventArgs.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.pMInstanceID> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.pMInstanceID>>) eventArgs).Cache.SetDefaultExt<SOQuickPayment.processingCenterID>((object) eventArgs.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.pMInstanceID>>) eventArgs).Cache.SetDefaultExt<SOQuickPayment.saveCard>((object) eventArgs.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.processingCenterID> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    if (eventArgs.Row.ProcessingCenterID != null)
    {
      CCProcessingCenter processingCenter = CCProcessingCenter.PK.Find((PXGraph) (object) this.Base, eventArgs.Row.ProcessingCenterID);
      if ((processingCenter != null ? (!processingCenter.AllowSaveProfile.GetValueOrDefault() ? 1 : 0) : 1) != 0)
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.processingCenterID>>) eventArgs).Cache.SetValueExt<SOQuickPayment.saveCard>((object) eventArgs.Row, (object) false);
    }
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOQuickPayment, SOQuickPayment.processingCenterID>>) eventArgs).Cache.SetDefaultExt<SOQuickPayment.terminalID>((object) eventArgs.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<SOQuickPayment, SOQuickPayment.curyOrigDocAmt> eventArgs)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<SOQuickPayment, SOQuickPayment.curyOrigDocAmt>, SOQuickPayment, object>) eventArgs).NewValue = (object) this.GetDefaultPaymentAmount(eventArgs.Row) ?? ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<SOQuickPayment, SOQuickPayment.curyOrigDocAmt>, SOQuickPayment, object>) eventArgs).NewValue;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<SOQuickPayment, SOQuickPayment.curyRefundAmt> eventArgs)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<SOQuickPayment, SOQuickPayment.curyRefundAmt>, SOQuickPayment, object>) eventArgs).NewValue = (object) this.GetDefaultPaymentAmount(eventArgs.Row) ?? ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<SOQuickPayment, SOQuickPayment.curyRefundAmt>, SOQuickPayment, object>) eventArgs).NewValue;
  }

  protected virtual Decimal? GetDefaultPaymentAmount(SOQuickPayment qp)
  {
    Decimal? defaultPaymentAmount = new Decimal?();
    if (qp == null || (object) this.GetCurrent<TDocument>() == null)
      return defaultPaymentAmount;
    TDocument current = this.GetCurrent<TDocument>();
    if (qp.CuryID == current.CuryID)
      defaultPaymentAmount = current.CuryUnpaidBalance;
    else if (qp.CuryID != null)
    {
      Decimal curyval;
      PX.Objects.CM.PXCurrencyAttribute.CuryConvCury(((PXSelectBase) this.QuickPayment).Cache, (object) qp, current.UnpaidBalance.GetValueOrDefault(), out curyval);
      defaultPaymentAmount = new Decimal?(curyval);
    }
    return defaultPaymentAmount;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<SOQuickPayment, SOQuickPayment.curyOrigDocAmt> eventArgs)
  {
    if (eventArgs.Row.IsRefund.GetValueOrDefault())
      return;
    this.ValidateAmount<SOQuickPayment.curyOrigDocAmt>(eventArgs.Row, (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOQuickPayment, SOQuickPayment.curyOrigDocAmt>, SOQuickPayment, object>) eventArgs).NewValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<SOQuickPayment, SOQuickPayment.curyRefundAmt> eventArgs)
  {
    if (!eventArgs.Row.IsRefund.GetValueOrDefault())
      return;
    this.ValidateAmount<SOQuickPayment.curyRefundAmt>(eventArgs.Row, (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOQuickPayment, SOQuickPayment.curyRefundAmt>, SOQuickPayment, object>) eventArgs).NewValue);
  }

  protected virtual void ValidateAmount<AmountField>(SOQuickPayment qp, Decimal? value) where AmountField : IBqlField
  {
    if (!value.HasValue || qp == null || qp.CuryID == null)
      return;
    PXException pxException = (PXException) null;
    object obj;
    ((PXSelectBase) this.QuickPayment).Cache.RaiseFieldDefaulting<AmountField>((object) qp, ref obj);
    Decimal? nullable1 = value;
    Decimal? nullable2 = (Decimal?) obj;
    Decimal? nullable3;
    if (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
    {
      pxException = (PXException) new PXSetPropertyException<AmountField>(this.GetExceedingAmountErrorMessage(qp), new object[1]
      {
        obj
      });
    }
    else
    {
      nullable3 = value;
      Decimal num = 0M;
      if (nullable3.GetValueOrDefault() <= num & nullable3.HasValue)
        pxException = (PXException) new PXSetPropertyException<AmountField>("The payment amount should be more than zero.");
    }
    if (qp.RefTranExtNbr != null && pxException == null)
    {
      PX.Objects.AR.ExternalTransaction externalTransaction = (PX.Objects.AR.ExternalTransaction) PXSelectorAttribute.Select<SOQuickPayment.refTranExtNbr>(((PXSelectBase) this.QuickPayment).Cache, (object) qp);
      if (externalTransaction != null)
      {
        PX.Objects.AR.ARPayment arPayment = PX.Objects.AR.ARPayment.PK.Find((PXGraph) (object) this.Base, externalTransaction.DocType, externalTransaction.RefNbr);
        Decimal? nullable4;
        if (arPayment == null || arPayment.CuryID == qp.CuryID)
        {
          nullable4 = externalTransaction.Amount;
        }
        else
        {
          PXCache cache = ((PXSelectBase) this.QuickPayment).Cache;
          SOQuickPayment row = qp;
          nullable3 = externalTransaction.Amount;
          Decimal valueOrDefault = nullable3.GetValueOrDefault();
          Decimal num;
          ref Decimal local = ref num;
          PX.Objects.CM.PXCurrencyAttribute.CuryConvCury(cache, (object) row, valueOrDefault, out local);
          nullable4 = new Decimal?(num);
        }
        if (nullable4.HasValue)
        {
          nullable3 = value;
          nullable1 = nullable4;
          if (nullable3.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable3.HasValue & nullable1.HasValue)
            pxException = (PXException) new PXSetPropertyException<AmountField>("The amount of the customer refund exceeds the amount of the original transaction.");
        }
      }
    }
    ((PXSelectBase) this.QuickPayment).Cache.RaiseExceptionHandling<AmountField>((object) qp, (object) value, (Exception) pxException);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<SOQuickPayment, SOQuickPayment.extRefNbr> eventArgs)
  {
    string newValue = (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOQuickPayment, SOQuickPayment.extRefNbr>, SOQuickPayment, object>) eventArgs).NewValue;
    if (eventArgs.Row == null || string.IsNullOrEmpty(newValue))
      return;
    PX.Objects.AR.ARPayment paymentByPaymentRef = this.FindARPaymentByPaymentRef(newValue, eventArgs.Row.PaymentMethodID);
    if (paymentByPaymentRef == null)
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<SOQuickPayment, SOQuickPayment.extRefNbr>>) eventArgs).Cache.RaiseExceptionHandling<SOQuickPayment.extRefNbr>((object) eventArgs.Row, (object) newValue, (Exception) new PXSetPropertyException("Payment with Payment Ref. '{0}' dated '{1}' already exists for this Customer and have the same Payment Method. It's Reference Number - {2} {3}.", (PXErrorLevel) 2, new object[4]
    {
      (object) paymentByPaymentRef.ExtRefNbr,
      (object) paymentByPaymentRef.DocDate,
      (object) paymentByPaymentRef.DocType,
      (object) paymentByPaymentRef.RefNbr
    }));
  }

  protected virtual PX.Objects.AR.ARPayment FindARPaymentByPaymentRef(
    string paymentRef,
    string paymentMethodID)
  {
    if (string.IsNullOrEmpty(paymentMethodID))
      return (PX.Objects.AR.ARPayment) null;
    return PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) (object) this.Base, paymentMethodID).IsAccountNumberRequired.GetValueOrDefault() ? PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(PXSelectBase<PX.Objects.AR.ARPayment, PXSelectReadonly<PX.Objects.AR.ARPayment, Where<PX.Objects.AR.ARPayment.customerID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>, And<PX.Objects.AR.ARPayment.pMInstanceID, Equal<Current<SOQuickPayment.pMInstanceID>>, And<PX.Objects.AR.ARPayment.extRefNbr, Equal<Required<SOQuickPayment.extRefNbr>>, And<PX.Objects.AR.ARPayment.voided, Equal<False>>>>>>.Config>.Select((PXGraph) (object) this.Base, new object[2]
    {
      (object) this.GetCurrent<TDocument>().CustomerID,
      (object) paymentRef
    })) : PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(PXSelectBase<PX.Objects.AR.ARPayment, PXSelectReadonly<PX.Objects.AR.ARPayment, Where<PX.Objects.AR.ARPayment.customerID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>, And<PX.Objects.AR.ARPayment.paymentMethodID, Equal<Current<SOQuickPayment.paymentMethodID>>, And<PX.Objects.AR.ARPayment.extRefNbr, Equal<Required<SOQuickPayment.extRefNbr>>, And<PX.Objects.AR.ARPayment.voided, Equal<False>>>>>>.Config>.Select((PXGraph) (object) this.Base, new object[2]
    {
      (object) this.GetCurrent<TDocument>().CustomerID,
      (object) paymentRef
    }));
  }

  protected virtual void FieldDefaulting(
    PX.Data.Events.FieldDefaulting<SOQuickPayment, SOQuickPayment.newCard> e)
  {
    if (e.Row == null)
      return;
    if (e.Row?.PaymentMethodID != null && e.Row.ProcessingCenterID != null)
    {
      bool? nullable = e.Row.IsRefund;
      if (!nullable.GetValueOrDefault())
      {
        PX.Objects.CA.PaymentMethod paymentMethod = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) (object) this.Base, e.Row.PaymentMethodID);
        CCProcessingCenter processingCenter = CCProcessingCenter.PK.Find((PXGraph) (object) this.Base, e.Row.ProcessingCenterID);
        if (processingCenter != null)
        {
          int num;
          if (paymentMethod.PaymentType == "CCD" || paymentMethod.PaymentType == "EFT")
          {
            nullable = processingCenter.UseAcceptPaymentForm;
            num = nullable.GetValueOrDefault() ? 1 : 0;
          }
          else
            num = 0;
          if (num != 0 && !e.Row.PMInstanceID.HasValue)
          {
            ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<SOQuickPayment, SOQuickPayment.newCard>, SOQuickPayment, object>) e).NewValue = (object) (PXSelectorAttribute.SelectAll<SOQuickPayment.pMInstanceID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<SOQuickPayment, SOQuickPayment.newCard>>) e).Cache, (object) e.Row).Count == 0);
            return;
          }
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<SOQuickPayment, SOQuickPayment.newCard>, SOQuickPayment, object>) e).NewValue = (object) false;
          return;
        }
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<SOQuickPayment, SOQuickPayment.newCard>, SOQuickPayment, object>) e).NewValue = (object) false;
        return;
      }
    }
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<SOQuickPayment, SOQuickPayment.newCard>, SOQuickPayment, object>) e).NewValue = (object) false;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<SOQuickPayment, SOQuickPayment.refTranExtNbr> eventArgs)
  {
    if (!eventArgs.Row.IsRefund.GetValueOrDefault())
      return;
    List<PX.Objects.AR.ExternalTransaction> list = GraphHelper.RowCast<PX.Objects.AR.ExternalTransaction>((IEnumerable) PXSelectorAttribute.SelectAll<SOQuickPayment.refTranExtNbr>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<SOQuickPayment, SOQuickPayment.refTranExtNbr>>) eventArgs).Cache, (object) eventArgs.Row)).ToList<PX.Objects.AR.ExternalTransaction>();
    if (list.Count != 1)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<SOQuickPayment, SOQuickPayment.refTranExtNbr>, SOQuickPayment, object>) eventArgs).NewValue = (object) list[0].TranNumber;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<SOQuickPayment, SOQuickPayment.refTranExtNbr> eventArgs)
  {
    PX.Objects.AR.ExternalTransaction externalTransaction = (PX.Objects.AR.ExternalTransaction) PXSelectorAttribute.Select<SOQuickPayment.refTranExtNbr>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<SOQuickPayment, SOQuickPayment.refTranExtNbr>>) eventArgs).Cache, (object) eventArgs.Row, (object) (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOQuickPayment, SOQuickPayment.refTranExtNbr>, SOQuickPayment, object>) eventArgs).NewValue);
    if (!string.IsNullOrEmpty(externalTransaction?.TranNumber) && !this.HasReturnLineForOrigTran(externalTransaction.ProcessingCenterID, externalTransaction.TranNumber) && this.ValidateCCRefundOrigTransaction())
      throw new PXSetPropertyException("The original {0} transaction is not related to any of the documents with the items to be returned.", new object[1]
      {
        (object) externalTransaction.TranNumber
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<SOIncreaseAuthorizedAmountDialog, SOIncreaseAuthorizedAmountDialog.curyAuthorizedAmtNew> eventArgs)
  {
    SOIncreaseAuthorizedAmountDialog row = eventArgs.Row;
    Exception exception = (Exception) null;
    Decimal? authorizedAmtMax = row.CuryAuthorizedAmtMax;
    Decimal? nullable1 = authorizedAmtMax;
    Decimal? nullable2 = row.CuryAuthorizedAmt;
    Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    Decimal newValue = (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOIncreaseAuthorizedAmountDialog, SOIncreaseAuthorizedAmountDialog.curyAuthorizedAmtNew>, SOIncreaseAuthorizedAmountDialog, object>) eventArgs).NewValue;
    Decimal num1 = newValue;
    nullable2 = authorizedAmtMax;
    Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
    if (num1 > valueOrDefault1 & nullable2.HasValue)
      exception = (Exception) new PXSetPropertyException<SOIncreaseAuthorizedAmountDialog.curyAuthorizedAmtNew>("The unpaid balance for the current document is {0}. You cannot increase the authorized amount for an amount greater than the unpaid balance amount.", new object[1]
      {
        (object) nullable3
      });
    Decimal? curyAuthorizedAmt = row.CuryAuthorizedAmt;
    Decimal num2 = newValue;
    nullable2 = curyAuthorizedAmt;
    Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
    if (num2 <= valueOrDefault2 & nullable2.HasValue)
      exception = (Exception) new PXSetPropertyException<SOIncreaseAuthorizedAmountDialog.curyAuthorizedAmtNew>("The increased authorized amount must be greater than the current authorized amount.");
    if (exception == null)
      return;
    ((PXSelectBase) this.IncreaseAmount).Cache.RaiseExceptionHandling<SOIncreaseAuthorizedAmountDialog.curyAuthorizedAmtNew>((object) row, (object) newValue, exception);
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<SOIncreaseAuthorizedAmountDialog> eventArgs)
  {
    SOIncreaseAuthorizedAmountDialog row = eventArgs.Row;
    PXUIFieldAttribute.SetVisible<SOIncreaseAuthorizedAmountDialog.curyID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOIncreaseAuthorizedAmountDialog>>) eventArgs).Cache, (object) row, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    bool flag1 = this.CheckIncreaseAmount();
    bool flag2 = string.IsNullOrEmpty(row.WarningMessage);
    ((PXAction) this.increaseAuthorizedOK).SetEnabled(flag1);
    PXUIFieldAttribute.SetVisible<SOIncreaseAuthorizedAmountDialog.warningMessage>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOIncreaseAuthorizedAmountDialog>>) eventArgs).Cache, (object) row, this.CanIncreaseAndCapture && !flag2);
    ((PXAction) this.increaseAndCaptureAuthorizedOK).SetEnabled(flag1 & flag2 && this.CanIncreaseAndCapture);
  }

  protected virtual void PaymentMethodFieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs eventArgs)
  {
    if (!this.IsCashSale() || eventArgs.NewValue == null)
      return;
    PX.Objects.CA.PaymentMethod paymentMethod = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) (object) this.Base, (string) eventArgs.NewValue);
    if (paymentMethod == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.CA.PaymentMethod>((PXGraph) (object) this.Base), new object[1]
      {
        eventArgs.NewValue
      });
    if (paymentMethod.PaymentType == "CCD")
      throw new PXSetPropertyException(this.GetCCPaymentIsNotSupportedMessage());
  }

  protected virtual void _(PX.Data.Events.RowSelected<TDocument> eventArgs)
  {
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.acumaticaPayments>();
    ((PXAction) this.voidDocumentPayment).SetCaption(flag ? "Void Card/EFT Payment" : "Void Card Payment");
    ((PXAction) this.importDocumentPayment).SetCaption(flag ? "Import Card/EFT Payment" : "Import Card Payment");
    ((PXAction) this.increaseAndCaptureAuthorizedOK).SetVisible(this.CanIncreaseAndCapture);
  }

  protected virtual void _(PX.Data.Events.RowSelected<TDocumentAdjust> eventArgs)
  {
    if ((object) eventArgs.Row == null)
      return;
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>() && eventArgs.Row.IsCCPayment.GetValueOrDefault() && !eventArgs.Row.IsCCAuthorized.GetValueOrDefault() && !eventArgs.Row.IsCCCaptured.GetValueOrDefault() && !eventArgs.Row.Voided.GetValueOrDefault() && !eventArgs.Row.Released.GetValueOrDefault() && ARPaymentType.DrCr(eventArgs.Row.AdjgDocType) == "D")
    {
      int? ccReauthTriesLeft = (PX.Objects.AR.ARPayment.PK.Find((PXGraph) (object) this.Base, eventArgs.Row.AdjgDocType, eventArgs.Row.AdjgRefNbr) ?? throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.AR.ARPayment>((PXGraph) (object) this.Base), new object[2]
      {
        (object) eventArgs.Row.AdjgDocType,
        (object) eventArgs.Row.AdjgRefNbr
      })).CCReauthTriesLeft;
      int num = 0;
      propertyException = new PXSetPropertyException(ccReauthTriesLeft.GetValueOrDefault() > num & ccReauthTriesLeft.HasValue ? "The max. number of attempts to reauthorize the {0} payment has not been reached. To process the {0} payment, open the Payments and Applications (AR302000) form." : "The {0} payment has no active pre-authorized or captured transactions. To process the {0} payment, open the Payments and Applications (AR302000) form.", (PXErrorLevel) 3, new object[1]
      {
        (object) eventArgs.Row.AdjgRefNbr
      });
    }
    if (string.IsNullOrEmpty(eventArgs.Row.AdjgRefNbr))
      return;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<TDocumentAdjust>>) eventArgs).Cache.RaiseExceptionHandling(this.GetPaymentErrorFieldName(), (object) eventArgs.Row, (object) null, (Exception) propertyException);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.CM.CurrencyInfo> eventArgs)
  {
    if (eventArgs.Row == null || !NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.QuickPayment).Cache.Cached))
      return;
    long? curyInfoId1 = eventArgs.Row.CuryInfoID;
    long? curyInfoId2 = ((PXSelectBase<SOQuickPayment>) this.QuickPayment).Current.CuryInfoID;
    if (!(curyInfoId1.GetValueOrDefault() == curyInfoId2.GetValueOrDefault() & curyInfoId1.HasValue == curyInfoId2.HasValue))
      return;
    eventArgs.Cancel = true;
  }

  protected virtual (WebDialogResult webDialogResult, bool capture) AskIncreaseAuthorizedAmountDialog(
    string header)
  {
    string str1 = PXMessages.LocalizeNoPrefix(header);
    PXView view = ((PXSelectBase) this.IncreaseAmount).View;
    string str2 = str1;
    CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust> createPaymentExtBase = this;
    // ISSUE: virtual method pointer
    PXView.InitializePanel initializePanel = new PXView.InitializePanel((object) createPaymentExtBase, __vmethodptr(createPaymentExtBase, InitializeIncreaseAuthorizedAmountPanel));
    WebDialogResult webDialogResult = view.AskExtWithHeader(str2, initializePanel, (List<string>) null, false);
    if (!((PXAction) this.increaseAuthorizedOK).GetEnabled())
      return ((WebDialogResult) 2, false);
    if (webDialogResult == 1)
      return ((WebDialogResult) 1, false);
    return webDialogResult == 6 ? ((WebDialogResult) 1, true) : ((WebDialogResult) 2, false);
  }

  protected virtual WebDialogResult AskCreatePaymentDialog(string header)
  {
    try
    {
      string str1 = PXMessages.LocalizeNoPrefix(header);
      PXView view = ((PXSelectBase) this.QuickPayment).View;
      string str2 = str1;
      CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust> createPaymentExtBase = this;
      // ISSUE: virtual method pointer
      PXView.InitializePanel initializePanel = new PXView.InitializePanel((object) createPaymentExtBase, __vmethodptr(createPaymentExtBase, InitializeQuickPaymentPanel));
      WebDialogResult paymentDialog = view.AskExtWithHeader(str2, initializePanel, (List<string>) null, false);
      switch (paymentDialog - 3)
      {
        case 0:
          this.AssignRefund();
          return (WebDialogResult) 1;
        case 3:
          this.AssignCapture();
          return (WebDialogResult) 1;
        case 4:
          this.AssignAuthorize();
          return (WebDialogResult) 1;
        default:
          return paymentDialog;
      }
    }
    catch (PXBaseRedirectException ex)
    {
      ex.RepaintControls = true;
      throw ex;
    }
  }

  protected virtual bool IsAnyPayment(SOQuickPayment quickPayment)
  {
    return quickPayment.Capture.GetValueOrDefault() || quickPayment.Authorize.GetValueOrDefault() || quickPayment.Refund.GetValueOrDefault() || quickPayment.AuthorizeRemainder.GetValueOrDefault();
  }

  protected virtual void PrepareForCreateCCPayment(TDocument doc)
  {
    this.VerifyHasActiveCustomerPaymentMethod(doc);
  }

  protected virtual void CreateCCPayment(TDocument doc, Action onBeforeCreatePayment)
  {
    ((PXGraph) (object) this.Base).Clear();
    this.SetCurrentDocument(doc);
    this.PrepareForCreateCCPayment(doc);
    this.InitializeQuickPaymentPanel((PXGraph) (object) this.Base, ((PXSelectBase) this.QuickPayment).View.Name);
    if (onBeforeCreatePayment != null)
      onBeforeCreatePayment();
    this.CreatePayment(((PXSelectBase<SOQuickPayment>) this.QuickPayment).Current, "PMT", true);
  }

  protected virtual void IncreaseCCAuthorizedAmount(ARPaymentEntry aRPaymentEntry, bool capture)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.\u003C\u003Ec__DisplayClass96_0 cDisplayClass960 = new CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.\u003C\u003Ec__DisplayClass96_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass960.aRPaymentEntry = aRPaymentEntry;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass960.capture = capture;
    ((PXAction) this.Base.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass960.document = this.GetCurrent<TDocument>();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) (object) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass960, __methodptr(\u003CIncreaseCCAuthorizedAmount\u003Eb__0)));
  }

  protected virtual void CreatePayment(
    SOQuickPayment quickPayment,
    string paymentType,
    bool throwErrors)
  {
    ((PXAction) this.Base.Save).Press();
    if (this.IsAnyPayment(quickPayment))
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.\u003C\u003Ec__DisplayClass97_0 cDisplayClass970 = new CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.\u003C\u003Ec__DisplayClass97_0();
      if (quickPayment.NewCard.GetValueOrDefault())
        CCProcessingHelper.CheckHttpsConnection();
      CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.CreatePaymentParameter paymentParameter = new CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.CreatePaymentParameter();
      paymentParameter.Document = this.GetCurrent<TDocument>();
      paymentParameter.CcPaymentConnectorUrl = CCPaymentProcessingHelper.GetCurrentSiteUrl(HttpContext.Current);
      paymentParameter.QuickPayment = quickPayment;
      paymentParameter.PaymentType = paymentType;
      paymentParameter.ThrowErrors = throwErrors;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass970.createParameter = paymentParameter;
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) (object) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass970, __methodptr(\u003CCreatePayment\u003Eb__0)));
    }
    else
    {
      ((PXAction) this.CreatePayment(quickPayment, this.GetCurrent<TDocument>(), paymentType).Save).Press();
      ((PXAction) this.Base.Cancel).Press();
    }
  }

  protected virtual void ProcessIncreaseAuthorizedAmount(ARPaymentEntry paymentEntry, bool capture)
  {
    using (new ForcePaymentAppScope())
    {
      try
      {
        if (capture)
        {
          string refNbr = ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current.RefNbr;
          string docType = ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current.DocType;
          this.PressButtonIfEnabled((PXGraph) paymentEntry, "increazeAuthorizedCCPayment");
          ((PXGraph) paymentEntry).Clear();
          ((PXGraph) paymentEntry).Clear((PXClearOption) 4);
          ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Search<PX.Objects.AR.ARPayment.refNbr>((object) refNbr, new object[1]
          {
            (object) docType
          }));
          this.PressButtonIfEnabled((PXGraph) paymentEntry, "captureCCPayment");
        }
        else
          this.PressButtonIfEnabled((PXGraph) paymentEntry, "increazeAuthorizedCCPayment");
      }
      catch (PXBaseRedirectException ex)
      {
        PXLongOperation.SetCustomInfo((object) new CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.EndPaymentOperationCustomInfo(((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current.DocType, ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current.RefNbr, ex));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }

  protected virtual void ProcessCreatePayment(
    CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.CreatePaymentParameter parameter)
  {
    CCPaymentProcessingHelper.SetCurrentSiteUrl(parameter.CcPaymentConnectorUrl);
    using (new ForcePaymentAppScope())
    {
      ARPaymentEntry payment = this.CreatePayment(parameter.QuickPayment, parameter.Document, parameter.PaymentType);
      ((PXAction) payment.Save).Press();
      try
      {
        bool? nullable = parameter.QuickPayment.Capture;
        if (nullable.GetValueOrDefault())
        {
          this.PressButtonIfEnabled((PXGraph) payment, "captureCCPayment");
        }
        else
        {
          nullable = parameter.QuickPayment.Authorize;
          if (!nullable.GetValueOrDefault())
          {
            nullable = parameter.QuickPayment.AuthorizeRemainder;
            if (!nullable.GetValueOrDefault())
            {
              nullable = parameter.QuickPayment.Refund;
              if (!nullable.GetValueOrDefault())
                return;
              nullable = ((PXSelectBase<PX.Objects.AR.ARPayment>) payment.Document).Current.Hold;
              bool flag = false;
              if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
                return;
              this.PressButtonIfEnabled((PXGraph) payment, "creditCCPayment");
              return;
            }
          }
          this.PressButtonIfEnabled((PXGraph) payment, "authorizeCCPayment");
        }
      }
      catch (PXBaseRedirectException ex)
      {
        PXLongOperation.SetCustomInfo((object) new CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.EndPaymentOperationCustomInfo(((PXSelectBase<PX.Objects.AR.ARPayment>) payment.Document).Current.DocType, ((PXSelectBase<PX.Objects.AR.ARPayment>) payment.Document).Current.RefNbr, ex));
      }
      catch (Exception ex)
      {
        if (parameter.ThrowErrors)
          throw;
        this.RedirectToNewGraph(payment, ex);
      }
      finally
      {
        CCPaymentProcessingHelper.SetCurrentSiteUrl((string) null);
      }
    }
  }

  public virtual ARPaymentEntry CreatePayment(
    SOQuickPayment quickPayment,
    TDocument document,
    string paymentType)
  {
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    instance.AutoPaymentApp = true;
    if (this.IsAnyPayment(quickPayment))
      ((PXSelectBase<ARSetup>) instance.arsetup).Current.HoldEntry = new bool?(false);
    PXSelectJoin<PX.Objects.AR.ARPayment, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.AR.ARPayment.customerID>>>, Where<PX.Objects.AR.ARPayment.docType, Equal<Optional<PX.Objects.AR.ARPayment.docType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> document1 = instance.Document;
    PX.Objects.AR.ARPayment arPayment1 = new PX.Objects.AR.ARPayment();
    arPayment1.DocType = paymentType;
    PX.Objects.AR.ARPayment arpayment = ((PXSelectBase<PX.Objects.AR.ARPayment>) document1).Insert(arPayment1);
    this.FillARPayment(arpayment, quickPayment, document);
    PX.Objects.AR.ARPayment arPayment2 = ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Update(arpayment);
    if (quickPayment.AuthorizeRemainder.GetValueOrDefault())
      ((PXSelectBase) instance.Document).Cache.SetValue<PX.Objects.AR.ARPayment.pMInstanceID>((object) arPayment2, (object) quickPayment.PMInstanceID);
    else if (quickPayment.NewCard.GetValueOrDefault())
      ((PXSelectBase) instance.Document).Cache.SetValue<PX.Objects.AR.ARPayment.pMInstanceID>((object) arPayment2, (object) PaymentTranExtConstants.NewPaymentProfile);
    if (string.IsNullOrEmpty(quickPayment.RefTranExtNbr))
      ((PXSelectBase) instance.Document).Cache.SetValue<PX.Objects.AR.ARPayment.cCTransactionRefund>((object) arPayment2, (object) false);
    this.AddAdjust(instance, document);
    return instance;
  }

  protected virtual void FillARPayment(
    PX.Objects.AR.ARPayment arpayment,
    SOQuickPayment quickPayment,
    TDocument document)
  {
    arpayment.CustomerID = document.CustomerID;
    arpayment.CustomerLocationID = document.CustomerLocationID;
    arpayment.PaymentMethodID = quickPayment.PaymentMethodID;
    arpayment.RefTranExtNbr = quickPayment.RefTranExtNbr;
    arpayment.PMInstanceID = quickPayment.PMInstanceID;
    arpayment.CuryOrigDocAmt = quickPayment.IsRefund.GetValueOrDefault() ? quickPayment.CuryRefundAmt : quickPayment.CuryOrigDocAmt;
    arpayment.DocDesc = quickPayment.DocDesc;
    arpayment.CashAccountID = quickPayment.CashAccountID;
    arpayment.ProcessingCenterID = quickPayment.ProcessingCenterID;
    arpayment.ExtRefNbr = quickPayment.ExtRefNbr;
    arpayment.UpdateNextNumber = quickPayment.UpdateNextNumber;
    arpayment.TerminalID = quickPayment.TerminalID;
    if (quickPayment.AuthorizeRemainder.GetValueOrDefault())
      PXCacheEx.GetExtension<PX.Objects.Extensions.PaymentTransaction.Payment>((IBqlTable) arpayment).PreviousExternalTransactionID = quickPayment.PreviousExternalTransactionID;
    switch (this.GetSavePaymentProfileCode(quickPayment))
    {
      case "A":
        arpayment.SaveCard = quickPayment.SaveCard;
        break;
      case "F":
        arpayment.SaveCard = new bool?(true);
        break;
    }
  }

  protected virtual string GetSavePaymentProfileCode(SOQuickPayment quickPayment)
  {
    return this.GetSavePaymentProfileCode(quickPayment.NewCard, quickPayment.ProcessingCenterID);
  }

  public virtual string GetSavePaymentProfileCode(bool? newCard, string ProcessingCenterID)
  {
    if (newCard.GetValueOrDefault())
    {
      CCProcessingCenter processingCenter = CCProcessingCenter.PK.Find((PXGraph) (object) this.Base, ProcessingCenterID);
      if ((processingCenter != null ? (processingCenter.AllowSaveProfile.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        return this.GetCustomerClass()?.SavePaymentProfiles;
    }
    return (string) null;
  }

  protected virtual bool AllowsAuthorization(SOQuickPayment quickPayment)
  {
    return !quickPayment.IsRefund.GetValueOrDefault();
  }

  protected virtual string GetExceedingAmountErrorMessage(SOQuickPayment quickPayment)
  {
    return "The payment amount should be less than or equal to the order unpaid amount ({0}).";
  }

  private void VerifyHasActiveCustomerPaymentMethod(TDocument doc)
  {
    if (!doc.PMInstanceID.HasValue)
      throw new FieldIsEmptyException((PXCache) GraphHelper.Caches<TDocument>((PXGraph) (object) this.Base), (object) doc, this.GetDocumentPMInstanceIDField(), true);
    PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = PX.Objects.AR.CustomerPaymentMethod.PK.Find((PXGraph) (object) this.Base, doc.PMInstanceID);
    if (customerPaymentMethod == null)
      throw new RowNotFoundException(((PXGraph) (object) this.Base).Caches[typeof (PX.Objects.AR.CustomerPaymentMethod)], new object[1]
      {
        (object) doc.PMInstanceID
      });
    if (!customerPaymentMethod.IsActive.GetValueOrDefault())
      throw new PXException("The {0} card/account number is inactive on the Customer Payment Methods (AR303010) form and cannot be processed.", new object[1]
      {
        (object) customerPaymentMethod.Descr
      });
  }

  protected virtual void _(PX.Data.Events.RowSelected<SOImportExternalTran> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<SOImportExternalTran.processingCenterID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOImportExternalTran>>) eventArgs).Cache, (object) eventArgs.Row, !eventArgs.Row.PMInstanceID.HasValue);
    ((PXAction) this.importDocumentPaymentCreate).SetEnabled(!string.IsNullOrEmpty(eventArgs.Row.ProcessingCenterID));
  }

  protected virtual void ImportPayment(SOImportExternalTran panel)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.\u003C\u003Ec__DisplayClass108_0 displayClass1080 = new CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.\u003C\u003Ec__DisplayClass108_0();
    ((PXAction) this.Base.Save).Press();
    CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.ImportPaymentParameter paymentParameter = new CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.ImportPaymentParameter();
    paymentParameter.Document = this.GetCurrent<TDocument>();
    paymentParameter.Panel = panel;
    // ISSUE: reference to a compiler-generated field
    displayClass1080.importParameter = paymentParameter;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) (object) this.Base, new PXToggleAsyncDelegate((object) displayClass1080, __methodptr(\u003CImportPayment\u003Eb__0)));
  }

  protected virtual void ProcessImportPayment(
    CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.ImportPaymentParameter parameter)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      ((PXAction) this.ApplyPayment(new PaymentDocCreator().CreateDoc(new PaymentDocCreator.InputParams()
      {
        Customer = parameter.Document.CustomerID,
        CashAccountID = parameter.Document.CashAccountID,
        PaymentMethodID = parameter.Panel.PaymentMethodID,
        PMInstanceID = parameter.Panel.PMInstanceID,
        ProcessingCenterID = parameter.Panel.ProcessingCenterID,
        TransactionID = parameter.Panel.TranNumber
      }), this.GetCurrent<TDocument>()).Save).Press();
      transactionScope.Complete();
    }
  }

  protected virtual ARPaymentEntry ApplyPayment(IDocumentKey payment, TDocument document)
  {
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    instance.AutoPaymentApp = true;
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Search<PX.Objects.AR.ARPayment.refNbr>((object) payment.RefNbr, new object[1]
    {
      (object) payment.DocType
    }));
    this.AddAdjust(instance, document);
    return instance;
  }

  protected virtual void ExecutePaymentTransactionAction(
    Action<CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>, TDocumentAdjust, ARPaymentEntry, ARPaymentEntryPaymentTransaction> paymentAction)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.\u003C\u003Ec__DisplayClass111_0 displayClass1110 = new CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.\u003C\u003Ec__DisplayClass111_0();
    ((PXAction) this.Base.Save).Press();
    TDocumentAdjust current = this.GetAdjustView().Current;
    if ((object) current == null)
      return;
    CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.ExecutePaymentTransactionActionParameter transactionActionParameter = new CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.ExecutePaymentTransactionActionParameter();
    transactionActionParameter.Document = this.GetCurrent<TDocument>();
    transactionActionParameter.PaymentAction = paymentAction;
    transactionActionParameter.Adjustment = current;
    // ISSUE: reference to a compiler-generated field
    displayClass1110.executeParameter = transactionActionParameter;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) (object) this.Base, new PXToggleAsyncDelegate((object) displayClass1110, __methodptr(\u003CExecutePaymentTransactionAction\u003Eb__0)));
  }

  protected virtual void ProcessExecutePaymentTransactionAction(
    CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.ExecutePaymentTransactionActionParameter parameter)
  {
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    ((PXGraph) instance).Clear();
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Search<PX.Objects.AR.ARPayment.refNbr>((object) parameter.Adjustment.AdjgRefNbr, new object[1]
    {
      (object) parameter.Adjustment.AdjgDocType
    }));
    ARPaymentEntryPaymentTransaction extension = ((PXGraph) instance).GetExtension<ARPaymentEntryPaymentTransaction>();
    try
    {
      parameter.PaymentAction(this, parameter.Adjustment, instance, extension);
      PXLongOperation.SetCustomInfo((object) new CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.EndPaymentOperationCustomInfo());
    }
    catch (Exception ex)
    {
      PXTrace.WriteError("Processing of the {0} transaction has failed.", new object[1]
      {
        (object) parameter.Adjustment.AdjgRefNbr
      });
      PXTrace.WriteError(ex);
      this.RedirectToNewGraph(parameter.Adjustment, ex);
    }
  }

  public virtual void RedirectToNewGraph(TDocumentAdjust adjustment, Exception exception)
  {
    if (exception != null)
    {
      PXLongOperation.SetCustomInfo((object) new CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.EndPaymentOperationCustomInfo(adjustment, exception));
      throw new PXException("Processing of the {0} transaction has failed.", new object[1]
      {
        (object) adjustment.AdjgRefNbr
      });
    }
  }

  public virtual void RedirectToNewGraph(ARPaymentEntry paymentEntry, Exception exception)
  {
    PXTrace.WriteError("Processing of the {0} transaction has failed.", new object[1]
    {
      (object) ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current?.RefNbr
    });
    PXTrace.WriteError(exception);
    PXSelectBase<TPaymentAdjust> adjustView = this.GetAdjustView(paymentEntry);
    TPaymentAdjust paymentAdjust = adjustView.Current ?? adjustView.SelectSingle(Array.Empty<object>());
    if ((object) paymentAdjust == null)
      throw exception;
    TDocumentAdjust adjustment;
    if (typeof (TPaymentAdjust) == typeof (TDocumentAdjust))
    {
      adjustment = (object) paymentAdjust as TDocumentAdjust;
    }
    else
    {
      adjustment = (TDocumentAdjust) ((PXSelectBase) this.GetAdjustView()).Cache.CreateInstance();
      PXCache cache = ((PXSelectBase) this.GetAdjustView()).Cache;
      foreach (string key in (IEnumerable<string>) cache.Keys)
        cache.SetValue((object) adjustment, key, ((PXSelectBase) adjustView).Cache.GetValue((object) paymentAdjust, key));
    }
    if ((object) adjustment == null)
      throw exception;
    this.RedirectToNewGraph(adjustment, exception);
  }

  public virtual void CopyError(TDocumentAdjust errorAdjustment, Exception exception)
  {
    this.CopyError<TDocumentAdjust>("AdjgDocType", (ICreatePaymentAdjust) errorAdjustment, exception);
  }

  protected virtual void CopyError<TAdjust>(
    string fieldName,
    ICreatePaymentAdjust errorAdjustment,
    Exception exception)
    where TAdjust : class, ICreatePaymentAdjust
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXGraph) (object) this.Base).RowSelected.AddHandler<TAdjust>(new PXRowSelected((object) new CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.\u003C\u003Ec__DisplayClass116_0<TAdjust>()
    {
      errorAdjustment = errorAdjustment,
      fieldName = fieldName,
      exception = exception
    }, __methodptr(\u003CCopyError\u003Eb__0)));
  }

  public virtual void IncreasePayment(
    TDocumentAdjust adjustment,
    ARPaymentEntry paymentEntry,
    ARPaymentEntryPaymentTransaction paymentTransaction,
    bool capture)
  {
    TDocument current = this.GetCurrent<TDocument>();
    this.CheckUnappliedBalance(paymentEntry, adjustment, current);
    if (capture)
      this.VerifyAdjustments(paymentEntry, "IncreaseAuthorizedAmount");
    this.IncreaseCCAuthorizedAmount(paymentEntry, capture);
  }

  public virtual void CapturePayment(
    TDocumentAdjust adjustment,
    ARPaymentEntry paymentEntry,
    ARPaymentEntryPaymentTransaction paymentTransaction)
  {
    this.VerifyAdjustments(paymentEntry, "CaptureDocumentPayment");
    if (((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current.Hold.GetValueOrDefault())
      throw new PXException("The payment cannot be captured because it has the On Hold status. To capture the payment, open it on the Payments and Applications (AR302000) form, and click Remove Hold.");
    this.ThrowIfCanNotCapture(adjustment, ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current);
    Decimal? nullable = adjustment.CuryAdjdAmt;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      throw new PXException("Amount to capture must be greater than zero.");
    PX.Objects.AR.ARPayment current = ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current;
    nullable = current.CuryDocBal;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    this.RemoveUnappliedBalance(paymentEntry);
    this.PressButtonIfEnabled((PXGraph) paymentEntry, "captureCCPayment");
    ARPaymentEntry paymentEntry1 = paymentEntry;
    TDocumentAdjust adjustment1 = adjustment;
    Decimal authorizedCuryPaymentAmount = valueOrDefault1;
    nullable = current.CuryDocBal;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    this.AuthorizeRemainderIfNeeded(paymentEntry1, adjustment1, authorizedCuryPaymentAmount, valueOrDefault2);
  }

  protected virtual void AuthorizeRemainderIfNeeded(
    ARPaymentEntry paymentEntry,
    TDocumentAdjust adjustment,
    Decimal authorizedCuryPaymentAmount,
    Decimal capturedCuryPaymentAmount)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.acumaticaPayments>())
      return;
    PX.Objects.AR.ARPayment current = ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current;
    if (current == null)
      return;
    PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) (object) this.Base, current.PaymentMethodID);
    bool flag = CCProcessingFeatureHelper.IsFeatureSupported(CCProcessingCenter.PK.Find((PXGraph) (object) this.Base, current.ProcessingCenterID), CCProcessingFeature.AuthorizeBasedOnPrevious);
    Decimal? nullable;
    int num1;
    if (current.IsCCCaptured.GetValueOrDefault())
    {
      nullable = current.CuryDocBal;
      Decimal num2 = authorizedCuryPaymentAmount;
      num1 = nullable.GetValueOrDefault() < num2 & nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    int num3 = flag ? 1 : 0;
    if ((num1 & num3) == 0)
      return;
    PX.Objects.SO.SOOrder remainderRequired = this.GetDocIfAuthorizeRemainderRequired(paymentEntry, adjustment);
    if (remainderRequired == null)
      return;
    Decimal val1 = authorizedCuryPaymentAmount - capturedCuryPaymentAmount;
    nullable = remainderRequired.CuryUnpaidBalance;
    Decimal val2 = nullable.GetValueOrDefault();
    ARPaymentEntry.MultiCurrency extension = ((PXGraph) paymentEntry).GetExtension<ARPaymentEntry.MultiCurrency>();
    if (current.CuryID != remainderRequired.CuryID)
    {
      nullable = remainderRequired.UnpaidBalance;
      Decimal valueOrDefault = nullable.GetValueOrDefault();
      val2 = extension.GetCurrencyInfo(current.CuryInfoID).CuryConvCury(valueOrDefault);
    }
    Decimal num4 = 2M * (Decimal) Math.Pow(10.0, (double) -((short?) extension?.GetCurrencyInfo(remainderRequired.CuryInfoID)?.CuryPrecision ?? (short) 2));
    Decimal amount = Math.Abs(val1 - val2) <= num4 ? val2 : Math.Min(val1, val2);
    SOQuickPayment remainderPayment = this.CreateRemainderPayment(current, remainderRequired, amount);
    if (remainderPayment == null)
      return;
    CreatePaymentExtBase<SOOrderEntry, PX.Objects.SO.SOOrder, SOAdjust> implementation = ((PXGraph) PXGraph.CreateInstance<SOOrderEntry>()).FindImplementation<CreatePaymentExtBase<SOOrderEntry, PX.Objects.SO.SOOrder, SOAdjust>>();
    implementation.SetCurrentDocument(remainderRequired);
    if (!implementation.CanCreatePayment())
      return;
    implementation.CreatePayment(remainderPayment, current.DocType, false);
  }

  protected virtual SOQuickPayment CreateRemainderPayment(
    PX.Objects.AR.ARPayment payment,
    PX.Objects.SO.SOOrder order,
    Decimal amount)
  {
    return new SOQuickPayment()
    {
      AdjgDocType = order.DocType,
      AdjgRefNbr = order.RefNbr,
      CuryID = payment.CuryID,
      CuryInfoID = payment.CuryInfoID,
      Authorize = new bool?(false),
      Capture = new bool?(false),
      AuthorizeRemainder = new bool?(true),
      IsRefund = new bool?(false),
      NewCard = new bool?(false),
      PaymentMethodID = payment.PaymentMethodID,
      PMInstanceID = payment.PMInstanceID,
      DocDesc = payment.DocDesc,
      CashAccountID = payment.CashAccountID,
      ProcessingCenterID = payment.ProcessingCenterID,
      UpdateNextNumber = payment.UpdateNextNumber,
      PreviousExternalTransactionID = payment.ExtRefNbr,
      CuryOrigDocAmt = new Decimal?(amount)
    };
  }

  protected virtual PX.Objects.SO.SOOrder GetDocIfAuthorizeRemainderRequired(
    ARPaymentEntry paymentEntry,
    TDocumentAdjust adjust)
  {
    return (PX.Objects.SO.SOOrder) null;
  }

  protected virtual bool CanCreatePayment() => false;

  protected virtual void RemoveUnappliedBalance(ARPaymentEntry paymentEntry)
  {
    PX.Objects.AR.ARPayment current = ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current;
    PXCache cache = ((PXSelectBase) paymentEntry.Document).Cache;
    Decimal? curyUnappliedBal1 = current.CuryUnappliedBal;
    Decimal num = 0M;
    if (!(curyUnappliedBal1.GetValueOrDefault() > num & curyUnappliedBal1.HasValue))
      return;
    PXCache pxCache = cache;
    PX.Objects.AR.ARPayment arPayment1 = current;
    Decimal? curyOrigDocAmt = current.CuryOrigDocAmt;
    Decimal? curyUnappliedBal2 = current.CuryUnappliedBal;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) (curyOrigDocAmt.HasValue & curyUnappliedBal2.HasValue ? new Decimal?(curyOrigDocAmt.GetValueOrDefault() - curyUnappliedBal2.GetValueOrDefault()) : new Decimal?());
    pxCache.SetValueExt<PX.Objects.AR.ARPayment.curyOrigDocAmt>((object) arPayment1, (object) local);
    PX.Objects.AR.ARPayment arPayment2 = (PX.Objects.AR.ARPayment) cache.Update((object) current);
  }

  protected virtual void CheckUnappliedBalance(
    ARPaymentEntry paymentEntry,
    TDocumentAdjust adjust,
    TDocument doc)
  {
    PX.Objects.AR.ARPayment current = ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current;
    Decimal? curyUnappliedBal = current.CuryUnappliedBal;
    Decimal num = 0M;
    if (curyUnappliedBal.GetValueOrDefault() > num & curyUnappliedBal.HasValue)
    {
      string documentType = this.GetDocumentType(doc);
      string documentNbr = this.GetDocumentNbr(doc);
      string displayName = ARDocType.GetDisplayName(current.DocType ?? "PMT");
      throw new PXException("The {0} {1} has the available balance of {2} {3} that can be applied to the current {4} document with the {5} ref. number.", new object[6]
      {
        (object) current.RefNbr,
        (object) displayName,
        (object) current.CuryUnappliedBal,
        (object) current.CuryID,
        (object) documentType,
        (object) documentNbr
      });
    }
  }

  public virtual void VoidPayment(
    TDocumentAdjust adjustment,
    ARPaymentEntry paymentEntry,
    ARPaymentEntryPaymentTransaction paymentTransaction)
  {
    this.VerifyAdjustments(paymentEntry, "VoidDocumentPayment");
    PX.Objects.AR.ARPayment current = ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current;
    this.ThrowIfCanNotVoid(adjustment, ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current, "voidCCPayment");
    ExternalTransactionState transactionState = paymentTransaction.GetActiveTransactionState();
    if (current.PendingProcessing.GetValueOrDefault() && transactionState.IsPreAuthorized)
    {
      string extRefNbr = ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current.ExtRefNbr;
      this.PressButtonIfEnabled((PXGraph) paymentEntry, "voidCCPayment");
      ((PXGraph) paymentEntry).Clear();
      ((PXGraph) paymentEntry).Clear((PXClearOption) 4);
      ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Search<PX.Objects.AR.ARPayment.refNbr>((object) adjustment.AdjgRefNbr, new object[1]
      {
        (object) adjustment.AdjgDocType
      }));
      ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current.ExtRefNbr = extRefNbr;
      this.PressButtonIfEnabled((PXAction) paymentEntry.voidCheck);
    }
    else
    {
      bool? nullable = current.Released;
      if (!nullable.GetValueOrDefault())
        return;
      nullable = current.OpenDoc;
      if (!nullable.GetValueOrDefault() || !transactionState.IsCaptured)
        return;
      this.PressButtonIfEnabled((PXAction) paymentEntry.voidCheck, true);
      ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Search<PX.Objects.AR.ARPayment.refNbr>((object) ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current.RefNbr, new object[1]
      {
        (object) "RPM"
      }));
      if (((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current == null)
        throw new RowNotFoundException(((PXSelectBase) paymentEntry.Document).Cache, new object[2]
        {
          (object) ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current.RefNbr,
          (object) "RPM"
        });
      nullable = ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current.Hold;
      if (nullable.GetValueOrDefault())
      {
        ((PXSelectBase) paymentEntry.Document).Cache.SetValueExt<PX.Objects.AR.ARPayment.hold>((object) ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current, (object) false);
        ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).UpdateCurrent();
      }
      this.PressButtonIfEnabled((PXGraph) paymentEntry, "voidCCPayment");
      ((PXAction) paymentEntry.Save).Press();
    }
  }

  public virtual void VoidCCTransactionForReAuthorization(
    TDocumentAdjust adjustment,
    ARPaymentEntry paymentEntry,
    ARPaymentEntryPaymentTransaction paymentTransaction)
  {
    this.VerifyAdjustments(paymentEntry, "VoidDocumentPayment");
    PX.Objects.AR.ARPayment current = ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current;
    this.ThrowIfCanNotVoid(adjustment, ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current, "voidCCPaymentForReAuthorization");
    ExternalTransactionState transactionState = paymentTransaction.GetActiveTransactionState();
    if (!current.PendingProcessing.GetValueOrDefault() || !transactionState.IsPreAuthorized)
      throw new PXActionDisabledException("voidCCPaymentForReAuthorization");
    this.PressButtonIfEnabled((PXGraph) paymentEntry, "voidCCPaymentForReAuthorization");
  }

  public virtual void ValidatePayment(
    TDocumentAdjust adjustment,
    ARPaymentEntry paymentEntry,
    ARPaymentEntryPaymentTransaction paymentTransaction)
  {
    this.VerifyAdjustments(paymentEntry, "CaptureDocumentPayment");
    this.PressButtonIfEnabled((PXGraph) paymentEntry, "validateCCPayment");
  }

  public virtual void AuthorizePayment(
    TDocumentAdjust adjustment,
    ARPaymentEntry paymentEntry,
    ARPaymentEntryPaymentTransaction paymentTransaction)
  {
    this.ThrowIfCanNotAuthorize(adjustment, ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current);
    this.VerifyAdjustments(paymentEntry, "CaptureDocumentPayment");
    this.RemoveUnappliedBalance(paymentEntry);
    this.PressButtonIfEnabled((PXGraph) paymentEntry, "authorizeCCPayment");
  }

  public virtual void PressButtonIfEnabled(PXGraph graph, string actionName)
  {
    if (!((OrderedDictionary) graph.Actions).Contains((object) actionName))
      throw new PXException("The {0} action cannot be found.", new object[1]
      {
        (object) actionName
      });
    this.PressButtonIfEnabled(graph.Actions[actionName]);
  }

  protected virtual void PressButtonIfEnabled(PXAction action)
  {
    this.PressButtonIfEnabled(action, false);
  }

  protected virtual void PressButtonIfEnabled(PXAction action, bool suppressRedirectExc)
  {
    try
    {
      if (!action.GetEnabled())
        throw new PXActionDisabledException((action.GetState((object) null) is PXButtonState state ? ((PXFieldState) state).DisplayName : (string) null) ?? ((PXFieldState) state)?.Name);
      action.Press();
    }
    catch (PXBaseRedirectException ex) when (suppressRedirectExc)
    {
    }
  }

  protected virtual PXFieldSelecting CreateVoidCaptureFieldSelecting(
    string fieldName,
    Func<TDocumentAdjust, PX.Objects.AR.ARPayment, bool> funcGetValue)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    return new PXFieldSelecting((object) new CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.\u003C\u003Ec__DisplayClass132_0()
    {
      fieldName = fieldName,
      \u003C\u003E4__this = this,
      funcGetValue = funcGetValue
    }, __methodptr(\u003CCreateVoidCaptureFieldSelecting\u003Eb__0));
  }

  protected virtual bool CanVoid(TDocumentAdjust adjust, PX.Objects.AR.ARPayment payment)
  {
    return this.CanVoid(adjust, payment, false, (string) null);
  }

  protected virtual bool ThrowIfCanNotVoid(
    TDocumentAdjust adjust,
    PX.Objects.AR.ARPayment payment,
    string actionName)
  {
    return this.CanVoid(adjust, payment, true, actionName);
  }

  private bool CanVoid(
    TDocumentAdjust adjust,
    PX.Objects.AR.ARPayment payment,
    bool throwOnFalse,
    string actionName)
  {
    // ISSUE: variable of a boxed type
    __Boxed<TDocumentAdjust> local = (object) adjust;
    if ((local != null ? (!local.IsCCPayment.GetValueOrDefault() ? 1 : 0) : 1) != 0 || (payment == null || !payment.PendingProcessing.GetValueOrDefault() || !payment.IsCCAuthorized.GetValueOrDefault()) && (payment == null || !payment.Released.GetValueOrDefault() || !payment.OpenDoc.GetValueOrDefault() || !payment.IsCCCaptured.GetValueOrDefault()))
    {
      if (throwOnFalse)
        throw new PXActionDisabledException(actionName);
      return false;
    }
    CCProcessingCenter processingCenter = CCProcessingCenter.PK.Find((PXGraph) (object) this.Base, payment.ProcessingCenterID);
    if (throwOnFalse)
      CCPluginTypeHelper.ThrowIfProcCenterFeatureDisabled(processingCenter.ProcessingTypeName);
    else if (CCPluginTypeHelper.IsProcCenterFeatureDisabled(processingCenter.ProcessingTypeName))
      return false;
    if (processingCenter.IsActive.GetValueOrDefault())
      return true;
    if (throwOnFalse)
      throw new PXException("Processing center {0} is inactive", new object[1]
      {
        (object) processingCenter.ProcessingCenterID
      });
    return false;
  }

  protected virtual bool CanCapture(TDocumentAdjust adjust, PX.Objects.AR.ARPayment payment)
  {
    return this.CanCapture(adjust, payment, false);
  }

  protected virtual void ThrowIfCanNotCapture(TDocumentAdjust adjust, PX.Objects.AR.ARPayment payment)
  {
    this.CanCapture(adjust, payment, true);
  }

  private bool CanCapture(TDocumentAdjust adjust, PX.Objects.AR.ARPayment payment, bool throwOnFalse)
  {
    // ISSUE: variable of a boxed type
    __Boxed<TDocumentAdjust> local = (object) adjust;
    if ((local != null ? (!local.IsCCPayment.GetValueOrDefault() ? 1 : 0) : 1) != 0 || (payment != null ? (!payment.PendingProcessing.GetValueOrDefault() ? 1 : 0) : 1) != 0 || !payment.IsCCAuthorized.GetValueOrDefault())
    {
      if (throwOnFalse)
        throw new PXActionDisabledException("captureCCPayment");
      return false;
    }
    CCProcessingCenter processingCenter = CCProcessingCenter.PK.Find((PXGraph) (object) this.Base, payment.ProcessingCenterID);
    if (throwOnFalse)
      CCPluginTypeHelper.ThrowIfProcCenterFeatureDisabled(processingCenter.ProcessingTypeName);
    else if (CCPluginTypeHelper.IsProcCenterFeatureDisabled(processingCenter.ProcessingTypeName))
      return false;
    if (processingCenter.IsActive.GetValueOrDefault())
      return true;
    if (throwOnFalse)
      throw new PXException("Processing center {0} is inactive", new object[1]
      {
        (object) processingCenter.ProcessingCenterID
      });
    return false;
  }

  protected virtual void ThrowIfCanNotAuthorize(TDocumentAdjust adjust, PX.Objects.AR.ARPayment payment)
  {
    if (payment == null)
      throw new PXActionDisabledException("authorizeCCPayment");
    CCProcessingCenter processingCenter = CCProcessingCenter.PK.Find((PXGraph) (object) this.Base, payment.ProcessingCenterID);
    CCPluginTypeHelper.ThrowIfProcCenterFeatureDisabled(processingCenter.ProcessingTypeName);
    if (!processingCenter.IsActive.GetValueOrDefault())
      throw new PXException("Processing center {0} is inactive", new object[1]
      {
        (object) processingCenter.ProcessingCenterID
      });
  }

  protected virtual bool CanIncreaseAuthorizedAmount(TDocumentAdjust adjust, PX.Objects.AR.ARPayment payment)
  {
    // ISSUE: variable of a boxed type
    __Boxed<TDocumentAdjust> local = (object) adjust;
    if ((local != null ? (!local.IsCCPayment.GetValueOrDefault() ? 1 : 0) : 1) != 0 || payment == null)
      return false;
    CCProcessingCenter processingCenter = CCProcessingCenter.PK.Find((PXGraph) (object) this.Base, payment.ProcessingCenterID);
    return payment != null && payment.PendingProcessing.GetValueOrDefault() && payment.IsCCAuthorized.GetValueOrDefault() && !payment.IsCCCaptured.GetValueOrDefault() && ((bool?) processingCenter?.AllowAuthorizedIncrement).GetValueOrDefault();
  }

  protected virtual void AssignAuthorize()
  {
    ((PXSelectBase<SOQuickPayment>) this.QuickPayment).Current.Authorize = new bool?(true);
  }

  protected virtual void AssignCapture()
  {
    ((PXSelectBase<SOQuickPayment>) this.QuickPayment).Current.Capture = new bool?(true);
  }

  protected virtual void AssignRefund()
  {
    ((PXSelectBase<SOQuickPayment>) this.QuickPayment).Current.Refund = new bool?(true);
  }

  protected virtual bool IsMultipleApplications(ARPaymentEntry paymentEntry)
  {
    return this.IsMultipleApplications(paymentEntry, out ARPaymentTotals _, out PX.Objects.SO.SOInvoice _);
  }

  protected virtual bool IsMultipleApplications(
    ARPaymentEntry paymentEntry,
    out ARPaymentTotals paymentTotals,
    out PX.Objects.SO.SOInvoice invoice)
  {
    invoice = (PX.Objects.SO.SOInvoice) null;
    PX.Objects.AR.ARPayment current = ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current;
    paymentTotals = ARPaymentTotals.PK.Find((PXGraph) paymentEntry, current.DocType, current.RefNbr);
    if (paymentTotals != null)
    {
      int? nullable = paymentTotals.OrderCntr;
      int num1 = 0;
      if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      {
        nullable = paymentTotals.InvoiceCntr;
        int num2 = 0;
        if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
          goto label_3;
      }
      nullable = paymentTotals.OrderCntr;
      int num3 = 0;
      if (nullable.GetValueOrDefault() > num3 & nullable.HasValue && paymentTotals.AdjdOrderNbr != null)
      {
        nullable = paymentTotals.InvoiceCntr;
        int num4 = 0;
        if (nullable.GetValueOrDefault() == num4 & nullable.HasValue)
          return false;
      }
      nullable = paymentTotals.InvoiceCntr;
      int num5 = 0;
      if (nullable.GetValueOrDefault() > num5 & nullable.HasValue && paymentTotals.AdjdRefNbr != null)
      {
        nullable = paymentTotals.OrderCntr;
        int num6 = 0;
        if (nullable.GetValueOrDefault() == num6 & nullable.HasValue)
          return false;
      }
      if (paymentTotals.AdjdRefNbr == null || paymentTotals.AdjdOrderNbr == null)
        return true;
      invoice = PX.Objects.SO.SOInvoice.PK.Find((PXGraph) paymentEntry, paymentTotals.AdjdDocType, paymentTotals.AdjdRefNbr);
      return invoice == null || paymentTotals.AdjdOrderType != invoice.SOOrderType || paymentTotals.AdjdOrderNbr != invoice.SOOrderNbr;
    }
label_3:
    return false;
  }

  protected virtual string GetPaymentErrorFieldName() => "AdjgRefNbr";

  protected virtual bool CheckIncreaseAmount()
  {
    SOIncreaseAuthorizedAmountDialog current = ((PXSelectBase<SOIncreaseAuthorizedAmountDialog>) this.IncreaseAmount).Current;
    Decimal? authorizedAmtNew = current.CuryAuthorizedAmtNew;
    Decimal? nullable1 = authorizedAmtNew;
    Decimal? authorizedAmtMax = current.CuryAuthorizedAmtMax;
    if (!(nullable1.GetValueOrDefault() <= authorizedAmtMax.GetValueOrDefault() & nullable1.HasValue & authorizedAmtMax.HasValue))
      return false;
    Decimal? nullable2 = authorizedAmtNew;
    Decimal? curyAuthorizedAmt = current.CuryAuthorizedAmt;
    return nullable2.GetValueOrDefault() > curyAuthorizedAmt.GetValueOrDefault() & nullable2.HasValue & curyAuthorizedAmt.HasValue;
  }

  protected abstract PXSelectBase<TDocumentAdjust> GetAdjustView();

  protected abstract PXSelectBase<TPaymentAdjust> GetAdjustView(ARPaymentEntry paymentEntry);

  protected abstract CustomerClass GetCustomerClass();

  protected abstract void SetCurrentDocument(TDocument document);

  protected abstract void AddAdjust(ARPaymentEntry paymentEntry, TDocument document);

  protected abstract void VerifyAdjustments(ARPaymentEntry paymentEntry, string actionName);

  protected abstract string GetDocumentDescr(TDocument document);

  protected abstract string GetDocumentType(TDocument document);

  protected abstract string GetDocumentNbr(TDocument document);

  protected abstract ARSetup GetARSetup();

  protected abstract Type GetPaymentMethodField();

  protected abstract bool IsCashSale();

  protected abstract string GetCCPaymentIsNotSupportedMessage();

  protected abstract Type GetDocumentPMInstanceIDField();

  protected abstract bool HasReturnLineForOrigTran(string procCenterID, string tranNumber);

  protected abstract bool ValidateCCRefundOrigTransaction();

  protected abstract long? GetOrigDocumentCurrencyInfoID();

  protected abstract bool CanIncreaseAndCapture { get; }

  protected class EndPaymentOperationCustomInfo : IPXCustomInfo
  {
    protected string _adjgDocType;
    protected string _adjgRefNbr;
    protected PXBaseRedirectException _redirectException;
    protected Exception _processPaymentException;
    protected TDocumentAdjust _failedPayment;

    public EndPaymentOperationCustomInfo()
    {
    }

    public EndPaymentOperationCustomInfo(
      string adjgDocType,
      string adjgRefNbr,
      PXBaseRedirectException redirectException)
    {
      this._adjgDocType = adjgDocType;
      this._adjgRefNbr = adjgRefNbr;
      this._redirectException = redirectException;
    }

    public EndPaymentOperationCustomInfo(
      TDocumentAdjust failedPayment,
      Exception processPaymentException)
    {
      this._failedPayment = failedPayment;
      this._processPaymentException = processPaymentException;
    }

    public void Complete(PXLongRunStatus status, PXGraph graph)
    {
      if (EnumerableExtensions.IsNotIn<PXLongRunStatus>(status, (PXLongRunStatus) 2, (PXLongRunStatus) 3))
        return;
      CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust> implementation = graph.FindImplementation<CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>>();
      if (implementation == null)
      {
        if (this._redirectException != null)
          throw this._redirectException;
        if (this._processPaymentException != null)
          throw this._processPaymentException;
      }
      else
      {
        ((PXSelectBase) implementation.GetAdjustView()).Cache.ClearQueryCache();
        if (status == 2)
        {
          if (this._adjgDocType != null && this._adjgRefNbr != null)
          {
            ((PXSelectBase<SOQuickPayment>) implementation.QuickPayment).Current.AdjgDocType = this._adjgDocType;
            ((PXSelectBase<SOQuickPayment>) implementation.QuickPayment).Current.AdjgRefNbr = this._adjgRefNbr;
          }
          if (this._redirectException != null)
            throw this._redirectException;
        }
        else
        {
          if ((object) this._failedPayment == null || this._processPaymentException == null)
            return;
          implementation.CopyError(this._failedPayment, this._processPaymentException);
        }
      }
    }
  }

  public class LongOperationParameterBase
  {
    public TDocument Document { get; set; }
  }

  public class SyncPaymentTransactionParameter : 
    CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.LongOperationParameterBase
  {
    public string CancelStr { get; set; }

    public string TranResponseStr { get; set; }

    public string AdjgRefNbr { get; set; }

    public string AdjgDocType { get; set; }
  }

  public class CreatePaymentParameter : 
    CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.LongOperationParameterBase
  {
    public string CcPaymentConnectorUrl { get; set; }

    public SOQuickPayment QuickPayment { get; set; }

    public string PaymentType { get; set; }

    public bool ThrowErrors { get; set; }
  }

  public class ImportPaymentParameter : 
    CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.LongOperationParameterBase
  {
    public SOImportExternalTran Panel { get; set; }
  }

  public class ExecutePaymentTransactionActionParameter : 
    CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>.LongOperationParameterBase
  {
    public TDocumentAdjust Adjustment { get; set; }

    public Action<CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>, TDocumentAdjust, ARPaymentEntry, ARPaymentEntryPaymentTransaction> PaymentAction { get; set; }
  }
}
