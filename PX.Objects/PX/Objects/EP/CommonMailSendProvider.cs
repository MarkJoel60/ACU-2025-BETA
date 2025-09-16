// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.CommonMailSendProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Mail;
using PX.Data;
using PX.Data.EP;
using PX.Data.Wiki.Parser;
using PX.Objects.CR;
using PX.Objects.CS.Email;
using PX.SM;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

#nullable disable
namespace PX.Objects.EP;

public sealed class CommonMailSendProvider : IMailSendProvider
{
  private readonly ILogger _logger;

  public CommonMailSendProvider(ILogger logger) => this._logger = logger;

  public void Send(int accountId)
  {
    new PXGraph().SelectTimeStamp();
    if (MailAccountManager.IsMailProcessingOff)
      throw new PXException("Mail processing is turned off.");
    using (CommonMailSendProvider.MessageProcessor messageProcessor = new CommonMailSendProvider.MessageProcessor(new int?(accountId), this._logger))
      messageProcessor.ProcessAll();
  }

  public void SendMessage(object message)
  {
    if (message == null)
      throw new ArgumentNullException(nameof (message), PXMessages.LocalizeNoPrefix(Messages.Message));
    SMEmail message1 = message is SMEmail ? message as SMEmail : throw new ArgumentException(PXMessages.LocalizeFormatNoPrefixNLA("The system cannot process the '{0}' message. The expected type is {1}.", new object[2]
    {
      (object) message.GetType().Name,
      (object) typeof (SMEmail).Name
    }), PXMessages.LocalizeNoPrefix(Messages.Message));
    int? mailAccountId = message1.MailAccountID;
    if (mailAccountId.HasValue)
    {
      mailAccountId = message1.MailAccountID;
      if (PXEmailSyncHelper.IsExchange(mailAccountId.Value))
      {
        PXEmailSyncHelper.SendMessage(PXResultset<CRSMEmail>.op_Implicit(PXSelectBase<CRSMEmail, PXSelect<CRSMEmail, Where<CRSMEmail.noteID, Equal<Required<SMEmail.refNoteID>>>>.Config>.Select(new PXGraph(), new object[1]
        {
          (object) message1.RefNoteID
        })));
        return;
      }
    }
    using (CommonMailSendProvider.MessageProcessor messageProcessor = new CommonMailSendProvider.MessageProcessor(message1.MailAccountID, this._logger))
      messageProcessor.Process(message1);
  }

  private class AttachmentCollection : IEnumerable<Attachment>, IEnumerable
  {
    private readonly List<CommonMailSendProvider.AttachmentCollection.File> _items = new List<CommonMailSendProvider.AttachmentCollection.File>();
    private readonly PXGraph _graph;

    public AttachmentCollection(PXGraph graph) => this._graph = graph;

    public void Add(Guid id, string name, byte[] data)
    {
      if (this._items.Any<CommonMailSendProvider.AttachmentCollection.File>((Func<CommonMailSendProvider.AttachmentCollection.File, bool>) (e => e.Id == id)))
        return;
      this._items.Add(new CommonMailSendProvider.AttachmentCollection.File(id, name, data));
    }

    public void Add(Guid id)
    {
      if (this._items.Any<CommonMailSendProvider.AttachmentCollection.File>((Func<CommonMailSendProvider.AttachmentCollection.File, bool>) (e => e.Id == id)))
        return;
      UploadFile uploadFile = this.ReadFile(new Guid?(id));
      if (uploadFile == null)
        return;
      string[] strArray = uploadFile.Name.Split('\\');
      string name = strArray[strArray.Length - 1].Replace('/', '_').Replace('\\', '_');
      this._items.Add(new CommonMailSendProvider.AttachmentCollection.File(id, name, uploadFile.Data));
    }

    public static string CreateLink(Guid id) => "cid:" + id.ToString();

