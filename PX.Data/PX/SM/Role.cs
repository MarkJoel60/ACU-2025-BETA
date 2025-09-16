// Decompiled with JetBrains decompiler
// Type: PX.SM.Role
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
public class Role : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private Guid? _NodeID;
  private int? _Level;
  private 
  #nullable disable
  string _ScreenID;
  private string _CacheName;
  private string _MemberName;
  private string _RoleName;
  private string _RoleDescr;
  private int? _RoleRight;
  private string _OrderBy;
  private Guid? _CreatedByID;
  private string _CreatedByScreenID;
  private System.DateTime? _CreatedDateTime;
  private Guid? _LastModifiedByID;
  private string _LastModifiedByScreenID;
  private System.DateTime? _LastModifiedDateTime;

  [PXGuid]
  [PXUnboundKey]
  public Guid? NodeID
  {
    get => this._NodeID;
    set => this._NodeID = value;
  }

  [PXInt]
  public int? Level
  {
    get => this._Level;
    set => this._Level = value;
  }

  [PXUIField(DisplayName = "", IsReadOnly = true)]
  [PXImage]
  public string DescriptionIcon { get; set; }

  [PXString]
  [PXUnboundKey]
  public string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXString]
  [PXUnboundKey]
  public string CacheName
  {
    get => this._CacheName;
    set => this._CacheName = value;
  }

  [PXString]
  [PXUnboundKey]
  public string MemberName
  {
    get => this._MemberName;
    set => this._MemberName = value;
  }

  [PXDBString(64 /*0x40*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Role", Enabled = false)]
  public string RoleName
  {
    get => this._RoleName;
    set => this._RoleName = value;
  }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  [PXUnboundKey]
  public string RoleDescr
  {
    get => this._RoleDescr;
    set => this._RoleDescr = value;
  }

  [PXDefault(0)]
  [PXRoleRight]
  [PXDBInt]
  [PXUIField(DisplayName = "Access Rights")]
  public int? RoleRight
  {
    get => this._RoleRight;
    set => this._RoleRight = value;
  }

  [PXString]
  public string OrderBy
  {
    get => this._OrderBy;
    set => this._OrderBy = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Applied to Nested", Enabled = false)]
  public bool? InheritedByChildren { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Guest Role", Enabled = false)]
  public bool? Guest { get; set; }

  [PXDBCreatedByID]
  public Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public abstract class nodeID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Role.nodeID>
  {
  }

  public abstract class level : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Role.level>
  {
  }

  public abstract class descriptionIcon : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Role.screenID>
  {
  }

  public abstract class cacheName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Role.cacheName>
  {
  }

  public abstract class memberName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Role.memberName>
  {
  }

  public abstract class roleName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Role.roleName>
  {
  }

  public abstract class roleDescr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Role.roleDescr>
  {
  }

  public abstract class roleRight : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Role.roleRight>
  {
  }

  public abstract class orderBy : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Role.orderBy>
  {
  }

  public abstract class inheritedByChildren : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Role.inheritedByChildren>
  {
  }

  public abstract class guest : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Role.guest>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Role.createdByID>
  {
  }

  public abstract class createdByScreenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Role.createdByScreenID>
  {
  }

  public abstract class createdDateTime : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  Role.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Role.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Role.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    Role.lastModifiedDateTime>
  {
  }
}
