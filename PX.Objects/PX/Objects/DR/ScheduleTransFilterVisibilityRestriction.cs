// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.ScheduleTransFilterVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.DR;

public sealed class ScheduleTransFilterVisibilityRestriction : 
  PXCacheExtension<ScheduleTransInq.ScheduleTransFilter>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [RestrictCustomerByBranch(typeof (BAccountR.cOrgBAccountID), typeof (ScheduleTransInq.ScheduleTransFilter.branchID), ResetCustomer = true)]
  [RestrictVendorByBranch(typeof (BAccountR.vOrgBAccountID), typeof (ScheduleTransInq.ScheduleTransFilter.branchID), ResetVendor = true)]
  public int? BAccountID { get; set; }
}
