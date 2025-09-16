// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.ARPaymentEntryExt.Correction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.ARPaymentEntryExt;

public class Correction : ARAdjustCorrectionExtension<ARPaymentEntry>
{
  [PXOverride]
  public virtual void CreatePayment(
    PX.Objects.AR.ARInvoice ardoc,
    PX.Objects.CM.Extensions.CurrencyInfo info,
    DateTime? paymentDate,
    string aFinPeriod,
    bool overrideDesc,
    Correction.CreatePaymentDelegate baseMethod)
  {
    if (ardoc.IsUnderCorrection.GetValueOrDefault())
      throw new PXException("The application cannot be created because another cancellation invoice or correction invoice already exists for the invoice {0}.", new object[1]
      {
        (object) ardoc.RefNbr
      });
    baseMethod(ardoc, info, paymentDate, aFinPeriod, overrideDesc);
  }

  [PXOverride]
  public virtual void ReverseApplicationProc(
    ARAdjust application,
    ARPayment payment,
    Action<ARAdjust, ARPayment> baseMethod)
  {
    if (payment != null && payment.IsCancellation.GetValueOrDefault())
      throw new PXException("The application of the cancellation credit memo {0} to invoice {1} cannot be reversed.", new object[2]
      {
        (object) application.AdjgRefNbr,
        (object) application.AdjdRefNbr
      });
    baseMethod(application, payment);
  }

  public delegate void CreatePaymentDelegate(
    PX.Objects.AR.ARInvoice ardoc,
    PX.Objects.CM.Extensions.CurrencyInfo info,
    DateTime? paymentDate,
    string aFinPeriod,
    bool overrideDesc);
}
