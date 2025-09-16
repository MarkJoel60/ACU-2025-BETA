// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationMethods.AustralianPrimeCostMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.FA.DepreciationMethods.Parameters;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA.DepreciationMethods;

public class AustralianPrimeCostMethod : DepreciationMethodBase
{
  private const int DaysInYear = 365;

  protected override string CalculationMethod => "PC";

  protected override string[] ApplicableAveragingConventions { get; }

  protected override SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> Calculate()
  {
    SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> sortedDictionary1 = new SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>>();
    foreach (FAAddition addition in this.CalculationParameters.Additions)
    {
      SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem> sortedDictionary2 = new SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>();
      AustralianPrimeCostAdditionParameters additionParameters = this.CalculateAdditionParameters(this.CalculationParameters, addition);
      string period = this.IncomingParameters.PeriodDepreciationUtils.AddPeriodsCountToPeriod(additionParameters.DepreciateToPeriodID, this.IncomingParameters.SuspendedPeriodsIDs.Count);
      List<string> additionPeriods = this.GetAdditionPeriods(additionParameters.DepreciateFromPeriodID, period);
      Decimal num1 = 0M;
      foreach (string str in additionPeriods)
      {
        FABookPeriod bookPeriod = this.IncomingParameters.PeriodDepreciationUtils.BookPeriods[str];
        double num2 = !(str == additionParameters.DepreciateFromPeriodID) ? (bookPeriod.EndDate.Value - bookPeriod.StartDate.Value).TotalDays : (additionParameters.DepreciateFromPeriod.EndDate.Value - additionParameters.DepreciateFromDate).TotalDays;
        Decimal num3 = additionParameters.DepreciationBasis * additionParameters.PercentPerYear * (Decimal) num2 / 365M / 100M;
        if (str == additionParameters.DepreciateToPeriodID)
          num3 = additionParameters.DepreciationBasis - num1;
        if (this.IncomingParameters.SuspendedPeriodsIDs.Contains(str))
          num3 = 0M;
        num1 += num3;
        if (string.CompareOrdinal(str, this.CalculationParameters.MaxDepreciateToPeriodID) <= 0)
        {
          DepreciationMethodBase.FADepreciationScheduleItem depreciationScheduleItem;
          if (sortedDictionary2.TryGetValue(str, out depreciationScheduleItem))
            depreciationScheduleItem.DepreciationAmount += num3;
          else
            sortedDictionary2[str] = new DepreciationMethodBase.FADepreciationScheduleItem()
            {
              FinPeriodID = str,
              DepreciationAmount = num3
            };
        }
        else
          break;
      }
      sortedDictionary1[addition.PeriodID] = sortedDictionary2;
    }
    return sortedDictionary1;
  }

