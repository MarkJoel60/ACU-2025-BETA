// Decompiled with JetBrains decompiler
// Type: PX.Translation.PXSystemTranslator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.SM;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace PX.Translation;

/// <summary>
/// Finds strings for translation in the whole system and saves translated values to database.
/// </summary>
public class PXSystemTranslator : PXTranslator
{
  private readonly Dictionary<PXSystemTranslator.ResourceType, string> SearchPatternsByType;
  private readonly string workingFolder;
  private readonly LocalizationResourceType[] resourceTypesToCollect;
  public const string DLL_EXTENSION = ".dll";
  public const string PAGE_EXTENSION = ".aspx";
  public const string REPORT_EXTENSION = ".rpx";

  /// <summary>
  /// Initializes a new instance of PXSystemTranslator class
  /// </summary>
  /// <param name="workingFolder">Folder to search resources in.</param>
  /// <param name="language">Destination language.</param>
  public PXSystemTranslator(string workingFolder, LocalizationResourceType[] resTypesToCollect)
  {
    this.workingFolder = workingFolder;
    this.resourceTypesToCollect = resTypesToCollect;
    this.SearchPatternsByType = new Dictionary<PXSystemTranslator.ResourceType, string>()
    {
      {
        PXSystemTranslator.ResourceType.Dll,
        PXSystemTranslator.GetSearchPatternFromExtension(".dll")
      },
      {
        PXSystemTranslator.ResourceType.Page,
        PXSystemTranslator.GetSearchPatternFromExtension(".aspx")
      },
      {
        PXSystemTranslator.ResourceType.Report,
        PXSystemTranslator.GetSearchPatternFromExtension(".rpx")
      }
    };
  }

  public static string GetSearchPatternFromExtension(string extension)
  {
    return !string.IsNullOrEmpty(extension) ? "*" + extension : (string) null;
  }

  public override ResourceCollection GetNeutral(out ResourceByScreenCollection resourcesByScreens)
  {
    ResourceCollection result = new ResourceCollection();
    DirectoryInfo dir = new DirectoryInfo(this.workingFolder);
    List<string> processed = new List<string>();
    using (new StringCollectingScope())
      this.CollectResources(dir, processed, result, out resourcesByScreens);
    return result;
  }

  public ResourceCollection GetNeutralForTranslationSet(
    List<LocalizationTranslationSetItem> translationSetItems,
    TranslationSetMaint graph,
    out ResourceByScreenCollection resourcesByScreens)
  {
    ResourceCollection result = new ResourceCollection();
    DirectoryInfo dir = new DirectoryInfo(this.workingFolder);
    List<string> processed = new List<string>();
    using (new StringCollectingScope())
      this.CollectTranslationSetResources(dir, processed, result, translationSetItems, graph, out resourcesByScreens);
    return result;
  }

