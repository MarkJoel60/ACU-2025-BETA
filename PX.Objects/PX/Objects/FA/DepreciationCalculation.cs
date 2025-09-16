// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationCalculation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.FA.DepreciationMethods;
using PX.Objects.FA.DepreciationMethods.Parameters;
using PX.Objects.FA.Overrides.AssetProcess;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FA;

public class DepreciationCalculation : PXGraph<DepreciationCalculation, FADepreciationMethod>
{
  protected int _Precision;
  protected DepreciationCalculation.FADestination _Destination;
  public PXSelect<FADepreciationMethod> DepreciationMethod;
  public PXSelect<FADepreciationMethodLines, Where<FADepreciationMethodLines.methodID, Equal<Current<FADepreciationMethod.methodID>>>> DepreciationMethodLines;
  public PXSetup<PX.Objects.FA.FASetup> FASetup;
  public PXSelect<FABookHistory> RawHistory;
  public PXSelect<FABookHist> BookHistory;
  public PXSelect<FABookBalance> BookBalance;
  public PXSelect<PX.Objects.FA.FADetails> FADetails;
  private Dictionary<string, FABookHist> histdict;
  public DeprCalcParameters Params;
  private int previousCalculatedPeriods;

  public DepreciationCalculation()
  {
    PX.Objects.FA.FASetup current = ((PXSelectBase<PX.Objects.FA.FASetup>) this.FASetup).Current;
    PXCache cache = ((PXSelectBase) this.DepreciationMethodLines).Cache;
    cache.AllowDelete = false;
    cache.AllowInsert = false;
    PXDBCurrencyAttribute.SetBaseCalc<FADepreciationMethod.totalPercents>(((PXSelectBase) this.DepreciationMethod).Cache, (object) null, true);
    this.Params = new DeprCalcParameters();
  }

  public virtual void Clear()
  {
    PXDBDecimalAttribute.EnsurePrecision(((PXGraph) this).Caches[typeof (FABookHist)]);
    ((PXGraph) this).Clear();
  }

  public Decimal Round(Decimal value)
  {
    return Math.Round(value, this._Precision, MidpointRounding.AwayFromZero);
  }

  public virtual bool UseAcceleratedDepreciation(FixedAsset cls, FADepreciationMethod method)
  {
    if (cls.AcceleratedDepreciation.GetValueOrDefault() && method != null && method.IsPureStraightLine)
      return true;
    if (method == null || method.IsTableMethod.GetValueOrDefault())
      return false;
    return method.DepreciationMethod == "RV" || method.DepreciationMethod == "RD";
  }

  [InjectDependency]
  public IFABookPeriodRepository FABookPeriodRepository { get; set; }

  [InjectDependency]
  public IFABookPeriodUtils FABookPeriodUtils { get; set; }

