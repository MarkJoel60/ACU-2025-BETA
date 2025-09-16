// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Models.OrganizationMeta
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Services;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Api.Models;

public class OrganizationMeta
{
  public string DisplayName { get; set; }

  public string Id { get; set; }

  public Dictionary<string, string> ThemeVariables { get; set; }

  public IEnumerable<BranchMeta> Branches { get; set; }
}
