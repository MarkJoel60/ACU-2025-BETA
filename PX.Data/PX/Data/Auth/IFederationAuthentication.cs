// Decompiled with JetBrains decompiler
// Type: PX.Data.Auth.IFederationAuthentication
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Security.Claims;
using System.Web;

#nullable disable
namespace PX.Data.Auth;

internal interface IFederationAuthentication
{
  ClaimsPrincipal TryAuthenticate(HttpRequestWrapper wrapper, out string company);

  string GetRedirectUrl(HttpRequest request, Dictionary<string, string> parameters);

  string GetSignOutQueryString(string redirectUrl, Dictionary<string, string> parameters);
}
