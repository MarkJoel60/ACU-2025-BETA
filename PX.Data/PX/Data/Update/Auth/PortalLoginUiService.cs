// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.Auth.PortalLoginUiService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Services;
using PX.Common;
using PX.Common.Services;
using PX.Data.EP;
using PX.Export.Authentication;
using PX.Security;
using PX.SM;
using PX.SP;
using System;

#nullable disable
namespace PX.Data.Update.Auth;

internal class PortalLoginUiService(
  ILoginService loginService,
  ICurrentUserInformationProvider currentUserInformationProvider,
  IPXLogin pxLogin,
  Func<ILicenseService> licenseServiceFactory,
  IUserManagementService userManagementService,
  IFormsAuthenticationService formsAuthenticationService,
  ISessionContextFactory sessionContextFactory) : LoginUiService(loginService, currentUserInformationProvider, pxLogin, licenseServiceFactory, userManagementService, formsAuthenticationService, sessionContextFactory)
{
  protected override string GenerateRecoveryUrl(
    Access access,
    string linkToSend,
    string paramName,
    string hash)
  {
    bool? guest = access.UserList.Current.Guest;
    bool flag = true;
    string url;
    if (guest.GetValueOrDefault() == flag & guest.HasValue)
    {
      url = PortalHelper.GetPortal().Url.TrimEnd('/') + "/Frames/Login.aspx";
      if (PXDatabase.Companies.Length != 0)
      {
        string companyId = PXAccess.GetCompanyID();
        if (!string.IsNullOrEmpty(companyId))
          url = PXUrl.AppendUrlParameter(url, "cid", TextUtils.UrlEncode(companyId));
      }
    }
    else
      url = MailAccountManager.GetEmailPreferences().NotificationSiteUrl ?? linkToSend;
    return PXUrl.AppendUrlParameter(url, paramName, hash).Replace(' ', '+');
  }
}
