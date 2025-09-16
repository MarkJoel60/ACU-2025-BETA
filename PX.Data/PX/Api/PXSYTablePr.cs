// Decompiled with JetBrains decompiler
// Type: PX.Api.PXSYTablePr
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Api;

internal class PXSYTablePr : PXSYTable
{
  public List<object> PrimaryViewRows { get; private set; }

  public PXSYTablePr(IEnumerable<string> columns, IEnumerable<object> primaryViewRows)
    : base(columns)
  {
    this.PrimaryViewRows = new List<object>(primaryViewRows);
  }
}
