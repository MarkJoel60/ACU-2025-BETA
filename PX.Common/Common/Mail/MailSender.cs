// Decompiled with JetBrains decompiler
// Type: PX.Common.Mail.MailSender
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using CommonServiceLocator;
using PX.Common.MIME;
using PX.Common.Services;
using PX.Common.SMTP.Client;
using PX.Common.TCP;
using PX.Mail;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Hosting;

#nullable disable
namespace PX.Common.Mail;

[Serializable]
public abstract class MailSender : IDisposable
{
  private readonly ConnectionSettings _connectionSettings;
  private readonly CredentialSettings _credentialSettings;
  private readonly string _emailAddress;
  private readonly int _timeout;

  protected MailSender(
    ConnectionSettings connectionSettings,
    CredentialSettings credentialSettings,
    string emailAddress,
    int timeout)
  {
    this._connectionSettings = connectionSettings;
    this._credentialSettings = credentialSettings;
    this._emailAddress = emailAddress;
    this._timeout = timeout;
  }

  public ConnectionSettings Host => this._connectionSettings;

  public CredentialSettings Credential => this._credentialSettings;

  public string EmailAddress => this._emailAddress;

  public int Timeout => this._timeout;

  [Obsolete]
  public static MailSender Create(
    MailSender.Types type,
    ConnectionSettings host,
    CredentialSettings login,
    string emailAddress,
    int timeout)
  {
    switch (type)
    {
      case MailSender.Types.Dummy:
        return (MailSender) new MailSender.DummySmtpSender(host, login, emailAddress, timeout);
      case MailSender.Types.SMTP:
        return (MailSender) new MailSender.SmtpSender(host, login, emailAddress, timeout);
      case MailSender.Types.NativeSMTP:
        return (MailSender) new MailSender.NativeSmtpSender(host, login, emailAddress, timeout);
      case MailSender.Types.File:
        return (MailSender) new MailSender.DebugMailSender();
      default:
        throw new ArgumentOutOfRangeException(nameof (type));
    }
  }

  public abstract void Send(MailSender.MailMessageT message, Attachment[] files);

  public abstract void Test();

  public virtual void Dispose()
  {
  }

  ~MailSender() => this.Dispose();

  /// <summary>
  /// Dumps all attachments to the ~/App_Data/Attachments folder.
  /// </summary>
  [Serializable]
  public class DebugMailSender : MailSender
  {
    public DebugMailSender()
      : base(new ConnectionSettings(), new CredentialSettings(), "test@acumatica.com", 60)
    {
    }

    public override void Send(MailSender.MailMessageT message, Attachment[] files)
    {
      string str = HostingEnvironment.MapPath("~/App_Data/Attachments");
      if (string.IsNullOrEmpty(str))
        return;
      if (!Directory.Exists(str))
        Directory.CreateDirectory(str);
      foreach (Attachment file in files)
      {
        file.ContentStream.Seek(0L, SeekOrigin.Begin);
        string path2 = MainTools.Replace(file.Name, Path.GetInvalidFileNameChars(), '_');
        using (FileStream destination = System.IO.File.Create(Path.Combine(str, path2)))
        {
          file.ContentStream.CopyTo((Stream) destination);
          destination.Flush();
        }
      }
    }

    public override void Test()
    {
    }
  }

  [Serializable]
  public class DummySmtpSender(
    ConnectionSettings host,
    CredentialSettings login,
    string emailAddress,
    int timeout = 60000) : MailSender(host, login, emailAddress, timeout)
  {
    public override void Send(MailSender.MailMessageT msg, Attachment[] files)
    {
    }

    public override void Test()
    {
    }
  }

  [Serializable]
  public class MailMessageT
  {
    private int _hashCode;

    public MailMessageT(
      string from,
      string uid,
      MailSender.MessageAddressee addressee,
      MailSender.MessageContent content,
      object payload = null)
    {
      this.From = from;
      this.UID = uid;
      this.Addressee = addressee;
      this.Content = content;
      this.Payload = payload;
    }

