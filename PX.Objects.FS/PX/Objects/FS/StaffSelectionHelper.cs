// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.StaffSelectionHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.EP;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Objects.FS;

public class StaffSelectionHelper
{
  public void LaunchStaffSelector(PXGraph graph, PXFilter<StaffSelectionFilter> filter)
  {
    if (((PXSelectBase<StaffSelectionFilter>) filter).Current.PostalCode != null && !((PXSelectBase<StaffSelectionFilter>) filter).Current.GeoZoneID.HasValue)
    {
      FSGeoZonePostalCode geoZonePostalCode = StaffSelectionHelper.GetMatchingGeoZonePostalCode(graph, ((PXSelectBase<StaffSelectionFilter>) filter).Current.PostalCode);
      if (geoZonePostalCode != null)
        ((PXSelectBase<StaffSelectionFilter>) filter).Current.GeoZoneID = geoZonePostalCode.GeoZoneID;
    }
    ((PXSelectBase<StaffSelectionFilter>) filter).Current.ExistContractEmployees = this.ExistContractEmployees(((PXSelectBase) filter).Cache.Graph, ((PXSelectBase<StaffSelectionFilter>) filter).Current.ProjectID);
    ((PXSelectBase<StaffSelectionFilter>) filter).AskExt();
  }

  public static FSGeoZonePostalCode GetMatchingGeoZonePostalCode(
    PXGraph graph,
    string fullPostalCode)
  {
    return PXResult<FSGeoZonePostalCode>.op_Implicit(((IEnumerable<PXResult<FSGeoZonePostalCode>>) PXSelectBase<FSGeoZonePostalCode, PXSelectReadonly<FSGeoZonePostalCode>.Config>.Select(graph, Array.Empty<object>())).AsEnumerable<PXResult<FSGeoZonePostalCode>>().Where<PXResult<FSGeoZonePostalCode>>((Func<PXResult<FSGeoZonePostalCode>, bool>) (x => Regex.Match(fullPostalCode.Trim(), PXResult<FSGeoZonePostalCode>.op_Implicit(x).PostalCode.Trim()).Success)).FirstOrDefault<PXResult<FSGeoZonePostalCode>>());
  }

  /// <summary>
  /// Checks the existence of assigned Employees to the ProjectID.
  /// </summary>
  /// <param name="graph">PXGraph instance for BQL execution.</param>
  /// <param name="projectID">ProjectID to check the existing of Employees.</param>
  /// <returns>True if there are employees assigned to the projectID, False if not.</returns>
  private bool? ExistContractEmployees(PXGraph graph, int? projectID)
  {
    return new bool?(PXSelectBase<EPEmployeeContract, PXSelect<EPEmployeeContract, Where<EPEmployeeContract.contractID, Equal<Required<StaffSelectionFilter.projectID>>>>.Config>.Select(graph, new object[1]
    {
      (object) projectID
    }).Count > 0);
  }

  /// <summary>
  /// Evaluates if the employeeItemList (belonging to an Employee) provided has the items (skills or licenseTypes) selected in filter.
  /// </summary>
  /// <param name="employeeItemList">Employee item list instance.</param>
  /// <param name="itemsSelection">Items list selected in filter.</param>
  /// <returns>True if Employee has items Selected, otherwise False.</returns>
  private static bool HasEmployeeItemsSelected(
    SharedClasses.ItemList employeeItemList,
    List<int?> itemsSelection)
  {
    if (employeeItemList != null)
    {
      List<int?> list = employeeItemList.list.Cast<int?>().ToList<int?>();
      if (itemsSelection.Except<int?>((IEnumerable<int?>) list).Any<int?>())
        return false;
    }
    else if (itemsSelection.Count > 0)
      return false;
    return true;
  }

