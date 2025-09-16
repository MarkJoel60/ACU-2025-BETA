// Decompiled with JetBrains decompiler
// Type: PX.Data.SelectCacheEntry
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class SelectCacheEntry
{
  public PXCommandKey KeyDigest;
  public PXView.PXSearchColumn[] Sorts;
  public PXFilterRow[] Filters;
  public System.Type NewOrder;
  public bool overrideSort;
  public bool anySearch;
  public bool resetTopCount;
  public PXFilterRow[] filters;
  public bool extFilter;

  /// <exclude />
  public sealed class Parameters
  {
    public object[] searches;
    public string[] sortcolumns;
    public bool[] descendings;
    public PXFilterRow[] filters;
    public int maximumRows;
    public System.Type Select;
  }
}