    public MailMessageT(MailSender.MailMessageT source)
    {
      this.From = source.From;
      this.Addressee = source.Addressee;
      this.Content = source.Content;
      this.Payload = source.Payload;
    }

    public virtual bool IsEmpty
    {
      get => string.IsNullOrEmpty(this.From) && this.Addressee.IsEmpty && this.Content.IsEmpty;
    }

    public MailSender.MessageAddressee Addressee { get; }

    public MailSender.MessageContent Content { get; }

    public string From { get; }

    public string UID { get; }

    public object Payload { get; }

    public bool Equals(MailSender.MailMessageT other)
    {
      if (other == null)
        return false;
      if (this == other)
        return true;
      return object.Equals((object) other.From, (object) this.From) && object.Equals((object) other.UID, (object) this.UID) && object.Equals((object) other.Addressee, (object) this.Addressee) && object.Equals((object) other.Content, (object) this.Content);
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if (this == obj)
        return true;
      return !(obj.GetType() != typeof (MailSender.MailMessageT)) && this.Equals((MailSender.MailMessageT) obj);
    }

    public override int GetHashCode()
    {
      if (this._hashCode == 0)
        this._hashCode = (((this.From != null ? this.From.GetHashCode() : 0) * 397 ^ (this.UID != null ? this.UID.GetHashCode() : 0)) * 397 ^ (this.Addressee != null ? this.Addressee.GetHashCode() : 0)) * 397 ^ (this.Content != null ? this.Content.GetHashCode() : 0);
      return this._hashCode;
    }
  }

  [Serializable]
  public sealed class MessageAddressee
  {
    public static readonly MailSender.MessageAddressee Empty = new MailSender.MessageAddressee((string) null, (string) null, (string) null, (string) null);
    private int _hashCode;

    public MessageAddressee(string to, string reply, string cc, string bcc)
    {
      this.To = to;
      this.Reply = reply;
      this.Cc = cc;
      this.Bcc = bcc;
    }

    public MessageAddressee(MailSender.MessageAddressee source)
    {
      this.To = source.To;
      this.Reply = source.Reply;
      this.Cc = source.Cc;
      this.Bcc = source.Bcc;
    }

    public string To { get; }

    public string Reply { get; }

    public string Cc { get; }

    public string Bcc { get; }

    public bool Equals(MailSender.MessageAddressee other)
    {
      if (other == null)
        return false;
      if (this == other)
        return true;
      return object.Equals((object) other.To, (object) this.To) && object.Equals((object) other.Reply, (object) this.Reply) && object.Equals((object) other.Cc, (object) this.Cc) && object.Equals((object) other.Bcc, (object) this.Bcc) && other._hashCode == this._hashCode;
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if (this == obj)
        return true;
      return !(obj.GetType() != typeof (MailSender.MessageAddressee)) && this.Equals((MailSender.MessageAddressee) obj);
    }

    public override int GetHashCode()
    {
      if (this._hashCode == 0)
        this._hashCode = (((this.To != null ? this.To.GetHashCode() : 0) * 397 ^ (this.Reply != null ? this.Reply.GetHashCode() : 0)) * 397 ^ (this.Cc != null ? this.Cc.GetHashCode() : 0)) * 397 ^ (this.Bcc != null ? this.Bcc.GetHashCode() : 0);
      return this._hashCode;
    }

    public bool IsEmpty
    {
      get
      {
        return string.IsNullOrEmpty(this.To) && string.IsNullOrEmpty(this.Reply) && string.IsNullOrEmpty(this.Cc) && string.IsNullOrEmpty(this.Bcc);
      }
    }
  }

  [Serializable]
  public sealed class MessageContent
  {
    public static readonly MailSender.MessageContent Empty = new MailSender.MessageContent((string) null, false, (string) null);
    private int _hashCode;

    public MessageContent(string subject, bool isHtml, string body)
    {
      this.Subject = subject;
      this.IsHtml = isHtml;
      this.Body = body;
    }

