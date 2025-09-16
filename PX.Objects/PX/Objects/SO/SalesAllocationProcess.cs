// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SalesAllocationProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using PX.Objects.SO.GraphExtensions.SOOrderEntryExt;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

public class SalesAllocationProcess : PXGraph<SalesAllocationProcess>
{
  protected Dictionary<SalesAllocation, Exception> ProcessingErrors;

  private INSiteStatusByCostCenter FetchSiteStatus(
    PXGraph graph,
    INSiteStatusByCostCenter available)
  {
    return this.FetchStatus<INSiteStatusByCostCenter, PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>(graph, available);
  }

  private TStatus FetchStatus<TStatus, TStatusAccum>(PXGraph graph, TStatus status)
    where TStatus : IStatus, IBqlTable, new()
    where TStatusAccum : IStatus, IBqlTable, new()
  {
    PXCache cach = graph.Caches[typeof (TStatus)];
    status = (TStatus) cach.CreateCopy((object) status);
    TStatusAccum statusAccum = new TStatusAccum();
    cach.RestoreCopy((object) statusAccum, (object) status);
    TStatusAccum other = (TStatusAccum) graph.Caches[typeof (TStatusAccum)].Insert((object) statusAccum);
    status = status.Add<TStatus>((IStatus) other);
    return status;
  }

  public virtual void AllocateOrders(List<SalesAllocation> allocations)
  {
    this.ProcessAllocations(allocations.Where<SalesAllocation>((Func<SalesAllocation, bool>) (x =>
    {
      Decimal? qtyToAllocate = x.QtyToAllocate;
      Decimal num = 0M;
      return qtyToAllocate.GetValueOrDefault() > num & qtyToAllocate.HasValue;
    })).ToList<SalesAllocation>(), new SalesAllocationProcess.ValidateOrder(this.ValidateOrderToAllocate), new SalesAllocationProcess.ValidateLine(this.ValidateLineToAllocate), new SalesAllocationProcess.ProcessLine(this.Allocate));
  }

  public virtual void DeallocateOrders(List<SalesAllocation> allocations)
  {
    this.ProcessAllocations(allocations.Where<SalesAllocation>((Func<SalesAllocation, bool>) (x =>
    {
      Decimal? qtyToDeallocate = x.QtyToDeallocate;
      Decimal num = 0M;
      return qtyToDeallocate.GetValueOrDefault() > num & qtyToDeallocate.HasValue;
    })).ToList<SalesAllocation>(), new SalesAllocationProcess.ValidateOrder(this.ValidateOrderToDeallocate), new SalesAllocationProcess.ValidateLine(this.ValidateLineToDeallocate), new SalesAllocationProcess.ProcessLine(this.Deallocate));
  }

  protected void SetProcessingError(SalesAllocation allocation, Exception error)
  {
    if (this.ProcessingErrors == null)
      return;
    if (this.ProcessingErrors.ContainsKey(allocation))
      this.ProcessingErrors.Remove(allocation);
    this.ProcessingErrors.Add(allocation, error);
  }

  protected virtual void OnBeforeProcess()
  {
    this.ProcessingErrors = new Dictionary<SalesAllocation, Exception>();
  }

  protected virtual void OnAfterProcess()
  {
    this.ProcessingErrors = (Dictionary<SalesAllocation, Exception>) null;
  }

  protected virtual void ProcessAllocations(
    List<SalesAllocation> allocations,
    SalesAllocationProcess.ValidateOrder validateOrder,
    SalesAllocationProcess.ValidateLine validateLine,
    SalesAllocationProcess.ProcessLine processLine)
  {
    this.OnBeforeProcess();
    IEnumerable<IGrouping<(string, string), SalesAllocation>> groupings = allocations.GroupBy<SalesAllocation, (string, string)>((Func<SalesAllocation, (string, string)>) (x => (x.OrderType, x.OrderNbr)));
    SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
    try
    {
      foreach (IGrouping<(string, string), SalesAllocation> source in groupings)
      {
        SOOrder order = new SOOrder()
        {
          OrderType = source.Key.Item1,
          OrderNbr = source.Key.Item2
        };
        this.ProcessAllocations(instance, order, source.ToList<SalesAllocation>(), validateOrder, validateLine, processLine);
      }
      Dictionary<SalesAllocation, Exception> processingErrors = this.ProcessingErrors;
      if ((processingErrors != null ? (processingErrors.Any<KeyValuePair<SalesAllocation, Exception>>() ? 1 : 0) : 0) != 0)
        throw new PXOperationCompletedWithErrorException();
    }
    finally
    {
      this.OnAfterProcess();
    }
  }

