// Decompiled with JetBrains decompiler
// Type: PX.SM.Notification
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.Process;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM.Email;
using System;

#nullable enable
namespace PX.SM;

/// <exclude />
[PXPrimaryGraph(typeof (SMNotificationMaint))]
[PXCacheName("Notification")]
public class Notification : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  INotification,
  ISentByEvent,
  IItemWithRefId
{
  protected 
  #nullable disable
  string _Subject;
  internal const string EventSubscriberType = "EMLT";

  public Guid? GetRefId() => this.NoteID;

  public Guid? GetHandlerId() => this.NoteID;

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Notification ID")]
  [PXSelector(typeof (Notification.notificationID), new System.Type[] {typeof (Notification.notificationID), typeof (Notification.subject), typeof (Notification.screenID)}, DescriptionField = typeof (Notification.name))]
  public virtual int? NotificationID { get; set; }

  [PXDefault]
  [PXDBString(255 /*0xFF*/, InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Name { get; set; }

  [EmailAccountRaw(EmailAccountsToShowOptions.OnlySystem, null, DisplayName = "From")]
  public virtual int? NFrom { get; set; }

  [PXDBString(1000, IsUnicode = true)]
  [PXUIField(DisplayName = "To")]
  public virtual string NTo { get; set; }

  [PXDBString(1000, IsUnicode = true)]
  [PXUIField(DisplayName = "CC")]
  public virtual string NCc { get; set; }

  [PXDBString(1000, IsUnicode = true)]
  [PXUIField(DisplayName = "BCC")]
  public virtual string NBcc { get; set; }

  [PXDBString(255 /*0xFF*/, InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Subject", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Subject { get; set; }

  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Screen", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string ScreenID { get; set; }

  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Body")]
  public virtual string Body { get; set; }

  [PXSelector(typeof (Search<Locale.localeName, Where<Locale.isActive, Equal<PX.Data.True>>>), DescriptionField = typeof (Locale.translatedName))]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Locale")]
  public virtual string LocaleName { get; set; }

  [PXNote]
  [PXSearchable(65535 /*0xFFFF*/, "Notification: {0}", new System.Type[] {typeof (Notification.name)}, new System.Type[] {typeof (Notification.name)}, Line1Format = "{0}", Line1Fields = new System.Type[] {typeof (Notification.subject)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (Notification.screenID)})]
  [PXReferentialIntegrityCheck]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Link-To Account")]
  public string BAccountID { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Link-To Contact")]
  public string ContactID { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Link-To Entity")]
  public string RefNoteID { get; set; }

  [PXDBString]
  [PXStringList(new string[] {null}, new string[] {""})]
  [PXUIField(DisplayName = "Attach Report Opened by Action")]
  public string ReportAction { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Attach Activity")]
  public bool? AttachActivity { get; set; }

  [PXInternalUseOnly]
  string ISentByEvent.SubscriberType
  {
    get => "EMLT";
    set
    {
    }
  }

  /// <summary>The type of the activity.</summary>
  [PXDBString(5, IsFixed = true, IsUnicode = false)]
  [PXUIField(DisplayName = "Activity Type")]
  public string Type { get; set; }

  public class PK : PrimaryKeyOf<Notification>.By<Notification.notificationID>
  {
    public static Notification Find(PXGraph graph, int? notificationID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<Notification>.By<Notification.notificationID>.FindBy(graph, (object) notificationID, options);
    }
  }

  public class UK : PrimaryKeyOf<Notification>.By<Notification.noteID>
  {
    public static Notification Find(PXGraph graph, Guid? noteID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<Notification>.By<Notification.noteID>.FindBy(graph, (object) noteID, options);
    }
  }

  public static class FK
  {
    public class EMailAccount : 
      PrimaryKeyOf<EMailAccount>.By<EMailAccount.emailAccountID>.ForeignKeyOf<Notification>.By<Notification.nfrom>
    {
    }

    public class Locale : 
      PrimaryKeyOf<Locale>.By<Locale.localeName>.ForeignKeyOf<Notification>.By<Notification.localeName>
    {
    }
  }

  /// <exclude />
  public abstract class notificationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Notification.notificationID>
  {
  }

  /// <exclude />
  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Notification.name>
  {
  }

  /// <exclude />
  public abstract class nfrom : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Notification.nfrom>
  {
    public class EmailAccountRule : 
      EMailAccount.userID.PreventMakingPersonalIfUsedAsSystem<Select<Notification, Where<KeysRelation<PX.Data.ReferentialIntegrity.Attributes.Field<Notification.nfrom>.IsRelatedTo<EMailAccount.emailAccountID>.AsSimpleKey.WithTablesOf<EMailAccount, Notification>, EMailAccount, Notification>.SameAsCurrent>>>
    {
    }
  }

  /// <exclude />
  public abstract class nto : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Notification.nto>
  {
  }

  /// <exclude />
  public abstract class ncc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Notification.ncc>
  {
  }

  /// <exclude />
  public abstract class nBcc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Notification.nBcc>
  {
  }

  /// <exclude />
  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Notification.subject>
  {
  }

  /// <exclude />
  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Notification.screenID>
  {
  }

  /// <exclude />
  public abstract class body : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Notification.body>
  {
  }

  /// <exclude />
  public abstract class localeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Notification.localeName>
  {
  }

  /// <exclude />
  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Notification.noteID>
  {
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Notification.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Notification.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    Notification.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    Notification.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Notification.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    Notification.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Notification.Tstamp>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Notification.bAccountID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Notification.contactID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Notification.refNoteID>
  {
  }

  public abstract class reportAction : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Notification.reportAction>
  {
  }

  public abstract class attachActivity : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Notification.attachActivity>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Notification.type>
  {
  }
}
