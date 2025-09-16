// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.OUSearchMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Outlook.Services;
using PX.Async;
using PX.CloudServices.DAC;
using PX.Common;
using PX.Common.Extensions;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP.InvoiceRecognition;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.CR.Threading;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.SM;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reactive.Disposables;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

#nullable enable
namespace PX.Objects.CR;

public class OUSearchMaint : PXGraph<
#nullable disable
OUSearchMaint>
{
  private const string BillsAndAdjustmentsScreenId = "AP301000";
  public PXSelect<BAccount> _baccount;
  public PXSelect<PX.Objects.AR.Customer> _customer;
  public PXSave<OUSearchEntity> Save;
  public PXCancel<OUSearchEntity> Cancel;
  public PXFilter<OUMessage> SourceMessage;
  public PXFilter<OUSearchEntity> Filter;
  public PXFilter<OUCase> NewCase;
  public PXFilter<OUOpportunity> NewOpportunity;
  public PXFilter<OUActivity> NewActivity;
  public PXSelectOrderBy<OUAPBillAttachment, OrderBy<Asc<OUAPBillAttachment.name>>> APBillAttachments;
  public PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.eMail, Equal<Current<OUSearchEntity.outgoingEmail>>, And<PX.Objects.CR.Contact.isActive, Equal<True>, And<PX.Objects.CR.Contact.contactType, NotEqual<ContactTypesAttribute.bAccountProperty>>>>, OrderBy<Asc<PX.Objects.CR.Contact.contactPriority, Desc<PX.Objects.CR.Contact.bAccountID, Asc<PX.Objects.CR.Contact.contactID>>>>> DefaultContact;
  public PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Current<OUSearchEntity.contactID>>>> Contact;
  public PXSelect<CRSMEmail, Where<CRSMEmail.messageId, Equal<Current<OUMessage.messageId>>>> Message;
  public PXSetup<CRSetup> setup;
  public PXSelect<CRCase> _case;
  public PXSelect<CROpportunity> _opportunity;
  [PXHidden]
  public PXSetup<CRSetup> Setup;
  [PXHidden]
  public PXSetup<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Optional<PX.Objects.CR.Contact.bAccountID>>>> customer;
  public PXAction<OUSearchEntity> LogOut;
  public PXAction<OUSearchEntity> CreateAPDoc;
  public PXAction<OUSearchEntity> CreateAPDocContinue;
  public PXAction<OUSearchEntity> ViewAPDoc;
  public PXAction<OUSearchEntity> ViewAPDocContinue;
  public PXAction<OUSearchEntity> ViewContact;
  public PXAction<OUSearchEntity> ViewBAccount;
  public PXAction<OUSearchEntity> ViewEntity;
  public PXAction<OUSearchEntity> GoCreateLead;
  public PXAction<OUSearchEntity> CreateLead;
  public PXAction<OUSearchEntity> GoCreateContact;
  public PXAction<OUSearchEntity> CreateContact;
  public PXAction<OUSearchEntity> GoCreateCase;
  public PXAction<OUSearchEntity> CreateCase;
  public PXAction<OUSearchEntity> GoCreateOpportunity;
  public PXAction<OUSearchEntity> CreateOpportunity;
  public PXAction<OUSearchEntity> GoCreateActivity;
  public PXAction<OUSearchEntity> CreateActivity;
  public PXAction<OUSearchEntity> Back;
  public PXAction<OUSearchEntity> Reply;
  private const string GetAttachmentSoapRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\r\nxmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"\r\nxmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"\r\nxmlns:t=\"http://schemas.microsoft.com/exchange/services/2006/types\">\r\n<soap:Header>\r\n<t:RequestServerVersion Version=\"Exchange2013\" />\r\n</soap:Header>\r\n  <soap:Body>\r\n    <GetAttachment xmlns=\"http://schemas.microsoft.com/exchange/services/2006/messages\"\r\n    xmlns:t=\"http://schemas.microsoft.com/exchange/services/2006/types\">\r\n      <AttachmentShape/>\r\n      <AttachmentIds>\r\n        <t:AttachmentId Id=\"{0}\"/>\r\n      </AttachmentIds>\r\n    </GetAttachment>\r\n  </soap:Body>\r\n</soap:Envelope>";
  private const string GetMessageSoapRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<soap:Envelope\r\n  xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\r\n  xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"\r\n  xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"\r\n  xmlns:t=\"http://schemas.microsoft.com/exchange/services/2006/types\">\r\n<soap:Header>\r\n<t:RequestServerVersion Version=\"Exchange2007_SP1\" />\r\n</soap:Header>\r\n  <soap:Body>\r\n    <GetItem\r\n\t  xmlns = \"http://schemas.microsoft.com/exchange/services/2006/messages\"\r\n\t  xmlns:t=\"http://schemas.microsoft.com/exchange/services/2006/types\">\r\n      <ItemShape>\r\n        <t:BaseShape>Default</t:BaseShape>\r\n        <t:IncludeMimeContent>false</t:IncludeMimeContent>\r\n\t\t<t:BodyType>HTML</t:BodyType>\r\n      </ItemShape>\r\n      <ItemIds>\r\n        <t:ItemId Id = \"{0}\" />\r\n      </ItemIds>\r\n    </GetItem>\r\n  </soap:Body>\r\n</soap:Envelope>";

  [InjectDependency]
  private ILoginUiService _loginUiService { get; set; }

  [InjectDependency]
  private IInvoiceRecognitionService InvoiceRecognitionClient { get; set; }

  [InjectDependency]
  private IOutlookAuthService OutlookAuthService { get; set; }

  [InjectDependency]
  private ILogger Logger { get; set; }

  public virtual IEnumerable apBillAttachments()
  {
    OUMessage outlookMessage = ((PXSelectBase<OUMessage>) this.SourceMessage).Current;
    if ((string.IsNullOrWhiteSpace(((PXSelectBase<OUSearchEntity>) this.Filter).Current.PrevItemId) ? 1 : (string.IsNullOrWhiteSpace(outlookMessage.ItemId) ? 0 : (!outlookMessage.ItemId.Equals(((PXSelectBase<OUSearchEntity>) this.Filter).Current.PrevItemId, StringComparison.Ordinal) ? 1 : 0))) == 0)
    {
      IEnumerable cached = ((PXSelectBase) this.APBillAttachments).Cache.Cached;
      foreach (OUAPBillAttachment ouapBillAttachment in cached)
      {
        Guid? duplicateLink;
        if (!string.IsNullOrEmpty(ouapBillAttachment.RecognitionStatus))
        {
          duplicateLink = ouapBillAttachment.DuplicateLink;
          if (duplicateLink.HasValue)
            continue;
        }
        (Guid? nullable, string Status) = this.GetFileRecognitionInfo(ouapBillAttachment.Name);
        ouapBillAttachment.RecognitionStatus = Status ?? ouapBillAttachment.RecognitionStatus;
        duplicateLink = ouapBillAttachment.DuplicateLink;
        if (!duplicateLink.HasValue)
          ouapBillAttachment.DuplicateLink = this.GetFileDuplicateLink(nullable, ouapBillAttachment.FileHash);
      }
      return cached;
    }
    ((PXSelectBase) this.APBillAttachments).Cache.Clear();
    if (outlookMessage != null && !string.IsNullOrWhiteSpace(outlookMessage.EwsUrl) && !string.IsNullOrWhiteSpace(outlookMessage.Token))
    {
      if (!string.IsNullOrWhiteSpace(outlookMessage.ItemId))
      {
        string token;
        try
        {
          token = this.GetEwsAccessToken();
        }
        catch
        {
          return (IEnumerable) Enumerable.Empty<OUAPBillAttachment>();
        }
        ExchangeMessage failed1 = this.TryDoAndLogIfExchangeReceiveFailed<ExchangeMessage>((Func<ExchangeMessage>) (() => OUSearchMaint.GetExchangeMessage(outlookMessage, token)));
        ((PXSelectBase<OUSearchEntity>) this.Filter).Current.PrevItemId = outlookMessage.ItemId;
        if (failed1 == null || failed1.Attachments == null || failed1.Attachments.Length == 0)
          return (IEnumerable) Enumerable.Empty<OUAPBillAttachment>();
        OUAPBillAttachment[] array = ((IEnumerable<AttachmentDetails>) failed1.Attachments).Where<AttachmentDetails>((Func<AttachmentDetails, bool>) (a =>
        {
          if (a.ContentType == "application/pdf")
            return true;
          return a.ContentType == "application/octet-stream" && APInvoiceRecognitionEntry.IsAllowedFile(a.Name);
        })).Select<AttachmentDetails, OUAPBillAttachment>((Func<AttachmentDetails, OUAPBillAttachment>) (a => new OUAPBillAttachment()
        {
          ItemId = outlookMessage.ItemId,
          Id = a.Id,
          Name = a.Name,
          ContentType = a.ContentType
        })).ToArray<OUAPBillAttachment>();
        foreach (OUAPBillAttachment ouapBillAttachment in array)
        {
          OUAPBillAttachment a = ouapBillAttachment;
          XElement failed2 = this.TryDoAndLogIfExchangeReceiveFailed<XElement>((Func<XElement>) (() => OUSearchMaint.GetAttachmentsFromExchangeServerUsingEWS(outlookMessage.EwsUrl, token, a.Id).FirstOrDefault<XElement>()));
          if (failed2 == null)
          {
            ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ErrorMessage = PXLocalizer.Localize("This email does not contain attached invoices.", typeof (Messages).FullName);
          }
          else
          {
            a.FileData = OUSearchMaint.GetFileData(failed2);
            a.FileHash = APInvoiceRecognitionEntry.ComputeFileHash(a.FileData);
            (Guid? nullable, string Status) = this.GetFileRecognitionInfo(a.Name);
            a.RecognitionStatus = Status;
            a.DuplicateLink = this.GetFileDuplicateLink(nullable, a.FileHash);
          }
          ((PXSelectBase) this.APBillAttachments).Cache.Insert((object) a);
          ((PXSelectBase) this.APBillAttachments).Cache.SetStatus((object) a, (PXEntryStatus) 5);
        }
        ((PXSelectBase) this.APBillAttachments).Cache.IsDirty = false;
        return (IEnumerable) array;
      }
    }
    return (IEnumerable) Enumerable.Empty<OUAPBillAttachment>();
  }

  public OUSearchMaint()
  {
    ((PXSelectBase) this.Contact).Cache.AllowUpdate = false;
    ((PXSelectBase) this.Message).Cache.AllowUpdate = false;
    ((PXAction) this.Save).SetVisible(false);
    ((PXAction) this.Cancel).SetVisible(false);
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable back(PXAdapter adapter)
  {
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ContactType = (string) null;
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.Operation = (string) null;
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ErrorMessage = (string) null;
    if (adapter.ExternalCall)
      ((PXSelectBase<OUSearchEntity>) this.Filter).Current.DuplicateFilesMsg = (string) null;
    this.ClearAttachmentsSelection();
    return adapter.Get();
  }

  private void ClearAttachmentsSelection()
  {
    foreach (OUAPBillAttachment ouapBillAttachment in ((PXSelectBase) this.APBillAttachments).Cache.Cached)
      ouapBillAttachment.Selected = new bool?(false);
  }

  [PXUIField]
  [PXButton]
  protected virtual void viewContact()
  {
    PX.Objects.CR.Contact contact = ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).SelectSingle(Array.Empty<object>());
    try
    {
      if (contact == null)
        return;
      PXRedirectHelper.TryRedirect(((PXSelectBase) this.Contact).Cache, (object) contact, string.Empty, (PXRedirectHelper.WindowMode) 1);
    }
    catch (PXRedirectRequiredException ex)
    {
      this.ExternalRedirect(ex);
    }
  }

  [PXUIField]
  [PXButton]
  protected virtual void viewBAccount()
  {
    PX.Objects.CR.Contact contact = ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).SelectSingle(Array.Empty<object>());
    try
    {
      if (contact == null || !contact.BAccountID.HasValue)
        return;
      BAccount baccount = PXResultset<BAccount>.op_Implicit(PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
      {
        (object) contact.BAccountID
      }));
      if (baccount == null)
        return;
      PXRedirectHelper.TryRedirect(((PXGraph) this).Caches[typeof (BAccount)], (object) baccount, string.Empty, (PXRedirectHelper.WindowMode) 1);
    }
    catch (PXRedirectRequiredException ex)
    {
      this.ExternalRedirect(ex);
    }
  }

  [PXUIField]
  [PXButton]
  protected virtual void viewEntity()
  {
    CRSMEmail crsmEmail = ((PXSelectBase<CRSMEmail>) this.Message).SelectSingle(Array.Empty<object>());
    if (crsmEmail == null)
      return;
    try
    {
      new EntityHelper((PXGraph) this).NavigateToRow(crsmEmail.RefNoteID, (PXRedirectHelper.WindowMode) 1);
    }
    catch (PXRedirectRequiredException ex)
    {
      this.ExternalRedirect(ex);
    }
  }

  [PXUIField]
  [PXButton]
  protected virtual void goCreateLead()
  {
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.Operation = "CreateLead";
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ContactType = "LD";
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.LeadClassID = ((PXSelectBase<CRSetup>) this.setup).Current.DefaultLeadClassID;
    ((PXSelectBase) this.Filter).Cache.SetDefaultExt<OUSearchEntity.leadSource>((object) ((PXSelectBase<OUSearchEntity>) this.Filter).Current);
  }

  [PXUIField]
  [PXButton]
  protected virtual void goCreateContact()
  {
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.Operation = "CreateContact";
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ContactType = "PN";
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ContactClassID = PXAccess.FeatureInstalled<FeaturesSet.customerModule>() ? ((PXSelectBase<CRSetup>) this.setup).Current.DefaultContactClassID : string.Empty;
  }

  [PXUIField]
  [PXButton]
  protected virtual void goCreateCase()
  {
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.Operation = "Case";
    ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current = ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).SelectSingle(Array.Empty<object>());
    ((PXSelectBase) this.NewCase).Cache.SetDefaultExt<OUCase.subject>((object) ((PXSelectBase<OUCase>) this.NewCase).Current);
  }

  [PXUIField]
  [PXButton]
  protected virtual void goCreateOpportunity()
  {
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.Operation = "Opp";
    ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current = ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).SelectSingle(Array.Empty<object>());
    ((PXSelectBase) this.NewOpportunity).Cache.SetDefaultExt<OUOpportunity.subject>((object) ((PXSelectBase<OUOpportunity>) this.NewOpportunity).Current);
    ((PXSelectBase) this.NewOpportunity).Cache.SetDefaultExt<OUOpportunity.classID>((object) ((PXSelectBase<OUOpportunity>) this.NewOpportunity).Current);
  }

  [PXUIField]
  [PXButton]
  protected virtual void goCreateActivity()
  {
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.Operation = "Msg";
    ((PXSelectBase) this.NewActivity).Cache.SetDefaultExt<OUActivity.subject>((object) ((PXSelectBase<OUActivity>) this.NewActivity).Current);
    ((PXSelectBase<OUActivity>) this.NewActivity).Current.IsLinkContact = new bool?(true);
    ((PXSelectBase) this.Contact).View.Clear();
  }

  [PXUIField]
  [PXButton]
  protected virtual void createAPDoc()
  {
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.Operation = "CreateAPDocument";
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.DuplicateFilesMsg = (string) null;
    if (((PXSelectBase<OUSearchEntity>) this.Filter).Current.AttachmentsCount.GetValueOrDefault() != 1)
      return;
    this.CreateAPDocContinue.PressImpl(true, false);
  }

  [PXUIField]
  [PXButton]
  protected virtual void viewAPDoc()
  {
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.Operation = "ViewAPDocument";
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.DuplicateFilesMsg = (string) null;
    if (((PXSelectBase<OUSearchEntity>) this.Filter).Current.AttachmentsCount.GetValueOrDefault() != 1)
      return;
    this.ViewAPDocContinue.PressImpl(true, false);
    this.Back.PressImpl(true, false);
  }

  [PXUIField(DisplayName = "Continue")]
  [PXButton]
  protected virtual IEnumerable createAPDocContinue(PXAdapter adapter)
  {
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ErrorMessage = (string) null;
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.RecognitionIsNotStarted = new bool?(true);
    OUMessage message = ((PXSelectBase<OUMessage>) this.SourceMessage).Current;
    IEnumerable<OUAPBillAttachment> selectedAttachments = this.GetSelectedAttachments();
    try
    {
      int num = 0;
      List<(string, byte[], Guid)> filesToRecognize = new List<(string, byte[], Guid)>();
      foreach (OUAPBillAttachment ouapBillAttachment in selectedAttachments)
      {
        if (ouapBillAttachment.DuplicateLink.HasValue)
        {
          ++num;
          ouapBillAttachment.RecognitionStatus = "R";
          ((PXSelectBase) this.APBillAttachments).Cache.Update((object) ouapBillAttachment);
          ((PXSelectBase) this.APBillAttachments).Cache.SetStatus((object) ouapBillAttachment, (PXEntryStatus) 5);
        }
        else
        {
          filesToRecognize.Add((ouapBillAttachment.Name, ouapBillAttachment.FileData, Guid.NewGuid()));
          ((PXSelectBase<OUSearchEntity>) this.Filter).Current.RecognitionIsNotStarted = new bool?(false);
        }
      }
      if (num == 1)
      {
        ((PXSelectBase<OUSearchEntity>) this.Filter).Current.IsDuplicateDelected = new bool?(true);
        ((PXSelectBase<OUSearchEntity>) this.Filter).Current.DuplicateFilesMsg = PXMessages.LocalizeNoPrefix("Recognition has already been started for another document.");
      }
      else if (num > 1)
      {
        ((PXSelectBase<OUSearchEntity>) this.Filter).Current.IsDuplicateDelected = new bool?(true);
        ((PXSelectBase<OUSearchEntity>) this.Filter).Current.DuplicateFilesMsg = PXMessages.LocalizeNoPrefix("Recognition has already been started for another document or documents.");
      }
      this.ClearAttachmentsSelection();
      if (filesToRecognize.Count > 0)
        ((PXGraph) this).LongOperationManager.StartAsyncOperation((Func<CancellationToken, Task>) (cancellationToken =>
        {
          IEnumerable<RecognizedRecordFileInfo> batch = filesToRecognize.Select<(string, byte[], Guid), RecognizedRecordFileInfo>((Func<(string, byte[], Guid), RecognizedRecordFileInfo>) (f => new RecognizedRecordFileInfo($"{Guid.NewGuid().ToString()}\\{f.fileName}", f.fileData, f.fileId)));
          string subject = message.Subject;
          string from = message.From;
          string messageId = message.MessageId;
          CancellationToken cancellationToken1 = cancellationToken;
          int? ownerId = new int?();
          CancellationToken externalCancellationToken = cancellationToken1;
          return APInvoiceRecognitionEntry.RecognizeRecordsBatch(batch, subject, from, messageId, ownerId, true, externalCancellationToken);
        }));
    }
    catch (PXException ex)
    {
      ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ErrorMessage = ex.MessageNoPrefix;
    }
    this.Back.PressImpl(true, false);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Continue")]
  [PXButton]
  protected virtual void viewAPDocContinue()
  {
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ErrorMessage = (string) null;
    if (!NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.APBillAttachments).Cache.Cached))
      ((PXSelectBase<OUSearchEntity>) this.Filter).Current.PrevItemId = (string) null;
    IEnumerable<OUAPBillAttachment> selectedAttachments = this.GetSelectedAttachments();
    try
    {
      foreach (OUAPBillAttachment ouapBillAttachment in selectedAttachments)
        this.NavigateToAPDocument(ouapBillAttachment.FileHash, ouapBillAttachment.DuplicateLink);
    }
    catch (PXBaseRedirectException ex)
    {
      throw;
    }
    catch (PXException ex)
    {
      ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ErrorMessage = ex.MessageNoPrefix;
    }
  }

  private IEnumerable<OUAPBillAttachment> GetSelectedAttachments()
  {
    Enumerable.Empty<OUAPBillAttachment>();
    IEnumerable<OUAPBillAttachment> source = ((IEnumerable<PXResult<OUAPBillAttachment>>) ((PXSelectBase<OUAPBillAttachment>) this.APBillAttachments).Select(Array.Empty<object>())).AsEnumerable<PXResult<OUAPBillAttachment>>().Select<PXResult<OUAPBillAttachment>, OUAPBillAttachment>((Func<PXResult<OUAPBillAttachment>, OUAPBillAttachment>) (a => PXResult<OUAPBillAttachment>.op_Implicit(a)));
    return ((PXSelectBase<OUSearchEntity>) this.Filter).Current.AttachmentsCount.GetValueOrDefault() != 1 ? source.Where<OUAPBillAttachment>((Func<OUAPBillAttachment, bool>) (a => a.Selected.GetValueOrDefault())) : source.Take<OUAPBillAttachment>(1);
  }

  private (Guid? RefNbr, string Status) GetFileRecognitionInfo(string fileName)
  {
    if (string.IsNullOrWhiteSpace(fileName))
      return (new Guid?(), (string) null);
    string subject = ((PXSelectBase<OUMessage>) this.SourceMessage).Current.Subject;
    if (string.IsNullOrWhiteSpace(subject))
      return (new Guid?(), (string) null);
    string messageId = ((PXSelectBase<OUMessage>) this.SourceMessage).Current.MessageId;
    if (string.IsNullOrWhiteSpace(messageId))
      return (new Guid?(), (string) null);
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<RecognizedRecord>(new PXDataField[4]
    {
      (PXDataField) new PXDataField<RecognizedRecord.refNbr>(),
      (PXDataField) new PXDataField<RecognizedRecord.status>(),
      (PXDataField) new PXDataFieldValue<RecognizedRecord.messageID>((object) APInvoiceRecognitionEntry.NormalizeMessageId(messageId)),
      (PXDataField) new PXDataFieldValue<RecognizedRecord.subject>((object) APInvoiceRecognitionEntry.GetRecognizedSubject(subject, fileName))
    }))
      return pxDataRecord == null ? (new Guid?(), (string) null) : (pxDataRecord.GetGuid(0), pxDataRecord.GetString(1));
  }

  private Guid? GetFileDuplicateLink(Guid? refNbr, byte[] fileHash)
  {
    List<object> objectList = new List<object>()
    {
      (object) fileHash
    };
    BqlCommand bqlCommand = (BqlCommand) new SelectFromBase<RecognizedRecord, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<RecognizedRecord.fileHash, Equal<P.AsByteArray>>>>>.And<BqlOperand<RecognizedRecord.duplicateLink, IBqlGuid>.IsNull>>();
    if (refNbr.HasValue)
    {
      bqlCommand = bqlCommand.WhereAnd<Where<BqlOperand<RecognizedRecord.refNbr, IBqlGuid>.IsNotEqual<P.AsGuid>>>();
      objectList.Add((object) refNbr);
    }
    return ((RecognizedRecord) new PXView((PXGraph) this, true, bqlCommand).SelectSingle(objectList.ToArray()))?.RefNbr;
  }

  private void NavigateToAPDocument(byte[] fileHash, Guid? duplicateLink)
  {
    string str1 = APInvoiceRecognitionEntry.NormalizeMessageId(((PXSelectBase<OUMessage>) this.SourceMessage).Current.MessageId);
    RecognizedRecord recognizedRecord1;
    if (duplicateLink.HasValue)
      recognizedRecord1 = PXResultset<RecognizedRecord>.op_Implicit(PXSelectBase<RecognizedRecord, PXViewOf<RecognizedRecord>.BasedOn<SelectFromBase<RecognizedRecord, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<RecognizedRecord.refNbr, IBqlGuid>.IsEqual<P.AsGuid>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
      {
        (object) duplicateLink
      }));
    else
      recognizedRecord1 = PXResultset<RecognizedRecord>.op_Implicit(PXSelectBase<RecognizedRecord, PXViewOf<RecognizedRecord>.BasedOn<SelectFromBase<RecognizedRecord, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<RecognizedRecord.messageID, Equal<P.AsString>>>>>.And<BqlOperand<RecognizedRecord.fileHash, IBqlByteArray>.IsEqual<P.AsByteArray>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[2]
      {
        (object) str1,
        (object) fileHash
      }));
    RecognizedRecord recognizedRecord2 = recognizedRecord1;
    if (recognizedRecord2 == null)
      return;
    APInvoiceRecognitionEntry instance = PXGraph.CreateInstance<APInvoiceRecognitionEntry>();
    StringBuilder stringBuilder = new StringBuilder();
    string str2 = HttpUtility.UrlEncode(recognizedRecord2.RefNbr.ToString());
    stringBuilder.Append("&RecognizedRecordRefNbr=" + str2);
    try
    {
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 1);
    }
    catch (PXRedirectRequiredException ex)
    {
      this.ExternalRedirect(ex, append: stringBuilder.ToString());
    }
  }

  [PXUIField]
  [PXButton]
  protected virtual void createLead()
  {
    if (!this.Filter.VerifyRequired())
      return;
    LeadMaint instance = PXGraph.CreateInstance<LeadMaint>();
    CRLead crLead = ((PXSelectBase<CRLead>) instance.Lead).Insert();
    ((PXSelectBase<CRLead>) instance.Lead).Search<CRLead.contactID>((object) crLead.ContactID, Array.Empty<object>());
    crLead.FirstName = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.NewContactFirstName;
    crLead.LastName = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.NewContactLastName;
    crLead.EMail = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.NewContactEmail;
    crLead.BAccountID = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.BAccountID;
    crLead.DisplayName = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.NewContactDisplayName;
    crLead.Salutation = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.Salutation;
    crLead.FullName = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.FullName;
    crLead.Source = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.LeadSource;
    if (((PXSelectBase) instance.AddressCurrent).View.SelectSingle(Array.Empty<object>()) is Address address)
    {
      address.CountryID = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.CountryID;
      ((PXSelectBase) instance.AddressCurrent).Cache.Update((object) address);
    }
    ((PXSelectBase<CRLead>) instance.Lead).Update(crLead);
    ((PXAction<CRLead>) instance.Save).PressImpl(false, true);
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ContactType = (string) null;
    ((PXSelectBase<OUSearchEntity>) this.Filter).SetValueExt<OUSearchEntity.contactID>(((PXSelectBase<OUSearchEntity>) this.Filter).Current, (object) crLead.ContactID);
    ((PXSelectBase) this.Contact).View.Clear();
  }

  [PXUIField]
  [PXButton]
  protected virtual void createContact()
  {
    if (!this.Filter.VerifyRequired())
      return;
    ContactMaint instance = PXGraph.CreateInstance<ContactMaint>();
    PX.Objects.CR.Contact contact = ((PXSelectBase<PX.Objects.CR.Contact>) instance.Contact).Insert();
    ((PXSelectBase<PX.Objects.CR.Contact>) instance.Contact).Search<PX.Objects.CR.Contact.contactID>((object) contact.ContactID, Array.Empty<object>());
    contact.FirstName = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.NewContactFirstName;
    contact.LastName = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.NewContactLastName;
    contact.EMail = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.NewContactEmail;
    contact.BAccountID = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.BAccountID;
    contact.DisplayName = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.NewContactDisplayName;
    contact.Salutation = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.Salutation;
    contact.FullName = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.FullName;
    contact.Source = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ContactSource;
    if (((PXSelectBase) instance.AddressCurrent).View.SelectSingle(Array.Empty<object>()) is Address address)
    {
      address.CountryID = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.CountryID;
      ((PXSelectBase) instance.AddressCurrent).Cache.Update((object) address);
    }
    ((PXSelectBase<PX.Objects.CR.Contact>) instance.Contact).Update(contact);
    ((PXAction<PX.Objects.CR.Contact>) instance.Save).PressImpl(false, true);
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ContactType = (string) null;
    ((PXSelectBase<OUSearchEntity>) this.Filter).SetValueExt<OUSearchEntity.contactID>(((PXSelectBase<OUSearchEntity>) this.Filter).Current, (object) contact.ContactID);
    ((PXSelectBase) this.Contact).View.Clear();
  }

  [PXUIField]
  [PXButton]
  protected virtual void createCase()
  {
    try
    {
      ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ErrorMessage = (string) null;
      CRCaseMaint instance = PXGraph.CreateInstance<CRCaseMaint>();
      CRCase crCase = ((PXSelectBase<CRCase>) instance.Case).Insert();
      ((PXSelectBase<CRCase>) instance.Case).Search<CRCase.caseCD>((object) crCase.CaseCD, Array.Empty<object>());
      foreach (string field in (List<string>) ((PXSelectBase) this.NewCase).Cache.Fields)
        ((PXSelectBase) instance.Case).Cache.SetValue((object) crCase, field, ((PXSelectBase) this.NewCase).Cache.GetValue((object) ((PXSelectBase<OUCase>) this.NewCase).Current, field));
      PX.Objects.CR.Contact contact = ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).SelectSingle(Array.Empty<object>());
      crCase.CustomerID = contact.BAccountID;
      crCase.ContactID = contact.ContactID;
      ((PXSelectBase<CRCase>) instance.Case).Update(crCase);
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        BAccount baccount = PXSelectorAttribute.Select<CRCase.customerID>(((PXSelectBase) instance.Case).Cache, (object) crCase) as BAccount;
        if (this.PersistMessageDefault(PXNoteAttribute.GetNoteID<CRCase.noteID>(((PXSelectBase) instance.Case).Cache, (object) crCase), (int?) baccount?.BAccountID, crCase.ContactID))
          ((PXAction<CRCase>) instance.Save).PressImpl(false, true);
        transactionScope.Complete();
      }
      ((PXSelectBase<OUSearchEntity>) this.Filter).Current.Operation = (string) null;
    }
    catch (PXFieldValueProcessingException ex)
    {
      ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ErrorMessage = ((Exception) ex).InnerException != null ? ((Exception) ex).InnerException.Message : ((Exception) ex).Message;
    }
    catch (PXOuterException ex)
    {
      ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ErrorMessage = ex.InnerMessages[0];
    }
    catch (Exception ex)
    {
      ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ErrorMessage = ex.Message;
    }
  }

  [PXUIField]
  [PXButton]
  protected virtual void createOpportunity()
  {
    try
    {
      ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ErrorMessage = (string) null;
      OpportunityMaint instance = PXGraph.CreateInstance<OpportunityMaint>();
      CROpportunity crOpportunity = ((PXSelectBase<CROpportunity>) instance.Opportunity).Insert();
      ((PXSelectBase<CROpportunity>) instance.Opportunity).Search<CROpportunity.opportunityID>((object) crOpportunity.OpportunityID, Array.Empty<object>());
      foreach (string field in (List<string>) ((PXSelectBase) this.NewOpportunity).Cache.Fields)
        ((PXSelectBase) instance.Opportunity).Cache.SetValue((object) crOpportunity, field, ((PXSelectBase) this.NewOpportunity).Cache.GetValue((object) ((PXSelectBase<OUOpportunity>) this.NewOpportunity).Current, field));
      PX.Objects.CR.Contact contact = ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).SelectSingle(Array.Empty<object>());
      crOpportunity.BAccountID = contact.BAccountID;
      crOpportunity.ContactID = contact.ContactID;
      ((PXSelectBase<CROpportunity>) instance.Opportunity).Update(crOpportunity);
      crOpportunity.CuryID = ((PXSelectBase<OUOpportunity>) this.NewOpportunity).Current.CurrencyID;
      ((PXSelectBase<CROpportunity>) instance.Opportunity).Update(crOpportunity);
      Decimal? manualAmount = ((PXSelectBase<OUOpportunity>) this.NewOpportunity).Current.ManualAmount;
      Decimal num = 0M;
      if (manualAmount.GetValueOrDefault() > num & manualAmount.HasValue)
      {
        crOpportunity.ManualTotalEntry = new bool?(true);
        crOpportunity.CuryAmount = ((PXSelectBase<OUOpportunity>) this.NewOpportunity).Current.ManualAmount;
      }
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        BAccount baccount = PXSelectorAttribute.Select<CROpportunity.bAccountID>(((PXSelectBase) instance.Opportunity).Cache, (object) crOpportunity) as BAccount;
        if (this.PersistMessageDefault(PXNoteAttribute.GetNoteID<CROpportunity.noteID>(((PXSelectBase) instance.Opportunity).Cache, (object) crOpportunity), (int?) baccount?.BAccountID, crOpportunity.ContactID))
        {
          this.SetIgnorePersistFields((PXGraph) instance, typeof (CRContact), (PXFieldCollection) null);
          this.SetIgnorePersistFields((PXGraph) instance, typeof (CROpportunity), ((PXSelectBase) this.NewOpportunity).Cache.Fields);
          ((PXAction<CROpportunity>) instance.Save).PressImpl(false, true);
        }
        transactionScope.Complete();
      }
      ((PXSelectBase<OUSearchEntity>) this.Filter).Current.Operation = (string) null;
    }
    catch (PXFieldValueProcessingException ex)
    {
      ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ErrorMessage = ((Exception) ex).InnerException != null ? ((Exception) ex).InnerException.Message : ((Exception) ex).Message;
    }
    catch (PXOuterException ex)
    {
      ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ErrorMessage = ex.InnerMessages[0];
    }
    catch (Exception ex)
    {
      ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ErrorMessage = ex.Message;
    }
  }

  [PXUIField]
  [PXButton]
  protected virtual void logOut()
  {
    if (HttpContext.Current == null)
      return;
    this._loginUiService.LogoutCurrentUser();
    bool? isOidcFirstRun = ((PXSelectBase<OUMessage>) this.SourceMessage).Current.IsOidcFirstRun;
    if (isOidcFirstRun.HasValue && isOidcFirstRun.GetValueOrDefault())
      throw new PXRedirectToUrlException("../../Frames/Outlook/OidcFirstRun.html?returnUrl=../../Pages/CR/OU201000.aspx", string.Empty);
    PXDatabase.Delete<UserIdentity>(new PXDataFieldRestrict[2]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<UserIdentity.providerName>((PXDbType) 22, (object) "ExchangeIdentityToken"),
      (PXDataFieldRestrict) new PXDataFieldRestrict<UserIdentity.userID>((PXDbType) 14, (object) PXAccess.GetUserID())
    });
    throw new PXRedirectToUrlException("../../Frames/Outlook/FirstRun.html", string.Empty);
  }

  [PXUIField]
  [PXButton]
  protected virtual void createActivity()
  {
    if (!this.NewActivity.VerifyRequired())
      return;
    try
    {
      ((PXGraph) this).SelectTimeStamp();
      ((PXSelectBase<OUMessage>) this.SourceMessage).Current.Subject = ((PXSelectBase<OUActivity>) this.NewActivity).Current.Subject;
      OUActivity current = ((PXSelectBase<OUActivity>) this.NewActivity).Current;
      Guid? refNoteID = new Guid?();
      int? contactID = new int?();
      int? bAccountID = new int?();
      bool? nullable = current.IsLinkOpportunity;
      if (nullable.GetValueOrDefault())
      {
        PXResult<CROpportunity, BAccount> pxResult = (PXResult<CROpportunity, BAccount>) PXResultset<CROpportunity>.op_Implicit(PXSelectBase<CROpportunity, PXSelectJoin<CROpportunity, LeftJoin<BAccount, On<CROpportunity.bAccountID, Equal<BAccount.bAccountID>>>, Where<CROpportunity.opportunityID, Equal<Required<CROpportunity.opportunityID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
        {
          (object) current.OpportunityID
        }));
        if (pxResult != null)
        {
          refNoteID = PXNoteAttribute.GetNoteID<CROpportunity.noteID>(((PXSelectBase) this._opportunity).Cache, (object) PXResult<CROpportunity, BAccount>.op_Implicit(pxResult));
          bAccountID = PXResult<CROpportunity, BAccount>.op_Implicit(pxResult).BAccountID;
          contactID = PXResult<CROpportunity, BAccount>.op_Implicit(pxResult).ContactID;
        }
      }
      else
      {
        nullable = current.IsLinkCase;
        if (nullable.GetValueOrDefault())
        {
          PXResult<CRCase, BAccount> pxResult = (PXResult<CRCase, BAccount>) PXResultset<CRCase>.op_Implicit(PXSelectBase<CRCase, PXSelectJoin<CRCase, LeftJoin<BAccount, On<CRCase.customerID, Equal<BAccount.bAccountID>>>, Where<CRCase.caseCD, Equal<Required<CRCase.caseCD>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
          {
            (object) current.CaseCD
          }));
          if (pxResult != null)
          {
            refNoteID = PXNoteAttribute.GetNoteID<CRCase.noteID>(((PXSelectBase) this._case).Cache, (object) PXResult<CRCase, BAccount>.op_Implicit(pxResult));
            bAccountID = PXResult<CRCase, BAccount>.op_Implicit(pxResult).BAccountID;
            contactID = PXResult<CRCase, BAccount>.op_Implicit(pxResult).ContactID;
          }
        }
        else
        {
          PXResult<PX.Objects.CR.Contact, BAccount> pxResult = (PXResult<PX.Objects.CR.Contact, BAccount>) PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>>>, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>, And<PX.Objects.CR.Contact.isActive, Equal<True>, And<PX.Objects.CR.Contact.contactType, NotEqual<ContactTypesAttribute.bAccountProperty>>>>, OrderBy<Asc<PX.Objects.CR.Contact.contactPriority, Desc<PX.Objects.CR.Contact.bAccountID, Asc<PX.Objects.CR.Contact.contactID>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
          {
            (object) ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ContactID
          }));
          if (pxResult != null)
          {
            if (PXResult<PX.Objects.CR.Contact, BAccount>.op_Implicit(pxResult).ContactType == "EP")
            {
              BAccount baccount = PXResultset<BAccount>.op_Implicit(PXSelectBase<BAccount, PXSelectReadonly<BAccount, Where<BAccount.defContactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
              {
                (object) PXResult<PX.Objects.CR.Contact, BAccount>.op_Implicit(pxResult).ContactID
              }));
              refNoteID = baccount.NoteID;
              bAccountID = baccount.BAccountID;
              contactID = new int?();
            }
            else
            {
              refNoteID = PXNoteAttribute.GetNoteID<PX.Objects.CR.Contact.noteID>(((PXSelectBase) this.Contact).Cache, (object) PXResult<PX.Objects.CR.Contact, BAccount>.op_Implicit(pxResult));
              bAccountID = PXResult<PX.Objects.CR.Contact, BAccount>.op_Implicit(pxResult).BAccountID;
              contactID = PXResult<PX.Objects.CR.Contact, BAccount>.op_Implicit(pxResult).ContactID;
            }
          }
        }
      }
      ((PXGraph) this).Caches[typeof (Note)].ClearQueryCacheObsolete();
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        this.PersistMessageDefault(refNoteID, bAccountID, contactID);
        ((PXAction) this.Save).Press();
        transactionScope.Complete();
      }
      ((PXSelectBase<OUSearchEntity>) this.Filter).Current.Operation = (string) null;
    }
    catch (PXFieldValueProcessingException ex)
    {
      ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ErrorMessage = ((Exception) ex).InnerException != null ? ((Exception) ex).InnerException.Message : ((Exception) ex).Message;
    }
    catch (PXOuterException ex)
    {
      ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ErrorMessage = ex.InnerMessages[0];
    }
    catch (Exception ex)
    {
      ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ErrorMessage = ex.Message;
    }
  }

  [PXUIField]
  [PXButton]
  protected virtual void reply()
  {
    CRSMEmail crsmEmail = ((PXSelectBase<CRSMEmail>) this.Message).SelectSingle(Array.Empty<object>());
    if (crsmEmail == null)
      return;
    try
    {
      PXRedirectHelper.TryRedirect(((PXSelectBase) this.Message).Cache, (object) crsmEmail, string.Empty, (PXRedirectHelper.WindowMode) 1);
    }
    catch (PXRedirectRequiredException ex)
    {
      this.ExternalRedirect(ex, true, "&Run=ReplyInline");
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<OUSearchEntity.isRecognitionInProgress> e)
  {
    string messageId = ((PXSelectBase<OUMessage>) this.SourceMessage).Current.MessageId;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<OUSearchEntity.isRecognitionInProgress>>) e).ReturnValue = (object) (bool) (string.IsNullOrWhiteSpace(messageId) ? 0 : (APInvoiceRecognitionEntry.IsRecognitionInProgress(messageId) ? 1 : 0));
  }

  protected virtual void _(PX.Data.Events.RowSelected<OUSearchEntity> e)
  {
    OUSearchEntity row = e.Row;
    bool valueOrDefault = ((PXSelectBase<OUMessage>) this.SourceMessage).Current.IsIncome.GetValueOrDefault();
    int num1 = 0;
    if (!((PXSelectBase<OUMessage>) this.SourceMessage).Current.IsIncome.GetValueOrDefault() && ((PXSelectBase<OUMessage>) this.SourceMessage).Current.To != null)
    {
      string[] source = ((PXSelectBase<OUMessage>) this.SourceMessage).Current.To.Replace("\" <", "\"|<").Split(';');
      string[] array1 = ((IEnumerable<string>) source).Take<string>(source.Length - 1).Select<string, string>((Func<string, string>) (x => StringExtensions.Segment(x, '|', (ushort) 0).Replace("\"", string.Empty).Replace("'", string.Empty))).ToArray<string>();
      string[] array2 = ((IEnumerable<string>) source).Take<string>(source.Length - 1).Select<string, string>((Func<string, string>) (x => StringExtensions.Segment(x, '|', (ushort) 1).Replace("\"", string.Empty).Replace("'", string.Empty).Replace("<", string.Empty).Replace(">", string.Empty))).ToArray<string>();
      PXStringListAttribute.SetList<OUSearchEntity.outgoingEmail>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache, (object) row, array2, array1);
      row.OutgoingEmail = row.OutgoingEmail ?? ((IEnumerable<string>) array2).FirstOrDefault<string>();
      num1 = ((IEnumerable<string>) array2).Count<string>();
    }
    bool? nullable = ((PXSelectBase<OUMessage>) this.SourceMessage).Current.IsIncome;
    bool flag1 = false;
    if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue && !string.IsNullOrEmpty(row.OutgoingEmail))
    {
      string displayName = this.GetDisplayName(row.OutgoingEmail);
      string firstName;
      string lastName;
      OUSearchMaint.ParseDisplayName(displayName, out firstName, out lastName);
      row.EMail = row.OutgoingEmail;
      row.NewContactEmail = row.OutgoingEmail;
      row.DisplayName = displayName;
      row.NewContactFirstName = firstName;
      row.NewContactLastName = lastName;
    }
    PX.Objects.CR.Contact o = ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).SelectSingle(Array.Empty<object>());
    CRSMEmail crsmEmail = string.IsNullOrEmpty(((PXSelectBase<OUMessage>) this.SourceMessage).Current.MessageId) ? (CRSMEmail) null : ((PXSelectBase<CRSMEmail>) this.Message).SelectSingle(Array.Empty<object>());
    if (row.ContactType == null && row.Operation == null)
      row.ErrorMessage = string.Empty;
    row.ContactID = o.With<PX.Objects.CR.Contact, int?>((Func<PX.Objects.CR.Contact, int?>) (_ => _.ContactID));
    row.ContactBaccountID = o.With<PX.Objects.CR.Contact, int?>((Func<PX.Objects.CR.Contact, int?>) (_ => _.BAccountID));
    if (row.ContactType == null)
    {
      row.Salutation = o.With<PX.Objects.CR.Contact, string>((Func<PX.Objects.CR.Contact, string>) (_ => _.Salutation));
      row.FullName = o.With<PX.Objects.CR.Contact, string>((Func<PX.Objects.CR.Contact, string>) (_ => _.FullName));
    }
    bool flag2 = row.Operation == "CreateLead" || row.Operation == "CreateContact" || row.Operation == null;
    int? baccountId1;
    if (o != null)
    {
      string fieldString = EntityHelper.GetFieldString(((PXSelectBase) this.Contact).Cache.GetStateExt<PX.Objects.CR.Contact.contactType>((object) o) as PXFieldState);
      ((PXAction) this.ViewContact).SetCaption($"{PXMessages.LocalizeNoPrefix("View")} {PXMessages.LocalizeNoPrefix(fieldString)}");
      PX.Objects.CR.Contact contact = ((PXSelectBase<PX.Objects.CR.Contact>) this.DefaultContact).SelectSingle(Array.Empty<object>());
      if (contact != null)
      {
        int? baccountId2 = o.BAccountID;
        int? baccountId3 = contact.BAccountID;
        if (!(baccountId2.GetValueOrDefault() == baccountId3.GetValueOrDefault() & baccountId2.HasValue == baccountId3.HasValue))
          row.ErrorMessage = PXMessages.LocalizeNoPrefix("The selected contact belongs to a business account that differs from the business account associated with the sender contact.");
      }
    }
    else if (row.ContactType != null && !flag2 && PXSelectBase<CRCaseClass, PXSelect<CRCaseClass, Where<CRCaseClass.requireCustomer, Equal<True>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()).Count > 0)
    {
      row.ErrorMessage = PXMessages.LocalizeNoPrefix("Warning: Some case classes require customer.");
      baccountId1 = row.BAccountID;
      if (baccountId1.HasValue)
      {
        if (PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>, And<Where<BAccount.type, Equal<BAccountType.customerType>, Or<BAccount.type, Equal<BAccountType.combinedType>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
        {
          (object) row.BAccountID
        }).Count > 0)
          row.ErrorMessage = string.Empty;
      }
    }
    bool flag3 = row.Operation != "CreateAPDocument" && row.Operation != "ViewAPDocument";
    PXUIFieldAttribute.SetVisible<OUSearchEntity.contactID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache, (object) row, flag3);
    PXUIFieldAttribute.SetVisible<OUSearchEntity.errorMessage>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache, (object) row, !string.IsNullOrWhiteSpace(row.ErrorMessage));
    bool flag4 = !valueOrDefault && row.OutgoingEmail != null && num1 > 1 && row.Operation != "CreateAPDocument" && row.Operation != "ViewAPDocument";
    PXUIFieldAttribute.SetVisible<OUSearchEntity.outgoingEmail>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache, (object) row, flag4);
    bool flag5 = PXAccess.FeatureInstalled<FeaturesSet.customerModule>();
    bool flag6 = PXAccess.FeatureInstalled<FeaturesSet.caseManagement>();
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.NewCase).Cache, (object) ((PXSelectBase<OUCase>) this.NewCase).Current, (string) null, row.Operation == "Case");
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.NewOpportunity).Cache, (object) ((PXSelectBase<OUOpportunity>) this.NewOpportunity).Current, (string) null, flag5 && row.Operation == "Opp");
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.NewActivity).Cache, (object) ((PXSelectBase<OUActivity>) this.NewActivity).Current, (string) null, row.Operation == "Msg");
    if (row.Operation == "Msg")
    {
      PXUIFieldAttribute.SetEnabled<OUActivity.isLinkOpportunity>(((PXSelectBase) this.NewActivity).Cache, (object) ((PXSelectBase<OUActivity>) this.NewActivity).Current, PXResultset<CROpportunity>.op_Implicit(PXSelectBase<CROpportunity, PXSelect<CROpportunity, Where<CROpportunity.contactID, Equal<Current<OUSearchEntity.contactID>>, Or<CROpportunity.bAccountID, Equal<Current<OUSearchEntity.contactBaccountID>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>())) != null);
      PXUIFieldAttribute.SetEnabled<OUActivity.isLinkCase>(((PXSelectBase) this.NewActivity).Cache, (object) ((PXSelectBase<OUActivity>) this.NewActivity).Current, PXResultset<CRCase>.op_Implicit(PXSelectBase<CRCase, PXSelect<CRCase, Where<CRCase.contactID, Equal<Current<OUSearchEntity.contactID>>, Or<CRCase.customerID, Equal<Current<OUSearchEntity.contactBaccountID>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>())) != null);
      PXCache cache1 = ((PXSelectBase) this.NewActivity).Cache;
      OUActivity current1 = ((PXSelectBase<OUActivity>) this.NewActivity).Current;
      nullable = ((PXSelectBase<OUActivity>) this.NewActivity).Current.IsLinkCase;
      int num2 = nullable.GetValueOrDefault() ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<OUActivity.caseCD>(cache1, (object) current1, num2 != 0);
      PXCache cache2 = ((PXSelectBase) this.NewActivity).Cache;
      OUActivity current2 = ((PXSelectBase<OUActivity>) this.NewActivity).Current;
      nullable = ((PXSelectBase<OUActivity>) this.NewActivity).Current.IsLinkOpportunity;
      int num3 = nullable.GetValueOrDefault() ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<OUActivity.opportunityID>(cache2, (object) current2, num3 != 0);
    }
    PXUIFieldAttribute.SetVisible<OUActivity.isLinkCase>(((PXSelectBase) this.NewActivity).Cache, (object) ((PXSelectBase<OUActivity>) this.NewActivity).Current, row.Operation == "Msg" & flag6);
    PXCache cache3 = ((PXSelectBase) this.NewActivity).Cache;
    OUActivity current3 = ((PXSelectBase<OUActivity>) this.NewActivity).Current;
    int num4;
    if (row.Operation == "Msg")
    {
      nullable = ((PXSelectBase<OUActivity>) this.NewActivity).Current.IsLinkOpportunity;
      num4 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num4 = 0;
    int num5 = flag6 ? 1 : 0;
    int num6 = num4 & num5;
    PXUIFieldAttribute.SetVisible<OUActivity.caseCD>(cache3, (object) current3, num6 != 0);
    PXUIFieldAttribute.SetVisible<OUActivity.isLinkOpportunity>(((PXSelectBase) this.NewActivity).Cache, (object) ((PXSelectBase<OUActivity>) this.NewActivity).Current, row.Operation == "Msg" & flag5);
    PXCache cache4 = ((PXSelectBase) this.NewActivity).Cache;
    OUActivity current4 = ((PXSelectBase<OUActivity>) this.NewActivity).Current;
    int num7;
    if (row.Operation == "Msg")
    {
      nullable = ((PXSelectBase<OUActivity>) this.NewActivity).Current.IsLinkOpportunity;
      num7 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num7 = 0;
    int num8 = flag5 ? 1 : 0;
    int num9 = num7 & num8;
    PXUIFieldAttribute.SetVisible<OUActivity.opportunityID>(cache4, (object) current4, num9 != 0);
    PXCache cache5 = ((PXSelectBase) this.NewActivity).Cache;
    object current5 = ((PXSelectBase) this.NewActivity).Cache.Current;
    int num10;
    if (row.Operation == "Msg")
    {
      nullable = ((PXSelectBase<OUActivity>) this.NewActivity).Current.IsLinkCase;
      if (nullable.GetValueOrDefault())
      {
        num10 = 1;
        goto label_28;
      }
    }
    num10 = 2;
label_28:
    PXDefaultAttribute.SetPersistingCheck<OUActivity.caseCD>(cache5, current5, (PXPersistingCheck) num10);
    PXCache cache6 = ((PXSelectBase) this.NewActivity).Cache;
    object current6 = ((PXSelectBase) this.NewActivity).Cache.Current;
    int num11;
    if (row.Operation == "Msg")
    {
      nullable = ((PXSelectBase<OUActivity>) this.NewActivity).Current.IsLinkOpportunity;
      if (nullable.GetValueOrDefault())
      {
        num11 = 1;
        goto label_32;
      }
    }
    num11 = 2;
label_32:
    PXDefaultAttribute.SetPersistingCheck<OUActivity.opportunityID>(cache6, current6, (PXPersistingCheck) num11);
    bool flag7 = false;
    if (crsmEmail != null)
    {
      EntityHelper entityHelper = new EntityHelper((PXGraph) this);
      if (crsmEmail.RefNoteID.HasValue)
      {
        object entityRow = entityHelper.GetEntityRow(crsmEmail.RefNoteID);
        if (entityRow != null)
        {
          System.Type primaryGraphType = entityHelper.GetPrimaryGraphType(entityRow, true);
          if (primaryGraphType != (System.Type) null)
            flag7 = this.IsRights(primaryGraphType, entityHelper.GetEntityRowType(crsmEmail.RefNoteID), (PXCacheRights) 1);
        }
      }
      string str = entityHelper.GetFriendlyEntityName(crsmEmail.RefNoteID) ?? "Entity";
      ((PXAction) this.ViewEntity).SetCaption($"{PXMessages.LocalizeNoPrefix("View")} {PXMessages.LocalizeNoPrefix(str)}");
      row.EntityName = (str ?? "Entity") + ":";
      row.EntityID = entityHelper.GetEntityDescription(crsmEmail.RefNoteID, ((object) crsmEmail).GetType());
      if (row.EntityID == null)
        crsmEmail = (CRSMEmail) null;
    }
    PXUIFieldAttribute.SetVisible<OUSearchEntity.salutation>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache, (object) row, flag2 && (o != null || row.ContactType != null));
    PXUIFieldAttribute.SetVisible<OUSearchEntity.fullName>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache, (object) row, flag2 && (o != null || row.ContactType != null));
    PXUIFieldAttribute.SetVisible<OUSearchEntity.entityName>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache, (object) row, flag2 && o != null && crsmEmail != null);
    PXUIFieldAttribute.SetVisible<OUSearchEntity.entityID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache, (object) row, flag2 && o != null && crsmEmail != null);
    PXUIFieldAttribute.SetVisible<OUSearchEntity.newContactFirstName>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache, (object) row, row.ContactType != null);
    PXUIFieldAttribute.SetVisible<OUSearchEntity.newContactLastName>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache, (object) row, row.ContactType != null);
    PXUIFieldAttribute.SetVisible<OUSearchEntity.newContactEmail>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache, (object) row, row.ContactType != null);
    PXUIFieldAttribute.SetVisible<OUSearchEntity.bAccountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache, (object) row, row.ContactType != null);
    PXUIFieldAttribute.SetVisible<OUSearchEntity.leadSource>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache, (object) row, row.ContactType == "LD");
    PXUIFieldAttribute.SetVisible<OUSearchEntity.contactSource>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache, (object) row, row.ContactType == "PN");
    PXUIFieldAttribute.SetVisible<OUSearchEntity.countryID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache, (object) row, row.ContactType != null);
    PXUIFieldAttribute.SetEnabled<OUSearchEntity.salutation>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache, (object) row, row.ContactType != null);
    PXCache cache7 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache;
    OUSearchEntity ouSearchEntity = row;
    int num12;
    if (row.ContactType != null)
    {
      baccountId1 = row.BAccountID;
      num12 = !baccountId1.HasValue ? 1 : 0;
    }
    else
      num12 = 0;
    PXUIFieldAttribute.SetEnabled<OUSearchEntity.fullName>(cache7, (object) ouSearchEntity, num12 != 0);
    PXUIFieldAttribute.SetEnabled<OUSearchEntity.leadSource>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache, (object) row, row.ContactType == "LD");
    PXUIFieldAttribute.SetEnabled<OUSearchEntity.contactSource>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache, (object) row, row.ContactType == "PN");
    PXUIFieldAttribute.SetEnabled<OUSearchEntity.countryID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache, (object) row, row.ContactType != null);
    System.Type graphType = o == null || !(o.ContactType == "EP") ? typeof (ContactMaint) : typeof (EmployeeMaint);
    ((PXAction) this.ViewContact).SetVisible(o != null & flag2 && this.IsRights(graphType, typeof (PX.Objects.CR.Contact), (PXCacheRights) 1));
    PXAction<OUSearchEntity> viewBaccount = this.ViewBAccount;
    int num13;
    if (o != null & flag2 && o.ContactType != "EP")
    {
      baccountId1 = o.BAccountID;
      if (baccountId1.HasValue)
      {
        num13 = this.IsRights(typeof (BusinessAccountMaint), typeof (BAccount), (PXCacheRights) 1) ? 1 : 0;
        goto label_46;
      }
    }
    num13 = 0;
