// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiAccessRoles
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
public class WikiAccessRoles : WikiAccessRights
{
  protected bool? _Guest;
  protected short? _ParentAccessRights;

  [PXDBString(IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXSelector(typeof (Roles.rolename))]
  [PXUIField(DisplayName = "Role Name")]
  public override 
  #nullable disable
  string RoleName
  {
    get => this._RoleName;
    set => this._RoleName = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Guest Role", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault(false)]
  public virtual bool? Guest
  {
    get => this._Guest;
    set => this._Guest = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Access Rights")]
  [PXIntList(new int[] {-1, 0, 1, 2, 3, 4, 5}, new string[] {"Inherited", "Revoked", "View Only", "Edit", "Insert", "Publish", "Delete"})]
  public override short? AccessRights
  {
    get => this._AccessRights;
    set => this._AccessRights = value;
  }

  [PXShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Parent Access Rights", Enabled = false)]
  [PXIntList(new int[] {-1, 0, 1, 2, 3, 4, 5}, new string[] {"Not Set", "Revoked", "View Only", "Edit", "Insert", "Publish", "Delete"})]
  public virtual short? ParentAccessRights
  {
    get => this._ParentAccessRights;
    set => this._ParentAccessRights = value;
  }

  public abstract class guest : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiAccessRoles.guest>
  {
  }

  public abstract class parentAccessRights : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    WikiAccessRoles.parentAccessRights>
  {
  }
}
