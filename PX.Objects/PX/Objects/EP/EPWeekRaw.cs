// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPWeekRaw
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Globalization;

#nullable enable
namespace PX.Objects.EP;

[PXHidden]
[Serializable]
public class EPWeekRaw : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt(IsKey = true)]
  [PXUIField(Visible = false)]
  public virtual int? WeekID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Week")]
  public virtual 
  #nullable disable
  string FullNumber { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Year", Visible = false)]
  public virtual int? Year { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Number")]
  public virtual int? Number { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Start")]
  public virtual DateTime? StartDate { get; set; }

  [PXDate]
  [PXUIField]
  public virtual DateTime? EndDate { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsFullWeek { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    [PXDependsOnFields(new Type[] {typeof (EPWeekRaw.fullNumber), typeof (EPWeekRaw.startDate), typeof (EPWeekRaw.endDate)})] get
    {
      CultureInfo culture = LocaleInfo.GetCulture();
      return culture != null && !culture.DateTimeFormat.ShortDatePattern.StartsWith("M") ? $"{this.FullNumber} ({this.StartDate:dd/MM} - {this.EndDate:dd/MM})" : $"{this.FullNumber} ({this.StartDate:MM/dd} - {this.EndDate:MM/dd})";
    }
  }

  [PXString]
  [PXUIField(DisplayName = "Description", Visible = false)]
  public virtual string ShortDescription
  {
    [PXDependsOnFields(new Type[] {typeof (EPWeekRaw.fullNumber), typeof (EPWeekRaw.startDate), typeof (EPWeekRaw.endDate)})] get
    {
      CultureInfo culture = LocaleInfo.GetCulture();
      return culture != null && !culture.DateTimeFormat.ShortDatePattern.StartsWith("M") ? $"{this.FullNumber} ({this.StartDate:dd/MM} - {this.EndDate:dd/MM})" : $"{this.FullNumber} ({this.StartDate:MM/dd} - {this.EndDate:MM/dd})";
    }
  }

  public static EPWeekRaw ToEPWeekRaw(EPCustomWeek week)
  {
    return new EPWeekRaw()
    {
      EndDate = week.EndDate,
      StartDate = week.StartDate,
      WeekID = week.WeekID,
      Year = week.Year,
      Number = week.Number,
      FullNumber = week.FullNumber,
      IsFullWeek = week.IsFullWeek,
      IsActive = week.IsActive
    };
  }

  public static EPWeekRaw ToEPWeekRaw(PXWeekSelectorAttribute.EPWeek week)
  {
    return new EPWeekRaw()
    {
      EndDate = week.EndDate,
      StartDate = week.StartDate,
      WeekID = week.WeekID,
      Number = week.Number,
      FullNumber = week.FullNumber,
      IsFullWeek = new bool?(true),
      IsActive = new bool?(true)
    };
  }

  public abstract class weekID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPWeekRaw.weekID>
  {
  }

  public abstract class fullNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPWeekRaw.fullNumber>
  {
  }

  public abstract class year : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPWeekRaw.year>
  {
  }

  public abstract class number : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPWeekRaw.number>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  EPWeekRaw.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  EPWeekRaw.endDate>
  {
  }

  public abstract class isFullWeek : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPWeekRaw.isFullWeek>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPWeekRaw.isActive>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPWeekRaw.description>
  {
  }

  public abstract class shortDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPWeekRaw.shortDescription>
  {
  }
}
