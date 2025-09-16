// Decompiled with JetBrains decompiler
// Type: PX.Data.ErpLoginUiService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Services;
using PX.Common;
using PX.Common.Services;
using PX.Export.Authentication;
using PX.Security;
using System;

#nullable disable
namespace PX.Data;

internal class ErpLoginUiService(
  ILoginService loginService,
  ICurrentUserInformationProvider currentUserInformationProvider,
  IPXLogin pxLogin,
  Func<ILicenseService> licenseServiceFactory,
  IUserManagementService userManagementService,
  IFormsAuthenticationService formsAuthenticationService,
  ISessionContextFactory sessionContextFactory) : LoginUiService(loginService, currentUserInformationProvider, pxLogin, licenseServiceFactory, userManagementService, formsAuthenticationService, sessionContextFactory)
{
}
