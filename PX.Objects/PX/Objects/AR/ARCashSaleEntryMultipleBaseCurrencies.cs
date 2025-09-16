// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARCashSaleEntryMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.Standalone;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.AR;

public sealed class ARCashSaleEntryMultipleBaseCurrencies : PXGraphExtension<ARCashSaleEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXRestrictor(typeof (Where<Customer.baseCuryID, IsNull, Or<Customer.baseCuryID, EqualBaseCuryID<BqlField<ARCashSale.branchID, IBqlInt>.FromCurrent>>>), "", new System.Type[] {}, SuppressVerify = false)]
  [PXMergeAttributes]
  public void _(PX.Data.Events.CacheAttached<ARCashSale.customerID> e)
  {
  }

  protected void _(PX.Data.Events.FieldVerifying<ARCashSale.branchID> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARCashSale.branchID>, object, object>) e).NewValue == null)
      return;
    PX.Objects.GL.Branch branch = PXSelectorAttribute.Select<ARCashSale.branchID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ARCashSale.branchID>>) e).Cache, e.Row, (object) (int) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARCashSale.branchID>, object, object>) e).NewValue) as PX.Objects.GL.Branch;
    BAccountR baccountR = PXSelectorAttribute.Select<ARCashSale.customerID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ARCashSale.branchID>>) e).Cache, e.Row) as BAccountR;
    if (branch != null && baccountR != null && baccountR.BaseCuryID != null && branch.BaseCuryID != baccountR.BaseCuryID)
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARCashSale.branchID>, object, object>) e).NewValue = (object) branch.BranchCD;
      throw new PXSetPropertyException("The branch base currency differs from the base currency of the {0} entity associated with the {1} account.", new object[2]
      {
        (object) PXOrgAccess.GetCD(baccountR.COrgBAccountID),
        (object) baccountR.AcctCD
      });
    }
  }
}