  public virtual void CalculateDepreciationAddition(
    FixedAsset cls,
    FABookBalance bookbal,
    FADepreciationMethod method,
    FABookHistory next)
  {
    string deprFromPeriod = bookbal.DeprFromPeriod;
    string str = this.FABookPeriodUtils.PeriodPlusPeriodsCount(next.FinPeriodID, -next.YtdReversed.GetValueOrDefault(), next.BookID, next.AssetID);
    DateTime? nullable1 = new DateTime?();
    if (this.UseAcceleratedDepreciation(cls, method) && string.CompareOrdinal(str, deprFromPeriod) > 0)
    {
      this.Params.Fill((PXGraph) this, bookbal, depreciationMethod: method);
      nullable1 = new DateTime?(this.Params.RecoveryEndDate);
    }
    FABookBalance copy1 = PXCache<FABookBalance>.CreateCopy(bookbal);
    bookbal.DeprFromPeriod = str;
    bookbal.DeprFromDate = str == deprFromPeriod ? bookbal.DeprFromDate : new DateTime?(this.FABookPeriodUtils.GetFABookPeriodStartDate(str, bookbal.BookID, bookbal.AssetID));
    bookbal.DeprToPeriod = this.FABookPeriodUtils.PeriodPlusPeriodsCount(bookbal.DeprToPeriod, -bookbal.YtdSuspended.GetValueOrDefault(), bookbal.BookID, bookbal.AssetID);
    DateTime? nullable2 = bookbal.DeprToDate;
    if (nullable2.HasValue)
    {
      FABookPeriod bookPeriodOfDate = this.FABookPeriodRepository.FindFABookPeriodOfDate(bookbal.DeprToDate, bookbal.BookID, bookbal.AssetID);
      FABookPeriod faBookPeriodById = this.FABookPeriodRepository.FindOrganizationFABookPeriodByID(bookbal.DeprToPeriod, bookbal.BookID, bookbal.AssetID);
      // ISSUE: variable of a boxed type
      __Boxed<DateTime?> deprToDate = (ValueType) bookbal.DeprToDate;
      nullable2 = bookPeriodOfDate.EndDate;
      DateTime dateTime = nullable2.Value;
      // ISSUE: variable of a boxed type
      __Boxed<DateTime> objB = (ValueType) dateTime.AddDays(-1.0);
      if (object.Equals((object) deprToDate, (object) objB))
      {
        FABookBalance faBookBalance = bookbal;
        nullable2 = faBookPeriodById.EndDate;
        dateTime = nullable2.Value;
        DateTime? nullable3 = new DateTime?(dateTime.AddDays(-1.0));
        faBookBalance.DeprToDate = nullable3;
      }
      else
      {
        nullable2 = bookbal.DeprToDate;
        DateTime? startDate = bookPeriodOfDate.StartDate;
        int days1 = (nullable2.HasValue & startDate.HasValue ? new TimeSpan?(nullable2.GetValueOrDefault() - startDate.GetValueOrDefault()) : new TimeSpan?()).Value.Days;
        FABookBalance faBookBalance = bookbal;
        int num = days1;
        nullable2 = faBookPeriodById.EndDate;
        startDate = faBookPeriodById.StartDate;
        int days2 = (nullable2.HasValue & startDate.HasValue ? new TimeSpan?(nullable2.GetValueOrDefault() - startDate.GetValueOrDefault()) : new TimeSpan?()).Value.Days;
        DateTime? nullable4;
        if (num >= days2)
        {
          nullable4 = faBookPeriodById.EndDate;
        }
        else
        {
          dateTime = faBookPeriodById.StartDate.Value;
          nullable4 = new DateTime?(dateTime.AddDays((double) days1));
        }
        faBookBalance.DeprToDate = nullable4;
      }
      FABookBalance copy2 = PXCache<FABookBalance>.CreateCopy(bookbal);
      FABookBalance faBookBalance1 = copy2;
      nullable2 = new DateTime?();
      DateTime? nullable5 = nullable2;
      faBookBalance1.DeprToDate = nullable5;
      this.Params.Fill((PXGraph) this, copy2, depreciationMethod: method, recoveryEndDate: nullable1);
      DateTime recoveryEndDate = this.Params.RecoveryEndDate;
      nullable2 = bookbal.DeprToDate;
      DateTime t2 = nullable2.Value;
      if (DateTime.Compare(recoveryEndDate, t2) < 0)
      {
        bookbal.DeprToDate = new DateTime?(this.Params.RecoveryEndDate);
        bookbal.DeprToPeriod = this.FABookPeriodRepository.GetFABookPeriodIDOfDate(new DateTime?(this.Params.RecoveryEndDate), bookbal.BookID, bookbal.AssetID);
      }
    }
    FABookBalance faBookBalance2 = bookbal;
    Decimal? ptdDeprBase = next.PtdDeprBase;
    Decimal? nullable6 = bookbal.BusinessUse;
    Decimal? nullable7 = new Decimal?(this.Round((ptdDeprBase.HasValue & nullable6.HasValue ? new Decimal?(ptdDeprBase.GetValueOrDefault() * nullable6.GetValueOrDefault() * 0.01M) : new Decimal?()).Value));
    faBookBalance2.AcquisitionCost = nullable7;
    FABookBalance faBookBalance3 = bookbal;
    Decimal? acquisitionCost = faBookBalance3.AcquisitionCost;
    nullable6 = str == deprFromPeriod ? bookbal.Tax179Amount : new Decimal?(0M);
    faBookBalance3.AcquisitionCost = acquisitionCost.HasValue & nullable6.HasValue ? new Decimal?(acquisitionCost.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
    FABookBalance faBookBalance4 = bookbal;
    nullable6 = faBookBalance4.AcquisitionCost;
    Decimal? nullable8 = str == deprFromPeriod ? bookbal.BonusAmount : new Decimal?(0M);
    faBookBalance4.AcquisitionCost = nullable6.HasValue & nullable8.HasValue ? new Decimal?(nullable6.GetValueOrDefault() - nullable8.GetValueOrDefault()) : new Decimal?();
    bookbal.SalvageAmount = str == deprFromPeriod ? bookbal.SalvageAmount : new Decimal?(0M);
    this._Destination = DepreciationCalculation.FADestination.Calculated;
    this.CalculateDepreciation(method, bookbal, nullable1);
    copy1.MaxHistoryPeriodID = bookbal.MaxHistoryPeriodID;
    PXCache<FABookBalance>.RestoreCopy(bookbal, copy1);
  }

  public virtual void CalculateDepreciation(FABookBalance assetBalance, string maxPeriodID)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    DepreciationCalculation.\u003C\u003Ec__DisplayClass24_0 cDisplayClass240 = new DepreciationCalculation.\u003C\u003Ec__DisplayClass24_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass240.maxPeriodID = maxPeriodID;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass240.assetBalance = assetBalance;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    PXUpdate<Set<FABookHistory.ptdCalculated, decimal0, Set<FABookHistory.ytdCalculated, decimal0>>, FABookHistory, Where<FABookHistory.assetID, Equal<Required<FABookHistory.assetID>>, And<FABookHistory.bookID, Equal<Required<FABookHistory.bookID>>>>>.Update((PXGraph) this, new object[2]
    {
      (object) cDisplayClass240.assetBalance.AssetID,
      (object) cDisplayClass240.assetBalance.BookID
    });
    this.histdict = new Dictionary<string, FABookHist>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    foreach (PXResult<FABookHist> pxResult in PXSelectBase<FABookHist, PXSelectReadonly<FABookHist, Where<FABookHist.assetID, Equal<Required<FABookHist.assetID>>, And<FABookHist.bookID, Equal<Required<FABookHist.bookID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) cDisplayClass240.assetBalance.AssetID,
      (object) cDisplayClass240.assetBalance.BookID
    }))
    {
      FABookHist faBookHist = PXResult<FABookHist>.op_Implicit(pxResult);
      this.histdict[faBookHist.FinPeriodID] = faBookHist;
    }
    // ISSUE: reference to a compiler-generated field
    FADepreciationMethod method = PXResultset<FADepreciationMethod>.op_Implicit(PXSelectBase<FADepreciationMethod, PXSelect<FADepreciationMethod, Where<FADepreciationMethod.methodID, Equal<Required<FADepreciationMethod.methodID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) cDisplayClass240.assetBalance.DepreciationMethodID
    }));
    // ISSUE: reference to a compiler-generated field
    PXResult<FixedAsset, FAClass> pxResult1 = (PXResult<FixedAsset, FAClass>) ((IQueryable<PXResult<FixedAsset>>) PXSelectBase<FixedAsset, PXSelectJoin<FixedAsset, LeftJoin<FAClass, On<FAClass.assetID, Equal<FixedAsset.classID>>>, Where<FixedAsset.assetID, Equal<Required<FABookBalance.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) cDisplayClass240.assetBalance.AssetID
    })).FirstOrDefault<PXResult<FixedAsset>>();
    FAClass cls = ((PXResult) pxResult1).GetItem<FAClass>();
    FixedAsset asset = ((PXResult) pxResult1).GetItem<FixedAsset>();
    this._Precision = (int) (PXResultset<PX.Objects.CM.Currency>.op_Implicit(PXSelectBase<PX.Objects.CM.Currency, PXSelect<PX.Objects.CM.Currency, Where<BqlOperand<PX.Objects.CM.Currency.curyID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) asset.BaseCuryID
    })).DecimalPlaces ?? (short) 4);
    // ISSUE: reference to a compiler-generated field
    bool? nullable1 = cDisplayClass240.assetBalance.Depreciate;
    if (nullable1.GetValueOrDefault())
    {
      nullable1 = cls.UnderConstruction;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (!nullable1.GetValueOrDefault() && (string.IsNullOrEmpty(cDisplayClass240.assetBalance.DeprFromPeriod) || string.IsNullOrEmpty(cDisplayClass240.assetBalance.DeprToPeriod) || string.CompareOrdinal(cDisplayClass240.assetBalance.DeprFromPeriod, cDisplayClass240.assetBalance.DeprToPeriod) > 0))
      {
        // ISSUE: reference to a compiler-generated field
        throw new PXException("Incorrect periods beginning and end of depreciation for Book '{0}'", new object[1]
        {
          (object) PXResultset<FABook>.op_Implicit(PXSelectBase<FABook, PXSelect<FABook, Where<FABook.bookID, Equal<Current<FABookBalance.bookID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
          {
            (object) cDisplayClass240.assetBalance
          }, Array.Empty<object>())).BookCode
        });
      }
    }
    // ISSUE: reference to a compiler-generated field
    PX.Objects.FA.FADetails faDetails = PXResultset<PX.Objects.FA.FADetails>.op_Implicit(PXSelectBase<PX.Objects.FA.FADetails, PXSelect<PX.Objects.FA.FADetails, Where<PX.Objects.FA.FADetails.assetID, Equal<Required<FABookBalance.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) cDisplayClass240.assetBalance.AssetID
    }));
    GraphHelper.MarkUpdated(((PXSelectBase) this.FADetails).Cache, (object) faDetails);
    // ISSUE: reference to a compiler-generated field
    string str = cDisplayClass240.assetBalance.DeprFromPeriod;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass240.maxPeriodID == null || string.CompareOrdinal(cDisplayClass240.maxPeriodID, cDisplayClass240.assetBalance.DeprToPeriod) > 0)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass240.maxPeriodID = cDisplayClass240.assetBalance.DeprToPeriod;
    }
    // ISSUE: method pointer
    PXRowInserting pxRowInserting1 = new PXRowInserting((object) cDisplayClass240, __methodptr(\u003CCalculateDepreciation\u003Eb__0));
    ((PXGraph) this).RowInserting.AddHandler<FABookHist>(pxRowInserting1);
    // ISSUE: reference to a compiler-generated field
    foreach (PXResult<FABookHistoryNextPeriod, FABookHistory> pxResult2 in PXSelectBase<FABookHistoryNextPeriod, PXSelectReadonly2<FABookHistoryNextPeriod, InnerJoin<FABookHistory, On<FABookHistory.assetID, Equal<FABookHistoryNextPeriod.assetID>, And<FABookHistory.bookID, Equal<FABookHistoryNextPeriod.bookID>, And<FABookHistory.finPeriodID, Equal<FABookHistoryNextPeriod.nextPeriodID>>>>>, Where<FABookHistoryNextPeriod.assetID, Equal<Current<FABookBalance.assetID>>, And<FABookHistoryNextPeriod.bookID, Equal<Current<FABookBalance.bookID>>, And<FABookHistoryNextPeriod.ptdDeprBase, NotEqual<decimal0>, And<FABookHistoryNextPeriod.finPeriodID, LessEqual<Current<FABookBalance.deprToPeriod>>>>>>, OrderBy<Asc<FABookHistoryNextPeriod.finPeriodID>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) cDisplayClass240.assetBalance
    }, Array.Empty<object>()))
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      DepreciationCalculation.\u003C\u003Ec__DisplayClass24_1 cDisplayClass241 = new DepreciationCalculation.\u003C\u003Ec__DisplayClass24_1();
      FABookHistory next = PXResult<FABookHistoryNextPeriod, FABookHistory>.op_Implicit(pxResult2);
      next.PtdDeprBase = PXResult<FABookHistoryNextPeriod, FABookHistory>.op_Implicit(pxResult2).PtdDeprBase;
      // ISSUE: reference to a compiler-generated field
      FABookBalance assetBalance1 = cDisplayClass240.assetBalance;
      string deprFromPeriod = assetBalance1.DeprFromPeriod;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass241.additionDeprFromPeriod = this.FABookPeriodUtils.PeriodPlusPeriodsCount(next.FinPeriodID, -next.YtdReversed.GetValueOrDefault(), next.BookID, next.AssetID);
      // ISSUE: reference to a compiler-generated field
      if (string.CompareOrdinal(cDisplayClass241.additionDeprFromPeriod, str) < 0)
      {
        // ISSUE: reference to a compiler-generated field
        str = cDisplayClass241.additionDeprFromPeriod;
      }
      // ISSUE: method pointer
      PXRowInserting pxRowInserting2 = new PXRowInserting((object) cDisplayClass241, __methodptr(\u003CCalculateDepreciation\u003Eb__2));
      ((PXGraph) this).RowInserting.AddHandler<FABookHist>(pxRowInserting2);
      this.CalculateDepreciationAddition((FixedAsset) cls, assetBalance1, method, next);
      Decimal? nullable2;
      Decimal? nullable3;
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass241.additionDeprFromPeriod == deprFromPeriod)
      {
        nullable2 = assetBalance1.Tax179Amount;
        Decimal num = 0M;
        if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
        {
          FABookHist keyedHistory = new FABookHist();
          keyedHistory.AssetID = assetBalance1.AssetID;
          keyedHistory.BookID = assetBalance1.BookID;
          keyedHistory.FinPeriodID = deprFromPeriod;
          FABookHist faBookHist1 = ((PXSelectBase<FABookHist>) this.BookHistory).Insert(FAHelper.InsertFABookHist((PXGraph) this, keyedHistory, ref assetBalance1));
          FABookHist faBookHist2 = faBookHist1;
          nullable2 = faBookHist2.PtdCalculated;
          nullable3 = assetBalance1.Tax179Amount;
          faBookHist2.PtdCalculated = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          FABookHist faBookHist3 = faBookHist1;
          nullable3 = faBookHist3.YtdCalculated;
          nullable2 = assetBalance1.Tax179Amount;
          faBookHist3.YtdCalculated = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
          FABookHist faBookHist4 = faBookHist1;
          nullable2 = faBookHist4.PtdTax179Calculated;
          nullable3 = assetBalance1.Tax179Amount;
          faBookHist4.PtdTax179Calculated = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          FABookHist faBookHist5 = faBookHist1;
          nullable3 = faBookHist5.YtdTax179Calculated;
          nullable2 = assetBalance1.Tax179Amount;
          faBookHist5.YtdTax179Calculated = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
          FABookBalance copy = PXCache<FABookBalance>.CreateCopy(assetBalance1);
          assetBalance1.AcquisitionCost = assetBalance1.Tax179Amount;
          assetBalance1.SalvageAmount = new Decimal?(0M);
          this._Destination = DepreciationCalculation.FADestination.Tax179;
          this.CalculateDepreciation(method, assetBalance1);
          copy.MaxHistoryPeriodID = assetBalance1.MaxHistoryPeriodID;
          PXCache<FABookBalance>.RestoreCopy(assetBalance1, copy);
        }
      }
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass241.additionDeprFromPeriod == deprFromPeriod)
      {
        nullable2 = assetBalance1.BonusAmount;
        Decimal num = 0M;
        if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
        {
          FABookHist keyedHistory = new FABookHist();
          keyedHistory.AssetID = assetBalance1.AssetID;
          keyedHistory.BookID = assetBalance1.BookID;
          keyedHistory.FinPeriodID = deprFromPeriod;
          FABookHist faBookHist6 = FAHelper.InsertFABookHist((PXGraph) this, keyedHistory, ref assetBalance1);
          FABookHist faBookHist7 = faBookHist6;
          nullable2 = faBookHist7.PtdCalculated;
          nullable3 = assetBalance1.BonusAmount;
          faBookHist7.PtdCalculated = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          FABookHist faBookHist8 = faBookHist6;
          nullable3 = faBookHist8.YtdCalculated;
          nullable2 = assetBalance1.BonusAmount;
          faBookHist8.YtdCalculated = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
          FABookHist faBookHist9 = faBookHist6;
          nullable2 = faBookHist9.PtdBonusCalculated;
          nullable3 = assetBalance1.BonusAmount;
          faBookHist9.PtdBonusCalculated = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          FABookHist faBookHist10 = faBookHist6;
          nullable3 = faBookHist10.YtdBonusCalculated;
          nullable2 = assetBalance1.BonusAmount;
          faBookHist10.YtdBonusCalculated = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
          FABookBalance copy = PXCache<FABookBalance>.CreateCopy(assetBalance1);
          assetBalance1.AcquisitionCost = assetBalance1.BonusAmount;
          assetBalance1.SalvageAmount = new Decimal?(0M);
          this._Destination = DepreciationCalculation.FADestination.Bonus;
          this.CalculateDepreciation(method, assetBalance1);
          copy.MaxHistoryPeriodID = assetBalance1.MaxHistoryPeriodID;
          PXCache<FABookBalance>.RestoreCopy(assetBalance1, copy);
        }
      }
      ((PXGraph) this).RowInserting.RemoveHandler<FABookHist>(pxRowInserting2);
    }
    PXCache cach = ((PXGraph) this).Caches[typeof (FABookHist)];
    List<FABookHist> source = new List<FABookHist>((IEnumerable<FABookHist>) cach.Inserted);
    source.Sort((Comparison<FABookHist>) ((a, b) => string.CompareOrdinal(a.FinPeriodID, b.FinPeriodID)));
    Decimal? nullable4 = new Decimal?(0M);
    string strB = str;
    Decimal? nullable5 = new Decimal?(0M);
    bool flag = true;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass240.assetBalance.UpdateGL.GetValueOrDefault())
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      OrganizationFinPeriod organizationFinPeriod = PXResultset<OrganizationFinPeriod>.op_Implicit(PXSelectBase<OrganizationFinPeriod, PXViewOf<OrganizationFinPeriod>.BasedOn<SelectFromBase<OrganizationFinPeriod, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FABookBalance>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookBalance.assetID, Equal<P.AsInt>>>>>.And<BqlOperand<FABookBalance.bookID, IBqlInt>.IsEqual<P.AsInt>>>>, FbqlJoins.Inner<FixedAsset>.On<BqlOperand<FixedAsset.assetID, IBqlInt>.IsEqual<FABookBalance.assetID>>>, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<FixedAsset.branchID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.Branch.organizationID, Equal<OrganizationFinPeriod.organizationID>>>>, And<BqlOperand<OrganizationFinPeriod.finPeriodID, IBqlString>.IsGreaterEqual<FABookBalance.deprFromPeriod>>>, And<BqlOperand<OrganizationFinPeriod.finPeriodID, IBqlString>.IsLessEqual<FABookBalance.deprToPeriod>>>, And<BqlOperand<OrganizationFinPeriod.fAClosed, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<OrganizationFinPeriod.startDate, IBqlDateTime>.IsNotEqual<OrganizationFinPeriod.endDate>>>.Order<PX.Data.BQL.Fluent.By<Desc<OrganizationFinPeriod.finPeriodID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
      {
        (object) cDisplayClass240.assetBalance.AssetID,
        (object) cDisplayClass240.assetBalance.BookID
      }));
      if (organizationFinPeriod != null)
      {
        // ISSUE: reference to a compiler-generated field
        PX.Objects.FA.AssetProcess.SetLastDeprPeriod(asset, (PXSelectBase<FABookBalance>) this.BookBalance, cDisplayClass240.assetBalance, organizationFinPeriod.FinPeriodID);
        // ISSUE: reference to a compiler-generated field
        PX.Objects.FA.AssetProcess.AdjustFixedAssetStatus((PXGraph) this, cDisplayClass240.assetBalance.AssetID);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated method
        foreach (FABookHist faBookHist in source.Where<FABookHist>(cDisplayClass240.\u003C\u003E9__3 ?? (cDisplayClass240.\u003C\u003E9__3 = new Func<FABookHist, bool>(cDisplayClass240.\u003CCalculateDepreciation\u003Eb__3))))
        {
          if (!this.histdict.ContainsKey(faBookHist.FinPeriodID))
            this.histdict[faBookHist.FinPeriodID] = faBookHist;
        }
      }
    }
    foreach (FABookHist faBookHist11 in source)
    {
      FABookHist faBookHist12 = faBookHist11;
      Decimal? nullable6 = faBookHist12.YtdCalculated;
      Decimal? nullable7 = nullable4;
      faBookHist12.YtdCalculated = nullable6.HasValue & nullable7.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + nullable7.GetValueOrDefault()) : new Decimal?();
      nullable7 = nullable4;
      nullable6 = faBookHist11.PtdCalculated;
      nullable4 = nullable7.HasValue & nullable6.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
      FABookHist faBookHist13;
      if (this.histdict.TryGetValue(faBookHist11.FinPeriodID, out faBookHist13))
      {
        faBookHist11.PtdDepreciated = faBookHist13.PtdDepreciated;
        faBookHist11.YtdDepreciated = faBookHist13.YtdDepreciated;
        // ISSUE: reference to a compiler-generated field
        if (this.UseAcceleratedDepreciation((FixedAsset) cls, method) && string.Equals(faBookHist13.FinPeriodID, cDisplayClass240.assetBalance.CurrDeprPeriod))
        {
          nullable6 = faBookHist11.YtdCalculated;
          Decimal num1 = nullable6.Value;
          nullable6 = faBookHist11.PtdCalculated;
          Decimal num2 = nullable6.Value;
          Decimal num3 = num1 - num2;
          nullable6 = faBookHist13.YtdDepreciated;
          Decimal num4 = nullable6.Value;
          if (Math.Abs(num3 - num4) > 0.00005M)
          {
            Decimal? ytdCalculated = faBookHist11.YtdCalculated;
            Decimal? nullable8 = faBookHist11.PtdCalculated;
            nullable6 = ytdCalculated.HasValue & nullable8.HasValue ? new Decimal?(ytdCalculated.GetValueOrDefault() - nullable8.GetValueOrDefault()) : new Decimal?();
            nullable7 = faBookHist13.YtdDepreciated;
            Decimal? nullable9;
            if (!(nullable6.HasValue & nullable7.HasValue))
            {
              nullable8 = new Decimal?();
              nullable9 = nullable8;
            }
            else
              nullable9 = new Decimal?(nullable6.GetValueOrDefault() - nullable7.GetValueOrDefault());
            Decimal? nullable10 = nullable9;
            // ISSUE: reference to a compiler-generated field
            FABookBalance copy = PXCache<FABookBalance>.CreateCopy(cDisplayClass240.assetBalance);
            copy.NoteID = new Guid?();
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            FABookHistory next = new FABookHistory()
            {
              AssetID = cDisplayClass240.assetBalance.AssetID,
              FinPeriodID = cDisplayClass240.assetBalance.CurrDeprPeriod,
              BookID = cDisplayClass240.assetBalance.BookID,
              PtdDeprBase = nullable10,
              YtdSuspended = faBookHist13.YtdSuspended,
              YtdReversed = faBookHist13.YtdReversed
            };
            this.CalculateDepreciationAddition((FixedAsset) cls, copy, method, next);
            FABookHist faBookHist14 = faBookHist11;
            nullable7 = faBookHist13.YtdDepreciated;
            nullable6 = faBookHist11.PtdCalculated;
            Decimal? nullable11;
            if (!(nullable7.HasValue & nullable6.HasValue))
            {
              nullable8 = new Decimal?();
              nullable11 = nullable8;
            }
            else
              nullable11 = new Decimal?(nullable7.GetValueOrDefault() + nullable6.GetValueOrDefault());
            faBookHist14.YtdCalculated = nullable11;
            nullable4 = faBookHist11.YtdCalculated;
          }
        }
        if (flag)
        {
          strB = faBookHist11.FinPeriodID;
          // ISSUE: reference to a compiler-generated field
          if (this.UseAcceleratedDepreciation((FixedAsset) cls, method) && string.CompareOrdinal(faBookHist11.FinPeriodID, cDisplayClass240.assetBalance.CurrDeprPeriod) < 0)
          {
            nullable6 = faBookHist13.YtdDepreciated;
            Decimal num5 = nullable6.Value;
            nullable6 = faBookHist13.YtdCalculated;
            Decimal num6 = nullable6.Value;
            if (Math.Abs(num5 - num6) >= 0.00005M)
            {
              flag = false;
              continue;
            }
            nullable5 = faBookHist13.YtdDepreciated;
          }
          else
          {
            nullable6 = faBookHist13.YtdCalculated;
            Decimal num7 = nullable6.Value;
            nullable6 = faBookHist11.YtdCalculated;
            Decimal num8 = nullable6.Value;
            if (Math.Abs(num7 - num8) >= 0.00005M)
            {
              flag = false;
              continue;
            }
            nullable5 = faBookHist13.YtdCalculated;
          }
          cach.SetStatus((object) faBookHist11, (PXEntryStatus) 0);
        }
      }
      else if (flag)
      {
        strB = faBookHist11.FinPeriodID;
        flag = false;
      }
    }
    ((PXGraph) this).RowInserting.RemoveHandler<FABookHist>(pxRowInserting1);
    foreach (FABookHist faBookHist15 in source)
    {
      Decimal num9 = 0M;
      // ISSUE: reference to a compiler-generated field
      if (this.UseAcceleratedDepreciation((FixedAsset) cls, method) && string.CompareOrdinal(faBookHist15.FinPeriodID, cDisplayClass240.assetBalance.CurrDeprPeriod) < 0)
        faBookHist15.PtdCalculated = faBookHist15.PtdDepreciated;
      FABookHist faBookHist16;
      if (this.UseAcceleratedDepreciation((FixedAsset) cls, method) && string.CompareOrdinal(faBookHist15.FinPeriodID, strB) >= 0 && this.histdict.TryGetValue(faBookHist15.FinPeriodID, out faBookHist16))
        num9 = faBookHist16.PtdAdjusted.GetValueOrDefault();
      FABookHist faBookHist17 = faBookHist15;
      Decimal? ptdCalculated = faBookHist15.PtdCalculated;
      Decimal num10 = num9;
      Decimal? nullable12 = ptdCalculated.HasValue ? new Decimal?(ptdCalculated.GetValueOrDefault() + num10) : new Decimal?();
      faBookHist17.YtdCalculated = nullable12;
      faBookHist15.PtdDepreciated = new Decimal?(0M);
      faBookHist15.YtdDepreciated = new Decimal?(0M);
    }
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      string period1 = this.FABookPeriodRepository.GetFABookPeriodIDOfDate(faDetails.ReceiptDate, cDisplayClass240.assetBalance.BookID, cDisplayClass240.assetBalance.AssetID, false) ?? str;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      PXDatabase.Delete<FABookHistory>(new PXDataFieldRestrict[3]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<FABookHistory.assetID>((PXDbType) 8, new int?(4), (object) cDisplayClass240.assetBalance.AssetID, (PXComp) 0),
        (PXDataFieldRestrict) new PXDataFieldRestrict<FABookHistory.bookID>((PXDbType) 8, new int?(4), (object) cDisplayClass240.assetBalance.BookID, (PXComp) 0),
        (PXDataFieldRestrict) new PXDataFieldRestrict<FABookHistory.finPeriodID>((PXDbType) 3, new int?(6), (object) FinPeriodUtils.Min(period1, str), (PXComp) 4)
      });
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (string.CompareOrdinal(cDisplayClass240.assetBalance.LastDeprPeriod, cDisplayClass240.assetBalance.DeprToPeriod) < 0)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        PXDatabase.Delete<FABookHistory>(new PXDataFieldRestrict[3]
        {
          (PXDataFieldRestrict) new PXDataFieldRestrict<FABookHistory.assetID>((PXDbType) 8, new int?(4), (object) cDisplayClass240.assetBalance.AssetID, (PXComp) 0),
          (PXDataFieldRestrict) new PXDataFieldRestrict<FABookHistory.bookID>((PXDbType) 8, new int?(4), (object) cDisplayClass240.assetBalance.BookID, (PXComp) 0),
          (PXDataFieldRestrict) new PXDataFieldRestrict<FABookHistory.finPeriodID>((PXDbType) 3, new int?(6), (object) cDisplayClass240.assetBalance.DeprToPeriod, (PXComp) 2)
        });
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass240.assetBalance.MaxHistoryPeriodID = FAHelper.GetFABookHistoryMaxPeriodID((PXGraph) this, cDisplayClass240.assetBalance.AssetID, cDisplayClass240.assetBalance.BookID);
      // ISSUE: reference to a compiler-generated field
      GraphHelper.EnsureRowPersistence((PXGraph) this, (object) cDisplayClass240.assetBalance);
      if (!flag)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        PXDatabase.Update<FABookHistory>(new PXDataFieldParam[14]
        {
          (PXDataFieldParam) new PXDataFieldRestrict<FABookHistory.assetID>((PXDbType) 8, new int?(4), (object) cDisplayClass240.assetBalance.AssetID, (PXComp) 0),
          (PXDataFieldParam) new PXDataFieldRestrict<FABookHistory.bookID>((PXDbType) 8, new int?(4), (object) cDisplayClass240.assetBalance.BookID, (PXComp) 0),
          (PXDataFieldParam) new PXDataFieldRestrict<FABookHistory.finPeriodID>((PXDbType) 3, new int?(6), (object) strB, (PXComp) 3),
          (PXDataFieldParam) new PXDataFieldRestrict<FABookHistory.finPeriodID>((PXDbType) 3, new int?(6), (object) cDisplayClass240.maxPeriodID, (PXComp) 5),
          (PXDataFieldParam) new PXDataFieldAssign<FABookHistory.ptdCalculated>((PXDbType) 5, (object) 0M),
          (PXDataFieldParam) new PXDataFieldAssign<FABookHistory.ytdCalculated>((PXDbType) 5, (object) nullable5),
          (PXDataFieldParam) new PXDataFieldAssign<FABookHistory.ptdBonusCalculated>((PXDbType) 5, (object) 0M),
          (PXDataFieldParam) new PXDataFieldAssign<FABookHistory.ytdBonusCalculated>((PXDbType) 5, (object) 0M),
          (PXDataFieldParam) new PXDataFieldAssign<FABookHistory.ptdTax179Calculated>((PXDbType) 5, (object) 0M),
          (PXDataFieldParam) new PXDataFieldAssign<FABookHistory.ytdTax179Calculated>((PXDbType) 5, (object) 0M),
          (PXDataFieldParam) new PXDataFieldAssign<FABookHistory.ptdBonusTaken>((PXDbType) 5, (object) 0M),
          (PXDataFieldParam) new PXDataFieldAssign<FABookHistory.ytdBonusTaken>((PXDbType) 5, (object) 0M),
          (PXDataFieldParam) new PXDataFieldAssign<FABookHistory.ptdTax179Taken>((PXDbType) 5, (object) 0M),
          (PXDataFieldParam) new PXDataFieldAssign<FABookHistory.ytdTax179Taken>((PXDbType) 5, (object) 0M)
        });
      }
      ((PXAction) this.Save).Press();
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass240.assetBalance.UpdateGL.GetValueOrDefault())
      {
        // ISSUE: reference to a compiler-generated field
        foreach (PXResult<FABookHistory> pxResult3 in PXSelectBase<FABookHistory, PXSelectJoin<FABookHistory, InnerJoin<FixedAsset, On<FixedAsset.assetID, Equal<FABookHistory.assetID>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<FixedAsset.branchID>>, InnerJoin<OrganizationFinPeriod, On<OrganizationFinPeriod.organizationID, Equal<PX.Objects.GL.Branch.organizationID>, And<OrganizationFinPeriod.finPeriodID, Equal<FABookHistory.finPeriodID>>>>>>, Where<FABookHistory.closed, NotEqual<True>, And<OrganizationFinPeriod.fAClosed, Equal<True>, And<FABookHistory.assetID, Equal<Current<FABookBalance.assetID>>, And<FABookHistory.bookID, Equal<Current<FABookBalance.bookID>>>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
        {
          (object) cDisplayClass240.assetBalance
        }, Array.Empty<object>()))
        {
          FABookHistory faBookHistory = PXResult<FABookHistory>.op_Implicit(pxResult3);
          FABookHist keyedHistory = new FABookHist();
          // ISSUE: reference to a compiler-generated field
          keyedHistory.AssetID = cDisplayClass240.assetBalance.AssetID;
          // ISSUE: reference to a compiler-generated field
          keyedHistory.BookID = cDisplayClass240.assetBalance.BookID;
          keyedHistory.FinPeriodID = faBookHistory.FinPeriodID;
          // ISSUE: reference to a compiler-generated field
          FAHelper.InsertFABookHist((PXGraph) this, keyedHistory, ref cDisplayClass240.assetBalance).Closed = new bool?(true);
        }
        ((PXAction) this.Save).Press();
      }
      transactionScope.Complete();
    }
  }

  public virtual void CalculateDepreciation(
    FADepreciationMethod method,
    FABookBalance assetBalance,
    DateTime? additionEndDate = null)
  {
    if (!assetBalance.Depreciate.GetValueOrDefault())
      return;
    FABook book = PXResultset<FABook>.op_Implicit(PXSelectBase<FABook, PXSelect<FABook, Where<FABook.bookID, Equal<Required<FABook.bookID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) assetBalance.BookID
    }));
    this.FABookPeriodRepository.FindFABookYearSetup(book);
    string depreciationMethod = method.DepreciationMethod;
    Decimal? nullable1 = method.DBMultiPlier;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    bool? nullable2 = method.SwitchToSL;
    bool valueOrDefault2 = nullable2.GetValueOrDefault();
    nullable2 = method.IsTableMethod;
    bool valueOrDefault3 = nullable2.GetValueOrDefault();
    nullable2 = method.YearlyAccountancy;
    bool valueOrDefault4 = nullable2.GetValueOrDefault();
    this.Params.Fill((PXGraph) this, assetBalance, book, method, additionEndDate);
    DeprCalcParameters deprCalcParameters = this.Params;
    Decimal num1;
    if (!(method.DepreciationMethod == "DB"))
    {
      nullable1 = assetBalance.AcquisitionCost;
      Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
      nullable1 = assetBalance.SalvageAmount;
      Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
      num1 = valueOrDefault5 - valueOrDefault6;
    }
    else
    {
      nullable1 = assetBalance.AcquisitionCost;
      num1 = nullable1.GetValueOrDefault();
    }
    deprCalcParameters.DepreciationBasis = num1;
    if (this.Params.DepreciationPeriodsInYear == 0 || this.Params.DepreciationBasis == 0M || string.IsNullOrEmpty(depreciationMethod) || depreciationMethod == "DB" && !valueOrDefault3 && valueOrDefault1 == 0M)
      return;
    Decimal otherDepreciation = 0M;
    Decimal lastDepreciation = 0M;
    Decimal rounding = 0M;
    int num2 = 0;
    DateTime minValue = DateTime.MinValue;
    this.previousCalculatedPeriods = 0;
    foreach (FABookPeriod deprPeriod in this.Params.DeprPeriods)
    {
      int result;
      int.TryParse(deprPeriod.FinYear, out result);
      if (num2 != result)
      {
        FADepreciationMethodLines line = (FADepreciationMethodLines) null;
        int year = result - this.Params.DepreciationStartYear + 1;
        nullable2 = method.IsTableMethod;
        if (nullable2.GetValueOrDefault())
        {
          line = PXResultset<FADepreciationMethodLines>.op_Implicit(PXSelectBase<FADepreciationMethodLines, PXSelect<FADepreciationMethodLines, Where<FADepreciationMethodLines.methodID, Equal<Required<FADepreciationMethodLines.methodID>>, And<FADepreciationMethodLines.year, Equal<Required<FADepreciationMethodLines.year>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) method.MethodID,
            (object) year
          }));
          if (line == null)
            throw new PXException("Table depreciation method '{0}' has no line for {1} year.", new object[2]
            {
              (object) method.MethodCD,
              (object) deprPeriod.FinYear
            });
        }
        this.Params.DepreciationPeriodsInYear = this.Params.GetPeriodsInYear(result);
        this.SetLinePercents(valueOrDefault3, true, valueOrDefault4, line, year, assetBalance, depreciationMethod, valueOrDefault1, valueOrDefault2, ref otherDepreciation, ref lastDepreciation, ref rounding, ref minValue);
        num2 = result;
      }
    }
  }

  protected virtual void SetLinePercents(
    bool isTableMethod,
    bool writeToAsset,
    bool yearlyAccountancy,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    string depreciationMethod,
    Decimal multiPlier,
    bool switchToSL,
    ref Decimal otherDepreciation,
    ref Decimal lastDepreciation,
    ref Decimal rounding,
    ref DateTime previousEndDate)
  {
    if (isTableMethod & yearlyAccountancy)
    {
      if (this.Params.RecoveryYears != 1 || !(this.Params.AveragingConvention == "FY"))
        this.Params.AveragingConvention = "FP";
      this.SetSLDeprOther(writeToAsset, true, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding, ref previousEndDate);
    }
    else
    {
      if (depreciationMethod == null || depreciationMethod.Length != 2)
        return;
      switch (depreciationMethod[1])
      {
        case '1':
          if (!(depreciationMethod == "N1"))
            return;
          this.SetNL1Depr(writeToAsset, line, year, assetBalance, ref rounding);
          return;
        case '2':
          if (!(depreciationMethod == "N2"))
            return;
          this.SetNL2Depr(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref rounding);
          return;
        case 'B':
          if (!(depreciationMethod == "DB"))
            return;
          this.SetDBDepr(writeToAsset, yearlyAccountancy, line, year, assetBalance, multiPlier, switchToSL, ref rounding, ref previousEndDate);
          return;
        case 'C':
          if (!(depreciationMethod == "PC"))
            return;
          this.SetAustralianPrimeCostDepr(writeToAsset, year, assetBalance, ref rounding);
          return;
        case 'D':
          switch (depreciationMethod)
          {
            case "YD":
              this.SetYDDepr(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref rounding, ref previousEndDate);
              return;
            case "RD":
              this.SetRemainingValueByPeriodLengthDepr(writeToAsset, line, year, assetBalance, ref rounding);
              return;
            case "ZD":
              this.SetNewZealandDiminishingValueDepr(writeToAsset, year, assetBalance, ref rounding);
              return;
            default:
              return;
          }
        case 'E':
          switch (depreciationMethod)
          {
            case "LE":
              this.SetNewZealandStraightLineEvenlyDepr(writeToAsset, year, assetBalance, ref rounding);
              return;
            case "DE":
              this.SetNewZealandDiminishingValueEvenlyDepr(writeToAsset, year, assetBalance, ref rounding);
              return;
            default:
              return;
          }
        case 'L':
          switch (depreciationMethod)
          {
            case "SL":
              break;
            case "ZL":
              this.SetNewZealandStraightLineDepr(writeToAsset, year, assetBalance, ref rounding);
              return;
            default:
              return;
          }
          break;
        case 'P':
          if (!(depreciationMethod == "DP"))
            return;
          this.SetDecliningBalanceByPeriodLengthDepr(writeToAsset, yearlyAccountancy, line, year, assetBalance, multiPlier, ref rounding);
          return;
        case 'V':
          switch (depreciationMethod)
          {
            case "RV":
              break;
            case "DV":
              this.SetAustralianDiminishingValueDepr(writeToAsset, year, assetBalance, ref rounding);
              return;
            default:
              return;
          }
          break;
        default:
          return;
      }
      if (this.Params.AveragingConvention == "FD")
      {
        this.SetSLDeprOther(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding, ref previousEndDate);
      }
      else
      {
        switch (this.Params.DepreciationPeriodsInYear)
        {
          case 1:
            this.SetSLDepr1(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
            break;
          case 2:
            this.SetSLDepr2(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
            break;
          case 4:
            this.SetSLDepr4(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
            break;
          case 12:
            this.SetSLDepr12(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
            break;
          default:
            this.SetSLDeprOther(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding, ref previousEndDate);
            break;
        }
      }
    }
  }

  private Decimal GetRoundingDelta(Decimal rounding)
  {
    Decimal num = (Decimal) Math.Pow(0.1, (double) this._Precision);
    return !(rounding > 0M) ? (!(rounding < 0M) || !(rounding <= -num) ? 0M : -num) : (!(rounding >= num) ? 0M : num);
  }

  private void SetFinalRounding(ref Decimal rounding)
  {
    Decimal num1 = (Decimal) Math.Pow(0.1, (double) this._Precision);
    Decimal num2 = this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation;
    if (num2 != num1 && num2 != -num1)
      return;
    Decimal num3 = rounding > 0M ? (this.Round(rounding) == num1 ? num1 : 0M) : (rounding < 0M ? (this.Round(rounding) == -num1 ? -num1 : 0M) : 0M);
    if (num3 != num2)
      num3 = num2 == num1 ? num1 : (num2 == -num1 ? -num1 : 0M);
    rounding = num3;
  }

  private void SetDepreciationPerPeriod(
    FADepreciationMethodLines line,
    bool useRounding,
    ref Decimal rounding)
  {
    if (line == null || !useRounding || !(rounding != 0M))
      return;
    this.SetFinalRounding(ref rounding);
    this.Params.AccumulatedDepreciation += this.GetRoundingDelta(rounding);
    rounding -= this.GetRoundingDelta(rounding);
  }

  private void SetBookDepreciationPerPeriod(
    FABookBalance assetBalance,
    int year,
    int period,
    Decimal value,
    bool useRounding,
    ref Decimal rounding)
  {
    if ((assetBalance != null ? (!assetBalance.DeprFromDate.HasValue ? 1 : 0) : 1) != 0 || string.IsNullOrEmpty(assetBalance.DeprFromPeriod))
      return;
    int num1 = this.Params.DepreciationStartYear + year - 1;
    if (useRounding && rounding != 0M)
    {
      this.SetFinalRounding(ref rounding);
      this.Params.AccumulatedDepreciation += this.GetRoundingDelta(rounding);
      value += this.GetRoundingDelta(rounding);
      rounding -= this.GetRoundingDelta(rounding);
    }
    value = this.Round(value);
    string str = $"{num1:0000}{period:00}";
    FABookHist faBookHist1 = (FABookHist) null;
    FABookHist faBookHist2;
    if (this.histdict.TryGetValue(str, out faBookHist2))
    {
      int? ytdSuspended1 = faBookHist2.YtdSuspended;
      int num2 = 0;
      if (ytdSuspended1.GetValueOrDefault() > num2 & ytdSuspended1.HasValue)
      {
        int counter = 0;
        while (true)
        {
          int num3 = counter;
          int? ytdSuspended2 = faBookHist2.YtdSuspended;
          int valueOrDefault = ytdSuspended2.GetValueOrDefault();
          if (num3 <= valueOrDefault & ytdSuspended2.HasValue)
          {
            FABookHist keyedHistory = new FABookHist();
            keyedHistory.AssetID = assetBalance.AssetID;
            keyedHistory.BookID = assetBalance.BookID;
            keyedHistory.FinPeriodID = this.FABookPeriodUtils.PeriodPlusPeriodsCount(str, counter, faBookHist2.BookID, faBookHist2.AssetID);
            faBookHist1 = FAHelper.InsertFABookHist((PXGraph) this, keyedHistory, ref assetBalance);
            if (faBookHist1 != null && !this.histdict.ContainsKey(faBookHist1.FinPeriodID))
            {
              FABookHist copy = PXCache<FABookHist>.CreateCopy(faBookHist1);
              copy.YtdSuspended = faBookHist2.YtdSuspended;
              copy.PtdCalculated = new Decimal?(0M);
              copy.YtdCalculated = new Decimal?(0M);
              copy.PtdDepreciated = new Decimal?(0M);
              copy.YtdDepreciated = new Decimal?(0M);
              this.histdict.Add(faBookHist1.FinPeriodID, copy);
            }
            ++counter;
          }
          else
            goto label_13;
        }
      }
    }
    FABookHist keyedHistory1 = new FABookHist();
    keyedHistory1.AssetID = assetBalance.AssetID;
    keyedHistory1.BookID = assetBalance.BookID;
    keyedHistory1.FinPeriodID = str;
    faBookHist1 = FAHelper.InsertFABookHist((PXGraph) this, keyedHistory1, ref assetBalance);
label_13:
    if (faBookHist1 == null)
      return;
    switch (this._Destination)
    {
      case DepreciationCalculation.FADestination.Tax179:
        FABookHist faBookHist3 = faBookHist1;
        Decimal? nullable1 = faBookHist3.PtdTax179Taken;
        Decimal num4 = value;
        faBookHist3.PtdTax179Taken = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num4) : new Decimal?();
        FABookHist faBookHist4 = faBookHist1;
        nullable1 = faBookHist4.YtdTax179Taken;
        Decimal num5 = value;
        faBookHist4.YtdTax179Taken = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num5) : new Decimal?();
        break;
      case DepreciationCalculation.FADestination.Bonus:
        FABookHist faBookHist5 = faBookHist1;
        Decimal? nullable2 = faBookHist5.PtdBonusTaken;
        Decimal num6 = value;
        faBookHist5.PtdBonusTaken = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num6) : new Decimal?();
        FABookHist faBookHist6 = faBookHist1;
        nullable2 = faBookHist6.YtdBonusTaken;
        Decimal num7 = value;
        faBookHist6.YtdBonusTaken = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num7) : new Decimal?();
        break;
      default:
        FABookHist faBookHist7 = faBookHist1;
        Decimal? nullable3 = faBookHist7.PtdCalculated;
        Decimal num8 = value;
        faBookHist7.PtdCalculated = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + num8) : new Decimal?();
        FABookHist faBookHist8 = faBookHist1;
        nullable3 = faBookHist8.YtdCalculated;
        Decimal num9 = value;
        faBookHist8.YtdCalculated = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + num9) : new Decimal?();
        break;
    }
  }

  protected virtual void WhereToWriteDepreciation(
    bool writeToAsset,
    FADepreciationMethodLines methodLine,
    FABookBalance assetBalance,
    int year,
    int period,
    Decimal value,
    bool useRounding,
    ref Decimal rounding)
  {
    if (!writeToAsset)
      this.SetDepreciationPerPeriod(methodLine, useRounding, ref rounding);
    else
      this.SetBookDepreciationPerPeriod(assetBalance, year, period, value, useRounding, ref rounding);
  }

  private void SetSLDepr12(
    bool writeToAsset,
    bool yearlyAccountancy,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal lastDepreciation,
    ref Decimal rounding)
  {
    Decimal yearDepreciation = this.Params.DepreciationBasis / this.Params.WholeRecoveryPeriods * (Decimal) this.Params.DepreciationPeriodsInYear;
    if (this.Params.WholeRecoveryPeriods <= 2M && this.Params.AveragingConvention == "HP" || this.Params.RecoveryYears <= 2 && this.Params.AveragingConvention == "HY" || this.Params.WholeRecoveryPeriods / 4M <= 2M && this.Params.AveragingConvention == "HQ" || !(this.Params.AveragingConvention == "HY") && !(this.Params.AveragingConvention == "FY") && !(this.Params.AveragingConvention == "HQ") && !(this.Params.AveragingConvention == "FQ") && !(this.Params.AveragingConvention == "HP") && !(this.Params.AveragingConvention == "MP") && !(this.Params.AveragingConvention == "M2") && !(this.Params.AveragingConvention == "FP") && !(this.Params.AveragingConvention == "NP") || this.Params.RecoveryYears == 1 && !(this.Params.AveragingConvention == "FP") && !(this.Params.AveragingConvention == "NP") && !(this.Params.AveragingConvention == "HP") && !(this.Params.AveragingConvention == "MP") && !(this.Params.AveragingConvention == "M2") && !(this.Params.AveragingConvention == "HQ") && !(this.Params.AveragingConvention == "FQ") && (!(this.Params.AveragingConvention == "FY") || !(this.Params.WholeRecoveryPeriods == (Decimal) this.Params.DepreciationPeriodsInYear)))
      return;
    if (year == 1 && year < this.Params.RecoveryYears)
    {
      otherDepreciation = yearDepreciation / (Decimal) this.Params.DepreciationPeriodsInYear;
      string averagingConvention = this.Params.AveragingConvention;
      if (averagingConvention == null || averagingConvention.Length != 2)
        return;
      switch (averagingConvention[0])
      {
        case 'F':
          switch (averagingConvention)
          {
            case "FP":
              break;
            case "FQ":
              this.SetSLDeprFullQuarterFirstYearNotEqualLastYear(writeToAsset, line, year, assetBalance, yearDepreciation, ref otherDepreciation, ref lastDepreciation, ref rounding);
              return;
            case "FY":
              this.SetSLDeprFullYearFirstYearNotEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref rounding);
              return;
            default:
              return;
          }
          break;
        case 'H':
          switch (averagingConvention)
          {
            case "HP":
              this.SetSLDeprHalfPeriodFirstYearNotEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
              return;
            case "HQ":
              this.SetSLDeprHalfQuarterFirstYearNotEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
              return;
            case "HY":
              this.SetSLDeprHalfYearFirstYearNotEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
              return;
            default:
              return;
          }
        case 'M':
          if (!(averagingConvention == "MP") && !(averagingConvention == "M2"))
            return;
          this.SetSLDeprModifiedPeriodFirstYearNotEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
          return;
        case 'N':
          if (!(averagingConvention == "NP"))
            return;
          break;
        default:
          return;
      }
      this.SetSLDeprFullPeriodFirstYearNotEqualLastYear(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref rounding);
    }
    else if (year == 1 && year == this.Params.RecoveryYears)
    {
      otherDepreciation = this.Params.DepreciationBasis / this.Params.WholeRecoveryPeriods;
      switch (this.Params.AveragingConvention)
      {
        case "HP":
          this.SetSLDeprHalfPeriodFirstYearEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref rounding);
          break;
        case "MP":
        case "M2":
          this.SetSLDeprModifiedPeriodFirstYearEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
          break;
        case "FP":
        case "NP":
          this.SetSLDeprFullPeriodFirstYearEqualLastYear(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref rounding);
          break;
        case "FY":
          this.SetSLDeprFullYearFirstYearEqualLastYear(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref rounding);
          break;
      }
    }
    else if (year == this.Params.RecoveryYears)
    {
      string averagingConvention = this.Params.AveragingConvention;
      if (averagingConvention == null || averagingConvention.Length != 2)
        return;
      switch (averagingConvention[0])
      {
        case 'F':
          switch (averagingConvention)
          {
            case "FP":
              goto label_41;
            case "FQ":
              this.SetSLDeprFullQuarterLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref rounding);
              return;
            case "FY":
              this.SetSLDeprFullYearLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref rounding);
              return;
            default:
              return;
          }
        case 'H':
          switch (averagingConvention)
          {
            case "HP":
              break;
            case "HQ":
              this.SetSLDeprHalfQuarterLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
              return;
            case "HY":
              this.SetSLDeprHalfYearLastYear(writeToAsset, line, year, assetBalance, ref lastDepreciation, ref rounding);
              return;
            default:
              return;
          }
          break;
        case 'M':
          if (!(averagingConvention == "MP") && !(averagingConvention == "M2"))
            return;
          break;
        case 'N':
          if (!(averagingConvention == "NP"))
            return;
          goto label_41;
        default:
          return;
      }
      this.SetSLDeprHalfPeriodLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
      return;
