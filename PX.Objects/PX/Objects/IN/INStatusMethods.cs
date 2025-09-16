// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INStatusMethods
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

public static class INStatusMethods
{
  public static T Add<T>(this T it, IStatus other) where T : IStatus
  {
    return (object) it == null || other == null ? it : INStatusMethods.ApplyChange<T>(it, (IStatus) it, other, (Func<Decimal, Decimal, Decimal>) ((l, r) => l + r));
  }

  public static T AddToNew<T>(this IStatus it, IStatus other) where T : class, IBqlTable, IStatus, new()
  {
    T obj = new T();
    if (!typeof (T).IsAssignableFrom(it.GetType()))
      return INStatusMethods.ApplyChange<T>(obj, it, other, (Func<Decimal, Decimal, Decimal>) ((l, r) => l + r));
    PXCache<T>.RestoreCopy(obj, (T) it);
    return obj.Add<T>(other);
  }

  public static T Subtract<T>(this T it, IStatus other) where T : IStatus
  {
    return (object) it == null || other == null ? it : INStatusMethods.ApplyChange<T>(it, (IStatus) it, other, (Func<Decimal, Decimal, Decimal>) ((l, r) => l - r));
  }

  public static T SubtractToNew<T>(this IStatus it, IStatus other) where T : class, IBqlTable, IStatus, new()
  {
    T obj = new T();
    if (!typeof (T).IsAssignableFrom(it.GetType()))
      return INStatusMethods.ApplyChange<T>(obj, it, other, (Func<Decimal, Decimal, Decimal>) ((l, r) => l - r));
    PXCache<T>.RestoreCopy(obj, (T) it);
    return obj.Subtract<T>(other);
  }

  public static T Multiply<T>(this T it, Decimal unitRate) where T : IStatus
  {
    return (object) it == null ? it : INStatusMethods.ApplyChange<T>(it, (IStatus) it, (IStatus) it, (Func<Decimal, Decimal, Decimal>) ((l, r) => l * unitRate));
  }

  public static T OverrideBy<T>(this T it, IStatus other) where T : IStatus
  {
    return (object) it == null || other == null ? it : INStatusMethods.ApplyChange<T>(it, other, other, (Func<Decimal, Decimal, Decimal>) ((l, r) => l));
  }

  public static T CopyToNew<T>(this IStatus source) where T : class, IBqlTable, IStatus, new()
  {
    return new T().OverrideBy<T>(source);
  }

