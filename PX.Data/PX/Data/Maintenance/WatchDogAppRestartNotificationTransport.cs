// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.WatchDogAppRestartNotificationTransport
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Serilog;
using System;
using System.Threading;

#nullable disable
namespace PX.Data.Maintenance;

internal class WatchDogAppRestartNotificationTransport : 
  IAppRestartNotificationTransport,
  IDisposable
{
  private readonly ILogger _logger;
  private readonly Func<PXDatabaseProvider> _databaseProviderFactory;
  private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
  private readonly Guid? _restartRequestIdOnSubscription;

  public WatchDogAppRestartNotificationTransport(
    ILogger logger,
    Func<PXDatabaseProvider> databaseProviderFactory)
  {
    this._logger = logger;
    this._databaseProviderFactory = databaseProviderFactory;
    PXDatabaseProvider provider = databaseProviderFactory();
    this._restartRequestIdOnSubscription = WatchDogAppRestartNotificationTransport.GetLastRestartRequestId(provider);
    this.SubscribeToNotifications(provider);
  }

  public void SendRestartRequestNotification()
  {
    PXDatabaseProvider provider = this._databaseProviderFactory();
    Guid guid = Guid.NewGuid();
    this._logger.Debug<Guid>("Sending restart request notification with ID {RequestId}", guid);
    WatchDogAppRestartNotificationTransport.SetLastRestartRequestId(provider, guid);
    provider.GetMaintenance().InsertRestartMark(typeof (AppRestartRequest));
  }

  public CancellationToken RestartRequested => this._cancellationTokenSource.Token;

  void IDisposable.Dispose() => this._cancellationTokenSource.Dispose();

  private void SubscribeToNotifications(PXDatabaseProvider provider)
  {
    this._logger.Debug<Guid?>("Subscribing to restart notifications. Last known request is {LastKnownRequestId}", this._restartRequestIdOnSubscription);
    if (!provider.Subscribe(typeof (AppRestartRequest), new PXDatabaseTableChanged(this.DbChangedHandler), nameof (WatchDogAppRestartNotificationTransport)))
    {
      this._logger.Error<string>("Failed to subscribe to {TableName} table modifications", "AppRestartRequest");
      throw new Exception("Failed to subscribe to AppRestartRequest table notifications");
    }
  }

  private void DbChangedHandler()
  {
    if (this._cancellationTokenSource.IsCancellationRequested)
    {
      this._logger.Debug("Restart request notification have already been handled");
    }
    else
    {
      Guid? restartRequestId = WatchDogAppRestartNotificationTransport.GetLastRestartRequestId(this._databaseProviderFactory());
      this._logger.Debug<Guid?, Guid?>("Got restart request notification. Last known request is {LastKnownRequestId}, current request is {CurrentRequestId}", this._restartRequestIdOnSubscription, restartRequestId);
      Guid? nullable = restartRequestId;
      Guid? idOnSubscription = this._restartRequestIdOnSubscription;
      if ((nullable.HasValue == idOnSubscription.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == idOnSubscription.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        return;
      this._cancellationTokenSource.Cancel();
    }
  }

  private static void SetLastRestartRequestId(PXDatabaseProvider provider, Guid guid)
  {
    PXDataFieldAssign pxDataFieldAssign1 = new PXDataFieldAssign("RequestId", (object) guid);
    PXDataFieldAssign pxDataFieldAssign2 = new PXDataFieldAssign("CreatedDateTime", (object) System.DateTime.UtcNow);
    if (provider.Update(typeof (AppRestartRequest), (PXDataFieldParam) pxDataFieldAssign1, (PXDataFieldParam) pxDataFieldAssign2))
      return;
    provider.Insert(typeof (AppRestartRequest), pxDataFieldAssign1, pxDataFieldAssign2);
  }

  private static Guid? GetLastRestartRequestId(PXDatabaseProvider provider)
  {
    using (PXDataRecord pxDataRecord = provider.SelectSingle(typeof (AppRestartRequest), (PXDataField) new PXDataField<AppRestartRequest.requestId>()))
      return (Guid?) pxDataRecord?.GetGuid(0);
  }
}
