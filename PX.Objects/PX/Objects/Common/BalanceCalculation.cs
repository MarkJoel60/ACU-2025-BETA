// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.BalanceCalculation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.Abstractions;
using PX.Objects.Common.Extensions;
using System;

#nullable disable
namespace PX.Objects.Common;

public static class BalanceCalculation
{
  /// <summary>
  /// For an application of a payment to an invoice, calculates the
  /// payment's document balance in the currency of the target invoice
  /// document.
  /// </summary>
  /// <param name="paymentBalanceInBase">
  /// The balance of the payment document in the base currency.
  /// </param>
  /// <param name="paymentBalanceInCurrency">
  /// The balance of the payment document in the document's currency.
  /// Will be re-used in case when the invoice's currency is the same
  /// as the payment's currency.
  /// </param>
  public static Decimal CalculateApplicationDocumentBalance(
    PX.Objects.CM.Extensions.CurrencyInfo paymentCurrencyInfo,
    PX.Objects.CM.Extensions.CurrencyInfo invoiceCurrencyInfo,
    Decimal? paymentBalanceInBase,
    Decimal? paymentBalanceInCurrency)
  {
    return string.Equals(paymentCurrencyInfo.CuryID, invoiceCurrencyInfo.CuryID) ? paymentBalanceInCurrency.GetValueOrDefault() : invoiceCurrencyInfo.CuryConvCury(paymentBalanceInBase.GetValueOrDefault());
  }

  /// <summary>
  /// For an application of a payment to an invoice, calculates the
  /// value of the <see cref="P:PX.Objects.AR.ARAdjust.CuryDocBal" /> field, which is
  /// the remaining balance of the applied payment.
  /// </summary>
  public static void CalculateApplicationDocumentBalance(
    ARPayment payment,
    IAdjustment application,
    PX.Objects.CM.Extensions.CurrencyInfo paymentCurrencyInfo,
    PX.Objects.CM.Extensions.CurrencyInfo invoiceCurrencyInfo)
  {
    Decimal applicationDocumentBalance = BalanceCalculation.CalculateApplicationDocumentBalance(paymentCurrencyInfo, invoiceCurrencyInfo, payment.Released.GetValueOrDefault() ? payment.DocBal : payment.OrigDocAmt, payment.Released.GetValueOrDefault() ? payment.CuryDocBal : payment.CuryOrigDocAmt);
    if (application == null)
      return;
    bool? released = application.Released;
    bool flag = false;
    if (released.GetValueOrDefault() == flag & released.HasValue)
    {
      Decimal? curyAdjdAmt = application.CuryAdjdAmt;
      Decimal num1 = applicationDocumentBalance;
      if (curyAdjdAmt.GetValueOrDefault() > num1 & curyAdjdAmt.HasValue)
      {
        application.CuryDocBal = new Decimal?(applicationDocumentBalance);
        application.CuryAdjdAmt = new Decimal?(0M);
      }
      else
      {
        IAdjustment adjustment = application;
        Decimal num2 = applicationDocumentBalance;
        curyAdjdAmt = application.CuryAdjdAmt;
        Decimal? nullable = curyAdjdAmt.HasValue ? new Decimal?(num2 - curyAdjdAmt.GetValueOrDefault()) : new Decimal?();
        adjustment.CuryDocBal = nullable;
      }
    }
    else
      application.CuryDocBal = new Decimal?(applicationDocumentBalance);
  }

