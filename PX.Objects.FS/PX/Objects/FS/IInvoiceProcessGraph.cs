// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.IInvoiceProcessGraph
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public interface IInvoiceProcessGraph
{
  OnDocumentHeaderInsertedDelegate OnDocumentHeaderInserted { get; set; }

  OnTransactionInsertedDelegate OnTransactionInserted { get; set; }

  BeforeSaveDelegate BeforeSave { get; set; }

  AfterCreateInvoiceDelegate AfterCreateInvoice { get; set; }

  void Clear(PXClearOption option);

  PXGraph GetGraph();

  List<DocLineExt> GetInvoiceLines(
    Guid currentProcessID,
    int billingCycleID,
    string groupKey,
    bool getOnlyTotal,
    out Decimal? invoiceTotal,
    string postTo);

  void UpdateSourcePostDoc(
    ServiceOrderEntry soGraph,
    AppointmentEntry apptGraph,
    PXCache<FSPostDet> cacheFSPostDet,
    FSPostBatch fsPostBatchRow,
    FSPostDoc fsPostDocRow);
}
