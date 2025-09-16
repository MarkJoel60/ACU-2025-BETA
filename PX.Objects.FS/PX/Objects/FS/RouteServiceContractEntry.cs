// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteServiceContractEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.EP;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.FS;

public class RouteServiceContractEntry : 
  ServiceContractEntryBase<RouteServiceContractEntry, FSServiceContract, Where<FSServiceContract.recordType, Equal<ListField_RecordType_ContractSchedule.RouteServiceContract>>>
{
  public PXAction<FSServiceContract> OpenRouteScheduleScreen;
  public PXAction<FSServiceContract> OpenRouteScheduleScreenByScheduleDetService;
  public PXAction<FSServiceContract> OpenRouteScheduleScreenByScheduleDetPart;

  public RouteServiceContractEntry() => ((PXSelectBase) this.ContractSchedules).AllowUpdate = false;

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [FSSelectorContractRefNbrAttribute(typeof (ListField_RecordType_ContractSchedule.RouteServiceContract))]
  [AutoNumber(typeof (Search<FSSetup.serviceContractNumberingID>), typeof (AccessInfo.businessDate))]
  [PXFieldDescription]
  protected virtual void FSServiceContract_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [FSSelectorCustomerContractNbrAttribute(typeof (ListField_RecordType_ContractSchedule.RouteServiceContract), typeof (FSServiceContract.customerID))]
  [ServiceContractAutoNumber]
  [PXFieldDescription]
  protected virtual void FSServiceContract_CustomerContractNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(4, IsUnicode = true)]
  [PXDefault("IRSC")]
  protected virtual void FSServiceContract_RecordType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type")]
  [PXDefault]
  [PXRestrictor(typeof (Where<FSSrvOrdType.active, Equal<True>>), null, new Type[] {})]
  [FSSelectorRouteContractSrvOrdType]
  protected virtual void FSContractSchedule_SrvOrdType_CacheAttached(PXCache sender)
  {
  }

  [PXButton]
  [PXUIField(DisplayName = "Add Schedule")]
  public override void AddSchedule()
  {
    RouteServiceContractScheduleEntry instance = PXGraph.CreateInstance<RouteServiceContractScheduleEntry>();
    FSRouteContractSchedule contractSchedule = new FSRouteContractSchedule();
    contractSchedule.EntityID = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.ServiceContractID;
    contractSchedule.ScheduleGenType = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.ScheduleGenType;
    contractSchedule.ProjectID = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.ProjectID;
    ((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Insert(contractSchedule);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXButton]
  [PXUIField]
  protected virtual void openRouteScheduleScreen()
  {
    if (((PXSelectBase<FSContractSchedule>) this.ContractSchedules).Current != null)
    {
      RouteServiceContractScheduleEntry instance = PXGraph.CreateInstance<RouteServiceContractScheduleEntry>();
      ((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Current = PXResultset<FSRouteContractSchedule>.op_Implicit(((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Search<FSSchedule.scheduleID>((object) ((PXSelectBase<FSContractSchedule>) this.ContractSchedules).Current.ScheduleID, new object[2]
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
  protected virtual void openRouteScheduleScreenByScheduleDetService()
  {
    if (((PXSelectBase<FSScheduleDet>) this.ScheduleDetServicesByContract).Current != null)
    {
      RouteServiceContractScheduleEntry instance = PXGraph.CreateInstance<RouteServiceContractScheduleEntry>();
      ((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Current = PXResultset<FSRouteContractSchedule>.op_Implicit(((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Search<FSSchedule.scheduleID>((object) ((PXSelectBase<FSScheduleDet>) this.ScheduleDetServicesByContract).Current.ScheduleID, new object[2]
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
  protected virtual void openRouteScheduleScreenByScheduleDetPart()
  {
    if (((PXSelectBase<FSScheduleDet>) this.ScheduleDetPartsByContract).Current != null)
    {
      RouteServiceContractScheduleEntry instance = PXGraph.CreateInstance<RouteServiceContractScheduleEntry>();
      ((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Current = PXResultset<FSRouteContractSchedule>.op_Implicit(((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Search<FSSchedule.scheduleID>((object) ((PXSelectBase<FSScheduleDet>) this.ScheduleDetPartsByContract).Current.ScheduleID, new object[1]
      {
        (object) ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.ServiceContractID
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  protected virtual void _(Events.RowSelected<FSContractSchedule> e)
  {
    if (e.Row == null)
      return;
    FSContractSchedule row = e.Row;
    SharedFunctions.ShowWarningScheduleNotProcessed(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<FSContractSchedule>>) e).Cache, (FSSchedule) row);
  }

  protected override void _(Events.RowSelected<FSServiceContract> e)
  {
    base._(e);
    if (e.Row == null)
      return;
    FSServiceContract row = e.Row;
    this.EnableDisableRenewalFields(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<FSServiceContract>>) e).Cache, row);
    SharedFunctions.ServiceContractDynamicDropdown(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<FSServiceContract>>) e).Cache, row);
  }

  public override void CopySchedules(int? serviceContractID, DateTime? date)
  {
    if (!serviceContractID.HasValue || !date.HasValue)
      return;
    RouteServiceContractScheduleEntry instance = PXGraph.CreateInstance<RouteServiceContractScheduleEntry>();
    PXResultset<FSRouteContractSchedule> pxResultset = PXSelectBase<FSRouteContractSchedule, PXSelectReadonly<FSRouteContractSchedule, Where<FSRouteContractSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>, And<FSRouteContractSchedule.entityID, Equal<Required<FSRouteContractSchedule.entityID>>, And<FSSchedule.active, Equal<True>>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) serviceContractID
    });
    int? serviceContractId = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.ServiceContractID;
    foreach (PXResult<FSRouteContractSchedule> pxResult1 in pxResultset)
    {
      FSRouteContractSchedule contractSchedule1 = PXResult<FSRouteContractSchedule>.op_Implicit(pxResult1);
      ((PXGraph) instance).Clear((PXClearOption) 1);
      try
      {
        FSServiceContract fsServiceContract = FSServiceContract.PK.Find((PXGraph) this, contractSchedule1.EntityID);
        FSRouteContractSchedule copy1 = PXCache<FSRouteContractSchedule>.CreateCopy(contractSchedule1);
        copy1.EntityID = serviceContractId;
        copy1.ScheduleID = new int?();
        copy1.RefNbr = (string) null;
        copy1.NoteID = new Guid?();
        copy1.LastGeneratedElementDate = new DateTime?();
        copy1.EstimatedDurationTotal = new int?(0);
        copy1.ScheduleDuration = new int?(0);
        copy1.OrigServiceContractRefNbr = fsServiceContract?.RefNbr;
        copy1.OrigScheduleRefNbr = contractSchedule1.RefNbr;
        FSRouteContractSchedule contractSchedule2 = ((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Insert(copy1);
        SharedFunctions.CopyNotesAndFiles(((PXSelectBase) instance.ContractScheduleRecords).Cache, ((PXSelectBase) instance.ContractScheduleRecords).Cache, (object) contractSchedule1, (object) contractSchedule2, new bool?(true), new bool?(true));
        instance.Answers.CopyAllAttributes((object) contractSchedule2, (object) contractSchedule1);
        FSScheduleRoute copy2 = PXCache<FSScheduleRoute>.CreateCopy(PXResultset<FSScheduleRoute>.op_Implicit(PXSelectBase<FSScheduleRoute, PXSelectReadonly<FSScheduleRoute, Where<FSScheduleRoute.scheduleID, Equal<Required<FSScheduleRoute.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) contractSchedule1.ScheduleID
        })));
        copy2.ScheduleID = contractSchedule2.ScheduleID;
        copy2.NoteID = new Guid?();
        ((PXSelectBase<FSScheduleRoute>) instance.ScheduleRoutes).Insert(copy2);
        foreach (PXResult<FSScheduleDet> pxResult2 in PXSelectBase<FSScheduleDet, PXSelectReadonly<FSScheduleDet, Where<FSScheduleDet.scheduleID, Equal<Required<FSScheduleDet.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) contractSchedule1.ScheduleID
        }))
        {
          FSScheduleDet srcObj = PXResult<FSScheduleDet>.op_Implicit(pxResult2);
          FSScheduleDet copy3 = PXCache<FSScheduleDet>.CreateCopy(srcObj);
          copy3.ScheduleID = contractSchedule2.ScheduleID;
          copy3.LineNbr = new int?();
          copy3.NoteID = new Guid?();
          FSScheduleDet dstObj = (FSScheduleDet) ((PXSelectBase) instance.ScheduleDetails).Cache.Insert((object) copy3);
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
    RouteServiceContractEntry instance = PXGraph.CreateInstance<RouteServiceContractEntry>();
    ((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Search<FSServiceContract.refNbr>((object) contract.RefNbr, Array.Empty<object>()));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
    throw requiredException;
  }
}
