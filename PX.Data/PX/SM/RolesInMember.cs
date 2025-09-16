// Decompiled with JetBrains decompiler
// Type: PX.SM.RolesInMember
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

[PXCacheName("Roles In Member")]
public class RolesInMember : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ScreenID;
  protected string _Cachetype;
  protected string _Membername;
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
  [PXParent(typeof (RolesInMember.FK.SiteMap))]
  [PXParent(typeof (RolesInMember.FK.PortalMap))]
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

  [PXDBString(255 /*0xFF*/, IsKey = true, InputMask = "")]
  [PXDefault]
  public virtual string Membername
  {
    get => this._Membername;
    set => this._Membername = value;
  }

  [PXParent(typeof (Select<Roles, Where<Roles.rolename, Equal<Current<RolesInMember.rolename>>>>))]
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
    PrimaryKeyOf<RolesInMember>.By<RolesInMember.screenID, RolesInMember.cachetype, RolesInMember.membername, RolesInMember.rolename, RolesInMember.applicationName>
  {
    public static RolesInMember Find(
      PXGraph graph,
      string screenID,
      string cachetype,
      string membername,
      string rolename,
      string applicationName,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<RolesInMember>.By<RolesInMember.screenID, RolesInMember.cachetype, RolesInMember.membername, RolesInMember.rolename, RolesInMember.applicationName>.FindBy(graph, (object) screenID, (object) cachetype, (object) membername, (object) rolename, (object) applicationName, options);
    }
  }

  public static class FK
  {
    public class Role : 
      PrimaryKeyOf<Roles>.By<Roles.applicationName, Roles.rolename>.ForeignKeyOf<RolesInMember>.By<RolesInMember.applicationName, RolesInMember.rolename>
    {
    }

    public class SiteMap : 
      PrimaryKeyOf<SiteMap>.By<SiteMap.screenID>.ForeignKeyOf<RolesInMember>.By<RolesInMember.screenID>
    {
    }

    public class PortalMap : 
      PrimaryKeyOf<PortalMap>.By<PortalMap.screenID>.ForeignKeyOf<RolesInMember>.By<RolesInMember.screenID>
    {
    }
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RolesInMember.screenID>
  {
  }

  public abstract class cachetype : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RolesInMember.cachetype>
  {
  }

  public abstract class membername : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RolesInMember.membername>
  {
  }

  public abstract class rolename : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RolesInMember.rolename>
  {
  }

  public abstract class applicationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RolesInMember.applicationName>
  {
  }

  public abstract class accessrights : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RolesInMember.accessrights>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RolesInMember.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RolesInMember.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RolesInMember.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RolesInMember.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RolesInMember.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RolesInMember.lastModifiedDateTime>
  {
  }
}
