// Decompiled with JetBrains decompiler
// Type: PX.SM.AUNotificationHistory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.SM.AU;
using PX.SM.Email;
using System;

#nullable enable
namespace PX.SM;

[PXProjection(typeof (Select2<AUNotificationTemplate, InnerJoin<SMEmail, On<SMEmail.noteID, Equal<AUNotificationTemplate.emailNoteID>>>>), Persistent = true)]
[Serializable]
public class AUNotificationHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _BodyVirtual;

  [PXBool]
  [PXUIField(Visible = false, DisplayName = "Selected")]
  public bool? Selected { get; set; }

  [PXDBString(8, IsKey = true, IsFixed = true, BqlField = typeof (AUNotificationTemplate.screenID))]
  [PXDefault]
  public virtual string ScreenID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (AUNotificationTemplate.notificationID))]
  [PXUIField(DisplayName = "Notification ID")]
  [PXSelector(typeof (Search<AUNotification.notificationID, Where<AUNotification.screenID, Equal<Current<AUNotificationHistory.screenID>>>>), DescriptionField = typeof (AUNotification.description))]
  public virtual int? NotificationID { get; set; }

  [PXDBString(IsUnicode = true, BqlField = typeof (SMEmail.exception))]
  [PXUIField(DisplayName = "Error Message")]
  public virtual string Exception { get; set; }

  [PXDBDate(IsKey = true, PreserveTime = true, UseSmallDateTime = false, InputMask = "g", BqlField = typeof (AUNotificationTemplate.executionDate))]
  [PXUIField(DisplayName = "Execution Date")]
  [PXDefault]
  public virtual System.DateTime? ExecutionDate { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (AUNotificationTemplate.status))]
  [PXDefault("P")]
  [PXStringList(new string[] {"P", "S", "F"}, new string[] {"Pending", "Sent", "Failed"})]
  [PXUIField(DisplayName = "Status")]
  public virtual string Status { get; set; }

  [PXDBDate(PreserveTime = true, UseSmallDateTime = false, InputMask = "g", BqlField = typeof (AUNotificationTemplate.dateSent))]
  [PXUIField(DisplayName = "Date Sent")]
  public virtual System.DateTime? DateSent { get; set; }

  [PXDBGuid(false, IsKey = true, BqlField = typeof (AUNotificationTemplate.refNoteID))]
  [PXDefault]
  public virtual Guid? RefNoteID { get; set; }

  [PXDBText(IsUnicode = true, BqlField = typeof (AUNotificationTemplate.fieldValues))]
  [PXUIField(DisplayName = "Field Values")]
  public virtual string FieldValues { get; set; }

  [PXDefault]
  [PXDBDateAndTime(UseTimeZone = true, BqlField = typeof (SMEmail.mailDate))]
  public System.DateTime? MailDate { get; set; }

  [PXDBInt(BqlField = typeof (SMEmail.mailAccountID))]
  [PXUIField(DisplayName = "From")]
  public int? MailAccountID { get; set; }

  [SMDBRecipient(false, BqlField = typeof (SMEmail.mailReply))]
  [PXUIField(DisplayName = "From")]
  public virtual string MailFrom { get; set; }

  [SMDBRecipient(true, BqlField = typeof (SMEmail.mailTo))]
  [PXUIField(DisplayName = "To")]
  public virtual string MailTo { get; set; }

  [SMDBRecipient(true, BqlField = typeof (SMEmail.mailCc))]
  [PXUIField(DisplayName = "Cc")]
  public virtual string MailCc { get; set; }

  [SMDBRecipient(true, BqlField = typeof (SMEmail.mailBcc))]
  [PXUIField(DisplayName = "Bcc")]
  public virtual string MailBcc { get; set; }

  [SMDBRecipient(false, BqlField = typeof (SMEmail.mailReply))]
  [PXUIField(DisplayName = "Reply")]
  public virtual string MailReply { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (SMEmail.subject))]
  [PXUIField(DisplayName = "Subject")]
  public virtual string Subject { get; set; }

  [PXDBText(IsUnicode = true, BqlField = typeof (SMEmail.body))]
  [PXUIField(DisplayName = "Body")]
  public virtual string Body { get; set; }

  [PXDBString(255 /*0xFF*/, BqlField = typeof (SMEmail.messageId))]
  [PXUIField(Visible = false)]
  public virtual string MessageId { get; set; }

  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC", BqlField = typeof (AUNotificationTemplate.reportID))]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Report ID", Required = false)]
  public virtual string ReportID { get; set; }

  [PXDBString(5, BqlField = typeof (AUNotificationTemplate.reportFormat))]
  [PXDefault("PDF", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXStringList(new string[] {"HTML", "PDF", "Excel"}, new string[] {"Html", "Pdf", "Excel"})]
  [PXUIField(DisplayName = "Report Format")]
  public virtual string ReportFormat { get; set; }

  [PXDBText(IsUnicode = true, BqlField = typeof (AUNotificationTemplate.resultset))]
  [PXUIField(DisplayName = "Resultset")]
  public virtual string Resultset { get; set; }

  [PXLong(IsKey = true)]
  [PXUIField(Visible = false)]
  public long? Ticks
  {
    [PXDependsOnFields(new System.Type[] {typeof (AUNotificationHistory.executionDate)})] get
    {
      return !this.ExecutionDate.HasValue ? new long?() : new long?(this.ExecutionDate.Value.Ticks);
    }
    set
    {
      if (!value.HasValue)
        return;
      this.ExecutionDate = new System.DateTime?(new System.DateTime(value.Value));
    }
  }

  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Body")]
  public virtual string BodyVirtual
  {
    get => !string.IsNullOrEmpty(this._BodyVirtual) ? this._BodyVirtual : this.Body;
    set => this._BodyVirtual = value;
  }

  [PXDBSequentialGuid(BqlField = typeof (SMEmail.noteID))]
  [PXExtraKey]
  public virtual Guid? SMEmailNoteID { get; set; }

  [PXDBGuid(false, BqlField = typeof (SMEmail.refNoteID))]
  [PXDBDefault(null, PersistingCheck = PXPersistingCheck.Nothing, DefaultForUpdate = false)]
  public virtual Guid? EmailRefNoteID => this.SMEmailNoteID;

  [PXDBString(10, BqlField = typeof (SMEmail.reportFormat))]
  [PXDBDefault(null, PersistingCheck = PXPersistingCheck.Nothing, DefaultForUpdate = false)]
  public virtual string EmailReportFormat
  {
    [PXDependsOnFields(new System.Type[] {typeof (AUNotificationHistory.reportFormat)})] get
    {
      return this.ReportFormat;
    }
  }

  [PXDBGuid(false, BqlField = typeof (AUNotificationTemplate.emailNoteID))]
  [PXDBDefault(null, PersistingCheck = PXPersistingCheck.Nothing, DefaultForUpdate = false)]
  public virtual Guid? EmailNoteID
  {
    [PXDependsOnFields(new System.Type[] {typeof (AUNotificationHistory.smEmailNoteID)})] get
    {
      return this.SMEmailNoteID;
    }
  }

  [PXDBInt(BqlField = typeof (SMEmail.retryCount))]
  [PXUIField(Visible = false)]
  [PXDefault(0)]
  public virtual int? RetryCount { get; set; }

  [PXDBString(2, IsFixed = true, IsUnicode = false, BqlField = typeof (SMEmail.mpstatus))]
  [MailStatusList]
  [PXDefault("DR")]
  [PXUIField(DisplayName = "Mail Status", Enabled = false)]
  public virtual string MPStatus { get; set; }

  [PXDBGuid(true, BqlField = typeof (SMEmail.imcUID))]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual Guid? ImcUID { get; set; }

  [PXDBCreatedByID(DontOverrideValue = true, BqlField = typeof (SMEmail.createdByID))]
  [PXUIField(DisplayName = "Created By", Enabled = false)]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID(BqlField = typeof (SMEmail.createdByScreenID))]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Created At", Enabled = false)]
  [PXDBCreatedDateTime(BqlField = typeof (SMEmail.createdDateTime))]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (SMEmail.lastModifiedByID))]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (SMEmail.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (SMEmail.lastModifiedDateTime))]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUNotificationHistory.selected>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotificationHistory.screenID>
  {
  }

  public abstract class notificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUNotificationHistory.notificationID>
  {
  }

  public abstract class exception : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotificationHistory.exception>
  {
  }

  public abstract class executionDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUNotificationHistory.executionDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotificationHistory.status>
  {
  }

  public abstract class dateSent : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUNotificationHistory.dateSent>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUNotificationHistory.refNoteID>
  {
  }

  public abstract class fieldValues : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotificationHistory.fieldValues>
  {
  }

  public abstract class mailDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUNotificationHistory.mailDate>
  {
  }

  public abstract class mailAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUNotificationHistory.mailAccountID>
  {
  }

  public abstract class mailFrom : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotificationHistory.mailFrom>
  {
  }

  public abstract class mailTo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotificationHistory.mailTo>
  {
  }

  public abstract class mailCc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotificationHistory.mailCc>
  {
  }

  public abstract class mailBcc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotificationHistory.mailBcc>
  {
  }

  public abstract class mailReply : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotificationHistory.mailReply>
  {
  }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotificationHistory.subject>
  {
  }

  public abstract class body : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotificationHistory.body>
  {
  }

  public abstract class messageId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotificationHistory.messageId>
  {
  }

  public abstract class reportID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotificationHistory.reportID>
  {
  }

  public abstract class reportFormat : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotificationHistory.reportFormat>
  {
  }

  public abstract class resultset : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotificationHistory.resultset>
  {
  }

  public abstract class bodyVirtual : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotificationHistory.bodyVirtual>
  {
  }

  public abstract class smEmailNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUNotificationHistory.smEmailNoteID>
  {
  }

  public abstract class emailRefNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUNotificationHistory.emailRefNoteID>
  {
  }

  public abstract class emailReportFormat : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotificationHistory.emailReportFormat>
  {
  }

  public abstract class emailNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUNotificationHistory.emailNoteID>
  {
  }

  public abstract class retryCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUNotificationHistory.retryCount>
  {
  }

  public abstract class mpstatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotificationHistory.mpstatus>
  {
  }

  public abstract class imcUID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUNotificationHistory.imcUID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUNotificationHistory.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotificationHistory.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUNotificationHistory.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUNotificationHistory.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotificationHistory.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUNotificationHistory.lastModifiedDateTime>
  {
  }
}
