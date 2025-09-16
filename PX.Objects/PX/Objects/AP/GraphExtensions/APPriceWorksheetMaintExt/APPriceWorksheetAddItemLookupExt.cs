// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.GraphExtensions.APPriceWorksheetMaintExt.APPriceWorksheetAddItemLookupExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Extensions.AddItemLookup;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP.GraphExtensions.APPriceWorksheetMaintExt;

public class APPriceWorksheetAddItemLookupExt : 
  AddItemLookupExt<APPriceWorksheetMaint, APPriceWorksheet, APAddItemSelected, AddItemFilter, AddItemParameters>
{
  protected override IEnumerable AddSelectedItemsHandler(PXAdapter adapter)
  {
    if (!this.addItemParameters.Current.VendorID.HasValue)
    {
      this.addItemParameters.Cache.RaiseExceptionHandling<AddItemParameters.vendorID>((object) this.addItemParameters.Current, (object) this.addItemParameters.Current.VendorID, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", PXErrorLevel.Error, new object[1]
      {
        (object) "[vendorID]"
      }));
      return adapter.Get();
    }
    foreach (APAddItemSelected line in this.ItemInfo.Cache.Cached)
    {
      if (line.Selected.GetValueOrDefault())
        this.InsertOrUpdateARAddItem(line);
    }
    this.ItemFilter.Cache.Clear();
    this.ItemInfo.Cache.Clear();
    this.addItemParameters.Cache.Clear();
    return adapter.Get();
  }

  protected override IEnumerable AddAllItemsHandler(PXAdapter adapter)
  {
    if (!this.addItemParameters.Current.VendorID.HasValue)
    {
      this.addItemParameters.Cache.RaiseExceptionHandling<AddItemParameters.vendorID>((object) this.addItemParameters.Current, (object) this.addItemParameters.Current.VendorID, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", PXErrorLevel.Error, new object[1]
      {
        (object) "[vendorID]"
      }));
      return adapter.Get();
    }
    foreach (PXResult<APAddItemSelected> line in this.ItemInfo.Select())
      this.InsertOrUpdateARAddItem((APAddItemSelected) line);
    this.ItemFilter.Cache.Clear();
    this.ItemInfo.Cache.Clear();
    this.addItemParameters.Cache.Clear();
    return adapter.Get();
  }

  protected virtual void InsertOrUpdateARAddItem(APAddItemSelected line)
  {
    List<PXResult<APVendorPrice>> list = PXSelectBase<APVendorPrice, PXViewOf<APVendorPrice>.BasedOn<SelectFromBase<APVendorPrice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APVendorPrice.vendorID, Equal<P.AsInt>>>>, PX.Data.And<BqlOperand<APVendorPrice.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>, PX.Data.And<BqlOperand<APVendorPrice.curyID, IBqlString>.IsEqual<P.AsString>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APVendorPrice.effectiveDate, LessEqual<P.AsDateTime>>>>, PX.Data.And<BqlOperand<APVendorPrice.expirationDate, IBqlDateTime>.IsNull>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APVendorPrice.effectiveDate, LessEqual<P.AsDateTime>>>>>.And<BqlOperand<APVendorPrice.expirationDate, IBqlDateTime>.IsGreater<P.AsDateTime>>>>>.Aggregate<To<GroupBy<APVendorPrice.vendorID, GroupBy<APVendorPrice.inventoryID, GroupBy<APVendorPrice.uOM, GroupBy<APVendorPrice.breakQty, GroupBy<APVendorPrice.curyID>>>>>>>>.Config>.SelectMultiBound((PXGraph) this.Base, (object[]) null, (object) this.addItemParameters.Current.VendorID, (object) line.InventoryID, (object) this.addItemParameters.Current.CuryID, (object) this.Base.Document.Current.EffectiveDate, (object) this.Base.Document.Current.EffectiveDate, (object) this.Base.Document.Current.EffectiveDate).ToList<PXResult<APVendorPrice>>();
    if (list.Count > 0)
      EnumerableExtensions.ForEach<APPriceWorksheetDetail>(list.Select<PXResult<APVendorPrice>, APPriceWorksheetDetail>((Func<PXResult<APVendorPrice>, APPriceWorksheetDetail>) (price => this.CreateWorksheetDetailFromVendorPriceOnAddSelItems((APVendorPrice) price))), (System.Action<APPriceWorksheetDetail>) (newWorksheetDetail => this.Base.Details.Update(newWorksheetDetail)));
    else
      this.Base.Details.Update(this.CreateWorksheetDetailWhenPriceNotFoundOnAddSelItems(line));
  }

  protected virtual APPriceWorksheetDetail CreateWorksheetDetailFromVendorPriceOnAddSelItems(
    APVendorPrice salesPrice)
  {
    return new APPriceWorksheetDetail()
    {
      VendorID = this.addItemParameters.Current.VendorID,
      InventoryID = salesPrice.InventoryID,
      SiteID = this.addItemParameters.Current.SiteID ?? salesPrice.SiteID,
      UOM = salesPrice.UOM,
      BreakQty = salesPrice.BreakQty,
      CurrentPrice = salesPrice.SalesPrice,
      CuryID = this.addItemParameters.Current.CuryID
    };
  }

  protected virtual APPriceWorksheetDetail CreateWorksheetDetailWhenPriceNotFoundOnAddSelItems(
    APAddItemSelected line)
  {
    return new APPriceWorksheetDetail()
    {
      InventoryID = line.InventoryID,
      SiteID = this.addItemParameters.Current.SiteID,
      CuryID = this.addItemParameters.Current.CuryID,
      UOM = line.BaseUnit,
      VendorID = this.addItemParameters.Current.VendorID,
      CurrentPrice = new Decimal?(0M)
    };
  }

  protected override void _(PX.Data.Events.RowSelected<AddItemFilter> e)
  {
    base._(e);
    PXUIFieldAttribute.SetVisible<APAddItemSelected.curyID>((PXCache) e.Cache.Graph.Caches<APAddItemSelected>(), (object) null, true);
    PXCache cache1 = e.Cache;
    AddItemFilter row1 = e.Row;
    AddItemFilter row2 = e.Row;
    bool? nullable;
    int num1;
    if (row2 == null)
    {
      num1 = 1;
    }
    else
    {
      nullable = row2.MyWorkGroup;
      num1 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<AddItemFilter.workGroupID>(cache1, (object) row1, num1 != 0);
    PXCache cache2 = e.Cache;
    AddItemFilter row3 = e.Row;
    AddItemFilter row4 = e.Row;
    int num2;
    if (row4 == null)
    {
      num2 = 1;
    }
    else
    {
      nullable = row4.MyOwner;
      num2 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<AddItemFilter.ownerID>(cache2, (object) row3, num2 != 0);
  }

  protected override System.Type CreateAdditionalWhere()
  {
    return typeof (PX.Data.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<AddItemFilter.ownerID>, PX.Data.IsNull>>>>.Or<BqlOperand<Current<AddItemFilter.ownerID>, IBqlInt>.IsEqual<APAddItemSelected.productManagerID>>>>, PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<AddItemFilter.myWorkGroup>, Equal<False>>>>>.Or<BqlOperand<APAddItemSelected.productWorkgroupID, IBqlInt>.Is<IsWorkgroupOfContact<BqlField<AddItemFilter.currentOwnerID, IBqlInt>.FromCurrent.Value>>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<AddItemFilter.workGroupID>, PX.Data.IsNull>>>>.Or<BqlOperand<Current<AddItemFilter.workGroupID>, IBqlInt>.IsEqual<APAddItemSelected.productWorkgroupID>>>>);
  }

  protected virtual void _(PX.Data.Events.RowSelected<APPriceWorksheet> e)
  {
    if (e.Row == null)
      return;
    bool flag = e.Row.Status == "H" || e.Row.Status == "N";
    this.showItems.SetEnabled(e.Row.Hold.GetValueOrDefault() & flag);
  }

  protected virtual void _(PX.Data.Events.RowSelected<AddItemParameters> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<AddItemParameters.curyID>(e.Cache, (object) e.Row, PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>());
  }

  protected virtual void _(PX.Data.Events.RowUpdated<AddItemParameters> e)
  {
    if (e.Row == null)
      return;
    int? vendorId1 = e.Row.VendorID;
    int? vendorId2 = e.OldRow.VendorID;
    if (vendorId1.GetValueOrDefault() == vendorId2.GetValueOrDefault() & vendorId1.HasValue == vendorId2.HasValue)
      return;
    Vendor vendor = (Vendor) PXSelectBase<Vendor, PXViewOf<Vendor>.BasedOn<SelectFromBase<Vendor, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Vendor.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this.Base, (object) e.Row.VendorID);
    if (vendor == null)
      return;
    if (vendor.CuryID != null)
      e.Row.CuryID = vendor.CuryID;
    else
      e.Cache.SetDefaultExt<AddItemParameters.curyID>((object) e.Row);
  }
}
