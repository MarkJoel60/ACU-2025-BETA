// Decompiled with JetBrains decompiler
// Type: PX.Security.LegacyBasicSchemeExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using PX.Common;
using PX.Export.Authentication;
using System;

#nullable disable
namespace PX.Security;

[PXInternalUseOnly]
public static class LegacyBasicSchemeExtensions
{
  internal static AuthenticationBuilder AddLegacyBasic(
    this AuthenticationBuilder builder,
    string schemaName,
    Action<BasicAuthenticationOptions> configureOptions)
  {
    return builder.AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>(schemaName, configureOptions);
  }

  internal static AuthenticationBuilder AddLegacyBasic(
    this AuthenticationBuilder builder,
    string name,
    Func<HttpContext, string, string> prepareUsername)
  {
    return builder.AddLegacyBasic(name, (Action<BasicAuthenticationOptions>) (options => options.PrepareUsername = prepareUsername));
  }

  [PXInternalUseOnly]
  public static AuthenticationBuilder AddLegacyBasic(this AuthenticationBuilder builder)
  {
    return builder.AddLegacyBasic("acumatica:legacy:basic", (Func<HttpContext, string, string>) ((_, username) => username)).AddLegacyBasicNoCompany();
  }

  [PXInternalUseOnly]
  public static IAuthenticationManagerLocationOptionsBuilder WithBasic(
    this IAuthenticationManagerLocationOptionsBuilder builder)
  {
    return builder.WithScheme("acumatica:legacy:basic");
  }
}
