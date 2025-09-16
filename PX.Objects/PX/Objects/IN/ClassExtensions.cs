// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.ClassExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

public static class ClassExtensions
{
  internal static InventoryUnitType ToUnitTypes(this InventoryItem inventoryItem, string unit)
  {
    InventoryUnitType unitTypes = InventoryUnitType.None;
    if (inventoryItem.BaseUnit == unit)
      unitTypes |= InventoryUnitType.BaseUnit;
    if (inventoryItem.SalesUnit == unit)
      unitTypes |= InventoryUnitType.SalesUnit;
    if (inventoryItem.PurchaseUnit == unit)
      unitTypes |= InventoryUnitType.PurchaseUnit;
    return unitTypes;
  }

  internal static InventoryUnitType ToIntegerUnits(this InventoryItem inventoryItem)
  {
    InventoryUnitType integerUnits = InventoryUnitType.None;
    bool? decimalBaseUnit = inventoryItem.DecimalBaseUnit;
    bool flag1 = false;
    if (decimalBaseUnit.GetValueOrDefault() == flag1 & decimalBaseUnit.HasValue)
      integerUnits |= InventoryUnitType.BaseUnit;
    bool? decimalSalesUnit = inventoryItem.DecimalSalesUnit;
    bool flag2 = false;
    if (decimalSalesUnit.GetValueOrDefault() == flag2 & decimalSalesUnit.HasValue)
      integerUnits |= InventoryUnitType.SalesUnit;
    bool? decimalPurchaseUnit = inventoryItem.DecimalPurchaseUnit;
    bool flag3 = false;
    if (decimalPurchaseUnit.GetValueOrDefault() == flag3 & decimalPurchaseUnit.HasValue)
      integerUnits |= InventoryUnitType.PurchaseUnit;
    return integerUnits;
  }

  internal static string GetUnitID(
    this InventoryItem inventoryItem,
    InventoryUnitType inventoryUnitType)
  {
    switch (inventoryUnitType)
    {
      case InventoryUnitType.None:
        return (string) null;
      case InventoryUnitType.BaseUnit:
        return inventoryItem.BaseUnit;
      case InventoryUnitType.SalesUnit:
        return inventoryItem.SalesUnit;
      case InventoryUnitType.PurchaseUnit:
        return inventoryItem.PurchaseUnit;
      default:
        throw new ArgumentOutOfRangeException(nameof (inventoryUnitType));
    }
  }

  /// <summary>
  /// Is equivalent to Enum.GetValues(typeof(InventoryUnitType)) without default value
  /// </summary>
  /// <param name="unitType"></param>
  /// <returns></returns>
  internal static IEnumerable<InventoryUnitType> Split(this InventoryUnitType unitType)
  {
    if ((unitType & InventoryUnitType.PurchaseUnit) != InventoryUnitType.None)
      yield return InventoryUnitType.PurchaseUnit;
    if ((unitType & InventoryUnitType.SalesUnit) != InventoryUnitType.None)
      yield return InventoryUnitType.SalesUnit;
    if ((unitType & InventoryUnitType.BaseUnit) != InventoryUnitType.None)
      yield return InventoryUnitType.BaseUnit;
  }

