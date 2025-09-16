// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.PXWeekSelector2Attribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.EP;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.EP;

/// <summary>
/// Allow select weeks.
/// Shows start and end date of week, fixed diapason only.
/// </summary>
/// <example>[PXWeekSelector2]</example>
[Serializable]
public class PXWeekSelector2Attribute : PXWeekSelectorAttribute
{
  public PXWeekSelector2Attribute()
    : base(typeof (EPWeekRaw.weekID), new Type[3]
    {
      typeof (EPWeekRaw.fullNumber),
      typeof (EPWeekRaw.startDate),
      typeof (EPWeekRaw.endDate)
    })
  {
  }

  protected static EPSetup GetEPSetup(PXGraph graph)
  {
    return ((Dictionary<Type, PXCache>) graph.Caches).ContainsKey(typeof (EPSetup)) && graph.Caches[typeof (EPSetup)].Current != null ? graph.Caches[typeof (EPSetup)].Current as EPSetup : PXResultset<EPSetup>.op_Implicit(PXSelectBase<EPSetup, PXSelectReadonly<EPSetup>.Config>.SelectSingleBound(graph, (object[]) null, Array.Empty<object>()));
  }

  protected override IEnumerable GetAllRecords()
  {
    return (IEnumerable) PXWeekSelector2Attribute.FullWeekList.Weeks();
  }

  public static bool IsCustomWeek(PXGraph graph)
  {
    EPSetup epSetup = PXWeekSelector2Attribute.GetEPSetup(graph);
    return epSetup != null && epSetup.CustomWeek.GetValueOrDefault();
  }

  public static bool IsCustomWeek(PXGraph graph, int weekID)
  {
    EPSetup epSetup = PXWeekSelector2Attribute.GetEPSetup(graph);
    if (epSetup == null || !epSetup.CustomWeek.GetValueOrDefault())
      return false;
    int num = weekID;
    int? firstCustomWeekId = epSetup.FirstCustomWeekID;
    int valueOrDefault = firstCustomWeekId.GetValueOrDefault();
    return num >= valueOrDefault & firstCustomWeekId.HasValue;
  }

