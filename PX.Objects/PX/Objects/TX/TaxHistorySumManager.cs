// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxHistorySumManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;
using PX.Objects.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.TX;

public class TaxHistorySumManager
{
  private const int StartRow = 0;
  private const int TotalRows = 1;

  public static void UpdateTaxHistorySums(
    PXGraph graph,
    RoundingManager rmanager,
    string taxPeriodId,
    int? revisionId,
    int? organizationID,
    int? branchID,
    Func<PXResult<TaxReportLine, TaxBucketLine, PX.Objects.CM.Extensions.Currency, TaxTranRevReport>, bool> ShowTaxReportLine = null)
  {
    if (!rmanager.IsRequireRounding)
      return;
    PXCache cach = graph.Caches[typeof (TaxHistory)];
    using (new PXReadBranchRestrictedScope(organizationID.SingleToArray<int?>(), branchID.SingleToArrayOrNull<int?>(), true, true))
    {
      PXResultset<TaxHistory> taxHistoryLines = TaxHistorySumManager.GetTaxHistoryLines(graph, rmanager.CurrentVendor.BAccountID, new int?(rmanager.CurrentTaxReportRevisionID), taxPeriodId, revisionId);
      if (taxHistoryLines.Count == 0)
        return;
      string baseCuryId;
      if (!organizationID.HasValue)
      {
        PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(branchID);
        organizationID = (int?) ((PXAccess.Organization) branch?.Organization).OrganizationID;
        baseCuryId = branch.BaseCuryID;
      }
      else
        baseCuryId = ((PXAccess.Organization) PXAccess.GetOrganizationByID(organizationID)).BaseCuryID;
      TaxPeriod taxPeriodByKey = TaxYearMaint.GetTaxPeriodByKey(graph, organizationID, rmanager.CurrentVendor.BAccountID, taxPeriodId);
      PXResult<PX.Objects.CM.Currency, CurrencyRateByDate> currencyAndRateByDate = TaxHistorySumManager.GetCurrencyAndRateByDate(graph, rmanager.CurrentVendor, baseCuryId, taxPeriodByKey);
      PX.Objects.CM.Currency aCurrency = PXResult<PX.Objects.CM.Currency, CurrencyRateByDate>.op_Implicit(currencyAndRateByDate);
      CurrencyRateByDate curyRateByDate = PXResult<PX.Objects.CM.Currency, CurrencyRateByDate>.op_Implicit(aCurrency.CuryID != baseCuryId ? currencyAndRateByDate : (PXResult<PX.Objects.CM.Currency, CurrencyRateByDate>) null);
      TaxHistorySumManager.TaxBucketsCalculation bucketsCalculation1 = new TaxHistorySumManager.TaxBucketsCalculation("P", graph, rmanager, aCurrency, curyRateByDate, ShowTaxReportLine);
      TaxHistorySumManager.TaxBucketsCalculation bucketsCalculation2 = new TaxHistorySumManager.TaxBucketsCalculation("A", graph, rmanager, aCurrency, curyRateByDate, ShowTaxReportLine);
      bucketsCalculation1.CalculateTaxBuckets(taxHistoryLines);
      PXResultset<TaxHistory> taxLines = taxHistoryLines;
      bucketsCalculation2.CalculateTaxBuckets(taxLines);
    }
    cach.Persist((PXDBOperation) 2);
    cach.Persisted(false);
  }

  private static PXResultset<TaxHistory> GetTaxHistoryLines(
    PXGraph graph,
    int? curVendorBaccountID,
    int? curTaxReportRevisionID,
    string taxPeriodId,
    int? revisionId)
  {
    return PXSelectBase<TaxHistory, PXSelectJoinGroupBy<TaxHistory, InnerJoin<TaxReportLine, On<TaxReportLine.vendorID, Equal<TaxHistory.vendorID>, And<TaxReportLine.taxReportRevisionID, Equal<TaxHistory.taxReportRevisionID>, And<TaxReportLine.lineNbr, Equal<TaxHistory.lineNbr>>>>>, Where<TaxHistory.vendorID, Equal<Required<TaxHistory.vendorID>>, And<TaxHistory.taxReportRevisionID, Equal<Required<TaxHistory.taxReportRevisionID>>, And<TaxHistory.taxPeriodID, Equal<Required<TaxHistory.taxPeriodID>>, And<TaxHistory.revisionID, Equal<Required<TaxHistory.revisionID>>>>>>, Aggregate<GroupBy<TaxHistory.branchID, GroupBy<TaxReportLine.lineNbr, GroupBy<TaxReportLine.netTax, Sum<TaxHistory.reportFiledAmt>>>>>>.Config>.Select(graph, new object[4]
    {
      (object) curVendorBaccountID,
      (object) curTaxReportRevisionID,
      (object) taxPeriodId,
      (object) revisionId
    });
  }

