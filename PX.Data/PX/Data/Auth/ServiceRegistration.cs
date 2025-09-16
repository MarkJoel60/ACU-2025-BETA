// Decompiled with JetBrains decompiler
// Type: PX.Data.Auth.ServiceRegistration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Core;
using PX.Common;
using PX.Data.Update.Auth;
using System;
using System.Monads;

#nullable enable
namespace PX.Data.Auth;

[PXInternalUseOnly]
public class ServiceRegistration : Module
{
  protected virtual void Load(
  #nullable disable
  ContainerBuilder builder)
  {
    RegistrationExtensions.RegisterType<PXFederationAuthenticationModule>(builder).OnActivating((System.Action<IActivatingEventArgs<PXFederationAuthenticationModule>>) (args => args.Instance.InitializeConfiguration())).As<IFederationAuthentication>().SingleInstance();
    RegistrationExtensions.RegisterType<ExternalAuthenticationModule>(builder).SingleInstance();
    object obj;
    RegistrationExtensions.Register<Func<string, IExternalAuthenticationProvider>>(builder, (Func<IComponentContext, Func<string, IExternalAuthenticationProvider>>) (c => MaybeObjects.With<IComponentContext, Func<string, IExternalAuthenticationProvider>>(ResolutionExtensions.Resolve<IComponentContext>(c), (Func<IComponentContext, Func<string, IExternalAuthenticationProvider>>) (ctx => (Func<string, IExternalAuthenticationProvider>) (name => !ResolutionExtensions.TryResolveNamed(ctx, name, typeof (IExternalAuthenticationProvider), ref obj) ? (IExternalAuthenticationProvider) null : MaybeObjects.OfType<IExternalAuthenticationProvider>(obj)))))).SingleInstance();
    RegistrationExtensions.RegisterType<ExternalAuthenticationService>(builder).As<IExternalAuthenticationService>().As<IExternalAuthenticationUiService>().SingleInstance();
    RegistrationExtensions.RegisterType<ErpLoginUiService>(builder).As<ILoginUiService>().SingleInstance();
    RegistrationExtensions.RegisterType<PortalLoginUiService>(builder).As<ILoginUiService>().SingleInstance();
    RegistrationExtensions.RegisterComposite<CompositeLoginUiService, ILoginUiService>(builder).SingleInstance();
  }
}
