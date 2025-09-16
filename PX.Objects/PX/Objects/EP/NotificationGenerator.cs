// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.NotificationGenerator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Mail;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Objects.EP;

public class NotificationGenerator
{
  private static readonly Regex _HtmlRegex = new Regex("<(.|\\n)*?>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
  private List<Guid> _attachmentLinks = new List<Guid>();
  private List<FileInfo> _newAttachments = new List<FileInfo>();
  private readonly PXGraph _graph;
  private int? _owner;
  private bool _ownerSet;
  protected string _localeName;

  public NotificationGenerator()
    : this(new PXGraph())
  {
  }

  public NotificationGenerator(PXGraph graph, AccessInfo accessinfo = null)
  {
    this._graph = graph;
    this._graph.Caches[typeof (AccessInfo)].Current = (object) (accessinfo ?? this._graph.Accessinfo);
    this.MassProcessMode = true;
    if (!string.IsNullOrEmpty(PXContext.GetScreenID()))
      return;
    PXContext.SetScreenID(PXSiteMap.Provider.FindSiteMapNodeByGraphType(typeof (CREmailActivityMaint).FullName)?.ScreenID);
  }

  public string BodyFormat { get; set; }

  public string Body { get; set; }

  public string Subject { get; set; }

  public string From { get; set; }

  public string Bcc { get; set; }

  public string Cc { get; set; }

  public string To { get; set; }

  public int? MailAccountId { get; set; }

  public string Reply { get; set; }

  public Guid? RefNoteID { get; set; }

  public Guid? DocumentNoteID { get; set; }

  public int? BAccountID { get; set; }

  public int? ContactID { get; set; }

  public Guid? ParentNoteID { get; set; }

  public string ActivityType { get; set; }

  public bool HasAttachments => this._attachmentLinks.Any<Guid>();

  public bool? CreateAsDraft { get; set; } = new bool?(false);

  public int? Owner
  {
    get
    {
      return this._ownerSet ? this._owner : this._owner ?? EmployeeMaint.GetCurrentOwnerID(this._graph);
    }
    set
    {
      this._owner = value;
      this._ownerSet = true;
    }
  }

  public IEnumerable<NotificationRecipient> Watchers { get; set; }

  public Guid? AttachmentsID { get; set; }

  public bool MassProcessMode { get; set; }

  public void AddAttachment(string name, byte[] content)
  {
    this.AddAttachment(name, content, (string) null);
  }

  public void AddAttachment(string name, byte[] content, string cid)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    if (content == null)
      throw new ArgumentNullException(nameof (content));
    Guid uid = string.IsNullOrEmpty(cid) ? Guid.NewGuid() : new Guid(cid);
    this._newAttachments.Add(new FileInfo(uid, $"{uid.ToString()}\\{name}", (string) null, content));
    this._attachmentLinks.Add(uid);
  }

  public void AddAttachmentLink(Guid Value) => this._attachmentLinks.Add(Value);

  public IEnumerable<CRSMEmail> Send()
  {
    IEnumerable<CRSMEmail> messages1 = this.CreateMessages();
    if (!(messages1 is CRSMEmail[] crsmEmailArray))
      crsmEmailArray = messages1.ToArray<CRSMEmail>();
    CRSMEmail[] messages2 = crsmEmailArray;
    this.PersistMessages((IEnumerable<CRSMEmail>) messages2);
    return (IEnumerable<CRSMEmail>) messages2;
  }

  public void Send(IEnumerable<CRSMEmail> messages)
  {
    if (!(messages is CRSMEmail[] messages1))
      messages1 = messages.ToArray<CRSMEmail>();
    this.PersistMessages((IEnumerable<CRSMEmail>) messages1);
  }

  public IEnumerable<SMEmail> CastToSMEmail(IEnumerable<CRSMEmail> crsmemails)
  {
    return PXSelectBase<SMEmail, PXViewOf<SMEmail>.BasedOn<SelectFromBase<SMEmail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<SMEmail.refNoteID, IBqlGuid>.IsIn<P.AsGuid>>>.ReadOnly.Config>.Select(this._graph, (object) crsmemails.Select<CRSMEmail, Guid?>((Func<CRSMEmail, Guid?>) (m => m.NoteID)).ToArray<Guid?>()).RowCast<SMEmail>();
  }

  public IEnumerable<CRSMEmail> MailMessages() => this.CreateMessages();

  public List<FileInfo> GetAttachments() => this._newAttachments;

  public void SetAttachments(List<FileInfo> attachmentsList)
  {
    this._newAttachments = attachmentsList;
  }

  public List<Guid> GetAttachmentLinks() => this._attachmentLinks;

  public void SetAttachmentLinks(List<Guid> attachmentLinks)
  {
    this._attachmentLinks = attachmentLinks;
  }

