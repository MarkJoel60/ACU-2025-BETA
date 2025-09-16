// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POOrderVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.Common.Formula;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;

#nullable disable
namespace PX.Objects.PO;

public sealed class POOrderVisibilityRestriction : PXCacheExtension<POOrder>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDetail = false, TabOrder = 0)]
  [PXFormula(typeof (Switch<Case<Where<IsCopyPasteContext, Equal<True>, And<Current2<POOrder.branchID>, IsNotNull>>, Current2<POOrder.branchID>, Case<Where<POOrder.vendorLocationID, IsNotNull, And<Selector<POOrder.vendorLocationID, PX.Objects.CR.Location.vBranchID>, IsNotNull>>, Selector<POOrder.vendorLocationID, PX.Objects.CR.Location.vBranchID>, Case<Where<POOrder.vendorID, IsNotNull, And<Not<Selector<POOrder.vendorID, PX.Objects.AP.Vendor.vOrgBAccountID>, RestrictByBranch<Current2<POOrder.branchID>>>>>, Null, Case<Where<Current2<POOrder.branchID>, IsNotNull>, Current2<POOrder.branchID>>>>>, Current<AccessInfo.branchID>>))]
  public int? BranchID { get; set; }

  [PXMergeAttributes]
  [RestrictCustomerByUserBranches(typeof (BAccount2.cOrgBAccountID), typeof (Or<BAccount2.isBranch, Equal<True>>))]
  public int? ShipToBAccountID { get; set; }

  [PXMergeAttributes]
  [RestrictVendorByBranch(typeof (POOrder.branchID), ResetVendor = false)]
  public int? VendorID { get; set; }

  [PXMergeAttributes]
  [RestrictVendorByBranch(typeof (POOrder.branchID), ResetVendor = false)]
  public int? PayToVendorID { get; set; }
}
