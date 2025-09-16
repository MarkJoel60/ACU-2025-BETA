// Decompiled with JetBrains decompiler
// Type: PX.Data.Auth.ExternalAuthenticationService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using PX.Api;
using PX.Common;
using PX.Data.Access.ActiveDirectory;
using PX.Export.Authentication;
using PX.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Security;

#nullable disable
namespace PX.Data.Auth;

internal class ExternalAuthenticationService : 
  IExternalAuthenticationService,
  IExternalAuthenticationUiService
{
  private const string SilentLoginKey = "SilentLogin";
  private const string SilentLoginNone = "None";
  private const string FederatedProviderName = "Federated";
  internal const string Federation = "Federation";
  private readonly ExternalAuthenticationModule _externalAuthenticationModule;
  private readonly ExternalAuthenticationOptions _options;
  private readonly Func<string, IExternalAuthenticationProvider> _providerFactory;
  private readonly IUserManagementService _userManagementService;
  private readonly IActiveDirectoryProvider _activeDirectoryProvider;
  private readonly IFederationAuthentication _federationAuthentication;

  public ExternalAuthenticationService(
    ExternalAuthenticationModule externalAuthenticationModule,
    IOptions<ExternalAuthenticationOptions> options,
    Func<string, IExternalAuthenticationProvider> providerFactory,
    IUserManagementService userManagementService,
    IActiveDirectoryProvider activeDirectoryProvider,
    IFederationAuthentication federationAuthentication)
  {
    this._externalAuthenticationModule = externalAuthenticationModule;
    this._providerFactory = providerFactory;
    this._userManagementService = userManagementService;
    this._activeDirectoryProvider = activeDirectoryProvider;
    this._federationAuthentication = federationAuthentication;
    this._options = options.Value;
  }

  string IExternalAuthenticationService.FederatedProviderName => "Federated";

  public IEnumerable<(string Name, string Realm)> GetProviders()
  {
    return IdentityProviderDefinition.GetProviders(this._options.GetInstanceKey()).Select<IdentityProviderDefinition.IdentityProvider, (string, string)>((Func<IdentityProviderDefinition.IdentityProvider, (string, string)>) (p => (p.Name, p.Realm)));
  }

  public string GetCurrentCompany(string provider)
  {
    return !provider.OrdinalEquals("Federated") ? IdentityProviderDefinition.GetCurrentCompany(this._options.GetInstanceKey(), provider) : ((IEnumerable<string>) PXDatabase.Companies).First<string>();
  }

  public bool FederatedLoginEnabled(string company)
  {
    return (this._options.ClaimsAuthEnabled() || this._activeDirectoryProvider.IsAzureActiveDirectoryProvider() || this._activeDirectoryProvider.IsClaimActiveDirectoryProvider()) && ExternalAuthenticationModule.IsFeatureEnabledForProvider("Federated", out string _, company);
  }

  bool IExternalAuthenticationUiService.OAuthProviderLoginEnabled(
    string providerName,
    string company)
  {
    return IdentityProviderDefinition.GetProviderConfiguration(this._options.GetInstanceKey(), providerName, company, true) != null && ExternalAuthenticationModule.IsFeatureEnabledForProvider(providerName, out string _, company);
  }

  public bool ClaimsAuthEnabled() => this._options.ClaimsAuthEnabled();

  bool IExternalAuthenticationUiService.AssociateLoginEnabled(HttpContext context)
  {
    if (!this._options.SelfAssociate || !PXContext.Session.IsSessionEnabled)
      return false;
    string key = context.Request.QueryString["exceptionID"];
    return key != null && PXContext.Session.Exception[key] is PXExternalLoginAssociateException;
  }

  public bool IsFeatureEnabledForProvider(
    string providerName,
    out string disabledFeature,
    string company)
  {
    return ExternalAuthenticationModule.IsFeatureEnabledForProvider(providerName, out disabledFeature, company);
  }

  public bool HasActiveProvider(string company)
  {
    return IdentityProviderDefinition.HasActiveProvider(this._options.GetInstanceKey(), company);
  }

  public void SignInFederation(HttpContext context, string company, string locale)
  {
    context.Response.Redirect(this._federationAuthentication.GetRedirectUrl(context.Request, ExternalAuthenticationParameters.CreateSignInParameters(context.Request, (string) null, company, locale)));
  }

  public void SignInOAuth(HttpContext context, string provider, string company, string locale)
  {
    this.SignInOAuth(provider, context, ExternalAuthenticationParameters.CreateSignInParameters(context.Request, provider, company, locale), true);
  }

  void IExternalAuthenticationUiService.SignInSilent(
    HttpContext context,
    string company,
    string locale)
  {
    switch (context.Request.QueryString["SilentLogin"] ?? this._options.SilentLogin)
    {
      case "Federation":
        if (!this.FederatedLoginEnabled(company))
          break;
        this.SignInFederation(context, company, locale);
        break;
    }
  }

  void IExternalAuthenticationService.SignInFederation(
    HttpRequest request,
    HttpResponse response,
    Dictionary<string, string> parameters,
    bool hasCompany)
  {
    if (!hasCompany)
      parameters.RemoveCompany();
    response.Redirect(this._federationAuthentication.GetRedirectUrl(request, parameters));
  }

  public void SignInOAuth(
    string providerName,
    HttpContext context,
    Dictionary<string, string> parameters,
    bool hasCompany = true)
  {
    throw new NotSupportedException();
  }

  public void TryAssociate(HttpContext context, object userKey)
  {
    if (!this._options.SelfAssociate || !PXContext.Session.IsSessionEnabled)
      return;
    string str1;
    switch (userKey)
    {
      case string str2:
        str1 = str2;
        break;
      case Guid guid:
        str1 = guid.ToString();
        break;
      default:
        return;
    }
    string key = context.Request.QueryString["exceptionID"];
    if (key == null)
      return;
    PXExternalLoginAssociateException ex = PXContext.Session.Exception[key] as PXExternalLoginAssociateException;
    if (ex == null)
      return;
    if (!string.IsNullOrWhiteSpace(ex.ProviderID))
    {
      Guid result;
      Guid? userId = Guid.TryParse(str1, out result) ? new Guid?(result) : new Guid?();
      if (this._providerFactory(ex.ProviderID).Return<IExternalAuthenticationProvider, bool>((Func<IExternalAuthenticationProvider, bool>) (p => p.TryAssociate(userId, ex.ExternalID, ex.ExternalEmail)), false))
        return;
    }
    if (this._externalAuthenticationModule.GetUser(context.GetRequestBase(), ex.ProviderID, str1, ex.ExternalID) == null)
      throw new PXException("The system cannot associate the specified user with the provided external login. Please make sure that the specified user is allowed to authenticate with external logins.");
  }

  public void AssociateOAuth(
    HttpContext context,
    string providerName,
    string company,
    Guid userid)
  {
    Dictionary<string, string> parameters = ExternalAuthenticationParameters.Create(providerName);
    parameters.SetCompany(company);
    parameters.SetAssociation(userid);
    this.SignInOAuth(providerName, context, parameters, true);
  }

  void IExternalAuthenticationUiService.CancelAssociate(HttpContext context)
  {
    if (!PXContext.Session.IsSessionEnabled)
      return;
    string key = context.Request.QueryString["exceptionID"];
    if (key == null)
      return;
    PXContext.Session.Exception[key] = (Exception) null;
    Redirector.SmartRedirect(HttpContext.Current, PXUrl.IgnoreQueryParameter(context.Request.RawUrl, "exceptionID"));
  }

  void IExternalAuthenticationUiService.SignOut(HttpContext context, string redirectUrl)
  {
    if (context == null || !this._options.ExternalLogout || !this.IsUserExternal(this._userManagementService.GetUserAndMarkOnline(context)))
      return;
    Dictionary<string, string> parameters = new Dictionary<string, string>()
    {
      {
        "SilentLogin",
        "None"
      }
    };
    context.Response.Redirect(this._federationAuthentication.GetSignOutQueryString(redirectUrl, parameters));
  }

  public string GetSignOutQueryString(HttpContext httpContext, string redirectUrl)
  {
    if (!this._options.ExternalLogout || !this.IsUserExternal(this._userManagementService.GetUserAndMarkOnline(httpContext)))
      return (string) null;
    return this._federationAuthentication.GetSignOutQueryString(redirectUrl, new Dictionary<string, string>()
    {
      {
        "SilentLogin",
        "None"
      }
    });
  }

  public ClaimsPrincipal Authenticate(HttpContext context, out string company)
  {
    return this._externalAuthenticationModule.Authenticate(context, out company);
  }

  private bool IsUserExternal(MembershipUser membershipUser)
  {
    if (!(membershipUser is MembershipUserExt membershipUserExt))
      return false;
    if (membershipUserExt.Source == 1 && this._activeDirectoryProvider.IsAzureActiveDirectoryProvider())
      return true;
    return membershipUserExt.Source == 2 && this._options.ClaimsAuthEnabled();
  }
}
