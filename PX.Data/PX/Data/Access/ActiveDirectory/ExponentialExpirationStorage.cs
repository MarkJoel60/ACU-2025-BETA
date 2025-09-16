// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.ExponentialExpirationStorage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Concurrent;

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

internal class ExponentialExpirationStorage : IExpirationStorage
{
  private const double InitialExpirationInSeconds = 1.0;
  private const double ExpirationMultiplier = 2.0;
  protected readonly ConcurrentDictionary<object, (System.DateTime InitialTime, System.DateTime ExpirationTime)> ErrorExpiration = new ConcurrentDictionary<object, (System.DateTime, System.DateTime)>();

  protected static (System.DateTime InitialTime, System.DateTime ExpirationTime) GetNextExpiration(
    (System.DateTime InitialTime, System.DateTime ExpirationTime) expiration)
  {
    return (expiration.InitialTime, expiration.InitialTime.AddSeconds((expiration.ExpirationTime - expiration.InitialTime).TotalSeconds * 2.0));
  }

  protected virtual (System.DateTime InitialTime, System.DateTime ExpirationTime) DefaultExpiration
  {
    get
    {
      return ExponentialExpirationStorage.GetNextExpiration((System.DateTime.UtcNow, System.DateTime.UtcNow.AddSeconds(0.5)));
    }
  }

  public bool IsExpired(object key)
  {
    return this.ErrorExpiration.GetOrAdd(key, this.DefaultExpiration).ExpirationTime < System.DateTime.UtcNow;
  }

  public void ClearExpiration(object key)
  {
    this.ErrorExpiration.TryRemove(key, out (System.DateTime, System.DateTime) _);
  }

  public void UpdateExpiration(object key)
  {
    this.ErrorExpiration.AddOrUpdate(key, this.DefaultExpiration, (Func<object, (System.DateTime, System.DateTime), (System.DateTime, System.DateTime)>) ((_, expirationToUpdate) => ExponentialExpirationStorage.GetNextExpiration(expirationToUpdate)));
  }
}
