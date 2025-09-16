// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.CreateInvoiceBase`2
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.FS.DAC;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

#nullable disable
namespace PX.Objects.FS;

public abstract class CreateInvoiceBase<TGraph, TPostLine> : PXGraph<TGraph>, IInvoiceProcessGraph
  where TGraph : PXGraph
  where TPostLine : class, IBqlTable, IPostLine, new()
{
  protected StringBuilder groupKey;
  protected string billingBy;
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;
  public PXFilter<CreateInvoiceFilter> Filter;
  public PXCancel<CreateInvoiceFilter> Cancel;
  public PXAction<CreateInvoiceFilter> filterManually;
  public PXAction<CreateInvoiceFilter> openReviewTemporaryBatch;

  [PXUIField(DisplayName = "Apply Filters")]
  public virtual IEnumerable FilterManually(PXAdapter adapter)
  {
    ((PXSelectBase<CreateInvoiceFilter>) this.Filter).Current.LoadData = new bool?(true);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(VisibleOnProcessingResults = true)]
  public virtual void OpenReviewTemporaryBatch()
  {
    PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<ReviewInvoiceBatches>(), (PXRedirectHelper.WindowMode) 3);
  }

  public CreateInvoiceBase() => this.IncludeReviewInvoiceBatchesAction();

  public OnDocumentHeaderInsertedDelegate OnDocumentHeaderInserted { get; set; }

  public OnTransactionInsertedDelegate OnTransactionInserted { get; set; }

  public BeforeSaveDelegate BeforeSave { get; set; }

  public AfterCreateInvoiceDelegate AfterCreateInvoice { get; set; }

  public PXGraph GetGraph() => (PXGraph) this;

  protected virtual void _(PX.Data.Events.RowSelected<CreateInvoiceFilter> e)
  {
    if (e.Row == null)
      return;
    CreateInvoiceFilter row1 = e.Row;
    bool? nullable;
    if (((PXSelectBase<FSSetup>) this.SetupRecord).Current != null)
    {
      PXAction<CreateInvoiceFilter> filterManually = this.filterManually;
      nullable = ((PXSelectBase<FSSetup>) this.SetupRecord).Current.FilterInvoicingManually;
      int num = nullable.GetValueOrDefault() ? 1 : 0;
      ((PXAction) filterManually).SetVisible(num != 0);
    }
    this.HideOrShowInvoiceActions(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CreateInvoiceFilter>>) e).Cache, row1);
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CreateInvoiceFilter>>) e).Cache;
    CreateInvoiceFilter row2 = e.Row;
    nullable = e.Row.IgnoreBillingCycles;
    int num1 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<CreateInvoiceFilter.fromDate>(cache, (object) row2, num1 != 0);
    FSPostTo.SetLineTypeList<CreateInvoiceFilter.postTo>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CreateInvoiceFilter>>) e).Cache, (object) e.Row, true, showProjects: true, showAPAR: true);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CreateInvoiceFilter> e)
  {
    if (e.Row == null)
      return;
    ((PXSelectBase<CreateInvoiceFilter>) this.Filter).Current.LoadData = new bool?(false);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CreateInvoiceFilter, CreateInvoiceFilter.fromDate> e)
  {
    DateTime dateTime = ((PXGraph) this).Accessinfo.BusinessDate ?? DateTime.Now;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CreateInvoiceFilter, CreateInvoiceFilter.fromDate>, CreateInvoiceFilter, object>) e).NewValue = (object) new DateTime(dateTime.Year, dateTime.Month, 1);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CreateInvoiceFilter, CreateInvoiceFilter.upToDate> e)
  {
    DateTime dateTime1 = ((PXGraph) this).Accessinfo.BusinessDate ?? DateTime.Now;
    DateTime dateTime2 = new DateTime(dateTime1.Year, dateTime1.Month, 1).AddMonths(1).AddDays(-1.0);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CreateInvoiceFilter, CreateInvoiceFilter.upToDate>, CreateInvoiceFilter, object>) e).NewValue = (object) dateTime2;
  }

  public virtual Guid CreateInvoices(
    CreateInvoiceBase<TGraph, TPostLine> processGraph,
    List<TPostLine> postLineRows,
    CreateInvoiceFilter filter,
    PXQuickProcess.ActionFlow quickProcessFlow,
    bool isGenerateInvoiceScreen)
  {
    PXTrace.WriteInformation("Data preparation started.");
    Guid fromUserSelection = processGraph.CreatePostDocsFromUserSelection(postLineRows);
    foreach (PXResult<FSPostDoc> pxResult in PXSelectBase<FSPostDoc, PXSelectGroupBy<FSPostDoc, Where<FSPostDoc.processID, Equal<Required<FSPostDoc.processID>>>, Aggregate<GroupBy<FSPostDoc.billingCycleID>>, OrderBy<Asc<FSPostDoc.billingCycleID>>>.Config>.Select((PXGraph) processGraph, new object[1]
    {
      (object) fromUserSelection
    }))
    {
      FSPostDoc fsPostDoc = PXResult<FSPostDoc>.op_Implicit(pxResult);
      if (filter.SOQuickProcess.GetValueOrDefault() && quickProcessFlow == null)
        quickProcessFlow = (PXQuickProcess.ActionFlow) 2;
      PXTrace.WriteInformation("Invoice generation started.");
      processGraph.CreatePostingBatchForBillingCycle(fromUserSelection, fsPostDoc.BillingCycleID.Value, filter, postLineRows, quickProcessFlow, isGenerateInvoiceScreen);
      PXTrace.WriteInformation("Invoice generation completed.");
    }
    PXTrace.WriteInformation("Data preparation completed.");
    PXTrace.WriteInformation("Clean of unprocessed documents started.");
    processGraph.DeletePostDocsWithError(fromUserSelection);
    PXTrace.WriteInformation("Clean of unprocessed documents completed.");
    PXTrace.WriteInformation("External tax calculation started.");
    processGraph.CalculateExternalTaxes(fromUserSelection);
    PXTrace.WriteInformation("External tax calculation completed.");
    this.ApplyInvoiceActions(processGraph.GetGraph(), filter, fromUserSelection);
    return fromUserSelection;
  }

  public virtual void ApplyInvoiceActions(
    PXGraph graph,
    CreateInvoiceFilter filter,
    Guid currentProcessID)
  {
    switch (filter.PostTo)
    {
      case "SO":
        bool? nullable = filter.EmailSalesOrder;
        if (!nullable.GetValueOrDefault())
        {
          nullable = filter.PrepareInvoice;
          if (!nullable.GetValueOrDefault())
          {
            nullable = filter.SOQuickProcess;
            if (!nullable.GetValueOrDefault())
              break;
          }
        }
        SOOrderEntry instance1 = PXGraph.CreateInstance<SOOrderEntry>();
        using (IEnumerator<PXResult<PX.Objects.SO.SOOrder>> enumerator = PXSelectBase<PX.Objects.SO.SOOrder, PXSelectJoin<PX.Objects.SO.SOOrder, InnerJoin<FSPostDoc, On<FSPostDoc.postRefNbr, Equal<PX.Objects.SO.SOOrder.orderNbr>, And<Where<FSPostDoc.postOrderType, Equal<PX.Objects.SO.SOOrder.orderType>, Or<FSPostDoc.postOrderTypeNegativeBalance, Equal<PX.Objects.SO.SOOrder.orderType>>>>>>, Where<FSPostDoc.processID, Equal<Required<FSPostDoc.processID>>>>.Config>.Select(graph, new object[1]
        {
          (object) currentProcessID
        }).GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            PX.Objects.SO.SOOrder soOrder = PXResult<PX.Objects.SO.SOOrder>.op_Implicit(enumerator.Current);
            ((PXSelectBase<PX.Objects.SO.SOOrder>) instance1.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) instance1.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) soOrder.OrderNbr, new object[1]
            {
              (object) soOrder.OrderType
            }));
            nullable = soOrder.Hold;
            if (nullable.GetValueOrDefault())
            {
              ((PXSelectBase) instance1.Document).Cache.SetValueExt<PX.Objects.SO.SOOrder.hold>((object) soOrder, (object) false);
              ((PXAction) instance1.Save).Press();
            }
            PXAdapter pxAdapter = new PXAdapter((PXSelectBase) instance1.CurrentDocument, Array.Empty<object>());
            nullable = filter.EmailSalesOrder;
            if (nullable.GetValueOrDefault())
            {
              pxAdapter.Arguments = new Dictionary<string, object>()
              {
                ["notificationCD"] = (object) "SALES ORDER"
              };
              GraphHelper.PressButton((PXAction) instance1.notification, pxAdapter);
            }
            nullable = filter.SOQuickProcess;
            if (nullable.GetValueOrDefault() && ((PXSelectBase<PX.Objects.SO.SOOrderType>) instance1.soordertype).Current != null)
            {
              nullable = ((PXSelectBase<PX.Objects.SO.SOOrderType>) instance1.soordertype).Current.AllowQuickProcess;
              if (nullable.GetValueOrDefault())
              {
                SOOrderEntry.SOQuickProcess.InitQuickProcessPanel((PXGraph) instance1, "");
                PXQuickProcess.Start<SOOrderEntry, PX.Objects.SO.SOOrder, SOQuickProcessParameters>(instance1, soOrder, ((PXSelectBase<SOQuickProcessParameters>) instance1.SOQuickProcessExt.QuickProcessParameters).Current);
                continue;
              }
            }
            nullable = filter.PrepareInvoice;
            if (nullable.GetValueOrDefault())
            {
              if (((PXAction) instance1.prepareInvoice).GetEnabled())
              {
                pxAdapter.MassProcess = true;
                GraphHelper.PressButton((PXAction) instance1.prepareInvoice, pxAdapter);
              }
              nullable = filter.ReleaseInvoice;
              if (nullable.GetValueOrDefault())
              {
                PXResultset<PX.Objects.SO.SOOrderShipment> pxResultset = ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) instance1.shipmentlist).Select(Array.Empty<object>());
                if (pxResultset.Count > 0)
                {
                  PX.Objects.SO.SOOrderShipment soOrderShipment = PXResult<PX.Objects.SO.SOOrderShipment>.op_Implicit(pxResultset[0]);
                  SOInvoiceEntry instance2 = PXGraph.CreateInstance<SOInvoiceEntry>();
                  ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance2.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance2.Document).Search<PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.refNbr>((object) soOrderShipment.InvoiceType, (object) soOrderShipment.InvoiceNbr, new object[1]
                  {
                    (object) soOrderShipment.InvoiceType
                  }));
                  GraphHelper.PressButton((PXAction) instance2.release, new PXAdapter((PXSelectBase) instance2.CurrentDocument, Array.Empty<object>())
                  {
                    MassProcess = true
                  });
                }
              }
            }
          }
          break;
        }
      case "PM":
        try
        {
          this.ReleaseCreatedINIssues(graph, currentProcessID);
          this.ReleaseCreatedPMTransactions(graph, currentProcessID);
          break;
        }
        catch (Exception ex)
        {
          PXTrace.WriteError(ex);
          break;
        }
    }
  }

  public virtual void ReleaseCreatedINIssues(PXGraph graph, Guid currentProcessID)
  {
    IEnumerable<PX.Objects.IN.INRegister> list = (IEnumerable<PX.Objects.IN.INRegister>) GraphHelper.RowCast<PX.Objects.IN.INRegister>((IEnumerable) PXSelectBase<PX.Objects.IN.INRegister, PXSelectJoin<PX.Objects.IN.INRegister, InnerJoin<FSPostDoc, On<FSPostDoc.postRefNbr, Equal<PX.Objects.IN.INRegister.refNbr>, And<FSPostDoc.postDocType, Equal<PX.Objects.IN.INRegister.docType>>>, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSPostDoc.sOID>>, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSServiceOrder.srvOrdType>>>>>, Where<FSPostDoc.processID, Equal<Required<FSPostDoc.processID>>, And<FSPostDoc.postedTO, Equal<Required<FSPostDoc.postedTO>>, And<FSSrvOrdType.releaseIssueOnInvoice, Equal<True>>>>>.Config>.Select(graph, new object[2]
    {
      (object) currentProcessID,
      (object) "IN"
    })).AsEnumerable<PX.Objects.IN.INRegister>().ToList<PX.Objects.IN.INRegister>();
    if (list.Count<PX.Objects.IN.INRegister>() <= 0)
      return;
    INIssueEntry instance = PXGraph.CreateInstance<INIssueEntry>();
    foreach (PX.Objects.IN.INRegister inRegister1 in list)
    {
      PXSelect<PX.Objects.IN.INRegister, Where<PX.Objects.IN.INRegister.docType, Equal<INDocType.issue>>> issue1 = instance.issue;
      PXSelect<PX.Objects.IN.INRegister, Where<PX.Objects.IN.INRegister.docType, Equal<INDocType.issue>>> issue2 = instance.issue;
      string refNbr = inRegister1.RefNbr;
      object[] objArray = new object[1]
      {
        (object) inRegister1.DocType
      };
      PX.Objects.IN.INRegister inRegister2;
      PX.Objects.IN.INRegister inRegister3 = inRegister2 = PXResultset<PX.Objects.IN.INRegister>.op_Implicit(((PXSelectBase<PX.Objects.IN.INRegister>) issue2).Search<PX.Objects.IN.INRegister.refNbr>((object) refNbr, objArray));
      ((PXSelectBase<PX.Objects.IN.INRegister>) issue1).Current = inRegister2;
      PX.Objects.IN.INRegister data = inRegister3;
      if (data.Hold.GetValueOrDefault())
      {
        ((PXSelectBase) instance.issue).Cache.SetValueExtIfDifferent<PX.Objects.IN.INRegister.hold>((object) data, (object) false);
        ((PXSelectBase<PX.Objects.IN.INRegister>) instance.issue).Update(data);
      }
      ((PXAction) instance.release).Press();
    }
  }

  public virtual void ReleaseCreatedPMTransactions(PXGraph graph, Guid currentProcessID)
  {
    IEnumerable<PMRegister> list = (IEnumerable<PMRegister>) GraphHelper.RowCast<PMRegister>((IEnumerable) PXSelectBase<PMRegister, PXSelectJoin<PMRegister, InnerJoin<FSPostDoc, On<FSPostDoc.postRefNbr, Equal<PMRegister.refNbr>, And<FSPostDoc.postDocType, Equal<PMRegister.module>>>, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSPostDoc.sOID>>, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSServiceOrder.srvOrdType>>>>>, Where<FSPostDoc.processID, Equal<Required<FSPostDoc.processID>>, And<FSPostDoc.postedTO, Equal<Required<FSPostDoc.postedTO>>, And<FSSrvOrdType.releaseProjectTransactionOnInvoice, Equal<True>>>>>.Config>.Select(graph, new object[2]
    {
      (object) currentProcessID,
      (object) "PM"
    })).AsEnumerable<PMRegister>().ToList<PMRegister>();
    if (list.Count<PMRegister>() <= 0)
      return;
    RegisterEntry instance = PXGraph.CreateInstance<RegisterEntry>();
    foreach (PMRegister pmRegister1 in list)
    {
      PXSelect<PMRegister, Where<PMRegister.module, Equal<Optional<PMRegister.module>>>> document1 = instance.Document;
      PXSelect<PMRegister, Where<PMRegister.module, Equal<Optional<PMRegister.module>>>> document2 = instance.Document;
      string refNbr = pmRegister1.RefNbr;
      object[] objArray = new object[1]
      {
        (object) pmRegister1.Module
      };
      PMRegister pmRegister2;
      PMRegister pmRegister3 = pmRegister2 = PXResultset<PMRegister>.op_Implicit(((PXSelectBase<PMRegister>) document2).Search<PMRegister.refNbr>((object) refNbr, objArray));
      ((PXSelectBase<PMRegister>) document1).Current = pmRegister2;
      PMRegister doc = pmRegister3;
      instance.ReleaseDocument(doc);
    }
  }

  protected virtual void CreatePostingBatchAndInvoices(
    Guid currentProcessID,
    int billingCycleID,
    DateTime? upToDate,
    DateTime? invoiceDate,
    string invoiceFinPeriodID,
    string postTo,
    List<FSPostDoc> invoiceList,
    List<TPostLine> postLineRows,
    PXQuickProcess.ActionFlow quickProcessFlow,
    bool isGenerateInvoiceScreen)
  {
    CreateInvoiceBase<TGraph, TPostLine>.PostBatchShared postBatchShared = new CreateInvoiceBase<TGraph, TPostLine>.PostBatchShared();
    this.CreatePostingBatch(postBatchShared, billingCycleID, upToDate, invoiceDate, invoiceFinPeriodID, postTo);
    int documentsQty = 0;
    foreach (FSPostDoc invoice in invoiceList)
    {
      List<DocLineExt> invoiceLines = this.GetInvoiceLines(currentProcessID, billingCycleID, invoice.GroupKey, false, out Decimal? _, postTo);
      try
      {
        documentsQty += this.CreateInvoiceDocument(postBatchShared, postTo, currentProcessID, billingCycleID, invoice.GroupKey, invoice.InvtMult, invoiceLines, this.billingBy, quickProcessFlow);
        foreach (TPostLine postLineRow in postLineRows)
        {
          int? nullable1 = postLineRow.BillingCycleID;
          int num = billingCycleID;
          if (nullable1.GetValueOrDefault() == num & nullable1.HasValue && postLineRow.GroupKey == invoice.GroupKey)
          {
            // ISSUE: variable of a boxed type
            __Boxed<TPostLine> local = (object) postLineRow;
            FSPostBatch fsPostBatchRow = postBatchShared.FSPostBatchRow;
            int? nullable2;
            if (fsPostBatchRow == null)
            {
              nullable1 = new int?();
              nullable2 = nullable1;
            }
            else
              nullable2 = fsPostBatchRow.BatchID;
            local.BatchID = nullable2;
            if (isGenerateInvoiceScreen)
            {
              nullable1 = postLineRow.RowIndex;
              PXProcessing<TPostLine>.SetInfo(nullable1.Value, "Record processed successfully.");
            }
          }
        }
      }
      catch (Exception ex)
      {
        foreach (TPostLine postLineRow in postLineRows)
        {
          int? nullable3 = postLineRow.BillingCycleID;
          int num = billingCycleID;
          if (nullable3.GetValueOrDefault() == num & nullable3.HasValue && postLineRow.GroupKey == invoice.GroupKey)
          {
            // ISSUE: variable of a boxed type
            __Boxed<TPostLine> local = (object) postLineRow;
            nullable3 = new int?();
            int? nullable4 = nullable3;
            local.BatchID = nullable4;
            if (isGenerateInvoiceScreen)
            {
              nullable3 = postLineRow.RowIndex;
              PXProcessing<TPostLine>.SetError(nullable3.Value, ex is PXOuterException ? $"{ex.Message}\r\n{string.Join("\r\n", ((PXOuterException) ex).InnerMessages)}" : ex.Message);
            }
          }
        }
        if (!isGenerateInvoiceScreen)
          throw;
      }
    }
    this.ApplyPrepayments(postBatchShared);
    this.CompletePostingBatch(postBatchShared, documentsQty);
  }

  public virtual void CreatePostingBatch(
    CreateInvoiceBase<TGraph, TPostLine>.PostBatchShared postBatchShared,
    int billingCycleID,
    DateTime? upToDate,
    DateTime? invoiceDate,
    string invoiceFinPeriodID,
    string targetScreen)
  {
    postBatchShared.PostBatchEntryGraph = PXGraph.CreateInstance<PostBatchEntry>();
    postBatchShared.FSPostBatchRow = postBatchShared.PostBatchEntryGraph.CreatePostingBatch(new int?(billingCycleID), upToDate, invoiceDate, invoiceFinPeriodID, targetScreen);
  }

  public virtual void ApplyPrepayments(FSPostBatch fsPostBatchRow)
  {
    try
    {
      if (fsPostBatchRow != null && fsPostBatchRow.PostTo == "SO")
      {
        SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
        PXResultset<PostingBatchDetail> pxResultset1 = PXSelectBase<PostingBatchDetail, PXSelect<PostingBatchDetail, Where<PostingBatchDetail.batchID, Equal<Required<FSPostBatch.batchID>>, And<Exists<Select<FSAdjust, Where<PostingBatchDetail.sORefNbr, Equal<FSAdjust.adjdOrderNbr>, And<PostingBatchDetail.srvOrdType, Equal<FSAdjust.adjdOrderType>>>>>>>>.Config>.Select((PXGraph) instance, new object[1]
        {
          (object) fsPostBatchRow.BatchID
        });
        HashSet<(string, string)> valueTupleSet = new HashSet<(string, string)>();
        foreach (PXResult<PostingBatchDetail> pxResult1 in pxResultset1)
        {
          ((PXGraph) instance).Clear();
          PostingBatchDetail postingBatchDetail = PXResult<PostingBatchDetail>.op_Implicit(pxResult1);
          if (valueTupleSet.Add((postingBatchDetail.SOOrderNbr, postingBatchDetail.SOOrderType)))
          {
            PXSelectJoin<PX.Objects.SO.SOOrder, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.SO.SOOrder.customerID>>>, Where<PX.Objects.SO.SOOrder.orderType, Equal<Optional<PX.Objects.SO.SOOrder.orderType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> document1 = instance.Document;
            PXSelectJoin<PX.Objects.SO.SOOrder, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.SO.SOOrder.customerID>>>, Where<PX.Objects.SO.SOOrder.orderType, Equal<Optional<PX.Objects.SO.SOOrder.orderType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> document2 = instance.Document;
            string soOrderNbr = postingBatchDetail.SOOrderNbr;
            object[] objArray = new object[1]
            {
              (object) postingBatchDetail.SOOrderType
            };
            PX.Objects.SO.SOOrder soOrder1;
            PX.Objects.SO.SOOrder soOrder2 = soOrder1 = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) document2).Search<PX.Objects.SO.SOOrder.orderNbr>((object) soOrderNbr, objArray));
            ((PXSelectBase<PX.Objects.SO.SOOrder>) document1).Current = soOrder1;
            PX.Objects.SO.SOOrder soOrder3 = soOrder2;
            PXResultset<SOTax> pxResultset2 = ((PXSelectBase<SOTax>) instance.Tax_Rows).Select(Array.Empty<object>());
            SharedClasses.SOPrepaymentHelper prepaymentHelper = new SharedClasses.SOPrepaymentHelper();
            foreach (PXResult<PX.Objects.SO.SOLine> pxResult2 in ((PXSelectBase<PX.Objects.SO.SOLine>) instance.Transactions).Select(Array.Empty<object>()))
            {
              PX.Objects.SO.SOLine soLineRow = PXResult<PX.Objects.SO.SOLine>.op_Implicit(pxResult2);
              FSxSOLine extension = ((PXSelectBase) instance.Transactions).Cache.GetExtension<FSxSOLine>((object) soLineRow);
              Decimal valueOrDefault = GraphHelper.RowCast<SOTax>((IEnumerable) pxResultset2).Where<SOTax>((Func<SOTax, bool>) (_ =>
              {
                int? lineNbr1 = _.LineNbr;
                int? lineNbr2 = soLineRow.LineNbr;
                return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
              })).Sum<SOTax>((Func<SOTax, Decimal?>) (soTaxRow =>
              {
                Decimal? curyTaxableAmt = soTaxRow.CuryTaxableAmt;
                Decimal? nullable1 = soTaxRow.TaxRate;
                Decimal? nullable2 = curyTaxableAmt.HasValue & nullable1.HasValue ? new Decimal?(curyTaxableAmt.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
                Decimal num = (Decimal) 100;
                if (nullable2.HasValue)
                  return new Decimal?(nullable2.GetValueOrDefault() / num);
                nullable1 = new Decimal?();
                return nullable1;
              })).GetValueOrDefault();
              prepaymentHelper.Add(soLineRow, extension, valueOrDefault);
            }
            Decimal num1 = 0M;
            using (List<SharedClasses.SOPrepaymentBySO>.Enumerator enumerator = prepaymentHelper.SOPrepaymentList.GetEnumerator())
            {
label_46:
              while (enumerator.MoveNext())
              {
                SharedClasses.SOPrepaymentBySO current = enumerator.Current;
                PXResultset<PX.Objects.AR.ARPayment> prepaymentBySo = current.GetPrepaymentBySO((PXGraph) instance);
                int num2 = 0;
                while (true)
                {
                  if (prepaymentBySo != null && num2 < prepaymentBySo.Count)
                  {
                    Decimal? nullable3 = current.unpaidAmount;
                    Decimal num3 = 0M;
                    if (nullable3.GetValueOrDefault() > num3 & nullable3.HasValue)
                    {
                      if (string.Equals(PXResult<PX.Objects.AR.ARPayment>.op_Implicit(prepaymentBySo[num2]).CuryID, soOrder3.CuryID))
                      {
                        SOAdjust soAdjust = ((PXSelectBase<SOAdjust>) instance.Adjustments).Current = ((PXSelectBase<SOAdjust>) instance.Adjustments).Insert(new SOAdjust()
                        {
                          AdjgDocType = "PPM",
                          AdjgRefNbr = PXResult<PX.Objects.AR.ARPayment>.op_Implicit(prepaymentBySo[num2]).RefNbr
                        });
                        Decimal num4 = soAdjust.CuryDocBal.GetValueOrDefault();
                        Decimal? nullable4 = new Decimal?(0M);
                        Decimal? nullable5;
                        if (num4 > 0M)
                        {
                          nullable5 = current.unpaidAmount;
                          Decimal num5 = num4;
                          if (nullable5.GetValueOrDefault() > num5 & nullable5.HasValue)
                          {
                            nullable4 = new Decimal?(num4);
                            SharedClasses.SOPrepaymentBySO soPrepaymentBySo = current;
                            nullable5 = soPrepaymentBySo.unpaidAmount;
                            Decimal num6 = num4;
                            Decimal? nullable6;
                            if (!nullable5.HasValue)
                            {
                              nullable3 = new Decimal?();
                              nullable6 = nullable3;
                            }
                            else
                              nullable6 = new Decimal?(nullable5.GetValueOrDefault() - num6);
                            soPrepaymentBySo.unpaidAmount = nullable6;
                            num4 = 0M;
                          }
                          else
                          {
                            ((PXSelectBase<SOAdjust>) instance.Adjustments).SetValueExt<SOAdjust.adjgRefNbr>(soAdjust, (object) PXResult<PX.Objects.AR.ARPayment>.op_Implicit(prepaymentBySo[num2]).RefNbr);
                            nullable4 = current.unpaidAmount;
                            Decimal num7 = num4;
                            nullable5 = current.unpaidAmount;
                            Decimal? nullable7;
                            if (!nullable5.HasValue)
                            {
                              nullable3 = new Decimal?();
                              nullable7 = nullable3;
                            }
                            else
                              nullable7 = new Decimal?(num7 - nullable5.GetValueOrDefault());
                            nullable3 = nullable7;
                            num4 = nullable3.GetValueOrDefault();
                            current.unpaidAmount = new Decimal?(0M);
                          }
                          ((PXSelectBase<SOAdjust>) instance.Adjustments).SetValueExt<SOAdjust.curyAdjdAmt>(soAdjust, (object) nullable4);
                        }
                        foreach (PXResult<SOTaxTran> pxResult3 in ((PXSelectBase<SOTaxTran>) instance.Taxes).Select(Array.Empty<object>()))
                        {
                          SOTaxTran soTaxTran = PXResult<SOTaxTran>.op_Implicit(pxResult3);
                          if (num4 > 0M)
                          {
                            nullable5 = soTaxTran.CuryTaxAmt;
                            Decimal num8 = num4;
                            if (nullable5.GetValueOrDefault() > num8 & nullable5.HasValue)
                            {
                              nullable5 = nullable4;
                              Decimal num9 = num4;
                              Decimal? nullable8;
                              if (!nullable5.HasValue)
                              {
                                nullable3 = new Decimal?();
                                nullable8 = nullable3;
                              }
                              else
                                nullable8 = new Decimal?(nullable5.GetValueOrDefault() + num9);
                              nullable4 = nullable8;
                              break;
                            }
                            nullable5 = nullable4;
                            nullable3 = soTaxTran.CuryTaxAmt;
                            nullable4 = nullable5.HasValue & nullable3.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
                            Decimal num10 = num4;
                            nullable3 = soTaxTran.CuryTaxAmt;
                            Decimal? nullable9;
                            if (!nullable3.HasValue)
                            {
                              nullable5 = new Decimal?();
                              nullable9 = nullable5;
                            }
                            else
                              nullable9 = new Decimal?(num10 - nullable3.GetValueOrDefault());
                            nullable5 = nullable9;
                            num4 = nullable5.GetValueOrDefault();
                            ((PXSelectBase<SOAdjust>) instance.Adjustments).SetValueExt<SOAdjust.curyAdjdAmt>(soAdjust, (object) nullable4);
                          }
                        }
                        ((PXSelectBase<SOAdjust>) instance.Adjustments).Update(soAdjust);
                      }
                      num1 = 0M;
                      ++num2;
                    }
                    else
                      goto label_46;
                  }
                  else
                    goto label_46;
                }
              }
            }
            foreach (PXResult<SOAdjust> pxResult4 in ((PXSelectBase<SOAdjust>) instance.Adjustments).Select(Array.Empty<object>()))
            {
              SOAdjust soAdjust = PXResult<SOAdjust>.op_Implicit(pxResult4);
              Decimal? curyAdjdAmt = soAdjust.CuryAdjdAmt;
              Decimal num11 = 0M;
              if (curyAdjdAmt.GetValueOrDefault() == num11 & curyAdjdAmt.HasValue)
                ((PXSelectBase<SOAdjust>) instance.Adjustments).Delete(soAdjust);
            }
            ((PXAction) instance.Save).Press();
          }
        }
      }
      else
      {
        if (fsPostBatchRow == null || !(fsPostBatchRow.PostTo == "SI"))
          return;
        SOInvoiceEntry instance = PXGraph.CreateInstance<SOInvoiceEntry>();
        PXResultset<PostingBatchDetail> pxResultset3 = PXSelectBase<PostingBatchDetail, PXSelect<PostingBatchDetail, Where<PostingBatchDetail.batchID, Equal<Required<FSPostBatch.batchID>>, And<Exists<Select<FSAdjust, Where<PostingBatchDetail.sORefNbr, Equal<FSAdjust.adjdOrderNbr>, And<PostingBatchDetail.srvOrdType, Equal<FSAdjust.adjdOrderType>>>>>>>>.Config>.Select((PXGraph) instance, new object[1]
        {
          (object) fsPostBatchRow.BatchID
        });
        HashSet<(string, string)> valueTupleSet = new HashSet<(string, string)>();
        foreach (PXResult<PostingBatchDetail> pxResult5 in pxResultset3)
        {
          ((PXGraph) instance).Clear();
          PostingBatchDetail postingBatchDetail = PXResult<PostingBatchDetail>.op_Implicit(pxResult5);
          if (!(postingBatchDetail.SOInvDocType != "INV") && valueTupleSet.Add((postingBatchDetail.SOInvRefNbr, postingBatchDetail.SOInvDocType)))
          {
            PXSelectJoin<PX.Objects.AR.ARInvoice, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.AR.ARInvoice.customerID>>>, Where<PX.Objects.AR.ARInvoice.docType, Equal<Optional<PX.Objects.AR.ARInvoice.docType>>, And2<Where<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleAR>, Or<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleEP>, Or<PX.Objects.AR.ARInvoice.released, Equal<True>>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>> document3 = instance.Document;
            PXSelectJoin<PX.Objects.AR.ARInvoice, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.AR.ARInvoice.customerID>>>, Where<PX.Objects.AR.ARInvoice.docType, Equal<Optional<PX.Objects.AR.ARInvoice.docType>>, And2<Where<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleAR>, Or<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleEP>, Or<PX.Objects.AR.ARInvoice.released, Equal<True>>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>> document4 = instance.Document;
            string soInvRefNbr = postingBatchDetail.SOInvRefNbr;
            object[] objArray = new object[1]
            {
              (object) postingBatchDetail.SOInvDocType
            };
            PX.Objects.AR.ARInvoice arInvoice1;
            PX.Objects.AR.ARInvoice arInvoice2 = arInvoice1 = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) document4).Search<PX.Objects.AR.ARInvoice.refNbr>((object) soInvRefNbr, objArray));
            ((PXSelectBase<PX.Objects.AR.ARInvoice>) document3).Current = arInvoice1;
            PX.Objects.AR.ARInvoice arInvoice3 = arInvoice2;
            instance.LoadDocumentsProc();
            SharedClasses.SOPrepaymentHelper prepaymentHelper = new SharedClasses.SOPrepaymentHelper();
            PXResultset<ARTax> pxResultset4 = ((PXSelectBase<ARTax>) instance.Tax_Rows).Select(Array.Empty<object>());
            foreach (PXResult<PX.Objects.AR.ARTran> pxResult6 in PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARTran.tranType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARTran.refNbr>>>>>.Config>.Select((PXGraph) instance, new object[2]
            {
              (object) arInvoice3.DocType,
              (object) arInvoice3.RefNbr
            }))
            {
              PX.Objects.AR.ARTran arTranRow = PXResult<PX.Objects.AR.ARTran>.op_Implicit(pxResult6);
              FSxARTran extension = ((PXSelectBase) instance.Transactions).Cache.GetExtension<FSxARTran>((object) arTranRow);
              Decimal valueOrDefault = GraphHelper.RowCast<ARTax>((IEnumerable) pxResultset4).Where<ARTax>((Func<ARTax, bool>) (_ =>
              {
                int? lineNbr3 = _.LineNbr;
                int? lineNbr4 = arTranRow.LineNbr;
                return lineNbr3.GetValueOrDefault() == lineNbr4.GetValueOrDefault() & lineNbr3.HasValue == lineNbr4.HasValue;
              })).Sum<ARTax>((Func<ARTax, Decimal?>) (arTaxRow =>
              {
                Decimal? curyTaxableAmt = arTaxRow.CuryTaxableAmt;
                Decimal? nullable10 = arTaxRow.TaxRate;
                Decimal? nullable11 = curyTaxableAmt.HasValue & nullable10.HasValue ? new Decimal?(curyTaxableAmt.GetValueOrDefault() * nullable10.GetValueOrDefault()) : new Decimal?();
                Decimal num = (Decimal) 100;
                if (nullable11.HasValue)
                  return new Decimal?(nullable11.GetValueOrDefault() / num);
                nullable10 = new Decimal?();
                return nullable10;
              })).GetValueOrDefault();
              prepaymentHelper.Add(arTranRow, extension, valueOrDefault);
            }
            Decimal num12 = 0M;
            using (List<SharedClasses.SOPrepaymentBySO>.Enumerator enumerator = prepaymentHelper.SOPrepaymentList.GetEnumerator())
            {
label_111:
              while (enumerator.MoveNext())
              {
                SharedClasses.SOPrepaymentBySO current = enumerator.Current;
                PXResultset<PX.Objects.AR.ARPayment> prepaymentBySo = current.GetPrepaymentBySO((PXGraph) instance);
                int num13 = 0;
                while (true)
                {
                  if (prepaymentBySo != null && num13 < prepaymentBySo.Count)
                  {
                    Decimal? nullable12 = current.unpaidAmount;
                    Decimal num14 = 0M;
                    if (nullable12.GetValueOrDefault() > num14 & nullable12.HasValue)
                    {
                      PX.Objects.AR.ARPayment arPaymentRow = PXResult<PX.Objects.AR.ARPayment>.op_Implicit(prepaymentBySo[num13]);
                      if (string.Equals(arPaymentRow.CuryID, arInvoice3.CuryID))
                      {
                        ARAdjust2 arAdjust2 = PXResult<ARAdjust2>.op_Implicit(((IQueryable<PXResult<ARAdjust2>>) ((PXSelectBase<ARAdjust2>) instance.Adjustments).Select(Array.Empty<object>())).Where<PXResult<ARAdjust2>>((Expression<Func<PXResult<ARAdjust2>, bool>>) (x => ((ARAdjust2) x).AdjgRefNbr == arPaymentRow.RefNbr)).FirstOrDefault<PXResult<ARAdjust2>>());
                        if (arAdjust2 == null)
                        {
                          num12 = 0M;
                          ++num13;
                          continue;
                        }
                        Decimal num15 = arAdjust2.CuryDocBal.GetValueOrDefault();
                        Decimal? nullable13 = new Decimal?(0M);
                        Decimal? nullable14;
                        if (num15 > 0M)
                        {
                          nullable14 = current.unpaidAmount;
                          Decimal num16 = num15;
                          if (nullable14.GetValueOrDefault() > num16 & nullable14.HasValue)
                          {
                            nullable13 = new Decimal?(num15);
                            SharedClasses.SOPrepaymentBySO soPrepaymentBySo = current;
                            nullable14 = current.unpaidAmount;
                            Decimal num17 = num15;
                            Decimal? nullable15;
                            if (!nullable14.HasValue)
                            {
                              nullable12 = new Decimal?();
                              nullable15 = nullable12;
                            }
                            else
                              nullable15 = new Decimal?(nullable14.GetValueOrDefault() - num17);
                            soPrepaymentBySo.unpaidAmount = nullable15;
                            num15 = 0M;
                          }
                          else
                          {
                            nullable13 = current.unpaidAmount;
                            Decimal num18 = num15;
                            nullable14 = current.unpaidAmount;
                            Decimal? nullable16;
                            if (!nullable14.HasValue)
                            {
                              nullable12 = new Decimal?();
                              nullable16 = nullable12;
                            }
                            else
                              nullable16 = new Decimal?(num18 - nullable14.GetValueOrDefault());
                            nullable12 = nullable16;
                            num15 = nullable12.GetValueOrDefault();
                            current.unpaidAmount = new Decimal?(0M);
                          }
                          ((PXSelectBase<ARAdjust2>) instance.Adjustments).SetValueExt<ARAdjust2.curyAdjdAmt>(arAdjust2, (object) nullable13);
                        }
                        foreach (PXResult<ARTaxTran> pxResult7 in ((PXSelectBase<ARTaxTran>) instance.Taxes).Select(Array.Empty<object>()))
                        {
                          ARTaxTran arTaxTran = PXResult<ARTaxTran>.op_Implicit(pxResult7);
                          if (num15 > 0M)
                          {
                            nullable14 = arTaxTran.CuryTaxAmt;
                            Decimal num19 = num15;
                            if (nullable14.GetValueOrDefault() > num19 & nullable14.HasValue)
                            {
                              nullable14 = nullable13;
                              Decimal num20 = num15;
                              Decimal? nullable17;
                              if (!nullable14.HasValue)
                              {
                                nullable12 = new Decimal?();
                                nullable17 = nullable12;
                              }
                              else
                                nullable17 = new Decimal?(nullable14.GetValueOrDefault() + num20);
                              nullable13 = nullable17;
                              break;
                            }
                            nullable14 = nullable13;
                            nullable12 = arTaxTran.CuryTaxAmt;
                            nullable13 = nullable14.HasValue & nullable12.HasValue ? new Decimal?(nullable14.GetValueOrDefault() + nullable12.GetValueOrDefault()) : new Decimal?();
                            Decimal num21 = num15;
                            nullable12 = arTaxTran.CuryTaxAmt;
                            Decimal? nullable18;
                            if (!nullable12.HasValue)
                            {
                              nullable14 = new Decimal?();
                              nullable18 = nullable14;
                            }
                            else
                              nullable18 = new Decimal?(num21 - nullable12.GetValueOrDefault());
                            nullable14 = nullable18;
                            num15 = nullable14.GetValueOrDefault();
                            ((PXSelectBase<ARAdjust2>) instance.Adjustments).SetValueExt<ARAdjust2.curyAdjdAmt>(arAdjust2, (object) nullable13);
                          }
                        }
                        ((PXSelectBase<ARAdjust2>) instance.Adjustments).Update(arAdjust2);
                      }
                      num12 = 0M;
                      ++num13;
                    }
                    else
                      goto label_111;
                  }
                  else
                    goto label_111;
                }
              }
            }
            ((PXAction) instance.Save).Press();
          }
        }
      }
    }
    catch (Exception ex)
    {
    }
  }

  public virtual void ApplyPrepayments(
    CreateInvoiceBase<TGraph, TPostLine>.PostBatchShared postBatchShared)
  {
    this.ApplyPrepayments(postBatchShared.FSPostBatchRow);
  }

  public virtual void CompletePostingBatch(
    CreateInvoiceBase<TGraph, TPostLine>.PostBatchShared postBatchShared,
    int documentsQty)
  {
    postBatchShared.PostBatchEntryGraph.CompletePostingBatch(postBatchShared.FSPostBatchRow, documentsQty);
  }

  public virtual int CreateInvoiceDocument(
    CreateInvoiceBase<TGraph, TPostLine>.PostBatchShared postBatchShared,
    string targetScreen,
    Guid currentProcessID,
    int billingCycleID,
    string groupKey,
    short? invtMult,
    List<DocLineExt> docLines,
    string billingBy,
    PXQuickProcess.ActionFlow quickProcessFlow)
  {
    if (billingBy == "SO")
      this.CheckLotSerialClass(docLines);
    InvoicingProcessStepGroupShared processStepGroupShared = new InvoicingProcessStepGroupShared();
    processStepGroupShared.Initialize(targetScreen, billingBy);
    processStepGroupShared.InvoiceGraph.IsInvoiceProcessRunning = true;
    OnTransactionInsertedDelegate transactionInserted = processStepGroupShared.ProcessGraph.OnTransactionInserted;
    FSCreatedDoc fsCreatedDoc = (FSCreatedDoc) null;
    docLines = docLines.OrderBy<DocLineExt, string>((Func<DocLineExt, string>) (x => x.docLine.SrvOrdType)).ThenBy<DocLineExt, string>((Func<DocLineExt, string>) (x => x.docLine.RefNbr)).ThenBy<DocLineExt, int?>((Func<DocLineExt, int?>) (x => x.docLine.SortOrder)).ToList<DocLineExt>();
    int num1 = !(postBatchShared.FSPostBatchRow.PostTo == "PM") ? 0 : (docLines.Where<DocLineExt>((Func<DocLineExt, bool>) (x => x.docLine.LineType == "SLPRO")).Count<DocLineExt>() > 0 ? 1 : 0);
    processStepGroupShared.InvoiceGraph.CreateInvoice(processStepGroupShared.ProcessGraph.GetGraph(), docLines, invtMult.GetValueOrDefault(), postBatchShared.FSPostBatchRow.InvoiceDate, postBatchShared.FSPostBatchRow.FinPeriodID, processStepGroupShared.ProcessGraph.OnDocumentHeaderInserted, transactionInserted, quickProcessFlow);
    IInvoiceGraph invoiceGraph = num1 != 0 ? this.CreateInvoiceGraph("IN") : (IInvoiceGraph) null;
    invoiceGraph?.CreateInvoice(processStepGroupShared.ProcessGraph.GetGraph(), docLines, invtMult.GetValueOrDefault(), postBatchShared.FSPostBatchRow.InvoiceDate, postBatchShared.FSPostBatchRow.FinPeriodID, processStepGroupShared.ProcessGraph.OnDocumentHeaderInserted, transactionInserted, quickProcessFlow);
    this.DeallocateItemsThatAreBeingPosted(processStepGroupShared.ServiceOrderGraph, docLines, processStepGroupShared.ProcessGraph is CreateInvoiceByAppointmentPost);
    if (processStepGroupShared.InvoiceGraph.GetGraph() is SOInvoiceEntry)
    {
      SOInvoiceEntry graph = processStepGroupShared.InvoiceGraph.GetGraph() as SOInvoiceEntry;
      foreach (PXResult<PX.Objects.AR.ARTran> pxResult in ((PXSelectBase<PX.Objects.AR.ARTran>) graph.Transactions).Select(Array.Empty<object>()))
      {
        PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran>.op_Implicit(pxResult);
        Decimal? unassignedQty = arTran.UnassignedQty;
        Decimal num2 = 0M;
        if (unassignedQty.GetValueOrDefault() > num2 & unassignedQty.HasValue && !string.IsNullOrEmpty(arTran.LotSerialNbr))
        {
          PX.Objects.AR.ARTran copy = (PX.Objects.AR.ARTran) ((PXSelectBase) graph.Transactions).Cache.CreateCopy((object) arTran);
          ((PXSelectBase) graph.Transactions).Cache.RaiseFieldUpdated<PX.Objects.AR.ARTran.qty>((object) arTran, (object) copy.Qty);
          ((PXSelectBase) graph.Transactions).Cache.RaiseRowUpdated((object) arTran, (object) copy);
        }
      }
    }
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (invoiceGraph != null)
        fsCreatedDoc = invoiceGraph.PressSave(postBatchShared.FSPostBatchRow.BatchID.Value, docLines, processStepGroupShared.ProcessGraph.BeforeSave);
      FSCreatedDoc fsCreatedDocRow = processStepGroupShared.InvoiceGraph.PressSave(postBatchShared.FSPostBatchRow.BatchID.Value, docLines, processStepGroupShared.ProcessGraph.BeforeSave);
      processStepGroupShared.CacheFSCreatedDoc.Insert(fsCreatedDocRow);
      ((PXCache) processStepGroupShared.CacheFSCreatedDoc).Persist((PXDBOperation) 2);
      if (invoiceGraph != null)
      {
        processStepGroupShared.CacheFSCreatedDoc.Insert(fsCreatedDoc);
        ((PXCache) processStepGroupShared.CacheFSCreatedDoc).Persist((PXDBOperation) 2);
      }
      PXGraph graph = processStepGroupShared.ProcessGraph.GetGraph();
      CreateInvoiceBase<TGraph, TPostLine>.UpdateFSPostDoc(graph, fsCreatedDocRow, currentProcessID, new int?(billingCycleID), groupKey);
      List<DocLineExt> list = docLines.GroupBy<DocLineExt, int?>((Func<DocLineExt, int?>) (r => r.docLine.DocID)).Select<IGrouping<int?, DocLineExt>, DocLineExt>((Func<IGrouping<int?, DocLineExt>, DocLineExt>) (g => g.First<DocLineExt>())).ToList<DocLineExt>();
      CreateInvoiceBase<TGraph, TPostLine>.CreatePostRegisterAndBillHistory(graph, list, fsCreatedDocRow, currentProcessID);
      if (fsCreatedDoc != null)
      {
        CreateInvoiceBase<TGraph, TPostLine>.CreatePostRegisterAndBillHistory(graph, list, fsCreatedDoc, currentProcessID);
        CreateInvoiceBase<TGraph, TPostLine>.CreateNewPostDocs(graph, list, fsCreatedDoc, currentProcessID);
      }
      if (processStepGroupShared.ProcessGraph.AfterCreateInvoice != null)
        processStepGroupShared.ProcessGraph.AfterCreateInvoice(processStepGroupShared.InvoiceGraph.GetGraph(), fsCreatedDocRow);
      this.UpdatePostInfoAndPostDet(processStepGroupShared.ServiceOrderGraph, docLines, postBatchShared.FSPostBatchRow, processStepGroupShared.PostInfoEntryGraph, processStepGroupShared.CacheFSPostDet, fsCreatedDocRow, fsCreatedDoc);
      transactionScope.Complete();
    }
    int invoiceDocument = docLines.GroupBy<DocLineExt, int?>((Func<DocLineExt, int?>) (y => y.docLine.DocID)).Count<IGrouping<int?, DocLineExt>>();
    processStepGroupShared.InvoiceGraph.IsInvoiceProcessRunning = false;
    processStepGroupShared.InvoiceGraph.Clear();
    ((PXCache) processStepGroupShared.CacheFSCreatedDoc).Clear();
    invoiceGraph?.Clear();
    processStepGroupShared.Clear();
    return invoiceDocument;
  }

  private void CheckLotSerialClass(List<DocLineExt> docLines)
  {
    foreach (DocLineExt docLine in docLines)
    {
      if (this.GetLotSerialClass(docLine.fsSODet.InventoryID)?.LotSerAssign == "U")
        throw new PXException("Items that have a lot or serial class with the When Used assignment method cannot be used with a service document where Billing By is set to Service Orders.");
    }
  }

  private INLotSerClass GetLotSerialClass(int? inventoryID)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, inventoryID);
    return inventoryItem != null && inventoryItem.LotSerClassID != null ? INLotSerClass.PK.Find((PXGraph) this, inventoryItem.LotSerClassID) : (INLotSerClass) null;
  }

  protected virtual Guid CreatePostDocsFromUserSelection(List<TPostLine> postLineRows)
  {
    Guid fromUserSelection = Guid.NewGuid();
    int num = 0;
    FSPostDoc fsPostDoc = new FSPostDoc();
    string str = ((PXGraph) this).Accessinfo.ScreenID.Replace(".", string.Empty);
    foreach (TPostLine postLineRow in postLineRows)
    {
      if ((object) postLineRow != null)
      {
        fsPostDoc.ProcessID = new Guid?(fromUserSelection);
        fsPostDoc.BillingCycleID = postLineRow.BillingCycleID;
        fsPostDoc.GroupKey = this.GetGroupKey(postLineRow);
        fsPostDoc.SOID = postLineRow.SOID;
        fsPostDoc.AppointmentID = postLineRow.AppointmentID;
        fsPostDoc.RowIndex = new int?(num);
        fsPostDoc.PostNegBalanceToAP = postLineRow.PostNegBalanceToAP;
        fsPostDoc.PostOrderType = postLineRow.PostOrderType;
        fsPostDoc.PostOrderTypeNegativeBalance = postLineRow.PostOrderTypeNegativeBalance;
        postLineRow.RowIndex = fsPostDoc.RowIndex;
        postLineRow.GroupKey = fsPostDoc.GroupKey;
        fsPostDoc.EntityType = postLineRow.EntityType;
        ++num;
        PXDatabase.Insert<FSPostDoc>(new PXDataFieldAssign[13]
        {
          (PXDataFieldAssign) new PXDataFieldAssign<FSPostDoc.processID>((object) fsPostDoc.ProcessID),
          (PXDataFieldAssign) new PXDataFieldAssign<FSPostDoc.billingCycleID>((object) fsPostDoc.BillingCycleID),
          (PXDataFieldAssign) new PXDataFieldAssign<FSPostDoc.groupKey>((object) fsPostDoc.GroupKey),
          (PXDataFieldAssign) new PXDataFieldAssign<FSPostDoc.entityType>((object) fsPostDoc.EntityType),
          (PXDataFieldAssign) new PXDataFieldAssign<FSPostDoc.sOID>((object) fsPostDoc.SOID),
          (PXDataFieldAssign) new PXDataFieldAssign<FSPostDoc.appointmentID>((object) fsPostDoc.AppointmentID),
          (PXDataFieldAssign) new PXDataFieldAssign<FSPostDoc.rowIndex>((object) fsPostDoc.RowIndex),
          (PXDataFieldAssign) new PXDataFieldAssign<FSPostDoc.postNegBalanceToAP>((object) fsPostDoc.PostNegBalanceToAP),
          (PXDataFieldAssign) new PXDataFieldAssign<FSPostDoc.postOrderType>((object) fsPostDoc.PostOrderType),
          (PXDataFieldAssign) new PXDataFieldAssign<FSPostDoc.postOrderTypeNegativeBalance>((object) fsPostDoc.PostOrderTypeNegativeBalance),
          (PXDataFieldAssign) new PXDataFieldAssign<FSPostDoc.createdByID>((object) ((PXGraph) this).Accessinfo.UserID),
          (PXDataFieldAssign) new PXDataFieldAssign<FSPostDoc.createdByScreenID>((object) str),
          (PXDataFieldAssign) new PXDataFieldAssign<FSPostDoc.createdDateTime>((object) DateTime.Now)
        });
      }
    }
    return fromUserSelection;
  }

  protected virtual void DeletePostDocsWithError(Guid currentProcessID)
  {
    PXDatabase.Delete<FSPostDoc>(new PXDataFieldRestrict[2]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSPostDoc.batchID>((PXDbType) 8, new int?(4), (object) null, (PXComp) 6),
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSPostDoc.processID>((object) currentProcessID)
    });
    PXDatabase.Delete<FSPostDoc>(new PXDataFieldRestrict[2]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSPostDoc.batchID>((PXDbType) 8, new int?(4), (object) null, (PXComp) 6),
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSPostDoc.createdDateTime>((PXDbType) 4, new int?(8), (object) DateTime.Now.AddDays(-3.0), (PXComp) 5)
    });
  }

  protected virtual void CalculateExternalTaxes(Guid currentProcessID)
  {
    PXResultset<FSPostDoc> pxResultset = PXSelectBase<FSPostDoc, PXSelectGroupBy<FSPostDoc, Where<FSPostDoc.processID, Equal<Required<FSPostDoc.processID>>>, Aggregate<GroupBy<FSPostDoc.postedTO, GroupBy<FSPostDoc.postDocType, GroupBy<FSPostDoc.postRefNbr>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) currentProcessID
    });
    SOOrderEntry soOrderEntry = (SOOrderEntry) null;
    ARInvoiceEntry arInvoiceEntry = (ARInvoiceEntry) null;
    APInvoiceEntry apInvoiceEntry = (APInvoiceEntry) null;
    bool flag1 = false;
    foreach (PXResult<FSPostDoc> pxResult in pxResultset)
    {
      FSPostDoc fsPostDoc = PXResult<FSPostDoc>.op_Implicit(pxResult);
      if (fsPostDoc.PostedTO == "SO")
      {
        if (soOrderEntry == null || flag1)
        {
          soOrderEntry = (SOOrderEntry) this.CreateInvoiceGraph(fsPostDoc.PostedTO).GetGraph();
          flag1 = false;
        }
        PXSelectJoin<PX.Objects.SO.SOOrder, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.SO.SOOrder.customerID>>>, Where<PX.Objects.SO.SOOrder.orderType, Equal<Optional<PX.Objects.SO.SOOrder.orderType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> document1 = soOrderEntry.Document;
        PXSelectJoin<PX.Objects.SO.SOOrder, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.SO.SOOrder.customerID>>>, Where<PX.Objects.SO.SOOrder.orderType, Equal<Optional<PX.Objects.SO.SOOrder.orderType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> document2 = soOrderEntry.Document;
        string postRefNbr = fsPostDoc.PostRefNbr;
        object[] objArray = new object[1]
        {
          (object) fsPostDoc.PostDocType
        };
        PX.Objects.SO.SOOrder soOrder1;
        PX.Objects.SO.SOOrder soOrder2 = soOrder1 = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) document2).Search<PX.Objects.SO.SOOrder.orderNbr>((object) postRefNbr, objArray));
        ((PXSelectBase<PX.Objects.SO.SOOrder>) document1).Current = soOrder1;
        PX.Objects.SO.SOOrder soOrder3 = soOrder2;
        if (soOrder3 != null)
        {
          bool? isTaxValid = soOrder3.IsTaxValid;
          bool flag2 = false;
          if (isTaxValid.GetValueOrDefault() == flag2 & isTaxValid.HasValue && soOrderEntry.IsExternalTax(soOrder3.TaxZoneID))
          {
            ((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Update(((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current);
            try
            {
              ((PXAction) soOrderEntry.Save).Press();
            }
            catch (Exception ex)
            {
              PXTrace.WriteError("Error trying to calculate external taxes for the Sales Order {0}-{1} with the message: {2}", new object[3]
              {
                (object) soOrder3.OrderType,
                (object) soOrder3.OrderNbr,
                (object) ex.Message
              });
              ((PXGraph) soOrderEntry).Clear((PXClearOption) 3);
              flag1 = true;
            }
          }
        }
      }
      else if (fsPostDoc.PostedTO == "AR")
      {
        if (arInvoiceEntry == null || flag1)
        {
          arInvoiceEntry = (ARInvoiceEntry) this.CreateInvoiceGraph(fsPostDoc.PostedTO).GetGraph();
          flag1 = false;
        }
        PXSelectJoin<PX.Objects.AR.ARInvoice, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.AR.ARInvoice.customerID>>>, Where<PX.Objects.AR.ARInvoice.docType, Equal<Optional<PX.Objects.AR.ARInvoice.docType>>, And2<Where<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleAR>, Or<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleEP>, Or<PX.Objects.AR.ARInvoice.released, Equal<True>>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>> document3 = arInvoiceEntry.Document;
        PXSelectJoin<PX.Objects.AR.ARInvoice, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.AR.ARInvoice.customerID>>>, Where<PX.Objects.AR.ARInvoice.docType, Equal<Optional<PX.Objects.AR.ARInvoice.docType>>, And2<Where<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleAR>, Or<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleEP>, Or<PX.Objects.AR.ARInvoice.released, Equal<True>>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>> document4 = arInvoiceEntry.Document;
        string postRefNbr = fsPostDoc.PostRefNbr;
        object[] objArray = new object[1]
        {
          (object) fsPostDoc.PostDocType
        };
        PX.Objects.AR.ARInvoice arInvoice1;
        PX.Objects.AR.ARInvoice arInvoice2 = arInvoice1 = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) document4).Search<PX.Objects.AR.ARInvoice.refNbr>((object) postRefNbr, objArray));
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) document3).Current = arInvoice1;
        PX.Objects.AR.ARInvoice arInvoice3 = arInvoice2;
        if (arInvoice3 != null)
        {
          bool? isTaxValid = arInvoice3.IsTaxValid;
          bool flag3 = false;
          if (isTaxValid.GetValueOrDefault() == flag3 & isTaxValid.HasValue && arInvoiceEntry.IsExternalTax(arInvoice3.TaxZoneID))
          {
            ((PXSelectBase<PX.Objects.AR.ARInvoice>) arInvoiceEntry.Document).Update(((PXSelectBase<PX.Objects.AR.ARInvoice>) arInvoiceEntry.Document).Current);
            try
            {
              ((PXAction) arInvoiceEntry.Save).Press();
            }
            catch (Exception ex)
            {
              PXTrace.WriteError("Error trying to calculate external taxes for the AR Invoice {0}-{1} with the message: {2}", new object[3]
              {
                (object) arInvoice3.DocType,
                (object) arInvoice3.RefNbr,
                (object) ex.Message
              });
              ((PXGraph) arInvoiceEntry).Clear((PXClearOption) 3);
              flag1 = true;
            }
          }
        }
      }
      else if (fsPostDoc.PostedTO == "AP")
      {
        if (apInvoiceEntry == null || flag1)
        {
          apInvoiceEntry = (APInvoiceEntry) this.CreateInvoiceGraph(fsPostDoc.PostedTO).GetGraph();
          flag1 = false;
        }
        PXSelectJoin<PX.Objects.AP.APInvoice, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.AP.APInvoice.vendorID>>>, Where<PX.Objects.AP.APInvoice.docType, Equal<Optional<PX.Objects.AP.APInvoice.docType>>, And2<Where<PX.Objects.AP.APRegister.origModule, NotEqual<BatchModule.moduleTX>, Or<PX.Objects.AP.APInvoice.released, Equal<True>>>, And<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>>> document5 = apInvoiceEntry.Document;
        PXSelectJoin<PX.Objects.AP.APInvoice, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.AP.APInvoice.vendorID>>>, Where<PX.Objects.AP.APInvoice.docType, Equal<Optional<PX.Objects.AP.APInvoice.docType>>, And2<Where<PX.Objects.AP.APRegister.origModule, NotEqual<BatchModule.moduleTX>, Or<PX.Objects.AP.APInvoice.released, Equal<True>>>, And<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>>> document6 = apInvoiceEntry.Document;
        string postRefNbr = fsPostDoc.PostRefNbr;
        object[] objArray = new object[1]
        {
          (object) fsPostDoc.PostDocType
        };
        PX.Objects.AP.APInvoice apInvoice1;
        PX.Objects.AP.APInvoice apInvoice2 = apInvoice1 = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AP.APInvoice>) document6).Search<PX.Objects.AP.APInvoice.refNbr>((object) postRefNbr, objArray));
        ((PXSelectBase<PX.Objects.AP.APInvoice>) document5).Current = apInvoice1;
        PX.Objects.AP.APInvoice apInvoice3 = apInvoice2;
        if (apInvoice3 != null)
        {
          bool? isTaxValid = apInvoice3.IsTaxValid;
          bool flag4 = false;
          if (isTaxValid.GetValueOrDefault() == flag4 & isTaxValid.HasValue && apInvoiceEntry.IsExternalTax(apInvoice3.TaxZoneID))
          {
            ((PXSelectBase<PX.Objects.AP.APInvoice>) apInvoiceEntry.Document).Update(((PXSelectBase<PX.Objects.AP.APInvoice>) apInvoiceEntry.Document).Current);
            try
            {
              ((PXAction) apInvoiceEntry.Save).Press();
            }
            catch (Exception ex)
            {
              PXTrace.WriteError("Error trying to calculate external taxes for the AP Bill {0}-{1} with the message: {2}", new object[3]
              {
                (object) apInvoice3.DocType,
                (object) apInvoice3.RefNbr,
                (object) ex.Message
              });
              ((PXGraph) apInvoiceEntry).Clear((PXClearOption) 3);
              flag1 = true;
            }
          }
        }
      }
    }
  }

  protected virtual string GetGroupKey(TPostLine postLineRow)
  {
    return !(postLineRow.PostTo != "PM") ? this.GetProjectGroupKey(postLineRow) : this.GetNonProjectGroupKey(postLineRow);
  }

  protected virtual string GetNonProjectGroupKey(TPostLine postLineRow)
  {
    if (this.groupKey == null)
      this.groupKey = new StringBuilder();
    else
      this.groupKey.Clear();
    StringBuilder groupKey1 = this.groupKey;
    string[] strArray = new string[10];
    int? nullable = postLineRow.BranchID;
    strArray[0] = nullable.ToString();
    strArray[1] = "|";
    nullable = postLineRow.BillCustomerID;
    strArray[2] = nullable.ToString();
    strArray[3] = "|";
    strArray[4] = postLineRow.CuryID.ToString();
    strArray[5] = "|";
    strArray[6] = postLineRow.TaxZoneID == null ? "" : postLineRow.TaxZoneID.ToString();
    strArray[7] = "[";
    strArray[8] = postLineRow.BillingCycleType == null ? string.Empty : postLineRow.BillingCycleType.ToString();
    strArray[9] = "]";
    string str1 = string.Concat(strArray);
    groupKey1.Append(str1);
    nullable = postLineRow.ProjectID;
    if (nullable.HasValue && !ProjectDefaultAttribute.IsNonProject(postLineRow.ProjectID))
    {
      StringBuilder groupKey2 = this.groupKey;
      nullable = postLineRow.ProjectID;
      string str2 = nullable.ToString() + "|";
      groupKey2.Append(str2);
    }
    string empty;
    if (!postLineRow.GroupBillByLocations.GetValueOrDefault())
    {
      empty = string.Empty;
    }
    else
    {
      nullable = postLineRow.BillLocationID;
      empty = nullable.ToString();
    }
    string str3 = empty;
    if (postLineRow.BillingCycleType == "AP")
    {
      StringBuilder groupKey3 = this.groupKey;
      nullable = postLineRow.AppointmentID;
      string str4 = nullable.ToString();
      groupKey3.Append(str4);
    }
    else if (postLineRow.BillingCycleType == "SO")
    {
      StringBuilder groupKey4 = this.groupKey;
      nullable = postLineRow.SOID;
      string str5 = nullable.ToString();
      groupKey4.Append(str5);
    }
    else if (postLineRow.BillingCycleType == "TC")
      this.groupKey.Append(str3);
    else if (postLineRow.BillingCycleType == "PO")
    {
      this.groupKey.Append($"{(postLineRow.CustPORefNbr == null ? string.Empty : postLineRow.CustPORefNbr.Trim())}|{str3}");
    }
    else
    {
      if (!(postLineRow.BillingCycleType == "WO"))
        throw new PXException("The billing cycle type is not valid.");
      this.groupKey.Append($"{(postLineRow.CustWorkOrderRefNbr == null ? string.Empty : postLineRow.CustWorkOrderRefNbr.Trim())}|{str3}");
    }
    return this.groupKey.ToString();
  }

  protected virtual string GetProjectGroupKey(TPostLine postLineRow)
  {
    if (this.groupKey == null)
      this.groupKey = new StringBuilder();
    else
      this.groupKey.Clear();
    StringBuilder groupKey = this.groupKey;
    string[] strArray = new string[7];
    int? nullable = postLineRow.BranchID;
    strArray[0] = nullable.ToString();
    strArray[1] = "|";
    strArray[2] = postLineRow.DocType.ToString();
    strArray[3] = "|";
    nullable = postLineRow.SOID;
    strArray[4] = nullable.ToString();
    strArray[5] = "|";
    nullable = postLineRow.AppointmentID;
    strArray[6] = nullable.ToString();
    string str = string.Concat(strArray);
    groupKey.Append(str);
    return this.groupKey.ToString();
  }

  protected virtual void CreatePostingBatchForBillingCycle(
    Guid currentProcessID,
    int billingCycleID,
    CreateInvoiceFilter filter,
    List<TPostLine> postLineRows,
    PXQuickProcess.ActionFlow quickProcessFlow,
    bool isGenerateInvoiceScreen)
  {
    PXResultset<FSPostDoc> pxResultset = PXSelectBase<FSPostDoc, PXSelectGroupBy<FSPostDoc, Where<FSPostDoc.processID, Equal<Required<FSPostDoc.processID>>, And<FSPostDoc.billingCycleID, Equal<Required<FSPostDoc.billingCycleID>>>>, Aggregate<GroupBy<FSPostDoc.groupKey>>, OrderBy<Asc<FSPostDoc.groupKey>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) currentProcessID,
      (object) billingCycleID
    });
    if (filter.PostTo == "AA")
    {
      List<FSPostDoc> invoiceList1 = new List<FSPostDoc>();
      List<FSPostDoc> invoiceList2 = new List<FSPostDoc>();
      Decimal? invoiceTotal = new Decimal?(0M);
      foreach (PXResult<FSPostDoc> pxResult in pxResultset)
      {
        FSPostDoc fsPostDoc = PXResult<FSPostDoc>.op_Implicit(pxResult);
        this.GetInvoiceLines(currentProcessID, billingCycleID, fsPostDoc.GroupKey, true, out invoiceTotal, filter.PostTo);
        Decimal? nullable = invoiceTotal;
        Decimal num1 = 0M;
        if (nullable.GetValueOrDefault() < num1 & nullable.HasValue && fsPostDoc.PostNegBalanceToAP.GetValueOrDefault())
        {
          fsPostDoc.InvtMult = new short?((short) -1);
          invoiceList2.Add(fsPostDoc);
        }
        else
        {
          nullable = invoiceTotal;
          Decimal num2 = 0M;
          fsPostDoc.InvtMult = !(nullable.GetValueOrDefault() < num2 & nullable.HasValue) ? new short?((short) 1) : new short?((short) -1);
          invoiceList1.Add(fsPostDoc);
        }
      }
      if (invoiceList1.Count > 0)
      {
        this.CreatePostingBatchAndInvoices(currentProcessID, billingCycleID, filter.UpToDate, filter.InvoiceDate, filter.InvoiceFinPeriodID, "AR", invoiceList1, postLineRows, quickProcessFlow, isGenerateInvoiceScreen);
        invoiceList1.Clear();
      }
      if (invoiceList2.Count <= 0)
        return;
      this.CreatePostingBatchAndInvoices(currentProcessID, billingCycleID, filter.UpToDate, filter.InvoiceDate, filter.InvoiceFinPeriodID, "AP", invoiceList2, postLineRows, quickProcessFlow, isGenerateInvoiceScreen);
      invoiceList2.Clear();
    }
    else if ((filter.PostTo == "SO" || filter.PostTo == "SI") && PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
    {
      List<FSPostDoc> invoiceList = new List<FSPostDoc>();
      Decimal? invoiceTotal = new Decimal?(0M);
      foreach (PXResult<FSPostDoc> pxResult in pxResultset)
      {
        FSPostDoc fsPostDoc = PXResult<FSPostDoc>.op_Implicit(pxResult);
        this.GetInvoiceLines(currentProcessID, billingCycleID, fsPostDoc.GroupKey, true, out invoiceTotal, filter.PostTo);
        Decimal? nullable = invoiceTotal;
        Decimal num = 0M;
        fsPostDoc.InvtMult = !(nullable.GetValueOrDefault() < num & nullable.HasValue) ? new short?((short) 1) : new short?((short) -1);
        invoiceList.Add(fsPostDoc);
      }
      this.CreatePostingBatchAndInvoices(currentProcessID, billingCycleID, filter.UpToDate, filter.InvoiceDate, filter.InvoiceFinPeriodID, filter.PostTo, invoiceList, postLineRows, quickProcessFlow, isGenerateInvoiceScreen);
    }
    else
    {
      if (!(filter.PostTo == "PM"))
        return;
      List<FSPostDoc> invoiceList = new List<FSPostDoc>();
      Decimal? invoiceTotal = new Decimal?(0M);
      foreach (PXResult<FSPostDoc> pxResult in pxResultset)
      {
        FSPostDoc fsPostDoc = PXResult<FSPostDoc>.op_Implicit(pxResult);
        this.GetInvoiceLines(currentProcessID, billingCycleID, fsPostDoc.GroupKey, true, out invoiceTotal, filter.PostTo);
        Decimal? nullable = invoiceTotal;
        Decimal num = 0M;
        fsPostDoc.InvtMult = !(nullable.GetValueOrDefault() < num & nullable.HasValue) ? new short?((short) 1) : new short?((short) -1);
        invoiceList.Add(fsPostDoc);
      }
      this.CreatePostingBatchAndInvoices(currentProcessID, billingCycleID, filter.UpToDate, filter.InvoiceDate, filter.InvoiceFinPeriodID, filter.PostTo, invoiceList, postLineRows, quickProcessFlow, isGenerateInvoiceScreen);
    }
  }

  public abstract List<DocLineExt> GetInvoiceLines(
    Guid currentProcessID,
    int billingCycleID,
    string groupKey,
    bool getOnlyTotal,
    out Decimal? invoiceTotal,
    string postTo);

  public static void UpdateFSPostDoc(
    PXGraph graph,
    FSCreatedDoc fsCreatedDocRow,
    Guid currentProcessID,
    int? billingCycleID,
    string groupKey)
  {
    PXUpdate<Set<FSPostDoc.batchID, Required<FSPostDoc.batchID>, Set<FSPostDoc.postedTO, Required<FSPostDoc.postedTO>, Set<FSPostDoc.postDocType, Required<FSPostDoc.postDocType>, Set<FSPostDoc.postRefNbr, Required<FSPostDoc.postRefNbr>>>>>, FSPostDoc, Where<FSPostDoc.processID, Equal<Required<FSPostDoc.processID>>, And<FSPostDoc.billingCycleID, Equal<Required<FSPostDoc.billingCycleID>>, And<FSPostDoc.groupKey, Equal<Required<FSPostDoc.groupKey>>>>>>.Update(graph, new object[7]
    {
      (object) fsCreatedDocRow.BatchID,
      (object) fsCreatedDocRow.PostTo,
      (object) fsCreatedDocRow.CreatedDocType,
      (object) fsCreatedDocRow.CreatedRefNbr,
      (object) currentProcessID,
      (object) billingCycleID,
      (object) groupKey
    });
  }

  public static void CreatePostRegisterAndBillHistory(
    PXGraph graph,
    List<DocLineExt> docs,
    FSCreatedDoc fsCreatedDocRow,
    Guid currentProcessID)
  {
    PXCache cach1 = graph.Caches[typeof (FSPostRegister)];
    PXCache cach2 = graph.Caches[typeof (FSBillHistory)];
    foreach (DocLineExt doc in docs)
    {
      FSPostRegister fsPostRegister = new FSPostRegister();
      fsPostRegister.SrvOrdType = doc.fsAppointment == null ? doc.fsServiceOrder.SrvOrdType : doc.fsAppointment.SrvOrdType;
      fsPostRegister.RefNbr = doc.fsAppointment == null ? doc.fsServiceOrder.RefNbr : doc.fsAppointment.RefNbr;
      fsPostRegister.Type = "INVCP";
      fsPostRegister.BatchID = fsCreatedDocRow.BatchID;
      fsPostRegister.EntityType = doc.fsAppointment == null ? "SO" : "AP";
      fsPostRegister.ProcessID = new Guid?(currentProcessID);
      fsPostRegister.PostedTO = fsCreatedDocRow.PostTo;
      fsPostRegister.PostDocType = fsCreatedDocRow.CreatedDocType;
      fsPostRegister.PostRefNbr = fsCreatedDocRow.CreatedRefNbr;
      cach1.Insert((object) fsPostRegister);
      FSBillHistory fsBillHistory = new FSBillHistory();
      fsBillHistory.BatchID = fsCreatedDocRow.BatchID;
      fsBillHistory.SrvOrdType = fsPostRegister.SrvOrdType;
      fsBillHistory.ServiceOrderRefNbr = doc.fsAppointment == null ? doc.fsServiceOrder.RefNbr : doc.fsAppointment.SORefNbr;
      fsBillHistory.AppointmentRefNbr = doc.fsAppointment != null ? doc.fsAppointment.RefNbr : (string) null;
      if (fsCreatedDocRow.PostTo == "SO")
        fsBillHistory.ChildEntityType = "PXSO";
      else if (fsCreatedDocRow.PostTo == "SI")
        fsBillHistory.ChildEntityType = "PXSI";
      else if (fsCreatedDocRow.PostTo == "AR")
        fsBillHistory.ChildEntityType = "PXAR";
      else if (fsCreatedDocRow.PostTo == "AP")
        fsBillHistory.ChildEntityType = "PXAP";
      else if (fsCreatedDocRow.PostTo == "PM")
      {
        fsBillHistory.ChildEntityType = "PXPM";
      }
      else
      {
        if (!(fsCreatedDocRow.PostTo == "IN"))
          throw new NotImplementedException();
        fsBillHistory.ChildEntityType = "PXIS";
      }
      fsBillHistory.ChildDocType = fsCreatedDocRow.CreatedDocType;
      fsBillHistory.ChildRefNbr = fsCreatedDocRow.CreatedRefNbr;
      cach2.Insert((object) fsBillHistory);
    }
    cach1.Persist((PXDBOperation) 2);
    cach2.Persist((PXDBOperation) 2);
  }

  public static void CreateNewPostDocs(
    PXGraph graph,
    List<DocLineExt> docs,
    FSCreatedDoc createdDoc,
    Guid currentProcessID)
  {
    PXCache cach = graph.Caches[typeof (FSPostDoc)];
    foreach (DocLineExt doc in docs)
    {
      FSPostDoc fsPostDoc1 = doc.fsPostDoc;
      FSPostDoc fsPostDoc2 = new FSPostDoc()
      {
        AppointmentID = (int?) doc.fsAppointment?.AppointmentID,
        BatchID = createdDoc.BatchID,
        BillingCycleID = fsPostDoc1.BillingCycleID,
        DocLineRef = fsPostDoc1.DocLineRef,
        EntityType = fsPostDoc1.EntityType,
        GroupKey = fsPostDoc1.GroupKey,
        INDocLineRef = fsPostDoc1.INDocLineRef,
        InvtMult = fsPostDoc1.InvtMult,
        PostDocType = createdDoc.CreatedDocType,
        PostedTO = createdDoc.PostTo,
        PostNegBalanceToAP = new bool?(),
        PostOrderType = (string) null,
        PostOrderTypeNegativeBalance = (string) null,
        PostRefNbr = createdDoc.CreatedRefNbr,
        ProcessID = new Guid?(currentProcessID),
        RowIndex = fsPostDoc1.RowIndex,
        SOID = (int?) doc.fsServiceOrder?.SOID
      };
      cach.Insert((object) fsPostDoc2);
    }
    cach.Persist((PXDBOperation) 2);
  }

  public virtual void UpdatePostInfoAndPostDet(
    ServiceOrderEntry soGraph,
    List<DocLineExt> docLinesWithPostInfo,
    FSPostBatch fsPostBatchRow,
    PostInfoEntry graphPostInfoEntry,
    PXCache<FSPostDet> cacheFSPostDet,
    FSCreatedDoc fsCreatedDocRow,
    FSCreatedDoc fsINCreatedDocRow = null)
  {
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    foreach (DocLineExt docLineExt in docLinesWithPostInfo)
    {
      IDocLine docLine = docLineExt.docLine;
      FSPostDoc fsPostDoc = docLineExt.fsPostDoc;
      FSPostInfo fsPostInfo = docLineExt.fsPostInfo;
      FSPostDet fsPostDet1 = new FSPostDet();
      FSPostDet fsPostDet2 = (FSPostDet) null;
      bool flag;
      if (fsPostInfo == null || !fsPostInfo.PostID.HasValue)
      {
        fsPostInfo = new FSPostInfo();
        flag = true;
      }
      else
        flag = false;
      if (fsPostDoc.DocLineRef is PX.Objects.SO.SOLine)
      {
        PX.Objects.SO.SOLine docLineRef = (PX.Objects.SO.SOLine) fsPostDoc.DocLineRef;
        fsPostInfo.SOPosted = new bool?(true);
        if (fsCreatedDocRow == null)
        {
          fsPostInfo.SOOrderType = docLineRef.OrderType;
          fsPostInfo.SOOrderNbr = docLineRef.OrderNbr;
        }
        else
        {
          fsPostInfo.SOOrderType = fsCreatedDocRow.CreatedDocType;
          fsPostInfo.SOOrderNbr = fsCreatedDocRow.CreatedRefNbr;
        }
        fsPostInfo.SOLineNbr = docLineRef.LineNbr;
        fsPostDet1.SOPosted = fsPostInfo.SOPosted;
        fsPostDet1.SOOrderType = fsPostInfo.SOOrderType;
        fsPostDet1.SOOrderNbr = fsPostInfo.SOOrderNbr;
        fsPostDet1.SOLineNbr = fsPostInfo.SOLineNbr;
      }
      else if (fsPostDoc.DocLineRef is PX.Objects.AR.ARTran && (fsPostBatchRow.PostTo == "AA" || fsPostBatchRow.PostTo == "AR"))
      {
        PX.Objects.AR.ARTran docLineRef = (PX.Objects.AR.ARTran) fsPostDoc.DocLineRef;
        fsPostInfo.ARPosted = new bool?(true);
        if (fsCreatedDocRow == null)
        {
          fsPostInfo.ARDocType = docLineRef.TranType;
          fsPostInfo.ARRefNbr = docLineRef.RefNbr;
        }
        else
        {
          fsPostInfo.ARDocType = fsCreatedDocRow.CreatedDocType;
          fsPostInfo.ARRefNbr = fsCreatedDocRow.CreatedRefNbr;
        }
        fsPostInfo.ARLineNbr = docLineRef.LineNbr;
        fsPostDet1.ARPosted = fsPostInfo.ARPosted;
        fsPostDet1.ARDocType = fsPostInfo.ARDocType;
        fsPostDet1.ARRefNbr = fsPostInfo.ARRefNbr;
        fsPostDet1.ARLineNbr = fsPostInfo.ARLineNbr;
      }
      else if (fsPostDoc.DocLineRef is PX.Objects.AR.ARTran && fsPostBatchRow.PostTo == "SI")
      {
        PX.Objects.AR.ARTran docLineRef = (PX.Objects.AR.ARTran) fsPostDoc.DocLineRef;
        fsPostInfo.SOInvPosted = new bool?(true);
        fsPostInfo.SOInvDocType = docLineRef.TranType;
        fsPostInfo.SOInvRefNbr = docLineRef.RefNbr;
        fsPostInfo.SOInvLineNbr = docLineRef.LineNbr;
        fsPostDet1.SOInvPosted = fsPostInfo.SOInvPosted;
        fsPostDet1.SOInvDocType = fsPostInfo.SOInvDocType;
        fsPostDet1.SOInvRefNbr = fsPostInfo.SOInvRefNbr;
        fsPostDet1.SOInvLineNbr = fsPostInfo.SOInvLineNbr;
      }
      else if (fsPostDoc.DocLineRef is APTran)
      {
        APTran docLineRef = (APTran) fsPostDoc.DocLineRef;
        fsPostInfo.APPosted = new bool?(true);
        if (fsCreatedDocRow == null)
        {
          fsPostInfo.APDocType = docLineRef.TranType;
          fsPostInfo.APRefNbr = docLineRef.RefNbr;
        }
        else
        {
          fsPostInfo.APDocType = fsCreatedDocRow.CreatedDocType;
          fsPostInfo.APRefNbr = fsCreatedDocRow.CreatedRefNbr;
        }
        fsPostInfo.APLineNbr = docLineRef.LineNbr;
        fsPostDet1.APPosted = fsPostInfo.APPosted;
        fsPostDet1.APDocType = fsPostInfo.APDocType;
        fsPostDet1.APRefNbr = fsPostInfo.APRefNbr;
        fsPostDet1.APLineNbr = fsPostInfo.APLineNbr;
      }
      else if (fsPostDoc.DocLineRef is PMTran)
      {
        PMTran docLineRef = (PMTran) fsPostDoc.DocLineRef;
        fsPostInfo.PMPosted = new bool?(true);
        if (fsCreatedDocRow == null)
        {
          fsPostInfo.PMDocType = docLineRef.TranType;
          fsPostInfo.PMRefNbr = docLineRef.RefNbr;
        }
        else
        {
          fsPostInfo.PMDocType = fsCreatedDocRow.CreatedDocType;
          fsPostInfo.PMRefNbr = fsCreatedDocRow.CreatedRefNbr;
        }
        fsPostInfo.PMTranID = docLineRef.TranID;
        fsPostDet1.PMPosted = fsPostInfo.PMPosted;
        fsPostDet1.PMDocType = fsPostInfo.PMDocType;
        fsPostDet1.PMRefNbr = fsPostInfo.PMRefNbr;
        fsPostDet1.PMTranID = fsPostInfo.PMTranID;
        if (fsINCreatedDocRow != null && fsPostDoc.INDocLineRef != null && fsPostDoc.INDocLineRef is INTran)
        {
          fsPostDet2 = new FSPostDet();
          INTran inDocLineRef = (INTran) fsPostDoc.INDocLineRef;
          fsPostInfo.INPosted = new bool?(true);
          if (fsCreatedDocRow == null)
          {
            fsPostInfo.INDocType = inDocLineRef.TranType;
            fsPostInfo.INRefNbr = inDocLineRef.RefNbr;
          }
          else
          {
            fsPostInfo.INDocType = fsINCreatedDocRow.CreatedDocType;
            fsPostInfo.INRefNbr = fsINCreatedDocRow.CreatedRefNbr;
          }
          fsPostInfo.INLineNbr = inDocLineRef.LineNbr;
          fsPostDet2.INPosted = fsPostInfo.INPosted;
          fsPostDet2.INDocType = fsPostInfo.INDocType;
          fsPostDet2.INRefNbr = fsPostInfo.INRefNbr;
          fsPostDet2.INLineNbr = fsPostInfo.INLineNbr;
        }
      }
      if (docLine.SourceTable == "FSAppointmentDet")
        fsPostInfo.AppointmentID = docLine.DocID;
      else if (docLine.SourceTable == "FSSODet")
        fsPostInfo.SOID = docLine.DocID;
      if (flag)
        ((PXSelectBase<FSPostInfo>) graphPostInfoEntry.PostInfoRecords).Current = ((PXSelectBase<FSPostInfo>) graphPostInfoEntry.PostInfoRecords).Insert(fsPostInfo);
      else
        ((PXSelectBase<FSPostInfo>) graphPostInfoEntry.PostInfoRecords).Current = ((PXSelectBase<FSPostInfo>) graphPostInfoEntry.PostInfoRecords).Update(fsPostInfo);
      ((PXAction) graphPostInfoEntry.Save).Press();
      FSPostInfo current = ((PXSelectBase<FSPostInfo>) graphPostInfoEntry.PostInfoRecords).Current;
      fsPostDet1.BatchID = fsPostBatchRow.BatchID;
      fsPostDet1.PostID = current.PostID;
      cacheFSPostDet.Insert(fsPostDet1);
      if (fsPostDet2 != null)
      {
        fsPostDet2.BatchID = fsPostBatchRow.BatchID;
        fsPostDet2.PostID = current.PostID;
        cacheFSPostDet.Insert(fsPostDet2);
      }
      if (flag)
      {
        if (docLine.SourceTable == "FSAppointmentDet")
          PXUpdate<Set<FSAppointmentDet.postID, Required<FSAppointmentDet.postID>>, FSAppointmentDet, Where<FSAppointmentDet.appDetID, Equal<Required<FSAppointmentDet.appDetID>>>>.Update(((PXCache) cacheFSPostDet).Graph, new object[2]
          {
            (object) current.PostID,
            (object) docLine.LineID
          });
        else if (docLine.SourceTable == "FSSODet")
          PXUpdate<Set<FSSODet.postID, Required<FSSODet.postID>>, FSSODet, Where<FSSODet.sODetID, Equal<Required<FSSODet.sODetID>>>>.Update(((PXCache) cacheFSPostDet).Graph, new object[2]
          {
            (object) current.PostID,
            (object) docLine.LineID
          });
      }
      this.UpdateSourcePostDoc(soGraph, instance, cacheFSPostDet, fsPostBatchRow, fsPostDoc);
    }
    ((PXCache) cacheFSPostDet).Persist((PXDBOperation) 2);
  }

  public virtual IInvoiceGraph CreateInvoiceGraph(string targetScreen)
  {
    return InvoiceHelper.CreateInvoiceGraph(targetScreen);
  }

  public abstract void UpdateSourcePostDoc(
    ServiceOrderEntry soGraph,
    AppointmentEntry apptGraph,
    PXCache<FSPostDet> cacheFSPostDet,
    FSPostBatch fsPostBatchRow,
    FSPostDoc fsPostDocRow);

  public virtual void DeallocateItemsThatAreBeingPosted(
    ServiceOrderEntry graph,
    List<DocLineExt> docLines,
    bool postingAppointments)
  {
    List<FSSODetSplit> splitsToDeallocate = new List<FSSODetSplit>();
    IEnumerable<IGrouping<(string, string), DocLineExt>> groupings = docLines.GroupBy<DocLineExt, (string, string)>((Func<DocLineExt, (string, string)>) (x => (x.fsServiceOrder.SrvOrdType, x.fsServiceOrder.RefNbr)));
    if (!postingAppointments)
    {
      foreach (IEnumerable<DocLineExt> source in groupings)
      {
        FSServiceOrder fsServiceOrder = source.First<DocLineExt>().fsServiceOrder;
        foreach (PXResult<FSSODetSplit> pxResult in PXSelectBase<FSSODetSplit, PXSelect<FSSODetSplit, Where<FSSODetSplit.srvOrdType, Equal<Required<FSSODetSplit.srvOrdType>>, And<FSSODetSplit.refNbr, Equal<Required<FSSODetSplit.refNbr>>, And<FSSODetSplit.completed, Equal<False>, And<FSSODetSplit.pOCreate, Equal<False>, And<FSSODetSplit.inventoryID, IsNotNull>>>>>, OrderBy<Asc<FSSODetSplit.lineNbr, Asc<FSSODetSplit.splitLineNbr>>>>.Config>.Select((PXGraph) graph, new object[2]
        {
          (object) fsServiceOrder.SrvOrdType,
          (object) fsServiceOrder.RefNbr
        }))
        {
          FSSODetSplit fssoDetSplit = PXResult<FSSODetSplit>.op_Implicit(pxResult);
          FSSODetSplit copy = (FSSODetSplit) ((PXSelectBase) graph.Splits).Cache.CreateCopy((object) fssoDetSplit);
          copy.BaseQty = new Decimal?(0M);
          splitsToDeallocate.Add(copy);
        }
      }
    }
    else
    {
      PXCache pxCache1 = (PXCache) new PXCache<FSAppointmentDet>((PXGraph) this);
      PXCache pxCache2 = (PXCache) new PXCache<FSApptLineSplit>((PXGraph) this);
      int? nullable1 = new int?();
      FSSODet soLine = (FSSODet) null;
      List<FSAppointmentDet> source1 = new List<FSAppointmentDet>();
      List<FSApptLineSplit> source2 = new List<FSApptLineSplit>();
      foreach (IGrouping<(string, string), DocLineExt> source3 in groupings)
      {
        FSServiceOrder fsServiceOrder = source3.First<DocLineExt>().fsServiceOrder;
        int? nullable2 = new int?();
        soLine = (FSSODet) null;
        bool flag = false;
        source1.Clear();
        source2.Clear();
        foreach (PXResult<FSSODetSplit> pxResult in PXSelectBase<FSSODetSplit, PXSelect<FSSODetSplit, Where<FSSODetSplit.srvOrdType, Equal<Required<FSSODetSplit.srvOrdType>>, And<FSSODetSplit.refNbr, Equal<Required<FSSODetSplit.refNbr>>, And<FSSODetSplit.completed, Equal<False>, And<FSSODetSplit.pOCreate, Equal<False>, And<FSSODetSplit.inventoryID, IsNotNull>>>>>, OrderBy<Asc<FSSODetSplit.lineNbr, Asc<FSSODetSplit.splitLineNbr>>>>.Config>.Select((PXGraph) graph, new object[2]
        {
          (object) fsServiceOrder.SrvOrdType,
          (object) fsServiceOrder.RefNbr
        }))
        {
          FSSODetSplit soSplit = PXResult<FSSODetSplit>.op_Implicit(pxResult);
          if (nullable2.HasValue)
          {
            int? nullable3 = nullable2;
            int? lineNbr = soSplit.LineNbr;
            if (nullable3.GetValueOrDefault() == lineNbr.GetValueOrDefault() & nullable3.HasValue == lineNbr.HasValue)
              goto label_36;
          }
          soLine = source3.Where<DocLineExt>((Func<DocLineExt, bool>) (x =>
          {
            int? lineNbr1 = x.fsSODet.LineNbr;
            int? lineNbr2 = soSplit.LineNbr;
            return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
          })).FirstOrDefault<DocLineExt>()?.fsSODet;
          if (soLine != null)
          {
            flag = SharedFunctions.IsLotSerialRequired(((PXSelectBase) graph.ServiceOrderDetails).Cache, soSplit.InventoryID);
            nullable2 = soSplit.LineNbr;
            source1.Clear();
            source2.Clear();
            foreach (FSAppointmentDet fsAppointmentDet in source3.Where<DocLineExt>((Func<DocLineExt, bool>) (x =>
            {
              int? soDetId1 = x.fsAppointmentDet.SODetID;
              int? soDetId2 = soLine.SODetID;
              return soDetId1.GetValueOrDefault() == soDetId2.GetValueOrDefault() & soDetId1.HasValue == soDetId2.HasValue;
            })).Select<DocLineExt, FSAppointmentDet>((Func<DocLineExt, FSAppointmentDet>) (x => x.fsAppointmentDet)))
              source1.Add((FSAppointmentDet) pxCache1.CreateCopy((object) fsAppointmentDet));
            if (flag)
            {
              foreach (FSAppointmentDet fsAppointmentDet in source1)
              {
                foreach (FSApptLineSplit selectChild in PXParentAttribute.SelectChildren(pxCache2, (object) fsAppointmentDet, typeof (FSAppointmentDet)))
                  source2.Add((FSApptLineSplit) pxCache2.CreateCopy((object) selectChild));
              }
            }
          }
          else
            continue;
label_36:
          if (flag)
          {
            foreach (FSApptLineSplit fsApptLineSplit1 in source2.Where<FSApptLineSplit>((Func<FSApptLineSplit, bool>) (x => !string.IsNullOrEmpty(x.LotSerialNbr) && string.Equals(x.LotSerialNbr, soSplit.LotSerialNbr, StringComparison.OrdinalIgnoreCase))))
            {
              Decimal? baseQty1 = fsApptLineSplit1.BaseQty;
              Decimal? baseQty2 = soSplit.BaseQty;
              if (baseQty1.GetValueOrDefault() <= baseQty2.GetValueOrDefault() & baseQty1.HasValue & baseQty2.HasValue)
              {
                FSSODetSplit fssoDetSplit = soSplit;
                Decimal? baseQty3 = fssoDetSplit.BaseQty;
                baseQty1 = fsApptLineSplit1.BaseQty;
                fssoDetSplit.BaseQty = baseQty3.HasValue & baseQty1.HasValue ? new Decimal?(baseQty3.GetValueOrDefault() - baseQty1.GetValueOrDefault()) : new Decimal?();
                fsApptLineSplit1.BaseQty = new Decimal?(0M);
              }
              else
              {
                FSApptLineSplit fsApptLineSplit2 = fsApptLineSplit1;
                baseQty1 = fsApptLineSplit2.BaseQty;
                Decimal? baseQty4 = soSplit.BaseQty;
                fsApptLineSplit2.BaseQty = baseQty1.HasValue & baseQty4.HasValue ? new Decimal?(baseQty1.GetValueOrDefault() - baseQty4.GetValueOrDefault()) : new Decimal?();
                soSplit.BaseQty = new Decimal?(0M);
              }
              FSAppointmentDet fsAppointmentDet = FSAppointmentDet.PK.Find((PXGraph) graph, fsApptLineSplit1.SrvOrdType, fsApptLineSplit1.ApptNbr, fsApptLineSplit1.LineNbr);
              if (fsAppointmentDet != null && !(fsAppointmentDet.SrvOrdType != fsApptLineSplit1.SrvOrdType) && !(fsAppointmentDet.RefNbr != fsApptLineSplit1.ApptNbr))
              {
                int? lineNbr3 = fsAppointmentDet.LineNbr;
                int? lineNbr4 = fsApptLineSplit1.LineNbr;
                if (lineNbr3.GetValueOrDefault() == lineNbr4.GetValueOrDefault() & lineNbr3.HasValue == lineNbr4.HasValue)
                  continue;
              }
              throw new PXException("The {0} record was not found.", new object[1]
              {
                (object) DACHelper.GetDisplayName(typeof (FSAppointmentDet))
              });
            }
          }
          else
          {
            foreach (FSAppointmentDet fsAppointmentDet1 in source1.Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (x =>
            {
              Decimal? baseEffTranQty = x.BaseEffTranQty;
              Decimal num = 0M;
              return baseEffTranQty.GetValueOrDefault() > num & baseEffTranQty.HasValue;
            })))
            {
              Decimal? baseEffTranQty = fsAppointmentDet1.BaseEffTranQty;
              Decimal? baseQty5 = soSplit.BaseQty;
              if (baseEffTranQty.GetValueOrDefault() <= baseQty5.GetValueOrDefault() & baseEffTranQty.HasValue & baseQty5.HasValue)
              {
                FSSODetSplit fssoDetSplit = soSplit;
                Decimal? baseQty6 = fssoDetSplit.BaseQty;
                baseEffTranQty = fsAppointmentDet1.BaseEffTranQty;
                fssoDetSplit.BaseQty = baseQty6.HasValue & baseEffTranQty.HasValue ? new Decimal?(baseQty6.GetValueOrDefault() - baseEffTranQty.GetValueOrDefault()) : new Decimal?();
                fsAppointmentDet1.BaseEffTranQty = new Decimal?(0M);
              }
              else
              {
                FSAppointmentDet fsAppointmentDet2 = fsAppointmentDet1;
                baseEffTranQty = fsAppointmentDet2.BaseEffTranQty;
                Decimal? baseQty7 = soSplit.BaseQty;
                fsAppointmentDet2.BaseEffTranQty = baseEffTranQty.HasValue & baseQty7.HasValue ? new Decimal?(baseEffTranQty.GetValueOrDefault() - baseQty7.GetValueOrDefault()) : new Decimal?();
                soSplit.BaseQty = new Decimal?(0M);
              }
            }
          }
          splitsToDeallocate.Add(soSplit);
        }
      }
    }
    FSAllocationProcess.DeallocateServiceOrderSplits(graph, splitsToDeallocate, false);
  }

  protected virtual void IncludeReviewInvoiceBatchesAction()
  {
    if (PXSelectBase<FSPostBatch, PXSelect<FSPostBatch, Where<FSPostBatch.status, Equal<FSPostBatch.status.temporary>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()).Count == 0)
      ((PXAction) this.openReviewTemporaryBatch).SetVisible(false);
    else
      ((PXAction) this.openReviewTemporaryBatch).SetVisible(true);
  }

  protected virtual void HideOrShowInvoiceActions(
    PXCache cache,
    CreateInvoiceFilter createInvoiceFilterRow)
  {
    bool flag1 = createInvoiceFilterRow.PostTo == "SO";
    int num = createInvoiceFilterRow.PostTo == "AA" ? 1 : 0;
    bool flag2 = false;
    bool flag3 = createInvoiceFilterRow.PostTo == "PM";
    PXUIFieldAttribute.SetVisible<CreateInvoiceFilter.prepareInvoice>(cache, (object) createInvoiceFilterRow, flag1);
    PXUIFieldAttribute.SetVisible<CreateInvoiceFilter.emailSalesOrder>(cache, (object) createInvoiceFilterRow, flag1);
    PXUIFieldAttribute.SetVisible<CreateInvoiceFilter.sOQuickProcess>(cache, (object) createInvoiceFilterRow, flag1);
    PXUIFieldAttribute.SetVisible<CreateInvoiceFilter.releaseInvoice>(cache, (object) createInvoiceFilterRow, flag2 | flag1);
    PXUIFieldAttribute.SetVisible<CreateInvoiceFilter.emailInvoice>(cache, (object) createInvoiceFilterRow, flag2);
    PXUIFieldAttribute.SetVisible<CreateInvoiceFilter.releaseBill>(cache, (object) createInvoiceFilterRow, flag2);
    PXUIFieldAttribute.SetVisible<CreateInvoiceFilter.payBill>(cache, (object) createInvoiceFilterRow, flag2);
    PXUIFieldAttribute.SetVisible<CreateInvoiceFilter.ignoreBillingCycles>(cache, (object) createInvoiceFilterRow, !flag3);
  }

  protected DateTime? GetCutOffDate(
    PXGraph graph,
    DateTime? docDate,
    int? customerID,
    string srvOrdType)
  {
    if (!docDate.HasValue)
      return new DateTime?();
    string str = string.Empty;
    int? nullable1 = new int?();
    int? nullable2 = new int?();
    PXResult<FSCustomerBillingSetup, FSSrvOrdType> pxResult = (PXResult<FSCustomerBillingSetup, FSSrvOrdType>) PXResultset<FSCustomerBillingSetup>.op_Implicit(PXSelectBase<FSCustomerBillingSetup, PXSelectJoin<FSCustomerBillingSetup, LeftJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSCustomerBillingSetup.srvOrdType>>, CrossJoin<FSSetup>>, Where<FSCustomerBillingSetup.customerID, Equal<Required<FSCustomerBillingSetup.customerID>>, And<Where2<Where<FSSetup.customerMultipleBillingOptions, Equal<True>, And<FSCustomerBillingSetup.srvOrdType, Equal<Required<FSCustomerBillingSetup.srvOrdType>>>>, Or<Where<FSSetup.customerMultipleBillingOptions, Equal<False>, And<FSCustomerBillingSetup.srvOrdType, IsNull>>>>>>>.Config>.Select(graph, new object[2]
    {
      (object) customerID,
      (object) srvOrdType
    }));
    FSCustomerBillingSetup customerBillingSetup = PXResult<FSCustomerBillingSetup, FSSrvOrdType>.op_Implicit(pxResult);
    FSSrvOrdType fsSrvOrdType = PXResult<FSCustomerBillingSetup, FSSrvOrdType>.op_Implicit(pxResult) ?? FSSrvOrdType.PK.Find(graph, srvOrdType);
    if (customerBillingSetup != null && fsSrvOrdType != null && fsSrvOrdType.PostTo != "PM")
    {
      if (customerBillingSetup.FrequencyType != "NO")
      {
        str = customerBillingSetup.FrequencyType;
        nullable1 = customerBillingSetup.WeeklyFrequency;
        nullable2 = customerBillingSetup.MonthlyFrequency;
      }
      else
      {
        FSBillingCycle fsBillingCycle = FSBillingCycle.PK.Find(graph, customerBillingSetup.BillingCycleID);
        if (fsBillingCycle != null && fsBillingCycle.BillingCycleType == "TC")
        {
          str = fsBillingCycle.TimeCycleType;
          nullable1 = fsBillingCycle.TimeCycleWeekDay;
          nullable2 = fsBillingCycle.TimeCycleDayOfMonth;
        }
      }
    }
    else if (fsSrvOrdType != null && fsSrvOrdType.PostTo == "PM")
    {
      str = "NO";
      nullable1 = new int?(0);
      nullable2 = new int?(0);
    }
    if (!docDate.HasValue)
      return new DateTime?();
    ref DateTime? local1 = ref docDate;
    DateTime dateTime1 = docDate.Value;
    int year1 = dateTime1.Year;
    dateTime1 = docDate.Value;
    int month1 = dateTime1.Month;
    dateTime1 = docDate.Value;
    int day1 = dateTime1.Day;
    DateTime dateTime2 = new DateTime(year1, month1, day1);
    local1 = new DateTime?(dateTime2);
    DateTime? cutOffDate = docDate;
    switch (str)
    {
      case "WK":
        int num1 = nullable1.Value;
        dateTime1 = docDate.Value;
        int dayOfWeek1 = (int) dateTime1.DayOfWeek;
        int num2 = num1 - dayOfWeek1;
        dateTime1 = docDate.Value;
        int dayOfWeek2 = (int) dateTime1.DayOfWeek;
        int? nullable3 = nullable1;
        int valueOrDefault1 = nullable3.GetValueOrDefault();
        if (dayOfWeek2 > valueOrDefault1 & nullable3.HasValue)
          num2 += 7;
        ref DateTime? local2 = ref cutOffDate;
        dateTime1 = docDate.Value;
        DateTime dateTime3 = dateTime1.AddDays((double) num2);
        local2 = new DateTime?(dateTime3);
        break;
      case "MT":
        dateTime1 = docDate.Value;
        int day2 = dateTime1.Day;
        int? nullable4 = nullable2;
        int valueOrDefault2 = nullable4.GetValueOrDefault();
        if (day2 <= valueOrDefault2 & nullable4.HasValue)
        {
          dateTime1 = docDate.Value;
          int year2 = dateTime1.Year;
          dateTime1 = docDate.Value;
          int month2 = dateTime1.Month;
          int num3 = DateTime.DaysInMonth(year2, month2);
          nullable4 = nullable2;
          int num4 = num3;
          if (nullable4.GetValueOrDefault() <= num4 & nullable4.HasValue)
          {
            ref DateTime? local3 = ref cutOffDate;
            dateTime1 = docDate.Value;
            DateTime dateTime4 = dateTime1.AddDays((double) (nullable2.Value - docDate.Value.Day));
            local3 = new DateTime?(dateTime4);
            break;
          }
          ref DateTime? local4 = ref cutOffDate;
          dateTime1 = docDate.Value;
          DateTime dateTime5 = dateTime1.AddDays((double) (num3 - docDate.Value.Day));
          local4 = new DateTime?(dateTime5);
          break;
        }
        ref DateTime? local5 = ref cutOffDate;
        dateTime1 = docDate.Value;
        dateTime1 = dateTime1.AddDays((double) (nullable2.Value - docDate.Value.Day));
        DateTime dateTime6 = dateTime1.AddMonths(1);
        local5 = new DateTime?(dateTime6);
        break;
    }
    return cutOffDate;
  }

  public class PostBatchShared
  {
    public PostBatchEntry PostBatchEntryGraph;
    public FSPostBatch FSPostBatchRow;
  }
}