    public MessageContent(MailSender.MessageContent source)
    {
      this.Subject = source.Subject;
      this.IsHtml = source.IsHtml;
      this.Body = source.Body;
    }

    public string Subject { get; }

    public string Body { get; }

    public bool IsHtml { get; }

    public bool Equals(MailSender.MessageContent other)
    {
      if (other == null)
        return false;
      if (this == other)
        return true;
      return object.Equals((object) other.Subject, (object) this.Subject) && object.Equals((object) other.Body, (object) this.Body) && other.IsHtml.Equals(this.IsHtml);
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if (this == obj)
        return true;
      return !(obj.GetType() != typeof (MailSender.MessageContent)) && this.Equals((MailSender.MessageContent) obj);
    }

    public override int GetHashCode()
    {
      if (this._hashCode == 0)
        this._hashCode = ((this.Subject != null ? this.Subject.GetHashCode() : 0) * 397 ^ (this.Body != null ? this.Body.GetHashCode() : 0)) * 397 ^ this.IsHtml.GetHashCode();
      return this._hashCode;
    }

    public bool IsEmpty
    {
      get => string.IsNullOrEmpty(this.Subject) && string.IsNullOrEmpty(this.Body) && !this.IsHtml;
    }
  }

  [Serializable]
  public class NativeSmtpSender : MailSender
  {
    private readonly SmtpClient _client;

    public NativeSmtpSender(
      ConnectionSettings host,
      CredentialSettings login,
      string emailAddress,
      int timeout = 60000)
      : base(host, login, emailAddress, timeout)
    {
      this._client = new SmtpClient();
      SmtpClient client1 = this._client;
      ConnectionSettings host1 = this.Host;
      string path = ((ConnectionSettings) ref host1).Path;
      client1.Host = path;
      SmtpClient client2 = this._client;
      ConnectionSettings host2 = this.Host;
      int port = ((ConnectionSettings) ref host2).Port;
      client2.Port = port;
      this._client.Timeout = this._timeout;
      SmtpClient client3 = this._client;
      ConnectionSettings connectionSettings = this._connectionSettings;
      int num = ((ConnectionSettings) ref connectionSettings).Security == 1 ? 1 : 0;
      client3.EnableSsl = num != 0;
      this._client.UseDefaultCredentials = false;
      SmtpClient client4 = this._client;
      CredentialSettings credential = this.Credential;
      string login1 = ((CredentialSettings) ref credential).Login;
      credential = this.Credential;
      string password = ((CredentialSettings) ref credential).Password;
      NetworkCredential networkCredential = new NetworkCredential(login1, password);
      client4.Credentials = (ICredentialsByHost) networkCredential;
      this._client.DeliveryMethod = SmtpDeliveryMethod.Network;
    }

    public override void Send(MailSender.MailMessageT msg, Attachment[] files)
    {
      MailMessage message = new MailMessage();
      message.From = new MailAddress(MailSender.NativeSmtpSender.\u0002(msg.From));
      MailSender.NativeSmtpSender.\u0002(message.To, msg.Addressee.To);
      MailSender.NativeSmtpSender.\u0002(message.CC, msg.Addressee.Cc);
      MailSender.NativeSmtpSender.\u0002(message.Bcc, msg.Addressee.Bcc);
      if (string.IsNullOrEmpty(msg.Addressee.Reply))
        message.ReplyToList.Add(message.From);
      else
        message.ReplyToList.Add(msg.Addressee.Reply);
      message.Subject = msg.Content.Subject;
      message.BodyTransferEncoding = TransferEncoding.Base64;
      message.Body = msg.Content.Body;
      message.IsBodyHtml = msg.Content.IsHtml;
      message.Headers.Add("Date", Utils.DateTimeToRfc2822(DateTime.Now));
      if (files != null && files.Length != 0)
      {
        foreach (Attachment file in files)
          message.Attachments.Add(file);
      }
      try
      {
        this._client.Send(message);
      }
      catch (SmtpException ex)
      {
        throw ex.InnerException == null ? (Exception) ex : ex.InnerException.InnerException ?? ex.InnerException;
      }
    }

