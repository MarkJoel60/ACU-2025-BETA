// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSiteMap
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using CommonServiceLocator;
using PX.Common;
using PX.Data.Description;
using PX.Data.GenericInquiry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable enable
namespace PX.Data;

/// <summary>
/// Helper class to access some site-map-related functionality
/// </summary>
public static class PXSiteMap
{
  private static 
  #nullable disable
  PXSiteMapProvider _provider;
  private static PXWikiProvider _wikiProvider;
  internal const string ForceUISlotName = "ForceUI";
  internal const string HiddenScreenId = "HD000000";
  internal const string RootNodeScreenId = "00000000";
  internal const string ReportLauncherPage = "reportlauncher.aspx";
  internal const string ArmReportLauncherPage = "rmlauncher.aspx";
  internal const string WikiPage = "wiki.aspx";
  private const string PivotBaseUrl = "/pivot/pivot.aspx";
  [PXInternalUseOnly]
  public const string DefaultFrame = "~/Frames/Default.aspx";
  internal const string NewUiDashboardUrl = "~/Scripts/Screens/Dashboard.html";
  [PXInternalUseOnly]
  public const string DefaultScreenID = "00000000";

  /// <summary>Current node of the active site map provider</summary>
  /// <remarks>
  /// It might be main or wiki site map node.
  /// It's better to use specific provider if you need to get current node of the specific provider
  /// </remarks>
  public static PXSiteMapNode CurrentNode
  {
    get
    {
      try
      {
        return PXSiteMap.Provider.CurrentNode ?? PXSiteMap.WikiProvider.CurrentNode ?? PXSiteMap.Provider.RootNode;
      }
      catch
      {
        return PXSiteMap.Provider.RootNode;
      }
    }
  }

  /// <summary>Root node of the active site map provider</summary>
  /// <remarks>
  /// It might be main or wiki site map node.
  /// It's better to use specific provider if you need to get current node of the specific provider
  /// </remarks>
  [PXInternalUseOnly]
  [Obsolete("Use RootNode from the specific provider")]
  public static PXSiteMapNode RootNode
  {
    get
    {
      try
      {
        return PXSiteMap.Provider.RootNode ?? PXSiteMap.WikiProvider.RootNode;
      }
      catch
      {
        return PXSiteMap.Provider.RootNode;
      }
    }
  }

  /// <summary>Screen identifier of the current site map node</summary>
  public static string CurrentScreenID => PXSiteMap.CurrentNode?.ScreenID;

  /// <summary>
  /// Indicates whether the portal or the main instance is running
  /// </summary>
  public static bool IsPortal { get; private set; }

  /// <summary>Active site map provider instance</summary>
  public static PXSiteMapProvider Provider
  {
    get
    {
      if (PXSiteMap._provider != null)
        return PXSiteMap._provider;
      if (PXHostingEnvironment.IsHosted)
        throw new InvalidOperationException("PXSiteMapProvider should have been initialized by now");
      return ServiceLocator.Current.GetInstance<PXSiteMapProvider>();
    }
  }

  /// <summary>Active wiki site map provider instance</summary>
  public static PXWikiProvider WikiProvider
  {
    get
    {
      if (PXSiteMap._wikiProvider != null)
        return PXSiteMap._wikiProvider;
      if (PXHostingEnvironment.IsHosted)
        throw new InvalidOperationException("PXWikiProvider should have been initialized by now");
      return ServiceLocator.Current.GetInstance<PXWikiProvider>();
    }
  }

  /// <summary>
  /// Checks whether the specific URL is a generic inquiry URL
  /// </summary>
  /// <param name="url">The URL to be checked</param>
  /// <returns><see langword="true" /> if the URL is a generic inquiry URL, otherwise <see langword="false" /></returns>
  [PXInternalUseOnly]
  public static bool IsGenericInquiry(string url)
  {
    return !string.IsNullOrEmpty(url) && UrlConstants.Path.GenericInquiryPaths.Any<string>((Func<string, bool>) (path => url.IndexOf(path, StringComparison.InvariantCultureIgnoreCase) > 0));
  }

  /// <summary>Checks whether the specific URL is a pivot table URL</summary>
  /// <param name="url">The URL to be checked</param>
  /// <returns><see langword="true" /> if the URL is a pivot table URL, otherwise <see langword="false" /></returns>
  internal static bool IsPivot(string url)
  {
    return !string.IsNullOrEmpty(url) && url.ToLowerInvariant().IndexOf("/pivot/pivot.aspx", StringComparison.InvariantCultureIgnoreCase) > 0;
  }

  /// <summary>Checks whether the specific URL is a report URL</summary>
  /// <param name="url">The URL to be checked</param>
  /// <returns><see langword="true" /> if the URL is a report URL, otherwise <see langword="false" /></returns>
  public static bool IsReport(string url)
  {
    if (string.IsNullOrEmpty(url))
      return false;
    return url.ToLowerInvariant().Contains("reportlauncher.aspx") || PXSiteMap.IsNewUiReport(url);
  }

  private static bool IsNewUiReport(string url)
  {
    if (!Str.Contains(url, "/Scripts/Screens/ReportScreen.html", StringComparison.OrdinalIgnoreCase))
      return false;
    return Str.Contains(url, "IsARm=False", StringComparison.OrdinalIgnoreCase) || !Str.Contains(url, "IsARm", StringComparison.OrdinalIgnoreCase);
  }

