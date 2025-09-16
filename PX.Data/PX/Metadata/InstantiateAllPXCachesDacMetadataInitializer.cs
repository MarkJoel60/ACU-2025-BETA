// Decompiled with JetBrains decompiler
// Type: PX.Metadata.InstantiateAllPXCachesDacMetadataInitializer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using PX.Data;
using PX.Data.DependencyInjection;
using PX.Logging.Sinks.SystemEventsDbSink;
using Serilog;
using Serilog.Events;
using SerilogTimings;
using SerilogTimings.Extensions;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Metadata;

internal class InstantiateAllPXCachesDacMetadataInitializer : IDacMetadataInitializer
{
  private readonly IDacRegistry _dacRegistry;
  private readonly ILogger _logger;
  private readonly DacMetadataInitializerOptions _options;
  private readonly TaskCompletionSource<bool> _taskCompletionSource = new TaskCompletionSource<bool>();

  public InstantiateAllPXCachesDacMetadataInitializer(
    IOptions<DacMetadataInitializerOptions> options,
    IDacRegistry dacRegistry,
    ILogger logger)
  {
    this._options = options.Value;
    this._dacRegistry = dacRegistry;
    this._logger = logger;
    if (options.Value.CollectOnStartup)
      return;
    this._taskCompletionSource.SetCanceled();
    logger.ForSystemEvents("System", "System_CollectDacMetadataOnStartupTurnedOffInProductionEventId").Warning("The CollectDacMetadataOnStartup key is set to false. This key must be set to true because it is required by various Platform features.");
  }

  public async Task RunAsync(CancellationToken cancellationToken = default (CancellationToken))
  {
    if (!this._options.CollectOnStartup)
      return;
    await Task.Delay(this._options.CollectOnStartupAfter, cancellationToken);
    using (Operation operation = LoggerOperationExtensions.OperationAt(this._logger, (LogEventLevel) 1, new LogEventLevel?()).Begin("Initializing DAC metadata (instantiating all PXCache objects) with a {DacMetadataInitializationDelay} delay", new object[1]
    {
      (object) this._options.CollectOnStartupAfter
    }))
    {
      this.InstantiateAllCaches(cancellationToken);
      this._taskCompletionSource.SetResult(true);
      operation.Complete();
    }
  }

  public Task InitializationCompleted => (Task) this._taskCompletionSource.Task;

  internal void InstantiateAllCaches(CancellationToken cancellationToken)
  {
    using (PXAccess.GetAdminLoginScope())
    {
      using (LifetimeScopeHelper.BeginLifetimeScope())
      {
        foreach (System.Type type in this._dacRegistry.All)
        {
          cancellationToken.ThrowIfCancellationRequested();
          try
          {
            MethodInfo method = typeof (PXCache<>).MakeGenericType(type).GetMethod("GetItemTypeInternal", BindingFlags.Static | BindingFlags.NonPublic);
            if (method != (MethodInfo) null)
            {
              if (!method.ContainsGenericParameters)
                method.Invoke((object) null, Array.Empty<object>());
            }
          }
          catch
          {
          }
        }
      }
    }
  }
}
