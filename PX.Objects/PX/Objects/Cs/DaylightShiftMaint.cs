// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.DaylightShiftMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CS;

public class DaylightShiftMaint : PXGraph<DaylightShiftMaint>
{
  private static readonly HybridDictionary _timeZonesHashtable = new HybridDictionary();
  public PXFilter<DaylightShiftFilter> Filter;
  public PXSelect<DaylightShift, Where<DaylightShift.year, Equal<Current<DaylightShiftFilter.year>>>, OrderBy<Asc<DaylightShift.originalShift>>> Calendar;
  public PXSave<DaylightShiftFilter> Save;
  public PXCancel<DaylightShiftFilter> Cancel;
  public PXAction<DaylightShiftFilter> Previous;
  public PXAction<DaylightShiftFilter> Next;

  static DaylightShiftMaint()
  {
    foreach (PXTimeZoneInfo systemTimeZone in PXTimeZoneInfo.GetSystemTimeZones())
      DaylightShiftMaint._timeZonesHashtable.Add((object) systemTimeZone.Id, (object) systemTimeZone);
  }

  [PXUIField]
  [PXPreviousButton]
  protected IEnumerable previous(PXAdapter adapter)
  {
    ((PXSelectBase<DaylightShiftFilter>) this.Filter).Current.Year = new int?(((PXSelectBase<DaylightShiftFilter>) this.Filter).Current.Year.Return<int?, int>((Func<int?, int>) (_ => _.Value - 1), DateTime.Today.Year));
    return adapter.Get();
  }

  [PXUIField]
  [PXNextButton]
  protected IEnumerable next(PXAdapter adapter)
  {
    ((PXSelectBase<DaylightShiftFilter>) this.Filter).Current.Year = new int?(((PXSelectBase<DaylightShiftFilter>) this.Filter).Current.Year.Return<int?, int>((Func<int?, int>) (_ => _.Value + 1), DateTime.Today.Year));
    return adapter.Get();
  }

  protected IEnumerable calendar()
  {
    DaylightShiftMaint daylightShiftMaint = this;
    int? nullable = ((PXSelectBase<DaylightShiftFilter>) daylightShiftMaint.Filter).Current.With<DaylightShiftFilter, int?>((Func<DaylightShiftFilter, int?>) (_ => _.Year));
    if (nullable.HasValue)
    {
      HybridDictionary hashtable = new HybridDictionary();
      foreach (PXResult<DaylightShift> pxResult in PXSelectBase<DaylightShift, PXSelect<DaylightShift, Where<DaylightShift.year, Equal<Current<DaylightShiftFilter.year>>, And<DaylightShift.isActive, Equal<True>>>>.Config>.Select((PXGraph) daylightShiftMaint, Array.Empty<object>()))
      {
        DaylightShift row = PXResult<DaylightShift>.op_Implicit(pxResult);
        row.OriginalShift = new double?(((PXTimeZoneInfo) DaylightShiftMaint._timeZonesHashtable[(object) row.TimeZone]).BaseUtcOffset.TotalMinutes);
        yield return (object) row;
        hashtable.Add((object) row.TimeZone, (object) row);
        row = (DaylightShift) null;
      }
      if (hashtable.Count < DaylightShiftMaint._timeZonesHashtable.Count)
      {
        SystemTimeRegionProvider provider = new SystemTimeRegionProvider();
        foreach (DictionaryEntry dictionaryEntry in DaylightShiftMaint._timeZonesHashtable)
        {
          object key = dictionaryEntry.Key;
          if (!hashtable.Contains(key))
          {
            PXTimeZoneInfo pxTimeZoneInfo = (PXTimeZoneInfo) dictionaryEntry.Value;
            nullable = ((PXSelectBase<DaylightShiftFilter>) daylightShiftMaint.Filter).Current.Year;
            int year = nullable.Value;
            DaylightShift daylightShift = new DaylightShift()
            {
              Year = new int?(year),
              TimeZone = (string) key,
              TimeZoneDescription = PXMessages.LocalizeFormatNoPrefix(pxTimeZoneInfo.DisplayName, Array.Empty<object>()),
              IsActive = new bool?(false),
              OriginalShift = new double?(pxTimeZoneInfo.BaseUtcOffset.TotalMinutes)
            };
            DaylightSavingTime daylightSavingTime = provider.FindTimeRegionByTimeZone((string) key).With<ITimeRegion, DaylightSavingTime>((Func<ITimeRegion, DaylightSavingTime>) (_ => new DaylightSavingTime(year, _)));
            if (daylightSavingTime != null && daylightSavingTime.IsActive)
            {
              daylightShift.ToDate = new DateTime?(daylightSavingTime.End);
              daylightShift.FromDate = new DateTime?(daylightSavingTime.Start);
              daylightShift.Shift = new int?((int) daylightSavingTime.DaylightOffset.TotalMinutes);
            }
            yield return (object) daylightShift;
          }
        }
        provider = (SystemTimeRegionProvider) null;
      }
    }
  }

