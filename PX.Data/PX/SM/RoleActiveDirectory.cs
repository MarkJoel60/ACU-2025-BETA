// Decompiled with JetBrains decompiler
// Type: PX.SM.RoleActiveDirectory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.SM;

[PXCacheName("Role Active Directory")]
public class RoleActiveDirectory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXParent(typeof (Select<Roles, Where<Roles.rolename, Equal<Current<RoleActiveDirectory.role>>>>))]
  [PXDBString(64 /*0x40*/, IsKey = true, IsUnicode = true, InputMask = "")]
  public virtual 
  #nullable disable
  string Role { get; set; }

  [PXDBString(255 /*0xFF*/, IsKey = true, IsUnicode = true)]
  [ADGroupSelector]
  [PXUIField(DisplayName = "Group ID")]
  public virtual string GroupID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Name", Enabled = false)]
  public virtual string GroupName { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Domain", Enabled = false)]
  public virtual string GroupDomain { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public virtual string GroupDescription { get; set; }

  public class PK : 
    PrimaryKeyOf<RoleActiveDirectory>.By<RoleActiveDirectory.role, RoleActiveDirectory.groupID>
  {
    public static RoleActiveDirectory Find(
      PXGraph graph,
      string role,
      string groupID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<RoleActiveDirectory>.By<RoleActiveDirectory.role, RoleActiveDirectory.groupID>.FindBy(graph, (object) role, (object) groupID, options);
    }
  }

  public static class FK
  {
    public class Role : 
      PrimaryKeyOf<Roles>.By<Roles.rolename>.ForeignKeyOf<RoleActiveDirectory>.By<RoleActiveDirectory.role>
    {
    }

    public class ActiveDirectoryGroup : 
      PrimaryKeyOf<RoleClaims>.By<ActiveDirectoryGroup.groupID>.ForeignKeyOf<RoleActiveDirectory>.By<RoleActiveDirectory.groupID>
    {
    }
  }

  public abstract class role : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RoleActiveDirectory.role>
  {
  }

  public abstract class groupID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RoleActiveDirectory.groupID>
  {
  }

  public abstract class groupName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RoleActiveDirectory.groupName>
  {
  }

  public abstract class groupDomain : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RoleActiveDirectory.groupDomain>
  {
  }

  public abstract class groupDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RoleActiveDirectory.groupDescription>
  {
  }
}
