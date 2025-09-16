// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FiscalPeriodCreator`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

public class FiscalPeriodCreator<TYear, TPeriod>
  where TYear : class, IYear, IBqlTable, new()
  where TPeriod : class, IPeriod, IBqlTable, new()
{
  public PXGraph Graph;
  private int firstPeriodYear;
  private string finYear;
  private DateTime yearStartDate;
  private DateTime periodsStartDate;
  private IYearSetup yearSetup;
  private IEnumerable<IPeriodSetup> periodsSetup;
  private FiscalPeriodSetupCreator<TPeriod> _finPeriodCreator;

  public FiscalPeriodCreator(
    IYearSetup aYearSetup,
    string aFinYear,
    DateTime? aPeriodsStartDate,
    IEnumerable<IPeriodSetup> aPeriods)
  {
    this.yearSetup = aYearSetup;
    this.finYear = aFinYear;
    this.periodsStartDate = aPeriodsStartDate.Value;
    this.periodsSetup = aPeriods;
    this.yearStartDate = FiscalYearCreator<TYear, TPeriod>.CalcYearStartDate(this.yearSetup, aFinYear);
    this.firstPeriodYear = this.periodsStartDate.Year;
  }

  public virtual void CreatePeriods(PXCache cache)
  {
    this.Graph = this.Graph ?? cache.Graph;
    TPeriod current = default (TPeriod);
    int num = 0;
    do
    {
      current = this.createNextPeriod(current);
      if ((object) current != null)
      {
        cache.Insert((object) current);
        ++num;
      }
    }
    while ((object) current != null && num < 1000);
    if (num >= 1000)
      throw new PXException("System error - Infinit Loop detected");
  }

  public static void CreatePeriods(
    PXCache cache,
    IEnumerable<IPeriodSetup> aPeriods,
    string aYear,
    DateTime? YearStartDate,
    IYearSetup aYearSetup)
  {
    new FiscalPeriodCreator<TYear, TPeriod>(aYearSetup, aYear, YearStartDate, aPeriods).CreatePeriods(cache);
  }

  public virtual bool fillNextPeriod(TPeriod result, TPeriod current)
  {
    if (!FiscalPeriodSetupCreator.IsFixedLengthPeriod(this.yearSetup.FPType))
    {
      bool? custom;
      if ((object) current != null)
      {
        custom = current.Custom;
        if (custom.GetValueOrDefault())
          goto label_5;
      }
      if ((object) result != null)
      {
        custom = result.Custom;
        if (custom.GetValueOrDefault())
          goto label_5;
      }
      return this.fillNextFromTemplate(result, current);
    }
label_5:
    if (!this.FinPeriodCreator.fillNextPeriod(result, current))
      return false;
    result.DateLocked = new bool?(false);
    result.FinYear = this.finYear;
    result.FinPeriodID = result.FinYear + result.PeriodNbr;
    return true;
  }

  protected virtual bool fillNextFromTemplate(TPeriod result, TPeriod current)
  {
    IPeriodSetup aSetup = (IPeriodSetup) null;
    IPeriodSetup periodSetup = (IPeriodSetup) null;
    int num = 0;
    foreach (IPeriodSetup current1 in this.periodsSetup)
    {
      num += FiscalPeriodCreator<TYear, TPeriod>.GetYearDelta(current1, periodSetup);
      if (FiscalPeriodCreator<TYear, TPeriod>.IsEqual(periodSetup, current))
      {
        aSetup = current1;
        break;
      }
      periodSetup = current1;
    }
    if (aSetup == null)
      return false;
    int aYear = this.firstPeriodYear + num;
    this.CopyFrom(result, aSetup, aYear, this.Graph);
    result.DateLocked = new bool?(false);
    return true;
  }

  private static int GetYearDelta(IPeriodSetup current, IPeriodSetup prev)
  {
    return prev != null ? current.StartDate.Value.Year - prev.StartDate.Value.Year : 0;
  }

  private static int GetYearDelta(IPeriodSetup current)
  {
    DateTime dateTime = current.EndDate.Value;
    int year1 = dateTime.Year;
    dateTime = current.StartDate.Value;
    int year2 = dateTime.Year;
    return year1 - year2;
  }

  private static bool IsEqual(IPeriodSetup op1, TPeriod op2)
  {
    if (op1 != null && (object) op2 != null)
      return op1.PeriodNbr == op2.PeriodNbr;
    return op1 == null && (object) op2 == null;
  }

  public virtual TPeriod createNextPeriod(TPeriod current)
  {
    TPeriod result = new TPeriod();
    return !this.fillNextPeriod(result, current) ? default (TPeriod) : result;
  }

  /// <summary>
  /// This function fills result based on aSetup (as template) and current year
  /// Start and End Date of result are adjusted using the following alorithm only month/date parts are used, aYear is used as year ;
  /// If there is year break inside aSetup period, Dates are adjusted accordingly
  /// </summary>
  /// <param name="result">Result of operation (must be not null)</param>
  /// <param name="aSetup">Setup - used as template. ReadOnly.</param>
  /// <param name="aYear">Year</param>
  private void CopyFrom(TPeriod result, IPeriodSetup aSetup, int aYear, PXGraph graph)
  {
    result.PeriodNbr = aSetup.PeriodNbr;
    int num = aYear + FiscalPeriodCreator<TYear, TPeriod>.GetYearDelta(aSetup);
    // ISSUE: variable of a boxed type
    __Boxed<TPeriod> local1 = (object) result;
    int year1 = aYear;
    DateTime dateTime1 = aSetup.StartDate.Value;
    int month1 = dateTime1.Month;
    int year2 = aYear;
    dateTime1 = aSetup.StartDate.Value;
    int month2 = dateTime1.Month;
    int val1_1 = DateTime.DaysInMonth(year2, month2);
    dateTime1 = aSetup.StartDate.Value;
    int day1 = dateTime1.Day;
    int day2 = Math.Min(val1_1, day1);
    DateTime? nullable1 = new DateTime?(new DateTime(year1, month1, day2));
    local1.StartDate = nullable1;
    // ISSUE: variable of a boxed type
    __Boxed<TPeriod> local2 = (object) result;
    int year3 = num;
    DateTime dateTime2 = aSetup.EndDate.Value;
    int month3 = dateTime2.Month;
    int year4 = num;
    dateTime2 = aSetup.EndDate.Value;
    int month4 = dateTime2.Month;
    int val1_2 = DateTime.DaysInMonth(year4, month4);
    dateTime2 = aSetup.EndDate.Value;
    int day3 = dateTime2.Day;
    int day4 = Math.Min(val1_2, day3);
    DateTime? nullable2 = new DateTime?(new DateTime(year3, month3, day4));
    local2.EndDate = nullable2;
    result.Descr = aSetup.Descr;
    result.FinYear = result.FinYear ?? this.finYear;
    result.FinPeriodID = result.FinYear + result.PeriodNbr;
    PXCache cach1 = graph.Caches[aSetup.GetType()];
    PXCache cach2 = graph.Caches[result.GetType()];
    IPeriodSetup periodSetup = aSetup;
    PXCache pxCache = cach2;
    // ISSUE: variable of a boxed type
    __Boxed<TPeriod> local3 = (object) result;
    PXDBLocalizableStringAttribute.CopyTranslations<FinPeriodSetup.descr, MasterFinPeriod.descr>(cach1, (object) periodSetup, pxCache, (object) local3);
  }

  private FiscalPeriodSetupCreator<TPeriod> FinPeriodCreator
  {
    get
    {
      if (this._finPeriodCreator == null)
      {
        DateTime yearStartDate = this.yearStartDate;
        this._finPeriodCreator = new FiscalPeriodSetupCreator<TPeriod>(this.yearSetup, FiscalPeriodSetupCreator.FormatYear(this.firstPeriodYear), this.periodsStartDate, yearStartDate);
      }
      return this._finPeriodCreator;
    }
  }
}
