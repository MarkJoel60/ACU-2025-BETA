// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Objects.FS;

public class RouteMaint : PXGraph<RouteMaint, FSRoute>
{
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> BAccount;
  public PXSelect<FSRoute> RouteRecords;
  public PXSelect<FSRoute, Where<FSRoute.routeID, Equal<Current<FSRoute.routeID>>>> RouteSelected;
  public PXSelectJoin<FSRouteEmployee, InnerJoin<PX.Objects.CR.BAccount, On<FSRouteEmployee.employeeID, Equal<PX.Objects.CR.BAccount.bAccountID>>>, Where<FSRouteEmployee.routeID, Equal<Current<FSRoute.routeID>>>> RouteEmployeeRecords;
  public PXFilter<PX.Objects.FS.WeekCodeFilter> WeekCodeFilter;
  public PXSelectReadonly<FSWeekCodeDate> WeekCodeDateRecords;
  [PXViewName("Attributes")]
  public CSAttributeGroupList<FSRoute, FSRouteDocument> Mapping;

  public RouteMaint()
  {
    PXGraph.FieldUpdatingEvents fieldUpdating1 = ((PXGraph) this).FieldUpdating;
    System.Type type1 = typeof (FSRoute);
    string str1 = typeof (FSRoute.beginTimeOnMonday).Name + "_Time";
    RouteMaint routeMaint1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating1 = new PXFieldUpdating((object) routeMaint1, __vmethodptr(routeMaint1, FSRoute_BeginTimeOnMonday__Time_FieldUpdating));
    fieldUpdating1.AddHandler(type1, str1, pxFieldUpdating1);
    PXGraph.FieldUpdatingEvents fieldUpdating2 = ((PXGraph) this).FieldUpdating;
    System.Type type2 = typeof (FSRoute);
    string str2 = typeof (FSRoute.beginTimeOnTuesday).Name + "_Time";
    RouteMaint routeMaint2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating2 = new PXFieldUpdating((object) routeMaint2, __vmethodptr(routeMaint2, FSRoute_BeginTimeOnTuesday__Time_FieldUpdating));
    fieldUpdating2.AddHandler(type2, str2, pxFieldUpdating2);
    PXGraph.FieldUpdatingEvents fieldUpdating3 = ((PXGraph) this).FieldUpdating;
    System.Type type3 = typeof (FSRoute);
    string str3 = typeof (FSRoute.beginTimeOnWednesday).Name + "_Time";
    RouteMaint routeMaint3 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating3 = new PXFieldUpdating((object) routeMaint3, __vmethodptr(routeMaint3, FSRoute_BeginTimeOnWednesday__Time_FieldUpdating));
    fieldUpdating3.AddHandler(type3, str3, pxFieldUpdating3);
    PXGraph.FieldUpdatingEvents fieldUpdating4 = ((PXGraph) this).FieldUpdating;
    System.Type type4 = typeof (FSRoute);
    string str4 = typeof (FSRoute.beginTimeOnThursday).Name + "_Time";
    RouteMaint routeMaint4 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating4 = new PXFieldUpdating((object) routeMaint4, __vmethodptr(routeMaint4, FSRoute_BeginTimeOnThursday__Time_FieldUpdating));
    fieldUpdating4.AddHandler(type4, str4, pxFieldUpdating4);
    PXGraph.FieldUpdatingEvents fieldUpdating5 = ((PXGraph) this).FieldUpdating;
    System.Type type5 = typeof (FSRoute);
    string str5 = typeof (FSRoute.beginTimeOnFriday).Name + "_Time";
    RouteMaint routeMaint5 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating5 = new PXFieldUpdating((object) routeMaint5, __vmethodptr(routeMaint5, FSRoute_BeginTimeOnFriday__Time_FieldUpdating));
    fieldUpdating5.AddHandler(type5, str5, pxFieldUpdating5);
    PXGraph.FieldUpdatingEvents fieldUpdating6 = ((PXGraph) this).FieldUpdating;
    System.Type type6 = typeof (FSRoute);
    string str6 = typeof (FSRoute.beginTimeOnSaturday).Name + "_Time";
    RouteMaint routeMaint6 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating6 = new PXFieldUpdating((object) routeMaint6, __vmethodptr(routeMaint6, FSRoute_BeginTimeOnSaturday__Time_FieldUpdating));
    fieldUpdating6.AddHandler(type6, str6, pxFieldUpdating6);
    PXGraph.FieldUpdatingEvents fieldUpdating7 = ((PXGraph) this).FieldUpdating;
    System.Type type7 = typeof (FSRoute);
    string str7 = typeof (FSRoute.beginTimeOnSunday).Name + "_Time";
    RouteMaint routeMaint7 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating7 = new PXFieldUpdating((object) routeMaint7, __vmethodptr(routeMaint7, FSRoute_BeginTimeOnSunday__Time_FieldUpdating));
    fieldUpdating7.AddHandler(type7, str7, pxFieldUpdating7);
  }

