// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.SalesTax.TaxGraph`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.TX;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Extensions.SalesTax;

public abstract class TaxGraph<TGraph, TPrimary> : TaxBaseGraph<TGraph, TPrimary>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  /// <exclude />
  protected bool _NoSumTotals;

  protected override IEnumerable<ITaxDetail> ManualTaxes(Detail row)
  {
    List<ITaxDetail> taxDetailList = new List<ITaxDetail>();
    foreach (PXResult selectTax in this.SelectTaxes((object) row, PXTaxCheck.RecalcTotals))
    {
      TaxTotal extension = this.TaxTotals.Cache.GetExtension<TaxTotal>(selectTax[0]);
      taxDetailList.Add((ITaxDetail) (TaxItem) extension);
    }
    return (IEnumerable<ITaxDetail>) taxDetailList;
  }

  protected virtual void TaxTotal_CuryTaxableAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    TaxTotal extension = sender.GetExtension<TaxTotal>(e.Row);
    if (extension == null)
      return;
    Decimal num1 = (Decimal) (sender.GetValue(e.Row, typeof (TaxTotal.curyTaxableAmt).Name) ?? (object) 0M);
    Decimal num2 = (Decimal) (e.OldValue ?? (object) 0M);
    TaxCalc? taxCalc1 = this.CurrentDocument.TaxCalc;
    TaxCalc taxCalc2 = TaxCalc.NoCalc;
    if (taxCalc1.GetValueOrDefault() == taxCalc2 & taxCalc1.HasValue || !e.ExternalCall || !(num1 != num2))
      return;
    foreach (object selectTax in this.SelectTaxes<Where<PX.Objects.TX.Tax.taxID, Equal<Required<PX.Objects.TX.Tax.taxID>>>>(sender.Graph, (object) null, PXTaxCheck.RecalcTotals, (object) extension.RefTaxID))
      this.CalculateTaxSumTaxAmt(sender.GetExtension<TaxTotal>(((PXResult) selectTax)[0]), PXResult.Unwrap<PX.Objects.TX.Tax>(selectTax), PXResult.Unwrap<TaxRev>(selectTax));
  }

  protected virtual void TaxTotal_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    TaxTotal extension1 = sender.GetExtension<TaxTotal>(e.Row);
    TaxTotal extension2 = sender.GetExtension<TaxTotal>(e.OldRow);
    if (e.ExternalCall && (!this.TaxTotals.Cache.ObjectsEqual<TaxTotal.curyTaxAmt>((object) extension1, (object) extension2) || !this.TaxTotals.Cache.ObjectsEqual<TaxTotal.curyExpenseAmt>((object) extension1, (object) extension2)))
    {
      PXCache cache = this.Documents.Cache;
      if (cache.Current != null)
      {
        Decimal taxDiscrepancy = this.CalculateTaxDiscrepancy(cache.Current);
        Decimal num = this.Base.FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo().CuryConvBase(taxDiscrepancy);
        this.ParentSetValue<Document.curyTaxRoundDiff>((object) taxDiscrepancy);
        this.ParentSetValue<Document.taxRoundDiff>((object) num);
      }
    }
    TaxCalc? taxCalc1 = this.CurrentDocument.TaxCalc;
    TaxCalc taxCalc2 = TaxCalc.NoCalc;
    if ((taxCalc1.GetValueOrDefault() == taxCalc2 & taxCalc1.HasValue || !e.ExternalCall) && this.CurrentDocument.TaxCalc.GetValueOrDefault() != TaxCalc.ManualCalc || e.OldRow == null || e.Row == null)
      return;
    if (extension2.RefTaxID != extension1.RefTaxID)
      this.VerifyTaxID(extension1, e.ExternalCall);
    if (this.TaxTotals.Cache.ObjectsEqual<TaxTotal.curyTaxAmt>((object) extension1, (object) extension2) && this.TaxTotals.Cache.ObjectsEqual<TaxTotal.curyExpenseAmt>((object) extension1, (object) extension2))
      return;
    this.CalcTotals((object) null, false);
  }

  protected virtual void TaxTotal_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    TaxTotal extension = sender.GetExtension<TaxTotal>(e.Row);
    TaxCalc? taxCalc1 = this.CurrentDocument.TaxCalc;
    TaxCalc taxCalc2 = TaxCalc.NoCalc;
    if ((taxCalc1.GetValueOrDefault() == taxCalc2 & taxCalc1.HasValue || !e.ExternalCall) && this.CurrentDocument.TaxCalc.GetValueOrDefault() != TaxCalc.ManualCalc)
      return;
    this.VerifyTaxID(extension, e.ExternalCall);
  }

  protected virtual void TaxTotal_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    TaxCalc? taxCalc1 = this.CurrentDocument.TaxCalc;
    TaxCalc taxCalc2 = TaxCalc.NoCalc;
    if ((taxCalc1.GetValueOrDefault() == taxCalc2 & taxCalc1.HasValue || !e.ExternalCall) && this.CurrentDocument.TaxCalc.GetValueOrDefault() != TaxCalc.ManualCalc)
      return;
    TaxTotal extension = sender.GetExtension<TaxTotal>(e.Row);
    foreach (PXResult<Detail> detrow in this.Details.Select())
      this.DelOneTax((Detail) detrow, extension.RefTaxID);
    this.CalcTaxes((object) null);
  }

  protected override List<object> SelectDocumentLines(PXGraph graph, object row)
  {
    throw new PXException("Method must be overridden in module-specific tax attributes!");
  }

  protected virtual void VerifyTaxID(TaxTotal row, bool externalCall)
  {
    bool nomatch = false;
    foreach (PXResult<Detail> pxResult in this.Details.Select())
    {
      Detail detail = (Detail) pxResult;
      ITaxDetail taxitem = this.MatchesCategory(detail, (ITaxDetail) (TaxItem) row);
      this.AddOneTax(detail, taxitem);
    }
    object originalRow = this.TaxTotals.Cache.GetMain<TaxTotal>(row);
    int num1;
    if (this.CurrentDocument.TaxCalc.GetValueOrDefault() == TaxCalc.ManualCalc)
    {
      Decimal? taxRate = row.TaxRate;
      Decimal num2 = 0M;
      if (!(taxRate.GetValueOrDefault() == num2 & taxRate.HasValue))
      {
        num1 = !externalCall ? 1 : 0;
        goto label_11;
      }
    }
    num1 = 0;
label_11:
    this._NoSumTotals = num1 != 0;
    PXRowDeleting handler = (PXRowDeleting) ((_sender, _e) => nomatch |= originalRow == _e.Row);
    this.Base.RowDeleting.AddHandler(originalRow.GetType(), handler);
    try
    {
      this.CalcTaxes((object) null);
    }
    finally
    {
      this.Base.RowDeleting.RemoveHandler(originalRow.GetType(), handler);
    }
    this._NoSumTotals = false;
    if (nomatch)
      this.TaxTotals.Cache.RaiseExceptionHandling<TaxTotal.refTaxID>((object) row, (object) row.RefTaxID, (Exception) new PXSetPropertyException("The {0} tax cannot be applied to the document because there are no document lines whose tax category contains the {0} tax.", PXErrorLevel.RowError, new object[1]
      {
        (object) row.RefTaxID
      }));
    this.TaxTotals.Cache.Current = (object) row;
  }

  protected override void CalcTotals(object row, bool CalcTaxes)
  {
    string taxZoneId = this.CurrentDocument?.TaxZoneID;
    bool? importInProgress = (bool?) this.CurrentDocument?.ExternalTaxesImportInProgress;
    bool flag = ExternalTaxBase<PXGraph>.IsExternalTax((PXGraph) this.Base, taxZoneId);
    bool CalcTaxes1 = !this._NoSumTotals & CalcTaxes && !flag && !importInProgress.GetValueOrDefault();
    if (CalcTaxes1 && this.CurrentDocument != null)
      this.ResetDiscrepancy((PXGraph) this.Base);
    base.CalcTotals(row, CalcTaxes1);
  }

  protected override void SetExtCostExt(PXCache sender, object child, Decimal? value)
  {
    throw new PXException("Method must be overridden in module-specific tax attributes!");
  }

  protected override string GetExtCostLabel(PXCache sender, object row)
  {
    throw new PXException("Method must be overridden in module-specific tax attributes!");
  }

  public static Decimal CalcTaxableFromTotalAmount(
    PXCache cache,
    object row,
    string aTaxZoneID,
    string aTaxCategoryID,
    System.DateTime aDocDate,
    Decimal aCuryTotal)
  {
    return new TaxGraph<TGraph, TPrimary>.CalcTaxable(true, TaxGraph<TGraph, TPrimary>.TaxCalcLevelEnforcing.None).CalcTaxableFromTotalAmount(cache, row, aTaxZoneID, aTaxCategoryID, aDocDate, aCuryTotal);
  }

  public static Decimal CalcTaxableFromTotalAmount(
    PXCache cache,
    object row,
    string aTaxZoneID,
    string aTaxCategoryID,
    System.DateTime aDocDate,
    Decimal aCuryTotal,
    bool aSalesOrPurchaseSwitch,
    TaxGraph<TGraph, TPrimary>.TaxCalcLevelEnforcing enforceType)
  {
    return new TaxGraph<TGraph, TPrimary>.CalcTaxable(aSalesOrPurchaseSwitch, enforceType).CalcTaxableFromTotalAmount(cache, row, aTaxZoneID, aTaxCategoryID, aDocDate, aCuryTotal);
  }

  public static Decimal CalcResidualAmt(
    PXCache cache,
    object row,
    string aTaxZoneID,
    string aTaxCategoryID,
    System.DateTime aDocDate,
    string TaxCalcMode,
    Decimal ControlTotalAmt,
    Decimal LinesTotal,
    Decimal TaxTotal)
  {
    Decimal num = 0.0M;
    switch (TaxCalcMode)
    {
      case "G":
        num = TaxAttribute.CalcTaxableFromTotalAmount(cache, row, aTaxZoneID, aTaxCategoryID, aDocDate, ControlTotalAmt - LinesTotal, false, TaxAttribute.TaxCalcLevelEnforcing.EnforceInclusive);
        break;
      case "N":
        num = TaxAttribute.CalcTaxableFromTotalAmount(cache, row, aTaxZoneID, aTaxCategoryID, aDocDate, ControlTotalAmt - LinesTotal - TaxTotal, false, TaxAttribute.TaxCalcLevelEnforcing.EnforceCalcOnItemAmount);
        break;
    }
    return num;
  }

  protected Decimal CalculateTaxDiscrepancy(object row)
  {
    Decimal discrepancy = 0M;
    EnumerableExtensions.ForEach<PXResult>(this.SelectTaxes(row, PXTaxCheck.RecalcTotals).FindAll((Predicate<object>) (taxrow => ((PX.Objects.TX.Tax) ((PXResult) taxrow)[1]).TaxCalcLevel == "0")).Cast<PXResult>(), (System.Action<PXResult>) (taxrow =>
    {
      object data = taxrow[0];
      PXCache cach = this.Base.Caches[data.GetType()];
      Decimal num1 = (Decimal) cach.GetValue(data, this.GetTaxDetailMapping().CuryTaxAmt.Name);
      Decimal num2 = (Decimal) cach.GetValue(data, this.GetTaxDetailMapping().CuryExpenseAmt.Name);
      TaxTotal taxSum = this.CalculateTaxSum((object) taxrow, row);
      if (taxSum == null)
        return;
      PXCache cache = this.TaxTotals.Cache;
      Decimal num3 = discrepancy;
      Decimal num4 = num1 + num2;
      Decimal? nullable = taxSum.CuryTaxAmt;
      Decimal num5 = nullable.Value;
      Decimal num6 = num4 - num5;
      nullable = taxSum.CuryExpenseAmt;
      Decimal num7 = nullable.Value;
      Decimal num8 = num6 - num7;
      discrepancy = num3 + num8;
    }));
    return discrepancy;
  }

  private void ResetDiscrepancy(PXGraph graph)
  {
    this.ParentSetValue<Document.curyTaxRoundDiff>((object) 0M);
    this.ParentSetValue<Document.taxRoundDiff>((object) 0M);
  }

  protected override Decimal? GetTaxableAmt(object row)
  {
    throw new PXException("Method must be overridden in module-specific tax attributes!");
  }

  protected override Decimal? GetTaxAmt(object row)
  {
    throw new PXException("Method must be overridden in module-specific tax attributes!");
  }

  public enum TaxCalcLevelEnforcing
  {
    None,
    EnforceCalcOnItemAmount,
    EnforceInclusive,
  }

  public class CalcTaxable
  {
    private bool _aSalesOrPurchaseSwitch;
    private TaxGraph<TGraph, TPrimary>.TaxCalcLevelEnforcing _enforcing;

    public CalcTaxable(
      bool aSalesOrPurchaseSwitch,
      TaxGraph<TGraph, TPrimary>.TaxCalcLevelEnforcing enforceType)
    {
      this._aSalesOrPurchaseSwitch = aSalesOrPurchaseSwitch;
      this._enforcing = enforceType;
    }

    public Decimal CalcTaxableFromTotalAmount(
      PXCache cache,
      object row,
      string aTaxZoneID,
      string aTaxCategoryID,
      System.DateTime aDocDate,
      Decimal aCuryTotal)
    {
      IComparer<PX.Objects.TX.Tax> taxComparer = this.GetTaxComparer();
      ExceptionExtensions.ThrowOnNull<IComparer<PX.Objects.TX.Tax>>(taxComparer, "taxComparer", (string) null);
      PXGraph graph = cache.Graph;
      List<TaxZoneDet> applicableTaxList = this.GetApplicableTaxList(graph, aTaxZoneID, aTaxCategoryID, aDocDate);
      Dictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> taxRevisionList = this.GetTaxRevisionList(graph, aDocDate);
      List<PXResult<PX.Objects.TX.Tax, TaxRev>> pxResultList = new List<PXResult<PX.Objects.TX.Tax, TaxRev>>(applicableTaxList.Count);
      foreach (TaxZoneDet taxZoneDet in applicableTaxList)
      {
        PXResult<PX.Objects.TX.Tax, TaxRev> pxResult;
        if (taxRevisionList.TryGetValue(taxZoneDet.TaxID, out pxResult))
        {
          int count = pxResultList.Count;
          while (count > 0 && taxComparer.Compare((PX.Objects.TX.Tax) pxResultList[count - 1], (PX.Objects.TX.Tax) pxResult) > 0)
            --count;
          pxResultList.Insert(count, new PXResult<PX.Objects.TX.Tax, TaxRev>((PX.Objects.TX.Tax) pxResult, (TaxRev) pxResult));
        }
      }
      Decimal num1 = 0M;
      Decimal num2 = 0M;
      Decimal num3 = 0M;
      foreach (PXResult<PX.Objects.TX.Tax, TaxRev> pxResult in pxResultList)
      {
        PX.Objects.TX.Tax tax = (PX.Objects.TX.Tax) pxResult;
        TaxRev taxRev = (TaxRev) pxResult;
        Decimal num4 = tax.ReverseTax.GetValueOrDefault() ? -1M : 1M;
        switch (tax.TaxCalcLevel)
        {
          case "0":
            num1 += num4 * taxRev.TaxRate.Value;
            continue;
          case "1":
            num2 += num4 * taxRev.TaxRate.Value;
            continue;
          case "2":
            num3 += num4 * taxRev.TaxRate.Value;
            continue;
          default:
            continue;
        }
      }
      Decimal num5 = cache.Graph.FindImplementation<IPXCurrencyHelper>().RoundCury(aCuryTotal / (1M + num3 / 100M));
      Decimal num6 = cache.Graph.FindImplementation<IPXCurrencyHelper>().RoundCury(num5 / (1M + (num2 + num1) / 100M));
      Decimal num7 = 0M;
      Decimal num8 = 0M;
      foreach (PXResult<PX.Objects.TX.Tax, TaxRev> pxResult in pxResultList)
      {
        PX.Objects.TX.Tax tax = (PX.Objects.TX.Tax) pxResult;
        TaxRev taxRev = (TaxRev) pxResult;
        Decimal num9 = tax.ReverseTax.GetValueOrDefault() ? -1M : 1M;
        Decimal? taxRate;
        switch (tax.TaxCalcLevel)
        {
          case "1":
            Decimal num10 = num7;
            Decimal num11 = num9;
            IPXCurrencyHelper implementation1 = cache.Graph.FindImplementation<IPXCurrencyHelper>();
            Decimal num12 = num6;
            taxRate = taxRev.TaxRate;
            Decimal valueOrDefault1 = (taxRate.HasValue ? new Decimal?(num12 * taxRate.GetValueOrDefault() / 100M) : new Decimal?()).GetValueOrDefault();
            Decimal num13 = implementation1.RoundCury(valueOrDefault1);
            Decimal num14 = num11 * num13;
            num7 = num10 + num14;
            continue;
          case "2":
            Decimal num15 = num8;
            Decimal num16 = num9;
            IPXCurrencyHelper implementation2 = cache.Graph.FindImplementation<IPXCurrencyHelper>();
            Decimal num17 = num5;
            taxRate = taxRev.TaxRate;
            Decimal valueOrDefault2 = (taxRate.HasValue ? new Decimal?(num17 * taxRate.GetValueOrDefault() / 100M) : new Decimal?()).GetValueOrDefault();
            Decimal num18 = implementation2.RoundCury(valueOrDefault2);
            Decimal num19 = num16 * num18;
            num8 = num15 + num19;
            continue;
          default:
            continue;
        }
      }
      return aCuryTotal - num7 - num8;
    }

    private List<TaxZoneDet> GetApplicableTaxList(
      PXGraph aGraph,
      string aTaxZoneID,
      string aTaxCategoryID,
      System.DateTime aDocDate)
    {
      List<TaxZoneDet> applicableTaxList = new List<TaxZoneDet>();
      HashSet<string> stringSet = new HashSet<string>();
      foreach (PXResult<TaxZoneDet, PX.Objects.TX.TaxCategory, TaxRev, TaxCategoryDet> pxResult in PXSelectBase<TaxZoneDet, PXSelectJoin<TaxZoneDet, CrossJoin<PX.Objects.TX.TaxCategory, InnerJoin<TaxRev, On<TaxRev.taxID, Equal<TaxZoneDet.taxID>>, LeftJoin<TaxCategoryDet, On<TaxCategoryDet.taxID, Equal<TaxZoneDet.taxID>, And<TaxCategoryDet.taxCategoryID, Equal<PX.Objects.TX.TaxCategory.taxCategoryID>>>>>>, Where<TaxZoneDet.taxZoneID, Equal<Required<TaxZoneDet.taxZoneID>>, And<PX.Objects.TX.TaxCategory.taxCategoryID, Equal<Required<PX.Objects.TX.TaxCategory.taxCategoryID>>, And<Required<TaxRev.startDate>, Between<TaxRev.startDate, TaxRev.endDate>, And<TaxRev.outdated, Equal<False>, And<Where<PX.Objects.TX.TaxCategory.taxCatFlag, Equal<False>, And<TaxCategoryDet.taxCategoryID, IsNotNull, Or<PX.Objects.TX.TaxCategory.taxCatFlag, Equal<True>, And<TaxCategoryDet.taxCategoryID, IsNull>>>>>>>>>>.Config>.Select(aGraph, (object) aTaxZoneID, (object) aTaxCategoryID, (object) aDocDate))
      {
        TaxZoneDet taxZoneDet = (TaxZoneDet) pxResult;
        if (!stringSet.Contains(taxZoneDet.TaxID))
        {
          stringSet.Add(taxZoneDet.TaxID);
          applicableTaxList.Add(taxZoneDet);
        }
      }
      return applicableTaxList;
    }

    private Dictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> GetTaxRevisionList(
      PXGraph aGraph,
      System.DateTime aDocDate)
    {
      PXSelectBase<PX.Objects.TX.Tax> pxSelectBase = !this._aSalesOrPurchaseSwitch ? (PXSelectBase<PX.Objects.TX.Tax>) new PXSelectReadonly2<PX.Objects.TX.Tax, InnerJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<False>, And<PX.Objects.TX.Tax.directTax, Equal<False>, And2<Where<TaxRev.taxType, Equal<TaxType.purchase>, And<PX.Objects.TX.Tax.reverseTax, Equal<False>, Or<TaxRev.taxType, Equal<TaxType.sales>, And<Where<PX.Objects.TX.Tax.reverseTax, Equal<True>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.use>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.withholding>>>>>>>>, And<Required<TaxRev.startDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>>(aGraph) : (PXSelectBase<PX.Objects.TX.Tax>) new PXSelectReadonly2<PX.Objects.TX.Tax, InnerJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<False>, And<TaxRev.taxType, Equal<TaxType.sales>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.use>, And<PX.Objects.TX.Tax.reverseTax, Equal<False>, And<PX.Objects.TX.Tax.directTax, Equal<False>, And<Current<GLTranDoc.tranDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>>>>>(aGraph);
      Dictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> taxRevisionList = new Dictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>>();
      foreach (PXResult<PX.Objects.TX.Tax, TaxRev> pxResult in pxSelectBase.Select((object) aDocDate))
      {
        taxRevisionList[((PX.Objects.TX.Tax) pxResult).TaxID] = pxResult;
        PX.Objects.TX.Tax tax = (PX.Objects.TX.Tax) pxResult;
        if (tax.TaxCalcType == "I")
        {
          switch (this._enforcing)
          {
            case TaxGraph<TGraph, TPrimary>.TaxCalcLevelEnforcing.EnforceCalcOnItemAmount:
              if (tax.TaxCalcLevel == "0")
              {
                tax.TaxCalcLevel = "1";
                continue;
              }
              continue;
            case TaxGraph<TGraph, TPrimary>.TaxCalcLevelEnforcing.EnforceInclusive:
              if (tax.TaxCalcLevel == "1")
              {
                tax.TaxCalcLevel = "0";
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
      return taxRevisionList;
    }

    protected virtual IComparer<PX.Objects.TX.Tax> GetTaxComparer()
    {
      return (IComparer<PX.Objects.TX.Tax>) TaxByCalculationLevelComparer.Instance;
    }
  }
}
