// Decompiled with JetBrains decompiler
// Type: PX.Common.Mail.MailReceiver
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using PX.Common.IMAP;
using PX.Common.IMAP.Client;
using PX.Common.POP3.Client;
using PX.Common.TCP;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Common.Mail;

[Serializable]
public abstract class MailReceiver : IDisposable
{
  private readonly ConnectionSettings _connectionSettings;
  private readonly CredentialSettings _credentialSettings;
  private readonly int _timeout;

  protected MailReceiver(
    ConnectionSettings connectionSettings,
    CredentialSettings credentialSettings,
    int timeout)
  {
    this._connectionSettings = connectionSettings;
    this._credentialSettings = credentialSettings;
    this._timeout = timeout;
  }

  ~MailReceiver() => this.Dispose();

  public ConnectionSettings Host => this._connectionSettings;

  public CredentialSettings Credential => this._credentialSettings;

  public int Timeout => this._timeout;

  public int MaxSize { get; set; }

  public bool DeleteFromServer { get; set; }

  public bool EnableImapEnvelope { get; set; }

  public string RootFolder { get; set; }

  public abstract IEnumerable<Email> Receive(MailReceiver.Context context);

  public abstract Email ReceiveMail(object uid);

  public abstract void Test();

  public virtual void Dispose()
  {
  }

  public sealed class Context
  {
    private readonly MailReceiver.IReadUIDsCollection \u0002;
    private readonly IDictionary<string, object> \u000E;
    private MailReceiver.FetchingBehavior \u0006;

    public Context(MailReceiver.Context source, MailReceiver.IReadUIDsCollection readUIDs)
    {
      this.\u000E = source != null ? source.\u000E : throw new ArgumentNullException(nameof (source));
      this.\u0002 = readUIDs != null ? readUIDs : throw new ArgumentNullException(nameof (readUIDs));
    }

    public Context(MailReceiver.IReadUIDsCollection readUIDs)
    {
      this.\u0002 = readUIDs != null ? readUIDs : throw new ArgumentNullException(nameof (readUIDs));
      this.\u000E = (IDictionary<string, object>) new Dictionary<string, object>();
    }

    public MailReceiver.FetchingBehavior ImapFetchingBehavior
    {
      get => this.\u0006;
      set => this.\u0006 = value;
    }

    public object this[string key]
    {
      get
      {
        object obj;
        return !this.\u000E.TryGetValue(key, out obj) ? (object) null : obj;
      }
      set => this.\u000E[key] = value;
    }

    public MailReceiver.IReadUIDsCollection ReadUIDs => this.\u0002;
  }

  internal class DefaultMailReceiverBuilder : MailReceiver.IMailReceiverBuilder
  {
    public MailReceiver Create(
      MailReceiver.Types _param1,
      ConnectionSettings _param2,
      CredentialSettings _param3,
      int _param4)
    {
      if (_param1 == MailReceiver.Types.Pop3)
        return (MailReceiver) new MailReceiver.Pop3Receiver(_param2, _param3, _param4);
      return _param1 == MailReceiver.Types.Imap ? (MailReceiver) new MailReceiver.ImapReceiver(_param2, _param3, _param4) : (MailReceiver) null;
    }
  }

  public enum FetchingBehavior
  {
    MarkEmailOnServerAsRead,
    LeaveEmailOnServerUntouched,
    DeleteEmailOnServer,
  }

  public interface IMailReceiverBuilder
  {
    MailReceiver Create(
      MailReceiver.Types type,
      ConnectionSettings host,
      CredentialSettings login,
      int timeout);
  }

  public interface IReadUIDsCollection
  {
    MailReceiver.ReadUIDs Get(int marker);

    void Add(object uid);
  }

