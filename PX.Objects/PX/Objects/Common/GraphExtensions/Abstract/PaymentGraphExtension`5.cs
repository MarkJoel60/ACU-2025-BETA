// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.PaymentGraphExtension`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract;

public abstract class PaymentGraphExtension<TGraph, TPayment, TAdjust, TInvoice, TTran> : 
  PXGraphExtension<TGraph>,
  IDocumentWithFinDetailsGraphExtension
  where TGraph : PXGraph
  where TPayment : class, IBqlTable, IInvoice, new()
  where TAdjust : class, IBqlTable, IFinAdjust, new()
  where TInvoice : class, IBqlTable, IInvoice, new()
  where TTran : class, IBqlTable, IDocumentTran, new()
{
  /// <summary>A mapping-based view of the <see cref="T:PX.Objects.Common.GraphExtensions.Abstract.DAC.Payment" /> data.</summary>
  public PXSelectExtension<Payment> Documents;
  private AbstractPaymentBalanceCalculator<TAdjust, TTran> _balanceCalculator;

  protected abstract PaymentMapping GetPaymentMapping();

  public abstract PXSelectBase<TAdjust> Adjustments { get; }

  protected abstract AbstractPaymentBalanceCalculator<TAdjust, TTran> GetAbstractBalanceCalculator();

  private AbstractPaymentBalanceCalculator<TAdjust, TTran> BalanceClaculator
  {
    get
    {
      return this._balanceCalculator ?? (this._balanceCalculator = this.GetAbstractBalanceCalculator());
    }
  }

  protected abstract bool InternalCall { get; }

  protected IPXCurrencyHelper curyHelper => this.BalanceClaculator.curyHelper;

  protected virtual bool DiscOnDiscDate => false;

  public List<int?> GetOrganizationIDsInDetails()
  {
    return ((IEnumerable<PXResult<TAdjust>>) this.Adjustments.Select(Array.Empty<object>())).AsEnumerable<PXResult<TAdjust>>().SelectMany<PXResult<TAdjust>, int?>((Func<PXResult<TAdjust>, IEnumerable<int?>>) (row => this.GetAdjustBranchIDs(PXResult<TAdjust>.op_Implicit(row)))).Select<int?, int?>(new Func<int?, int?>(PXAccess.GetParentOrganizationID)).Distinct<int?>().ToList<int?>();
  }

  protected virtual IEnumerable<int?> GetAdjustBranchIDs(TAdjust adjust)
  {
    yield return adjust.AdjdBranchID;
    yield return adjust.AdjgBranchID;
  }

  protected virtual void _(Events.RowUpdated<Payment> e)
  {
    if (((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<Payment>>) e).Cache.ObjectsEqual<Payment.adjDate, Payment.adjTranPeriodID, Payment.curyID, Payment.branchID>((object) e.Row, (object) e.OldRow))
      return;
    foreach (PXResult<TAdjust> pxResult in this.Adjustments.Select(Array.Empty<object>()))
    {
      TAdjust row = PXResult<TAdjust>.op_Implicit(pxResult);
      if (!((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<Payment>>) e).Cache.ObjectsEqual<Payment.branchID>((object) e.Row, (object) e.OldRow))
        ((PXSelectBase) this.Adjustments).Cache.SetDefaultExt<Adjust.adjgBranchID>((object) row);
      if (!((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<Payment>>) e).Cache.ObjectsEqual<Payment.adjTranPeriodID>((object) e.Row, (object) e.OldRow))
        FinPeriodIDAttribute.DefaultPeriods<Adjust.adjgFinPeriodID>(((PXSelectBase) this.Adjustments).Cache, (object) row);
      if (((PXSelectBase) this.Adjustments).Cache is PXModelExtension<Adjust> cache)
        cache.UpdateExtensionMapping((object) row);
      GraphHelper.MarkUpdated(((PXSelectBase) this.Adjustments).Cache, (object) row);
    }
  }

  public virtual void CalcBalances<T>(
    TAdjust adj,
    T voucher,
    bool isCalcRGOL,
    bool DiscOnDiscDate,
    TTran tran)
    where T : class, IInvoice, IBqlTable, new()
  {
    this.BalanceClaculator.CalcBalances<T>(adj, voucher, isCalcRGOL, DiscOnDiscDate, tran);
  }

  public abstract void CalcBalancesFromAdjustedDocument(
    TAdjust adj,
    bool isCalcRGOL,
    bool DiscOnDiscDate);

  protected virtual void _(Events.FieldUpdating<TAdjust, Adjust.curyDocBal> e)
  {
    ((Events.FieldUpdatingBase<Events.FieldUpdating<TAdjust, Adjust.curyDocBal>>) e).Cancel = true;
    if (this.InternalCall || (object) e.Row == null)
      return;
    if (e.Row.AdjdCuryInfoID.HasValue && !e.Row.CuryDocBal.HasValue && ((Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TAdjust, Adjust.curyDocBal>>) e).Cache.GetStatus((object) e.Row) != 3)
      this.CalcBalancesFromAdjustedDocument(e.Row, false, this.DiscOnDiscDate);
    ((Events.FieldUpdatingBase<Events.FieldUpdating<TAdjust, Adjust.curyDocBal>>) e).NewValue = (object) e.Row.CuryDocBal;
  }

  protected virtual void _(
    Events.FieldUpdating<TAdjust, Adjust.curyDiscBal> e)
  {
    ((Events.FieldUpdatingBase<Events.FieldUpdating<TAdjust, Adjust.curyDiscBal>>) e).Cancel = true;
    if (this.InternalCall || (object) e.Row == null)
      return;
    if (e.Row.AdjdCuryInfoID.HasValue && !e.Row.CuryDiscBal.HasValue && ((Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TAdjust, Adjust.curyDiscBal>>) e).Cache.GetStatus((object) e.Row) != 3)
      this.CalcBalancesFromAdjustedDocument(e.Row, false, this.DiscOnDiscDate);
    ((Events.FieldUpdatingBase<Events.FieldUpdating<TAdjust, Adjust.curyDiscBal>>) e).NewValue = (object) e.Row.CuryDiscBal;
  }

  protected virtual void _(Events.RowSelecting<TAdjust> e)
  {
    if (this.InternalCall || (object) e.Row == null)
      return;
    // ISSUE: variable of a boxed type
    __Boxed<TAdjust> row = (object) e.Row;
    if ((row != null ? (row.AdjdCuryInfoID.HasValue ? 1 : 0) : 0) == 0)
      return;
    bool flag = false;
    if (e.Row.AdjdHasPPDTaxes.GetValueOrDefault())
    {
      flag = true;
      e.Row.AdjdHasPPDTaxes = new bool?(false);
    }
    try
    {
      this.CalcBalancesFromAdjustedDocument(e.Row, false, this.DiscOnDiscDate);
    }
    finally
    {
      if (flag)
        e.Row.AdjdHasPPDTaxes = new bool?(true);
    }
  }

  protected virtual void _(
    Events.FieldUpdated<TAdjust, Adjust.adjdCuryRate> e)
  {
    TAdjust row = e.Row;
    if (row.VoidAppl.GetValueOrDefault() || row.Voided.GetValueOrDefault())
      return;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = this.curyHelper.GetCurrencyInfo(row.AdjgCuryInfoID);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = this.curyHelper.GetCurrencyInfo(row.AdjdCuryInfoID);
    Decimal? nullable1 = row.CuryAdjgAmt;
    Decimal num1 = nullable1.Value;
    nullable1 = row.CuryAdjgDiscAmt;
    Decimal num2 = nullable1.Value;
    Decimal? nullable2;
    if (string.Equals(currencyInfo1.CuryID, currencyInfo2.CuryID))
    {
      nullable2 = row.AdjdCuryRate;
      Decimal num3 = 1M;
      if (!(nullable2.GetValueOrDefault() == num3 & nullable2.HasValue))
      {
        row.AdjdCuryRate = new Decimal?(1M);
        goto label_17;
      }
    }
    if (string.Equals(currencyInfo2.CuryID, currencyInfo2.BaseCuryID))
    {
      // ISSUE: variable of a boxed type
      __Boxed<TAdjust> local = (object) row;
      Decimal? nullable3;
      if (!(currencyInfo1.CuryMultDiv == "M"))
      {
        nullable3 = currencyInfo1.CuryRate;
      }
      else
      {
        Decimal num4 = (Decimal) 1;
        nullable2 = currencyInfo1.CuryRate;
        nullable3 = nullable2.HasValue ? new Decimal?(num4 / nullable2.GetValueOrDefault()) : new Decimal?();
      }
      local.AdjdCuryRate = nullable3;
    }
    else
    {
      currencyInfo2.CuryRate = row.AdjdCuryRate;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = currencyInfo2;
      nullable2 = row.AdjdCuryRate;
      Decimal? nullable4 = new Decimal?(Math.Round(1M / nullable2.Value, 8, MidpointRounding.AwayFromZero));
      currencyInfo3.RecipRate = nullable4;
      currencyInfo2.CuryMultDiv = "M";
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo4 = currencyInfo2;
      nullable2 = row.CuryAdjdAmt;
      Decimal curyval1 = nullable2.Value;
      num1 = currencyInfo4.CuryConvBase(curyval1);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo5 = currencyInfo2;
      nullable2 = row.CuryAdjdDiscAmt;
      Decimal curyval2 = nullable2.Value;
      num2 = currencyInfo5.CuryConvBase(curyval2);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo6 = currencyInfo2;
      nullable2 = row.CuryAdjdAmt;
      Decimal? curyAdjdDiscAmt = row.CuryAdjdDiscAmt;
      Decimal curyval3 = (nullable2.HasValue & curyAdjdDiscAmt.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + curyAdjdDiscAmt.GetValueOrDefault()) : new Decimal?()).Value;
      Decimal num5 = currencyInfo6.CuryConvBase(curyval3);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo7 = currencyInfo2;
      nullable2 = row.AdjdCuryRate;
      Decimal num6 = nullable2.Value;
      Decimal num7;
      if (!(currencyInfo1.CuryMultDiv == "M"))
      {
        nullable2 = currencyInfo1.CuryRate;
        num7 = 1M / nullable2.Value;
      }
      else
      {
        nullable2 = currencyInfo1.CuryRate;
        num7 = nullable2.Value;
      }
      Decimal? nullable5 = new Decimal?(Math.Round(num6 * num7, 8, MidpointRounding.AwayFromZero));
      currencyInfo7.CuryRate = nullable5;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo8 = currencyInfo2;
      Decimal num8;
      if (!(currencyInfo1.CuryMultDiv == "M"))
      {
        nullable2 = currencyInfo1.CuryRate;
        num8 = nullable2.Value;
      }
      else
      {
        nullable2 = currencyInfo1.CuryRate;
        num8 = 1M / nullable2.Value;
      }
      nullable2 = row.AdjdCuryRate;
      Decimal num9 = nullable2.Value;
      Decimal? nullable6 = new Decimal?(Math.Round(num8 / num9, 8, MidpointRounding.AwayFromZero));
      currencyInfo8.RecipRate = nullable6;
      if (num1 + num2 != num5)
        num2 += num5 - num2 - num1;
    }
label_17:
    GraphHelper.MarkUpdated(this.Base.Caches[typeof (PX.Objects.CM.Extensions.CurrencyInfo)], (object) currencyInfo2);
    Decimal num10 = num1;
    nullable2 = row.CuryAdjgAmt;
    Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
    if (!(num10 == valueOrDefault1 & nullable2.HasValue))
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TAdjust, Adjust.adjdCuryRate>>) e).Cache.SetValue<Adjust.curyAdjgAmt>((object) e.Row, (object) num1);
    Decimal num11 = num2;
    nullable2 = row.CuryAdjgDiscAmt;
    Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
    if (!(num11 == valueOrDefault2 & nullable2.HasValue))
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TAdjust, Adjust.adjdCuryRate>>) e).Cache.SetValue<Adjust.curyAdjgDiscAmt>((object) e.Row, (object) num2);
    this.FillPPDAmts(row);
    this.CalcBalancesFromAdjustedDocument(row, true, true);
  }

  protected void FillPPDAmts(TAdjust adj)
  {
    adj.CuryAdjgPPDAmt = adj.CuryAdjgDiscAmt;
    adj.CuryAdjdPPDAmt = adj.CuryAdjdDiscAmt;
    adj.AdjPPDAmt = adj.AdjDiscAmt;
  }

  protected virtual void _(Events.FieldUpdated<TAdjust, Adjust.voided> e)
  {
    this.CalcBalancesFromAdjustedDocument(e.Row, true, false);
  }

  protected virtual void _(
    Events.FieldUpdated<TAdjust, Adjust.curyAdjgDiscAmt> e)
  {
    if ((object) e.Row == null)
      return;
    this.FillPPDAmts(e.Row);
    this.CalcBalancesFromAdjustedDocument(e.Row, true, this.DiscOnDiscDate);
  }
}
