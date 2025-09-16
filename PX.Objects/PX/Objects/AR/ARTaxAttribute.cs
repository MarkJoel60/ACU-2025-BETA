// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM.TemporaryHelpers;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.SO;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable enable
namespace PX.Objects.AR;

public class ARTaxAttribute : TaxAttribute
{
  protected 
  #nullable disable
  Type CuryRetainageAmt = typeof (ARTaxAttribute.curyRetainageAmt);
  protected Type PaymentsByLinesAllowed = typeof (ARTaxAttribute.paymentsByLinesAllowed);
  protected Type RetainageApply = typeof (ARTaxAttribute.retainageApply);
  protected Type IsRetainageDocument = typeof (ARTaxAttribute.isRetainageDocument);
  protected string _DisableAutomaticTaxCalculation = "DisableAutomaticTaxCalculation";

  protected override bool CalcGrossOnDocumentLevel
  {
    get => true;
    set => base.CalcGrossOnDocumentLevel = value;
  }

  protected virtual short SortOrder => 0;

  protected string _CuryRetainageAmt => this.CuryRetainageAmt.Name;

  protected string _PaymentsByLinesAllowed => this.PaymentsByLinesAllowed.Name;

  protected string _RetainageApply => this.RetainageApply.Name;

  protected string _IsRetainageDocument => this.IsRetainageDocument.Name;

  public ARTaxAttribute(Type ParentType, Type TaxType, Type TaxSumType = null, Type parentBranchIDField = null)
    : this(ParentType, TaxType, TaxSumType, (Type) null, parentBranchIDField)
  {
  }

