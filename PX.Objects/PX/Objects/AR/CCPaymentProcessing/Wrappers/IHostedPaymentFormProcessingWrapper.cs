// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Wrappers.IHostedPaymentFormProcessingWrapper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Wrappers;

public interface IHostedPaymentFormProcessingWrapper
{
  void GetPaymentForm(ProcessingInput inputData);

  HostedFormResponse ParsePaymentFormResponse(string response);

  PXPluginRedirectOptions PreparePaymentForm(PaymentFormPrepareOptions inputData);

  PaymentFormResponseProcessResult ProcessPaymentFormResponse(
    PaymentFormPrepareOptions inputData,
    string response);
}
