// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateInvoice.CRCreateInvoice_ARInvoiceEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.CS;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CR.Extensions.CRCreateInvoice;

public class CRCreateInvoice_ARInvoiceEntry : PXGraphExtension<ARInvoiceEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.customerModule>();

  public virtual void _(PX.Data.Events.RowPersisting<ARInvoice> e)
  {
    ARInvoice row = e.Row;
    if (row == null || e.Operation != 2)
      return;
    PXCache cach = ((PXGraph) this.Base).Caches[typeof (CRQuote)];
    foreach (object obj in cach.Cached)
    {
      GraphHelper.EnsureCachePersistence<CRQuote>((PXGraph) this.Base);
      if (cach.GetStatus(obj) == 5)
        ((SelectedEntityEvent<ARInvoice, CRQuote>) PXEntityEventBase<ARInvoice>.Container<ARInvoice.Events>.Select<CRQuote>((Expression<Func<ARInvoice.Events, PXEntityEvent<ARInvoice.Events, CRQuote>>>) (o => o.ARInvoiceCreatedFromQuote))).FireOn((PXGraph) this.Base, row, obj as CRQuote);
    }
  }
}
