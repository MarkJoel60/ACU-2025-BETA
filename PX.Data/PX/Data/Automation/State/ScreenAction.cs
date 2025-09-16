// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.State.ScreenAction
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.WorkflowAPI;

#nullable disable
namespace PX.Data.Automation.State;

internal sealed class ScreenAction : ScreenActionBase
{
  public readonly string Method;

  public ScreenActionExtraData ExtraData { get; }

  public ScreenAction(
    string actionId,
    string dataMember,
    string method,
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
    string connotation,
    ScreenActionExtraData extraData = null)
    : base(actionId, dataMember, actionName, actionFolderType, menuFolderType, menuFolder, isTopLevel, disableCondition, hideCondition, before, after, placementInCategory, afterInMenu, category, mapEnableRights, mapViewRights, isLockedOnToolbar, ignoresArchiveDisabling, connotation)
  {
    this.Method = method;
    this.ExtraData = extraData;
  }
}
