// Decompiled with JetBrains decompiler
// Type: PX.SM.OtherCompanySelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Update;
using System.Collections;

#nullable disable
namespace PX.SM;

internal class OtherCompanySelectorAttribute : CompanySelectorAttribute
{
  protected override IEnumerable GetRecords()
  {
    UPCompany current = (UPCompany) this._Graph.Caches[typeof (UPCompany)].Current;
    foreach (UPCompany selectCompany in PXCompanyHelper.SelectCompanies())
    {
      int? companyId1 = current.CompanyID;
      int? companyId2 = selectCompany.CompanyID;
      if (!(companyId1.GetValueOrDefault() == companyId2.GetValueOrDefault() & companyId1.HasValue == companyId2.HasValue))
        yield return (object) selectCompany;
    }
  }
}
