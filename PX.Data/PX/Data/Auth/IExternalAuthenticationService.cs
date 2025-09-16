// Decompiled with JetBrains decompiler
// Type: PX.Data.Auth.IExternalAuthenticationService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;

#nullable disable
namespace PX.Data.Auth;

[PXInternalUseOnly]
public interface IExternalAuthenticationService
{
  IEnumerable<(string Name, string Realm)> GetProviders();

  string GetCurrentCompany(string provider);

  bool FederatedLoginEnabled(string company);

  bool ClaimsAuthEnabled();

  bool IsFeatureEnabledForProvider(string providerName, out string disabledFeature, string company);

  bool HasActiveProvider(string company);

  void SignInFederation(
    HttpRequest request,
    HttpResponse response,
    Dictionary<string, string> parameters,
    bool hasCompany);

  void SignInOAuth(
    string providerName,
    HttpContext context,
    Dictionary<string, string> parameters,
    bool hasCompany = true);

  void TryAssociate(HttpContext context, object userKey);

  void AssociateOAuth(HttpContext context, string providerName, string company, Guid userid);

  string GetSignOutQueryString(HttpContext httpContext, string redirectUrl);

  ClaimsPrincipal Authenticate(HttpContext context, out string company);

  string FederatedProviderName { get; }
}
