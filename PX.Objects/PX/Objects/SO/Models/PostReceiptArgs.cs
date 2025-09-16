// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Models.PostReceiptArgs
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.CS;
using PX.Objects.IN;

#nullable disable
namespace PX.Objects.SO.Models;

public class PostReceiptArgs
{
  public INIssueEntry INIssueGraph { get; set; }

  public PX.Objects.SO.SOOrderShipment SOOrderShipment { get; set; }

  public PX.Objects.SO.SOOrder SOOrder { get; set; }

  public PX.Objects.AR.ARInvoice Invoice { get; set; }

  public DocumentList<PX.Objects.IN.INRegister> CreatedDocuments { get; set; }

  public PX.Objects.PO.POReceipt Receipt { get; set; }

  public bool IsReversal { get; set; }

  public PostReceiptArgs(
    INIssueEntry INIssueGraph,
    PX.Objects.SO.SOOrderShipment SOOrderShipment,
    PX.Objects.SO.SOOrder SOOrder,
    PX.Objects.AR.ARInvoice Invoice,
    DocumentList<PX.Objects.IN.INRegister> CreatedDocuments,
    PX.Objects.PO.POReceipt Receipt = null,
    bool IsReversal = false)
  {
    this.INIssueGraph = INIssueGraph;
    this.SOOrderShipment = SOOrderShipment;
    this.SOOrder = SOOrder;
    this.Invoice = Invoice;
    this.CreatedDocuments = CreatedDocuments;
    this.Receipt = Receipt;
    this.IsReversal = IsReversal;
  }
}
