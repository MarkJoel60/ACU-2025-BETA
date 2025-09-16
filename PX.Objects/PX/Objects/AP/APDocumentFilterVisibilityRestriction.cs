// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APDocumentFilterVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AP;

public sealed class APDocumentFilterVisibilityRestriction : 
  PXCacheExtension<APDocumentEnq.APDocumentFilter>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [RestrictVendorByOrganization(typeof (APDocumentEnq.APDocumentFilter.orgBAccountID))]
  public int? VendorID { get; set; }
}
