// Decompiled with JetBrains decompiler
// Type: PX.Data.GraphSessionStateSubKey
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable enable
namespace PX.Data;

internal static class GraphSessionStateSubKey
{
  internal const string Separator = "$";
  internal const StringComparison Comparison = StringComparison.OrdinalIgnoreCase;

  internal static string Create(GraphSessionStatePrefix sessionStatePrefix, string key)
  {
    return sessionStatePrefix.SubKeyPrefix + key;
  }

  internal static string CreatePrefix(GraphSessionStatePrefix sessionStatePrefix)
  {
    return sessionStatePrefix.Value + "$";
  }

  internal static bool IsSubKey(GraphSessionStatePrefix sessionStatePrefix, string key)
  {
    return key.StartsWith(sessionStatePrefix.SubKeyPrefix, StringComparison.OrdinalIgnoreCase);
  }
}
