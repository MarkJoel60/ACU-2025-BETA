// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPPDTaxAdjustmentParametersVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.AR;

/// <summary>
/// Grpah extention to restrict customer visibility for ARPPD tax adjustment parameters
/// </summary>
public sealed class ARPPDTaxAdjustmentParametersVisibilityRestriction : 
  PXCacheExtension<ARPPDTaxAdjustmentParameters>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  /// <summary>Customer ID</summary>
  [PXMergeAttributes]
  [RestrictCustomerByBranch(typeof (ARPPDTaxAdjustmentParameters.branchID), ResetCustomer = true)]
  public int? CustomerID { get; set; }
}
