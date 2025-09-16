// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.CommonMailReceiveProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.IMAP;
using PX.Common.Mail;
using PX.Common.MIME;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Logging.Sinks.SystemEventsDbSink;
using PX.Mail.Log;
using PX.Mail.Log.Sinks;
using PX.Objects.CR;
using PX.SM;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using SerilogTimings;
using SerilogTimings.Extensions;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#nullable enable
namespace PX.Objects.EP;

public sealed class CommonMailReceiveProvider : 
  IMailReceiveProvider,
  IMessageProccessor,
  IOriginalMailProvider
{
  private static readonly 
  #nullable disable
  Dictionary<string, MailReceiver.Context> _contexts = new Dictionary<string, MailReceiver.Context>();
  private readonly IEmailProcessorsProvider _emailProcessorsProvider;
  private readonly ILogger _logger;
  private readonly IEmailLogProvider _emailLogProvider;
  private readonly IEmailLogCleaner _emailLogCleaner;

  public CommonMailReceiveProvider(
    IEmailProcessorsProvider emailProcessorsProvider,
    ILogger logger,
    IEmailLogProvider emailLogProvider,
    IEmailLogCleaner emailLogCleaner)
  {
    this._emailProcessorsProvider = emailProcessorsProvider;
    this._logger = logger;
    this._emailLogProvider = emailLogProvider;
    this._emailLogCleaner = emailLogCleaner;
  }

  public void Receive(int accountId)
  {
    int num1 = 0;
    int num2 = 0;
    string str = (string) null;
    LogEventLevel enabledLogLevel = PreferencesEmail.emailProcessingLogging.ToEventLevel(MailAccountManager.GetEmailPreferences().EmailProcessingLogging);
    using (Operation operation = LoggerOperationExtensions.OperationAt(this._logger, (LogEventLevel) 0, new LogEventLevel?()).Begin("Receive emails for account {EmailAccountID}", new object[1]
    {
      (object) accountId
    }))
    {
      try
      {
        PXGraph graph = this.GetGraph();
        EMailAccount account = CommonMailReceiveProvider.GetAccount(graph, accountId);
        if (MailAccountManager.IsMailProcessingOff)
          throw new PXException("Mail processing is turned off.");
        bool isImap = account.IncomingHostProtocol.GetValueOrDefault() == 1;
        string serverType = isImap ? "IMAP Server" : "POP3 Server";
        str = account?.Description;
        ILogger mailLogger = this._emailLogProvider.GetLogger();
        MailReceiver.FetchingBehavior valueOrDefault = (MailReceiver.FetchingBehavior) account.FetchingBehavior.GetValueOrDefault();
        MailReceiver.Context context = this.GetContext(accountId, isImap, valueOrDefault);
        using (MailReceiver receiver = CommonMailReceiveProvider.GetReceiver(account))
        {
          foreach (Email email in receiver.Receive(context))
          {
            ++num1;
            if (email.Exception != null || email.Message == null)
            {
              LogEmail(email, new Guid?(), false);
              ++num2;
            }
            else
            {
              string messageId = email.Message?.MessageID;
              Guid? refNoteId;
              if (messageId != null && CommonMailReceiveProvider.IsMessageExists(accountId, messageId, out refNoteId))
              {
                if (CommonMailReceiveProvider.TryUpdateActivity(isImap, accountId, messageId, email.UID))
                  context.ReadUIDs.Add((object) email.UID);
                LogEmail(email, refNoteId, true);
              }
              else
              {
                using (SerilogTimingsExtensions.TimeOperationVerbose(this._logger, "Process email {EmailUID} for {EmailAccountID}", new object[2]
                {
                  (object) email.UID,
                  (object) accountId
                }))
                  refNoteId = CommonMailReceiveProvider.MailProcessor.Process(graph, account, this._emailProcessorsProvider, email);
                context.ReadUIDs.Add((object) email.UID);
                LogEmail(email, refNoteId, false);
              }
            }
          }
        }
        if (num2 > 0)
          GetSystemLoggerFor((LogEventLevel) 4, "Email_EmailsReceivedWithErrors").ForContext("EmailsCount", (object) num1, false).Error<int, int, string>("{ErrorsCount} errors occurred during the receiving of email messages. {SuccessesCount} email messages were received successfully. For details, check the email processing log for the {EmailAccountDescription} email account.", num2, num1 - num2, str);
        else
          GetSystemLoggerFor((LogEventLevel) 2, "Email_EmailsReceivedSuccessfully").Information<int, string>("{EmailsCount} email messages were successfully received. For details, check the email processing log for the {EmailAccountDescription} email account.", num1, str);
        operation.Complete();

        void LogEmail(Email mail, Guid? refNoteId = null, bool emailSkipped = false)
        {
          object[] objArray;
          string str;
          LogEventLevel logEventLevel;
          if (!emailSkipped)
          {
            if (mail.Exception == null)
            {
              objArray = Array.Empty<object>();
              str = "Received an email message";
              logEventLevel = (LogEventLevel) 2;
            }
            else
            {
              objArray = new object[1]
              {
                (object) mail.Exception.Message
              };
              str = "Failed to receive an email message. {Message}";
              logEventLevel = (LogEventLevel) 4;
            }
          }
          else
          {
            objArray = Array.Empty<object>();
            str = "Skipped an email message";
            logEventLevel = (LogEventLevel) 1;
          }
          if (!mailLogger.IsEnabled(logEventLevel))
            return;
          Envelope envelope = mail.Envelope;
          ILogger ilogger;
          if (envelope != null)
          {
            ilogger = mailLogger.ForContext((ILogEventEnricher) new EmailPropertiesEnricher(new int?(accountId), nameof (Receive), serverType, mail.UID, refNoteId, envelope.MessageID, new DateTime?(envelope.Date.ToUniversalTime()), (IEnumerable<PX.Common.Mail.Address>) envelope.From, (IEnumerable<PX.Common.Mail.Address>) envelope.Sender, (IEnumerable<PX.Common.Mail.Address>) envelope.To, (IEnumerable<PX.Common.Mail.Address>) envelope.Cc, (IEnumerable<PX.Common.Mail.Address>) envelope.Bcc));
          }
          else
          {
            Message message = mail.Message;
            if (message != null)
              ilogger = mailLogger.ForContext((ILogEventEnricher) new EmailPropertiesEnricher(new int?(accountId), nameof (Receive), serverType, mail.UID, refNoteId, message.MessageID, new DateTime?(message.Date.ToUniversalTime()), (IEnumerable<PX.Common.Mail.Address>) message.From, (IEnumerable<PX.Common.Mail.Address>) new Mailbox[1]
              {
                message.Sender
              }, (IEnumerable<PX.Common.Mail.Address>) message.To, (IEnumerable<PX.Common.Mail.Address>) message.Cc, (IEnumerable<PX.Common.Mail.Address>) message.Bcc));
            else
              ilogger = mailLogger.ForContext((ILogEventEnricher) new EmailPropertiesEnricher(new int?(accountId), nameof (Receive), serverType, mail.UID, refNoteId, (string) null, new DateTime?(), (IEnumerable<PX.Common.Mail.Address>) null, (IEnumerable<PX.Common.Mail.Address>) null, (IEnumerable<PX.Common.Mail.Address>) null, (IEnumerable<PX.Common.Mail.Address>) null, (IEnumerable<PX.Common.Mail.Address>) null));
          }
          ilogger.Write(logEventLevel, mail.Exception, str, objArray);
        }
      }
      catch (Exception ex)
      {
        GetSystemLoggerFor((LogEventLevel) 4, "Email_EmailsReceivedWithErrors").Error<string>(ex, "An error occurred during the receiving of email messages for the {EmailAccountDescription} email account.", str);
        OperationExtensions.Abandon(operation, ex);
        throw;
      }
      finally
      {
        this._emailLogCleaner.Clean();
      }
    }

    ILogger GetSystemLoggerFor(LogEventLevel level, string eventId)
    {
      ILogger ilogger = this._logger;
      if (level >= enabledLogLevel)
        ilogger = SystemEventHelper.ForSystemEvents(this._logger, "Email", eventId);
      return SystemEventHelper.ForCurrentCompanyContext(ilogger).ForContext("EmailAccountID", (object) accountId, false);
    }
  }

  private MailReceiver.Context GetContext(
    int accountId,
    bool isImap,
    MailReceiver.FetchingBehavior fetchingBehavior)
  {
    MailReceiver.Context context = (MailReceiver.Context) null;
    string key = $"{accountId.ToString()}_{isImap.ToString()}";
    MailReceiver.IReadUIDsCollection readUIDs = CommonMailReceiveProvider.UIDsCache.Get(accountId, isImap);
    lock (CommonMailReceiveProvider._contexts)
    {
      MailReceiver.Context source;
      context = CommonMailReceiveProvider._contexts.TryGetValue(key, out source) ? new MailReceiver.Context(source, readUIDs) : new MailReceiver.Context(readUIDs);
      CommonMailReceiveProvider._contexts[key] = context;
      context.ImapFetchingBehavior = fetchingBehavior;
    }
    return context;
  }

  public void Process(object message)
  {
    if (!(message is SMEmail email))
      throw new ArgumentException(nameof (message));
    PXGraph graph = this.GetGraph();
    CommonMailReceiveProvider.MailProcessor.Process(graph, CommonMailReceiveProvider.GetAccount(graph, email.MailAccountID.Value), this._emailProcessorsProvider, email);
  }

  public void ProcessMessage(EMailAccount account, object message)
  {
    if (!(message is SMEmail email))
      throw new ArgumentException(nameof (message));
    CommonMailReceiveProvider.MailProcessor.Process(this.GetGraph(), account, this._emailProcessorsProvider, email);
  }

  public Email GetMail(object message)
  {
    if (!(message is SMEmail smEmail) || !smEmail.IsIncome.GetValueOrDefault())
      throw new PXException("Invalid email message for this operation");
    PXGraph graph = this.GetGraph();
    EMailAccount emailAccount = smEmail.MailAccountID.With<int, EMailAccount>((Func<int, EMailAccount>) (_ => CommonMailReceiveProvider.GetAccount(graph, _)));
    if (emailAccount == null)
      throw new PXException("Email account is not specified");
    object uid;
    if ((emailAccount.IncomingHostProtocol.GetValueOrDefault() == 1 ? 1 : 0) != 0)
    {
      if (smEmail.ImapUID.HasValue)
      {
        int? imapUid = smEmail.ImapUID;
        int num = 0;
        if (!(imapUid.GetValueOrDefault() < num & imapUid.HasValue))
        {
          uid = (object) smEmail.ImapUID;
          goto label_12;
        }
      }
      throw new PXException("Invalid email message for this operation");
    }
    uid = !string.IsNullOrEmpty(smEmail.Pop3UID) ? (object) smEmail.Pop3UID : throw new PXException("Invalid email message for this operation");
label_12:
    return (MailAccountManager.GetReceiver(emailAccount) ?? throw new PXException("The email account isn't properly configured to receive email.")).ReceiveMail(uid);
  }

  private PXGraph GetGraph()
  {
    MailReceiveProcessingGraph instance = PXGraph.CreateInstance<MailReceiveProcessingGraph>();
    ((PXGraph) instance).SelectTimeStamp();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<CRActivity.workgroupID>(CommonMailReceiveProvider.\u003C\u003Ec.\u003C\u003E9__16_0 ?? (CommonMailReceiveProvider.\u003C\u003Ec.\u003C\u003E9__16_0 = new PXFieldVerifying((object) CommonMailReceiveProvider.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CGetGraph\u003Eb__16_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<CRActivity.uistatus>(CommonMailReceiveProvider.\u003C\u003Ec.\u003C\u003E9__16_1 ?? (CommonMailReceiveProvider.\u003C\u003Ec.\u003C\u003E9__16_1 = new PXFieldVerifying((object) CommonMailReceiveProvider.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CGetGraph\u003Eb__16_1))));
    return (PXGraph) instance;
  }

  private static MailReceiver GetReceiver(EMailAccount account)
  {
    return MailAccountManager.GetReceiver(account) ?? throw new PXException("The email account isn't properly configured to receive email.");
  }

  private static EMailAccount GetAccount(PXGraph graph, int accountId)
  {
    EMailAccount o = PXResultset<EMailAccount>.op_Implicit(PXSelectBase<EMailAccount, PXSelect<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Required<EMailAccount.emailAccountID>>>>.Config>.SelectWindowed(graph, 0, 1, new object[1]
    {
      (object) accountId
    }));
    if (o == null || string.IsNullOrEmpty(o.With<EMailAccount, string>((Func<EMailAccount, string>) (_ => _.Address)).With<string, string>((Func<string, string>) (_ => _.Trim()))) || string.IsNullOrEmpty(o.IncomingHostName) && o.EmailAccountType == "S" || o.IncomingHostProtocol.GetValueOrDefault() == 1 && string.IsNullOrEmpty(o.ImapRootFolder))
      throw new PXException("The email account isn't properly configured to receive email.");
    return o;
  }

  private static bool IsMessageExists(int accountId, string messageId, out Guid? refNoteId)
  {
    using (new PXReadDeletedScope(false))
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<SMEmail>(new PXDataField[3]
      {
        (PXDataField) new PXDataField<SMEmail.refNoteID>(),
        (PXDataField) new PXDataFieldValue<SMEmail.mailAccountID>((object) accountId),
        (PXDataField) new PXDataFieldValue<SMEmail.messageId>((object) messageId)
      }))
      {
        if (pxDataRecord != null)
        {
          refNoteId = pxDataRecord.GetGuid(0);
          return true;
        }
      }
    }
    refNoteId = new Guid?();
    return false;
  }

  private static bool TryUpdateActivity(bool isImap, int accountId, string messageId, string uid)
  {
    return PXDatabase.Update<SMEmail>(new PXDataFieldParam[4]
    {
      (PXDataFieldParam) new PXDataFieldAssign(isImap ? typeof (SMEmail.imapUID).Name : typeof (SMEmail.pop3UID).Name, (object) uid),
      (PXDataFieldParam) new PXDataFieldRestrict(typeof (SMEmail.mailAccountID).Name, (object) accountId),
      (PXDataFieldParam) new PXDataFieldRestrict(typeof (SMEmail.messageId).Name, (object) messageId),
      isImap ? (PXDataFieldParam) new PXDataFieldRestrict(typeof (SMEmail.imapUID).Name, (PXDbType) 8, new int?(4), (object) null, (PXComp) 6) : (PXDataFieldParam) new PXDataFieldRestrict(typeof (SMEmail.pop3UID).Name, (PXDbType) 12, new int?(150), (object) null, (PXComp) 6)
    });
  }

  [PXHidden]
  [Serializable]
  public class UploadFile : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _Name;

    [PXDBGuid(false, IsKey = true)]
    public virtual Guid? FileID { get; set; }

    [PXDBString(InputMask = "", IsUnicode = true)]
    public virtual string Name
    {
      get => this._Name;
      set
      {
        this._Name = value;
        this.ShortName = Str.GetShortName(value);
      }
    }

    [PXDBString(IsUnicode = true)]
    public virtual string ShortName { get; set; }

    [PXDBBool]
    public virtual bool? Versioned { get; set; }

    [PXDBBool]
    public virtual bool? IsPublic { get; set; }

    [PXDBCreatedByID]
    public virtual Guid? CreatedByID { get; set; }

    [PXDBCreatedDateTime]
    public virtual DateTime? CreatedDateTime { get; set; }

    [PXDBInt]
    [PXDefault(0)]
    public virtual int? LastRevisionID { get; set; }

    [PXDBString]
    public virtual string PrimaryScreenID { get; set; }

    [PXDBTimestamp]
    public virtual byte[] tstamp { get; set; }

    [PXNote]
    public virtual Guid? NoteID { get; set; }

    /// <summary>Inherit Access Rights from Entities</summary>
    [PXDBBool]
    [PXDefault(true)]
    public virtual bool? IsAccessRightsFromEntities { get; set; }

    public abstract class fileID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      CommonMailReceiveProvider.UploadFile.fileID>
    {
    }

    public abstract class name : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CommonMailReceiveProvider.UploadFile.name>
    {
    }

    public abstract class shortName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CommonMailReceiveProvider.UploadFile.shortName>
    {
    }

    public abstract class versioned : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CommonMailReceiveProvider.UploadFile.versioned>
    {
    }

    public abstract class isPublic : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CommonMailReceiveProvider.UploadFile.isPublic>
    {
    }

    public abstract class createdByID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      CommonMailReceiveProvider.UploadFile.createdByID>
    {
    }

    public abstract class createdDateTime : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CommonMailReceiveProvider.UploadFile.createdDateTime>
    {
    }

    public abstract class lastRevisionID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CommonMailReceiveProvider.UploadFile.lastRevisionID>
    {
    }

    public abstract class primaryScreenID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CommonMailReceiveProvider.UploadFile.primaryScreenID>
    {
    }

    public abstract class Tstamp : 
      BqlType<
      #nullable enable
      IBqlByteArray, byte[]>.Field<
      #nullable disable
      CommonMailReceiveProvider.UploadFile.Tstamp>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      CommonMailReceiveProvider.UploadFile.noteID>
    {
    }

    public abstract class isAccessRightsFromEntities : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CommonMailReceiveProvider.UploadFile.isAccessRightsFromEntities>
    {
    }
  }

  public sealed class ImageReplacer : Dictionary<string, Guid>
  {
    private static readonly Regex rHref = new Regex("<a href=\\\"cid:.*?>\\s*(?<RefContent>.*?)</a>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.Singleline);
    private static readonly Regex rImg = new Regex("<img\\s+(?<Content>[^>]*?)>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.Singleline);
    private static readonly Regex rContent = new Regex("(?<Item>.*?)\\s*=\\s*(?<Value>(\\\".*?\\\")|(\\d+))\\s*", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.Singleline);

    public string Replace(string source)
    {
      string input = CommonMailReceiveProvider.ImageReplacer.rHref.Replace(source, new MatchEvaluator(this.ReplaceLink));
      return CommonMailReceiveProvider.ImageReplacer.rImg.Replace(input, new MatchEvaluator(this.ReplaceImage));
    }

    private string ReplaceLink(Match refMatch)
    {
      return this.ReplaceImage(CommonMailReceiveProvider.ImageReplacer.rImg.Match(refMatch.Groups["RefContent"].Value));
    }

    private string ReplaceImage(Match match)
    {
      if (!string.IsNullOrEmpty(match.Groups["Content"].Value))
      {
        Guid empty = Guid.Empty;
        int result1 = 0;
        int result2 = 0;
        foreach (Match match1 in CommonMailReceiveProvider.ImageReplacer.rContent.Matches(match.Groups["Content"].Value))
        {
          string s = match1.Groups["Value"].Value;
          if (s.StartsWith("\"") && s.EndsWith("\""))
            s = s.Substring(1, s.Length - 2);
          switch (match1.Groups["Item"].Value.ToLower())
          {
            case "width":
              int.TryParse(s, out result1);
              continue;
            case "height":
              int.TryParse(s, out result2);
              continue;
            case "src":
              if (empty == Guid.Empty && s.Length > 4)
              {
                this.TryGetValue($"<{s.Substring(4)}>", out empty);
                continue;
              }
              continue;
            default:
              continue;
          }
        }
        if (empty != Guid.Empty)
        {
          string str = $"<img src=\"~/Frames/GetFile.ashx?fileID={empty.ToString()}\" ";
          if (result1 != 0)
            str = $"{str}width=\"{result1.ToString()}\" ";
          if (result2 != 0)
            str = $"{str}height=\"{result2.ToString()}\" ";
          return str + " />";
        }
      }
      return match.Value;
    }
  }

  private sealed class MailProcessor
  {
    private static readonly Regex extRegex = new Regex("\\w+", RegexOptions.Multiline | RegexOptions.Compiled);
    private readonly Regex _tRegex;
    private readonly PXGraph _graph;
    private readonly Email _email;
    private readonly EMailAccount _account;
    private CRSMEmail _activityMessage;
    private CRActivity _activity;
    private SMEmail _message;
    private readonly IEmailProcessorsProvider _emailProcessorsProvider;

    private MailProcessor(
      PXGraph graph,
      EMailAccount account,
      IEmailProcessorsProvider emailProcessorsProvider)
    {
      this._graph = ExceptionExtensions.CheckIfNull<PXGraph>(graph, nameof (graph), (string) null);
      this._account = ExceptionExtensions.CheckIfNull<EMailAccount>(account, nameof (account), (string) null);
      this._emailProcessorsProvider = ExceptionExtensions.CheckIfNull<IEmailProcessorsProvider>(emailProcessorsProvider, nameof (emailProcessorsProvider), (string) null);
      string str1 = "\\[";
      string str2 = "\\]";
      foreach (PreferencesEmail preferencesEmail in GraphHelper.RowCast<PreferencesEmail>((IEnumerable) PXSelectBase<PreferencesEmail, PXSelect<PreferencesEmail>.Config>.Select(this._graph, Array.Empty<object>())).Where<PreferencesEmail>((Func<PreferencesEmail, bool>) (curacc => curacc.DefaultEMailAccountID.HasValue)))
      {
        str1 = preferencesEmail.EmailTagPrefix;
        str2 = preferencesEmail.EmailTagSuffix;
        foreach (char ch in "[]()^|?*.-+")
        {
          string newValue = "\\" + ch.ToString();
          str1 = str1.Replace(ch.ToString(), newValue);
          str2 = str2.Replace(ch.ToString(), newValue);
        }
      }
      this._tRegex = new Regex($"{str1}(?<Ticket>\\d+?){str2}", RegexOptions.Multiline | RegexOptions.Compiled);
      PXCache cach = this._graph.Caches[typeof (CRActivityStatistics)];
      this._graph.Views.Caches.Remove(typeof (CRActivityStatistics));
      this._graph.Views.Caches.Add(typeof (CRActivityStatistics));
    }

    private MailProcessor(
      PXGraph graph,
      EMailAccount account,
      IEmailProcessorsProvider emailProcessorsProvider,
      Email email)
      : this(graph, account, emailProcessorsProvider)
    {
      this._email = ExceptionExtensions.CheckIfNull<Email>(email, nameof (email), (string) null);
    }

    private MailProcessor(
      PXGraph graph,
      EMailAccount account,
      IEmailProcessorsProvider emailProcessorsProvider,
      SMEmail message)
      : this(graph, account, emailProcessorsProvider)
    {
      this._message = ExceptionExtensions.CheckIfNull<SMEmail>(message, nameof (message), (string) null);
    }

    /// <summary>Process incoming message.</summary>
    /// <param name="graph"></param>
    /// <param name="account"></param>
    /// <param name="emailProcessorsProvider"></param>
    /// <param name="email"></param>
    /// <returns><see cref="P:PX.Objects.CR.CRSMEmail.NoteID" /> of the created activity or <see langword="null" /> if error happened and activity wasn't created.</returns>
    public static Guid? Process(
      #nullable enable
      PXGraph graph,
      EMailAccount account,
      IEmailProcessorsProvider emailProcessorsProvider,
      Email email)
    {
      return new CommonMailReceiveProvider.MailProcessor(graph, account, emailProcessorsProvider, email).Process();
    }

    /// <summary>Process incoming message.</summary>
    /// <param name="graph"></param>
    /// <param name="account"></param>
    /// <param name="emailProcessorsProvider"></param>
    /// <param name="email"></param>
    /// <returns><see cref="P:PX.Objects.CR.CRSMEmail.NoteID" /> of the created activity or <see langword="null" /> if error happened and activity wasn't created.</returns>
    public static Guid? Process(
      PXGraph graph,
      EMailAccount account,
      IEmailProcessorsProvider emailProcessorsProvider,
      SMEmail email)
    {
      return new CommonMailReceiveProvider.MailProcessor(graph, account, emailProcessorsProvider, email).Process();
    }

    private Guid? Process()
    {
      Guid? noteId;
      using (new PXScreenIDScope("CR306015"))
      {
        try
        {
          if (this.CreateMessage())
            this.ProcessMessage();
        }
        finally
        {
          noteId = (Guid?) this._activityMessage?.NoteID;
          this._graph.Clear();
          this._activity = (CRActivity) null;
          if (this._activityMessage != null)
          {
            this._message.Exception = this._activityMessage.Exception;
            this._message.MPStatus = this._activityMessage.MPStatus;
          }
          this._message = (SMEmail) null;
          this._activityMessage = (CRSMEmail) null;
        }
      }
      return noteId;
    }

    private bool CreateMessage()
    {
      if (this._message != null)
      {
        this._activityMessage = PXResultset<CRSMEmail>.op_Implicit(PXSelectBase<CRSMEmail, PXSelect<CRSMEmail, Where<CRSMEmail.noteID, Equal<Required<SMEmail.refNoteID>>>>.Config>.Select(this._graph, new object[1]
        {
          (object) this._message.RefNoteID
        }));
        return true;
      }
      try
      {
        int? count = this._email?.Message?.From?.Count;
        if (!count.HasValue || count.GetValueOrDefault() <= 0)
          return false;
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          this.CreateActivity();
          this.CreateEmail();
          transactionScope.Complete();
        }
        if (this._message == null)
          return false;
        this._activityMessage = PXResultset<CRSMEmail>.op_Implicit(PXSelectBase<CRSMEmail, PXSelect<CRSMEmail, Where<CRSMEmail.noteID, Equal<Required<SMEmail.refNoteID>>>>.Config>.Select(this._graph, new object[1]
        {
          (object) this._message.RefNoteID
        }));
        this.AppendAttachments();
        this.PersistAM();
        return true;
      }
      catch (Exception ex)
      {
        PXTrace.WriteError(ex);
        this.PersistException(ex);
        return false;
      }
    }

    private void AppendAttachments()
    {
      this.ReplaceInlineImages(this.CreateAttachments(this.AllowedFileExtensions(this._account.IncomingAttachmentType)));
    }

    private void ReplaceInlineImages(
      #nullable disable
      IEnumerable<KeyValuePair<string, Guid>> createAttachments)
    {
      CommonMailReceiveProvider.ImageReplacer imageReplacer = new CommonMailReceiveProvider.ImageReplacer();
      foreach (KeyValuePair<string, Guid> createAttachment in createAttachments)
      {
        if (!imageReplacer.ContainsKey(createAttachment.Key))
          imageReplacer.Add(createAttachment.Key, createAttachment.Value);
      }
      if (imageReplacer.Count <= 0)
        return;
      this._activityMessage.Body = imageReplacer.Replace(this._activityMessage.Body);
      this.UpdateAM();
    }

    private IEnumerable<KeyValuePair<string, Guid>> CreateAttachments(
      ICollection<string> allowedExtensions)
    {
      CommonMailReceiveProvider.MailProcessor mailProcessor1 = this;
      Entity[] entityArray = mailProcessor1._email.Message.Attachments;
      for (int index = 0; index < entityArray.Length; ++index)
      {
        Entity entity = entityArray[index];
        string str1 = ".eml";
        string subject = entity.Body is BodyMessageRfc822 body ? body.Message?.Subject : (string) null;
        Guid guid = Guid.NewGuid();
        string str2 = (entity.ContentDisposition?.Param_FileName ?? entity.ContentType?.Param_Name ?? (subject != null ? subject + str1 : "untitled")).Trim();
        string str3;
        try
        {
          str3 = PXPath.GetExtension(str2);
        }
        catch (ArgumentException ex)
        {
          PXTrace.WriteError(new Exception("Cannot parse extension of the attachment name. Name contains invalid symbols.", (Exception) ex));
          str3 = (string) null;
        }
        if (!string.IsNullOrEmpty(str3))
          str3 = str3.Substring(1).ToLower();
        if (allowedExtensions == null || allowedExtensions.Contains(str3))
        {
          if (!(entity.Body is BodySinglepartBase) && body == null)
          {
            PXTrace.WriteError((Exception) new NullReferenceException("Cannot get content from attachment because its body is null."));
          }
          else
          {
            byte[] numArray = ((Entity) body?.Message)?.ToByte(new EncodingEncodedWord((EncodedWordEncoding) 1, Encoding.UTF8), Encoding.UTF8) ?? (entity.Body as BodySinglepartBase).Data;
            CRSMEmail activityMessage = mailProcessor1._activityMessage;
            Guid? noteId1;
            int num;
            if (activityMessage == null)
            {
              num = 1;
            }
            else
            {
              noteId1 = activityMessage.NoteID;
              num = !noteId1.HasValue ? 1 : 0;
            }
            if (num != 0)
            {
              PXTrace.WriteError((Exception) new NullReferenceException("Cannot get content from attachment because message note ID is null."));
            }
            else
            {
              CommonMailReceiveProvider.MailProcessor mailProcessor2 = mailProcessor1;
              noteId1 = mailProcessor1._activityMessage.NoteID;
              Guid noteId2 = noteId1.Value;
              Guid newFileId = guid;
              string name = str2;
              byte[] content = numArray;
              mailProcessor2.CreateFile(noteId2, newFileId, name, content);
              if (!string.IsNullOrEmpty(entity.ContentID))
                yield return new KeyValuePair<string, Guid>(entity.ContentID, guid);
            }
          }
        }
      }
      entityArray = (Entity[]) null;
    }

    private ICollection<string> AllowedFileExtensions(string allowedTypes)
    {
      string input = allowedTypes.With<string, string>((Func<string, string>) (s => s.Trim().ToLower()));
      List<string> stringList = (List<string>) null;
      if (!(input == "all") && !string.IsNullOrEmpty(input))
      {
        foreach (Match match in CommonMailReceiveProvider.MailProcessor.extRegex.Matches(input))
        {
          if (stringList == null)
            stringList = new List<string>();
          stringList.Add(match.Value.ToLower());
        }
      }
      return (ICollection<string>) stringList;
    }

    private void CreateFile(Guid noteId, Guid newFileId, string name, byte[] content)
    {
      PXCache cach1 = this._graph.Caches[typeof (NoteDoc)];
      NoteDoc instance1 = (NoteDoc) cach1.CreateInstance();
      instance1.NoteID = new Guid?(noteId);
      instance1.FileID = new Guid?(newFileId);
      cach1.Insert((object) instance1);
      GraphHelper.EnsureCachePersistence(this._graph, typeof (NoteDoc));
      PXCache cach2 = this._graph.Caches[typeof (CommonMailReceiveProvider.UploadFile)];
      CommonMailReceiveProvider.UploadFile instance2 = (CommonMailReceiveProvider.UploadFile) cach2.CreateInstance();
      instance2.FileID = new Guid?(newFileId);
      instance2.LastRevisionID = new int?(1);
      instance2.Versioned = new bool?(true);
      instance2.IsPublic = new bool?(false);
      if (name.Length > 200)
        name = name.Substring(0, 200);
      instance2.Name = $"{newFileId.ToString()}\\{name}";
      instance2.PrimaryScreenID = "CR306015";
      cach2.Insert((object) instance2);
      GraphHelper.EnsureCachePersistence(this._graph, typeof (CommonMailReceiveProvider.UploadFile));
      PXCache cach3 = this._graph.Caches[typeof (UploadFileRevision)];
      UploadFileRevision instance3 = (UploadFileRevision) cach3.CreateInstance();
      instance3.FileID = new Guid?(newFileId);
      instance3.FileRevisionID = new int?(1);
      instance3.Data = content;
      instance3.Size = new int?(UploadFileHelper.BytesToKilobytes(content.Length));
      cach3.Insert((object) instance3);
      GraphHelper.EnsureCachePersistence(this._graph, typeof (UploadFileRevision));
    }

    private void PersistAM()
    {
      object obj = this._graph.Caches[((object) this._activityMessage).GetType()].Locate((object) this._activityMessage);
      this._graph.Persist();
      this._graph.SelectTimeStamp();
      this._activityMessage = (CRSMEmail) this._graph.Caches[((object) this._activityMessage).GetType()].CreateCopy(obj);
    }

    private int? DecodeTicket(string str, out string clearStr)
    {
      if (!string.IsNullOrEmpty(str) && this._tRegex.IsMatch(str))
      {
        MatchCollection matchCollection = this._tRegex.Matches(str);
        int result;
        if (int.TryParse(matchCollection[matchCollection.Count - 1].Groups["Ticket"].Value.Trim(), out result))
        {
          clearStr = this._tRegex.Replace(str, string.Empty);
          return new int?(result);
        }
      }
      clearStr = str;
      return new int?();
    }

    private void CreateEmail()
    {
      PXCache cach = this._graph.Caches[typeof (SMEmail)];
      this._message = (SMEmail) cach.CreateCopy(cach.Insert());
      this._message.RefNoteID = this._activity.NoteID;
      this._message.MailAccountID = this._account.EmailAccountID;
      this._message.IsIncome = new bool?(true);
      if (!string.IsNullOrEmpty(this._email.UID))
      {
        if (this._account.IncomingHostProtocol.GetValueOrDefault() == 1)
          this._message.ImapUID = new int?(int.Parse(this._email.UID));
        else
          this._message.Pop3UID = this._email.UID;
      }
      this._message.MPStatus = "PP";
      Message message = this._email.Message;
      this._message.MailFrom = message.From?.ToString() ?? string.Empty;
      this._message.MailReply = message.ReplyTo?.ToString() ?? string.Empty;
      this._message.MailTo = message.To?.ToString() ?? string.Empty;
      this._message.MailCc = message.Cc?.ToString() ?? string.Empty;
      this._message.MailBcc = message.Bcc?.ToString() ?? string.Empty;
      this._message.Subject = message.Subject?.ToString() ?? " ";
      this._message.MessageId = message.MessageID;
      string str = message.BodyHtmlText;
      if (str == null)
      {
        if (message.BodyText != null)
          str = Tools.ConvertSimpleTextToHtml(message.BodyText);
      }
      else
        str = new Regex("<base(.|\\n)*?>").Replace(message.BodyHtmlText.Replace(Environment.NewLine, " ").Replace("<spanstyle", "<span style"), string.Empty);
      this._message.Body = str ?? string.Empty;
      string clearStr;
      int? nullable = this.DecodeTicket(this._message.Subject, out clearStr);
      if (nullable.HasValue)
      {
        this._message.Ticket = new int?(nullable.GetValueOrDefault());
        this._message.Subject = clearStr;
      }
      if (this._email.Exception != null)
        this._message.Exception = this._email.Exception.ToString();
      this._message = (SMEmail) cach.CreateCopy(cach.Update((object) this._message));
      GraphHelper.EnsureCachePersistence(this._graph, ((object) this._message).GetType());
      object obj = this._graph.Caches[((object) this._message).GetType()].Locate((object) this._message);
      this._graph.Persist();
      this._graph.SelectTimeStamp();
      this._message = (SMEmail) cach.CreateCopy(obj);
    }

    private void CreateActivity()
    {
      PXCache cach = this._graph.Caches[typeof (CRActivity)];
      this._activity = (CRActivity) cach.CreateCopy(cach.Insert());
      this._activity.ClassID = new int?(4);
      this._activity.Type = (string) null;
      string clearStr;
      this.DecodeTicket(this._email.Message.Subject, out clearStr);
      this._activity.Subject = clearStr;
      this._activity.StartDate = new DateTime?(this._email.Message.Date == DateTime.MinValue ? PXTimeZoneInfo.Now : PXTimeZoneInfo.ConvertTimeFromUtc(this._email.Message.Date.ToUniversalTime(), LocaleInfo.GetTimeZone()));
      this._activity = (CRActivity) cach.CreateCopy(cach.Update((object) this._activity));
      GraphHelper.EnsureCachePersistence(this._graph, ((object) this._activity).GetType());
      object obj = this._graph.Caches[((object) this._activity).GetType()].Locate((object) this._activity);
      this._graph.Persist();
      this._graph.SelectTimeStamp();
      this._activity = (CRActivity) cach.CreateCopy(obj);
    }

    private void UpdateAM()
    {
      System.Type type = ((object) this._activityMessage).GetType();
      PXCache cach = this._graph.Caches[type];
      this._activityMessage = (CRSMEmail) cach.CreateCopy(cach.Update((object) this._activityMessage));
      GraphHelper.EnsureCachePersistence(this._graph, type);
    }

    private void ProcessMessage()
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        try
        {
          this.PreProcessActivity();
        }
        catch (Exception ex)
        {
          return;
        }
        try
        {
          this.ProcessActivity();
          this.PostProcessActivity();
        }
        catch (Exception ex)
        {
          this.PersistException(ex);
        }
        transactionScope.Complete();
      }
    }

    private void PreProcessActivity()
    {
      this._activityMessage.MPStatus = "IP";
      this._activityMessage.Exception = (string) null;
      this.UpdateAM();
      this.PersistAM();
    }

    private void PostProcessActivity()
    {
      int num = this._graph.Caches[((object) this._activityMessage).GetType()].GetStatus((object) this._activityMessage) == 3 ? 1 : 0;
      if (this._email != null && this._email.Exception != null && this._activityMessage.Exception == null)
        this._activityMessage.Exception = this._email.Exception.Message;
      this._activityMessage.MPStatus = "PD";
      this._activityMessage.UIStatus = "CD";
      this.UpdateAM();
      this.PersistAM();
      if (num == 0)
        return;
      this._graph.Caches[((object) this._activityMessage).GetType()].SetStatus((object) this._activityMessage, (PXEntryStatus) 3);
      this.PersistAM();
    }

    private void PersistException(Exception ex)
    {
      this._graph.Clear();
      if (this._activityMessage == null || this._graph.Caches[((object) this._activityMessage).GetType()].GetStatus((object) this._activityMessage) == 2)
        return;
      this._activityMessage = PXResultset<CRSMEmail>.op_Implicit(PXSelectBase<CRSMEmail, PXSelect<CRSMEmail, Where<CRSMEmail.noteID, Equal<Required<CRSMEmail.noteID>>>>.Config>.SelectWindowed(this._graph, 0, 1, new object[1]
      {
        (object) this._activityMessage.NoteID
      }));
      if (this._activityMessage == null)
        return;
      this._activityMessage = (CRSMEmail) this._graph.Caches[((object) this._activityMessage).GetType()].CreateCopy((object) this._activityMessage);
      this._activityMessage.Exception = ex.Message;
      this._activityMessage.MPStatus = "FL";
      this.UpdateAM();
      this.PersistAM();
    }

    private void ProcessActivity()
    {
      EmailProcessEventArgs e = new EmailProcessEventArgs(this._graph, this._account, this._activityMessage);
      foreach (IEmailProcessor emailProcessor in this._emailProcessorsProvider.GetEmailProcessors())
      {
        try
        {
          emailProcessor.Process(e);
        }
        catch (Exception ex)
        {
          e.IsSuccessful = false;
          this._activityMessage.Exception += ex.Message;
          CRSMEmail activityMessage = this._activityMessage;
          activityMessage.Exception = activityMessage.Exception + Environment.NewLine + this.AddUIErrorsInfo(ex);
        }
      }
      if (this._email != null && this._email.Exception != null)
        this._activityMessage.Exception = this._email.Exception.Message;
      if (this._activityMessage.Exception != null && !this._activityMessage.Exception.Equals(PXMessages.LocalizeNoPrefix("An HTML element with the \"img\" tag cannot be parsed because it has the \"src\" attribute with invalid base64 content.") + "\r\n"))
        throw new PXException(this._activityMessage.Exception);
    }

    private string AddUIErrorsInfo(Exception source)
    {
      if (!(source is PXOuterException pxOuterException) || pxOuterException.Row == null)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      PXCache cach = this._graph.Caches[pxOuterException.Row.GetType()];
      Dictionary<string, string> errors = PXUIFieldAttribute.GetErrors(cach, pxOuterException.Row, Array.Empty<PXErrorLevel>());
      if (errors != null)
      {
        try
        {
          stringBuilder.AppendLine($"=== {PXMessages.LocalizeNoPrefix("Errors")} ===");
          foreach (KeyValuePair<string, string> keyValuePair in errors)
          {
            stringBuilder.Append(keyValuePair.Key);
            stringBuilder.Append(": ");
            stringBuilder.AppendLine(keyValuePair.Value);
          }
          stringBuilder.AppendLine($"=== {PXMessages.LocalizeNoPrefix("Fields")} ===");
          foreach (string field in (List<string>) cach.Fields)
          {
            string str = field;
            if (cach.GetStateExt(pxOuterException.Row, field) is PXFieldState stateExt && !string.IsNullOrWhiteSpace(stateExt.DisplayName))
              str = stateExt.DisplayName;
            stringBuilder.Append(str);
            stringBuilder.Append(": ");
            object obj = cach.GetValue(pxOuterException.Row, field);
            if (obj != null)
              stringBuilder.AppendLine(obj.ToString());
            else
              stringBuilder.AppendLine();
          }
        }
        catch (StackOverflowException ex)
        {
          throw;
        }
        catch (OutOfMemoryException ex)
        {
          throw;
        }
        catch
        {
        }
      }
      return stringBuilder.ToString();
    }
  }

  private class UIDsCache
  {
    private static readonly IDictionary<int, CommonMailReceiveProvider.UIDsCache> _items = (IDictionary<int, CommonMailReceiveProvider.UIDsCache>) new ConcurrentDictionary<int, CommonMailReceiveProvider.UIDsCache>();
    private readonly IList _pop3;
    private readonly IList _imap;
    private int _accountId;
    private DateTime _lastCreated;

    private void Add(object pop3, object imap)
    {
      if (pop3 != null)
        this._pop3.Add((object) pop3.ToString());
      int result;
      if (imap == null || !int.TryParse(imap.ToString(), out result))
        return;
      this._imap.Add((object) result);
    }

    private UIDsCache()
    {
      this._pop3 = (IList) new List<string>();
      this._imap = (IList) new List<int>();
    }

    public static MailReceiver.IReadUIDsCollection Get(int accountId, bool isImap)
    {
      CommonMailReceiveProvider.UIDsCache cache;
      if (!CommonMailReceiveProvider.UIDsCache._items.TryGetValue(accountId, out cache))
      {
        cache = new CommonMailReceiveProvider.UIDsCache();
        cache._accountId = accountId;
        cache._lastCreated = new DateTime(1970, 1, 1);
        CommonMailReceiveProvider.UIDsCache._items[accountId] = cache;
      }
      cache.Prefetch();
      return (MailReceiver.IReadUIDsCollection) new CommonMailReceiveProvider.UIDsCache.UIDsCacheInterface(cache, isImap);
    }

    private void Prefetch()
    {
      int num = 100000;
      PXSelect<SMEmail, Where<SMEmail.mailAccountID, Equal<Required<SMEmail.mailAccountID>>, And<SMEmail.createdDateTime, Greater<Required<SMEmail.createdDateTime>>>>, OrderBy<Asc<SMEmail.createdDateTime>>> pxSelect = new PXSelect<SMEmail, Where<SMEmail.mailAccountID, Equal<Required<SMEmail.mailAccountID>>, And<SMEmail.createdDateTime, Greater<Required<SMEmail.createdDateTime>>>>, OrderBy<Asc<SMEmail.createdDateTime>>>(new PXGraph());
      using (new PXFieldScope(((PXSelectBase) pxSelect).View, new System.Type[5]
      {
        typeof (SMEmail.noteID),
        typeof (SMEmail.mailAccountID),
        typeof (SMEmail.createdDateTime),
        typeof (SMEmail.pop3UID),
        typeof (SMEmail.imapUID)
      }))
      {
        using (new PXReadDeletedScope(false))
        {
          bool flag = true;
          while (flag)
          {
            flag = false;
            foreach (PXResult<SMEmail> pxResult in ((PXSelectBase<SMEmail>) pxSelect).SelectWindowed(0, num, new object[2]
            {
              (object) this._accountId,
              (object) this._lastCreated
            }))
            {
              SMEmail smEmail = PXResult<SMEmail>.op_Implicit(pxResult);
              flag = true;
              DateTime? createdDateTime = smEmail.CreatedDateTime;
              if (createdDateTime.HasValue)
              {
                string pop3Uid = smEmail.Pop3UID;
                int? imapUid = smEmail.ImapUID;
                this._lastCreated = createdDateTime.Value;
                if (!string.IsNullOrEmpty(pop3Uid))
                  this._pop3.Add((object) pop3Uid);
                if (imapUid.HasValue)
                  this._imap.Add((object) imapUid.Value);
              }
            }
          }
        }
      }
    }

    private class ListEnumerator : IEnumerator, IEnumerable
    {
      private readonly IList _items;
      private readonly int _startIndex;
      private int _currentIndex;
      private object _current;

      public ListEnumerator(IList items, int startIndex)
      {
        if (startIndex < 0)
          throw new ArgumentOutOfRangeException(nameof (startIndex));
        this._items = ExceptionExtensions.CheckIfNull<IList>(items, nameof (items), (string) null);
        this._startIndex = startIndex;
        this.Reset();
      }

      public bool MoveNext()
      {
        for (this._current = (object) null; this._current == null; this._current = this._items[this._currentIndex])
        {
          if (this._currentIndex >= this._items.Count - 1)
            return false;
          ++this._currentIndex;
        }
        return true;
      }

      public void Reset()
      {
        this._currentIndex = this._startIndex - 1;
        this.MoveNext();
      }

      public object Current => this._current;

      public IEnumerator GetEnumerator() => (IEnumerator) this;
    }

    private class UIDsCacheInterface : MailReceiver.IReadUIDsCollection
    {
      private readonly CommonMailReceiveProvider.UIDsCache _cache;
      private readonly bool _isImap;

      public UIDsCacheInterface(CommonMailReceiveProvider.UIDsCache cache, bool isImap)
      {
        this._cache = ExceptionExtensions.CheckIfNull<CommonMailReceiveProvider.UIDsCache>(cache, nameof (cache), (string) null);
        this._isImap = isImap;
      }

      public MailReceiver.ReadUIDs Get(int marker)
      {
        IList items = this._isImap ? this._cache._imap : this._cache._pop3;
        return new MailReceiver.ReadUIDs((IEnumerable) new CommonMailReceiveProvider.UIDsCache.ListEnumerator(items, marker), items.Count);
      }

      public void Add(object uid)
      {
        ExceptionExtensions.ThrowOnNull<object>(uid, nameof (uid), (string) null);
        this._cache.Add(this._isImap ? (object) null : uid, this._isImap ? uid : (object) null);
      }
    }
  }
}
