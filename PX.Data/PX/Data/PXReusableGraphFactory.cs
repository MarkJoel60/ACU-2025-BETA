// Decompiled with JetBrains decompiler
// Type: PX.Data.PXReusableGraphFactory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Session;
using PX.Data.Automation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

internal class PXReusableGraphFactory
{
  private const string SAME_CONTEXT_KEY = "PXReusableGraphFactory_SAME_CONTEXT";

  private static PXReusableGraphInfo ReusableGraphInfo
  {
    get
    {
      PXReusableGraphInfo reusableGraphInfo = PXContext.SessionTyped<PXSessionStatePXData>().ReusableGraphInfo;
      if (reusableGraphInfo == null)
      {
        reusableGraphInfo = new PXReusableGraphInfo();
        PXContext.SessionTyped<PXSessionStatePXData>().ReusableGraphInfo = reusableGraphInfo;
      }
      return reusableGraphInfo;
    }
  }

  public static bool TryGet(System.Type type, string sessionPrefix, out PXGraph result)
  {
    result = (PXGraph) null;
    if (!string.IsNullOrEmpty(sessionPrefix) || PXReusableGraphFactory.ReusableGraphInfo.Graph == null)
      return false;
    if (!PXReusableGraphFactory.IsSameScreen() && !PXReusableGraphFactory.ReusableGraphInfo.FromNavigation || PXReusableGraphFactory.ReusableGraphInfo.IsFromRedirect)
    {
      PXReusableGraphFactory.ReusableGraphInfo.Graph.ReuseRestricted = true;
      PXReusableGraphFactory.ReusableGraphInfo.Graph.Unload();
      PXReusableGraphFactory.ReusableGraphInfo.Clear();
      return false;
    }
    if (!typeof (IPXReusableGraph).IsAssignableFrom(type))
      return false;
    if (PXReusableGraphFactory.ReusableGraphInfo.Graph.ReuseRestricted || !PXReusableGraphFactory.IsSameCodeVersion() || !type.IsInstanceOfType((object) PXReusableGraphFactory.ReusableGraphInfo.Graph))
    {
      PXReusableGraphFactory.ReusableGraphInfo.Clear();
      return false;
    }
    result = PXReusableGraphFactory.ReusableGraphInfo.Graph;
    ++result.ReuseCount;
    result.IsReusableGraph = true;
    PXReusableGraphFactory.ReusableGraphInfo.FromNavigation = false;
    PXReusableGraphFactory.ReuseInitialize(result);
    return true;
  }

  private static void ReuseInitialize(PXGraph graph)
  {
    graph.RaiseReuseInitialize();
    PXWorkflowService workflowService = graph.WorkflowService;
    if (workflowService != null)
    {
      System.Action onReuseInitialize = workflowService.OnReuseInitialize;
      if (onReuseInitialize != null)
        onReuseInitialize();
    }
    graph._ReWriteUnloadData = true;
    graph.IsSessionReadOnly = false;
  }

  public static void Load(PXGraph graph, IPXSessionState session)
  {
    if (session.GetCacheInfo(GraphSessionStatePrefix.WithoutStatePrefixFor(graph), typeof (DialogAnswer)) != null)
    {
      PXCache cach1 = graph.Caches[typeof (DialogAnswer)];
    }
    graph.InitScopedAccessInfoProperties();
    graph.stateLoading = true;
    foreach (PXCache cach2 in graph.Caches.Caches)
      cach2.Load();
    graph.stateLoading = false;
    graph._ReWriteUnloadData = true;
  }

