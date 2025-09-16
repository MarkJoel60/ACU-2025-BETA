// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POCreateFilterVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.SO.POCreateExt;

#nullable disable
namespace PX.Objects.PO;

public sealed class POCreateFilterVisibilityRestriction : 
  PXCacheExtension<SOxPOCreateFilter, POCreate.POCreateFilter>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [RestrictCustomerByUserBranches(typeof (BAccountR.cOrgBAccountID))]
  public int? CustomerID { get; set; }

  [PXMergeAttributes]
  [RestrictVendorByBranch(typeof (POCreate.POCreateFilter.branchID))]
  public int? VendorID { get; set; }
}
