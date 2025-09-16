// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationMethods.AustralianDiminishingValueMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.FA.DepreciationMethods.Parameters;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA.DepreciationMethods;

public class AustralianDiminishingValueMethod : DepreciationMethodBase
{
  private const int DaysInYear = 365;
  private readonly bool DepreciateInDisposalPeriod;

  protected override string CalculationMethod => "DV";

  protected override string[] ApplicableAveragingConventions { get; }

  public AustralianDiminishingValueMethod(FASetup faSetup)
  {
    this.DepreciateInDisposalPeriod = faSetup.DepreciateInDisposalPeriod.GetValueOrDefault();
  }

  protected override void ApplyDispose(
    SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> schedules)
  {
    if (!this.IncomingParameters.Details.DisposalDate.HasValue)
      return;
    FABookPeriod bookPeriodOfDate = this.IncomingParameters.PeriodDepreciationUtils.FindFABookPeriodOfDate(this.IncomingParameters.Details.DisposalDate);
    if (this.CalculationParameters.MaxDepreciateToPeriodID != bookPeriodOfDate.FinPeriodID)
      return;
    string finPeriodIdOfYear = FinPeriodUtils.GetFirstFinPeriodIDOfYear(bookPeriodOfDate.FinPeriodID);
    foreach (FAAddition addition in this.CalculationParameters.Additions)
    {
      SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem> schedule = schedules[addition.PeriodID];
      AustralianDiminishingValueAdditionParameters additionParameters = addition.CalculatedAdditionParameters as AustralianDiminishingValueAdditionParameters;
      DepreciationMethodBase.FADepreciationScheduleItem depreciationScheduleItem = schedule[bookPeriodOfDate.FinPeriodID];
      if (!this.DepreciateInDisposalPeriod)
      {
        depreciationScheduleItem.DepreciationAmount = 0M;
        break;
      }
      Decimal num1 = 0M;
      foreach (KeyValuePair<string, DepreciationMethodBase.FADepreciationScheduleItem> keyValuePair in schedule)
      {
        if (string.CompareOrdinal(keyValuePair.Key, finPeriodIdOfYear) < 0)
          num1 += keyValuePair.Value.DepreciationAmount;
        else
          break;
      }
      DateTime dateTime = bookPeriodOfDate.EndDate.Value.AddDays(-1.0);
      DateTime? endDate = bookPeriodOfDate.EndDate;
      DateTime? startDate = bookPeriodOfDate.StartDate;
      TimeSpan timeSpan = (endDate.HasValue & startDate.HasValue ? new TimeSpan?(endDate.GetValueOrDefault() - startDate.GetValueOrDefault()) : new TimeSpan?()).Value;
      double totalDays1 = timeSpan.TotalDays;
      timeSpan = dateTime - this.IncomingParameters.Details.DisposalDate.Value;
      double totalDays2 = timeSpan.TotalDays;
      Decimal num2 = (Decimal) (totalDays1 - totalDays2);
      depreciationScheduleItem.DepreciationAmount = (additionParameters.DepreciationBasis - num1) * this.CalculationParameters.BookBalance.PercentPerYear.Value * num2 / 365M / 100M;
    }
  }