  private static PXResult<PX.Objects.CM.Currency, CurrencyRateByDate> GetCurrencyAndRateByDate(
    PXGraph graph,
    PX.Objects.AP.Vendor curVendor,
    string baseCuryID,
    TaxPeriod period)
  {
    PXCache<CurrencyFilter> pxCache = GraphHelper.Caches<CurrencyFilter>(graph);
    ((PXCache) pxCache).Clear();
    pxCache.Insert(new CurrencyFilter()
    {
      FromCuryID = curVendor.CuryID ?? baseCuryID,
      ToCuryID = baseCuryID
    });
    return (PXResult<PX.Objects.CM.Currency, CurrencyRateByDate>) PXResultset<PX.Objects.CM.Currency>.op_Implicit(PXSelectBase<PX.Objects.CM.Currency, PXSelectJoin<PX.Objects.CM.Currency, LeftJoin<CurrencyRateByDate, On<CurrencyRateByDate.fromCuryID, Equal<PX.Objects.CM.Currency.curyID>, And<CurrencyRateByDate.toCuryID, Equal<Required<CurrencyRateByDate.toCuryID>>, And<CurrencyRateByDate.curyRateType, Equal<Required<CurrencyRateByDate.curyRateType>>, And<CurrencyRateByDate.curyEffDate, LessEqual<Required<CurrencyRateByDate.curyEffDate>>, And<Where<CurrencyRateByDate.nextEffDate, Greater<Required<CurrencyRateByDate.curyEffDate>>, Or<CurrencyRateByDate.nextEffDate, IsNull>>>>>>>>, Where<PX.Objects.CM.Currency.curyID, Equal<Required<PX.Objects.CM.Currency.curyID>>>>.Config>.SelectWindowed(graph, 0, 1, new object[5]
    {
      (object) baseCuryID,
      (object) curVendor.CuryRateTypeID,
      (object) period.EndDate,
      (object) period.EndDate,
      (object) (curVendor.CuryID ?? baseCuryID)
    }));
  }

  public static Decimal RecalcCurrency(int? decimalPlaces, PX.Objects.CM.CurrencyRate rate, Decimal value)
  {
    if (rate.CuryRate.HasValue)
    {
      Decimal? curyRate = rate.CuryRate;
      Decimal num1 = 0M;
      if (!(curyRate.GetValueOrDefault() == num1 & curyRate.HasValue))
      {
        Decimal d = 0M;
        if (rate.CuryMultDiv == "M")
        {
          Decimal num2 = value;
          curyRate = rate.CuryRate;
          Decimal num3 = curyRate.Value;
          d = num2 * num3;
        }
        else if (rate.CuryMultDiv == "D")
        {
          Decimal num4 = value;
          curyRate = rate.CuryRate;
          Decimal num5 = curyRate.Value;
          d = num4 / num5;
        }
        return Math.Round(d, decimalPlaces ?? 2, MidpointRounding.AwayFromZero);
      }
    }
    return 0M;
  }

