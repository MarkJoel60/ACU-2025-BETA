// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXCompanyHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.BulkInsert.Installer.DatabaseSetup;
using PX.Common;
using PX.DbServices.Model.Entities;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.QueryObjectModel;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Update;

public static class PXCompanyHelper
{
  private const string IsTestTenantSlotName = "IsTestTenant";

  public static IEnumerable<UPCompany> SelectCompanies(
    PXCompanySelectOptions option = PXCompanySelectOptions.Visible,
    bool includeMarkedForDeletion = false)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return PXDatabase.Provider.SelectCompanies(option, includeMarkedForDeletion).Select<CompanyInfo, UPCompany>(PXCompanyHelper.\u003C\u003EO.\u003C0\u003E__Convert ?? (PXCompanyHelper.\u003C\u003EO.\u003C0\u003E__Convert = new Func<CompanyInfo, UPCompany>(PXCompanyHelper.Convert)));
  }

  private static IEnumerable<CompanyInfo> SelectAllCompanyInfos(
    this PXDatabaseProvider databaseProvider,
    bool includeMarkedForDeletion = false)
  {
    return (IEnumerable<CompanyInfo>) databaseProvider.SelectCompanies(true, includeMarkedForDeletion) ?? Enumerable.Empty<CompanyInfo>();
  }

  private static IEnumerable<CompanyInfo> SelectPositiveCompanyInfos(
    this PXDatabaseProvider databaseProvider,
    bool includeMarkedForDeletion = false)
  {
    return (IEnumerable<CompanyInfo>) databaseProvider.SelectCompanies(false, includeMarkedForDeletion) ?? Enumerable.Empty<CompanyInfo>();
  }

  private static IEnumerable<CompanyInfo> FilterCompanyInfos(
    this IEnumerable<CompanyInfo> companies,
    PXCompanySelectOptions option)
  {
    string[] accessibleCompanies = PXAccess.GetCompaniesUnrestricted();
    if (option == PXCompanySelectOptions.Visible)
      return companies.Where<CompanyInfo>((Func<CompanyInfo, bool>) (info =>
      {
        if (((IEnumerable<string>) accessibleCompanies).Contains<string>(info.LoginName))
          return true;
        return accessibleCompanies.Length == 0 && info.Joined;
      }));
    if (option == PXCompanySelectOptions.Accessible)
      return companies.Where<CompanyInfo>((Func<CompanyInfo, bool>) (info => ((IEnumerable<string>) accessibleCompanies).Contains<string>(info.LoginName) || accessibleCompanies.Length == 0 && info.Joined || info.System));
    throw new ArgumentOutOfRangeException(nameof (option));
  }

  internal static IEnumerable<CompanyInfo> SelectCompanies(
    this PXDatabaseProvider databaseProvider,
    PXCompanySelectOptions option,
    bool includeMarkedForDeletion = false)
  {
    switch (option)
    {
      case PXCompanySelectOptions.Visible:
      case PXCompanySelectOptions.Accessible:
        return databaseProvider.SelectPositiveCompanyInfos(includeMarkedForDeletion).FilterCompanyInfos(option);
      case PXCompanySelectOptions.Positive:
        return databaseProvider.SelectPositiveCompanyInfos(includeMarkedForDeletion);
      case PXCompanySelectOptions.Negative:
        return databaseProvider.SelectAllCompanyInfos(includeMarkedForDeletion).Where<CompanyInfo>((Func<CompanyInfo, bool>) (info => info.CompanyID < 0));
      case PXCompanySelectOptions.All:
        return databaseProvider.SelectAllCompanyInfos(includeMarkedForDeletion);
      default:
        return Enumerable.Empty<CompanyInfo>();
    }
  }

  public static IEnumerable<UPCompany> SelectVisibleCompaniesIncludeWithoutUsers()
  {
    foreach (UPCompany selectCompany in PXCompanyHelper.SelectCompanies())
      yield return selectCompany;
    List<int?> companyIdsWithoutUsers = PXCompanyHelper.GetCompanyIdsWithoutUsers();
    foreach (UPCompany upCompany in PXCompanyHelper.SelectCompanies(PXCompanySelectOptions.Positive).Where<UPCompany>((Func<UPCompany, bool>) (x => !x.System.GetValueOrDefault() && companyIdsWithoutUsers.Contains(x.CompanyID))))
      yield return upCompany;
  }

  private static List<int?> GetCompanyIdsWithoutUsers()
  {
    YaqlTableQuery yaqlTableQuery = new YaqlTableQuery((YaqlTable) Yaql.schemaTable("Company", (string) null), Yaql.join("Users", Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column("CompanyID", "Company"), Yaql.column("CompanyID", "Users")), (YaqlJoinType) 1), (string) null);
    ((YaqlQueryBase) yaqlTableQuery).where(Yaql.isNull((YaqlScalar) Yaql.column("CompanyID", "Users")));
    yaqlTableQuery.Columns.Add(YaqlScalarAlilased.op_Implicit(Yaql.column("CompanyID", "Company")));
    PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint();
    TableColumn columnByName = dbServicesPoint.Schema.GetTable("Company").getColumnByName("CompanyID");
    return dbServicesPoint.selectTable(yaqlTableQuery, new List<TableColumn>()
    {
      columnByName
    }, (SqlGenerationOptions) null).Select<object[], int?>((Func<object[], int?>) (x => x[0] as int?)).ToList<int?>();
  }

  internal static bool IsNotVisibleCompany(int? companyID)
  {
    return !PXCompanyHelper.SelectCompanies().Select<UPCompany, int?>((Func<UPCompany, int?>) (x => x.CompanyID)).Contains<int?>(companyID);
  }

  public static UPCompany FindCompany(int companyID)
  {
    return PXCompanyHelper.FindCompany(PXCompanySelectOptions.Visible, companyID);
  }

  public static UPCompany FindCompany(PXCompanySelectOptions option, int companyID)
  {
    return PXCompanyHelper.SelectCompanies(option).FirstOrDefault<UPCompany>((Func<UPCompany, bool>) (c =>
    {
      int? companyId = c.CompanyID;
      int num = companyID;
      return companyId.GetValueOrDefault() == num & companyId.HasValue;
    }));
  }

  public static IEnumerable<int> GetParentCompanies(int? companyID)
  {
    IEnumerable<UPCompany> source = PXCompanyHelper.SelectCompanies(PXCompanySelectOptions.Positive);
    List<int> parentCompanies = new List<int>();
    while (companyID.HasValue)
    {
      companyID = (int?) source.FirstOrDefault<UPCompany>((Func<UPCompany, bool>) (c =>
      {
        int? companyId = c.CompanyID;
        int? nullable = companyID;
        return companyId.GetValueOrDefault() == nullable.GetValueOrDefault() & companyId.HasValue == nullable.HasValue;
      }))?.ParentID;
      if (companyID.HasValue)
        parentCompanies.Add(companyID.Value);
    }
    return (IEnumerable<int>) parentCompanies;
  }

  public static UPCompany Convert(CompanyInfo info)
  {
    return PXCompanyHelper.Convert(new UPCompany(), info);
  }

  public static UPCompany Convert(UPCompany company, CompanyInfo info)
  {
    company.Active = new bool?(info.Joined);
    company.CompanyCD = info.CompanyCD;
    company.CompanyID = new int?(info.CompanyID);
    company.DataType = info.CurrentDataType.Name;
    company.Hidden = new bool?(info.Hidden);
    company.LoginName = info.LoginName;
    company.ParentID = info.ParentID == -1 ? new int?() : new int?(info.ParentID);
    company.Sequence = info.Sequence;
    company.Size = info.Size;
    return company;
  }

  public static IEnumerable<CompanyInfo> LoadCompanies(DatabasePayloadReader helper, bool newDb)
  {
    if (newDb)
      return PXCompanyHelper.DefaultNewBase(helper);
    List<CompanyInfo> source = new List<CompanyInfo>();
    source.AddRange((IEnumerable<CompanyInfo>) PXDatabase.SelectCompanies());
    foreach (CompanyInfo companyInfo in source)
    {
      if (companyInfo.System)
        companyInfo.Joined = false;
    }
    if (source.Where<CompanyInfo>((Func<CompanyInfo, bool>) (c => c.Joined)).All<CompanyInfo>((Func<CompanyInfo, bool>) (c => string.IsNullOrEmpty(c.LoginName))))
    {
      foreach (CompanyInfo companyInfo in source)
      {
        if (string.IsNullOrEmpty(companyInfo.LoginName) && !companyInfo.CurrentDataType.Hidden)
          companyInfo.LoginName = "Company" + companyInfo.CompanyID.ToString();
      }
    }
    foreach (CompanyInfo companyInfo in source)
    {
      DataTypeInfo currentDataType = companyInfo.CurrentDataType;
      if (currentDataType != null)
      {
        DataTypeInfo itemByCode = helper.DataTypes.GetItemByCode(currentDataType.Name);
        if (itemByCode != null)
        {
          currentDataType.Execution = itemByCode.Execution;
          currentDataType.Hidden = itemByCode.Hidden;
        }
      }
    }
    return (IEnumerable<CompanyInfo>) source;
  }

  private static IEnumerable<CompanyInfo> DefaultNewBase(DatabasePayloadReader helper)
  {
    List<CompanyInfo> allcompanies = new List<CompanyInfo>();
    if (!helper.DefaultDatabase.Any<KeyValuePair<int, DefaultCompanyInfo>>())
      return PXCompanyHelper.GenerateNewBase(helper);
    int num = 1;
    foreach (DefaultCompanyInfo defaultCompanyInfo in helper.DefaultDatabase.Values)
    {
      DataTypeInfo dataType = ((Dictionary<string, DataTypeInfo>) helper.DataTypes).TryGetValue(defaultCompanyInfo.DataCode, out dataType) ? dataType : helper.DataTypes.UserCompany;
      CompanyInfo companyInfo = new CompanyInfo(dataType, num++);
      if ((dataType.Execution & 1) != 1)
        companyInfo.DataType = dataType;
      companyInfo.CompanyCD = companyInfo.LoginName = PXCompanyHelper.GetNewCompanyName(defaultCompanyInfo.Name, (IEnumerable<CompanyInfo>) allcompanies);
      companyInfo.ParentID = defaultCompanyInfo.Parent ?? -1;
      companyInfo.Exist = companyInfo.CurrentDataType.Hidden;
      companyInfo.Joined = !companyInfo.CurrentDataType.Hidden;
      companyInfo.LoginName = !companyInfo.CurrentDataType.Hidden ? defaultCompanyInfo.Name : string.Empty;
      companyInfo.Hidden = dataType.Hidden;
      allcompanies.Add(companyInfo);
    }
    return (IEnumerable<CompanyInfo>) allcompanies;
  }

  private static IEnumerable<CompanyInfo> GenerateNewBase(DatabasePayloadReader helper)
  {
    List<CompanyInfo> newBase = new List<CompanyInfo>();
    newBase.Add(new CompanyInfo(helper.DataTypes.SystemCompany, 1)
    {
      CompanyID = 1,
      CompanyCD = "1",
      ParentID = -1,
      Exist = true,
      Joined = false,
      Hidden = true,
      LoginName = "System"
    });
    Dictionary<int, string> loginCompanies = PXDatabase.Provider.GetLoginCompanies();
    if (loginCompanies.Count == 0)
      loginCompanies.Add(2, (string) null);
    newBase.AddRange(loginCompanies.Select<KeyValuePair<int, string>, CompanyInfo>((Func<KeyValuePair<int, string>, CompanyInfo>) (companyDef => new CompanyInfo(helper.DataTypes.UserCompany, 2)
    {
      CompanyID = companyDef.Key,
      CompanyCD = string.IsNullOrEmpty(companyDef.Value) ? "Company" + companyDef.Key.ToString() : companyDef.Value,
      LoginName = string.IsNullOrEmpty(companyDef.Value) ? "Company" + companyDef.Key.ToString() : companyDef.Value,
      ParentID = 1,
      Exist = false,
      Joined = true
    })));
    return (IEnumerable<CompanyInfo>) newBase;
  }

  private static string GetNewCompanyName(string defaultName, IEnumerable<CompanyInfo> allcompanies)
  {
    string name = defaultName ?? "Company";
    IEnumerable<int> source = allcompanies.Where<CompanyInfo>((Func<CompanyInfo, bool>) (ci => ci != null)).Select<CompanyInfo, int>((Func<CompanyInfo, int>) (ci => ci.CompanyID));
    int num = source.Count<int>() > 0 ? source.Max() : 1;
    while (allcompanies.Any<CompanyInfo>((Func<CompanyInfo, bool>) (c => c.LoginName == name)))
    {
      name = (defaultName ?? "Company") + num.ToString();
      ++num;
    }
    return name;
  }

  internal static bool IsTestTenant()
  {
    bool? slot = PXContext.GetSlot<bool?>(nameof (IsTestTenant));
    if (slot.HasValue)
      return slot.Value;
    int companyId = PXInstanceHelper.CurrentCompany;
    bool flag = string.Equals(PXCompanyHelper.SelectCompanies(PXCompanySelectOptions.Positive).FirstOrDefault<UPCompany>((Func<UPCompany, bool>) (c =>
    {
      int? companyId1 = c.CompanyID;
      int num = companyId;
      return companyId1.GetValueOrDefault() == num & companyId1.HasValue;
    }))?.DataType, PXDataTypesHelper.TrialCompany.Name, StringComparison.OrdinalIgnoreCase);
    PXContext.SetSlot<bool?>(nameof (IsTestTenant), new bool?(flag));
    return flag;
  }
}
