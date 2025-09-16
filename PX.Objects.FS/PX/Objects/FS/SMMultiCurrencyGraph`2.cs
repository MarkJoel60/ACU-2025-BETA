// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SMMultiCurrencyGraph`2
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using System;

#nullable disable
namespace PX.Objects.FS;

public abstract class SMMultiCurrencyGraph<TGraph, TPrimary> : MultiCurrencyGraph<TGraph, TPrimary>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  protected override string Module => "AR";

  protected override MultiCurrencyGraph<TGraph, TPrimary>.CurySourceMapping GetCurySourceMapping()
  {
    return new MultiCurrencyGraph<TGraph, TPrimary>.CurySourceMapping(typeof (PX.Objects.AR.Customer));
  }

  protected override void _(PX.Data.Events.RowSelected<PX.Objects.Extensions.MultiCurrency.Document> e)
  {
    base._(e);
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<PX.Objects.Extensions.MultiCurrency.Document.curyID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.Extensions.MultiCurrency.Document>>) e).Cache, (object) e.Row, this.IsMultyCurrencyEnabled);
    switch (((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.Extensions.MultiCurrency.Document>(e.Row))
    {
      case FSServiceOrder _:
        ServiceOrderEntry graph = (ServiceOrderEntry) ((PXSelectBase) this.Documents).Cache.Graph;
        FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) graph.ServiceOrderRecords)?.Current;
        bool flag = current != null && current.AllowInvoice.GetValueOrDefault() || current != null && current.Billed.GetValueOrDefault();
        PXUIFieldAttribute.SetEnabled<PX.Objects.Extensions.MultiCurrency.Document.curyID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.Extensions.MultiCurrency.Document>>) e).Cache, (object) e.Row, ((PXSelectBase<FSAppointment>) graph.ServiceOrderAppointments).Select(Array.Empty<object>()).Count == 0 && !flag);
        break;
      case FSAppointment fsAppointment:
        PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.Extensions.MultiCurrency.Document>>) e).Cache;
        PX.Objects.Extensions.MultiCurrency.Document row = e.Row;
        int? soid = fsAppointment.SOID;
        int num1 = 0;
        int num2 = soid.GetValueOrDefault() < num1 & soid.HasValue ? 1 : 0;
        PXUIFieldAttribute.SetEnabled<PX.Objects.Extensions.MultiCurrency.Document.curyID>(cache, (object) row, num2 != 0);
        break;
    }
  }

  public virtual bool IsMultyCurrencyEnabled
  {
    get => PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();
  }
}
