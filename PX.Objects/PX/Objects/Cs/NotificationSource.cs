// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.NotificationSource
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

[PXCacheName("Notification Source")]
[DebuggerDisplay("SetupID = {SetupID}: {ClassID} | {RefNoteID}, {NBranchID}")]
[Serializable]
public class NotificationSource : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SourceID;
  protected Guid? _SetupID;
  protected Guid? _RefNoteID;
  protected 
  #nullable disable
  string _ClassID;
  protected int? _NBranchID;
  protected int _EMailAccountID;
  protected string _ReportID;
  protected int? _NotificationID;
  protected string _Format;
  protected bool? _Active;
  protected bool? _OverrideSource;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true)]
  [PXParent(typeof (Select<NotificationSetup, Where<NotificationSetup.setupID, Equal<Current<NotificationSource.setupID>>>>))]
  public virtual int? SourceID
  {
    get => this._SourceID;
    set => this._SourceID = value;
  }

  [PXDBGuid(false, IsKey = true)]
  [PXUIField(DisplayName = "Mailing ID")]
  [PXUIEnabled(typeof (Where<BqlOperand<NotificationSource.setupID, IBqlGuid>.IsNull>))]
  [PXSelector(typeof (Search<NotificationSetup.setupID>))]
  public virtual Guid? SetupID
  {
    get => this._SetupID;
    set => this._SetupID = value;
  }

  [PXDBDefault]
  [PXDBGuid(false)]
  public virtual Guid? RefNoteID
  {
    get => this._RefNoteID;
    set => this._RefNoteID = value;
  }

  [PXDBString(10)]
  [PXUIField]
  public virtual string ClassID
  {
    get => this._ClassID;
    set => this._ClassID = value;
  }

  [Branch(null, null, true, true, false)]
  [PXCheckUnique(new Type[] {typeof (NotificationSource.setupID)}, IgnoreNulls = false, Where = typeof (Where<NotificationSource.classID, Equal<Current<NotificationSource.classID>>>))]
  [PXDefault(typeof (Search<NotificationSetup.nBranchID, Where<NotificationSetup.setupID, Equal<Current<NotificationSource.setupID>>>>))]
  public virtual int? NBranchID
  {
    get => this._NBranchID;
    set => this._NBranchID = value;
  }

  [EmailAccountRaw]
  [PXDefault(typeof (Search2<NotificationSetup.eMailAccountID, InnerJoin<EMailAccount, On<NotificationSetup.eMailAccountID, Equal<EMailAccount.emailAccountID>>>, Where<NotificationSetup.setupID, Equal<Current<NotificationSource.setupID>>>>))]
  [PXFormula(typeof (Default<NotificationSource.setupID>))]
  public virtual int? EMailAccountID { get; set; }

  [PXDefault(typeof (Search<NotificationSetup.reportID, Where<NotificationSetup.setupID, Equal<Current<NotificationSource.setupID>>>>))]
  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Report")]
  [PXFormula(typeof (Default<NotificationSource.setupID>))]
  public virtual string ReportID
  {
    get => this._ReportID;
    set => this._ReportID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Email Template")]
  [PXDefault(typeof (Search<NotificationSetup.notificationID, Where<NotificationSetup.setupID, Equal<Current<NotificationSource.setupID>>>>))]
  [PXSelector(typeof (Search<Notification.notificationID>), SubstituteKey = typeof (Notification.name), DescriptionField = typeof (Notification.name))]
  public virtual int? NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [PXDefault(typeof (Search<NotificationSetup.format, Where<NotificationSetup.setupID, Equal<Current<NotificationSource.setupID>>>>))]
  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Format")]
  [NotificationFormat.List]
  [PXNotificationFormat(typeof (NotificationSource.reportID), typeof (NotificationSource.notificationID))]
  [PXFormula(typeof (Default<NotificationSource.setupID>))]
  [PXUIVerify]
  public virtual string Format
  {
    get => this._Format;
    set => this._Format = value;
  }

  [PXDBBool]
  [PXDefault(typeof (Search<NotificationSetup.active, Where<NotificationSetup.setupID, Equal<Current<NotificationSource.setupID>>>>))]
  [PXUIField(DisplayName = "Active")]
  [PXFormula(typeof (Default<NotificationSource.setupID>))]
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

  [PXBool]
  [PXUIField]
  [PXUIEnabled(typeof (Where<BqlOperand<NotificationSource.overrideSource, IBqlBool>.IsEqual<True>>))]
  public virtual bool? OverrideSource
  {
    [PXDependsOnFields(new Type[] {typeof (NotificationSource.refNoteID)})] get
    {
      return new bool?(((int) this._OverrideSource ?? (this.RefNoteID.HasValue ? 1 : 0)) != 0);
    }
    set => this._OverrideSource = value;
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

  public static class FK
  {
    /// <summary>Default Email Account</summary>
    public class EmailAccount : 
      PrimaryKeyOf<EMailAccount>.By<EMailAccount.emailAccountID>.ForeignKeyOf<NotificationSource>.By<NotificationSource.eMailAccountID>
    {
    }
  }

  public abstract class sourceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  NotificationSource.sourceID>
  {
  }

  public abstract class setupID_description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationSource.setupID_description>
  {
  }

  public abstract class setupID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  NotificationSource.setupID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  NotificationSource.refNoteID>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NotificationSource.classID>
  {
  }

  public abstract class nBranchID_description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationSource.nBranchID_description>
  {
  }

  public abstract class nBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  NotificationSource.nBranchID>
  {
  }

  public abstract class eMailAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    NotificationSource.eMailAccountID>
  {
    public class EmailAccountRule : 
      EMailAccount.userID.PreventMakingPersonalIfUsedAsSystem<SelectFromBase<NotificationSource, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<NotificationSource.eMailAccountID>.IsRelatedTo<EMailAccount.emailAccountID>.AsSimpleKey.WithTablesOf<EMailAccount, NotificationSource>, EMailAccount, NotificationSource>.SameAsCurrent>>
    {
    }
  }

  public abstract class reportID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NotificationSource.reportID>
  {
  }

  public abstract class notificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    NotificationSource.notificationID>
  {
  }

  public abstract class format : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NotificationSource.format>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  NotificationSource.active>
  {
  }

  public abstract class recipientsBehavior : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationSource.recipientsBehavior>
  {
  }

  public abstract class overrideSource : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    NotificationSource.overrideSource>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  NotificationSource.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  NotificationSource.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationSource.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    NotificationSource.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    NotificationSource.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationSource.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    NotificationSource.lastModifiedDateTime>
  {
  }
}
