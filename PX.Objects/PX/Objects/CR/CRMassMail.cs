// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMassMail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.CS;
using PX.SM.Email;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXCacheName("Mass Emails")]
[PXPrimaryGraph(typeof (CRMassMailMaint))]
[Serializable]
public class CRMassMail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, INotable
{
  protected int? _MassMailID;
  protected 
  #nullable disable
  string _MassMailCD;
  protected string _Status;
  protected int? _Source;
  protected string _SourceType;
  protected DateTime? _PlannedDate;
  protected DateTime? _SentDateTime;
  protected string _MailSubject;
  protected string _MailTo;
  protected string _MailCc;
  protected string _MailBcc;
  protected string _MailContent;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity]
  public virtual int? MassMailID
  {
    get => this._MassMailID;
    set => this._MassMailID = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [AutoNumber(typeof (CRSetup.massMailNumberingID), typeof (AccessInfo.businessDate))]
  [PXSelector(typeof (CRMassMail.massMailCD))]
  [PXFieldDescription]
  public virtual string MassMailCD
  {
    get => this._MassMailCD;
    set => this._MassMailCD = value;
  }

  [PXDBString]
  [PXDefault("H")]
  [CRMassMailStatuses]
  [PXUIField(DisplayName = "Status")]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [CRMassMailSources]
  [PXUIField]
  public virtual int? Source
  {
    get => this._Source;
    set => this._Source = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Source Type")]
  public virtual string SourceType
  {
    get => this._SourceType;
    set => this._SourceType = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? PlannedDate
  {
    get => this._PlannedDate;
    set => this._PlannedDate = value;
  }

  [PXDBDate(PreserveTime = true, InputMask = "g")]
  [PXUIField(DisplayName = "Sent", Visible = true, Enabled = false)]
  public virtual DateTime? SentDateTime
  {
    get => this._SentDateTime;
    set => this._SentDateTime = value;
  }

  [PXDBString(998, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Subject")]
  public virtual string MailSubject
  {
    get => this._MailSubject;
    set => this._MailSubject = value;
  }

  [EmailAccountRaw]
  [PXDefault]
  public virtual int? MailAccountID { get; set; }

  [SMDBRecipient(true)]
  [PXDefault]
  [PXUIField(DisplayName = "To")]
  public virtual string MailTo
  {
    get => this._MailTo;
    set => this._MailTo = value;
  }

  [SMDBRecipient(true)]
  [PXUIField(DisplayName = "CC")]
  public virtual string MailCc
  {
    get => this._MailCc;
    set => this._MailCc = value;
  }

  [SMDBRecipient(true)]
  [PXUIField(DisplayName = "BCC")]
  public virtual string MailBcc
  {
    get => this._MailBcc;
    set => this._MailBcc = value;
  }

  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Content")]
  public virtual string MailContent
  {
    get => this._MailContent;
    set => this._MailContent = value;
  }

  [PXNote(DescriptionField = typeof (CRMassMail.massMailCD), Selector = typeof (CRMassMail.massMailCD), ShowInReferenceSelector = true)]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public abstract class massMailID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRMassMail.massMailID>
  {
  }

  public abstract class massMailCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMassMail.massMailCD>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMassMail.status>
  {
  }

  public abstract class source : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRMassMail.source>
  {
  }

  public abstract class sourceType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMassMail.sourceType>
  {
  }

  public abstract class plannedDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRMassMail.plannedDate>
  {
  }

  public abstract class sentDateTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRMassMail.sentDateTime>
  {
  }

  public abstract class mailSubject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMassMail.mailSubject>
  {
  }

  public abstract class mailAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRMassMail.mailAccountID>
  {
  }

  public abstract class mailTo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMassMail.mailTo>
  {
  }

  public abstract class mailCc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMassMail.mailCc>
  {
  }

  public abstract class mailBcc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMassMail.mailBcc>
  {
  }

  public abstract class mailContent : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMassMail.mailContent>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRMassMail.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRMassMail.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRMassMail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMassMail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRMassMail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRMassMail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMassMail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRMassMail.lastModifiedDateTime>
  {
  }
}
