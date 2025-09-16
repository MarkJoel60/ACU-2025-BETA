// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationMethods.NewZealandDiminishingValueMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.FA.DepreciationMethods.Parameters;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FA.DepreciationMethods;

public class NewZealandDiminishingValueMethod : NewZealandMethodBase
{
  protected override string CalculationMethod => "ZD";

  protected override string[] ApplicableAveragingConventions { get; }

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
      Decimal num5 = 0M;
      Decimal num6 = 0M;
      NewZealandDiminishingValueMethod.FinYearInfo finYearInfo1 = (NewZealandDiminishingValueMethod.FinYearInfo) null;
      IYearSetup yearSetup = this.IncomingParameters.PeriodDepreciationUtils.YearSetup;
      Dictionary<string, NewZealandDiminishingValueMethod.FinYearInfo> finYearInfo2 = this.GetFinYearInfo(additionPeriods, yearSetup);
      this.RecalculateYearInfoForDisposalPeriod(finYearInfo2, additionPeriods.Last<string>(), yearSetup);
      foreach (string str in additionPeriods)
      {
        if (string.CompareOrdinal(str, this.CalculationParameters.MaxDepreciateToPeriodID) <= 0)
        {
          FABookPeriod bookPeriod = this.IncomingParameters.PeriodDepreciationUtils.BookPeriods[str];
          if ((finYearInfo1 == null || finYearInfo1.FinYear != bookPeriod.FinYear) && finYearInfo2.TryGetValue(bookPeriod.FinYear, out finYearInfo1))
          {
            num4 = finYearInfo1.NumberOfDaysHeldInFinYear;
            num5 = finYearInfo1.NumberOfPeriodsInFinYear;
            num6 = finYearInfo1.NumberOfPeriodsHeldInFinYear;
          }
          if (this.IsFirstPeriod(str))
          {
            num2 = 0M;
            num3 = additionParameters.DepreciationBasis - num1;
          }
          Decimal daysHeldInPeriod = this.GetDaysHeldInPeriod(bookPeriod, yearSetup);
          Decimal num7 = !(str == finYearInfo1.LastFinPeriodOfFinYear) ? num3 * this.CalculationParameters.BookBalance.PercentPerYear.Value / 100M * (num6 / num5) * (daysHeldInPeriod / num4) : num3 * this.CalculationParameters.BookBalance.PercentPerYear.Value / 100M * (num6 / num5) - num2;
          if (this.IncomingParameters.SuspendedPeriodsIDs.Contains(str))
          {
            num2 += num7;
            num7 = 0M;
          }
          num1 += num7;
          num2 += num7;
          DepreciationMethodBase.FADepreciationScheduleItem depreciationScheduleItem;
          if (sortedDictionary2.TryGetValue(str, out depreciationScheduleItem))
            depreciationScheduleItem.DepreciationAmount += num7;
          else
            sortedDictionary2[str] = new DepreciationMethodBase.FADepreciationScheduleItem()
            {
              FinPeriodID = str,
              DepreciationAmount = num7
            };
        }
        else
          break;
      }
      sortedDictionary1[addition.PeriodID] = sortedDictionary2;
    }
    return sortedDictionary1;
  }

  private Dictionary<string, NewZealandDiminishingValueMethod.FinYearInfo> GetFinYearInfo(
    List<string> periods,
    IYearSetup yearSetup)
  {
    Dictionary<string, NewZealandDiminishingValueMethod.FinYearInfo> finYearInfo1 = new Dictionary<string, NewZealandDiminishingValueMethod.FinYearInfo>();
    foreach (string period in periods)
    {
      FABookPeriod bookPeriod = this.IncomingParameters.PeriodDepreciationUtils.BookPeriods[period];
      NewZealandDiminishingValueMethod.FinYearInfo finYearInfo2;
      if (finYearInfo1.TryGetValue(bookPeriod.FinYear, out finYearInfo2))
      {
        finYearInfo2.NumberOfDaysHeldInFinYear += this.GetDaysHeldInPeriod(bookPeriod, yearSetup);
        finYearInfo2.NumberOfPeriodsHeldInFinYear += 1M;
      }
      else
        finYearInfo1[bookPeriod.FinYear] = new NewZealandDiminishingValueMethod.FinYearInfo()
        {
          FinYear = bookPeriod.FinYear,
          LastFinPeriodOfFinYear = this.IncomingParameters.PeriodDepreciationUtils.GetLastFinPeriodIdOfFinYear(bookPeriod.FinYear),
          NumberOfPeriodsInFinYear = (Decimal) this.GetNumberOfPeriods(yearSetup),
          NumberOfPeriodsHeldInFinYear = 1M,
          NumberOfDaysHeldInFinYear = this.GetDaysHeldInPeriod(bookPeriod, yearSetup)
        };
    }
    return finYearInfo1;
  }

  private void RecalculateYearInfoForDisposalPeriod(
    Dictionary<string, NewZealandDiminishingValueMethod.FinYearInfo> map,
    string depriciateToPeriod,
    IYearSetup yearSetup)
  {
    if (!this.IncomingParameters.Details.DisposalDate.HasValue)
      return;
    FABookPeriod bookPeriodOfDate = this.IncomingParameters.PeriodDepreciationUtils.FindFABookPeriodOfDate(this.IncomingParameters.Details.DisposalDate);
    string periodIdOfFinYear = this.IncomingParameters.PeriodDepreciationUtils.GetLastFinPeriodIdOfFinYear(bookPeriodOfDate.FinYear);
    List<string> additionPeriods = this.GetAdditionPeriods(FinPeriodUtils.GetFirstFinPeriodIDOfYear(bookPeriodOfDate.FinPeriodID), string.CompareOrdinal(periodIdOfFinYear, depriciateToPeriod) < 0 ? periodIdOfFinYear : depriciateToPeriod);
    NewZealandDiminishingValueMethod.FinYearInfo finYearInfo;
    if (!map.TryGetValue(bookPeriodOfDate.FinYear, out finYearInfo))
      return;
    finYearInfo.NumberOfDaysHeldInFinYear = 0M;
    finYearInfo.NumberOfPeriodsHeldInFinYear = 0M;
    foreach (string key in additionPeriods)
    {
      FABookPeriod bookPeriod = this.IncomingParameters.PeriodDepreciationUtils.BookPeriods[key];
      finYearInfo.NumberOfDaysHeldInFinYear += this.GetDaysHeldInPeriod(bookPeriod, yearSetup);
      finYearInfo.NumberOfPeriodsHeldInFinYear += 1M;
    }
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
      DepreciationBasis = addition.Amount * addition.BusinessUse
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
    if (!bookBalance.PercentPerYear.HasValue)
      throw new ArgumentNullException("PercentPerYear");
  }

  public class FinYearInfo
  {
    public string FinYear { get; set; }

    public string LastFinPeriodOfFinYear { get; set; }

    public Decimal NumberOfPeriodsInFinYear { get; set; }

    public Decimal NumberOfPeriodsHeldInFinYear { get; set; }

    public Decimal NumberOfDaysHeldInFinYear { get; set; }
  }
}
