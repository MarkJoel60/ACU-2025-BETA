// Decompiled with JetBrains decompiler
// Type: PX.SM.AU.SMEmail
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.SM.Email;
using System;

#nullable enable
namespace PX.SM.AU;

[Serializable]
public class SMEmail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _ImcUID;

  [PXDBGuid(true, IsKey = true)]
  public virtual Guid? NoteID { get; set; }

  [PXSequentialSelfRefNote(SuppressActivitiesCount = true, NoteField = typeof (SMEmail.noteID))]
  [PXDBDefault(null, PersistingCheck = PXPersistingCheck.Nothing, DefaultForUpdate = false)]
  public virtual Guid? RefNoteID { get; set; }

  [PXDBString(998, InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Summary", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual 
  #nullable disable
  string Subject { get; set; }

  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Activity Details")]
  public virtual string Body { get; set; }

  [PXDBString(2, IsFixed = true, IsUnicode = false)]
  [MailStatusList]
  [PXUIField(DisplayName = "Email Status", Enabled = false)]
  public virtual string MPStatus { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsArchived { get; set; }

  [PXDBGuid(true)]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual Guid? ImcUID
  {
    get => this._ImcUID ?? (this._ImcUID = new Guid?(Guid.NewGuid()));
    set => this._ImcUID = value;
  }

  [PXDBString(150)]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual string Pop3UID { get; set; }

  [PXDBInt]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual int? ImapUID { get; set; }

  [PXDefault]
  [PXDBDateAndTime(UseTimeZone = true)]
  public System.DateTime? MailDate { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "From")]
  public virtual int? MailAccountID { get; set; }

  [SMDBRecipient(false)]
  [PXUIField(DisplayName = "From")]
  public virtual string MailFrom { get; set; }

  [SMDBRecipient(false)]
  [PXUIField(DisplayName = "Reply")]
  public virtual string MailReply { get; set; }

  [SMDBRecipient(true)]
  [PXUIField(DisplayName = "To")]
  public virtual string MailTo { get; set; }

  [SMDBRecipient(true)]
  [PXUIField(DisplayName = "CC")]
  public virtual string MailCc { get; set; }

  [SMDBRecipient(true)]
  [PXUIField(DisplayName = "BCC")]
  public virtual string MailBcc { get; set; }

  [PXDBInt]
  [PXUIField(Visible = false)]
  [PXDefault(0)]
  public virtual int? RetryCount { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(Visible = false)]
  public virtual string MessageId { get; set; }

  [PXDBString]
  [PXUIField(Visible = false)]
  public virtual string MessageReference { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Error Message")]
  public virtual string Exception { get; set; }

  [PXDefault("H", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Format")]
  [EmailFormatList]
  public virtual string Format { get; set; }

  [PXDBString(10)]
  [PXUIField(DisplayName = "Format")]
  [PXStringList(new string[] {"PDF", "HTML", "Excel"}, new string[] {"PDF", "HTML", "Excel"})]
  public virtual string ReportFormat { get; set; }

  [PXDBIdentity]
  [PXUIField(Visible = false)]
  public virtual int? ID { get; set; }

  [PXDBInt]
  [PXUIField(Visible = false)]
  public virtual int? Ticket { get; set; }

  [PXDBBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Is Income")]
  public virtual bool? IsIncome { get; set; }

  [PXDBCreatedByID(DontOverrideValue = true)]
  [PXUIField(DisplayName = "Created By", Enabled = false)]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Created At", Enabled = false)]
  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMEmail.noteID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMEmail.refNoteID>
  {
  }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMEmail.subject>
  {
  }

  public abstract class body : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMEmail.body>
  {
  }

  public abstract class mpstatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMEmail.mpstatus>
  {
  }

  public abstract class isArchived : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SMEmail.isArchived>
  {
  }

  public abstract class imcUID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMEmail.imcUID>
  {
  }

  public abstract class pop3UID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMEmail.pop3UID>
  {
  }

  public abstract class imapUID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMEmail.imapUID>
  {
  }

  public abstract class mailDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  SMEmail.mailDate>
  {
  }

  public abstract class mailAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMEmail.mailAccountID>
  {
  }

  public abstract class mailFrom : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMEmail.mailFrom>
  {
  }

  public abstract class mailReply : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMEmail.mailReply>
  {
  }

  public abstract class mailTo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMEmail.mailTo>
  {
  }

  public abstract class mailCc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMEmail.mailCc>
  {
  }

  public abstract class mailBcc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMEmail.mailBcc>
  {
  }

  public abstract class retryCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMEmail.retryCount>
  {
  }

  public abstract class messageId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMEmail.messageId>
  {
  }

  public abstract class messageReference : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMEmail.messageReference>
  {
  }

  public abstract class exception : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMEmail.exception>
  {
  }

  public abstract class format : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMEmail.format>
  {
  }

  public abstract class reportFormat : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMEmail.reportFormat>
  {
  }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMEmail.id>
  {
  }

  public abstract class ticket : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMEmail.ticket>
  {
  }

  public abstract class isIncome : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SMEmail.isIncome>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMEmail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMEmail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SMEmail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMEmail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMEmail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SMEmail.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SMEmail.Tstamp>
  {
  }
}
