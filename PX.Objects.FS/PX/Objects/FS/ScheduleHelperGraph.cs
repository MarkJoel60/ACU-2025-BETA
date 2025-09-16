// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ScheduleHelperGraph
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using System.Text;

#nullable disable
namespace PX.Objects.FS;

public class ScheduleHelperGraph : PXGraph<ScheduleHelperGraph>
{
  public static ScheduleHelperGraph SingleScheduleHelperGraph
  {
    get
    {
      return PXContext.GetSlot<ScheduleHelperGraph>() ?? PXContext.SetSlot<ScheduleHelperGraph>(PXGraph.CreateInstance<ScheduleHelperGraph>());
    }
  }

  public static string GetRecurrenceDescription(FSSchedule fsScheduleRow)
  {
    return ScheduleHelperGraph.SingleScheduleHelperGraph.GetRecurrenceDescriptionInt(fsScheduleRow);
  }

  public virtual string GetDaysOnWeeklyFrequency(FSSchedule fsScheduleRow)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (fsScheduleRow.FrequencyType == "W")
    {
      bool? nullable = fsScheduleRow.WeeklyOnSun;
      if (nullable.GetValueOrDefault())
        stringBuilder.Append(PXMessages.LocalizeNoPrefix("Sunday"));
      nullable = fsScheduleRow.WeeklyOnMon;
      if (nullable.GetValueOrDefault())
        stringBuilder.Append((stringBuilder.Length > 0 ? ", " : "") + PXMessages.LocalizeNoPrefix("Monday"));
      nullable = fsScheduleRow.WeeklyOnTue;
      if (nullable.GetValueOrDefault())
        stringBuilder.Append((stringBuilder.Length > 0 ? ", " : "") + PXMessages.LocalizeNoPrefix("Tuesday"));
      nullable = fsScheduleRow.WeeklyOnWed;
      if (nullable.GetValueOrDefault())
        stringBuilder.Append((stringBuilder.Length > 0 ? ", " : "") + PXMessages.LocalizeNoPrefix("Wednesday"));
      nullable = fsScheduleRow.WeeklyOnThu;
      if (nullable.GetValueOrDefault())
        stringBuilder.Append((stringBuilder.Length > 0 ? ", " : "") + PXMessages.LocalizeNoPrefix("Thursday"));
      nullable = fsScheduleRow.WeeklyOnFri;
      if (nullable.GetValueOrDefault())
        stringBuilder.Append((stringBuilder.Length > 0 ? ", " : "") + PXMessages.LocalizeNoPrefix("Friday"));
      nullable = fsScheduleRow.WeeklyOnSat;
      if (nullable.GetValueOrDefault())
        stringBuilder.Append((stringBuilder.Length > 0 ? ", " : "") + PXMessages.LocalizeNoPrefix("Saturday"));
    }
    return stringBuilder.ToString();
  }

  public virtual string GetMonthsOnAnnualFrequency(FSSchedule fsScheduleRow)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (fsScheduleRow.FrequencyType == "A")
    {
      bool? nullable = fsScheduleRow.AnnualOnJan;
      if (nullable.GetValueOrDefault())
        stringBuilder.Append(PXMessages.LocalizeNoPrefix("January"));
      nullable = fsScheduleRow.AnnualOnFeb;
      if (nullable.GetValueOrDefault())
        stringBuilder.Append((stringBuilder.Length > 0 ? ", " : "") + PXMessages.LocalizeNoPrefix("February"));
      nullable = fsScheduleRow.AnnualOnMar;
      if (nullable.GetValueOrDefault())
        stringBuilder.Append((stringBuilder.Length > 0 ? ", " : "") + PXMessages.LocalizeNoPrefix("March"));
      nullable = fsScheduleRow.AnnualOnApr;
      if (nullable.GetValueOrDefault())
        stringBuilder.Append((stringBuilder.Length > 0 ? ", " : "") + PXMessages.LocalizeNoPrefix("April"));
      nullable = fsScheduleRow.AnnualOnMay;
      if (nullable.GetValueOrDefault())
        stringBuilder.Append((stringBuilder.Length > 0 ? ", " : "") + PXMessages.LocalizeNoPrefix("May"));
      nullable = fsScheduleRow.AnnualOnJun;
      if (nullable.GetValueOrDefault())
        stringBuilder.Append((stringBuilder.Length > 0 ? ", " : "") + PXMessages.LocalizeNoPrefix("June"));
      nullable = fsScheduleRow.AnnualOnJul;
      if (nullable.GetValueOrDefault())
        stringBuilder.Append((stringBuilder.Length > 0 ? ", " : "") + PXMessages.LocalizeNoPrefix("July"));
      nullable = fsScheduleRow.AnnualOnAug;
      if (nullable.GetValueOrDefault())
        stringBuilder.Append((stringBuilder.Length > 0 ? ", " : "") + PXMessages.LocalizeNoPrefix("August"));
      nullable = fsScheduleRow.AnnualOnSep;
      if (nullable.GetValueOrDefault())
        stringBuilder.Append((stringBuilder.Length > 0 ? ", " : "") + PXMessages.LocalizeNoPrefix("September"));
      nullable = fsScheduleRow.AnnualOnOct;
      if (nullable.GetValueOrDefault())
        stringBuilder.Append((stringBuilder.Length > 0 ? ", " : "") + PXMessages.LocalizeNoPrefix("October"));
      nullable = fsScheduleRow.AnnualOnNov;
      if (nullable.GetValueOrDefault())
        stringBuilder.Append((stringBuilder.Length > 0 ? ", " : "") + PXMessages.LocalizeNoPrefix("November"));
      nullable = fsScheduleRow.AnnualOnDec;
      if (nullable.GetValueOrDefault())
        stringBuilder.Append((stringBuilder.Length > 0 ? ", " : "") + PXMessages.LocalizeNoPrefix("December"));
    }
    return stringBuilder.ToString();
  }

  public virtual string GetRecurrenceDescriptionInt(FSSchedule fsScheduleRow)
  {
    string empty = string.Empty;
    string str1;
    object[] objArray1;
    switch (fsScheduleRow.FrequencyType)
    {
      case "D":
        str1 = "Occurs every {0} {1}.";
        object[] objArray2 = new object[2];
        short? dailyFrequency = fsScheduleRow.DailyFrequency;
        ref short? local1 = ref dailyFrequency;
        objArray2[0] = (object) (local1.HasValue ? local1.GetValueOrDefault().ToString() : (string) null);
        objArray2[1] = (object) "day(s)";
        objArray1 = objArray2;
        break;
      case "W":
        str1 = "Occurs every {0} {1} on {2}.";
        string onWeeklyFrequency = this.GetDaysOnWeeklyFrequency(fsScheduleRow);
        object[] objArray3 = new object[3];
        short? weeklyFrequency = fsScheduleRow.WeeklyFrequency;
        ref short? local2 = ref weeklyFrequency;
        objArray3[0] = (object) (local2.HasValue ? local2.GetValueOrDefault().ToString() : (string) null);
        objArray3[1] = (object) "week(s)";
        objArray3[2] = (object) onWeeklyFrequency;
        objArray1 = objArray3;
        break;
      case "M":
        int num1 = fsScheduleRow.MonthlyRecurrenceType1 == "D" ? 1 : 0;
        short? monthlyFrequency = fsScheduleRow.MonthlyFrequency;
        ref short? local3 = ref monthlyFrequency;
        string str2 = local3.HasValue ? local3.GetValueOrDefault().ToString() : (string) null;
        string str3 = "month(s)";
        string str4 = num1 != 0 ? TX.RecurrencyFrecuency.dayOfMonthOrdinals[(int) fsScheduleRow.MonthlyOnDay1.Value - 1] : TX.RecurrencyFrecuency.weeksOfMonth[(int) fsScheduleRow.MonthlyOnWeek1.Value - 1];
        string str5 = num1 != 0 ? "day" : TX.RecurrencyFrecuency.daysOfWeek[(int) fsScheduleRow.MonthlyOnDayOfWeek1.Value];
        if (fsScheduleRow.Monthly2Selected.GetValueOrDefault())
        {
          int num2 = fsScheduleRow.MonthlyRecurrenceType2 == "D" ? 1 : 0;
          string str6 = num2 != 0 ? TX.RecurrencyFrecuency.dayOfMonthOrdinals[(int) fsScheduleRow.MonthlyOnDay2.Value - 1] : TX.RecurrencyFrecuency.weeksOfMonth[(int) fsScheduleRow.MonthlyOnWeek2.Value - 1];
          string str7 = num2 != 0 ? "day" : TX.RecurrencyFrecuency.daysOfWeek[(int) fsScheduleRow.MonthlyOnDayOfWeek2.Value];
          if (fsScheduleRow.Monthly3Selected.GetValueOrDefault())
          {
            int num3 = fsScheduleRow.MonthlyRecurrenceType3 == "D" ? 1 : 0;
            string str8 = num3 != 0 ? TX.RecurrencyFrecuency.dayOfMonthOrdinals[(int) fsScheduleRow.MonthlyOnDay3.Value - 1] : TX.RecurrencyFrecuency.weeksOfMonth[(int) fsScheduleRow.MonthlyOnWeek3.Value - 1];
            string str9 = num3 != 0 ? "day" : TX.RecurrencyFrecuency.daysOfWeek[(int) fsScheduleRow.MonthlyOnDayOfWeek3.Value];
            if (fsScheduleRow.Monthly4Selected.GetValueOrDefault())
            {
              int num4 = fsScheduleRow.MonthlyRecurrenceType4 == "D" ? 1 : 0;
              string str10 = num4 != 0 ? TX.RecurrencyFrecuency.dayOfMonthOrdinals[(int) fsScheduleRow.MonthlyOnDay4.Value - 1] : TX.RecurrencyFrecuency.weeksOfMonth[(int) fsScheduleRow.MonthlyOnWeek4.Value - 1];
              string str11 = num4 != 0 ? "day" : TX.RecurrencyFrecuency.daysOfWeek[(int) fsScheduleRow.MonthlyOnDayOfWeek4.Value];
              str1 = "Occurs every {0} {1} on the {2} {3} and on the {4} {5} and on the {6} {7} and on the {8} {9} of that month.";
              objArray1 = new object[10]
              {
                (object) str2,
                (object) str3,
                (object) str4,
                (object) str5,
                (object) str6,
                (object) str7,
                (object) str8,
                (object) str9,
                (object) str10,
                (object) str11
              };
              break;
            }
            str1 = "Occurs every {0} {1} on the {2} {3} and on the {4} {5} and on the {6} {7} of that month.";
            objArray1 = new object[8]
            {
              (object) str2,
              (object) str3,
              (object) str4,
              (object) str5,
              (object) str6,
              (object) str7,
              (object) str8,
              (object) str9
            };
            break;
          }
          str1 = "Occurs every {0} {1} on the {2} {3} and on the {4} {5} of that month.";
          objArray1 = new object[6]
          {
            (object) str2,
            (object) str3,
            (object) str4,
            (object) str5,
            (object) str6,
            (object) str7
          };
          break;
        }
        str1 = "Occurs every {0} {1} on the {2} {3} of that month.";
        objArray1 = new object[4]
        {
          (object) str2,
          (object) str3,
          (object) str4,
          (object) str5
        };
        break;
      case "A":
        short? nullable = fsScheduleRow.AnnualFrequency;
        ref short? local4 = ref nullable;
        string str12 = local4.HasValue ? local4.GetValueOrDefault().ToString() : (string) null;
        string str13 = "year(s)";
        if (fsScheduleRow.AnnualRecurrenceType == "W")
        {
          str1 = "Occurs every {0} {1} on the {2} {3} of {4}.";
          string[] weeksOfMonth = TX.RecurrencyFrecuency.weeksOfMonth;
          nullable = fsScheduleRow.AnnualOnWeek;
          int index1 = (int) nullable.Value - 1;
          string str14 = weeksOfMonth[index1];
          string[] daysOfWeek = TX.RecurrencyFrecuency.daysOfWeek;
          nullable = fsScheduleRow.AnnualOnDayOfWeek;
          int index2 = (int) nullable.Value;
          string str15 = daysOfWeek[index2];
          string onAnnualFrequency = this.GetMonthsOnAnnualFrequency(fsScheduleRow);
          objArray1 = new object[5]
          {
            (object) str12,
            (object) str13,
            (object) str14,
            (object) str15,
            (object) onAnnualFrequency
          };
          break;
        }
        str1 = "Occurs every {0} {1} on {2}, {3}.";
        string onAnnualFrequency1 = this.GetMonthsOnAnnualFrequency(fsScheduleRow);
        string[] dayOfMonthOrdinals = TX.RecurrencyFrecuency.dayOfMonthOrdinals;
        nullable = fsScheduleRow.AnnualOnDay;
        int index = (int) nullable.Value - 1;
        string str16 = dayOfMonthOrdinals[index];
        objArray1 = new object[4]
        {
          (object) str12,
          (object) str13,
          (object) onAnnualFrequency1,
          (object) str16
        };
        break;
      default:
        throw new PXSetPropertyException("The operation is not supported.");
    }
    return PXMessages.LocalizeFormatNoPrefix(str1, objArray1);
  }
}
