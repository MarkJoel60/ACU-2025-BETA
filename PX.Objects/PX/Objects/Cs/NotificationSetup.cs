// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.NotificationSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using PX.SM;
using PX.SM.Email;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName("Default Notification setup")]
[DebuggerDisplay("SetupID = {SetupID}: {NotificationCD}, {NBranchID}")]
[Serializable]
public class NotificationSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _SetupID;
  protected 
  #nullable disable
  string _Module;
  protected string _SourceCD;
  protected string _NotificationCD;
  protected int? _NBranchID;
  protected int? _EMailAccountID;
  protected string _ReportID;
  protected Guid? _DefaultPrinterID;
  protected int? _NotificationID;
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
  public virtual Guid? SetupID
  {
    get => this._SetupID;
    set => this._SetupID = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Module", Enabled = false)]
  public virtual string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXDBString(10, IsFixed = true, InputMask = "")]
  [PXUIField(DisplayName = "Source", Enabled = false)]
  public virtual string SourceCD
  {
    get => this._SourceCD;
    set => this._SourceCD = value;
  }

  [PXDBString(30, InputMask = "", IsUnicode = true)]
  [PXUIField]
  [PXCheckUnique(new Type[] {typeof (NotificationSetup.module), typeof (NotificationSetup.sourceCD), typeof (NotificationSetup.nBranchID)})]
  [PXDefault]
  public virtual string NotificationCD
  {
    get => this._NotificationCD;
    set => this._NotificationCD = value;
  }

  [Branch(null, null, true, true, false)]
  public virtual int? NBranchID
  {
    get => this._NBranchID;
    set => this._NBranchID = value;
  }

  [EmailAccountRaw]
  public virtual int? EMailAccountID { get; set; }

  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXUIField]
  public virtual string ReportID
  {
    get => this._ReportID;
    set => this._ReportID = value;
  }

  [PXPrinterSelector]
  [PXForeignReference(typeof (Field<NotificationSetup.defaultPrinterID>.IsRelatedTo<SMPrinter.printerID>))]
  public virtual Guid? DefaultPrinterID
  {
    get => this._DefaultPrinterID;
    set => this._DefaultPrinterID = value;
  }

  [PXDBInt]
  [PXUIField]
  [PXSelector(typeof (Search<Notification.notificationID>), SubstituteKey = typeof (Notification.name), DescriptionField = typeof (Notification.name))]
  public virtual int? NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [PXDefault("H")]
  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Format")]
  [NotificationFormat.List]
  [PXNotificationFormat(typeof (NotificationSetup.reportID), typeof (NotificationSetup.notificationID))]
  [PXUIVerify]
  public virtual string Format
  {
    get => this._Format;
    set => this._Format = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PX.Objects.CS.RecipientsBehavior]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Recipients")]
  public virtual string RecipientsBehavior { get; set; }

  [PXDBString(15, IsUnicode = true)]
  public virtual string ShipVia { get; set; }

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

  public class PK : PrimaryKeyOf<NotificationSetup>.By<NotificationSetup.setupID>
  {
    public static NotificationSetup Find(PXGraph graph, Guid? setupID, PKFindOptions options = 0)
    {
      return (NotificationSetup) PrimaryKeyOf<NotificationSetup>.By<NotificationSetup.setupID>.FindBy(graph, (object) setupID, options);
    }
  }

  public static class FK
  {
    public class DefaultEmailAccount : 
      PrimaryKeyOf<EMailAccount>.By<EMailAccount.emailAccountID>.ForeignKeyOf<NotificationSetup>.By<NotificationSetup.eMailAccountID>
    {
    }
  }

  public abstract class setupID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  NotificationSetup.setupID>
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NotificationSetup.module>
  {
  }

  public abstract class sourceCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NotificationSetup.sourceCD>
  {
  }

  public abstract class notificationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationSetup.notificationCD>
  {
    public const string _MULTIPLE = " MULTIPLE";

    public class _Multiple : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      NotificationSetup.notificationCD._Multiple>
    {
      public _Multiple()
        : base(" MULTIPLE")
      {
      }
    }
  }

  public abstract class nBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  NotificationSetup.nBranchID>
  {
  }

  public abstract class eMailAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    NotificationSetup.eMailAccountID>
  {
    public class EmailAccountRule : 
      EMailAccount.userID.PreventMakingPersonalIfUsedAsSystem<SelectFromBase<NotificationSetup, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<NotificationSetup.eMailAccountID>.IsRelatedTo<EMailAccount.emailAccountID>.AsSimpleKey.WithTablesOf<EMailAccount, NotificationSetup>, EMailAccount, NotificationSetup>.SameAsCurrent>>
    {
    }
  }

  public abstract class reportID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NotificationSetup.reportID>
  {
  }

  public abstract class defaultPrinterID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    NotificationSetup.defaultPrinterID>
  {
  }

  public abstract class notificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    NotificationSetup.notificationID>
  {
  }

  public abstract class format : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NotificationSetup.format>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  NotificationSetup.active>
  {
  }

  public abstract class recipientsBehavior : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationSetup.recipientsBehavior>
  {
  }

  public abstract class shipVia : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NotificationSetup.shipVia>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  NotificationSetup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  NotificationSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    NotificationSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    NotificationSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    NotificationSetup.lastModifiedDateTime>
  {
  }
}
