// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxYearMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.TX.Data;
using PX.Objects.TX.Descriptor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.TX;

public class TaxYearMaint : PXGraph<
#nullable disable
TaxYearMaint>
{
  public PXSave<TaxYearMaint.TaxYearFilter> Save;
  public PXCancel<TaxYearMaint.TaxYearFilter> Cancel;
  public PXDelete<TaxYearMaint.TaxYearFilter> Delete;
  public PXAction<TaxYearMaint.TaxYearFilter> RedirectToPrepareTaxReport;
  public PXAction<TaxYearMaint.TaxYearFilter> RedirectToReleaseTaxReport;
  public PXAction<TaxYearMaint.TaxYearFilter> RedirectToTaxSummaryReport;
  public PXAction<TaxYearMaint.TaxYearFilter> RedirectToTaxDetailsReport;
  public PXAction<TaxYearMaint.TaxYearFilter> AddPeriod;
  public PXAction<TaxYearMaint.TaxYearFilter> DeletePeriod;
  public PXAction<TaxYearMaint.TaxYearFilter> ViewTaxPeriodDetails;
  public PXAction<TaxYearMaint.TaxYearFilter> SyncTaxPeriods;
  public PXFilter<TaxYearMaint.TaxYearFilter> TaxYearFilterSelectView;
  public PXSelect<TaxPeriod> TaxPeriodSelectView;
  public PXSelect<TaxYearMaint.TaxPeriodEx> TaxPeriodExSelectView;
  public PXSelect<TaxYear> TaxYearSelectView;
  public PXSelect<TaxYearMaint.TaxYearEx> TaxYearExSelectView;
  protected PX.Objects.TX.TaxCalendar<TaxYear, TaxPeriod> TaxCalendar;
  protected PX.Objects.TX.TaxCalendar<TaxYearMaint.TaxYearEx, TaxYearMaint.TaxPeriodEx> TaxCalendarEx;

  public static TaxPeriod FindTaxPeriodByKey(
    PXGraph graph,
    int? organizationID,
    int? taxAgencyID,
    string taxPeriodID)
  {
    return PXResultset<TaxPeriod>.op_Implicit(PXSelectBase<TaxPeriod, PXSelect<TaxPeriod, Where<TaxPeriod.organizationID, Equal<Required<TaxPeriod.organizationID>>, And<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>, And<TaxPeriod.taxPeriodID, Equal<Required<TaxPeriod.taxPeriodID>>>>>>.Config>.Select(graph, new object[3]
    {
      (object) organizationID,
      (object) taxAgencyID,
      (object) taxPeriodID
    }));
  }

  public static TaxPeriod GetTaxPeriodByKey(
    PXGraph graph,
    int? organizationID,
    int? taxAgencyID,
    string taxPeriodID)
  {
    return TaxYearMaint.FindTaxPeriodByKey(graph, organizationID, taxAgencyID, taxPeriodID) ?? throw new PXException("Reporting period '{0}' does not exist for the tax agency '{1}'.", new object[2]
    {
      (object) taxPeriodID,
      (object) VendorMaint.GetByID(graph, taxAgencyID).AcctCD.Trim()
    });
  }

  public static TaxPeriod FindFirstOpenTaxPeriod(
    PXGraph graph,
    int? organizationID,
    int? taxAgencyID)
  {
    return PXResultset<TaxPeriod>.op_Implicit(PXSelectBase<TaxPeriod, PXSelect<TaxPeriod, Where<TaxPeriod.organizationID, Equal<Required<TaxPeriod.organizationID>>, And<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>, And<TaxPeriod.status, Equal<TaxPeriodStatus.open>>>>, OrderBy<Asc<TaxPeriod.taxPeriodID>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
    {
      (object) organizationID,
      (object) taxAgencyID
    }));
  }

  public static bool IsAnyClosedTaxPeriodInTaxYear(
    PXGraph graph,
    int? organizationID,
    int? taxAgencyID,
    string year)
  {
    return ((IQueryable<PXResult<TaxPeriod>>) PXSelectBase<TaxPeriod, PXSelect<TaxPeriod, Where<TaxPeriod.organizationID, Equal<Required<TaxPeriod.organizationID>>, And<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>, And<TaxPeriod.taxYear, Equal<Required<TaxPeriod.taxYear>>, And<TaxPeriod.status, Equal<TaxPeriodStatus.closed>>>>>, OrderBy<Asc<TaxPeriod.taxPeriodID>>>.Config>.SelectWindowed(graph, 0, 1, new object[3]
    {
      (object) organizationID,
      (object) taxAgencyID,
      (object) year
    })).Any<PXResult<TaxPeriod>>();
  }

  public static TaxPeriod FindPreparedPeriod(PXGraph graph, int? organizationID, int? taxAgencyID)
  {
    return PXResultset<TaxPeriod>.op_Implicit(PXSelectBase<TaxPeriod, PXSelect<TaxPeriod, Where<TaxPeriod.organizationID, Equal<Required<TaxPeriod.organizationID>>, And<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>, And<TaxPeriod.status, Equal<TaxPeriodStatus.prepared>>>>>.Config>.Select(graph, new object[2]
    {
      (object) organizationID,
      (object) taxAgencyID
    }));
  }

  public static TaxPeriod FindLastClosedPeriod(
    PXGraph graph,
    int? organizationID,
    int? taxAgencyID)
  {
    return PXResultset<TaxPeriod>.op_Implicit(PXSelectBase<TaxPeriod, PXSelect<TaxPeriod, Where<TaxPeriod.organizationID, Equal<Required<TaxPeriod.organizationID>>, And<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>, And<TaxPeriod.status, Equal<TaxPeriodStatus.closed>>>>, OrderBy<Desc<TaxPeriod.taxPeriodID>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
    {
      (object) organizationID,
      (object) taxAgencyID
    }));
  }

  public static TaxPeriod FindLastTaxPeriod(PXGraph graph, int? organizationID, int? taxAgencyID)
  {
    return PXResultset<TaxPeriod>.op_Implicit(PXSelectBase<TaxPeriod, PXSelect<TaxPeriod, Where<TaxPeriod.organizationID, Equal<Required<TaxPeriod.organizationID>>, And<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>>>, OrderBy<Desc<TaxPeriod.taxPeriodID>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
    {
      (object) organizationID,
      (object) taxAgencyID
    }));
  }

  public static TaxPeriod FinTaxPeriodByDate(
    PXGraph graph,
    int? organizationID,
    int? taxAgencyID,
    DateTime? date)
  {
    return PXResultset<TaxPeriod>.op_Implicit(PXSelectBase<TaxPeriod, PXSelect<TaxPeriod, Where<TaxPeriod.organizationID, Equal<Required<TaxPeriodFilter.organizationID>>, And<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>, And<TaxPeriod.startDate, LessEqual<Required<TaxPeriod.startDate>>, And<TaxPeriod.endDate, Greater<Required<TaxPeriod.endDate>>>>>>>.Config>.Select(graph, new object[4]
    {
      (object) organizationID,
      (object) taxAgencyID,
      (object) date,
      (object) date
    }));
  }

  public static TaxYear FindTaxYearByKey(
    PXGraph graph,
    int? organizationID,
    int? taxAgencyID,
    string year)
  {
    return PXResultset<TaxYear>.op_Implicit(PXSelectBase<TaxYear, PXSelect<TaxYear, Where<TaxYear.organizationID, Equal<Required<TaxYear.organizationID>>, And<TaxYear.vendorID, Equal<Required<TaxYear.vendorID>>, And<TaxYear.year, Equal<Required<TaxYear.year>>>>>>.Config>.Select(graph, new object[3]
    {
      (object) organizationID,
      (object) taxAgencyID,
      (object) year
    }));
  }

  public static TaxYear FindLastTaxYear(PXGraph graph, int? branchID, int? taxAgencyID)
  {
    return PXResultset<TaxYear>.op_Implicit(PXSelectBase<TaxYear, PXSelect<TaxYear, Where<TaxYear.organizationID, Equal<Required<TaxYear.organizationID>>, And<TaxYear.vendorID, Equal<Required<TaxYear.vendorID>>>>, OrderBy<Desc<TaxYear.year>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
    {
      (object) branchID,
      (object) taxAgencyID
    }));
  }

  protected static TaxYearMaint.TaxYearEx FindTaxYearExByKey(
    PXGraph graph,
    int? organizationID,
    int? taxAgencyID,
    string year)
  {
    return PXResultset<TaxYearMaint.TaxYearEx>.op_Implicit(PXSelectBase<TaxYearMaint.TaxYearEx, PXSelect<TaxYearMaint.TaxYearEx, Where<TaxYearMaint.TaxYearEx.organizationID, Equal<Required<TaxYearMaint.TaxYearEx.organizationID>>, And<TaxYearMaint.TaxYearEx.vendorID, Equal<Required<TaxYearMaint.TaxYearEx.vendorID>>, And<TaxYearMaint.TaxYearEx.year, Equal<Required<TaxYearMaint.TaxYearEx.year>>>>>>.Config>.Select(graph, new object[3]
    {
      (object) organizationID,
      (object) taxAgencyID,
      (object) year
    }));
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public virtual bool IsDirty
  {
    get
    {
      return ((PXSelectBase) this.TaxYearExSelectView).Cache.IsDirty || ((PXSelectBase) this.TaxPeriodExSelectView).Cache.IsDirty;
    }
  }

  public TaxYearMaint()
  {
    this.TaxCalendar = new PX.Objects.TX.TaxCalendar<TaxYear, TaxPeriod>((PXGraph) this);
    this.TaxCalendarEx = new PX.Objects.TX.TaxCalendar<TaxYearMaint.TaxYearEx, TaxYearMaint.TaxPeriodEx>((PXGraph) this);
    PXDelete<TaxYearMaint.TaxYearFilter> delete = this.Delete;
    TaxYearMaint taxYearMaint1 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting1 = new PXFieldSelecting((object) taxYearMaint1, __vmethodptr(taxYearMaint1, DeleteButtonFieldSelectingHandler));
    ((PXAction) delete).StateSelectingEvents += pxFieldSelecting1;
    PXAction<TaxYearMaint.TaxYearFilter> addPeriod = this.AddPeriod;
    TaxYearMaint taxYearMaint2 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting2 = new PXFieldSelecting((object) taxYearMaint2, __vmethodptr(taxYearMaint2, AddPeriodButtonFieldSelectingHandler));
    ((PXAction) addPeriod).StateSelectingEvents += pxFieldSelecting2;
    PXAction<TaxYearMaint.TaxYearFilter> deletePeriod = this.DeletePeriod;
    TaxYearMaint taxYearMaint3 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting3 = new PXFieldSelecting((object) taxYearMaint3, __vmethodptr(taxYearMaint3, DeletePeriodButtonFieldSelectingHandler));
    ((PXAction) deletePeriod).StateSelectingEvents += pxFieldSelecting3;
    PXUIFieldAttribute.SetReadOnly(((PXSelectBase) this.TaxPeriodExSelectView).Cache, (object) null, true);
  }

  protected virtual IEnumerable taxPeriodExSelectView()
  {
    return ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current == null ? (IEnumerable) new object[0] : (IEnumerable) this.GetActualStoredTaxPeriods();
  }

  public virtual void Persist()
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      TaxYearMaint.TaxYearEx current = ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current;
      if (((PXSelectBase) this.TaxYearExSelectView).Cache.GetStatus((object) current) == 1 && !current.Existing.GetValueOrDefault())
        ((PXSelectBase) this.TaxYearExSelectView).Cache.SetStatus((object) current, (PXEntryStatus) 2);
      List<TaxYearMaint.TaxPeriodEx> list = ((PXSelectBase) this.TaxPeriodExSelectView).Cache.Cached.Cast<TaxYearMaint.TaxPeriodEx>().Where<TaxYearMaint.TaxPeriodEx>((Func<TaxYearMaint.TaxPeriodEx, bool>) (period =>
      {
        PXEntryStatus status = ((PXSelectBase) this.TaxPeriodExSelectView).Cache.GetStatus((object) period);
        return status == 2 || status == 1;
      })).ToList<TaxYearMaint.TaxPeriodEx>();
      TaxYearMaint.TaxPeriodEx taxPeriodEx = ((PXSelectBase) this.TaxPeriodExSelectView).Cache.Cached.Cast<TaxYearMaint.TaxPeriodEx>().Where<TaxYearMaint.TaxPeriodEx>((Func<TaxYearMaint.TaxPeriodEx, bool>) (period =>
      {
        PXEntryStatus status = ((PXSelectBase) this.TaxPeriodExSelectView).Cache.GetStatus((object) period);
        return status == 2 || status == 1 || status == 3;
      })).OrderByDescending<TaxYearMaint.TaxPeriodEx, string>((Func<TaxYearMaint.TaxPeriodEx, string>) (period => period.TaxPeriodID)).FirstOrDefault<TaxYearMaint.TaxPeriodEx>();
      if (taxPeriodEx != null)
      {
        TaxYear taxYear = PXResultset<TaxYear>.op_Implicit(PXSelectBase<TaxYear, PXSelect<TaxYear, Where<TaxYear.organizationID, Equal<Required<TaxYear.organizationID>>, And<TaxYear.vendorID, Equal<Required<TaxYear.vendorID>>, And<TaxYear.year, Greater<Required<TaxYear.year>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
        {
          (object) taxPeriodEx.OrganizationID,
          (object) taxPeriodEx.VendorID,
          (object) taxPeriodEx.TaxYear
        }));
        if (taxYear != null)
        {
          ((PXSelectBase<TaxYearMaint.TaxYearFilter>) this.TaxYearFilterSelectView).Ask(string.Format(PXMessages.LocalizeNoPrefix("The start date specified for the {0} tax year does not match the end date of the {1} tax year for the {2} company. The {0} tax year will be deleted."), (object) taxYear.Year, (object) taxPeriodEx.TaxYear, (object) PXAccess.GetOrganizationCD(taxPeriodEx.OrganizationID)), (MessageButtons) 0);
          PXDatabase.Delete<TaxYear>(new PXDataFieldRestrict[3]
          {
            new PXDataFieldRestrict(typeof (TaxYear.organizationID).Name, (PXDbType) 8, new int?(4), (object) current.OrganizationID, (PXComp) 0),
            new PXDataFieldRestrict(typeof (TaxYear.vendorID).Name, (PXDbType) 8, new int?(4), (object) current.VendorID, (PXComp) 0),
            new PXDataFieldRestrict(typeof (TaxYear.year).Name, (PXDbType) 3, new int?(4), (object) taxPeriodEx.TaxYear, (PXComp) 2)
          });
          PXDatabase.Delete<TaxPeriod>(new PXDataFieldRestrict[3]
          {
            new PXDataFieldRestrict(typeof (TaxPeriod.organizationID).Name, (PXDbType) 8, new int?(4), (object) current.OrganizationID, (PXComp) 0),
            new PXDataFieldRestrict(typeof (TaxPeriod.vendorID).Name, (PXDbType) 8, new int?(4), (object) current.VendorID, (PXComp) 0),
            new PXDataFieldRestrict(typeof (TaxPeriod.taxPeriodID).Name, (PXDbType) 3, new int?(6), (object) taxPeriodEx.TaxPeriodID, (PXComp) 2)
          });
        }
      }
      ((PXGraph) this).Persist();
      transactionScope.Complete();
      foreach (object obj in list)
        ((PXSelectBase) this.TaxPeriodExSelectView).Cache.SetStatus(obj, (PXEntryStatus) 5);
    }
  }

  protected virtual void DeleteButtonFieldSelectingHandler(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    e.ReturnState = (object) PXButtonState.CreateDefaultState<TaxYearMaint.TaxYearFilter>(e.ReturnState);
    TaxPeriod taxPeriod = PXResultset<TaxPeriod>.op_Implicit(PXSelectBase<TaxPeriod, PXSelect<TaxPeriod, Where<TaxPeriod.organizationID, Equal<Current<TaxYearMaint.TaxYearEx.organizationID>>, And<TaxPeriod.vendorID, Equal<Current<TaxYearMaint.TaxYearEx.vendorID>>, And<TaxPeriod.taxYear, Equal<Current<TaxYearMaint.TaxYearEx.year>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current
    }, Array.Empty<object>()));
    ((PXFieldState) e.ReturnState).Enabled = ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current != null && ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current.Existing.GetValueOrDefault() && taxPeriod == null;
  }

  protected virtual void DeletePeriodButtonFieldSelectingHandler(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    this.PeriodButtonsFieldSelectingHandler(e);
    PXButtonState returnState = (PXButtonState) e.ReturnState;
    int num1 = ((PXFieldState) returnState).Enabled ? 1 : 0;
    int num2;
    if (((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current != null)
    {
      int? periodsCount = ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current.PeriodsCount;
      int num3 = 0;
      if (periodsCount.GetValueOrDefault() > num3 & periodsCount.HasValue)
      {
        num2 = this.GetActualStoredTaxPeriods().Last<TaxYearMaint.TaxPeriodEx>().Status == "N" ? 1 : 0;
        goto label_4;
      }
    }
    num2 = 0;
label_4:
    ((PXFieldState) returnState).Enabled = (num1 & num2) != 0;
  }

  protected virtual void AddPeriodButtonFieldSelectingHandler(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    this.PeriodButtonsFieldSelectingHandler(e);
    PXButtonState returnState = (PXButtonState) e.ReturnState;
    int num1 = ((PXFieldState) returnState).Enabled ? 1 : 0;
    int num2;
    if (((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current != null)
    {
      int? periodsCount = ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current.PeriodsCount;
      int? planPeriodsCount = ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current.PlanPeriodsCount;
      num2 = periodsCount.GetValueOrDefault() < planPeriodsCount.GetValueOrDefault() & periodsCount.HasValue & planPeriodsCount.HasValue ? 1 : 0;
    }
    else
      num2 = 0;
    ((PXFieldState) returnState).Enabled = (num1 & num2) != 0;
  }

  protected virtual void TaxYearFilter_ShortTaxYear_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (!((bool?) e.NewValue).GetValueOrDefault() || ((PXSelectBase<TaxYearMaint.TaxYearFilter>) this.TaxYearFilterSelectView).Ask("Are you sure you want to shorten the tax year?", (MessageButtons) 4) == 6)
      return;
    e.NewValue = (object) null;
  }

  protected virtual void TaxYearEx_RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
  {
    if (!(e.Row is TaxYearMaint.TaxYearEx row) || e.TranStatus != 1)
      return;
    TaxYearMaint.TaxYearFilter current = ((PXSelectBase<TaxYearMaint.TaxYearFilter>) this.TaxYearFilterSelectView).Current;
    if (current != null)
    {
      TaxYearMaint.TaxYearFilter taxYearFilter = current;
      int? planPeriodsCount = row.PlanPeriodsCount;
      int? periodsCount = row.PeriodsCount;
      bool? nullable = new bool?(!(planPeriodsCount.GetValueOrDefault() == periodsCount.GetValueOrDefault() & planPeriodsCount.HasValue == periodsCount.HasValue));
      taxYearFilter.ShortTaxYear = nullable;
      PXUIFieldAttribute.SetEnabled<TaxYearMaint.TaxYearFilter.shortTaxYear>(cache, (object) null, !current.ShortTaxYear.GetValueOrDefault());
    }
    row.Existing = new bool?(true);
  }

  protected virtual void TaxYearFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is TaxYearMaint.TaxYearFilter row))
      return;
    PXUIFieldAttribute.SetEnabled<TaxYearMaint.TaxYearFilter.startDate>(cache, (object) null, ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current != null);
    PXUIFieldAttribute.SetEnabled<TaxYearMaint.TaxYearFilter.taxPeriodType>(cache, (object) null, ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current != null);
    TaxYearMaint.TaxYearFilter taxYearFilter = row;
    bool? shortTaxYear = taxYearFilter.ShortTaxYear;
    int? periodsCount = (int?) ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current?.PeriodsCount;
    int? nullable1 = (int?) ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current?.PlanPeriodsCount;
    taxYearFilter.ShortTaxYear = periodsCount.GetValueOrDefault() < nullable1.GetValueOrDefault() & periodsCount.HasValue & nullable1.HasValue ? new bool?(true) : shortTaxYear;
    PXCache pxCache1 = cache;
    bool? nullable2 = row.ShortTaxYear;
    int num1 = nullable2.GetValueOrDefault() ? 0 : (((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current != null ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<TaxYearMaint.TaxYearFilter.shortTaxYear>(pxCache1, (object) null, num1 != 0);
    PXCache pxCache2 = cache;
    nullable2 = row.IsStartDateEditable;
    int num2 = nullable2.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<TaxYearMaint.TaxYearFilter.startDate>(pxCache2, (object) null, num2 != 0);
    PXCache pxCache3 = cache;
    nullable2 = row.IsTaxPeriodTypeEditable;
    int num3 = nullable2.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<TaxYearMaint.TaxYearFilter.taxPeriodType>(pxCache3, (object) null, num3 != 0);
    PXCache cache1 = ((PXSelectBase) this.TaxPeriodExSelectView).Cache;
    nullable2 = row.ShortTaxYear;
    int num4 = nullable2.GetValueOrDefault() ? 1 : 0;
    cache1.AllowDelete = num4 != 0;
    PXCache cache2 = ((PXSelectBase) this.TaxPeriodExSelectView).Cache;
    nullable2 = row.ShortTaxYear;
    int num5 = nullable2.GetValueOrDefault() ? 1 : 0;
    cache2.AllowInsert = num5 != 0;
    ((PXAction) this.SyncTaxPeriods).SetEnabled(row.TaxPeriodType == "F");
    string errorOnly = PXUIFieldAttribute.GetErrorOnly<TaxYearMaint.TaxYearFilter.year>(cache, (object) row);
    if (row.Year == null)
      cache.RaiseExceptionHandling<TaxYearMaint.TaxYearFilter.year>((object) row, (object) null, (Exception) null);
    else if (string.IsNullOrEmpty(errorOnly))
    {
      cache.RaiseExceptionHandling<TaxYearMaint.TaxYearFilter.year>((object) row, (object) null, (Exception) null);
      if (row.TaxPeriodType == "F")
      {
        IFinPeriodRepository periodRepository = this.FinPeriodRepository;
        nullable1 = row.OrganizationID;
        int? organizationID = new int?(nullable1.GetValueOrDefault());
        string year = periodRepository.FindLastYear(organizationID)?.Year;
        if (row.Year != null && year != null && int.Parse(year) < int.Parse(row.Year))
          cache.RaiseExceptionHandling<TaxYearMaint.TaxYearFilter.year>(e.Row, (object) row.Year, (Exception) new PXSetPropertyException((IBqlTable) row, "Tax periods cannot be displayed for the {0} tax year because the corresponding financial periods have not yet been generated for the {1} company.", (PXErrorLevel) 2, new object[2]
          {
            (object) row.Year,
            (object) PXAccess.GetOrganizationCD(row.OrganizationID)
          }));
        else if (!this.IsPeriodSynchronized())
          cache.RaiseExceptionHandling<TaxYearMaint.TaxYearFilter.year>((object) row, (object) null, (Exception) new PXSetPropertyException((IBqlTable) row, "Tax periods do not match the financial periods in the general ledger for the {0} company. Use the {1} action to correct the period structure for the tax year.", (PXErrorLevel) 2, new object[2]
          {
            (object) PXAccess.GetOrganizationCD(row.OrganizationID),
            (object) "Synchronize Periods with GL"
          }));
      }
    }
    bool flag1 = false;
    bool flag2 = false;
    nullable1 = row.OrganizationID;
    if (nullable1.HasValue)
    {
      nullable1 = row.VendorID;
      if (nullable1.HasValue)
      {
        PX.Objects.GL.DAC.Organization organization = PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(PXSelectBase<PX.Objects.GL.DAC.Organization, PXViewOf<PX.Objects.GL.DAC.Organization>.BasedOn<SelectFromBase<PX.Objects.GL.DAC.Organization, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.GL.DAC.Organization.organizationID, IBqlInt>.IsEqual<BqlField<TaxYearMaint.TaxYearFilter.organizationID, IBqlInt>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()));
        bool? nullable3;
        if (organization == null)
        {
          nullable2 = new bool?();
          nullable3 = nullable2;
        }
        else
          nullable3 = organization.FileTaxesByBranches;
        nullable2 = nullable3;
        flag1 = nullable2.GetValueOrDefault();
        flag2 = ((IQueryable<PXResult<TaxYearMaint.TaxPeriodEx>>) PXSelectBase<TaxYearMaint.TaxPeriodEx, PXViewOf<TaxYearMaint.TaxPeriodEx>.BasedOn<SelectFromBase<TaxYearMaint.TaxPeriodEx, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxYearMaint.TaxPeriodEx.organizationID, Equal<BqlField<TaxYearMaint.TaxYearFilter.organizationID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<TaxYearMaint.TaxPeriodEx.vendorID, IBqlInt>.IsEqual<BqlField<TaxYearMaint.TaxYearFilter.vendorID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<TaxYearMaint.TaxPeriodEx.status, IBqlString>.IsEqual<TaxPeriodStatus.prepared>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Any<PXResult<TaxYearMaint.TaxPeriodEx>>();
      }
    }
    PXAction<TaxYearMaint.TaxYearFilter> prepareTaxReport = this.RedirectToPrepareTaxReport;
    int num6;
    if (flag1 || !flag2)
    {
      nullable1 = row.VendorID;
      num6 = nullable1.HasValue ? 1 : 0;
    }
    else
      num6 = 0;
    ((PXAction) prepareTaxReport).SetEnabled(num6 != 0);
    ((PXAction) this.RedirectToReleaseTaxReport).SetEnabled(flag2);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<TaxYearMaint.TaxYearFilter, TaxYearMaint.TaxYearFilter.taxPeriodType> e)
  {
    TaxYearMaint.TaxYearFilter row = e.Row;
    if (row == null || !row.OrganizationID.HasValue || !row.VendorID.HasValue || row.Year == null || ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<TaxYearMaint.TaxYearFilter, TaxYearMaint.TaxYearFilter.taxPeriodType>, TaxYearMaint.TaxYearFilter, object>) e).OldValue == e.NewValue || !(e.NewValue.ToString() == "F"))
      return;
    OrganizationFinYear organizationFinYearById = this.FinPeriodRepository.FindOrganizationFinYearByID(row.OrganizationID, row.Year);
    if (organizationFinYearById == null)
      return;
    row.StartDate = organizationFinYearById.StartDate;
  }

  protected virtual void TaxYearFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    TaxYearMaint.TaxYearFilter row = e.Row as TaxYearMaint.TaxYearFilter;
    TaxYearMaint.TaxYearFilter oldRow = e.OldRow as TaxYearMaint.TaxYearFilter;
    if (row == null || oldRow == null)
      return;
    int? nullable1;
    if (!((PXSelectBase) this.TaxYearFilterSelectView).Cache.ObjectsEqual<TaxYearMaint.TaxYearFilter.organizationID, TaxYearMaint.TaxYearFilter.vendorID>((object) row, (object) oldRow))
    {
      nullable1 = row.OrganizationID;
      if (nullable1.HasValue)
      {
        nullable1 = row.VendorID;
        if (nullable1.HasValue)
        {
          TaxPeriod currentTaxPeriod = this.TaxCalendar.GetOrCreateCurrentTaxPeriod(((PXSelectBase) this.TaxYearSelectView).Cache, ((PXSelectBase) this.TaxPeriodSelectView).Cache, row.OrganizationID, row.VendorID);
          row.Year = currentTaxPeriod?.TaxYear;
          if (VendorMaint.GetByID((PXGraph) this, row.VendorID).TaxPeriodType == "F")
          {
            TaxYear taxYear = PXResultset<TaxYear>.op_Implicit(PXSelectBase<TaxYear, PXSelect<TaxYear, Where<TaxYear.year, Equal<Required<TaxYear.year>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) row.Year
            }));
            row.StartDate = (DateTime?) taxYear?.StartDate;
          }
          ((PXSelectBase) this.TaxYearSelectView).Cache.Clear();
          ((PXSelectBase) this.TaxYearSelectView).Cache.ClearQueryCacheObsolete();
          ((PXSelectBase) this.TaxPeriodSelectView).Cache.Clear();
          ((PXSelectBase) this.TaxPeriodSelectView).Cache.ClearQueryCacheObsolete();
          goto label_9;
        }
      }
      row.Year = (string) null;
    }
