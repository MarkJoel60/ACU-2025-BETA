// Decompiled with JetBrains decompiler
// Type: PX.Data.Handlers.ServiceRegistration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Builder;
using PX.Security;
using System;
using System.Web;
using System.Web.Routing;

#nullable disable
namespace PX.Data.Handlers;

internal class ServiceRegistration : Module
{
  protected virtual void Load(ContainerBuilder builder)
  {
    OptionsContainerBuilderExtensions.Configure<AuthenticationManagerOptions>(builder, (System.Action<AuthenticationManagerOptions>) (options => CookieAuthenticationExtensions.WithCookie(options.AddLocation("/PageUnload.axd"))));
    RegistrationExtensions.RegisterGeneric(builder, typeof (ServiceRegistration.RouteHandler<>)).SingleInstance();
  }

  private static void RegisterHandler<T>(
    ContainerBuilder builder,
    string url,
    bool isReusable,
    params string[] allowedMethods)
    where T : class, IHttpHandler
  {
    IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> iregistrationBuilder = RegistrationExtensions.AsSelf<T, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<T>(builder));
    if (isReusable)
      iregistrationBuilder.SingleInstance();
    else
      iregistrationBuilder.InstancePerDependency();
    ApplicationStartActivation.RunOnApplicationStart<ServiceRegistration.RouteHandler<T>>(builder, (System.Action<ServiceRegistration.RouteHandler<T>>) (routeHandler =>
    {
      RouteCollection routes = RouteTable.Routes;
      string url1 = url;
      RouteValueDictionary constraints = new RouteValueDictionary();
      constraints.Add("method", (object) new HttpMethodConstraint(allowedMethods));
      ServiceRegistration.RouteHandler<T> routeHandler1 = routeHandler;
      Route route = new Route(url1, (RouteValueDictionary) null, constraints, (IRouteHandler) routeHandler1);
      routes.Add((RouteBase) route);
    }), (string) null);
  }

  private class RouteHandler<T> : IRouteHandler where T : class, IHttpHandler
  {
    private readonly Func<T> _httpHandlerFactory;

    public RouteHandler(Func<T> httpHandlerFactory)
    {
      this._httpHandlerFactory = httpHandlerFactory;
    }

    IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
    {
      return (IHttpHandler) this._httpHandlerFactory();
    }
  }
}
