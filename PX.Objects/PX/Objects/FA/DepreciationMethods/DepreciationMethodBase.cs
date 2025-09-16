// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationMethods.DepreciationMethodBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.FA.DepreciationMethods.Parameters;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FA.DepreciationMethods;

/// <exclude />
public abstract class DepreciationMethodBase
{
  protected abstract string CalculationMethod { get; }

  protected abstract string[] ApplicableAveragingConventions { get; }

  public bool IsStraightLine => this.CalculationMethod == "SL";

  public ICollection<DepreciationMethodBase.FADepreciationScheduleItem> CalculateDepreciation(
    string maxPeriodID = null)
  {
    if (this.IncomingParameters.RepositoryHelper.FindFABookPeriodOfDate(this.IncomingParameters.BookBalance.DeprFromDate, this.IncomingParameters.BookID, this.IncomingParameters.AssetID).FinPeriodID != this.IncomingParameters.BookBalance.DeprFromPeriod)
      throw new PXException("The start depreciation date ({0}) of the {2} fixed asset is outside the {1} period specified in the Depr. from Period column. The asset cannot be depreciated. To solve the issue, contact your Acumatica support provider.", new object[3]
      {
        (object) this.IncomingParameters.BookBalance.DeprFromDate,
        (object) FinPeriodIDFormattingAttribute.FormatForError(this.IncomingParameters.BookBalance.DeprFromPeriod),
        (object) this.IncomingParameters.FixedAsset.AssetCD
      });
    return this.CalculateDepreciation(new CalculationParameters(this.IncomingParameters, maxPeriodID));
  }

  protected abstract SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> Calculate();

  public ICollection<DepreciationMethodBase.FADepreciationScheduleItem> CalculateDepreciation(
    CalculationParameters parameters)
  {
    this.CalculationParameters = parameters;
    SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> schedules = this.Calculate();
    this.ApplySuspend(schedules);
    this.ApplyDispose(schedules);
    return this.Summarize(schedules);
  }

  /// <summary>
  /// Apply suspension to whole asset schedule which consists all additions calculated depreciation schedules
  /// Method gets calculated depreciation schedules and modify each of them
  /// If you need to apply another suspension algorithm after calculation - you should override this method.
  /// </summary>
  /// <param name="schedules">Calculated depreciation schedules</param>
  protected virtual void ApplySuspend(
    SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> schedules)
  {
    foreach (SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem> depreciationScheduleItems in schedules.Values)
      this.ApplySuspendedPeriodsToSchedule(depreciationScheduleItems);
  }

  /// <summary>
  /// Apply method-specific disposing rule to calculated depreciation schedules
  /// Method gets calculated depreciation data and modifies it as required by the disposal logic
  /// If you need to apply disposing after suspension - you should override this method.
  /// </summary>
  /// <param name="schedules">Calculated depreciation schedules</param>
  protected abstract void ApplyDispose(
    SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> schedules);

  private ICollection<DepreciationMethodBase.FADepreciationScheduleItem> Summarize(
    SortedDictionary<string, SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>> schedules)
  {
    SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem> sortedDictionary1 = new SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem>();
    foreach (SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem> sortedDictionary2 in schedules.Values)
    {
      foreach (DepreciationMethodBase.FADepreciationScheduleItem depreciationScheduleItem1 in sortedDictionary2.Values)
      {
        DepreciationMethodBase.FADepreciationScheduleItem depreciationScheduleItem2;
        if (sortedDictionary1.TryGetValue(depreciationScheduleItem1.FinPeriodID, out depreciationScheduleItem2))
          depreciationScheduleItem2.DepreciationAmount += depreciationScheduleItem1.DepreciationAmount;
        else
          sortedDictionary1[depreciationScheduleItem1.FinPeriodID] = new DepreciationMethodBase.FADepreciationScheduleItem()
          {
            FinPeriodID = depreciationScheduleItem1.FinPeriodID,
            DepreciationAmount = depreciationScheduleItem1.DepreciationAmount
          };
      }
    }
    return (ICollection<DepreciationMethodBase.FADepreciationScheduleItem>) sortedDictionary1.Values;
  }

  public IncomingCalculationParameters IncomingParameters { get; private set; }

  public CalculationParameters CalculationParameters { get; set; }

  public bool IsSuitable(IncomingCalculationParameters incomingParameters)
  {
    this.IncomingParameters = incomingParameters;
    if (!(this.CalculationMethod == incomingParameters.CalculationMethod))
      return false;
    return this.ApplicableAveragingConventions == null || ((IEnumerable<string>) this.ApplicableAveragingConventions).Contains<string>(incomingParameters.AveragingConvention);
  }

