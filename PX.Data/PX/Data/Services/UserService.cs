// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.UserService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Services;

/// <exclude />
public class UserService : IUserService
{
  public bool AllowDeleteRolesOnGuestChange(string roleName) => true;

  public void BeforeSaveUser()
  {
  }

  public IEnumerable<string> GetRolesToCopy()
  {
    return (IEnumerable<string>) PXAccess.GetAdministratorRoles();
  }

  public IEnumerable<string> FilterRoles(IEnumerable<string> roles) => roles;
}
