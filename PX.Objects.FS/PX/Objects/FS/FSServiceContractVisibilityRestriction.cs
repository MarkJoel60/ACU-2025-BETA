// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSServiceContractVisibilityRestriction
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.FS;

public sealed class FSServiceContractVisibilityRestriction : PXCacheExtension<FSServiceContract>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [RestrictCustomerByUserBranches]
  public int? CustomerID { get; set; }

  [PXMergeAttributes]
  [RestrictCustomerByBranch(typeof (FSServiceContract.branchID), ResetCustomer = true)]
  public int? BillCustomerID { get; set; }

  [PXRemoveBaseAttribute(typeof (FSSelectorVendorAttribute))]
  [PXMergeAttributes]
  [FSSelectorVendorRestrictVisibility]
  public int? VendorID { get; set; }
}
