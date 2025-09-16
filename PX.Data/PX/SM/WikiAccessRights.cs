// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiAccessRights
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
public class WikiAccessRights : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _PageID;
  protected 
  #nullable disable
  string _RoleName;
  protected string _ApplicationName;
  protected short? _AccessRights;

  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? PageID
  {
    get => this._PageID;
    set => this._PageID = value;
  }

  [PXDBString(IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Role Name")]
  public virtual string RoleName
  {
    get => this._RoleName;
    set => this._RoleName = value;
  }

  [PXDBString(IsKey = true, InputMask = "")]
  [PXDefault]
  public string ApplicationName
  {
    get => this._ApplicationName;
    set => this._ApplicationName = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Access Rights")]
  [PXIntList(new int[] {-1, 0, 1, 2, 3, 4, 5}, new string[] {"Not Set", "Revoked", "View Only", "Edit", "Insert", "Publish", "Delete"})]
  public virtual short? AccessRights
  {
    get => this._AccessRights;
    set => this._AccessRights = value;
  }

  public abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiAccessRights.pageID>
  {
  }

  public abstract class roleName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiAccessRights.roleName>
  {
  }

  public abstract class applicationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WikiAccessRights.applicationName>
  {
  }

  public abstract class accessRights : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  WikiAccessRights.accessRights>
  {
  }
}
