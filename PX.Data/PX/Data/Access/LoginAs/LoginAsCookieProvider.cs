// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.LoginAs.LoginAsCookieProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using IdentityModel;
using Microsoft.Extensions.Options;
using PX.Common.Extensions;
using PX.Hosting.MachineKey;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;

#nullable disable
namespace PX.Data.Access.LoginAs;

internal sealed class LoginAsCookieProvider : ILoginAsCookieProvider
{
  private readonly byte[] _secretKey;
  private const string LoginAsConst = "LoginAs";
  private const string UsernameConst = "Username";
  private const string SignConst = "Sign";

  public LoginAsCookieProvider(IOptions<MachineKeyOptions> options)
  {
    this._secretKey = Encoding.UTF8.GetBytes(options.Value.ValidationKey);
  }

  public void Write(HttpContext context, string loginAs)
  {
    HttpContext httpContext = context;
    if (httpContext == null)
      return;
    httpContext.Response.AddOnSendingHeadersIfHeadersNotWritten((System.Action<HttpContext>) (c => context.Response.Cookies.Add(new HttpCookie("LoginAs")
    {
      ["Username"] = loginAs,
      ["Sign"] = this.GetHash(loginAs),
      Expires = System.DateTime.Now.AddDays(3.0)
    })));
  }

  public string Get(HttpContext context)
  {
    HttpCookie cookie = context?.Request.Cookies["LoginAs"];
    if (cookie == null)
      return (string) null;
    string loginAs = cookie["Username"];
    string str = cookie["Sign"];
    if (!string.IsNullOrEmpty(loginAs) && TimeConstantComparer.IsEqual(str, this.GetHash(loginAs)))
      return loginAs;
    this.Remove(context);
    return (string) null;
  }

  public void Remove(HttpContext context)
  {
    if (context?.Request.Cookies["LoginAs"] == null)
      return;
    context.Response.AddOnSendingHeadersIfHeadersNotWritten((System.Action<HttpContext>) (c => context.Response.Cookies.Add(new HttpCookie("LoginAs")
    {
      Expires = System.DateTime.Now.AddDays(-1.0)
    })));
  }

  private string GetHash(string loginAs)
  {
    string stringForLoginAs = this.GetVerifyStringForLoginAs(loginAs);
    using (HMACSHA256 hmacshA256 = new HMACSHA256(this._secretKey))
      return Convert.ToBase64String(hmacshA256.ComputeHash(Encoding.UTF8.GetBytes(stringForLoginAs)));
  }

  private string GetVerifyStringForLoginAs(string loginAs)
  {
    string str = string.Empty;
    if (!string.IsNullOrEmpty(Thread.CurrentPrincipal?.Identity?.Name))
      str = Thread.CurrentPrincipal.Identity.Name;
    else if (!string.IsNullOrEmpty(HttpContext.Current?.User?.Identity?.Name))
      str = HttpContext.Current.User.Identity.Name;
    return $"{loginAs}${loginAs.Length}/{str}${str.Length}";
  }
}
