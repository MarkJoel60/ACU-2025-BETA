// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POAccrualInquiry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.Common;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO;

[TableAndChartDashboardType]
public class POAccrualInquiry : PXGraph<POAccrualInquiry>
{
  protected Lazy<IEnumerable<POAccrualInquiryResult>> RecordsCache;
  public PXFilter<POAccrualInquiryFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXSelect<POAccrualInquiryResult> ResultRecords;
  public PXAction<POAccrualInquiryFilter> viewDocument;
  public PXAction<POAccrualInquiryFilter> refreshAll;
  public PXCancel<POAccrualInquiryFilter> Cancel;
  public PXAction<POAccrualInquiryFilter> previousPeriod;
  public PXAction<POAccrualInquiryFilter> nextPeriod;
  public PXAction<POAccrualInquiryFilter> openReleaseINDocuments;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public virtual bool IsDirty => false;

  public POAccrualInquiry()
  {
    GraphHelper.Caches<POAccrualInquiryFilter>((PXGraph) this);
    ((PXSelectBase) this.ResultRecords).Cache.AllowInsert = false;
    ((PXSelectBase) this.ResultRecords).Cache.AllowUpdate = false;
    ((PXSelectBase) this.ResultRecords).Cache.AllowDelete = false;
    this.InitializeRecordsCache();
  }

  protected virtual IEnumerable filter()
  {
    PXCache cache = ((PXSelectBase) this.Filter).Cache;
    POAccrualInquiryFilter filter = cache.Current as POAccrualInquiryFilter;
    if (filter != null)
    {
      this.ClearSummary(filter);
      if (!this.IsEmptyFilter(filter))
        EnumerableExtensions.ForEach<POAccrualInquiryResult>((IEnumerable<POAccrualInquiryResult>) GraphHelper.RowCast<POAccrualInquiryResult>((IEnumerable) ((PXSelectBase<POAccrualInquiryResult>) this.ResultRecords).Select(Array.Empty<object>())).ToArray<POAccrualInquiryResult>(), (Action<POAccrualInquiryResult>) (record => this.AggregateSummary(filter, record)));
    }
    cache.IsDirty = false;
    yield return (object) filter;
  }

  protected virtual IEnumerable resultRecords() => (IEnumerable) this.RecordsCache.Value;

