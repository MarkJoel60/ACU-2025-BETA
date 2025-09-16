// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractMaintMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.CT;

public class ContractMaintMultipleBaseCurrencies : PXGraphExtension<ContractMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  protected virtual void _(PX.Data.Events.FieldVerifying<Contract.curyID> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Contract.curyID>, object, object>) e).NewValue == null)
      return;
    Contract row = (Contract) e.Row;
    string newValue = (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Contract.curyID>, object, object>) e).NewValue;
    if (row.CuryID == newValue)
      return;
    foreach (PXResult<ContractDetail> pxResult in ((PXSelectBase<ContractDetail>) this.Base.ContractDetails).Select(Array.Empty<object>()))
    {
      ContractItem contractItem = ContractItem.PK.Find((PXGraph) this.Base, PXResult<ContractDetail>.op_Implicit(pxResult).ContractItemID);
      if (contractItem.CuryID != newValue)
        throw new PXSetPropertyException("The contract currency cannot be changed because the {0} contract item with the {1} currency is provided under the contract.", new object[2]
        {
          (object) contractItem.ContractItemCD,
          (object) contractItem.CuryID
        });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<ContractBillingSchedule.accountID> e)
  {
    int? valueOriginal = (int?) ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ContractBillingSchedule.accountID>>) e).Cache.GetValueOriginal<ContractBillingSchedule.accountID>(e.Row);
    if (!valueOriginal.HasValue || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ContractBillingSchedule.accountID>, object, object>) e).NewValue == null)
      return;
    int? newValue = (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ContractBillingSchedule.accountID>, object, object>) e).NewValue;
    int? nullable = valueOriginal;
    if (newValue.GetValueOrDefault() == nullable.GetValueOrDefault() & newValue.HasValue == nullable.HasValue)
      return;
    BAccountR baccountR1 = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXSelect<BAccountR, Where<BAccountR.bAccountID, Equal<Required<BAccountR.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) valueOriginal
    }));
    BAccountR baccountR2 = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXSelect<BAccountR, Where<BAccountR.bAccountID, Equal<Required<BAccountR.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ContractBillingSchedule.accountID>, object, object>) e).NewValue
    }));
    if (baccountR2.BaseCuryID == null || baccountR1.BaseCuryID == baccountR2.BaseCuryID)
      return;
    PMTran pmTran = PXResultset<PMTran>.op_Implicit(PXSelectBase<PMTran, PXSelectJoin<PMTran, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<PMTran.branchID>, And<PX.Objects.GL.Branch.baseCuryID, NotEqual<Required<PX.Objects.GL.Branch.baseCuryID>>>>>, Where<PMTran.projectID, Equal<Required<PMTran.projectID>>, And<PMTran.billable, Equal<True>, And<PMTran.billed, Equal<False>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[2]
    {
      (object) baccountR2.BaseCuryID,
      (object) ((ContractBillingSchedule) e.Row).ContractID
    }));
    if (pmTran != null)
    {
      PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(pmTran.BranchID);
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ContractBillingSchedule.accountID>, object, object>) e).NewValue = (object) baccountR2.AcctCD;
      throw new PXSetPropertyException("There is contract usage recorded for this contract under the {0} branch. Usage of the {1} customer is restricted in this branch and you will not be able to bill the contract usage to the customer.", new object[2]
      {
        (object) branch.BranchCD,
        (object) baccountR2.AcctCD
      });
    }
  }
}
