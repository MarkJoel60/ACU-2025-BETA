// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_ExpenseClaimDetailEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.FS;

public class SM_ExpenseClaimDetailEntry : PXGraphExtension<ExpenseClaimDetailEntry>
{
  private ServiceOrderEntry _ServiceOrderGraph;
  private AppointmentEntry _AppointmentGraph;
  public PXSelect<FSServiceOrder> ServiceOrderRecords;
  public PXSelect<FSAppointment> AppointmentRecords;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  protected ServiceOrderEntry GetServiceOrderGraph(bool clearGraph)
  {
    if (this._ServiceOrderGraph == null)
      this._ServiceOrderGraph = PXGraph.CreateInstance<ServiceOrderEntry>();
    else if (clearGraph)
      ((PXGraph) this._ServiceOrderGraph).Clear();
    return this._ServiceOrderGraph;
  }

  protected AppointmentEntry GetAppointmentGraph(bool clearGraph)
  {
    if (this._AppointmentGraph == null)
      this._AppointmentGraph = PXGraph.CreateInstance<AppointmentEntry>();
    else if (clearGraph)
      ((PXGraph) this._AppointmentGraph).Clear();
    return this._AppointmentGraph;
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType>), SubstituteKey = typeof (FSSrvOrdType.srvOrdType), DescriptionField = typeof (FSSrvOrdType.srvOrdType))]
  protected virtual void FSServiceOrder_SrvOrdType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType>), SubstituteKey = typeof (FSSrvOrdType.srvOrdType), DescriptionField = typeof (FSSrvOrdType.srvOrdType))]
  protected virtual void FSAppointment_SrvOrdType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search<PX.Objects.AR.Customer.bAccountID>), SubstituteKey = typeof (PX.Objects.AR.Customer.acctCD), DescriptionField = typeof (PX.Objects.AR.Customer.acctName))]
  protected virtual void FSServiceOrder_CustomerID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search<PX.Objects.AR.Customer.bAccountID>), SubstituteKey = typeof (PX.Objects.AR.Customer.acctCD), DescriptionField = typeof (PX.Objects.AR.Customer.acctName))]
  protected virtual void FSAppointment_CustomerID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSxEPExpenseClaimDetails.fsEntityType> e)
  {
    if (e.Row == null)
      return;
    FSxEPExpenseClaimDetails extension = ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSxEPExpenseClaimDetails.fsEntityType>>) e).Cache.GetExtension<FSxEPExpenseClaimDetails>(e.Row);
    if (string.IsNullOrEmpty(extension.FSEntityTypeUI))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSxEPExpenseClaimDetails.fsEntityType>, object, object>) e).NewValue = (object) extension.FSEntityTypeUI;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPExpenseClaimDetails.billable> e)
  {
    if (e.Row == null || !((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails.billable>>) e).Cache.GetExtension<FSxEPExpenseClaimDetails>(e.Row).FSBillable.GetValueOrDefault() || !((bool?) e.NewValue).GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails.billable>>) e).Cache.SetValueExt<FSxEPExpenseClaimDetails.fsBillable>(e.Row, (object) false);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSxEPExpenseClaimDetails.fsBillable> e)
  {
    if (e.Row == null)
      return;
    bool? nullable = ((EPExpenseClaimDetails) e.Row).Billable;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = (bool?) e.NewValue;
    if (!nullable.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSxEPExpenseClaimDetails.fsBillable>>) e).Cache.SetValueExt<EPExpenseClaimDetails.billable>(e.Row, (object) false);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSxEPExpenseClaimDetails.fsEntityNoteID> e)
  {
    if (e.Row == null)
      return;
    FSxEPExpenseClaimDetails extension = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSxEPExpenseClaimDetails.fsEntityNoteID>>) e).Cache.GetExtension<FSxEPExpenseClaimDetails>(e.Row);
    if (!extension.FSEntityNoteID.HasValue)
      return;
    EPExpenseClaimDetails row = (EPExpenseClaimDetails) e.Row;
    FSServiceOrder serviceOrder = this.GetServiceOrder(extension);
    if (serviceOrder == null)
      return;
    int? customerId = (int?) row?.CustomerID;
    int? billCustomerId = (int?) serviceOrder?.BillCustomerID;
    if (customerId.GetValueOrDefault() == billCustomerId.GetValueOrDefault() & customerId.HasValue == billCustomerId.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSxEPExpenseClaimDetails.fsEntityNoteID>>) e).Cache.SetValueExt<EPExpenseClaimDetails.customerID>(e.Row, (object) serviceOrder.BillCustomerID);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSxEPExpenseClaimDetails.fsEntityNoteID>>) e).Cache.SetValueExt<EPExpenseClaimDetails.customerLocationID>(e.Row, (object) serviceOrder.BillLocationID);
  }

  private FSServiceOrder GetServiceOrder(FSxEPExpenseClaimDetails row)
  {
    FSServiceOrder serviceOrder = (FSServiceOrder) null;
    if (row.FSEntityType == "PX.Objects.FS.FSServiceOrder")
      serviceOrder = PXResultset<FSServiceOrder>.op_Implicit(PXSelectBase<FSServiceOrder, PXViewOf<FSServiceOrder>.BasedOn<SelectFromBase<FSServiceOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AR.Customer>.On<BqlOperand<FSServiceOrder.customerID, IBqlInt>.IsEqual<PX.Objects.AR.Customer.bAccountID>>>>.Where<BqlOperand<FSServiceOrder.noteID, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
      {
        (object) row.FSEntityNoteID
      }));
    else if (row.FSEntityType == "PX.Objects.FS.FSAppointment")
      serviceOrder = PXResultset<FSServiceOrder>.op_Implicit(PXSelectBase<FSServiceOrder, PXViewOf<FSServiceOrder>.BasedOn<SelectFromBase<FSServiceOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AR.Customer>.On<BqlOperand<FSServiceOrder.customerID, IBqlInt>.IsEqual<PX.Objects.AR.Customer.bAccountID>>>, FbqlJoins.Inner<FSAppointment>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSAppointment.soRefNbr, Equal<FSServiceOrder.refNbr>>>>>.And<BqlOperand<FSAppointment.srvOrdType, IBqlString>.IsEqual<FSServiceOrder.srvOrdType>>>>>.Where<BqlOperand<FSAppointment.noteID, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
      {
        (object) row.FSEntityNoteID
      }));
    return serviceOrder;
  }

  protected virtual void _(PX.Data.Events.RowSelecting<EPExpenseClaimDetails> e)
  {
    if (e.Row == null)
      return;
    FSxEPExpenseClaimDetails extension = ((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<EPExpenseClaimDetails>>) e).Cache.GetExtension<FSxEPExpenseClaimDetails>((object) e.Row);
    if (!extension.FSEntityNoteID.HasValue)
      return;
    using (new PXConnectionScope())
    {
      object entityRow = new EntityHelper((PXGraph) this.Base).GetEntityRow(extension.FSEntityNoteID, true);
      if (entityRow == null)
        return;
      if (extension.FSEntityTypeUI == null)
      {
        extension.FSEntityTypeUI = MainTools.GetLongName(entityRow.GetType());
        extension.FSEntityType = extension.FSEntityTypeUI;
      }
      extension.IsDocBilledOrClosed = new bool?(this.IsFSDocumentBilledOrClosed(entityRow, extension.FSEntityType));
      extension.IsDocRelatedToProject = new bool?(this.IsFSDocumentRelatedToProjects(entityRow, extension.FSEntityType));
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<EPExpenseClaimDetails> e)
  {
    if (e.Row == null)
      return;
    FSxEPExpenseClaimDetails extension = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache.GetExtension<FSxEPExpenseClaimDetails>((object) e.Row);
    bool flag1 = extension != null && !string.IsNullOrEmpty(extension.FSEntityTypeUI);
    PXDefaultAttribute.SetPersistingCheck<FSxEPExpenseClaimDetails.fsEntityNoteID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) e.Row, flag1 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetEnabled<FSxEPExpenseClaimDetails.fsEntityNoteID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) e.Row, flag1);
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache;
    EPExpenseClaimDetails row = e.Row;
    bool? nullable;
    int num;
    if (extension.FSEntityNoteID.HasValue)
    {
      nullable = extension.IsDocBilledOrClosed;
      bool flag2 = false;
      num = nullable.GetValueOrDefault() == flag2 & nullable.HasValue ? 1 : 0;
    }
    else
      num = 0;
    PXUIFieldAttribute.SetEnabled<FSxEPExpenseClaimDetails.fsBillable>(cache, (object) row, num != 0);
    nullable = extension.FSBillable;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = extension.IsDocBilledOrClosed;
    if (!nullable.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.inventoryID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.customerID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.customerLocationID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.billable>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.qty>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.curyUnitCost>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.curyExtCost>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.curyEmployeePart>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.contractID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.taskID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.costCodeID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) e.Row, false);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<EPExpenseClaimDetails> e)
  {
    FSxEPExpenseClaimDetails extension1 = ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<EPExpenseClaimDetails>>) e).Cache.GetExtension<FSxEPExpenseClaimDetails>((object) e.Row);
    FSxEPExpenseClaimDetails extension2 = ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<EPExpenseClaimDetails>>) e).Cache.GetExtension<FSxEPExpenseClaimDetails>((object) e.OldRow);
    Guid? fsEntityNoteId1;
    if (extension1.FSEntityTypeUI != extension2.FSEntityTypeUI)
    {
      fsEntityNoteId1 = extension1.FSEntityNoteID;
      Guid? fsEntityNoteId2 = extension2.FSEntityNoteID;
      if ((fsEntityNoteId1.HasValue == fsEntityNoteId2.HasValue ? (fsEntityNoteId1.HasValue ? (fsEntityNoteId1.GetValueOrDefault() == fsEntityNoteId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      {
        extension1.FSEntityNoteID = new Guid?();
        extension1.IsDocBilledOrClosed = new bool?(false);
        extension1.IsDocRelatedToProject = new bool?(false);
      }
    }
    if (!extension1.FSEntityNoteID.HasValue)
      return;
    Guid? fsEntityNoteId3 = extension1.FSEntityNoteID;
    fsEntityNoteId1 = extension2.FSEntityNoteID;
    if ((fsEntityNoteId3.HasValue == fsEntityNoteId1.HasValue ? (fsEntityNoteId3.HasValue ? (fsEntityNoteId3.GetValueOrDefault() != fsEntityNoteId1.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      return;
    object entityRow = new EntityHelper((PXGraph) this.Base).GetEntityRow(extension1.FSEntityNoteID, true);
    extension1.IsDocBilledOrClosed = new bool?(this.IsFSDocumentBilledOrClosed(entityRow, extension1.FSEntityType));
    extension1.IsDocRelatedToProject = new bool?(this.IsFSDocumentRelatedToProjects(entityRow, extension1.FSEntityType));
  }

  protected virtual void _(PX.Data.Events.RowPersisting<EPExpenseClaimDetails> e)
  {
    if (e.Operation != 2 && e.Operation != 1)
      return;
    EPExpenseClaimDetails row = e.Row;
    FSxEPExpenseClaimDetails extension = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<EPExpenseClaimDetails>>) e).Cache.GetExtension<FSxEPExpenseClaimDetails>((object) e.Row);
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (extension == null || !extension.FSEntityNoteID.HasValue)
      return;
    if (extension.FSEntityType == "PX.Objects.FS.FSServiceOrder")
    {
      FSServiceOrder entityRow = (FSServiceOrder) new EntityHelper((PXGraph) this.Base).GetEntityRow(typeof (FSServiceOrder), extension.FSEntityNoteID);
      if (entityRow != null)
      {
        int? branchId1 = entityRow.BranchID;
        int? branchId2 = row.BranchID;
        if (!(branchId1.GetValueOrDefault() == branchId2.GetValueOrDefault() & branchId1.HasValue == branchId2.HasValue))
          propertyException = new PXSetPropertyException("The document cannot be assigned to the expense receipt because the branch in the document differs from the branch of the expense receipt.", (PXErrorLevel) 5);
        int? projectId = entityRow.ProjectID;
        int? contractId = row.ContractID;
        if (!(projectId.GetValueOrDefault() == contractId.GetValueOrDefault() & projectId.HasValue == contractId.HasValue))
          propertyException = new PXSetPropertyException("The document cannot be assigned to {0} because its project {1} differs from the {0} project {2}.", new object[4]
          {
            (object) "AP Bill",
            (object) PMProject.PK.Find((PXGraph) this.Base, entityRow.ProjectID)?.ContractCD,
            ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<EPExpenseClaimDetails>>) e).Cache.GetValueExt<EPExpenseClaimDetails.contractID>((object) row),
            (object) (PXErrorLevel) 4
          });
      }
    }
    if (extension.FSEntityType == "PX.Objects.FS.FSAppointment")
    {
      FSAppointment entityRow = (FSAppointment) new EntityHelper((PXGraph) this.Base).GetEntityRow(typeof (FSAppointment), extension.FSEntityNoteID);
      if (entityRow != null && this.GetServiceOrderRelated((PXGraph) this.Base, entityRow.SrvOrdType, entityRow.SORefNbr) != null)
      {
        int? branchId3 = entityRow.BranchID;
        int? branchId4 = row.BranchID;
        if (!(branchId3.GetValueOrDefault() == branchId4.GetValueOrDefault() & branchId3.HasValue == branchId4.HasValue))
          propertyException = new PXSetPropertyException("The document cannot be assigned to the expense receipt because the branch in the document differs from the branch of the expense receipt.", (PXErrorLevel) 5);
        int? projectId = entityRow.ProjectID;
        int? contractId = row.ContractID;
        if (!(projectId.GetValueOrDefault() == contractId.GetValueOrDefault() & projectId.HasValue == contractId.HasValue))
          propertyException = new PXSetPropertyException("The document cannot be assigned to {0} because its project {1} differs from the {0} project {2}.", new object[4]
          {
            (object) "AP Bill",
            (object) PMProject.PK.Find((PXGraph) this.Base, entityRow.ProjectID)?.ContractCD,
            ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<EPExpenseClaimDetails>>) e).Cache.GetValueExt<EPExpenseClaimDetails.contractID>((object) row),
            (object) (PXErrorLevel) 4
          });
      }
    }
    if (propertyException == null)
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<EPExpenseClaimDetails>>) e).Cache.RaiseExceptionHandling<FSxEPExpenseClaimDetails.fsEntityNoteID>((object) e.Row, (object) extension.FSEntityNoteID, (Exception) propertyException);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<EPExpenseClaimDetails> e)
  {
    PXGraph graph1 = ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<EPExpenseClaimDetails>>) e).Cache.Graph;
    EPExpenseClaimDetails row = e.Row;
    if (row != null && e.TranStatus == null)
    {
      FSxEPExpenseClaimDetails extension = ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<EPExpenseClaimDetails>>) e).Cache.GetExtension<FSxEPExpenseClaimDetails>((object) e.Row);
      if (extension != null)
      {
        int? valueOriginal1 = (int?) ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<EPExpenseClaimDetails>>) e).Cache.GetValueOriginal<EPExpenseClaimDetails.inventoryID>((object) e.Row);
        Guid? valueOriginal2 = (Guid?) ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<EPExpenseClaimDetails>>) e).Cache.GetValueOriginal<FSxEPExpenseClaimDetails.fsEntityNoteID>((object) e.Row);
        AppointmentEntry graph2 = (AppointmentEntry) null;
        ServiceOrderEntry graph3 = (ServiceOrderEntry) null;
        if (e.Operation != 3)
        {
          int? nullable1 = valueOriginal1;
          int? inventoryId = row.InventoryID;
          if (nullable1.GetValueOrDefault() == inventoryId.GetValueOrDefault() & nullable1.HasValue == inventoryId.HasValue)
          {
            Guid? nullable2 = valueOriginal2;
            Guid? fsEntityNoteId = extension.FSEntityNoteID;
            if ((nullable2.HasValue == fsEntityNoteId.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() != fsEntityNoteId.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
              goto label_18;
          }
        }
        PXResult<FSSODet, FSAppointmentDet> pxResult = (PXResult<FSSODet, FSAppointmentDet>) PXResultset<FSSODet>.op_Implicit(PXSelectBase<FSSODet, PXViewOf<FSSODet>.BasedOn<SelectFromBase<FSSODet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<FSAppointmentDet>.On<BqlOperand<FSAppointmentDet.sODetID, IBqlInt>.IsEqual<FSSODet.sODetID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODet.linkedEntityType, Equal<P.AsString>>>>>.And<BqlOperand<FSSODet.linkedDocRefNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.SelectSingleBound(graph1, (object[]) null, new object[2]
        {
          (object) "ER",
          (object) row.ClaimDetailCD
        }));
        if (pxResult != null)
        {
          FSAppointmentDet fsAppointmentDet = PXResult<FSSODet, FSAppointmentDet>.op_Implicit(pxResult);
          if (fsAppointmentDet != null && !string.IsNullOrEmpty(fsAppointmentDet.LineRef))
          {
            graph2 = this.GetAppointmentGraph(true);
            ((PXSelectBase<FSAppointment>) graph2.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) graph2.AppointmentRecords).Search<FSAppointment.refNbr>((object) fsAppointmentDet.RefNbr, new object[1]
            {
              (object) fsAppointmentDet.SrvOrdType
            }));
            ((PXSelectBase<FSAppointmentDet>) graph2.AppointmentDetails).Delete(fsAppointmentDet);
            if (((PXGraph) graph2).IsDirty)
              ((PXAction) graph2.Save).Press();
            if (((PXSelectBase<FSAppointment>) graph2.AppointmentRecords).Current != null && ((PXGraph) this.Base).Caches[typeof (FSAppointment)].GetStatus((object) ((PXSelectBase<FSAppointment>) graph2.AppointmentRecords).Current) == 1)
              graph1.Caches[typeof (FSAppointment)].Update((object) ((PXSelectBase<FSAppointment>) graph2.AppointmentRecords).Current);
          }
          FSSODet fssoDet = PXResult<FSSODet, FSAppointmentDet>.op_Implicit(pxResult);
          if (fssoDet != null && !string.IsNullOrEmpty(fssoDet.LineRef))
          {
            graph3 = this.GetServiceOrderGraph(true);
            if (((PXSelectBase<FSServiceOrder>) graph3.ServiceOrderRecords).Current == null || ((PXSelectBase<FSServiceOrder>) graph3.ServiceOrderRecords).Current.RefNbr != fsAppointmentDet.RefNbr || ((PXSelectBase<FSServiceOrder>) graph3.ServiceOrderRecords).Current.SrvOrdType != fsAppointmentDet.SrvOrdType)
              ((PXSelectBase<FSServiceOrder>) graph3.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) graph3.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) fssoDet.RefNbr, new object[1]
              {
                (object) fssoDet.SrvOrdType
              }));
            ((PXSelectBase<FSSODet>) graph3.ServiceOrderDetails).Delete(fssoDet);
            if (((PXGraph) graph3).IsDirty)
              ((PXAction) graph3.Save).Press();
            if (((PXSelectBase<FSServiceOrder>) graph3.ServiceOrderRecords).Current != null && ((PXGraph) this.Base).Caches[typeof (FSServiceOrder)].GetStatus((object) ((PXSelectBase<FSServiceOrder>) graph3.ServiceOrderRecords).Current) == 1)
              graph1.Caches[typeof (FSServiceOrder)].Update((object) ((PXSelectBase<FSServiceOrder>) graph3.ServiceOrderRecords).Current);
          }
        }
