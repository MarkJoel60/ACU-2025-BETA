// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationMethods.StraightLineFullDayMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.FA.DepreciationMethods.Parameters;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA.DepreciationMethods;

/// <summary>
/// Class implements calculation logic for Strainght Line calculation method with Full Day averaging convention
/// as described in https://wiki.acumatica.com/display/SPEC/AC-143586:+Straight+Line+method%2C+Full+Day+averaging+convention specification
/// </summary>
public class StraightLineFullDayMethod : StraightLineFullPeriodMethod
{
  protected override string[] ApplicableAveragingConventions { get; } = new string[1]
  {
    "FD"
  };

  protected override SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> Calculate()
  {
    SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> sortedDictionary1 = new SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>>();
    double num1 = 0.0;
    foreach (FAAddition addition in this.CalculationParameters.Additions)
    {
      SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem> sortedDictionary2 = new SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>();
      SLMethodFullDayAdditionParameters additionParameters = this.CalculateAdditionParameters(this.CalculationParameters, addition);
      List<string> additionPeriods = this.GetAdditionPeriods(additionParameters.DepreciateFromPeriodID, additionParameters.DepreciateToPeriodID);
      if (additionParameters.IsFirstAddition)
        num1 = (double) additionPeriods.Count - (additionParameters.TotalDaysInFromPeriod - additionParameters.DaysHeldInFromPeriod) / additionParameters.TotalDaysInFromPeriod - (additionParameters.TotalDaysInToPeriod - additionParameters.DaysHeldInToPeriod) / additionParameters.TotalDaysInToPeriod;
      Decimal num2 = 0M;
      Decimal num3 = additionParameters.DepreciationBasis / (Decimal) num1;
      foreach (string str in additionPeriods)
      {
        Decimal num4 = num3;
        if (str == additionParameters.DepreciateFromPeriodID && !additionParameters.IsFirstPeriodFull)
          num4 = additionParameters.DepreciationBasis / (Decimal) num1 * (Decimal) (additionParameters.DaysHeldInFromPeriod / additionParameters.TotalDaysInFromPeriod);
        if (str == additionParameters.DepreciateToPeriodID)
          num4 = additionParameters.DepreciationBasis - num2;
        num2 += num4;
        if (string.CompareOrdinal(str, this.CalculationParameters.MaxDepreciateToPeriodID) <= 0)
        {
          DepreciationMethodBase.FADepreciationScheduleItem depreciationScheduleItem;
          if (sortedDictionary2.TryGetValue(str, out depreciationScheduleItem))
            depreciationScheduleItem.DepreciationAmount += num4;
          else
            sortedDictionary2[str] = new DepreciationMethodBase.FADepreciationScheduleItem()
            {
              FinPeriodID = str,
              DepreciationAmount = num4
            };
        }
        else
          break;
      }
      sortedDictionary1[addition.PeriodID] = sortedDictionary2;
    }
    return sortedDictionary1;
  }

