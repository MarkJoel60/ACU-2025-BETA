// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.AssetMaintMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.FA;

public class AssetMaintMultipleBaseCurrencies : 
  FAAccrualTranMultipleBaseCurrenciesBase<AssetMaint.AdditionsViewExtension, AssetMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.GL.Branch.baseCuryID, Equal<Current<FixedAsset.baseCuryID>>, Or<IsNull<Current<FixedAsset.isAcquired>, False>, Equal<False>, And<Current<FixedAsset.splittedFrom>, IsNull, And<NotExists<Select<FATran, Where<FATran.assetID, Equal<Current<FixedAsset.assetID>>, And<FATran.tranType, Equal<FATran.tranType.reconcilliationPlus>>>>>>>>>), "The base currency of the {0} branch differs from the fixed asset's currency.", new Type[] {typeof (PX.Objects.GL.Branch.branchCD)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FALocationHistory.locationID> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FALocationHistory.locationID> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FALocationHistory.locationID>, object, object>) e).NewValue == null)
      return;
    FALocationHistory row = (FALocationHistory) e.Row;
    PX.Objects.GL.Branch branch = PXSelectorAttribute.Select<FALocationHistory.locationID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FALocationHistory.locationID>>) e).Cache, (object) row, (object) (int) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FALocationHistory.locationID>, object, object>) e).NewValue) as PX.Objects.GL.Branch;
    FixedAsset fixedAsset = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FALocationHistory.locationID>>) e).Cache.Graph, new object[1]
    {
      (object) row.AssetID
    }));
    if (fixedAsset.BaseCuryID == null || branch == null || !(branch.BaseCuryID != fixedAsset.BaseCuryID))
      return;
    if (((IQueryable<PXResult<FATran>>) PXSelectBase<FATran, PXSelect<FATran, Where<FATran.assetID, Equal<Required<FATran.assetID>>, And<FATran.tranType, Equal<FATran.tranType.reconcilliationPlus>>>>.Config>.SelectSingleBound(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FALocationHistory.locationID>>) e).Cache.Graph, (object[]) null, new object[1]
    {
      (object) fixedAsset.AssetID
    })).Any<PXResult<FATran>>())
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FALocationHistory.locationID>, object, object>) e).NewValue = (object) branch.BranchCD;
      throw new PXSetPropertyException("The base currency of the {0} branch differs from the fixed asset's currency.", new object[1]
      {
        (object) branch.BranchCD
      });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FALocationHistory, FALocationHistory.locationID> e)
  {
    if (e.Row == null)
      return;
    FADetails current = ((PXSelectBase<FADetails>) ((PXGraphExtension<AssetMaint>) this).Base.AssetDetails).Current;
    if (current == null)
      return;
    current.BaseCuryID = PXAccess.GetBranch(e.Row.LocationID).BaseCuryID;
  }

  [PXOverride]
  public IEnumerable additions(
    AssetMaintMultipleBaseCurrencies.ViewDelegate baseDelegate)
  {
    int? branchId = ((PXSelectBase<GLTranFilter>) ((PXGraphExtension<AssetMaint>) this).Base.GLTrnFilter).Current.BranchID;
    ((PXSelectBase<GLTranFilter>) ((PXGraphExtension<AssetMaint>) this).Base.GLTrnFilter).Current.BranchID = ((PXSelectBase<FixedAsset>) ((PXGraphExtension<AssetMaint>) this).Base.Asset).Current.BranchID;
    ((PXSelectBase) ((PXGraphExtension<AssetMaint>) this).Base.GLTrnFilter).Cache.RaiseFieldUpdated<GLTranFilter.branchID>((object) ((PXSelectBase<GLTranFilter>) ((PXGraphExtension<AssetMaint>) this).Base.GLTrnFilter).Current, (object) branchId);
    return baseDelegate();
  }

  [PXOverride]
  public virtual BqlCommand GetSelectCommand(
    GLTranFilter filter,
    AssetMaintMultipleBaseCurrencies.GetSelectCommandDelegate baseDelegate)
  {
    BqlCommand query = baseDelegate(filter);
    return this.ModifySelectCommand(filter, query);
  }

  public delegate IEnumerable ViewDelegate();

  public delegate BqlCommand GetSelectCommandDelegate(GLTranFilter filter);
}
