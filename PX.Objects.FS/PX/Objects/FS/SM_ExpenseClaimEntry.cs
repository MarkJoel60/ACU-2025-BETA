// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_ExpenseClaimEntry
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

#nullable enable
namespace PX.Objects.FS;

public class SM_ExpenseClaimEntry : PXGraphExtension<
#nullable disable
ExpenseClaimEntry>
{
  private ServiceOrderEntry _ServiceOrderGraph;
  private AppointmentEntry _AppointmentGraph;
  public PXSelect<FSServiceOrder> ServiceOrderRecords;
  public PXSelect<FSAppointment> AppointmentRecords;
  public string _FSEntityType;

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

  [PXMergeAttributes]
  [PXString]
  [PXDefault]
  protected virtual void EPExpenseClaimDetails_FSEntityType_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSxEPExpenseClaimDetails.fsEntityType> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPExpenseClaimDetails.billable> e)
  {
    if (e.Row == null || !((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails.billable>>) e).Cache.GetExtension<FSxEPExpenseClaimDetails>((object) (EPExpenseClaimDetails) e.Row).FSBillable.GetValueOrDefault() || !((bool?) e.NewValue).GetValueOrDefault())
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
      if (extension.FSEntityType == null)
        extension.FSEntityType = MainTools.GetLongName(entityRow.GetType());
      extension.IsDocBilledOrClosed = new bool?(this.IsFSDocumentBilledOrClosed(entityRow, extension.FSEntityType));
      extension.IsDocRelatedToProject = new bool?(this.IsFSDocumentRelatedToProjects(entityRow, extension.FSEntityType));
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<EPExpenseClaimDetails> e)
  {
    if (e.Row == null)
      return;
    FSxEPExpenseClaimDetails extension = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache.GetExtension<FSxEPExpenseClaimDetails>((object) e.Row);
    PXDefaultAttribute.SetPersistingCheck<FSxEPExpenseClaimDetails.fsEntityNoteID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) e.Row, (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetEnabled<FSxEPExpenseClaimDetails.fsEntityNoteID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) e.Row, false);
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache;
    EPExpenseClaimDetails row = e.Row;
    bool? nullable;
    int num;
    if (extension.FSEntityNoteID.HasValue)
    {
      nullable = extension.IsDocBilledOrClosed;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = extension.IsDocRelatedToProject;
        bool flag2 = false;
        num = nullable.GetValueOrDefault() == flag2 & nullable.HasValue ? 1 : 0;
        goto label_6;
      }
    }
    num = 0;
label_6:
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
      object entityRow1 = new EntityHelper((PXGraph) this.Base).GetEntityRow(extension.FSEntityNoteID, true);
      if (entityRow1 != null)
      {
        extension.FSEntityType = MainTools.GetLongName(entityRow1.GetType());
        extension.IsDocBilledOrClosed = new bool?(this.IsFSDocumentBilledOrClosed(entityRow1, extension.FSEntityType));
        extension.IsDocRelatedToProject = new bool?(this.IsFSDocumentRelatedToProjects(entityRow1, extension.FSEntityType));
      }
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
              goto label_20;
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
label_20:
        if (e.Operation != 3 && extension.FSEntityNoteID.HasValue)
        {
          if (extension.FSEntityType == "PX.Objects.FS.FSServiceOrder")
          {
            FSServiceOrder entityRow2 = (FSServiceOrder) new EntityHelper((PXGraph) this.Base).GetEntityRow(typeof (FSServiceOrder), extension.FSEntityNoteID);
            FSServiceOrder fsServiceOrder = this.UpdateServiceOrderDetail(graph3, entityRow2, row, extension);
            if (entityRow2 != null && ((PXGraph) this.Base).Caches[typeof (FSServiceOrder)].GetStatus((object) entityRow2) == 1)
            {
              graph1.Caches[typeof (FSServiceOrder)].Update((object) fsServiceOrder);
              graph1.SelectTimeStamp();
            }
          }
          if (extension.FSEntityType == "PX.Objects.FS.FSAppointment")
          {
            FSAppointment entityRow3 = (FSAppointment) new EntityHelper((PXGraph) this.Base).GetEntityRow(typeof (FSAppointment), extension.FSEntityNoteID);
            FSAppointment fsAppointment = this.UpdateAppointmentDetail(graph2, entityRow3, row, extension);
            if (entityRow3 != null && ((PXGraph) this.Base).Caches[typeof (FSAppointment)].GetStatus((object) entityRow3) == 1)
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
    object entityRow = new EntityHelper(graph1).GetEntityRow(type, note.NoteID);
    if (cach.GetStatus(entityRow) != 1)
      return;
    cach.PersistUpdated(entityRow);
  }

  public virtual bool IsFSDocumentBilledOrClosed(object row, string fsEntityType)
  {
    return SM_ExpenseClaimDetailEntry.IsFSDocumentBilledOrClosedInt(row, fsEntityType);
  }

  public virtual bool IsFSDocumentRelatedToProjects(object row, string fsEntityType)
  {
    return SM_ExpenseClaimDetailEntry.IsFSDocumentRelatedToProjectsInt(row, fsEntityType);
  }

  public virtual FSServiceOrder GetServiceOrderRelated(
    PXGraph graph,
    string srvOrdType,
    string refNbr)
  {
    return SM_ExpenseClaimDetailEntry.GetServiceOrderRelatedInt(graph, srvOrdType, refNbr);
  }

  public virtual FSServiceOrder UpdateServiceOrderDetail(
    ServiceOrderEntry graph,
    FSServiceOrder serviceOrder,
    EPExpenseClaimDetails row,
    FSxEPExpenseClaimDetails extRow)
  {
    return SM_ExpenseClaimDetailEntry.UpdateServiceOrderDetailInt(graph = this.GetServiceOrderGraph(false), serviceOrder, row, extRow);
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

  public virtual FSAppointment UpdateAppointmentDetail(
    AppointmentEntry graph,
    FSAppointment appointment,
    EPExpenseClaimDetails row,
    FSxEPExpenseClaimDetails extRow)
  {
    return SM_ExpenseClaimDetailEntry.UpdateAppointmentDetailInt(graph = this.GetAppointmentGraph(false), appointment, row, extRow);
  }

  public abstract class fsEntityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SM_ExpenseClaimEntry.fsEntityType>
  {
  }
}
