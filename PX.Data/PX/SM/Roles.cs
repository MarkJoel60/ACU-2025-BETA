// Decompiled with JetBrains decompiler
// Type: PX.SM.Roles
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Collections;

#nullable enable
namespace PX.SM;

[PXCacheName("Role")]
[PXPrimaryGraph(typeof (RoleAccess))]
public class Roles : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ApplicationName;
  protected string _Rolename;
  protected string _Descr;
  protected bool? _Guest;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  public const string DefaultApplicationName = "/";

  [PXDBString(32 /*0x20*/, IsKey = true, InputMask = "")]
  [PXDefault("/")]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual string ApplicationName
  {
    get => this._ApplicationName;
    set => this._ApplicationName = value;
  }

  [PXDBString(64 /*0x40*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Role Name", Visibility = PXUIVisibility.SelectorVisible)]
  [Roles.RolesSelector]
  [PXReferentialIntegrityCheck]
  public virtual string Rolename
  {
    get => this._Rolename;
    set => this._Rolename = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Role Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Guest Role", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault(false)]
  public virtual bool? Guest
  {
    get => this._Guest;
    set => this._Guest = value;
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

  public class PK : PrimaryKeyOf<Roles>.By<Roles.applicationName, Roles.rolename>
  {
    public static Roles Find(
      PXGraph graph,
      string applicationName,
      string rolename,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<Roles>.By<Roles.applicationName, Roles.rolename>.FindBy(graph, (object) applicationName, (object) rolename, options);
    }
  }

  public class UK : PrimaryKeyOf<Roles>.By<Roles.rolename>
  {
    public static Roles Find(PXGraph graph, string rolename, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<Roles>.By<Roles.rolename>.FindBy(graph, (object) rolename, options);
    }
  }

  public class RolesSelectorAttribute : PXCustomSelectorAttribute
  {
    public RolesSelectorAttribute()
      : base(typeof (Roles.rolename))
    {
      this.DescriptionField = typeof (Roles.descr);
    }

    public IEnumerable GetRecords()
    {
      foreach (PXResult<Roles> pxResult in PXSelectBase<Roles, PXSelectReadonly<Roles>.Config>.Select(this._Graph))
      {
        Roles record = (Roles) pxResult;
        if (PXAccess.IsRoleEnabled(record.Rolename))
          yield return (object) record;
      }
    }
  }

  public abstract class applicationName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Roles.applicationName>
  {
  }

  public abstract class rolename : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Roles.rolename>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Roles.descr>
  {
  }

  public abstract class guest : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Roles.guest>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Roles.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Roles.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    Roles.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Roles.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Roles.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    Roles.lastModifiedDateTime>
  {
  }

  public sealed class defaultApplicationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Roles.defaultApplicationName>
  {
    public defaultApplicationName()
      : base("/")
    {
    }
  }
}
