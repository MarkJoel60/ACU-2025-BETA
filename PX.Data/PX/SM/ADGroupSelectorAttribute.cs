// Decompiled with JetBrains decompiler
// Type: PX.SM.ADGroupSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Access.ActiveDirectory;
using System.Collections;

#nullable disable
namespace PX.SM;

public sealed class ADGroupSelectorAttribute : PXCustomSelectorAttribute
{
  [InjectDependencyOnTypeLevel]
  private IActiveDirectoryProvider _activeDirectoryProvider { get; set; }

  public ADGroupSelectorAttribute()
    : base(typeof (ActiveDirectoryGroup.groupID))
  {
  }

  public bool UseCached { get; set; } = true;

  public IEnumerable GetRecords()
  {
    ADGroupSelectorAttribute selectorAttribute = this;
    PX.SM.Access graph = selectorAttribute._Graph as PX.SM.Access;
    foreach (Group group in selectorAttribute._activeDirectoryProvider.GetGroups(selectorAttribute.UseCached))
    {
      PXResultset<RoleActiveDirectory> pxResultset = graph == null ? (PXResultset<RoleActiveDirectory>) null : graph.ActiveDirectoryMap.Search<RoleActiveDirectory.groupID>((object) group.SID);
      if (pxResultset == null || pxResultset.Count == 0)
        yield return (object) new ActiveDirectoryGroup()
        {
          GroupID = group.SID,
          Name = group.DisplayName,
          Domain = group.DC,
          Description = group.Description
        };
    }
  }
}