label_41:
      this.SetSLDeprFullPeriodLastYear(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref rounding);
    }
    else
      this.SetSLDeprOtherYears(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref rounding);
  }

  private void SetSLDepr4(
    bool writeToAsset,
    bool yearlyAccountancy,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal lastDepreciation,
    ref Decimal rounding)
  {
    if (this.Params.WholeRecoveryPeriods <= 2M && (this.Params.AveragingConvention == "HP" || this.Params.AveragingConvention == "HQ") || this.Params.RecoveryYears <= 2 && this.Params.AveragingConvention == "HY" || !(this.Params.AveragingConvention == "HY") && !(this.Params.AveragingConvention == "FY") && !(this.Params.AveragingConvention == "HP") && !(this.Params.AveragingConvention == "MP") && !(this.Params.AveragingConvention == "M2") && !(this.Params.AveragingConvention == "FP") && !(this.Params.AveragingConvention == "NP") && !(this.Params.AveragingConvention == "HQ") && !(this.Params.AveragingConvention == "FQ") || this.Params.RecoveryYears == 1 && !(this.Params.AveragingConvention == "FP") && !(this.Params.AveragingConvention == "NP") && !(this.Params.AveragingConvention == "HP") && !(this.Params.AveragingConvention == "MP") && !(this.Params.AveragingConvention == "M2") && !(this.Params.AveragingConvention == "HQ") && !(this.Params.AveragingConvention == "FQ") && (!(this.Params.AveragingConvention == "FY") || !(this.Params.WholeRecoveryPeriods == (Decimal) this.Params.DepreciationPeriodsInYear)))
      return;
    if (year == 1 && year < this.Params.RecoveryYears)
    {
      otherDepreciation = this.Params.DepreciationBasis / this.Params.WholeRecoveryPeriods;
      string averagingConvention = this.Params.AveragingConvention;
      if (averagingConvention == null || averagingConvention.Length != 2)
        return;
      switch (averagingConvention[0])
      {
        case 'F':
          switch (averagingConvention)
          {
            case "FP":
            case "FQ":
              break;
            case "FY":
              this.SetSLDeprFullYearFirstYearNotEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref rounding);
              return;
            default:
              return;
          }
          break;
        case 'H':
          switch (averagingConvention)
          {
            case "HP":
            case "HQ":
              this.SetSLDeprHalfPeriodFirstYearNotEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
              return;
            case "HY":
              this.SetSLDeprHalfYearFirstYearNotEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
              return;
            default:
              return;
          }
        case 'M':
          if (!(averagingConvention == "MP") && !(averagingConvention == "M2"))
            return;
          this.SetSLDeprModifiedPeriodFirstYearNotEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
          return;
        case 'N':
          if (!(averagingConvention == "NP"))
            return;
          break;
        default:
          return;
      }
      this.SetSLDeprFullPeriodFirstYearNotEqualLastYear(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref rounding);
    }
    else if (year == 1 && year == this.Params.RecoveryYears)
    {
      otherDepreciation = this.Params.DepreciationBasis / this.Params.WholeRecoveryPeriods;
      string averagingConvention = this.Params.AveragingConvention;
      if (averagingConvention == null || averagingConvention.Length != 2)
        return;
      switch (averagingConvention[1])
      {
        case '2':
          if (!(averagingConvention == "M2"))
            return;
          goto label_32;
        case 'P':
          switch (averagingConvention)
          {
            case "HP":
              break;
            case "MP":
              goto label_32;
            case "FP":
            case "NP":
              goto label_33;
            default:
              return;
          }
          break;
        case 'Q':
          switch (averagingConvention)
          {
            case "HQ":
              break;
            case "FQ":
              goto label_33;
            default:
              return;
          }
          break;
        case 'Y':
          if (!(averagingConvention == "FY"))
            return;
          goto label_33;
        default:
          return;
      }
      this.SetSLDeprHalfPeriodFirstYearEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref rounding);
      return;