    public bool ResizeImage(Guid id, int width, int height)
    {
      bool flag = false;
      foreach (CommonMailSendProvider.AttachmentCollection.File file1 in this._items)
      {
        if (file1.Id == id)
        {
          byte[] data = Drawing.ScaleImageFromBytes(file1.Data, width, height);
          if (data != null)
          {
            CommonMailSendProvider.AttachmentCollection.File file2 = new CommonMailSendProvider.AttachmentCollection.File(file1.Id, file1.Name, data);
            this._items.Remove(file1);
            this._items.Add(file2);
            flag = true;
            break;
          }
          break;
        }
      }
      return flag;
    }

    private UploadFile ReadFile(Guid? id)
    {
      PXResult<UploadFile, UploadFileRevision> pxResult = (PXResult<UploadFile, UploadFileRevision>) PXResultset<UploadFile>.op_Implicit(PXSelectBase<UploadFile, PXSelectJoin<UploadFile, InnerJoin<UploadFileRevision, On<UploadFile.fileID, Equal<UploadFileRevision.fileID>, And<UploadFile.lastRevisionID, Equal<UploadFileRevision.fileRevisionID>>>>, Where<UploadFile.fileID, Equal<Required<UploadFile.fileID>>>>.Config>.Select(this._graph, new object[1]
      {
        (object) id
      }));
      if (pxResult == null)
        return (UploadFile) null;
      UploadFile uploadFile = (UploadFile) ((PXResult) pxResult)[typeof (UploadFile)];
      UploadFileRevision uploadFileRevision = (UploadFileRevision) ((PXResult) pxResult)[typeof (UploadFileRevision)];
      if (uploadFile != null && uploadFileRevision != null)
        uploadFile.Data = uploadFileRevision.Data;
      return uploadFile;
    }

    public IEnumerator<Attachment> GetEnumerator()
    {
      foreach (CommonMailSendProvider.AttachmentCollection.File file in this._items)
      {
        Attachment attachment = new Attachment((Stream) new MemoryStream(file.Data), file.Name, file.Extension);
        attachment.ContentId = file.Id.ToString();
        yield return attachment;
      }
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    private sealed class File
    {
      private readonly Guid _id;
      private readonly string _name;
      private readonly byte[] _data;
      private string _ext;

      public File(Guid id, string name, byte[] data)
      {
        this._id = id;
        this._name = name;
        this._data = data;
      }

      public Guid Id => this._id;

      public string Name => this._name;

      public byte[] Data => this._data;

      public string Extension
      {
        get => this._ext ?? (this._ext = MimeTypes.GetMimeType(PXPath.GetExtension(this._name)));
      }
    }
  }

