// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.Feedback.DocumentFeedbackBuilder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Newtonsoft.Json;
using PX.CloudServices.DocumentRecognition;
using PX.Common;
using PX.Data;
using PX.Data.Search;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition.Feedback;

internal class DocumentFeedbackBuilder
{
  private const char ViewFieldNameSeparator = '.';
  private const string DateStringFormat = "yyyy-MM-dd";
  private const string _primaryViewName = "Document";
  private readonly string _vendorInvoiceFieldName = DocumentFeedbackBuilder.GetDocumentFieldName("Document", "VendorID");
  private static readonly string[] _primaryReadonlyFieldsToBypass = new string[2]
  {
    "CuryViewState",
    "CuryLineTotal"
  };
  private readonly HashSet<string> _primaryFields;
  private readonly HashSet<string> _detailFields;
  private static readonly HashSet<System.Type> _numericFieldTypes = new HashSet<System.Type>()
  {
    typeof (byte),
    typeof (short),
    typeof (int),
    typeof (long),
    typeof (Decimal),
    typeof (float),
    typeof (double)
  };
  private readonly List<TableContext> _tableContextDump = new List<TableContext>();
  private TableContext _currentTableContext;

  public DocumentFeedbackBuilder(
    PXCache recognizedInvoiceCache,
    HashSet<string> primaryFields,
    HashSet<string> detailFields)
  {
    ExceptionExtensions.ThrowOnNull<PXCache>(recognizedInvoiceCache, nameof (recognizedInvoiceCache), (string) null);
    ExceptionExtensions.ThrowOnNull<HashSet<string>>(primaryFields, nameof (primaryFields), (string) null);
    ExceptionExtensions.ThrowOnNull<HashSet<string>>(detailFields, nameof (detailFields), (string) null);
    this._primaryFields = primaryFields;
    this._detailFields = detailFields;
    HashSet<string> other = new HashSet<string>((IEnumerable<string>) recognizedInvoiceCache.Fields, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    PXCache cach = PXGraph.CreateInstance<PXGraph>().Caches[typeof (APInvoice)];
    other.ExceptWith((IEnumerable<string>) cach.Fields);
    other.UnionWith((IEnumerable<string>) DocumentFeedbackBuilder._primaryReadonlyFieldsToBypass);
    this._primaryFields.ExceptWith((IEnumerable<string>) other);
  }

  private static string GetDocumentFieldName(string viewName, string fieldName)
  {
    return $"{viewName}{(ValueType) '.'}{fieldName}";
  }

  public void ProcessCellBound(string cellBoundJson)
  {
    if (string.IsNullOrEmpty(cellBoundJson))
      return;
    CellBound cellBound;
    try
    {
      cellBound = JsonConvert.DeserializeObject<CellBound>(cellBoundJson);
    }
    catch (JsonSerializationException ex)
    {
      PXTrace.WriteError("Malformed cell bound json:\n{error}\n{cellBoundJson}", (object) ((Exception) ex).Message, (object) cellBoundJson);
      return;
    }
    if (string.IsNullOrWhiteSpace(cellBound.DetailColumn))
    {
      PXTrace.WriteError("Inconsistent cell bound json:\n{cellBoundJson}", (object) cellBoundJson);
    }
    else
    {
      if (this._currentTableContext != null && !this._currentTableContext.CanBeBounded(cellBound))
      {
        this._tableContextDump.Add(this._currentTableContext);
        this._currentTableContext = (TableContext) null;
      }
      this.RegisterCellBound(cellBound);
    }
  }

  public void DumpTableFeedback()
  {
    if (this._currentTableContext == null)
      return;
    this._tableContextDump.Add(this._currentTableContext);
    this._currentTableContext = (TableContext) null;
  }

  private void RegisterCellBound(CellBound cellBound)
  {
    if (this._currentTableContext == null)
      this._currentTableContext = new TableContext(cellBound.Page, cellBound.Table);
    if (cellBound.Columns.Count == 1 && cellBound.Columns[0] == (short) -1)
    {
      this._currentTableContext.RegisterColumnUnbound(cellBound.DetailColumn);
    }
    else
    {
      this._currentTableContext.RegisterColumnSelected(cellBound.DetailColumn, cellBound.Columns);
      this._currentTableContext.RegisterRowBound(cellBound.DetailRow, cellBound.Row);
    }
  }

  public List<DocumentFeedback> ToTableFeedbackList(string detailViewName)
  {
    List<DocumentFeedback> tableFeedbackList = new List<DocumentFeedback>();
    this.DumpTableFeedback();
    foreach (TableContext tableContext1 in this._tableContextDump)
    {
      TableContext tableContext = tableContext1;
      tableFeedbackList.Add(this.GetTableSelectedFeedback(detailViewName, tableContext.Page, tableContext.Table));
      IEnumerable<DocumentFeedback> collection = tableContext.ColumnSelected.Select(columnSelected => new
      {
        Value = this.GetColumnBoundFeedback(detailViewName, tableContext.Page, tableContext.Table, columnSelected.DetailColumn, columnSelected.Columns),
        Created = columnSelected.Created
      }).Concat(tableContext.ColumnUnbound.Select(columnUnbound => new
      {
        Value = this.GetColumnUnboundFeedback(detailViewName, columnUnbound.DetailColumn),
        Created = columnUnbound.Created
      })).OrderBy(feedback => feedback.Created).Select(feedback => feedback.Value);
      tableFeedbackList.AddRange(collection);
      List<KeyValuePair<short, short>> list = tableContext.RowByDetailRow.ToList<KeyValuePair<short, short>>();
      if (list.Count != 0)
        tableFeedbackList.Add(this.GetRowsBoundFeedback(detailViewName, tableContext.Page, tableContext.Table, (IEnumerable<KeyValuePair<short, short>>) list));
    }
    this._tableContextDump.Clear();
    return tableFeedbackList;
  }

  private DocumentFeedback GetRowsBoundFeedback(
    string detailViewName,
    short page,
    short table,
    IEnumerable<KeyValuePair<short, short>> rowByDetailRow)
  {
    Detail detail = new Detail()
    {
      Value = new List<DetailValue>()
    };
    int num1 = -1;
    foreach (KeyValuePair<short, short> keyValuePair in rowByDetailRow)
    {
      int num2 = (int) keyValuePair.Key - num1;
      if (num2 > 1)
      {
        for (int index = 0; index < num2 - 1; ++index)
          detail.Value.Add((DetailValue) null);
      }
      num1 = (int) keyValuePair.Key;
      DetailValue detailValue = new DetailValue()
      {
        BoundingBoxes = new List<BoundingBoxDetail>()
        {
          new BoundingBoxDetail()
          {
            Page = new short?(page),
            Table = new short?(table),
            Row = new short?(keyValuePair.Value)
          }
        }
      };
      detail.Value.Add(detailValue);
    }
    return this.CreateTableVersionedFeedback(detailViewName, detail);
  }

  private DocumentFeedback GetTableSelectedFeedback(string detailViewName, short page, short table)
  {
    Detail detail = new Detail()
    {
      Ocr = new List<DetailOcr>()
      {
        new DetailOcr()
        {
          Page = new short?(page),
          Table = new short?(table)
        }
      }
    };
    return this.CreateTableVersionedFeedback(detailViewName, detail);
  }

  private DocumentFeedback GetColumnBoundFeedback(
    string detailViewName,
    short page,
    short table,
    string columnName,
    List<short> columnIndexes)
  {
    Detail detail = new Detail()
    {
      Ocr = new List<DetailOcr>()
      {
        new DetailOcr()
        {
          Page = new short?(page),
          Table = new short?(table),
          FieldColumns = new Dictionary<string, List<short>>()
          {
            [columnName] = columnIndexes
          }
        }
      }
    };
    return this.CreateTableVersionedFeedback(detailViewName, detail);
  }

  private DocumentFeedback GetColumnUnboundFeedback(string detailViewName, string columnName)
  {
    Detail detail = new Detail()
    {
      Ocr = new List<DetailOcr>()
      {
        new DetailOcr()
        {
          FieldColumns = new Dictionary<string, List<short>>()
          {
            [columnName] = new List<short>()
          }
        }
      }
    };
    return this.CreateTableVersionedFeedback(detailViewName, detail);
  }

  private DocumentFeedback CreateTableVersionedFeedback(string detailViewName, Detail detail)
  {
    DocumentFeedback versionedFeedback = new DocumentFeedback();
    versionedFeedback.Documents = new List<Document>();
    Document document = new Document()
    {
      Details = new Dictionary<string, Detail>()
    };
    versionedFeedback.Documents.Add(document);
    document.Details.Add(detailViewName, detail);
    return versionedFeedback;
  }

  public DocumentFeedback ToFieldBoundFeedback(string documentJson)
  {
    if (string.IsNullOrWhiteSpace(documentJson))
      return (DocumentFeedback) null;
    Dictionary<string, Field> source;
    try
    {
      source = JsonConvert.DeserializeObject<Dictionary<string, Field>>(documentJson);
    }
    catch (JsonSerializationException ex)
    {
      PXTrace.WriteError("Malformed document json:\n{error}\n{documentJson}", (object) ((Exception) ex).Message, (object) documentJson);
      return (DocumentFeedback) null;
    }
    (string ViewName, string FieldName) = InvoiceDataLoader.GetFieldInfo(source.First<KeyValuePair<string, Field>>().Key);
    if (ViewName == null || FieldName == null)
      return (DocumentFeedback) null;
    if ((!ViewName.Equals("Document", StringComparison.OrdinalIgnoreCase) ? 1 : (!this._primaryFields.Contains(FieldName) ? 1 : 0)) != 0)
      return (DocumentFeedback) null;
    DocumentFeedback fieldBoundFeedback = new DocumentFeedback();
    fieldBoundFeedback.Documents = new List<Document>((IEnumerable<Document>) new Document[1]
    {
      new Document() { Fields = source }
    });
    return fieldBoundFeedback;
  }

  public DocumentFeedback ToRecordSavedFeedback(
    PXView primaryView,
    APInvoice primaryRow,
    PXView detailView,
    IEnumerable<APTran> detailRows,
    IEntitySearchService entitySearchService)
  {
    ExceptionExtensions.ThrowOnNull<APInvoice>(primaryRow, nameof (primaryRow), (string) null);
    ExceptionExtensions.ThrowOnNull<IEnumerable<APTran>>(detailRows, nameof (detailRows), (string) null);
    ExceptionExtensions.ThrowOnNull<IEntitySearchService>(entitySearchService, nameof (entitySearchService), (string) null);
    DocumentFeedback recordSavedFeedback = new DocumentFeedback();
    recordSavedFeedback.Documents = new List<Document>();
    Document document = new Document();
    recordSavedFeedback.Documents.Add(document);
    this.FillDocumentFields(document, primaryView, primaryRow, entitySearchService);
    this.FillDocumentDetails(document, detailView, detailRows);
    return recordSavedFeedback;
  }

  private void FillDocumentFields(
    Document document,
    PXView view,
    APInvoice row,
    IEntitySearchService entitySearchService)
  {
    List<string> list = view.Cache.Fields.Where<string>((Func<string, bool>) (f => this._primaryFields.Contains(f))).ToList<string>();
    if (list.Count == 0)
      return;
    document.Fields = new Dictionary<string, Field>();
    this.FillRowTrackedFields(view, (object) row, (IEnumerable<string>) list, document.Fields, entitySearchService);
  }

  private void FillRowTrackedFields(
    PXView view,
    object row,
    IEnumerable<string> trackedFields,
    Dictionary<string, Field> fieldContainer,
    IEntitySearchService entitySearchService)
  {
    foreach (string trackedField in trackedFields)
    {
      string documentFieldName = DocumentFeedbackBuilder.GetDocumentFieldName(view.Name, trackedField);
      TypedField field = new TypedField();
      fieldContainer[documentFieldName] = (Field) field;
      bool isVendorInvoiceField = documentFieldName.Equals(this._vendorInvoiceFieldName, StringComparison.OrdinalIgnoreCase);
      (FieldTypes? nullable, object Value) = this.GetFieldInfo(view.Cache, trackedField, row, isVendorInvoiceField);
      FullTextTerm vendorFullTextTerm = isVendorInvoiceField ? this.GetVendorFullTextTerm(view.Cache, row, entitySearchService) : (FullTextTerm) null;
      this.FillDocumentField(field, Value, nullable, isVendorInvoiceField, vendorFullTextTerm);
    }
  }

  private (FieldTypes? Type, object Value) GetFieldInfo(
    PXCache cache,
    string fieldName,
    object row,
    bool isVendorInvoiceField)
  {
    object obj = PXFieldState.UnwrapValue(cache.GetValueExt(row, fieldName));
    if (isVendorInvoiceField)
      return (new FieldTypes?(FieldTypes.Entity), cache.GetValue(row, fieldName));
    PXFieldState stateExt = cache.GetStateExt(row, fieldName) as PXFieldState;
    if (stateExt.DataType == typeof (string))
      return (new FieldTypes?(FieldTypes.String), (object) ((string) obj)?.TrimEnd());
    if (DocumentFeedbackBuilder._numericFieldTypes.Contains(stateExt.DataType))
      return (new FieldTypes?(FieldTypes.Number), obj);
    if (!(stateExt.DataType == typeof (System.DateTime)))
      return (new FieldTypes?(), obj);
    IEnumerable<PXEventSubscriberAttribute> attributesReadonly = cache.GetAttributesReadonly(row, fieldName);
    if (attributesReadonly.OfType<PXDateAttribute>().Any<PXDateAttribute>())
      return (new FieldTypes?(FieldTypes.DateTime), obj);
    PXDBDateAttribute pxdbDateAttribute = attributesReadonly.OfType<PXDBDateAttribute>().FirstOrDefault<PXDBDateAttribute>();
    if (pxdbDateAttribute == null || pxdbDateAttribute is PXDBTimeAttribute)
      return (new FieldTypes?(), obj);
    if (pxdbDateAttribute.PreserveTime)
      return (new FieldTypes?(FieldTypes.DateTime), obj);
    System.DateTime? nullable = obj as System.DateTime?;
    if (nullable.HasValue)
      obj = (object) nullable.Value.ToString("yyyy-MM-dd");
    return (new FieldTypes?(FieldTypes.Date), obj);
  }

  private void FillDocumentField(
    TypedField field,
    object value,
    FieldTypes? type,
    bool isVendorInvoiceField,
    FullTextTerm fullTextTerm)
  {
    if (isVendorInvoiceField)
    {
      if (value is int num)
        field.EntityId = num.ToString();
      if (fullTextTerm != null)
        field.FullTextTerms = new List<FullTextTerm>((IEnumerable<FullTextTerm>) new FullTextTerm[1]
        {
          fullTextTerm
        });
    }
    else
      field.Value = value;
    field.Type = type;
  }

  private FullTextTerm GetVendorFullTextTerm(
    PXCache cache,
    object row,
    IEntitySearchService entitySearchService)
  {
    if (entitySearchService == null)
      return (FullTextTerm) null;
    int? bAccountID = cache.GetValue<APInvoice.vendorID>(row) as int?;
    if (!bAccountID.HasValue)
      return (FullTextTerm) null;
    Vendor vendor = Vendor.PK.Find(cache.Graph, bAccountID);
    if (vendor == null)
      return (FullTextTerm) null;
    Guid? noteId = vendor.NoteID;
    if (!noteId.HasValue)
      return (FullTextTerm) null;
    string searchIndexContent = entitySearchService.GetSearchIndexContent(noteId.Value);
    if (searchIndexContent == null)
      return (FullTextTerm) null;
    return new FullTextTerm() { Text = searchIndexContent };
  }

  private void FillDocumentDetails(Document document, PXView view, IEnumerable<APTran> rows)
  {
    List<string> list = view.Cache.Fields.Where<string>((Func<string, bool>) (f => this._detailFields.Contains(f))).ToList<string>();
    if (list.Count == 0)
      return;
    document.Details = new Dictionary<string, Detail>();
    Detail detail = new Detail()
    {
      Value = new List<DetailValue>()
    };
    document.Details[view.Name] = detail;
    foreach (APTran row in rows)
    {
      DetailValue detailValue = new DetailValue()
      {
        Fields = new Dictionary<string, Field>()
      };
      detail.Value.Add(detailValue);
      this.FillRowTrackedFields(view, (object) row, (IEnumerable<string>) list, detailValue.Fields, (IEntitySearchService) null);
    }
  }
}
