// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRReminder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM;
using PX.TM;
using PX.Web.UI;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <summary>
/// The reminder for the <b>task</b> or <b>event</b> (<see cref="T:PX.Objects.CR.CRActivity" />).
/// </summary>
[PXCacheName("Reminder")]
[Serializable]
public class CRReminder : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the reminder is enabled.
  /// </summary>
  /// <value>The value of this field is calculated by formula.</value>
  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<CRReminder.reminderDate, IsNotNull>, True>, False>))]
  [PXUIField(DisplayName = "Reminder")]
  public virtual bool? IsReminderOn { get; set; }

  /// <summary>The alias for the icon used by the reminder.</summary>
  /// <value>
  /// The value is a full path to the icon which is calculated by formula.
  /// This value is used by the related <see cref="T:PX.Objects.CR.CRActivity" /> to display the reminder icon in generic inquiries.
  /// </value>
  [PXUIField(DisplayName = "Reminder Icon", IsReadOnly = true)]
  [PXImage(HeaderImage = "control@ReminderHead")]
  [PXFormula(typeof (Switch<Case<Where<CRReminder.reminderDate, IsNotNull>, CRReminder.reminderIcon.reminder>>))]
  public virtual 
  #nullable disable
  string ReminderIcon { get; set; }

  /// <summary>The unique identifier of the reminder.</summary>
  [PXSequentialNote(SuppressActivitiesCount = true, IsKey = true)]
  [PXTimeTag(typeof (CRReminder.noteID))]
  public virtual Guid? NoteID { get; set; }

  /// <summary>
  /// The identifier of the related <see cref="T:PX.Objects.CR.CRActivity" />.
  /// This field is included in <see cref="T:PX.Objects.CR.CRReminder.FK.Activity" />.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.CRActivity.NoteID" /> field.
  /// </value>
  [PXDBGuid(false)]
  [PXDBDefault(null)]
  public virtual Guid? RefNoteID { get; set; }

  /// <summary>The date and time of the reminder.</summary>
  /// <value>
  /// The value of this field is stored in the Coordinated Universal Time (UTC)
  /// time zone and shown in the UI in the user's time zone.
  /// </value>
  [PXDBDateAndTime(InputMask = "g", PreserveTime = true, UseTimeZone = true, WithoutDisplayNames = true)]
  [PXUIField(DisplayName = "Remind At", Visible = false, Enabled = true)]
  public virtual DateTime? ReminderDate { get; set; }

  /// <summary>
  /// The value that shows the relative time before the start of the reminder.
  /// Allows to select <see cref="P:PX.Objects.CR.CRReminder.ReminderDate" /> relatively to <see cref="P:PX.Objects.CR.CRActivity.StartDate" />.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in the <see cref="T:PX.Objects.CR.RemindAtListAttribute" />.
  /// </value>
  [PXDBString]
  [PXUIField(DisplayName = "Remind At", Visible = false)]
  [RemindAtList]
  [PXUIVisible(typeof (Where<CRReminder.isReminderOn, Equal<True>>))]
  public virtual string RemindAt { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Contact">owner</see> of the reminder.
  /// The field is included in <see cref="T:PX.Objects.CR.CRReminder.FK.Owner" />.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [PXChildUpdatable(AutoRefresh = true)]
  [SubordinateOwner]
  public virtual int? Owner { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the reminder is dismissed.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Dismiss { get; set; }

  [PXDBCreatedByID(DontOverrideValue = true)]
  [PXUIField(Enabled = false)]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Created At", Enabled = false)]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<CRReminder>.By<CRReminder.noteID>
  {
    public static CRReminder Find(PXGraph graph, Guid? noteID, PKFindOptions options = 0)
    {
      return (CRReminder) PrimaryKeyOf<CRReminder>.By<CRReminder.noteID>.FindBy(graph, (object) noteID, options);
    }
  }

  /// <summary>Foreign Keys.</summary>
  public static class FK
  {
    /// <summary>Owner of the reminder.</summary>
    public class Owner : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CRReminder>.By<CRReminder.owner>
    {
    }

    /// <summary>Event or task related to the reminder.</summary>
    public class Activity : 
      PrimaryKeyOf<CRActivity>.By<CRActivity.noteID>.ForeignKeyOf<CRReminder>.By<CRReminder.refNoteID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRReminder.selected>
  {
  }

  public abstract class isReminderOn : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRReminder.isReminderOn>
  {
  }

  public abstract class reminderIcon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRReminder.reminderIcon>
  {
    public class reminder : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRReminder.reminderIcon.reminder>
    {
      public reminder()
        : base(Sprite.Control.GetFullUrl("Reminder"))
      {
      }
    }
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRReminder.noteID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRReminder.refNoteID>
  {
  }

  public abstract class reminderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRReminder.reminderDate>
  {
  }

  public abstract class remindAt : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRReminder.remindAt>
  {
  }

  public abstract class owner : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRReminder.owner>
  {
  }

  public abstract class dismiss : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRReminder.dismiss>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRReminder.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRReminder.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRReminder.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRReminder.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRReminder.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRReminder.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRReminder.Tstamp>
  {
  }
}
