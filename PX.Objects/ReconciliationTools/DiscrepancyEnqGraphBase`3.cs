// Decompiled with JetBrains decompiler
// Type: ReconciliationTools.DiscrepancyEnqGraphBase`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace ReconciliationTools;

[TableAndChartDashboardType]
public class DiscrepancyEnqGraphBase<TGraph, TEnqFilter, TEnqResult> : PXGraph<TGraph>
  where TGraph : PXGraph
  where TEnqFilter : DiscrepancyEnqFilter, new()
  where TEnqResult : class, IBqlTable, IDiscrepancyEnqResult, new()
{
  public PXFilter<TEnqFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<TEnqResult> Rows;
  public PXAction<TEnqFilter> previousPeriod;
  public PXAction<TEnqFilter> nextPeriod;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [PXSuppressActionValidation]
  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = true)]
  [PXButton(ImageKey = "Refresh")]
  public IEnumerable Refresh(PXAdapter adapter)
  {
    this.Filter.Current.FilterDetails = (byte[][]) null;
    return adapter.Get();
  }

  protected virtual IEnumerable filter()
  {
    DiscrepancyEnqGraphBase<TGraph, TEnqFilter, TEnqResult> discrepancyEnqGraphBase = this;
    PXCache cache = discrepancyEnqGraphBase.Caches[typeof (TEnqFilter)];
    if (cache != null && cache.Current is DiscrepancyEnqFilter current)
    {
      current.TotalGLAmount = new Decimal?(0M);
      current.TotalXXAmount = new Decimal?(0M);
      current.TotalDiscrepancy = new Decimal?(0M);
      foreach (PXResult<TEnqResult> pxResult in discrepancyEnqGraphBase.Rows.Select())
      {
        TEnqResult enqResult = (TEnqResult) pxResult;
        DiscrepancyEnqFilter discrepancyEnqFilter1 = current;
        Decimal? nullable1 = discrepancyEnqFilter1.TotalGLAmount;
        Decimal? nullable2 = enqResult.GLTurnover;
        Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
        Decimal? nullable3;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable3 = nullable2;
        }
        else
          nullable3 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault1);
        discrepancyEnqFilter1.TotalGLAmount = nullable3;
        DiscrepancyEnqFilter discrepancyEnqFilter2 = current;
        nullable1 = discrepancyEnqFilter2.TotalXXAmount;
        nullable2 = enqResult.XXTurnover;
        Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
        Decimal? nullable4;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable4 = nullable2;
        }
        else
          nullable4 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault2);
        discrepancyEnqFilter2.TotalXXAmount = nullable4;
        DiscrepancyEnqFilter discrepancyEnqFilter3 = current;
        nullable1 = discrepancyEnqFilter3.TotalDiscrepancy;
        nullable2 = enqResult.Discrepancy;
        Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
        Decimal? nullable5;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable5 = nullable2;
        }
        else
          nullable5 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault3);
        discrepancyEnqFilter3.TotalDiscrepancy = nullable5;
      }
    }
    yield return cache.Current;
    cache.IsDirty = false;
  }

  protected IEnumerable rows()
  {
    DiscrepancyEnqFilter current = this.Caches[typeof (TEnqFilter)].Current as DiscrepancyEnqFilter;
    if (current.FilterDetails != null && (this.Filter.Cache.GetStateExt<DiscrepancyEnqFilter.filterDetails>((object) current) is PXFieldState stateExt ? stateExt.Value : (object) null) is IEnumerable enumerable)
      return enumerable;
    List<TEnqResult> enqResultList = this.SelectDetails();
    this.Filter.Cache.SetValueExt<DiscrepancyEnqFilter.filterDetails>((object) current, (object) enqResultList);
    return (IEnumerable) enqResultList;
  }

  protected virtual List<TEnqResult> SelectDetails() => (List<TEnqResult>) null;

  public DiscrepancyEnqGraphBase()
  {
    this.Rows.Cache.AllowDelete = false;
    this.Rows.Cache.AllowInsert = false;
    this.Rows.Cache.AllowUpdate = false;
    this.RowUpdated.AddHandler(typeof (TEnqFilter), new PXRowUpdated(this.FilterRowUpdated));
    this.FieldUpdated.AddHandler(typeof (TEnqFilter), typeof (DiscrepancyEnqFilter.periodFrom).Name, new PXFieldUpdated(this.PeriodFromFieldUpdated));
    this.FieldUpdated.AddHandler(typeof (TEnqFilter), typeof (DiscrepancyEnqFilter.periodTo).Name, new PXFieldUpdated(this.PeriodToFieldUpdated));
  }

  public override bool IsDirty => false;

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXPreviousButton]
  public virtual IEnumerable PreviousPeriod(PXAdapter adapter)
  {
    DiscrepancyEnqFilter current = (DiscrepancyEnqFilter) this.Filter.Current;
    current.UseMasterCalendar = new bool?(!current.OrganizationID.HasValue && !current.BranchID.HasValue);
    int? calendarOrganizationId = this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, current.UseMasterCalendar);
    FinPeriod prevPeriod1 = this.FinPeriodRepository.FindPrevPeriod(calendarOrganizationId, current.PeriodFrom, true);
    current.PeriodFrom = prevPeriod1?.FinPeriodID;
    FinPeriod prevPeriod2 = this.FinPeriodRepository.FindPrevPeriod(calendarOrganizationId, current.PeriodTo, true);
    current.PeriodTo = prevPeriod2?.FinPeriodID;
    current.FilterDetails = (byte[][]) null;
    return adapter.Get();
  }

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXNextButton]
  public virtual IEnumerable NextPeriod(PXAdapter adapter)
  {
    DiscrepancyEnqFilter current = (DiscrepancyEnqFilter) this.Filter.Current;
    current.UseMasterCalendar = new bool?(!current.OrganizationID.HasValue && !current.BranchID.HasValue);
    int? calendarOrganizationId = this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, current.UseMasterCalendar);
    FinPeriod nextPeriod1 = this.FinPeriodRepository.FindNextPeriod(calendarOrganizationId, current.PeriodFrom, true);
    current.PeriodFrom = nextPeriod1?.FinPeriodID;
    FinPeriod nextPeriod2 = this.FinPeriodRepository.FindNextPeriod(calendarOrganizationId, current.PeriodTo, true);
    current.PeriodTo = nextPeriod2?.FinPeriodID;
    current.FilterDetails = (byte[][]) null;
    return adapter.Get();
  }

  protected virtual void PeriodFromFieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    DiscrepancyEnqFilter row = (DiscrepancyEnqFilter) e.Row;
    if (string.CompareOrdinal(row.PeriodFrom, row.PeriodTo) <= 0)
      return;
    cache.SetValue<DiscrepancyEnqFilter.periodTo>(e.Row, (object) row.PeriodFrom);
  }

  protected virtual void PeriodToFieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    DiscrepancyEnqFilter row = (DiscrepancyEnqFilter) e.Row;
    if (string.CompareOrdinal(row.PeriodFrom, row.PeriodTo) <= 0)
      return;
    cache.SetValue<DiscrepancyEnqFilter.periodFrom>(e.Row, (object) row.PeriodTo);
  }

  protected virtual void FilterRowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    ((DiscrepancyEnqFilter) e.Row).FilterDetails = (byte[][]) null;
  }

  protected virtual string GetSubCD(int? subID)
  {
    return ((Sub) PXSelectBase<Sub, PXSelect<Sub, Where<Sub.subID, Equal<Required<Sub.subID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) subID))?.SubCD;
  }

  protected virtual Decimal CalcGLTurnover(GLTran tran) => throw new NotImplementedException();
}
