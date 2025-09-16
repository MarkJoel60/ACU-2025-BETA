// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.PRWeekDaySelector
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.EP;

public class PRWeekDaySelector : PXCustomSelectorAttribute
{
  protected 
  #nullable disable
  Type WeekField;

  public PRWeekDaySelector(Type weekField)
    : base(typeof (PRWeekDaySelector.WeekDate.date))
  {
    ((PXSelectorAttribute) this).DescriptionField = typeof (PRWeekDaySelector.WeekDate.day);
    this.WeekField = weekField;
    ((PXSelectorAttribute) this).ValidateValue = false;
  }

  public IEnumerable GetRecords()
  {
    return (IEnumerable) PRWeekDaySelector.GetWeekDates(this._Graph, (int) PRWeekDaySelector.GetValue(this._Graph, this._Graph.Caches[BqlCommand.GetItemType(this.WeekField)].Current, this.WeekField));
  }

  public static IEnumerable<PRWeekDaySelector.WeekDate> GetWeekDates(PXGraph graph, int weekID)
  {
    DateTime startDate = PXWeekSelector2Attribute.IsCustomWeek(graph) ? PXWeekSelector2Attribute.GetWeekStartDate(graph, weekID) : PXWeekSelectorAttribute.GetWeekStartDate(weekID);
    for (DateTime date = startDate; date < startDate.AddDays(7.0); date = date.AddDays(1.0))
      yield return new PRWeekDaySelector.WeekDate()
      {
        Date = new DateTime?(date),
        Day = PXLocalizer.Localize(date.DayOfWeek.ToString())
      };
  }

  private static object GetValue(PXGraph graph, object row, Type field)
  {
    return graph.Caches[BqlCommand.GetItemType(field)].GetValue(row, field.Name);
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2022 R2.")]
  public static IEnumerable<PRWeekDaySelector.WeekDate> GetWeekDates(int weekID)
  {
    DateTime startDate = PXWeekSelectorAttribute.GetWeekStartDate(weekID);
    for (DateTime date = startDate; date < startDate.AddDays(7.0); date = date.AddDays(1.0))
      yield return new PRWeekDaySelector.WeekDate()
      {
        Date = new DateTime?(date),
        Day = PXLocalizer.Localize(date.DayOfWeek.ToString())
      };
  }

  [PXHidden]
  public class WeekDate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDate]
    public virtual DateTime? Date { get; set; }

    [PXString]
    public virtual string Day { get; set; }

    public abstract class date : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      PRWeekDaySelector.WeekDate.date>
    {
    }

    public abstract class day : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PRWeekDaySelector.WeekDate.day>
    {
    }
  }
}
