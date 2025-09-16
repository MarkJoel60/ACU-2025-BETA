// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.CCTransactionsProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using System;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing;

public class CCTransactionsProcessor : ICCTransactionsProcessor
{
  private ICCPaymentProcessor _processingClass;

  protected CCTransactionsProcessor(ICCPaymentProcessor processingClass)
  {
    this._processingClass = processingClass;
  }

  public static ICCTransactionsProcessor GetCCTransactionsProcessor()
  {
    return (ICCTransactionsProcessor) new CCTransactionsProcessor(PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing.GetCCPaymentProcessing());
  }

  public void ProcessAuthorize(ICCPayment doc, IExternalTransaction tran)
  {
    this.CheckInput(doc, tran);
    this.Process((Func<TranOperationResult>) (() => this._processingClass.Authorize(doc, false)));
  }

  public void ProcessIncreaseAuthorizedAmount(ICCPayment doc, IExternalTransaction tran)
  {
    this.CheckInput(doc, tran);
    this.Process((Func<TranOperationResult>) (() => this._processingClass.IncreaseAuthorizedAmount(doc, tran.TransactionID)));
  }

  public void ProcessAuthorizeCapture(ICCPayment doc, IExternalTransaction tran)
  {
    this.CheckInput(doc, tran);
    this.Process((Func<TranOperationResult>) (() => this._processingClass.Authorize(doc, true)));
  }

  public void ProcessPriorAuthorizedCapture(ICCPayment doc, IExternalTransaction tran)
  {
    this.CheckInput(doc, tran);
    this.Process((Func<TranOperationResult>) (() => this._processingClass.Capture(doc, tran.TransactionID)));
  }

  public void ProcessVoid(ICCPayment doc, IExternalTransaction tran)
  {
    this.CheckInput(doc, tran);
    this.Process((Func<TranOperationResult>) (() => this._processingClass.Void(doc, tran.TransactionID)));
  }

  public void ProcessVoidOrCredit(ICCPayment doc, IExternalTransaction tran)
  {
    this.CheckInput(doc, tran);
    this.Process((Func<TranOperationResult>) (() => this._processingClass.VoidOrCredit(doc, tran.TransactionID)));
  }

  public void ProcessCredit(ICCPayment doc, IExternalTransaction tran)
  {
    this.CheckInput(doc, tran);
    this.Process((Func<TranOperationResult>) (() => !tran.TransactionID.HasValue ? this._processingClass.Credit(doc, tran.TranNumber, tran.ProcessingCenterID) : this._processingClass.Credit(doc, new int?(tran.TransactionID.Value))));
  }

  public void ProcessCaptureOnly(ICCPayment doc, IExternalTransaction tran)
  {
    this.CheckInput(doc, tran);
    this.Process((Func<TranOperationResult>) (() => this._processingClass.CaptureOnly(doc, tran.AuthNumber)));
  }

  public void UpdateLevel3Data(PX.Objects.Extensions.PaymentTransaction.Payment doc, IExternalTransaction tran)
  {
    this.CheckInput((ICCPayment) doc, (IExternalTransaction) null);
    this.Process((Func<TranOperationResult>) (() => this._processingClass.UpdateLevel3Data(doc, tran.TransactionID)));
  }

  private void CheckInput(ICCPayment doc, IExternalTransaction tran)
  {
    if (doc == null)
      throw new ArgumentNullException(nameof (doc));
  }

  private void Process(Func<TranOperationResult> func)
  {
    TranOperationResult tranOperationResult = func();
    if (!tranOperationResult.Success)
      throw new PXException("Processing of the {0} transaction failed. See the transaction description for details.", new object[1]
      {
        (object) tranOperationResult.TransactionId
      });
  }
}
