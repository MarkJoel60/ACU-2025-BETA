// Decompiled with JetBrains decompiler
// Type: PX.SM.CompanyInquire
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Update;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.SM;

public class CompanyInquire : PXGraph<CompanyInquire>
{
  public PXSelectRedirect<UPCompany, UPCompany> Companies_Redirect;
  [PXVirtualDAC]
  public PXSelectOrderBy<UPCompany, OrderBy<Asc<UPCompany.sequence>>> Companies;
  public PXAction<UPCompany> InsertCompanyCommand;
  public PXAction<UPCompany> MoveCompanyUp;
  public PXAction<UPCompany> MoveCompanyDown;

  protected IEnumerable companies()
  {
    List<UPCompany> upCompanyList = new List<UPCompany>();
    foreach (UPCompany upCompany in this.Companies.Cache.Inserted)
    {
      upCompany.CompanyID = new int?(upCompany.CompanyID.GetValueOrDefault());
      upCompanyList.Add(upCompany);
    }
    foreach (UPCompany upCompany in this.Companies.Cache.Updated)
    {
      upCompany.CompanyID = new int?(upCompany.CompanyID.GetValueOrDefault());
      upCompanyList.Add(upCompany);
    }
    foreach (UPCompany selectCompany in PXCompanyHelper.SelectCompanies())
    {
      if (!upCompanyList.Contains(selectCompany))
        upCompanyList.Add(selectCompany);
    }
    return (IEnumerable) upCompanyList;
  }

  protected PXLoginScope GetLoginScope()
  {
    return CompanyInquire.GetLoginScope(this.Companies.Current ?? throw new PXException("A tenant is not selected."));
  }

  protected static PXLoginScope GetLoginScope(UPCompany company)
  {
    return new PXLoginScope($"{PXAccess.GetUserName()}@{company.LoginName}", Array.Empty<string>());
  }

  protected UPCompany GetCurrentCompany()
  {
    return this.companies().Cast<UPCompany>().FirstOrDefault<UPCompany>((Func<UPCompany, bool>) (c =>
    {
      int? companyId = c.CompanyID;
      int currentCompany = PXInstanceHelper.CurrentCompany;
      return companyId.GetValueOrDefault() == currentCompany & companyId.HasValue;
    }));
  }

