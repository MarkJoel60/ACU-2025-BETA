// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.UsageFilterVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CT;

public sealed class UsageFilterVisibilityRestriction : PXCacheExtension<UsageMaint.UsageFilter>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [PXSelector(typeof (Search2<Contract.contractID, LeftJoin<BAccountR, On<Contract.customerID, Equal<BAccountR.bAccountID>>>, Where<Contract.baseType, Equal<CTPRType.contract>, And<Contract.status, NotEqual<Contract.status.draft>, And<Contract.status, NotEqual<Contract.status.inApproval>>>>>), SubstituteKey = typeof (Contract.contractCD), DescriptionField = typeof (Contract.description))]
  [RestrictCustomerByUserBranches(typeof (BAccountR.cOrgBAccountID))]
  public int? ContractID { get; set; }
}
