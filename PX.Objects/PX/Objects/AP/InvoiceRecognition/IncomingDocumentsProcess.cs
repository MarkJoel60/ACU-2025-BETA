// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.IncomingDocumentsProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CloudServices.DAC;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AP.InvoiceRecognition.DAC;
using PX.SM;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace PX.Objects.AP.InvoiceRecognition;

[PXInternalUseOnly]
public class IncomingDocumentsProcess : PXGraph<
#nullable disable
IncomingDocumentsProcess>
{
  private const string _processButtonName = "Process";
  private const string ViewPdfActionName = "ViewPdf";
  internal static readonly HashSet<string> JsonFields = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
  {
    "recognitionResult",
    "recognitionFeedback"
  };
  [PXFilterable(new System.Type[] {})]
  public PXProcessingJoin<RecognizedRecordForProcessing, LeftJoin<RecognizedRecordDetail, On<RecognizedRecord.entityType, Equal<RecognizedRecordDetail.entityType>, And<RecognizedRecord.refNbr, Equal<RecognizedRecordDetail.refNbr>>>>, Where<RecognizedRecord.entityType, Equal<RecognizedRecordEntityTypeListAttribute.aPDocument>, PX.Data.And<MatchWithBranch<RecognizedRecordDetail.defaultBranchID>>>, PX.Data.OrderBy<Desc<RecognizedRecordForProcessing.createdDateTime>>> Records;
  public PXViewOf<RecognizedRecordErrorHistory>.BasedOn<SelectFromBase<RecognizedRecordErrorHistory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  RecognizedRecordErrorHistory.refNbr, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  RecognizedRecord.refNbr, IBqlGuid>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  RecognizedRecordErrorHistory.entityType, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  RecognizedRecord.entityType, IBqlString>.FromCurrent>>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  RecognizedRecordErrorHistory.createdDateTime, IBqlDateTime>.Desc>>>.ReadOnly ErrorHistory;
  public 
  #nullable disable
  PXCancel<RecognizedRecordForProcessing> Cancel;
  public PXAction<RecognizedRecordForProcessing> Insert;
  public PXAction<RecognizedRecordForProcessing> Delete;
  public PXAction<RecognizedRecordForProcessing> EditRecord;
  public PXAction<RecognizedRecordForProcessing> ViewDocument;
  public PXAction<RecognizedRecordForProcessing> ViewErrorHistory;
  public PXAction<RecognizedRecordForProcessing> SearchVendor;
  public PXAction<RecognizedRecordForProcessing> UploadFiles;

  public IncomingDocumentsProcess()
  {
    this.Records.SetProcessCaption("Recognize");
    this.Records.SetProcessAllCaption("Recognize All");
    this.Records.SetAsyncProcessDelegate(new Func<List<RecognizedRecordForProcessing>, CancellationToken, Task>(IncomingDocumentsProcess.RecognizeAsync));
    this.Actions.Move("Process", nameof (Insert));
    this.Actions.Move("Process", nameof (Delete));
    this.Actions.Move(nameof (Insert), nameof (Cancel));
    PXUIFieldAttribute.SetDisplayName<RecognizedRecordDetail.vendorID>(this.Caches[typeof (RecognizedRecordDetail)], "Recognized Vendor");
  }

  public sealed override void Configure(PXScreenConfiguration config)
  {
    IncomingDocumentsProcess.Configure(config.GetScreenConfigurationContext<IncomingDocumentsProcess, RecognizedRecordForProcessing>());
  }

  protected static void Configure(
    WorkflowContext<IncomingDocumentsProcess, RecognizedRecordForProcessing> context)
  {
    context.UpdateScreenConfigurationFor((Func<BoundedTo<IncomingDocumentsProcess, RecognizedRecordForProcessing>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<IncomingDocumentsProcess, RecognizedRecordForProcessing>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((System.Action<BoundedTo<IncomingDocumentsProcess, RecognizedRecordForProcessing>.ActionDefinition.ContainerAdjusterActions>) (actions => actions.AddNew("ViewPdf", (Func<BoundedTo<IncomingDocumentsProcess, RecognizedRecordForProcessing>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<IncomingDocumentsProcess, RecognizedRecordForProcessing>.ActionDefinition.IConfigured>) (configAction => (BoundedTo<IncomingDocumentsProcess, RecognizedRecordForProcessing>.ActionDefinition.IConfigured) configAction.DisplayName("View PDF").IsSidePanelScreen((Func<BoundedTo<IncomingDocumentsProcess, RecognizedRecordForProcessing>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<IncomingDocumentsProcess, RecognizedRecordForProcessing>.NavigationDefinition.IConfiguredSidePanel>) (sidePanelAction => sidePanelAction.NavigateToScreen<PdfViewerManager>().WithIcon("receipt").WithAssignments((System.Action<BoundedTo<IncomingDocumentsProcess, RecognizedRecordForProcessing>.NavigationParameter.IContainerFillerNavigationActionParameters>) (containerFiller => containerFiller.Add<PdfFileInfo.recognizedRecordRefNbr>((Func<BoundedTo<IncomingDocumentsProcess, RecognizedRecordForProcessing>.NavigationParameter.INeedRightOperand, BoundedTo<IncomingDocumentsProcess, RecognizedRecordForProcessing>.NavigationParameter.IConfigured>) (c => c.SetFromField<RecognizedRecord.refNbr>()))))))))))));
  }

  public IEnumerable records()
  {
    PXView view;
    IEnumerable<System.Type> fieldsAndTables;
    if (PXView.RetrieveTotalRowCount)
    {
      view = new PXView((PXGraph) this, true, (BqlCommand) new SelectFromBase<RecognizedRecordForProcessing, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<RecognizedRecord.entityType, IBqlString>.IsEqual<RecognizedRecordEntityTypeListAttribute.aPDocument>>());
      HashSet<string> filterFieldNameSet = PXView.Filters.OfType<PXFilterRow>().Select<PXFilterRow, string>((Func<PXFilterRow, string>) (f => ((IEnumerable<string>) f.DataField.Split('_')).First<string>())).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      fieldsAndTables = this.Records.Cache.BqlKeys.Union<System.Type>(this.Records.Cache.BqlFields.Where<System.Type>((Func<System.Type, bool>) (f => filterFieldNameSet.Contains(f.Name))));
    }
    else
    {
      view = new PXView((PXGraph) this, this.Records.View.IsReadOnly, this.Records.View.BqlSelect);
      fieldsAndTables = this.Records.Cache.BqlFields.Where<System.Type>((Func<System.Type, bool>) (f => !IncomingDocumentsProcess.JsonFields.Contains(f.Name))).Concat<System.Type>((IEnumerable<System.Type>) new System.Type[1]
      {
        typeof (RecognizedRecordDetail)
      });
    }
    int startRow = PXView.StartRow;
    int totalRows = 0;
    using (new PXFieldScope(view, fieldsAndTables))
    {
      List<object> objectList = view.Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, (PXFilterRow[]) PXView.Filters, ref startRow, PXView.MaximumRows, ref totalRows);
      PXView.StartRow = 0;
      return (IEnumerable) objectList;
    }
  }

  [PXEntryScreenRights(typeof (APRecognizedInvoice), "Insert")]
  [PXInsertButton]
  [PXUIField]
  protected virtual void insert()
  {
    throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<APInvoiceRecognitionEntry>(), (string) null);
  }

  [PXEntryScreenRights(typeof (APRecognizedInvoice), "Delete")]
  [PXButton(ImageKey = "Remove", ConfirmationMessage = "The selected records will be deleted.")]
  [PXUIField]
  protected virtual void delete()
  {
    this.Records.SetProcessDelegate(new PXProcessingBase<RecognizedRecordForProcessing>.ProcessItemDelegate(RecognizedRecordProcess.DeleteRecognizedRecord));
    this.Actions["Process"].PressButton();
  }

  [PXButton]
  [PXUIField(Visible = false)]
  protected virtual void editRecord()
  {
    Guid? refNbr = (Guid?) this.Records.Current?.RefNbr;
    if (!refNbr.HasValue)
      return;
    PXViewOf<APRecognizedInvoice>.BasedOn<SelectFromBase<APRecognizedInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<APRecognizedInvoice.recognizedRecordRefNbr, IBqlGuid>.IsEqual<P.AsGuid>>>.ReadOnly readOnly = new PXViewOf<APRecognizedInvoice>.BasedOn<SelectFromBase<APRecognizedInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<APRecognizedInvoice.recognizedRecordRefNbr, IBqlGuid>.IsEqual<P.AsGuid>>>.ReadOnly((PXGraph) this);
    readOnly.View.Clear();
    APRecognizedInvoice recognizedInvoice = readOnly.SelectSingle((object) refNbr);
    if (recognizedInvoice != null)
    {
      APInvoiceRecognitionEntry instance = PXGraph.CreateInstance<APInvoiceRecognitionEntry>();
      instance.Document.Current = recognizedInvoice;
      throw new PXRedirectRequiredException((PXGraph) instance, (string) null);
    }
  }

  [PXButton]
  [PXUIField(Visible = false)]
  protected virtual void viewDocument()
  {
    Guid? documentLink = (Guid?) this.Records.Current?.DocumentLink;
    if (!documentLink.HasValue)
      return;
    APInvoice apInvoice = (APInvoice) PXSelectBase<APInvoice, PXViewOf<APInvoice>.BasedOn<SelectFromBase<APInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<APInvoice.noteID, IBqlGuid>.IsEqual<P.AsGuid>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) documentLink);
    if (apInvoice != null)
    {
      APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
      instance.Document.Current = apInvoice;
      throw new PXRedirectRequiredException((PXGraph) instance, (string) null);
    }
  }

  [PXEntryScreenRights(typeof (APRecognizedInvoice), "ViewErrorHistory")]
  [PXButton(Category = "Actions")]
  [PXUIField(DisplayName = "View History", Enabled = false)]
  public virtual void viewErrorHistory()
  {
    int num = (int) this.ErrorHistory.AskExt();
  }

  [PXEntryScreenRights(typeof (APRecognizedInvoice), "SearchVendor")]
  [PXButton(Category = "Actions")]
  [PXUIField(DisplayName = "Search for Vendor", Enabled = false)]
  protected virtual void searchVendor()
  {
    this.Records.SetAsyncProcessDelegate(new Func<List<RecognizedRecordForProcessing>, CancellationToken, Task>(IncomingDocumentsProcess.SearchForVendorAsync));
    this.Actions["Process"].PressButton();
  }

  [PXButton]
  [PXUIField(DisplayName = "Upload Files", Visible = false)]
  public virtual IEnumerable uploadFiles(PXAdapter adapter)
  {
    APInvoiceRecognitionEntry instance1 = PXGraph.CreateInstance<APInvoiceRecognitionEntry>();
    UploadFileMaintenance instance2 = PXGraph.CreateInstance<UploadFileMaintenance>();
    foreach (KeyValuePair<string, object> keyValuePair in adapter.Arguments)
    {
      if (keyValuePair.Value is byte[] data1)
      {
        string key = keyValuePair.Key;
        Guid uid = Guid.NewGuid();
        string name = $"{uid}\\{key}";
        FileInfo finfo = new FileInfo(uid, name, (string) null, data1);
        if (!instance2.SaveFile(finfo))
          throw new PXException("The {0} file cannot be saved.", new object[1]
          {
            (object) key
          });
        APInvoiceRecognitionEntry recognitionEntry = instance1;
        string fileName = key;
        byte[] fileData = data1;
        Guid? nullable = finfo.UID;
        Guid fileId = nullable.Value;
        int? owner = new int?();
        nullable = new Guid?();
        Guid? noteId = nullable;
        RecognizedRecord recognizedRecord = recognitionEntry.CreateRecognizedRecord(fileName, fileData, fileId, owner: owner, noteId: noteId);
        PXNoteAttribute.ForcePassThrow<RecognizedRecord.noteID>(instance1.RecognizedRecords.Cache);
        PXCache cache = instance1.RecognizedRecords.Cache;
        RecognizedRecord data = recognizedRecord;
        Guid[] guidArray = new Guid[1];
        nullable = finfo.UID;
        guidArray[0] = nullable.Value;
        PXNoteAttribute.SetFileNotes(cache, (object) data, guidArray);
        instance1.RecognizedRecords.Cache.PersistUpdated((object) recognizedRecord);
      }
    }
    adapter.StartRow = 0;
    adapter.MaximumRows = 1;
    return adapter.Get();
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXUIField(DisplayName = "Created Date")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<RecognizedRecordForProcessing.createdDateTime> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Replace)]
  [Owner]
  protected virtual void _(
    PX.Data.Events.CacheAttached<RecognizedRecordForProcessing.owner> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Document Link", Visible = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<RecognizedRecordForProcessing.documentLink> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<RecognizedRecordForProcessing> e)
  {
    if (e.Row == null)
      return;
    this.ViewErrorHistory.SetEnabled(e.Row.Status == "E");
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<RecognizedRecordForProcessing.documentLink> e)
  {
    if (!(e.Row is RecognizedRecordForProcessing row) || !row.DocumentLink.HasValue)
      return;
    APRegister apRegister = (APRegister) PXSelectBase<APRegister, PXViewOf<APRegister>.BasedOn<SelectFromBase<APRegister, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<APRegister.noteID, IBqlGuid>.IsEqual<P.AsGuid>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) row.DocumentLink);
    e.ReturnValue = apRegister != null ? (object) $"{apRegister.DocType} {apRegister.RefNbr}" : (object) string.Empty;
  }

  public static async Task SearchForVendorAsync(
    List<RecognizedRecordForProcessing> records,
    CancellationToken cancellationToken)
  {
    Guid?[] array = records.Select<RecognizedRecordForProcessing, Guid?>((Func<RecognizedRecordForProcessing, Guid?>) (r => r.RefNbr)).ToArray<Guid?>();
    APInvoiceRecognitionEntry graph = PXGraph.CreateInstance<APInvoiceRecognitionEntry>();
    RecognizedRecordForProcessing[] orderedRecordsWithJsonFields = new PXViewOf<RecognizedRecordForProcessing>.BasedOn<SelectFromBase<RecognizedRecordForProcessing, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<RecognizedRecord.refNbr, IBqlGuid>.IsIn<P.AsGuid>>>.ReadOnly((PXGraph) graph).Select((object) array).FirstTableItems.OrderBy<RecognizedRecordForProcessing, Guid?>((Func<RecognizedRecordForProcessing, Guid?>) (r => r.RefNbr)).ToArray<RecognizedRecordForProcessing>();
    RecognizedRecordForProcessing[] orderedRecords = records.OrderBy<RecognizedRecordForProcessing, Guid?>((Func<RecognizedRecordForProcessing, Guid?>) (r => r.RefNbr)).ToArray<RecognizedRecordForProcessing>();
    for (int i = 0; i < orderedRecords.Length; ++i)
    {
      PXProcessing.SetCurrentItem((object) orderedRecords[i]);
      if (orderedRecords[i].Status != "R")
      {
        PXProcessing.SetProcessed();
      }
      else
      {
        RecognizedRecordDetail detail = (RecognizedRecordDetail) PXSelectBase<RecognizedRecordDetail, PXViewOf<RecognizedRecordDetail>.BasedOn<SelectFromBase<RecognizedRecordDetail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<RecognizedRecordDetail.entityType, Equal<P.AsString>>>>>.And<BqlOperand<RecognizedRecordDetail.refNbr, IBqlGuid>.IsEqual<P.AsGuid>>>>.ReadOnly.Config>.Select((PXGraph) graph, (object) orderedRecords[i].EntityType, (object) orderedRecords[i].RefNbr);
        if (detail != null && detail.VendorID.HasValue)
        {
          PXProcessing.SetProcessed();
        }
        else
        {
          await graph.PopulateVendorId((RecognizedRecord) orderedRecordsWithJsonFields[i], detail);
          PXProcessing.SetProcessed();
        }
      }
    }
    graph = (APInvoiceRecognitionEntry) null;
    orderedRecordsWithJsonFields = (RecognizedRecordForProcessing[]) null;
    orderedRecords = (RecognizedRecordForProcessing[]) null;
  }

  public static async Task RecognizeAsync(
    List<RecognizedRecordForProcessing> records,
    CancellationToken cancellationToken)
  {
    PXCache cach = PXGraph.CreateInstance<PXGraph>().Caches[typeof (RecognizedRecordForProcessing)];
    List<RecognizedRecordFileInfo> batch = new List<RecognizedRecordFileInfo>();
    foreach (RecognizedRecordForProcessing record in records)
    {
      if (!APInvoiceRecognitionEntry.StatusValidForRecognitionSet.Contains(record.Status))
      {
        PXProcessing.SetCurrentItem((object) record);
        PXProcessing.SetProcessed();
      }
      else
      {
        UploadFile[] filesToRecognize = APInvoiceRecognitionEntry.GetFilesToRecognize(cach, (object) record);
        if (filesToRecognize == null || filesToRecognize.Length == 0)
        {
          PXProcessing.SetCurrentItem((object) record);
          PXProcessing.SetProcessed();
        }
        else
        {
          UploadFile uploadFile = filesToRecognize[0];
          RecognizedRecordFileInfo recognizedRecordFileInfo = new RecognizedRecordFileInfo(uploadFile.Name, uploadFile.Data, uploadFile.FileID.Value, (RecognizedRecord) record);
          batch.Add(recognizedRecordFileInfo);
        }
      }
    }
    if (batch.Count == 0)
      return;
    await APInvoiceRecognitionEntry.RecognizeRecordsBatch((IEnumerable<RecognizedRecordFileInfo>) batch, cancellationToken);
  }
}