  /// <summary>
  /// Given an application, returns a <see cref="T:PX.Objects.Common.FullBalanceDelta" />
  /// object indicating by how much the application should reduce the
  /// balances of the adjusting and the adjusted document.
  /// </summary>
  public static FullBalanceDelta GetFullBalanceDelta(this IAdjustmentAmount application)
  {
    FullBalanceDelta fullBalanceDelta = new FullBalanceDelta();
    fullBalanceDelta.BaseAdjustingBalanceDelta = application.AdjAmt.GetValueOrDefault();
    fullBalanceDelta.CurrencyAdjustingBalanceDelta = application.CuryAdjgAmt.GetValueOrDefault();
    ref FullBalanceDelta local1 = ref fullBalanceDelta;
    Decimal? nullable1 = application.AdjDiscAmt;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    nullable1 = application.AdjThirdAmount;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal num1 = valueOrDefault1 + valueOrDefault2;
    Decimal? nullable2;
    if (!application.ReverseGainLoss.GetValueOrDefault())
    {
      nullable2 = application.RGOLAmt;
    }
    else
    {
      nullable1 = application.RGOLAmt;
      nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
    }
    nullable1 = nullable2;
    Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
    Decimal num2 = num1 + valueOrDefault3;
    local1.BaseAdjustedExtraAmount = num2;
    ref FullBalanceDelta local2 = ref fullBalanceDelta;
    nullable1 = application.CuryAdjdDiscAmt;
    Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
    nullable1 = application.CuryAdjdThirdAmount;
    Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
    Decimal num3 = valueOrDefault4 + valueOrDefault5;
    local2.CurrencyAdjustedExtraAmount = num3;
    ref FullBalanceDelta local3 = ref fullBalanceDelta;
    nullable1 = application.CuryAdjgDiscAmt;
    Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
    nullable1 = application.CuryAdjgThirdAmount;
    Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
    Decimal num4 = valueOrDefault6 + valueOrDefault7;
    local3.CurrencyAdjustingExtraAmount = num4;
    ref FullBalanceDelta local4 = ref fullBalanceDelta;
    nullable1 = application.AdjAmt;
    Decimal valueOrDefault8 = nullable1.GetValueOrDefault();
    nullable1 = application.AdjDiscAmt;
    Decimal valueOrDefault9 = nullable1.GetValueOrDefault();
    Decimal num5 = valueOrDefault8 + valueOrDefault9;
    nullable1 = application.AdjThirdAmount;
    Decimal valueOrDefault10 = nullable1.GetValueOrDefault();
    Decimal num6 = num5 + valueOrDefault10;
    Decimal? nullable3;
    if (!application.ReverseGainLoss.GetValueOrDefault())
    {
      nullable3 = application.RGOLAmt;
    }
    else
    {
      nullable1 = application.RGOLAmt;
      nullable3 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
    }
    nullable1 = nullable3;
    Decimal valueOrDefault11 = nullable1.GetValueOrDefault();
    Decimal num7 = num6 + valueOrDefault11;
    local4.BaseAdjustedBalanceDelta = num7;
    ref FullBalanceDelta local5 = ref fullBalanceDelta;
    nullable1 = application.CuryAdjdAmt;
    Decimal valueOrDefault12 = nullable1.GetValueOrDefault();
    nullable1 = application.CuryAdjdDiscAmt;
    Decimal valueOrDefault13 = nullable1.GetValueOrDefault();
    Decimal num8 = valueOrDefault12 + valueOrDefault13;
    nullable1 = application.CuryAdjdThirdAmount;
    Decimal valueOrDefault14 = nullable1.GetValueOrDefault();
    Decimal num9 = num8 + valueOrDefault14;
    local5.CurrencyAdjustedBalanceDelta = num9;
    return fullBalanceDelta;
  }

  /// <summary>
  /// Gets the sign with which the document of a particular
  /// type affects the business account balance.
  /// </summary>
  public static Decimal GetBalanceSign(string documentType, string module)
  {
    switch (module)
    {
      case "AP":
        return APDocType.SignBalance(documentType).GetValueOrDefault();
      case "AR":
        return ARDocType.SignBalance(documentType).GetValueOrDefault();
      default:
        throw new PXException();
    }
  }

  /// <summary>
  /// Get the sign with which the adjusting document affects the
  /// business account balance.
  /// </summary>
  public static Decimal GetSignAffectingBusinessAccountBalanceAdjusting(
    this IDocumentAdjustment application)
  {
    return BalanceCalculation.GetBalanceSign(application.AdjgDocType, application.Module);
  }

  /// <summary>
  /// Gets the sign with which the adjusted document affects the
  /// business account balance.
  /// </summary>
  public static Decimal GetSignAffectingBusinessAccountBalanceAdjusted(
    this IDocumentAdjustment application)
  {
    return BalanceCalculation.GetBalanceSign(application.AdjdDocType, application.Module);
  }

  /// <summary>
  /// Gets the sign correction necessary to correctly calculate balances
  /// of the adjusting document inside the <see cref="M:PX.Objects.Common.BalanceCalculation.AdjustBalance``2(``0,``1,System.Decimal)" /> method.
  /// </summary>
  public static Decimal GetDocumentBalanceSignCorrection(this IDocumentAdjustment application)
  {
    return application.AdjgDocType == "REF" || application.AdjgDocType == "VRF" ? -1M : 1M;
  }

