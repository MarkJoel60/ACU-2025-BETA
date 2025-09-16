// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSortColumnsTuple
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

internal class PXSortColumnsTuple
{
  public string[] SortColumns { get; set; }

  public bool[] Descendings { get; set; }

  public PXSortColumnsTuple(string[] sortColumns, bool[] descendings, bool isOnlyDefSorts)
  {
    this.SortColumns = sortColumns;
    this.Descendings = descendings;
    this.IsOnlyDefSorts = isOnlyDefSorts;
  }

  public bool IsOnlyDefSorts { get; }
}