  public static bool IsZero(this IStatus a)
  {
    Decimal? qtyAvail = a.QtyAvail;
    Decimal num1 = 0M;
    if (qtyAvail.GetValueOrDefault() == num1 & qtyAvail.HasValue)
    {
      Decimal? qtyHardAvail = a.QtyHardAvail;
      Decimal num2 = 0M;
      if (qtyHardAvail.GetValueOrDefault() == num2 & qtyHardAvail.HasValue)
      {
        Decimal? qtyActual = a.QtyActual;
        Decimal num3 = 0M;
        if (qtyActual.GetValueOrDefault() == num3 & qtyActual.HasValue)
        {
          Decimal? qtyNotAvail = a.QtyNotAvail;
          Decimal num4 = 0M;
          if (qtyNotAvail.GetValueOrDefault() == num4 & qtyNotAvail.HasValue)
          {
            Decimal? qtyOnHand = a.QtyOnHand;
            Decimal num5 = 0M;
            if (qtyOnHand.GetValueOrDefault() == num5 & qtyOnHand.HasValue)
            {
              Decimal? fsSrvOrdPrepared1 = a.QtyFSSrvOrdPrepared;
              Decimal num6 = 0M;
              if (fsSrvOrdPrepared1.GetValueOrDefault() == num6 & fsSrvOrdPrepared1.HasValue)
              {
                Decimal? qtyFsSrvOrdBooked = a.QtyFSSrvOrdBooked;
                Decimal num7 = 0M;
                if (qtyFsSrvOrdBooked.GetValueOrDefault() == num7 & qtyFsSrvOrdBooked.HasValue)
                {
                  Decimal? fsSrvOrdAllocated = a.QtyFSSrvOrdAllocated;
                  Decimal num8 = 0M;
                  if (fsSrvOrdAllocated.GetValueOrDefault() == num8 & fsSrvOrdAllocated.HasValue)
                  {
                    Decimal? qtySoPrepared = a.QtySOPrepared;
                    Decimal num9 = 0M;
                    if (qtySoPrepared.GetValueOrDefault() == num9 & qtySoPrepared.HasValue)
                    {
                      Decimal? qtySoBooked = a.QtySOBooked;
                      Decimal num10 = 0M;
                      if (qtySoBooked.GetValueOrDefault() == num10 & qtySoBooked.HasValue)
                      {
                        Decimal? qtySoShipping = a.QtySOShipping;
                        Decimal num11 = 0M;
                        if (qtySoShipping.GetValueOrDefault() == num11 & qtySoShipping.HasValue)
                        {
                          Decimal? qtySoShipped = a.QtySOShipped;
                          Decimal num12 = 0M;
                          if (qtySoShipped.GetValueOrDefault() == num12 & qtySoShipped.HasValue)
                          {
                            Decimal? qtySoBackOrdered = a.QtySOBackOrdered;
                            Decimal num13 = 0M;
                            if (qtySoBackOrdered.GetValueOrDefault() == num13 & qtySoBackOrdered.HasValue)
                            {
                              Decimal? qtyInIssues = a.QtyINIssues;
                              Decimal num14 = 0M;
                              if (qtyInIssues.GetValueOrDefault() == num14 & qtyInIssues.HasValue)
                              {
                                Decimal? qtyInReceipts = a.QtyINReceipts;
                                Decimal num15 = 0M;
                                if (qtyInReceipts.GetValueOrDefault() == num15 & qtyInReceipts.HasValue)
                                {
                                  Decimal? qtyInTransit = a.QtyInTransit;
                                  Decimal num16 = 0M;
                                  if (qtyInTransit.GetValueOrDefault() == num16 & qtyInTransit.HasValue)
                                  {
                                    Decimal? qtyInTransitToSo = a.QtyInTransitToSO;
                                    Decimal num17 = 0M;
                                    if (qtyInTransitToSo.GetValueOrDefault() == num17 & qtyInTransitToSo.HasValue)
                                    {
                                      Decimal? qtyInReplaned = a.QtyINReplaned;
                                      Decimal num18 = 0M;
                                      if (qtyInReplaned.GetValueOrDefault() == num18 & qtyInReplaned.HasValue)
                                      {
                                        Decimal? qtyPoPrepared = a.QtyPOPrepared;
                                        Decimal num19 = 0M;
                                        if (qtyPoPrepared.GetValueOrDefault() == num19 & qtyPoPrepared.HasValue)
                                        {
                                          Decimal? qtyPoOrders = a.QtyPOOrders;
                                          Decimal num20 = 0M;
                                          if (qtyPoOrders.GetValueOrDefault() == num20 & qtyPoOrders.HasValue)
                                          {
                                            Decimal? qtyPoReceipts = a.QtyPOReceipts;
                                            Decimal num21 = 0M;
                                            if (qtyPoReceipts.GetValueOrDefault() == num21 & qtyPoReceipts.HasValue)
                                            {
                                              Decimal? inAssemblyDemand = a.QtyINAssemblyDemand;
                                              Decimal num22 = 0M;
                                              if (inAssemblyDemand.GetValueOrDefault() == num22 & inAssemblyDemand.HasValue)
                                              {
                                                Decimal? inAssemblySupply = a.QtyINAssemblySupply;
                                                Decimal num23 = 0M;
                                                if (inAssemblySupply.GetValueOrDefault() == num23 & inAssemblySupply.HasValue)
                                                {
                                                  Decimal? transitToProduction = a.QtyInTransitToProduction;
                                                  Decimal num24 = 0M;
                                                  if (transitToProduction.GetValueOrDefault() == num24 & transitToProduction.HasValue)
                                                  {
                                                    Decimal? productionSupplyPrepared = a.QtyProductionSupplyPrepared;
                                                    Decimal num25 = 0M;
                                                    if (productionSupplyPrepared.GetValueOrDefault() == num25 & productionSupplyPrepared.HasValue)
                                                    {
                                                      Decimal? productionSupply = a.QtyProductionSupply;
                                                      Decimal num26 = 0M;
                                                      if (productionSupply.GetValueOrDefault() == num26 & productionSupply.HasValue)
                                                      {
                                                        Decimal? productionPrepared = a.QtyPOFixedProductionPrepared;
                                                        Decimal num27 = 0M;
                                                        if (productionPrepared.GetValueOrDefault() == num27 & productionPrepared.HasValue)
                                                        {
                                                          Decimal? productionOrders = a.QtyPOFixedProductionOrders;
                                                          Decimal num28 = 0M;
                                                          if (productionOrders.GetValueOrDefault() == num28 & productionOrders.HasValue)
                                                          {
                                                            Decimal? productionDemandPrepared = a.QtyProductionDemandPrepared;
                                                            Decimal num29 = 0M;
                                                            if (productionDemandPrepared.GetValueOrDefault() == num29 & productionDemandPrepared.HasValue)
                                                            {
                                                              Decimal? productionDemand = a.QtyProductionDemand;
                                                              Decimal num30 = 0M;
                                                              if (productionDemand.GetValueOrDefault() == num30 & productionDemand.HasValue)
                                                              {
                                                                Decimal? productionAllocated = a.QtyProductionAllocated;
                                                                Decimal num31 = 0M;
                                                                if (productionAllocated.GetValueOrDefault() == num31 & productionAllocated.HasValue)
                                                                {
                                                                  Decimal? soFixedProduction = a.QtySOFixedProduction;
                                                                  Decimal num32 = 0M;
                                                                  if (soFixedProduction.GetValueOrDefault() == num32 & soFixedProduction.HasValue)
                                                                  {
                                                                    Decimal? prodFixedPurchase = a.QtyProdFixedPurchase;
                                                                    Decimal num33 = 0M;
                                                                    if (prodFixedPurchase.GetValueOrDefault() == num33 & prodFixedPurchase.HasValue)
                                                                    {
                                                                      Decimal? prodFixedProduction = a.QtyProdFixedProduction;
                                                                      Decimal num34 = 0M;
                                                                      if (prodFixedProduction.GetValueOrDefault() == num34 & prodFixedProduction.HasValue)
                                                                      {
                                                                        Decimal? prodOrdersPrepared = a.QtyProdFixedProdOrdersPrepared;
                                                                        Decimal num35 = 0M;
                                                                        if (prodOrdersPrepared.GetValueOrDefault() == num35 & prodOrdersPrepared.HasValue)
                                                                        {
                                                                          Decimal? prodFixedProdOrders = a.QtyProdFixedProdOrders;
                                                                          Decimal num36 = 0M;
                                                                          if (prodFixedProdOrders.GetValueOrDefault() == num36 & prodFixedProdOrders.HasValue)
                                                                          {
                                                                            Decimal? salesOrdersPrepared = a.QtyProdFixedSalesOrdersPrepared;
                                                                            Decimal num37 = 0M;
                                                                            if (salesOrdersPrepared.GetValueOrDefault() == num37 & salesOrdersPrepared.HasValue)
                                                                            {
                                                                              Decimal? fixedSalesOrders = a.QtyProdFixedSalesOrders;
                                                                              Decimal num38 = 0M;
                                                                              if (fixedSalesOrders.GetValueOrDefault() == num38 & fixedSalesOrders.HasValue)
                                                                              {
                                                                                Decimal? qtyFixedFsSrvOrd = a.QtyFixedFSSrvOrd;
                                                                                Decimal num39 = 0M;
                                                                                if (qtyFixedFsSrvOrd.GetValueOrDefault() == num39 & qtyFixedFsSrvOrd.HasValue)
                                                                                {
                                                                                  Decimal? qtyPoFixedFsSrvOrd = a.QtyPOFixedFSSrvOrd;
                                                                                  Decimal num40 = 0M;
                                                                                  if (qtyPoFixedFsSrvOrd.GetValueOrDefault() == num40 & qtyPoFixedFsSrvOrd.HasValue)
                                                                                  {
                                                                                    Decimal? fsSrvOrdPrepared2 = a.QtyPOFixedFSSrvOrdPrepared;
                                                                                    Decimal num41 = 0M;
                                                                                    if (fsSrvOrdPrepared2.GetValueOrDefault() == num41 & fsSrvOrdPrepared2.HasValue)
                                                                                    {
                                                                                      Decimal? fsSrvOrdReceipts = a.QtyPOFixedFSSrvOrdReceipts;
                                                                                      Decimal num42 = 0M;
                                                                                      if (fsSrvOrdReceipts.GetValueOrDefault() == num42 & fsSrvOrdReceipts.HasValue)
                                                                                      {
                                                                                        Decimal? qtySoFixed = a.QtySOFixed;
                                                                                        Decimal num43 = 0M;
                                                                                        if (qtySoFixed.GetValueOrDefault() == num43 & qtySoFixed.HasValue)
                                                                                        {
                                                                                          Decimal? qtyPoFixedOrders = a.QtyPOFixedOrders;
                                                                                          Decimal num44 = 0M;
                                                                                          if (qtyPoFixedOrders.GetValueOrDefault() == num44 & qtyPoFixedOrders.HasValue)
                                                                                          {
                                                                                            Decimal? qtyPoFixedPrepared = a.QtyPOFixedPrepared;
                                                                                            Decimal num45 = 0M;
                                                                                            if (qtyPoFixedPrepared.GetValueOrDefault() == num45 & qtyPoFixedPrepared.HasValue)
                                                                                            {
                                                                                              Decimal? qtyPoFixedReceipts = a.QtyPOFixedReceipts;
                                                                                              Decimal num46 = 0M;
                                                                                              if (qtyPoFixedReceipts.GetValueOrDefault() == num46 & qtyPoFixedReceipts.HasValue)
                                                                                              {
                                                                                                Decimal? qtySoDropShip = a.QtySODropShip;
                                                                                                Decimal num47 = 0M;
                                                                                                if (qtySoDropShip.GetValueOrDefault() == num47 & qtySoDropShip.HasValue)
                                                                                                {
                                                                                                  Decimal? poDropShipOrders = a.QtyPODropShipOrders;
                                                                                                  Decimal num48 = 0M;
                                                                                                  if (poDropShipOrders.GetValueOrDefault() == num48 & poDropShipOrders.HasValue)
                                                                                                  {
                                                                                                    Decimal? dropShipPrepared = a.QtyPODropShipPrepared;
                                                                                                    Decimal num49 = 0M;
                                                                                                    if (dropShipPrepared.GetValueOrDefault() == num49 & dropShipPrepared.HasValue)
                                                                                                    {
                                                                                                      Decimal? dropShipReceipts = a.QtyPODropShipReceipts;
                                                                                                      Decimal num50 = 0M;
                                                                                                      return dropShipReceipts.GetValueOrDefault() == num50 & dropShipReceipts.HasValue;
                                                                                                    }
                                                                                                  }
                                                                                                }
                                                                                              }
                                                                                            }
                                                                                          }
                                                                                        }
                                                                                      }
                                                                                    }
                                                                                  }
                                                                                }
                                                                              }
                                                                            }
                                                                          }
                                                                        }
                                                                      }
                                                                    }
                                                                  }
                                                                }
                                                              }
                                                            }
                                                          }
                                                        }
                                                      }
                                                    }
                                                  }
                                                }
                                              }
                                            }
                                          }
                                        }
                                      }
                                    }
                                  }
                                }
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
    return false;
  }

  public static bool IsZero(this PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial a)
  {
    Decimal? qtyOnHand = a.QtyOnHand;
    Decimal num1 = 0M;
    if (qtyOnHand.GetValueOrDefault() == num1 & qtyOnHand.HasValue)
    {
      Decimal? qtyAvail = a.QtyAvail;
      Decimal num2 = 0M;
      if (qtyAvail.GetValueOrDefault() == num2 & qtyAvail.HasValue)
      {
        Decimal? qtyHardAvail = a.QtyHardAvail;
        Decimal num3 = 0M;
        if (qtyHardAvail.GetValueOrDefault() == num3 & qtyHardAvail.HasValue)
        {
          Decimal? qtyActual = a.QtyActual;
          Decimal num4 = 0M;
          if (qtyActual.GetValueOrDefault() == num4 & qtyActual.HasValue)
          {
            Decimal? qtyNotAvail = a.QtyNotAvail;
            Decimal num5 = 0M;
            if (qtyNotAvail.GetValueOrDefault() == num5 & qtyNotAvail.HasValue)
            {
              Decimal? qtyInTransit = a.QtyInTransit;
              Decimal num6 = 0M;
              if (qtyInTransit.GetValueOrDefault() == num6 & qtyInTransit.HasValue)
              {
                Decimal? qtyOnReceipt = a.QtyOnReceipt;
                Decimal num7 = 0M;
                if (qtyOnReceipt.GetValueOrDefault() == num7 & qtyOnReceipt.HasValue)
                  return !a.UpdateExpireDate.GetValueOrDefault();
              }
            }
          }
        }
      }
    }
    return false;
  }

  public static bool IsZero(this SiteLotSerial a)
  {
    Decimal? qtyOnHand = a.QtyOnHand;
    Decimal num1 = 0M;
    if (qtyOnHand.GetValueOrDefault() == num1 & qtyOnHand.HasValue)
    {
      Decimal? qtyAvail = a.QtyAvail;
      Decimal num2 = 0M;
      if (qtyAvail.GetValueOrDefault() == num2 & qtyAvail.HasValue)
      {
        Decimal? qtyHardAvail = a.QtyHardAvail;
        Decimal num3 = 0M;
        if (qtyHardAvail.GetValueOrDefault() == num3 & qtyHardAvail.HasValue)
        {
          Decimal? qtyActual = a.QtyActual;
          Decimal num4 = 0M;
          if (qtyActual.GetValueOrDefault() == num4 & qtyActual.HasValue)
          {
            Decimal? qtyNotAvail = a.QtyNotAvail;
            Decimal num5 = 0M;
            if (qtyNotAvail.GetValueOrDefault() == num5 & qtyNotAvail.HasValue)
            {
              Decimal? qtyInTransit = a.QtyInTransit;
              Decimal num6 = 0M;
              if (qtyInTransit.GetValueOrDefault() == num6 & qtyInTransit.HasValue)
                return !a.UpdateExpireDate.GetValueOrDefault();
            }
          }
        }
      }
    }
    return false;
  }
}
