// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.MsmqQueueBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using ConfigCore.Code;
using Newtonsoft.Json;
using PX.Common;
using PX.Common.Extensions;
using System;
using System.IO;
using System.Messaging;
using System.Security.Principal;
using System.Threading;
using System.Transactions;
using System.Web.Hosting;

#nullable disable
namespace PX.Data.PushNotifications;

internal abstract class MsmqQueueBase : IDisposable, IQueueReaderWithStatusInfo
{
  protected const string PrivatePrefix = ".\\private$";
  private const string AdminQueueName = "\\adminQueue";
  private const int DefaultMaximumQueueSize = 102400 /*0x019000*/;
  private const long HardLimitMaximumQueueSize = 1048576 /*0x100000*/;
  protected readonly IMessageFormatter _formatter;
  private readonly bool _queueDenySharedReceive;
  protected readonly string _path;
  private readonly bool? _transactional;
  private MessageQueue _innerQueue;
  protected MessageQueue _adminQueue;
  protected long _lastHeartBeat;
  protected static string _lastFailedToCommitMessage;
  private const string DoNotRestrictQueueSizeKey = "PushNotifications:DoNotRestrictQueueSize";

  internal static bool DoNotRestrictQueueSize()
  {
    return WebConfig.GetBool("PushNotifications:DoNotRestrictQueueSize", false);
  }

  protected void Send(Message message, MessageQueueTransactionType? transactionType = null)
  {
    this.EnsureInnerQueue();
    message.AdministrationQueue = this._adminQueue;
    message.AcknowledgeType = AcknowledgeTypes.NotAcknowledgeReachQueue;
    try
    {
      bool? transactional = this._transactional;
      bool flag = true;
      if (transactional.GetValueOrDefault() == flag & transactional.HasValue || !this._transactional.HasValue && MsmqExtensions.IsPathLocal(this._innerQueue.Path) && this._innerQueue.Transactional)
      {
        MessageQueueTransactionType transactionType1 = (MessageQueueTransactionType) ((int) transactionType ?? (Transaction.Current != (Transaction) null ? 1 : 3));
        this._innerQueue.Send((object) message, transactionType1);
      }
      else
      {
        this._innerQueue.Send((object) message, MessageQueueTransactionType.None);
        this.CheckAdminQueueIfSuccess();
      }
    }
    catch (MessageQueueException ex)
    {
      if (ex.MessageQueueErrorCode == MessageQueueErrorCode.SharingViolation || ex.MessageQueueErrorCode == MessageQueueErrorCode.StaleHandle || ex.MessageQueueErrorCode == MessageQueueErrorCode.InvalidHandle || ex.MessageQueueErrorCode == MessageQueueErrorCode.ServiceNotAvailable)
        this.CleanInnerQueue();
      throw;
    }
  }

  protected void EnsureInnerQueue()
  {
    if (this._innerQueue == null)
      this._innerQueue = this.InitQueue(this._path, true);
    if (this._adminQueue != null)
      return;
    this.CreateAdminQueueIfNotExists();
  }

  protected void Receive(TimeSpan? timeout = null)
  {
    this.EnsureInnerQueue();
    TimeSpan timeout1 = timeout ?? TimeSpan.Zero;
    try
    {
      bool? transactional = this._transactional;
      bool flag = true;
      if (transactional.GetValueOrDefault() == flag & transactional.HasValue || !this._transactional.HasValue && MsmqExtensions.IsPathLocal(this._innerQueue.Path) && this._innerQueue.Transactional)
      {
        MessageQueueTransactionType transactionType = Transaction.Current != (Transaction) null ? MessageQueueTransactionType.Automatic : MessageQueueTransactionType.Single;
        this._innerQueue.Receive(timeout1, transactionType);
      }
      else
        this._innerQueue.Receive(timeout1);
    }
    catch (MessageQueueException ex)
    {
      if (ex.MessageQueueErrorCode == MessageQueueErrorCode.SharingViolation || ex.MessageQueueErrorCode == MessageQueueErrorCode.StaleHandle || ex.MessageQueueErrorCode == MessageQueueErrorCode.InvalidHandle || ex.MessageQueueErrorCode == MessageQueueErrorCode.ServiceNotAvailable)
        this.CleanInnerQueue();
      throw;
    }
  }

  protected void RemoveByCorrelationId(string correlationId, string finalMessageId, long count)
  {
    this.EnsureInnerQueue();
    try
    {
      if (count == 0L)
      {
        count = this._innerQueue.GetSize();
        if (count == 1L)
          count = long.MaxValue;
      }
      using (MessageEnumerator messageEnumerator = this.GetMessageEnumerator())
      {
        if (!messageEnumerator.MoveNext(TimeSpan.Zero))
          return;
        for (long index = 0; index < count + 1L; ++index)
        {
          Message current = messageEnumerator.Current;
          if (current?.CorrelationId == correlationId)
            messageEnumerator.RemoveCurrent(TimeSpan.Zero);
          else if (!messageEnumerator.MoveNext(TimeSpan.Zero))
            break;
          if (current?.Id == finalMessageId)
            break;
        }
      }
    }
    catch (MessageQueueException ex)
    {
      if (ex.MessageQueueErrorCode == MessageQueueErrorCode.SharingViolation || ex.MessageQueueErrorCode == MessageQueueErrorCode.StaleHandle || ex.MessageQueueErrorCode == MessageQueueErrorCode.InvalidHandle || ex.MessageQueueErrorCode == MessageQueueErrorCode.ServiceNotAvailable)
        this.CleanInnerQueue();
      if (ex.MessageQueueErrorCode == MessageQueueErrorCode.IOTimeout)
        return;
      throw;
    }
  }

