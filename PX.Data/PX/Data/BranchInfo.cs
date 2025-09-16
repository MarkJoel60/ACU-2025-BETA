// Decompiled with JetBrains decompiler
// Type: PX.Data.BranchInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public sealed class BranchInfo
{
  internal BranchInfo(int id, string cd, string name, bool deleted)
  {
    this.Id = id;
    this.Cd = cd;
    this.Name = name;
    this.Deleted = deleted;
  }

  public int Id { get; }

  public string Cd { get; }

  public string Name { get; }

  internal bool Deleted { get; }
}
