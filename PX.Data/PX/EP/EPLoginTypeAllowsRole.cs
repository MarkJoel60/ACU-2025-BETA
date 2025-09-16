// Decompiled with JetBrains decompiler
// Type: PX.EP.EPLoginTypeAllowsRole
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.EP;

/// <exclude />
[PXCacheName("Login Type Allow Role")]
public class EPLoginTypeAllowsRole : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Assigned by Default")]
  [PXDefault(false)]
  public bool? IsDefault { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDBChildIdentity(typeof (EPLoginType.loginTypeID))]
  [PXParent(typeof (Select<EPLoginType, Where<EPLoginType.loginTypeID, Equal<Current<EPLoginTypeAllowsRole.loginTypeID>>>>))]
  [PXDefault(typeof (EPLoginType.loginTypeID))]
  [PXUIField(DisplayName = "User Type")]
  public virtual int? LoginTypeID { get; set; }

  [PXDBString(64 /*0x40*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Role Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (Search<PX.SM.Roles.rolename, Where<Current<EPLoginType.isExternal>, Equal<PX.SM.Roles.guest>, Or<PX.SM.Roles.guest, Equal<PX.Data.True>>>>), DescriptionField = typeof (PX.SM.Roles.descr))]
  [PXParent(typeof (EPLoginTypeAllowsRole.FK.Role))]
  public virtual string Rolename { get; set; }

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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : 
    PrimaryKeyOf<EPLoginTypeAllowsRole>.By<EPLoginTypeAllowsRole.loginTypeID, EPLoginTypeAllowsRole.rolename>
  {
    public static EPLoginTypeAllowsRole Find(
      PXGraph graph,
      int? loginTypeID,
      string rolename,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<EPLoginTypeAllowsRole>.By<EPLoginTypeAllowsRole.loginTypeID, EPLoginTypeAllowsRole.rolename>.FindBy(graph, (object) loginTypeID, (object) rolename, options);
    }
  }

  public static class FK
  {
    public class LoginType : 
      PrimaryKeyOf<EPLoginType>.By<EPLoginType.loginTypeID>.ForeignKeyOf<EPLoginTypeAllowsRole>.By<EPLoginTypeAllowsRole.loginTypeID>
    {
    }

    public class Role : 
      PrimaryKeyOf<PX.SM.Roles>.By<PX.SM.Roles.rolename>.ForeignKeyOf<EPLoginTypeAllowsRole>.By<EPLoginTypeAllowsRole.rolename>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPLoginTypeAllowsRole.selected>
  {
  }

  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPLoginTypeAllowsRole.isDefault>
  {
  }

  public abstract class loginTypeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPLoginTypeAllowsRole.loginTypeID>
  {
  }

  public abstract class rolename : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPLoginTypeAllowsRole.rolename>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPLoginTypeAllowsRole.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPLoginTypeAllowsRole.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPLoginTypeAllowsRole.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EPLoginTypeAllowsRole.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPLoginTypeAllowsRole.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPLoginTypeAllowsRole.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EPLoginTypeAllowsRole.lastModifiedDateTime>
  {
  }
}
