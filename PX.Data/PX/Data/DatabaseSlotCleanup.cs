// Decompiled with JetBrains decompiler
// Type: PX.Data.DatabaseSlotCleanup
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using PX.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Data;

internal class DatabaseSlotCleanup
{
  private readonly DatabaseSlotCleanupOptions _options;
  private readonly System.Action<Func<CancellationToken, Task>> _queueBackgroundWorkItem;
  private bool isStarted;
  private readonly object startlock = new object();

  public DatabaseSlotCleanup(
    IOptions<DatabaseSlotCleanupOptions> options,
    IOptions<BackgroundExecutionOptions> backgroundExecutionOptions)
  {
    this._options = options.Value;
    this._queueBackgroundWorkItem = backgroundExecutionOptions.Value.QueueBackgroundWorkItem;
  }

  internal void Start()
  {
    if (!this._options.SlotsTimeoutEnabled)
      return;
    lock (this.startlock)
    {
      if (this.isStarted)
        return;
      this.isStarted = true;
    }
    this._queueBackgroundWorkItem((Func<CancellationToken, Task>) (async ct =>
    {
      while (!ct.IsCancellationRequested)
      {
        PXDatabase.CleanupExpiredSlots(this._options.SlotsTimeoutMinutes);
        if (ct.IsCancellationRequested)
          break;
        await Task.Delay(300000, ct);
      }
    }));
  }
}
