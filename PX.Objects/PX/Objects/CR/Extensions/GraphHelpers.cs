// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.GraphHelpers
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.Extensions;

[PXInternalUseOnly]
public static class GraphHelpers
{
  public static TGraph CloneGraphState<TGraph>(this TGraph graph) where TGraph : PXGraph, new()
  {
    TGraph graph1 = GraphHelper.Clone<TGraph>(graph);
    graph1.IsContractBasedAPI = graph.IsContractBasedAPI;
    graph1.IsCopyPasteContext = graph.IsCopyPasteContext;
    graph1.IsExport = graph.IsExport;
    graph1.IsImport = graph.IsImport;
    graph1.IsMobile = graph.IsMobile;
    if ((graph1.IsImport || graph.IsImportFromExcel) && !PXContext.Session.IsSessionEnabled)
    {
      foreach (KeyValuePair<System.Type, PXCache> keyValuePair in ((IEnumerable<KeyValuePair<System.Type, PXCache>>) graph.Caches).ToList<KeyValuePair<System.Type, PXCache>>())
      {
        System.Type type1;
        PXCache pxCache1;
        EnumerableExtensions.Deconstruct<System.Type, PXCache>(keyValuePair, ref type1, ref pxCache1);
        System.Type type2 = type1;
        PXCache pxCache2 = pxCache1;
        PXCache cach = graph1.Caches[type2];
        cach.Current = pxCache2.Current;
        foreach (object obj in pxCache2.Cached)
          cach.SetStatus(obj, pxCache2.GetStatus(obj));
      }
    }
    return graph1;
  }
}
