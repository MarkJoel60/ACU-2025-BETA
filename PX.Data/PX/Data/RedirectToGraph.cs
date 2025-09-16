// Decompiled with JetBrains decompiler
// Type: PX.Data.RedirectToGraph
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Primitives;
using PX.Common;
using PX.Common.Session;
using System;
using System.Linq;

#nullable enable
namespace PX.Data;

[PXInternalUseOnly]
public static class RedirectToGraph
{
  private const string GraphStatePrefix = "RedirectPrefix";

  internal static void SetRedirectGraphStatePrefix(this PXSessionState session, PXGraph graph)
  {
    session.Set<string>(RedirectToGraph.Keys.RedirectGraphStatePrefix, graph.StatePrefix);
  }

  internal static string? GetRedirectGraphStatePrefix(this IPXSessionState session)
  {
    return session.Get<string>(RedirectToGraph.Keys.RedirectGraphStatePrefix);
  }

  internal static void RemoveRedirectGraphStatePrefix(this IPXSessionState session)
  {
    session.Remove<string>(RedirectToGraph.Keys.RedirectGraphStatePrefix);
  }

  internal static void SetRedirectGraphType(
    this IPXSessionState session,
    string url,
    System.Type graphType)
  {
    session.Set(url, (object) graphType);
  }

  internal static System.Type? GetRedirectGraphType(this IPXSessionState session, string url)
  {
    return session.Get(url) as System.Type;
  }

  internal static void RemoveRedirectGraphType(this IPXSessionState session, string url)
  {
    session.Remove(url);
  }

  [PXInternalUseOnly]
  public static void SetRedirectGraphType(this PXSessionState session, string url, PXGraph graph)
  {
    session.SetRedirectGraphType(url, graph.GetType());
  }

  internal static void SetRedirectGraphType(
    this PXSessionState session,
    string url,
    System.Type graphType)
  {
    IPXSessionState inner = session.Inner;
    if (inner == null)
      return;
    inner.SetRedirectGraphType(url, graphType);
  }

  internal static System.Type? GetRedirectGraphType(this PXSessionState session, string url)
  {
    IPXSessionState inner = session.Inner;
    return inner == null ? (System.Type) null : inner.GetRedirectGraphType(url);
  }

  internal static void RemoveRedirectGraphType(this PXSessionState session, string url)
  {
    IPXSessionState inner = session.Inner;
    if (inner == null)
      return;
    inner.RemoveRedirectGraphType(url);
  }

  internal static void MoveCompleteGraphInfo(
    IPXSessionState sourceSession,
    GraphSessionStatePrefix sourcePrefix,
    IPXSessionState targetSession,
    GraphSessionStatePrefix targetPrefix)
  {
    object[] graphInfo = sourceSession.GetGraphInfo(sourcePrefix);
    sourceSession.RemoveGraphInfo(sourcePrefix);
    targetSession.RemoveSubKeys(targetPrefix);
    foreach ((string key, StringSegment subKey) in sourceSession.GetSubKeys(sourcePrefix).ToList<(string, StringSegment)>())
    {
      targetSession.Set(targetPrefix.GetSubKey(((StringSegment) ref subKey).Value), sourceSession.Get(key));
      sourceSession.Remove(key);
    }
    targetSession.SetGraphInfo(targetPrefix, graphInfo);
  }

  internal static void ConvertCompleteGraphInfo(
    this PXSessionState session,
    GraphSessionStatePrefix sourcePrefix,
    GraphSessionStatePrefix targetPrefix)
  {
    IPXSessionState pxSessionState = session.RequireInner();
    RedirectToGraph.MoveCompleteGraphInfo(pxSessionState, sourcePrefix, pxSessionState, targetPrefix);
  }

  internal static void UnloadForRedirect(this PXGraph graph)
  {
    using (graph.UnderRedirectStatePrefix())
      graph.Unload();
  }

  internal static IDisposable UnderRedirectStatePrefix(this PXGraph graph)
  {
    return graph.UnderDifferentStatePrefix("RedirectPrefix");
  }

  internal static GraphSessionStatePrefix CreateGraphSessionStatePrefixWithRedirect(System.Type redirect)
  {
    return GraphSessionStatePrefix.Create("RedirectPrefix", redirect);
  }

  private static class Keys
  {
    internal static readonly SessionKey<string> RedirectGraphStatePrefix = new SessionKey<string>("Redirect_GraphStatePrefix");
  }
}
