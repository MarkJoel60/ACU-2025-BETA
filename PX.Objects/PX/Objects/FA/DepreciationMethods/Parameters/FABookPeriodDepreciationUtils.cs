// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationMethods.Parameters.FABookPeriodDepreciationUtils
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FA.DepreciationMethods.Parameters;

/// <exclude />
public class FABookPeriodDepreciationUtils
{
  public SortedDictionary<int, string> BookPeriodsIDByIndex;
  public SortedDictionary<string, int> BookPeriodsIndexByID;
  public SortedDictionary<string, FABookPeriod> BookPeriods;
  public IYearSetup YearSetup;

  private IncomingCalculationParameters IncomingParameters { get; }

  public FABookPeriodDepreciationUtils(IncomingCalculationParameters incomingParameters)
  {
    this.IncomingParameters = incomingParameters;
    this.FillBookPeriodsCollection();
    this.YearSetup = this.IncomingParameters.RepositoryHelper.FindFABookYearSetup(this.IncomingParameters.BookID);
  }

  private void FillBookPeriodsCollection()
  {
    this.BookPeriodsIDByIndex = new SortedDictionary<int, string>();
    this.BookPeriodsIndexByID = new SortedDictionary<string, int>();
    this.BookPeriods = new SortedDictionary<string, FABookPeriod>();
    IEnumerable<FABookPeriod> faBookPeriods = GraphHelper.RowCast<FABookPeriod>((IEnumerable) PXSelectBase<FABookPeriod, PXViewOf<FABookPeriod>.BasedOn<SelectFromBase<FABookPeriod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.organizationID, Equal<P.AsInt>>>>, And<BqlOperand<FABookPeriod.bookID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<FABookPeriod.startDate, IBqlDateTime>.IsNotEqual<FABookPeriod.endDate>>>.Order<By<BqlField<FABookPeriod.finPeriodID, IBqlString>.Asc>>>.Config>.Select(this.IncomingParameters.Graph, new object[2]
    {
      (object) this.IncomingParameters.OrganizationID,
      (object) this.IncomingParameters.BookID
    }));
    int key = 0;
    foreach (FABookPeriod faBookPeriod in faBookPeriods)
    {
      this.BookPeriods.Add(faBookPeriod.FinPeriodID, faBookPeriod);
      this.BookPeriodsIDByIndex.Add(key, faBookPeriod.FinPeriodID);
      this.BookPeriodsIndexByID.Add(faBookPeriod.FinPeriodID, key);
      ++key;
    }
  }

  public int GetNumberOfPeriodsBetweenPeriods(string periodID1, string periodID2)
  {
    return this.BookPeriodsIndexByID[periodID1] - this.BookPeriodsIndexByID[periodID2];
  }

  public string AddPeriodsCountToPeriod(string periodID, int offset)
  {
    return this.BookPeriodsIDByIndex[this.BookPeriodsIndexByID[periodID] + offset];
  }

  public IEnumerable<string> GetPeriods(string firstPeriodID, string lastPeriodID)
  {
    return this.BookPeriodsIDByIndex.Where<KeyValuePair<int, string>>((Func<KeyValuePair<int, string>, bool>) (period => string.CompareOrdinal(period.Value, firstPeriodID) >= 0 && string.CompareOrdinal(period.Value, lastPeriodID) <= 0)).Select<KeyValuePair<int, string>, string>((Func<KeyValuePair<int, string>, string>) (period => period.Value));
  }

  public FABookPeriod FindFABookPeriodOfDate(DateTime? date)
  {
    if (!date.HasValue)
      throw new ArgumentNullException(nameof (date));
    return this.BookPeriods.Values.Where<FABookPeriod>((Func<FABookPeriod, bool>) (period =>
    {
      DateTime? startDate = period.StartDate;
      DateTime? nullable1 = date;
      if ((startDate.HasValue & nullable1.HasValue ? (startDate.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return false;
      nullable1 = period.EndDate;
      DateTime? nullable2 = date;
      return nullable1.HasValue & nullable2.HasValue && nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault();
    })).FirstOrDefault<FABookPeriod>();
  }

  public string GetLastFinPeriodIdOfFinYear(string finYear)
  {
    return this.BookPeriods.LastOrDefault<KeyValuePair<string, FABookPeriod>>((Func<KeyValuePair<string, FABookPeriod>, bool>) (a => a.Value.FinYear == finYear)).Key;
  }
}