  public static PXResultset<TaxReportLine, TaxHistory> GetPreviewReport(
    PXGraph graph,
    PX.Objects.AP.Vendor vendor,
    int taxReportRevisionID,
    PXResultset<TaxReportLine, TaxHistory> records,
    Func<PXResult<TaxReportLine, TaxBucketLine, PX.Objects.CM.Extensions.Currency, TaxTranRevReport>, bool> ShowTaxReportLine = null)
  {
    if (((PXResultset<TaxReportLine>) records).Count == 0)
      return records;
    int BAccountID = vendor.BAccountID.Value;
    RoundingManager roundingManager = new RoundingManager(vendor, taxReportRevisionID);
    Dictionary<int, List<int>> aggregatesTable1 = TaxReportMaint.AnalyseBuckets(graph, BAccountID, taxReportRevisionID, "P", true, ShowTaxReportLine) ?? new Dictionary<int, List<int>>();
    Dictionary<int, List<int>> aggregatesTable2 = TaxReportMaint.AnalyseBuckets(graph, BAccountID, taxReportRevisionID, "A", true, ShowTaxReportLine) ?? new Dictionary<int, List<int>>();
    Dictionary<int, PXResult<TaxReportLine, TaxHistory>> recordsByLineNumberTable = new Dictionary<int, PXResult<TaxReportLine, TaxHistory>>();
    foreach (PXResult<TaxReportLine, TaxHistory> record in (PXResultset<TaxReportLine>) records)
    {
      TaxReportLine taxReportLine = PXResult<TaxReportLine, TaxHistory>.op_Implicit(record);
      TaxHistory taxHistory = PXResult<TaxReportLine, TaxHistory>.op_Implicit(record);
      taxHistory.ReportUnfiledAmt = roundingManager.Round(taxHistory.ReportUnfiledAmt);
      recordsByLineNumberTable[taxReportLine.LineNbr.Value] = record;
    }
    TaxHistorySumManager.CalculateReportUnfiledAmtForAggregatedTaxLines(aggregatesTable1, recordsByLineNumberTable);
    TaxHistorySumManager.CalculateReportUnfiledAmtForAggregatedTaxLines(aggregatesTable2, recordsByLineNumberTable);
    return records;
  }

  private static void CalculateReportUnfiledAmtForAggregatedTaxLines(
    Dictionary<int, List<int>> aggregatesTable,
    Dictionary<int, PXResult<TaxReportLine, TaxHistory>> recordsByLineNumberTable)
  {
    if (aggregatesTable == null)
      return;
    foreach (KeyValuePair<int, List<int>> keyValuePair in aggregatesTable)
    {
      int key = keyValuePair.Key;
      List<int> componentLinesNumbers = keyValuePair.Value;
      if (recordsByLineNumberTable.ContainsKey(key))
        PXResult<TaxReportLine, TaxHistory>.op_Implicit(recordsByLineNumberTable[key]).ReportUnfiledAmt = TaxHistorySumManager.SumComponentLinesAmounts(componentLinesNumbers, recordsByLineNumberTable);
    }
  }

