// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.ShipmentInvoices
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO;

[PXInternalUseOnly]
public class ShipmentInvoices : InvoiceList
{
  private readonly Dictionary<string, List<int>> typedInvoices;

  public ShipmentInvoices(PXGraph graph)
    : base(graph)
  {
    this.typedInvoices = new Dictionary<string, List<int>>();
  }

  public override void Add(PX.Objects.AR.ARInvoice item0, SOInvoice item1, PX.Objects.CM.Extensions.CurrencyInfo curyInfo)
  {
    base.Add(item0, item1, curyInfo);
    List<int> intList;
    if (!this.typedInvoices.TryGetValue(item0.DocType, out intList))
      this.typedInvoices.Add(item0.DocType, intList = new List<int>());
    intList.Add(this.Count - 1);
  }

  public IEnumerable<PXResult<PX.Objects.AR.ARInvoice, SOInvoice>> GetInvoices(string docType)
  {
    ShipmentInvoices shipmentInvoices = this;
    List<int> intList;
    if (shipmentInvoices.typedInvoices.TryGetValue(docType, out intList))
    {
      foreach (int index in intList)
      {
        // ISSUE: explicit non-virtual call
        yield return __nonvirtual (shipmentInvoices[index]);
      }
    }
  }
}
