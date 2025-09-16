// Decompiled with JetBrains decompiler
// Type: PX.Api.AuthorizationViaLoginServiceAttributeBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

#nullable disable
namespace PX.Api;

public abstract class AuthorizationViaLoginServiceAttributeBase : AuthorizationFilterAttribute
{
  private readonly string _prefix;

  protected AuthorizationViaLoginServiceAttributeBase(string prefix) => this._prefix = prefix;

  public virtual void OnAuthorization(HttpActionContext actionContext)
  {
    if (!actionContext.Request.GetRequestScopedService<ILoginService>().IsUserAuthenticated(false, this._prefix))
      throw new HttpResponseException(this.CreateHttpResponseMessage(actionContext, HttpStatusCode.Unauthorized));
  }

  protected virtual HttpResponseMessage CreateHttpResponseMessage(
    HttpActionContext actionContext,
    HttpStatusCode defaultHttpStatusCode)
  {
    return new HttpResponseMessage(defaultHttpStatusCode);
  }
}
