// Decompiled with JetBrains decompiler
// Type: PX.SM.UsersInRoles
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.SM;

/// <exclude />
[PXCacheName("Users In Roles")]
[DebuggerDisplay("Username = {Username}, Rolename = {Rolename}")]
public class UsersInRoles : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Username;
  protected string _Rolename;
  protected string _ApplicationName;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  [PXDBString(256 /*0x0100*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault(typeof (Users.username))]
  [PXUIField(DisplayName = "Username")]
  [PXParent(typeof (Select<Users, Where<Users.username, Equal<Current<UsersInRoles.username>>>>))]
  [PXSelector(typeof (Search<Users.username, Where2<Where<Users.isHidden, Equal<False>>, PX.Data.And<Where2<Where<Users.source, Equal<PXUsersSourceListAttribute.application>, Or<Users.overrideADRoles, Equal<PX.Data.True>>>, PX.Data.And<Where<Current<Roles.guest>, Equal<PX.Data.True>, Or<Users.guest, NotEqual<PX.Data.True>>>>>>>>), DescriptionField = typeof (Users.comment), DirtyRead = true)]
  public virtual string Username
  {
    get => this._Username;
    set => this._Username = value;
  }

  [PXDBString(64 /*0x40*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault(typeof (Roles.rolename))]
  [PXUIField(DisplayName = "Role Name")]
  [PXParent(typeof (Select<Roles, Where<Roles.rolename, Equal<Current<UsersInRoles.rolename>>>>))]
  [PXSelector(typeof (Search<Roles.rolename, Where<Current<Users.guest>, Equal<False>, Or<Roles.guest, Equal<PX.Data.True>>>>), DescriptionField = typeof (Roles.descr), DirtyRead = true)]
  public virtual string Rolename
  {
    get => this._Rolename;
    set => this._Rolename = value;
  }

  [PXDBString(32 /*0x20*/, IsKey = true, InputMask = "")]
  [PXDefault("/")]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual string ApplicationName
  {
    get => this._ApplicationName;
    set => this._ApplicationName = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Inherited", Enabled = false, IsReadOnly = true)]
  public virtual bool? Inherited { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Display Name", Enabled = false)]
  public virtual string DisplayName { get; set; }

  [PXString(1, IsFixed = true, IsUnicode = false)]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  [Users.state.List]
  public virtual string State { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Domain", Enabled = false)]
  public virtual string Domain { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Comment", Enabled = false)]
  public virtual string Comment { get; set; }

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
    PrimaryKeyOf<UsersInRoles>.By<UsersInRoles.username, UsersInRoles.rolename, UsersInRoles.applicationName>
  {
    public static UsersInRoles Find(
      PXGraph graph,
      string username,
      string rolename,
      string applicationName,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<UsersInRoles>.By<UsersInRoles.username, UsersInRoles.rolename, UsersInRoles.applicationName>.FindBy(graph, (object) username, (object) rolename, (object) applicationName, options);
    }
  }

  public static class FK
  {
    public class User : 
      PrimaryKeyOf<Users>.By<Users.username>.ForeignKeyOf<UsersInRoles>.By<UsersInRoles.username>
    {
    }

    public class Role : 
      PrimaryKeyOf<Roles>.By<Roles.applicationName, Roles.rolename>.ForeignKeyOf<UsersInRoles>.By<UsersInRoles.applicationName, UsersInRoles.rolename>
    {
    }
  }

  public abstract class username : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UsersInRoles.username>
  {
  }

  /// <exclude />
  public abstract class rolename : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UsersInRoles.rolename>
  {
  }

  /// <exclude />
  public abstract class applicationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UsersInRoles.applicationName>
  {
  }

  /// <exclude />
  public abstract class inherited : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UsersInRoles.inherited>
  {
  }

  /// <exclude />
  public abstract class displayName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UsersInRoles.displayName>
  {
  }

  /// <exclude />
  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UsersInRoles.state>
  {
  }

  /// <exclude />
  public abstract class domain : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UsersInRoles.domain>
  {
  }

  /// <exclude />
  public abstract class comment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UsersInRoles.comment>
  {
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UsersInRoles.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UsersInRoles.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    UsersInRoles.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    UsersInRoles.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UsersInRoles.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    UsersInRoles.lastModifiedDateTime>
  {
  }
}