    private static void \u0002(MailAddressCollection _param0, string _param1)
    {
      foreach (Mailbox mailbox in (string.IsNullOrEmpty(_param1) ? new AddressList() : AddressList.Parse(_param1)).Mailboxes)
      {
        MailAddress mailAddress = new MailAddress(mailbox.Address, mailbox.DisplayName);
        _param0.Add(mailAddress);
      }
    }

    private static string \u0002(string _param0) => Mailbox.Parse(_param0).Address;

    public override void Test()
    {
      MailMessage message = new MailMessage();
      MailMessage mailMessage = message;
      CredentialSettings credential1 = this.Credential;
      MailAddress mailAddress = new MailAddress(MailSender.NativeSmtpSender.\u0002(((CredentialSettings) ref credential1).Login));
      mailMessage.From = mailAddress;
      MailAddressCollection to = message.To;
      CredentialSettings credential2 = this.Credential;
      string login = ((CredentialSettings) ref credential2).Login;
      MailSender.NativeSmtpSender.\u0002(to, login);
      message.Subject = nameof (Test);
      message.BodyTransferEncoding = TransferEncoding.Base64;
      message.Body = "Test mail account";
      message.IsBodyHtml = false;
      try
      {
        this._client.Send(message);
      }
      catch (SmtpException ex)
      {
        throw ex.InnerException == null ? (Exception) ex : ex.InnerException.InnerException ?? ex.InnerException;
      }
    }

    ~NativeSmtpSender() => this._client.Dispose();
  }