label_18:
        if (e.Operation != 3 && extension.FSEntityNoteID.HasValue)
        {
          if (extension.FSEntityType == "PX.Objects.FS.FSServiceOrder")
          {
            FSServiceOrder entityRow = (FSServiceOrder) new EntityHelper((PXGraph) this.Base).GetEntityRow(typeof (FSServiceOrder), extension.FSEntityNoteID);
            FSServiceOrder fsServiceOrder = this.UpdateServiceOrderDetail(graph3, entityRow, row, extension);
            if (entityRow != null && ((PXGraph) this.Base).Caches[typeof (FSServiceOrder)].GetStatus((object) entityRow) == 1)
            {
              graph1.Caches[typeof (FSServiceOrder)].Update((object) fsServiceOrder);
              graph1.SelectTimeStamp();
            }
          }
          if (extension.FSEntityType == "PX.Objects.FS.FSAppointment")
          {
            FSAppointment entityRow = (FSAppointment) new EntityHelper((PXGraph) this.Base).GetEntityRow(typeof (FSAppointment), extension.FSEntityNoteID);
            FSAppointment fsAppointment = this.UpdateAppointmentDetail(graph2, entityRow, row, extension);
            if (entityRow != null && ((PXGraph) this.Base).Caches[typeof (FSAppointment)].GetStatus((object) entityRow) == 1)
            {
              graph1.Caches[typeof (FSAppointment)].Update((object) fsAppointment);
              graph1.SelectTimeStamp();
            }
          }
        }
      }
    }
    if (row == null || e.TranStatus != null)
      return;
    Note note = PXResultset<Note>.op_Implicit(PXSelectBase<Note, PXSelect<Note, Where<Note.noteID, Equal<Required<Note.noteID>>>>.Config>.SelectSingleBound(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<EPExpenseClaimDetails>>) e).Cache.Graph, (object[]) null, new object[1]
    {
      ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<EPExpenseClaimDetails>>) e).Cache.GetValue((object) row, typeof (FSxEPExpenseClaimDetails.fsEntityNoteID).Name)
    }));
    if (note == null || note.EntityType == null)
      return;
    Type type = note.NoteID.With<Guid?, object>((Func<Guid?, object>) (id => new EntityHelper((PXGraph) this.Base).GetEntityRow(new Guid?(id.Value), true))).GetType();
    if (!(type != (Type) null) || graph1.Views.Caches.Contains(type))
      return;
    PXCache cach = graph1.Caches[type];
    object entityRow1 = new EntityHelper(graph1).GetEntityRow(type, note.NoteID);
    if (cach.GetStatus(entityRow1) != 1)
      return;
    cach.PersistUpdated(entityRow1);
  }

  public virtual bool IsFSDocumentBilledOrClosed(object row, string fsEntityType)
  {
    return SM_ExpenseClaimDetailEntry.IsFSDocumentBilledOrClosedInt(row, fsEntityType);
  }

  public static bool IsFSDocumentBilledOrClosedInt(object row, string fsEntityType)
  {
    if (row == null)
      return false;
    switch (fsEntityType)
    {
      case "PX.Objects.FS.FSAppointment":
        FSAppointment fsAppointment = (FSAppointment) row;
        return fsAppointment.Closed.GetValueOrDefault() || fsAppointment.Billed.GetValueOrDefault();
      case "PX.Objects.FS.FSServiceOrder":
        FSServiceOrder fsServiceOrder = (FSServiceOrder) row;
        return fsServiceOrder.Closed.GetValueOrDefault() || fsServiceOrder.Billed.GetValueOrDefault();
      default:
        return false;
    }
  }

  public virtual bool IsFSDocumentRelatedToProjects(object row, string fsEntityType)
  {
    return SM_ExpenseClaimDetailEntry.IsFSDocumentRelatedToProjectsInt(row, fsEntityType);
  }

  public static bool IsFSDocumentRelatedToProjectsInt(object row, string fsEntityType)
  {
    if (row == null)
      return false;
    int? projectID = new int?();
    if (fsEntityType == "PX.Objects.FS.FSAppointment")
      projectID = ((FSAppointment) row).ProjectID;
    if (fsEntityType == "PX.Objects.FS.FSServiceOrder")
      projectID = ((FSServiceOrder) row).ProjectID;
    return projectID.HasValue && !ProjectDefaultAttribute.IsNonProject(projectID);
  }

  public virtual FSServiceOrder GetServiceOrderRelated(
    PXGraph graph,
    string srvOrdType,
    string refNbr)
  {
    return SM_ExpenseClaimDetailEntry.GetServiceOrderRelatedInt(graph, srvOrdType, refNbr);
  }

  public static FSServiceOrder GetServiceOrderRelatedInt(
    PXGraph graph,
    string srvOrdType,
    string refNbr)
  {
    if (string.IsNullOrEmpty(srvOrdType) || string.IsNullOrEmpty(refNbr))
      return (FSServiceOrder) null;
    return PXResultset<FSServiceOrder>.op_Implicit(PXSelectBase<FSServiceOrder, PXSelect<FSServiceOrder, Where<FSServiceOrder.srvOrdType, Equal<Required<FSServiceOrder.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Required<FSServiceOrder.refNbr>>>>>.Config>.Select(graph, new object[2]
    {
      (object) srvOrdType,
      (object) refNbr
    }));
  }

  public virtual FSServiceOrder UpdateServiceOrderDetail(
    ServiceOrderEntry graph,
    FSServiceOrder serviceOrder,
    EPExpenseClaimDetails row,
    FSxEPExpenseClaimDetails extRow)
  {
    return SM_ExpenseClaimDetailEntry.UpdateServiceOrderDetailInt(graph = this.GetServiceOrderGraph(false), serviceOrder, row, extRow);
  }

  public static FSServiceOrder UpdateServiceOrderDetailInt(
    ServiceOrderEntry graph,
    FSServiceOrder serviceOrder,
    EPExpenseClaimDetails row,
    FSxEPExpenseClaimDetails extRow)
  {
    if (serviceOrder != null)
    {
      if (((PXSelectBase<FSServiceOrder>) graph.ServiceOrderRecords).Current == null || ((PXSelectBase<FSServiceOrder>) graph.ServiceOrderRecords).Current.RefNbr != serviceOrder.RefNbr || ((PXSelectBase<FSServiceOrder>) graph.ServiceOrderRecords).Current.SrvOrdType != serviceOrder.SrvOrdType)
        ((PXSelectBase<FSServiceOrder>) graph.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) graph.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) serviceOrder.RefNbr, new object[1]
        {
          (object) serviceOrder.SrvOrdType
        }));
      FSSODet dacRow = PXResultset<FSSODet>.op_Implicit(PXSelectBase<FSSODet, PXSelect<FSSODet, Where<FSSODet.linkedEntityType, Equal<Required<FSSODet.linkedEntityType>>, And<FSSODet.linkedDocRefNbr, Equal<Required<FSSODet.linkedDocRefNbr>>>>>.Config>.Select((PXGraph) graph, new object[2]
      {
        (object) "ER",
        (object) row.ClaimDetailCD
      }));
      SM_ExpenseClaimDetailEntry.InsertUpdateDocDetailInt<FSSODet>((PXSelectBase) graph.ServiceOrderDetails, (object) dacRow, row, extRow);
      if (((PXGraph) graph).IsDirty)
      {
        ((PXAction) graph.Save).Press();
        return ((PXSelectBase<FSServiceOrder>) graph.ServiceOrderRecords).Current;
      }
    }
    return serviceOrder;
  }

  public virtual void InsertUpdateDocDetail<DAC>(
    PXSelectBase dacView,
    object dacRow,
    EPExpenseClaimDetails epExpenseClaimRow,
    FSxEPExpenseClaimDetails fsxEPExpenseClaimDetails)
    where DAC : class, IBqlTable, IFSSODetBase, new()
  {
    SM_ExpenseClaimDetailEntry.InsertUpdateDocDetailInt<DAC>(dacView, dacRow, epExpenseClaimRow, fsxEPExpenseClaimDetails);
  }

  public static void InsertUpdateDocDetailInt<DAC>(
    PXSelectBase dacView,
    object dacRow,
    EPExpenseClaimDetails epExpenseClaimRow,
    FSxEPExpenseClaimDetails fsxEPExpenseClaimDetails)
    where DAC : class, IBqlTable, IFSSODetBase, new()
  {
    DAC dac1 = (DAC) dacRow;
    bool? nullable1;
    if ((object) dac1 == null)
    {
      DAC dac2 = new DAC();
      dac2.LineType = "NSTKI";
      dac2.BillingRule = "FLRA";
      dac2.InventoryID = epExpenseClaimRow.InventoryID;
      dac2.ProjectID = epExpenseClaimRow.ContractID;
      dac2.ProjectTaskID = epExpenseClaimRow.TaskID;
      dac2.LinkedEntityType = "ER";
      dac2.LinkedDocRefNbr = epExpenseClaimRow.ClaimDetailCD;
      // ISSUE: variable of a boxed type
      __Boxed<DAC> local = (object) dac2;
      nullable1 = fsxEPExpenseClaimDetails.FSBillable;
      bool? nullable2 = nullable1.HasValue ? new bool?(!nullable1.GetValueOrDefault()) : new bool?();
      local.IsBillable = nullable2;
      dac1 = (DAC) dacView.Cache.Insert((object) dac2);
    }
    DAC dac3 = (DAC) dacView.Cache.CreateCopy((object) dac1);
    bool flag = false;
    if (dac3.TranDesc != epExpenseClaimRow.TranDesc)
    {
      dac3.TranDesc = epExpenseClaimRow.TranDesc;
      flag = true;
    }
    if (dac3.UOM != epExpenseClaimRow.UOM)
    {
      dac3.UOM = epExpenseClaimRow.UOM;
      flag = true;
    }
    int? estimatedDuration = dac3.EstimatedDuration;
    int num = 0;
    if (!(estimatedDuration.GetValueOrDefault() == num & estimatedDuration.HasValue))
    {
      dac3.EstimatedDuration = new int?(0);
      flag = true;
    }
    Decimal? nullable3 = dac3.EstimatedQty;
    Decimal? qty = epExpenseClaimRow.Qty;
    if (!(nullable3.GetValueOrDefault() == qty.GetValueOrDefault() & nullable3.HasValue == qty.HasValue))
    {
      dac3.EstimatedQty = epExpenseClaimRow.Qty;
      flag = true;
    }
    Decimal? nullable4 = dac3.Qty;
    nullable3 = epExpenseClaimRow.Qty;
    if (!(nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue))
    {
      dac3.Qty = epExpenseClaimRow.Qty;
      flag = true;
    }
    if (flag)
    {
      dac3 = (DAC) dacView.Cache.Update((object) dac3);
      flag = false;
    }
    nullable1 = dac3.IsBillable;
    bool? fsBillable = fsxEPExpenseClaimDetails.FSBillable;
    if (!(nullable1.GetValueOrDefault() == fsBillable.GetValueOrDefault() & nullable1.HasValue == fsBillable.HasValue))
    {
      dac3.IsBillable = fsxEPExpenseClaimDetails.FSBillable;
      flag = true;
    }
    Decimal? nullable5 = fsxEPExpenseClaimDetails.FSBillable.GetValueOrDefault() ? epExpenseClaimRow.CuryUnitCost : new Decimal?(0M);
    nullable3 = dac3.CuryUnitPrice;
    nullable4 = nullable5;
    if (!(nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue))
    {
      dac3.CuryUnitPrice = nullable5;
      flag = true;
    }
    nullable4 = dac3.CuryUnitCost;
    nullable3 = epExpenseClaimRow.CuryUnitCost;
    if (!(nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue))
    {
      dac3.CuryUnitCost = epExpenseClaimRow.CuryUnitCost;
      flag = true;
    }
    nullable3 = dac3.CuryExtCost;
    nullable4 = epExpenseClaimRow.CuryTranAmt;
    if (!(nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue))
    {
      dac3.CuryExtCost = epExpenseClaimRow.CuryTranAmt;
      flag = true;
    }
    Decimal? nullable6 = fsxEPExpenseClaimDetails.FSBillable.GetValueOrDefault() ? epExpenseClaimRow.CuryTranAmtWithTaxes : new Decimal?(0M);
    nullable4 = dac3.CuryBillableExtPrice;
    nullable3 = nullable6;
    if (!(nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue))
    {
      dac3.CuryBillableExtPrice = nullable6;
      flag = true;
    }
    int? nullable7 = dac3.ProjectTaskID;
    int? taskId = epExpenseClaimRow.TaskID;
    if (!(nullable7.GetValueOrDefault() == taskId.GetValueOrDefault() & nullable7.HasValue == taskId.HasValue))
    {
      dac3.ProjectTaskID = epExpenseClaimRow.TaskID;
      flag = true;
    }
    int? costCodeId = dac3.CostCodeID;
    nullable7 = epExpenseClaimRow.CostCodeID;
    if (!(costCodeId.GetValueOrDefault() == nullable7.GetValueOrDefault() & costCodeId.HasValue == nullable7.HasValue))
    {
      dac3.CostCodeID = epExpenseClaimRow.CostCodeID;
      flag = true;
    }
    if (!flag)
      return;
    DAC dac4 = (DAC) dacView.Cache.Update((object) dac3);
  }

  public virtual FSAppointment UpdateAppointmentDetail(
    AppointmentEntry graph,
    FSAppointment appointment,
    EPExpenseClaimDetails row,
    FSxEPExpenseClaimDetails extRow)
  {
    return SM_ExpenseClaimDetailEntry.UpdateAppointmentDetailInt(graph = this.GetAppointmentGraph(false), appointment, row, extRow);
  }

  public static FSAppointment UpdateAppointmentDetailInt(
    AppointmentEntry graph,
    FSAppointment appointment,
    EPExpenseClaimDetails row,
    FSxEPExpenseClaimDetails extRow)
  {
    if (appointment != null)
    {
      if (((PXSelectBase<FSAppointment>) graph.AppointmentRecords).Current == null || ((PXSelectBase<FSAppointment>) graph.AppointmentRecords).Current.RefNbr != appointment.RefNbr || ((PXSelectBase<FSAppointment>) graph.AppointmentRecords).Current.SrvOrdType != appointment.SrvOrdType)
        ((PXSelectBase<FSAppointment>) graph.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) graph.AppointmentRecords).Search<FSAppointment.refNbr>((object) appointment.RefNbr, new object[1]
        {
          (object) appointment.SrvOrdType
        }));
      FSAppointmentDet dacRow = PXResultset<FSAppointmentDet>.op_Implicit(PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.linkedEntityType, Equal<Required<FSAppointmentDet.linkedEntityType>>, And<FSAppointmentDet.linkedDocRefNbr, Equal<Required<FSAppointmentDet.linkedDocRefNbr>>>>>.Config>.Select((PXGraph) graph, new object[2]
      {
        (object) "ER",
        (object) row.ClaimDetailCD
      }));
      SM_ExpenseClaimDetailEntry.InsertUpdateDocDetailInt<FSAppointmentDet>((PXSelectBase) graph.AppointmentDetails, (object) dacRow, row, extRow);
      if (((PXGraph) graph).IsDirty)
      {
        ((PXAction) graph.Save).Press();
        return ((PXSelectBase<FSAppointment>) graph.AppointmentRecords).Current;
      }
    }
    return appointment;
  }
}
