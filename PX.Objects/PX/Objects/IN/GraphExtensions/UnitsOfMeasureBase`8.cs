// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.UnitsOfMeasureBase`8
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class UnitsOfMeasureBase<TGraph, TUnitID, TUnitType, TParent, TParentID, TParentBaseUnit, TParentSalesUnit, TParentPurchaseUnit> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TUnitID : class, IBqlField
  where TUnitType : class, IConstant, new()
  where TParent : class, IBqlTable, new()
  where TParentID : class, IBqlField
  where TParentBaseUnit : class, IBqlField
  where TParentSalesUnit : class, IBqlField
  where TParentPurchaseUnit : class, IBqlField
{
  protected PXCache<TParent> ParentCache => GraphHelper.Caches<TParent>((PXGraph) this.Base);

  protected PXCache<INUnit> UnitCache => GraphHelper.Caches<INUnit>((PXGraph) this.Base);

  public TParent ParentCurrent => (TParent) ((PXCache) this.ParentCache).Current;

  protected abstract IEnumerable<INUnit> SelectOwnConversions(string baseUnit);

  protected abstract IEnumerable<INUnit> SelectParentConversions(string baseUnit);

  protected abstract INUnit GetBaseUnit(int? parentID, string baseUnit);

  protected abstract INUnit CreateUnitCopy(int? parentID, INUnit unit);

  protected abstract void InitBaseUnit(int? parentID, string newValue);

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  protected virtual void _(Events.CacheAttached<INUnit.unitType> eventArgs)
  {
  }

  protected virtual void _(Events.FieldDefaulting<INUnit, INUnit.toUnit> e)
  {
    if ((object) this.ParentCurrent == null)
      return;
    e.Row.SampleToUnit = (string) ((PXCache) this.ParentCache).GetValue<TParentBaseUnit>((object) this.ParentCurrent);
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<INUnit, INUnit.toUnit>, INUnit, object>) e).NewValue = (object) e.Row.SampleToUnit;
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<INUnit, INUnit.toUnit>>) e).Cancel = true;
  }

  protected virtual void _(Events.FieldDefaulting<INUnit, INUnit.unitType> e)
  {
    if ((object) this.ParentCurrent == null)
      return;
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<INUnit, INUnit.unitType>, INUnit, object>) e).NewValue = this.GetUnitType();
  }

  protected virtual void _(Events.FieldVerifying<INUnit, INUnit.unitRate> e)
  {
    Decimal? newValue = (Decimal?) ((Events.FieldVerifyingBase<Events.FieldVerifying<INUnit, INUnit.unitRate>, INUnit, object>) e).NewValue;
    Decimal num = 0M;
    if (newValue.GetValueOrDefault() <= num & newValue.HasValue)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
      {
        (object) "0"
      });
  }

  protected virtual void _(Events.FieldVerifying<INUnit, INUnit.fromUnit> e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>() || e.Row == null || !((Events.FieldVerifyingBase<Events.FieldVerifying<INUnit, INUnit.fromUnit>>) e).ExternalCall)
      return;
    string newValue = (string) ((Events.FieldVerifyingBase<Events.FieldVerifying<INUnit, INUnit.fromUnit>, INUnit, object>) e).NewValue;
    if (!string.IsNullOrEmpty(newValue) && newValue == (string) ((PXCache) this.ParentCache).GetValue<TParentBaseUnit>((object) this.ParentCurrent))
      throw new PXSetPropertyException("The entered unit is the base unit and cannot be used to convert from. Enter a different unit.");
  }

  protected virtual void _(Events.RowInserted<TParent> e)
  {
    if (string.IsNullOrEmpty((string) ((PXCache) this.ParentCache).GetValue<TParentBaseUnit>((object) e.Row)))
      return;
    using (new ReadOnlyScope(new PXCache[1]
    {
      (PXCache) this.UnitCache
    }))
      ((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TParent>>) e).Cache.RaiseFieldUpdated<TParentBaseUnit>((object) e.Row, (object) null);
  }

  protected virtual void _(Events.RowPersisting<TParent> e)
  {
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TParent>>) e).Cache.RaiseFieldUpdated<TParentBaseUnit>((object) e.Row, ((PXCache) this.ParentCache).GetValue<TParentBaseUnit>((object) e.Row));
  }

  protected virtual void _(Events.FieldVerifying<TParent, TParentSalesUnit> e)
  {
    ((Events.FieldVerifyingBase<Events.FieldVerifying<TParent, TParentSalesUnit>>) e).Cancel = true;
  }

  protected virtual void _(
    Events.FieldVerifying<TParent, TParentPurchaseUnit> e)
  {
    ((Events.FieldVerifyingBase<Events.FieldVerifying<TParent, TParentPurchaseUnit>>) e).Cancel = true;
  }

  protected virtual void _(Events.FieldVerifying<TParent, TParentBaseUnit> e)
  {
    ((Events.FieldVerifyingBase<Events.FieldVerifying<TParent, TParentBaseUnit>>) e).Cancel = true;
  }

  protected virtual void _(Events.FieldUpdated<TParent, TParentBaseUnit> e)
  {
    int? parentID = (int?) ((PXCache) this.ParentCache).GetValue<TParentID>((object) e.Row);
    string str = (string) ((PXCache) this.ParentCache).GetValue<TParentBaseUnit>((object) e.Row);
    string oldValue = (string) ((Events.FieldUpdatedBase<Events.FieldUpdated<TParent, TParentBaseUnit>, TParent, object>) e).OldValue;
    if (!string.Equals(oldValue, str))
    {
      if (!string.IsNullOrEmpty(oldValue))
      {
        this.UnitCache.Delete(this.GetBaseUnit(parentID, oldValue));
        foreach (INUnit selectOwnConversion in this.SelectOwnConversions(oldValue))
          this.UnitCache.Delete(selectOwnConversion);
      }
      if (!string.IsNullOrEmpty(str))
      {
        foreach (INUnit parentConversion in this.SelectParentConversions(str))
          this.UnitCache.Insert(this.CreateUnitCopy(parentID, parentConversion));
      }
    }
    if (string.IsNullOrEmpty(str))
      return;
    this.InitBaseUnit(parentID, str);
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TParent, TParentBaseUnit>>) e).Cache.RaiseFieldUpdated<TParentSalesUnit>((object) e.Row, (object) (string) ((PXCache) this.ParentCache).GetValue<TParentSalesUnit>((object) e.Row));
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TParent, TParentBaseUnit>>) e).Cache.RaiseFieldUpdated<TParentPurchaseUnit>((object) e.Row, (object) (string) ((PXCache) this.ParentCache).GetValue<TParentPurchaseUnit>((object) e.Row));
  }

  protected virtual void _(
    Events.ExceptionHandling<TParent, TParentSalesUnit> e)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>())
      return;
    ((Events.ExceptionHandlingBase<Events.ExceptionHandling<TParent, TParentSalesUnit>>) e).Cancel = true;
  }

  protected virtual void _(
    Events.ExceptionHandling<TParent, TParentPurchaseUnit> e)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>())
      return;
    ((Events.ExceptionHandlingBase<Events.ExceptionHandling<TParent, TParentPurchaseUnit>>) e).Cancel = true;
  }

  private object GetUnitType() => new TUnitType().Value;

  protected INUnit ResolveConversion(
    short unitType,
    string fromUnit,
    string toUnit,
    int? inventoryID,
    int? itemClassID)
  {
    return new INUnit()
    {
      UnitType = new short?(unitType),
      ItemClassID = itemClassID,
      InventoryID = inventoryID,
      FromUnit = fromUnit,
      ToUnit = toUnit,
      UnitRate = new Decimal?(1M),
      PriceAdjustmentMultiplier = new Decimal?(1M),
      UnitMultDiv = "M"
    };
  }
}
