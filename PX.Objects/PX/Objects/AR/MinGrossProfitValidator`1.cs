// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.MinGrossProfitValidator`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.IN;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.AR;

public static class MinGrossProfitValidator<TLine> where TLine : 
#nullable disable
class, IBqlTable, IHasMinGrossProfit, new()
{
  public static Decimal CalculateMinPrice<InfoKeyField, inventoryIDField, uOMField>(
    PXCache sender,
    TLine line,
    PX.Objects.IN.InventoryItem inventoryItem,
    INItemCost inItemCost)
    where InfoKeyField : IBqlField
    where inventoryIDField : IBqlField
    where uOMField : IBqlField
  {
    Decimal curyval = MinGrossProfitValidator<TLine>.CalculateMinPrice<inventoryIDField, uOMField>(sender, line, inventoryItem, inItemCost);
    if (sender.GetValue<InfoKeyField>((object) line) != null)
    {
      try
      {
        PXDBCurrencyAttribute.CuryConvCury<InfoKeyField>(sender, (object) line, curyval, out curyval, true);
      }
      catch (PXRateNotFoundException ex)
      {
        return curyval;
      }
    }
    return curyval;
  }

  public static Decimal CalculateMinPrice<inventoryIDField, uOMField>(
    PXCache sender,
    TLine line,
    PX.Objects.IN.InventoryItem inventoryItem,
    INItemCost inItemCost)
    where inventoryIDField : IBqlField
    where uOMField : IBqlField
  {
    if ((object) line == null || inventoryItem == null || inItemCost == null)
      return 0M;
    InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find(sender.Graph, inventoryItem.InventoryID, inItemCost.CuryID);
    return INUnitAttribute.ConvertToBase<inventoryIDField, uOMField>(sender, (object) line, PXPriceCostAttribute.MinPrice(inventoryItem, inItemCost, itemCurySettings), INPrecision.UNITCOST);
  }

