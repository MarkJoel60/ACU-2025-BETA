// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProformaEntryMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.PM;

public class ProformaEntryMultipleBaseCurrencies : PXGraphExtension<ProformaEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  protected virtual void _(PX.Data.Events.FieldVerifying<PMProforma.branchID> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProforma.branchID>, object, object>) e).NewValue == null)
      return;
    PX.Objects.GL.Branch branch = PXSelectorAttribute.Select<PMProforma.branchID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMProforma.branchID>>) e).Cache, e.Row, (object) (int) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProforma.branchID>, object, object>) e).NewValue) as PX.Objects.GL.Branch;
    if (((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMProforma.branchID>>) e).Cache.GetValueExt<ProformaMultipleBaseCurrenciesRestriction.customerBaseCuryID>(e.Row) is PXFieldState valueExt && valueExt.Value != null && branch.BaseCuryID != valueExt.ToString())
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProforma.branchID>, object, object>) e).NewValue = (object) branch.BranchCD;
      BAccountR baccountR = PXSelectorAttribute.Select<PMProforma.customerID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMProforma.branchID>>) e).Cache, e.Row) as BAccountR;
      throw new PXSetPropertyException("The branch base currency differs from the base currency of the {0} entity associated with the {1} account.", new object[2]
      {
        (object) PXOrgAccess.GetCD(baccountR.COrgBAccountID),
        (object) baccountR.AcctCD
      });
    }
  }
}
