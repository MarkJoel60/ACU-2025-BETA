// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCosts.PurchasePriceVarianceAllocationService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO.LandedCosts;

public class PurchasePriceVarianceAllocationService : AllocationServiceBase
{
  public static PurchasePriceVarianceAllocationService Instance
  {
    get
    {
      return PXContext.GetSlot<PurchasePriceVarianceAllocationService>() ?? PXContext.SetSlot<PurchasePriceVarianceAllocationService>(PXGraph.CreateInstance<PurchasePriceVarianceAllocationService>());
    }
  }

  public virtual Decimal AllocateOverRCTLine(
    PXGraph graph,
    List<AllocationServiceBase.POReceiptLineAdjustment> result,
    POReceiptLine aLine,
    Decimal toDistribute,
    int? branchID)
  {
    AllocationServiceBase.AllocationItem allocationItem = new AllocationServiceBase.AllocationItem()
    {
      LandedCostCode = new LandedCostCode(),
      ReceiptLine = aLine
    };
    allocationItem.LandedCostCode.AllocationMethod = "Q";
    return this.AllocateOverRCTLine(graph, result, allocationItem, toDistribute, branchID);
  }
}
