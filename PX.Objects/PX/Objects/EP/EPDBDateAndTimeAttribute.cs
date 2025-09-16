// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPDBDateAndTimeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.EP;

/// <summary>
/// Time is displayed and modified in the timezone of the user/Employee.
/// </summary>
public class EPDBDateAndTimeAttribute : PXDBDateAndTimeAttribute
{
  protected Type typeOwnerID;
  protected PXTimeZoneInfo timezone;

  public EPDBDateAndTimeAttribute(Type ownerID) => this.typeOwnerID = ownerID;

  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    this.InitTimeZone(sender, e.Row);
    ((PXDBFieldAttribute) this).CommandPreparing(sender, e);
  }

  public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    using (new PXConnectionScope())
      this.InitTimeZone(sender, e.Row);
    ((PXDBDateAttribute) this).RowSelecting(sender, e);
  }

  protected virtual void InitTimeZone(PXCache sender, object row)
  {
    if (row == null)
      return;
    int? nullable = (int?) sender.GetValue(row, sender.GetField(this.typeOwnerID));
    if (!nullable.HasValue)
      return;
    Guid? userId = PXAccess.GetUserID(nullable);
    if (!userId.HasValue)
      return;
    UserPreferences userPreferences = PXResultset<UserPreferences>.op_Implicit(PXSelectBase<UserPreferences, PXSelect<UserPreferences, Where<UserPreferences.userID, Equal<Required<UserPreferences.userID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) userId
    }));
    if (userPreferences != null && !string.IsNullOrEmpty(userPreferences.TimeZone))
    {
      this.timezone = PXTimeZoneInfo.FindSystemTimeZoneById(userPreferences.TimeZone);
    }
    else
    {
      EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.userID, Equal<Required<EPEmployee.userID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) userId
      }));
      if (epEmployee == null || epEmployee.CalendarID == null)
        return;
      CSCalendar csCalendar = PXResultset<CSCalendar>.op_Implicit(PXSelectBase<CSCalendar, PXSelect<CSCalendar, Where<CSCalendar.calendarID, Equal<Required<CSCalendar.calendarID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) epEmployee.CalendarID
      }));
      if (csCalendar == null || string.IsNullOrEmpty(csCalendar.TimeZone))
        return;
      this.timezone = PXTimeZoneInfo.FindSystemTimeZoneById(csCalendar.TimeZone);
    }
  }

  protected virtual PXTimeZoneInfo GetTimeZone()
  {
    return this.timezone != null ? this.timezone : ((PXDBDateAttribute) this).GetTimeZone();
  }
}
