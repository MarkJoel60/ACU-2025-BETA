// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRQuoteProcessFilterVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CR;

/// <exclude />
public sealed class CRQuoteProcessFilterVisibilityRestriction : 
  PXCacheExtension<CRQuoteProcessFilter>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [RestrictCustomerByUserBranches(typeof (BAccount.cOrgBAccountID))]
  public int? BAccountID { get; set; }
}
