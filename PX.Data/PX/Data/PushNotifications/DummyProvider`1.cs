// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.DummyProvider`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.PushNotifications;

public class DummyProvider<T> : IStorageProvider<T>, IStorageInfoProvider, IDisposable where T : class
{
  public long CurrentStorageSize => 0;

  public long MaxStorageSize => 0;

  public int ItemsCount => 0;

  public string LastFailedToCommitMessage { get; set; }

  public bool ContainsKey(Guid key) => false;

  public void Dispose()
  {
  }

  public void Purge()
  {
  }

  public void RemoveValue(Guid key)
  {
  }

  public void SaveValue(Guid key, T value)
  {
  }

  public bool TryGetValue(Guid key, out T value)
  {
    value = default (T);
    return false;
  }
}
