// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.CostStatuses.Abstraction.CostStatusAccumulatorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.IN.Exceptions;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN.InventoryRelease.Accumulators.CostStatuses.Abstraction;

public class CostStatusAccumulatorAttribute : PXAccumulatorAttribute
{
  protected Type _QuantityField;
  protected Type _CostField;
  protected Type _InventoryIDField;
  protected Type _SubItemIDField;
  protected Type _SiteIDField;
  protected Type _SpecificNumberField;
  protected Type _LayerTypeField;
  protected Type _ReceiptNbr;

  public CostStatusAccumulatorAttribute(
    Type quantityField,
    Type costField,
    Type inventoryIDField,
    Type subItemIDField,
    Type siteIDField,
    Type specificNumberField,
    Type layerTypeField,
    Type receiptNbr)
    : this()
  {
    this._QuantityField = quantityField;
    this._CostField = costField;
    this._InventoryIDField = inventoryIDField;
    this._SubItemIDField = subItemIDField;
    this._SiteIDField = siteIDField;
    this._SpecificNumberField = specificNumberField;
    this._LayerTypeField = layerTypeField;
    this._ReceiptNbr = receiptNbr;
    ((PXDBInterceptorAttribute) this).PersistOrder = (PersistOrder) 10;
  }

  public CostStatusAccumulatorAttribute(
    Type quantityField,
    Type costField,
    Type inventoryIDField,
    Type subItemIDField,
    Type siteIDField,
    Type layerTypeField,
    Type receiptNbr)
    : this(quantityField, costField, inventoryIDField, subItemIDField, siteIDField, (Type) null, layerTypeField, receiptNbr)
  {
  }

  protected CostStatusAccumulatorAttribute() => this.SingleRecord = true;

