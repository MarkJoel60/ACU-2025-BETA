// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.RemindAtListAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

public class RemindAtListAttribute : PXStringListAttribute, IPXRowSelectedSubscriber
{
  public const 
  #nullable disable
  string AtTheTimeOfEvent = "ATEV";
  public const string Before5minutes = "B05m";
  public const string Before15minutes = "B15m";
  public const string Before30minutes = "B30m";
  public const string Before1hour = "B01h";
  public const string Before2hours = "B02h";
  public const string Before1day = "B01d";
  public const string Before3days = "B03d";
  public const string Before1week = "B07d";
  public const string DateTimeFromExchange = "EXCH";
  protected string[] ValuesForMinutes = new string[9]
  {
    "ATEV",
    "B05m",
    "B15m",
    "B30m",
    "B01h",
    "B02h",
    "B01d",
    "B03d",
    "B07d"
  };
  protected string[] LabelsForMinutes = new string[9]
  {
    "At the Time of the Event",
    "5 Minutes Before",
    "15 Minutes Before",
    "30 Minutes Before",
    "1 Hour Before",
    "2 Hours Before",
    "1 Day Before",
    "3 Days Before",
    "1 Week Before"
  };
  protected string[] ValuesForDays = new string[4]
  {
    "ATEV",
    "B01d",
    "B03d",
    "B07d"
  };
  protected string[] LabelsForDays = new string[4]
  {
    "At the Time of the Event",
    "1 Day Before",
    "3 Days Before",
    "1 Week Before"
  };

  public System.Type IsAllDay { get; set; }

  public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (this.IsAllDay != (System.Type) null)
    {
      bool? nullable = new bool?();
      object obj = (object) null;
      BqlFormula.Verify(sender, e.Row, PXFormulaAttribute.InitFormula(BqlCommand.Compose(new System.Type[2]
      {
        typeof (Current<>),
        this.IsAllDay
      })), ref nullable, ref obj);
      if (((!(obj is bool flag) ? 0 : 1) & (flag ? 1 : 0)) != 0)
      {
        PXStringListAttribute.SetList(sender, e.Row, ((PXEventSubscriberAttribute) this)._FieldName, this.ValuesForDays, this.LabelsForDays);
        return;
      }
    }
    PXStringListAttribute.SetList(sender, e.Row, ((PXEventSubscriberAttribute) this)._FieldName, this.ValuesForMinutes, this.LabelsForMinutes);
  }

  public static TimeSpan GetRemindAtTimeSpan(string remindAt)
  {
    TimeSpan remindAtTimeSpan = new TimeSpan();
    if (remindAt != null && remindAt.Length == 4)
    {
      switch (remindAt[2])
      {
        case '0':
          if (remindAt == "B30m")
          {
            remindAtTimeSpan = new TimeSpan(0, -30, 0);
            break;
          }
          break;
        case '1':
          switch (remindAt)
          {
            case "B01h":
              remindAtTimeSpan = new TimeSpan(-1, 0, 0);
              break;
            case "B01d":
              remindAtTimeSpan = new TimeSpan(-1, 0, 0, 0);
              break;
          }
          break;
        case '2':
          if (remindAt == "B02h")
          {
            remindAtTimeSpan = new TimeSpan(-2, 0, 0);
            break;
          }
          break;
        case '3':
          if (remindAt == "B03d")
          {
            remindAtTimeSpan = new TimeSpan(-3, 0, 0, 0);
            break;
          }
          break;
        case '5':
          switch (remindAt)
          {
            case "B05m":
              remindAtTimeSpan = new TimeSpan(0, -5, 0);
              break;
            case "B15m":
              remindAtTimeSpan = new TimeSpan(0, -15, 0);
              break;
          }
          break;
        case '7':
          if (remindAt == "B07d")
          {
            remindAtTimeSpan = new TimeSpan(-7, 0, 0, 0);
            break;
          }
          break;
        case 'E':
          if (remindAt == "ATEV")
            break;
          break;
      }
    }
    return remindAtTimeSpan;
  }

  public class before15minutes : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    RemindAtListAttribute.before15minutes>
  {
    public before15minutes()
      : base("B15m")
    {
    }
  }

  public class before1day : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RemindAtListAttribute.before1day>
  {
    public before1day()
      : base("B01d")
    {
    }
  }
}
