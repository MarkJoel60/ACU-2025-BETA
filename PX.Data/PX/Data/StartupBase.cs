// Decompiled with JetBrains decompiler
// Type: PX.Data.StartupBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PX.Common;
using PX.Hosting;
using PX.Logging;
using Serilog;
using System;
using System.Configuration.Provider;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public abstract class StartupBase : IStartup
{
  protected IConfiguration Configuration { get; }

  protected ILogger RootLogger { get; }

  protected StartupBase(HostBuilderContext context)
  {
    this.Configuration = context.Configuration;
    this.RootLogger = context.GetLogger();
  }

  void IStartup.ConfigureServices(IServiceCollection services) => this.ConfigureServices(services);

  protected abstract void ConfigureServices(IServiceCollection services);

  protected void AddSingletons(IServiceCollection services)
  {
    ServiceCollectionServiceExtensions.AddSingleton<Func<PXDatabaseProvider>>(ServiceCollectionServiceExtensions.AddSingleton<PXAccessProvider>(services, PXAccess.Initialize(this.Configuration)), PXDatabase.Initialize(this.Configuration, (Func<System.Type, ILogger>) (type => this.RootLogger.ForContext(type))));
  }

  void IStartup.ConfigureContainer(ContainerBuilder containerBuilder)
  {
    this.ConfigureContainer(containerBuilder);
  }

  protected abstract void ConfigureContainer(ContainerBuilder containerBuilder);

  protected static void RegisterProvider<T>(ContainerBuilder containerBuilder, Func<T> func) where T : ProviderBase
  {
    containerBuilder.RegisterProvider<T>(func);
  }
}
