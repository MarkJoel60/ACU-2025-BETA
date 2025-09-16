// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CM.TemporaryHelpers;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PO;
using PX.Objects.SO;
using PX.Objects.TX;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// Specialized for AP version of the TaxAttribute. <br />
/// Provides Tax calculation for APTran, by default is attached to APTran (details) and APInvoice (header) <br />
/// Normally, should be placed on the TaxCategoryID field. <br />
/// Internally, it uses APInvoiceEntry graphs, otherwise taxes are not calculated (tax calc Level is set to NoCalc).<br />
/// As a result of this attribute work a set of APTax tran related to each AP Tran  and to their parent will created <br />
/// May be combined with other attrbibutes with similar type - for example, APTaxAttribute <br />
/// <example>
/// [APTax(typeof(APRegister), typeof(APTax), typeof(APTaxTran))]
/// </example>
/// </summary>
public class APTaxAttribute : TaxAttribute
{
  protected 
  #nullable disable
  System.Type CuryRetainageAmt = typeof (APTaxAttribute.curyRetainageAmt);
  protected System.Type PaymentsByLinesAllowed = typeof (APTaxAttribute.paymentsByLinesAllowed);

  protected virtual short SortOrder => 0;

  protected string _CuryRetainageAmt => this.CuryRetainageAmt.Name;

  protected string _PaymentsByLinesAllowed => this.PaymentsByLinesAllowed.Name;

  protected override bool CalcGrossOnDocumentLevel
  {
    get => true;
    set => base.CalcGrossOnDocumentLevel = value;
  }

  protected override bool AskRecalculationOnCalcModeChange
  {
    get => true;
    set => base.AskRecalculationOnCalcModeChange = value;
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.Graph.FieldUpdated.AddHandler(this._ChildType, this._TaxCategoryID, new PXFieldUpdated(this.TaxCategoryUpdated));
  }

  protected override List<object> SelectDocumentLines(PXGraph graph, object row)
  {
    return PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Current<APRegister.docType>>, And<APTran.refNbr, Equal<Current<APRegister.refNbr>>>>>.Config>.SelectMultiBound(graph, new object[1]
    {
      row
    }).RowCast<APTran>().Select<APTran, object>((Func<APTran, object>) (_ => (object) _)).ToList<object>();
  }

  /// <summary>
  /// <param name="ParentType">Type of parent(main) document. Must Be IBqlTable </param>
  /// <param name="TaxType">Type of the TaxTran records for the row(details). Must be IBqlTable</param>
  /// <param name="TaxSumType">Type of the TaxTran recorde for the main documect (summary). Must be iBqlTable</param>
  /// </summary>
  public APTaxAttribute(
    System.Type ParentType,
    System.Type TaxType,
    System.Type TaxSumType,
    System.Type CalcMode = null,
    System.Type parentBranchIDField = null)
    : base(ParentType, TaxType, TaxSumType, CalcMode, parentBranchIDField)
  {
    this.Init();
  }