  private PXGraph Graph => this._graph;

  protected PXGraph GetEmailGraph() => this._graph;

  protected void PersistMessages(IEnumerable<CRSMEmail> messages)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (this._newAttachments.Count > 0)
      {
        UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
        instance.IgnoreFileRestrictions = true;
        foreach (FileInfo newAttachment in this._newAttachments)
        {
          instance.SaveFile(newAttachment);
          if (!newAttachment.UID.HasValue)
            throw new Exception(string.Format("Cannot save file '{0}'" + newAttachment.Name));
        }
      }
      foreach (CRSMEmail message in messages)
      {
        try
        {
          PXCache cach = this.Graph.Caches[message.GetType()];
          if (this._attachmentLinks.Count > 0)
            PXNoteAttribute.SetFileNotes(cach, (object) message, this._attachmentLinks.ToArray());
          PXDBDefaultAttribute.SetSourceType<CRSMEmail.refNoteID>(cach, (object) message, (System.Type) null);
          cach.PersistInserted((object) message);
        }
        catch (Exception ex)
        {
          if (!this.MassProcessMode)
            throw;
          PXTrace.WriteInformation(ex);
        }
      }
      this.Graph.Caches<CRActivityStatistics>().Persist(PXDBOperation.Insert);
      this.Graph.Caches<CRActivityStatistics>().Persist(PXDBOperation.Update);
      transactionScope.Complete();
    }
  }

  protected IEnumerable<CRSMEmail> CreateMessages()
  {
    List<CRSMEmail> messages = new List<CRSMEmail>();
    CRSMEmail message = this.CreateMessage();
    messages.Add(message);
    if (this.Watchers != null && this.Watchers.Any<NotificationRecipient>())
    {
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      foreach (NotificationRecipient watcher in this.Watchers)
      {
        switch (watcher.AddTo)
        {
          case "C":
            str2 = PXDBEmailAttribute.AppendAddresses(str2, watcher.Email);
            continue;
          case "B":
            str3 = PXDBEmailAttribute.AppendAddresses(str3, watcher.Email);
            continue;
          default:
            str1 = PXDBEmailAttribute.AppendAddresses(str1, watcher.Email);
            continue;
        }
      }
      message.MailTo = NotificationGenerator.MergeAddressList(message, str1, message.MailTo);
      message.MailCc = NotificationGenerator.MergeAddressList(message, str2, message.MailCc);
      message.MailBcc = NotificationGenerator.MergeAddressList(message, str3, message.MailBcc);
    }
    for (int index = messages.Count - 1; index >= 0; --index)
    {
      CRSMEmail crsmEmail = messages[index];
      if (!crsmEmail.MailAccountID.HasValue)
      {
        this.Graph.Caches[typeof (CRSMEmail)].Delete((object) crsmEmail);
        messages.RemoveAt(index);
        if (!this.MassProcessMode && messages.Count == 0)
          throw new Exception("Create message failed. Email account should be defined.");
        PXTrace.WriteInformation("Create message failed. Email account should be defined.");
      }
      if (string.IsNullOrEmpty(crsmEmail.MailTo) && string.IsNullOrEmpty(crsmEmail.MailCc) && string.IsNullOrEmpty(crsmEmail.MailBcc))
      {
        this.Graph.Caches[typeof (CRSMEmail)].Delete((object) crsmEmail);
        messages.RemoveAt(index);
        if (!this.MassProcessMode && messages.Count == 0)
          throw new PXInvalidOperationException("Create message failed. Email recipient should be defined.");
        PXTrace.WriteInformation("Create message failed. Email recipient should be defined.");
      }
    }
    return (IEnumerable<CRSMEmail>) messages;
  }

  protected virtual CRSMEmail CreateMessage() => this.CreateMessage(false);

  protected CRSMEmail CreateMessage(bool isTemplate)
  {
    PXCache cach = this.Graph.Caches[typeof (CRSMEmail)];
    CRSMEmail email = (CRSMEmail) cach.Insert();
    email.ClassID = new int?(4);
    email.OwnerID = this.Owner;
    email.StartDate = new DateTime?(PXTimeZoneInfo.Now);
    email.BAccountID = this.BAccountID;
    if (!email.ContactID.HasValue)
      email.ContactID = this.ContactID;
    email.RefNoteID = this.RefNoteID;
    email.DocumentNoteID = this.DocumentNoteID;
    email.ParentNoteID = this.ParentNoteID;
    email.Body = this.Body;
    if (!string.IsNullOrEmpty(this.ActivityType))
      email.Type = this.ActivityType;
    int? nullable = this.MailAccountId ?? MailAccountManager.DefaultMailAccountID;
    email.MailAccountID = nullable;
    EMailAccount emailAccount = (EMailAccount) PXSelectBase<EMailAccount, PXSelect<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Required<EMailAccount.emailAccountID>>>>.Config>.Select(this._graph, (object) nullable);
    if (emailAccount != null)
      email.MailFrom = !(emailAccount.SenderDisplayNameSource == "A") ? $"{TextUtils.QuoteString(emailAccount.Description)} <{emailAccount.Address}>" : (string.IsNullOrEmpty(emailAccount.AccountDisplayName) ? emailAccount.Address : new MailAddress(emailAccount.Address, emailAccount.AccountDisplayName).ToString());
    if (!isTemplate)
    {
      email.MailTo = NotificationGenerator.MergeAddressList(email, this.To, email.MailTo);
      email.MailCc = NotificationGenerator.MergeAddressList(email, this.Cc, email.MailCc);
      email.MailBcc = NotificationGenerator.MergeAddressList(email, this.Bcc, email.MailBcc);
      email.MailReply = string.IsNullOrEmpty(this.Reply) ? email.MailFrom : this.Reply;
      email.Subject = this.Subject;
      email.Body = this.BodyFormat == null || this.BodyFormat == "H" ? NotificationGenerator.CreateHtmlBody(this.Body) : NotificationGenerator.CreateTextBody(this.Body);
    }
    email.IsIncome = new bool?(false);
    bool? createAsDraft = this.CreateAsDraft;
    if (createAsDraft.HasValue && createAsDraft.GetValueOrDefault())
    {
      email.UIStatus = "DR";
      email.MPStatus = "DR";
    }
    else
    {
      email.UIStatus = "OP";
      email.MPStatus = "PP";
    }
    email.Format = this.BodyFormat ?? "H";
    if (this.AttachmentsID.HasValue)
    {
      foreach (PXResult<NoteDoc> pxResult in PXSelectBase<NoteDoc, PXSelect<NoteDoc, Where<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>>>.Config>.Select(this.Graph, (object) this.AttachmentsID))
      {
        NoteDoc noteDoc = (NoteDoc) pxResult;
        Guid? fileId = noteDoc.FileID;
        if (fileId.HasValue)
        {
          List<Guid> attachmentLinks1 = this._attachmentLinks;
          fileId = noteDoc.FileID;
          Guid guid1 = fileId.Value;
          if (!attachmentLinks1.Contains(guid1))
          {
            List<Guid> attachmentLinks2 = this._attachmentLinks;
            fileId = noteDoc.FileID;
            Guid guid2 = fileId.Value;
            attachmentLinks2.Add(guid2);
          }
        }
      }
    }
    return (CRSMEmail) cach.Update((object) email);
  }

  private static bool IsHtml(string text)
  {
    return !string.IsNullOrEmpty(text) && NotificationGenerator._HtmlRegex.IsMatch(text);
  }

  private static string CreateHtmlBody(string text)
  {
    return !NotificationGenerator.IsHtml(text) ? Tools.ConvertSimpleTextToHtml(text) : text;
  }

  private static string CreateTextBody(string text)
  {
    return !NotificationGenerator.IsHtml(text) ? text : Tools.ConvertHtmlToSimpleText(text);
  }

  private static string GetFirstMail(ref string addressList)
  {
    MailAddress mailAddress;
    if (EmailParser.TryParse(addressList, ref mailAddress))
      return mailAddress.ToString();
    string firstMail = addressList;
    addressList = (string) null;
    return firstMail;
  }

  protected static string MergeAddressList(CRSMEmail email, string addressList, string sourceList)
  {
    if (string.IsNullOrEmpty(addressList))
      return sourceList;
    List<MailAddress> addresses = new List<MailAddress>();
    HashSet<string> stringSet = new HashSet<string>();
    foreach (MailAddress address in EmailParser.ParseAddresses(addressList))
    {
      if (!stringSet.Contains(address.Address) && (email.MailTo == null || !Str.Contains(email.MailTo, address.Address, StringComparison.InvariantCultureIgnoreCase)) && (email.MailCc == null || !Str.Contains(email.MailCc, address.Address, StringComparison.InvariantCultureIgnoreCase)) && (email.MailBcc == null || !Str.Contains(email.MailBcc, address.Address, StringComparison.InvariantCultureIgnoreCase)))
      {
        stringSet.Add(address.Address);
        addresses.Add(address);
      }
    }
    if (addresses.Count == 0)
      return sourceList;
    return !string.IsNullOrEmpty(sourceList) ? PXDBEmailAttribute.AppendAddresses(sourceList, PXDBEmailAttribute.ToString((IEnumerable<MailAddress>) addresses)) : PXDBEmailAttribute.ToString((IEnumerable<MailAddress>) addresses);
  }
}
