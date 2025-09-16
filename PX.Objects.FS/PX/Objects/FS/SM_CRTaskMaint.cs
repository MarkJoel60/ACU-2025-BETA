// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_CRTaskMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.FS;

public class SM_CRTaskMaint : PXGraphExtension<CRTaskMaint>
{
  protected bool _disableServiceID = true;
  protected System.Type _callerEntity;
  public PXSetup<FSSrvOrdType, LeftJoin<CROpportunity, On<FSSrvOrdType.srvOrdType, Equal<FSxCROpportunity.srvOrdType>>, LeftJoin<CRCase, On<FSSrvOrdType.srvOrdType, Equal<FSxCRCase.srvOrdType>>>>, Where<CROpportunity.noteID, Equal<Current<CRActivity.refNoteID>>, Or<Where<CRCase.noteID, Equal<Current<CRActivity.refNoteID>>>>>> ServiceOrderTypeSelected;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.GetCallerEntity((Guid?) ((PXSelectBase<CRActivity>) this.Base.Tasks).Current?.RefNoteID);
  }

  protected virtual void GetCallerEntity(Guid? refNoteID)
  {
    if (!refNoteID.HasValue)
      return;
    CRCase crCase = PXResultset<CRCase>.op_Implicit(PXSelectBase<CRCase, PXSelect<CRCase, Where<CRCase.noteID, Equal<Required<CRCase.noteID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) refNoteID
    }));
    if (crCase != null)
    {
      FSxCRCase extension = PXCache<CRCase>.GetExtension<FSxCRCase>(crCase);
      if (extension != null)
      {
        bool? sdEnabled = extension.SDEnabled;
        bool flag = false;
        if (sdEnabled.GetValueOrDefault() == flag & sdEnabled.HasValue)
          this._disableServiceID = false;
      }
      this._callerEntity = typeof (CRCase);
    }
    else
    {
      CROpportunity crOpportunity = PXResultset<CROpportunity>.op_Implicit(PXSelectBase<CROpportunity, PXSelect<CROpportunity, Where<CROpportunity.noteID, Equal<Required<CROpportunity.noteID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) refNoteID
      }));
      if (crOpportunity == null)
        return;
      FSxCROpportunity extension = PXCache<CROpportunity>.GetExtension<FSxCROpportunity>(crOpportunity);
      if (extension != null && extension.SDEnabled.GetValueOrDefault())
        this._disableServiceID = false;
      this._callerEntity = typeof (CROpportunity);
    }
  }

  public virtual void UpdateSubject(
    PXCache cache,
    PMTimeActivity pmTimeActivityRow,
    CRActivity crActivityRow)
  {
    FSxPMTimeActivity extension = cache.GetExtension<FSxPMTimeActivity>((object) pmTimeActivityRow);
    if (crActivityRow.Subject != null)
    {
      int num = crActivityRow.Subject.IndexOf("|");
      if (num != -1)
      {
        crActivityRow.Subject = crActivityRow.Subject.Substring(num + 1).Trim();
        if (crActivityRow.Subject == string.Empty)
          crActivityRow.Subject = (string) null;
      }
    }
    if (!extension.ServiceID.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, extension.ServiceID);
    if (inventoryItem == null || !(inventoryItem.ItemType == "S"))
      return;
    if (string.IsNullOrWhiteSpace(crActivityRow.Subject))
      crActivityRow.Subject = string.Empty;
    crActivityRow.Subject = $"(SERVICE){inventoryItem.Descr} | {crActivityRow.Subject}";
  }

  public virtual FSServiceOrder GetServiceOrderRecord(PXGraph graph, CRActivity crActivityRow)
  {
    return PXResultset<FSServiceOrder>.op_Implicit(PXSelectBase<FSServiceOrder, PXSelectJoin<FSServiceOrder, LeftJoin<CRCase, On<CRCase.caseCD, Equal<FSServiceOrder.sourceRefNbr>, And<FSServiceOrder.sourceType, Equal<ListField_SourceType_ServiceOrder.Case>>>, LeftJoin<CROpportunity, On<CROpportunity.opportunityID, Equal<FSServiceOrder.sourceRefNbr>, And<FSServiceOrder.sourceType, Equal<ListField_SourceType_ServiceOrder.Opportunity>>>, InnerJoin<CRActivity, On<CRActivity.refNoteID, Equal<CRCase.noteID>, Or<CRActivity.refNoteID, Equal<CROpportunity.noteID>>>>>>, Where<CRActivity.noteID, Equal<Required<CRActivity.noteID>>>>.Config>.Select(graph, new object[1]
    {
      (object) crActivityRow.NoteID
    }));
  }

  public virtual void UpdateSODetServiceRow(
    ServiceOrderEntry graphServiceOrder,
    FSSODet fsSODetRow,
    PMTimeActivity pmTimeActivity,
    FSxPMTimeActivity fsxPMTimeActivity)
  {
    if (fsSODetRow.LineType != "SERVI")
      ((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).SetValueExt<FSSODet.lineType>(fsSODetRow, (object) "SERVI");
    int? inventoryId = fsSODetRow.InventoryID;
    int? serviceId = fsxPMTimeActivity.ServiceID;
    if (!(inventoryId.GetValueOrDefault() == serviceId.GetValueOrDefault() & inventoryId.HasValue == serviceId.HasValue))
      ((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).SetValueExt<FSSODet.inventoryID>(fsSODetRow, (object) fsxPMTimeActivity.ServiceID);
    int? projectId1 = fsSODetRow.ProjectID;
    int? projectId2 = pmTimeActivity.ProjectID;
    if (!(projectId1.GetValueOrDefault() == projectId2.GetValueOrDefault() & projectId1.HasValue == projectId2.HasValue))
      ((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).SetValueExt<FSSODet.projectID>(fsSODetRow, (object) pmTimeActivity.ProjectID);
    int? projectTaskId1 = fsSODetRow.ProjectTaskID;
    int? projectTaskId2 = pmTimeActivity.ProjectTaskID;
    if (!(projectTaskId1.GetValueOrDefault() == projectTaskId2.GetValueOrDefault() & projectTaskId1.HasValue == projectTaskId2.HasValue))
      ((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).SetValueExt<FSSODet.projectTaskID>(fsSODetRow, (object) pmTimeActivity.ProjectTaskID);
    int? costCodeId1 = fsSODetRow.CostCodeID;
    int? costCodeId2 = pmTimeActivity.CostCodeID;
    if (!(costCodeId1.GetValueOrDefault() == costCodeId2.GetValueOrDefault() & costCodeId1.HasValue == costCodeId2.HasValue))
      ((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).SetValueExt<FSSODet.costCodeID>(fsSODetRow, (object) pmTimeActivity.CostCodeID);
    ((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).Update(fsSODetRow);
  }

  public virtual void InsertUpdateDeleteSODet(
    ServiceOrderEntry graphServiceOrder,
    PMTimeActivity pmTimeActivityRow,
    FSxPMTimeActivity fsxPMTimeActivityRow,
    PXDBOperation operation)
  {
    ((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).Current = PXResultset<FSSODet>.op_Implicit(((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).Search<FSSODet.sourceNoteID>((object) pmTimeActivityRow.NoteID, Array.Empty<object>()));
    if (((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).Current != null)
    {
      if (operation == 3 || !fsxPMTimeActivityRow.ServiceID.HasValue)
      {
        ((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).Delete(((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).Current);
        return;
      }
    }
    else
    {
      if (!fsxPMTimeActivityRow.ServiceID.HasValue)
        return;
      ((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).Current = ((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).Insert(new FSSODet()
      {
        SourceNoteID = pmTimeActivityRow.NoteID
      });
    }
    this.UpdateSODetServiceRow(graphServiceOrder, ((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).Current, pmTimeActivityRow, fsxPMTimeActivityRow);
  }

  public virtual void UpdateServiceOrderDetail(
    PXCache cache,
    CRActivity crActivityRow,
    PXDBOperation operation)
  {
    FSServiceOrder serviceOrderRecord = this.GetServiceOrderRecord(cache.Graph, crActivityRow);
    if (serviceOrderRecord == null)
      return;
    PMTimeActivity pmTimeActivityRow = PXResultset<PMTimeActivity>.op_Implicit(PXSelectBase<PMTimeActivity, PXSelect<PMTimeActivity, Where<PMTimeActivity.refNoteID, Equal<Required<PMTimeActivity.refNoteID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) crActivityRow.NoteID
    }));
    if (pmTimeActivityRow == null)
      return;
    FSxPMTimeActivity extension = PXCache<PMTimeActivity>.GetExtension<FSxPMTimeActivity>(pmTimeActivityRow);
    ServiceOrderEntry instance = PXGraph.CreateInstance<ServiceOrderEntry>();
    ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) serviceOrderRecord.RefNbr, new object[1]
    {
      (object) serviceOrderRecord.SrvOrdType
    }));
    this.InsertUpdateDeleteSODet(instance, pmTimeActivityRow, extension, operation);
    if (!((PXGraph) instance).IsDirty)
      return;
    ((PXAction) instance.Save).Press();
  }

  protected virtual void _(PX.Data.Events.RowSelecting<CRActivity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<CRActivity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<CRActivity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<CRActivity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<CRActivity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CRActivity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<CRActivity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<CRActivity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CRActivity> e)
  {
    if (e.Operation == 3 || this.Base.TimeActivity.Current == null || !PXAccess.FeatureInstalled<FeaturesSet.timeReportingModule>())
      return;
    this.UpdateSubject(((PXSelectBase) this.Base.TimeActivity).Cache, this.Base.TimeActivity.Current, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<CRActivity> e)
  {
    if (e.TranStatus != null || !PXAccess.FeatureInstalled<FeaturesSet.timeReportingModule>())
      return;
    CRActivity row = e.Row;
    this.UpdateServiceOrderDetail(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<CRActivity>>) e).Cache, row, e.Operation);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PMTimeActivity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMTimeActivity> e)
  {
    if (e.Row == null)
      return;
    if (this._callerEntity == typeof (CRCase))
    {
      PXUIFieldAttribute.SetEnabled<FSxPMTimeActivity.serviceID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTimeActivity>>) e).Cache, (object) e.Row, this._disableServiceID);
    }
    else
    {
      if (!(this._callerEntity == (System.Type) null) && !(this._callerEntity == typeof (CROpportunity)))
        return;
      PXUIFieldAttribute.SetVisible<FSxPMTimeActivity.serviceID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTimeActivity>>) e).Cache, (object) e.Row, false);
    }
  }

  protected virtual void _(PX.Data.Events.RowInserting<PMTimeActivity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<PMTimeActivity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<PMTimeActivity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMTimeActivity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PMTimeActivity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMTimeActivity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMTimeActivity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PMTimeActivity> e)
  {
  }
}
