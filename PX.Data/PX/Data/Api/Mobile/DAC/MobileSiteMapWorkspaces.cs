// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Mobile.DAC.MobileSiteMapWorkspaces
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Api.Mobile.DAC;

public class MobileSiteMapWorkspaces : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false)]
  public Guid? MobileWorkspaceID { get; set; }

  [PXDBString]
  [PXDefault("")]
  public 
  #nullable disable
  string Owner { get; set; }

  [PXDBString(100, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Workspace ID", Visibility = PXUIVisibility.SelectorVisible)]
  public string Name { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Display Name")]
  public string DisplayName { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Icon")]
  [PXDefault("")]
  public string Icon { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "SortOrder")]
  public int? SortOrder { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Active")]
  [PXDefault(true)]
  public bool? IsActive { get; set; }

  public abstract class mobileWorkspaceID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    MobileSiteMapWorkspaces.mobileWorkspaceID>
  {
  }

  public abstract class owner : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MobileSiteMapWorkspaces.owner>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MobileSiteMapWorkspaces.name>
  {
  }

  public abstract class displayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MobileSiteMapWorkspaces.displayName>
  {
  }

  public abstract class icon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MobileSiteMapWorkspaces.icon>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  MobileSiteMapWorkspaces.sortOrder>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  MobileSiteMapWorkspaces.isActive>
  {
  }
}
