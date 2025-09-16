// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationMethods.DutchMethod2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.FA.DepreciationMethods.Parameters;
using PX.Objects.GL;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA.DepreciationMethods;

public class DutchMethod2 : DepreciationMethodBase
{
  protected override string CalculationMethod => "N2";

  protected override string[] ApplicableAveragingConventions { get; } = new string[1]
  {
    "FP"
  };

  protected override void ApplyDispose(
    SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> schedules)
  {
  }

  protected override SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> Calculate()
  {
    SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> sortedDictionary1 = new SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>>();
    foreach (FAAddition addition in this.CalculationParameters.Additions)
    {
      SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem> sortedDictionary2 = new SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>();
      AdditionParameters additionParameters = this.CalculateAdditionParameters(this.CalculationParameters, addition);
      string period = this.IncomingParameters.PeriodDepreciationUtils.AddPeriodsCountToPeriod(additionParameters.DepreciateToPeriodID, this.IncomingParameters.SuspendedPeriodsIDs.Count);
      List<string> additionPeriods = this.GetAdditionPeriods(additionParameters.DepreciateFromPeriodID, period);
      Decimal num1 = 0M;
      Decimal num2 = 0M;
      Decimal num3 = additionParameters.DepreciationBasis;
      Decimal num4 = 0M;
      DutchMethod2.FinYearInfo finYearInfo1 = (DutchMethod2.FinYearInfo) null;
      IYearSetup yearSetup = this.IncomingParameters.PeriodDepreciationUtils.YearSetup;
      Dictionary<string, DutchMethod2.FinYearInfo> finYearInfo2 = this.GetFinYearInfo(additionPeriods, yearSetup);
      foreach (string str in additionPeriods)
      {
        if (string.CompareOrdinal(str, this.CalculationParameters.MaxDepreciateToPeriodID) <= 0)
        {
          FABookPeriod bookPeriod = this.IncomingParameters.PeriodDepreciationUtils.BookPeriods[str];
          if ((finYearInfo1 == null || finYearInfo1.FinYear != bookPeriod.FinYear) && finYearInfo2.TryGetValue(bookPeriod.FinYear, out finYearInfo1))
          {
            Decimal? percentPerYear = this.IncomingParameters.Method.PercentPerYear;
            Decimal num5 = (Decimal) 100;
            num4 = (Decimal) (1.0 - Math.Pow(1.0 - (double) (percentPerYear.HasValue ? new Decimal?(percentPerYear.GetValueOrDefault() / num5) : new Decimal?()).Value, (double) (1M / finYearInfo1.NumberOfPeriodsInFinYear)));
            num3 = additionParameters.DepreciationBasis - num1;
            num2 = 0M;
          }
          Decimal num6 = !this.IncomingParameters.Method.YearlyAccountancy.GetValueOrDefault() ? (num3 - num2) * num4 : num3 * (this.IncomingParameters.Method.PercentPerYear.Value / 100M) / finYearInfo1.NumberOfPeriodsInFinYear;
          if (this.IncomingParameters.SuspendedPeriodsIDs.Contains(str))
            num6 = 0M;
          num2 += num6;
          num1 += num6;
          DepreciationMethodBase.FADepreciationScheduleItem depreciationScheduleItem;
          if (sortedDictionary2.TryGetValue(str, out depreciationScheduleItem))
            depreciationScheduleItem.DepreciationAmount += num6;
          else
            sortedDictionary2[str] = new DepreciationMethodBase.FADepreciationScheduleItem()
            {
              FinPeriodID = str,
              DepreciationAmount = num6
            };
        }
        else
          break;
      }
      sortedDictionary1[addition.PeriodID] = sortedDictionary2;
    }
    return sortedDictionary1;
  }

  private Dictionary<string, DutchMethod2.FinYearInfo> GetFinYearInfo(
    List<string> periods,
    IYearSetup yearSetup)
  {
    Dictionary<string, DutchMethod2.FinYearInfo> finYearInfo = new Dictionary<string, DutchMethod2.FinYearInfo>();
    foreach (string period in periods)
    {
      FABookPeriod bookPeriod = this.IncomingParameters.PeriodDepreciationUtils.BookPeriods[period];
      if (!finYearInfo.ContainsKey(bookPeriod.FinYear))
        finYearInfo[bookPeriod.FinYear] = new DutchMethod2.FinYearInfo()
        {
          FinYear = bookPeriod.FinYear,
          NumberOfPeriodsInFinYear = (Decimal) this.GetNumberOfPeriods(yearSetup)
        };
    }
    return finYearInfo;
  }

  protected override void ApplySuspend(
    SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> schedules)
  {
  }

  private AdditionParameters CalculateAdditionParameters(
    CalculationParameters calculationData,
    FAAddition addition)
  {
    FABookBalance bookBalance = calculationData.BookBalance;
    this.CheckParametersContracts();
    int assetId = calculationData.AssetID;
    int bookId = calculationData.BookID;
    AdditionParameters additionParameters = new AdditionParameters()
    {
      DepreciationBasis = addition.Amount * addition.BusinessUse - addition.Section179Amount - addition.BonusAmount - addition.SalvageAmount
    };
    addition.CalculatedAdditionParameters = additionParameters;
    FABookPeriod bookPeriodOfDate1 = this.IncomingParameters.RepositoryHelper.FindFABookPeriodOfDate(new DateTime?(addition.Date), new int?(bookId), new int?(assetId));
    FABookPeriod bookPeriodOfDate2 = this.IncomingParameters.RepositoryHelper.FindFABookPeriodOfDate(new DateTime?(bookBalance.DeprFromDate.Value), new int?(bookId), new int?(assetId));
    additionParameters.DepreciateFromDate = bookPeriodOfDate1.StartDate.Value;
    additionParameters.DepreciateFromPeriodID = bookPeriodOfDate1.FinPeriodID;
    additionParameters.DepreciateToDate = DeprCalcParameters.GetDatePlusYears(bookPeriodOfDate2.StartDate.Value, bookBalance.UsefulLife.Value);
    additionParameters.DepreciateToPeriodID = this.IncomingParameters.RepositoryHelper.GetFABookPeriodIDOfDate(new DateTime?(additionParameters.DepreciateToDate), new int?(bookId), new int?(assetId));
    return additionParameters;
  }

  protected void CheckParametersContracts()
  {
    FABookBalance bookBalance = this.CalculationParameters.BookBalance;
    if (bookBalance == null)
      throw new ArgumentNullException("BookBalance");
    if (!bookBalance.DeprFromDate.HasValue)
      throw new ArgumentNullException("DeprFromDate");
    if (!bookBalance.BusinessUse.HasValue)
      throw new ArgumentNullException("BusinessUse");
    if (!bookBalance.SalvageAmount.HasValue)
      throw new ArgumentNullException("SalvageAmount");
    if (!bookBalance.Tax179Amount.HasValue)
      throw new ArgumentNullException("Tax179Amount");
    if (!bookBalance.BonusAmount.HasValue)
      throw new ArgumentNullException("BonusAmount");
  }

  public class FinYearInfo
  {
    public string FinYear { get; set; }

    public Decimal NumberOfPeriodsInFinYear { get; set; }
  }
}
