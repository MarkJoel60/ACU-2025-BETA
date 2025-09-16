// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEventVCalendarProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Export.Imc;
using PX.Objects.CR;
using PX.Objects.EP.Imc;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.EP;

public class EPEventVCalendarProcessor : IVCalendarProcessor
{
  private PXSelectBase<CRActivity> _infoSelect;
  private PXSelectBase<EPAttendee> _attendees;
  private PXGraph _graph;

  protected PXGraph Graph => this._graph ?? (this._graph = new PXGraph());

  public virtual void Process(vEvent card, object item)
  {
    if (!(item is CRActivity row))
      return;
    EPEventVCalendarProcessor.FillCommon(card, row);
    this.FillOrganizer(card, row);
    this.FillAttendee(card, row);
  }

  private PXSelectBase<CRActivity> InfoSelect
  {
    get
    {
      if (this._infoSelect == null)
        this._infoSelect = (PXSelectBase<CRActivity>) new PXSelectJoin<CRActivity, LeftJoin<EPEmployee, On<EPEmployee.defContactID, Equal<CRActivity.ownerID>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<EPEmployee.defContactID>>, LeftJoin<Users, On<Users.pKID, Equal<EPEmployee.userID>>>>>, Where<CRActivity.noteID, Equal<Required<CRActivity.noteID>>>>(this.Graph);
      return this._infoSelect;
    }
  }

  private PXSelectBase<EPAttendee> Attendees
  {
    get
    {
      if (this._attendees == null)
        this._attendees = (PXSelectBase<EPAttendee>) new PXSelectJoin<EPAttendee, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<EPAttendee.contactID>>, LeftJoin<EPEmployee, On<EPEmployee.defContactID, Equal<PX.Objects.CR.Contact.contactID>>, LeftJoin<Users, On<Users.pKID, Equal<EPEmployee.userID>>>>>, Where<EPAttendee.eventNoteID, Equal<Required<EPAttendee.eventNoteID>>>>(this.Graph);
      return this._attendees;
    }
  }

  private void FillAttendee(vEvent card, CRActivity row)
  {
    ((PXSelectBase) this.Attendees).View.Clear();
    foreach (PXResult<EPAttendee, PX.Objects.CR.Contact, EPEmployee, Users> pxResult in this.Attendees.Select(new object[1]
    {
      (object) row.NoteID
    }))
    {
      EPAttendee epAttendee = (EPAttendee) ((PXResult) pxResult)[typeof (EPAttendee)];
      Users user = (Users) ((PXResult) pxResult)[typeof (Users)];
      PX.Objects.CR.Contact contact = (PX.Objects.CR.Contact) ((PXResult) pxResult)[typeof (PX.Objects.CR.Contact)];
      vEvent.Attendee.Statuses statuses = (vEvent.Attendee.Statuses) 0;
      string email;
      string fullName;
      if (contact == null)
      {
        email = epAttendee.Email;
        fullName = (string) null;
      }
      else
      {
        EPEventVCalendarProcessor.ExtractAttendeeInfo(user, contact, out fullName, out email);
        int? invitation = epAttendee.Invitation;
        if (invitation.HasValue)
        {
          switch (invitation.GetValueOrDefault())
          {
            case 2:
              statuses = (vEvent.Attendee.Statuses) 1;
              break;
            case 3:
              statuses = (vEvent.Attendee.Statuses) 2;
              break;
          }
        }
      }
      card.Attendees.Add(new vEvent.Attendee(fullName, email, statuses));
    }
  }

  private static void FillCommon(vEvent card, CRActivity row)
  {
    if (!row.StartDate.HasValue)
      throw new ArgumentNullException(nameof (row), "Start Date cannot be null");
    PXTimeZoneInfo timeZone = LocaleInfo.GetTimeZone();
    DateTime? nullable = row.StartDate;
    DateTime utc1 = PXTimeZoneInfo.ConvertTimeToUtc(nullable.Value, timeZone);
    card.Summary = row.Subject;
    card.IsHtml = true;
    card.Description = row.Body?.Replace("\r\n", "\\n");
    card.StartDate = utc1;
    vEvent vEvent1 = card;
    nullable = row.EndDate;
    DateTime dateTime;
    if (!nullable.HasValue)
    {
      dateTime = utc1;
    }
    else
    {
      nullable = row.EndDate;
      dateTime = PXTimeZoneInfo.ConvertTimeToUtc(nullable.Value, timeZone);
    }
    vEvent1.EndDate = dateTime;
    card.Location = row.Location;
    card.IsPrivate = row.IsPrivate.GetValueOrDefault();
    card.UID = "ACUMATICA_" + row.NoteID.ToString();
    vEvent vEvent2 = card;
    nullable = row.CreatedDateTime;
    DateTime utc2 = PXTimeZoneInfo.ConvertTimeToUtc(nullable.Value, timeZone);
    vEvent2.CreateDate = utc2;
  }

  private void FillOrganizer(vEvent card, CRActivity row)
  {
    ((PXSelectBase) this.InfoSelect).View.Clear();
    PXResultset<CRActivity> pxResultset = this.InfoSelect.Select(new object[1]
    {
      (object) row.NoteID
    });
    if (pxResultset == null || pxResultset.Count == 0)
      return;
    string fullName;
    string email;
    EPEventVCalendarProcessor.ExtractAttendeeInfo((Users) ((PXResult) pxResultset[0])[typeof (Users)], (PX.Objects.CR.Contact) ((PXResult) pxResultset[0])[typeof (PX.Objects.CR.Contact)], out fullName, out email);
    card.OrganizerName = fullName;
    card.OrganizerEmail = email;
    if (string.IsNullOrEmpty(fullName))
      return;
    card.Attendees.Add(new vEvent.Attendee(fullName, email, (vEvent.Attendee.Statuses) 1, (vEvent.Attendee.Rules) 0));
  }

  private static void ExtractAttendeeInfo(
    Users user,
    PX.Objects.CR.Contact contact,
    out string fullName,
    out string email)
  {
    fullName = user.FullName;
    email = user.Email;
    if (contact.DisplayName != null && contact.DisplayName.Trim().Length > 0)
      fullName = contact.DisplayName;
    if (contact.EMail == null || contact.EMail.Trim().Length <= 0)
      return;
    email = contact.EMail;
  }
}
