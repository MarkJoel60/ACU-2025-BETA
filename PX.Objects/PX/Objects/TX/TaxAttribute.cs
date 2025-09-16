// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.TemporaryHelpers;
using PX.Objects.GL;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.TX;

public abstract class TaxAttribute : TaxBaseAttribute
{
  protected 
  #nullable disable
  string _DocCuryTaxAmt = "CuryTaxAmt";
  protected string _CuryOrigDocAmt = nameof (CuryOrigDocAmt);
  protected string _CuryTaxRoundDiff = nameof (CuryTaxRoundDiff);
  protected string _TaxRoundDiff = nameof (TaxRoundDiff);
  protected bool _NoSumTotals;

  public Type DocCuryTaxAmt
  {
    set => this._DocCuryTaxAmt = value.Name;
    get => (Type) null;
  }

  public Type CuryOrigDocAmt
  {
    set => this._CuryOrigDocAmt = value.Name;
    get => (Type) null;
  }

  public Type CuryTaxRoundDiff
  {
    set => this._CuryTaxRoundDiff = value.Name;
    get => (Type) null;
  }

  public Type TaxRoundDiff
  {
    set => this._TaxRoundDiff = value.Name;
    get => (Type) null;
  }

  public TaxAttribute(
    Type ParentType,
    Type TaxType,
    Type TaxSumType,
    Type CalcMode = null,
    Type parentBranchIDField = null)
    : base(ParentType, TaxType, TaxSumType, CalcMode, parentBranchIDField)
  {
  }

