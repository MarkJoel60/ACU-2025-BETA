// Decompiled with JetBrains decompiler
// Type: PX.Api.ImportSimple.ImportSimpleDefaulter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

#nullable disable
namespace PX.Api.ImportSimple;

public static class ImportSimpleDefaulter
{
  private const string DEFAULT_SCENARIO_NAME_PREFIX = "Import";

  private static string DefaultScreenID => (string) null;

  private static string DefaultScenarioName => (string) null;

  public static string GetDefaultScenarioName(string screenID)
  {
    string defaultScenarioName = (string) null;
    if (!string.IsNullOrEmpty(screenID))
    {
      string[] array1 = PXSelectBase<SYMapping, PXSelect<SYMapping>.Config>.Select(new PXGraph(), (object[]) null).Select<PXResult<SYMapping>, string>((Expression<Func<PXResult<SYMapping>, string>>) (mapping => ((SYMapping) mapping).Name)).ToArray<string>();
      string[] array2 = PXSelectBase<SYProvider, PXSelect<SYProvider>.Config>.Select(new PXGraph(), (object[]) null).Select<PXResult<SYProvider>, string>((Expression<Func<PXResult<SYProvider>, string>>) (provider => ((SYProvider) provider).Name)).ToArray<string>();
      PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenID);
      if (screenIdUnsecure != null && !string.IsNullOrEmpty(screenIdUnsecure.Title))
      {
        bool flag = false;
        int revision = 0;
        while (!flag)
        {
          defaultScenarioName = ImportSimpleDefaulter.GenerateScenarioName(screenIdUnsecure.Title, revision);
          flag = !((IEnumerable<string>) array1).Contains<string>(defaultScenarioName) && !((IEnumerable<string>) array2).Contains<string>(defaultScenarioName);
          ++revision;
        }
      }
    }
    return defaultScenarioName;
  }

  public static string GetDefaultProviderType(string fileName)
  {
    string defaultProviderType = (string) null;
    if (!string.IsNullOrEmpty(fileName))
    {
      string fileExtension = PXPath.GetExtension(fileName);
      if (!string.IsNullOrEmpty(fileExtension))
        defaultProviderType = new PXSYProviderSelector().GetRecords().Cast<PXSYProviderSelector.ProviderRec>().Where<PXSYProviderSelector.ProviderRec>((Func<PXSYProviderSelector.ProviderRec, bool>) (provider => provider.DefaultFileExtension == fileExtension)).Select<PXSYProviderSelector.ProviderRec, string>((Func<PXSYProviderSelector.ProviderRec, string>) (provider => provider.TypeName)).OrderBy<string, string>((Func<string, string>) (type => type)).FirstOrDefault<string>();
    }
    return defaultProviderType;
  }

  public static void FillDefaultsToScenarioProperties(SYMappingSimpleProperty properties)
  {
    if (properties == null)
      return;
    properties.ScreenID = ImportSimpleDefaulter.DefaultScreenID;
    properties.Name = ImportSimpleDefaulter.DefaultScenarioName;
    properties.ProviderType = ImportSimpleDefaulter.GetDefaultProviderType(properties.File == null ? (string) null : properties.File.OriginalName);
  }

  private static string GenerateScenarioName(string nameBase, int revision)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("{0} {1}", (object) "Import", (object) nameBase);
    if (revision > 0)
      stringBuilder.AppendFormat(" ({0})", (object) revision);
    return stringBuilder.ToString();
  }
}
