// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRSchedulePrimaryVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.DR;

public class DRSchedulePrimaryVisibilityRestriction : PXGraphExtension<DRSchedulePrimary>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase<DRSchedule>) this.Base.Items).Join<LeftJoin<BAccountR, On<BAccountR.bAccountID, Equal<DRSchedule.bAccountID>>>>();
    ((PXSelectBase<DRSchedule>) this.Base.Items).WhereAnd<Where<BAccountR.cOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>();
    ((PXSelectBase<DRSchedule>) this.Base.Items).WhereAnd<Where<BAccountR.vOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>();
  }
}
