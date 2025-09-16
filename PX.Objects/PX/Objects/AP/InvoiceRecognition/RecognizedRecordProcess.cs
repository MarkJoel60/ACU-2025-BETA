// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.RecognizedRecordProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CloudServices;
using PX.CloudServices.DAC;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP.InvoiceRecognition.DAC;
using PX.Objects.EP;
using PX.SM;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AP.InvoiceRecognition;

[PXInternalUseOnly]
public class RecognizedRecordProcess : PXGraph<
#nullable disable
RecognizedRecordProcess>
{
  public PXFilter<RecognizedRecordProcess.RecognizedRecordFilter> Filter;
  public PXCancel<RecognizedRecordProcess.RecognizedRecordFilter> Cancel;
  public PXAction<RecognizedRecordProcess.RecognizedRecordFilter> ViewDocument;
  public PXFilteredProcessing<RecognizedRecordForProcessing, RecognizedRecordProcess.RecognizedRecordFilter, PX.Data.Where<BqlOperand<
  #nullable enable
  RecognizedRecordForProcessing.createdDateTime, IBqlDateTime>.IsLess<
  #nullable disable
  BqlField<
  #nullable enable
  RecognizedRecordProcess.RecognizedRecordFilter.createdBefore, IBqlDateTime>.FromCurrent>>> Records;

  public RecognizedRecordProcess()
  {
    this.Records.SetSelected<RecognizedRecordForProcessing.selected>();
    this.Records.SetProcessDelegate(new PXProcessingBase<RecognizedRecordForProcessing>.ProcessItemDelegate(RecognizedRecordProcess.DeleteRecognizedRecord));
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXUIField(DisplayName = "Created Date")]
  protected virtual void RecognizedRecordForProcessing_CreatedDateTime_CacheAttached(
  #nullable disable
  PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Replace)]
  [Owner]
  protected virtual void RecognizedRecordForProcessing_Owner_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Document Link", Visible = true)]
  protected virtual void RecognizedRecordForProcessing_DocumentLink_CacheAttached(PXCache sender)
  {
  }

  public IEnumerable records()
  {
    IEnumerable<string> fields = this.Records.Cache.BqlFields.Select<System.Type, string>((Func<System.Type, string>) (f => f.Name)).Except<string>((IEnumerable<string>) IncomingDocumentsProcess.JsonFields);
    if (this.Filter.Current.ShowUnprocessedRecords.GetValueOrDefault())
    {
      PXView view = new PXView((PXGraph) this, this.Records.View.IsReadOnly, this.Records.View.BqlSelect);
      int startRow = PXView.StartRow;
      int totalRows = 0;
      using (new PXFieldScope(view, fields))
      {
        List<object> objectList = view.Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, (PXFilterRow[]) PXView.Filters, ref startRow, PXView.MaximumRows, ref totalRows);
        PXView.StartRow = 0;
        return (IEnumerable) objectList;
      }
    }
    BqlCommand select = this.Records.View.BqlSelect.WhereAnd<PX.Data.Where<BqlOperand<RecognizedRecord.status, IBqlString>.IsEqual<P.AsString>>>();
    object[] parameters = new object[1]{ (object) "P" };
    object[] currents = new object[1]
    {
      (object) this.Filter.Current
    };
    PXView view1 = new PXView((PXGraph) this, this.Records.View.IsReadOnly, select);
    int startRow1 = PXView.StartRow;
    int totalRows1 = 0;
    using (new PXFieldScope(view1, fields))
    {
      List<object> objectList = view1.Select(currents, parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, (PXFilterRow[]) PXView.Filters, ref startRow1, PXView.MaximumRows, ref totalRows1);
      PXView.StartRow = 0;
      return (IEnumerable) objectList;
    }
  }

  internal static void DeleteRecognizedRecord(RecognizedRecordForProcessing record)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      PXCache cache;
      if (record.EntityType == "INV")
      {
        APInvoiceRecognitionEntry instance;
        APInvoiceRecognitionEntry recognitionEntry = instance = PXGraph.CreateInstance<APInvoiceRecognitionEntry>();
        object[] objArray = new object[1]
        {
          (object) record.RefNbr
        };
        APRecognizedInvoice recognizedInvoice;
        if (!(recognizedInvoice = (APRecognizedInvoice) PXSelectBase<APRecognizedInvoice, PXViewOf<APRecognizedInvoice>.BasedOn<SelectFromBase<APRecognizedInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<APRecognizedInvoice.recognizedRecordRefNbr, IBqlGuid>.IsEqual<P.AsGuid>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) instance, (object[]) null, objArray)).ReleasedOrPrebooked.GetValueOrDefault())
        {
          recognitionEntry.Document.Current = recognizedInvoice;
          recognitionEntry.Delete.Press();
          cache = recognitionEntry.RecognizedRecords.Cache;
          goto label_5;
        }
      }
      PXGraph instance1 = PXGraph.CreateInstance<PXGraph>();
      cache = instance1.Caches[typeof (RecognizedRecord)];
      record.RecognitionResult = (string) null;
      cache.PersistUpdated((object) record);
      cache.ResetPersisted((object) record);
      instance1.SelectTimeStamp();
      cache.PersistDeleted((object) record);
