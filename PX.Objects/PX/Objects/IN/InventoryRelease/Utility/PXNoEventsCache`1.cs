// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Utility.PXNoEventsCache`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.IN.InventoryRelease.Utility;

public class PXNoEventsCache<TNode> : PXCache<TNode> where TNode : class, IBqlTable, new()
{
  public PXNoEventsCache(PXGraph graph)
    : base(graph.Caches[typeof (TNode)].Graph)
  {
    ((PXCache) this)._EventsRowAttr.RowSelecting = (IPXRowSelectingSubscriber[]) null;
    ((PXCache) this)._EventsRowAttr.RowSelected = (IPXRowSelectedSubscriber[]) null;
    ((PXCache) this)._EventsRowAttr.RowInserting = (IPXRowInsertingSubscriber[]) null;
    ((PXCache) this)._EventsRowAttr.RowInserted = (IPXRowInsertedSubscriber[]) null;
    ((PXCache) this)._EventsRowAttr.RowUpdating = (IPXRowUpdatingSubscriber[]) null;
    ((PXCache) this)._EventsRowAttr.RowUpdated = (IPXRowUpdatedSubscriber[]) null;
    ((PXCache) this)._EventsRowAttr.RowDeleting = (IPXRowDeletingSubscriber[]) null;
    ((PXCache) this)._EventsRowAttr.RowDeleted = (IPXRowDeletedSubscriber[]) null;
    ((PXCache) this)._EventsRowAttr.RowPersisting = (IPXRowPersistingSubscriber[]) null;
    ((PXCache) this)._EventsRowAttr.RowPersisted = (IPXRowPersistedSubscriber[]) null;
  }
}