  protected override IEnumerable<ITaxDetail> ManualTaxes(PXCache sender, object row)
  {
    List<ITaxDetail> taxDetailList = new List<ITaxDetail>();
    foreach (PXResult selectTax in this.SelectTaxes(sender, row, PXTaxCheck.RecalcTotals))
      taxDetailList.Add((ITaxDetail) selectTax[0]);
    return (IEnumerable<ITaxDetail>) taxDetailList;
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.RowInsertedEvents rowInserted = sender.Graph.RowInserted;
    Type taxSumType1 = this._TaxSumType;
    TaxAttribute taxAttribute1 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted = new PXRowInserted((object) taxAttribute1, __vmethodptr(taxAttribute1, Tax_RowInserted));
    rowInserted.AddHandler(taxSumType1, pxRowInserted);
    PXGraph.RowDeletedEvents rowDeleted = sender.Graph.RowDeleted;
    Type taxSumType2 = this._TaxSumType;
    TaxAttribute taxAttribute2 = this;
    // ISSUE: virtual method pointer
    PXRowDeleted pxRowDeleted = new PXRowDeleted((object) taxAttribute2, __vmethodptr(taxAttribute2, Tax_RowDeleted));
    rowDeleted.AddHandler(taxSumType2, pxRowDeleted);
    PXGraph.RowUpdatedEvents rowUpdated = sender.Graph.RowUpdated;
    Type taxSumType3 = this._TaxSumType;
    TaxAttribute taxAttribute3 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) taxAttribute3, __vmethodptr(taxAttribute3, Tax_RowUpdated));
    rowUpdated.AddHandler(taxSumType3, pxRowUpdated);
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type taxSumType4 = this._TaxSumType;
    string curyTaxableAmt = this._CuryTaxableAmt;
    TaxAttribute taxAttribute4 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) taxAttribute4, __vmethodptr(taxAttribute4, Tax_CuryTaxableAmt_FieldUpdated));
    fieldUpdated.AddHandler(taxSumType4, curyTaxableAmt, pxFieldUpdated);
    PXGraph.RowSelectedEvents rowSelected = sender.Graph.RowSelected;
    Type taxSumType5 = this._TaxSumType;
    TaxAttribute taxAttribute5 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) taxAttribute5, __vmethodptr(taxAttribute5, Tax_RowSelected));
    rowSelected.AddHandler(taxSumType5, pxRowSelected);
  }

  protected virtual void Tax_CuryTaxableAmt_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is TaxDetail row))
      return;
    Decimal num1 = (Decimal) (sender.GetValue(e.Row, this._CuryTaxableAmt) ?? (object) 0M);
    Decimal num2 = (Decimal) (e.OldValue ?? (object) 0M);
    if (this._TaxCalc == TaxCalc.NoCalc || !e.ExternalCall || !(num1 != num2))
      return;
    foreach (object selectTax in this.SelectTaxes<Where<Tax.taxID, Equal<Required<Tax.taxID>>>>(sender.Graph, (object) null, PXTaxCheck.RecalcTotals, (object) row.TaxID))
    {
      object obj;
      TaxDetail taxdet = (TaxDetail) ((PXResult) (obj = selectTax))[0];
      Tax tax = PXResult.Unwrap<Tax>(obj);
      TaxRev taxrev = PXResult.Unwrap<TaxRev>(obj);
      this.CalculateTaxSumTaxAmt(sender, taxdet, tax, taxrev);
    }
  }

  protected virtual void Tax_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    TaxDetail row = e.Row as TaxDetail;
    TaxDetail oldRow = e.OldRow as TaxDetail;
    if (e.ExternalCall && this.IsTaxRowAmountUpdated(sender, e))
    {
      PXCache sender1 = this.ParentCache(sender.Graph);
      if (sender1.Current != null)
      {
        Decimal taxDiscrepancy = this.CalculateTaxDiscrepancy(sender1, sender1.Current);
        Decimal baseval;
        MultiCurrencyCalculator.CuryConvBase(this.ParentCache(sender.Graph), this.ParentRow(sender.Graph), taxDiscrepancy, out baseval);
        this.ParentSetValue(sender.Graph, this._CuryTaxRoundDiff, (object) taxDiscrepancy);
        this.ParentSetValue(sender.Graph, this._TaxRoundDiff, (object) baseval);
      }
    }
    if ((this._TaxCalc == TaxCalc.NoCalc || !e.ExternalCall) && this._TaxCalc != TaxCalc.ManualCalc || e.OldRow == null || e.Row == null)
      return;
    if (oldRow.TaxID != row.TaxID || this.IsTaxRowQuantityUpdated(sender, e))
    {
      PXCache cach1 = sender.Graph.Caches[this._ChildType];
      PXCache cach2 = sender.Graph.Caches[this._TaxType];
      foreach (object obj in this.ChildSelect(cach1, e.Row))
      {
        ITaxDetail taxrow = this.MatchesCategory(cach1, obj, (ITaxDetail) e.OldRow);
        if (taxrow != null)
          this.DelOneTax(cach2, obj, (object) taxrow);
      }
      this.VerifyTaxID(sender, e.Row, e.ExternalCall);
    }
    if (!this.IsTaxRowAmountUpdated(sender, e))
      return;
    this.CalcTotals(sender.Graph.Caches[this._ChildType], (object) null, false);
  }

  protected virtual void Tax_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!this._IncludeDirectTaxLine)
      return;
    TaxDetail row = e.Row as TaxDetail;
    Tax tax = Tax.PK.Find(sender.Graph, row?.TaxID);
    if ((tax != null ? (tax.DirectTax.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    PXUIFieldAttribute.SetEnabled(sender, (object) row, this._TaxID, false);
    PXUIFieldAttribute.SetEnabled(sender, (object) row, this._CuryTaxableAmt, false);
    PXUIFieldAttribute.SetEnabled(sender, (object) row, this._CuryTaxAmt, false);
    PXUIFieldAttribute.SetEnabled(sender, (object) row, this._CuryExpenseAmt, false);
  }

  protected virtual bool IsTaxRowAmountUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    Decimal? nullable1 = sender.GetValue(e.OldRow, this._CuryTaxAmt) as Decimal?;
    Decimal? nullable2 = sender.GetValue(e.Row, this._CuryTaxAmt) as Decimal?;
    Decimal? nullable3 = sender.GetValue(e.OldRow, this._CuryExpenseAmt) as Decimal?;
    Decimal? nullable4 = sender.GetValue(e.Row, this._CuryExpenseAmt) as Decimal?;
    Decimal? nullable5 = nullable1;
    Decimal? nullable6 = nullable2;
    if (!(nullable5.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable5.HasValue == nullable6.HasValue))
      return true;
    Decimal? nullable7 = nullable3;
    Decimal? nullable8 = nullable4;
    return !(nullable7.GetValueOrDefault() == nullable8.GetValueOrDefault() & nullable7.HasValue == nullable8.HasValue);
  }

  protected virtual bool IsTaxRowQuantityUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    Decimal? nullable1 = sender.GetValue(e.OldRow, this.TaxableQtyFieldNameForTaxDetail) as Decimal?;
    Decimal? nullable2 = sender.GetValue(e.Row, this.TaxableQtyFieldNameForTaxDetail) as Decimal?;
    Decimal? nullable3 = nullable1;
    Decimal? nullable4 = nullable2;
    return !(nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue);
  }

  protected virtual void Tax_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if ((this._TaxCalc == TaxCalc.NoCalc || !e.ExternalCall) && this._TaxCalc != TaxCalc.ManualCalc)
      return;
    this.VerifyTaxID(sender, e.Row, e.ExternalCall);
  }

  protected virtual void VerifyTaxID(PXCache sender, object row, bool externalCall)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    TaxAttribute.\u003C\u003Ec__DisplayClass30_0 cDisplayClass300 = new TaxAttribute.\u003C\u003Ec__DisplayClass30_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass300.row = row;
    PXCache cach1 = sender.Graph.Caches[this._ChildType];
    PXCache cach2 = sender.Graph.Caches[this._TaxType];
    // ISSUE: reference to a compiler-generated field
    TaxDetail row1 = (TaxDetail) cDisplayClass300.row;
    // ISSUE: reference to a compiler-generated field
    foreach (object obj in this.ChildSelect(cach1, cDisplayClass300.row))
    {
      ITaxDetail taxitem = this.MatchesCategory(cach1, obj, (ITaxDetail) row1);
      this.AddOneTax(cach2, obj, taxitem);
    }
    int num1;
    if (this._TaxCalc == TaxCalc.ManualCalc)
    {
      Decimal? taxRate = row1.TaxRate;
      Decimal num2 = 0M;
      if (!(taxRate.GetValueOrDefault() == num2 & taxRate.HasValue))
      {
        num1 = !externalCall ? 1 : 0;
        goto label_9;
      }
    }
    num1 = 0;