  private class MessageProcessor : IDisposable
  {
    private const string _FILEID_REGEX_GROUP = "fileid";
    private const string _SRC_REGEX_GROUP = "src";
    private const string _SRC_ATT_PREFIX = "src=\"";
    private const string _SRC_ATT_POSTFIX = "\"";
    private static readonly Regex _imagesRegex = new Regex("<img [^<>]*src=\"(?<src>[^<>\"]*?)/getfile.ashx\\?([^<>\"]*&)*fileid=(?<fileid>[^\\.<>\"&]*?)(\\.[^<>&\"]{1,}){0,1}(&[^<>\"]*)*\"[^<>]*>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.Singleline);
    private static readonly Regex _imagesRegexnewRTE = new Regex("<img [^<>]* src=\"([^<>]*)\">", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.Singleline);
    private readonly PXGraph _graph;
    private EMailAccount _account;
    private MailSender _mailer;
    private ILogger _logger;

    public MessageProcessor(int? accountID, ILogger logger)
    {
      this._graph = (PXGraph) PXGraph.CreateInstance<MailSendProcessingGraph>();
      this._graph.SelectTimeStamp();
      this.ReadAccount(accountID);
      PXSelectBase<PreferencesEmail, PXSelect<PreferencesEmail>.Config>.Clear(this._graph);
      this._logger = logger.ForContext<CommonMailSendProvider.MessageProcessor>();
    }

    public void ProcessAll()
    {
      bool flag = true;
      foreach (PXResult<SMEmail> pxResult in PXSelectBase<SMEmail, PXSelect<SMEmail, Where<SMEmail.mailAccountID, Equal<Required<SMEmail.mailAccountID>>, And<SMEmail.isIncome, NotEqual<True>, And<SMEmail.mpstatus, Equal<MailStatusListAttribute.preProcess>>>>>.Config>.SelectWindowed(this._graph, 0, this._account.SendGroupMails.GetValueOrDefault(), new object[1]
      {
        (object) this._account.EmailAccountID
      }))
      {
        SMEmail message = PXResult<SMEmail>.op_Implicit(pxResult);
        this.Process(message);
        if (flag && message.MPStatus == "FL")
          flag = false;
      }
      if (!flag)
        throw new PXException("At least one item has not been processed.");
    }

    public void Process(SMEmail message)
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        if (!this.PreProcessMessage(message))
          return;
        try
        {
          this.ProcessMessage(message);
          this.PostProcessMessage(message);
        }
        catch (Exception ex)
        {
          this._logger.Error<Guid?, string, int?>(ex, "Sending email {NoteID} {MessageID} for account {EmailAccountID} failed", message.NoteID, message.MessageId, message.MailAccountID);
          this._graph.Clear();
          if (message == null || this._graph.Caches[((object) message).GetType()].GetStatus((object) message) == 2)
            return;
          message = PXResultset<SMEmail>.op_Implicit(PXSelectBase<SMEmail, PXSelect<SMEmail, Where<SMEmail.noteID, Equal<Required<SMEmail.noteID>>>>.Config>.SelectWindowed(this._graph, 0, 1, new object[1]
          {
            (object) message.NoteID
          }));
          if (message != null)
          {
            SMEmail copy = (SMEmail) this._graph.Caches[((object) message).GetType()].CreateCopy((object) message);
            copy.Exception = ex.Message;
            copy.MPStatus = "FL";
            this.UpdateMessage(copy);
            this._graph.Caches[((object) message).GetType()].RestoreCopy((object) message, (object) copy);
          }
        }
        transactionScope.Complete();
      }
    }

    private void ReadAccount(int? accountID)
    {
      int? accountId = accountID;
      EMailAccount emailAccount = (EMailAccount) null;
      if (accountId.HasValue)
        emailAccount = this.ReadAccountSettings(accountId);
      if (emailAccount == null)
        emailAccount = this.ReadDefaultAccountSettings();
      if (emailAccount == null)
        throw new PXException("The email account is empty. Please define the Default Email Account in Email Preferences.");
      if (!emailAccount.IsActive.GetValueOrDefault())
        throw new PXException("The email account {0} ({1}) is inactive.", new object[2]
        {
          (object) emailAccount.Description,
          (object) emailAccount.Address
        });
      if (string.IsNullOrEmpty(emailAccount.Address.With<string, string>((Func<string, string>) (_ => _.Trim()))))
        throw new PXException("Email account address is empty");
      MailSender sender = MailAccountManager.GetSender(emailAccount);
      if (sender == null)
        throw new PXException("The email account is empty. Please define the Default Email Account in Email Preferences.");
      this._account = emailAccount;
      this._mailer = sender;
    }

    private bool PreProcessMessage(SMEmail message)
    {
      try
      {
        if (!this.TryUpdatedEmailStatusAndAcquireDbLock(message))
        {
          this._logger.Warning<Guid?, int?>("Cannot update status of email {NoteID} of email account {EmailAccountID}.", message.NoteID, message.MailAccountID);
          return false;
        }
        SMEmail copy = (SMEmail) this._graph.Caches[((object) message).GetType()].CreateCopy((object) message);
        copy.MPStatus = "IP";
        copy.Exception = (string) null;
        if (copy.MessageId == null)
          copy.MessageId = $"<{Guid.NewGuid().ToString()}_acumatica@{this._account.OutcomingHostName}>";
        this.UpdateMessage(copy);
        this._graph.Caches[((object) message).GetType()].RestoreCopy((object) message, (object) copy);
        return true;
      }
      catch (Exception ex)
      {
        this._logger.Error<Guid?, int?>(ex, "Cannot update status of email {NoteID} of email account {EmailAccountID} in PreProcess.", message.NoteID, message.MailAccountID);
        return false;
      }
    }

