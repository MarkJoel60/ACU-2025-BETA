// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POItemCostManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CM.TemporaryHelpers;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.PO;

[Serializable]
public static class POItemCostManager
{
  private static 
  #nullable disable
  POItemCostManager.ItemCost FetchStdCost(
    PXGraph graph,
    PX.Objects.IN.InventoryItem item,
    string baseCuryID,
    DateTime? docDate)
  {
    bool? stkItem = item.StkItem;
    bool flag = false;
    if (stkItem.GetValueOrDefault() == flag & stkItem.HasValue || item.ValMethod == "T")
    {
      InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find(graph, item.InventoryID, baseCuryID ?? graph.Accessinfo.BaseCuryID);
      if (itemCurySettings == null)
        return (POItemCostManager.ItemCost) null;
      if (!docDate.HasValue || itemCurySettings.StdCostDate.HasValue && itemCurySettings.StdCostDate.Value <= docDate.Value)
        return new POItemCostManager.ItemCost(item, itemCurySettings.StdCost.Value);
    }
    return (POItemCostManager.ItemCost) null;
  }

  private static POItemCostManager.ItemCost FetchSiteLastCost(
    PXGraph graph,
    PX.Objects.IN.InventoryItem item,
    int? siteID,
    string baseCuryID)
  {
    INItemStats inItemStats;
    if (siteID.HasValue)
      inItemStats = PXResultset<INItemStats>.op_Implicit(PXSelectBase<INItemStats, PXSelect<INItemStats, Where<INItemStats.inventoryID, Equal<Required<INItemStats.inventoryID>>, And<INItemStats.siteID, Equal<Required<INItemStats.siteID>>>>>.Config>.Select(graph, new object[2]
      {
        (object) item.InventoryID,
        (object) siteID
      }));
    else
      inItemStats = PXResultset<INItemStats>.op_Implicit(PXSelectBase<INItemStats, PXSelectJoin<INItemStats, InnerJoin<INSite, On<INItemStats.FK.Site>>, Where<INItemStats.inventoryID, Equal<Required<INItemStats.inventoryID>>, And<INSite.baseCuryID, Equal<Required<INSite.baseCuryID>>>>, OrderBy<Desc<INItemStats.lastCostDate>>>.Config>.Select(graph, new object[2]
      {
        (object) item.InventoryID,
        (object) baseCuryID
      }));
    if (inItemStats != null)
    {
      Decimal? lastCost = inItemStats.LastCost;
      if (lastCost.HasValue)
      {
        PX.Objects.IN.InventoryItem inventoryItem = item;
        lastCost = inItemStats.LastCost;
        Decimal cost = lastCost.Value;
        return new POItemCostManager.ItemCost(inventoryItem, cost);
      }
    }
    return (POItemCostManager.ItemCost) null;
  }