  public virtual IEnumerable weekCodeDateRecords()
  {
    FSRoute current1 = ((PXSelectBase<FSRoute>) this.RouteRecords).Current;
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
      bqlCommand = bqlCommand.WhereOr<Where2<Where<FSWeekCodeDate.weekCodeP1, Like<Required<FSWeekCodeDate.weekCodeP1>>, Or<FSWeekCodeDate.weekCodeP1, Like<Required<FSWeekCodeDate.weekCodeP1>>, Or<FSWeekCodeDate.weekCodeP1, IsNull>>>, And2<Where<FSWeekCodeDate.weekCodeP2, Like<Required<FSWeekCodeDate.weekCodeP2>>, Or<FSWeekCodeDate.weekCodeP2, Like<Required<FSWeekCodeDate.weekCodeP2>>, Or<FSWeekCodeDate.weekCodeP2, IsNull>>>, And2<Where<FSWeekCodeDate.weekCodeP3, Like<Required<FSWeekCodeDate.weekCodeP3>>, Or<FSWeekCodeDate.weekCodeP3, Like<Required<FSWeekCodeDate.weekCodeP3>>, Or<FSWeekCodeDate.weekCodeP3, IsNull>>>, And<Where<FSWeekCodeDate.weekCodeP4, Like<Required<FSWeekCodeDate.weekCodeP4>>, Or<FSWeekCodeDate.weekCodeP4, Like<Required<FSWeekCodeDate.weekCodeP4>>, Or<FSWeekCodeDate.weekCodeP4, IsNull>>>>>>>>();
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
      DateTime? nullable = current2.DateBegin;
      if (nullable.HasValue)
        bqlCommand = bqlCommand.WhereAnd(typeof (Where<FSWeekCodeDate.weekCodeDate, GreaterEqual<Current<FromToFilter.dateBegin>>>));
      nullable = current2.DateEnd;
      if (nullable.HasValue)
        bqlCommand = bqlCommand.WhereAnd(typeof (Where<FSWeekCodeDate.weekCodeDate, LessEqual<Current<FromToFilter.dateEnd>>>));
    }
    bool? nullable1 = current1.ActiveOnSunday;
    if (nullable1.GetValueOrDefault())
      intList.Add(0);
    nullable1 = current1.ActiveOnMonday;
    if (nullable1.GetValueOrDefault())
      intList.Add(1);
    nullable1 = current1.ActiveOnTuesday;
    if (nullable1.GetValueOrDefault())
      intList.Add(2);
    nullable1 = current1.ActiveOnWednesday;
    if (nullable1.GetValueOrDefault())
      intList.Add(3);
    nullable1 = current1.ActiveOnThursday;
    if (nullable1.GetValueOrDefault())
      intList.Add(4);
    nullable1 = current1.ActiveOnFriday;
    if (nullable1.GetValueOrDefault())
      intList.Add(5);
    nullable1 = current1.ActiveOnSaturday;
    if (nullable1.GetValueOrDefault())
      intList.Add(6);
    if (intList != null && intList.Count > 0)
    {
      bqlCommand = bqlCommand.WhereAnd(InHelper<FSWeekCodeDate.dayOfWeek>.Create(intList.Count));
      foreach (int num in intList)
        objectList2.Add((object) num);
    }
    PXView pxView = new PXView((PXGraph) this, true, bqlCommand);
    int startRow = PXView.StartRow;
    int num1 = 0;
    object[] currents = PXView.Currents;
    object[] array = objectList2.ToArray();
    object[] searches = PXView.Searches;
    string[] sortColumns = PXView.SortColumns;
    bool[] descendings = PXView.Descendings;
    PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
    ref int local1 = ref startRow;
    int maximumRows = PXView.MaximumRows;
    ref int local2 = ref num1;
    List<object> objectList3 = pxView.Select(currents, array, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2);
    PXView.StartRow = 0;
    return (IEnumerable) objectList3;
  }

  [PXMergeAttributes]
  [PXSelector(typeof (FSRoute.routeCD))]
  protected virtual void _(PX.Data.Events.CacheAttached<FSRoute.routeCD> e)
  {
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Employee Name", Enabled = false)]
  protected virtual void BAccount_AcctName_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, IsFixed = true)]
  [PXDefault]
  [PXUIField]
  protected virtual void CSAttributeGroup_EntityClassID_CacheAttached(PXCache sender)
  {
  }

  /// <summary>
  /// Enables/Disables the document fields depending on several factors.
  /// </summary>
  public virtual void EnableDisableDocument(PXCache cache, FSRoute fsRouteRow)
  {
    PXUIFieldAttribute.SetEnabled<FSRoute.maxAppointmentQty>(cache, (object) fsRouteRow, !fsRouteRow.NoAppointmentLimit.Value);
    PXUIFieldAttribute.SetEnabled<FSRoute.beginTimeOnMonday>(cache, (object) fsRouteRow, fsRouteRow.ActiveOnMonday.Value);
    PXUIFieldAttribute.SetEnabled<FSRoute.beginTimeOnTuesday>(cache, (object) fsRouteRow, fsRouteRow.ActiveOnTuesday.Value);
    PXUIFieldAttribute.SetEnabled<FSRoute.beginTimeOnWednesday>(cache, (object) fsRouteRow, fsRouteRow.ActiveOnWednesday.Value);
    PXUIFieldAttribute.SetEnabled<FSRoute.beginTimeOnThursday>(cache, (object) fsRouteRow, fsRouteRow.ActiveOnThursday.Value);
    PXUIFieldAttribute.SetEnabled<FSRoute.beginTimeOnFriday>(cache, (object) fsRouteRow, fsRouteRow.ActiveOnFriday.Value);
    PXUIFieldAttribute.SetEnabled<FSRoute.beginTimeOnSaturday>(cache, (object) fsRouteRow, fsRouteRow.ActiveOnSaturday.Value);
    PXUIFieldAttribute.SetEnabled<FSRoute.beginTimeOnSunday>(cache, (object) fsRouteRow, fsRouteRow.ActiveOnSunday.Value);
    PXUIFieldAttribute.SetEnabled<FSRoute.nbrTripOnMonday>(cache, (object) fsRouteRow, fsRouteRow.ActiveOnMonday.Value);
    PXUIFieldAttribute.SetEnabled<FSRoute.nbrTripOnTuesday>(cache, (object) fsRouteRow, fsRouteRow.ActiveOnTuesday.Value);
    PXUIFieldAttribute.SetEnabled<FSRoute.nbrTripOnWednesday>(cache, (object) fsRouteRow, fsRouteRow.ActiveOnWednesday.Value);
    PXUIFieldAttribute.SetEnabled<FSRoute.nbrTripOnThursday>(cache, (object) fsRouteRow, fsRouteRow.ActiveOnThursday.Value);
    PXUIFieldAttribute.SetEnabled<FSRoute.nbrTripOnFriday>(cache, (object) fsRouteRow, fsRouteRow.ActiveOnFriday.Value);
    PXUIFieldAttribute.SetEnabled<FSRoute.nbrTripOnSaturday>(cache, (object) fsRouteRow, fsRouteRow.ActiveOnSaturday.Value);
    PXUIFieldAttribute.SetEnabled<FSRoute.nbrTripOnSunday>(cache, (object) fsRouteRow, fsRouteRow.ActiveOnSunday.Value);
    this.CleanInactiveDayFields(fsRouteRow);
    if (!this.isThereAnyActiveDay(fsRouteRow) && cache.GetStatus((object) fsRouteRow) != 2)
      cache.RaiseExceptionHandling<FSRoute.routeCD>((object) fsRouteRow, (object) fsRouteRow.RouteCD, (Exception) new PXSetPropertyException("Execution dates are not selected for this route. Appointments cannot be generated for the route.", (PXErrorLevel) 2));
    else
      cache.RaiseExceptionHandling<FSRoute.routeCD>((object) fsRouteRow, (object) fsRouteRow.RouteCD, (Exception) null);
  }

  /// <summary>
  /// Checks if any execution day flag is active for a given route.
  /// </summary>
  /// <param name="fsRouteRow">FSRoute row.</param>
  /// <returns>True if at least one flag is on else returns false.</returns>
  public virtual bool isThereAnyActiveDay(FSRoute fsRouteRow)
  {
    return fsRouteRow.ActiveOnMonday.Value || fsRouteRow.ActiveOnTuesday.Value || fsRouteRow.ActiveOnWednesday.Value || fsRouteRow.ActiveOnThursday.Value || fsRouteRow.ActiveOnFriday.Value || fsRouteRow.ActiveOnSaturday.Value || fsRouteRow.ActiveOnSunday.Value;
  }

  /// <summary>
  /// Checks the execution flag for a given day and return the the corresponding PXPersistingCheck to be assigned.
  /// </summary>
  /// <param name="enableForWeekDay">Execution flag for a given day.</param>
  /// <returns>PXPersistingCheck.NullOrBlank if the input is required, PXPersistingCheck.Nothing otherwise.</returns>
  public virtual PXPersistingCheck EnableDisableDayPersistingCheck(bool enableForWeekDay)
  {
    return !enableForWeekDay ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1;
  }

  /// <summary>
  /// Checks if the current RouteShort already exists to ensure is unique.
  /// </summary>
  public virtual bool isRouteShortDuplicated(FSRoute fsRouteRow)
  {
    return PXResultset<FSRoute>.op_Implicit(PXSelectBase<FSRoute, PXSelect<FSRoute, Where<FSRoute.routeID, NotEqual<Required<FSRoute.routeID>>, And<FSRoute.routeShort, Equal<Required<FSRoute.routeShort>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) fsRouteRow.RouteID,
      (object) fsRouteRow.RouteShort
    })) != null;
  }

  /// <summary>
  /// Validates if a Week Code is well formatted. (Creates an exception in the cache parameter).
  /// </summary>
  public virtual void ValidateWeekCode(PXCache cache, FSRoute fsRouteRow)
  {
    List<object> objectList = new List<object>();
    bool flag = false;
    Regex regex = new Regex("^[1-4]?[a-bA-B]?[c-fC-F]?[s-zS-Z]?$");
    if (fsRouteRow.WeekCode == null)
      return;
    foreach (string str in SharedFunctions.SplitWeekcodeByComma(fsRouteRow.WeekCode))
    {
      if (string.IsNullOrEmpty(str))
      {
        cache.RaiseExceptionHandling<FSRoute.weekCode>((object) fsRouteRow, (object) fsRouteRow.WeekCode, (Exception) new PXSetPropertyException("Week codes cannot be empty.", (PXErrorLevel) 4));
        flag = true;
      }
      else if (!SharedFunctions.IsAValidWeekCodeLength(str))
      {
        cache.RaiseExceptionHandling<FSRoute.weekCode>((object) fsRouteRow, (object) fsRouteRow.WeekCode, (Exception) new PXSetPropertyException("The length of each week code must be less than or equal to 4.", (PXErrorLevel) 4));
        flag = true;
      }
      else
      {
        foreach (string splitWeekcodeInChar in SharedFunctions.SplitWeekcodeInChars(str))
        {
          if (!SharedFunctions.IsAValidCharForWeekCode(splitWeekcodeInChar))
          {
            cache.RaiseExceptionHandling<FSRoute.weekCode>((object) fsRouteRow, (object) fsRouteRow.WeekCode, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefix("The {0} character is not allowed to build valid week codes.", new object[1]
            {
              (object) splitWeekcodeInChar
            }), (PXErrorLevel) 4));
            flag = true;
          }
        }
        if (!regex.IsMatch(str) && !flag)
        {
          cache.RaiseExceptionHandling<FSRoute.weekCode>((object) fsRouteRow, (object) fsRouteRow.WeekCode, (Exception) new PXSetPropertyException("At least one week code is not correctly structured. Check field label for examples.", (PXErrorLevel) 4));
          flag = true;
        }
      }
      int num = flag ? 1 : 0;
    }
  }

  /// <summary>
  /// Clean <c>StartTime</c> and <c>Nbr.</c> of Trip(s) per Day fields when Day is set inactive.
  /// </summary>
  public virtual void CleanInactiveDayFields(FSRoute fsRouteRow)
  {
    bool? activeOnMonday = fsRouteRow.ActiveOnMonday;
    bool flag1 = false;
    if (activeOnMonday.GetValueOrDefault() == flag1 & activeOnMonday.HasValue)
    {
      fsRouteRow.BeginTimeOnMonday = new DateTime?();
      fsRouteRow.NbrTripOnMonday = new int?();
    }
    bool? activeOnTuesday = fsRouteRow.ActiveOnTuesday;
    bool flag2 = false;
    if (activeOnTuesday.GetValueOrDefault() == flag2 & activeOnTuesday.HasValue)
    {
      fsRouteRow.BeginTimeOnTuesday = new DateTime?();
      fsRouteRow.NbrTripOnTuesday = new int?();
    }
    bool? activeOnWednesday = fsRouteRow.ActiveOnWednesday;
    bool flag3 = false;
    if (activeOnWednesday.GetValueOrDefault() == flag3 & activeOnWednesday.HasValue)
    {
      fsRouteRow.BeginTimeOnWednesday = new DateTime?();
      fsRouteRow.NbrTripOnWednesday = new int?();
    }
    bool? activeOnThursday = fsRouteRow.ActiveOnThursday;
    bool flag4 = false;
    if (activeOnThursday.GetValueOrDefault() == flag4 & activeOnThursday.HasValue)
    {
      fsRouteRow.BeginTimeOnThursday = new DateTime?();
      fsRouteRow.NbrTripOnThursday = new int?();
    }
    bool? activeOnFriday = fsRouteRow.ActiveOnFriday;
    bool flag5 = false;
    if (activeOnFriday.GetValueOrDefault() == flag5 & activeOnFriday.HasValue)
    {
      fsRouteRow.BeginTimeOnFriday = new DateTime?();
      fsRouteRow.NbrTripOnFriday = new int?();
    }
    bool? activeOnSaturday = fsRouteRow.ActiveOnSaturday;
    bool flag6 = false;
    if (activeOnSaturday.GetValueOrDefault() == flag6 & activeOnSaturday.HasValue)
    {
      fsRouteRow.BeginTimeOnSaturday = new DateTime?();
      fsRouteRow.NbrTripOnSaturday = new int?();
    }
    bool? activeOnSunday = fsRouteRow.ActiveOnSunday;
    bool flag7 = false;
    if (!(activeOnSunday.GetValueOrDefault() == flag7 & activeOnSunday.HasValue))
      return;
    fsRouteRow.BeginTimeOnSunday = new DateTime?();
    fsRouteRow.NbrTripOnSunday = new int?();
  }

  protected virtual void FSRoute_BeginTimeOnMonday__Time_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    ((FSRoute) e.Row).BeginTimeOnMonday = SharedFunctions.TryParseHandlingDateTime(cache, e.NewValue);
  }

  protected virtual void FSRoute_BeginTimeOnTuesday__Time_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    ((FSRoute) e.Row).BeginTimeOnTuesday = SharedFunctions.TryParseHandlingDateTime(cache, e.NewValue);
  }

  protected virtual void FSRoute_BeginTimeOnWednesday__Time_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    ((FSRoute) e.Row).BeginTimeOnWednesday = SharedFunctions.TryParseHandlingDateTime(cache, e.NewValue);
  }

  protected virtual void FSRoute_BeginTimeOnThursday__Time_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    ((FSRoute) e.Row).BeginTimeOnThursday = SharedFunctions.TryParseHandlingDateTime(cache, e.NewValue);
  }

  protected virtual void FSRoute_BeginTimeOnFriday__Time_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    ((FSRoute) e.Row).BeginTimeOnFriday = SharedFunctions.TryParseHandlingDateTime(cache, e.NewValue);
  }

  protected virtual void FSRoute_BeginTimeOnSaturday__Time_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    ((FSRoute) e.Row).BeginTimeOnSaturday = SharedFunctions.TryParseHandlingDateTime(cache, e.NewValue);
  }

  protected virtual void FSRoute_BeginTimeOnSunday__Time_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    ((FSRoute) e.Row).BeginTimeOnSunday = SharedFunctions.TryParseHandlingDateTime(cache, e.NewValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRoute, FSRoute.activeOnMonday> e)
  {
    if (e.Row == null)
      return;
    FSRoute row = e.Row;
    row.NbrTripOnMonday = new int?(row.ActiveOnMonday.Value ? 1 : 0);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRoute, FSRoute.activeOnTuesday> e)
  {
    if (e.Row == null)
      return;
    FSRoute row = e.Row;
    row.NbrTripOnTuesday = new int?(row.ActiveOnTuesday.Value ? 1 : 0);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRoute, FSRoute.activeOnWednesday> e)
  {
    if (e.Row == null)
      return;
    FSRoute row = e.Row;
    row.NbrTripOnWednesday = new int?(row.ActiveOnWednesday.Value ? 1 : 0);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRoute, FSRoute.activeOnThursday> e)
  {
    if (e.Row == null)
      return;
    FSRoute row = e.Row;
    row.NbrTripOnThursday = new int?(row.ActiveOnThursday.Value ? 1 : 0);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRoute, FSRoute.activeOnFriday> e)
  {
    if (e.Row == null)
      return;
    FSRoute row = e.Row;
    row.NbrTripOnFriday = new int?(row.ActiveOnFriday.Value ? 1 : 0);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRoute, FSRoute.activeOnSaturday> e)
  {
    if (e.Row == null)
      return;
    FSRoute row = e.Row;
    row.NbrTripOnSaturday = new int?(row.ActiveOnSaturday.Value ? 1 : 0);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRoute, FSRoute.activeOnSunday> e)
  {
    if (e.Row == null)
      return;
    FSRoute row = e.Row;
    row.NbrTripOnSunday = new int?(row.ActiveOnSunday.Value ? 1 : 0);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRoute, FSRoute.noAppointmentLimit> e)
  {
    if (e.Row == null)
      return;
    FSRoute row = e.Row;
    row.MaxAppointmentQty = new int?(!row.NoAppointmentLimit.GetValueOrDefault() ? 1 : 0);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<FSRoute, FSRoute.weekCode> e)
  {
    if (e.Row == null)
      return;
    FSRoute row = e.Row;
    this.ValidateWeekCode(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSRoute, FSRoute.weekCode>>) e).Cache, row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSRoute> e)
  {
    if (e.Row == null)
      return;
    FSRoute row = e.Row;
    this.EnableDisableDocument(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSRoute>>) e).Cache, row);
    if (!row.NoAppointmentLimit.Value)
      return;
    row.MaxAppointmentQty = new int?(0);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSRoute> e)
  {
    if (e.Row == null)
      return;
    FSRoute row = e.Row;
    bool? appointmentLimit = row.NoAppointmentLimit;
    bool flag = false;
    if (appointmentLimit.GetValueOrDefault() == flag & appointmentLimit.HasValue)
    {
      int? maxAppointmentQty = row.MaxAppointmentQty;
      int num = 1;
      if (maxAppointmentQty.GetValueOrDefault() < num & maxAppointmentQty.HasValue)
        ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSRoute>>) e).Cache.RaiseExceptionHandling<FSRoute.maxAppointmentQty>((object) row, (object) null, (Exception) new PXSetPropertyException("An entry must be greater than zero.", (PXErrorLevel) 4));
    }
    if (this.isRouteShortDuplicated(row))
      ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSRoute>>) e).Cache.RaiseExceptionHandling<FSRoute.routeShort>((object) row, (object) null, (Exception) new PXSetPropertyException("The route short ID cannot be duplicated.", (PXErrorLevel) 4));
    this.ValidateWeekCode(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSRoute>>) e).Cache, row);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSRoute> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRouteEmployee, FSRouteEmployee.priorityPreference> e)
  {
    if (e.Row == null)
      return;
    FSRouteEmployee row = e.Row;
    int? priorityPreference = row.PriorityPreference;
    int num = 1;
    if (!(priorityPreference.GetValueOrDefault() < num & priorityPreference.HasValue))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSRouteEmployee, FSRouteEmployee.priorityPreference>>) e).Cache.RaiseExceptionHandling<FSRouteEmployee.priorityPreference>((object) row, (object) row.PriorityPreference, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefix("The minimum value allowed for this field is {0}", new object[1]
    {
      (object) 1
    }), (PXErrorLevel) 4));
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSRouteEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSRouteEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSRouteEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSRouteEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSRouteEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSRouteEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSRouteEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSRouteEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSRouteEmployee> e)
  {
    if (e.Row == null)
      return;
    FSRouteEmployee row = e.Row;
    int? priorityPreference = row.PriorityPreference;
    int num = 1;
    if (priorityPreference.GetValueOrDefault() < num & priorityPreference.HasValue)
    {
      ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSRouteEmployee>>) e).Cache.RaiseExceptionHandling<FSRouteEmployee.priorityPreference>((object) row, (object) row.PriorityPreference, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefix("The minimum value allowed for this field is {0}", new object[1]
      {
        (object) 1
      }), (PXErrorLevel) 4));
      throw new PXException(PXMessages.LocalizeFormatNoPrefix("The minimum value allowed for {0} is {1}", new object[2]
      {
        (object) "Priority Option",
        (object) 1
      }), new object[1]{ (object) (PXErrorLevel) 4 });
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSRouteEmployee> e)
  {
  }
}
