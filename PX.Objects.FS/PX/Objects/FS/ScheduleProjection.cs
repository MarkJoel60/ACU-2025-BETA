// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ScheduleProjection
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Globalization;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class ScheduleProjection : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDate(IsKey = true)]
  [PXUIField(DisplayName = "Date")]
  public virtual DateTime? Date { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Start Date of Week")]
  public virtual DateTime? BeginDateOfWeek { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Day of Week")]
  public virtual 
  #nullable disable
  string DayOfWeek
  {
    get
    {
      if (!this.Date.HasValue)
        return (string) null;
      DateTime dateTime = this.Date.Value;
      return PXMessages.LocalizeFormatNoPrefix(TX.RecurrencyFrecuency.daysOfWeek[(int) this.Date.Value.DayOfWeek], Array.Empty<object>());
    }
  }

  [PXInt]
  [PXUIField(DisplayName = "Week of Year")]
  public virtual int? WeekOfYear
  {
    get
    {
      if (!this.Date.HasValue)
        return new int?();
      DateTime dateTime = this.Date.Value;
      DateTime time = this.Date.Value;
      switch (CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time))
      {
        case System.DayOfWeek.Monday:
        case System.DayOfWeek.Tuesday:
        case System.DayOfWeek.Wednesday:
          time = time.AddDays(3.0);
          break;
      }
      return new int?(CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, System.DayOfWeek.Monday));
    }
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ScheduleProjection.date>
  {
  }

  public abstract class beginDateOfWeek : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ScheduleProjection.beginDateOfWeek>
  {
  }

  public abstract class dayOfWeek : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ScheduleProjection.dayOfWeek>
  {
  }

  public abstract class weekOfYear : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ScheduleProjection.weekOfYear>
  {
  }
}
