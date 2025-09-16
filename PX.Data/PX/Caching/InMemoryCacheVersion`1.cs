// Decompiled with JetBrains decompiler
// Type: PX.Caching.InMemoryCacheVersion`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Concurrent;

#nullable disable
namespace PX.Caching;

[PXInternalUseOnly]
public class InMemoryCacheVersion<TCacheName> : ICacheVersion<TCacheName>
{
  private readonly ConcurrentDictionary<string, int> _counterByTenant = new ConcurrentDictionary<string, int>();
  private readonly Func<string> _getTenant;

  public int Current
  {
    get => this._counterByTenant.GetOrAdd(this._getTenant(), (Func<string, int>) (_ => 0));
  }

  public void Increment()
  {
    this._counterByTenant.AddOrUpdate(this._getTenant(), (Func<string, int>) (_ => 1), (Func<string, int, int>) ((_, v) => v + 1));
  }

  public InMemoryCacheVersion() => this._getTenant = (Func<string>) (() => string.Empty);

  public InMemoryCacheVersion(IPXIdentityAccessor identityAccessor)
  {
    this._getTenant = (Func<string>) (() => identityAccessor.Identity?.TenantId ?? string.Empty);
  }
}
