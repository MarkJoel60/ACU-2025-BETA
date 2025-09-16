// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.ARInvoiceBalanceCalculator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.Extensions.MultiCurrency;
using System;

#nullable disable
namespace PX.Objects.CM.Extensions;

public class ARInvoiceBalanceCalculator
{
  private readonly IPXCurrencyHelper curyHelper;
  private readonly PXGraph Graph;

  public ARInvoiceBalanceCalculator(IPXCurrencyHelper curyHelper, PXGraph Graph)
  {
    this.curyHelper = curyHelper;
    this.Graph = Graph;
  }

  /// <summary>
  /// The base method to calculate application
  /// balances in Invoice currency. Both invoice
  /// and payment documents should be set.
  /// </summary>
  public void CalcBalancesFromInvoiceSide(
    ARAdjust adj,
    IInvoice invoice,
    ARPayment payment,
    bool isCalcRGOL,
    bool DiscOnDiscDate,
    ARAdjust others = null)
  {
    if (invoice == null)
      return;
    this.InitBalancesFromInvoiceSide(adj, invoice, payment, others);
    if (DiscOnDiscDate)
      PaymentEntry.CalcDiscount(adj.AdjgDocDate, invoice, (IAdjustment) adj);
    new PaymentBalanceAjuster(this.curyHelper).AdjustBalance((IAdjustment) adj, adj.AdjdCuryInfoID, adj.CuryAdjdAmt, adj.CuryAdjdDiscAmt, adj.CuryAdjdWOAmt, false);
    if (isCalcRGOL && !adj.Voided.GetValueOrDefault())
      this.CalcRGOLFromInvoiceSide<ARPayment, ARAdjust>(payment, adj);
    ARAdjust arAdjust = adj;
    Decimal? curyAdjdAmt = adj.CuryAdjdAmt;
    Decimal num = 0M;
    bool? nullable = new bool?(!(curyAdjdAmt.GetValueOrDefault() == num & curyAdjdAmt.HasValue));
    arAdjust.Selected = nullable;
  }

