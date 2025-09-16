// Decompiled with JetBrains decompiler
// Type: PX.Licensing.NoRedirectOnLogout
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using PX.AspNetCore;
using System.Web;

#nullable disable
namespace PX.Licensing;

internal sealed class NoRedirectOnLogout
{
  internal static bool Enabled(HttpContext context)
  {
    try
    {
      return NoRedirectOnLogout.Enabled(context.GetCoreHttpContext());
    }
    catch
    {
      return false;
    }
  }

  internal static bool Enabled(HttpContext httpContext)
  {
    return httpContext.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata?.GetMetadata<NoRedirectOnLogout>() != null;
  }
}
