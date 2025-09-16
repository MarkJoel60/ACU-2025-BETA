// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.AP1099YearMasterVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AP;

public sealed class AP1099YearMasterVisibilityRestriction : PXCacheExtension<AP1099YearMaster>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [RestrictVendorByOrganization(typeof (AP1099YearMaster.orgBAccountID))]
  public int? VendorID { get; set; }
}
