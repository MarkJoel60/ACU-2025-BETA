// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationMethods.RemainingValueFullPeriodMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using PX.Objects.FA.DepreciationMethods.Parameters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FA.DepreciationMethods;

public class RemainingValueFullPeriodMethod : DepreciationMethodBase
{
  private Dictionary<string, FABookHistory> existingCalculatedHistory;

  protected override string[] ApplicableAveragingConventions { get; } = new string[1]
  {
    "FP"
  };

  protected override string CalculationMethod => "RV";

  protected override void ApplyDispose(
    SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> schedules)
  {
  }

  protected override void ApplySuspend(
    SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> schedules)
  {
  }

  protected override SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> Calculate()
  {
    SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> sortedDictionary1 = new SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>>();
    FAAddition faAddition = this.CalculationParameters.Additions.First<FAAddition>();
    Dictionary<string, RemainingValueAdditionParameters> dictionary = this.CalculationParameters.Additions.ToDictionary<FAAddition, string, RemainingValueAdditionParameters>((Func<FAAddition, string>) (a => a.PeriodID), (Func<FAAddition, RemainingValueAdditionParameters>) (b => this.CalculateAdditionParameters(this.CalculationParameters, b)));
    RemainingValueAdditionParameters additionParameters1;
    dictionary.TryGetValue(faAddition.PeriodID, out additionParameters1);
    string period = this.IncomingParameters.PeriodDepreciationUtils.AddPeriodsCountToPeriod(additionParameters1.DepreciateToPeriodID, this.IncomingParameters.SuspendedPeriodsIDs.Count);
    List<string> additionPeriods = this.GetAdditionPeriods(additionParameters1.DepreciateFromPeriodID, period);
    FABookBalance bookBalance = this.CalculationParameters.BookBalance;
    if (bookBalance.CurrDeprPeriod != null && string.CompareOrdinal(additionParameters1.PlacedInPeriod, bookBalance.CurrDeprPeriod) < 0 && this.existingCalculatedHistory == null)
      this.existingCalculatedHistory = this.GetExistingHistory(bookBalance, additionParameters1.PlacedInPeriod, bookBalance.CurrDeprPeriod);
    SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem> sortedDictionary2 = new SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>();
    sortedDictionary1[faAddition.PeriodID] = sortedDictionary2;
    int num1 = additionPeriods.Count - this.IncomingParameters.SuspendedPeriodsIDs.Count;
    Decimal num2 = 0M;
    Decimal depreciationBasis = 0M;
    foreach (string str in additionPeriods)
    {
      RemainingValueAdditionParameters additionParameters2;
      if (dictionary.TryGetValue(str, out additionParameters2))
        depreciationBasis += additionParameters2.DepreciationBasis;
      if (string.CompareOrdinal(str, this.CalculationParameters.MaxDepreciateToPeriodID) <= 0)
      {
        Decimal num3 = 0M;
        Decimal? nullable;
        if (string.CompareOrdinal(str, additionParameters1.DepreciateFromPeriodID) >= 0)
        {
          FABookHistory faBookHistory1;
          if (bookBalance.CurrDeprPeriod != null && string.CompareOrdinal(str, bookBalance.CurrDeprPeriod) < 0 && this.existingCalculatedHistory.TryGetValue(str, out faBookHistory1))
          {
            if (RemainingValueFullPeriodMethod.Is179Calculation(bookBalance, depreciationBasis))
            {
              nullable = faBookHistory1.PtdTax179Taken;
              num3 = nullable.Value;
            }
            else if (RemainingValueFullPeriodMethod.IsBonusCalculation(bookBalance, depreciationBasis))
            {
              nullable = faBookHistory1.PtdBonusTaken;
              num3 = nullable.Value;
            }
            else if (depreciationBasis != 0M)
            {
              nullable = faBookHistory1.PtdDepreciated;
              Decimal num4 = nullable.Value;
              nullable = faBookHistory1.PtdAdjusted;
              Decimal num5 = nullable.Value;
              num3 = num4 + num5;
              if (str == additionParameters1.DepreciateFromPeriodID)
                num3 -= faAddition.Section179Amount + faAddition.BonusAmount;
            }
          }
          else
          {
            FABookHistory faBookHistory2;
            if (string.Equals(str, bookBalance.CurrDeprPeriod) && this.existingCalculatedHistory != null && !RemainingValueFullPeriodMethod.Is179Calculation(bookBalance, depreciationBasis) && !RemainingValueFullPeriodMethod.IsBonusCalculation(bookBalance, depreciationBasis) && this.existingCalculatedHistory.TryGetValue(str, out faBookHistory2))
            {
              Decimal num6 = 0M;
              nullable = faBookHistory2.YtdDepreciated;
              if (nullable.Value == 0M && this.IncomingParameters.SuspendedPeriodsIDs.Contains(additionParameters1.DepreciateFromPeriodID))
                num6 = faAddition.Section179Amount + faAddition.BonusAmount;
              Decimal num7 = num2 + num6;
              nullable = faBookHistory2.PtdAdjusted;
              Decimal num8 = nullable.Value;
              num3 = (depreciationBasis - num7 - num8) / (Decimal) num1 + num8 + num6;
              num2 = num7 - num6;
            }
            else
              num3 = (depreciationBasis - num2) / (Decimal) num1;
          }
        }
        if (!this.IncomingParameters.SuspendedPeriodsIDs.Contains(str))
          --num1;
        num2 += num3;
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
    return sortedDictionary1;
  }

  private static bool IsBonusCalculation(FABookBalance bookBal, Decimal depreciationBasis)
  {
    return bookBal.BonusAmount.Value != 0M && bookBal.BonusAmount.Value == depreciationBasis;
  }

  private static bool Is179Calculation(FABookBalance bookBal, Decimal depreciationBasis)
  {
    return bookBal.Tax179Amount.Value != 0M && bookBal.Tax179Amount.Value == depreciationBasis;
  }

  public static DateTime AddUsefulLifeToDate(DateTime date, Decimal usefulLife)
  {
    return DeprCalcParameters.GetDatePlusYears(date, usefulLife);
  }

  public virtual Dictionary<string, FABookHistory> GetExistingHistory(
    FABookBalance bookBalance,
    string minPeriodID,
    string maxPeriodID)
  {
    PXView view = PXSelectBase<FABookHistory, PXViewOf<FABookHistory>.BasedOn<SelectFromBase<FABookHistory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookHistory.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FABookHistory.bookID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<FABookHistory.finPeriodID, IBqlString>.IsGreaterEqual<P.AsString>>>>.And<BqlOperand<FABookHistory.finPeriodID, IBqlString>.IsLessEqual<P.AsString>>>>.ReadOnly.Config>.GetCommand().CreateView(this.IncomingParameters.Graph);
    using (new PXFieldScope(view, (IEnumerable<Type>) new List<Type>()
    {
      typeof (FABookHistory.assetID),
      typeof (FABookHistory.bookID),
      typeof (FABookHistory.finPeriodID),
      typeof (FABookHistory.ptdDepreciated),
      typeof (FABookHistory.ptdTax179Taken),
      typeof (FABookHistory.ptdBonusTaken),
      typeof (FABookHistory.ytdDepreciated),
      typeof (FABookHistory.ptdAdjusted)
    }, true))
      return GraphHelper.RowCast<FABookHistory>((IEnumerable) view.SelectMulti(new object[4]
      {
        (object) bookBalance.AssetID,
        (object) bookBalance.BookID,
        (object) minPeriodID,
        (object) maxPeriodID
      })).ToDictionary<FABookHistory, string, FABookHistory>((Func<FABookHistory, string>) (h => h.FinPeriodID), (Func<FABookHistory, FABookHistory>) (h => h));
  }

  private RemainingValueAdditionParameters CalculateAdditionParameters(
    CalculationParameters calculationData,
    FAAddition addition)
  {
    FABookBalance bookBalance = calculationData.BookBalance;
    this.CheckParametersContracts();
    int assetId = calculationData.AssetID;
    int bookId = calculationData.BookID;
    RemainingValueAdditionParameters additionParameters1 = new RemainingValueAdditionParameters();
    additionParameters1.DepreciationBasis = addition.DepreciationBasis;
    additionParameters1.PlacedInServiceDate = bookBalance.DeprFromDate.Value;
    RemainingValueAdditionParameters additionParameters2 = additionParameters1;
    addition.CalculatedAdditionParameters = (AdditionParameters) additionParameters2;
    FABookPeriod bookPeriodOfDate1 = this.IncomingParameters.RepositoryHelper.FindFABookPeriodOfDate(new DateTime?(addition.Date), new int?(bookId), new int?(assetId));
    additionParameters2.DepreciateFromDate = bookPeriodOfDate1?.StartDate.Value.Value;
    additionParameters2.DepreciateFromPeriodID = bookPeriodOfDate1.FinPeriodID;
    FABookPeriod bookPeriodOfDate2 = this.IncomingParameters.RepositoryHelper.FindFABookPeriodOfDate(new DateTime?(additionParameters2.PlacedInServiceDate), new int?(bookId), new int?(assetId));
    additionParameters2.PlacedInPeriod = bookPeriodOfDate2.FinPeriodID;
    DateTime date = RemainingValueFullPeriodMethod.AddUsefulLifeToDate(bookPeriodOfDate2.StartDate.Value, bookBalance.UsefulLife.Value);
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
