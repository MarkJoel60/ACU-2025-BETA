// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FiscalPeriodSetupCreator`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL;

public class FiscalPeriodSetupCreator<TPeriodSetup> : FiscalPeriodSetupCreator where TPeriodSetup : class, IPeriodSetup, new()
{
  private const int FiscalYearLength = 1;
  private const short weekLength = 7;
  private bool hasAdjustmentPeriod;
  private FiscalPeriodSetupCreator.FPType periodType;
  private DateTime yearStartingDate;
  private DateTime yearEndingDate;
  private DateTime periodsStartDate;
  private DateTime periodsEndDate;
  private bool adjustStartDate;
  private short? customPeriodLength;
  private short numberOfPeriods;
  private short actualNumberOfPeriods;
  private bool fixedNumberOfPeriods;
  private bool fixedEndOfYear;
  private readonly string fiscalYear;

  public FiscalPeriodSetupCreator(IYearSetup yearSetup)
  {
    IYearSetup yearSetup1 = yearSetup;
    string firstFinYear = yearSetup.FirstFinYear;
    DateTime? nullable = yearSetup.PeriodsStartDate;
    DateTime aPeriodsStartDate = nullable.Value;
    nullable = yearSetup.BegFinYear;
    DateTime aYearStartDate = nullable.Value;
    // ISSUE: explicit constructor call
    this.\u002Ector(yearSetup1, firstFinYear, aPeriodsStartDate, aYearStartDate);
  }

  public FiscalPeriodSetupCreator(
    IYearSetup yearSetup,
    string aFiscalYear,
    DateTime aPeriodsStartDate,
    DateTime aYearStartDate)
    : this(aFiscalYear, aPeriodsStartDate, yearSetup.FPType, yearSetup.HasAdjustmentPeriod.Value, yearSetup.PeriodLength, aYearStartDate, yearSetup.EndYearCalcMethod, new int?(yearSetup.EndYearDayOfWeek.Value))
  {
    this.AdjustFiscalPeriodsCount(yearSetup);
  }

