// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ContractGenerationEnqBase`4
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.FS.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.FS;

public class ContractGenerationEnqBase<TGraph, TPrimary, TFiltering, TRecordType> : PXGraph<TGraph>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
  where TFiltering : class, IBqlTable, new()
  where TRecordType : IConstant<string>, IBqlOperand, new()
{
  public AppointmentEntry graphAppointmentEntry;
  public ServiceOrderEntry graphServiceOrderEntry;
  public int? nextGenerationID;
  public PXFilter<TFiltering> Filter;
  public PXCancel<TFiltering> Cancel;
  public ContractGenerationEnqBase<TGraph, TPrimary, TFiltering, TRecordType>.ContractHistoryRecords_View ContractHistoryRecords;
  public ContractGenerationEnqBase<TGraph, TPrimary, TFiltering, TRecordType>.ErrorMessageRecords_View ErrorMessageRecords;
  public PXSelect<FSSchedule, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>> ScheduleSelected;
  public PXSelect<FSServiceContract, Where<FSServiceContract.serviceContractID, Equal<Required<FSServiceContract.serviceContractID>>>> ServiceContractSelected;
  public PXSelectJoin<FSScheduleDet, InnerJoin<FSSchedule, On<FSSchedule.scheduleID, Equal<FSScheduleDet.scheduleID>>>, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>, OrderBy<Asc<FSScheduleDet.sortOrder>>> ScheduleLinesSelected;
  public PXSelectJoin<FSScheduleDet, InnerJoin<FSSchedule, On<FSSchedule.scheduleID, Equal<FSScheduleDet.scheduleID>>>, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>, And<Where<FSScheduleDet.lineType, Equal<ListField_LineType_ALL.Service_Template>>>>, OrderBy<Asc<FSScheduleDet.sortOrder>>> ScheduleTemplatesSelected;
  public PXSelect<FSServiceTemplateDet, Where<FSServiceTemplateDet.serviceTemplateID, Equal<Required<FSServiceTemplate.serviceTemplateID>>>> ServiceTemplateSelected;
  public PXAction<TFiltering> ClearAll;
  public PXAction<TFiltering> OpenServiceContractScreenBySchedules;
  public PXAction<TFiltering> OpenServiceContractScreenByGenerationLogError;
  public PXAction<TFiltering> OpenScheduleScreenBySchedules;
  public PXAction<TFiltering> OpenScheduleScreenByGenerationLogError;

  /// <summary>
  /// Generates Appointments (Routes Contract) or Service Orders (Service Contract) for each TimeSlot in the [scheduleRules] List.
  /// </summary>
  public void GenerateAPPSOUpdateContracts(
    List<PX.Objects.FS.Scheduler.Schedule> scheduleRules,
    string recordType,
    DateTime? fromDate,
    DateTime? toDate,
    FSSchedule fsScheduleRow)
  {
    TimeSlotGenerator timeSlotGenerator = new TimeSlotGenerator();
    DateTime lastProcessDate = this.GetProcessEndDate(scheduleRules.ElementAt<PX.Objects.FS.Scheduler.Schedule>(0), toDate).Value;
    Period period1 = new Period(fromDate.Value, new DateTime?(lastProcessDate));
    if (!this.nextGenerationID.HasValue)
    {
      FSProcessIdentity fsProcessIdentity = new FSProcessIdentity();
      fsProcessIdentity.ProcessType = recordType;
      fsProcessIdentity.FilterFromTo = fromDate;
      fsProcessIdentity.FilterUpTo = toDate;
      ProcessIdentityMaint instance = PXGraph.CreateInstance<ProcessIdentityMaint>();
      ((PXSelectBase<FSProcessIdentity>) instance.processIdentityRecords).Insert(fsProcessIdentity);
      ((PXAction) instance.Save).Press();
      this.nextGenerationID = ((PXSelectBase<FSProcessIdentity>) instance.processIdentityRecords).Current.ProcessID;
    }
    Period period2 = period1;
    List<PX.Objects.FS.Scheduler.Schedule> scheduleList = scheduleRules;
    int? nextGenerationId = this.nextGenerationID;
    List<TimeSlot> calendar = timeSlotGenerator.GenerateCalendar(period2, (IEnumerable<PX.Objects.FS.Scheduler.Schedule>) scheduleList, nextGenerationId);
    DateTime? nullable = new DateTime?();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      PX.Objects.AR.Customer customer = PXResult<PX.Objects.AR.Customer>.op_Implicit(((IQueryable<PXResult<PX.Objects.AR.Customer>>) PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) fsScheduleRow.CustomerID
      })).FirstOrDefault<PXResult<PX.Objects.AR.Customer>>());
      if (customer.Status != "A" && customer.Status != "T")
        throw new PXException(PXMessages.LocalizeFormat("The customer status is '{0}'.", new object[1]
        {
          (object) new CustomerStatus.ListAttribute().ValueLabelDic[customer.Status]
        }));
      try
      {
        foreach (TimeSlot timeSlotServiceOrder in calendar)
        {
          nullable = new DateTime?(timeSlotServiceOrder.DateTimeBegin);
          switch (recordType)
          {
            case "NRSC":
              bool createAppointmentFlag = fsScheduleRow.ScheduleGenType == "AP";
              this.CreateServiceOrder(timeSlotServiceOrder, createAppointmentFlag);
              continue;
            case "IRSC":
              this.CreateServiceOrder(timeSlotServiceOrder, true, true);
              continue;
            default:
              continue;
          }
        }
        DateTime? lastGeneratedElementDate = new DateTime?();
        if (calendar.Count > 0)
          lastGeneratedElementDate = new DateTime?(calendar.Max<TimeSlot, DateTime>((Func<TimeSlot, DateTime>) (a => a.DateTimeBegin)).Date);
        this.CreateContractGenerationHistory(this.nextGenerationID.Value, scheduleRules.ElementAt<PX.Objects.FS.Scheduler.Schedule>(0).ScheduleID, lastProcessDate, lastGeneratedElementDate, recordType);
        this.UpdateGeneratedSchedule(scheduleRules.ElementAt<PX.Objects.FS.Scheduler.Schedule>(0).ScheduleID, new DateTime?(lastProcessDate), lastGeneratedElementDate, fsScheduleRow);
      }
      catch (Exception ex)
      {
        Exception withContextMessage = ExceptionHelper.GetExceptionWithContextMessage(PXMessages.Localize("Could not process this record."), ex);
        FSGenerationLogError generationLogError = new FSGenerationLogError();
        generationLogError.ProcessType = recordType;
        generationLogError.ErrorMessage = withContextMessage.Message;
        generationLogError.ScheduleID = new int?(scheduleRules.ElementAt<PX.Objects.FS.Scheduler.Schedule>(0).ScheduleID);
        generationLogError.GenerationID = this.nextGenerationID;
        generationLogError.ErrorDate = nullable;
        transactionScope.Dispose();
        GenerationLogErrorMaint instance = PXGraph.CreateInstance<GenerationLogErrorMaint>();
        ((PXSelectBase<FSGenerationLogError>) instance.LogErrorMessageRecords).Insert(generationLogError);
        ((PXAction) instance.Save).Press();
        throw withContextMessage;
      }
      transactionScope.Complete((PXGraph) this);
    }
  }

  /// <summary>Create an Appointment from a TimeSlot.</summary>
  protected void CreateAppointment(
    FSServiceOrder fsServiceOrderRow,
    TimeSlot timeSlotAppointment,
    FSSchedule fsScheduleRow,
    bool insertingFromServiceOrder,
    bool isARouteAppointment,
    bool setBillServiceContract)
  {
    if (this.graphAppointmentEntry != null)
    {
      ((PXGraph) this.graphAppointmentEntry).Clear((PXClearOption) 3);
    }
    else
    {
      this.graphAppointmentEntry = PXGraph.CreateInstance<AppointmentEntry>();
      this.graphAppointmentEntry.CalculateGoogleStats = false;
      this.graphAppointmentEntry.AvoidCalculateRouteStats = true;
      this.graphAppointmentEntry.IsGeneratingAppointment = true;
      this.graphAppointmentEntry.DisableServiceOrderUnboundFieldCalc = true;
    }
    this.graphAppointmentEntry.SkipManualTimeFlagUpdate = true;
    FSScheduleRoute fsScheduleRoute = (FSScheduleRoute) null;
    if (isARouteAppointment)
      fsScheduleRoute = PXResultset<FSScheduleRoute>.op_Implicit(PXSelectBase<FSScheduleRoute, PXSelect<FSScheduleRoute, Where<FSScheduleRoute.scheduleID, Equal<Required<FSScheduleRoute.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) fsServiceOrderRow.ScheduleID
      }));
    FSAppointment copy1 = (FSAppointment) ((PXSelectBase) this.graphAppointmentEntry.AppointmentRecords).Cache.CreateCopy((object) (((PXSelectBase<FSAppointment>) this.graphAppointmentEntry.AppointmentRecords).Current = ((PXSelectBase<FSAppointment>) this.graphAppointmentEntry.AppointmentRecords).Insert(new FSAppointment()
    {
      SrvOrdType = fsServiceOrderRow.SrvOrdType,
      ValidatedByDispatcher = new bool?(false),
      GeneratedByContract = new bool?(true)
    })));
    copy1.SORefNbr = fsServiceOrderRow.RefNbr;
    FSAppointment copy2 = (FSAppointment) ((PXSelectBase) this.graphAppointmentEntry.AppointmentRecords).Cache.CreateCopy((object) ((PXSelectBase<FSAppointment>) this.graphAppointmentEntry.AppointmentRecords).Update(copy1));
    copy2.ServiceContractID = fsServiceOrderRow.ServiceContractID;
    copy2.ScheduleID = fsServiceOrderRow.ScheduleID;
    copy2.GenerationID = fsServiceOrderRow.GenerationID;
    if (setBillServiceContract)
      copy2.BillServiceContractID = fsServiceOrderRow.ServiceContractID;
    copy2.DocDesc = fsServiceOrderRow.DocDesc;
    if (isARouteAppointment)
    {
      copy2.ScheduledDateTimeBegin = new DateTime?(timeSlotAppointment.DateTimeBegin);
      copy2.ScheduledDateTimeEnd = new DateTime?(timeSlotAppointment.DateTimeEnd);
    }
    else
    {
      DateTime? nullable = PXDBDateAndTimeAttribute.CombineDateTime(new DateTime?(timeSlotAppointment.DateTimeBegin), fsScheduleRow.ScheduleStartTime);
      copy2.ScheduledDateTimeBegin = nullable;
    }
    copy2.HandleManuallyScheduleTime = fsScheduleRow.OverrideDuration;
    DateTime dateTimeBegin;
    if (fsScheduleRow.OverrideDuration.GetValueOrDefault())
    {
      DateTime? nullable;
      ref DateTime? local = ref nullable;
      dateTimeBegin = copy2.ScheduledDateTimeBegin.Value;
      DateTime dateTime = dateTimeBegin.AddMinutes(Convert.ToDouble((object) fsScheduleRow.ScheduleDuration));
      local = new DateTime?(dateTime);
      copy2.ScheduledDateTimeEnd = nullable;
    }
    copy2.SalesPersonID = fsServiceOrderRow.SalesPersonID;
    copy2.Commissionable = fsServiceOrderRow.Commissionable;
    this.graphAppointmentEntry.Answers.CopyAllAttributes((object) copy2, (object) fsScheduleRow);
    if (fsScheduleRoute != null)
    {
      dateTimeBegin = timeSlotAppointment.DateTimeBegin;
      int? nullable;
      switch (dateTimeBegin.DayOfWeek)
      {
        case DayOfWeek.Sunday:
          nullable = fsScheduleRoute.RouteIDSunday;
          if (nullable.HasValue)
          {
            copy2.RouteID = fsScheduleRoute.RouteIDSunday;
            break;
          }
          break;
        case DayOfWeek.Monday:
          nullable = fsScheduleRoute.RouteIDMonday;
          if (nullable.HasValue)
          {
            copy2.RouteID = fsScheduleRoute.RouteIDMonday;
            break;
          }
          break;
        case DayOfWeek.Tuesday:
          nullable = fsScheduleRoute.RouteIDTuesday;
          if (nullable.HasValue)
          {
            copy2.RouteID = fsScheduleRoute.RouteIDTuesday;
            break;
          }
          break;
        case DayOfWeek.Wednesday:
          nullable = fsScheduleRoute.RouteIDWednesday;
          if (nullable.HasValue)
          {
            copy2.RouteID = fsScheduleRoute.RouteIDWednesday;
            break;
          }
          break;
        case DayOfWeek.Thursday:
          nullable = fsScheduleRoute.RouteIDThursday;
          if (nullable.HasValue)
          {
            copy2.RouteID = fsScheduleRoute.RouteIDThursday;
            break;
          }
          break;
        case DayOfWeek.Friday:
          nullable = fsScheduleRoute.RouteIDFriday;
          if (nullable.HasValue)
          {
            copy2.RouteID = fsScheduleRoute.RouteIDFriday;
            break;
          }
          break;
        case DayOfWeek.Saturday:
          nullable = fsScheduleRoute.RouteIDSaturday;
          if (nullable.HasValue)
          {
            copy2.RouteID = fsScheduleRoute.RouteIDSaturday;
            break;
          }
          break;
      }
      nullable = copy2.RouteID;
      if (!nullable.HasValue)
        copy2.RouteID = fsScheduleRoute.DfltRouteID;
      copy2.RoutePosition = new int?(int.Parse(fsScheduleRoute.GlobalSequence));
      if (copy2.DeliveryNotes == null)
        copy2.DeliveryNotes = fsScheduleRoute.DeliveryNotes;
    }
    ((PXSelectBase<FSAppointment>) this.graphAppointmentEntry.AppointmentRecords).Update(copy2);
    foreach (PXResult<FSSODet> pxResult in ((PXSelectBase<FSSODet>) this.graphServiceOrderEntry.ServiceOrderDetails).Select(Array.Empty<object>()))
    {
      FSSODet objSourceRow = PXResult<FSSODet>.op_Implicit(pxResult);
      AppointmentEntry.InsertDetailLine<FSAppointmentDet, FSSODet>(((PXSelectBase) this.graphAppointmentEntry.AppointmentDetails).Cache, (object) new FSAppointmentDet()
      {
        ScheduleID = objSourceRow.ScheduleID,
        ScheduleDetID = objSourceRow.ScheduleDetID
      }, ((PXSelectBase) this.graphServiceOrderEntry.ServiceOrderDetails).Cache, (object) objSourceRow, objSourceRow.NoteID, objSourceRow.SODetID, false, objSourceRow.TranDate, false, false);
    }
    foreach (PXResult<FSSOEmployee> pxResult in ((PXSelectBase<FSSOEmployee>) this.graphServiceOrderEntry.ServiceOrderEmployees).Select(Array.Empty<object>()))
    {
      FSSOEmployee fssoEmployee = PXResult<FSSOEmployee>.op_Implicit(pxResult);
      ((PXSelectBase<FSAppointmentEmployee>) this.graphAppointmentEntry.AppointmentServiceEmployees).Insert(new FSAppointmentEmployee()
      {
        EmployeeID = fssoEmployee.EmployeeID
      });
    }
    if (fsScheduleRow.VendorID.HasValue)
      ((PXSelectBase<FSAppointmentEmployee>) this.graphAppointmentEntry.AppointmentServiceEmployees).Insert(new FSAppointmentEmployee()
      {
        EmployeeID = fsScheduleRow.VendorID
      });
    ((PXAction) this.graphAppointmentEntry.Save).Press();
  }

  /// <summary>Create a Service Order from a TimeSlot.</summary>
  protected void CreateServiceOrder(
    TimeSlot timeSlotServiceOrder,
    bool createAppointmentFlag = false,
    bool appointmentsBelongToRoute = false)
  {
    if (this.graphServiceOrderEntry != null)
    {
      ((PXGraph) this.graphServiceOrderEntry).Clear((PXClearOption) 3);
    }
    else
    {
      this.graphServiceOrderEntry = PXGraph.CreateInstance<ServiceOrderEntry>();
      this.graphServiceOrderEntry.DisableServiceOrderUnboundFieldCalc = true;
    }
    FSSchedule fsSchedule = PXResultset<FSSchedule>.op_Implicit(((PXSelectBase<FSSchedule>) this.ScheduleSelected).Select(new object[1]
    {
      (object) timeSlotServiceOrder.ScheduleID
    }));
    FSServiceContract fsServiceContract = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Select(new object[1]
    {
      (object) fsSchedule.EntityID
    }));
    bool setBillServiceContract = fsServiceContract.BillingType == "STDB" || fsServiceContract.IsFixedRateContract.GetValueOrDefault();
    PXResultset<FSScheduleDet> pxResultset1 = ((PXSelectBase<FSScheduleDet>) this.ScheduleLinesSelected).Select(new object[1]
    {
      (object) timeSlotServiceOrder.ScheduleID
    });
    PXResultset<FSScheduleDet> pxResultset2 = ((PXSelectBase<FSScheduleDet>) this.ScheduleTemplatesSelected).Select(new object[1]
    {
      (object) timeSlotServiceOrder.ScheduleID
    });
    FSServiceOrder copy1 = (FSServiceOrder) ((PXSelectBase) this.graphServiceOrderEntry.ServiceOrderRecords).Cache.CreateCopy((object) (((PXSelectBase<FSServiceOrder>) this.graphServiceOrderEntry.ServiceOrderRecords).Current = ((PXSelectBase<FSServiceOrder>) this.graphServiceOrderEntry.ServiceOrderRecords).Insert(new FSServiceOrder()
    {
      SrvOrdType = fsSchedule.SrvOrdType
    })));
    copy1.BranchID = fsSchedule.BranchID;
    copy1.BranchLocationID = fsSchedule.BranchLocationID;
    copy1.OrderDate = new DateTime?(timeSlotServiceOrder.DateTimeBegin.Date);
    copy1.CustomerID = fsServiceContract.CustomerID;
    copy1.LocationID = fsSchedule.CustomerLocationID;
    FSServiceOrder copy2 = (FSServiceOrder) ((PXSelectBase) this.graphServiceOrderEntry.ServiceOrderRecords).Cache.CreateCopy((object) ((PXSelectBase<FSServiceOrder>) this.graphServiceOrderEntry.ServiceOrderRecords).Update(copy1));
    if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
    {
      string str = (string) null;
      PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) fsServiceContract.CustomerID
      }));
      if (customer != null)
        str = customer.CuryID;
      if (string.IsNullOrEmpty(str))
        str = ((PXGraph) this).Accessinfo.BaseCuryID ?? ((PXSelectBase<PX.Objects.GL.Company>) new PXSetup<PX.Objects.GL.Company>((PXGraph) this)).Current?.BaseCuryID;
      copy2.CuryID = str;
    }
    copy2.ContactID = fsServiceContract.CustomerContactID;
    copy2.DocDesc = timeSlotServiceOrder.Descr;
    copy2.BillCustomerID = fsServiceContract.BillCustomerID;
    copy2.BillLocationID = fsServiceContract.BillLocationID;
    copy2.ProjectID = fsServiceContract.ProjectID;
    copy2.DfltProjectTaskID = fsServiceContract.DfltProjectTaskID;
    copy2.ServiceContractID = fsServiceContract.ServiceContractID;
    copy2.ScheduleID = new int?(timeSlotServiceOrder.ScheduleID);
    copy2.GenerationID = timeSlotServiceOrder.GenerationID;
    FSServiceOrder copy3 = (FSServiceOrder) ((PXSelectBase) this.graphServiceOrderEntry.ServiceOrderRecords).Cache.CreateCopy((object) ((PXSelectBase<FSServiceOrder>) this.graphServiceOrderEntry.ServiceOrderRecords).Update(copy2));
    if (setBillServiceContract)
      copy3.BillServiceContractID = fsServiceContract.ServiceContractID;
    copy3.SalesPersonID = fsServiceContract.SalesPersonID;
    copy3.Commissionable = fsServiceContract.Commissionable;
    int? nullable = copy3.SalesPersonID;
    if (!nullable.HasValue)
    {
      object obj1;
      ((PXSelectBase) this.graphServiceOrderEntry.ServiceOrderRecords).Cache.RaiseFieldDefaulting<FSServiceOrder.salesPersonID>((object) copy3, ref obj1);
      copy3.SalesPersonID = (int?) obj1;
      object obj2;
      ((PXSelectBase) this.graphServiceOrderEntry.ServiceOrderRecords).Cache.RaiseFieldDefaulting<FSServiceOrder.commissionable>((object) copy3, ref obj2);
      copy3.Commissionable = (bool?) obj2;
    }
    this.graphServiceOrderEntry.Answers.CopyAllAttributes((object) ((PXSelectBase<FSServiceOrder>) this.graphServiceOrderEntry.ServiceOrderRecords).Update(copy3), (object) fsSchedule);
    foreach (PXResult<FSScheduleDet> pxResult1 in pxResultset1)
    {
      FSScheduleDet fsScheduleDetRow = PXResult<FSScheduleDet>.op_Implicit(pxResult1);
      if (fsScheduleDetRow.LineType == "TEMPL")
      {
        PXResultset<FSScheduleDet> source = pxResultset2;
        Expression<Func<PXResult<FSScheduleDet>, bool>> predicate = (Expression<Func<PXResult<FSScheduleDet>, bool>>) (x => ((FSScheduleDet) x).ServiceTemplateID == fsScheduleDetRow.ServiceTemplateID && ((FSScheduleDet) x).ScheduleDetID == fsScheduleDetRow.ScheduleDetID);
        foreach (PXResult<FSScheduleDet> pxResult2 in (IEnumerable<PXResult<FSScheduleDet>>) ((IQueryable<PXResult<FSScheduleDet>>) source).Where<PXResult<FSScheduleDet>>(predicate))
        {
          FSScheduleDet fsScheduleDet = PXResult<FSScheduleDet>.op_Implicit(pxResult2);
          foreach (PXResult<FSServiceTemplateDet> pxResult3 in ((PXSelectBase<FSServiceTemplateDet>) this.ServiceTemplateSelected).Select(new object[1]
          {
            (object) fsScheduleDetRow.ServiceTemplateID
          }))
          {
            FSServiceTemplateDet serviceTemplateDet = PXResult<FSServiceTemplateDet>.op_Implicit(pxResult3);
            FSSODet fssoDet1 = ((PXSelectBase<FSSODet>) this.graphServiceOrderEntry.ServiceOrderDetails).Insert(new FSSODet()
            {
              ScheduleID = fsScheduleDet.ScheduleID,
              ScheduleDetID = fsScheduleDet.ScheduleDetID,
              LineType = serviceTemplateDet.LineType
            });
            fssoDet1.InventoryID = serviceTemplateDet.InventoryID;
            FSSODet fssoDet2 = ((PXSelectBase<FSSODet>) this.graphServiceOrderEntry.ServiceOrderDetails).Update(fssoDet1);
            if (fssoDet2.UOM != serviceTemplateDet.UOM)
            {
              fssoDet2.UOM = serviceTemplateDet.UOM;
              ((PXSelectBase) this.graphServiceOrderEntry.ServiceOrderDetails).Cache.SetDefaultExt<FSSODet.curyUnitPrice>((object) fssoDet2);
            }
            PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.ScheduleLinesSelected).Cache, (object) fsScheduleDet, ((PXSelectBase) this.graphServiceOrderEntry.ServiceOrderDetails).Cache, (object) fssoDet2, new bool?(true), new bool?(true));
            FSSODet copy4 = (FSSODet) ((PXSelectBase) this.graphServiceOrderEntry.ServiceOrderDetails).Cache.CreateCopy((object) fssoDet2);
            copy4.TranDesc = serviceTemplateDet.TranDesc;
            PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow((PXGraph) this, serviceTemplateDet.InventoryID);
            if (inventoryItemRow != null)
            {
              FSxService extension = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxService>(inventoryItemRow);
              if (extension != null && extension.BillingRule == "TIME")
              {
                int? estimatedDuration = this.CalculateEstimatedDuration(copy4, serviceTemplateDet.Qty);
                copy4.EstimatedDuration = estimatedDuration;
              }
              else
                copy4.EstimatedQty = serviceTemplateDet.Qty;
            }
            if (fsServiceContract.SourcePrice == "C")
              copy4.ManualPrice = new bool?(true);
            copy4.Status = !createAppointmentFlag ? this.graphServiceOrderEntry.CalculateLineStatus(copy4) : "SC";
            copy4.EquipmentAction = fsScheduleDetRow.EquipmentAction;
            copy4.SMEquipmentID = fsScheduleDetRow.SMEquipmentID;
            copy4.ComponentID = fsScheduleDetRow.ComponentID;
            copy4.EquipmentLineRef = fsScheduleDetRow.EquipmentLineRef;
            copy4.ProjectTaskID = fsScheduleDetRow.ProjectTaskID;
            copy4.CostCodeID = fsScheduleDetRow.CostCodeID;
            nullable = copy4.InventoryID;
            if (nullable.HasValue)
            {
              copy4.ProjectTaskID = fsScheduleDetRow.ProjectTaskID;
              copy4.CostCodeID = fsScheduleDetRow.CostCodeID;
            }
            ((PXSelectBase<FSSODet>) this.graphServiceOrderEntry.ServiceOrderDetails).Update(copy4);
          }
        }
      }
      else
      {
        FSSODet fssoDet3 = ((PXSelectBase<FSSODet>) this.graphServiceOrderEntry.ServiceOrderDetails).Insert(new FSSODet()
        {
          ScheduleID = fsScheduleDetRow.ScheduleID,
          ScheduleDetID = fsScheduleDetRow.ScheduleDetID,
          LineType = fsScheduleDetRow.LineType
        });
        fssoDet3.InventoryID = fsScheduleDetRow.InventoryID;
        FSSODet fssoDet4 = ((PXSelectBase<FSSODet>) this.graphServiceOrderEntry.ServiceOrderDetails).Update(fssoDet3);
        if (fssoDet4.UOM != fsScheduleDetRow.UOM)
        {
          fssoDet4.UOM = fsScheduleDetRow.UOM;
          ((PXSelectBase) this.graphServiceOrderEntry.ServiceOrderDetails).Cache.SetDefaultExt<FSSODet.curyUnitPrice>((object) fssoDet4);
        }
        PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.ScheduleLinesSelected).Cache, (object) fsScheduleDetRow, ((PXSelectBase) this.graphServiceOrderEntry.ServiceOrderDetails).Cache, (object) fssoDet4, new bool?(true), new bool?(true));
        FSSODet copy5 = (FSSODet) ((PXSelectBase) this.graphServiceOrderEntry.ServiceOrderDetails).Cache.CreateCopy((object) fssoDet4);
        copy5.TranDesc = fsScheduleDetRow.TranDesc;
        copy5.BillingRule = fsScheduleDetRow.BillingRule;
        copy5.EstimatedDuration = fsScheduleDetRow.EstimatedDuration;
        copy5.EstimatedQty = fsScheduleDetRow.LineType == "IT_LN" || fsScheduleDetRow.LineType == "CM_LN" ? new Decimal?(0M) : fsScheduleDetRow.Qty;
        if (fsServiceContract.SourcePrice == "C")
          copy5.ManualPrice = new bool?(true);
        copy5.Status = !createAppointmentFlag ? this.graphServiceOrderEntry.CalculateLineStatus(copy5) : "SC";
        copy5.EquipmentAction = fsScheduleDetRow.EquipmentAction;
        copy5.SMEquipmentID = fsScheduleDetRow.SMEquipmentID;
        copy5.ComponentID = fsScheduleDetRow.ComponentID;
        copy5.EquipmentLineRef = fsScheduleDetRow.EquipmentLineRef;
        copy5.ProjectTaskID = fsScheduleDetRow.ProjectTaskID;
        copy5.CostCodeID = fsScheduleDetRow.CostCodeID;
        nullable = copy5.InventoryID;
        if (nullable.HasValue)
        {
          copy5.ProjectTaskID = fsScheduleDetRow.ProjectTaskID;
          copy5.CostCodeID = fsScheduleDetRow.CostCodeID;
        }
        ((PXSelectBase<FSSODet>) this.graphServiceOrderEntry.ServiceOrderDetails).Update(copy5);
      }
    }
    if (fsSchedule.VendorID.HasValue)
      ((PXSelectBase<FSSOEmployee>) this.graphServiceOrderEntry.ServiceOrderEmployees).Insert(new FSSOEmployee()
      {
        EmployeeID = fsSchedule.VendorID
      });
    ((PXAction) this.graphServiceOrderEntry.Save).Press();
    FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) this.graphServiceOrderEntry.ServiceOrderRecords).Current;
    if (!createAppointmentFlag)
      return;
    bool? openDoc = current.OpenDoc;
    if (!openDoc.GetValueOrDefault())
      PXUpdate<Set<FSServiceOrder.openDoc, True>, FSServiceOrder, Where<FSServiceOrder.sOID, Equal<Required<FSServiceOrder.sOID>>>>.Update((PXGraph) this, new object[1]
      {
        (object) current.SOID
      });
    this.CreateAppointment(current, timeSlotServiceOrder, fsSchedule, true, appointmentsBelongToRoute, setBillServiceContract);
    if (openDoc.GetValueOrDefault())
      return;
    PXUpdate<Set<FSServiceOrder.openDoc, Required<FSServiceOrder.openDoc>>, FSServiceOrder, Where<FSServiceOrder.sOID, Equal<Required<FSServiceOrder.sOID>>>>.Update((PXGraph) this, new object[2]
    {
      (object) openDoc,
      (object) current.SOID
    });
  }

  protected int? CalculateEstimatedDuration(FSSODet fsSODetRow, Decimal? qty)
  {
    Decimal num = 0M;
    if (fsSODetRow == null)
      return new int?((int) num);
    Decimal? estimatedQty = fsSODetRow.EstimatedQty;
    if (estimatedQty.HasValue)
    {
      int? estimatedDuration = fsSODetRow.EstimatedDuration;
      if (estimatedDuration.HasValue)
      {
        estimatedDuration = fsSODetRow.EstimatedDuration;
        Decimal d1 = (Decimal) estimatedDuration.Value * qty.Value;
        estimatedQty = fsSODetRow.EstimatedQty;
        Decimal d2 = estimatedQty.Value;
        num = Decimal.Divide(d1, d2);
      }
    }
    return new int?((int) num);
  }

  /// <summary>Create a ContractGenerationHistory.</summary>
  protected void CreateContractGenerationHistory(
    int nextGenerationID,
    int scheduleID,
    DateTime lastProcessDate,
    DateTime? lastGeneratedElementDate,
    string recordType)
  {
    ContractGenerationHistoryMaint instance = PXGraph.CreateInstance<ContractGenerationHistoryMaint>();
    FSContractGenerationHistory generationHistory = new FSContractGenerationHistory();
    generationHistory.GenerationID = new int?(nextGenerationID);
    generationHistory.ScheduleID = new int?(scheduleID);
    generationHistory.LastProcessedDate = new DateTime?(lastProcessDate);
    generationHistory.LastGeneratedElementDate = lastGeneratedElementDate;
    generationHistory.EntityType = "C";
    generationHistory.RecordType = recordType;
    FSContractGenerationHistory historyRowBySchedule = this.GetLastGenerationHistoryRowBySchedule(scheduleID);
    if (historyRowBySchedule != null && historyRowBySchedule.ContractGenerationHistoryID.HasValue)
    {
      generationHistory.PreviousGeneratedElementDate = historyRowBySchedule.LastGeneratedElementDate;
      generationHistory.PreviousProcessedDate = historyRowBySchedule.LastProcessedDate;
      if (!lastGeneratedElementDate.HasValue)
        generationHistory.LastGeneratedElementDate = generationHistory.PreviousGeneratedElementDate;
    }
    ((PXSelectBase<FSContractGenerationHistory>) instance.ContractGenerationHistoryRecords).Insert(generationHistory);
    ((PXAction) instance.Save).Press();
  }

  /// <summary>
  /// Update an Schedule (lastGeneratedAppointmentDate and lastProcessedDate).
  /// </summary>
  protected void UpdateGeneratedSchedule(
    int scheduleID,
    DateTime? toDate,
    DateTime? lastGeneratedElementDate,
    FSSchedule fsScheduleRow)
  {
    if (PXResultset<FSSchedule>.op_Implicit(PXSelectBase<FSSchedule, PXSelect<FSSchedule, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) scheduleID
    })) == null || !lastGeneratedElementDate.HasValue && toDate.HasValue)
      return;
    if (fsScheduleRow != null)
    {
      fsScheduleRow.LastGeneratedElementDate = lastGeneratedElementDate;
      fsScheduleRow.NextExecutionDate = SharedFunctions.GetNextExecution(((PXSelectBase) this.ScheduleSelected).Cache, fsScheduleRow);
    }
    PXUpdate<Set<FSSchedule.lastGeneratedElementDate, Required<FSSchedule.lastGeneratedElementDate>, Set<FSSchedule.nextExecutionDate, Required<FSSchedule.nextExecutionDate>>>, FSSchedule, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>>.Update((PXGraph) this, new object[3]
    {
      (object) lastGeneratedElementDate,
      (object) fsScheduleRow.NextExecutionDate,
      (object) scheduleID
    });
  }

  /// <summary>
  /// Return the last FSContractGenerationHistory by Schedule.
  /// </summary>
  public FSContractGenerationHistory GetLastGenerationHistoryRowBySchedule(int scheduleID)
  {
    FSContractGenerationHistory generationHistory = PXResultset<FSContractGenerationHistory>.op_Implicit(PXSelectBase<FSContractGenerationHistory, PXSelectGroupBy<FSContractGenerationHistory, Where<FSContractGenerationHistory.scheduleID, Equal<Required<FSContractGenerationHistory.scheduleID>>>, Aggregate<Max<FSContractGenerationHistory.generationID>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) scheduleID
    }));
    return generationHistory != null && generationHistory.GenerationID.HasValue ? generationHistory : (FSContractGenerationHistory) null;
  }

  /// <summary>
  /// Return the smallest date between schedule EndDate and Process EndDate.
  /// </summary>
  protected DateTime? GetProcessEndDate(PX.Objects.FS.Scheduler.Schedule fsScheduleRule, DateTime? toDate)
  {
    FSSchedule fsSchedule = PXResultset<FSSchedule>.op_Implicit(PXSelectBase<FSSchedule, PXSelect<FSSchedule, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fsScheduleRule.ScheduleID
    }));
    bool? enableExpirationDate = fsSchedule.EnableExpirationDate;
    bool flag = false;
    if (enableExpirationDate.GetValueOrDefault() == flag & enableExpirationDate.HasValue)
      return new DateTime?(toDate.Value);
    DateTime? endDate = fsSchedule.EndDate;
    if (endDate.HasValue)
    {
      endDate = fsSchedule.EndDate;
      DateTime? nullable = toDate;
      if ((endDate.HasValue & nullable.HasValue ? (endDate.GetValueOrDefault() < nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        return fsSchedule.EndDate;
    }
    return new DateTime?(toDate.Value);
  }

  public virtual void clearAll()
  {
  }

  public virtual void openServiceContractScreenBySchedules()
  {
  }

  public virtual void openServiceContractScreenByGenerationLogError()
  {
  }

  public virtual void openScheduleScreenBySchedules()
  {
  }

  public virtual void openScheduleScreenByGenerationLogError()
  {
  }

  public class ContractHistoryRecords_View : 
    PXSelectJoinGroupBy<FSContractGenerationHistory, InnerJoin<FSProcessIdentity, On<FSProcessIdentity.processID, Equal<FSContractGenerationHistory.generationID>>>, Where<FSContractGenerationHistory.recordType, Equal<TRecordType>>, Aggregate<GroupBy<FSContractGenerationHistory.generationID>>, OrderBy<Desc<FSContractGenerationHistory.generationID>>>
  {
    public ContractHistoryRecords_View(PXGraph graph)
      : base(graph)
    {
    }

    public ContractHistoryRecords_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class ErrorMessageRecords_View : 
    PXSelectReadonly2<FSGenerationLogError, InnerJoin<FSSchedule, On<FSSchedule.scheduleID, Equal<FSGenerationLogError.scheduleID>>, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSSchedule.entityID>, And<FSServiceContract.recordType, Equal<FSGenerationLogError.processType>>>>>, Where<FSGenerationLogError.ignore, Equal<False>, And<FSGenerationLogError.processType, Equal<TRecordType>>>, OrderBy<Desc<FSGenerationLogError.generationID>>>
  {
    public ErrorMessageRecords_View(PXGraph graph)
      : base(graph)
    {
    }

    public ErrorMessageRecords_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }
}
