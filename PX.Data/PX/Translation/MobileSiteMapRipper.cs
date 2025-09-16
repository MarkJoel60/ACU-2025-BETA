// Decompiled with JetBrains decompiler
// Type: PX.Translation.MobileSiteMapRipper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Api.Mobile;
using PX.Api.Mobile.Legacy;
using System.Collections.Generic;

#nullable disable
namespace PX.Translation;

/// <exclude />
public class MobileSiteMapRipper : IPXRipper
{
  protected IMobileWorkspacesCache MobileWorkspacesCache { get; set; }

  protected IMobileSiteMapCache MobileSiteMapCache { get; set; }

  public MobileSiteMapRipper()
  {
    if (!ServiceLocator.IsLocationProviderSet)
      return;
    this.MobileSiteMapCache = ServiceLocator.Current.GetInstance<IMobileSiteMapCache>();
    this.MobileWorkspacesCache = ServiceLocator.Current.GetInstance<IMobileWorkspacesCache>();
  }

  public void CollectResources(ResourceCollection result)
  {
    using (new MobileSiteMapRipperScope())
    {
      SiteMap siteMap = this.MobileSiteMapCache.SiteMap();
      string key = "Mobile SiteMap";
      this.CollectFromItems(key, result, siteMap.Items);
      this.CollectWorkspaces(key, this.MobileWorkspacesCache.Workspaces(), result);
    }
  }

  private void CollectWorkspaces(
    string key,
    Dictionary<string, string> workspaces,
    ResourceCollection result)
  {
    if (workspaces == null)
      return;
    foreach (KeyValuePair<string, string> workspace in workspaces)
    {
      string resKey = $"{key} -> Workspace -> {workspace.Key}";
      result.AddResource(new LocalizationResourceLite(resKey, LocalizationResourceType.MobileSiteMapDisplayName, workspace.Value));
    }
  }

  private void CollectFromItems(string key, ResourceCollection result, SiteMapElement[] items)
  {
    if (items == null)
      return;
    foreach (SiteMapElement siteMapElement in items)
    {
      switch (siteMapElement)
      {
        case Folder folder:
          string resKey1 = $"{key} -> {((SiteMapElement) folder).DisplayName}";
          result.AddResource(new LocalizationResourceLite(resKey1, LocalizationResourceType.MobileSiteMapDisplayName, ((SiteMapElement) folder).DisplayName));
          this.CollectFromItems(key, result, folder.Items);
          break;
        case Screen screen:
          if (!((ScreenBase) screen).IsFilter)
          {
            string resKey2 = $"{key} -> {siteMapElement.DisplayName}";
            result.AddResource(new LocalizationResourceLite(resKey2, LocalizationResourceType.MobileSiteMapDisplayName, siteMapElement.DisplayName));
          }
          this.CollectFormContainers("Mobile SiteMap -> " + ((ScreenBase) screen).Id, result, ((ScreenBase) screen).Containers);
          break;
      }
    }
  }

  private void CollectFormContainers(
    string key,
    ResourceCollection result,
    ScreenContainer[] containers)
  {
    if (containers == null)
      return;
    foreach (ScreenContainer container in containers)
    {
      string str = $"{key} -> {container.Name}";
      if (!string.IsNullOrEmpty(container.DisplayName))
        result.AddResource(new LocalizationResourceLite(str, LocalizationResourceType.MobileSiteMapDisplayName, container.DisplayName));
      this.CollectFromContainerItems(str, result, container.Items);
      this.CollectFromContainerActions(str, result, container.Actions);
    }
  }

  private void CollectFromContainerActions(
    string key,
    ResourceCollection result,
    ScreenContainerAction[] actions)
  {
    if (actions == null)
      return;
    foreach (ScreenContainerAction action in actions)
    {
      string resKey = $"{key} -> {action.Name}";
      if (!string.IsNullOrEmpty(action.DisplayName))
        result.AddResource(new LocalizationResourceLite(resKey, LocalizationResourceType.MobileSiteMapDisplayName, action.DisplayName));
    }
  }

  private void CollectFromContainerItems(
    string key,
    ResourceCollection result,
    ContainerElement[] items)
  {
    if (items == null)
      return;
    foreach (ContainerElement containerElement in items)
    {
      switch (containerElement)
      {
        case ScreenContainerField screenContainerField:
          string str1 = $"{key} -> {((ContainerElement) screenContainerField).Name}";
          if (!string.IsNullOrEmpty(screenContainerField.DisplayName))
            result.AddResource(new LocalizationResourceLite(str1, LocalizationResourceType.MobileSiteMapDisplayName, screenContainerField.DisplayName));
          if (screenContainerField.SelectorContainer != null)
          {
            this.CollectFormContainers(str1, result, new ScreenContainer[1]
            {
              screenContainerField.SelectorContainer
            });
            break;
          }
          break;
        case Group group:
          string str2 = $"{key} -> {group.DisplayName}";
          result.AddResource(new LocalizationResourceLite(str2, LocalizationResourceType.MobileSiteMapDisplayName, group.DisplayName));
          this.CollectFromContainerItems(str2, result, group.Fields);
          break;
        case Layout layout:
          string str3 = $"{key} -> {layout.DisplayName}";
          result.AddResource(new LocalizationResourceLite(str3, LocalizationResourceType.MobileSiteMapDisplayName, layout.DisplayName));
          this.CollectFromContainerItems(str3, result, layout.Fields);
          break;
      }
    }
  }
}
