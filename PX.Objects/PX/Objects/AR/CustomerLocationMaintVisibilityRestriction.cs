// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerLocationMaintVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.AR;

public sealed class CustomerLocationMaintVisibilityRestriction : 
  PXGraphExtension<CustomerLocationMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase<PX.Objects.CR.Location>) this.Base.Location).WhereAnd<Where<PX.Objects.CR.BAccount.cOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>();
  }

  [PXMergeAttributes]
  [RestrictCustomerByUserBranches]
  public void Location_BAccountID_CacheAttached(PXCache sender)
  {
  }
}
