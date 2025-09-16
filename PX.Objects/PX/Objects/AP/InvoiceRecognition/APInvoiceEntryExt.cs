// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.APInvoiceEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Microsoft.Extensions.Configuration;
using PX.CloudServices.DAC;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.Search;
using PX.Data.WorkflowAPI;
using PX.Objects.AP.InvoiceRecognition.DAC;
using PX.Objects.AP.InvoiceRecognition.Feedback;
using PX.SM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace PX.Objects.AP.InvoiceRecognition;

[PXInternalUseOnly]
public class APInvoiceEntryExt : PXGraphExtension<
#nullable disable
APInvoiceEntry>
{
  internal const string FEEDBACK_FIELD_BOUND_KEY = "feedback:field-bound";
  internal const string FEEDBACK_RECORD_SAVED_KEY = "feedback:record-saved";
  public PXFilter<PX.Objects.AP.InvoiceRecognition.DAC.FeedbackParameters> FeedbackParameters;
  public PXViewOf<APRecognizedInvoice>.BasedOn<SelectFromBase<APRecognizedInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  APRecognizedInvoice.documentLink, IBqlGuid>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  APInvoice.noteID, IBqlGuid>.AsOptional>>>.ReadOnly SourceDocument;
  public 
  #nullable disable
  PXAction<APInvoice> viewSourceDocument;

  [InjectDependency]
  internal IEntitySearchService EntitySearchService { get; set; }

  [InjectDependency]
  public IInvoiceRecognitionService InvoiceRecognitionClient { get; set; }

  [InjectDependency]
  public IConfiguration Configuration { get; set; }

  [PXUIField(DisplayName = "View Source Document")]
  [PXLookupButton]
  public virtual void ViewSourceDocument()
  {
    APRecognizedInvoice recognizedInvoice = this.SourceDocument.SelectSingle();
    if (recognizedInvoice != null)
    {
      APInvoiceRecognitionEntry instance = PXGraph.CreateInstance<APInvoiceRecognitionEntry>();
      instance.Document.Current = recognizedInvoice;
      throw new PXRedirectRequiredException((PXGraph) instance, (string) null);
    }
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.apDocumentRecognition>();

  public override void Initialize()
  {
    if (this.InvoiceRecognitionClient.IsConfigured())
      this.Base.OnAfterPersist += new System.Action<PXGraph>(this.SendFeedback);
    this.Base.Views.Caches.Add(this.Base.Caches[typeof (RecognizedRecord)].GetItemType());
    this.Base.RowPersisted.AddHandler<APInvoice>(new PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<APInvoice>>.EventDelegate(this.ReplacePrimaryScreenId));
    this.Base.OnAfterPersist += new System.Action<PXGraph>(this.CorrectRecognizedVendor);
  }

  private void ReplacePrimaryScreenId(PX.Data.Events.RowPersisted<APInvoice> e)
  {
    if (e.Operation != PXDBOperation.Insert || e.TranStatus != PXTranStatus.Open)
      return;
    APInvoiceExt extension = e.Cache.GetExtension<APInvoiceExt>((object) e.Row);
    if ((extension != null ? (!extension.RenameFileScreenId.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      return;
    e.Cache.SetValue<APInvoiceExt.renameFileScreenId>((object) e.Row, (object) false);
    string screenId = PXContext.GetScreenID().Replace(".", "") ?? PXSiteMap.Provider.FindSiteMapNodeByGraphType(typeof (APInvoiceEntry).FullName)?.ScreenID;
    if (string.IsNullOrEmpty(screenId))
      return;
    Guid[] fileNotes = PXNoteAttribute.GetFileNotes(e.Cache, (object) e.Row);
    if (fileNotes == null)
      return;
    foreach (Guid guid in fileNotes)
    {
      Guid fileId = guid;
      UploadFile uploadFile = this.Base.Select<UploadFile>().Where<UploadFile>((Expression<Func<UploadFile, bool>>) (f => f.FileID == (Guid?) fileId && f.PrimaryScreenID != screenId)).FirstOrDefault<UploadFile>();
      if (uploadFile != null)
      {
        PXCache cach = this.Base.Caches[typeof (UploadFile)];
        uploadFile.PrimaryScreenID = screenId;
        UploadFile row = cach.Update((object) uploadFile) as UploadFile;
        using (new PXTimeStampScope((byte[]) null))
        {
          PXTimeStampScope.SetRecordComesFirst(typeof (UploadFile), true);
          cach.PersistUpdated((object) row);
        }
      }
    }
  }

  public sealed override void Configure(PXScreenConfiguration configuration)
  {
    APInvoiceEntryExt.Configure(configuration.GetScreenConfigurationContext<APInvoiceEntry, APInvoice>());
  }

  protected static void Configure(WorkflowContext<APInvoiceEntry, APInvoice> context)
  {
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.apDocumentRecognition>())
      return;
    context.UpdateScreenConfigurationFor((Func<BoundedTo<APInvoiceEntry, APInvoice>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<APInvoiceEntry, APInvoice>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.ContainerAdjusterActions>) (actions => actions.Add<APInvoiceEntryExt>((Expression<Func<APInvoiceEntryExt, PXAction<APInvoice>>>) (g => g.viewSourceDocument), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.InFolder(FolderType.InquiriesFolder)))))));
  }

  protected void _(PX.Data.Events.RowSelected<APInvoice> e, PXRowSelected baseEvent)
  {
    baseEvent(e.Cache, e.Args);
    if (e.Row == null)
      return;
    this.viewSourceDocument.SetEnabled((RecognizedRecord) PXSelectBase<RecognizedRecord, PXSelect<RecognizedRecord, Where<RecognizedRecord.documentLink, Equal<Required<RecognizedRecord.documentLink>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, (object) e.Row.NoteID) != null);
  }

  protected void _(PX.Data.Events.RowDeleted<APInvoice> e, PXRowDeleted baseEvent)
  {
    if (baseEvent != null)
      baseEvent(e.Cache, e.Args);
    APInvoice row = e.Row;
    if ((row != null ? (!row.NoteID.HasValue ? 1 : 0) : 1) != 0)
      return;
    PXResultset<APRecognizedInvoice> pxResultset = this.SourceDocument.Select((object) e.Row.NoteID);
    if (pxResultset == null)
      return;
    APInvoiceRecognitionEntry instance = PXGraph.CreateInstance<APInvoiceRecognitionEntry>();
    instance.Document.Current = (APRecognizedInvoice) pxResultset;
    instance.Delete.Press();
  }

  private void SendFeedback(PXGraph graph)
  {
    if (!(graph is APInvoiceEntry apInvoiceEntry))
      return;
    APInvoice current = apInvoiceEntry.Document.Current;
    if (current == null || !"INV".Equals(current.DocType, StringComparison.Ordinal))
      return;
    DocumentFeedbackBuilder feedbackBuilder = this.FeedbackParameters.Current.FeedbackBuilder;
    if (feedbackBuilder == null)
      return;
    PXView view1 = apInvoiceEntry.Document.View;
    PXView view2 = apInvoiceEntry.Transactions.View;
    IQueryable<APTran> detailRows = apInvoiceEntry.Transactions.Select().Select<PXResult<APTran>, APTran>((Expression<Func<PXResult<APTran>, APTran>>) (t => (APTran) t));
    DocumentFeedback feedback = feedbackBuilder.ToRecordSavedFeedback(view1, current, view2, (IEnumerable<APTran>) detailRows, this.EntitySearchService);
    Guid? docNoteId = current.NoteID;
    Dictionary<string, Uri> links = this.FeedbackParameters.Current.Links;
    if (links == null)
      return;
    this.FeedbackParameters.Reset();
    IInvoiceRecognitionFeedback feedbackService = this.InvoiceRecognitionClient as IInvoiceRecognitionFeedback;
    if (feedbackService == null)
      return;
    bool sendBoundFeedback = ConfigurationBinder.GetValue<bool>(this.Configuration, "SendDocumentInboxFeedback", false);
    graph.LongOperationManager.StartAsyncOperation((object) Guid.NewGuid(), (Func<CancellationToken, Task>) (_ => APInvoiceEntryExt.SendFeedbackAsync(links, feedbackService, docNoteId, feedback, sendBoundFeedback)));
  }

  private static async Task SendFeedbackAsync(
    Dictionary<string, Uri> links,
    IInvoiceRecognitionFeedback feedbackService,
    Guid? documentLink,
    DocumentFeedback recordSavedFeedback,
    bool sendBoundFeedback)
  {
    if (links == null)
    {
      PXTrace.WriteError("IDocumentRecognitionClient: Unable to send feedback - links are not found");
    }
    else
    {
      if (sendBoundFeedback)
        await APInvoiceEntryExt.SendBoundFeedbackAsync(links, feedbackService, documentLink);
      await APInvoiceEntryExt.SendRecordSavedFeedbackAsync(links, feedbackService, recordSavedFeedback);
    }
  }

  private static async Task SendRecordSavedFeedbackAsync(
    Dictionary<string, Uri> links,
    IInvoiceRecognitionFeedback feedbackService,
    DocumentFeedback recordSavedFeedback)
  {
    if (recordSavedFeedback == null)
      return;
    Uri address;
    if (!links.TryGetValue("feedback:record-saved", out address))
    {
      PXTrace.WriteError("IDocumentRecognitionClient: Unable to send feedback - link is not found:{LinkKey}", (object) "feedback:record-saved");
    }
    else
    {
      JsonMediaTypeFormatter mediaTypeFormatter1 = new JsonMediaTypeFormatter();
      ((BaseJsonMediaTypeFormatter) mediaTypeFormatter1).SerializerSettings = DocumentFeedback._settings;
      JsonMediaTypeFormatter mediaTypeFormatter2 = mediaTypeFormatter1;
      await feedbackService.Send(address, (HttpContent) new ObjectContent(recordSavedFeedback.GetType(), (object) recordSavedFeedback, (MediaTypeFormatter) mediaTypeFormatter2));
    }
  }

  private static async Task SendBoundFeedbackAsync(
    Dictionary<string, Uri> links,
    IInvoiceRecognitionFeedback feedbackService,
    Guid? documentLink)
  {
    Uri link;
    StringReader reader;
    if (!links.TryGetValue("feedback:field-bound", out link))
    {
      PXTrace.WriteError("IDocumentRecognitionClient: Unable to send feedback - link is not found:{LinkKey}", (object) "feedback:field-bound");
      link = (Uri) null;
      reader = (StringReader) null;
    }
    else
    {
      RecognizedRecord recognizedRecord = PXSelectBase<RecognizedRecord, PXSelect<RecognizedRecord, Where<RecognizedRecord.documentLink, Equal<Required<RecognizedRecord.documentLink>>>>.Config>.Select(PXGraph.CreateInstance<PXGraph>(), (object) documentLink).FirstOrDefault<PXResult<RecognizedRecord>>()?.GetItem<RecognizedRecord>();
      if (recognizedRecord == null)
      {
        link = (Uri) null;
        reader = (StringReader) null;
      }
      else if (recognizedRecord.RecognitionFeedback == null)
      {
        link = (Uri) null;
        reader = (StringReader) null;
      }
      else
      {
        reader = new StringReader(recognizedRecord.RecognitionFeedback);
        while (true)
        {
          string content;
          do
          {
            content = await reader.ReadLineAsync();
            if (content == null)
              goto label_9;
          }
          while (string.IsNullOrWhiteSpace(content));
          await feedbackService.Send(link, (HttpContent) new StringContent(content, Encoding.UTF8, "application/json"));
        }
label_9:
        link = (Uri) null;
        reader = (StringReader) null;
      }
    }
  }

  private void CorrectRecognizedVendor(PXGraph graph)
  {
    if (!(graph is APInvoiceEntry apInvoiceEntry))
      return;
    int? vendorId1 = (int?) apInvoiceEntry.Document.Current?.VendorID;
    PXCache pxCache;
    if (!vendorId1.HasValue || !apInvoiceEntry.Caches.TryGetValue(typeof (RecognizedVendorMapping), out pxCache) || !(pxCache.Current is RecognizedVendorMapping current) || current.VendorNamePrefix == null || current.VendorName == null)
      return;
    RecognizedVendorMapping topFirst = PXSelectBase<RecognizedVendorMapping, PXViewOf<RecognizedVendorMapping>.BasedOn<SelectFromBase<RecognizedVendorMapping, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<RecognizedVendorMapping.vendorNamePrefix, Equal<P.AsString>>>>>.And<BqlOperand<RecognizedVendorMapping.vendorName, IBqlString>.IsEqual<P.AsString>>>>.ReadOnly.Config>.Select(graph, (object) current.VendorNamePrefix, (object) current.VendorName)?.TopFirst;
    int num;
    if (topFirst == null)
    {
      num = !vendorId1.HasValue ? 1 : 0;
    }
    else
    {
      int? vendorId2 = topFirst.VendorID;
      int? nullable = vendorId1;
      num = vendorId2.GetValueOrDefault() == nullable.GetValueOrDefault() & vendorId2.HasValue == nullable.HasValue ? 1 : 0;
    }
    if (num != 0)
      return;
    if (topFirst != null)
    {
      topFirst.VendorID = vendorId1;
      pxCache.PersistUpdated((object) topFirst);
    }
    else
    {
      current.VendorID = vendorId1;
      pxCache.PersistInserted((object) current);
    }
    pxCache.Clear();
  }
}
