// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoiceVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.GL;

#nullable disable
namespace PX.Objects.AP;

public sealed class APInvoiceVisibilityRestriction : PXCacheExtension<APInvoice>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDetail = false, TabOrder = 0)]
  [PXFormula(typeof (Switch<Case<Where<PendingValue<APInvoice.branchID>, IsPending>, Null, Case<Where<APInvoice.vendorLocationID, IsNotNull, And<Selector<APInvoice.vendorLocationID, PX.Objects.CR.Location.vBranchID>, IsNotNull>>, Selector<APInvoice.vendorLocationID, PX.Objects.CR.Location.vBranchID>, Case<Where<APInvoice.vendorID, IsNotNull, And<Not<Selector<APInvoice.vendorID, Vendor.vOrgBAccountID>, RestrictByBranch<Current2<APInvoice.branchID>>>>>, Null, Case<Where<Current2<APInvoice.branchID>, IsNotNull>, Current2<APInvoice.branchID>>>>>, Current<AccessInfo.branchID>>))]
  public int? BranchID { get; set; }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [RestrictVendorByBranch(typeof (APInvoice.branchID))]
  public int? VendorID { get; set; }
}
