// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorRVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.AP;

public sealed class VendorRVisibilityRestriction : PXCacheExtension<VendorR>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [VendorRaw(typeof (Where2<Where<Vendor.type, Equal<BAccountType.vendorType>, Or<Vendor.type, Equal<BAccountType.combinedType>>>, And<Where<Vendor.vOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>>), DescriptionField = typeof (Vendor.acctName), IsKey = true, DisplayName = "Vendor ID")]
  public string AcctCD { get; set; }
}