  private void EnsureCompanySequence()
  {
    List<UPCompany> list1 = PXCompanyHelper.SelectCompanies().ToList<UPCompany>();
    List<UPCompany> list2 = list1.Where<UPCompany>((Func<UPCompany, bool>) (company => !company.Sequence.HasValue)).OrderBy<UPCompany, int?>((Func<UPCompany, int?>) (company => company.CompanyID)).ToList<UPCompany>();
    HashSet<UPCompany> upCompanySet = new HashSet<UPCompany>((IEnumerable<UPCompany>) list2);
    int? nullable1 = new int?(list1.Select<UPCompany, int?>((Func<UPCompany, int?>) (company => company.Sequence)).Where<int?>((Func<int?, bool>) (sequence => sequence.HasValue)).Max().GetValueOrDefault());
    foreach (UPCompany upCompany in list2)
    {
      int? nullable2 = nullable1;
      int? nullable3;
      nullable1 = nullable3 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() + 1) : new int?();
      upCompany.Sequence = nullable3;
    }
    foreach (UPCompany company in upCompanySet)
      CompanyInquire.UpdateCompanySequence(company, company.Sequence.Value);
  }

  public CompanyInquire()
  {
    this.EnsureCompanySequence();
    this.Companies.Cache.AllowDelete = false;
    this.Companies.Cache.AllowInsert = false;
    bool flag = PXDatabase.Provider.GetLoginCompanies().Any<KeyValuePair<int, string>>();
    this.MoveCompanyUp.SetEnabled(PXDatabase.Provider.DatabaseDefinedCompanies || !flag);
    this.MoveCompanyDown.SetEnabled(PXDatabase.Provider.DatabaseDefinedCompanies || !flag);
    if (!PXDatabase.Provider.DatabaseDefinedCompanies | flag)
    {
      this.InsertCompanyCommand.SetEnabled(false);
      this.InsertCompanyCommand.SetTooltip("The web configuration does not allow you to create tenants.");
    }
    else
      this.InsertCompanyCommand.SetEnabled(true);
  }

  [PXUIField(DisplayName = "Insert Tenant", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXInsertButton]
  protected IEnumerable insertCompanyCommand(PXAdapter adapter)
  {
    CompanyMaint instance = PXGraph.CreateInstance<CompanyMaint>();
    PXRedirectHelper.TryRedirect((PXGraph) instance, (object) instance.Companies.Insert(new UPCompany()), PXRedirectHelper.WindowMode.NewWindow);
    return adapter.Get();
  }

  [PXButton(Tooltip = "Move Tenant Up")]
  [PXUIField(DisplayName = "Move Up", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected IEnumerable moveCompanyUp(PXAdapter adapter)
  {
    int? currentCompanyId = this.Companies.Current.CompanyID;
    List<UPCompany> list = this.Companies.Select().Select<PXResult<UPCompany>, UPCompany>((Expression<Func<PXResult<UPCompany>, UPCompany>>) (c => (UPCompany) c)).ToList<UPCompany>();
    UPCompany upCompany = list.First<UPCompany>((Func<UPCompany, bool>) (c =>
    {
      int? companyId = c.CompanyID;
      int? nullable = currentCompanyId;
      return companyId.GetValueOrDefault() == nullable.GetValueOrDefault() & companyId.HasValue == nullable.HasValue;
    }));
    int num = list.IndexOf(upCompany);
    if (num <= 0 || num > list.Count<UPCompany>() - 1)
      return adapter.Get();
    UPCompany company1 = list[num - 1];
    int? sequence1 = upCompany.Sequence;
    int sequence2 = sequence1.Value;
    UPCompany company2 = upCompany;
    sequence1 = company1.Sequence;
    int sequence3 = sequence1.Value;
    CompanyInquire.UpdateCompanySequence(company2, sequence3);
    CompanyInquire.UpdateCompanySequence(company1, sequence2);
    PXDatabase.ClearCompanyCache();
    return adapter.Get();
  }

  [PXButton(Tooltip = "Move Tenant Down")]
  [PXUIField(DisplayName = "Move Down", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected IEnumerable moveCompanyDown(PXAdapter adapter)
  {
    int? currentCompanyId = this.Companies.Current.CompanyID;
    List<UPCompany> list = this.Companies.Select().Select<PXResult<UPCompany>, UPCompany>((Expression<Func<PXResult<UPCompany>, UPCompany>>) (c => (UPCompany) c)).ToList<UPCompany>();
    UPCompany upCompany = list.First<UPCompany>((Func<UPCompany, bool>) (c =>
    {
      int? companyId = c.CompanyID;
      int? nullable = currentCompanyId;
      return companyId.GetValueOrDefault() == nullable.GetValueOrDefault() & companyId.HasValue == nullable.HasValue;
    }));
    int num = list.IndexOf(upCompany);
    if (num < 0 || num >= list.Count<UPCompany>() - 1)
      return adapter.Get();
    UPCompany company1 = list[num + 1];
    int? sequence1 = upCompany.Sequence;
    int sequence2 = sequence1.Value;
    UPCompany company2 = upCompany;
    sequence1 = company1.Sequence;
    int sequence3 = sequence1.Value;
    CompanyInquire.UpdateCompanySequence(company2, sequence3);
    CompanyInquire.UpdateCompanySequence(company1, sequence2);
    PXDatabase.ClearCompanyCache();
    return adapter.Get();
  }

  protected virtual void UPCompany_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (PXDatabase.Provider.DatabaseDefinedCompanies && !PXDatabase.Provider.GetLoginCompanies().Any<KeyValuePair<int, string>>())
      return;
    this.InsertCompanyCommand.SetEnabled(false);
    this.InsertCompanyCommand.SetTooltip("The web configuration does not allow you to create tenants.");
  }

  protected static void UpdateCompanySequence(UPCompany company, int sequence)
  {
    PXDatabase.ResetCredentials();
    using (CompanyInquire.GetLoginScope(company))
      PXDatabase.Update(typeof (PX.Data.Update.Company), (PXDataFieldParam) new PXDataFieldAssign("Sequence", (object) sequence));
    PXDatabase.ResetCredentials();
  }
}