  protected virtual void CollectTranslationSetResources(
    DirectoryInfo dir,
    List<string> processed,
    ResourceCollection result,
    List<LocalizationTranslationSetItem> translationSetItems,
    TranslationSetMaint graph,
    out ResourceByScreenCollection resourcesByScreens)
  {
    ResourceCollection.PermittedResourceTypes = this.resourceTypesToCollect;
    new PXPageRipper().CollectTranslationSetResources(dir, this.SearchPatternsByType[PXSystemTranslator.ResourceType.Page], processed, result, translationSetItems, graph);
    new MobileSiteMapRipper().CollectResources(result);
    DllResourceTypesToCollect dllResourceTypesToCollect = DllResourceTypesToCollect.None;
    if (ResourceCollectingManager.RipMessages(this.resourceTypesToCollect))
      dllResourceTypesToCollect |= DllResourceTypesToCollect.Message;
    if (ResourceCollectingManager.RipXmlComments(this.resourceTypesToCollect))
      dllResourceTypesToCollect |= DllResourceTypesToCollect.XmlComment;
    if (ResourceCollectingManager.RipDisplayNames(this.resourceTypesToCollect))
      dllResourceTypesToCollect |= DllResourceTypesToCollect.DisplayName;
    if (ResourceCollectingManager.RipListAttributes(this.resourceTypesToCollect))
      dllResourceTypesToCollect |= DllResourceTypesToCollect.ListAttribute;
    if (ResourceCollectingManager.RipUIExtensionAttributes(this.resourceTypesToCollect))
      dllResourceTypesToCollect |= DllResourceTypesToCollect.LabelAttribute;
    if (dllResourceTypesToCollect != DllResourceTypesToCollect.None)
      new PXDllRipper(dllResourceTypesToCollect).CollectResources(dir, this.SearchPatternsByType[PXSystemTranslator.ResourceType.Dll], processed, result);
    if (ResourceCollectingManager.RipReports(this.resourceTypesToCollect))
      new PXReportRipper().CollectResources(dir, this.SearchPatternsByType[PXSystemTranslator.ResourceType.Report], processed, result);
    if (ResourceCollectingManager.RipSiteMap(this.resourceTypesToCollect))
      new PXSiteMapRipper().CollectResources(result);
    if (ResourceCollectingManager.RipSpecial(this.resourceTypesToCollect))
      new PXSpecialRipper().CollectResources(result);
    if (ResourceCollectingManager.RipWorkspace(this.resourceTypesToCollect))
      ServiceLocator.Current.GetInstance<IWorkspaceRipper>().CollectResources(result);
    if (ResourceCollectingManager.RipSubcategory(this.resourceTypesToCollect))
      ServiceLocator.Current.GetInstance<ISubcategoryRipper>().CollectResources(result);
    if (ResourceCollectingManager.RipArea(this.resourceTypesToCollect))
      ServiceLocator.Current.GetInstance<IAreaRipper>().CollectResources(result);
    if (ResourceCollectingManager.RipTile(this.resourceTypesToCollect))
      ServiceLocator.Current.GetInstance<ITileRipper>().CollectResources(result);
    if (ResourceCollectingManager.RipTSLiterals(this.resourceTypesToCollect))
      ServiceLocator.Current.GetInstance<TSRipper>().CollectResources(result, (IEnumerable<LocalizationTranslationSetItem>) translationSetItems);
    if (ResourceCollectingManager.RipScreenSidePanel(this.resourceTypesToCollect))
      ((IPXRipper) ServiceLocator.Current.GetInstance<ScreenSidePanelRipper>()).CollectResources(result);
    resourcesByScreens = result.ResourcesByScreens;
  }

  protected virtual void CollectResources(
    DirectoryInfo dir,
    List<string> processed,
    ResourceCollection result,
    out ResourceByScreenCollection resourcesByScreens)
  {
    ResourceCollection.PermittedResourceTypes = this.resourceTypesToCollect;
    using (new SuppressPerformanceInfoScope())
    {
      new MobileSiteMapRipper().CollectResources(result);
      new PXDllRipper().CollectResources(dir, this.SearchPatternsByType[PXSystemTranslator.ResourceType.Dll], processed, result);
      new PXPageRipper().CollectResources(dir, this.SearchPatternsByType[PXSystemTranslator.ResourceType.Page], processed, result);
      new PXReportRipper().CollectResources(dir, this.SearchPatternsByType[PXSystemTranslator.ResourceType.Report], processed, result);
      new PXSiteMapRipper().CollectResources(result);
      new PXSpecialRipper().CollectResources(result);
      RipBy<IWorkspaceRipper>();
      RipBy<TSRipper>();
      RipBy<ISubcategoryRipper>();
      RipBy<IAreaRipper>();
      RipBy<ITileRipper>();
      RipBy<ScreenSidePanelRipper>();
    }
    resourcesByScreens = result.ResourcesByScreens;

    void RipBy<T>() where T : IPXRipper
    {
      ServiceLocator.Current.GetInstance<T>().CollectResources(result);
    }
  }

  /// <exclude />
  public enum ResourceType
  {
    Dll,
    Page,
    Report,
  }
}
