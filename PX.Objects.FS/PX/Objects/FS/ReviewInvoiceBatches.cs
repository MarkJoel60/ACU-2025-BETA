// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ReviewInvoiceBatches
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class ReviewInvoiceBatches : PXGraph<ReviewInvoiceBatches>
{
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;
  public PXCancel<FSPostBatch> Cancel;
  [PXFilterable(new Type[] {})]
  public PXProcessing<FSPostBatch, Where<FSPostBatch.status, Equal<FSPostBatch.status.temporary>>, OrderBy<Asc<FSPostBatch.createdDateTime>>> Batches;

  public ReviewInvoiceBatches()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<FSPostBatch>) this.Batches).SetProcessDelegate<PostBatchEntry>(ReviewInvoiceBatches.\u003C\u003Ec.\u003C\u003E9__3_0 ?? (ReviewInvoiceBatches.\u003C\u003Ec.\u003C\u003E9__3_0 = new PXProcessingBase<FSPostBatch>.ProcessItemDelegate<PostBatchEntry>((object) ReviewInvoiceBatches.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__3_0))));
    ((PXProcessing<FSPostBatch>) this.Batches).SetProcessCaption("Delete");
    ((PXProcessing<FSPostBatch>) this.Batches).SetProcessAllCaption("Delete All");
  }
}
