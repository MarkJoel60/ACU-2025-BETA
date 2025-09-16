// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSPriceManagement
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class FSPriceManagement : PXGraph<FSPriceManagement>
{
  public string errorCode;

  public static FSPriceManagement SingleFSPriceManagement
  {
    get
    {
      return PXContext.GetSlot<FSPriceManagement>() ?? PXContext.SetSlot<FSPriceManagement>(PXGraph.CreateInstance<FSPriceManagement>());
    }
  }

  /// <summary>
  /// Determine the PriceCode value depending of PriceType of the calculated price.
  /// </summary>
  private static void DeterminePriceCode(PXCache cache, ref SalesPriceSet salesPriceSet)
  {
    if (salesPriceSet.PriceType == "CUSTM")
    {
      List<object> objectList = new List<object>();
      BqlCommand bqlCommand = (BqlCommand) new Select<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>();
      PXView pxView = new PXView(cache.Graph, true, bqlCommand);
      objectList.Add((object) salesPriceSet.CustomerID);
      object[] array = objectList.ToArray();
      PX.Objects.AR.Customer customer = (PX.Objects.AR.Customer) pxView.SelectSingle(array);
      salesPriceSet.PriceCode = customer.AcctCD;
    }
    else
    {
      if (!(salesPriceSet.PriceType == "BASEP") && !(salesPriceSet.PriceType == "DEFLT"))
        return;
      salesPriceSet.PriceCode = string.Empty;
    }
  }

  /// <summary>
  /// Calculates the price retrieving the correct price depending of the price set for that item.
  /// </summary>
  public static SalesPriceSet CalculateSalesPriceWithCustomerContract(
    PXCache cache,
    int? serviceContractID,
    int? billServiceContractID,
    int? billContractPeriodID,
    int? customerID,
    int? customerLocationID,
    bool? lineRelatedToContract,
    int? inventoryID,
    int? siteID,
    Decimal? quantity,
    string uom,
    DateTime date,
    Decimal? currentUnitPrice,
    bool alwaysFromBaseCurrency,
    PX.Objects.CM.CurrencyInfo currencyInfo,
    bool catchSalesPriceException)
  {
    return FSPriceManagement.CalculateSalesPriceWithCustomerContract(cache, serviceContractID, billServiceContractID, billContractPeriodID, customerID, customerLocationID, lineRelatedToContract, inventoryID, siteID, quantity, uom, date, currentUnitPrice, alwaysFromBaseCurrency, currencyInfo, catchSalesPriceException, "T");
  }

  /// <summary>
  /// Calculates the price retrieving the correct price depending of the price set for that item.
  /// </summary>
  public static SalesPriceSet CalculateSalesPriceWithCustomerContract(
    PXCache cache,
    int? serviceContractID,
    int? billServiceContractID,
    int? billContractPeriodID,
    int? customerID,
    int? customerLocationID,
    bool? lineRelatedToContract,
    int? inventoryID,
    int? siteID,
    Decimal? quantity,
    string uom,
    DateTime date,
    Decimal? currentUnitPrice,
    bool alwaysFromBaseCurrency,
    PX.Objects.CM.CurrencyInfo currencyInfo,
    bool catchSalesPriceException,
    string taxCalcMode)
  {
    date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
    if (currencyInfo == null)
    {
      currencyInfo = new PX.Objects.CM.CurrencyInfo();
      currencyInfo.BaseCuryID = cache.Graph.Accessinfo.BaseCuryID;
      currencyInfo.CuryID = cache.Graph.Accessinfo.BaseCuryID;
      currencyInfo.CuryRate = new Decimal?((Decimal) 1);
      currencyInfo.CuryMultDiv = "M";
      currencyInfo.CuryEffDate = cache.Graph.Accessinfo.BusinessDate;
    }
    Decimal? customerContractPrice = FSPriceManagement.GetCustomerContractPrice(cache, currencyInfo, serviceContractID, billServiceContractID, billContractPeriodID, lineRelatedToContract, inventoryID, uom);
    SalesPriceSet salesPriceSet;
    if (!customerContractPrice.HasValue)
    {
      salesPriceSet = FSPriceManagement.CalculateSalesPrice(cache, customerID, customerLocationID, inventoryID, siteID, currencyInfo, quantity, uom, date, currentUnitPrice, alwaysFromBaseCurrency, catchSalesPriceException, taxCalcMode);
      FSPriceManagement.DeterminePriceCode(cache, ref salesPriceSet);
    }
    else
      salesPriceSet = new SalesPriceSet(string.Empty, customerContractPrice, "CONTR", customerID, "OK");
    return salesPriceSet;
  }

  /// <summary>
  /// Gets the price for the item in the contract if it exists.
  /// </summary>
  private static Decimal? GetCustomerContractPrice(
    PXCache cache,
    int? serviceContractID,
    int? billServiceContractID,
    int? billContractPeriodID,
    bool? lineRelatedToContract,
    int? inventoryID,
    string uom)
  {
    if (!serviceContractID.HasValue && !billServiceContractID.HasValue)
      return new Decimal?();
    Decimal? customerContractPrice = new Decimal?();
    FSServiceContract fsServiceContract = FSServiceContract.PK.Find(cache.Graph, serviceContractID) ?? FSServiceContract.PK.Find(cache.Graph, billServiceContractID);
    if (fsServiceContract != null && fsServiceContract.BillingType == "APFB" && fsServiceContract.SourcePrice == "C")
    {
      FSSalesPrice fsSalesPrice = PXResultset<FSSalesPrice>.op_Implicit(PXSelectBase<FSSalesPrice, PXSelect<FSSalesPrice, Where<FSSalesPrice.serviceContractID, Equal<Required<FSSalesPrice.serviceContractID>>, And<FSSalesPrice.inventoryID, Equal<Required<FSSalesPrice.inventoryID>>, And<FSSalesPrice.uOM, Equal<Required<FSSalesPrice.uOM>>>>>>.Config>.Select(cache.Graph, new object[3]
      {
        (object) serviceContractID,
        (object) inventoryID,
        (object) uom
      }));
      if (fsSalesPrice != null)
        customerContractPrice = fsSalesPrice.UnitPrice;
    }
    else if (fsServiceContract != null && fsServiceContract.BillingType == "STDB" && lineRelatedToContract.GetValueOrDefault())
    {
      FSContractPeriodDet contractPeriodDet = PXResultset<FSContractPeriodDet>.op_Implicit(PXSelectBase<FSContractPeriodDet, PXSelect<FSContractPeriodDet, Where<FSContractPeriodDet.serviceContractID, Equal<Required<FSContractPeriodDet.serviceContractID>>, And<FSContractPeriodDet.contractPeriodID, Equal<Required<FSContractPeriodDet.contractPeriodID>>, And<FSContractPeriodDet.inventoryID, Equal<Required<FSContractPeriodDet.inventoryID>>, And<FSContractPeriodDet.uOM, Equal<Required<FSContractPeriodDet.uOM>>>>>>>.Config>.Select(cache.Graph, new object[4]
      {
        (object) billServiceContractID,
        (object) billContractPeriodID,
        (object) inventoryID,
        (object) uom
      }));
      if (contractPeriodDet != null)
        customerContractPrice = contractPeriodDet.RecurringUnitPrice;
    }
    return customerContractPrice;
  }

  private static Decimal? GetCustomerContractPrice(
    PXCache cache,
    PX.Objects.CM.CurrencyInfo info,
    int? serviceContractID,
    int? billServiceContractID,
    int? billContractPeriodID,
    bool? lineRelatedToContract,
    int? inventoryID,
    string uom)
  {
    Decimal? customerContractPrice = FSPriceManagement.GetCustomerContractPrice(cache, serviceContractID, billServiceContractID, billContractPeriodID, lineRelatedToContract, inventoryID, uom);
    if (!customerContractPrice.HasValue || info == null)
      return customerContractPrice;
    Decimal curyval;
    PXDBCurrencyAttribute.CuryConvCury(cache, (object) info, customerContractPrice.Value, out curyval, CommonSetupDecPl.PrcCst);
    return new Decimal?(curyval);
  }

  /// <summary>
  /// Calculates/Retrieves the price for an item depending on the price set for it.
  /// </summary>
  private static SalesPriceSet CalculateSalesPrice(
    PXCache cache,
    int? customerID,
    int? customerLocationID,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    Decimal? quantity,
    string uom,
    DateTime date,
    Decimal? currentUnitPrice,
    bool alwaysFromBaseCurrency,
    bool catchSalesPriceException)
  {
    return FSPriceManagement.CalculateSalesPrice(cache, customerID, customerLocationID, inventoryID, siteID, currencyinfo, quantity, uom, date, currentUnitPrice, alwaysFromBaseCurrency, catchSalesPriceException, "T");
  }

  /// <summary>
  /// Calculates/Retrieves the price for an item depending on the price set for it.
  /// </summary>
  private static SalesPriceSet CalculateSalesPrice(
    PXCache cache,
    int? customerID,
    int? customerLocationID,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    Decimal? quantity,
    string uom,
    DateTime date,
    Decimal? currentUnitPrice,
    bool alwaysFromBaseCurrency,
    bool catchSalesPriceException,
    string taxCalcMode)
  {
    string str = "BASE";
    string errorCode = "OK";
    string priceType = FSPriceManagement.CheckPriceByPriceType(cache, str, customerID, inventoryID, currencyinfo.BaseCuryID, alwaysFromBaseCurrency ? currencyinfo.BaseCuryID : currencyinfo.CuryID, new Decimal?(Math.Abs(quantity.GetValueOrDefault())), uom, date, "CUSTM", ref errorCode);
    if (priceType == null)
    {
      str = FSPriceManagement.SingleFSPriceManagement.DetermineCustomerPriceClass(cache, customerID, customerLocationID, inventoryID, currencyinfo, quantity, uom, date, alwaysFromBaseCurrency);
      priceType = !(str == "BASE") ? "PRCLS" : FSPriceManagement.CheckPriceByPriceType(cache, str, customerID, inventoryID, currencyinfo.BaseCuryID, alwaysFromBaseCurrency ? currencyinfo.BaseCuryID : currencyinfo.CuryID, new Decimal?(Math.Abs(quantity.GetValueOrDefault())), uom, date, "BASEP", ref errorCode);
    }
    Decimal? price1 = new Decimal?();
    ARSalesPriceMaint arSalesPriceMaint = ARSalesPriceMaint.SingleARSalesPriceMaint;
    bool baseCurrencySetting = arSalesPriceMaint.GetAlwaysFromBaseCurrencySetting(cache);
    ARSalesPriceMaint.SalesPriceItem salesPrice;
    try
    {
      salesPrice = arSalesPriceMaint.FindSalesPrice(cache, str, customerID, inventoryID, siteID, currencyinfo.BaseCuryID, baseCurrencySetting ? currencyinfo.BaseCuryID : currencyinfo.CuryID, new Decimal?(Math.Abs(quantity.GetValueOrDefault())), uom, date, taxCalcMode);
    }
    catch (PXUnitConversionException ex)
    {
      if (catchSalesPriceException)
        return new SalesPriceSet(str, price1, priceType, customerID, "UOM_INCONSISTENCY");
      throw ex;
    }
    Decimal? price2 = arSalesPriceMaint.AdjustSalesPrice(cache, salesPrice, inventoryID, currencyinfo, uom);
    return new SalesPriceSet(str, price2, priceType, customerID, errorCode);
  }

  /// <summary>
  /// Determines if an item has a Customer Price Class defined depending on the Customer Location.
  /// </summary>
  public virtual string DetermineCustomerPriceClass(
    PXCache cache,
    int? customerID,
    int? customerLocationID,
    int? inventoryID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    Decimal? quantity,
    string uom,
    DateTime date,
    bool alwaysFromBaseCurrency)
  {
    string customerPriceClass = "BASE";
    this.errorCode = "OK";
    if (!customerLocationID.HasValue || !customerID.HasValue)
      return customerPriceClass;
    PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select(cache.Graph, new object[2]
    {
      (object) customerID,
      (object) customerLocationID
    }));
    return location == null || string.IsNullOrEmpty(location.CPriceClassID) || FSPriceManagement.CheckPriceByPriceType(cache, location.CPriceClassID, customerID, inventoryID, currencyinfo.BaseCuryID, alwaysFromBaseCurrency ? currencyinfo.BaseCuryID : currencyinfo.CuryID, new Decimal?(Math.Abs(quantity.GetValueOrDefault())), uom, date, "PRCLS", ref this.errorCode) == null ? customerPriceClass : location.CPriceClassID;
  }

  /// <summary>
  /// Returns true if for the Customer and/or Customer Price Class there is a price defined for the item.
  /// </summary>
  private static string CheckPriceByPriceType(
    PXCache cache,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    string baseCuryID,
    string curyID,
    Decimal? quantity,
    string uom,
    DateTime date,
    string type,
    ref string errorCode)
  {
    PXSelectBase<ARSalesPrice> pxSelectBase1 = (PXSelectBase<ARSalesPrice>) new PXSelect<ARSalesPrice, Where<ARSalesPrice.inventoryID, Equal<Required<ARSalesPrice.inventoryID>>, And2<Where2<Where<ARSalesPrice.priceType, Equal<PriceTypes.customer>, And<ARSalesPrice.customerID, Equal<Required<ARSalesPrice.customerID>>, And<Required<ARSalesPrice.custPriceClassID>, IsNull>>>, Or2<Where<ARSalesPrice.priceType, Equal<PriceTypes.customerPriceClass>, And<ARSalesPrice.custPriceClassID, Equal<Required<ARSalesPrice.custPriceClassID>>, And<Required<ARSalesPrice.customerID>, IsNull>>>, Or<Where<ARSalesPrice.priceType, Equal<PriceTypes.basePrice>, And<Required<ARSalesPrice.customerID>, IsNull, And<Required<ARSalesPrice.custPriceClassID>, IsNull>>>>>>, And<ARSalesPrice.curyID, Equal<Required<ARSalesPrice.curyID>>, And<Where2<Where<ARSalesPrice.breakQty, LessEqual<Required<ARSalesPrice.breakQty>>>, And<Where2<Where<ARSalesPrice.effectiveDate, LessEqual<Required<ARSalesPrice.effectiveDate>>, And<ARSalesPrice.expirationDate, GreaterEqual<Required<ARSalesPrice.expirationDate>>>>, Or2<Where<ARSalesPrice.effectiveDate, LessEqual<Required<ARSalesPrice.effectiveDate>>, And<ARSalesPrice.expirationDate, IsNull>>, Or<Where<ARSalesPrice.expirationDate, GreaterEqual<Required<ARSalesPrice.expirationDate>>, And<ARSalesPrice.effectiveDate, IsNull, Or<ARSalesPrice.effectiveDate, IsNull, And<ARSalesPrice.expirationDate, IsNull>>>>>>>>>>>>>, OrderBy<Asc<ARSalesPrice.priceType, Desc<ARSalesPrice.isPromotionalPrice, Desc<ARSalesPrice.breakQty>>>>>(cache.Graph);
    PXSelectBase<ARSalesPrice> pxSelectBase2 = (PXSelectBase<ARSalesPrice>) new PXSelectJoin<ARSalesPrice, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ARSalesPrice.inventoryID>, And<PX.Objects.IN.InventoryItem.baseUnit, Equal<ARSalesPrice.uOM>>>>, Where<ARSalesPrice.inventoryID, Equal<Required<ARSalesPrice.inventoryID>>, And2<Where2<Where<ARSalesPrice.priceType, Equal<PriceTypes.customer>, And<ARSalesPrice.customerID, Equal<Required<ARSalesPrice.customerID>>, And<Required<ARSalesPrice.custPriceClassID>, IsNull>>>, Or2<Where<ARSalesPrice.priceType, Equal<PriceTypes.customerPriceClass>, And<ARSalesPrice.custPriceClassID, Equal<Required<ARSalesPrice.custPriceClassID>>, And<Required<ARSalesPrice.customerID>, IsNull>>>, Or<Where<ARSalesPrice.priceType, Equal<PriceTypes.basePrice>, And<Required<ARSalesPrice.customerID>, IsNull, And<Required<ARSalesPrice.custPriceClassID>, IsNull>>>>>>, And<ARSalesPrice.curyID, Equal<Required<ARSalesPrice.curyID>>, And<Where2<Where<ARSalesPrice.breakQty, LessEqual<Required<ARSalesPrice.breakQty>>>, And<Where2<Where<ARSalesPrice.effectiveDate, LessEqual<Required<ARSalesPrice.effectiveDate>>, And<ARSalesPrice.expirationDate, GreaterEqual<Required<ARSalesPrice.expirationDate>>>>, Or2<Where<ARSalesPrice.effectiveDate, LessEqual<Required<ARSalesPrice.effectiveDate>>, And<ARSalesPrice.expirationDate, IsNull>>, Or<Where<ARSalesPrice.expirationDate, GreaterEqual<Required<ARSalesPrice.expirationDate>>, And<ARSalesPrice.effectiveDate, IsNull, Or<ARSalesPrice.effectiveDate, IsNull, And<ARSalesPrice.expirationDate, IsNull>>>>>>>>>>>>>, OrderBy<Asc<ARSalesPrice.priceType, Desc<ARSalesPrice.isPromotionalPrice, Desc<ARSalesPrice.breakQty>>>>>(cache.Graph);
    string str = (string) null;
    switch (type)
    {
      case "CUSTM":
        ARSalesPrice arSalesPriceRow1 = PXResultset<ARSalesPrice>.op_Implicit(pxSelectBase1.SelectWindowed(0, 1, new object[13]
        {
          (object) inventoryID,
          (object) customerID,
          null,
          (object) custPriceClass,
          (object) customerID,
          (object) customerID,
          (object) custPriceClass,
          (object) curyID,
          (object) quantity,
          (object) date,
          (object) date,
          (object) date,
          (object) date
        }));
        errorCode = FSPriceManagement.CheckInventoryItemUOM(cache, arSalesPriceRow1, inventoryID, uom);
        if (arSalesPriceRow1 == null && errorCode == "OK")
        {
          Decimal num = INUnitAttribute.ConvertToBase(cache, inventoryID, uom, quantity.Value, INPrecision.QUANTITY);
          arSalesPriceRow1 = PXResultset<ARSalesPrice>.op_Implicit(pxSelectBase2.Select(new object[13]
          {
            (object) inventoryID,
            (object) customerID,
            null,
            (object) custPriceClass,
            (object) customerID,
            (object) customerID,
            (object) custPriceClass,
            (object) curyID,
            (object) num,
            (object) date,
            (object) date,
            (object) date,
            (object) date
          }));
        }
        if (arSalesPriceRow1 != null && errorCode == "OK")
        {
          str = "CUSTM";
          break;
        }
        break;
      case "PRCLS":
        ARSalesPrice arSalesPriceRow2 = PXResultset<ARSalesPrice>.op_Implicit(pxSelectBase1.SelectWindowed(0, 1, new object[13]
        {
          (object) inventoryID,
          (object) customerID,
          (object) custPriceClass,
          (object) custPriceClass,
          null,
          (object) customerID,
          (object) custPriceClass,
          (object) curyID,
          (object) quantity,
          (object) date,
          (object) date,
          (object) date,
          (object) date
        }));
        errorCode = FSPriceManagement.CheckInventoryItemUOM(cache, arSalesPriceRow2, inventoryID, uom);
        if (arSalesPriceRow2 == null && errorCode == "OK")
        {
          Decimal num = INUnitAttribute.ConvertToBase(cache, inventoryID, uom, quantity.Value, INPrecision.QUANTITY);
          arSalesPriceRow2 = PXResultset<ARSalesPrice>.op_Implicit(pxSelectBase2.Select(new object[13]
          {
            (object) inventoryID,
            (object) customerID,
            (object) custPriceClass,
            (object) custPriceClass,
            null,
            (object) customerID,
            (object) custPriceClass,
            (object) curyID,
            (object) num,
            (object) date,
            (object) date,
            (object) date,
            (object) date
          }));
        }
        if (arSalesPriceRow2 != null && errorCode == "OK")
        {
          str = "PRCLS";
          break;
        }
        break;
      default:
        ARSalesPrice arSalesPriceRow3 = PXResultset<ARSalesPrice>.op_Implicit(pxSelectBase1.SelectWindowed(0, 1, new object[13]
        {
          (object) inventoryID,
          (object) customerID,
          (object) custPriceClass,
          (object) custPriceClass,
          (object) customerID,
          null,
          null,
          (object) curyID,
          (object) quantity,
          (object) date,
          (object) date,
          (object) date,
          (object) date
        }));
        errorCode = FSPriceManagement.CheckInventoryItemUOM(cache, arSalesPriceRow3, inventoryID, uom);
        if (arSalesPriceRow3 == null)
        {
          Decimal num = INUnitAttribute.ConvertToBase(cache, inventoryID, uom, quantity.Value, INPrecision.QUANTITY);
          arSalesPriceRow3 = PXResultset<ARSalesPrice>.op_Implicit(pxSelectBase2.Select(new object[13]
          {
            (object) inventoryID,
            (object) customerID,
            (object) custPriceClass,
            (object) custPriceClass,
            (object) customerID,
            null,
            null,
            (object) curyID,
            (object) num,
            (object) date,
            (object) date,
            (object) date,
            (object) date
          }));
          if (arSalesPriceRow3 == null)
            str = "DEFLT";
        }
        if (arSalesPriceRow3 != null && errorCode == "OK")
        {
          str = "BASEP";
          break;
        }
        break;
    }
    return str;
  }

  /// <summary>
  /// Verifies whether or not the system can convert the InventoryItem's <c>UOM</c> to the one defined in the Sales Price screen.
  /// If <c>arSalesPriceRow != null</c> means that the price from Sale Price applies.
  /// If <c>arSalesPriceRow.UOM != uom</c> means that the <c>UOM</c> conversion applies.
  /// </summary>
  /// <param name="cache">PXCache instance.</param>
  /// <param name="arSalesPriceRow"><c>ARSalesPrice</c> instance.</param>
  /// <param name="inventoryID">Inventory Item ID.</param>
  /// <param name="uom">Unit of measure required.</param>
  /// <returns>Returns an errorCode status.</returns>
  private static string CheckInventoryItemUOM(
    PXCache cache,
    ARSalesPrice arSalesPriceRow,
    int? inventoryID,
    string uom)
  {
    string str = "OK";
    if (arSalesPriceRow != null)
    {
      if (arSalesPriceRow.UOM != uom)
      {
        try
        {
          Decimal num = 0M;
          INUnitAttribute.ConvertFromBase(cache, inventoryID, arSalesPriceRow.UOM, num, INPrecision.NOROUND);
        }
        catch (PXException ex)
        {
          str = ((Exception) ex).Message;
          if (((Exception) ex).Message.IndexOf("Unit conversion is missing.") != -1)
            str = "UOM_INCONSISTENCY";
        }
      }
    }
    return str;
  }
}
