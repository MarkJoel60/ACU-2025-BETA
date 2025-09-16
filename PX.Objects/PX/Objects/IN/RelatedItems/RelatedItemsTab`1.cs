// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.RelatedItemsTab`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN.RelatedItems;

/// <summary>
/// The extension provide an ability to edit related items for the current <see cref="T:PX.Objects.IN.InventoryItem" />.
/// The extension is active only if one or both of the following features are enabled: <see cref="T:PX.Objects.CS.FeaturesSet.relatedItems" /> and <see cref="T:PX.Objects.CS.FeaturesSet.commerceIntegration" />.
/// </summary>
/// <typeparam name="TGraph"></typeparam>
public class RelatedItemsTab<TGraph> : 
  PXGraphExtension<
  #nullable disable
  TGraph>,
  PXImportAttribute.IPXPrepareItems,
  PXImportAttribute.IPXProcess
  where TGraph : PXGraph
{
  [PXDependToCache(new Type[] {typeof (PX.Objects.IN.InventoryItem)})]
  [PXImport(typeof (INRelatedInventory))]
  public FbqlSelect<SelectFromBase<INRelatedInventory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  INRelatedInventory.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.FromCurrent>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  INRelatedInventory.relation, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  INRelatedInventory.rank, IBqlInt>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  INRelatedInventory.inventoryID, IBqlInt>.Asc>>, 
  #nullable disable
  INRelatedInventory>.View RelatedItems;
  public PXAction<PX.Objects.IN.InventoryItem> viewRelatedItem;
  private MultiDuplicatesSearchEngine<INRelatedInventory> _duplicateFinder;

  protected PX.Objects.IN.InventoryItem OriginalInventory
  {
    get
    {
      return (PX.Objects.IN.InventoryItem) ((PXCache) GraphHelper.Caches<PX.Objects.IN.InventoryItem>((PXGraph) this.Base)).Current;
    }
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewRelatedItem(PXAdapter adapter)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, (int?) ((PXSelectBase<INRelatedInventory>) this.RelatedItems).Current?.RelatedInventoryID);
    if (inventoryItem != null)
      PXRedirectHelper.TryRedirect(this.Base.Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem, "View Related Item", (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  protected virtual void _(
    Events.FieldUpdated<INRelatedInventory, INRelatedInventory.relation> e)
  {
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INRelatedInventory, INRelatedInventory.relation>>) e).Cache.SetValue<INRelatedInventory.rank>((object) e.Row, (object) 0);
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INRelatedInventory, INRelatedInventory.relation>>) e).Cache.SetDefaultExt<INRelatedInventory.rank>((object) e.Row);
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INRelatedInventory, INRelatedInventory.relation>>) e).Cache.SetDefaultExt<INRelatedInventory.interchangeable>((object) e.Row);
    if (!e.Row.Required.GetValueOrDefault() || !(e.Row.Relation == "USELL"))
      return;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INRelatedInventory, INRelatedInventory.relation>>) e).Cache.SetDefaultExt<INRelatedInventory.required>((object) e.Row);
  }

  protected virtual void _(
    Events.FieldUpdated<INRelatedInventory, INRelatedInventory.uom> e)
  {
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INRelatedInventory, INRelatedInventory.uom>>) e).Cache.SetDefaultExt<INRelatedInventory.qty>((object) e.Row);
  }

  protected virtual void _(
    Events.FieldUpdated<INRelatedInventory, INRelatedInventory.isActive> e)
  {
    if (!((bool?) e.NewValue).GetValueOrDefault() || ((Events.FieldUpdatedBase<Events.FieldUpdated<INRelatedInventory, INRelatedInventory.isActive>, INRelatedInventory, object>) e).OldValue == e.NewValue)
      return;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INRelatedInventory, INRelatedInventory.isActive>>) e).Cache.SetValue<INRelatedInventory.expirationDate>((object) e.Row, (object) null);
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INRelatedInventory, INRelatedInventory.isActive>>) e).Cache.SetDefaultExt<INRelatedInventory.effectiveDate>((object) e.Row);
  }

  protected virtual void _(
    Events.FieldUpdated<INRelatedInventory, INRelatedInventory.effectiveDate> e)
  {
    DateTime? newValue = (DateTime?) e.NewValue;
    if (!this.IsCorrectActivePeriod((DateTime?) e.NewValue, e.Row.ExpirationDate))
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INRelatedInventory, INRelatedInventory.effectiveDate>>) e).Cache.SetValue<INRelatedInventory.expirationDate>((object) e.Row, (object) null);
    if (!newValue.HasValue)
      return;
    DateTime? nullable = newValue;
    DateTime? businessDate = this.Base.Accessinfo.BusinessDate;
    if ((nullable.HasValue & businessDate.HasValue ? (nullable.GetValueOrDefault() > businessDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INRelatedInventory, INRelatedInventory.effectiveDate>>) e).Cache.SetValue<INRelatedInventory.isActive>((object) e.Row, (object) false);
  }

  protected virtual void _(
    Events.FieldUpdated<INRelatedInventory, INRelatedInventory.expirationDate> e)
  {
    DateTime? newValue = (DateTime?) e.NewValue;
    if (this.IsCorrectActivePeriod(newValue, e.Row.ExpirationDate))
    {
      if (!newValue.HasValue)
        return;
      DateTime? nullable = newValue;
      DateTime? businessDate = this.Base.Accessinfo.BusinessDate;
      if ((nullable.HasValue & businessDate.HasValue ? (nullable.GetValueOrDefault() < businessDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return;
    }
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INRelatedInventory, INRelatedInventory.expirationDate>>) e).Cache.SetValue<INRelatedInventory.isActive>((object) e.Row, (object) false);
  }

  protected virtual void _(Events.RowInserted<INRelatedInventory> e)
  {
    if (!this.Base.IsImportFromExcel || this._duplicateFinder == null)
      return;
    this._duplicateFinder.AddItem(e.Row);
  }

  protected virtual void _(
    Events.FieldDefaulting<INRelatedInventory, INRelatedInventory.rank> e)
  {
    if (e.Row?.Relation == null)
      return;
    INRelatedInventory relatedInventory = PXResultset<INRelatedInventory>.op_Implicit(PXSelectBase<INRelatedInventory, PXViewOf<INRelatedInventory>.BasedOn<SelectFromBase<INRelatedInventory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INRelatedInventory.inventoryID, Equal<BqlField<INRelatedInventory.inventoryID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<INRelatedInventory.relation, IBqlString>.IsEqual<BqlField<INRelatedInventory.relation, IBqlString>.FromCurrent>>>.Order<PX.Data.BQL.Fluent.By<BqlField<INRelatedInventory.rank, IBqlInt>.Desc>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new INRelatedInventory[1]
    {
      e.Row
    }, Array.Empty<object>()));
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<INRelatedInventory, INRelatedInventory.rank>, INRelatedInventory, object>) e).NewValue = (object) (((int?) relatedInventory?.Rank).GetValueOrDefault() + 1);
  }

  protected virtual void _(
    Events.FieldDefaulting<INRelatedInventory, INRelatedInventory.qty> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<INRelatedInventory, INRelatedInventory.qty>, INRelatedInventory, object>) e).NewValue = (object) this.CalculateRelatedItemQty(e.Row);
  }

  protected virtual void _(
    Events.FieldVerifying<INRelatedInventory, INRelatedInventory.required> e)
  {
    if (!((bool?) ((Events.FieldVerifyingBase<Events.FieldVerifying<INRelatedInventory, INRelatedInventory.required>, INRelatedInventory, object>) e).NewValue).GetValueOrDefault() || !(e.Row.Relation == "USELL"))
      return;
    ((Events.FieldVerifyingBase<Events.FieldVerifying<INRelatedInventory, INRelatedInventory.required>, INRelatedInventory, object>) e).NewValue = (object) false;
    ((Events.FieldVerifyingBase<Events.FieldVerifying<INRelatedInventory, INRelatedInventory.required>>) e).Cancel = true;
  }

  protected virtual bool IsCorrectActivePeriod(DateTime? startTime, DateTime? endTime)
  {
    if (!endTime.HasValue || !startTime.HasValue)
      return true;
    DateTime? nullable1 = startTime;
    DateTime? nullable2 = endTime;
    return nullable1.HasValue & nullable2.HasValue && nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault();
  }

  protected virtual Decimal? CalculateRelatedItemQty(INRelatedInventory relatedInventory)
  {
    if (relatedInventory == null || this.OriginalInventory == null)
      return new Decimal?();
    if (relatedInventory.Relation != "SUBST")
      return new Decimal?(1M);
    string baseUnit = this.OriginalInventory.BaseUnit;
    if (baseUnit == relatedInventory.UOM)
      return new Decimal?(1M);
    Decimal result;
    return INUnitAttribute.TryConvertGlobalUnits((PXGraph) this.Base, baseUnit, relatedInventory.UOM, 1M, INPrecision.QUANTITY, out result) ? new Decimal?(result) : new Decimal?(1M);
  }

  protected virtual void CheckForDuplicates()
  {
    bool flag = false;
    INRelatedInventory[] relatedInventoryArray = ((PXSelectBase<INRelatedInventory>) this.RelatedItems).SelectMain(Array.Empty<object>());
    MultiDuplicatesSearchEngine<INRelatedInventory> duplicatesSearchEngine = new MultiDuplicatesSearchEngine<INRelatedInventory>(((PXSelectBase) this.RelatedItems).Cache, (IEnumerable<Type>) this.GetAlternativeKeyFields());
    foreach (INRelatedInventory relatedInventory in relatedInventoryArray)
    {
      INRelatedInventory relatedItem = relatedInventory;
      if (duplicatesSearchEngine[relatedItem].Any<INRelatedInventory>((Func<INRelatedInventory, bool>) (x => this.HaveIntersection(x, relatedItem))))
      {
        flag = true;
        PXSetPropertyException propertyException = new PXSetPropertyException("A line with the {0} relation and the {1} UOM already exists for {2} for the specified date range. Remove the line or change its details.", (PXErrorLevel) 5, new object[3]
        {
          (object) new InventoryRelation.ListAttribute().ValueLabelDic[relatedItem.Relation],
          (object) relatedItem.UOM,
          ((PXSelectBase) this.RelatedItems).Cache.GetValueExt<INRelatedInventory.relatedInventoryID>((object) relatedItem)
        });
        if (((PXSelectBase) this.RelatedItems).Cache.RaiseExceptionHandling<INRelatedInventory.relatedInventoryID>((object) relatedItem, (object) null, (Exception) propertyException))
          throw propertyException;
      }
      else
        duplicatesSearchEngine.AddItem(relatedItem);
    }
    if (flag)
      throw new PXException("Changes cannot be saved. View the Related Items tab for details.");
  }

  protected virtual bool HaveIntersection(INRelatedInventory first, INRelatedInventory second)
  {
    if (first.ExpirationDate.HasValue || second.ExpirationDate.HasValue)
    {
      DateTime? nullable1;
      if (!first.ExpirationDate.HasValue)
      {
        DateTime? effectiveDate = first.EffectiveDate;
        nullable1 = second.ExpirationDate;
        if ((effectiveDate.HasValue & nullable1.HasValue ? (effectiveDate.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_14;
      }
      nullable1 = second.ExpirationDate;
      DateTime? nullable2;
      if (!nullable1.HasValue)
      {
        nullable1 = second.EffectiveDate;
        nullable2 = first.ExpirationDate;
        if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_14;
      }
      nullable2 = first.EffectiveDate;
      nullable1 = second.EffectiveDate;
      if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable1 = second.EffectiveDate;
        nullable2 = first.ExpirationDate;
        if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_14;
      }
      nullable2 = first.EffectiveDate;
      nullable1 = second.ExpirationDate;
      if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable1 = second.ExpirationDate;
        nullable2 = first.ExpirationDate;
        if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_14;
      }
      nullable2 = second.EffectiveDate;
      nullable1 = first.EffectiveDate;
      if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return false;
      nullable1 = first.ExpirationDate;
      nullable2 = second.ExpirationDate;
      return nullable1.HasValue & nullable2.HasValue && nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault();
    }
label_14:
    return true;
  }

  protected virtual Type[] GetAlternativeKeyFields()
  {
    return new Type[3]
    {
      typeof (INRelatedInventory.relatedInventoryID),
      typeof (INRelatedInventory.relation),
      typeof (INRelatedInventory.uom)
    };
  }

  private bool DontUpdateExistRecords
  {
    get
    {
      object obj;
      return this.Base.IsImportFromExcel && PXExecutionContext.Current.Bag.TryGetValue("_DONT_UPDATE_EXIST_RECORDS", out obj) && true.Equals(obj);
    }
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (viewName.Equals("RelatedItems", StringComparison.InvariantCultureIgnoreCase) && !this.DontUpdateExistRecords)
    {
      if (this._duplicateFinder == null)
      {
        INRelatedInventory[] items = ((PXSelectBase<INRelatedInventory>) this.RelatedItems).SelectMain(Array.Empty<object>());
        this._duplicateFinder = new MultiDuplicatesSearchEngine<INRelatedInventory>(((PXSelectBase) this.RelatedItems).Cache, (IEnumerable<Type>) this.GetAlternativeKeyFields(), (ICollection<INRelatedInventory>) items);
      }
      INRelatedInventory row = this._duplicateFinder.CreateEntity(values, typeof (INRelatedInventory.effectiveDate), typeof (INRelatedInventory.expirationDate));
      INRelatedInventory relatedInventory = this._duplicateFinder[row].FirstOrDefault<INRelatedInventory>((Func<INRelatedInventory, bool>) (x => this.HaveIntersection(x, row)));
      if (relatedInventory != null)
      {
        if (keys.Contains((object) "LineID"))
          keys[(object) "LineID"] = (object) relatedInventory.LineID;
        else
          keys.Add((object) "LineID", (object) relatedInventory.LineID);
      }
    }
    return true;
  }

  public virtual bool RowImporting(string viewName, object row) => row == null;

  public virtual bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public virtual void ImportDone(PXImportAttribute.ImportMode.Value mode)
  {
    this._duplicateFinder = (MultiDuplicatesSearchEngine<INRelatedInventory>) null;
  }
}
