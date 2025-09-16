// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable disable
namespace PX.Objects.AP;

public sealed class VendorMultipleBaseCurrencies : 
  PXCacheExtension<VendorVisibilityRestriction, Vendor>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>();

  [PXRestrictor(typeof (Where<Vendor.baseCuryID, Equal<BqlField<Vendor.baseCuryID, IBqlString>.FromCurrent>>), "", new System.Type[] {})]
  [PXMergeAttributes(Method = MergeMethod.Append)]
  public int? PayToVendorID { get; set; }
}
