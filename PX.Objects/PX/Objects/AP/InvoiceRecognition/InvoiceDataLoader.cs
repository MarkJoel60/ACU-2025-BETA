// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.InvoiceDataLoader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CloudServices.DocumentRecognition;
using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition;

internal class InvoiceDataLoader
{
  private const string DateTimeStringFormat = "yyyy-MM-dd";
  private const char ViewPlusFieldNameSeparator = '.';
  private readonly Document _recognizedDocument;
  private readonly APInvoiceRecognitionEntry _graph;
  private readonly string[] _detailOrderedFields;
  private readonly int? _vendorId;
  private readonly bool _loadVendorIdOnly;
  private static readonly char[] _specCharsToReplace = new char[3]
  {
    '\n',
    '\r',
    '\t'
  };

  public InvoiceDataLoader(
    DocumentRecognitionResult recognitionResult,
    APInvoiceRecognitionEntry graph,
    string[] detailOrderedFields,
    int? vendorId,
    bool loadVendorIdOnly)
  {
    ExceptionExtensions.ThrowOnNull<DocumentRecognitionResult>(recognitionResult, nameof (recognitionResult), (string) null);
    ExceptionExtensions.ThrowOnNull<APInvoiceRecognitionEntry>(graph, nameof (graph), (string) null);
    ExceptionExtensions.ThrowOnNull<string[]>(detailOrderedFields, nameof (detailOrderedFields), (string) null);
    Document document;
    if (recognitionResult != null)
    {
      int? count = recognitionResult.Documents?.Count;
      int num = 0;
      if (count.GetValueOrDefault() > num & count.HasValue)
      {
        document = recognitionResult.Documents[0];
        goto label_4;
      }
    }
    document = (Document) null;
label_4:
    this._recognizedDocument = document;
    this._graph = graph;
    this._detailOrderedFields = detailOrderedFields;
    this._vendorId = vendorId;
    this._loadVendorIdOnly = loadVendorIdOnly;
  }

  public void Load(object primaryRow)
  {
    ExceptionExtensions.ThrowOnNull<object>(primaryRow, nameof (primaryRow), (string) null);
    this._graph.PreserveErrorInfo = true;
    this.LoadPrimary(primaryRow);
    this.LoadDetails();
  }

  private void LoadPrimary(object row)
  {
    ExceptionExtensions.ThrowOnNull<object>(row, nameof (row), (string) null);
    if (this._vendorId.HasValue)
      InvoiceDataLoader.SetExtValue(this._graph.Document.Cache, row, "VendorID", (object) this._vendorId, true);
    if (this._loadVendorIdOnly || this._recognizedDocument?.Fields == null)
      return;
    foreach (KeyValuePair<string, Field> field in this._recognizedDocument.Fields)
    {
      if (!string.IsNullOrWhiteSpace(field.Key))
      {
        (string str1, string str2) = InvoiceDataLoader.GetFieldInfo(field.Key);
        if (!string.IsNullOrWhiteSpace(str1) && !string.IsNullOrWhiteSpace(str2))
          this.LoadPrimaryField(this._graph.Views[str1].Cache, row, str2, field.Value);
      }
    }
  }

  internal static (string ViewName, string FieldName) GetFieldInfo(string viewNameFieldName)
  {
    string[] strArray = viewNameFieldName.Split('.');
    return strArray.Length != 2 ? ((string) null, (string) null) : (strArray[0], strArray[1]);
  }

  private void LoadPrimaryField(PXCache cache, object row, string fieldName, Field field)
  {
    InvoiceDataLoader.SetFieldExtValue(cache, row, fieldName, field);
  }

  private void LoadDetails()
  {
    if (this._recognizedDocument?.Details == null)
      return;
    foreach (KeyValuePair<string, Detail> detail in this._recognizedDocument.Details)
    {
      List<DetailValue> rows = detail.Value?.Value;
      if (rows != null)
        this.LoadDetailsRows((IList<DetailValue>) rows);
    }
  }

  private void LoadDetailsRows(IList<DetailValue> rows)
  {
    foreach (DetailValue row in (IEnumerable<DetailValue>) rows)
    {
      Dictionary<string, Field> fields = row.Fields;
      if (fields != null)
      {
        List<KeyValuePair<string, Field>> list = fields.ToList<KeyValuePair<string, Field>>();
        list.Sort(new Comparison<KeyValuePair<string, Field>>(this.CompareDetailFields));
        this.LoadDetailsRow(list);
      }
    }
  }

