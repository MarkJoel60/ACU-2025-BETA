// Decompiled with JetBrains decompiler
// Type: PX.Data.LegacyCompanyServiceExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public static class LegacyCompanyServiceExtensions
{
  internal static string ExtractCompanyWithBranch(
    this ILegacyCompanyService legacyCompanyService,
    string login)
  {
    string company;
    string branch;
    legacyCompanyService.ParseLogin(login, out string _, out company, out branch);
    return LegacyCompanyService.ConcatCompanyAndBranch(company, branch);
  }

  public static string ExtractUsername(
    this ILegacyCompanyService legacyCompanyService,
    string login)
  {
    string username;
    legacyCompanyService.ParseLogin(login, out username, out string _, out string _);
    return username;
  }

  /// <remarks>
  /// Will always return <see langword="null" /> in single-tenant deployments
  /// </remarks>
  public static string ExtractCompany(this ILegacyCompanyService legacyCompanyService, string login)
  {
    string company;
    legacyCompanyService.ParseLogin(login, out string _, out company, out string _);
    return company;
  }

  public static string ConcatLogin(this ILegacyCompanyService _, string username, string company)
  {
    return LegacyCompanyService.ConcatLogin(username, company);
  }
}
