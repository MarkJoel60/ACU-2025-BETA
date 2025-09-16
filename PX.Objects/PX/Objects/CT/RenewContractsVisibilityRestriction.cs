// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.RenewContractsVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CT;

public class RenewContractsVisibilityRestriction : PXGraphExtension<RenewContracts>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.Base.ItemsInitialCommand = BqlCommand.AppendJoin<LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<RenewContracts.ContractsList.customerID>>>>(this.Base.ItemsInitialCommand);
    this.Base.ItemsInitialCommand = this.Base.ItemsInitialCommand.WhereAnd<Where<PX.Objects.AR.Customer.cOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>();
  }
}
