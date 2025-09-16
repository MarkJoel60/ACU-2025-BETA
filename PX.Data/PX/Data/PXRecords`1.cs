// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRecords`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <summary>A data view that is used to store multiple unbound data records (never requests data from the database).</summary>
/// <typeparam name="Table">The DAC that provides the unbound data records.</typeparam>
public class PXRecords<Table> : PXSelectBase<Table> where Table : class, IBqlTable, new()
{
  private PXCache cache;

  /// <exclude />
  public PXRecords(PXGraph graph)
  {
    this._Graph = graph;
    this.View = new PXView(graph, false, (BqlCommand) new PX.Data.Select<Table>(), (Delegate) new PXSelectDelegate(this.Get));
    System.Type type = typeof (Table);
    this.cache = this._Graph.Caches[type];
    this._Graph.Caches.ProcessCacheMapping(this._Graph, this._Graph.Prototype.Memoize<System.Type>((Func<System.Type>) (() => this.cache.GetItemType()), (object) type));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this._Graph.RowPersisting.AddHandler(type, PXRecords<Table>.\u003C\u003EO.\u003C0\u003E__CancelPersist ?? (PXRecords<Table>.\u003C\u003EO.\u003C0\u003E__CancelPersist = new PXRowPersisting(PXRecords<Table>.CancelPersist)));
  }

  /// <exclude />
  [PXDependToCache(new System.Type[] {})]
  public IEnumerable Get()
  {
    this.cache._AllowInsert = true;
    this.cache._AllowUpdate = true;
    object current = this.cache.Current;
    if (current != null)
    {
      if (this.cache.Locate(current) == null)
      {
        try
        {
          this.cache.Insert(current);
        }
        catch
        {
          this.cache.SetStatus(current, PXEntryStatus.Inserted);
        }
      }
    }
    this.cache.IsDirty = false;
    this.cache.Version = 0;
    return this.cache.Cached;
  }

  private static void CancelPersist(PXCache sender, PXRowPersistingEventArgs e) => e.Cancel = true;
}