  public static Decimal? ValidateUnitPrice<InfoKeyField, inventoryIDField, uOMField>(
    PXCache sender,
    TLine line,
    Decimal? curyNewUnitPrice)
    where InfoKeyField : IBqlField
    where inventoryIDField : IBqlField
    where uOMField : IBqlField
  {
    if (sender.Graph.UnattendedMode)
      return curyNewUnitPrice;
    SOSetup soSetup = PXResultset<SOSetup>.op_Implicit(PXSetupSelect<SOSetup>.Select(sender.Graph, Array.Empty<object>()));
    if (soSetup.MinGrossProfitValidation == "N" || (object) line == null || !line.InventoryID.HasValue || line.UOM == null)
      return curyNewUnitPrice;
    Decimal? nullable = curyNewUnitPrice;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() >= num & nullable.HasValue && !line.IsFree.GetValueOrDefault() && line.SiteID.HasValue)
    {
      PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find(sender.Graph, line.SiteID);
      PXResult<PX.Objects.IN.InventoryItem, INItemCost> pxResult = (PXResult<PX.Objects.IN.InventoryItem, INItemCost>) PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelectJoin<PX.Objects.IN.InventoryItem, LeftJoin<INItemCost, On<INItemCost.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<INItemCost.curyID, Equal<Required<PX.Objects.IN.INSite.baseCuryID>>>>>, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<ARSalesPrice.inventoryID>>>>.Config>.Select(sender.Graph, new object[2]
      {
        (object) inSite?.BaseCuryID,
        (object) line.InventoryID
      }));
      PX.Objects.IN.InventoryItem inItem = PXResult<PX.Objects.IN.InventoryItem, INItemCost>.op_Implicit(pxResult);
      INItemCost inItemCost = PXResult<PX.Objects.IN.InventoryItem, INItemCost>.op_Implicit(pxResult);
      curyNewUnitPrice = MinGrossProfitValidator<TLine>.ValidateUnitPrice<InfoKeyField, inventoryIDField, uOMField>(sender, line, inItem, inItemCost, curyNewUnitPrice, soSetup.MinGrossProfitValidation);
    }
    return curyNewUnitPrice;
  }

  public static Decimal? ValidateUnitPrice<InfoKeyField, inventoryIDField, uOMField>(
    PXCache sender,
    TLine line,
    PX.Objects.IN.InventoryItem inItem,
    INItemCost inItemCost,
    Decimal? curyNewUnitPrice,
    string MinGrossProfitValidation)
    where InfoKeyField : IBqlField
    where inventoryIDField : IBqlField
    where uOMField : IBqlField
  {
    if (inItem == null || inItemCost == null)
      return curyNewUnitPrice;
    Decimal minPrice = MinGrossProfitValidator<TLine>.CalculateMinPrice<InfoKeyField, inventoryIDField, uOMField>(sender, line, inItem, inItemCost);
    return MinGrossProfitValidator.Validate<MinGrossProfitValidator<TLine>.curyUnitPrice>(sender, (object) line, inItem, MinGrossProfitValidation, curyNewUnitPrice, new Decimal?(minPrice), curyNewUnitPrice, new Decimal?(minPrice), MinGrossProfitValidator.Target.UnitPrice);
  }

  /// <summary>
  /// Validates Discount %. The Unit Price with discount for an item cannot be less then StdCost plus Minimal Gross Profit.
  /// </summary>
  /// <param name="sender">Cache</param>
  /// <param name="line">Target row</param>
  /// <param name="unitPrice">UnitPrice in base currency</param>
  /// <param name="newDiscPct">new Discount %</param>
  public static Decimal? ValidateDiscountPct<inventoryIDField, uOMField>(
    PXCache sender,
    TLine line,
    Decimal? unitPrice,
    Decimal? newDiscPct)
    where inventoryIDField : IBqlField
    where uOMField : IBqlField
  {
    if (sender.Graph.UnattendedMode)
      return newDiscPct;
    SOSetup soSetup = PXResultset<SOSetup>.op_Implicit(PXSetupSelect<SOSetup>.Select(sender.Graph, Array.Empty<object>()));
    if (soSetup.MinGrossProfitValidation == "N" || (object) line == null)
      return newDiscPct;
    Decimal? nullable1 = unitPrice;
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue)
    {
      Decimal? nullable2 = newDiscPct;
      Decimal num2 = 0M;
      if (nullable2.GetValueOrDefault() > num2 & nullable2.HasValue && !line.IsFree.GetValueOrDefault() && line.SiteID.HasValue)
      {
        PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find(sender.Graph, line.SiteID);
        PXResult<PX.Objects.IN.InventoryItem, INItemCost> pxResult = (PXResult<PX.Objects.IN.InventoryItem, INItemCost>) PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelectJoin<PX.Objects.IN.InventoryItem, LeftJoin<INItemCost, On<INItemCost.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<INItemCost.curyID, Equal<Required<PX.Objects.IN.INSite.baseCuryID>>>>>, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<ARSalesPrice.inventoryID>>>>.Config>.Select(sender.Graph, new object[2]
        {
          (object) inSite?.BaseCuryID,
          (object) line.InventoryID
        }));
        PX.Objects.IN.InventoryItem inItem = PXResult<PX.Objects.IN.InventoryItem, INItemCost>.op_Implicit(pxResult);
        INItemCost inItemCost = PXResult<PX.Objects.IN.InventoryItem, INItemCost>.op_Implicit(pxResult);
        newDiscPct = MinGrossProfitValidator<TLine>.ValidateDiscountPct<inventoryIDField, uOMField>(sender, line, inItem, inItemCost, unitPrice, newDiscPct, soSetup.MinGrossProfitValidation);
      }
    }
    return newDiscPct;
  }

  public static Decimal? ValidateDiscountPct<inventoryIDField, uOMField>(
    PXCache sender,
    TLine line,
    PX.Objects.IN.InventoryItem inItem,
    INItemCost inItemCost,
    Decimal? unitPrice,
    Decimal? newDiscPct,
    string MinGrossProfitValidation)
    where inventoryIDField : IBqlField
    where uOMField : IBqlField
  {
    if (inItem == null || inItemCost == null)
      return newDiscPct;
    Decimal minPrice = MinGrossProfitValidator<TLine>.CalculateMinPrice<inventoryIDField, uOMField>(sender, line, inItem, inItemCost);
    Decimal? nullable1 = unitPrice;
    Decimal? nullable2 = newDiscPct;
    Decimal? nullable3 = unitPrice;
    Decimal? nullable4 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable3.GetValueOrDefault() * 0.01M) : new Decimal?();
    Decimal? currentValue = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable5;
    if (unitPrice.HasValue)
    {
      nullable4 = unitPrice;
      Decimal num1 = 0M;
      if (!(nullable4.GetValueOrDefault() == num1 & nullable4.HasValue))
      {
        Decimal? nullable6 = unitPrice;
        Decimal num2 = minPrice;
        Decimal? nullable7 = nullable6.HasValue ? new Decimal?(nullable6.GetValueOrDefault() - num2) : new Decimal?();
        Decimal num3 = (Decimal) 100;
        nullable4 = nullable7.HasValue ? new Decimal?(nullable7.GetValueOrDefault() * num3) : new Decimal?();
        nullable1 = unitPrice;
        nullable5 = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() / nullable1.GetValueOrDefault()) : new Decimal?();
        goto label_6;
      }
    }
    nullable5 = new Decimal?(0M);
