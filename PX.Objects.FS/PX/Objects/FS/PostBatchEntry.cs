// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.PostBatchEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class PostBatchEntry : PXGraph<PostBatchEntry, FSPostBatch>
{
  public PXSelect<FSPostBatch> PostBatchRecords;

  public virtual FSPostBatch InitFSPostBatch(
    int? billingCycleID,
    DateTime? invoiceDate,
    string postTo,
    DateTime? upToDate,
    string invoicePeriodID)
  {
    FSPostBatch fsPostBatch = new FSPostBatch();
    fsPostBatch.QtyDoc = new int?(0);
    fsPostBatch.BillingCycleID = billingCycleID;
    DateTime dateTime1 = invoiceDate.Value;
    int year1 = dateTime1.Year;
    dateTime1 = invoiceDate.Value;
    int month1 = dateTime1.Month;
    dateTime1 = invoiceDate.Value;
    int day1 = dateTime1.Day;
    fsPostBatch.InvoiceDate = new DateTime?(new DateTime(year1, month1, day1, 0, 0, 0));
    fsPostBatch.PostTo = postTo;
    DateTime? nullable;
    if (!upToDate.HasValue)
    {
      nullable = upToDate;
    }
    else
    {
      int year2 = upToDate.Value.Year;
      DateTime dateTime2 = upToDate.Value;
      int month2 = dateTime2.Month;
      dateTime2 = upToDate.Value;
      int day2 = dateTime2.Day;
      nullable = new DateTime?(new DateTime(year2, month2, day2, 0, 0, 0));
    }
    fsPostBatch.UpToDate = nullable;
    fsPostBatch.CutOffDate = new DateTime?();
    fsPostBatch.FinPeriodID = invoicePeriodID;
    fsPostBatch.Status = "T";
    return fsPostBatch;
  }

  public virtual FSPostBatch CreatePostingBatch(
    int? billingCycleID,
    DateTime? upToDate,
    DateTime? invoiceDate,
    string invoiceFinPeriodID,
    string postTo)
  {
    ((PXSelectBase<FSPostBatch>) this.PostBatchRecords).Current = ((PXSelectBase<FSPostBatch>) this.PostBatchRecords).Insert(this.InitFSPostBatch(billingCycleID, invoiceDate, postTo, upToDate, invoiceFinPeriodID));
    ((PXAction) this.Save).Press();
    return ((PXSelectBase<FSPostBatch>) this.PostBatchRecords).Current;
  }

  public virtual void CompletePostingBatch(FSPostBatch fsPostBatchRow, int documentsQty)
  {
    fsPostBatchRow.QtyDoc = new int?(documentsQty);
    fsPostBatchRow.Status = "C";
    ((PXSelectBase<FSPostBatch>) this.PostBatchRecords).Update(fsPostBatchRow);
    ((PXAction) this.Save).Press();
  }

  public virtual IInvoiceGraph CreateInvoiceGraph(string targetScreen)
  {
    return InvoiceHelper.CreateInvoiceGraph(targetScreen);
  }

  public virtual bool AreAppointmentsPostedInSO(PXGraph graph, int? sOID)
  {
    return InvoiceHelper.AreAppointmentsPostedInSO(graph, sOID);
  }

  public virtual void DeletePostingBatch(FSPostBatch fsPostBatchRow)
  {
    int? batchId1 = fsPostBatchRow.BatchID;
    int num = 0;
    if (batchId1.GetValueOrDefault() < num & batchId1.HasValue)
      return;
    ((PXSelectBase<FSPostBatch>) this.PostBatchRecords).Current = PXResultset<FSPostBatch>.op_Implicit(((PXSelectBase<FSPostBatch>) this.PostBatchRecords).Search<FSPostBatch.batchID>((object) fsPostBatchRow.BatchID, Array.Empty<object>()));
    if (((PXSelectBase<FSPostBatch>) this.PostBatchRecords).Current == null)
      return;
    int? batchId2 = ((PXSelectBase<FSPostBatch>) this.PostBatchRecords).Current.BatchID;
    int? batchId3 = fsPostBatchRow.BatchID;
    if (!(batchId2.GetValueOrDefault() == batchId3.GetValueOrDefault() & batchId2.HasValue == batchId3.HasValue))
      return;
    IInvoiceGraph invoiceGraph = this.CreateInvoiceGraph(fsPostBatchRow.PostTo);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (PXResult<FSCreatedDoc> pxResult in PXSelectBase<FSCreatedDoc, PXSelect<FSCreatedDoc, Where<FSCreatedDoc.batchID, Equal<Required<FSCreatedDoc.batchID>>>>.Config>.Select(invoiceGraph.GetGraph(), new object[1]
      {
        (object) fsPostBatchRow.BatchID
      }))
      {
        FSCreatedDoc fsCreatedDocRow = PXResult<FSCreatedDoc>.op_Implicit(pxResult);
        if (fsCreatedDocRow.PostTo != fsPostBatchRow.PostTo)
          throw new PXException("The module specified in the document {0} differs from the one specified in the batch {1}.", new object[2]
          {
            (object) fsCreatedDocRow.PostTo,
            (object) fsPostBatchRow.PostTo
          });
        invoiceGraph.DeleteDocument(fsCreatedDocRow);
      }
      transactionScope.Complete();
    }
  }
}
