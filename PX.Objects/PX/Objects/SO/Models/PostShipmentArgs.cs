// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Models.PostShipmentArgs
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.SO.Models;

public class PostShipmentArgs
{
  public PostShipmentArgs(
    INRegisterEntryBase docgraph,
    PX.Objects.SO.SOOrderShipment orderShipment,
    PX.Objects.SO.SOOrder order,
    DocumentList<PX.Objects.IN.INRegister> list,
    PX.Objects.AR.ARInvoice invoice)
  {
    this.INRegisterEntry = docgraph;
    this.ShipmentType = orderShipment.ShipmentType;
    this.ShipmentNbr = orderShipment.ShipmentNbr;
    this.SiteID = orderShipment.SiteID;
    this.ShipDate = orderShipment.ShipDate;
    this.DefaultBranchID = order.BranchID;
    this.Confirmed = orderShipment.Confirmed;
    this.SourceDocType = order.OrderType;
    this.SourceDocNbr = order.OrderNbr;
    this.SourceDocEntry = new Lazy<PXGraph>((Func<PXGraph>) (() => (PXGraph) PXGraph.CreateInstance<SOOrderEntry>()));
    this.Documents = list;
    this.Invoice = invoice;
  }

  public string ShipmentType { get; }

  public string ShipmentNbr { get; }

  public int? SiteID { get; }

  public DateTime? ShipDate { get; }

  public bool? Confirmed { get; }

  public int? DefaultBranchID { get; }

  public string SourceDocType { get; }

  public string SourceDocNbr { get; }

  public INRegisterEntryBase INRegisterEntry { get; }

  public DocumentList<PX.Objects.IN.INRegister> Documents { get; }

  public PX.Objects.AR.ARInvoice Invoice { get; }

  public Lazy<PXGraph> SourceDocEntry { get; }
}
