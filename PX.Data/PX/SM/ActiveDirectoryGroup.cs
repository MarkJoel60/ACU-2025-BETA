// Decompiled with JetBrains decompiler
// Type: PX.SM.ActiveDirectoryGroup
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.SM;

[PXCacheName("Active Directory Group")]
public class ActiveDirectoryGroup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsKey = true, IsUnicode = true)]
  [PXUIField(Visible = false, Visibility = PXUIVisibility.Invisible)]
  public virtual 
  #nullable disable
  string GroupID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Name", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Name { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Domain", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Domain { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Description", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description { get; set; }

  public class PK : PrimaryKeyOf<RoleClaims>.By<ActiveDirectoryGroup.groupID>
  {
    public static RoleClaims Find(PXGraph graph, string groupID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<RoleClaims>.By<ActiveDirectoryGroup.groupID>.FindBy(graph, (object) groupID, options);
    }
  }

  public abstract class groupID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ActiveDirectoryGroup.groupID>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ActiveDirectoryGroup.name>
  {
  }

  public abstract class domain : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ActiveDirectoryGroup.domain>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ActiveDirectoryGroup.description>
  {
  }
}
