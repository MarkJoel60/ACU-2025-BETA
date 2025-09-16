// Decompiled with JetBrains decompiler
// Type: PX.Translation.ResourceCollectingManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using Microsoft.Extensions.Options;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#nullable disable
namespace PX.Translation;

/// <exclude />
public class ResourceCollectingManager
{
  public const string DONT_ADD_HEADER_TO_RESPONSE = "DontAddHeaderToResponse";
  private static readonly List<ResourceCollectingManager.ResourceModel> resourcesForFilter;
  private static readonly List<ResourceCollectingManager.ResourceModel> allUnboundResources = new List<ResourceCollectingManager.ResourceModel>()
  {
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.SiteMapNode, "Site Map Node"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.TextBoxValue, "Text Box Value"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.Prompt, "Prompt"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.ValidValue, "Valid Value"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.ChartName, "Chart Name"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.ConstantInFormula, "Constant in Formula"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.Message, "Message"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.ColorName, "Color Name"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.FontName, "Font Name"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.FontFamily, "Font Family"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.MobileSiteMapDisplayName, "Mobile SiteMap Display Name"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.MailSettings, "Mail Settings"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.ExportFileName, "Export File Name"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.NonUIField, "Non UI Field"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.Workspace, "Workspace"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.Subcategory, "Category"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.Area, "Area"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.Tile, "Tile"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.XmlComment, "XML Comment"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.TSLiteral, "TypeScript Literal")
  };
  private static readonly List<ResourceCollectingManager.ResourceModel> allBoundResources = new List<ResourceCollectingManager.ResourceModel>()
  {
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.DisplayName, "Display Name"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.StringListItem, "String List Item"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.IntListItem, "Int List Item"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.Control, "Control"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.VirtualTableDisplayName, "Virtual Table Display Name"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.DbStringListItem, "DB String List Item"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.DescriptionDisplayName, "Description Display Name"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.ActionDisplayName, "Action Display Name"),
    new ResourceCollectingManager.ResourceModel(LocalizationResourceType.TSLiteral, "TypeScript Literal")
  };

  public static bool? DontAddHeaderToResponce { get; set; }

  public static CollectStringsSettings? CollectSettings { get; set; }

  internal static string[] AllUnboundResourceTypes
  {
    get
    {
      return ResourceCollectingManager.allUnboundResources.Select<ResourceCollectingManager.ResourceModel, string>((Func<ResourceCollectingManager.ResourceModel, string>) (resource => Enum.GetName(typeof (LocalizationResourceType), (object) resource.Type))).ToArray<string>();
    }
  }

  internal static string[] AllUnboundResourceNames
  {
    get
    {
      return ResourceCollectingManager.allUnboundResources.Select<ResourceCollectingManager.ResourceModel, string>((Func<ResourceCollectingManager.ResourceModel, string>) (resource => resource.Name)).ToArray<string>();
    }
  }

  internal static string[] ResourceForFilterTypes
  {
    get
    {
      return ResourceCollectingManager.resourcesForFilter.Select<ResourceCollectingManager.ResourceModel, string>((Func<ResourceCollectingManager.ResourceModel, string>) (resource => Enum.GetName(typeof (LocalizationResourceType), (object) resource.Type))).ToArray<string>();
    }
  }

  internal static string[] AllResourceForFilterNames
  {
    get
    {
      return ResourceCollectingManager.resourcesForFilter.Select<ResourceCollectingManager.ResourceModel, string>((Func<ResourceCollectingManager.ResourceModel, string>) (resource => resource.Name)).ToArray<string>();
    }
  }

  public static bool IsStringCollecting
  {
    get => HttpContext.Current != null && PXPageRipper.ContainsCollector(HttpContext.Current);
  }

  static ResourceCollectingManager()
  {
    ResourceCollectingManager.resourcesForFilter = ResourceCollectingManager.allUnboundResources.Union<ResourceCollectingManager.ResourceModel>((IEnumerable<ResourceCollectingManager.ResourceModel>) ResourceCollectingManager.allBoundResources).ToList<ResourceCollectingManager.ResourceModel>();
    ResourceCollectingManager.resourcesForFilter.Sort((Comparison<ResourceCollectingManager.ResourceModel>) ((x, y) => string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase)));
    ResourceCollectingManager.allUnboundResources.Sort((Comparison<ResourceCollectingManager.ResourceModel>) ((x, y) => string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase)));
  }

  internal static int[] AllResourceTypeValues()
  {
    return Enum.GetValues(typeof (LocalizationResourceType)) as int[];
  }

  internal static LocalizationResourceType[] ParseResourceTypesFromString(string typesString)
  {
    List<LocalizationResourceType> localizationResourceTypeList = new List<LocalizationResourceType>();
    if (!string.IsNullOrEmpty(typesString))
    {
      string str1 = typesString;
      char[] chArray = new char[1]{ ',' };
      foreach (string str2 in str1.Split(chArray))
      {
        LocalizationResourceType result;
        if (Enum.TryParse<LocalizationResourceType>(str2, out result))
          localizationResourceTypeList.Add(result);
      }
    }
    return localizationResourceTypeList.ToArray();
  }

  internal static LocalizationResourceType[] EnsureBoundResourceTypes(
    LocalizationResourceType[] resourceTypes)
  {
    return ((IEnumerable<LocalizationResourceType>) resourceTypes).Union<LocalizationResourceType>(ResourceCollectingManager.allBoundResources.Select<ResourceCollectingManager.ResourceModel, LocalizationResourceType>((Func<ResourceCollectingManager.ResourceModel, LocalizationResourceType>) (r => r.Type))).ToArray<LocalizationResourceType>();
  }

  internal static LocalizationResourceType[] ExceptBoundTypes(
    IEnumerable<LocalizationResourceType> types)
  {
    return types.Except<LocalizationResourceType>(ResourceCollectingManager.allBoundResources.Select<ResourceCollectingManager.ResourceModel, LocalizationResourceType>((Func<ResourceCollectingManager.ResourceModel, LocalizationResourceType>) (r => r.Type))).ToArray<LocalizationResourceType>();
  }

  internal static bool RipMessages(LocalizationResourceType[] resourceTypesToCollect)
  {
    bool flag = false;
    if (resourceTypesToCollect != null)
      flag = ((IEnumerable<LocalizationResourceType>) resourceTypesToCollect).Contains<LocalizationResourceType>(LocalizationResourceType.Message);
    return flag;
  }

  internal static bool RipXmlComments(LocalizationResourceType[] resourceTypesToCollect)
  {
    bool flag = resourceTypesToCollect != null && ((IEnumerable<LocalizationResourceType>) resourceTypesToCollect).Contains<LocalizationResourceType>(LocalizationResourceType.XmlComment);
    if (flag)
    {
      StringCollectingOptions collectingOptions = ServiceLocator.Current.GetInstance<IOptions<StringCollectingOptions>>().Value;
      flag = collectingOptions == null || collectingOptions.CollectXmlComments;
    }
    return flag;
  }

  internal static bool RipDisplayNames(LocalizationResourceType[] resourceTypesToCollect)
  {
    return resourceTypesToCollect != null && ((IEnumerable<LocalizationResourceType>) resourceTypesToCollect).Any<LocalizationResourceType>((Func<LocalizationResourceType, bool>) (resourceType => resourceType == LocalizationResourceType.DisplayName || resourceType == LocalizationResourceType.VirtualTableDisplayName || resourceType == LocalizationResourceType.ActionDisplayName));
  }

  internal static bool RipListAttributes(LocalizationResourceType[] resourceTypesToCollect)
  {
    return resourceTypesToCollect != null && ((IEnumerable<LocalizationResourceType>) resourceTypesToCollect).Any<LocalizationResourceType>((Func<LocalizationResourceType, bool>) (resourceType => resourceType == LocalizationResourceType.StringListItem || resourceType == LocalizationResourceType.IntListItem || resourceType == LocalizationResourceType.DbStringListItem));
  }

  internal static bool RipUIExtensionAttributes(LocalizationResourceType[] resourceTypesToCollect)
  {
    return resourceTypesToCollect != null && ((IEnumerable<LocalizationResourceType>) resourceTypesToCollect).Contains<LocalizationResourceType>(LocalizationResourceType.Control);
  }

  internal static bool RipReports(LocalizationResourceType[] resourceTypesToCollect)
  {
    bool flag = false;
    if (resourceTypesToCollect != null)
      flag = ((IEnumerable<LocalizationResourceType>) new LocalizationResourceType[5]
      {
        LocalizationResourceType.Prompt,
        LocalizationResourceType.ValidValue,
        LocalizationResourceType.ChartName,
        LocalizationResourceType.ConstantInFormula,
        LocalizationResourceType.TextBoxValue
      }).Any<LocalizationResourceType>(new Func<LocalizationResourceType, bool>(((Enumerable) resourceTypesToCollect).Contains<LocalizationResourceType>));
    return flag;
  }

  internal static bool RipSiteMap(LocalizationResourceType[] resourceTypesToCollect)
  {
    bool flag = false;
    if (resourceTypesToCollect != null)
      flag = ((IEnumerable<LocalizationResourceType>) resourceTypesToCollect).Contains<LocalizationResourceType>(LocalizationResourceType.SiteMapNode);
    return flag;
  }

  internal static bool RipWorkspace(LocalizationResourceType[] resourceTypesToCollect)
  {
    return resourceTypesToCollect != null && ((IEnumerable<LocalizationResourceType>) resourceTypesToCollect).Contains<LocalizationResourceType>(LocalizationResourceType.Workspace);
  }

  internal static bool RipSubcategory(LocalizationResourceType[] resourceTypesToCollect)
  {
    return resourceTypesToCollect != null && ((IEnumerable<LocalizationResourceType>) resourceTypesToCollect).Contains<LocalizationResourceType>(LocalizationResourceType.Subcategory);
  }

  internal static bool RipArea(LocalizationResourceType[] resourceTypesToCollect)
  {
    return resourceTypesToCollect != null && ((IEnumerable<LocalizationResourceType>) resourceTypesToCollect).Contains<LocalizationResourceType>(LocalizationResourceType.Area);
  }

  internal static bool RipTile(LocalizationResourceType[] resourceTypesToCollect)
  {
    return resourceTypesToCollect != null && ((IEnumerable<LocalizationResourceType>) resourceTypesToCollect).Contains<LocalizationResourceType>(LocalizationResourceType.Tile);
  }

  internal static bool RipTSLiterals(LocalizationResourceType[] resourceTypesToCollect)
  {
    return resourceTypesToCollect != null && ((IEnumerable<LocalizationResourceType>) resourceTypesToCollect).Contains<LocalizationResourceType>(LocalizationResourceType.TSLiteral);
  }

  internal static bool RipScreenSidePanel(LocalizationResourceType[] resourceTypesToCollect)
  {
    return resourceTypesToCollect != null && ((IEnumerable<LocalizationResourceType>) resourceTypesToCollect).Contains<LocalizationResourceType>(LocalizationResourceType.SidePanel);
  }

  internal static bool RipSpecial(LocalizationResourceType[] resourceTypesToCollect)
  {
    bool flag = false;
    if (resourceTypesToCollect != null)
      flag = ((IEnumerable<LocalizationResourceType>) new LocalizationResourceType[3]
      {
        LocalizationResourceType.ColorName,
        LocalizationResourceType.FontFamily,
        LocalizationResourceType.FontName
      }).Any<LocalizationResourceType>(new Func<LocalizationResourceType, bool>(((Enumerable) resourceTypesToCollect).Contains<LocalizationResourceType>));
    return flag;
  }

  private class ResourceModel
  {
    public LocalizationResourceType Type { get; private set; }

    public string Name { get; private set; }

    public ResourceModel(LocalizationResourceType type, string name)
    {
      this.Type = type;
      this.Name = name;
    }
  }
}