  /// <summary>Checks whether the specific URL is an ARM report URL</summary>
  /// <param name="url">The URL to be checked</param>
  /// <returns><see langword="true" /> if the URL is an ARM report URL, otherwise <see langword="false" /></returns>
  public static bool IsARmReport(string url)
  {
    if (string.IsNullOrEmpty(url))
      return false;
    return url.ToLowerInvariant().Contains("rmlauncher.aspx") || PXSiteMap.IsNewUiARmReport(url);
  }

  private static bool IsNewUiARmReport(string url)
  {
    return Str.Contains(url, "/Scripts/Screens/ReportScreen.html", StringComparison.OrdinalIgnoreCase) && Str.Contains(url, "IsARm=True", StringComparison.OrdinalIgnoreCase);
  }

  /// <summary>Checks whether the specific node is a dashboard URL</summary>
  /// <param name="node">The node to be checked</param>
  /// <returns><see langword="true" /> if the node is a dashboard URL, otherwise <see langword="false" /></returns>
  [Obsolete("Use node.IsDashboard() instead")]
  public static bool IsDashboard(PXSiteMapNode node) => PXSiteMap.IsDashboard(node?.Url);

  /// <summary>Checks whether the specific URL is a dashboard URL</summary>
  /// <param name="url">The URL to be checked</param>
  /// <returns><see langword="true" /> if the URL is a dashboard URL, otherwise <see langword="false" /></returns>
  internal static bool IsDashboard(string url)
  {
    if (string.IsNullOrEmpty(url))
      return false;
    return url.StartsWith("~/Frames/Default.aspx", StringComparison.OrdinalIgnoreCase) || url.StartsWith("~/Scripts/Screens/Dashboard.html", StringComparison.OrdinalIgnoreCase);
  }

  [PXInternalUseOnly]
  public class ScreenInfo
  {
    /// <summary>Actions for primary view</summary>
    public readonly PXSiteMap.ScreenInfo.Action[] Actions;
    /// <summary>Actions for all views</summary>
    public readonly PXSiteMap.ScreenInfo.Action[] AllActions;
    public readonly Dictionary<string, string[]> Caches;
    public readonly Dictionary<string, PXViewDescription> Containers;
    public readonly string GraphName;
    public readonly string PrimaryView;
    public readonly string PrimaryViewTypeName;
    public readonly IReadOnlyCollection<byte> SourceFileHash;
    public readonly System.DateTime? SourceFileLastModificationTimeUtc;
    public readonly string TimeStamp;
    public readonly Dictionary<string, string[]> Views;
    public bool HasWorkflow;
    public bool IsNewUI;

    public ScreenInfo(
      string graphName,
      string primaryView,
      string primaryViewTypeName,
      PXSiteMap.ScreenInfo.Action[] actions,
      PXSiteMap.ScreenInfo.Action[] allActions,
      string timeStamp,
      IReadOnlyCollection<byte> sourceFileHash = null,
      System.DateTime? sourceFileLastModificationTimeUtc = null)
    {
      this.GraphName = graphName;
      this.PrimaryView = primaryView;
      this.PrimaryViewTypeName = primaryViewTypeName;
      this.Actions = actions;
      this.AllActions = allActions;
      this.TimeStamp = timeStamp;
      this.Containers = new Dictionary<string, PXViewDescription>();
      this.Views = new Dictionary<string, string[]>();
      this.Caches = new Dictionary<string, string[]>();
      this.SourceFileHash = sourceFileHash;
      this.SourceFileLastModificationTimeUtc = sourceFileLastModificationTimeUtc;
    }

    [DebuggerDisplay("{Name} (Type = {ButtonType}, IsMass = {IsMass})")]
    public class Action
    {
      public readonly PXSpecialButtonType ActionFolderType;
      public readonly string ActionTypeName;
      public readonly PXSpecialButtonType ButtonType;
      public readonly string DependsOn;
      public readonly string DisplayName;
      public readonly bool Enabled;
      public readonly bool IsMass;
      public readonly string Name;
      public readonly string ShortName;
      public readonly string StateColumn;
      public readonly string ViewTypeName;
      public readonly bool Visible;
      public readonly bool? VisibleOnDataSource;

      public Action(
        string name,
        string displayName,
        bool enabled,
        PXSpecialButtonType buttonType,
        bool isMass,
        bool visible,
        string dependsOn = null,
        string stateColumn = null,
        string actionTypeName = null,
        string viewName = null,
        PXSpecialButtonType actionFolderType = PXSpecialButtonType.Default,
        string shortName = null,
        bool? visibleOnDataSource = null)
      {
        this.Name = name;
        this.DisplayName = displayName;
        this.Enabled = enabled;
        this.ButtonType = buttonType;
        this.IsMass = isMass;
        this.Visible = visible;
        this.DependsOn = dependsOn;
        this.StateColumn = stateColumn;
        this.ActionTypeName = actionTypeName;
        this.ViewTypeName = viewName;
        this.ActionFolderType = actionFolderType;
        this.ShortName = shortName ?? name;
        this.VisibleOnDataSource = visibleOnDataSource;
      }
    }
  }

  private class ServiceRegistration : Module
  {
    protected virtual void Load(ContainerBuilder builder)
    {
      builder.RegisterBuildCallback((System.Action<ILifetimeScope>) (container =>
      {
        PXSiteMap.IsPortal = ResolutionExtensions.Resolve<WebAppType>((IComponentContext) container).IsPortal();
        PXSiteMap._provider = ResolutionExtensions.Resolve<PXSiteMapProvider>((IComponentContext) container);
        PXSiteMap._wikiProvider = ResolutionExtensions.Resolve<PXWikiProvider>((IComponentContext) container);
      }));
      builder.AddWikiSiteMapProvider<PXWikiProvider>();
    }
  }
}