  protected virtual bool ProcessAllocations(
    SOOrderEntry graph,
    SOOrder order,
    List<SalesAllocation> allocations,
    SalesAllocationProcess.ValidateOrder validateOrder,
    SalesAllocationProcess.ValidateLine validateLine,
    SalesAllocationProcess.ProcessLine processLine)
  {
    ((PXGraph) graph).Clear();
    Exception error = (Exception) null;
    bool flag = false;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      ((PXSelectBase<SOOrder>) graph.Document).Current = PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) graph.Document).Search<SOOrder.orderNbr>((object) order.OrderNbr, new object[1]
      {
        (object) order.OrderType
      }));
      if (validateOrder(graph, ((PXSelectBase<SOOrder>) graph.Document).Current, allocations[0], out error))
      {
        foreach (SalesAllocation allocation in allocations)
        {
          if (this.ProcessAllocation(graph, allocation, validateLine, processLine))
            flag = ((flag ? 1 : 0) | 1) != 0;
        }
        if (flag)
        {
          try
          {
            ((PXAction) graph.Save).Press();
          }
          catch (Exception ex)
          {
            error = ex;
            flag = false;
          }
        }
      }
      if (flag)
        transactionScope.Complete();
    }
    if (error == null)
      return flag;
    foreach (SalesAllocation allocation in allocations)
    {
      PXProcessing<SalesAllocation>.SetCurrentItem((object) allocation);
      PXProcessing<SalesAllocation>.SetError(error);
      this.SetProcessingError(allocation, error);
    }
    return false;
  }

  protected virtual bool ProcessAllocation(
    SOOrderEntry graph,
    SalesAllocation allocation,
    SalesAllocationProcess.ValidateLine validateLine,
    SalesAllocationProcess.ProcessLine processLine)
  {
    PXProcessing<SalesAllocation>.SetCurrentItem((object) allocation);
    try
    {
      ((PXSelectBase<SOLine>) graph.Transactions).Current = PXResultset<SOLine>.op_Implicit(((PXSelectBase<SOLine>) graph.Transactions).Search<SOLine.lineNbr>((object) allocation.LineNbr, Array.Empty<object>()));
      Exception error;
      if (!validateLine(graph, ((PXSelectBase<SOLine>) graph.Transactions).Current, allocation, out error))
        throw error;
      Decimal? nullable = processLine(graph, ((PXSelectBase<SOLine>) graph.Transactions).Current, allocation);
    }
    catch (Exception ex)
    {
      PXProcessing<SalesAllocation>.SetError(ex);
      this.SetProcessingError(allocation, ex);
      return false;
    }
    PXProcessing<SalesAllocation>.SetProcessed();
    return true;
  }

  protected virtual bool ValidateOrderToAllocate(
    SOOrderEntry graph,
    SOOrder order,
    SalesAllocation allocation,
    out Exception error)
  {
    error = (Exception) null;
    if (order == null)
      error = (Exception) new PXException("Another process has deleted the '{0}' record. Your changes will be lost.", new object[1]
      {
        (object) ((PXSelectBase<SOOrder>) graph.Document).GetItemType().Name
      });
    else if (!SetOfConstants<string, Equal<IBqlOperand>, SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<SOOrderStatus.hold, SOOrderStatus.creditHold, SOOrderStatus.awaitingPayment, SOOrderStatus.pendingProcessing, SOOrderStatus.open, SOOrderStatus.backOrder>>.Provider>.ContainsValue(order.Status) || !order.Hold.GetValueOrDefault() && !order.DontApprove.GetValueOrDefault())
      error = (Exception) new PXException("Another process has updated the '{0}' record. Your changes will be lost.", new object[1]
      {
        (object) ((PXSelectBase<SOOrder>) graph.Document).GetItemType().Name
      });
    return error == null;
  }

  protected virtual bool ValidateLineToAllocate(
    SOOrderEntry graph,
    SOLine line,
    SalesAllocation allocation,
    out Exception error)
  {
    error = (Exception) null;
    if (line == null)
    {
      error = (Exception) new PXException("Another process has deleted the '{0}' record. Your changes will be lost.", new object[1]
      {
        (object) ((PXSelectBase<SOLine>) graph.Transactions).GetItemType().Name
      });
    }
    else
    {
      int? siteId = line.SiteID;
      int? lineSiteId = allocation.LineSiteID;
      if (siteId.GetValueOrDefault() == lineSiteId.GetValueOrDefault() & siteId.HasValue == lineSiteId.HasValue)
      {
        int? inventoryId1 = line.InventoryID;
        int? inventoryId2 = allocation.InventoryID;
        if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
        {
          short? invtMult1 = line.InvtMult;
          int? nullable1 = invtMult1.HasValue ? new int?((int) invtMult1.GetValueOrDefault()) : new int?();
          int num = 0;
          int? nullable2;
          if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
          {
            short? invtMult2 = line.InvtMult;
            if (!invtMult2.HasValue)
            {
              nullable1 = new int?();
              nullable2 = nullable1;
            }
            else
              nullable2 = new int?((int) -invtMult2.GetValueOrDefault());
          }
          else
            nullable2 = new int?(1);
          int? nullable3 = nullable2;
          Decimal? nullable4 = nullable3.HasValue ? new Decimal?((Decimal) nullable3.GetValueOrDefault()) : new Decimal?();
          Decimal? baseOrderQty = line.BaseOrderQty;
          Decimal? nullable5 = nullable4.HasValue & baseOrderQty.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * baseOrderQty.GetValueOrDefault()) : new Decimal?();
          Decimal? qtyToAllocate = allocation.QtyToAllocate;
          if (nullable5.GetValueOrDefault() < qtyToAllocate.GetValueOrDefault() & nullable5.HasValue & qtyToAllocate.HasValue)
          {
            error = (Exception) new PXException("The quantity to allocate cannot be greater than the unallocated quantity.");
            goto label_12;
          }
          goto label_12;
        }
      }
      error = (Exception) new PXException("Another process has updated the '{0}' record. Your changes will be lost.", new object[1]
      {
        (object) ((PXSelectBase<SOLine>) graph.Transactions).GetItemType().Name
      });
    }