label_5:
      if (record.Status != "P")
        RecognizedRecordProcess.DeleteFileNotes(cache, record);
      transactionScope.Complete();
    }
  }

  private static void DeleteFileNotes(PXCache cache, RecognizedRecordForProcessing record)
  {
    foreach (Guid fileNote in PXNoteAttribute.GetFileNotes(cache, (object) record))
    {
      if (PXSelectBase<NoteDoc, PXViewOf<NoteDoc>.BasedOn<SelectFromBase<NoteDoc, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<NoteDoc.fileID, IBqlGuid>.IsEqual<P.AsGuid>>>.ReadOnly.Config>.Select(cache.Graph, (object) fileNote).Count <= 1)
        UploadFileMaintenance.DeleteFile(new Guid?(fileNote));
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<RecognizedRecordForProcessing.documentLink> e)
  {
    if (!(e.Row is RecognizedRecordForProcessing row) || !row.DocumentLink.HasValue)
      return;
    string str = string.Empty;
    if (row.EntityType == Models.KnownModels[Models.ApInvoicesModel].DocumentType)
      str = this.GetAPDocumentLinkStateValue(row.DocumentLink);
    else if (row.EntityType == Models.KnownModels[Models.ReceiptModel].DocumentType)
      str = this.GetReceiptDocumentLinkStateValue(row.DocumentLink);
    e.ReturnValue = (object) (str ?? string.Empty);
  }

  private EPExpenseClaimDetails GetExpenseClaimDetail(Guid? noteId)
  {
    return (EPExpenseClaimDetails) PXSelectBase<EPExpenseClaimDetails, PXViewOf<EPExpenseClaimDetails>.BasedOn<SelectFromBase<EPExpenseClaimDetails, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<EPExpenseClaimDetails.noteID, IBqlGuid>.IsEqual<P.AsGuid>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) noteId);
  }

  private string GetAPDocumentLinkStateValue(Guid? noteId)
  {
    APRegister apRegister = (APRegister) PXSelectBase<APRegister, PXViewOf<APRegister>.BasedOn<SelectFromBase<APRegister, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<APRegister.noteID, IBqlGuid>.IsEqual<P.AsGuid>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) noteId);
    return apRegister == null ? (string) null : $"{apRegister.DocType} {apRegister.RefNbr}";
  }

  private string GetReceiptDocumentLinkStateValue(Guid? noteId)
  {
    EPExpenseClaimDetails expenseClaimDetail = this.GetExpenseClaimDetail(noteId);
    return expenseClaimDetail == null ? (string) null : "ECD " + expenseClaimDetail.ClaimDetailCD;
  }

  [PXButton]
  [PXUIField(Visible = false)]
  protected virtual void viewDocument()
  {
    RecognizedRecordForProcessing current = this.Records.Current;
    if (current == null || !current.DocumentLink.HasValue)
      return;
    if (current.EntityType == Models.KnownModels[Models.ApInvoicesModel].DocumentType)
    {
      this.ViewAPDocument(current.DocumentLink);
    }
    else
    {
      if (!(current.EntityType == Models.KnownModels[Models.ReceiptModel].DocumentType))
        return;
      this.ViewReceipt(current.DocumentLink);
    }
  }

  private void ViewAPDocument(Guid? noteId)
  {
    APInvoice apInvoice = (APInvoice) PXSelectBase<APInvoice, PXViewOf<APInvoice>.BasedOn<SelectFromBase<APInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<APInvoice.noteID, IBqlGuid>.IsEqual<P.AsGuid>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) noteId);
    if (apInvoice != null)
    {
      APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
      instance.Document.Current = apInvoice;
      throw new PXRedirectRequiredException((PXGraph) instance, (string) null);
    }
  }

  private void ViewReceipt(Guid? noteId)
  {
    EPExpenseClaimDetails expenseClaimDetail = this.GetExpenseClaimDetail(noteId);
    if (expenseClaimDetail != null)
    {
      ExpenseClaimDetailEntry instance = PXGraph.CreateInstance<ExpenseClaimDetailEntry>();
      instance.ClaimDetails.Current = expenseClaimDetail;
      throw new PXRedirectRequiredException((PXGraph) instance, (string) null);
    }
  }

  [PXHidden]
  public class RecognizedRecordFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXUIField(DisplayName = "Created Before")]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXDBDate]
    public virtual System.DateTime? CreatedBefore { get; set; }

    [PXUIField(DisplayName = "Show Unprocessed Records")]
    [PXDBBool]
    public virtual bool? ShowUnprocessedRecords { get; set; }

    public abstract class createdBefore : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      RecognizedRecordProcess.RecognizedRecordFilter.createdBefore>
    {
    }

    public abstract class showUnprocessedRecords : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      RecognizedRecordProcess.RecognizedRecordFilter.showUnprocessedRecords>
    {
    }
  }
}
