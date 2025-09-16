// Decompiled with JetBrains decompiler
// Type: PX.Api.Services.CompanyServiceExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Linq;

#nullable disable
namespace PX.Api.Services;

internal static class CompanyServiceExtensions
{
  internal static bool IsExistingCompany(this ICompanyService companyService, string companyName)
  {
    return companyService.GetCompanyLoginNames().Contains<string>(companyName, companyService.CompanyComparer);
  }
}
