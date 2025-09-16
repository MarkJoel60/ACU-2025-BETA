// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCosts.APInvoiceWrapper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.AP;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO.LandedCosts;

public class APInvoiceWrapper
{
  public APInvoiceWrapper(
    PX.Objects.AP.APInvoice doc,
    ICollection<PX.Objects.AP.APTran> transactions,
    ICollection<APTaxTran> taxes)
  {
    this.Document = doc;
    this.Transactions = transactions;
    this.Taxes = taxes;
  }

  public PX.Objects.AP.APInvoice Document { get; }

  public ICollection<PX.Objects.AP.APTran> Transactions { get; }

  public ICollection<APTaxTran> Taxes { get; }
}