  /// <summary>
  /// Gets Staff Members already existing in the Staff tab with the <c>lineRef</c> related. The <c>employeesView</c> can be of type AppointmentEmployees_View or ServiceOrderEmployees_View.
  /// </summary>
  /// <param name="staffView">Object of type AppointmentEmployees_View or ServiceOrderEmployees_View.</param>
  /// <param name="lineRef">Line ref of related Service Line.</param>
  /// <returns>List of EmployeeID's existing in Employee Tab.</returns>
  private static List<int?> GetStaffByLineRefTab(object staffView, string lineRef)
  {
    List<int?> staffByLineRefTab = new List<int?>();
    if (staffView is AppointmentEntry.AppointmentServiceEmployees_View)
    {
      foreach (PXResult<FSAppointmentEmployee> pxResult in ((IEnumerable<PXResult<FSAppointmentEmployee>>) ((PXSelectBase<FSAppointmentEmployee>) staffView).Select(Array.Empty<object>())).AsEnumerable<PXResult<FSAppointmentEmployee>>().Where<PXResult<FSAppointmentEmployee>>((Func<PXResult<FSAppointmentEmployee>, bool>) (y => PXResult<FSAppointmentEmployee>.op_Implicit(y).ServiceLineRef == lineRef)))
      {
        FSAppointmentEmployee appointmentEmployee = PXResult<FSAppointmentEmployee>.op_Implicit(pxResult);
        staffByLineRefTab.Add(appointmentEmployee.EmployeeID);
      }
    }
    if (staffView is ServiceOrderEntry.ServiceOrderEmployees_View)
    {
      foreach (PXResult<FSSOEmployee> pxResult in ((IEnumerable<PXResult<FSSOEmployee>>) ((PXSelectBase<FSSOEmployee>) staffView).Select(Array.Empty<object>())).AsEnumerable<PXResult<FSSOEmployee>>().Where<PXResult<FSSOEmployee>>((Func<PXResult<FSSOEmployee>, bool>) (y => PXResult<FSSOEmployee>.op_Implicit(y).ServiceLineRef == lineRef)))
      {
        FSSOEmployee fssoEmployee = PXResult<FSSOEmployee>.op_Implicit(pxResult);
        staffByLineRefTab.Add(fssoEmployee.EmployeeID);
      }
    }
    return staffByLineRefTab;
  }

  private static IEnumerable<BAccountStaffMember> GetStaffAvailableForSelect(
    PXFilter<StaffSelectionFilter> filter,
    object staffView)
  {
    List<int?> staffIDList = StaffSelectionHelper.GetStaffByLineRefTab(staffView, ((PXSelectBase<StaffSelectionFilter>) filter).Current.ServiceLineRef);
    foreach (BAccountStaffMember baccountStaffMember in ((IEnumerable<PXResult<BAccountStaffMember>>) ((PXSelectBase<BAccountStaffMember>) new StaffSelectionHelper.StaffRecords_View(((PXSelectBase) filter).Cache.Graph)).Select(Array.Empty<object>())).AsEnumerable<PXResult<BAccountStaffMember>>().GroupBy((Func<PXResult<BAccountStaffMember>, int?>) (p => PXResult<BAccountStaffMember>.op_Implicit(p).BAccountID), (key, group) => new
    {
      Group = PXResult<BAccountStaffMember>.op_Implicit(group.First<PXResult<BAccountStaffMember>>())
    }).Select(g => g.Group).ToList<BAccountStaffMember>())
    {
      BAccountStaffMember staffMemberRow = baccountStaffMember;
      if (staffIDList.Exists((Predicate<int?>) (staffMemberID =>
      {
        int? nullable = staffMemberID;
        int? baccountId = staffMemberRow.BAccountID;
        return nullable.GetValueOrDefault() == baccountId.GetValueOrDefault() & nullable.HasValue == baccountId.HasValue;
      })))
        staffMemberRow.Selected = new bool?(true);
      else
        staffMemberRow.Selected = new bool?(false);
      yield return staffMemberRow;
    }
  }