  protected override SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> Calculate()
  {
    SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> sortedDictionary1 = new SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>>();
    foreach (FAAddition addition in this.CalculationParameters.Additions)
    {
      SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem> sortedDictionary2 = new SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>();
      AustralianDiminishingValueAdditionParameters additionParameters = this.CalculateAdditionParameters(this.CalculationParameters, addition);
      string period = this.IncomingParameters.PeriodDepreciationUtils.AddPeriodsCountToPeriod(additionParameters.DepreciateToPeriodID, this.IncomingParameters.SuspendedPeriodsIDs.Count);
      List<string> additionPeriods = this.GetAdditionPeriods(additionParameters.DepreciateFromPeriodID, period);
      Decimal num1 = 0M;
      Decimal num2 = additionParameters.DepreciationBasis;
      foreach (string str in additionPeriods)
      {
        if (string.CompareOrdinal(str, this.CalculationParameters.MaxDepreciateToPeriodID) <= 0)
        {
          FABookPeriod bookPeriod = this.IncomingParameters.PeriodDepreciationUtils.BookPeriods[str];
          if (this.IsFirstPeriod(str))
            num2 = additionParameters.DepreciationBasis - num1;
          Decimal num3;
          if (str == additionParameters.DepreciateFromPeriodID)
            num3 = (Decimal) (additionParameters.DepreciateFromPeriod.EndDate.Value - additionParameters.DepreciateFromDate).TotalDays;
          else if (str == additionParameters.DepreciateToPeriodID)
          {
            DateTime dateTime = additionParameters.DepreciateToPeriod.EndDate.Value.AddDays(-1.0);
            DateTime? endDate = additionParameters.DepreciateToPeriod.EndDate;
            DateTime? startDate = additionParameters.DepreciateToPeriod.StartDate;
            num3 = (Decimal) ((endDate.HasValue & startDate.HasValue ? new TimeSpan?(endDate.GetValueOrDefault() - startDate.GetValueOrDefault()) : new TimeSpan?()).Value.TotalDays - (dateTime - additionParameters.DepreciateToDate).TotalDays);
          }
          else
            num3 = (Decimal) (bookPeriod.EndDate.Value - bookPeriod.StartDate.Value).TotalDays;
          Decimal num4 = num2 * this.CalculationParameters.BookBalance.PercentPerYear.Value * num3 / 365M / 100M;
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

  protected override void ApplySuspend(
    SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> schedules)
  {
  }

  private AustralianDiminishingValueAdditionParameters CalculateAdditionParameters(
    CalculationParameters calculationData,
    FAAddition addition)
  {
    FABookBalance bookBalance = calculationData.BookBalance;
    this.CheckParametersContracts();
    int assetId = calculationData.AssetID;
    int bookId = calculationData.BookID;
    AustralianDiminishingValueAdditionParameters additionParameters1 = new AustralianDiminishingValueAdditionParameters();
    additionParameters1.DepreciationBasis = addition.Amount * addition.BusinessUse;
    AustralianDiminishingValueAdditionParameters additionParameters2 = additionParameters1;
    addition.CalculatedAdditionParameters = (AdditionParameters) additionParameters2;
    FABookPeriod bookPeriodOfDate = this.IncomingParameters.RepositoryHelper.FindFABookPeriodOfDate(new DateTime?(addition.IsOriginal ? bookBalance.DeprFromDate.Value : addition.Date), new int?(bookId), new int?(assetId));
    additionParameters2.DepreciateFromDate = bookBalance.DeprFromDate.Value;
    additionParameters2.DepreciateFromPeriodID = bookPeriodOfDate.FinPeriodID;
    additionParameters2.IsFirstAddition = addition.IsOriginal;
    additionParameters2.DepreciateToDate = DeprCalcParameters.GetDatePlusYears(additionParameters2.DepreciateFromDate, bookBalance.UsefulLife.Value);
    additionParameters2.DepreciateToPeriodID = this.IncomingParameters.RepositoryHelper.GetFABookPeriodIDOfDate(new DateTime?(additionParameters2.DepreciateToDate), new int?(bookId), new int?(assetId));
    additionParameters2.DepreciateFromPeriod = this.IncomingParameters.RepositoryHelper.FindByKey(new int?(bookId), this.IncomingParameters.OrganizationID, additionParameters2.DepreciateFromPeriodID);
    additionParameters2.DepreciateToPeriod = this.IncomingParameters.RepositoryHelper.FindByKey(new int?(bookId), this.IncomingParameters.OrganizationID, additionParameters2.DepreciateToPeriodID);
    if (!additionParameters2.IsFirstAddition && this.IncomingParameters.RepositoryHelper.GetFABookPeriodIDOfDate(new DateTime?(additionParameters2.DepreciateFromDate), new int?(bookId), new int?(assetId)) != additionParameters2.DepreciateFromPeriodID)
      additionParameters2.DepreciateFromDate = additionParameters2.DepreciateFromPeriod.StartDate.Value;
    return additionParameters2;
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
}
