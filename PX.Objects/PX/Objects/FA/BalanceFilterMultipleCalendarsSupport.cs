// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.BalanceFilterMultipleCalendarsSupport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.FA;

public sealed class BalanceFilterMultipleCalendarsSupport : PXCacheExtension<BalanceFilter>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>();
  }

  [PXMergeAttributes]
  [PXDefault]
  public int? OrgBAccountID { get; set; }
}
