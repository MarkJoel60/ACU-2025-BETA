// Decompiled with JetBrains decompiler
// Type: PX.SP.UrlMatcher
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.Http;
using System;

#nullable disable
namespace PX.SP;

internal static class UrlMatcher
{
  internal static bool MatchUrl(HostString host, PathString basePath, string template)
  {
    Uri result;
    if (string.IsNullOrEmpty(template) || !Uri.TryCreate(template, UriKind.Absolute, out result))
      return false;
    string str = ((PathString) ref basePath).HasValue ? ((PathString) ref basePath).Value : "/";
    if (!str.EndsWith("/"))
      str += "/";
    if (((HostString) ref host).Host.Equals(result.Host, StringComparison.OrdinalIgnoreCase))
    {
      int? port1 = ((HostString) ref host).Port;
      if (port1.HasValue || !result.IsDefaultPort)
      {
        port1 = ((HostString) ref host).Port;
        int port2 = result.Port;
        if (!(port1.GetValueOrDefault() == port2 & port1.HasValue))
          goto label_8;
      }
      return str.StartsWith(result.AbsolutePath, StringComparison.OrdinalIgnoreCase);
    }
label_8:
    return false;
  }
}
