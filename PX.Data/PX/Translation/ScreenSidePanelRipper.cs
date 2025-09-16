// Decompiled with JetBrains decompiler
// Type: PX.Translation.ScreenSidePanelRipper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Automation;
using PX.Data.LocalizationKeyGenerators;
using PX.SM;
using System;
using System.Linq;

#nullable disable
namespace PX.Translation;

internal sealed class ScreenSidePanelRipper : IPXRipper
{
  private readonly IScreenToGraphWorkflowMappingService _screenToGraphWorkflowMappingService;

  public ScreenSidePanelRipper(
    IScreenToGraphWorkflowMappingService screenToGraphWorkflowMappingService)
  {
    this._screenToGraphWorkflowMappingService = screenToGraphWorkflowMappingService;
  }

  void IPXRipper.CollectResources(ResourceCollection result)
  {
    foreach (AUScreenNavigationActionState navigationActionState in PXSystemWorkflows.SelectTable<AUScreenNavigationActionState>().Where<AUScreenNavigationActionState>((Func<AUScreenNavigationActionState, bool>) (a =>
    {
      int? menuFolderType = a.MenuFolderType;
      int num = 23;
      return menuFolderType.GetValueOrDefault() == num & menuFolderType.HasValue;
    })).ToArray<AUScreenNavigationActionState>())
    {
      string nameLocalizationKey = PXUIFieldKeyGenerator.GetActionNameLocalizationKey(this._screenToGraphWorkflowMappingService.GetGraphTypeByScreenID(navigationActionState.ScreenID));
      result.AddResource(new LocalizationResourceLite(nameLocalizationKey, LocalizationResourceType.SidePanel, navigationActionState.DisplayName));
    }
  }
}
