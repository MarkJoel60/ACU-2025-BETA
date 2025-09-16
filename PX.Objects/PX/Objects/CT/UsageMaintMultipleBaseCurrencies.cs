// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.UsageMaintMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.CT;

public class UsageMaintMultipleBaseCurrencies : PXGraphExtension<UsageMaint>
{
  public PXSelect<ContractBillingSchedule, Where<ContractBillingSchedule.contractID, Equal<Current<Contract.contractID>>>> Billing;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  protected virtual void _(PX.Data.Events.RowSelected<Contract> e)
  {
    ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.Billing).Select(Array.Empty<object>()));
  }

  [PXMergeAttributes]
  [Branch(typeof (Coalesce<Search<PX.Objects.GL.Branch.branchID, Where<PX.Objects.GL.Branch.bAccountID, Equal<Current<ContractBillingScheduleMultipleBaseCurrencies.cOrgBAccountID>>>>, Search<PX.Objects.GL.Branch.branchID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>>), null, true, true, true, IsDetail = true)]
  [PXRestrictor(typeof (Where<PX.Objects.GL.Branch.baseCuryID, Equal<Current<ContractBillingScheduleMultipleBaseCurrencies.customerBaseCuryID>>, Or<Current<ContractBillingScheduleMultipleBaseCurrencies.customerBaseCuryID>, IsNull>>), "", new System.Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.branchID> e)
  {
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PMTran.branchID> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTran.branchID>, object, object>) e).NewValue == null || ((PXSelectBase<Contract>) this.Base.CurrentContract).Current == null || ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current == null)
      return;
    BAccountR baccountR = (BAccountR) PXSelectorAttribute.Select<ContractBillingSchedule.accountID>(((PXSelectBase) this.Billing).Cache, (object) ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current);
    PX.Objects.GL.Branch branch = PXSelectorAttribute.Select<PMTran.branchID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMTran.branchID>>) e).Cache, e.Row, (object) (int) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTran.branchID>, object, object>) e).NewValue) as PX.Objects.GL.Branch;
    if (baccountR.BaseCuryID != null && branch.BaseCuryID != baccountR.BaseCuryID)
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTran.branchID>, object, object>) e).NewValue = (object) branch.BranchCD;
      throw new PXSetPropertyException("The branch base currency differs from the base currency of the {0} entity associated with the {1} account.", new object[2]
      {
        (object) PXOrgAccess.GetCD(baccountR.COrgBAccountID),
        (object) baccountR.AcctCD
      });
    }
  }

  [PXDecimal(8)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.CM.Extensions.CurrencyInfo.sampleCuryRate> e)
  {
  }
}