  public FiscalPeriodSetupCreator(
    string aFiscalYear,
    DateTime aPeriodsStartDate,
    FiscalPeriodSetupCreator.FPType aPeriodType,
    bool aHasAdjPeriod,
    short? aPeriodLength,
    DateTime aYearStartDate,
    string aEndYearCalcMethod,
    int? aEndYearDayOfWeek)
  {
    this.periodType = aPeriodType;
    this.hasAdjustmentPeriod = this.periodType != FiscalPeriodSetupCreator.FPType.Custom && aHasAdjPeriod;
    if (FiscalPeriodSetupCreator.IsFixedLengthPeriod(aPeriodType))
    {
      this.customPeriodLength = aPeriodType == FiscalPeriodSetupCreator.FPType.FixedLength ? aPeriodLength : FiscalPeriodSetupCreator.GetPeriodLength(aPeriodType);
      this.fixedNumberOfPeriods = false;
      this.fixedEndOfYear = false;
    }
    else
    {
      this.customPeriodLength = new short?();
      this.fixedNumberOfPeriods = true;
      this.fixedEndOfYear = true;
      short fiscalPeriodsNbr = FiscalPeriodSetupCreator<TPeriodSetup>.GetFiscalPeriodsNbr(this.periodType, this.hasAdjustmentPeriod);
      this.numberOfPeriods = fiscalPeriodsNbr > (short) 0 ? fiscalPeriodsNbr : (short) 0;
    }
    this.yearStartingDate = aYearStartDate;
    int? nullable;
    if (this.periodType == FiscalPeriodSetupCreator.FPType.Week && aEndYearCalcMethod != "CA")
    {
      switch (aEndYearCalcMethod)
      {
        case "LD":
          int num1 = 0;
          int dayOfWeek1 = (int) aYearStartDate.AddYears(1).DayOfWeek;
          nullable = aEndYearDayOfWeek;
          int valueOrDefault1 = nullable.GetValueOrDefault();
          DateTime dateTime1;
          if (!(dayOfWeek1 == valueOrDefault1 & nullable.HasValue))
          {
            dateTime1 = aYearStartDate.AddYears(1);
            int num2;
            if (dateTime1.DayOfWeek > (DayOfWeek) aEndYearDayOfWeek.Value)
            {
              dateTime1 = aYearStartDate.AddYears(1);
              num2 = (int) (dateTime1.DayOfWeek - aEndYearDayOfWeek.Value);
            }
            else
            {
              dateTime1 = aYearStartDate.AddYears(1);
              num2 = (int) (dateTime1.DayOfWeek - aEndYearDayOfWeek.Value + 7);
            }
            num1 = Math.Abs(num2) * -1;
          }
          dateTime1 = aYearStartDate.AddYears(1);
          this.yearEndingDate = dateTime1.AddDays((double) num1);
          this.periodsStartDate = aPeriodsStartDate;
          dateTime1 = this.periodsStartDate.AddYears(1);
          this.periodsEndDate = dateTime1.AddDays((double) num1);
          break;
        case "ND":
          int num3 = 0;
          int dayOfWeek2 = (int) aYearStartDate.AddYears(1).DayOfWeek;
          nullable = aEndYearDayOfWeek;
          int valueOrDefault2 = nullable.GetValueOrDefault();
          if (!(dayOfWeek2 == valueOrDefault2 & nullable.HasValue))
          {
            num3 = (int) (aEndYearDayOfWeek.Value - aYearStartDate.AddYears(1).DayOfWeek);
            if ((double) Math.Abs(num3) > Math.Floor(3.5))
              num3 = num3 > 0 ? num3 - 7 : num3 + 7;
          }
          DateTime dateTime2 = aYearStartDate.AddYears(1);
          this.yearEndingDate = dateTime2.AddDays((double) num3);
          this.periodsStartDate = aPeriodsStartDate;
          dateTime2 = this.periodsStartDate.AddYears(1);
          this.periodsEndDate = dateTime2.AddDays((double) num3);
          break;
      }
    }
    else
    {
      this.yearEndingDate = aYearStartDate.AddYears(1);
      this.periodsStartDate = aPeriodsStartDate;
      this.periodsEndDate = this.periodsStartDate.AddYears(1);
    }
    this.actualNumberOfPeriods = this.numberOfPeriods;
    if (!this.fixedEndOfYear && this.customPeriodLength.HasValue)
    {
      short? customPeriodLength = this.customPeriodLength;
      nullable = customPeriodLength.HasValue ? new int?((int) customPeriodLength.GetValueOrDefault()) : new int?();
      int num4 = 0;
      if (nullable.GetValueOrDefault() > num4 & nullable.HasValue)
      {
        TimeSpan timeSpan = this.yearEndingDate - this.periodsStartDate;
        short num5 = this.customPeriodLength.Value;
        short num6 = (short) (timeSpan.Days / (int) num5);
        if ((double) (timeSpan.Days % (int) num5) > Math.Floor((double) this.customPeriodLength.Value / 2.0))
          ++num6;
        this.periodsEndDate = this.periodsStartDate.AddDays((double) ((int) num6 * (int) num5));
        this.actualNumberOfPeriods = (short) ((int) num6 + (this.hasAdjustmentPeriod ? 1 : 0));
      }
    }
    if (aPeriodType == FiscalPeriodSetupCreator.FPType.FourFourFive || aPeriodType == FiscalPeriodSetupCreator.FPType.FourFiveFour || aPeriodType == FiscalPeriodSetupCreator.FPType.FiveFourFour)
    {
      this.periodsEndDate = (double) (this.yearEndingDate - this.periodsStartDate.AddDays(364.0)).Days > Math.Floor(3.5) ? this.periodsStartDate.AddDays(371.0) : this.periodsStartDate.AddDays(364.0);
      this.fixedNumberOfPeriods = true;
      this.actualNumberOfPeriods = this.numberOfPeriods = FiscalPeriodSetupCreator<TPeriodSetup>.GetFiscalPeriodsNbr(this.periodType, this.hasAdjustmentPeriod);
    }
    this.fiscalYear = aFiscalYear;
  }

  public FiscalPeriodSetupCreator(
    string aFiscalYear,
    DateTime aPeriodsStartingDate,
    short aNumberOfPeriods)
  {
    this.numberOfPeriods = aNumberOfPeriods > (short) 0 ? aNumberOfPeriods : (short) 1;
    this.customPeriodLength = new short?((short) Math.Round(366.0M / (Decimal) this.numberOfPeriods, 0, MidpointRounding.AwayFromZero));
    this.fixedNumberOfPeriods = true;
    this.periodsStartDate = aPeriodsStartingDate;
    this.periodsEndDate = this.periodsStartDate.AddYears(1);
    this.fiscalYear = aFiscalYear;
    this.periodType = FiscalPeriodSetupCreator.FPType.Custom;
    this.hasAdjustmentPeriod = false;
    this.actualNumberOfPeriods = this.numberOfPeriods;
  }

