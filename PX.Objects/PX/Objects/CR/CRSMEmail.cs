// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRSMEmail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.EP;
using PX.SM;
using PX.SM.Email;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
[PXBreakInheritance]
[PXProjection(typeof (Select2<CRActivity, InnerJoin<SMEmail, On<SMEmail.refNoteID, Equal<CRActivity.noteID>>>, Where<CRActivity.classID, Equal<CRActivityClass.email>, Or<CRActivity.classID, Equal<CRActivityClass.emailRouting>>>>), Persistent = true)]
[PXCacheName("Email Activity")]
[CRCacheIndependentPrimaryGraph(typeof (CREmailActivityMaint), typeof (Select<CRSMEmail, Where<CRSMEmail.noteID, Equal<Current<CRActivity.noteID>>>>))]
[Serializable]
public class CRSMEmail : CRActivity
{
  protected Guid? _ImcUID;

  /// <inheritdoc />
  [PXSequentialNote(SuppressActivitiesCount = true, IsKey = true, BqlField = typeof (CRActivity.noteID))]
  [PXTimeTag(typeof (CRSMEmail.noteID))]
  [CRSMEmailStatisticFormulas]
  [PXReferentialIntegrityCheck]
  public override Guid? NoteID { get; set; }

  [PXUIField(DisplayName = "Task")]
  [PXDBGuid(false, BqlField = typeof (CRActivity.parentNoteID))]
  [PXSelector(typeof (Search<CRActivity.noteID>))]
  [PXRestrictor(typeof (Where<CRActivity.classID, Equal<CRActivityClass.task>, Or<CRActivity.classID, Equal<CRActivityClass.events>>>), null, new System.Type[] {})]
  public override Guid? ParentNoteID { get; set; }

  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Related Document", Enabled = false)]
  public 
  #nullable disable
  string DocumentSource { get; set; }

  /// <inheritdoc />
  [PXDBInt(BqlField = typeof (CRActivity.classID))]
  [CRActivityClass]
  [PXDefault(typeof (CRActivityClass.email))]
  [PXUIField]
  [PXFieldDescription]
  public override int? ClassID { get; set; }

