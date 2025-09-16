// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_POOrderEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.FS;

public class SM_POOrderEntry : PXGraphExtension<POOrderEntry>
{
  [PXHidden]
  public PXSelect<FSServiceOrder> serviceOrderView;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<FSSODetSplit, InnerJoin<INItemPlan, On<INItemPlan.planID, Equal<FSSODetSplit.planID>, And<INItemPlan.planID, Equal<Required<PX.Objects.PO.POLine.planID>>>>>> FSSODetSplitFixedDemand;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<FSSODet, InnerJoin<FSSODetSplit, On<FSSODetSplit.srvOrdType, Equal<FSSODet.srvOrdType>, And<FSSODetSplit.refNbr, Equal<FSSODet.refNbr>, And<FSSODetSplit.lineNbr, Equal<FSSODet.lineNbr>>>>, InnerJoin<INItemPlan, On<INItemPlan.planID, Equal<FSSODetSplit.planID>, And<INItemPlan.planID, Equal<Required<PX.Objects.PO.POLine.planID>>>>>>> FSSODetFixedDemand;
  [PXHidden]
  public PXSelect<FSAppointment> AppointmentView;
  [PXHidden]
  public PXSelect<FSAppointmentDet> AppointmentLineView;
  [PXHidden]
  public PXSelect<FSApptLineSplit> apptSplitView;
  [PXHidden]
  public PXSelect<FSSrvOrdType> fsSrvOrdTypeView;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXDBTimestamp]
  public virtual void _(PX.Data.Events.CacheAttached<FSSODet.Tstamp> e)
  {
  }

  [PXDBTimestamp]
  public virtual void _(PX.Data.Events.CacheAttached<FSAppointmentDet.Tstamp> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.PO.POOrder> e)
  {
    PX.Objects.PO.POOrder row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.PO.POOrder>>) e).Cache;
    if (row.OrderType != "RO" || e.TranStatus != null || e.Operation != 1)
      return;
    string valueOriginal = (string) cache.GetValueOriginal<PX.Objects.PO.POOrder.status>((object) row);
    if (row.Status != "M" && row.Status != "L" && row.Status != "V")
      FSPOReceiptProcess.UpdateSrvOrdLinePOStatus(cache.Graph, row);
    if (!(row.Status == "N") || !(valueOriginal != row.Status))
      return;
    FSPOReceiptProcess.UpdateSrvOrdApptLineUnitCost(cache.Graph, row);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.PO.POOrder> e)
  {
    PX.Objects.PO.POOrder row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<PX.Objects.PO.POOrder>>) e).Cache;
    if (row == null || row.OrderType != "RO")
      return;
    if (!((PXGraph) this.Base).Views.Caches.Contains(typeof (FSAppointment)))
      ((PXGraph) this.Base).Views.Caches.Add(typeof (FSAppointment));
    if (!((PXGraph) this.Base).Views.Caches.Contains(typeof (FSAppointmentDet)))
      ((PXGraph) this.Base).Views.Caches.Add(typeof (FSAppointmentDet));
    if (!((PXGraph) this.Base).Views.Caches.Contains(typeof (FSApptLineSplit)))
      ((PXGraph) this.Base).Views.Caches.Add(typeof (FSApptLineSplit));
    if (!((PXGraph) this.Base).Views.Caches.Contains(typeof (FSServiceOrder)))
      ((PXGraph) this.Base).Views.Caches.Add(typeof (FSServiceOrder));
    if (!((PXGraph) this.Base).Views.Caches.Contains(typeof (FSSODet)))
      ((PXGraph) this.Base).Views.Caches.Add(typeof (FSSODet));
    PXSelect<FSSODetSplit> fsSODetSplitFixedDemand = new PXSelect<FSSODetSplit>((PXGraph) this.Base);
    if (!((PXGraph) this.Base).Views.Caches.Contains(typeof (FSSODetSplit)))
      ((PXGraph) this.Base).Views.Caches.Add(typeof (FSSODetSplit));
    PXSelect<INItemPlan> inItemPlans = new PXSelect<INItemPlan>((PXGraph) this.Base);
    if (!((PXGraph) this.Base).Views.Caches.Contains(typeof (INItemPlan)))
      ((PXGraph) this.Base).Views.Caches.Add(typeof (INItemPlan));
    List<FSSODetSplit> list = GraphHelper.RowCast<FSSODetSplit>((IEnumerable) PXSelectBase<FSSODetSplit, PXSelect<FSSODetSplit, Where<FSSODetSplit.pOType, Equal<Required<FSSODetSplit.pOType>>, And<FSSODetSplit.pONbr, Equal<Required<FSSODetSplit.pONbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) row.OrderType,
      (object) row.OrderNbr
    })).ToList<FSSODetSplit>();
    if (!list.Any<FSSODetSplit>())
      return;
    ServiceOrderEntry instance = PXGraph.CreateInstance<ServiceOrderEntry>();
    foreach (FSSODetSplit fssoDetSplit in list)
    {
      ((PXSelectBase<FSServiceOrder>) this.serviceOrderView).Current = (FSServiceOrder) PXParentAttribute.SelectParent(((PXSelectBase) fsSODetSplitFixedDemand).Cache, (object) fssoDetSplit, typeof (FSServiceOrder));
      ((PXSelectBase<FSSODet>) this.FSSODetFixedDemand).Current = (FSSODet) PXParentAttribute.SelectParent(((PXSelectBase) fsSODetSplitFixedDemand).Cache, (object) fssoDetSplit, typeof (FSSODet));
      ((PXSelectBase<FSSODetSplit>) fsSODetSplitFixedDemand).Current = fssoDetSplit;
      FSPOReceiptProcess.RemovePOReferenceInSrvOrdLine((PXGraph) this.Base, instance, (PXSelectBase<FSSODet>) this.FSSODetFixedDemand, (PXSelectBase<FSSODetSplit>) fsSODetSplitFixedDemand, (PXSelectBase<FSAppointment>) this.AppointmentView, (PXSelectBase<FSAppointmentDet>) this.AppointmentLineView, (PXSelectBase<INItemPlan>) inItemPlans, ((PXSelectBase<FSSODet>) this.FSSODetFixedDemand).Current, ((PXSelectBase<FSSODetSplit>) fsSODetSplitFixedDemand).Current);
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.PO.POLine> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.PO.POLine row = e.Row;
    FSSODet topFirst = PXSelectBase<FSSODet, PXViewOf<FSSODet>.BasedOn<SelectFromBase<FSSODet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POLine>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POLine.orderType, Equal<FSSODet.poType>>>>, And<BqlOperand<PX.Objects.PO.POLine.orderNbr, IBqlString>.IsEqual<FSSODet.poNbr>>>>.And<BqlOperand<PX.Objects.PO.POLine.lineNbr, IBqlInt>.IsEqual<FSSODet.poLineNbr>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODet.poType, Equal<P.AsString>>>>, And<BqlOperand<FSSODet.poNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<FSSODet.poLineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) row.OrderType,
      (object) row.OrderNbr,
      (object) row.LineNbr
    }).TopFirst;
    if (topFirst == null)
      return;
    int? projectId1 = topFirst.ProjectID;
    int? projectId2 = row.ProjectID;
    if (!(projectId1.GetValueOrDefault() == projectId2.GetValueOrDefault() & projectId1.HasValue == projectId2.HasValue) && ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.PO.POLine>>) e).Cache.RaiseExceptionHandling<PX.Objects.PO.POLine.projectID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.PO.POLine>>) e).Cache.Current, ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.PO.POLine>>) e).Cache.GetValueExt<PX.Objects.PO.POLine.projectID>((object) row), (Exception) new PXSetPropertyException("The Project ID cannot be modified because this line is related to a service order.", (PXErrorLevel) 4)))
      throw new PXRowPersistingException("projectID", ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.PO.POLine>>) e).Cache.GetValueExt<PX.Objects.PO.POLine.projectID>((object) row), "The Project ID cannot be modified because this line is related to a service order.");
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.PO.POLine> e)
  {
    if (e.Row == null || e.TranStatus != null || e.Operation != 1)
      return;
    FSPOReceiptProcess.VerifyAndUpdateSrvOrdApptLineProjectTask(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.PO.POLine>>) e).Cache.Graph, e.Row, this.fsSrvOrdTypeView, (PXSelectBase<FSSODet>) this.FSSODetFixedDemand, (PXSelectBase<FSAppointmentDet>) this.AppointmentLineView);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<INItemPlan> e)
  {
    if (e.Row == null || e.TranStatus != null)
      return;
    INItemPlan inItemPlanRow = e.Row;
    if (e.Operation == 1)
    {
      if (!inItemPlanRow.SupplyPlanID.HasValue || !(inItemPlanRow.PlanType == "F6"))
        return;
      PX.Objects.PO.POLine poLine = (PX.Objects.PO.POLine) null;
      if (inItemPlanRow.SupplyPlanID.HasValue)
        poLine = PXResult<PX.Objects.PO.POLine>.op_Implicit(((IQueryable<PXResult<PX.Objects.PO.POLine>>) ((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).Select(Array.Empty<object>())).Where<PXResult<PX.Objects.PO.POLine>>((Expression<Func<PXResult<PX.Objects.PO.POLine>, bool>>) (x => ((PX.Objects.PO.POLine) x).PlanID == inItemPlanRow.SupplyPlanID)).First<PXResult<PX.Objects.PO.POLine>>());
      FSPOReceiptProcess.UpdatePOReferenceInSrvOrdLine((PXGraph) this.Base, (PXSelectBase<FSServiceOrder>) this.serviceOrderView, (PXSelectBase<FSSODet>) this.FSSODetFixedDemand, (PXSelectBase<FSSODetSplit>) this.FSSODetSplitFixedDemand, (PXSelectBase<FSAppointment>) this.AppointmentView, (PXSelectBase<FSAppointmentDet>) this.AppointmentLineView, PXResultset<FSSODet>.op_Implicit(((PXSelectBase<FSSODet>) this.FSSODetFixedDemand).Select(new object[1]
      {
        (object) inItemPlanRow.PlanID
      })), ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current, poLine.LineNbr, poLine.Completed, ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<INItemPlan>>) e).Cache, inItemPlanRow, false, true);
    }
    else
    {
      if (e.Operation != 3 || !(inItemPlanRow.PlanType == "F7") && !(inItemPlanRow.PlanType == "F8"))
        return;
      inItemPlanRow = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.supplyPlanID, Equal<Required<INItemPlan.supplyPlanID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) inItemPlanRow.PlanID
      }));
      if (inItemPlanRow == null || !inItemPlanRow.SupplyPlanID.HasValue)
        return;
      FSPOReceiptProcess.UpdatePOReferenceInSrvOrdLine((PXGraph) this.Base, (PXSelectBase<FSServiceOrder>) this.serviceOrderView, (PXSelectBase<FSSODet>) this.FSSODetFixedDemand, (PXSelectBase<FSSODetSplit>) this.FSSODetSplitFixedDemand, (PXSelectBase<FSAppointment>) this.AppointmentView, (PXSelectBase<FSAppointmentDet>) this.AppointmentLineView, PXResultset<FSSODet>.op_Implicit(((PXSelectBase<FSSODet>) this.FSSODetFixedDemand).Select(new object[1]
      {
        (object) inItemPlanRow.PlanID
      })), ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current, new int?(), new bool?(false), ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<INItemPlan>>) e).Cache, inItemPlanRow, true, true);
    }
  }

  /// Overrides <see cref="M:PX.Objects.PO.POOrderEntry.ClearPOLinePlanIDIfPlanIsDeleted" />
  [PXOverride]
  public void ClearPOLinePlanIDIfPlanIsDeleted(System.Action base_ClearPOLinePlanIDIfPlanIsDeleted)
  {
    this.ProcessSrvOrder((PXGraph) this.Base, ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document)?.Current);
    base_ClearPOLinePlanIDIfPlanIsDeleted();
  }

  public virtual void ProcessSrvOrder(PXGraph graph, PX.Objects.PO.POOrder poOrder)
  {
    if (poOrder == null)
      return;
    GraphHelper.EnsureCachePersistence<FSAppointment>(graph);
    GraphHelper.EnsureCachePersistence<FSAppointmentDet>(graph);
    GraphHelper.EnsureCachePersistence<FSApptLineSplit>(graph);
    GraphHelper.EnsureCachePersistence<FSServiceOrder>(graph);
    GraphHelper.EnsureCachePersistence<FSSODet>(graph);
    GraphHelper.EnsureCachePersistence<FSSODetSplit>(graph);
    PXSelect<FSSODetSplit> pxSelect = new PXSelect<FSSODetSplit>(graph);
    PXSelect<INItemPlan> initemplan = new PXSelect<INItemPlan>(graph);
    List<FSPOReceiptProcess.SrvOrdLineWithSplits> ordLineWithSplitsList = new List<FSPOReceiptProcess.SrvOrdLineWithSplits>();
    PXResultset<PX.Objects.PO.POLine> pxResultset = PXSelectBase<PX.Objects.PO.POLine, PXSelectJoin<PX.Objects.PO.POLine, InnerJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POLine.FK.Order>, InnerJoin<INItemPlan, On<INItemPlan.supplyPlanID, Equal<PX.Objects.PO.POLine.planID>>, InnerJoin<FSSODetSplit, On<FSSODetSplit.planID, Equal<INItemPlan.planID>, And<FSSODetSplit.pOType, Equal<PX.Objects.PO.POLine.orderType>, And<FSSODetSplit.pONbr, Equal<PX.Objects.PO.POLine.orderNbr>, And<FSSODetSplit.pOLineNbr, Equal<PX.Objects.PO.POLine.lineNbr>>>>>>>>, Where<PX.Objects.PO.POLine.orderType, Equal<Required<PX.Objects.PO.POLine.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Required<PX.Objects.PO.POLine.orderNbr>>, And2<Where<PX.Objects.PO.POLine.cancelled, Equal<boolTrue>, Or<PX.Objects.PO.POLine.completed, Equal<boolTrue>>>, And2<Where<PX.Objects.PO.POOrder.orderType, NotEqual<POOrderType.dropShip>, Or<PX.Objects.PO.POOrder.isLegacyDropShip, Equal<True>>>, And<FSSODetSplit.receivedQty, Less<FSSODetSplit.qty>, And<FSSODetSplit.pOCancelled, NotEqual<boolTrue>, And<FSSODetSplit.completed, NotEqual<boolTrue>>>>>>>>>.Config>.Select(graph, new object[2]
    {
      (object) poOrder.OrderType,
      (object) poOrder.OrderNbr
    });
    if (pxResultset != null && pxResultset.Count > 0)
    {
      foreach (PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, INItemPlan, FSSODetSplit> pxResult in pxResultset)
      {
        PX.Objects.PO.POLine poLine = PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, INItemPlan, FSSODetSplit>.op_Implicit(pxResult);
        INItemPlan copy1 = PXCache<INItemPlan>.CreateCopy(PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, INItemPlan, FSSODetSplit>.op_Implicit(pxResult));
        FSSODetSplit copy2 = PXCache<FSSODetSplit>.CreateCopy(PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, INItemPlan, FSSODetSplit>.op_Implicit(pxResult));
        ((PXSelectBase<FSServiceOrder>) this.serviceOrderView).Current = (FSServiceOrder) PXParentAttribute.SelectParent(((PXSelectBase) pxSelect).Cache, (object) copy2, typeof (FSServiceOrder));
        ((PXSelectBase<FSSODet>) this.FSSODetFixedDemand).Current = (FSSODet) PXParentAttribute.SelectParent(((PXSelectBase) pxSelect).Cache, (object) copy2, typeof (FSSODet));
        if (!copy2.Completed.GetValueOrDefault() && !copy2.POCancelled.GetValueOrDefault())
        {
          Decimal? baseQty = copy2.BaseQty;
          Decimal? baseReceivedQty = copy2.BaseReceivedQty;
          if (baseQty.GetValueOrDefault() > baseReceivedQty.GetValueOrDefault() & baseQty.HasValue & baseReceivedQty.HasValue)
          {
            FSPOReceiptProcess.UpdateSchedulesFromCompletedPOStatic(graph, pxSelect, initemplan, copy2, this.serviceOrderView, copy1);
            if (((PXSelectBase) initemplan).Cache.GetStatus((object) copy1) != 2)
              ((PXSelectBase<INItemPlan>) initemplan).Delete(copy1);
            ((PXSelectBase) pxSelect).Cache.SetStatus((object) copy2, (PXEntryStatus) 0);
            FSSODetSplit copy3 = PXCache<FSSODetSplit>.CreateCopy(copy2);
            copy3.PlanID = new long?();
            copy3.Completed = new bool?(true);
            copy3.POCompleted = new bool?(true);
            copy3.POCancelled = new bool?(true);
            ((PXSelectBase) pxSelect).Cache.Update((object) copy3);
            ordLineWithSplitsList.Add(new FSPOReceiptProcess.SrvOrdLineWithSplits(((PXSelectBase<FSSODet>) this.FSSODetFixedDemand).Current, copy3, copy1.PlanQty));
            INItemPlan inItemPlan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.supplyPlanID, Equal<Required<INItemPlan.supplyPlanID>>>>.Config>.Select(graph, new object[1]
            {
              (object) poLine.PlanID
            }));
            FSPOReceiptProcess.UpdatePOReferenceInSrvOrdLine(graph, (PXSelectBase<FSServiceOrder>) this.serviceOrderView, (PXSelectBase<FSSODet>) this.FSSODetFixedDemand, (PXSelectBase<FSSODetSplit>) pxSelect, (PXSelectBase<FSAppointment>) this.AppointmentView, (PXSelectBase<FSAppointmentDet>) this.AppointmentLineView, ((PXSelectBase<FSSODet>) this.FSSODetFixedDemand).Current, poOrder, poLine.LineNbr, poLine.Completed, (PXCache) null, inItemPlan, false, false);
            foreach (FSPOReceiptProcess.SrvOrdLineWithSplits srvOrdLineExt in ordLineWithSplitsList)
              FSPOReceiptProcess.UpdatePOReceiptInfoInAppointmentsStatic(graph, srvOrdLineExt, pxSelect, this.AppointmentView, this.AppointmentLineView, this.apptSplitView, false);
          }
        }
      }
    }
    else
      FSPOReceiptProcess.UpdateSrvOrdLinePOStatus(graph, poOrder);
  }

  public class FSSODetSplitPlanSyncOnly : ItemPlanSyncOnly<POOrderEntry, FSSODetSplit>
  {
    public static bool IsActive()
    {
      return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();
    }
  }
}
