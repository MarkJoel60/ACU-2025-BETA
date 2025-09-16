// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFilterTuple
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXFilterTuple
{
  public Guid? FilterID { get; set; }

  public PXFilterRow[] FilterRows { get; set; }

  public PXFilterTuple()
  {
  }

  public PXFilterTuple(Guid? filterID, PXFilterRow[] filterRows)
    : this()
  {
    this.FilterID = filterID;
    this.FilterRows = filterRows;
  }

  public bool IsEmpty()
  {
    if (this.FilterID.HasValue)
      return false;
    return this.FilterRows == null || this.FilterRows.Length == 0;
  }
}
