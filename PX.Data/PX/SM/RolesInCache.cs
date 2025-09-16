// Decompiled with JetBrains decompiler
// Type: PX.SM.RolesInCache
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Roles In Cache")]
public class RolesInCache : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ScreenID;
  protected string _Cachetype;
  protected string _Rolename;
  protected string _ApplicationName;
  protected short? _Accessrights;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  [PXDBString(8, IsKey = true, InputMask = ">CC.CC.CC.CC")]
  [PXDefault]
  [PXParent(typeof (RolesInCache.FK.SiteMap))]
  [PXParent(typeof (RolesInCache.FK.PortalMap))]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXDBString(255 /*0xFF*/, IsKey = true, InputMask = "")]
  [PXDefault]
  public virtual string Cachetype
  {
    get => this._Cachetype;
    set => this._Cachetype = value;
  }

  [PXParent(typeof (Select<Roles, Where<Roles.rolename, Equal<Current<RolesInCache.rolename>>>>))]
  [PXDBString(64 /*0x40*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  public virtual string Rolename
  {
    get => this._Rolename;
    set => this._Rolename = value;
  }

  [PXDBString(32 /*0x20*/, IsKey = true, InputMask = "")]
  [PXDefault]
  public virtual string ApplicationName
  {
    get => this._ApplicationName;
    set => this._ApplicationName = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? Accessrights
  {
    get => this._Accessrights;
    set => this._Accessrights = value;
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

  public class PK : 
    PrimaryKeyOf<RolesInCache>.By<RolesInCache.screenID, RolesInCache.cachetype, RolesInCache.rolename, RolesInCache.applicationName>
  {
    public static RolesInCache Find(
      PXGraph graph,
      string screenID,
      string cachetype,
      string rolename,
      string applicationName,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<RolesInCache>.By<RolesInCache.screenID, RolesInCache.cachetype, RolesInCache.rolename, RolesInCache.applicationName>.FindBy(graph, (object) screenID, (object) cachetype, (object) rolename, (object) applicationName, options);
    }
  }

  public static class FK
  {
    public class Role : 
      PrimaryKeyOf<Roles>.By<Roles.applicationName, Roles.rolename>.ForeignKeyOf<RolesInCache>.By<RolesInCache.applicationName, RolesInCache.rolename>
    {
    }

    public class SiteMap : 
      PrimaryKeyOf<SiteMap>.By<SiteMap.screenID>.ForeignKeyOf<RolesInCache>.By<RolesInCache.screenID>
    {
    }

    public class PortalMap : 
      PrimaryKeyOf<PortalMap>.By<PortalMap.screenID>.ForeignKeyOf<RolesInCache>.By<RolesInCache.screenID>
    {
    }
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RolesInCache.screenID>
  {
  }

  public abstract class cachetype : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RolesInCache.cachetype>
  {
  }

  public abstract class rolename : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RolesInCache.rolename>
  {
  }

  public abstract class applicationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RolesInCache.applicationName>
  {
  }

  public abstract class accessrights : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RolesInCache.accessrights>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RolesInCache.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RolesInCache.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RolesInCache.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RolesInCache.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RolesInCache.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RolesInCache.lastModifiedDateTime>
  {
  }
}
