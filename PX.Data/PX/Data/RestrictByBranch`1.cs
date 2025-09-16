// Decompiled with JetBrains decompiler
// Type: PX.Data.RestrictByBranch`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

public class RestrictByBranch<Operand> : RestrictByOrganization<Operand> where Operand : IBqlParameter
{
  protected override HashSet<int> GetVisibilityList(object val)
  {
    PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(new int?((int) val));
    return RestrictByOrganization<Operand>.GetParents(branch != null ? branch.BAccountID : 0);
  }
}
