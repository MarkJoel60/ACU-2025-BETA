// Decompiled with JetBrains decompiler
// Type: PX.Data.Internal.IUserOrganizationService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Internal;

internal interface IUserOrganizationService
{
  IEnumerable<BranchInfo> GetBranches(string userName, bool isGuest, bool onlyActive);

  IEnumerable<int> GetBranchesWithParents(string userName, bool isGuest, bool onlyActive);

  IEnumerable<PXAccess.MasterCollection.Organization> GetOrganizations(
    string userName,
    bool isGuest,
    bool onlyActive,
    bool skipGroups);
}
