// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.Intermediary.SkipLineDiscountsOptimizer`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.Common.Discount.Mappers;
using PX.Objects.Extensions.Discount;
using PX.Objects.Extensions.SalesPrice;
using System;

#nullable disable
namespace PX.Objects.Extensions.Intermediary;

public abstract class SkipLineDiscountsOptimizer<TPriceExt, TDiscountExt, TGraph, TPrimary> : 
  PXGraphExtension<TPriceExt, TDiscountExt, TGraph>
  where TPriceExt : SalesPriceGraph<TGraph, TPrimary>
  where TDiscountExt : DiscountGraph<TGraph, TPrimary>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  private ARSalesPriceMaint.SalesPriceItem _salesPriceItem;

  [PXOverride]
  public (ARSalesPriceMaint.SalesPriceItem, Decimal?) GetSalesPriceItemAndCalculatedPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    Decimal? quantity,
    System.DateTime date,
    Decimal? currentUnitPrice,
    string taxCalcMode,
    Func<PXCache, string, int?, int?, int?, PX.Objects.CM.CurrencyInfo, string, Decimal?, System.DateTime, Decimal?, string, (ARSalesPriceMaint.SalesPriceItem, Decimal?)> base_GetSalesPriceItemAndCalculatedPrice)
  {
    (ARSalesPriceMaint.SalesPriceItem, Decimal?) andCalculatedPrice = base_GetSalesPriceItemAndCalculatedPrice(sender, custPriceClass, customerID, inventoryID, siteID, currencyinfo, UOM, quantity, date, currentUnitPrice, taxCalcMode);
    this._salesPriceItem = andCalculatedPrice.Item1;
    return andCalculatedPrice;
  }

  /// Overrides <see cref="M:PX.Objects.Extensions.Discount.DiscountGraph`2.ProcessDiscountsOnDetailRowUpdated(PX.Objects.Extensions.Discount.Detail,PX.Objects.Extensions.Discount.Detail,PX.Data.PXCache)" />
  [PXOverride]
  public void ProcessDiscountsOnDetailRowUpdated(
    PX.Objects.Extensions.Discount.Detail row,
    PX.Objects.Extensions.Discount.Detail oldRow,
    PXCache cache,
    Action<PX.Objects.Extensions.Discount.Detail, PX.Objects.Extensions.Discount.Detail, PXCache> base_ProcessDiscountsOnDetailRowUpdated)
  {
    if (this._salesPriceItem != null)
      DiscountLineFields.GetMapFor<PX.Objects.Extensions.Discount.Detail>(row, cache).SkipLineDiscounts = new bool?(this._salesPriceItem.SkipLineDiscounts);
    base_ProcessDiscountsOnDetailRowUpdated(row, oldRow, cache);
  }
}
