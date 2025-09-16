// Decompiled with JetBrains decompiler
// Type: PX.Data.DependencyInjection.CompositionRoot
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Extras.CommonServiceLocator;
using Autofac.Features.Scanning;
using CommonServiceLocator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PX.Common;
using PX.Common.Context;
using PX.Hosting;
using PX.Hosting.MachineKey;
using PX.Licensing;
using PX.Logging;
using Serilog;
using SerilogTimings;
using SerilogTimings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reflection;
using System.Threading;
using System.Web.Compilation;
using System.Web.Configuration;

#nullable enable
namespace PX.Data.DependencyInjection;

/// <exclude />
[PXInternalUseOnly]
public static class CompositionRoot
{
  private static 
  #nullable disable
  IDependencyInjector _dependencyInjector;
  private static readonly IDependencyInjector _dummyDependencyInjector = (IDependencyInjector) new DummyDependencyInjector();

  [PXInternalUseOnly]
  public static IHost CreateContainer()
  {
    return CompositionRoot.CreateContainer(WebConfigurationManager.OpenWebConfiguration("~"));
  }

  internal static IHost CreateContainer(System.Configuration.Configuration configuration)
  {
    Assembly[] array = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray<Assembly>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return (IHost) new LicensingHostWrapper(StartupHostBuilderExtensions.UseStartupFactory(ModuleAssemblerExtensions.UseModuleAssembler(SystemWebHostBuilderExtensions.UseSystemWeb((IHostBuilder) new HostBuilder()).ConfigureHostConfiguration((System.Action<IConfigurationBuilder>) (builder => EnvironmentVariablesExtensions.AddEnvironmentVariables(EnvironmentVariablesExtensions.AddEnvironmentVariables(builder, "DOTNET_"), "ASPNETCORE_").AddAppSettings(CompositionRoot.\u003C\u003EO.\u003C0\u003E__HostingSettingsExtractor ?? (CompositionRoot.\u003C\u003EO.\u003C0\u003E__HostingSettingsExtractor = new Func<IEnumerable<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>>(CompositionRoot.HostingSettingsExtractor))))).UseLicensing().ConfigureAppConfiguration((Action<HostBuilderContext, IConfigurationBuilder>) ((context, builder) => EnvironmentVariablesExtensions.AddEnvironmentVariables(ConfigurationBuilderExtensions.AddRootConfigurationSection(ConfigurationExtensions.AddMachineKeyConfigurationSection(ConfigurationBuilderExtensions.AddMembershipConfigurationSection(PX.Hosting.SessionState.Extensions.AddSessionStateConfigurationSection(builder, configuration), configuration), configuration), configuration).AddConnectionStrings(configuration.ConnectionStrings.ConnectionStrings).AddAppSettings(CompositionRoot.\u003C\u003EO.\u003C1\u003E__SerilogAppSettingsProcessor ?? (CompositionRoot.\u003C\u003EO.\u003C1\u003E__SerilogAppSettingsProcessor = new Func<IEnumerable<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>>(LoggingExtensions.SerilogAppSettingsProcessor))), context.Configuration["AppConfigurationEnvironmentPrefix"] ?? "ACUMATICA_"))).UseFirstChanceExceptionLogger(), (IReadOnlyList<Assembly>) array, CompositionRoot.\u003C\u003EO.\u003C2\u003E__RegisterAssemblyTypesAssignableToWithCachingStatic ?? (CompositionRoot.\u003C\u003EO.\u003C2\u003E__RegisterAssemblyTypesAssignableToWithCachingStatic = new Func<ContainerBuilder, Func<System.Type, bool>, System.Type, Assembly[], IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>>(AutofacExtensions.RegisterAssemblyTypesAssignableToWithCachingStatic))).UseLogging(), CompositionRoot.\u003C\u003EO.\u003C3\u003E__StartupFactory ?? (CompositionRoot.\u003C\u003EO.\u003C3\u003E__StartupFactory = new Func<HostBuilderContext, IStartup>(CompositionRoot.StartupFactory))).ConfigureContainer<ContainerBuilder>((Action<HostBuilderContext, ContainerBuilder>) ((_, containerBuilder) => containerBuilder.DisallowSingleInstanceGraphs())).ConfigureContainer<ContainerBuilder>(CompositionRoot.\u003C\u003EO.\u003C4\u003E__RegisterDependencyInjectorAndServiceLocator ?? (CompositionRoot.\u003C\u003EO.\u003C4\u003E__RegisterDependencyInjectorAndServiceLocator = new Action<HostBuilderContext, ContainerBuilder>(CompositionRoot.RegisterDependencyInjectorAndServiceLocator))).Build());
  }

