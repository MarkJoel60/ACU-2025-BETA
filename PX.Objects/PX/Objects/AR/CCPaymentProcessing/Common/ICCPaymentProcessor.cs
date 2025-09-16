// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Common.ICCPaymentProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Common;

public interface ICCPaymentProcessor
{
  TranOperationResult Authorize(ICCPayment payment, bool aCapture);

  TranOperationResult IncreaseAuthorizedAmount(ICCPayment payment, int? transactionId);

  TranOperationResult Capture(ICCPayment payment, int? transactionId);

  TranOperationResult CaptureOnly(ICCPayment payment, string aAuthorizationNbr);

  TranOperationResult Credit(ICCPayment payment, string aExtRefTranNbr, string procCetnerId);

  TranOperationResult Credit(ICCPayment payment, int? transactionId);

  TranOperationResult Void(ICCPayment payment, int? transactionId);

  TranOperationResult VoidOrCredit(ICCPayment payment, int? transactionId);

  /// <summary>Update Level 3 data for transaction.</summary>
  /// <param name="payment">Document for processing.</param>
  /// <param name="transactionId">Transaction ID.</param>
  /// <returns></returns>
  TranOperationResult UpdateLevel3Data(PX.Objects.Extensions.PaymentTransaction.Payment payment, int? transactionId);
}
