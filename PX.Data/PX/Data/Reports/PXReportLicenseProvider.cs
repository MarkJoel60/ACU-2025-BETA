// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.PXReportLicenseProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Common;
using PX.Licensing;
using PX.Reports;
using System.Collections.ObjectModel;

#nullable disable
namespace PX.Data.Reports;

public class PXReportLicenseProvider : ReportLicenseProvider
{
  private readonly ILicensing _licensing = ServiceLocator.Current.GetInstance<ILicensing>();
  private readonly ILegacyCompanyService _legacyCompanyService = ServiceLocator.Current.GetInstance<ILegacyCompanyService>();

  public virtual bool IsLicenseValid => this._licensing.License.Licensed;

  public virtual bool IsTrialVersion
  {
    get
    {
      string company = this._legacyCompanyService.ExtractCompany(PXContext.PXIdentity.IdentityName);
      ReadOnlyCollection<string> trials = this._licensing.License.Trials;
      return trials != null && __nonvirtual (trials.Contains(company));
    }
  }
}
