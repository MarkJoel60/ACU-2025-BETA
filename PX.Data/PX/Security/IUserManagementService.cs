// Decompiled with JetBrains decompiler
// Type: PX.Security.IUserManagementService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Web.Security;

#nullable disable
namespace PX.Security;

/// <remarks>
/// Not only is this internal, it is also <b>experimental</b>, we're still fiddling with it.
/// So don't use it unless you either really have to or like living on the bleeding edge.
/// </remarks>
[PXInternalUseOnly]
public interface IUserManagementService : IUserValidationService
{
  MembershipUser GetUser(Guid id);

  MembershipUser GetUser(string username);

  [Obsolete("This is a legacy of MembershipProvider-based implementation, which should not be used without good reason")]
  MembershipUser GetUserAndMarkOnline(string username);

  [Obsolete("This is a legacy of MembershipProvider-based implementation, which should not be used without good reason")]
  void MarkAllUsersOffline();

  [Obsolete("This is a legacy of MembershipProvider-based implementation, which should not be used without good reason")]
  int GetNumberOfUsersOnline();

  void UpdateUser(string username, bool skipWatchdog, params PXDataFieldAssign[] changes);

  void UpdateUserPassword(string username, string password);

  bool ChangePassword(string username, string oldPassword, string newPassword);

  PXDataRecord SelectSMUser(string username, params PXDataField[] pars);
}
