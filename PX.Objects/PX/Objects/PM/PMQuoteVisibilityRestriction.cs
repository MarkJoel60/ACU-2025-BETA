// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMQuoteVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CR.Standalone;
using PX.Objects.CS;
using PX.Objects.GL;

#nullable disable
namespace PX.Objects.PM;

public sealed class PMQuoteVisibilityRestriction : PXCacheExtension<PMQuote>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [Branch(null, null, true, true, true, IsDetail = false, TabOrder = 0, BqlField = typeof (CROpportunityRevision.branchID))]
  [PXFormula(typeof (Switch<Case<Where<PendingValue<CROpportunityRevision.branchID>, IsNotNull>, Null, Case<Where<CROpportunityRevision.locationID, IsNotNull, And<Selector<CROpportunityRevision.locationID, PX.Objects.CR.Location.cBranchID>, IsNotNull>>, Selector<CROpportunityRevision.locationID, PX.Objects.CR.Location.cBranchID>, Case<Where<CROpportunityRevision.bAccountID, IsNotNull, And<Not<Selector<CROpportunityRevision.bAccountID, PX.Objects.CR.BAccount.cOrgBAccountID>, RestrictByBranch<Current2<CROpportunityRevision.branchID>>>>>, Null, Case<Where<Current2<CROpportunityRevision.branchID>, IsNotNull>, Current2<CROpportunityRevision.branchID>>>>>, Current<AccessInfo.branchID>>))]
  public int? BranchID { get; set; }

  [PXMergeAttributes]
  [RestrictCustomerByBranch(typeof (BAccountR.cOrgBAccountID), typeof (PMQuote.branchID))]
  public int? BAccountID { get; set; }
}
