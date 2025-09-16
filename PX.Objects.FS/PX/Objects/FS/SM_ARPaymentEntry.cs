// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_ARPaymentEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class SM_ARPaymentEntry : PXGraphExtension<ARPaymentEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelectJoin<FSAdjust, LeftJoin<FSServiceOrder, On<FSServiceOrder.srvOrdType, Equal<FSAdjust.adjdOrderType>, And<FSServiceOrder.refNbr, Equal<FSAdjust.adjdOrderNbr>>>>, Where<FSAdjust.adjgDocType, Equal<Current<ARPayment.docType>>, And<FSAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>>>> FSAdjustments;
  public PXAction<ARPayment> viewFSDocumentToApply;
  public PXAction<ARPayment> viewFSAppointmentSource;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewFSDocumentToApply(PXAdapter adapter)
  {
    FSAdjust current = ((PXSelectBase<FSAdjust>) this.FSAdjustments).Current;
    if (current != null && !string.IsNullOrEmpty(current.AdjdOrderType) && !string.IsNullOrEmpty(current.AdjdOrderNbr))
    {
      ServiceOrderEntry instance = PXGraph.CreateInstance<ServiceOrderEntry>();
      ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) current.AdjdOrderNbr, new object[1]
      {
        (object) current.AdjdOrderType
      }));
      if (((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Service Order");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewFSAppointmentSource(PXAdapter adapter)
  {
    FSAdjust current = ((PXSelectBase<FSAdjust>) this.FSAdjustments).Current;
    if (current != null && !string.IsNullOrEmpty(current.AdjdOrderType) && !string.IsNullOrEmpty(current.AdjdAppRefNbr))
    {
      AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
      ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.refNbr>((object) current.AdjdAppRefNbr, new object[1]
      {
        (object) current.AdjdOrderType
      }));
      if (((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Appointment Source");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelecting<ARPayment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<ARPayment> e)
  {
    if (e.Row == null)
      return;
    ARPayment row = e.Row;
    bool flag = ((PXSelectBase<FSAdjust>) this.FSAdjustments).SelectWindowed(0, 1, Array.Empty<object>()).Count > 0;
    ((PXSelectBase) this.FSAdjustments).Cache.AllowInsert = !flag;
    ((PXSelectBase) this.FSAdjustments).Cache.AllowDelete = false;
    ((PXSelectBase) this.FSAdjustments).AllowSelect = ((row.CreatedByScreenID == "FS300200" ? 1 : (row.CreatedByScreenID == "FS300100" ? 1 : 0)) | (flag ? 1 : 0)) != 0;
  }

  protected virtual void _(PX.Data.Events.RowInserting<ARPayment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<ARPayment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<ARPayment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<ARPayment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<ARPayment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<ARPayment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<ARPayment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<ARPayment> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAdjust, FSAdjust.adjdOrderNbr> e)
  {
    try
    {
      FSAdjust row = e.Row;
      using (IEnumerator<PXResult<FSServiceOrder>> enumerator = PXSelectBase<FSServiceOrder, PXSelectJoin<FSServiceOrder, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<FSServiceOrder.curyInfoID>>>, Where<FSServiceOrder.srvOrdType, Equal<Required<FSServiceOrder.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Required<FSServiceOrder.refNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) row.AdjdOrderType,
        (object) row.AdjdOrderNbr
      }).GetEnumerator())
      {
        if (!enumerator.MoveNext())
          return;
        this.FSAdjust_AdjdOrderNbr_FieldUpdated<FSServiceOrder>((PXResult<FSServiceOrder, PX.Objects.CM.CurrencyInfo>) enumerator.Current, row);
      }
    }
    catch (PXSetPropertyException ex)
    {
      throw new PXException(((Exception) ex).Message);
    }
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSAdjust> e)
  {
    if (e.Row == null)
      return;
    FSAdjust row = e.Row;
    using (new PXConnectionScope())
      row.SOCuryCompletedBillableTotal = this.GetServiceOrderBillableTotal(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<FSAdjust>>) e).Cache.Graph, row.AdjdOrderType, row.AdjdOrderNbr);
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSAdjust> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSAdjust> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSAdjust> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSAdjust> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSAdjust> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSAdjust> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSAdjust> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSAdjust> e)
  {
    if (((PXSelectBase<ARPayment>) this.Base.Document).Current == null || e.Row == null)
      return;
    FSAdjust row = e.Row;
    FSServiceOrder fsServiceOrder = PXResultset<FSServiceOrder>.op_Implicit(PXSelectBase<FSServiceOrder, PXSelect<FSServiceOrder, Where<FSServiceOrder.srvOrdType, Equal<Required<FSServiceOrder.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Required<FSServiceOrder.refNbr>>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[2]
    {
      (object) row.AdjdOrderType,
      (object) row.AdjdOrderNbr
    }));
    if (fsServiceOrder == null || string.Equals(fsServiceOrder.CuryID, ((PXSelectBase<ARPayment>) this.Base.Document).Current.CuryID))
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAdjust>>) e).Cache.RaiseExceptionHandling<FSAdjust.adjdOrderNbr>((object) row, (object) row.AdjdOrderNbr, (Exception) new PXSetPropertyException("The currency in the current document does not match the currency in the source service order.", (PXErrorLevel) 4));
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSAdjust> e)
  {
  }

  protected void FSAdjust_AdjdOrderNbr_FieldUpdated<T>(PXResult<T, PX.Objects.CM.CurrencyInfo> res, FSAdjust adj) where T : FSServiceOrder, new()
  {
    T copy = PXCache<T>.CreateCopy(PXResult<T, PX.Objects.CM.CurrencyInfo>.op_Implicit(res));
    adj.CustomerID = ((PXSelectBase<ARPayment>) this.Base.Document).Current.CustomerID;
    adj.AdjgDocDate = ((PXSelectBase<ARPayment>) this.Base.Document).Current.AdjDate;
    adj.AdjgCuryInfoID = ((PXSelectBase<ARPayment>) this.Base.Document).Current.CuryInfoID;
    adj.AdjdCuryInfoID = copy.CuryInfoID;
    adj.AdjdOrigCuryInfoID = copy.CuryInfoID;
    FSAdjust fsAdjust = adj;
    DateTime? orderDate = copy.OrderDate;
    DateTime? adjDate = ((PXSelectBase<ARPayment>) this.Base.Document).Current.AdjDate;
    DateTime? nullable = (orderDate.HasValue & adjDate.HasValue ? (orderDate.GetValueOrDefault() > adjDate.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? ((PXSelectBase<ARPayment>) this.Base.Document).Current.AdjDate : copy.OrderDate;
    fsAdjust.AdjdOrderDate = nullable;
    adj.Released = new bool?(false);
    if (((PXSelectBase<ARPayment>) this.Base.Document).Current == null || !string.IsNullOrEmpty(((PXSelectBase<ARPayment>) this.Base.Document).Current.DocDesc))
      return;
    ((PXSelectBase<ARPayment>) this.Base.Document).Current.DocDesc = copy.DocDesc;
  }

  public virtual Decimal? GetServiceOrderBillableTotal(
    PXGraph graph,
    string srvOrdType,
    string refNbr)
  {
    if (string.IsNullOrEmpty(srvOrdType) || string.IsNullOrEmpty(refNbr))
      return new Decimal?();
    FSServiceOrder fsServiceOrderRow = PXResultset<FSServiceOrder>.op_Implicit(PXSelectBase<FSServiceOrder, PXSelect<FSServiceOrder, Where<FSServiceOrder.srvOrdType, Equal<Required<FSServiceOrder.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Required<FSServiceOrder.refNbr>>>>>.Config>.Select(graph, new object[2]
    {
      (object) srvOrdType,
      (object) refNbr
    }));
    if (fsServiceOrderRow == null)
      return new Decimal?(0M);
    ServiceOrderEntry.UpdateServiceOrderUnboundFields(graph, fsServiceOrderRow);
    return new Decimal?(fsServiceOrderRow.CuryEffectiveBillableDocTotal.GetValueOrDefault());
  }
}
