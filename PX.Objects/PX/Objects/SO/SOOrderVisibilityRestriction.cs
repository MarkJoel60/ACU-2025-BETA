// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.Common.Formula;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;

#nullable disable
namespace PX.Objects.SO;

public sealed class SOOrderVisibilityRestriction : PXCacheExtension<SOOrder>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDetail = false, TabOrder = 0)]
  [PXFormula(typeof (Switch<Case<Where<IsCopyPasteContext, Equal<True>, And<Current2<SOOrder.branchID>, IsNotNull>>, Current2<SOOrder.branchID>, Case<Where<SOOrder.customerLocationID, IsNotNull, And<Selector<SOOrder.customerLocationID, PX.Objects.CR.Location.cBranchID>, IsNotNull>>, Selector<SOOrder.customerLocationID, PX.Objects.CR.Location.cBranchID>, Case<Where<Current2<SOOrder.customerID>, IsNotNull, And<Selector<Current2<SOOrder.customerID>, PX.Objects.AR.Customer.cOrgBAccountID>, IsNotNull, And<Not<Selector<Current2<SOOrder.customerID>, PX.Objects.AR.Customer.cOrgBAccountID>, RestrictByBranch<Current2<SOOrder.branchID>>>>>>, Null, Case<Where<Current2<SOOrder.branchID>, IsNotNull>, Current2<SOOrder.branchID>>>>>, Current<AccessInfo.branchID>>), KeepIdleSelfUpdates = true)]
  public int? BranchID { get; set; }

  [PXMergeAttributes]
  [RestrictCustomerByBranch(typeof (BAccountR.cOrgBAccountID), typeof (Or<Current<SOOrder.behavior>, Equal<SOBehavior.tR>>), typeof (SOOrder.branchID))]
  public int? CustomerID { get; set; }
}