  protected virtual bool PrepareInsert(PXCache cache, object row, PXAccumulatorCollection columns)
  {
    if (!base.PrepareInsert(cache, row, columns))
      return false;
    INCostStatus inCostStatus = (INCostStatus) row;
    if (inCostStatus.LayerType != "U")
    {
      columns.AppendException(this._SpecificNumberField == (Type) null ? "The available quantity of the {0} {1} item is not sufficient in the {2} warehouse." : "Updating item '{0} {1}' in warehouse '{2}' in cost layer '{3}' quantity  will go negative.", new PXAccumulatorRestriction[2]
      {
        new PXAccumulatorRestriction(this._QuantityField.Name, (PXComp) 3, (object) 0M),
        new PXAccumulatorRestriction(this._LayerTypeField.Name, (PXComp) 0, (object) "O")
      });
      columns.AppendException(this._SpecificNumberField == (Type) null ? "The available quantity of the {0} {1} item is not sufficient in the {2} warehouse." : "Updating item '{0} {1}' in warehouse '{2}' in cost layer '{3}' quantity  will go negative.", new PXAccumulatorRestriction[2]
      {
        new PXAccumulatorRestriction(this._QuantityField.Name, (PXComp) 5, (object) 0M),
        new PXAccumulatorRestriction(this._LayerTypeField.Name, (PXComp) 0, (object) "N")
      });
      columns.AppendException("Updating item '{0} {1}' in warehouse '{2}' caused cost to quantity imbalance.", new PXAccumulatorRestriction[2]
      {
        new PXAccumulatorRestriction(this._QuantityField.Name, (PXComp) 1, (object) 0M),
        new PXAccumulatorRestriction(this._CostField.Name, (PXComp) 0, (object) 0M)
      });
      columns.AppendException("Updating item '{0} {1}' in warehouse '{2}' caused cost to quantity imbalance.", new PXAccumulatorRestriction[2]
      {
        new PXAccumulatorRestriction(this._QuantityField.Name, (PXComp) 5, (object) 0M),
        new PXAccumulatorRestriction(this._CostField.Name, (PXComp) 3, (object) 0M)
      });
      columns.AppendException("Updating item '{0} {1}' in warehouse '{2}' caused cost to quantity imbalance.", new PXAccumulatorRestriction[2]
      {
        new PXAccumulatorRestriction(this._QuantityField.Name, (PXComp) 3, (object) 0M),
        new PXAccumulatorRestriction(this._CostField.Name, (PXComp) 5, (object) 0M)
      });
    }
    columns.Update<INCostStatus.unitCost>((PXDataFieldAssign.AssignBehavior) 0);
    columns.Update<INCostStatus.origQty>(inCostStatus.OverrideOrigQty.GetValueOrDefault() ? (PXDataFieldAssign.AssignBehavior) 0 : (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<INCostStatus.qtyOnHand>((PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<INCostStatus.totalCost>((PXDataFieldAssign.AssignBehavior) 1);
    return true;
  }

  public virtual bool PersistInserted(PXCache cache, object row)
  {
    try
    {
      return base.PersistInserted(cache, row);
    }
    catch (PXRestrictionViolationException ex)
    {
      List<object> objectList = new List<object>();
      int? inventoryID = new int?();
      int? subitemID = new int?();
      int? siteID = new int?();
      if (cache.BqlKeys.Contains(this._InventoryIDField))
      {
        inventoryID = (int?) cache.GetValue(row, this._InventoryIDField.Name);
        objectList.Add(PXForeignSelectorAttribute.GetValueExt(cache, row, this._InventoryIDField.Name));
      }
      if (cache.BqlKeys.Contains(this._SubItemIDField))
        subitemID = (int?) cache.GetValue(row, this._SubItemIDField.Name);
      if (cache.BqlKeys.Contains(this._SiteIDField))
        siteID = (int?) cache.GetValue(row, this._SiteIDField.Name);
      bool flag = this.ShouldUseDefaultQtyCostImbalanceError(cache, inventoryID, siteID);
      if (cache.BqlKeys.Contains(this._SubItemIDField) & flag)
        objectList.Add(PXForeignSelectorAttribute.GetValueExt(cache, row, this._SubItemIDField.Name));
      if (cache.BqlKeys.Contains(this._SiteIDField))
        objectList.Add(PXForeignSelectorAttribute.GetValueExt(cache, row, this._SiteIDField.Name));
      if (EnumerableExtensions.IsIn<int>(ex.Index, 0, 1) && this._SpecificNumberField != (Type) null && cache.BqlKeys.Contains(this._SpecificNumberField))
        objectList.Add(PXForeignSelectorAttribute.GetValueExt(cache, row, this._SpecificNumberField.Name));
      throw new UpdateQtyCostStatusImbalanceException(inventoryID, subitemID, siteID, EnumerableExtensions.IsIn<int>(ex.Index, 0, 1) ? (this._SpecificNumberField == (Type) null ? "The available quantity of the {0} {1} item is not sufficient in the {2} warehouse." : "Updating item '{0} {1}' in warehouse '{2}' in cost layer '{3}' quantity  will go negative.") : (flag ? "Updating item '{0} {1}' in warehouse '{2}' caused cost to quantity imbalance." : "The total cost of the {0} item in the {1} warehouse differs from the quantity on hand multiplied by standard cost. Recalculate the item costs at the warehouse on the Update Standard Costs (IN502000) form by selecting the Revalue Inventory check box and processing the item."), objectList.ToArray());
    }
  }

  private bool ShouldUseDefaultQtyCostImbalanceError(PXCache cache, int? inventoryID, int? siteID)
  {
    InventoryItem inventoryItem = InventoryItem.PK.Find(cache.Graph, inventoryID);
    if (inventoryItem == null || inventoryItem.ValMethod != "T")
      return true;
    CommonSetup commonSetup = PXResultset<CommonSetup>.op_Implicit(PXSelectBase<CommonSetup, PXSelect<CommonSetup>.Config>.Select(cache.Graph, Array.Empty<object>()));
    PX.Objects.CM.Currency currency = CurrencyCollection.GetCurrency(INSite.PK.Find(cache.Graph, siteID)?.BaseCuryID);
    if (commonSetup != null && currency != null)
    {
      short? nullable1 = commonSetup.DecPlPrcCst;
      int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      nullable1 = currency.DecimalPlaces;
      int? nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      if (!(nullable2.GetValueOrDefault() <= nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue))
      {
        INItemSite inItemSite = INItemSite.PK.Find(cache.Graph, inventoryID, siteID);
        if (inItemSite == null)
          return true;
        InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find(cache.Graph, inventoryID, currency.CuryID);
        if (itemCurySettings == null)
          return true;
        Decimal num = inItemSite.StdCostOverride.GetValueOrDefault() ? inItemSite.StdCost.GetValueOrDefault() : itemCurySettings.StdCost.GetValueOrDefault();
        Decimal d = num;
        nullable1 = currency.DecimalPlaces;
        int valueOrDefault = (int) nullable1.GetValueOrDefault();
        return Math.Round(d, valueOrDefault) == num;
      }
    }
    return true;
  }
}
