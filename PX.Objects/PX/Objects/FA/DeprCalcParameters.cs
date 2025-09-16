// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DeprCalcParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FA;

public class DeprCalcParameters
{
  public const int MonthsInYear = 12;
  public const int MonthsInQuarter = 3;
  public const int QuartersInYear = 4;
  protected DateTime? recoveryEndDate;
  protected string DeprToYear;
  protected string DeprFromYear;
  protected SortedList<string, FABookPeriod> Periods;
  protected SortedList<int, FABookYear> Years;

  public DeprCalcParameters Fill(
    PXGraph graph,
    FABookBalance balance,
    FABook book = null,
    FADepreciationMethod depreciationMethod = null,
    DateTime? recoveryEndDate = null)
  {
    if (!balance.BookID.HasValue || !balance.DeprFromDate.HasValue)
      return this;
    PXGraph graph1 = graph;
    int? nullable = balance.BookID;
    int bookID = nullable.Value;
    nullable = balance.AssetID;
    int assetID = nullable.Value;
    int? depreciationMethodId = balance.DepreciationMethodID;
    int num = balance.Depreciate.GetValueOrDefault() ? 1 : 0;
    Decimal valueOrDefault1 = balance.UsefulLife.GetValueOrDefault();
    Decimal valueOrDefault2 = balance.PercentPerYear.GetValueOrDefault();
    string averagingConvention = balance.AveragingConvention;
    string midMonthType = balance.MidMonthType;
    int valueOrDefault3 = (int) balance.MidMonthDay.GetValueOrDefault();
    DateTime depreciationStartDate = balance.DeprFromDate.Value;
    DateTime? deprToDate = balance.DeprToDate;
    DateTime? recoveryEndDate1 = recoveryEndDate;
    FABook book1 = book;
    FADepreciationMethod depreciationMethod1 = depreciationMethod;
    string deprFromYear = balance.DeprFromYear;
    string deprToYear = balance.DeprToYear;
    return this.Fill(graph1, bookID, assetID, depreciationMethodId, num != 0, valueOrDefault1, valueOrDefault2, averagingConvention, midMonthType, (short) valueOrDefault3, depreciationStartDate, deprToDate, recoveryEndDate1, book1, depreciationMethod1, deprFromYear, deprToYear);
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  public DeprCalcParameters Fill(
    PXGraph graph,
    int bookID,
    int assetID,
    int? methodID,
    bool depreciate,
    Decimal usefulLife,
    string averagingConvention,
    string midMonthType,
    short midMonthDay,
    DateTime depreciationStartDate,
    DateTime? depreciationStopDate,
    DateTime? recoveryEndDate = null,
    FABook book = null,
    FADepreciationMethod depreciationMethod = null,
    string deprFromYear = null,
    string deprToYear = null)
  {
    return this.Fill(graph, bookID, assetID, methodID, depreciate, usefulLife, 0M, averagingConvention, midMonthType, midMonthDay, depreciationStartDate, depreciationStopDate, recoveryEndDate, book, depreciationMethod, deprFromYear, deprToYear);
  }

  public DeprCalcParameters Fill(
    PXGraph graph,
    int bookID,
    int assetID,
    int? methodID,
    bool depreciate,
    Decimal usefulLife,
    Decimal percentPerYear,
    string averagingConvention,
    string midMonthType,
    short midMonthDay,
    DateTime depreciationStartDate,
    DateTime? depreciationStopDate,
    DateTime? recoveryEndDate = null,
    FABook book = null,
    FADepreciationMethod depreciationMethod = null,
    string deprFromYear = null,
    string deprToYear = null)
  {
    return this.Init(bookID, assetID, methodID, depreciate, usefulLife, percentPerYear, averagingConvention, midMonthType, midMonthDay, depreciationStartDate, depreciationStopDate, recoveryEndDate, book, depreciationMethod, deprFromYear, deprToYear).SelectFromDatabase(graph).Calculate();
  }

  public int BookID { get; set; }

  public int AssetID { get; set; }

  public int? DepreciationMethodID { get; set; }

  public Decimal UsefulLife { get; set; }

  public Decimal PercentPerYear { get; set; }

  public string AveragingConvention { get; set; }

  public string MidMonthType { get; set; }

  public short MidMonthDay { get; set; }

  public bool Depreciate { get; set; }

  public FABook Book { get; set; }

  public FADepreciationMethod DepreciationMethod { get; set; }

  protected IYearSetup YearSetup { get; set; }

  public IEnumerable<FABookPeriod> DeprPeriods
  {
    get
    {
      int num = this.Periods.IndexOfKey(this.DepreciationStartBookPeriod.FinPeriodID);
      int stopIdx = this.Periods.IndexOfKey(this.DepreciationStopBookPeriod.FinPeriodID);
      for (int i = num; i <= stopIdx; ++i)
        yield return this.Periods.Values[i];
    }
  }

  /// <summary>
  /// The integer field that contains the number of the period when depreciation has been started.
  /// The value of the field does not depend on the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.AveragingConvention" /> parameter.
  /// </summary>
  /// <value>
  /// Is calculated from the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.DepreciationStartBookPeriod" /> parameter.
  /// </value>
  public int DepreciationStartPeriod { get; set; }

  /// <summary>
  /// The integer field that contains the number of the period when depreciation is to be stopped.
  /// </summary>
  /// <value>
  /// Generally, the value is taken from the <see cref="P:PX.Objects.FA.DeprCalcParameters.RecoveryEndPeriod" /> parameter.
  /// But if the <see cref="P:PX.Objects.FA.DeprCalcParameters.DepreciationStopDate" /> parameter is overridden during
  /// <see cref="M:PX.Objects.FA.DepreciationCalculation.CalculateDepreciationAddition(PX.Objects.FA.FixedAsset,PX.Objects.FA.FABookBalance,PX.Objects.FA.FADepreciationMethod,PX.Objects.FA.FABookHistory)"> calculation of depreciation additions</see>,
  /// the value is calculated from the <see cref="P:PX.Objects.FA.DeprCalcParameters.DepreciationStopBookPeriod" /> parameter.
  /// </value>
  public int DepreciationStopPeriod { get; set; }

  /// <summary>
  /// The date when depreciation was started.
  /// The value of the field depends on the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.AveragingConvention" /> parameter.
  /// </summary>
  /// <value>
  /// Is calculated from the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.RecoveryStartBookPeriod" /> parameter.
  /// The default value is taken from the <see cref="P:PX.Objects.FA.DeprCalcParameters.DepreciationStartDate" /> parameter.
  /// </value>
  public DateTime RecoveryStartDate { get; set; }

  /// <summary>The date when depreciation was stopped.</summary>
  /// <value>
  /// Generally, the value is calculated by the <see cref="M:PX.Objects.FA.DeprCalcParameters.GetDatePlusYears(System.DateTime,System.Decimal)" /> method with
  /// <see cref="P:PX.Objects.FA.DeprCalcParameters.RecoveryStartDate" /> and <see cref="P:PX.Objects.FA.DeprCalcParameters.UsefulLife" /> parameters.
  /// But if the <see cref="F:PX.Objects.FA.DeprCalcParameters.recoveryEndDate" /> parameter is overridden during
  /// <see cref="M:PX.Objects.FA.DepreciationCalculation.CalculateDepreciationAddition(PX.Objects.FA.FixedAsset,PX.Objects.FA.FABookBalance,PX.Objects.FA.FADepreciationMethod,PX.Objects.FA.FABookHistory)"> calculation of depreciation additions</see>,
  /// the value is equal to the new value of this parameter.
  /// </value>
  public DateTime RecoveryEndDate { get; set; } = DateTime.MinValue;

  /// <summary>
  /// The period of a book when depreciation was started.
  /// The value of the field depends on the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.AveragingConvention" /> parameter.
  /// </summary>
  /// <value>
  /// Is calculated from the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.AveragingConvention" /> parameter.
  /// The default value is taken from the <see cref="P:PX.Objects.FA.DeprCalcParameters.DepreciationStartBookPeriod" /> parameter.
  /// </value>
  public FABookPeriod RecoveryStartBookPeriod { get; set; }

  /// <summary>The period of a book when depreciation was stopped.</summary>
  /// <value>
  /// Is calculated from the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.RecoveryEndDate" /> parameter.
  /// </value>
  public FABookPeriod RecoveryEndBookPeriod { get; set; }

  /// <summary>
  /// The period of a book when depreciation is to be started.
  /// The value of the field does not depend on the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.AveragingConvention" /> parameter.
  /// </summary>
  /// <value>
  /// Is calculated from the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.DepreciationStartDate" /> parameter.
  /// </value>
  public FABookPeriod DepreciationStartBookPeriod { get; set; }

  /// <summary>
  /// The period of a book when depreciation is to be stopped.
  /// </summary>
  /// <value>
  /// Generally, the value is taken from the <see cref="P:PX.Objects.FA.DeprCalcParameters.RecoveryEndBookPeriod" /> parameter.
  /// But if the <see cref="P:PX.Objects.FA.DeprCalcParameters.DepreciationStopDate" /> parameter is overridden during
  /// <see cref="M:PX.Objects.FA.DepreciationCalculation.CalculateDepreciationAddition(PX.Objects.FA.FixedAsset,PX.Objects.FA.FABookBalance,PX.Objects.FA.FADepreciationMethod,PX.Objects.FA.FABookHistory)"> calculation of depreciation additions</see>,
  /// the value is calculated from the new value of this parameter.
  /// </value>
  public FABookPeriod DepreciationStopBookPeriod { get; set; }

  /// <summary>
  /// The integer field that contains the year when depreciation is to be started.
  /// The value of the field does not depend on the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.AveragingConvention" /> parameter.
  /// </summary>
  /// <value>
  /// Is calculated from the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.DepreciationStartBookPeriod" /> parameter.
  /// </value>
  public int DepreciationStartYear { get; set; }

  /// <summary>
  /// The number of periods in the current year.
  /// This parameter can be changed dynamically during
  /// <see cref="M:PX.Objects.FA.DepreciationCalculation.CalculateDepreciation(PX.Objects.FA.FADepreciationMethod,PX.Objects.FA.FABookBalance,System.Nullable{System.DateTime})">
  /// depreciation calculation process</see>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.FA.FABookYear.FinPeriods" /> field.
  /// </value>
  public int DepreciationPeriodsInYear { get; set; } = 12;

  /// <summary>The depreciation basis.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.FA.FABookBalance.YtdDeprBase" /> field.
  /// </value>
  public Decimal DepreciationBasis { get; set; }

  /// <summary>
  /// The currently accumulated depreciation amount.
  /// This parameter can be changed dynamically during depreciation calculation process.
  /// </summary>
  public Decimal AccumulatedDepreciation { get; set; }

  /// <summary>
  /// The integer field that specifies the period when depreciation was stopped.
  /// </summary>
  /// <value>
  /// Is calculated from the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.RecoveryEndBookPeriod" /> parameter.
  /// </value>
  public int RecoveryEndPeriod { get; set; }

  /// <summary>
  /// The integer field that specifies the number of the period from which the quarter
  /// of the <see cref="P:PX.Objects.FA.DeprCalcParameters.RecoveryEndBookPeriod" /> period is started.
  /// This parameter is used only for the <see cref="F:PX.Objects.FA.FAAveragingConvention.FullQuarter" /> and
  /// <see cref="F:PX.Objects.FA.FAAveragingConvention.HalfQuarter" /> averaging conventions.
  /// </summary>
  /// <value>
  /// The value of the field depends on the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.AveragingConvention" /> parameter
  /// and calculated inside the <see cref="M:PX.Objects.FA.DeprCalcParameters.Calculate" /> method.
  /// </value>
  public int FirstRecoveryEndQuarterPeriod { get; set; }

  /// <summary>
  /// The integer field that specifies the number of the period from which the quarter
  /// of the <see cref="P:PX.Objects.FA.DeprCalcParameters.DepreciationStopBookPeriod" /> period is started.
  /// This parameter is used only for the <see cref="F:PX.Objects.FA.FAAveragingConvention.FullQuarter" /> and
  /// <see cref="F:PX.Objects.FA.FAAveragingConvention.HalfQuarter" /> averaging conventions.
  /// </summary>
  /// <value>
  /// The value of the field depends on the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.AveragingConvention" /> parameter
  /// and calculated inside the <see cref="M:PX.Objects.FA.DeprCalcParameters.Calculate" /> method.
  /// </value>
  public int FirstDepreciationStopQuarterPeriod { get; set; }

  /// <summary>
  /// The integer field that specifies the number of the period by which the quarter
  /// of the <see cref="P:PX.Objects.FA.DeprCalcParameters.DepreciationStartBookPeriod" /> period is finished.
  /// This parameter is used only for the <see cref="F:PX.Objects.FA.FAAveragingConvention.FullQuarter" /> and
  /// <see cref="F:PX.Objects.FA.FAAveragingConvention.HalfQuarter" /> averaging conventions.
  /// </summary>
  /// <value>
  /// The value of the field depends on the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.AveragingConvention" /> parameter
  /// and calculated inside the <see cref="M:PX.Objects.FA.DeprCalcParameters.Calculate" /> method.
  /// </value>
  public int LastDepreciationStartQuarterPeriod { get; set; }

  /// <summary>
  /// The number of years between <see cref="P:PX.Objects.FA.DeprCalcParameters.RecoveryEndBookPeriod" /> and
  /// <see cref="P:PX.Objects.FA.DeprCalcParameters.DepreciationStartBookPeriod" /> periods.
  /// </summary>
  /// <value>
  /// The value of the field depends on the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.AveragingConvention" /> parameter
  /// and calculated inside the <see cref="M:PX.Objects.FA.DeprCalcParameters.Calculate" /> method.
  /// </value>
  public int RecoveryYears { get; set; }

  /// <summary>The number of years for depreciation.</summary>
  /// <value>
  /// Generally, the value is taken from the <see cref="P:PX.Objects.FA.DeprCalcParameters.RecoveryYears" /> parameter.
  /// But if the <see cref="P:PX.Objects.FA.DeprCalcParameters.DepreciationStopDate" /> parameter is overridden during
  /// <see cref="M:PX.Objects.FA.DepreciationCalculation.CalculateDepreciationAddition(PX.Objects.FA.FixedAsset,PX.Objects.FA.FABookBalance,PX.Objects.FA.FADepreciationMethod,PX.Objects.FA.FABookHistory)"> calculation of depreciation additions</see>,
  /// the value is calculated as the number of years between the <see cref="P:PX.Objects.FA.DeprCalcParameters.DepreciationStopBookPeriod" />
  /// and <see cref="P:PX.Objects.FA.DeprCalcParameters.DepreciationStartBookPeriod" /> periods.
  /// </value>
  public int DepreciationYears { get; set; }

  /// <summary>
  /// The decimal value that specifies which part of <see cref="P:PX.Objects.FA.DeprCalcParameters.DepreciationStartPeriod" /> should be used for calculations.
  /// This parameter is used only for <see cref="F:PX.Objects.FA.FAAveragingConvention.ModifiedPeriod" /> and
  /// <see cref="F:PX.Objects.FA.FAAveragingConvention.ModifiedPeriod2" /> averaging conventions.
  /// </summary>
  /// <value>
  /// See the <see cref="M:PX.Objects.FA.DeprCalcParameters.Calculate" /> method.
  /// Possible values for <see cref="F:PX.Objects.FA.FAAveragingConvention.ModifiedPeriod" /> averaging convention:
  /// 0.5m, 1m
  /// Possible values for <see cref="F:PX.Objects.FA.FAAveragingConvention.ModifiedPeriod2" /> averaging convention:
  /// 0m, 1m
  /// </value>
  public Decimal StartDepreciationMidPeriodRatio { get; set; }

  /// <summary>
  /// The decimal value that specifies which part of <see cref="P:PX.Objects.FA.DeprCalcParameters.DepreciationStopPeriod" /> should be used for calculations.
  /// This parameter is used only for <see cref="F:PX.Objects.FA.FAAveragingConvention.ModifiedPeriod" /> and
  /// <see cref="F:PX.Objects.FA.FAAveragingConvention.ModifiedPeriod2" /> averaging conventions.
  /// </summary>
  /// <value>
  /// See the <see cref="M:PX.Objects.FA.DeprCalcParameters.Calculate" /> method.
  /// For the <see cref="F:PX.Objects.FA.FAAveragingConvention.ModifiedPeriod" /> averaging convention, the value is always equal to
  /// the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.StartDepreciationMidPeriodRatio" /> parameter.
  /// For the <see cref="F:PX.Objects.FA.FAAveragingConvention.ModifiedPeriod2" /> averaging convention, the value is always equal to 1m.
  /// </value>
  public Decimal StopDepreciationMidPeriodRatio { get; set; }

  /// <summary>
  /// The date when depreciation is to be started.
  /// The value of the field does not depend on the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.AveragingConvention" /> parameter.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.FA.FABookBalance.DeprFromDate" /> field.
  /// </value>
  public DateTime DepreciationStartDate { get; set; }

  /// <summary>The date when depreciation is to be stopped.</summary>
  /// <value>
  /// Generally, the value is taken from the <see cref="P:PX.Objects.FA.DeprCalcParameters.RecoveryEndDate" /> parameter.
  /// The value can be overridden during <see cref="M:PX.Objects.FA.DepreciationCalculation.CalculateDepreciationAddition(PX.Objects.FA.FixedAsset,PX.Objects.FA.FABookBalance,PX.Objects.FA.FADepreciationMethod,PX.Objects.FA.FABookHistory)"> calculation for depreciation additions</see>.
  /// In this case, the <see cref="P:PX.Objects.FA.DeprCalcParameters.DepreciationStopBookPeriod" /> parameter is calculated from this new value.
  /// </value>
  public DateTime? DepreciationStopDate { get; set; }

  /// <summary>
  /// The number of days between <see cref="P:PX.Objects.FA.DeprCalcParameters.RecoveryStartDate" /> and
  /// <see cref="P:PX.Objects.FA.DeprCalcParameters.RecoveryStartDate" /> dates.
  /// This parameter is used only for <see cref="F:PX.Objects.FA.FAAveragingConvention.FullDay" /> averaging convention.
  /// </summary>
  /// <value>
  /// The value of the field depends on the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.AveragingConvention" /> parameter
  /// and calculated inside the <see cref="M:PX.Objects.FA.DeprCalcParameters.Calculate" /> method.
  /// </value>
  public int WholeRecoveryDays { get; set; }

  /// <summary>
  /// The number of periods between <see cref="P:PX.Objects.FA.DeprCalcParameters.RecoveryStartBookPeriod" /> and
  /// <see cref="P:PX.Objects.FA.DeprCalcParameters.RecoveryEndBookPeriod" /> periods.
  /// </summary>
  /// <value>
  /// The value of the field depends on the value of the <see cref="P:PX.Objects.FA.DeprCalcParameters.AveragingConvention" /> parameter
  /// and calculated inside the <see cref="M:PX.Objects.FA.DeprCalcParameters.Calculate" /> method.
  /// </value>
  public Decimal WholeRecoveryPeriods { get; set; }

  /// <summary>
  /// The current year of useful life.
  /// This parameter can be changed dynamically during depreciation calculation process.
  /// </summary>
  public int YearOfUsefulLife { get; set; } = 1;

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  protected DeprCalcParameters Init(
    int bookID,
    int assetID,
    int? methodID,
    bool depreciate,
    Decimal usefulLife,
    string averagingConvention,
    string midMonthType,
    short midMonthDay,
    DateTime depreciationStartDate,
    DateTime? depreciationStopDate,
    DateTime? recoveryEndDate = null,
    FABook book = null,
    FADepreciationMethod depreciationMethod = null,
    string deprFromYear = null,
    string deprToYear = null)
  {
    return this.Init(bookID, assetID, methodID, depreciate, usefulLife, 0M, averagingConvention, midMonthType, midMonthDay, depreciationStartDate, depreciationStopDate, recoveryEndDate, book, depreciationMethod, deprFromYear, deprToYear);
  }

  protected DeprCalcParameters Init(
    int bookID,
    int assetID,
    int? methodID,
    bool depreciate,
    Decimal usefulLife,
    Decimal percentPerYear,
    string averagingConvention,
    string midMonthType,
    short midMonthDay,
    DateTime depreciationStartDate,
    DateTime? depreciationStopDate,
    DateTime? recoveryEndDate = null,
    FABook book = null,
    FADepreciationMethod depreciationMethod = null,
    string deprFromYear = null,
    string deprToYear = null)
  {
    this.BookID = (int?) book?.BookID ?? bookID;
    this.AssetID = assetID;
    this.DepreciationMethodID = (int?) depreciationMethod?.MethodID ?? methodID;
    this.Depreciate = depreciate;
    this.DepreciationStartDate = depreciationStartDate;
    this.DepreciationStopDate = depreciationStopDate;
    this.PercentPerYear = percentPerYear;
    this.UsefulLife = !(depreciationMethod?.DepreciationMethod == "PC") && !(depreciationMethod?.DepreciationMethod == "ZL") && !(depreciationMethod?.DepreciationMethod == "LE") || !(percentPerYear > 0M) ? usefulLife : 100M / this.PercentPerYear;
    switch (depreciationMethod?.DepreciationMethod)
    {
      case "PC":
        this.AveragingConvention = "FD";
        break;
      case "ZL":
      case "LE":
        this.AveragingConvention = "FP";
        break;
      default:
        this.AveragingConvention = averagingConvention;
        break;
    }
    this.MidMonthType = midMonthType;
    this.MidMonthDay = midMonthDay;
    this.Book = book;
    this.DepreciationMethod = depreciationMethod;
    this.recoveryEndDate = recoveryEndDate;
    this.DeprFromYear = deprFromYear;
    this.DeprToYear = deprToYear;
    this.DepreciationStartPeriod = 0;
    this.DepreciationStopPeriod = 0;
    this.RecoveryStartDate = DateTime.MinValue;
    this.RecoveryEndDate = DateTime.MinValue;
    this.RecoveryStartBookPeriod = (FABookPeriod) null;
    this.RecoveryEndBookPeriod = (FABookPeriod) null;
    this.DepreciationStartBookPeriod = (FABookPeriod) null;
    this.DepreciationStopBookPeriod = (FABookPeriod) null;
    this.DepreciationStartYear = 0;
    this.DepreciationPeriodsInYear = 12;
    this.DepreciationBasis = 0M;
    this.AccumulatedDepreciation = 0M;
    this.RecoveryEndPeriod = 0;
    this.FirstRecoveryEndQuarterPeriod = 0;
    this.FirstDepreciationStopQuarterPeriod = 0;
    this.LastDepreciationStartQuarterPeriod = 0;
    this.RecoveryYears = 0;
    this.DepreciationYears = 0;
    this.StartDepreciationMidPeriodRatio = 0M;
    this.StopDepreciationMidPeriodRatio = 0M;
    this.WholeRecoveryDays = 0;
    this.WholeRecoveryPeriods = 0M;
    this.YearOfUsefulLife = 1;
    return this;
  }

  private string GetDBPeriodYearByDate(PXGraph graph, DateTime date)
  {
    return graph.GetService<IFABookPeriodRepository>().FindFABookPeriodOfDate(new DateTime?(date), new int?(this.BookID), new int?(this.AssetID))?.FinYear ?? date.Year.ToString("0000");
  }

  protected virtual DeprCalcParameters SelectFromDatabase(PXGraph graph)
  {
    FABook faBook = this.Book;
    if (faBook == null)
      faBook = PXResultset<FABook>.op_Implicit(PXSelectBase<FABook, PXSelect<FABook, Where<FABook.bookID, Equal<Required<FABook.bookID>>>>.Config>.Select(graph, new object[1]
      {
        (object) this.BookID
      }));
    this.Book = faBook;
    FADepreciationMethod depreciationMethod = this.DepreciationMethod;
    if (depreciationMethod == null)
      depreciationMethod = PXResultset<FADepreciationMethod>.op_Implicit(PXSelectBase<FADepreciationMethod, PXSelect<FADepreciationMethod, Where<FADepreciationMethod.methodID, Equal<Required<FADepreciationMethod.methodID>>>>.Config>.Select(graph, new object[1]
      {
        (object) this.DepreciationMethodID
      }));
    this.DepreciationMethod = depreciationMethod;
    int? nullable = new int?(graph.GetService<IFABookPeriodRepository>().GetFABookPeriodOrganizationID(new int?(this.BookID), new int?(this.AssetID)));
    PXSelectBase<FABookPeriod> pxSelectBase1 = (PXSelectBase<FABookPeriod>) new PXSelectReadonly<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>>>>, OrderBy<Asc<FABookPeriod.finPeriodID>>>(graph);
    PXSelectBase<FABookYear> pxSelectBase2 = (PXSelectBase<FABookYear>) new PXSelectReadonly<FABookYear, Where<FABookYear.bookID, Equal<Required<FABookYear.bookID>>, And<FABookYear.organizationID, Equal<Required<FABookYear.organizationID>>>>, OrderBy<Asc<FABookYear.year>>>(graph);
    List<object> objectList1 = new List<object>()
    {
      (object) this.BookID,
      (object) nullable
    };
    string periodYearByDate1;
    try
    {
      periodYearByDate1 = this.GetDBPeriodYearByDate(graph, this.DepreciationStartDate);
    }
    catch (PXFABookPeriodException ex)
    {
      throw new PXException("The book calendar corresponding to the start date of the asset's useful life ({0:d}) is missing in the {1} book. Create a calendar on the Generate Book Calendars (FA501000) form.", new object[2]
      {
        (object) this.DepreciationStartDate,
        (object) this.Book.BookCode
      });
    }
    int num;
    if (!string.IsNullOrEmpty(periodYearByDate1))
    {
      pxSelectBase1.WhereAnd<Where<FABookPeriod.finYear, GreaterEqual<Required<FABookPeriod.finYear>>>>();
      pxSelectBase2.WhereAnd<Where<FABookYear.year, GreaterEqual<Required<FABookYear.year>>>>();
      List<object> objectList2 = objectList1;
      num = int.Parse(periodYearByDate1) - 1;
      string str = num.ToString();
      objectList2.Add((object) str);
    }
    DateTime date = this.recoveryEndDate ?? DeprCalcParameters.GetDatePlusYears(this.DepreciationStartDate, this.UsefulLife);
    string periodYearByDate2;
    try
    {
      periodYearByDate2 = this.GetDBPeriodYearByDate(graph, date);
    }
    catch (PXFABookPeriodException ex)
    {
      throw new PXException("The book calendar corresponding to the end date of the asset's useful life ({0:d}) is missing in the {1} book. Create a calendar on the Generate Book Calendars (FA501000) form.", new object[2]
      {
        (object) date,
        (object) this.Book.BookCode
      });
    }
    if (!string.IsNullOrEmpty(periodYearByDate2))
    {
      pxSelectBase1.WhereAnd<Where<FABookPeriod.finYear, LessEqual<Required<FABookPeriod.finYear>>>>();
      pxSelectBase2.WhereAnd<Where<FABookYear.year, LessEqual<Required<FABookYear.year>>>>();
      List<object> objectList3 = objectList1;
      num = int.Parse(periodYearByDate2) + 1;
      string str = num.ToString();
      objectList3.Add((object) str);
    }
    this.Periods = new SortedList<string, FABookPeriod>((IDictionary<string, FABookPeriod>) GraphHelper.RowCast<FABookPeriod>((IEnumerable) pxSelectBase1.Select(objectList1.ToArray())).ToDictionary<FABookPeriod, string>((Func<FABookPeriod, string>) (period => period.FinPeriodID)));
    this.Years = new SortedList<int, FABookYear>((IDictionary<int, FABookYear>) GraphHelper.RowCast<FABookYear>((IEnumerable) pxSelectBase2.Select(objectList1.ToArray())).ToDictionary<FABookYear, int>((Func<FABookYear, int>) (year => int.Parse(year.Year))));
    this.YearSetup = graph.GetService<IFABookPeriodRepository>().FindFABookYearSetup(this.Book);
    return this;
  }

  protected virtual DeprCalcParameters Calculate()
  {
    if (this.YearSetup.FPType == FiscalPeriodSetupCreator.FPType.Quarter)
    {
      switch (this.AveragingConvention)
      {
        case "FQ":
          this.AveragingConvention = "FP";
          break;
        case "HQ":
          this.AveragingConvention = "HP";
          break;
      }
    }
    this.DepreciationStartBookPeriod = this.GetPeriodFromDate(this.DepreciationStartDate, false);
    this.DepreciationStartYear = this.DepreciationStartBookPeriod != null ? int.Parse(this.DepreciationStartBookPeriod.FinYear) : throw new PXException("Asset will be depreciated from {0}, book '{2}' does not have financial periods generated from {1}. You need to go to 'Generate FA Calendars' screen and generate financial periods before changing this asset.", new object[3]
    {
      (object) this.DepreciationStartDate.ToShortDateString(),
      (object) this.DepreciationStartDate.Year,
      (object) this.Book.BookCode
    });
    this.DepreciationStartPeriod = int.Parse(this.DepreciationStartBookPeriod.PeriodNbr);
    this.DepreciationPeriodsInYear = this.GetPeriodsInYear(this.DepreciationStartYear);
    this.RecoveryStartDate = this.DepreciationStartDate;
    this.RecoveryStartBookPeriod = this.DepreciationStartBookPeriod;
    bool flag = false;
    string averagingConvention = this.AveragingConvention;
    DateTime? nullable1;
    if (averagingConvention != null && averagingConvention.Length == 2)
    {
      switch (averagingConvention[1])
      {
        case '2':
          if (averagingConvention == "M2")
            break;
          goto label_47;
        case 'D':
          if (averagingConvention == "FD")
            goto label_47;
          goto label_47;
        case 'P':
          switch (averagingConvention)
          {
            case "MP":
              break;
            case "FP":
              this.RecoveryStartDate = this.DepreciationStartBookPeriod.StartDate.Value;
              goto label_47;
            case "NP":
              string nextPeriodId = this.GetNextPeriodID(this.DepreciationStartBookPeriod.FinPeriodID);
              this.RecoveryStartBookPeriod = this.GetBookPeriod(nextPeriodId);
              if (this.RecoveryStartBookPeriod == null)
                throw new PXException("Asset will be depreciated from {0}, book '{2}' does not have financial periods generated from {1}. You need to go to 'Generate FA Calendars' screen and generate financial periods before changing this asset.", new object[3]
                {
                  (object) PeriodIDAttribute.FormatForError(nextPeriodId),
                  (object) FinPeriodUtils.FiscalYear(nextPeriodId),
                  (object) this.Book.BookCode
                });
              this.DepreciationStartPeriod = int.Parse(FinPeriodUtils.PeriodInYear(nextPeriodId));
              this.DepreciationStartYear = int.Parse(FinPeriodUtils.FiscalYear(nextPeriodId));
              nullable1 = this.RecoveryStartBookPeriod.StartDate;
              this.RecoveryStartDate = nullable1.Value;
              goto label_47;
            case "HP":
              this.RecoveryStartDate = this.DepreciationStartBookPeriod.StartDate.Value;
              flag = true;
              goto label_47;
            default:
              goto label_47;
          }
          break;
        case 'Q':
          switch (averagingConvention)
          {
            case "FQ":
              if (this.DepreciationPeriodsInYear == 12)
              {
                int period = ((int) Decimal.Ceiling((Decimal) this.DepreciationStartPeriod / 3M) - 1) * 3 + 1;
                this.RecoveryStartBookPeriod = this.GetBookPeriod(period, this.DepreciationStartYear);
                if (this.RecoveryStartBookPeriod == null)
                  throw new PXException("Asset will be depreciated from {0}, book '{2}' does not have financial periods generated from {1}. You need to go to 'Generate FA Calendars' screen and generate financial periods before changing this asset.", new object[3]
                  {
                    (object) $"{period:00}-{this.DepreciationStartYear}",
                    (object) this.DepreciationStartYear,
                    (object) this.Book.BookCode
                  });
                this.RecoveryStartDate = this.RecoveryStartBookPeriod.StartDate.Value;
                goto label_47;
              }
              goto label_47;
            case "HQ":
              if (this.DepreciationPeriodsInYear == 12)
              {
                if (this.WholeRecoveryPeriods % 3M != 0M)
                  throw new PXException("Can not use averaging convention {0} with recovery period {1}.", new object[2]
                  {
                    (object) this.AveragingConvention,
                    (object) this.WholeRecoveryPeriods
                  });
                int period = ((int) Decimal.Ceiling((Decimal) this.DepreciationStartPeriod / 3M) - 1) * 3 + 1;
                this.RecoveryStartBookPeriod = this.GetBookPeriod(period, this.DepreciationStartYear);
                if (this.RecoveryStartBookPeriod == null)
                  throw new PXException("Asset will be depreciated from {0}, book '{2}' does not have financial periods generated from {1}. You need to go to 'Generate FA Calendars' screen and generate financial periods before changing this asset.", new object[3]
                  {
                    (object) $"{period:00}-{this.DepreciationStartYear}",
                    (object) this.DepreciationStartYear,
                    (object) this.Book.BookCode
                  });
                DateTime depreciationStartDate = this.DepreciationStartDate;
                nullable1 = this.RecoveryStartBookPeriod.StartDate;
                flag = nullable1.HasValue && depreciationStartDate == nullable1.GetValueOrDefault();
                this.RecoveryStartBookPeriod = this.DepreciationStartBookPeriod;
                goto label_47;
              }
              goto label_47;
            default:
              goto label_47;
          }
        case 'Y':
          switch (averagingConvention)
          {
            case "FY":
              int period1 = 1;
              this.RecoveryStartBookPeriod = this.GetBookPeriod(period1, this.DepreciationStartYear);
              if (this.RecoveryStartBookPeriod == null)
                throw new PXException("Asset will be depreciated from {0}, book '{2}' does not have financial periods generated from {1}. You need to go to 'Generate FA Calendars' screen and generate financial periods before changing this asset.", new object[3]
                {
                  (object) $"{period1:00}-{this.DepreciationStartYear}",
                  (object) this.DepreciationStartYear,
                  (object) this.Book.BookCode
                });
              this.RecoveryStartDate = this.RecoveryStartBookPeriod.StartDate.Value;
              goto label_47;
            case "HY":
              int period2 = 1;
              this.RecoveryStartBookPeriod = this.GetBookPeriod(period2, this.DepreciationStartYear);
              if (this.RecoveryStartBookPeriod == null)
                throw new PXException("Asset will be depreciated from {0}, book '{2}' does not have financial periods generated from {1}. You need to go to 'Generate FA Calendars' screen and generate financial periods before changing this asset.", new object[3]
                {
                  (object) $"{period2:00}-{this.DepreciationStartYear}",
                  (object) this.DepreciationStartYear,
                  (object) this.Book.BookCode
                });
              DateTime depreciationStartDate1 = this.DepreciationStartDate;
              nullable1 = this.RecoveryStartBookPeriod.StartDate;
              flag = nullable1.HasValue && depreciationStartDate1 == nullable1.GetValueOrDefault();
              this.RecoveryStartBookPeriod = this.DepreciationStartBookPeriod;
              goto label_47;
            default:
              goto label_47;
          }
        default:
          goto label_47;
      }
      this.RecoveryStartDate = this.DepreciationStartBookPeriod.StartDate.Value;
      switch (this.MidMonthType)
      {
        case "H":
          Decimal num1 = (Decimal) ((this.DepreciationStartDate - this.DepreciationStartBookPeriod.StartDate.Value).Days + 1);
          nullable1 = this.DepreciationStartBookPeriod.EndDate;
          DateTime dateTime1 = nullable1.Value;
          nullable1 = this.DepreciationStartBookPeriod.StartDate;
          DateTime dateTime2 = nullable1.Value;
          Decimal num2 = (Decimal) (dateTime1 - dateTime2).Days / 2M;
          if (num1 > num2)
          {
            this.StartDepreciationMidPeriodRatio = this.AveragingConvention == "M2" ? 0M : 0.5M;
            flag = true;
            break;
          }
          this.StartDepreciationMidPeriodRatio = 1M;
          break;
        case "F":
          if ((this.DepreciationStartDate - this.DepreciationStartBookPeriod.StartDate.Value).Days + 1 > (int) this.MidMonthDay)
          {
            this.StartDepreciationMidPeriodRatio = this.AveragingConvention == "M2" ? 0M : 0.5M;
            flag = true;
            break;
          }
          this.StartDepreciationMidPeriodRatio = 1M;
          break;
        case "N":
          int period3 = this.DepreciationStartPeriod - 1;
          int depreciationStartYear = this.DepreciationStartYear;
          if (period3 == 0)
          {
            --depreciationStartYear;
            period3 = this.GetPeriodsInYear(depreciationStartYear);
          }
          if ((this.DepreciationStartDate - this.GetBookPeriod(period3, depreciationStartYear).EndDate.Value).Days + 1 > (int) this.MidMonthDay)
          {
            this.StartDepreciationMidPeriodRatio = this.AveragingConvention == "M2" ? 0M : 0.5M;
            flag = true;
            break;
          }
          this.StartDepreciationMidPeriodRatio = 1M;
          break;
      }
      this.StopDepreciationMidPeriodRatio = this.AveragingConvention == "MP" ? this.StartDepreciationMidPeriodRatio : 1M;
    }
label_47:
    nullable1 = this.recoveryEndDate;
    this.RecoveryEndDate = nullable1 ?? DeprCalcParameters.GetDatePlusYears(this.RecoveryStartDate, this.UsefulLife);
    this.RecoveryEndBookPeriod = this.GetPeriodFromDate(this.RecoveryEndDate, false);
    if (this.RecoveryEndBookPeriod == null)
    {
      object[] objArray = new object[5]
      {
        (object) this.DepreciationStartDate.ToShortDateString(),
        null,
        null,
        null,
        null
      };
      DateTime dateTime = this.RecoveryEndDate;
      objArray[1] = (object) dateTime.ToShortDateString();
      dateTime = this.DepreciationStartDate;
      objArray[2] = (object) dateTime.Year;
      dateTime = this.RecoveryEndDate;
      objArray[3] = (object) dateTime.Year;
      objArray[4] = (object) this.Book.BookCode;
      throw new PXException("Asset will be depreciated from {0} to {1}, book '{4}' does not have financial periods generated from {2} to {3}. You need to go to 'Generate FA Calendars' screen and generate financial periods before changing this asset.", objArray);
    }
    if (flag && !this.recoveryEndDate.HasValue)
    {
      string nextPeriodId = this.GetNextPeriodID(this.RecoveryEndBookPeriod.FinPeriodID);
      this.RecoveryEndBookPeriod = this.GetBookPeriod(nextPeriodId);
      nullable1 = this.RecoveryEndBookPeriod != null ? this.RecoveryEndBookPeriod.EndDate : throw new PXException("Asset will be depreciated from {0}, book '{2}' does not have financial periods generated from {1}. You need to go to 'Generate FA Calendars' screen and generate financial periods before changing this asset.", new object[3]
      {
        (object) PeriodIDAttribute.FormatForError(nextPeriodId),
        (object) FinPeriodUtils.FiscalYear(nextPeriodId),
        (object) this.Book.BookCode
      });
      this.RecoveryEndDate = nullable1.Value.AddDays(-1.0);
    }
    int? nullable2 = this.PeriodMinusPeriod(this.RecoveryEndBookPeriod.FinPeriodID, this.RecoveryStartBookPeriod.FinPeriodID);
    this.WholeRecoveryPeriods = (nullable2.HasValue ? (Decimal) nullable2.GetValueOrDefault() : 0M) + 1M;
    if (!this.Depreciate)
      return this;
    int currYear1 = int.Parse(this.RecoveryEndBookPeriod.FinYear);
    this.RecoveryEndPeriod = int.Parse(this.RecoveryEndBookPeriod.PeriodNbr);
    this.RecoveryYears = currYear1 - this.DepreciationStartYear + 1;
    this.WholeRecoveryDays = (this.RecoveryEndDate - this.RecoveryStartDate).Days + 1;
    nullable1 = this.DepreciationStopDate;
    if (nullable1.HasValue)
    {
      nullable1 = this.DepreciationStopDate;
      DateTime recoveryEndDate = this.RecoveryEndDate;
      if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() > recoveryEndDate ? 1 : 0) : 0) != 0)
        throw new PXException("DepreciationToDate cannot be greater than RecoveryEndDate. Please contact support.");
      nullable1 = this.DepreciationStopDate;
      this.DepreciationStopBookPeriod = this.GetPeriodFromDate(nullable1.Value, false);
      if (this.DepreciationStopBookPeriod == null)
      {
        object[] objArray = new object[5]
        {
          (object) this.DepreciationStartDate.ToShortDateString(),
          null,
          null,
          null,
          null
        };
        nullable1 = this.DepreciationStopDate;
        DateTime depreciationStartDate = nullable1.Value;
        objArray[1] = (object) depreciationStartDate.ToShortDateString();
        depreciationStartDate = this.DepreciationStartDate;
        objArray[2] = (object) depreciationStartDate.Year;
        nullable1 = this.DepreciationStopDate;
        depreciationStartDate = nullable1.Value;
        objArray[3] = (object) depreciationStartDate.Year;
        objArray[4] = (object) this.Book.BookCode;
        throw new PXException("Asset will be depreciated from {0} to {1}, book '{4}' does not have financial periods generated from {2} to {3}. You need to go to 'Generate FA Calendars' screen and generate financial periods before changing this asset.", objArray);
      }
      this.DepreciationYears = int.Parse(this.DepreciationStopBookPeriod.FinYear) - this.DepreciationStartYear + 1;
      this.DepreciationStopPeriod = int.Parse(this.DepreciationStopBookPeriod.PeriodNbr);
    }
    else
    {
      this.DepreciationStopDate = new DateTime?(this.RecoveryEndDate);
      this.DepreciationStopPeriod = this.RecoveryEndPeriod;
      this.DepreciationYears = this.RecoveryYears;
      this.DepreciationStopBookPeriod = this.RecoveryEndBookPeriod;
    }
    switch (this.AveragingConvention)
    {
      case "FD":
        int currYear2 = int.Parse(this.RecoveryStartBookPeriod.FinYear);
        int currPeriod = int.Parse(this.RecoveryStartBookPeriod.PeriodNbr);
        if (currYear2 == currYear1 && currPeriod == this.RecoveryEndPeriod)
        {
          DateTime recoveryEndDate = this.RecoveryEndDate;
          int periodLength = this.GetPeriodLength(currYear2, currPeriod);
          this.WholeRecoveryPeriods = (Decimal) this.GetDaysOnPeriod(this.RecoveryStartDate, new DateTime?(this.RecoveryEndDate), currYear2, currPeriod, ref recoveryEndDate) / (Decimal) periodLength;
          break;
        }
        DateTime minValue = DateTime.MinValue;
        int periodLength1 = this.GetPeriodLength(currYear2, currPeriod);
        this.WholeRecoveryPeriods += (Decimal) this.GetDaysOnPeriod(this.RecoveryStartDate, new DateTime?(this.RecoveryEndDate), currYear2, currPeriod, ref minValue) / (Decimal) periodLength1 - 1M;
        int periodLength2 = this.GetPeriodLength(currYear1, this.RecoveryEndPeriod);
        this.WholeRecoveryPeriods += (Decimal) this.GetDaysOnPeriod(this.RecoveryStartDate, new DateTime?(this.RecoveryEndDate), currYear1, this.RecoveryEndPeriod, ref minValue) / (Decimal) periodLength2 - 1M;
        break;
      case "FQ":
      case "HQ":
        if (this.DepreciationPeriodsInYear == 12)
        {
          this.FirstRecoveryEndQuarterPeriod = ((int) Decimal.Ceiling((Decimal) this.RecoveryEndPeriod / 3M) - 1) * 3 + 1;
          this.FirstDepreciationStopQuarterPeriod = ((int) Decimal.Ceiling((Decimal) this.DepreciationStopPeriod / 3M) - 1) * 3 + 1;
          this.LastDepreciationStartQuarterPeriod = (int) Decimal.Ceiling((Decimal) this.DepreciationStartPeriod / 3M) * 3;
          break;
        }
        break;
    }
    return this;
  }

  public FABookPeriod GetPeriodFromDate(DateTime d, bool check = true)
  {
    FABookPeriod faBookPeriod = this.Periods.Values.FirstOrDefault<FABookPeriod>((Func<FABookPeriod, bool>) (p =>
    {
      DateTime? startDate = p.StartDate;
      DateTime dateTime1 = d;
      if ((startDate.HasValue ? (startDate.GetValueOrDefault() <= dateTime1 ? 1 : 0) : 0) == 0)
        return false;
      DateTime? endDate = p.EndDate;
      DateTime dateTime2 = d;
      return endDate.HasValue && endDate.GetValueOrDefault() > dateTime2;
    }));
    return !check || faBookPeriod != null ? faBookPeriod : throw new PXFABookPeriodException();
  }

  [Obsolete("Will be removed in Acumatica 2018R1.")]
  public FABookYear GetBookYear(int Year) => this.Years[Year];

  public int GetPeriodsInYearByNumber(int yearNumber)
  {
    return this.GetPeriodsInYear(this.DepreciationStartYear + yearNumber - 1);
  }

  [Obsolete("Will be removed in Acumatica 2018R1. Use GetPeriodsInYear with one parameter instead.")]
  public int GetPeriodsInYear(FABook book, int Year) => this.GetPeriodsInYear(Year);

  public int GetPeriodsInYear(int year)
  {
    FABookYear faBookYear;
    if (!this.Years.TryGetValue(year, out faBookYear) || !faBookYear.FinPeriods.HasValue)
      throw new PXException("Financial Periods are not defined for the book '{0}' in {1}.", new object[2]
      {
        (object) this.Book.BookCode,
        (object) year
      });
    return this.Periods.Values.Count<FABookPeriod>((Func<FABookPeriod, bool>) (period => int.Parse(period.FinYear) == year));
  }

  public FABookPeriod GetBookPeriod(int period, int year)
  {
    return this.GetBookPeriod($"{year:0000}{period:00}");
  }

  public FABookPeriod GetBookPeriod(string periodID) => this.Periods[periodID];

  [Obsolete("Will be removed in Acumatica 2018R1. Use GetNextPeriodID with one parameter instead.")]
  public string GetNextPeriodID(
    IYearSetup yearSetup,
    FABook book,
    string periodID,
    int periodsInYear)
  {
    return this.GetNextPeriodID(periodID);
  }

  public string GetNextPeriodID(string periodID)
  {
    int num = int.Parse(FinPeriodUtils.PeriodInYear(periodID)) + 1;
    int year = int.Parse(FinPeriodUtils.FiscalYear(periodID));
    if (num > this.GetPeriodsInYear(year))
    {
      ++year;
      num = 1;
    }
    return $"{year:0000}{num:00}";
  }

  public static DateTime GetDatePlusYears(DateTime DeprFromDate, Decimal usefulLife)
  {
    DateTime datePlusYears = DeprFromDate;
    if (usefulLife > 0M)
    {
      Decimal num1 = Decimal.Truncate(usefulLife);
      DateTime dateTime = DeprFromDate.AddYears((int) num1);
      datePlusYears = dateTime.AddDays(-1.0);
      Decimal num2 = usefulLife - num1;
      if (num2 != 0M)
      {
        Decimal num3 = Decimal.Ceiling(usefulLife);
        dateTime = DeprFromDate.AddYears((int) num3);
        int num4 = (int) ((Decimal) (dateTime.AddDays(-1.0) - datePlusYears).Days * num2);
        datePlusYears = datePlusYears.AddDays((double) (num4 - 1));
      }
    }
    return datePlusYears;
  }

  public static int GetFinancialYears(
    int wholeRecoveryPeriods,
    int startPeriod,
    int depreciationPeriodsInYear,
    bool startPeriodIsWhole)
  {
    return wholeRecoveryPeriods == 0 || startPeriod == 0 ? 0 : (int) Decimal.Ceiling((Decimal) (wholeRecoveryPeriods + startPeriod - 1 + (!startPeriodIsWhole ? 1 : 0)) / (Decimal) depreciationPeriodsInYear);
  }

  public int? PeriodMinusPeriod(string FiscalPeriodID1, string FiscalPeriodID2)
  {
    if (!this.Periods.TryGetValue(FiscalPeriodID1, out FABookPeriod _) || !this.Periods.TryGetValue(FiscalPeriodID2, out FABookPeriod _))
      throw new PXException("Book Calendar cannot be found in the system.");
    return new int?(this.Periods.Values.Count<FABookPeriod>((Func<FABookPeriod, bool>) (period => string.CompareOrdinal(period.FinPeriodID, FiscalPeriodID1) <= 0 && string.CompareOrdinal(period.FinPeriodID, FiscalPeriodID2) > 0)));
  }

  public int GetPeriodLength(int currYear, int currPeriod)
  {
    FABookPeriod bookPeriod = this.GetBookPeriod(currPeriod, currYear);
    if ((bookPeriod != null ? (!bookPeriod.StartDate.HasValue ? 1 : 0) : 1) != 0 || !bookPeriod.EndDate.HasValue)
      throw new PXException("Financial Periods are not defined for the book '{0}' in {1}.", new object[2]
      {
        (object) this.Book.BookCode,
        (object) currYear
      });
    return (bookPeriod.EndDate.Value - bookPeriod.StartDate.Value).Days;
  }

  public int GetFinPeriodLengthInDays(int yearNumber, int periodNumberInYear)
  {
    return this.GetFinPeriodLengthInDays(yearNumber, periodNumberInYear, this.AveragingConvention);
  }

  public int GetFinPeriodLengthInDaysFebAlways28(int yearNumber, int currPeriod)
  {
    int year = this.DepreciationStartYear + yearNumber - 1;
    FABookPeriod bookPeriod = this.GetBookPeriod(currPeriod, year);
    if ((bookPeriod != null ? (!bookPeriod.StartDate.HasValue ? 1 : 0) : 1) == 0)
    {
      DateTime? nullable = bookPeriod.EndDate;
      if (nullable.HasValue)
      {
        if (this.YearSetup.PeriodType == "MO")
        {
          nullable = bookPeriod.StartDate;
          if (nullable.Value.DayOfYear == 32 /*0x20*/)
            return 28;
        }
        nullable = bookPeriod.EndDate;
        DateTime dateTime1 = nullable.Value;
        nullable = bookPeriod.StartDate;
        DateTime dateTime2 = nullable.Value;
        return (dateTime1 - dateTime2).Days;
      }
    }
    throw new PXException("Financial Periods are not defined for the book '{0}' in {1}.", new object[2]
    {
      (object) this.Book.BookCode,
      (object) year
    });
  }

  public int GetDaysHeldInYear(int year, int fromPeriod, int toPeriod)
  {
    int daysHeldInYear = 0;
    for (int currPeriod = fromPeriod; currPeriod <= toPeriod; ++currPeriod)
    {
      int inDaysFebAlways28 = this.GetFinPeriodLengthInDaysFebAlways28(year, currPeriod);
      daysHeldInYear += inDaysFebAlways28;
    }
    return daysHeldInYear;
  }

  public int GetFinPeriodLengthInDays(
    int yearNumber,
    int periodNumberInYear,
    string averagingConvention)
  {
    int num1 = yearNumber == 1 ? this.DepreciationStartPeriod : 1;
    int num2 = yearNumber == this.DepreciationYears ? this.DepreciationStopPeriod : this.GetPeriodsInYearByNumber(yearNumber);
    int currYear1 = this.DepreciationStartYear + yearNumber - 1;
    if (averagingConvention == "FD" && yearNumber == 1 && periodNumberInYear == num1)
    {
      DateTime minValue = DateTime.MinValue;
      return this.GetDaysOnPeriod(this.RecoveryStartDate, new DateTime?(this.GetPeriodFromDate(this.RecoveryStartDate).EndDate.Value), currYear1, periodNumberInYear, ref minValue);
    }
    if (!(averagingConvention == "FD") || yearNumber != this.DepreciationYears || periodNumberInYear != num2)
      return this.GetPeriodLength(currYear1, periodNumberInYear);
    DateTime minValue1 = DateTime.MinValue;
    DateTime? nullable = this.DepreciationStopDate;
    nullable = this.GetPeriodFromDate(nullable ?? this.RecoveryEndDate).StartDate;
    DateTime recoveryStartDate = nullable.Value;
    nullable = this.DepreciationStopDate;
    DateTime? recoveryEndDate = new DateTime?(nullable ?? this.RecoveryEndDate);
    int currYear2 = currYear1;
    int currPeriod = periodNumberInYear;
    ref DateTime local = ref minValue1;
    return this.GetDaysOnPeriod(recoveryStartDate, recoveryEndDate, currYear2, currPeriod, ref local) + 1;
  }

  public int GetFinYearLengthInDays(int yearNumber)
  {
    FABookYear year = this.Years[this.DepreciationStartYear + yearNumber - 1];
    DateTime? nullable = year.EndDate;
    DateTime dateTime1 = nullable.Value;
    nullable = year.StartDate;
    DateTime dateTime2 = nullable.Value;
    return (dateTime1 - dateTime2).Days;
  }

  public int GetDaysOnPeriod(
    DateTime recoveryStartDate,
    DateTime? recoveryEndDate,
    int currYear,
    int currPeriod,
    ref DateTime previousEndDate)
  {
    FABookPeriod bookPeriod = this.GetBookPeriod(currPeriod, currYear);
    DateTime? nullable1 = bookPeriod != null ? bookPeriod.StartDate : throw new PXException("Financial Periods are not defined for the book '{0}' in {1}.", new object[2]
    {
      (object) this.Book.BookCode,
      (object) currYear
    });
    DateTime? endDate = bookPeriod.EndDate;
    int daysOnPeriod = 0;
    DateTime dateTime1 = recoveryStartDate;
    DateTime? nullable2 = endDate;
    if ((nullable2.HasValue ? (dateTime1 <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      nullable2 = nullable1;
      DateTime? nullable3 = recoveryEndDate;
      if ((nullable2.HasValue & nullable3.HasValue ? (nullable2.GetValueOrDefault() <= nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable3 = nullable1;
        DateTime dateTime2 = recoveryStartDate;
        DateTime? nullable4 = (nullable3.HasValue ? (nullable3.GetValueOrDefault() > dateTime2 ? 1 : 0) : 0) != 0 ? nullable1 : new DateTime?(recoveryStartDate);
        nullable3 = endDate;
        nullable2 = recoveryEndDate;
        DateTime? nullable5 = (nullable3.HasValue & nullable2.HasValue ? (nullable3.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? endDate : recoveryEndDate;
        int days = (nullable5.Value - nullable4.Value).Days;
        nullable2 = nullable4;
        DateTime dateTime3 = previousEndDate;
        int num;
        if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() == dateTime3 ? 1 : 0) : 0) == 0)
        {
          num = !(previousEndDate == DateTime.MinValue) ? 1 : 0;
        }
        else
        {
          nullable2 = nullable5;
          nullable3 = recoveryEndDate;
          num = nullable2.HasValue == nullable3.HasValue && (!nullable2.HasValue || nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault()) ? 1 : 0;
        }
        daysOnPeriod = days + num;
        previousEndDate = nullable5.Value;
      }
    }
    return daysOnPeriod;
  }

  public static DateTime GetRecoveryEndDate(PXGraph graph, FABookBalance assetBalance)
  {
    return new DeprCalcParameters().Fill(graph, assetBalance).RecoveryEndDate;
  }

  public static string GetRecoveryStartPeriod(PXGraph graph, FABookBalance assetBalance)
  {
    DeprCalcParameters deprCalcParameters = new DeprCalcParameters().Fill(graph, assetBalance);
    string recoveryStartPeriod = $"{deprCalcParameters.DepreciationStartYear:0000}{deprCalcParameters.DepreciationStartPeriod:00}";
    if (deprCalcParameters.DepreciationStartYear == 0 && deprCalcParameters.DepreciationStartPeriod == 0)
      recoveryStartPeriod = (string) null;
    if (deprCalcParameters.StartDepreciationMidPeriodRatio == 0M && (assetBalance.AveragingConvention == "MP" || assetBalance.AveragingConvention == "M2"))
      recoveryStartPeriod = graph.GetService<IFABookPeriodUtils>().PeriodPlusPeriodsCount(recoveryStartPeriod, 1, assetBalance.BookID, assetBalance.AssetID);
    if (recoveryStartPeriod != null)
    {
      bool? changeDeprFromPeriod = assetBalance.AllowChangeDeprFromPeriod;
      bool flag = false;
      if (changeDeprFromPeriod.GetValueOrDefault() == flag & changeDeprFromPeriod.HasValue && string.CompareOrdinal(assetBalance.DeprFromPeriod, recoveryStartPeriod) != 0)
        throw new PXSetPropertyException("The Depr. From Period value cannot be changed because at least one depreciation transaction exists.");
    }
    return recoveryStartPeriod;
  }
}
