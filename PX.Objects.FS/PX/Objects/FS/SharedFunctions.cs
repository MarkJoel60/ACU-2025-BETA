// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SharedFunctions
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.FS.DAC;
using PX.Objects.FS.Scheduler;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

#nullable disable
namespace PX.Objects.FS;

public static class SharedFunctions
{
  /// <summary>Retrieves an InventoryItem row by its ID.</summary>
  public static PX.Objects.IN.InventoryItem GetInventoryItemRow(PXGraph graph, int? inventoryID)
  {
    return !inventoryID.HasValue ? (PX.Objects.IN.InventoryItem) null : PX.Objects.IN.InventoryItem.PK.Find(graph, inventoryID);
  }

  /// <summary>
  /// Split a string by commas and returns the result as a list of strings.
  /// </summary>
  public static List<string> SplitWeekcodeByComma(string weekCodes)
  {
    List<string> stringList = new List<string>();
    string str1 = weekCodes;
    char[] chArray = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray))
      stringList.Add(str2.Trim());
    return stringList;
  }

  /// <summary>
  /// Split a string in chars and returns the result as a list of strings.
  /// </summary>
  public static List<string> SplitWeekcodeInChars(string weekcode)
  {
    List<string> stringList = new List<string>();
    for (int startIndex = 0; startIndex < weekcode.Length; ++startIndex)
      stringList.Add(weekcode.Substring(startIndex, 1));
    return stringList;
  }

  /// <summary>Validates if a Week Code is less than or equal to 4.</summary>
  public static bool IsAValidWeekCodeLength(string weekcode) => weekcode.Length <= 4;

  /// <summary>
  /// Validates if a specific Char is valid for a Week Code (1-4), (A-B), (C-F), (S-Z).
  /// </summary>
  public static bool IsAValidCharForWeekCode(string charToCompare)
  {
    return new Regex("^[1-4]?[a-bA-B]?[c-fC-F]?[s-zS-Z]?$").IsMatch(charToCompare);
  }

  /// <summary>
  /// Validates if a Week Code is valid for a schedule time and list of Week Code(s) given.
  /// </summary>
  public static bool WeekCodeIsValid(string sourceWeekcodes, DateTime? scheduleTime, PXGraph graph)
  {
    PXResultset<FSWeekCodeDate> pxResultset = new PXResultset<FSWeekCodeDate>();
    List<object> objectList = new List<object>();
    PXSelectBase<FSWeekCodeDate> pxSelectBase = (PXSelectBase<FSWeekCodeDate>) new PXSelect<FSWeekCodeDate, Where<FSWeekCodeDate.weekCodeDate, Equal<Required<FSWeekCodeDate.weekCodeDate>>>>(graph);
    objectList.Add((object) scheduleTime);
    Regex regex1 = new Regex("^[1-4]$");
    Regex regex2 = new Regex("^[a-bA-B]$");
    Regex regex3 = new Regex("^[c-fC-F]$");
    Regex regex4 = new Regex("^[s-zS-Z]$");
    foreach (string weekcode in SharedFunctions.SplitWeekcodeByComma(sourceWeekcodes))
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
      pxSelectBase.WhereOr<Where2<Where<FSWeekCodeDate.weekCodeP1, Like<Required<FSWeekCodeDate.weekCodeP1>>, Or<FSWeekCodeDate.weekCodeP1, Like<Required<FSWeekCodeDate.weekCodeP1>>, Or<FSWeekCodeDate.weekCodeP1, IsNull>>>, And2<Where<FSWeekCodeDate.weekCodeP2, Like<Required<FSWeekCodeDate.weekCodeP2>>, Or<FSWeekCodeDate.weekCodeP2, Like<Required<FSWeekCodeDate.weekCodeP2>>, Or<FSWeekCodeDate.weekCodeP2, IsNull>>>, And2<Where<FSWeekCodeDate.weekCodeP3, Like<Required<FSWeekCodeDate.weekCodeP3>>, Or<FSWeekCodeDate.weekCodeP3, Like<Required<FSWeekCodeDate.weekCodeP3>>, Or<FSWeekCodeDate.weekCodeP3, IsNull>>>, And<Where<FSWeekCodeDate.weekCodeP4, Like<Required<FSWeekCodeDate.weekCodeP4>>, Or<FSWeekCodeDate.weekCodeP4, Like<Required<FSWeekCodeDate.weekCodeP4>>, Or<FSWeekCodeDate.weekCodeP4, IsNull>>>>>>>>();
      objectList.Add((object) str5);
      objectList.Add((object) str5.ToLower());
      objectList.Add((object) str4);
      objectList.Add((object) str4.ToLower());
      objectList.Add((object) str3);
      objectList.Add((object) str3.ToLower());
      objectList.Add((object) str2);
      objectList.Add((object) str2.ToLower());
    }
    return pxSelectBase.Select(objectList.ToArray()).Count > 0;
  }

  /// <summary>
  /// Returns the day of the week depending on the ID [dayID]. Sunday is (0) and Monday (6).
  /// </summary>
  public static DayOfWeek getDayOfWeekByID(int dayID)
  {
    switch (dayID)
    {
      case 0:
        return DayOfWeek.Sunday;
      case 1:
        return DayOfWeek.Monday;
      case 2:
        return DayOfWeek.Tuesday;
      case 3:
        return DayOfWeek.Wednesday;
      case 4:
        return DayOfWeek.Thursday;
      case 5:
        return DayOfWeek.Friday;
      case 6:
        return DayOfWeek.Saturday;
      default:
        return DayOfWeek.Monday;
    }
  }

  /// <summary>
  /// Returns the month of the year depending on the ID [dayID]. January is (1) and December (12).
  /// </summary>
  public static SharedFunctions.MonthsOfYear getMonthOfYearByID(int monthID)
  {
    switch (monthID)
    {
      case 1:
        return SharedFunctions.MonthsOfYear.January;
      case 2:
        return SharedFunctions.MonthsOfYear.February;
      case 3:
        return SharedFunctions.MonthsOfYear.March;
      case 4:
        return SharedFunctions.MonthsOfYear.April;
      case 5:
        return SharedFunctions.MonthsOfYear.May;
      case 6:
        return SharedFunctions.MonthsOfYear.June;
      case 7:
        return SharedFunctions.MonthsOfYear.July;
      case 8:
        return SharedFunctions.MonthsOfYear.August;
      case 9:
        return SharedFunctions.MonthsOfYear.September;
      case 10:
        return SharedFunctions.MonthsOfYear.October;
      case 11:
        return SharedFunctions.MonthsOfYear.November;
      default:
        return SharedFunctions.MonthsOfYear.December;
    }
  }

  /// <summary>
  /// Returns the month in string of the year depending on the ID [dayID]. January is (JAN) and December (DEC).
  /// </summary>
  public static string GetMonthOfYearInStringByID(int monthID)
  {
    switch (monthID)
    {
      case 1:
        return "JAN";
      case 2:
        return "FEB";
      case 3:
        return "MAR";
      case 4:
        return "APR";
      case 5:
        return "MAY";
      case 6:
        return "JUN";
      case 7:
        return "JUL";
      case 8:
        return "AUG";
      case 9:
        return "SEP";
      case 10:
        return "OCT";
      case 11:
        return "NOV";
      default:
        return "DEC";
    }
  }

  /// <summary>
  /// Calculates the beginning of the week for the specific <c>date</c> using the <c>startOfWeek</c> as reference.
  /// </summary>
  public static DateTime StartOfWeek(DateTime date, DayOfWeek startOfWeek)
  {
    int num = date.DayOfWeek - startOfWeek;
    if (num < 0)
      num += 7;
    return date.AddDays((double) (-1 * num)).Date;
  }

  /// <summary>
  /// Calculates the end of the week for the specific <c>date</c> using the <c>startOfWeek</c> as reference.
  /// </summary>
  public static DateTime EndOfWeek(DateTime date, DayOfWeek startOfWeek)
  {
    return SharedFunctions.StartOfWeek(date, startOfWeek).AddDays(6.0);
  }

  /// <summary>
  /// Verifies that the EndDate is greater than the StartDate.
  /// </summary>
  public static bool IsValidDateRange(DateTime? startDate, DateTime? endDate)
  {
    if (!startDate.HasValue || !endDate.HasValue)
      return false;
    DateTime? nullable1 = startDate;
    DateTime? nullable2 = endDate;
    return (nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) == 0;
  }

  /// <summary>
  /// Validates if the appointment scheduled day of the week belongs to the defined executions days of the given route.
  /// Also if it is valid sets the begin time of the route for the given week day.
  /// </summary>
  /// <param name="fsRouteRow">FSRoute row.</param>
  /// <param name="appointmentScheduledDayOfWeek">Monday Sunday.</param>
  /// <param name="beginTimeOnWeekDay">Begin time of the route in a specific day of week.</param>
  /// <returns>true if the route runs in the given week day, otherwise returns false.</returns>
  public static bool EvaluateExecutionDay(
    FSRoute fsRouteRow,
    DayOfWeek appointmentScheduledDayOfWeek,
    ref DateTime? beginTimeOnWeekDay)
  {
    if (fsRouteRow == null)
      return false;
    switch (appointmentScheduledDayOfWeek)
    {
      case DayOfWeek.Sunday:
        if (fsRouteRow.ActiveOnSunday.Value)
        {
          beginTimeOnWeekDay = fsRouteRow.BeginTimeOnSunday;
          return true;
        }
        break;
      case DayOfWeek.Monday:
        if (fsRouteRow.ActiveOnMonday.Value)
        {
          beginTimeOnWeekDay = fsRouteRow.BeginTimeOnMonday;
          return true;
        }
        break;
      case DayOfWeek.Tuesday:
        if (fsRouteRow.ActiveOnTuesday.Value)
        {
          beginTimeOnWeekDay = fsRouteRow.BeginTimeOnTuesday;
          return true;
        }
        break;
      case DayOfWeek.Wednesday:
        if (fsRouteRow.ActiveOnWednesday.Value)
        {
          beginTimeOnWeekDay = fsRouteRow.BeginTimeOnWednesday;
          return true;
        }
        break;
      case DayOfWeek.Thursday:
        if (fsRouteRow.ActiveOnThursday.Value)
        {
          beginTimeOnWeekDay = fsRouteRow.BeginTimeOnThursday;
          return true;
        }
        break;
      case DayOfWeek.Friday:
        if (fsRouteRow.ActiveOnFriday.Value)
        {
          beginTimeOnWeekDay = fsRouteRow.BeginTimeOnFriday;
          return true;
        }
        break;
      case DayOfWeek.Saturday:
        if (fsRouteRow.ActiveOnSaturday.Value)
        {
          beginTimeOnWeekDay = fsRouteRow.BeginTimeOnSaturday;
          return true;
        }
        break;
    }
    return false;
  }

  /// <summary>
  /// Throw the Exception depending on the result of the EvaluateExecutionDay function.
  /// </summary>
  /// <param name="fsRouteRow">FSRoute row.</param>
  /// <param name="appointmentScheduledDayOfWeek">Monday Sunday.</param>
  /// <param name="beginTimeOnWeekDay">Begin time of the route in a specific day of week.</param>
  public static void ValidateExecutionDay(
    FSRoute fsRouteRow,
    DayOfWeek appointmentScheduledDayOfWeek,
    ref DateTime? beginTimeOnWeekDay)
  {
    if (!SharedFunctions.EvaluateExecutionDay(fsRouteRow, appointmentScheduledDayOfWeek, ref beginTimeOnWeekDay))
      throw new PXException("The appointment cannot be created. The scheduled day of the week for the appointment does not correspond to the execution days defined for the route: {0}. Review the recurrence of this schedule or modify the execution days of the {0} route on the Routes (FS203700) form.", new object[1]
      {
        (object) fsRouteRow.RouteCD
      });
  }

  /// <summary>
  /// Sets the given ScreenID to a format separated by dots
  /// SetScreenIDToDotFormat("SD300200") will return  "SD.300.200".
  /// </summary>
  /// <param name="screenID">8 characters ScreenID.</param>
  /// <returns>The given ScreenID in a dot separated format.</returns>
  public static string SetScreenIDToDotFormat(string screenID)
  {
    if (screenID.Length < 8 || screenID.Length > 8)
      throw new PXException("The provided screen ID does not follow the standard format.");
    return $"{screenID.Substring(0, 2)}.{screenID.Substring(2, 2)}.{screenID.Substring(4, 2)}.{screenID.Substring(6)}";
  }

  /// <summary>
  /// Get an appointment complete address from its service order.
  /// </summary>
  /// <returns>Returns a string containing the complete address of the appointment.</returns>
  public static string GetAppointmentAddress(FSAddress fsAddressRow)
  {
    return fsAddressRow == null ? string.Empty : SharedFunctions.GetAddressForGeolocation(fsAddressRow.PostalCode, fsAddressRow.AddressLine1, fsAddressRow.AddressLine2, fsAddressRow.City, fsAddressRow.State, fsAddressRow.CountryID);
  }

  /// <summary>Get a complete address from a branch location.</summary>
  /// <returns>Returns a string containing the complete address of the branch location.</returns>
  public static string GetBranchLocationAddress(PXGraph graph, FSBranchLocation fsBranchLocationRow)
  {
    if (fsBranchLocationRow == null)
      return string.Empty;
    FSAddress fsAddress = FSAddress.PK.Find(graph, fsBranchLocationRow.BranchLocationAddressID);
    return SharedFunctions.GetAddressForGeolocation(fsAddress.PostalCode, fsAddress.AddressLine1, fsAddress.AddressLine2, fsAddress.City, fsAddress.State, fsAddress.CountryID);
  }

  public static string GetAddressForGeolocation(
    string postalCode,
    string addressLine1,
    string addressLine2,
    string city,
    string state,
    string countryID)
  {
    string addressForGeolocation = string.Empty;
    bool flag = true;
    if (!string.IsNullOrEmpty(addressLine1))
    {
      addressForGeolocation = flag ? addressLine1.Trim() : $"{addressForGeolocation}, {addressLine1.Trim()}";
      flag = false;
    }
    if (!string.IsNullOrEmpty(addressLine2))
    {
      addressForGeolocation = flag ? addressLine2.Trim() : $"{addressForGeolocation}, {addressLine2.Trim()}";
      flag = false;
    }
    if (!string.IsNullOrEmpty(city))
    {
      addressForGeolocation = flag ? city.Trim() : $"{addressForGeolocation}, {city.Trim()}";
      flag = false;
    }
    if (!string.IsNullOrEmpty(state))
    {
      addressForGeolocation = flag ? state.Trim() : $"{addressForGeolocation}, {state.Trim()}";
      flag = false;
    }
    if (!string.IsNullOrEmpty(postalCode))
    {
      addressForGeolocation = flag ? postalCode.Trim() : $"{addressForGeolocation}, {postalCode.Trim()}";
      flag = false;
    }
    if (!string.IsNullOrEmpty(countryID))
      addressForGeolocation = flag ? countryID.Trim() : $"{addressForGeolocation}, {countryID.Trim()}";
    return addressForGeolocation;
  }

  /// <summary>Extracts time info from 'date' field.</summary>
  /// <param name="date">DateTime field from where the time info is extracted.</param>
  /// <returns>A string with the following format: HH:MM AM/PM.</returns>
  public static string GetTimeStringFromDate(DateTime? date)
  {
    return !date.HasValue ? "No scheduled time is available." : date.Value.ToString("hh:mm tt");
  }

  /// <summary>Get the BAccountType based on the staffMemberID.</summary>
  public static string GetBAccountType(PXGraph graph, int? staffMemberID)
  {
    PX.Objects.CR.BAccount baccount = staffMemberID.HasValue ? PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) staffMemberID
    })) : throw new PXException("The staff member selected has an invalid reference. Please contact support service for assistance.");
    if (baccount == null)
      throw new PXException("The staff member selected has an invalid reference. Please contact support service for assistance.");
    if (baccount.Type == "VC" || baccount.Type == "VE")
      return "VE";
    if (baccount.Type == "EC" || baccount.Type == "EP")
      return "EP";
    throw new PXException("The business account is not of the Employee or Vendor type.");
  }

  /// <summary>
  /// Gets a SharedClasses.ItemList type list of recordID's of a field (FieldSearch) belonging to a list of items (FieldList)
  /// from a table (Table) with a Join condition (Join) and a where clause (TWhere).
  /// </summary>
  /// <typeparam name="Table">Main table for the BQL.</typeparam>
  /// <typeparam name="Join">Join for the BQL.</typeparam>
  /// <typeparam name="FieldList">Search field for the select in BQL.</typeparam>
  /// <typeparam name="FieldSearch">Row filter field.</typeparam>
  /// <typeparam name="TWhere">Where BQL conditions.</typeparam>
  public static List<SharedClasses.ItemList> GetItemWithList<Table, Join, FieldList, FieldSearch, TWhere>(
    PXGraph graph,
    List<int?> fieldList,
    params object[] paramObjects)
    where Table : class, IBqlTable, new()
    where Join : IBqlJoin, new()
    where FieldList : IBqlField
    where FieldSearch : IBqlField
    where TWhere : IBqlWhere, new()
  {
    if (fieldList.Count == 0)
      return new List<SharedClasses.ItemList>();
    List<object> list = fieldList.Cast<object>().ToList<object>();
    if (((IEnumerable<object>) paramObjects).Count<object>() > 0)
      list = ((IEnumerable<object>) paramObjects).Concat<object>((IEnumerable<object>) list).ToList<object>();
    BqlCommand fsTableBql = ((BqlCommand) new Select2<Table, Join>()).WhereAnd<TWhere>().WhereAnd(InHelper<FieldList>.Create(list.Count));
    return SharedFunctions.PopulateItemList<Table, FieldList, FieldSearch>(graph, fsTableBql, list);
  }

  /// <summary>
  /// Gets a SharedClasses.ItemList type list of recordID's of a field (FieldSearch) belonging to a list of items (FieldList)
  /// from a table (Table) with a where clause (TWhere).
  /// </summary>
  /// <typeparam name="Table">Main table for the BQL.</typeparam>
  /// <typeparam name="FieldList">Search field for the select in BQL.</typeparam>
  /// <typeparam name="FieldSearch">Row filter field.</typeparam>
  /// <typeparam name="TWhere">Where BQL conditions.</typeparam>
  public static List<SharedClasses.ItemList> GetItemWithList<Table, FieldList, FieldSearch, TWhere>(
    PXGraph graph,
    List<int?> fieldList,
    params object[] paramObjects)
    where Table : class, IBqlTable, new()
    where FieldList : IBqlField
    where FieldSearch : IBqlField
    where TWhere : IBqlWhere, new()
  {
    if (fieldList.Count == 0)
      return new List<SharedClasses.ItemList>();
    List<object> list = fieldList.Cast<object>().ToList<object>();
    if (((IEnumerable<object>) paramObjects).Count<object>() > 0)
      list = ((IEnumerable<object>) paramObjects).Concat<object>((IEnumerable<object>) list).ToList<object>();
    BqlCommand fsTableBql = ((BqlCommand) new Select<Table>()).WhereAnd<TWhere>().WhereAnd(InHelper<FieldList>.Create(list.Count));
    return SharedFunctions.PopulateItemList<Table, FieldList, FieldSearch>(graph, fsTableBql, list);
  }

  /// <summary>
  /// Gets a SharedClasses.ItemList type list of recordID's of a field (FieldSearch) belonging to a list of items (FieldList)
  /// from a table (Table).
  /// </summary>
  /// <typeparam name="Table">Main table for the BQL.</typeparam>
  /// <typeparam name="FieldList">Search field for the select in BQL.</typeparam>
  /// <typeparam name="FieldSearch">Row filter field.</typeparam>
  public static List<SharedClasses.ItemList> GetItemWithList<Table, FieldList, FieldSearch>(
    PXGraph graph,
    List<int?> fieldList)
    where Table : class, IBqlTable, new()
    where FieldList : IBqlField
    where FieldSearch : IBqlField
  {
    if (fieldList.Count == 0)
      return new List<SharedClasses.ItemList>();
    List<object> list = fieldList.Cast<object>().ToList<object>();
    BqlCommand fsTableBql = ((BqlCommand) new Select<Table>()).WhereAnd(InHelper<FieldList>.Create(list.Count));
    return SharedFunctions.PopulateItemList<Table, FieldList, FieldSearch>(graph, fsTableBql, list);
  }

  /// <summary>
  /// Populate a SharedClasses.ItemList type list with recordID's of a field (FieldSearch) belonging to a list of items (FieldList)
  /// from a table (Table).
  /// </summary>
  /// <typeparam name="Table">Main table for the BQL.</typeparam>
  /// <typeparam name="FieldList">Search field for the select in BQL.</typeparam>
  /// <typeparam name="FieldSearch">Row filter field.</typeparam>
  private static List<SharedClasses.ItemList> PopulateItemList<Table, FieldList, FieldSearch>(
    PXGraph graph,
    BqlCommand fsTableBql,
    List<object> fieldList)
    where Table : class, IBqlTable, new()
    where FieldList : IBqlField
    where FieldSearch : IBqlField
  {
    List<SharedClasses.ItemList> source = new List<SharedClasses.ItemList>();
    List<object> objectList = new PXView(graph, true, fsTableBql).SelectMulti(fieldList.ToArray());
    string name1 = Regex.Replace(typeof (FieldList).Name, "^[a-z]", (MatchEvaluator) (m => m.Value.ToUpper()));
    string name2 = Regex.Replace(typeof (FieldSearch).Name, "^[a-z]", (MatchEvaluator) (m => m.Value.ToUpper()));
    bool flag = ((IEnumerable<System.Type>) fsTableBql.GetTables()).Count<System.Type>() > 1;
    foreach (object obj1 in objectList)
    {
      Table able = !flag ? (Table) obj1 : PXResult<Table>.op_Implicit((PXResult<Table>) obj1);
      object fieldListValue = typeof (Table).GetProperty(name1).GetValue((object) able);
      object obj2 = typeof (Table).GetProperty(name2).GetValue((object) able);
      SharedClasses.ItemList itemList = source.Where<SharedClasses.ItemList>((Func<SharedClasses.ItemList, bool>) (list =>
      {
        int? itemId = list.itemID;
        int? nullable = (int?) fieldListValue;
        return itemId.GetValueOrDefault() == nullable.GetValueOrDefault() & itemId.HasValue == nullable.HasValue;
      })).FirstOrDefault<SharedClasses.ItemList>();
      if (itemList != null)
        itemList.list.Add(obj2);
      else
        source.Add(new SharedClasses.ItemList((int?) fieldListValue)
        {
          list = {
            obj2
          }
        });
    }
    return source;
  }

  /// <summary>
  /// Checks if the given Business Account identifier is a prospect type.
  /// </summary>
  /// <param name="graph">Context graph.</param>
  /// <param name="bAccountID">Business Account identifier.</param>
  /// <returns>True is the Business Account is a Prospect.</returns>
  public static bool isThisAProspect(PXGraph graph, int? bAccountID)
  {
    return PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>, And<PX.Objects.CR.BAccount.type, Equal<BAccountType.prospectType>>>>.Config>.Select(graph, new object[1]
    {
      (object) bAccountID
    })) != null;
  }

  /// <summary>
  /// Validates the Actual Start/End date times for a given Route Document.
  /// </summary>
  /// <param name="cache">Route Document Cache.</param>
  /// <param name="fsRouteDocumentRow">Route Document row.</param>
  /// <param name="businessDate">Current graph business date.</param>
  public static void CheckRouteActualDateTimes(
    PXCache cache,
    FSRouteDocument fsRouteDocumentRow,
    DateTime? businessDate)
  {
    if (!fsRouteDocumentRow.ActualStartTime.HasValue || !fsRouteDocumentRow.ActualEndTime.HasValue)
      return;
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    DateTime? nullable = fsRouteDocumentRow.ActualStartTime;
    DateTime dateTime1 = nullable.Value;
    nullable = fsRouteDocumentRow.ActualEndTime;
    DateTime dateTime2 = nullable.Value;
    if (dateTime1 > dateTime2)
      propertyException = new PXSetPropertyException("The times are invalid. The end time cannot be earlier than the start time.", (PXErrorLevel) 5);
    cache.RaiseExceptionHandling<FSRouteDocument.actualStartTime>((object) fsRouteDocumentRow, (object) dateTime1, (Exception) propertyException);
    cache.RaiseExceptionHandling<FSRouteDocument.actualEndTime>((object) fsRouteDocumentRow, (object) dateTime2, (Exception) propertyException);
  }

  /// <summary>
  /// Tries to parse the <c>newValue</c> to DateTime?. When the <c>newValue</c> is string and the DateTime TryParse is not possible returns null. Otherwise returns (DateTime?) <c>newValue</c>.
  /// </summary>
  public static DateTime? TryParseHandlingDateTime(PXCache cache, object newValue)
  {
    if (newValue == null)
      return new DateTime?();
    if (!(newValue is string))
      return (DateTime?) newValue;
    DateTime result;
    return DateTime.TryParse((string) newValue, (IFormatProvider) cache.Graph.Culture, DateTimeStyles.None, out result) ? new DateTime?(result) : new DateTime?();
  }

  /// <summary>Create an Equipment from a sold Inventory Item.</summary>
  /// <param name="graphSMEquipmentMaint"> Equipment graph.</param>
  /// <param name="soldInventoryItemRow">Sold Inventory Item data.</param>
  public static FSEquipment CreateSoldEquipment(
    SMEquipmentMaint graphSMEquipmentMaint,
    SoldInventoryItem soldInventoryItemRow,
    PX.Objects.AR.ARTran arTranRow,
    FSxARTran fsxARTranRow,
    PX.Objects.SO.SOLine soLineRow,
    string action,
    PX.Objects.IN.InventoryItem inventoryItemRow)
  {
    FSEquipment fsEquipment1 = new FSEquipment();
    fsEquipment1.OwnerType = "TP";
    fsEquipment1.RequireMaintenance = new bool?(true);
    fsEquipment1.SiteID = soldInventoryItemRow.SiteID;
    if (inventoryItemRow != null)
    {
      FSxEquipmentModel extension = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxEquipmentModel>(inventoryItemRow);
      fsEquipment1.EquipmentTypeID = (int?) extension?.EquipmentTypeID;
    }
    fsEquipment1.SalesOrderType = soldInventoryItemRow.SOOrderType;
    fsEquipment1.SalesOrderNbr = soldInventoryItemRow.SOOrderNbr;
    fsEquipment1.OwnerID = soldInventoryItemRow.CustomerID;
    fsEquipment1.CustomerID = soldInventoryItemRow.CustomerID;
    fsEquipment1.CustomerLocationID = soldInventoryItemRow.CustomerLocationID;
    fsEquipment1.INSerialNumber = soldInventoryItemRow.LotSerialNumber;
    fsEquipment1.SerialNumber = soldInventoryItemRow.LotSerialNumber;
    fsEquipment1.SourceType = "ARI";
    fsEquipment1.SourceDocType = soldInventoryItemRow.DocType;
    fsEquipment1.SourceRefNbr = soldInventoryItemRow.InvoiceRefNbr;
    fsEquipment1.ARTranLineNbr = soldInventoryItemRow.InvoiceLineNbr;
    if (fsxARTranRow != null)
    {
      fsEquipment1.InstSrvOrdType = fsxARTranRow.SrvOrdType;
      fsEquipment1.InstServiceOrderRefNbr = fsxARTranRow.ServiceOrderRefNbr;
      fsEquipment1.InstAppointmentRefNbr = fsxARTranRow.AppointmentRefNbr;
    }
    if (action == "RT" && fsxARTranRow != null)
      fsEquipment1.EquipmentReplacedID = fsxARTranRow.SMEquipmentID;
    FSEquipment fsEquipment2 = ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Insert(fsEquipment1);
    ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).SetValueExt<FSEquipment.inventoryID>(fsEquipment2, (object) soldInventoryItemRow.InventoryID);
    ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).SetValueExt<FSEquipment.dateInstalled>(fsEquipment2, (object) soldInventoryItemRow.DocDate);
    ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).SetValueExt<FSEquipment.salesDate>(fsEquipment2, (object) (soldInventoryItemRow.SOOrderDate.HasValue ? soldInventoryItemRow.SOOrderDate : soldInventoryItemRow.DocDate));
    ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).SetValueExt<FSEquipment.descr>(fsEquipment2, soldInventoryItemRow.Descr == null ? (object) soldInventoryItemRow.InventoryCD : (object) soldInventoryItemRow.Descr);
    if (soldInventoryItemRow.Descr != null)
    {
      object obj = PXSelectorAttribute.Select<FSEquipment.inventoryID>(((PXSelectBase) graphSMEquipmentMaint.EquipmentRecords).Cache, (object) ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Current);
      PXDBLocalizableStringAttribute.CopyTranslations<PX.Objects.IN.InventoryItem.descr, FSEquipment.descr>((PXGraph) graphSMEquipmentMaint, obj, (object) fsEquipment2);
    }
    graphSMEquipmentMaint.Answers.CopyAllAttributes((object) ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Current, (object) inventoryItemRow);
    PXNoteAttribute.CopyNoteAndFiles(((PXGraph) graphSMEquipmentMaint).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItemRow, ((PXGraph) graphSMEquipmentMaint).Caches[typeof (FSEquipment)], (object) ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Current, new bool?(false), new bool?(true));
    fsEquipment2.ImageUrl = inventoryItemRow.ImageUrl;
    ((PXAction) graphSMEquipmentMaint.Save).Press();
    FSEquipment current = ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Current;
    if (current != null && arTranRow != null && fsxARTranRow != null)
    {
      foreach (PXResult<FSEquipmentComponent> pxResult in ((PXSelectBase<FSEquipmentComponent>) graphSMEquipmentMaint.EquipmentWarranties).Select(Array.Empty<object>()))
      {
        FSEquipmentComponent equipmentComponent = PXResult<FSEquipmentComponent>.op_Implicit(pxResult);
        equipmentComponent.SalesOrderNbr = arTranRow.SOOrderNbr;
        equipmentComponent.SalesOrderType = arTranRow.SOOrderType;
        equipmentComponent.InstSrvOrdType = fsxARTranRow.SrvOrdType;
        equipmentComponent.InstServiceOrderRefNbr = fsxARTranRow.ServiceOrderRefNbr;
        equipmentComponent.InstAppointmentRefNbr = fsxARTranRow.AppointmentRefNbr;
        equipmentComponent.InvoiceRefNbr = soldInventoryItemRow.InvoiceRefNbr;
        ((PXSelectBase<FSEquipmentComponent>) graphSMEquipmentMaint.EquipmentWarranties).Update(equipmentComponent);
        ((PXSelectBase<FSEquipmentComponent>) graphSMEquipmentMaint.EquipmentWarranties).SetValueExt<FSEquipmentComponent.installationDate>(equipmentComponent, (object) soldInventoryItemRow.DocDate);
        ((PXSelectBase<FSEquipmentComponent>) graphSMEquipmentMaint.EquipmentWarranties).SetValueExt<FSEquipmentComponent.salesDate>(equipmentComponent, (object) (soLineRow == null || !soLineRow.OrderDate.HasValue ? arTranRow.TranDate : soLineRow.OrderDate));
      }
      ((PXAction) graphSMEquipmentMaint.Save).Press();
    }
    return current;
  }

  /// <summary>Retrieves an Equipment row by its ID.</summary>
  public static FSEquipment GetEquipmentRow(PXGraph graph, int? smEquipmentID)
  {
    if (!smEquipmentID.HasValue)
      return (FSEquipment) null;
    return PXResultset<FSEquipment>.op_Implicit(PXSelectBase<FSEquipment, PXSelect<FSEquipment, Where<FSEquipment.SMequipmentID, Equal<Required<FSEquipment.SMequipmentID>>>>.Config>.Select(graph, new object[1]
    {
      (object) smEquipmentID
    }));
  }

  /// <summary>
  /// Checks whether there is or not any generation process associated with scheduleID.
  /// </summary>
  /// <returns>True if there is a generation process, otherwise it returns False.</returns>
  public static bool isThereAnyGenerationProcessForThisSchedule(PXCache cache, int? scheduleID)
  {
    bool flag = true;
    int? nullable = scheduleID;
    int num = 0;
    if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      flag = PXSelectBase<FSContractGenerationHistory, PXSelect<FSContractGenerationHistory, Where<FSContractGenerationHistory.scheduleID, Equal<Required<FSContractGenerationHistory.scheduleID>>>>.Config>.SelectWindowed(cache.Graph, 0, 1, new object[1]
      {
        (object) scheduleID
      }).Count != 0;
    return flag;
  }

  /// <summary>
  /// Shows a warning message if the current schedule has not been processed yet.
  /// </summary>
  /// <returns>True if there is a generation process, otherwise it returns False.</returns>
  public static bool ShowWarningScheduleNotProcessed(PXCache cache, FSSchedule fsScheduleRow)
  {
    int num = SharedFunctions.isThereAnyGenerationProcessForThisSchedule(cache, fsScheduleRow.ScheduleID) ? 1 : 0;
    if (num != 0)
      return num != 0;
    cache.RaiseExceptionHandling<FSSchedule.refNbr>((object) fsScheduleRow, (object) fsScheduleRow.RefNbr, (Exception) new PXSetPropertyException("This schedule will not affect the system until a generation process takes place.", (PXErrorLevel) 2));
    return num != 0;
  }

  /// <summary>
  /// Gets the name of the specified field with the default option to capitalize its first letter.
  /// </summary>
  /// <typeparam name="field">Field from where to get the name.</typeparam>
  /// <param name="capitalizedFirstLetter">Flag to indicate if the first letter is capital.</param>
  /// <returns>Returns the field's name.</returns>
  public static string GetFieldName<field>(bool capitalizedFirstLetter = true) where field : IBqlField
  {
    string source = typeof (field).Name;
    if (capitalizedFirstLetter)
      source = source.First<char>().ToString().ToUpper() + source.Substring(1);
    return source;
  }

  /// <summary>
  /// Copy all common fields from a source row to a target row skipping special fields like key fields and Acumatica creation/update fields.
  /// Optionally you can pass a list of field names to exclude of the copy.
  /// </summary>
  /// <param name="cacheTarget">The cache of the target row.</param>
  /// <param name="rowTarget">The target row.</param>
  /// <param name="cacheSource">The cache of the source row.</param>
  /// <param name="rowSource">The source row.</param>
  /// <param name="excludeFields">List of field names to exclude of the copy.</param>
  public static void CopyCommonFields(
    PXCache cacheTarget,
    IBqlTable rowTarget,
    PXCache cacheSource,
    IBqlTable rowSource,
    params string[] excludeFields)
  {
    foreach (System.Type bqlField in cacheTarget.BqlFields)
    {
      string fieldName = bqlField.Name;
      if ((excludeFields == null || !Array.Exists<string>(excludeFields, (Predicate<string>) (element => element.Equals(fieldName, StringComparison.OrdinalIgnoreCase)))) && !fieldName.Contains("_") && ((List<string>) cacheSource.Fields).Exists((Predicate<string>) (element => element.Equals(fieldName, StringComparison.OrdinalIgnoreCase))))
      {
        bool flag = false;
        foreach (PXEventSubscriberAttribute attribute in cacheTarget.GetAttributes(fieldName))
        {
          if (attribute is PXDBIdentityAttribute || attribute is PXDBFieldAttribute && ((PXDBFieldAttribute) attribute).IsKey || attribute is PXDBCreatedByIDAttribute || attribute is PXDBCreatedByScreenIDAttribute || attribute is PXDBCreatedDateTimeAttribute || attribute is PXDBLastModifiedByIDAttribute || attribute is PXDBLastModifiedByScreenIDAttribute || attribute is PXDBLastModifiedDateTimeAttribute || attribute is PXDBTimestampAttribute)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          cacheTarget.SetValue((object) rowTarget, fieldName, cacheSource.GetValue((object) rowSource, fieldName));
      }
    }
  }

  public static int ReplicateCacheExceptions(
    PXCache cache,
    IBqlTable row,
    PXCache cacheWithExceptions,
    IBqlTable rowWithExceptions)
  {
    return SharedFunctions.ReplicateCacheExceptions(cache, row, (PXCache) null, (IBqlTable) null, cacheWithExceptions, rowWithExceptions);
  }

  public static int ReplicateCacheExceptions(
    PXCache cache1,
    IBqlTable row1,
    PXCache cache2,
    IBqlTable row2,
    PXCache cacheWithExceptions,
    IBqlTable rowWithExceptions)
  {
    int num = 0;
    List<string> uiFields = SharedFunctions.GetUIFields(cache1, (object) row1);
    bool flag = false;
    string str = string.Empty;
    foreach (string field1 in (List<string>) cache1.Fields)
    {
      string field = field1;
      PXFieldState pxFieldState;
      try
      {
        pxFieldState = (PXFieldState) cacheWithExceptions.GetStateExt((object) rowWithExceptions, field);
      }
      catch
      {
        pxFieldState = (PXFieldState) null;
      }
      if (pxFieldState != null && pxFieldState.Error != null)
      {
        PXException pxException = (PXException) new PXSetPropertyException(pxFieldState.Error, pxFieldState.ErrorLevel);
        cache1.RaiseExceptionHandling(field, (object) row1, (object) null, (Exception) pxException);
        if (cache2 != null && row2 != null)
          cache2.RaiseExceptionHandling(field, (object) row2, (object) null, (Exception) pxException);
        if (pxFieldState.ErrorLevel != 1 && pxFieldState.ErrorLevel != 3 && pxFieldState.ErrorLevel != 2)
        {
          ++num;
          if (!uiFields.Any<string>((Func<string, bool>) (e => e.Equals(field, StringComparison.OrdinalIgnoreCase))))
          {
            flag = true;
            str = pxFieldState.Error;
          }
        }
      }
    }
    if (flag)
      throw new PXException("An error occurred while updating the service order, with the message: {0}.", new object[1]
      {
        (object) str
      });
    return num;
  }

  public static List<string> GetUIFields(PXCache cache, object data)
  {
    List<string> uiFields = new List<string>();
    foreach (IPXInterfaceField ipxInterfaceField in cache.GetAttributes(data, (string) null).OfType<IPXInterfaceField>())
      uiFields.Add(((PXEventSubscriberAttribute) ipxInterfaceField).FieldName);
    return uiFields;
  }

  /// <summary>Get the web methods file path.</summary>
  public static string GetWebMethodPath(string path)
  {
    if (string.IsNullOrEmpty(path))
      return string.Empty;
    string str1 = "/Pages/";
    string str2 = "FS/";
    int num1 = path.LastIndexOf(str1.ToLower()) != -1 ? path.LastIndexOf(str1.ToLower()) : path.LastIndexOf(str1);
    int startIndex = num1 != -1 ? num1 + str1.Length : 0;
    int num2 = path.IndexOf(str2.ToLower(), startIndex) != -1 ? path.IndexOf(str2.ToLower(), startIndex) : path.IndexOf(str2, startIndex);
    return num2 == -1 ? string.Empty : path.Substring(0, num2 + "FS".Length) + "/FS300000.aspx";
  }

  public static string GetMapApiKey(PXGraph graph)
  {
    FSSetup fsSetup = PXResultset<FSSetup>.op_Implicit(PXSelectBase<FSSetup, PXSelect<FSSetup>.Config>.Select(graph, Array.Empty<object>()));
    return fsSetup == null ? string.Empty : fsSetup.MapApiKey;
  }

  public static bool? IsGPSTrackingEnabled(PXGraph graph)
  {
    FSSetup fsSetup = PXResultset<FSSetup>.op_Implicit(PXSelectBase<FSSetup, PXSelect<FSSetup>.Config>.Select(graph, Array.Empty<object>()));
    return fsSetup == null ? new bool?(false) : fsSetup.EnableGPSTracking;
  }

  public static int? GetGPSRefreshTrackingTime(PXGraph graph)
  {
    FSSetup fsSetup = PXResultset<FSSetup>.op_Implicit(PXSelectBase<FSSetup, PXSelect<FSSetup>.Config>.Select(graph, Array.Empty<object>()));
    return fsSetup == null ? new int?(0) : fsSetup.GPSRefreshTrackingTime;
  }

  public static XmlNamespaceManager GenerateXmlNameSpace(ref XmlNamespaceManager nameSpace)
  {
    nameSpace.AddNamespace($"{"bingSchema"}", $"{"http://schemas.microsoft.com/search/local/ws/rest/v1"}");
    return nameSpace;
  }

  public static string parseSecsDurationToString(int duration)
  {
    TimeSpan timeSpan = TimeSpan.FromSeconds((double) duration);
    string str1 = (string) null;
    string durationToString;
    if (timeSpan.Hours > 0)
    {
      string str2 = $"{str1}{timeSpan.Hours.ToString()} hour";
      string str3 = timeSpan.Hours <= 1 ? str2 + " " : str2 + "s ";
      durationToString = timeSpan.Seconds < 30 ? $"{str3}{timeSpan.Minutes.ToString()} min" : $"{str3}{(timeSpan.Minutes + 1).ToString()} min";
      if (timeSpan.Minutes > 1)
        durationToString += "s";
    }
    else
    {
      durationToString = timeSpan.Seconds < 30 ? $"{str1}{timeSpan.Minutes.ToString()} min" : $"{str1}{(timeSpan.Minutes + 1).ToString()} min";
      if (timeSpan.Minutes > 1)
        durationToString += "s";
    }
    return durationToString;
  }

  public static bool isFSSetupSet(PXGraph graph)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>() && PXResultset<FSSetup>.op_Implicit(PXSelectBase<FSSetup, PXSelect<FSSetup>.Config>.SelectWindowed(graph, 0, 1, Array.Empty<object>())) != null;
  }

  public static PX.Objects.AR.Customer GetCustomerRow(PXGraph graph, int? customerID)
  {
    if (!customerID.HasValue)
      return (PX.Objects.AR.Customer) null;
    return PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) customerID
    }));
  }

  public static TimeSpan GetTimeSpanDiff(DateTime begin, DateTime end)
  {
    begin = new DateTime(begin.Year, begin.Month, begin.Day, begin.Hour, begin.Minute, 0);
    end = new DateTime(end.Year, end.Month, end.Day, end.Hour, end.Minute, 0);
    return end - begin;
  }

  public static DateTime? RemoveTimeInfo(DateTime? date)
  {
    if (date.HasValue)
    {
      int hour = date.Value.Hour;
      date = new DateTime?(date.Value.AddHours((double) -hour));
      int minute = date.Value.Minute;
      date = new DateTime?(date.Value.AddMinutes((double) -minute));
    }
    return date;
  }

  public static string GetCompleteCoordinate(Decimal? longitude, Decimal? latitude)
  {
    if (!longitude.HasValue || !latitude.HasValue)
      return string.Empty;
    return $"[{longitude.ToString()}],[{latitude.ToString()}]";
  }

  public static bool IsNotAllowedBillingOptionsModification(FSBillingCycle fsBillingCycleRow)
  {
    if (fsBillingCycleRow == null)
      return true;
    bool? groupBillByLocations;
    if (fsBillingCycleRow.BillingCycleType == "PO")
    {
      groupBillByLocations = fsBillingCycleRow.GroupBillByLocations;
      bool flag = false;
      if (groupBillByLocations.GetValueOrDefault() == flag & groupBillByLocations.HasValue)
        goto label_8;
    }
    if (fsBillingCycleRow.BillingCycleType == "WO")
    {
      groupBillByLocations = fsBillingCycleRow.GroupBillByLocations;
      bool flag = false;
      if (groupBillByLocations.GetValueOrDefault() == flag & groupBillByLocations.HasValue)
        goto label_8;
    }
    if (!(fsBillingCycleRow.BillingCycleType == "TC"))
      return false;
    groupBillByLocations = fsBillingCycleRow.GroupBillByLocations;
    bool flag1 = false;
    return groupBillByLocations.GetValueOrDefault() == flag1 & groupBillByLocations.HasValue;
label_8:
    return true;
  }

  public static bool AreEquipmentFieldsValid(
    PXCache cache,
    int? inventoryID,
    int? targetEQ,
    object newTargetEQLineNbr,
    string equipmentAction,
    ref string errorMessage)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>())
      return true;
    errorMessage = string.Empty;
    if (inventoryID.HasValue)
    {
      FSxEquipmentModel extension = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxEquipmentModel>(SharedFunctions.GetInventoryItemRow(cache.Graph, inventoryID));
      switch (equipmentAction)
      {
        case "ST":
          if (extension.EquipmentItemClass != "ME")
          {
            errorMessage = "A model equipment identifier must be selected in the Inventory ID column for this action.";
            return false;
          }
          break;
        case "CC":
          if (extension.EquipmentItemClass != "CT")
          {
            errorMessage = "A component identifier has to be selected in the Inventory ID column for this action.";
            return false;
          }
          if (newTargetEQLineNbr == null && !targetEQ.HasValue)
          {
            errorMessage = "You have to select an equipment entity in the Target Equipment ID or Model Equipment Line Nbr. column for this action.";
            return false;
          }
          break;
        case "RC":
          if (extension.EquipmentItemClass != "CT")
          {
            errorMessage = "A component identifier has to be selected in the Inventory ID column for this action.";
            return false;
          }
          if (!targetEQ.HasValue)
          {
            errorMessage = "You have to select an equipment entity in the Target Equipment ID or Model Equipment Line Nbr. column for this action.";
            return false;
          }
          break;
        case "RT":
          if (extension.EquipmentItemClass != "ME")
          {
            errorMessage = "A model equipment identifier must be selected in the Inventory ID column for this action.";
            return false;
          }
          if (newTargetEQLineNbr == null && !targetEQ.HasValue)
          {
            errorMessage = "You have to select an equipment entity in the Target Equipment ID or Model Equipment Line Nbr. column for this action.";
            return false;
          }
          break;
      }
    }
    return true;
  }

  public static void UpdateEquipmentFields(
    PXGraph graph,
    PXCache cache,
    object row,
    int? inventoryID,
    bool updateQty = true)
  {
    SharedFunctions.UpdateEquipmentFields(graph, cache, row, inventoryID, (FSSrvOrdType) null, updateQty);
  }

  public static void UpdateEquipmentFields(
    PXGraph graph,
    PXCache cache,
    object row,
    int? inventoryID,
    FSSrvOrdType fsSrvOrdTypeRow,
    bool updateQty = true)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>())
      return;
    SharedFunctions.SetEquipmentActionFromInventory(graph, row, inventoryID, fsSrvOrdTypeRow);
    SharedFunctions.SetEquipmentFieldEnablePersistingCheck(cache, row, updateQty);
  }

  public static void SetEquipmentActionFromInventory(PXGraph graph, object row, int? inventoryID)
  {
    SharedFunctions.SetEquipmentActionFromInventory(graph, row, inventoryID, (FSSrvOrdType) null);
  }

  public static void SetEquipmentActionFromInventory(
    PXGraph graph,
    object row,
    int? inventoryID,
    FSSrvOrdType fsSrvOrdTypeRow)
  {
    if (!inventoryID.HasValue || !PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>())
      return;
    string str = "NO";
    FSxEquipmentModel extension1 = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxEquipmentModel>(SharedFunctions.GetInventoryItemRow(graph, inventoryID));
    string equipmentItemClass = extension1?.EquipmentItemClass;
    if (row is PX.Objects.SO.SOLine && SharedFunctions.SetScreenIDToDotFormat("SO301000") != graph.Accessinfo.ScreenID || row is FSSODet && SharedFunctions.SetScreenIDToDotFormat("FS300100") != graph.Accessinfo.ScreenID)
      str = "NO";
    else if (extension1 != null && extension1.EQEnabled.GetValueOrDefault())
      str = SharedFunctions.getEquipmentModelAction(graph, extension1, fsSrvOrdTypeRow);
    switch (row)
    {
      case PX.Objects.SO.SOLine _:
        FSxSOLine extension2 = PXCache<PX.Objects.SO.SOLine>.GetExtension<FSxSOLine>(row as PX.Objects.SO.SOLine);
        extension2.EquipmentAction = str;
        extension2.Comment = string.Empty;
        extension2.SMEquipmentID = new int?();
        extension2.NewEquipmentLineNbr = new int?();
        extension2.ComponentID = new int?();
        extension2.EquipmentComponentLineNbr = new int?();
        extension2.EquipmentItemClass = equipmentItemClass;
        break;
      case FSSODet _:
        FSSODet fssoDet = row as FSSODet;
        fssoDet.EquipmentAction = str;
        fssoDet.SMEquipmentID = new int?();
        fssoDet.NewTargetEquipmentLineNbr = (string) null;
        fssoDet.ComponentID = new int?();
        fssoDet.EquipmentLineRef = new int?();
        fssoDet.Comment = (string) null;
        fssoDet.EquipmentItemClass = equipmentItemClass;
        break;
      case FSAppointmentDet _:
        FSAppointmentDet fsAppointmentDet = row as FSAppointmentDet;
        fsAppointmentDet.EquipmentAction = str;
        fsAppointmentDet.SMEquipmentID = new int?();
        fsAppointmentDet.NewTargetEquipmentLineNbr = (string) null;
        fsAppointmentDet.ComponentID = new int?();
        fsAppointmentDet.EquipmentLineRef = new int?();
        fsAppointmentDet.Comment = (string) null;
        fsAppointmentDet.EquipmentItemClass = equipmentItemClass;
        break;
      case FSScheduleDet _:
        FSScheduleDet fsScheduleDet = row as FSScheduleDet;
        fsScheduleDet.EquipmentAction = str != "ST" ? "NO" : str;
        fsScheduleDet.SMEquipmentID = new int?();
        fsScheduleDet.ComponentID = new int?();
        fsScheduleDet.EquipmentItemClass = equipmentItemClass;
        break;
    }
  }

  public static string getEquipmentModelAction(
    PXGraph graph,
    FSxEquipmentModel fsxEquipmentModelRow)
  {
    return SharedFunctions.getEquipmentModelAction(graph, fsxEquipmentModelRow, (FSSrvOrdType) null);
  }

  public static string getEquipmentModelAction(
    PXGraph graph,
    FSxEquipmentModel fsxEquipmentModelRow,
    FSSrvOrdType fsSrvOrdTypeRow)
  {
    if (fsxEquipmentModelRow == null)
      return (string) null;
    if (fsSrvOrdTypeRow != null && fsSrvOrdTypeRow.PostTo == "PM")
      return "NO";
    switch (fsxEquipmentModelRow.EquipmentItemClass)
    {
      case "CT":
        return "CC";
      case "ME":
        return "ST";
      default:
        return "NO";
    }
  }

  public static void SetEquipmentFieldEnablePersistingCheck(
    PXCache cache,
    object row,
    bool updateQty = true)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>())
      return;
    switch (row)
    {
      case PX.Objects.SO.SOLine _:
        PX.Objects.SO.SOLine row1 = row as PX.Objects.SO.SOLine;
        FSxSOLine extension = cache.GetExtension<FSxSOLine>((object) row1);
        SharedFunctions.SetEquipmentFieldEnablePersistingCheck(cache, (object) row1, row1.InventoryID, extension.EquipmentItemClass, (string) null, extension.EquipmentAction, (object) extension.NewEquipmentLineNbr, extension.SMEquipmentID, extension.ComponentID, typeof (FSxSOLine.equipmentAction).Name, typeof (FSxSOLine.sMEquipmentID).Name, typeof (FSxSOLine.newEquipmentLineNbr).Name, typeof (FSxSOLine.componentID).Name, typeof (FSxSOLine.equipmentComponentLineNbr).Name, typeof (FSxSOLine.comment).Name, typeof (PX.Objects.SO.SOLine.orderQty).Name, updateQty);
        break;
      case FSSODet _:
        FSSODet row2 = row as FSSODet;
        SharedFunctions.SetEquipmentFieldEnablePersistingCheck(cache, (object) row2, row2.InventoryID, row2.EquipmentItemClass, row2.LineType, row2.EquipmentAction, (object) row2.NewTargetEquipmentLineNbr, row2.SMEquipmentID, row2.ComponentID, typeof (FSSODet.equipmentAction).Name, typeof (FSSODet.SMequipmentID).Name, typeof (FSSODet.newTargetEquipmentLineNbr).Name, typeof (FSSODet.componentID).Name, typeof (FSSODet.equipmentLineRef).Name, typeof (FSSODet.comment).Name, typeof (FSSODet.estimatedQty).Name, updateQty);
        break;
      case FSAppointmentDet _:
        FSAppointmentDet row3 = row as FSAppointmentDet;
        SharedFunctions.SetEquipmentFieldEnablePersistingCheck(cache, (object) row3, row3.InventoryID, row3.EquipmentItemClass, row3.LineType, row3.EquipmentAction, (object) row3.NewTargetEquipmentLineNbr, row3.SMEquipmentID, row3.ComponentID, typeof (FSAppointmentDet.equipmentAction).Name, typeof (FSAppointmentDet.SMequipmentID).Name, typeof (FSAppointmentDet.newTargetEquipmentLineNbr).Name, typeof (FSAppointmentDet.componentID).Name, typeof (FSAppointmentDet.equipmentLineRef).Name, typeof (FSAppointmentDet.comment).Name, typeof (FSAppointmentDet.actualQty).Name, updateQty);
        break;
      case FSScheduleDet _:
        FSScheduleDet row4 = row as FSScheduleDet;
        SharedFunctions.SetEquipmentFieldEnablePersistingCheck(cache, (object) row4, row4.InventoryID, row4.EquipmentItemClass, row4.LineType, row4.EquipmentAction, (object) null, row4.SMEquipmentID, row4.ComponentID, typeof (FSScheduleDet.equipmentAction).Name, typeof (FSScheduleDet.SMequipmentID).Name, (string) null, typeof (FSScheduleDet.componentID).Name, typeof (FSScheduleDet.equipmentLineRef).Name, (string) null, typeof (FSScheduleDet.qty).Name, updateQty);
        break;
    }
  }

  public static void ResetEquipmentFields(PXCache cache, object row)
  {
    switch (row)
    {
      case PX.Objects.SO.SOLine _:
        PX.Objects.SO.SOLine soLine = row as PX.Objects.SO.SOLine;
        FSxSOLine extension = cache.GetExtension<FSxSOLine>((object) soLine);
        extension.SMEquipmentID = new int?();
        extension.NewEquipmentLineNbr = new int?();
        extension.ComponentID = new int?();
        extension.EquipmentComponentLineNbr = new int?();
        break;
      case FSSODet _:
        FSSODet fssoDet = row as FSSODet;
        fssoDet.SMEquipmentID = new int?();
        fssoDet.NewTargetEquipmentLineNbr = (string) null;
        fssoDet.ComponentID = new int?();
        fssoDet.EquipmentLineRef = new int?();
        break;
      case FSAppointmentDet _:
        FSAppointmentDet fsAppointmentDet = row as FSAppointmentDet;
        fsAppointmentDet.SMEquipmentID = new int?();
        fsAppointmentDet.NewTargetEquipmentLineNbr = (string) null;
        fsAppointmentDet.ComponentID = new int?();
        fsAppointmentDet.EquipmentLineRef = new int?();
        break;
      case FSScheduleDet _:
        FSScheduleDet fsScheduleDet = row as FSScheduleDet;
        fsScheduleDet.SMEquipmentID = new int?();
        fsScheduleDet.ComponentID = new int?();
        fsScheduleDet.EquipmentLineRef = new int?();
        break;
    }
  }

  public static void SetEquipmentFieldEnablePersistingCheck(
    PXCache cache,
    object row,
    int? inventoryID,
    string EquipmentItemClass,
    string lineType,
    string eQAction,
    object newTargetEQLineNbr,
    int? sMEquipmentID,
    int? componentID,
    string eQActionFieldName,
    string sMEquipmentIDFieldName,
    string newTargetEQFieldName,
    string componentIDFieldName,
    string equipmentLineRefFieldName,
    string commentFieldName,
    string quantityFieldName,
    bool updateQty = true)
  {
    if (inventoryID.HasValue || lineType == "SERVI")
    {
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = sMEquipmentID.HasValue;
      bool flag5 = true;
      bool flag6 = false;
      bool flag7 = false;
      bool flag8 = false;
      if (EquipmentItemClass != null)
      {
        switch (eQAction)
        {
          case "ST":
            if (EquipmentItemClass != "ME")
            {
              cache.RaiseExceptionHandling(eQActionFieldName, row, (object) eQAction, (Exception) new PXSetPropertyException("A model equipment identifier must be selected in the Inventory ID column for this action."));
              break;
            }
            break;
          case "RT":
            if (EquipmentItemClass != "ME")
            {
              cache.RaiseExceptionHandling(eQActionFieldName, row, (object) eQAction, (Exception) new PXSetPropertyException("A model equipment identifier must be selected in the Inventory ID column for this action."));
              break;
            }
            flag2 = true;
            int num1;
            flag5 = (num1 = 0) != 0;
            flag4 = num1 != 0;
            flag3 = num1 != 0;
            break;
          default:
            if (!(eQAction == "CC") && !(eQAction == "UC") && !(eQAction == "RC"))
            {
              if (eQAction == "NO")
              {
                if (EquipmentItemClass == "CE" || EquipmentItemClass == "OI")
                {
                  flag2 = flag3 = true;
                  flag1 = newTargetEQLineNbr != null || sMEquipmentID.HasValue;
                }
                flag6 = false;
                break;
              }
              break;
            }
            if (EquipmentItemClass != "CT")
            {
              cache.RaiseExceptionHandling(eQActionFieldName, row, (object) eQAction, (Exception) new PXSetPropertyException("A component identifier has to be selected in the Inventory ID column for this action."));
              break;
            }
            int num2;
            flag8 = (num2 = 1) != 0;
            flag6 = num2 != 0;
            flag3 = num2 != 0;
            flag2 = num2 != 0;
            flag1 = num2 != 0;
            flag5 = false;
            switch (eQAction)
            {
              case "UC":
                flag2 = false;
                break;
              case "RC":
                flag3 = false;
                flag4 = true;
                flag7 = true;
                break;
            }
            break;
        }
      }
      if (eQAction == "NO" && (lineType == "SERVI" || lineType == "NSTKI" || lineType == "SLPRO"))
      {
        int num3;
        flag3 = (num3 = 1) != 0;
        flag2 = num3 != 0;
        flag1 = num3 != 0;
        flag7 = flag4 && componentID.HasValue;
      }
      PXUIFieldAttribute.SetEnabled(cache, row, sMEquipmentIDFieldName, flag2 && newTargetEQLineNbr == null);
      PXUIFieldAttribute.SetEnabled(cache, row, componentIDFieldName, flag1);
      if (row is FSSODet && cache.Graph.Accessinfo.ScreenID != SharedFunctions.SetScreenIDToDotFormat("FS300100") || row is FSAppointmentDet && cache.Graph.Accessinfo.ScreenID != SharedFunctions.SetScreenIDToDotFormat("FS300200"))
        PXDefaultAttribute.SetPersistingCheck(cache, componentIDFieldName, row, (PXPersistingCheck) 2);
      else
        PXDefaultAttribute.SetPersistingCheck(cache, componentIDFieldName, row, flag6 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
      if (newTargetEQFieldName != null)
        PXUIFieldAttribute.SetEnabled(cache, row, newTargetEQFieldName, flag3 && !sMEquipmentID.HasValue);
      if (equipmentLineRefFieldName != null)
      {
        PXUIFieldAttribute.SetEnabled(cache, row, equipmentLineRefFieldName, flag4);
        if (row is FSSODet && cache.Graph.Accessinfo.ScreenID != SharedFunctions.SetScreenIDToDotFormat("FS300100") || row is FSAppointmentDet && cache.Graph.Accessinfo.ScreenID != SharedFunctions.SetScreenIDToDotFormat("FS300200"))
          PXDefaultAttribute.SetPersistingCheck(cache, equipmentLineRefFieldName, row, (PXPersistingCheck) 2);
        else
          PXDefaultAttribute.SetPersistingCheck(cache, equipmentLineRefFieldName, row, flag7 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
      }
      if (commentFieldName != null)
        PXUIFieldAttribute.SetEnabled(cache, row, commentFieldName, flag8 && componentID.HasValue);
      if (lineType != "SERVI" && !flag5 && !cache.Graph.IsCopyPasteContext)
      {
        if (updateQty)
        {
          Decimal? nullable = (Decimal?) cache.GetValue(row, quantityFieldName);
          Decimal num4 = 1.0M;
          if (!(nullable.GetValueOrDefault() == num4 & nullable.HasValue))
            cache.SetValueExt(row, quantityFieldName, (object) 1.0M);
        }
        PXUIFieldAttribute.SetEnabled(cache, row, quantityFieldName, flag5);
      }
    }
    if (newTargetEQFieldName == null || !(lineType == "CM_LN") && !(lineType == "IT_LN"))
      return;
    PXUIFieldAttribute.SetEnabled(cache, row, newTargetEQFieldName, !sMEquipmentID.HasValue);
  }

  /// <summary>
  /// Creates note record in Note table in the RowInserted event.
  /// </summary>
  public static void InitializeNote(PXCache cache, PXRowInsertedEventArgs e)
  {
    if (string.IsNullOrEmpty(cache.Graph.PrimaryView))
      return;
    PXCache cach = cache.Graph.Caches[typeof (Note)];
    bool isDirty = cach.IsDirty;
    PXNoteAttribute.GetNoteID(cache, e.Row, EntityHelper.GetNoteField(cache.Graph.Views[cache.Graph.PrimaryView].Cache.GetItemType()));
    cach.IsDirty = isDirty;
  }

  public static int? GetCurrentEmployeeID(PXCache cache)
  {
    EPEmployee currentEmployee = EmployeeMaint.GetCurrentEmployee(cache.Graph);
    if (currentEmployee == null)
      return new int?();
    return PXCache<EPEmployee>.GetExtension<FSxEPEmployee>(currentEmployee).SDEnabled.GetValueOrDefault() && currentEmployee.VStatus != "I" ? currentEmployee.BAccountID : new int?();
  }

  public static DateTime? GetNextExecution(PXCache cache, FSSchedule fsScheduleRow)
  {
    bool flag = false;
    TimeSlotGenerator timeSlotGenerator = new TimeSlotGenerator();
    List<PX.Objects.FS.Scheduler.Schedule> scheduleList = new List<PX.Objects.FS.Scheduler.Schedule>();
    List<PX.Objects.FS.Scheduler.Schedule> schedule = MapFSScheduleToSchedule.convertFSScheduleToSchedule(cache, fsScheduleRow, fsScheduleRow.LastGeneratedElementDate, "IRSC");
    DateTime fromDate = fsScheduleRow.LastGeneratedElementDate ?? fsScheduleRow.StartDate.Value;
    DateTime? endDate = fsScheduleRow.EndDate;
    ref bool local = ref flag;
    return timeSlotGenerator.GenerateNextOccurrence((IEnumerable<PX.Objects.FS.Scheduler.Schedule>) schedule, fromDate, endDate, out local)?.Date;
  }

  public static bool? IsDisableFixScheduleAction(PXGraph graph)
  {
    return PXResultset<FSSetup>.op_Implicit(PXSelectBase<FSSetup, PXSelect<FSSetup>.Config>.Select(graph, Array.Empty<object>())).DisableFixScheduleAction;
  }

  public static string WarnUserWithSchedulesWithoutNextExecution(
    PXGraph graph,
    PXCache cache,
    PXAction fixButton,
    out bool warning)
  {
    PXGraph tempGraph = new PXGraph();
    warning = false;
    string str = "";
    bool? nullable = SharedFunctions.IsDisableFixScheduleAction(graph);
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue && SharedFunctions.SchedulesWithoutNextExecution(tempGraph) > 0)
    {
      warning = true;
      fixButton.SetVisible(true);
      str = "Some schedules are not displayed on the form because the next execution dates are not defined for them. Click the Fix Schedules Without Next Execution button to define the dates and view the schedules on the form.";
    }
    return str;
  }

  public static int SchedulesWithoutNextExecution(PXGraph tempGraph)
  {
    return PXSelectBase<FSSchedule, PXSelectReadonly<FSSchedule, Where<FSSchedule.nextExecutionDate, IsNull, And<FSSchedule.active, Equal<True>>>>.Config>.Select(tempGraph, Array.Empty<object>()).Count;
  }

  public static void UpdateSchedulesWithoutNextExecution(PXGraph callerGraph, PXCache cache)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SharedFunctions.\u003C\u003Ec__DisplayClass66_0 cDisplayClass660 = new SharedFunctions.\u003C\u003Ec__DisplayClass66_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass660.callerGraph = callerGraph;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass660.cache = cache;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    PXLongOperation.StartOperation(cDisplayClass660.callerGraph, new PXToggleAsyncDelegate((object) cDisplayClass660, __methodptr(\u003CUpdateSchedulesWithoutNextExecution\u003Eb__0)));
  }

  public static bool GetEnableSeasonSetting(
    PXGraph graph,
    FSSchedule fsScheduleRow,
    FSSetup fsSetupRow = null)
  {
    bool enableSeasonSetting = false;
    if (fsScheduleRow is FSRouteContractSchedule)
    {
      FSRouteSetup managementRouteSetup = ServiceManagementSetup.GetServiceManagementRouteSetup(graph);
      if (managementRouteSetup != null)
        enableSeasonSetting = managementRouteSetup.EnableSeasonScheduleContract.GetValueOrDefault();
    }
    else
    {
      if (fsSetupRow == null)
        fsSetupRow = ServiceManagementSetup.GetServiceManagementSetup(graph);
      if (fsSetupRow != null)
        enableSeasonSetting = fsSetupRow.EnableSeasonScheduleContract.GetValueOrDefault();
    }
    return enableSeasonSetting;
  }

  public static SharedClasses.SubAccountIDTupla GetSubAccountIDs(
    PXGraph graph,
    FSSrvOrdType fsSrvOrdTypeRow,
    int? inventoryID,
    int? branchID,
    int? locationID,
    int? branchLocationID,
    int? salesPersonID,
    int? siteID)
  {
    PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow(graph, inventoryID);
    return SharedFunctions.GetSubAccountIDsTuple(graph, fsSrvOrdTypeRow, branchID, locationID, branchLocationID, salesPersonID, inventoryItemRow, siteID);
  }

  private static SharedClasses.SubAccountIDTupla GetSubAccountIDsTuple(
    PXGraph graph,
    FSSrvOrdType fsSrvOrdTypeRow,
    int? branchID,
    int? locationID,
    int? branchLocationID,
    int? salesPersonID,
    PX.Objects.IN.InventoryItem inventoryItemRow,
    int? inSiteID)
  {
    PX.Objects.CR.Location location1 = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select(graph, new object[1]
    {
      (object) branchID
    }));
    PX.Objects.CR.Location location2 = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>.Config>.Select(graph, new object[1]
    {
      (object) locationID
    }));
    FSBranchLocation fsBranchLocation = FSBranchLocation.PK.Find(graph, branchLocationID);
    PX.Objects.IN.INSite inSite = PXResultset<PX.Objects.IN.INSite>.op_Implicit(PXSelectBase<PX.Objects.IN.INSite, PXSelect<PX.Objects.IN.INSite, Where<PX.Objects.IN.INSite.siteID, Equal<Required<PX.Objects.IN.INSite.siteID>>>>.Config>.Select(graph, new object[1]
    {
      (object) inSiteID
    }));
    INPostClass inPostClass = PXResultset<INPostClass>.op_Implicit(PXSelectBase<INPostClass, PXSelect<INPostClass, Where<INPostClass.postClassID, Equal<Required<INPostClass.postClassID>>>>.Config>.Select(graph, new object[1]
    {
      (object) inventoryItemRow?.PostClassID
    }));
    PX.Objects.AR.SalesPerson salesPerson = PXResultset<PX.Objects.AR.SalesPerson>.op_Implicit(PXSelectBase<PX.Objects.AR.SalesPerson, PXSelect<PX.Objects.AR.SalesPerson, Where<PX.Objects.AR.SalesPerson.salesPersonID, Equal<Required<PX.Objects.AR.SalesPerson.salesPersonID>>>>.Config>.Select(graph, new object[1]
    {
      (object) salesPersonID
    }));
    if (location2 == null || inventoryItemRow == null || fsSrvOrdTypeRow == null || location1 == null || fsBranchLocation == null)
      return (SharedClasses.SubAccountIDTupla) null;
    int? subId1 = fsBranchLocation?.SubID;
    int? cmpSalesSubId = (int?) location1?.CMPSalesSubID;
    int? salesSubId1 = (int?) inventoryItemRow?.SalesSubID;
    int? csalesSubId = (int?) location2?.CSalesSubID;
    int? salesSubId2 = (int?) inPostClass?.SalesSubID;
    int? salesSubId3 = (int?) salesPerson?.SalesSubID;
    int? subId2 = (int?) fsSrvOrdTypeRow?.SubID;
    int? salesSubId4 = (int?) inSite?.SalesSubID;
    int? company_SubID = cmpSalesSubId;
    int? item_SubID = salesSubId1;
    int? customer_SubID = csalesSubId;
    int? postingClass_SubID = salesSubId2;
    int? salesPerson_SubID = salesSubId3;
    int? srvOrdType_SubID = subId2;
    int? warehouse_SubID = salesSubId4;
    return new SharedClasses.SubAccountIDTupla(subId1, company_SubID, item_SubID, customer_SubID, postingClass_SubID, salesPerson_SubID, srvOrdType_SubID, warehouse_SubID);
  }

  public static bool ConcatenateNote(
    PXCache srcCache,
    PXCache dstCache,
    object srcObj,
    object dstObj)
  {
    string note1 = PXNoteAttribute.GetNote(dstCache, dstObj);
    if (!(note1 != string.Empty) || note1 == null)
      return true;
    string note2 = PXNoteAttribute.GetNote(srcCache, srcObj);
    string str = note1 + Environment.NewLine + Environment.NewLine + note2;
    PXNoteAttribute.SetNote(dstCache, dstObj, str);
    return false;
  }

  public static void CopyNotesAndFiles(
    PXCache srcCache,
    PXCache dstCache,
    object srcObj,
    object dstObj,
    bool? copyNotes,
    bool? copyFiles)
  {
    if (copyNotes.GetValueOrDefault())
      copyNotes = new bool?(SharedFunctions.ConcatenateNote(srcCache, dstCache, srcObj, dstObj));
    PXNoteAttribute.CopyNoteAndFiles(srcCache, srcObj, dstCache, dstObj, copyNotes, copyFiles);
  }

  public static void CopyNotesAndFiles(
    PXCache cache,
    FSSrvOrdType fsSrvOrdTypeRow,
    object document,
    int? customerID,
    int? locationID)
  {
    bool flag1 = false;
    if (fsSrvOrdTypeRow.CopyNotesFromCustomer.GetValueOrDefault() || fsSrvOrdTypeRow.CopyAttachmentsFromCustomer.GetValueOrDefault())
    {
      CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
      ((PXSelectBase<PX.Objects.AR.Customer>) instance.BAccount).Current = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) instance.BAccount).Search<PX.Objects.AR.Customer.bAccountID>((object) customerID, Array.Empty<object>()));
      SharedFunctions.CopyNotesAndFiles(((PXSelectBase) instance.BAccount).Cache, cache, (object) ((PXSelectBase<PX.Objects.AR.Customer>) instance.BAccount).Current, document, fsSrvOrdTypeRow.CopyNotesFromCustomer, fsSrvOrdTypeRow.CopyAttachmentsFromCustomer);
      flag1 = true;
    }
    if (fsSrvOrdTypeRow.CopyNotesFromCustomerLocation.GetValueOrDefault() || fsSrvOrdTypeRow.CopyAttachmentsFromCustomerLocation.GetValueOrDefault())
    {
      CustomerLocationMaint instance = PXGraph.CreateInstance<CustomerLocationMaint>();
      PX.Objects.AR.Customer customerRow = SharedFunctions.GetCustomerRow(cache.Graph, customerID);
      if (customerRow != null)
      {
        ((PXSelectBase<PX.Objects.CR.Location>) instance.Location).Current = PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) instance.Location).Search<PX.Objects.CR.Location.locationID>((object) locationID, new object[1]
        {
          (object) customerRow.AcctCD
        }));
        SharedFunctions.CopyNotesAndFiles(((PXSelectBase) instance.Location).Cache, cache, (object) ((PXSelectBase<PX.Objects.CR.Location>) instance.Location).Current, document, fsSrvOrdTypeRow.CopyNotesFromCustomerLocation, fsSrvOrdTypeRow.CopyAttachmentsFromCustomerLocation);
        flag1 = true;
      }
    }
    if (!(document.GetType() == typeof (FSAppointment)) || !fsSrvOrdTypeRow.CopyNotesToAppoinment.GetValueOrDefault() && !fsSrvOrdTypeRow.CopyAttachmentsToAppoinment.GetValueOrDefault())
      return;
    FSServiceOrder srcObj = PXResultset<FSServiceOrder>.op_Implicit(PXSelectBase<FSServiceOrder, PXSelect<FSServiceOrder, Where<FSServiceOrder.sOID, Equal<Required<FSServiceOrder.sOID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) ((FSAppointment) document).SOID
    }));
    string str = (string) null;
    if (flag1)
      str = PXNoteAttribute.GetNote(cache, document);
    bool flag2 = (str == string.Empty || str == null) && fsSrvOrdTypeRow.CopyNotesToAppoinment.Value;
    SharedFunctions.CopyNotesAndFiles((PXCache) new PXCache<FSServiceOrder>(cache.Graph), cache, (object) srcObj, document, new bool?(flag2), fsSrvOrdTypeRow.CopyAttachmentsToAppoinment);
  }

  public static void CopyNotesAndFiles(
    PXCache dstCache,
    object lineDocument,
    IDocLine srcLineDocument,
    FSSrvOrdType fsSrvOrdTypeRow)
  {
    if (!fsSrvOrdTypeRow.CopyLineNotesToInvoice.GetValueOrDefault() && !fsSrvOrdTypeRow.CopyLineAttachmentsToInvoice.GetValueOrDefault())
      return;
    if (srcLineDocument.SourceTable == "FSSODet")
      SharedFunctions.CopyNotesAndFiles((PXCache) new PXCache<FSSODet>(dstCache.Graph), dstCache, (object) srcLineDocument, lineDocument, fsSrvOrdTypeRow.CopyLineNotesToInvoice, fsSrvOrdTypeRow.CopyLineAttachmentsToInvoice);
    else
      SharedFunctions.CopyNotesAndFiles((PXCache) new PXCache<FSAppointmentDet>(dstCache.Graph), dstCache, (object) srcLineDocument, lineDocument, fsSrvOrdTypeRow.CopyLineNotesToInvoice, fsSrvOrdTypeRow.CopyLineAttachmentsToInvoice);
  }

  public static PXResultset<FSPostBatch> GetPostBachByProcessID(
    PXGraph graph,
    Guid currentProcessID)
  {
    return PXSelectBase<FSPostBatch, PXSelectJoinGroupBy<FSPostBatch, InnerJoin<FSPostDoc, On<FSPostDoc.batchID, Equal<FSPostBatch.batchID>>>, Where<FSPostDoc.processID, Equal<Required<FSPostDoc.processID>>>, Aggregate<GroupBy<FSPostBatch.batchID>>>.Config>.Select(graph, new object[1]
    {
      (object) currentProcessID
    });
  }

  public static void ServiceContractDynamicDropdown(
    PXCache cache,
    FSServiceContract fsServiceContractRow)
  {
    if (fsServiceContractRow == null)
      return;
    switch (fsServiceContractRow.RecordType)
    {
      case "NRSC":
        if (fsServiceContractRow.BillingType == "STDB")
        {
          PXStringListAttribute.SetList<FSServiceContract.scheduleGenType>(cache, (object) fsServiceContractRow, new Tuple<string, string>[3]
          {
            new Tuple<string, string>("SO", "Service Orders"),
            new Tuple<string, string>("AP", "Appointments"),
            new Tuple<string, string>("NA", "None")
          });
          break;
        }
        PXStringListAttribute.SetList<FSServiceContract.scheduleGenType>(cache, (object) fsServiceContractRow, new Tuple<string, string>[2]
        {
          new Tuple<string, string>("SO", "Service Orders"),
          new Tuple<string, string>("AP", "Appointments")
        });
        break;
      case "IRSC":
        if (fsServiceContractRow.BillingType == "STDB")
        {
          PXStringListAttribute.SetList<FSServiceContract.scheduleGenType>(cache, (object) fsServiceContractRow, new Tuple<string, string>[2]
          {
            new Tuple<string, string>("AP", "Appointments"),
            new Tuple<string, string>("NA", "None")
          });
          break;
        }
        PXStringListAttribute.SetList<FSServiceContract.scheduleGenType>(cache, (object) fsServiceContractRow, new Tuple<string, string>[1]
        {
          new Tuple<string, string>("AP", "Appointments")
        });
        break;
    }
  }

  public static void DefaultGenerationType(
    PXCache cache,
    FSServiceContract fsServiceContractRow,
    PXFieldDefaultingEventArgs e)
  {
    if (fsServiceContractRow == null || fsServiceContractRow.RecordType == null)
      return;
    switch (fsServiceContractRow.RecordType)
    {
      case "NRSC":
        e.NewValue = (object) "SO";
        ((CancelEventArgs) e).Cancel = true;
        break;
      case "IRSC":
        e.NewValue = (object) "AP";
        ((CancelEventArgs) e).Cancel = true;
        break;
    }
  }

  internal static int? GetEquipmentComponentID(
    PXGraph graph,
    int? smEquipmentID,
    int? equipmentLineNbr)
  {
    return PXResultset<FSEquipmentComponent>.op_Implicit(PXSelectBase<FSEquipmentComponent, PXSelect<FSEquipmentComponent, Where<FSEquipmentComponent.SMequipmentID, Equal<Required<FSEquipmentComponent.SMequipmentID>>, And<FSEquipmentComponent.lineNbr, Equal<Required<FSEquipmentComponent.lineNbr>>>>>.Config>.Select(graph, new object[2]
    {
      (object) smEquipmentID,
      (object) equipmentLineNbr
    }))?.ComponentID;
  }

  public static void SetVisibleEnableProjectField<projectRelatedField>(
    PXCache cache,
    object row,
    int? projectID)
    where projectRelatedField : IBqlField
  {
    bool flag = ProjectDefaultAttribute.IsNonProject(projectID);
    PXUIFieldAttribute.SetVisible<projectRelatedField>(cache, row, !flag);
    PXUIFieldAttribute.SetEnabled<projectRelatedField>(cache, row, !flag);
    PXUIFieldAttribute.SetRequired<projectRelatedField>(cache, !flag);
    PXDefaultAttribute.SetPersistingCheck<projectRelatedField>(cache, row, !flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  public static void SetEnableCostCodeProjectTask<projectTaskField, costCodeField>(
    PXCache cache,
    object row,
    string lineType,
    int? projectID)
    where projectTaskField : IBqlField
    where costCodeField : IBqlField
  {
    bool flag1 = ProjectDefaultAttribute.IsNonProject(projectID);
    bool flag2 = lineType != "CM_LN" && lineType != "IT_LN";
    PXUIFieldAttribute.SetEnabled<projectTaskField>(cache, row, !flag1 & flag2);
    PXUIFieldAttribute.SetEnabled<costCodeField>(cache, row, projectID.HasValue & flag2);
    PXUIFieldAttribute.SetRequired<projectTaskField>(cache, !flag1 & flag2);
    PXDefaultAttribute.SetPersistingCheck<projectTaskField>(cache, row, !flag1 & flag2 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  public static Dictionary<string, string> GetCalendarMessages()
  {
    Dictionary<string, string> calendarMessages = new Dictionary<string, string>();
    foreach (FieldInfo field in typeof (TX.CalendarMessages).GetFields())
      calendarMessages[field.Name] = PXLocalizer.Localize(field.GetValue((object) null).ToString(), typeof (TX.CalendarMessages).FullName);
    int index1 = 0;
    foreach (string dayName1 in DateTimeFormatInfo.CurrentInfo.DayNames)
    {
      string dayName2 = DateTimeFormatInfo.InvariantInfo.DayNames[index1];
      calendarMessages[dayName2] = dayName1;
      ++index1;
    }
    int index2 = 0;
    foreach (string abbreviatedDayName in DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames)
    {
      string key = "abbr_" + DateTimeFormatInfo.InvariantInfo.DayNames[index2];
      calendarMessages[key] = abbreviatedDayName;
      ++index2;
    }
    int index3 = 0;
    foreach (string monthName1 in DateTimeFormatInfo.CurrentInfo.MonthNames)
    {
      string monthName2 = DateTimeFormatInfo.InvariantInfo.MonthNames[index3];
      calendarMessages[monthName2] = monthName1;
      ++index3;
    }
    int index4 = 0;
    foreach (string abbreviatedMonthName in DateTimeFormatInfo.CurrentInfo.AbbreviatedMonthNames)
    {
      string key = "abbr_" + DateTimeFormatInfo.InvariantInfo.MonthNames[index4];
      calendarMessages[key] = abbreviatedMonthName;
      ++index4;
    }
    return calendarMessages;
  }

  public static void ValidatePostToByFeatures<postToField>(
    PXCache cache,
    object row,
    string postTo)
    where postToField : IBqlField
  {
    PXCache<FeaturesSet> pxCache = new PXCache<FeaturesSet>(cache.Graph);
    if (postTo == "SO" && !PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
    {
      cache.RaiseExceptionHandling<postToField>(row, (object) postTo, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormat("Documents of the {0} type cannot be generated because the {1} feature is disabled on the Enable/Disable Features (CS100000) form.", new object[2]
      {
        (object) "Sales Order",
        (object) PXUIFieldAttribute.GetDisplayName<FeaturesSet.distributionModule>((PXCache) pxCache)
      }), (PXErrorLevel) 4));
    }
    else
    {
      if (!(postTo == "SI") || PXAccess.FeatureInstalled<FeaturesSet.advancedSOInvoices>())
        return;
      cache.RaiseExceptionHandling<postToField>(row, (object) postTo, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormat("Documents of the {0} type cannot be generated because the {1} feature is disabled on the Enable/Disable Features (CS100000) form.", new object[2]
      {
        (object) "SO Invoice",
        (object) PXUIFieldAttribute.GetDisplayName<FeaturesSet.advancedSOInvoices>((PXCache) pxCache)
      }), (PXErrorLevel) 4));
    }
  }

  public static bool IsLotSerialRequired(PXCache cache, int? inventoryID)
  {
    INLotSerClass inLotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(SharedFunctions.ReadInventoryItem(cache, inventoryID));
    return inLotSerClass != null && inLotSerClass.LotSerTrack != null && !(inLotSerClass.LotSerTrack == "N");
  }

  public static PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> ReadInventoryItem(
    PXCache sender,
    int? inventoryID)
  {
    if (!inventoryID.HasValue)
      return (PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>) null;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, inventoryID);
    if (inventoryItem == null)
      throw new PXException("{0} '{1}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[2]
      {
        (object) "Inventory Item",
        (object) inventoryID
      });
    INLotSerClass inLotSerClass;
    if (inventoryItem.StkItem.GetValueOrDefault())
    {
      inLotSerClass = INLotSerClass.PK.Find(sender.Graph, inventoryItem.LotSerClassID);
      if (inLotSerClass == null)
        throw new PXException("{0} '{1}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[2]
        {
          (object) "Lot/Serial Class",
          (object) inventoryItem.LotSerClassID
        });
    }
    else
      inLotSerClass = new INLotSerClass();
    return new PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>(inventoryItem, inLotSerClass);
  }

  public static List<SM_ARReleaseProcess.ItemInfo> GetDifferentItemList(
    PXGraph graph,
    PX.Objects.AR.ARTran arTran,
    bool createDifferentEntriesForQtyGreaterThan1)
  {
    if (!arTran.InventoryID.HasValue)
      return (List<SM_ARReleaseProcess.ItemInfo>) null;
    List<SM_ARReleaseProcess.ItemInfo> lotSerialList = new List<SM_ARReleaseProcess.ItemInfo>();
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = SharedFunctions.ReadInventoryItem(graph.Caches[typeof (PX.Objects.IN.InventoryItem)], arTran.InventoryID);
    PX.Objects.PO.POReceipt poReceipt = PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceipt, PXSelect<PX.Objects.PO.POReceipt, Where<PX.Objects.PO.POReceipt.receiptNbr, Equal<Required<PX.Objects.AR.ARTran.sOShipmentNbr>>>>.Config>.Select(graph, new object[1]
    {
      (object) arTran.SOShipmentNbr
    }));
    if (pxResult == null || PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerTrack == "N")
    {
      SM_ARReleaseProcess.ItemInfo itemInfo = new SM_ARReleaseProcess.ItemInfo(arTran);
      itemInfo.UOM = (string) null;
      itemInfo.Qty = new Decimal?();
      if (!createDifferentEntriesForQtyGreaterThan1)
      {
        lotSerialList.Add(itemInfo);
      }
      else
      {
        itemInfo.BaseQty = new Decimal?((Decimal) 1);
        int num1 = 0;
        while (true)
        {
          Decimal num2 = (Decimal) num1;
          Decimal? baseQty = arTran.BaseQty;
          Decimal valueOrDefault = baseQty.GetValueOrDefault();
          if (num2 < valueOrDefault & baseQty.HasValue)
          {
            lotSerialList.Add(itemInfo);
            ++num1;
          }
          else
            break;
        }
      }
    }
    else if (arTran.SOShipmentType != null && arTran.SOShipmentNbr != null && PX.Objects.SO.SOShipment.UK.Find(graph, arTran.SOShipmentType, arTran.SOShipmentNbr) != null)
    {
      IEnumerable<SOShipLineSplit> soShipLineSplits;
      if (arTran.SOShipmentLineNbr.HasValue)
        soShipLineSplits = GraphHelper.RowCast<SOShipLineSplit>((IEnumerable) PXSelectBase<SOShipLineSplit, PXSelect<SOShipLineSplit, Where<SOShipLineSplit.shipmentNbr, Equal<Required<SOShipLineSplit.shipmentNbr>>, And<SOShipLineSplit.lineNbr, Equal<Required<SOShipLineSplit.lineNbr>>>>>.Config>.Select(graph, new object[2]
        {
          (object) arTran.SOShipmentNbr,
          (object) arTran.SOShipmentLineNbr
        }));
      else
        soShipLineSplits = GraphHelper.RowCast<SOShipLineSplit>((IEnumerable) PXSelectBase<SOShipLineSplit, PXViewOf<SOShipLineSplit>.BasedOn<SelectFromBase<SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipLineSplit.shipmentNbr, Equal<P.AsString>>>>, And<BqlOperand<SOShipLineSplit.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<SOShipLineSplit.origOrderType, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<SOShipLineSplit.origOrderNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<SOShipLineSplit.origLineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select(graph, new object[5]
        {
          (object) arTran.SOShipmentNbr,
          (object) arTran.InventoryID,
          (object) arTran.SOOrderType,
          (object) arTran.SOOrderNbr,
          (object) arTran.SOOrderLineNbr
        }));
      using (IEnumerator<SOShipLineSplit> enumerator = soShipLineSplits.GetEnumerator())
      {
label_19:
        while (enumerator.MoveNext())
        {
          SOShipLineSplit current = enumerator.Current;
          SM_ARReleaseProcess.ItemInfo itemInfo1 = new SM_ARReleaseProcess.ItemInfo(current);
          itemInfo1.UOM = (string) null;
          SM_ARReleaseProcess.ItemInfo itemInfo2 = itemInfo1;
          Decimal? nullable1 = new Decimal?();
          Decimal? nullable2 = nullable1;
          itemInfo2.Qty = nullable2;
          if (!createDifferentEntriesForQtyGreaterThan1)
          {
            lotSerialList.Add(itemInfo1);
          }
          else
          {
            itemInfo1.BaseQty = new Decimal?((Decimal) 1);
            int num3 = 0;
            while (true)
            {
              Decimal num4 = (Decimal) num3;
              nullable1 = current.BaseQty;
              Decimal valueOrDefault = nullable1.GetValueOrDefault();
              if (num4 < valueOrDefault & nullable1.HasValue)
              {
                lotSerialList.Add(itemInfo1);
                ++num3;
              }
              else
                goto label_19;
            }
          }
        }
      }
    }
    else if (arTran.SOShipmentType != null && arTran.SOShipmentNbr != null && poReceipt != null)
    {
      using (IEnumerator<PX.Objects.PO.POReceiptLine> enumerator = GraphHelper.RowCast<PX.Objects.PO.POReceiptLine>((IEnumerable) PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsEqual<P.AsString>>>.ReadOnly.Config>.Select(graph, new object[1]
      {
        (object) poReceipt.ReceiptNbr
      })).GetEnumerator())
      {
label_31:
        while (enumerator.MoveNext())
        {
          PX.Objects.PO.POReceiptLine current = enumerator.Current;
          SM_ARReleaseProcess.ItemInfo itemInfo3 = new SM_ARReleaseProcess.ItemInfo(current);
          itemInfo3.UOM = (string) null;
          Decimal? nullable = new Decimal?();
          itemInfo3.Qty = nullable;
          SM_ARReleaseProcess.ItemInfo itemInfo4 = itemInfo3;
          if (!createDifferentEntriesForQtyGreaterThan1)
          {
            lotSerialList.Add(itemInfo4);
          }
          else
          {
            itemInfo4.BaseQty = new Decimal?((Decimal) 1);
            int num5 = 0;
            while (true)
            {
              Decimal num6 = (Decimal) num5;
              nullable = current.BaseQty;
              Decimal valueOrDefault = nullable.GetValueOrDefault();
              if (num6 < valueOrDefault & nullable.HasValue)
              {
                lotSerialList.Add(itemInfo4);
                ++num5;
              }
              else
                goto label_31;
            }
          }
        }
      }
    }
    else if (arTran.SOOrderType != null && arTran.SOOrderNbr != null && arTran.SOOrderLineNbr.HasValue)
    {
      using (IEnumerator<PX.Objects.SO.SOLineSplit> enumerator = GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) PXSelectBase<PX.Objects.SO.SOLineSplit, PXSelect<PX.Objects.SO.SOLineSplit, Where<PX.Objects.SO.SOLineSplit.orderType, Equal<Required<PX.Objects.SO.SOLineSplit.orderType>>, And<PX.Objects.SO.SOLineSplit.orderNbr, Equal<Required<PX.Objects.SO.SOLineSplit.orderNbr>>, And<PX.Objects.SO.SOLineSplit.lineNbr, Equal<Required<PX.Objects.SO.SOLineSplit.lineNbr>>>>>>.Config>.Select(graph, new object[3]
      {
        (object) arTran.SOOrderType,
        (object) arTran.SOOrderNbr,
        (object) arTran.SOOrderLineNbr
      })).GetEnumerator())
      {
label_44:
        while (enumerator.MoveNext())
        {
          PX.Objects.SO.SOLineSplit current = enumerator.Current;
          if (!string.IsNullOrEmpty(current.LotSerialNbr) || !current.POCreate.GetValueOrDefault() || !(current.POSource != "D"))
          {
            SM_ARReleaseProcess.ItemInfo itemInfo5 = new SM_ARReleaseProcess.ItemInfo(current);
            itemInfo5.UOM = (string) null;
            SM_ARReleaseProcess.ItemInfo itemInfo6 = itemInfo5;
            Decimal? nullable3 = new Decimal?();
            Decimal? nullable4 = nullable3;
            itemInfo6.Qty = nullable4;
            if (!createDifferentEntriesForQtyGreaterThan1)
            {
              lotSerialList.Add(itemInfo5);
            }
            else
            {
              itemInfo5.BaseQty = new Decimal?((Decimal) 1);
              int num7 = 0;
              while (true)
              {
                Decimal num8 = (Decimal) num7;
                nullable3 = current.BaseQty;
                Decimal valueOrDefault = nullable3.GetValueOrDefault();
                if (num8 < valueOrDefault & nullable3.HasValue)
                {
                  lotSerialList.Add(itemInfo5);
                  ++num7;
                }
                else
                  goto label_44;
              }
            }
          }
        }
      }
    }
    else if (pxResult != null && PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerTrack != "N")
    {
      SM_ARReleaseProcess.ItemInfo itemInfo = new SM_ARReleaseProcess.ItemInfo(arTran);
      itemInfo.UOM = (string) null;
      itemInfo.Qty = new Decimal?();
      if (!createDifferentEntriesForQtyGreaterThan1)
      {
        lotSerialList.Add(itemInfo);
      }
      else
      {
        itemInfo.BaseQty = new Decimal?((Decimal) 1);
        int num9 = 0;
        while (true)
        {
          Decimal num10 = (Decimal) num9;
          Decimal? baseQty = arTran.BaseQty;
          Decimal valueOrDefault = baseQty.GetValueOrDefault();
          if (num10 < valueOrDefault & baseQty.HasValue)
          {
            lotSerialList.Add(itemInfo);
            ++num9;
          }
          else
            break;
        }
      }
    }
    return SharedFunctions.GetVerifiedDifferentItemList(graph, arTran, lotSerialList);
  }

  public static List<SM_ARReleaseProcess.ItemInfo> GetVerifiedDifferentItemList(
    PXGraph graph,
    PX.Objects.AR.ARTran arTran,
    List<SM_ARReleaseProcess.ItemInfo> lotSerialList)
  {
    if (lotSerialList == null)
      lotSerialList = new List<SM_ARReleaseProcess.ItemInfo>();
    Decimal count = (Decimal) lotSerialList.Count;
    Decimal? baseQty = arTran.BaseQty;
    Decimal valueOrDefault = baseQty.GetValueOrDefault();
    if (count > valueOrDefault & baseQty.HasValue)
      throw new PXException("There are more Lot/Serial numbers in the shipment or sales order than the quantity of items specified in the invoice line.");
    return lotSerialList;
  }

  public enum MonthsOfYear
  {
    January = 1,
    February = 2,
    March = 3,
    April = 4,
    May = 5,
    June = 6,
    July = 7,
    August = 8,
    September = 9,
    October = 10, // 0x0000000A
    November = 11, // 0x0000000B
    December = 12, // 0x0000000C
  }

  public enum SOAPDetOriginTab
  {
    Services,
    InventoryItems,
  }
}
