// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.CreateInvoiceByAppointmentPost
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.GL;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.FS;

public class CreateInvoiceByAppointmentPost : 
  CreateInvoiceBase<CreateInvoiceByAppointmentPost, AppointmentToPost>
{
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<AppointmentToPost, CreateInvoiceFilter, Where<True, Equal<True>>> PostLines;
  public PXAction<CreateInvoiceFilter> viewPostBatch;

  public virtual IEnumerable postLines()
  {
    PXView pxView = new PXView((PXGraph) this, false, ((PXSelectBase) new FbqlSelect<SelectFromBase<AppointmentToPost, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<FSServiceContract>.On<BqlOperand<FSServiceContract.serviceContractID, IBqlInt>.IsEqual<AppointmentToPost.serviceContractID>>>, FbqlJoins.Left<PX.Objects.AR.Customer>.On<BqlOperand<PX.Objects.AR.Customer.bAccountID, IBqlInt>.IsEqual<AppointmentToPost.billCustomerID>>.SingleTableOnly>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<FSSetup.filterInvoicingManually>, Equal<False>>>>>.Or<BqlOperand<Current<CreateInvoiceFilter.loadData>, IBqlBool>.IsEqual<True>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, IsNull>>>>.Or<Match<PX.Objects.AR.Customer, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSServiceContract.serviceContractID, IsNull>>>>.Or<BqlOperand<FSServiceContract.billingType, IBqlString>.IsNotEqual<ListField.ServiceContractBillingType.standardizedBillings>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CreateInvoiceFilter.postTo>, Equal<ListField_PostTo.AR_AP>>>>, And<BqlOperand<AppointmentToPost.postTo, IBqlString>.IsEqual<ListField_PostTo.AR>>>, Or<BqlOperand<Current<CreateInvoiceFilter.postTo>, IBqlString>.IsEqual<ListField_PostTo.SO>>>, And<BqlOperand<AppointmentToPost.postTo, IBqlString>.IsEqual<ListField_PostTo.SO>>>, Or<BqlOperand<Current<CreateInvoiceFilter.postTo>, IBqlString>.IsEqual<ListField_PostTo.SI>>>, And<BqlOperand<AppointmentToPost.postTo, IBqlString>.IsEqual<ListField_PostTo.SI>>>, Or<BqlOperand<Current<CreateInvoiceFilter.postTo>, IBqlString>.IsEqual<ListField_PostTo_CreateInvoice.PM>>>>.And<BqlOperand<AppointmentToPost.postTo, IBqlString>.IsEqual<ListField_PostTo_CreateInvoice.PM>>>>, And<BqlOperand<AppointmentToPost.billingBy, IBqlString>.IsEqual<ListField_Billing_By.Appointment>>>, And<BqlOperand<FSAppointment.pendingAPARSOPost, IBqlBool>.IsEqual<True>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<AppointmentToPost.postedBy, Equal<ListField_Billing_By.Appointment>>>>>.Or<BqlOperand<AppointmentToPost.postedBy, IBqlString>.IsNull>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CreateInvoiceFilter.billingCycleID>, IsNull>>>>.Or<BqlOperand<AppointmentToPost.billingCycleID, IBqlInt>.IsEqual<BqlField<CreateInvoiceFilter.billingCycleID, IBqlInt>.FromCurrent>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CreateInvoiceFilter.customerID>, IsNull>>>>.Or<BqlOperand<AppointmentToPost.billCustomerID, IBqlInt>.IsEqual<BqlField<CreateInvoiceFilter.customerID, IBqlInt>.FromCurrent>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSAppointment.status, Equal<ListField.AppointmentStatus.completed>>>>>.Or<BqlOperand<FSAppointment.status, IBqlString>.IsEqual<ListField.AppointmentStatus.closed>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CreateInvoiceFilter.ignoreBillingCycles>, Equal<False>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSAppointment.actualDateTimeEnd, GreaterEqual<BqlField<CreateInvoiceFilter.fromDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlOperand<FSAppointment.actualDateTimeEnd, IBqlDateTime>.IsLessEqual<BqlField<CreateInvoiceFilter.upToDateWithTimeZone, IBqlDateTime>.FromCurrent>>>>>, AppointmentToPost>.View((PXGraph) this)).View.BqlSelect);
    int startRow = PXView.StartRow;
    int num = 0;
    object[] currents = PXView.Currents;
    object[] parameters = PXView.Parameters;
    object[] searches = PXView.Searches;
    string[] sortColumns = PXView.SortColumns;
    bool[] descendings = PXView.Descendings;
    PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
    ref int local1 = ref startRow;
    int maximumRows = PXView.MaximumRows;
    ref int local2 = ref num;
    List<object> objectList = pxView.Select(currents, parameters, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2);
    PXView.StartRow = 0;
    List<AppointmentToPost> appointmentToPostList = new List<AppointmentToPost>();
    foreach (AppointmentToPost postLineRow in GraphHelper.RowCast<AppointmentToPost>((IEnumerable) objectList))
    {
      postLineRow.GroupKey = this.GetGroupKey(postLineRow);
      postLineRow.CutOffDate = this.GetCutOffDate((PXGraph) this, postLineRow.ActualDateTimeEnd, postLineRow.BillCustomerID, postLineRow.SrvOrdType);
      if (!((PXSelectBase<CreateInvoiceFilter>) this.Filter).Current.IgnoreBillingCycles.GetValueOrDefault())
      {
        DateTime? cutOffDate = postLineRow.CutOffDate;
        DateTime? upToDate = ((PXSelectBase<CreateInvoiceFilter>) this.Filter).Current.UpToDate;
        if ((cutOffDate.HasValue & upToDate.HasValue ? (cutOffDate.GetValueOrDefault() <= upToDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          continue;
      }
      appointmentToPostList.Add(postLineRow);
    }
    return (IEnumerable) appointmentToPostList;
  }

  [PXUIField(DisplayName = "")]
  public virtual IEnumerable ViewPostBatch(PXAdapter adapter)
  {
    if (((PXSelectBase<AppointmentToPost>) this.PostLines).Current != null)
    {
      AppointmentToPost current = ((PXSelectBase<AppointmentToPost>) this.PostLines).Current;
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

  [PXDBIdentity]
  [PXUIField(DisplayName = "Appointment Nbr.")]
  [PXSelector(typeof (Search<FSAppointment.appointmentID, Where<FSAppointment.srvOrdType, Equal<Current<AppointmentToPost.srvOrdType>>>>), SubstituteKey = typeof (FSAppointment.refNbr))]
  protected virtual void AppointmentToPost_AppointmentID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Service Order Nbr.")]
  [PXSelector(typeof (Search<FSAppointment.sOID, Where<AppointmentToPost.srvOrdType, Equal<Current<AppointmentToPost.srvOrdType>>>>), SubstituteKey = typeof (AppointmentToPost.soRefNbr))]
  protected virtual void AppointmentToPost_SOID_CacheAttached(PXCache sender)
  {
  }

  protected override void _(PX.Data.Events.RowSelected<CreateInvoiceFilter> e)
  {
    if (e.Row == null)
      return;
    base._(e);
    CreateInvoiceFilter row = e.Row;
    bool flag = string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<CreateInvoiceFilter.invoiceFinPeriodID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CreateInvoiceFilter>>) e).Cache, (object) row));
    ((PXProcessing<AppointmentToPost>) this.PostLines).SetProcessAllEnabled(flag);
    ((PXProcessing<AppointmentToPost>) this.PostLines).SetProcessEnabled(flag);
  }

  public CreateInvoiceByAppointmentPost()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CreateInvoiceByAppointmentPost.\u003C\u003Ec__DisplayClass7_0 cDisplayClass70 = new CreateInvoiceByAppointmentPost.\u003C\u003Ec__DisplayClass7_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass70.\u003C\u003E4__this = this;
    this.billingBy = "AP";
    // ISSUE: reference to a compiler-generated field
    cDisplayClass70.graphCreateInvoiceByAppointmentPost = (CreateInvoiceByAppointmentPost) null;
    if (!WebConfig.ParallelProcessingDisabled)
      ((PXProcessingBase<AppointmentToPost>) this.PostLines).ParallelProcessingOptions = (Action<PXParallelProcessingOptions>) (settings =>
      {
        settings.IsEnabled = true;
        settings.BatchSize = 1000;
        settings.SplitToBatches = new Func<IList, PXParallelProcessingOptions, IEnumerable<(int, int)>>(this.SplitToBatches);
      });
    // ISSUE: method pointer
    ((PXProcessingBase<AppointmentToPost>) this.PostLines).SetProcessDelegate(new PXProcessingBase<AppointmentToPost>.ProcessListDelegate((object) cDisplayClass70, __methodptr(\u003C\u002Ector\u003Eb__1)));
    OpenPeriodAttribute.SetValidatePeriod<CreateInvoiceFilter.invoiceFinPeriodID>(((PXSelectBase) this.Filter).Cache, (object) null, ((PXGraph) this).IsContractBasedAPI || ((PXGraph) this).IsImport || ((PXGraph) this).IsExport || ((PXGraph) this).UnattendedMode ? PeriodValidation.DefaultUpdate : PeriodValidation.DefaultSelectUpdate);
  }

  private IEnumerable<(int, int)> SplitToBatches(IList list, PXParallelProcessingOptions options)
  {
    int num = 0;
    int end = 0;
    for (; num < list.Count; num = end + 1)
    {
      end = Math.Min(num + options.BatchSize - 1, list.Count - 1) - 1;
      AppointmentToPost appointmentToPost1;
      AppointmentToPost appointmentToPost2;
      int? billingCycleId1;
      int? billingCycleId2;
      do
      {
        ++end;
        appointmentToPost1 = (AppointmentToPost) list[end];
        appointmentToPost2 = end + 1 < list.Count ? (AppointmentToPost) list[end + 1] : (AppointmentToPost) null;
        billingCycleId1 = appointmentToPost1.BillingCycleID;
        billingCycleId2 = (int?) appointmentToPost2?.BillingCycleID;
      }
      while (billingCycleId1.GetValueOrDefault() == billingCycleId2.GetValueOrDefault() & billingCycleId1.HasValue == billingCycleId2.HasValue && appointmentToPost1.GroupKey == appointmentToPost2?.GroupKey);
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
      FSAppointmentDet fsAppointmentDet1 = PXResultset<FSAppointmentDet>.op_Implicit(PXSelectBase<FSAppointmentDet, PXSelectJoinGroupBy<FSAppointmentDet, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentDet.appointmentID>>, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSServiceOrder.srvOrdType>>, InnerJoin<FSPostDoc, On<FSPostDoc.appointmentID, Equal<FSAppointment.appointmentID>, And<FSPostDoc.entityType, Equal<ListField_PostDoc_EntityType.Appointment>>>, LeftJoin<FSPostInfo, On<FSPostInfo.postID, Equal<FSAppointmentDet.postID>>>>>>>, Where<FSPostDoc.processID, Equal<Required<FSPostDoc.processID>>, And<FSPostDoc.billingCycleID, Equal<Required<FSPostDoc.billingCycleID>>, And<FSPostDoc.groupKey, Equal<Required<FSPostDoc.groupKey>>, And<FSAppointmentDet.lineType, NotEqual<FSLineType.Comment>, And<FSAppointmentDet.lineType, NotEqual<FSLineType.Instruction>, And<FSAppointmentDet.isCanceledNotPerformed, NotEqual<True>, And<FSAppointmentDet.lineType, NotEqual<ListField_LineType_Pickup_Delivery.Pickup_Delivery>, And<FSAppointmentDet.isPrepaid, Equal<False>, And<FSAppointmentDet.isBillable, Equal<True>, And<Where2<Where<FSAppointmentDet.postID, IsNull>, Or<Where<FSPostInfo.aRPosted, Equal<False>, And<FSPostInfo.aPPosted, Equal<False>, And<FSPostInfo.sOPosted, Equal<False>, And<FSPostInfo.sOInvPosted, Equal<False>, And<Where2<Where<Required<FSPostBatch.postTo>, NotEqual<ListField_PostTo.SO>>, Or<Where<Required<FSPostBatch.postTo>, Equal<ListField_PostTo.SO>, And<FSPostInfo.iNPosted, Equal<False>>>>>>>>>>>>>>>>>>>>>>, Aggregate<Sum<FSAppointmentDet.billableTranAmt>>>.Config>.Select(pxGraph, new object[5]
      {
        (object) currentProcessID,
        (object) billingCycleID,
        (object) groupKey,
        (object) postTo,
        (object) postTo
      }));
      invoiceTotal = fsAppointmentDet1.BillableTranAmt;
      FSAppointmentDet fsAppointmentDet2 = PXResultset<FSAppointmentDet>.op_Implicit(PXSelectBase<FSAppointmentDet, PXSelectJoinGroupBy<FSAppointmentDet, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentDet.appointmentID>>, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSServiceOrder.srvOrdType>>, InnerJoin<FSPostDoc, On<FSPostDoc.appointmentID, Equal<FSAppointment.appointmentID>, And<FSPostDoc.entityType, Equal<ListField_PostDoc_EntityType.Appointment>>>, LeftJoin<FSPostInfo, On<FSPostInfo.postID, Equal<FSAppointmentDet.postID>>>>>>>, Where<FSAppointmentDet.lineType, Equal<ListField_LineType_Pickup_Delivery.Pickup_Delivery>, And<FSPostDoc.processID, Equal<Required<FSPostDoc.processID>>, And<FSPostDoc.billingCycleID, Equal<Required<FSPostDoc.billingCycleID>>, And<FSPostDoc.groupKey, Equal<Required<FSPostDoc.groupKey>>, And<Where2<Where<FSAppointmentDet.postID, IsNull>, Or<Where<FSPostInfo.aRPosted, Equal<False>, And<FSPostInfo.aPPosted, Equal<False>, And<FSPostInfo.sOPosted, Equal<False>, And<FSPostInfo.sOInvPosted, Equal<False>, And<Where2<Where<Required<FSPostBatch.postTo>, NotEqual<ListField_PostTo.SO>>, Or<Where<Required<FSPostBatch.postTo>, Equal<ListField_PostTo.SO>, And<FSPostInfo.iNPosted, Equal<False>>>>>>>>>>>>>>>>>, Aggregate<Sum<FSAppointmentDet.billableTranAmt>>>.Config>.Select(pxGraph, new object[5]
      {
        (object) currentProcessID,
        (object) billingCycleID,
        (object) groupKey,
        (object) postTo,
        (object) postTo
      }));
      ref Decimal? local = ref invoiceTotal;
      Decimal? nullable1 = invoiceTotal;
      Decimal? nullable2 = fsAppointmentDet2.BillableTranAmt;
      Decimal valueOrDefault = nullable2.GetValueOrDefault();
      Decimal? nullable3;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable3 = nullable2;
      }
      else
        nullable3 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault);
      local = nullable3;
      return (List<DocLineExt>) null;
    }
    invoiceTotal = new Decimal?();
    PXResultset<FSAppointmentDet> pxResultset = PXSelectBase<FSAppointmentDet, PXSelectJoin<FSAppointmentDet, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentDet.appointmentID>>, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSServiceOrder.srvOrdType>>, InnerJoin<FSPostDoc, On<FSPostDoc.appointmentID, Equal<FSAppointment.appointmentID>, And<FSPostDoc.entityType, Equal<ListField_PostDoc_EntityType.Appointment>>>, LeftJoin<FSPostInfo, On<FSPostInfo.postID, Equal<FSAppointmentDet.postID>>, LeftJoin<FSSODet, On<FSSODet.srvOrdType, Equal<FSServiceOrder.srvOrdType>, And<FSSODet.refNbr, Equal<FSServiceOrder.refNbr>, And<FSSODet.sODetID, Equal<FSAppointmentDet.sODetID>>>>, LeftJoin<PMTask, On<PMTask.projectID, Equal<FSAppointmentDet.projectID>, And<PMTask.taskID, Equal<FSAppointmentDet.projectTaskID>>>>>>>>>>, Where<FSPostDoc.processID, Equal<Required<FSPostDoc.processID>>, And<FSPostDoc.billingCycleID, Equal<Required<FSPostDoc.billingCycleID>>, And<FSPostDoc.groupKey, Equal<Required<FSPostDoc.groupKey>>, And<FSAppointmentDet.lineType, NotEqual<FSLineType.Comment>, And<FSAppointmentDet.lineType, NotEqual<FSLineType.Instruction>, And<FSAppointmentDet.isCanceledNotPerformed, NotEqual<True>, And<FSAppointmentDet.lineType, NotEqual<ListField_LineType_Pickup_Delivery.Pickup_Delivery>, And<FSAppointmentDet.isPrepaid, Equal<False>, And<FSAppointmentDet.isBillable, Equal<True>, And<Where2<Where<FSAppointmentDet.postID, IsNull>, Or<Where<FSPostInfo.aRPosted, Equal<False>, And<FSPostInfo.aPPosted, Equal<False>, And<FSPostInfo.sOPosted, Equal<False>, And<FSPostInfo.sOInvPosted, Equal<False>, And<Where2<Where<Required<FSPostBatch.postTo>, NotEqual<ListField_PostTo.SO>>, Or<Where<Required<FSPostBatch.postTo>, Equal<ListField_PostTo.SO>, And<FSPostInfo.iNPosted, Equal<False>>>>>>>>>>>>>>>>>>>>>>, OrderBy<Asc<FSAppointment.executionDate, Asc<FSAppointmentDet.appointmentID, Asc<FSAppointmentDet.appDetID>>>>>.Config>.Select(pxGraph, new object[5]
    {
      (object) currentProcessID,
      (object) billingCycleID,
      (object) groupKey,
      (object) postTo,
      (object) postTo
    });
    List<DocLineExt> invoiceLines = new List<DocLineExt>();
    foreach (PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, FSSODet, PMTask> appointmentDetLine in pxResultset)
      invoiceLines.Add(new DocLineExt(appointmentDetLine));
    foreach (PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, PMTask> appointmentDetLine in PXSelectBase<FSAppointmentDet, PXSelectJoin<FSAppointmentDet, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentDet.appointmentID>>, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSServiceOrder.srvOrdType>>, InnerJoin<FSPostDoc, On<FSPostDoc.appointmentID, Equal<FSAppointment.appointmentID>, And<FSPostDoc.entityType, Equal<ListField_PostDoc_EntityType.Appointment>>>, LeftJoin<FSPostInfo, On<FSPostInfo.postID, Equal<FSAppointmentDet.postID>>, LeftJoin<PMTask, On<PMTask.projectID, Equal<FSAppointmentDet.projectID>, And<PMTask.taskID, Equal<FSAppointmentDet.projectTaskID>>>>>>>>>, Where<FSAppointmentDet.lineType, Equal<ListField_LineType_Pickup_Delivery.Pickup_Delivery>, And<FSPostDoc.processID, Equal<Required<FSPostDoc.processID>>, And<FSPostDoc.billingCycleID, Equal<Required<FSPostDoc.billingCycleID>>, And<FSPostDoc.groupKey, Equal<Required<FSPostDoc.groupKey>>, And<Where2<Where<FSAppointmentDet.postID, IsNull>, Or<Where<FSPostInfo.aRPosted, Equal<False>, And<FSPostInfo.aPPosted, Equal<False>, And<FSPostInfo.sOPosted, Equal<False>, And<FSPostInfo.sOInvPosted, Equal<False>, And<Where2<Where<Required<FSPostBatch.postTo>, NotEqual<ListField_PostTo.SO>>, Or<Where<Required<FSPostBatch.postTo>, Equal<ListField_PostTo.SO>, And<FSPostInfo.iNPosted, Equal<False>>>>>>>>>>>>>>>>>, OrderBy<Asc<FSAppointment.executionDate, Asc<FSAppointmentDet.appointmentID, Asc<FSAppointmentDet.appDetID>>>>>.Config>.Select(pxGraph, new object[5]
    {
      (object) currentProcessID,
      (object) billingCycleID,
      (object) groupKey,
      (object) postTo,
      (object) postTo
    }))
    {
      DocLineExt docLineExt = new DocLineExt(appointmentDetLine);
      docLineExt.docLine.AcctID = this.Get_TranAcctID_DefaultValue((PXGraph) this, docLineExt.fsSrvOrdType.SalesAcctSource, docLineExt.docLine.InventoryID, docLineExt.docLine.SiteID, docLineExt.fsServiceOrder);
      invoiceLines.Add(docLineExt);
    }
    return invoiceLines;
  }

  public override void UpdateSourcePostDoc(
    ServiceOrderEntry soGraph,
    AppointmentEntry apptGraph,
    PXCache<FSPostDet> cacheFSPostDet,
    FSPostBatch fsPostBatchRow,
    FSPostDoc fsPostDocRow)
  {
    ((PXGraph) apptGraph).Clear((PXClearOption) 3);
    ((PXGraph) soGraph).Clear((PXClearOption) 3);
    FSAppointment fsAppointment = ((PXSelectBase<FSAppointment>) apptGraph.AppointmentRecords).Current = FSAppointment.UK.Find((PXGraph) apptGraph, fsPostDocRow.AppointmentID);
    if (fsAppointment == null)
      throw new PXException("The appointment you select cannot be found. Refresh the appointment and try again.");
    if (!fsAppointment.PendingAPARSOPost.GetValueOrDefault())
      return;
    FSAppointment copy1 = (FSAppointment) ((PXSelectBase) apptGraph.AppointmentRecords).Cache.CreateCopy((object) fsAppointment);
    copy1.PostingStatusAPARSO = "PT";
    copy1.PendingAPARSOPost = new bool?(false);
    ((SelectedEntityEvent<FSAppointment>) PXEntityEventBase<FSAppointment>.Container<FSAppointment.Events>.Select((Expression<Func<FSAppointment.Events, PXEntityEvent<FSAppointment.Events>>>) (ev => ev.AppointmentPosted))).FireOn((PXGraph) apptGraph, copy1);
    ((PXSelectBase) apptGraph.AppointmentRecords).Cache.Update((object) copy1);
    ((PXSelectBase) apptGraph.AppointmentRecords).Cache.SetValue<FSAppointment.finPeriodID>((object) copy1, (object) fsPostBatchRow.FinPeriodID);
    apptGraph.SkipTaxCalcAndSave();
    FSServiceOrder fsServiceOrder = ((PXSelectBase<FSServiceOrder>) soGraph.ServiceOrderRecords).Current = FSServiceOrder.UK.Find((PXGraph) soGraph, fsPostDocRow.SOID);
    if (fsServiceOrder == null)
      throw new PXException("The service order was not found.");
    if (fsServiceOrder.PostedBy != null)
      return;
    FSServiceOrder copy2 = (FSServiceOrder) ((PXSelectBase) soGraph.ServiceOrderRecords).Cache.CreateCopy((object) fsServiceOrder);
    copy2.PostedBy = "AP";
    copy2.BillingBy = "AP";
    copy2.PendingAPARSOPost = new bool?(false);
    ((PXSelectBase<FSServiceOrder>) soGraph.ServiceOrderRecords).Update(copy2);
    soGraph.SkipTaxCalcAndSave();
  }

  public virtual int? Get_TranAcctID_DefaultValue(
    PXGraph graph,
    string salesAcctSource,
    int? inventoryID,
    int? siteID,
    FSServiceOrder fsServiceOrderRow)
  {
    return ServiceOrderEntry.Get_TranAcctID_DefaultValueInt(graph, salesAcctSource, inventoryID, siteID, fsServiceOrderRow);
  }
}
