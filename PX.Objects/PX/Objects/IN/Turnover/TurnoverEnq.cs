// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Turnover.TurnoverEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.IN.GraphExtensions.TurnoverEnqExt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN.Turnover;

[TableAndChartDashboardType]
public class TurnoverEnq : 
  PXGraph<
  #nullable disable
  TurnoverEnq>,
  PXImportAttribute.IPXPrepareItems,
  PXImportAttribute.IPXProcess
{
  public PXAction<INTurnoverEnqFilter> refresh;
  public PXCancel<INTurnoverEnqFilter> Cancel;
  public PXAction<INTurnoverEnqFilter> previousPeriod;
  public PXAction<INTurnoverEnqFilter> nextPeriod;
  public PXAction<INTurnoverEnqFilter> calculateTurnover;
  public PXAction<INTurnoverEnqFilter> manageTurnoverHistory;
  public PXFilter<INTurnoverEnqFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXViewOf<TurnoverCalcItem>.BasedOn<SelectFromBase<TurnoverCalcItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  TurnoverCalcItem.branchID, 
  #nullable disable
  Inside<BqlField<
  #nullable enable
  INTurnoverEnqFilter.orgBAccountID, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  TurnoverCalcItem.fromPeriodID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INTurnoverEnqFilter.fromPeriodID, IBqlString>.FromCurrent>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  TurnoverCalcItem.toPeriodID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INTurnoverEnqFilter.toPeriodID, IBqlString>.FromCurrent>>>.Order<
  #nullable disable
  By<Asc<TurnoverCalcItem.inventoryCD>, Asc<TurnoverCalcItem.siteCD>>>>.ReadOnly TurnoverCalcItems;
  public PXSetup<INSetup> insetup;

  public InventoryLinkFilterExt InventoryLinkFilterExt
  {
    get => ((PXGraph) this).FindImplementation<InventoryLinkFilterExt>();
  }

  public virtual bool IsDirty => false;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [PXUIField]
  [PXButton(ImageKey = "Refresh")]
  public virtual IEnumerable Refresh(PXAdapter adapter)
  {
    this.FindTurnoverCalc(((PXSelectBase<INTurnoverEnqFilter>) this.Filter).Current);
    return adapter.Get();
  }

  [PXUIField]
  [PXPreviousButton]
  public virtual IEnumerable PreviousPeriod(PXAdapter adapter)
  {
    INTurnoverEnqFilter current = ((PXSelectBase<INTurnoverEnqFilter>) this.Filter).Current;
    int? calendarOrganizationId = this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, new bool?(false));
    FinPeriod prevPeriod1 = this.FinPeriodRepository.FindPrevPeriod(calendarOrganizationId, current.FromPeriodID, true);
    FinPeriod prevPeriod2 = this.FinPeriodRepository.FindPrevPeriod(calendarOrganizationId, current.ToPeriodID, true);
    if (prevPeriod1 != null && prevPeriod2 != null)
    {
      current.FromPeriodID = prevPeriod1.FinPeriodID;
      current.ToPeriodID = prevPeriod2.FinPeriodID;
      this.FindTurnoverCalc(current);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXNextButton]
  public virtual IEnumerable NextPeriod(PXAdapter adapter)
  {
    INTurnoverEnqFilter current = ((PXSelectBase<INTurnoverEnqFilter>) this.Filter).Current;
    int? calendarOrganizationId = this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, new bool?(false));
    FinPeriod nextPeriod1 = this.FinPeriodRepository.FindNextPeriod(calendarOrganizationId, current.FromPeriodID, true);
    FinPeriod nextPeriod2 = this.FinPeriodRepository.FindNextPeriod(calendarOrganizationId, current.ToPeriodID, true);
    if (nextPeriod1 != null && nextPeriod2 != null)
    {
      current.FromPeriodID = nextPeriod1.FinPeriodID;
      current.ToPeriodID = nextPeriod2.FinPeriodID;
      this.FindTurnoverCalc(current);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CalculateTurnover(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) new TurnoverEnq.\u003C\u003Ec__DisplayClass16_0()
    {
      clone = GraphHelper.Clone<TurnoverEnq>(this)
    }, __methodptr(\u003CCalculateTurnover\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ManageTurnoverHistory(PXAdapter adapter)
  {
    INTurnoverEnqFilter current = ((PXSelectBase<INTurnoverEnqFilter>) this.Filter).Current;
    ManageTurnover instance = PXGraph.CreateInstance<ManageTurnover>();
    ((PXSelectBase<INTurnoverCalcFilter>) instance.Filter).SetValueExt<INTurnoverCalcFilter.action>(((PXSelectBase<INTurnoverCalcFilter>) instance.Filter).Current, (object) "CALC");
    if (current.OrgBAccountID.HasValue)
      ((PXSelectBase<INTurnoverCalcFilter>) instance.Filter).SetValueExt<INTurnoverCalcFilter.orgBAccountID>(((PXSelectBase<INTurnoverCalcFilter>) instance.Filter).Current, (object) current.OrgBAccountID);
    if (current.FromPeriodID != null)
      ((PXSelectBase) instance.Filter).Cache.SetValue<INTurnoverCalcFilter.fromPeriodID>((object) ((PXSelectBase<INTurnoverCalcFilter>) instance.Filter).Current, (object) current.FromPeriodID);
    if (current.ToPeriodID != null)
      ((PXSelectBase) instance.Filter).Cache.SetValue<INTurnoverCalcFilter.toPeriodID>((object) ((PXSelectBase<INTurnoverCalcFilter>) instance.Filter).Current, (object) current.ToPeriodID);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Turnover Calculation");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
    throw requiredException;
  }

  protected virtual IEnumerable turnoverCalcItems()
  {
    INTurnoverEnqFilter current = ((PXSelectBase<INTurnoverEnqFilter>) this.Filter).Current;
    if (!current.IsSuitableCalcsFound.GetValueOrDefault())
      return (IEnumerable) Array<TurnoverCalcItem>.Empty;
    BqlCommand bqlSelect = ((PXSelectBase) this.TurnoverCalcItems).View.BqlSelect;
    List<object> parameters = new List<object>();
    PXView pxView = new PXView((PXGraph) this, ((PXSelectBase) this.TurnoverCalcItems).View.IsReadOnly, this.AppendFilter(bqlSelect, (IList<object>) parameters, current));
    int startRow = PXView.StartRow;
    int num = 0;
    object[] currents = PXView.Currents;
    object[] array = parameters.ToArray();
    object[] searches = PXView.Searches;
    string[] sortColumns = PXView.SortColumns;
    bool[] descendings = PXView.Descendings;
    PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
    ref int local1 = ref startRow;
    int maximumRows = PXView.MaximumRows;
    ref int local2 = ref num;
    List<object> objectList = pxView.Select(currents, array, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2);
    PXView.StartRow = 0;
    return (IEnumerable) objectList;
  }

  public TurnoverEnq()
  {
    INSetup current = ((PXSelectBase<INSetup>) this.insetup).Current;
  }

  protected virtual void _(PX.Data.Events.RowSelected<INTurnoverEnqFilter> e)
  {
    if (e.Row == null)
      return;
    this.SetFilterWarnings(e.Row);
    this.SetCostColumnsDisplayName(e.Row);
  }

  protected virtual void SetFilterWarnings(INTurnoverEnqFilter filter)
  {
    PXCache cache = ((PXSelectBase) this.Filter).Cache;
    PXAccess.MasterCollection.Organization organizationByBaccountId = PXAccess.GetOrganizationByBAccountID(filter.OrgBAccountID);
    PXSetPropertyException propertyException1 = (PXSetPropertyException) null;
    int? nullable1;
    bool? nullable2;
    if (filter.IsPartialSuitableCalcs.GetValueOrDefault())
    {
      nullable1 = filter.BranchID;
      if (nullable1.HasValue)
        propertyException1 = new PXSetPropertyException("The turnover for the {0} - {1} period range has not been calculated yet. To review the turnover, click Calculate Turnover.", (PXErrorLevel) 2, new object[2]
        {
          cache.GetStateExt<INTurnoverEnqFilter.fromPeriodID>((object) filter),
          cache.GetStateExt<INTurnoverEnqFilter.toPeriodID>((object) filter)
        });
      else if (!organizationByBaccountId.IsGroup)
        propertyException1 = new PXSetPropertyException("The turnover for the {0} - {1} period range has not been calculated for all branches of the company. To review the turnover for the company, click Calculate Turnover.", (PXErrorLevel) 2, new object[2]
        {
          cache.GetStateExt<INTurnoverEnqFilter.fromPeriodID>((object) filter),
          cache.GetStateExt<INTurnoverEnqFilter.toPeriodID>((object) filter)
        });
      else
        propertyException1 = new PXSetPropertyException("The turnover for the {0} - {1} period range has not been calculated for all companies of the group. To review the turnover for the group of companies, click Calculate Turnover.", (PXErrorLevel) 2, new object[2]
        {
          cache.GetStateExt<INTurnoverEnqFilter.fromPeriodID>((object) filter),
          cache.GetStateExt<INTurnoverEnqFilter.toPeriodID>((object) filter)
        });
    }
    else
    {
      nullable2 = filter.IsMixedSuitableCalcs;
      if (nullable2.GetValueOrDefault())
      {
        if ((organizationByBaccountId != null ? (!organizationByBaccountId.IsGroup ? 1 : 0) : 1) != 0)
          propertyException1 = new PXSetPropertyException("The turnover calculations for some branches of the {0} company have been filtered by one or more of the following parameters: warehouse, item class, or inventory ID. The data cannot be shown. To review the turnover for the selected company, click Calculate Turnover.", (PXErrorLevel) 2, new object[1]
          {
            cache.GetStateExt<INTurnoverEnqFilter.orgBAccountID>((object) filter)
          });
        else
          propertyException1 = new PXSetPropertyException("The turnover calculations for some branches of the {0} group of companies have been filtered by one or more of the following parameters: warehouse, item class, or inventory ID. The data cannot be shown. To review the turnover for the selected group of companies, click Calculate Turnover.", (PXErrorLevel) 2, new object[1]
          {
            cache.GetStateExt<INTurnoverEnqFilter.orgBAccountID>((object) filter)
          });
      }
    }
    cache.RaiseExceptionHandling<INTurnoverEnqFilter.toPeriodID>((object) filter, (object) filter.ToPeriodID, (Exception) propertyException1);
    PXCache pxCache1 = cache;
    INTurnoverEnqFilter turnoverEnqFilter1 = filter;
    // ISSUE: variable of a boxed type
    __Boxed<int?> siteId = (ValueType) filter.SiteID;
    nullable1 = filter.SuitableCalcsSiteID;
    PXSetPropertyException propertyException2;
    if (!nullable1.HasValue)
      propertyException2 = (PXSetPropertyException) null;
    else
      propertyException2 = new PXSetPropertyException("The turnover for the selected period range has been calculated for the following warehouse: {0}. The calculated data is not complete for the current value of the Warehouse box. To calculate the turnover for the current value, click Calculate Turnover.", (PXErrorLevel) 2, new object[1]
      {
        cache.GetStateExt<INTurnoverEnqFilter.suitableCalcsSiteID>((object) filter)
      });
    pxCache1.RaiseExceptionHandling<INTurnoverEnqFilter.siteID>((object) turnoverEnqFilter1, (object) siteId, (Exception) propertyException2);
    PXCache pxCache2 = cache;
    INTurnoverEnqFilter turnoverEnqFilter2 = filter;
    // ISSUE: variable of a boxed type
    __Boxed<int?> itemClassId = (ValueType) filter.ItemClassID;
    nullable1 = filter.SuitableCalcsItemClassID;
    PXSetPropertyException propertyException3;
    if (!nullable1.HasValue)
      propertyException3 = (PXSetPropertyException) null;
    else
      propertyException3 = new PXSetPropertyException("The turnover for the selected period range has been calculated for the following item class: {0}.The calculated data is not complete for the current value of the Item Class box. To calculate the turnover for the current value, click Calculate Turnover.", (PXErrorLevel) 2, new object[1]
      {
        cache.GetStateExt<INTurnoverEnqFilter.suitableCalcsItemClassID>((object) filter)
      });
    pxCache2.RaiseExceptionHandling<INTurnoverEnqFilter.itemClassID>((object) turnoverEnqFilter2, (object) itemClassId, (Exception) propertyException3);
    nullable1 = filter.SuitableCalcsInventoryID;
    if (nullable1.HasValue)
    {
      cache.RaiseExceptionHandling<INTurnoverEnqFilter.inventoryID>((object) filter, (object) filter.InventoryID, (Exception) new PXSetPropertyException("The turnover for the selected period range has been calculated for the following inventory ID: {0}. The calculated data is not complete for the current value of the Inventory ID box. To calculate the turnover for the current value, click Calculate Turnover.", (PXErrorLevel) 2, new object[1]
      {
        cache.GetStateExt<INTurnoverEnqFilter.suitableCalcsInventoryID>((object) filter)
      }));
    }
    else
    {
      PXCache pxCache3 = cache;
      INTurnoverEnqFilter turnoverEnqFilter3 = filter;
      // ISSUE: variable of a boxed type
      __Boxed<int?> inventoryId = (ValueType) filter.InventoryID;
      nullable2 = filter.IsInventoryListCalc;
      PXSetPropertyException propertyException4 = nullable2.GetValueOrDefault() ? new PXSetPropertyException("The turnover for the selected period range has been calculated for the list of inventory IDs. The calculated data is not complete for the current list of the inventory IDs. To calculate the turnover for the current list, click Calculate Turnover.", (PXErrorLevel) 2) : (PXSetPropertyException) null;
      pxCache3.RaiseExceptionHandling<INTurnoverEnqFilter.inventoryID>((object) turnoverEnqFilter3, (object) inventoryId, (Exception) propertyException4);
    }
  }

  protected virtual void SetCostColumnsDisplayName(INTurnoverEnqFilter filter)
  {
    CurrencyList currency = this.GetCurrency(filter);
    if (string.IsNullOrEmpty(currency?.CurySymbol))
    {
      PXUIFieldAttribute.SetDisplayName(((PXSelectBase) this.TurnoverCalcItems).Cache, "begCost", "Beginning Inventory");
      PXUIFieldAttribute.SetDisplayName(((PXSelectBase) this.TurnoverCalcItems).Cache, "ytdCost", "Ending Inventory");
      PXUIFieldAttribute.SetDisplayName(((PXSelectBase) this.TurnoverCalcItems).Cache, "avgCost", "Average Inventory");
      PXUIFieldAttribute.SetDisplayName(((PXSelectBase) this.TurnoverCalcItems).Cache, "soldCost", "Cost of Goods Sold");
      PXUIFieldAttribute.SetDisplayName(((PXSelectBase) this.TurnoverCalcItems).Cache, "costRatio", "Turnover Ratio");
    }
    else
    {
      PXUIFieldAttribute.SetDisplayNameLocalized(((PXSelectBase) this.TurnoverCalcItems).Cache, "begCost", PXMessages.LocalizeFormatNoPrefix("Beginning Inventory ({0})", new object[1]
      {
        (object) currency.CurySymbol
      }));
      PXUIFieldAttribute.SetDisplayNameLocalized(((PXSelectBase) this.TurnoverCalcItems).Cache, "ytdCost", PXMessages.LocalizeFormatNoPrefix("Ending Inventory ({0})", new object[1]
      {
        (object) currency.CurySymbol
      }));
      PXUIFieldAttribute.SetDisplayNameLocalized(((PXSelectBase) this.TurnoverCalcItems).Cache, "avgCost", PXMessages.LocalizeFormatNoPrefix("Average Inventory ({0})", new object[1]
      {
        (object) currency.CurySymbol
      }));
      PXUIFieldAttribute.SetDisplayNameLocalized(((PXSelectBase) this.TurnoverCalcItems).Cache, "soldCost", PXMessages.LocalizeFormatNoPrefix("Cost of Goods Sold ({0})", new object[1]
      {
        (object) currency.CurySymbol
      }));
      PXUIFieldAttribute.SetDisplayNameLocalized(((PXSelectBase) this.TurnoverCalcItems).Cache, "costRatio", PXMessages.LocalizeFormatNoPrefix("Turnover Ratio ({0})", new object[1]
      {
        (object) currency.CurySymbol
      }));
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INTurnoverEnqFilter, INTurnoverEnqFilter.fromPeriodID> e)
  {
    if (e.Row.ToPeriodID == null || string.CompareOrdinal(e.Row.FromPeriodID, e.Row.ToPeriodID) <= 0)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTurnoverEnqFilter, INTurnoverEnqFilter.fromPeriodID>>) e).Cache.SetValue<INTurnoverEnqFilter.toPeriodID>((object) e.Row, (object) e.Row.FromPeriodID);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INTurnoverEnqFilter, INTurnoverEnqFilter.toPeriodID> e)
  {
    if (e.Row.FromPeriodID == null || string.CompareOrdinal(e.Row.FromPeriodID, e.Row.ToPeriodID) <= 0)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTurnoverEnqFilter, INTurnoverEnqFilter.toPeriodID>>) e).Cache.SetValue<INTurnoverEnqFilter.fromPeriodID>((object) e.Row, (object) e.Row.ToPeriodID);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INTurnoverEnqFilter, INTurnoverEnqFilter.orgBAccountID> e)
  {
    if (this.IsValidValue<INTurnoverEnqFilter.siteID>((object) e.Row))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTurnoverEnqFilter, INTurnoverEnqFilter.orgBAccountID>>) e).Cache.SetValueExt<INTurnoverEnqFilter.siteID>((object) e.Row, (object) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INTurnoverEnqFilter, INTurnoverEnqFilter.itemClassID> e)
  {
    if (this.IsValidValue<INTurnoverEnqFilter.inventoryID>((object) e.Row))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTurnoverEnqFilter, INTurnoverEnqFilter.itemClassID>>) e).Cache.SetValueExt<INTurnoverEnqFilter.inventoryID>((object) e.Row, (object) null);
  }

  public virtual void FindTurnoverCalc(INTurnoverEnqFilter filter)
  {
    if (!filter.OrgBAccountID.HasValue || filter.FromPeriodID == null || filter.ToPeriodID == null)
    {
      found(false);
    }
    else
    {
      int[] branches = this.GetBranches(filter);
      if (!((IEnumerable<int>) branches).Any<int>())
      {
        found(false);
      }
      else
      {
        INTurnoverCalc[] source = ((PXSelectBase<PX.Objects.GL.Branch>) new PXViewOf<PX.Objects.GL.Branch>.BasedOn<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<INTurnoverCalc>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTurnoverCalc.branchID, Equal<PX.Objects.GL.Branch.branchID>>>>, And<BqlOperand<INTurnoverCalc.fromPeriodID, IBqlString>.IsEqual<P.AsString.ASCII>>>>.And<BqlOperand<INTurnoverCalc.toPeriodID, IBqlString>.IsEqual<P.AsString.ASCII>>>>>.Where<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsIn<P.AsInt>>>.ReadOnly((PXGraph) this)).Select<INTurnoverCalc>(new object[3]
        {
          (object) filter.FromPeriodID,
          (object) filter.ToPeriodID,
          (object) branches
        });
        if (((IEnumerable<INTurnoverCalc>) source).Any<INTurnoverCalc>((Func<INTurnoverCalc, bool>) (x => !x.BranchID.HasValue)))
        {
          found(false);
          filter.IsPartialSuitableCalcs = new bool?(true);
        }
        else
        {
          int?[] nullableArray;
          if (!filter.InventoryID.HasValue)
            nullableArray = ((IEnumerable<InventoryLinkFilter>) ((PXSelectBase<InventoryLinkFilter>) this.InventoryLinkFilterExt.SelectedItems).SelectMain(Array.Empty<object>())).Select<InventoryLinkFilter, int?>((Func<InventoryLinkFilter, int?>) (x => x.InventoryID)).ToArray<int?>();
          else
            nullableArray = new int?[1]
            {
              filter.InventoryID
            };
          int?[] first = nullableArray;
          if (!filter.SiteID.HasValue && !filter.ItemClassID.HasValue && first.Length == 0)
          {
            if (((IEnumerable<INTurnoverCalc>) source).All<INTurnoverCalc>((Func<INTurnoverCalc, bool>) (x => x.IsFullCalc.GetValueOrDefault())))
              found(true);
            else if (((IEnumerable<INTurnoverCalc>) source).Any<INTurnoverCalc>((Func<INTurnoverCalc, bool>) (x => x.IsFullCalc.GetValueOrDefault())) && ((IEnumerable<INTurnoverCalc>) source).Any<INTurnoverCalc>((Func<INTurnoverCalc, bool>) (x =>
            {
              bool? isFullCalc = x.IsFullCalc;
              bool flag = false;
              return isFullCalc.GetValueOrDefault() == flag & isFullCalc.HasValue;
            })))
            {
              found(false);
              filter.IsMixedSuitableCalcs = new bool?(true);
            }
            else
            {
              List<IGrouping<(int?, int?, int?, bool?), INTurnoverCalc>> list = ((IEnumerable<INTurnoverCalc>) source).GroupBy<INTurnoverCalc, (int?, int?, int?, bool?)>((Func<INTurnoverCalc, (int?, int?, int?, bool?)>) (x => (x.SiteID, x.ItemClassID, x.InventoryID, x.IsInventoryListCalc))).ToList<IGrouping<(int?, int?, int?, bool?), INTurnoverCalc>>();
              if (list.Count > 1)
              {
                found(false);
                filter.IsMixedSuitableCalcs = new bool?(true);
              }
              else
              {
                (int?, int?, int?, bool?) key = list[0].Key;
                if (key.Item4.GetValueOrDefault() && !this.IsInventoryListMatch(filter, ((IEnumerable<INTurnoverCalc>) source).Select<INTurnoverCalc, int?>((Func<INTurnoverCalc, int?>) (x => x.BranchID)).ToArray<int?>()))
                {
                  found(false);
                  filter.IsMixedSuitableCalcs = new bool?(true);
                }
                else
                {
                  found(true);
                  filter.SuitableCalcsSiteID = key.Item1;
                  filter.SuitableCalcsItemClassID = key.Item2;
                  filter.SuitableCalcsInventoryID = key.Item3;
                  filter.IsInventoryListCalc = new bool?(key.Item4.GetValueOrDefault());
                }
              }
            }
          }
          else
          {
            INTurnoverCalc[] array = ((IEnumerable<INTurnoverCalc>) source).Where<INTurnoverCalc>((Func<INTurnoverCalc, bool>) (x =>
            {
              bool? isFullCalc = x.IsFullCalc;
              bool flag = false;
              return isFullCalc.GetValueOrDefault() == flag & isFullCalc.HasValue;
            })).ToArray<INTurnoverCalc>();
            if (!((IEnumerable<INTurnoverCalc>) array).Any<INTurnoverCalc>())
            {
              found(true);
            }
            else
            {
              List<IGrouping<(int?, int?, int?, bool?), INTurnoverCalc>> list = ((IEnumerable<INTurnoverCalc>) array).GroupBy<INTurnoverCalc, (int?, int?, int?, bool?)>((Func<INTurnoverCalc, (int?, int?, int?, bool?)>) (x => (x.SiteID, x.ItemClassID, x.InventoryID, x.IsInventoryListCalc))).ToList<IGrouping<(int?, int?, int?, bool?), INTurnoverCalc>>();
              if (list.Count > 1)
              {
                found(false);
                filter.IsMixedSuitableCalcs = new bool?(true);
              }
              else
              {
                (int?, int?, int?, bool?) key = list[0].Key;
                int?[] inventories = (int?[]) null;
                if (key.Item4.GetValueOrDefault() && !this.IsInventoryListMatch(filter, ((IEnumerable<INTurnoverCalc>) source).Select<INTurnoverCalc, int?>((Func<INTurnoverCalc, int?>) (x => x.BranchID)).ToArray<int?>(), out inventories))
                {
                  found(false);
                  filter.IsMixedSuitableCalcs = new bool?(true);
                }
                else
                {
                  found(true);
                  int? nullable1;
                  if (key.Item1.HasValue)
                  {
                    int? siteId = filter.SiteID;
                    nullable1 = key.Item1;
                    if (!(siteId.GetValueOrDefault() == nullable1.GetValueOrDefault() & siteId.HasValue == nullable1.HasValue))
                      filter.SuitableCalcsSiteID = key.Item1;
                  }
                  int? nullable2;
                  if (key.Item2.HasValue)
                  {
                    nullable1 = filter.ItemClassID;
                    nullable2 = key.Item2;
                    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
                      filter.SuitableCalcsItemClassID = key.Item2;
                  }
                  if (key.Item3.HasValue)
                  {
                    if (first.Length == 1)
                    {
                      nullable2 = first[0];
                      nullable1 = key.Item3;
                      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
                        goto label_35;
                    }
                    filter.SuitableCalcsInventoryID = key.Item3;
                  }
label_35:
                  if (!key.Item4.GetValueOrDefault() || inventories == null || first.Length != 0 && !((IEnumerable<int?>) first).Except<int?>((IEnumerable<int?>) inventories).Any<int?>())
                    return;
                  filter.IsInventoryListCalc = new bool?(true);
                }
              }
            }
          }
        }
      }
    }

    void found(bool isCalcFound)
    {
      filter.IsSuitableCalcsFound = new bool?(isCalcFound);
      filter.IsPartialSuitableCalcs = new bool?(false);
      filter.IsMixedSuitableCalcs = new bool?(false);
      filter.SuitableCalcsSiteID = new int?();
      filter.SuitableCalcsItemClassID = new int?();
      filter.SuitableCalcsInventoryID = new int?();
      filter.IsInventoryListCalc = new bool?(false);
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<INTurnoverEnqFilter> e)
  {
    this.FindTurnoverCalc(e.Row);
  }

  protected virtual bool IsInventoryListMatch(INTurnoverEnqFilter filter, int?[] branches)
  {
    return branches.Length == 1 || this.IsInventoryListMatch(filter, branches, out int?[] _);
  }

  protected virtual bool IsInventoryListMatch(
    INTurnoverEnqFilter filter,
    int?[] branches,
    out int?[] inventories)
  {
    PXViewOf<INTurnoverCalcItem>.BasedOn<SelectFromBase<INTurnoverCalcItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTurnoverCalcItem.branchID, Equal<P.AsInt>>>>, And<BqlOperand<INTurnoverCalcItem.fromPeriodID, IBqlString>.IsEqual<P.AsString.ASCII>>>>.And<BqlOperand<INTurnoverCalcItem.toPeriodID, IBqlString>.IsEqual<P.AsString.ASCII>>>>.ReadOnly readOnly = new PXViewOf<INTurnoverCalcItem>.BasedOn<SelectFromBase<INTurnoverCalcItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTurnoverCalcItem.branchID, Equal<P.AsInt>>>>, And<BqlOperand<INTurnoverCalcItem.fromPeriodID, IBqlString>.IsEqual<P.AsString.ASCII>>>>.And<BqlOperand<INTurnoverCalcItem.toPeriodID, IBqlString>.IsEqual<P.AsString.ASCII>>>>.ReadOnly((PXGraph) this);
    using (new PXFieldScope(((PXSelectBase) readOnly).View, new Type[1]
    {
      typeof (INTurnoverCalcItem.inventoryID)
    }))
      inventories = ((IEnumerable<INTurnoverCalcItem>) ((PXSelectBase<INTurnoverCalcItem>) readOnly).SelectMain(new object[3]
      {
        (object) branches[0],
        (object) filter.FromPeriodID,
        (object) filter.ToPeriodID
      })).Select<INTurnoverCalcItem, int?>((Func<INTurnoverCalcItem, int?>) (x => x.InventoryID)).ToArray<int?>();
    branches = ((IEnumerable<int?>) branches).Skip<int?>(1).ToArray<int?>();
    return branches.Length == 1 || this.IsInventoryListMatch(filter, branches, inventories);
  }

  protected virtual bool IsInventoryListMatch(
    INTurnoverEnqFilter filter,
    int?[] branches,
    int?[] inventories)
  {
    PXViewOf<INTurnoverCalcItem>.BasedOn<SelectFromBase<INTurnoverCalcItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTurnoverCalcItem.branchID, In<P.AsInt>>>>, And<BqlOperand<INTurnoverCalcItem.fromPeriodID, IBqlString>.IsEqual<P.AsString.ASCII>>>, And<BqlOperand<INTurnoverCalcItem.toPeriodID, IBqlString>.IsEqual<P.AsString.ASCII>>>>.And<BqlOperand<INTurnoverCalcItem.inventoryID, IBqlInt>.IsIn<P.AsInt>>>.Aggregate<To<GroupBy<INTurnoverCalcItem.branchID>, GroupBy<INTurnoverCalcItem.fromPeriodID>, GroupBy<INTurnoverCalcItem.toPeriodID>, Count<INTurnoverCalcItem.inventoryID>>>>.ReadOnly readOnly = new PXViewOf<INTurnoverCalcItem>.BasedOn<SelectFromBase<INTurnoverCalcItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTurnoverCalcItem.branchID, In<P.AsInt>>>>, And<BqlOperand<INTurnoverCalcItem.fromPeriodID, IBqlString>.IsEqual<P.AsString.ASCII>>>, And<BqlOperand<INTurnoverCalcItem.toPeriodID, IBqlString>.IsEqual<P.AsString.ASCII>>>>.And<BqlOperand<INTurnoverCalcItem.inventoryID, IBqlInt>.IsIn<P.AsInt>>>.Aggregate<To<GroupBy<INTurnoverCalcItem.branchID>, GroupBy<INTurnoverCalcItem.fromPeriodID>, GroupBy<INTurnoverCalcItem.toPeriodID>, Count<INTurnoverCalcItem.inventoryID>>>>.ReadOnly((PXGraph) this);
    PXResult<INTurnoverCalcItem>[] array;
    using (new PXFieldScope(((PXSelectBase) readOnly).View, new Type[3]
    {
      typeof (INTurnoverCalcItem.branchID),
      typeof (INTurnoverCalcItem.fromPeriodID),
      typeof (INTurnoverCalcItem.toPeriodID)
    }))
      array = ((IEnumerable<PXResult<INTurnoverCalcItem>>) ((PXSelectBase<INTurnoverCalcItem>) readOnly).Select(new object[4]
      {
        (object) branches,
        (object) filter.FromPeriodID,
        (object) filter.ToPeriodID,
        (object) inventories
      })).ToArray<PXResult<INTurnoverCalcItem>>();
    return !((IEnumerable<PXResult<INTurnoverCalcItem>>) array).Any<PXResult<INTurnoverCalcItem>>((Func<PXResult<INTurnoverCalcItem>, bool>) (x =>
    {
      int? rowCount = ((PXResult) x).RowCount;
      int length = inventories.Length;
      return !(rowCount.GetValueOrDefault() == length & rowCount.HasValue);
    }));
  }

  protected virtual int[] GetBranches(INTurnoverEnqFilter filter)
  {
    int? nullable;
    int[] branches1;
    if (filter.BranchID.HasValue)
    {
      int[] numArray = new int[1];
      nullable = filter.BranchID;
      numArray[0] = nullable.Value;
      branches1 = numArray;
    }
    else
      branches1 = PXAccess.GetChildBranchIDs(filter.OrganizationID.HasValue ? filter.OrganizationID : ((PXAccess.Organization) PXAccess.GetOrganizationByBAccountID(filter.OrgBAccountID))?.OrganizationID, true);
    nullable = filter.SiteID;
    if (!nullable.HasValue)
      return branches1;
    INSite inSite = INSite.PK.Find((PXGraph) this, filter.SiteID);
    if (inSite != null)
    {
      nullable = inSite.BranchID;
      if (nullable.HasValue)
      {
        int[] source = branches1;
        nullable = inSite.BranchID;
        int num = nullable.Value;
        if (((IEnumerable<int>) source).Contains<int>(num))
        {
          int[] branches2 = new int[1];
          nullable = inSite.BranchID;
          branches2[0] = nullable.Value;
          return branches2;
        }
      }
    }
    return Array<int>.Empty;
  }

  protected virtual CurrencyList GetCurrency(INTurnoverEnqFilter filter)
  {
    return CurrencyList.PK.Find((PXGraph) this, (filter.OrganizationID.HasValue ? (PXAccess.Organization) PXAccess.GetOrganizationByID(filter.OrganizationID) : (PXAccess.Organization) PXAccess.GetOrganizationByBAccountID(filter.OrgBAccountID))?.BaseCuryID);
  }

  protected virtual bool IsValidValue<TField>(object row) where TField : IBqlField
  {
    PXCache cach = ((PXGraph) this).Caches[BqlCommand.GetItemType<TField>()];
    object obj = ((PXSelectBase) this.Filter).Cache.GetValue<TField>(row);
    if (obj == null)
      return true;
    try
    {
      cach.RaiseFieldVerifying<TField>(row, ref obj);
      return true;
    }
    catch
    {
      return false;
    }
  }

  public virtual BqlCommand AppendFilter(
    BqlCommand cmd,
    IList<object> parameters,
    INTurnoverEnqFilter filter)
  {
    if (filter.SiteID.HasValue)
    {
      cmd = cmd.WhereAnd<Where<BqlOperand<TurnoverCalcItem.siteID, IBqlInt>.IsIn<P.AsInt>>>();
      parameters.Add((object) filter.SiteID);
    }
    if (filter.ItemClassID.HasValue)
    {
      cmd = cmd.WhereAnd<Where<BqlOperand<TurnoverCalcItem.itemClassID, IBqlInt>.IsIn<P.AsInt>>>();
      parameters.Add((object) filter.ItemClassID);
    }
    return cmd;
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
}