  public ARTaxAttribute(
    Type ParentType,
    Type TaxType,
    Type TaxSumType,
    Type TaxCalculationMode,
    Type parentBranchIDField)
    : base(ParentType, TaxType, TaxSumType, TaxCalculationMode, parentBranchIDField)
  {
    this.CuryTranAmt = typeof (ARTran.curyTranAmt);
    this.GroupDiscountRate = typeof (ARTran.groupDiscountRate);
    this.CuryLineTotal = typeof (ARInvoice.curyLineTotal);
    this.CuryDiscTot = typeof (ARInvoice.curyDiscTot);
    this.TaxCalcMode = typeof (ARInvoice.taxCalcMode);
    PXAggregateAttribute.AggregatedAttributesCollection attributes1 = this._Attributes;
    PXUnboundFormulaAttribute formulaAttribute1 = new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<ARTran.lineType, NotEqual<SOLineType.discount>>, ARTran.curyTranAmt>, decimal0>), typeof (SumCalc<ARInvoice.curyLineTotal>));
    ((PXFormulaAttribute) formulaAttribute1).ForceAggregateRecalculation = PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>();
    attributes1.Add((PXEventSubscriberAttribute) formulaAttribute1);
    PXAggregateAttribute.AggregatedAttributesCollection attributes2 = this._Attributes;
    PXUnboundFormulaAttribute formulaAttribute2 = new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<ARTran.tranType, Equal<ARDocType.prepaymentInvoice>, And<ARTran.lineType, NotIn3<SOLineType.discount, SOLineType.freight>>>, Mult<ARTran.curyDiscAmt, Div<ARTranVATRecognitionOnPrepayments.prepaymentPct, decimal100>>, Case<Where<ARTran.lineType, NotIn3<SOLineType.discount, SOLineType.freight>>, ARTran.curyDiscAmt>>, decimal0>), typeof (SumCalc<ARInvoice.curyLineDiscTotal>));
    ((PXFormulaAttribute) formulaAttribute2).ForceAggregateRecalculation = PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>();
    attributes2.Add((PXEventSubscriberAttribute) formulaAttribute2);
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<ARTran.lineType, NotEqual<SOLineType.miscCharge>, And<ARTran.lineType, NotEqual<SOLineType.freight>, And<ARTran.lineType, NotEqual<SOLineType.discount>, And<ARTran.lineType, IsNotNull, And<ARTran.lineType, NotEqual<Empty>>>>>>, ARTran.curyExtPrice>, decimal0>), typeof (SumCalc<ARInvoice.curyGoodsExtPriceTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<ARTran.lineType, Equal<SOLineType.miscCharge>, Or<ARTran.lineType, IsNull, Or<ARTran.lineType, Equal<Empty>>>>, ARTran.curyExtPrice>, decimal0>), typeof (SumCalc<ARInvoice.curyMiscExtPriceTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<ARTran.commissionable, Equal<True>, And<Where<ARTran.lineType, NotEqual<SOLineType.discount>, And<Where<ARTran.tranType, Equal<ARDocType.creditMemo>, Or<ARTran.tranType, Equal<ARDocType.cashReturn>>>>>>>, Minus<Sub<Sub<ARTran.curyTranAmt, Sub<ARTran.curyTranAmt, Mult<Mult<ARTran.curyTranAmt, ARTran.origGroupDiscountRate>, ARTran.origDocumentDiscountRate>>>, Sub<ARTran.curyTranAmt, Mult<Mult<ARTran.curyTranAmt, ARTran.groupDiscountRate>, ARTran.documentDiscountRate>>>>, Case<Where<ARTran.commissionable, Equal<True>, And<Where<ARTran.lineType, NotEqual<SOLineType.discount>>>>, Sub<Sub<ARTran.curyTranAmt, Sub<ARTran.curyTranAmt, Mult<Mult<ARTran.curyTranAmt, ARTran.origGroupDiscountRate>, ARTran.origDocumentDiscountRate>>>, Sub<ARTran.curyTranAmt, Mult<Mult<ARTran.curyTranAmt, ARTran.groupDiscountRate>, ARTran.documentDiscountRate>>>>>, decimal0>), typeof (SumCalc<ARSalesPerTran.curyCommnblAmt>)));
  }

  public override int CompareTo(object other)
  {
    return this.SortOrder.CompareTo(((ARTaxAttribute) other).SortOrder);
  }

  public override object Insert(PXCache cache, object item)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARTaxAttribute.\u003C\u003Ec__DisplayClass25_0 cDisplayClass250 = new ARTaxAttribute.\u003C\u003Ec__DisplayClass25_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass250.cache = cache;
    // ISSUE: reference to a compiler-generated field
    PXResultset<ARTax> pxResultset = PXSelectBase<ARTax, PXSelect<ARTax, Where<ARTax.tranType, Equal<Current<ARRegister.docType>>, And<ARTax.refNbr, Equal<Current<ARRegister.refNbr>>>>>.Config>.Select(cDisplayClass250.cache.Graph, Array.Empty<object>());
    // ISSUE: reference to a compiler-generated field
    cDisplayClass250.taxLinesList = new List<object>((IEnumerable<object>) GraphHelper.RowCast<ARTax>((IEnumerable) pxResultset));
    // ISSUE: method pointer
    PXRowInserted pxRowInserted = new PXRowInserted((object) cDisplayClass250, __methodptr(\u003CInsert\u003Eb__0));
    // ISSUE: reference to a compiler-generated field
    cDisplayClass250.cache.Graph.RowInserted.AddHandler<ARTax>(pxRowInserted);
    try
    {
      // ISSUE: reference to a compiler-generated field
      return base.Insert(cDisplayClass250.cache, item);
    }
    finally
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass250.cache.Graph.RowInserted.RemoveHandler<ARTax>(pxRowInserted);
    }
  }

  protected override void AddTaxTotals(PXCache sender, string taxID, object row)
  {
    if ((row is ARTran arTran ? arTran.TranType : (string) null) == "PPI")
    {
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      if (((IQueryable<PXResult<PX.Objects.TX.Tax>>) PXSelectBase<PX.Objects.TX.Tax, PXSelect<PX.Objects.TX.Tax, Where<PX.Objects.TX.Tax.taxID, Equal<Required<PX.Objects.TX.Tax.taxID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) taxID
      })).Select<PXResult<PX.Objects.TX.Tax>, PX.Objects.TX.Tax>(Expression.Lambda<Func<PXResult<PX.Objects.TX.Tax>, PX.Objects.TX.Tax>>((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression)).SingleOrDefault<PX.Objects.TX.Tax>()?.TaxType == "Q")
        return;
    }
    base.AddTaxTotals(sender, taxID, row);
  }

  protected override void FillTaxDetailValuesForPerUnitTax(
    PXCache taxDetailCache,
    PX.Objects.TX.Tax tax,
    TaxRev taxRevision,
    TaxDetail taxDetail,
    PXCache rowCache,
    object row,
    Decimal taxableQty,
    Decimal curyTaxAmount)
  {
    base.FillTaxDetailValuesForPerUnitTax(taxDetailCache, tax, taxRevision, taxDetail, rowCache, row, taxableQty, curyTaxAmount);
    ARTran Line = row as ARTran;
    ARTax taxdetail = taxDetail as ARTax;
    if (!(Line?.TranType == "PPI") || taxdetail == null)
      return;
    Decimal? nullable1 = taxdetail.CuryTaxAmt;
    if (!nullable1.HasValue || !(tax.TaxCalcLevel != "0"))
      return;
    ARTranVATRecognitionOnPrepayments extension = taxDetailCache.Graph.Caches[typeof (ARTran)].GetExtension<ARTranVATRecognitionOnPrepayments>((object) Line);
    ARTran arTran = Line;
    PXCache sender = taxDetailCache;
    ARTran row1 = Line;
    nullable1 = extension.PrepaymentAmt;
    Decimal num1 = nullable1.Value;
    Decimal num2 = this.GetPerUnitTaxDetails(taxDetailCache, taxdetail, Line).Sum<ARTax>((Func<ARTax, Decimal>) (_ => _.CuryTaxAmt.GetValueOrDefault()));
    nullable1 = extension.PrepaymentPct;
    Decimal num3 = nullable1.Value;
    Decimal num4 = num2 * num3 / 100M;
    Decimal val = num1 + num4;
    Decimal? nullable2 = new Decimal?(MultiCurrencyCalculator.RoundCury(sender, (object) row1, val));
    arTran.CuryTranAmt = nullable2;
  }

  protected virtual IEnumerable<ARTax> GetPerUnitTaxDetails(
    PXCache cache,
    ARTax taxdetail,
    ARTran Line)
  {
    foreach (ARTax perUnitTaxDetail in PXParentAttribute.SelectSiblings(cache, (object) taxdetail, ((object) Line).GetType()).Cast<ARTax>().Where<ARTax>((Func<ARTax, bool>) (_ => _.TaxUOM != null)))
    {
      PX.Objects.TX.Tax tax = PXResultset<PX.Objects.TX.Tax>.op_Implicit(PXSelectBase<PX.Objects.TX.Tax, PXViewOf<PX.Objects.TX.Tax>.BasedOn<SelectFromBase<PX.Objects.TX.Tax, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.TX.Tax.taxID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select(cache.Graph, new object[1]
      {
        (object) perUnitTaxDetail.TaxID
      }));
      if (tax.TaxType == "Q" && tax.TaxCalcLevel != "0")
        yield return perUnitTaxDetail;
    }
  }

  protected override Decimal GetPerUnitTaxAmountForTaxableAdjustmentCalculation(
    PX.Objects.TX.Tax taxForTaxableAdustment,
    TaxDetail taxDetail,
    PXCache taxDetailCache,
    object row,
    PXCache rowCache)
  {
    return (row is ARTran arTran ? arTran.TranType : (string) null) == "PPI" ? base.GetPerUnitTaxAmountForTaxableAdjustmentCalculation(taxForTaxableAdustment, taxDetail, taxDetailCache, row, rowCache) * (rowCache.GetExtension<ARTranVATRecognitionOnPrepayments>(row).PrepaymentPct.GetValueOrDefault() / 100M) : base.GetPerUnitTaxAmountForTaxableAdjustmentCalculation(taxForTaxableAdustment, taxDetail, taxDetailCache, row, rowCache);
  }

  public override object Update(PXCache cache, object item)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARTaxAttribute.\u003C\u003Ec__DisplayClass30_0 cDisplayClass300 = new ARTaxAttribute.\u003C\u003Ec__DisplayClass30_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass300.cache = cache;
    // ISSUE: reference to a compiler-generated field
    PXResultset<ARTax> pxResultset = PXSelectBase<ARTax, PXSelect<ARTax, Where<ARTax.tranType, Equal<Current<ARRegister.docType>>, And<ARTax.refNbr, Equal<Current<ARRegister.refNbr>>>>>.Config>.Select(cDisplayClass300.cache.Graph, Array.Empty<object>());
    // ISSUE: reference to a compiler-generated field
    cDisplayClass300.taxLinesList = new List<object>((IEnumerable<object>) GraphHelper.RowCast<ARTax>((IEnumerable) pxResultset));
    // ISSUE: method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) cDisplayClass300, __methodptr(\u003CUpdate\u003Eb__0));
    // ISSUE: reference to a compiler-generated field
    cDisplayClass300.cache.Graph.RowUpdated.AddHandler<ARTax>(pxRowUpdated);
    try
    {
      // ISSUE: reference to a compiler-generated field
      return base.Update(cDisplayClass300.cache, item);
    }
    finally
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass300.cache.Graph.RowUpdated.RemoveHandler<ARTax>(pxRowUpdated);
    }
  }

  public override object Delete(PXCache cache, object item)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARTaxAttribute.\u003C\u003Ec__DisplayClass31_0 cDisplayClass310 = new ARTaxAttribute.\u003C\u003Ec__DisplayClass31_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass310.cache = cache;
    // ISSUE: reference to a compiler-generated field
    PXResultset<ARTax> pxResultset = PXSelectBase<ARTax, PXSelect<ARTax, Where<ARTax.tranType, Equal<Current<ARRegister.docType>>, And<ARTax.refNbr, Equal<Current<ARRegister.refNbr>>>>>.Config>.Select(cDisplayClass310.cache.Graph, Array.Empty<object>());
    // ISSUE: reference to a compiler-generated field
    cDisplayClass310.taxLinesList = new List<object>((IEnumerable<object>) GraphHelper.RowCast<ARTax>((IEnumerable) pxResultset));
    // ISSUE: method pointer
    PXRowDeleted pxRowDeleted = new PXRowDeleted((object) cDisplayClass310, __methodptr(\u003CDelete\u003Eb__0));
    // ISSUE: reference to a compiler-generated field
    cDisplayClass310.cache.Graph.RowDeleted.AddHandler<ARTax>(pxRowDeleted);
    try
    {
      // ISSUE: reference to a compiler-generated field
      return base.Delete(cDisplayClass310.cache, item);
    }
    finally
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass310.cache.Graph.RowDeleted.RemoveHandler<ARTax>(pxRowDeleted);
    }
  }

  protected override Decimal? GetCuryTranAmt(PXCache sender, object row, string TaxCalcType)
  {
    Decimal? finalAmtNoRounding = this.GetDocLineFinalAmtNoRounding(sender, row, TaxCalcType);
    return new Decimal?(MultiCurrencyCalculator.RoundCury(sender, row, finalAmtNoRounding.Value));
  }

  protected override Decimal? GetDocLineFinalAmtNoRounding(
    PXCache sender,
    object row,
    string TaxCalcType)
  {
    Decimal num1 = this.IsRetainedTaxes(sender.Graph) ? 0M : (Decimal) (sender.GetValue(row, this._CuryRetainageAmt) ?? (object) 0M);
    Decimal? nullable1;
    Decimal valueOrDefault;
    if (row is ARTran arTran && arTran.TranType == "PPI" && arTran.LineType != "DS")
    {
      nullable1 = sender.GetExtension<ARTranVATRecognitionOnPrepayments>(row).CuryPrepaymentAmt;
      valueOrDefault = nullable1.GetValueOrDefault();
    }
    else
    {
      nullable1 = base.GetCuryTranAmt(sender, row);
      valueOrDefault = nullable1.GetValueOrDefault();
    }
    Decimal num2;
    Decimal num3 = num2 = valueOrDefault + num1;
    Decimal num4 = num2;
    Decimal num5 = num2;
    Decimal? nullable2 = (Decimal?) sender.GetValue(row, this._OrigGroupDiscountRate);
    Decimal? nullable3 = nullable2.HasValue ? new Decimal?(num5 * nullable2.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable4 = (Decimal?) sender.GetValue(row, this._OrigDocumentDiscountRate);
    Decimal? nullable5;
    if (!(nullable3.HasValue & nullable4.HasValue))
    {
      nullable2 = new Decimal?();
      nullable5 = nullable2;
    }
    else
      nullable5 = new Decimal?(nullable3.GetValueOrDefault() * nullable4.GetValueOrDefault());
    Decimal? nullable6 = nullable5;
    Decimal? nullable7 = nullable6.HasValue ? new Decimal?(num4 - nullable6.GetValueOrDefault()) : new Decimal?();
    nullable1 = nullable7.HasValue ? new Decimal?(num3 - nullable7.GetValueOrDefault()) : new Decimal?();
    Decimal num6 = num2;
    Decimal num7 = num2;
    Decimal? nullable8 = (Decimal?) sender.GetValue(row, this._GroupDiscountRate);
    Decimal? nullable9;
    if (!nullable8.HasValue)
    {
      nullable2 = new Decimal?();
      nullable9 = nullable2;
    }
    else
      nullable9 = new Decimal?(num7 * nullable8.GetValueOrDefault());
    Decimal? nullable10 = nullable9;
    Decimal? nullable11 = (Decimal?) sender.GetValue(row, this._DocumentDiscountRate);
    Decimal? nullable12 = nullable10.HasValue & nullable11.HasValue ? new Decimal?(nullable10.GetValueOrDefault() * nullable11.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable13 = nullable12.HasValue ? new Decimal?(num6 - nullable12.GetValueOrDefault()) : new Decimal?();
    return new Decimal?((nullable1.HasValue & nullable13.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable13.GetValueOrDefault()) : new Decimal?()).Value);
  }

  protected override void DateUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ARRegister row))
      return;
    bool? nullable = row.RetainageApply;
    if ((!nullable.GetValueOrDefault() ? 0 : (row.DocType == "CRM" ? 1 : 0)) != 0)
      return;
    nullable = row.IsRetainageDocument;
    if (nullable.GetValueOrDefault())
      return;
    base.DateUpdated(sender, e);
  }

  protected override void SetTaxableAmt(PXCache sender, object row, Decimal? value)
  {
    sender.SetValue<ARTran.curyTaxableAmt>(row, (object) value);
  }

  protected override void SetTaxAmt(PXCache sender, object row, Decimal? value)
  {
    sender.SetValue<ARTran.curyTaxAmt>(row, (object) value);
  }

  protected override void SetExtCostExt(PXCache sender, object child, Decimal? value)
  {
    if (!(child is ARTran arTran))
      return;
    arTran.CuryExtPrice = value;
    sender.Update((object) arTran);
  }

  protected override string GetExtCostLabel(PXCache sender, object row)
  {
    return ((PXFieldState) sender.GetValueExt<ARTran.curyExtPrice>(row)).DisplayName;
  }

  protected override bool AskRecalculate(PXCache sender, PXCache detailCache, object detail)
  {
    return !PXAccess.FeatureInstalled<FeaturesSet.retainage>() && base.AskRecalculate(sender, detailCache, detail);
  }

  protected override bool IsRetainedTaxes(PXGraph graph)
  {
    ARSetup current = graph.Caches[typeof (ARSetup)].Current as ARSetup;
    ARRegister arRegister = this.ParentRow(graph) as ARRegister;
    return PXAccess.FeatureInstalled<FeaturesSet.retainage>() && arRegister != null && arRegister.RetainageApply.GetValueOrDefault() && current != null && current.RetainTaxes.GetValueOrDefault();
  }

  protected virtual bool IsRoundingNeeded(PXGraph graph)
  {
    PX.Objects.CM.Currency currentCurrency = MultiCurrencyCalculator.GetCurrentCurrency(graph);
    if (((bool?) this.ParentGetValue(graph, this._PaymentsByLinesAllowed)).GetValueOrDefault() || ((bool?) this.ParentGetValue(graph, this._RetainageApply)).GetValueOrDefault() || ((bool?) this.ParentGetValue(graph, this._IsRetainageDocument)).GetValueOrDefault())
      return false;
    if (currentCurrency != null)
    {
      bool? preferencesSettings = currentCurrency.UseARPreferencesSettings;
      bool flag = false;
      if (preferencesSettings.GetValueOrDefault() == flag & preferencesSettings.HasValue)
        return currentCurrency.ARInvoiceRounding != "N";
    }
    return ((ARSetup) ((PXCache) GraphHelper.Caches<ARSetup>(graph)).Current).InvoiceRounding != "N";
  }

  protected virtual Decimal? RoundAmount(PXGraph graph, Decimal? value)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.invoiceRounding>())
      return value;
    PX.Objects.CM.Currency currentCurrency = MultiCurrencyCalculator.GetCurrentCurrency(graph);
    if (currentCurrency != null)
    {
      bool? preferencesSettings = currentCurrency.UseARPreferencesSettings;
      bool flag = false;
      if (preferencesSettings.GetValueOrDefault() == flag & preferencesSettings.HasValue)
        return ARReleaseProcess.RoundAmount(value, currentCurrency.ARInvoiceRounding, currentCurrency.ARInvoicePrecision);
    }
    ARSetup current = (ARSetup) graph.Caches[typeof (ARSetup)].Current;
    return ARReleaseProcess.RoundAmount(value, current.InvoiceRounding, current.InvoicePrecision);
  }

  protected virtual void ResetRoundingDiff(PXGraph graph)
  {
    base.ParentSetValue(graph, typeof (ARRegister.curyRoundDiff).Name, (object) 0M);
    base.ParentSetValue(graph, typeof (ARRegister.roundDiff).Name, (object) 0M);
  }

  protected override void ParentSetValue(PXGraph graph, string fieldname, object value)
  {
    if (this.ParentCache(graph).Current == null)
      return;
    PXCache cach = graph.Caches[typeof (ARSetup)];
    if (fieldname == this._CuryDocBal && cach.Current != null && this.IsRoundingNeeded(graph))
    {
      Decimal? nullable1 = (Decimal?) value;
      value = (object) this.RoundAmount(graph, (Decimal?) value);
      graph.Caches[typeof (PX.Objects.CM.CurrencyInfo)].ClearQueryCacheObsolete();
      Decimal baseval1;
      MultiCurrencyCalculator.CuryConvBase(this.ParentCache(graph), this.ParentRow(graph), nullable1.Value, out baseval1);
      Decimal baseval2;
      MultiCurrencyCalculator.CuryConvBase(this.ParentCache(graph), this.ParentRow(graph), (Decimal) value, out baseval2);
      Decimal num = baseval1 - baseval2;
      Decimal? nullable2 = nullable1;
      Decimal? nullable3 = (Decimal?) value;
      Decimal? nullable4 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
      base.ParentSetValue(graph, typeof (ARRegister.curyRoundDiff).Name, (object) nullable4);
      base.ParentSetValue(graph, typeof (ARRegister.roundDiff).Name, (object) num);
    }
    else
      this.ResetRoundingDiff(graph);
    base.ParentSetValue(graph, fieldname, value);
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
    if (Math.Abs(CuryTaxableAmt - valueOrDefault) < 0.00005M)
    {
      CuryTaxableAmt -= num;
    }
    else
    {
      if (!(Math.Abs(valueOrDefault - num - CuryTaxableAmt) < this.GetPrecisionBasedNegligibleDifference(sender.Graph, row)))
        return;
      CuryTaxableAmt = valueOrDefault - num;
    }
  }

  public override IEnumerable<ITaxDetail> MatchesCategory(
    PXCache sender,
    object row,
    IEnumerable<ITaxDetail> zonetaxlist)
  {
    string taxCategory1 = this.GetTaxCategory(sender, row);
    List<ITaxDetail> taxDetailList = new List<ITaxDetail>();
    PX.Objects.TX.TaxCategory taxCategory2 = PXResultset<PX.Objects.TX.TaxCategory>.op_Implicit(PXSelectBase<PX.Objects.TX.TaxCategory, PXSelect<PX.Objects.TX.TaxCategory, Where<PX.Objects.TX.TaxCategory.taxCategoryID, Equal<Required<PX.Objects.TX.TaxCategory.taxCategoryID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) taxCategory1
    }));
    if (taxCategory2 == null)
      return (IEnumerable<ITaxDetail>) taxDetailList;
    HashSet<string> stringSet;
    if (sender.Graph is ARInvoiceEntry graph)
    {
      if (!graph.TaxesByTaxCategory.TryGetValue(taxCategory2.TaxCategoryID, out stringSet))
      {
        stringSet = new HashSet<string>();
        foreach (PXResult<TaxCategoryDet> pxResult in PXSelectBase<TaxCategoryDet, PXSelect<TaxCategoryDet, Where<TaxCategoryDet.taxCategoryID, Equal<Required<TaxCategoryDet.taxCategoryID>>>>.Config>.Select(sender.Graph, new object[1]
        {
          (object) taxCategory1
        }))
        {
          TaxCategoryDet taxCategoryDet = PXResult<TaxCategoryDet>.op_Implicit(pxResult);
          stringSet.Add(taxCategoryDet.TaxID);
        }
        graph.TaxesByTaxCategory.Add(taxCategory2.TaxCategoryID, stringSet);
      }
    }
    else
    {
      stringSet = new HashSet<string>();
      foreach (PXResult<TaxCategoryDet> pxResult in PXSelectBase<TaxCategoryDet, PXSelect<TaxCategoryDet, Where<TaxCategoryDet.taxCategoryID, Equal<Required<TaxCategoryDet.taxCategoryID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) taxCategory1
      }))
      {
        TaxCategoryDet taxCategoryDet = PXResult<TaxCategoryDet>.op_Implicit(pxResult);
        stringSet.Add(taxCategoryDet.TaxID);
      }
    }
    foreach (ITaxDetail taxDetail in zonetaxlist)
    {
      bool flag1 = stringSet.Contains(taxDetail.TaxID);
      bool? taxCatFlag = taxCategory2.TaxCatFlag;
      bool flag2 = false;
      if (!(taxCatFlag.GetValueOrDefault() == flag2 & taxCatFlag.HasValue & flag1))
      {
        taxCatFlag = taxCategory2.TaxCatFlag;
        if (!taxCatFlag.GetValueOrDefault() || flag1)
          continue;
      }
      taxDetailList.Add(taxDetail);
    }
    return (IEnumerable<ITaxDetail>) taxDetailList;
  }

  protected override List<object> SelectTaxes<Where>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
  {
    List<object> taxList = new List<object>();
    BqlCommand select = (BqlCommand) new Select2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<False>, And<TaxRev.taxType, Equal<TaxType.sales>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.use>, And<PX.Objects.TX.Tax.reverseTax, Equal<False>, And<Required<TaxRev.startDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>>>>();
    object[] selectParameters = new object[1]
    {
      (object) this.GetDocDate(this.ParentCache(graph), row)
    };
    object[] currents = new object[1]{ row };
    switch (taxchk)
    {
      case PXTaxCheck.Line:
        List<ITaxDetail> list1 = ((IEnumerable<ITaxDetail>) GraphHelper.RowCast<ARTax>((IEnumerable) PXSelectBase<ARTax, PXSelect<ARTax, Where<ARTax.tranType, Equal<Current<ARRegister.docType>>, And<ARTax.refNbr, Equal<Current<ARRegister.refNbr>>>>>.Config>.SelectMultiBound(graph, new object[1]
        {
          row
        }, Array.Empty<object>()))).ToList<ITaxDetail>();
        if (list1 == null || list1.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails1 = this.CollectInvolvedTaxes<Where>(graph, (IEnumerable<ITaxDetail>) list1, select, currents, parameters, selectParameters);
        int? nullable1 = new int?(int.MinValue);
        if (row != null && row.GetType() == typeof (ARTran))
          nullable1 = (int?) graph.Caches[typeof (ARTran)].GetValue<ARTran.lineNbr>(row);
        foreach (ARTax record in list1)
        {
          int? lineNbr = record.LineNbr;
          int? nullable2 = nullable1;
          if (lineNbr.GetValueOrDefault() == nullable2.GetValueOrDefault() & lineNbr.HasValue == nullable2.HasValue)
            this.InsertTax<ARTax>(graph, taxchk, record, tails1, taxList);
        }
        return taxList;
      case PXTaxCheck.RecalcLine:
        List<ITaxDetail> list2 = ((IEnumerable<ITaxDetail>) GraphHelper.RowCast<ARTax>((IEnumerable) PXSelectBase<ARTax, PXSelect<ARTax, Where<ARTax.tranType, Equal<Current<ARRegister.docType>>, And<ARTax.refNbr, Equal<Current<ARRegister.refNbr>>>>>.Config>.SelectMultiBound(graph, new object[1]
        {
          row
        }, Array.Empty<object>())).OrderBy<ARTax, int?>((Func<ARTax, int?>) (_ => _.LineNbr)).ThenBy<ARTax, string>((Func<ARTax, string>) (_ => _.TaxID))).ToList<ITaxDetail>();
        if (list2 == null || list2.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails2 = this.CollectInvolvedTaxes<Where>(graph, (IEnumerable<ITaxDetail>) list2, select, currents, parameters, selectParameters);
        foreach (ARTax record in list2)
          this.InsertTax<ARTax>(graph, taxchk, record, tails2, taxList);
        return taxList;
      case PXTaxCheck.RecalcTotals:
        List<ITaxDetail> list3 = ((IEnumerable<ITaxDetail>) GraphHelper.RowCast<ARTaxTran>((IEnumerable) PXSelectBase<ARTaxTran, PXSelect<ARTaxTran, Where<ARTaxTran.module, Equal<BatchModule.moduleAR>, And<ARTaxTran.tranType, Equal<Current<ARRegister.docType>>, And<ARTaxTran.refNbr, Equal<Current<ARRegister.refNbr>>>>>>.Config>.SelectMultiBound(graph, new object[1]
        {
          row
        }, Array.Empty<object>()))).ToList<ITaxDetail>();
        if (list3 == null || list3.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails3 = this.CollectInvolvedTaxes<Where>(graph, (IEnumerable<ITaxDetail>) list3, select, currents, parameters, selectParameters);
        foreach (ARTaxTran record in list3)
          this.InsertTax<ARTaxTran>(graph, taxchk, record, tails3, taxList);
        return taxList;
      default:
        return taxList;
    }
  }

  protected override List<object> SelectInclusiveTaxes(PXGraph graph, object document)
  {
    int num;
    if (!(graph is ARInvoiceEntry arInvoiceEntry))
    {
      num = 0;
    }
    else
    {
      PXSelectJoin<ARInvoice, LeftJoinSingleTable<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>>, Where<ARInvoice.docType, Equal<Optional<ARInvoice.docType>>, And2<Where<ARInvoice.origModule, Equal<BatchModule.moduleAR>, Or<ARInvoice.origModule, Equal<BatchModule.moduleEP>, Or<ARInvoice.released, Equal<True>>>>, And<Where<Customer.bAccountID, IsNull, Or<Match<Customer, Current<AccessInfo.userName>>>>>>>> document1 = arInvoiceEntry.Document;
      bool? nullable;
      if (document1 == null)
      {
        nullable = new bool?();
      }
      else
      {
        ARInvoice current = ((PXSelectBase<ARInvoice>) document1).Current;
        nullable = current != null ? new bool?(current.IsPrepaymentInvoiceDocument()) : new bool?();
      }
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    return num != 0 ? base.SelectInclusiveTaxes(graph, document).Where<object>((Func<object, bool>) (taxRow => !this.IsPerUnitTax(PXResult.Unwrap<PX.Objects.TX.Tax>(taxRow)))).ToList<object>() : base.SelectInclusiveTaxes(graph, document);
  }

  protected override List<object> SelectDocumentLines(PXGraph graph, object row)
  {
    return GraphHelper.RowCast<ARTran>((IEnumerable) PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.tranType, Equal<Current<ARRegister.docType>>, And<ARTran.refNbr, Equal<Current<ARRegister.refNbr>>>>>.Config>.SelectMultiBound(graph, new object[1]
    {
      row
    }, Array.Empty<object>())).Select<ARTran, object>((Func<ARTran, object>) (_ => (object) _)).ToList<object>();
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2022 R2.")]
  protected virtual void AppendTail<T, W>(
    PXGraph graph,
    List<object> ret,
    T record,
    object row,
    params object[] parameters)
    where T : class, ITaxDetail, IBqlTable, new()
    where W : IBqlWhere, new()
  {
    IComparer<PX.Objects.TX.Tax> calculationLevelComparer = this.GetTaxByCalculationLevelComparer();
    ExceptionExtensions.ThrowOnNull<IComparer<PX.Objects.TX.Tax>>(calculationLevelComparer, "taxByCalculationLevelComparer", (string) null);
    PXSelectReadonly2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<False>, And<TaxRev.taxType, Equal<TaxType.sales>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.use>, And<PX.Objects.TX.Tax.reverseTax, Equal<False>, And<Required<TaxRev.startDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>>>, Where2<Where<PX.Objects.TX.Tax.taxID, Equal<Required<PX.Objects.TX.Tax.taxID>>>, And<W>>> pxSelectReadonly2 = new PXSelectReadonly2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<False>, And<TaxRev.taxType, Equal<TaxType.sales>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.use>, And<PX.Objects.TX.Tax.reverseTax, Equal<False>, And<Required<TaxRev.startDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>>>, Where2<Where<PX.Objects.TX.Tax.taxID, Equal<Required<PX.Objects.TX.Tax.taxID>>>, And<W>>>(graph);
    List<object> objectList = new List<object>();
    objectList.Add((object) this.GetDocDate(this.ParentCache(graph), row));
    objectList.Add((object) record.TaxID);
    if (parameters != null)
      objectList.AddRange((IEnumerable<object>) parameters);
    foreach (PXResult<PX.Objects.TX.Tax, TaxRev> pxResult in ((PXSelectBase) pxSelectReadonly2).View.SelectMultiBound(new object[1]
    {
      row
    }, objectList.ToArray()))
    {
      int count = ret.Count;
      while (count > 0 && calculationLevelComparer.Compare(PXResult<T, PX.Objects.TX.Tax, TaxRev>.op_Implicit((PXResult<T, PX.Objects.TX.Tax, TaxRev>) ret[count - 1]), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult)) > 0)
        --count;
      PX.Objects.TX.Tax tax = this.AdjustTaxLevel(graph, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult));
      ret.Insert(count, (object) new PXResult<T, PX.Objects.TX.Tax, TaxRev>(record, tax, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult)));
    }
  }

  protected override void _CalcDocTotals(
    PXCache sender,
    object row,
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    if (((bool?) this.ParentGetValue<ARRegister.released>(sender.Graph)).GetValueOrDefault())
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

  public override bool IsTaxCalculationNeeded(PXCache sender, object row)
  {
    if (row != null && ((bool?) sender.GetValue(row, this._DisableAutomaticTaxCalculation)).GetValueOrDefault())
      return false;
    return sender.Current == null || !((bool?) sender.GetValue(sender.Current, this._DisableAutomaticTaxCalculation)).GetValueOrDefault();
  }

  protected override void IsTaxSavedFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ARInvoice row))
      return;
    Decimal? nullable1 = (Decimal?) sender.GetValue(e.Row, this._CuryTaxTotal);
    Decimal? nullable2 = (Decimal?) sender.GetValue(e.Row, this._CuryWhTaxTotal);
    Decimal? nullable3 = (Decimal?) sender.GetValue(e.Row, this._CuryLineTotal);
    bool flag = this.DocHasInclusiveTax(sender, row);
    if (!(nullable3.GetValueOrDefault() == 0M) && flag)
      return;
    this.CalcDocTotals(sender, (object) row, nullable1.GetValueOrDefault(), 0M, nullable2.GetValueOrDefault(), 0M);
  }

  protected virtual bool DocHasInclusiveTax(PXCache sender, ARInvoice invoice)
  {
    return ((IEnumerable<ARTaxTran>) GraphHelper.RowCast<ARTaxTran>((IEnumerable) PXSelectBase<ARTaxTran, PXSelect<ARTaxTran, Where<ARTaxTran.module, Equal<BatchModule.moduleAR>, And<ARTaxTran.tranType, Equal<Current<ARRegister.docType>>, And<ARTaxTran.refNbr, Equal<Current<ARRegister.refNbr>>, And<TaxTran.isTaxInclusive, Equal<True>>>>>>.Config>.SelectMultiBound(sender.Graph, new object[1]
    {
      (object) invoice
    }, Array.Empty<object>())).ToArray<ARTaxTran>()).Any<ARTaxTran>();
  }

  protected abstract class signedCuryTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTaxAttribute.signedCuryTranAmt>
  {
  }

  protected abstract class curyRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTaxAttribute.curyRetainageAmt>
  {
  }

  protected abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARTaxAttribute.paymentsByLinesAllowed>
  {
  }

  protected abstract class retainageApply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARTaxAttribute.retainageApply>
  {
  }

  protected abstract class isRetainageDocument : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARTaxAttribute.isRetainageDocument>
  {
  }
}
