// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Services.InvoicePostingContext
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.SO.Services;

public class InvoicePostingContext
{
  private IFinPeriodUtils _finPeriodUtils;
  protected Lazy<INIssueEntry> _issueEntry;
  protected Lazy<SOShipmentEntry> _shipmentEntry;
  protected Lazy<POReceiptEntry> _receiptEntry;
  protected Lazy<SOOrderEntry> _orderEntry;

  protected virtual INIssueEntry InitIssueEntry()
  {
    INIssueEntry instance = PXGraph.CreateInstance<INIssueEntry>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<INTran.inventoryID>(InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_0 ?? (InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_0 = new PXFieldVerifying((object) InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CInitIssueEntry\u003Eb__5_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<INTran.siteID>(InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_1 ?? (InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_1 = new PXFieldVerifying((object) InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CInitIssueEntry\u003Eb__5_1))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<INTran.locationID>(InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_2 ?? (InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_2 = new PXFieldVerifying((object) InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CInitIssueEntry\u003Eb__5_2))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<INTran.sOOrderNbr>(InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_3 ?? (InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_3 = new PXFieldVerifying((object) InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CInitIssueEntry\u003Eb__5_3))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<INTran.sOShipmentNbr>(InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_4 ?? (InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_4 = new PXFieldVerifying((object) InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CInitIssueEntry\u003Eb__5_4))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<INTran.pOReceiptNbr>(InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_5 ?? (InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_5 = new PXFieldVerifying((object) InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CInitIssueEntry\u003Eb__5_5))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<INTran.projectID>(InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_6 ?? (InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_6 = new PXFieldVerifying((object) InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CInitIssueEntry\u003Eb__5_6))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<INTran.taskID>(InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_7 ?? (InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_7 = new PXFieldVerifying((object) InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CInitIssueEntry\u003Eb__5_7))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<INTranSplit.inventoryID>(InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_8 ?? (InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_8 = new PXFieldVerifying((object) InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CInitIssueEntry\u003Eb__5_8))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<INTranSplit.siteID>(InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_9 ?? (InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_9 = new PXFieldVerifying((object) InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CInitIssueEntry\u003Eb__5_9))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<INTranSplit.locationID>(InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_10 ?? (InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9__5_10 = new PXFieldVerifying((object) InvoicePostingContext.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CInitIssueEntry\u003Eb__5_10))));
    // ISSUE: method pointer
    ((PXGraph) instance).RowPersisting.AddHandler<PX.Objects.IN.INRegister>(new PXRowPersisting((object) this, __methodptr(\u003CInitIssueEntry\u003Eb__5_11)));
    return instance;
  }

  protected virtual SOShipmentEntry InitShipmentEntry()
  {
    SOShipmentEntry instance = PXGraph.CreateInstance<SOShipmentEntry>();
    instance.MergeCachesWithINRegisterEntry((INRegisterEntryBase) this._issueEntry.Value);
    return instance;
  }

  protected virtual POReceiptEntry InitPOReceiptEntry() => PXGraph.CreateInstance<POReceiptEntry>();

  protected virtual SOOrderEntry InitOrderEntry() => PXGraph.CreateInstance<SOOrderEntry>();

  public InvoicePostingContext(IFinPeriodUtils finPeriodUtils)
  {
    this._finPeriodUtils = finPeriodUtils;
    this._issueEntry = new Lazy<INIssueEntry>(new Func<INIssueEntry>(this.InitIssueEntry));
    this._shipmentEntry = new Lazy<SOShipmentEntry>(new Func<SOShipmentEntry>(this.InitShipmentEntry));
    this._receiptEntry = new Lazy<POReceiptEntry>(new Func<POReceiptEntry>(this.InitPOReceiptEntry));
    this._orderEntry = new Lazy<SOOrderEntry>(new Func<SOOrderEntry>(this.InitOrderEntry));
  }

  public virtual INIssueEntry IssueEntry => this._issueEntry.Value;

  public virtual SOShipmentEntry GetClearShipmentEntry()
  {
    if (this._shipmentEntry.IsValueCreated)
      ((PXGraph) this._shipmentEntry.Value).Clear();
    return this._shipmentEntry.Value;
  }

  public virtual POReceiptEntry GetClearPOReceiptEntry()
  {
    if (this._receiptEntry.IsValueCreated)
      ((PXGraph) this._receiptEntry.Value).Clear();
    return this._receiptEntry.Value;
  }

  public virtual SOOrderEntry GetClearOrderEntry()
  {
    if (this._orderEntry.IsValueCreated)
      ((PXGraph) this._orderEntry.Value).Clear();
    return this._orderEntry.Value;
  }
}
