// Decompiled with JetBrains decompiler
// Type: PX.Metadata.ServiceRegistration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Microsoft.Extensions.Configuration;
using PX.Api.Soap.Screen;
using PX.Caching;
using PX.Data;
using PX.Data.ReferentialIntegrity.Inspecting;
using PX.Data.Services.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace PX.Metadata;

public class ServiceRegistration : Module
{
  protected virtual void Load(
  #nullable disable
  ContainerBuilder builder)
  {
    RegistrationExtensions.AsSelf<InMemoryScreenInfoCache, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<InMemoryScreenInfoCache>(builder)).SingleInstance();
    builder.RegisterDbCacheVersion<PXSiteMap.ScreenInfo>();
    OptionsContainerBuilderExtensions.BindFromConfiguration<DistributedScreenInfoCacheOptions>(builder, new string[2]
    {
      "metadata",
      "screenInfo"
    });
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    RegistrationExtensions.AsSelf<DistributedScreenInfoCache, ConcreteReflectionActivatorData>(RegistrationExtensions.WithParameter<DistributedScreenInfoCache, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<DistributedScreenInfoCache>(builder), (Parameter) TypedParameter.From<Func<IEnumerable<string>>>(ServiceRegistration.\u003C\u003EO.\u003C0\u003E__GetActiveLocales ?? (ServiceRegistration.\u003C\u003EO.\u003C0\u003E__GetActiveLocales = new Func<IEnumerable<string>>(LocalesHelper.GetActiveLocales))))).SingleInstance();
    RegistrationExtensions.AsSelf<LayeredScreenInfoCache, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<LayeredScreenInfoCache>(builder)).SingleInstance();
    RegistrationExtensions.AsSelf<ScreenUiTypeAbsenceCache, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<ScreenUiTypeAbsenceCache>(builder).As<ICacheControlledBy<ScreenUiTypeAbsence>>()).SingleInstance();
    RegistrationExtensions.Register<object>(builder, (Func<IComponentContext, object>) (c =>
    {
      IComponentContext icomponentContext = ResolutionExtensions.Resolve<IComponentContext>(c);
      return ResolutionExtensions.Resolve<IAppInstanceInfo>(icomponentContext).IsCluster ? (object) ResolutionExtensions.Resolve<DistributedScreenInfoCache>(icomponentContext) : (object) ResolutionExtensions.Resolve<LayeredScreenInfoCache>(icomponentContext);
    })).As<IScreenInfoStorage>().As<ICacheControl>().As<ICacheControlledBy<PXSiteMap.ScreenInfo>>().As<IScreenInfoCacheControl>().SingleInstance();
    RegistrationExtensions.PreserveExistingDefaults<ServiceManagerDacRegistry, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<ServiceManagerDacRegistry>(builder).As<IDacRegistry>().SingleInstance());
    RegistrationExtensions.PreserveExistingDefaults<ServiceManagerGraphRegistry, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<ServiceManagerGraphRegistry>(builder).As<IGraphRegistry>().SingleInstance());
    OptionsContainerBuilderExtensions.BindFromConfiguration<DacMetadataInitializerOptions>(builder, new string[2]
    {
      "metadata",
      "dac"
    });
    RegistrationExtensions.PreserveExistingDefaults<InstantiateAllPXCachesDacMetadataInitializer, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.AsSelf<InstantiateAllPXCachesDacMetadataInitializer, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<InstantiateAllPXCachesDacMetadataInitializer>(builder)).As<IDacMetadataInitializer>().SingleInstance());
    ApplicationStartActivation.RunOnApplicationStart<IConfiguration, ILogger>(builder, (Action<IConfiguration, ILogger>) ((configuration, logger) =>
    {
      if (configuration["InstantiateAllCaches"] != null)
        logger.Warning("InstantiateAllCaches setting is obsolete and has no effect. Please remove this configuration key.");
      if (configuration["InstantiateAllCachesDelayMs"] == null)
        return;
      logger.Warning("InstantiateAllCachesDelayMs setting is obsolete and has no effect. Please remove this configuration key.");
    }), (string) null);
    ApplicationStartActivation.QueueBackgroundItemOnApplicationStart<IDacMetadataInitializer, GraphViewReferenceAnalyzer, ITableReferenceCollector>(builder, (Func<IDacMetadataInitializer, GraphViewReferenceAnalyzer, ITableReferenceCollector, CancellationToken, Task>) (async (dacMetadataInitializer, graphViewReferenceAnalyzer, tableReferenceCollector, cancellationToken) =>
    {
      await dacMetadataInitializer.RunAsync(cancellationToken);
      await graphViewReferenceAnalyzer.CollectReferencesAsync(cancellationToken);
      tableReferenceCollector.CollectionCompleted();
    }), (string) null);
  }
}
