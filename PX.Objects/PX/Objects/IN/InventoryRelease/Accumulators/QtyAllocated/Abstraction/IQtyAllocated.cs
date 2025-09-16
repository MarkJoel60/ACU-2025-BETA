// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction.IQtyAllocated
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction;

public interface IQtyAllocated : IQtyAllocatedBase
{
  Decimal? QtyINIssues { get; set; }

  Decimal? QtyINReceipts { get; set; }

  Decimal? QtyPOPrepared { get; set; }

  Decimal? QtyPOOrders { get; set; }

  Decimal? QtyPOReceipts { get; set; }

  Decimal? QtyFSSrvOrdPrepared { get; set; }

  Decimal? QtyFSSrvOrdBooked { get; set; }

  Decimal? QtyFSSrvOrdAllocated { get; set; }

  Decimal? QtySOBackOrdered { get; set; }

  Decimal? QtySOPrepared { get; set; }

  Decimal? QtySOBooked { get; set; }

  Decimal? QtySOShipped { get; set; }

  Decimal? QtySOShipping { get; set; }

  Decimal? QtyINAssemblySupply { get; set; }

  Decimal? QtyINAssemblyDemand { get; set; }

  Decimal? QtyInTransitToProduction { get; set; }

  Decimal? QtyProductionSupplyPrepared { get; set; }

  Decimal? QtyProductionSupply { get; set; }

  Decimal? QtyPOFixedProductionPrepared { get; set; }

  Decimal? QtyPOFixedProductionOrders { get; set; }

  Decimal? QtyProductionDemandPrepared { get; set; }

  Decimal? QtyProductionDemand { get; set; }

  Decimal? QtyProductionAllocated { get; set; }

  Decimal? QtySOFixedProduction { get; set; }

  Decimal? QtyProdFixedPurchase { get; set; }

  Decimal? QtyProdFixedProduction { get; set; }

  Decimal? QtyProdFixedProdOrdersPrepared { get; set; }

  Decimal? QtyProdFixedProdOrders { get; set; }

  Decimal? QtyProdFixedSalesOrdersPrepared { get; set; }

  Decimal? QtyProdFixedSalesOrders { get; set; }

  Decimal? QtyINReplaned { get; set; }

  Decimal? QtyFixedFSSrvOrd { get; set; }

  Decimal? QtyPOFixedFSSrvOrd { get; set; }

  Decimal? QtyPOFixedFSSrvOrdPrepared { get; set; }

  Decimal? QtyPOFixedFSSrvOrdReceipts { get; set; }

  Decimal? QtySOFixed { get; set; }

  Decimal? QtyPOFixedOrders { get; set; }

  Decimal? QtyPOFixedPrepared { get; set; }

  Decimal? QtyPOFixedReceipts { get; set; }

  Decimal? QtySODropShip { get; set; }

  Decimal? QtyPODropShipOrders { get; set; }

  Decimal? QtyPODropShipPrepared { get; set; }

  Decimal? QtyPODropShipReceipts { get; set; }

  Decimal? QtyInTransitToSO { get; set; }
}