  [Serializable]
  internal class ImapReceiver(ConnectionSettings _param1, CredentialSettings _param2, int _param3 = 60000) : 
    MailReceiver(_param1, _param2, _param3)
  {
    public override IEnumerable<Email> Receive(MailReceiver.Context _param1)
    {
      return (IEnumerable<Email>) new MailReceiver.ImapReceiver.\u0008(-2)
      {
        \u0008 = this,
        \u000F = _param1
      };
    }

    private MailReceiver.ImapReceiver.\u000F \u0002(MailReceiver.Context _param1)
    {
      MailReceiver.IReadUIDsCollection readUiDs1 = _param1.ReadUIDs;
      MailReceiver.ImapReceiver.\u000F obj1 = _param1["request"] as MailReceiver.ImapReceiver.\u000F;
      object obj2 = _param1["marker"];
      if (obj1 == null || obj2 == null)
      {
        MailReceiver.ReadUIDs readUiDs2 = readUiDs1.Get(0);
        obj1 = new MailReceiver.ImapReceiver.\u000F(readUiDs2.UIDs.Cast<int>());
        _param1["request"] = (object) obj1;
        _param1["marker"] = (object) readUiDs2.Marker;
      }
      else
      {
        int marker = (int) obj2;
        MailReceiver.ReadUIDs readUiDs3 = readUiDs1.Get(marker);
        obj1.\u0002(readUiDs3.UIDs.Cast<int>());
        _param1["marker"] = (object) readUiDs3.Marker;
      }
      return obj1;
    }

    public override Email ReceiveMail(object _param1)
    {
      MailReceiver.ImapReceiver.\u0006 obj1 = new MailReceiver.ImapReceiver.\u0006();
      obj1.\u0002 = _param1;
      if (obj1.\u0002 == null)
        return (Email) null;
      using (ImapClient imapClient = this.\u0002(false))
      {
        MailReceiver.ImapReceiver.\u0003 obj2 = new MailReceiver.ImapReceiver.\u0003(imapClient);
        obj2.\u0002(this.Timeout);
        obj2.\u0002(this.EnableImapEnvelope);
        return obj2.\u0002(new MailReceiver.ImapReceiver.\u000F((IEnumerable<int>) new int[1]
        {
          (int) obj1.\u0002
        })).FirstOrDefault<Email>(new Func<Email, bool>(obj1.\u0002));
      }
    }

    public override void Test() => ((IDisposable) this.\u0002(false)).Dispose();

    private ImapClient \u0002(bool _param1)
    {
      ImapClient imapClient1 = new ImapClient();
      ImapClient imapClient2 = imapClient1;
      ConnectionSettings host = this.Host;
      string path = ((ConnectionSettings) ref host).Path;
      host = this.Host;
      int port = ((ConnectionSettings) ref host).Port;
      host = this.Host;
      int num = ((ConnectionSettings) ref host).Security > 0 ? 1 : 0;
      ((TcpClient) imapClient2).Connect(path, port, num != 0);
      CredentialSettings credential1 = this.Credential;
      if (!string.IsNullOrEmpty(((CredentialSettings) ref credential1).Login))
        imapClient1.Authenticate(this.Credential);
      try
      {
        if (_param1)
          imapClient1.ExamineFolder(this.RootFolder);
        else
          imapClient1.SelectFolder(this.RootFolder);
      }
      catch (ImapClientException ex)
      {
        CredentialSettings credential2 = this.Credential;
        if (((CredentialSettings) ref credential2).IsOAuth && ex.ResponseText == "User is authenticated but not connected.")
          throw new ImapClientException("Emails cannot be received because the account you signed in with does not have permission for using the email address specified in the system email account on the System Email Accounts (SM204002) form.", ex);
        throw;
      }
      return imapClient1;
    }

    [Serializable]
    private sealed class \u0002
    {
      public static readonly MailReceiver.ImapReceiver.\u0002 \u0002 = new MailReceiver.ImapReceiver.\u0002();
      public static Func<TimeSpan, Exception> \u000E;

      internal Exception \u0002(TimeSpan _param1)
      {
        return (Exception) new TimeoutException("The IMAP connection could not be established because of a timeout error.");
      }
    }

    private sealed class \u0003
    {
      private readonly ImapClient \u0002;
      private int \u000E;
      private bool \u0006;

      public \u0003(ImapClient _param1)
      {
        this.\u0002 = _param1 != null ? _param1 : throw new ArgumentNullException("client");
      }

      public int \u0002() => this.\u000E;

      public void \u0002(int _param1) => this.\u000E = _param1;

      public bool \u0002() => this.\u0006;

      public void \u0002(bool _param1) => this.\u0006 = _param1;

      public IEnumerable<Email> \u0002(MailReceiver.ImapReceiver.\u000F _param1)
      {
        return this.\u0002(_param1.\u0002());
      }

      private IEnumerable<Email> \u0002(string _param1)
      {
        return (IEnumerable<Email>) new MailReceiver.ImapReceiver.\u0003.\u0008(-2)
        {
          \u0008 = this,
          \u000F = _param1
        };
      }

      private void \u0002(string _param1, out string _param2, out string _param3)
      {
        _param2 = (string) null;
        _param3 = (string) null;
        if (string.IsNullOrWhiteSpace(_param1))
          return;
        int num = _param1.LastIndexOf(',');
        if (num < 0 || num >= _param1.Length - 1)
          _param2 = _param1;
        else
          _param2 = _param1.Substring(num + 1);
      }

      [Serializable]
      private sealed class \u0002
      {
        public static readonly MailReceiver.ImapReceiver.\u0003.\u0002 \u0002 = new MailReceiver.ImapReceiver.\u0003.\u0002();
        public static Func<TimeSpan, Exception> \u000E;

        internal Exception \u0002(TimeSpan _param1)
        {
          return (Exception) new TimeoutException("An IMAP item could not be fetched because of a timeout error.");
        }
      }

      private sealed class \u0003 : ImapClientFetchHandler
      {
        private readonly ConcurrentDictionary<long, MailReceiver.ImapReceiver.\u0003.\u000F> \u0002 = new ConcurrentDictionary<long, MailReceiver.ImapReceiver.\u0003.\u000F>();

        public \u0003()
        {
          this.NextMessage += new EventHandler(this.\u0002);
          this.UID += new EventHandler<EventArgs<long>>(this.\u0002);
          this.Rfc822 += new EventHandler<ImapClientFetchRfc822EArgs>(this.\u0002);
          this.Envelope += new EventHandler<EventArgs<Envelope>>(this.\u0002);
        }

        public ConcurrentDictionary<long, MailReceiver.ImapReceiver.\u0003.\u000F> \u0002()
        {
          return this.\u0002;
        }

        private void \u0002(object _param1, EventArgs _param2)
        {
          this.\u0002().TryAdd((long) this.CurrentSeqNo, new MailReceiver.ImapReceiver.\u0003.\u000F());
        }

        private void \u0002(object _param1, EventArgs<long> _param2)
        {
          MailReceiver.ImapReceiver.\u0003.\u000F obj;
          if (!this.\u0002().TryGetValue((long) this.CurrentSeqNo, out obj))
            return;
          obj.\u0002(_param2.Value);
        }

        private void \u0002(object _param1, ImapClientFetchRfc822EArgs _param2)
        {
          MailReceiver.ImapReceiver.\u0003.\u0003.\u0002 obj = new MailReceiver.ImapReceiver.\u0003.\u0003.\u0002();
          if (!this.\u0002().TryGetValue((long) this.CurrentSeqNo, out obj.\u0002))
            return;
          obj.\u000E = new MemoryStream();
          _param2.Stream = (Stream) obj.\u000E;
          _param2.StoringCompleted += new EventHandler(obj.\u0002);
        }

        private void \u0002(object _param1, EventArgs<Envelope> _param2)
        {
          MailReceiver.ImapReceiver.\u0003.\u000F obj;
          if (!this.\u0002().TryGetValue((long) this.CurrentSeqNo, out obj))
            return;
          obj.\u0002(_param2.Value);
        }

        private sealed class \u0002
        {
          public MailReceiver.ImapReceiver.\u0003.\u000F \u0002;
          public MemoryStream \u000E;

          internal void \u0002(object _param1, EventArgs _param2)
          {
            this.\u000E.Position = 0L;
            try
            {
              this.\u0002.\u0002(Message.ParseFromStream((Stream) this.\u000E));
            }
            catch (Exception ex)
            {
              this.\u0002.\u0002(ex);
            }
          }
        }
      }

      private sealed class \u0006
      {
        public SequenceSet \u0002;
        public MailReceiver.ImapReceiver.\u0003.\u000E \u000E;

        internal void \u0002()
        {
          this.\u000E.\u0002.\u0002.Fetch(true, this.\u0002, this.\u000E.\u000E, (ImapClientFetchHandler) this.\u000E.\u0006);
        }
      }

      private sealed class \u0008 : 
        IEnumerable<Email>,
        IEnumerable,
        IEnumerator<Email>,
        IDisposable,
        IEnumerator
      {
        private int \u0002;
        private Email \u000E;
        private int \u0006;
        public MailReceiver.ImapReceiver.\u0003 \u0008;
        private string \u0003;
        public string \u000F;
        private MailReceiver.ImapReceiver.\u0003.\u000E \u0005;
        private MailReceiver.ImapReceiver.\u0003.\u0006 \u0002\u2009;
        private string \u000E\u2009;
        private IEnumerator<KeyValuePair<long, MailReceiver.ImapReceiver.\u0003.\u000F>> \u0006\u2009;

        [DebuggerHidden]
        public \u0008(int _param1)
        {
          this.\u0002 = _param1;
          this.\u0006 = Environment.CurrentManagedThreadId;
        }

        [DebuggerHidden]
        void IDisposable.\u0008\u2009\u2009\u2009\u0002()
        {
          switch (this.\u0002)
          {
            case -3:
            case 1:
              try
              {
              }
              finally
              {
                this.\u000E();
              }
              break;
          }
        }

        bool IEnumerator.MoveNext()
        {
          // ISSUE: fault handler
          try
          {
            int num1 = this.\u0002;
            MailReceiver.ImapReceiver.\u0003 obj1 = this.\u0008;
            switch (num1)
            {
              case 0:
                this.\u0002 = -1;
                this.\u0005 = new MailReceiver.ImapReceiver.\u0003.\u000E();
                this.\u0005.\u0002 = this.\u0008;
                if (string.IsNullOrWhiteSpace(this.\u0003))
                  return false;
                this.\u0005.\u000E = new FetchDataItem[3]
                {
                  (FetchDataItem) new FetchDataItemUid(),
                  (FetchDataItem) new FetchDataItemRfc822Size(),
                  (FetchDataItem) new FetchDataItemRfc822()
                };
                if (obj1.\u0002())
                  this.\u0005.\u000E = EnumerableExtensions.Append<FetchDataItem>(this.\u0005.\u000E, (FetchDataItem) new FetchDataItemEnvelope());
                this.\u0005.\u0006 = new MailReceiver.ImapReceiver.\u0003.\u0003();
                break;
              case 1:
                this.\u0002 = -3;
                goto label_10;
              default:
                return false;
            }
label_7:
            this.\u0002\u2009 = new MailReceiver.ImapReceiver.\u0003.\u0006();
            this.\u0002\u2009.\u000E = this.\u0005;
            string str;
            obj1.\u0002(this.\u0003, out str, out this.\u000E\u2009);
            this.\u0002\u2009.\u0002 = new SequenceSet();
            this.\u0002\u2009.\u0002.Parse(str);
            ThreadCop.PerformWithTimeout(TimeSpan.FromMilliseconds((double) obj1.\u0002()), new Action(this.\u0002\u2009.\u0002), MailReceiver.ImapReceiver.\u0003.\u0002.\u000E ?? (MailReceiver.ImapReceiver.\u0003.\u0002.\u000E = new Func<TimeSpan, Exception>(MailReceiver.ImapReceiver.\u0003.\u0002.\u0002.\u0002)));
            this.\u0006\u2009 = this.\u0002\u2009.\u000E.\u0006.\u0002().GetEnumerator();
            this.\u0002 = -3;
label_10:
            if (this.\u0006\u2009.MoveNext())
            {
              long num2;
              MailReceiver.ImapReceiver.\u0003.\u000F obj2;
              EnumerableExtensions.Deconstruct<long, MailReceiver.ImapReceiver.\u0003.\u000F>(this.\u0006\u2009.Current, ref num2, ref obj2);
              long key = num2;
              MailReceiver.ImapReceiver.\u0003.\u000F obj3 = obj2;
              this.\u0002\u2009.\u000E.\u0006.\u0002().TryRemove(key, out obj2);
              this.\u000E = new Email(obj3.\u0002().ToString(), obj3.\u0002(), obj3.\u0002(), obj3.\u0002());
              this.\u0002 = 1;
              return true;
            }
            this.\u000E();
            this.\u0006\u2009 = (IEnumerator<KeyValuePair<long, MailReceiver.ImapReceiver.\u0003.\u000F>>) null;
            this.\u0002\u2009 = (MailReceiver.ImapReceiver.\u0003.\u0006) null;
            if (string.IsNullOrEmpty(this.\u000E\u2009))
              return false;
            goto label_7;
          }
          __fault
          {
            this.\u0008\u2009\u2009\u2009\u0002();
          }
        }

        private void \u000E()
        {
          this.\u0002 = -1;
          if (this.\u0006\u2009 == null)
            return;
          this.\u0006\u2009.Dispose();
        }

        [DebuggerHidden]
        Email IEnumerator<Email>.\u0008\u2009\u2009\u2009\u0002() => this.\u000E;

        [DebuggerHidden]
        void IEnumerator.\u0008\u2009\u2009\u2009\u0006() => throw new NotSupportedException();

        [DebuggerHidden]
        object IEnumerator.\u0008\u2009\u2009\u2009\u0002() => (object) this.\u000E;

        [DebuggerHidden]
        IEnumerator<Email> IEnumerable<Email>.\u0008\u2009\u2009\u2009\u0002()
        {
          MailReceiver.ImapReceiver.\u0003.\u0008 obj;
          if (this.\u0002 == -2 && this.\u0006 == Environment.CurrentManagedThreadId)
          {
            this.\u0002 = 0;
            obj = this;
          }
          else
          {
            obj = new MailReceiver.ImapReceiver.\u0003.\u0008(0);
            obj.\u0008 = this.\u0008;
          }
          obj.\u0003 = this.\u000F;
          return (IEnumerator<Email>) obj;
        }

        [DebuggerHidden]
        IEnumerator IEnumerable.\u0008\u2009\u2009\u2009\u0002()
        {
          return (IEnumerator) this.\u0008\u2009\u2009\u2009\u0002();
        }
      }

      private sealed class \u000E
      {
        public MailReceiver.ImapReceiver.\u0003 \u0002;
        public FetchDataItem[] \u000E;
        public MailReceiver.ImapReceiver.\u0003.\u0003 \u0006;
      }

      private sealed class \u000F
      {
        private long \u0002;
        private Message \u000E;
        private Exception \u0006;
        private Envelope \u0008;

        public long \u0002() => this.\u0002;

        public void \u0002(long _param1) => this.\u0002 = _param1;

        public Message \u0002() => this.\u000E;

        public void \u0002(Message _param1) => this.\u000E = _param1;

        public Exception \u0002() => this.\u0006;

        public void \u0002(Exception _param1) => this.\u0006 = _param1;

        public Envelope \u0002() => this.\u0008;

        public void \u0002(Envelope _param1) => this.\u0008 = _param1;
      }
    }

    private sealed class \u0006
    {
      public object \u0002;

      internal bool \u0002(Email _param1)
      {
        int result;
        return int.TryParse(_param1.UID, out result) && result == (int) this.\u0002;
      }
    }

    private sealed class \u0008 : 
      IEnumerable<Email>,
      IEnumerable,
      IEnumerator<Email>,
      IDisposable,
      IEnumerator
    {
      private int \u0002;
      private Email \u000E;
      private int \u0006;
      public MailReceiver.ImapReceiver \u0008;
      private MailReceiver.Context \u0003;
      public MailReceiver.Context \u000F;
      private ImapClient \u0005;
      private IEnumerator<Email> \u0002\u2009;

      [DebuggerHidden]
      public \u0008(int _param1)
      {
        this.\u0002 = _param1;
        this.\u0006 = Environment.CurrentManagedThreadId;
      }

      [DebuggerHidden]
      void IDisposable.\u0008\u2009\u2009\u2009\u0002()
      {
        int num = this.\u0002;
        switch (num)
        {
          case -4:
          case -3:
          case 1:
            try
            {
              if (num != -4 && num != 1)
                break;
              try
              {
              }
              finally
              {
                this.\u0006();
              }
              break;
            }
            finally
            {
              this.\u000E();
            }
        }
      }

      bool IEnumerator.MoveNext()
      {
        // ISSUE: fault handler
        try
        {
          int num = this.\u0002;
          MailReceiver.ImapReceiver imapReceiver = this.\u0008;
          switch (num)
          {
            case 0:
              this.\u0002 = -1;
              this.\u0005 = ThreadCop.PerformWithTimeout<ImapClient>(TimeSpan.FromMilliseconds((double) imapReceiver.Timeout), new Func<ImapClient>(new MailReceiver.ImapReceiver.\u000E()
              {
                \u0002 = this.\u0008,
                \u000E = this.\u0003.ImapFetchingBehavior == MailReceiver.FetchingBehavior.LeaveEmailOnServerUntouched
              }.\u0002), MailReceiver.ImapReceiver.\u0002.\u000E ?? (MailReceiver.ImapReceiver.\u0002.\u000E = new Func<TimeSpan, Exception>(MailReceiver.ImapReceiver.\u0002.\u0002.\u0002)));
              this.\u0002 = -3;
              MailReceiver.ImapReceiver.\u0003 obj = new MailReceiver.ImapReceiver.\u0003(this.\u0005);
              obj.\u0002(imapReceiver.Timeout);
              obj.\u0002(imapReceiver.EnableImapEnvelope);
              this.\u0002\u2009 = obj.\u0002(imapReceiver.\u0002(this.\u0003)).GetEnumerator();
              this.\u0002 = -4;
              break;
            case 1:
              this.\u0002 = -4;
              break;
            default:
              return false;
          }
          if (this.\u0002\u2009.MoveNext())
          {
            Email email = this.\u0002\u2009.Current;
            SequenceSet sequenceSet = new SequenceSet();
            sequenceSet.Parse(email.UID);
            if (email.Exception == null)
            {
              try
              {
                switch (this.\u0003.ImapFetchingBehavior)
                {
                  case MailReceiver.FetchingBehavior.MarkEmailOnServerAsRead:
                    try
                    {
                      this.\u0005.StoreMessageFlags(true, sequenceSet, (FlagsSetType) 1, (MessageFlags) 2);
                      break;
                    }
                    catch (ImapClientException ex)
                    {
                      throw new ImapClientException("The email was not marked as read after it was received on the mail server.", ex);
                    }
                  case MailReceiver.FetchingBehavior.DeleteEmailOnServer:
                    try
                    {
                      this.\u0005.StoreMessageFlags(true, sequenceSet, (FlagsSetType) 1, (MessageFlags) 16 /*0x10*/);
                      this.\u0005.Expunge();
                      break;
                    }
                    catch (ImapClientException ex)
                    {
                      throw new ImapClientException("After the email was received, it was not marked as deleted on the mail server.", ex);
                    }
                }
              }
              catch (Exception ex)
              {
                email = new Email(email.UID, email.Message, ex, (Envelope) null);
              }
            }
            this.\u000E = email;
            this.\u0002 = 1;
            return true;
          }
          this.\u0006();
          this.\u0002\u2009 = (IEnumerator<Email>) null;
          this.\u000E();
          this.\u0005 = (ImapClient) null;
          return false;
        }
        __fault
        {
          this.\u0008\u2009\u2009\u2009\u0002();
        }
      }

      private void \u000E()
      {
        this.\u0002 = -1;
        if (this.\u0005 == null)
          return;
        ((IDisposable) this.\u0005).Dispose();
      }

      private void \u0006()
      {
        this.\u0002 = -3;
        if (this.\u0002\u2009 == null)
          return;
        this.\u0002\u2009.Dispose();
      }

      [DebuggerHidden]
      Email IEnumerator<Email>.\u0008\u2009\u2009\u2009\u0002() => this.\u000E;

      [DebuggerHidden]
      void IEnumerator.\u0008\u2009\u2009\u2009\u0008() => throw new NotSupportedException();

      [DebuggerHidden]
      object IEnumerator.\u0008\u2009\u2009\u2009\u0002() => (object) this.\u000E;

      [DebuggerHidden]
      IEnumerator<Email> IEnumerable<Email>.\u0008\u2009\u2009\u2009\u0002()
      {
        MailReceiver.ImapReceiver.\u0008 obj;
        if (this.\u0002 == -2 && this.\u0006 == Environment.CurrentManagedThreadId)
        {
          this.\u0002 = 0;
          obj = this;
        }
        else
        {
          obj = new MailReceiver.ImapReceiver.\u0008(0);
          obj.\u0008 = this.\u0008;
        }
        obj.\u0003 = this.\u000F;
        return (IEnumerator<Email>) obj;
      }

      [DebuggerHidden]
      IEnumerator IEnumerable.\u0008\u2009\u2009\u2009\u0002()
      {
        return (IEnumerator) this.\u0008\u2009\u2009\u2009\u0002();
      }
    }

    private sealed class \u000E
    {
      public MailReceiver.ImapReceiver \u0002;
      public bool \u000E;

      internal ImapClient \u0002() => this.\u0002.\u0002(this.\u000E);
    }

    private sealed class \u000F
    {
      private string \u0002;
      private int \u000E;

      public \u000F(IEnumerable<int> _param1)
      {
        if (_param1 == null)
          throw new ArgumentNullException("uids");
        this.\u000E(this.\u0002(_param1));
      }

      public void \u0002(IEnumerable<int> _param1) => this.\u000E(this.\u0002(_param1));

      public string \u0002() => this.\u0002;

      private IEnumerable<int> \u0002(IEnumerable<int> _param1)
      {
        List<int> intList = new List<int>(_param1);
        intList.Sort();
        return (IEnumerable<int>) intList;
      }

      private void \u000E(IEnumerable<int> _param1)
      {
        List<int> list = _param1.ToList<int>();
        if (this.\u000E == 0 && list.Count == 1)
        {
          this.\u000E = list[0];
          this.\u0002 = this.\u000E.ToString();
        }
        else
        {
          StringBuilder stringBuilder1;
          if (this.\u0002 != null)
            stringBuilder1 = new StringBuilder(this.\u0002.TrimEnd('*').TrimEnd(':'));
          else
            stringBuilder1 = new StringBuilder();
          StringBuilder stringBuilder2 = stringBuilder1;
          int num1 = this.\u000E;
          if (list.Count > 0)
          {
            foreach (int num2 in _param1)
            {
              if (num2 > this.\u000E)
              {
                this.\u000E = num2;
                int num3 = num2 - num1;
                if (num3 > 2)
                {
                  if (stringBuilder2.Length > 0)
                    stringBuilder2.Append(',');
                  stringBuilder2.Append(num1 + 1);
                  stringBuilder2.Append(":");
                  stringBuilder2.Append(num2 - 1);
                }
                else if (num3 > 1)
                {
                  if (stringBuilder2.Length > 0)
                    stringBuilder2.Append(',');
                  stringBuilder2.Append(num1 + 1);
                }
                num1 = num2;
              }
            }
            if (stringBuilder2.Length > 0)
              stringBuilder2.Append(',');
            this.\u000E = num1 + 1;
            stringBuilder2.Append(this.\u000E);
          }
          if (stringBuilder2.Length == 0)
            stringBuilder2.Append(this.\u000E + 1);
          stringBuilder2.Append(":*");
          this.\u0002 = stringBuilder2.ToString();
        }
      }
    }
  }