  private AustralianPrimeCostAdditionParameters CalculateAdditionParameters(
    CalculationParameters calculationData,
    FAAddition addition)
  {
    FABookBalance bookBalance = calculationData.BookBalance;
    this.CheckParametersContracts();
    int assetId = calculationData.AssetID;
    int bookId = calculationData.BookID;
    AustralianPrimeCostAdditionParameters additionParameters1 = new AustralianPrimeCostAdditionParameters();
    additionParameters1.DepreciationBasis = addition.DepreciationBasis;
    additionParameters1.PlacedInServiceDate = bookBalance.DeprFromDate.Value;
    additionParameters1.PercentPerYear = bookBalance.PercentPerYear.Value;
    AustralianPrimeCostAdditionParameters additionParameters2 = additionParameters1;
    addition.CalculatedAdditionParameters = (AdditionParameters) additionParameters2;
    FABookPeriod bookPeriodOfDate = this.IncomingParameters.RepositoryHelper.FindFABookPeriodOfDate(new DateTime?(addition.IsOriginal ? additionParameters2.PlacedInServiceDate : addition.Date), new int?(bookId), new int?(assetId));
    additionParameters2.DepreciateFromDate = additionParameters2.PlacedInServiceDate;
    additionParameters2.DepreciateFromPeriodID = bookPeriodOfDate.FinPeriodID;
    additionParameters2.DepreciateToDate = DeprCalcParameters.GetDatePlusYears(additionParameters2.DepreciateFromDate, bookBalance.UsefulLife.Value);
    additionParameters2.DepreciateToPeriodID = this.IncomingParameters.RepositoryHelper.GetFABookPeriodIDOfDate(new DateTime?(additionParameters2.DepreciateToDate), new int?(bookId), new int?(assetId));
    additionParameters2.DepreciateFromPeriod = this.IncomingParameters.RepositoryHelper.FindByKey(new int?(bookId), this.IncomingParameters.OrganizationID, additionParameters2.DepreciateFromPeriodID);
    additionParameters2.DepreciateToPeriod = this.IncomingParameters.RepositoryHelper.FindByKey(new int?(bookId), this.IncomingParameters.OrganizationID, additionParameters2.DepreciateToPeriodID);
    AustralianPrimeCostAdditionParameters additionParameters3 = additionParameters2;
    DateTime? endDate = additionParameters2.DepreciateFromPeriod.EndDate;
    DateTime? startDate = additionParameters2.DepreciateFromPeriod.StartDate;
    TimeSpan timeSpan = (endDate.HasValue & startDate.HasValue ? new TimeSpan?(endDate.GetValueOrDefault() - startDate.GetValueOrDefault()) : new TimeSpan?()).Value;
    double totalDays1 = timeSpan.TotalDays;
    additionParameters3.TotalDaysInFromPeriod = totalDays1;
    AustralianPrimeCostAdditionParameters additionParameters4 = additionParameters2;
    endDate = additionParameters2.DepreciateFromPeriod.EndDate;
    timeSpan = endDate.Value - additionParameters2.DepreciateFromDate;
    double totalDays2 = timeSpan.TotalDays;
    additionParameters4.DaysHeldInFromPeriod = totalDays2;
    endDate = additionParameters2.DepreciateToPeriod.EndDate;
    DateTime dateTime = endDate.Value.AddDays(-1.0);
    AustralianPrimeCostAdditionParameters additionParameters5 = additionParameters2;
    endDate = additionParameters2.DepreciateToPeriod.EndDate;
    startDate = additionParameters2.DepreciateToPeriod.StartDate;
    timeSpan = (endDate.HasValue & startDate.HasValue ? new TimeSpan?(endDate.GetValueOrDefault() - startDate.GetValueOrDefault()) : new TimeSpan?()).Value;
    double totalDays3 = timeSpan.TotalDays;
    additionParameters5.TotalDaysInToPeriod = totalDays3;
    AustralianPrimeCostAdditionParameters additionParameters6 = additionParameters2;
    double totalDaysInToPeriod = additionParameters2.TotalDaysInToPeriod;
    timeSpan = dateTime - additionParameters2.DepreciateToDate;
    double totalDays4 = timeSpan.TotalDays;
    double num = totalDaysInToPeriod - totalDays4;
    additionParameters6.DaysHeldInToPeriod = num;
    return additionParameters2;
  }

  protected void CheckParametersContracts()
  {
    FABookBalance bookBalance = this.CalculationParameters.BookBalance;
    if (bookBalance == null)
      throw new ArgumentNullException("BookBalance");
    if (!bookBalance.DeprFromDate.HasValue)
      throw new ArgumentNullException("DeprFromDate");
    if (!bookBalance.UsefulLife.HasValue)
      throw new ArgumentNullException("UsefulLife");
  }

  protected override void ApplyDispose(
    SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> schedules)
  {
    if (!this.IncomingParameters.Details.DisposalDate.HasValue)
      return;
    FABookPeriod bookPeriodOfDate = this.IncomingParameters.PeriodDepreciationUtils.FindFABookPeriodOfDate(this.IncomingParameters.Details.DisposalDate);
    if (this.CalculationParameters.MaxDepreciateToPeriodID != bookPeriodOfDate.FinPeriodID)
      return;
    foreach (FAAddition addition in this.CalculationParameters.Additions)
    {
      SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem> schedule = schedules[addition.PeriodID];
      AustralianPrimeCostAdditionParameters additionParameters = addition.CalculatedAdditionParameters as AustralianPrimeCostAdditionParameters;
      string finPeriodId = bookPeriodOfDate.FinPeriodID;
      DepreciationMethodBase.FADepreciationScheduleItem depreciationScheduleItem = schedule[finPeriodId];
      DateTime? nullable = bookPeriodOfDate.EndDate;
      DateTime dateTime1 = nullable.Value.AddDays(-1.0);
      nullable = bookPeriodOfDate.EndDate;
      DateTime? startDate = bookPeriodOfDate.StartDate;
      TimeSpan timeSpan = (nullable.HasValue & startDate.HasValue ? new TimeSpan?(nullable.GetValueOrDefault() - startDate.GetValueOrDefault()) : new TimeSpan?()).Value;
      double totalDays1 = timeSpan.TotalDays;
      DateTime dateTime2 = dateTime1;
      nullable = this.IncomingParameters.Details.DisposalDate;
      DateTime dateTime3 = nullable.Value;
      timeSpan = dateTime2 - dateTime3;
      double totalDays2 = timeSpan.TotalDays;
      double num1 = totalDays1 - totalDays2;
      Decimal num2 = additionParameters.DepreciationBasis * (additionParameters.PercentPerYear / 100M) * ((Decimal) num1 / 365M);
      depreciationScheduleItem.DepreciationAmount = num2;
    }
  }

  protected override void ApplySuspend(
    SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> schedules)
  {
  }
}