  protected void RemoveByCorrelationId(string correlationId, string finalMessageId)
  {
    this.EnsureInnerQueue();
    this.EnsureInnerQueue();
    try
    {
      do
        ;
      while (!(this.ReceiveByCorrelationId(correlationId)?.Id == finalMessageId));
    }
    catch (MessageQueueException ex)
    {
      if (ex.MessageQueueErrorCode == MessageQueueErrorCode.SharingViolation || ex.MessageQueueErrorCode == MessageQueueErrorCode.StaleHandle || ex.MessageQueueErrorCode == MessageQueueErrorCode.InvalidHandle || ex.MessageQueueErrorCode == MessageQueueErrorCode.ServiceNotAvailable)
        this.CleanInnerQueue();
      throw;
    }
  }

  protected Message ReceiveByCorrelationId(string correlationId, TimeSpan? timeout = null)
  {
    TimeSpan timeout1 = timeout ?? TimeSpan.Zero;
    bool? transactional = this._transactional;
    bool flag = true;
    if (!(transactional.GetValueOrDefault() == flag & transactional.HasValue) && (this._transactional.HasValue || !MsmqExtensions.IsPathLocal(this._innerQueue.Path) || !this._innerQueue.Transactional))
      return this._innerQueue.ReceiveByCorrelationId(correlationId, timeout1);
    MessageQueueTransactionType transactionType = Transaction.Current != (Transaction) null ? MessageQueueTransactionType.Automatic : MessageQueueTransactionType.Single;
    return this._innerQueue.ReceiveByCorrelationId(correlationId, timeout1, transactionType);
  }

  protected bool TryPeek(
    CancellationToken cancellationToken,
    out Message message,
    TimeSpan? timeout = null)
  {
    return this._innerQueue.PeekWithCancellation(cancellationToken, out message, timeout, (System.Action<long>) (c => this._lastHeartBeat = c));
  }

  protected MessageEnumerator GetMessageEnumerator() => this._innerQueue.GetMessageEnumerator2();

  protected MsmqQueueBase(
    IMessageFormatter formatter,
    string path,
    bool queueDenySharedReceive,
    bool? transactional)
  {
    this._formatter = formatter;
    this._queueDenySharedReceive = queueDenySharedReceive;
    this._transactional = transactional;
    this._innerQueue = this.InitQueue(path, queueDenySharedReceive);
    this._path = path;
  }

  protected MsmqQueueBase(
    JsonSerializer serializer,
    string path,
    bool queueDenySharedReceive,
    bool? transactional)
  {
    this._path = path;
    this._queueDenySharedReceive = queueDenySharedReceive;
    this._transactional = transactional;
    this._formatter = (IMessageFormatter) new JsonMessageFormatter(serializer);
    this._innerQueue = this.InitQueue(this._path, queueDenySharedReceive);
  }

  protected MessageQueue InitQueue(string path, bool queueDenySharedReceive)
  {
    MessageQueue queueIfNotExists = MsmqQueueBase.CreateMessageQueueIfNotExists(path, queueDenySharedReceive, this._transactional.GetValueOrDefault());
    queueIfNotExists.DefaultPropertiesToSend.Recoverable = true;
    queueIfNotExists.MessageReadPropertyFilter.CorrelationId = true;
    queueIfNotExists.MessageReadPropertyFilter.Body = true;
    queueIfNotExists.MessageReadPropertyFilter.ArrivedTime = true;
    queueIfNotExists.MessageReadPropertyFilter.Id = true;
    queueIfNotExists.Formatter = this._formatter;
    return queueIfNotExists;
  }

