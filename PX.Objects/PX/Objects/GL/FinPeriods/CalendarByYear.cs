// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriods.CalendarByYear
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL.FinPeriods;

internal class CalendarByYear : IPrefetchable<CalendarByYearPrefetchParameters>, IPXCompanyDependent
{
  private readonly Dictionary<int, List<FinPeriod>> periodsByYear = new Dictionary<int, List<FinPeriod>>();
  private readonly Dictionary<string, FinPeriod> periodsByID = new Dictionary<string, FinPeriod>();
  private readonly Dictionary<string, FinPeriod> periodsByMasterID = new Dictionary<string, FinPeriod>();

  private void AddPeriodForCalendarYear(int year, FinPeriod period)
  {
    this.periodsByYear.GetOrAdd<int, List<FinPeriod>>(year, (Func<List<FinPeriod>>) (() => new List<FinPeriod>())).Add(period);
  }

  public void Prefetch(CalendarByYearPrefetchParameters parameters)
  {
    foreach (FinPeriod period in GraphHelper.RowCast<FinPeriod>((IEnumerable) PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>.Config>.Select(parameters.Graph, new object[1]
    {
      (object) parameters.OrgID
    })))
    {
      DateTime? nullable = period.StartDate;
      DateTime dateTime = nullable.Value;
      int year1 = dateTime.Year;
      nullable = period.EndDate;
      dateTime = nullable.Value;
      int year2 = dateTime.Year;
      if (year1 == year2)
      {
        nullable = period.StartDate;
        dateTime = nullable.Value;
        this.AddPeriodForCalendarYear(dateTime.Year, period);
      }
      else
      {
        nullable = period.StartDate;
        dateTime = nullable.Value;
        this.AddPeriodForCalendarYear(dateTime.Year, period);
        nullable = period.EndDate;
        dateTime = nullable.Value;
        this.AddPeriodForCalendarYear(dateTime.Year, period);
      }
      this.periodsByID.Add(period.FinPeriodID, period);
      if (period.MasterFinPeriodID != null)
        this.periodsByMasterID.Add(period.MasterFinPeriodID, period);
    }
  }

  public FinPeriod FetchFinPeriod(DateTime date)
  {
    List<FinPeriod> source;
    return this.periodsByYear.TryGetValue(date.Year, out source) ? source.SingleOrDefault<FinPeriod>((Func<FinPeriod, bool>) (_ => _.StartDate.Value <= date && _.EndDate.Value > date)) : (FinPeriod) null;
  }

  public FinPeriod FetchFinPeriod(string finperiodID)
  {
    if (finperiodID == null)
      return (FinPeriod) null;
    FinPeriod finPeriod;
    return this.periodsByID.TryGetValue(finperiodID, out finPeriod) ? finPeriod : (FinPeriod) null;
  }

  public FinPeriod FetchFinPeriodByMasterID(string finperiodID)
  {
    if (finperiodID == null)
      return (FinPeriod) null;
    FinPeriod finPeriod;
    return this.periodsByMasterID.TryGetValue(finperiodID, out finPeriod) ? finPeriod : (FinPeriod) null;
  }
}