    private bool TryUpdatedEmailStatusAndAcquireDbLock(SMEmail message)
    {
      return PXDatabase.Update<SMEmail>(new PXDataFieldParam[4]
      {
        (PXDataFieldParam) new PXDataFieldAssign(typeof (SMEmail.mpstatus).Name, (object) "IP"),
        (PXDataFieldParam) new PXDataFieldRestrict(typeof (SMEmail.noteID).Name, (object) message.NoteID),
        (PXDataFieldParam) new PXDataFieldRestrict(typeof (SMEmail.mpstatus).Name, (object) "PP")
        {
          OpenBrackets = 1
        },
        (PXDataFieldParam) new PXDataFieldRestrict(typeof (SMEmail.mpstatus).Name, (object) "FL")
        {
          CloseBrackets = 1,
          OrOperator = true
        }
      });
    }

    private void PostProcessMessage(SMEmail message)
    {
      PXDatabase.Update<CRActivity>(new PXDataFieldParam[3]
      {
        (PXDataFieldParam) new PXDataFieldAssign<CRActivity.startDate>((object) PXTimeZoneInfo.UtcNow),
        (PXDataFieldParam) new PXDataFieldAssign<CRActivity.uistatus>(message.Exception == null ? (object) "CD" : (object) "OP"),
        (PXDataFieldParam) new PXDataFieldRestrict<CRActivity.noteID>((PXDbType) 14, new int?(16 /*0x10*/), (object) message.RefNoteID, (PXComp) 0)
      });
      if (message.Exception == null)
        PXUpdate<Set<PMTimeActivity.approvalStatus, Switch<Case<Where<PMTimeActivity.approverID, IsNull>, ActivityStatusListAttribute.completed>, ActivityStatusListAttribute.pendingApproval>>, PMTimeActivity, Where<PMTimeActivity.refNoteID, Equal<Required<PMTimeActivity.refNoteID>>, And<PMTimeActivity.isCorrected, Equal<Zero>>>>.Update(this._graph, new object[1]
        {
          (object) message.RefNoteID
        });
      if (message.Exception == null)
      {
        message.RetryCount = new int?(0);
        message.MPStatus = "PD";
      }
      else
      {
        SMEmail smEmail = message;
        int? retryCount = smEmail.RetryCount;
        smEmail.RetryCount = retryCount.HasValue ? new int?(retryCount.GetValueOrDefault() + 1) : new int?();
        if (!message.Exception.StartsWith("5"))
        {
          retryCount = message.RetryCount;
          int? repeatOnErrorSending = MailAccountManager.GetEmailPreferences().RepeatOnErrorSending;
          if (!(retryCount.GetValueOrDefault() >= repeatOnErrorSending.GetValueOrDefault() & retryCount.HasValue & repeatOnErrorSending.HasValue))
          {
            message.MPStatus = "PP";
            goto label_8;
          }
        }
        message.RetryCount = new int?(0);
        message.MPStatus = "FL";
      }
label_8:
      this.UpdateMessage(message);
      this._logger.Verbose<Guid?, int?>("Processed email {NoteID} for account {EmailAccountID}", message.NoteID, message.MailAccountID);
    }

    private void ProcessMessage(SMEmail message)
    {
      try
      {
        this.SendMail(message);
        message.Exception = (string) null;
        this._logger.Verbose<Guid?, int?>("Sent email {NoteID} for account {EmailAccountID}", message.NoteID, message.MailAccountID);
      }
      catch (OutOfMemoryException ex)
      {
        throw;
      }
      catch (StackOverflowException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        message.Exception = ex.Message;
      }
    }

