// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.YearSetupGraphBase`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.GL;

public abstract class YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect> : 
  PXGraph<
  #nullable disable
  TGraph>,
  IYearSetupMaintenanceGraph
  where TGraph : PXGraph
  where TYearSetup : class, IYearSetup, IBqlTable, new()
  where TPeriodSetup : class, IPeriodSetup, IBqlTable, new()
  where TPeriodSetupSelect : PXSelectBase<TPeriodSetup>
{
  public PXSave<TYearSetup> Save;
  public PXCancel<TYearSetup> Cancel;
  public PXDelete<TYearSetup> Delete;
  public PXFirst<TYearSetup> First;
  public PXPrevious<TYearSetup> Previous;
  public PXNext<TYearSetup> Next;
  public PXLast<TYearSetup> Last;
  public PXAction<TYearSetup> AutoFill;
  public PXSelect<TYearSetup> FiscalYearSetup;
  public TPeriodSetupSelect Periods;
  private bool massUpdateFlag;
  public PXAction<TYearSetup> decrFirstYear;
  private const FiscalPeriodSetupCreator.FPType defaultPeriodType = FiscalPeriodSetupCreator.FPType.Month;
  private bool isLastInsertingPeriod;
  private bool isMassDelete;
  private bool isAutoInsert;
  private bool skipPeriodReset;
  private bool? _hasPartiallyDefinedYear;
  private FiscalPeriodSetupCreator<TPeriodSetup> _periodCreator;

  public YearSetupGraphBase()
  {
    ((PXSelectBase) this.FiscalYearSetup).Cache.AllowDelete = true;
    if (this.IsFiscalYearSetupExists())
      ((PXSelectBase) this.FiscalYearSetup).Cache.AllowInsert = false;
    PXGraph.FieldVerifyingEvents fieldVerifying1 = ((PXGraph) this).FieldVerifying;
    Type type1 = typeof (TPeriodSetup);
    string name1 = typeof (YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.PeriodSetup.endDate).Name;
    YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect> yearSetupGraphBase1 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying1 = new PXFieldVerifying((object) yearSetupGraphBase1, __vmethodptr(yearSetupGraphBase1, TPeriodSetupOnEndDateFieldVerifying));
    fieldVerifying1.AddHandler(type1, name1, pxFieldVerifying1);
    PXGraph.RowDeletedEvents rowDeleted = ((PXGraph) this).RowDeleted;
    Type type2 = typeof (TPeriodSetup);
    YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect> yearSetupGraphBase2 = this;
    // ISSUE: virtual method pointer
    PXRowDeleted pxRowDeleted = new PXRowDeleted((object) yearSetupGraphBase2, __vmethodptr(yearSetupGraphBase2, TPeriodSetupOnRowDeleted));
    rowDeleted.AddHandler(type2, pxRowDeleted);
    PXGraph.RowDeletingEvents rowDeleting1 = ((PXGraph) this).RowDeleting;
    Type type3 = typeof (TPeriodSetup);
    YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect> yearSetupGraphBase3 = this;
    // ISSUE: virtual method pointer
    PXRowDeleting pxRowDeleting1 = new PXRowDeleting((object) yearSetupGraphBase3, __vmethodptr(yearSetupGraphBase3, TPeriodSetupOnRowDeleting));
    rowDeleting1.AddHandler(type3, pxRowDeleting1);
    PXGraph.RowInsertedEvents rowInserted = ((PXGraph) this).RowInserted;
    Type type4 = typeof (TPeriodSetup);
    YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect> yearSetupGraphBase4 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted = new PXRowInserted((object) yearSetupGraphBase4, __vmethodptr(yearSetupGraphBase4, TPeriodSetupOnRowInserted));
    rowInserted.AddHandler(type4, pxRowInserted);
    PXGraph.RowInsertingEvents rowInserting1 = ((PXGraph) this).RowInserting;
    Type type5 = typeof (TPeriodSetup);
    YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect> yearSetupGraphBase5 = this;
    // ISSUE: virtual method pointer
    PXRowInserting pxRowInserting1 = new PXRowInserting((object) yearSetupGraphBase5, __vmethodptr(yearSetupGraphBase5, TPeriodSetupOnRowInserting));
    rowInserting1.AddHandler(type5, pxRowInserting1);
    PXGraph.RowUpdatedEvents rowUpdated = ((PXGraph) this).RowUpdated;
    Type type6 = typeof (TPeriodSetup);
    YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect> yearSetupGraphBase6 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) yearSetupGraphBase6, __vmethodptr(yearSetupGraphBase6, TPeriodSetupOnRowUpdated));
    rowUpdated.AddHandler(type6, pxRowUpdated);
    PXGraph.FieldUpdatedEvents fieldUpdated1 = ((PXGraph) this).FieldUpdated;
    Type type7 = typeof (TYearSetup);
    string name2 = typeof (YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.begFinYear).Name;
    YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect> yearSetupGraphBase7 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) yearSetupGraphBase7, __vmethodptr(yearSetupGraphBase7, TYearSetupOnBegFinYearFieldUpdated));
    fieldUpdated1.AddHandler(type7, name2, pxFieldUpdated1);
    PXGraph.FieldUpdatedEvents fieldUpdated2 = ((PXGraph) this).FieldUpdated;
    Type type8 = typeof (TYearSetup);
    string name3 = typeof (YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.belongsToNextYear).Name;
    YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect> yearSetupGraphBase8 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) yearSetupGraphBase8, __vmethodptr(yearSetupGraphBase8, TYearSetupOnBelongsToNextYearFieldUpdated));
    fieldUpdated2.AddHandler(type8, name3, pxFieldUpdated2);
    PXGraph.FieldUpdatedEvents fieldUpdated3 = ((PXGraph) this).FieldUpdated;
    Type type9 = typeof (TYearSetup);
    string name4 = typeof (YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.periodType).Name;
    YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect> yearSetupGraphBase9 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated3 = new PXFieldUpdated((object) yearSetupGraphBase9, __vmethodptr(yearSetupGraphBase9, TYearSetupOnPeriodTypeFieldUpdated));
    fieldUpdated3.AddHandler(type9, name4, pxFieldUpdated3);
    PXGraph.FieldUpdatedEvents fieldUpdated4 = ((PXGraph) this).FieldUpdated;
    Type type10 = typeof (TYearSetup);
    string name5 = typeof (YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.hasAdjustmentPeriod).Name;
    YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect> yearSetupGraphBase10 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated4 = new PXFieldUpdated((object) yearSetupGraphBase10, __vmethodptr(yearSetupGraphBase10, TYearSetupOnHasAdjustmentPeriodFieldUpdated));
    fieldUpdated4.AddHandler(type10, name5, pxFieldUpdated4);
    PXGraph.FieldUpdatedEvents fieldUpdated5 = ((PXGraph) this).FieldUpdated;
    Type type11 = typeof (TYearSetup);
    string name6 = typeof (YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.endYearDayOfWeek).Name;
    YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect> yearSetupGraphBase11 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated5 = new PXFieldUpdated((object) yearSetupGraphBase11, __vmethodptr(yearSetupGraphBase11, TYearSetupOnEndYearDayOfWeekFieldUpdated));
    fieldUpdated5.AddHandler(type11, name6, pxFieldUpdated5);
    PXGraph.FieldUpdatedEvents fieldUpdated6 = ((PXGraph) this).FieldUpdated;
    Type type12 = typeof (TYearSetup);
    string name7 = typeof (YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.endYearCalcMethod).Name;
    YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect> yearSetupGraphBase12 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated6 = new PXFieldUpdated((object) yearSetupGraphBase12, __vmethodptr(yearSetupGraphBase12, TYearSetupOnEndYearCalcMethodFieldUpdated));
    fieldUpdated6.AddHandler(type12, name7, pxFieldUpdated6);
    PXGraph.FieldVerifyingEvents fieldVerifying2 = ((PXGraph) this).FieldVerifying;
    Type type13 = typeof (TYearSetup);
    string name8 = typeof (YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.periodsStartDate).Name;
    YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect> yearSetupGraphBase13 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying2 = new PXFieldVerifying((object) yearSetupGraphBase13, __vmethodptr(yearSetupGraphBase13, TYearSetupOnPeriodsStartDateFieldVerifying));
    fieldVerifying2.AddHandler(type13, name8, pxFieldVerifying2);
    PXGraph.FieldDefaultingEvents fieldDefaulting = ((PXGraph) this).FieldDefaulting;
    Type type14 = typeof (TYearSetup);
    string name9 = typeof (YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.periodsStartDate).Name;
    YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect> yearSetupGraphBase14 = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) yearSetupGraphBase14, __vmethodptr(yearSetupGraphBase14, TYearSetupOnPeriodsStartDateFieldDefaulting));
    fieldDefaulting.AddHandler(type14, name9, pxFieldDefaulting);
    PXGraph.RowDeletingEvents rowDeleting2 = ((PXGraph) this).RowDeleting;
    Type type15 = typeof (TYearSetup);
    YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect> yearSetupGraphBase15 = this;
    // ISSUE: virtual method pointer
    PXRowDeleting pxRowDeleting2 = new PXRowDeleting((object) yearSetupGraphBase15, __vmethodptr(yearSetupGraphBase15, TYearSetupOnRowDeleting));
    rowDeleting2.AddHandler(type15, pxRowDeleting2);
    PXGraph.RowInsertingEvents rowInserting2 = ((PXGraph) this).RowInserting;
    Type type16 = typeof (TYearSetup);
    YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect> yearSetupGraphBase16 = this;
    // ISSUE: virtual method pointer
    PXRowInserting pxRowInserting2 = new PXRowInserting((object) yearSetupGraphBase16, __vmethodptr(yearSetupGraphBase16, TYearSetupOnRowInserting));
    rowInserting2.AddHandler(type16, pxRowInserting2);
    PXGraph.RowPersistingEvents rowPersisting = ((PXGraph) this).RowPersisting;
    Type type17 = typeof (TYearSetup);
    YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect> yearSetupGraphBase17 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) yearSetupGraphBase17, __vmethodptr(yearSetupGraphBase17, TYearSetupOnRowPersisting));
    rowPersisting.AddHandler(type17, pxRowPersisting);
    PXGraph.RowSelectedEvents rowSelected = ((PXGraph) this).RowSelected;
    Type type18 = typeof (TYearSetup);
    YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect> yearSetupGraphBase18 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) yearSetupGraphBase18, __vmethodptr(yearSetupGraphBase18, TYearSetupOnRowSelected));
    rowSelected.AddHandler(type18, pxRowSelected);
  }

  [PXUIField(DisplayName = "Shift First Year", Visible = true)]
  [PXButton(Category = "Period Management", DisplayOnMainToolbar = true)]
  protected virtual IEnumerable DecrFirstYear(PXAdapter adapter)
  {
    return ((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Ask(((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current, "Important", adapter.View.Graph.GetType() == typeof (FiscalYearSetupMaint) ? "Warning - This operation will shift the company's first financial year one year earlier. Shifting the first year to an earlier date can affect statistics and data on reports. Do you want to continue?" : "Warning - This operation will shift the first year one year earlier. Do you want to continue?", (MessageButtons) 4, (MessageIcon) 2) == 6 ? (IEnumerable) this.DecrementFirstYear(adapter.Get<TYearSetup>()) : adapter.Get();
  }

  private IEnumerable<TYearSetup> DecrementFirstYear(IEnumerable<TYearSetup> yearTemplates)
  {
    foreach (TYearSetup yearTemplate in yearTemplates)
    {
      if (yearTemplate.BegFinYear.HasValue)
      {
        this.DoIncrementFirstYear(yearTemplate, true);
        yield return yearTemplate;
      }
    }
  }

  public virtual void SetCurrentYearSetup(object[] key = null)
  {
    ((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current = ((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current ?? ((PXSelectBase<TYearSetup>) this.FiscalYearSetup).SelectSingle(Array.Empty<object>());
  }

  public virtual void ShiftBackFirstYearTo(string yearNumber)
  {
    if ((object) ((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current == null)
      throw new ArgumentNullException("Current");
    string firstFinYear = ((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current.FirstFinYear;
    if (string.Compare(((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current.FirstFinYear, yearNumber) <= 0)
      return;
    while (string.CompareOrdinal(((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current.FirstFinYear, yearNumber) > 0)
      this.DecrementFirstYear((IEnumerable<TYearSetup>) new TYearSetup[1]
      {
        ((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current
      }).FirstOrDefault<TYearSetup>();
    ((PXAction) this.Save).Press();
  }

  [PXButton(Category = "Period Management", DisplayOnMainToolbar = true)]
  [PXUIField]
  public virtual IEnumerable autoFill(PXAdapter adapter)
  {
    if (FiscalPeriodSetupCreator.IsFixedLengthPeriod(((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current.FPType))
      throw new PXException("Period templates can't be generated for this Period Type");
    this.AutoFillPeriods();
    return adapter.Get();
  }

  protected virtual void TYearSetupOnRowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    TYearSetup row = (TYearSetup) e.Row;
    if ((object) row == null)
      return;
    if (!row.BelongsToNextYear.HasValue)
    {
      row.BelongsToNextYear = new bool?(false);
      if (row.BegFinYear.HasValue && !string.IsNullOrEmpty(row.FirstFinYear))
      {
        int num = int.Parse(row.FirstFinYear);
        row.BelongsToNextYear = new bool?(num == row.BegFinYear.Value.Year + 1);
      }
    }
    bool flag1 = this.AllowAlterStartDate();
    bool flag2 = this.AllowSetupModification(row);
    bool flag3 = FiscalPeriodSetupCreator.IsFixedLengthPeriod(row.FPType);
    bool flag4 = row.FPType == FiscalPeriodSetupCreator.FPType.FourFourFive || row.FPType == FiscalPeriodSetupCreator.FPType.FourFiveFour || row.FPType == FiscalPeriodSetupCreator.FPType.FiveFourFour;
    bool flag5 = row.FPType == FiscalPeriodSetupCreator.FPType.Custom;
    PXUIFieldAttribute.SetEnabled<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.belongsToNextYear>(cache, (object) row, flag1 & flag2);
    PXUIFieldAttribute.SetEnabled<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.begFinYear>(cache, (object) row, flag1 & flag2);
    PXUIFieldAttribute.SetEnabled<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.finPeriods>(cache, (object) row, !flag3 & flag2 && flag5 && !this.HasPartiallyDefinedYear);
    PXUIFieldAttribute.SetEnabled<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.userDefined>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.PeriodSetup.endDateUI>(((PXSelectBase) (object) this.Periods).Cache, (object) null, !flag3 & flag5);
    ((PXAction) this.decrFirstYear).SetEnabled(!flag1 & flag2);
    if (!flag5)
      ((PXSelectBase) (object) this.Periods).Cache.AllowUpdate = !flag3 & flag2;
    else
      ((PXSelectBase) (object) this.Periods).Cache.AllowUpdate = !flag3 & flag2 && !this.HasPartiallyDefinedYear;
    ((PXSelectBase) (object) this.Periods).Cache.AllowInsert = false;
    ((PXSelectBase) (object) this.Periods).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetVisible<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.finPeriods>(cache, (object) row, !flag3 | flag4);
    PXUIFieldAttribute.SetVisible<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.userDefined>(cache, (object) row, false);
    PXUIFieldAttribute.SetVisible<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.periodsStartDate>(cache, (object) row, true);
    PXUIFieldAttribute.SetEnabled<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.periodsStartDate>(cache, (object) row, flag3 & flag1 & flag2 && row.FPType != FiscalPeriodSetupCreator.FPType.Week);
    PXUIFieldAttribute.SetVisible<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.periodLength>(cache, (object) row, flag3 && !flag4);
    PXUIFieldAttribute.SetEnabled<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.periodLength>(cache, (object) row, ((!flag3 ? 0 : (row.FPType == FiscalPeriodSetupCreator.FPType.FixedLength ? 1 : 0)) & (flag1 ? 1 : 0) & (flag2 ? 1 : 0)) != 0);
    PXUIFieldAttribute.SetEnabled<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.periodType>(cache, (object) row, flag1 & flag2);
    PXUIFieldAttribute.SetEnabled<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.hasAdjustmentPeriod>(cache, (object) row, flag1 & flag2 && !flag5);
    PXUIFieldAttribute.SetVisible<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.hasAdjustmentPeriod>(cache, (object) row, !flag5);
    PXUIFieldAttribute.SetVisible<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.endYearCalcMethod>(cache, (object) row, row.FPType == FiscalPeriodSetupCreator.FPType.Week);
    PXUIFieldAttribute.SetEnabled<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.endYearCalcMethod>(cache, (object) row, row.FPType == FiscalPeriodSetupCreator.FPType.Week & flag2);
    PXUIFieldAttribute.SetVisible<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.endYearDayOfWeek>(cache, (object) row, row.FPType == FiscalPeriodSetupCreator.FPType.Week);
    PXUIFieldAttribute.SetEnabled<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.endYearDayOfWeek>(cache, (object) row, row.FPType == FiscalPeriodSetupCreator.FPType.Week & flag2);
    PXUIFieldAttribute.SetVisible<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.yearLastDayOfWeek>(cache, (object) row, row.FPType == FiscalPeriodSetupCreator.FPType.Week && row.EndYearCalcMethod != "CA");
    ((PXAction) this.AutoFill).SetEnabled(!flag3 & flag2 && !((IQueryable<PXResult<TPeriodSetup>>) this.Periods.Select(Array.Empty<object>())).Any<PXResult<TPeriodSetup>>());
  }

  protected virtual void TYearSetupOnRowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    string errMsg;
    if (!this.AllowDeleteYearSetup(out errMsg))
      throw new PXException(errMsg);
    this.isMassDelete = true;
  }

  protected virtual void TYearSetupOnRowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    TYearSetup row = (TYearSetup) e.Row;
    DateTime dateTime = ((PXGraph) this).Accessinfo.BusinessDate.Value;
    if (!row.BegFinYear.HasValue)
      row.BegFinYear = new DateTime?(new DateTime(dateTime.Year, 1, 1));
    this.RecalcFirstYear(row);
    short? finPeriods = row.FinPeriods;
    if (finPeriods.HasValue)
    {
      finPeriods = row.FinPeriods;
      if (finPeriods.Value > (short) 0)
        goto label_5;
    }
    row.FinPeriods = new short?(FiscalPeriodSetupCreator<TPeriodSetup>.GetFiscalPeriodsNbr(row.FPType, row.HasAdjustmentPeriod.Value));
label_5:
    row.PeriodLength = FiscalPeriodSetupCreator.GetPeriodLength(row.FPType);
    this.RecalcPeriodStartDate(row);
  }

  protected virtual void TYearSetupOnRowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if (2 != e.Operation && 1 != e.Operation)
      return;
    TYearSetup row = (TYearSetup) e.Row;
    int num1 = 0;
    if (!FiscalPeriodSetupCreator.IsFixedLengthPeriod(row.FPType))
    {
      TPeriodSetup periodSetup1 = default (TPeriodSetup);
      TPeriodSetup periodSetup2 = default (TPeriodSetup);
      foreach (PXResult<TPeriodSetup> pxResult in this.Periods.Select(Array.Empty<object>()))
      {
        TPeriodSetup periodSetup3 = PXResult<TPeriodSetup>.op_Implicit(pxResult);
        if ((object) periodSetup1 == null && periodSetup3.PeriodNbr == "01")
          periodSetup1 = periodSetup3;
        ++num1;
        short? finPeriods = row.FinPeriods;
        int? nullable = finPeriods.HasValue ? new int?((int) finPeriods.GetValueOrDefault()) : new int?();
        int num2 = num1;
        DateTime? endDateUi1;
        if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
        {
          if ((object) periodSetup2 != null)
          {
            endDateUi1 = periodSetup3.EndDateUI;
            DateTime dateTime1 = endDateUi1.Value;
            endDateUi1 = periodSetup2.EndDateUI;
            DateTime dateTime2 = endDateUi1.Value;
            if (dateTime1 < dateTime2)
            {
              PXCache cache1 = ((PXSelectBase) (object) this.Periods).Cache;
              // ISSUE: variable of a boxed type
              __Boxed<TPeriodSetup> local = (object) periodSetup3;
              // ISSUE: variable of a boxed type
              __Boxed<DateTime?> endDateUi2 = (ValueType) periodSetup3.EndDateUI;
              object[] objArray = new object[1];
              endDateUi1 = periodSetup2.EndDateUI;
              objArray[0] = (object) endDateUi1.Value.ToShortDateString();
              PXSetPropertyException propertyException = new PXSetPropertyException("The end date must not be earlier than the start date. To create an adjustment period, enter {0} in the End Date column.", (PXErrorLevel) 4, objArray);
              cache1.RaiseExceptionHandling<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.PeriodSetup.endDateUI>((object) local, (object) endDateUi2, (Exception) propertyException);
            }
          }
        }
        else
        {
          if ((object) periodSetup2 != null)
          {
            endDateUi1 = periodSetup3.EndDateUI;
            DateTime dateTime3 = endDateUi1.Value;
            endDateUi1 = periodSetup2.EndDateUI;
            DateTime dateTime4 = endDateUi1.Value;
            if (dateTime3 == dateTime4)
            {
              ((PXSelectBase) (object) this.Periods).Cache.RaiseExceptionHandling<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.PeriodSetup.endDateUI>((object) periodSetup3, (object) periodSetup3.EndDateUI, (Exception) new PXSetPropertyException("Adjustment period should be the last period of the financial year.", (PXErrorLevel) 4));
              goto label_16;
            }
          }
          if ((object) periodSetup2 != null)
          {
            endDateUi1 = periodSetup3.EndDateUI;
            DateTime dateTime5 = endDateUi1.Value;
            endDateUi1 = periodSetup2.EndDateUI;
            DateTime dateTime6 = endDateUi1.Value;
            if (dateTime5 < dateTime6)
              ((PXSelectBase) (object) this.Periods).Cache.RaiseExceptionHandling<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.PeriodSetup.endDateUI>((object) periodSetup3, (object) periodSetup3.EndDateUI, (Exception) new PXSetPropertyException("The end date must not be earlier than the start date.", (PXErrorLevel) 4));
          }
        }
label_16:
        periodSetup2 = periodSetup3;
      }
      short? finPeriods1 = row.FinPeriods;
      int? nullable1 = finPeriods1.HasValue ? new int?((int) finPeriods1.GetValueOrDefault()) : new int?();
      int num3 = num1;
      if (!(nullable1.GetValueOrDefault() == num3 & nullable1.HasValue) && cache.RaiseExceptionHandling<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.finPeriods>(e.Row, (object) row.FinPeriods, (Exception) new PXSetPropertyException("Please configure all the Financial Periods for the Year to commit your changes.", (PXErrorLevel) 5)))
        throw new PXRowPersistingException(typeof (YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.finPeriods).Name, (object) row.FinPeriods, "Please configure all the Financial Periods for the Year to commit your changes.");
    }
    else
    {
      string message;
      if (row.FPType == FiscalPeriodSetupCreator.FPType.Week || this.ValidatePeriodsStartDate(row, row.PeriodsStartDate.Value, out message))
        return;
      cache.RaiseExceptionHandling<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.periodsStartDate>((object) row, (object) row.PeriodsStartDate, (Exception) new PXSetPropertyException(message));
    }
  }

  protected virtual void TYearSetupOnBegFinYearFieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if ((object) ((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current == null)
      return;
    TYearSetup row = (TYearSetup) e.Row;
    this.RecalcFirstYear(row);
    if (!this.skipPeriodReset)
      this.ResetPeriods(row, false);
    if (row.FPType != FiscalPeriodSetupCreator.FPType.Week)
      return;
    row.PeriodsStartDate = this.AdjustPeriodStartOnDate(row);
  }

  protected virtual void TYearSetupOnBelongsToNextYearFieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    this.RecalcFirstYear((TYearSetup) e.Row);
  }

  protected virtual void TYearSetupOnPeriodTypeFieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if ((object) ((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current == null)
      return;
    TYearSetup row = (TYearSetup) e.Row;
    this.ResetPeriods(row, false);
    row.EndYearCalcMethod = "CA";
  }

  protected virtual void TYearSetupOnEndYearCalcMethodFieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if ((object) ((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current == null)
      return;
    TYearSetup row = (TYearSetup) e.Row;
    row.PeriodsStartDate = this.AdjustPeriodStartOnDate(row);
  }

  protected virtual void TYearSetupOnEndYearDayOfWeekFieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if ((object) ((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current == null)
      return;
    TYearSetup row = (TYearSetup) e.Row;
    row.PeriodsStartDate = this.AdjustPeriodStartOnDate(row);
  }

  protected virtual void TYearSetupOnPeriodsStartDateFieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    TYearSetup row = (TYearSetup) e.Row;
    DateTime newValue = (DateTime) e.NewValue;
    string message;
    if (row.FPType != FiscalPeriodSetupCreator.FPType.Week && !this.ValidatePeriodsStartDate(row, newValue, out message))
    {
      e.NewValue = (object) row.PeriodsStartDate;
      throw new PXSetPropertyException(message);
    }
  }

  protected virtual void TYearSetupOnPeriodsStartDateFieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    TYearSetup row = (TYearSetup) e.Row;
    if ((object) row == null || !row.BegFinYear.HasValue)
      return;
    if (row.FPType == FiscalPeriodSetupCreator.FPType.Week || row.FPType == FiscalPeriodSetupCreator.FPType.BiWeek || row.FPType == FiscalPeriodSetupCreator.FPType.FourWeek)
    {
      int num1 = (int) (row.BegFinYear.Value.DayOfWeek - 1);
      int num2;
      if (num1 < 0)
      {
        int num3 = num1 + 7;
        num2 = Math.Abs(num1) < Math.Abs(num3) ? num1 : num3;
      }
      else
      {
        int num4 = num1 - 7;
        num2 = Math.Abs(num1) < Math.Abs(num4) ? num1 : num4;
      }
      e.NewValue = (object) row.BegFinYear.Value.AddDays((double) num2);
    }
    else
      e.NewValue = (object) row.BegFinYear;
  }

  protected virtual void TYearSetupOnHasAdjustmentPeriodFieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if ((object) ((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current == null)
      return;
    this.ResetPeriods((TYearSetup) e.Row, true);
  }

  protected virtual void RecalcFirstYear(TYearSetup row)
  {
    int year = row.BegFinYear.Value.Year;
    if (row.BelongsToNextYear.GetValueOrDefault())
      ++year;
    string str = FiscalPeriodSetupCreator.FormatYear(year);
    if (!(str != row.FirstFinYear))
      return;
    row.FirstFinYear = str;
  }

  protected virtual void ResetPeriods(TYearSetup row, bool skipDatesRecalc)
  {
    this.DeletePeriods();
    this._periodCreator = (FiscalPeriodSetupCreator<TPeriodSetup>) null;
    if (!skipDatesRecalc)
      this.RecalcPeriodStartDate(row);
    row.PeriodLength = FiscalPeriodSetupCreator.GetPeriodLength(row.FPType);
    row.FinPeriods = this.PeriodCreator?.ActualNumberOfPeriods;
  }

  protected virtual void RecalcPeriodStartDate(TYearSetup row)
  {
    if (row.FPType == FiscalPeriodSetupCreator.FPType.Week || row.FPType == FiscalPeriodSetupCreator.FPType.BiWeek || row.FPType == FiscalPeriodSetupCreator.FPType.FourWeek)
    {
      DateTime? begFinYear = row.BegFinYear;
      int num1 = -(int) begFinYear.Value.DayOfWeek;
      int num2;
      if (num1 < 0)
      {
        int num3 = num1 + 7;
        num2 = Math.Abs(num1) < Math.Abs(num3) ? num1 : num3;
      }
      else
      {
        int num4 = num1 - 7;
        num2 = Math.Abs(num1) < Math.Abs(num4) ? num1 : num4;
      }
      // ISSUE: variable of a boxed type
      __Boxed<TYearSetup> local = (object) row;
      begFinYear = row.BegFinYear;
      DateTime? nullable = new DateTime?(begFinYear.Value.AddDays((double) num2));
      local.PeriodsStartDate = nullable;
    }
    else
      row.PeriodsStartDate = row.BegFinYear;
  }

  protected virtual bool ValidatePeriodsStartDate(
    TYearSetup row,
    DateTime aPSDValue,
    out string message)
  {
    message = "";
    if (FiscalPeriodSetupCreator.IsFixedLengthPeriod(row.FPType))
    {
      short? periodLength = row.PeriodLength;
      if (periodLength.HasValue)
      {
        int days = (aPSDValue - row.BegFinYear.Value).Days;
        periodLength = row.PeriodLength;
        int num = (int) Math.Floor((Decimal) periodLength.Value / 2.0M);
        if (Math.Abs(days) > num)
        {
          message = PXMessages.LocalizeFormatNoPrefix("Difference between Periods starting date and Year start date must not exceed {0} days", new object[1]
          {
            (object) num
          });
          return false;
        }
      }
    }
    return true;
  }

  protected virtual void TPeriodSetupOnRowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    if (this.isMassDelete)
      return;
    TPeriodSetup row = (TPeriodSetup) e.Row;
    TYearSetup current = ((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current;
    if (!current.UserDefined.Value)
      throw new PXException("Modification of Financial Periods settings is only allowed when the Custom Number of Periods option is selected.");
    ((PXSelectBase) (object) this.Periods).Cache.GetStatus((object) row);
    PXEntryStatus status = ((PXSelectBase) this.FiscalYearSetup).Cache.GetStatus((object) current);
    bool flag1 = 4 == status || 3 == status;
    bool flag2 = false;
    foreach (PXResult<TPeriodSetup> pxResult in this.Periods.Select(Array.Empty<object>()))
    {
      if (int.Parse(PXResult<TPeriodSetup>.op_Implicit(pxResult).PeriodNbr) > int.Parse(row.PeriodNbr))
      {
        flag2 = true;
        break;
      }
    }
    if (!flag1 & flag2)
      throw new PXException("Financial Period cannot be deleted. You must delete all Financial Periods after this Financial Period first.");
  }

  protected virtual void TPeriodSetupOnRowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    TPeriodSetup row = (TPeriodSetup) e.Row;
    TYearSetup current1 = ((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current;
    int num = 0;
    TPeriodSetup current2 = default (TPeriodSetup);
    foreach (PXResult<TPeriodSetup> pxResult in this.Periods.Select(Array.Empty<object>()))
    {
      TPeriodSetup periodSetup = PXResult<TPeriodSetup>.op_Implicit(pxResult);
      ++num;
      if ((object) current2 == null)
        current2 = periodSetup;
      else if (periodSetup.StartDate.Value > current2.StartDate.Value)
        current2 = periodSetup;
    }
    if (!string.IsNullOrEmpty(row.PeriodNbr))
      return;
    int year = current1.BegFinYear.Value.Year;
    if (current1.UserDefined.GetValueOrDefault() && current1.FinPeriods.GetValueOrDefault() < (short) 1 && current1.PeriodLength.GetValueOrDefault() < (short) 5)
      throw new PXException("The row cannot be inserted.");
    if (!this.PeriodCreator.fillNextPeriod(row, current2))
    {
      this.isLastInsertingPeriod = true;
      if (!this.isAutoInsert)
        throw new PXException("All the periods are already inserted");
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      if (!PXDBLocalizableStringAttribute.IsEnabled || !(((PXSelectBase) (object) this.Periods).Cache.GetValueExt((object) row, "DescrTranslations") is string[] valueExt) || valueExt.Length == 0)
        return;
      valueExt[0] = row.Descr;
      ((PXSelectBase) (object) this.Periods).Cache.SetValueExt((object) row, "DescrTranslations", (object) valueExt);
    }
  }

  protected virtual void TPeriodSetupOnRowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    TPeriodSetup row = (TPeriodSetup) e.Row;
    TPeriodSetup oldRow = (TPeriodSetup) e.OldRow;
    if (this.massUpdateFlag || !(oldRow.EndDate.Value != row.EndDate.Value))
      return;
    string str = (int.Parse(row.PeriodNbr) + 1).ToString("00");
    foreach (PXResult<TPeriodSetup> pxResult in this.Periods.Select(Array.Empty<object>()))
    {
      TPeriodSetup periodSetup = PXResult<TPeriodSetup>.op_Implicit(pxResult);
      if (periodSetup.PeriodNbr == str)
      {
        periodSetup.StartDate = row.EndDate;
        GraphHelper.MarkUpdated(((PXSelectBase) (object) this.Periods).Cache, (object) periodSetup);
        ((PXSelectBase) (object) this.Periods).View.RequestRefresh();
        break;
      }
    }
  }

  protected virtual void TPeriodSetupOnEndDateFieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    TPeriodSetup row = (TPeriodSetup) e.Row;
    DateTime? newValue = (DateTime?) e.NewValue;
    if (!newValue.HasValue)
      return;
    DateTime? nullable = row.StartDate;
    if (!nullable.HasValue)
      return;
    nullable = newValue;
    DateTime dateTime = row.StartDate.Value;
    if ((nullable.HasValue ? (nullable.GetValueOrDefault() < dateTime ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException("Financial Period End Date must be greater or equal then the Start Date.");
    nullable = newValue;
    DateTime yearEnd = this.PeriodCreator.YearEnd;
    if ((nullable.HasValue ? (nullable.GetValueOrDefault() > yearEnd ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException("The End Date of the financial period must belong to the same financial year as its Start Date.");
  }

  protected virtual void TPeriodSetupOnRowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    TYearSetup current = ((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current;
    if (((PXSelectBase) this.FiscalYearSetup).Cache.GetStatus((object) current) != null)
      return;
    ((PXGraph) this).Caches[typeof (TYearSetup)].SetStatus((object) current, (PXEntryStatus) 1);
  }

  protected virtual void TPeriodSetupOnRowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    TYearSetup current = ((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current;
    if (((PXSelectBase) this.FiscalYearSetup).Cache.GetStatus((object) current) != null)
      return;
    ((PXGraph) this).Caches[typeof (TYearSetup)].SetStatus((object) current, (PXEntryStatus) 1);
  }

  private void DoIncrementFirstYear(TYearSetup yearSetup, bool negative)
  {
    int num = negative ? -1 : 1;
    DateTime? nullable1 = new DateTime?(yearSetup.BegFinYear.Value.AddYears(num));
    FiscalPeriodSetupCreator<TPeriodSetup> periodSetupCreator = new FiscalPeriodSetupCreator<TPeriodSetup>((IYearSetup) yearSetup);
    try
    {
      this.skipPeriodReset = true;
      ((PXSelectBase) this.FiscalYearSetup).Cache.SetValueExt<YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.begFinYear>((object) yearSetup, (object) nullable1);
    }
    finally
    {
      this.skipPeriodReset = false;
    }
    if (FiscalPeriodSetupCreator.IsFixedLengthPeriod(yearSetup.FPType))
    {
      DateTime? nullable2 = new DateTime?(periodSetupCreator.CalcPeriodsStartDate(yearSetup.FirstFinYear));
      yearSetup.PeriodsStartDate = nullable2;
    }
    else
      this.RecalcPeriodStartDate(yearSetup);
    try
    {
      this.skipPeriodReset = true;
      TYearSetup yearSetup1 = (TYearSetup) ((PXSelectBase) this.FiscalYearSetup).Cache.Update((object) yearSetup);
    }
    finally
    {
      this.skipPeriodReset = false;
    }
    if (FiscalPeriodSetupCreator.IsFixedLengthPeriod(yearSetup.FPType))
      return;
    this.massUpdateFlag = true;
    foreach (PXResult<TPeriodSetup> pxResult in this.Periods.Select(Array.Empty<object>()))
    {
      TPeriodSetup copy = (TPeriodSetup) ((PXSelectBase) (object) this.Periods).Cache.CreateCopy((object) PXResult<TPeriodSetup>.op_Implicit(pxResult));
      // ISSUE: variable of a boxed type
      __Boxed<TPeriodSetup> local1 = (object) copy;
      DateTime? nullable3 = copy.StartDate;
      DateTime dateTime = nullable3.Value;
      DateTime? nullable4 = new DateTime?(dateTime.AddYears(num));
      local1.StartDate = nullable4;
      // ISSUE: variable of a boxed type
      __Boxed<TPeriodSetup> local2 = (object) copy;
      nullable3 = copy.EndDate;
      dateTime = nullable3.Value;
      DateTime? nullable5 = new DateTime?(dateTime.AddYears(num));
      local2.EndDate = nullable5;
      this.Periods.Update(copy);
    }
    this.massUpdateFlag = false;
  }

  protected abstract bool IsFiscalYearExists();

  protected abstract bool IsFiscalYearSetupExists();

  protected abstract bool CheckForPartiallyDefinedYear();

  protected abstract bool AllowDeleteYearSetup(out string errMsg);

  protected virtual bool AllowAlterStartDate() => !this.IsFiscalYearExists();

  protected virtual bool AllowSetupModification(TYearSetup aRow) => true;

  private bool AutoFillPeriods()
  {
    int num1 = int.Parse(((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current.FirstFinYear);
    if (num1 < 0 || num1 > 9999 || !this.DeletePeriods())
      return false;
    int num2 = 0;
    this.isAutoInsert = true;
    try
    {
      do
      {
        this.isLastInsertingPeriod = false;
        ((PXSelectBase) (object) this.Periods).Cache.Insert((object) new TPeriodSetup());
        ++num2;
        if (this.isLastInsertingPeriod)
          break;
      }
      while (num2 < 1000);
    }
    finally
    {
      this.isAutoInsert = false;
    }
    if (num2 >= 1000)
      throw new PXException("System error - Infinit Loop detected");
    return true;
  }

  private bool DeletePeriods()
  {
    this.isMassDelete = true;
    try
    {
      foreach (PXResult<TPeriodSetup> pxResult in this.Periods.Select(Array.Empty<object>()))
        ((PXSelectBase) (object) this.Periods).Cache.Delete((object) PXResult<TPeriodSetup>.op_Implicit(pxResult));
    }
    finally
    {
      this.isMassDelete = false;
    }
    return true;
  }

  private DateTime? AdjustPeriodStartOnDate(TYearSetup row)
  {
    if (row.FPType != FiscalPeriodSetupCreator.FPType.Week)
      return row.PeriodsStartDate;
    if (row.EndYearCalcMethod == "LD")
    {
      int num1 = 0;
      int num2 = (int) (row.BegFinYear.Value.DayOfWeek + 1);
      int? endYearDayOfWeek = row.EndYearDayOfWeek;
      int valueOrDefault = endYearDayOfWeek.GetValueOrDefault();
      DateTime? begFinYear;
      if (!(num2 == valueOrDefault & endYearDayOfWeek.HasValue))
      {
        begFinYear = row.BegFinYear;
        int num3 = (int) (begFinYear.Value.DayOfWeek + 1);
        endYearDayOfWeek = row.EndYearDayOfWeek;
        int num4 = endYearDayOfWeek.Value;
        int num5;
        if (num3 > num4)
        {
          begFinYear = row.BegFinYear;
          int num6 = (int) (begFinYear.Value.DayOfWeek + 1);
          endYearDayOfWeek = row.EndYearDayOfWeek;
          int num7 = endYearDayOfWeek.Value;
          num5 = num6 - num7;
        }
        else
        {
          begFinYear = row.BegFinYear;
          int num8 = (int) (begFinYear.Value.DayOfWeek + 1);
          endYearDayOfWeek = row.EndYearDayOfWeek;
          int num9 = endYearDayOfWeek.Value;
          num5 = num8 - num9 + 7;
        }
        num1 = Math.Abs(num5) * -1;
      }
      begFinYear = row.BegFinYear;
      return new DateTime?(begFinYear.Value.AddDays((double) num1));
    }
    int num = (int) (row.EndYearDayOfWeek.Value - 1 - row.BegFinYear.Value.DayOfWeek);
    if ((double) Math.Abs(num) > Math.Floor(3.5))
      num = num > 0 ? num - 7 : num + 7;
    return new DateTime?(row.BegFinYear.Value.AddDays((double) num));
  }

  protected bool HasPartiallyDefinedYear
  {
    get
    {
      if (!this._hasPartiallyDefinedYear.HasValue)
        this._hasPartiallyDefinedYear = new bool?(this.CheckForPartiallyDefinedYear());
      return this._hasPartiallyDefinedYear.Value;
    }
  }

  private FiscalPeriodSetupCreator<TPeriodSetup> PeriodCreator
  {
    get
    {
      if (this._periodCreator == null)
      {
        TYearSetup current = ((PXSelectBase<TYearSetup>) this.FiscalYearSetup).Current;
        if ((object) current != null)
        {
          if (current.FPType != FiscalPeriodSetupCreator.FPType.Custom)
          {
            this._periodCreator = new FiscalPeriodSetupCreator<TPeriodSetup>((IYearSetup) current);
          }
          else
          {
            short valueOrDefault = current.FinPeriods.GetValueOrDefault();
            this._periodCreator = new FiscalPeriodSetupCreator<TPeriodSetup>(current.FirstFinYear, current.BegFinYear.Value, valueOrDefault);
          }
        }
      }
      return this._periodCreator;
    }
  }

  public abstract class YearSetup
  {
    public abstract class belongsToNextYear : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.belongsToNextYear>
    {
    }

    public abstract class begFinYear : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.begFinYear>
    {
    }

    public abstract class finPeriods : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.finPeriods>
    {
    }

    public abstract class userDefined : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.userDefined>
    {
    }

    public abstract class periodLength : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.periodLength>
    {
    }

    public abstract class periodsStartDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.periodsStartDate>
    {
    }

    public abstract class periodType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.periodType>
    {
    }

    public abstract class hasAdjustmentPeriod : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.hasAdjustmentPeriod>
    {
    }

    public abstract class endYearCalcMethod : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.endYearCalcMethod>
    {
    }

    public abstract class endYearDayOfWeek : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.YearSetup.endYearDayOfWeek>
    {
    }

    public abstract class yearLastDayOfWeek : IBqlField, IBqlOperand
    {
    }
  }

  public abstract class PeriodSetup
  {
    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.PeriodSetup.endDate>
    {
    }

    public abstract class endDateUI : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, TPeriodSetupSelect>.PeriodSetup.endDateUI>
    {
    }
  }
}
