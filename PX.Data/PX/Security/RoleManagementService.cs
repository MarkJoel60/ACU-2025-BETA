// Decompiled with JetBrains decompiler
// Type: PX.Security.RoleManagementService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Web.Security;

#nullable disable
namespace PX.Security;

internal class RoleManagementService : IRoleManagementService
{
  private readonly RoleProvider _roleProvider;

  public RoleManagementService(RoleProvider roleProvider) => this._roleProvider = roleProvider;

  string[] IRoleManagementService.GetRolesForUser(string username)
  {
    return this._roleProvider.GetRolesForUser(username);
  }

  bool IRoleManagementService.RoleExists(string roleName)
  {
    return this._roleProvider.RoleExists(roleName);
  }

  string[] IRoleManagementService.GetUsersInRole(string roleName)
  {
    return this._roleProvider.GetUsersInRole(roleName);
  }

  string[] IRoleManagementService.GetAllRoles() => this._roleProvider.GetAllRoles();
}
