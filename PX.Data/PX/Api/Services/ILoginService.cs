// Decompiled with JetBrains decompiler
// Type: PX.Api.Services.ILoginService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Security.Principal;

#nullable disable
namespace PX.Api.Services;

[PXInternalUseOnly]
public interface ILoginService
{
  void Login(
    string login,
    string password,
    string company = null,
    string branch = null,
    string locale = null,
    string prefix = "");

  void Logout();

  bool IsUserAuthenticated(bool throwException, string prefix = "");

  PXLoginScope GetAdminLoginScope(string company = null);

  string[] GetCompanies(string login, string password);

  string[] GetCompanies();

  string GetLoginWithCompany(string login, string company, string branch, string password = "");

  IEnumerable<BranchMeta> GetBranches();

  IEnumerable<object> GetBranchTree();

  bool IsMultiCompany { get; }

  IPrincipal TryLoginUser(
    string login,
    string password,
    string company,
    string branch,
    string locale);

  bool ValidateUser(string login);

  void InitUserEnvironment(string login);

  void InitUserEnvironment(string login, string locale);

  /// <returns>Current user's company name when multicompany or default (the only) company when singlecompany</returns>
  string GetCurrentCompany();

  /// <returns>Current user's company name when multicompany or default (the only) company when singlecompany</returns>
  string TryGetCurrentCompany();

  IReadOnlyList<(int companyId, Guid userId, int twoFactorLevel, bool passwordChange)> GetUserIdsWithTwoFactorType(
    string userName,
    string password);

  Dictionary<string, string> GetBranchThemeVariables(string branchCD);
}