  protected virtual void DaylightShift_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is DaylightShift row) || !row.Year.HasValue || row.TimeZone == null || !row.IsActive.GetValueOrDefault())
      return;
    this.DefaultEditableRow(row);
  }

  protected virtual void DaylightShift_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is DaylightShift row))
      return;
    int? nullable1 = row.Year;
    if (!nullable1.HasValue || row.TimeZone == null)
      return;
    if (row.IsActive.GetValueOrDefault())
    {
      this.DefaultEditableRow(row);
    }
    else
    {
      string timeZone = row.TimeZone;
      nullable1 = row.Year;
      int year = nullable1.Value;
      DaylightSavingTime dst = this.GetDST(timeZone, year);
      if (dst != null && dst.IsActive)
      {
        row.FromDate = new DateTime?(dst.Start);
        row.ToDate = new DateTime?(dst.End);
        row.Shift = new int?((int) dst.DaylightOffset.TotalMinutes);
      }
      else
      {
        row.ToDate = new DateTime?();
        row.FromDate = new DateTime?();
        DaylightShift daylightShift = row;
        nullable1 = new int?();
        int? nullable2 = nullable1;
        daylightShift.Shift = nullable2;
      }
    }
  }

  private DaylightSavingTime GetDST(string timeZone, int year)
  {
    return new SystemTimeRegionProvider().FindTimeRegionByTimeZone(timeZone).With<ITimeRegion, DaylightSavingTime>((Func<ITimeRegion, DaylightSavingTime>) (_ => new DaylightSavingTime(year, _)));
  }

  private void DefaultEditableRow(DaylightShift row)
  {
    DaylightShift daylightShift = PXResultset<DaylightShift>.op_Implicit(PXSelectBase<DaylightShift, PXSelectReadonly<DaylightShift, Where<DaylightShift.year, Equal<Current<DaylightShiftFilter.year>>, And<DaylightShift.timeZone, Equal<Required<DaylightShift.timeZone>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.TimeZone
    }));
    DateTime date = DateTime.UtcNow.Date;
    DateTime dateTime1 = new DateTime(row.Year.Value, date.Month, date.Day) + new TimeSpan(12, 0, 0);
    DateTime dateTime2 = dateTime1;
    DateTime dateTime3 = dateTime1.AddDays(1.0);
    int num1 = 60;
    DaylightSavingTime dst = this.GetDST(row.TimeZone, dateTime1.Year);
    if (dst != null && dst.IsActive)
    {
      dateTime2 = dst.Start;
      dateTime3 = dst.End;
      num1 = (int) dst.DaylightOffset.TotalMinutes;
    }
    if (daylightShift != null)
    {
      row.Year = daylightShift.Year;
      DateTime? nullable;
      if (row.ToDate.HasValue)
      {
        nullable = row.ToDate;
        DateTime dateTime4 = dateTime3;
        if ((nullable.HasValue ? (nullable.GetValueOrDefault() == dateTime4 ? 1 : 0) : 0) == 0)
          goto label_6;
      }
      row.ToDate = daylightShift.ToDate;
label_6:
      nullable = row.FromDate;
      if (nullable.HasValue)
      {
        nullable = row.FromDate;
        DateTime dateTime5 = dateTime2;
        if ((nullable.HasValue ? (nullable.GetValueOrDefault() == dateTime5 ? 1 : 0) : 0) == 0)
          goto label_9;
      }
      row.FromDate = daylightShift.FromDate;
label_9:
      int? shift = row.Shift;
      if (shift.HasValue)
      {
        shift = row.Shift;
        int num2 = num1;
        if (!(shift.GetValueOrDefault() == num2 & shift.HasValue))
          return;
      }
      row.Shift = daylightShift.Shift;
    }
    else
    {
      int? nullable1 = row.Year;
      if (!nullable1.HasValue)
        row.Year = new int?(dateTime1.Year);
      DateTime? nullable2 = row.ToDate;
      if (!nullable2.HasValue)
        row.ToDate = new DateTime?(dateTime3);
      nullable2 = row.FromDate;
      if (!nullable2.HasValue)
        row.FromDate = new DateTime?(dateTime2);
      nullable1 = row.Shift;
      if (nullable1.HasValue)
        return;
      row.Shift = new int?(num1);
    }
  }

  protected virtual void DaylightShift_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is DaylightShift row))
      return;
    int? nullable1 = row.Year;
    if (!nullable1.HasValue || row.TimeZone == null || row.IsActive.GetValueOrDefault())
      return;
    string timeZone = row.TimeZone;
    nullable1 = row.Year;
    int year = nullable1.Value;
    DaylightSavingTime dst = this.GetDST(timeZone, year);
    if (dst != null && dst.IsActive)
    {
      DateTime? fromDate = row.FromDate;
      DateTime start = dst.Start;
      if ((fromDate.HasValue ? (fromDate.GetValueOrDefault() == start ? 1 : 0) : 0) != 0)
      {
        DateTime? toDate = row.ToDate;
        DateTime end = dst.End;
        if ((toDate.HasValue ? (toDate.GetValueOrDefault() == end ? 1 : 0) : 0) != 0)
        {
          nullable1 = row.Shift;
          int totalMinutes = (int) dst.DaylightOffset.TotalMinutes;
          if (nullable1.GetValueOrDefault() == totalMinutes & nullable1.HasValue)
            goto label_10;
        }
      }
    }
    DateTime? nullable2 = row.FromDate;
    if (nullable2.HasValue)
      return;
    nullable2 = row.ToDate;
    if (nullable2.HasValue)
      return;
    nullable1 = row.Shift;
    if (nullable1.HasValue)
      return;
