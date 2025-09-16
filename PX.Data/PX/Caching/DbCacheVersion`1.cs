// Decompiled with JetBrains decompiler
// Type: PX.Caching.DbCacheVersion`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Caching;

internal class DbCacheVersion<TCacheName> : ICacheVersion<TCacheName>
{
  private static readonly string SlotKey = "CacheVersions";

  private static IReadOnlyDictionary<string, int> GetSlot()
  {
    return (IReadOnlyDictionary<string, int>) (PXDatabase.Provider.GetSlot<Dictionary<string, int>>(DbCacheVersion<TCacheName>.SlotKey, (PrefetchDelegate<Dictionary<string, int>>) (() => PXDatabase.SelectMulti<CacheVersion>((PXDataField) new PXDataField<CacheVersion.cacheName>(), (PXDataField) new PXDataField<CacheVersion.version>()).ToDictionary<PXDataRecord, string, int>((Func<PXDataRecord, string>) (r => r.GetString(0)), (Func<PXDataRecord, int>) (r => r.GetInt32(1).Value))), typeof (CacheVersion)) ?? throw new DBSlotNotAvailableException());
  }

  private static void ResetSlot()
  {
    PXDatabase.ResetSlot<Dictionary<string, int>>(DbCacheVersion<TCacheName>.SlotKey, typeof (CacheVersion));
  }

  private static string CacheName => typeof (TCacheName).FullName;

  public int Current
  {
    get
    {
      int num;
      return !DbCacheVersion<TCacheName>.GetSlot().TryGetValue(DbCacheVersion<TCacheName>.CacheName, out num) ? 0 : num;
    }
  }

  public void Increment()
  {
    if (this.Current > 0 || !TryInsert())
    {
      PXDataFieldParam[] pxDataFieldParamArray;
      do
      {
        int num;
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<CacheVersion>((PXDataField) new PXDataField<CacheVersion.version>(), (PXDataField) new PXDataFieldValue<CacheVersion.cacheName>((object) DbCacheVersion<TCacheName>.CacheName)))
          num = pxDataRecord.GetInt32(0).Value;
        pxDataFieldParamArray = new PXDataFieldParam[3]
        {
          (PXDataFieldParam) new PXDataFieldAssign<CacheVersion.version>((object) (num + 1)),
          (PXDataFieldParam) new PXDataFieldRestrict<CacheVersion.cacheName>((object) DbCacheVersion<TCacheName>.CacheName),
          (PXDataFieldParam) new PXDataFieldRestrict<CacheVersion.version>((object) num)
        };
      }
      while (!PXDatabase.Update<CacheVersion>(pxDataFieldParamArray));
    }
    DbCacheVersion<TCacheName>.ResetSlot();

    static bool TryInsert()
    {
      try
      {
        return PXDatabase.Insert<CacheVersion>((PXDataFieldAssign) new PXDataFieldAssign<CacheVersion.cacheName>((object) DbCacheVersion<TCacheName>.CacheName), (PXDataFieldAssign) new PXDataFieldAssign<CacheVersion.version>((object) 1));
      }
      catch (PXDatabaseException ex) when (ex.ErrorCode == PXDbExceptions.PrimaryKeyConstraintViolation)
      {
        return false;
      }
    }
  }
}
