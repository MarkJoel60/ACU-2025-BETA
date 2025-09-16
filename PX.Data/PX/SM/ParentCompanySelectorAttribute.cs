// Decompiled with JetBrains decompiler
// Type: PX.SM.ParentCompanySelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Update;
using System.Collections;

#nullable disable
namespace PX.SM;

internal class ParentCompanySelectorAttribute : CompanySelectorAttribute
{
  public bool ExcludeHiddenCopmanies;

  protected override IEnumerable GetRecords()
  {
    ParentCompanySelectorAttribute selectorAttribute = this;
    UPCompany current = (UPCompany) selectorAttribute._Graph.Caches[typeof (UPCompany)].Current;
    foreach (UPCompany selectCompany in PXCompanyHelper.SelectCompanies(PXCompanySelectOptions.Accessible))
    {
      if (!selectorAttribute.ExcludeHiddenCopmanies || !(selectCompany.Hidden ?? false))
      {
        int? companyId1 = current.CompanyID;
        int? companyId2 = selectCompany.CompanyID;
        if (!(companyId1.GetValueOrDefault() == companyId2.GetValueOrDefault() & companyId1.HasValue == companyId2.HasValue))
          yield return (object) selectCompany;
      }
    }
  }
}