label_10:
    DaylightShift daylightShift = PXResultset<DaylightShift>.op_Implicit(PXSelectBase<DaylightShift, PXSelectReadonly<DaylightShift, Where<DaylightShift.year, Equal<Current<DaylightShiftFilter.year>>, And<DaylightShift.timeZone, Equal<Required<DaylightShift.timeZone>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.TimeZone
    }));
    if (daylightShift != null)
    {
      row.FromDate = daylightShift.FromDate;
      row.ToDate = daylightShift.ToDate;
      row.Shift = daylightShift.Shift;
    }
    else
      ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void DaylightShift_ToDate_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row is DaylightShift row && !row.ToDate.HasValue && row.IsActive.GetValueOrDefault())
      throw new PXSetPropertyException<DaylightShift.toDate>("Cannot be empty.");
  }

  protected virtual void DaylightShift_FromDate_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row is DaylightShift row && !row.FromDate.HasValue && row.IsActive.GetValueOrDefault())
      throw new PXSetPropertyException<DaylightShift.fromDate>("Cannot be empty.");
  }

  protected virtual void DaylightShift_Shift_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row is DaylightShift row && !row.Shift.HasValue && row.IsActive.GetValueOrDefault())
      throw new PXSetPropertyException<DaylightShift.shift>("Cannot be empty.");
  }

  protected virtual void DaylightShift_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is DaylightShift row))
      return;
    PXTimeZoneInfo pxTimeZoneInfo = (PXTimeZoneInfo) DaylightShiftMaint._timeZonesHashtable[(object) row.TimeZone];
    row.TimeZoneDescription = PXMessages.LocalizeFormatNoPrefix(pxTimeZoneInfo.DisplayName, Array.Empty<object>());
    bool valueOrDefault = row.IsActive.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<DaylightShift.toDate>(sender, (object) row, valueOrDefault);
    PXUIFieldAttribute.SetEnabled<DaylightShift.fromDate>(sender, (object) row, valueOrDefault);
    PXUIFieldAttribute.SetEnabled<DaylightShift.shift>(sender, (object) row, valueOrDefault);
    if (!valueOrDefault)
      return;
    DateTime? nullable = row.ToDate;
    if (!nullable.HasValue)
      return;
    nullable = row.FromDate;
    if (!nullable.HasValue)
      return;
    nullable = row.ToDate;
    DateTime dateTime1 = nullable.Value;
    nullable = row.FromDate;
    DateTime dateTime2 = nullable.Value;
    if (!(dateTime1 <= dateTime2))
      return;
    sender.RaiseExceptionHandling<DaylightShift.toDate>((object) row, (object) row.ToDate, (Exception) new PXSetPropertyException("'From' date cannot be less than 'To' date."));
  }
}
