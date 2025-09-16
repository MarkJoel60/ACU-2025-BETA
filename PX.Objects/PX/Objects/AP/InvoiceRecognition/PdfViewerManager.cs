// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.PdfViewerManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CloudServices.DAC;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition;

[PXInternalUseOnly]
public class PdfViewerManager : PXGraph<PdfViewerManager>
{
  public PXFilter<PdfFileInfo> File;

  public virtual void _(
    PX.Data.Events.FieldUpdated<PdfFileInfo.recognizedRecordRefNbr> e)
  {
    if (!(e.Row is PdfFileInfo row))
      return;
    Guid? nullable = row.RecognizedRecordRefNbr;
    if (!nullable.HasValue)
      return;
    row.FileId = this.GetFileId(row.RecognizedRecordRefNbr);
    nullable = row.FileId;
    if (!nullable.HasValue)
      return;
    nullable = row.FileId;
    UploadFile file = APInvoiceRecognitionEntry.GetFile((PXGraph) this, nullable.Value);
    if (file == null)
      return;
    nullable = row.FileId;
    FileInfo fileInfo1 = new FileInfo(nullable.Value, file.Name, (string) null, file.Data);
    PXSessionState.Indexer<FileInfo> fileInfo2 = PXContext.SessionTyped<PXSessionStatePXData>().FileInfo;
    nullable = fileInfo1.UID;
    string key = nullable.ToString();
    FileInfo fileInfo3 = fileInfo1;
    fileInfo2[key] = fileInfo3;
  }

  private Guid? GetFileId(Guid? recognizedRecordRefNbr)
  {
    RecognizedRecord topFirst = PXSelectBase<RecognizedRecord, PXViewOf<RecognizedRecord>.BasedOn<SelectFromBase<RecognizedRecord, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<RecognizedRecord.refNbr, IBqlGuid>.IsEqual<P.AsGuid>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) recognizedRecordRefNbr).TopFirst;
    if (topFirst == null)
      return new Guid?();
    Guid[] fileNotes = PXNoteAttribute.GetFileNotes(this.Caches[typeof (RecognizedRecord)], (object) topFirst);
    return fileNotes == null || fileNotes.Length == 0 ? new Guid?() : new Guid?(fileNotes[0]);
  }
}
