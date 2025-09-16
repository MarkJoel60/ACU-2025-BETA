// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.RecognizedRecordDetailsManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Newtonsoft.Json;
using PX.CloudServices.DAC;
using PX.CloudServices.DocumentRecognition;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP.InvoiceRecognition.DAC;
using PX.Objects.AP.InvoiceRecognition.Feedback;
using PX.Objects.AP.InvoiceRecognition.VendorSearch;
using PX.Objects.CR;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition;

internal class RecognizedRecordDetailsManager
{
  private const string FEEDBACK_VENDOR_SEARCH = "feedback:entity-resolution";
  private const string VENDOR_NAME_ENTITY = "name";
  private static readonly Dictionary<string, string> _fieldsToPopulate = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
  {
    {
      "CuryOrigDocAmt",
      "Amount"
    },
    {
      "DocDate",
      "Date"
    },
    {
      "DueDate",
      "DueDate"
    },
    {
      "InvoiceNbr",
      "VendorRef"
    }
  };
  private readonly PXGraph _graph;
  private readonly IVendorSearchService _vendorSearchService;
  private readonly IInvoiceRecognitionService _invoiceRecognitionClient;

  public RecognizedRecordDetailsManager(
    PXGraph graph,
    IVendorSearchService vendorSearchService,
    IInvoiceRecognitionService invoiceRecognitionClient)
  {
    ExceptionExtensions.ThrowOnNull<PXGraph>(graph, nameof (graph), (string) null);
    ExceptionExtensions.ThrowOnNull<IVendorSearchService>(vendorSearchService, nameof (vendorSearchService), (string) null);
    ExceptionExtensions.ThrowOnNull<IInvoiceRecognitionService>(invoiceRecognitionClient, nameof (invoiceRecognitionClient), (string) null);
    this._graph = graph;
    this._vendorSearchService = vendorSearchService;
    this._invoiceRecognitionClient = invoiceRecognitionClient;
  }

  public async Task FillRecognizedFields(
    RecognizedRecord record,
    DocumentRecognitionResult recognitionResult)
  {
    ExceptionExtensions.ThrowOnNull<RecognizedRecord>(record, nameof (record), (string) null);
    RecognizedRecordDetail recognizedRecordDetail1 = new RecognizedRecordDetail()
    {
      EntityType = record.EntityType,
      RefNbr = record.RefNbr
    };
    PXCache cach = this._graph.Caches[typeof (RecognizedRecordDetail)];
    RecognizedRecordDetail recognizedRecordDetail2 = cach.Insert((object) recognizedRecordDetail1) as RecognizedRecordDetail;
    Dictionary<string, Field> fields = recognitionResult?.Documents?[0]?.Fields;
    if (fields != null)
    {
      foreach (KeyValuePair<string, Field> keyValuePair in fields)
      {
        if (!string.IsNullOrWhiteSpace(keyValuePair.Key))
        {
          (string ViewName, string str) = InvoiceDataLoader.GetFieldInfo(keyValuePair.Key);
          string fieldName;
          if (!string.IsNullOrWhiteSpace(ViewName) && !string.IsNullOrWhiteSpace(str) && ViewName.Equals("Document", StringComparison.OrdinalIgnoreCase) && RecognizedRecordDetailsManager._fieldsToPopulate.TryGetValue(str, out fieldName))
            InvoiceDataLoader.SetFieldExtValue(cach, (object) recognizedRecordDetail2, fieldName, keyValuePair.Value);
        }
      }
    }
    VendorSearchFeedback feedback = this.FillVendorId(recognizedRecordDetail2, fields, record.MailFrom);
    recognizedRecordDetail2.DefaultBranchID = this.GetDefaultBranchIDByEmailAccountPreferences(record);
    RecognizedRecordDetail row = cach.Update((object) recognizedRecordDetail2) as RecognizedRecordDetail;
    cach.PersistInserted((object) row);
    await this.SendVendorSearchFeedback(feedback, recognitionResult?.Links);
  }

  protected virtual int? GetDefaultBranchIDByEmailAccountPreferences(RecognizedRecord record)
  {
    int? accountPreferences = new int?();
    EMailAccount topFirst = PXSelectBase<EMailAccount, PXViewOf<EMailAccount>.BasedOn<SelectFromBase<EMailAccount, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SMEmail>.On<BqlOperand<SMEmail.mailAccountID, IBqlInt>.IsEqual<EMailAccount.emailAccountID>>>, FbqlJoins.Inner<RecognizedRecord>.On<BqlOperand<RecognizedRecord.messageID, IBqlString>.IsEqual<SMEmail.messageId>>>>.Where<BqlOperand<RecognizedRecord.refNbr, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.Select(this._graph, (object) record.RefNbr).TopFirst;
    if (topFirst != null)
      accountPreferences = this._graph.Caches[typeof (EMailAccount)].GetExtension<PX.Objects.AP.InvoiceRecognition.DAC.EMailAccountExt>((object) topFirst).DefaultBranchID;
    return accountPreferences;
  }

