// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceContractEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class ServiceContractEntry : 
  ServiceContractEntryBase<ServiceContractEntry, FSServiceContract, Where<FSServiceContract.recordType, Equal<ListField_RecordType_ContractSchedule.ServiceContract>>>
{
  public PXAction<FSServiceContract> OpenScheduleScreen;
  public PXAction<FSServiceContract> OpenScheduleScreenByScheduleDetService;
  public PXAction<FSServiceContract> OpenScheduleScreenByScheduleDetPart;

  public ServiceContractEntry() => ((PXSelectBase) this.ContractSchedules).AllowUpdate = false;

  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type")]
  [PXDefault]
  [PXRestrictor(typeof (Where<FSSrvOrdType.active, Equal<True>>), null, new System.Type[] {})]
  [FSSelectorContractSrvOrdType]
  protected virtual void FSContractSchedule_SrvOrdType_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "Location ID")]
  [PXMergeAttributes]
  protected virtual void FSContractSchedule_CustomerLocationID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXBool]
  [PXDefault(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMCostCode.isProjectOverride> e)
  {
  }

  protected override void _(PX.Data.Events.RowSelecting<FSServiceContract> e)
  {
    if (e.Row == null)
      return;
    base._(e);
    FSServiceContract row = e.Row;
    row.HasProcessedSchedule = new bool?(false);
    row.HasSchedule = new bool?(false);
    using (new PXConnectionScope())
    {
      IEnumerable<FSSchedule> source = GraphHelper.RowCast<FSSchedule>((IEnumerable) PXSelectBase<FSSchedule, PXSelectReadonly<FSSchedule, Where<FSSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>, And<FSSchedule.entityID, Equal<Required<FSSchedule.entityID>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.ServiceContractID
      }));
      List<FSSchedule> list = source != null ? source.ToList<FSSchedule>() : (List<FSSchedule>) null;
      row.HasSchedule = new bool?(list.Count<FSSchedule>() > 0);
      row.HasProcessedSchedule = new bool?(list.Where<FSSchedule>((Func<FSSchedule, bool>) (x => x.LastGeneratedElementDate.HasValue)).Count<FSSchedule>() > 0);
    }
  }

  protected override void _(PX.Data.Events.RowSelected<FSServiceContract> e)
  {
    base._(e);
    if (e.Row == null)
      return;
    FSServiceContract row1 = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSServiceContract>>) e).Cache;
    PXCache pxCache = cache;
    FSServiceContract row2 = e.Row;
    bool? processedSchedule = row1.HasProcessedSchedule;
    bool flag = false;
    int num = processedSchedule.GetValueOrDefault() == flag & processedSchedule.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<FSServiceContract.scheduleGenType>(pxCache, (object) row2, num != 0);
    this.EnableDisableRenewalFields(cache, row1);
    SharedFunctions.ServiceContractDynamicDropdown(cache, row1);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.scheduleGenType> e)
  {
    if (e.Row == null)
      return;
    FSServiceContract row = e.Row;
    foreach (PXResult<FSContractSchedule> pxResult in ((PXSelectBase<FSContractSchedule>) this.ContractSchedules).Select(Array.Empty<object>()))
    {
      FSContractSchedule contractSchedule = PXResult<FSContractSchedule>.op_Implicit(pxResult);
      if (!contractSchedule.LastGeneratedElementDate.HasValue && contractSchedule.ScheduleGenType != row.ScheduleGenType)
        ((PXSelectBase<FSContractSchedule>) this.ContractSchedules).SetValueExt<FSSchedule.scheduleGenType>(contractSchedule, (object) row.ScheduleGenType);
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSContractSchedule> e)
  {
    if (e.Row == null)
      return;
    FSContractSchedule row = e.Row;
    SharedFunctions.ShowWarningScheduleNotProcessed(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSContractSchedule>>) e).Cache, (FSSchedule) row);
  }

  [PXButton]
  [PXUIField(DisplayName = "Add Schedule")]
  public override void AddSchedule()
  {
    if (((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Current != null)
      this.ValidateContact(((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Current);
    ServiceContractScheduleEntry instance = PXGraph.CreateInstance<ServiceContractScheduleEntry>();
    FSContractSchedule contractSchedule = new FSContractSchedule();
    contractSchedule.EntityID = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.ServiceContractID;
    contractSchedule.ScheduleGenType = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.ScheduleGenType;
    contractSchedule.ProjectID = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.ProjectID;
    ((PXSelectBase<FSContractSchedule>) instance.ContractScheduleRecords).Insert(contractSchedule);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXButton]
  [PXUIField]
  protected virtual void openScheduleScreen()
  {
    if (((PXSelectBase<FSContractSchedule>) this.ContractSchedules).Current != null)
    {
      ServiceContractScheduleEntry instance = PXGraph.CreateInstance<ServiceContractScheduleEntry>();
      ((PXSelectBase<FSContractSchedule>) instance.ContractScheduleRecords).Current = PXResultset<FSContractSchedule>.op_Implicit(((PXSelectBase<FSContractSchedule>) instance.ContractScheduleRecords).Search<FSSchedule.scheduleID>((object) ((PXSelectBase<FSContractSchedule>) this.ContractSchedules).Current.ScheduleID, new object[2]
      {
        (object) ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.ServiceContractID,
        (object) ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.CustomerID
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  [PXButton]
  [PXUIField]
  protected virtual void openScheduleScreenByScheduleDetService()
  {
    if (((PXSelectBase<FSScheduleDet>) this.ScheduleDetServicesByContract).Current != null)
    {
      ServiceContractScheduleEntry instance = PXGraph.CreateInstance<ServiceContractScheduleEntry>();
      ((PXSelectBase<FSContractSchedule>) instance.ContractScheduleRecords).Current = PXResultset<FSContractSchedule>.op_Implicit(((PXSelectBase<FSContractSchedule>) instance.ContractScheduleRecords).Search<FSSchedule.scheduleID>((object) ((PXSelectBase<FSScheduleDet>) this.ScheduleDetServicesByContract).Current.ScheduleID, new object[1]
      {
        (object) ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.ServiceContractID
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  [PXButton]
  [PXUIField]
  protected virtual void openScheduleScreenByScheduleDetPart()
  {
    if (((PXSelectBase<FSScheduleDet>) this.ScheduleDetPartsByContract).Current != null)
    {
      ServiceContractScheduleEntry instance = PXGraph.CreateInstance<ServiceContractScheduleEntry>();
      ((PXSelectBase<FSContractSchedule>) instance.ContractScheduleRecords).Current = PXResultset<FSContractSchedule>.op_Implicit(((PXSelectBase<FSContractSchedule>) instance.ContractScheduleRecords).Search<FSSchedule.scheduleID>((object) ((PXSelectBase<FSScheduleDet>) this.ScheduleDetPartsByContract).Current.ScheduleID, new object[1]
      {
        (object) ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.ServiceContractID
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  public override void CopySchedules(int? serviceContractID, DateTime? date)
  {
    if (!serviceContractID.HasValue || !date.HasValue)
      return;
    ServiceContractScheduleEntry instance = PXGraph.CreateInstance<ServiceContractScheduleEntry>();
    PXResultset<FSContractSchedule> pxResultset = PXSelectBase<FSContractSchedule, PXSelectReadonly<FSContractSchedule, Where<FSContractSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>, And<FSContractSchedule.entityID, Equal<Required<FSContractSchedule.entityID>>, And<FSSchedule.active, Equal<True>>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) serviceContractID
    });
    int? serviceContractId = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.ServiceContractID;
    foreach (PXResult<FSContractSchedule> pxResult1 in pxResultset)
    {
      FSContractSchedule contractSchedule1 = PXResult<FSContractSchedule>.op_Implicit(pxResult1);
      ((PXGraph) instance).Clear((PXClearOption) 1);
      try
      {
        instance.IsCopyContract = true;
        FSServiceContract fsServiceContract = FSServiceContract.PK.Find((PXGraph) this, contractSchedule1.EntityID);
        FSContractSchedule copy1 = PXCache<FSContractSchedule>.CreateCopy(contractSchedule1);
        copy1.EntityID = serviceContractId;
        copy1.ScheduleID = new int?();
        copy1.RefNbr = (string) null;
        copy1.NoteID = new Guid?();
        copy1.LastGeneratedElementDate = new DateTime?();
        copy1.EstimatedDurationTotal = new int?(0);
        copy1.ScheduleDuration = new int?(0);
        copy1.OrigServiceContractRefNbr = fsServiceContract?.RefNbr;
        copy1.OrigScheduleRefNbr = contractSchedule1.RefNbr;
        copy1.StartDate = date;
        DateTime? nullable1 = contractSchedule1.StartDate;
        if (nullable1.HasValue)
        {
          nullable1 = contractSchedule1.EndDate;
          if (nullable1.HasValue)
          {
            FSContractSchedule contractSchedule2 = copy1;
            DateTime dateTime1 = date.Value;
            ref DateTime local1 = ref dateTime1;
            nullable1 = contractSchedule1.EndDate;
            DateTime dateTime2 = nullable1.Value;
            ref DateTime local2 = ref dateTime2;
            nullable1 = contractSchedule1.StartDate;
            DateTime dateTime3 = nullable1.Value;
            double days = (double) local2.Subtract(dateTime3).Days;
            DateTime? nullable2 = new DateTime?(local1.AddDays(days));
            contractSchedule2.EndDate = nullable2;
          }
        }
        FSContractSchedule contractSchedule3 = ((PXSelectBase<FSContractSchedule>) instance.ContractScheduleRecords).Insert(copy1);
        SharedFunctions.CopyNotesAndFiles(((PXSelectBase) instance.ContractScheduleRecords).Cache, ((PXSelectBase) instance.ContractScheduleRecords).Cache, (object) contractSchedule1, (object) contractSchedule3, new bool?(true), new bool?(true));
        instance.Answers.CopyAllAttributes((object) contractSchedule3, (object) contractSchedule1);
        foreach (PXResult<FSScheduleDet> pxResult2 in PXSelectBase<FSScheduleDet, PXSelectReadonly<FSScheduleDet, Where<FSScheduleDet.scheduleID, Equal<Required<FSScheduleDet.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) contractSchedule1.ScheduleID
        }))
        {
          FSScheduleDet srcObj = PXResult<FSScheduleDet>.op_Implicit(pxResult2);
          FSScheduleDet copy2 = PXCache<FSScheduleDet>.CreateCopy(srcObj);
          copy2.ScheduleID = contractSchedule3.ScheduleID;
          copy2.LineNbr = new int?();
          copy2.NoteID = new Guid?();
          FSScheduleDet dstObj = (FSScheduleDet) ((PXSelectBase) instance.ScheduleDetails).Cache.Insert((object) copy2);
          SharedFunctions.CopyNotesAndFiles(((PXSelectBase) instance.ScheduleDetails).Cache, ((PXSelectBase) instance.ScheduleDetails).Cache, (object) srcObj, (object) dstObj, new bool?(true), new bool?(true));
        }
        ((PXGraph) instance).Actions.PressSave();
      }
      finally
      {
        instance.IsCopyContract = false;
      }
    }
  }

  public override void OpenCopiedContract(FSServiceContract contract)
  {
    ServiceContractEntry instance = PXGraph.CreateInstance<ServiceContractEntry>();
    ((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Search<FSServiceContract.refNbr>((object) contract.RefNbr, Array.Empty<object>()));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
    throw requiredException;
  }

  public virtual void ValidateContact(FSServiceContract row)
  {
    if (!row.CustomerContactID.HasValue)
      return;
    PX.Objects.CR.Contact contact = PX.Objects.CR.Contact.PK.Find((PXGraph) this, row.CustomerContactID);
    if (contact?.Status != "A")
      throw new PXException($"The contact {contact.DisplayName} is deactivated. To perform the operation, select an active contact.", new object[1]
      {
        (object) (PXErrorLevel) 4
      });
  }
}
