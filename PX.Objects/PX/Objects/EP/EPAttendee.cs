// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPAttendee
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.EP;

/// <summary>Represents an attendee of the event.</summary>
/// <remarks>
/// This is a child entity for the <see cref="T:PX.Objects.CR.CRActivity" /> of the <b>Event</b> type
/// (<see cref="P:PX.Objects.CR.CRActivity.ClassID" /> is equal to <see cref="F:PX.Objects.CR.CRActivityClass.Event" />).
/// </remarks>
[DebuggerDisplay("EventNoteID = {EventNoteID}, ContactID = {ContactID}")]
[PXCacheName("Attendee")]
[Serializable]
public class EPAttendee : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The identifier of the parent <see cref="T:PX.Objects.CR.CRActivity" />.
  /// The field is included in <see cref="T:PX.Objects.EP.EPAttendee.FK.Activity" />.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.CRActivity.NoteID" /> field.
  /// </value>
  [PXDBGuid(false, IsKey = true)]
  [PXDBDefault(typeof (CRActivity.noteID))]
  public virtual Guid? EventNoteID { get; set; }

  /// <summary>The unique identifier of the attendee.</summary>
  [PXDBGuid(true, IsKey = true)]
  public virtual Guid? AttendeeID { get; set; }

  /// <summary>The email address of the attendee.</summary>
  [PXDBEmail]
  [PXUIField(DisplayName = "Email")]
  public virtual 
  #nullable disable
  string Email { get; set; }

  /// <summary>The comment of the event owner for the attendee.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Comment")]
  public virtual string Comment { get; set; }

  /// <summary>
  /// The identifier of the related <see cref="T:PX.Objects.CR.Contact" />.
  /// The field is included in <see cref="T:PX.Objects.EP.EPAttendee.FK.Contact" />.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// Can be <see langword="null" />.
  /// </value>
  /// <remarks>
  /// The related contact's type (<see cref="P:PX.Objects.CR.Contact.ContactType" />) can have one of the following values:
  /// <see cref="F:PX.Objects.CR.ContactTypesAttribute.Person" />,
  /// <see cref="F:PX.Objects.CR.ContactTypesAttribute.Lead" />,
  /// <see cref="F:PX.Objects.CR.ContactTypesAttribute.Employee" />.
  /// </remarks>
  [PXUIField(DisplayName = "Contact")]
  [ContactRaw(null, new System.Type[] {typeof (ContactTypesAttribute.person), typeof (ContactTypesAttribute.employee), typeof (ContactTypesAttribute.lead)}, null, null, new System.Type[] {typeof (PX.Objects.CR.Contact.displayName), typeof (PX.Objects.CR.Contact.contactType), typeof (PX.Objects.CR.Contact.fullName), typeof (PX.Objects.CR.Contact.salutation), typeof (PX.Objects.CR.Contact.eMail)}, null)]
  public virtual int? ContactID { get; set; }

  /// <summary>The invitation status of the attendee.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.EP.PXInvitationStatusAttribute" />.
  /// The default value is <see cref="F:PX.Objects.EP.PXInvitationStatusAttribute.NOTINVITED" />.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Invitation", Enabled = false)]
  [PXDefault(0)]
  [PXInvitationStatus]
  public virtual int? Invitation { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the attendee is optional for the event.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Optional")]
  public virtual bool? IsOptional { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that this attendee is a system attendee
  /// that corresponds to the event owner (<see cref="P:PX.Objects.CR.CRActivity.OwnerID" />).
  /// </summary>
  /// <remarks>
  /// It also means that <see cref="P:PX.Objects.EP.EPAttendee.ContactID" /> equals to <see cref="P:PX.Objects.CR.CRActivity.OwnerID" />
  /// and <see cref="P:PX.Objects.EP.EPAttendee.Invitation" /> always equals to <see cref="F:PX.Objects.EP.PXInvitationStatusAttribute.ACCEPTED" />.
  /// This attendee is exluded from all actions in <see cref="T:PX.Objects.EP.Graphs.EPEventMaint.Extensions.EPEventMaint_AttendeeExt" />,
  /// such as <see cref="F:PX.Objects.EP.Graphs.EPEventMaint.Extensions.EPEventMaint_AttendeeExt.SendInvitations" />.
  /// </remarks>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Owner", Visible = false)]
  public virtual bool? IsOwner { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  /// <summary>Primary Key.</summary>
  public class PK : PrimaryKeyOf<EPAttendee>.By<EPAttendee.eventNoteID, EPAttendee.attendeeID>
  {
    public static EPAttendee Find(
      PXGraph graph,
      Guid? eventNoteID,
      Guid? attendeeID,
      PKFindOptions options = 0)
    {
      return (EPAttendee) PrimaryKeyOf<EPAttendee>.By<EPAttendee.eventNoteID, EPAttendee.attendeeID>.FindBy(graph, (object) eventNoteID, (object) attendeeID, options);
    }
  }

  /// <summary>Foreign Keys.</summary>
  public static class FK
  {
    /// <summary>Event.</summary>
    public class Activity : 
      PrimaryKeyOf<CRActivity>.By<CRActivity.noteID>.ForeignKeyOf<EPAttendee>.By<EPAttendee.eventNoteID>
    {
    }

    /// <summary>Contact.</summary>
    public class Contact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<EPAttendee>.By<EPAttendee.contactID>
    {
    }
  }

  public abstract class eventNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPAttendee.eventNoteID>
  {
  }

  public abstract class attendeeID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPAttendee.attendeeID>
  {
  }

  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPAttendee.email>
  {
  }

  public abstract class comment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPAttendee.comment>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPAttendee.contactID>
  {
  }

  public abstract class invitation : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPAttendee.invitation>
  {
  }

  public abstract class isOptional : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPAttendee.isOptional>
  {
  }

  public abstract class isOwner : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPAttendee.isOwner>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPAttendee.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPAttendee.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPAttendee.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPAttendee.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPAttendee.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPAttendee.lastModifiedDateTime>
  {
  }
}
