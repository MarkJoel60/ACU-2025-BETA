// Decompiled with JetBrains decompiler
// Type: PX.SM.AUNotification
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class AUNotification : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ScreenID;
  protected int? _NotificationID;
  protected int? _ParentNotificationID;
  protected string _Description;
  protected bool? _IsActive;
  protected bool? _IsPublic;
  protected string _Subject;
  protected string _Body;
  protected string _CreationMethod;
  protected string _ReportID;
  protected string _ReportFormat;
  protected bool? _IsEmbeded;
  protected string _ActionName;
  protected string _MenuText;
  protected string _GraphName;
  protected string _ViewName;
  protected string _TableName;
  protected string _BqlTableName;
  protected short? _FilterCntr;
  protected short? _ContentCntr;
  protected short? _AddressCntr;
  protected short? _ParameterCntr;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _TStamp;

  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXDefault]
  [PXUIField(DisplayName = "Screen ID", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Notification ID")]
  [PXSelector(typeof (Search<AUNotification.notificationID, Where<AUNotification.screenID, Equal<Optional<AUNotification.screenID>>>>), DescriptionField = typeof (AUNotification.description))]
  public virtual int? NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Parent Notification ID")]
  [PXSelector(typeof (Search<AUNotification.notificationID, Where<AUNotification.screenID, Equal<Optional<AUNotification.screenID>>>>), DescriptionField = typeof (AUNotification.description))]
  public virtual int? ParentNotificationID
  {
    get => this._ParentNotificationID;
    set => this._ParentNotificationID = value;
  }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Public", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual bool? IsPublic
  {
    get => this._IsPublic;
    set => this._IsPublic = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Subject")]
  public virtual string Subject
  {
    get => this._Subject;
    set => this._Subject = value;
  }

  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Body")]
  public virtual string Body
  {
    get => this._Body;
    set => this._Body = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("R")]
  [PXStringList(new string[] {"R", "A", "N"}, new string[] {"Report", "Action", "None"})]
  [PXUIField(DisplayName = "Data Source")]
  public virtual string CreationMethod
  {
    get => this._CreationMethod;
    set => this._CreationMethod = value;
  }

  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Report ID", Required = false)]
  public virtual string ReportID
  {
    get => this._ReportID;
    set => this._ReportID = value;
  }

  [PXDBString(5)]
  [PXDefault("PDF", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXStringList(new string[] {"HTML", "PDF", "Excel"}, new string[] {"Html", "Pdf", "Excel"})]
  [PXUIField(DisplayName = "Report Format")]
  public virtual string ReportFormat
  {
    get => this._ReportFormat;
    set => this._ReportFormat = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Embedded", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual bool? IsEmbeded
  {
    get => this._IsEmbeded;
    set => this._IsEmbeded = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Action Name", Enabled = false)]
  [PXStringList(new string[] {null}, new string[] {""})]
  public virtual string ActionName
  {
    get => this._ActionName;
    set => this._ActionName = value;
  }

  [PXDBString(64 /*0x40*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Menu Text", Enabled = false)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXStringList(new string[] {null}, new string[] {""})]
  public virtual string MenuText
  {
    get => this._MenuText;
    set => this._MenuText = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault]
  public virtual string GraphName
  {
    get => this._GraphName;
    set => this._GraphName = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault]
  public virtual string ViewName
  {
    get => this._ViewName;
    set => this._ViewName = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault]
  public virtual string TableName
  {
    get => this._TableName;
    set => this._TableName = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault]
  public virtual string BqlTableName
  {
    get => this._BqlTableName;
    set => this._BqlTableName = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? FilterCntr
  {
    get => this._FilterCntr;
    set => this._FilterCntr = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? ContentCntr
  {
    get => this._ContentCntr;
    set => this._ContentCntr = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? AddressCntr
  {
    get => this._AddressCntr;
    set => this._AddressCntr = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? ParameterCntr
  {
    get => this._ParameterCntr;
    set => this._ParameterCntr = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
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
  public virtual System.DateTime? CreatedDateTime
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
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] TStamp
  {
    get => this._TStamp;
    set => this._TStamp = value;
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotification.screenID>
  {
  }

  public abstract class notificationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUNotification.notificationID>
  {
  }

  public abstract class parentNotificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUNotification.parentNotificationID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotification.description>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUNotification.isActive>
  {
  }

  public abstract class isPublic : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUNotification.isPublic>
  {
  }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotification.subject>
  {
  }

  public abstract class body : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotification.body>
  {
  }

  public abstract class creationMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotification.creationMethod>
  {
  }

  public abstract class reportID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotification.reportID>
  {
  }

  public abstract class reportFormat : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotification.reportFormat>
  {
  }

  public abstract class isEmbeded : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUNotification.isEmbeded>
  {
  }

  public abstract class actionName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotification.actionName>
  {
  }

  public abstract class menuText : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotification.menuText>
  {
  }

  public abstract class graphName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotification.graphName>
  {
  }

  public abstract class viewName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotification.viewName>
  {
  }

  public abstract class tableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotification.tableName>
  {
  }

  public abstract class bqlTableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotification.bqlTableName>
  {
  }

  public abstract class filterCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUNotification.filterCntr>
  {
  }

  public abstract class contentCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUNotification.contentCntr>
  {
  }

  public abstract class addressCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUNotification.addressCntr>
  {
  }

  public abstract class parameterCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUNotification.parameterCntr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUNotification.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUNotification.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotification.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUNotification.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUNotification.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotification.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUNotification.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUNotification.tStamp>
  {
  }
}
