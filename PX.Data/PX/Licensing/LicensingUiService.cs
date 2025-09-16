// Decompiled with JetBrains decompiler
// Type: PX.Licensing.LicensingUiService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.Services;
using System;

#nullable disable
namespace PX.Licensing;

internal sealed class LicensingUiService : ILicensingUiService
{
  private readonly ILicensingManager _licensingManager;
  private readonly Func<ILicenseWarningService> _warningServiceFactoryFactory;
  private readonly IPXLicensePolicy _policy;
  private readonly ILegacyCompanyService _legacyCompanyService;

  public LicensingUiService(
    ILicensingManager licensingManager,
    Func<ILicenseWarningService> warningServiceFactory,
    IPXLicensePolicy policy,
    ILegacyCompanyService legacyCompanyService)
  {
    this._licensingManager = licensingManager;
    this._warningServiceFactoryFactory = warningServiceFactory;
    this._policy = policy;
    this._legacyCompanyService = legacyCompanyService;
  }

  public bool CheckWarning(out string message, out string navigation, out string navScreenID)
  {
    navScreenID = "SM201510";
    PXLicense license = this._licensingManager.License;
    message = navigation = (string) null;
    ILicenseWarningService licenseWarningService = this._warningServiceFactoryFactory();
    if (licenseWarningService.BypassWarning(license))
      return false;
    switch (license.State)
    {
      case PXLicenseState.Valid:
        if (this._policy.HasConstraintsViolations(license))
        {
          navScreenID = "SM604000";
          licenseWarningService.AddMessage("System constraints maximum defined for your current license has been exceeded.");
          navigation = "More Details";
          break;
        }
        PXSessionContext pxIdentity = PXContext.PXIdentity;
        if (pxIdentity != null && pxIdentity.Authenticated)
        {
          string company = this._legacyCompanyService.ExtractCompany(pxIdentity.IdentityName);
          if (license.Trials.Contains(company))
          {
            licenseWarningService.AddMessage("You are currently using a test tenant that is not intended for production use.");
            break;
          }
          break;
        }
        break;
      case PXLicenseState.GracePeriod:
        licenseWarningService.AddMessage("Your license has expired. Please renew your license. ");
        navigation = "Renew";
        break;
      case PXLicenseState.NotifyPeriod:
        string message1 = string.Compare(license.Type, "PERP", StringComparison.OrdinalIgnoreCase) == 0 ? "The system was not able to connect to the licensing server. This may happen due to network connectivity or configuration issues. Please contact the support team to resolve this problem." : "Your license will expire soon. Please renew your license. ";
        licenseWarningService.AddMessage(message1);
        navigation = "Renew";
        break;
      case PXLicenseState.Expired:
        licenseWarningService.AddMessage("Your license has expired. Your product has been switched into trial mode. ");
        navigation = "Renew";
        break;
      case PXLicenseState.Invalid:
      case PXLicenseState.Rejected:
        licenseWarningService.AddMessage("Your product is in trial mode. Only two concurrent users are allowed. ");
        navigation = "Activate";
        break;
    }
    if (!string.IsNullOrEmpty(navigation))
      navigation = PXMessages.LocalizeNoPrefix(navigation);
    if (licenseWarningService.ShowMessage())
      message = licenseWarningService.GetMessage();
    return true;
  }
}
