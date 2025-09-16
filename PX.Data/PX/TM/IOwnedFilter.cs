// Decompiled with JetBrains decompiler
// Type: PX.TM.IOwnedFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.TM;

/// <exclude />
public interface IOwnedFilter
{
  int? OwnerID { get; set; }

  bool? MyOwner { get; set; }

  int? WorkGroupID { get; set; }

  bool? MyWorkGroup { get; set; }
}