  [PXDBString(5, IsFixed = true, IsUnicode = false)]
  [PXUIField(DisplayName = "Type", Required = true)]
  [PXSelector(typeof (EPActivityType.type), DescriptionField = typeof (EPActivityType.description))]
  [PXRestrictor(typeof (Where<EPActivityType.active, Equal<True>>), "Activity Type '{0}' is not active.", new System.Type[] {typeof (EPActivityType.type)})]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPActivityType.application, Equal<PXActivityApplicationAttribute.backend>>>>>.Or<BqlOperand<EPActivityType.isSystem, IBqlBool>.IsEqual<True>>>), "Activity Type '{0}' is external. Only portal should create activities of this type.", new System.Type[] {typeof (EPActivityType.type)})]
  [PXDefault(typeof (Search<EPActivityType.type, Where<Current<CRSMEmail.classID>, Equal<EPActivityType.classID>>>))]
  [PXFormula(typeof (Default<CRSMEmail.classID>))]
  public override string Type { get; set; }

  [PXDBString(2, IsFixed = true, IsUnicode = false, BqlField = typeof (SMEmail.mpstatus))]
  [MailStatusList]
  [PXDefault("DR")]
  [PXUIField(DisplayName = "Email Status", Enabled = false)]
  public virtual string MPStatus { get; set; }

  [PXDBBool(BqlField = typeof (SMEmail.isArchived))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Archived", Enabled = true)]
  public virtual bool? IsArchived { get; set; }

  /// <inheritdoc />
  [PXDBString(2, IsFixed = true, BqlField = typeof (CRActivity.uistatus))]
  [PXFormula(typeof (Switch<Case<Where<CRSMEmail.isArchived, Equal<True>>, CRSMEmail.uistatus, Case<Where<CRSMEmail.type, IsNull>, ActivityStatusListAttribute.open, Case<Where<CRSMEmail.mpstatus, Equal<DoubleSpace>>, ActivityStatusListAttribute.completed, Case<Where<CRSMEmail.mpstatus, Equal<MailStatusListAttribute.processed>>, ActivityStatusListAttribute.completed, Case<Where<CRSMEmail.mpstatus, Equal<MailStatusListAttribute.deleted>, Or<CRSMEmail.mpstatus, Equal<MailStatusListAttribute.failed>, Or<CRSMEmail.mpstatus, Equal<MailStatusListAttribute.canceled>>>>, ActivityStatusListAttribute.canceled>>>>>, ActivityStatusListAttribute.open>))]
  [ActivityStatus]
  [PXUIField(DisplayName = "Status")]
  [PXDefault("OP")]
  public override string UIStatus { get; set; }

  /// <inheritdoc />
  [PXDBBool(BqlField = typeof (CRActivity.incoming))]
  [PXFormula(typeof (Switch<Case<Where<CRSMEmail.type, IsNotNull>, Selector<CRSMEmail.type, EPActivityType.incoming>>, False>))]
  [PXUIField(DisplayName = "Incoming")]
  public override bool? Incoming
  {
    [PXDependsOnFields(new System.Type[] {typeof (CRSMEmail.isIncome)})] get => this.IsIncome;
  }

  /// <inheritdoc />
  [PXDBBool(BqlField = typeof (CRActivity.outgoing))]
  [PXFormula(typeof (Switch<Case<Where<CRSMEmail.type, IsNotNull>, Selector<CRSMEmail.type, EPActivityType.outgoing>>, False>))]
  [PXUIField(DisplayName = "Outgoing")]
  public override bool? Outgoing
  {
    [PXDependsOnFields(new System.Type[] {typeof (CRSMEmail.isIncome)})] get
    {
      bool? isIncome = this.IsIncome;
      return !isIncome.HasValue ? new bool?() : new bool?(!isIncome.GetValueOrDefault());
    }
  }

  [PXDBSequentialGuid(BqlField = typeof (SMEmail.noteID))]
  [PXExtraKey]
  public virtual Guid? EmailNoteID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.SMEmail.ResponseToNoteID" />
  [PXUIField(DisplayName = "In Response To")]
  [PXDBGuid(false, BqlField = typeof (SMEmail.responseToNoteID))]
  [PXSelector(typeof (Search<SMEmail.noteID>))]
  public virtual Guid? ResponseToNoteID { get; set; }

  [PXDBGuid(false, BqlField = typeof (SMEmail.refNoteID))]
  [PXDBDefault(null)]
  public virtual Guid? EmailRefNoteID
  {
    [PXDependsOnFields(new System.Type[] {typeof (CRSMEmail.noteID)})] get => this.NoteID;
  }

  [PXDBString(256 /*0x0100*/, InputMask = "", IsUnicode = true, BqlField = typeof (SMEmail.subject))]
  [PXUIField]
  [PXNavigateSelector(typeof (CRSMEmail.subject))]
  public virtual string EmailSubject
  {
    [PXDependsOnFields(new System.Type[] {typeof (CRSMEmail.subject)})] get => this.Subject;
  }

  [PXDBText(IsUnicode = true, BqlField = typeof (SMEmail.body))]
  [PXUIField(DisplayName = "Activity Details")]
  public override string Body { get; set; }

  [PXDBGuid(true, BqlField = typeof (SMEmail.imcUID))]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual Guid? ImcUID
  {
    get => this._ImcUID ?? (this._ImcUID = new Guid?(Guid.NewGuid()));
    set => this._ImcUID = value;
  }

  [PXDBString(150, BqlField = typeof (SMEmail.pop3UID))]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual string Pop3UID { get; set; }

  [PXDBInt(BqlField = typeof (SMEmail.imapUID))]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual int? ImapUID { get; set; }

  [EmailAccountRaw]
  public int? MailAccountID { get; set; }

  [SMDBRecipient(false, BqlField = typeof (SMEmail.mailFrom))]
  [PXUIField(DisplayName = "From")]
  public virtual string MailFrom { get; set; }

  [SMDBRecipient(false, BqlField = typeof (SMEmail.mailReply))]
  [PXUIField(DisplayName = "Reply")]
  public virtual string MailReply { get; set; }

  [SMDBRecipient(true, BqlField = typeof (SMEmail.mailTo))]
  [PXUIField(DisplayName = "To")]
  public virtual string MailTo { get; set; }

  [SMDBRecipient(true, BqlField = typeof (SMEmail.mailCc))]
  [PXUIField(DisplayName = "CC")]
  public virtual string MailCc { get; set; }

  [SMDBRecipient(true, BqlField = typeof (SMEmail.mailBcc))]
  [PXUIField(DisplayName = "BCC")]
  public virtual string MailBcc { get; set; }

  [PXDBInt(BqlField = typeof (SMEmail.retryCount))]
  [PXUIField(Visible = false)]
  [PXDefault(0)]
  public virtual int? RetryCount { get; set; }

  [PXDBString(255 /*0xFF*/, BqlField = typeof (SMEmail.messageId))]
  [PXUIField(Visible = false)]
  public virtual string MessageId { get; set; }

  [PXDBString(BqlField = typeof (SMEmail.messageReference))]
  [PXUIField(Visible = false)]
  public virtual string MessageReference { get; set; }

  [PXDBString(IsUnicode = true, BqlField = typeof (SMEmail.exception))]
  [PXUIField(DisplayName = "Error Message")]
  public virtual string Exception { get; set; }

  [PXDefault("H")]
  [PXDBString(255 /*0xFF*/, BqlField = typeof (SMEmail.format))]
  [PXUIField(DisplayName = "Format")]
  [EmailFormatList]
  public virtual string Format { get; set; }

  [PXDBString(10, BqlField = typeof (SMEmail.reportFormat))]
  [PXUIField(DisplayName = "Format")]
  [PXStringList(new string[] {"PDF", "HTML", "Excel"}, new string[] {"PDF", "HTML", "Excel"})]
  public virtual string ReportFormat { get; set; }

  [PXDBIdentity(BqlField = typeof (SMEmail.id))]
  [PXUIField(Visible = false)]
  public virtual int? ID { get; set; }

  [PXDBInt(BqlField = typeof (SMEmail.ticket))]
  [PXUIField(Visible = false)]
  public virtual int? Ticket { get; set; }

  [PXDBBool(BqlField = typeof (SMEmail.isIncome))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Income")]
  public virtual bool? IsIncome { get; set; }

  [PXDBCreatedByID(DontOverrideValue = true, BqlField = typeof (SMEmail.createdByID))]
  [PXUIField(Enabled = false)]
  public virtual Guid? EmailCreatedByID { get; set; }

  [PXDBCreatedByScreenID(BqlField = typeof (SMEmail.createdByScreenID))]
  public virtual string EmailCreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Created At", Enabled = false)]
  [PXDBCreatedDateTime(BqlField = typeof (SMEmail.createdDateTime))]
  public virtual DateTime? EmailCreatedDateTime { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (SMEmail.lastModifiedByID))]
  public virtual Guid? EmailLastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (SMEmail.lastModifiedByScreenID))]
  public virtual string EmailLastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (SMEmail.lastModifiedDateTime))]
  public virtual DateTime? EmailLastModifiedDateTime { get; set; }

  public new class PK : PrimaryKeyOf<CRSMEmail>.By<CRSMEmail.noteID>
  {
    public static CRSMEmail Find(PXGraph graph, Guid? noteID, PKFindOptions options = 0)
    {
      return (CRSMEmail) PrimaryKeyOf<CRSMEmail>.By<CRSMEmail.noteID>.FindBy(graph, (object) noteID, options);
    }
  }

  public new static class FK
  {
    public class ActivityType : 
      PrimaryKeyOf<EPActivityType>.By<EPActivityType.type>.ForeignKeyOf<CRSMEmail>.By<CRSMEmail.type>
    {
    }

    public class Contact : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CRSMEmail>.By<CRSMEmail.contactID>
    {
    }

    public class BusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<CRSMEmail>.By<CRSMEmail.bAccountID>
    {
    }

    public class EmailAccount : 
      PrimaryKeyOf<EMailAccount>.By<EMailAccount.emailAccountID>.ForeignKeyOf<CRSMEmail>.By<CRSMEmail.mailAccountID>
    {
    }

    public class Owner : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CRSMEmail>.By<CRSMEmail.ownerID>
    {
    }

    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<CRSMEmail>.By<CRSMEmail.workgroupID>
    {
    }
  }

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRSMEmail.selected>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRSMEmail.noteID>
  {
  }

  public new abstract class parentNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRSMEmail.parentNoteID>
  {
  }

  public new abstract class refNoteIDType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRSMEmail.refNoteIDType>
  {
  }

  public new abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRSMEmail.refNoteID>
  {
  }

  public new abstract class source : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.source>
  {
  }

  public new abstract class documentNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRSMEmail.documentNoteID>
  {
  }

  public abstract class documentSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.documentSource>
  {
  }

  public new abstract class classID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRSMEmail.classID>
  {
  }

  public new abstract class classIcon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.classIcon>
  {
  }

  public new abstract class classInfo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.classInfo>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.type>
  {
  }

  public new abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.subject>
  {
  }

  public new abstract class location : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.location>
  {
  }

  public new abstract class priority : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRSMEmail.priority>
  {
  }

  public new abstract class priorityIcon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.priorityIcon>
  {
  }

  public abstract class mpstatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.mpstatus>
  {
  }

  public abstract class isArchived : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRSMEmail.isArchived>
  {
  }

  public new abstract class uistatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.uistatus>
  {
  }

  public new abstract class isOverdue : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRSMEmail.isOverdue>
  {
  }

  public new abstract class isCompleteIcon : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRSMEmail.isCompleteIcon>
  {
  }

  public new abstract class categoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRSMEmail.categoryID>
  {
  }

  public new abstract class allDay : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRSMEmail.allDay>
  {
  }

  public new abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRSMEmail.startDate>
  {
  }

  public new abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRSMEmail.endDate>
  {
  }

  public new abstract class completedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRSMEmail.completedDate>
  {
  }

  public new abstract class dayOfWeek : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRSMEmail.dayOfWeek>
  {
  }

  public new abstract class percentCompletion : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRSMEmail.percentCompletion>
  {
  }

  public new abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRSMEmail.ownerID>
  {
  }

  public new abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRSMEmail.workgroupID>
  {
  }

  public new abstract class isExternal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRSMEmail.isExternal>
  {
  }

  public new abstract class isPrivate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRSMEmail.isPrivate>
  {
  }

  public new abstract class incoming : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRSMEmail.incoming>
  {
  }

  public new abstract class outgoing : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRSMEmail.outgoing>
  {
  }

  public new abstract class synchronize : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRSMEmail.synchronize>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRSMEmail.bAccountID>
  {
  }

  public new abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRSMEmail.contactID>
  {
  }

  public new abstract class entityDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRSMEmail.entityDescription>
  {
  }

  public new abstract class showAsID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRSMEmail.showAsID>
  {
  }

  public new abstract class isLocked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRSMEmail.isLocked>
  {
  }

  public new abstract class deletedDatabaseRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRSMEmail.deletedDatabaseRecord>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRSMEmail.createdDateTime>
  {
  }

  public abstract class emailNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRSMEmail.emailNoteID>
  {
  }

  public abstract class responseToNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRSMEmail.responseToNoteID>
  {
  }

  public abstract class emailRefNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRSMEmail.emailRefNoteID>
  {
  }

  public abstract class emailSubject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.emailSubject>
  {
  }

  public new abstract class body : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.body>
  {
  }

  public abstract class imcUID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRSMEmail.imcUID>
  {
  }

  public abstract class pop3UID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.pop3UID>
  {
  }

  public abstract class imapUID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRSMEmail.imapUID>
  {
  }

  public abstract class mailAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRSMEmail.mailAccountID>
  {
  }

  public abstract class mailFrom : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.mailFrom>
  {
  }

  public abstract class mailReply : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.mailReply>
  {
  }

  public abstract class mailTo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.mailTo>
  {
  }

  public abstract class mailCc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.mailCc>
  {
  }

  public abstract class mailBcc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.mailBcc>
  {
  }

  public abstract class retryCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRSMEmail.retryCount>
  {
  }

  public abstract class messageId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.messageId>
  {
  }

  public abstract class messageReference : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRSMEmail.messageReference>
  {
  }

  public abstract class exception : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.exception>
  {
  }

  public abstract class format : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.format>
  {
  }

  public abstract class reportFormat : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSMEmail.reportFormat>
  {
  }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRSMEmail.id>
  {
  }

  public abstract class ticket : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRSMEmail.ticket>
  {
  }

  public abstract class isIncome : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRSMEmail.isIncome>
  {
  }

  public abstract class emailCreatedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRSMEmail.emailCreatedByID>
  {
  }

  public abstract class emailCreatedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRSMEmail.emailCreatedByScreenID>
  {
  }

  public abstract class emailCreatedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRSMEmail.emailCreatedDateTime>
  {
  }

  public abstract class emailLastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRSMEmail.emailLastModifiedByID>
  {
  }

  public abstract class emailLastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRSMEmail.emailLastModifiedByScreenID>
  {
  }

  public abstract class emailLastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRSMEmail.emailLastModifiedDateTime>
  {
  }
}
