// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_TimeCardMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.FS;

public class SM_TimeCardMaint : PXGraphExtension<TimeCardMaint>
{
  public PXSelect<FSSetup> SetupRecord;
  public PXSelect<PMSetup> PMSetupRecord;
  public PXAction<EPTimeCard> OpenAppointment;
  public PXAction<EPTimeCard> normalizeTimecard;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>() && PXAccess.FeatureInstalled<FeaturesSet.timeReportingModule>();
  }

  /// <summary>
  /// Update ApprovedTime and actualDuration fields in the <c>AppointmentDetInfo</c> lines.
  /// </summary>
  public virtual void UpdateAppointmentFromApprovedTimeCard(PXCache cache)
  {
    AppointmentEntry graphAppointmentEntry = (AppointmentEntry) null;
    PXCache cach = ((PXGraph) this.Base).Caches[typeof (FSAppointmentLog)];
    foreach (PXResult<TimeCardMaint.EPTimecardDetail> pxResult in ((PXSelectBase<TimeCardMaint.EPTimecardDetail>) this.Base.Activities).Select(Array.Empty<object>()))
    {
      TimeCardMaint.EPTimecardDetail epTimecardDetail = PXResult<TimeCardMaint.EPTimecardDetail>.op_Implicit(pxResult);
      FSxPMTimeActivity extension = ((PXSelectBase) this.Base.Activities).Cache.GetExtension<FSxPMTimeActivity>((object) epTimecardDetail);
      if (extension.LogLineNbr.HasValue)
      {
        cach.ClearQueryCache();
        FSAppointmentLog fsAppointmentLogRow = PXResultset<FSAppointmentLog>.op_Implicit(PXSelectBase<FSAppointmentLog, PXSelect<FSAppointmentLog, Where<FSAppointmentLog.docID, Equal<Required<FSAppointmentLog.docID>>, And<FSAppointmentLog.lineNbr, Equal<Required<FSAppointmentLog.lineNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
        {
          (object) extension.AppointmentID,
          (object) extension.LogLineNbr
        }));
        if (fsAppointmentLogRow != null)
        {
          TimeCardHelper.LoadAppointmentGraph((PXGraph) this.Base, extension, fsAppointmentLogRow, ref graphAppointmentEntry);
          fsAppointmentLogRow.TimeCardCD = epTimecardDetail.TimeCardCD;
          fsAppointmentLogRow.ApprovedTime = new bool?(true);
          graphAppointmentEntry.SkipTimeCardUpdate = true;
          ((PXSelectBase<FSAppointmentLog>) graphAppointmentEntry.LogRecords).Update(fsAppointmentLogRow);
          graphAppointmentEntry.SkipTaxCalcAndSave();
        }
      }
    }
  }

  [PXUIField(DisplayName = "Open Appointment")]
  [PXLookupButton]
  protected virtual void openAppointment()
  {
    if (((PXSelectBase<TimeCardMaint.EPTimecardDetail>) this.Base.Activities).Current == null)
      return;
    FSxPMTimeActivity extension = ((PXSelectBase) this.Base.Activities).Cache.GetExtension<FSxPMTimeActivity>((object) ((PXSelectBase<TimeCardMaint.EPTimecardDetail>) this.Base.Activities).Current);
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    FSAppointment fsAppointment = PXResultset<FSAppointment>.op_Implicit(PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.appointmentID, Equal<Required<FSAppointment.appointmentID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) extension.AppointmentID
    }));
    if (fsAppointment == null)
      return;
    ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.refNbr>((object) fsAppointment.RefNbr, new object[1]
    {
      (object) fsAppointment.SrvOrdType
    }));
    if (((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current != null)
    {
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  [PXUIField(DisplayName = "Normalize Time Card")]
  [PXButton(Tooltip = "Normalize Time Card")]
  protected virtual void NormalizeTimecard()
  {
    foreach (PXResult<TimeCardMaint.EPTimecardDetail> pxResult in ((PXSelectBase<TimeCardMaint.EPTimecardDetail>) this.Base.Activities).Select(Array.Empty<object>()))
    {
      TimeCardMaint.EPTimecardDetail epTimecardDetail = PXResult<TimeCardMaint.EPTimecardDetail>.op_Implicit(pxResult);
      if (((PXSelectBase) this.Base.Activities).Cache.GetExtension<FSxPMTimeActivity>((object) epTimecardDetail).AppointmentID.HasValue)
      {
        ((PXSelectBase) this.Base.Activities).Cache.SetValue<FSxPMTimeActivity.lastBillable>((object) epTimecardDetail, (object) epTimecardDetail.IsBillable);
        ((PXSelectBase) this.Base.Activities).Cache.SetValue<TimeCardMaint.EPTimecardDetail.isBillable>((object) epTimecardDetail, (object) true);
        ((PXSelectBase<TimeCardMaint.EPTimecardDetail>) this.Base.Activities).Update(epTimecardDetail);
      }
    }
    ((PXAction) this.Base.normalizeTimecard).Press();
    foreach (PXResult<TimeCardMaint.EPTimecardDetail> pxResult in ((PXSelectBase<TimeCardMaint.EPTimecardDetail>) this.Base.Activities).Select(Array.Empty<object>()))
    {
      TimeCardMaint.EPTimecardDetail epTimecardDetail = PXResult<TimeCardMaint.EPTimecardDetail>.op_Implicit(pxResult);
      FSxPMTimeActivity extension = ((PXSelectBase) this.Base.Activities).Cache.GetExtension<FSxPMTimeActivity>((object) epTimecardDetail);
      if (extension.AppointmentID.HasValue)
      {
        ((PXSelectBase) this.Base.Activities).Cache.SetValue<TimeCardMaint.EPTimecardDetail.isBillable>((object) epTimecardDetail, (object) extension.LastBillable);
        ((PXSelectBase) this.Base.Activities).Cache.SetValue<FSxPMTimeActivity.lastBillable>((object) epTimecardDetail, (object) false);
        ((PXSelectBase<TimeCardMaint.EPTimecardDetail>) this.Base.Activities).Update(epTimecardDetail);
      }
    }
    if (!((PXSelectBase) this.Base.Activities).Cache.IsDirty || ((PXSelectBase) this.Base.Document).Cache.GetStatus((object) ((PXSelectBase<EPTimeCard>) this.Base.Document).Current) == 2)
      return;
    ((PXAction) this.Base.Save).Press();
  }

  protected virtual void _(
    PX.Data.Events.RowSelecting<TimeCardMaint.EPTimecardDetail> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<TimeCardMaint.EPTimecardDetail> e)
  {
    if (e.Row == null || !TimeCardHelper.IsTheTimeCardIntegrationEnabled((PXGraph) this.Base))
      return;
    TimeCardMaint.EPTimecardDetail row = e.Row;
    TimeCardHelper.PMTimeActivity_RowSelected_Handler(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<TimeCardMaint.EPTimecardDetail>>) e).Cache, (PMTimeActivity) row);
  }

  protected virtual void _(
    PX.Data.Events.RowInserting<TimeCardMaint.EPTimecardDetail> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowInserted<TimeCardMaint.EPTimecardDetail> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowUpdating<TimeCardMaint.EPTimecardDetail> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowUpdated<TimeCardMaint.EPTimecardDetail> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowDeleting<TimeCardMaint.EPTimecardDetail> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowDeleted<TimeCardMaint.EPTimecardDetail> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowPersisting<TimeCardMaint.EPTimecardDetail> e)
  {
    if (e.Row == null)
      return;
    TimeCardHelper.PMTimeActivity_RowPersisting_Handler(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<TimeCardMaint.EPTimecardDetail>>) e).Cache, (PXGraph) this.Base, (AppointmentEntry) null, (PMTimeActivity) e.Row, ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<TimeCardMaint.EPTimecardDetail>>) e).Args);
  }

  protected virtual void _(
    PX.Data.Events.RowPersisted<TimeCardMaint.EPTimecardDetail> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelecting<EPTimeCard> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<EPTimeCard> e)
  {
    if (e.Row == null)
      return;
    EPTimeCard row = e.Row;
    bool flag = TimeCardHelper.IsTheTimeCardIntegrationEnabled((PXGraph) this.Base);
    PXUIFieldAttribute.SetVisible<FSxPMTimeActivity.appointmentID>(((PXSelectBase) this.Base.Activities).Cache, (object) ((PXSelectBase<TimeCardMaint.EPTimecardDetail>) this.Base.Activities).Current, flag);
    PXUIFieldAttribute.SetVisible<FSxPMTimeActivity.appointmentCustomerID>(((PXSelectBase) this.Base.Activities).Cache, (object) ((PXSelectBase<TimeCardMaint.EPTimecardDetail>) this.Base.Activities).Current, flag);
    PXUIFieldAttribute.SetVisible<FSxPMTimeActivity.logLineNbr>(((PXSelectBase) this.Base.Activities).Cache, (object) ((PXSelectBase<TimeCardMaint.EPTimecardDetail>) this.Base.Activities).Current, flag);
    PXUIFieldAttribute.SetVisible<FSxPMTimeActivity.serviceID>(((PXSelectBase) this.Base.Activities).Cache, (object) ((PXSelectBase<TimeCardMaint.EPTimecardDetail>) this.Base.Activities).Current, flag);
  }

  protected virtual void _(PX.Data.Events.RowInserting<EPTimeCard> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<EPTimeCard> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<EPTimeCard> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<EPTimeCard> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<EPTimeCard> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<EPTimeCard> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<EPTimeCard> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<EPTimeCard> e)
  {
    if (e.Row == null || !TimeCardHelper.IsTheTimeCardIntegrationEnabled((PXGraph) this.Base))
      return;
    EPTimeCard row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<EPTimeCard>>) e).Cache;
    if (!row.IsApproved.GetValueOrDefault() || (bool) cache.GetValueOriginal<EPTimeCard.isApproved>((object) row) || e.TranStatus != null)
      return;
    this.UpdateAppointmentFromApprovedTimeCard(cache);
  }
}
