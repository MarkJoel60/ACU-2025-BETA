// Decompiled with JetBrains decompiler
// Type: PX.SM.AUNotificationParameter
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
public class AUNotificationParameter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ScreenID;
  protected int? _NotificationID;
  protected short? _RowNbr;
  protected bool? _IsActive;
  protected string _ParameterName;
  protected string _Value;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _TStamp;

  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXDefault(typeof (AUNotification.screenID))]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (AUNotification.notificationID))]
  [PXParent(typeof (Select<AUNotification, Where<AUNotification.screenID, Equal<Current<AUNotificationParameter.screenID>>, And<AUNotification.notificationID, Equal<Current<AUNotificationParameter.notificationID>>>>>))]
  public virtual int? NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [PXDBShort(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (AUNotification.parameterCntr))]
  public virtual short? RowNbr
  {
    get => this._RowNbr;
    set => this._RowNbr = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault]
  [PXUIField(DisplayName = "Parameter Name", Required = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string ParameterName
  {
    get => this._ParameterName;
    set => this._ParameterName = value;
  }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Value")]
  public virtual string Value
  {
    get => this._Value;
    set => this._Value = value;
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

  public abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotificationParameter.screenID>
  {
  }

  public abstract class notificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUNotificationParameter.notificationID>
  {
  }

  public abstract class rowNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUNotificationParameter.rowNbr>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUNotificationParameter.isActive>
  {
  }

  public abstract class parameterName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotificationParameter.parameterName>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUNotificationParameter.value>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUNotificationParameter.noteID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUNotificationParameter.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotificationParameter.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUNotificationParameter.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUNotificationParameter.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUNotificationParameter.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUNotificationParameter.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUNotificationParameter.tStamp>
  {
  }
}
