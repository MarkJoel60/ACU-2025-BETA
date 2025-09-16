// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.BaseExchangeSyncProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.Update;
using PX.Data.Update.WebServices;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Reflection;
using System.Text;

#nullable disable
namespace PX.Objects.CS.Email;

public abstract class BaseExchangeSyncProvider : IDisposable
{
  public readonly EMailSyncServer Account;
  public readonly EMailSyncPolicy Policy;
  public readonly PXSyncCache Cache;
  public PXExchangeEventDelegate Logger;

  public void LogVerbose(string mailbox, string message, params object[] args)
  {
    this.LogEvent(new PXExchangeEvent(mailbox, EventLevel.Verbose, string.Format(message, args), (Exception) null, (string[]) null));
  }

  public void LogInfo(string mailbox, string message, params object[] args)
  {
    this.LogEvent(new PXExchangeEvent(mailbox, EventLevel.Informational, string.Format(message, args), (Exception) null, (string[]) null));
  }

  public void LogWarning(string mailbox, string message, params object[] args)
  {
    this.LogEvent(new PXExchangeEvent(mailbox, EventLevel.Warning, string.Format(message, args), (Exception) null, (string[]) null));
  }

  public void LogError(string mailbox, Exception error, string message = null)
  {
    this.LogEvent(new PXExchangeEvent(mailbox, EventLevel.Error, message, error, (string[]) null));
  }

  public void LogResult(PXSyncResult result)
  {
    string str1 = string.IsNullOrEmpty(result.ActionTitle) ? (result.ItemStatus == PXSyncItemStatus.None ? "Processing" : result.ItemStatus.ToString()) : result.ActionTitle;
    if (result.Success)
    {
      string str2 = PXMessages.LocalizeFormatNoPrefix("{0} of {1}, mailbox '{2}'. The processing of the '{3}' item with the {4} status has completed successfully.", new object[5]
      {
        (object) result.Direction.ToString(),
        (object) result.OperationTitle,
        (object) result.Address,
        (object) result.DisplayKey,
        (object) str1
      });
      this.LogEvent(new PXExchangeEvent(result.Address, EventLevel.Informational, str2, result.Error, (string[]) null)
      {
        Date = result.Date
      });
    }
    else
    {
      string errorMessage = this.CreateErrorMessage(true, PXMessages.LocalizeFormatNoPrefix("{0} of {1}, mailbox '{2}'. The processing of the '{3}' item with the {4} status has failed.", new object[5]
      {
        (object) result.Direction.ToString(),
        (object) result.OperationTitle,
        (object) result.Address,
        (object) result.DisplayKey,
        (object) str1
      }), result.Message, result.Error, result.Details);
      this.LogEvent(new PXExchangeEvent(result.Address, EventLevel.Error, errorMessage, result.Error, (string[]) null)
      {
        Date = result.Date
      });
    }
  }

  public void LogEvent(PXExchangeEvent occasion)
  {
    bool flag = false;
    switch (occasion.Level)
    {
      case EventLevel.LogAlways:
      case EventLevel.Verbose:
        if (this.Account.LoggingLevel == "V")
        {
          flag = true;
          break;
        }
        break;
      case EventLevel.Critical:
      case EventLevel.Error:
      case EventLevel.Warning:
        if (this.Account.LoggingLevel != null && this.Account.LoggingLevel != "N")
        {
          flag = true;
          break;
        }
        break;
      case EventLevel.Informational:
        if (this.Account.LoggingLevel == "I" || this.Account.LoggingLevel == "V")
        {
          flag = true;
          break;
        }
        break;
    }
    if (!flag)
      return;
    if (this.Logger != null)
      this.Logger.Invoke(occasion);
    this.SaveEvent(occasion);
  }

  public string CreateErrorMessage(
    bool detailed,
    string text,
    string message,
    Exception error,
    string[] details)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine(text);
    if (message != null)
      stringBuilder.AppendLine(message);
    if (error != null)
    {
      string str = PXExchangeServer.MapErrors(error.ToString());
      stringBuilder.AppendLine(detailed ? str : error.Message);
    }
    if (details != null)
    {
      stringBuilder.AppendLine();
      foreach (string detail in details)
        stringBuilder.AppendLine(detail);
    }
    if (error is PXOuterException)
    {
      foreach (string innerMessage in (error as PXOuterException).InnerMessages)
        stringBuilder.AppendLine(innerMessage);
    }
    return stringBuilder.ToString();
  }

  protected void SaveEvent(PXExchangeEvent occasion)
  {
    if (occasion == null)
      return;
    PXDatabase.Insert<EMailSyncLog>(new PXDataFieldAssign[6]
    {
      (PXDataFieldAssign) new PXDataFieldAssign<EMailSyncLog.serverID>((object) this.Account.AccountID),
      (PXDataFieldAssign) new PXDataFieldAssign<EMailSyncLog.address>((object) ((PXExchangeItemBase) occasion).Address),
      (PXDataFieldAssign) new PXDataFieldAssign<EMailSyncLog.level>((object) (byte) occasion.Level),
      (PXDataFieldAssign) new PXDataFieldAssign<EMailSyncLog.date>((object) occasion.Date),
      (PXDataFieldAssign) new PXDataFieldAssign<EMailSyncLog.message>((object) occasion.Message),
      (PXDataFieldAssign) new PXDataFieldAssign<EMailSyncLog.details>(occasion.Details == null ? (object) (string) null : (object) string.Join(Environment.NewLine, occasion.Details))
    });
  }

  protected BaseExchangeSyncProvider(EMailSyncServer account, EMailSyncPolicy policy)
  {
    if (account == null)
      throw new ArgumentNullException(nameof (account));
    if (policy == null)
      throw new ArgumentNullException(nameof (policy));
    this.Account = account;
    this.Policy = policy;
    this.Cache = new PXSyncCache();
    this.LogVerbose((string) null, "Exchange sync provider for policy '{0}' has been initialised.", (object) this.Policy.PolicyName);
  }

  public void Dispose()
  {
  }

  public bool AllowContactsSync => this.IsMethodOverride("ContactsSync");

  public bool AllowTasksSync => this.IsMethodOverride("TasksSync");

  public bool AllowEventsSync => this.IsMethodOverride("EventsSync");

  public bool AllowEmailsSync => this.IsMethodOverride("EmailsSync");

  [MethodDisabled]
  public virtual void ContactsSync(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes)
  {
    throw new PXException("Exchange provider does not support this operation.");
  }

  [MethodDisabled]
  public virtual void TasksSync(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes)
  {
    throw new PXException("Exchange provider does not support this operation.");
  }

  [MethodDisabled]
  public virtual void EventsSync(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes)
  {
    throw new PXException("Exchange provider does not support this operation.");
  }

  [MethodDisabled]
  public virtual void EmailsSync(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes)
  {
    throw new PXException("Exchange provider does not support this operation.");
  }

  protected bool IsMethodOverride(string name)
  {
    foreach (MethodInfo method in this.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public))
    {
      if (!(method == (MethodInfo) null) && !(method.Name != name) && method.GetCustomAttributes(typeof (MethodDisabledAttribute), false).Length == 0)
        return true;
    }
    return false;
  }
}
