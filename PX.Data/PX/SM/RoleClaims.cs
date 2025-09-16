// Decompiled with JetBrains decompiler
// Type: PX.SM.RoleClaims
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.SM;

[PXCacheName("Role Claims")]
public class RoleClaims : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(64 /*0x40*/, IsKey = true, IsUnicode = true, InputMask = "")]
  public virtual 
  #nullable disable
  string Role { get; set; }

  [PXDBString(255 /*0xFF*/, IsKey = true, IsUnicode = true, IsFixed = false, InputMask = "")]
  [PXUIField(DisplayName = "Group ID")]
  public virtual string GroupID { get; set; }

  public class PK : PrimaryKeyOf<RoleClaims>.By<RoleClaims.role, RoleClaims.groupID>
  {
    public static RoleClaims Find(
      PXGraph graph,
      string role,
      string groupID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<RoleClaims>.By<RoleClaims.role, RoleClaims.groupID>.FindBy(graph, (object) role, (object) groupID, options);
    }
  }

  public static class FK
  {
    public class Role : 
      PrimaryKeyOf<Roles>.By<Roles.rolename>.ForeignKeyOf<RoleClaims>.By<RoleClaims.role>
    {
    }

    public class ActiveDirectoryGroup : 
      PrimaryKeyOf<RoleClaims>.By<ActiveDirectoryGroup.groupID>.ForeignKeyOf<RoleClaims>.By<RoleClaims.groupID>
    {
    }
  }

  public abstract class role : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RoleClaims.role>
  {
  }

  public abstract class groupID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RoleClaims.groupID>
  {
  }
}
