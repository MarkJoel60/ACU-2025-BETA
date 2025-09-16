// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POLandedCostDocEntryExt.TaxExpenseAllocationExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.PO.LandedCosts;
using PX.Objects.PO.Services.AmountDistribution;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POLandedCostDocEntryExt;

public class TaxExpenseAllocationExt : PXGraphExtension<POLandedCostDocEntry>
{
  [InjectDependency]
  public AmountDistributionFactory AmountDistributionFactory { get; set; }

  [PXOverride]
  public virtual void TrackLandedCostSplits(
    IEnumerable<POLandedCostSplit> landedCostSplits,
    Action<IEnumerable<POLandedCostSplit>> baseMethod)
  {
    baseMethod(landedCostSplits);
    this.CalculateTaxes<TaxExpenseAllocationExt.POLCSplit>(landedCostSplits.Select<POLandedCostSplit, TaxExpenseAllocationExt.POLCSplit>((Func<POLandedCostSplit, TaxExpenseAllocationExt.POLCSplit>) (s => new TaxExpenseAllocationExt.POLCSplit(s))));
  }

  [PXOverride]
  public virtual List<PX.Objects.IN.INRegister> CreateLandedCostAdjustment(
    POLandedCostDoc doc,
    IEnumerable<LandedCostAllocationService.POLandedCostReceiptLineAdjustment> adjustments,
    Func<POLandedCostDoc, IEnumerable<LandedCostAllocationService.POLandedCostReceiptLineAdjustment>, List<PX.Objects.IN.INRegister>> baseMethod)
  {
    this.CalculateTaxes<TaxExpenseAllocationExt.LineAdjustments>(adjustments.Select<LandedCostAllocationService.POLandedCostReceiptLineAdjustment, TaxExpenseAllocationExt.LineAdjustments>((Func<LandedCostAllocationService.POLandedCostReceiptLineAdjustment, TaxExpenseAllocationExt.LineAdjustments>) (s => new TaxExpenseAllocationExt.LineAdjustments(s))));
    return baseMethod(doc, adjustments);
  }