    private void UpdateMessage(SMEmail message)
    {
      System.Type type = ((object) message).GetType();
      PXCache cach = this._graph.Caches[type];
      message.tstamp = PXDatabase.SelectTimeStamp();
      message = (SMEmail) cach.Update((object) message);
      GraphHelper.EnsureCachePersistence(this._graph, type);
      object obj = this._graph.Caches[((object) message).GetType()].Locate((object) message);
      this._graph.Persist();
      this._graph.SelectTimeStamp();
      message = (SMEmail) cach.CreateCopy(obj);
    }

    private string ReadTemplateAttachments(SMEmail message)
    {
      if (message.Body != null && message.Body.IndexOf("embedded=\"true\"") > -1)
      {
        Regex regex1 = new Regex("(<(img data-convert=\"view\")[^<>]*(src=\"([^\"]*)\" ([^<>]*)>))");
        Regex regex2 = new Regex("(<img[^<>]*src=(\"[^<>]*GetFile.*;file=([^\"]*)\") ([^<>]*)>)");
        List<string> stringList = new List<string>();
        List<int> intList1 = new List<int>();
        List<int> intList2 = new List<int>();
        string body = message.Body;
        foreach (Match match in regex1.Matches(body))
        {
          if (match.Groups[4].Value != null && match.Groups[1].Value.IndexOf("embedded=\"true\"") > -1)
          {
            foreach (PXResult pxResult in PXSelectBase<UploadFile, PXSelectJoin<UploadFile, InnerJoin<UploadFileRevision, On<UploadFile.fileID, Equal<UploadFileRevision.fileID>, And<UploadFile.lastRevisionID, Equal<UploadFileRevision.fileRevisionID>>>>, Where<UploadFile.name, Equal<Required<UploadFile.name>>>>.Config>.SelectWindowed(new PXGraph(), 0, 1, new object[1]
            {
              (object) HttpUtility.UrlDecode(match.Groups[4].Value)
            }))
            {
              if (pxResult[typeof (UploadFileRevision)] is UploadFileRevision uploadFileRevision)
              {
                string newValue = $"<img title=\"\" src=\"data:image/jpeg;base64,{Convert.ToBase64String(uploadFileRevision.Data)}\">";
                message.Body = message.Body.Replace(match.Groups[1].Value, newValue);
              }
            }
          }
        }
        foreach (Match match in regex2.Matches(message.Body))
        {
          if (match.Groups[3].Value != null && match.Groups[1].Value.IndexOf("embedded=\"true\"") > -1)
          {
            foreach (PXResult pxResult in PXSelectBase<UploadFile, PXSelectJoin<UploadFile, InnerJoin<UploadFileRevision, On<UploadFile.fileID, Equal<UploadFileRevision.fileID>, And<UploadFile.lastRevisionID, Equal<UploadFileRevision.fileRevisionID>>>>, Where<UploadFile.name, Equal<Required<UploadFile.name>>>>.Config>.SelectWindowed(new PXGraph(), 0, 1, new object[1]
            {
              (object) HttpUtility.UrlDecode(match.Groups[3].Value)
            }))
            {
              if (pxResult[typeof (UploadFileRevision)] is UploadFileRevision uploadFileRevision)
              {
                string newValue = $"<img title=\"\" src=\"data:image/jpeg;base64,{Convert.ToBase64String(uploadFileRevision.Data)}\">";
                message.Body = message.Body.Replace(match.Groups[1].Value, newValue);
              }
            }
          }
        }
      }
      return (string) null;
    }

