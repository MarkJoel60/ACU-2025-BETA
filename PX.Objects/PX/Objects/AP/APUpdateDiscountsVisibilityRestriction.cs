// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APUpdateDiscountsVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AP;

public class APUpdateDiscountsVisibilityRestriction : PXGraphExtension<APUpdateDiscounts>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.visibilityRestriction>();

  public override void Initialize()
  {
    base.Initialize();
    this.Base.NonPromotionSequences.WhereAnd<Where<Vendor.vOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>();
    this.Base.Sequences.WhereAnd<Where<Vendor.vOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>();
  }
}
