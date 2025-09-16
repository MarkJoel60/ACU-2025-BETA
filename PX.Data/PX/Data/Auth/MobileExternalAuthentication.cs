// Decompiled with JetBrains decompiler
// Type: PX.Data.Auth.MobileExternalAuthentication
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

#nullable disable
namespace PX.Data.Auth;

[PXInternalUseOnly]
public static class MobileExternalAuthentication
{
  private const string SourceKey = "_source_";
  private const string SourceMobile = "mobile";
  private const string SuccessResult = "success";
  private const string FailResult = "fail";
  private const string MobileAuthUrl = "Frames/MobileAuth.aspx";
  [PXInternalUseOnly]
  public static readonly string MobileAuthenticationPrefix = "mobile";

  [PXInternalUseOnly]
  public static void MobileSignIn(
    this IExternalAuthenticationService externalAuthenticationService,
    string provider,
    string returnUrl,
    string company,
    string branch,
    string locale,
    bool hasCompany)
  {
    Dictionary<string, string> parameters = ExternalAuthenticationParameters.Create(provider);
    parameters.Add("_source_", "mobile");
    parameters.SetLocaleIfNotEmpty(locale);
    parameters.SetReturnUrlIfNotEmpty(returnUrl);
    parameters.SetCompanyIfNotEmpty(company);
    parameters.SetBranchIfNotEmpty(branch);
    if (provider.OrdinalEquals(externalAuthenticationService.FederatedProviderName))
      externalAuthenticationService.SignInFederation(HttpContext.Current.Request, HttpContext.Current.Response, parameters, hasCompany);
    else
      externalAuthenticationService.SignInOAuth(provider, HttpContext.Current, parameters, hasCompany);
  }

  private static string GetSource(this HttpRequest request)
  {
    return request.GetQueryParameter("_source_");
  }

  internal static bool HasMobileSourceInQueryString(this HttpRequest request)
  {
    return request.GetSource() == "mobile";
  }

  internal static string GetAuthenticationPrefix(this HttpRequest request) => request.GetSource();

  private static string GetMobileAuthUrl(string result, string locale)
  {
    return $"~/Frames/MobileAuth.aspx?result={result}&locale={locale}";
  }

  [PXInternalUseOnly]
  public static string GetMobileRedirectUrl(string baseUrl)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(baseUrl);
    if (baseUrl.Last<char>() != '/')
      stringBuilder.Append('/');
    stringBuilder.Append("Frames/MobileAuth.aspx");
    return stringBuilder.ToString();
  }

  [PXInternalUseOnly]
  public static string GetFailedMobileAuthUrl(string locale)
  {
    return MobileExternalAuthentication.GetMobileAuthUrl("fail", locale);
  }

  [PXInternalUseOnly]
  public static string GetSuccessMobileAuthUrl(string locale)
  {
    return MobileExternalAuthentication.GetMobileAuthUrl("success", locale);
  }

  private static void RedirectToMobileAuth(this HttpContext context, string result, string locale)
  {
    Redirector.Redirect(context, MobileExternalAuthentication.GetMobileAuthUrl(result, locale));
  }

  internal static void RedirectToFailedMobileAuth(this HttpContext context, string locale)
  {
    context.RedirectToMobileAuth("fail", locale);
  }

  internal static void RedirectToSuccessMobileAuth(this HttpContext context, string locale)
  {
    context.RedirectToMobileAuth("success", locale);
  }
}