    private string ExtractInlineImages(
      SMEmail message,
      CommonMailSendProvider.AttachmentCollection fs)
    {
      string inlineImages;
      ICollection<ImageExtractor.ImageInfo> imageInfos;
      if (message.Body == null || !new ImageExtractor().Extract(message.Body, ref inlineImages, ref imageInfos, (Func<ImageExtractor.ImageInfo, (string, string)>) null, (Func<(string, string), ImageExtractor.ImageInfo>) null))
        return message.Body;
      foreach (ImageExtractor.ImageInfo imageInfo in (IEnumerable<ImageExtractor.ImageInfo>) imageInfos)
        fs.Add(imageInfo.ID, imageInfo.Name, imageInfo.Bytes);
      return inlineImages;
    }

    private void SendMail(SMEmail message)
    {
      CommonMailSendProvider.AttachmentCollection attachmentCollection = this.ReadAttachments(message);
      this.ReadTemplateAttachments(message);
      string inlineImages = this.ExtractInlineImages(message, attachmentCollection);
      string str = this._account?.ReplyAddress?.Trim();
      string reply = string.IsNullOrEmpty(str) ? message.MailReply : str;
      string mailFrom = message.MailFrom;
      string from;
      if (mailFrom == null)
        from = (string) null;
      else
        from = mailFrom.TrimEnd(';');
      string messageId = message.MessageId;
      MailSender.MessageAddressee addressee = new MailSender.MessageAddressee(message.MailTo, reply, message.MailCc, message.MailBcc);
      MailSender.MessageContent content = new MailSender.MessageContent(this.GenerateSubject(message), message.Format == null || message.Format == "H", this.GenerateBody(inlineImages, attachmentCollection));
      SMEmail payload = message;
      this._mailer.Send(new MailSender.MailMessageT(from, messageId, addressee, content, (object) payload), attachmentCollection.ToArray<Attachment>());
    }

    private EMailAccount ReadDefaultAccountSettings()
    {
      PXSelectBase<PreferencesEmail, PXSelect<PreferencesEmail>.Config>.Clear(this._graph);
      return this.ReadAccountSettings(PXResultset<PreferencesEmail>.op_Implicit(PXSelectBase<PreferencesEmail, PXSelect<PreferencesEmail>.Config>.SelectWindowed(this._graph, 0, 1, Array.Empty<object>())).With<PreferencesEmail, int?>((Func<PreferencesEmail, int?>) (_ => _.DefaultEMailAccountID)));
    }

    private EMailAccount ReadAccountSettings(int? accountId)
    {
      if (!accountId.HasValue)
        return (EMailAccount) null;
      PXSelectBase<EMailAccount, PXSelect<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Required<EMailAccount.emailAccountID>>>>.Config>.Clear(this._graph);
      return PXResultset<EMailAccount>.op_Implicit(PXSelectBase<EMailAccount, PXSelect<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Required<EMailAccount.emailAccountID>>>>.Config>.Select(this._graph, new object[1]
      {
        (object) accountId
      }));
    }

