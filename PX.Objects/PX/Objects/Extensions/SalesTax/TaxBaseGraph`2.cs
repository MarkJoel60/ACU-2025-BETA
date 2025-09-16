// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.SalesTax.TaxBaseGraph`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.IN;
using PX.Objects.TX;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Extensions.SalesTax;

/// <summary>The generic graph extension that provides the sales tax functionality.</summary>
public abstract class TaxBaseGraph<TGraph, TPrimary> : PXGraphExtension<TGraph>, ITaxRecalculator
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  /// <summary>A mapping-based view of the <see cref="T:PX.Objects.Extensions.SalesTax.Document" /> data.</summary>
  public PXSelectExtension<Document> Documents;
  /// <summary>A mapping-based view of the <see cref="T:PX.Objects.Extensions.SalesTax.Detail" /> data.</summary>
  public PXSelectExtension<Detail> Details;
  /// <summary>A mapping-based view of the <see cref="T:PX.Objects.Extensions.SalesTax.TaxDetail" /> data.</summary>
  public PXSelectExtension<TaxDetail> Taxes;
  /// <summary>A mapping-based view of the <see cref="T:PX.Objects.Extensions.SalesTax.TaxTotal" /> data.</summary>
  public PXSelectExtension<TaxTotal> TaxTotals;
  /// <exclude />
  protected bool _NoSumTaxable;
  private readonly Dictionary<object, bool> OrigDiscAmtExtCallDict = new Dictionary<object, bool>();
  private readonly Dictionary<object, Decimal?> DiscPercentsDict = new Dictionary<object, Decimal?>();
  /// <exclude />
  protected Document _ParentRow;
  /// <exclude />
  protected string _RecordID = "RecordID";
  /// <summary>The dictionary of inserted records.</summary>
  protected Dictionary<object, object> inserted;
  /// <summary>The dictionary of updated records.</summary>
  protected Dictionary<object, object> updated;

  /// <summary>Returns the mapping of the <see cref="T:PX.Objects.Extensions.SalesTax.Document" /> mapped cache extension to a DAC. This method must be overridden in the implementation class of the base graph.</summary>
  /// <remarks>In the implementation graph for a particular graph, you can either return the default mapping or override the default mapping in this method.</remarks>
  /// <example><para>The following code shows the method that overrides the GetDocumentMapping() method in the implementation class. The method overrides the default mapping of the %Document% mapped cache extension to a DAC: For the CROpportunity DAC, mapping of three fields of the mapped cache extension is overridden.</para>
  ///   <code title="Example" lang="CS">
  /// protected override DocumentMapping GetDocumentMapping()
  /// {
  ///     return new DocumentMapping(typeof(CROpportunity)) {DocumentDate = typeof(CROpportunity.closeDate), CuryDocBal = typeof(CROpportunity.curyProductsAmount), CuryDiscAmt = typeof(CROpportunity.curyDiscTot) };
  /// }</code>
  /// </example>
  protected abstract TaxBaseGraph<TGraph, TPrimary>.DocumentMapping GetDocumentMapping();

  /// <summary>Returns the mapping of the <see cref="T:PX.Objects.Extensions.SalesTax.Detail" /> mapped cache extension to a DAC. This method must be overridden in the implementation class of the base graph.</summary>
  /// <remarks>In the implementation graph for a particular graph, you can either return the default mapping or override the default mapping in this method.</remarks>
  /// <example><para>The following code shows the method that overrides the GetDetailMapping() method in the implementation class. The method overrides the default mapping of the %Detail% mapped cache extension to a DAC: For the CROpportunityProducts DAC, the CuryTranAmt field of the mapped cache extension is mapped to the curyAmount of the DAC; other fields of the extension are mapped by default.</para>
  ///   <code title="Example" lang="CS">
  /// protected override DetailMapping GetDetailMapping()
  /// {
  ///     return new DetailMapping(typeof(CROpportunityProducts)) { CuryTranAmt = typeof(CROpportunityProducts.curyAmount) };
  /// }</code>
  /// </example>
  protected abstract TaxBaseGraph<TGraph, TPrimary>.DetailMapping GetDetailMapping();

  /// <summary>Returns the mapping of the <see cref="T:PX.Objects.Extensions.SalesTax.TaxDetail" /> mapped cache extension to a DAC. This method must be overridden in the implementation class of the base graph.</summary>
  /// <remarks>In the implementation graph for a particular graph, you can either return the default mapping or override the default mapping in this method.</remarks>
  /// <example><para>The following code shows the method that overrides the GetTaxDetailMapping() method in the implementation class. The method returns the defaul mapping of the %TaxDetail% mapped cache extension to the CROpportunityTax DAC.</para>
  ///   <code title="Example" lang="CS">
  /// protected override TaxDetailMapping GetTaxDetailMapping(
  /// {
  ///     return new TaxDetailMapping(typeof(CROpportunityTax), typeof(CROpportunityTax.taxID));
  /// }</code>
  /// </example>
  protected abstract TaxBaseGraph<TGraph, TPrimary>.TaxDetailMapping GetTaxDetailMapping();

  /// <summary>Returns the mapping of the <see cref="T:PX.Objects.Extensions.SalesTax.TaxTotal" /> mapped cache extension to a DAC. This method must be overridden in the implementation class of the base graph.</summary>
  /// <remarks>In the implementation graph for a particular graph, you can either return the default mapping or override the default mapping in this method.</remarks>
  /// <example><para>The following code shows the method that overrides the GetTaxTotalMapping() method in the implementation class. The method returns the defaul mapping of the %TaxTotal% mapped cache extension to the CRTaxTran DAC.</para>
  ///   <code title="Example" lang="CS">
  /// protected override TaxTotalMapping GetTaxTotalMapping()
  /// {
  ///     return new TaxTotalMapping(typeof(CRTaxTran), typeof(CRTaxTran.taxID));
  /// }</code>
  /// </example>
  protected abstract TaxBaseGraph<TGraph, TPrimary>.TaxTotalMapping GetTaxTotalMapping();

  /// <summary>The current document.</summary>
  protected virtual Document CurrentDocument
  {
    get => this._ParentRow ?? (Document) this.Documents.Cache.Current;
  }

  /// <summary>If the value is <tt>true</tt>, indicates that tax calculation is enabled for the document.</summary>
  protected bool IsTaxCalcModeEnabled
  {
    get
    {
      return this.GetDocumentMapping().TaxCalcMode != typeof (Document.taxCalcMode) && PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>();
    }
  }

  /// <exclude />
  protected virtual bool CalcGrossOnDocumentLevel { get; set; }

  /// <exclude />
  protected virtual bool AskConfirmationToRecalculateExtCost { get; set; } = true;

  protected virtual PXResultset<Detail> SelectDetails() => this.Details.Select();

  /// <summary>Inserts the specified record in the specified cache object.</summary>
  /// <param name="cache">The cache object to which the record should be inserted.</param>
  /// <param name="item">The record that should be inserted.</param>
  [PXSuppressActionValidation]
  public virtual object Insert(PXCache cache, object item) => cache.Insert(item);

  /// <summary>Deletes the specified record from the specified cache object.</summary>
  /// <param name="cache">The cache object from which the record should be deleted.</param>
  /// <param name="item">The record that should be deleted.</param>
  [PXSuppressActionValidation]
  public virtual object Delete(PXCache cache, object item) => cache.Delete(item);

  public static void Calculate<TaxExtension>(PXCache sender, PXRowInsertedEventArgs e) where TaxExtension : TaxBaseGraph<TGraph, TPrimary>
  {
    sender.Graph.GetExtension<TaxExtension>()?.Calculate(sender, e);
  }

  public static void Calculate<TaxExtension>(PXCache sender, PXRowUpdatedEventArgs e) where TaxExtension : TaxBaseGraph<TGraph, TPrimary>
  {
    sender.Graph.GetExtension<TaxExtension>()?.Calculate(sender, e);
  }

  public void Calculate(PXCache sender, PXRowInsertedEventArgs e)
  {
    Document currentDocument = this.CurrentDocument;
    if (currentDocument.TaxCalc.GetValueOrDefault() == TaxCalc.ManualLineCalc)
    {
      currentDocument.TaxCalc = new TaxCalc?(TaxCalc.Calc);
      try
      {
        this.Detail_RowInserted(sender, e);
      }
      finally
      {
        currentDocument.TaxCalc = new TaxCalc?(TaxCalc.ManualLineCalc);
      }
    }
    object oldRow;
    if (currentDocument.TaxCalc.GetValueOrDefault() != TaxCalc.ManualCalc || !this.inserted.TryGetValue(e.Row, out oldRow))
      return;
    this.Detail_RowUpdated(sender, new PXRowUpdatedEventArgs(e.Row, oldRow, false));
    this.inserted.Remove(e.Row);
    if (!this.updated.ContainsKey(e.Row))
      return;
    this.updated.Remove(e.Row);
  }

  public void Calculate(PXCache sender, PXRowUpdatedEventArgs e)
  {
    Document currentDocument = this.CurrentDocument;
    if (currentDocument.TaxCalc.GetValueOrDefault() == TaxCalc.ManualLineCalc)
    {
      currentDocument.TaxCalc = new TaxCalc?(TaxCalc.Calc);
      try
      {
        this.Detail_RowUpdated(sender, e);
      }
      finally
      {
        currentDocument.TaxCalc = new TaxCalc?(TaxCalc.ManualLineCalc);
      }
    }
    object oldRow;
    if (currentDocument.TaxCalc.GetValueOrDefault() != TaxCalc.ManualCalc || !this.updated.TryGetValue(e.Row, out oldRow))
      return;
    this.Detail_RowUpdated(sender, new PXRowUpdatedEventArgs(e.Row, oldRow, false));
    this.updated.Remove(e.Row);
  }

  public virtual bool IsExternalTax(PXGraph graph, string taxZoneID)
  {
    PX.Objects.TX.TaxZone taxZone = (PX.Objects.TX.TaxZone) PXSelectBase<PX.Objects.TX.TaxZone, PXSelect<PX.Objects.TX.TaxZone, Where<PX.Objects.TX.TaxZone.taxZoneID, Equal<Required<PX.Objects.TX.TaxZone.taxZoneID>>>>.Config>.Select(graph, (object) taxZoneID);
    return taxZone != null && (taxZone.IsExternal ?? false) && !string.IsNullOrEmpty(taxZone.TaxPluginID);
  }

  protected virtual TaxDetail InitializeTaxDet(TaxDetail data) => data;

  /// <summary>Adds a tax to the specified detail line.</summary>
  /// <param name="detrow">The detail line.</param>
  /// <param name="taxitem">The tax item.</param>
  protected virtual void AddOneTax(Detail detrow, ITaxDetail taxitem)
  {
    if (taxitem == null)
      return;
    object main1 = this.Details.Cache.GetMain<Detail>(detrow);
    PXCache cach = this.Base.Caches[this.GetTaxDetailMapping().Table];
    object child;
    TaxParentAttribute.NewChild(cach, main1, main1.GetType(), out child);
    TaxDetail extension = this.Taxes.Cache.GetExtension<TaxDetail>(child);
    extension.RefTaxID = taxitem.TaxID;
    TaxDetail taxDetail = (TaxDetail) this.Insert(this.Taxes.Cache, (object) this.InitializeTaxDet(extension));
    object main2 = this.Taxes.Cache.GetMain<TaxDetail>(taxDetail);
    if (taxDetail == null)
      return;
    PXParentAttribute.SetParent(cach, main2, main1.GetType(), main1);
  }

  public virtual ITaxDetail MatchesCategory(Detail row, ITaxDetail zoneitem)
  {
    string taxCategoryId = row.TaxCategoryID;
    string taxId = row.TaxID;
    System.DateTime? documentDate = this.CurrentDocument.DocumentDate;
    if ((TaxRev) PXSelectBase<TaxRev, PXSelect<TaxRev, Where<TaxRev.taxID, Equal<Required<TaxRev.taxID>>, And<Required<TaxRev.startDate>, Between<TaxRev.startDate, TaxRev.endDate>, And<TaxRev.outdated, Equal<False>>>>>.Config>.Select((PXGraph) this.Base, (object) zoneitem.TaxID, (object) documentDate) == null)
      return (ITaxDetail) null;
    if (string.Equals(taxId, zoneitem.TaxID))
      return zoneitem;
    if ((PX.Objects.TX.TaxCategory) PXSelectBase<PX.Objects.TX.TaxCategory, PXSelect<PX.Objects.TX.TaxCategory, Where<PX.Objects.TX.TaxCategory.taxCategoryID, Equal<Required<PX.Objects.TX.TaxCategory.taxCategoryID>>>>.Config>.Select((PXGraph) this.Base, (object) taxCategoryId) == null)
      return (ITaxDetail) null;
    return this.MatchesCategory(row, (IEnumerable<ITaxDetail>) new ITaxDetail[1]
    {
      zoneitem
    }).FirstOrDefault<ITaxDetail>();
  }

  public virtual IEnumerable<ITaxDetail> MatchesCategory(
    Detail row,
    IEnumerable<ITaxDetail> zonetaxlist)
  {
    string taxCategoryId = row.TaxCategoryID;
    List<ITaxDetail> taxDetailList = new List<ITaxDetail>();
    PX.Objects.TX.TaxCategory taxCategory = (PX.Objects.TX.TaxCategory) PXSelectBase<PX.Objects.TX.TaxCategory, PXSelect<PX.Objects.TX.TaxCategory, Where<PX.Objects.TX.TaxCategory.taxCategoryID, Equal<Required<PX.Objects.TX.TaxCategory.taxCategoryID>>>>.Config>.Select((PXGraph) this.Base, (object) taxCategoryId);
    if (taxCategory == null)
      return (IEnumerable<ITaxDetail>) taxDetailList;
    HashSet<string> stringSet = new HashSet<string>();
    foreach (PXResult<TaxCategoryDet> pxResult in PXSelectBase<TaxCategoryDet, PXSelect<TaxCategoryDet, Where<TaxCategoryDet.taxCategoryID, Equal<Required<TaxCategoryDet.taxCategoryID>>>>.Config>.Select((PXGraph) this.Base, (object) taxCategoryId))
    {
      TaxCategoryDet taxCategoryDet = (TaxCategoryDet) pxResult;
      stringSet.Add(taxCategoryDet.TaxID);
    }
    foreach (ITaxDetail taxDetail in zonetaxlist)
    {
      bool flag1 = stringSet.Contains(taxDetail.TaxID);
      bool? taxCatFlag = taxCategory.TaxCatFlag;
      bool flag2 = false;
      if (taxCatFlag.GetValueOrDefault() == flag2 & taxCatFlag.HasValue & flag1 || taxCategory.TaxCatFlag.GetValueOrDefault() && !flag1)
        taxDetailList.Add(taxDetail);
    }
    return (IEnumerable<ITaxDetail>) taxDetailList;
  }

  protected abstract IEnumerable<ITaxDetail> ManualTaxes(Detail row);

  /// <exclude />
  protected virtual void DefaultTaxes(Detail row, bool DefaultExisting)
  {
    Document currentDocument = this.CurrentDocument;
    string taxZoneId = currentDocument.TaxZoneID;
    System.DateTime? documentDate = currentDocument.DocumentDate;
    string taxCategoryId = row.TaxCategoryID;
    HashSet<string> stringSet = new HashSet<string>();
    foreach (PXResult<TaxZoneDet, PX.Objects.TX.TaxCategory, TaxRev, TaxCategoryDet> taxitem in PXSelectBase<TaxZoneDet, PXSelectJoin<TaxZoneDet, CrossJoin<PX.Objects.TX.TaxCategory, InnerJoin<TaxRev, On<TaxRev.taxID, Equal<TaxZoneDet.taxID>>, LeftJoin<TaxCategoryDet, On<TaxCategoryDet.taxID, Equal<TaxZoneDet.taxID>, And<TaxCategoryDet.taxCategoryID, Equal<PX.Objects.TX.TaxCategory.taxCategoryID>>>>>>, Where<TaxZoneDet.taxZoneID, Equal<Required<TaxZoneDet.taxZoneID>>, And<PX.Objects.TX.TaxCategory.taxCategoryID, Equal<Required<PX.Objects.TX.TaxCategory.taxCategoryID>>, And<Required<TaxRev.startDate>, Between<TaxRev.startDate, TaxRev.endDate>, And<TaxRev.outdated, Equal<False>, And<Where<PX.Objects.TX.TaxCategory.taxCatFlag, Equal<False>, And<TaxCategoryDet.taxCategoryID, IsNotNull, Or<PX.Objects.TX.TaxCategory.taxCatFlag, Equal<True>, And<TaxCategoryDet.taxCategoryID, IsNull>>>>>>>>>>.Config>.Select((PXGraph) this.Base, (object) taxZoneId, (object) taxCategoryId, (object) documentDate))
    {
      this.AddOneTax(row, (ITaxDetail) (TaxZoneDet) taxitem);
      stringSet.Add(((TaxZoneDet) taxitem).TaxID);
    }
    string taxId;
    if ((taxId = row.TaxID) != null)
    {
      this.AddOneTax(row, (ITaxDetail) new TaxZoneDet()
      {
        TaxID = taxId
      });
      stringSet.Add(taxId);
    }
    foreach (ITaxDetail manualTax in this.ManualTaxes(row))
    {
      if (stringSet.Contains(manualTax.TaxID))
        stringSet.Remove(manualTax.TaxID);
    }
    foreach (string taxID in stringSet)
      this.AddTaxTotals(taxID, (object) row);
    if (!DefaultExisting)
      return;
    foreach (ITaxDetail taxitem in this.MatchesCategory(row, this.ManualTaxes(row)))
      this.AddOneTax(row, taxitem);
  }

  /// <summary>Assigns default taxes for the specified row.</summary>
  /// <param name="row">The detail line.</param>
  protected virtual void DefaultTaxes(Detail row) => this.DefaultTaxes(row, true);

  protected List<object> SelectTaxes(object row, PXTaxCheck taxchk)
  {
    return this.SelectTaxes<Where<True, Equal<True>>>((PXGraph) this.Base, row, taxchk);
  }

  protected abstract List<object> SelectTaxes<Where>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
    where Where : IBqlWhere, new();

  protected abstract List<object> SelectDocumentLines(PXGraph graph, object row);

  protected PX.Objects.TX.Tax AdjustTaxLevel(PX.Objects.TX.Tax taxToAdjust)
  {
    if (this.IsTaxCalcModeEnabled && taxToAdjust.TaxCalcLevel != "2")
    {
      string taxCalcMode = this.GetTaxCalcMode();
      if (!string.IsNullOrEmpty(taxCalcMode))
      {
        PX.Objects.TX.Tax copy = (PX.Objects.TX.Tax) this.Base.Caches[typeof (PX.Objects.TX.Tax)].CreateCopy((object) taxToAdjust);
        switch (taxCalcMode)
        {
          case "G":
            copy.TaxCalcLevel = "0";
            break;
          case "N":
            copy.TaxCalcLevel = "1";
            break;
        }
        return copy;
      }
    }
    return taxToAdjust;
  }

  protected virtual void ClearTaxes(object row)
  {
    PXCache cache = this.Taxes.Cache;
    foreach (object selectTax in this.SelectTaxes(row, PXTaxCheck.Line))
      this.Delete(cache, ((PXResult) selectTax)[0]);
  }

  private Decimal Sum(List<object> list, System.Type field)
  {
    Decimal ret = 0.0M;
    list.ForEach((System.Action<object>) (a => ret += ((Decimal?) this.Taxes.Cache.GetValue((object) this.Taxes.Cache.GetExtension<TaxDetail>(((PXResult) a)[0]), field.Name)).GetValueOrDefault()));
    return ret;
  }

  /// <exclude />
  protected virtual void AddTaxTotals(string taxID, object row)
  {
    PXCache cache = this.TaxTotals.Cache;
    TaxTotal instance = (TaxTotal) cache.CreateInstance();
    instance.RefTaxID = taxID;
    this.Insert(cache, (object) instance);
  }

  protected PX.Objects.CS.Terms SelectTerms()
  {
    return TermsAttribute.SelectTerms((PXGraph) this.Base, this.CurrentDocument.TermsID) ?? new PX.Objects.CS.Terms();
  }

  protected virtual void SetTaxableAmt(object row, Decimal? value)
  {
  }

  /// <summary>Sets the value of the</summary>
  protected virtual void SetTaxAmt(object row, Decimal? value)
  {
  }

  protected virtual bool IsExemptTaxCategory(object row)
  {
    return this.IsExemptTaxCategory(this.Details.Cache, row);
  }

  protected virtual bool IsExemptTaxCategory(PXCache sender, object row)
  {
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.exemptedTaxReporting>())
      return false;
    bool flag = false;
    string taxCategory1 = this.GetTaxCategory(sender, row);
    if (!string.IsNullOrEmpty(taxCategory1))
    {
      PX.Objects.TX.TaxCategory taxCategory2 = (PX.Objects.TX.TaxCategory) PXSelectBase<PX.Objects.TX.TaxCategory, PXSelect<PX.Objects.TX.TaxCategory, Where<PX.Objects.TX.TaxCategory.taxCategoryID, Equal<Required<PX.Objects.TX.TaxCategory.taxCategoryID>>>>.Config>.Select(sender.Graph, (object) taxCategory1);
      flag = taxCategory2 != null && taxCategory2.Exempt.GetValueOrDefault();
    }
    return flag;
  }

  protected abstract Decimal? GetTaxableAmt(object row);

  protected abstract Decimal? GetTaxAmt(object row);

  protected List<object> SelectInclusiveTaxes(object row)
  {
    List<object> objectList = new List<object>();
    if (this.IsExemptTaxCategory(row))
      return objectList;
    string str = this.IsTaxCalcModeEnabled ? this.GetTaxCalcMode() : "T";
    if (this.IsTaxCalcModeEnabled)
    {
      switch (str)
      {
        case "T":
          break;
        case "G":
          objectList = this.SelectTaxes<Where<PX.Objects.TX.Tax.taxCalcLevel, NotEqual<CSTaxCalcLevel.calcOnItemAmtPlusTaxAmt>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.directTax, Equal<False>>>>>((PXGraph) this.Base, row, PXTaxCheck.Line);
          goto label_6;
        default:
          goto label_6;
      }
    }
    objectList = this.SelectTaxes<Where<PX.Objects.TX.Tax.taxCalcLevel, Equal<CSTaxCalcLevel.inclusive>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.directTax, Equal<False>>>>>((PXGraph) this.Base, row, PXTaxCheck.Line);
label_6:
    return objectList;
  }

  protected List<object> SelectLvl1Taxes(object row)
  {
    return !this.IsExemptTaxCategory(row) ? this.SelectTaxes<Where<PX.Objects.TX.Tax.taxCalcLevel, Equal<CSTaxCalcLevel.calcOnItemAmt>, And<PX.Objects.TX.Tax.taxCalcLevel2Exclude, Equal<False>>>>((PXGraph) this.Base, row, PXTaxCheck.Line) : new List<object>();
  }

  private void TaxSetLineDefault(object taxrow, Detail row)
  {
    if (taxrow == null)
      throw new PXArgumentException(nameof (taxrow), "The argument cannot be null.");
    PXCache cache = this.Taxes.Cache;
    TaxDetail extension = cache.GetExtension<TaxDetail>(((PXResult) taxrow)[0]);
    PX.Objects.TX.Tax tax = PXResult.Unwrap<PX.Objects.TX.Tax>(taxrow);
    TaxRev taxRev = PXResult.Unwrap<TaxRev>(taxrow);
    Decimal valueOrDefault = this.GetCuryTranAmt(this.Details.Cache, (object) row).GetValueOrDefault();
    if (taxRev.TaxID == null)
    {
      taxRev.TaxableMin = new Decimal?(0M);
      taxRev.TaxableMax = new Decimal?(0M);
      taxRev.TaxRate = new Decimal?(0M);
    }
    if (this.IsPerUnitTax(tax))
    {
      this.TaxSetLineDefaultForPerUnitTaxes(row, tax, taxRev, extension);
    }
    else
    {
      PX.Objects.CS.Terms terms = this.SelectTerms();
      List<object> objectList = this.SelectInclusiveTaxes((object) row);
      this.SumWithReverseAdjustment(objectList, typeof (TaxRev), typeof (TaxRev.taxRate));
      Decimal num1 = this.SumWithReverseAdjustment(objectList, this.GetTaxDetailMapping().Table, this.GetTaxDetailMapping().CuryTaxAmt);
      Decimal curyTaxableAmt = 0.0M;
      Decimal taxableAmt = 0.0M;
      Decimal num2 = 0.0M;
      Decimal? nullable;
      this.DiscPercentsDict.TryGetValue((object) this.CurrentDocument, out nullable);
      Decimal calculatedTaxRate = taxRev.TaxRate.Value / 100M;
      switch (tax.TaxCalcLevel)
      {
        case "0":
          (curyTaxableAmt, num2) = this.CalculateInclusiveTaxAmounts(row, extension, objectList, in calculatedTaxRate);
          this.SetTaxableAmt((object) row, new Decimal?(curyTaxableAmt));
          this.SetTaxAmt((object) row, new Decimal?(num1));
          break;
        case "1":
          Decimal adjustmentCalculation1 = this.GetPerUnitTaxAmountForTaxableAdjustmentCalculation(tax, extension, row);
          curyTaxableAmt = valueOrDefault - num1 + adjustmentCalculation1;
          break;
        case "2":
          Decimal adjustmentCalculation2 = this.GetPerUnitTaxAmountForTaxableAdjustmentCalculation(tax, extension, row);
          Decimal num3 = this.SumWithReverseAdjustment(this.SelectLvl1Taxes((object) row), this.GetTaxDetailMapping().Table, this.GetTaxDetailMapping().CuryTaxAmt);
          curyTaxableAmt = valueOrDefault - num1 + num3 + adjustmentCalculation2;
          break;
      }
      if ((tax.TaxCalcLevel == "1" || tax.TaxCalcLevel == "2") && tax.TaxApplyTermsDisc == "X")
        curyTaxableAmt *= 1M - (nullable ?? terms.DiscPercent.GetValueOrDefault()) / 100M;
      if (tax.TaxCalcType == "I" && (tax.TaxCalcLevel == "1" || tax.TaxCalcLevel == "2"))
      {
        if (this.GetTaxDetailMapping().CuryOrigTaxableAmt != typeof (TaxDetail.curyOrigTaxableAmt))
          cache.SetValue<TaxDetail.curyOrigTaxableAmt>((object) extension, (object) this.Base.FindImplementation<IPXCurrencyHelper>().RoundCury(curyTaxableAmt));
        this.Base.AdjustMinMaxTaxableAmt(taxRev, ref curyTaxableAmt, ref taxableAmt);
        num2 = curyTaxableAmt * taxRev.TaxRate.Value / 100M;
        if (tax.TaxApplyTermsDisc == "T")
          num2 *= 1M - (nullable ?? terms.DiscPercent.GetValueOrDefault()) / 100M;
      }
      else
      {
        int num4 = tax.TaxCalcType != "I" ? 1 : 0;
      }
      extension.TaxRate = taxRev.TaxRate;
      extension.NonDeductibleTaxRate = taxRev.NonDeductibleTaxRate;
      Decimal num5 = this.Base.FindImplementation<IPXCurrencyHelper>().RoundCury(curyTaxableAmt);
      int num6 = this.IsExemptTaxCategory((object) row) ? 1 : 0;
      if (num6 != 0)
        this.SetTaxDetailExemptedAmount(cache, extension, new Decimal?(num5));
      else
        this.SetTaxDetailTaxableAmount(cache, extension, new Decimal?(num5));
      PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = this.Base.FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo();
      Decimal num7 = defaultCurrencyInfo.RoundCury(num2);
      if (tax.DeductibleVAT.GetValueOrDefault())
      {
        extension.SetExpenseAmountsForDeductibleVAT(taxRev, num2, defaultCurrencyInfo);
        num7 -= extension.CuryExpenseAmt.Value;
      }
      if (num6 == 0)
        this.SetTaxDetailTaxAmount(cache, extension, new Decimal?(num7));
      if (taxRev.TaxID != null && !tax.DirectTax.GetValueOrDefault())
      {
        cache.Update((object) extension);
        if (!(tax.TaxCalcLevel == "0") || this.Details.Cache.GetStatus((object) row) != PXEntryStatus.Notchanged && this.Details.Cache.GetStatus((object) row) != PXEntryStatus.Held)
          return;
        this.Details.Cache.SetStatus((object) row, PXEntryStatus.Updated);
      }
      else
        this.Delete(cache, (object) extension);
    }
  }

  private (Decimal InclTaxTaxable, Decimal InclTaxAmount) CalculateInclusiveTaxAmounts(
    Detail row,
    TaxDetail nonPerUnitTaxDetail,
    List<object> inclusiveTaxes,
    in Decimal calculatedTaxRate)
  {
    (List<object> objectList1, List<object> objectList2, List<object> objectList3) = this.SegregateInclusiveTaxes(inclusiveTaxes);
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    Decimal num3 = this.SumWithReverseAdjustment(objectList3, typeof (TaxRev), typeof (TaxRev.taxRate)) / 100M;
    if (this.GetTaxDetailMapping().CuryTaxAmt != (System.Type) null)
    {
      num1 = this.SumWithReverseAdjustment(objectList1, this.GetTaxDetailMapping().Table, this.GetTaxDetailMapping().CuryTaxAmt);
      num2 = this.SumWithReverseAdjustment(objectList2, this.GetTaxDetailMapping().Table, this.GetTaxDetailMapping().CuryTaxAmt);
    }
    Decimal num4 = (row.CuryTranAmt.Value - num2) / (1M + num3) - num1 + num1;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = this.Base.FindImplementation<IPXCurrencyHelper>().GetCurrencyInfo(nonPerUnitTaxDetail.CuryInfoID);
    Decimal val = num4 * calculatedTaxRate;
    Decimal num5 = currencyInfo.RoundCury(val);
    Decimal num6 = num1 + num2;
    PXCache cach = this.Base.Caches[typeof (TaxRev)];
    foreach (PXResult pxResult in objectList3)
    {
      object data = pxResult[typeof (TaxRev)];
      PX.Objects.TX.Tax tax = (PX.Objects.TX.Tax) pxResult[typeof (PX.Objects.TX.Tax)];
      Decimal? nullable1 = cach.GetValue<TaxRev.taxRate>(data) as Decimal?;
      Decimal num7 = tax.ReverseTax.GetValueOrDefault() ? -1M : 1M;
      Decimal num8 = num4;
      Decimal? nullable2 = nullable1;
      Decimal valueOrDefault = (nullable2.HasValue ? new Decimal?(num8 * nullable2.GetValueOrDefault() / 100M) : new Decimal?()).GetValueOrDefault();
      Decimal num9 = currencyInfo.RoundCury(valueOrDefault) * num7;
      num6 += num9;
    }
    return (row.CuryTranAmt.Value - num6 + num1, num5);
  }

  private (List<object> PerUnitTaxesIncludedInTaxOnTaxCalc, List<object> PerUnitTaxesExcludedFromTaxOnTaxCalc, List<object> NonPerUnitTaxes) SegregateInclusiveTaxes(
    List<object> inclusiveTaxes)
  {
    List<object> objectList1 = new List<object>();
    List<object> objectList2 = new List<object>();
    List<object> objectList3 = new List<object>(inclusiveTaxes.Count);
    foreach (PXResult inclusiveTax in inclusiveTaxes)
    {
      PX.Objects.TX.Tax tax = inclusiveTax.GetItem<PX.Objects.TX.Tax>();
      if (tax != null)
      {
        if (!this.IsPerUnitTax(tax))
          objectList3.Add((object) inclusiveTax);
        else if (tax.TaxCalcLevel2Exclude.GetValueOrDefault())
          objectList2.Add((object) inclusiveTax);
        else
          objectList1.Add((object) inclusiveTax);
      }
    }
    return (objectList1, objectList2, objectList3);
  }

  protected virtual void SetTaxDetailTaxableAmount(
    PXCache cache,
    TaxDetail taxdet,
    Decimal? curyTaxableAmt)
  {
    cache.SetValue((object) taxdet, typeof (TaxDetail.curyTaxableAmt).Name, (object) curyTaxableAmt);
  }

  protected virtual void SetTaxDetailExemptedAmount(
    PXCache cache,
    TaxDetail taxdet,
    Decimal? curyExemptedAmt)
  {
    cache.SetValue((object) taxdet, typeof (TaxDetail.curyExemptedAmt).Name, (object) curyExemptedAmt);
  }

  protected virtual void SetTaxDetailTaxAmount(
    PXCache cache,
    TaxDetail taxdet,
    Decimal? curyTaxAmt)
  {
    cache.SetValue((object) taxdet, typeof (TaxDetail.curyTaxAmt).Name, (object) curyTaxAmt);
  }

  /// <exclude />
  public static Pair<double, double> SolveQuadraticEquation(double a, double b, double c)
  {
    Pair<double, double> pair = (Pair<double, double>) null;
    double d = b * b - 4.0 * a * c;
    if (d == 0.0)
    {
      double aSecond;
      pair = new Pair<double, double>(aSecond = -(b / 2.0 * a), aSecond);
    }
    else if (d > 0.0)
    {
      double num = System.Math.Sqrt(d);
      pair = new Pair<double, double>((-b + num) / (2.0 * a), (-b - num) / (2.0 * a));
    }
    return pair;
  }

  /// <summary>The FieldUpdated2 event handler for the <see cref="P:PX.Objects.Extensions.SalesTax.Document.CuryDiscAmt" /> field.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(
    PX.Data.Events.FieldUpdated<Document, Document.curyDiscAmt> e)
  {
    if (e.Row == null)
      return;
    this.OrigDiscAmtExtCallDict[(object) e.Row] = e.ExternalCall;
  }

  /// <summary>The RowUpdated event handler for the <see cref="T:PX.Objects.Extensions.SalesTax.Document" /> mapped cache extension.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(PX.Data.Events.RowUpdated<Document> e)
  {
    if (e.Row == null)
      return;
    bool flag;
    this.OrigDiscAmtExtCallDict.TryGetValue((object) e.Row, out flag);
    if (!flag)
      return;
    Decimal? curyDiscAmt = e.Row.CuryDiscAmt;
    Decimal valueOrDefault1 = curyDiscAmt.GetValueOrDefault();
    curyDiscAmt = e.OldRow.CuryDiscAmt;
    Decimal valueOrDefault2 = curyDiscAmt.GetValueOrDefault();
    if (!(valueOrDefault1 != valueOrDefault2) || this.DiscPercentsDict.ContainsKey((object) e.Row))
      return;
    this.DiscPercentsDict.Add((object) e.Row, new Decimal?(0M));
    PXFieldUpdatedEventArgs e1 = new PXFieldUpdatedEventArgs((object) e.Row, (object) valueOrDefault2, false);
    using (new TermsAttribute.UnsubscribeCalcDiscScope(e.Cache))
    {
      try
      {
        if (valueOrDefault1 == 0M)
          return;
        this.ParentFieldUpdated(e.Cache, e1);
        this.DiscPercentsDict[(object) e.Row] = new Decimal?();
        Decimal a = 0M;
        PXCache cache = this.TaxTotals.Cache;
        foreach (object selectTax in this.SelectTaxes((object) e.Row, PXTaxCheck.RecalcTotals))
        {
          object row;
          object data = ((PXResult) (row = selectTax))[0];
          PX.Objects.TX.Tax tax = PXResult.Unwrap<PX.Objects.TX.Tax>(row);
          if (tax?.TaxCalcLevel != "0" && tax?.TaxApplyTermsDisc == "X")
          {
            Decimal num1 = a;
            bool? nullable1 = tax.ReverseTax;
            Decimal num2 = nullable1.GetValueOrDefault() ? -1M : 1M;
            Decimal? nullable2 = (Decimal?) cache.GetValue<TaxTotal.curyTaxAmt>(data);
            Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
            nullable1 = tax.DeductibleVAT;
            Decimal num3;
            if (!nullable1.GetValueOrDefault())
            {
              num3 = 0M;
            }
            else
            {
              nullable2 = (Decimal?) cache.GetValue<TaxTotal.curyExpenseAmt>(data);
              num3 = nullable2.GetValueOrDefault();
            }
            Decimal num4 = valueOrDefault3 + num3;
            Decimal num5 = num2 * num4;
            a = num1 + num5;
          }
        }
        if (!(a != 0M))
          return;
        Decimal valueOrDefault4 = e.Row.CuryDocBal.GetValueOrDefault();
        Pair<double, double> pair = TaxBaseGraph<TGraph, TPrimary>.SolveQuadraticEquation((double) a, -(double) valueOrDefault4, (double) valueOrDefault1);
        this.DiscPercentsDict[(object) e.Row] = pair == null || pair.first < 0.0 || pair == null || pair.first > 1.0 ? (pair == null || pair.second < 0.0 || pair == null || pair.second > 1.0 ? new Decimal?() : new Decimal?((Decimal) System.Math.Round(pair.second * 100.0, 2))) : new Decimal?((Decimal) System.Math.Round(pair.first * 100.0, 2));
      }
      catch
      {
        this.DiscPercentsDict[(object) e.Row] = new Decimal?();
      }
      finally
      {
        this.ParentFieldUpdated(e.Cache, e1);
        e.Cache.RaiseRowUpdated((object) e.Row, (object) e.OldRow);
        this.OrigDiscAmtExtCallDict.Remove((object) e.Row);
        this.DiscPercentsDict.Remove((object) e.Row);
      }
    }
  }

  protected virtual void AdjustTaxableAmount(
    object row,
    List<object> taxitems,
    ref Decimal CuryTaxableAmt,
    string TaxCalcType)
  {
  }

  protected virtual TaxTotal CalculateTaxSum(object taxrow, object row)
  {
    if (taxrow == null)
      throw new PXArgumentException(nameof (taxrow), "The argument cannot be null.");
    PXCache cache = this.Taxes.Cache;
    PXCache cache1 = this.TaxTotals.Cache;
    TaxTotal taxdet = cache1.GetExtension<TaxTotal>(((PXResult) taxrow)[0]);
    PX.Objects.TX.Tax tax = PXResult.Unwrap<PX.Objects.TX.Tax>(taxrow);
    TaxRev taxRev = PXResult.Unwrap<TaxRev>(taxrow);
    if (taxRev.TaxID == null)
    {
      taxRev.TaxableMin = new Decimal?(0M);
      taxRev.TaxableMax = new Decimal?(0M);
      taxRev.TaxRate = new Decimal?(0M);
    }
    Decimal num1 = 0M;
    Decimal val1 = 0M;
    Decimal val2 = 0.0M;
    Decimal taxableAmt = 0.0M;
    Decimal curyExpenseAmt1 = 0.0M;
    List<object> objectList1 = this.SelectTaxes<Where<PX.Objects.TX.Tax.taxID, Equal<Required<PX.Objects.TX.Tax.taxID>>>>((PXGraph) this.Base, row, PXTaxCheck.RecalcLine, (object) taxdet.RefTaxID);
    if (objectList1.Count == 0 || taxRev.TaxID == null)
      return (TaxTotal) null;
    Decimal curyTaxAmt;
    Decimal? nullable1;
    if (tax.TaxCalcType == "I")
    {
      if (this.GetTaxDetailMapping().CuryOrigTaxableAmt != typeof (TaxDetail.curyOrigTaxableAmt))
        num1 = this.Sum(objectList1, typeof (TaxDetail.curyOrigTaxableAmt));
      val2 = this.Sum(objectList1, typeof (TaxDetail.curyTaxableAmt));
      this.AdjustTaxableAmount(row, objectList1, ref val2, tax.TaxCalcType);
      curyTaxAmt = val1 = this.Sum(objectList1, typeof (TaxDetail.curyTaxAmt));
      curyExpenseAmt1 = this.Sum(objectList1, typeof (TaxDetail.curyExpenseAmt));
    }
    else if (tax.TaxType != "W" && (this.CalcGrossOnDocumentLevel && this.IsTaxCalcModeEnabled && this.GetTaxCalcMode() == "G" || tax.TaxCalcLevel == "0" && (!this.IsTaxCalcModeEnabled || this.GetTaxCalcMode() != "N")))
    {
      val2 = this.Sum(objectList1, typeof (TaxDetail.curyTaxableAmt));
      curyTaxAmt = val1 = this.Sum(objectList1, typeof (TaxDetail.curyTaxAmt));
      List<object> source1 = this.SelectDocumentLines((PXGraph) this.Base, row);
      if (source1.Any<object>())
      {
        PXCache docLineCache = source1.Any<object>() ? this.Base.Caches[source1[0].GetType()] : this.Details.Cache;
        Dictionary<int, Decimal> dictionary = source1.ToDictionary<object, int, Decimal>((Func<object, int>) (_ => (int) docLineCache.GetValue(_, "LineNbr")), (Func<object, Decimal>) (_ => this.GetCuryTranAmt(this.Details.Cache, _) ?? 0.0M));
        IEnumerable<\u003C\u003Ef__AnonymousType53<int, string, Decimal, Decimal>> source2 = this.SelectTaxes(row, PXTaxCheck.RecalcLine).Where<object>((Func<object, bool>) (_ => PXResult.Unwrap<PX.Objects.TX.Tax>(_).TaxCalcLevel == "0")).ToList<object>().Select(_ => new
        {
          LineNbr = (int) cache.GetValue(((PXResult) _)[0], "LineNbr"),
          TaxID = PXResult.Unwrap<PX.Objects.TX.Tax>(_).TaxID,
          TaxRate = PXResult.Unwrap<TaxRev>(_).TaxRate.GetValueOrDefault(),
          TaxRateMultiplier = PXResult.Unwrap<PX.Objects.TX.Tax>(_).ReverseTax.GetValueOrDefault() ? -1.0M : 1.0M
        });
        Decimal? taxRate = taxdet.TaxRate;
        Decimal num2 = 0.0M;
        Decimal? currentTaxRate = (!(taxRate.GetValueOrDefault() == num2 & taxRate.HasValue) ? taxdet.TaxRate : source2.FirstOrDefault(_ => _.TaxID == taxdet.RefTaxID)?.TaxRate) ?? new Decimal?(0.0M);
        List<int> list1 = source2.Where(_ => _.TaxID == taxdet.RefTaxID).Select(_ => _.LineNbr).ToList<int>();
        List<TaxBaseGraph<TGraph, TPrimary>.InclusiveTaxGroup> source3 = new List<TaxBaseGraph<TGraph, TPrimary>.InclusiveTaxGroup>();
        foreach (int num3 in list1)
        {
          int lineNbr = num3;
          List<\u003C\u003Ef__AnonymousType53<int, string, Decimal, Decimal>> list2 = source2.Where(_ => _.LineNbr == lineNbr).OrderBy(_ => _.TaxID).ToList();
          string groupKey = string.Join("::", list2.Select(_ => _.TaxID));
          Decimal num4 = list2.Sum(_ => _.TaxRate);
          Decimal num5 = dictionary[lineNbr];
          if (source3.Any<TaxBaseGraph<TGraph, TPrimary>.InclusiveTaxGroup>((Func<TaxBaseGraph<TGraph, TPrimary>.InclusiveTaxGroup, bool>) (g => g.Key == groupKey)))
            source3.Single<TaxBaseGraph<TGraph, TPrimary>.InclusiveTaxGroup>((Func<TaxBaseGraph<TGraph, TPrimary>.InclusiveTaxGroup, bool>) (g => g.Key == groupKey)).TotalAmount += num5;
          else
            source3.Add(new TaxBaseGraph<TGraph, TPrimary>.InclusiveTaxGroup()
            {
              Key = groupKey,
              Rate = num4,
              TotalAmount = num5
            });
        }
        curyTaxAmt = source3.Sum<TaxBaseGraph<TGraph, TPrimary>.InclusiveTaxGroup>((Func<TaxBaseGraph<TGraph, TPrimary>.InclusiveTaxGroup, Decimal>) (g =>
        {
          IPXCurrencyHelper implementation = this.Base.FindImplementation<IPXCurrencyHelper>();
          Decimal num6 = g.TotalAmount / (1M + g.Rate / 100.0M);
          Decimal? nullable2 = currentTaxRate;
          Decimal val3 = (nullable2.HasValue ? new Decimal?(num6 * nullable2.GetValueOrDefault() / 100.0M) : new Decimal?()) ?? 0.0M;
          return implementation.RoundCury(val3);
        }));
        val2 = this.Base.FindImplementation<IPXCurrencyHelper>().RoundCury(source3.Sum<TaxBaseGraph<TGraph, TPrimary>.InclusiveTaxGroup>((Func<TaxBaseGraph<TGraph, TPrimary>.InclusiveTaxGroup, Decimal>) (g => g.TotalAmount / (1M + g.Rate / 100.0M))));
      }
      if (tax.DeductibleVAT.GetValueOrDefault())
      {
        Decimal num7 = curyTaxAmt;
        nullable1 = taxRev.NonDeductibleTaxRate;
        Decimal num8 = 1.0M - (nullable1 ?? 0.0M) / 100.0M;
        curyExpenseAmt1 = num7 * num8;
      }
    }
    else
    {
      List<object> objectList2 = this.SelectLvl1Taxes(row);
      if (this._NoSumTaxable && (tax.TaxCalcLevel == "1" || objectList2.Count == 0))
      {
        nullable1 = taxdet.CuryTaxableAmt;
        val2 = nullable1.GetValueOrDefault();
      }
      else
      {
        val2 = this.Sum(objectList1, typeof (TaxDetail.curyTaxableAmt));
        Decimal val4 = this.Sum(objectList1, typeof (TaxDetail.curyTaxAmt));
        val1 = this.Base.FindImplementation<IPXCurrencyHelper>().RoundCury(val4);
        this.AdjustTaxableAmount(row, objectList1, ref val2, tax.TaxCalcType);
      }
      num1 = this.Base.FindImplementation<IPXCurrencyHelper>().RoundCury(val2);
      this.Base.AdjustMinMaxTaxableAmt(taxRev, ref val2, ref taxableAmt);
      Decimal num9 = val2;
      nullable1 = taxRev.TaxRate;
      Decimal num10 = nullable1.Value;
      curyTaxAmt = num9 * num10 / 100M;
      if (curyTaxAmt != val1)
      {
        Decimal num11 = val1 - curyTaxAmt;
      }
      this.AdjustExpenseAmt(tax, taxRev, curyTaxAmt, ref curyExpenseAmt1);
      this.AdjustTaxAmtOnDiscount(tax, ref curyTaxAmt);
    }
    Decimal val5 = this.Sum(objectList1, typeof (TaxDetail.curyExemptedAmt));
    taxdet = (TaxTotal) cache1.CreateCopy((object) taxdet);
    if (this.GetTaxTotalMapping().CuryOrigTaxableAmt != typeof (TaxTotal.curyOrigTaxableAmt))
      taxdet.CuryOrigTaxableAmt = new Decimal?(num1);
    bool? isExternal = tax.IsExternal;
    bool flag = false;
    if (isExternal.GetValueOrDefault() == flag & isExternal.HasValue)
      taxdet.IsTaxInclusive = new bool?(tax.TaxCalcLevel == "0");
    taxdet.TaxRate = taxRev.TaxRate;
    taxdet.NonDeductibleTaxRate = taxRev.NonDeductibleTaxRate;
    taxdet.CuryTaxableAmt = new Decimal?(this.Base.FindImplementation<IPXCurrencyHelper>().RoundCury(val2));
    taxdet.CuryExemptedAmt = new Decimal?(this.Base.FindImplementation<IPXCurrencyHelper>().RoundCury(val5));
    taxdet.CuryTaxAmt = new Decimal?(this.Base.FindImplementation<IPXCurrencyHelper>().RoundCury(curyTaxAmt));
    taxdet.CuryTaxAmtSumm = new Decimal?(this.Base.FindImplementation<IPXCurrencyHelper>().RoundCury(val1));
    taxdet.CuryExpenseAmt = new Decimal?(this.Base.FindImplementation<IPXCurrencyHelper>().RoundCury(curyExpenseAmt1));
    if (tax.DeductibleVAT.GetValueOrDefault() && tax.TaxCalcType != "I")
    {
      TaxTotal taxTotal = taxdet;
      nullable1 = taxdet.CuryTaxAmt;
      Decimal? curyExpenseAmt2 = taxdet.CuryExpenseAmt;
      Decimal? nullable3 = nullable1.HasValue & curyExpenseAmt2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - curyExpenseAmt2.GetValueOrDefault()) : new Decimal?();
      taxTotal.CuryTaxAmt = nullable3;
    }
    if (this.IsPerUnitTax(tax))
      taxdet = this.FillAggregatedTaxDetailForPerUnitTax(row, tax, taxRev, taxdet, objectList1);
    return taxdet;
  }

  protected virtual void CalculateTaxSumTaxAmt(TaxTotal taxdet, PX.Objects.TX.Tax tax, TaxRev taxrev)
  {
    PXCache cache = this.TaxTotals.Cache;
    Decimal taxableAmt = 0.0M;
    Decimal curyExpenseAmt1 = 0.0M;
    Decimal curyTaxableAmt = (Decimal) cache.GetValue((object) taxdet, typeof (TaxTotal.curyTaxableAmt).Name);
    Decimal num = this.Base.FindImplementation<IPXCurrencyHelper>().RoundCury(curyTaxableAmt);
    Decimal valueOrDefault = taxrev.TaxRate.GetValueOrDefault();
    this.Base.AdjustMinMaxTaxableAmt(taxrev, ref curyTaxableAmt, ref taxableAmt);
    Decimal curyTaxAmt1 = curyTaxableAmt * valueOrDefault / 100M;
    this.AdjustExpenseAmt(tax, taxrev, curyTaxAmt1, ref curyExpenseAmt1);
    this.AdjustTaxAmtOnDiscount(tax, ref curyTaxAmt1);
    if (this.GetTaxTotalMapping().CuryOrigTaxableAmt != typeof (TaxTotal.curyOrigTaxableAmt))
      cache.SetValue((object) taxdet, typeof (TaxTotal.curyOrigTaxableAmt).Name, (object) num);
    cache.SetValue((object) taxdet, typeof (TaxTotal.taxRate).Name, (object) valueOrDefault);
    cache.SetValue((object) taxdet, typeof (TaxTotal.nonDeductibleTaxRate).Name, (object) taxrev.NonDeductibleTaxRate);
    cache.SetValue((object) taxdet, typeof (TaxTotal.curyTaxableAmt).Name, (object) this.Base.FindImplementation<IPXCurrencyHelper>().RoundCury(curyTaxableAmt));
    cache.SetValue((object) taxdet, typeof (TaxTotal.curyTaxAmt).Name, (object) this.Base.FindImplementation<IPXCurrencyHelper>().RoundCury(curyTaxAmt1));
    cache.SetValue((object) taxdet, typeof (TaxTotal.curyExpenseAmt).Name, (object) this.Base.FindImplementation<IPXCurrencyHelper>().RoundCury(curyExpenseAmt1));
    if (!tax.DeductibleVAT.GetValueOrDefault() || !(tax.TaxCalcType != "I"))
      return;
    PXCache pxCache = cache;
    TaxTotal data = taxdet;
    string name = typeof (TaxTotal.curyTaxAmt).Name;
    Decimal? curyTaxAmt2 = taxdet.CuryTaxAmt;
    Decimal? curyExpenseAmt2 = taxdet.CuryExpenseAmt;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) (curyTaxAmt2.HasValue & curyExpenseAmt2.HasValue ? new Decimal?(curyTaxAmt2.GetValueOrDefault() - curyExpenseAmt2.GetValueOrDefault()) : new Decimal?());
    pxCache.SetValue((object) data, name, (object) local);
  }

  private void AdjustExpenseAmt(
    PX.Objects.TX.Tax tax,
    TaxRev taxrev,
    Decimal curyTaxAmt,
    ref Decimal curyExpenseAmt)
  {
    if (!tax.DeductibleVAT.GetValueOrDefault())
      return;
    curyExpenseAmt = curyTaxAmt * (1M - taxrev.NonDeductibleTaxRate.GetValueOrDefault() / 100M);
  }

  private void AdjustTaxAmtOnDiscount(PX.Objects.TX.Tax tax, ref Decimal curyTaxAmt)
  {
    if (!(tax.TaxCalcLevel == "1") && !(tax.TaxCalcLevel == "2") || !(tax.TaxApplyTermsDisc == "T"))
      return;
    Decimal? nullable;
    this.DiscPercentsDict.TryGetValue((object) this.CurrentDocument, out nullable);
    PX.Objects.CS.Terms terms = this.SelectTerms();
    curyTaxAmt *= 1M - (nullable ?? terms.DiscPercent.GetValueOrDefault()) / 100M;
  }

  private TaxTotal TaxSummarize(object taxrow, object row)
  {
    if (taxrow == null)
      throw new PXArgumentException(nameof (taxrow), "The argument cannot be null.");
    PXCache cache = this.TaxTotals.Cache;
    TaxTotal taxSum = this.CalculateTaxSum(taxrow, row);
    if (taxSum != null)
      return (TaxTotal) cache.Update((object) taxSum);
    TaxTotal extension = this.TaxTotals.Cache.GetExtension<TaxTotal>(((PXResult) taxrow)[0]);
    this.Delete(cache, (object) extension);
    return (TaxTotal) null;
  }

  protected virtual void CalcTaxes(object row) => this.CalcTaxes(row, PXTaxCheck.RecalcLine);

  public virtual void RecalcTaxes()
  {
    Document current = this.Documents.Current;
    this.Details.View.Cache.ClearQueryCache();
    if (current.TaxCalc.GetValueOrDefault() != TaxCalc.Calc)
    {
      TaxCalc? taxCalc = current.TaxCalc;
      if (taxCalc.GetValueOrDefault() != TaxCalc.ManualLineCalc)
      {
        taxCalc = current.TaxCalc;
        if (taxCalc.GetValueOrDefault() != TaxCalc.ManualCalc)
          return;
        this._ParentRow = current;
        this.CalcTotals((object) null, false);
        this._ParentRow = (Document) null;
        return;
      }
    }
    this._ParentRow = current;
    this.CalcTaxes((object) null);
    this._ParentRow = (Document) null;
  }

  protected virtual object SelectParent(PXCache cache, object row)
  {
    return PXParentAttribute.SelectParent(cache, row, this.GetDetailMapping().Table);
  }

  protected virtual void CalcTaxes(object row, PXTaxCheck taxchk)
  {
    PXCache cache = this.Taxes.Cache;
    object obj = row;
    foreach (object selectTax in this.SelectTaxes(row, taxchk))
    {
      if (row == null)
        obj = this.SelectParent(cache, ((PXResult) selectTax)[0]);
      if (obj != null)
        this.TaxSetLineDefault(selectTax, this.Details.Cache.GetExtension<Detail>(obj));
    }
    this.CalcTotals(row, true);
  }

  protected virtual void CalcDocTotals(
    object row,
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal)
  {
    this._CalcDocTotals(row, CuryTaxTotal, CuryInclTaxTotal, CuryWhTaxTotal);
  }

  protected virtual Decimal CalcLineTotal(object row)
  {
    Decimal num1 = 0M;
    Detail extension = this.Details.Cache.GetExtension<Detail>(row);
    foreach (PXResult<Detail> selectDetail in this.SelectDetails())
    {
      Detail a = (Detail) selectDetail;
      Decimal num2 = num1;
      Detail detail = this.Details.Cache.ObjectsEqual((object) a, (object) extension) ? extension : a;
      Decimal? nullable1;
      Decimal? nullable2;
      if (detail == null)
      {
        nullable1 = new Decimal?();
        nullable2 = nullable1;
      }
      else
        nullable2 = detail.CuryTranAmt;
      nullable1 = nullable2;
      Decimal valueOrDefault = nullable1.GetValueOrDefault();
      num1 = num2 + valueOrDefault;
    }
    return num1;
  }

  protected virtual Decimal CalcDiscountLineTotal(object row)
  {
    Decimal num1 = 0M;
    Detail extension = this.Details.Cache.GetExtension<Detail>(row);
    foreach (PXResult<Detail> selectDetail in this.SelectDetails())
    {
      Detail a = (Detail) selectDetail;
      Decimal num2 = num1;
      Detail detail = this.Details.Cache.ObjectsEqual((object) a, (object) extension) ? extension : a;
      Decimal? nullable1;
      Decimal? nullable2;
      if (detail == null)
      {
        nullable1 = new Decimal?();
        nullable2 = nullable1;
      }
      else
        nullable2 = detail.CuryTranDiscount;
      nullable1 = nullable2;
      Decimal valueOrDefault = nullable1.GetValueOrDefault();
      num1 = num2 + valueOrDefault;
    }
    return num1;
  }

  protected virtual Decimal CalcTranExtPriceTotal(object row)
  {
    Decimal num1 = 0M;
    Detail extension = this.Details.Cache.GetExtension<Detail>(row);
    foreach (PXResult<Detail> selectDetail in this.SelectDetails())
    {
      Detail a = (Detail) selectDetail;
      Decimal num2 = num1;
      Detail detail = this.Details.Cache.ObjectsEqual((object) a, (object) extension) ? extension : a;
      Decimal? nullable1;
      Decimal? nullable2;
      if (detail == null)
      {
        nullable1 = new Decimal?();
        nullable2 = nullable1;
      }
      else
        nullable2 = detail.CuryTranExtPrice;
      nullable1 = nullable2;
      Decimal valueOrDefault = nullable1.GetValueOrDefault();
      num1 = num2 + valueOrDefault;
    }
    return num1;
  }

  /// <exclude />
  protected virtual void _CalcDocTotals(
    object row,
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal)
  {
    Decimal curyLineTotal = 0M;
    Decimal discountLineTotal = 0M;
    Decimal tranExtPriceTotal = 0M;
    this.CalcLineTotals(row, this.SelectDetails(), out curyLineTotal, out discountLineTotal, out tranExtPriceTotal);
    Decimal objA = curyLineTotal + CuryTaxTotal - CuryInclTaxTotal;
    Decimal valueOrDefault1 = this.CurrentDocument.CuryLineTotal.GetValueOrDefault();
    Decimal valueOrDefault2 = this.CurrentDocument.CuryTaxTotal.GetValueOrDefault();
    if (!object.Equals((object) curyLineTotal, (object) valueOrDefault1) || !object.Equals((object) CuryTaxTotal, (object) valueOrDefault2))
    {
      this.ParentSetValue<Document.curyLineTotal>((object) curyLineTotal);
      this.ParentSetValue<Document.curyDiscountLineTotal>((object) discountLineTotal);
      this.ParentSetValue<Document.curyExtPriceTotal>((object) tranExtPriceTotal);
      this.ParentSetValue<Document.curyTaxTotal>((object) CuryTaxTotal);
      if (this.GetDocumentMapping().CuryDocBal != typeof (Document.curyDocBal))
      {
        this.ParentSetValue<Document.curyDocBal>((object) objA);
        return;
      }
    }
    if (!(this.GetDocumentMapping().CuryDocBal != typeof (Document.curyDocBal)))
      return;
    Decimal valueOrDefault3 = this.CurrentDocument.CuryDocBal.GetValueOrDefault();
    if (object.Equals((object) objA, (object) valueOrDefault3))
      return;
    this.ParentSetValue<Document.curyDocBal>((object) objA);
  }

  protected virtual void CalcLineTotals(
    object row,
    PXResultset<Detail> detailsRows,
    out Decimal curyLineTotal,
    out Decimal discountLineTotal,
    out Decimal tranExtPriceTotal)
  {
    Detail extension = this.Details.Cache.GetExtension<Detail>(row);
    curyLineTotal = 0M;
    discountLineTotal = 0M;
    tranExtPriceTotal = 0M;
    foreach (PXResult<Detail> detailsRow in detailsRows)
    {
      Detail a = (Detail) detailsRow;
      ref Decimal local1 = ref curyLineTotal;
      Decimal num1 = curyLineTotal;
      Detail detail1 = this.Details.Cache.ObjectsEqual((object) a, (object) extension) ? extension : a;
      Decimal? nullable1;
      Decimal? nullable2;
      if (detail1 == null)
      {
        nullable1 = new Decimal?();
        nullable2 = nullable1;
      }
      else
        nullable2 = detail1.CuryTranAmt;
      nullable1 = nullable2;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      Decimal num2 = num1 + valueOrDefault1;
      local1 = num2;
      ref Decimal local2 = ref discountLineTotal;
      Decimal num3 = discountLineTotal;
      Detail detail2 = this.Details.Cache.ObjectsEqual((object) a, (object) extension) ? extension : a;
      Decimal? nullable3;
      if (detail2 == null)
      {
        nullable1 = new Decimal?();
        nullable3 = nullable1;
      }
      else
        nullable3 = detail2.CuryTranDiscount;
      nullable1 = nullable3;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      Decimal num4 = num3 + valueOrDefault2;
      local2 = num4;
      ref Decimal local3 = ref tranExtPriceTotal;
      Decimal num5 = tranExtPriceTotal;
      Detail detail3 = this.Details.Cache.ObjectsEqual((object) a, (object) extension) ? extension : a;
      Decimal? nullable4;
      if (detail3 == null)
      {
        nullable1 = new Decimal?();
        nullable4 = nullable1;
      }
      else
        nullable4 = detail3.CuryTranExtPrice;
      nullable1 = nullable4;
      Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
      Decimal num6 = num5 + valueOrDefault3;
      local3 = num6;
    }
  }

  protected virtual void CalcTotals(object row, bool CalcTaxes)
  {
    bool flag1 = false;
    Decimal num1 = 0M;
    Decimal CuryInclTaxTotal = 0M;
    Decimal CuryWhTaxTotal = 0M;
    foreach (object selectTax in this.SelectTaxes(row, PXTaxCheck.RecalcTotals))
    {
      TaxTotal taxTotal = !CalcTaxes ? this.TaxTotals.Cache.GetExtension<TaxTotal>(((PXResult) selectTax)[0]) : this.TaxSummarize(selectTax, row);
      if (taxTotal != null && PXResult.Unwrap<PX.Objects.TX.Tax>(selectTax).TaxType == "P")
        flag1 = true;
      else if (taxTotal != null)
      {
        Decimal? nullable = taxTotal.CuryTaxAmt;
        Decimal num2 = nullable.Value;
        bool flag2 = taxTotal.IsTaxInclusive.Value;
        Decimal num3 = PXResult.Unwrap<PX.Objects.TX.Tax>(selectTax).ReverseTax.GetValueOrDefault() ? -1M : 1M;
        if (PXResult.Unwrap<PX.Objects.TX.Tax>(selectTax).TaxType == "W")
          CuryWhTaxTotal += num3 * num2;
        if (PXResult.Unwrap<PX.Objects.TX.Tax>(selectTax).TaxCalcLevel == "0" || flag2)
          CuryInclTaxTotal += num3 * num2;
        num1 += num3 * num2;
        if (PXResult.Unwrap<PX.Objects.TX.Tax>(selectTax).DeductibleVAT.GetValueOrDefault())
        {
          Decimal num4 = num1;
          Decimal num5 = num3;
          nullable = taxTotal.CuryExpenseAmt;
          Decimal num6 = nullable.Value;
          Decimal num7 = num5 * num6;
          num1 = num4 + num7;
          if (PXResult.Unwrap<PX.Objects.TX.Tax>(selectTax).TaxCalcLevel == "0")
          {
            Decimal num8 = CuryInclTaxTotal;
            Decimal num9 = num3;
            nullable = taxTotal.CuryExpenseAmt;
            Decimal num10 = nullable.Value;
            Decimal num11 = num9 * num10;
            CuryInclTaxTotal = num8 + num11;
          }
        }
      }
    }
    if (this.ParentGetStatus() != PXEntryStatus.Deleted && this.ParentGetStatus() != PXEntryStatus.InsertedDeleted)
      this.CalcDocTotals(row, num1, CuryInclTaxTotal, CuryWhTaxTotal);
    if (!flag1)
      return;
    this.Documents.Cache.RaiseExceptionHandling<Document.curyTaxTotal>((object) this.CurrentDocument, (object) num1, (Exception) new PXSetPropertyException("Use Tax is excluded from Tax Total.", PXErrorLevel.Warning));
  }

  protected virtual PXEntryStatus ParentGetStatus()
  {
    return this.Documents.Cache.GetStatus((object) this.CurrentDocument);
  }

  protected virtual void ParentSetValue(string fieldname, object value)
  {
    PXCache cache = this.Documents.Cache;
    if (this._ParentRow == null)
    {
      object copy = cache.CreateCopy(cache.Current);
      cache.SetValueExt(cache.Current, fieldname, value);
      if (cache.GetStatus(cache.Current) == PXEntryStatus.Notchanged)
        cache.SetStatus(cache.Current, PXEntryStatus.Updated);
      cache.RaiseRowUpdated(cache.Current, copy);
    }
    else
      cache.SetValueExt((object) this._ParentRow, fieldname, value);
  }

  protected virtual object ParentGetValue(string fieldname)
  {
    return this.Documents.Cache.GetValue((object) this.CurrentDocument, fieldname);
  }

  protected object ParentGetValue<Field>() where Field : IBqlField
  {
    return this.ParentGetValue(typeof (Field).Name.ToLower());
  }

  protected void ParentSetValue<Field>(object value) where Field : IBqlField
  {
    this.ParentSetValue(typeof (Field).Name.ToLower(), value);
  }

  protected virtual bool CompareZone(string zoneA, string zoneB)
  {
    if (!string.Equals(zoneA, zoneB, StringComparison.OrdinalIgnoreCase))
    {
      foreach (PXResult pxResult in PXSelectBase<TaxZoneDet, PXSelectGroupBy<TaxZoneDet, Where<TaxZoneDet.taxZoneID, Equal<Required<TaxZoneDet.taxZoneID>>, Or<TaxZoneDet.taxZoneID, Equal<Required<TaxZoneDet.taxZoneID>>>>, Aggregate<GroupBy<TaxZoneDet.taxID, Count>>>.Config>.Select((PXGraph) this.Base, (object) zoneA, (object) zoneB))
      {
        if (pxResult.RowCount.GetValueOrDefault() == 1)
          return false;
      }
    }
    return true;
  }

  /// <summary>The RowInserted event handler for the <see cref="T:PX.Objects.Extensions.SalesTax.Detail" /> mapped cache extension.</summary>
  /// <param name="sender">The cache object that raised the event.</param>
  /// <param name="e">Parameters of the event.</param>
  public virtual void Detail_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    TaxCalc valueOrDefault = this.CurrentDocument.TaxCalc.GetValueOrDefault();
    switch (valueOrDefault)
    {
      case TaxCalc.NoCalc:
      case TaxCalc.ManualLineCalc:
        Detail extension = sender.GetExtension<Detail>(e.Row);
        if (extension.TaxCategoryID == null && extension.CuryTranAmt.GetValueOrDefault() == 0M)
          break;
        if (valueOrDefault == TaxCalc.Calc)
        {
          this.Preload();
          this.DefaultTaxes(extension);
          this.CalcTaxes((object) extension, PXTaxCheck.Line);
          break;
        }
        if (valueOrDefault != TaxCalc.ManualCalc)
          break;
        this.CalcTotals((object) extension, false);
        break;
      default:
        if (!this.inserted.TryGetValue(e.Row, out object _))
        {
          this.inserted[e.Row] = sender.CreateCopy(e.Row);
          goto case TaxCalc.NoCalc;
        }
        goto case TaxCalc.NoCalc;
    }
  }

  /// <summary>The RowUpdated event handler</summary>
  /// <param name="sender">The cache object that raised the event.</param>
  /// <param name="e">Parameters of the event.</param>
  public virtual void Detail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    TaxCalc valueOrDefault = this.CurrentDocument.TaxCalc.GetValueOrDefault();
    switch (valueOrDefault)
    {
      case TaxCalc.NoCalc:
      case TaxCalc.ManualLineCalc:
        Detail extension1 = sender.GetExtension<Detail>(e.Row);
        Detail extension2 = sender.GetExtension<Detail>(e.OldRow);
        if (valueOrDefault == TaxCalc.Calc)
        {
          if (!object.Equals((object) extension2.TaxCategoryID, (object) extension1.TaxCategoryID))
          {
            this.Preload();
            this.ReDefaultTaxes(extension2, extension1);
          }
          else if (!object.Equals((object) extension2.TaxID, (object) extension1.TaxID))
          {
            TaxDetail instance = (TaxDetail) this.Taxes.Cache.CreateInstance();
            instance.RefTaxID = extension2.TaxID;
            this.DelOneTax(extension1, instance.RefTaxID);
            this.AddOneTax(extension1, (ITaxDetail) new TaxZoneDet()
            {
              TaxID = extension1.TaxID
            });
          }
          bool flag = false;
          if (TaxBaseGraph<TGraph, TPrimary>.ShouldRecalculateTaxesOnRowUpdate(extension1, extension2))
          {
            this.CalcTaxes((object) extension1, PXTaxCheck.Line);
            flag = true;
          }
          if (flag)
            break;
          this.CalcTotals((object) extension1, false);
          break;
        }
        if (valueOrDefault != TaxCalc.ManualCalc)
          break;
        this.CalcTotals((object) extension1, false);
        break;
      default:
        if (!this.updated.TryGetValue(e.Row, out object _))
        {
          this.updated[e.Row] = sender.CreateCopy(e.Row);
          goto case TaxCalc.NoCalc;
        }
        goto case TaxCalc.NoCalc;
    }
  }

  private static bool ShouldRecalculateTaxesOnRowUpdate(Detail row, Detail oldRow)
  {
    if (!object.Equals((object) oldRow.TaxCategoryID, (object) row.TaxCategoryID) || !object.Equals((object) oldRow.CuryTranAmt, (object) row.CuryTranAmt) || !object.Equals((object) oldRow.TaxID, (object) row.TaxID))
      return true;
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.perUnitTaxSupport>())
    {
      Decimal? qty1 = oldRow.Qty;
      Decimal? qty2 = row.Qty;
      if (!(qty1.GetValueOrDefault() == qty2.GetValueOrDefault() & qty1.HasValue == qty2.HasValue) || oldRow.UOM != row.UOM)
        return true;
    }
    return false;
  }

  /// <summary>The RowDeleted event handler for the <see cref="T:PX.Objects.Extensions.SalesTax.Detail" /> mapped cache extension.</summary>
  /// <param name="sender">The cache object that raised the event.</param>
  /// <param name="e">Parameters of the event.</param>
  public virtual void Detail_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    TaxCalc valueOrDefault = this.CurrentDocument.TaxCalc.GetValueOrDefault();
    switch (this.ParentGetStatus())
    {
      case PXEntryStatus.Deleted:
        break;
      case PXEntryStatus.InsertedDeleted:
        break;
      default:
        Detail extension = sender.GetExtension<Detail>(e.Row);
        if (extension.TaxCategoryID == null)
        {
          Decimal? curyTranAmt;
          if (!(curyTranAmt = extension.CuryTranAmt).HasValue)
            break;
          Decimal? nullable = curyTranAmt;
          Decimal num = 0M;
          if (nullable.GetValueOrDefault() == num & nullable.HasValue)
            break;
        }
        switch (valueOrDefault)
        {
          case TaxCalc.Calc:
          case TaxCalc.ManualLineCalc:
            this.ClearTaxes((object) extension);
            this.CalcTaxes((object) null, PXTaxCheck.Line);
            return;
          case TaxCalc.ManualCalc:
            this.CalcTotals((object) extension, false);
            return;
          default:
            return;
        }
    }
  }

  /// <summary>The RowPersisted event handler.</summary>
  /// <param name="sender">The cache object that raised the event.</param>
  /// <param name="e">Parameters of the event.</param>
  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != PXTranStatus.Completed)
      return;
    this.inserted?.Clear();
    this.updated?.Clear();
  }

  /// <summary>
  /// Should return View representing documentDetails. Required to make decisions about tax recalculations on CurrencyInfo_RowUpdated
  /// </summary>
  protected abstract PXView DocumentDetailsView { get; }

  /// <summary>The RowUpdated event handler for the CurrencyInfo DAC.</summary>
  /// <param name="sender">The cache object that raised the event.</param>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void CurrencyInfo_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    Document currentDocument1 = this.CurrentDocument;
    if ((currentDocument1 != null ? (currentDocument1.TaxCalc.GetValueOrDefault() == TaxCalc.Calc ? 1 : 0) : 0) == 0)
    {
      Document currentDocument2 = this.CurrentDocument;
      if ((currentDocument2 != null ? (currentDocument2.TaxCalc.GetValueOrDefault() == TaxCalc.ManualLineCalc ? 1 : 0) : 0) == 0)
        return;
    }
    if (e.Row == null || !((PX.Objects.CM.Extensions.CurrencyInfo) e.Row).CuryRate.HasValue || e.OldRow != null && sender.ObjectsEqual<PX.Objects.CM.Extensions.CurrencyInfo.curyRate, PX.Objects.CM.Extensions.CurrencyInfo.curyMultDiv>(e.Row, e.OldRow) || this.DocumentDetailsView?.SelectSingle() == null)
      return;
    this.CalcTaxes((object) null);
  }

  /// <summary>The FieldUpdated event handler for the <see cref="P:PX.Objects.Extensions.SalesTax.Document.CuryID" /> field of the <see cref="T:PX.Objects.Extensions.SalesTax.Document" /> mapped cache extension.</summary>
  /// <param name="sender">The cache object that raised the event.</param>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void Document_CuryID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.ParentFieldUpdated(sender, e);
  }

  /// <summary>The FieldUpdated event handler for the <see cref="P:PX.Objects.Extensions.SalesTax.Document.TermsID" /> field of the <see cref="T:PX.Objects.Extensions.SalesTax.Document" /> mapped cache extension.</summary>
  /// <param name="sender">The cache object that raised the event.</param>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void Document_TermsID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.ParentFieldUpdated(sender, e);
  }

  protected virtual void ParentFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    Document extension = sender.GetExtension<Document>(e.Row);
    if (extension.TaxCalc.GetValueOrDefault() != TaxCalc.Calc && extension.TaxCalc.GetValueOrDefault() != TaxCalc.ManualLineCalc)
      return;
    if (e.Row.GetType() == sender.GetItemType())
      this._ParentRow = extension;
    this.CalcTaxes((object) null);
    this._ParentRow = (Document) null;
  }

  /// <summary>The FieldUpdated event handler for the <see cref="P:PX.Objects.Extensions.SalesTax.Document.IsTaxSaved" /> field of the <see cref="T:PX.Objects.Extensions.SalesTax.Document" /> mapped cache extension.</summary>
  /// <param name="sender">The cache object that raised the event.</param>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void Document_IsTaxSaved_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    Document extension = sender.GetExtension<Document>(e.Row);
    if (!(extension.CuryLineTotal.GetValueOrDefault() == 0M))
      return;
    this.CalcDocTotals((object) extension, extension.CuryTaxTotal.GetValueOrDefault(), 0M, extension.CuryWhTaxTotal.GetValueOrDefault());
  }

  protected virtual List<object> ChildSelect(PXCache cache, object data)
  {
    return TaxParentAttribute.ChildSelect(cache, data, this.GetDocumentMapping().Table);
  }

  /// <summary>The FieldUpdated event handler for the <see cref="P:PX.Objects.Extensions.SalesTax.Document.TaxZoneID" /> field of the <see cref="T:PX.Objects.Extensions.SalesTax.Document" /> mapped cache extension.</summary>
  /// <param name="sender">The cache object that raised the event.</param>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void Document_TaxZoneID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    Document extension = sender.GetExtension<Document>(e.Row);
    if (this.IsExternalTax(sender.Graph, (string) e.OldValue))
      extension.TaxCalc = new TaxCalc?(TaxCalc.Calc);
    if (this.IsExternalTax(sender.Graph, extension.TaxZoneID) || extension.ExternalTaxesImportInProgress.GetValueOrDefault())
      extension.TaxCalc = new TaxCalc?(TaxCalc.ManualCalc);
    if (extension.TaxCalc.GetValueOrDefault() != TaxCalc.Calc && extension.TaxCalc.GetValueOrDefault() != TaxCalc.ManualLineCalc || this.CompareZone((string) e.OldValue, extension.TaxZoneID) && extension.TaxZoneID != null)
      return;
    this.Preload();
    this.ReDefaultTaxes(this.SelectDetails());
    this._ParentRow = extension;
    this.CalcTaxes((object) null);
    this._ParentRow = (Document) null;
  }

  protected virtual void ReDefaultTaxes(PXResultset<Detail> details)
  {
    foreach (PXResult<Detail> detail1 in details)
    {
      Detail detail2 = (Detail) detail1;
      this.ClearTaxes((object) detail2);
      this.ClearChildTaxAmts(detail2);
    }
    foreach (PXResult<Detail> detail in details)
      this.DefaultTaxes((Detail) detail, false);
  }

  protected virtual void ClearChildTaxAmts(Detail childRow)
  {
    PXCache cache = this.Details.Cache;
    this.SetTaxableAmt((object) childRow, new Decimal?(0M));
    this.SetTaxAmt((object) childRow, new Decimal?(0M));
    if (cache.Locate((object) childRow) == null || cache.GetStatus((object) childRow) != PXEntryStatus.Notchanged && cache.GetStatus((object) childRow) != PXEntryStatus.Held)
      return;
    cache.Update((object) childRow);
  }

  protected virtual void ReDefaultTaxes(Detail clearDet, Detail defaultDet, bool defaultExisting = true)
  {
    this.ClearTaxes((object) clearDet);
    this.ClearChildTaxAmts(clearDet);
    this.DefaultTaxes(defaultDet, defaultExisting);
  }

  /// <summary>The FieldUpdated event handler for the <see cref="P:PX.Objects.Extensions.SalesTax.Document.DocumentDate" /> field of the <see cref="T:PX.Objects.Extensions.SalesTax.Document" /> mapped cache extension.</summary>
  /// <param name="sender">The cache object that raised the event.</param>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void Document_DocumentDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    Document extension = sender.GetExtension<Document>(e.Row);
    if (extension.TaxCalc.GetValueOrDefault() != TaxCalc.Calc && extension.TaxCalc.GetValueOrDefault() != TaxCalc.ManualLineCalc)
      return;
    this.Preload();
    foreach (PXResult<Detail> selectDetail in this.SelectDetails())
    {
      Detail detail = (Detail) selectDetail;
      this.ReDefaultTaxes(detail, detail);
    }
    this._ParentRow = extension;
    this._NoSumTaxable = true;
    try
    {
      this.CalcTaxes((object) null);
    }
    finally
    {
      this._ParentRow = (Document) null;
      this._NoSumTaxable = false;
    }
  }

  /// <summary>The FieldUpdated event handler for the <see cref="P:PX.Objects.Extensions.SalesTax.Document.FinPeriodID" /> field of the <see cref="T:PX.Objects.Extensions.SalesTax.Document" /> mapped cache extension.</summary>
  /// <param name="sender">The cache object that raised the event.</param>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void Document_FinPeriodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    Document extension = sender.GetExtension<Document>(e.Row);
    if (extension.TaxCalc.GetValueOrDefault() != TaxCalc.Calc && extension.TaxCalc.GetValueOrDefault() != TaxCalc.ManualLineCalc)
      return;
    PXCache cache = this.TaxTotals.Cache;
    foreach (object obj in TaxParentAttribute.ChildSelect(cache, e.Row, sender.GetItemType()))
    {
      if (cache.GetStatus(obj) == PXEntryStatus.Notchanged || cache.GetStatus(obj) == PXEntryStatus.Held)
        cache.SetStatus(obj, PXEntryStatus.Updated);
    }
  }

  protected abstract void SetExtCostExt(PXCache sender, object child, Decimal? value);

  protected abstract string GetExtCostLabel(PXCache sender, object row);

  protected string GetTaxCalcMode()
  {
    if (!this.IsTaxCalcModeEnabled)
      throw new PXException("Document Tax Calculation mode is not enabled!");
    string taxCalcMode = (string) this.ParentGetValue<Document.taxCalcMode>();
    if (string.IsNullOrWhiteSpace(taxCalcMode))
      taxCalcMode = "T";
    return taxCalcMode;
  }

  /// <summary>The FieldUpdated event handler for the <see cref="P:PX.Objects.Extensions.SalesTax.Document.TaxCalcMode" /> field of the <see cref="T:PX.Objects.Extensions.SalesTax.Document" /> mapped cache extension.</summary>
  /// <param name="sender">The cache object that raised the event.</param>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void Document_TaxCalcMode_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    Document extension = sender.GetExtension<Document>(e.Row);
    string taxCalcMode = extension.TaxCalcMode;
    if (!(taxCalcMode != (string) e.OldValue))
      return;
    PXCache cache = this.Details.Cache;
    PXResultset<Detail> pxResultset = this.SelectDetails();
    bool grossOnDocumentLevel = this.CalcGrossOnDocumentLevel;
    Decimal? curyTaxTotal = extension.CuryTaxTotal;
    if (pxResultset != null && pxResultset.Count != 0 && !grossOnDocumentLevel && curyTaxTotal.HasValue && curyTaxTotal.Value != 0M && this.ConfirmRecalculateExtCost(sender, (Detail) pxResultset[0]))
    {
      foreach (PXResult<Detail> pxResult in pxResultset)
      {
        TaxDetail taxDetail1 = this.TaxSummarizeOneLine((object) pxResult, TaxBaseGraph<TGraph, TPrimary>.SummType.All);
        Decimal? curyTaxableAmt1;
        if (taxDetail1 != null)
        {
          switch (taxCalcMode)
          {
            case "N":
              curyTaxableAmt1 = taxDetail1.CuryTaxableAmt;
              this.SetExtCostExt(cache, (object) pxResult, new Decimal?(this.Base.FindImplementation<IPXCurrencyHelper>().RoundCury(curyTaxableAmt1.Value)));
              continue;
            case "G":
              curyTaxableAmt1 = taxDetail1.CuryTaxableAmt;
              Decimal? curyTaxAmt1 = taxDetail1.CuryTaxAmt;
              this.SetExtCostExt(cache, (object) pxResult, new Decimal?(this.Base.FindImplementation<IPXCurrencyHelper>().RoundCury(curyTaxableAmt1.Value + curyTaxAmt1.Value)));
              continue;
            case "T":
              TaxDetail taxDetail2 = this.TaxSummarizeOneLine((object) pxResult, TaxBaseGraph<TGraph, TPrimary>.SummType.Inclusive);
              Decimal? nullable1;
              if (taxDetail2 == null)
              {
                nullable1 = taxDetail1.CuryTaxableAmt;
              }
              else
              {
                Decimal? curyTaxableAmt2 = taxDetail2.CuryTaxableAmt;
                Decimal? curyTaxAmt2 = taxDetail2.CuryTaxAmt;
                nullable1 = curyTaxableAmt2.HasValue & curyTaxAmt2.HasValue ? new Decimal?(curyTaxableAmt2.GetValueOrDefault() + curyTaxAmt2.GetValueOrDefault()) : new Decimal?();
              }
              Decimal? nullable2 = nullable1;
              this.SetExtCostExt(cache, (object) pxResult, new Decimal?(this.Base.FindImplementation<IPXCurrencyHelper>().RoundCury(nullable2.Value)));
              continue;
            default:
              continue;
          }
        }
      }
    }
    this.Preload();
    foreach (PXResult<Detail> pxResult in pxResultset)
    {
      Detail detail = (Detail) pxResult;
      this.ReDefaultTaxes(detail, detail, false);
    }
    this._ParentRow = extension;
    this.CalcTaxes((object) null);
    this._ParentRow = (Document) null;
  }

  private bool ConfirmRecalculateExtCost(PXCache sender, Detail detail)
  {
    if (!this.AskConfirmationToRecalculateExtCost)
      return true;
    return sender.Graph.Views[sender.Graph.PrimaryView].Ask(PXLocalizer.LocalizeFormat("Do you want the system to recalculate the amount(s) in the '{0}' column?", (object) this.GetExtCostLabel(this.Details.Cache, (object) detail)), MessageButtons.YesNo) == WebDialogResult.Yes;
  }

  private TaxDetail TaxSummarizeOneLine(
    object row,
    TaxBaseGraph<TGraph, TPrimary>.SummType summType)
  {
    List<object> list = new List<object>();
    switch (summType)
    {
      case TaxBaseGraph<TGraph, TPrimary>.SummType.Inclusive:
        list = !this.CalcGrossOnDocumentLevel || !this.IsTaxCalcModeEnabled ? this.SelectTaxes<Where<PX.Objects.TX.Tax.taxCalcLevel, Equal<CSTaxCalcLevel.inclusive>, And<PX.Objects.TX.Tax.taxCalcType, Equal<CSTaxCalcType.item>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.directTax, Equal<False>>>>>>((PXGraph) this.Base, row, PXTaxCheck.Line) : this.SelectTaxes<Where<PX.Objects.TX.Tax.taxCalcLevel, Equal<CSTaxCalcLevel.inclusive>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.directTax, Equal<False>>>>>((PXGraph) this.Base, row, PXTaxCheck.Line);
        break;
      case TaxBaseGraph<TGraph, TPrimary>.SummType.All:
        list = !this.CalcGrossOnDocumentLevel || !this.IsTaxCalcModeEnabled ? this.SelectTaxes<Where<PX.Objects.TX.Tax.taxCalcLevel, NotEqual<CSTaxCalcLevel.calcOnItemAmtPlusTaxAmt>, And<PX.Objects.TX.Tax.taxCalcType, Equal<CSTaxCalcType.item>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.directTax, Equal<False>>>>>>((PXGraph) this.Base, row, PXTaxCheck.Line) : this.SelectTaxes<Where<PX.Objects.TX.Tax.taxCalcLevel, NotEqual<CSTaxCalcLevel.calcOnItemAmtPlusTaxAmt>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.directTax, Equal<False>>>>>((PXGraph) this.Base, row, PXTaxCheck.Line);
        break;
    }
    if (list.Count == 0)
      return (TaxDetail) null;
    PXCache cache = this.Taxes.Cache;
    TaxDetail instance = (TaxDetail) cache.CreateInstance();
    Decimal? curyTaxableAmt = cache.GetExtension<TaxDetail>(((PXResult) list[0])[0]).CuryTaxableAmt;
    Decimal? nullable1 = new Decimal?(this.SumWithReverseAdjustment(list, this.GetTaxDetailMapping().Table, this.GetTaxDetailMapping().CuryTaxAmt));
    Decimal? nullable2 = new Decimal?(this.SumWithReverseAdjustment(list, this.GetTaxDetailMapping().Table, this.GetTaxDetailMapping().CuryExpenseAmt));
    instance.CuryTaxableAmt = curyTaxableAmt;
    TaxDetail taxDetail = instance;
    Decimal? nullable3 = nullable1;
    Decimal? nullable4 = nullable2;
    Decimal? nullable5 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
    taxDetail.CuryTaxAmt = nullable5;
    return instance;
  }

  private Decimal SumWithReverseAdjustment(List<object> list, System.Type table, System.Type field)
  {
    Decimal ret = 0.0M;
    list.ForEach((System.Action<object>) (a =>
    {
      Decimal? nullable = (Decimal?) this.Base.Caches[table].GetValue(((PXResult) a)[table], field.Name);
      Decimal num = ((PX.Objects.TX.Tax) ((PXResult) a)[typeof (PX.Objects.TX.Tax)]).ReverseTax.GetValueOrDefault() ? -1M : 1M;
      ret += nullable.GetValueOrDefault() * num;
    }));
    return ret;
  }

  /// <summary>The RowInserting event handler for the <see cref="T:PX.Objects.Extensions.SalesTax.TaxTotal" /> mapped cache extension.</summary>
  /// <param name="cache">The cache object that raised the event.</param>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void TaxTotal_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    TaxTotal extension = cache.GetExtension<TaxTotal>(e.Row);
    if (extension == null)
      return;
    Dictionary<string, object> keyFieldValues1 = this.GetKeyFieldValues(cache, extension);
    bool flag1 = true;
    if (ExternalTaxBase<PXGraph>.IsExternalTax(cache.Graph, extension.TaxZoneID))
      return;
    foreach (object extrow in cache.Cached)
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      Dictionary<string, object> keyFieldValues2 = this.GetKeyFieldValues(cache, (TaxTotal) extrow);
      bool flag2 = true;
      switch (cache.GetStatus(extrow))
      {
        case PXEntryStatus.Deleted:
        case PXEntryStatus.InsertedDeleted:
          continue;
        default:
          foreach (KeyValuePair<string, object> keyValuePair in keyFieldValues1)
          {
            if (keyFieldValues2.ContainsKey(keyValuePair.Key) && !object.Equals(keyFieldValues2[keyValuePair.Key], keyValuePair.Value))
            {
              flag2 = false;
              break;
            }
          }
          if (flag2)
          {
            if (cache.Graph.IsMobile)
            {
              cache.Delete(extrow);
              continue;
            }
            flag1 = false;
            goto label_22;
          }
          continue;
      }
    }
