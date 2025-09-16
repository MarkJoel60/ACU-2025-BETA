// Decompiled with JetBrains decompiler
// Type: ReconciliationTools.ARGLDiscrepancyByCustomerEnqFilterVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;

#nullable disable
namespace ReconciliationTools;

public sealed class ARGLDiscrepancyByCustomerEnqFilterVisibilityRestriction : 
  PXCacheExtension<ARGLDiscrepancyByCustomerEnqFilter>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [RestrictCustomerByBranch(typeof (DiscrepancyEnqFilter.branchID), ResetCustomer = true)]
  public int? CustomerID { get; set; }
}
