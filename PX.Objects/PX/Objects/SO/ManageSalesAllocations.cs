// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.ManageSalesAllocations
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.DbServices.QueryObjectModel;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.SO;

public class ManageSalesAllocations : 
  PXGraph<
  #nullable disable
  ManageSalesAllocations>,
  PXImportAttribute.IPXPrepareItems,
  PXImportAttribute.IPXProcess
{
  private const string MultiSelectSeparator = ",";
  public PXCancel<SalesAllocationsFilter> Cancel;
  public PXFilter<SalesAllocationsFilter> Filter;
  [PXFilterable(new Type[] {})]
  public ManageSalesAllocations.AllocationsProcessing Allocations;

  public ManageSalesAllocations()
  {
    SOSetup current = ((PXSelectBase<SOSetup>) new PXSetup<SOSetup>((PXGraph) this)).Current;
    ((PXProcessingBase<SalesAllocation>) this.Allocations).SuppressUpdate = true;
    ((PXSelectBase) this.Allocations).Cache.DisableReadItem = true;
    PXUIFieldAttribute.SetEnabled<SalesAllocation.qtyToAllocate>(((PXSelectBase) this.Allocations).Cache, (object) null);
    PXUIFieldAttribute.SetEnabled<SalesAllocation.qtyToDeallocate>(((PXSelectBase) this.Allocations).Cache, (object) null);
    this.SetOrderTypeList();
  }

  protected virtual IEnumerable allocations()
  {
    SalesAllocationsFilter current = ((PXSelectBase<SalesAllocationsFilter>) this.Filter).Current;
    if (!this.AllowProcess(current))
      return (IEnumerable) Array<SalesAllocation>.Empty;
    int maximumRows = PXView.MaximumRows;
    if (maximumRows == 1)
    {
      SalesAllocation salesAllocation = (SalesAllocation) ((PXSelectBase) this.Allocations).Cache.Locate((object) ((PXSelectBase) this.Allocations).Cache.CreateInstance<SalesAllocation>(PXView.SortColumns, PXView.Searches));
      if (salesAllocation != null)
        return (IEnumerable) new List<SalesAllocation>()
        {
          salesAllocation
        };
    }
    else
      ((PXSelectBase) this.Allocations).Cache.UnHoldRows<SalesAllocation>(((PXSelectBase) this.Allocations).Cache.Cached.OfType<SalesAllocation>());
    bool flag = current.Action == "Alloc";
    ViewSettings viewSettings = ViewSettings.FromCurrentContext();
    viewSettings.FieldsMap.Add("inventoryID", "inventoryCD");
    if (flag)
      viewSettings.SelectFromFirstPage();
    List<PXResult<SalesAllocation>> list = this.LoadAllocations(current, viewSettings).ToList<PXResult<SalesAllocation>>();
    PXView.StartRow = 0;
    if (list.Any<PXResult<SalesAllocation>>() && maximumRows != 1)
    {
      if (flag)
        this.CalculateQtyToAllocate(list);
      else
        this.CalculateQtyToDeallocate(list);
    }
    List<PXResult<SalesAllocation>> collection = flag ? viewSettings.GetCurrentPageRange<PXResult<SalesAllocation>>(list) : list;
    ((PXSelectBase) this.Allocations).Cache.HoldRows<SalesAllocation>(GraphHelper.RowCast<SalesAllocation>((IEnumerable) collection));
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    pxDelegateResult.IsResultFiltered = true;
    pxDelegateResult.IsResultSorted = true;
    pxDelegateResult.IsResultTruncated = true;
    ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) collection);
    return (IEnumerable) pxDelegateResult;
  }

  protected virtual IEnumerable<PXResult<SalesAllocation>> LoadAllocations(
    SalesAllocationsFilter filter,
    ViewSettings viewSettings)
  {
    List<object> parameters = new List<object>();
    PXSelectBase<SalesAllocation> query = this.CreateQuery(filter, parameters);
    parameters.AddRange((IEnumerable<object>) viewSettings.Parameters);
    viewSettings.Parameters = parameters;
    viewSettings.ApplySortingFrom<SalesAllocation>(query);
    foreach (object obj in ((PXSelectBase) query).View.Select(viewSettings))
    {
      if (!(obj is PXResult<SalesAllocation> pxResult))
        pxResult = new PXResult<SalesAllocation>((SalesAllocation) obj);
      yield return pxResult;
    }
  }

  protected virtual PXSelectBase<SalesAllocation> CreateQuery(
    SalesAllocationsFilter filter,
    List<object> parameters)
  {
    PXSelectBase<SalesAllocation> query = (PXSelectBase<SalesAllocation>) null;
    switch (filter.Action)
    {
      case "Alloc":
        query = this.CreateAllocateQuery(filter, parameters);
        break;
      case "Dealloc":
        query = this.CreateDeallocateQuery(filter, parameters);
        break;
    }
    return query;
  }

  protected virtual PXSelectBase<SalesAllocation> CreateBaseQuery(
    SalesAllocationsFilter filter,
    List<object> parameters)
  {
    // ISSUE: variable of a compiler-generated type
    ManageSalesAllocations.\u003C\u003Ec__DisplayClass9_0 cDisplayClass90;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.parameters = parameters;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.filter = filter;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.query = new FbqlSelect<SelectFromBase<SalesAllocation, TypeArrayOf<IFbqlJoin>.Empty>.Order<PX.Data.BQL.Fluent.By<BqlField<SalesAllocation.orderPriority, IBqlShort>.Asc>>, SalesAllocation>.View((PXGraph) this);
    string[] orderTypes = SOOrderTypeApproval.GetOrderTypes();
    if (orderTypes.Length != 0)
    {
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase<SalesAllocation>) cDisplayClass90.query).WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SalesAllocation.orderHold, Equal<True>>>>>.Or<BqlOperand<SalesAllocation.orderType, IBqlString>.IsNotIn<P.AsString.ASCII>>>>();
      ManageSalesAllocations.\u003CCreateBaseQuery\u003Eg__push\u007C9_0((object) orderTypes, ref cDisplayClass90);
    }
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass90.filter.SelectBy != null)
    {
      // ISSUE: reference to a compiler-generated field
      switch (cDisplayClass90.filter.SelectBy)
      {
        case "OrderDate":
          ManageSalesAllocations.\u003CCreateBaseQuery\u003Eg__addFilter\u007C9_1<SalesAllocation.orderDate>(ref cDisplayClass90);
          break;
        case "RequestedOn":
          ManageSalesAllocations.\u003CCreateBaseQuery\u003Eg__addFilter\u007C9_1<SalesAllocation.requestDate>(ref cDisplayClass90);
          break;
        case "ShipOn":
          ManageSalesAllocations.\u003CCreateBaseQuery\u003Eg__addFilter\u007C9_1<SalesAllocation.shipDate>(ref cDisplayClass90);
          break;
        case "CancelBy":
          ManageSalesAllocations.\u003CCreateBaseQuery\u003Eg__addFilter\u007C9_1<SalesAllocation.cancelDate>(ref cDisplayClass90);
          break;
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (!string.IsNullOrEmpty(cDisplayClass90.filter.OrderType))
    {
      // ISSUE: reference to a compiler-generated field
      string[] strArray = cDisplayClass90.filter.OrderType.Split(new string[1]
      {
        ","
      }, StringSplitOptions.RemoveEmptyEntries);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase<SalesAllocation>) cDisplayClass90.query).WhereAnd<Where<BqlOperand<SalesAllocation.orderType, IBqlString>.IsIn<P.AsString.ASCII>>>();
      ref ManageSalesAllocations.\u003C\u003Ec__DisplayClass9_0 local = ref cDisplayClass90;
      ManageSalesAllocations.\u003CCreateBaseQuery\u003Eg__push\u007C9_0((object) strArray, ref local);
    }
    // ISSUE: reference to a compiler-generated field
    if (!string.IsNullOrEmpty(cDisplayClass90.filter.OrderStatus))
    {
      // ISSUE: reference to a compiler-generated field
      string[] strArray = cDisplayClass90.filter.OrderStatus.Split(new string[1]
      {
        ","
      }, StringSplitOptions.RemoveEmptyEntries);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase<SalesAllocation>) cDisplayClass90.query).WhereAnd<Where<BqlOperand<SalesAllocation.orderStatus, IBqlString>.IsIn<P.AsString.ASCII>>>();
      ref ManageSalesAllocations.\u003C\u003Ec__DisplayClass9_0 local = ref cDisplayClass90;
      ManageSalesAllocations.\u003CCreateBaseQuery\u003Eg__push\u007C9_0((object) strArray, ref local);
    }
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass90.filter.OrderNbr != null)
    {
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase<SalesAllocation>) cDisplayClass90.query).WhereAnd<Where<BqlOperand<SalesAllocation.orderNbr, IBqlString>.IsEqual<P.AsString>>>();
      // ISSUE: reference to a compiler-generated field
      ManageSalesAllocations.\u003CCreateBaseQuery\u003Eg__push\u007C9_0((object) cDisplayClass90.filter.OrderNbr, ref cDisplayClass90);
    }
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass90.filter.Priority.HasValue)
    {
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase<SalesAllocation>) cDisplayClass90.query).WhereAnd<Where<BqlOperand<SalesAllocation.orderPriority, IBqlShort>.IsEqual<P.AsInt>>>();
      // ISSUE: reference to a compiler-generated field
      ManageSalesAllocations.\u003CCreateBaseQuery\u003Eg__push\u007C9_0((object) cDisplayClass90.filter.Priority, ref cDisplayClass90);
    }
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass90.filter.CustomerClassID != null)
    {
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase<SalesAllocation>) cDisplayClass90.query).WhereAnd<Where<BqlOperand<SalesAllocation.customerClassID, IBqlString>.IsEqual<P.AsString>>>();
      // ISSUE: reference to a compiler-generated field
      ManageSalesAllocations.\u003CCreateBaseQuery\u003Eg__push\u007C9_0((object) cDisplayClass90.filter.CustomerClassID, ref cDisplayClass90);
    }
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass90.filter.CustomerID.HasValue)
    {
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase<SalesAllocation>) cDisplayClass90.query).WhereAnd<Where<BqlOperand<SalesAllocation.customerID, IBqlInt>.IsEqual<P.AsInt>>>();
      // ISSUE: reference to a compiler-generated field
      ManageSalesAllocations.\u003CCreateBaseQuery\u003Eg__push\u007C9_0((object) cDisplayClass90.filter.CustomerID, ref cDisplayClass90);
    }
    // ISSUE: reference to a compiler-generated field
    return (PXSelectBase<SalesAllocation>) cDisplayClass90.query;
  }

  protected virtual PXSelectBase<SalesAllocation> CreateAllocateQuery(
    SalesAllocationsFilter filter,
    List<object> parameters)
  {
    PXSelectBase<SalesAllocation> baseQuery = this.CreateBaseQuery(filter, parameters);
    baseQuery.Join<InnerJoin<INSiteStatusByCostCenterShort, On<SalesAllocation.FK.SiteStatusByCostCenterShort>>>();
    baseQuery.WhereAnd<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SalesAllocation.qtyUnallocated, Greater<decimal0>>>>, And<BqlOperand<INSiteStatusByCostCenterShort.qtyHardAvail, IBqlDecimal>.IsGreater<decimal0>>>>.And<BqlOperand<SalesAllocation.lineSiteID, IBqlInt>.IsEqual<P.AsInt>>>>();
    parameters.Add((object) filter.SiteID);
    if (string.IsNullOrEmpty(filter.OrderStatus))
      baseQuery.WhereAnd<Where<BqlOperand<SalesAllocation.orderStatus, IBqlString>.IsInSequence<SalesAllocationsFilter.orderStatus.list>>>();
    this.AddAllocateSorting(baseQuery, filter);
    return baseQuery;
  }

  protected virtual PXSelectBase<SalesAllocation> CreateDeallocateQuery(
    SalesAllocationsFilter filter,
    List<object> parameters)
  {
    PXSelectBase<SalesAllocation> baseQuery = this.CreateBaseQuery(filter, parameters);
    baseQuery.WhereAnd<Where<BqlOperand<SalesAllocation.qtyAllocated, IBqlDecimal>.IsGreater<decimal0>>>();
    if (string.IsNullOrEmpty(filter.OrderStatus))
      baseQuery.WhereAnd<Where<BqlOperand<SalesAllocation.orderStatus, IBqlString>.IsInSequence<SalesAllocationsFilter.orderStatus.list.withExpired>>>();
    this.AddDeallocateSorting(baseQuery, filter);
    return baseQuery;
  }

  protected virtual void AddAllocateSorting(
    PXSelectBase<SalesAllocation> query,
    SalesAllocationsFilter filter)
  {
    List<IBqlSortColumn> sortFields = new List<IBqlSortColumn>();
    sort<SalesAllocation.orderPriority>();
    if (filter.SelectBy != null)
    {
      switch (filter.SelectBy)
      {
        case "OrderDate":
          sort<SalesAllocation.orderDate>();
          break;
        case "RequestedOn":
          sort<SalesAllocation.requestDate>();
          break;
        case "ShipOn":
          sort<SalesAllocation.shipDate>();
          break;
        case "CancelBy":
          sort<SalesAllocation.cancelDate>();
          break;
      }
    }
    sort<SalesAllocation.orderCreatedOn>();
    sort<SalesAllocation.orderType>();
    sort<SalesAllocation.orderNbr>();
    sort<SalesAllocation.inventoryCD>();
    sort<SalesAllocation.lineNbr>();
    ((PXSelectBase) query).OrderByNew((IEnumerable<IBqlSortColumn>) sortFields);

    void addSort<TSortField>() where TSortField : IBqlSortColumn, new()
    {
      sortFields.Add((IBqlSortColumn) new TSortField());
    }

    void sort<TField>(bool asc = true) where TField : IBqlField
    {
      if (asc)
        addSort<Asc<TField>>();
      else
        addSort<Desc<TField>>();
    }
  }

  protected virtual void AddDeallocateSorting(
    PXSelectBase<SalesAllocation> query,
    SalesAllocationsFilter filter)
  {
    List<IBqlSortColumn> sortFields = new List<IBqlSortColumn>();
    sort<SalesAllocation.orderPriority>(false);
    if (filter.SelectBy != null)
    {
      switch (filter.SelectBy)
      {
        case "OrderDate":
          sort<SalesAllocation.orderDate>(false);
          break;
        case "RequestedOn":
          sort<SalesAllocation.requestDate>(false);
          break;
        case "ShipOn":
          sort<SalesAllocation.shipDate>(false);
          break;
        case "CancelBy":
          sort<SalesAllocation.cancelDate>(false);
          break;
      }
    }
    sort<SalesAllocation.orderCreatedOn>(false);
    sort<SalesAllocation.orderType>();
    sort<SalesAllocation.orderNbr>();
    sort<SalesAllocation.inventoryCD>();
    sort<SalesAllocation.lineNbr>();
    ((PXSelectBase) query).OrderByNew((IEnumerable<IBqlSortColumn>) sortFields);

    void addSort<TSortField>() where TSortField : IBqlSortColumn, new()
    {
      sortFields.Add((IBqlSortColumn) new TSortField());
    }

    void sort<TField>(bool asc = true) where TField : IBqlField
    {
      if (asc)
        addSort<Asc<TField>>();
      else
        addSort<Desc<TField>>();
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<SalesAllocationsFilter> e)
  {
    SalesAllocationsFilter row = e.Row;
    if (row == null)
      return;
    this.SetOrderStatusList(row);
    this.SetFieldsVisibility(row);
    bool flag = this.AllowProcess(row);
    ((PXProcessing<SalesAllocation>) this.Allocations).SetProcessEnabled(flag);
    ((PXProcessing<SalesAllocation>) this.Allocations).SetProcessAllEnabled(flag);
    if (!flag)
      return;
    this.SetProcessDelegate(row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SalesAllocationsFilter> e)
  {
    if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SalesAllocationsFilter>>) e).Cache.ObjectsEqual<SalesAllocationsFilter.action, SalesAllocationsFilter.siteID>((object) e.OldRow, (object) e.Row))
      this.ResetAllocations();
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SalesAllocationsFilter>>) e).Cache.ObjectsEqual<SalesAllocationsFilter.action>((object) e.OldRow, (object) e.Row))
      return;
    this.NormalizeOrderStatusValue(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<SalesAllocation> e)
  {
    if (e.Row == null || !(((PXSelectBase<SalesAllocationsFilter>) this.Filter).Current.Action == "Alloc") || !string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<SalesAllocation.qtyToAllocate>(((PXSelectBase) this.Allocations).Cache, (object) e.Row)))
      return;
    this.ShowAllocationError(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<SalesAllocation, SalesAllocation.qtyToAllocate> e)
  {
    if (!((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<SalesAllocation, SalesAllocation.qtyToAllocate>>) e).ExternalCall || e.Row.Selected.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SalesAllocation, SalesAllocation.qtyToAllocate>>) e).Cache.SetValueExt<SalesAllocation.selected>((object) e.Row, (object) true);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<SalesAllocation, SalesAllocation.qtyToDeallocate> e)
  {
    if (!((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<SalesAllocation, SalesAllocation.qtyToDeallocate>>) e).ExternalCall || e.Row.Selected.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SalesAllocation, SalesAllocation.qtyToDeallocate>>) e).Cache.SetValueExt<SalesAllocation.selected>((object) e.Row, (object) true);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SalesAllocation> e)
  {
    if (!e.ExternalCall || !e.OldRow.Selected.GetValueOrDefault() || e.Row.Selected.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SalesAllocation>>) e).Cache.SetStatus((object) e.Row, (PXEntryStatus) 5);
    if (!(((PXSelectBase<SalesAllocationsFilter>) this.Filter).Current.Action == "Alloc"))
      return;
    ((PXSelectBase) this.Allocations).View.RequestRefresh();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<SalesAllocation, SalesAllocation.qtyToDeallocate> e)
  {
    if (!((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SalesAllocation, SalesAllocation.qtyToDeallocate>>) e).ExternalCall)
      return;
    Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SalesAllocation, SalesAllocation.qtyToDeallocate>, SalesAllocation, object>) e).NewValue;
    PXSetPropertyException deallocationError = this.GetDeallocationError(e.Row, newValue);
    if (deallocationError != null)
    {
      deallocationError.ErrorValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SalesAllocation, SalesAllocation.qtyToDeallocate>, SalesAllocation, object>) e).NewValue;
      throw deallocationError;
    }
    if (!(Decimal.Remainder(newValue.GetValueOrDefault(), 1M) > 0M) || !(INLotSerClass.PK.Find((PXGraph) this, PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, e.Row.InventoryID)?.LotSerClassID)?.LotSerTrack == "S"))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SalesAllocation, SalesAllocation.qtyToDeallocate>, SalesAllocation, object>) e).NewValue = (object) Math.Truncate(newValue.GetValueOrDefault());
  }

  protected virtual bool AllowProcess(SalesAllocationsFilter filter)
  {
    return filter != null && filter.Action != null && filter.Action != "None" && filter.SelectBy != null && filter.EndDate.HasValue && filter.SiteID.HasValue;
  }

  protected virtual void CalculateQtyToAllocate(List<PXResult<SalesAllocation>> rows)
  {
    foreach (PXResult<SalesAllocation> row in rows)
    {
      SalesAllocation salesAllocation = PXResult<SalesAllocation>.op_Implicit(row);
      INSiteStatusByCostCenterShort byCostCenterShort = ((PXResult) row).GetItem<INSiteStatusByCostCenterShort>();
      if (byCostCenterShort != null)
        salesAllocation.QtyHardAvail = byCostCenterShort.QtyHardAvail;
    }
  }

  protected virtual void CalculateQtyToDeallocate(List<PXResult<SalesAllocation>> rows)
  {
    foreach (PXResult<SalesAllocation> row in rows)
    {
      SalesAllocation salesAllocation = PXResult<SalesAllocation>.op_Implicit(row);
      if (!salesAllocation.Selected.GetValueOrDefault())
        salesAllocation.QtyToDeallocate = salesAllocation.QtyAllocated;
    }
  }

  protected virtual bool ShowAllocationError(SalesAllocation row)
  {
    PXSetPropertyException allocationError = this.GetAllocationError(row);
    ((PXSelectBase) this.Allocations).Cache.RaiseExceptionHandling<SalesAllocation.qtyToAllocate>((object) row, (object) row.QtyToAllocate, (Exception) allocationError);
    return allocationError != null;
  }

  protected virtual PXSetPropertyException GetAllocationError(SalesAllocation row)
  {
    return this.GetAllocationError(row, row.QtyToAllocate);
  }

  protected virtual PXSetPropertyException GetAllocationError(
    SalesAllocation row,
    Decimal? qtyToAllocate)
  {
    Decimal? nullable1 = qtyToAllocate;
    Decimal num = 0M;
    if (nullable1.GetValueOrDefault() < num & nullable1.HasValue)
      return new PXSetPropertyException("'{0}' should not be negative.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<SalesAllocation.qtyToAllocate>(((PXSelectBase) this.Allocations).Cache)
      });
    Decimal? nullable2 = qtyToAllocate;
    Decimal? qtyUnallocated = row.QtyUnallocated;
    return nullable2.GetValueOrDefault() > qtyUnallocated.GetValueOrDefault() & nullable2.HasValue & qtyUnallocated.HasValue ? new PXSetPropertyException("The quantity to allocate cannot be greater than the unallocated quantity.") : (PXSetPropertyException) null;
  }

  protected virtual PXSetPropertyException GetDeallocationError(
    SalesAllocation row,
    Decimal? qtyToDeallocate)
  {
    Decimal? nullable1 = qtyToDeallocate;
    Decimal num = 0M;
    if (nullable1.GetValueOrDefault() < num & nullable1.HasValue)
      return new PXSetPropertyException("'{0}' should not be negative.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<SalesAllocation.qtyToDeallocate>(((PXSelectBase) this.Allocations).Cache)
      });
    Decimal? nullable2 = qtyToDeallocate;
    Decimal? nullable3 = row.QtyAllocated;
    if (nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue)
      return new PXSetPropertyException("The quantity to deallocate cannot be greater than the allocated quantity.");
    Decimal? nullable4;
    if (row.OrderStatus == "D")
    {
      nullable3 = qtyToDeallocate;
      nullable4 = row.QtyAllocated;
      if (nullable3.GetValueOrDefault() < nullable4.GetValueOrDefault() & nullable3.HasValue & nullable4.HasValue)
        return new PXSetPropertyException("The quantity to deallocate cannot be changed for lines of expired blanket sales orders.");
    }
    nullable4 = qtyToDeallocate;
    nullable3 = row.QtyAllocated;
    if (nullable4.GetValueOrDefault() < nullable3.GetValueOrDefault() & nullable4.HasValue & nullable3.HasValue)
    {
      nullable3 = row.QtyAllocated;
      nullable4 = row.LotSerialQtyAllocated;
      nullable4 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
      nullable3 = qtyToDeallocate;
      if (nullable4.GetValueOrDefault() < nullable3.GetValueOrDefault() & nullable4.HasValue & nullable3.HasValue)
        throw new PXSetPropertyException("For lot/serial-tracked items, you can deallocate only the full quantity or the quantity allocated without lot/serial numbers. If you need to deallocate a partial quantity that includes lot/serial numbers, use the Line Details dialog box on the Details tab of the Sales Orders (SO301000) form.");
    }
    return (PXSetPropertyException) null;
  }

  protected virtual void ResetAllocations()
  {
    ((PXSelectBase) this.Allocations).Cache.Clear();
    ((PXSelectBase) this.Allocations).Cache.ClearQueryCache();
  }

  protected virtual void SetProcessDelegate(SalesAllocationsFilter filter)
  {
    PXProcessingBase<SalesAllocation>.ProcessListDelegate processListDelegate = (PXProcessingBase<SalesAllocation>.ProcessListDelegate) null;
    Func<SalesAllocation, bool> func = (Func<SalesAllocation, bool>) null;
    switch (filter.Action)
    {
      case "Alloc":
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        processListDelegate = ManageSalesAllocations.\u003C\u003Ec.\u003C\u003E9__29_0 ?? (ManageSalesAllocations.\u003C\u003Ec.\u003C\u003E9__29_0 = new PXProcessingBase<SalesAllocation>.ProcessListDelegate((object) ManageSalesAllocations.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CSetProcessDelegate\u003Eb__29_0)));
        func = (Func<SalesAllocation, bool>) (s =>
        {
          Decimal? qtyToAllocate = s.QtyToAllocate;
          Decimal num = 0M;
          return qtyToAllocate.GetValueOrDefault() == num & qtyToAllocate.HasValue;
        });
        break;
      case "Dealloc":
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        processListDelegate = ManageSalesAllocations.\u003C\u003Ec.\u003C\u003E9__29_2 ?? (ManageSalesAllocations.\u003C\u003Ec.\u003C\u003E9__29_2 = new PXProcessingBase<SalesAllocation>.ProcessListDelegate((object) ManageSalesAllocations.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CSetProcessDelegate\u003Eb__29_2)));
        func = (Func<SalesAllocation, bool>) (s =>
        {
          Decimal? qtyToDeallocate = s.QtyToDeallocate;
          Decimal num = 0M;
          return qtyToDeallocate.GetValueOrDefault() == num & qtyToDeallocate.HasValue;
        });
        break;
    }
    if (processListDelegate != null)
      ((PXProcessingBase<SalesAllocation>) this.Allocations).SetProcessDelegate(processListDelegate);
    if (func == null)
      return;
    this.Allocations.SkipProcessing = func;
  }

  protected virtual void SetFieldsVisibility(SalesAllocationsFilter filter)
  {
    bool flag = filter.Action == "Dealloc";
    PXUIFieldAttribute.SetVisible<SalesAllocation.qtyAllocated>(((PXSelectBase) this.Allocations).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<SalesAllocation.qtyUnallocated>(((PXSelectBase) this.Allocations).Cache, (object) null, !flag);
  }

  protected virtual void SetOrderStatusList(SalesAllocationsFilter filter)
  {
    SalesAllocationsFilter.orderStatus.ListAttribute listAttribute = filter?.Action == "Dealloc" ? (SalesAllocationsFilter.orderStatus.ListAttribute) new SalesAllocationsFilter.orderStatus.ListAttribute.WithExpiredAttribute() : new SalesAllocationsFilter.orderStatus.ListAttribute();
    PXStringListAttribute.SetList<SalesAllocationsFilter.orderStatus>(((PXSelectBase) this.Filter).Cache, (object) filter, (PXStringListAttribute) listAttribute);
  }

  protected virtual void SetOrderTypeList()
  {
    (string, string)[] array = ((IEnumerable<string>) ManageSalesAllocations.AvailableOrderTypes.GetOrderTypes()).Select<string, (string, string)>((Func<string, (string, string)>) (ot =>
    {
      SOOrderType soOrderType = SOOrderType.PK.Find((PXGraph) this, ot);
      return string.IsNullOrEmpty(soOrderType?.Descr) ? (ot, ot) : (ot, $"{ot} - {soOrderType.Descr}");
    })).ToArray<(string, string)>();
    PXStringListAttribute.SetLocalizable<SalesAllocationsFilter.orderType>(((PXSelectBase) this.Filter).Cache, (object) null, false);
    PXStringListAttribute.SetList<SalesAllocationsFilter.orderType>(((PXSelectBase) this.Filter).Cache, (object) null, array);
  }

  protected virtual void NormalizeOrderStatusValue(SalesAllocationsFilter filter)
  {
    if (string.IsNullOrEmpty(filter?.OrderStatus))
      return;
    PXStringListAttribute stringListAttribute = ((PXSelectBase) this.Filter).Cache.GetAttributes<SalesAllocationsFilter.orderStatus>((object) filter).OfType<PXStringListAttribute>().FirstOrDefault<PXStringListAttribute>();
    if (stringListAttribute == null)
      return;
    string[] availableStatuses = stringListAttribute.GetAllowedValues(((PXSelectBase) this.Filter).Cache);
    List<string> list = ((IEnumerable<string>) filter.OrderStatus.Split(new string[1]
    {
      ","
    }, StringSplitOptions.RemoveEmptyEntries)).ToList<string>();
    if (list.RemoveAll((Predicate<string>) (x => !((IEnumerable<string>) availableStatuses).Contains<string>(x))) <= 0)
      return;
    filter.OrderStatus = string.Join(",", (IEnumerable<string>) list);
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    return true;
  }

  public virtual bool RowImporting(string viewName, object row) => row == null;

  public virtual bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public virtual void ImportDone(PXImportAttribute.ImportMode.Value mode)
  {
  }

  public class AllocationsProcessing : PXFilteredProcessing<SalesAllocation, SalesAllocationsFilter>
  {
    public Func<SalesAllocation, bool> SkipProcessing;

    public AllocationsProcessing(PXGraph graph)
      : base(graph)
    {
    }

    public AllocationsProcessing(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }

    protected virtual bool startPendingProcess(List<SalesAllocation> items)
    {
      if (this.SkipProcessing != null)
        items = items.Where<SalesAllocation>((Func<SalesAllocation, bool>) (x => !this.SkipProcessing(x))).ToList<SalesAllocation>();
      return ((PXFilteredProcessingBase<SalesAllocation, SalesAllocationsFilter>) this).startPendingProcess(items);
    }
  }

  private class AvailableOrderTypes : IPrefetchable, IPXCompanyDependent
  {
    private string[] _orderTypes;

    public static string[] GetOrderTypes()
    {
      return PXDatabase.GetSlot<ManageSalesAllocations.AvailableOrderTypes>(nameof (AvailableOrderTypes), new Type[2]
      {
        typeof (SOOrderType),
        typeof (SOOrderTypeOperation)
      })._orderTypes;
    }

    void IPrefetchable.Prefetch()
    {
      HashSet<string> source = new HashSet<string>();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<SOOrderType>(Yaql.join<SOOrderTypeOperation>(Yaql.and(Yaql.and(Yaql.and(Yaql.and(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<SOOrderTypeOperation.orderType>((string) null), Yaql.column<SOOrderType.orderType>((string) null)), Yaql.eq<string>((YaqlScalar) Yaql.column<SOOrderTypeOperation.operation>((string) null), "I")), Yaql.eq<YaqlScalar>((YaqlScalar) Yaql.column<SOOrderTypeOperation.active>((string) null), Yaql.@true)), Yaql.eq<YaqlScalar>((YaqlScalar) Yaql.column<SOOrderType.active>((string) null), Yaql.@true)), Yaql.isIn<string>((YaqlScalar) Yaql.column<SOOrderType.behavior>((string) null), SetOfConstants<string, Equal<IBqlOperand>, SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<SOBehavior.bL, SOBehavior.sO, SOBehavior.tR, SOBehavior.rM>>.Provider>.Set)), (YaqlJoinType) 0), new PXDataField[1]
      {
        (PXDataField) new PXAliasedDataField<SOOrderType.orderType>()
      }))
      {
        string str = pxDataRecord.GetString(0);
        if (!string.IsNullOrEmpty(str))
          source.Add(str);
      }
      this._orderTypes = source.ToArray<string>();
    }
  }
}
