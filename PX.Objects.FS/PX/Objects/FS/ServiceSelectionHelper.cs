// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceSelectionHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.FS;

public class ServiceSelectionHelper
{
  /// <summary>
  /// Initialize the EmployeeGrid filter with the existing employees in the Employee tab.
  /// </summary>
  /// <param name="employeesView">Employee view from Appointment or ServiceOrder screen.</param>
  private static IEnumerable<EmployeeGridFilter> PopulateEmployeeFilter(object employeesView)
  {
    HashSet<EmployeeGridFilter> employeeGridFilterSet = new HashSet<EmployeeGridFilter>();
    if (employeesView is AppointmentEntry.AppointmentServiceEmployees_View)
    {
      foreach (PXResult<FSAppointmentEmployee> pxResult in ((PXSelectBase<FSAppointmentEmployee>) employeesView).Select(Array.Empty<object>()))
      {
        FSAppointmentEmployee appointmentEmployee = PXResult<FSAppointmentEmployee>.op_Implicit(pxResult);
        employeeGridFilterSet.Add(new EmployeeGridFilter()
        {
          EmployeeID = appointmentEmployee.EmployeeID
        });
      }
    }
    if (employeesView is ServiceOrderEntry.ServiceOrderEmployees_View)
    {
      foreach (PXResult<FSSOEmployee> pxResult in ((PXSelectBase<FSSOEmployee>) employeesView).Select(Array.Empty<object>()))
      {
        FSSOEmployee fssoEmployee = PXResult<FSSOEmployee>.op_Implicit(pxResult);
        employeeGridFilterSet.Add(new EmployeeGridFilter()
        {
          EmployeeID = fssoEmployee.EmployeeID
        });
      }
    }
    return (IEnumerable<EmployeeGridFilter>) employeeGridFilterSet;
  }

  public static List<int?> GetServicesInServiceTab<ServiceDetType>(
    PXSelectBase<ServiceDetType> servicesView,
    string serviceLineRefNbr)
    where ServiceDetType : class, IBqlTable, IFSSODetBase, new()
  {
    List<int?> servicesInServiceTab = new List<int?>();
    foreach (ServiceDetType serviceDetType in GraphHelper.RowCast<ServiceDetType>((IEnumerable) servicesView.Select(Array.Empty<object>())).Where<ServiceDetType>((Func<ServiceDetType, bool>) (x => x.IsService)))
    {
      if (string.IsNullOrEmpty(serviceLineRefNbr) || serviceDetType.LineRef == serviceLineRefNbr)
        servicesInServiceTab.Add(serviceDetType.InventoryID);
    }
    return servicesInServiceTab;
  }

  private static IEnumerable<PX.Objects.IN.InventoryItem> GetListWithServicesOnly(
    PXSelectBase<PX.Objects.IN.InventoryItem> cmd,
    List<int?> serviceList)
  {
    foreach (PXResult<PX.Objects.IN.InventoryItem> pxResult in cmd.Select(Array.Empty<object>()))
    {
      PX.Objects.IN.InventoryItem inventoryRow = PXResult<PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      if (!serviceList.Exists((Predicate<int?>) (serviceID =>
      {
        int? nullable = serviceID;
        int? inventoryId = inventoryRow.InventoryID;
        return nullable.GetValueOrDefault() == inventoryId.GetValueOrDefault() & nullable.HasValue == inventoryId.HasValue;
      })))
        yield return inventoryRow;
    }
  }

  /// <summary>
  /// Check if the given service skills are contained by each given employee.
  /// </summary>
  /// <param name="serviceSkills">Service with its skills.</param>
  /// <param name="employeeSkillList">List of employees and their skills.</param>
  private static bool CanThisServiceBeCompleteByTheseEmployeesSkills(
    SharedClasses.ItemList serviceSkills,
    List<SharedClasses.ItemList> employeeSkillList)
  {
    if (employeeSkillList.Count <= 0)
      return false;
    foreach (SharedClasses.ItemList itemList in employeeSkillList.Where<SharedClasses.ItemList>((Func<SharedClasses.ItemList, bool>) (x => x.list.Count > 0)))
    {
      if (serviceSkills.list.Except<object>((IEnumerable<object>) itemList.list).Any<object>())
        return false;
    }
    return true;
  }

  /// <summary>
  /// Check if the given service License are contained by each given employee.
  /// </summary>
  /// <param name="serviceLicenses">Service with its licenses.</param>
  /// <param name="employeeLicenseList">List of employees and their licenses.</param>
  private static bool CanThisServiceBeCompleteByTheseEmployeesLicenses(
    SharedClasses.ItemList serviceLicenses,
    List<SharedClasses.ItemList> employeeLicenseList)
  {
    if (employeeLicenseList.Count <= 0)
      return false;
    foreach (SharedClasses.ItemList itemList in employeeLicenseList.Where<SharedClasses.ItemList>((Func<SharedClasses.ItemList, bool>) (x => x.list.Count > 0)))
    {
      if (serviceLicenses.list.Except<object>((IEnumerable<object>) itemList.list).Any<object>())
        return false;
    }
    return true;
  }

