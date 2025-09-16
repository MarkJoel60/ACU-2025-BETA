// Decompiled with JetBrains decompiler
// Type: PX.Api.Services.CompanyService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Monads;

#nullable disable
namespace PX.Api.Services;

internal class CompanyService : ICompanyService
{
  public IEqualityComparer<string> CompanyComparer
  {
    get => (IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase;
  }

  private static PXDatabaseProvider _databaseProvider => PXDatabase.Provider;

  public bool IsMultiCompany => CompanyService._databaseProvider.Companies.Length != 0;

  private IEnumerable<string> SelectCompanyLoginNames()
  {
    using (new PXConnectionScope())
    {
      foreach (CompanyInfo selectCompany in CompanyService._databaseProvider.SelectCompanies(PXCompanySelectOptions.Visible))
        yield return selectCompany.LoginName;
    }
  }

  string ICompanyService.GetSingleCompanyLoginName()
  {
    if (this.IsMultiCompany)
      throw new InvalidOperationException("Can't get single company name while in multicompany mode");
    return this.SingleCompanyLoginName();
  }

  private string SingleCompanyLoginName()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return ArgumentCheck.CheckNull<string>(MaybeObjects.IfNot<string>(this.SelectCompanyLoginNames().First<string>(), CompanyService.\u003C\u003EO.\u003C0\u003E__IsNullOrWhiteSpace ?? (CompanyService.\u003C\u003EO.\u003C0\u003E__IsNullOrWhiteSpace = new Func<string, bool>(string.IsNullOrWhiteSpace))), (Func<Exception>) (() => (Exception) new InvalidOperationException("Company doesn't have a login name")));
  }

  IEnumerable<string> ICompanyService.GetCompanyLoginNames()
  {
    if (this.IsMultiCompany)
      return this.SelectCompanyLoginNames();
    return (IEnumerable<string>) new string[1]
    {
      this.SingleCompanyLoginName()
    };
  }

  public void ParseLogin(string login, out string username, out string company, out string branch)
  {
    HashSet<string> companies = new HashSet<string>(this.SelectCompanyLoginNames(), this.CompanyComparer);
    CompanyService.ParseLogin(login, out username, out company, out branch, (Func<string, bool>) (c => !companies.Contains(c)));
  }

  public string ExtractCompany(string login)
  {
    string company;
    this.ParseLogin(login, out string _, out company, out string _);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return MaybeObjects.IfNot<string>(company, CompanyService.\u003C\u003EO.\u003C0\u003E__IsNullOrWhiteSpace ?? (CompanyService.\u003C\u003EO.\u003C0\u003E__IsNullOrWhiteSpace = new Func<string, bool>(string.IsNullOrWhiteSpace)));
  }

  internal static void ParseLogin(
    string login,
    out string username,
    out string company,
    out string branch,
    Func<string, bool> companyDoesntExist)
  {
    username = login;
    company = (string) null;
    branch = (string) null;
    if (string.IsNullOrEmpty(login))
      return;
    int length1 = username.LastIndexOf('@');
    if (length1 > 0 && length1 < username.Length - 1)
    {
      company = username.Substring(length1 + 1);
      if (companyDoesntExist(company))
      {
        int length2 = login.LastIndexOf(':');
        if (length2 >= 0)
        {
          branch = length2 < login.Length - 1 ? login.Substring(length2 + 1) : string.Empty;
          username = username.Substring(0, length2);
        }
        company = username.Substring(length1 + 1);
        if (companyDoesntExist(company))
          company = (string) null;
        else
          username = username.Substring(0, length1);
      }
      else
        username = username.Substring(0, length1);
    }
    else
    {
      int length3 = login.LastIndexOf(':');
      if (length3 < 0)
        return;
      branch = length3 < login.Length - 1 ? login.Substring(length3 + 1) : string.Empty;
      username = username.Substring(0, length3);
    }
  }
}
