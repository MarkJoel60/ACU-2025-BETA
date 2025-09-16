// Decompiled with JetBrains decompiler
// Type: PX.Api.ImportGraphNavigator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Api;

internal static class ImportGraphNavigator
{
  private static string IMPORT_GRAPH_KEY = "SYGraphWithErrors";

  public static void StoreGraph(PXGraph graph, List<PXErrorInfo> nonUIErrors)
  {
    if (graph == null)
      return;
    graph.PreserveErrorInfo = true;
    if (nonUIErrors != null && nonUIErrors.Count > 0)
      graph.NonUIErrors = nonUIErrors.ToArray();
    PXLongOperation.SetCustomInfo((object) graph, ImportGraphNavigator.IMPORT_GRAPH_KEY);
  }

  public static PXGraph LoadGraph(object UID)
  {
    return PXContext.Session.LongOpCustomInfo[ImportGraphNavigator.IMPORT_GRAPH_KEY] as PXGraph;
  }

  public static void ClearImportInfo()
  {
    PXContext.Session.Remove(ImportGraphNavigator.IMPORT_GRAPH_KEY);
  }

  public static void NavigateToGraph(object UID)
  {
    PXGraph graph = ImportGraphNavigator.LoadGraph(UID);
    if (graph != null)
      throw new PXRedirectRequiredException(graph, true, (string) null);
  }
}
