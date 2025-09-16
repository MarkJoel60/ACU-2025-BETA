// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.State.ScreenNavigationAction
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.WorkflowAPI;

#nullable disable
namespace PX.Data.Automation.State;

internal sealed class ScreenNavigationAction : ScreenActionBase
{
  public ScreenNavigationAction(
    string actionId,
    string dataMember,
    string destinationScreenId,
    PXBaseRedirectException.WindowMode windowMode,
    string actionName,
    string icon,
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
    : base(actionId, dataMember, actionName, new PXSpecialButtonType?(), menuFolderType, menuFolder, isTopLevel, disableCondition, hideCondition, before, after, placementInCategory, afterInMenu, category, mapEnableRights, mapViewRights, isLockedOnToolbar, ignoresArchiveDisabling, connotation)
  {
    this.Parameters = new StateMap<ScreenNavigationParameter>();
    this.DestinationScreenId = destinationScreenId;
    this.WindowMode = windowMode;
    this.Icon = icon;
  }

  public string DestinationScreenId { get; }

  public PXBaseRedirectException.WindowMode WindowMode { get; }

  public string Icon { get; }

  public StateMap<ScreenNavigationParameter> Parameters { get; }
}
