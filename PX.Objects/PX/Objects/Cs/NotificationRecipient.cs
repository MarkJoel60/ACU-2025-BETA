// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.NotificationRecipient
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName("Notification Recipient")]
[Serializable]
public class NotificationRecipient : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _NotificationID;
  protected Guid? _SetupID;
  protected int? _SourceID;
  protected 
  #nullable disable
  string _ClassID;
  protected Guid? _RefNoteID;
  protected string _ContactType;
  protected int? _ContactID;
  protected string _Format;
  protected bool? _Active;
  protected string _Email;
  protected int? _OrderID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true)]
  [PXParent(typeof (Select<NotificationSource, Where<NotificationSource.sourceID, Equal<Current<NotificationRecipient.sourceID>>>>))]
  [PXUIField(DisplayName = "Notification ID")]
  public virtual int? NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [PXDBGuid(false)]
  [PXDefault(typeof (NotificationSource.setupID))]
  public virtual Guid? SetupID
  {
    get => this._SetupID;
    set => this._SetupID = value;
  }

  [PXDBInt]
  [PXDBDefault(typeof (NotificationSource.sourceID))]
  public virtual int? SourceID
  {
    get => this._SourceID;
    set => this._SourceID = value;
  }

  [PXDBString(10)]
  [PXDefault(typeof (NotificationSource.classID))]
  public virtual string ClassID
  {
    get => this._ClassID;
    set => this._ClassID = value;
  }

  [PXDBDefault]
  [PXDBGuid(false)]
  public virtual Guid? RefNoteID
  {
    get => this._RefNoteID;
    set => this._RefNoteID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Contact Type")]
  public virtual string ContactType
  {
    get => this._ContactType;
    set => this._ContactType = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Contact ID")]
  [PXNotificationContactSelector(typeof (NotificationRecipient.contactType), DirtyRead = true)]
  [PXFormula(typeof (Default<NotificationRecipient.contactType>))]
  public virtual int? ContactID
  {
    get => this._ContactID;
    set => this._ContactID = value;
  }

  [PXInt]
  [PXUIField(Visible = false)]
  public int? OriginalContactID => this.ContactID;

  [PXDefault(typeof (Search<NotificationSource.format, Where<NotificationSource.sourceID, Equal<Current<NotificationRecipient.sourceID>>>>))]
  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Format")]
  [NotificationFormat.List]
  [PXNotificationFormat(typeof (NotificationSource.reportID), typeof (NotificationSource.notificationID), typeof (Where<NotificationRecipient.sourceID, Equal<Current<NotificationSource.sourceID>>>))]
  public virtual string Format
  {
    get => this._Format;
    set => this._Format = value;
  }

  [PXDBBool]
  [PXDefault(typeof (Search<NotificationSource.active, Where<NotificationSource.sourceID, Equal<Current<NotificationRecipient.sourceID>>>>))]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  [PXDBString(1, IsFixed = true)]
  [RecipientAddTo]
  [PXDefault("T")]
  [PXUIField(DisplayName = "Add To")]
  public virtual string AddTo { get; set; }

  [PXBool]
  [PXUIField]
  [PXDependsOnFields(new System.Type[] {typeof (NotificationRecipient.addTo)})]
  [Obsolete]
  public virtual bool? Hidden => new bool?(this.AddTo == "B");

  [PXString]
  [PXUIField(DisplayName = "Email", Enabled = false)]
  [PXFormula(typeof (Selector<NotificationRecipient.contactID, Contact.eMail>))]
  [PXDependsOnFields(new System.Type[] {typeof (NotificationRecipient.contactID), typeof (NotificationRecipient.contactType)})]
  public virtual string Email
  {
    get => this._Email;
    set => this._Email = value;
  }

  [PXInt]
  public virtual int? OrderID
  {
    get => this._OrderID;
    set => this._OrderID = value;
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
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public abstract class notificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    NotificationRecipient.notificationID>
  {
  }

  public abstract class setupID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  NotificationRecipient.setupID>
  {
  }

  public abstract class sourceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  NotificationRecipient.sourceID>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NotificationRecipient.classID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  NotificationRecipient.refNoteID>
  {
  }

  public abstract class contactType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationRecipient.contactType>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  NotificationRecipient.contactID>
  {
  }

  public abstract class originalContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    NotificationRecipient.originalContactID>
  {
  }

  public abstract class format : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NotificationRecipient.format>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  NotificationRecipient.active>
  {
  }

  public abstract class addTo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NotificationRecipient.addTo>
  {
  }

  [Obsolete]
  public abstract class hidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  NotificationRecipient.hidden>
  {
  }

  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NotificationRecipient.email>
  {
  }

  public abstract class orderID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  NotificationRecipient.orderID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  NotificationRecipient.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    NotificationRecipient.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationRecipient.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    NotificationRecipient.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    NotificationRecipient.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationRecipient.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    NotificationRecipient.lastModifiedDateTime>
  {
  }
}
