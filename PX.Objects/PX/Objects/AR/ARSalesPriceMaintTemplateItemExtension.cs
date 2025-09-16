// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSalesPriceMaintTemplateItemExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.AR;

public class ARSalesPriceMaintTemplateItemExtension : PXGraphExtension<ARSalesPriceMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.matrixItem>();

  public static int? GetTemplateInventoryID(PXCache sender, int? inventoryID)
  {
    return PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, inventoryID)?.TemplateItemID;
  }

  [PXOverride]
  public virtual ARSalesPriceMaint.SalesPriceItem FindSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    string lotSerialNbr,
    int? siteID,
    string baseCuryID,
    string curyID,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool isFairValue,
    string taxCalcMode,
    ARSalesPriceMaintTemplateItemExtension.FindSalesPriceOrig baseMethod)
  {
    ARSalesPriceMaint.SalesPriceItem salesPrice = this.Base.SelectCustomItemPrice(sender, custPriceClass, customerID, inventoryID, lotSerialNbr, siteID, baseCuryID, curyID, quantity, UOM, date, isFairValue, taxCalcMode);
    if (salesPrice != null)
      return salesPrice;
    if (!RecordExistsSlot<ARSalesPrice, ARSalesPrice.recordID, Where<True, Equal<True>>>.IsRowsExists())
      return !isFairValue ? this.Base.SelectDefaultItemPrice(sender, inventoryID, baseCuryID) : (ARSalesPriceMaint.SalesPriceItem) null;
    ARSalesPriceMaintTemplateItemExtension.SalesPriceSelectWithTemplateItem withTemplateItem1 = new ARSalesPriceMaintTemplateItemExtension.SalesPriceSelectWithTemplateItem(sender, inventoryID, UOM, quantity.Value, isFairValue, taxCalcMode);
    withTemplateItem1.CustomerID = customerID;
    withTemplateItem1.CustPriceClass = custPriceClass;
    withTemplateItem1.CuryID = curyID;
    withTemplateItem1.SiteID = siteID;
    withTemplateItem1.Date = date;
    withTemplateItem1.TaxCalcMode = taxCalcMode;
    ARSalesPriceMaintTemplateItemExtension.SalesPriceForCurrentUOMWithTemplateItem withTemplateItem2 = withTemplateItem1.ForCurrentUOM();
    ARSalesPriceMaintTemplateItemExtension.SalesPriceForBaseUOMWithTemplateItem withTemplateItem3 = withTemplateItem1.ForBaseUOM();
    ARSalesPriceMaintTemplateItemExtension.SalesPriceForSalesUOMWithTemplateItem withTemplateItem4 = withTemplateItem1.ForSalesUOM();
    return withTemplateItem2.SelectCustomerPrice() ?? withTemplateItem3.SelectCustomerPrice() ?? withTemplateItem2.SelectBasePrice() ?? withTemplateItem3.SelectBasePrice() ?? (isFairValue ? (ARSalesPriceMaint.SalesPriceItem) null : this.Base.SelectDefaultItemPrice(sender, inventoryID, baseCuryID)) ?? (isFairValue ? (ARSalesPriceMaint.SalesPriceItem) null : this.Base.SelectDefaultItemPrice(sender, ARSalesPriceMaintTemplateItemExtension.GetTemplateInventoryID(sender, inventoryID), baseCuryID)) ?? withTemplateItem4.SelectCustomerPrice() ?? withTemplateItem4.SelectBasePrice();
  }

  public delegate ARSalesPriceMaint.SalesPriceItem FindSalesPriceOrig(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    string lotSerialNbr,
    int? siteID,
    string baseCuryID,
    string curyID,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool isFairValue,
    string taxCalcMode);

  internal class SalesPriceSelectWithTemplateItem : ARSalesPriceMaint.SalesPriceSelect
  {
    public SalesPriceSelectWithTemplateItem(
      PXCache cache,
      int? inventoryID,
      string uom,
      Decimal qty,
      bool isFairValue)
      : base(cache, inventoryID, uom, qty, isFairValue)
    {
    }

    public SalesPriceSelectWithTemplateItem(
      PXCache cache,
      int? inventoryID,
      string uom,
      Decimal qty,
      bool isFairValue,
      string taxCalcMode)
      : base(cache, inventoryID, uom, qty, isFairValue, taxCalcMode)
    {
    }

    public ARSalesPriceMaintTemplateItemExtension.SalesPriceForCurrentUOMWithTemplateItem ForCurrentUOM()
    {
      ARSalesPriceMaintTemplateItemExtension.SalesPriceForCurrentUOMWithTemplateItem withTemplateItem = new ARSalesPriceMaintTemplateItemExtension.SalesPriceForCurrentUOMWithTemplateItem(this.Cache, this.InventoryID, this.UOM, this.Qty);
      withTemplateItem.CustomerID = this.CustomerID;
      withTemplateItem.CustPriceClass = this.CustPriceClass;
      withTemplateItem.CuryID = this.CuryID;
      withTemplateItem.SiteID = this.SiteID;
      withTemplateItem.Date = this.Date;
      withTemplateItem.IsFairValue = this.IsFairValue;
      withTemplateItem.TaxCalcMode = this.TaxCalcMode;
      return withTemplateItem;
    }

    public ARSalesPriceMaintTemplateItemExtension.SalesPriceForBaseUOMWithTemplateItem ForBaseUOM()
    {
      ARSalesPriceMaintTemplateItemExtension.SalesPriceForBaseUOMWithTemplateItem withTemplateItem = new ARSalesPriceMaintTemplateItemExtension.SalesPriceForBaseUOMWithTemplateItem(this.Cache, this.InventoryID, this.UOM, this.Qty);
      withTemplateItem.CustomerID = this.CustomerID;
      withTemplateItem.CustPriceClass = this.CustPriceClass;
      withTemplateItem.CuryID = this.CuryID;
      withTemplateItem.SiteID = this.SiteID;
      withTemplateItem.Date = this.Date;
      withTemplateItem.IsFairValue = this.IsFairValue;
      withTemplateItem.TaxCalcMode = this.TaxCalcMode;
      return withTemplateItem;
    }

    public ARSalesPriceMaintTemplateItemExtension.SalesPriceForSalesUOMWithTemplateItem ForSalesUOM()
    {
      ARSalesPriceMaintTemplateItemExtension.SalesPriceForSalesUOMWithTemplateItem withTemplateItem = new ARSalesPriceMaintTemplateItemExtension.SalesPriceForSalesUOMWithTemplateItem(this.Cache, this.InventoryID, this.UOM, this.Qty);
      withTemplateItem.CustomerID = this.CustomerID;
      withTemplateItem.CustPriceClass = this.CustPriceClass;
      withTemplateItem.CuryID = this.CuryID;
      withTemplateItem.SiteID = this.SiteID;
      withTemplateItem.Date = this.Date;
      withTemplateItem.IsFairValue = this.IsFairValue;
      withTemplateItem.TaxCalcMode = this.TaxCalcMode;
      return withTemplateItem;
    }
  }

  internal class SalesPriceForCurrentUOMWithTemplateItem : ARSalesPriceMaint.SalesPriceForCurrentUOM
  {
    public SalesPriceForCurrentUOMWithTemplateItem(
      PXCache cache,
      int? inventoryID,
      string uom,
      Decimal qty)
      : base(cache, inventoryID, uom, qty)
    {
      this.SelectCommand.Join<InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ARSalesPrice.inventoryID>>>>();
      this.SelectCommand.OrderByNew<OrderBy<Asc<ARSalesPrice.priceType, Desc<ARSalesPrice.isPromotionalPrice, Desc<ARSalesPrice.siteID, Asc<PX.Objects.IN.InventoryItem.isTemplate, Desc<ARSalesPrice.breakQty>>>>>>>();
    }

    public override int?[] GetInventoryIDs(PXCache sender, int? inventoryID)
    {
      return new int?[2]
      {
        inventoryID,
        ARSalesPriceMaintTemplateItemExtension.GetTemplateInventoryID(sender, inventoryID)
      };
    }
  }

  internal class SalesPriceForBaseUOMWithTemplateItem : ARSalesPriceMaint.SalesPriceForBaseUOM
  {
    public SalesPriceForBaseUOMWithTemplateItem(
      PXCache cache,
      int? inventoryID,
      string uom,
      Decimal qty)
      : base(cache, inventoryID, uom, qty)
    {
      this.SelectCommand.OrderByNew<OrderBy<Asc<ARSalesPrice.priceType, Desc<ARSalesPrice.isPromotionalPrice, Desc<ARSalesPrice.siteID, Asc<PX.Objects.IN.InventoryItem.isTemplate, Desc<ARSalesPrice.breakQty>>>>>>>();
    }

    public override int?[] GetInventoryIDs(PXCache sender, int? inventoryID)
    {
      return new int?[2]
      {
        inventoryID,
        ARSalesPriceMaintTemplateItemExtension.GetTemplateInventoryID(sender, inventoryID)
      };
    }
  }

  internal class SalesPriceForSalesUOMWithTemplateItem : ARSalesPriceMaint.SalesPriceForSalesUOM
  {
    public SalesPriceForSalesUOMWithTemplateItem(
      PXCache cache,
      int? inventoryID,
      string uom,
      Decimal qty)
      : base(cache, inventoryID, uom, qty)
    {
      this.SelectCommand.OrderByNew<OrderBy<Asc<ARSalesPrice.priceType, Desc<ARSalesPrice.isPromotionalPrice, Desc<ARSalesPrice.siteID, Asc<PX.Objects.IN.InventoryItem.isTemplate, Desc<ARSalesPrice.breakQty>>>>>>>();
    }

    public override int?[] GetInventoryIDs(PXCache sender, int? inventoryID)
    {
      return new int?[2]
      {
        inventoryID,
        ARSalesPriceMaintTemplateItemExtension.GetTemplateInventoryID(sender, inventoryID)
      };
    }
  }
}
