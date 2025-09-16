// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ContractPostBatchEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class ContractPostBatchEntry : PXGraph<ContractPostBatchEntry, FSContractPostBatch>
{
  public PXSelect<FSContractPostBatch> PostBatchRecords;

  protected virtual FSContractPostBatch InitFSPostBatch(
    DateTime? invoiceDate,
    string postTo,
    DateTime? upToDate,
    string invoicePeriodID)
  {
    FSContractPostBatch contractPostBatch = new FSContractPostBatch();
    DateTime dateTime = invoiceDate.Value;
    int year = dateTime.Year;
    dateTime = invoiceDate.Value;
    int month = dateTime.Month;
    dateTime = invoiceDate.Value;
    int day = dateTime.Day;
    contractPostBatch.InvoiceDate = new DateTime?(new DateTime(year, month, day, 0, 0, 0));
    contractPostBatch.PostTo = postTo;
    contractPostBatch.UpToDate = upToDate.HasValue ? new DateTime?(new DateTime(upToDate.Value.Year, upToDate.Value.Month, upToDate.Value.Day, 0, 0, 0)) : upToDate;
    contractPostBatch.FinPeriodID = invoicePeriodID;
    return contractPostBatch;
  }

  public virtual FSContractPostBatch CreatePostingBatch(
    DateTime? upToDate,
    DateTime? invoiceDate,
    string invoiceFinPeriodID,
    string postTo)
  {
    ((PXSelectBase<FSContractPostBatch>) this.PostBatchRecords).Current = ((PXSelectBase<FSContractPostBatch>) this.PostBatchRecords).Insert(this.InitFSPostBatch(invoiceDate, postTo, upToDate, invoiceFinPeriodID));
    ((PXAction) this.Save).Press();
    return ((PXSelectBase<FSContractPostBatch>) this.PostBatchRecords).Current;
  }

  public virtual void DeletePostingBatch(FSContractPostBatch fsPostBatchRow)
  {
    int? contractPostBatchId1 = fsPostBatchRow.ContractPostBatchID;
    int num = 0;
    if (contractPostBatchId1.GetValueOrDefault() < num & contractPostBatchId1.HasValue)
      return;
    ((PXSelectBase<FSContractPostBatch>) this.PostBatchRecords).Current = PXResultset<FSContractPostBatch>.op_Implicit(((PXSelectBase<FSContractPostBatch>) this.PostBatchRecords).Search<FSContractPostBatch.contractPostBatchID>((object) fsPostBatchRow.ContractPostBatchID, Array.Empty<object>()));
    if (((PXSelectBase<FSContractPostBatch>) this.PostBatchRecords).Current == null)
      return;
    int? contractPostBatchId2 = ((PXSelectBase<FSContractPostBatch>) this.PostBatchRecords).Current.ContractPostBatchID;
    int? contractPostBatchId3 = fsPostBatchRow.ContractPostBatchID;
    if (!(contractPostBatchId2.GetValueOrDefault() == contractPostBatchId3.GetValueOrDefault() & contractPostBatchId2.HasValue == contractPostBatchId3.HasValue))
      return;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      PXDatabase.Delete<FSCreatedDoc>(new PXDataFieldRestrict[1]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<FSCreatedDoc.batchID>((object) fsPostBatchRow.ContractPostBatchID)
      });
      ((PXSelectBase<FSContractPostBatch>) this.PostBatchRecords).Delete(fsPostBatchRow);
      ((PXAction) this.Save).Press();
      transactionScope.Complete();
    }
  }
}
