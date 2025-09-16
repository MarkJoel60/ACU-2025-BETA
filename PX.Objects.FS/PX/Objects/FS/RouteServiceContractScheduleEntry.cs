// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteServiceContractScheduleEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.DependencyInjection;
using PX.LicensePolicy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable enable
namespace PX.Objects.FS;

public class RouteServiceContractScheduleEntry : 
  ServiceContractScheduleEntryBase<
  #nullable disable
  RouteServiceContractScheduleEntry, FSRouteContractSchedule, FSSchedule.scheduleID, FSRouteContractSchedule.entityID, FSRouteContractSchedule.customerID>,
  IGraphWithInitialization
{
  [PXCopyPasteHiddenFields(new Type[] {typeof (FSScheduleRoute.globalSequence)})]
  public PXSelect<FSScheduleRoute, Where<FSScheduleRoute.scheduleID, Equal<Current<FSSchedule.scheduleID>>>> ScheduleRoutes;
  public PXFilter<PX.Objects.FS.WeekCodeFilter> WeekCodeFilter;
  public PXSelectReadonly<FSWeekCodeDate> WeekCodeDateRecords;
  public PXAction<FSRouteContractSchedule> openRouteScheduleProcess;

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
        (PXDataFieldValue) new PXDataFieldValue<FSSchedule.customerID>((object) (int?) ((PXSelectBase<FSRouteContractSchedule>) ((ServiceContractScheduleEntryBase<RouteServiceContractScheduleEntry, FSRouteContractSchedule, FSSchedule.scheduleID, FSRouteContractSchedule.entityID, FSRouteContractSchedule.customerID>) graph).ContractScheduleRecords).Current?.CustomerID),
        (PXDataFieldValue) new PXDataFieldValue<FSSchedule.entityID>((object) (int?) ((PXSelectBase<FSRouteContractSchedule>) ((ServiceContractScheduleEntryBase<RouteServiceContractScheduleEntry, FSRouteContractSchedule, FSSchedule.scheduleID, FSRouteContractSchedule.entityID, FSRouteContractSchedule.customerID>) graph).ContractScheduleRecords).Current?.EntityID)
      }))
    });
    ((PXGraph) this).OnBeforeCommit += this._licenseLimits.GetCheckerDelegate<FSSchedule>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (FSScheduleDet), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[1]
      {
        (PXDataFieldValue) new PXDataFieldValue<FSScheduleDet.scheduleID>((object) (int?) ((PXSelectBase<FSRouteContractSchedule>) ((ServiceContractScheduleEntryBase<RouteServiceContractScheduleEntry, FSRouteContractSchedule, FSSchedule.scheduleID, FSRouteContractSchedule.entityID, FSRouteContractSchedule.customerID>) graph).ContractScheduleRecords).Current?.ScheduleID)
      }))
    });
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Branch ID", Enabled = false)]
  [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  protected virtual void FSRouteContractSchedule_BranchID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Branch Location ID", Enabled = false)]
  [FSSelectorBranchLocationByFSSchedule]
  [PXFormula(typeof (Default<FSSchedule.branchID>))]
  protected virtual void FSRouteContractSchedule_BranchLocationID_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(Visible = false)]
  [PXDBCreatedByScreenID]
  protected virtual void FSRouteContractSchedule_CreatedByScreenID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Description")]
  protected virtual void FSRouteContractSchedule_RecurrenceDescription_CacheAttached(PXCache sender)
  {
  }

  [PXUIField]
  [PXButton]
  public virtual void OpenRouteScheduleProcess()
  {
    RouteScheduleProcess instance = PXGraph.CreateInstance<RouteScheduleProcess>();
    ((PXSelectBase<RouteServiceContractFilter>) instance.Filter).Insert(new RouteServiceContractFilter()
    {
      ScheduleID = ((PXSelectBase<FSRouteContractSchedule>) this.ContractScheduleRecords).Current.ScheduleID,
      FromDate = ((PXSelectBase<FSRouteContractSchedule>) this.ContractScheduleRecords).Current.StartDate,
      ToDate = ((PXSelectBase<FSRouteContractSchedule>) this.ContractScheduleRecords).Current.EndDate ?? ((PXSelectBase<FSRouteContractSchedule>) this.ContractScheduleRecords).Current.StartDate
    });
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  public virtual IEnumerable weekCodeDateRecords()
  {
    FSRouteContractSchedule current1 = ((PXSelectBase<FSRouteContractSchedule>) this.ContractScheduleRecords).Current;
    List<object> objectList1 = new List<object>();
    List<object> objectList2 = new List<object>();
    List<int> intList = new List<int>();
    BqlCommand bqlCommand = (BqlCommand) new Select<FSWeekCodeDate>();
    Regex regex1 = new Regex("^[1-4]$");
    Regex regex2 = new Regex("^[a-bA-B]$");
    Regex regex3 = new Regex("^[c-fC-F]$");
    Regex regex4 = new Regex("^[s-zS-Z]$");
    if (current1 == null || string.IsNullOrEmpty(current1.WeekCode))
      return (IEnumerable) objectList1;
    foreach (string weekcode in SharedFunctions.SplitWeekcodeByComma(current1.WeekCode))
    {
      List<string> stringList = SharedFunctions.SplitWeekcodeInChars(weekcode);
      string str1;
      string str2 = str1 = "%";
      string str3 = str1;
      string str4 = str1;
      string str5 = str1;
      foreach (string str6 in stringList)
      {
        string upper = str6.ToUpper();
        if (regex1.IsMatch(upper))
          str5 = upper;
        else if (regex2.IsMatch(upper))
          str4 = upper;
        else if (regex3.IsMatch(upper))
          str3 = upper;
        else if (regex4.IsMatch(upper))
          str2 = upper;
      }
      bqlCommand = bqlCommand.WhereOr(typeof (Where2<Where<FSWeekCodeDate.weekCodeP1, Like<Required<FSWeekCodeDate.weekCodeP1>>, Or<FSWeekCodeDate.weekCodeP1, Like<Required<FSWeekCodeDate.weekCodeP1>>, Or<FSWeekCodeDate.weekCodeP1, IsNull>>>, And2<Where<FSWeekCodeDate.weekCodeP2, Like<Required<FSWeekCodeDate.weekCodeP2>>, Or<FSWeekCodeDate.weekCodeP2, Like<Required<FSWeekCodeDate.weekCodeP2>>, Or<FSWeekCodeDate.weekCodeP2, IsNull>>>, And2<Where<FSWeekCodeDate.weekCodeP3, Like<Required<FSWeekCodeDate.weekCodeP3>>, Or<FSWeekCodeDate.weekCodeP3, Like<Required<FSWeekCodeDate.weekCodeP3>>, Or<FSWeekCodeDate.weekCodeP3, IsNull>>>, And<Where<FSWeekCodeDate.weekCodeP4, Like<Required<FSWeekCodeDate.weekCodeP4>>, Or<FSWeekCodeDate.weekCodeP4, Like<Required<FSWeekCodeDate.weekCodeP4>>, Or<FSWeekCodeDate.weekCodeP4, IsNull>>>>>>>));
      objectList2.Add((object) str5);
      objectList2.Add((object) str5.ToLower());
      objectList2.Add((object) str4);
      objectList2.Add((object) str4.ToLower());
      objectList2.Add((object) str3);
      objectList2.Add((object) str3.ToLower());
      objectList2.Add((object) str2);
      objectList2.Add((object) str2.ToLower());
    }
    PX.Objects.FS.WeekCodeFilter current2 = ((PXSelectBase<PX.Objects.FS.WeekCodeFilter>) this.WeekCodeFilter).Current;
    if (current2 != null)
    {
      DateTime? dateBegin = current2.DateBegin;
      DateTime? nullable = new DateTime?();
      if (current2.DateEnd.HasValue)
        nullable = current2.DateEnd;
      if (dateBegin.HasValue && !nullable.HasValue)
        nullable = current2.DateEnd.HasValue ? current2.DateEnd : new DateTime?(dateBegin.Value.AddYears(1));
      if (dateBegin.HasValue)
      {
        bqlCommand = bqlCommand.WhereAnd(typeof (Where<FSWeekCodeDate.weekCodeDate, GreaterEqual<Required<PX.Objects.FS.FromToFilter.dateBegin>>>));
        objectList2.Add((object) dateBegin);
      }
      if (nullable.HasValue)
      {
        bqlCommand = bqlCommand.WhereAnd(typeof (Where<FSWeekCodeDate.weekCodeDate, LessEqual<Required<PX.Objects.FS.FromToFilter.dateEnd>>>));
        objectList2.Add((object) nullable);
      }
    }
    if (current1.FrequencyType == "W")
    {
      bool? nullable = current1.WeeklyOnSun;
      if (nullable.GetValueOrDefault())
        intList.Add(0);
      nullable = current1.WeeklyOnMon;
      if (nullable.GetValueOrDefault())
        intList.Add(1);
      nullable = current1.WeeklyOnTue;
      if (nullable.GetValueOrDefault())
        intList.Add(2);
      nullable = current1.WeeklyOnWed;
      if (nullable.GetValueOrDefault())
        intList.Add(3);
      nullable = current1.WeeklyOnThu;
      if (nullable.GetValueOrDefault())
        intList.Add(4);
      nullable = current1.WeeklyOnFri;
      if (nullable.GetValueOrDefault())
        intList.Add(5);
      nullable = current1.WeeklyOnSat;
      if (nullable.GetValueOrDefault())
        intList.Add(6);
      if (intList != null && intList.Count > 0)
      {
        bqlCommand = bqlCommand.WhereAnd(InHelper<FSWeekCodeDate.dayOfWeek>.Create(intList.Count));
        foreach (int num in intList)
          objectList2.Add((object) num);
      }
    }
    return (IEnumerable) new PXView((PXGraph) this, true, bqlCommand).SelectMulti(objectList2.ToArray());
  }

  public virtual IEnumerable scheduleProjectionRecords()
  {
    return (IEnumerable) this.Delegate_ScheduleProjectionRecords(((PXSelectBase) this.ContractScheduleRecords).Cache, (FSSchedule) ((PXSelectBase<FSRouteContractSchedule>) this.ContractScheduleRecords).Current, ((PXSelectBase<PX.Objects.FS.FromToFilter>) this.FromToFilter).Current, "IRSC");
  }

  /// <summary>
  /// Allows to reset the Route field values when the VehicleTypeID selected in the header changes.
  /// </summary>
  public virtual void ResetRouteFields(FSRouteContractSchedule fsScheduleRow)
  {
    if (((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Current == null)
      return;
    FSRoute fsRoute = PXResultset<FSRoute>.op_Implicit(PXSelectBase<FSRoute, PXSelect<FSRoute>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (fsRoute != null)
    {
      int? dfltRouteId = ((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Current.DfltRouteID;
      int? routeId = fsRoute.RouteID;
      if (dfltRouteId.GetValueOrDefault() == routeId.GetValueOrDefault() & dfltRouteId.HasValue == routeId.HasValue)
        goto label_5;
    }
    ((PXSelectBase) this.ScheduleRoutes).Cache.SetValueExt<FSScheduleRoute.dfltRouteID>((object) ((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Current, (object) null);
label_5:
    if (fsRoute != null)
    {
      int? routeIdSunday = ((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Current.RouteIDSunday;
      int? routeId = fsRoute.RouteID;
      if (routeIdSunday.GetValueOrDefault() == routeId.GetValueOrDefault() & routeIdSunday.HasValue == routeId.HasValue)
        goto label_8;
    }
    ((PXSelectBase) this.ScheduleRoutes).Cache.SetValueExt<FSScheduleRoute.routeIDSunday>((object) ((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Current, (object) null);
label_8:
    if (fsRoute != null)
    {
      int? routeIdMonday = ((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Current.RouteIDMonday;
      int? routeId = fsRoute.RouteID;
      if (routeIdMonday.GetValueOrDefault() == routeId.GetValueOrDefault() & routeIdMonday.HasValue == routeId.HasValue)
        goto label_11;
    }
    ((PXSelectBase) this.ScheduleRoutes).Cache.SetValueExt<FSScheduleRoute.routeIDMonday>((object) ((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Current, (object) null);
label_11:
    if (fsRoute != null)
    {
      int? routeIdTuesday = ((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Current.RouteIDTuesday;
      int? routeId = fsRoute.RouteID;
      if (routeIdTuesday.GetValueOrDefault() == routeId.GetValueOrDefault() & routeIdTuesday.HasValue == routeId.HasValue)
        goto label_14;
    }
    ((PXSelectBase) this.ScheduleRoutes).Cache.SetValueExt<FSScheduleRoute.routeIDTuesday>((object) ((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Current, (object) null);
label_14:
    if (fsRoute != null)
    {
      int? routeIdWednesday = ((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Current.RouteIDWednesday;
      int? routeId = fsRoute.RouteID;
      if (routeIdWednesday.GetValueOrDefault() == routeId.GetValueOrDefault() & routeIdWednesday.HasValue == routeId.HasValue)
        goto label_17;
    }
    ((PXSelectBase) this.ScheduleRoutes).Cache.SetValueExt<FSScheduleRoute.routeIDWednesday>((object) ((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Current, (object) null);
label_17:
    if (fsRoute != null)
    {
      int? routeIdThursday = ((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Current.RouteIDThursday;
      int? routeId = fsRoute.RouteID;
      if (routeIdThursday.GetValueOrDefault() == routeId.GetValueOrDefault() & routeIdThursday.HasValue == routeId.HasValue)
        goto label_20;
    }
    ((PXSelectBase) this.ScheduleRoutes).Cache.SetValueExt<FSScheduleRoute.routeIDThursday>((object) ((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Current, (object) null);
label_20:
    if (fsRoute != null)
    {
      int? routeIdFriday = ((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Current.RouteIDFriday;
      int? routeId = fsRoute.RouteID;
      if (routeIdFriday.GetValueOrDefault() == routeId.GetValueOrDefault() & routeIdFriday.HasValue == routeId.HasValue)
        goto label_23;
    }
    ((PXSelectBase) this.ScheduleRoutes).Cache.SetValueExt<FSScheduleRoute.routeIDFriday>((object) ((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Current, (object) null);
label_23:
    if (fsRoute != null)
    {
      int? routeIdSaturday = ((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Current.RouteIDSaturday;
      int? routeId = fsRoute.RouteID;
      if (routeIdSaturday.GetValueOrDefault() == routeId.GetValueOrDefault() & routeIdSaturday.HasValue == routeId.HasValue)
        return;
    }
    ((PXSelectBase) this.ScheduleRoutes).Cache.SetValueExt<FSScheduleRoute.routeIDSaturday>((object) ((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Current, (object) null);
  }

  /// <summary>
  /// Validates if a Week Code is well formatted. (Creates an exception in the cache parameter).
  /// </summary>
  public virtual void ValidateWeekCode(
    PXCache cache,
    FSRouteContractSchedule fsRouteContractScheduleRow)
  {
    List<object> objectList = new List<object>();
    bool flag = false;
    Regex regex = new Regex("^[1-4]?[a-bA-B]?[c-fC-F]?[s-zS-Z]?$");
    if (fsRouteContractScheduleRow.WeekCode == null)
      return;
    foreach (string str in SharedFunctions.SplitWeekcodeByComma(fsRouteContractScheduleRow.WeekCode))
    {
      if (string.IsNullOrEmpty(str))
      {
        cache.RaiseExceptionHandling<FSSchedule.weekCode>((object) fsRouteContractScheduleRow, (object) fsRouteContractScheduleRow.WeekCode, (Exception) new PXSetPropertyException("Week codes cannot be empty.", (PXErrorLevel) 4));
        flag = true;
      }
      else if (!SharedFunctions.IsAValidWeekCodeLength(str))
      {
        cache.RaiseExceptionHandling<FSSchedule.weekCode>((object) fsRouteContractScheduleRow, (object) fsRouteContractScheduleRow.WeekCode, (Exception) new PXSetPropertyException("The length of each week code must be less than or equal to 4.", (PXErrorLevel) 4));
        flag = true;
      }
      else
      {
        foreach (string splitWeekcodeInChar in SharedFunctions.SplitWeekcodeInChars(str))
        {
          if (!SharedFunctions.IsAValidCharForWeekCode(splitWeekcodeInChar))
          {
            cache.RaiseExceptionHandling<FSSchedule.weekCode>((object) fsRouteContractScheduleRow, (object) fsRouteContractScheduleRow.WeekCode, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefix("The {0} character is not allowed to build valid week codes.", new object[1]
            {
              (object) splitWeekcodeInChar
            }), (PXErrorLevel) 4));
            flag = true;
          }
        }
        if (!regex.IsMatch(str) && !flag)
        {
          cache.RaiseExceptionHandling<FSSchedule.weekCode>((object) fsRouteContractScheduleRow, (object) fsRouteContractScheduleRow.WeekCode, (Exception) new PXSetPropertyException("At least one week code is not correctly structured. Check field label for examples.", (PXErrorLevel) 4));
          flag = true;
        }
      }
      int num = flag ? 1 : 0;
    }
  }

  /// <summary>
  /// Verifies if the specified recurrence dates match the Route's definition.
  /// </summary>
  /// <param name="cache">PXCache instance.</param>
  /// <param name="fsRouteContractScheduleRow">FSRouteContractSchedule object row.</param>
  public virtual void ValidateDays(
    PXCache cache,
    FSRouteContractSchedule fsRouteContractScheduleRow)
  {
    FSScheduleRoute current = ((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Current;
    if (current == null || !current.DfltRouteID.HasValue)
      return;
    FSRoute fsRoute = FSRoute.PK.Find((PXGraph) this, current.DfltRouteID);
    if (fsRoute != null && fsRouteContractScheduleRow.FrequencyType == "W")
    {
      bool? nullable = fsRouteContractScheduleRow.WeeklyOnMon;
      if (nullable.GetValueOrDefault())
      {
        nullable = fsRoute.ActiveOnMonday;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
          goto label_17;
      }
      nullable = fsRouteContractScheduleRow.WeeklyOnThu;
      if (nullable.GetValueOrDefault())
      {
        nullable = fsRoute.ActiveOnThursday;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
          goto label_17;
      }
      nullable = fsRouteContractScheduleRow.WeeklyOnWed;
      if (nullable.GetValueOrDefault())
      {
        nullable = fsRoute.ActiveOnWednesday;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
          goto label_17;
      }
      nullable = fsRouteContractScheduleRow.WeeklyOnTue;
      if (nullable.GetValueOrDefault())
      {
        nullable = fsRoute.ActiveOnTuesday;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
          goto label_17;
      }
      nullable = fsRouteContractScheduleRow.WeeklyOnFri;
      if (nullable.GetValueOrDefault())
      {
        nullable = fsRoute.ActiveOnFriday;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
          goto label_17;
      }
      nullable = fsRouteContractScheduleRow.WeeklyOnSat;
      if (nullable.GetValueOrDefault())
      {
        nullable = fsRoute.ActiveOnSaturday;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
          goto label_17;
      }
      nullable = fsRouteContractScheduleRow.WeeklyOnSun;
      if (!nullable.GetValueOrDefault())
        return;
      nullable = fsRoute.ActiveOnSunday;
      bool flag1 = false;
      if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue))
        return;
label_17:
      throw new PXException("The specified recurrence dates do not match the dates specified for the route on the Routes (FS203700) form.");
    }
  }

  /// <summary>
  /// Force 'required fields' in Route tab to be filled when a new record is inserted.
  /// </summary>
  public virtual void ForceFilling_RequiredFields_RouteTab(
    PXCache cache,
    FSRouteContractSchedule fsRouteContractScheduleRow)
  {
    if (cache.GetStatus((object) fsRouteContractScheduleRow) != 2 || ((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Current != null)
      return;
    ((PXSelectBase<FSScheduleRoute>) this.ScheduleRoutes).Insert(new FSScheduleRoute());
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRouteContractSchedule, FSSchedule.vehicleTypeID> e)
  {
    if (e.Row == null)
      return;
    this.ResetRouteFields(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRouteContractSchedule, FSRouteContractSchedule.entityID> e)
  {
    if (e.Row == null)
      return;
    FSRouteContractSchedule row = e.Row;
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
    PX.Data.Events.FieldUpdated<FSRouteContractSchedule, FSSchedule.weekCode> e)
  {
    if (e.Row == null)
      return;
    FSRouteContractSchedule row = e.Row;
    this.ValidateWeekCode(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSRouteContractSchedule, FSSchedule.weekCode>>) e).Cache, row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSRouteContractSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSRouteContractSchedule> e)
  {
    if (e.Row == null)
      return;
    FSRouteContractSchedule row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSRouteContractSchedule>>) e).Cache;
    this.ContractSchedule_RowSelected_PartialHandler(cache, (FSSchedule) row);
    bool hasValue = row.EntityID.HasValue;
    ((PXSelectBase) this.ScheduleRoutes).Cache.AllowDelete = hasValue;
    ((PXSelectBase) this.ScheduleRoutes).Cache.AllowUpdate = hasValue;
    ((PXAction) this.openRouteScheduleProcess).SetEnabled(!SharedFunctions.ShowWarningScheduleNotProcessed(cache, (FSSchedule) row));
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSRouteContractSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSRouteContractSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSRouteContractSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSRouteContractSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSRouteContractSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSRouteContractSchedule> e)
  {
    this.FSSchedule_Row_Deleted_PartialHandler(((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<FSRouteContractSchedule>>) e).Cache, ((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<FSRouteContractSchedule>>) e).Args);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSRouteContractSchedule> e)
  {
    if (e.Row == null)
      return;
    FSRouteContractSchedule row = e.Row;
    FSServiceContract current = ((PXSelectBase<FSServiceContract>) this.ContractSelected).Current;
    PXCache cache = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSRouteContractSchedule>>) e).Cache;
    this.ValidateWeekCode(cache, row);
    this.ValidateDays(cache, row);
    this.ForceFilling_RequiredFields_RouteTab(cache, row);
    this.ContractSchedule_RowPersisting_PartialHandler(cache, current, (FSSchedule) row, e.Operation, "Routes Management");
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSRouteContractSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSScheduleRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSScheduleRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSScheduleRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSScheduleRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSScheduleRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSScheduleRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSScheduleRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSScheduleRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSScheduleRoute> e)
  {
    if (e.Row == null)
      return;
    FSScheduleRoute row = e.Row;
    if (e.Operation != 2 || !row.DfltRouteID.HasValue)
      return;
    if (string.IsNullOrEmpty(row.GlobalSequence))
    {
      FSScheduleRoute fsScheduleRoute = PXResultset<FSScheduleRoute>.op_Implicit(PXSelectBase<FSScheduleRoute, PXSelectGroupBy<FSScheduleRoute, Aggregate<Max<FSScheduleRoute.globalSequence>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      row.GlobalSequence = fsScheduleRoute == null || string.IsNullOrEmpty(fsScheduleRoute.GlobalSequence) ? "00010" : (int.Parse(fsScheduleRoute.GlobalSequence) + 10).ToString("00000");
    }
    else
      row.GlobalSequence = int.Parse(row.GlobalSequence).ToString("00000");
    row.SequenceSunday = row.GlobalSequence;
    row.SequenceMonday = row.GlobalSequence;
    row.SequenceTuesday = row.GlobalSequence;
    row.SequenceWednesday = row.GlobalSequence;
    row.SequenceThursday = row.GlobalSequence;
    row.SequenceFriday = row.GlobalSequence;
    row.SequenceSaturday = row.GlobalSequence;
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSScheduleRoute> e)
  {
  }

  public abstract class branchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteServiceContractScheduleEntry.branchID>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteServiceContractScheduleEntry.branchLocationID>
  {
  }
}
