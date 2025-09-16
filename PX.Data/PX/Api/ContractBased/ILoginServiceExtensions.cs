// Decompiled with JetBrains decompiler
// Type: PX.Api.ContractBased.ILoginServiceExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Services;
using PX.Api.Soap.Screen;
using PX.Data;
using PX.Data.MultiFactorAuth;
using System;

#nullable disable
namespace PX.Api.ContractBased;

internal static class ILoginServiceExtensions
{
  internal static readonly string Prefix = ScreenGate.SoapApiAuthPrefix;

  public static bool IsUserAuthenticatedForSoapApi(this ILoginService loginService)
  {
    return loginService.IsUserAuthenticated(false, ILoginServiceExtensions.Prefix);
  }

  public static void LoginForSoapApi(
    this ILoginService loginService,
    IMultiFactorService multiFactorService,
    string login,
    string password,
    string company,
    string branch,
    string locale)
  {
    if (!multiFactorService.IsAccessCodeValid(login, password, (string) null, (object) null, out Tuple<int, Guid, bool> _, out ErrorReason? _))
      throw new PXException("Two-factor authentication is enabled for this user. You cannot log in with this user account.");
    loginService.Login(login, password, company, branch, locale, ILoginServiceExtensions.Prefix);
  }
}
