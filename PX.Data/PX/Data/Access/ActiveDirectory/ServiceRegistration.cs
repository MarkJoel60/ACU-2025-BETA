// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.ServiceRegistration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

#nullable enable
namespace PX.Data.Access.ActiveDirectory;

internal class ServiceRegistration : Module
{
  protected virtual void Load(
  #nullable disable
  ContainerBuilder builder)
  {
    OptionsContainerBuilderExtensions.Configure<PX.Data.Access.ActiveDirectory.Options>(builder, (Action<PX.Data.Access.ActiveDirectory.Options, IConfiguration>) ((options, configuration) => options.BindFrom(configuration)));
    RegistrationExtensions.RegisterType<GraphServiceClientFactory>(builder).As<IGraphServiceClientFactory>().SingleInstance();
    RegistrationExtensions.RegisterType<GraphApiClientConfigurationProvider>(builder).As<IGraphApiClientConfigurationProvider>().SingleInstance();
    RegistrationExtensions.RegisterType<ExponentialExpirationStorage>(builder).As<IExpirationStorage>().SingleInstance();
    RegistrationExtensions.Register<IActiveDirectoryProvider>(builder, (Func<IComponentContext, IActiveDirectoryProvider>) (context => ActiveDirectoryProvider.CreateInstance(ResolutionExtensions.Resolve<IServiceProvider>(context)) ?? ActiveDirectoryProviderEmpty.Instance)).As<IActiveDirectoryProvider>().SingleInstance();
    ApplicationStartActivation.RunOnApplicationStart<IActiveDirectoryProvider, IOptions<PX.Data.Access.ActiveDirectory.Options>>(builder, (Action<IActiveDirectoryProvider, IOptions<PX.Data.Access.ActiveDirectory.Options>>) ((provider, options) =>
    {
      if (!provider.IsEnabled() || !options.Value.Preload)
        return;
      Task.Run((System.Action) (() =>
      {
        provider.GetUsers();
        provider.GetGroups();
      }));
    }), (string) null);
  }
}
