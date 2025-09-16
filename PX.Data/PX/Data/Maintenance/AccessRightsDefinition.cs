// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.AccessRightsDefinition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Maintenance;

internal class AccessRightsDefinition : IPrefetchable, IPXCompanyDependent
{
  public Dictionary<string, Dictionary<string, short>> AccessRights { get; private set; }

  public void Prefetch()
  {
    this.AccessRights = new Dictionary<string, Dictionary<string, short>>();
    foreach (PX.SM.RolesInGraph rolesInGraph in (IEnumerable<PX.SM.RolesInGraph>) PXDatabase.Select<PX.SM.RolesInGraph>())
    {
      Dictionary<string, short> dictionary;
      if (!this.AccessRights.TryGetValue(rolesInGraph.ScreenID, out dictionary))
      {
        dictionary = new Dictionary<string, short>();
        this.AccessRights[rolesInGraph.ScreenID] = dictionary;
      }
      dictionary[rolesInGraph.Rolename] = rolesInGraph.Accessrights.Value;
    }
  }
}