  private static Decimal? SumComponentLinesAmounts(
    List<int> componentLinesNumbers,
    Dictionary<int, PXResult<TaxReportLine, TaxHistory>> recordsByLineNumberTable)
  {
    Decimal? nullable1 = new Decimal?(0M);
    foreach (int key in componentLinesNumbers.Where<int>((Func<int, bool>) (l => recordsByLineNumberTable.ContainsKey(l))))
    {
      PXResult<TaxReportLine, TaxHistory> pxResult = recordsByLineNumberTable[key];
      TaxReportLine taxReportLine = PXResult<TaxReportLine, TaxHistory>.op_Implicit(pxResult);
      TaxHistory taxHistory = PXResult<TaxReportLine, TaxHistory>.op_Implicit(pxResult);
      Decimal? nullable2 = taxHistory.ReportUnfiledAmt;
      if (nullable2.HasValue)
      {
        nullable2 = nullable1;
        short? lineMult = taxReportLine.LineMult;
        Decimal? nullable3 = lineMult.HasValue ? new Decimal?((Decimal) lineMult.GetValueOrDefault()) : new Decimal?();
        Decimal? reportUnfiledAmt = taxHistory.ReportUnfiledAmt;
        Decimal? nullable4 = nullable3.HasValue & reportUnfiledAmt.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * reportUnfiledAmt.GetValueOrDefault()) : new Decimal?();
        nullable1 = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
      }
    }
    return nullable1;
  }

  private class TaxBucketsCalculation
  {
    private const bool CalcWithZones = true;
    private readonly Dictionary<int, List<int>> aggregatesTable;
    private readonly Dictionary<int, List<int>> linesWithRelatedAggregatesTable;
    private readonly string taxType;
    private readonly RoundingManager roundingManager;
    private readonly PXGraph graph;
    private readonly PX.Objects.CM.Currency currency;
    private readonly CurrencyRateByDate rateByDate;
    private readonly Dictionary<int, TaxHistorySumManager.TaxBucketsCalculation.TaxLinesWithHistoryPerBranch> netHistoryByBranchID = new Dictionary<int, TaxHistorySumManager.TaxBucketsCalculation.TaxLinesWithHistoryPerBranch>();
    private readonly Dictionary<int, TaxHistorySumManager.TaxBucketsCalculation.TaxLinesWithRoundAmountsPerBranch> roundedNetAmtByBranchID = new Dictionary<int, TaxHistorySumManager.TaxBucketsCalculation.TaxLinesWithRoundAmountsPerBranch>();

    public TaxBucketsCalculation(
      string taxTypeForCalculation,
      PXGraph aGraph,
      RoundingManager rmanager,
      PX.Objects.CM.Currency aCurrency,
      CurrencyRateByDate curyRateByDate,
      Func<PXResult<TaxReportLine, TaxBucketLine, PX.Objects.CM.Extensions.Currency, TaxTranRevReport>, bool> showTaxReportLine)
    {
      this.taxType = taxTypeForCalculation;
      this.roundingManager = rmanager;
      this.graph = aGraph;
      this.currency = aCurrency;
      this.rateByDate = curyRateByDate;
      this.aggregatesTable = TaxReportMaint.AnalyseBuckets(this.graph, this.roundingManager.CurrentVendor.BAccountID.Value, this.roundingManager.CurrentTaxReportRevisionID, taxTypeForCalculation, true, showTaxReportLine);
      this.linesWithRelatedAggregatesTable = TaxHistorySumManager.TaxBucketsCalculation.TransposeDictionary(this.aggregatesTable);
    }

    public void CalculateTaxBuckets(PXResultset<TaxHistory> taxLines)
    {
      if (this.aggregatesTable == null || this.linesWithRelatedAggregatesTable == null)
        return;
      this.netHistoryByBranchID.Clear();
      this.roundedNetAmtByBranchID.Clear();
      foreach (PXResult<TaxHistory, TaxReportLine> taxLine1 in taxLines)
      {
        TaxReportLine taxLine2 = PXResult<TaxHistory, TaxReportLine>.op_Implicit(taxLine1);
        TaxHistory record = PXResult<TaxHistory, TaxReportLine>.op_Implicit(taxLine1);
        if (!(taxLine2.LineType != this.taxType))
        {
          int? nullable = record.BranchID;
          if (nullable.HasValue)
          {
            nullable = record.BranchID;
            int key = nullable.Value;
            nullable = record.LineNbr;
            int num = nullable.Value;
            if (this.aggregatesTable.ContainsKey(num))
            {
              if (!this.netHistoryByBranchID.ContainsKey(key))
                this.netHistoryByBranchID[key] = new TaxHistorySumManager.TaxBucketsCalculation.TaxLinesWithHistoryPerBranch();
              this.netHistoryByBranchID[key][num] = taxLine1;
            }
            else
            {
              Decimal? roundedTaxAmount = this.roundingManager.Round(record.ReportFiledAmt);
              this.ProcessTaxRecord(record, taxLine2, roundedTaxAmount);
            }
          }
        }
      }
      foreach (KeyValuePair<int, TaxHistorySumManager.TaxBucketsCalculation.TaxLinesWithRoundAmountsPerBranch> keyValuePair in this.roundedNetAmtByBranchID)
      {
        TaxHistorySumManager.TaxBucketsCalculation.TaxLinesWithHistoryPerBranch branchTaxLinesAndHistory = this.netHistoryByBranchID[keyValuePair.Key];
        TaxHistorySumManager.TaxBucketsCalculation.TaxLinesWithRoundAmountsPerBranch amountsPerBranch = keyValuePair.Value;
        foreach (int lineNumber in this.aggregatesTable.Keys.Where<int>((Func<int, bool>) (aggrLineNumber => branchTaxLinesAndHistory.ContainsTaxLine(aggrLineNumber))))
          this.ProcessTaxRecord(PXResult<TaxHistory, TaxReportLine>.op_Implicit(branchTaxLinesAndHistory[lineNumber]), PXResult<TaxHistory, TaxReportLine>.op_Implicit(branchTaxLinesAndHistory[lineNumber]), new Decimal?(amountsPerBranch[lineNumber]));
      }
    }

    /// <summary>
    /// Transpose input dictionary. Each key in result was a value in initial dictionary and corresponding entry in result consists of the keys from initial dictionary
    /// which lists contained that entry's key.
    /// </summary>
    /// <returns />
    private static Dictionary<int, List<int>> TransposeDictionary(Dictionary<int, List<int>> oldDict)
    {
      return oldDict == null ? (Dictionary<int, List<int>>) null : oldDict.SelectMany<KeyValuePair<int, List<int>>, Tuple<int, int>>((Func<KeyValuePair<int, List<int>>, IEnumerable<Tuple<int, int>>>) (pair => pair.Value.Select<int, Tuple<int, int>>((Func<int, Tuple<int, int>>) (val => Tuple.Create<int, int>(val, pair.Key))))).GroupBy<Tuple<int, int>, int, int>((Func<Tuple<int, int>, int>) (tuple => tuple.Item1), (Func<Tuple<int, int>, int>) (tuple => tuple.Item2)).ToDictionary<IGrouping<int, int>, int, List<int>>((Func<IGrouping<int, int>, int>) (group => group.Key), (Func<IGrouping<int, int>, List<int>>) (group => group.ToList<int>()));
    }

    private void ProcessTaxRecord(
      TaxHistory record,
      TaxReportLine taxLine,
      Decimal? roundedTaxAmount)
    {
      PXCache cach = this.graph.Caches[typeof (TaxHistory)];
      Decimal? reportFiledAmt = record.ReportFiledAmt;
      int key = record.LineNbr.Value;
      int branchID = record.BranchID.Value;
      Decimal? nullable1 = reportFiledAmt;
      Decimal? nullable2 = roundedTaxAmount;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      {
        TaxHistory deltaHistory = this.CreateDeltaHistory(record, roundedTaxAmount);
        cach.Insert((object) deltaHistory);
      }
      if (!this.linesWithRelatedAggregatesTable.ContainsKey(key))
        return;
      foreach (int aggrLineNumber in this.linesWithRelatedAggregatesTable[key])
      {
        Decimal valueOrDefault1 = roundedTaxAmount.GetValueOrDefault();
        short? lineMult = taxLine.LineMult;
        Decimal valueOrDefault2 = lineMult.HasValue ? (Decimal) lineMult.GetValueOrDefault() : 0M;
        Decimal amount = valueOrDefault1 * valueOrDefault2;
        this.AddTaxAmountToAggregateLine(branchID, aggrLineNumber, amount);
      }
    }

    private TaxHistory CreateDeltaHistory(TaxHistory original, Decimal? roundedAmount)
    {
      TaxHistory taxHistory1 = new TaxHistory();
      taxHistory1.BranchID = original.BranchID;
      taxHistory1.VendorID = original.VendorID;
      taxHistory1.CuryID = original.CuryID;
      taxHistory1.TaxID = string.Empty;
      taxHistory1.TaxPeriodID = original.TaxPeriodID;
      taxHistory1.TaxReportRevisionID = original.TaxReportRevisionID;
      taxHistory1.LineNbr = original.LineNbr;
      taxHistory1.RevisionID = original.RevisionID;
      Decimal? nullable1 = roundedAmount;
      Decimal? reportFiledAmt = original.ReportFiledAmt;
      taxHistory1.ReportFiledAmt = nullable1.HasValue & reportFiledAmt.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - reportFiledAmt.GetValueOrDefault()) : new Decimal?();
      TaxHistory deltaHistory = taxHistory1;
      Decimal? nullable2;
      if (this.rateByDate == null)
      {
        deltaHistory.FiledAmt = deltaHistory.ReportFiledAmt;
      }
      else
      {
        nullable2 = this.rateByDate.CuryRate;
        if (!nullable2.HasValue)
          throw new PXException("Currency Rate is not defined.");
        TaxHistory taxHistory2 = deltaHistory;
        short? decimalPlaces1 = this.currency.DecimalPlaces;
        int? decimalPlaces2 = decimalPlaces1.HasValue ? new int?((int) decimalPlaces1.GetValueOrDefault()) : new int?();
        CurrencyRateByDate rateByDate = this.rateByDate;
        nullable2 = deltaHistory.ReportFiledAmt;
        Decimal valueOrDefault = nullable2.GetValueOrDefault();
        Decimal? nullable3 = new Decimal?(TaxHistorySumManager.RecalcCurrency(decimalPlaces2, (PX.Objects.CM.CurrencyRate) rateByDate, valueOrDefault));
        taxHistory2.FiledAmt = nullable3;
      }
      nullable2 = deltaHistory.ReportFiledAmt;
      Decimal num = 0M;
      if (nullable2.GetValueOrDefault() > num & nullable2.HasValue)
      {
        deltaHistory.AccountID = this.currency.RoundingLossAcctID;
        deltaHistory.SubID = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Currency.roundingLossSubID>(this.graph, deltaHistory.BranchID, this.currency);
      }
      else
      {
        deltaHistory.AccountID = this.currency.RoundingGainAcctID;
        deltaHistory.SubID = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Currency.roundingGainSubID>(this.graph, deltaHistory.BranchID, this.currency);
      }
      if (!deltaHistory.AccountID.HasValue || !deltaHistory.SubID.HasValue)
        throw new PXException("The tax report cannot be prepared because the accounts to be used for recording rounding amounts are not specified on the Currencies (CM202000) form. To proceed, specify the Rounding Gain/Loss Account (and subaccounts, if applicable).");
      return deltaHistory;
    }

    private void AddTaxAmountToAggregateLine(int branchID, int aggrLineNumber, Decimal amount)
    {
      if (!this.roundedNetAmtByBranchID.ContainsKey(branchID))
        this.roundedNetAmtByBranchID[branchID] = new TaxHistorySumManager.TaxBucketsCalculation.TaxLinesWithRoundAmountsPerBranch();
      Decimal valueOrDefault = this.roundedNetAmtByBranchID[branchID].TryGetTaxAmount(aggrLineNumber).GetValueOrDefault();
      this.roundedNetAmtByBranchID[branchID][aggrLineNumber] = valueOrDefault + amount;
    }

    /// <summary>
    /// A class which contains a table with tax lines and history per branch.
    /// </summary>
    private class TaxLinesWithHistoryPerBranch
    {
      private readonly Dictionary<int, PXResult<TaxHistory, TaxReportLine>> taxLines = new Dictionary<int, PXResult<TaxHistory, TaxReportLine>>();

      public PXResult<TaxHistory, TaxReportLine> this[int lineNumber]
      {
        get
        {
          PXResult<TaxHistory, TaxReportLine> pxResult;
          return !this.taxLines.TryGetValue(lineNumber, out pxResult) ? (PXResult<TaxHistory, TaxReportLine>) null : pxResult;
        }
        set => this.taxLines[lineNumber] = value;
      }

      public bool ContainsTaxLine(int lineNumber) => this.taxLines.ContainsKey(lineNumber);
    }

    /// <summary>
    /// A class which represents a table of tax lines with corresponding rounded tax amounts per branch.
    /// </summary>
    private class TaxLinesWithRoundAmountsPerBranch
    {
      private readonly Dictionary<int, Decimal> taxLinesWithAmounts = new Dictionary<int, Decimal>();

      public Decimal this[int lineNumber]
      {
        get => this.taxLinesWithAmounts[lineNumber];
        set => this.taxLinesWithAmounts[lineNumber] = value;
      }

      /// <summary>
      /// Tries to get rounded tax amount by tax line number without raising exception.
      /// </summary>
      /// <param name="lineNumber">The line number.</param>
      /// <returns />
      public Decimal? TryGetTaxAmount(int lineNumber)
      {
        Decimal num;
        return !this.taxLinesWithAmounts.TryGetValue(lineNumber, out num) ? new Decimal?() : new Decimal?(num);
      }
    }
  }
}
