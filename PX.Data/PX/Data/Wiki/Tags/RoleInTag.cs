// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Tags.RoleInTag
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Data.Wiki.Tags;

/// <summary>A relation between a tag and a user role.</summary>
[PXCacheName("Roles In Tag")]
[PXPrimaryGraph(typeof (TagMaint))]
public class RoleInTag : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The unique identifier of the tag.</summary>
  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  public virtual Guid? TagID { get; set; }

  /// <summary>The string identifier of the user role.</summary>
  [PXDBString(64 /*0x40*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXParent(typeof (Select<PX.SM.Roles, Where<PX.SM.Roles.rolename, Equal<Current<RoleInTag.rolename>>>>))]
  public virtual string? Rolename { get; set; }

  /// <summary>Access rights of the role for the tag.</summary>
  [PXDBShort]
  [PXDefault(0)]
  [TagAccessLevelList]
  public virtual short? AccessRights { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string? CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false)]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string? LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false)]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  public class PK : PrimaryKeyOf<RoleInTag>.By<RoleInTag.tagID, RoleInTag.rolename>
  {
    public static RoleInTag Find(
      PXGraph graph,
      Guid tagID,
      string rolename,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<RoleInTag>.By<RoleInTag.tagID, RoleInTag.rolename>.FindBy(graph, (object) tagID, (object) rolename, options);
    }
  }

  public abstract class tagID : BqlType<IBqlGuid, Guid>.Field<RoleInTag.tagID>
  {
  }

  public abstract class rolename : BqlType<IBqlString, string>.Field<RoleInTag.rolename>
  {
  }

  public abstract class accessRights : BqlType<IBqlShort, short>.Field<RoleInTag.accessRights>
  {
  }

  public abstract class createdByID : BqlType<IBqlGuid, Guid>.Field<RoleInTag.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<IBqlString, string>.Field<RoleInTag.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<RoleInTag.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<IBqlGuid, Guid>.Field<RoleInTag.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<IBqlString, string>.Field<RoleInTag.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<RoleInTag.lastModifiedDateTime>
  {
  }
}
