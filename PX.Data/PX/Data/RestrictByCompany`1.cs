// Decompiled with JetBrains decompiler
// Type: PX.Data.RestrictByCompany`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

public class RestrictByCompany<Operand> : RestrictByOrganization<Operand> where Operand : IBqlParameter
{
  protected override HashSet<int> GetVisibilityList(object val)
  {
    int? organizationBaccountId = PXAccess.GetOrganizationBAccountID(new int?((int) val));
    HashSet<int> parents = RestrictByOrganization<Operand>.GetParents(organizationBaccountId.Value);
    PXAccess.MasterCollection.Organization organizationByBaccountId = PXAccess.GetOrganizationByBAccountID(organizationBaccountId);
    if (organizationByBaccountId != null)
    {
      foreach (PXAccess.MasterCollection.Branch childBranch in organizationByBaccountId.ChildBranches)
        parents.Add(childBranch.BAccountID);
    }
    return parents;
  }
}
