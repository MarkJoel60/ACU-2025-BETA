// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceTransactionReleasingArgs
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.DR;
using PX.Objects.GL;
using PX.Objects.PO;

#nullable disable
namespace PX.Objects.AP;

public class InvoiceTransactionReleasingArgs
{
  protected APTran _transaction;

  public PXResult<APTran, APTax, PX.Objects.TX.Tax, DRDeferredCode, LandedCostCode, PX.Objects.IN.InventoryItem, APTaxTran> TransactionResult { get; set; }

  public virtual APTax TaxDetail => (APTax) this.TransactionResult;

  public virtual APTaxTran TaxTransaction => (APTaxTran) this.TransactionResult;

  public virtual PX.Objects.TX.Tax Tax => (PX.Objects.TX.Tax) this.TransactionResult;

  public virtual DRDeferredCode DeferredCode => (DRDeferredCode) this.TransactionResult;

  public virtual LandedCostCode LandedCostCode => (LandedCostCode) this.TransactionResult;

  public virtual PX.Objects.IN.InventoryItem Inventory => (PX.Objects.IN.InventoryItem) this.TransactionResult;

  public virtual APTran Transaction
  {
    get => this._transaction ?? (APTran) this.TransactionResult;
    set => this._transaction = value;
  }

  public PX.Objects.GL.GLTran GLTransaction { get; set; }

  public ARReleaseProcess.Amount PostedAmount { get; set; }

  public APRegister Register { get; set; }

  public APInvoice Invoice { get; set; }

  public virtual bool IsPrebookVoiding
  {
    get => this.Invoice.DocType == "VQC" && !string.IsNullOrEmpty(this.Invoice.PrebookBatchNbr);
  }

  public JournalEntry JournalEntry { get; set; }

  public PX.Objects.CM.Extensions.CurrencyInfo CurrencyInfo { get; set; }

  public bool IsPrebooking { get; set; }
}