    private string GenerateBody(string content, CommonMailSendProvider.AttachmentCollection fs)
    {
      string input = content ?? string.Empty;
      MatchCollection matchCollection1 = CommonMailSendProvider.MessageProcessor._imagesRegex.Matches(input);
      MatchCollection matchCollection2 = CommonMailSendProvider.MessageProcessor._imagesRegexnewRTE.Matches(input);
      if (matchCollection1.Count > 0)
      {
        StringBuilder stringBuilder = new StringBuilder();
        int startIndex = 0;
        foreach (Match match in matchCollection1)
        {
          Group group1 = match.Groups["fileid"];
          Group group2 = match.Groups["src"];
          Guid id;
          if (GUID.TryParse(group1.Value, ref id))
          {
            fs.Add(id);
            stringBuilder.Append(input.Substring(startIndex, group2.Index - startIndex));
            stringBuilder.Append(CommonMailSendProvider.AttachmentCollection.CreateLink(id));
            stringBuilder.Append("\"");
            startIndex = input.IndexOf("\"", group1.Index + group1.Length) + "\"".Length;
          }
          else
          {
            int num = group2.Index + group2.Length + "\"".Length;
            stringBuilder.Append(input.Substring(startIndex, num - startIndex));
            startIndex = num;
          }
        }
        if (startIndex < input.Length - 1)
          stringBuilder.Append(input.Substring(startIndex));
        input = stringBuilder.ToString();
      }
      else if (matchCollection2.Count > 0)
      {
        StringBuilder stringBuilder = new StringBuilder();
        int startIndex = 0;
        foreach (Match match in matchCollection2)
        {
          Group group3 = match.Groups[1];
          Group group4 = match.Groups[1];
          foreach (PXResult pxResult in PXSelectBase<UploadFile, PXSelect<UploadFile, Where<UploadFile.name, Equal<Required<UploadFile.name>>>>.Config>.SelectWindowed(new PXGraph(), 0, 1, new object[1]
          {
            (object) HttpUtility.UrlDecode(group3.Value)
          }))
          {
            if (pxResult[typeof (UploadFile)] is UploadFile uploadFile)
            {
              Guid id = uploadFile.FileID.Value;
              fs.Add(id);
              stringBuilder.Append(input.Substring(startIndex, group4.Index - startIndex));
              stringBuilder.Append(CommonMailSendProvider.AttachmentCollection.CreateLink(id));
              stringBuilder.Append("\"");
              startIndex = input.IndexOf("\"", group3.Index + group3.Length) + "\"".Length;
            }
            else
            {
              int num = group4.Index + group4.Length + "\"".Length;
              stringBuilder.Append(input.Substring(startIndex, num - startIndex));
              startIndex = num;
            }
          }
        }
        if (startIndex < input.Length - 1)
          stringBuilder.Append(input.Substring(startIndex));
        input = stringBuilder.ToString();
      }
      return input;
    }

    private string GenerateSubject(SMEmail message)
    {
      string subject = message.Subject;
      if (!message.Ticket.HasValue)
        message.Ticket = message.ID;
      if (message.Ticket.HasValue)
      {
        EMailAccount emailAccount = PXResultset<EMailAccount>.op_Implicit(PXSelectBase<EMailAccount, PXSelect<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Required<EMailAccount.emailAccountID>>>>.Config>.Select(this._graph, new object[1]
        {
          (object) message.MailAccountID
        }));
        if (emailAccount != null && emailAccount.AddIncomingProcessingTags.GetValueOrDefault())
        {
          string str = this.EncodeTicket(message.ID.GetValueOrDefault());
          subject = $"{subject} {str}";
        }
      }
      return subject;
    }

    private string EncodeTicket(int id)
    {
      string str1 = "[";
      string str2 = "]";
      foreach (PXResult<PreferencesEmail> pxResult in PXSelectBase<PreferencesEmail, PXSelect<PreferencesEmail>.Config>.Select(this._graph, Array.Empty<object>()))
      {
        PreferencesEmail preferencesEmail = PXResult<PreferencesEmail>.op_Implicit(pxResult);
        if (preferencesEmail.DefaultEMailAccountID.HasValue)
        {
          str1 = preferencesEmail.EmailTagPrefix;
          str2 = preferencesEmail.EmailTagSuffix;
        }
      }
      return str1 + id.ToString() + str2;
    }

    private CommonMailSendProvider.AttachmentCollection ReadAttachments(SMEmail message)
    {
      CommonMailSendProvider.AttachmentCollection attachmentCollection = new CommonMailSendProvider.AttachmentCollection(this._graph);
      foreach (PXResult<NoteDoc> pxResult in PXSelectBase<NoteDoc, PXSelect<NoteDoc, Where<NoteDoc.fileID, IsNotNull, And<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>>>>.Config>.Select(this._graph, new object[1]
      {
        (object) message.RefNoteID
      }))
      {
        NoteDoc noteDoc = PXResult<NoteDoc>.op_Implicit(pxResult);
        attachmentCollection.Add(noteDoc.FileID.Value);
      }
      return attachmentCollection;
    }

    public void Dispose() => this._mailer.Dispose();
  }
}
