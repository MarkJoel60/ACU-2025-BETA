// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.CompanyService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.BulkInsert;
using PX.Common;
using PX.Common.Context;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.QueryObjectModel;
using PX.SM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Services;

/// <exclude />
public class CompanyService : ICompanyService
{
  private readonly ICurrentUserInformationProvider _currentUserInformationProvider;
  private int? _cid;
  private DbmsMaintenance _maintProvider;
  private Users _user;
  private PreferencesSecurity _preferences;
  private Dictionary<string, List<RoleClaims>> _claims;
  private Dictionary<string, List<RoleActiveDirectory>> _directories;

  public CompanyService(
    ICurrentUserInformationProvider currentUserInformationProvider)
  {
    this._currentUserInformationProvider = currentUserInformationProvider;
  }

  public virtual void PrepareGeneralInfo(IEnumerable<int> companies)
  {
    PXDatabase.Provider.CreateDbServicesPoint();
    this._maintProvider = PXDatabase.Provider.GetMaintenance();
    this._cid = SlotStore.Instance.GetSingleCompanyId();
    this._maintProvider.AdjustCompanyMaskWidth(companies);
  }

  public virtual void PrepareCompanyInfo()
  {
    this._user = (Users) PXSelectBase<Users, PXSelectReadonly<Users, Where<Users.pKID, Equal<Required<Users.pKID>>>>.Config>.Select(new PXGraph(), (object) this._currentUserInformationProvider.GetUserIdOrDefault());
    this._preferences = (PreferencesSecurity) PXSelectBase<PreferencesSecurity, PXSelectReadonly<PreferencesSecurity>.Config>.Select(new PXGraph());
    this._preferences.DefaultMenuEditorRole = PXAccess.GetAdministratorRole();
    this._claims = new Dictionary<string, List<RoleClaims>>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
    foreach (PXResult<RoleClaims> pxResult in PXSelectBase<RoleClaims, PXSelectReadonly<RoleClaims>.Config>.Select(new PXGraph()))
    {
      RoleClaims roleClaims = (RoleClaims) pxResult;
      if (!string.IsNullOrEmpty(roleClaims.Role))
      {
        List<RoleClaims> roleClaimsList;
        if (!this._claims.TryGetValue(roleClaims.Role, out roleClaimsList))
          this._claims[roleClaims.Role] = roleClaimsList = new List<RoleClaims>();
        roleClaimsList.Add(roleClaims);
      }
    }
    this._directories = new Dictionary<string, List<RoleActiveDirectory>>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
    foreach (PXResult<RoleActiveDirectory> pxResult in PXSelectBase<RoleActiveDirectory, PXSelectReadonly<RoleActiveDirectory>.Config>.Select(new PXGraph()))
    {
      RoleActiveDirectory roleActiveDirectory = (RoleActiveDirectory) pxResult;
      if (!string.IsNullOrEmpty(roleActiveDirectory.Role))
      {
        List<RoleActiveDirectory> roleActiveDirectoryList;
        if (!this._directories.TryGetValue(roleActiveDirectory.Role, out roleActiveDirectoryList))
          this._directories[roleActiveDirectory.Role] = roleActiveDirectoryList = new List<RoleActiveDirectory>();
        roleActiveDirectoryList.Add(roleActiveDirectory);
      }
    }
  }

  public virtual void PersistCompanyInfo(UPCompany company, bool disableSchedulers)
  {
    CompanyMaint.PersistUser(company, this._user, this._preferences, disableSchedulers, this._maintProvider, this._claims, this._directories);
  }

  public virtual void AfterCompaniesPersist()
  {
    if (this._cid.HasValue)
      SlotStore.Instance.SetSingleCompanyId(this._cid.GetValueOrDefault());
    this._maintProvider.CorrectCompanyMask(new int?());
    PXDatabase.ResetSlots();
    PXDatabase.Provider.GetMaintenance().ReinitialiseCompanies();
    PXDatabase.ClearCompanyCache();
  }

  [PXInternalUseOnly]
  public List<string> GetOtherCompanyNamesWithEnabledFeature(int currentCompanyID, string feature)
  {
    PointDbmsBase currentTransaction = PXDatabase.Provider.CreateDbServicesPointWithCurrentTransaction();
    string str1 = "fs";
    string str2 = "c";
    YaqlVectorQuery yaqlVectorQuery1 = new YaqlVectorQuery((YaqlTable) Yaql.schemaTable("FeaturesSet", str1), (YaqlJoin[]) new YaqlJoinCommon[1]
    {
      Yaql.innerJoin((YaqlTable) Yaql.schemaTable("Company", str2), Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column("CompanyID", str1), Yaql.column("CompanyID", str2)))
    });
    ((YaqlQueryBase) yaqlVectorQuery1).Condition = Yaql.and(Yaql.ne<int>((YaqlScalar) Yaql.column("CompanyID", str1), currentCompanyID), Yaql.eq<YaqlScalar>((YaqlScalar) Yaql.column(feature, str1), Yaql.@true));
    yaqlVectorQuery1.Column = YaqlScalarAlilased.op_Implicit(Yaql.column("CompanyCD", str2));
    ((YaqlQueryBase) yaqlVectorQuery1).Distinct = true;
    YaqlVectorQuery yaqlVectorQuery2 = yaqlVectorQuery1;
    return currentTransaction.selectVector(yaqlVectorQuery2, false);
  }

  [PXInternalUseOnly]
  public List<string> GetOtherCompanyNamesHavingAnyData(int currentCompanyID, string table)
  {
    PointDbmsBase currentTransaction = PXDatabase.Provider.CreateDbServicesPointWithCurrentTransaction();
    string str1 = "c";
    string str2 = "t";
    YaqlVectorQuery yaqlVectorQuery1 = new YaqlVectorQuery((YaqlTable) Yaql.schemaTable(table, str2), (YaqlJoin[]) new YaqlJoinCommon[1]
    {
      Yaql.innerJoin((YaqlTable) Yaql.schemaTable("Company", str1), Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column("CompanyID", str2), Yaql.column("CompanyID", str1)))
    });
    ((YaqlQueryBase) yaqlVectorQuery1).Condition = Yaql.ne<int>((YaqlScalar) Yaql.column("CompanyID", str2), currentCompanyID);
    yaqlVectorQuery1.Column = YaqlScalarAlilased.op_Implicit(Yaql.column("CompanyCD", str1));
    ((YaqlQueryBase) yaqlVectorQuery1).Distinct = true;
    YaqlVectorQuery yaqlVectorQuery2 = yaqlVectorQuery1;
    return currentTransaction.selectVector(yaqlVectorQuery2, false);
  }
}
