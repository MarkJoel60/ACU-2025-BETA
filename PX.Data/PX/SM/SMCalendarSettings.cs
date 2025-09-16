// Decompiled with JetBrains decompiler
// Type: PX.SM.SMCalendarSettings
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

/// <exclude />
[PXCacheName("Calendar Settings")]
[Serializable]
public class SMCalendarSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private int? _PKID;
  private Guid? _UserID;
  private Guid? _UrlGuid;
  private bool? _IsPublic;
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true)]
  public int? PKID
  {
    get => this._PKID;
    set => this._PKID = value;
  }

  [PXDBGuid(false)]
  [PXDBDefault(typeof (AccessInfo.userID))]
  public Guid? UserID
  {
    get => this._UserID;
    set => this._UserID = value;
  }

  [PXDBGuid(false)]
  [SMGuidDefault]
  public Guid? UrlGuid
  {
    get => this._UrlGuid;
    set => this._UrlGuid = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Make My Calendar Public")]
  public bool? IsPublic
  {
    get => this._IsPublic;
    set => this._IsPublic = value;
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

  /// <exclude />
  public abstract class pKID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMCalendarSettings.pKID>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMCalendarSettings.userID>
  {
  }

  /// <exclude />
  public abstract class urlGuid : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMCalendarSettings.urlGuid>
  {
  }

  /// <exclude />
  public abstract class isPublic : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SMCalendarSettings.isPublic>
  {
  }

  /// <exclude />
  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SMCalendarSettings.Tstamp>
  {
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMCalendarSettings.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMCalendarSettings.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SMCalendarSettings.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SMCalendarSettings.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMCalendarSettings.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SMCalendarSettings.lastModifiedDateTime>
  {
  }
}
