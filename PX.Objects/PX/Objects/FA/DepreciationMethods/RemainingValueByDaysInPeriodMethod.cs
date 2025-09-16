// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationMethods.RemainingValueByDaysInPeriodMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.FA.DepreciationMethods.Parameters;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA.DepreciationMethods;

public class RemainingValueByDaysInPeriodMethod : DepreciationMethodBase
{
  protected override string CalculationMethod => "RD";

  protected override string[] ApplicableAveragingConventions { get; } = new string[1]
  {
    "FD"
  };

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
      AdditionParameters additionParameters = addition.CalculatedAdditionParameters;
      string finPeriodId = bookPeriodOfDate.FinPeriodID;
      DepreciationMethodBase.FADepreciationScheduleItem depreciationScheduleItem = schedule[finPeriodId];
      TimeSpan timeSpan = additionParameters.DepreciateToDate - additionParameters.DepreciateFromDate;
      Decimal num1 = (Decimal) (timeSpan.Days + 1);
      DateTime? nullable = bookPeriodOfDate.EndDate;
      DateTime dateTime1 = nullable.Value.AddDays(-1.0);
      nullable = bookPeriodOfDate.EndDate;
      DateTime? startDate = bookPeriodOfDate.StartDate;
      timeSpan = (nullable.HasValue & startDate.HasValue ? new TimeSpan?(nullable.GetValueOrDefault() - startDate.GetValueOrDefault()) : new TimeSpan?()).Value;
      Decimal totalDays1 = (Decimal) timeSpan.TotalDays;
      DateTime dateTime2 = dateTime1;
      nullable = this.IncomingParameters.Details.DisposalDate;
      DateTime dateTime3 = nullable.Value;
      timeSpan = dateTime2 - dateTime3;
      Decimal totalDays2 = (Decimal) timeSpan.TotalDays;
      Decimal num2 = totalDays1 - totalDays2;
      Decimal num3 = additionParameters.DepreciationBasis * (num2 / num1);
      depreciationScheduleItem.DepreciationAmount = num3;
    }
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
      Decimal num2 = (Decimal) ((additionParameters.DepreciateToDate - additionParameters.DepreciateFromDate).Days + 1);
      foreach (string str in additionPeriods)
      {
        if (string.CompareOrdinal(str, this.CalculationParameters.MaxDepreciateToPeriodID) <= 0)
        {
          FABookPeriod bookPeriod = this.IncomingParameters.PeriodDepreciationUtils.BookPeriods[str];
          Decimal num3 = this.GetDaysHeldInPeriod(bookPeriod);
          if (addition.IsOriginal && str == additionParameters.DepreciateFromPeriodID)
            num3 = (Decimal) (bookPeriod.EndDate.Value - additionParameters.DepreciateFromDate).TotalDays;
          Decimal num4 = !(str == period) ? additionParameters.DepreciationBasis * (num3 / num2) : additionParameters.DepreciationBasis - num1;
          if (this.IncomingParameters.SuspendedPeriodsIDs.Contains(str))
            num4 = 0M;
          num1 += num4;
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

  protected Decimal GetDaysHeldInPeriod(FABookPeriod fABookPeriod)
  {
    DateTime? nullable = fABookPeriod.EndDate;
    DateTime dateTime1 = nullable.Value;
    nullable = fABookPeriod.StartDate;
    DateTime dateTime2 = nullable.Value;
    return (Decimal) (dateTime1 - dateTime2).TotalDays;
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
    FABookPeriod bookPeriodOfDate = this.IncomingParameters.RepositoryHelper.FindFABookPeriodOfDate(new DateTime?(addition.Date), new int?(bookId), new int?(assetId));
    additionParameters.DepreciateFromDate = addition.IsOriginal ? bookBalance.DeprFromDate.Value : bookPeriodOfDate.StartDate.Value;
    additionParameters.DepreciateFromPeriodID = bookPeriodOfDate.FinPeriodID;
    additionParameters.DepreciateToDate = DeprCalcParameters.GetDatePlusYears(bookBalance.DeprFromDate.Value, bookBalance.UsefulLife.Value);
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
}
