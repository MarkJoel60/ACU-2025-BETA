// Decompiled with JetBrains decompiler
// Type: PX.Export.Authentication.BasicAuthenticationHandler
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using PX.AspNetCore.Authentication;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;

#nullable disable
namespace PX.Export.Authentication;

internal sealed class BasicAuthenticationHandler : 
  AuthenticationHandler<BasicAuthenticationOptions>,
  ICanVerifyContext
{
  private const string Charset = "UTF-8";
  private const string Basic = "Basic";
  private const string AuthenticationType = "CustomBasic";
  private readonly IPXLogin _pxLogin;

  public BasicAuthenticationHandler(
    IOptionsMonitor<BasicAuthenticationOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ISystemClock clock,
    IPXLogin pxLogin)
    : base(options, logger, encoder, clock)
  {
    this._pxLogin = pxLogin;
  }

  protected virtual Task<AuthenticateResult> HandleAuthenticateAsync()
  {
    try
    {
      return Task.FromResult<AuthenticateResult>(this.HandleAuthenticate());
    }
    catch (Exception ex)
    {
      return Task.FromResult<AuthenticateResult>(AuthenticateResult.Fail(ex));
    }
  }

  private AuthenticateResult HandleAuthenticate()
  {
    string str = ((IEnumerable<string>) (object) this.Context.Request.Headers["Authorization"]).FirstOrDefault<string>((Func<string, bool>) (value => value != null && value.StartsWith("Basic")));
    if (str == null)
      return AuthenticateResult.NoResult();
    string s = str.Substring("Basic".Length).Trim();
    string[] strArray = Encoding.GetEncoding("UTF-8").GetString(Convert.FromBase64String(s)).Split(new char[1]
    {
      ':'
    }, 2);
    if (strArray.Length < 2)
      return AuthenticateResult.NoResult();
    AuthenticationTicket authenticationTicket = new AuthenticationTicket(new ClaimsPrincipal((IIdentity) new ClaimsIdentity(this._pxLogin.Authenticate(this.TryPrepareUsername(strArray[0]), strArray[1]).Identity, (IEnumerable<Claim>) null, "CustomBasic", (string) null, (string) null)), this.Scheme.Name);
    PX.AspNetCore.Authentication.Extensions.SetEnvironmentInitializer(authenticationTicket.Properties, new Func<HttpContext, ClaimsPrincipal, Task>(this.InitializeEnvironmentAsync));
    return AuthenticateResult.Success(authenticationTicket);
  }

  private string TryPrepareUsername(string username)
  {
    try
    {
      return this.Options.PrepareUsername(this.Context, username);
    }
    catch (StackOverflowException ex)
    {
      throw;
    }
    catch (OutOfMemoryException ex)
    {
      throw;
    }
    catch (Exception ex)
    {
      LoggerExtensions.LogError(this.Logger, ex, "An error {Message} occurred while preparing username for {AuthenticationScheme}", new object[2]
      {
        (object) ex.Message,
        (object) this.Scheme.Name
      });
      return username;
    }
  }

  private Task InitializeEnvironmentAsync(HttpContext _, ClaimsPrincipal principal)
  {
    using (PXLoginScope pxLoginScope = new PXLoginScope(principal.Identity.Name, Array.Empty<string>()))
      this._pxLogin.TrackAndFinishLogin(HttpContext.Current.GetContextBase(), pxLoginScope.UserName, pxLoginScope.CompanyName, pxLoginScope.Branch);
    return Task.CompletedTask;
  }

  Task<bool> ICanVerifyContext.VerifyAsync()
  {
    IIdentity identity = this.Context.User?.Identity;
    return Task.FromResult<bool>(identity != null && identity.IsAuthenticated && identity.AuthenticationType == "CustomBasic");
  }

  private void ChallengeOrForbid()
  {
    HeaderDictionaryExtensions.Append(this.Context.Response.Headers, "WWW-Authenticate", StringValues.op_Implicit("Basic charset=\"UTF-8\""));
  }

  protected virtual Task HandleChallengeAsync(AuthenticationProperties properties)
  {
    try
    {
      this.ChallengeOrForbid();
      if (this.Options.SetResponseStatusCode)
        this.Response.StatusCode = 401;
    }
    catch (Exception ex)
    {
      LoggerExtensions.LogError(this.Logger, ex, "{AuthenticationScheme} was not challenged. Failure message: {FailureMessage}", new object[2]
      {
        (object) this.Scheme.Name,
        (object) ex.Message
      });
    }
    return Task.CompletedTask;
  }

  protected virtual Task HandleForbiddenAsync(AuthenticationProperties properties)
  {
    try
    {
      this.ChallengeOrForbid();
      if (this.Options.SetResponseStatusCode)
        this.Response.StatusCode = 403;
    }
    catch (Exception ex)
    {
      LoggerExtensions.LogError(this.Logger, ex, "{AuthenticationScheme} was not forbidden. Failure message: {FailureMessage}", new object[2]
      {
        (object) this.Scheme.Name,
        (object) ex.Message
      });
    }
    return Task.CompletedTask;
  }
}
