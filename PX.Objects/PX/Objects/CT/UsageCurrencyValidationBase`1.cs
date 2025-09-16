// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.UsageCurrencyValidationBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CT;

public abstract class UsageCurrencyValidationBase<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  protected virtual void ValidateContractUsageBaseCurrency(int? contractID)
  {
    if (!contractID.HasValue)
      return;
    BAccount baccount = (BAccount) PXSelectorAttribute.Select<ContractBillingSchedule.accountID>(this.Base.Caches[typeof (ContractBillingSchedule)], (object) PXResultset<ContractBillingSchedule>.op_Implicit(PXSelectBase<ContractBillingSchedule, PXSelect<ContractBillingSchedule, Where<ContractBillingSchedule.contractID, Equal<Required<Contract.contractID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) contractID
    })));
    if (baccount.BaseCuryID != null && baccount.BaseCuryID != this.Base.Accessinfo.BaseCuryID)
      throw new PXException("The usage of the {0} customer who is to be billed for the contract is restricted in the {1} branch, which will be used in an AR document for contract billing. Sign in to the branch where the usage of the {2} customer is allowed.", new object[3]
      {
        (object) baccount.AcctCD,
        (object) PXAccess.GetBranchCD(this.Base.Accessinfo.BranchID),
        (object) baccount.AcctCD
      });
  }
}
