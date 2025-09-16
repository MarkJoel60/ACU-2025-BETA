// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.APPaymentBalanceCalculator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.Extensions.MultiCurrency;
using System;

#nullable disable
namespace PX.Objects.CM.Extensions;

internal class APPaymentBalanceCalculator(IPXCurrencyHelper curyhelper) : 
  AbstractPaymentBalanceCalculator<APAdjust, APTran>(curyhelper)
{
  public APPaymentBalanceCalculator(PXSelectBase<PX.Objects.CM.CurrencyInfo> curyInfoSelect)
    : this((IPXCurrencyHelper) new CuryHelper(curyInfoSelect))
  {
  }

  protected override void AfterBalanceCalculatedBeforeBalanceAjusted<T>(
    APAdjust adj,
    T voucher,
    bool DiscOnDiscDate,
    APTran tran)
  {
    adj.CuryOrigDocAmt = (Decimal?) tran?.CuryOrigTranAmt ?? voucher.CuryOrigDocAmt;
    adj.OrigDocAmt = (Decimal?) tran?.OrigTranAmt ?? voucher.OrigDocAmt;
    if (DiscOnDiscDate)
      PaymentEntry.CalcDiscount(adj.AdjgDocDate, (IInvoice) voucher, (IAdjustment) adj);
    base.AfterBalanceCalculatedBeforeBalanceAjusted<T>(adj, voucher, DiscOnDiscDate, tran);
  }

  protected override bool ShouldRgolBeResetInZero(APAdjust adj)
  {
    return (adj.AdjgDocType == "CHK" || adj.AdjgDocType == "VCK" || adj.AdjgDocType == "PPM") && adj.AdjdDocType == "PPM";
  }
}
