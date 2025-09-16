// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CurrencyInfoCache
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CM;

public static class CurrencyInfoCache
{
  public static CurrencyInfo GetInfo(PXSelectBase<CurrencyInfo> select, long? curyInfoID)
  {
    if (!curyInfoID.HasValue)
      return ((PXSelectBase) select).Cache.Current as CurrencyInfo;
    CurrencyInfo info = (CurrencyInfo) ((PXSelectBase) select).Cache.Locate((object) new CurrencyInfo()
    {
      CuryInfoID = curyInfoID
    });
    if (info == null)
    {
      info = PXResultset<CurrencyInfo>.op_Implicit(select.Select(new object[1]
      {
        (object) curyInfoID
      }));
      PXCurrencyAttribute.PXCurrencyHelper.PopulatePrecision(((PXSelectBase) select).Cache, info);
      CurrencyInfoCache.StoreCached(select, info);
    }
    return info;
  }

  public static void StoreCached(PXSelectBase<CurrencyInfo> select, CurrencyInfo info)
  {
    if (((PXSelectBase) select).Cache.Locate((object) info) != null)
      return;
    ((PXSelectBase) select).Cache.SetStatus((object) info, (PXEntryStatus) 0);
  }

  public static void StoreCached(PXSelectBase<CurrencyInfo> select, long? curyInfoID)
  {
    CurrencyInfoCache.GetInfo(select, curyInfoID);
  }
}
