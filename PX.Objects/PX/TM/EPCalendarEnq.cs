// Decompiled with JetBrains decompiler
// Type: PX.TM.EPCalendarEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.SM;
using System;
using System.Collections;

#nullable disable
namespace PX.TM;

[DashboardType(new int[] {7})]
public class EPCalendarEnq : PXGraph<EPCalendarEnq>
{
  private const int _30MINUTES = 30;
  public PXFilter<EPCalendarFilter> Filter;
  [PXViewDetailsButton(typeof (EPCalendarFilter))]
  public PXSelectJoin<CRActivity, LeftJoin<EPAttendee, On<EPAttendee.contactID, Equal<Current<AccessInfo.contactID>>, And<EPAttendee.eventNoteID, Equal<CRActivity.noteID>>>>, Where2<Where<CRActivity.classID, Equal<CRActivityClass.events>>, And<Where<CRActivity.startDate, GreaterEqual<Current<EPCalendarFilter.startDate>>, And<CRActivity.startDate, LessEqual<Current<EPCalendarFilter.endDate>>, And<Where<CRActivity.createdByID, Equal<Current<AccessInfo.userID>>, Or<EPAttendee.contactID, IsNotNull>>>>>>>, OrderBy<Asc<CRActivity.startDate>>> Events;
  public PXAction<EPCalendarFilter> NextList;
  public PXAction<EPCalendarFilter> PreviousList;
  public PXAction<EPCalendarFilter> ToDayList;
  public PXAction<EPCalendarFilter> createNew;

  [PXUIField(DisplayName = "Afterward")]
  [PXButton(ImageKey = "DataEntry")]
  public virtual IEnumerable nextList(PXAdapter adapter)
  {
    EPCalendarEnq.MoveDateList(((PXSelectBase<EPCalendarFilter>) this.Filter).Current, true);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Earlier")]
  [PXButton(ImageKey = "DataEntry")]
  public virtual IEnumerable previousList(PXAdapter adapter)
  {
    EPCalendarEnq.MoveDateList(((PXSelectBase<EPCalendarFilter>) this.Filter).Current, false);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Today")]
  [PXButton(ImageKey = "DataEntry")]
  public virtual IEnumerable toDayList(PXAdapter adapter)
  {
    ((PXSelectBase<EPCalendarFilter>) this.Filter).Current.CentralDate = new DateTime?(DateTime.Today);
    return adapter.Get();
  }

  [PXButton(ImageKey = "DataEntry")]
  public virtual IEnumerable CreateNew(PXAdapter adapter)
  {
    EPEventMaint instance = PXGraph.CreateInstance<EPEventMaint>();
    CRActivity crActivity = (CRActivity) ((PXSelectBase) instance.Events).Cache.Insert();
    crActivity.StartDate = ((PXSelectBase<EPCalendarFilter>) this.Filter).Current.CentralDate;
    if (crActivity.StartDate.HasValue)
      crActivity.EndDate = new DateTime?(crActivity.StartDate.Value.AddMinutes(30.0));
    throw new PXPopupRedirectException((PXGraph) instance, string.Empty, true);
  }

  private static void MoveDateList(EPCalendarFilter filter, bool forward)
  {
    switch (filter.Type)
    {
      case "Day":
        filter.CentralDate = EPCalendarEnq.AddDays(filter.CentralDate, 1, forward);
        break;
      case "Week":
        filter.CentralDate = EPCalendarEnq.AddDays(filter.CentralDate, 7, forward);
        break;
      case "Month":
        DateTime dateTime1 = filter.CentralDate.Value;
        DateTime dateTime2 = dateTime1.AddMonths(1);
        int diff = DateTime.DaysInMonth(dateTime1.Year, dateTime1.Month) - dateTime1.Day + DateTime.DaysInMonth(dateTime2.Year, dateTime2.Month) / 2;
        filter.CentralDate = EPCalendarEnq.AddDays(filter.CentralDate, diff, forward);
        break;
    }
  }

  private static DateTime? AddDays(DateTime? date, int diff, bool forward)
  {
    return date.HasValue ? new DateTime?(date.Value.AddDays((double) ((forward ? 1 : -1) * diff))) : date;
  }
}