  public static IEnumerable SkillFilterDelegate<ServiceDetType>(
    PXGraph graph,
    PXSelectBase<ServiceDetType> servicesView,
    PXFilter<StaffSelectionFilter> filter,
    PXSelectBase<SkillGridFilter> skillView)
    where ServiceDetType : class, IBqlTable, IFSSODetBase, new()
  {
    List<int?> serviceIDList = new List<int?>();
    List<int?> serviceIDCheckList = new List<int?>();
    bool initSkillFilter = ((PXSelectBase) skillView).Cache.Cached.Cast<SkillGridFilter>().Count<SkillGridFilter>() == 0;
    if (initSkillFilter)
    {
      serviceIDList = ServiceSelectionHelper.GetServicesInServiceTab<ServiceDetType>(servicesView, (string) null);
      serviceIDCheckList = ServiceSelectionHelper.GetServicesInServiceTab<ServiceDetType>(servicesView, ((PXSelectBase<StaffSelectionFilter>) filter).Current.ServiceLineRef);
    }
    foreach (PXResult<SkillGridFilter> pxResult in PXSelectBase<SkillGridFilter, PXSelect<SkillGridFilter>.Config>.Select(graph, Array.Empty<object>()))
    {
      SkillGridFilter skillGridFilter = PXResult<SkillGridFilter>.op_Implicit(pxResult);
      if (initSkillFilter)
      {
        skillGridFilter.Mem_ServicesList = SkillGridFilter.GetServiceListField(graph, serviceIDList, skillGridFilter.SkillID);
        string serviceListField = SkillGridFilter.GetServiceListField(graph, serviceIDCheckList, skillGridFilter.SkillID);
        skillGridFilter.Mem_Selected = new bool?(!string.IsNullOrEmpty(serviceListField));
      }
      ((PXSelectBase) skillView).Cache.SetStatus((object) skillGridFilter, (PXEntryStatus) 5);
      ((PXSelectBase) skillView).Cache.IsDirty = false;
      yield return (object) skillGridFilter;
    }
  }

  public static IEnumerable LicenseTypeFilterDelegate<ServiceDetType>(
    PXGraph graph,
    PXSelectBase<ServiceDetType> servicesView,
    PXFilter<StaffSelectionFilter> filter,
    PXSelectBase<LicenseTypeGridFilter> licenseTypeView)
    where ServiceDetType : class, IBqlTable, IFSSODetBase, new()
  {
    List<int?> serviceIDList = new List<int?>();
    bool initLicenseTypeFilter = ((PXSelectBase) licenseTypeView).Cache.Cached.Cast<LicenseTypeGridFilter>().Count<LicenseTypeGridFilter>() == 0;
    if (initLicenseTypeFilter)
      serviceIDList = ServiceSelectionHelper.GetServicesInServiceTab<ServiceDetType>(servicesView, ((PXSelectBase<StaffSelectionFilter>) filter).Current.ServiceLineRef);
    foreach (PXResult<LicenseTypeGridFilter> pxResult in PXSelectBase<LicenseTypeGridFilter, PXSelect<LicenseTypeGridFilter>.Config>.Select(graph, Array.Empty<object>()))
    {
      LicenseTypeGridFilter licenseTypeGridFilter = PXResult<LicenseTypeGridFilter>.op_Implicit(pxResult);
      if (initLicenseTypeFilter)
      {
        licenseTypeGridFilter.Mem_FromService = new bool?(LicenseTypeGridFilter.IsThisLicenseTypeRequiredByAnyService(graph, licenseTypeGridFilter.LicenseTypeID, serviceIDList));
        licenseTypeGridFilter.Mem_Selected = new bool?(licenseTypeGridFilter.Mem_FromService.GetValueOrDefault());
      }
      ((PXSelectBase) licenseTypeView).Cache.SetStatus((object) licenseTypeGridFilter, (PXEntryStatus) 5);
      ((PXSelectBase) licenseTypeView).Cache.IsDirty = false;
      yield return (object) licenseTypeGridFilter;
    }
  }

