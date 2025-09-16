// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.LateRowUpdatedAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common;

/// <summary>
/// Represents an event handler for the RowUpdated event that subscribes as late as possible.
/// </summary>
public abstract class LateRowUpdatedAttribute : PXEventSubscriberAttribute
{
  public Type TargetTable { get; set; }

  public virtual void CacheAttached(PXCache cache)
  {
    base.CacheAttached(cache);
    // ISSUE: method pointer
    cache.Graph.Initialized -= new PXGraphInitializedDelegate((object) this, __methodptr(LateSubscription));
    // ISSUE: method pointer
    cache.Graph.Initialized += new PXGraphInitializedDelegate((object) this, __methodptr(LateSubscription));
  }

  private void LateSubscription(PXGraph graph)
  {
    PXGraph.RowUpdatedEvents rowUpdated1 = graph.RowUpdated;
    Type type1 = this.TargetTable;
    if ((object) type1 == null)
      type1 = this.BqlTable;
    LateRowUpdatedAttribute updatedAttribute1 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated1 = new PXRowUpdated((object) updatedAttribute1, __vmethodptr(updatedAttribute1, LateRowUpdated));
    rowUpdated1.RemoveHandler(type1, pxRowUpdated1);
    PXGraph.RowUpdatedEvents rowUpdated2 = graph.RowUpdated;
    Type type2 = this.TargetTable;
    if ((object) type2 == null)
      type2 = this.BqlTable;
    LateRowUpdatedAttribute updatedAttribute2 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated2 = new PXRowUpdated((object) updatedAttribute2, __vmethodptr(updatedAttribute2, LateRowUpdated));
    rowUpdated2.AddHandler(type2, pxRowUpdated2);
  }

  protected abstract void LateRowUpdated(PXCache cache, PXRowUpdatedEventArgs args);
}
