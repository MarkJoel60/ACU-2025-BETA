// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Mobile.DAC.MobileSiteMapWorkspaceWidgets
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data.Api.Mobile.DAC;

public class MobileSiteMapWorkspaceWidgets : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString]
  [PXDefault(typeof (MobileSiteMapWorkspaces.owner))]
  [PXParent(typeof (Select<MobileSiteMapWorkspaces, Where<MobileSiteMapWorkspaces.owner, Equal<Current<MobileSiteMapWorkspaceWidgets.workspaceOwner>>, And<MobileSiteMapWorkspaces.name, Equal<Current<MobileSiteMapWorkspaceWidgets.workspaceName>>>>>))]
  public 
  #nullable disable
  string WorkspaceOwner { get; set; }

  [PXDBString]
  [PXDefault(typeof (MobileSiteMapWorkspaces.name))]
  [PXParent(typeof (Select<MobileSiteMapWorkspaces, Where<MobileSiteMapWorkspaces.owner, Equal<Current<MobileSiteMapWorkspaceWidgets.workspaceOwner>>, And<MobileSiteMapWorkspaces.name, Equal<Current<MobileSiteMapWorkspaceWidgets.workspaceName>>>>>))]
  public string WorkspaceName { get; set; }

  [PXDBIdentity]
  [PXDefault(0)]
  public int? DashboardID { get; set; }

  [PXDBIdentity(IsKey = true)]
  [PXDefault]
  public int? WidgetID { get; set; }

  [PXDBString]
  public string Owner { get; set; }

  public abstract class workspaceOwner : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MobileSiteMapWorkspaceWidgets.workspaceOwner>
  {
  }

  public abstract class workspaceName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MobileSiteMapWorkspaceWidgets.workspaceName>
  {
  }

  public abstract class dashboardID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MobileSiteMapWorkspaceWidgets.dashboardID>
  {
  }

  public abstract class widgetID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MobileSiteMapWorkspaceWidgets.widgetID>
  {
  }

  public abstract class owner : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MobileSiteMapWorkspaceWidgets.owner>
  {
  }
}