label_6:
    Decimal? setToMinValue = nullable5;
    return MinGrossProfitValidator.Validate<MinGrossProfitValidator<TLine>.discPct>(sender, (object) line, inItem, MinGrossProfitValidation, currentValue, new Decimal?(minPrice), newDiscPct, setToMinValue, MinGrossProfitValidator.Target.Discount);
  }

  /// <summary>
  /// Validates Discount Amount. The Unit Price with discount for an item cannot be less then StdCost plus Minimal Gross Profit.
  /// </summary>
  /// <param name="sender">Cache</param>
  /// <param name="line">Target row</param>
  /// <param name="unitPrice">UnitPrice in base currency</param>
  /// <param name="newDiscPct">new Discount amount</param>
  public static Decimal? ValidateDiscountAmt<inventoryIDField, uOMField>(
    PXCache sender,
    TLine line,
    Decimal? unitPrice,
    Decimal? newDisc)
    where inventoryIDField : IBqlField
    where uOMField : IBqlField
  {
    if (sender.Graph.UnattendedMode)
      return newDisc;
    SOSetup soSetup = PXResultset<SOSetup>.op_Implicit(PXSetupSelect<SOSetup>.Select(sender.Graph, Array.Empty<object>()));
    if (soSetup.MinGrossProfitValidation == "N" || (object) line == null || !(Math.Abs(line.Qty.GetValueOrDefault()) > 0M))
      return newDisc;
    Decimal? nullable1 = newDisc;
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue)
    {
      Decimal? nullable2 = unitPrice;
      Decimal num2 = 0M;
      if (nullable2.GetValueOrDefault() > num2 & nullable2.HasValue && !line.IsFree.GetValueOrDefault() && line.SiteID.HasValue)
      {
        PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find(sender.Graph, line.SiteID);
        PXResult<PX.Objects.IN.InventoryItem, INItemCost> pxResult = (PXResult<PX.Objects.IN.InventoryItem, INItemCost>) PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelectJoin<PX.Objects.IN.InventoryItem, LeftJoin<INItemCost, On<INItemCost.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<INItemCost.curyID, Equal<Required<PX.Objects.IN.INSite.baseCuryID>>>>>, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<ARSalesPrice.inventoryID>>>>.Config>.Select(sender.Graph, new object[2]
        {
          (object) inSite.BaseCuryID,
          (object) line.InventoryID
        }));
        PX.Objects.IN.InventoryItem inItem = PXResult<PX.Objects.IN.InventoryItem, INItemCost>.op_Implicit(pxResult);
        INItemCost inItemCost = PXResult<PX.Objects.IN.InventoryItem, INItemCost>.op_Implicit(pxResult);
        newDisc = MinGrossProfitValidator<TLine>.ValidateDiscountAmt<inventoryIDField, uOMField>(sender, line, inItem, inItemCost, unitPrice, newDisc, soSetup.MinGrossProfitValidation);
      }
    }
    return newDisc;
  }

  public static Decimal? ValidateDiscountAmt<inventoryIDField, uOMField>(
    PXCache sender,
    TLine line,
    PX.Objects.IN.InventoryItem inItem,
    INItemCost inItemCost,
    Decimal? unitPrice,
    Decimal? newDisc,
    string MinGrossProfitValidation)
    where inventoryIDField : IBqlField
    where uOMField : IBqlField
  {
    if (inItem == null || inItemCost == null)
      return newDisc;
    Decimal minPrice = MinGrossProfitValidator<TLine>.CalculateMinPrice<inventoryIDField, uOMField>(sender, line, inItem, inItemCost);
    Decimal? nullable1 = unitPrice;
    Decimal? nullable2 = newDisc;
    Decimal num1 = Math.Abs(line.Qty.Value);
    Decimal? nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() / num1) : new Decimal?();
    Decimal? currentValue = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
    nullable1 = line.Qty;
    Decimal num2 = Math.Abs(nullable1.Value);
    nullable1 = unitPrice;
    Decimal num3 = minPrice;
    nullable3 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num3) : new Decimal?();
    Decimal? nullable4;
    if (!nullable3.HasValue)
    {
      nullable1 = new Decimal?();
      nullable4 = nullable1;
    }
    else
      nullable4 = new Decimal?(num2 * nullable3.GetValueOrDefault());
    Decimal? setToMinValue = nullable4;
    return MinGrossProfitValidator.Validate<MinGrossProfitValidator<TLine>.curyDiscAmt>(sender, (object) line, inItem, MinGrossProfitValidation, currentValue, new Decimal?(minPrice), newDisc, setToMinValue, MinGrossProfitValidator.Target.Discount);
  }

  private abstract class curyUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MinGrossProfitValidator<TLine>.curyUnitPrice>
  {
  }

  private abstract class curyDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MinGrossProfitValidator<TLine>.curyDiscAmt>
  {
  }

  private abstract class discPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MinGrossProfitValidator<TLine>.discPct>
  {
  }
}
