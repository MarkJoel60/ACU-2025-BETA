// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.GetItemPriceValue`9
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CT;

public class GetItemPriceValue<ContractID, ContractItemID, ItemType, ItemPriceType, ItemID, FixedPrice, SetupPrice, Qty, PriceDate> : 
  BqlFormulaEvaluator<ContractID, ContractItemID, ItemType, ItemPriceType, ItemID, FixedPrice, SetupPrice, Qty, PriceDate>
  where ContractID : IBqlOperand
  where ContractItemID : IBqlOperand
  where ItemType : IBqlOperand
  where ItemPriceType : IBqlOperand
  where ItemID : IBqlOperand
  where FixedPrice : IBqlOperand
  where SetupPrice : IBqlOperand
  where Qty : IBqlOperand
  where PriceDate : IBqlOperand
{
  public virtual object Evaluate(PXCache cache, object item, Dictionary<System.Type, object> pars)
  {
    int? par1 = (int?) pars[typeof (ContractID)];
    string par2 = (string) pars[typeof (ItemPriceType)];
    string par3 = (string) pars[typeof (ItemType)];
    int? par4 = (int?) pars[typeof (ContractItemID)];
    int? par5 = (int?) pars[typeof (ItemID)];
    Decimal? par6 = (Decimal?) pars[typeof (FixedPrice)];
    Decimal? par7 = (Decimal?) pars[typeof (SetupPrice)];
    Decimal? par8 = (Decimal?) pars[typeof (Qty)];
    DateTime? par9 = (DateTime?) pars[typeof (PriceDate)];
    PXResult<Contract, ContractBillingSchedule> pxResult = (PXResult<Contract, ContractBillingSchedule>) PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelectJoin<Contract, LeftJoin<ContractBillingSchedule, On<ContractBillingSchedule.contractID, Equal<Contract.contractID>>>, Where<Contract.contractID, Equal<Required<Contract.contractID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) par1
    }));
    if (pxResult == null)
      return (object) null;
    Contract contract = PXResult<Contract, ContractBillingSchedule>.op_Implicit(pxResult);
    ContractBillingSchedule contractBillingSchedule = PXResult<Contract, ContractBillingSchedule>.op_Implicit(pxResult);
    return (object) this.GetItemPrice(cache, contract.CuryID, contractBillingSchedule.AccountID ?? contract.CustomerID, contractBillingSchedule.LocationID ?? contract.LocationID, contract.Status, par4, par5, par3, par2, par6, par7, par8, par9);
  }

  public virtual Decimal GetItemPrice(
    PXCache sender,
    string curyID,
    int? customerID,
    int? locationID,
    string contractStatus,
    int? contractItemID,
    int? itemID,
    string itemType,
    string priceOption,
    Decimal? fixedPrice,
    Decimal? setupPrice,
    Decimal? qty,
    DateTime? date)
  {
    ContractItem contractItem = PXResultset<ContractItem>.op_Implicit(PXSelectBase<ContractItem, PXSelect<ContractItem, Where<ContractItem.contractItemID, Equal<Required<ContractItem.contractItemID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) contractItemID
    }));
    if (contractItem == null)
      return 0M;
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) itemID
    }));
    PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<Contract.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<Contract.locationID>>>>>.Config>.Select(sender.Graph, new object[2]
    {
      (object) customerID,
      (object) locationID
    }));
    string custPriceClass = string.IsNullOrEmpty(location?.CPriceClassID) ? "BASE" : location.CPriceClassID;
    string taxCalcMode = location?.CTaxCalcMode ?? "T";
    PX.Objects.CM.Extensions.CurrencyInfo info = new PX.Objects.CM.Extensions.CurrencyInfo()
    {
      BaseCuryID = sender.Graph.Accessinfo.BaseCuryID,
      CuryID = curyID,
      CuryEffDate = date
    };
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) customerID
    }));
    if (customer != null && customer.CuryRateTypeID != null)
      info.CuryRateTypeID = customer.CuryRateTypeID;
    if (customer != null && customer.BaseCuryID != null)
      info.BaseCuryID = customer.BaseCuryID;
    if (info.CuryRateTypeID == null)
    {
      CMSetup cmSetup = PXResultset<CMSetup>.op_Implicit(PXSetup<CMSetup>.Select(sender.Graph, Array.Empty<object>()));
      info.CuryRateTypeID = cmSetup?.ARRateTypeDflt;
    }
    IPXCurrencyRate rate = info.SearchForNewRate(sender.Graph);
    if (rate != null)
      rate.Populate(info);
    if (inventoryItem == null || info == null)
      return 0M;
    switch (priceOption ?? GetItemPriceValue<ContractID, ContractItemID, ItemType, ItemPriceType, ItemID, FixedPrice, SetupPrice, Qty, PriceDate>.GetPriceOptionFromItem(itemType, contractItem))
    {
      case "I":
        return ARSalesPriceMaint.CalculateSalesPrice(sender, custPriceClass, customerID, itemID, info.GetCM(), qty, inventoryItem.BaseUnit, date ?? DateTime.Now, false, taxCalcMode).GetValueOrDefault();
      case "P":
        return ARSalesPriceMaint.CalculateSalesPrice(sender, custPriceClass, customerID, itemID, info.GetCM(), qty, inventoryItem.BaseUnit, date ?? DateTime.Now, false, taxCalcMode).GetValueOrDefault() / 100M * fixedPrice.GetValueOrDefault();
      case "B":
        return setupPrice.GetValueOrDefault() / 100M * fixedPrice.GetValueOrDefault();
      case "M":
        return fixedPrice.GetValueOrDefault();
      default:
        throw new InvalidOperationException("Unexpected Price Option: " + priceOption);
    }
  }

  private static string GetPriceOptionFromItem(string itemType, ContractItem item)
  {
    switch (itemType)
    {
      case "S":
        return item.BasePriceOption;
      case "R":
        return item.RenewalPriceOption;
      case "B":
        return item.FixedRecurringPriceOption;
      case "U":
        return item.UsagePriceOption;
      default:
        throw new InvalidOperationException("Unexpected Item Type: " + itemType);
    }
  }
}
