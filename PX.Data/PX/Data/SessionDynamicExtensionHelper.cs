// Decompiled with JetBrains decompiler
// Type: PX.Data.SessionDynamicExtensionHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Session;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Data;

internal static class SessionDynamicExtensionHelper
{
  private static IEnumerable<IBqlTableSystemDataStorage> GetPXBqlTables(IPXSessionState items)
  {
    foreach (string key in items.Keys)
    {
      foreach (IBqlTableSystemDataStorage pxBqlTable in items.GetPXBqlTablesFromCacheInfo(key))
        yield return pxBqlTable;
      foreach (IBqlTableSystemDataStorage pxBqlTable in items.GetPXBqlTablesFromGraphQueryCache(key))
        yield return pxBqlTable;
    }
  }

  internal static bool CheckDynamicExtensions(this IPXSessionState items)
  {
    try
    {
      PXGraph pxGraph;
      using (new PXPreserveScope())
        pxGraph = new PXGraph();
      foreach (IBqlTable bqlTable in SessionDynamicExtensionHelper.GetPXBqlTables(items).ToList<IBqlTableSystemDataStorage>())
      {
        PXCacheExtension[] extensions;
        if (bqlTable.TryGetExtensions(out extensions))
        {
          int length = extensions.Length;
          PXCache cach = pxGraph.Caches[bqlTable.GetType()];
          System.Type[] extensionTypes = cach.GetExtensionTypes();
          if (extensionTypes.Length == 0 || extensionTypes.Length != length)
          {
            bqlTable.ClearExtensions();
            try
            {
              cach.GetCacheExtensions(bqlTable);
            }
            catch
            {
            }
          }
          else
          {
            for (int index = 0; index < extensionTypes.Length; ++index)
            {
              if (!extensionTypes[index].IsInstanceOfType((object) extensions[index]))
              {
                bqlTable.ClearExtensions();
                try
                {
                  cach.GetCacheExtensions(bqlTable);
                  break;
                }
                catch
                {
                  break;
                }
              }
            }
          }
        }
      }
      return true;
    }
    catch
    {
      System.Action extensionsDelegate = PXCache.ClearCacheExtensionsDelegate;
      if (extensionsDelegate != null)
        extensionsDelegate();
      return false;
    }
  }
}
