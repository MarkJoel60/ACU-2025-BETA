// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.IInvoiceGraph
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public interface IInvoiceGraph
{
  void CreateInvoice(
    PXGraph graphProcess,
    List<DocLineExt> docLines,
    short invtMult,
    DateTime? invoiceDate,
    string invoiceFinPeriodID,
    OnDocumentHeaderInsertedDelegate onDocumentHeaderInserted,
    OnTransactionInsertedDelegate onTransactionInserted,
    PXQuickProcess.ActionFlow quickProcessFlow);

  FSCreatedDoc PressSave(int batchID, List<DocLineExt> docLines, BeforeSaveDelegate beforeSave);

  void Clear();

  PXGraph GetGraph();

  void DeleteDocument(FSCreatedDoc fsCreatedDocRow);

  void CleanPostInfo(PXGraph cleanerGraph, FSPostDet fsPostDetRow);

  List<MessageHelper.ErrorInfo> GetErrorInfo();

  bool IsInvoiceProcessRunning { get; set; }
}