  [PXUIField]
  [PXEditDetailButton(ImageKey = "DataEntry")]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    POAccrualInquiryResult current = ((PXSelectBase<POAccrualInquiryResult>) this.ResultRecords).Current;
    if (current != null)
    {
      switch (current.DocumentType)
      {
        case "PR":
        case "RT":
          POReceiptEntry instance1 = PXGraph.CreateInstance<POReceiptEntry>();
          ((PXSelectBase<POReceipt>) instance1.Document).Current = PXResultset<POReceipt>.op_Implicit(((PXSelectBase<POReceipt>) instance1.Document).Search<POReceipt.receiptNbr>((object) current.POReceiptNbr, new object[1]
          {
            (object) current.POReceiptType
          }));
          PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, true, "Purchase Receipt");
          ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
          throw requiredException1;
        case "BL":
        case "DA":
          APInvoiceEntry instance2 = PXGraph.CreateInstance<APInvoiceEntry>();
          ((PXSelectBase<PX.Objects.AP.APInvoice>) instance2.Document).Current = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AP.APInvoice>) instance2.Document).Search<PX.Objects.AP.APInvoice.refNbr>((object) current.APRefNbr, new object[1]
          {
            (object) current.APDocType
          }));
          PXRedirectRequiredException requiredException2 = new PXRedirectRequiredException((PXGraph) instance2, true, "AP document");
          ((PXBaseRedirectException) requiredException2).Mode = (PXBaseRedirectException.WindowMode) 3;
          throw requiredException2;
      }
    }
    return (IEnumerable) adapter.Get<POAccrualInquiryFilter>();
  }

  [PXUIField(DisplayName = "", Visible = true)]
  [PXButton]
  public virtual IEnumerable RefreshAll(PXAdapter adapter)
  {
    ((PXSelectBase) this.Filter).Cache.Current = (object) PXResultset<POAccrualInquiryFilter>.op_Implicit(((PXSelectBase<POAccrualInquiryFilter>) this.Filter).Select(Array.Empty<object>()));
    ((PXSelectBase) this.ResultRecords).View.RequestRefresh();
    return adapter.Get();
  }

  [PXUIField]
  [PXPreviousButton]
  public virtual IEnumerable PreviousPeriod(PXAdapter adapter)
  {
    POAccrualInquiryFilter current = ((PXSelectBase<POAccrualInquiryFilter>) this.Filter).Current;
    FinPeriod prevPeriod = this.FinPeriodRepository.FindPrevPeriod(this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, new bool?(false)), current.FinPeriodID, true);
    if (prevPeriod != null)
    {
      current.FinPeriodID = prevPeriod.FinPeriodID;
      this.ResetCaches();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXNextButton]
  public virtual IEnumerable NextPeriod(PXAdapter adapter)
  {
    POAccrualInquiryFilter current = ((PXSelectBase<POAccrualInquiryFilter>) this.Filter).Current;
    FinPeriod nextPeriod = this.FinPeriodRepository.FindNextPeriod(this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, new bool?(false)), current.FinPeriodID, true);
    if (nextPeriod != null)
    {
      current.FinPeriodID = nextPeriod.FinPeriodID;
      this.ResetCaches();
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Unreleased IN Documents", Visible = true)]
  [PXButton]
  public virtual IEnumerable OpenReleaseINDocuments(PXAdapter adapter)
  {
    POAccrualInquiryFilter current = ((PXSelectBase<POAccrualInquiryFilter>) this.Filter).Current;
    PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<INDocumentRelease>(), true, "AP document");
    ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 2;
    PXRedirectRequiredException requiredException2 = requiredException1;
    Decimal? notAdjustedAmt = current.NotAdjustedAmt;
    Decimal num = 0M;
    if (!(notAdjustedAmt.GetValueOrDefault() == num & notAdjustedAmt.HasValue))
    {
      PXBaseRedirectException.Filter filter;
      // ISSUE: explicit constructor call
      ((PXBaseRedirectException.Filter) ref filter).\u002Ector("INDocumentList", new PXFilterRow[2]
      {
        new PXFilterRow("origModule", (PXCondition) 0, (object) "AP"),
        new PXFilterRow("docType", (PXCondition) 0, (object) "A")
      });
      ((PXBaseRedirectException) requiredException2).Filters.Add(filter);
    }
    throw requiredException2;
  }

  protected virtual bool IsEmptyFilter(POAccrualInquiryFilter filter)
  {
    int? nullable;
    int num;
    if (filter == null)
    {
      num = 1;
    }
    else
    {
      nullable = filter.OrgBAccountID;
      num = !nullable.HasValue ? 1 : 0;
    }
    if (num != 0 || filter.FinPeriodID == null)
      return true;
    nullable = filter.AcctID;
    return !nullable.HasValue;
  }

  protected virtual IEnumerable<POAccrualInquiryResult> LoadRecords()
  {
    POAccrualInquiryFilter current = ((PXSelectBase<POAccrualInquiryFilter>) this.Filter).Current;
    return this.IsEmptyFilter(current) ? (IEnumerable<POAccrualInquiryResult>) Array<POAccrualInquiryResult>.Empty : (IEnumerable<POAccrualInquiryResult>) this.GetQuery(current).SelectMain(Array.Empty<object>());
  }

  protected virtual PXSelectBase<POAccrualInquiryResult> GetQuery(POAccrualInquiryFilter filter)
  {
    return !filter.ShowByLines.GetValueOrDefault() ? (PXSelectBase<POAccrualInquiryResult>) new FbqlSelect<SelectFromBase<POAccrualInquiryResult, TypeArrayOf<IFbqlJoin>.Empty>.Aggregate<To<GroupBy<POAccrualInquiryResult.documentNoteID>, GroupBy<POAccrualInquiryResult.subID>, Sum<POAccrualInquiryResult.accruedCost>, Sum<POAccrualInquiryResult.pPVAmt>, Sum<POAccrualInquiryResult.accruedCostTotal>, Sum<POAccrualInquiryResult.taxAdjAmt>, Sum<POAccrualInquiryResult.accruedByReceiptsCost>, Sum<POAccrualInquiryResult.accruedByReceiptsPPVAmt>, Sum<POAccrualInquiryResult.accruedByReceiptsTotal>, Sum<POAccrualInquiryResult.accruedByBillsTotal>, Min<POAccrualInquiryResult.pPVAdjPosted>, Min<POAccrualInquiryResult.taxAdjPosted>>>, POAccrualInquiryResult>.View((PXGraph) this) : (PXSelectBase<POAccrualInquiryResult>) new FbqlSelect<SelectFromBase<POAccrualInquiryResult, TypeArrayOf<IFbqlJoin>.Empty>, POAccrualInquiryResult>.View((PXGraph) this);
  }

  protected virtual void InitializeRecordsCache()
  {
    this.RecordsCache = new Lazy<IEnumerable<POAccrualInquiryResult>>(new Func<IEnumerable<POAccrualInquiryResult>>(this.LoadRecords));
  }

  protected virtual void ClearSummary(POAccrualInquiryFilter filter)
  {
    filter.UnbilledAmt = new Decimal?(0M);
    filter.NotReceivedAmt = new Decimal?(0M);
    filter.NotInvoicedAmt = new Decimal?(0M);
    filter.NotAdjustedAmt = new Decimal?(0M);
    filter.Balance = new Decimal?(0M);
  }

  protected virtual void AggregateSummary(
    POAccrualInquiryFilter filter,
    POAccrualInquiryResult record)
  {
    POAccrualInquiryFilter accrualInquiryFilter1 = filter;
    Decimal? nullable1 = accrualInquiryFilter1.UnbilledAmt;
    Decimal? unbilledAmt = record.UnbilledAmt;
    accrualInquiryFilter1.UnbilledAmt = nullable1.HasValue & unbilledAmt.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + unbilledAmt.GetValueOrDefault()) : new Decimal?();
    POAccrualInquiryFilter accrualInquiryFilter2 = filter;
    Decimal? nullable2 = accrualInquiryFilter2.NotReceivedAmt;
    nullable1 = record.NotReceivedAmt;
    accrualInquiryFilter2.NotReceivedAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    POAccrualInquiryFilter accrualInquiryFilter3 = filter;
    nullable1 = accrualInquiryFilter3.NotInvoicedAmt;
    nullable2 = record.NotInvoicedAmt;
    accrualInquiryFilter3.NotInvoicedAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    POAccrualInquiryFilter accrualInquiryFilter4 = filter;
    nullable2 = accrualInquiryFilter4.NotAdjustedAmt;
    nullable1 = record.NotAdjustedAmt;
    accrualInquiryFilter4.NotAdjustedAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    POAccrualInquiryFilter accrualInquiryFilter5 = filter;
    nullable1 = accrualInquiryFilter5.Balance;
    nullable2 = record.AccrualAmt;
    accrualInquiryFilter5.Balance = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<POAccrualInquiryFilter.acctID> e)
  {
    PX.Objects.GL.Account[] array = GraphHelper.RowCast<PX.Objects.GL.Account>((IEnumerable) PXSelectBase<PX.Objects.GL.Account, PXViewOf<PX.Objects.GL.Account>.BasedOn<SelectFromBase<PX.Objects.GL.Account, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.Account.controlAccountModule, Equal<ControlAccountModule.pO>>>>>.And<Match<Current<AccessInfo.userName>>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) this, 0, 2, Array.Empty<object>())).ToArray<PX.Objects.GL.Account>();
    if (array.Length != 1)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POAccrualInquiryFilter.acctID>, object, object>) e).NewValue = (object) array[0].AccountID;
  }

  protected virtual void _(PX.Data.Events.RowInserted<POAccrualInquiryFilter> e)
  {
    this.ResetCaches();
  }

  protected virtual void _(PX.Data.Events.RowSelected<POAccrualInquiryFilter> e)
  {
    if (e.Row == null)
      return;
    bool showByLines = e.Row.ShowByLines.GetValueOrDefault();
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.ResultRecords).Cache, (object) null).For<POAccrualInquiryResult.siteID>((Action<PXUIFieldAttribute>) (a => a.Visible = showByLines));
    chained = chained.SameFor<POAccrualInquiryResult.inventoryID>();
    chained = chained.SameFor<POAccrualInquiryResult.tranDesc>();
    chained = chained.SameFor<POAccrualInquiryResult.notReceivedQty>();
    chained = chained.SameFor<POAccrualInquiryResult.orderQty>();
    chained.SameFor<POAccrualInquiryResult.unbilledQty>();
  }

  protected virtual void _(PX.Data.Events.RowUpdated<POAccrualInquiryFilter> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<POAccrualInquiryFilter>>) e).Cache.ObjectsEqual<POAccrualInquiryFilter.orgBAccountID, POAccrualInquiryFilter.vendorID, POAccrualInquiryFilter.finPeriodID, POAccrualInquiryFilter.acctID, POAccrualInquiryFilter.subCD, POAccrualInquiryFilter.showByLines>((object) e.OldRow, (object) e.Row))
      return;
    this.ResetCaches();
  }

  protected virtual void _(PX.Data.Events.RowSelected<POAccrualInquiryResult> e)
  {
    if (e.Row == null)
      return;
    PXCache cache1 = ((PXSelectBase) this.ResultRecords).Cache;
    POAccrualInquiryResult row1 = e.Row;
    string ppvAdjRefNbr = e.Row.PPVAdjRefNbr;
    bool? nullable;
    PXSetPropertyException propertyException1;
    if (e.Row.PPVAdjRefNbr != null)
    {
      nullable = e.Row.PPVAdjPosted;
      if (!nullable.GetValueOrDefault())
      {
        propertyException1 = new PXSetPropertyException("The {0} adjustment has not been released. To release the adjustment, use the Release IN Documents or Adjustments form.", (PXErrorLevel) 2, new object[1]
        {
          (object) e.Row.PPVAdjRefNbr
        });
        goto label_5;
      }
    }
    propertyException1 = (PXSetPropertyException) null;
label_5:
    cache1.RaiseExceptionHandling<POAccrualInquiryResult.pPVAdjRefNbr>((object) row1, (object) ppvAdjRefNbr, (Exception) propertyException1);
    PXCache cache2 = ((PXSelectBase) this.ResultRecords).Cache;
    POAccrualInquiryResult row2 = e.Row;
    string taxAdjRefNbr = e.Row.TaxAdjRefNbr;
    PXSetPropertyException propertyException2;
    if (e.Row.TaxAdjRefNbr != null)
    {
      nullable = e.Row.TaxAdjPosted;
      if (!nullable.GetValueOrDefault())
      {
        propertyException2 = new PXSetPropertyException("The {0} adjustment has not been released. To release the adjustment, use the Release IN Documents or Adjustments form.", (PXErrorLevel) 2, new object[1]
        {
          (object) e.Row.TaxAdjRefNbr
        });
        goto label_9;
      }
    }
    propertyException2 = (PXSetPropertyException) null;
label_9:
    cache2.RaiseExceptionHandling<POAccrualInquiryResult.taxAdjRefNbr>((object) row2, (object) taxAdjRefNbr, (Exception) propertyException2);
  }

  protected virtual void ResetCaches()
  {
    ((PXSelectBase) this.ResultRecords).Cache.Clear();
    ((PXSelectBase) this.ResultRecords).Cache.ClearQueryCache();
    this.InitializeRecordsCache();
  }

  [PXUIField(DisplayName = "UOM")]
  public class UOM : 
    PXFieldAttachedTo<POAccrualInquiryResult>.By<POAccrualInquiry>.AsString.Named<POAccrualInquiry.UOM>
  {
    public override string GetValue(POAccrualInquiryResult row)
    {
      return PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, row.InventoryID)?.BaseUnit;
    }

    protected override bool? Visible
    {
      get
      {
        POAccrualInquiryFilter current = ((PXSelectBase<POAccrualInquiryFilter>) this.Base.Filter).Current;
        return new bool?(current != null && current.ShowByLines.GetValueOrDefault());
      }
    }
  }
}
