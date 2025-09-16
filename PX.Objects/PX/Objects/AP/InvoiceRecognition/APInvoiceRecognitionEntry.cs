// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.APInvoiceRecognitionEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Newtonsoft.Json;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PX.CloudServices.DAC;
using PX.CloudServices.DocumentRecognition;
using PX.CloudServices.Tenants;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.DependencyInjection;
using PX.Data.Description;
using PX.Data.Wiki.Parser;
using PX.Metadata;
using PX.Objects.AP.InvoiceRecognition.DAC;
using PX.Objects.AP.InvoiceRecognition.Feedback;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.Extensions.MultiCurrency.AP;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.Objects.SO;
using PX.SM;
using Serilog;
using Serilog.Events;
using SerilogTimings.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

#nullable enable
namespace PX.Objects.AP.InvoiceRecognition;

[PXInternalUseOnly]
public class APInvoiceRecognitionEntry : 
  PXGraph<
  #nullable disable
  APInvoiceRecognitionEntry, APRecognizedInvoice>,
  IGraphWithInitialization,
  ICaptionable
{
  internal const string FEEDBACK_VENDOR_SEARCH = "feedback:entity-resolution";
  private const int _recognitionTimeoutMinutes = 20;
  private const int MaxFilePageCount = 50;
  private static readonly TimeSpan RecognitionPollingInterval = TimeSpan.FromSeconds(1.0);
  private static readonly string _screenInfoLoad = typeof (APInvoiceRecognitionEntry).FullName + nameof (_screenInfoLoad);
  internal const string PdfExtension = ".pdf";
  public const string RefNbrNavigationParam = "RecognizedRecordRefNbr";
  public const string StatusNavigationParam = "RecognitionStatus";
  public const string NoteIdNavigationParam = "NoteID";
  private JsonSerializer _jsonSerializer = JsonSerializer.CreateDefault(DocumentFeedback._settings);
  private readonly HashSet<string> _alwaysDefaultPrimaryFields = new HashSet<string>()
  {
    "VendorLocationID",
    "RecognizedRecordRefNbr",
    "RecognizedRecordStatus",
    "RecognitionStatus",
    "AllowFiles",
    "AllowFilesMsg",
    "AllowUploadFile"
  };
  public PXSetup<PX.Objects.AP.APSetup> APSetup;
  public SelectFromBase<PX.Objects.AP.APInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  PX.Objects.AP.APInvoice.docType, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  APRecognizedInvoice.docType, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PX.Objects.AP.APInvoice.refNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  APRecognizedInvoice.refNbr, IBqlString>.FromCurrent>>> Invoices;
  public 
  #nullable disable
  PXSelect<APRecognizedInvoice> Document;
  public PXSelect<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APInvoice.docType, Equal<Current<APRecognizedInvoice.docType>>, PX.Data.And<Where<PX.Objects.AP.APInvoice.refNbr, Equal<Current<APRecognizedInvoice.refNbr>>>>>> DocumentBase;
  public PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APRecognizedInvoice.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Optional<APRecognizedInvoice.vendorLocationID>>>>> VendorLocation;
  public PXSelect<APRecognizedTran, Where<APRecognizedTran.tranType, Equal<Current<APRecognizedInvoice.docType>>, PX.Data.And<Where<APRecognizedTran.refNbr, Equal<Current<APRecognizedInvoice.refNbr>>>>>> Transactions;
  public PXSelect<VendorR> Vendors;
  public FbqlSelect<SelectFromBase<RecognizedRecord, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  RecognizedRecord.refNbr, IBqlGuid>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  APRecognizedInvoice.recognizedRecordRefNbr, IBqlGuid>.FromCurrent>>, 
  #nullable disable
  RecognizedRecord>.View RecognizedRecords;
  public PXViewOf<RecognizedRecordErrorHistory>.BasedOn<SelectFromBase<RecognizedRecordErrorHistory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  RecognizedRecordErrorHistory.refNbr, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  APRecognizedInvoice.recognizedRecordRefNbr, IBqlGuid>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  RecognizedRecordErrorHistory.entityType, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  APRecognizedInvoice.entityType, IBqlString>.FromCurrent>>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  RecognizedRecordErrorHistory.createdDateTime, IBqlDateTime>.Desc>>>.ReadOnly ErrorHistory;
  public 
  #nullable disable
  FbqlSelect<SelectFromBase<RecognizedRecordDetail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  RecognizedRecordDetail.refNbr, 
  #nullable disable
  Equal<P.AsGuid>>>>>.And<BqlOperand<
  #nullable enable
  RecognizedRecordDetail.entityType, IBqlString>.IsEqual<
  #nullable disable
  P.AsString>>>, RecognizedRecordDetail>.View RecognizedRecordDetails;
  public PXViewOf<APRecognizedInvoice>.BasedOn<SelectFromBase<APRecognizedInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<BqlField<
  #nullable enable
  APRecognizedInvoice.recognizedRecordCreatedDateTime, IBqlDateTime>.Desc>>>.ReadOnly NavigationSelect;
  public 
  #nullable disable
  PXFilter<PX.Objects.AP.InvoiceRecognition.DAC.BoundFeedback> BoundFeedback;
  public PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<APRecognizedInvoice.vendorID>>>> CurrentVendor;
  public PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.branchID, Equal<Current<PX.Objects.AP.APInvoice.branchID>>>> CurrentBranch;
  public APInvoiceRecognitionEntry.PXDeleteWithRecognizedRecord<APRecognizedInvoice> Delete;
  public PXAction<APRecognizedInvoice> First;
  public PXAction<APRecognizedInvoice> Previous;
  public PXAction<APRecognizedInvoice> Next;
  public PXAction<APRecognizedInvoice> Last;
  public PXAction<APRecognizedInvoice> ContinueSave;
  public PXAction<APRecognizedInvoice> ProcessRecognition;
  public PXAction<APRecognizedInvoice> RefreshStatus;
  public PXAction<APRecognizedInvoice> OpenDocument;
  public PXAction<APRecognizedInvoice> OpenDuplicate;
  public PXAction<APRecognizedInvoice> DeleteAllTransactions;
  public PXAction<APRecognizedInvoice> ViewErrorHistory;
  public PXAction<APRecognizedInvoice> SearchVendor;
  public PXAction<APRecognizedInvoice> DumpTableFeedback;
  public PXAction<APRecognizedInvoice> AttachFromMobile;

  internal static HashSet<string> StatusValidForRecognitionSet { get; } = new HashSet<string>()
  {
    "N",
    "E"
  };

  [InjectDependency]
  internal IScreenInfoProvider ScreenInfoProvider { get; set; }

  [InjectDependency]
  internal ILogger _logger { get; set; }

  [InjectDependency]
  public IInvoiceRecognitionService InvoiceRecognitionClient { get; set; }

  [InjectDependency]
  internal ICloudTenantService _cloudTenantService { get; set; }

  [InjectDependency]
  internal RecognizedRecordDetailsManager DetailsPopulator { get; set; }

  public void Initialize()
  {
    this.SetupDefaultActionStates();
    this.SwitchDefaultsOffForUIFields();
  }

  private void SetupDefaultActionStates()
  {
    this.Save.SetVisible(false);
    this.Cancel.SetVisible(false);
    this.DumpTableFeedback.SetVisible(false);
  }

  private void SwitchDefaultsOffForUIFields()
  {
    (IEnumerable<string> strings1, IEnumerable<string> strings2) = this.GetUIFields();
    if (strings1 == null || strings2 == null)
      return;
    PXFieldDefaulting handler = (PXFieldDefaulting) ((sender, args) => args.Cancel = true);
    foreach (string field in strings1.Where<string>((Func<string, bool>) (fieldName => !this._alwaysDefaultPrimaryFields.Contains(fieldName))))
      this.FieldDefaulting.AddHandler(this.Document.View.Name, field, handler);
    foreach (string field in strings2.Where<string>((Func<string, bool>) (fieldName => !this.GetAlwaysDefaultDetailFields().Contains(fieldName))))
      this.FieldDefaulting.AddHandler(this.Transactions.View.Name, field, handler);
  }

  public virtual HashSet<string> GetAlwaysDefaultDetailFields() => new HashSet<string>();

  protected virtual IEnumerable document()
  {
    APInvoiceRecognitionEntry graph = this;
    if (graph.Document.Current != null && graph.Caches[typeof (PX.Objects.CM.Extensions.CurrencyInfo)].Current != null)
    {
      if (graph.Caches[typeof (PX.Objects.AP.APRegister)].Current != graph.Document.Current)
        graph.Caches[typeof (PX.Objects.AP.APRegister)].Current = (object) graph.Document.Current;
      yield return (object) graph.Document.Current;
    }
    else
    {
      foreach (APRecognizedInvoice recognizedInvoice in graph.QuickSelect(graph.Document.View.BqlSelect))
      {
        if (recognizedInvoice.RefNbr == null && recognizedInvoice.DocType == null)
        {
          graph.DefaultInvoiceValues(recognizedInvoice);
          graph.Document.Cache.SetStatus((object) recognizedInvoice, PXEntryStatus.Held);
          graph.Caches[typeof (PX.Objects.AP.APRegister)].Current = (object) recognizedInvoice;
        }
        if (graph.Document.Current == null)
        {
          recognizedInvoice.IsRedirect = new bool?(true);
        }
        else
        {
          IEnumerable<string> primaryFields = graph.GetUIFields().PrimaryFields;
          if (primaryFields != null)
          {
            foreach (string fieldName in primaryFields.Union<string>((IEnumerable<string>) new string[1]
            {
              "IsDataLoaded"
            }))
            {
              object obj = graph.Document.Cache.GetValue((object) graph.Document.Current, fieldName);
              graph.Document.Cache.SetValue((object) recognizedInvoice, fieldName, obj);
            }
          }
        }
        yield return (object) recognizedInvoice;
      }
    }
  }

  private void DefaultInvoiceValues(APRecognizedInvoice record)
  {
    record.DocType = record.EntityType;
    PX.Objects.AP.APInvoice copy = this.DocumentBase.Insert((PX.Objects.AP.APInvoice) record);
    this.DocumentBase.Cache.Remove((object) copy);
    this.DocumentBase.Cache.RestoreCopy((object) record, (object) copy);
    this.DocumentBase.Cache.IsDirty = false;
  }

  public override bool CanClipboardCopyPaste() => false;

  [PXButton]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual void attachFromMobile()
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXDBString(IsKey = false)]
  protected virtual void APRecognizedInvoice_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXDBString(IsKey = false)]
  protected virtual void APRecognizedInvoice_DocType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Replace)]
  [PopupMessage]
  [VendorActiveOrHoldPayments(Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof (PX.Objects.AP.Vendor.acctName), CacheGlobal = true, Filterable = true)]
  [PXDefault]
  protected virtual void APRecognizedInvoice_VendorID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXDBDefault(typeof (APRecognizedInvoice.refNbr))]
  [PXParent(typeof (PX.Data.Select<APRecognizedInvoice, Where<APRecognizedInvoice.docType, Equal<Current<APRecognizedTran.tranType>>, And<APRecognizedInvoice.refNbr, Equal<Current<APRecognizedTran.refNbr>>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<APRecognizedTran.refNbr> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXFormula(null, typeof (SumCalc<APRecognizedInvoice.curyLineTotal>))]
  [PXUIField(Visible = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<APRecognizedTran.curyTranAmt> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXDBDefault(typeof (APRecognizedInvoice.docType))]
  protected virtual void _(PX.Data.Events.CacheAttached<APRecognizedTran.tranType> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXDefault("INV")]
  protected virtual void RecognizedRecord_EntityType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXDefault("INV")]
  protected virtual void APRecognizedInvoice_EntityType_CacheAttached(PXCache sender)
  {
  }

  [PXUIField]
  [PXFirstButton]
  public IEnumerable first(PXAdapter adapter)
  {
    APInvoiceRecognitionEntry recognitionEntry = this;
    APRecognizedInvoice current = recognitionEntry.Document.Current;
    if ((current != null ? (!current.CreatedDateTime.HasValue ? 1 : 0) : 1) == 0)
    {
      APRecognizedInvoice recognizedInvoice = recognitionEntry.NavigationSelect.SelectSingle();
      if (recognizedInvoice == null)
      {
        yield return (object) recognitionEntry.Document.Current;
      }
      else
      {
        recognitionEntry.Clear();
        recognitionEntry.SelectTimeStamp();
        yield return (object) recognizedInvoice;
      }
    }
  }

  [PXUIField]
  [PXPreviousButton]
  public IEnumerable previous(PXAdapter adapter)
  {
    APInvoiceRecognitionEntry graph = this;
    APRecognizedInvoice current = graph.Document.Current;
    if ((current != null ? (!current.CreatedDateTime.HasValue ? 1 : 0) : 1) == 0)
    {
      BqlCommand select = graph.NavigationSelect.View.BqlSelect.WhereNew<PX.Data.Where<BqlOperand<APRecognizedInvoice.recognizedRecordCreatedDateTime, IBqlDateTime>.IsGreater<BqlField<APRecognizedInvoice.recognizedRecordCreatedDateTime, IBqlDateTime>.FromCurrent>>>().OrderByNew<PX.Data.OrderBy<Asc<APRecognizedInvoice.recognizedRecordCreatedDateTime>>>();
      object obj = new PXView((PXGraph) graph, true, select).SelectSingle();
      if (obj == null)
      {
        yield return (object) graph.Document.Current;
      }
      else
      {
        graph.Clear();
        graph.SelectTimeStamp();
        yield return obj;
      }
    }
  }

  [PXUIField]
  [PXNextButton]
  public IEnumerable next(PXAdapter adapter)
  {
    APInvoiceRecognitionEntry graph = this;
    APRecognizedInvoice current = graph.Document.Current;
    if ((current != null ? (!current.CreatedDateTime.HasValue ? 1 : 0) : 1) == 0)
    {
      BqlCommand select = graph.NavigationSelect.View.BqlSelect.WhereNew<PX.Data.Where<BqlOperand<APRecognizedInvoice.recognizedRecordCreatedDateTime, IBqlDateTime>.IsLess<BqlField<APRecognizedInvoice.recognizedRecordCreatedDateTime, IBqlDateTime>.FromCurrent>>>();
      object obj = new PXView((PXGraph) graph, true, select).SelectSingle();
      if (obj == null)
      {
        yield return (object) graph.Document.Current;
      }
      else
      {
        graph.Clear();
        graph.SelectTimeStamp();
        yield return obj;
      }
    }
  }

  [PXUIField]
  [PXLastButton]
  public IEnumerable last(PXAdapter adapter)
  {
    APInvoiceRecognitionEntry recognitionEntry = this;
    APRecognizedInvoice current = recognitionEntry.Document.Current;
    if ((current != null ? (!current.CreatedDateTime.HasValue ? 1 : 0) : 1) == 0)
    {
      APRecognizedInvoice topFirst = recognitionEntry.NavigationSelect.SelectWindowed(-1, 1).TopFirst;
      if (topFirst == null)
      {
        yield return (object) recognitionEntry.Document.Current;
      }
      else
      {
        recognitionEntry.Clear();
        recognitionEntry.SelectTimeStamp();
        yield return (object) topFirst;
      }
    }
  }

  public IEnumerable transactions()
  {
    if (this.Document.Current?.RecognitionStatus == "P")
      return (IEnumerable) PXSelectBase<APRecognizedTran, PXViewOf<APRecognizedTran>.BasedOn<SelectFromBase<APRecognizedTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APRecognizedTran.tranType, Equal<BqlField<APRecognizedInvoice.docType, IBqlString>.FromCurrent>>>>, PX.Data.And<BqlOperand<APRecognizedTran.refNbr, IBqlString>.IsEqual<BqlField<APRecognizedInvoice.refNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.AP.APTran.lineType, IBqlString>.IsNotEqual<SOLineType.discount>>>.Order<By<BqlField<APRecognizedTran.tranType, IBqlString>.Asc, BqlField<APRecognizedTran.refNbr, IBqlString>.Asc, BqlField<PX.Objects.AP.APTran.lineNbr, IBqlInt>.Asc>>>.ReadOnly.Config>.Select((PXGraph) this);
    List<APRecognizedTran> apRecognizedTranList = new List<APRecognizedTran>();
    foreach (APRecognizedTran apRecognizedTran in this.Transactions.Cache.Cached)
    {
      switch (this.Transactions.Cache.GetStatus((object) apRecognizedTran))
      {
        case PXEntryStatus.Deleted:
        case PXEntryStatus.InsertedDeleted:
          continue;
        default:
          apRecognizedTranList.Add(apRecognizedTran);
          continue;
      }
    }
    return (IEnumerable) apRecognizedTranList;
  }

  private void RemoveAttachedFile()
  {
    APRecognizedInvoice current = this.Document.Current;
    if ((current != null ? (!current.FileID.HasValue ? 1 : 0) : 1) != 0)
      return;
    UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
    NoteDoc noteDoc = (NoteDoc) instance.FileNoteDoc.Select((object) this.Document.Current.FileID, (object) this.Document.Current.NoteID);
    if (noteDoc == null)
      return;
    instance.FileNoteDoc.Delete(noteDoc);
    instance.Persist();
    PXNoteAttribute.ResetFileListCache(this.Document.Cache);
    this.Document.Current.FileID = new Guid?();
  }

  [PXUIField(DisplayName = "Save and Continue")]
  [PXButton]
  public void continueSave()
  {
    APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this.SaveFeedback();
      this.EnsureTransactions();
      this.Document.Cache.IsDirty = false;
      this.Transactions.Cache.IsDirty = false;
      instance.SelectTimeStamp();
      instance.IsRecognitionProcess = true;
      using (new APInvoiceFillFromRecognizedScope())
        this.InsertInvoiceData(instance);
      this.InsertCrossReferences(instance);
      this.InsertRecognizedRecordVendor(instance);
      transactionScope.Complete();
    }
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, false, (string) null);
    requiredException.Mode = PXBaseRedirectException.WindowMode.InlineWindow;
    throw requiredException;
  }

  private void SaveFeedback()
  {
    RecognizedRecord recognizedRecord = this.RecognizedRecords.Current ?? this.RecognizedRecords.SelectSingle();
    StringBuilder sb = new StringBuilder(recognizedRecord.RecognitionFeedback);
    StringWriter stringWriter = new StringWriter(sb, (IFormatProvider) CultureInfo.InvariantCulture);
    if (this.Document.Current.FeedbackBuilder == null)
      return;
    List<DocumentFeedback> tableFeedbackList = this.Document.Current.FeedbackBuilder.ToTableFeedbackList(this.Transactions.View.Name);
    JsonTextWriter jsonTextWriter1 = new JsonTextWriter((TextWriter) stringWriter);
    ((JsonWriter) jsonTextWriter1).Formatting = (Newtonsoft.Json.Formatting) 0;
    using (JsonTextWriter jsonTextWriter2 = jsonTextWriter1)
    {
      foreach (DocumentFeedback documentFeedback in tableFeedbackList)
      {
        sb.AppendLine();
        this._jsonSerializer.Serialize((JsonWriter) jsonTextWriter2, (object) documentFeedback);
      }
    }
    this.RecognizedRecords.Cache.SetValue<RecognizedRecord.recognitionFeedback>((object) recognizedRecord, (object) stringWriter.ToString());
    this.RecognizedRecords.Cache.PersistUpdated((object) recognizedRecord);
  }

  [PXButton]
  [PXUIField(DisplayName = "Recognize")]
  public virtual IEnumerable processRecognition(PXAdapter adapter)
  {
    Guid refNbr = this.Document.Current.RecognizedRecordRefNbr.Value;
    Guid fileId = this.Document.Current.FileID.Value;
    ILogger logger = this._logger;
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() =>
    {
      APInvoiceRecognitionEntry.RecognizeInvoiceData(refNbr, fileId, logger);
      PXLongOperation.SetCustomInfoPersistent((object) APInvoiceRecognitionEntry.ReloadData.All);
    }));
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Refresh Status")]
  public virtual IEnumerable refreshStatus(PXAdapter adapter)
  {
    Guid refNbr = this.Document.Current.RecognizedRecordRefNbr.Value;
    Guid guid = this.Document.Current.FileID.Value;
    ILogger logger = this._logger;
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() =>
    {
      APInvoiceRecognitionEntry.RefreshInvoiceStatus(refNbr, logger);
      PXLongOperation.SetCustomInfoPersistent((object) APInvoiceRecognitionEntry.ReloadData.All);
    }));
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Open Document")]
  public virtual void openDocument()
  {
    this.Document.Cache.IsDirty = false;
    this.Transactions.Cache.IsDirty = false;
    PX.Objects.AP.APInvoice apInvoice = (PX.Objects.AP.APInvoice) PXSelectBase<PX.Objects.AP.APInvoice, PXViewOf<PX.Objects.AP.APInvoice>.BasedOn<SelectFromBase<PX.Objects.AP.APInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.AP.APInvoice.noteID, IBqlGuid>.IsEqual<P.AsGuid>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) this.Document.Current.DocumentLink);
    APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
    instance.Document.Current = apInvoice;
    throw new PXRedirectRequiredException((PXGraph) instance, (string) null);
  }

  [PXButton]
  [PXUIField(DisplayName = "Open Duplicate Document")]
  public virtual void openDuplicate()
  {
    APRecognizedInvoice recognizedInvoice = (APRecognizedInvoice) PXSelectBase<APRecognizedInvoice, PXViewOf<APRecognizedInvoice>.BasedOn<SelectFromBase<APRecognizedInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<APRecognizedInvoice.recognizedRecordRefNbr, IBqlGuid>.IsEqual<BqlField<APRecognizedInvoice.duplicateLink, IBqlGuid>.FromCurrent>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null);
    APInvoiceRecognitionEntry instance = PXGraph.CreateInstance<APInvoiceRecognitionEntry>();
    instance.Document.Current = recognizedInvoice;
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    requiredException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
    throw requiredException;
  }

  [PXButton(Tooltip = "Clear Table")]
  [PXUIField(DisplayName = "Clear Table")]
  public virtual void deleteAllTransactions()
  {
    foreach (PXResult<APRecognizedTran> pxResult in this.Transactions.Select())
      this.Transactions.Delete((APRecognizedTran) pxResult);
  }

  [PXButton]
  [PXUIField(DisplayName = "View History")]
  public virtual void viewErrorHistory()
  {
    int num = (int) this.ErrorHistory.AskExt();
  }

  [PXButton]
  [PXUIField(DisplayName = "Search for Vendor")]
  public virtual IEnumerable searchVendor(PXAdapter adapter)
  {
    Guid? refNbr = this.RecognizedRecords.SelectSingle().RefNbr;
    this.LongOperationManager.StartAsyncOperation((Func<CancellationToken, System.Threading.Tasks.Task>) (async _ =>
    {
      await APInvoiceRecognitionEntry.PopulateVendorId(refNbr);
      PXLongOperation.SetCustomInfoPersistent((object) APInvoiceRecognitionEntry.ReloadData.VendorId);
    }));
    return adapter.Get();
  }

  public static async System.Threading.Tasks.Task PopulateVendorId(Guid? refNbr)
  {
    APInvoiceRecognitionEntry instance = PXGraph.CreateInstance<APInvoiceRecognitionEntry>();
    PXResult<RecognizedRecord> pxResult = (PXResult<RecognizedRecord>) PXSelectBase<RecognizedRecord, PXViewOf<RecognizedRecord>.BasedOn<SelectFromBase<RecognizedRecord, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<RecognizedRecordDetail>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<RecognizedRecord.entityType, Equal<RecognizedRecordDetail.entityType>>>>>.And<BqlOperand<RecognizedRecord.refNbr, IBqlGuid>.IsEqual<RecognizedRecordDetail.refNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<RecognizedRecord.entityType, Equal<RecognizedRecordEntityTypeListAttribute.aPDocument>>>>>.And<BqlOperand<RecognizedRecord.refNbr, IBqlGuid>.IsEqual<P.AsGuid>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) instance, (object[]) null, (object) refNbr);
    RecognizedRecord record = pxResult[typeof (RecognizedRecord)] as RecognizedRecord;
    RecognizedRecordDetail detail = pxResult[typeof (RecognizedRecordDetail)] as RecognizedRecordDetail;
    await instance.PopulateVendorId(record, detail);
  }

  [PXButton]
  [PXUIField]
  public virtual void dumpTableFeedback()
  {
    this.Document.Current.FeedbackBuilder?.DumpTableFeedback();
  }

  protected virtual void _(PX.Data.Events.RowDeleting<APRecognizedInvoice> e)
  {
    if (e.Row == null)
      return;
    RecognizedRecord row = this.RecognizedRecords.SelectSingle();
    if (row == null)
      return;
    if (row.Status == "P")
    {
      Guid? documentLink = this.Document.Current.DocumentLink;
      Guid? noteId = this.Document.Current.NoteID;
      if ((documentLink.HasValue == noteId.HasValue ? (documentLink.HasValue ? (documentLink.GetValueOrDefault() == noteId.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      {
        PXNoteAttribute.ForceRetain<RecognizedRecord.noteID>(this.RecognizedRecords.Cache);
        PXNoteAttribute.ForceRetain<APRecognizedInvoice.noteID>(e.Cache);
      }
    }
    row.RecognitionResult = (string) null;
    PXCache<Note> pxCache = this.Caches<Note>();
    if (pxCache.IsInsertedUpdatedDeleted)
      pxCache.Clear();
    this.SelectTimeStamp();
    this.RecognizedRecords.Cache.PersistUpdated((object) row);
    this.RecognizedRecords.Cache.ResetPersisted((object) row);
    this.SelectTimeStamp();
    this.RecognizedRecords.Delete(row);
    this.UpdateDuplicates(row.RefNbr);
    this.Transactions.Cache.Clear();
  }

  protected virtual void _(PX.Data.Events.RowPersisting<APRecognizedInvoice> e) => e.Cancel = true;

  protected virtual void _(PX.Data.Events.FieldDefaulting<PX.Objects.AP.APInvoice.hold> e)
  {
    e.NewValue = (object) true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<APRecognizedInvoice.allowFilesMsg> e)
  {
    e.NewValue = (object) PXMessages.LocalizeFormatNoPrefixNLA("Only the following file types are allowed: {0}.", (object) ".pdf");
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<PX.Objects.AP.APInvoice.docDate> e)
  {
    e.NewValue = (object) null;
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<BAccountR.type> e)
  {
    e.NewValue = (object) "VE";
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<APRecognizedInvoice.vendorID> e)
  {
  }

  protected virtual void CurrencyInfo_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>())
      return;
    PX.Objects.AP.Vendor vendor = (PX.Objects.AP.Vendor) this.CurrentVendor.Select();
    PX.Objects.GL.Branch branch = (PX.Objects.GL.Branch) this.CurrentBranch.Select();
    if (vendor != null && vendor.CuryID != null)
    {
      e.NewValue = (object) vendor.CuryID;
      e.Cancel = true;
    }
    else
    {
      if (branch == null || string.IsNullOrEmpty(branch.BaseCuryID))
        return;
      e.NewValue = (object) branch.BaseCuryID;
      e.Cancel = true;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<APRecognizedInvoice.vendorLocationID> e)
  {
    List<APRecognizedTran> transactions = new List<APRecognizedTran>();
    foreach (PXResult<APRecognizedTran> pxResult in this.Transactions.Select())
    {
      APRecognizedTran tran = (APRecognizedTran) pxResult;
      if (!tran.InventoryIDManualInput.GetValueOrDefault())
        this.SetTranInventoryID(this.Transactions.Cache, tran);
      transactions.Add(tran);
    }
    if (transactions.Count <= 0)
      return;
    this.SetTranRecognizedPONumbers(this.Transactions.Cache, (IEnumerable<APRecognizedTran>) transactions, false);
  }

  private void SetTranRecognizedPONumbers(int? vendorId, HashSet<int?> inventoryIds)
  {
    this.SetTranRecognizedPONumbers(vendorId, inventoryIds, (APRecognizedTran) null);
  }

  private void SetTranRecognizedPONumbers(
    int? vendorId,
    HashSet<int?> inventoryIds,
    APRecognizedTran apTran)
  {
    IList<(int?, string, PageWord)> recognizedPoNumbers = this.GetRecognizedPONumbers(vendorId, inventoryIds);
    if (apTran == null)
    {
      foreach (PXResult<APRecognizedTran> pxResult in this.Transactions.Select())
      {
        APRecognizedTran apTran1 = (APRecognizedTran) pxResult;
        if (inventoryIds.Contains(apTran1.InventoryID))
          this.SetPOLink(apTran1, recognizedPoNumbers);
      }
    }
    else
    {
      if (!inventoryIds.Contains(apTran.InventoryID))
        return;
      this.SetPOLink(apTran, recognizedPoNumbers);
    }
  }

  public virtual void SetPOLink(
    APRecognizedTran apTran,
    IList<(int? InventoryId, string PONumber, PageWord PageWord)> recognizedPONumbers)
  {
    (int?, string, PageWord) tuple = recognizedPONumbers.FirstOrDefault<(int?, string, PageWord)>((Func<(int?, string, PageWord), bool>) (r =>
    {
      int? inventoryId1 = apTran.InventoryID;
      int? inventoryId2 = r.InventoryId;
      return inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue;
    }));
    string poNumber = tuple.Item2;
    string str = HttpUtility.UrlEncode(JsonConvert.SerializeObject((object) tuple.Item3));
    this.Transactions.Cache.SetValueExt<APRecognizedTran.pONumberJson>((object) apTran, (object) str);
    this.AutoLinkAPAndPO(apTran, poNumber);
  }

  public virtual void AutoLinkAPAndPO(APRecognizedTran tran, string poNumber)
  {
  }

  private IList<(int? InventoryId, string PONumber, PageWord PageWord)> GetRecognizedPONumbers(
    int? vendorId,
    HashSet<int?> inventoryIds)
  {
    List<(int?, string, PageWord)> recognizedPoNumbers = new List<(int?, string, PageWord)>();
    RecognizedRecord recognizedRecord = this.RecognizedRecords.SelectSingle();
    if (recognizedRecord == null || string.IsNullOrEmpty(recognizedRecord.RecognitionResult))
      return (IList<(int?, string, PageWord)>) recognizedPoNumbers;
    DocumentRecognitionResult recognitionResult = JsonConvert.DeserializeObject<DocumentRecognitionResult>(recognizedRecord.RecognitionResult);
    int? count = recognitionResult?.Pages?.Count;
    if (count.HasValue)
    {
      int? nullable = count;
      int num1 = 0;
      if (!(nullable.GetValueOrDefault() == num1 & nullable.HasValue))
      {
        IList<(int? InventoryId, string PONumber)> poNumbers = this.GetPONumbers(vendorId, inventoryIds);
        if (poNumbers.Count == 0)
          return (IList<(int?, string, PageWord)>) recognizedPoNumbers;
        foreach ((int? InventoryId, string str) in (IEnumerable<(int? InventoryId, string PONumber)>) poNumbers)
        {
          PageWord pageWord = (PageWord) null;
          int index1 = 0;
          while (true)
          {
            int num2 = index1;
            nullable = recognitionResult.Pages?.Count;
            int valueOrDefault1 = nullable.GetValueOrDefault();
            if (num2 < valueOrDefault1 & nullable.HasValue && pageWord == null)
            {
              Page page = recognitionResult.Pages[index1];
              int index2 = 0;
              while (true)
              {
                int num3 = index2;
                nullable = page?.Words?.Count;
                int valueOrDefault2 = nullable.GetValueOrDefault();
                if (num3 < valueOrDefault2 & nullable.HasValue && pageWord == null)
                {
                  if (string.Equals(page.Words[index2]?.Text, str, StringComparison.OrdinalIgnoreCase))
                    pageWord = new PageWord()
                    {
                      Page = new int?(index1),
                      Word = new int?(index2)
                    };
                  ++index2;
                }
                else
                  break;
              }
              ++index1;
            }
            else
              break;
          }
          if (pageWord != null)
            recognizedPoNumbers.Add((InventoryId, str, pageWord));
        }
        return (IList<(int?, string, PageWord)>) recognizedPoNumbers;
      }
    }
    return (IList<(int?, string, PageWord)>) recognizedPoNumbers;
  }

  protected virtual void _(PX.Data.Events.RowInserted<APRecognizedInvoice> e)
  {
    if (e.Row == null)
      return;
    this.Caches[typeof (PX.Objects.AP.APRegister)].Current = (object) e.Row;
  }

  protected virtual void _(PX.Data.Events.RowSelected<APRecognizedInvoice> e)
  {
    this.Document.View.SetAnswer((string) null, WebDialogResult.OK);
    APRecognizedInvoice row = e.Row;
    if (row == null)
      return;
    if (e.Row.DocType == null)
      this.DefaultInvoiceValues(row);
    RecognizedRecord recognizedRecord = this.RecognizedRecords.Current ?? this.RecognizedRecords.SelectSingle();
    if (recognizedRecord != null)
      e.Row.RecognitionStatus = recognizedRecord.Status;
    if (e.Row.IsRedirect.GetValueOrDefault())
    {
      e.Row.IsDataLoaded = new bool?(false);
      e.Row.IsRedirect = new bool?(false);
    }
    APInvoiceRecognitionEntry.ReloadData? customInfoPersistent = PXLongOperation.GetCustomInfoPersistent(this.UID) as APInvoiceRecognitionEntry.ReloadData?;
    Guid? nullable1 = e.Row.RecognizedRecordRefNbr;
    if (nullable1.HasValue && (!e.Row.IsDataLoaded.GetValueOrDefault() || customInfoPersistent.HasValue))
    {
      PXLongOperation.RemoveCustomInfoPersistent(this.UID);
      this.RecognizedRecords.Cache.SetValue<RecognizedRecord.recognitionFeedback>((object) recognizedRecord, (object) null);
      this.LoadRecognizedData(customInfoPersistent.GetValueOrDefault() == APInvoiceRecognitionEntry.ReloadData.VendorId);
    }
    nullable1 = e.Row.NoteID;
    if (nullable1.HasValue)
      this.ProcessFile(e.Cache, e.Row);
    e.Row.AllowUploadFile = new bool?(e.Row.RecognitionStatus == "F" || e.Row.RecognitionStatus == "N" || e.Row.RecognitionStatus == "E");
    this.Delete.SetVisible(e.Row.RecognitionStatus != "F");
    this.ContinueSave.SetVisible(e.Row.RecognitionStatus == "R" || e.Row.RecognitionStatus == "E");
    nullable1 = e.Row.FileID;
    this.ProcessRecognition.SetVisible(nullable1.HasValue && (e.Row.RecognitionStatus == "N" || e.Row.RecognitionStatus == "E"));
    this.OpenDocument.SetVisible(e.Row.RecognitionStatus == "P");
    nullable1 = e.Row.DuplicateLink;
    bool hasValue = nullable1.HasValue;
    this.OpenDuplicate.SetVisible(hasValue);
    this.SetWarningOnStatus(e.Cache, e.Row, hasValue, recognizedRecord);
    this.DeleteAllTransactions.SetEnabled(e.Row.RecognitionStatus == "R");
    if (APInvoiceRecognitionEntry.StatusValidForRecognitionSet.Contains(e.Row.RecognitionStatus))
    {
      nullable1 = e.Row.FileID;
      if (!nullable1.HasValue)
      {
        PXUIFieldAttribute.SetWarning<APRecognizedInvoice.recognitionStatus>(e.Cache, (object) e.Row, "The document cannot be recognized due to lack of attachment. Try resubmitting the PDF file.");
        goto label_16;
      }
    }
    if (e.Row.RecognitionStatus == "E" && !string.IsNullOrEmpty(e.Row.ErrorMessage))
      PXUIFieldAttribute.SetError<APRecognizedInvoice.recognitionStatus>(e.Cache, (object) e.Row, e.Row.ErrorMessage, e.Row.RecognitionStatus);
label_16:
    bool flag = e.Row.RecognitionStatus == "R" || e.Row.RecognitionStatus == "E";
    this.Document.AllowInsert = flag;
    this.Document.AllowUpdate = flag;
    this.Document.AllowDelete = flag;
    this.Transactions.AllowInsert = flag;
    this.Transactions.AllowUpdate = flag;
    this.Transactions.AllowDelete = flag;
    this.ViewErrorHistory.SetVisible(e.Row.RecognitionStatus == "E" && this.ErrorHistory.Select().Count > 0);
    this.SearchVendor.SetVisible(e.Row.RecognitionStatus == "R");
    int num;
    if (e.Row.RecognitionStatus == "I")
    {
      System.DateTime now = PXTimeZoneInfo.Now;
      System.DateTime? nullable2 = recognizedRecord?.LastModifiedDateTime.Value.AddMinutes(40.0);
      num = nullable2.HasValue ? (now >= nullable2.GetValueOrDefault() ? 1 : 0) : 0;
    }
    else
      num = 0;
    this.RefreshStatus.SetVisible(num != 0);
    this.HideNotSupportedActions();
  }

  private void SetWarningOnStatus(
    PXCache invoiceCache,
    APRecognizedInvoice recognizedInvoice,
    bool showOpenDuplicate,
    RecognizedRecord recognizedRecord)
  {
    string duplicateFileWarning = this.GetDuplicateFileWarning(showOpenDuplicate, recognizedRecord);
    string manyPagesWarning = APInvoiceRecognitionEntry.GetFileHasManyPagesWarning(recognizedRecord);
    StringBuilder stringBuilder = new StringBuilder();
    if (!string.IsNullOrEmpty(duplicateFileWarning))
      stringBuilder.Append(duplicateFileWarning);
    if (!string.IsNullOrEmpty(manyPagesWarning))
    {
      if (stringBuilder.Length > 0)
        stringBuilder.AppendLine();
      stringBuilder.Append(manyPagesWarning);
    }
    if (stringBuilder.Length == 0)
      return;
    PXUIFieldAttribute.SetWarning<APRecognizedInvoice.recognitionStatus>(invoiceCache, (object) recognizedInvoice, stringBuilder.ToString());
  }

  private string GetDuplicateFileWarning(bool showOpenDuplicate, RecognizedRecord recognizedRecord)
  {
    if (!showOpenDuplicate || recognizedRecord == null)
      return (string) null;
    string subject = this.CheckForDuplicates(recognizedRecord.RefNbr, recognizedRecord.FileHash).Subject;
    if (subject == null)
      return (string) null;
    return PXMessages.LocalizeFormatNoPrefixNLA("Recognition has already been started for another document: {0}.", (object) subject);
  }

  private static string GetFileHasManyPagesWarning(RecognizedRecord recognizedRecord)
  {
    if (recognizedRecord == null)
      return (string) null;
    int? pageCount = recognizedRecord.PageCount;
    int num = 50;
    return !(pageCount.GetValueOrDefault() > num & pageCount.HasValue) ? (string) null : PXMessages.LocalizeNoPrefix("The uploaded document has a significant number of pages that may cause an unsuccessful recognition.");
  }

  private void HideNotSupportedActions()
  {
    this.Actions["Scan"]?.SetVisible(false);
    if (this.Actions["AttachFromMobile"] == null)
      return;
    this.Actions["AttachFromMobile"] = (PXAction) this.AttachFromMobile;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<APRecognizedInvoice.docType> e)
  {
    if (!(e.Args.Row is APRecognizedInvoice row))
      return;
    string docType = row.DocType;
    string drCr = row.DrCr;
    foreach (PXResult<APRecognizedTran> pxResult in this.Transactions.Select())
    {
      APRecognizedTran apRecognizedTran = (APRecognizedTran) pxResult;
      this.Transactions.Cache.SetValue<APRecognizedTran.tranType>((object) apRecognizedTran, (object) docType);
      this.Transactions.Cache.SetValue<PX.Objects.AP.APTran.drCr>((object) apRecognizedTran, (object) drCr);
      if (apRecognizedTran.InventoryID.HasValue)
      {
        object inventoryId = (object) apRecognizedTran.InventoryID;
        try
        {
          this.Transactions.Cache.RaiseFieldVerifying<PX.Objects.AP.APTran.inventoryID>((object) apRecognizedTran, ref inventoryId);
        }
        catch (PXSetPropertyException ex)
        {
          this.Transactions.Cache.RaiseExceptionHandling<PX.Objects.AP.APTran.inventoryID>((object) apRecognizedTran, inventoryId, (Exception) ex);
        }
      }
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdating<PX.Objects.AP.InvoiceRecognition.DAC.BoundFeedback.tableRelated> e)
  {
    APRecognizedInvoice current = this.Document.Current;
    if (current == null || !"INV".Equals(current.DocType, StringComparison.Ordinal))
      return;
    DocumentFeedbackBuilder feedbackBuilder = current.FeedbackBuilder;
    if (feedbackBuilder == null)
      return;
    string newValue = e.NewValue as string;
    if (string.IsNullOrWhiteSpace(newValue))
      return;
    string cellBoundJson = HttpUtility.UrlDecode(newValue);
    feedbackBuilder.ProcessCellBound(cellBoundJson);
    e.NewValue = (object) null;
  }

  protected virtual void _(PX.Data.Events.FieldUpdating<PX.Objects.AP.InvoiceRecognition.DAC.BoundFeedback.fieldBound> e)
  {
    APRecognizedInvoice current1 = this.Document.Current;
    RecognizedRecord current2 = this.RecognizedRecords.Current;
    if (current1 == null || current2 == null || !"INV".Equals(current1.DocType, StringComparison.Ordinal))
      return;
    DocumentFeedbackBuilder feedbackBuilder = current1.FeedbackBuilder;
    if (feedbackBuilder == null)
      return;
    string newValue = e.NewValue as string;
    if (string.IsNullOrWhiteSpace(newValue))
      return;
    string documentJson = HttpUtility.UrlDecode(newValue);
    DocumentFeedback fieldBoundFeedback = feedbackBuilder.ToFieldBoundFeedback(documentJson);
    if (fieldBoundFeedback == null)
      return;
    StringBuilder sb = new StringBuilder(current2.RecognitionFeedback);
    StringWriter stringWriter = new StringWriter(sb, (IFormatProvider) CultureInfo.InvariantCulture);
    JsonTextWriter jsonTextWriter1 = new JsonTextWriter((TextWriter) stringWriter);
    ((JsonWriter) jsonTextWriter1).Formatting = (Newtonsoft.Json.Formatting) 0;
    using (JsonTextWriter jsonTextWriter2 = jsonTextWriter1)
    {
      sb.AppendLine();
      this._jsonSerializer.Serialize((JsonWriter) jsonTextWriter2, (object) fieldBoundFeedback);
    }
    this.RecognizedRecords.Cache.SetValue<RecognizedRecord.recognitionFeedback>((object) current2, (object) stringWriter.ToString());
    e.NewValue = (object) null;
  }

  protected virtual void _(PX.Data.Events.RowPersisting<APRecognizedTran> e) => e.Cancel = true;

  protected virtual void _(PX.Data.Events.RowSelected<APRecognizedTran> e)
  {
    if (e.Row == null)
      return;
    string str;
    if (e.Row.AlternateID != null)
    {
      int? foundIdByAlternate = e.Row.NumOfFoundIDByAlternate;
      int num = 1;
      if (foundIdByAlternate.GetValueOrDefault() > num & foundIdByAlternate.HasValue)
      {
        str = PXMessages.LocalizeNoPrefix("The specified alternate ID is assigned to multiple inventory items. Please make sure that the correct inventory ID has been specified in the row.");
        goto label_5;
      }
    }
    str = (string) null;
label_5:
    string error = str;
    PXUIFieldAttribute.SetWarning<APRecognizedTran.alternateID>(e.Cache, (object) e.Row, error);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<APRecognizedTran.alternateID> e)
  {
    if (!(e.Row is APRecognizedTran row))
      return;
    this.SetTranInventoryID(e.Cache, row);
    this.SetTranRecognizedPONumbers(e.Cache, (IEnumerable<APRecognizedTran>) new APRecognizedTran[1]
    {
      row
    }, false);
  }

  private void SetTranInventoryID(PXCache cache, APRecognizedTran tran)
  {
    cache.SetValueExt<APRecognizedTran.internalAlternateID>((object) tran, (object) tran.AlternateID);
    if (!tran.InternalAlternateID.HasValue && tran.InventoryIDManualInput.GetValueOrDefault())
      return;
    cache.SetValueExt<PX.Objects.AP.APTran.inventoryID>((object) tran, (object) tran.InternalAlternateID);
    this.Transactions.View.RequestRefresh();
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.AP.APTran.inventoryID> e)
  {
    if (!(e.Row is APRecognizedTran row))
      return;
    if ((e.NewValue as int?).HasValue)
      e.Cache.SetDefaultExt<PX.Objects.AP.APTran.uOM>((object) row);
    bool flag = e.ExternalCall && e.NewValue != null;
    this.SetTranRecognizedPONumbers(e.Cache, (IEnumerable<APRecognizedTran>) new APRecognizedTran[1]
    {
      row
    }, (flag ? 1 : 0) != 0);
  }

  private void SetTranRecognizedPONumbers(
    PXCache cache,
    IEnumerable<APRecognizedTran> transactions,
    bool isManualInput)
  {
    int? vendorId = (int?) this.Document.Current?.VendorID;
    HashSet<int?> inventoryIds = new HashSet<int?>();
    foreach (APRecognizedTran transaction in transactions)
    {
      if (transaction.InventoryID.HasValue)
      {
        if (!transaction.InventoryIDManualInput.GetValueOrDefault())
          transaction.InventoryIDManualInput = new bool?(isManualInput);
        if (vendorId.HasValue)
        {
          inventoryIds.Add(transaction.InventoryID);
        }
        else
        {
          cache.SetValueExt<APRecognizedTran.pONumberJson>((object) transaction, (object) null);
          this.AutoLinkAPAndPO(transaction, (string) null);
        }
      }
      else
      {
        cache.SetValueExt<APRecognizedTran.pONumberJson>((object) transaction, (object) null);
        this.AutoLinkAPAndPO(transaction, (string) null);
      }
    }
    if (inventoryIds.Count <= 0)
      return;
    if (transactions.Count<APRecognizedTran>() == 1)
      this.SetTranRecognizedPONumbers(vendorId, inventoryIds, transactions.First<APRecognizedTran>());
    else
      this.SetTranRecognizedPONumbers(vendorId, inventoryIds);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.AP.APTran.uOM> e)
  {
    if (!(e.Row is APRecognizedTran row) || !e.ExternalCall || e.NewValue is string)
      return;
    this.AutoLinkAPAndPO(row, (string) null);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.CM.Extensions.CurrencyInfo> e)
  {
    e.Cancel = true;
  }

  internal static bool IsAllowedFile(string name)
  {
    return !string.IsNullOrWhiteSpace(name) && string.Equals(Path.GetExtension(name), ".pdf", StringComparison.OrdinalIgnoreCase);
  }

  private void LoadRecognizedData(bool reloadVendorId)
  {
    RecognizedRecord record = this.RecognizedRecords.SelectSingle();
    if (record == null)
      return;
    if (string.IsNullOrEmpty(record.RecognitionResult))
    {
      this.Document.Current.RecognitionStatus = record.Status;
      this.Document.Current.DuplicateLink = record.DuplicateLink;
    }
    else
    {
      if (this.Caches[typeof (PX.Objects.AP.APRegister)].Current != this.Document.Current)
        this.Caches[typeof (PX.Objects.AP.APRegister)].Current = (object) this.Document.Current;
      RecognizedRecordDetail detail = this.RecognizedRecordDetails.SelectSingle((object) record.RefNbr, (object) record.EntityType);
      DocumentRecognitionResult recognitionResult = JsonConvert.DeserializeObject<DocumentRecognitionResult>(record.RecognitionResult);
      APInvoiceRecognitionEntry.LoadRecognizedDataToGraph(this, record, detail, recognitionResult, reloadVendorId);
    }
  }

  private void ProcessFile(PXCache cache, APRecognizedInvoice invoice)
  {
    Guid[] fileNotes = PXNoteAttribute.GetFileNotes(cache, (object) invoice);
    if (fileNotes == null || fileNotes.Length == 0)
    {
      Guid? nullable1 = invoice.FileID;
      if (!nullable1.HasValue)
        return;
      this.RemoveAttachedFile();
      this.UpdateFileInfo((UploadFile) null);
      APRecognizedInvoice recognizedInvoice = invoice;
      nullable1 = new Guid?();
      Guid? nullable2 = nullable1;
      recognizedInvoice.FileID = nullable2;
    }
    else
    {
      Guid guid1 = fileNotes[0];
      UploadFile file = APInvoiceRecognitionEntry.GetFile((PXGraph) this, guid1);
      Guid? nullable;
      if (invoice.RecognitionStatus == "F")
      {
        invoice.RecognitionStatus = "N";
        RecognizedRecord recognizedRecord = this.CreateRecognizedRecord(file.Name, file.Data, invoice, guid1);
        invoice.EntityType = recognizedRecord.EntityType;
        invoice.FileHash = recognizedRecord.FileHash;
        invoice.RecognitionStatus = recognizedRecord.Status;
        invoice.DuplicateLink = recognizedRecord.DuplicateLink;
      }
      else
      {
        nullable = invoice.FileID;
        if (nullable.HasValue)
        {
          nullable = invoice.FileID;
          Guid guid2 = guid1;
          if ((nullable.HasValue ? (nullable.GetValueOrDefault() != guid2 ? 1 : 0) : 1) != 0)
          {
            this.RemoveAttachedFile();
            this.UpdateFileInfo(file);
          }
          else if (fileNotes.Length == 2)
          {
            guid1 = fileNotes[1];
            file = APInvoiceRecognitionEntry.GetFile((PXGraph) this, guid1);
            this.RemoveAttachedFile();
            this.UpdateFileInfo(file);
          }
        }
      }
      if (file == null)
        return;
      invoice.FileID = new Guid?(guid1);
      PX.SM.FileInfo fileInfo1 = new PX.SM.FileInfo(guid1, file.Name, (string) null, file.Data);
      PXSessionState.Indexer<PX.SM.FileInfo> fileInfo2 = PXContext.SessionTyped<PXSessionStatePXData>().FileInfo;
      nullable = fileInfo1.UID;
      string key = nullable.ToString();
      PX.SM.FileInfo fileInfo3 = fileInfo1;
      fileInfo2[key] = fileInfo3;
    }
  }

  private void UpdateFileInfo(UploadFile file)
  {
    RecognizedRecord recognizedRecord = this.RecognizedRecords.SelectSingle();
    if (recognizedRecord == null)
      return;
    if (file == null)
    {
      recognizedRecord.Subject = (string) null;
      recognizedRecord.FileHash = (byte[]) null;
      recognizedRecord.DuplicateLink = new Guid?();
    }
    else
    {
      string shortName = PX.SM.FileInfo.GetShortName(file.Name);
      recognizedRecord.Subject = APInvoiceRecognitionEntry.GetRecognizedSubject((string) null, shortName);
      recognizedRecord.FileHash = APInvoiceRecognitionEntry.ComputeFileHash(file.Data);
      this.SetDuplicateLink(recognizedRecord);
    }
    recognizedRecord.Owner = PXAccess.GetContactID();
    this.Caches[typeof (Note)].Clear();
    this.SelectTimeStamp();
    this.RecognizedRecords.Cache.PersistUpdated((object) recognizedRecord);
    this.RecognizedRecords.Cache.Clear();
    this.RecognizedRecords.Cache.IsDirty = false;
    this.SelectTimeStamp();
  }

  public RecognizedRecord CreateRecognizedRecord(
    string fileName,
    byte[] fileData,
    Guid fileId,
    string description = null,
    string mailFrom = null,
    string messageId = null,
    int? owner = null,
    Guid? noteId = null)
  {
    RecognizedRecord recognizedRecord = this.RecognizedRecords.Insert();
    string shortName = PX.SM.FileInfo.GetShortName(fileName);
    recognizedRecord.Subject = description ?? APInvoiceRecognitionEntry.GetRecognizedSubject((string) null, shortName);
    recognizedRecord.MailFrom = mailFrom;
    recognizedRecord.MessageID = string.IsNullOrWhiteSpace(messageId) ? messageId : APInvoiceRecognitionEntry.NormalizeMessageId(messageId);
    recognizedRecord.FileHash = APInvoiceRecognitionEntry.ComputeFileHash(fileData ?? new byte[0]);
    recognizedRecord.Owner = owner ?? PXAccess.GetContactID();
    recognizedRecord.CloudTenantId = new Guid?(this._cloudTenantService.TenantId);
    recognizedRecord.ModelName = this.InvoiceRecognitionClient.ModelName;
    recognizedRecord.CloudFileId = new Guid?(fileId);
    if (noteId.HasValue)
      recognizedRecord.NoteID = noteId;
    this.SetDuplicateLink(recognizedRecord);
    APInvoiceRecognitionEntry.SetFilePageCount(recognizedRecord, fileData);
    this.RecognizedRecords.Cache.PersistInserted((object) recognizedRecord);
    this.RecognizedRecords.Cache.Persisted(false);
    this.SelectTimeStamp();
    return recognizedRecord;
  }

  private RecognizedRecord CreateRecognizedRecord(
    string fileName,
    byte[] fileData,
    APRecognizedInvoice recognizedInvoice,
    Guid fileId)
  {
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(fileName, nameof (fileName), (string) null);
    ExceptionExtensions.ThrowOnNull<byte[]>(fileData, nameof (fileData), (string) null);
    RecognizedRecord instance = (RecognizedRecord) this.RecognizedRecords.Cache.CreateInstance();
    instance.NoteID = recognizedInvoice.NoteID;
    instance.CustomInfo = recognizedInvoice.CustomInfo;
    instance.DocumentLink = recognizedInvoice.DocumentLink;
    instance.DuplicateLink = recognizedInvoice.DuplicateLink;
    instance.EntityType = recognizedInvoice.EntityType;
    instance.FileHash = APInvoiceRecognitionEntry.ComputeFileHash(fileData);
    instance.MailFrom = recognizedInvoice.MailFrom;
    instance.MessageID = recognizedInvoice.MessageID;
    instance.Owner = recognizedInvoice.Owner ?? PXAccess.GetContactID();
    instance.RecognitionResult = recognizedInvoice.RecognitionResult;
    instance.RecognitionStarted = recognizedInvoice.RecognitionStarted;
    instance.RefNbr = recognizedInvoice.RecognizedRecordRefNbr;
    instance.Status = recognizedInvoice.RecognitionStatus;
    string shortName = PX.SM.FileInfo.GetShortName(fileName);
    instance.Subject = APInvoiceRecognitionEntry.GetRecognizedSubject((string) null, shortName);
    this.SetDuplicateLink(instance);
    instance.CloudTenantId = new Guid?(this._cloudTenantService.TenantId);
    instance.ModelName = this.InvoiceRecognitionClient.ModelName;
    instance.CloudFileId = new Guid?(fileId);
    APInvoiceRecognitionEntry.SetFilePageCount(instance, fileData);
    RecognizedRecord row = (RecognizedRecord) this.RecognizedRecords.Cache.Insert((object) instance);
    this.RecognizedRecords.Cache.SetStatus((object) row, PXEntryStatus.Notchanged);
    this.RecognizedRecords.Cache.PersistInserted((object) row);
    this.RecognizedRecords.Cache.IsDirty = false;
    this.SelectTimeStamp();
    return row;
  }

  internal static byte[] ComputeFileHash(byte[] data)
  {
    using (MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider())
      return cryptoServiceProvider.ComputeHash(data);
  }

  private static void SetFilePageCount(RecognizedRecord record, byte[] data)
  {
    if (PdfReader.TestPdfFile(data) == 0)
      return;
    MemoryStream memoryStream = new MemoryStream(data);
    int pageCount;
    try
    {
      using (PdfDocument pdfDocument = PdfReader.Open((Stream) memoryStream, (PdfDocumentOpenMode) 2))
        pageCount = pdfDocument.PageCount;
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex);
      return;
    }
    record.PageCount = new int?(pageCount);
  }

  private void SetDuplicateLink(RecognizedRecord recognizedRecord)
  {
    Guid? refNbr = this.CheckForDuplicates(recognizedRecord.RefNbr, recognizedRecord.FileHash).RefNbr;
    recognizedRecord.DuplicateLink = refNbr;
  }

  public virtual void EnsureTransactions()
  {
    if (this.Transactions.Cache.Cached.Cast<object>().Any<object>())
    {
      foreach (APRecognizedTran data in this.Transactions.Cache.Cached)
      {
        APRecognizedInvoice current = this.Document.Current;
        long? nullable1;
        long? nullable2;
        if (current == null)
        {
          nullable1 = new long?();
          nullable2 = nullable1;
        }
        else
          nullable2 = current.CuryInfoID;
        long? nullable3 = nullable2;
        nullable1 = data.CuryInfoID;
        long? nullable4 = nullable3;
        if (!(nullable1.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable1.HasValue == nullable4.HasValue))
          this.Transactions.Cache.SetValue<PX.Objects.AP.APTran.curyInfoID>((object) data, (object) nullable3);
      }
    }
    else
    {
      APRecognizedInvoice current = this.Document.Current;
      if (current == null)
        return;
      APRecognizedTran apRecognizedTran = this.Transactions.Insert();
      if (apRecognizedTran == null)
        return;
      apRecognizedTran.TranDesc = current.DocDesc;
      apRecognizedTran.CuryLineAmt = current.CuryOrigDocAmt;
      this.Transactions.Update(apRecognizedTran);
    }
  }

  private void InsertRowWithFieldValues(
    PXCache sourceCache,
    PXCache destCache,
    IEnumerable<string> fieldsToCopy,
    HashSet<string> forcedFields,
    object sourceRow,
    System.Action<object> onAfterInsert = null)
  {
    object data = destCache.Insert();
    if (onAfterInsert != null)
      onAfterInsert(data);
    foreach (string fieldName in fieldsToCopy.Where<string>((Func<string, bool>) (field => destCache.Fields.Contains(field))))
    {
      object obj = PXFieldState.UnwrapValue(sourceCache.GetValueExt(sourceRow, fieldName));
      if (obj != null)
      {
        destCache.RaiseRowSelected(data);
        // ISSUE: explicit non-virtual call
        if ((destCache.GetStateExt(data, fieldName) is PXFieldState stateExt ? (!stateExt.Enabled ? 1 : 0) : 0) == 0 || (forcedFields != null ? (!__nonvirtual (forcedFields.Contains(fieldName)) ? 1 : 0) : 1) == 0)
        {
          destCache.SetValueExt(data, fieldName, (object) new PXCache.ExternalCallMarker(obj));
          destCache.Update(data);
        }
      }
    }
    destCache.SetStatus(data, PXEntryStatus.Inserted);
  }

  private void CopyFiles(APInvoiceEntry graph)
  {
    graph.Document.Cache.SetValueExt<PX.Objects.AP.APInvoice.noteID>((object) graph.Document.Current, (object) null);
    graph.Caches<APVendorRefNbr>().Clear();
    PXNoteAttribute.CopyNoteAndFiles(this.Document.Cache, (object) this.Document.Current, graph.Document.Cache, (object) graph.Document.Current, new bool?(false), new bool?(true));
    graph.Document.Cache.SetValue<APInvoiceExt.renameFileScreenId>((object) graph.Document.Current, (object) true);
  }

  private void InsertInvoiceData(APInvoiceEntry graph)
  {
    (IEnumerable<string> strings1, IEnumerable<string> strings2) = this.GetUIFields();
    string[] strArray1 = new string[1]{ "Hold" };
    IEnumerable<string> fieldsToCopy1 = ((IEnumerable<string>) strArray1).Union<string>(strings1, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this.InsertRowWithFieldValues(this.Document.Cache, graph.Document.Cache, fieldsToCopy1, ((IEnumerable<string>) strArray1).ToHashSet<string>(), (object) this.Document.Current);
    this.CopyFiles(graph);
    POAccrualSet usedPoAccrualSet = graph.GetUsedPOAccrualSet();
    string[] strArray2 = new string[2]
    {
      "ManualPrice",
      "ManualDisc"
    };
    IEnumerable<string> first = ((IEnumerable<string>) strArray2).Union<string>(strings2, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    IEnumerable<string> strings3 = first.Union<string>((IEnumerable<string>) LinkRecognizedLineExtension.APTranPOFields, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    string[] apTranPoFields = LinkRecognizedLineExtension.APTranPOFields;
    foreach (PXResult<APRecognizedTran> pxResult in this.Transactions.Select())
    {
      APRecognizedTran sourceRow = (APRecognizedTran) pxResult;
      if (sourceRow.ReceiptType != null && sourceRow.ReceiptNbr != null && sourceRow.ReceiptLineNbr.HasValue)
      {
        POReceiptLineS aLine = (POReceiptLineS) PXSelectBase<POReceiptLineS, PXViewOf<POReceiptLineS>.BasedOn<SelectFromBase<POReceiptLineS, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<POReceiptLineS.receiptType, Equal<P.AsString>>>>, PX.Data.And<BqlOperand<POReceiptLineS.receiptNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<POReceiptLineS.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, (object) sourceRow.ReceiptType, (object) sourceRow.ReceiptNbr, (object) sourceRow.ReceiptLineNbr);
        PX.Objects.AP.APTran data = graph.AddPOReceiptLine((IAPTranSource) aLine, (HashSet<PX.Objects.AP.APTran>) usedPoAccrualSet);
        graph.Transactions.Cache.SetValue((object) data, "qty", (object) sourceRow.BaseQty);
        graph.Transactions.Cache.SetValue((object) data, "curyUnitCost", (object) sourceRow.CuryUnitCost);
        graph.Transactions.Cache.Update((object) data);
      }
      else
      {
        sourceRow.ManualPrice = new bool?(true);
        sourceRow.ManualDisc = new bool?(true);
        IEnumerable<string> fieldsToCopy2;
        HashSet<string> hashSet;
        if (sourceRow.POLinkStatus == "L")
        {
          fieldsToCopy2 = strings3;
          hashSet = ((IEnumerable<string>) apTranPoFields).Union<string>((IEnumerable<string>) strArray2, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase).ToHashSet<string>();
        }
        else
        {
          fieldsToCopy2 = first;
          hashSet = ((IEnumerable<string>) strArray2).ToHashSet<string>();
        }
        this.InsertRowWithFieldValues(this.Transactions.Cache, graph.Transactions.Cache, fieldsToCopy2, hashSet, (object) sourceRow);
      }
    }
    APInvoiceEntryExt extension = graph.GetExtension<APInvoiceEntryExt>();
    extension.FeedbackParameters.Current.FeedbackBuilder = this.Document.Current.FeedbackBuilder;
    extension.FeedbackParameters.Current.Links = this.Document.Current.Links;
    RecognizedRecord recognizedRecord = PXSelectBase<RecognizedRecord, PXSelect<RecognizedRecord, Where<RecognizedRecord.refNbr, Equal<Required<RecognizedRecord.refNbr>>>>.Config>.Select((PXGraph) graph, (object) this.Document.Current.RecognizedRecordRefNbr).FirstTableItems.FirstOrDefault<RecognizedRecord>();
    if (recognizedRecord == null)
      return;
    recognizedRecord.DocumentLink = graph.Document.Current.NoteID;
    recognizedRecord.Status = "P";
    graph.Caches[typeof (RecognizedRecord)].Update((object) recognizedRecord);
    this.RecognizedRecords.View.Clear();
  }

  private void InsertCrossReferences(APInvoiceEntry graph)
  {
    APRecognizedInvoice current = this.Document.Current;
    if ((current != null ? (!current.VendorID.HasValue ? 1 : 0) : 1) != 0)
      return;
    ParameterExpression parameterExpression;
    // ISSUE: method reference
    List<APRecognizedTran> list = this.Transactions.Select().Select<PXResult<APRecognizedTran>, APRecognizedTran>(Expression.Lambda<Func<PXResult<APRecognizedTran>, APRecognizedTran>>((Expression) Expression.Call(r, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression)).Where<APRecognizedTran>((Expression<Func<APRecognizedTran, bool>>) (t => !string.IsNullOrEmpty(t.AlternateID) && t.InventoryID != new int?())).ToList<APRecognizedTran>();
    if (list.Count == 0)
      return;
    PXCache cach = graph.Caches[typeof (INItemXRef)];
    object newValue;
    cach.RaiseFieldDefaulting<INItemXRef.subItemID>((object) null, out newValue);
    PXViewOf<INItemXRef>.BasedOn<SelectFromBase<INItemXRef, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Match<BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>, PX.Data.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<INItemXRef.inventoryID, Equal<P.AsInt>>>>, PX.Data.And<BqlOperand<INItemXRef.alternateType, IBqlString>.IsEqual<INAlternateType.vPN>>>>.And<BqlOperand<INItemXRef.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>>, PX.Data.And<BqlOperand<INItemXRef.alternateID, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<INItemXRef.subItemID, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly readOnly = new PXViewOf<INItemXRef>.BasedOn<SelectFromBase<INItemXRef, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Match<BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>, PX.Data.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<INItemXRef.inventoryID, Equal<P.AsInt>>>>, PX.Data.And<BqlOperand<INItemXRef.alternateType, IBqlString>.IsEqual<INAlternateType.vPN>>>>.And<BqlOperand<INItemXRef.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>>, PX.Data.And<BqlOperand<INItemXRef.alternateID, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<INItemXRef.subItemID, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly((PXGraph) graph);
    foreach (APRecognizedTran apRecognizedTran in list)
    {
      if (readOnly.SelectSingle((object) apRecognizedTran.InventoryID, (object) this.Document.Current.VendorID, (object) apRecognizedTran.AlternateID, newValue) == null)
      {
        INItemXRef inItemXref = new INItemXRef()
        {
          InventoryID = apRecognizedTran.InventoryID,
          AlternateType = "0VPN",
          BAccountID = this.Document.Current.VendorID,
          AlternateID = apRecognizedTran.AlternateID,
          SubItemID = newValue as int?
        };
        cach.Insert((object) inItemXref);
      }
    }
  }

  private void InsertRecognizedRecordVendor(APInvoiceEntry graph)
  {
    if (string.IsNullOrEmpty(this.Document.Current?.VendorName))
      return;
    RecognizedVendorMapping recognizedVendorMapping = new RecognizedVendorMapping()
    {
      VendorNamePrefix = RecognizedVendorMapping.GetVendorPrefixFromName(this.Document.Current.VendorName),
      VendorName = this.Document.Current.VendorName
    };
    PXCache cach = graph.Caches[typeof (RecognizedVendorMapping)];
    cach.Insert((object) recognizedVendorMapping);
    cach.SetStatus((object) recognizedVendorMapping, PXEntryStatus.Held);
  }

  internal static string NormalizeMessageId(string rawMessageId)
  {
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(rawMessageId, nameof (rawMessageId), (string) null);
    int num = rawMessageId.IndexOf('>');
    return num == -1 || num == rawMessageId.Length - 1 ? rawMessageId : rawMessageId.Substring(0, num + 1);
  }

  internal static string GetRecognizedSubject(string emailSubject, string fileName)
  {
    return string.IsNullOrWhiteSpace(emailSubject) ? fileName : $"{emailSubject}: {fileName}";
  }

  public (Guid? RefNbr, string Subject) CheckForDuplicates(Guid? recognizedRefNbr, byte[] fileHash)
  {
    RecognizedRecord recognizedRecord = (RecognizedRecord) PXSelectBase<RecognizedRecord, PXViewOf<RecognizedRecord>.BasedOn<SelectFromBase<RecognizedRecord, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<RecognizedRecord.refNbr, NotEqual<P.AsGuid>>>>>.And<BqlOperand<RecognizedRecord.fileHash, IBqlByteArray>.IsEqual<P.AsByteArray>>>.Order<By<BqlField<RecognizedRecord.createdDateTime, IBqlDateTime>.Asc>>>.ReadOnly.Config>.Select((PXGraph) this, (object) recognizedRefNbr, (object) fileHash);
    return recognizedRecord == null ? (new Guid?(), (string) null) : (recognizedRecord.RefNbr, recognizedRecord.Subject);
  }

  public void UpdateDuplicates(Guid? refNbr)
  {
    FbqlSelect<SelectFromBase<RecognizedRecord, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<RecognizedRecord.duplicateLink, IBqlGuid>.IsEqual<P.AsGuid>>.Order<By<BqlField<RecognizedRecord.createdDateTime, IBqlDateTime>.Asc>>, RecognizedRecord>.View view = new FbqlSelect<SelectFromBase<RecognizedRecord, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<RecognizedRecord.duplicateLink, IBqlGuid>.IsEqual<P.AsGuid>>.Order<By<BqlField<RecognizedRecord.createdDateTime, IBqlDateTime>.Asc>>, RecognizedRecord>.View((PXGraph) this);
    Guid? nullable = new Guid?();
    object[] objArray = new object[1]{ (object) refNbr };
    foreach (PXResult<RecognizedRecord> pxResult in view.Select(objArray))
    {
      RecognizedRecord recognizedRecord = (RecognizedRecord) pxResult;
      if (!nullable.HasValue)
      {
        nullable = recognizedRecord.RefNbr;
        recognizedRecord.DuplicateLink = new Guid?();
      }
      else
        recognizedRecord.DuplicateLink = nullable;
      this.RecognizedRecords.Update(recognizedRecord);
    }
  }

  public static void RefreshInvoiceStatus(Guid recognizedRecordRefNbr, ILogger logger)
  {
    PXGraph.CreateInstance<APInvoiceRecognitionEntry>().RefreshInvoiceStatusInternal(recognizedRecordRefNbr, logger);
  }

  public static void RecognizeInvoiceData(Guid recognizedRecordRefNbr, Guid fileId, ILogger logger)
  {
    PXGraph.CreateInstance<APInvoiceRecognitionEntry>().RecognizeInvoiceDataInternal(recognizedRecordRefNbr, fileId, logger);
  }

  private void RefreshInvoiceStatusInternal(Guid recognizedRecordRefNbr, ILogger logger)
  {
    int startRow = 0;
    int maximumRows = 1;
    int totalRows = 1;
    this.Document.Current = this.Document.View.Select((object[]) null, (object[]) null, new object[1]
    {
      (object) recognizedRecordRefNbr
    }, new string[1]{ nameof (recognizedRecordRefNbr) }, (bool[]) null, (PXFilterRow[]) null, ref startRow, maximumRows, ref totalRows).First<object>() as APRecognizedInvoice;
    RecognizedRecord record = this.RecognizedRecords.SelectSingle();
    string errorMessage = (string) null;
    DocumentRecognitionResult recognitionResult = (DocumentRecognitionResult) null;
    using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(20.0)))
    {
      try
      {
        DocumentRecognitionResponse response = new DocumentRecognitionResponse(record.ResultUrl);
        recognitionResult = APInvoiceRecognitionEntry.PollForResults(this, record, this.InvoiceRecognitionClient, response, logger, cancellationTokenSource.Token).Result;
      }
      catch (AggregateException ex)
      {
        int? pageCount = record.PageCount;
        errorMessage = APInvoiceRecognitionEntry.GetRecognitionErrorMessage(ex, pageCount);
        throw;
      }
      finally
      {
        APInvoiceRecognitionEntry.UpdateRecognizedRecord(this, record, recognitionResult, errorMessage);
        this.PopulateRecognizedRecordDetail(record, recognitionResult).Wait();
      }
    }
  }

  private static string GetRecognitionErrorMessage(
    AggregateException exception,
    int? recognizedPageCount)
  {
    int? nullable = recognizedPageCount;
    int num = 50;
    bool fileHasManyPages = (nullable.GetValueOrDefault() > num & nullable.HasValue ? 1 : 0) != 0;
    return string.Join(Environment.NewLine, exception.Flatten().InnerExceptions.Select<Exception, string>((Func<Exception, string>) (i => (!fileHasManyPages || !(i is RecognitionServiceUnexpectedResponseException)) && !(i is TaskCanceledException) ? i.Message : PXMessages.LocalizeNoPrefix("The recognition service has failed to process the document. Try to recognize a document with a less number of pages."))));
  }

  private void RecognizeInvoiceDataInternal(
    Guid recognizedRecordRefNbr,
    Guid fileId,
    ILogger logger)
  {
    APRecognizedInvoice instance = (APRecognizedInvoice) this.Document.Cache.CreateInstance();
    instance.RecognizedRecordRefNbr = new Guid?(recognizedRecordRefNbr);
    int totalRows = 1;
    int startRow = 0;
    object obj = this.Document.View.Select((object[]) null, (object[]) null, new object[1]
    {
      (object) recognizedRecordRefNbr
    }, new string[1]{ nameof (recognizedRecordRefNbr) }, new bool[1], (PXFilterRow[]) null, ref startRow, 1, ref totalRows).FirstOrDefault<object>();
    if (obj != null)
      this.Document.Current = obj as APRecognizedInvoice;
    RecognizedRecord record = this.RecognizedRecords.SelectSingle();
    instance.RecognitionStatus = record.Status;
    try
    {
      UploadFile file = APInvoiceRecognitionEntry.GetFile((PXGraph) this, fileId);
      if (file == null)
        throw new PXException("There is no file attached to the document.");
      if (file.Data == null || file.Data.Length == 0)
        throw new PXException("The attached file is corrupted and could not be processed.");
      if (!APInvoiceRecognitionEntry.IsAllowedFile(file.Name))
        throw new PXArgumentException("file", PXMessages.LocalizeFormatNoPrefixNLA("Invalid file for recognition. Only the following file types are allowed: {0}.", (object) ".pdf"));
      if (!record.RecognitionStarted.GetValueOrDefault())
        APInvoiceRecognitionEntry.MarkRecognitionStarted(this, record, (string) null);
      if (record.Status == "E")
        APInvoiceRecognitionEntry.SetNewCloudFileId(this, record);
      DocumentRecognitionResult recognitionResult = (DocumentRecognitionResult) null;
      string errorMessage = (string) null;
      try
      {
        PXTrace.WriteInformation("Starting Recognition of the \"{0}\" file", (object) file.Name);
        recognitionResult = APInvoiceRecognitionEntry.GetRecognitionInfo(this, record, this.InvoiceRecognitionClient, file, logger, record.CloudFileId.Value).Result;
      }
      catch (AggregateException ex)
      {
        int? pageCount = record.PageCount;
        errorMessage = APInvoiceRecognitionEntry.GetRecognitionErrorMessage(ex, pageCount);
        throw;
      }
      finally
      {
        APInvoiceRecognitionEntry.UpdateRecognizedRecord(this, record, recognitionResult, errorMessage);
        this.PopulateRecognizedRecordDetail(record, recognitionResult).Wait();
      }
    }
    catch (PXException ex)
    {
      APInvoiceRecognitionEntry.UpdateRecognizedRecord(this, record, (DocumentRecognitionResult) null, ex.Message);
      instance.RecognitionStatus = "E";
      throw;
    }
    catch
    {
      instance.RecognitionStatus = "E";
      throw;
    }
  }

  private async System.Threading.Tasks.Task PopulateRecognizedRecordDetail(
    RecognizedRecord record,
    DocumentRecognitionResult recognitionResult)
  {
    if (record.Status != "R")
      return;
    await this.DetailsPopulator.FillRecognizedFields(record, recognitionResult);
  }

  internal async System.Threading.Tasks.Task PopulateVendorId(
    RecognizedRecord record,
    RecognizedRecordDetail detail)
  {
    if (record.Status != "R")
      return;
    await this.DetailsPopulator.FillVendorId(record, detail);
  }

  internal static UploadFile GetFile(PXGraph graph, Guid fileId)
  {
    PXResult<UploadFile, UploadFileRevision> pxResult = (PXResult<UploadFile, UploadFileRevision>) (PXResult<UploadFile>) PXSelectBase<UploadFile, PXSelectJoin<UploadFile, InnerJoin<UploadFileRevision, On<UploadFile.fileID, Equal<UploadFileRevision.fileID>, And<UploadFile.lastRevisionID, Equal<UploadFileRevision.fileRevisionID>>>>, Where<UploadFile.fileID, Equal<Required<UploadFile.fileID>>>>.Config>.Select(graph, (object) fileId);
    if (pxResult == null)
      return (UploadFile) null;
    UploadFile file = (UploadFile) pxResult;
    UploadFileRevision uploadFileRevision = (UploadFileRevision) pxResult;
    file.Data = uploadFileRevision.Data;
    return file;
  }

  private static void SetNewCloudFileId(APInvoiceRecognitionEntry graph, RecognizedRecord record)
  {
    record.CloudFileId = new Guid?(Guid.NewGuid());
    graph.RecognizedRecords.Update(record);
    graph.Persist();
  }

  private static void MarkRecognitionStarted(
    APInvoiceRecognitionEntry graph,
    RecognizedRecord record,
    string url)
  {
    record.RecognitionStarted = new bool?(true);
    record.Status = "I";
    record.ResultUrl = url;
    graph.RecognizedRecords.Update(record);
    graph.Persist();
  }

  private static void UpdateRecognizedRecordUrl(
    APInvoiceRecognitionEntry graph,
    RecognizedRecord record,
    string url)
  {
    record.ResultUrl = url;
    record = graph.RecognizedRecords.Update(record);
    graph.RecognizedRecords.Cache.PersistUpdated((object) record);
    graph.RecognizedRecords.Cache.Persisted(false);
    graph.SelectTimeStamp();
  }

  private static void UpdateRecognizedRecord(
    APInvoiceRecognitionEntry graph,
    RecognizedRecord record,
    DocumentRecognitionResult recognitionResult,
    string errorMessage)
  {
    bool flag = recognitionResult == null;
    record.RecognitionResult = JsonConvert.SerializeObject((object) recognitionResult);
    int? count = recognitionResult?.Pages?.Count;
    int? nullable1 = record.PageCount;
    int? nullable2;
    if (nullable1.HasValue)
    {
      if (count.HasValue)
      {
        nullable1 = count;
        nullable2 = record.PageCount;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          goto label_4;
      }
      else
        goto label_4;
    }
    record.PageCount = new int?(count.GetValueOrDefault());
label_4:
    if (!string.IsNullOrEmpty(errorMessage))
    {
      record.ErrorMessage = errorMessage;
      flag = true;
    }
    else
    {
      if (count.HasValue)
      {
        nullable2 = count;
        int num = 0;
        if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
          goto label_9;
      }
      record.ErrorMessage = "The recognition service returned empty result, contact your Acumatica support provider for assistance.";
      flag = true;
    }
label_9:
    record.Status = flag ? "E" : "R";
    if (record.Status == "R")
    {
      record.ErrorMessage = (string) null;
    }
    else
    {
      PXCache cach = graph.Caches[typeof (RecognizedRecordErrorHistory)];
      RecognizedRecordErrorHistory instance = cach.CreateInstance() as RecognizedRecordErrorHistory;
      instance.RefNbr = record.RefNbr;
      instance.EntityType = record.EntityType;
      instance.CloudFileId = record.CloudFileId;
      instance.ErrorMessage = record.ErrorMessage;
      cach.PersistInserted((object) (cach.Insert((object) instance) as RecognizedRecordErrorHistory));
    }
    record = graph.RecognizedRecords.Update(record);
    graph.RecognizedRecords.Cache.PersistUpdated((object) record);
    graph.RecognizedRecords.Cache.Persisted(false);
    graph.SelectTimeStamp();
  }

  private static async Task<DocumentRecognitionResult> GetRecognitionInfo(
    APInvoiceRecognitionEntry graph,
    RecognizedRecord record,
    IInvoiceRecognitionService client,
    UploadFile file,
    ILogger logger,
    Guid cloudFileId)
  {
    string mimeType = MimeTypes.GetMimeType(Path.GetExtension(file.Name));
    DocumentRecognitionResult recognitionInfo;
    using (SerilogTimings.Operation op = LoggerOperationExtensions.OperationAt(logger, (LogEventLevel) 0, new LogEventLevel?((LogEventLevel) 4)).Begin("Recognizing document", Array.Empty<object>()))
    {
      using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(20.0)))
      {
        try
        {
          DocumentRecognitionResponse response = await client.SendFile(cloudFileId, file.Data, mimeType, cancellationTokenSource.Token);
          APInvoiceRecognitionEntry.UpdateRecognizedRecordUrl(graph, record, response.State);
          DocumentRecognitionResult recognitionResult = await APInvoiceRecognitionEntry.PollForResults(graph, record, client, response, logger, cancellationTokenSource.Token);
          op.Complete();
          recognitionInfo = recognitionResult;
        }
        catch (Exception ex)
        {
          op.SetException(ex);
          throw;
        }
      }
    }
    return recognitionInfo;
  }

  private static async Task<DocumentRecognitionResult> PollForResults(
    APInvoiceRecognitionEntry graph,
    RecognizedRecord record,
    IInvoiceRecognitionService imageRecognitionWebClient,
    DocumentRecognitionResponse response,
    ILogger logger,
    CancellationToken cancellationToken)
  {
    (DocumentRecognitionResult result, string state1) = response;
    DocumentRecognitionResult recognitionResult1 = result;
    string state = state1;
    if (recognitionResult1 != null)
    {
      APInvoiceRecognitionEntry.LogSyncResponse(logger);
      return recognitionResult1;
    }
    using (SerilogTimings.Operation op = logger.BeginOperationVerbose("Polling for recognition results"))
    {
      int attempts = 0;
      while (!cancellationToken.IsCancellationRequested)
      {
        if (state == null)
          throw new InvalidOperationException("Unexpected empty state in document recognition response");
        ++attempts;
        (result, state1) = await imageRecognitionWebClient.GetResult(state, cancellationToken);
        DocumentRecognitionResult recognitionResult2 = result;
        state = state1;
        if (recognitionResult2 != null)
        {
          op.Complete("Attempts", (object) attempts, false);
          return recognitionResult2;
        }
        if (record.ResultUrl != state)
          APInvoiceRecognitionEntry.UpdateRecognizedRecordUrl(graph, record, state);
        await System.Threading.Tasks.Task.Delay(APInvoiceRecognitionEntry.RecognitionPollingInterval, cancellationToken);
      }
      op.EnrichWith("Attempts", (object) attempts, false);
      throw new PXException("Waiting time exceeded");
    }
  }

  private static void LogSyncResponse(ILogger logger)
  {
    logger.Verbose("Recognition returned result synchronously");
  }

  private static void LoadRecognizedDataToGraph(
    APInvoiceRecognitionEntry graph,
    RecognizedRecord record,
    RecognizedRecordDetail detail,
    DocumentRecognitionResult recognitionResult,
    bool reloadVendorId)
  {
    APRecognizedInvoice current = graph.Document.Current;
    PXCache cache = graph.Document.Cache;
    cache.SetValue<APRecognizedInvoice.recognitionStatus>((object) current, (object) record.Status);
    cache.SetValue<APRecognizedInvoice.duplicateLink>((object) current, (object) record.DuplicateLink);
    cache.SetValue<APRecognizedInvoice.recognizedDataJson>((object) current, (object) HttpUtility.UrlEncode(record.RecognitionResult));
    cache.SetValue<APRecognizedInvoice.noteID>((object) current, (object) record.NoteID);
    cache.SetValue<APRecognizedInvoice.isDataLoaded>((object) current, (object) true);
    if (recognitionResult == null)
      return;
    if (current.RecognitionStatus != "P")
      cache.SetValue<APRecognizedInvoice.curyLineTotal>((object) current, (object) 0M);
    if (PXSiteMap.Provider.FindSiteMapNodesByGraphType(typeof (APInvoiceRecognitionEntry).FullName).FirstOrDefault<PXSiteMapNode>() == null)
      return;
    IEnumerable<string> detailFields = graph.GetUIFields().DetailFields;
    if (detailFields == null)
      return;
    if (current.RecognitionStatus != "P")
    {
      int? vendorId = (int?) detail?.VendorID;
      new InvoiceDataLoader(recognitionResult, graph, detailFields.ToArray<string>(), vendorId, reloadVendorId).Load((object) current);
      cache.SetValue<APRecognizedInvoice.vendorTermIndex>((object) current, (object) (int?) detail?.VendorTermIndex);
      cache.SetValue<APRecognizedInvoice.vendorName>((object) current, (object) detail?.VendorName);
      if (detail != null)
      {
        int? nullable1 = detail.DefaultBranchID;
        if (nullable1.HasValue)
        {
          PXCache pxCache = cache;
          APRecognizedInvoice data = current;
          int? nullable2;
          if (detail == null)
          {
            nullable1 = new int?();
            nullable2 = nullable1;
          }
          else
            nullable2 = detail.DefaultBranchID;
          // ISSUE: variable of a boxed type
          __Boxed<int?> local = (ValueType) nullable2;
          pxCache.SetValue<PX.Objects.AP.APInvoice.branchID>((object) data, (object) local);
        }
      }
    }
    current.FeedbackBuilder = graph.GetFeedbackBuilder();
    current.Links = recognitionResult.Links;
    graph.Document.Cache.IsDirty = false;
    graph.Transactions.Cache.IsDirty = false;
  }

  private static async System.Threading.Tasks.Task SendVendorSearchFeedbackAsync(
    Dictionary<string, Uri> links,
    IInvoiceRecognitionFeedback feedbackService,
    VendorSearchFeedback feedback)
  {
    Uri address;
    if (!links.TryGetValue("feedback:entity-resolution", out address))
    {
      PXTrace.WriteError("IDocumentRecognitionClient: Unable to send feedback - link is not found:{LinkKey}", (object) "feedback:entity-resolution");
    }
    else
    {
      JsonMediaTypeFormatter mediaTypeFormatter1 = new JsonMediaTypeFormatter();
      ((BaseJsonMediaTypeFormatter) mediaTypeFormatter1).SerializerSettings = VendorSearchFeedback.Settings;
      JsonMediaTypeFormatter mediaTypeFormatter2 = mediaTypeFormatter1;
      ObjectContent content = new ObjectContent(feedback.GetType(), (object) feedback, (MediaTypeFormatter) mediaTypeFormatter2);
      await feedbackService.Send(address, (HttpContent) content);
    }
  }

  private (IEnumerable<string> PrimaryFields, IEnumerable<string> DetailFields) GetUIFields()
  {
    PXSiteMapNode pxSiteMapNode = PXSiteMap.Provider.FindSiteMapNodesByGraphType(typeof (APInvoiceRecognitionEntry).FullName).FirstOrDefault<PXSiteMapNode>();
    if (pxSiteMapNode == null)
      return ((IEnumerable<string>) null, (IEnumerable<string>) null);
    if (PXContext.GetSlot<bool?>(APInvoiceRecognitionEntry._screenInfoLoad).GetValueOrDefault())
      return ((IEnumerable<string>) null, (IEnumerable<string>) null);
    PXContext.SetSlot<bool>(APInvoiceRecognitionEntry._screenInfoLoad, true);
    PXSiteMap.ScreenInfo screenInfo;
    try
    {
      screenInfo = this.ScreenInfoProvider.TryGet(pxSiteMapNode.ScreenID);
    }
    finally
    {
      PXContext.ClearSlot(APInvoiceRecognitionEntry._screenInfoLoad);
    }
    if (screenInfo == null)
      return ((IEnumerable<string>) null, (IEnumerable<string>) null);
    PXViewDescription pxViewDescription1;
    PXViewDescription pxViewDescription2;
    return !screenInfo.Containers.TryGetValue("Document", out pxViewDescription1) || !screenInfo.Containers.TryGetValue("Transactions", out pxViewDescription2) ? ((IEnumerable<string>) null, (IEnumerable<string>) null) : (((IEnumerable<PX.Data.Description.FieldInfo>) pxViewDescription1.Fields).Select<PX.Data.Description.FieldInfo, string>((Func<PX.Data.Description.FieldInfo, string>) (f => f.FieldName)), ((IEnumerable<PX.Data.Description.FieldInfo>) pxViewDescription2.Fields).Select<PX.Data.Description.FieldInfo, string>((Func<PX.Data.Description.FieldInfo, string>) (f => f.FieldName)));
  }

  private DocumentFeedbackBuilder GetFeedbackBuilder()
  {
    (IEnumerable<string> strings1, IEnumerable<string> strings2) = this.GetUIFields();
    return strings1 == null || strings2 == null ? (DocumentFeedbackBuilder) null : new DocumentFeedbackBuilder(this.Document.Cache, strings1.ToHashSet<string>(), strings2.ToHashSet<string>());
  }

  internal static System.Threading.Tasks.Task RecognizeRecordsBatch(
    IEnumerable<RecognizedRecordFileInfo> batch,
    CancellationToken cancellationToken = default (CancellationToken))
  {
    return APInvoiceRecognitionEntry.RecognizeRecordsBatch(batch, (string) null, (string) null, (string) null, new int?(), false, cancellationToken);
  }

  internal static async System.Threading.Tasks.Task RecognizeRecordsBatch(
    IEnumerable<RecognizedRecordFileInfo> batch,
    string subject = null,
    string mailFrom = null,
    string messageId = null,
    int? ownerId = null,
    bool newFiles = false,
    CancellationToken externalCancellationToken = default (CancellationToken))
  {
    APInvoiceRecognitionEntry recognitionGraph = PXGraph.CreateInstance<APInvoiceRecognitionEntry>();
    List<RecognizedRecord> listToProcess = await APInvoiceRecognitionEntry.StartFilesRecogntion(recognitionGraph, batch, subject, mailFrom, messageId, ownerId, newFiles);
    await APInvoiceRecognitionEntry.ProcessStartedFiles(recognitionGraph, listToProcess, externalCancellationToken);
    bool flag = false;
    foreach (RecognizedRecord currentItem in listToProcess.Where<RecognizedRecord>((Func<RecognizedRecord, bool>) (r => r.Status == "E")))
    {
      flag = true;
      PXProcessing.SetCurrentItem((object) currentItem);
      PXProcessing.SetError(currentItem.ErrorMessage);
    }
    if (flag)
      throw new PXException("At least one file could not be processed. For details about each error, see the error message displayed for the particular document.");
    recognitionGraph = (APInvoiceRecognitionEntry) null;
    listToProcess = (List<RecognizedRecord>) null;
  }

  private static async Task<List<RecognizedRecord>> StartFilesRecogntion(
    APInvoiceRecognitionEntry recognitionGraph,
    IEnumerable<RecognizedRecordFileInfo> batch,
    string subject,
    string mailFrom,
    string messageId,
    int? ownerId,
    bool newFiles)
  {
    List<RecognizedRecord> listToProcess = new List<RecognizedRecord>();
    foreach (RecognizedRecordFileInfo recognizedRecordFileInfo in batch)
    {
      string errorMessage = (string) null;
      string str1 = (string) null;
      if (recognizedRecordFileInfo.FileId == Guid.Empty)
        errorMessage = "There is no file attached to the document.";
      else if (string.IsNullOrWhiteSpace(recognizedRecordFileInfo.FileName) || recognizedRecordFileInfo.FileData == null || recognizedRecordFileInfo.FileData.Length == 0)
        errorMessage = "The attached file is corrupted and could not be processed.";
      else if (!APInvoiceRecognitionEntry.IsAllowedFile(str1 = PX.SM.FileInfo.GetShortName(recognizedRecordFileInfo.FileName)))
        errorMessage = PXMessages.LocalizeNoPrefix("Only PDF files can be recognized.");
      else if (newFiles)
      {
        PX.SM.FileInfo finfo = new PX.SM.FileInfo(recognizedRecordFileInfo.FileId, recognizedRecordFileInfo.FileName, (string) null, recognizedRecordFileInfo.FileData);
        if (!PXGraph.CreateInstance<UploadFileMaintenance>().SaveFile(finfo))
          errorMessage = PXMessages.LocalizeFormatNoPrefixNLA("The {0} file cannot be saved.", (object) recognizedRecordFileInfo.FileName);
      }
      RecognizedRecord recognizedRecord = recognizedRecordFileInfo.RecognizedRecord;
      if (recognizedRecord == null)
      {
        string recognizedSubject = APInvoiceRecognitionEntry.GetRecognizedSubject(subject, str1);
        if (!string.IsNullOrWhiteSpace(messageId))
          messageId = APInvoiceRecognitionEntry.NormalizeMessageId(messageId);
        recognizedRecord = recognitionGraph.CreateRecognizedRecord(str1, recognizedRecordFileInfo.FileData, recognizedRecordFileInfo.FileId, recognizedSubject, mailFrom, messageId, ownerId);
        if (!string.IsNullOrEmpty(errorMessage))
        {
          APInvoiceRecognitionEntry.UpdateRecognizedRecord(recognitionGraph, recognizedRecord, (DocumentRecognitionResult) null, errorMessage);
          PXProcessing.SetCurrentItem((object) recognizedRecord);
          PXProcessing.SetError(recognizedRecord.ErrorMessage);
          continue;
        }
        PXNoteAttribute.ForcePassThrow<RecognizedRecord.noteID>(recognitionGraph.RecognizedRecords.Cache);
        PXNoteAttribute.SetFileNotes(recognitionGraph.RecognizedRecords.Cache, (object) recognizedRecord, recognizedRecordFileInfo.FileId);
      }
      else if (recognizedRecord.Status == "E")
        APInvoiceRecognitionEntry.SetNewCloudFileId(recognitionGraph, recognizedRecord);
      if (!APInvoiceRecognitionEntry.IsAllowedFile(str1))
      {
        APInvoiceRecognitionEntry.UpdateRecognizedRecord(recognitionGraph, recognizedRecord, (DocumentRecognitionResult) null, PXMessages.LocalizeNoPrefix("Only PDF files can be recognized."));
        PXProcessing.SetCurrentItem((object) recognizedRecord);
        PXProcessing.SetError(recognizedRecord.ErrorMessage);
      }
      else
      {
        string mimeType = MimeTypes.GetMimeType(Path.GetExtension(str1));
        DocumentRecognitionResult recognitionResult;
        string str2;
        try
        {
          (recognitionResult, str2) = await recognitionGraph.InvoiceRecognitionClient.SendFile(recognizedRecordFileInfo.FileId, recognizedRecordFileInfo.FileData, mimeType, CancellationToken.None);
        }
        catch (Exception ex)
        {
          APInvoiceRecognitionEntry.UpdateRecognizedRecord(recognitionGraph, recognizedRecord, (DocumentRecognitionResult) null, ex.Message);
          PXProcessing.SetCurrentItem((object) recognizedRecord);
          PXProcessing.SetError(recognizedRecord.ErrorMessage);
          continue;
        }
        APInvoiceRecognitionEntry.MarkRecognitionStarted(recognitionGraph, recognizedRecord, str2);
        if (recognitionResult != null)
        {
          APInvoiceRecognitionEntry.UpdateRecognizedRecord(recognitionGraph, recognizedRecord, recognitionResult, (string) null);
          PXProcessing.SetCurrentItem((object) recognizedRecord);
          PXProcessing.SetProcessed();
        }
        else
          listToProcess.Add(recognizedRecord);
        recognizedRecord = (RecognizedRecord) null;
      }
    }
    List<RecognizedRecord> recognizedRecordList = listToProcess;
    listToProcess = (List<RecognizedRecord>) null;
    return recognizedRecordList;
  }

  private static async System.Threading.Tasks.Task ProcessStartedFiles(
    APInvoiceRecognitionEntry recognitionGraph,
    List<RecognizedRecord> listToProcess,
    CancellationToken externalCancellationToken)
  {
    IEnumerable<RecognizedRecord> filesInProgress = listToProcess.Where<RecognizedRecord>((Func<RecognizedRecord, bool>) (r => r.Status == "I"));
    int filesToProcessCount = filesInProgress.Count<RecognizedRecord>();
    using (CancellationTokenSource timedCts = new CancellationTokenSource(TimeSpan.FromMinutes(20.0)))
    {
      using (CancellationTokenSource joinedCts = CancellationTokenSource.CreateLinkedTokenSource(externalCancellationToken, timedCts.Token))
      {
        CancellationToken cancellationToken = joinedCts.Token;
        using (SerilogTimings.Operation op = recognitionGraph._logger.BeginOperationVerbose("Polling for recognition results"))
        {
          int attempts = 0;
          try
          {
            int processedFilesCount = 0;
            while (!cancellationToken.IsCancellationRequested && processedFilesCount < filesToProcessCount)
            {
              ++attempts;
              foreach (RecognizedRecord recognizedRecord in filesInProgress)
              {
                DocumentRecognitionResult result = (DocumentRecognitionResult) null;
                string state = (string) null;
                try
                {
                  (result, state) = await recognitionGraph.InvoiceRecognitionClient.GetResult(recognizedRecord.ResultUrl, cancellationToken);
                }
                catch (Exception ex)
                {
                  int? pageCount = recognizedRecord.PageCount;
                  int num = 50;
                  APInvoiceRecognitionEntry.UpdateRecognizedRecord(recognitionGraph, recognizedRecord, (DocumentRecognitionResult) null, !(pageCount.GetValueOrDefault() > num & pageCount.HasValue) || !(ex is RecognitionServiceUnexpectedResponseException) ? ex.Message : PXMessages.LocalizeNoPrefix("The recognition service has failed to process the document. Try to recognize a document with a less number of pages."));
                  PXProcessing.SetCurrentItem((object) recognizedRecord);
                  PXProcessing.SetError(ex);
                }
                if (recognizedRecord.Status == "E")
                  ++processedFilesCount;
                else if (result != null)
                {
                  op.Complete("Attempts", (object) attempts, false);
                  APInvoiceRecognitionEntry.UpdateRecognizedRecord(recognitionGraph, recognizedRecord, result, (string) null);
                  await recognitionGraph.PopulateRecognizedRecordDetail(recognizedRecord, result);
                  ++processedFilesCount;
                  PXProcessing.SetCurrentItem((object) recognizedRecord);
                  PXProcessing.SetProcessed();
                }
                else if (state != recognizedRecord.ResultUrl)
                  APInvoiceRecognitionEntry.UpdateRecognizedRecordUrl(recognitionGraph, recognizedRecord, state);
                result = (DocumentRecognitionResult) null;
                state = (string) null;
              }
              if (processedFilesCount < filesToProcessCount)
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1.0), cancellationToken);
            }
            if (processedFilesCount < filesToProcessCount)
            {
              op.EnrichWith("Attempts", (object) attempts, false);
              throw new PXException("Waiting time exceeded");
            }
          }
          catch (Exception ex)
          {
            foreach (RecognizedRecord record in filesInProgress)
              APInvoiceRecognitionEntry.UpdateRecognizedRecord(recognitionGraph, record, (DocumentRecognitionResult) null, ex.Message);
          }
        }
        cancellationToken = new CancellationToken();
      }
    }
    filesInProgress = (IEnumerable<RecognizedRecord>) null;
  }

  internal static bool IsRecognitionInProgress(string messageId)
  {
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(messageId, nameof (messageId), (string) null);
    messageId = APInvoiceRecognitionEntry.NormalizeMessageId(messageId);
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<RecognizedRecord>((PXDataField) new PXDataField<RecognizedRecord.refNbr>(), (PXDataField) new PXDataFieldValue<RecognizedRecord.messageID>((object) messageId), (PXDataField) new PXDataFieldValue<RecognizedRecord.status>((object) "I")))
      return pxDataRecord != null;
  }

  internal static UploadFile[] GetFilesToRecognize(PXCache cache, object row)
  {
    Guid[] fileNotes = PXNoteAttribute.GetFileNotes(cache, row);
    return fileNotes == null ? (UploadFile[]) null : ((IEnumerable<Guid>) fileNotes).Select<Guid, UploadFile>((Func<Guid, UploadFile>) (n => APInvoiceRecognitionEntry.GetFile(cache.Graph, n))).Where<UploadFile>((Func<UploadFile, bool>) (file => file != null && file.Name != null && APInvoiceRecognitionEntry.IsAllowedFile(file.Name))).ToArray<UploadFile>();
  }

  public virtual IList<(int? InventoryId, string PONumber)> GetPONumbers(
    int? vendorId,
    HashSet<int?> inventoryIds)
  {
    List<(int?, string)> poNumbers = new List<(int?, string)>();
    foreach (PXResult<PX.Objects.PO.POLine> pxResult in PXSelectBase<PX.Objects.PO.POLine, PXViewOf<PX.Objects.PO.POLine>.BasedOn<SelectFromBase<PX.Objects.PO.POLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.PO.POLine.vendorID, Equal<P.AsInt>>>>, PX.Data.And<BqlOperand<PX.Objects.PO.POLine.cancelled, IBqlBool>.IsEqual<False>>>, PX.Data.And<BqlOperand<PX.Objects.PO.POLine.closed, IBqlBool>.IsEqual<False>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<BqlOperand<PX.Objects.PO.POLine.curyUnbilledAmt, IBqlDecimal>.IsNotEqual<decimal0>>>>.Or<BqlOperand<PX.Objects.PO.POLine.unbilledQty, IBqlDecimal>.IsNotEqual<decimal0>>>>>.Config>.Select((PXGraph) this, (object) vendorId))
    {
      PX.Objects.PO.POLine poLine = (PX.Objects.PO.POLine) pxResult;
      if (inventoryIds.Contains(poLine.InventoryID))
        poNumbers.Add((poLine.InventoryID, poLine.OrderNbr));
    }
    return (IList<(int?, string)>) poNumbers;
  }

  public string Caption()
  {
    if (this.Document.Cache.GetStatus((object) this.Document.Current) == PXEntryStatus.Inserted)
      return PXMessages.Localize("New Record");
    return this.Document.Current?.Subject;
  }

  public class MultiCurrency : APMultiCurrencyGraph<APInvoiceRecognitionEntry, APRecognizedInvoice>
  {
    protected override string DocumentStatus => this.Base.Document.Current?.Status;

    protected override MultiCurrencyGraph<APInvoiceRecognitionEntry, APRecognizedInvoice>.CurySourceMapping GetCurySourceMapping()
    {
      return new MultiCurrencyGraph<APInvoiceRecognitionEntry, APRecognizedInvoice>.CurySourceMapping(typeof (VendorR));
    }

    protected override MultiCurrencyGraph<APInvoiceRecognitionEntry, APRecognizedInvoice>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<APInvoiceRecognitionEntry, APRecognizedInvoice>.DocumentMapping(typeof (APRecognizedInvoice))
      {
        DocumentDate = typeof (APRecognizedInvoice.docDate),
        BAccountID = typeof (APRecognizedInvoice.vendorID)
      };
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[2]
      {
        (PXSelectBase) this.Base.Document,
        (PXSelectBase) this.Base.Transactions
      };
    }
  }

  public class PXDeleteWithRecognizedRecord<TNode> : PXDelete<TNode> where TNode : class, IBqlTable, new()
  {
    public PXDeleteWithRecognizedRecord(PXGraph graph, string name)
      : base(graph, name)
    {
    }

    public PXDeleteWithRecognizedRecord(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }

    [PXUIField(DisplayName = "Delete", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
    [PXDeleteButton(ConfirmationMessage = "The current {0} record will be deleted.")]
    protected override IEnumerable Handler(PXAdapter adapter)
    {
      if (this.Graph is APInvoiceRecognitionEntry graph)
      {
        APRecognizedInvoice current1 = graph.Document.Current;
        if ((current1 != null ? (current1.ReleasedOrPrebooked.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          RecognizedRecord current2 = graph.RecognizedRecords.Current;
          RecognizedRecordProcess.DeleteRecognizedRecord((RecognizedRecordForProcessing) PXSelectBase<RecognizedRecordForProcessing, PXViewOf<RecognizedRecordForProcessing>.BasedOn<SelectFromBase<RecognizedRecordForProcessing, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<RecognizedRecord.refNbr, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.Select((PXGraph) graph, (object) current2.RefNbr));
        }
      }
      return base.Handler(adapter);
    }
  }

  private enum ReloadData
  {
    All,
    VendorId,
  }
}
