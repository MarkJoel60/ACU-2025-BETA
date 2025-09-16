// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceContractScheduleEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.DependencyInjection;
using PX.LicensePolicy;
using PX.Objects.CR;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable enable
namespace PX.Objects.FS;

public class ServiceContractScheduleEntry : 
  ServiceContractScheduleEntryBase<
  #nullable disable
  ServiceContractScheduleEntry, FSContractSchedule, FSSchedule.scheduleID, FSContractSchedule.entityID, FSContractSchedule.customerID>,
  IGraphWithInitialization
{
  public PXAction<FSContractSchedule> report;
  public PXAction<FSContractSchedule> openServiceContractInq;

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits == null)
      return;
    ((PXGraph) this).OnBeforeCommit += this._licenseLimits.GetCheckerDelegate<FSServiceContract>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (FSSchedule), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[2]
      {
        (PXDataFieldValue) new PXDataFieldValue<FSSchedule.customerID>((object) (int?) ((PXSelectBase<FSContractSchedule>) ((ServiceContractScheduleEntryBase<ServiceContractScheduleEntry, FSContractSchedule, FSSchedule.scheduleID, FSContractSchedule.entityID, FSContractSchedule.customerID>) graph).ContractScheduleRecords).Current?.CustomerID),
        (PXDataFieldValue) new PXDataFieldValue<FSSchedule.entityID>((object) (int?) ((PXSelectBase<FSContractSchedule>) ((ServiceContractScheduleEntryBase<ServiceContractScheduleEntry, FSContractSchedule, FSSchedule.scheduleID, FSContractSchedule.entityID, FSContractSchedule.customerID>) graph).ContractScheduleRecords).Current?.EntityID)
      }))
    });
    ((PXGraph) this).OnBeforeCommit += this._licenseLimits.GetCheckerDelegate<FSSchedule>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (FSScheduleDet), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[1]
      {
        (PXDataFieldValue) new PXDataFieldValue<FSScheduleDet.scheduleID>((object) (int?) ((PXSelectBase<FSContractSchedule>) ((ServiceContractScheduleEntryBase<ServiceContractScheduleEntry, FSContractSchedule, FSSchedule.scheduleID, FSContractSchedule.entityID, FSContractSchedule.customerID>) graph).ContractScheduleRecords).Current?.ScheduleID)
      }))
    });
  }

  public virtual IEnumerable scheduleProjectionRecords()
  {
    return (IEnumerable) this.Delegate_ScheduleProjectionRecords(((PXSelectBase) this.ContractScheduleRecords).Cache, (FSSchedule) ((PXSelectBase<FSContractSchedule>) this.ContractScheduleRecords).Current, ((PXSelectBase<PX.Objects.FS.FromToFilter>) this.FromToFilter).Current, "NRSC");
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Branch ID", Enabled = false)]
  [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  protected virtual void FSContractSchedule_BranchID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Branch Location ID", Enabled = false)]
  [FSSelectorBranchLocationByFSSchedule]
  [PXFormula(typeof (Default<FSSchedule.branchID>))]
  protected virtual void FSContractSchedule_BranchLocationID_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(Visible = false)]
  [PXDBCreatedByScreenID]
  protected virtual void FSContractSchedule_CreatedByScreenID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Description")]
  protected virtual void FSContractSchedule_RecurrenceDescription_CacheAttached(PXCache sender)
  {
  }

  public virtual bool IsTheScheduleExpired(FSContractSchedule fsContractScheduleRow)
  {
    if (fsContractScheduleRow == null || !fsContractScheduleRow.EndDate.HasValue)
      return false;
    DateTime? endDate = fsContractScheduleRow.EndDate;
    DateTime? businessDate = ((PXGraph) this).Accessinfo.BusinessDate;
    return endDate.HasValue & businessDate.HasValue && endDate.GetValueOrDefault() < businessDate.GetValueOrDefault();
  }

  [PXButton]
  [PXUIField(DisplayName = "Reports")]
  public virtual IEnumerable Report(PXAdapter adapter, [PXString(8, InputMask = "CC.CC.CC.CC")] string reportID)
  {
    List<FSContractSchedule> list = adapter.Get<FSContractSchedule>().ToList<FSContractSchedule>();
    if (!string.IsNullOrEmpty(reportID))
    {
      ((PXAction) this.Save).Press();
      Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
      PXReportRequiredException ex = (PXReportRequiredException) null;
      Dictionary<PrintSettings, PXReportRequiredException> reportsToPrint = new Dictionary<PrintSettings, PXReportRequiredException>();
      foreach (FSContractSchedule contractSchedule in list)
      {
        Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
        dictionary2["FSServiceContract.RefNbr"] = ((PXSelectBase<FSServiceContract>) this.CurrentServiceContract).Current?.RefNbr;
        dictionary2["FSSchedule.RefNbr"] = contractSchedule.RefNbr;
        string str = new NotificationUtility((PXGraph) this).SearchCustomerReport(reportID, contractSchedule.CustomerID, contractSchedule.BranchID);
        ex = PXReportRequiredException.CombineReport(ex, str, dictionary2, (CurrentLocalization) null);
        reportsToPrint = SMPrintJobMaint.AssignPrintJobToPrinter(reportsToPrint, dictionary2, adapter, new Func<string, string, int?, Guid?>(new NotificationUtility((PXGraph) this).SearchPrinter), "Customer", reportID, str, contractSchedule.BranchID, (CurrentLocalization) null);
      }
      if (ex != null)
        ((PXGraph) this).LongOperationManager.StartAsyncOperation((Func<CancellationToken, System.Threading.Tasks.Task>) (async ct =>
        {
          int num = await SMPrintJobMaint.CreatePrintJobGroups(reportsToPrint, ct) ? 1 : 0;
          throw ex;
        }));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual void OpenServiceContractInq()
  {
    ServiceContractInq instance = PXGraph.CreateInstance<ServiceContractInq>();
    ((PXSelectBase<ServiceContractFilter>) instance.Filter).Insert(new ServiceContractFilter()
    {
      ScheduleID = ((PXSelectBase<FSContractSchedule>) this.ContractScheduleRecords).Current.ScheduleID,
      ToDate = ((PXSelectBase<FSContractSchedule>) this.ContractScheduleRecords).Current.EndDate ?? ((PXSelectBase<FSContractSchedule>) this.ContractScheduleRecords).Current.StartDate
    });
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSContractSchedule, FSSchedule.scheduleStartTime> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSContractSchedule, FSSchedule.scheduleStartTime>, FSContractSchedule, object>) e).NewValue = (object) PXDBDateAndTimeAttribute.CombineDateTime(new DateTime?(PXTimeZoneInfo.Now), new DateTime?(PXTimeZoneInfo.Now));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSContractSchedule, FSContractSchedule.entityID> e)
  {
    if (e.Row == null)
      return;
    FSContractSchedule row = e.Row;
    if (!row.EntityID.HasValue)
      return;
    FSServiceContract fsServiceContract = FSServiceContract.PK.Find((PXGraph) this, row.EntityID);
    if (fsServiceContract == null)
      return;
    row.CustomerID = fsServiceContract.CustomerID;
    row.CustomerLocationID = fsServiceContract.CustomerLocationID;
    row.BranchID = fsServiceContract.BranchID;
    row.BranchLocationID = fsServiceContract.BranchLocationID;
    row.StartDate = fsServiceContract.StartDate;
    row.EndDate = fsServiceContract.EndDate;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSContractSchedule, FSSchedule.branchID> e)
  {
    if (e.Row == null)
      return;
    e.Row.BranchLocationID = new int?();
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSContractSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSContractSchedule> e)
  {
    if (e.Row == null)
      return;
    FSContractSchedule row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSContractSchedule>>) e).Cache;
    this.ContractSchedule_RowSelected_PartialHandler(cache, (FSSchedule) row);
    ((PXAction) this.openServiceContractInq).SetEnabled(!SharedFunctions.ShowWarningScheduleNotProcessed(cache, (FSSchedule) row) && !this.IsTheScheduleExpired(row));
    bool flag = row.ScheduleGenType == "AP";
    PXUIFieldAttribute.SetEnabled<FSSchedule.scheduleStartTime>(cache, (object) row, flag);
    PXDefaultAttribute.SetPersistingCheck<FSSchedule.scheduleStartTime>(cache, (object) row, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSContractSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSContractSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSContractSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSContractSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSContractSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSContractSchedule> e)
  {
    this.FSSchedule_Row_Deleted_PartialHandler(((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<FSContractSchedule>>) e).Cache, ((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<FSContractSchedule>>) e).Args);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSContractSchedule> e)
  {
    if (e.Row == null)
      return;
    FSContractSchedule row = e.Row;
    FSServiceContract current = ((PXSelectBase<FSServiceContract>) this.ContractSelected).Current;
    this.ContractSchedule_RowPersisting_PartialHandler(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSContractSchedule>>) e).Cache, current, (FSSchedule) row, e.Operation, "Equipment Management");
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSContractSchedule> e)
  {
  }
}