label_32:
      this.SetSLDeprModifiedPeriodFirstYearEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
      return;
label_33:
      this.SetSLDeprFullPeriodFirstYearEqualLastYear(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref rounding);
    }
    else if (year == this.Params.RecoveryYears)
    {
      string averagingConvention = this.Params.AveragingConvention;
      if (averagingConvention == null || averagingConvention.Length != 2)
        return;
      switch (averagingConvention[0])
      {
        case 'F':
          switch (averagingConvention)
          {
            case "FP":
            case "FQ":
              goto label_47;
            case "FY":
              this.SetSLDeprFullYearLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref rounding);
              return;
            default:
              return;
          }
        case 'H':
          switch (averagingConvention)
          {
            case "HP":
            case "HQ":
              break;
            case "HY":
              this.SetSLDeprHalfYearLastYear(writeToAsset, line, year, assetBalance, ref lastDepreciation, ref rounding);
              return;
            default:
              return;
          }
          break;
        case 'M':
          if (!(averagingConvention == "MP") && !(averagingConvention == "M2"))
            return;
          break;
        case 'N':
          if (!(averagingConvention == "NP"))
            return;
          goto label_47;
        default:
          return;
      }
      this.SetSLDeprHalfPeriodLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
      return;
label_47:
      this.SetSLDeprFullPeriodLastYear(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref rounding);
    }
    else
      this.SetSLDeprOtherYears(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref rounding);
  }

  private void SetSLDepr2(
    bool writeToAsset,
    bool yearlyAccountancy,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal lastDepreciation,
    ref Decimal rounding)
  {
    Decimal wholeRecoveryPeriods = this.Params.WholeRecoveryPeriods;
    if (wholeRecoveryPeriods <= 2M && (this.Params.AveragingConvention == "HP" || this.Params.AveragingConvention == "FQ") || this.Params.RecoveryYears <= 2 && this.Params.AveragingConvention == "HY" || !(this.Params.AveragingConvention == "HY") && !(this.Params.AveragingConvention == "FY") && !(this.Params.AveragingConvention == "HP") && !(this.Params.AveragingConvention == "MP") && !(this.Params.AveragingConvention == "M2") && !(this.Params.AveragingConvention == "FQ") && !(this.Params.AveragingConvention == "FP") && !(this.Params.AveragingConvention == "NP") || this.Params.RecoveryYears == 1 && !(this.Params.AveragingConvention == "HP") && !(this.Params.AveragingConvention == "MP") && !(this.Params.AveragingConvention == "M2") && !(this.Params.AveragingConvention == "FQ") && !(this.Params.AveragingConvention == "FP") && !(this.Params.AveragingConvention == "NP") && !(this.Params.AveragingConvention == "HY") && (!(this.Params.AveragingConvention == "FY") || !(wholeRecoveryPeriods == (Decimal) this.Params.DepreciationPeriodsInYear)))
      return;
    if (year == 1 && year < this.Params.RecoveryYears)
    {
      otherDepreciation = this.Params.DepreciationBasis / wholeRecoveryPeriods;
      string averagingConvention = this.Params.AveragingConvention;
      if (averagingConvention == null || averagingConvention.Length != 2)
        return;
      switch (averagingConvention[1])
      {
        case '2':
          if (!(averagingConvention == "M2"))
            return;
          goto label_15;
        case 'P':
          switch (averagingConvention)
          {
            case "HP":
              break;
            case "MP":
              goto label_15;
            case "FP":
            case "NP":
              goto label_16;
            default:
              return;
          }
          break;
        case 'Q':
          if (!(averagingConvention == "FQ"))
            return;
          break;
        case 'Y':
          switch (averagingConvention)
          {
            case "HY":
              goto label_16;
            case "FY":
              this.SetSLDeprFullYearFirstYearNotEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref rounding);
              return;
            default:
              return;
          }
        default:
          return;
      }
      this.SetSLDeprHalfPeriodFirstYearNotEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
      return;
label_15:
      this.SetSLDeprModifiedPeriodFirstYearNotEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
      return;
label_16:
      this.SetSLDeprFullPeriodFirstYearNotEqualLastYear(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref rounding);
    }
    else if (year == 1 && year == this.Params.RecoveryYears)
    {
      otherDepreciation = this.Params.DepreciationBasis / wholeRecoveryPeriods;
      string averagingConvention = this.Params.AveragingConvention;
      if (averagingConvention == null || averagingConvention.Length != 2)
        return;
      switch (averagingConvention[1])
      {
        case '2':
          if (!(averagingConvention == "M2"))
            return;
          goto label_31;
        case 'P':
          switch (averagingConvention)
          {
            case "HP":
              break;
            case "MP":
              goto label_31;
            case "FP":
            case "NP":
              goto label_32;
            default:
              return;
          }
          break;
        case 'Q':
          if (!(averagingConvention == "FQ"))
            return;
          break;
        case 'Y':
          if (!(averagingConvention == "HY") && !(averagingConvention == "FY"))
            return;
          goto label_32;
        default:
          return;
      }
      this.SetSLDeprHalfPeriodFirstYearEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref rounding);
      return;
label_31:
      this.SetSLDeprModifiedPeriodFirstYearEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
      return;
label_32:
      this.SetSLDeprFullPeriodFirstYearEqualLastYear(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref rounding);
    }
    else if (year == this.Params.RecoveryYears)
    {
      string averagingConvention = this.Params.AveragingConvention;
      if (averagingConvention == null || averagingConvention.Length != 2)
        return;
      switch (averagingConvention[1])
      {
        case '2':
          if (!(averagingConvention == "M2"))
            return;
          break;
        case 'P':
          switch (averagingConvention)
          {
            case "HP":
            case "MP":
              break;
            case "FP":
            case "NP":
              goto label_46;
            default:
              return;
          }
          break;
        case 'Q':
          if (!(averagingConvention == "FQ"))
            return;
          break;
        case 'Y':
          switch (averagingConvention)
          {
            case "HY":
              goto label_46;
            case "FY":
              this.SetSLDeprFullYearLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref rounding);
              return;
            default:
              return;
          }
        default:
          return;
      }
      this.SetSLDeprHalfPeriodLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
      return;
label_46:
      this.SetSLDeprFullPeriodLastYear(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref rounding);
    }
    else
      this.SetSLDeprOtherYears(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref rounding);
  }

  private void SetSLDepr1(
    bool writeToAsset,
    bool yearlyAccountancy,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal lastDepreciation,
    ref Decimal rounding)
  {
    this.Params.DepreciationStartPeriod = 1;
    this.Params.DepreciationStopPeriod = 1;
    this.Params.RecoveryEndPeriod = 1;
    if (this.Params.DepreciationYears <= 2 && (this.Params.AveragingConvention == "HY" || this.Params.AveragingConvention == "HP") || !(this.Params.AveragingConvention == "HY") && !(this.Params.AveragingConvention == "FY") && !(this.Params.AveragingConvention == "HP") && !(this.Params.AveragingConvention == "MP") && !(this.Params.AveragingConvention == "M2") && !(this.Params.AveragingConvention == "FP") && !(this.Params.AveragingConvention == "NP") || this.Params.RecoveryYears == 1 && !(this.Params.AveragingConvention == "FP") && !(this.Params.AveragingConvention == "NP") && !(this.Params.AveragingConvention == "FY"))
      return;
    if (year == 1 && year < this.Params.RecoveryYears)
    {
      otherDepreciation = this.Params.DepreciationBasis / (Decimal) this.Params.DepreciationYears;
      string averagingConvention = this.Params.AveragingConvention;
      if (averagingConvention == null || averagingConvention.Length != 2)
        return;
      switch (averagingConvention[0])
      {
        case 'F':
          if (!(averagingConvention == "FY") && !(averagingConvention == "FP"))
            return;
          break;
        case 'H':
          if (!(averagingConvention == "HY") && !(averagingConvention == "HP"))
            return;
          this.SetSLDeprHalfPeriodFirstYearNotEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
          return;
        case 'M':
          if (!(averagingConvention == "MP") && !(averagingConvention == "M2"))
            return;
          this.SetSLDeprModifiedPeriodFirstYearNotEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
          return;
        case 'N':
          if (!(averagingConvention == "NP"))
            return;
          break;
        default:
          return;
      }
      this.SetSLDeprFullPeriodFirstYearNotEqualLastYear(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref rounding);
    }
    else if (year == 1 && year == this.Params.RecoveryYears)
    {
      otherDepreciation = this.Params.DepreciationBasis / (Decimal) this.Params.DepreciationYears;
      string averagingConvention = this.Params.AveragingConvention;
      if (averagingConvention == null || averagingConvention.Length != 2)
        return;
      switch (averagingConvention[0])
      {
        case 'F':
          if (!(averagingConvention == "FY") && !(averagingConvention == "FP"))
            return;
          break;
        case 'H':
          if (!(averagingConvention == "HP") && !(averagingConvention == "HY"))
            return;
          this.SetSLDeprHalfPeriodFirstYearEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref rounding);
          return;
        case 'M':
          if (!(averagingConvention == "MP") && !(averagingConvention == "M2"))
            return;
          this.SetSLDeprModifiedPeriodFirstYearEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
          return;
        case 'N':
          if (!(averagingConvention == "NP"))
            return;
          break;
        default:
          return;
      }
      this.SetSLDeprFullPeriodFirstYearEqualLastYear(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref rounding);
    }
    else if (year == this.Params.RecoveryYears)
    {
      string averagingConvention = this.Params.AveragingConvention;
      if (averagingConvention == null || averagingConvention.Length != 2)
        return;
      switch (averagingConvention[0])
      {
        case 'F':
          if (!(averagingConvention == "FY") && !(averagingConvention == "FP"))
            return;
          goto label_45;
        case 'H':
          if (!(averagingConvention == "HY") && !(averagingConvention == "HP"))
            return;
          break;
        case 'M':
          if (!(averagingConvention == "MP") && !(averagingConvention == "M2"))
            return;
          break;
        case 'N':
          if (!(averagingConvention == "NP"))
            return;
          goto label_45;
        default:
          return;
      }
      this.SetSLDeprHalfPeriodLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
      return;
label_45:
      this.SetSLDeprFullPeriodLastYear(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref rounding);
    }
    else
      this.SetSLDeprOtherYears(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref rounding);
  }

  private void SetSLDeprOther(
    bool writeToAsset,
    bool yearlyAccountancy,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal lastDepreciation,
    ref Decimal rounding,
    ref DateTime previousEndDate)
  {
    if (this.Params.WholeRecoveryPeriods <= 2M && this.Params.AveragingConvention == "HP" || this.Params.RecoveryYears <= 2 && this.Params.AveragingConvention == "HY" || !(this.Params.AveragingConvention == "HY") && !(this.Params.AveragingConvention == "FY") && !(this.Params.AveragingConvention == "HP") && !(this.Params.AveragingConvention == "MP") && !(this.Params.AveragingConvention == "M2") && !(this.Params.AveragingConvention == "FP") && !(this.Params.AveragingConvention == "NP") && (!(this.Params.AveragingConvention == "FD") || !this.Params.DepreciationStopDate.HasValue || this.Params.WholeRecoveryDays == 0 || this.Params.DepreciationStartYear == 0) || this.Params.RecoveryYears == 1 && !(this.Params.AveragingConvention == "HP") && !(this.Params.AveragingConvention == "MP") && !(this.Params.AveragingConvention == "M2") && !(this.Params.AveragingConvention == "FP") && !(this.Params.AveragingConvention == "NP") && (!(this.Params.AveragingConvention == "FD") || !this.Params.DepreciationStopDate.HasValue || this.Params.WholeRecoveryDays == 0 || this.Params.DepreciationStartYear == 0) && (!(this.Params.AveragingConvention == "FY") || !(this.Params.WholeRecoveryPeriods == (Decimal) this.Params.DepreciationPeriodsInYear)))
      return;
    if (this.Params.AveragingConvention == "FD")
    {
      Decimal num1 = this.Params.DepreciationBasis / this.Params.WholeRecoveryPeriods;
      int currYear = this.Params.DepreciationStartYear + year - 1;
      int num2;
      int num3;
      if (year == 1 && year < this.Params.RecoveryYears)
      {
        num2 = this.Params.DepreciationStartPeriod;
        num3 = this.Params.DepreciationPeriodsInYear;
      }
      else if (year == 1 && year == this.Params.RecoveryYears)
      {
        num2 = this.Params.DepreciationStartPeriod;
        num3 = this.Params.DepreciationStopPeriod;
      }
      else if (year == this.Params.RecoveryYears)
      {
        num2 = 1;
        num3 = this.Params.DepreciationStopPeriod;
      }
      else
      {
        num2 = 1;
        num3 = this.Params.DepreciationPeriodsInYear;
      }
      for (int index = num2; index <= num3; ++index)
      {
        if (index == this.Params.DepreciationStartPeriod && index == this.Params.DepreciationStopPeriod && currYear == this.Params.DepreciationStartYear && currYear == int.Parse(this.Params.DepreciationStopBookPeriod.FinYear))
        {
          previousEndDate = this.Params.DepreciationStopDate.Value;
          int periodLength = this.Params.GetPeriodLength(currYear, index);
          int daysOnPeriod = this.Params.GetDaysOnPeriod(this.Params.DepreciationStartDate, this.Params.DepreciationStopDate, currYear, index, ref previousEndDate);
          otherDepreciation = num1 * (Decimal) daysOnPeriod / (Decimal) periodLength;
        }
        else if (index == this.Params.DepreciationStartPeriod && currYear == this.Params.DepreciationStartYear)
        {
          int periodLength = this.Params.GetPeriodLength(currYear, index);
          int daysOnPeriod = this.Params.GetDaysOnPeriod(this.Params.DepreciationStartDate, this.Params.DepreciationStopDate, currYear, index, ref previousEndDate);
          otherDepreciation = num1 * (Decimal) daysOnPeriod / (Decimal) periodLength;
        }
        else if (index == this.Params.DepreciationStopPeriod && currYear == int.Parse(this.Params.DepreciationStopBookPeriod.FinYear))
        {
          if (this.Params.DepreciationStopDate.HasValue)
          {
            int periodLength = this.Params.GetPeriodLength(currYear, index);
            int daysOnPeriod = this.Params.GetDaysOnPeriod(this.Params.DepreciationStartDate, this.Params.DepreciationStopDate, currYear, index, ref previousEndDate);
            otherDepreciation = num1 * (Decimal) daysOnPeriod / (Decimal) periodLength;
          }
          else
            otherDepreciation = this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation;
        }
        else
          otherDepreciation = num1;
        this.Params.AccumulatedDepreciation += this.Round(otherDepreciation);
        rounding += otherDepreciation - this.Round(otherDepreciation);
        this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, index, otherDepreciation, true, ref rounding);
      }
    }
    else if (year == 1 && year < this.Params.RecoveryYears)
    {
      otherDepreciation = this.Params.DepreciationBasis / this.Params.WholeRecoveryPeriods;
      string averagingConvention = this.Params.AveragingConvention;
      if (averagingConvention == null || averagingConvention.Length != 2)
        return;
      switch (averagingConvention[0])
      {
        case 'F':
          switch (averagingConvention)
          {
            case "FP":
              break;
            case "FY":
              this.SetSLDeprFullYearFirstYearNotEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref rounding);
              return;
            default:
              return;
          }
          break;
        case 'H':
          switch (averagingConvention)
          {
            case "HP":
              this.SetSLDeprHalfPeriodFirstYearNotEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
              return;
            case "HY":
              this.SetSLDeprHalfYearFirstYearNotEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
              return;
            default:
              return;
          }
        case 'M':
          if (!(averagingConvention == "MP") && !(averagingConvention == "M2"))
            return;
          this.SetSLDeprModifiedPeriodFirstYearNotEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
          return;
        case 'N':
          if (!(averagingConvention == "NP"))
            return;
          break;
        default:
          return;
      }
      this.SetSLDeprFullPeriodFirstYearNotEqualLastYear(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref rounding);
    }
    else if (year == 1 && year == this.Params.RecoveryYears)
    {
      otherDepreciation = this.Params.DepreciationBasis / this.Params.WholeRecoveryPeriods;
      switch (this.Params.AveragingConvention)
      {
        case "HP":
          this.SetSLDeprHalfPeriodFirstYearEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref rounding);
          break;
        case "MP":
        case "M2":
          this.SetSLDeprModifiedPeriodFirstYearEqualLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
          break;
        case "FP":
        case "NP":
          this.SetSLDeprFullPeriodFirstYearEqualLastYear(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref rounding);
          break;
        case "FY":
          this.SetSLDeprFullYearFirstYearEqualLastYear(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref rounding);
          break;
      }
    }
    else if (year == this.Params.RecoveryYears)
    {
      string averagingConvention = this.Params.AveragingConvention;
      if (averagingConvention == null || averagingConvention.Length != 2)
        return;
      switch (averagingConvention[0])
      {
        case 'F':
          switch (averagingConvention)
          {
            case "FP":
              goto label_60;
            case "FY":
              this.SetSLDeprFullYearLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref rounding);
              return;
            default:
              return;
          }
        case 'H':
          switch (averagingConvention)
          {
            case "HP":
              break;
            case "HY":
              this.SetSLDeprHalfYearLastYear(writeToAsset, line, year, assetBalance, ref lastDepreciation, ref rounding);
              return;
            default:
              return;
          }
          break;
        case 'M':
          if (!(averagingConvention == "MP") && !(averagingConvention == "M2"))
            return;
          break;
        case 'N':
          if (!(averagingConvention == "NP"))
            return;
          goto label_60;
        default:
          return;
      }
      this.SetSLDeprHalfPeriodLastYear(writeToAsset, line, year, assetBalance, ref otherDepreciation, ref lastDepreciation, ref rounding);
      return;
