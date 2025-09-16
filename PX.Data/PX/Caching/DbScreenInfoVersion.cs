// Decompiled with JetBrains decompiler
// Type: PX.Caching.DbScreenInfoVersion
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

internal class DbScreenInfoVersion : IScreenInfoVersion
{
  private const string SlotKey = "ScreenInfoVersions";

  int IScreenInfoVersion.GetCurrent(string screenInfoId)
  {
    return DbScreenInfoVersion.GetCurrent(screenInfoId);
  }

  void IScreenInfoVersion.Increment(string screenInfoId)
  {
    if (DbScreenInfoVersion.GetCurrent(screenInfoId) > 0 || !TryInsert())
    {
      PXDataFieldParam[] pxDataFieldParamArray;
      do
      {
        int num;
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<ScreenInfoVersion>((PXDataField) new PXDataField<ScreenInfoVersion.version>(), (PXDataField) new PXDataFieldValue<ScreenInfoVersion.screenInfoId>((object) screenInfoId)))
          num = pxDataRecord.GetInt32(0).Value;
        pxDataFieldParamArray = new PXDataFieldParam[3]
        {
          (PXDataFieldParam) new PXDataFieldAssign<ScreenInfoVersion.version>((object) (num + 1)),
          (PXDataFieldParam) new PXDataFieldRestrict<ScreenInfoVersion.screenInfoId>((object) screenInfoId),
          (PXDataFieldParam) new PXDataFieldRestrict<ScreenInfoVersion.version>((object) num)
        };
      }
      while (!PXDatabase.Update<ScreenInfoVersion>(pxDataFieldParamArray));
    }
    DbScreenInfoVersion.ResetSlot();

    bool TryInsert()
    {
      try
      {
        return PXDatabase.Insert<ScreenInfoVersion>((PXDataFieldAssign) new PXDataFieldAssign<ScreenInfoVersion.screenInfoId>((object) screenInfoId), (PXDataFieldAssign) new PXDataFieldAssign<ScreenInfoVersion.version>((object) 1));
      }
      catch (PXDatabaseException ex) when (ex.ErrorCode == PXDbExceptions.PrimaryKeyConstraintViolation)
      {
        return false;
      }
    }
  }

  private static IReadOnlyDictionary<string, int> GetSlot()
  {
    return (IReadOnlyDictionary<string, int>) (PXDatabase.Provider.GetSlot<Dictionary<string, int>>("ScreenInfoVersions", (PrefetchDelegate<Dictionary<string, int>>) (() => PXDatabase.SelectMulti<ScreenInfoVersion>((PXDataField) new PXDataField<ScreenInfoVersion.screenInfoId>(), (PXDataField) new PXDataField<ScreenInfoVersion.version>()).ToDictionary<PXDataRecord, string, int>((Func<PXDataRecord, string>) (r => r.GetString(0)), (Func<PXDataRecord, int>) (r => r.GetInt32(1).Value))), typeof (ScreenInfoVersion)) ?? throw new DBSlotNotAvailableException());
  }

  private static void ResetSlot()
  {
    PXDatabase.ResetSlot<Dictionary<string, int>>("ScreenInfoVersions", typeof (ScreenInfoVersion));
  }

  private static int GetCurrent(string screenInfoId)
  {
    int num;
    return !DbScreenInfoVersion.GetSlot().TryGetValue(screenInfoId, out num) ? 0 : num;
  }
}