  public static int? FetchLocation(
    PXGraph graph,
    int? vendorID,
    int? itemID,
    int? subItemID,
    int? siteID)
  {
    BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXSelectJoin<BAccountR, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.bAccountID>>>, Where<BAccountR.bAccountID, Equal<Required<BAccountR.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) vendorID
    }));
    if (baccountR != null)
      return baccountR.DefLocationID;
    PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelectReadonly<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) vendorID
    }));
    var data = ((IEnumerable<PXResult<INItemSiteSettings>>) ((IEnumerable<PXResult<INItemSiteSettings>>) PXSelectBase<INItemSiteSettings, PXSelectJoin<INItemSiteSettings, LeftJoin<POVendorInventory, On<POVendorInventory.inventoryID, Equal<INItemSiteSettings.inventoryID>, And<POVendorInventory.active, Equal<boolTrue>, And<POVendorInventory.vendorID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>, And<Where<POVendorInventory.subItemID, Equal<Required<POVendorInventory.subItemID>>, Or<POVendorInventory.subItemID, Equal<INItemSiteSettings.defaultSubItemID>, Or<POVendorInventory.subItemID, IsNull, Or<Where<Required<POVendorInventory.subItemID>, IsNull, And<POVendorInventory.subItemID, Equal<True>>>>>>>>>>>>, Where<INItemSiteSettings.inventoryID, Equal<Required<INItemSiteSettings.inventoryID>>, And<INItemSiteSettings.siteID, Equal<Required<INItemSiteSettings.siteID>>>>>.Config>.Select(graph, new object[5]
    {
      (object) vendorID,
      (object) subItemID,
      (object) subItemID,
      (object) itemID,
      (object) siteID
    })).ToArray<PXResult<INItemSiteSettings>>()).Select(r => new
    {
      Item = ((PXResult) r).GetItem<POVendorInventory>(),
      Site = ((PXResult) r).GetItem<INItemSiteSettings>()
    }).Where(r => r.Item != null && r.Site != null).OrderBy(r => r.Item.LastPrice).ThenByDescending(r =>
    {
      int? subItemId = r.Item.SubItemID;
      int? defaultSubItemId = r.Site.DefaultSubItemID;
      return subItemId.GetValueOrDefault() == defaultSubItemId.GetValueOrDefault() & subItemId.HasValue == defaultSubItemId.HasValue;
    }).ThenByDescending(r => r.Item.VendorLocationID.HasValue).ThenByDescending(r => r.Item.IsDefault.GetValueOrDefault()).ThenByDescending(r =>
    {
      int? vendorLocationId = r.Item.VendorLocationID;
      int? defLocationId = (int?) vendor?.DefLocationID;
      return vendorLocationId.GetValueOrDefault() == defLocationId.GetValueOrDefault() & vendorLocationId.HasValue == defLocationId.HasValue;
    }).FirstOrDefault();
    if (data == null)
      return new int?();
    int? nullable1 = data.Item.VendorLocationID;
    if (nullable1.HasValue)
      return data.Item.VendorLocationID;
    nullable1 = data.Site.PreferredVendorID;
    int? nullable2 = vendorID;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
    {
      int? vendorLocationId = data.Site.PreferredVendorLocationID;
      if (vendorLocationId.HasValue)
        return vendorLocationId;
      PX.Objects.AP.Vendor vendor1 = vendor;
      if (vendor1 != null)
        return vendor1.DefLocationID;
      nullable1 = new int?();
      return nullable1;
    }
    if (vendor != null)
    {
      int? baccountId = vendor.BAccountID;
      nullable1 = vendorID;
      if (baccountId.GetValueOrDefault() == nullable1.GetValueOrDefault() & baccountId.HasValue == nullable1.HasValue)
        return vendor.DefLocationID;
    }
    nullable1 = new int?();
    return nullable1;
  }

  public static PX.Objects.CM.Extensions.CurrencyInfo FetchCuryInfo<CuryInfoIDField>(
    PXGraph graph,
    object row)
    where CuryInfoIDField : IBqlField
  {
    PXCache cach = graph.Caches[row.GetType()];
    if (cach.GetValue<CuryInfoIDField>(row) == null)
      cach.SetDefaultExt<CuryInfoIDField>(row);
    return MultiCurrencyCalculator.GetCurrencyInfo<CuryInfoIDField>(graph, row);
  }

  public static Decimal? Fetch<InventoryIDField, CuryInfoIDField>(
    PXGraph graph,
    object row,
    int? vendorID,
    int? vendorLocationID,
    DateTime? docDate,
    string curyID,
    int? inventoryID,
    int? subItemID,
    int? siteID,
    string uom,
    bool onlyVendor = false)
    where InventoryIDField : IBqlField
    where CuryInfoIDField : IBqlField
  {
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = POItemCostManager.FetchCuryInfo<CuryInfoIDField>(graph, row);
    return new Decimal?(POItemCostManager.Fetch(graph, vendorID, vendorLocationID, docDate, curyID, currencyInfo?.BaseCuryID, inventoryID, subItemID, siteID, uom, onlyVendor).Convert<InventoryIDField>(graph, row, currencyInfo, uom));
  }

  public static POItemCostManager.ItemCost Fetch(
    PXGraph graph,
    int? vendorID,
    int? vendorLocationID,
    DateTime? docDate,
    string curyID,
    string baseCuryID,
    int? inventoryID,
    int? subItemID,
    int? siteID,
    string uom,
    bool onlyVendor = false)
  {
    PXSelectBase<PX.Objects.IN.InventoryItem> vendorCostSelect = (PXSelectBase<PX.Objects.IN.InventoryItem>) new PXSelectReadonly2<PX.Objects.IN.InventoryItem, LeftJoin<INItemCost, On<INItemCost.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<INItemCost.curyID, Equal<Required<INItemCost.curyID>>>>, LeftJoin<POVendorInventory, On<POVendorInventory.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<POVendorInventory.active, Equal<True>, And<POVendorInventory.vendorID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>, And<POVendorInventory.curyID, Equal<Required<POVendorInventory.curyID>>, And2<Where<POVendorInventory.subItemID, Equal<Required<POVendorInventory.subItemID>>, Or<POVendorInventory.subItemID, Equal<PX.Objects.IN.InventoryItem.defaultSubItemID>, Or<POVendorInventory.subItemID, IsNull, Or<Where<Required<POVendorInventory.subItemID>, IsNull, And<POVendorInventory.subItemID, Equal<True>>>>>>>, And2<Where<POVendorInventory.purchaseUnit, Equal<Required<POVendorInventory.purchaseUnit>>>, And<Where<POVendorInventory.vendorLocationID, Equal<Required<POVendorInventory.vendorLocationID>>, Or<POVendorInventory.vendorLocationID, IsNull>>>>>>>>>>>, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>, OrderBy<Asc<Switch<Case<Where<POVendorInventory.purchaseUnit, Equal<PX.Objects.IN.InventoryItem.purchaseUnit>>, True>, False>, Asc<Switch<Case<Where<POVendorInventory.subItemID, Equal<PX.Objects.IN.InventoryItem.defaultSubItemID>>, True>, False>, Asc<Switch<Case<Where<POVendorInventory.vendorLocationID, IsNull>, True>, False>, Asc<PX.Objects.IN.InventoryItem.inventoryCD>>>>>>(graph);
    Func<string, PXResult<PX.Objects.IN.InventoryItem, INItemCost, POVendorInventory>> func1 = (Func<string, PXResult<PX.Objects.IN.InventoryItem, INItemCost, POVendorInventory>>) (uomParam => ((IEnumerable<PXResult<PX.Objects.IN.InventoryItem>>) vendorCostSelect.Select(new object[8]
    {
      (object) baseCuryID,
      (object) vendorID,
      (object) curyID,
      (object) subItemID,
      (object) subItemID,
      (object) uomParam,
      (object) vendorLocationID,
      (object) inventoryID
    })).AsEnumerable<PXResult<PX.Objects.IN.InventoryItem>>().FirstOrDefault<PXResult<PX.Objects.IN.InventoryItem>>((Func<PXResult<PX.Objects.IN.InventoryItem>, bool>) (r => ((PXResult) r).GetItem<POVendorInventory>() != null)) as PXResult<PX.Objects.IN.InventoryItem, INItemCost, POVendorInventory>);
    PXResult<PX.Objects.IN.InventoryItem, INItemCost, POVendorInventory> pxResult = func1(uom);
    PX.Objects.IN.InventoryItem item = ((PXResult) pxResult).GetItem<PX.Objects.IN.InventoryItem>();
    Func<POVendorInventory, POItemCostManager.ItemCost> func2 = (Func<POVendorInventory, POItemCostManager.ItemCost>) (vendorPrice =>
    {
      if (vendorPrice.LastPrice.HasValue)
      {
        Decimal? lastPrice = vendorPrice.LastPrice;
        Decimal num = 0M;
        if (!(lastPrice.GetValueOrDefault() == num & lastPrice.HasValue))
        {
          PX.Objects.IN.InventoryItem inventoryItem = item;
          string purchaseUnit = vendorPrice.PurchaseUnit;
          string curyID1 = curyID;
          lastPrice = vendorPrice.LastPrice;
          Decimal cost = lastPrice.Value;
          return new POItemCostManager.ItemCost(inventoryItem, purchaseUnit, curyID1, cost, false);
        }
      }
      return (POItemCostManager.ItemCost) null;
    });
    return func2(((PXResult) pxResult).GetItem<POVendorInventory>()) ?? func2(((PXResult) func1(item.BaseUnit)).GetItem<POVendorInventory>()) ?? (onlyVendor ? (POItemCostManager.ItemCost) null : POItemCostManager.FetchStdCost(graph, item, baseCuryID, docDate)) ?? (onlyVendor ? (POItemCostManager.ItemCost) null : POItemCostManager.FetchSiteLastCost(graph, item, siteID, baseCuryID)) ?? new POItemCostManager.ItemCost(item, ((Decimal?) ((PXResult) pxResult).GetItem<INItemCost>()?.LastCost).GetValueOrDefault());
  }

  public static void Update(
    PXGraph graph,
    int? vendorID,
    int? vendorLocationID,
    string curyID,
    int? inventoryID,
    int? subItemID,
    string uom,
    Decimal curyCost)
  {
    APSetup apSetup = PXResultset<APSetup>.op_Implicit(PXSelectBase<APSetup, PXSelectReadonly<APSetup>.Config>.Select(graph, Array.Empty<object>()));
    if (curyCost <= 0M || string.IsNullOrEmpty(uom) || !vendorID.HasValue || !vendorLocationID.HasValue || apSetup?.VendorPriceUpdate == "N")
      return;
    PXCache cach = graph.Caches[typeof (POItemCostManager.POVendorInventoryPriceUpdate)];
    foreach (PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AP.Vendor, Company> pxResult in PXSelectBase<PX.Objects.IN.InventoryItem, PXSelectReadonly2<PX.Objects.IN.InventoryItem, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>, CrossJoin<Company>>, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select(graph, new object[2]
    {
      (object) vendorID,
      (object) inventoryID
    }))
    {
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AP.Vendor, Company>.op_Implicit(pxResult);
      PX.Objects.AP.Vendor vendor = PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AP.Vendor, Company>.op_Implicit(pxResult);
      PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AP.Vendor, Company>.op_Implicit(pxResult);
      int? nullable1 = inventoryItem.InventoryID;
      if (nullable1.HasValue)
      {
        nullable1 = vendor.BAccountID;
        if (nullable1.HasValue)
        {
          bool? stkItem = inventoryItem.StkItem;
          if (!stkItem.GetValueOrDefault() || subItemID.HasValue)
          {
            PXResultset<INSetup>.op_Implicit(PXSelectBase<INSetup, PXSelectReadonly<INSetup>.Config>.Select(graph, Array.Empty<object>()));
            stkItem = inventoryItem.StkItem;
            int? nullable2;
            if (!stkItem.GetValueOrDefault())
            {
              nullable1 = new int?();
              nullable2 = nullable1;
            }
            else
              nullable2 = subItemID;
            int? nullable3 = nullable2;
            POItemCostManager.POVendorInventoryPriceUpdate instance = (POItemCostManager.POVendorInventoryPriceUpdate) cach.CreateInstance();
            instance.InventoryID = inventoryID;
            instance.SubItemID = nullable3;
            instance.VendorID = vendorID;
            instance.VendorLocationID = vendorLocationID;
            instance.PurchaseUnit = uom;
            POItemCostManager.POVendorInventoryPriceUpdate inventoryPriceUpdate = (POItemCostManager.POVendorInventoryPriceUpdate) cach.Insert((object) instance);
            stkItem = inventoryItem.StkItem;
            if (!stkItem.GetValueOrDefault())
              inventoryPriceUpdate.SubItemID = nullable3;
            inventoryPriceUpdate.CuryID = curyID;
            cach.Normalize();
            inventoryPriceUpdate.Active = new bool?(true);
            inventoryPriceUpdate.LastPrice = new Decimal?(curyCost);
          }
        }
      }
    }
  }

  public static Decimal ConvertUOM(
    PXGraph graph,
    PX.Objects.IN.InventoryItem item,
    string uom,
    Decimal cost,
    string destinationUOM)
  {
    if (item == null)
      return 0M;
    if (destinationUOM == uom)
      return cost;
    PXCache cach = graph.Caches[typeof (PX.Objects.IN.InventoryItem)];
    POItemCostManager.ConvertUOM(cach, item.InventoryID, uom, item.BaseUnit, true, ref cost);
    POItemCostManager.ConvertUOM(cach, item.InventoryID, item.BaseUnit, destinationUOM, false, ref cost);
    return cost;
  }

  private static void ConvertUOM(
    PXCache cache,
    int? inventoryID,
    string sourceUom,
    string destinationUom,
    bool viceVersa,
    ref Decimal cost)
  {
    if (!(sourceUom != destinationUom) || !(cost != 0M))
      return;
    cost = INUnitAttribute.Convert(cache, INUnit.UK.ByInventory.FindDirty(cache.Graph, inventoryID, viceVersa ? sourceUom : destinationUom) ?? throw new PXUnitConversionException(), cost, INPrecision.UNITCOST, viceVersa);
  }

  public class ItemCost
  {
    private readonly string uom;
    public readonly PX.Objects.IN.InventoryItem Item;
    public readonly string CuryID;
    public readonly Decimal Cost;
    public readonly Decimal BaseCost;
    public readonly bool ConvertCury;

    public ItemCost(PX.Objects.IN.InventoryItem item, Decimal cost)
      : this(item, (string) null, (string) null, cost, cost, true)
    {
    }

    public ItemCost(
      PX.Objects.IN.InventoryItem item,
      string uom,
      string curyID,
      Decimal cost,
      bool convertCury)
      : this(item, uom, curyID, cost, cost, convertCury)
    {
    }

    public ItemCost(
      PX.Objects.IN.InventoryItem item,
      string uom,
      string curyID,
      Decimal cost,
      Decimal baseCost,
      bool convertCury)
    {
      this.Item = item;
      this.uom = uom;
      this.CuryID = curyID;
      this.Cost = cost;
      this.BaseCost = baseCost;
      this.ConvertCury = convertCury;
    }

    public string UOM => this.Item == null ? (string) null : this.uom ?? this.Item.BaseUnit;

    public Decimal Convert<InventoryIDField>(
      PXGraph graph,
      object inventoryRow,
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo,
      string uom)
      where InventoryIDField : IBqlField
    {
      POItemCostManager.ItemCost itemCost = this;
      if (itemCost == null || itemCost.Cost == 0M || itemCost.Item == null || inventoryRow == null)
        return 0M;
      Decimal num1 = !this.ConvertCury || currencyInfo == null ? itemCost.BaseCost : currencyInfo.CuryConvCuryRaw(itemCost.BaseCost);
      if (itemCost.UOM != uom && !string.IsNullOrEmpty(uom))
      {
        if (inventoryRow == null)
          return 0M;
        PXCache cach = graph.Caches[inventoryRow.GetType()];
        Decimal num2 = itemCost.UOM != itemCost.Item.BaseUnit ? INUnitAttribute.ConvertFromBase<InventoryIDField>(cach, inventoryRow, itemCost.UOM, num1, INPrecision.UNITCOST) : num1;
        num1 = uom != itemCost.Item.BaseUnit ? INUnitAttribute.ConvertToBase<InventoryIDField>(cach, inventoryRow, uom, num2, INPrecision.UNITCOST) : num2;
      }
      return num1;
    }
  }

  [POItemCostManager.POVendorInventoryAccumulator]
  [Serializable]
  public class POVendorInventoryPriceUpdate : POVendorInventory
  {
    [PXDBIdentity]
    public override int? RecordID
    {
      get => this._RecordID;
      set => this._RecordID = value;
    }

    [VendorNonEmployeeActiveOrHoldPayments]
    public override int? VendorID
    {
      get => this._VendorID;
      set => this._VendorID = value;
    }

    [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POItemCostManager.POVendorInventoryPriceUpdate.vendorID>>>))]
    public override int? VendorLocationID
    {
      get => this._VendorLocationID;
      set => this._VendorLocationID = value;
    }

    [AnyInventory(Filterable = true, DirtyRead = true, IsKey = true)]
    public override int? InventoryID
    {
      get => this._InventoryID;
      set => this._InventoryID = value;
    }

    [SubItem(typeof (POItemCostManager.POVendorInventoryPriceUpdate.inventoryID), DisplayName = "Subitem", BqlField = typeof (POVendorInventory.subItemID), IsKey = true)]
    public override int? SubItemID
    {
      get => this._SubItemID;
      set => this._SubItemID = value;
    }

    [INUnit(typeof (POVendorInventory.inventoryID))]
    public override string PurchaseUnit
    {
      get => this._PurchaseUnit;
      set => this._PurchaseUnit = value;
    }

    [PXDBPriceCost(BqlField = typeof (POVendorInventory.lastPrice))]
    [PXUIField(DisplayName = "Last Vendor Price", Enabled = false)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public override Decimal? LastPrice
    {
      get => this._LastPrice;
      set => this._LastPrice = value;
    }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Active")]
    public override bool? Active
    {
      get => this._Active;
      set => this._Active = value;
    }

    public override byte[] tstamp
    {
      get => this._tstamp;
      set => this._tstamp = value;
    }

    public new abstract class recordID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POItemCostManager.POVendorInventoryPriceUpdate.recordID>
    {
    }

    public new abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POItemCostManager.POVendorInventoryPriceUpdate.vendorID>
    {
    }

    public new abstract class vendorLocationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POItemCostManager.POVendorInventoryPriceUpdate.vendorLocationID>
    {
    }

    public new abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POItemCostManager.POVendorInventoryPriceUpdate.inventoryID>
    {
    }

    public new abstract class subItemID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POItemCostManager.POVendorInventoryPriceUpdate.subItemID>
    {
    }

    public new abstract class purchaseUnit : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POItemCostManager.POVendorInventoryPriceUpdate.purchaseUnit>
    {
    }

    public new abstract class lastPrice : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POItemCostManager.POVendorInventoryPriceUpdate.lastPrice>
    {
    }

    public new abstract class active : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POItemCostManager.POVendorInventoryPriceUpdate.active>
    {
    }

    public new abstract class Tstamp : 
      BqlType<
      #nullable enable
      IBqlByteArray, byte[]>.Field<
      #nullable disable
      POItemCostManager.POVendorInventoryPriceUpdate.Tstamp>
    {
    }
  }

  public class POVendorInventoryAccumulatorAttribute : PXAccumulatorAttribute
  {
    public POVendorInventoryAccumulatorAttribute() => this._SingleRecord = true;

    protected virtual bool PrepareInsert(
      PXCache sender,
      object row,
      PXAccumulatorCollection columns)
    {
      if (!base.PrepareInsert(sender, row, columns))
        return false;
      POItemCostManager.POVendorInventoryPriceUpdate inventoryPriceUpdate = (POItemCostManager.POVendorInventoryPriceUpdate) row;
      columns.Update<POVendorInventory.curyID>((object) inventoryPriceUpdate.CuryID, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<POItemCostManager.POVendorInventoryPriceUpdate.lastPrice>((object) inventoryPriceUpdate.LastPrice, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<POItemCostManager.POVendorInventoryPriceUpdate.active>((object) inventoryPriceUpdate.Active, (PXDataFieldAssign.AssignBehavior) 0);
      return true;
    }
  }
}
