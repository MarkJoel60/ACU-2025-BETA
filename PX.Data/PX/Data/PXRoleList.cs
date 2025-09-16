// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRoleList
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXRoleList
{
  public readonly string[] Common;
  public readonly string[] Prioritized;
  public readonly string[] CommonDenied;

  public PXRoleList(List<string> common, List<string> prioritized, List<string> commonDenied)
  {
    if (common != null)
      this.Common = common.ToArray();
    if (prioritized != null)
      this.Prioritized = prioritized.ToArray();
    if (commonDenied == null)
      return;
    this.CommonDenied = commonDenied.ToArray();
  }
}