  /// <summary>
  /// The method to initialize application
  /// balances in Invoice currency.
  /// </summary>
  public void InitBalancesFromInvoiceSide(
    ARAdjust adj,
    IInvoice invoice,
    ARPayment payment,
    ARAdjust others = null)
  {
    PaymentBalanceCalculator balanceCalculator = new PaymentBalanceCalculator(this.curyHelper);
    long? adjdCuryInfoId = adj.AdjdCuryInfoID;
    long? adjgCuryInfoId = adj.AdjgCuryInfoID;
    long? curyInfoId = payment.CuryInfoID;
    Decimal? nullable1 = payment.Released.GetValueOrDefault() ? payment.CuryDocBal : payment.CuryOrigDocAmt;
    Decimal? nullable2 = (Decimal?) others?.CuryAdjgAmt;
    Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
    Decimal? fromCuryDocBal;
    if (!nullable1.HasValue)
    {
      nullable2 = new Decimal?();
      fromCuryDocBal = nullable2;
    }
    else
      fromCuryDocBal = new Decimal?(nullable1.GetValueOrDefault() - valueOrDefault1);
    bool? nullable3 = payment.Released;
    nullable1 = nullable3.GetValueOrDefault() ? payment.DocBal : payment.OrigDocAmt;
    Decimal? nullable4;
    if (others == null)
    {
      nullable2 = new Decimal?();
      nullable4 = nullable2;
    }
    else
      nullable4 = others.AdjAmt;
    nullable2 = nullable4;
    Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
    Decimal? fromDocBal;
    if (!nullable1.HasValue)
    {
      nullable2 = new Decimal?();
      fromDocBal = nullable2;
    }
    else
      fromDocBal = new Decimal?(nullable1.GetValueOrDefault() - valueOrDefault2);
    CalculatedBalance calculatedBalance = balanceCalculator.CalcBalance(adjdCuryInfoId, adjgCuryInfoId, curyInfoId, fromCuryDocBal, fromDocBal);
    adj.CuryDocBal = new Decimal?(calculatedBalance.CuryBalance);
    adj.DocBal = new Decimal?(calculatedBalance.Balance);
    adj.CuryDiscBal = invoice.CuryDiscBal;
    adj.DiscBal = invoice.DiscBal;
    adj.CuryWhTaxBal = new Decimal?(0M);
    adj.WhTaxBal = new Decimal?(0M);
    invoice.CuryWhTaxBal = new Decimal?(0M);
    invoice.WhTaxBal = new Decimal?(0M);
    if (!(adj.AdjgDocType != "REF") || !(adj.AdjdDocType != "CRM"))
      return;
    Customer customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select(this.Graph, new object[1]
    {
      (object) adj.AdjdCustomerID
    }));
    if (customer == null)
      return;
    nullable3 = customer.SmallBalanceAllow;
    if (!nullable3.GetValueOrDefault())
      return;
    CurrencyInfo currencyInfo = this.curyHelper.GetCurrencyInfo(adj.AdjdCuryInfoID);
    nullable1 = customer.SmallBalanceLimit;
    Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
    Decimal num = currencyInfo.CuryConvCury(valueOrDefault3);
    adj.CuryWhTaxBal = new Decimal?(num);
    adj.WhTaxBal = customer.SmallBalanceLimit;
    invoice.CuryWhTaxBal = new Decimal?(num);
    invoice.WhTaxBal = customer.SmallBalanceLimit;
  }

  /// <summary>
  /// The method to calculate application RGOL
  /// from the Invoice document side.
  /// </summary>
  public void CalcRGOLFromInvoiceSide<TInvoice, TAdjustment>(TInvoice document, TAdjustment adj)
    where TInvoice : IInvoice
    where TAdjustment : class, IBqlTable, IAdjustment
  {
    if (!adj.CuryAdjdAmt.HasValue || !adj.CuryAdjdDiscAmt.HasValue || !adj.CuryAdjdWhTaxAmt.HasValue)
      return;
    CurrencyInfo currencyInfo1 = this.curyHelper.GetCurrencyInfo(adj.AdjdCuryInfoID);
    CurrencyInfo currencyInfo2 = this.curyHelper.GetCurrencyInfo(adj.AdjgCuryInfoID);
    CurrencyInfo currencyInfo3 = this.curyHelper.GetCurrencyInfo(document.CuryInfoID);
    CurrencyInfo to_info = currencyInfo2;
    CurrencyInfo to_originfo = currencyInfo3;
    RGOLCalculator rgolCalculator = new RGOLCalculator(currencyInfo1, to_info, to_originfo);
    RGOLCalculationResult calculationResult1 = rgolCalculator.CalcRGOL(adj.CuryAdjdDiscAmt, adj.AdjDiscAmt);
    adj.CuryAdjgDiscAmt = calculationResult1.ToCuryAdjAmt;
    RGOLCalculationResult calculationResult2 = rgolCalculator.CalcRGOL(adj.CuryAdjdWhTaxAmt, adj.AdjWhTaxAmt);
    adj.CuryAdjgWhTaxAmt = calculationResult2.ToCuryAdjAmt;
    RGOLCalculationResult calculationResult3 = rgolCalculator.CalcRGOL(adj.CuryAdjdAmt, adj.AdjAmt);
    adj.CuryAdjgAmt = calculationResult3.ToCuryAdjAmt;
    // ISSUE: variable of a boxed type
    __Boxed<TAdjustment> local1 = (object) adj;
    Decimal? rgolAmt1 = calculationResult1.RgolAmt;
    Decimal? nullable1 = calculationResult2.RgolAmt;
    Decimal? nullable2 = rgolAmt1.HasValue & nullable1.HasValue ? new Decimal?(rgolAmt1.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? rgolAmt2 = calculationResult3.RgolAmt;
    Decimal? nullable3;
    if (!(nullable2.HasValue & rgolAmt2.HasValue))
    {
      nullable1 = new Decimal?();
      nullable3 = nullable1;
    }
    else
      nullable3 = new Decimal?(nullable2.GetValueOrDefault() + rgolAmt2.GetValueOrDefault());
    local1.RGOLAmt = nullable3;
    // ISSUE: variable of a boxed type
    __Boxed<TAdjustment> local2 = (object) adj;
    Decimal? nullable4;
    if (!adj.ReverseGainLoss.GetValueOrDefault())
    {
      nullable4 = adj.RGOLAmt;
    }
    else
    {
      Decimal num = -1M;
      rgolAmt2 = adj.RGOLAmt;
      nullable4 = rgolAmt2.HasValue ? new Decimal?(num * rgolAmt2.GetValueOrDefault()) : new Decimal?();
    }
    local2.RGOLAmt = nullable4;
  }
}