  public static IEnumerable StaffRecordsDelegate(
    object staffView,
    PXSelectBase<SkillGridFilter> skillView,
    PXSelectBase<LicenseTypeGridFilter> licenseTypeView,
    PXFilter<StaffSelectionFilter> filter)
  {
    if (((PXSelectBase<StaffSelectionFilter>) filter).Current != null)
    {
      PXGraph graph = ((PXSelectBase) filter).Cache.Graph;
      IEnumerable<BAccountStaffMember> availableForSelect = StaffSelectionHelper.GetStaffAvailableForSelect(filter, staffView);
      List<int?> skillsSelection = ((IQueryable<PXResult<SkillGridFilter>>) skillView.Select(Array.Empty<object>())).Where<PXResult<SkillGridFilter>>((Expression<Func<PXResult<SkillGridFilter>, bool>>) (y => ((SkillGridFilter) y).Mem_Selected == (bool?) true)).Select<PXResult<SkillGridFilter>, int?>((Expression<Func<PXResult<SkillGridFilter>, int?>>) (y => ((SkillGridFilter) y).SkillID)).ToList<int?>();
      List<int?> licenseTypesSelection = ((IQueryable<PXResult<LicenseTypeGridFilter>>) licenseTypeView.Select(Array.Empty<object>())).Where<PXResult<LicenseTypeGridFilter>>((Expression<Func<PXResult<LicenseTypeGridFilter>, bool>>) (y => ((LicenseTypeGridFilter) y).Mem_Selected == (bool?) true)).Select<PXResult<LicenseTypeGridFilter>, int?>((Expression<Func<PXResult<LicenseTypeGridFilter>, int?>>) (y => ((LicenseTypeGridFilter) y).LicenseTypeID)).ToList<int?>();
      if (skillsSelection.Count == 0 && licenseTypesSelection.Count == 0)
      {
        foreach (object obj in availableForSelect)
          yield return obj;
      }
      else
      {
        List<int?> list1 = availableForSelect.Select<BAccountStaffMember, int?>((Func<BAccountStaffMember, int?>) (y => y.BAccountID)).ToList<int?>();
        int count1 = 100;
        List<SharedClasses.ItemList> allStaffSkillList = new List<SharedClasses.ItemList>();
        List<SharedClasses.ItemList> allStaffLicenseTypeList = new List<SharedClasses.ItemList>();
        ((PXSelectBase<StaffSelectionFilter>) filter).Current.ScheduledDateTimeBegin = SharedFunctions.RemoveTimeInfo(((PXSelectBase<StaffSelectionFilter>) filter).Current.ScheduledDateTimeBegin);
        for (int count2 = 0; count2 < list1.Count; count2 += count1)
        {
          List<int?> list2 = list1.Skip<int?>(count2).Take<int?>(count1).ToList<int?>();
          allStaffSkillList.AddRange((IEnumerable<SharedClasses.ItemList>) SharedFunctions.GetItemWithList<FSEmployeeSkill, FSEmployeeSkill.employeeID, FSEmployeeSkill.skillID>(graph, list2));
          allStaffLicenseTypeList.AddRange((IEnumerable<SharedClasses.ItemList>) SharedFunctions.GetItemWithList<FSLicense, FSLicense.employeeID, FSLicense.licenseTypeID, Where2<Where<Current<StaffSelectionFilter.scheduledDateTimeBegin>, IsNull>, Or<Where<FSLicense.issueDate, LessEqual<Current<StaffSelectionFilter.scheduledDateTimeBegin>>, And<Where<FSLicense.expirationDate, GreaterEqual<Current<StaffSelectionFilter.scheduledDateTimeBegin>>, Or<FSLicense.expirationDate, IsNull>>>>>>>(graph, list2));
        }
        foreach (BAccountStaffMember baccountStaffMember in availableForSelect)
        {
          BAccountStaffMember staffRow = baccountStaffMember;
          SharedClasses.ItemList employeeItemList1 = allStaffSkillList.FirstOrDefault<SharedClasses.ItemList>((Func<SharedClasses.ItemList, bool>) (y =>
          {
            int? itemId = y.itemID;
            int? baccountId = staffRow.BAccountID;
            return itemId.GetValueOrDefault() == baccountId.GetValueOrDefault() & itemId.HasValue == baccountId.HasValue;
          }));
          SharedClasses.ItemList employeeItemList2 = allStaffLicenseTypeList.FirstOrDefault<SharedClasses.ItemList>((Func<SharedClasses.ItemList, bool>) (y =>
          {
            int? itemId = y.itemID;
            int? baccountId = staffRow.BAccountID;
            return itemId.GetValueOrDefault() == baccountId.GetValueOrDefault() & itemId.HasValue == baccountId.HasValue;
          }));
          List<int?> itemsSelection = skillsSelection;
          bool? selected;
          if (!StaffSelectionHelper.HasEmployeeItemsSelected(employeeItemList1, itemsSelection))
          {
            selected = staffRow.Selected;
            bool flag = false;
            if (selected.GetValueOrDefault() == flag & selected.HasValue)
              continue;
          }
          if (!StaffSelectionHelper.HasEmployeeItemsSelected(employeeItemList2, licenseTypesSelection))
          {
            selected = staffRow.Selected;
            bool flag = false;
            if (selected.GetValueOrDefault() == flag & selected.HasValue)
              continue;
          }
          yield return (object) staffRow;
        }
        allStaffSkillList = (List<SharedClasses.ItemList>) null;
        allStaffLicenseTypeList = (List<SharedClasses.ItemList>) null;
      }
    }
  }

