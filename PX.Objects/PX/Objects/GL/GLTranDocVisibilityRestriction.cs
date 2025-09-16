// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLTranDocVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.GL;

public sealed class GLTranDocVisibilityRestriction : PXCacheExtension<GLTranDoc>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [RestrictCustomerByBranch(typeof (BAccountR.cOrgBAccountID), typeof (GLDocBatch.branchID), ResetCustomer = true)]
  [RestrictVendorByBranch(typeof (BAccountR.vOrgBAccountID), typeof (GLDocBatch.branchID), ResetVendor = true)]
  public int? BAccountID { get; set; }
}
