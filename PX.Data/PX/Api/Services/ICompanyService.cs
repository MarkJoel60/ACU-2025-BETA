// Decompiled with JetBrains decompiler
// Type: PX.Api.Services.ICompanyService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;

#nullable disable
namespace PX.Api.Services;

[PXInternalUseOnly]
public interface ICompanyService
{
  bool IsMultiCompany { get; }

  string GetSingleCompanyLoginName();

  /// <remarks>Will return a single item when in single company mode</remarks>
  /// &gt;
  IEnumerable<string> GetCompanyLoginNames();

  IEqualityComparer<string> CompanyComparer { get; }

  string ExtractCompany(string login);

  void ParseLogin(string login, out string username, out string company, out string branch);
}
