// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.NotificationSetupRecipient
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

[PXCacheName("Default Notification Recipient")]
[Serializable]
public class NotificationSetupRecipient : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _RecipientID;
  protected Guid? _SetupID;
  protected 
  #nullable disable
  string _ContactType;
  protected int? _ContactID;
  protected string _Format;
  protected bool? _Active;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBGuid(true, IsKey = true)]
  public virtual Guid? RecipientID
  {
    get => this._RecipientID;
    set => this._RecipientID = value;
  }

  [PXDBGuid(false)]
  [PXDefault(typeof (NotificationSetup.setupID))]
  [PXParent(typeof (Select<NotificationSetup, Where<NotificationSetup.setupID, Equal<Current<NotificationSetupRecipient.setupID>>>>))]
  public virtual Guid? SetupID
  {
    get => this._SetupID;
    set => this._SetupID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Contact Type")]
  public virtual string ContactType
  {
    get => this._ContactType;
    set => this._ContactType = value;
  }

  [PXDBInt]
  [PXFormula(typeof (Default<NotificationSetupRecipient.contactType>))]
  public virtual int? ContactID
  {
    get => this._ContactID;
    set => this._ContactID = value;
  }

  [PXInt]
  [PXUIField(Visible = false)]
  public int? OriginalContactID => this.ContactID;

  [PXDefault(typeof (Search<NotificationSetup.format, Where<NotificationSetup.setupID, Equal<Current<NotificationSetupRecipient.setupID>>>>))]
  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Format")]
  [NotificationFormat.List]
  [PXNotificationFormat(typeof (NotificationSetup.reportID), typeof (NotificationSetup.notificationID), typeof (Where<NotificationSetupRecipient.setupID, Equal<Current<NotificationSetup.setupID>>>))]
  public virtual string Format
  {
    get => this._Format;
    set => this._Format = value;
  }

  [PXDBBool]
  [PXDefault(typeof (Search<NotificationSetup.active, Where<NotificationSetup.setupID, Equal<Current<NotificationSetupRecipient.setupID>>>>))]
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
  [PXDependsOnFields(new System.Type[] {typeof (NotificationSetupRecipient.addTo)})]
  [Obsolete]
  public virtual bool? Hidden => new bool?(this.AddTo == "B");

  /// <summary>The email address of the linked contact.</summary>
  [PXString]
  [PXUIField(DisplayName = "Email", Enabled = false)]
  [PXFormula(typeof (Selector<NotificationSetupRecipient.contactID, Contact.eMail>))]
  public virtual string Email { get; set; }

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

  public abstract class recipientID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    NotificationSetupRecipient.recipientID>
  {
  }

  public abstract class setupID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  NotificationSetupRecipient.setupID>
  {
  }

  public abstract class contactType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationSetupRecipient.contactType>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  NotificationSetupRecipient.contactID>
  {
  }

  public abstract class originalContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    NotificationSetupRecipient.originalContactID>
  {
  }

  public abstract class format : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NotificationSetupRecipient.format>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  NotificationSetupRecipient.active>
  {
  }

  public abstract class addTo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NotificationSetupRecipient.addTo>
  {
  }

  [Obsolete]
  public abstract class hidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  NotificationSetupRecipient.hidden>
  {
  }

  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NotificationSetupRecipient.email>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    NotificationSetupRecipient.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    NotificationSetupRecipient.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationSetupRecipient.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    NotificationSetupRecipient.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    NotificationSetupRecipient.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationSetupRecipient.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    NotificationSetupRecipient.lastModifiedDateTime>
  {
  }
}
