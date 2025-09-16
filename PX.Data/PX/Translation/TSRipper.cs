// Decompiled with JetBrains decompiler
// Type: PX.Translation.TSRipper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PX.Api;
using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace PX.Translation;

internal class TSRipper : IPXRipper
{
  private readonly IHostEnvironment _hostEnvironment;

  public TSRipper(IHostEnvironment hostEnvironment) => this._hostEnvironment = hostEnvironment;

  public void CollectResources(ResourceCollection result)
  {
    this.CollectResources(result, (IEnumerable<LocalizationTranslationSetItem>) null);
  }

  public void CollectResources(
    ResourceCollection result,
    IEnumerable<LocalizationTranslationSetItem> translationSetItems)
  {
    if (result == null)
      throw new ArgumentNullException(nameof (result));
    IDirectoryContents directoryContents1 = this._hostEnvironment.ContentRootFileProvider.GetDirectoryContents("App_Data\\TSLocalizations\\Common");
    if (!directoryContents1.Exists)
    {
      PXTrace.WriteError("Directory {DirectoryName} not found", (object) "App_Data\\TSLocalizations\\Common");
    }
    else
    {
      foreach (IFileInfo ifileInfo in (IEnumerable<IFileInfo>) directoryContents1)
      {
        try
        {
          if (ifileInfo.Name.EndsWith(".json"))
          {
            using (StreamReader streamReader = new StreamReader(ifileInfo.CreateReadStream()))
              EnumerableExtensions.ForEach<KeyValuePair<string, string>>((IEnumerable<KeyValuePair<string, string>>) JsonConvert.DeserializeObject<Dictionary<string, string>>(streamReader.ReadToEnd()), (System.Action<KeyValuePair<string, string>>) (item => result.AddResource(new LocalizationResourceLite(item.Key, LocalizationResourceType.TSLiteral, item.Value))));
          }
        }
        catch (Exception ex)
        {
          PXTrace.WithStack().Error<string>("Failed to rip TS localization strings: {Reason}", ex.Message);
        }
      }
      IDirectoryContents directoryContents2 = this._hostEnvironment.ContentRootFileProvider.GetDirectoryContents("App_Data\\TSLocalizations");
      if (!directoryContents2.Exists)
      {
        PXTrace.WriteError("Directory {DirectoryName} not found", (object) "App_Data\\TSLocalizations");
      }
      else
      {
        List<LocalizationTranslationSetItem> list = translationSetItems != null ? translationSetItems.ToList<LocalizationTranslationSetItem>() : (List<LocalizationTranslationSetItem>) null;
        bool flag = list != null && list.Count > 0;
        HashSet<string> hashSet = list != null ? list.Where<LocalizationTranslationSetItem>((Func<LocalizationTranslationSetItem, bool>) (item => item.NameForStringCollection.EndsWith(".html", StringComparison.OrdinalIgnoreCase))).Select<LocalizationTranslationSetItem, string>((Func<LocalizationTranslationSetItem, string>) (item => item.ScreenID.ToUpperInvariant())).ToHashSet<string>() : (HashSet<string>) null;
        if (flag && hashSet.Count == 0)
          return;
        foreach (IFileInfo ifileInfo in (IEnumerable<IFileInfo>) directoryContents2)
        {
          try
          {
            string screen = ifileInfo.Name;
            if (screen.EndsWith(".json"))
            {
              screen = screen.Split('.')[0].ToUpperInvariant();
              if (flag)
              {
                if (!hashSet.Contains(screen))
                  continue;
              }
              if (!screen.OrdinalIgnoreCaseEquals("AuPanelViewChangesScreen"))
              {
                LocalizationResourceLite.CurrentScreenID = screen;
                using (StreamReader streamReader = new StreamReader(ifileInfo.CreateReadStream()))
                {
                  if (!ifileInfo.Name.EndsWith("ts.json", StringComparison.OrdinalIgnoreCase))
                    EnumerableExtensions.ForEach<KeyValuePair<string, Dictionary<string, string>>>((IEnumerable<KeyValuePair<string, Dictionary<string, string>>>) JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(streamReader.ReadToEnd()), (System.Action<KeyValuePair<string, Dictionary<string, string>>>) (subMap => EnumerableExtensions.ForEach<KeyValuePair<string, string>>((IEnumerable<KeyValuePair<string, string>>) subMap.Value, (System.Action<KeyValuePair<string, string>>) (item => result.AddResourceByScreen(new LocalizationResourceLite($"{screen}.{subMap.Key}.{item.Key}", LocalizationResourceType.TSLiteral, item.Value))))));
                  else
                    EnumerableExtensions.ForEach<KeyValuePair<string, string>>((IEnumerable<KeyValuePair<string, string>>) JsonConvert.DeserializeObject<Dictionary<string, string>>(streamReader.ReadToEnd()), (System.Action<KeyValuePair<string, string>>) (item => result.AddResourceByScreen(new LocalizationResourceLite(item.Key, LocalizationResourceType.TSLiteral, item.Value))));
                }
              }
            }
          }
          catch (Exception ex)
          {
            PXTrace.WithStack().Error<string>("Failed to rip TS localization strings: {Reason}", ex.Message);
          }
          finally
          {
            LocalizationResourceLite.CurrentScreenID = (string) null;
          }
        }
      }
    }
  }
}
