// Decompiled with JetBrains decompiler
// Type: PX.SM.RolesFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.SM;

[PXCacheName("Role")]
public class RolesFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ApplicationName;
  protected string _Rolename;
  protected string _Descr;
  protected bool? _Isinherited;

  [PXString(32 /*0x20*/)]
  [PXDefault("/")]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual string ApplicationName
  {
    get => this._ApplicationName;
    set => this._ApplicationName = value;
  }

  [PXString(64 /*0x40*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Role Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (Roles.rolename), DescriptionField = typeof (Roles.descr))]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<RolesFilter.rolename>.IsRelatedTo<Roles.rolename>))]
  public virtual string Rolename
  {
    get => this._Rolename;
    set => this._Rolename = value;
  }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Role Description", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Apply rights to nested nodes")]
  [PXDefault(true)]
  public virtual bool? Isinherited
  {
    get => this._Isinherited;
    set => this._Isinherited = value;
  }

  public abstract class applicationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RolesFilter.applicationName>
  {
  }

  public abstract class rolename : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RolesFilter.rolename>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RolesFilter.descr>
  {
  }

  public abstract class isinherited : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RolesFilter.isinherited>
  {
  }
}
