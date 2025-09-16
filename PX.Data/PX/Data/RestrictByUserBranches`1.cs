// Decompiled with JetBrains decompiler
// Type: PX.Data.RestrictByUserBranches`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

public class RestrictByUserBranches<Operand> : RestrictByOrganization<Operand> where Operand : IBqlParameter
{
  private static readonly Lazy<IUserOrganizationService> UserOrganizationService = new Lazy<IUserOrganizationService>((Func<IUserOrganizationService>) (() => ServiceLocator.Current.GetInstance<IUserOrganizationService>()));

  protected override HashSet<int> GetVisibilityList(object val)
  {
    if (val is string userName)
      return RestrictByUserBranches<Operand>.UserOrganizationService.Value.GetBranchesWithParents(userName).ToHashSet<int>();
    return new HashSet<int>() { 0 };
  }
}