  protected virtual void CalculateTaxes<TSplit>(IEnumerable<TSplit> landedCostSplits) where TSplit : TaxExpenseAllocationExt.ISplit, IAmountItem
  {
    POLandedCostDoc current = ((PXSelectBase<POLandedCostDoc>) this.Base.Document).Current;
    IEnumerable<\u003C\u003Ef__AnonymousType81<POLandedCostTaxTran, PX.Objects.TX.Tax>> source = ((PXSelectBase) this.Base.Taxes).View.SelectMultiBound(new object[1]
    {
      (object) current
    }, Array.Empty<object>()).Cast<PXResult<POLandedCostTaxTran, PX.Objects.TX.Tax>>().Select(t => new
    {
      POLandedCostTaxTran = ((PXResult) t).GetItem<POLandedCostTaxTran>(),
      Tax = ((PXResult) t).GetItem<PX.Objects.TX.Tax>()
    }).Where(t => this.IsItemCostTax(t.Tax));
    if (source.Any())
      throw new PXException("Cannot save the landed cost document because the tax with the {0} tax ID has the Use Tax Expense Account check box cleared and thus cannot be applied to the document. For processing taxable landed costs, configure a separate tax ID with the Use Tax Expense Account check box selected.", new object[1]
      {
        (object) source.First().Tax.TaxID
      });
    Dictionary<string, List<TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>>> taxesGrouppedByTaxId = this.GetLCTaxesGrouppedByTaxId<TSplit>(landedCostSplits, current);
    foreach (var data in source)
    {
      if (!taxesGrouppedByTaxId.ContainsKey(data.Tax.TaxID))
        throw new PXArgumentException("TaxID");
      List<TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>> landedCostTaxTaxSplitList = taxesGrouppedByTaxId[data.Tax.TaxID];
      if (landedCostTaxTaxSplitList.Count == 0)
        throw new PXArgumentException("lcTaxes");
      int num = 1;
      ARReleaseProcess.Amount amount1 = data.Tax.TaxType != "V" || !data.Tax.DeductibleVAT.GetValueOrDefault() ? new ARReleaseProcess.Amount(data.POLandedCostTaxTran.CuryTaxAmt, data.POLandedCostTaxTran.TaxAmt) * (Decimal) num : new ARReleaseProcess.Amount(data.POLandedCostTaxTran.CuryExpenseAmt, data.POLandedCostTaxTran.ExpenseAmt) * (Decimal) num;
      Func<TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>, Decimal?, Decimal?, TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>> addAmount = (Func<TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>, Decimal?, Decimal?, TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>>) ((item, amount, curyAmount) =>
      {
        TSplit split1 = item.Split;
        ref TSplit local1 = ref split1;
        ref TSplit local2 = ref local1;
        TSplit split2;
        if ((object) default (TSplit) == null)
        {
          split2 = local2;
          local2 = ref split2;
        }
        Decimal? lineAmt = local1.LineAmt;
        Decimal? nullable1 = amount;
        Decimal? nullable2 = lineAmt.HasValue & nullable1.HasValue ? new Decimal?(lineAmt.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        local2.LineAmt = nullable2;
        TSplit split3 = item.Split;
        ref TSplit local3 = ref split3;
        ref TSplit local4 = ref local3;
        split2 = default (TSplit);
        if ((object) split2 == null)
        {
          split2 = local4;
          local4 = ref split2;
        }
        nullable1 = local3.CuryLineAmt;
        Decimal? nullable3 = curyAmount;
        Decimal? nullable4 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
        local4.CuryLineAmt = nullable4;
        return item;
      });
      this.AmountDistributionFactory.CreateService<TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>>(DistributionMethod.RemainderToBiggestLine, new DistributionParameter<TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>>()
      {
        Items = (IEnumerable<TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>>) landedCostTaxTaxSplitList,
        Amount = amount1.Base,
        CuryAmount = amount1.Cury,
        CuryRow = (object) current,
        CacheOfCuryRow = ((PXSelectBase) this.Base.Document).Cache,
        OnValueCalculated = addAmount,
        OnRoundingDifferenceApplied = (Action<TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>, Decimal?, Decimal?, Decimal?, Decimal?>) ((item, newAmount, curyNewAmount, oldAmount, curyOldAmount) =>
        {
          Func<TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>, Decimal?, Decimal?, TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>> func = addAmount;
          TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit> landedCostTaxTaxSplit1 = item;
          Decimal? nullable5 = newAmount;
          Decimal? nullable6 = oldAmount;
          Decimal? nullable7 = nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
          nullable6 = curyNewAmount;
          nullable5 = curyOldAmount;
          Decimal? nullable8 = nullable6.HasValue & nullable5.HasValue ? new Decimal?(nullable6.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
          TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit> landedCostTaxTaxSplit2 = func(landedCostTaxTaxSplit1, nullable7, nullable8);
        })
      }).Distribute();
    }
  }

  protected virtual Dictionary<string, List<TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>>> GetLCTaxesGrouppedByTaxId<TSplit>(
    IEnumerable<TSplit> landedCostSplits,
    POLandedCostDoc doc)
    where TSplit : TaxExpenseAllocationExt.ISplit, IAmountItem
  {
    return ((IEnumerable<PXResult<POLandedCostTax>>) ((PXSelectBase<POLandedCostTax>) new PXSelectJoin<POLandedCostTax, InnerJoin<PX.Objects.TX.Tax, On<POLandedCostTax.taxID, Equal<PX.Objects.TX.Tax.taxID>>>, Where<POLandedCostTax.docType, Equal<Required<POLandedCostTax.docType>>, And<POLandedCostTax.refNbr, Equal<Required<POLandedCostTax.refNbr>>>>>((PXGraph) this.Base)).Select(new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    })).AsEnumerable<PXResult<POLandedCostTax>>().Join<PXResult<POLandedCostTax>, TSplit, int?, TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>>(landedCostSplits, (Func<PXResult<POLandedCostTax>, int?>) (t => PXResult<POLandedCostTax>.op_Implicit(t).LineNbr), (Func<TSplit, int?>) (s => new int?(s.LineNbr)), (Func<PXResult<POLandedCostTax>, TSplit, TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>>) ((t, s) => new TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>(((PXResult) t).GetItem<POLandedCostTax>(), ((PXResult) t).GetItem<PX.Objects.TX.Tax>(), s))).Where<TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>>((Func<TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>, bool>) (t => !string.IsNullOrEmpty(t.Tax.TaxID))).GroupBy<TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>, string>((Func<TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>, string>) (t => t.Tax.TaxID)).ToDictionary<IGrouping<string, TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>>, string, List<TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>>>((Func<IGrouping<string, TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>>, string>) (r => r.Key), (Func<IGrouping<string, TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>>, List<TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>>>) (r => r.ToList<TaxExpenseAllocationExt.POLandedCostTax_Tax_Split<TSplit>>()));
  }

  protected virtual bool IsItemCostTax(PX.Objects.TX.Tax tax)
  {
    if (tax.ReportExpenseToSingleAccount.GetValueOrDefault())
      return false;
    if (EnumerableExtensions.IsIn<string>(tax.TaxType, "S", "P"))
      return true;
    return tax.TaxType == "V" && tax.DeductibleVAT.GetValueOrDefault();
  }

  protected interface ISplit
  {
    int LineNbr { get; }

    Decimal? LineAmt { get; set; }

    Decimal? CuryLineAmt { get; set; }
  }

  protected class POLandedCostTax_Tax_Split<TSplit> : IAmountItem where TSplit : TaxExpenseAllocationExt.ISplit, IAmountItem
  {
    public POLandedCostTax_Tax_Split(POLandedCostTax pOLandedCostTax, PX.Objects.TX.Tax tax, TSplit split)
    {
      this.POLandedCostTax = pOLandedCostTax;
      this.Tax = tax;
      this.Split = split;
    }

    public POLandedCostTax POLandedCostTax { get; private set; }

    public PX.Objects.TX.Tax Tax { get; private set; }

    public TSplit Split { get; private set; }

    public Decimal Weight => this.Split.Weight;

    public Decimal? Amount
    {
      get => this.Split.Amount;
      set => this.Split.Amount = value;
    }

    public Decimal? CuryAmount
    {
      get => this.Split.CuryAmount;
      set => this.Split.CuryAmount = value;
    }
  }

  protected class POLCSplit : IAmountItem, TaxExpenseAllocationExt.ISplit
  {
    private POLandedCostSplit POLandedCostSplit;

    public POLCSplit(POLandedCostSplit adj) => this.POLandedCostSplit = adj;

    public Decimal Weight => this.POLandedCostSplit.LineAmt.GetValueOrDefault();

    public Decimal? Amount { get; set; }

    public Decimal? CuryAmount { get; set; }

    public int LineNbr => this.POLandedCostSplit.DetailLineNbr.GetValueOrDefault();

    public Decimal? LineAmt
    {
      get => this.POLandedCostSplit.LineAmt;
      set => this.POLandedCostSplit.LineAmt = value;
    }

    public Decimal? CuryLineAmt
    {
      get => this.POLandedCostSplit.CuryLineAmt;
      set => this.POLandedCostSplit.CuryLineAmt = value;
    }
  }

  protected class LineAdjustments : IAmountItem, TaxExpenseAllocationExt.ISplit
  {
    private LandedCostAllocationService.POLandedCostReceiptLineAdjustment POLandedCostReceiptLineAdjustment;

    public LineAdjustments(
      LandedCostAllocationService.POLandedCostReceiptLineAdjustment adj)
    {
      this.POLandedCostReceiptLineAdjustment = adj;
    }

    public Decimal Weight => this.POLandedCostReceiptLineAdjustment.AllocatedAmt;

    public Decimal? Amount { get; set; }

    public Decimal? CuryAmount { get; set; }

    public int LineNbr
    {
      get
      {
        return ((int?) this.POLandedCostReceiptLineAdjustment.LandedCostDetail?.LineNbr).GetValueOrDefault();
      }
    }

    public Decimal? LineAmt
    {
      get => new Decimal?(this.POLandedCostReceiptLineAdjustment.AllocatedAmt);
      set => this.POLandedCostReceiptLineAdjustment.AllocatedAmt = value.GetValueOrDefault();
    }

    public Decimal? CuryLineAmt { get; set; }
  }
}
