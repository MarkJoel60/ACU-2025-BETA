// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Common.ICCTransactionsProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.AR.CCPaymentProcessing.Interfaces;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Common;

public interface ICCTransactionsProcessor
{
  void ProcessAuthorize(ICCPayment doc, IExternalTransaction tran);

  void ProcessIncreaseAuthorizedAmount(ICCPayment doc, IExternalTransaction tran);

  void ProcessAuthorizeCapture(ICCPayment doc, IExternalTransaction tran);

  void ProcessPriorAuthorizedCapture(ICCPayment doc, IExternalTransaction tran);

  void ProcessVoid(ICCPayment doc, IExternalTransaction tran);

  void ProcessVoidOrCredit(ICCPayment doc, IExternalTransaction tran);

  void ProcessCredit(ICCPayment doc, IExternalTransaction tran);

  void ProcessCaptureOnly(ICCPayment doc, IExternalTransaction tran);

  /// <summary>Update Level 3 data for transaction.</summary>
  /// <param name="doc">Document for processing.</param>
  /// <param name="tran">External transaction with Level 3 Data.</param>
  /// <returns></returns>
  void UpdateLevel3Data(PX.Objects.Extensions.PaymentTransaction.Payment doc, IExternalTransaction tran);
}
