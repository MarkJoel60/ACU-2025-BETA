// Decompiled with JetBrains decompiler
// Type: PX.Data.LegacyCompanyService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

internal class LegacyCompanyService : ILegacyCompanyService
{
  private readonly Func<PXDatabaseProvider> _providerAccessor;

  public LegacyCompanyService(Func<PXDatabaseProvider> providerAccessor)
  {
    this._providerAccessor = providerAccessor;
  }

  void ILegacyCompanyService.ParseLogin(
    string login,
    out string username,
    out string company,
    out string branch)
  {
    CompanyService.ParseLogin(login, out username, out company, out branch, (Func<string, bool>) (c => LegacyCompanyService.IsInvalidCompany(this._providerAccessor(), c)));
  }

  [Obsolete("Use ILegacyCompanyService")]
  internal static void ParseLogin(
    PXDatabaseProvider databaseProvider,
    string login,
    out string username,
    out string company,
    out string branch)
  {
    CompanyService.ParseLogin(login, out username, out company, out branch, (Func<string, bool>) (c => LegacyCompanyService.IsInvalidCompany(databaseProvider, c)));
  }

  [Obsolete("Use ILegacyCompanyService")]
  internal static void ParseLogin(
    string login,
    out string username,
    out string company,
    out string branch)
  {
    CompanyService.ParseLogin(login, out username, out company, out branch, (Func<string, bool>) (c => LegacyCompanyService.IsInvalidCompany(PXDatabase.Provider, c)));
  }

  private static bool IsInvalidCompany(PXDatabaseProvider databaseProvider, string company)
  {
    string[] companies = databaseProvider.Companies;
    return companies == null || companies.Length == 0 || !((IEnumerable<string>) companies).Contains<string>(company, (IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
  }

  internal static string ConcatCompanyAndBranch(string company, string branch)
  {
    if (!string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(branch))
      return $"{company}:{branch}";
    return !string.IsNullOrEmpty(company) ? company : branch;
  }

  internal static string ConcatLogin(string username, string company)
  {
    return string.IsNullOrEmpty(username) || string.IsNullOrEmpty(company) ? username : $"{username}@{company}";
  }

  internal static string ConcatLogin(string username, string company, string branch)
  {
    string str = LegacyCompanyService.ConcatLogin(username, company);
    return string.IsNullOrEmpty(str) || string.IsNullOrEmpty(branch) ? str : $"{str}:{branch}";
  }
}
