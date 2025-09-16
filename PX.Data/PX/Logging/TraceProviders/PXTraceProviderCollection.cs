// Decompiled with JetBrains decompiler
// Type: PX.Logging.TraceProviders.PXTraceProviderCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Configuration;
using PX.Data;
using PX.Hosting;
using System;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PX.Logging.TraceProviders;

internal sealed class PXTraceProviderCollection
{
  private readonly TraceLevelFilter _filter;
  private readonly PXTraceProvider[] _providers;

  public PXTraceProviderCollection(IConfiguration section)
  {
    this._filter = new TraceLevelFilter(section?["minimumLevel"], SourceLevels.All);
    this._providers = (section != null ? ConfigurationSectionExtensions.GetProviders(section).Select<IConfigurationSection, PXTraceProvider>((Func<IConfigurationSection, PXTraceProvider>) (config => ConfigurationSectionExtensions.InstantiateProvider<PXTraceProvider>(config))).ToArray<PXTraceProvider>() : (PXTraceProvider[]) null) ?? new PXTraceProvider[0];
    this.MinimumLevel = SourceLevels.Off;
    foreach (PXTraceProvider provider in this._providers)
    {
      if (!(provider is IExposesMinimumLevel exposesMinimumLevel))
      {
        this.MinimumLevel = SourceLevels.All;
        break;
      }
      this.MinimumLevel = this.MinimumLevel | exposesMinimumLevel.MinimumLevel;
    }
    this.MinimumLevel = this.MinimumLevel & this._filter.MinimumLevel;
  }

  internal SourceLevels MinimumLevel { get; }

  public void TraceEvent(PXTrace.Event item)
  {
    try
    {
      if (!this._filter.ShouldTrace(item.EventType))
        return;
    }
    catch (Exception ex)
    {
      SelfLog.SafeWriteExceptionInCaller(ex, nameof (TraceEvent), "C:\\build\\code_repo\\NetTools\\PX.Data\\Logging\\TraceProviders\\PXTraceProviderCollection.cs", 57);
    }
    foreach (PXTraceProvider provider in this._providers)
    {
      try
      {
        provider.TraceEvent(item);
      }
      catch (Exception ex)
      {
        SelfLog.SafeWriteExceptionInCaller(ex, nameof (TraceEvent), "C:\\build\\code_repo\\NetTools\\PX.Data\\Logging\\TraceProviders\\PXTraceProviderCollection.cs", 68);
      }
    }
  }
}