  /// <summary>
  /// Apply suspension to single addition in calculated schedules
  /// Method gets depreciation schedule items and modify each of them as described in https://wiki.acumatica.com/display/SPEC/Asset+Suspension
  /// If you need to apply another suspension method after calculation - you should override this method.
  /// </summary>
  /// <param name="depreciationScheduleItems">Calculated depreciation items</param>
  protected virtual SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem> ApplySuspendedPeriodsToSchedule(
    SortedDictionary<string, DepreciationMethodBase.FADepreciationScheduleItem> depreciationScheduleItems)
  {
    if (depreciationScheduleItems.Count == 0)
      return depreciationScheduleItems;
    List<DepreciationMethodBase.FADepreciationScheduleItem> list = depreciationScheduleItems.Values.ToList<DepreciationMethodBase.FADepreciationScheduleItem>();
    DepreciationMethodBase.FADepreciationScheduleItem depreciationScheduleItem1 = list.First<DepreciationMethodBase.FADepreciationScheduleItem>();
    if (this.IncomingParameters.SuspendedPeriodsIDs.Count > 0)
    {
      List<(string, string)> sectionsForAddition = this.GetSuspendSectionsForAddition(depreciationScheduleItem1.FinPeriodID);
      if (sectionsForAddition.Count == 0)
        return depreciationScheduleItems;
      list.Reverse();
      List<DepreciationMethodBase.FADepreciationScheduleItem> depreciationScheduleItemList = new List<DepreciationMethodBase.FADepreciationScheduleItem>();
      foreach (DepreciationMethodBase.FADepreciationScheduleItem depreciationScheduleItem2 in list)
      {
        foreach ((string, string) tuple in sectionsForAddition)
        {
          if (string.CompareOrdinal(depreciationScheduleItem2.FinPeriodID, tuple.Item1) >= 0)
          {
            int offset = this.IncomingParameters.PeriodDepreciationUtils.GetNumberOfPeriodsBetweenPeriods(tuple.Item2, tuple.Item1) + 1;
            string period = this.IncomingParameters.PeriodDepreciationUtils.AddPeriodsCountToPeriod(depreciationScheduleItem2.FinPeriodID, offset);
            depreciationScheduleItem2.FinPeriodID = period;
            if (string.CompareOrdinal(period, this.CalculationParameters.MaxDepreciateToPeriodID) > 0)
              depreciationScheduleItemList.Add(depreciationScheduleItem2);
          }
          else
            break;
        }
      }
      foreach (DepreciationMethodBase.FADepreciationScheduleItem depreciationScheduleItem3 in depreciationScheduleItemList)
        list.Remove(depreciationScheduleItem3);
      if (!list.Where<DepreciationMethodBase.FADepreciationScheduleItem>((Func<DepreciationMethodBase.FADepreciationScheduleItem, bool>) (scheduleItem => scheduleItem.FinPeriodID == this.IncomingParameters.BookBalance.DeprFromPeriod)).Any<DepreciationMethodBase.FADepreciationScheduleItem>())
        list.Add(new DepreciationMethodBase.FADepreciationScheduleItem()
        {
          FinPeriodID = this.IncomingParameters.BookBalance.DeprFromPeriod
        });
    }
    depreciationScheduleItems.Clear();
    foreach (DepreciationMethodBase.FADepreciationScheduleItem depreciationScheduleItem4 in list)
      depreciationScheduleItems.Add(depreciationScheduleItem4.FinPeriodID, depreciationScheduleItem4);
    return depreciationScheduleItems;
  }

  private List<(string, string)> GetSuspendSectionsForAddition(string startPeriodID)
  {
    return this.IncomingParameters.SuspendSections.Where<(string, string)>((Func<(string, string), bool>) (section => string.CompareOrdinal(section.Item2, startPeriodID) >= 0)).Select<(string, string), (string, string)>((Func<(string, string), (string, string)>) (section => (string.CompareOrdinal(startPeriodID, section.Item1) > 0 ? startPeriodID : section.Item1, section.Item2))).ToList<(string, string)>();
  }

  protected List<string> GetAdditionPeriods(string fromPeriodID, string toPeriodID)
  {
    return this.IncomingParameters.PeriodDepreciationUtils.GetPeriods(fromPeriodID, toPeriodID).ToList<string>();
  }

  protected bool IsFirstPeriod(string period) => period.EndsWith("01");

  protected virtual int GetNumberOfPeriods(IYearSetup yearSetup)
  {
    if (!yearSetup.HasAdjustmentPeriod.GetValueOrDefault())
      return (int) yearSetup.FinPeriods.Value;
    short? finPeriods = yearSetup.FinPeriods;
    return (finPeriods.HasValue ? new int?((int) finPeriods.GetValueOrDefault() - 1) : new int?()).Value;
  }

  public class FADepreciationScheduleItem
  {
    public string FinPeriodID { get; set; }

    public Decimal DepreciationAmount { get; set; }
  }
}
