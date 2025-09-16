// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Standalone.APQuickCheckVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.GL;

#nullable disable
namespace PX.Objects.AP.Standalone;

public sealed class APQuickCheckVisibilityRestriction : PXCacheExtension<APQuickCheck>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.visibilityRestriction>();

  [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDetail = false, TabOrder = 0)]
  [PXFormula(typeof (Switch<Case<Where<PendingValue<APQuickCheck.branchID>, IsPending>, Null, Case<Where<APQuickCheck.vendorLocationID, IsNotNull, And<Selector<APQuickCheck.vendorLocationID, PX.Objects.CR.Location.vBranchID>, IsNotNull>>, Selector<APQuickCheck.vendorLocationID, PX.Objects.CR.Location.vBranchID>, Case<Where<APQuickCheck.vendorID, IsNotNull, And<Not<Selector<APQuickCheck.vendorID, PX.Objects.AP.Vendor.vOrgBAccountID>, RestrictByBranch<Current2<APQuickCheck.branchID>>>>>, Null, Case<Where<Current2<APQuickCheck.branchID>, IsNotNull>, Current2<APQuickCheck.branchID>>>>>, Current<AccessInfo.branchID>>))]
  public int? BranchID { get; set; }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [RestrictVendorByBranch(typeof (APQuickCheck.branchID))]
  public int? VendorID { get; set; }
}
