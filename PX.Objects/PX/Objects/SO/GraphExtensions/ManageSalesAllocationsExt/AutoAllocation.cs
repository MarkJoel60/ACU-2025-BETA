// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.ManageSalesAllocationsExt.AutoAllocation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.ManageSalesAllocationsExt;

[PXProtectedAccess(typeof (ManageSalesAllocations))]
public abstract class AutoAllocation : PXGraphExtension<ManageSalesAllocations>
{
  [PXReadOnlyView]
  public PXSelect<SalesAllocationStatus> AllocationStatuses;

  /// Uses <see cref="M:PX.Objects.SO.ManageSalesAllocations.GetAllocationError(PX.Objects.SO.SalesAllocation,System.Nullable{System.Decimal})" />
  [PXProtectedAccess(null)]
  protected abstract PXSetPropertyException GetAllocationError(
    SalesAllocation row,
    Decimal? qtyToAllocate);

  /// Uses <see cref="M:PX.Objects.SO.ManageSalesAllocations.ShowAllocationError(PX.Objects.SO.SalesAllocation)" />
  [PXProtectedAccess(null)]
  protected abstract bool ShowAllocationError(SalesAllocation row);

  /// Overrides <see cref="M:PX.Objects.SO.ManageSalesAllocations.ResetAllocations" />
  public void ResetAllocations(Action baseImpl)
  {
    baseImpl();
    ((PXSelectBase) this.AllocationStatuses).Cache.Clear();
  }

  [PXOverride]
  public void CalculateQtyToAllocate(
    List<PXResult<SalesAllocation>> rows,
    Action<List<PXResult<SalesAllocation>>> baseImpl)
  {
    baseImpl(rows);
    List<SalesAllocation> list1 = GraphHelper.RowCast<SalesAllocation>((IEnumerable) rows).ToList<SalesAllocation>();
    this.ResetAllocationStatuses((IEnumerable<SalesAllocation>) list1);
    PXCache<SalesAllocation> pxCache = GraphHelper.Caches<SalesAllocation>((PXGraph) this.Base);
    List<SalesAllocation> list2 = NonGenericIEnumerableExtensions.Concat_(((PXCache) pxCache).Inserted, ((PXCache) pxCache).Updated).Cast<SalesAllocation>().Where<SalesAllocation>((Func<SalesAllocation, bool>) (x => x.Selected.GetValueOrDefault())).ToList<SalesAllocation>();
    foreach (SalesAllocation row in list2.Except<SalesAllocation>((IEnumerable<SalesAllocation>) list1, PXCacheEx.GetComparer<SalesAllocation>(pxCache)).ToList<SalesAllocation>())
    {
      row.IsExtraAllocation = new bool?(false);
      this.Allocate(row);
    }
    foreach (SalesAllocation row in list1.Intersect<SalesAllocation>((IEnumerable<SalesAllocation>) list2, PXCacheEx.GetComparer<SalesAllocation>(pxCache)).ToList<SalesAllocation>())
    {
      row.IsExtraAllocation = new bool?(false);
      SalesAllocationStatus allocationStatus = this.Allocate(row);
      Decimal? nullable = row.QtyToAllocate;
      Decimal num = 0M;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      {
        nullable = allocationStatus.AllocatedQty;
        Decimal? availableQty = allocationStatus.AvailableQty;
        if (nullable.GetValueOrDefault() > availableQty.GetValueOrDefault() & nullable.HasValue & availableQty.HasValue)
          row.IsExtraAllocation = new bool?(true);
      }
    }
    foreach (SalesAllocation row in list1)
    {
      if (!row.Selected.GetValueOrDefault())
      {
        row.IsExtraAllocation = new bool?(false);
        this.Allocate(row);
      }
    }
  }

