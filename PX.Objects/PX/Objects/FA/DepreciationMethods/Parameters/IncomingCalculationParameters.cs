// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationMethods.Parameters.IncomingCalculationParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FA.DepreciationMethods.Parameters;

/// <exclude />
public class IncomingCalculationParameters
{
  public PXGraph Graph;
  public FixedAsset FixedAsset;
  public FADetails Details;
  public FABookBalance BookBalance;
  public FADepreciationMethod Method;
  public List<FAAddition> Additions;
  public SortedSet<string> SuspendedPeriodsIDs;
  public List<(string, string)> SuspendSections;
  private IFABookPeriodRepository repositoryHelper;
  private IFABookPeriodUtils utilsHelper;

  public int Precision { get; set; }

  public string CalculationMethod => this.Method?.DepreciationMethod;

  public string AveragingConvention => this.BookBalance?.AveragingConvention;

  public int? BookID => this.BookBalance?.BookID;

  public int? AssetID => this.BookBalance?.AssetID;

  public int? OrganizationID
  {
    get
    {
      return new int?(this.RepositoryHelper.IsPostingFABook(this.BookID) ? PXAccess.GetParentOrganizationID(this.FixedAsset.BranchID).Value : 0);
    }
  }

  public IFABookPeriodRepository RepositoryHelper
  {
    get
    {
      this.repositoryHelper = this.repositoryHelper ?? this.Graph.GetService<IFABookPeriodRepository>();
      return this.repositoryHelper;
    }
  }

  public IFABookPeriodUtils UtilsHelper
  {
    get
    {
      this.utilsHelper = this.utilsHelper ?? this.Graph.GetService<IFABookPeriodUtils>();
      return this.utilsHelper;
    }
  }

  public FABookPeriodDepreciationUtils PeriodDepreciationUtils { get; }

