// Decompiled with JetBrains decompiler
// Type: PX.Data.GraphSessionStateExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Primitives;
using PX.Common;
using PX.Common.Session;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Data;

internal static class GraphSessionStateExtensions
{
  internal static object[]? GetGraphInfo(
    this IPXSessionState session,
    GraphSessionStatePrefix sessionPrefix)
  {
    return session.Get(sessionPrefix.GraphInfoKey) as object[];
  }

  internal static void SetGraphInfo(
    this IPXSessionState session,
    GraphSessionStatePrefix sessionPrefix,
    object[]? graphInfo)
  {
    session.Set(sessionPrefix.GraphInfoKey, (object) graphInfo);
  }

  internal static void RemoveGraphInfo(
    this IPXSessionState session,
    GraphSessionStatePrefix sessionPrefix)
  {
    session.Remove(sessionPrefix.GraphInfoKey);
  }

  internal static IEnumerable<(string key, StringSegment subKey)> GetSubKeys(
    this IPXSessionState session,
    GraphSessionStatePrefix sessionPrefix)
  {
    return session.Keys.Where<string>(new Func<string, bool>(sessionPrefix.IsSubKey)).Select<string, (string, StringSegment)>((Func<string, (string, StringSegment)>) (key => (key, new StringSegment(key, sessionPrefix.SubKeyPrefix.Length, key.Length - sessionPrefix.SubKeyPrefix.Length))));
  }

  internal static void RemoveSubKeys(
    this IPXSessionState session,
    GraphSessionStatePrefix sessionPrefix)
  {
    foreach (string str in session.GetSubKeys(sessionPrefix).Select<(string, StringSegment), string>((Func<(string, StringSegment), string>) (x => x.key)).ToList<string>())
      session.Remove(str);
  }

  internal static void RemoveAll(this PXSessionState session, GraphSessionStatePrefix sessionPrefix)
  {
    session.RemoveAll(sessionPrefix.Value);
  }
}
