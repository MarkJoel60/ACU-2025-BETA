// Decompiled with JetBrains decompiler
// Type: PX.Data.DataSourceSession
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Session;
using System;

#nullable enable
namespace PX.Data;

internal static class DataSourceSession
{
  internal static void EnsureInitialized(IPXSessionState session, string unmodifiedSessionId)
  {
    if (session.Contains<string>(DataSourceSession.Keys.SessionId))
      return;
    session.Set<string>(DataSourceSession.Keys.SessionId, Guid.NewGuid().ToString());
    session.Set<string>(DataSourceSession.Keys.LoginId, unmodifiedSessionId);
  }

  internal static bool IsValid(IPXSessionState session, string? clientSessionId)
  {
    if (string.IsNullOrEmpty(clientSessionId))
      return true;
    string b = session.Get<string>(DataSourceSession.Keys.SessionId);
    return b == null || string.Equals(clientSessionId, b);
  }

  internal static string? GetDataSourceSessionId(this PXSessionState session)
  {
    return session.Get<string>(DataSourceSession.Keys.SessionId);
  }

  internal static string? GetDataSourceLoginId(this PXSessionState session)
  {
    return session.Get<string>(DataSourceSession.Keys.LoginId);
  }

  private static class Keys
  {
    internal static readonly SessionKey<string> SessionId = new SessionKey<string>("DataSourceSessionID");
    internal static readonly SessionKey<string> LoginId = new SessionKey<string>("DataSourceLoginID");
  }
}
