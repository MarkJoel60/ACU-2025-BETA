// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CurrencyHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CM;

public class CurrencyHelper
{
  public static bool IsSameCury(
    long? CuryInfoIDA,
    long? CuryInfoIDB,
    CurrencyInfo curyInfoA,
    CurrencyInfo curyInfoB)
  {
    long? nullable1 = CuryInfoIDA;
    long? nullable2 = CuryInfoIDB;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return true;
    return curyInfoA != null && curyInfoB != null && curyInfoA.CuryID == curyInfoB.CuryID;
  }

  public static bool IsSameCury(PXGraph graph, long? curyInfoIDA, long? curyInfoIDB)
  {
    CurrencyInfo curyInfoA = PXResultset<CurrencyInfo>.op_Implicit(PXSelectBase<CurrencyInfo, PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Required<CurrencyInfo.curyInfoID>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) curyInfoIDA
    }));
    CurrencyInfo curyInfoB = PXResultset<CurrencyInfo>.op_Implicit(PXSelectBase<CurrencyInfo, PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Required<CurrencyInfo.curyInfoID>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) curyInfoIDB
    }));
    return CurrencyHelper.IsSameCury(curyInfoIDA, curyInfoIDB, curyInfoA, curyInfoB);
  }
}