  private VendorSearchFeedback FillVendorId(
    RecognizedRecordDetail detail,
    Dictionary<string, Field> fields,
    string mailFrom)
  {
    List<FullTextTerm> fullTextTerms = (List<FullTextTerm>) null;
    string vendorName = (string) null;
    if (fields != null)
    {
      string key = "Document.VendorID";
      Field field1;
      if (fields.TryGetValue(key, out field1))
      {
        fullTextTerms = field1?.FullTextTerms;
        Field field2;
        if (field1.Entity != null && field1.Entity.TryGetValue("name", out field2))
        {
          string str = field2.Value as string;
          if (!string.IsNullOrEmpty(str))
            vendorName = str;
        }
      }
    }
    string email = RecognizedRecordDetailsManager.ExtractEmail(mailFrom);
    VendorSearchResult vendor = this._vendorSearchService.FindVendor(this._graph, vendorName, (IList<FullTextTerm>) fullTextTerms, email);
    detail.VendorID = vendor.VendorId;
    detail.VendorTermIndex = vendor.TermIndex;
    detail.VendorName = vendorName;
    return vendor.Feedback;
  }

  private static string ExtractEmail(string mailFrom)
  {
    if (string.IsNullOrEmpty(mailFrom))
      return (string) null;
    int num1 = mailFrom.IndexOf('<');
    int num2 = mailFrom.IndexOf('>');
    if (num1 == -1 || num2 == -1)
      return mailFrom;
    int length = num2 - num1 - 1;
    return mailFrom.Substring(num1 + 1, length);
  }

  private async Task SendVendorSearchFeedback(
    VendorSearchFeedback feedback,
    Dictionary<string, Uri> links)
  {
    if (!(this._invoiceRecognitionClient is IInvoiceRecognitionFeedback recognitionClient) || feedback == null)
      return;
    if (links == null)
      return;
    try
    {
      await RecognizedRecordDetailsManager.SendVendorSearchFeedbackAsync(links, recognitionClient, feedback);
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex);
    }
  }

  private static async Task SendVendorSearchFeedbackAsync(
    Dictionary<string, Uri> links,
    IInvoiceRecognitionFeedback feedbackService,
    VendorSearchFeedback feedback)
  {
    Uri address;
    if (!links.TryGetValue("feedback:entity-resolution", out address))
    {
      PXTrace.WriteError("IInvoiceRecognitionFeedback: Unable to send feedback - link is not found:{LinkKey}", (object) "feedback:entity-resolution");
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

  public async Task FillVendorId(RecognizedRecord record, RecognizedRecordDetail detail)
  {
    ExceptionExtensions.ThrowOnNull<RecognizedRecord>(record, nameof (record), (string) null);
    PXCache cach = this._graph.Caches[typeof (RecognizedRecordDetail)];
    DocumentRecognitionResult recognitionResult = string.IsNullOrWhiteSpace(record.RecognitionResult) ? (DocumentRecognitionResult) null : JsonConvert.DeserializeObject<DocumentRecognitionResult>(record.RecognitionResult);
    RecognizedRecordDetail recognizedRecordDetail = detail;
    int num = recognizedRecordDetail != null ? (!recognizedRecordDetail.RefNbr.HasValue ? 1 : 0) : 1;
    if (num != 0)
      detail = new RecognizedRecordDetail()
      {
        EntityType = record.EntityType,
        RefNbr = record.RefNbr
      };
    Dictionary<string, Field> fields = recognitionResult?.Documents?[0]?.Fields;
    VendorSearchFeedback feedback = this.FillVendorId(detail, fields, record.MailFrom);
    if (num != 0)
    {
      detail = cach.Insert((object) detail) as RecognizedRecordDetail;
      cach.PersistInserted((object) detail);
    }
    else
    {
      detail = cach.Update((object) detail) as RecognizedRecordDetail;
      cach.PersistUpdated((object) detail);
    }
    await this.SendVendorSearchFeedback(feedback, recognitionResult?.Links);
  }
}
