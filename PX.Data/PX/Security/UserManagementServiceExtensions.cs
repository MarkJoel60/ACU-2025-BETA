// Decompiled with JetBrains decompiler
// Type: PX.Security.UserManagementServiceExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Security;

#nullable disable
namespace PX.Security;

internal static class UserManagementServiceExtensions
{
  /// <summary>Mimics <see cref="M:System.Web.Security.Membership.GetUser">Membership.GetUser()</see>.</summary>
  internal static MembershipUser GetUserAndMarkOnline(
    this IUserManagementService userManagementService,
    HttpContext httpContext)
  {
    return userManagementService.GetUserAndMarkOnline(UserManagementServiceExtensions.GetCurrentUserName(httpContext));
  }

  /// <summary>Mimics (private) <see cref="!:Membership.GetCurrentUserName()">Membership.GetCurrentUserName</see>.</summary>
  private static string GetCurrentUserName(HttpContext httpContext)
  {
    if (httpContext != null)
      return httpContext.User.Identity.Name;
    IPrincipal currentPrincipal = Thread.CurrentPrincipal;
    return currentPrincipal?.Identity != null ? currentPrincipal.Identity.Name : string.Empty;
  }

  [Obsolete("This is a legacy of MembershipProvider-based implementation, which should not be used without good reason")]
  internal static void MarkOffline(
    this IUserManagementService userManagementService,
    string username)
  {
    userManagementService.UpdateUser(username, true, new PXDataFieldAssign("IsOnLine", (object) false));
  }
}
