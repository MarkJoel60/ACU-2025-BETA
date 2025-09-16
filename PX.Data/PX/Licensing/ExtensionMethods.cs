// Decompiled with JetBrains decompiler
// Type: PX.Licensing.ExtensionMethods
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using PX.Data;
using Serilog;
using Serilog.Events;
using SerilogTimings;
using SerilogTimings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Licensing;

internal static class ExtensionMethods
{
  internal static 
  #nullable disable
  IHostBuilder UseLicensing(this IHostBuilder builder)
  {
    ExtensionMethods.LicensingConfigurationProvider licensingConfigurationProvider = new ExtensionMethods.LicensingConfigurationProvider();
    return builder.ConfigureAppConfiguration((Action<HostBuilderContext, IConfigurationBuilder>) ((_, configurationBuilder) => configurationBuilder.Add((IConfigurationSource) new ExtensionMethods.LicensingConfigurationSource(licensingConfigurationProvider)))).ConfigureContainer<ContainerBuilder>((Action<HostBuilderContext, ContainerBuilder>) ((_, containerBuilder) =>
    {
      RegistrationExtensions.RegisterInstance<ExtensionMethods.LicensingConfigurationProvider>(containerBuilder, licensingConfigurationProvider).As<ILicensingConfigurationProvider>().OnActivated((System.Action<IActivatedEventArgs<ExtensionMethods.LicensingConfigurationProvider>>) (args => args.Instance.Logger = LicensingLog.ForClassContext(((object) args.Instance).GetType())));
      RegistrationExtensions.RegisterType<LicensingManager>(containerBuilder).As<ILicensingManager>().As<ILicensing>().As<IStartable>().SingleInstance();
    }));
  }

  private class LicensingConfigurationSource : IConfigurationSource
  {
    private readonly ExtensionMethods.LicensingConfigurationProvider _provider;

    internal LicensingConfigurationSource(
      ExtensionMethods.LicensingConfigurationProvider provider)
    {
      this._provider = provider;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
      return (IConfigurationProvider) this._provider;
    }
  }

  private class LicensingConfigurationProvider : 
    ConfigurationProvider,
    ILicensingConfigurationProvider
  {
    private PXLicenseDefinition _license;
    private ILogger _logger;

    public ILogger Logger
    {
      get
      {
        return this._logger ?? PXTrace.Logger.ForContext<ExtensionMethods.LicensingConfigurationSource>();
      }
      set => this._logger = value;
    }

    public virtual void Load() => this.Load((LogEventLevel) 2, nameof (Load));

    void ILicensingConfigurationProvider.SetLicense(PXLicenseDefinition license)
    {
      this._license = license;
      this.Load((LogEventLevel) 0, "SetLicense");
    }

    private void Load(LogEventLevel level, string reason)
    {
      using (Operation operation = LoggerOperationExtensions.OperationAt(this.Logger, level, new LogEventLevel?()).Begin("Loading configuration from license ({Reason})", new object[1]
      {
        (object) reason
      }))
      {
        this.Data = (IDictionary<string, string>) (this._license?.Configuration ?? (IReadOnlyDictionary<string, string>) new Dictionary<string, string>()).ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (pair => pair.Key), (Func<KeyValuePair<string, string>, string>) (pair => pair.Value), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        operation.Complete("EntryCount", (object) this.Data.Count, false);
      }
      this.OnReload();
    }
  }
}