  public static void AdjustBalance<TDocument, TAdjustment>(
    this TDocument document,
    TAdjustment application)
    where TDocument : IBalance, IRegister
    where TAdjustment : IAdjustmentAmount, IDocumentAdjustment
  {
    document.AdjustBalance<TDocument, TAdjustment>(application, 1M);
  }

  public static void AdjustBalance<TDocument, TAdjustment>(
    this TDocument document,
    TAdjustment application,
    Decimal sign)
    where TDocument : IBalance, IRegister
    where TAdjustment : IAdjustmentAmount, IDocumentAdjustment
  {
    if ((object) document == null)
      throw new ArgumentNullException(nameof (document));
    if ((object) application == null)
      throw new ArgumentNullException(nameof (application));
    FullBalanceDelta fullBalanceDelta = application.IsApplicationFor((IDocumentKey) document) ? application.GetFullBalanceDelta() : throw new PXException("The specified application does not correspond to the document");
    if (application.IsIncomingApplicationFor((IDocumentKey) document))
    {
      ref TDocument local1 = ref document;
      ref TDocument local2 = ref local1;
      TDocument document1;
      if ((object) default (TDocument) == null)
      {
        document1 = local2;
        local2 = ref document1;
      }
      Decimal? docBal = local1.DocBal;
      Decimal num1 = fullBalanceDelta.BaseAdjustedBalanceDelta * sign;
      Decimal? nullable1 = docBal.HasValue ? new Decimal?(docBal.GetValueOrDefault() - num1) : new Decimal?();
      local2.DocBal = nullable1;
      ref TDocument local3 = ref document;
      ref TDocument local4 = ref local3;
      document1 = default (TDocument);
      if ((object) document1 == null)
      {
        document1 = local4;
        local4 = ref document1;
      }
      Decimal? curyDocBal = local3.CuryDocBal;
      Decimal num2 = fullBalanceDelta.CurrencyAdjustedBalanceDelta * sign;
      Decimal? nullable2 = curyDocBal.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() - num2) : new Decimal?();
      local4.CuryDocBal = nullable2;
    }
    else
    {
      if (!application.IsOutgoingApplicationFor((IDocumentKey) document))
        return;
      Decimal num3 = application.GetSignAffectingBusinessAccountBalanceAdjusted() * application.GetDocumentBalanceSignCorrection();
      ref TDocument local5 = ref document;
      ref TDocument local6 = ref local5;
      TDocument document2;
      if ((object) default (TDocument) == null)
      {
        document2 = local6;
        local6 = ref document2;
      }
      Decimal? nullable3 = local5.DocBal;
      Decimal num4 = num3 * fullBalanceDelta.BaseAdjustingBalanceDelta * sign;
      Decimal? nullable4 = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - num4) : new Decimal?();
      local6.DocBal = nullable4;
      ref TDocument local7 = ref document;
      ref TDocument local8 = ref local7;
      document2 = default (TDocument);
      if ((object) document2 == null)
      {
        document2 = local8;
        local8 = ref document2;
      }
      nullable3 = local7.CuryDocBal;
      Decimal num5 = num3 * fullBalanceDelta.CurrencyAdjustingBalanceDelta * sign;
      Decimal? nullable5 = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - num5) : new Decimal?();
      local8.CuryDocBal = nullable5;
    }
  }

  public static bool HasBalance(this IBalance document)
  {
    return document.DocBal.IsNonZero() && document.CuryDocBal.IsNonZero();
  }

  /// <summary>
  /// Forces the document's control total amount to be equal to the
  /// document's outstanding balance, afterwards updating the
  /// record in the relevant cache.
  /// </summary>
  public static void ForceDocumentControlTotals(PXGraph graph, IInvoice invoice)
  {
    Decimal? curyOrigDocAmt = invoice.CuryOrigDocAmt;
    Decimal? curyDocBal = invoice.CuryDocBal;
    if (curyOrigDocAmt.GetValueOrDefault() == curyDocBal.GetValueOrDefault() & curyOrigDocAmt.HasValue == curyDocBal.HasValue)
      return;
    invoice.CuryOrigDocAmt = new Decimal?(invoice.CuryDocBal.GetValueOrDefault());
    graph.Caches[invoice.GetType()].Update((object) invoice);
  }
}
