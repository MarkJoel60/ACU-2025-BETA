// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.BaseProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

#nullable disable
namespace PX.Data.PushNotifications;

public abstract class BaseProvider : IStorageInfoProvider, IDisposable
{
  private bool _isDisposed;
  protected readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
  protected readonly ConcurrentDictionary<Guid, long> _fileSizes = new ConcurrentDictionary<Guid, long>();

  public long CurrentStorageSize
  {
    get => Convert.ToInt64(BaseProvider.ConvertBytesToKilobytes(this._fileSizes.Values.Sum()));
  }

  public abstract long MaxStorageSize { get; }

  public int ItemsCount => this._fileSizes.Count;

  public string LastFailedToCommitMessage { get; set; }

  public bool ContainsKey(Guid key) => this._fileSizes.ContainsKey(key);

  private static double ConvertBytesToKilobytes(long bytes) => (double) bytes / 1024.0;

  public void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }

  private void Dispose(bool disposing)
  {
    if (this._isDisposed)
      return;
    if (disposing)
    {
      this._tokenSource?.Cancel();
      this._tokenSource?.Dispose();
    }
    this._isDisposed = true;
  }
}
