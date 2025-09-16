// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.CreateInvoiceByServiceOrderPost
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class CreateInvoiceByServiceOrderPost : 
  CreateInvoiceBase<CreateInvoiceByServiceOrderPost, ServiceOrderToPost>
{
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<ServiceOrderToPost, CreateInvoiceFilter, Where<True, Equal<True>>, OrderBy<Asc<ServiceOrderToPost.billingCycleID, Asc<ServiceOrderToPost.groupKey>>>> PostLines;
  public PXAction<CreateInvoiceFilter> viewPostBatch;

  public virtual IEnumerable postLines()
  {
    CreateInvoiceByServiceOrderPost graph = this;
    foreach (PXResult<ServiceOrderToPost> pxResult in ((PXSelectBase<ServiceOrderToPost>) new FbqlSelect<SelectFromBase<ServiceOrderToPost, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FSBillingCycle>.On<BqlOperand<FSBillingCycle.billingCycleID, IBqlInt>.IsEqual<ServiceOrderToPost.billingCycleID>>>, FbqlJoins.Left<PX.Objects.AR.Customer>.On<BqlOperand<PX.Objects.AR.Customer.bAccountID, IBqlInt>.IsEqual<ServiceOrderToPost.billCustomerID>>.SingleTableOnly>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<FSSetup.filterInvoicingManually>, Equal<False>>>>>.Or<BqlOperand<Current<CreateInvoiceFilter.loadData>, IBqlBool>.IsEqual<True>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, IsNull>>>>.Or<Match<PX.Objects.AR.Customer, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CreateInvoiceFilter.postTo>, Equal<ListField_PostTo.AR_AP>>>>, And<BqlOperand<ServiceOrderToPost.postTo, IBqlString>.IsEqual<ListField_PostTo.AR>>>, Or<BqlOperand<Current<CreateInvoiceFilter.postTo>, IBqlString>.IsEqual<ListField_PostTo.SO>>>, And<BqlOperand<ServiceOrderToPost.postTo, IBqlString>.IsEqual<ListField_PostTo.SO>>>, Or<BqlOperand<Current<CreateInvoiceFilter.postTo>, IBqlString>.IsEqual<ListField_PostTo.SI>>>, And<BqlOperand<ServiceOrderToPost.postTo, IBqlString>.IsEqual<ListField_PostTo.SI>>>, Or<BqlOperand<Current<CreateInvoiceFilter.postTo>, IBqlString>.IsEqual<ListField_PostTo_CreateInvoice.PM>>>>.And<BqlOperand<ServiceOrderToPost.postTo, IBqlString>.IsEqual<ListField_PostTo_CreateInvoice.PM>>>>, And<BqlOperand<IsNull<ServiceOrderToPost.billingBy, FSBillingCycle.billingBy>, IBqlString>.IsEqual<ListField_Billing_By.ServiceOrder>>>, And<BqlOperand<FSServiceOrder.pendingAPARSOPost, IBqlBool>.IsEqual<True>>>, And<BqlOperand<FSServiceOrder.postedBy, IBqlString>.IsNull>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CreateInvoiceFilter.billingCycleID>, IsNull>>>>.Or<BqlOperand<ServiceOrderToPost.billingCycleID, IBqlInt>.IsEqual<BqlField<CreateInvoiceFilter.billingCycleID, IBqlInt>.FromCurrent>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CreateInvoiceFilter.customerID>, IsNull>>>>.Or<BqlOperand<ServiceOrderToPost.billCustomerID, IBqlInt>.IsEqual<BqlField<CreateInvoiceFilter.customerID, IBqlInt>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CreateInvoiceFilter.ignoreBillingCycles>, Equal<False>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSServiceOrder.orderDate, GreaterEqual<BqlField<CreateInvoiceFilter.fromDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlOperand<FSServiceOrder.orderDate, IBqlDateTime>.IsLessEqual<BqlField<CreateInvoiceFilter.upToDateWithTimeZone, IBqlDateTime>.FromCurrent>>>>>, ServiceOrderToPost>.View((PXGraph) graph)).Select(Array.Empty<object>()))
    {
      ServiceOrderToPost postLineRow = PXResult<ServiceOrderToPost>.op_Implicit(pxResult);
      postLineRow.GroupKey = graph.GetGroupKey(postLineRow);
      postLineRow.CutOffDate = graph.GetCutOffDate((PXGraph) graph, postLineRow.OrderDate, postLineRow.BillCustomerID, postLineRow.SrvOrdType);
      if (!((PXSelectBase<CreateInvoiceFilter>) graph.Filter).Current.IgnoreBillingCycles.GetValueOrDefault())
      {
        DateTime? cutOffDate = postLineRow.CutOffDate;
        DateTime? upToDate = ((PXSelectBase<CreateInvoiceFilter>) graph.Filter).Current.UpToDate;
        if ((cutOffDate.HasValue & upToDate.HasValue ? (cutOffDate.GetValueOrDefault() <= upToDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          continue;
      }
      yield return (object) pxResult;
    }
  }

  [PXUIField(DisplayName = "")]
  public virtual IEnumerable ViewPostBatch(PXAdapter adapter)
  {
    if (((PXSelectBase<ServiceOrderToPost>) this.PostLines).Current != null)
    {
      ServiceOrderToPost current = ((PXSelectBase<ServiceOrderToPost>) this.PostLines).Current;
      PostBatchMaint instance = PXGraph.CreateInstance<PostBatchMaint>();
      if (current.BatchID.HasValue)
      {
        ((PXSelectBase<FSPostBatch>) instance.BatchRecords).Current = PXResultset<FSPostBatch>.op_Implicit(((PXSelectBase<FSPostBatch>) instance.BatchRecords).Search<FSPostBatch.batchID>((object) current.BatchID, Array.Empty<object>()));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Service Order Nbr.")]
  [PXSelector(typeof (Search<FSServiceOrder.sOID, Where<ServiceOrderToPost.srvOrdType, Equal<Current<ServiceOrderToPost.srvOrdType>>>>), SubstituteKey = typeof (FSServiceOrder.refNbr))]
  protected virtual void ServiceOrderToPost_SOID_CacheAttached(PXCache sender)
  {
  }

  protected override void _(PX.Data.Events.RowSelected<CreateInvoiceFilter> e)
  {
    if (e.Row == null)
      return;
    base._(e);
    CreateInvoiceFilter row = e.Row;
    bool flag = string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<CreateInvoiceFilter.invoiceFinPeriodID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CreateInvoiceFilter>>) e).Cache, (object) row));
    ((PXProcessing<ServiceOrderToPost>) this.PostLines).SetProcessAllEnabled(flag);
    ((PXProcessing<ServiceOrderToPost>) this.PostLines).SetProcessEnabled(flag);
  }

  public CreateInvoiceByServiceOrderPost()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CreateInvoiceByServiceOrderPost.\u003C\u003Ec__DisplayClass6_0 cDisplayClass60 = new CreateInvoiceByServiceOrderPost.\u003C\u003Ec__DisplayClass6_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass60.\u003C\u003E4__this = this;
    this.billingBy = "SO";
    // ISSUE: reference to a compiler-generated field
    cDisplayClass60.graphCreateInvoiceByServiceOrderPost = (CreateInvoiceByServiceOrderPost) null;
    if (!WebConfig.ParallelProcessingDisabled)
      ((PXProcessingBase<ServiceOrderToPost>) this.PostLines).ParallelProcessingOptions = (Action<PXParallelProcessingOptions>) (settings =>
      {
        settings.IsEnabled = true;
        settings.BatchSize = 1000;
        settings.SplitToBatches = new Func<IList, PXParallelProcessingOptions, IEnumerable<(int, int)>>(this.SplitToBatches);
      });
    // ISSUE: method pointer
    ((PXProcessingBase<ServiceOrderToPost>) this.PostLines).SetProcessDelegate(new PXProcessingBase<ServiceOrderToPost>.ProcessListDelegate((object) cDisplayClass60, __methodptr(\u003C\u002Ector\u003Eb__1)));
    OpenPeriodAttribute.SetValidatePeriod<CreateInvoiceFilter.invoiceFinPeriodID>(((PXSelectBase) this.Filter).Cache, (object) null, ((PXGraph) this).IsContractBasedAPI || ((PXGraph) this).IsImport || ((PXGraph) this).IsExport || ((PXGraph) this).UnattendedMode ? PeriodValidation.DefaultUpdate : PeriodValidation.DefaultSelectUpdate);
  }

  private IEnumerable<(int, int)> SplitToBatches(IList list, PXParallelProcessingOptions options)
  {
    int num = 0;
    int end = 0;
    for (; num < list.Count; num = end + 1)
    {
      end = Math.Min(num + options.BatchSize - 1, list.Count - 1) - 1;
      ServiceOrderToPost serviceOrderToPost1;
      ServiceOrderToPost serviceOrderToPost2;
      int? billingCycleId1;
      int? billingCycleId2;
      do
      {
        ++end;
        serviceOrderToPost1 = (ServiceOrderToPost) list[end];
        serviceOrderToPost2 = end + 1 < list.Count ? (ServiceOrderToPost) list[end + 1] : (ServiceOrderToPost) null;
        billingCycleId1 = serviceOrderToPost1.BillingCycleID;
        billingCycleId2 = (int?) serviceOrderToPost2?.BillingCycleID;
      }
      while (billingCycleId1.GetValueOrDefault() == billingCycleId2.GetValueOrDefault() & billingCycleId1.HasValue == billingCycleId2.HasValue && serviceOrderToPost1.GroupKey == serviceOrderToPost2?.GroupKey);
      yield return (num, end);
    }
  }

  public override List<DocLineExt> GetInvoiceLines(
    Guid currentProcessID,
    int billingCycleID,
    string groupKey,
    bool getOnlyTotal,
    out Decimal? invoiceTotal,
    string postTo)
  {
    PXGraph pxGraph = new PXGraph();
    if (getOnlyTotal)
    {
      FSSODet fssoDet = PXResultset<FSSODet>.op_Implicit(PXSelectBase<FSSODet, PXSelectJoinGroupBy<FSSODet, InnerJoin<FSServiceOrder, On<FSServiceOrder.srvOrdType, Equal<FSSODet.srvOrdType>, And<FSServiceOrder.refNbr, Equal<FSSODet.refNbr>>>, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSServiceOrder.srvOrdType>>, InnerJoin<FSPostDoc, On<FSPostDoc.sOID, Equal<FSSODet.sOID>, And<FSPostDoc.entityType, Equal<ListField_PostDoc_EntityType.Service_Order>>>, LeftJoin<FSPostInfo, On<FSPostInfo.postID, Equal<FSSODet.postID>>>>>>, Where<FSPostDoc.processID, Equal<Required<FSPostDoc.processID>>, And<FSPostDoc.billingCycleID, Equal<Required<FSPostDoc.billingCycleID>>, And<FSPostDoc.groupKey, Equal<Required<FSPostDoc.groupKey>>, And<FSSODet.lineType, NotEqual<FSLineType.Comment>, And<FSSODet.lineType, NotEqual<FSLineType.Instruction>, And<FSSODet.status, NotEqual<FSSODet.ListField_Status_SODet.Canceled>, And<FSSODet.isPrepaid, Equal<False>, And<FSSODet.isBillable, Equal<True>, And<Where2<Where<FSSODet.postID, IsNull>, Or<Where<FSPostInfo.aRPosted, Equal<False>, And<FSPostInfo.aPPosted, Equal<False>, And<FSPostInfo.sOPosted, Equal<False>, And<FSPostInfo.sOInvPosted, Equal<False>, And<Where2<Where<Required<FSPostBatch.postTo>, NotEqual<ListField_PostTo.SO>>, Or<Where<Required<FSPostBatch.postTo>, Equal<ListField_PostTo.SO>, And<FSPostInfo.iNPosted, Equal<False>>>>>>>>>>>>>>>>>>>>>, Aggregate<Sum<FSSODet.curyBillableTranAmt>>>.Config>.Select(pxGraph, new object[5]
      {
        (object) currentProcessID,
        (object) billingCycleID,
        (object) groupKey,
        (object) postTo,
        (object) postTo
      }));
      invoiceTotal = fssoDet.CuryBillableTranAmt;
      return (List<DocLineExt>) null;
    }
    invoiceTotal = new Decimal?();
    PXResultset<FSSODet> pxResultset = PXSelectBase<FSSODet, PXSelectJoin<FSSODet, InnerJoin<FSServiceOrder, On<FSServiceOrder.srvOrdType, Equal<FSSODet.srvOrdType>, And<FSServiceOrder.refNbr, Equal<FSSODet.refNbr>>>, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSServiceOrder.srvOrdType>>, InnerJoin<FSPostDoc, On<FSPostDoc.sOID, Equal<FSSODet.sOID>, And<FSPostDoc.entityType, Equal<ListField_PostDoc_EntityType.Service_Order>>>, LeftJoin<FSPostInfo, On<FSPostInfo.postID, Equal<FSSODet.postID>>, LeftJoin<PMTask, On<PMTask.projectID, Equal<FSSODet.projectID>, And<PMTask.taskID, Equal<FSSODet.projectTaskID>>>>>>>>, Where<FSPostDoc.processID, Equal<Required<FSPostDoc.processID>>, And<FSPostDoc.billingCycleID, Equal<Required<FSPostDoc.billingCycleID>>, And<FSPostDoc.groupKey, Equal<Required<FSPostDoc.groupKey>>, And<FSSODet.lineType, NotEqual<FSLineType.Comment>, And<FSSODet.lineType, NotEqual<FSLineType.Instruction>, And<FSSODet.status, NotEqual<FSSODet.ListField_Status_SODet.Canceled>, And<FSSODet.isPrepaid, Equal<False>, And<FSSODet.isBillable, Equal<True>, And<Where2<Where<FSSODet.postID, IsNull>, Or<Where<FSPostInfo.aRPosted, Equal<False>, And<FSPostInfo.aPPosted, Equal<False>, And<FSPostInfo.sOPosted, Equal<False>, And<FSPostInfo.sOInvPosted, Equal<False>, And<Where2<Where<Required<FSPostBatch.postTo>, NotEqual<ListField_PostTo.SO>>, Or<Where<Required<FSPostBatch.postTo>, Equal<ListField_PostTo.SO>, And<FSPostInfo.iNPosted, Equal<False>>>>>>>>>>>>>>>>>>>>>, OrderBy<Asc<FSServiceOrder.orderDate, Asc<FSSODet.sOID, Asc<FSSODet.sODetID>>>>>.Config>.Select(pxGraph, new object[5]
    {
      (object) currentProcessID,
      (object) billingCycleID,
      (object) groupKey,
      (object) postTo,
      (object) postTo
    });
    List<DocLineExt> invoiceLines = new List<DocLineExt>();
    foreach (PXResult<FSSODet, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, PMTask> soDetLine in pxResultset)
      invoiceLines.Add(new DocLineExt(soDetLine));
    return invoiceLines;
  }

  public override void UpdateSourcePostDoc(
    ServiceOrderEntry soGraph,
    AppointmentEntry apptGraph,
    PXCache<FSPostDet> cacheFSPostDet,
    FSPostBatch fsPostBatchRow,
    FSPostDoc fsPostDocRow)
  {
    if (fsPostBatchRow == null || fsPostDocRow == null || !fsPostDocRow.SOID.HasValue)
      throw new ArgumentNullException();
    FSServiceOrder fsServiceOrder = ((PXSelectBase<FSServiceOrder>) soGraph.ServiceOrderRecords).Current = FSServiceOrder.UK.Find((PXGraph) soGraph, fsPostDocRow.SOID);
    if (fsServiceOrder == null)
      throw new PXException("The service order was not found.");
    if (!fsServiceOrder.PendingAPARSOPost.GetValueOrDefault() || fsServiceOrder.PostedBy != null)
      return;
    FSServiceOrder copy = (FSServiceOrder) ((PXSelectBase) soGraph.ServiceOrderRecords).Cache.CreateCopy((object) fsServiceOrder);
    copy.PostedBy = "SO";
    copy.PendingAPARSOPost = new bool?(false);
    copy.Billed = new bool?(true);
    copy.BillingBy = ((PXSelectBase<FSBillingCycle>) soGraph.BillingCycleRelated).Current.BillingBy;
    ((PXSelectBase<FSServiceOrder>) soGraph.ServiceOrderRecords).Update(copy);
    ((PXSelectBase) soGraph.ServiceOrderRecords).Cache.SetValue<FSServiceOrder.finPeriodID>((object) copy, (object) fsPostBatchRow.FinPeriodID);
    soGraph.SkipTaxCalcAndSave();
  }
}
