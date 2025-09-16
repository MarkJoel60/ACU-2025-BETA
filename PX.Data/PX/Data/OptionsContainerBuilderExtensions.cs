// Decompiled with JetBrains decompiler
// Type: PX.Data.OptionsContainerBuilderExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Microsoft.Extensions.Options;
using System;

#nullable enable
namespace PX.Data;

internal static class OptionsContainerBuilderExtensions
{
  /// <summary>
  /// Configures <typeparamref name="TOptions" /> values from actual <typeparamref name="TDac" /> DB values
  /// </summary>
  /// <param name="containerBuilder">Container builder</param>
  /// <param name="action">Actions to be performed to set up <typeparamref name="TOptions" /></param>
  /// <typeparam name="TOptions">Options type to be configured</typeparam>
  /// <typeparam name="TDac">DAC to be used as data source</typeparam>
  /// <returns>Provided container builder</returns>
  internal static 
  #nullable disable
  ContainerBuilder ConfigureFromDac<TOptions, TDac>(
    this ContainerBuilder containerBuilder,
    DacBasedConfigureOptions<TOptions, TDac>.ConfigureFromDacDelegate action)
    where TOptions : class
    where TDac : class, IBqlTable, new()
  {
    RegistrationExtensions.Register<IOptionsChangeTokenSource<TOptions>>(containerBuilder, (Func<IComponentContext, IOptionsChangeTokenSource<TOptions>>) (ctx =>
    {
      PXDatabaseProvider databaseProvider = ctx.ResolveDatabaseProvider();
      OptionsContainerBuilderExtensions.GuardDacWithoutCompanyId<TDac>(databaseProvider);
      return (IOptionsChangeTokenSource<TOptions>) new DatabaseOptionsChangeTokenSource<TOptions, TDac>(databaseProvider);
    })).SingleInstance();
    RegistrationExtensions.Register<IConfigureOptions<TOptions>>(containerBuilder, (Func<IComponentContext, IConfigureOptions<TOptions>>) (ctx =>
    {
      PXDatabaseProvider provider = ctx.ResolveDatabaseProvider();
      return (IConfigureOptions<TOptions>) new DacBasedConfigureOptions<TOptions, TDac>(action, (Func<TDac>) (() => provider.SelectRecord<TDac>()));
    })).SingleInstance();
    ApplicationStartActivation.RunOnApplicationStart<IOptionsMonitor<TOptions>>(containerBuilder, (System.Action<IOptionsMonitor<TOptions>>) (_ => { }), (string) null);
    return containerBuilder;
  }

  private static PXDatabaseProvider ResolveDatabaseProvider(this IComponentContext componentContext)
  {
    return ResolutionExtensions.Resolve<Func<PXDatabaseProvider>>(componentContext)();
  }

  private static void GuardDacWithoutCompanyId<TDac>(PXDatabaseProvider provider)
  {
    string name = typeof (TDac).Name;
    if (provider.SchemaCache.TableExists(name) && provider.SchemaCache.getTableSetting(name, true).Flag != companySetting.companyFlag.Global)
      throw new ArgumentOutOfRangeException(typeof (TDac).Name, "DACs with CompanyID field can't be used as it's required to use tenant-independent configuration");
  }
}
