// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.InvoiceGraphExtension`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;
using PX.Objects.GL;
using System;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract;

public abstract class InvoiceGraphExtension<TGraph, TAdjust> : 
  InvoiceBaseGraphExtension<TGraph, Invoice, InvoiceMapping>
  where TGraph : PXGraph
  where TAdjust : class, IBqlTable, IFinAdjust, new()
{
  public abstract PXSelectBase<TAdjust> AppliedAdjustments { get; }

  protected override void _(Events.RowUpdated<Invoice> e)
  {
    base._(e);
    if (!this.ShouldUpdateAdjustmentsOnDocumentUpdated(e))
      return;
    foreach (PXResult<TAdjust> pxResult in this.AppliedAdjustments.Select(Array.Empty<object>()))
    {
      TAdjust row = PXResult<TAdjust>.op_Implicit(pxResult);
      if (!((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<Invoice>>) e).Cache.ObjectsEqual<Document.branchID>((object) e.Row, (object) e.OldRow))
        ((PXSelectBase) this.AppliedAdjustments).Cache.SetDefaultExt<Adjust.adjdBranchID>((object) row);
      if (!((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<Invoice>>) e).Cache.ObjectsEqual<Document.headerTranPeriodID>((object) e.Row, (object) e.OldRow))
      {
        FinPeriodIDAttribute.DefaultPeriods<Adjust.adjgFinPeriodID>(((PXSelectBase) this.AppliedAdjustments).Cache, (object) row);
        FinPeriodIDAttribute.DefaultPeriods<Adjust.adjdFinPeriodID>(((PXSelectBase) this.AppliedAdjustments).Cache, (object) row);
      }
      if (((PXSelectBase) this.AppliedAdjustments).Cache is PXModelExtension<Adjust> cache)
        cache.UpdateExtensionMapping((object) row);
      GraphHelper.MarkUpdated(((PXSelectBase) this.AppliedAdjustments).Cache, (object) row);
    }
  }

  protected virtual bool ShouldUpdateAdjustmentsOnDocumentUpdated(Events.RowUpdated<Invoice> e)
  {
    return this.ShouldUpdateDetailsOnDocumentUpdated(e);
  }
}
