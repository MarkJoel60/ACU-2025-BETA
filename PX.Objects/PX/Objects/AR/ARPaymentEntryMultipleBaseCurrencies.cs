// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPaymentEntryMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.AR;

public class ARPaymentEntryMultipleBaseCurrencies : PXGraphExtension<ARPaymentEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  protected virtual void _(PX.Data.Events.FieldVerifying<ARPayment.branchID> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARPayment.branchID>, object, object>) e).NewValue == null)
      return;
    PX.Objects.GL.Branch branch = PXSelectorAttribute.Select<ARPayment.branchID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ARPayment.branchID>>) e).Cache, e.Row, (object) (int) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARPayment.branchID>, object, object>) e).NewValue) as PX.Objects.GL.Branch;
    string str = (string) PXFormulaAttribute.Evaluate<ARPaymentMultipleBaseCurrenciesRestriction.customerBaseCuryID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ARPayment.branchID>>) e).Cache, e.Row);
    if (str != null && branch != null && branch.BaseCuryID != str)
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARPayment.branchID>, object, object>) e).NewValue = (object) branch.BranchCD;
      BAccountR baccountR = PXSelectorAttribute.Select<ARPayment.customerID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ARPayment.branchID>>) e).Cache, e.Row) as BAccountR;
      throw new PXSetPropertyException("The branch base currency differs from the base currency of the {0} entity associated with the {1} account.", new object[2]
      {
        (object) PXOrgAccess.GetCD(baccountR.COrgBAccountID),
        (object) baccountR.AcctCD
      });
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<ARPayment> e)
  {
    PX.Objects.GL.Branch branch = PXSelectorAttribute.Select<ARPayment.branchID>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARPayment>>) e).Cache, (object) e.Row, (object) e.Row.BranchID) as PX.Objects.GL.Branch;
    if (!(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARPayment>>) e).Cache.GetValueExt<ARPaymentMultipleBaseCurrenciesRestriction.customerBaseCuryID>((object) e.Row) is PXFieldState valueExt) || valueExt.Value == null || branch == null || !(branch.BaseCuryID != valueExt.ToString()))
      return;
    e.Row.BranchID = new int?();
  }
}