  internal static void Unload(PXGraph graph, IPXSessionState session)
  {
    graph._viewNames = (List<string>) null;
    foreach (KeyValuePair<string, PXView> view in (Dictionary<string, PXView>) graph.Views)
    {
      view.Value.ReinitializeSelectQueries();
      view.Value.RefreshRequested = (EventHandler) null;
    }
    foreach (PXCache cach in graph.Caches.Caches)
    {
      cach.ClearItemAttributes();
      cach.ReinitializeCollection();
      cach.ClearSessionUnmodified();
      cach.ResetState();
    }
    if (graph.Caches.ContainsKey(typeof (DialogAnswer)) && graph.Caches[typeof (DialogAnswer)] is DialogManager.DialogAnswerCache cach1)
    {
      cach1.Unload();
      graph.Caches.Remove(typeof (DialogAnswer));
      if (graph.Views.ContainsKey("DialogAnswerView"))
        graph.Views.Remove("DialogAnswerView");
    }
    graph.UnloadQueryCache(session);
    PXReusableGraphFactory.ReusableGraphInfo.Graph = graph;
  }

  public static void ReuseGraph(PXGraph graph, string sessionPrefix)
  {
    if (!string.IsNullOrEmpty(sessionPrefix) || !(graph is IPXReusableGraph) || !PXReusableGraphFactory.DirectlyImplementsInterface(graph))
      return;
    if (!graph.IsReusableGraph)
      PXReusableGraphFactory.ReusableGraphInfo.CodeVersion = PXCodeDirectoryCompiler.Version;
    graph.IsReusableGraph = true;
    PXReusableGraphFactory.ReusableGraphInfo.Graph = graph;
  }

  public static void ReuseGraphFromNavigation(PXGraph graph, string screenId)
  {
    if (screenId == PXReusableGraphFactory.ReusableGraphInfo.CurrentScreenId?.Replace(".", "") || !(graph is IPXReusableGraph) || !PXReusableGraphFactory.DirectlyImplementsInterface(graph))
      return;
    PXReusableGraphFactory.ReuseGraph(graph, (string) null);
    PXReusableGraphFactory.ReusableGraphInfo.FromNavigation = true;
  }

  public static void FillPreviousScreen()
  {
    string screenId = PXContext.GetScreenID();
    if (PXContext.GetSlot<bool>("PXReusableGraphFactory_SAME_CONTEXT") || string.IsNullOrEmpty(screenId))
      return;
    PXContext.SetSlot<bool>("PXReusableGraphFactory_SAME_CONTEXT", true);
    PXReusableGraphFactory.ReusableGraphInfo.PreviousScreenId = PXReusableGraphFactory.ReusableGraphInfo.CurrentScreenId;
    PXReusableGraphFactory.ReusableGraphInfo.CurrentScreenId = screenId;
  }

  public static bool IsSameScreen()
  {
    string currentScreenId = PXReusableGraphFactory.ReusableGraphInfo.CurrentScreenId;
    string previousScreenId = PXReusableGraphFactory.ReusableGraphInfo.PreviousScreenId;
    return !string.IsNullOrEmpty(currentScreenId) && !string.IsNullOrEmpty(previousScreenId) && currentScreenId == previousScreenId;
  }

  public static void Clear(IPXSessionState session) => session.Remove("ReusableGraphInfo");

  public static void SetRedirect()
  {
    if (PXReusableGraphFactory.ReusableGraphInfo.Graph == null || PXReusableGraphFactory.ReusableGraphInfo.FromNavigation)
      return;
    PXReusableGraphFactory.ReusableGraphInfo.IsFromRedirect = true;
  }

  private static bool IsSameCodeVersion()
  {
    return PXReusableGraphFactory.ReusableGraphInfo.CodeVersion == PXCodeDirectoryCompiler.Version;
  }

  private static bool DirectlyImplementsInterface(PXGraph graph)
  {
    System.Type baseType = graph.GetType()?.BaseType;
    return ((IEnumerable<System.Type>) baseType.GetInterfaces()).Except<System.Type>((IEnumerable<System.Type>) (baseType.BaseType?.GetInterfaces() ?? Array.Empty<System.Type>())).Contains<System.Type>(typeof (IPXReusableGraph));
  }
}
