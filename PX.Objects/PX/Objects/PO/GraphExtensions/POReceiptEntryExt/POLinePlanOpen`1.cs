// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.POLinePlanOpen`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public abstract class POLinePlanOpen<TGraph> : POLinePlanBase<TGraph, POLine> where TGraph : PXGraph
{
  public override void _(Events.RowUpdated<POOrder> e)
  {
    base._(e);
    if (e.Row == null || e.OldRow == null || ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<POOrder>>) e).Cache.ObjectsEqual<POOrder.hold>((object) e.Row, (object) e.OldRow))
      return;
    foreach (PXResult<POLine> pxResult in PXSelectBase<POLine, PXSelect<POLine, Where<POLine.orderType, Equal<Current<POOrder.orderType>>, And<POLine.orderNbr, Equal<Current<POOrder.orderNbr>>>>>.Config>.SelectMultiBound((PXGraph) this.Base, (object[]) new POOrder[1]
    {
      e.Row
    }, Array.Empty<object>()))
    {
      POLine poLine = PXResult<POLine>.op_Implicit(pxResult);
      GraphHelper.Caches<POLine>((PXGraph) this.Base).RaiseRowUpdated(poLine, PXCache<POLine>.CreateCopy(poLine));
    }
  }
}