  public static void InsertCurrentService<ServiceDetType>(
    PXSelectBase<PX.Objects.IN.InventoryItem> inventoryView,
    PXSelectBase<ServiceDetType> serviceDetView)
    where ServiceDetType : class, IBqlTable, IFSSODetBase, new()
  {
    if (inventoryView.Current == null)
      return;
    PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow(((PXSelectBase) serviceDetView).Cache.Graph, inventoryView.Current.InventoryID);
    ServiceDetType serviceDetType = new ServiceDetType();
    serviceDetType.LineType = "SERVI";
    serviceDetType.InventoryID = inventoryItemRow.InventoryID;
    serviceDetView.Insert(serviceDetType);
  }

  public static void OpenServiceSelector<DocDate>(
    PXCache documentCache,
    PXFilter<ServiceSelectionFilter> serviceSelectorFilter,
    PXSelect<EmployeeGridFilter> employeeGridFilter)
    where DocDate : class, IBqlField
  {
    if (documentCache.Current != null)
      ((PXSelectBase<ServiceSelectionFilter>) serviceSelectorFilter).Current.ScheduledDateTimeBegin = (DateTime?) documentCache.GetValue<DocDate>(documentCache.Current);
    ((PXSelectBase) employeeGridFilter).Cache.Clear();
    ((PXSelectBase<ServiceSelectionFilter>) serviceSelectorFilter).AskExt();
  }

  public static IEnumerable EmployeeRecordsDelegate(
    object employeeView,
    PXSelectBase<EmployeeGridFilter> employeeFilterView)
  {
    foreach (EmployeeGridFilter employeeGridFilter1 in ServiceSelectionHelper.PopulateEmployeeFilter(employeeView))
    {
      EmployeeGridFilter employeeGridFilter2 = employeeFilterView.Locate(employeeGridFilter1);
      if (employeeGridFilter2 == null)
      {
        employeeGridFilter1.Mem_Selected = new bool?(true);
        employeeGridFilter2 = employeeFilterView.Insert(employeeGridFilter1);
      }
      ((PXSelectBase) employeeFilterView).Cache.SetStatus((object) employeeGridFilter2, (PXEntryStatus) 5);
      ((PXSelectBase) employeeFilterView).Cache.IsDirty = false;
      yield return (object) employeeGridFilter2;
    }
  }

