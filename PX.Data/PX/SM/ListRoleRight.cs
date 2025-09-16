// Decompiled with JetBrains decompiler
// Type: PX.SM.ListRoleRight
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

public static class ListRoleRight
{
  public static List<int> GetIndexesByType(string type)
  {
    List<int> indexesByType;
    if (type != null)
    {
      switch (type.Length)
      {
        case 3:
          if (type == "All")
          {
            indexesByType = new List<int>()
            {
              -1,
              0,
              1,
              2,
              3,
              4,
              4,
              4,
              5,
              6
            };
            goto label_19;
          }
          goto label_18;
        case 4:
          if (type == "Wiki")
          {
            indexesByType = new List<int>()
            {
              0,
              1,
              2,
              3,
              4,
              5
            };
            goto label_19;
          }
          goto label_18;
        case 7:
          if (type == "Level 3")
          {
            indexesByType = new List<int>() { -1, 0, 1, 2 };
            goto label_19;
          }
          goto label_18;
        case 10:
          switch (type[0])
          {
            case 'D':
              if (type == "Dashboards")
                break;
              goto label_18;
            case 'W':
              if (type == "Workspaces")
                break;
              goto label_18;
            default:
              goto label_18;
          }
          break;
        case 11:
          if (type == "Wiki Folder")
            break;
          goto label_18;
        case 15:
          if (type == "Site Map Node 2")
          {
            indexesByType = new List<int>()
            {
              0,
              1,
              2,
              3,
              4
            };
            goto label_19;
          }
          goto label_18;
        case 22:
          if (type == "WorkspacesWithMultiple")
          {
            indexesByType = new List<int>() { 0, 4, 6 };
            goto label_19;
          }
          goto label_18;
        case 27:
          if (type == "Site Map Node Without Graph")
            break;
          goto label_18;
        default:
          goto label_18;
      }
      indexesByType = new List<int>() { 0, 4 };
      goto label_19;
    }
label_18:
    indexesByType = new List<int>() { -1, 0, 1, 2, 3, 4 };
label_19:
    return indexesByType;
  }

  public static List<string> GetByType(string type)
  {
    List<string> byType;
    if (type != null)
    {
      switch (type.Length)
      {
        case 3:
          if (type == "All")
          {
            byType = new List<string>()
            {
              "Inherited",
              "Revoked",
              "View Only",
              "Edit",
              "Insert",
              "Delete",
              "Granted",
              "Publish",
              "Delete",
              "Multiple Rights"
            };
            goto label_19;
          }
          goto label_18;
        case 4:
          if (type == "Wiki")
          {
            byType = new List<string>()
            {
              "Revoked",
              "View Only",
              "Edit",
              "Insert",
              "Publish",
              "Delete"
            };
            goto label_19;
          }
          goto label_18;
        case 7:
          if (type == "Level 3")
          {
            byType = new List<string>()
            {
              "Inherited",
              "Revoked",
              "View Only",
              "Edit"
            };
            goto label_19;
          }
          goto label_18;
        case 10:
          switch (type[0])
          {
            case 'D':
              if (type == "Dashboards")
                break;
              goto label_18;
            case 'W':
              if (type == "Workspaces")
                break;
              goto label_18;
            default:
              goto label_18;
          }
          break;
        case 11:
          if (type == "Wiki Folder")
            break;
          goto label_18;
        case 15:
          if (type == "Site Map Node 2")
          {
            byType = new List<string>()
            {
              "Revoked",
              "View Only",
              "Edit",
              "Insert",
              "Delete"
            };
            goto label_19;
          }
          goto label_18;
        case 22:
          if (type == "WorkspacesWithMultiple")
          {
            byType = new List<string>()
            {
              "Revoked",
              "Granted",
              "Multiple Rights"
            };
            goto label_19;
          }
          goto label_18;
        case 27:
          if (type == "Site Map Node Without Graph")
            break;
          goto label_18;
        default:
          goto label_18;
      }
      byType = new List<string>() { "Revoked", "Granted" };
      goto label_19;
    }
label_18:
    byType = new List<string>()
    {
      "Inherited",
      "Revoked",
      "View Only",
      "Edit",
      "Insert",
      "Delete"
    };
label_19:
    return byType;
  }

  public static string GetRoleRightIdentifier(Role row, PXGraph graph)
  {
    if (!row.NodeID.HasValue)
      return "Default";
    PXSiteMapNode nodeFromKeyUnsecure = PXSiteMap.Provider.FindSiteMapNodeFromKeyUnsecure(row.NodeID.Value);
    if (nodeFromKeyUnsecure == null)
    {
      if (!(graph is Access access) || !access.IsWorkspace(row.NodeID))
        return "Default";
      int? roleRight = row.RoleRight;
      int num = 6;
      return !(roleRight.GetValueOrDefault() == num & roleRight.HasValue) ? "Workspaces" : "WorkspacesWithMultiple";
    }
    int? level = row.Level;
    string roleRightIdentifier;
    if (level.HasValue)
    {
      switch (level.GetValueOrDefault())
      {
        case 1:
          roleRightIdentifier = !PXSiteMap.IsDashboard(nodeFromKeyUnsecure) ? (!(TypeInfoProvider.GetType(nodeFromKeyUnsecure.GraphType) == (System.Type) null) || PXGraph.GeneratorIsActive ? (row.CacheName != null ? "Site Map Node" : "Site Map Node 2") : "Site Map Node Without Graph") : "Dashboards";
          goto label_14;
        case 2:
          roleRightIdentifier = "Level 2";
          goto label_14;
        case 3:
          roleRightIdentifier = "Level 3";
          goto label_14;
      }
    }
    roleRightIdentifier = "Default";
label_14:
    return roleRightIdentifier;
  }

  internal static int SelectRightFromAccessed(Role r, PXGraph graph)
  {
    return ListRoleRight.SelectRightFromAccessed(r, r.RoleRight.Value, graph);
  }

  internal static int SelectRightFromAccessed(Role r, int accessRights, PXGraph graph)
  {
    List<int> indexesByType = ListRoleRight.GetIndexesByType(ListRoleRight.GetRoleRightIdentifier(r, graph));
    int[] array = indexesByType.Where<int>((Func<int, bool>) (x => x >= accessRights)).OrderBy<int, int>((Func<int, int>) (x => x)).ToArray<int>();
    return !((IEnumerable<int>) array).Any<int>() ? indexesByType.Max() : ((IEnumerable<int>) array).Min();
  }
}