  protected static MessageQueue CreateMessageQueueIfNotExists(
    string path,
    bool queueDenySharedReceive = false,
    bool isTransactional = false)
  {
    bool flag = MsmqExtensions.IsPathLocal(path);
    MessageQueue queueIfNotExists;
    if (flag && !MessageQueue.Exists(path))
    {
      queueIfNotExists = MessageQueue.Create(path, isTransactional);
      string user = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, (SecurityIdentifier) null).Translate(typeof (NTAccount)).Value;
      queueIfNotExists.SetPermissions(user, MessageQueueAccessRights.FullControl);
      if (MsmqQueueBase.DoNotRestrictQueueSize())
        return queueIfNotExists;
      try
      {
        queueIfNotExists.MaximumQueueSize = 102400L /*0x019000*/;
      }
      catch (Exception ex)
      {
        PXTrace.WriteError(ex);
      }
    }
    else
    {
      queueIfNotExists = new MessageQueue(path, queueDenySharedReceive, true, QueueAccessMode.SendAndReceive);
      if (flag && !MsmqQueueBase.DoNotRestrictQueueSize())
      {
        if (queueIfNotExists.MaximumQueueSize > 1048576L /*0x100000*/)
        {
          try
          {
            queueIfNotExists.MaximumQueueSize = 1048576L /*0x100000*/;
            goto label_10;
          }
          catch (Exception ex)
          {
            PXTrace.WriteError(ex);
            goto label_10;
          }
        }
      }
      return queueIfNotExists;
    }
label_10:
    return queueIfNotExists;
  }

  private void CreateAdminQueueIfNotExists()
  {
    string str = ".\\private$\\adminQueue" + MsmqQueueBase.GetUniqueQueueId();
    int num = MsmqExtensions.IsPathLocal(str) ? 1 : 0;
    MessageQueue messageQueue;
    if (num != 0 && !MessageQueue.Exists(str))
    {
      messageQueue = MessageQueue.Create(str, false);
      string user = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, (SecurityIdentifier) null).Translate(typeof (NTAccount)).Value;
      messageQueue.SetPermissions(user, MessageQueueAccessRights.FullControl);
    }
    else
      messageQueue = new MessageQueue(str, false, true, QueueAccessMode.SendAndReceive);
    if (num != 0 && messageQueue.MaximumQueueSize >= 1048576L /*0x100000*/)
    {
      if (!MsmqQueueBase.DoNotRestrictQueueSize())
      {
        try
        {
          messageQueue.MaximumQueueSize = System.Math.Min(messageQueue.MaximumQueueSize, 102400L /*0x019000*/);
        }
        catch (Exception ex)
        {
          PXTrace.WriteError(ex);
        }
      }
    }
    this._adminQueue = messageQueue;
  }

  protected static string FormatCorrelationId(Guid correlationId)
  {
    return correlationId.ToString("D") + "\\32";
  }

  protected static Guid ParseCorrelationId(string correlationId)
  {
    return new Guid(StringExtensions.FirstSegment(correlationId, '\\'));
  }

  public void Dispose() => this.CleanInnerQueue();

  protected void CleanInnerQueue()
  {
    this._innerQueue?.Dispose();
    this._innerQueue = (MessageQueue) null;
  }

  public static string GetUniqueQueueId()
  {
    return PXCriptoHelperMSMQ.GetUniqueQueueId(HostingEnvironment.SiteName, HostingEnvironment.ApplicationVirtualPath);
  }

  public void ReInitQueue()
  {
    this._innerQueue?.Dispose();
    MsmqQueueBase._lastFailedToCommitMessage = (string) null;
    this._innerQueue = this.InitQueue(this._path, this._queueDenySharedReceive);
  }

  public int GetQueueCount()
  {
    return this._innerQueue == null || !MsmqExtensions.IsPathLocal($"{this._innerQueue.MachineName}\\{this._innerQueue.QueueName}") ? -1 : this._innerQueue.GetCount();
  }

  public System.DateTime GetLastHeartBeat() => new System.DateTime(this._lastHeartBeat);

  public string GetQueueName() => this._path;

  public void PurgeQueue()
  {
    if (!MsmqExtensions.IsPathLocal(this._path))
      throw new PXInvalidOperationException("Can not purge non local message queue");
    this.EnsureInnerQueue();
    this._innerQueue.Purge();
  }

  public string GetLastFailedToCommitMessage() => MsmqQueueBase._lastFailedToCommitMessage;

  public (long current, long max) GetQueueSize()
  {
    MessageQueue innerQueue1 = this._innerQueue;
    long num1 = innerQueue1 != null ? innerQueue1.GetSize() / 1024L /*0x0400*/ : -1L;
    MessageQueue innerQueue2 = this._innerQueue;
    long num2 = innerQueue2 != null ? innerQueue2.MaximumQueueSize : -1L;
    return (num1, num2);
  }

  protected void CheckAdminQueueIfSuccess()
  {
    int count = this._adminQueue.GetCount();
    if (count <= 0)
      return;
    try
    {
      for (int index = 0; index < count; ++index)
      {
        Message message = this._adminQueue.Receive(new TimeSpan(0, 0, 0, 0, 10));
        if (message != null && message.Acknowledgment == Acknowledgment.QueueExceedMaximumSize || message != null && message.Acknowledgment == Acknowledgment.AccessDenied)
        {
          if (message.BodyStream != null)
          {
            using (StreamReader streamReader = new StreamReader(message.BodyStream))
              MsmqQueueBase._lastFailedToCommitMessage = streamReader.ReadToEnd();
          }
          else
            MsmqQueueBase._lastFailedToCommitMessage = message.Label;
        }
      }
    }
    catch
    {
    }
  }
}
