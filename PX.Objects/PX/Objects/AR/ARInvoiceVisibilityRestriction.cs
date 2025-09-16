// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceVisibilityRestriction
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

public sealed class ARInvoiceVisibilityRestriction : PXCacheExtension<ARInvoice>
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
  [PXFormula(typeof (Switch<Case<Where<PendingValue<ARInvoice.branchID>, IsPending>, Null, Case<Where<ARInvoice.customerLocationID, IsNotNull, And<Selector<ARInvoice.customerLocationID, PX.Objects.CR.Location.cBranchID>, IsNotNull>>, Selector<ARInvoice.customerLocationID, PX.Objects.CR.Location.cBranchID>, Case<Where<ARInvoice.customerID, IsNotNull, And<Not<Selector<ARInvoice.customerID, Customer.cOrgBAccountID>, RestrictByBranch<Current2<ARInvoice.branchID>>>>>, Null, Case<Where<Current2<ARInvoice.branchID>, IsNotNull>, Current2<ARInvoice.branchID>>>>>, Current<AccessInfo.branchID>>))]
  public int? BranchID { get; set; }

  [PXMergeAttributes]
  [RestrictCustomerByBranch(typeof (ARInvoice.branchID))]
  public int? CustomerID { get; set; }
}
