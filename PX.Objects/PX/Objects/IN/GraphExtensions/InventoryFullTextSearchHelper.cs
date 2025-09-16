// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.InventoryFullTextSearchHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.IN.DAC;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

[PXInternalUseOnly]
public static class InventoryFullTextSearchHelper
{
  public static bool IsFTSAvailable()
  {
    return InventoryFullTextSearchHelper.IsSqlDialectAllowedForFTS() && PXDatabase.Provider.GetFullTextSearchCapability<InventorySearchIndex.contentAlternateID>() != 2;
  }

  public static bool IsSqlDialectAllowedForFTS() => PXDatabase.Provider.SqlDialect.DialectType == 0;
}
