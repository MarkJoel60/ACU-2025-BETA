// Decompiled with JetBrains decompiler
// Type: PX.Async.ServiceRegistrationExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PX.Async.Internal;
using PX.Common.Configuration;
using PX.Data;
using PX.Distributed.Messaging;
using PX.Distributed.Stores;
using PX.Reports;
using Serilog;
using System;

#nullable enable
namespace PX.Async;

internal static class ServiceRegistrationExtensions
{
  internal static void RegisterLongOperations(this 
  #nullable disable
  ContainerBuilder builder)
  {
    OptionsContainerBuilderExtensions.Configure<ThreadPoolOptions>(builder, (Action<ThreadPoolOptions, IConfiguration>) ((options, configuration) => ConfigurationBinder.Bind(configuration, (object) options, (System.Action<BinderOptions>) (binderOptions => binderOptions.BindNonPublicProperties = true))));
    OptionsContainerBuilderExtensions.Configure<MultiNodeTaskStateStoreOptions>(builder, (Action<MultiNodeTaskStateStoreOptions, IConfiguration>) ((options, configuration) =>
    {
      options.Timeout = PXSessionStateStore.GetWebConfigSessionTimeout() ?? PXSessionStateStore.DefaultSessionTimeout;
      options.ReadFrom(configuration);
    }));
    OptionsContainerBuilderExtensions.Configure<ApplicationStoreOptions<PXLongOperationState>, IOptions<MultiNodeTaskStateStoreOptions>>(builder, (Action<ApplicationStoreOptions<PXLongOperationState>, IOptions<MultiNodeTaskStateStoreOptions>>) ((options, source) => options.DefaultEntryOptions = new ApplicationStoreEntryOptions()
    {
      SlidingExpiration = new TimeSpan?(source.Value.Timeout)
    }));
    RegistrationExtensions.Register<PXTaskPool>(builder, (Func<IComponentContext, PXTaskPool>) (context => !ResolutionExtensions.Resolve<IConfiguration>(context).IsClusterEnabled() ? (PXTaskPool) new PXSingleNodeTaskPool() : (PXTaskPool) new PXMultiNodeTaskPool(ResolutionExtensions.Resolve<ILogger>(context).ForContext<PXMultiNodeTaskPool>(), ResolutionExtensions.Resolve<IApplicationStore<PXLongOperationState>>(context), ResolutionExtensions.Resolve<IPXSyncMessageBus>(context)))).As<PXTaskPool>().As<IPXMessageHandler<AbortLongOperationMessage>>().As<IPXMessageHandler<RemoveLongOperationMessage>>().SingleInstance();
    OptionsContainerBuilderExtensions.Configure<PXMessageBusOptions>(builder, (System.Action<PXMessageBusOptions>) (options => options.AddSubscription<AbortLongOperationMessage>().AddSubscription<RemoveLongOperationMessage>()));
    RegistrationExtensions.Register<LongOperationManager>(builder, (Func<IComponentContext, LongOperationManager>) (context => ResolutionExtensions.Resolve<LongOperationManager>(context))).As<ILongOperationManager>().As<ILongOperationTaskManager>().SingleInstance();
    RegistrationExtensions.RegisterType<GraphLongOperationManager>(builder).As<IGraphLongOperationManager>().InstancePerDependency();
    RegistrationExtensions.RegisterType<AsyncExecutor>(builder).SingleInstance().As<IAsyncExecutor>();
  }

  internal static ContainerBuilder RegisterDefaultLongOperations(this ContainerBuilder builder)
  {
    RegistrationExtensions.RegisterType<RuntimeLongOperationManager>(builder).As<LongOperationManager>().SingleInstance();
    return builder;
  }

  private static ContainerBuilder RegisterHostedModeServices(this ContainerBuilder builder)
  {
    builder.RegisterBuildCallback((System.Action<ILifetimeScope>) (lifetimeScope => LongOperationManager.Instance = ResolutionExtensions.Resolve<LongOperationManager>((IComponentContext) lifetimeScope)));
    return builder;
  }

  private class HostedModeRegistration : Module
  {
    protected virtual void Load(ContainerBuilder builder)
    {
      builder.RegisterDefaultLongOperations().RegisterHostedModeServices().EnableInstrumentation();
    }
  }
}
