// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.GraphExtensions.ARPriceWorksheetMaintExt.ARPriceWorksheetAddItemLookupExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR.Repositories;
using PX.Objects.CS;
using PX.Objects.Extensions.AddItemLookup;
using PX.Objects.PM;
using PX.TM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.AR.GraphExtensions.ARPriceWorksheetMaintExt;

[PXProtectedAccess(typeof (ARPriceWorksheetMaint))]
public abstract class ARPriceWorksheetAddItemLookupExt : 
  AddItemLookupExt<ARPriceWorksheetMaint, ARPriceWorksheet, ARAddItemSelected, AddItemFilter, AddItemParameters>
{
  protected override IEnumerable AddSelectedItemsHandler(PXAdapter adapter)
  {
    if (((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.PriceType != "B" && ((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.PriceCode != null || ((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.PriceType == "B" && ((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.PriceCode == null)
    {
      foreach (ARAddItemSelected line in ((PXSelectBase) this.ItemInfo).Cache.Cached)
      {
        if (line.Selected.GetValueOrDefault())
          ((PXSelectBase<ARPriceWorksheetDetail>) this.Base.Details).Update(this.CreateWorksheetDetailOnAddSelItems(line));
      }
      ((PXSelectBase) this.ItemFilter).Cache.Clear();
      ((PXSelectBase) this.ItemInfo).Cache.Clear();
      ((PXSelectBase) this.addItemParameters).Cache.Clear();
    }
    else if (string.IsNullOrEmpty(((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.PriceCode))
      ((PXSelectBase) this.addItemParameters).Cache.RaiseExceptionHandling<AddItemParameters.priceCode>((object) ((PXSelectBase<AddItemParameters>) this.addItemParameters).Current, (object) ((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.PriceCode, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
      {
        (object) "[priceCode]"
      }));
    return adapter.Get();
  }

  protected override IEnumerable AddAllItemsHandler(PXAdapter adapter)
  {
    if (((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.PriceType != "B" && ((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.PriceCode != null || ((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.PriceType == "B" && ((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.PriceCode == null)
    {
      foreach (PXResult<ARAddItemSelected> pxResult in ((PXSelectBase<ARAddItemSelected>) this.ItemInfo).Select(Array.Empty<object>()))
      {
        ARAddItemSelected line = PXResult<ARAddItemSelected>.op_Implicit(pxResult);
        int? inventoryId = line.InventoryID;
        int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
        if (!(inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue))
          ((PXSelectBase<ARPriceWorksheetDetail>) this.Base.Details).Update(this.CreateWorksheetDetailOnAddSelItems(line));
      }
      ((PXSelectBase) this.ItemFilter).Cache.Clear();
      ((PXSelectBase) this.ItemInfo).Cache.Clear();
      ((PXSelectBase) this.addItemParameters).Cache.Clear();
    }
    else if (string.IsNullOrEmpty(((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.PriceCode))
      ((PXSelectBase) this.addItemParameters).Cache.RaiseExceptionHandling<AddItemParameters.priceCode>((object) ((PXSelectBase<AddItemParameters>) this.addItemParameters).Current, (object) ((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.PriceCode, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
      {
        (object) "[priceCode]"
      }));
    return adapter.Get();
  }

  protected virtual ARPriceWorksheetDetail CreateWorksheetDetailOnAddSelItems(ARAddItemSelected line)
  {
    string priceCode = ((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.PriceCode;
    if (((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.PriceType == "C")
    {
      PX.Objects.AR.Customer byCd = this.CustomerRepository.FindByCD(((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.PriceCode);
      if (byCd != null)
        priceCode = byCd.BAccountID.ToString();
    }
    ARPriceWorksheetDetail priceWorksheetDetail = new ARPriceWorksheetDetail();
    priceWorksheetDetail.InventoryID = line.InventoryID;
    priceWorksheetDetail.SiteID = ((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.SiteID;
    priceWorksheetDetail.CuryID = ((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.CuryID;
    priceWorksheetDetail.UOM = line.BaseUnit;
    priceWorksheetDetail.PriceType = ((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.PriceType;
    bool? nullable = ((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.SkipLineDiscounts;
    int num;
    if (nullable.GetValueOrDefault())
    {
      nullable = ((PXSelectBase<ARPriceWorksheet>) this.Base.Document).Current.IsFairValue;
      num = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    priceWorksheetDetail.SkipLineDiscounts = new bool?(num != 0);
    ARPriceWorksheetDetail detailOnAddSelItems = priceWorksheetDetail;
    detailOnAddSelItems.CurrentPrice = new Decimal?(this.GetItemPrice((PXGraph) this.Base, ((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.PriceType, priceCode, detailOnAddSelItems.InventoryID, detailOnAddSelItems.CuryID, ((PXSelectBase<ARPriceWorksheet>) this.Base.Document).Current.EffectiveDate));
    if (((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.PriceType == "C")
      detailOnAddSelItems.CustomerID = new int?(Convert.ToInt32(priceCode));
    else
      detailOnAddSelItems.CustPriceClassID = priceCode;
    detailOnAddSelItems.PriceCode = ((PXSelectBase<AddItemParameters>) this.addItemParameters).Current.PriceCode;
    return detailOnAddSelItems;
  }

  protected virtual void _(PX.Data.Events.RowSelected<ARPriceWorksheet> e)
  {
    ((PXAction) this.showItems).SetEnabled(e.Row.Hold.GetValueOrDefault() && e.Row.Status == "H");
  }

  protected override void _(PX.Data.Events.RowSelected<AddItemFilter> e)
  {
    base._(e);
    PXUIFieldAttribute.SetVisible<ARAddItemSelected.curyID>((PXCache) GraphHelper.Caches<ARAddItemSelected>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AddItemFilter>>) e).Cache.Graph), (object) null, true);
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AddItemFilter>>) e).Cache;
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
    PXCache cache2 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AddItemFilter>>) e).Cache;
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

  protected override Type CreateAdditionalWhere()
  {
    return typeof (Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<AddItemFilter.ownerID>, IsNull>>>>.Or<BqlOperand<Current<AddItemFilter.ownerID>, IBqlInt>.IsEqual<ARAddItemSelected.priceManagerID>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<AddItemFilter.myWorkGroup>, Equal<False>>>>>.Or<BqlOperand<ARAddItemSelected.priceWorkgroupID, IBqlInt>.Is<IsWorkgroupOfContact<BqlField<AddItemFilter.currentOwnerID, IBqlInt>.FromCurrent.Value>>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<AddItemFilter.workGroupID>, IsNull>>>>.Or<BqlOperand<Current<AddItemFilter.workGroupID>, IBqlInt>.IsEqual<ARAddItemSelected.priceWorkgroupID>>>>);
  }

  protected virtual void _(PX.Data.Events.RowSelected<AddItemParameters> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<AddItemParameters.curyID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AddItemParameters>>) e).Cache, (object) e.Row, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    PXUIFieldAttribute.SetEnabled<AddItemParameters.priceCode>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AddItemParameters>>) e).Cache, (object) e.Row, e.Row.PriceType != "B");
  }

  protected virtual void _(PX.Data.Events.RowUpdated<AddItemParameters> e)
  {
    if (e.Row == null)
      return;
    if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<AddItemParameters>>) e).Cache.ObjectsEqual<AddItemParameters.priceType>((object) e.Row, (object) e.OldRow))
      e.Row.PriceCode = (string) null;
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<AddItemParameters>>) e).Cache.ObjectsEqual<AddItemParameters.priceCode>((object) e.Row, (object) e.OldRow) || !(e.Row.PriceType == "C"))
      return;
    PXResult<PX.Objects.AR.Customer> pxResult = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXViewOf<PX.Objects.AR.Customer>.BasedOn<SelectFromBase<PX.Objects.AR.Customer, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.AR.Customer.acctCD, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) e.Row.PriceCode
    }));
    if (pxResult == null)
      return;
    if (PXResult<PX.Objects.AR.Customer>.op_Implicit(pxResult).CuryID != null)
      e.Row.CuryID = PXResult<PX.Objects.AR.Customer>.op_Implicit(pxResult).CuryID;
    else
      ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<AddItemParameters>>) e).Cache.SetDefaultExt<AddItemParameters.curyID>((object) e.Row);
  }

  [PXProtectedAccess(null)]
  protected abstract CustomerRepository CustomerRepository { get; }

  [PXProtectedAccess(null)]
  protected abstract Decimal GetItemPrice(
    PXGraph graph,
    string priceType,
    string priceCode,
    int? inventoryID,
    string toCuryID,
    DateTime? curyEffectiveDate);
}
