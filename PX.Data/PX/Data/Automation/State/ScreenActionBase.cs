// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.State.ScreenActionBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.WorkflowAPI;

#nullable disable
namespace PX.Data.Automation.State;

internal abstract class ScreenActionBase
{
  public string ActionId { get; }

  public string DataMember { get; }

  public string ActionName { get; }

  public PXSpecialButtonType? ActionFolderType { get; }

  public PXSpecialButtonType? MenuFolderType { get; }

  public string MenuFolder { get; }

  public bool? IsTopLevel { get; }

  public string DisableCondition { get; }

  public string HideCondition { get; }

  public string Before { get; }

  public string After { get; }

  public Placement PlacementInCategory { get; }

  public string AfterInMenu { get; }

  public string ActionType { get; set; }

  public PXCacheRights? MapEnableRights { get; set; }

  public PXCacheRights? MapViewRights { get; set; }

  public string Category { get; }

  public bool? IsLockedOnToolbar { get; }

  public bool? IgnoresArchiveDisabling { get; }

  public string Connotation { get; }

  protected ScreenActionBase(
    string actionId,
    string dataMember,
    string actionName,
    PXSpecialButtonType? actionFolderType,
    PXSpecialButtonType? menuFolderType,
    string menuFolder,
    bool? isTopLevel,
    string disableCondition,
    string hideCondition,
    string before,
    string after,
    Placement placementInCategory,
    string afterInMenu,
    string category,
    PXCacheRights? mapEnableRights,
    PXCacheRights? mapViewRights,
    bool? isLockedOnToolbar,
    bool? ignoresArchiveDisabling,
    string connotation)
  {
    this.ActionId = actionId;
    this.DataMember = dataMember;
    this.ActionName = actionName;
    this.ActionFolderType = actionFolderType;
    this.MenuFolderType = menuFolderType;
    this.MenuFolder = menuFolder;
    this.IsTopLevel = isTopLevel;
    this.DisableCondition = disableCondition;
    this.HideCondition = hideCondition;
    this.Before = before;
    this.After = after;
    this.PlacementInCategory = placementInCategory;
    this.AfterInMenu = afterInMenu;
    this.MapViewRights = mapViewRights;
    this.MapEnableRights = mapEnableRights;
    this.Category = category;
    this.IsLockedOnToolbar = isLockedOnToolbar;
    this.IgnoresArchiveDisabling = ignoresArchiveDisabling;
    this.Connotation = connotation;
  }
}