  [Serializable]
  internal class Pop3Receiver(ConnectionSettings _param1, CredentialSettings _param2, int _param3 = 60000) : 
    MailReceiver(_param1, _param2, _param3)
  {
    public override IEnumerable<Email> Receive(MailReceiver.Context _param1)
    {
      return (IEnumerable<Email>) new MailReceiver.Pop3Receiver.\u0006(-2)
      {
        \u0008 = this,
        \u000F = _param1
      };
    }

    private MailReceiver.Pop3Receiver.\u0008 \u0002(MailReceiver.Context _param1)
    {
      MailReceiver.IReadUIDsCollection readUiDs1 = _param1.ReadUIDs;
      MailReceiver.Pop3Receiver.\u0008 obj1 = _param1["request"] as MailReceiver.Pop3Receiver.\u0008;
      object obj2 = _param1["marker"];
      if (obj1 == null || obj2 == null)
      {
        MailReceiver.ReadUIDs readUiDs2 = readUiDs1.Get(0);
        obj1 = new MailReceiver.Pop3Receiver.\u0008((IEnumerable) readUiDs2.UIDs.Cast<string>());
        _param1["request"] = (object) obj1;
        _param1["marker"] = (object) readUiDs2.Marker;
      }
      else
      {
        int marker = (int) obj2;
        MailReceiver.ReadUIDs readUiDs3 = readUiDs1.Get(marker);
        obj1.\u0002((IEnumerable) readUiDs3.UIDs.Cast<string>());
        _param1["marker"] = (object) readUiDs3.Marker;
      }
      return obj1;
    }

    public override Email ReceiveMail(object _param1)
    {
      if (_param1 != null)
      {
        using (Pop3Client pop3Client = new Pop3Client())
        {
          this.\u0002(pop3Client);
          if (pop3Client.Messages.Count > 0)
          {
            foreach (Pop3ClientMessage message in pop3Client.Messages)
            {
              if (object.Equals((object) message.UID, _param1))
              {
                Message fromByte = Message.ParseFromByte(message.MessageToByte());
                return new Email(message.UID, fromByte, message.Exception, (Envelope) null);
              }
            }
          }
        }
      }
      return (Email) null;
    }

    public override void Test()
    {
      using (Pop3Client pop3Client = new Pop3Client())
        this.\u0002(pop3Client);
    }

    private void \u0002(Pop3Client _param1)
    {
      Pop3Client pop3Client = _param1;
      ConnectionSettings host = this.Host;
      string path = ((ConnectionSettings) ref host).Path;
      host = this.Host;
      int port = ((ConnectionSettings) ref host).Port;
      host = this.Host;
      int num = ((ConnectionSettings) ref host).Security > 0 ? 1 : 0;
      ((TcpClient) pop3Client).Connect(path, port, num != 0);
      CredentialSettings credential = this.Credential;
      if (string.IsNullOrEmpty(((CredentialSettings) ref credential).Login))
        return;
      _param1.Authenticate(this.Credential);
    }

    [Serializable]
    private sealed class \u0002
    {
      public static readonly MailReceiver.Pop3Receiver.\u0002 \u0002 = new MailReceiver.Pop3Receiver.\u0002();
      public static Func<TimeSpan, Exception> \u000E;

      internal Exception \u0002(TimeSpan _param1)
      {
        return (Exception) new TimeoutException("The POP3 connection could not be established because of a timeout error.");
      }
    }

    private sealed class \u0006 : 
      IEnumerable<Email>,
      IEnumerable,
      IEnumerator<Email>,
      IDisposable,
      IEnumerator
    {
      private int \u0002;
      private Email \u000E;
      private int \u0006;
      public MailReceiver.Pop3Receiver \u0008;
      private MailReceiver.Context \u0003;
      public MailReceiver.Context \u000F;
      private MailReceiver.Pop3Receiver.\u000E \u0005;
      private MailReceiver.Pop3Receiver.\u0008 \u0002\u2009;
      private IEnumerator \u000E\u2009;
      private Pop3ClientMessage \u0006\u2009;

      [DebuggerHidden]
      public \u0006(int _param1)
      {
        this.\u0002 = _param1;
        this.\u0006 = Environment.CurrentManagedThreadId;
      }

      [DebuggerHidden]
      void IDisposable.\u0006\u2009\u2009\u2009\u0002()
      {
        int num = this.\u0002;
        switch (num)
        {
          case -4:
          case -3:
          case 1:
            try
            {
              if (num != -4 && num != 1)
                break;
              try
              {
              }
              finally
              {
                this.\u0006();
              }
              break;
            }
            finally
            {
              this.\u000E();
            }
        }
      }

      bool IEnumerator.MoveNext()
      {
        // ISSUE: fault handler
        try
        {
          int num = this.\u0002;
          MailReceiver.Pop3Receiver pop3Receiver = this.\u0008;
          switch (num)
          {
            case 0:
              this.\u0002 = -1;
              this.\u0005 = new MailReceiver.Pop3Receiver.\u000E();
              this.\u0005.\u0002 = this.\u0008;
              this.\u0002\u2009 = pop3Receiver.\u0002(this.\u0003);
              this.\u0005.\u000E = new Pop3Client();
              this.\u0002 = -3;
              ThreadCop.PerformWithTimeout(TimeSpan.FromMilliseconds((double) pop3Receiver.Timeout), new Action(this.\u0005.\u0002), MailReceiver.Pop3Receiver.\u0002.\u000E ?? (MailReceiver.Pop3Receiver.\u0002.\u000E = new Func<TimeSpan, Exception>(MailReceiver.Pop3Receiver.\u0002.\u0002.\u0002)));
              if (this.\u0005.\u000E.Messages.Count > 0)
              {
                this.\u000E\u2009 = this.\u0005.\u000E.Messages.GetEnumerator();
                this.\u0002 = -4;
                break;
              }
              goto label_14;
            case 1:
              this.\u0002 = -4;
              if (pop3Receiver.DeleteFromServer)
                this.\u0006\u2009.MarkForDeletion();
              this.\u0006\u2009 = (Pop3ClientMessage) null;
              break;
            default:
              return false;
          }
          while (this.\u000E\u2009.MoveNext())
          {
            this.\u0006\u2009 = (Pop3ClientMessage) this.\u000E\u2009.Current;
            if (this.\u0006\u2009.UID == null || !this.\u0002\u2009.\u0002((object) this.\u0006\u2009.UID))
            {
              Message fromByte;
              try
              {
                fromByte = Message.ParseFromByte(this.\u0006\u2009.MessageToByte());
              }
              catch (Exception ex)
              {
                this.\u0003.ReadUIDs.Add((object) this.\u0006\u2009.UID);
                continue;
              }
              this.\u000E = new Email(this.\u0006\u2009.UID, fromByte, this.\u0006\u2009.Exception, (Envelope) null);
              this.\u0002 = 1;
              return true;
            }
          }
          this.\u0006();
          this.\u000E\u2009 = (IEnumerator) null;
label_14:
          this.\u000E();
          return false;
        }
        __fault
        {
          this.\u0006\u2009\u2009\u2009\u0002();
        }
      }

      private void \u000E()
      {
        this.\u0002 = -1;
        if (this.\u0005.\u000E == null)
          return;
        ((IDisposable) this.\u0005.\u000E).Dispose();
      }

      private void \u0006()
      {
        this.\u0002 = -3;
        if (!(this.\u000E\u2009 is IDisposable disposable))
          return;
        disposable.Dispose();
      }

      [DebuggerHidden]
      Email IEnumerator<Email>.\u0006\u2009\u2009\u2009\u0002() => this.\u000E;

      [DebuggerHidden]
      void IEnumerator.\u0006\u2009\u2009\u2009\u0008() => throw new NotSupportedException();

      [DebuggerHidden]
      object IEnumerator.\u0006\u2009\u2009\u2009\u0002() => (object) this.\u000E;

      [DebuggerHidden]
      IEnumerator<Email> IEnumerable<Email>.\u0006\u2009\u2009\u2009\u0002()
      {
        MailReceiver.Pop3Receiver.\u0006 obj;
        if (this.\u0002 == -2 && this.\u0006 == Environment.CurrentManagedThreadId)
        {
          this.\u0002 = 0;
          obj = this;
        }
        else
        {
          obj = new MailReceiver.Pop3Receiver.\u0006(0);
          obj.\u0008 = this.\u0008;
        }
        obj.\u0003 = this.\u000F;
        return (IEnumerator<Email>) obj;
      }

      [DebuggerHidden]
      IEnumerator IEnumerable.\u0006\u2009\u2009\u2009\u0002()
      {
        return (IEnumerator) this.\u0006\u2009\u2009\u2009\u0002();
      }
    }

    private sealed class \u0008
    {
      private HybridDictionary \u0002;

      public \u0008(IEnumerable _param1) => this.\u0002(_param1);

      public void \u0002(IEnumerable _param1)
      {
        if (_param1 == null)
          return;
        foreach (string key in _param1)
        {
          if (this.\u0002 == null)
            this.\u0002 = new HybridDictionary();
          this.\u0002[(object) key] = (object) key;
        }
      }

      public bool \u0002(object _param1)
      {
        return _param1 != null && this.\u0002 != null && this.\u0002.Contains(_param1);
      }
    }

    private sealed class \u000E
    {
      public MailReceiver.Pop3Receiver \u0002;
      public Pop3Client \u000E;

      internal void \u0002() => this.\u0002.\u0002(this.\u000E);
    }
  }

  public sealed class ReadUIDs : IEnumerable
  {
    private readonly IEnumerable \u0002;
    private readonly int \u000E;

    public ReadUIDs(IEnumerable uids, int marker)
    {
      this.\u0002 = uids != null ? uids : throw new ArgumentNullException(nameof (uids));
      this.\u000E = marker;
    }

    public IEnumerable UIDs => this.\u0002;

    public int Marker => this.\u000E;

    public IEnumerator GetEnumerator() => this.UIDs.GetEnumerator();
  }

  public enum Types
  {
    Pop3,
    Imap,
  }
}
