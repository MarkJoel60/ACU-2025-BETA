// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_APReleaseProcess
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO.GraphExtensions.APReleaseProcessExt;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class SM_APReleaseProcess : 
  PXGraphExtension<UpdatePOOnRelease, APReleaseProcess.MultiCurrency, APReleaseProcess>
{
  [PXHidden]
  public PXSelect<FSSODet> FsSODet;
  [PXHidden]
  public PXSelect<FSAppointmentDet> FsAppointmentDet;
  [PXHidden]
  public PXSelect<FSSODetSplit> soDetSplitView;
  [PXHidden]
  public PXSelect<FSAppointment> appointmentView;
  [PXHidden]
  public PXSelect<FSApptLineSplit> apptSplitView;
  [PXHidden]
  public PXSelect<FSServiceOrder> serviceOrderView;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXOverride]
  public void VerifyStockItemLineHasReceipt(PX.Objects.AP.APRegister arRegisterRow, Action<PX.Objects.AP.APRegister> del)
  {
    if (!(arRegisterRow.CreatedByScreenID != "FS500100") || !(arRegisterRow.CreatedByScreenID != "FS500600") || del == null)
      return;
    del(arRegisterRow);
  }

  [PXOverride]
  public virtual PX.Objects.AP.APRegister OnBeforeRelease(
    PX.Objects.AP.APRegister apdoc,
    SM_APReleaseProcess.OnBeforeReleaseDelegate del)
  {
    this.ValidatePostBatchStatus((PXDBOperation) 1, "AP", apdoc.DocType, apdoc.RefNbr);
    return del != null ? del(apdoc) : (PX.Objects.AP.APRegister) null;
  }

  [PXOverride]
  public void PerformPersist(PXGraph.IPersistPerformer persister)
  {
    persister.Update(((PXSelectBase) this.FsSODet).Cache);
    persister.Update(((PXSelectBase) this.serviceOrderView).Cache);
    persister.Update(((PXSelectBase) this.FsAppointmentDet).Cache);
    persister.Update(((PXSelectBase) this.soDetSplitView).Cache);
    persister.Update(((PXSelectBase) this.appointmentView).Cache);
    persister.Update(((PXSelectBase) this.apptSplitView).Cache);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.PO.POOrder> e)
  {
    if (e.TranStatus != null || e.Operation != 1)
      return;
    PX.Objects.PO.POOrder row = e.Row;
    if (!((string) ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.PO.POOrder>>) e).Cache.GetValueOriginal<PX.Objects.PO.POOrder.status>((object) row) != row.Status))
      return;
    FSPOReceiptProcess.UpdateSrvOrdLinePOStatus(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.PO.POOrder>>) e).Cache.Graph, row);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.PO.POLine> e)
  {
    if (e.TranStatus != null || e.Operation != 1)
      return;
    PX.Objects.PO.POLine row = e.Row;
    FSSODet fsSODet = PXResultset<FSSODet>.op_Implicit(PXSelectBase<FSSODet, PXSelect<FSSODet, Where<FSSODet.poType, Equal<Required<FSSODet.poType>>, And<FSSODet.poNbr, Equal<Required<FSSODet.poNbr>>, And<FSSODet.poLineNbr, Equal<Required<FSSODet.poLineNbr>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, new object[3]
    {
      (object) row.OrderType,
      (object) row.OrderNbr,
      (object) row.LineNbr
    }));
    INItemPlan inItemPlan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.supplyPlanID, Equal<Required<INItemPlan.supplyPlanID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, new object[1]
    {
      (object) row.PlanID
    }));
    PX.Objects.PO.POOrder poOrder = (PX.Objects.PO.POOrder) PXParentAttribute.SelectParent((PXCache) GraphHelper.Caches<PX.Objects.PO.POLine>((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base), (object) row, typeof (PX.Objects.PO.POOrder));
    FSPOReceiptProcess.UpdatePOReferenceInSrvOrdLine((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, (PXSelectBase<FSServiceOrder>) this.serviceOrderView, (PXSelectBase<FSSODet>) this.FsSODet, (PXSelectBase<FSSODetSplit>) this.soDetSplitView, (PXSelectBase<FSAppointment>) this.appointmentView, (PXSelectBase<FSAppointmentDet>) this.FsAppointmentDet, fsSODet, poOrder, row.LineNbr, row.Completed, (PXCache) null, inItemPlan, false, true);
  }

  /// <summary>
  /// Extends <see cref="M:PX.Objects.AP.APReleaseProcess.InvoiceTransactionsReleased(PX.Objects.AP.InvoiceTransactionsReleasedArgs)" />
  /// </summary>
  [PXOverride]
  public virtual void InvoiceTransactionsReleased(InvoiceTransactionsReleasedArgs args)
  {
    PXCache sender = (PXCache) GraphHelper.Caches<FSSODet>((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base);
    PXCache pxCache1 = (PXCache) GraphHelper.Caches<FSAppointmentDet>((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base);
    PXCache pxCache2 = (PXCache) GraphHelper.Caches<FSServiceOrder>((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base);
    PXCache pxCache3 = (PXCache) GraphHelper.Caches<FSAppointment>((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base);
    foreach (PX.Objects.AP.APTran apTran in ((PXSelectBase) ((PXGraphExtension<APReleaseProcess>) this).Base.APTran_TranType_RefNbr).Cache.Updated)
    {
      PXResult<FSSODet> pxResult = ((IQueryable<PXResult<FSSODet>>) PXSelectBase<FSSODet, PXViewOf<FSSODet>.BasedOn<SelectFromBase<FSSODet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<FSAppointmentDet>.On<BqlOperand<FSAppointmentDet.sODetID, IBqlInt>.IsEqual<FSSODet.sODetID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODet.poType, Equal<P.AsString>>>>, And<BqlOperand<FSSODet.poNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<FSSODet.poLineNbr, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<FSSODet.curyUnitCost, IBqlDecimal>.IsNotEqual<P.AsDecimal>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODet.lineType, Equal<FSLineType.NonStockItem>>>>>.Or<BqlOperand<FSSODet.lineType, IBqlString>.IsEqual<FSLineType.Service>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, new object[4]
      {
        (object) apTran.POOrderType,
        (object) apTran.PONbr,
        (object) apTran.POLineNbr,
        (object) apTran.CuryUnitCost
      })).FirstOrDefault<PXResult<FSSODet>>();
      if (pxResult != null)
      {
        FSSODet fssoDet = ((PXResult) pxResult).GetItem<FSSODet>();
        FSAppointmentDet fsAppointmentDet = ((PXResult) pxResult).GetItem<FSAppointmentDet>();
        if (fssoDet != null)
        {
          Decimal num = INUnitAttribute.ConvertFromBase(sender, apTran.InventoryID, apTran.UOM, apTran.UnitCost.GetValueOrDefault(), INPrecision.UNITCOST);
          FSSODet copy1 = (FSSODet) sender.CreateCopy((object) fssoDet);
          copy1.CuryUnitCost = new Decimal?(num);
          copy1.UnitCost = new Decimal?(num);
          sender.Update((object) copy1);
          if (fsAppointmentDet != null)
          {
            FSAppointmentDet copy2 = (FSAppointmentDet) pxCache1.CreateCopy((object) fsAppointmentDet);
            copy2.CuryUnitCost = new Decimal?(num);
            copy2.UnitCost = new Decimal?(num);
            pxCache1.Update((object) copy2);
          }
        }
      }
    }
    sender.Persist((PXDBOperation) 1);
    pxCache1.Persist((PXDBOperation) 1);
    pxCache2.Persist((PXDBOperation) 1);
    pxCache3.Persist((PXDBOperation) 1);
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXCheckUnique))]
  protected virtual void _(PX.Data.Events.CacheAttached<FSSODet.lineNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXCheckUnique))]
  protected virtual void _(PX.Data.Events.CacheAttached<FSAppointmentDet.lineNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXUIVerifyAttribute))]
  [PXRemoveBaseAttribute(typeof (FSDBTimeSpanLongAllowNegativeAttribute))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FSAppointmentDet.actualDuration> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXUIVerifyAttribute))]
  protected virtual void _(PX.Data.Events.CacheAttached<FSAppointmentDet.actualQty> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXUIVerifyAttribute))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FSAppointmentDet.billableQty> e)
  {
  }

  public virtual void ValidatePostBatchStatus(
    PXDBOperation dbOperation,
    string postTo,
    string createdDocType,
    string createdRefNbr)
  {
    DocGenerationHelper.ValidatePostBatchStatus<PX.Objects.AP.APRegister>((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, dbOperation, postTo, createdDocType, createdRefNbr);
  }

  public delegate PX.Objects.AP.APRegister OnBeforeReleaseDelegate(PX.Objects.AP.APRegister apdoc);
}
