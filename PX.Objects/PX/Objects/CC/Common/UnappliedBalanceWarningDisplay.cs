// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.Common.UnappliedBalanceWarningDisplay
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using System;

#nullable disable
namespace PX.Objects.CC.Common;

[Obsolete("This class has been deprecated and will be removed in the later Acumatica versions.")]
public sealed class UnappliedBalanceWarningDisplay : IPXCustomInfo
{
  private readonly Decimal? _appliedAmount;
  private readonly string _docType;
  private readonly string _docNbr;
  private readonly Decimal? _curyDocBal;
  private readonly string _paymentCurrency;
  private readonly string _docCurrency;

  /// <summary>
  /// Warning in case of unpaid document balance is less than amount to be applied
  /// </summary>
  /// <param name="appliedAmount">Amount to be applied</param>
  /// <param name="docType">Type of document (key)</param>
  /// <param name="docNbr">Number of document (key)</param>
  /// <param name="unpaidBalance">Amount of unpaid balance</param>
  /// <param name="paymentCurrency">Currency of payment</param>
  /// <param name="docCurrency">Currency of document</param>
  public UnappliedBalanceWarningDisplay(
    Decimal? appliedAmount,
    string docType,
    string docNbr,
    Decimal? unpaidBalance,
    string paymentCurrency,
    string docCurrency)
  {
    this._appliedAmount = appliedAmount;
    this._docType = docType;
    this._docNbr = docNbr;
    this._curyDocBal = unpaidBalance;
    this._paymentCurrency = paymentCurrency;
    this._docCurrency = docCurrency;
  }

  public void Complete(PXLongRunStatus status, PXGraph graph)
  {
    if (status != 2 || !(graph is ARPaymentEntry))
      return;
    // ISSUE: method pointer
    graph.RowSelected.AddHandler<ARPayment>(new PXRowSelected((object) this, __methodptr(\u003CComplete\u003Eb__7_0)));
  }
}
