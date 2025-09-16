// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.MergeCacheContext
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.SQLTree;

internal class MergeCacheContext
{
  private readonly MergeCacheContext.CreateItemDelegate _createItemDelegate;

  public MergeCacheContext(
    PXGraph graph,
    SQLinqExecutor linqExecutor,
    bool createNew = false,
    MergeCacheContext.CreateItemDelegate createItemDelegate = null)
  {
    this.Graph = graph;
    this.LinqExecutor = linqExecutor;
    this.CreateNew = createNew;
    this.Status = MergeCacheStatus.None;
    this._createItemDelegate = createItemDelegate;
  }

  public object CreateItem(
    PXCache cache,
    PXDataRecord record,
    ref int position,
    bool isReadOnly,
    out bool wasUpdated)
  {
    return this._createItemDelegate != null ? this._createItemDelegate(cache, record, ref position, isReadOnly, out wasUpdated) : cache.Select(record, ref position, isReadOnly, out wasUpdated);
  }

  public PXGraph Graph { get; }

  public SQLinqExecutor LinqExecutor { get; }

  public System.Type DacTypeToMerge => this.LinqExecutor.GetDacTypeToMerge();

  /// <summary>
  /// Tells Projection to create object for dac that was inserted in PXCache
  /// </summary>
  public bool CreateNew { get; }

  public object MergedDac { get; set; }

  public MergeCacheStatus Status { get; set; }

  internal delegate object CreateItemDelegate(
    PXCache cache,
    PXDataRecord record,
    ref int position,
    bool isReadOnly,
    out bool wasUpdated);
}