label_46:
    ((PXAction) viewBaccount).SetVisible(num13 != 0);
    PXAction<OUSearchEntity> viewEntity = this.ViewEntity;
    int num14;
    if (((!valueOrDefault ? 0 : (o != null ? 1 : 0)) & (flag2 ? 1 : 0)) != 0 && crsmEmail != null)
    {
      Guid? refNoteId = crsmEmail.RefNoteID;
      Guid? noteId = o.NoteID;
      if ((refNoteId.HasValue == noteId.HasValue ? (refNoteId.HasValue ? (refNoteId.GetValueOrDefault() != noteId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        num14 = this.IsRights(typeof (CRCaseMaint), typeof (CRCase), (PXCacheRights) 1) | flag7 ? 1 : 0;
        goto label_50;
      }
    }
    num14 = 0;
label_50:
    ((PXAction) viewEntity).SetVisible(num14 != 0);
    bool flag8 = ((o != null || row.OutgoingEmail == null ? 0 : (row.ContactType == null ? 1 : 0)) & (flag2 ? 1 : 0)) != 0;
    ((PXAction) this.GoCreateLead).SetVisible(flag8 && this.IsRights(typeof (LeadMaint), typeof (CRLead), (PXCacheRights) 3));
    ((PXAction) this.GoCreateContact).SetVisible(flag8 && this.IsRights(typeof (ContactMaint), typeof (PX.Objects.CR.Contact), (PXCacheRights) 3));
    ((PXAction) this.GoCreateCase).SetVisible(((!valueOrDefault || o == null || !(o.ContactType == "PN") ? 0 : (((PXSelectBase<OUMessage>) this.SourceMessage).Current.MessageId != null ? 1 : 0)) & (flag2 ? 1 : 0)) != 0 && this.IsRights(typeof (CRCaseMaint), typeof (CRCase), (PXCacheRights) 3));
    ((PXAction) this.GoCreateOpportunity).SetVisible(valueOrDefault && ((o == null || !(o.ContactType != "EP") ? 0 : (((PXSelectBase<OUMessage>) this.SourceMessage).Current.MessageId != null ? 1 : 0)) & (flag2 ? 1 : 0)) != 0 && this.IsRights(typeof (OpportunityMaint), typeof (CROpportunity), (PXCacheRights) 3));
    ((PXAction) this.GoCreateActivity).SetVisible(((o == null ? 0 : (row.ContactType == null ? 1 : 0)) & (flag2 ? 1 : 0)) != 0 && this.IsRights(typeof (CRActivityMaint), typeof (CRActivity), (PXCacheRights) 3));
    ((PXAction) this.Back).SetVisible(row.ContactType != null || row.Operation != null);
    ((PXAction) this.Reply).SetVisible(valueOrDefault && crsmEmail != null & flag2 && row.ContactType == null && this.IsRights(typeof (CREmailActivityMaint), typeof (CRSMEmail), (PXCacheRights) 3));
    ((PXAction) this.CreateLead).SetVisible(row.ContactType == "LD");
    ((PXAction) this.CreateContact).SetVisible(row.ContactType == "PN");
    ((PXAction) this.CreateCase).SetVisible(row.Operation == "Case");
    ((PXAction) this.CreateOpportunity).SetVisible(row.Operation == "Opp");
    ((PXAction) this.CreateActivity).SetVisible(row.Operation == "Msg");
    ((PXAction) this.LogOut).SetVisible(row.Operation != "CreateAPDocument" && row.Operation != "ViewAPDocument");
    this.SetAPDocumentButtonsState(row, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUSearchEntity>>) e).Cache);
  }

  private void SetAPDocumentButtonsState(OUSearchEntity row, PXCache cache)
  {
    bool flag1 = PXSiteMap.Provider.FindSiteMapNodeByScreenID("AP301000") != null;
    bool flag2 = ((row.Operation != null ? 0 : (row.ContactType == null ? 1 : 0)) & (flag1 ? 1 : 0)) != 0 && PXAccess.FeatureInstalled<FeaturesSet.apDocumentRecognition>() && this.InvoiceRecognitionClient.IsConfigured();
    List<\u003C\u003Ef__AnonymousType33<string>> list1 = OUSearchMaint.DeserializeAttachmentNames(row).Select(n => new
    {
      Status = this.GetFileRecognitionInfo(n).Status
    }).ToList();
    if (row.ContactType != null || row.Operation != null && row.Operation != "CreateAPDocument" && row.Operation != "ViewAPDocument")
      row.DuplicateFilesMsg = (string) null;
    PXUIFieldAttribute.SetVisible<OUSearchEntity.duplicateFilesMsg>(cache, (object) row, !string.IsNullOrEmpty(row.DuplicateFilesMsg));
    bool valueOrDefault = row.IsDuplicateDelected.GetValueOrDefault();
    int num = list1.Count(a => !string.IsNullOrEmpty(a.Status) && a.Status != "I" && a.Status != "N");
    bool flag3 = num > 0;
    ((PXAction) this.ViewAPDoc).SetVisible(flag2 && flag3 | valueOrDefault);
    List<OUAPBillAttachment> list2 = ((IQueryable<PXResult<OUAPBillAttachment>>) ((PXSelectBase<OUAPBillAttachment>) this.APBillAttachments).Select(Array.Empty<object>())).Select<PXResult<OUAPBillAttachment>, OUAPBillAttachment>((Expression<Func<PXResult<OUAPBillAttachment>, OUAPBillAttachment>>) (a => (OUAPBillAttachment) a)).ToList<OUAPBillAttachment>();
    bool flag4 = list2.Any<OUAPBillAttachment>((Func<OUAPBillAttachment, bool>) (a => a.Selected.GetValueOrDefault()));
    ((PXAction) this.CreateAPDocContinue).SetVisible(((!(row.Operation == "CreateAPDocument") ? 0 : (!row.IsRecognitionInProgress.GetValueOrDefault() ? 1 : 0)) & (flag4 ? 1 : 0)) != 0);
    ((PXAction) this.ViewAPDocContinue).SetVisible(row.Operation == "ViewAPDocument" & flag4);
    bool flag5 = list2.All<OUAPBillAttachment>((Func<OUAPBillAttachment, bool>) (a => !string.IsNullOrEmpty(a.RecognitionStatus)));
    bool flag6 = list1.Any(a => string.IsNullOrEmpty(a.Status));
    ((PXAction) this.CreateAPDoc).SetVisible(flag2 & flag6 && (list2.Count == 0 || !valueOrDefault || !flag5));
    bool flag7 = flag2 & flag3;
    PXUIFieldAttribute.SetVisible<OUSearchEntity.numOfRecognizedDocumentsCheck>(cache, (object) row, flag7);
    PXUIFieldAttribute.SetVisible<OUSearchEntity.numOfRecognizedDocuments>(cache, (object) row, flag7);
    if (num == 1)
      row.NumOfRecognizedDocuments = PXMessages.LocalizeNoPrefix("A document has been recognized.");
    else if (num > 1)
      row.NumOfRecognizedDocuments = PXMessages.LocalizeFormatNoPrefixNLA("{0} documents have been recognized.", new object[1]
      {
        (object) num
      });
    else
      row.NumOfRecognizedDocuments = (string) null;
  }

  private static IEnumerable<string> DeserializeAttachmentNames(OUSearchEntity row)
  {
    string[] strArray1;
    if (row == null)
    {
      strArray1 = (string[]) null;
    }
    else
    {
      string attachmentNames = row.AttachmentNames;
      if (attachmentNames == null)
        strArray1 = (string[]) null;
      else
        strArray1 = attachmentNames.Split(new char[1]{ ';' }, StringSplitOptions.RemoveEmptyEntries);
    }
    string[] strArray2 = strArray1;
    return strArray2 == null || strArray2.Length == 0 ? Enumerable.Empty<string>() : (IEnumerable<string>) strArray2;
  }

  protected virtual void OUSearchEntity_OutgoingEmail_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase<OUSearchEntity>) this.Filter).Current.Operation = (string) null;
    ((PXSelectBase) this.Filter).Cache.SetValueExt<OUSearchEntity.newContactEmail>((object) ((PXSelectBase<OUSearchEntity>) this.Filter).Current, (object) ((PXSelectBase<OUSearchEntity>) this.Filter).Current.OutgoingEmail);
    ((PXSelectBase) this.Filter).Cache.SetValueExt<OUSearchEntity.contactID>((object) ((PXSelectBase<OUSearchEntity>) this.Filter).Current, (object) null);
  }

  protected virtual void OUSearchEntity_ContactID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    PX.Objects.CR.Contact o = ((PXSelectBase<PX.Objects.CR.Contact>) this.DefaultContact).SelectSingle(Array.Empty<object>());
    e.NewValue = (object) o.With<PX.Objects.CR.Contact, int?>((Func<PX.Objects.CR.Contact, int?>) (_ => _.ContactID));
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void OUSearchEntity_ContactID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is OUSearchEntity row))
      return;
    if (!row.ContactID.HasValue)
    {
      sender.SetValueExt<OUSearchEntity.eMail>((object) row, (object) row.OutgoingEmail);
      object obj;
      sender.RaiseFieldDefaulting<OUSearchEntity.contactID>((object) row, ref obj);
    }
    else
    {
      PX.Objects.CR.Contact contact = ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).SelectSingle(Array.Empty<object>());
      row.EMail = contact.EMail;
    }
  }

  protected virtual void OUSearchEntity_ContactBaccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    PX.Objects.CR.Contact o = ((PXSelectBase<PX.Objects.CR.Contact>) this.DefaultContact).SelectSingle(Array.Empty<object>());
    e.NewValue = (object) o.With<PX.Objects.CR.Contact, int?>((Func<PX.Objects.CR.Contact, int?>) (_ => _.BAccountID));
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void OUCase_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is OUCase row) || row.CaseClassID == null)
      return;
    if (PXSelectBase<CRCaseClass, PXSelect<CRCaseClass, Where<CRCaseClass.requireContract, Equal<True>, And<CRCaseClass.caseClassID, Equal<Current<OUCase.caseClassID>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()).Count == 0)
    {
      PXUIFieldAttribute.SetVisible<OUCase.contractID>(sender, (object) row, false);
    }
    else
    {
      object obj1 = PXSelectorAttribute.SelectFirst<OUCase.contractID>(sender, e.Row);
      if (obj1 == null)
        return;
      int? contractId1 = PXResult.Unwrap<PX.Objects.CT.Contract>(obj1).ContractID;
      object obj2 = PXSelectorAttribute.SelectLast<OUCase.contractID>(sender, e.Row);
      if (obj2 == null)
        return;
      int? contractId2 = PXResult.Unwrap<PX.Objects.CT.Contract>(obj2).ContractID;
      if (!contractId1.HasValue)
        return;
      int? nullable1 = contractId1;
      int? nullable2 = contractId2;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        return;
      ((PXSelectBase<OUCase>) this.NewCase).SetValueExt<OUCase.contractID>(row, (object) contractId1);
    }
  }

  private OUAPBillAttachment FindAttachmentByKeys(string itemId, string id)
  {
    return ((IEnumerable<PXResult<OUAPBillAttachment>>) ((PXSelectBase<OUAPBillAttachment>) this.APBillAttachments).Select(Array.Empty<object>())).AsEnumerable<PXResult<OUAPBillAttachment>>().Select<PXResult<OUAPBillAttachment>, OUAPBillAttachment>((Func<PXResult<OUAPBillAttachment>, OUAPBillAttachment>) (a => PXResult<OUAPBillAttachment>.op_Implicit(a))).Where<OUAPBillAttachment>((Func<OUAPBillAttachment, bool>) (a => a.ItemId == itemId && a.Id == id)).FirstOrDefault<OUAPBillAttachment>();
  }

  public virtual void OUAPBillAttachmentSelectFileFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    string itemId,
    string id)
  {
    OUAPBillAttachment attachmentByKeys = this.FindAttachmentByKeys(itemId, id);
    if (attachmentByKeys == null)
      return;
    if (!attachmentByKeys.DuplicateLink.HasValue)
      attachmentByKeys.RecognitionStatus = this.GetFileRecognitionInfo(attachmentByKeys.Name).Status;
    object selected = (object) attachmentByKeys.Selected;
    bool flag = false;
    string operation = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.Operation;
    string str1 = (string) null;
    PXErrorLevel pxErrorLevel1 = (PXErrorLevel) 0;
    switch (operation)
    {
      case "CreateAPDocument":
        flag = string.IsNullOrEmpty(attachmentByKeys.RecognitionStatus);
        if (!flag)
        {
          str1 = PXMessages.LocalizeNoPrefix("The file is already recognized.");
          pxErrorLevel1 = (PXErrorLevel) 1;
          break;
        }
        break;
      case "ViewAPDocument":
        flag = !string.IsNullOrEmpty(attachmentByKeys.RecognitionStatus) && attachmentByKeys.RecognitionStatus != "I";
        if (!flag)
        {
          str1 = PXMessages.LocalizeNoPrefix("The file has not been recognized.");
          pxErrorLevel1 = (PXErrorLevel) 1;
          break;
        }
        break;
    }
    ((PXSelectBase) this.APBillAttachments).Cache.RaiseFieldSelecting<OUAPBillAttachment.selected>((object) attachmentByKeys, ref selected, true);
    PXFieldState pxFieldState1 = (PXFieldState) selected;
    PXFieldState pxFieldState2 = pxFieldState1;
    System.Type dataType = pxFieldState1.DataType;
    string name = attachmentByKeys.Name;
    bool? nullable1 = new bool?(true);
    bool? nullable2 = new bool?(flag);
    string str2 = str1;
    PXErrorLevel pxErrorLevel2 = pxErrorLevel1;
    bool? nullable3 = new bool?();
    bool? nullable4 = new bool?();
    int? nullable5 = new int?();
    int? nullable6 = new int?();
    int? nullable7 = new int?();
    string str3 = name;
    string str4 = str2;
    PXErrorLevel pxErrorLevel3 = pxErrorLevel2;
    bool? nullable8 = nullable2;
    bool? nullable9 = nullable1;
    bool? nullable10 = new bool?();
    PXFieldState instance = PXFieldState.CreateInstance((object) pxFieldState2, dataType, nullable3, nullable4, nullable5, nullable6, nullable7, (object) null, (string) null, (string) null, str3, str4, pxErrorLevel3, nullable8, nullable9, nullable10, (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    e.ReturnState = (object) instance;
  }

  protected virtual void _(PX.Data.Events.RowSelected<OUAPBillAttachment> e)
  {
    OUAPBillAttachment row = e.Row;
    if (row == null)
      return;
    string operation = ((PXSelectBase<OUSearchEntity>) this.Filter).Current.Operation;
    bool flag = false;
    switch (operation)
    {
      case "CreateAPDocument":
        flag = string.IsNullOrEmpty(row.RecognitionStatus);
        break;
      case "ViewAPDocument":
        flag = !string.IsNullOrEmpty(row.RecognitionStatus) && row.RecognitionStatus != "I";
        break;
    }
    PXUIFieldAttribute.SetEnabled<OUAPBillAttachment.selected>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<OUAPBillAttachment>>) e).Cache, (object) row, flag);
  }

  public virtual void _(
    PX.Data.Events.FieldUpdating<OUAPBillAttachment, OUAPBillAttachment.selected> e)
  {
    OUAPBillAttachment attachmentRow = e.Row;
    if (attachmentRow == null || !(PXBoolAttribute.ConvertValue(((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<OUAPBillAttachment, OUAPBillAttachment.selected>>) e).NewValue) as bool?).GetValueOrDefault() || ((PXSelectBase<OUSearchEntity>) this.Filter).Current.Operation == "CreateAPDocument")
      return;
    foreach (object obj in ((IEnumerable<PXResult<OUAPBillAttachment>>) ((PXSelectBase<OUAPBillAttachment>) this.APBillAttachments).Select(Array.Empty<object>())).AsEnumerable<PXResult<OUAPBillAttachment>>().Select<PXResult<OUAPBillAttachment>, OUAPBillAttachment>((Func<PXResult<OUAPBillAttachment>, OUAPBillAttachment>) (a => PXResult<OUAPBillAttachment>.op_Implicit(a))).Where<OUAPBillAttachment>((Func<OUAPBillAttachment, bool>) (a => !((object) attachmentRow).Equals((object) a))))
      ((PXSelectBase) this.APBillAttachments).Cache.SetValue<OUAPBillAttachment.selected>(obj, (object) false);
  }

  public virtual void OUAPBillAttachmentSelectFileFieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e,
    string itemId,
    string id)
  {
    OUAPBillAttachment attachmentRow = this.FindAttachmentByKeys(itemId, id);
    if (attachmentRow == null)
      return;
    bool? nullable = PXBoolAttribute.ConvertValue(e.NewValue) as bool?;
    ((PXSelectBase) this.APBillAttachments).Cache.SetValue<OUAPBillAttachment.selected>((object) attachmentRow, (object) nullable);
    if (!nullable.GetValueOrDefault() || ((PXSelectBase<OUSearchEntity>) this.Filter).Current.Operation == "CreateAPDocument")
      return;
    foreach (object obj in ((IEnumerable<PXResult<OUAPBillAttachment>>) ((PXSelectBase<OUAPBillAttachment>) this.APBillAttachments).Select(Array.Empty<object>())).AsEnumerable<PXResult<OUAPBillAttachment>>().Select<PXResult<OUAPBillAttachment>, OUAPBillAttachment>((Func<PXResult<OUAPBillAttachment>, OUAPBillAttachment>) (a => PXResult<OUAPBillAttachment>.op_Implicit(a))).Where<OUAPBillAttachment>((Func<OUAPBillAttachment, bool>) (a => !((object) attachmentRow).Equals((object) a))))
      ((PXSelectBase) this.APBillAttachments).Cache.SetValue<OUAPBillAttachment.selected>(obj, (object) false);
  }

  public bool IsRights(System.Type graphType, System.Type cacheType, PXCacheRights checkRights)
  {
    List<string> stringList1 = (List<string>) null;
    List<string> stringList2 = (List<string>) null;
    PXSiteMapNode siteMapNode = PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, graphType);
    if (siteMapNode == null)
      return false;
    PXCacheRights pxCacheRights;
    PXAccess.GetRights(siteMapNode.ScreenID, graphType.Name, cacheType, ref pxCacheRights, ref stringList1, ref stringList2);
    return pxCacheRights >= checkRights;
  }

  public string GetDisplayName(string email)
  {
    if (((PXSelectBase<OUMessage>) this.SourceMessage).Current.To == null)
      return string.Empty;
    string str = ((IEnumerable<string>) ((PXSelectBase<OUMessage>) this.SourceMessage).Current.To.Replace("\" <", "\"|<").Split(';')).FirstOrDefault<string>((Func<string, bool>) (x => x.Contains(email)));
    return str == null ? string.Empty : StringExtensions.FirstSegment(str, '|').Replace("\"", string.Empty).Replace("'", string.Empty);
  }

  private void SetIgnorePersistFields(
    PXGraph graph,
    System.Type cacheType,
    PXFieldCollection ignoredFields)
  {
    foreach (object obj in graph.Caches[cacheType].Cached)
    {
      foreach (string str in ((IEnumerable<string>) graph.Caches[cacheType].Fields).Where<string>((Func<string, bool>) (x => ignoredFields == null || !ignoredFields.Contains(x))))
      {
        if (graph.Caches[cacheType].GetValue(obj, str) == null)
          PXDefaultAttribute.SetPersistingCheck(graph.Caches[cacheType], str, obj, (PXPersistingCheck) 2);
      }
    }
  }

  protected bool PersistMessageDefault(Guid? refNoteID, int? bAccountID, int? contactID)
  {
    int num = this.TryDoAndLogIfExchangeReceiveFailed<bool>((Func<bool>) (() =>
    {
      this.SaveCRSMEmail(((PXSelectBase<OUMessage>) this.SourceMessage).Current, (OUSearchMaint.CRSMEmailPreparer) new OUSearchMaint.DefaultCRSMEmailPreparer(refNoteID, bAccountID, contactID, ((PXSelectBase<OUSearchEntity>) this.Filter).Current.OutgoingEmail));
      return true;
    })) ? 1 : 0;
    ((PXSelectBase) this.Message).View.Clear();
    ((PXSelectBase) this.Message).Cache.Clear();
    return num != 0;
  }

  protected virtual void SaveCRSMEmail(
    OUMessage message,
    OUSearchMaint.CRSMEmailPreparer emailPreparer = null)
  {
    if (message == null)
      throw new ArgumentNullException(nameof (message));
    if (message.MessageId == null)
      throw new ArgumentException("message.MessageId is null.", nameof (message));
    CREmailActivityMaint instance = PXGraph.CreateInstance<CREmailActivityMaint>();
    CRSMEmail email = PXResultset<CRSMEmail>.op_Implicit(PXSelectBase<CRSMEmail, PXViewOf<CRSMEmail>.BasedOn<SelectFromBase<CRSMEmail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRSMEmail.messageId, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) message.MessageId
    }));
    if (email == null)
    {
      email = this.CreateNewCRSMEmail(message, instance);
      emailPreparer?.PrepareNewEmail(email);
    }
    else
      emailPreparer?.PrepareExistingEmail(email);
    email.Subject = string.IsNullOrEmpty(message.Subject) ? PXMessages.LocalizeNoPrefix("(No subject)") : message.Subject;
    ((PXSelectBase<CRSMEmail>) instance.Message).Update(email);
    ((PXAction<CRSMEmail>) instance.Save).PressImpl(false, true);
  }

  protected virtual CRSMEmail CreateNewCRSMEmail(OUMessage message, CREmailActivityMaint graph)
  {
    string ewsAccessToken = this.GetEwsAccessToken();
    ExchangeMessage exchangeMessage = OUSearchMaint.GetExchangeMessage(message, ewsAccessToken);
    ImageExtractor imageExtractor = new ImageExtractor();
    CRSMEmail crsmEmail = ((PXSelectBase<CRSMEmail>) graph.Message).Insert();
    List<FileDto> files = (List<FileDto>) null;
    if (!crsmEmail.MailAccountID.HasValue)
      throw new OUSearchMaint.ExchangeReceiveEmailFailedException("You cannot add an email because the default email account is not specified. Specify the account in the Default Email Account box of the Email Preferences (SM204001) form.");
    crsmEmail.MessageId = message.MessageId;
    crsmEmail.MailTo = message.To;
    crsmEmail.MailReply = (string) null;
    crsmEmail.MailCc = message.CC;
    crsmEmail.IsIncome = message.IsIncome;
    crsmEmail.MPStatus = "PD";
    crsmEmail.StartDate = exchangeMessage.StartDate;
    CRSMEmail msg = ((PXSelectBase<CRSMEmail>) graph.Message).Update(crsmEmail);
    PXNoteAttribute.GetNoteID<CRSMEmail.noteID>(((PXSelectBase) graph.Message).Cache, (object) msg);
    if (exchangeMessage.Attachments != null)
    {
      files = new List<FileDto>();
      UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
      foreach (AttachmentDetails attachment1 in exchangeMessage.Attachments)
      {
        XElement attachment2 = OUSearchMaint.GetAttachmentsFromExchangeServerUsingEWS(message.EwsUrl, ewsAccessToken, attachment1).FirstOrDefault<XElement>();
        FileDto file;
        if (attachment2 != null && OUSearchMaint.TryParseAttachment(msg, attachment2, out file))
        {
          string lower = PXPath.GetExtension(file.Name).ToLower();
          if (instance.IgnoreFileRestrictions || ((IEnumerable<string>) instance.AllowedFileTypes).Contains<string>(lower))
          {
            PXBlobStorage.SaveContext = new PXBlobStorageContext()
            {
              ViewName = ((PXSelectBase) graph.Message).Name,
              Graph = (PXGraph) graph,
              DataRow = (object) msg,
              NoteID = msg.NoteID
            };
            using (Disposable.Create((Action) (() => PXBlobStorage.SaveContext = (PXBlobStorageContext) null)))
              graph.InsertFile(file);
            files.Add(file);
          }
        }
      }
    }
    string str;
    if (files == null)
    {
      ICollection<ImageExtractor.ImageInfo> imageInfos;
      imageExtractor.Extract(exchangeMessage.Body, ref str, ref imageInfos, (Func<ImageExtractor.ImageInfo, (string, string)>) null, (Func<(string, string), ImageExtractor.ImageInfo>) null);
    }
    else
    {
      ICollection<ImageExtractor.ImageInfo> imageInfos;
      imageExtractor.Extract(exchangeMessage.Body, ref str, ref imageInfos, (Func<ImageExtractor.ImageInfo, (string, string)>) (img => ("~/Frames/GetFile.ashx?fileID=" + img.ID.ToString(), img.Name)), (Func<(string, string), ImageExtractor.ImageInfo>) (item =>
      {
        FileDto fileDto = files.Find((Predicate<FileDto>) (i => string.Equals(i.ContentId, item.cid, StringComparison.OrdinalIgnoreCase)));
        return fileDto == null ? (ImageExtractor.ImageInfo) null : new ImageExtractor.ImageInfo(fileDto.FileId, fileDto.Content, fileDto.FullName, fileDto.ContentId);
      }));
    }
    msg.Body = str;
    return msg;
  }

  [Obsolete("Don't use this method directly from application code.")]
  public virtual bool PersisMessage(Guid? refNoteID, int? bAccountID, int? contactID)
  {
    return this.PersistMessageDefault(refNoteID, bAccountID, contactID);
  }

  public virtual void ExternalRedirect(PXRedirectRequiredException ex, bool popup = false, string append = null)
  {
    StringBuilder stringBuilder = new StringBuilder();
    string companyName = PXAccess.GetCompanyName();
    PXSiteMapNode siteMapNode = PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, ex.Graph.GetType());
    if (siteMapNode != null && siteMapNode.ScreenID != null && HttpContext.Current != null && HttpContext.Current.Request != null)
    {
      string str1 = HttpContext.Current.Request.GetWebsiteUrl().TrimEnd('/') + PXUrl.ToAbsoluteUrl(popup ? siteMapNode.Url : "~/Main");
      stringBuilder.Append("?");
      if (!string.IsNullOrEmpty(companyName))
      {
        stringBuilder.Append("CompanyID=" + HttpUtility.UrlEncode(companyName));
        stringBuilder.Append("&");
      }
      stringBuilder.Append("ScreenId=" + siteMapNode.ScreenID);
      PXGraph graph = ex.Graph;
      PXView view = graph.Views[graph.PrimaryView];
      object current = view.Cache.Current;
      List<string> stringList = new List<string>();
      foreach (string key in (IEnumerable<string>) view.Cache.Keys)
      {
        object obj = view.Cache.GetValue(current, key);
        if (obj != null)
          stringList.Add($"{key}={HttpUtility.UrlEncode(obj.ToString())}");
      }
      if (stringList.Count > 0)
      {
        stringBuilder.Append("&");
        stringBuilder.Append(string.Join("&", stringList.ToArray()));
      }
      string str2 = str1 + stringBuilder.ToString();
      if (append != null)
        str2 += append;
      throw new PXRedirectToUrlException(str2, (PXBaseRedirectException.WindowMode) 2, string.Empty);
    }
  }

  public static void ParseDisplayName(
    string displayName,
    out string firstName,
    out string lastName)
  {
    firstName = (string) null;
    lastName = (string) null;
    displayName = displayName.Trim();
    while (displayName.IndexOf("  ") > -1)
      displayName = displayName.Replace("  ", " ");
    string[] strArray = displayName.Split(' ');
    firstName = strArray.Length > 1 ? strArray[0] : (string) null;
    lastName = strArray.Length > 1 ? strArray[strArray.Length - 1] : strArray[0];
  }

  private string GetEwsAccessToken()
  {
    OUMessage current1 = ((PXSelectBase<OUMessage>) this.SourceMessage).Current;
    if (current1 != null)
    {
      string token = current1.Token;
      if (token != null)
      {
        bool? isOidcFirstRun = current1.IsOidcFirstRun;
        if (isOidcFirstRun.HasValue && !isOidcFirstRun.GetValueOrDefault() && !string.IsNullOrWhiteSpace(token))
        {
          this.Logger.Verbose("Old token returned");
          return token;
        }
      }
    }
    TimeSpan timeSpan = TimeSpan.FromMinutes(1.0);
    OUMessage current2 = ((PXSelectBase<OUMessage>) this.SourceMessage).Current;
    if (current2 != null)
    {
      string oboToken = current2.OboToken;
      if (oboToken != null)
      {
        DateTime? oboTokenExpiresOn = current2.OboTokenExpiresOn;
        if (oboTokenExpiresOn.HasValue)
        {
          DateTime valueOrDefault = oboTokenExpiresOn.GetValueOrDefault();
          if (!string.IsNullOrWhiteSpace(oboToken) && valueOrDefault <= DateTime.UtcNow + timeSpan)
          {
            this.Logger.Verbose("OBO Token is relevant");
            return oboToken;
          }
        }
      }
    }
    OUMessage current3 = ((PXSelectBase<OUMessage>) this.SourceMessage).Current;
    if (current3 != null)
    {
      string newToken = current3.Token;
      if (newToken != null)
      {
        bool? isOidcFirstRun = current3.IsOidcFirstRun;
        if (isOidcFirstRun.HasValue && isOidcFirstRun.GetValueOrDefault())
        {
          if (!string.IsNullOrWhiteSpace(newToken))
          {
            try
            {
              this.Logger.Information("OBO Token is expired or not obtained. Requesting new token.");
              DateTime utcNow = DateTime.UtcNow;
              (string, int) tuple = ((ILongOperationManager) ((PXGraph) this).LongOperationManager).RunSynchronously<(string, int)>((Func<CancellationToken, Task<(string, int)>>) (ct => this.OutlookAuthService.RequestOboAccessTokenAsync(newToken, ct)), true);
              ((PXSelectBase<OUMessage>) this.SourceMessage).Current.OboToken = tuple.Item1;
              ((PXSelectBase<OUMessage>) this.SourceMessage).Current.OboTokenExpiresOn = new DateTime?(utcNow + TimeSpan.FromSeconds((double) tuple.Item2));
              ((PXSelectBase<OUMessage>) this.SourceMessage).UpdateCurrent();
              return tuple.Item1;
            }
            catch (Exception ex)
            {
              this.Logger.Error(ex, "Failed to obtain OBO token");
              throw;
            }
          }
        }
      }
    }
    this.Logger.Error("Cannot obtain OBO token, access token is empty");
    throw new PXInvalidOperationException("The access token is missing. The on-behalf-of flow of the OAuth 2.0 authentication cannot be initialized.");
  }

  private static IEnumerable<XElement> GetAttachmentsFromExchangeServerUsingEWS(
    string ewsUrl,
    string token,
    AttachmentDetails detail)
  {
    return detail == null ? (IEnumerable<XElement>) null : OUSearchMaint.GetAttachmentsFromExchangeServerUsingEWS(ewsUrl, token, detail.Id);
  }

  private static IEnumerable<XElement> GetAttachmentsFromExchangeServerUsingEWS(
    string ewsUrl,
    string token,
    string attachmentDetailId)
  {
    if (ewsUrl == null || token == null)
      return (IEnumerable<XElement>) null;
    XElement exchange = OUSearchMaint.SendRequestToExchange(ewsUrl, token, $"<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\r\nxmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"\r\nxmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"\r\nxmlns:t=\"http://schemas.microsoft.com/exchange/services/2006/types\">\r\n<soap:Header>\r\n<t:RequestServerVersion Version=\"Exchange2013\" />\r\n</soap:Header>\r\n  <soap:Body>\r\n    <GetAttachment xmlns=\"http://schemas.microsoft.com/exchange/services/2006/messages\"\r\n    xmlns:t=\"http://schemas.microsoft.com/exchange/services/2006/types\">\r\n      <AttachmentShape/>\r\n      <AttachmentIds>\r\n        <t:AttachmentId Id=\"{attachmentDetailId}\"/>\r\n      </AttachmentIds>\r\n    </GetAttachment>\r\n  </soap:Body>\r\n</soap:Envelope>");
    return exchange == null ? (IEnumerable<XElement>) null : OUSearchMaint.GetAttachments(exchange);
  }

  private static ExchangeMessage GetExchangeMessage(OUMessage ouMessage, string ewsToken)
  {
    XElement exchange = OUSearchMaint.SendRequestToExchange(ouMessage.EwsUrl, ewsToken, $"<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<soap:Envelope\r\n  xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\r\n  xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"\r\n  xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"\r\n  xmlns:t=\"http://schemas.microsoft.com/exchange/services/2006/types\">\r\n<soap:Header>\r\n<t:RequestServerVersion Version=\"Exchange2007_SP1\" />\r\n</soap:Header>\r\n  <soap:Body>\r\n    <GetItem\r\n\t  xmlns = \"http://schemas.microsoft.com/exchange/services/2006/messages\"\r\n\t  xmlns:t=\"http://schemas.microsoft.com/exchange/services/2006/types\">\r\n      <ItemShape>\r\n        <t:BaseShape>Default</t:BaseShape>\r\n        <t:IncludeMimeContent>false</t:IncludeMimeContent>\r\n\t\t<t:BodyType>HTML</t:BodyType>\r\n      </ItemShape>\r\n      <ItemIds>\r\n        <t:ItemId Id = \"{ouMessage.ItemId}\" />\r\n      </ItemIds>\r\n    </GetItem>\r\n  </soap:Body>\r\n</soap:Envelope>");
    if (exchange == null)
      return (ExchangeMessage) null;
    foreach (XElement xelement in exchange.Descendants((XName) "{http://schemas.microsoft.com/exchange/services/2006/messages}ResponseCode").Select<XElement, XElement>((Func<XElement, XElement>) (errorCode => errorCode)))
    {
      if (xelement.Value != "NoError")
        throw new OUSearchMaint.ExchangeReceiveEmailFailedException("Error occurred: {0}", new object[1]
        {
          (object) xelement.Value
        });
    }
    ExchangeMessage exchangeMessage = new ExchangeMessage(ouMessage.EwsUrl, ewsToken);
    using (IEnumerator<XElement> enumerator = exchange.Descendants((XName) "{http://schemas.microsoft.com/exchange/services/2006/types}Message").Select<XElement, XElement>((Func<XElement, XElement>) (item => item)).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        XElement current = enumerator.Current;
        exchangeMessage.Body = current.Element((XName) "{http://schemas.microsoft.com/exchange/services/2006/types}Body").Value;
        string s = current.Element((XName) "{http://schemas.microsoft.com/exchange/services/2006/types}DateTimeCreated").Value;
        exchangeMessage.StartDate = new DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse(s).ToUniversalTime(), LocaleInfo.GetTimeZone()));
      }
    }
    IEnumerable<XElement> source = exchange.Descendants((XName) "{http://schemas.microsoft.com/exchange/services/2006/types}FileAttachment").Select<XElement, XElement>((Func<XElement, XElement>) (fileAttachment => fileAttachment));
    if (source == null || source.Count<XElement>() == 0)
      return exchangeMessage;
    List<AttachmentDetails> attachmentDetailsList = new List<AttachmentDetails>();
    foreach (XElement xelement1 in source)
    {
      AttachmentDetails attachmentDetails = new AttachmentDetails();
      attachmentDetails.Id = xelement1.Element((XName) "{http://schemas.microsoft.com/exchange/services/2006/types}AttachmentId").Attribute((XName) "Id").Value;
      XElement xelement2 = xelement1.Element((XName) "{http://schemas.microsoft.com/exchange/services/2006/types}Name");
      attachmentDetails.Name = xelement2 == null ? (string) null : xelement2.Value;
      XElement xelement3 = xelement1.Element((XName) "{http://schemas.microsoft.com/exchange/services/2006/types}ContentType");
      attachmentDetails.ContentType = xelement3 == null ? (string) null : xelement3.Value;
      XElement xelement4 = xelement1.Element((XName) "{http://schemas.microsoft.com/exchange/services/2006/types}ContentId");
      attachmentDetails.ContentId = xelement4 == null ? (string) null : xelement4.Value;
      attachmentDetailsList.Add(attachmentDetails);
    }
    exchangeMessage.Attachments = attachmentDetailsList.ToArray();
    return exchangeMessage;
  }

  private static IEnumerable<XElement> GetAttachments(XElement responseEnvelope)
  {
    foreach (XElement xelement in responseEnvelope.Descendants((XName) "{http://schemas.microsoft.com/exchange/services/2006/messages}ResponseCode").Select<XElement, XElement>((Func<XElement, XElement>) (errorCode => errorCode)))
    {
      if (xelement.Value != "NoError")
        return (IEnumerable<XElement>) null;
    }
    return responseEnvelope.Descendants((XName) "{http://schemas.microsoft.com/exchange/services/2006/types}FileAttachment").Select<XElement, XElement>((Func<XElement, XElement>) (fileAttachment => fileAttachment));
  }

  private static byte[] GetFileData(XElement attachment)
  {
    return Convert.FromBase64String(attachment.Element((XName) "{http://schemas.microsoft.com/exchange/services/2006/types}Content").Value);
  }

  private static bool TryParseAttachment(CRSMEmail msg, XElement attachment, out FileDto file)
  {
    byte[] fileData = OUSearchMaint.GetFileData(attachment);
    if (fileData != null)
    {
      XElement xelement1 = attachment.Element((XName) "{http://schemas.microsoft.com/exchange/services/2006/types}Name");
      XElement xelement2 = attachment.Element((XName) "{http://schemas.microsoft.com/exchange/services/2006/types}ContentId");
      ref FileDto local = ref file;
      Guid? nullable = msg.NoteID;
      Guid entityId = nullable.Value;
      string name = xelement1.Value;
      byte[] content = fileData;
      string str = xelement2?.Value;
      nullable = new Guid?();
      Guid? fileId = nullable;
      string contentId = str;
      FileDto fileDto = new FileDto(entityId, name, content, fileId, contentId);
      local = fileDto;
      return true;
    }
    file = (FileDto) null;
    return false;
  }

  private static void ThrowOnNullOrWhiteSpace(string value, string paramName)
  {
    if (value == null)
      throw new ArgumentNullException(paramName);
    if (value == string.Empty)
      throw new ArgumentException(paramName);
  }

  private static XElement SendRequestToExchange(string url, string token, string soapRequest)
  {
    OUSearchMaint.ThrowOnNullOrWhiteSpace(url, nameof (url));
    OUSearchMaint.ThrowOnNullOrWhiteSpace(token, nameof (token));
    OUSearchMaint.ThrowOnNullOrWhiteSpace(soapRequest, nameof (soapRequest));
    try
    {
      HttpWebRequest http = WebRequest.CreateHttp(url);
      http.Headers.Add("Authorization", $"Bearer {token}");
      http.PreAuthenticate = true;
      http.UseDefaultCredentials = true;
      http.AllowAutoRedirect = false;
      http.Method = "POST";
      http.ContentType = "text/xml; charset=utf-8";
      byte[] bytes = Encoding.UTF8.GetBytes(soapRequest);
      http.ContentLength = (long) bytes.Length;
      using (Stream requestStream = http.GetRequestStream())
        requestStream.Write(bytes, 0, bytes.Length);
      using (HttpWebResponse response = (HttpWebResponse) http.GetResponse())
      {
        if (response.StatusCode == HttpStatusCode.OK)
        {
          using (Stream responseStream = response.GetResponseStream())
            return XElement.Load(responseStream);
        }
        throw new OUSearchMaint.ExchangeReceiveEmailFailedException("Error occurred: {0}", new object[1]
        {
          (object) response.StatusDescription
        });
      }
    }
    catch (WebException ex1)
    {
      string str = (string) null;
      HttpStatusCode? nullable = new HttpStatusCode?();
      try
      {
        using (WebResponse response = ex1.Response)
        {
          nullable = new HttpStatusCode?(((HttpWebResponse) response).StatusCode);
          using (Stream responseStream = response.GetResponseStream())
          {
            using (StreamReader streamReader = new StreamReader(responseStream))
              str = streamReader.ReadToEnd();
          }
        }
      }
      catch (Exception ex2)
      {
        PXTrace.Logger.ForContext<OUSearchMaint>().Error(ex2, "Failed to read response from EWS");
      }
      PXTrace.Logger.ForContext<OUSearchMaint>().ForContext("Body", (object) str, false).ForContext("ResponseCode", (object) nullable, false).Error((Exception) ex1, "Failed to connect to EWS");
      throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("The system cannot execute a web request.\nStatus code: {0}\nResponse: {1}", new object[2]
      {
        (object) nullable,
        (object) str
      }));
    }
  }

  protected T TryDoAndLogIfExchangeReceiveFailed<T>(Func<T> action)
  {
    try
    {
      return action();
    }
    catch (OUSearchMaint.ExchangeReceiveEmailFailedException ex)
    {
      ((PXSelectBase<OUSearchEntity>) this.Filter).Current.ErrorMessage = ((Exception) ex).Message;
      return default (T);
    }
  }

  [Serializable]
  public class ExchangeReceiveEmailFailedException : PXException
  {
    public ExchangeReceiveEmailFailedException()
    {
    }

    public ExchangeReceiveEmailFailedException(string message)
      : base(message)
    {
    }

    public ExchangeReceiveEmailFailedException(string format, params object[] args)
      : base(format, args)
    {
    }

    public ExchangeReceiveEmailFailedException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    public ExchangeReceiveEmailFailedException(
      Exception innerException,
      string format,
      params object[] args)
      : base(innerException, format, args)
    {
    }

    public ExchangeReceiveEmailFailedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }

  public abstract class CRSMEmailPreparer
  {
    public abstract void PrepareNewEmail(CRSMEmail email);

    public abstract void PrepareExistingEmail(CRSMEmail email);
  }

  public class DefaultCRSMEmailPreparer : OUSearchMaint.CRSMEmailPreparer
  {
    private readonly Guid? _refNoteID;
    private readonly int? _bAccountID;
    private readonly int? _contactID;
    private readonly string _outgoingEmail;

    public DefaultCRSMEmailPreparer(
      Guid? refNoteID,
      int? bAccountID,
      int? contactID,
      string outgoingEmail)
    {
      this._refNoteID = refNoteID;
      this._bAccountID = bAccountID;
      this._contactID = contactID;
      this._outgoingEmail = outgoingEmail;
    }

    public override void PrepareExistingEmail(CRSMEmail email)
    {
      email.RefNoteID = this._refNoteID;
      email.BAccountID = this._bAccountID;
      email.ContactID = this._contactID;
    }

    public override void PrepareNewEmail(CRSMEmail email)
    {
      email.MailFrom = this._outgoingEmail;
      email.RefNoteID = this._refNoteID;
      email.BAccountID = this._bAccountID;
      email.ContactID = this._contactID;
    }
  }

  public sealed class CustomerModuleOUOpportunityExtension : PXCacheExtension<OUOpportunity>
  {
    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.customerModule>();

    [PXDefault(typeof (Coalesce<Search<CRContactClass.targetOpportunityClassID, Where<CRContactClass.classID, Equal<Current<PX.Objects.CR.Contact.classID>>>>, Search<CRSetup.defaultOpportunityClassID>>))]
    [PXMergeAttributes]
    public string ClassID { get; set; }

    public abstract class classID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      OUSearchMaint.CustomerModuleOUOpportunityExtension.classID>
    {
    }
  }

  public class CustomerModuleOUSearchMaintExtension : PXGraphExtension<OUSearchMaint>
  {
    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.customerModule>();

    protected virtual void OUOpportunity_CurrencyID_FieldDefaulting(
      PXCache sender,
      PXFieldDefaultingEventArgs e)
    {
      if (((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current == null || string.IsNullOrEmpty(((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current.CuryID))
        return;
      e.NewValue = (object) ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current.CuryID;
      ((CancelEventArgs) e).Cancel = true;
    }
  }
}
