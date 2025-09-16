// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.InventoryItemMaintBaseExt.UnitsOfMeasure
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN.GraphExtensions.InventoryItemMaintBaseExt;

public class UnitsOfMeasure : 
  INUnitLotSerClassBase<
  #nullable disable
  InventoryItemMaintBase, INUnit.inventoryID, INUnitType.inventoryItem, PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.inventoryID, PX.Objects.IN.InventoryItem.baseUnit, PX.Objects.IN.InventoryItem.salesUnit, PX.Objects.IN.InventoryItem.purchaseUnit, PX.Objects.IN.InventoryItem.lotSerClassID>
{
  [PXDependToCache(new Type[] {typeof (PX.Objects.IN.InventoryItem)})]
  public FbqlSelect<SelectFromBase<INUnit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INUnit.inventoryID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  INUnit.toUnit, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.IN.InventoryItem.baseUnit, IBqlString>.AsOptional>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  INUnit.fromUnit, IBqlString>.IsNotEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.IN.InventoryItem.baseUnit, IBqlString>.AsOptional>>>, 
  #nullable disable
  INUnit>.View itemunits;

  public override void Initialize()
  {
    base.Initialize();
    ((PXSelectBase) this.itemunits).AllowSelect = PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>();
    SOSetup current = ((PXSelectBase<SOSetup>) this.Base.sosetup).Current;
    PXUIFieldAttribute.SetVisible<INUnit.toUnit>(((PXSelectBase) this.itemunits).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<INUnit.toUnit>(((PXSelectBase) this.itemunits).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<INUnit.sampleToUnit>(((PXSelectBase) this.itemunits).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<INUnit.sampleToUnit>(((PXSelectBase) this.itemunits).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<INUnit.priceAdjustmentMultiplier>(((PXSelectBase) this.itemunits).Cache, (object) null, current != null && current.UsePriceAdjustmentMultiplier.GetValueOrDefault());
    this.MaintainCachesOrder();
  }

  protected virtual void MaintainCachesOrder()
  {
    int num = ((PXGraph) this.Base).Views.Caches.IndexOf(typeof (PX.Objects.IN.InventoryItem));
    int index = ((PXGraph) this.Base).Views.Caches.IndexOf(typeof (INUnit));
    if (index <= num || num < 0)
      return;
    ((PXGraph) this.Base).Views.Caches.RemoveAt(index);
    ((PXGraph) this.Base).Views.Caches.Insert(num + 1, typeof (INUnit));
  }

  protected override IEnumerable<INUnit> SelectOwnConversions(string baseUnit)
  {
    return (IEnumerable<INUnit>) ((PXSelectBase<INUnit>) this.itemunits).SelectMain(new object[2]
    {
      (object) baseUnit,
      (object) baseUnit
    });
  }

  protected override IEnumerable<INUnit> SelectParentConversions(string baseUnit)
  {
    return GraphHelper.RowCast<INUnit>((IEnumerable) PXSelectBase<INUnit, PXViewOf<INUnit>.BasedOn<SelectFromBase<INUnit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INUnit.unitType, Equal<INUnitType.itemClass>>>>, And<BqlOperand<INUnit.itemClassID, IBqlInt>.IsEqual<BqlField<PX.Objects.IN.InventoryItem.parentItemClassID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INUnit.toUnit, IBqlString>.IsEqual<BqlField<PX.Objects.IN.InventoryItem.baseUnit, IBqlString>.FromCurrent>>>>.And<BqlOperand<INUnit.fromUnit, IBqlString>.IsNotEqual<BqlField<PX.Objects.IN.InventoryItem.baseUnit, IBqlString>.FromCurrent>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) baseUnit,
      (object) baseUnit
    }));
  }

  protected override INUnit GetBaseUnit(int? parentID, string baseUnit)
  {
    return this.ResolveConversion((short) 1, baseUnit, baseUnit, parentID, new int?(0));
  }

  protected override INUnit CreateUnitCopy(int? parentID, INUnit unit)
  {
    INUnit copy = PXCache<INUnit>.CreateCopy(unit);
    copy.InventoryID = parentID;
    copy.ItemClassID = new int?(0);
    copy.UnitType = new short?((short) 1);
    copy.RecordID = new long?();
    return copy;
  }

  protected override void InitBaseUnit(int? parentID, string newValue)
  {
    if (INUnit.UK.ByInventory.FindDirty((PXGraph) this.Base, parentID, newValue) != null)
      return;
    this.UnitCache.Insert(this.ResolveConversion((short) 1, newValue, newValue, parentID, new int?(0)));
  }

  private void InsertConversion(
    int? parentID,
    string fromUnit,
    string toUnit,
    string oppositeTypeUnit,
    string oldFromValue)
  {
    if (string.IsNullOrEmpty(fromUnit))
      return;
    if (INUnit.UK.ByInventory.FindDirty((PXGraph) this.Base, parentID, fromUnit) == null)
    {
      INUnit dirty = INUnit.UK.ByGlobal.FindDirty((PXGraph) this.Base, fromUnit, toUnit);
      INUnit inUnit;
      if (dirty != null)
      {
        inUnit = PXCache<INUnit>.CreateCopy(dirty);
        inUnit.UnitType = new short?((short) 1);
        inUnit.ItemClassID = new int?(0);
        inUnit.InventoryID = parentID;
        inUnit.RecordID = new long?();
      }
      else
        inUnit = this.ResolveConversion((short) 1, fromUnit, toUnit, parentID, new int?(0));
      this.UnitCache.Insert(inUnit);
    }
    if (!EnumerableExtensions.IsNotIn<string>(oldFromValue, (string) null, string.Empty, fromUnit, toUnit, oppositeTypeUnit, Array.Empty<string>()))
      return;
    INUnit inUnit1 = this.ResolveConversion((short) 1, oldFromValue, toUnit, parentID, new int?(0));
    if (this.UnitCache.GetStatus(inUnit1) != 2)
      return;
    this.UnitCache.Delete(inUnit1);
  }

  protected override void ValidateUnitConversions(PX.Objects.IN.InventoryItem validatedItem)
  {
    if (validatedItem == null)
      return;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<INUnit>(new PXDataField[4]
    {
      (PXDataField) new PXDataField<INUnit.toUnit>(),
      (PXDataField) new PXDataFieldValue<INUnit.unitType>((object) (short) 1),
      (PXDataField) new PXDataFieldValue<INUnit.inventoryID>((object) validatedItem.InventoryID),
      (PXDataField) new PXDataFieldValue<INUnit.toUnit>((PXDbType) 12, new int?(6), (object) validatedItem.BaseUnit, (PXComp) 1)
    }))
    {
      if (pxDataRecord != null)
        throw new PXException("The {0} value specified in the To Unit box differs from the {2} base unit specified for the {1} item. To resolve the issue, please contact your Acumatica support provider.", new object[3]
        {
          (object) pxDataRecord.GetString(0),
          (object) validatedItem.InventoryCD,
          (object) validatedItem.BaseUnit
        });
    }
    if (PXDatabase.SelectMulti<INUnit>(new PXDataField[5]
    {
      (PXDataField) new PXDataField<INUnit.toUnit>(),
      (PXDataField) new PXDataFieldValue<INUnit.unitType>((object) (short) 1),
      (PXDataField) new PXDataFieldValue<INUnit.inventoryID>((object) validatedItem.InventoryID),
      (PXDataField) new PXDataFieldValue<INUnit.fromUnit>((PXDbType) 12, (object) validatedItem.BaseUnit),
      (PXDataField) new PXDataFieldValue<INUnit.toUnit>((PXDbType) 12, (object) validatedItem.BaseUnit)
    }).Count<PXDataRecord>() != 1)
      throw new PXException("The conversion rule of the {0} unit of measure to the {1} unit of measure is not found for the {2} item. To resolve the issue, please contact your Acumatica support provider.", new object[3]
      {
        (object) validatedItem.BaseUnit,
        (object) validatedItem.BaseUnit,
        (object) validatedItem.InventoryCD
      });
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDBDefault(typeof (PX.Objects.IN.InventoryItem.inventoryID))]
  protected virtual void _(PX.Data.Events.CacheAttached<INUnit.inventoryID> eventArgs)
  {
  }

  protected override void _(PX.Data.Events.RowSelected<INUnit> e)
  {
    PXFieldState stateExt = (PXFieldState) ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INUnit>>) e).Cache.GetStateExt<INUnit.unitMultDiv>((object) e.Row);
    string str;
    if (stateExt.Error != null && !(stateExt.Error == PXMessages.Localize("Fractional unit conversions not supported for serial numbered items", ref str)))
      return;
    base._(e);
  }

  protected virtual void _(PX.Data.Events.RowInserting<INUnit> e)
  {
    if (e.Row != null && e.Row.ToUnit == null)
      e.Cancel = true;
    if (e.Row == null)
      return;
    foreach (INUnit inUnit in ((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<INUnit>>) e).Cache.Deleted)
    {
      if (((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<INUnit>>) e).Cache.ObjectsEqual((object) inUnit, (object) e.Row))
      {
        e.Row.RecordID = inUnit.RecordID;
        e.Row.tstamp = inUnit.tstamp;
        break;
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowInserted<INUnit> e)
  {
    if (e.Row.FromUnit == null)
      return;
    short? unitType = e.Row.UnitType;
    int? nullable = unitType.HasValue ? new int?((int) unitType.GetValueOrDefault()) : new int?();
    int num = 1;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue) || INUnit.UK.ByGlobal.FindDirty((PXGraph) this.Base, e.Row.FromUnit, e.Row.FromUnit) != null)
      return;
    INUnit inUnit = this.ResolveConversion((short) 3, e.Row.FromUnit, e.Row.FromUnit, new int?(0), new int?(0));
    ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<INUnit>>) e).Cache.RaiseRowInserting((object) inUnit);
    ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<INUnit>>) e).Cache.SetStatus((object) inUnit, (PXEntryStatus) 2);
    ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<INUnit>>) e).Cache.ClearQueryCacheObsolete();
  }

  protected virtual void _(PX.Data.Events.RowDeleted<INUnit> e)
  {
    short? unitType = e.Row.UnitType;
    int? nullable = unitType.HasValue ? new int?((int) unitType.GetValueOrDefault()) : new int?();
    int num = 1;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
      return;
    INUnit inUnit = this.ResolveConversion((short) 3, e.Row.FromUnit, e.Row.FromUnit, new int?(0), new int?(0));
    if (((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<INUnit>>) e).Cache.GetStatus((object) inUnit) != 2)
      return;
    ((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<INUnit>>) e).Cache.SetStatus((object) inUnit, (PXEntryStatus) 4);
    ((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<INUnit>>) e).Cache.ClearQueryCacheObsolete();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.salesUnit> e)
  {
    this.InsertConversion(e.Row.InventoryID, e.Row.SalesUnit, e.Row.BaseUnit, e.Row.PurchaseUnit, (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.salesUnit>, PX.Objects.IN.InventoryItem, object>) e).OldValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.purchaseUnit> e)
  {
    this.InsertConversion(e.Row.InventoryID, e.Row.PurchaseUnit, e.Row.BaseUnit, e.Row.SalesUnit, (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.purchaseUnit>, PX.Objects.IN.InventoryItem, object>) e).OldValue);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.IN.InventoryItem> e)
  {
    if (e.Row == null)
      return;
    foreach (PXResult<INUnit> pxResult in PXSelectBase<INUnit, PXViewOf<INUnit>.BasedOn<SelectFromBase<INUnit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INUnit.unitType, Equal<INUnitType.inventoryItem>>>>>.And<BqlOperand<INUnit.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) e.Row.InventoryID
    }))
      ((PXSelectBase<INUnit>) this.itemunits).Delete(PXResult<INUnit>.op_Implicit(pxResult));
  }
}