  public IncomingCalculationParameters(PXGraph graph, FABookBalance bookBalance)
  {
    this.Graph = graph;
    this.BookBalance = bookBalance;
    this.FixedAsset = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select(this.Graph, new object[1]
    {
      (object) this.AssetID
    }));
    this.Details = PXResultset<FADetails>.op_Implicit(PXSelectBase<FADetails, PXSelect<FADetails, Where<FADetails.assetID, Equal<Required<FADetails.assetID>>>>.Config>.Select(this.Graph, new object[1]
    {
      (object) this.AssetID
    }));
    this.Method = PXResultset<FADepreciationMethod>.op_Implicit(PXSelectBase<FADepreciationMethod, PXSelect<FADepreciationMethod, Where<FADepreciationMethod.methodID, Equal<Required<FADepreciationMethod.methodID>>>>.Config>.Select(this.Graph, new object[1]
    {
      (object) this.BookBalance.DepreciationMethodID
    }));
    this.Precision = (int) GraphHelper.RowCast<PX.Objects.CM.Currency>((IEnumerable) PXSelectBase<PX.Objects.CM.Currency, PXViewOf<PX.Objects.CM.Currency>.BasedOn<SelectFromBase<PX.Objects.CM.Currency, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.CM.Currency.curyID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select(this.Graph, new object[1]
    {
      (object) this.FixedAsset.BaseCuryID
    })).FirstOrDefault<PX.Objects.CM.Currency>().DecimalPlaces.Value;
    this.PeriodDepreciationUtils = new FABookPeriodDepreciationUtils(this);
    this.SuspendedPeriodsIDs = new SortedSet<string>((IEnumerable<string>) GraphHelper.RowCast<FABookHistory>((IEnumerable) PXSelectBase<FABookHistory, PXViewOf<FABookHistory>.BasedOn<SelectFromBase<FABookHistory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookHistory.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FABookHistory.bookID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<FABookHistory.suspended, IBqlBool>.IsEqual<True>>>.Order<PX.Data.BQL.Fluent.By<BqlField<FABookHistory.finPeriodID, IBqlString>.Asc>>>.Config>.Select(this.Graph, new object[2]
    {
      (object) this.AssetID,
      (object) this.BookID
    })).Select<FABookHistory, string>((Func<FABookHistory, string>) (history => history.FinPeriodID)).ToHashSet<string>());
    this.SuspendSections = this.CalculateSuspendSections();
    this.Additions = this.CollectAdditionsFromHistory();
  }

  private List<FAAddition> CollectAdditionsFromHistory()
  {
    List<FAAddition> list = GraphHelper.RowCast<FABookHistory>((IEnumerable) PXSelectBase<FABookHistory, PXViewOf<FABookHistory>.BasedOn<SelectFromBase<FABookHistory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookHistory.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FABookHistory.bookID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<FABookHistory.ptdDeprBase, IBqlDecimal>.IsNotEqual<decimal0>>>.Order<PX.Data.BQL.Fluent.By<BqlField<FABookHistory.finPeriodID, IBqlString>.Asc>>>.Config>.Select(this.Graph, new object[2]
    {
      (object) this.AssetID,
      (object) this.BookID
    })).Select<FABookHistory, FAAddition>((Func<FABookHistory, FAAddition>) (history =>
    {
      Decimal? nullable = history.PtdDeprBase;
      Decimal amount = nullable.Value;
      string finPeriodId = history.FinPeriodID;
      DateTime additionDate = this.CalculateAdditionDate(history.FinPeriodID, this.BookBalance.DeprFromDate);
      int precision = this.Precision;
      nullable = this.BookBalance.BusinessUse;
      Decimal businessUse = nullable.Value;
      return new FAAddition(amount, finPeriodId, additionDate, precision, businessUse);
    })).ToList<FAAddition>();
    if (list.IsEmpty<FAAddition>())
      list.Add(new FAAddition(0M, this.BookBalance.DeprFromPeriod, this.CalculateAdditionDate(this.BookBalance.DeprFromPeriod, this.BookBalance.DeprFromDate), this.Precision, this.BookBalance.BusinessUse.Value));
    list.FirstOrDefault<FAAddition>()?.MarkOriginal(this.BookBalance);
    return list;
  }

  private List<FAAddition> CollectAdditions()
  {
    List<FAAddition> list = GraphHelper.RowCast<FATran>((IEnumerable) PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FATran.bookID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<FATran.released, IBqlBool>.IsEqual<True>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.tranType, Equal<FATran.tranType.purchasingPlus>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.tranType, Equal<FATran.tranType.purchasingMinus>>>>>.And<BqlOperand<FATran.origin, IBqlString>.IsNotEqual<FARegister.origin.split>>>>>>.Config>.Select(this.Graph, new object[2]
    {
      (object) this.AssetID,
      (object) this.BookID
    })).Select<FATran, FAAddition>((Func<FATran, FAAddition>) (transaction =>
    {
      Decimal num = (Decimal) (transaction.TranType == "P-" ? -1 : 1);
      Decimal? nullable = transaction.TranAmt;
      Decimal valueOrDefault = nullable.GetValueOrDefault();
      Decimal amount = num * valueOrDefault;
      string tranPeriodId = transaction.TranPeriodID;
      DateTime additionDate = this.CalculateAdditionDate(transaction.TranPeriodID, transaction.TranDate, this.BookBalance.DeprFromDate);
      int precision = this.Precision;
      nullable = this.BookBalance.BusinessUse;
      Decimal businessUse = nullable.Value;
      return new FAAddition(amount, tranPeriodId, additionDate, precision, businessUse);
    })).GroupBy<FAAddition, (DateTime, string), FAAddition>((Func<FAAddition, (DateTime, string)>) (addition => (addition.Date, addition.PeriodID)), (Func<(DateTime, string), IEnumerable<FAAddition>, FAAddition>) ((key, group) => new FAAddition(group.Sum<FAAddition>((Func<FAAddition, Decimal>) (addition => addition.Amount)), key.PeriodID, key.Date, this.Precision, this.BookBalance.BusinessUse.Value))).ToList<FAAddition>();
    list.Sort((Comparison<FAAddition>) ((x, y) => x.Date.CompareTo(y.Date)));
    list.FirstOrDefault<FAAddition>()?.MarkOriginal(this.BookBalance);
    foreach (PXResult<FATran> pxResult in PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FATran.bookID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<FATran.released, IBqlBool>.IsEqual<True>>>, And<BqlOperand<FATran.tranType, IBqlString>.IsEqual<FATran.tranType.purchasingMinus>>>>.And<BqlOperand<FATran.origin, IBqlString>.IsEqual<FARegister.origin.split>>>.Aggregate<To<GroupBy<FATran.tranPeriodID>, Sum<FATran.tranAmt>>>>.Config>.Select(this.Graph, new object[2]
    {
      (object) this.AssetID,
      (object) this.BookID
    }))
    {
      FATran splitReduceTransaction = PXResult<FATran>.op_Implicit(pxResult);
      this.ReduceProportionally(list, splitReduceTransaction);
    }
    this.CheckCollectedAdditionsInHistory(list);
    return list;
  }

  private DateTime MaxDate(DateTime date1, DateTime date2)
  {
    return new DateTime(Math.Max(date1.Ticks, date2.Ticks));
  }

  private DateTime CalculateAdditionDate(
    string tranPeriodID,
    DateTime? tranDate,
    DateTime? deprFromDate)
  {
    DateTime additionDate = this.MaxDate(this.RepositoryHelper.FindFABookPeriodOfDate(tranDate, this.BookID, this.AssetID).FinPeriodID == tranPeriodID ? tranDate.Value : this.RepositoryHelper.FindOrganizationFABookPeriodByID(tranPeriodID, this.BookID, this.AssetID).StartDate.Value, deprFromDate.Value);
    if (this.RepositoryHelper.GetFABookPeriodIDOfDate(new DateTime?(additionDate), this.BookID, this.AssetID) != tranPeriodID)
      throw new PXException("The start depreciation date {0} of the {2} fixed asset is outside the {1} transaction period.", new object[3]
      {
        (object) additionDate,
        (object) FinPeriodIDFormattingAttribute.FormatForError(tranPeriodID),
        (object) this.FixedAsset.AssetCD
      });
    return additionDate;
  }

  private DateTime CalculateAdditionDate(string periodID, DateTime? deprFromDate)
  {
    DateTime additionDate = this.MaxDate(this.RepositoryHelper.FindOrganizationFABookPeriodByID(periodID, this.BookID, this.AssetID).StartDate.Value, deprFromDate.Value);
    if (this.RepositoryHelper.GetFABookPeriodIDOfDate(new DateTime?(additionDate), this.BookID, this.AssetID) != periodID)
      throw new PXException("The start depreciation date {0} of the {2} fixed asset is outside the {1} transaction period.", new object[3]
      {
        (object) additionDate,
        (object) FinPeriodIDFormattingAttribute.FormatForError(periodID),
        (object) this.FixedAsset.AssetCD
      });
    return additionDate;
  }

  private void CheckCollectedAdditionsInHistory(List<FAAddition> additions)
  {
    HashSet<(string, Decimal)> hashSet1 = additions.GroupBy<FAAddition, string, (string, Decimal)>((Func<FAAddition, string>) (addition => addition.PeriodID), (Func<string, IEnumerable<FAAddition>, (string, Decimal)>) ((finPeriodID, group) => (finPeriodID, group.Sum<FAAddition>((Func<FAAddition, Decimal>) (a => a.Amount))))).ToHashSet<(string, Decimal)>();
    HashSet<(string, Decimal)> hashSet2 = GraphHelper.RowCast<FABookHistory>((IEnumerable) PXSelectBase<FABookHistory, PXViewOf<FABookHistory>.BasedOn<SelectFromBase<FABookHistory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookHistory.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FABookHistory.bookID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<FABookHistory.ptdDeprBase, IBqlDecimal>.IsNotEqual<decimal0>>>>.Config>.Select(this.Graph, new object[2]
    {
      (object) this.AssetID,
      (object) this.BookID
    })).Select<FABookHistory, (string, Decimal)>((Func<FABookHistory, (string, Decimal)>) (history => (history.FinPeriodID, history.PtdDeprBase.Value))).ToHashSet<(string, Decimal)>();
    if (!hashSet1.SetEquals((IEnumerable<(string, Decimal)>) hashSet2))
      throw new PXException("The purchasing additions ({0}) of the {2} fixed asset do not match the depreciation basis history ({1}).", new object[3]
      {
        (object) this.FormatAdditions((IEnumerable<(string, Decimal)>) hashSet1),
        (object) this.FormatAdditions((IEnumerable<(string, Decimal)>) hashSet2),
        (object) this.FixedAsset.AssetCD
      });
  }

  private string FormatAdditions(
    IEnumerable<(string FinPeriodID, Decimal Amount)> additions)
  {
    return string.Join("; ", additions.Select<(string, Decimal), string>((Func<(string, Decimal), string>) (addition => $"{addition.FinPeriodID}: {addition.Amount}")));
  }

  private void ReduceProportionally(List<FAAddition> additions, FATran splitReduceTransaction)
  {
    List<FAAddition> list = additions.Where<FAAddition>((Func<FAAddition, bool>) (addition => addition.PeriodID == splitReduceTransaction.TranPeriodID)).ToList<FAAddition>();
    Decimal num1 = list.Sum<FAAddition>((Func<FAAddition, Decimal>) (addition => addition.Amount));
    Decimal num2 = 0M;
    foreach (FAAddition faAddition in list)
    {
      Decimal amount = faAddition.Amount;
      Decimal? nullable1 = splitReduceTransaction.TranAmt;
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(amount * nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal num3 = num1;
      Decimal? nullable3;
      if (!nullable2.HasValue)
      {
        nullable1 = new Decimal?();
        nullable3 = nullable1;
      }
      else
        nullable3 = new Decimal?(nullable2.GetValueOrDefault() / num3);
      nullable1 = nullable3;
      Decimal num4 = PXRounder.Round(nullable1.GetValueOrDefault(), this.Precision);
      num2 += num4;
      faAddition.Amount -= num4;
    }
    FAAddition faAddition1 = list.Last<FAAddition>();
    Decimal amount1 = faAddition1.Amount;
    Decimal? tranAmt = splitReduceTransaction.TranAmt;
    Decimal num5 = num2;
    Decimal valueOrDefault = (tranAmt.HasValue ? new Decimal?(tranAmt.GetValueOrDefault() - num5) : new Decimal?()).GetValueOrDefault();
    faAddition1.Amount = amount1 - valueOrDefault;
  }

  private List<(string, string)> CalculateSuspendSections()
  {
    List<(string, string)> suspendSections = new List<(string, string)>();
    if (this.SuspendedPeriodsIDs.Count > 0)
    {
      string str1 = this.SuspendedPeriodsIDs.First<string>();
      string periodID1 = this.SuspendedPeriodsIDs.First<string>();
      string periodID2 = this.SuspendedPeriodsIDs.First<string>();
      foreach (string suspendedPeriodsId in this.SuspendedPeriodsIDs)
      {
        string str2;
        periodID1 = str2 = suspendedPeriodsId;
        if (this.PeriodDepreciationUtils.GetNumberOfPeriodsBetweenPeriods(periodID1, periodID2) > 1)
        {
          suspendSections.Add((str1, periodID2));
          str1 = periodID1;
        }
        periodID2 = str2;
      }
      suspendSections.Add((str1, periodID1));
    }
    suspendSections.Sort();
    return suspendSections;
  }
}
