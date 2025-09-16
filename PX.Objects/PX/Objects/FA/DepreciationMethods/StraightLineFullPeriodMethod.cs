// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationMethods.StraightLineFullPeriodMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.FA.DepreciationMethods.Parameters;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA.DepreciationMethods;

/// <exclude />
public class StraightLineFullPeriodMethod : StraightLineMethodBase
{
  protected override string[] ApplicableAveragingConventions { get; } = new string[1]
  {
    "FP"
  };

  protected override SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> Calculate()
  {
    SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> sortedDictionary1 = new SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>>();
    foreach (FAAddition addition in this.CalculationParameters.Additions)
    {
      SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem> sortedDictionary2 = new SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>();
      SLMethodAdditionParameters additionParameters = this.CalculateAdditionParameters(this.CalculationParameters, addition);
      List<string> additionPeriods = this.GetAdditionPeriods(additionParameters.DepreciateFromPeriodID, additionParameters.DepreciateToPeriodID);
      Decimal num = additionParameters.DepreciationBasis / (Decimal) additionPeriods.Count;
      foreach (string str in additionPeriods)
      {
        if (string.CompareOrdinal(str, this.CalculationParameters.MaxDepreciateToPeriodID) <= 0)
        {
          DepreciationMethodBase.FADepreciationScheduleItem depreciationScheduleItem;
          if (sortedDictionary2.TryGetValue(str, out depreciationScheduleItem))
            depreciationScheduleItem.DepreciationAmount += num;
          else
            sortedDictionary2[str] = new DepreciationMethodBase.FADepreciationScheduleItem()
            {
              FinPeriodID = str,
              DepreciationAmount = num
            };
        }
        else
          break;
      }
      sortedDictionary1[addition.PeriodID] = sortedDictionary2;
    }
    return sortedDictionary1;
  }

  public static DateTime AddUsefulLifeToDate(DateTime date, Decimal usefulLife)
  {
    return DeprCalcParameters.GetDatePlusYears(date, usefulLife);
  }

  private SLMethodAdditionParameters CalculateAdditionParameters(
    CalculationParameters calculationData,
    FAAddition addition)
  {
    FABookBalance bookBalance = calculationData.BookBalance;
    this.CheckParametersContracts();
    int assetId = calculationData.AssetID;
    int bookId = calculationData.BookID;
    SLMethodAdditionParameters additionParameters1 = new SLMethodAdditionParameters();
    additionParameters1.DepreciationBasis = addition.DepreciationBasis;
    additionParameters1.PlacedInServiceDate = bookBalance.DeprFromDate.Value;
    SLMethodAdditionParameters additionParameters2 = additionParameters1;
    addition.CalculatedAdditionParameters = (AdditionParameters) additionParameters2;
    FABookPeriod bookPeriodOfDate = this.IncomingParameters.RepositoryHelper.FindFABookPeriodOfDate(new DateTime?(addition.IsOriginal ? additionParameters2.PlacedInServiceDate : addition.Date), new int?(bookId), new int?(assetId));
    additionParameters2.DepreciateFromDate = bookPeriodOfDate?.StartDate.Value.Value;
    additionParameters2.DepreciateFromPeriodID = bookPeriodOfDate.FinPeriodID;
    DateTime date = StraightLineFullPeriodMethod.AddUsefulLifeToDate(additionParameters2.DepreciateFromDate, bookBalance.UsefulLife.Value);
    additionParameters2.DepreciateToPeriodID = this.IncomingParameters.RepositoryHelper.GetFABookPeriodIDOfDate(new DateTime?(date), new int?(bookId), new int?(assetId));
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
  }
}
