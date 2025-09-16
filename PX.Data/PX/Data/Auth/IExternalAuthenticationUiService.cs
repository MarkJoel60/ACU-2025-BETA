// Decompiled with JetBrains decompiler
// Type: PX.Data.Auth.IExternalAuthenticationUiService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Web;

#nullable disable
namespace PX.Data.Auth;

[PXInternalUseOnly]
public interface IExternalAuthenticationUiService
{
  bool FederatedLoginEnabled(string company);

  bool OAuthProviderLoginEnabled(string providerName, string company);

  bool AssociateLoginEnabled(HttpContext context);

  void SignInOAuth(HttpContext context, string provider, string company, string locale);

  void SignInFederation(HttpContext context, string company, string locale);

  void SignInSilent(HttpContext context, string company, string locale);

  void CancelAssociate(HttpContext context);

  void SignOut(HttpContext context, string redirectUrl);
}
