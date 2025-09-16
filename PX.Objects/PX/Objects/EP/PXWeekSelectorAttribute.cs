// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.PXWeekSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.SQLTree;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

#nullable enable
namespace PX.Objects.EP;

/// <summary>
/// Allow select weeks.
/// Shows start and end date of week.
/// </summary>
/// <example>[PXWeekSelector]</example>
[Serializable]
public class PXWeekSelectorAttribute : PXCustomSelectorAttribute, IPXFieldDefaultingSubscriber
{
  private readonly 
  #nullable disable
  Type _startDateBqlField;
  private readonly Type _timeSpentBqlField;
  private readonly bool _limited;
  private int _startDateOrdinal;
  private int _timeSpentOrdinal;

  public PXWeekSelectorAttribute()
    : base(typeof (PXWeekSelectorAttribute.EPWeek.weekID), new Type[3]
    {
      typeof (PXWeekSelectorAttribute.EPWeek.number),
      typeof (PXWeekSelectorAttribute.EPWeek.startDate),
      typeof (PXWeekSelectorAttribute.EPWeek.endDate)
    })
  {
  }

  public PXWeekSelectorAttribute(Type type, Type[] fieldList)
    : base(type, fieldList)
  {
  }

  public PXWeekSelectorAttribute(Type startDateBqlField, Type timeSpentBqlField)
    : this()
  {
    this._startDateBqlField = startDateBqlField;
    this._timeSpentBqlField = timeSpentBqlField;
    this._limited = true;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXSelectorAttribute) this).EmitColumnForDescriptionField(sender);
    base.CacheAttached(sender);
    if (!this._limited)
      return;
    this._startDateOrdinal = sender.GetFieldOrdinal(sender.GetField(this._startDateBqlField));
    this._timeSpentOrdinal = sender.GetFieldOrdinal(sender.GetField(this._timeSpentBqlField));
  }

  public virtual void DescriptionFieldCommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 3) != null || (e.Operation & 124) != 16 /*0x10*/ || e.Value != null && !(e.Value is string))
      return;
    ((CancelEventArgs) e).Cancel = true;
    e.Expr = SQLExpression.Null();
    e.DataLength = new int?();
    e.DataType = (PXDbType) 100;
    e.DataValue = (object) null;
  }

  protected IEnumerable GetRecords()
  {
    if (!this._limited)
      return this.GetAllRecords();
    PXCache cach = this._Graph.Caches[((PXSelectorAttribute) this)._CacheType];
    return PXWeekSelectorAttribute.GetRecordsByDate((DateTime?) cach.GetValue(cach.Current, this._startDateOrdinal));
  }

  protected virtual IEnumerable GetAllRecords()
  {
    // ISSUE: unable to decompile the method.
  }

  public static IEnumerable GetRecordsByDate(DateTime? startDate)
  {
    if (startDate.HasValue)
    {
      DateTime date = startDate.Value;
      int dateWeek = PXDateTimeInfo.GetWeekNumber(date);
      DateTime utcDate = PXTimeZoneInfo.ConvertTimeToUtc(date, LocaleInfo.GetTimeZone());
      int utcDateWeek = PXDateTimeInfo.GetWeekNumber(utcDate);
      if (dateWeek != utcDateWeek)
      {
        if (date > utcDate)
        {
          yield return (object) new PXWeekSelectorAttribute.EPWeek()
          {
            WeekID = new int?(utcDate.Year * 100 + utcDateWeek)
          };
          yield return (object) new PXWeekSelectorAttribute.EPWeek()
          {
            WeekID = new int?(date.Year * 100 + dateWeek)
          };
        }
        else
        {
          yield return (object) new PXWeekSelectorAttribute.EPWeek()
          {
            WeekID = new int?(date.Year * 100 + dateWeek)
          };
          yield return (object) new PXWeekSelectorAttribute.EPWeek()
          {
            WeekID = new int?(utcDate.Year * 100 + utcDateWeek)
          };
        }
      }
      else
        yield return (object) new PXWeekSelectorAttribute.EPWeek()
        {
          WeekID = new int?(date.Year * 100 + dateWeek)
        };
    }
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!this._limited)
      return;
    object current = sender.Current;
    sender.Current = e.Row;
    base.FieldVerifying(sender, e);
    sender.Current = current;
  }

  public void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (!this._limited)
      return;
    object obj = sender.GetValue(e.Row, this._startDateOrdinal);
    if (obj == null)
      return;
    DateTime date = (DateTime) obj;
    e.NewValue = (object) PXWeekSelectorAttribute.GetWeekID(date);
  }

  public static int GetWeekID(DateTime date)
  {
    int year = date.Year;
    int weekNumber = PXDateTimeInfo.GetWeekNumber(date.Date);
    if (weekNumber >= 52 && date.Month == 1)
      --year;
    if (weekNumber == 1 && date.Month == 12)
      ++year;
    return year * 100 + weekNumber;
  }

  public static DateTime GetWeekStartDate(int weekId)
  {
    return PXDateTimeInfo.GetWeekStart(weekId / 100, weekId % 100);
  }

  [PXVirtual]
  [PXHidden]
  [Serializable]
  public class EPWeek : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    private string _fullNumber;
    private int? _number;
    private DateTime? _startDate;
    private DateTime? _endDate;

    [PXDBInt(IsKey = true)]
    [PXUIField(Visible = false)]
    public virtual int? WeekID { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Week")]
    public virtual string FullNumber
    {
      get
      {
        this.Initialize();
        return this._fullNumber;
      }
    }

    [PXInt]
    [PXUIField(DisplayName = "Number", Visible = false)]
    public virtual int? Number
    {
      get
      {
        this.Initialize();
        return this._number;
      }
    }

    [PXDate]
    [PXUIField(DisplayName = "Start")]
    public virtual DateTime? StartDate
    {
      get
      {
        this.Initialize();
        return this._startDate;
      }
    }

    [PXDate]
    [PXUIField]
    public virtual DateTime? EndDate
    {
      get
      {
        this.Initialize();
        return this._endDate;
      }
    }

    [PXString]
    [PXUIField(DisplayName = "Description")]
    public virtual string Description
    {
      [PXDependsOnFields(new Type[] {typeof (PXWeekSelectorAttribute.EPWeek.fullNumber), typeof (PXWeekSelectorAttribute.EPWeek.startDate), typeof (PXWeekSelectorAttribute.EPWeek.endDate)})] get
      {
        CultureInfo culture = LocaleInfo.GetCulture();
        return culture != null && !culture.DateTimeFormat.ShortDatePattern.StartsWith("M") ? $"{this.FullNumber} ({this.StartDate:dd/MM} - {this.EndDate:dd/MM})" : $"{this.FullNumber} ({this.StartDate:MM/dd} - {this.EndDate:MM/dd})";
      }
    }

    [PXString]
    [PXUIField(DisplayName = "Description", Visible = false)]
    public virtual string ShortDescription
    {
      [PXDependsOnFields(new Type[] {typeof (PXWeekSelectorAttribute.EPWeek.fullNumber), typeof (PXWeekSelectorAttribute.EPWeek.startDate), typeof (PXWeekSelectorAttribute.EPWeek.endDate)})] get
      {
        CultureInfo culture = LocaleInfo.GetCulture();
        return culture != null && !culture.DateTimeFormat.ShortDatePattern.StartsWith("M") ? $"{this.FullNumber} ({this.StartDate:dd/MM} - {this.EndDate:dd/MM})" : $"{this.FullNumber} ({this.StartDate:MM/dd} - {this.EndDate:MM/dd})";
      }
    }

    private void Initialize()
    {
      if (this._number.HasValue && this._fullNumber != null && this._startDate.HasValue && this._endDate.HasValue)
        return;
      int? weekId = this.WeekID;
      if (!weekId.HasValue)
        return;
      weekId = this.WeekID;
      int num = weekId.Value;
      int year = num / 100;
      int weekNumber = num % 100;
      this._number = new int?(weekNumber);
      this._fullNumber = $"{year}-{weekNumber:00}";
      DateTime weekStart = PXDateTimeInfo.GetWeekStart(year, weekNumber);
      this._startDate = new DateTime?(weekStart);
      this._endDate = new DateTime?(weekStart.AddDays(7.0).AddTicks(-1L));
    }

    public abstract class weekID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PXWeekSelectorAttribute.EPWeek.weekID>
    {
    }

    public abstract class fullNumber : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXWeekSelectorAttribute.EPWeek.fullNumber>
    {
    }

    public abstract class number : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PXWeekSelectorAttribute.EPWeek.number>
    {
    }

    public abstract class startDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      PXWeekSelectorAttribute.EPWeek.startDate>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      PXWeekSelectorAttribute.EPWeek.endDate>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXWeekSelectorAttribute.EPWeek.description>
    {
    }

    public abstract class shortDescription : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXWeekSelectorAttribute.EPWeek.shortDescription>
    {
    }
  }
}