label_9:
    this._NoSumTotals = num1 != 0;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass300.nomatch = false;
    // ISSUE: method pointer
    sender.Graph.RowDeleting.AddHandler(this._TaxSumType, new PXRowDeleting((object) cDisplayClass300, __methodptr(\u003CVerifyTaxID\u003Eg__del\u007C0)));
    try
    {
      this.CalcTaxes(cach1, (object) null);
    }
    finally
    {
      // ISSUE: method pointer
      sender.Graph.RowDeleting.RemoveHandler(this._TaxSumType, new PXRowDeleting((object) cDisplayClass300, __methodptr(\u003CVerifyTaxID\u003Eg__del\u007C0)));
    }
    this._NoSumTotals = false;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass300.nomatch)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      sender.RaiseExceptionHandling("TaxID", cDisplayClass300.row, (object) row1.TaxID, (Exception) new PXSetPropertyException((IBqlTable) cDisplayClass300.row, "The {0} tax cannot be applied to the document because there are no document lines whose tax category contains the {0} tax.", (PXErrorLevel) 5, new object[1]
      {
        (object) row1.TaxID
      }));
    }
    // ISSUE: reference to a compiler-generated field
    sender.Current = cDisplayClass300.row;
  }

  protected virtual void Tax_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if ((this._TaxCalc == TaxCalc.NoCalc || !e.ExternalCall) && this._TaxCalc != TaxCalc.ManualCalc)
      return;
    PXCache cach1 = sender.Graph.Caches[this._ChildType];
    PXCache cach2 = sender.Graph.Caches[this._TaxType];
    foreach (object detrow in this.ChildSelect(cach1, e.Row))
      this.DelOneTax(cach2, detrow, e.Row);
    this.CalcTaxes(cach1, (object) null);
  }

  protected override List<object> SelectDocumentLines(PXGraph graph, object row)
  {
    throw new PXException("Method must be overridden in module-specific tax attributes!");
  }

  protected override void CalcTotals(PXCache sender, object row, bool CalcTaxes)
  {
    string taxZone = this.GetTaxZone(sender, row);
    bool flag = ExternalTaxBase<PXGraph>.IsExternalTax(sender.Graph, taxZone);
    bool CalcTaxes1 = !this._NoSumTotals & CalcTaxes && !flag;
    if (CalcTaxes1 && (this.ParentCache(sender.Graph).Current != null || this._ParentRow != null))
      this.ResetDiscrepancy(sender.Graph);
    base.CalcTotals(sender, row, CalcTaxes1);
  }

  protected override void SetExtCostExt(PXCache sender, object child, Decimal? value)
  {
    throw new PXException("Method must be overridden in module-specific tax attributes!");
  }

  protected override string GetExtCostLabel(PXCache sender, object row)
  {
    throw new PXException("Method must be overridden in module-specific tax attributes!");
  }

  protected override Decimal? GetTaxableAmt(PXCache sender, object row)
  {
    throw new PXException("Method must be overridden in module-specific tax attributes!");
  }

  protected override Decimal? GetTaxAmt(PXCache sender, object row)
  {
    throw new PXException("Method must be overridden in module-specific tax attributes!");
  }

  public static Decimal CalcTaxableFromTotalAmount(
    PXCache cache,
    object row,
    string aTaxZoneID,
    string aTaxCategoryID,
    DateTime aDocDate,
    Decimal aCuryTotal)
  {
    return new TaxAttribute.CalcTaxable(true, TaxAttribute.TaxCalcLevelEnforcing.None).CalcTaxableFromTotalAmount(cache, row, aTaxZoneID, aTaxCategoryID, aDocDate, aCuryTotal);
  }

  public static Decimal CalcTaxableFromTotalAmount(
    PXCache cache,
    object row,
    string aTaxZoneID,
    string aTaxCategoryID,
    DateTime aDocDate,
    Decimal aCuryTotal,
    bool aSalesOrPurchaseSwitch,
    TaxAttribute.TaxCalcLevelEnforcing enforceType)
  {
    return new TaxAttribute.CalcTaxable(aSalesOrPurchaseSwitch, enforceType).CalcTaxableFromTotalAmount(cache, row, aTaxZoneID, aTaxCategoryID, aDocDate, aCuryTotal);
  }

  public static Decimal CalcTaxableFromTotalAmount(
    PXCache cache,
    object row,
    string aTaxZoneID,
    string aTaxCategoryID,
    DateTime aDocDate,
    Decimal aCuryTotal,
    bool aSalesOrPurchaseSwitch,
    TaxAttribute.TaxCalcLevelEnforcing enforceType,
    string taxCalcMode)
  {
    return new TaxAttribute.CalcTaxable(aSalesOrPurchaseSwitch, enforceType, taxCalcMode).CalcTaxableFromTotalAmount(cache, row, aTaxZoneID, aTaxCategoryID, aDocDate, aCuryTotal);
  }

  public static Decimal CalcTaxableFromTotalAmount(
    PXCache cache,
    object row,
    string aTaxZoneID,
    string aTaxCategoryID,
    DateTime aDocDate,
    Decimal aCuryTotal,
    int customPrecision)
  {
    return new TaxAttribute.CalcTaxable(true, TaxAttribute.TaxCalcLevelEnforcing.None, customPrecision).CalcTaxableFromTotalAmount(cache, row, aTaxZoneID, aTaxCategoryID, aDocDate, aCuryTotal);
  }

  public static Decimal CalcTaxableFromTotalAmount(
    PXCache cache,
    object row,
    string aTaxZoneID,
    string aTaxCategoryID,
    DateTime aDocDate,
    Decimal aCuryTotal,
    bool aSalesOrPurchaseSwitch,
    TaxAttribute.TaxCalcLevelEnforcing enforceType,
    int customPrecision)
  {
    return new TaxAttribute.CalcTaxable(aSalesOrPurchaseSwitch, enforceType, customPrecision).CalcTaxableFromTotalAmount(cache, row, aTaxZoneID, aTaxCategoryID, aDocDate, aCuryTotal);
  }

  public static Decimal CalcResidualAmt(
    PXCache cache,
    object row,
    string aTaxZoneID,
    string aTaxCategoryID,
    DateTime aDocDate,
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

  protected Decimal CalculateTaxDiscrepancy(PXCache sender, object row)
  {
    Decimal discrepancy = 0M;
    this.SelectTaxes(sender, row, PXTaxCheck.RecalcTotals).FindAll((Predicate<object>) (taxrow => ((Tax) ((PXResult) taxrow)[1]).TaxCalcLevel == "0")).ForEach((Action<object>) (taxrow =>
    {
      TaxDetail taxDetail = (TaxDetail) ((PXResult) taxrow)[0];
      TaxDetail taxSum = this.CalculateTaxSum(sender, taxrow, row);
      if (taxSum == null)
        return;
      PXCache cach = sender.Graph.Caches[this._TaxSumType];
      discrepancy += (Decimal) cach.GetValue((object) taxDetail, this._CuryTaxAmt) + taxDetail.CuryExpenseAmt.Value - (Decimal) cach.GetValue((object) taxSum, this._CuryTaxAmt) - taxSum.CuryExpenseAmt.Value;
    }));
    return discrepancy;
  }

  private void ResetDiscrepancy(PXGraph graph)
  {
    this.ParentSetValue(graph, this._CuryTaxRoundDiff, (object) 0M);
    this.ParentSetValue(graph, this._TaxRoundDiff, (object) 0M);
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxAttribute.curyTaxableAmt>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxAttribute.curyTaxAmt>
  {
  }

  public abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxAttribute.curyExpenseAmt>
  {
  }

  public abstract class curyExemptedAmt : IBqlField, IBqlOperand
  {
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
    private TaxAttribute.TaxCalcLevelEnforcing _enforcing;
    private int? _precision;
    private string _taxCalcMode;

    public CalcTaxable(bool aSalesOrPurchaseSwitch, TaxAttribute.TaxCalcLevelEnforcing enforceType)
    {
      this._aSalesOrPurchaseSwitch = aSalesOrPurchaseSwitch;
      this._enforcing = enforceType;
    }

    public CalcTaxable(
      bool aSalesOrPurchaseSwitch,
      TaxAttribute.TaxCalcLevelEnforcing enforceType,
      string taxCalcMode)
      : this(aSalesOrPurchaseSwitch, enforceType)
    {
      this._taxCalcMode = taxCalcMode;
    }

    public CalcTaxable(
      bool aSalesOrPurchaseSwitch,
      TaxAttribute.TaxCalcLevelEnforcing enforceType,
      int precision)
    {
      this._aSalesOrPurchaseSwitch = aSalesOrPurchaseSwitch;
      this._enforcing = enforceType;
      this._precision = new int?(precision);
    }

    public Decimal CalcTaxableFromTotalAmount(
      PXCache cache,
      object row,
      HashSet<string> taxList,
      DateTime aDocDate,
      Decimal aCuryTotal)
    {
      IComparer<Tax> taxComparer = this.GetTaxComparer();
      ExceptionExtensions.ThrowOnNull<IComparer<Tax>>(taxComparer, "taxComparer", (string) null);
      Dictionary<string, PXResult<Tax, TaxRev>> taxRevisionList = this.GetTaxRevisionList(cache.Graph, aDocDate);
      List<PXResult<Tax, TaxRev>> pxResultList = new List<PXResult<Tax, TaxRev>>(taxList.Count);
      foreach (string tax1 in taxList)
      {
        PXResult<Tax, TaxRev> pxResult;
        if (taxRevisionList.TryGetValue(tax1, out pxResult))
        {
          int count = pxResultList.Count;
          while (count > 0 && taxComparer.Compare(PXResult<Tax, TaxRev>.op_Implicit(pxResultList[count - 1]), PXResult<Tax, TaxRev>.op_Implicit(pxResult)) > 0)
            --count;
          Tax tax2 = PXResult<Tax, TaxRev>.op_Implicit(pxResult);
          if (this._taxCalcMode != null && tax2.TaxCalcLevel != "2")
          {
            switch (this._taxCalcMode)
            {
              case "N":
                tax2.TaxCalcLevel = "1";
                break;
              case "G":
                tax2.TaxCalcLevel = "0";
                break;
            }
          }
          pxResultList.Insert(count, new PXResult<Tax, TaxRev>(tax2, PXResult<Tax, TaxRev>.op_Implicit(pxResult)));
        }
      }
      Decimal num1 = 0M;
      Decimal num2 = 0M;
      Decimal num3 = 0M;
      foreach (PXResult<Tax, TaxRev> pxResult in pxResultList)
      {
        Tax tax = PXResult<Tax, TaxRev>.op_Implicit(pxResult);
        TaxRev taxRev = PXResult<Tax, TaxRev>.op_Implicit(pxResult);
        Decimal num4 = tax.ReverseTax.GetValueOrDefault() ? -1M : 1M;
        if (tax.TaxType == "Q")
        {
          PXTrace.WriteError("This operation is not supported for per-unit taxes.");
          throw new PXException("This operation is not supported for per-unit taxes.");
        }
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
      Decimal num5 = MultiCurrencyCalculator.RoundCury(cache, row, aCuryTotal / (1M + num3 / 100M), this._precision);
      Decimal num6 = MultiCurrencyCalculator.RoundCury(cache, row, num5 / (1M + (num2 + num1) / 100M), this._precision);
      Decimal num7 = 0M;
      Decimal num8 = 0M;
      foreach (PXResult<Tax, TaxRev> pxResult in pxResultList)
      {
        Tax tax = PXResult<Tax, TaxRev>.op_Implicit(pxResult);
        TaxRev taxRev = PXResult<Tax, TaxRev>.op_Implicit(pxResult);
        Decimal num9 = tax.ReverseTax.GetValueOrDefault() ? -1M : 1M;
        Decimal? taxRate;
        switch (tax.TaxCalcLevel)
        {
          case "1":
            Decimal num10 = num7;
            Decimal num11 = num9;
            PXCache sender1 = cache;
            object row1 = row;
            Decimal num12 = num6;
            taxRate = taxRev.TaxRate;
            Decimal valueOrDefault1 = (taxRate.HasValue ? new Decimal?(num12 * taxRate.GetValueOrDefault() / 100M) : new Decimal?()).GetValueOrDefault();
            int? precision1 = this._precision;
            Decimal num13 = MultiCurrencyCalculator.RoundCury(sender1, row1, valueOrDefault1, precision1);
            Decimal num14 = num11 * num13;
            num7 = num10 + num14;
            continue;
          case "2":
            Decimal num15 = num8;
            Decimal num16 = num9;
            PXCache sender2 = cache;
            object row2 = row;
            Decimal num17 = num5;
            taxRate = taxRev.TaxRate;
            Decimal valueOrDefault2 = (taxRate.HasValue ? new Decimal?(num17 * taxRate.GetValueOrDefault() / 100M) : new Decimal?()).GetValueOrDefault();
            int? precision2 = this._precision;
            Decimal num18 = MultiCurrencyCalculator.RoundCury(sender2, row2, valueOrDefault2, precision2);
            Decimal num19 = num16 * num18;
            num8 = num15 + num19;
            continue;
          default:
            continue;
        }
      }
      return aCuryTotal - num7 - num8;
    }

    public Decimal CalcTaxableFromTotalAmount(
      PXCache cache,
      object row,
      string aTaxZoneID,
      string aTaxCategoryID,
      DateTime aDocDate,
      Decimal aCuryTotal)
    {
      HashSet<string> applicableTaxList = this.GetApplicableTaxList(cache.Graph, aTaxZoneID, aTaxCategoryID, aDocDate);
      return this.CalcTaxableFromTotalAmount(cache, row, applicableTaxList, aDocDate, aCuryTotal);
    }

    private HashSet<string> GetApplicableTaxList(
      PXGraph aGraph,
      string aTaxZoneID,
      string aTaxCategoryID,
      DateTime aDocDate)
    {
      HashSet<string> applicableTaxList = new HashSet<string>();
      foreach (PXResult<TaxZoneDet, TaxCategory, TaxRev, TaxCategoryDet> pxResult in PXSelectBase<TaxZoneDet, PXSelectJoin<TaxZoneDet, CrossJoin<TaxCategory, InnerJoin<TaxRev, On<TaxRev.taxID, Equal<TaxZoneDet.taxID>>, LeftJoin<TaxCategoryDet, On<TaxCategoryDet.taxID, Equal<TaxZoneDet.taxID>, And<TaxCategoryDet.taxCategoryID, Equal<TaxCategory.taxCategoryID>>>>>>, Where<TaxZoneDet.taxZoneID, Equal<Required<TaxZoneDet.taxZoneID>>, And<TaxCategory.taxCategoryID, Equal<Required<TaxCategory.taxCategoryID>>, And<Required<TaxRev.startDate>, Between<TaxRev.startDate, TaxRev.endDate>, And<TaxRev.outdated, Equal<False>, And<Where<TaxCategory.taxCatFlag, Equal<False>, And<TaxCategoryDet.taxCategoryID, IsNotNull, Or<TaxCategory.taxCatFlag, Equal<True>, And<TaxCategoryDet.taxCategoryID, IsNull>>>>>>>>>>.Config>.Select(aGraph, new object[3]
      {
        (object) aTaxZoneID,
        (object) aTaxCategoryID,
        (object) aDocDate
      }))
      {
        TaxZoneDet taxZoneDet = PXResult<TaxZoneDet, TaxCategory, TaxRev, TaxCategoryDet>.op_Implicit(pxResult);
        if (!applicableTaxList.Contains(taxZoneDet.TaxID))
          applicableTaxList.Add(taxZoneDet.TaxID);
      }
      return applicableTaxList;
    }

    private Dictionary<string, PXResult<Tax, TaxRev>> GetTaxRevisionList(
      PXGraph aGraph,
      DateTime aDocDate)
    {
      PXSelectBase<Tax> pxSelectBase = !this._aSalesOrPurchaseSwitch ? (PXSelectBase<Tax>) new PXSelectReadonly2<Tax, InnerJoin<TaxRev, On<TaxRev.taxID, Equal<Tax.taxID>, And<TaxRev.outdated, Equal<False>, And<Tax.directTax, Equal<False>, And2<Where<TaxRev.taxType, Equal<TaxType.purchase>, And<Tax.reverseTax, Equal<False>, Or<TaxRev.taxType, Equal<TaxType.sales>, And<Where<Tax.reverseTax, Equal<True>, Or<Tax.taxType, Equal<CSTaxType.use>, Or<Tax.taxType, Equal<CSTaxType.withholding>>>>>>>>, And<Required<TaxRev.startDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>>(aGraph) : (PXSelectBase<Tax>) new PXSelectReadonly2<Tax, InnerJoin<TaxRev, On<TaxRev.taxID, Equal<Tax.taxID>, And<TaxRev.outdated, Equal<False>, And<TaxRev.taxType, Equal<TaxType.sales>, And<Tax.taxType, NotEqual<CSTaxType.withholding>, And<Tax.taxType, NotEqual<CSTaxType.use>, And<Tax.reverseTax, Equal<False>, And<Tax.directTax, Equal<False>, And<Current<GLTranDoc.tranDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>>>>>(aGraph);
      Dictionary<string, PXResult<Tax, TaxRev>> taxRevisionList = new Dictionary<string, PXResult<Tax, TaxRev>>();
      foreach (PXResult<Tax, TaxRev> pxResult in pxSelectBase.Select(new object[1]
      {
        (object) aDocDate
      }))
      {
        taxRevisionList[PXResult<Tax, TaxRev>.op_Implicit(pxResult).TaxID] = pxResult;
        Tax tax = PXResult<Tax, TaxRev>.op_Implicit(pxResult);
        if (tax.TaxCalcType == "I")
        {
          switch (this._enforcing)
          {
            case TaxAttribute.TaxCalcLevelEnforcing.EnforceCalcOnItemAmount:
              if (tax.TaxCalcLevel == "0")
              {
                tax.TaxCalcLevel = "1";
                continue;
              }
              continue;
            case TaxAttribute.TaxCalcLevelEnforcing.EnforceInclusive:
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

    protected virtual IComparer<Tax> GetTaxComparer()
    {
      return (IComparer<Tax>) TaxByCalculationLevelComparer.Instance;
    }
  }
}
