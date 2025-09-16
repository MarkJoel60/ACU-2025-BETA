// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INDeadStockEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN.DAC.Unbound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN;

[TableAndChartDashboardType]
public class INDeadStockEnq : PXGraph<
#nullable disable
INDeadStockEnq>
{
  public PXCancel<INDeadStockEnqFilter> Cancel;
  public PXFilter<INDeadStockEnqFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXSelect<INDeadStockEnqResult> Result;
  public PXSetup<PX.Objects.GL.Company> Company;

  public INDeadStockEnq()
  {
    ((PXSelectBase) this.Result).AllowDelete = false;
    ((PXSelectBase) this.Result).AllowInsert = false;
    ((PXSelectBase) this.Result).AllowUpdate = false;
  }

  protected virtual IEnumerable result()
  {
    INDeadStockEnqFilter current = ((PXSelectBase<INDeadStockEnqFilter>) this.Filter).Current;
    if (!this.ValidateFilter(current))
      return (IEnumerable) new INDeadStockEnqResult[0];
    DateTime? inStockSince;
    DateTime? noSalesSince;
    this.GetStartDates(current, out inStockSince, out noSalesSince);
    PXSelectBase<INSiteStatusByCostCenter> command = this.CreateCommand();
    List<object> objectList = this.AddFilters(current, command, inStockSince, noSalesSince);
    INDeadStockEnqResult rowByPrimaryKeys = this.GetRowByPrimaryKeys(command, current, inStockSince, noSalesSince);
    if (rowByPrimaryKeys != null)
      return (IEnumerable) new INDeadStockEnqResult[1]
      {
        rowByPrimaryKeys
      };
    bool flag = this.ValidateViewSortsFilters();
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    pxDelegateResult.IsResultFiltered = !flag;
    pxDelegateResult.IsResultSorted = !flag;
    int num = 0;
    foreach (PXResult<INSiteStatusByCostCenter> selectResult in command.Select(objectList.ToArray()))
    {
      INDeadStockEnqResult deadStockEnqResult = this.MakeResult(selectResult, inStockSince, noSalesSince);
      if (deadStockEnqResult != null)
      {
        ((List<object>) pxDelegateResult).Add((object) new PXResult<INDeadStockEnqResult, InventoryItem>(deadStockEnqResult, ((PXResult) selectResult).GetItem<InventoryItem>()));
        ++num;
        if (PXView.MaximumRows > 0 && !flag)
        {
          if (PXView.StartRow + PXView.MaximumRows <= num)
            break;
        }
      }
    }
    return (IEnumerable) pxDelegateResult;
  }

  protected virtual bool ValidateFilter(INDeadStockEnqFilter filter)
  {
    return filter != null && filter.SiteID.HasValue && filter.SelectBy != null && (!(filter.SelectBy == "Days") || filter.InStockDays.HasValue || filter.NoSalesDays.HasValue) && (!(filter.SelectBy == "Date") || filter.InStockSince.HasValue || filter.NoSalesSince.HasValue);
  }

  protected virtual void GetStartDates(
    INDeadStockEnqFilter filter,
    out DateTime? inStockSince,
    out DateTime? noSalesSince)
  {
    switch (filter.SelectBy)
    {
      case "Days":
        inStockSince = !filter.InStockDays.HasValue ? new DateTime?() : new DateTime?(this.GetCurrentDate().AddDays((double) (-1 * filter.InStockDays.Value)));
        noSalesSince = !filter.NoSalesDays.HasValue ? new DateTime?() : new DateTime?(this.GetCurrentDate().AddDays((double) (-1 * filter.NoSalesDays.Value)));
        break;
      case "Date":
        inStockSince = filter.InStockSince;
        noSalesSince = filter.NoSalesSince;
        break;
      default:
        throw new NotImplementedException();
    }
  }

  protected virtual DateTime GetCurrentDate()
  {
    return ((PXGraph) this).Accessinfo.BusinessDate.Value.Date;
  }

  protected virtual bool ValidateViewSortsFilters()
  {
    PXView.PXFilterRowCollection filters = PXView.Filters;
    if ((filters != null ? filters.Length : 0) != 0)
      return true;
    string[] sortColumns = PXView.SortColumns;
    if ((sortColumns != null ? sortColumns.Length : 0) != 0)
    {
      if (PXView.SortColumns.Length == ((PXSelectBase) this.Result).Cache.Keys.Count && ((IEnumerable<string>) PXView.SortColumns).SequenceEqual<string>((IEnumerable<string>) ((PXSelectBase) this.Result).Cache.Keys, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
      {
        bool[] descendings = PXView.Descendings;
        if ((descendings != null ? (((IEnumerable<bool>) descendings).Any<bool>((Func<bool, bool>) (d => d)) ? 1 : 0) : 0) == 0)
          goto label_6;
      }
      return true;
    }
label_6:
    if (PXView.ReverseOrder)
      return true;
    object[] searches = PXView.Searches;
    return (searches != null ? (((IEnumerable<object>) searches).Any<object>((Func<object, bool>) (v => v != null)) ? 1 : 0) : 0) != 0;
  }

  protected virtual PXSelectBase<INSiteStatusByCostCenter> CreateCommand()
  {
    return (PXSelectBase<INSiteStatusByCostCenter>) new PXViewOf<INSiteStatusByCostCenter>.BasedOn<SelectFromBase<INSiteStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<InventoryItem>.On<INSiteStatusByCostCenter.FK.InventoryItem>>>.Where<BqlOperand<INSiteStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<CostCenter.freeStock>>.Order<By<BqlField<InventoryItem.inventoryCD, IBqlString>.Asc>>>.ReadOnly((PXGraph) this);
  }

  protected virtual List<object> AddFilters(
    INDeadStockEnqFilter filter,
    PXSelectBase<INSiteStatusByCostCenter> command,
    DateTime? inStockSince,
    DateTime? noSalesSince)
  {
    List<object> parameters = new List<object>();
    this.AddQtyOnHandFilter(command);
    this.AddSiteFilter(command, filter);
    this.AddInventoryFilter(command, filter);
    this.AddItemClassFilter(command, filter);
    this.AddNoSalesSinceFilter(command, parameters, noSalesSince);
    return parameters;
  }

  protected virtual void AddQtyOnHandFilter(PXSelectBase<INSiteStatusByCostCenter> command)
  {
    command.WhereAnd<Where<BqlOperand<INSiteStatusByCostCenter.qtyOnHand, IBqlDecimal>.IsGreater<decimal0>>>();
    Type[] negativePlanFields = this.GetNegativePlanFields();
    Type lastField = ((IEnumerable<Type>) negativePlanFields).Last<Type>();
    List<Type> typeList = new List<Type>();
    typeList.Add(typeof (Where<,>));
    typeList.AddRange(((IEnumerable<Type>) negativePlanFields).Where<Type>((Func<Type, bool>) (field => field != lastField)).SelectMany<Type, Type>((Func<Type, IEnumerable<Type>>) (field => (IEnumerable<Type>) new Type[2]
    {
      typeof (Add<,>),
      field
    })));
    typeList.Add(lastField);
    typeList.Add(typeof (Less<INSiteStatusByCostCenter.qtyOnHand>));
    Type type = BqlCommand.Compose(typeList.ToArray());
    command.WhereAnd(type);
  }

  protected virtual void AddSiteFilter(
    PXSelectBase<INSiteStatusByCostCenter> command,
    INDeadStockEnqFilter filter)
  {
    if (!filter.SiteID.HasValue)
      return;
    command.WhereAnd<Where<BqlOperand<INSiteStatusByCostCenter.siteID, IBqlInt>.IsEqual<BqlField<INDeadStockEnqFilter.siteID, IBqlInt>.FromCurrent>>>();
  }

  protected virtual void AddInventoryFilter(
    PXSelectBase<INSiteStatusByCostCenter> command,
    INDeadStockEnqFilter filter)
  {
    if (filter.InventoryID.HasValue)
      command.WhereAnd<Where<BqlOperand<INSiteStatusByCostCenter.inventoryID, IBqlInt>.IsEqual<BqlField<INDeadStockEnqFilter.inventoryID, IBqlInt>.FromCurrent>>>();
    command.WhereAnd<Where<BqlOperand<InventoryItem.itemStatus, IBqlString>.IsNotIn<InventoryItemStatus.markedForDeletion, InventoryItemStatus.inactive>>>();
  }

  protected virtual void AddItemClassFilter(
    PXSelectBase<INSiteStatusByCostCenter> command,
    INDeadStockEnqFilter filter)
  {
    if (!filter.ItemClassID.HasValue)
      return;
    command.WhereAnd<Where<BqlOperand<InventoryItem.itemClassID, IBqlInt>.IsEqual<BqlField<INDeadStockEnqFilter.itemClassID, IBqlInt>.FromCurrent>>>();
  }

  protected virtual void AddNoSalesSinceFilter(
    PXSelectBase<INSiteStatusByCostCenter> command,
    List<object> parameters,
    DateTime? noSalesSince)
  {
    if (!noSalesSince.HasValue)
      return;
    command.WhereAnd<Where<NotExists<SelectFromBase<INItemSiteHistByCostCenterD, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSiteHistByCostCenterD.siteID, Equal<INSiteStatusByCostCenter.siteID>>>>, And<BqlOperand<INItemSiteHistByCostCenterD.inventoryID, IBqlInt>.IsEqual<INSiteStatusByCostCenter.inventoryID>>>, And<BqlOperand<INItemSiteHistByCostCenterD.subItemID, IBqlInt>.IsEqual<INSiteStatusByCostCenter.subItemID>>>, And<BqlOperand<INItemSiteHistByCostCenterD.costCenterID, IBqlInt>.IsEqual<INSiteStatusByCostCenter.costCenterID>>>, And<BqlOperand<INItemSiteHistByCostCenterD.sDate, IBqlDateTime>.IsGreaterEqual<P.AsDateTime>>>>.And<BqlOperand<INItemSiteHistByCostCenterD.qtySales, IBqlDecimal>.IsGreater<decimal0>>>>>>();
    parameters.Add((object) noSalesSince);
  }

  protected virtual INDeadStockEnqResult GetRowByPrimaryKeys(
    PXSelectBase<INSiteStatusByCostCenter> command,
    INDeadStockEnqFilter filter,
    DateTime? inStockSince,
    DateTime? noSalesSince)
  {
    if (PXView.MaximumRows == 1 && PXView.StartRow == 0)
    {
      int? length = PXView.Searches?.Length;
      int count = ((PXSelectBase) this.Result).Cache.Keys.Count;
      if (length.GetValueOrDefault() == count & length.HasValue && PXView.SearchColumns.Select<PXView.PXSearchColumn, string>((Func<PXView.PXSearchColumn, string>) (sc => ((PXView.PXSortColumn) sc).Column)).SequenceEqual<string>((IEnumerable<string>) ((PXSelectBase) this.Result).Cache.Keys, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase) && ((IEnumerable<object>) PXView.Searches).All<object>((Func<object, bool>) (k => k != null)))
      {
        int num1 = 0;
        int num2 = 0;
        using (List<object>.Enumerator enumerator = ((PXSelectBase) command).View.Select(new object[1]
        {
          (object) filter
        }, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref num1, PXView.MaximumRows, ref num2).GetEnumerator())
        {
          if (enumerator.MoveNext())
          {
            object current = enumerator.Current;
            return current is PXResult ? this.MakeResult(current as PXResult<INSiteStatusByCostCenter>, inStockSince, noSalesSince) : this.MakeResult(new PXResult<INSiteStatusByCostCenter>(current as INSiteStatusByCostCenter), inStockSince, noSalesSince);
          }
        }
      }
    }
    return (INDeadStockEnqResult) null;
  }

  protected virtual INDeadStockEnqResult MakeResult(
    PXResult<INSiteStatusByCostCenter> selectResult,
    DateTime? inStockSince,
    DateTime? noSalesSince)
  {
    INSiteStatusByCostCenter siteStatus = PXResult<INSiteStatusByCostCenter>.op_Implicit(selectResult);
    INItemSiteHistByCostCenterD currentHistoryRecord = this.GetCurrentHistoryRecord(siteStatus, inStockSince, noSalesSince);
    Decimal valueOrDefault = ((Decimal?) currentHistoryRecord?.EndQty).GetValueOrDefault();
    if (valueOrDefault <= 0M)
      return (INDeadStockEnqResult) null;
    Decimal? negativeQty = this.GetNegativeQty(siteStatus, inStockSince, noSalesSince);
    Decimal deadStockQty = valueOrDefault - negativeQty.GetValueOrDefault();
    if (deadStockQty <= 0M)
      return (INDeadStockEnqResult) null;
    INItemStats inItemStats = INItemStats.PK.Find((PXGraph) this, siteStatus.InventoryID, siteStatus.SiteID);
    INDeadStockEnqResult result = new INDeadStockEnqResult()
    {
      BaseCuryID = ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID,
      DeadStockQty = new Decimal?(deadStockQty),
      InStockQty = siteStatus.QtyOnHand,
      SiteID = siteStatus.SiteID,
      LastCost = (Decimal?) inItemStats?.LastCost,
      LastSaleDate = this.GetLastSaleDate(siteStatus),
      LastPurchaseDate = (DateTime?) inItemStats?.LastPurchaseDate,
      InventoryID = siteStatus.InventoryID,
      SubItemID = siteStatus.SubItemID
    };
    this.CalculateDeadStockValues(result, siteStatus, currentHistoryRecord, deadStockQty);
    return result;
  }

  protected virtual INItemSiteHistByCostCenterD GetCurrentHistoryRecord(
    INSiteStatusByCostCenter siteStatus,
    DateTime? inStockSince,
    DateTime? noSalesSince)
  {
    return PXResultset<INItemSiteHistByCostCenterD>.op_Implicit(PXSelectBase<INItemSiteHistByCostCenterD, PXViewOf<INItemSiteHistByCostCenterD>.BasedOn<SelectFromBase<INItemSiteHistByCostCenterD, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSiteHistByCostCenterD.siteID, Equal<BqlField<INSiteStatusByCostCenter.siteID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<INItemSiteHistByCostCenterD.inventoryID, IBqlInt>.IsEqual<BqlField<INSiteStatusByCostCenter.inventoryID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INItemSiteHistByCostCenterD.subItemID, IBqlInt>.IsEqual<BqlField<INSiteStatusByCostCenter.subItemID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INItemSiteHistByCostCenterD.costCenterID, IBqlInt>.IsEqual<BqlField<INSiteStatusByCostCenter.costCenterID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<INItemSiteHistByCostCenterD.sDate, IBqlDateTime>.IsLessEqual<P.AsDateTime>>>.Order<By<BqlField<INItemSiteHistByCostCenterD.sDate, IBqlDateTime>.Desc>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) siteStatus
    }, new object[1]
    {
      (object) (inStockSince ?? noSalesSince)
    }));
  }

  protected virtual Decimal? GetNegativeQty(
    INSiteStatusByCostCenter siteStatus,
    DateTime? inStockSince,
    DateTime? noSalesSince)
  {
    PXCache siteStatusCache = ((PXGraph) this).Caches[typeof (INSiteStatusByCostCenter)];
    return new Decimal?(((IEnumerable<Type>) this.GetNegativePlanFields()).Sum<Type>((Func<Type, Decimal?>) (field => (Decimal?) siteStatusCache.GetValue((object) siteStatus, field.Name))).GetValueOrDefault() + ((Decimal?) PXResultset<INItemSiteHistByCostCenterD>.op_Implicit(PXSelectBase<INItemSiteHistByCostCenterD, PXViewOf<INItemSiteHistByCostCenterD>.BasedOn<SelectFromBase<INItemSiteHistByCostCenterD, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSiteHistByCostCenterD.siteID, Equal<BqlField<INSiteStatusByCostCenter.siteID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<INItemSiteHistByCostCenterD.inventoryID, IBqlInt>.IsEqual<BqlField<INSiteStatusByCostCenter.inventoryID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INItemSiteHistByCostCenterD.subItemID, IBqlInt>.IsEqual<BqlField<INSiteStatusByCostCenter.subItemID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INItemSiteHistByCostCenterD.costCenterID, IBqlInt>.IsEqual<BqlField<INSiteStatusByCostCenter.costCenterID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<INItemSiteHistByCostCenterD.sDate, IBqlDateTime>.IsGreater<P.AsDateTime>>>.Aggregate<To<Sum<INItemSiteHistByCostCenterD.qtyCredit>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) siteStatus
    }, new object[1]
    {
      (object) (inStockSince ?? noSalesSince)
    }))?.QtyCredit).GetValueOrDefault());
  }

  protected virtual Decimal GetPlannedNegativeQty(INSiteStatusByCostCenter siteStatus)
  {
    PXCache siteStatusCache = ((PXGraph) this).Caches[typeof (INSiteStatusByCostCenter)];
    return ((IEnumerable<Type>) this.GetNegativePlanFields()).Sum<Type>((Func<Type, Decimal?>) (field => (Decimal?) siteStatusCache.GetValue((object) siteStatus, field.Name))).GetValueOrDefault();
  }

  protected virtual Type[] GetNegativePlanFields()
  {
    return new Type[11]
    {
      typeof (INSiteStatusByCostCenter.qtySOBackOrdered),
      typeof (INSiteStatusByCostCenter.qtySOPrepared),
      typeof (INSiteStatusByCostCenter.qtySOBooked),
      typeof (INSiteStatusByCostCenter.qtySOShipping),
      typeof (INSiteStatusByCostCenter.qtySOShipped),
      typeof (INSiteStatusByCostCenter.qtyINIssues),
      typeof (INSiteStatusByCostCenter.qtyFSSrvOrdPrepared),
      typeof (INSiteStatusByCostCenter.qtyFSSrvOrdBooked),
      typeof (INSiteStatusByCostCenter.qtyFSSrvOrdAllocated),
      typeof (INSiteStatusByCostCenter.qtyINAssemblyDemand),
      typeof (INSiteStatusByCostCenter.qtyProductionDemand)
    };
  }

  protected virtual INItemSiteHistByCostCenterD GetHistoryRecordByQuantity(
    INSiteStatusByCostCenter siteStatus,
    DateTime? date,
    Decimal begQty)
  {
    return PXResultset<INItemSiteHistByCostCenterD>.op_Implicit(PXSelectBase<INItemSiteHistByCostCenterD, PXViewOf<INItemSiteHistByCostCenterD>.BasedOn<SelectFromBase<INItemSiteHistByCostCenterD, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSiteHistByCostCenterD.siteID, Equal<BqlField<INSiteStatusByCostCenter.siteID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<INItemSiteHistByCostCenterD.inventoryID, IBqlInt>.IsEqual<BqlField<INSiteStatusByCostCenter.inventoryID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INItemSiteHistByCostCenterD.subItemID, IBqlInt>.IsEqual<BqlField<INSiteStatusByCostCenter.subItemID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INItemSiteHistByCostCenterD.costCenterID, IBqlInt>.IsEqual<BqlField<INSiteStatusByCostCenter.costCenterID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INItemSiteHistByCostCenterD.begQty, IBqlDecimal>.IsLessEqual<P.AsDecimal>>>, And<BqlOperand<INItemSiteHistByCostCenterD.endQty, IBqlDecimal>.IsGreater<P.AsDecimal>>>>.And<BqlOperand<INItemSiteHistByCostCenterD.sDate, IBqlDateTime>.IsLessEqual<P.AsDateTime>>>.Order<By<BqlField<INItemSiteHistByCostCenterD.sDate, IBqlDateTime>.Desc>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) siteStatus
    }, new object[3]
    {
      (object) begQty,
      (object) begQty,
      (object) date
    }));
  }

  protected virtual DateTime? GetLastSaleDate(INSiteStatusByCostCenter siteStatus)
  {
    return PXResultset<INItemSiteHistByCostCenterD>.op_Implicit(PXSelectBase<INItemSiteHistByCostCenterD, PXViewOf<INItemSiteHistByCostCenterD>.BasedOn<SelectFromBase<INItemSiteHistByCostCenterD, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSiteHistByCostCenterD.siteID, Equal<BqlField<INSiteStatusByCostCenter.siteID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<INItemSiteHistByCostCenterD.inventoryID, IBqlInt>.IsEqual<BqlField<INSiteStatusByCostCenter.inventoryID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INItemSiteHistByCostCenterD.subItemID, IBqlInt>.IsEqual<BqlField<INSiteStatusByCostCenter.subItemID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INItemSiteHistByCostCenterD.costCenterID, IBqlInt>.IsEqual<BqlField<INSiteStatusByCostCenter.costCenterID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<INItemSiteHistByCostCenterD.qtySales, IBqlDecimal>.IsGreater<decimal0>>>.Aggregate<To<Max<INItemSiteHistByCostCenterD.sDate>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) siteStatus
    }, Array.Empty<object>()))?.SDate;
  }

  protected virtual void CalculateDeadStockValues(
    INDeadStockEnqResult result,
    INSiteStatusByCostCenter siteStatus,
    INItemSiteHistByCostCenterD currentRow,
    Decimal deadStockQty)
  {
    Decimal begQty = currentRow.EndQty.Value - deadStockQty;
    INItemSiteHistByCostCenterD recordByQuantity = this.GetHistoryRecordByQuantity(siteStatus, currentRow.SDate, begQty);
    if (recordByQuantity == null)
    {
      this.OnNotEnoughHistoryRecords(siteStatus, currentRow, deadStockQty, -1M);
    }
    else
    {
      Decimal? endCost1 = recordByQuantity.EndCost;
      Decimal? nullable1 = recordByQuantity.CostDebit;
      Decimal? nullable2 = endCost1.HasValue & nullable1.HasValue ? new Decimal?(endCost1.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable3 = recordByQuantity.CostCredit;
      Decimal? nullable4;
      if (!(nullable2.HasValue & nullable3.HasValue))
      {
        nullable1 = new Decimal?();
        nullable4 = nullable1;
      }
      else
        nullable4 = new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault());
      nullable1 = nullable4;
      Decimal num1 = Math.Max(nullable1.GetValueOrDefault(), 0M);
      Decimal num2 = Math.Max(recordByQuantity.BegQty.GetValueOrDefault(), 0M);
      Decimal num3 = recordByQuantity.EndQty.GetValueOrDefault() - num2 == 0M ? 0M : (recordByQuantity.EndCost.GetValueOrDefault() - num1) / (recordByQuantity.EndQty.GetValueOrDefault() - num2);
      Decimal num4 = num1 + num3 * (begQty - num2);
      INDeadStockEnqResult deadStockEnqResult = result;
      Decimal? endCost2 = currentRow.EndCost;
      Decimal num5 = num4;
      Decimal? nullable5;
      if (!endCost2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable5 = nullable3;
      }
      else
        nullable5 = new Decimal?(endCost2.GetValueOrDefault() - num5);
      deadStockEnqResult.TotalDeadStockCost = nullable5;
      Decimal deadStockQtyCounter = deadStockQty;
      result.InDeadStockDays = new Decimal?(0M);
      foreach (INItemSiteHistByCostCenterD lastHistoryRecord in this.GetLastHistoryRecords(siteStatus, deadStockQty, currentRow))
      {
        if (!(lastHistoryRecord.QtyDebit.GetValueOrDefault() == 0M) && this.CalculateDeadStockValues(ref deadStockQtyCounter, result, lastHistoryRecord))
          return;
      }
      this.OnNotEnoughHistoryRecords(siteStatus, currentRow, deadStockQty, deadStockQtyCounter);
    }
  }

  protected virtual IEnumerable<INItemSiteHistByCostCenterD> GetLastHistoryRecords(
    INSiteStatusByCostCenter siteStatus,
    Decimal deadStockQty,
    INItemSiteHistByCostCenterD currentRow)
  {
    PXViewOf<INItemSiteHistByCostCenterD>.BasedOn<SelectFromBase<INItemSiteHistByCostCenterD, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSiteHistByCostCenterD.siteID, Equal<P.AsInt>>>>, And<BqlOperand<INItemSiteHistByCostCenterD.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INItemSiteHistByCostCenterD.subItemID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INItemSiteHistByCostCenterD.costCenterID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INItemSiteHistByCostCenterD.sDate, IBqlDateTime>.IsLess<P.AsDateTime>>>>.And<BqlOperand<INItemSiteHistByCostCenterD.qtyDebit, IBqlDecimal>.IsGreater<decimal0>>>.Order<By<BqlField<INItemSiteHistByCostCenterD.sDate, IBqlDateTime>.Desc>>>.ReadOnly getRows = new PXViewOf<INItemSiteHistByCostCenterD>.BasedOn<SelectFromBase<INItemSiteHistByCostCenterD, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSiteHistByCostCenterD.siteID, Equal<P.AsInt>>>>, And<BqlOperand<INItemSiteHistByCostCenterD.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INItemSiteHistByCostCenterD.subItemID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INItemSiteHistByCostCenterD.costCenterID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INItemSiteHistByCostCenterD.sDate, IBqlDateTime>.IsLess<P.AsDateTime>>>>.And<BqlOperand<INItemSiteHistByCostCenterD.qtyDebit, IBqlDecimal>.IsGreater<decimal0>>>.Order<By<BqlField<INItemSiteHistByCostCenterD.sDate, IBqlDateTime>.Desc>>>.ReadOnly((PXGraph) this);
    DateTime? lastDate = currentRow.SDate;
    Decimal deadStockQtyCounter = deadStockQty;
    yield return currentRow;
    while (lastDate.HasValue && deadStockQtyCounter > 0M)
    {
      PXResultset<INItemSiteHistByCostCenterD> pxResultset = ((PXSelectBase<INItemSiteHistByCostCenterD>) getRows).SelectWindowed(0, 1000, new object[5]
      {
        (object) siteStatus.SiteID,
        (object) siteStatus.InventoryID,
        (object) siteStatus.SubItemID,
        (object) siteStatus.CostCenterID,
        (object) lastDate
      });
      lastDate = new DateTime?();
      foreach (PXResult<INItemSiteHistByCostCenterD> pxResult in pxResultset)
      {
        INItemSiteHistByCostCenterD newRow = PXResult<INItemSiteHistByCostCenterD>.op_Implicit(pxResult);
        yield return newRow;
        lastDate = newRow.SDate;
        deadStockQtyCounter -= newRow.QtyDebit.GetValueOrDefault();
        if (!(deadStockQtyCounter <= 0M))
          newRow = (INItemSiteHistByCostCenterD) null;
        else
          break;
      }
    }
  }

  protected virtual bool CalculateDeadStockValues(
    ref Decimal deadStockQtyCounter,
    INDeadStockEnqResult result,
    INItemSiteHistByCostCenterD lastRow)
  {
    Decimal num1 = lastRow.QtyDebit.Value;
    Decimal num2 = deadStockQtyCounter >= num1 ? 1M : deadStockQtyCounter / num1;
    Decimal totalDays = (Decimal) this.GetCurrentDate().Subtract(lastRow.SDate.Value.Date).TotalDays;
    INDeadStockEnqResult deadStockEnqResult1 = result;
    Decimal? inDeadStockDays = deadStockEnqResult1.InDeadStockDays;
    Decimal num3 = totalDays * num1 * num2;
    deadStockEnqResult1.InDeadStockDays = inDeadStockDays.HasValue ? new Decimal?(inDeadStockDays.GetValueOrDefault() + num3) : new Decimal?();
    deadStockQtyCounter -= num1;
    if (!(deadStockQtyCounter <= 0M))
      return false;
    INDeadStockEnqResult deadStockEnqResult2 = result;
    Decimal? totalDeadStockCost = result.TotalDeadStockCost;
    Decimal? nullable1 = result.DeadStockQty;
    Decimal? nullable2 = totalDeadStockCost.HasValue & nullable1.HasValue ? new Decimal?(totalDeadStockCost.GetValueOrDefault() / nullable1.GetValueOrDefault()) : new Decimal?();
    deadStockEnqResult2.AverageItemCost = nullable2;
    INDeadStockEnqResult deadStockEnqResult3 = result;
    nullable1 = deadStockEnqResult3.InDeadStockDays;
    Decimal? deadStockQty = result.DeadStockQty;
    deadStockEnqResult3.InDeadStockDays = nullable1.HasValue & deadStockQty.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / deadStockQty.GetValueOrDefault()) : new Decimal?();
    return true;
  }

  protected virtual void OnNotEnoughHistoryRecords(
    INSiteStatusByCostCenter siteStatus,
    INItemSiteHistByCostCenterD currentRow,
    Decimal deadStockQty,
    Decimal deadStockQtyCounter)
  {
    PXTrace.WriteError((Exception) new RowNotFoundException(((PXGraph) this).Caches[typeof (INItemSiteHist)], new object[6]
    {
      (object) siteStatus.SiteID,
      (object) siteStatus.InventoryID,
      (object) siteStatus.SubItemID,
      (object) currentRow.SDate,
      (object) deadStockQty,
      (object) deadStockQtyCounter
    }));
  }
}
