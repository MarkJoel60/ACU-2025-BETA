// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.ARPaymentBalanceCalculator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.Extensions.MultiCurrency;
using System;

#nullable disable
namespace PX.Objects.CM.Extensions;

internal class ARPaymentBalanceCalculator : AbstractPaymentBalanceCalculator<ARAdjust, ARTran>
{
  private readonly ARPaymentEntry Base;

  public ARPaymentBalanceCalculator(ARPaymentEntry graph)
    : base((IPXCurrencyHelper) ((PXGraph) graph).GetExtension<ARPaymentEntry.MultiCurrency>())
  {
    this.Base = graph;
  }

  protected override T AjustInvoiceBalanceForAutoApply<T>(ARAdjust adj, T originalInvoice)
  {
    if (!this.Base.AutoPaymentApp)
      return originalInvoice;
    try
    {
      ARAdjust applicationAggregated = this.GetSisterAdjustmentsJustCreatedByAutoApplicationAggregated(adj);
      if (applicationAggregated == null || applicationAggregated.AdjdRefNbr == null)
        return originalInvoice;
      FullBalanceDelta fullBalanceDelta = applicationAggregated.GetFullBalanceDelta();
      T copy = PXCache<T>.CreateCopy(originalInvoice);
      ref T local1 = ref copy;
      // ISSUE: variable of a boxed type
      __Boxed<T> local2 = (object) local1;
      Decimal? nullable1 = local1.CuryDocBal;
      Decimal adjustedBalanceDelta1 = fullBalanceDelta.CurrencyAdjustedBalanceDelta;
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - adjustedBalanceDelta1) : new Decimal?();
      local2.CuryDocBal = nullable2;
      ref T local3 = ref copy;
      // ISSUE: variable of a boxed type
      __Boxed<T> local4 = (object) local3;
      nullable1 = local3.DocBal;
      Decimal adjustedBalanceDelta2 = fullBalanceDelta.BaseAdjustedBalanceDelta;
      Decimal? nullable3 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - adjustedBalanceDelta2) : new Decimal?();
      local4.DocBal = nullable3;
      ref T local5 = ref copy;
      // ISSUE: variable of a boxed type
      __Boxed<T> local6 = (object) local5;
      nullable1 = local5.CuryDiscBal;
      Decimal? nullable4 = applicationAggregated.CuryAdjdDiscAmt;
      Decimal? nullable5 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
      local6.CuryDiscBal = nullable5;
      ref T local7 = ref copy;
      // ISSUE: variable of a boxed type
      __Boxed<T> local8 = (object) local7;
      nullable4 = local7.DiscBal;
      nullable1 = applicationAggregated.AdjDiscAmt;
      Decimal? nullable6 = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      local8.DiscBal = nullable6;
      ref T local9 = ref copy;
      // ISSUE: variable of a boxed type
      __Boxed<T> local10 = (object) local9;
      nullable1 = local9.CuryWhTaxBal;
      nullable4 = applicationAggregated.CuryAdjdWOAmt;
      Decimal? nullable7 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
      local10.CuryWhTaxBal = nullable7;
      ref T local11 = ref copy;
      // ISSUE: variable of a boxed type
      __Boxed<T> local12 = (object) local11;
      nullable4 = local11.WhTaxBal;
      nullable1 = applicationAggregated.AdjWOAmt;
      Decimal? nullable8 = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      local12.WhTaxBal = nullable8;
      return copy;
    }
    finally
    {
      this.Base.AutoPaymentApp = false;
    }
  }

  private ARAdjust GetSisterAdjustmentsJustCreatedByAutoApplicationAggregated(ARAdjust adj)
  {
    this.Base.internalCall = true;
    try
    {
      return PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXSelectGroupBy<ARAdjust, Where<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<ARAdjust.released, Equal<False>, And<ARAdjust.voided, Equal<False>, And<Where<ARAdjust.adjgDocType, NotEqual<Required<ARAdjust.adjgDocType>>, Or<ARAdjust.adjgRefNbr, NotEqual<Required<ARAdjust.adjgRefNbr>>>>>>>>>, Aggregate<GroupBy<ARAdjust.adjdDocType, GroupBy<ARAdjust.adjdRefNbr, Sum<ARAdjust.curyAdjdAmt, Sum<ARAdjust.adjAmt, Sum<ARAdjust.curyAdjdDiscAmt, Sum<ARAdjust.adjDiscAmt>>>>>>>>.Config>.Select((PXGraph) this.Base, new object[4]
      {
        (object) adj.AdjdDocType,
        (object) adj.AdjdRefNbr,
        (object) adj.AdjgDocType,
        (object) adj.AdjgRefNbr
      }));
    }
    finally
    {
      this.Base.internalCall = false;
    }
  }

  protected override void AfterBalanceCalculatedBeforeBalanceAjusted<T>(
    ARAdjust adj,
    T invoice,
    bool DiscOnDiscDate,
    ARTran tran)
  {
    adj.CuryOrigDocAmt = (Decimal?) tran?.CuryOrigTranAmt ?? invoice.CuryOrigDocAmt;
    adj.OrigDocAmt = (Decimal?) tran?.OrigTranAmt ?? invoice.OrigDocAmt;
    if (DiscOnDiscDate)
      PaymentEntry.CalcDiscount(adj.AdjgDocDate, (IInvoice) invoice, (IAdjustment) adj);
    PaymentEntry.WarnPPDiscount<T, ARAdjust>((PXGraph) this.Base, adj.AdjgDocDate, invoice, adj, adj.CuryAdjgPPDAmt);
    base.AfterBalanceCalculatedBeforeBalanceAjusted<T>(adj, invoice, DiscOnDiscDate, tran);
    Customer customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) adj.AdjdCustomerID
    }));
    if (customer != null && customer.SmallBalanceAllow.GetValueOrDefault() && adj.AdjgDocType != "REF" && adj.AdjdDocType != "CRM")
    {
      Decimal num1 = this.curyHelper.GetCurrencyInfo(adj.AdjgCuryInfoID).CuryConvCury(customer.SmallBalanceLimit.Value);
      Decimal? nullable1 = adj.CuryOrigDocAmt;
      Decimal num2 = 0M;
      int num3 = nullable1.GetValueOrDefault() < num2 & nullable1.HasValue ? -1 : 1;
      adj.CuryWOBal = new Decimal?((Decimal) num3 * num1);
      ARAdjust arAdjust = adj;
      Decimal num4 = (Decimal) num3;
      nullable1 = customer.SmallBalanceLimit;
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num4 * nullable1.GetValueOrDefault()) : new Decimal?();
      arAdjust.WOBal = nullable2;
      invoice.CuryWhTaxBal = new Decimal?(num1);
      invoice.WhTaxBal = customer.SmallBalanceLimit;
    }
    else
    {
      adj.CuryWOBal = new Decimal?(0M);
      adj.WOBal = new Decimal?(0M);
      invoice.CuryWhTaxBal = new Decimal?(0M);
      invoice.WhTaxBal = new Decimal?(0M);
    }
  }
}
