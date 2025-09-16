// Decompiled with JetBrains decompiler
// Type: PX.Data.Auth.UserValidationExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.Http;
using PX.AspNetCore;
using PX.Common;
using PX.Security;
using PX.SM;
using System;
using System.Web;
using System.Web.Security;

#nullable disable
namespace PX.Data.Auth;

[PXInternalUseOnly]
public static class UserValidationExtensions
{
  public static bool IsValidForLogin(
    this IUserValidationService userValidationService,
    HttpRequestBase request,
    Users user)
  {
    if (user == null)
      return false;
    bool? nullable = user.IsApproved;
    nullable = nullable.GetValueOrDefault() ? user.IsLockedOut : throw new PXException("Your account is disabled. Please contact your system administrator.");
    if (nullable.GetValueOrDefault())
      throw new PXException("Your account is locked out. Please contact your system administrator.");
    return userValidationService.ValidateUserIP(request, user.Username);
  }

  internal static bool IsValidForLogin(
    this IUserValidationService userValidationService,
    HttpRequestBase request,
    MembershipUser user)
  {
    if (user == null)
      return false;
    if (!user.IsApproved)
      throw new PXException("Your account is disabled. Please contact your system administrator.");
    if (user.IsLockedOut)
      throw new PXException("Your account is locked out. Please contact your system administrator.");
    return userValidationService.ValidateUserIP(request, user.UserName);
  }

  internal static bool IsValidForLogin(
    this IUserValidationService userValidationService,
    HttpContext coreContext,
    MembershipUser user)
  {
    return userValidationService.IsValidForLogin(coreContext.GetSystemWebHttpContextBase().Request, user);
  }

  internal static bool IsAllowedToLogin(
    this IUserValidationService userValidationService,
    string login)
  {
    return userValidationService.ValidateUserRoles(login) && userValidationService.ValidateExternalUser(login);
  }

  [PXInternalUseOnly]
  public static bool IsUserValid(
    this IUserManagementService userManagementService,
    string login,
    out string message)
  {
    using (PXLoginScope pxLoginScope = new PXLoginScope(login, Array.Empty<string>()))
    {
      if (userManagementService.GetUser(pxLoginScope.UserName) == null)
      {
        message = "User does not exist";
        return false;
      }
      message = ((IUserValidationService) userManagementService).GetValidationMessage(pxLoginScope.UserName);
      return message == null;
    }
  }
}
