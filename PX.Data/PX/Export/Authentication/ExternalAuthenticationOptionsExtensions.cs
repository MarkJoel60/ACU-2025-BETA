// Decompiled with JetBrains decompiler
// Type: PX.Export.Authentication.ExternalAuthenticationOptionsExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Auth;
using PX.SP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#nullable disable
namespace PX.Export.Authentication;

[PXInternalUseOnly]
public static class ExternalAuthenticationOptionsExtensions
{
  private const string PortalInstanceKey = "Portal";
  internal const string DefaultInstanceKey = "Default";

  internal static bool ClaimsAuthEnabled(this ExternalAuthenticationOptions options)
  {
    return options.ClaimsAuth;
  }

  internal static bool OAuthLoginEnabled(
    this ExternalAuthenticationOptions options,
    string company,
    string ssoType)
  {
    return IdentityProviderDefinition.HasActiveProvider(options.GetInstanceKey(), company) && ExternalAuthenticationModule.IsFeatureEnabledForProvider(ssoType, out string _, company);
  }

  internal static string GetDefaultInstanceKey(this ExternalAuthenticationOptions options)
  {
    return "Default";
  }

  [PXInternalUseOnly]
  public static string GetInstanceKey(this ExternalAuthenticationOptions options)
  {
    string instanceKey = options.InstanceKey;
    if (!string.IsNullOrWhiteSpace(instanceKey))
      return instanceKey;
    return !PortalHelper.IsPortalContext() ? "Default" : "Portal";
  }

  internal static string GetReturnUrl(
    this ExternalAuthenticationOptions externalAuthenticationOptions,
    string realm,
    Dictionary<string, string> parameters)
  {
    return ExternalAuthenticationOptionsExtensions.GetUrl(externalAuthenticationOptions.ReturnUrl, realm, parameters);
  }

  internal static string GetAuthUrl(
    this ExternalAuthenticationOptions externalAuthenticationOptions,
    string realm,
    Dictionary<string, string> parameters)
  {
    return ExternalAuthenticationOptionsExtensions.GetUrl(externalAuthenticationOptions.AuthUrl, realm, parameters);
  }

  private static string GetUrl(string page, string realm, Dictionary<string, string> parameters)
  {
    string url = realm ?? string.Empty;
    if (!string.IsNullOrEmpty(url) && !url.EndsWith("/"))
      url += "/";
    if (!string.IsNullOrEmpty(page))
      url += page;
    if (parameters == null || parameters.Count <= 0)
      return url;
    if (!string.IsNullOrEmpty(url) && !url.EndsWith("?"))
      url += "?";
    return url + string.Join("&", parameters.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (p => $"{p.Key}={HttpUtility.UrlEncode(p.Value)}")).ToArray<string>());
  }

  [PXInternalUseOnly]
  public static string GetAuthUrl(
    this ExternalAuthenticationOptions externalAuthenticationOptions,
    string returnUrl)
  {
    return "~/Frames/AuthDock.ashx?_returnUrl_=" + HttpUtility.UrlEncode(returnUrl);
  }
}
