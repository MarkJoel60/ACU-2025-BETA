// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.BillingFilterVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.PM;

public sealed class BillingFilterVisibilityRestriction : 
  PXCacheExtension<BillingProcess.BillingFilter>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [RestrictCustomerClassByUserBranches]
  public string CustomerClassID { get; set; }

  [PXMergeAttributes]
  [RestrictCustomerByUserBranches]
  public int? CustomerID { get; set; }
}
