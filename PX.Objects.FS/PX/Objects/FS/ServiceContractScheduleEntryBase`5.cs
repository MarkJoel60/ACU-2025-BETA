// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceContractScheduleEntryBase`5
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.FS.Scheduler;
using PX.Objects.FS.SiteStatusLookup;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class ServiceContractScheduleEntryBase<TGraph, TPrimary, TScheduleID, TEntityID, TCustomerID> : 
  PXGraph<TGraph, TPrimary>,
  ISiteStatusLookupCompatible
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
  where TScheduleID : IBqlField
  where TEntityID : IBqlField
  where TCustomerID : IBqlField
{
  public bool statusChanged;
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> BAccount;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Contact> Contact;
  [PXHidden]
  public PXSelect<FSSchedule> Schedule;
  [PXHidden]
  public PXSetup<PX.Objects.GL.Company> companySetup;
  public PXFilter<PX.Objects.FS.FromToFilter> FromToFilter;
  [PXHidden]
  public PXSetup<FSSetup> Setup;
  [PXHidden]
  public PXSelect<FSSalesPrice> salesPriceRecords;
  public PXSelectJoin<TPrimary, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<TCustomerID>>>, Where2<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>, And<TEntityID, Equal<Optional<TEntityID>>>>> ContractScheduleRecords;
  public PXSelect<TPrimary, Where<TScheduleID, Equal<Current<TScheduleID>>>> ContractScheduleSelected;
  public PXSetup<FSSrvOrdType>.Where<Where<FSSrvOrdType.srvOrdType, Equal<Current<FSSchedule.srvOrdType>>>> SrvOrdTypeSelected;
  public PXSetup<FSServiceContract>.Where<Where<FSServiceContract.serviceContractID, Equal<Current<TEntityID>>>> CurrentServiceContract;
  [PXFilterable(new System.Type[] {})]
  [PXImport(typeof (FSSchedule))]
  public ServiceContractScheduleEntryBase<TGraph, TPrimary, TScheduleID, TEntityID, TCustomerID>.ScheduleDetOrdered ScheduleDetails;
  [PXCopyPasteHiddenView]
  public PXSelect<FSSalesPrice, Where<FSSalesPrice.serviceContractID, Equal<Required<FSSalesPrice.serviceContractID>>, And<FSSalesPrice.inventoryID, Equal<Required<FSSalesPrice.inventoryID>>>>> SalesPriceLines;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<FSScheduleDet, InnerJoin<FSSchedule, On<FSSchedule.scheduleID, Equal<FSScheduleDet.scheduleID>>>, Where<FSSchedule.entityID, Equal<Required<FSSchedule.entityID>>, And<FSScheduleDet.inventoryID, Equal<Required<FSScheduleDet.inventoryID>>, And<FSScheduleDet.scheduleDetID, NotEqual<Required<FSScheduleDet.scheduleDetID>>>>>> ScheduleDetByContract;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<FSServiceTemplateDet, InnerJoin<FSScheduleDet, On<FSScheduleDet.serviceTemplateID, Equal<FSServiceTemplateDet.serviceTemplateID>>, InnerJoin<FSSchedule, On<FSSchedule.scheduleID, Equal<FSScheduleDet.scheduleID>>>>, Where<FSSchedule.entityID, Equal<Required<FSSchedule.entityID>>, And<FSServiceTemplateDet.inventoryID, Equal<Required<FSServiceTemplateDet.inventoryID>>, And<FSScheduleDet.scheduleDetID, NotEqual<Required<FSScheduleDet.scheduleDetID>>>>>> ScheduleDetServicesInServiceTemplatesByContract;
  [PXCopyPasteHiddenView]
  [PXVirtualDAC]
  public PXSelectReadonly<ScheduleProjection> ScheduleProjectionRecords;
  [PXCopyPasteHiddenView]
  public PXSetup<FSServiceContract>.Where<Where<FSServiceContract.serviceContractID, Equal<Current<TEntityID>>>> ContractSelected;
  [PXCopyPasteHiddenView]
  public PXSelect<FSContractAction, Where<FSContractAction.serviceContractID, Equal<Current<FSServiceContract.serviceContractID>>>> ContractHistory;
  [PXViewName("Answers")]
  public FSAttributeList<FSSchedule> Answers;
  [PXCopyPasteHiddenView]
  public PXSelect<FSAppointmentDet, Where<FSAppointmentDet.scheduleID, Equal<Current<FSSchedule.scheduleID>>, And<FSAppointmentDet.scheduleDetID, Equal<Required<FSAppointmentDet.scheduleDetID>>>>> CurrentScheduleAppointmentDet;
  private PXGraph tempGraph;

  public bool IsCopyContract { get; set; }

  private PXGraph TempGraph
  {
    get
    {
      if (this.tempGraph == null)
        this.tempGraph = new PXGraph();
      return this.tempGraph;
    }
  }

  public void FSSchedule_Row_Deleted_PartialHandler(PXCache cache, PXRowDeletedEventArgs e)
  {
    if (e.Row == null)
      return;
    FSSchedule row = (FSSchedule) e.Row;
    PXUpdate<Set<FSServiceOrder.scheduleID, Required<FSServiceOrder.scheduleID>>, FSServiceOrder, Where<FSServiceOrder.scheduleID, Equal<Required<FSServiceOrder.scheduleID>>>>.Update((PXGraph) this, new object[2]
    {
      null,
      (object) row.ScheduleID
    });
    PXUpdate<Set<FSAppointment.scheduleID, Required<FSAppointment.scheduleID>>, FSAppointment, Where<FSAppointment.scheduleID, Equal<Required<FSAppointment.scheduleID>>>>.Update((PXGraph) this, new object[2]
    {
      null,
      (object) row.ScheduleID
    });
  }

  public virtual IEnumerable<ScheduleProjection> Delegate_ScheduleProjectionRecords(
    PXCache cache,
    FSSchedule fsScheduleRow,
    PX.Objects.FS.FromToFilter filter,
    string recordType)
  {
    ServiceContractScheduleEntryBase<TGraph, TPrimary, TScheduleID, TEntityID, TCustomerID> graph = this;
    DateTime? dateBegin = filter.DateBegin;
    DateTime? toDate = new DateTime?();
    DateTime? nullable1 = filter.DateEnd;
    if (nullable1.HasValue)
      toDate = filter.DateEnd;
    if (dateBegin.HasValue && !toDate.HasValue)
    {
      nullable1 = filter.DateEnd;
      toDate = nullable1.HasValue ? filter.DateEnd : new DateTime?(dateBegin.Value.AddYears(1));
    }
    if (dateBegin.HasValue && toDate.HasValue)
    {
      Period period = new Period(dateBegin.Value, new DateTime?(toDate.Value));
      List<PX.Objects.FS.Scheduler.Schedule> scheduleList = new List<PX.Objects.FS.Scheduler.Schedule>();
      TimeSlotGenerator timeSlotGenerator = new TimeSlotGenerator();
      List<PX.Objects.FS.Scheduler.Schedule> schedule1 = MapFSScheduleToSchedule.convertFSScheduleToSchedule(cache, fsScheduleRow, toDate, recordType);
      if (recordType == "IRSC")
      {
        foreach (PX.Objects.FS.Scheduler.Schedule schedule2 in schedule1)
        {
          schedule2.Priority = new int?((int) RouteScheduleProcess.SetSchedulePriority(schedule2, (PXGraph) graph));
          schedule2.RouteInfoList = RouteScheduleProcess.getRouteListFromSchedule(schedule2, (PXGraph) graph);
        }
      }
      foreach (TimeSlot timeSlot in timeSlotGenerator.GenerateCalendar(period, (IEnumerable<PX.Objects.FS.Scheduler.Schedule>) schedule1))
      {
        ScheduleProjection scheduleProjection1 = new ScheduleProjection();
        scheduleProjection1.Date = new DateTime?(timeSlot.DateTimeBegin);
        ScheduleProjection scheduleProjection2 = scheduleProjection1;
        nullable1 = scheduleProjection1.Date;
        DateTime? nullable2 = new DateTime?(SharedFunctions.StartOfWeek(nullable1.Value, DayOfWeek.Monday));
        scheduleProjection2.BeginDateOfWeek = nullable2;
        ((PXSelectBase) graph.ScheduleProjectionRecords).Cache.SetStatus((object) scheduleProjection1, (PXEntryStatus) 5);
        yield return scheduleProjection1;
      }
    }
    ((PXSelectBase) graph.ScheduleProjectionRecords).Cache.IsDirty = false;
  }

  public string GetLineType(PX.Objects.IN.InventoryItem inventoryItemRow, string lineTypeFromSchedule)
  {
    if (!(lineTypeFromSchedule == "TEMPL"))
      return lineTypeFromSchedule;
    bool? stkItem = inventoryItemRow.StkItem;
    bool flag = false;
    if (!(stkItem.GetValueOrDefault() == flag & stkItem.HasValue))
      return "SLPRO";
    return inventoryItemRow.ItemType == "S" ? "SERVI" : "NSTKI";
  }

  /// <exclude />
  public void SyncFSSalesPrice(
    PXCache cache,
    int? serviceContractID,
    PXDBOperation operation,
    FSScheduleDet modifiedRow)
  {
    this.TempGraph.Clear((PXClearOption) 4);
    PX.Objects.GL.Company company = PXResultset<PX.Objects.GL.Company>.op_Implicit(PXSelectBase<PX.Objects.GL.Company, PXSelect<PX.Objects.GL.Company>.Config>.Select(cache.Graph, Array.Empty<object>()));
    if (operation == 2 || operation == 1)
    {
      FSServiceContract fsServiceContract = FSServiceContract.PK.Find(this.TempGraph, serviceContractID);
      if (fsServiceContract != null)
      {
        PXResultset<FSSchedule> pxResultset = PXSelectBase<FSSchedule, PXSelectJoin<FSSchedule, LeftJoin<FSScheduleDet, On<FSScheduleDet.scheduleID, Equal<FSSchedule.scheduleID>>, LeftJoin<FSServiceTemplateDet, On<FSServiceTemplateDet.serviceTemplateID, Equal<FSScheduleDet.serviceTemplateID>>, LeftJoin<FSSalesPrice, On<FSSalesPrice.serviceContractID, Equal<FSSchedule.entityID>, And<Where2<Where<FSSalesPrice.inventoryID, Equal<FSScheduleDet.inventoryID>, And<FSScheduleDet.inventoryID, IsNotNull, And<FSSalesPrice.uOM, Equal<FSScheduleDet.uOM>>>>, Or<Where<FSSalesPrice.inventoryID, Equal<FSServiceTemplateDet.inventoryID>, And<FSScheduleDet.serviceTemplateID, IsNotNull, And<FSSalesPrice.uOM, Equal<FSServiceTemplateDet.uOM>>>>>>>>>>>, Where<FSSalesPrice.salesPriceID, IsNull, And2<Where<FSScheduleDet.inventoryID, IsNotNull, Or<FSScheduleDet.serviceTemplateID, IsNotNull>>, And<FSSchedule.entityID, Equal<Required<FSSchedule.entityID>>>>>>.Config>.Select(this.TempGraph, new object[1]
        {
          (object) fsServiceContract.ServiceContractID
        });
        bool flag = false;
        foreach (PXResult<FSSchedule, FSScheduleDet, FSServiceTemplateDet, FSSalesPrice> pxResult in pxResultset)
        {
          PXResult<FSSchedule, FSScheduleDet, FSServiceTemplateDet, FSSalesPrice>.op_Implicit(pxResult);
          FSScheduleDet fsScheduleDet = PXResult<FSSchedule, FSScheduleDet, FSServiceTemplateDet, FSSalesPrice>.op_Implicit(pxResult);
          FSServiceTemplateDet serviceTemplateDet = PXResult<FSSchedule, FSScheduleDet, FSServiceTemplateDet, FSSalesPrice>.op_Implicit(pxResult);
          if (!(fsScheduleDet.LineType == "TEMPL") || modifiedRow == null || cache.ObjectsEqual((object) fsScheduleDet, (object) modifiedRow))
          {
            PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow(this.TempGraph, fsScheduleDet.LineType == "TEMPL" ? serviceTemplateDet.InventoryID : fsScheduleDet.InventoryID);
            if (inventoryItemRow != null)
            {
              FSSalesPrice fsSalesPrice = new FSSalesPrice();
              fsSalesPrice.ServiceContractID = fsServiceContract.ServiceContractID;
              fsSalesPrice.InventoryID = inventoryItemRow.InventoryID;
              fsSalesPrice.LineType = this.GetLineType(inventoryItemRow, fsScheduleDet.LineType);
              fsSalesPrice.CuryID = company?.BaseCuryID;
              fsSalesPrice.UOM = fsScheduleDet?.UOM ?? serviceTemplateDet?.UOM;
              if (fsServiceContract.SourcePrice == "C")
              {
                SalesPriceSet customerContract = FSPriceManagement.CalculateSalesPriceWithCustomerContract(cache, new int?(), new int?(), new int?(), fsServiceContract.CustomerID, fsServiceContract.CustomerLocationID, new bool?(), fsSalesPrice.InventoryID, new int?(), new Decimal?(0M), fsSalesPrice.UOM, (fsServiceContract.StartDate ?? cache.Graph.Accessinfo.BusinessDate).Value, fsSalesPrice.UnitPrice, true, (PX.Objects.CM.CurrencyInfo) null, true);
                fsSalesPrice.UnitPrice = customerContract.Price;
              }
              flag = true;
              ((PXSelectBase) this.salesPriceRecords).Cache.Insert((object) fsSalesPrice);
            }
          }
        }
        if (flag)
          ((PXSelectBase) this.salesPriceRecords).Cache.Persist((PXDBOperation) 2);
      }
    }
    if (operation != 3 && operation != 1)
      return;
    PXResultset<FSServiceContract> pxResultset1 = PXSelectBase<FSServiceContract, PXSelectJoin<FSServiceContract, LeftJoin<FSSchedule, On<FSSchedule.entityID, Equal<FSServiceContract.serviceContractID>>, LeftJoin<FSScheduleDet, On<FSScheduleDet.scheduleID, Equal<FSSchedule.scheduleID>>, LeftJoin<FSServiceTemplateDet, On<FSServiceTemplateDet.serviceTemplateID, Equal<FSScheduleDet.serviceTemplateID>>>>>, Where<FSServiceContract.serviceContractID, Equal<Required<FSServiceContract.serviceContractID>>>>.Config>.Select(this.TempGraph, new object[1]
    {
      (object) serviceContractID
    });
    List<FSSalesPrice> list = GraphHelper.RowCast<FSSalesPrice>((IEnumerable) PXSelectBase<FSSalesPrice, PXSelect<FSSalesPrice, Where<FSSalesPrice.serviceContractID, Equal<Required<FSSalesPrice.serviceContractID>>>>.Config>.Select(this.TempGraph, new object[1]
    {
      (object) serviceContractID
    })).ToList<FSSalesPrice>();
    bool flag1 = false;
    foreach (PXResult<FSServiceContract, FSSchedule, FSScheduleDet, FSServiceTemplateDet> pxResult in pxResultset1)
    {
      PXResult<FSServiceContract, FSSchedule, FSScheduleDet, FSServiceTemplateDet>.op_Implicit(pxResult);
      FSScheduleDet fsSCheduleDetRow = PXResult<FSServiceContract, FSSchedule, FSScheduleDet, FSServiceTemplateDet>.op_Implicit(pxResult);
      FSServiceTemplateDet serviceTemplateDet = PXResult<FSServiceContract, FSSchedule, FSScheduleDet, FSServiceTemplateDet>.op_Implicit(pxResult);
      int? inventoryID = fsSCheduleDetRow.LineType == "TEMPL" ? serviceTemplateDet.InventoryID : fsSCheduleDetRow.InventoryID;
      string uom = fsSCheduleDetRow.LineType == "TEMPL" ? serviceTemplateDet.UOM : fsSCheduleDetRow.UOM;
      if (inventoryID.HasValue)
      {
        FSSalesPrice fsSalesPrice = GraphHelper.RowCast<FSSalesPrice>((IEnumerable) list).Where<FSSalesPrice>((Func<FSSalesPrice, bool>) (sp =>
        {
          if (fsSCheduleDetRow.LineType == "TEMPL" && modifiedRow != null)
            return !cache.ObjectsEqual((object) modifiedRow, (object) fsSCheduleDetRow);
          int? inventoryId = sp.InventoryID;
          int? nullable = inventoryID;
          return inventoryId.GetValueOrDefault() == nullable.GetValueOrDefault() & inventoryId.HasValue == nullable.HasValue && sp.UOM == uom;
        })).FirstOrDefault<FSSalesPrice>();
        list.Remove(fsSalesPrice);
      }
    }
    foreach (FSSalesPrice fsSalesPrice in list)
    {
      flag1 = true;
      ((PXSelectBase) this.salesPriceRecords).Cache.Delete((object) fsSalesPrice);
    }
    if (!flag1)
      return;
    ((PXSelectBase) this.salesPriceRecords).Cache.Persist((PXDBOperation) 3);
  }

  /// <summary>
  /// Shows/Hides Season settings depending on the setup's flag EnableSeasonScheduleContract and on the frequencyType selected.
  /// </summary>
  public void ShowHideSeasonSetting(PXCache cache, FSSchedule fsScheduleRow)
  {
    bool flag = this.ShowSeasonSettings(fsScheduleRow);
    PXUIFieldAttribute.SetVisible<FSSchedule.seasonOnJan>(cache, (object) fsScheduleRow, flag);
    PXUIFieldAttribute.SetVisible<FSSchedule.seasonOnFeb>(cache, (object) fsScheduleRow, flag);
    PXUIFieldAttribute.SetVisible<FSSchedule.seasonOnMar>(cache, (object) fsScheduleRow, flag);
    PXUIFieldAttribute.SetVisible<FSSchedule.seasonOnApr>(cache, (object) fsScheduleRow, flag);
    PXUIFieldAttribute.SetVisible<FSSchedule.seasonOnMay>(cache, (object) fsScheduleRow, flag);
    PXUIFieldAttribute.SetVisible<FSSchedule.seasonOnJun>(cache, (object) fsScheduleRow, flag);
    PXUIFieldAttribute.SetVisible<FSSchedule.seasonOnJul>(cache, (object) fsScheduleRow, flag);
    PXUIFieldAttribute.SetVisible<FSSchedule.seasonOnAug>(cache, (object) fsScheduleRow, flag);
    PXUIFieldAttribute.SetVisible<FSSchedule.seasonOnSep>(cache, (object) fsScheduleRow, flag);
    PXUIFieldAttribute.SetVisible<FSSchedule.seasonOnOct>(cache, (object) fsScheduleRow, flag);
    PXUIFieldAttribute.SetVisible<FSSchedule.seasonOnNov>(cache, (object) fsScheduleRow, flag);
    PXUIFieldAttribute.SetVisible<FSSchedule.seasonOnDec>(cache, (object) fsScheduleRow, flag);
  }

  private bool ShowSeasonSettings(FSSchedule fsScheduleRow)
  {
    return SharedFunctions.GetEnableSeasonSetting((PXGraph) this, fsScheduleRow, ((PXSelectBase<FSSetup>) this.Setup).Current) && fsScheduleRow.FrequencyType != "A";
  }

  /// <summary>
  /// Manage common actions for FSRouteContractSchedule and FSContractSchedule in RowSelected event.
  /// </summary>
  public void ContractSchedule_RowSelected_PartialHandler(PXCache cache, FSSchedule fsScheduleRow)
  {
    ServiceContractScheduleEntryBase<TGraph, TPrimary, TScheduleID, TEntityID, TCustomerID>.SetControlsState(cache, fsScheduleRow);
    PXCache pxCache = cache;
    FSSchedule fsSchedule = fsScheduleRow;
    bool? noRunLimit = fsScheduleRow.NoRunLimit;
    bool flag = false;
    int num = noRunLimit.GetValueOrDefault() == flag & noRunLimit.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<FSSchedule.runLimit>(pxCache, (object) fsSchedule, num != 0);
    PXDefaultAttribute.SetPersistingCheck<FSSchedule.runLimit>(cache, (object) fsScheduleRow, fsScheduleRow.NoRunLimit.GetValueOrDefault() ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXUIFieldAttribute.SetEnabled<FSSchedule.restrictionMinTime>(cache, (object) fsScheduleRow, fsScheduleRow.RestrictionMin.GetValueOrDefault());
    PXDefaultAttribute.SetPersistingCheck<FSSchedule.restrictionMinTime>(cache, (object) fsScheduleRow, fsScheduleRow.RestrictionMin.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetEnabled<FSSchedule.restrictionMaxTime>(cache, (object) fsScheduleRow, fsScheduleRow.RestrictionMax.GetValueOrDefault());
    PXDefaultAttribute.SetPersistingCheck<FSSchedule.restrictionMaxTime>(cache, (object) fsScheduleRow, fsScheduleRow.RestrictionMax.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthly3Selected>(cache, (object) fsScheduleRow, fsScheduleRow.Monthly2Selected.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthly4Selected>(cache, (object) fsScheduleRow, fsScheduleRow.Monthly3Selected.GetValueOrDefault());
    fsScheduleRow.SrvOrdTypeMessage = "This Service Order Type will be used for the recurring appointments";
    this.ShowHideSeasonSetting(cache, fsScheduleRow);
  }

  /// <summary>
  /// Manage common actions for FSRouteContractSchedule and FSContractSchedule in RowPersisting event.
  /// </summary>
  public void ContractSchedule_RowPersisting_PartialHandler(
    PXCache cache,
    FSServiceContract fsServiceContractRow,
    FSSchedule fsScheduleRow,
    PXDBOperation operation,
    string moduleName)
  {
    if (fsScheduleRow.FrequencyType == "W" && !ServiceContractScheduleEntryBase<TGraph, TPrimary, TScheduleID, TEntityID, TCustomerID>.HasSelectedAtLeastOneDay(fsScheduleRow))
      cache.RaiseExceptionHandling<FSSchedule.weeklyFrequency>((object) fsScheduleRow, (object) fsScheduleRow.AnnualFrequency, (Exception) new PXSetPropertyException("You must select at least one day of the week."));
    else if (fsScheduleRow.FrequencyType == "A" && !ServiceContractScheduleEntryBase<TGraph, TPrimary, TScheduleID, TEntityID, TCustomerID>.HasSelectedAtLeastOneMonth(fsScheduleRow))
    {
      cache.RaiseExceptionHandling<FSSchedule.annualFrequency>((object) fsScheduleRow, (object) fsScheduleRow.AnnualFrequency, (Exception) new PXSetPropertyException("You must select at least one month."));
    }
    else
    {
      short? nullable1;
      int? nullable2;
      if (!fsScheduleRow.NoRunLimit.HasValue || !fsScheduleRow.NoRunLimit.Value)
      {
        nullable1 = fsScheduleRow.RunLimit;
        nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
        int num = 0;
        if (nullable2.GetValueOrDefault() < num & nullable2.HasValue)
        {
          cache.RaiseExceptionHandling<FSSchedule.runLimit>((object) fsScheduleRow, (object) fsScheduleRow.RunLimit, (Exception) new PXSetPropertyException("The execution limit has to be greater than 0."));
          return;
        }
      }
      if (fsScheduleRow.RestrictionMin.GetValueOrDefault() && !fsScheduleRow.RestrictionMinTime.HasValue)
        cache.RaiseExceptionHandling<FSSchedule.restrictionMinTime>((object) fsScheduleRow, (object) fsScheduleRow.RestrictionMinTime, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefix("'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<FSSchedule.restrictionMinTime>(cache)
        })));
      else if (fsScheduleRow.RestrictionMax.GetValueOrDefault() && !fsScheduleRow.RestrictionMaxTime.HasValue)
      {
        cache.RaiseExceptionHandling<FSSchedule.restrictionMaxTime>((object) fsScheduleRow, (object) fsScheduleRow.RestrictionMaxTime, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefix("'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<FSSchedule.restrictionMaxTime>(cache)
        })));
      }
      else
      {
        DateTime? startDate = fsScheduleRow.StartDate;
        DateTime? nullable3 = fsScheduleRow.EndDate;
        if ((startDate.HasValue & nullable3.HasValue ? (startDate.GetValueOrDefault() > nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          cache.RaiseExceptionHandling<FSSchedule.startDate>((object) fsScheduleRow, (object) fsScheduleRow.StartDate, (Exception) new PXSetPropertyException("The dates are invalid. The end date cannot be earlier than the start date."));
          cache.RaiseExceptionHandling<FSSchedule.endDate>((object) fsScheduleRow, (object) fsScheduleRow.EndDate, (Exception) new PXSetPropertyException("The dates are invalid. The end date cannot be earlier than the start date."));
        }
        else
        {
          DateTime? nullable4;
          if (fsServiceContractRow != null)
          {
            nullable3 = fsServiceContractRow.StartDate;
            nullable4 = fsScheduleRow.StartDate;
            if ((nullable3.HasValue & nullable4.HasValue ? (nullable3.GetValueOrDefault() > nullable4.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              cache.RaiseExceptionHandling<FSSchedule.startDate>((object) fsScheduleRow, (object) fsScheduleRow.StartDate, (Exception) new PXSetPropertyException("The dates are invalid. The schedule start date must be the same as or later than the start date of the related contract."));
          }
          if (fsServiceContractRow != null && fsServiceContractRow.ExpirationType == "E")
          {
            nullable4 = fsServiceContractRow.EndDate;
            if (nullable4.HasValue)
            {
              nullable4 = fsServiceContractRow.EndDate;
              nullable3 = fsScheduleRow.StartDate;
              if ((nullable4.HasValue & nullable3.HasValue ? (nullable4.GetValueOrDefault() < nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                cache.RaiseExceptionHandling<FSSchedule.startDate>((object) fsScheduleRow, (object) fsScheduleRow.StartDate, (Exception) new PXSetPropertyException("The dates are invalid. The schedule start date must be earlier than the expiration date of the related contract."));
            }
          }
          if (fsServiceContractRow != null && fsServiceContractRow.ExpirationType == "E")
          {
            nullable3 = fsServiceContractRow.EndDate;
            if (nullable3.HasValue)
            {
              nullable3 = fsServiceContractRow.EndDate;
              if (nullable3.HasValue)
              {
                nullable3 = fsServiceContractRow.EndDate;
                nullable4 = fsScheduleRow.EndDate;
                if ((nullable3.HasValue & nullable4.HasValue ? (nullable3.GetValueOrDefault() < nullable4.GetValueOrDefault() ? 1 : 0) : 0) == 0)
                  goto label_25;
              }
              cache.RaiseExceptionHandling<FSSchedule.endDate>((object) fsScheduleRow, (object) fsScheduleRow.EndDate, (Exception) new PXSetPropertyException("The dates are invalid. The schedule expiration date must be earlier than or the same as the expiration date of the related contract. "));
            }
          }
label_25:
          if (fsScheduleRow.FrequencyType == "D")
          {
            nullable1 = fsScheduleRow.DailyFrequency;
            nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
            int num = 0;
            if (nullable2.GetValueOrDefault() <= num & nullable2.HasValue)
            {
              cache.RaiseExceptionHandling<FSSchedule.dailyFrequency>((object) fsScheduleRow, (object) fsScheduleRow.DailyFrequency, (Exception) new PXSetPropertyException("For a schedule type with the Daily recurrence, the frequency has to be greater than 0."));
              return;
            }
          }
          if (fsScheduleRow.FrequencyType == "W")
          {
            nullable1 = fsScheduleRow.WeeklyFrequency;
            nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
            int num = 0;
            if (nullable2.GetValueOrDefault() <= num & nullable2.HasValue)
            {
              cache.RaiseExceptionHandling<FSSchedule.weeklyFrequency>((object) fsScheduleRow, (object) fsScheduleRow.WeeklyFrequency, (Exception) new PXSetPropertyException("For a schedule type with the Weekly recurrence, the frequency has to be greater than 0."));
              return;
            }
          }
          if (operation == 2 || operation == 1)
            fsScheduleRow.NextExecutionDate = SharedFunctions.GetNextExecution(cache, fsScheduleRow);
          if (operation != 2)
            return;
          nullable2 = fsScheduleRow.ScheduleID;
          int num1 = 0;
          int num2 = nullable2.GetValueOrDefault() > num1 & nullable2.HasValue ? 1 : 0;
        }
      }
    }
  }

  public void InsertContractAction(FSSchedule fsScheduleRow, PXDBOperation operation)
  {
    if (operation == 1 && !this.statusChanged || operation != 2 && operation != 1 && operation != 3)
      return;
    FSContractAction fsContractAction1 = (FSContractAction) ((PXSelectBase) this.ContractHistory).Cache.Insert((object) new FSContractAction()
    {
      Type = "S",
      ServiceContractID = fsScheduleRow.EntityID,
      ScheduleRefNbr = fsScheduleRow.RefNbr,
      ActionBusinessDate = ((PXGraph) this).Accessinfo.BusinessDate,
      ScheduleRecurrenceDescr = fsScheduleRow.RecurrenceDescription
    });
    if (operation == 2)
    {
      if (!this.IsCopyContract && fsScheduleRow.OrigScheduleRefNbr == null)
      {
        fsContractAction1.Action = "N";
      }
      else
      {
        fsContractAction1.Action = "C";
        fsContractAction1.OrigScheduleRefNbr = fsScheduleRow.OrigScheduleRefNbr;
        fsContractAction1.OrigServiceContractRefNbr = fsScheduleRow.OrigServiceContractRefNbr;
        fsScheduleRow.OrigServiceContractRefNbr = (string) null;
        fsScheduleRow.OrigScheduleRefNbr = (string) null;
      }
    }
    else if (operation == 1)
      fsContractAction1.Action = fsScheduleRow.Active.GetValueOrDefault() ? "A" : "I";
    else if (operation == 3)
      fsContractAction1.Action = "D";
    FSContractAction fsContractAction2 = (FSContractAction) ((PXSelectBase) this.ContractHistory).Cache.Update((object) fsContractAction1);
    ((PXSelectBase) this.ContractHistory).Cache.Persist((PXDBOperation) 2);
  }

  public virtual bool InventoryItemsAreIncluded()
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.distributionModule>() || !PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      return false;
    FSSrvOrdType current = ((PXSelectBase<FSSrvOrdType>) this.SrvOrdTypeSelected).Current;
    return current != null && current.AllowInventoryItems;
  }

  /// <summary>
  /// Makes visible the group that corresponds to the selected FrequencyType.
  /// </summary>
  public static void SetControlsState(PXCache cache, FSSchedule fsScheduleRow)
  {
    bool flag1 = fsScheduleRow.FrequencyType == "W";
    bool flag2 = fsScheduleRow.FrequencyType == "D";
    bool flag3 = fsScheduleRow.FrequencyType == "M";
    bool flag4 = fsScheduleRow.FrequencyType == "A";
    bool valueOrDefault1 = fsScheduleRow.Monthly2Selected.GetValueOrDefault();
    bool valueOrDefault2 = fsScheduleRow.Monthly3Selected.GetValueOrDefault();
    bool valueOrDefault3 = fsScheduleRow.Monthly4Selected.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<FSSchedule.dailyFrequency>(cache, (object) fsScheduleRow, flag2);
    PXUIFieldAttribute.SetVisible<FSSchedule.dailyLabel>(cache, (object) fsScheduleRow, flag2);
    PXUIFieldAttribute.SetVisible<FSSchedule.weeklyFrequency>(cache, (object) fsScheduleRow, flag1);
    PXUIFieldAttribute.SetVisible<FSSchedule.weeklyOnSun>(cache, (object) fsScheduleRow, flag1);
    PXUIFieldAttribute.SetVisible<FSSchedule.weeklyOnMon>(cache, (object) fsScheduleRow, flag1);
    PXUIFieldAttribute.SetVisible<FSSchedule.weeklyOnTue>(cache, (object) fsScheduleRow, flag1);
    PXUIFieldAttribute.SetVisible<FSSchedule.weeklyOnWed>(cache, (object) fsScheduleRow, flag1);
    PXUIFieldAttribute.SetVisible<FSSchedule.weeklyOnThu>(cache, (object) fsScheduleRow, flag1);
    PXUIFieldAttribute.SetVisible<FSSchedule.weeklyOnFri>(cache, (object) fsScheduleRow, flag1);
    PXUIFieldAttribute.SetVisible<FSSchedule.weeklyOnSat>(cache, (object) fsScheduleRow, flag1);
    PXUIFieldAttribute.SetVisible<FSSchedule.weeklyLabel>(cache, (object) fsScheduleRow, flag1);
    ServiceContractScheduleEntryBase<TGraph, TPrimary, TScheduleID, TEntityID, TCustomerID>.SetMonthlyControlsState(cache, fsScheduleRow);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyFrequency>(cache, (object) fsScheduleRow, flag3);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyLabel>(cache, (object) fsScheduleRow, flag3);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthly2Selected>(cache, (object) fsScheduleRow, flag3);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthly3Selected>(cache, (object) fsScheduleRow, flag3);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthly4Selected>(cache, (object) fsScheduleRow, flag3);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyRecurrenceType1>(cache, (object) fsScheduleRow, flag3);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyRecurrenceType2>(cache, (object) fsScheduleRow, flag3 & valueOrDefault1);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyRecurrenceType3>(cache, (object) fsScheduleRow, flag3 & valueOrDefault2);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyRecurrenceType4>(cache, (object) fsScheduleRow, flag3 & valueOrDefault3);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnDay1>(cache, (object) fsScheduleRow, flag3 && fsScheduleRow.MonthlyRecurrenceType1 == "D");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnDay2>(cache, (object) fsScheduleRow, flag3 & valueOrDefault1 && fsScheduleRow.MonthlyRecurrenceType2 == "D");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnDay3>(cache, (object) fsScheduleRow, flag3 & valueOrDefault2 && fsScheduleRow.MonthlyRecurrenceType3 == "D");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnDay4>(cache, (object) fsScheduleRow, flag3 & valueOrDefault3 && fsScheduleRow.MonthlyRecurrenceType4 == "D");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnWeek1>(cache, (object) fsScheduleRow, flag3 && fsScheduleRow.MonthlyRecurrenceType1 == "W");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnWeek2>(cache, (object) fsScheduleRow, flag3 & valueOrDefault1 && fsScheduleRow.MonthlyRecurrenceType2 == "W");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnWeek3>(cache, (object) fsScheduleRow, flag3 & valueOrDefault2 && fsScheduleRow.MonthlyRecurrenceType3 == "W");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnWeek4>(cache, (object) fsScheduleRow, flag3 & valueOrDefault3 && fsScheduleRow.MonthlyRecurrenceType4 == "W");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnDayOfWeek1>(cache, (object) fsScheduleRow, flag3 && fsScheduleRow.MonthlyRecurrenceType1 == "W");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnDayOfWeek2>(cache, (object) fsScheduleRow, flag3 & valueOrDefault1 && fsScheduleRow.MonthlyRecurrenceType2 == "W");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnDayOfWeek3>(cache, (object) fsScheduleRow, flag3 & valueOrDefault2 && fsScheduleRow.MonthlyRecurrenceType3 == "W");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnDayOfWeek4>(cache, (object) fsScheduleRow, flag3 & valueOrDefault3 && fsScheduleRow.MonthlyRecurrenceType4 == "W");
    PXUIFieldAttribute.SetVisible<FSSchedule.annualFrequency>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.yearlyLabel>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualRecurrenceType>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnJan>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnFeb>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnMar>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnApr>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnMay>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnJun>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnJul>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnAug>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnSep>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnOct>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnNov>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnDec>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnDay>(cache, (object) fsScheduleRow, flag4 && fsScheduleRow.AnnualRecurrenceType == "D");
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnWeek>(cache, (object) fsScheduleRow, flag4 && fsScheduleRow.AnnualRecurrenceType == "W");
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnDayOfWeek>(cache, (object) fsScheduleRow, flag4 && fsScheduleRow.AnnualRecurrenceType == "W");
    PXUIFieldAttribute.SetEnabled<FSSchedule.annualOnDay>(cache, (object) fsScheduleRow, flag4 && fsScheduleRow.AnnualRecurrenceType == "D");
    PXUIFieldAttribute.SetEnabled<FSSchedule.annualOnWeek>(cache, (object) fsScheduleRow, flag4 && fsScheduleRow.AnnualRecurrenceType == "W");
    PXUIFieldAttribute.SetEnabled<FSSchedule.annualOnDayOfWeek>(cache, (object) fsScheduleRow, flag4 && fsScheduleRow.AnnualRecurrenceType == "W");
  }

  /// <summary>Set exceptions in date fields.</summary>
  public static void SetDateFieldExceptions(
    PXCache cache,
    FSSchedule fsScheduleRow,
    Exception exception)
  {
    cache.RaiseExceptionHandling<FSSchedule.startDate>((object) fsScheduleRow, (object) fsScheduleRow.StartDate, exception);
    cache.RaiseExceptionHandling<FSSchedule.endDate>((object) fsScheduleRow, (object) fsScheduleRow.EndDate, exception);
  }

  /// <summary>Check if at least one month is selected.</summary>
  /// <param name="fsScheduleRow">Instance of the FSSchedule DAC.</param>
  /// <returns>True if at least one month is selected.</returns>
  public static bool HasSelectedAtLeastOneMonth(FSSchedule fsScheduleRow)
  {
    return fsScheduleRow.AnnualOnJan.GetValueOrDefault() || fsScheduleRow.AnnualOnFeb.GetValueOrDefault() || fsScheduleRow.AnnualOnMar.GetValueOrDefault() || fsScheduleRow.AnnualOnApr.GetValueOrDefault() || fsScheduleRow.AnnualOnMay.GetValueOrDefault() || fsScheduleRow.AnnualOnJun.GetValueOrDefault() || fsScheduleRow.AnnualOnJul.GetValueOrDefault() || fsScheduleRow.AnnualOnAug.GetValueOrDefault() || fsScheduleRow.AnnualOnSep.GetValueOrDefault() || fsScheduleRow.AnnualOnOct.GetValueOrDefault() || fsScheduleRow.AnnualOnNov.GetValueOrDefault() || fsScheduleRow.AnnualOnDec.GetValueOrDefault();
  }

  /// <summary>Check if at least one day of the week is selected.</summary>
  /// <param name="fsScheduleRow">Instance of the FSSchedule DAC.</param>
  /// <returns>True if at least one day is selected.</returns>
  public static bool HasSelectedAtLeastOneDay(FSSchedule fsScheduleRow)
  {
    return fsScheduleRow.WeeklyOnSun.GetValueOrDefault() || fsScheduleRow.WeeklyOnMon.GetValueOrDefault() || fsScheduleRow.WeeklyOnTue.GetValueOrDefault() || fsScheduleRow.WeeklyOnWed.GetValueOrDefault() || fsScheduleRow.WeeklyOnThu.GetValueOrDefault() || fsScheduleRow.WeeklyOnFri.GetValueOrDefault() || fsScheduleRow.WeeklyOnSat.GetValueOrDefault();
  }

  public static void SetMonthlyControlsState(PXCache cache, FSSchedule fsScheduleRow)
  {
    bool flag = fsScheduleRow.FrequencyType == "M";
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnDay1>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType1 == "D");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnWeek1>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType1 == "W");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnDayOfWeek1>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType1 == "W");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnDay2>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType2 == "D");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnWeek2>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType2 == "W");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnDayOfWeek2>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType2 == "W");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnDay3>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType3 == "D");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnWeek3>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType3 == "W");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnDayOfWeek3>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType3 == "W");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnDay4>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType4 == "D");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnWeek4>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType4 == "W");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnDayOfWeek4>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType4 == "W");
  }

  public static void CheckLineByLineType(
    PXCache cache,
    FSScheduleDet fsScheduleDetRow,
    PXErrorLevel errorLevel = 4)
  {
    switch (fsScheduleDetRow.LineType)
    {
      case "TEMPL":
        int? nullable1 = fsScheduleDetRow.InventoryID;
        if (nullable1.HasValue)
        {
          PXUIFieldAttribute.SetEnabled<FSScheduleDet.inventoryID>(cache, (object) fsScheduleDetRow, true);
          cache.RaiseExceptionHandling<FSScheduleDet.inventoryID>((object) fsScheduleDetRow, (object) null, (Exception) new PXSetPropertyException("The column must be empty for the selected line type.", errorLevel));
        }
        nullable1 = fsScheduleDetRow.SMEquipmentID;
        if (!nullable1.HasValue)
          PXUIFieldAttribute.SetEnabled<FSScheduleDet.SMequipmentID>(cache, (object) fsScheduleDetRow, true);
        nullable1 = fsScheduleDetRow.ServiceTemplateID;
        if (!nullable1.HasValue)
        {
          PXUIFieldAttribute.SetEnabled<FSScheduleDet.serviceTemplateID>(cache, (object) fsScheduleDetRow, true);
          cache.RaiseExceptionHandling<FSScheduleDet.serviceTemplateID>((object) fsScheduleDetRow, (object) null, (Exception) new PXSetPropertyException("Data is required for the selected line type.", errorLevel));
        }
        if (fsScheduleDetRow.TranDesc != null)
          break;
        PXUIFieldAttribute.SetEnabled<FSScheduleDet.tranDesc>(cache, (object) fsScheduleDetRow, true);
        cache.RaiseExceptionHandling<FSScheduleDet.tranDesc>((object) fsScheduleDetRow, (object) null, (Exception) new PXSetPropertyException("Data is required for the selected line type.", errorLevel));
        break;
      case "SERVI":
      case "NSTKI":
        int? nullable2 = fsScheduleDetRow.InventoryID;
        if (!nullable2.HasValue)
        {
          PXUIFieldAttribute.SetEnabled<FSScheduleDet.inventoryID>(cache, (object) fsScheduleDetRow, true);
          cache.RaiseExceptionHandling<FSScheduleDet.inventoryID>((object) fsScheduleDetRow, (object) null, (Exception) new PXSetPropertyException("Data is required for the selected line type.", errorLevel));
        }
        if (!fsScheduleDetRow.Qty.HasValue)
        {
          PXUIFieldAttribute.SetEnabled<FSScheduleDet.qty>(cache, (object) fsScheduleDetRow, true);
          cache.RaiseExceptionHandling<FSScheduleDet.qty>((object) fsScheduleDetRow, (object) null, (Exception) new PXSetPropertyException("Data is required for the selected line type.", errorLevel));
        }
        nullable2 = fsScheduleDetRow.SMEquipmentID;
        if (!nullable2.HasValue)
          PXUIFieldAttribute.SetEnabled<FSScheduleDet.SMequipmentID>(cache, (object) fsScheduleDetRow, true);
        nullable2 = fsScheduleDetRow.ServiceTemplateID;
        if (nullable2.HasValue)
        {
          PXUIFieldAttribute.SetEnabled<FSScheduleDet.serviceTemplateID>(cache, (object) fsScheduleDetRow, true);
          cache.RaiseExceptionHandling<FSScheduleDet.serviceTemplateID>((object) fsScheduleDetRow, (object) null, (Exception) new PXSetPropertyException("The column must be empty for the selected line type.", errorLevel));
        }
        if (fsScheduleDetRow.TranDesc != null)
          break;
        PXUIFieldAttribute.SetEnabled<FSScheduleDet.tranDesc>(cache, (object) fsScheduleDetRow, true);
        cache.RaiseExceptionHandling<FSScheduleDet.tranDesc>((object) fsScheduleDetRow, (object) null, (Exception) new PXSetPropertyException("Data is required for the selected line type.", errorLevel));
        break;
      case "SLPRO":
        if (!fsScheduleDetRow.InventoryID.HasValue)
        {
          PXUIFieldAttribute.SetEnabled<FSScheduleDet.inventoryID>(cache, (object) fsScheduleDetRow, true);
          cache.RaiseExceptionHandling<FSScheduleDet.inventoryID>((object) fsScheduleDetRow, (object) null, (Exception) new PXSetPropertyException("Data is required for the selected line type.", errorLevel));
        }
        if (!fsScheduleDetRow.Qty.HasValue)
        {
          PXUIFieldAttribute.SetEnabled<FSScheduleDet.qty>(cache, (object) fsScheduleDetRow, true);
          cache.RaiseExceptionHandling<FSScheduleDet.qty>((object) fsScheduleDetRow, (object) null, (Exception) new PXSetPropertyException("Data is required for the selected line type.", errorLevel));
        }
        if (fsScheduleDetRow.TranDesc != null)
          break;
        PXUIFieldAttribute.SetEnabled<FSScheduleDet.tranDesc>(cache, (object) fsScheduleDetRow, true);
        cache.RaiseExceptionHandling<FSScheduleDet.tranDesc>((object) fsScheduleDetRow, (object) null, (Exception) new PXSetPropertyException("Data is required for the selected line type.", errorLevel));
        break;
      case "CM_LN":
      case "IT_LN":
        int? nullable3 = fsScheduleDetRow.InventoryID;
        if (nullable3.HasValue)
        {
          PXUIFieldAttribute.SetEnabled<FSScheduleDet.inventoryID>(cache, (object) fsScheduleDetRow, true);
          cache.RaiseExceptionHandling<FSScheduleDet.inventoryID>((object) fsScheduleDetRow, (object) null, (Exception) new PXSetPropertyException("The column must be empty for the selected line type.", errorLevel));
        }
        nullable3 = fsScheduleDetRow.ServiceTemplateID;
        if (nullable3.HasValue)
        {
          PXUIFieldAttribute.SetEnabled<FSScheduleDet.serviceTemplateID>(cache, (object) fsScheduleDetRow, true);
          cache.RaiseExceptionHandling<FSScheduleDet.serviceTemplateID>((object) fsScheduleDetRow, (object) null, (Exception) new PXSetPropertyException("The column must be empty for the selected line type.", errorLevel));
        }
        if (fsScheduleDetRow.TranDesc != null)
          break;
        PXUIFieldAttribute.SetEnabled<FSScheduleDet.tranDesc>(cache, (object) fsScheduleDetRow, true);
        cache.RaiseExceptionHandling<FSScheduleDet.tranDesc>((object) fsScheduleDetRow, (object) null, (Exception) new PXSetPropertyException("Data is required for the selected line type.", errorLevel));
        break;
    }
  }

  /// <summary>
  /// This method enables or disables the fields on the grid depending on the selected LineType.
  /// </summary>
  public virtual void EnableDisable_LineType(PXCache cache, FSScheduleDet fsScheduleDetRow)
  {
    bool flag1 = fsScheduleDetRow.LineType == "SERVI" || fsScheduleDetRow.LineType == "NSTKI" || fsScheduleDetRow.LineType == "SLPRO";
    bool flag2 = (flag1 || fsScheduleDetRow.LineType == null) && !fsScheduleDetRow.ServiceTemplateID.HasValue;
    bool flag3 = (fsScheduleDetRow.LineType == null || fsScheduleDetRow.LineType == "TEMPL") && !fsScheduleDetRow.InventoryID.HasValue;
    PXUIFieldAttribute.SetEnabled<FSScheduleDet.inventoryID>(cache, (object) fsScheduleDetRow, flag2);
    PXDefaultAttribute.SetPersistingCheck<FSScheduleDet.inventoryID>(cache, (object) fsScheduleDetRow, flag1 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetEnabled<FSScheduleDet.qty>(cache, (object) fsScheduleDetRow, flag1 && fsScheduleDetRow.BillingRule != "TIME" && fsScheduleDetRow.InventoryID.HasValue);
    PXDefaultAttribute.SetPersistingCheck<FSScheduleDet.qty>(cache, (object) fsScheduleDetRow, !flag1 || !(fsScheduleDetRow.BillingRule != "TIME") || !fsScheduleDetRow.InventoryID.HasValue ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXUIFieldAttribute.SetEnabled<FSScheduleDet.serviceTemplateID>(cache, (object) fsScheduleDetRow, flag3);
    PXUIFieldAttribute.SetEnabled<FSScheduleDet.uOM>(cache, (object) fsScheduleDetRow, flag1);
    PXDefaultAttribute.SetPersistingCheck<FSScheduleDet.serviceTemplateID>(cache, (object) fsScheduleDetRow, fsScheduleDetRow.LineType == "TEMPL" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetEnabled<FSScheduleDet.billingRule>(cache, (object) fsScheduleDetRow, fsScheduleDetRow.LineType == "SERVI");
    PXUIFieldAttribute.SetEnabled<FSScheduleDet.equipmentAction>(cache, (object) fsScheduleDetRow, fsScheduleDetRow.LineType == "SLPRO");
    PXUIFieldAttribute.SetEnabled<FSScheduleDet.estimatedDuration>(cache, (object) fsScheduleDetRow, (fsScheduleDetRow.LineType == "SERVI" || fsScheduleDetRow.LineType == "NSTKI") && fsScheduleDetRow.InventoryID.HasValue);
  }

  /// <summary>
  /// This method reset the fields of the <c>fsScheduleDetRow</c> depending on the selected LineType.
  /// </summary>
  public static void ResetLineByType(FSScheduleDet fsScheduleDetRow, PXCache cache)
  {
    fsScheduleDetRow.BillingRule = "NONE";
    switch (fsScheduleDetRow.LineType)
    {
      case "NSTKI":
        fsScheduleDetRow.BillingRule = "FLRA";
        break;
      case "TEMPL":
        fsScheduleDetRow.SMEquipmentID = new int?();
        break;
      case "CM_LN":
      case "IT_LN":
        fsScheduleDetRow.ProjectTaskID = new int?();
        fsScheduleDetRow.CostCodeID = new int?();
        break;
    }
    fsScheduleDetRow.ServiceTemplateID = new int?();
    fsScheduleDetRow.InventoryID = new int?();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSchedule, FSSchedule.active> e)
  {
    if (e.Row == null)
      return;
    FSSchedule row = e.Row;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSchedule, FSSchedule.active>, FSSchedule, object>) e).NewValue = (object) (row.ScheduleGenType != "NA");
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSchedule, FSSchedule.active>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSchedule, FSSchedule.restrictionMinTime> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSchedule, FSSchedule.restrictionMinTime>, FSSchedule, object>) e).NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate.Value.AddHours(8.0);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSchedule, FSSchedule.restrictionMaxTime> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSchedule, FSSchedule.restrictionMaxTime>, FSSchedule, object>) e).NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate.Value.AddHours(12.0);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSchedule, FSSchedule.recurrenceDescription> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSchedule, FSSchedule.recurrenceDescription>, FSSchedule, object>) e).NewValue = (object) ScheduleHelperGraph.GetRecurrenceDescription(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSchedule, FSSchedule.startDate> e)
  {
    if (e.Row == null)
      return;
    FSSchedule row = e.Row;
    if (row.EndDate.HasValue && !SharedFunctions.IsValidDateRange(row.StartDate, row.EndDate))
      ServiceContractScheduleEntryBase<TGraph, TPrimary, TScheduleID, TEntityID, TCustomerID>.SetDateFieldExceptions(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSchedule, FSSchedule.startDate>>) e).Cache, row, (Exception) new PXSetPropertyException("The dates are invalid. The end date cannot be earlier than the start date.", (PXErrorLevel) 4));
    else
      ServiceContractScheduleEntryBase<TGraph, TPrimary, TScheduleID, TEntityID, TCustomerID>.SetDateFieldExceptions(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSchedule, FSSchedule.startDate>>) e).Cache, row, (Exception) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSchedule, FSSchedule.endDate> e)
  {
    if (e.Row == null)
      return;
    FSSchedule row = e.Row;
    if (row.EndDate.HasValue && !SharedFunctions.IsValidDateRange(row.StartDate, row.EndDate))
      ServiceContractScheduleEntryBase<TGraph, TPrimary, TScheduleID, TEntityID, TCustomerID>.SetDateFieldExceptions(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSchedule, FSSchedule.endDate>>) e).Cache, row, (Exception) new PXSetPropertyException("The dates are invalid. The end date cannot be earlier than the start date.", (PXErrorLevel) 4));
    else
      ServiceContractScheduleEntryBase<TGraph, TPrimary, TScheduleID, TEntityID, TCustomerID>.SetDateFieldExceptions(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSchedule, FSSchedule.endDate>>) e).Cache, row, (Exception) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSchedule, FSSchedule.noRunLimit> e)
  {
    if (e.Row == null)
      return;
    FSSchedule row = e.Row;
    row.RunLimit = new short?((short) !row.NoRunLimit.Value);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSchedule, FSSchedule.monthly2Selected> e)
  {
    if (e.Row == null)
      return;
    FSSchedule row = e.Row;
    bool? monthly2Selected = row.Monthly2Selected;
    bool flag = false;
    if (!(monthly2Selected.GetValueOrDefault() == flag & monthly2Selected.HasValue))
      return;
    row.Monthly3Selected = new bool?(false);
    row.Monthly4Selected = new bool?(false);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSchedule, FSSchedule.monthly3Selected> e)
  {
    if (e.Row == null)
      return;
    FSSchedule row = e.Row;
    bool? monthly3Selected = row.Monthly3Selected;
    bool flag = false;
    if (!(monthly3Selected.GetValueOrDefault() == flag & monthly3Selected.HasValue))
      return;
    row.Monthly4Selected = new bool?(false);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSchedule, FSSchedule.entityID> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSchedule, FSSchedule.active> e)
  {
    if (e.Row == null)
      return;
    bool? active = e.Row.Active;
    bool oldValue = (bool) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSSchedule, FSSchedule.active>, FSSchedule, object>) e).OldValue;
    this.statusChanged = !(active.GetValueOrDefault() == oldValue & active.HasValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSchedule, FSSchedule.srvOrdType> e)
  {
    if (e.Row == null)
      return;
    FSSchedule row = e.Row;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSchedule, FSSchedule.srvOrdType>>) e).Cache.SetDefaultExt<FSSchedule.dfltProjectTaskID>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSchedule, FSSchedule.customerID> e)
  {
    if (e.Row == null)
      return;
    FSSchedule row = e.Row;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSchedule, FSSchedule.customerID>>) e).Cache.SetDefaultExt<FSSchedule.projectID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSchedule, FSSchedule.projectID> e)
  {
    if (e.Row == null)
      return;
    if (this.ScheduleDetails != null)
    {
      foreach (PXResult<FSScheduleDet> pxResult in ((PXSelectBase<FSScheduleDet>) this.ScheduleDetails).Select(Array.Empty<object>()))
      {
        FSScheduleDet fsScheduleDet = PXResult<FSScheduleDet>.op_Implicit(pxResult);
        ((PXSelectBase) this.ScheduleDetails).Cache.SetDefaultExt<FSScheduleDet.projectID>((object) fsScheduleDet);
        if (ProjectDefaultAttribute.IsNonProject(e.Row.ProjectID) || !fsScheduleDet.CostCodeID.HasValue)
          ((PXSelectBase) this.ScheduleDetails).Cache.SetDefaultExt<FSScheduleDet.costCodeID>((object) fsScheduleDet);
      }
    }
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSchedule, FSSchedule.projectID>>) e).Cache.SetDefaultExt<FSSchedule.dfltProjectTaskID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSchedule, FSSchedule.dfltProjectTaskID> e)
  {
    if (e.Row == null)
      return;
    FSSchedule row = e.Row;
    if (this.ScheduleDetails == null)
      return;
    foreach (PXResult<FSScheduleDet> pxResult in ((PXSelectBase<FSScheduleDet>) this.ScheduleDetails).Select(Array.Empty<object>()))
    {
      FSScheduleDet fsScheduleDet1 = PXResult<FSScheduleDet>.op_Implicit(pxResult);
      int? valueOriginal = (int?) ((PXSelectBase) this.ScheduleDetails).Cache.GetValueOriginal<FSScheduleDet.projectID>((object) fsScheduleDet1);
      int? projectId = fsScheduleDet1.ProjectID;
      int? nullable1 = valueOriginal;
      if (projectId.GetValueOrDefault() == nullable1.GetValueOrDefault() & projectId.HasValue == nullable1.HasValue)
      {
        nullable1 = fsScheduleDet1.ProjectTaskID;
        if (nullable1.HasValue)
          continue;
      }
      PMTask pmTask = (PMTask) null;
      nullable1 = fsScheduleDet1.ProjectID;
      if (nullable1.HasValue)
      {
        nullable1 = e.Row.DfltProjectTaskID;
        if (nullable1.HasValue)
          pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskID, Equal<Required<PMTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) fsScheduleDet1.ProjectID,
            (object) e.Row.DfltProjectTaskID
          }));
      }
      FSScheduleDet fsScheduleDet2 = fsScheduleDet1;
      int? nullable2;
      if (pmTask == null)
      {
        nullable1 = new int?();
        nullable2 = nullable1;
      }
      else
        nullable2 = pmTask.TaskID;
      fsScheduleDet2.ProjectTaskID = nullable2;
      ((PXSelectBase<FSScheduleDet>) this.ScheduleDetails).Update(fsScheduleDet1);
    }
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSSchedule> e)
  {
    if (e.Row == null)
      return;
    FSSchedule row = e.Row;
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSchedule>>) e).Cache;
    PXUIFieldAttribute.SetEnabled<FSSchedule.active>(cache1, (object) row, row.ScheduleGenType != "NA");
    PXUIFieldAttribute.SetEnabled<FSSchedule.scheduleGenType>(cache1, (object) row, false);
    PXCache cache2 = ((PXSelectBase) this.ScheduleDetails).Cache;
    FSSrvOrdType current1 = ((PXSelectBase<FSSrvOrdType>) this.SrvOrdTypeSelected).Current;
    bool? postToSosipm;
    int num1;
    if (current1 == null)
    {
      num1 = 0;
    }
    else
    {
      postToSosipm = current1.PostToSOSIPM;
      num1 = postToSosipm.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetVisible<FSScheduleDet.equipmentAction>(cache2, (object) null, num1 != 0);
    PXCache cache3 = ((PXSelectBase) this.ScheduleDetails).Cache;
    FSSrvOrdType current2 = ((PXSelectBase<FSSrvOrdType>) this.SrvOrdTypeSelected).Current;
    int num2;
    if (current2 == null)
    {
      num2 = 0;
    }
    else
    {
      postToSosipm = current2.PostToSOSIPM;
      num2 = postToSosipm.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetVisible<FSScheduleDet.SMequipmentID>(cache3, (object) null, num2 != 0);
    PXCache cache4 = ((PXSelectBase) this.ScheduleDetails).Cache;
    FSSrvOrdType current3 = ((PXSelectBase<FSSrvOrdType>) this.SrvOrdTypeSelected).Current;
    int num3;
    if (current3 == null)
    {
      num3 = 0;
    }
    else
    {
      postToSosipm = current3.PostToSOSIPM;
      num3 = postToSosipm.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetVisible<FSScheduleDet.componentID>(cache4, (object) null, num3 != 0);
    PXCache cache5 = ((PXSelectBase) this.ScheduleDetails).Cache;
    FSSrvOrdType current4 = ((PXSelectBase<FSSrvOrdType>) this.SrvOrdTypeSelected).Current;
    int num4;
    if (current4 == null)
    {
      num4 = 0;
    }
    else
    {
      postToSosipm = current4.PostToSOSIPM;
      num4 = postToSosipm.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetVisible<FSScheduleDet.equipmentLineRef>(cache5, (object) null, num4 != 0);
    SharedFunctions.SetVisibleEnableProjectField<FSSchedule.dfltProjectTaskID>(cache1, (object) row, row.ProjectID);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSSchedule> e)
  {
    if (e.Row == null)
      return;
    object obj;
    ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSSchedule>>) e).Cache.RaiseFieldDefaulting<FSSchedule.recurrenceDescription>((object) e.Row, ref obj);
    e.Row.RecurrenceDescription = (string) obj;
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSSchedule> e)
  {
    this.FSSchedule_Row_Deleted_PartialHandler(((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<FSSchedule>>) e).Cache, ((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<FSSchedule>>) e).Args);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSSchedule> e)
  {
    if (e.Row != null && e.Row.ScheduleGenType == "NA")
      throw new PXException("The document cannot be saved because the associated service contract has the None schedule type.");
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSSchedule> e)
  {
    if (e.Row == null)
      return;
    if (e.TranStatus == null && e.Operation == 3)
    {
      FSSchedule row = e.Row;
      this.SyncFSSalesPrice(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<FSSchedule>>) e).Cache, row.EntityID, e.Operation, (FSScheduleDet) null);
    }
    if (e.TranStatus != 1)
      return;
    this.InsertContractAction(e.Row, e.Operation);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSScheduleDet, FSScheduleDet.costCodeID> e)
  {
    if (e.Row == null)
      return;
    if (!ProjectDefaultAttribute.IsNonProject((int?) ((PXSelectBase<FSServiceContract>) this.CurrentServiceContract).Current?.ProjectID) && CostCodeAttribute.UseCostCode())
    {
      int? nullable1 = e.Row.InventoryID;
      if (nullable1.HasValue || e.Row.LineType == "TEMPL")
      {
        PX.Data.Events.FieldDefaulting<FSScheduleDet, FSScheduleDet.costCodeID> fieldDefaulting = e;
        FSServiceContract current = ((PXSelectBase<FSServiceContract>) this.CurrentServiceContract).Current;
        int? nullable2;
        if (current == null)
        {
          nullable1 = new int?();
          nullable2 = nullable1;
        }
        else
          nullable2 = current.DfltCostCodeID;
        // ISSUE: variable of a boxed type
        __Boxed<int?> local = (ValueType) nullable2;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSScheduleDet, FSScheduleDet.costCodeID>, FSScheduleDet, object>) fieldDefaulting).NewValue = (object) local;
        goto label_8;
      }
    }
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSScheduleDet, FSScheduleDet.costCodeID>, FSScheduleDet, object>) e).NewValue = (object) null;
label_8:
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSScheduleDet, FSScheduleDet.costCodeID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSScheduleDet, FSScheduleDet.qty> e)
  {
    if (e.Row == null)
      return;
    FSScheduleDet row = e.Row;
    if (!(row.BillingRule == "TIME") || !(row.LineType == "SERVI"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSScheduleDet, FSScheduleDet.qty>, FSScheduleDet, object>) e).NewValue = (object) PXDBQuantityAttribute.Round(new Decimal?(Decimal.Divide((Decimal) row.EstimatedDuration.GetValueOrDefault(), 60M)));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.inventoryID> e)
  {
    if (e.Row == null)
      return;
    FSScheduleDet row = e.Row;
    PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.inventoryID>>) e).Cache.Graph, row.InventoryID);
    if (inventoryItemRow != null && row.LineType == null)
    {
      bool? stkItem = inventoryItemRow.StkItem;
      row.LineType = !stkItem.GetValueOrDefault() ? (!(inventoryItemRow.ItemType == "S") ? "NSTKI" : "SERVI") : "SLPRO";
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.inventoryID>>) e).Cache.SetDefaultExt<FSScheduleDet.billingRule>((object) row);
    }
    if (inventoryItemRow != null)
      row.TranDesc = inventoryItemRow.Descr;
    if (row.IsInventoryItem)
      SharedFunctions.UpdateEquipmentFields((PXGraph) this, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.inventoryID>>) e).Cache, (object) row, row.InventoryID, ((PXSelectBase<FSSrvOrdType>) this.SrvOrdTypeSelected).Current);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.inventoryID>>) e).Cache.SetDefaultExt<FSScheduleDet.estimatedDuration>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.inventoryID>>) e).Cache.SetDefaultExt<FSScheduleDet.qty>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.inventoryID>>) e).Cache.SetDefaultExt<FSScheduleDet.uOM>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.serviceTemplateID> e)
  {
    if (e.Row == null)
      return;
    FSScheduleDet row = e.Row;
    FSServiceTemplate fsServiceTemplate = FSServiceTemplate.PK.Find((PXGraph) this, row.ServiceTemplateID);
    if (fsServiceTemplate != null && row.LineType == null)
      row.LineType = "TEMPL";
    if (fsServiceTemplate != null)
      row.TranDesc = fsServiceTemplate.Descr;
    if (row.IsInventoryItem)
      SharedFunctions.UpdateEquipmentFields((PXGraph) this, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.serviceTemplateID>>) e).Cache, (object) row, row.InventoryID, ((PXSelectBase<FSSrvOrdType>) this.SrvOrdTypeSelected).Current);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.serviceTemplateID>>) e).Cache.SetDefaultExt<FSScheduleDet.qty>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.billingRule> e)
  {
    if (e.Row == null || !(e.Row.BillingRule == "TIME"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.billingRule>>) e).Cache.SetDefaultExt<FSScheduleDet.qty>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.estimatedDuration> e)
  {
    if (e.Row == null || !(e.Row.BillingRule == "TIME"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.estimatedDuration>>) e).Cache.SetDefaultExt<FSScheduleDet.qty>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.lineType> e)
  {
    if (e.Row == null)
      return;
    ServiceContractScheduleEntryBase<TGraph, TPrimary, TScheduleID, TEntityID, TCustomerID>.ResetLineByType(e.Row, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.lineType>>) e).Cache);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.equipmentAction> e)
  {
    if (e.Row == null)
      return;
    FSScheduleDet row = e.Row;
    if (!row.IsInventoryItem)
      return;
    SharedFunctions.ResetEquipmentFields(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.equipmentAction>>) e).Cache, (object) row);
    SharedFunctions.SetEquipmentFieldEnablePersistingCheck(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.equipmentAction>>) e).Cache, (object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.equipmentLineRef> e)
  {
    if (e.Row == null)
      return;
    FSScheduleDet row = e.Row;
    if (row.ComponentID.HasValue)
      return;
    row.ComponentID = SharedFunctions.GetEquipmentComponentID((PXGraph) this, row.SMEquipmentID, row.EquipmentLineRef);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.tranDesc> e)
  {
    if (e.Row == null)
      return;
    FSScheduleDet row = e.Row;
    if (string.IsNullOrEmpty(row.TranDesc) || row.LineType != null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSScheduleDet, FSScheduleDet.tranDesc>>) e).Cache.SetValueExt<FSScheduleDet.lineType>((object) row, (object) "IT_LN");
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSScheduleDet> e)
  {
    if (e.Row == null)
      return;
    FSScheduleDet row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSScheduleDet>>) e).Cache;
    this.EnableDisable_LineType(cache, row);
    SharedFunctions.SetEquipmentFieldEnablePersistingCheck(cache, (object) row, false);
    FSSchedule current = (object) ((PXSelectBase<TPrimary>) this.ContractScheduleRecords).Current as FSSchedule;
    if (row != null && current != null)
      SharedFunctions.SetEnableCostCodeProjectTask<FSScheduleDet.projectTaskID, FSScheduleDet.costCodeID>(cache, (object) row, row.LineType, current.ProjectID);
    bool includeIN = this.InventoryItemsAreIncluded();
    FSLineType.SetLineTypeList<FSScheduleDet.lineType>(cache, (object) null, includeIN, true, false, true, false);
    if (((PXSelectBase<FSSrvOrdType>) this.SrvOrdTypeSelected).Current == null || !(((PXSelectBase<FSSrvOrdType>) this.SrvOrdTypeSelected).Current.PostTo == "PM"))
      return;
    PXUIFieldAttribute.SetEnabled<FSScheduleDet.equipmentAction>(cache, (object) null, false);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSScheduleDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSScheduleDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSScheduleDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSScheduleDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSScheduleDet> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet fsAppointmentDet = ((IEnumerable<FSAppointmentDet>) ((PXSelectBase<FSAppointmentDet>) this.CurrentScheduleAppointmentDet).Select<FSAppointmentDet>(new object[1]
    {
      (object) e.Row.ScheduleDetID
    })).FirstOrDefault<FSAppointmentDet>();
    if (fsAppointmentDet == null)
      return;
    fsAppointmentDet.ScheduleDetID = new int?();
    ((PXSelectBase<FSAppointmentDet>) this.CurrentScheduleAppointmentDet).Update(fsAppointmentDet);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSScheduleDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSScheduleDet> e)
  {
    if (e.Row == null)
      return;
    FSScheduleDet row = e.Row;
    ServiceContractScheduleEntryBase<TGraph, TPrimary, TScheduleID, TEntityID, TCustomerID>.CheckLineByLineType(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSScheduleDet>>) e).Cache, row);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSScheduleDet> e)
  {
    if (e.Row == null || ((PXSelectBase<FSServiceContract>) this.CurrentServiceContract).Current == null || e.TranStatus != null)
      return;
    this.SyncFSSalesPrice(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<FSScheduleDet>>) e).Cache, ((PXSelectBase<FSServiceContract>) this.CurrentServiceContract).Current.ServiceContractID, e.Operation, e.Row);
  }

  [PXDynamicButton(new string[] {"PasteLine", "ResetOrder"}, new string[] {"Paste Line", "Reset Order"}, TranslationKeyType = typeof (PX.Objects.Common.Messages))]
  public class ScheduleDetOrdered : 
    PXOrderedSelect<TPrimary, FSScheduleDet, Where<FSScheduleDet.scheduleID, Equal<Current<TScheduleID>>>, OrderBy<Asc<FSScheduleDet.sortOrder, Asc<FSScheduleDet.lineNbr>>>>
  {
    public const string PasteLineCommand = "PasteLine";
    public const string ResetOrderCommand = "ResetOrder";

    public ScheduleDetOrdered(PXGraph graph)
      : base(graph)
    {
    }

    public ScheduleDetOrdered(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }

    protected virtual void AddActions(PXGraph graph)
    {
      PXGraph pxGraph1 = graph;
      ServiceContractScheduleEntryBase<TGraph, TPrimary, TScheduleID, TEntityID, TCustomerID>.ScheduleDetOrdered scheduleDetOrdered1 = this;
      // ISSUE: virtual method pointer
      PXButtonDelegate pxButtonDelegate1 = new PXButtonDelegate((object) scheduleDetOrdered1, __vmethodptr(scheduleDetOrdered1, PasteLine));
      ((PXOrderedSelectBase<TPrimary, FSScheduleDet>) this).AddAction(pxGraph1, "PasteLine", "Paste Line", pxButtonDelegate1, (List<PXEventSubscriberAttribute>) null);
      PXGraph pxGraph2 = graph;
      ServiceContractScheduleEntryBase<TGraph, TPrimary, TScheduleID, TEntityID, TCustomerID>.ScheduleDetOrdered scheduleDetOrdered2 = this;
      // ISSUE: virtual method pointer
      PXButtonDelegate pxButtonDelegate2 = new PXButtonDelegate((object) scheduleDetOrdered2, __vmethodptr(scheduleDetOrdered2, ResetOrder));
      ((PXOrderedSelectBase<TPrimary, FSScheduleDet>) this).AddAction(pxGraph2, "ResetOrder", "Reset Order", pxButtonDelegate2, (List<PXEventSubscriberAttribute>) null);
    }
  }
}
