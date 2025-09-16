// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.SM.Extension.CacheEntityItemExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.PO;
using PX.SM;

#nullable disable
namespace PX.Objects.CN.Subcontracts.SM.Extension;

public static class CacheEntityItemExtensions
{
  public static string GetSubcontractViewName(this CacheEntityItem cacheEntityItem)
  {
    if (cacheEntityItem.SubKey == typeof (POOrder).FullName)
      return "Subcontracts";
    return !(cacheEntityItem.SubKey == typeof (POLine).FullName) ? cacheEntityItem.Name : "Subcontract Line";
  }
}