  private int CompareDetailFields(KeyValuePair<string, Field> x, KeyValuePair<string, Field> y)
  {
    string fieldName1 = InvoiceDataLoader.GetFieldInfo(x.Key).FieldName;
    string fieldName2 = InvoiceDataLoader.GetFieldInfo(y.Key).FieldName;
    return Array.IndexOf<string>(this._detailOrderedFields, fieldName1).CompareTo(Array.IndexOf<string>(this._detailOrderedFields, fieldName2));
  }

  private void LoadDetailsRow(List<KeyValuePair<string, Field>> fields)
  {
    object obj = (object) null;
    PXCache cache = (PXCache) null;
    foreach (KeyValuePair<string, Field> field in fields)
    {
      if (!string.IsNullOrWhiteSpace(field.Key))
      {
        (string str1, string str2) = InvoiceDataLoader.GetFieldInfo(field.Key);
        if (!string.IsNullOrWhiteSpace(str1) && !string.IsNullOrWhiteSpace(str2))
        {
          if (cache == null)
            cache = this._graph.Views[str1].Cache;
          if (obj == null)
            obj = cache.Insert();
          this.LoadDetailsField(cache, obj, str2, field.Value);
        }
      }
    }
    if (cache == null)
      return;
    if (obj == null)
      return;
    try
    {
      cache.Update(obj);
    }
    catch (PXFieldValueProcessingException ex)
    {
      object newValue = ex.ErrorValue ?? cache.GetValueExt(obj, ex.FieldName);
      cache.RaiseExceptionHandling(ex.FieldName, obj, newValue, (Exception) ex);
    }
  }

  private void LoadDetailsField(PXCache cache, object row, string fieldName, Field field)
  {
    InvoiceDataLoader.SetFieldExtValue(cache, row, fieldName, field);
  }

  internal static void SetFieldExtValue(PXCache cache, object row, string fieldName, Field field)
  {
    if (field == null || !(cache.GetStateExt((object) null, fieldName) is PXFieldState stateExt) || stateExt.DataType == (System.Type) null)
      return;
    System.Type dataType = stateExt.DataType;
    object obj = InvoiceDataLoader.ParseExtValue(dataType, field.Value, field.Ocr?.Text);
    if (obj == null)
      return;
    System.Type type = obj.GetType();
    if (!dataType.IsAssignableFrom(type))
    {
      System.DateTime result;
      if (type == typeof (string) && dataType == typeof (System.DateTime) && System.DateTime.TryParseExact(obj as string, "yyyy-MM-dd", (IFormatProvider) null, DateTimeStyles.None, out result))
      {
        obj = (object) result;
      }
      else
      {
        try
        {
          obj = Convert.ChangeType(obj, dataType);
        }
        catch
        {
          return;
        }
      }
    }
    InvoiceDataLoader.SetExtValue(cache, row, fieldName, obj);
  }

  private static void SetExtValue(
    PXCache cache,
    object row,
    string fieldName,
    object extValue,
    bool showExtErrorValue = false)
  {
    try
    {
      cache.SetValueExt(row, fieldName, extValue);
    }
    catch (PXSetPropertyException ex)
    {
      showExtErrorValue &= ex.ErrorValue == null;
      object returnValue = ex.ErrorValue ?? extValue;
      if (showExtErrorValue)
      {
        cache.RaiseFieldSelecting(fieldName, row, ref returnValue, false);
        returnValue = PXFieldState.UnwrapValue(returnValue);
      }
      cache.RaiseExceptionHandling(fieldName, row, returnValue, (Exception) ex);
    }
  }

  private static object ParseExtValue(System.Type fieldType, object fieldValue, string fieldTextValue)
  {
    if (fieldValue != null)
      return fieldValue is string str ? (object) MainTools.Replace(str, InvoiceDataLoader._specCharsToReplace, ' ') : fieldValue;
    if (string.IsNullOrEmpty(fieldTextValue))
      return (object) null;
    if (fieldType == typeof (string))
      MainTools.Replace(fieldTextValue, InvoiceDataLoader._specCharsToReplace, ' ');
    int result1;
    if (fieldType == typeof (int?) && int.TryParse(fieldTextValue, out result1))
      return (object) result1;
    Decimal result2;
    if (fieldType == typeof (Decimal?) && Decimal.TryParse(fieldTextValue, out result2))
      return (object) result2;
    System.DateTime result3;
    return fieldType == typeof (System.DateTime?) && System.DateTime.TryParseExact(fieldTextValue, "yyyy-MM-dd", (IFormatProvider) null, DateTimeStyles.None, out result3) ? (object) result3 : (object) null;
  }
}
