// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.LoginAs.LoginAsCookieProviderExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Web;

#nullable disable
namespace PX.Data.Access.LoginAs;

public static class LoginAsCookieProviderExtensions
{
  public static void VerifyWithSession(
    this ILoginAsCookieProvider loginAsProvider,
    HttpContext context)
  {
    if (loginAsProvider == null || !PXContext.Session.IsSessionEnabled)
      return;
    string a = loginAsProvider.Get(context);
    if (string.IsNullOrEmpty(a) || string.Equals(a, PXContext.PXIdentity.IdentityName, StringComparison.OrdinalIgnoreCase))
      return;
    loginAsProvider.Remove(context);
  }
}
