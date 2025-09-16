// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.ReleaseContext
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.IN;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using System;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class ReleaseContext : PXGraphExtension<POReceiptEntry>
{
  protected Lazy<INReceiptEntry> _receiptEntry;
  protected Lazy<INIssueEntry> _issueEntry;
  protected Lazy<APInvoiceEntry> _invoiceEntry;

  public static bool IsActive() => true;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this._receiptEntry = new Lazy<INReceiptEntry>(new Func<INReceiptEntry>(this.InitReceiptEntry));
    this._issueEntry = new Lazy<INIssueEntry>(new Func<INIssueEntry>(this.InitIssueEntry));
    this._invoiceEntry = new Lazy<APInvoiceEntry>(new Func<APInvoiceEntry>(this.InitAPInvoiceEntry));
  }

  protected virtual INReceiptEntry InitReceiptEntry()
  {
    INReceiptEntry instance = PXGraph.CreateInstance<INReceiptEntry>();
    ((PXGraph) instance).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter)] = ((PXGraph) this.Base).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter)];
    ((PXGraph) instance).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter)] = ((PXGraph) this.Base).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter)];
    ((PXGraph) instance).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter)] = ((PXGraph) this.Base).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter)];
    ((PXGraph) instance).Caches[typeof (SiteLotSerial)] = ((PXGraph) this.Base).Caches[typeof (SiteLotSerial)];
    ((PXGraph) instance).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial)] = ((PXGraph) this.Base).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial)];
    ((PXGraph) instance).Views.Caches.Remove(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter));
    ((PXGraph) instance).Views.Caches.Remove(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter));
    ((PXGraph) instance).Views.Caches.Remove(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter));
    ((PXGraph) instance).Views.Caches.Remove(typeof (SiteLotSerial));
    ((PXGraph) instance).Views.Caches.Remove(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<INTran.inventoryID>(ReleaseContext.\u003C\u003Ec.\u003C\u003E9__5_0 ?? (ReleaseContext.\u003C\u003Ec.\u003C\u003E9__5_0 = new PXFieldVerifying((object) ReleaseContext.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CInitReceiptEntry\u003Eb__5_0))));
    return instance;
  }

  protected virtual INIssueEntry InitIssueEntry()
  {
    INIssueEntry instance = PXGraph.CreateInstance<INIssueEntry>();
    ((PXGraph) instance).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter)] = ((PXGraph) this.Base).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter)];
    ((PXGraph) instance).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter)] = ((PXGraph) this.Base).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter)];
    ((PXGraph) instance).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter)] = ((PXGraph) this.Base).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter)];
    ((PXGraph) instance).Caches[typeof (SiteLotSerial)] = ((PXGraph) this.Base).Caches[typeof (SiteLotSerial)];
    ((PXGraph) instance).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial)] = ((PXGraph) this.Base).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial)];
    ((PXGraph) instance).Views.Caches.Remove(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter));
    ((PXGraph) instance).Views.Caches.Remove(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter));
    ((PXGraph) instance).Views.Caches.Remove(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter));
    ((PXGraph) instance).Views.Caches.Remove(typeof (SiteLotSerial));
    ((PXGraph) instance).Views.Caches.Remove(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldDefaulting.AddHandler<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter.negAvailQty>(ReleaseContext.\u003C\u003Ec.\u003C\u003E9__6_0 ?? (ReleaseContext.\u003C\u003Ec.\u003C\u003E9__6_0 = new PXFieldDefaulting((object) ReleaseContext.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CInitIssueEntry\u003Eb__6_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<INTran.inventoryID>(ReleaseContext.\u003C\u003Ec.\u003C\u003E9__6_1 ?? (ReleaseContext.\u003C\u003Ec.\u003C\u003E9__6_1 = new PXFieldVerifying((object) ReleaseContext.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CInitIssueEntry\u003Eb__6_1))));
    return instance;
  }

  public virtual INRegisterEntryBase GetCleanINRegisterEntryWithInsertedHeader(
    PX.Objects.PO.POReceipt doc,
    bool isCancellation = false)
  {
    bool flag = doc.ReceiptType == "RT" && doc.OrigReceiptNbr != null;
    INRegisterEntryBase withInsertedHeader;
    if (doc.ReceiptType == "RN" | flag | isCancellation)
    {
      if (this._issueEntry.IsValueCreated)
        ((PXGraph) this._issueEntry.Value).Clear();
      withInsertedHeader = (INRegisterEntryBase) this._issueEntry.Value;
    }
    else
    {
      if (this._receiptEntry.IsValueCreated)
        ((PXGraph) this._receiptEntry.Value).Clear();
      withInsertedHeader = (INRegisterEntryBase) this._receiptEntry.Value;
    }
    ((PXSelectBase<INSetup>) withInsertedHeader.insetup).Current.RequireControlTotal = new bool?(false);
    ((PXSelectBase<INSetup>) withInsertedHeader.insetup).Current.HoldEntry = new bool?(false);
    PX.Objects.IN.INRegister inRegister = new PX.Objects.IN.INRegister()
    {
      BranchID = doc.BranchID,
      SiteID = new int?(),
      TranDate = doc.ReceiptDate,
      FinPeriodID = doc.FinPeriodID,
      OrigModule = "PO",
      IsCorrection = new bool?(flag | isCancellation),
      OrigReceiptNbr = flag | isCancellation ? this.GetOrigReceiptNbr(doc) : (string) null
    };
    withInsertedHeader.INRegisterDataMember.Insert(inRegister);
    return withInsertedHeader;
  }

  protected virtual APInvoiceEntry InitAPInvoiceEntry() => PXGraph.CreateInstance<APInvoiceEntry>();

  public virtual APInvoiceEntry GetCleanAPInvoiceEntry()
  {
    if (this._invoiceEntry.IsValueCreated)
      ((PXGraph) this._invoiceEntry.Value).Clear();
    ((PXSelectBase<APSetup>) this._invoiceEntry.Value.APSetup).Current.RequireControlTotal = new bool?(false);
    ((PXSelectBase<APSetup>) this._invoiceEntry.Value.APSetup).Current.RequireControlTaxTotal = new bool?(false);
    if (((PXSelectBase<POSetup>) this.Base.posetup).Current.AutoReleaseAP.GetValueOrDefault())
      ((PXSelectBase<APSetup>) this._invoiceEntry.Value.APSetup).Current.HoldEntry = new bool?(false);
    return this._invoiceEntry.Value;
  }

  private string GetOrigReceiptNbr(PX.Objects.PO.POReceipt doc)
  {
    while (doc != null && doc.OrigReceiptNbr != null && doc.POType != "DP" && doc.InvtRefNbr == null)
      doc = PX.Objects.PO.POReceipt.PK.Find((PXGraph) this.Base, "RT", doc.OrigReceiptNbr);
    if (doc.InvtRefNbr == null)
      return (string) null;
    PX.Objects.IN.INRegister inRegister = PX.Objects.IN.INRegister.PK.Find((PXGraph) this.Base, doc.InvtDocType, doc.InvtRefNbr);
    string origReceiptNbr = inRegister?.OrigReceiptNbr;
    if (origReceiptNbr != null)
      return origReceiptNbr;
    return inRegister?.RefNbr;
  }
}
