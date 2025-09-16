// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLandedCostDocVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.Common.Formula;
using PX.Objects.CS;
using PX.Objects.GL;

#nullable disable
namespace PX.Objects.PO;

public sealed class POLandedCostDocVisibilityRestriction : PXCacheExtension<POLandedCostDoc>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [Branch(null, null, true, true, true, IsDetail = false)]
  [PXFormula(typeof (Switch<Case<Where<IsCopyPasteContext, Equal<True>, And<Current2<POLandedCostDoc.branchID>, IsNotNull>>, Current2<POLandedCostDoc.branchID>, Case<Where<POLandedCostDoc.vendorLocationID, IsNotNull, And<Selector<POLandedCostDoc.vendorLocationID, PX.Objects.CR.Location.vBranchID>, IsNotNull>>, Selector<POLandedCostDoc.vendorLocationID, PX.Objects.CR.Location.vBranchID>, Case<Where<POLandedCostDoc.vendorID, IsNotNull, And<Not<Selector<POLandedCostDoc.vendorID, PX.Objects.AP.Vendor.vOrgBAccountID>, RestrictByBranch<Current2<POLandedCostDoc.branchID>>>>>, Null, Case<Where<Current2<POLandedCostDoc.branchID>, IsNotNull>, Current2<POLandedCostDoc.branchID>>>>>, Current<AccessInfo.branchID>>))]
  public int? BranchID { get; set; }

  [PXMergeAttributes]
  [RestrictVendorByBranch(typeof (POLandedCostDoc.branchID), ResetVendor = false)]
  public int? VendorID { get; set; }
}
