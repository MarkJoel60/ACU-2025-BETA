// Decompiled with JetBrains decompiler
// Type: PX.Data.Auth.ExternalAuthenticationParameters
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Web;

#nullable disable
namespace PX.Data.Auth;

internal static class ExternalAuthenticationParameters
{
  internal static string GetQueryParameter(this HttpRequest request, string param)
  {
    return new HttpRequestWrapper(request).GetQueryParameter(param);
  }

  internal static string GetQueryParameter(this HttpRequestBase request, string param)
  {
    string str1 = request.QueryString[param];
    if (str1 == null)
    {
      string str2 = request.QueryString["state"];
      if (!string.IsNullOrEmpty(str2))
      {
        string str3 = str2.Trim('?');
        string[] separator = new string[1]{ "&" };
        foreach (string str4 in str3.Split(separator, StringSplitOptions.RemoveEmptyEntries))
        {
          if (str4.StartsWith(param + "="))
            return HttpUtility.UrlDecode(str4.Substring(param.Length + 1));
        }
      }
    }
    return HttpUtility.UrlDecode(str1);
  }

  internal static Dictionary<string, string> ParseQueryParameters(this HttpRequestBase request)
  {
    Dictionary<string, string> queryParameters = new Dictionary<string, string>();
    foreach (string key in request.QueryString.Keys)
    {
      if (key.StartsWith("_") && key.EndsWith("_") && !key.StartsWith("__") && !key.EndsWith("__"))
      {
        string str1 = request.QueryString[key];
        if (string.Equals(key, "state", StringComparison.InvariantCultureIgnoreCase))
        {
          if (!string.IsNullOrEmpty(str1))
          {
            string str2 = str1.Trim('?');
            string[] separator = new string[1]{ "&" };
            foreach (string str3 in str2.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
              if (str3.Contains("="))
              {
                string[] strArray = str3.Split(new string[1]
                {
                  "="
                }, StringSplitOptions.None);
                if (strArray.Length == 2 && strArray[0].StartsWith("_") && strArray[0].EndsWith("_") && !strArray[0].StartsWith("__") && !strArray[0].EndsWith("__"))
                  queryParameters[strArray[0]] = strArray[1];
              }
            }
          }
        }
        else
          queryParameters[key] = str1;
      }
    }
    return queryParameters;
  }

  internal static Dictionary<string, string> Create(string provider)
  {
    return new Dictionary<string, string>()
    {
      {
        "_type_",
        provider
      }
    };
  }

  internal static string GetProvider(this HttpRequest request)
  {
    return request.GetQueryParameter("_type_");
  }

  internal static void SetSessionId(
    this IDictionary<string, string> parameters,
    HttpRequest request)
  {
    string suffix = PXSessionStateStore.GetSuffix(request);
    if (string.IsNullOrEmpty(suffix))
      return;
    parameters["_sessionid_"] = suffix;
  }

  internal static string GetSessionId(this HttpRequest request)
  {
    return request.GetQueryParameter("_sessionid_");
  }

  internal static string IgnoreSessionId(this Uri url)
  {
    return PXUrl.IgnoreQueryParameter(url.AbsoluteUri, "_sessionid_");
  }

  internal static void SetAssociation(this IDictionary<string, string> parameters, Guid userid)
  {
    parameters["_associate_"] = userid.ToString();
  }

  internal static string GetAssociation(this HttpRequest request)
  {
    return request.GetQueryParameter("_associate_");
  }

  internal static void SetReturnUrlIfNotEmpty(
    this IDictionary<string, string> parameters,
    params string[] returnUrls)
  {
    foreach (string returnUrl in returnUrls)
    {
      if (!string.IsNullOrEmpty(returnUrl))
      {
        parameters["_returnUrl_"] = returnUrl;
        break;
      }
    }
  }

  internal static string GetReturnUrl(this HttpRequestBase request)
  {
    return request.GetQueryParameter("_returnUrl_");
  }

  internal static void SetBranchIfNotEmpty(
    this IDictionary<string, string> parameters,
    string branch)
  {
    if (string.IsNullOrEmpty(branch))
      return;
    parameters["_branch_"] = branch;
  }

  internal static string GetBranch(this HttpRequest request)
  {
    return request.GetQueryParameter("_branch_");
  }

  internal static void SetLocaleIfNotEmpty(
    this IDictionary<string, string> parameters,
    string locale)
  {
    if (string.IsNullOrEmpty(locale))
      return;
    parameters["_locale_"] = locale;
  }

  internal static string GetLocale(this HttpRequest request)
  {
    return request.GetQueryParameter("_locale_");
  }

  internal static void SetCompanyIfNotEmpty(
    this IDictionary<string, string> parameters,
    string company)
  {
    if (string.IsNullOrEmpty(company))
      return;
    parameters.SetCompany(company);
  }

  internal static void SetCompany(this IDictionary<string, string> parameters, string company)
  {
    parameters["_company_"] = company;
  }

  internal static void RemoveCompany(this IDictionary<string, string> parameters)
  {
    parameters.Remove("_company_");
  }

  internal static string GetCompany(this IDictionary<string, string> parameters)
  {
    string str;
    return !parameters.TryGetValue("_company_", out str) ? (string) null : str;
  }

  internal static string GetCompany(this HttpRequest request)
  {
    return request.GetQueryParameter("_company_");
  }

  internal static Dictionary<string, string> CreateSignInParameters(
    HttpRequest request,
    string provider,
    string company,
    string locale,
    string returnUrl = null)
  {
    Dictionary<string, string> parameters = ExternalAuthenticationParameters.Create(provider);
    parameters.SetCompanyIfNotEmpty(company);
    parameters.SetLocaleIfNotEmpty(locale);
    parameters.SetReturnUrlIfNotEmpty(returnUrl, request.QueryString["ReturnUrl"]);
    if (!string.IsNullOrEmpty(request.QueryString["exceptionID"]))
      parameters["exceptionID"] = request.QueryString["exceptionID"];
    return parameters;
  }

  internal static class Keys
  {
    internal const string State = "state";
    internal const string Session = "_sessionid_";
    internal const string Associate = "_associate_";
    internal const string ReturnUrl = "_returnUrl_";
    internal const string Type = "_type_";
    internal const string Branch = "_branch_";
    internal const string Locale = "_locale_";
    internal const string Company = "_company_";
  }
}
