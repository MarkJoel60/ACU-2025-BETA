// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.ICompanyService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Services;

/// <exclude />
public interface ICompanyService
{
  void PrepareGeneralInfo(IEnumerable<int> companies);

  void PrepareCompanyInfo();

  void PersistCompanyInfo(UPCompany company, bool disableSchedulers);

  void AfterCompaniesPersist();

  List<string> GetOtherCompanyNamesWithEnabledFeature(int currentCompanyID, string feature);

  List<string> GetOtherCompanyNamesHavingAnyData(int currentCompanyID, string table);
}