  private void Init()
  {
    this.CuryTranAmt = typeof (APTran.curyTranAmt);
    this.GroupDiscountRate = typeof (APTran.groupDiscountRate);
    this.CuryLineTotal = typeof (APInvoice.curyLineTotal);
    this.CuryDiscTot = typeof (APInvoice.curyDiscTot);
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<APTran.lineType, NotEqual<SOLineType.discount>>, APTran.curyTranAmt>, decimal0>), typeof (SumCalc<APInvoice.curyLineTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<APTran.tranType, In3<APDocType.prepaymentInvoice, APDocType.prepayment>, And<APTran.lineType, NotIn3<SOLineType.discount, SOLineType.freight>>>, Mult<APTran.curyDiscAmt, Div<APTran.prepaymentPct, decimal100>>, Case<Where<APTran.lineType, NotIn3<SOLineType.discount, SOLineType.freight>>, APTran.curyDiscAmt>>, decimal0>), typeof (SumCalc<APInvoice.curyLineDiscTotal>)));
  }

  public override int CompareTo(object other)
  {
    return this.SortOrder.CompareTo(((APTaxAttribute) other).SortOrder);
  }

  public override object Insert(PXCache cache, object item)
  {
    List<object> recordsList = new List<object>((IEnumerable<object>) PXSelectBase<APTax, PXSelect<APTax, Where<APTax.tranType, Equal<Current<APRegister.docType>>, And<APTax.refNbr, Equal<Current<APRegister.refNbr>>>>>.Config>.Select(cache.Graph).RowCast<APTax>());
    PXRowInserted handler = (PXRowInserted) ((sender, e) =>
    {
      recordsList.Add(e.Row);
      PXCache cach = cache.Graph.Caches[typeof (APRegister)];
      object obj1 = cach.GetValue<APRegister.docType>(cach.Current);
      object obj2 = cach.GetValue<APRegister.refNbr>(cach.Current);
      PXSelectBase<APTax, PXSelect<APTax, Where<APTax.tranType, Equal<Current<APRegister.docType>>, And<APTax.refNbr, Equal<Current<APRegister.refNbr>>>>>.Config>.StoreCached(cache.Graph, new PXCommandKey(new object[2]
      {
        obj1,
        obj2
      }), recordsList);
    });
    cache.Graph.RowInserted.AddHandler<APTax>(handler);
    try
    {
      return base.Insert(cache, item);
    }
    finally
    {
      cache.Graph.RowInserted.RemoveHandler<APTax>(handler);
    }
  }

  public override object Update(PXCache cache, object item)
  {
    List<object> recordsList = new List<object>((IEnumerable<object>) PXSelectBase<APTax, PXSelect<APTax, Where<APTax.tranType, Equal<Current<APRegister.docType>>, And<APTax.refNbr, Equal<Current<APRegister.refNbr>>>>>.Config>.Select(cache.Graph).RowCast<APTax>());
    PXRowUpdated handler = (PXRowUpdated) ((sender, e) =>
    {
      IEqualityComparer<object> comparer = sender.GetComparer();
      int index = recordsList.FindIndex((Predicate<object>) (t => comparer.Equals(t, e.OldRow)));
      if (index >= 0)
        recordsList[index] = e.Row;
      PXCache cach = cache.Graph.Caches[typeof (APRegister)];
      object obj1 = cach.GetValue<APRegister.docType>(cach.Current);
      object obj2 = cach.GetValue<APRegister.refNbr>(cach.Current);
      PXSelectBase<APTax, PXSelect<APTax, Where<APTax.tranType, Equal<Current<APRegister.docType>>, And<APTax.refNbr, Equal<Current<APRegister.refNbr>>>>>.Config>.StoreCached(cache.Graph, new PXCommandKey(new object[2]
      {
        obj1,
        obj2
      }), recordsList);
    });
    cache.Graph.RowUpdated.AddHandler<APTax>(handler);
    try
    {
      return base.Update(cache, item);
    }
    finally
    {
      cache.Graph.RowUpdated.RemoveHandler<APTax>(handler);
    }
  }

  public override object Delete(PXCache cache, object item)
  {
    List<object> recordsList = new List<object>((IEnumerable<object>) PXSelectBase<APTax, PXSelect<APTax, Where<APTax.tranType, Equal<Current<APRegister.docType>>, And<APTax.refNbr, Equal<Current<APRegister.refNbr>>>>>.Config>.Select(cache.Graph).RowCast<APTax>());
    PXRowDeleted handler = (PXRowDeleted) ((sender, e) =>
    {
      recordsList.Remove(e.Row);
      PXCache cach = cache.Graph.Caches[typeof (APRegister)];
      object obj1 = cach.GetValue<APRegister.docType>(cach.Current);
      object obj2 = cach.GetValue<APRegister.refNbr>(cach.Current);
      PXSelectBase<APTax, PXSelect<APTax, Where<APTax.tranType, Equal<Current<APRegister.docType>>, And<APTax.refNbr, Equal<Current<APRegister.refNbr>>>>>.Config>.StoreCached(cache.Graph, new PXCommandKey(new object[2]
      {
        obj1,
        obj2
      }), recordsList);
    });
    cache.Graph.RowDeleted.AddHandler<APTax>(handler);
    try
    {
      return base.Delete(cache, item);
    }
    finally
    {
      cache.Graph.RowDeleted.RemoveHandler<APTax>(handler);
    }
  }

  protected override object SelectParent(PXCache cache, object row)
  {
    if (this._TaxCalc != TaxCalc.ManualCalc)
      return base.SelectParent(cache, row);
    object detrow = PXParentAttribute.LocateParent(cache, row, this._ChildType);
    return this.FilterParent(cache, detrow) ? (object) null : detrow;
  }

  protected override List<object> ChildSelect(PXCache cache, object data)
  {
    return this._TaxCalc == TaxCalc.ManualCalc ? base.ChildSelect(cache, data).Where<object>((Func<object, bool>) (c => !this.FilterParent(cache, c))).ToList<object>() : base.ChildSelect(cache, data);
  }

  protected virtual bool FilterParent(PXCache cache, object detrow)
  {
    if (detrow == null || cache.Graph.Caches[this._ChildType].GetStatus(detrow) == PXEntryStatus.Notchanged)
      return true;
    if (!(this._ChildType == typeof (APTran)))
      return false;
    APTran apTran = (APTran) detrow;
    POOrderReceiptLink current = (POOrderReceiptLink) cache.Graph.Caches[typeof (POOrderReceiptLink)]?.Current;
    if (current == null)
      return false;
    if (current.POType != apTran.POOrderType || current.PONbr != apTran.PONbr)
      return true;
    return (current.ReceiptType != apTran.ReceiptType || current.ReceiptNbr != apTran.ReceiptNbr) && apTran.POAccrualType == "R";
  }

  protected override void Tax_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (this._TaxCalc != TaxCalc.NoCalc && e.ExternalCall || this._TaxCalc == TaxCalc.ManualCalc)
      base.Tax_RowDeleted(sender, e);
    if (this._TaxCalc != TaxCalc.ManualCalc)
      return;
    PXCache cach1 = sender.Graph.Caches[this._ChildType];
    PXCache cach2 = sender.Graph.Caches[this._TaxType];
    this.SelectTaxes(sender, (object) null, PXTaxCheck.RecalcLine);
  }

  protected override void Tax_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (this._IncludeDirectTaxLine)
    {
      APTaxTran row = e.Row as APTaxTran;
      PX.Objects.TX.Tax tax = PX.Objects.TX.Tax.PK.Find(sender.Graph, row?.TaxID);
      if ((tax != null ? (tax.DirectTax.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        PXUIFieldAttribute.SetEnabled<APTaxTran.curyRetainedTaxableAmt>(sender, (object) row, false);
        PXUIFieldAttribute.SetEnabled<APTaxTran.curyRetainedTaxAmt>(sender, (object) row, false);
      }
    }
    base.Tax_RowSelected(sender, e);
  }

  protected override Decimal? GetCuryTranAmt(PXCache sender, object row, string TaxCalcType)
  {
    Decimal num1 = this.IsRetainedTaxes(sender.Graph) ? 0M : (Decimal) (sender.GetValue(row, this._CuryRetainageAmt) ?? (object) 0M);
    Decimal num2 = base.GetCuryTranAmt(sender, row).GetValueOrDefault() + num1;
    Decimal? nullable1 = (Decimal?) sender.GetValue(row, this._GroupDiscountRate);
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num2 * nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable3 = (Decimal?) sender.GetValue(row, this._DocumentDiscountRate);
    Decimal? nullable4;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable1 = new Decimal?();
      nullable4 = nullable1;
    }
    else
      nullable4 = new Decimal?(nullable2.GetValueOrDefault() * nullable3.GetValueOrDefault());
    Decimal? nullable5 = nullable4;
    return new Decimal?(MultiCurrencyCalculator.RoundCury(sender, row, nullable5.Value));
  }

  protected override void DateUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is APRegister row) || (!row.RetainageApply.GetValueOrDefault() ? 0 : (row.DocType == "ADR" ? 1 : 0)) != 0 || row.IsChildRetainageDocument())
      return;
    base.DateUpdated(sender, e);
  }

  protected override void SetTaxableAmt(PXCache sender, object row, Decimal? value)
  {
    sender.SetValue<APTran.curyTaxableAmt>(row, (object) value);
  }

  protected override void SetTaxAmt(PXCache sender, object row, Decimal? value)
  {
    sender.SetValue<APTran.curyTaxAmt>(row, (object) value);
  }

  protected override Decimal? GetTaxableAmt(PXCache sender, object row)
  {
    return (Decimal?) sender.GetValue<APTran.curyTaxableAmt>(row);
  }

  protected override Decimal? GetTaxAmt(PXCache sender, object row)
  {
    return (Decimal?) sender.GetValue<APTran.curyTaxAmt>(row);
  }

  protected override Decimal? GetDocLineFinalAmtNoRounding(
    PXCache sender,
    object row,
    string TaxCalcType)
  {
    Decimal num1 = this.IsRetainedTaxes(sender.Graph) ? 0M : (Decimal) (sender.GetValue(row, this._CuryRetainageAmt) ?? (object) 0M);
    Decimal num2;
    Decimal num3 = num2 = base.GetCuryTranAmt(sender, row).GetValueOrDefault() + num1;
    Decimal num4 = num2;
    Decimal num5 = num2;
    Decimal? nullable1 = (Decimal?) sender.GetValue(row, this._OrigGroupDiscountRate);
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num5 * nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable3 = (Decimal?) sender.GetValue(row, this._OrigDocumentDiscountRate);
    Decimal? nullable4;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable1 = new Decimal?();
      nullable4 = nullable1;
    }
    else
      nullable4 = new Decimal?(nullable2.GetValueOrDefault() * nullable3.GetValueOrDefault());
    Decimal? nullable5 = nullable4;
    Decimal? nullable6 = nullable5.HasValue ? new Decimal?(num4 - nullable5.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable7 = nullable6.HasValue ? new Decimal?(num3 - nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal num6 = num2;
    Decimal num7 = num2;
    Decimal? nullable8 = (Decimal?) sender.GetValue(row, this._GroupDiscountRate);
    Decimal? nullable9;
    if (!nullable8.HasValue)
    {
      nullable1 = new Decimal?();
      nullable9 = nullable1;
    }
    else
      nullable9 = new Decimal?(num7 * nullable8.GetValueOrDefault());
    Decimal? nullable10 = nullable9;
    Decimal? nullable11 = (Decimal?) sender.GetValue(row, this._DocumentDiscountRate);
    Decimal? nullable12 = nullable10.HasValue & nullable11.HasValue ? new Decimal?(nullable10.GetValueOrDefault() * nullable11.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable13 = nullable12.HasValue ? new Decimal?(num6 - nullable12.GetValueOrDefault()) : new Decimal?();
    return new Decimal?((nullable7.HasValue & nullable13.HasValue ? new Decimal?(nullable7.GetValueOrDefault() - nullable13.GetValueOrDefault()) : new Decimal?()).Value);
  }

  protected override bool AskRecalculate(PXCache sender, PXCache detailCache, object detail)
  {
    return !PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.retainage>() && base.AskRecalculate(sender, detailCache, detail);
  }

  protected override bool IsRetainedTaxes(PXGraph graph)
  {
    APSetup current = graph.Caches[typeof (APSetup)].Current as APSetup;
    APRegister apRegister = this.ParentRow(graph) as APRegister;
    return PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.retainage>() && apRegister != null && apRegister.RetainageApply.GetValueOrDefault() && current != null && current.RetainTaxes.GetValueOrDefault();
  }

  protected virtual bool IsRoundingNeeded(PXGraph graph)
  {
    PXCache cach = graph.Caches[typeof (APSetup)];
    PX.Objects.CM.Currency currentCurrency = MultiCurrencyCalculator.GetCurrentCurrency(graph);
    int num1;
    if (currentCurrency != null)
    {
      bool? preferencesSettings = currentCurrency.UseAPPreferencesSettings;
      bool flag = false;
      if (preferencesSettings.GetValueOrDefault() == flag & preferencesSettings.HasValue)
      {
        num1 = currentCurrency.APInvoiceRounding != "N" ? 1 : 0;
        goto label_4;
      }
    }
    num1 = ((APSetup) cach.Current).InvoiceRounding != "N" ? 1 : 0;
label_4:
    int num2 = (Decimal) this.ParentGetValue(graph, this._CuryTaxRoundDiff) != 0M ? 1 : 0;
    return (num1 | num2) != 0;
  }

  protected virtual Decimal? RoundAmount(PXGraph graph, Decimal? value)
  {
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.invoiceRounding>() || ((bool?) this.ParentGetValue(graph, this._PaymentsByLinesAllowed)).GetValueOrDefault())
      return value;
    PX.Objects.CM.Currency currentCurrency = MultiCurrencyCalculator.GetCurrentCurrency(graph);
    if (currentCurrency != null)
    {
      bool? preferencesSettings = currentCurrency.UseAPPreferencesSettings;
      bool flag = false;
      if (preferencesSettings.GetValueOrDefault() == flag & preferencesSettings.HasValue)
        return APReleaseProcess.RoundAmount(value, currentCurrency.APInvoiceRounding, currentCurrency.APInvoicePrecision);
    }
    PXCache cach = graph.Caches[typeof (APSetup)];
    return APReleaseProcess.RoundAmount(value, ((APSetup) cach.Current).InvoiceRounding, ((APSetup) cach.Current).InvoicePrecision);
  }

  protected virtual void ResetRoundingDiff(PXGraph graph)
  {
    base.ParentSetValue(graph, typeof (APRegister.curyRoundDiff).Name, (object) 0M);
    base.ParentSetValue(graph, typeof (APRegister.roundDiff).Name, (object) 0M);
  }

  protected override void ParentSetValue(PXGraph graph, string fieldname, object value)
  {
    if (this.ParentCache(graph).Current == null)
      return;
    PXCache cach = graph.Caches[typeof (APSetup)];
    if (fieldname == this._CuryDocBal && cach.Current != null && this.IsRoundingNeeded(graph))
    {
      Decimal? nullable1 = (Decimal?) value;
      value = (object) this.RoundAmount(graph, (Decimal?) value);
      Decimal baseval1;
      MultiCurrencyCalculator.CuryConvBase(this.ParentCache(graph), this.ParentRow(graph), nullable1.Value, out baseval1);
      Decimal baseval2;
      MultiCurrencyCalculator.CuryConvBase(this.ParentCache(graph), this.ParentRow(graph), (Decimal) value, out baseval2);
      Decimal num1 = baseval1 - baseval2;
      Decimal? nullable2 = nullable1;
      Decimal? nullable3 = (Decimal?) value;
      nullable1 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
      Decimal num2 = (Decimal) this.ParentGetValue(graph, this._CuryTaxRoundDiff);
      Decimal num3 = (Decimal) this.ParentGetValue(graph, this._TaxRoundDiff);
      Decimal num4 = num1 + num3;
      Decimal? nullable4 = nullable1;
      Decimal num5 = num2;
      nullable1 = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + num5) : new Decimal?();
      base.ParentSetValue(graph, typeof (APRegister.curyRoundDiff).Name, (object) nullable1);
      base.ParentSetValue(graph, typeof (APRegister.roundDiff).Name, (object) num4);
    }
    else
      this.ResetRoundingDiff(graph);
    base.ParentSetValue(graph, fieldname, value);
  }

  protected override List<object> SelectTaxes<WhereType>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
  {
    List<object> taxList = new List<object>();
    BqlCommand select = (BqlCommand) new Select2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<False>, And2<Where<TaxRev.taxType, Equal<TaxType.purchase>, And<PX.Objects.TX.Tax.reverseTax, Equal<False>, Or<TaxRev.taxType, Equal<TaxType.sales>, PX.Data.And<Where<PX.Objects.TX.Tax.reverseTax, Equal<True>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.use>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.withholding>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.perUnit>>>>>>>>>, And<Required<TaxRev.startDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>();
    object[] selectParameters = new object[1]
    {
      (object) this.GetDocDate(this.ParentCache(graph), row)
    };
    object[] currents = new object[1]{ row };
    switch (taxchk)
    {
      case PXTaxCheck.Line:
        List<ITaxDetail> list1 = ((IEnumerable<ITaxDetail>) PXSelectBase<APTax, PXSelect<APTax, Where<APTax.tranType, Equal<Current<APRegister.docType>>, And<APTax.refNbr, Equal<Current<APRegister.refNbr>>>>>.Config>.SelectMultiBound(graph, new object[1]
        {
          row
        }).RowCast<APTax>()).ToList<ITaxDetail>();
        if (list1 == null || list1.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails1 = this.CollectInvolvedTaxes<WhereType>(graph, (IEnumerable<ITaxDetail>) list1, select, currents, parameters, selectParameters);
        int? nullable1 = (int?) graph.Caches[typeof (APTran)].GetValue<APTran.lineNbr>(row);
        foreach (APTax record in list1)
        {
          int? lineNbr = record.LineNbr;
          int? nullable2 = nullable1;
          if (lineNbr.GetValueOrDefault() == nullable2.GetValueOrDefault() & lineNbr.HasValue == nullable2.HasValue)
            this.InsertTax<APTax>(graph, taxchk, record, tails1, taxList);
        }
        return taxList;
      case PXTaxCheck.RecalcLine:
        List<ITaxDetail> list2 = ((IEnumerable<ITaxDetail>) PXSelectBase<APTax, PXSelect<APTax, Where<APTax.tranType, Equal<Current<APRegister.docType>>, And<APTax.refNbr, Equal<Current<APRegister.refNbr>>>>>.Config>.SelectMultiBound(graph, new object[1]
        {
          row
        }).RowCast<APTax>().OrderBy<APTax, int?>((Func<APTax, int?>) (_ => _.LineNbr)).ThenBy<APTax, string>((Func<APTax, string>) (_ => _.TaxID))).ToList<ITaxDetail>();
        if (list2 == null || list2.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails2 = this.CollectInvolvedTaxes<WhereType>(graph, (IEnumerable<ITaxDetail>) list2, select, currents, parameters, selectParameters);
        foreach (APTax record in list2)
          this.InsertTax<APTax>(graph, taxchk, record, tails2, taxList);
        return taxList;
      case PXTaxCheck.RecalcTotals:
        List<ITaxDetail> list3 = ((IEnumerable<ITaxDetail>) PXSelectBase<APTaxTran, PXSelect<APTaxTran, Where<APTaxTran.module, Equal<BatchModule.moduleAP>, And<APTaxTran.tranType, Equal<Current<APRegister.docType>>, And<APTaxTran.refNbr, Equal<Current<APRegister.refNbr>>>>>>.Config>.SelectMultiBound(graph, new object[1]
        {
          row
        }).RowCast<APTaxTran>()).ToList<ITaxDetail>();
        if (list3 == null || list3.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails3 = this.CollectInvolvedTaxes<WhereType>(graph, (IEnumerable<ITaxDetail>) list3, select, currents, parameters, selectParameters);
        foreach (APTaxTran record in list3)
          this.InsertTax<APTaxTran>(graph, taxchk, record, tails3, taxList);
        return taxList;
      default:
        return taxList;
    }
  }

  protected override void CalcDocTotals(
    PXCache sender,
    object row,
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    base.CalcDocTotals(sender, row, CuryTaxTotal, CuryInclTaxTotal, CuryWhTaxTotal, CuryTaxDiscountTotal);
    if (this.ParentGetStatus(sender.Graph) == PXEntryStatus.Deleted)
      return;
    Decimal objB = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryWhTaxTotal) ?? (object) 0M);
    if (object.Equals((object) CuryWhTaxTotal, (object) objB))
      return;
    this.ParentSetValue(sender.Graph, this._CuryWhTaxTotal, (object) CuryWhTaxTotal);
  }

  protected override void _CalcDocTotals(
    PXCache sender,
    object row,
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    if (((bool?) this.ParentGetValue<APRegister.releasedOrPrebooked>(sender.Graph)).GetValueOrDefault())
      return;
    Decimal num1 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryDiscTot) ?? (object) 0M);
    Decimal num2 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryLineTotal) ?? (object) 0M) + CuryTaxTotal - CuryInclTaxTotal - num1;
    Decimal objB = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryTaxTotal) ?? (object) 0M);
    if (!object.Equals((object) CuryTaxTotal, (object) objB))
      this.ParentSetValue(sender.Graph, this._CuryTaxTotal, (object) CuryTaxTotal);
    if (string.IsNullOrEmpty(this._CuryDocBal))
      return;
    this.ParentSetValue(sender.Graph, this._CuryDocBal, (object) num2);
  }

  protected override void AdjustTaxableAmount(
    PXCache sender,
    object row,
    List<object> taxitems,
    ref Decimal CuryTaxableAmt,
    string TaxCalcType)
  {
    Decimal valueOrDefault = ((Decimal?) this.ParentGetValue(sender.Graph, this._CuryLineTotal)).GetValueOrDefault();
    Decimal num = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryDiscTot) ?? (object) 0M);
    if (!(valueOrDefault != 0M) || !(CuryTaxableAmt != 0M))
      return;
    if (System.Math.Abs(CuryTaxableAmt - valueOrDefault) < 0.00005M)
    {
      CuryTaxableAmt -= num;
    }
    else
    {
      if (!(System.Math.Abs(valueOrDefault - num - CuryTaxableAmt) < this.GetPrecisionBasedNegligibleDifference(sender.Graph, row)))
        return;
      CuryTaxableAmt = valueOrDefault - num;
    }
  }

  protected override void SetExtCostExt(PXCache sender, object child, Decimal? value)
  {
    if (!(child is APTran apTran))
      return;
    apTran.CuryLineAmt = value;
    sender.Update((object) apTran);
  }

  protected override string GetExtCostLabel(PXCache sender, object row)
  {
    return ((PXFieldState) sender.GetValueExt<APTran.curyLineAmt>(row)).DisplayName;
  }

  protected override bool ShouldRecalculateTaxesOnRowUpdate(
    PXCache rowCache,
    object newRow,
    object oldRow)
  {
    return !rowCache.ObjectsEqual<APTran.lCDocType, APTran.lCRefNbr, APTran.lCLineNbr, APTran.pONbr, APTran.receiptNbr>(newRow, oldRow) || base.ShouldRecalculateTaxesOnRowUpdate(rowCache, newRow, oldRow);
  }

  protected override bool SkipDirectTax(PXCache sender, object row, string applicableDirectTaxId)
  {
    if (!(row is APTran apTran))
      return false;
    if (!string.IsNullOrEmpty(apTran.PONbr) || !string.IsNullOrEmpty(apTran.ReceiptNbr))
    {
      sender.RaiseExceptionHandling(this._TaxCategoryID, row, (object) apTran.TaxCategoryID, (Exception) new PXSetPropertyException("The {0} direct-entry tax cannot be applied to a line linked to a purchase order, subcontract, or purchase receipt.", PXErrorLevel.Warning, new object[1]
      {
        (object) applicableDirectTaxId
      }));
      return true;
    }
    if (string.IsNullOrEmpty(apTran.LCRefNbr) || !(POLandedCostDetail.PK.Find(sender.Graph, apTran.LCDocType, apTran.LCRefNbr, apTran.LCLineNbr)?.AllocationMethod != "N"))
      return false;
    sender.RaiseExceptionHandling(this._TaxCategoryID, row, (object) apTran.TaxCategoryID, (Exception) new PXSetPropertyException("The {0} direct-entry tax can be applied only to a landed cost line with the None allocation method.", PXErrorLevel.Warning, new object[1]
    {
      (object) applicableDirectTaxId
    }));
    return true;
  }

  protected virtual void TaxCategoryUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null || !this._IncludeDirectTaxLine)
      return;
    this.DefualtDirectTaxRealtedFieldValue(sender, e.Row, this.IsDirectTaxLine(sender, e.Row));
  }

  protected override void UpdateIsDirectTaxLineFeildValue(
    PXCache cache,
    object row,
    bool newValue,
    bool oldValue)
  {
    if (!(row is APTran apTran) || newValue == oldValue)
      return;
    APTran copy = cache.CreateCopy(row) as APTran;
    this.DefualtDirectTaxRealtedFieldValue(cache, (object) apTran, newValue);
    cache.RaiseRowUpdated((object) apTran, (object) copy);
  }

  protected virtual void DefualtDirectTaxRealtedFieldValue(
    PXCache cache,
    object row,
    bool isDirectTax)
  {
    if (!(row is APTran apTran))
      return;
    if (!apTran.IsDirectTaxLine.GetValueOrDefault() & isDirectTax)
    {
      cache.SetValueExt<APTran.isDirectTaxLine>(row, (object) true);
      cache.SetValueExt<APTran.retainagePct>(row, (object) 0.0M);
      cache.SetValueExt<APTran.curyRetainageAmt>(row, (object) 0.0M);
      cache.SetValueExt<APTran.manualDisc>(row, (object) true);
      cache.SetValueExt<APTran.curyDiscAmt>(row, (object) 0.0M);
      cache.SetValueExt<APTran.discPct>(row, (object) 0.0M);
      cache.SetValueExt<APTran.skipDisc>(row, (object) true);
      cache.SetValueExt<APTran.automaticDiscountsDisabled>(row, (object) true);
      cache.SetValueExt<APTran.deferredCode>(row, (object) null);
      cache.SetValueExt<APTran.dRTermStartDate>(row, (object) null);
      cache.SetValueExt<APTran.dRTermEndDate>(row, (object) null);
    }
    else
    {
      if (!apTran.IsDirectTaxLine.GetValueOrDefault() || isDirectTax)
        return;
      cache.SetValueExt<APTran.isDirectTaxLine>(row, (object) false);
      cache.SetValueExt<APTran.skipDisc>(row, (object) false);
      cache.SetValueExt<APTran.manualDisc>(row, (object) false);
      cache.SetValueExt<APTran.automaticDiscountsDisabled>(row, (object) false);
    }
  }

  public virtual IEnumerable<T> DistributeTaxDiscrepancy<T, CuryTaxField, BaseTaxField>(
    PXGraph graph,
    IEnumerable<T> taxDetList,
    Decimal CuryTaxAmt)
    where T : TaxDetail, ITranTax
    where CuryTaxField : IBqlField
    where BaseTaxField : IBqlField
  {
    return this.DistributeTaxDiscrepancy<T, CuryTaxField, BaseTaxField>(graph, taxDetList, CuryTaxAmt, false);
  }

  protected abstract class curyRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxAttribute.curyRetainageAmt>
  {
  }

  protected abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxAttribute.paymentsByLinesAllowed>
  {
  }
}
