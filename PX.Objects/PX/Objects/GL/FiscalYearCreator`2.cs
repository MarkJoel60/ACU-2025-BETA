// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FiscalYearCreator`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

public class FiscalYearCreator<TYear, TPeriod>(
  IYearSetup aSetup,
  string aYear,
  DateTime? startDate,
  IEnumerable<IPeriodSetup> aPeriodSetup) : FiscalPeriodCreator<TYear, TPeriod>(aSetup, aYear, startDate, aPeriodSetup)
  where TYear : class, IYear, IBqlTable, new()
  where TPeriod : class, IPeriod, IBqlTable, new()
{
  public static bool CreateNextYear(IYearSetup setupValue, TYear lastYear, TYear newYear)
  {
    if (setupValue == null)
      throw new PXSetPropertyException("You must configure the Financial Year Settings first.");
    if ((object) lastYear != null)
    {
      DateTime? nullable = lastYear.EndDate;
      if (!nullable.HasValue)
      {
        IYearSetup yearSetup = setupValue;
        string year = lastYear.Year;
        nullable = lastYear.StartDate;
        DateTime aPeriodsStartDate = nullable.Value;
        nullable = setupValue.BegFinYear;
        DateTime aYearStartDate = nullable.Value;
        FiscalPeriodSetupCreator<TPeriod> periodSetupCreator = new FiscalPeriodSetupCreator<TPeriod>(yearSetup, year, aPeriodsStartDate, aYearStartDate);
        newYear.StartDate = new DateTime?(periodSetupCreator.YearEnd);
      }
      else
        newYear.StartDate = lastYear.EndDate;
      int aYear = int.Parse(lastYear.Year) + 1;
      string aYearNumber = FiscalPeriodSetupCreator.FormatYear(aYear);
      DateTime dateTime = FiscalYearCreator<TYear, TPeriod>.CalcYearStartDate(setupValue, aYearNumber);
      IYearSetup yearSetup1 = setupValue;
      string aFiscalYear = aYearNumber;
      nullable = newYear.StartDate;
      DateTime aPeriodsStartDate1 = nullable.Value;
      DateTime aYearStartDate1 = dateTime;
      FiscalPeriodSetupCreator<TPeriod> periodSetupCreator1 = new FiscalPeriodSetupCreator<TPeriod>(yearSetup1, aFiscalYear, aPeriodsStartDate1, aYearStartDate1);
      newYear.EndDate = new DateTime?(periodSetupCreator1.YearEnd);
      newYear.FinPeriods = new short?(periodSetupCreator1.ActualNumberOfPeriods);
      newYear.Year = FiscalPeriodSetupCreator.FormatYear(aYear);
    }
    else
      FiscalYearCreator<TYear, TPeriod>.CreateFromSetup(setupValue, newYear);
    return true;
  }

  public static bool CreatePrevYear(IYearSetup setupValue, TYear firstYear, TYear newYear)
  {
    if (setupValue == null)
      throw new PXSetPropertyException("You must configure the Financial Year Settings first.");
    int num = string.IsNullOrEmpty(setupValue.FirstFinYear) ? setupValue.BegFinYear.Value.Year : int.Parse(setupValue.FirstFinYear);
    if ((object) firstYear != null)
    {
      int aYear = int.Parse(firstYear.Year) - 1;
      if (aYear < num)
        return false;
      FiscalPeriodSetupCreator<FinPeriodSetup> periodSetupCreator1 = new FiscalPeriodSetupCreator<FinPeriodSetup>(setupValue);
      string str = FiscalPeriodSetupCreator.FormatYear(aYear);
      string aFiscalYear = str;
      DateTime dateTime = periodSetupCreator1.CalcPeriodsStartDate(aFiscalYear);
      DateTime aYearStartDate = FiscalYearCreator<TYear, TPeriod>.CalcYearStartDate(setupValue, str);
      newYear.StartDate = new DateTime?(dateTime);
      FiscalPeriodSetupCreator<FinPeriodSetup> periodSetupCreator2 = new FiscalPeriodSetupCreator<FinPeriodSetup>(setupValue, str, newYear.StartDate.Value, aYearStartDate);
      newYear.EndDate = new DateTime?(periodSetupCreator2.YearEnd);
      newYear.FinPeriods = new short?(periodSetupCreator2.ActualNumberOfPeriods);
      newYear.Year = FiscalPeriodSetupCreator.FormatYear(aYear);
    }
    else
      FiscalYearCreator<TYear, TPeriod>.CreateFromSetup(setupValue, newYear);
    return true;
  }

  public static void CreateFromSetup(IYearSetup setupValue, TYear newYear)
  {
    FiscalPeriodSetupCreator<TPeriod> periodSetupCreator = new FiscalPeriodSetupCreator<TPeriod>(setupValue);
    // ISSUE: variable of a boxed type
    __Boxed<TYear> local = (object) newYear;
    DateTime dateTime = setupValue.PeriodsStartDate.Value;
    int year = dateTime.Year;
    dateTime = setupValue.PeriodsStartDate.Value;
    int month = dateTime.Month;
    dateTime = setupValue.PeriodsStartDate.Value;
    int day = dateTime.Day;
    DateTime? nullable = new DateTime?(new DateTime(year, month, day));
    local.StartDate = nullable;
    newYear.EndDate = new DateTime?(periodSetupCreator.YearEnd);
    newYear.Year = setupValue.FirstFinYear;
    newYear.FinPeriods = FiscalPeriodSetupCreator.IsFixedLengthPeriod(setupValue.FPType) ? new short?(periodSetupCreator.ActualNumberOfPeriods) : setupValue.FinPeriods;
  }

  public static TYear CreateNextYear(
    PXGraph graph,
    IYearSetup setupValue,
    IEnumerable<IPeriodSetup> periods,
    TYear lastYear)
  {
    TYear newYear = new TYear();
    FiscalYearCreator<TYear, TPeriod>.CreateNextYear(setupValue, lastYear, newYear);
    TYear nextYear;
    if ((object) (nextYear = (TYear) graph.Caches[typeof (TYear)].Insert((object) newYear)) != null)
      FiscalPeriodCreator<TYear, TPeriod>.CreatePeriods(graph.Caches[typeof (TPeriod)], periods, nextYear.Year, nextYear.StartDate, setupValue);
    return nextYear;
  }

  public static TYear CreatePrevYear(
    PXGraph graph,
    IYearSetup setupValue,
    IEnumerable<IPeriodSetup> periods,
    TYear firstYear)
  {
    TYear newYear = new TYear();
    FiscalYearCreator<TYear, TPeriod>.CreatePrevYear(setupValue, firstYear, newYear);
    TYear prevYear;
    if ((object) (prevYear = (TYear) graph.Caches[typeof (TYear)].Insert((object) newYear)) != null)
      FiscalPeriodCreator<TYear, TPeriod>.CreatePeriods(graph.Caches[typeof (TPeriod)], periods, prevYear.Year, prevYear.StartDate, setupValue);
    return prevYear;
  }

  public static DateTime CalcYearStartDate(IYearSetup aSetup, string aYearNumber)
  {
    int num = aSetup.BegFinYear.Value.Year - int.Parse(aSetup.FirstFinYear);
    int year = int.Parse(aYearNumber) + num;
    DateTime dateTime = aSetup.BegFinYear.Value;
    int month = dateTime.Month;
    dateTime = aSetup.BegFinYear.Value;
    int day = dateTime.Day;
    return new DateTime(year, month, day);
  }
}
