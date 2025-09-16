// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.AbstractPaymentBalanceCalculator`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using PX.Objects.Extensions.MultiCurrency;
using System;

#nullable disable
namespace PX.Objects.CM.Extensions;

public abstract class AbstractPaymentBalanceCalculator<TAdjust, TTran>
  where TAdjust : class, IBqlTable, IFinAdjust, new()
  where TTran : class, IBqlTable, IDocumentTran, new()
{
  private readonly PaymentBalanceCalculator paymentBalanceCalculator;

  public IPXCurrencyHelper curyHelper { get; }

  public virtual bool DiscOnDiscDate => false;

  protected AbstractPaymentBalanceCalculator(IPXCurrencyHelper curyHelper)
  {
    this.curyHelper = curyHelper;
    this.paymentBalanceCalculator = new PaymentBalanceCalculator(curyHelper);
  }

  protected virtual bool ShouldRgolBeResetInZero(TAdjust adj) => false;

  public void CalcBalances<T>(
    TAdjust adj,
    T originalInvoice,
    bool isCalcRGOL,
    bool DiscOnDiscDate,
    TTran tran)
    where T : class, IInvoice, IBqlTable, new()
  {
    Decimal? nullable1;
    int num1;
    if (adj.CuryAdjgPPDAmt.HasValue)
    {
      nullable1 = adj.CuryAdjgPPDAmt;
      Decimal num2 = 0M;
      if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
      {
        num1 = adj.AdjdHasPPDTaxes.GetValueOrDefault() ? 1 : 0;
        goto label_4;
      }
    }
    num1 = 0;
label_4:
    bool flag1 = num1 != 0;
    if (flag1)
    {
      adj.CuryAdjgDiscAmt = new Decimal?(0M);
      adj.CuryAdjdDiscAmt = new Decimal?(0M);
      adj.AdjDiscAmt = new Decimal?(0M);
    }
    T voucher = this.AjustInvoiceBalanceForAutoApply<T>(adj, originalInvoice);
    this.paymentBalanceCalculator.CalcBalances(adj.AdjgCuryInfoID, adj.AdjdCuryInfoID, (IInvoice) voucher, (IAdjustment) adj, (IDocumentTran) tran);
    this.AfterBalanceCalculatedBeforeBalanceAjusted<T>(adj, voucher, DiscOnDiscDate, tran);
    PaymentBalanceAjuster paymentBalanceAjuster = new PaymentBalanceAjuster(this.curyHelper);
    paymentBalanceAjuster.AdjustBalance((IAdjustment) adj);
    if (flag1)
    {
      nullable1 = adj.AdjPPDAmt;
      if (!nullable1.HasValue && !adj.Released.GetValueOrDefault())
      {
        TAdjust copy = PXCache<TAdjust>.CreateCopy(adj);
        copy.FillDiscAmts();
        paymentBalanceAjuster.AdjustBalance((IAdjustment) copy);
        adj.AdjPPDAmt = copy.AdjDiscAmt;
      }
    }
    bool? voided;
    if (isCalcRGOL)
    {
      if (adj.Voided.HasValue)
      {
        voided = adj.Voided;
        bool flag2 = false;
        if (!(voided.GetValueOrDefault() == flag2 & voided.HasValue))
          goto label_17;
      }
      new PaymentRGOLCalculator(this.curyHelper, (IAdjustment) adj, adj.ReverseGainLoss).Calculate((IInvoice) voucher, (IDocumentTran) tran);
      if (this.ShouldRgolBeResetInZero(adj))
        adj.RGOLAmt = new Decimal?(0M);
      Decimal? curyAdjdDiscAmt = adj.CuryAdjdDiscAmt;
      if (flag1)
      {
        TAdjust copy = PXCache<TAdjust>.CreateCopy(adj);
        copy.FillDiscAmts();
        new PaymentRGOLCalculator(this.curyHelper, (IAdjustment) copy, new bool?(false)).Calculate((IInvoice) voucher, (IDocumentTran) tran);
        curyAdjdDiscAmt = copy.CuryAdjdDiscAmt;
      }
      adj.CuryAdjdPPDAmt = curyAdjdDiscAmt;
    }
label_17:
    if (!flag1)
      return;
    voided = adj.Voided;
    if (voided.GetValueOrDefault())
      return;
    ref TAdjust local1 = ref adj;
    // ISSUE: variable of a boxed type
    __Boxed<TAdjust> local2 = (object) local1;
    nullable1 = local1.CuryDocBal;
    Decimal? curyAdjgPpdAmt = adj.CuryAdjgPPDAmt;
    Decimal? nullable2 = nullable1.HasValue & curyAdjgPpdAmt.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - curyAdjgPpdAmt.GetValueOrDefault()) : new Decimal?();
    local2.CuryDocBal = nullable2;
    ref TAdjust local3 = ref adj;
    // ISSUE: variable of a boxed type
    __Boxed<TAdjust> local4 = (object) local3;
    Decimal? nullable3 = local3.DocBal;
    nullable1 = adj.AdjPPDAmt;
    Decimal? nullable4 = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    local4.DocBal = nullable4;
    ref TAdjust local5 = ref adj;
    // ISSUE: variable of a boxed type
    __Boxed<TAdjust> local6 = (object) local5;
    nullable1 = local5.CuryDiscBal;
    nullable3 = adj.CuryAdjgPPDAmt;
    Decimal? nullable5 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
    local6.CuryDiscBal = nullable5;
    ref TAdjust local7 = ref adj;
    // ISSUE: variable of a boxed type
    __Boxed<TAdjust> local8 = (object) local7;
    nullable3 = local7.DiscBal;
    nullable1 = adj.AdjPPDAmt;
    Decimal? nullable6 = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    local8.DiscBal = nullable6;
  }

  /// <summary>
  /// Behavior by default: adj.AdjdCuryRate = paymentBalanceCalculator.GetAdjdCuryRate(adj);
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="adj"></param>
  /// <param name="voucher"></param>
  /// <param name="DiscOnDiscDate"></param>
  protected virtual void AfterBalanceCalculatedBeforeBalanceAjusted<T>(
    TAdjust adj,
    T voucher,
    bool DiscOnDiscDate,
    TTran tran)
    where T : class, IInvoice, IBqlTable, new()
  {
    adj.AdjdCuryRate = new Decimal?(this.paymentBalanceCalculator.GetAdjdCuryRate((IAdjustment) adj));
  }

  protected virtual T AjustInvoiceBalanceForAutoApply<T>(TAdjust adj, T invoice) where T : class, IInvoice, IBqlTable, new()
  {
    return invoice;
  }
}
