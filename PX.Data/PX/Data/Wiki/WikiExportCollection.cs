// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.WikiExportCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki;

/// <exclude />
public static class WikiExportCollection
{
  private static readonly Dictionary<string, System.Type> Wikiexportcollection = new Dictionary<string, System.Type>();

  public static void RegisterWikiExport(string key, System.Type value)
  {
    if (!WikiExportCollection.Wikiexportcollection.ContainsKey(key))
      WikiExportCollection.Wikiexportcollection.Add(key, value);
    if (!(WikiExportCollection.Wikiexportcollection[key] == (System.Type) null))
      return;
    WikiExportCollection.Wikiexportcollection.Add(key, value);
  }

  public static object GetWikiExport(string key)
  {
    return WikiExportCollection.Wikiexportcollection.ContainsKey(key) ? Activator.CreateInstance(WikiExportCollection.Wikiexportcollection[key]) : (object) null;
  }
}
