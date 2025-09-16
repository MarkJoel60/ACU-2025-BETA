// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPCalendarSync
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.EP.Imc;
using PX.SM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.EP;

public class EPCalendarSync : PXGraph<EPCalendarSync>
{
  [InjectDependency]
  protected internal IVCalendarFactory VCalendarFactory { get; private set; }

  public IEnumerable<CRActivity> GetCalendarEvents(Guid settingsId)
  {
    List<CRActivity> calendarEvents = new List<CRActivity>();
    ((PXGraph) this).Load();
    if (this.IsPublished(settingsId))
      calendarEvents.AddRange(this.GetEvents(settingsId));
    return (IEnumerable<CRActivity>) calendarEvents;
  }

  public bool IsPublished(Guid id)
  {
    PXResultset<SMCalendarSettings> pxResultset = PXSelectBase<SMCalendarSettings, PXSelect<SMCalendarSettings, Where<SMCalendarSettings.urlGuid, Equal<Required<SMCalendarSettings.urlGuid>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) id
    });
    return pxResultset != null && pxResultset.Count > 0 && PXResult<SMCalendarSettings>.op_Implicit(pxResultset[0]).IsPublic.Value;
  }

  public virtual IEnumerable<CRActivity> GetEvents(Guid id)
  {
    object[] objArray = new object[1]{ (object) id };
    foreach (PXResult<CRActivity> pxResult in PXSelectBase<CRActivity, PXSelectJoin<CRActivity, LeftJoin<EPAttendee, On<EPAttendee.eventNoteID, Equal<CRActivity.noteID>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<CRActivity.ownerID>>, LeftJoin<Contact2, On<Contact2.contactID, Equal<EPAttendee.contactID>>, InnerJoin<SMCalendarSettings, On<SMCalendarSettings.userID, Equal<CRActivity.createdByID>, Or<SMCalendarSettings.userID, Equal<PX.Objects.CR.Contact.userID>, Or<SMCalendarSettings.userID, Equal<Contact2.userID>>>>>>>>, Where2<Where<CRActivity.classID, Equal<CRActivityClass.events>>, And<SMCalendarSettings.urlGuid, Equal<Required<SMCalendarSettings.urlGuid>>, And<SMCalendarSettings.isPublic, Equal<True>>>>, OrderBy<Desc<CRActivity.priority, Asc<CRActivity.startDate, Asc<CRActivity.endDate>>>>>.Config>.Select((PXGraph) this, objArray))
      yield return PXResult<CRActivity>.op_Implicit(pxResult);
  }
}
