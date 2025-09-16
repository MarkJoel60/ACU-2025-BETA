// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOLineCost
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

[PXProtectedAccess(null)]
public abstract class SOLineCost : PXGraphExtension<SOOrderEntry>
{
  private NonStockKitSpecHelper _kitSpecHelper;
  private bool _isUnitCostCorrection;
  private bool _isCuryUnitCostCorrection;
  private bool _isCostsRecalculationScope;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  /// <summary>
  /// Gets <see cref="T:PX.Objects.IN.NonStockKitSpecHelper" /> object used to handle non-stock kits.
  /// </summary>
  protected NonStockKitSpecHelper KitSpecHelper
  {
    get
    {
      return this._kitSpecHelper ?? (this._kitSpecHelper = new NonStockKitSpecHelper((PXGraph) this.Base));
    }
  }

  /// Uses <see cref="M:PX.Objects.SO.SOOrderEntry.IsCuryUnitCostEnabled(PX.Objects.SO.SOLine,PX.Objects.SO.SOOrder)" />
  [PXProtectedAccess(null)]
  protected abstract bool IsCuryUnitCostEnabled(PX.Objects.SO.SOLine line, PX.Objects.SO.SOOrder order);

  /// <summary>
  /// Returns cost of base UOM of specified item considering cost settings and order date for non-stock items.
  /// </summary>
  /// <param name="inventoryID">Inventory item ID.</param>
  private Decimal? GetBaseUomUnitCost(PX.Objects.SO.SOLine soline, int? inventoryID)
  {
    if (!inventoryID.HasValue)
      return new Decimal?();
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, inventoryID);
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, soline.BranchID);
    Decimal? baseUomUnitCost = new Decimal?();
    if (inventoryItem != null && !inventoryItem.StkItem.GetValueOrDefault() && ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current != null)
    {
      InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this.Base, inventoryID, branch?.BaseCuryID);
      DateTime? stdCostDate = (DateTime?) itemCurySettings?.StdCostDate;
      DateTime? orderDate = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderDate;
      baseUomUnitCost = (stdCostDate.HasValue & orderDate.HasValue ? (stdCostDate.GetValueOrDefault() <= orderDate.GetValueOrDefault() ? 1 : 0) : 0) == 0 ? new Decimal?(((Decimal?) itemCurySettings?.LastStdCost).GetValueOrDefault()) : new Decimal?(((Decimal?) itemCurySettings?.StdCost).GetValueOrDefault());
    }
    if (!baseUomUnitCost.HasValue)
    {
      INItemSite inItemSite = PXResultset<INItemSite>.op_Implicit(PXSelectBase<INItemSite, PXSelect<INItemSite, Where<INItemSite.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>, And<INItemSite.siteID, Equal<Current<PX.Objects.SO.SOLine.siteID>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new PX.Objects.SO.SOLine[1]
      {
        soline
      }, new object[1]{ (object) inventoryID }));
      baseUomUnitCost = inItemSite == null || !inItemSite.TranUnitCost.HasValue ? (Decimal?) INItemCost.PK.Find((PXGraph) this.Base, inventoryID, branch?.BaseCuryID)?.TranUnitCost : inItemSite.TranUnitCost;
    }
    return baseUomUnitCost;
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.unitCost> e)
  {
    PX.Objects.SO.SOLine row = e.Row;
    if (string.IsNullOrEmpty(row?.UOM))
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.unitCost>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) 0M;
    }
    else
    {
      Decimal? nullable1;
      if (PXAccess.FeatureInstalled<FeaturesSet.kitAssemblies>() && ((PXSelectBase<SOSetup>) this.Base.sosetup).Current != null && ((PXSelectBase<SOSetup>) this.Base.sosetup).Current.SalesProfitabilityForNSKits != "K" && PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, row.InventoryID).With<PX.Objects.IN.InventoryItem, bool>((Func<PX.Objects.IN.InventoryItem, bool>) (i =>
      {
        bool? stkItem = i.StkItem;
        bool flag = false;
        return stkItem.GetValueOrDefault() == flag & stkItem.HasValue && i.KitItem.GetValueOrDefault();
      })))
      {
        nullable1 = new Decimal?(0M);
        foreach (KeyValuePair<int, Decimal> keyValuePair in EnumerableExtensions.ToDictionary<int, Decimal>((IEnumerable<KeyValuePair<int, Decimal>>) this.KitSpecHelper.GetNonStockKitSpec(row.InventoryID.Value)))
        {
          Decimal? nullable2 = nullable1;
          Decimal? nullable3 = this.GetBaseUomUnitCost(row, new int?(keyValuePair.Key));
          Decimal num = keyValuePair.Value;
          Decimal? nullable4 = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num) : new Decimal?();
          Decimal? nullable5;
          if (!(nullable2.HasValue & nullable4.HasValue))
          {
            nullable3 = new Decimal?();
            nullable5 = nullable3;
          }
          else
            nullable5 = new Decimal?(nullable2.GetValueOrDefault() + nullable4.GetValueOrDefault());
          nullable1 = nullable5;
        }
        if (((PXSelectBase<SOSetup>) this.Base.sosetup).Current.SalesProfitabilityForNSKits == "C")
        {
          Decimal? nullable6 = nullable1;
          Decimal? baseUomUnitCost = this.GetBaseUomUnitCost(row, row.InventoryID);
          nullable1 = nullable6.HasValue & baseUomUnitCost.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + baseUomUnitCost.GetValueOrDefault()) : new Decimal?();
        }
      }
      else
        nullable1 = this.GetBaseUomUnitCost(row, row.InventoryID);
      if (!nullable1.HasValue)
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.unitCost>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) 0M;
      }
      else
      {
        Decimal? nullable7 = new Decimal?(INUnitAttribute.ConvertToBase<PX.Objects.SO.SOLine.inventoryID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.unitCost>>) e).Cache, (object) row, row.UOM, nullable1.Value, INPrecision.UNITCOST));
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.unitCost>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) nullable7;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.unitCost>>) e).Cancel = true;
      }
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.unitCost> e)
  {
    if (this._isCuryUnitCostCorrection)
      return;
    Decimal valueOrDefault = ((Decimal?) e.NewValue).GetValueOrDefault();
    Decimal baseval;
    PXDBCurrencyAttribute.CuryConvCury<PX.Objects.SO.SOLine.curyInfoID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.unitCost>>) e).Cache, (object) e.Row, valueOrDefault, out baseval, CommonSetupDecPl.PrcCst);
    Decimal? curyUnitCost = e.Row.CuryUnitCost;
    Decimal num = baseval;
    if (curyUnitCost.GetValueOrDefault() == num & curyUnitCost.HasValue)
      return;
    try
    {
      this._isUnitCostCorrection = true;
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.unitCost>>) e).Cache.SetValueExt<PX.Objects.SO.SOLine.curyUnitCost>((object) e.Row, (object) baseval);
    }
    finally
    {
      this._isUnitCostCorrection = false;
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.curyUnitCost> e)
  {
    if (this._isUnitCostCorrection || !((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.curyUnitCost>>) e).ExternalCall && !this._isCostsRecalculationScope)
      return;
    Decimal valueOrDefault = ((Decimal?) e.NewValue).GetValueOrDefault();
    Decimal baseval;
    PXDBCurrencyAttribute.CuryConvBase<PX.Objects.SO.SOLine.curyInfoID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.curyUnitCost>>) e).Cache, (object) e.Row, valueOrDefault, out baseval, CommonSetupDecPl.PrcCst);
    Decimal? unitCost = e.Row.UnitCost;
    Decimal num = baseval;
    if (unitCost.GetValueOrDefault() == num & unitCost.HasValue)
      return;
    try
    {
      this._isCuryUnitCostCorrection = true;
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.curyUnitCost>>) e).Cache.SetValueExt<PX.Objects.SO.SOLine.unitCost>((object) e.Row, (object) baseval);
    }
    finally
    {
      this._isCuryUnitCostCorrection = false;
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.extCost> e)
  {
    Decimal valueOrDefault = ((Decimal?) e.NewValue).GetValueOrDefault();
    Decimal curyval;
    PXDBCurrencyAttribute.CuryConvCury<PX.Objects.SO.SOLine.curyInfoID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.extCost>>) e).Cache, (object) e.Row, valueOrDefault, out curyval);
    Decimal? curyExtCost = e.Row.CuryExtCost;
    Decimal num = curyval;
    if (curyExtCost.GetValueOrDefault() == num & curyExtCost.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.extCost>>) e).Cache.SetValueExt<PX.Objects.SO.SOLine.curyExtCost>((object) e.Row, (object) curyval);
  }

  /// <summary>
  /// Create a scope for recalculation of unit costs and extended costs
  /// </summary>
  public IDisposable CostsRecalculationScope()
  {
    return (IDisposable) new SOLineCost.RecalculateCostsScope(this);
  }

  private class RecalculateCostsScope : IDisposable
  {
    private readonly SOLineCost _parent;
    private readonly bool _initMode;

    public RecalculateCostsScope(SOLineCost parent)
    {
      this._parent = parent;
      this._initMode = this._parent._isCostsRecalculationScope;
      this._parent._isCostsRecalculationScope = true;
    }

    void IDisposable.Dispose() => this._parent._isCostsRecalculationScope = this._initMode;
  }
}