label_12:
    return error == null;
  }

  public virtual Decimal? Allocate(SOOrderEntry graph, SOLine line, SalesAllocation allocation)
  {
    if (this.ReadLotSerClass((PXGraph) graph, line.InventoryID) == null || !graph.LineSplittingAllocatedExt.IsAllocationEntryEnabled)
      return new Decimal?(0M);
    Decimal? qtyToAllocate = allocation.QtyToAllocate;
    Decimal? nullable1 = ((IEnumerable<SOLineSplit>) this.SelectUnallocatedSplits(graph, line)).Sum<SOLineSplit>((Func<SOLineSplit, Decimal?>) (x => x.BaseQty));
    Decimal? nullable2 = qtyToAllocate;
    if (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
      throw new PXException("The quantity to allocate cannot be greater than the unallocated quantity.");
    INSiteStatusByCostCenter available = INSiteStatusByCostCenter.PK.Find((PXGraph) graph, line.InventoryID, line.SubItemID, line.SiteID, line.CostCenterID);
    nullable2 = this.FetchSiteStatus((PXGraph) graph, available).QtyHardAvail;
    nullable1 = qtyToAllocate;
    if (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
      throw new PXException("The {0} item cannot be allocated because the quantity to allocate is greater than the quantity available for allocation.", new object[1]
      {
        ((PXSelectBase) graph.Transactions).Cache.GetValueExt<SOLine.inventoryID>((object) line)
      });
    return this.AllocateNonLots(graph, line, qtyToAllocate);
  }

  protected virtual Decimal? AllocateNonLots(
    SOOrderEntry graph,
    SOLine line,
    Decimal? qtyToAllocate)
  {
    Decimal? qtyToAllocate1 = qtyToAllocate;
    Queue<SOLineSplit> queue = EnumerableExtensions.ToQueue<SOLineSplit>((IEnumerable<SOLineSplit>) this.SelectUnallocatedSplits(graph, line));
    Decimal? nullable1 = new Decimal?(0M);
    while (true)
    {
      Decimal? nullable2 = qtyToAllocate1;
      Decimal num = 0M;
      if (nullable2.GetValueOrDefault() > num & nullable2.HasValue && queue.Any<SOLineSplit>())
      {
        SOLineSplit splitToAllocate = queue.Dequeue();
        Decimal? nullable3 = this.AllocateNonLots(graph, splitToAllocate, qtyToAllocate1);
        nullable2 = nullable1;
        Decimal? nullable4 = nullable3;
        nullable1 = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
        nullable4 = qtyToAllocate1;
        nullable2 = nullable3;
        qtyToAllocate1 = nullable4.HasValue & nullable2.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      }
      else
        break;
    }
    return nullable1;
  }

  protected virtual Decimal? AllocateNonLots(
    SOOrderEntry graph,
    SOLineSplit splitToAllocate,
    Decimal? qtyToAllocate)
  {
    SOLineSplit copy = PXCache<SOLineSplit>.CreateCopy(splitToAllocate);
    Decimal? baseQty1 = copy.BaseQty;
    Decimal? nullable1 = qtyToAllocate;
    if (baseQty1.GetValueOrDefault() <= nullable1.GetValueOrDefault() & baseQty1.HasValue & nullable1.HasValue)
    {
      copy.IsAllocated = new bool?(true);
      return ((SOLineSplit) ((PXSelectBase) graph.splits).Cache.Update((object) copy)).BaseQty;
    }
    splitToAllocate.IsAllocated = new bool?(true);
    GraphHelper.MarkUpdated(((PXSelectBase) graph.splits).Cache, (object) splitToAllocate, true);
    SOOrderLineSplittingAllocatedExtension splittingAllocatedExt = graph.LineSplittingAllocatedExt;
    SOLineSplit split = splitToAllocate;
    Decimal? nullable2 = qtyToAllocate;
    Decimal? baseQty2 = splitToAllocate.BaseQty;
    Decimal? negQtyHardAvail = nullable2.HasValue & baseQty2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - baseQty2.GetValueOrDefault()) : new Decimal?();
    SOLineSplit splitCopy = copy;
    splittingAllocatedExt.BreakupAllocatedSplit(split, negQtyHardAvail, false, splitCopy);
    return qtyToAllocate;
  }

  protected virtual SOLineSplit[] SelectUnallocatedSplits(SOOrderEntry graph, SOLine line)
  {
    SOOrderLineSplittingAllocatedExtension splittingAllocatedExt = graph.LineSplittingAllocatedExt;
    return PXParentAttribute.SelectChildren(((PXSelectBase) graph.splits).Cache, (object) line, ((PXSelectBase<SOLine>) graph.Transactions).GetItemType()).Cast<SOLineSplit>().Where<SOLineSplit>((Func<SOLineSplit, bool>) (s =>
    {
      bool? isAllocated = s.IsAllocated;
      bool flag = false;
      return isAllocated.GetValueOrDefault() == flag & isAllocated.HasValue && s.POReceiptNbr == null && splittingAllocatedExt.AllowToManualAllocate(line, s);
    })).ToArray<SOLineSplit>();
  }

  protected virtual bool ValidateOrderToDeallocate(
    SOOrderEntry graph,
    SOOrder order,
    SalesAllocation allocation,
    out Exception error)
  {
    error = (Exception) null;
    if (order == null)
      error = (Exception) new PXException("Another process has deleted the '{0}' record. Your changes will be lost.", new object[1]
      {
        (object) ((PXSelectBase<SOOrder>) graph.Document).GetItemType().Name
      });
    else if (!SetOfConstants<string, Equal<IBqlOperand>, SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<SOOrderStatus.hold, SOOrderStatus.creditHold, SOOrderStatus.awaitingPayment, SOOrderStatus.pendingProcessing, SOOrderStatus.open, SOOrderStatus.backOrder, SOOrderStatus.expired>>.Provider>.ContainsValue(order.Status) || !order.Hold.GetValueOrDefault() && !order.DontApprove.GetValueOrDefault())
      error = (Exception) new PXException("Another process has updated the '{0}' record. Your changes will be lost.", new object[1]
      {
        (object) ((PXSelectBase<SOOrder>) graph.Document).GetItemType().Name
      });
    return error == null;
  }

  protected virtual bool ValidateLineToDeallocate(
    SOOrderEntry graph,
    SOLine line,
    SalesAllocation allocation,
    out Exception error)
  {
    error = (Exception) null;
    if (line == null)
    {
      error = (Exception) new PXException("Another process has deleted the '{0}' record. Your changes will be lost.", new object[1]
      {
        (object) ((PXSelectBase<SOLine>) graph.Transactions).GetItemType().Name
      });
    }
    else
    {
      int? siteId = line.SiteID;
      int? lineSiteId = allocation.LineSiteID;
      if (siteId.GetValueOrDefault() == lineSiteId.GetValueOrDefault() & siteId.HasValue == lineSiteId.HasValue)
      {
        int? inventoryId1 = line.InventoryID;
        int? inventoryId2 = allocation.InventoryID;
        if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
        {
          short? invtMult1 = line.InvtMult;
          int? nullable1 = invtMult1.HasValue ? new int?((int) invtMult1.GetValueOrDefault()) : new int?();
          int num = 0;
          int? nullable2;
          if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
          {
            short? invtMult2 = line.InvtMult;
            if (!invtMult2.HasValue)
            {
              nullable1 = new int?();
              nullable2 = nullable1;
            }
            else
              nullable2 = new int?((int) -invtMult2.GetValueOrDefault());
          }
          else
            nullable2 = new int?(1);
          int? nullable3 = nullable2;
          Decimal? nullable4 = nullable3.HasValue ? new Decimal?((Decimal) nullable3.GetValueOrDefault()) : new Decimal?();
          Decimal? baseOrderQty = line.BaseOrderQty;
          Decimal? nullable5 = nullable4.HasValue & baseOrderQty.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * baseOrderQty.GetValueOrDefault()) : new Decimal?();
          Decimal? qtyToDeallocate = allocation.QtyToDeallocate;
          if (nullable5.GetValueOrDefault() < qtyToDeallocate.GetValueOrDefault() & nullable5.HasValue & qtyToDeallocate.HasValue)
          {
            error = (Exception) new PXException("The quantity to deallocate cannot be greater than the allocated quantity.");
            goto label_12;
          }
          goto label_12;
        }
      }
      error = (Exception) new PXException("Another process has updated the '{0}' record. Your changes will be lost.", new object[1]
      {
        (object) ((PXSelectBase<SOLine>) graph.Transactions).GetItemType().Name
      });
    }
label_12:
    return error == null;
  }

  public virtual Decimal? Deallocate(SOOrderEntry graph, SOLine line, SalesAllocation allocation)
  {
    INLotSerClass inLotSerClass = this.ReadLotSerClass((PXGraph) graph, line.InventoryID);
    if (inLotSerClass == null || !graph.LineSplittingAllocatedExt.IsAllocationEntryEnabled)
      return new Decimal?(0M);
    int? splitSiteId = allocation.SplitSiteID;
    Decimal? qtyToDeallocate = allocation.QtyToDeallocate;
    Decimal? nullable1 = ((IEnumerable<SOLineSplit>) this.SelectAllocatedSplits(graph, line, splitSiteId)).Sum<SOLineSplit>((Func<SOLineSplit, Decimal?>) (x => x.BaseQty));
    Decimal? nullable2 = qtyToDeallocate;
    Decimal? nullable3 = nullable1;
    if (nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue)
      throw new PXException("The quantity to deallocate cannot be greater than the allocated quantity.");
    if (((PXSelectBase<SOOrder>) graph.Document).Current.IsExpired.GetValueOrDefault())
    {
      nullable3 = qtyToDeallocate;
      nullable2 = nullable1;
      if (nullable3.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable3.HasValue & nullable2.HasValue)
        throw new PXException("The quantity to deallocate cannot be changed for lines of expired blanket sales orders.");
    }
    if (!graph.LineSplittingExt.UseBaseUnitInSplit(SOLineSplit.FromSOLine(line), line, new PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>(PX.Objects.IN.InventoryItem.PK.Find((PXGraph) graph, line.InventoryID), inLotSerClass)))
      qtyToDeallocate = new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) graph.Transactions).Cache, line.InventoryID, line.UOM, qtyToDeallocate.GetValueOrDefault(), INPrecision.QUANTITY));
    using (graph.LineSplittingExt.SuppressedModeScope(true))
    {
      if (inLotSerClass.LotSerTrack == "N" || inLotSerClass.LotSerAssign == "U")
      {
        nullable2 = this.DeallocateNonLots(graph, line, splitSiteId, qtyToDeallocate);
        return nullable2;
      }
      nullable2 = this.DeallocateLots(graph, line, splitSiteId, qtyToDeallocate);
      return nullable2;
    }
  }

  protected virtual Decimal? DeallocateNonLots(
    SOOrderEntry graph,
    SOLine line,
    int? siteID,
    Decimal? qtyToDeallocate)
  {
    Decimal? qtyToDeallocate1 = qtyToDeallocate;
    Queue<SOLineSplit> queue = EnumerableExtensions.ToQueue<SOLineSplit>((IEnumerable<SOLineSplit>) ((IEnumerable<SOLineSplit>) this.SelectAllocatedSplits(graph, line, siteID)).OrderByDescending<SOLineSplit, int?>((Func<SOLineSplit, int?>) (s => s.SplitLineNbr)));
    Decimal? nullable1 = new Decimal?(0M);
    while (true)
    {
      Decimal? nullable2 = qtyToDeallocate1;
      Decimal num = 0M;
      if (nullable2.GetValueOrDefault() > num & nullable2.HasValue && queue.Any<SOLineSplit>())
      {
        SOLineSplit splitToDeallocate = queue.Dequeue();
        Decimal? nullable3 = this.DeallocateNonLots(graph, splitToDeallocate, qtyToDeallocate1);
        nullable2 = nullable1;
        Decimal? nullable4 = nullable3;
        nullable1 = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
        nullable4 = qtyToDeallocate1;
        nullable2 = nullable3;
        qtyToDeallocate1 = nullable4.HasValue & nullable2.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      }
      else
        break;
    }
    return nullable1;
  }

  protected virtual Decimal? DeallocateNonLots(
    SOOrderEntry graph,
    SOLineSplit splitToDeallocate,
    Decimal? qtyToDeallocate)
  {
    PXCache cache = ((PXSelectBase) graph.splits).Cache;
    SOLineSplit split = (SOLineSplit) cache.CreateCopy((object) splitToDeallocate);
    SOLineSplit unallocatedSplitToMerge = this.FindUnallocatedSplitToMerge(graph, split);
    bool flag = false;
    Decimal? qty1 = split.Qty;
    Decimal? nullable1 = qtyToDeallocate;
    Decimal? nullable2;
    if (qty1.GetValueOrDefault() > nullable1.GetValueOrDefault() & qty1.HasValue & nullable1.HasValue)
    {
      SOLineSplit soLineSplit = split;
      Decimal? qty2 = split.Qty;
      nullable2 = qtyToDeallocate;
      Decimal? nullable3 = qty2.HasValue & nullable2.HasValue ? new Decimal?(qty2.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      soLineSplit.Qty = nullable3;
      split = (SOLineSplit) cache.Update((object) split);
      flag = true;
    }
    Decimal? nullable4;
    if (unallocatedSplitToMerge != null)
    {
      if (!flag)
      {
        qtyToDeallocate = split.Qty;
        cache.Delete((object) split);
      }
      SOLineSplit copy = (SOLineSplit) cache.CreateCopy((object) unallocatedSplitToMerge);
      SOLineSplit soLineSplit = copy;
      nullable2 = soLineSplit.Qty;
      Decimal? nullable5 = qtyToDeallocate;
      soLineSplit.Qty = nullable2.HasValue & nullable5.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
      cache.Update((object) copy);
      nullable4 = qtyToDeallocate;
    }
    else if (flag)
    {
      SOLineSplit soLineSplit = (SOLineSplit) cache.Insert();
      soLineSplit.Qty = qtyToDeallocate;
      soLineSplit.IsAllocated = new bool?(false);
      if (soLineSplit.Behavior == "BL")
        soLineSplit.POCreate = new bool?(false);
      cache.Update((object) soLineSplit);
      nullable4 = qtyToDeallocate;
    }
    else
    {
      split.IsAllocated = new bool?(false);
      split.LotSerialNbr = (string) null;
      SOLineSplit soLineSplit = (SOLineSplit) cache.Update((object) split);
      if (soLineSplit.Behavior == "BL" && soLineSplit.POCreate.GetValueOrDefault())
      {
        soLineSplit = (SOLineSplit) cache.CreateCopy((object) soLineSplit);
        soLineSplit.POCreate = new bool?(false);
        cache.Update((object) soLineSplit);
      }
      nullable4 = soLineSplit.Qty;
    }
    return nullable4;
  }

  protected virtual Decimal? DeallocateLots(
    SOOrderEntry graph,
    SOLine line,
    int? siteID,
    Decimal? qtyToDeallocate)
  {
    SOLineSplit[] source = this.SelectAllocatedSplits(graph, line, siteID);
    Decimal? nullable1 = ((IEnumerable<SOLineSplit>) source).Sum<SOLineSplit>((Func<SOLineSplit, Decimal?>) (x => x.Qty));
    Decimal? nullable2 = qtyToDeallocate;
    Decimal? nullable3;
    if (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
    {
      List<SOLineSplit> list = ((IEnumerable<SOLineSplit>) source).Where<SOLineSplit>((Func<SOLineSplit, bool>) (x => string.IsNullOrEmpty(x.LotSerialNbr))).ToList<SOLineSplit>();
      nullable2 = list.Sum<SOLineSplit>((Func<SOLineSplit, Decimal?>) (x => x.Qty));
      nullable3 = qtyToDeallocate;
      if (nullable2.GetValueOrDefault() < nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue)
        throw new PXException("For lot/serial-tracked items, you can deallocate only the full quantity or the quantity allocated without lot/serial numbers. If you need to deallocate a partial quantity that includes lot/serial numbers, use the Line Details dialog box on the Details tab of the Sales Orders (SO301000) form.");
      source = list.ToArray();
    }
    Decimal? qtyToDeallocate1 = qtyToDeallocate;
    Queue<SOLineSplit> queue = EnumerableExtensions.ToQueue<SOLineSplit>((IEnumerable<SOLineSplit>) ((IEnumerable<SOLineSplit>) source).OrderByDescending<SOLineSplit, int?>((Func<SOLineSplit, int?>) (s => s.SplitLineNbr)));
    Decimal? nullable4 = new Decimal?(0M);
    while (true)
    {
      nullable3 = qtyToDeallocate1;
      Decimal num = 0M;
      if (nullable3.GetValueOrDefault() > num & nullable3.HasValue && queue.Any<SOLineSplit>())
      {
        SOLineSplit splitToDeallocate = queue.Dequeue();
        Decimal? nullable5 = this.DeallocateNonLots(graph, splitToDeallocate, qtyToDeallocate1);
        nullable3 = nullable4;
        nullable2 = nullable5;
        nullable4 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        nullable2 = qtyToDeallocate1;
        nullable3 = nullable5;
        qtyToDeallocate1 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
      }
      else
        break;
    }
    return nullable4;
  }

  protected virtual SOLineSplit FindUnallocatedSplitToMerge(SOOrderEntry graph, SOLineSplit split)
  {
    SOLineSplit splitCopy = (SOLineSplit) ((PXSelectBase) graph.splits).Cache.CreateCopy((object) split);
    splitCopy.IsAllocated = new bool?(false);
    splitCopy.LotSerialNbr = (string) null;
    return ((IEnumerable<SOLineSplit>) this.SelectUnallocatedSplits(graph, ((PXSelectBase<SOLine>) graph.Transactions).Current)).OrderByDescending<SOLineSplit, int?>((Func<SOLineSplit, int?>) (x => x.SplitLineNbr)).FirstOrDefault<SOLineSplit>((Func<SOLineSplit, bool>) (s =>
    {
      int? splitLineNbr1 = s.SplitLineNbr;
      int? splitLineNbr2 = splitCopy.SplitLineNbr;
      return !(splitLineNbr1.GetValueOrDefault() == splitLineNbr2.GetValueOrDefault() & splitLineNbr1.HasValue == splitLineNbr2.HasValue) && graph.LineSplittingAllocatedExt.SchedulesEqual(s, splitCopy, (PXDBOperation) 1);
    }));
  }

  protected virtual SOLineSplit[] SelectAllocatedSplits(
    SOOrderEntry graph,
    SOLine line,
    int? siteID)
  {
    SOOrderLineSplittingAllocatedExtension splittingAllocatedExt = graph.LineSplittingAllocatedExt;
    return PXParentAttribute.SelectChildren(((PXSelectBase) graph.splits).Cache, (object) line, ((PXSelectBase<SOLine>) graph.Transactions).GetItemType()).Cast<SOLineSplit>().Where<SOLineSplit>((Func<SOLineSplit, bool>) (s =>
    {
      if (s.IsAllocated.GetValueOrDefault())
      {
        int? siteId = s.SiteID;
        int? nullable = siteID;
        if (siteId.GetValueOrDefault() == nullable.GetValueOrDefault() & siteId.HasValue == nullable.HasValue && s.POReceiptNbr == null)
          return splittingAllocatedExt.AllowToManualAllocate(line, s);
      }
      return false;
    })).ToArray<SOLineSplit>();
  }

  protected virtual INLotSerClass ReadLotSerClass(PXGraph graph, int? inventoryID)
  {
    if (!inventoryID.HasValue)
      return (INLotSerClass) null;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(graph, inventoryID);
    if (inventoryItem == null)
      throw new PXException("{0} '{1}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[2]
      {
        (object) "Inventory Item",
        (object) inventoryID
      });
    if (!inventoryItem.StkItem.GetValueOrDefault())
      return (INLotSerClass) null;
    return INLotSerClass.PK.Find(graph, inventoryItem.LotSerClassID) ?? throw new PXException("{0} '{1}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[2]
    {
      (object) "Lot/Serial Class",
      (object) inventoryItem.LotSerClassID
    });
  }

  protected delegate bool ValidateOrder(
    SOOrderEntry graph,
    SOOrder order,
    SalesAllocation allocation,
    out Exception error);

  protected delegate bool ValidateLine(
    SOOrderEntry graph,
    SOLine line,
    SalesAllocation allocation,
    out Exception error);

  protected delegate Decimal? ProcessLine(
    SOOrderEntry graph,
    SOLine line,
    SalesAllocation allocation);
}