  [Serializable]
  public class SmtpSender(
    ConnectionSettings host,
    CredentialSettings login,
    string emailAddress,
    int timeout = 60000) : MailSender(host, login, emailAddress, timeout)
  {
    private SmtpClientDefinition slot;
    private readonly ILicenseService _licenseService = ServiceLocator.Current.GetInstance<ILicenseService>();

    private SmtpClient \u0002()
    {
      this.slot = PXContext.GetSlot<SmtpClientDefinition>(SmtpClientDefinition._SMTPCLIENT_SLOT_KEY_PREFIX);
      if (this.slot == null || !((TcpSession) this.slot.Client).IsConnected || ((TcpClient) this.slot.Client).IsDisposed)
        this.slot = PXContext.SetSlot<SmtpClientDefinition>(SmtpClientDefinition._SMTPCLIENT_SLOT_KEY_PREFIX, new SmtpClientDefinition((MailSender) this, this._timeout));
      return this.slot.Client;
    }

    public override void Send(MailSender.MailMessageT msg, Attachment[] files)
    {
      Message message1 = new Message();
      message1.From = MailSender.SmtpSender.\u0002(msg.From);
      message1.To = MailSender.SmtpSender.\u0002(msg.Addressee.To);
      message1.Cc = MailSender.SmtpSender.\u0002(msg.Addressee.Cc);
      message1.Bcc = MailSender.SmtpSender.\u0002(msg.Addressee.Bcc);
      if (message1.To.Count + message1.Cc.Count + message1.Bcc.Count == 0)
        throw new MailException("At least one recipient must be specified for this email.");
      if (string.IsNullOrEmpty(msg.Addressee.Reply))
      {
        message1.ReplyTo = MailSender.SmtpSender.\u0002(msg.From);
      }
      else
      {
        Mailbox mailbox = Mailbox.Parse(msg.Addressee.Reply);
        if (string.IsNullOrEmpty(mailbox.DisplayName))
          mailbox = new Mailbox(MailSender.SmtpSender.\u0002(msg.From).DisplayName, mailbox.Address);
        Message message2 = message1;
        AddressList addressList = new AddressList();
        addressList.Add((Address) mailbox);
        message2.ReplyTo = addressList;
      }
      message1.Subject = this._licenseService.PrepareEmailSubject(msg.Content.Subject);
      if (files != null && files.Length != 0)
      {
        BodyMultipartMixed bodyMultipartMixed = new BodyMultipartMixed(new HeaderContentType("multipart/mixed")
        {
          Param_Boundary = Guid.NewGuid().ToString().Replace('-', '_')
        });
        ((Entity) message1).Body = (Body) bodyMultipartMixed;
        Message message3 = new Message();
        BodyText bodyText = new BodyText(msg.Content.IsHtml ? "text/html" : "text/plain");
        ((Entity) message3).Body = (Body) bodyText;
        bodyText.SetText(TransferEncodings.Base64, Encoding.UTF8, msg.Content.Body);
        ((BodyMultipart) bodyMultipartMixed).BodyParts.Add((Entity) message3);
        foreach (Attachment file in files)
        {
          Message message4 = new Message();
          BodyUnknown bodyUnknown = new BodyUnknown(file.ContentType.MediaType);
          ((Entity) message4).Body = (Body) bodyUnknown;
          file.ContentStream.Seek(0L, SeekOrigin.Begin);
          ((BodySinglepartBase) bodyUnknown).SetData(file.ContentStream, MailSender.SmtpSender.\u0002(file.TransferEncoding));
          ((BodyMultipart) bodyMultipartMixed).BodyParts.Add((Entity) message4);
          if (!string.IsNullOrEmpty(file.ContentId))
            ((Body) bodyUnknown).Entity.Header.Add((Header) new HeaderUnstructured("Content-ID", $"<{file.ContentId}>"));
          ((Body) bodyUnknown).Entity.ContentType.Param_Name = file.Name;
        }
      }
      else
      {
        ((Entity) message1).ContentTransferEncoding = TransferEncodings.Base64;
        BodyText bodyText = new BodyText(msg.Content.IsHtml ? "text/html" : "text/plain");
        ((Entity) message1).Body = (Body) bodyText;
        bodyText.SetText(TransferEncodings.Base64, Encoding.UTF8, msg.Content.Body);
      }
      if (!string.IsNullOrEmpty(msg.UID))
        message1.MessageID = msg.UID;
      message1.Date = DateTime.Now;
      this.\u0002(message1);
    }

    private void \u0002(Message _param1)
    {
      MailSender.SmtpSender.\u0002 obj = new MailSender.SmtpSender.\u0002();
      obj.\u0002 = this;
      try
      {
        MailSender.SmtpSender.\u000E(this.\u0002(), _param1);
        MailSender.SmtpSender.\u0002(this.\u0002(), _param1);
      }
      catch (SmtpClientException ex)
      {
        this.slot?.Dispose();
        int? replyCode = ex.SmptReplyLines?[0]?.ReplyCode;
        if (replyCode.GetValueOrDefault() == 421)
        {
          MailSender.SmtpSender.\u000E(this.\u0002(), _param1);
          MailSender.SmtpSender.\u0002(this.\u0002(), _param1);
        }
        else
        {
          if (replyCode.GetValueOrDefault() == 554)
          {
            obj.\u000E = false;
            if (((IEnumerable) _param1.From).OfType<Mailbox>().Any<Mailbox>(new Func<Mailbox, bool>(obj.\u0002)))
              throw new MailSender.SmtpSender.FromFieldDoesntMatchSenderEmailAddressException((Exception) ex, obj.\u000E);
            throw;
          }
          throw;
        }
      }
    }

    private static AddressList \u0002(string _param0)
    {
      return !string.IsNullOrEmpty(_param0) ? AddressList.Parse(_param0) : new AddressList();
    }

    private static string \u0002(TransferEncoding _param0)
    {
      switch (_param0)
      {
        case TransferEncoding.QuotedPrintable:
          return TransferEncodings.QuotedPrintable;
        case TransferEncoding.Base64:
          return TransferEncodings.Base64;
        case TransferEncoding.SevenBit:
          return TransferEncodings.SevenBit;
        default:
          return TransferEncodings.Binary;
      }
    }

    private static void \u0002(SmtpClient _param0, Message _param1)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        ((Entity) _param1).ToStream((Stream) memoryStream);
        memoryStream.Position = 0L;
        _param0.SendMessage((Stream) memoryStream);
      }
    }

    private static void \u000E(SmtpClient _param0, Message _param1)
    {
      _param0.MailFrom(_param1.From[0].Address, -1L);
      foreach (Mailbox mailbox in _param1.To)
        _param0.RcptTo(mailbox.Address);
      foreach (Mailbox mailbox in _param1.Cc)
        _param0.RcptTo(mailbox.Address);
      foreach (Mailbox mailbox in _param1.Bcc)
        _param0.RcptTo(mailbox.Address);
      if (_param1.To.Count <= 0 && _param1.Cc.Count <= 0)
        return;
      _param1.Bcc = (AddressList) null;
    }

    private static MailAddress \u0002(string _param0)
    {
      Mailbox mailbox = Mailbox.Parse(_param0);
      return new MailAddress(mailbox.Address, mailbox.DisplayName);
    }

    private static MailboxList \u0002(string _param0)
    {
      MailboxList mailboxList = new MailboxList();
      if (!string.IsNullOrEmpty(_param0))
      {
        Mailbox mailbox = Mailbox.Parse(_param0);
        mailboxList.Add(mailbox);
      }
      return mailboxList;
    }

    public override void Test()
    {
      BodyMultipartMixed bodyMultipartMixed = new BodyMultipartMixed(new HeaderContentType("multipart/mixed")
      {
        Param_Boundary = Guid.NewGuid().ToString().Replace('-', '_')
      });
      Message message1 = new Message();
      BodyText bodyText = new BodyText("text/plain");
      ((Entity) message1).Body = (Body) bodyText;
      bodyText.SetText(TransferEncodings.Base64, Encoding.UTF8, "Test mail account");
      ((BodyMultipart) bodyMultipartMixed).BodyParts.Add((Entity) message1);
      Message message2 = new Message();
      message2.From = MailSender.SmtpSender.\u0002(this._emailAddress);
      message2.To = MailSender.SmtpSender.\u0002(this._emailAddress);
      message2.Cc = new AddressList();
      message2.Bcc = new AddressList();
      message2.Subject = nameof (Test);
      ((Entity) message2).Body = (Body) bodyMultipartMixed;
      message2.Date = DateTime.Now;
      this.\u0002(message2);
    }

    public override void Dispose() => this.slot?.Dispose();

    private sealed class \u0002
    {
      public MailSender.SmtpSender \u0002;
      public bool \u000E;

      internal bool \u0002(Mailbox _param1)
      {
        if (_param1.Address != this.\u0002.EmailAddress)
          return true;
        CredentialSettings credential1 = this.\u0002.Credential;
        if (((CredentialSettings) ref credential1).IsOAuth)
        {
          string address = _param1.Address;
          CredentialSettings credential2 = this.\u0002.Credential;
          string login = ((CredentialSettings) ref credential2).Login;
          if (address != login)
            return this.\u000E = true;
        }
        return false;
      }
    }

    [PXInternalUseOnly]
    [Serializable]
    public class FromFieldDoesntMatchSenderEmailAddressException : Exception
    {
      private readonly bool \u0002;

      internal FromFieldDoesntMatchSenderEmailAddressException(Exception _param1, bool _param2)
        : base("Email address of MailSender doesn't match email address specified in from. Looks like SMTP server require email address to be equivalent of email address of authenticated user.", _param1)
      {
        this.\u0002 = _param2;
      }

      internal FromFieldDoesntMatchSenderEmailAddressException(
        SerializationInfo _param1,
        StreamingContext _param2)
        : base(_param1, _param2)
      {
        this.\u0002 = _param1.GetBoolean(nameof (IsOAuthLoginInfoDiffers));
      }

      public bool IsOAuthLoginInfoDiffers => this.\u0002;

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        info.AddValue("IsOAuthLoginInfoDiffers", this.IsOAuthLoginInfoDiffers);
        base.GetObjectData(info, context);
      }
    }
  }

  public enum Types
  {
    Dummy,
    SMTP,
    NativeSMTP,
    File,
  }
}