  public class StaffRecords_View : 
    PXSelectJoin<BAccountStaffMember, LeftJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<BAccountStaffMember.bAccountID>, And<PX.Objects.AP.Vendor.vStatus, NotEqual<VendorStatus.inactive>>>, LeftJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<BAccountStaffMember.bAccountID>, And<EPEmployee.vStatus, NotEqual<VendorStatus.inactive>>>, LeftJoin<FSGeoZoneEmp, On<FSGeoZoneEmp.employeeID, Equal<BAccountStaffMember.bAccountID>>, LeftJoin<PMProject, On<PMProject.contractID, Equal<Current<FSServiceOrder.projectID>>>, LeftJoin<EPEmployeeContract, On<EPEmployeeContract.contractID, Equal<PMProject.contractID>, And<EPEmployeeContract.employeeID, Equal<BAccountStaffMember.bAccountID>>>, LeftJoin<EPEmployeePosition, On<EPEmployeePosition.employeeID, Equal<EPEmployee.bAccountID>, And<EPEmployeePosition.isActive, Equal<True>>>>>>>>>, Where<PMProject.isActive, Equal<True>, And<PMProject.baseType, Equal<CTPRType.project>, And<Where2<Where<CurrentValue<StaffSelectionFilter.geoZoneID>, IsNull, Or<FSGeoZoneEmp.geoZoneID, Equal<Current<StaffSelectionFilter.geoZoneID>>>>, And<Where2<Where<FSxVendor.sDEnabled, Equal<True>, And<Where2<Where2<Not<FeatureInstalled<FeaturesSet.visibilityRestriction>>, Or<PX.Objects.AP.Vendor.vOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>, And<PX.Objects.AP.Vendor.vStatus, Equal<VendorStatus.active>, Or<PX.Objects.AP.Vendor.vStatus, Equal<VendorStatus.oneTime>>>>>>, Or<Where<FSxEPEmployee.sDEnabled, Equal<True>, And<Where<PMProject.restrictToEmployeeList, Equal<False>, Or<Where<PMProject.restrictToEmployeeList, Equal<True>, And<EPEmployeeContract.employeeID, IsNotNull>>>>>>>>>>>>>, OrderBy<Asc<BAccountSelectorBase.acctCD, Asc<FSGeoZoneEmp.geoZoneID>>>>
  {
    public StaffRecords_View(PXGraph graph)
      : base(graph)
    {
    }

    public StaffRecords_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class SkillRecords_View : 
    PXSelectOrderBy<SkillGridFilter, OrderBy<Desc<SkillGridFilter.mem_ServicesList>>>
  {
    public SkillRecords_View(PXGraph graph)
      : base(graph)
    {
    }

    public SkillRecords_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class LicenseTypeRecords_View : 
    PXSelectOrderBy<LicenseTypeGridFilter, OrderBy<Desc<LicenseTypeGridFilter.mem_FromService>>>
  {
    public LicenseTypeRecords_View(PXGraph graph)
      : base(graph)
    {
    }

    public LicenseTypeRecords_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }
}
