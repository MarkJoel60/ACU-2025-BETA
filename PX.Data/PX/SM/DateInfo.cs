// Decompiled with JetBrains decompiler
// Type: PX.SM.DateInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;

#nullable enable
namespace PX.SM;

/// <summary>
/// DAC for exposing several calculated date fields for a date.
/// </summary>
[PXCacheName("Date Info")]
public class DateInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBDate(IsKey = true)]
  [PXUIField(DisplayName = "Date", IsReadOnly = true)]
  public virtual System.DateTime? Date { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Year", IsReadOnly = true)]
  public virtual int? Year { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Quarter Of Year", IsReadOnly = true)]
  public virtual int? Quarter { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Month Of Year", IsReadOnly = true)]
  public virtual int? Month { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Day Of Month", IsReadOnly = true)]
  public virtual int? Day { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Date (Integer)", IsReadOnly = true)]
  public virtual int? DateInt { get; set; }

  [PXString]
  [PXDependsOnFields(new System.Type[] {typeof (DateInfo.date)})]
  [PXUIField(DisplayName = "Month Name", IsReadOnly = true)]
  public virtual 
  #nullable disable
  string MonthName
  {
    get
    {
      System.DateTime? date = this.Date;
      ref System.DateTime? local = ref date;
      return !local.HasValue ? (string) null : local.GetValueOrDefault().ToString("MMMM");
    }
  }

  [PXString]
  [PXDependsOnFields(new System.Type[] {typeof (DateInfo.date)})]
  [PXUIField(DisplayName = "Month In Calendar", IsReadOnly = true)]
  public virtual string MonthInCalendar
  {
    get
    {
      System.DateTime? date = this.Date;
      ref System.DateTime? local = ref date;
      return !local.HasValue ? (string) null : local.GetValueOrDefault().ToString("MMM yyyy");
    }
  }

  [PXString]
  [PXDependsOnFields(new System.Type[] {typeof (DateInfo.date), typeof (DateInfo.quarter)})]
  [PXUIField(DisplayName = "Quarter", IsReadOnly = true)]
  public virtual string QuarterInCalendar
  {
    get
    {
      if (!this.Quarter.HasValue || !this.Date.HasValue)
        return (string) null;
      __Boxed<int?> quarter = (ValueType) this.Quarter;
      System.DateTime? date = this.Date;
      ref System.DateTime? local = ref date;
      string str = local.HasValue ? local.GetValueOrDefault().ToString("yyyy") : (string) null;
      return $"Q{quarter} {str}";
    }
  }

  [PXInt]
  [DateInfo.DayInWeek(typeof (DateInfo.date))]
  [PXUIField(DisplayName = "Day In Week", IsReadOnly = true)]
  public virtual int? DayInWeek { get; set; }

  [PXString]
  [PXDependsOnFields(new System.Type[] {typeof (DateInfo.date)})]
  [PXUIField(DisplayName = "Day Of Week (Name)", IsReadOnly = true)]
  public virtual string DayOfWeekName
  {
    get
    {
      System.DateTime? date = this.Date;
      ref System.DateTime? local = ref date;
      return !local.HasValue ? (string) null : local.GetValueOrDefault().ToString("dddd");
    }
  }

  [PXDate]
  [DateInfo.WeekEnding(typeof (DateInfo.date))]
  [PXUIField(DisplayName = "Week Ending", IsReadOnly = true)]
  public virtual System.DateTime? WeekEnding { get; set; }

  public class PK : PrimaryKeyOf<DateInfo>.By<DateInfo.date>
  {
    public static DateInfo Find(PXGraph graph, System.DateTime? date, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<DateInfo>.By<DateInfo.date>.FindBy(graph, (object) date, options);
    }
  }

  /// <exclude />
  private abstract class DateDerivativeBaseAttribute : 
    PXEventSubscriberAttribute,
    IPXFieldSelectingSubscriber,
    IPXDependsOnFields
  {
    private readonly System.Type _dateField;

    protected DateDerivativeBaseAttribute(System.Type dateField)
    {
      if (dateField == (System.Type) null)
        throw new ArgumentNullException(nameof (dateField));
      this._dateField = typeof (IBqlField).IsAssignableFrom(dateField) ? dateField : throw new ArgumentException(MainTools.GetLongName(typeof (IBqlField)) + " expected.", nameof (dateField));
    }

    public void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      if (e.Row == null)
        return;
      object obj = sender.GetValue(e.Row, this._dateField.Name);
      if (obj == null)
        return;
      System.DateTime date = (System.DateTime) obj;
      e.ReturnValue = this.GetValue(date, sender.Graph.Culture);
    }

    protected abstract object GetValue(System.DateTime date, CultureInfo cultureInfo);

    public ISet<System.Type> GetDependencies(PXCache cache)
    {
      return (ISet<System.Type>) new HashSet<System.Type>()
      {
        this._dateField
      };
    }
  }

  /// <exclude />
  private class DayInWeekAttribute(System.Type dateField) : DateInfo.DateDerivativeBaseAttribute(dateField)
  {
    protected int GetDayInWeek(DayOfWeek dayOfWeek, CultureInfo cultureInfo)
    {
      int firstDayOfWeek = (int) cultureInfo.DateTimeFormat.FirstDayOfWeek;
      int dayInWeek = (int) (dayOfWeek - firstDayOfWeek);
      if (dayInWeek < 0)
        dayInWeek += 7;
      return dayInWeek;
    }

    protected override object GetValue(System.DateTime date, CultureInfo cultureInfo)
    {
      return (object) this.GetDayInWeek(date.DayOfWeek, cultureInfo);
    }
  }

  /// <exclude />
  private class WeekEndingAttribute(System.Type dateField) : DateInfo.DayInWeekAttribute(dateField)
  {
    private System.DateTime GetWeekEnding(System.DateTime date, CultureInfo cultureInfo)
    {
      int dayInWeek = this.GetDayInWeek(date.DayOfWeek, cultureInfo);
      return date.AddDays((double) -dayInWeek).AddDays(7.0);
    }

    protected override object GetValue(System.DateTime date, CultureInfo cultureInfo)
    {
      return (object) this.GetWeekEnding(date, cultureInfo);
    }
  }

  /// <exclude />
  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  DateInfo.date>
  {
  }

  public abstract class year : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DateInfo.year>
  {
  }

  /// <exclude />
  public abstract class quarter : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DateInfo.quarter>
  {
  }

  /// <exclude />
  public abstract class month : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DateInfo.month>
  {
  }

  /// <exclude />
  public abstract class day : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DateInfo.day>
  {
  }

  /// <exclude />
  public abstract class dateInt : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DateInfo.dateInt>
  {
  }

  /// <exclude />
  public abstract class monthName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DateInfo.monthName>
  {
  }

  /// <exclude />
  public abstract class monthInCalendar : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DateInfo.monthInCalendar>
  {
  }

  /// <exclude />
  public abstract class quarterInCalendar : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DateInfo.quarterInCalendar>
  {
  }

  /// <exclude />
  public abstract class dayInWeek : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DateInfo.dayInWeek>
  {
  }

  /// <exclude />
  public abstract class dayOfWeekName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DateInfo.dayOfWeekName>
  {
  }

  /// <exclude />
  public abstract class weekEnding : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  DateInfo.weekEnding>
  {
  }
}
