// Decompiled with JetBrains decompiler
// Type: PX.Security.BasicAuthenticationNoCompany
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;

#nullable disable
namespace PX.Security;

internal static class BasicAuthenticationNoCompany
{
  private const string BasicNoCompanyAuthenticationScheme = "acumatica:legacy:basic:no-company";

  internal static AuthenticationBuilder AddLegacyBasicNoCompany(this AuthenticationBuilder builder)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return builder.AddLegacyBasic("acumatica:legacy:basic:no-company", BasicAuthenticationNoCompany.\u003C\u003EO.\u003C0\u003E__PrepareUsername ?? (BasicAuthenticationNoCompany.\u003C\u003EO.\u003C0\u003E__PrepareUsername = new Func<HttpContext, string, string>(BasicAuthenticationNoCompany.PrepareUsername)));
  }

  internal static IAuthenticationManagerLocationOptionsBuilder WithBasicNoCompany(
    this IAuthenticationManagerLocationOptionsBuilder builder)
  {
    return builder.WithScheme("acumatica:legacy:basic:no-company");
  }

  private static string PrepareUsername(HttpContext context, string username)
  {
    if (string.IsNullOrEmpty(BasicAuthenticationNoCompany.GetCompanyIDFromUserName(username)))
    {
      string companyIdFromUrl = BasicAuthenticationNoCompany.GetCompanyIDFromURL(context);
      if (!string.IsNullOrWhiteSpace(companyIdFromUrl))
        return $"{username}@{companyIdFromUrl}";
    }
    return username;
  }

  private static string GetCompanyIDFromURL(HttpContext context)
  {
    string str = context.Request.Query["CompanyID"].ToString();
    return !string.IsNullOrWhiteSpace(str) ? str : (string) null;
  }

  private static string GetCompanyIDFromUserName(string username)
  {
    if (string.IsNullOrWhiteSpace(username))
      return (string) null;
    int length = username.IndexOf('@');
    return length < 0 || length >= username.Length - 1 ? (string) null : username.Substring(0, length);
  }
}