  private static EPCustomWeek GetCustomWeek(PXGraph graph, DateTime date)
  {
    return PXResultset<EPCustomWeek>.op_Implicit(PXSelectBase<EPCustomWeek, PXSelect<EPCustomWeek, Where<Required<EPCustomWeek.startDate>, Between<EPCustomWeek.startDate, EPCustomWeek.endDate>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) date
    })) ?? throw new PXException("Custom Week cannot be found. Custom Week's must be generated with date greater than {0:d}", new object[1]
    {
      (object) date
    });
  }

  private static EPCustomWeek GetCustomWeek(PXGraph graph, int weekID)
  {
    return PXResultset<EPCustomWeek>.op_Implicit(PXSelectBase<EPCustomWeek, PXSelect<EPCustomWeek, Where<EPCustomWeek.weekID, Equal<Required<EPCustomWeek.weekID>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) weekID
    })) ?? throw new PXException("Custom Week cannot be found");
  }

  private static EPWeekRaw GetStandartWeek(PXGraph graph, DateTime date)
  {
    return EPWeekRaw.ToEPWeekRaw(new PXWeekSelectorAttribute.EPWeek()
    {
      WeekID = new int?(date.Year * 100 + PXDateTimeInfo.GetWeekNumber(date))
    });
  }

  public static int GetWeekID(PXGraph graph, DateTime date)
  {
    return PXWeekSelector2Attribute.IsCustomWeek(graph) ? PXWeekSelector2Attribute.GetCustomWeek(graph, date).WeekID.Value : PXWeekSelectorAttribute.GetWeekID(date);
  }

  public static int GetNextWeekID(PXGraph graph, int weekID)
  {
    return PXWeekSelector2Attribute.IsCustomWeek(graph, weekID) ? PXWeekSelector2Attribute.GetWeekID(graph, PXWeekSelector2Attribute.GetCustomWeek(graph, weekID).EndDate.Value.AddDays(1.0)) : PXWeekSelectorAttribute.GetWeekID(PXWeekSelectorAttribute.GetWeekStartDate(weekID).AddDays(7.0));
  }

  public static int GetNextWeekID(PXGraph graph, DateTime date)
  {
    return PXWeekSelector2Attribute.IsCustomWeek(graph) ? PXWeekSelector2Attribute.GetWeekID(graph, PXWeekSelector2Attribute.GetCustomWeek(graph, date).EndDate.Value.AddDays(1.0)) : PXWeekSelectorAttribute.GetWeekID(date.AddDays(7.0));
  }

  public static DateTime GetWeekStartDate(PXGraph graph, int weekId)
  {
    return PXWeekSelector2Attribute.IsCustomWeek(graph, weekId) ? PXWeekSelector2Attribute.GetCustomWeek(graph, weekId).StartDate.Value : PXWeekSelectorAttribute.GetWeekStartDate(weekId);
  }

  public static DateTime GetWeekEndDate(PXGraph graph, int weekId)
  {
    return PXWeekSelector2Attribute.IsCustomWeek(graph, weekId) ? PXWeekSelector2Attribute.GetCustomWeek(graph, weekId).EndDate.Value : PXWeekSelectorAttribute.GetWeekStartDate(weekId).AddDays(6.0);
  }

  public static DateTime GetWeekEndDateTime(PXGraph graph, int weekId)
  {
    return PXWeekSelector2Attribute.GetWeekEndDate(graph, weekId).Date.AddDays(1.0).AddSeconds(-1.0);
  }

  public static DateTime GetWeekStartDateUtc(PXGraph graph, int weekId)
  {
    PXTimeZoneInfo timeZone = LocaleInfo.GetTimeZone();
    return PXTimeZoneInfo.ConvertTimeToUtc(PXWeekSelector2Attribute.GetWeekStartDate(graph, weekId), timeZone);
  }

  public static DateTime GetWeekEndDateUtc(PXGraph graph, int weekId)
  {
    PXTimeZoneInfo timeZone = LocaleInfo.GetTimeZone();
    return PXTimeZoneInfo.ConvertTimeToUtc(PXWeekSelector2Attribute.GetWeekEndDateTime(graph, weekId), timeZone);
  }

  public static PXWeekSelector2Attribute.WeekInfo GetWeekInfo(PXGraph graph, int weekId)
  {
    PXWeekSelector2Attribute.WeekInfo weekInfo = new PXWeekSelector2Attribute.WeekInfo();
    for (DateTime date = PXWeekSelector2Attribute.GetWeekStartDate(graph, weekId); date <= PXWeekSelector2Attribute.GetWeekEndDate(graph, weekId); date = date.AddDays(1.0))
      weekInfo.AddDayInfo(date);
    return weekInfo;
  }

  public class FullWeekList
  {
    public static List<EPWeekRaw> Weeks()
    {
      PXWeekSelector2Attribute.FullWeekList.Definition definition = PXWeekSelector2Attribute.FullWeekList.Definition.Get();
      return definition == null ? new List<EPWeekRaw>() : definition.Weeks;
    }

    private class Definition : IPrefetchable, IPXCompanyDependent
    {
      private DateTime _startDay = new DateTime(2005, 1, 1);
      private List<EPWeekRaw> _weeks = new List<EPWeekRaw>();

      public void Prefetch()
      {
        int? firstWeekID;
        List<EPWeekRaw> customWeeks = this.GetCustomWeeks(out firstWeekID);
        int num1 = DateTime.UtcNow.Year + 3;
        DateTime date = this._startDay;
        int num2;
        int num3;
        do
        {
          int weekNumber = PXDateTimeInfo.GetWeekNumber(date);
          num2 = date.AddDays(-3.0).Year >= date.Year || weekNumber <= 1 ? (date.AddDays(3.0).Year <= date.Year || weekNumber != 1 ? date.Year : date.AddYears(1).Year) : date.AddYears(-1).Year;
          num3 = num2 * 100 + weekNumber;
          this._weeks.Add(EPWeekRaw.ToEPWeekRaw(new PXWeekSelectorAttribute.EPWeek()
          {
            WeekID = new int?(num3)
          }));
          date = date.AddDays(7.0);
        }
        while ((firstWeekID.HasValue ? (num3 < firstWeekID.Value ? 1 : 0) : (num2 < num1 ? 1 : 0)) != 0);
        this._weeks.AddRange((IEnumerable<EPWeekRaw>) customWeeks);
      }

      private List<EPWeekRaw> GetCustomWeeks(out int? firstWeekID)
      {
        firstWeekID = new int?();
        List<EPWeekRaw> customWeeks = new List<EPWeekRaw>();
        using (new PXConnectionScope())
        {
          bool flag = false;
          using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<EPSetup>(new PXDataField[1]
          {
            (PXDataField) new PXDataField<EPSetup.customWeek>()
          }))
            flag = pxDataRecord != null && pxDataRecord.GetBoolean(0).GetValueOrDefault();
          if (flag)
          {
            foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<EPCustomWeek>(new PXDataField[7]
            {
              (PXDataField) new PXDataField<EPCustomWeek.weekID>(),
              (PXDataField) new PXDataField<EPCustomWeek.year>(),
              (PXDataField) new PXDataField<EPCustomWeek.number>(),
              (PXDataField) new PXDataField<EPCustomWeek.startDate>(),
              (PXDataField) new PXDataField<EPCustomWeek.endDate>(),
              (PXDataField) new PXDataField<EPCustomWeek.isFullWeek>(),
              (PXDataField) new PXDataField<EPCustomWeek.isActive>()
            }))
            {
              int? int32 = pxDataRecord.GetInt32(0);
              if (!firstWeekID.HasValue)
                firstWeekID = int32;
              customWeeks.Add(EPWeekRaw.ToEPWeekRaw(new EPCustomWeek()
              {
                WeekID = int32,
                Year = pxDataRecord.GetInt32(1),
                Number = pxDataRecord.GetInt32(2),
                StartDate = pxDataRecord.GetDateTime(3),
                EndDate = pxDataRecord.GetDateTime(4),
                IsFullWeek = pxDataRecord.GetBoolean(5),
                IsActive = pxDataRecord.GetBoolean(6)
              }));
            }
          }
        }
        return customWeeks;
      }

      public static PXWeekSelector2Attribute.FullWeekList.Definition Get()
      {
        return PXDatabase.GetSlot<PXWeekSelector2Attribute.FullWeekList.Definition>(typeof (PXWeekSelector2Attribute.FullWeekList).Name, new Type[1]
        {
          typeof (EPSetup)
        });
      }

      public List<EPWeekRaw> Weeks => this._weeks;
    }
  }

  public class WeekInfo
  {
    private Dictionary<DayOfWeek, PXWeekSelector2Attribute.DayInfo> _days = new Dictionary<DayOfWeek, PXWeekSelector2Attribute.DayInfo>();

    public PXWeekSelector2Attribute.DayInfo Mon => this.GetDayInfo(DayOfWeek.Monday);

    public PXWeekSelector2Attribute.DayInfo Tue => this.GetDayInfo(DayOfWeek.Tuesday);

    public PXWeekSelector2Attribute.DayInfo Wed => this.GetDayInfo(DayOfWeek.Wednesday);

    public PXWeekSelector2Attribute.DayInfo Thu => this.GetDayInfo(DayOfWeek.Thursday);

    public PXWeekSelector2Attribute.DayInfo Fri => this.GetDayInfo(DayOfWeek.Friday);

    public PXWeekSelector2Attribute.DayInfo Sat => this.GetDayInfo(DayOfWeek.Saturday);

    public PXWeekSelector2Attribute.DayInfo Sun => this.GetDayInfo(DayOfWeek.Sunday);

    private PXWeekSelector2Attribute.DayInfo GetDayInfo(DayOfWeek mDay)
    {
      return this._days.ContainsKey(mDay) ? this._days[mDay] : new PXWeekSelector2Attribute.DayInfo(new DateTime?());
    }

    public void AddDayInfo(DateTime date)
    {
      this._days[date.DayOfWeek] = new PXWeekSelector2Attribute.DayInfo(new DateTime?(date));
    }

    public Dictionary<DayOfWeek, PXWeekSelector2Attribute.DayInfo> Days => this._days;

    public bool IsValid(DateTime date)
    {
      foreach (PXWeekSelector2Attribute.DayInfo dayInfo in this._days.Values)
      {
        if (dayInfo.Enabled)
        {
          DateTime date1 = dayInfo.Date.Value;
          DateTime date2 = date1.Date;
          date1 = date.Date;
          DateTime date3 = date1.Date;
          if (date2 == date3)
            return true;
        }
      }
      return false;
    }
  }

  public class DayInfo
  {
    private DateTime? _date;

    public DayInfo(DateTime? date) => this._date = date;

    public DateTime? Date => this._date;

    public bool Enabled => this._date.HasValue;
  }
}