label_9:
    nullable1 = row.OrganizationID;
    if (nullable1.HasValue)
    {
      nullable1 = row.VendorID;
      if (nullable1.HasValue && row.Year != null)
      {
        TaxYearMaint.TaxYearEx currentTaxYear;
        if (!((PXSelectBase) this.TaxYearFilterSelectView).Cache.ObjectsEqual<TaxYearMaint.TaxYearFilter.organizationID, TaxYearMaint.TaxYearFilter.vendorID, TaxYearMaint.TaxYearFilter.year>((object) row, (object) oldRow))
        {
          ((PXSelectBase) this.TaxYearExSelectView).Cache.Clear();
          ((PXSelectBase) this.TaxYearExSelectView).Cache.ClearQueryCacheObsolete();
          ((PXSelectBase) this.TaxPeriodExSelectView).Cache.Clear();
          ((PXSelectBase) this.TaxPeriodExSelectView).Cache.ClearQueryCacheObsolete();
          IFinPeriodRepository service = ((PXGraph) this).GetService<IFinPeriodRepository>();
          nullable1 = row.OrganizationID;
          int? organizationID = new int?(nullable1.GetValueOrDefault());
          string year = service.FindLastYear(organizationID)?.Year;
          if (year != null)
          {
            if (int.Parse(year) < int.Parse(row.Year) && row.TaxPeriodType == "F")
            {
              row.StartDate = new DateTime?();
              row.IsStartDateEditable = new bool?(false);
              cache.RaiseExceptionHandling<TaxYearMaint.TaxYearFilter.year>(e.Row, (object) row.Year, (Exception) new PXSetPropertyException("Tax periods cannot be displayed for the {0} tax year because the corresponding financial periods have not yet been generated for the {1} company.", (PXErrorLevel) 2, new object[2]
              {
                (object) row.Year,
                (object) PXAccess.GetOrganizationCD(row.OrganizationID)
              }));
              return;
            }
            cache.RaiseExceptionHandling<TaxYearMaint.TaxYearFilter.year>(e.Row, (object) row.Year, (Exception) null);
          }
          currentTaxYear = this.GetOrCreateTaxYearWithPeriodsInCacheByFilter(row);
          if (currentTaxYear == null)
            return;
          row.TaxPeriodType = currentTaxYear.TaxPeriodType;
          row.StartDate = currentTaxYear.StartDate;
          TaxYearMaint.TaxYearFilter taxYearFilter = row;
          nullable1 = currentTaxYear.PlanPeriodsCount;
          int? periodsCount = currentTaxYear.PeriodsCount;
          bool? nullable2 = new bool?(!(nullable1.GetValueOrDefault() == periodsCount.GetValueOrDefault() & nullable1.HasValue == periodsCount.HasValue));
          taxYearFilter.ShortTaxYear = nullable2;
          row.IsTaxPeriodTypeEditable = new bool?(this.IsTaxPeriodTypeEditable(row));
        }
        else
        {
          currentTaxYear = ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current;
          if (!(oldRow.TaxPeriodType != row.TaxPeriodType))
          {
            DateTime? startDate1 = oldRow.StartDate;
            DateTime? startDate2 = row.StartDate;
            if ((startDate1.HasValue == startDate2.HasValue ? (startDate1.HasValue ? (startDate1.GetValueOrDefault() != startDate2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
              goto label_28;
          }
          DateTime? startDate;
          if (row.TaxPeriodType == "F")
          {
            startDate = row.StartDate;
            if (startDate.HasValue)
            {
              if (PXResultset<PX.Objects.GL.FinPeriods.TableDefinition.FinYear>.op_Implicit(PXSelectBase<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, PXSelect<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.startDate, Equal<Required<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.startDate>>, And<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, Equal<Required<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year>>, And<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<Required<FinPeriod.organizationID>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
              {
                (object) row.StartDate,
                (object) row.Year,
                (object) row.OrganizationID
              })) == null)
              {
                cache.RaiseExceptionHandling<TaxYearMaint.TaxYearFilter.taxPeriodType>((object) row, (object) row.TaxPeriodType, (Exception) new PXSetPropertyException("The Financial Period type of tax period cannot be selected because the tax year start date does not match the financial year start date."));
                this.SetIsDirty(false);
                return;
              }
            }
          }
          if (oldRow.TaxPeriodType != row.TaxPeriodType)
          {
            row.ShortTaxYear = new bool?(false);
            currentTaxYear.PeriodsCount = new int?();
          }
          currentTaxYear.TaxPeriodType = row.TaxPeriodType;
          currentTaxYear.StartDate = row.StartDate;
          ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Update(currentTaxYear);
          startDate = currentTaxYear.StartDate;
          if (startDate.HasValue && currentTaxYear.TaxPeriodType != null)
            this.RegeneratePeriodsAndPutIntoCache(currentTaxYear);
        }
label_28:
        ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current = currentTaxYear;
        goto label_30;
      }
    }
    ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current = (TaxYearMaint.TaxYearEx) null;
    row.TaxPeriodType = (string) null;
    row.StartDate = new DateTime?();
    row.ShortTaxYear = new bool?();
    row.IsStartDateEditable = new bool?();
label_30:
    row.IsStartDateEditable = new bool?(this.IsStartDateEditable(row));
  }

  /// <summary>Set IsDirty value of the graph</summary>
  protected virtual void SetIsDirty(bool value)
  {
    ((PXSelectBase) this.TaxPeriodExSelectView).Cache.IsDirty = value;
    ((PXSelectBase) this.TaxYearExSelectView).Cache.IsDirty = value;
  }

  [PXDeleteButton]
  [PXUIField]
  protected virtual IEnumerable delete(PXAdapter adapter)
  {
    ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Delete(((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current);
    ((PXGraph) this).Actions.PressSave();
    ((PXSelectBase<TaxYearMaint.TaxYearFilter>) this.TaxYearFilterSelectView).Current.Year = (string) null;
    ((PXSelectBase<TaxYearMaint.TaxYearFilter>) this.TaxYearFilterSelectView).Update(((PXSelectBase<TaxYearMaint.TaxYearFilter>) this.TaxYearFilterSelectView).Current);
    return adapter.Get();
  }

  [PXButton(ImageSet = "main", ImageKey = "RecordDel")]
  [PXUIField]
  protected virtual IEnumerable deletePeriod(PXAdapter adapter)
  {
    TaxYearMaint.TaxYearEx current = ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current;
    if (current == null)
      return adapter.Get();
    TaxYearMaint.TaxPeriodEx taxPeriodEx = this.GetActualStoredTaxPeriods().Last<TaxYearMaint.TaxPeriodEx>();
    if (current.TaxPeriodsInDBExist.GetValueOrDefault())
      ((PXSelectBase) this.TaxPeriodExSelectView).Cache.Delete((object) taxPeriodEx);
    else
      ((PXSelectBase) this.TaxPeriodExSelectView).Cache.Remove((object) taxPeriodEx);
    TaxYearMaint.TaxYearEx taxYearEx = current;
    int? periodsCount = taxYearEx.PeriodsCount;
    taxYearEx.PeriodsCount = periodsCount.HasValue ? new int?(periodsCount.GetValueOrDefault() - 1) : new int?();
    ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Update(current);
    return adapter.Get();
  }

  [PXButton(ImageSet = "main", ImageKey = "AddNew")]
  [PXUIField]
  protected virtual IEnumerable addPeriod(PXAdapter adapter)
  {
    TaxYearMaint.TaxYearEx current = ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current;
    if (current == null)
      return adapter.Get();
    TaxYearMaint.TaxYearEx taxYearEx = current;
    int? periodsCount = taxYearEx.PeriodsCount;
    taxYearEx.PeriodsCount = periodsCount.HasValue ? new int?(periodsCount.GetValueOrDefault() + 1) : new int?();
    ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Update(current);
    TaxYearMaint.TaxPeriodEx taxPeriodEx1 = this.TaxCalendarEx.CreateWithCorrespondingTaxPeriodType(PX.Objects.TX.TaxCalendar.CreationParams.FromTaxYear((TaxYear) current)).TaxPeriods.Last<TaxYearMaint.TaxPeriodEx>();
    if (((PXSelectBase) this.TaxPeriodExSelectView).Cache.GetStatus((object) taxPeriodEx1) == 3)
    {
      ((PXSelectBase) this.TaxPeriodExSelectView).Cache.SetStatus((object) taxPeriodEx1, (PXEntryStatus) 5);
    }
    else
    {
      TaxYearMaint.TaxPeriodEx taxPeriodEx2 = (TaxYearMaint.TaxPeriodEx) ((PXSelectBase) this.TaxPeriodExSelectView).Cache.Insert((object) taxPeriodEx1);
      if (!current.TaxPeriodsInDBExist.GetValueOrDefault())
        ((PXSelectBase) this.TaxPeriodExSelectView).Cache.SetStatus((object) taxPeriodEx2, (PXEntryStatus) 5);
    }
    return adapter.Get();
  }

  [PXButton(Category = "Period Management", DisplayOnMainToolbar = true)]
  [PXUIField]
  protected virtual IEnumerable redirectToPrepareTaxReport(PXAdapter adapter)
  {
    TaxYearMaint.TaxYearFilter current = ((PXSelectBase<TaxYearMaint.TaxYearFilter>) this.TaxYearFilterSelectView).Current;
    int? nullable;
    int num;
    if (current == null)
    {
      num = 0;
    }
    else
    {
      nullable = current.OrganizationID;
      num = nullable.HasValue ? 1 : 0;
    }
    if (num != 0)
    {
      nullable = ((PXSelectBase<TaxYearMaint.TaxYearFilter>) this.TaxYearFilterSelectView).Current.VendorID;
      if (nullable.HasValue)
      {
        ReportTax instance = PXGraph.CreateInstance<ReportTax>();
        TaxPeriodFilter copy = (TaxPeriodFilter) ((PXSelectBase) instance.Period_Header).Cache.CreateCopy((object) ((PXSelectBase<TaxPeriodFilter>) instance.Period_Header).Current);
        copy.OrganizationID = ((PXSelectBase<TaxYearMaint.TaxYearFilter>) this.TaxYearFilterSelectView).Current.OrganizationID;
        copy.VendorID = ((PXSelectBase<TaxYearMaint.TaxYearFilter>) this.TaxYearFilterSelectView).Current.VendorID;
        ((PXSelectBase<TaxPeriodFilter>) instance.Period_Header).Update(copy);
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, string.Empty);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXButton(Category = "Period Management", DisplayOnMainToolbar = true)]
  [PXUIField]
  protected virtual IEnumerable redirectToReleaseTaxReport(PXAdapter adapter)
  {
    TaxYearMaint.TaxYearFilter current = ((PXSelectBase<TaxYearMaint.TaxYearFilter>) this.TaxYearFilterSelectView).Current;
    int? nullable;
    int num;
    if (current == null)
    {
      num = 0;
    }
    else
    {
      nullable = current.OrganizationID;
      num = nullable.HasValue ? 1 : 0;
    }
    if (num != 0)
    {
      nullable = ((PXSelectBase<TaxYearMaint.TaxYearFilter>) this.TaxYearFilterSelectView).Current.VendorID;
      if (nullable.HasValue)
      {
        ReportTaxReview instance = PXGraph.CreateInstance<ReportTaxReview>();
        TaxPeriodFilter copy = (TaxPeriodFilter) ((PXSelectBase) instance.Period_Header).Cache.CreateCopy((object) ((PXSelectBase<TaxPeriodFilter>) instance.Period_Header).Current);
        copy.OrganizationID = ((PXSelectBase<TaxYearMaint.TaxYearFilter>) this.TaxYearFilterSelectView).Current.OrganizationID;
        copy.VendorID = ((PXSelectBase<TaxYearMaint.TaxYearFilter>) this.TaxYearFilterSelectView).Current.VendorID;
        ((PXSelectBase<TaxPeriodFilter>) instance.Period_Header).Update(copy);
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, string.Empty);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXButton(Category = "Reports")]
  [PXUIField]
  protected virtual IEnumerable redirectToTaxSummaryReport(PXAdapter adapter)
  {
    this.TryRedirectToReport("TX621000");
    return adapter.Get();
  }

  [PXButton(Category = "Reports")]
  [PXUIField]
  protected virtual IEnumerable redirectToTaxDetailsReport(PXAdapter adapter)
  {
    this.TryRedirectToReport("TX620500");
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable viewTaxPeriodDetails(PXAdapter adapter)
  {
    ReportTaxDetail instance = PXGraph.CreateInstance<ReportTaxDetail>();
    TaxYearMaint.TaxPeriodEx current = ((PXSelectBase<TaxYearMaint.TaxPeriodEx>) this.TaxPeriodExSelectView).Current;
    ((PXSelectBase<TaxHistoryMaster>) instance.History_Header).Current.OrganizationID = current.OrganizationID;
    ((PXSelectBase<TaxHistoryMaster>) instance.History_Header).Current.BranchID = new int?();
    ((PXSelectBase<TaxHistoryMaster>) instance.History_Header).Current.VendorID = current.VendorID;
    ((PXSelectBase<TaxHistoryMaster>) instance.History_Header).Current.TaxPeriodID = current.TaxPeriodID;
    TaxReportLine taxReportLine = PXResultset<TaxReportLine>.op_Implicit(PXSelectBase<TaxReportLine, PXSelect<TaxReportLine, Where<TaxReportLine.vendorID, Equal<Required<TaxReportLine.vendorID>>, And<TaxReportLine.netTax, Equal<True>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) current.VendorID
    }));
    ((PXSelectBase<TaxHistoryMaster>) instance.History_Header).Current.LineNbr = (int?) taxReportLine?.LineNbr;
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, string.Empty);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
    throw requiredException;
  }

  [PXButton(Category = "Period Management")]
  [PXUIField]
  protected virtual IEnumerable syncTaxPeriods(PXAdapter adapter)
  {
    TaxYearMaint.TaxYearEx current = ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current;
    int? nullable;
    int num;
    if (current == null)
    {
      num = 0;
    }
    else
    {
      nullable = current.OrganizationID;
      num = nullable.HasValue ? 1 : 0;
    }
    if (num != 0)
    {
      nullable = ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current.VendorID;
      if (nullable.HasValue && ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current.TaxPeriodType == "F" && !this.IsPeriodSynchronized())
      {
        this.SyncPeriodsWithGL(((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Current);
        ((PXGraph) this).Actions.PressSave();
      }
    }
    return adapter.Get();
  }

  protected virtual void TryRedirectToReport(string reportPage)
  {
    TaxYearMaint.TaxYearFilter current = ((PXSelectBase<TaxYearMaint.TaxYearFilter>) this.TaxYearFilterSelectView).Current;
    int? nullable;
    int num;
    if (current == null)
    {
      num = 0;
    }
    else
    {
      nullable = current.OrganizationID;
      num = nullable.HasValue ? 1 : 0;
    }
    if (num == 0)
      return;
    nullable = ((PXSelectBase<TaxYearMaint.TaxYearFilter>) this.TaxYearFilterSelectView).Current.VendorID;
    if (nullable.HasValue && ((PXSelectBase<TaxYearMaint.TaxPeriodEx>) this.TaxPeriodExSelectView).Current != null)
    {
      BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) OrganizationMaint.FindOrganizationByID((PXGraph) this, ((PXSelectBase<TaxYearMaint.TaxYearFilter>) this.TaxYearFilterSelectView).Current.OrganizationID).BAccountID
      }));
      throw new PXRedirectWithReportException((PXGraph) this, new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["OrgBAccountID"] = baccountR.AcctCD,
        ["VendorID"] = VendorMaint.GetByID((PXGraph) this, ((PXSelectBase<TaxYearMaint.TaxYearFilter>) this.TaxYearFilterSelectView).Current.VendorID).AcctCD,
        ["TaxPeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(((PXSelectBase<TaxYearMaint.TaxPeriodEx>) this.TaxPeriodExSelectView).Current.TaxPeriodID)
      }, reportPage, string.Empty, (CurrentLocalization) null), string.Empty);
    }
  }

  protected virtual bool IsStartDateEditable(TaxYearMaint.TaxYearFilter taxYearFilter)
  {
    if (!taxYearFilter.OrganizationID.HasValue || !taxYearFilter.VendorID.HasValue)
      return false;
    TaxYear taxYear = PXResultset<TaxYear>.op_Implicit(PXSelectBase<TaxYear, PXSelect<TaxYear, Where<TaxYear.organizationID, Equal<Required<TaxYear.organizationID>>, And<TaxYear.vendorID, Equal<Required<TaxYear.vendorID>>, And<TaxYear.year, NotEqual<Required<TaxYear.year>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
    {
      (object) taxYearFilter.OrganizationID.Value,
      (object) taxYearFilter.VendorID.Value,
      (object) taxYearFilter.Year
    }));
    TaxPeriod taxPeriod = PXResultset<TaxPeriod>.op_Implicit(PXSelectBase<TaxPeriod, PXSelect<TaxPeriod, Where<TaxPeriod.organizationID, Equal<Required<TaxPeriod.organizationID>>, And<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>, And<TaxPeriod.status, NotEqual<TaxPeriodStatus.open>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
    {
      (object) taxYearFilter.OrganizationID.Value,
      (object) taxYearFilter.VendorID.Value
    }));
    return taxYear == null && taxPeriod == null && taxYearFilter.TaxPeriodType != "F";
  }

  protected virtual bool IsTaxPeriodTypeEditable(TaxYearMaint.TaxYearFilter taxYearFilter)
  {
    TaxYear lastTaxYear = TaxYearMaint.FindLastTaxYear((PXGraph) this, taxYearFilter.OrganizationID, taxYearFilter.VendorID);
    if (lastTaxYear == null)
      return true;
    if (string.CompareOrdinal(taxYearFilter.Year, lastTaxYear.Year) < 0)
      return false;
    return PXResultset<TaxPeriod>.op_Implicit(PXSelectBase<TaxPeriod, PXSelect<TaxPeriod, Where<TaxPeriod.organizationID, Equal<Required<TaxPeriod.organizationID>>, And<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>, And<TaxPeriod.taxYear, Equal<Required<TaxPeriod.taxYear>>, And<TaxPeriod.status, NotEqual<TaxPeriodStatus.open>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
    {
      (object) taxYearFilter.OrganizationID,
      (object) taxYearFilter.VendorID,
      (object) taxYearFilter.Year
    })) == null;
  }

  protected virtual TaxYearMaint.TaxYearEx GetOrCreateTaxYearWithPeriodsInCacheByFilter(
    TaxYearMaint.TaxYearFilter taxYearFilter)
  {
    TaxYearMaint.TaxYearEx periodsInCacheByFilter = TaxYearMaint.FindTaxYearExByKey((PXGraph) this, taxYearFilter.OrganizationID, taxYearFilter.VendorID, taxYearFilter.Year);
    List<TaxYearMaint.TaxPeriodEx> taxPeriodExList = (List<TaxYearMaint.TaxPeriodEx>) null;
    if (periodsInCacheByFilter != null)
    {
      periodsInCacheByFilter.Existing = new bool?(true);
      TaxYearMaint.TaxPeriodEx taxPeriodEx1 = PXResultset<TaxYearMaint.TaxPeriodEx>.op_Implicit(PXSelectBase<TaxYearMaint.TaxPeriodEx, PXSelect<TaxYearMaint.TaxPeriodEx, Where<TaxYearMaint.TaxPeriodEx.organizationID, Equal<Required<TaxYearMaint.TaxPeriodEx.organizationID>>, And<TaxYearMaint.TaxPeriodEx.vendorID, Equal<Required<TaxYearMaint.TaxPeriodEx.vendorID>>, And<TaxYearMaint.TaxPeriodEx.taxYear, Equal<Required<TaxYearMaint.TaxPeriodEx.taxYear>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
      {
        (object) taxYearFilter.OrganizationID.Value,
        (object) taxYearFilter.VendorID.Value,
        (object) taxYearFilter.Year
      }));
      periodsInCacheByFilter.TaxPeriodsInDBExist = new bool?(taxPeriodEx1 != null);
      if (periodsInCacheByFilter.TaxPeriodsInDBExist.GetValueOrDefault())
      {
        using (new PXReadBranchRestrictedScope(periodsInCacheByFilter.OrganizationID.SingleToArray<int?>(), (int?[]) null, false, false))
        {
          IEnumerable<PXResult<TaxYearMaint.TaxPeriodEx, TaxReportLine, TaxHistory>> pxResults = ((IEnumerable<PXResult<TaxYearMaint.TaxPeriodEx>>) PXSelectBase<TaxYearMaint.TaxPeriodEx, PXSelectJoinGroupBy<TaxYearMaint.TaxPeriodEx, LeftJoin<TaxReportLine, On<TaxYearMaint.TaxPeriodEx.vendorID, Equal<TaxReportLine.vendorID>, And<TaxReportLine.netTax, Equal<True>>>, LeftJoin<TaxHistory, On<TaxYearMaint.TaxPeriodEx.vendorID, Equal<TaxHistory.vendorID>, And<TaxReportLine.lineNbr, Equal<TaxHistory.lineNbr>, And<TaxReportLine.taxReportRevisionID, Equal<TaxHistory.taxReportRevisionID>, And<TaxYearMaint.TaxPeriodEx.taxPeriodID, Equal<TaxHistory.taxPeriodID>>>>>>>, Where<TaxYearMaint.TaxPeriodEx.organizationID, Equal<Current<TaxYearMaint.TaxYearEx.organizationID>>, And<TaxYearMaint.TaxPeriodEx.vendorID, Equal<Current<TaxYearMaint.TaxYearEx.vendorID>>, And<TaxYearMaint.TaxPeriodEx.taxYear, Equal<Current<TaxYearMaint.TaxYearEx.year>>>>>, Aggregate<GroupBy<TaxYearMaint.TaxPeriodEx.taxPeriodID, Sum<TaxHistory.reportFiledAmt>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
          {
            (object) periodsInCacheByFilter
          }, new object[1]
          {
            (object) periodsInCacheByFilter.OrganizationID
          })).AsEnumerable<PXResult<TaxYearMaint.TaxPeriodEx>>().Cast<PXResult<TaxYearMaint.TaxPeriodEx, TaxReportLine, TaxHistory>>();
          taxPeriodExList = new List<TaxYearMaint.TaxPeriodEx>();
          foreach (PXResult<TaxYearMaint.TaxPeriodEx, TaxReportLine, TaxHistory> pxResult in pxResults)
          {
            TaxHistory taxHistory = PXResult<TaxYearMaint.TaxPeriodEx, TaxReportLine, TaxHistory>.op_Implicit(pxResult);
            TaxYearMaint.TaxPeriodEx taxPeriodEx2 = PXResult<TaxYearMaint.TaxPeriodEx, TaxReportLine, TaxHistory>.op_Implicit(pxResult);
            taxPeriodEx2.NetTaxAmt = taxHistory.ReportFiledAmt;
            if (((PXSelectBase) this.TaxPeriodExSelectView).Cache.Locate((object) taxPeriodEx2) != null)
              ((PXSelectBase) this.TaxPeriodExSelectView).Cache.Remove((object) taxPeriodEx2);
            ((PXSelectBase) this.TaxPeriodExSelectView).Cache.SetStatus((object) taxPeriodEx2, (PXEntryStatus) 5);
          }
        }
      }
      else
        taxPeriodExList = this.TaxCalendarEx.CreateWithCorrespondingTaxPeriodType(PX.Objects.TX.TaxCalendar.CreationParams.FromTaxYear((TaxYear) periodsInCacheByFilter)).TaxPeriods;
    }
    else
    {
      PX.Objects.AP.Vendor byId = VendorMaint.GetByID((PXGraph) this, taxYearFilter.VendorID);
      TaxYearWithPeriods<TaxYearMaint.TaxYearEx, TaxYearMaint.TaxPeriodEx> taxYearWithPeriods = this.TaxCalendarEx.Create(taxYearFilter.OrganizationID, VendorMaint.GetByID((PXGraph) this, taxYearFilter.VendorID), new int?(Convert.ToInt32(taxYearFilter.Year)), byId.TaxPeriodType == "F" ? taxYearFilter.StartDate : new DateTime?());
      taxYearWithPeriods.TaxYear = (TaxYearMaint.TaxYearEx) ((PXSelectBase) this.TaxYearExSelectView).Cache.Insert((object) taxYearWithPeriods.TaxYear);
      ((PXSelectBase) this.TaxYearExSelectView).Cache.SetStatus((object) taxYearWithPeriods.TaxYear, (PXEntryStatus) 5);
      periodsInCacheByFilter = taxYearWithPeriods.TaxYear;
      taxPeriodExList = taxYearWithPeriods.TaxPeriods;
    }
    if (taxPeriodExList != null)
    {
      foreach (object obj in taxPeriodExList)
      {
        ((PXSelectBase) this.TaxPeriodExSelectView).Cache.SetStatus((object) (TaxYearMaint.TaxPeriodEx) ((PXSelectBase) this.TaxPeriodExSelectView).Cache.Insert(obj), (PXEntryStatus) 5);
        this.SetIsDirty(false);
      }
    }
    return periodsInCacheByFilter;
  }

  protected virtual void RegeneratePeriodsAndPutIntoCache(TaxYearMaint.TaxYearEx currentTaxYear)
  {
    this.GeneratePeriods(currentTaxYear, (TaxYearMaint.PeriodPredicate) (period => true));
  }

  protected virtual void GeneratePeriods(
    TaxYearMaint.TaxYearEx currentTaxYear,
    TaxYearMaint.PeriodPredicate predicate)
  {
    TaxYearWithPeriods<TaxYearMaint.TaxYearEx, TaxYearMaint.TaxPeriodEx> correspondingTaxPeriodType = this.TaxCalendarEx.CreateWithCorrespondingTaxPeriodType(PX.Objects.TX.TaxCalendar.CreationParams.FromTaxYear((TaxYear) currentTaxYear));
    correspondingTaxPeriodType.TaxYear.Existing = currentTaxYear.Existing;
    correspondingTaxPeriodType.TaxYear.TaxPeriodsInDBExist = currentTaxYear.TaxPeriodsInDBExist;
    correspondingTaxPeriodType.TaxYear.Year = currentTaxYear.Year;
    currentTaxYear = ((PXSelectBase<TaxYearMaint.TaxYearEx>) this.TaxYearExSelectView).Update(correspondingTaxPeriodType.TaxYear);
    Dictionary<string, TaxYearMaint.TaxPeriodEx> dictionary = GraphHelper.RowCast<TaxYearMaint.TaxPeriodEx>((IEnumerable) PXSelectBase<TaxYearMaint.TaxPeriodEx, PXSelect<TaxYearMaint.TaxPeriodEx, Where<TaxYearMaint.TaxPeriodEx.organizationID, Equal<Required<TaxYearMaint.TaxPeriodEx.organizationID>>, And<TaxYearMaint.TaxPeriodEx.vendorID, Equal<Required<TaxYearMaint.TaxPeriodEx.vendorID>>, And<TaxYearMaint.TaxPeriodEx.taxYear, Equal<Required<TaxYearMaint.TaxPeriodEx.taxYear>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) currentTaxYear.OrganizationID,
      (object) currentTaxYear.VendorID,
      (object) currentTaxYear.Year
    })).ToDictionary<TaxYearMaint.TaxPeriodEx, string, TaxYearMaint.TaxPeriodEx>((Func<TaxYearMaint.TaxPeriodEx, string>) (period => period.TaxPeriodID), (Func<TaxYearMaint.TaxPeriodEx, TaxYearMaint.TaxPeriodEx>) (period => period));
    if (dictionary.Any<KeyValuePair<string, TaxYearMaint.TaxPeriodEx>>())
    {
      currentTaxYear.TaxPeriodsInDBExist = new bool?(true);
      foreach (TaxYearMaint.TaxPeriodEx taxPeriod in correspondingTaxPeriodType.TaxPeriods)
      {
        if (dictionary.ContainsKey(taxPeriod.TaxPeriodID))
        {
          if (predicate(taxPeriod))
            ((PXSelectBase<TaxYearMaint.TaxPeriodEx>) this.TaxPeriodExSelectView).Update(taxPeriod);
        }
        else
          ((PXSelectBase<TaxYearMaint.TaxPeriodEx>) this.TaxPeriodExSelectView).Insert(taxPeriod);
      }
      HashSet<string> newPeriodSet = new HashSet<string>(correspondingTaxPeriodType.TaxPeriods.Select<TaxYearMaint.TaxPeriodEx, string>((Func<TaxYearMaint.TaxPeriodEx, string>) (period => period.TaxPeriodID)));
      foreach (TaxYearMaint.TaxPeriodEx taxPeriodEx in dictionary.Values.Where<TaxYearMaint.TaxPeriodEx>((Func<TaxYearMaint.TaxPeriodEx, bool>) (period => !newPeriodSet.Contains(period.TaxPeriodID))))
        ((PXSelectBase<TaxYearMaint.TaxPeriodEx>) this.TaxPeriodExSelectView).Delete(taxPeriodEx);
    }
    else
    {
      if (!currentTaxYear.TaxPeriodsInDBExist.GetValueOrDefault())
        ((PXSelectBase) this.TaxPeriodExSelectView).Cache.Clear();
      currentTaxYear.TaxPeriodsInDBExist = new bool?(false);
      foreach (TaxYearMaint.TaxPeriodEx taxPeriod in correspondingTaxPeriodType.TaxPeriods)
        ((PXSelectBase) this.TaxPeriodExSelectView).Cache.SetStatus((object) ((PXSelectBase<TaxYearMaint.TaxPeriodEx>) this.TaxPeriodExSelectView).Insert(taxPeriod), (PXEntryStatus) 5);
    }
  }

  protected virtual void PeriodButtonsFieldSelectingHandler(PXFieldSelectingEventArgs e)
  {
    e.ReturnState = (object) PXButtonState.CreateDefaultState<TaxYearMaint.TaxYearFilter>(e.ReturnState);
    bool flag = false;
    TaxYearMaint.TaxYearFilter current = ((PXSelectBase<TaxYearMaint.TaxYearFilter>) this.TaxYearFilterSelectView).Current;
    if (current != null && current.ShortTaxYear.GetValueOrDefault())
      flag = PXResultset<TaxYearMaint.TaxPeriodEx>.op_Implicit(PXSelectBase<TaxYearMaint.TaxPeriodEx, PXSelect<TaxYearMaint.TaxPeriodEx, Where<TaxYearMaint.TaxPeriodEx.organizationID, Equal<Required<TaxYearMaint.TaxPeriodEx.organizationID>>, And<TaxYearMaint.TaxPeriodEx.vendorID, Equal<Required<TaxYearMaint.TaxPeriodEx.vendorID>>, And<TaxYearMaint.TaxPeriodEx.taxYear, Greater<Required<TaxYearMaint.TaxPeriodEx.taxYear>>, And<TaxYearMaint.TaxPeriodEx.status, NotEqual<TaxPeriodStatus.open>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
      {
        (object) current.OrganizationID,
        (object) current.VendorID,
        (object) current.Year
      })) == null;
    ((PXFieldState) e.ReturnState).Enabled = flag;
  }

  protected virtual IEnumerable<TaxYearMaint.TaxPeriodEx> GetActualStoredTaxPeriods()
  {
    return (IEnumerable<TaxYearMaint.TaxPeriodEx>) ((PXSelectBase) this.TaxPeriodExSelectView).Cache.Cached.Cast<TaxYearMaint.TaxPeriodEx>().Where<TaxYearMaint.TaxPeriodEx>((Func<TaxYearMaint.TaxPeriodEx, bool>) (period =>
    {
      PXEntryStatus status = ((PXSelectBase) this.TaxPeriodExSelectView).Cache.GetStatus((object) period);
      return status == 5 || status == 2 || status == 1;
    })).OrderBy<TaxYearMaint.TaxPeriodEx, string>((Func<TaxYearMaint.TaxPeriodEx, string>) (period => period.TaxPeriodID));
  }

  public static bool PrepearedTaxPeriodForVendorExists(PXGraph graph, int? vendorID)
  {
    return PXResultset<TaxPeriod>.op_Implicit(PXSelectBase<TaxPeriod, PXSelect<TaxPeriod, Where<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>, And<TaxPeriod.status, Equal<TaxPeriodStatus.prepared>>>>.Config>.Select(graph, new object[1]
    {
      (object) vendorID
    })) != null;
  }

  protected virtual bool IsPeriodSynchronized()
  {
    IEnumerable<TaxYearMaint.TaxPeriodEx> storedTaxPeriods = this.GetActualStoredTaxPeriods();
    if (storedTaxPeriods != null)
    {
      foreach (TaxPeriod taxPeriod in storedTaxPeriods)
      {
        DateTime dateTime1 = this.FinPeriodRepository.PeriodStartDate(taxPeriod.TaxPeriodID, taxPeriod.OrganizationID);
        DateTime dateTime2 = this.FinPeriodRepository.PeriodEndDate(taxPeriod.TaxPeriodID, taxPeriod.OrganizationID);
        DateTime? nullable = taxPeriod.StartDate;
        DateTime dateTime3 = dateTime1;
        if ((nullable.HasValue ? (nullable.GetValueOrDefault() != dateTime3 ? 1 : 0) : 1) == 0)
        {
          nullable = taxPeriod.EndDate;
          DateTime dateTime4 = dateTime2.AddDays(1.0);
          if ((nullable.HasValue ? (nullable.GetValueOrDefault() != dateTime4 ? 1 : 0) : 1) == 0)
            continue;
        }
        nullable = taxPeriod.StartDate;
        DateTime? endDate = taxPeriod.EndDate;
        if ((nullable.HasValue == endDate.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != endDate.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
          return false;
      }
    }
    return true;
  }

  protected virtual void SyncPeriodsWithGL(TaxYearMaint.TaxYearEx currentTaxYear)
  {
    foreach (TaxYearMaint.TaxPeriodEx actualStoredTaxPeriod in this.GetActualStoredTaxPeriods())
    {
      if (actualStoredTaxPeriod.Status == "N" && actualStoredTaxPeriod.TaxYear == currentTaxYear.Year)
        ((PXSelectBase<TaxYearMaint.TaxPeriodEx>) this.TaxPeriodExSelectView).Delete(actualStoredTaxPeriod);
    }
    this.GeneratePeriods(currentTaxYear, (TaxYearMaint.PeriodPredicate) (period => period.Status == "N"));
  }

  public class TaxYearFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _TaxPeriodType;

    /// <summary>
    /// The reference to the <see cref="T:PX.Objects.GL.DAC.Organization" /> record to which the record belongs.
    /// </summary>
    [Organization(true, Required = true, IsDBField = false, FieldClass = "MULTICOMPANY")]
    public virtual int? OrganizationID { get; set; }

    /// <summary>
    /// The reference to the tax agency (<see cref="T:PX.Objects.AP.Vendor" />) record to which the record belongs.
    /// </summary>
    [TaxAgencyActive(Required = true)]
    public virtual int? VendorID { get; set; }

    [PXUIField(DisplayName = "Tax Year", Required = true)]
    [PXString(4, IsFixed = true)]
    [TaxYearMaint.TaxYearFilter.TaxYearSelector]
    public virtual string Year { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Short Tax Year")]
    public virtual bool? ShortTaxYear { get; set; }

    [PXDate]
    [PXUIField]
    public virtual DateTime? StartDate { get; set; }

    [PXDBString(1)]
    [PXDefault("M")]
    [PXUIField(DisplayName = "Tax Period Type")]
    [VendorTaxPeriodType.List]
    public virtual string TaxPeriodType
    {
      get => this._TaxPeriodType;
      set => this._TaxPeriodType = value;
    }

    [PXBool]
    public bool? IsStartDateEditable { get; set; }

    [PXBool]
    public bool? IsTaxPeriodTypeEditable { get; set; }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TaxYearMaint.TaxYearFilter.organizationID>
    {
    }

    public abstract class vendorID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaxYearMaint.TaxYearFilter.vendorID>
    {
    }

    public abstract class year : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxYearMaint.TaxYearFilter.year>
    {
    }

    public abstract class shortTaxYear : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      TaxYearMaint.TaxYearFilter.shortTaxYear>
    {
    }

    public abstract class startDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      TaxYearMaint.TaxYearFilter.startDate>
    {
    }

    public abstract class taxPeriodType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TaxYearMaint.TaxYearFilter.taxPeriodType>
    {
    }

    protected class TaxYearSelector : PXCustomSelectorAttribute
    {
      public TaxYearSelector()
        : base(typeof (Search<TaxYear.year, Where<TaxYear.organizationID, Equal<Current<TaxYearMaint.TaxYearFilter.organizationID>>, And<TaxYear.vendorID, Equal<Current<TaxYearMaint.TaxYearFilter.vendorID>>>>, OrderBy<Desc<TaxYear.year>>>), new System.Type[1]
        {
          typeof (TaxYear.year)
        })
      {
      }

      protected virtual IEnumerable GetRecords()
      {
        TaxYearMaint.TaxYearFilter current = (TaxYearMaint.TaxYearFilter) this._Graph.Caches[typeof (TaxYearMaint.TaxYearFilter)].Current;
        if (!current.OrganizationID.HasValue || !current.VendorID.HasValue)
          return (IEnumerable) new object[0];
        PXCache cach = this._Graph.Caches[typeof (TaxYear)];
        string key = ((PXSelectorAttribute) this).GenerateViewName() + "origSelect_";
        PXView pxView;
        if (!((Dictionary<string, PXView>) this._Graph.Views).TryGetValue(key, out pxView))
        {
          pxView = new PXView(this._Graph, !((PXSelectorAttribute) this)._DirtyRead, ((PXSelectorAttribute) this)._OriginalSelect);
          this._Graph.Views[key] = pxView;
        }
        List<TaxYear> list1 = GraphHelper.RowCast<TaxYear>((IEnumerable) pxView.SelectMultiBound(new object[1]
        {
          (object) current
        }, Array.Empty<object>())).ToList<TaxYear>();
        if (list1.Any<TaxYear>())
        {
          TaxPeriod lastTaxPeriod = TaxYearMaint.FindLastTaxPeriod(this._Graph, current.OrganizationID, current.VendorID);
          TaxYear taxYear = list1.First<TaxYear>();
          if (lastTaxPeriod != null && lastTaxPeriod.TaxYear == taxYear.Year && TaxYearMaint.IsAnyClosedTaxPeriodInTaxYear(this._Graph, current.OrganizationID, current.VendorID, taxYear.Year))
          {
            TaxYear copy = (TaxYear) cach.CreateCopy((object) taxYear);
            copy.Year = (Convert.ToInt32(copy.Year) + 1).ToString();
            list1.Insert(0, copy);
          }
        }
        else
        {
          PX.Objects.AP.Vendor byId = VendorMaint.GetByID(this._Graph, current.VendorID);
          TaxTran notReportedTaxTran = ReportTax.GetEarliestNotReportedTaxTran(this._Graph, byId, current.OrganizationID, new int?());
          if (notReportedTaxTran != null)
          {
            PX.Objects.TX.TaxCalendar<TaxYear, TaxPeriod> taxCalendar = new PX.Objects.TX.TaxCalendar<TaxYear, TaxPeriod>(this._Graph);
            DateTime? nullable = byId.TaxReportFinPeriod.GetValueOrDefault() ? notReportedTaxTran.FinDate : notReportedTaxTran.TranDate;
            int? organizationId = current.OrganizationID;
            PX.Objects.AP.Vendor vendor = byId;
            int? calendarYear = new int?(nullable.Value.Year);
            DateTime? baseDate = new DateTime?();
            TaxYearWithPeriods<TaxYear, TaxPeriod> taxYearWithPeriods = taxCalendar.Create(organizationId, vendor, calendarYear, baseDate);
            list1.Add(taxYearWithPeriods.TaxYear);
          }
          else
          {
            List<string> list2 = GraphHelper.RowCast<PX.Objects.GL.FinPeriods.TableDefinition.FinYear>((IEnumerable) PXSelectBase<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, PXSelect<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<Required<FinPeriod.organizationID>>>>.Config>.Select(this._Graph, new object[1]
            {
              (object) current.OrganizationID
            })).Select<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, string>((Func<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, string>) (finYear => finYear.Year)).ToList<string>();
            List<string> stringList1 = list2;
            int num = Convert.ToInt32(list2.First<string>()) - 1;
            string str1 = num.ToString();
            stringList1.Insert(0, str1);
            List<string> stringList2 = list2;
            num = Convert.ToInt32(list2.Last<string>()) + 1;
            string str2 = num.ToString();
            stringList2.Add(str2);
            list1 = list2.Select<string, TaxYear>((Func<string, TaxYear>) (year => new TaxYear()
            {
              Year = year
            })).ToList<TaxYear>();
          }
        }
        return (IEnumerable) list1;
      }
    }
  }

  public class TaxYearEx : TaxYear
  {
    [PXBool]
    public bool? TaxPeriodsInDBExist { get; set; }

    [PXBool]
    public bool? Existing { get; set; }

    public new abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TaxYearMaint.TaxYearEx.organizationID>
    {
    }

    public new abstract class vendorID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaxYearMaint.TaxYearEx.vendorID>
    {
    }

    public new abstract class year : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxYearMaint.TaxYearEx.year>
    {
    }
  }

  public class TaxPeriodEx : TaxPeriod
  {
    [PXBaseCury]
    [PXUIField(DisplayName = "Net Tax Amount")]
    public Decimal? NetTaxAmt { get; set; }

    public new abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TaxYearMaint.TaxPeriodEx.organizationID>
    {
    }

    public new abstract class taxPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TaxYearMaint.TaxPeriodEx.taxPeriodID>
    {
    }

    public new abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TaxYearMaint.TaxPeriodEx.vendorID>
    {
    }

    public new abstract class taxYear : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TaxYearMaint.TaxPeriodEx.taxYear>
    {
    }

    public new abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TaxYearMaint.TaxPeriodEx.status>
    {
    }
  }

  protected delegate bool PeriodPredicate(TaxYearMaint.TaxPeriodEx period);
}
