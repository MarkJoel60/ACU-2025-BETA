// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ExpiringContractsEngVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CT;

public class ExpiringContractsEngVisibilityRestriction : PXGraphExtension<ExpiringContractsEng>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R2.")]
  public virtual void Initialize() => ((PXGraphExtension) this).Initialize();

  [PXOverride]
  public virtual PXSelectBase<Contract> GetContractsView(Func<PXSelectBase<Contract>> baseMethod)
  {
    PXSelectBase<Contract> contractsView = baseMethod();
    contractsView.WhereAnd<Where<PX.Objects.AR.Customer.cOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>();
    return contractsView;
  }
}