  private SLMethodFullDayAdditionParameters CalculateAdditionParameters(
    CalculationParameters calculationData,
    FAAddition addition)
  {
    FABookBalance bookBalance = calculationData.BookBalance;
    this.CheckParametersContracts();
    int assetId = calculationData.AssetID;
    int bookId = calculationData.BookID;
    SLMethodFullDayAdditionParameters additionParameters1 = new SLMethodFullDayAdditionParameters();
    additionParameters1.DepreciationBasis = addition.DepreciationBasis;
    additionParameters1.PlacedInServiceDate = bookBalance.DeprFromDate.Value;
    SLMethodFullDayAdditionParameters additionParameters2 = additionParameters1;
    addition.CalculatedAdditionParameters = (AdditionParameters) additionParameters2;
    additionParameters2.IsFirstAddition = addition.IsOriginal;
    FABookPeriod bookPeriodOfDate = this.IncomingParameters.RepositoryHelper.FindFABookPeriodOfDate(new DateTime?(addition.IsOriginal ? additionParameters2.PlacedInServiceDate : addition.Date), new int?(bookId), new int?(assetId));
    additionParameters2.DepreciateFromDate = additionParameters2.PlacedInServiceDate;
    if (!additionParameters2.IsFirstAddition)
      additionParameters2.DepreciateFromDate = addition.Date;
    additionParameters2.DepreciateFromPeriodID = bookPeriodOfDate.FinPeriodID;
    additionParameters2.DepreciateToDate = StraightLineFullPeriodMethod.AddUsefulLifeToDate(additionParameters2.DepreciateFromDate, bookBalance.UsefulLife.Value);
    additionParameters2.DepreciateToPeriodID = this.IncomingParameters.RepositoryHelper.GetFABookPeriodIDOfDate(new DateTime?(additionParameters2.DepreciateToDate), new int?(bookId), new int?(assetId));
    additionParameters2.DepreciateFromPeriod = this.IncomingParameters.RepositoryHelper.FindByKey(new int?(bookId), this.IncomingParameters.OrganizationID, additionParameters2.DepreciateFromPeriodID);
    additionParameters2.DepreciateToPeriod = this.IncomingParameters.RepositoryHelper.FindByKey(new int?(bookId), this.IncomingParameters.OrganizationID, additionParameters2.DepreciateToPeriodID);
    SLMethodFullDayAdditionParameters additionParameters3 = additionParameters2;
    DateTime? endDate = additionParameters2.DepreciateFromPeriod.EndDate;
    DateTime? startDate = additionParameters2.DepreciateFromPeriod.StartDate;
    TimeSpan timeSpan = (endDate.HasValue & startDate.HasValue ? new TimeSpan?(endDate.GetValueOrDefault() - startDate.GetValueOrDefault()) : new TimeSpan?()).Value;
    double totalDays1 = timeSpan.TotalDays;
    additionParameters3.TotalDaysInFromPeriod = totalDays1;
    SLMethodFullDayAdditionParameters additionParameters4 = additionParameters2;
    timeSpan = additionParameters2.DepreciateFromPeriod.EndDate.Value - additionParameters2.DepreciateFromDate;
    double totalDays2 = timeSpan.TotalDays;
    additionParameters4.DaysHeldInFromPeriod = totalDays2;
    DateTime dateTime = additionParameters2.DepreciateToPeriod.EndDate.Value.AddDays(-1.0);
    SLMethodFullDayAdditionParameters additionParameters5 = additionParameters2;
    DateTime? nullable = additionParameters2.DepreciateToPeriod.EndDate;
    startDate = additionParameters2.DepreciateToPeriod.StartDate;
    timeSpan = (nullable.HasValue & startDate.HasValue ? new TimeSpan?(nullable.GetValueOrDefault() - startDate.GetValueOrDefault()) : new TimeSpan?()).Value;
    double totalDays3 = timeSpan.TotalDays;
    additionParameters5.TotalDaysInToPeriod = totalDays3;
    SLMethodFullDayAdditionParameters additionParameters6 = additionParameters2;
    double totalDaysInToPeriod = additionParameters2.TotalDaysInToPeriod;
    timeSpan = dateTime - additionParameters2.DepreciateToDate;
    double totalDays4 = timeSpan.TotalDays;
    double num1 = totalDaysInToPeriod - totalDays4;
    additionParameters6.DaysHeldInToPeriod = num1;
    SLMethodFullDayAdditionParameters additionParameters7 = additionParameters2;
    DateTime depreciateFromDate = additionParameters2.DepreciateFromDate;
    nullable = additionParameters2.DepreciateFromPeriod.StartDate;
    int num2 = nullable.HasValue ? (depreciateFromDate == nullable.GetValueOrDefault() ? 1 : 0) : 0;
    additionParameters7.IsFirstPeriodFull = num2 != 0;
    return additionParameters2;
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
      FAAddition originalAddition = this.CalculationParameters.OriginalAddition;
      SLMethodFullDayAdditionParameters additionParameters1 = addition.CalculatedAdditionParameters as SLMethodFullDayAdditionParameters;
      SLMethodFullDayAdditionParameters additionParameters2 = originalAddition.CalculatedAdditionParameters as SLMethodFullDayAdditionParameters;
      double num1 = (double) (this.IncomingParameters.PeriodDepreciationUtils.GetNumberOfPeriodsBetweenPeriods(additionParameters2.DepreciateToPeriodID, additionParameters2.DepreciateFromPeriodID) + 1) - (additionParameters2.TotalDaysInFromPeriod - additionParameters2.DaysHeldInFromPeriod) / additionParameters2.TotalDaysInFromPeriod - (additionParameters2.TotalDaysInToPeriod - additionParameters2.DaysHeldInToPeriod) / additionParameters2.TotalDaysInToPeriod;
      string finPeriodId = bookPeriodOfDate.FinPeriodID;
      DepreciationMethodBase.FADepreciationScheduleItem depreciationScheduleItem = schedule[finPeriodId];
      DateTime? nullable = bookPeriodOfDate.EndDate;
      DateTime placedInServiceDate1 = nullable.Value;
      DateTime dateTime1 = placedInServiceDate1.AddDays(-1.0);
      nullable = bookPeriodOfDate.EndDate;
      DateTime? startDate = bookPeriodOfDate.StartDate;
      TimeSpan timeSpan = (nullable.HasValue & startDate.HasValue ? new TimeSpan?(nullable.GetValueOrDefault() - startDate.GetValueOrDefault()) : new TimeSpan?()).Value;
      double totalDays1 = timeSpan.TotalDays;
      DateTime placedInServiceDate2 = additionParameters2.PlacedInServiceDate;
      nullable = bookPeriodOfDate.StartDate;
      DateTime dateTime2 = nullable.Value;
      double num2;
      if (!(placedInServiceDate2 > dateTime2))
      {
        num2 = 0.0;
      }
      else
      {
        placedInServiceDate1 = additionParameters2.PlacedInServiceDate;
        nullable = bookPeriodOfDate.StartDate;
        timeSpan = (nullable.HasValue ? new TimeSpan?(placedInServiceDate1 - nullable.GetValueOrDefault()) : new TimeSpan?()).Value;
        num2 = timeSpan.TotalDays;
      }
      double num3 = num2;
      double num4 = totalDays1;
      DateTime dateTime3 = dateTime1;
      nullable = this.IncomingParameters.Details.DisposalDate;
      DateTime dateTime4 = nullable.Value;
      timeSpan = dateTime3 - dateTime4;
      double totalDays2 = timeSpan.TotalDays;
      double num5 = num4 - totalDays2 - num3;
      Decimal num6 = additionParameters1.DepreciationBasis / (Decimal) num1 * (Decimal) (num5 / totalDays1);
      depreciationScheduleItem.DepreciationAmount = num6;
    }
  }
}