  public static IEnumerable ServiceRecordsDelegate<ServiceDetType>(
    PXSelectBase<ServiceDetType> servicesView,
    PXSelectBase<EmployeeGridFilter> employeesView,
    PXFilter<ServiceSelectionFilter> filter)
    where ServiceDetType : class, IBqlTable, IFSSODetBase, new()
  {
    if (((PXSelectBase<ServiceSelectionFilter>) filter).Current != null)
    {
      PXGraph graph = ((PXSelectBase) filter).Cache.Graph;
      PXSelectBase<PX.Objects.IN.InventoryItem> cmd = (PXSelectBase<PX.Objects.IN.InventoryItem>) new ServiceSelectionHelper.ServiceRecords_View(graph);
      List<int?> servicesInServiceTab = ServiceSelectionHelper.GetServicesInServiceTab<ServiceDetType>(servicesView, (string) null);
      ParameterExpression parameterExpression1;
      ParameterExpression parameterExpression2;
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      List<int?> list1 = ((IQueryable<PXResult<EmployeeGridFilter>>) employeesView.Select(Array.Empty<object>())).Where<PXResult<EmployeeGridFilter>>(Expression.Lambda<Func<PXResult<EmployeeGridFilter>, bool>>((Expression) Expression.Equal((Expression) Expression.Property((Expression) Expression.Call(y, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (EmployeeGridFilter.get_Mem_Selected))), (Expression) Expression.Convert((Expression) Expression.Constant((object) true, typeof (bool)), typeof (bool?))), parameterExpression1)).Select<PXResult<EmployeeGridFilter>, int?>(Expression.Lambda<Func<PXResult<EmployeeGridFilter>, int?>>((Expression) Expression.Property((Expression) Expression.Call(y, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (EmployeeGridFilter.get_EmployeeID))), parameterExpression2)).ToList<int?>();
      IEnumerable<PX.Objects.IN.InventoryItem> withServicesOnly = ServiceSelectionHelper.GetListWithServicesOnly(cmd, servicesInServiceTab);
      if (list1.Count == 0)
      {
        foreach (object obj in withServicesOnly)
          yield return obj;
      }
      else
      {
        List<int?> list2 = withServicesOnly.Select<PX.Objects.IN.InventoryItem, int?>((Func<PX.Objects.IN.InventoryItem, int?>) (y => y.InventoryID)).ToList<int?>();
        List<SharedClasses.ItemList> serviceSkillList = SharedFunctions.GetItemWithList<FSServiceSkill, FSServiceSkill.serviceID, FSServiceSkill.skillID>(graph, list2);
        List<SharedClasses.ItemList> employeeSkillList = SharedFunctions.GetItemWithList<FSEmployeeSkill, FSEmployeeSkill.employeeID, FSEmployeeSkill.skillID>(graph, list1);
        List<SharedClasses.ItemList> serviceLicenseList = SharedFunctions.GetItemWithList<FSServiceLicenseType, FSServiceLicenseType.serviceID, FSServiceLicenseType.licenseTypeID>(graph, list2);
        ((PXSelectBase<ServiceSelectionFilter>) filter).Current.ScheduledDateTimeBegin = SharedFunctions.RemoveTimeInfo(((PXSelectBase<ServiceSelectionFilter>) filter).Current.ScheduledDateTimeBegin);
        List<SharedClasses.ItemList> employeeLicenseList = SharedFunctions.GetItemWithList<FSLicense, FSLicense.employeeID, FSLicense.licenseTypeID, Where2<Where<Current<ServiceSelectionFilter.scheduledDateTimeBegin>, IsNull>, Or<Where<FSLicense.issueDate, LessEqual<Current<ServiceSelectionFilter.scheduledDateTimeBegin>>, And<Where<FSLicense.expirationDate, GreaterEqual<Current<ServiceSelectionFilter.scheduledDateTimeBegin>>, Or<FSLicense.expirationDate, IsNull>>>>>>>(graph, list1);
        foreach (PX.Objects.IN.InventoryItem inventoryItem in withServicesOnly)
        {
          PX.Objects.IN.InventoryItem inventoryRow = inventoryItem;
          SharedClasses.ItemList serviceSkills = (SharedClasses.ItemList) null;
          SharedClasses.ItemList serviceLicenses = (SharedClasses.ItemList) null;
          if (serviceSkillList.Count != 0)
            serviceSkills = serviceSkillList.FirstOrDefault<SharedClasses.ItemList>((Func<SharedClasses.ItemList, bool>) (y =>
            {
              int? itemId = y.itemID;
              int? inventoryId = inventoryRow.InventoryID;
              return itemId.GetValueOrDefault() == inventoryId.GetValueOrDefault() & itemId.HasValue == inventoryId.HasValue;
            }));
          if (serviceLicenseList.Count != 0)
            serviceLicenses = serviceLicenseList.FirstOrDefault<SharedClasses.ItemList>((Func<SharedClasses.ItemList, bool>) (y =>
            {
              int? itemId = y.itemID;
              int? inventoryId = inventoryRow.InventoryID;
              return itemId.GetValueOrDefault() == inventoryId.GetValueOrDefault() & itemId.HasValue == inventoryId.HasValue;
            }));
          if ((serviceSkills == null || ServiceSelectionHelper.CanThisServiceBeCompleteByTheseEmployeesSkills(serviceSkills, employeeSkillList)) && (serviceLicenses == null || ServiceSelectionHelper.CanThisServiceBeCompleteByTheseEmployeesLicenses(serviceLicenses, employeeLicenseList)))
            yield return (object) inventoryRow;
        }
        serviceSkillList = (List<SharedClasses.ItemList>) null;
        employeeSkillList = (List<SharedClasses.ItemList>) null;
        serviceLicenseList = (List<SharedClasses.ItemList>) null;
        employeeLicenseList = (List<SharedClasses.ItemList>) null;
      }
    }
  }

  public class ServiceRecords_View : 
    PXSelectJoin<PX.Objects.IN.InventoryItem, LeftJoin<INItemClass, On<PX.Objects.IN.InventoryItem.itemClassID, Equal<INItemClass.itemClassID>>>, Where2<Where<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.serviceItem>, And<PX.Objects.IN.InventoryItem.itemStatus, Equal<InventoryItemStatus.active>, And<FSxServiceClass.requireRoute, Equal<Current<FSSrvOrdType.requireRoute>>, And<Match<Current<AccessInfo.userName>>>>>>, And<Where<Current<ServiceSelectionFilter.serviceClassID>, IsNull, Or<INItemClass.itemClassID, Equal<Current<ServiceSelectionFilter.serviceClassID>>>>>>>
  {
    public ServiceRecords_View(PXGraph graph)
      : base(graph)
    {
    }

    public ServiceRecords_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }
}
