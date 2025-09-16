// Decompiled with JetBrains decompiler
// Type: PX.Objects.AU.FinPeriodScheduleAdjustmentRule
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.Process;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.AU;

internal class FinPeriodScheduleAdjustmentRule : ScheduleAdjustmentRuleBase
{
  private readonly Func<PXGraph> _cacheGraphFactory;

  public virtual string TypeID { get; } = "P";

  public FinPeriodScheduleAdjustmentRule(Func<PXGraph> cacheGraphFactory)
  {
    this._cacheGraphFactory = cacheGraphFactory ?? throw new ArgumentNullException(nameof (cacheGraphFactory));
  }

  public virtual void AdjustNextDate(AUSchedule schedule)
  {
    FinPeriod closestPeriod = this.GetClosestPeriod(schedule.LastRunDate ?? schedule.NextRunDate);
    if (closestPeriod == null)
      throw new PXFinPeriodDoesNotExist(PXMessages.LocalizeNoPrefix("The operation cannot be executed because the financial period of the execution date is not opened."));
    int months = (int) schedule.PeriodFrequency.Value;
    for (int index = 0; index < months && closestPeriod != null; ++index)
      closestPeriod = this.GetClosestPeriod(closestPeriod.EndDate);
    DateTime dateTime1;
    if (closestPeriod == null)
    {
      DateTime? lastRunDate = schedule.LastRunDate;
      ref DateTime? local = ref lastRunDate;
      dateTime1 = local.HasValue ? local.GetValueOrDefault().AddMonths(months) : schedule.NextRunDate.Value.AddMonths(months);
    }
    else
      dateTime1 = this.GetNextDate(schedule, closestPeriod);
    DateTime dateTime2 = dateTime1;
    schedule.NextRunDate = new DateTime?(dateTime2);
    if (closestPeriod == null)
      throw new PXFinPeriodDoesNotExist(PXMessages.LocalizeNoPrefix("The operation has been completed successfully, but the financial period for the next execution date is not opened. Open the period on the Manage Financial Periods (GL503000) form."), (PXErrorLevel) 1);
  }

  protected virtual FinPeriod GetClosestPeriod(DateTime? date)
  {
    FinPeriod closestPeriod = (FinPeriod) null;
    try
    {
      closestPeriod = PXResultset<FinPeriod>.op_Implicit(PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.endDate, Greater<Required<FinPeriod.endDate>>>>.Config>.SelectWindowed(this._cacheGraphFactory(), 0, 1, new object[1]
      {
        (object) date
      }));
    }
    catch
    {
    }
    return closestPeriod;
  }

  private DateTime GetNextDate(AUSchedule schedule, FinPeriod period)
  {
    DateTime nextDate;
    if (schedule.PeriodDateSel == "S")
      nextDate = period.StartDate.Value;
    else if (schedule.PeriodDateSel == "E")
    {
      nextDate = period.EndDate.Value.AddDays(-1.0);
    }
    else
    {
      double totalDays = (period.EndDate.Value - period.StartDate.Value).TotalDays;
      short? periodFixedDay = schedule.PeriodFixedDay;
      double? nullable = periodFixedDay.HasValue ? new double?((double) periodFixedDay.GetValueOrDefault()) : new double?();
      double valueOrDefault = nullable.GetValueOrDefault();
      if (totalDays >= valueOrDefault & nullable.HasValue)
      {
        DateTime dateTime = period.StartDate.Value;
        ref DateTime local = ref dateTime;
        periodFixedDay = schedule.PeriodFixedDay;
        double num = (periodFixedDay.HasValue ? new double?((double) periodFixedDay.GetValueOrDefault() - 1.0) : new double?()).Value;
        nextDate = local.AddDays(num);
      }
      else
        nextDate = period.EndDate.Value.AddDays(-1.0);
    }
    return nextDate;
  }
}
