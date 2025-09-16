// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.AssetMaintVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.FA;

public class AssetMaintVisibilityRestriction : PXGraphExtension<AssetMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [RestrictVendorByUserBranches]
  public virtual void FADetails_LessorID_CacheAttached(PXCache sender)
  {
  }
}
