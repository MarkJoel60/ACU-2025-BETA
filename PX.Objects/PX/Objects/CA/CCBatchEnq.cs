// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CCBatchEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Wrappers;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable enable
namespace PX.Objects.CA;

public class CCBatchEnq : PXGraph
{
  private const 
  #nullable disable
  string ProcessingCenterToken = "ProcessingCenterID";
  private const string NeedToWaitOnBatchToken = "NeedToWaitOnBatch";
  private const int LongRunUpdateSpan = 5000;
  public PXCancel<CCBatchEnq.BatchFilter> Cancel;
  public PXAction<CCBatchEnq.BatchFilter> importAndProcessBatches;
  public PXAction<CCBatchEnq.BatchFilter> ViewDocument;
  public PXFilter<CCBatchEnq.BatchFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<CCBatch, CCBatchEnq.BatchFilter, Where<CCBatch.processingCenterID, Equal<Current<CCBatchEnq.BatchFilter.processingCenterID>>>, OrderBy<Desc<CCBatch.settlementTimeUTC>>> Batches;

  [InjectDependency]
  private ILegacyCompanyService _legacyCompanyService { get; set; }

  [PXUIField]
  [PXProcessButton]
  protected IEnumerable ImportAndProcessBatches(PXAdapter a)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CCBatchEnq.\u003C\u003Ec__DisplayClass11_0 cDisplayClass110 = new CCBatchEnq.\u003C\u003Ec__DisplayClass11_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass110.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass110.uid = this.UID as Guid?;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass110.filter = ((PXSelectBase<CCBatchEnq.BatchFilter>) this.Filter).Current;
    // ISSUE: reference to a compiler-generated field
    this.CheckProcessRunning(cDisplayClass110.filter.ProcessingCenterID);
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass110, __methodptr(\u003CImportAndProcessBatches\u003Eb__0)));
    ((PXSelectBase) this.Batches).View.Cache.Clear();
    ((PXSelectBase) this.Batches).View.Cache.ClearQueryCache();
    return a.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewDocument(PXAdapter adapter)
  {
    CCBatch current = ((PXSelectBase<CCBatch>) this.Batches).Current;
    if (current != null)
    {
      CCBatchMaint instance = PXGraph.CreateInstance<CCBatchMaint>();
      ((PXSelectBase<CCBatch>) instance.BatchView).Current = PXResultset<CCBatch>.op_Implicit(((PXSelectBase<CCBatch>) instance.BatchView).Search<CCBatch.batchID>((object) current.BatchID, Array.Empty<object>()));
      if (((PXSelectBase<CCBatch>) instance.BatchView).Current != null)
        PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 4);
    }
    return adapter.Get();
  }

  internal void CheckProcessRunning(string filterValue)
  {
    string str = this.Accessinfo.ScreenID.Replace(".", "");
    string companyName = PXAccess.GetCompanyName();
    foreach (RowTaskInfo rowTaskInfo in PXLongOperation.GetTaskList().Where<RowTaskInfo>((Func<RowTaskInfo, bool>) (i => PXLongOperation.GetStatus(i.NativeKey) == 1)))
    {
      if (str == rowTaskInfo.Screen?.Replace(".", "") && companyName == LegacyCompanyServiceExtensions.ExtractCompany(this._legacyCompanyService, rowTaskInfo.User) && filterValue == PXLongOperation.GetCustomInfo(rowTaskInfo.NativeKey, "ProcessingCenterID") as string)
        throw new PXException("Import of Settlement batches for this processing center is already in progress. Please wait for the import process to finish.");
    }
  }

  protected virtual IEnumerable filter()
  {
    CCBatchEnq ccBatchEnq = this;
    PXCache cache = ((PXSelectBase) ccBatchEnq.Filter).Cache;
    if (cache != null && cache.Current is CCBatchEnq.BatchFilter current && current.ProcessingCenterID == null)
    {
      PXSelect<CCProcessingCenter, Where<CCProcessingCenter.isActive, Equal<boolTrue>>> pxSelect = new PXSelect<CCProcessingCenter, Where<CCProcessingCenter.isActive, Equal<boolTrue>>>((PXGraph) ccBatchEnq);
      if (((PXSelectBase<CCProcessingCenter>) pxSelect).Select(Array.Empty<object>()).Count == 1)
      {
        string processingCenterId = ((PXSelectBase<CCProcessingCenter>) pxSelect).SelectSingle(Array.Empty<object>()).ProcessingCenterID;
        cache.SetValueExt<CCBatchEnq.BatchFilter.processingCenterID>((object) current, (object) processingCenterId);
      }
    }
    yield return ((PXSelectBase) ccBatchEnq.Filter).Cache.Current;
    ((PXSelectBase) ccBatchEnq.Filter).Cache.IsDirty = false;
  }

  protected void _(
    PX.Data.Events.FieldUpdated<CCBatchEnq.BatchFilter.processingCenterID> e)
  {
    CCBatchEnq.BatchFilter row = (CCBatchEnq.BatchFilter) e.Row;
    if (row == null)
      return;
    this.SetDefaultImportDates(row);
  }

  private void SetDefaultImportDates(CCBatchEnq.BatchFilter filter)
  {
    CCProcessingCenter processingCenter = CCProcessingCenter.PK.Find((PXGraph) this, filter.ProcessingCenterID);
    filter.LastSettlementDateUTC = (DateTime?) processingCenter?.LastSettlementDateUTC;
    filter.FirstImportDateUTC = CCBatchEnq.MaxDate(filter.LastSettlementDateUTC, (DateTime?) processingCenter?.ImportStartDate);
    DateTime? dateA = CCBatchEnq.ShiftDateByDefaultImportPeriod(filter.FirstImportDateUTC);
    filter.ImportThroughDateUTC = CCBatchEnq.MinDate(dateA, new DateTime?(DateTime.UtcNow));
  }

  protected void _(PX.Data.Events.RowSelected<CCBatchEnq.BatchFilter> e)
  {
    if (e.Row == null)
      return;
    ((PXAction) this.importAndProcessBatches).SetEnabled(!string.IsNullOrEmpty(e.Row.ProcessingCenterID));
  }

  public CCBatchEnq()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CCBatchEnq.\u003C\u003Ec__DisplayClass21_0 cDisplayClass210 = new CCBatchEnq.\u003C\u003Ec__DisplayClass21_0();
    ((PXProcessingBase<CCBatch>) this.Batches).SetSelected<CCBatch.selected>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass210.filter = ((PXSelectBase<CCBatchEnq.BatchFilter>) this.Filter).Current;
    // ISSUE: method pointer
    ((PXProcessingBase<CCBatch>) this.Batches).SetProcessDelegate(new PXProcessingBase<CCBatch>.ProcessListDelegate((object) cDisplayClass210, __methodptr(\u003C\u002Ector\u003Eb__0)));
    ((PXProcessing<CCBatch>) this.Batches).SetProcessCaption("Import Transactions");
    ((PXProcessing<CCBatch>) this.Batches).SetProcessAllCaption("Import All Transactions");
    ((PXProcessing<CCBatch>) this.Batches).SetProcessAllVisible(false);
    ((PXProcessing<CCBatch>) this.Batches).SetProcessVisible(false);
  }

  private static PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing CreatePaymentProcessing(
    PXGraph contextGraph)
  {
    return new PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing(contextGraph);
  }

  private IEnumerable DoImportBatches(PXAdapter adapter, CCBatchEnq.BatchFilter filter)
  {
    string processingCenterId = filter.ProcessingCenterID;
    CCProcessingCenter processingCenter = CCProcessingCenter.PK.Find((PXGraph) this, processingCenterId);
    bool? nullable1;
    int num1;
    if (processingCenter == null)
    {
      num1 = 1;
    }
    else
    {
      nullable1 = processingCenter.IsActive;
      num1 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    if (num1 == 0)
    {
      int num2;
      if (processingCenter == null)
      {
        num2 = 1;
      }
      else
      {
        nullable1 = processingCenter.ImportSettlementBatches;
        num2 = !nullable1.GetValueOrDefault() ? 1 : 0;
      }
      if (num2 == 0)
      {
        DateTime utcNow = DateTime.UtcNow;
        DateTime? nullable2 = filter.FirstImportDateUTC;
        nullable2 = nullable2.HasValue ? new DateTime?(DateTime.SpecifyKind(nullable2.Value, DateTimeKind.Utc)) : throw new PXException("Settlement batches cannot be imported for the {0} processing center. On the Processing Center (CA205000) form, specify the Import Start Date for the processing center.", new object[1]
        {
          (object) processingCenterId
        });
        PXTimeZoneInfo timeZone = LocaleInfo.GetTimeZone();
        DateTime dateTime = PXTimeZoneInfo.ConvertTimeFromUtc(filter.ImportThroughDateUTC ?? utcNow, timeZone).Date;
        dateTime = dateTime.AddDays(1.0);
        DateTime lastImportDate = DateTime.SpecifyKind(PXTimeZoneInfo.ConvertTimeToUtc(dateTime.AddSeconds(-1.0), timeZone), DateTimeKind.Utc);
        this.ImportBatchesForPeriod(processingCenter, nullable2.Value, lastImportDate);
        ((PXSelectBase) this.Batches).View.Cache.Clear();
        ((PXSelectBase) this.Batches).View.Cache.ClearQueryCache();
        return adapter.Get();
      }
    }
    throw new PXException("Settlement batches cannot be imported for the {0} processing center. On the Processing Center (CA205000) form, make sure the processing center is active and the Import Settlement Batches check box is selected.", new object[1]
    {
      (object) processingCenterId
    });
  }

  private bool ImportBatchesForPeriod(
    CCProcessingCenter processingCenter,
    DateTime firstImportDate,
    DateTime lastImportDate)
  {
    if (processingCenter == null)
      return false;
    bool flag = false;
    CCBatchMaint instance = PXGraph.CreateInstance<CCBatchMaint>();
    string processingCenterId = processingCenter.ProcessingCenterID;
    foreach (BatchData batchData in this.GetBatches(processingCenterId, firstImportDate, lastImportDate).ToList<BatchData>())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        if (this.GetCCBatchByExtBatchId((PXGraph) instance, processingCenterId, batchData.BatchId, batchData.BatchType) == null)
        {
          CCBatch ccBatch1 = CCBatchEnq.TransformSettledBatch(processingCenter, batchData);
          CCBatch ccBatch2 = ((PXSelectBase<CCBatch>) instance.BatchView).Insert(ccBatch1);
          if (batchData.Statistics != null)
          {
            foreach (BatchStatisticsData statistic in batchData.Statistics)
            {
              CCBatchStatistics ccBatchStatistics = CCBatchEnq.TransformBatchStatistics(ccBatch2.BatchID, statistic);
              ((PXSelectBase<CCBatchStatistics>) instance.CardTypeSummary).Insert(ccBatchStatistics);
            }
          }
          flag = true;
          ((PXGraph) instance).Actions.PressSave();
          ((PXGraph) instance).Clear();
          transactionScope.Complete();
        }
      }
    }
    return flag;
  }

  private IEnumerable<BatchData> GetBatches(
    string processingCenterID,
    DateTime firstSettlementDate,
    DateTime lastSettlementDate)
  {
    return new CCBatchEnq.DateRange(firstSettlementDate, lastSettlementDate).Split(31 /*0x1F*/).SelectMany<CCBatchEnq.DateRange, BatchData>((Func<CCBatchEnq.DateRange, IEnumerable<BatchData>>) (dateRange => this.GetBatchesForSinglePeriod(processingCenterID, dateRange.StartDate, dateRange.EndDate)));
  }

  private IEnumerable<BatchData> GetBatchesForSinglePeriod(
    string processingCenterID,
    DateTime firstSettlementDate,
    DateTime lastSettlementDate)
  {
    return CCBatchEnq.CreatePaymentProcessing((PXGraph) this).GetSettledBatches(processingCenterID, new BatchSearchParams()
    {
      FirstSettlementDate = firstSettlementDate,
      LastSettlementDate = lastSettlementDate,
      IncludeStatistics = true
    });
  }

  private static DateTime? ShiftDateByDefaultImportPeriod(DateTime? firstDate)
  {
    return firstDate?.AddDays(7.0);
  }

  private static DateTime? MinDate(DateTime? dateA, DateTime? dateB)
  {
    if (dateB.HasValue)
    {
      DateTime? nullable1 = dateA;
      DateTime? nullable2 = dateB;
      if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return dateB;
    }
    return dateA;
  }

  private static DateTime? MaxDate(DateTime? dateA, DateTime? dateB)
  {
    if (dateB.HasValue)
    {
      DateTime? nullable1 = dateA;
      DateTime? nullable2 = dateB;
      if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return dateB;
    }
    return dateA;
  }

  private static void ImportTransactions(
    IEnumerable<CCBatch> batches,
    CCBatchEnq.BatchFilter filter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CCBatchEnq.\u003C\u003Ec__DisplayClass30_0 cDisplayClass300 = new CCBatchEnq.\u003C\u003Ec__DisplayClass30_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass300.filter = filter;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass300.batches = batches;
    if (PXContext.GetSlot<bool>("ScheduleIsRunning"))
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass300.enqGraph = PXGraph.CreateInstance<CCBatchEnq>();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) cDisplayClass300.enqGraph, new PXToggleAsyncDelegate((object) cDisplayClass300, __methodptr(\u003CImportTransactions\u003Eb__0)));
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      CCBatchEnq.DoImportTransactions(cDisplayClass300.batches);
    }
  }

  private static void DoImportTransactions(IEnumerable<CCBatch> batches)
  {
    bool flag = false;
    CCBatchMaint instance = PXGraph.CreateInstance<CCBatchMaint>();
    foreach (CCBatch batch in (IEnumerable<CCBatch>) batches.OrderBy<CCBatch, DateTime?>((Func<CCBatch, DateTime?>) (batch => batch.SettlementTimeUTC)))
    {
      PXProcessing.SetCurrentItem((object) batch);
      ((PXSelectBase<CCBatch>) instance.BatchView).Current = batch;
      if (batch.Status == "PIM" && !CCBatchEnq.TryImportTransactionsFromProcCenter(instance))
      {
        flag = true;
      }
      else
      {
        if (NonGenericIEnumerableExtensions.Empty_((IEnumerable) PXSelectBase<CCBatchStatistics, PXSelect<CCBatchStatistics, Where<CCBatchStatistics.batchID, Equal<Required<CCBatch.batchID>>>>.Config>.Select((PXGraph) instance, new object[1]
        {
          (object) batch.BatchID
        })))
          CCBatchEnq.CalculateStatistics(instance, batch);
        if (CCBatchEnq.TryProcessImportedTransactions(instance))
          PXProcessing.SetProcessed();
        else
          flag = true;
        int slot = PXContext.GetSlot<int>("NeedToWaitOnBatch");
        int? batchId = batch.BatchID;
        int valueOrDefault = batchId.GetValueOrDefault();
        if (slot == valueOrDefault & batchId.HasValue)
          Thread.Sleep(5000);
      }
    }
    if (flag)
      throw new PXOperationCompletedWithErrorException("At least one item has not been processed.");
  }

  private static bool TryImportTransactionsFromProcCenter(CCBatchMaint graph)
  {
    try
    {
      CCBatchEnq.SetBatchStatus(graph, "PRG");
      CCBatch current = ((PXSelectBase<CCBatch>) graph.BatchView).Current;
      int num = CCProcessingFeatureHelper.IsFeatureSupported(CCProcessingCenter.PK.Find((PXGraph) graph, current.ProcessingCenterID), CCProcessingFeature.TransactionGetter, false) ? 1 : 0;
      BatchType valueOrDefault = CCBatchType.GetType(current.BatchType).GetValueOrDefault();
      PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing paymentProcessing = CCBatchEnq.CreatePaymentProcessing((PXGraph) graph);
      IEnumerable<TransactionData> transactionDatas = num != 0 ? paymentProcessing.GetTransactionsByTypedBatch(current.ExtBatchID, current.ProcessingCenterID, valueOrDefault) : paymentProcessing.GetTransactionsByBatch(current.ExtBatchID, current.ProcessingCenterID);
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        foreach (TransactionData tranData in transactionDatas)
        {
          if (CCBatchTransaction.PK.Find((PXGraph) graph, current.BatchID, tranData.TranID) == null)
          {
            CCBatchTransaction batchTransaction = CCBatchEnq.TransformBatchTransaction(current.BatchID, tranData);
            ((PXSelectBase<CCBatchTransaction>) graph.Transactions).Insert(batchTransaction);
          }
        }
        ((PXGraph) graph).Actions.PressSave();
        transactionScope.Complete();
      }
      return true;
    }
    catch (Exception ex)
    {
      CCBatchEnq.SetBatchStatus(graph, "PIM");
      PXProcessing.SetError(ex);
      return false;
    }
  }

  private static bool TryProcessImportedTransactions(CCBatchMaint graph)
  {
    try
    {
      CCBatchEnq.SetBatchStatus(graph, "PRG");
      if (graph.MatchTransactions())
      {
        CCBatchEnq.SetBatchStatus(graph, "PRD");
        CCBatch current = ((PXSelectBase<CCBatch>) graph.BatchView).Current;
        CCProcessingCenter processingCenter = ((PXSelectBase<CCProcessingCenter>) graph.ProcessingCenter).SelectSingle(Array.Empty<object>());
        bool? nullable;
        int num;
        if (processingCenter == null)
        {
          num = 0;
        }
        else
        {
          nullable = processingCenter.AutoCreateBankDeposit;
          num = nullable.GetValueOrDefault() ? 1 : 0;
        }
        if (num == 0)
        {
          nullable = current.SkipDepositAutoCreation;
          if (nullable.GetValueOrDefault() || !(current.Status == "PRD"))
            goto label_8;
        }
        return CCBatchEnq.TryCreateDeposit(graph);
      }
      CCBatchEnq.SetBatchStatus(graph, "PRV");
label_8:
      return true;
    }
    catch (Exception ex)
    {
      CCBatchEnq.SetBatchStatus(graph, "PPR");
      PXProcessing.SetError(ex);
      return false;
    }
  }

  private static bool TryCreateDeposit(CCBatchMaint graph)
  {
    try
    {
      PXAdapter pxAdapter = new PXAdapter((PXSelectBase) graph.BatchView, Array.Empty<object>())
      {
        MassProcess = true
      };
      GraphHelper.PressButton((PXAction) graph.createDeposit, pxAdapter);
      return true;
    }
    catch (Exception ex)
    {
      CCBatchEnq.SetBatchStatus(graph, "PRD");
      PXProcessing.SetError(ex);
      return false;
    }
  }

  private CCBatch GetCCBatchByExtBatchId(
    PXGraph graph,
    string processinCenterID,
    string extBatchID,
    BatchType batchType)
  {
    string code = CCBatchType.GetCode(batchType);
    return PXResultset<CCBatch>.op_Implicit(PXSelectBase<CCBatch, PXSelect<CCBatch, Where<CCBatch.processingCenterID, Equal<Required<CCBatch.processingCenterID>>, And<CCBatch.extBatchID, Equal<Required<CCBatch.extBatchID>>, And<CCBatch.batchType, Equal<Required<CCBatch.batchType>>>>>>.Config>.Select(graph, new object[3]
    {
      (object) processinCenterID,
      (object) extBatchID,
      (object) code
    }));
  }

  private static PXResultset<CCBatch> SelectBatches(
    PXGraph graph,
    string processingCenterID,
    params string[] statuses)
  {
    return PXSelectBase<CCBatch, PXSelect<CCBatch, Where<CCBatch.processingCenterID, Equal<Required<CCBatch.processingCenterID>>, And<CCBatch.status, In<Required<CCBatch.status>>>>, OrderBy<Asc<CCBatch.settlementTimeUTC>>>.Config>.Select(graph, new object[2]
    {
      (object) processingCenterID,
      (object) statuses
    });
  }

  private static PXResultset<CCBatch> SelectBatchesToDeposit(
    PXGraph graph,
    string processingCenterID)
  {
    return PXSelectBase<CCBatch, PXSelect<CCBatch, Where<CCBatch.processingCenterID, Equal<Required<CCBatch.processingCenterID>>, And<CCBatch.status, Equal<Required<CCBatch.status>>, And<CCBatch.skipDepositAutoCreation, Equal<False>>>>, OrderBy<Asc<CCBatch.settlementTimeUTC>>>.Config>.Select(graph, new object[2]
    {
      (object) processingCenterID,
      (object) "PRD"
    });
  }

  private static void SetBatchStatus(CCBatchMaint graph, string newStatus)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      CCBatch current = ((PXSelectBase<CCBatch>) graph.BatchView).Current;
      if (current == null)
        return;
      current.Status = newStatus;
      ((PXSelectBase<CCBatch>) graph.BatchView).Update(current);
      ((PXGraph) graph).Actions.PressSave();
      transactionScope.Complete();
    }
  }

  private static void CalculateStatistics(CCBatchMaint graph, CCBatch batch)
  {
    List<CCBatchStatistics> batchStatistics = CCBatchEnq.ConvertBatchTransactionsToBatchStatistics(PXSelectBase<CCBatchTransaction, PXSelectGroupBy<CCBatchTransaction, Where<CCBatchTransaction.batchID, Equal<Required<CCBatch.batchID>>>, Aggregate<GroupBy<CCBatchTransaction.procCenterCardTypeCode, GroupBy<CCBatchTransaction.settlementStatus, Sum<CCBatchTransaction.amount, Count<CCBatchTransaction.pCTranNumber>>>>>>.Config>.Select((PXGraph) graph, new object[1]
    {
      (object) batch.BatchID
    }));
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (CCBatchStatistics ccBatchStatistics in batchStatistics)
        ((PXSelectBase<CCBatchStatistics>) graph.CardTypeSummary).Insert(ccBatchStatistics);
      ((PXGraph) graph).Actions.PressSave();
      transactionScope.Complete();
    }
  }

  private static List<CCBatchStatistics> ConvertBatchTransactionsToBatchStatistics(
    PXResultset<CCBatchTransaction> batchTransactions)
  {
    List<CCBatchStatistics> source = new List<CCBatchStatistics>();
    foreach (PXResult<CCBatchTransaction> batchTransaction1 in batchTransactions)
    {
      CCBatchTransaction batchTransaction = PXResult<CCBatchTransaction>.op_Implicit(batchTransaction1);
      CCBatchStatistics batchStatistic1 = source.FirstOrDefault<CCBatchStatistics>((Func<CCBatchStatistics, bool>) (item => item.ProcCenterCardTypeCode == batchTransaction.ProcCenterCardTypeCode));
      if (batchStatistic1 == null)
      {
        CCBatchStatistics batchStatistic2 = new CCBatchStatistics()
        {
          BatchID = batchTransaction.BatchID,
          ProcCenterCardTypeCode = batchTransaction.ProcCenterCardTypeCode,
          CardTypeCode = batchTransaction.CardTypeCode,
          SettledAmount = new Decimal?(0M),
          SettledCount = new int?(0),
          RefundAmount = new Decimal?(0M),
          RefundCount = new int?(0),
          RejectedAmount = new Decimal?(0M),
          RejectedCount = new int?(0),
          VoidCount = new int?(0),
          DeclineCount = new int?(0),
          ErrorCount = new int?(0)
        };
        CCBatchEnq.SetCCBatchStatistics(batchTransaction1, batchTransaction, batchStatistic2);
        source.Add(batchStatistic2);
      }
      else
        CCBatchEnq.SetCCBatchStatistics(batchTransaction1, batchTransaction, batchStatistic1);
    }
    return source;
  }

  private static void SetCCBatchStatistics(
    PXResult<CCBatchTransaction> result,
    CCBatchTransaction batchTransaction,
    CCBatchStatistics batchStatistic)
  {
    string settlementStatus = batchTransaction.SettlementStatus;
    if (settlementStatus == null || settlementStatus.Length != 3)
      return;
    switch (settlementStatus[1])
    {
      case 'E':
        switch (settlementStatus)
        {
          case "DEC":
            batchStatistic.DeclineCount = ((PXResult) result).RowCount;
            return;
          case "REJ":
            goto label_17;
          default:
            return;
        }
      case 'O':
        if (!(settlementStatus == "VOI"))
          return;
        break;
      case 'P':
        return;
      case 'Q':
        return;
      case 'R':
        if (!(settlementStatus == "RRJ"))
          return;
        goto label_17;
      case 'S':
        switch (settlementStatus)
        {
          case "SSC":
            batchStatistic.SettledAmount = batchTransaction.Amount;
            batchStatistic.SettledCount = ((PXResult) result).RowCount;
            return;
          case "RSS":
            batchStatistic.RefundAmount = batchTransaction.Amount;
            batchStatistic.RefundCount = ((PXResult) result).RowCount;
            return;
          default:
            return;
        }
      case 'V':
        if (!(settlementStatus == "RVO"))
          return;
        break;
      default:
        return;
    }
    batchStatistic.VoidCount = ((PXResult) result).RowCount;
    return;
label_17:
    batchStatistic.RejectedAmount = batchTransaction.Amount;
    batchStatistic.RejectedCount = ((PXResult) result).RowCount;
  }

  private static CCBatch TransformSettledBatch(
    CCProcessingCenter processingCenter,
    BatchData batchData)
  {
    return new CCBatch()
    {
      ExtBatchID = batchData.BatchId,
      Status = "PIM",
      ProcessingCenterID = processingCenter?.ProcessingCenterID,
      SettlementTimeUTC = new DateTime?(batchData.SettlementTimeUTC),
      SettlementState = CCBatchSettlementState.GetCode(batchData.SettlementState),
      SkipDepositAutoCreation = new bool?(processingCenter == null || !processingCenter.AutoCreateBankDeposit.GetValueOrDefault()),
      BatchType = CCBatchType.GetCode(batchData.BatchType)
    };
  }

  private static CCBatchStatistics TransformBatchStatistics(
    int? batchID,
    BatchStatisticsData statData)
  {
    return new CCBatchStatistics()
    {
      BatchID = batchID,
      ProcCenterCardTypeCode = statData.ProcCenterCardType,
      CardTypeCode = CCBatchEnq.GetCardTypeCode(statData.CardType),
      DeclineCount = new int?(statData.DeclineCount),
      ErrorCount = new int?(statData.ErrorCount),
      RefundAmount = new Decimal?(statData.RefundAmount),
      RefundCount = new int?(statData.RefundCount),
      RejectedAmount = new Decimal?(statData.RejectedAmount),
      RejectedCount = new int?(statData.RejectedCount),
      SettledAmount = new Decimal?(statData.SettledAmount),
      SettledCount = new int?(statData.SettledCount),
      VoidCount = new int?(statData.VoidCount)
    };
  }

  private static CCBatchTransaction TransformBatchTransaction(
    int? batchID,
    TransactionData tranData)
  {
    return new CCBatchTransaction()
    {
      BatchID = batchID,
      PCTranNumber = tranData.TranID,
      PCCustomerID = tranData.CustomerId,
      PCPaymentProfileID = tranData.PaymentId,
      SettlementStatus = CCBatchTranSettlementStatusCode.GetCode(tranData.TranStatus),
      InvoiceNbr = tranData.DocNum,
      SubmitTime = new DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(tranData.SubmitTime, LocaleInfo.GetTimeZone())),
      ProcCenterCardTypeCode = tranData.CardType,
      CardTypeCode = CCBatchEnq.GetCardTypeCode(tranData.CardTypeCode),
      AccountNumber = string.IsNullOrEmpty(tranData.AccountNumber) ? tranData.CardNumber : tranData.AccountNumber,
      Amount = new Decimal?(tranData.Amount),
      FixedFee = new Decimal?(tranData.FixedFee),
      PercentageFee = new Decimal?(tranData.PercentageFee),
      FeeType = tranData.FeeType,
      ProcessingStatus = "PPR"
    };
  }

  private static string GetCardTypeCode(CCCardType ccCardType)
  {
    return CardType.GetCardTypeCode(V2Converter.ConvertCardType(ccCardType));
  }

  [PXHidden]
  [Serializable]
  public class BatchFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDefault]
    [PXDBString(10, IsUnicode = true)]
    [PXSelector(typeof (Search<CCProcessingCenter.processingCenterID>))]
    [PXUIField(DisplayName = "Proc. Center ID")]
    public virtual string ProcessingCenterID { get; set; }

    [PXDate(UseTimeZone = true)]
    [PXUIField(DisplayName = "Last Settlement Date UTC")]
    public virtual DateTime? LastSettlementDateUTC { get; set; }

    [PXDate(UseTimeZone = true)]
    [PXUIField(DisplayName = "Last Settlement Date")]
    public virtual DateTime? LastSettlementDate
    {
      get
      {
        return !this.LastSettlementDateUTC.HasValue ? new DateTime?() : new DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(this.LastSettlementDateUTC.Value, LocaleInfo.GetTimeZone()));
      }
    }

    [PXDate(UseTimeZone = true)]
    [PXUIField(DisplayName = "First Import Date")]
    public virtual DateTime? FirstImportDateUTC { get; set; }

    [PXDate(UseTimeZone = true)]
    [PXUIField(DisplayName = "Import Batches Through UTC")]
    public virtual DateTime? ImportThroughDateUTC { get; set; }

    [PXDate(UseTimeZone = true)]
    [PXUIField(DisplayName = "Import Batches Through")]
    public virtual DateTime? ImportThroughDate
    {
      get
      {
        return !this.ImportThroughDateUTC.HasValue ? new DateTime?() : new DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(this.ImportThroughDateUTC.Value, LocaleInfo.GetTimeZone()));
      }
      set
      {
        this.ImportThroughDateUTC = value.HasValue ? new DateTime?(PXTimeZoneInfo.ConvertTimeToUtc(value.Value, LocaleInfo.GetTimeZone())) : new DateTime?();
      }
    }

    public abstract class processingCenterID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CCBatchEnq.BatchFilter.processingCenterID>
    {
    }

    public abstract class lastSettlementDateUTC : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CCBatchEnq.BatchFilter.lastSettlementDateUTC>
    {
    }

    public abstract class lastSettlementDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CCBatchEnq.BatchFilter.lastSettlementDate>
    {
    }

    public abstract class firstImportDateUTC : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CCBatchEnq.BatchFilter.firstImportDateUTC>
    {
    }

    public abstract class importThroughDateUTC : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CCBatchEnq.BatchFilter.importThroughDateUTC>
    {
    }

    public abstract class importThroughDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CCBatchEnq.BatchFilter.importThroughDate>
    {
    }
  }

  public struct DateRange(DateTime startDate, DateTime endDate)
  {
    public DateTime StartDate { get; set; } = startDate;

    public DateTime EndDate { get; set; } = endDate;

    public IEnumerable<CCBatchEnq.DateRange> Split(int intervalDays)
    {
      if (!(this.StartDate > this.EndDate) && intervalDays > 0)
      {
        DateTime startDate = this.StartDate;
        while (true)
        {
          DateTime partEnd = startDate.AddDays((double) intervalDays);
          if (!(partEnd >= this.EndDate))
          {
            yield return new CCBatchEnq.DateRange(startDate, partEnd);
            startDate = partEnd.AddSeconds(1.0);
          }
          else
            break;
        }
        if (startDate <= this.EndDate)
          yield return new CCBatchEnq.DateRange(startDate, this.EndDate);
      }
    }
  }
}