  public virtual bool fillNextPeriod(TPeriodSetup result, TPeriodSetup current)
  {
    int num1 = (object) current != null ? int.Parse(current.PeriodNbr) : 0;
    DateTime periodsEndDate;
    if ((object) current != null)
    {
      // ISSUE: variable of a boxed type
      __Boxed<TPeriodSetup> local = (object) result;
      periodsEndDate = current.EndDate.Value;
      int year = periodsEndDate.Year;
      periodsEndDate = current.EndDate.Value;
      int month = periodsEndDate.Month;
      periodsEndDate = current.EndDate.Value;
      int day = periodsEndDate.Day;
      DateTime? nullable = new DateTime?(new DateTime(year, month, day));
      local.StartDate = nullable;
    }
    else
      result.StartDate = new DateTime?(this.periodsStartDate);
    int num2 = num1 + 1;
    bool? custom1 = result.Custom;
    bool flag1;
    if ((custom1.HasValue ? new bool?(!custom1.GetValueOrDefault()) : new bool?()) ?? true)
    {
      DateTime? startDate = result.StartDate;
      periodsEndDate = this.periodsEndDate;
      flag1 = startDate.HasValue && startDate.GetValueOrDefault() >= periodsEndDate;
    }
    else
      flag1 = false;
    bool flag2 = this.fixedNumberOfPeriods && num2 == (int) this.numberOfPeriods;
    DateTime? nullable1 = result.EndDate;
    if (!nullable1.HasValue)
    {
      bool? custom2 = result.Custom;
      if ((custom2.HasValue ? new bool?(!custom2.GetValueOrDefault()) : new bool?()) ?? true)
      {
        // ISSUE: variable of a boxed type
        __Boxed<TPeriodSetup> local = (object) result;
        DateTime dateTime;
        if (!flag2)
        {
          int periodType = (int) this.periodType;
          nullable1 = result.StartDate;
          DateTime aStartDate = nullable1.Value;
          DateTime yearEndingDate = this.yearEndingDate;
          int valueOrDefault = (int) this.customPeriodLength.GetValueOrDefault();
          int aPeriodNum = num2;
          dateTime = FiscalPeriodSetupCreator<TPeriodSetup>.GetEndDate((FiscalPeriodSetupCreator.FPType) periodType, aStartDate, yearEndingDate, (short) valueOrDefault, aPeriodNum);
        }
        else
          dateTime = this.periodsEndDate;
        DateTime? nullable2 = new DateTime?(dateTime);
        local.EndDate = nullable2;
      }
      else
        result.EndDate = result.StartDate;
    }
    if (flag1)
    {
      if (!this.hasAdjustmentPeriod)
        return false;
      if ((object) current != null)
      {
        nullable1 = current.StartDate;
        DateTime? startDate = result.StartDate;
        if ((nullable1.HasValue == startDate.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == startDate.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
          return false;
      }
      result.EndDate = result.StartDate;
    }
    else
    {
      DateTime? endDate = result.EndDate;
      periodsEndDate = this.periodsEndDate;
      if ((endDate.HasValue ? (endDate.GetValueOrDefault() > periodsEndDate ? 1 : 0) : 0) != 0 && this.fixedEndOfYear || this.hasAdjustmentPeriod && this.fixedEndOfYear && num2 == (int) this.numberOfPeriods - 1)
        result.EndDate = new DateTime?(this.periodsEndDate);
    }
    result.PeriodNbr = num2.ToString("00");
    result.Descr = FiscalPeriodSetupCreator<TPeriodSetup>.GetPeriodDefaultDescr(this.periodType, result, this.hasAdjustmentPeriod);
    return true;
  }

  public virtual TPeriodSetup createNextPeriod(TPeriodSetup current)
  {
    TPeriodSetup result = new TPeriodSetup();
    return !this.fillNextPeriod(result, current) ? default (TPeriodSetup) : result;
  }

  public DateTime YearEnd => this.periodsEndDate;

  public short ActualNumberOfPeriods => this.actualNumberOfPeriods;

  public virtual DateTime CalcPeriodsStartDate(string aFiscalYear)
  {
    int num1 = int.Parse(this.fiscalYear);
    int num2 = int.Parse(aFiscalYear);
    if (num1 == num2)
      return this.periodsStartDate;
    int num3 = num2 - num1;
    DateTime dateTime = this.yearStartingDate.AddYears(num3);
    if (!FiscalPeriodSetupCreator.IsFixedLengthPeriod(this.periodType) || this.fixedEndOfYear)
      return this.periodsStartDate.AddYears(num3);
    TimeSpan timeSpan = dateTime - this.periodsStartDate;
    int num4 = (int) this.customPeriodLength.Value;
    int num5 = timeSpan.Days >= 0 ? 1 : -1;
    int num6 = timeSpan.Days * num5 / num4;
    if ((double) (timeSpan.Days * num5 % num4) > Math.Floor((double) this.customPeriodLength.Value / 2.0))
      ++num6;
    return this.periodsStartDate.AddDays((double) (num6 * num4 * num5));
  }

  public static DateTime GetEndDate(
    FiscalPeriodSetupCreator.FPType aType,
    DateTime aStartDate,
    DateTime aYearEndingDate,
    short aCustomLength,
    int aPeriodNum)
  {
    if (aType == FiscalPeriodSetupCreator.FPType.Month || aType == FiscalPeriodSetupCreator.FPType.BiMonth || aType == FiscalPeriodSetupCreator.FPType.Quarter)
    {
      int num;
      switch (aType)
      {
        case FiscalPeriodSetupCreator.FPType.Month:
          num = 1;
          break;
        case FiscalPeriodSetupCreator.FPType.BiMonth:
          num = 2;
          break;
        default:
          num = 3;
          break;
      }
      int months = num;
      return aStartDate.AddMonths(months);
    }
    if (FiscalPeriodSetupCreator.IsFixedLengthPeriod(aType))
    {
      int num = aType == FiscalPeriodSetupCreator.FPType.Custom || aType == FiscalPeriodSetupCreator.FPType.FixedLength ? (int) aCustomLength : (int) FiscalPeriodSetupCreator.GetPeriodLength(aType, aPeriodNum).Value;
      if ((aType == FiscalPeriodSetupCreator.FPType.FourFourFive || aType == FiscalPeriodSetupCreator.FPType.FourFiveFour || aType == FiscalPeriodSetupCreator.FPType.FiveFourFour) && aPeriodNum == 12 && (double) (aYearEndingDate - aStartDate.AddDays((double) num)).Days > Math.Floor(3.5))
        num += 7;
      return aStartDate.AddDays((double) num);
    }
    return aType == FiscalPeriodSetupCreator.FPType.Custom ? aStartDate.AddDays((double) aCustomLength) : aStartDate;
  }

  public static short GetFiscalPeriodsNbr(
    FiscalPeriodSetupCreator.FPType aType,
    bool hasAdjustemntPeriod)
  {
    short fiscalPeriodsNbr = -2;
    switch (aType)
    {
      case FiscalPeriodSetupCreator.FPType.Month:
        fiscalPeriodsNbr = (short) 12;
        break;
      case FiscalPeriodSetupCreator.FPType.BiMonth:
        fiscalPeriodsNbr = (short) 6;
        break;
      case FiscalPeriodSetupCreator.FPType.Quarter:
        fiscalPeriodsNbr = (short) 4;
        break;
      case FiscalPeriodSetupCreator.FPType.FourFourFive:
        fiscalPeriodsNbr = (short) 12;
        break;
      case FiscalPeriodSetupCreator.FPType.FourFiveFour:
        fiscalPeriodsNbr = (short) 12;
        break;
      case FiscalPeriodSetupCreator.FPType.FiveFourFour:
        fiscalPeriodsNbr = (short) 12;
        break;
      case FiscalPeriodSetupCreator.FPType.Custom:
        fiscalPeriodsNbr = (short) 1;
        break;
    }
    if (hasAdjustemntPeriod)
      ++fiscalPeriodsNbr;
    return fiscalPeriodsNbr;
  }

  private void AdjustFiscalPeriodsCount(IYearSetup yearSetup)
  {
    if (yearSetup.FPType != FiscalPeriodSetupCreator.FPType.Custom)
      return;
    this.actualNumberOfPeriods = yearSetup.FinPeriods ?? (short) 1;
  }

  /// <summary>
  /// Returns default description for a record;
  /// Algorithm require aRecord to be filled before calling of this function
  /// </summary>
  private static string GetPeriodDefaultDescr(
    FiscalPeriodSetupCreator.FPType aType,
    TPeriodSetup aRecord,
    bool aHasAdjustPeriod)
  {
    DateTime? endDate = aRecord.EndDate;
    DateTime? startDate = aRecord.StartDate;
    if (((endDate.HasValue == startDate.HasValue ? (endDate.HasValue ? (endDate.GetValueOrDefault() == startDate.GetValueOrDefault() ? 1 : 0) : 1) : 0) & (aHasAdjustPeriod ? 1 : 0)) != 0)
      return PXMessages.LocalizeNoPrefix("Adjustment Period");
    if (aType == FiscalPeriodSetupCreator.FPType.Month)
      return aRecord.StartDate.Value.ToString("MMMM");
    return aType == FiscalPeriodSetupCreator.FPType.Quarter ? PXMessages.LocalizeFormatNoPrefix("Quarter# {0}", new object[1]
    {
      (object) aRecord.PeriodNbr
    }) : PXMessages.LocalizeFormatNoPrefix("Period# {0}", new object[1]
    {
      (object) aRecord.PeriodNbr
    });
  }
}