label_22:
    if (flag1)
      return;
    e.Cancel = true;
  }

  private Dictionary<string, object> GetKeyFieldValues(PXCache extCache, TaxTotal extrow)
  {
    object main = extCache.GetMain<TaxTotal>(extrow);
    PXCache cach = extCache.Graph.Caches[main.GetType()];
    Dictionary<string, object> keyFieldValues = new Dictionary<string, object>();
    foreach (string key in (IEnumerable<string>) cach.Keys)
    {
      if (key != this._RecordID)
        keyFieldValues.Add(key, cach.GetValue(main, key));
    }
    return keyFieldValues;
  }

  protected virtual void DelOneTax(Detail detrow, string taxID)
  {
    foreach (PXResult selectTax in this.SelectTaxes((object) detrow, PXTaxCheck.Line))
    {
      TaxDetail extension = this.Taxes.Cache.GetExtension<TaxDetail>(selectTax[0]);
      if (extension.RefTaxID == taxID)
        this.Taxes.Delete(extension);
    }
  }

  protected virtual void Preload() => this.SelectTaxes((object) null, PXTaxCheck.RecalcTotals);

  /// <summary>Overrides the <tt>Initialize</tt> method of the base graph.</summary>
  public override void Initialize()
  {
    base.Initialize();
    if (this.GetDetailMapping().CuryInfoID != typeof (Detail.curyInfoID))
      this.Base.RowUpdated.AddHandler<PX.Objects.CM.Extensions.CurrencyInfo>(new PXRowUpdated(this.CurrencyInfo_RowUpdated));
    PXUIFieldAttribute.SetVisible<PX.Objects.TX.Tax.exemptTax>(this.Base.Caches[typeof (PX.Objects.TX.Tax)], (object) null, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.TX.Tax.statisticalTax>(this.Base.Caches[typeof (PX.Objects.TX.Tax)], (object) null, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.TX.Tax.reverseTax>(this.Base.Caches[typeof (PX.Objects.TX.Tax)], (object) null, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.TX.Tax.pendingTax>(this.Base.Caches[typeof (PX.Objects.TX.Tax)], (object) null, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.TX.Tax.taxType>(this.Base.Caches[typeof (PX.Objects.TX.Tax)], (object) null, false);
    this.inserted = new Dictionary<object, object>();
    this.updated = new Dictionary<object, object>();
  }

  protected virtual Decimal? GetCuryTranAmt(PXCache sender, object row)
  {
    Decimal? nullable1 = (Decimal?) sender.GetValue(row, typeof (Detail.curyTranAmt).Name);
    Decimal? nullable2 = (Decimal?) sender.GetValue(row, typeof (Detail.groupDiscountRate).Name);
    Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable4 = (Decimal?) sender.GetValue(row, typeof (Detail.documentDiscountRate).Name);
    Decimal? nullable5 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * nullable4.GetValueOrDefault()) : new Decimal?();
    return new Decimal?(sender.Graph.FindImplementation<IPXCurrencyHelper>().RoundCury(nullable5.Value));
  }

  protected virtual string GetTaxCategory(PXCache sender, object row)
  {
    return (string) sender.GetValue(row, typeof (Detail.taxCategoryID).Name);
  }

  protected virtual IComparer<PX.Objects.TX.Tax> GetTaxByCalculationLevelComparer()
  {
    return (IComparer<PX.Objects.TX.Tax>) TaxByCalculationLevelComparer.Instance;
  }

  protected virtual bool IsPerUnitTax(PX.Objects.TX.Tax tax) => tax?.TaxType == "Q";

  /// <summary>Fill tax details for line for per unit taxes.</summary>
  /// <exception cref="T:PX.Data.PXArgumentException">Thrown when a PX Argument error condition occurs.</exception>
  /// <param name="row">The row.</param>
  /// <param name="tax">The tax.</param>
  /// <param name="taxRevision">The tax revision.</param>
  /// <param name="taxDetail">The tax detail.</param>
  protected virtual void TaxSetLineDefaultForPerUnitTaxes(
    Detail row,
    PX.Objects.TX.Tax tax,
    TaxRev taxRevision,
    TaxDetail taxDetail)
  {
    string taxCalcLevel = tax.TaxCalcLevel;
    if (taxCalcLevel == "1" || taxCalcLevel == "0")
    {
      Decimal quantityForPerUnitTaxes = this.GetTaxableQuantityForPerUnitTaxes(row, tax, taxRevision);
      Decimal curyTaxAmount = this.GetTaxAmountForPerUnitTaxWithCorrectSign(row, tax, taxRevision, taxDetail, quantityForPerUnitTaxes).CuryTaxAmount;
      this.FillTaxDetailValuesForPerUnitTax(tax, taxRevision, taxDetail, row, quantityForPerUnitTaxes, curyTaxAmount);
    }
    else
    {
      PXTrace.WriteError("The calculation level {0} for the per-unit tax is not supported.");
      throw new PXArgumentException("The calculation level {0} for the per-unit tax is not supported.");
    }
  }

  private Decimal GetTaxableQuantityForPerUnitTaxes(Detail row, PX.Objects.TX.Tax tax, TaxRev taxRevison)
  {
    if (row.Qty.HasValue)
    {
      Decimal? qty = row.Qty;
      Decimal num = 0M;
      if (!(qty.GetValueOrDefault() == num & qty.HasValue) && tax.TaxUOM != null)
      {
        Decimal? taxUom = this.ConvertLineQtyToTaxUOM(row, tax);
        return !taxUom.HasValue ? 0M : this.GetAdjustedTaxableQuantity(taxUom.Value, taxRevison);
      }
    }
    return 0M;
  }

  private Decimal GetAdjustedTaxableQuantity(Decimal lineQty, TaxRev taxRevison)
  {
    return !taxRevison.TaxableMaxQty.HasValue || lineQty <= taxRevison.TaxableMaxQty.Value ? lineQty : taxRevison.TaxableMaxQty.Value;
  }

  protected virtual Decimal? ConvertLineQtyToTaxUOM(Detail row, PX.Objects.TX.Tax tax)
  {
    if (tax.TaxUOM == row.UOM)
      return row.Qty;
    if (!row.InventoryID.HasValue && string.IsNullOrEmpty(row.UOM))
    {
      string errorMessage = PXMessages.LocalizeFormatNoPrefixNLA("The {0} per-unit tax cannot be calculated for the document with the {1} tax zone if inventory item and UOM are not specified for the line with the {2} tax category.", (object) tax.TaxID, (object) this.CurrentDocument.TaxZoneID, (object) row.TaxCategoryID);
      this.SetPerUnitTaxUomConversionError(row, tax, errorMessage);
      return new Decimal?();
    }
    Decimal? taxUom = new Decimal?();
    if (row.InventoryID.HasValue)
    {
      try
      {
        Decimal quantityInBaseUom = this.GetNotRoundedLineQuantityInBaseUOM(row);
        taxUom = new Decimal?(INUnitAttribute.ConvertFromBase(this.Details.Cache, row.InventoryID, tax.TaxUOM, quantityInBaseUom, INPrecision.QUANTITY));
      }
      catch (PXUnitConversionException ex)
      {
        taxUom = new Decimal?();
      }
    }
    if (!taxUom.HasValue)
    {
      taxUom = this.ConvertLineQtyToTaxUOMWithGlobalConversions(row, tax);
      if (!taxUom.HasValue)
      {
        string errorMessage = PXMessages.LocalizeFormatNoPrefixNLA("The {0} per-unit tax cannot be calculated for the document with the {1} tax zone and the line with the {2} tax category. Conversion rule to the {3} tax UOM is missing.", (object) tax.TaxID, (object) this.CurrentDocument.TaxZoneID, (object) row.TaxCategoryID, (object) tax.TaxUOM);
        this.SetPerUnitTaxUomConversionError(row, tax, errorMessage);
      }
    }
    return taxUom;
  }

  protected virtual Decimal GetNotRoundedLineQuantityInBaseUOM(Detail row)
  {
    return INUnitAttribute.ConvertToBase(this.Details.Cache, row.InventoryID, row.UOM, row.Qty.Value, INPrecision.NOROUND);
  }

  protected Decimal? ConvertLineQtyToTaxUOMWithGlobalConversions(Detail row, PX.Objects.TX.Tax tax)
  {
    Decimal result;
    return INUnitAttribute.TryConvertGlobalUnits((PXGraph) this.Base, row.UOM, tax.TaxUOM, row.Qty.Value, INPrecision.QUANTITY, out result) ? new Decimal?(result) : new Decimal?();
  }

  protected virtual void SetPerUnitTaxUomConversionError(Detail row, PX.Objects.TX.Tax tax, string errorMessage)
  {
    PXException pxException = (PXException) new PXSetPropertyException(errorMessage, PXErrorLevel.Error);
    this.Details.Cache.RaiseExceptionHandling(this.GetDetailMapping().UOM.Name, (object) row, (object) row.UOM, (Exception) pxException);
  }

  protected virtual (Decimal TaxAmount, Decimal CuryTaxAmount) GetTaxAmountForPerUnitTaxWithCorrectSign(
    Detail row,
    PX.Objects.TX.Tax tax,
    TaxRev taxRevison,
    TaxDetail taxDetail,
    Decimal taxableQty)
  {
    (Decimal TaxAmount, Decimal CuryTaxAmount) = this.GetTaxAmountForPerUnitTax(taxRevison, taxDetail, taxableQty);
    if (TaxAmount == 0M && CuryTaxAmount == 0M)
      return (TaxAmount, CuryTaxAmount);
    return this.InvertPerUnitTaxAmountSign(row, tax, taxRevison, taxDetail) ? (-TaxAmount, -CuryTaxAmount) : (TaxAmount, CuryTaxAmount);
  }

  protected virtual (Decimal TaxAmount, Decimal CuryTaxAmount) GetTaxAmountForPerUnitTax(
    TaxRev taxRevison,
    TaxDetail taxDetail,
    Decimal taxableQty)
  {
    Decimal rateForPerUnitTaxes = this.GetTaxRateForPerUnitTaxes(taxRevison);
    Decimal num = taxableQty * rateForPerUnitTaxes;
    PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = this.Base.FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo();
    return (defaultCurrencyInfo.RoundCury(num), defaultCurrencyInfo.CuryConvCury(num));
  }

  protected virtual Decimal GetTaxRateForPerUnitTaxes(TaxRev taxRevison)
  {
    Decimal? taxRate = taxRevison.TaxRate;
    Decimal num = 0M;
    return !(taxRate.GetValueOrDefault() > num & taxRate.HasValue) ? 0M : taxRevison.TaxRate.Value;
  }

  protected virtual bool InvertPerUnitTaxAmountSign(
    Detail row,
    PX.Objects.TX.Tax tax,
    TaxRev taxRevison,
    TaxDetail taxDetail)
  {
    return false;
  }

  protected virtual void FillTaxDetailValuesForPerUnitTax(
    PX.Objects.TX.Tax tax,
    TaxRev taxRevision,
    TaxDetail taxDetail,
    Detail row,
    Decimal taxableQty,
    Decimal curyTaxAmount)
  {
    taxDetail.TaxUOM = tax.TaxUOM;
    taxDetail.TaxableQty = new Decimal?(taxableQty);
    taxDetail.TaxRate = taxRevision.TaxRate;
    taxDetail.NonDeductibleTaxRate = taxRevision.NonDeductibleTaxRate;
    switch (tax.TaxCalcLevel)
    {
      case "0":
        this.FillLineTaxableAndTaxAmountsForInclusivePerUnitTax(taxDetail, row, tax);
        break;
      case "1":
        bool? calcLevel2Exclude = tax.TaxCalcLevel2Exclude;
        bool flag = false;
        if (calcLevel2Exclude.GetValueOrDefault() == flag & calcLevel2Exclude.HasValue)
        {
          this.CheckThatExclusivePerUnitTaxIsNotUsedWithInclusiveNonPerUnitTax(row);
          break;
        }
        break;
    }
    if (!this.IsExemptTaxCategory((object) row))
      this.SetTaxDetailTaxAmount(this.Taxes.Cache, taxDetail, new Decimal?(curyTaxAmount));
    if (taxRevision.TaxID != null && !tax.DirectTax.GetValueOrDefault())
      this.Taxes.Update(taxDetail);
    else
      this.Delete(this.Taxes.Cache, (object) taxDetail);
  }

  private void CheckThatExclusivePerUnitTaxIsNotUsedWithInclusiveNonPerUnitTax(Detail row)
  {
    if (this.SelectInclusiveTaxes((object) row).Select<object, PX.Objects.TX.Tax>((Func<object, PX.Objects.TX.Tax>) (taxRow => PXResult.Unwrap<PX.Objects.TX.Tax>(taxRow))).Any<PX.Objects.TX.Tax>((Func<PX.Objects.TX.Tax, bool>) (inclusiveTax => inclusiveTax != null && !this.IsPerUnitTax(inclusiveTax))))
    {
      PXTrace.WriteInformation("This combination of inclusive taxes and exclusive per-unit taxes is forbidden. Please review and modify your tax settings on the Taxes (TX205000) form.");
      throw new PXSetPropertyException("This combination of inclusive taxes and exclusive per-unit taxes is forbidden. Please review and modify your tax settings on the Taxes (TX205000) form.", PXErrorLevel.Error);
    }
  }

  private void FillLineTaxableAndTaxAmountsForInclusivePerUnitTax(
    TaxDetail taxDetail,
    Detail row,
    PX.Objects.TX.Tax tax)
  {
    Decimal valueOrDefault = this.GetCuryTranAmt(this.Details.Cache, (object) row).GetValueOrDefault();
    Decimal taxesTotalAmount = this.GetInclusivePerUnitTaxesTotalAmount(taxDetail, row);
    Decimal num1 = taxesTotalAmount;
    Decimal num2 = valueOrDefault - num1;
    this.SetTaxableAmt((object) row, new Decimal?(num2));
    this.SetTaxAmt((object) row, new Decimal?(taxesTotalAmount));
  }

  private Decimal GetInclusivePerUnitTaxesTotalAmount(TaxDetail taxDetail, Detail row)
  {
    IEnumerable<object> inclusivePerUnitTaxRows = this.GetInclusivePerUnitTaxRows(row);
    List<object> list = inclusivePerUnitTaxRows != null ? inclusivePerUnitTaxRows.ToList<object>() : (List<object>) null;
    if (list == null || list.Count == 0)
      return 0M;
    Decimal taxesTotalAmount = 0M;
    foreach (PXResult pxResult in list)
    {
      TaxRev taxRevison = pxResult.GetItem<TaxRev>();
      PX.Objects.TX.Tax tax = pxResult.GetItem<PX.Objects.TX.Tax>();
      Decimal quantityForPerUnitTaxes = this.GetTaxableQuantityForPerUnitTaxes(row, tax, taxRevison);
      Decimal? taxRate = taxRevison.TaxRate;
      Decimal num = this.GetTaxAmountForPerUnitTaxWithCorrectSign(row, tax, taxRevison, taxDetail, quantityForPerUnitTaxes).CuryTaxAmount;
      if (tax.ReverseTax.GetValueOrDefault())
        num = -num;
      taxesTotalAmount += num;
    }
    return taxesTotalAmount;
  }

  private IEnumerable<object> GetInclusivePerUnitTaxRows(Detail row)
  {
    List<object> objectList = this.SelectInclusiveTaxes((object) row);
    if (objectList != null && objectList.Count != 0)
    {
      foreach (PXResult inclusivePerUnitTaxRow in objectList)
      {
        PX.Objects.TX.Tax tax = inclusivePerUnitTaxRow.GetItem<PX.Objects.TX.Tax>();
        if (tax != null && this.IsPerUnitTax(tax))
          yield return (object) inclusivePerUnitTaxRow;
      }
    }
  }

  /// <summary>Fill aggregated tax detail for per unit tax.</summary>
  /// <param name="rowCache">The row cache.</param>
  /// <param name="row">The row.</param>
  /// <param name="tax">The tax.</param>
  /// <param name="taxRevision">The tax revision.</param>
  /// <param name="aggrTaxDetail">The aggregated tax detail.</param>
  /// <param name="taxItems">The tax items.</param>
  /// <returns />
  protected virtual TaxTotal FillAggregatedTaxDetailForPerUnitTax(
    object row,
    PX.Objects.TX.Tax tax,
    TaxRev taxRevision,
    TaxTotal aggrTaxDetail,
    List<object> taxItems)
  {
    aggrTaxDetail.TaxableQty = new Decimal?(this.Sum(taxItems, typeof (TaxDetail.taxableQty)));
    aggrTaxDetail.TaxUOM = tax.TaxUOM;
    return aggrTaxDetail;
  }

  protected virtual Decimal GetPerUnitTaxAmountForTaxableAdjustmentCalculation(
    PX.Objects.TX.Tax taxForTaxableAdustment,
    TaxDetail taxDetail,
    Detail row)
  {
    if (taxForTaxableAdustment.TaxType == "Q")
      return 0M;
    PerUnitTaxesAdjustmentToTaxableCalculator adjustmentCalculator = this.GetPerUnitTaxAdjustmentCalculator();
    return adjustmentCalculator == null ? 0M : adjustmentCalculator?.GetPerUnitTaxAmountForTaxableAdjustmentCalculation(taxForTaxableAdustment, this.Taxes.Cache, (object) row, this.Details.Cache, "CuryTaxAmt", (Func<List<object>>) (() => this.SelectPerUnitTaxesForTaxableAdjustmentCalculation(this.Taxes.Cache.Graph, (object) row))).GetValueOrDefault();
  }

  protected virtual PerUnitTaxesAdjustmentToTaxableCalculator GetPerUnitTaxAdjustmentCalculator()
  {
    return PerUnitTaxesAdjustmentToTaxableCalculator.Instance;
  }

  protected virtual List<object> SelectPerUnitTaxesForTaxableAdjustmentCalculation(
    PXGraph graph,
    object row)
  {
    return !this.IsExemptTaxCategory(row) ? this.SelectTaxes<Where<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.perUnit>, And<PX.Objects.TX.Tax.taxCalcLevel2Exclude, Equal<False>>>>(graph, row, PXTaxCheck.Line) : new List<object>();
  }

  /// <summary>Defines the default mapping of the <see cref="T:PX.Objects.Extensions.SalesTax.Document" /> mapped cache extension to a DAC.</summary>
  protected class DocumentMapping : IBqlMapping
  {
    /// <exclude />
    protected System.Type _table;
    /// <exclude />
    public System.Type BranchID = typeof (Document.branchID);
    /// <exclude />
    public System.Type CuryID = typeof (Document.curyID);
    /// <exclude />
    public System.Type CuryInfoID = typeof (Document.curyInfoID);
    /// <exclude />
    public System.Type DocumentDate = typeof (Document.documentDate);
    /// <exclude />
    public System.Type FinPeriodID = typeof (Document.finPeriodID);
    /// <exclude />
    public System.Type TaxZoneID = typeof (Document.taxZoneID);
    /// <exclude />
    public System.Type TermsID = typeof (Document.termsID);
    /// <exclude />
    public System.Type CuryLinetotal = typeof (Document.curyLineTotal);
    /// <exclude />
    public System.Type CuryDiscountLineTotal = typeof (Document.curyDiscountLineTotal);
    /// <exclude />
    public System.Type CuryExtPriceTotal = typeof (Document.curyExtPriceTotal);
    /// <exclude />
    public System.Type CuryDocBal = typeof (Document.curyDocBal);
    /// <exclude />
    public System.Type CuryTaxTotal = typeof (Document.curyTaxTotal);
    /// <exclude />
    public System.Type CuryDiscTot = typeof (Document.curyDiscTot);
    /// <exclude />
    public System.Type CuryDiscAmt = typeof (Document.curyDiscAmt);
    /// <exclude />
    public System.Type CuryOrigWhTaxAmt = typeof (Document.curyOrigWhTaxAmt);
    /// <exclude />
    public System.Type CuryTaxRoundDiff = typeof (Document.curyTaxRoundDiff);
    /// <exclude />
    public System.Type TaxRoundDiff = typeof (Document.taxRoundDiff);
    /// <exclude />
    public System.Type IsTaxSaved = typeof (Document.isTaxSaved);
    /// <exclude />
    public System.Type TaxCalcMode = typeof (Document.taxCalcMode);

    /// <exclude />
    public System.Type Extension => typeof (Document);

    /// <exclude />
    public System.Type Table => this._table;

    /// <summary>Creates the default mapping of the <see cref="T:PX.Objects.Extensions.SalesTax.Document" /> mapped cache extension to the specified table.</summary>
    /// <param name="table">A DAC.</param>
    public DocumentMapping(System.Type table) => this._table = table;
  }

  /// <summary>Defines the default mapping of the <see cref="T:PX.Objects.Extensions.SalesTax.Detail" /> mapped cache extension to a DAC.</summary>
  protected class DetailMapping : IBqlMapping
  {
    /// <exclude />
    protected System.Type _table;
    /// <exclude />
    public System.Type CuryInfoID = typeof (Detail.curyInfoID);
    /// <exclude />
    public System.Type TaxCategoryID = typeof (Detail.taxCategoryID);
    /// <exclude />
    public System.Type GroupDiscountRate = typeof (Detail.groupDiscountRate);
    /// <exclude />
    public System.Type DocumentDiscountRate = typeof (Detail.documentDiscountRate);
    /// <exclude />
    public System.Type CuryTranAmt = typeof (Detail.curyTranAmt);
    /// <exclude />
    public System.Type CuryTranDiscount = typeof (Detail.curyTranDiscount);
    /// <exclude />
    public System.Type CuryTranExtPrice = typeof (Detail.curyTranExtPrice);
    /// <summary>
    /// Which field in Base DAC Detail.InventoryID field is mapped to.
    /// </summary>
    public System.Type InventoryID = typeof (Detail.inventoryID);
    /// <summary>
    /// Which field in Base DAC Detail.UOM field is mapped to.
    /// </summary>
    public System.Type UOM = typeof (Detail.uOM);
    /// <summary>
    /// Which field in Base DAC Detail.Qty field is mapped to.
    /// </summary>
    public System.Type Qty = typeof (Detail.qty);

    /// <exclude />
    public System.Type Extension => typeof (Detail);

    /// <exclude />
    public System.Type Table => this._table;

    /// <summary>Creates the default mapping of the <see cref="T:PX.Objects.Extensions.SalesTax.Detail" /> mapped cache extension to the specified table.</summary>
    /// <param name="table">A DAC.</param>
    public DetailMapping(System.Type table) => this._table = table;
  }

  /// <summary>Defines the default mapping of the <see cref="T:PX.Objects.Extensions.SalesTax.TaxDetail" /> mapped cache extension to a DAC.</summary>
  protected class TaxDetailMapping : IBqlMapping
  {
    /// <exclude />
    protected System.Type _table;
    /// <exclude />
    public System.Type RefTaxID;
    /// <exclude />
    public System.Type TaxRate = typeof (TaxDetail.taxRate);
    /// <exclude />
    public System.Type CuryInfoID = typeof (TaxDetail.curyInfoID);
    /// <exclude />
    public System.Type NonDeductibleTaxRate = typeof (TaxDetail.nonDeductibleTaxRate);
    /// <exclude />
    public System.Type CuryTaxableAmt = typeof (TaxDetail.curyTaxableAmt);
    /// <exclude />
    public System.Type CuryExemptedAmt = typeof (TaxDetail.curyExemptedAmt);
    /// <exclude />
    public System.Type CuryTaxAmt = typeof (TaxDetail.curyTaxAmt);
    /// <exclude />
    public System.Type ExpenseAmt = typeof (TaxDetail.expenseAmt);
    /// <exclude />
    public System.Type CuryExpenseAmt = typeof (TaxDetail.curyExpenseAmt);
    /// <exclude />
    public System.Type CuryOrigTaxableAmt = typeof (TaxDetail.curyOrigTaxableAmt);
    /// <summary>Field, TaxDetail.TaxUOM is mapped to</summary>
    public System.Type TaxUOM = typeof (TaxDetail.taxUOM);
    /// <summary>Field, TaxDetail.TaxableQty is mapped to</summary>
    public System.Type TaxableQty = typeof (TaxDetail.taxableQty);

    /// <exclude />
    public System.Type Extension => typeof (TaxDetail);

    /// <exclude />
    public System.Type Table => this._table;

    /// <summary>Creates the default mapping of the <see cref="T:PX.Objects.Extensions.SalesTax.TaxDetail" /> mapped cache extension to the specified table.</summary>
    /// <param name="table">A DAC.</param>
    /// <param name="TaxID">A DAC class field with tax ID.</param>
    public TaxDetailMapping(System.Type table, System.Type TaxID)
    {
      this._table = table;
      this.RefTaxID = TaxID;
    }
  }

  /// <summary>Defines the default mapping of the <see cref="T:PX.Objects.Extensions.SalesTax.TaxTotal" /> mapped cache extension to a DAC.</summary>
  protected class TaxTotalMapping : IBqlMapping
  {
    /// <exclude />
    protected System.Type _table;
    /// <exclude />
    public System.Type RefTaxID;
    /// <exclude />
    public System.Type TaxRate = typeof (TaxTotal.taxRate);
    /// <exclude />
    public System.Type TaxZoneID = typeof (TaxTotal.taxZoneID);
    /// <exclude />
    public System.Type CuryInfoID = typeof (TaxTotal.curyInfoID);
    /// <exclude />
    public System.Type NonDeductibleTaxRate = typeof (TaxTotal.nonDeductibleTaxRate);
    /// <exclude />
    public System.Type CuryTaxableAmt = typeof (TaxTotal.curyTaxableAmt);
    /// <exclude />
    public System.Type CuryExemptedAmt = typeof (TaxTotal.curyExemptedAmt);
    /// <exclude />
    public System.Type CuryTaxAmt = typeof (TaxTotal.curyTaxAmt);
    /// <exclude />
    public System.Type ExpenseAmt = typeof (TaxTotal.expenseAmt);
    /// <exclude />
    public System.Type CuryExpenseAmt = typeof (TaxTotal.curyExpenseAmt);
    /// <exclude />
    public System.Type CuryOrigTaxableAmt = typeof (TaxTotal.curyOrigTaxableAmt);
    /// <summary>Field, TaxDetail.TaxUOM is mapped to</summary>
    public System.Type TaxUOM = typeof (TaxTotal.taxUOM);
    /// <summary>Field, TaxDetail.TaxableQty is mapped to</summary>
    public System.Type TaxableQty = typeof (TaxTotal.taxableQty);
    /// <summary>Field, TaxDetail.IsTaxInclusive is mapped to</summary>
    public System.Type IsTaxInclusive = typeof (TaxTotal.isTaxInclusive);

    /// <exclude />
    public System.Type Extension => typeof (TaxTotal);

    /// <exclude />
    public System.Type Table => this._table;

    /// <summary>Creates the default mapping of the <see cref="T:PX.Objects.Extensions.SalesTax.TaxTotal" /> mapped cache extension to the specified table.</summary>
    /// <param name="table">A DAC.</param>
    /// <param name="TaxID">A DAC class field with tax ID.</param>
    public TaxTotalMapping(System.Type table, System.Type TaxID)
    {
      this._table = table;
      this.RefTaxID = TaxID;
    }
  }

  /// <exclude />
  private enum SummType
  {
    Inclusive,
    All,
  }

  protected class InclusiveTaxGroup
  {
    public string Key { get; set; }

    public Decimal Rate { get; set; }

    public Decimal TotalAmount { get; set; }
  }
}
