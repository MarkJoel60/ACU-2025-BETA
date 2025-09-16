// Decompiled with JetBrains decompiler
// Type: PX.Security.Authorization.ISystemRolesProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;

#nullable disable
namespace PX.Security.Authorization;

[PXInternalUseOnly]
public interface ISystemRolesProvider
{
  string GetAdministratorRole();

  IEnumerable<string> GetAdministratorRoles();

  string GetPortalAdministratorRole();

  string GetArchivistRole();
}