label_60:
      this.SetSLDeprFullPeriodLastYear(writeToAsset, yearlyAccountancy, line, year, assetBalance, ref otherDepreciation, ref rounding);
    }
    else
    {
      bool flag = yearlyAccountancy && line != null && this.Params.AveragingConvention == "FP";
      if (flag)
        otherDepreciation = line.RatioPerYear.GetValueOrDefault() * this.Params.DepreciationBasis / (Decimal) this.Params.DepreciationPeriodsInYear;
      for (int period = 1; period <= this.Params.DepreciationPeriodsInYear; ++period)
      {
        if (flag)
        {
          this.Params.AccumulatedDepreciation += this.Round(otherDepreciation);
          rounding += otherDepreciation - this.Round(otherDepreciation);
        }
        this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, period, otherDepreciation, true, ref rounding);
      }
    }
  }

  private void SetDBDepr(
    bool writeToAsset,
    bool yearlyAccountancy,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    Decimal multiPlier,
    bool switchToSL,
    ref Decimal rounding,
    ref DateTime previousEndDate)
  {
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    Decimal num3 = 1M;
    int num4 = 0;
    if (yearlyAccountancy && (Decimal) this.Params.RecoveryYears > this.Params.UsefulLife)
    {
      switch (this.Params.AveragingConvention)
      {
        case "HY":
          num3 = 0.5M;
          break;
        case "HQ":
          num3 = (9M - 2M * (Decimal) ((assetBalance.DeprFromDate.Value.Month + 2) / 3)) / 8M;
          break;
      }
    }
    if (yearlyAccountancy)
    {
      num1 = (this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation) * multiPlier / this.Params.UsefulLife;
      num2 = (this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation) / (year == 1 ? this.Params.UsefulLife : (Decimal) (this.Params.RecoveryYears - year) + (Decimal) this.Params.RecoveryEndPeriod / (Decimal) this.Params.DepreciationPeriodsInYear);
    }
    int num5;
    int num6;
    if (year == 1 && year < this.Params.RecoveryYears)
    {
      num1 *= num3;
      num2 *= num3;
      num5 = this.Params.DepreciationStartPeriod;
      num6 = this.Params.DepreciationPeriodsInYear;
    }
    else if (year == 1 && year == this.Params.RecoveryYears)
    {
      num5 = this.Params.DepreciationStartPeriod;
      num6 = this.Params.RecoveryEndPeriod;
    }
    else if (year == this.Params.RecoveryYears)
    {
      if (num3 < 1.0M)
        num2 *= 1M - num3;
      num5 = 1;
      num6 = this.Params.RecoveryEndPeriod;
    }
    else
    {
      num5 = 1;
      num6 = this.Params.DepreciationPeriodsInYear;
    }
    if (year == this.Params.DepreciationYears)
      num6 = this.Params.DepreciationStopPeriod;
    if (yearlyAccountancy)
    {
      if (this.Params.AveragingConvention == "FD")
      {
        num4 = 0;
        DateTime dateTime = previousEndDate;
        for (int currPeriod = num5; currPeriod <= num6; ++currPeriod)
          num4 += this.Params.GetDaysOnPeriod(this.Params.DepreciationStartDate, new DateTime?(this.Params.RecoveryEndDate), this.Params.DepreciationStartYear + year - 1, currPeriod, ref previousEndDate);
        previousEndDate = dateTime;
      }
      Decimal accumulatedDepreciation = this.Params.AccumulatedDepreciation;
      for (int index = num5; index <= num6 && !(this.Params.DepreciationBasis == this.Params.AccumulatedDepreciation); ++index)
      {
        Decimal num7;
        if (this.Params.AveragingConvention == "FD")
        {
          int daysOnPeriod = this.Params.GetDaysOnPeriod(this.Params.DepreciationStartDate, this.Params.DepreciationStopDate, this.Params.DepreciationStartYear + year - 1, index, ref previousEndDate);
          num7 = num1 * (Decimal) daysOnPeriod / (Decimal) num4;
        }
        else
          num7 = !(this.Params.AveragingConvention == "FP") ? num1 / (Decimal) (num6 - num5 + 1) : num1 / (Decimal) this.Params.DepreciationPeriodsInYear;
        if (switchToSL)
        {
          Decimal num8 = multiPlier / this.Params.UsefulLife;
          if ((Decimal.Round(this.Params.DepreciationBasis, 4) == Decimal.Round(accumulatedDepreciation, 4) ? 1M : num2 / (this.Params.DepreciationBasis - accumulatedDepreciation)) > num8)
          {
            Decimal num9 = year == 1 ? this.Params.UsefulLife * (Decimal) this.Params.DepreciationPeriodsInYear : (Decimal) ((this.Params.RecoveryYears - year) * this.Params.DepreciationPeriodsInYear + this.Params.RecoveryEndPeriod);
            Decimal num10 = (this.Params.DepreciationBasis - accumulatedDepreciation) / num9;
            num7 = Math.Abs(num10) > Math.Abs(this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation) ? this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation : num10;
          }
        }
        this.Params.AccumulatedDepreciation += this.Round(num7);
        if (this.Round(num7) != this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation)
          rounding += num7 - this.Round(num7);
        this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, index, num7, true, ref rounding);
      }
    }
    else
    {
      for (int period = num5; period <= num6 && !(this.Params.DepreciationBasis == this.Params.AccumulatedDepreciation) && !(this.Params.WholeRecoveryPeriods == (Decimal) this.previousCalculatedPeriods); ++period)
      {
        Decimal num11 = (this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation) * multiPlier / this.Params.WholeRecoveryPeriods;
        Decimal num12 = (this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation) / (this.Params.WholeRecoveryPeriods - (Decimal) this.previousCalculatedPeriods++);
        Decimal num13 = switchToSL ? (num12 > num11 ? (num12 > this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation ? this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation : num12) : num11) : num11;
        this.Params.AccumulatedDepreciation += this.Round(num13);
        if (this.Round(num13) != this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation)
          rounding += num13 - this.Round(num13);
        this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, period, num13, true, ref rounding);
      }
    }
  }

  private void SetNL1Depr(
    bool writeToAsset,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal rounding)
  {
    int num1;
    int num2;
    if (year == 1 && year < this.Params.RecoveryYears)
    {
      num1 = this.Params.DepreciationStartPeriod;
      num2 = this.Params.DepreciationPeriodsInYear;
    }
    else if (year == 1 && year == this.Params.RecoveryYears)
    {
      num1 = this.Params.DepreciationStartPeriod;
      num2 = this.Params.RecoveryEndPeriod;
    }
    else if (year == this.Params.RecoveryYears)
    {
      num1 = 1;
      num2 = this.Params.RecoveryEndPeriod;
    }
    else
    {
      num1 = 1;
      num2 = this.Params.DepreciationPeriodsInYear;
    }
    int num3 = num2;
    if (year == this.Params.DepreciationYears)
      num3 = this.Params.DepreciationStopPeriod;
    Decimal num4 = this.Params.DepreciationBasis + assetBalance.SalvageAmount.GetValueOrDefault();
    for (int period = num1; period <= num3 && !(num4 == this.Params.AccumulatedDepreciation); ++period)
    {
      Decimal num5 = (Decimal) ((double) (num4 - this.Params.AccumulatedDepreciation) * (1.0 - Math.Pow((double) assetBalance.SalvageAmount.GetValueOrDefault() / (double) num4, (double) (1M / this.Params.WholeRecoveryPeriods))));
      this.Params.AccumulatedDepreciation += this.Round(num5);
      if (this.Round(num5) != num4 - this.Params.AccumulatedDepreciation)
        rounding += num5 - this.Round(num5);
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, period, num5, true, ref rounding);
    }
  }

  private void SetNL2Depr(
    bool writeToAsset,
    bool yearlyAccountancy,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal rounding)
  {
    int num1;
    int num2;
    if (year == 1 && year < this.Params.RecoveryYears)
    {
      num1 = this.Params.DepreciationStartPeriod;
      num2 = this.Params.DepreciationPeriodsInYear;
    }
    else if (year == 1 && year == this.Params.RecoveryYears)
    {
      num1 = this.Params.DepreciationStartPeriod;
      num2 = this.Params.RecoveryEndPeriod;
    }
    else if (year == this.Params.RecoveryYears)
    {
      num1 = 1;
      num2 = this.Params.RecoveryEndPeriod;
    }
    else
    {
      num1 = 1;
      num2 = this.Params.DepreciationPeriodsInYear;
    }
    int num3 = num2;
    if (year == this.Params.DepreciationYears)
      num3 = this.Params.DepreciationStopPeriod;
    Decimal num4 = (this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation) * this.Params.DepreciationMethod.PercentPerYear.GetValueOrDefault() * 0.01M;
    for (int period = num1; period <= num3 && !(this.Params.DepreciationBasis == this.Params.AccumulatedDepreciation); ++period)
    {
      Decimal num5 = yearlyAccountancy ? num4 / (Decimal) this.Params.DepreciationPeriodsInYear : (Decimal) ((double) (this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation) * (1.0 - Math.Pow(1.0 - (double) this.Params.DepreciationMethod.PercentPerYear.GetValueOrDefault() * 0.01, 1.0 / (double) this.Params.DepreciationPeriodsInYear)));
      this.Params.AccumulatedDepreciation += this.Round(num5);
      if (this.Round(num5) != this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation)
        rounding += num5 - this.Round(num5);
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, period, num5, true, ref rounding);
    }
  }

  private void SetRemainingValueByPeriodLengthDepr(
    bool writeToAsset,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal rounding)
  {
    int num1;
    int num2;
    if (year == 1 && year < this.Params.RecoveryYears)
    {
      num1 = this.Params.DepreciationStartPeriod;
      num2 = this.Params.DepreciationPeriodsInYear;
    }
    else if (year == 1 && year == this.Params.RecoveryYears)
    {
      num1 = this.Params.DepreciationStartPeriod;
      num2 = this.Params.RecoveryEndPeriod;
    }
    else if (year == this.Params.RecoveryYears)
    {
      num1 = 1;
      num2 = this.Params.RecoveryEndPeriod;
    }
    else
    {
      num1 = 1;
      num2 = this.Params.DepreciationPeriodsInYear;
    }
    int num3 = num2;
    if (year == this.Params.DepreciationYears)
      num3 = this.Params.DepreciationStopPeriod;
    for (int index = num1; index <= num3; ++index)
    {
      Decimal num4 = this.Params.DepreciationBasis * (Decimal) this.Params.GetFinPeriodLengthInDays(year, index) / (Decimal) this.Params.WholeRecoveryDays;
      if (year == this.Params.RecoveryYears && index == num3 && Math.Abs(this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation) < Math.Abs(Math.Truncate(num4 * 100M) / 100M))
        num4 = this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation;
      Decimal num5 = this.Round(num4);
      this.Params.AccumulatedDepreciation += num5;
      if (num5 != this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation)
        rounding += num4 - num5;
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, index, num5, true, ref rounding);
    }
  }

  private void SetDecliningBalanceByPeriodLengthDepr(
    bool writeToAsset,
    bool yearlyAccountancy,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    Decimal multiPlier,
    ref Decimal rounding)
  {
    int num1;
    int num2;
    if (year == 1 && year < this.Params.RecoveryYears)
    {
      num1 = this.Params.DepreciationStartPeriod;
      num2 = this.Params.DepreciationPeriodsInYear;
    }
    else if (year == 1 && year == this.Params.RecoveryYears)
    {
      num1 = this.Params.DepreciationStartPeriod;
      num2 = this.Params.RecoveryEndPeriod;
    }
    else if (year == this.Params.RecoveryYears)
    {
      num1 = 1;
      num2 = this.Params.RecoveryEndPeriod;
    }
    else
    {
      num1 = 1;
      num2 = this.Params.DepreciationPeriodsInYear;
    }
    int num3 = num2;
    if (year == this.Params.DepreciationYears)
      num3 = this.Params.DepreciationStopPeriod;
    Decimal num4 = 0M;
    if (yearlyAccountancy)
      num4 = this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation;
    for (int index = num1; index <= num3; ++index)
    {
      Decimal num5 = 1M / this.Params.UsefulLife;
      int periodLengthInDays = this.Params.GetFinPeriodLengthInDays(year, index);
      if (!yearlyAccountancy)
        num4 = this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation;
      Decimal num6 = num4 * num5 * multiPlier * (Decimal) periodLengthInDays / 365M;
      Decimal num7 = this.Round(num6);
      if (num7 != this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation)
        rounding += num6 - num7;
      this.Params.AccumulatedDepreciation += num7;
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, index, num7, true, ref rounding);
    }
  }

  private void SetAustralianPrimeCostDepr(
    bool writeToAsset,
    int year,
    FABookBalance assetBalance,
    ref Decimal rounding)
  {
    int num1;
    int num2;
    if (year == 1 && year < this.Params.RecoveryYears)
    {
      num1 = this.Params.DepreciationStartPeriod;
      num2 = this.Params.DepreciationPeriodsInYear;
    }
    else if (year == 1 && year == this.Params.RecoveryYears)
    {
      num1 = this.Params.DepreciationStartPeriod;
      num2 = this.Params.RecoveryEndPeriod;
    }
    else if (year == this.Params.RecoveryYears)
    {
      num1 = 1;
      num2 = this.Params.RecoveryEndPeriod;
    }
    else
    {
      num1 = 1;
      num2 = this.Params.DepreciationPeriodsInYear;
    }
    int num3 = num2;
    if (year == this.Params.DepreciationYears)
      num3 = this.Params.DepreciationStopPeriod;
    for (int index = num1; index <= num3; ++index)
    {
      int periodLengthInDays = this.Params.GetFinPeriodLengthInDays(year, index);
      Decimal num4 = !($"{this.Params.DepreciationStartYear + year - 1:D4}{index:D2}" == this.Params.DepreciationStopBookPeriod.FinPeriodID) || !(this.Params.RecoveryEndBookPeriod.FinPeriodID == this.Params.DepreciationStopBookPeriod.FinPeriodID) ? this.Params.DepreciationBasis * this.Params.PercentPerYear / 100M * (Decimal) periodLengthInDays / 365M : this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation;
      Decimal num5 = this.Round(num4);
      if (num5 != this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation)
        rounding += num4 - num5;
      this.Params.AccumulatedDepreciation += num5;
      this.WhereToWriteDepreciation(writeToAsset, (FADepreciationMethodLines) null, assetBalance, year, index, num5, true, ref rounding);
    }
  }

  private void SetAustralianDiminishingValueDepr(
    bool writeToAsset,
    int year,
    FABookBalance assetBalance,
    ref Decimal rounding)
  {
    int num1;
    int num2;
    if (year == 1 && year < this.Params.RecoveryYears)
    {
      num1 = this.Params.DepreciationStartPeriod;
      num2 = this.Params.DepreciationPeriodsInYear;
    }
    else if (year == 1 && year == this.Params.RecoveryYears)
    {
      num1 = this.Params.DepreciationStartPeriod;
      num2 = this.Params.RecoveryEndPeriod;
    }
    else if (year == this.Params.RecoveryYears)
    {
      num1 = 1;
      num2 = this.Params.RecoveryEndPeriod;
    }
    else
    {
      num1 = 1;
      num2 = this.Params.DepreciationPeriodsInYear;
    }
    int num3 = num2;
    if (year == this.Params.DepreciationYears)
      num3 = this.Params.DepreciationStopPeriod;
    Decimal num4 = this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation;
    for (int index = num1; index <= num3; ++index)
    {
      int periodLengthInDays = this.Params.GetFinPeriodLengthInDays(year, index);
      Decimal num5 = num4 * this.Params.PercentPerYear / 100M * (Decimal) periodLengthInDays / 365M;
      Decimal num6 = this.Round(num5);
      if (num6 != this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation)
        rounding += num5 - num6;
      this.Params.AccumulatedDepreciation += num6;
      this.WhereToWriteDepreciation(writeToAsset, (FADepreciationMethodLines) null, assetBalance, year, index, num6, true, ref rounding);
    }
  }

  private void SetNewZealandStraightLineDepr(
    bool writeToAsset,
    int year,
    FABookBalance assetBalance,
    ref Decimal rounding)
  {
    int fromPeriod;
    int toPeriod1;
    if (year == 1 && year < this.Params.RecoveryYears)
    {
      fromPeriod = this.Params.DepreciationStartPeriod;
      toPeriod1 = this.Params.DepreciationPeriodsInYear;
    }
    else if (year == 1 && year == this.Params.RecoveryYears)
    {
      fromPeriod = this.Params.DepreciationStartPeriod;
      toPeriod1 = this.Params.RecoveryEndPeriod;
    }
    else if (year == this.Params.RecoveryYears)
    {
      fromPeriod = 1;
      toPeriod1 = this.Params.RecoveryEndPeriod;
    }
    else
    {
      fromPeriod = 1;
      toPeriod1 = this.Params.DepreciationPeriodsInYear;
    }
    int toPeriod2 = toPeriod1;
    if (year == this.Params.DepreciationYears)
      toPeriod2 = this.Params.DepreciationStopPeriod;
    int daysHeldInYear1 = this.Params.GetDaysHeldInYear(year, fromPeriod, toPeriod1);
    Decimal num1 = this.Params.DepreciationBasis * this.Params.PercentPerYear / 100M * (Decimal) (toPeriod1 - fromPeriod + 1) / (Decimal) this.Params.DepreciationPeriodsInYear;
    Decimal accumulatedDepreciation = this.Params.AccumulatedDepreciation;
    for (int index = fromPeriod; index <= toPeriod2; ++index)
    {
      int inDaysFebAlways28 = this.Params.GetFinPeriodLengthInDaysFebAlways28(year, index);
      string str = $"{this.Params.DepreciationStartYear + year - 1:D4}{index:D2}";
      bool useRounding = true;
      Decimal num2;
      if (str == this.Params.DepreciationStopBookPeriod.FinPeriodID && this.Params.RecoveryEndBookPeriod.FinPeriodID == this.Params.DepreciationStopBookPeriod.FinPeriodID)
        num2 = this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation;
      else if (index == toPeriod2)
      {
        Decimal num3 = this.Params.AccumulatedDepreciation - accumulatedDepreciation;
        Decimal num4;
        if (str == this.Params.DepreciationStopBookPeriod.FinPeriodID && this.Params.RecoveryEndBookPeriod.FinPeriodID != this.Params.DepreciationStopBookPeriod.FinPeriodID)
        {
          int daysHeldInYear2 = this.Params.GetDaysHeldInYear(year, fromPeriod, toPeriod2);
          num4 = num1 * (Decimal) daysHeldInYear2 / (Decimal) daysHeldInYear1;
        }
        else
          num4 = num1;
        num2 = num4 - num3;
        useRounding = false;
      }
      else
        num2 = num1 * (Decimal) inDaysFebAlways28 / (Decimal) daysHeldInYear1;
      Decimal num5 = this.Round(num2);
      if (num5 != this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation)
        rounding += num2 - num5;
      this.Params.AccumulatedDepreciation += num5;
      this.WhereToWriteDepreciation(writeToAsset, (FADepreciationMethodLines) null, assetBalance, year, index, num5, useRounding, ref rounding);
    }
  }

  private void SetNewZealandDiminishingValueDepr(
    bool writeToAsset,
    int year,
    FABookBalance assetBalance,
    ref Decimal rounding)
  {
    int fromPeriod;
    int toPeriod1;
    if (year == 1 && year < this.Params.RecoveryYears)
    {
      fromPeriod = this.Params.DepreciationStartPeriod;
      toPeriod1 = this.Params.DepreciationPeriodsInYear;
    }
    else if (year == 1 && year == this.Params.RecoveryYears)
    {
      fromPeriod = this.Params.DepreciationStartPeriod;
      toPeriod1 = this.Params.RecoveryEndPeriod;
    }
    else if (year == this.Params.RecoveryYears)
    {
      fromPeriod = 1;
      toPeriod1 = this.Params.RecoveryEndPeriod;
    }
    else
    {
      fromPeriod = 1;
      toPeriod1 = this.Params.DepreciationPeriodsInYear;
    }
    int toPeriod2 = toPeriod1;
    if (year == this.Params.DepreciationYears)
      toPeriod2 = this.Params.DepreciationStopPeriod;
    int daysHeldInYear1 = this.Params.GetDaysHeldInYear(year, fromPeriod, toPeriod1);
    Decimal num1 = (this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation) * this.Params.PercentPerYear / 100M * (Decimal) (toPeriod1 - fromPeriod + 1) / (Decimal) this.Params.DepreciationPeriodsInYear;
    Decimal accumulatedDepreciation = this.Params.AccumulatedDepreciation;
    for (int index = fromPeriod; index <= toPeriod2; ++index)
    {
      int inDaysFebAlways28 = this.Params.GetFinPeriodLengthInDaysFebAlways28(year, index);
      string str = $"{this.Params.DepreciationStartYear + year - 1:D4}{index:D2}";
      bool useRounding = true;
      Decimal num2;
      if (index == toPeriod2)
      {
        Decimal num3 = this.Params.AccumulatedDepreciation - accumulatedDepreciation;
        Decimal num4;
        if (str == this.Params.DepreciationStopBookPeriod.FinPeriodID && this.Params.RecoveryEndBookPeriod.FinPeriodID != this.Params.DepreciationStopBookPeriod.FinPeriodID)
        {
          int daysHeldInYear2 = this.Params.GetDaysHeldInYear(year, fromPeriod, toPeriod2);
          num4 = num1 * (Decimal) daysHeldInYear2 / (Decimal) daysHeldInYear1;
        }
        else
          num4 = num1;
        num2 = num4 - num3;
        useRounding = false;
      }
      else
        num2 = num1 * (Decimal) inDaysFebAlways28 / (Decimal) daysHeldInYear1;
      Decimal num5 = this.Round(num2);
      if (num5 != this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation)
        rounding += num2 - num5;
      this.Params.AccumulatedDepreciation += num5;
      this.WhereToWriteDepreciation(writeToAsset, (FADepreciationMethodLines) null, assetBalance, year, index, num5, useRounding, ref rounding);
    }
  }

  private void SetNewZealandStraightLineEvenlyDepr(
    bool writeToAsset,
    int year,
    FABookBalance assetBalance,
    ref Decimal rounding)
  {
    int num1;
    int num2;
    if (year == 1 && year < this.Params.RecoveryYears)
    {
      num1 = this.Params.DepreciationStartPeriod;
      num2 = this.Params.DepreciationPeriodsInYear;
    }
    else if (year == 1 && year == this.Params.RecoveryYears)
    {
      num1 = this.Params.DepreciationStartPeriod;
      num2 = this.Params.RecoveryEndPeriod;
    }
    else if (year == this.Params.RecoveryYears)
    {
      num1 = 1;
      num2 = this.Params.RecoveryEndPeriod;
    }
    else
    {
      num1 = 1;
      num2 = this.Params.DepreciationPeriodsInYear;
    }
    int num3 = num2;
    if (year == this.Params.DepreciationYears)
      num3 = this.Params.DepreciationStopPeriod;
    Decimal num4 = (Decimal) (num3 - num1 + 1);
    Decimal accumulatedDepreciation = this.Params.AccumulatedDepreciation;
    for (int period = num1; period <= num3; ++period)
    {
      string str = $"{this.Params.DepreciationStartYear + year - 1:D4}{period:D2}";
      bool useRounding = true;
      string finPeriodId = this.Params.DepreciationStopBookPeriod.FinPeriodID;
      Decimal num5;
      if (str == finPeriodId && this.Params.RecoveryEndBookPeriod.FinPeriodID == this.Params.DepreciationStopBookPeriod.FinPeriodID)
        num5 = this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation;
      else if (period == num3)
      {
        Decimal num6 = this.Params.AccumulatedDepreciation - accumulatedDepreciation;
        num5 = this.Params.DepreciationBasis * this.Params.PercentPerYear / 100M * (num4 / (Decimal) this.Params.DepreciationPeriodsInYear) - num6;
        useRounding = false;
      }
      else
        num5 = this.Params.DepreciationBasis * this.Params.PercentPerYear / 100M / (Decimal) this.Params.DepreciationPeriodsInYear;
      Decimal num7 = this.Round(num5);
      if (num7 != this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation)
        rounding += num5 - num7;
      this.Params.AccumulatedDepreciation += num7;
      this.WhereToWriteDepreciation(writeToAsset, (FADepreciationMethodLines) null, assetBalance, year, period, num7, useRounding, ref rounding);
    }
  }

  private void SetNewZealandDiminishingValueEvenlyDepr(
    bool writeToAsset,
    int year,
    FABookBalance assetBalance,
    ref Decimal rounding)
  {
    int num1;
    int num2;
    if (year == 1 && year < this.Params.RecoveryYears)
    {
      num1 = this.Params.DepreciationStartPeriod;
      num2 = this.Params.DepreciationPeriodsInYear;
    }
    else if (year == 1 && year == this.Params.RecoveryYears)
    {
      num1 = this.Params.DepreciationStartPeriod;
      num2 = this.Params.RecoveryEndPeriod;
    }
    else if (year == this.Params.RecoveryYears)
    {
      num1 = 1;
      num2 = this.Params.RecoveryEndPeriod;
    }
    else
    {
      num1 = 1;
      num2 = this.Params.DepreciationPeriodsInYear;
    }
    int num3 = num2;
    if (year == this.Params.DepreciationYears)
      num3 = this.Params.DepreciationStopPeriod;
    Decimal num4 = (Decimal) (num3 - num1 + 1);
    Decimal num5 = this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation;
    Decimal accumulatedDepreciation = this.Params.AccumulatedDepreciation;
    for (int period = num1; period <= num3; ++period)
    {
      bool useRounding = true;
      Decimal num6;
      if (period == num3)
      {
        Decimal num7 = this.Params.AccumulatedDepreciation - accumulatedDepreciation;
        num6 = num5 * this.Params.PercentPerYear / 100M * (num4 / (Decimal) this.Params.DepreciationPeriodsInYear) - num7;
        useRounding = false;
      }
      else
        num6 = num5 * this.Params.PercentPerYear / 100M / (Decimal) this.Params.DepreciationPeriodsInYear;
      Decimal num8 = this.Round(num6);
      if (num8 != this.Params.DepreciationBasis - this.Params.AccumulatedDepreciation)
        rounding += num6 - num8;
      this.Params.AccumulatedDepreciation += num8;
      this.WhereToWriteDepreciation(writeToAsset, (FADepreciationMethodLines) null, assetBalance, year, period, num8, useRounding, ref rounding);
    }
  }

  private void SetYDDepr(
    bool writeToAsset,
    bool yearlyAccountancy,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal rounding,
    ref DateTime previousEndDate)
  {
    if (!yearlyAccountancy)
      return;
    Decimal num1 = this.Params.WholeRecoveryPeriods / (Decimal) this.Params.DepreciationPeriodsInYear;
    Decimal num2 = num1 * (num1 + 1M) / 2M;
    Decimal num3 = num1 - (Decimal) this.Params.YearOfUsefulLife + 1M;
    Decimal num4 = this.Params.DepreciationBasis * num3 / num2;
    Decimal num5 = this.Params.DepreciationBasis * (num3 - 1M) / num2;
    int num6;
    int num7;
    if (year == 1 && year < this.Params.RecoveryYears)
    {
      num6 = this.Params.DepreciationStartPeriod;
      num7 = this.Params.DepreciationPeriodsInYear;
    }
    else if (year == 1 && year == this.Params.RecoveryYears)
    {
      num6 = this.Params.DepreciationStartPeriod;
      num7 = this.Params.RecoveryEndPeriod;
    }
    else if (year == this.Params.RecoveryYears)
    {
      num6 = 1;
      num7 = this.Params.RecoveryEndPeriod;
    }
    else
    {
      num6 = 1;
      num7 = this.Params.DepreciationPeriodsInYear;
    }
    if (year == this.Params.DepreciationYears)
      num7 = this.Params.DepreciationStopPeriod;
    bool flag = year == 1;
    switch (this.Params.AveragingConvention)
    {
      case "FD":
        int num8 = 0;
        DateTime dateTime = previousEndDate;
        for (int currPeriod = num6; currPeriod <= num7; ++currPeriod)
          num8 += this.Params.GetDaysOnPeriod(this.Params.DepreciationStartDate, new DateTime?(this.Params.RecoveryEndDate), this.Params.DepreciationStartYear + year - 1, currPeriod, ref previousEndDate);
        previousEndDate = dateTime;
        for (int index = num6; index <= num7 && !(this.Params.DepreciationBasis == this.Params.AccumulatedDepreciation); ++index)
        {
          if (this.Params.AveragingConvention == "FD")
          {
            if (!flag)
            {
              flag = index == this.Params.DepreciationStartPeriod;
              this.Params.YearOfUsefulLife += flag ? 1 : 0;
            }
            int daysOnPeriod = this.Params.GetDaysOnPeriod(this.Params.DepreciationStartDate, this.Params.DepreciationStopDate, this.Params.DepreciationStartYear + year - 1, index, ref previousEndDate);
            Decimal num9 = (num1 - num3 + 1M == (Decimal) this.Params.YearOfUsefulLife ? num4 : num5) * (Decimal) daysOnPeriod / (Decimal) num8;
            this.Params.AccumulatedDepreciation += this.Round(num9);
            rounding += num9 - this.Round(num9);
            this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, index, num9, true, ref rounding);
          }
        }
        break;
      case "FP":
        for (int period = num6; period <= num7; ++period)
        {
          if (!flag)
          {
            flag = period == this.Params.DepreciationStartPeriod;
            this.Params.YearOfUsefulLife += flag ? 1 : 0;
          }
          Decimal num10 = (num1 - num3 + 1M == (Decimal) this.Params.YearOfUsefulLife ? num4 : num5) / (Decimal) this.Params.DepreciationPeriodsInYear;
          this.Params.AccumulatedDepreciation += this.Round(num10);
          rounding += num10 - this.Round(num10);
          this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, period, num10, true, ref rounding);
        }
        break;
    }
  }

  private void SetSLDeprHalfPeriodFirstYearNotEqualLastYear(
    bool writeToAsset,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal lastDepreciation,
    ref Decimal rounding)
  {
    Decimal num1 = this.Params.DepreciationBasis / (this.Params.WholeRecoveryPeriods - 1M) / 2M;
    lastDepreciation = num1;
    Decimal num2 = this.Params.WholeRecoveryPeriods - 2M;
    rounding = this.Params.DepreciationBasis - this.Round(num1) * 2M;
    if (num2 > 0M)
    {
      otherDepreciation = (this.Params.DepreciationBasis - num1 * 2M) / num2;
      rounding -= this.Round(otherDepreciation) * num2;
    }
    int num3 = this.Params.DepreciationPeriodsInYear;
    if (year == this.Params.DepreciationYears)
      num3 = this.Params.DepreciationStopPeriod;
    for (int depreciationStartPeriod = this.Params.DepreciationStartPeriod; depreciationStartPeriod <= num3; ++depreciationStartPeriod)
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, depreciationStartPeriod, this.Params.DepreciationStartPeriod == depreciationStartPeriod ? num1 : otherDepreciation, depreciationStartPeriod > this.Params.DepreciationStartPeriod, ref rounding);
  }

  private void SetSLDeprHalfPeriodFirstYearEqualLastYear(
    bool writeToAsset,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal rounding)
  {
    Decimal num1 = this.Params.DepreciationBasis / (this.Params.WholeRecoveryPeriods - 1M) / 2M;
    Decimal num2 = this.Params.WholeRecoveryPeriods - 2M;
    if (num2 > 0M)
      otherDepreciation = (this.Params.DepreciationBasis - num1 * 2M) / num2;
    rounding = this.Params.DepreciationBasis - this.Round(num1) * 2M;
    if (num2 > 0M)
      rounding -= this.Round(otherDepreciation) * num2;
    for (int depreciationStartPeriod = this.Params.DepreciationStartPeriod; depreciationStartPeriod <= this.Params.DepreciationStopPeriod; ++depreciationStartPeriod)
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, depreciationStartPeriod, this.Params.DepreciationStartPeriod == depreciationStartPeriod || this.Params.RecoveryEndPeriod == depreciationStartPeriod ? num1 : otherDepreciation, depreciationStartPeriod > this.Params.DepreciationStartPeriod, ref rounding);
  }

  private void SetSLDeprHalfPeriodLastYear(
    bool writeToAsset,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal lastDepreciation,
    ref Decimal rounding)
  {
    for (int period = 1; period <= this.Params.DepreciationStopPeriod; ++period)
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, period, this.Params.RecoveryEndPeriod == period ? lastDepreciation : otherDepreciation, true, ref rounding);
  }

  private void SetSLDeprHalfQuarterFirstYearNotEqualLastYear(
    bool writeToAsset,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal lastDepreciation,
    ref Decimal rounding)
  {
    Decimal num1 = (Decimal) (this.Params.LastDepreciationStartQuarterPeriod - this.Params.DepreciationStartPeriod + 1);
    Decimal num2 = (Decimal) (this.Params.RecoveryEndPeriod - this.Params.FirstRecoveryEndQuarterPeriod + 1);
    Decimal num3 = this.Params.DepreciationBasis / (Decimal) (this.Params.RecoveryYears - 1) / 4M / 2M / num1;
    lastDepreciation = this.Params.DepreciationBasis / (Decimal) (this.Params.RecoveryYears - 1) / 2M / 4M / num2;
    Decimal num4 = this.Params.WholeRecoveryPeriods - num1 - num2;
    if (num4 > 0M)
      otherDepreciation = (this.Params.DepreciationBasis - num3 * num1 - lastDepreciation * num2) / num4;
    rounding = this.Params.DepreciationBasis - this.Round(num3) * num1;
    rounding -= this.Round(lastDepreciation) * num2;
    if (num4 > 0M)
      rounding -= this.Round(otherDepreciation) * num4;
    int num5 = this.Params.DepreciationPeriodsInYear;
    if (year == this.Params.DepreciationYears)
      num5 = this.Params.DepreciationStopPeriod;
    for (int depreciationStartPeriod = this.Params.DepreciationStartPeriod; depreciationStartPeriod <= num5; ++depreciationStartPeriod)
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, depreciationStartPeriod, this.Params.DepreciationStartPeriod > depreciationStartPeriod || this.Params.LastDepreciationStartQuarterPeriod < depreciationStartPeriod ? otherDepreciation : num3, depreciationStartPeriod > this.Params.DepreciationStartPeriod, ref rounding);
  }

  private void SetSLDeprHalfQuarterLastYear(
    bool writeToAsset,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal lastDepreciation,
    ref Decimal rounding)
  {
    for (int period = 1; period <= this.Params.DepreciationStopPeriod; ++period)
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, period, this.Params.RecoveryEndPeriod < period || this.Params.FirstRecoveryEndQuarterPeriod > period ? otherDepreciation : lastDepreciation, true, ref rounding);
  }

  private void SetSLDeprModifiedPeriodFirstYearNotEqualLastYear(
    bool writeToAsset,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal lastDepreciation,
    ref Decimal rounding)
  {
    Decimal num1 = this.Params.WholeRecoveryPeriods + this.Params.StartDepreciationMidPeriodRatio + this.Params.StopDepreciationMidPeriodRatio - 2M;
    Decimal num2 = this.Params.DepreciationBasis / num1 * this.Params.StartDepreciationMidPeriodRatio;
    lastDepreciation = this.Params.DepreciationBasis / num1 * this.Params.StopDepreciationMidPeriodRatio;
    rounding = this.Params.DepreciationBasis - this.Round(num2);
    rounding -= this.Round(lastDepreciation);
    Decimal num3 = this.Params.WholeRecoveryPeriods - 2M;
    if (num3 > 0M)
    {
      otherDepreciation = (this.Params.DepreciationBasis - num2 - lastDepreciation) / num3;
      rounding -= this.Round(otherDepreciation) * num3;
    }
    int num4 = this.Params.DepreciationPeriodsInYear;
    if (year == this.Params.DepreciationYears)
      num4 = this.Params.DepreciationStopPeriod;
    for (int depreciationStartPeriod = this.Params.DepreciationStartPeriod; depreciationStartPeriod <= num4; ++depreciationStartPeriod)
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, depreciationStartPeriod, this.Params.DepreciationStartPeriod == depreciationStartPeriod ? num2 : (year != this.Params.DepreciationYears || depreciationStartPeriod != this.Params.RecoveryEndPeriod ? otherDepreciation : lastDepreciation), depreciationStartPeriod > this.Params.DepreciationStartPeriod, ref rounding);
  }

  private void SetSLDeprModifiedPeriodFirstYearEqualLastYear(
    bool writeToAsset,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal lastDepreciation,
    ref Decimal rounding)
  {
    Decimal num1 = this.Params.WholeRecoveryPeriods + this.Params.StartDepreciationMidPeriodRatio + this.Params.StopDepreciationMidPeriodRatio - 2M;
    Decimal num2 = this.Params.DepreciationBasis / num1 * this.Params.StartDepreciationMidPeriodRatio;
    lastDepreciation = this.Params.DepreciationBasis / num1 * this.Params.StopDepreciationMidPeriodRatio;
    Decimal num3 = this.Params.WholeRecoveryPeriods - 2M;
    if (num3 > 0M)
      otherDepreciation = (this.Params.DepreciationBasis - num2 - lastDepreciation) / num3;
    rounding = this.Params.DepreciationBasis - this.Round(num2);
    rounding -= this.Round(lastDepreciation);
    if (num3 > 0M)
      rounding -= this.Round(otherDepreciation) * num3;
    for (int depreciationStartPeriod = this.Params.DepreciationStartPeriod; depreciationStartPeriod <= this.Params.DepreciationStopPeriod; ++depreciationStartPeriod)
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, depreciationStartPeriod, this.Params.DepreciationStartPeriod == depreciationStartPeriod ? num2 : (this.Params.RecoveryEndPeriod == depreciationStartPeriod ? lastDepreciation : otherDepreciation), depreciationStartPeriod > this.Params.DepreciationStartPeriod, ref rounding);
  }

  private void SetSLDeprFullYearFirstYearNotEqualLastYear(
    bool writeToAsset,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal rounding)
  {
    Decimal num1 = otherDepreciation * (Decimal) this.Params.DepreciationPeriodsInYear / (Decimal) (this.Params.DepreciationPeriodsInYear - this.Params.DepreciationStartPeriod + 1);
    Decimal num2 = this.Params.WholeRecoveryPeriods - (Decimal) this.Params.DepreciationPeriodsInYear;
    if (num2 > 0M)
      otherDepreciation = (this.Params.DepreciationBasis - num1 * (Decimal) (this.Params.DepreciationPeriodsInYear - this.Params.DepreciationStartPeriod + 1)) / num2;
    rounding = this.Params.DepreciationBasis - this.Round(num1) * (Decimal) (this.Params.DepreciationPeriodsInYear - this.Params.DepreciationStartPeriod + 1);
    if (num2 > 0M)
      rounding -= this.Round(otherDepreciation) * num2;
    int num3 = this.Params.DepreciationPeriodsInYear;
    if (year == this.Params.DepreciationYears)
      num3 = this.Params.DepreciationStopPeriod;
    for (int depreciationStartPeriod = this.Params.DepreciationStartPeriod; depreciationStartPeriod <= num3; ++depreciationStartPeriod)
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, depreciationStartPeriod, num1, depreciationStartPeriod > this.Params.DepreciationStartPeriod, ref rounding);
  }

  private void SetSLDeprFullYearLastYear(
    bool writeToAsset,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal rounding)
  {
    for (int period = 1; period <= this.Params.DepreciationStopPeriod; ++period)
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, period, otherDepreciation, true, ref rounding);
  }

  private void SetSLDeprFullPeriodFirstYearNotEqualLastYear(
    bool writeToAsset,
    bool yearlyAccountancy,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal rounding)
  {
    bool flag = yearlyAccountancy && line != null;
    if (flag)
      otherDepreciation = line.RatioPerYear.GetValueOrDefault() * this.Params.DepreciationBasis / (Decimal) (this.Params.DepreciationPeriodsInYear - this.Params.DepreciationStartPeriod + 1);
    else
      rounding = this.Params.DepreciationBasis - this.Round(otherDepreciation) * this.Params.WholeRecoveryPeriods;
    int num = this.Params.DepreciationPeriodsInYear;
    if (year == this.Params.DepreciationYears)
      num = this.Params.DepreciationStopPeriod;
    for (int depreciationStartPeriod = this.Params.DepreciationStartPeriod; depreciationStartPeriod <= num; ++depreciationStartPeriod)
    {
      if (flag)
      {
        this.Params.AccumulatedDepreciation += this.Round(otherDepreciation);
        rounding += otherDepreciation - this.Round(otherDepreciation);
      }
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, depreciationStartPeriod, otherDepreciation, ((this.Params.DepreciationStartPeriod >= this.Params.DepreciationPeriodsInYear ? 1 : (depreciationStartPeriod > this.Params.DepreciationStartPeriod ? 1 : 0)) | (yearlyAccountancy ? 1 : 0)) != 0, ref rounding);
    }
  }

  private void SetSLDeprFullPeriodFirstYearEqualLastYear(
    bool writeToAsset,
    bool yearlyAccountancy,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal rounding)
  {
    bool flag = yearlyAccountancy && line != null;
    if (flag)
      otherDepreciation = line.RatioPerYear.GetValueOrDefault() * this.Params.DepreciationBasis / this.Params.WholeRecoveryPeriods;
    else
      rounding = this.Params.DepreciationBasis - this.Round(otherDepreciation) * this.Params.WholeRecoveryPeriods;
    for (int depreciationStartPeriod = this.Params.DepreciationStartPeriod; depreciationStartPeriod <= this.Params.DepreciationStopPeriod; ++depreciationStartPeriod)
    {
      if (flag)
      {
        this.Params.AccumulatedDepreciation += this.Round(otherDepreciation);
        rounding += otherDepreciation - this.Round(otherDepreciation);
      }
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, depreciationStartPeriod, otherDepreciation, depreciationStartPeriod > this.Params.DepreciationStartPeriod | yearlyAccountancy, ref rounding);
    }
  }

  private void SetSLDeprFullYearFirstYearEqualLastYear(
    bool writeToAsset,
    bool yearlyAccountancy,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal rounding)
  {
    Decimal num = (Decimal) (this.Params.DepreciationPeriodsInYear - this.Params.DepreciationStartPeriod + 1);
    otherDepreciation = this.Params.DepreciationBasis / num;
    bool flag = yearlyAccountancy && line != null;
    if (flag)
      otherDepreciation = line.RatioPerYear.GetValueOrDefault() * otherDepreciation;
    else
      rounding = this.Params.DepreciationBasis - this.Round(otherDepreciation) * num;
    for (int depreciationStartPeriod = this.Params.DepreciationStartPeriod; depreciationStartPeriod <= this.Params.DepreciationStopPeriod; ++depreciationStartPeriod)
    {
      if (flag)
      {
        this.Params.AccumulatedDepreciation += this.Round(otherDepreciation);
        rounding += otherDepreciation - this.Round(otherDepreciation);
      }
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, depreciationStartPeriod, otherDepreciation, depreciationStartPeriod > this.Params.DepreciationStartPeriod | yearlyAccountancy, ref rounding);
    }
  }

  private void SetSLDeprFullPeriodLastYear(
    bool writeToAsset,
    bool yearlyAccountancy,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal rounding)
  {
    bool flag = yearlyAccountancy && line != null;
    if (flag)
      otherDepreciation = line.RatioPerYear.GetValueOrDefault() * this.Params.DepreciationBasis / (Decimal) this.Params.RecoveryEndPeriod;
    for (int period = 1; period <= this.Params.DepreciationStopPeriod; ++period)
    {
      if (flag)
      {
        this.Params.AccumulatedDepreciation += this.Round(otherDepreciation);
        rounding += otherDepreciation - this.Round(otherDepreciation);
      }
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, period, otherDepreciation, true, ref rounding);
    }
  }

  private void SetSLDeprFullQuarterFirstYearNotEqualLastYear(
    bool writeToAsset,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    Decimal yearDepreciation,
    ref Decimal otherDepreciation,
    ref Decimal lastDepreciation,
    ref Decimal rounding)
  {
    Decimal num1 = yearDepreciation / 4M / (Decimal) (this.Params.LastDepreciationStartQuarterPeriod - this.Params.DepreciationStartPeriod + 1);
    Decimal num2 = this.Params.WholeRecoveryPeriods - 3M;
    if (num2 > 0M)
      otherDepreciation = (this.Params.DepreciationBasis - num1 * (Decimal) (this.Params.LastDepreciationStartQuarterPeriod - this.Params.DepreciationStartPeriod + 1)) / num2;
    rounding = this.Params.DepreciationBasis - this.Round(num1) * (Decimal) (this.Params.LastDepreciationStartQuarterPeriod - this.Params.DepreciationStartPeriod + 1);
    if (num2 > 0M)
      rounding -= this.Round(otherDepreciation) * num2;
    int num3 = this.Params.DepreciationPeriodsInYear;
    if (year == this.Params.DepreciationYears)
      num3 = this.Params.DepreciationStopPeriod;
    for (int depreciationStartPeriod = this.Params.DepreciationStartPeriod; depreciationStartPeriod <= num3; ++depreciationStartPeriod)
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, depreciationStartPeriod, this.Params.DepreciationStartPeriod > depreciationStartPeriod || this.Params.LastDepreciationStartQuarterPeriod < depreciationStartPeriod ? otherDepreciation : num1, depreciationStartPeriod > this.Params.DepreciationStartPeriod, ref rounding);
  }

  private void SetSLDeprFullQuarterLastYear(
    bool writeToAsset,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal rounding)
  {
    for (int period = 1; period <= this.Params.DepreciationStopPeriod; ++period)
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, period, otherDepreciation, true, ref rounding);
  }

  private void SetSLDeprHalfYearFirstYearNotEqualLastYear(
    bool writeToAsset,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal lastDepreciation,
    ref Decimal rounding)
  {
    Decimal num1 = (Decimal) (this.Params.DepreciationPeriodsInYear - this.Params.DepreciationStartPeriod + 1);
    Decimal recoveryEndPeriod = (Decimal) this.Params.RecoveryEndPeriod;
    Decimal num2 = this.Params.DepreciationBasis / (Decimal) (this.Params.RecoveryYears - 1) / 2M / num1;
    lastDepreciation = this.Params.DepreciationBasis / (Decimal) (this.Params.RecoveryYears - 1) / 2M / recoveryEndPeriod;
    Decimal num3 = this.Params.WholeRecoveryPeriods - num1 - recoveryEndPeriod;
    if (num3 > 0M)
      otherDepreciation = (this.Params.DepreciationBasis - num2 * num1 - lastDepreciation * recoveryEndPeriod) / num3;
    rounding = this.Params.DepreciationBasis - this.Round(num2) * num1;
    rounding -= this.Round(lastDepreciation) * recoveryEndPeriod;
    if (num3 > 0M)
      rounding -= this.Round(otherDepreciation) * num3;
    int num4 = this.Params.DepreciationPeriodsInYear;
    if (year == this.Params.DepreciationYears)
      num4 = this.Params.DepreciationStopPeriod;
    for (int depreciationStartPeriod = this.Params.DepreciationStartPeriod; depreciationStartPeriod <= num4; ++depreciationStartPeriod)
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, depreciationStartPeriod, num2, depreciationStartPeriod > this.Params.DepreciationStartPeriod, ref rounding);
  }

  private void SetSLDeprHalfYearLastYear(
    bool writeToAsset,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal lastDepreciation,
    ref Decimal rounding)
  {
    for (int period = 1; period <= this.Params.DepreciationStopPeriod; ++period)
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, period, lastDepreciation, true, ref rounding);
  }

  private void SetSLDeprOtherYears(
    bool writeToAsset,
    FADepreciationMethodLines line,
    int year,
    FABookBalance assetBalance,
    ref Decimal otherDepreciation,
    ref Decimal rounding)
  {
    int num = this.Params.DepreciationPeriodsInYear;
    if (year == this.Params.DepreciationYears)
      num = this.Params.DepreciationStopPeriod;
    for (int period = 1; period <= num; ++period)
      this.WhereToWriteDepreciation(writeToAsset, line, assetBalance, year, period, otherDepreciation, true, ref rounding);
  }

  public virtual void Calculate(FABookBalance assetBalance, string maxPeriodID = null, PXGraph uiGraph = null)
  {
    if (this.PerformCalculation(assetBalance, maxPeriodID, uiGraph))
      return;
    this.CalculateDepreciation(assetBalance, maxPeriodID);
  }

  protected virtual IEnumerable<DepreciationMethodBase> GetDepreciationMethods()
  {
    yield return (DepreciationMethodBase) new StraightLineFullPeriodMethod();
    yield return (DepreciationMethodBase) new StraightLineFullDayMethod();
    yield return (DepreciationMethodBase) new AustralianPrimeCostMethod();
    yield return (DepreciationMethodBase) new AustralianDiminishingValueMethod(((PXSelectBase<PX.Objects.FA.FASetup>) this.FASetup).Current);
    yield return (DepreciationMethodBase) new NewZealandDiminishingValueMethod();
    yield return (DepreciationMethodBase) new NewZealandStraightLineMethod();
    yield return (DepreciationMethodBase) new DutchMethod2();
    yield return (DepreciationMethodBase) new RemainingValueByDaysInPeriodMethod();
    yield return (DepreciationMethodBase) new RemainingValueFullPeriodMethod();
  }

  public DepreciationMethodBase GetSuitableDepreciationMethod(FABookBalance bookBalance)
  {
    IncomingCalculationParameters incomingParameters = new IncomingCalculationParameters((PXGraph) this, bookBalance);
    if (incomingParameters.Method.IsTableMethod.GetValueOrDefault())
      return (DepreciationMethodBase) null;
    DepreciationMethodBase[] array = this.GetDepreciationMethods().Where<DepreciationMethodBase>((Func<DepreciationMethodBase, bool>) (method => method.IsSuitable(incomingParameters))).ToArray<DepreciationMethodBase>();
    return array.Length <= 1 ? ((IEnumerable<DepreciationMethodBase>) array).FirstOrDefault<DepreciationMethodBase>() : throw new PXException("Several competing depreciation methods are found for the {0} calculation method and the {1} averaging convention: {3}", new object[3]
    {
      (object) incomingParameters.CalculationMethod,
      (object) incomingParameters.AveragingConvention,
      (object) string.Join(", ", ((IEnumerable<DepreciationMethodBase>) array).Select<DepreciationMethodBase, string>((Func<DepreciationMethodBase, string>) (methodExtension => methodExtension.GetType().Name)))
    });
  }

  private bool PerformCalculation(FABookBalance bookBalance, string maxPeriodID = null, PXGraph uiGraph = null)
  {
    if (!bookBalance.Depreciate.GetValueOrDefault())
      return true;
    FixedAsset asset = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) bookBalance.AssetID
    }));
    if (asset.UnderConstruction.GetValueOrDefault())
      return false;
    this.CheckBalance(bookBalance);
    DepreciationMethodBase depreciationMethod = this.GetSuitableDepreciationMethod(bookBalance);
    if (depreciationMethod == null)
      return false;
    FAClass faClass = PXResultset<FAClass>.op_Implicit(PXSelectBase<FAClass, PXSelect<FAClass, Where<FAClass.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) depreciationMethod.IncomingParameters.FixedAsset.ClassID
    }));
    if (depreciationMethod.IsStraightLine && faClass.AcceleratedDepreciation.GetValueOrDefault())
    {
      PXSetPropertyException propertyException = new PXSetPropertyException("The Accelerated Depreciation for SL Depr. Method check box selected for the asset class does not affect the {0} depreciation method based on the Straight-Line calculation method. With these settings, the asset’s net value may not become zero at the end of its useful life in some cases. To get zero net value at the end of the asset’s useful life, on the Balance tab of the Fixed Assets (FA202500) form, change the depreciation method to a method based on the Remaining Value calculation method.", (PXErrorLevel) 3, new object[1]
      {
        (object) depreciationMethod.IncomingParameters.Method.MethodCD
      });
      if (uiGraph != null)
        ((PXCache) GraphHelper.Caches<FABookBalance>(uiGraph)).RaiseExceptionHandling<FABookBalance.depreciationMethodID>((object) bookBalance, (object) null, (Exception) propertyException);
      else
        PXProcessing<FABookBalance>.SetWarning((Exception) propertyException);
    }
    ICollection<DepreciationMethodBase.FADepreciationScheduleItem> depreciation = this.CalculateDepreciation(depreciationMethod, maxPeriodID);
    CalculationParameters calculationParameters = depreciationMethod.CalculationParameters;
    this.FillCalculatedHistory(bookBalance, depreciationMethod.IncomingParameters, depreciation, this.CalculateDepreciationOfAmount<FABookBalance.tax179Amount>(depreciationMethod, maxPeriodID), this.CalculateDepreciationOfAmount<FABookBalance.bonusAmount>(depreciationMethod, maxPeriodID), maxPeriodID);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this.TrimExtraHistory(depreciationMethod, calculationParameters);
      GraphHelper.MarkUpdated(((PXSelectBase) this.FADetails).Cache, (object) depreciationMethod.IncomingParameters.Details);
      ((PXAction) this.Save).Press();
      this.CloseHistoryInClosedPeriods(asset, depreciationMethod);
      transactionScope.Complete();
    }
    return true;
  }

  private void TrimExtraHistory(
    DepreciationMethodBase depreciationMethod,
    CalculationParameters calculationParameters)
  {
    FABookBalance bookBalance = depreciationMethod.IncomingParameters.BookBalance;
    PX.Objects.FA.FADetails details = depreciationMethod.IncomingParameters.Details;
    string str = bookBalance.DeprFromPeriod;
    if (calculationParameters != null && calculationParameters.Additions != null)
    {
      foreach (FAAddition addition in calculationParameters.Additions)
      {
        string depreciateFromPeriodId = addition?.CalculatedAdditionParameters?.DepreciateFromPeriodID;
        if (!string.IsNullOrEmpty(depreciateFromPeriodId) && string.CompareOrdinal(str, depreciateFromPeriodId) > 0)
          str = depreciateFromPeriodId;
      }
    }
    string period1 = this.FABookPeriodRepository.GetFABookPeriodIDOfDate(details.ReceiptDate, bookBalance.BookID, bookBalance.AssetID, false) ?? str;
    PXDatabase.Delete<FABookHistory>(new PXDataFieldRestrict[3]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<FABookHistory.assetID>((PXDbType) 8, new int?(4), (object) bookBalance.AssetID, (PXComp) 0),
      (PXDataFieldRestrict) new PXDataFieldRestrict<FABookHistory.bookID>((PXDbType) 8, new int?(4), (object) bookBalance.BookID, (PXComp) 0),
      (PXDataFieldRestrict) new PXDataFieldRestrict<FABookHistory.finPeriodID>((PXDbType) 3, new int?(6), (object) FinPeriodUtils.Min(period1, str), (PXComp) 4)
    });
    if (string.CompareOrdinal(bookBalance.LastDeprPeriod, bookBalance.DeprToPeriod) < 0)
      PXDatabase.Delete<FABookHistory>(new PXDataFieldRestrict[3]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<FABookHistory.assetID>((PXDbType) 8, new int?(4), (object) bookBalance.AssetID, (PXComp) 0),
        (PXDataFieldRestrict) new PXDataFieldRestrict<FABookHistory.bookID>((PXDbType) 8, new int?(4), (object) bookBalance.BookID, (PXComp) 0),
        (PXDataFieldRestrict) new PXDataFieldRestrict<FABookHistory.finPeriodID>((PXDbType) 3, new int?(6), (object) bookBalance.DeprToPeriod, (PXComp) 2)
      });
    bookBalance.MaxHistoryPeriodID = FAHelper.GetFABookHistoryMaxPeriodID((PXGraph) this, bookBalance.AssetID, bookBalance.BookID);
    GraphHelper.EnsureRowPersistence((PXGraph) this, (object) bookBalance);
  }

  private void CloseHistoryInClosedPeriods(
    FixedAsset asset,
    DepreciationMethodBase depreciationMethod)
  {
    FABookBalance bookBalance = depreciationMethod.IncomingParameters.BookBalance;
    if (!bookBalance.UpdateGL.GetValueOrDefault())
      return;
    string str = (string) null;
    foreach (PXResult<FABookHistory> pxResult in PXSelectBase<FABookHistory, PXSelectJoin<FABookHistory, InnerJoin<FixedAsset, On<FixedAsset.assetID, Equal<FABookHistory.assetID>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<FixedAsset.branchID>>, InnerJoin<OrganizationFinPeriod, On<OrganizationFinPeriod.organizationID, Equal<PX.Objects.GL.Branch.organizationID>, And<OrganizationFinPeriod.finPeriodID, Equal<FABookHistory.finPeriodID>>>>>>, Where<FABookHistory.closed, NotEqual<True>, And<OrganizationFinPeriod.fAClosed, Equal<True>, And<FABookHistory.assetID, Equal<Current<FABookBalance.assetID>>, And<FABookHistory.bookID, Equal<Current<FABookBalance.bookID>>>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) bookBalance
    }, Array.Empty<object>()))
    {
      FABookHistory faBookHistory = PXResult<FABookHistory>.op_Implicit(pxResult);
      FABookHist keyedHistory = new FABookHist();
      keyedHistory.AssetID = bookBalance.AssetID;
      keyedHistory.BookID = bookBalance.BookID;
      keyedHistory.FinPeriodID = faBookHistory.FinPeriodID;
      FAHelper.InsertFABookHist((PXGraph) this, keyedHistory, ref bookBalance).Closed = new bool?(true);
      if (str == null || string.CompareOrdinal(faBookHistory.FinPeriodID, str) > 0)
        str = faBookHistory.FinPeriodID;
    }
    if (string.CompareOrdinal(str, bookBalance.DeprFromPeriod) >= 0)
      PX.Objects.FA.AssetProcess.SetLastDeprPeriod(asset, (PXSelectBase<FABookBalance>) this.BookBalance, bookBalance, str);
    PX.Objects.FA.AssetProcess.AdjustFixedAssetStatus((PXGraph) this, bookBalance.AssetID);
    ((PXAction) this.Save).Press();
  }

  public virtual ICollection<DepreciationMethodBase.FADepreciationScheduleItem> CalculateDepreciation(
    DepreciationMethodBase depreciationMethod,
    string maxPeriodID = null)
  {
    return depreciationMethod.CalculateDepreciation(maxPeriodID).Round<DepreciationMethodBase.FADepreciationScheduleItem>((Func<DepreciationMethodBase.FADepreciationScheduleItem, Decimal>) (item => item.DepreciationAmount), (Action<DepreciationMethodBase.FADepreciationScheduleItem, Decimal>) ((item, value) => item.DepreciationAmount = value), depreciationMethod.IncomingParameters.Precision, PXRounder.SpreadType.Flow);
  }

  protected virtual ICollection<DepreciationMethodBase.FADepreciationScheduleItem> CalculateDepreciationOfAmount<TField>(
    DepreciationMethodBase depreciationMethod,
    string maxPeriodID = null,
    bool considerExistenceIncomingAdditions = true)
    where TField : IBqlField
  {
    if (depreciationMethod.IncomingParameters == null)
      throw new ArgumentNullException("IncomingParameters");
    if (depreciationMethod.IncomingParameters.Graph == null)
      throw new ArgumentNullException("Graph");
    FABookBalance bookBalance = depreciationMethod.IncomingParameters.BookBalance;
    if (bookBalance == null)
      throw new ArgumentNullException("BookBalance");
    ICollection<DepreciationMethodBase.FADepreciationScheduleItem> depreciationOfAmount = (ICollection<DepreciationMethodBase.FADepreciationScheduleItem>) new DepreciationMethodBase.FADepreciationScheduleItem[0];
    if (considerExistenceIncomingAdditions && (depreciationMethod.IncomingParameters.Additions == null || depreciationMethod.IncomingParameters.Additions.IsEmpty<FAAddition>()))
      return depreciationOfAmount;
    Decimal? nullable1 = (Decimal?) ((PXCache) GraphHelper.Caches<FABookBalance>(depreciationMethod.IncomingParameters.Graph)).GetValue<TField>((object) bookBalance);
    if (nullable1.HasValue)
    {
      Decimal? nullable2 = nullable1;
      Decimal num = 0M;
      if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
      {
        CalculationParameters parameters = new CalculationParameters(depreciationMethod.IncomingParameters, maxPeriodID)
        {
          Additions = new List<FAAddition>()
          {
            new FAAddition(nullable1.Value, bookBalance.DeprFromPeriod, bookBalance.DeprFromDate.Value, depreciationMethod.IncomingParameters.Precision, 100M)
          }
        };
        parameters.OriginalAddition = parameters.Additions.First<FAAddition>();
        parameters.OriginalAddition.MarkOriginal();
        return depreciationMethod.CalculateDepreciation(parameters).Round<DepreciationMethodBase.FADepreciationScheduleItem>((Func<DepreciationMethodBase.FADepreciationScheduleItem, Decimal>) (item => item.DepreciationAmount), (Action<DepreciationMethodBase.FADepreciationScheduleItem, Decimal>) ((item, value) => item.DepreciationAmount = value), depreciationMethod.IncomingParameters.Precision, PXRounder.SpreadType.Flow);
      }
    }
    return depreciationOfAmount;
  }

  protected virtual Dictionary<string, FABookHistory> GetExistingHistory(
    FABookBalance bookBalance,
    string minPeriodID,
    string maxPeriodID)
  {
    PXView view = PXSelectBase<FABookHistory, PXViewOf<FABookHistory>.BasedOn<SelectFromBase<FABookHistory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookHistory.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FABookHistory.bookID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<FABookHistory.finPeriodID, IBqlString>.IsGreaterEqual<P.AsString>>>>.And<BqlOperand<FABookHistory.finPeriodID, IBqlString>.IsLessEqual<P.AsString>>>>.ReadOnly.Config>.GetCommand().CreateView((PXGraph) this);
    using (new PXFieldScope(view, (IEnumerable<Type>) new List<Type>()
    {
      typeof (FABookHistory.assetID),
      typeof (FABookHistory.bookID),
      typeof (FABookHistory.finPeriodID),
      typeof (FABookHistory.ptdBonusCalculated),
      typeof (FABookHistory.ytdBonusCalculated),
      typeof (FABookHistory.ptdTax179Calculated),
      typeof (FABookHistory.ytdTax179Calculated),
      typeof (FABookHistory.ptdCalculated),
      typeof (FABookHistory.ytdCalculated),
      typeof (FABookHistory.ptdTax179Taken),
      typeof (FABookHistory.ytdTax179Taken),
      typeof (FABookHistory.ptdBonusTaken),
      typeof (FABookHistory.ytdBonusTaken)
    }, true))
      return GraphHelper.RowCast<FABookHistory>((IEnumerable) view.SelectMulti(new object[4]
      {
        (object) bookBalance.AssetID,
        (object) bookBalance.BookID,
        (object) minPeriodID,
        (object) maxPeriodID
      })).ToDictionary<FABookHistory, string, FABookHistory>((Func<FABookHistory, string>) (h => h.FinPeriodID), (Func<FABookHistory, FABookHistory>) (h => h));
  }

  protected virtual void ClearCalculatedHistoryTails(
    Dictionary<string, FABookHistory> existingHistory,
    string maxPeriodID)
  {
    FABookHistory faBookHistory;
    if (!existingHistory.TryGetValue(maxPeriodID, out faBookHistory))
      return;
    PXUpdate<Set<FABookHistory.ptdTax179Taken, decimal0, Set<FABookHistory.ytdTax179Taken, P.AsDecimal, Set<FABookHistory.ptdBonusTaken, decimal0, Set<FABookHistory.ytdBonusTaken, P.AsDecimal, Set<FABookHistory.ptdTax179Calculated, decimal0, Set<FABookHistory.ytdTax179Calculated, P.AsDecimal, Set<FABookHistory.ptdBonusCalculated, decimal0, Set<FABookHistory.ytdBonusCalculated, P.AsDecimal, Set<FABookHistory.ptdCalculated, decimal0, Set<FABookHistory.ytdCalculated, P.AsDecimal>>>>>>>>>>, FABookHistory, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookHistory.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FABookHistory.bookID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<FABookHistory.finPeriodID, IBqlString>.IsGreater<P.AsString>>>>.Update((PXGraph) this, new object[8]
    {
      (object) faBookHistory.YtdTax179Taken,
      (object) faBookHistory.YtdBonusTaken,
      (object) faBookHistory.YtdTax179Calculated,
      (object) faBookHistory.YtdBonusCalculated,
      (object) faBookHistory.YtdCalculated,
      (object) faBookHistory.AssetID,
      (object) faBookHistory.BookID,
      (object) maxPeriodID
    });
  }

  protected virtual void FillCalculatedHistory(
    FABookBalance bookBalance,
    IncomingCalculationParameters incomingParameters,
    ICollection<DepreciationMethodBase.FADepreciationScheduleItem> depreciationSchedule,
    ICollection<DepreciationMethodBase.FADepreciationScheduleItem> section179Schedule,
    ICollection<DepreciationMethodBase.FADepreciationScheduleItem> bonusSchedule,
    string maxPeriodID = null)
  {
    if (depreciationSchedule == null || depreciationSchedule.IsEmpty<DepreciationMethodBase.FADepreciationScheduleItem>())
      return;
    if (depreciationSchedule.First<DepreciationMethodBase.FADepreciationScheduleItem>().FinPeriodID != bookBalance.DeprFromPeriod)
      throw new ArgumentException("DeprFromPeriod");
    string minPeriodID = bookBalance.DeprFromPeriod;
    DateTime? receiptDate = incomingParameters.Details.ReceiptDate;
    DateTime? deprFromDate = bookBalance.DeprFromDate;
    if ((receiptDate.HasValue & deprFromDate.HasValue ? (receiptDate.GetValueOrDefault() < deprFromDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      FABookPeriod bookPeriodOfDate = incomingParameters.PeriodDepreciationUtils.FindFABookPeriodOfDate(incomingParameters.Details.ReceiptDate);
      if (string.Compare(bookPeriodOfDate.FinPeriodID, minPeriodID) < 0)
        minPeriodID = bookPeriodOfDate.FinPeriodID;
    }
    if (string.IsNullOrEmpty(maxPeriodID) || string.CompareOrdinal(maxPeriodID, bookBalance.DeprToPeriod) > 0)
      maxPeriodID = bookBalance.DeprToPeriod;
    Dictionary<string, FABookHistory> existingHistory = this.GetExistingHistory(bookBalance, minPeriodID, maxPeriodID);
    this.ClearCalculatedHistoryTails(existingHistory, maxPeriodID);
    DepreciationMethodBase.FADepreciationScheduleItem depreciationScheduleItem1 = depreciationSchedule.First<DepreciationMethodBase.FADepreciationScheduleItem>();
    Decimal depreciationAmount1 = depreciationScheduleItem1.DepreciationAmount;
    Decimal? nullable1 = bookBalance.BonusAmount;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    nullable1 = bookBalance.Tax179Amount;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal num1 = valueOrDefault1 + valueOrDefault2;
    depreciationScheduleItem1.DepreciationAmount = depreciationAmount1 + num1;
    FABookHistory faBookHistory1;
    Decimal? nullable2;
    if (existingHistory.TryGetValue(bookBalance.DeprFromPeriod, out faBookHistory1))
    {
      nullable1 = faBookHistory1.PtdBonusCalculated;
      nullable2 = bookBalance.BonusAmount;
      Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
      if (nullable1.GetValueOrDefault() == valueOrDefault3 & nullable1.HasValue)
      {
        nullable1 = faBookHistory1.PtdTax179Calculated;
        nullable2 = bookBalance.Tax179Amount;
        Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
        if (nullable1.GetValueOrDefault() == valueOrDefault4 & nullable1.HasValue)
          goto label_37;
      }
    }
    FABookHist keyedHistory1 = new FABookHist();
    keyedHistory1.AssetID = bookBalance.AssetID;
    keyedHistory1.BookID = bookBalance.BookID;
    keyedHistory1.FinPeriodID = bookBalance.DeprFromPeriod;
    FABookHist faBookHist1 = FAHelper.InsertFABookHist((PXGraph) this, keyedHistory1, ref bookBalance);
    FABookHist faBookHist2 = faBookHist1;
    nullable1 = faBookHist2.PtdBonusCalculated;
    nullable2 = bookBalance.BonusAmount;
    Decimal valueOrDefault5 = nullable2.GetValueOrDefault();
    Decimal? nullable3;
    if (faBookHistory1 == null)
    {
      nullable2 = new Decimal?();
      nullable3 = nullable2;
    }
    else
      nullable3 = faBookHistory1.PtdBonusCalculated;
    nullable2 = nullable3;
    Decimal valueOrDefault6 = nullable2.GetValueOrDefault();
    Decimal num2 = valueOrDefault5 - valueOrDefault6;
    Decimal? nullable4;
    if (!nullable1.HasValue)
    {
      nullable2 = new Decimal?();
      nullable4 = nullable2;
    }
    else
      nullable4 = new Decimal?(nullable1.GetValueOrDefault() + num2);
    faBookHist2.PtdBonusCalculated = nullable4;
    FABookHist faBookHist3 = faBookHist1;
    nullable1 = faBookHist3.YtdBonusCalculated;
    nullable2 = bookBalance.BonusAmount;
    Decimal valueOrDefault7 = nullable2.GetValueOrDefault();
    Decimal? nullable5;
    if (faBookHistory1 == null)
    {
      nullable2 = new Decimal?();
      nullable5 = nullable2;
    }
    else
      nullable5 = faBookHistory1.PtdBonusCalculated;
    nullable2 = nullable5;
    Decimal valueOrDefault8 = nullable2.GetValueOrDefault();
    Decimal num3 = valueOrDefault7 - valueOrDefault8;
    Decimal? nullable6;
    if (!nullable1.HasValue)
    {
      nullable2 = new Decimal?();
      nullable6 = nullable2;
    }
    else
      nullable6 = new Decimal?(nullable1.GetValueOrDefault() + num3);
    faBookHist3.YtdBonusCalculated = nullable6;
    FABookHist faBookHist4 = faBookHist1;
    nullable1 = faBookHist4.PtdTax179Calculated;
    nullable2 = bookBalance.Tax179Amount;
    Decimal valueOrDefault9 = nullable2.GetValueOrDefault();
    Decimal? nullable7;
    if (faBookHistory1 == null)
    {
      nullable2 = new Decimal?();
      nullable7 = nullable2;
    }
    else
      nullable7 = faBookHistory1.PtdTax179Calculated;
    nullable2 = nullable7;
    Decimal valueOrDefault10 = nullable2.GetValueOrDefault();
    Decimal num4 = valueOrDefault9 - valueOrDefault10;
    Decimal? nullable8;
    if (!nullable1.HasValue)
    {
      nullable2 = new Decimal?();
      nullable8 = nullable2;
    }
    else
      nullable8 = new Decimal?(nullable1.GetValueOrDefault() + num4);
    faBookHist4.PtdTax179Calculated = nullable8;
    FABookHist faBookHist5 = faBookHist1;
    nullable1 = faBookHist5.YtdTax179Calculated;
    nullable2 = bookBalance.Tax179Amount;
    Decimal valueOrDefault11 = nullable2.GetValueOrDefault();
    Decimal? nullable9;
    if (faBookHistory1 == null)
    {
      nullable2 = new Decimal?();
      nullable9 = nullable2;
    }
    else
      nullable9 = faBookHistory1.PtdTax179Calculated;
    nullable2 = nullable9;
    Decimal valueOrDefault12 = nullable2.GetValueOrDefault();
    Decimal num5 = valueOrDefault11 - valueOrDefault12;
    Decimal? nullable10;
    if (!nullable1.HasValue)
    {
      nullable2 = new Decimal?();
      nullable10 = nullable2;
    }
    else
      nullable10 = new Decimal?(nullable1.GetValueOrDefault() + num5);
    faBookHist5.YtdTax179Calculated = nullable10;
label_37:
    IDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem> dictionary1 = (IDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>) depreciationSchedule.ToDictionary<DepreciationMethodBase.FADepreciationScheduleItem, string, DepreciationMethodBase.FADepreciationScheduleItem>((Func<DepreciationMethodBase.FADepreciationScheduleItem, string>) (item => item.FinPeriodID), (Func<DepreciationMethodBase.FADepreciationScheduleItem, DepreciationMethodBase.FADepreciationScheduleItem>) (item => item));
    IDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem> dictionary2 = (IDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>) section179Schedule.ToDictionary<DepreciationMethodBase.FADepreciationScheduleItem, string, DepreciationMethodBase.FADepreciationScheduleItem>((Func<DepreciationMethodBase.FADepreciationScheduleItem, string>) (item => item.FinPeriodID), (Func<DepreciationMethodBase.FADepreciationScheduleItem, DepreciationMethodBase.FADepreciationScheduleItem>) (item => item));
    IDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem> dictionary3 = (IDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>) bonusSchedule.ToDictionary<DepreciationMethodBase.FADepreciationScheduleItem, string, DepreciationMethodBase.FADepreciationScheduleItem>((Func<DepreciationMethodBase.FADepreciationScheduleItem, string>) (item => item.FinPeriodID), (Func<DepreciationMethodBase.FADepreciationScheduleItem, DepreciationMethodBase.FADepreciationScheduleItem>) (item => item));
    foreach (string str in incomingParameters.PeriodDepreciationUtils.BookPeriods.Keys.Where<string>((Func<string, bool>) (p => string.Compare(p, minPeriodID) >= 0 && string.Compare(p, maxPeriodID) <= 0)))
    {
      FABookHistory faBookHistory2;
      existingHistory.TryGetValue(str, out faBookHistory2);
      if (existingHistory != null || string.Compare(str, bookBalance.DeprFromPeriod) >= 0)
      {
        Decimal? nullable11;
        if (faBookHistory2 == null)
        {
          nullable1 = new Decimal?();
          nullable11 = nullable1;
        }
        else
          nullable11 = faBookHistory2.PtdCalculated;
        nullable1 = nullable11;
        Decimal valueOrDefault13 = nullable1.GetValueOrDefault();
        Decimal? nullable12;
        if (faBookHistory2 == null)
        {
          nullable1 = new Decimal?();
          nullable12 = nullable1;
        }
        else
          nullable12 = faBookHistory2.PtdTax179Taken;
        nullable1 = nullable12;
        Decimal valueOrDefault14 = nullable1.GetValueOrDefault();
        Decimal? nullable13;
        if (faBookHistory2 == null)
        {
          nullable1 = new Decimal?();
          nullable13 = nullable1;
        }
        else
          nullable13 = faBookHistory2.PtdBonusTaken;
        nullable1 = nullable13;
        Decimal valueOrDefault15 = nullable1.GetValueOrDefault();
        DepreciationMethodBase.FADepreciationScheduleItem depreciationScheduleItem2;
        dictionary1.TryGetValue(str, out depreciationScheduleItem2);
        DepreciationMethodBase.FADepreciationScheduleItem depreciationScheduleItem3;
        dictionary2.TryGetValue(str, out depreciationScheduleItem3);
        DepreciationMethodBase.FADepreciationScheduleItem depreciationScheduleItem4;
        dictionary3.TryGetValue(str, out depreciationScheduleItem4);
        Decimal depreciationAmount2 = depreciationScheduleItem2 != null ? depreciationScheduleItem2.DepreciationAmount : 0M;
        Decimal depreciationAmount3 = depreciationScheduleItem3 != null ? depreciationScheduleItem3.DepreciationAmount : 0M;
        Decimal depreciationAmount4 = depreciationScheduleItem4 != null ? depreciationScheduleItem4.DepreciationAmount : 0M;
        if (faBookHistory2 == null || valueOrDefault13 != depreciationAmount2 || valueOrDefault14 != depreciationAmount3 || valueOrDefault15 != depreciationAmount4)
        {
          FABookHist keyedHistory2 = new FABookHist();
          keyedHistory2.AssetID = bookBalance.AssetID;
          keyedHistory2.BookID = bookBalance.BookID;
          keyedHistory2.FinPeriodID = str;
          FABookHist faBookHist6 = FAHelper.InsertFABookHist((PXGraph) this, keyedHistory2, ref bookBalance);
          FABookHist faBookHist7 = faBookHist6;
          nullable1 = faBookHist7.PtdCalculated;
          Decimal num6 = depreciationAmount2 - valueOrDefault13;
          Decimal? nullable14;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable14 = nullable2;
          }
          else
            nullable14 = new Decimal?(nullable1.GetValueOrDefault() + num6);
          faBookHist7.PtdCalculated = nullable14;
          FABookHist faBookHist8 = faBookHist6;
          nullable1 = faBookHist8.YtdCalculated;
          Decimal num7 = depreciationAmount2 - valueOrDefault13;
          Decimal? nullable15;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable15 = nullable2;
          }
          else
            nullable15 = new Decimal?(nullable1.GetValueOrDefault() + num7);
          faBookHist8.YtdCalculated = nullable15;
          FABookHist faBookHist9 = faBookHist6;
          nullable1 = faBookHist9.PtdTax179Taken;
          Decimal num8 = depreciationAmount3 - valueOrDefault14;
          Decimal? nullable16;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable16 = nullable2;
          }
          else
            nullable16 = new Decimal?(nullable1.GetValueOrDefault() + num8);
          faBookHist9.PtdTax179Taken = nullable16;
          FABookHist faBookHist10 = faBookHist6;
          nullable1 = faBookHist10.YtdTax179Taken;
          Decimal num9 = depreciationAmount3 - valueOrDefault14;
          Decimal? nullable17;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable17 = nullable2;
          }
          else
            nullable17 = new Decimal?(nullable1.GetValueOrDefault() + num9);
          faBookHist10.YtdTax179Taken = nullable17;
          FABookHist faBookHist11 = faBookHist6;
          nullable1 = faBookHist11.PtdBonusTaken;
          Decimal num10 = depreciationAmount4 - valueOrDefault15;
          Decimal? nullable18;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable18 = nullable2;
          }
          else
            nullable18 = new Decimal?(nullable1.GetValueOrDefault() + num10);
          faBookHist11.PtdBonusTaken = nullable18;
          FABookHist faBookHist12 = faBookHist6;
          nullable1 = faBookHist12.YtdBonusTaken;
          Decimal num11 = depreciationAmount4 - valueOrDefault15;
          Decimal? nullable19;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable19 = nullable2;
          }
          else
            nullable19 = new Decimal?(nullable1.GetValueOrDefault() + num11);
          faBookHist12.YtdBonusTaken = nullable19;
        }
      }
    }
  }

  public virtual void CheckBalance(FABookBalance bookBalance)
  {
    if (bookBalance.Depreciate.GetValueOrDefault() && (string.IsNullOrEmpty(bookBalance.DeprFromPeriod) || string.IsNullOrEmpty(bookBalance.DeprToPeriod) || string.CompareOrdinal(bookBalance.DeprFromPeriod, bookBalance.DeprToPeriod) > 0))
      throw new PXException("Incorrect periods beginning and end of depreciation for Book '{0}'", new object[1]
      {
        (object) PXResultset<FABook>.op_Implicit(PXSelectBase<FABook, PXSelect<FABook, Where<FABook.bookID, Equal<Current<FABookBalance.bookID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
        {
          (object) bookBalance
        }, Array.Empty<object>())).BookCode
      });
  }

  public enum FADestination
  {
    Calculated,
    Tax179,
    Bonus,
  }
}