  /// Overrides <see cref="M:PX.Objects.SO.ManageSalesAllocations.GetAllocationError(PX.Objects.SO.SalesAllocation)" />
  [PXOverride]
  public PXSetPropertyException GetAllocationError(
    SalesAllocation row,
    Decimal? qtyToAllocate,
    Func<SalesAllocation, Decimal?, PXSetPropertyException> baseImpl)
  {
    PXSetPropertyException allocationError = baseImpl(row, qtyToAllocate);
    if (allocationError == null)
    {
      Decimal? nullable1 = qtyToAllocate;
      Decimal num1 = 0M;
      SalesAllocationStatus status;
      if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue && this.TryGetAllocationStatus(row, out status))
      {
        nullable1 = row.QtyToAllocate;
        Decimal? nullable2 = qtyToAllocate;
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        {
          Decimal num2 = qtyToAllocate.GetValueOrDefault() - row.QtyToAllocate.GetValueOrDefault();
          if (num2 > 0M)
          {
            Decimal num3 = num2;
            nullable1 = status.UnallocatedQty;
            Decimal? bufferedQty = status.BufferedQty;
            Decimal? nullable3 = nullable1.HasValue & bufferedQty.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + bufferedQty.GetValueOrDefault()) : new Decimal?();
            Decimal valueOrDefault = nullable3.GetValueOrDefault();
            if (num3 > valueOrDefault & nullable3.HasValue)
              allocationError = new PXSetPropertyException("The quantity to allocate is greater than the quantity available for allocation.");
          }
        }
        else
        {
          Decimal? allocatedQty = status.AllocatedQty;
          Decimal? availableQty = status.AvailableQty;
          if (allocatedQty.GetValueOrDefault() > availableQty.GetValueOrDefault() & allocatedQty.HasValue & availableQty.HasValue && row.IsExtraAllocation.GetValueOrDefault())
            allocationError = new PXSetPropertyException("The quantity to allocate is greater than the quantity available for allocation.");
        }
      }
    }
    return allocationError;
  }

  protected virtual void ResetAllocationStatuses(IEnumerable<SalesAllocation> rows)
  {
    foreach (SalesAllocationStatus allocationStatus in NonGenericIEnumerableExtensions.Concat_(((PXSelectBase) this.AllocationStatuses).Cache.Inserted, ((PXSelectBase) this.AllocationStatuses).Cache.Updated).OfType<SalesAllocationStatus>().ToList<SalesAllocationStatus>())
    {
      allocationStatus.AvailableQty = new Decimal?(0M);
      allocationStatus.AllocatedQty = new Decimal?(0M);
      allocationStatus.AllocatedSelectedQty = new Decimal?(0M);
      allocationStatus.UnallocatedQty = new Decimal?(0M);
      allocationStatus.IsDirty = new bool?(true);
    }
    foreach (SalesAllocation row in rows.Where<SalesAllocation>((Func<SalesAllocation, bool>) (x => !x.Selected.GetValueOrDefault())))
      this.PrepareAllocationStatus(row);
    foreach (SalesAllocation row in rows.Where<SalesAllocation>((Func<SalesAllocation, bool>) (x => x.Selected.GetValueOrDefault())))
      this.PrepareAllocationStatus(row);
  }

  protected virtual bool TryGetAllocationStatus(
    SalesAllocation row,
    out SalesAllocationStatus status)
  {
    SalesAllocationStatus allocationStatus = (SalesAllocationStatus) ((PXSelectBase) this.AllocationStatuses).Cache.Locate((object) new SalesAllocationStatus()
    {
      SiteID = row.LineSiteID,
      InventoryID = row.InventoryID
    });
    if (allocationStatus != null)
    {
      status = allocationStatus;
      return true;
    }
    status = (SalesAllocationStatus) null;
    return false;
  }

  protected virtual SalesAllocationStatus PrepareAllocationStatus(SalesAllocation row)
  {
    SalesAllocationStatus status;
    if (this.TryGetAllocationStatus(row, out status))
    {
      if (status.IsDirty.GetValueOrDefault())
      {
        Decimal? nullable1;
        ref Decimal? local = ref nullable1;
        Decimal? nullable2 = row.QtyHardAvail;
        Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
        local = new Decimal?(valueOrDefault1);
        status.AvailableQty = nullable1;
        status.UnallocatedQty = nullable1;
        nullable2 = status.BufferedQty;
        Decimal num = 0M;
        if (nullable2.GetValueOrDefault() > num & nullable2.HasValue)
        {
          SalesAllocationStatus allocationStatus = status;
          nullable2 = allocationStatus.UnallocatedQty;
          Decimal? nullable3 = status.BufferedQty;
          Decimal valueOrDefault2 = nullable3.GetValueOrDefault();
          Decimal? nullable4;
          if (!nullable2.HasValue)
          {
            nullable3 = new Decimal?();
            nullable4 = nullable3;
          }
          else
            nullable4 = new Decimal?(nullable2.GetValueOrDefault() - valueOrDefault2);
          allocationStatus.UnallocatedQty = nullable4;
        }
        status.IsDirty = new bool?(false);
      }
    }
    else
      status = (SalesAllocationStatus) ((PXSelectBase) this.AllocationStatuses).Cache.Insert((object) new SalesAllocationStatus()
      {
        SiteID = row.LineSiteID,
        InventoryID = row.InventoryID,
        AvailableQty = row.QtyHardAvail,
        UnallocatedQty = row.QtyHardAvail
      });
    return status;
  }

  protected virtual SalesAllocationStatus Allocate(SalesAllocation row)
  {
    SalesAllocationStatus allocationStatus1 = this.PrepareAllocationStatus(row);
    Decimal? nullable1;
    Decimal num1;
    Decimal? nullable2;
    if (row.Selected.GetValueOrDefault())
    {
      nullable1 = row.QtyToAllocate;
      num1 = nullable1.GetValueOrDefault();
      nullable1 = row.QtyHardAvail;
      Decimal? availableQty = allocationStatus1.AvailableQty;
      if (!(nullable1.GetValueOrDefault() == availableQty.GetValueOrDefault() & nullable1.HasValue == availableQty.HasValue))
        row.QtyHardAvail = allocationStatus1.AvailableQty;
    }
    else
    {
      nullable2 = allocationStatus1.UnallocatedQty;
      Decimal num2 = 0M;
      if (nullable2.GetValueOrDefault() <= num2 & nullable2.HasValue)
      {
        num1 = 0M;
      }
      else
      {
        nullable2 = allocationStatus1.UnallocatedQty;
        nullable1 = row.QtyUnallocated;
        Decimal valueOrDefault = nullable1.GetValueOrDefault();
        if (nullable2.GetValueOrDefault() >= valueOrDefault & nullable2.HasValue)
        {
          nullable2 = row.QtyUnallocated;
          num1 = nullable2.GetValueOrDefault();
        }
        else
        {
          nullable2 = allocationStatus1.UnallocatedQty;
          num1 = nullable2.GetValueOrDefault();
        }
      }
    }
    bool? selected = row.Selected;
    if (!selected.GetValueOrDefault())
      row.QtyToAllocate = new Decimal?(num1);
    SalesAllocationStatus allocationStatus2 = allocationStatus1;
    nullable2 = allocationStatus2.AllocatedQty;
    Decimal num3 = num1;
    Decimal? nullable3;
    if (!nullable2.HasValue)
    {
      nullable1 = new Decimal?();
      nullable3 = nullable1;
    }
    else
      nullable3 = new Decimal?(nullable2.GetValueOrDefault() + num3);
    allocationStatus2.AllocatedQty = nullable3;
    SalesAllocationStatus allocationStatus3 = allocationStatus1;
    nullable2 = allocationStatus3.UnallocatedQty;
    Decimal num4 = num1;
    Decimal? nullable4;
    if (!nullable2.HasValue)
    {
      nullable1 = new Decimal?();
      nullable4 = nullable1;
    }
    else
      nullable4 = new Decimal?(nullable2.GetValueOrDefault() - num4);
    allocationStatus3.UnallocatedQty = nullable4;
    selected = row.Selected;
    if (selected.GetValueOrDefault())
    {
      SalesAllocationStatus allocationStatus4 = allocationStatus1;
      nullable2 = allocationStatus4.AllocatedSelectedQty;
      Decimal num5 = num1;
      Decimal? nullable5;
      if (!nullable2.HasValue)
      {
        nullable1 = new Decimal?();
        nullable5 = nullable1;
      }
      else
        nullable5 = new Decimal?(nullable2.GetValueOrDefault() + num5);
      allocationStatus4.AllocatedSelectedQty = nullable5;
    }
    return allocationStatus1;
  }

  protected virtual SalesAllocationStatus SyncAllocationStatus(
    SalesAllocation oldRow,
    SalesAllocation row)
  {
    bool flag1 = ((PXSelectBase) this.AllocationStatuses).Cache.ObjectsEqual<SalesAllocation.selected>((object) oldRow, (object) row);
    bool flag2 = ((PXSelectBase) this.AllocationStatuses).Cache.ObjectsEqual<SalesAllocation.qtyToAllocate>((object) oldRow, (object) row);
    if (!flag1 && !flag2)
      return (SalesAllocationStatus) null;
    SalesAllocationStatus status1;
    if (!this.TryGetAllocationStatus(row, out status1))
      return (SalesAllocationStatus) null;
    Decimal? nullable1 = row.QtyToAllocate;
    Decimal? nullable2 = oldRow.QtyToAllocate;
    Decimal? qty1 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    if (flag2)
    {
      nullable2 = qty1;
      Decimal num1 = 0M;
      if (nullable2.GetValueOrDefault() > num1 & nullable2.HasValue)
      {
        Decimal? nullable3 = qty1;
        nullable2 = status1.BufferedQty;
        Decimal num2 = 0M;
        if (nullable2.GetValueOrDefault() > num2 & nullable2.HasValue)
        {
          nullable2 = nullable3;
          nullable1 = this.TakeFromBuffer(row, status1, qty1);
          nullable3 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
        }
        SalesAllocationStatus allocationStatus = status1;
        nullable1 = allocationStatus.UnallocatedQty;
        nullable2 = nullable3;
        allocationStatus.UnallocatedQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      }
      else
      {
        SalesAllocation allocation = row;
        SalesAllocationStatus status2 = status1;
        nullable2 = qty1;
        Decimal? qty2;
        if (!nullable2.HasValue)
        {
          nullable1 = new Decimal?();
          qty2 = nullable1;
        }
        else
          qty2 = new Decimal?(-nullable2.GetValueOrDefault());
        this.PutToBuffer(allocation, status2, qty2);
      }
      SalesAllocationStatus allocationStatus1 = status1;
      nullable2 = allocationStatus1.AllocatedQty;
      nullable1 = qty1;
      allocationStatus1.AllocatedQty = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    }
    if (flag1)
    {
      if (row.Selected.GetValueOrDefault())
      {
        SalesAllocationStatus allocationStatus = status1;
        nullable1 = allocationStatus.AllocatedSelectedQty;
        nullable2 = qty1;
        allocationStatus.AllocatedSelectedQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      }
      else
      {
        SalesAllocationStatus allocationStatus2 = status1;
        nullable2 = allocationStatus2.AllocatedSelectedQty;
        nullable1 = qty1;
        allocationStatus2.AllocatedSelectedQty = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
        nullable1 = row.BufferedQty;
        Decimal num = 0M;
        if (nullable1.GetValueOrDefault() > num & nullable1.HasValue)
        {
          SalesAllocationStatus allocationStatus3 = status1;
          nullable1 = allocationStatus3.BufferedQty;
          nullable2 = row.BufferedQty;
          allocationStatus3.BufferedQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
          row.BufferedQty = new Decimal?(0M);
          row.BufferedTime = new DateTime?();
        }
      }
    }
    return status1;
  }

  protected virtual Decimal? PutToBuffer(
    SalesAllocation allocation,
    SalesAllocationStatus status,
    Decimal? qty)
  {
    SalesAllocation salesAllocation = allocation;
    Decimal? nullable1 = salesAllocation.BufferedQty;
    Decimal? nullable2 = qty;
    salesAllocation.BufferedQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    allocation.BufferedTime = new DateTime?(DateTime.UtcNow);
    SalesAllocationStatus allocationStatus = status;
    Decimal? bufferedQty = allocationStatus.BufferedQty;
    nullable1 = qty;
    allocationStatus.BufferedQty = bufferedQty.HasValue & nullable1.HasValue ? new Decimal?(bufferedQty.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    return qty;
  }

  protected virtual Decimal? TakeFromBuffer(
    SalesAllocation allocation,
    SalesAllocationStatus status,
    Decimal? qty)
  {
    Decimal? bufferedQty1 = status.BufferedQty;
    Decimal? nullable1 = qty;
    qty = bufferedQty1.GetValueOrDefault() >= nullable1.GetValueOrDefault() & bufferedQty1.HasValue & nullable1.HasValue ? qty : status.BufferedQty;
    SalesAllocationStatus allocationStatus = status;
    Decimal? nullable2 = allocationStatus.BufferedQty;
    Decimal? nullable3 = qty;
    allocationStatus.BufferedQty = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
    Queue<SalesAllocation> queue = EnumerableExtensions.ToQueue<SalesAllocation>((IEnumerable<SalesAllocation>) NonGenericIEnumerableExtensions.Concat_(((PXSelectBase) this.Base.Allocations).Cache.Inserted, ((PXSelectBase) this.Base.Allocations).Cache.Updated).OfType<SalesAllocation>().Where<SalesAllocation>((Func<SalesAllocation, bool>) (x =>
    {
      Decimal? bufferedQty2 = x.BufferedQty;
      Decimal num = 0M;
      if (!(bufferedQty2.GetValueOrDefault() > num & bufferedQty2.HasValue))
        return false;
      int? inventoryId1 = x.InventoryID;
      int? inventoryId2 = allocation.InventoryID;
      return inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue;
    })).OrderBy<SalesAllocation, DateTime?>((Func<SalesAllocation, DateTime?>) (x => x.BufferedTime)));
    Decimal? nullable4 = qty;
    while (true)
    {
      nullable2 = nullable4;
      Decimal num1 = 0M;
      if (nullable2.GetValueOrDefault() > num1 & nullable2.HasValue && queue.Any<SalesAllocation>())
      {
        SalesAllocation salesAllocation1 = queue.Dequeue();
        nullable3 = salesAllocation1.BufferedQty;
        nullable2 = nullable4;
        Decimal? nullable5 = nullable3.GetValueOrDefault() >= nullable2.GetValueOrDefault() & nullable3.HasValue & nullable2.HasValue ? nullable4 : salesAllocation1.BufferedQty;
        SalesAllocation salesAllocation2 = salesAllocation1;
        nullable2 = salesAllocation2.BufferedQty;
        nullable3 = nullable5;
        salesAllocation2.BufferedQty = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
        nullable3 = salesAllocation1.BufferedQty;
        Decimal num2 = 0M;
        if (nullable3.GetValueOrDefault() == num2 & nullable3.HasValue)
          salesAllocation1.BufferedTime = new DateTime?();
        nullable3 = nullable4;
        nullable2 = nullable5;
        nullable4 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      }
      else
        break;
    }
    return qty;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<SalesAllocation, SalesAllocation.qtyToAllocate> e)
  {
    if (!((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SalesAllocation, SalesAllocation.qtyToAllocate>>) e).ExternalCall)
      return;
    Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SalesAllocation, SalesAllocation.qtyToAllocate>, SalesAllocation, object>) e).NewValue;
    PXSetPropertyException allocationError = this.GetAllocationError(e.Row, newValue);
    if (allocationError != null)
    {
      allocationError.ErrorValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SalesAllocation, SalesAllocation.qtyToAllocate>, SalesAllocation, object>) e).NewValue;
      throw allocationError;
    }
    if (!(Decimal.Remainder(newValue.GetValueOrDefault(), 1M) > 0M) || !(INLotSerClass.PK.Find((PXGraph) this.Base, PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, e.Row.InventoryID)?.LotSerClassID)?.LotSerTrack == "S"))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SalesAllocation, SalesAllocation.qtyToAllocate>, SalesAllocation, object>) e).NewValue = (object) Math.Truncate(newValue.GetValueOrDefault());
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SalesAllocation> e)
  {
    if (!e.ExternalCall || !(((PXSelectBase<SalesAllocationsFilter>) this.Base.Filter).Current.Action == "Alloc"))
      return;
    SalesAllocationStatus allocationStatus = this.SyncAllocationStatus(e.OldRow, e.Row);
    if (allocationStatus == null)
      return;
    bool? nullable1 = e.Row.IsExtraAllocation;
    if (!nullable1.GetValueOrDefault())
      return;
    SalesAllocation row = e.Row;
    nullable1 = e.Row.Selected;
    int num1;
    if (nullable1.GetValueOrDefault())
    {
      Decimal? nullable2 = e.Row.QtyToAllocate;
      Decimal num2 = 0M;
      if (nullable2.GetValueOrDefault() > num2 & nullable2.HasValue)
      {
        nullable2 = allocationStatus.AllocatedQty;
        Decimal? availableQty = allocationStatus.AvailableQty;
        num1 = nullable2.GetValueOrDefault() > availableQty.GetValueOrDefault() & nullable2.HasValue & availableQty.HasValue ? 1 : 0;
        goto label_7;
      }
    }
    num1 = 0;
label_7:
    bool? nullable3 = new bool?(num1 != 0);
    row.IsExtraAllocation = nullable3;
    nullable1 = e.Row.IsExtraAllocation;
    bool flag = false;
    if (!(nullable1.GetValueOrDefault() == flag & nullable1.HasValue))
      return;
    this.ShowAllocationError(e.Row);
  }
}