  private static IStartup StartupFactory(HostBuilderContext context)
  {
    ILogger logger = context.GetLogger().ForContext(typeof (CompositionRoot));
    using (Operation operation = logger.BeginOperationVerbose("Discovering application startup class"))
    {
      try
      {
        IModuleAssembler moduleAssembler = ModuleAssemblerExtensions.GetModuleAssembler(context);
        operation.EnrichWith("Assemblies", (object) moduleAssembler.Assemblies, false);
        IStartup[] array = moduleAssembler.ScanAssembliesAndResolveAssignableTo<IStartup>().Where<IStartup>((Func<IStartup, bool>) (c => !(c is CompositionRoot.LoggingStartupWrapper))).ToArray<IStartup>();
        switch (array.Length)
        {
          case 0:
            throw new InvalidOperationException("No application startup class found");
          case 1:
            IStartup inner = array[0];
            string assemblyQualifiedName = inner.GetType().AssemblyQualifiedName;
            operation.EnrichWith("StartupClass", (object) assemblyQualifiedName, false);
            logger.Information<string>("Using application startup class {StartupClass}", assemblyQualifiedName);
            return (IStartup) new CompositionRoot.LoggingStartupWrapper(inner, logger);
          default:
            IEnumerable<string> values = ((IEnumerable<IStartup>) array).Select<IStartup, string>((Func<IStartup, string>) (c => c.GetType().AssemblyQualifiedName));
            operation.EnrichWith("Candidates", (object) values, false);
            throw new InvalidOperationException("Multiple application startup classes found:\n" + string.Join("\n", values));
        }
      }
      catch (Exception ex) when (OperationExtensions.SetExceptionAndRethrow(operation, ex))
      {
        throw;
      }
      finally
      {
        operation.Complete();
      }
    }
  }

  private static IEnumerable<KeyValuePair<string, string>> HostingSettingsExtractor(
    IEnumerable<KeyValuePair<string, string>> data)
  {
    return data.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (pair => pair.Key.StartsWith("hosting:"))).Select<KeyValuePair<string, string>, KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, KeyValuePair<string, string>>) (pair => new KeyValuePair<string, string>(pair.Key.Substring("hosting:".Length), pair.Value)));
  }

  private static void RegisterDependencyInjectorAndServiceLocator(
    HostBuilderContext _,
    ContainerBuilder builder)
  {
    builder.RegisterBuildCallback((System.Action<ILifetimeScope>) (container =>
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CompositionRoot.\u003C\u003Ec__DisplayClass7_0 cDisplayClass70 = new CompositionRoot.\u003C\u003Ec__DisplayClass7_0();
      CompositionRoot._dependencyInjector = ResolutionExtensions.Resolve<IDependencyInjector>((IComponentContext) container);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass70.serviceLocator = new AutofacServiceLocator((IComponentContext) container);
      // ISSUE: method pointer
      ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider((object) cDisplayClass70, __methodptr(\u003CRegisterDependencyInjectorAndServiceLocator\u003Eb__1)));
    }));
  }

  internal static IDependencyInjector GetDependencyInjector()
  {
    ISlotStore instance = SlotStore.Instance;
    return (CompositionRoot.InjectorSlot.GetInjector(instance, instance.GetLifetimeScope()) ?? (ServiceLocator.IsLocationProviderSet ? LazyInitializer.EnsureInitialized<IDependencyInjector>(ref CompositionRoot._dependencyInjector, new Func<IDependencyInjector>(ServiceLocator.Current.GetInstance<IDependencyInjector>)) : (IDependencyInjector) null)) ?? CompositionRoot._dummyDependencyInjector;
  }

  private class LoggingStartupWrapper : IStartup
  {
    private readonly IStartup _inner;
    private readonly ILogger _logger;

    [Obsolete("Only to make Autofac happy, never used in production path", true)]
    public LoggingStartupWrapper()
    {
    }

    public LoggingStartupWrapper(IStartup inner, ILogger logger)
    {
      this._inner = inner;
      this._logger = logger;
    }

    void IStartup.ConfigureServices(IServiceCollection services)
    {
      using (this._logger.TimeOperationVerbose("Configuring {DIRegistry}", (object) typeof (IServiceCollection).FullName))
        this._inner.ConfigureServices(services);
    }

    void IStartup.ConfigureContainer(ContainerBuilder containerBuilder)
    {
      using (this._logger.TimeOperationVerbose("Configuring {DIRegistry}", (object) typeof (ContainerBuilder).FullName))
        this._inner.ConfigureContainer(containerBuilder);
    }
  }

  internal static class InjectorSlot
  {
    private static TypedService _iDependencyInjectorService = new TypedService(typeof (IDependencyInjector));
    private static string _dictKey = typeof (CompositionRoot.InjectorSlot).FullName;

    public static IDependencyInjector GetInjector(ISlotStore slots, ILifetimeScope scope)
    {
      if (scope == null)
        return (IDependencyInjector) null;
      Dictionary<Guid, string> dictionary = slots.Get<Dictionary<Guid, string>>(CompositionRoot.InjectorSlot._dictKey);
      if (dictionary == null)
      {
        dictionary = new Dictionary<Guid, string>();
        slots.Set(CompositionRoot.InjectorSlot._dictKey, (object) dictionary);
      }
      IComponentRegistration icomponentRegistration;
      ((IComponentContext) scope).ComponentRegistry.TryGetRegistration((Service) CompositionRoot.InjectorSlot._iDependencyInjectorService, ref icomponentRegistration);
      string str;
      if (!dictionary.TryGetValue(icomponentRegistration.Id, out str))
      {
        str = icomponentRegistration.Id.ToString();
        dictionary.Add(icomponentRegistration.Id, str);
      }
      IDependencyInjector injector = slots.Get<IDependencyInjector>(str);
      if (injector == null)
      {
        injector = ResolutionExtensions.Resolve<IDependencyInjector>((IComponentContext) scope);
        slots.Set(str, (object) injector);
        scope.Disposer.AddInstanceForDisposal(Disposable.Create<string>(str, (System.Action<string>) (key => SlotStore.Instance.Remove(key))));
      }
      return injector;
    }
  }
}