  private static T ApplyChange<T>(
    T result,
    IStatus left,
    IStatus right,
    Func<Decimal, Decimal, Decimal> operation)
    where T : IStatus
  {
    ref T local1 = ref result;
    T obj;
    if ((object) default (T) == null)
    {
      obj = local1;
      local1 = ref obj;
    }
    Decimal? nullable1 = new Decimal?(opSafe(left.QtyOnHand, right.QtyOnHand));
    local1.QtyOnHand = nullable1;
    ref T local2 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local2;
      local2 = ref obj;
    }
    Decimal? nullable2 = new Decimal?(opSafe(left.QtyAvail, right.QtyAvail));
    local2.QtyAvail = nullable2;
    ref T local3 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local3;
      local3 = ref obj;
    }
    Decimal? nullable3 = new Decimal?(opSafe(left.QtyNotAvail, right.QtyNotAvail));
    local3.QtyNotAvail = nullable3;
    ref T local4 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local4;
      local4 = ref obj;
    }
    Decimal? nullable4 = new Decimal?(opSafe(left.QtyExpired, right.QtyExpired));
    local4.QtyExpired = nullable4;
    ref T local5 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local5;
      local5 = ref obj;
    }
    Decimal? nullable5 = new Decimal?(opSafe(left.QtyHardAvail, right.QtyHardAvail));
    local5.QtyHardAvail = nullable5;
    ref T local6 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local6;
      local6 = ref obj;
    }
    Decimal? nullable6 = new Decimal?(opSafe(left.QtyActual, right.QtyActual));
    local6.QtyActual = nullable6;
    ref T local7 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local7;
      local7 = ref obj;
    }
    Decimal? nullable7 = new Decimal?(opSafe(left.QtyINIssues, right.QtyINIssues));
    local7.QtyINIssues = nullable7;
    ref T local8 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local8;
      local8 = ref obj;
    }
    Decimal? nullable8 = new Decimal?(opSafe(left.QtyINReceipts, right.QtyINReceipts));
    local8.QtyINReceipts = nullable8;
    ref T local9 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local9;
      local9 = ref obj;
    }
    Decimal? nullable9 = new Decimal?(opSafe(left.QtyInTransit, right.QtyInTransit));
    local9.QtyInTransit = nullable9;
    ref T local10 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local10;
      local10 = ref obj;
    }
    Decimal? nullable10 = new Decimal?(opSafe(left.QtyPOPrepared, right.QtyPOPrepared));
    local10.QtyPOPrepared = nullable10;
    ref T local11 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local11;
      local11 = ref obj;
    }
    Decimal? nullable11 = new Decimal?(opSafe(left.QtyPOOrders, right.QtyPOOrders));
    local11.QtyPOOrders = nullable11;
    ref T local12 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local12;
      local12 = ref obj;
    }
    Decimal? nullable12 = new Decimal?(opSafe(left.QtyPOReceipts, right.QtyPOReceipts));
    local12.QtyPOReceipts = nullable12;
    ref T local13 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local13;
      local13 = ref obj;
    }
    Decimal? nullable13 = new Decimal?(opSafe(left.QtyFSSrvOrdPrepared, right.QtyFSSrvOrdPrepared));
    local13.QtyFSSrvOrdPrepared = nullable13;
    ref T local14 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local14;
      local14 = ref obj;
    }
    Decimal? nullable14 = new Decimal?(opSafe(left.QtyFSSrvOrdBooked, right.QtyFSSrvOrdBooked));
    local14.QtyFSSrvOrdBooked = nullable14;
    ref T local15 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local15;
      local15 = ref obj;
    }
    Decimal? nullable15 = new Decimal?(opSafe(left.QtyFSSrvOrdAllocated, right.QtyFSSrvOrdAllocated));
    local15.QtyFSSrvOrdAllocated = nullable15;
    ref T local16 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local16;
      local16 = ref obj;
    }
    Decimal? nullable16 = new Decimal?(opSafe(left.QtySOBackOrdered, right.QtySOBackOrdered));
    local16.QtySOBackOrdered = nullable16;
    ref T local17 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local17;
      local17 = ref obj;
    }
    Decimal? nullable17 = new Decimal?(opSafe(left.QtySOPrepared, right.QtySOPrepared));
    local17.QtySOPrepared = nullable17;
    ref T local18 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local18;
      local18 = ref obj;
    }
    Decimal? nullable18 = new Decimal?(opSafe(left.QtySOBooked, right.QtySOBooked));
    local18.QtySOBooked = nullable18;
    ref T local19 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local19;
      local19 = ref obj;
    }
    Decimal? nullable19 = new Decimal?(opSafe(left.QtySOShipped, right.QtySOShipped));
    local19.QtySOShipped = nullable19;
    ref T local20 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local20;
      local20 = ref obj;
    }
    Decimal? nullable20 = new Decimal?(opSafe(left.QtySOShipping, right.QtySOShipping));
    local20.QtySOShipping = nullable20;
    ref T local21 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local21;
      local21 = ref obj;
    }
    Decimal? nullable21 = new Decimal?(opSafe(left.QtyINAssemblySupply, right.QtyINAssemblySupply));
    local21.QtyINAssemblySupply = nullable21;
    ref T local22 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local22;
      local22 = ref obj;
    }
    Decimal? nullable22 = new Decimal?(opSafe(left.QtyINAssemblyDemand, right.QtyINAssemblyDemand));
    local22.QtyINAssemblyDemand = nullable22;
    ref T local23 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local23;
      local23 = ref obj;
    }
    Decimal? nullable23 = new Decimal?(opSafe(left.QtyInTransitToProduction, right.QtyInTransitToProduction));
    local23.QtyInTransitToProduction = nullable23;
    ref T local24 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local24;
      local24 = ref obj;
    }
    Decimal? nullable24 = new Decimal?(opSafe(left.QtyProductionSupplyPrepared, right.QtyProductionSupplyPrepared));
    local24.QtyProductionSupplyPrepared = nullable24;
    ref T local25 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local25;
      local25 = ref obj;
    }
    Decimal? nullable25 = new Decimal?(opSafe(left.QtyProductionSupply, right.QtyProductionSupply));
    local25.QtyProductionSupply = nullable25;
    ref T local26 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local26;
      local26 = ref obj;
    }
    Decimal? nullable26 = new Decimal?(opSafe(left.QtyPOFixedProductionPrepared, right.QtyPOFixedProductionPrepared));
    local26.QtyPOFixedProductionPrepared = nullable26;
    ref T local27 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local27;
      local27 = ref obj;
    }
    Decimal? nullable27 = new Decimal?(opSafe(left.QtyPOFixedProductionOrders, right.QtyPOFixedProductionOrders));
    local27.QtyPOFixedProductionOrders = nullable27;
    ref T local28 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local28;
      local28 = ref obj;
    }
    Decimal? nullable28 = new Decimal?(opSafe(left.QtyProductionDemandPrepared, right.QtyProductionDemandPrepared));
    local28.QtyProductionDemandPrepared = nullable28;
    ref T local29 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local29;
      local29 = ref obj;
    }
    Decimal? nullable29 = new Decimal?(opSafe(left.QtyProductionDemand, right.QtyProductionDemand));
    local29.QtyProductionDemand = nullable29;
    ref T local30 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local30;
      local30 = ref obj;
    }
    Decimal? nullable30 = new Decimal?(opSafe(left.QtyProductionAllocated, right.QtyProductionAllocated));
    local30.QtyProductionAllocated = nullable30;
    ref T local31 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local31;
      local31 = ref obj;
    }
    Decimal? nullable31 = new Decimal?(opSafe(left.QtySOFixedProduction, right.QtySOFixedProduction));
    local31.QtySOFixedProduction = nullable31;
    ref T local32 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local32;
      local32 = ref obj;
    }
    Decimal? nullable32 = new Decimal?(opSafe(left.QtyProdFixedPurchase, right.QtyProdFixedPurchase));
    local32.QtyProdFixedPurchase = nullable32;
    ref T local33 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local33;
      local33 = ref obj;
    }
    Decimal? nullable33 = new Decimal?(opSafe(left.QtyProdFixedProduction, right.QtyProdFixedProduction));
    local33.QtyProdFixedProduction = nullable33;
    ref T local34 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local34;
      local34 = ref obj;
    }
    Decimal? nullable34 = new Decimal?(opSafe(left.QtyProdFixedProdOrdersPrepared, right.QtyProdFixedProdOrdersPrepared));
    local34.QtyProdFixedProdOrdersPrepared = nullable34;
    ref T local35 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local35;
      local35 = ref obj;
    }
    Decimal? nullable35 = new Decimal?(opSafe(left.QtyProdFixedProdOrders, right.QtyProdFixedProdOrders));
    local35.QtyProdFixedProdOrders = nullable35;
    ref T local36 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local36;
      local36 = ref obj;
    }
    Decimal? nullable36 = new Decimal?(opSafe(left.QtyProdFixedSalesOrdersPrepared, right.QtyProdFixedSalesOrdersPrepared));
    local36.QtyProdFixedSalesOrdersPrepared = nullable36;
    ref T local37 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local37;
      local37 = ref obj;
    }
    Decimal? nullable37 = new Decimal?(opSafe(left.QtyProdFixedSalesOrders, right.QtyProdFixedSalesOrders));
    local37.QtyProdFixedSalesOrders = nullable37;
    ref T local38 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local38;
      local38 = ref obj;
    }
    Decimal? nullable38 = new Decimal?(opSafe(left.QtyFixedFSSrvOrd, right.QtyFixedFSSrvOrd));
    local38.QtyFixedFSSrvOrd = nullable38;
    ref T local39 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local39;
      local39 = ref obj;
    }
    Decimal? nullable39 = new Decimal?(opSafe(left.QtyPOFixedFSSrvOrd, right.QtyPOFixedFSSrvOrd));
    local39.QtyPOFixedFSSrvOrd = nullable39;
    ref T local40 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local40;
      local40 = ref obj;
    }
    Decimal? nullable40 = new Decimal?(opSafe(left.QtyPOFixedFSSrvOrdPrepared, right.QtyPOFixedFSSrvOrdPrepared));
    local40.QtyPOFixedFSSrvOrdPrepared = nullable40;
    ref T local41 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local41;
      local41 = ref obj;
    }
    Decimal? nullable41 = new Decimal?(opSafe(left.QtyPOFixedFSSrvOrdReceipts, right.QtyPOFixedFSSrvOrdReceipts));
    local41.QtyPOFixedFSSrvOrdReceipts = nullable41;
    ref T local42 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local42;
      local42 = ref obj;
    }
    Decimal? nullable42 = new Decimal?(opSafe(left.QtySOFixed, right.QtySOFixed));
    local42.QtySOFixed = nullable42;
    ref T local43 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local43;
      local43 = ref obj;
    }
    Decimal? nullable43 = new Decimal?(opSafe(left.QtyPOFixedOrders, right.QtyPOFixedOrders));
    local43.QtyPOFixedOrders = nullable43;
    ref T local44 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local44;
      local44 = ref obj;
    }
    Decimal? nullable44 = new Decimal?(opSafe(left.QtyPOFixedPrepared, right.QtyPOFixedPrepared));
    local44.QtyPOFixedPrepared = nullable44;
    ref T local45 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local45;
      local45 = ref obj;
    }
    Decimal? nullable45 = new Decimal?(opSafe(left.QtyPOFixedReceipts, right.QtyPOFixedReceipts));
    local45.QtyPOFixedReceipts = nullable45;
    ref T local46 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local46;
      local46 = ref obj;
    }
    Decimal? nullable46 = new Decimal?(opSafe(left.QtySODropShip, right.QtySODropShip));
    local46.QtySODropShip = nullable46;
    ref T local47 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local47;
      local47 = ref obj;
    }
    Decimal? nullable47 = new Decimal?(opSafe(left.QtyPODropShipOrders, right.QtyPODropShipOrders));
    local47.QtyPODropShipOrders = nullable47;
    ref T local48 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local48;
      local48 = ref obj;
    }
    Decimal? nullable48 = new Decimal?(opSafe(left.QtyPODropShipPrepared, right.QtyPODropShipPrepared));
    local48.QtyPODropShipPrepared = nullable48;
    ref T local49 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local49;
      local49 = ref obj;
    }
    Decimal? nullable49 = new Decimal?(opSafe(left.QtyPODropShipReceipts, right.QtyPODropShipReceipts));
    local49.QtyPODropShipReceipts = nullable49;
    ref T local50 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local50;
      local50 = ref obj;
    }
    Decimal? nullable50 = new Decimal?(opSafe(left.QtyInTransitToSO, right.QtyInTransitToSO));
    local50.QtyInTransitToSO = nullable50;
    ref T local51 = ref result;
    obj = default (T);
    if ((object) obj == null)
    {
      obj = local51;
      local51 = ref obj;
    }
    Decimal? nullable51 = new Decimal?(opSafe(left.QtyINReplaned, right.QtyINReplaned));
    local51.QtyINReplaned = nullable51;
    if (result is ICostStatus costStatus1 && left is ICostStatus costStatus2 && right is ICostStatus costStatus3)
    {
      Decimal? nullable52 = new Decimal?(operation(costStatus2.TotalCost.GetValueOrDefault(), costStatus3.TotalCost.GetValueOrDefault()));
      costStatus1.TotalCost = nullable52;
    }
    return result;

    Decimal opSafe(Decimal? l, Decimal? r)
    {
      return Math.Round(operation(l.GetValueOrDefault(), r.GetValueOrDefault()), CommonSetupDecPl.Qty, MidpointRounding.AwayFromZero);
    }
  }
}
