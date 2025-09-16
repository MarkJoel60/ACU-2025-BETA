// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPaymentVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;

#nullable disable
namespace PX.Objects.AR;

public sealed class ARPaymentVisibilityRestriction : PXCacheExtension<ARPayment>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see> to which the document belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [PXMergeAttributes]
  [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDetail = false, TabOrder = 0)]
  [PXFormula(typeof (Switch<Case<Where<PendingValue<ARPayment.branchID>, IsPending>, Null, Case<Where<ARPayment.customerLocationID, IsNotNull, And<Selector<ARPayment.customerLocationID, PX.Objects.CR.Location.cBranchID>, IsNotNull>>, Selector<ARPayment.customerLocationID, PX.Objects.CR.Location.cBranchID>, Case<Where<ARPayment.customerID, IsNotNull, And<Not<Selector<ARPayment.customerID, Customer.cOrgBAccountID>, RestrictByBranch<Current2<ARPayment.branchID>>>>>, Null, Case<Where<Current2<ARPayment.branchID>, IsNotNull>, Current2<ARPayment.branchID>>>>>, Current<AccessInfo.branchID>>))]
  public int? BranchID { get; set; }

  [PXMergeAttributes]
  [RestrictCustomerByBranch(typeof (ARPayment.branchID))]
  public int? CustomerID { get; set; }
}
