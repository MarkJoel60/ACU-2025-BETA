// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction.IQtyAllocatedBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction;

public interface IQtyAllocatedBase
{
  bool? NegQty { get; }

  bool? InclQtyAvail { get; }

  bool? InclQtyFSSrvOrdPrepared { get; }

  bool? InclQtyFSSrvOrdBooked { get; }

  bool? InclQtyFSSrvOrdAllocated { get; }

  bool? InclQtySOReverse { get; }

  bool? InclQtySOBackOrdered { get; }

  bool? InclQtySOPrepared { get; }

  bool? InclQtySOBooked { get; }

  bool? InclQtySOShipped { get; }

  bool? InclQtySOShipping { get; }

  bool? InclQtyPOPrepared { get; }

  bool? InclQtyPOOrders { get; }

  bool? InclQtyFixedSOPO { get; }

  bool? InclQtyPOReceipts { get; }

  bool? InclQtyInTransit { get; }

  bool? InclQtyINIssues { get; }

  bool? InclQtyINReceipts { get; }

  bool? InclQtyPOFixedReceipt { get; }

  bool? InclQtyINAssemblySupply { get; }

  bool? InclQtyINAssemblyDemand { get; }

  bool? InclQtyProductionDemandPrepared { get; }

  bool? InclQtyProductionDemand { get; }

  bool? InclQtyProductionAllocated { get; }

  bool? InclQtyProductionSupplyPrepared { get; }

  bool? InclQtyProductionSupply { get; }

  Decimal? QtyOnHand { get; set; }

  Decimal? QtyAvail { get; set; }

  Decimal? QtyHardAvail { get; set; }

  Decimal? QtyActual { get; set; }

  Decimal? QtyNotAvail { get; set; }

  Decimal? QtyInTransit { get; set; }
}
