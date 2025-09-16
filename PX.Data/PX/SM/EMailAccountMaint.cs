// Decompiled with JetBrains decompiler
// Type: PX.SM.EMailAccountMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.IMAP.Client;
using PX.Common.Mail;
using PX.Common.POP3.Client;
using PX.Common.SMTP;
using PX.Common.SMTP.Client;
using PX.Common.TCP;
using PX.Data;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM.AU;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

public class EMailAccountMaint : PXGraph<EMailAccountMaint, EMailAccount>, ICaptionable
{
  public PXSelect<EMailAccount> EMailAccounts;
  public PXSelect<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Current<EMailAccount.emailAccountID>>>> CurrentEMailAccounts;
  public PXSelect<EMailSyncAccount, Where<EMailSyncAccount.emailAccountID, Equal<Current<EMailAccount.emailAccountID>>>> SyncAccount;
  public PXSelect<PreferencesEmail> Preferences;
  public PXSelect<PX.SM.EMailAccountStatistics, Where<PX.SM.EMailAccountStatistics.emailAccountID, Equal<Current<EMailAccount.emailAccountID>>>> EMailAccountStatistics;
  public PXAction<EMailAccount> SendAll;
  public PXAction<EMailAccount> ReceiveAll;
  public PXAction<EMailAccount> SendReceiveAll;

  [InjectDependency]
  protected IMailSendProvider MailSendProvider { get; private set; }

  [InjectDependency]
  protected IMailReceiveProvider MailReceiveProvider { get; private set; }

  public EMailAccountMaint()
  {
    this.Actions.Move("Cancel", "CancelClose");
    this.Actions.Move("Save", "SaveClose");
    PXUIFieldAttribute.SetDisplayName<EMailAccount.deleteUnProcessed>(this.Caches[typeof (EMailAccount)], "Delete Emails");
    PXUIFieldAttribute.SetRequired<EMailAccount.description>(this.Caches[typeof (EMailAccount)], true);
    PXUIFieldAttribute.SetRequired<EMailAccount.outcomingHostName>(this.Caches[typeof (EMailAccount)], true);
    PXUIFieldAttribute.SetRequired<EMailAccount.address>(this.Caches[typeof (EMailAccount)], true);
  }

  public string Caption()
  {
    EMailAccount current = this.EMailAccounts.Current;
    string str;
    return current == null || string.IsNullOrEmpty(current.Description) || !(this.EMailAccounts.Cache.GetStateExt<EMailAccount.emailAccountType>((object) current) is PXStringState stateExt) || !stateExt.ValueLabelDic.TryGetValue(current.EmailAccountType, out str) ? "New Record" : $"{current.Description} - {str}";
  }

  [PXUIField(DisplayName = "Send All", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  protected virtual IEnumerable sendAll(PXAdapter adapter)
  {
    this.Actions.PressSave();
    IEnumerable<EMailAccount> accounts = adapter.Get().Cast<EMailAccount>();
    PXLongOperation.StartOperation(this.UID, (PXToggleAsyncDelegate) (() => EMailAccountMaint.PerformEmailingOperation(accounts, EMailAccountMaint.EmailingOperation.Send)));
    return (IEnumerable) accounts;
  }

  [PXUIField(DisplayName = "Receive All", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  protected virtual IEnumerable receiveAll(PXAdapter adapter)
  {
    this.Actions.PressSave();
    IEnumerable<EMailAccount> accounts = adapter.Get().Cast<EMailAccount>();
    PXLongOperation.StartOperation(this.UID, (PXToggleAsyncDelegate) (() => EMailAccountMaint.PerformEmailingOperation(accounts, EMailAccountMaint.EmailingOperation.Receive)));
    return (IEnumerable) accounts;
  }

  [PXUIField(DisplayName = "Send/Receive All", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  protected virtual IEnumerable sendReceiveAll(PXAdapter adapter)
  {
    this.Actions.PressSave();
    IEnumerable<EMailAccount> accounts = adapter.Get().Cast<EMailAccount>();
    PXLongOperation.StartOperation(this.UID, (PXToggleAsyncDelegate) (() => EMailAccountMaint.PerformEmailingOperation(accounts, EMailAccountMaint.EmailingOperation.SendAndReceive)));
    return (IEnumerable) accounts;
  }

  private static void PerformEmailingOperation(
    IEnumerable<EMailAccount> accounts,
    EMailAccountMaint.EmailingOperation operation)
  {
    EMailAccountMaint graph = PXGraph.CreateInstance<EMailAccountMaint>();
    using (IEnumerator<EMailAccount> enumerator = accounts.GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        EMailAccount account = enumerator.Current;
        Exception receiveException = (Exception) null;
        Exception sendException = (Exception) null;
        bool? nullable = account.SupportReceiving;
        if (nullable.HasValue && !nullable.GetValueOrDefault())
        {
          nullable = account.SupportSending;
          if (nullable.HasValue && !nullable.GetValueOrDefault())
            throw new PXException("The {0} email account does nоt support sending and receiving emails.", new object[1]
            {
              (object) account.EmailAccountID
            });
        }
        if (operation == EMailAccountMaint.EmailingOperation.Receive)
        {
          nullable = account.SupportReceiving;
          if (nullable.HasValue && !nullable.GetValueOrDefault())
            throw new PXException("The {0} email account does nоt support receiving emails.", new object[1]
            {
              (object) account.EmailAccountID
            });
        }
        if (operation == EMailAccountMaint.EmailingOperation.Send)
        {
          nullable = account.SupportSending;
          if (nullable.HasValue && !nullable.GetValueOrDefault())
            throw new PXException("The {0} email account does nоt support sending emails.", new object[1]
            {
              (object) account.EmailAccountID
            });
        }
        if (operation.HasFlag((Enum) EMailAccountMaint.EmailingOperation.Receive))
        {
          nullable = account.SupportReceiving;
          if (nullable.HasValue && nullable.GetValueOrDefault())
            receiveException = EMailAccountMaint.SafelyPerform((System.Action) (() =>
            {
              using (new PXConnectionScope())
              {
                if (!account.EmailAccountID.HasValue)
                  return;
                ExceptionExtensions.CheckIfNull<IMailReceiveProvider>(graph.MailReceiveProvider, "graph.MailReceiveProvider", (string) null).Receive(account.EmailAccountID.Value);
              }
            }));
        }
        if (operation.HasFlag((Enum) EMailAccountMaint.EmailingOperation.Send))
        {
          nullable = account.SupportSending;
          if (nullable.HasValue && nullable.GetValueOrDefault())
            sendException = EMailAccountMaint.SafelyPerform((System.Action) (() =>
            {
              using (new PXConnectionScope())
              {
                if (!account.EmailAccountID.HasValue)
                  return;
                ExceptionExtensions.CheckIfNull<IMailSendProvider>(graph.MailSendProvider, "graph.MailSendProvider", (string) null).Send(account.EmailAccountID.Value);
              }
            }));
        }
        EMailAccountMaint.ThrowIfSendOrReceiveIsError(sendException, receiveException);
        PX.SM.EMailAccountStatistics accountStatistics1 = PX.SM.EMailAccountStatistics.PK.Find((PXGraph) graph, account.EmailAccountID);
        if (accountStatistics1 == null)
        {
          PX.SM.EMailAccountStatistics accountStatistics2 = new PX.SM.EMailAccountStatistics()
          {
            EmailAccountID = account.EmailAccountID
          };
          accountStatistics1 = graph.EMailAccountStatistics.Insert(accountStatistics2);
        }
        if (operation.HasFlag((Enum) EMailAccountMaint.EmailingOperation.Receive))
          accountStatistics1.LastReceiveDateTime = new System.DateTime?(PXTimeZoneInfo.Now);
        if (operation.HasFlag((Enum) EMailAccountMaint.EmailingOperation.Send))
          accountStatistics1.LastSendDateTime = new System.DateTime?(PXTimeZoneInfo.Now);
        graph.EMailAccountStatistics.Update(accountStatistics1);
        graph.Save.Press();
      }
    }
    EMailAccountMaint.DoArchive(graph);
  }

  private static void ThrowIfSendOrReceiveIsError(
    Exception sendException,
    Exception receiveException)
  {
    if (sendException == null && receiveException == null)
      return;
    if (receiveException == null)
      throw new PXException(sendException, "The mail send has failed.\r\n{0}", new object[1]
      {
        (object) sendException.Message
      });
    if (sendException == null)
      throw new PXException(receiveException, "The mail receive has failed.\r\n{0}", new object[1]
      {
        (object) receiveException.Message
      });
    try
    {
      throw new AggregateException(new Exception[2]
      {
        sendException,
        receiveException
      });
    }
    catch (AggregateException ex)
    {
      throw new PXException("Mail sending and receiving has failed.", (Exception) ex);
    }
  }

  private static void DoArchive(EMailAccountMaint graph)
  {
    int? archiveEmailsOlderThan = graph.Preferences.SelectSingle().ArchiveEmailsOlderThan;
    if (!archiveEmailsOlderThan.HasValue)
      return;
    int? nullable = archiveEmailsOlderThan;
    int num = 0;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return;
    PXUpdate<Set<SMEmail.isArchived, PX.Data.True>, SMEmail, Where2<Where<SMEmail.mailAccountID, IsNull, Or<SMEmail.mailAccountID, Equal<Required<EMailAccount.emailAccountID>>>>, And<SMEmail.mpstatus, Equal<MailStatusListAttribute.processed>, And<SMEmail.isArchived, NotEqual<PX.Data.True>, And<SMEmail.createdDateTime, Less<Required<SMEmail.createdDateTime>>>>>>>.Update((PXGraph) graph, (object) graph.EMailAccounts.Current.EmailAccountID, (object) PXTimeZoneInfo.Now.AddMonths(-archiveEmailsOlderThan.Value));
  }

  [PXInternalUseOnly]
  public static void WrapException(System.Action handler, string format)
  {
    try
    {
      handler();
    }
    catch (Exception ex)
    {
      string message;
      switch (ex)
      {
        case PXTcpConnectionException connectionException:
          message = PXMessages.LocalizeFormatNoPrefix("Cannot connect to the host {0} with port {1}. Original error: {2}.", (object) connectionException.Host, (object) connectionException.Port, (object) connectionException.InnerException.Message);
          break;
        case MailSender.SmtpSender.FromFieldDoesntMatchSenderEmailAddressException addressException:
          message = PXMessages.LocalizeFormatNoPrefix(addressException.IsOAuthLoginInfoDiffers ? "The email address used to obtain the OAuth token does not match the email address specified in the From box of the email. The SMTP server requires that the specified email address matches the email address of the authenticated user." : "The email address of the email account does not match the email address specified in the From box of the email. The SMTP server requires that the specified email address matches the email address of the authenticated user.");
          break;
        case TimeoutException timeoutException1 when timeoutException1.Message == "The POP3 connection could not be established because of a timeout error.":
          message = PXMessages.LocalizeNoPrefix("The POP3 connection could not be established because of a timeout error.");
          break;
        case TimeoutException timeoutException2 when timeoutException2.Message == "The IMAP connection could not be established because of a timeout error.":
          message = PXMessages.LocalizeNoPrefix("The IMAP connection could not be established because of a timeout error.");
          break;
        case ImapClientException imapClientException:
          message = imapClientException.ResponseText;
          break;
        case Pop3ClientException pop3ClientException:
          message = pop3ClientException.ResponseText;
          break;
        case SmtpClientException smtpClientException:
          message = $"{smtpClientException.SmptReplyLines[0].ReplyCode.ToString()}-{smtpClientException.SmptReplyLines[0].EnhancedStatusCode} {string.Join(" \r\n", ((IEnumerable<SmptReplyLine>) smtpClientException.SmptReplyLines).Select<SmptReplyLine, string>((Func<SmptReplyLine, string>) (l => l.Text)))}";
          break;
        default:
          message = !EnumerableExtensions.IsIn<string>(ex.Message, "Emails cannot be received because the account you signed in with does not have permission for using the email address specified in the system email account on the System Email Accounts (SM204002) form.", "The email cannot be sent because the account you signed in with does not have permission for using the email address specified in the system email account on the System Email Accounts (SM204002) form.", "The email was not marked as read after it was received on the mail server.", "The email was not marked as unread after it was received on the mail server.", "After the email was received, it was not marked as deleted on the mail server.", new string[1]
          {
            "An IMAP item could not be fetched because of a timeout error."
          }) ? ex.Message : PXMessages.LocalizeNoPrefix(ex.Message);
          break;
      }
      if (format != null)
        throw new PXException(ex, format, new object[1]
        {
          (object) message
        });
      throw new PXException(message, ex);
    }
  }

  [PXInternalUseOnly]
  public static Exception SafelyPerform(System.Action action, string format = null)
  {
    try
    {
      EMailAccountMaint.WrapException(action, format);
    }
    catch (Exception ex)
    {
      return ex;
    }
    return (Exception) null;
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXCustomizeSelectorColumns(new System.Type[] {typeof (EMailAccount.address), typeof (EMailAccount.isActive), typeof (EMailAccount.description), typeof (EMailAccount.emailAccountType), typeof (EMailAccount.replyAddress)})]
  protected virtual void _(
    Events.CacheAttached<EMailAccount.emailAccountID> e)
  {
  }

  [PXRSACryptString]
  [PXUIField(DisplayName = "Incoming Mail Password")]
  protected virtual void _(Events.CacheAttached<EMailAccount.password> e)
  {
  }

  [PXFormula(typeof (PX.Data.True))]
  protected virtual void _(
    Events.CacheAttached<EMailAccount.passwordIsDecrypted> e)
  {
  }

  [PXRSACryptString]
  [PXUIField(DisplayName = "Outgoing Mail Password")]
  protected virtual void _(
    Events.CacheAttached<EMailAccount.outcomingPassword> e)
  {
  }

  [PXDBString(250, IsUnicode = true)]
  [PXDefault("Inbox")]
  [PXUIField(DisplayName = "Root Folder")]
  protected virtual void _(
    Events.CacheAttached<EMailAccount.imapRootFolder> e)
  {
  }

  protected virtual void _(Events.RowSelected<EMailAccount> e)
  {
    EMailAccount row = e.Row;
    if (row == null)
      return;
    int? incomingHostProtocol1 = row.IncomingHostProtocol;
    int num1 = 0;
    if (incomingHostProtocol1.GetValueOrDefault() == num1 & incomingHostProtocol1.HasValue)
      e.Cache.RaiseExceptionHandling<EMailAccount.incomingHostProtocol>((object) row, (object) row.IncomingHostProtocol, (Exception) new PXSetPropertyException("The POP3 protocol is not well suited for retrieving large mailboxes because all messages are retrieved from the server every time the mail is collected. We recommend that you use IMAP protocol instead.", PXErrorLevel.Warning));
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = e.Cache.AdjustUI((object) row).For<EMailAccount.confirmReceipt>((System.Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = ui;
      bool? incomingProcessing = row.IncomingProcessing;
      bool flag = true;
      int num2 = incomingProcessing.GetValueOrDefault() == flag & incomingProcessing.HasValue ? 1 : 0;
      pxuiFieldAttribute.Enabled = num2 != 0;
    }));
    chained = chained.SameFor<EMailAccount.confirmReceiptNotificationID>();
    chained = chained.SameFor<EMailAccount.processUnassigned>();
    chained = chained.SameFor<EMailAccount.responseNotificationID>();
    chained = chained.SameFor<EMailAccount.createCase>();
    chained = chained.SameFor<EMailAccount.routeEmployeeEmails>();
    chained = chained.SameFor<EMailAccount.createActivity>();
    chained = chained.SameFor<EMailAccount.createLead>();
    chained = chained.SameFor<EMailAccount.deleteUnProcessed>();
    chained = chained.SameFor<EMailAccount.typeDelete>();
    chained = chained.SameFor<EMailAccount.addUpInformation>();
    chained.For<EMailAccount.addIncomingProcessingTags>((System.Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = ui;
      EMailAccount row1 = e.Row;
      int num3;
      if (row1 == null)
      {
        num3 = 1;
      }
      else
      {
        bool? routeEmployeeEmails = row1.RouteEmployeeEmails;
        bool flag = true;
        num3 = !(routeEmployeeEmails.GetValueOrDefault() == flag & routeEmployeeEmails.HasValue) ? 1 : 0;
      }
      int num4;
      if (num3 == 0)
      {
        EMailAccount row2 = e.Row;
        if (row2 == null)
        {
          num4 = 0;
        }
        else
        {
          bool? forbidRouting = row2.ForbidRouting;
          bool flag = true;
          num4 = forbidRouting.GetValueOrDefault() == flag & forbidRouting.HasValue ? 1 : 0;
        }
      }
      else
        num4 = 1;
      pxuiFieldAttribute.Enabled = num4 != 0;
    }));
    chained = e.Cache.AdjustUI((object) row).For<EMailAccount.typeDelete>((System.Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = ui;
      int num5;
      if (ui.Enabled)
      {
        bool? deleteUnProcessed = row.DeleteUnProcessed;
        bool flag = true;
        num5 = deleteUnProcessed.GetValueOrDefault() == flag & deleteUnProcessed.HasValue ? 1 : 0;
      }
      else
        num5 = 0;
      pxuiFieldAttribute.Enabled = num5 != 0;
    }));
    chained = chained.For<EMailAccount.responseNotificationID>((System.Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = ui;
      int num6;
      if (ui.Enabled)
      {
        bool? processUnassigned = row.ProcessUnassigned;
        bool flag = true;
        num6 = processUnassigned.GetValueOrDefault() == flag & processUnassigned.HasValue ? 1 : 0;
      }
      else
        num6 = 0;
      pxuiFieldAttribute.Enabled = num6 != 0;
    }));
    chained = chained.For<EMailAccount.confirmReceiptNotificationID>((System.Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = ui;
      int num7;
      if (ui.Enabled)
      {
        bool? confirmReceipt = row.ConfirmReceipt;
        bool flag = true;
        num7 = confirmReceipt.GetValueOrDefault() == flag & confirmReceipt.HasValue ? 1 : 0;
      }
      else
        num7 = 0;
      pxuiFieldAttribute.Enabled = num7 != 0;
    }));
    chained = chained.For<EMailAccount.outcomingAuthenticationDifferent>((System.Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = ui;
      int? authenticationType = row.AuthenticationType;
      int num8 = 0;
      int num9 = authenticationType.GetValueOrDefault() > num8 & authenticationType.HasValue ? 1 : 0;
      pxuiFieldAttribute.Enabled = num9 != 0;
    }));
    chained = chained.For<EMailAccount.outcomingLoginName>((System.Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = ui;
      int? authenticationType = row.AuthenticationType;
      int num10 = 1;
      int num11 = authenticationType.GetValueOrDefault() > num10 & authenticationType.HasValue ? 1 : 0;
      pxuiFieldAttribute.Enabled = num11 != 0;
    }));
    chained.SameFor<EMailAccount.outcomingPassword>();
    bool isExchange = row.EmailAccountType == "E";
    PXAction<EMailAccount> sendAll = this.SendAll;
    bool? nullable1;
    bool? nullable2;
    int num12;
    if (row != null)
    {
      nullable1 = row.IsActive;
      if (nullable1.HasValue && nullable1.GetValueOrDefault())
      {
        nullable2 = row.SupportSending;
        if (nullable2.HasValue && nullable2.GetValueOrDefault())
        {
          num12 = !isExchange ? 1 : 0;
          goto label_8;
        }
      }
    }
    num12 = 0;
label_8:
    sendAll.SetEnabled(num12 != 0);
    PXAction<EMailAccount> receiveAll = this.ReceiveAll;
    int num13;
    if (row != null)
    {
      nullable2 = row.IsActive;
      if (nullable2.HasValue && nullable2.GetValueOrDefault())
      {
        nullable1 = row.SupportReceiving;
        if (nullable1.HasValue && nullable1.GetValueOrDefault())
        {
          num13 = !isExchange ? 1 : 0;
          goto label_13;
        }
      }
    }
    num13 = 0;
label_13:
    receiveAll.SetEnabled(num13 != 0);
    PXAction<EMailAccount> sendReceiveAll = this.SendReceiveAll;
    nullable1 = row.IsActive;
    int num14 = !nullable1.HasValue || !nullable1.GetValueOrDefault() ? 0 : (!isExchange ? 1 : 0);
    sendReceiveAll.SetEnabled(num14 != 0);
    this.Delete.SetEnabled(!isExchange);
    bool isNew = e.Cache.GetOriginal((object) row) == null;
    chained = e.Cache.AdjustUI((object) row).For<EMailAccount.description>((System.Action<PXUIFieldAttribute>) (_ => _.Enabled = !isExchange));
    chained = chained.SameFor<EMailAccount.userID>();
    chained = chained.SameFor<EMailAccount.address>();
    chained = chained.SameFor<EMailAccount.sendGroupMails>();
    chained = chained.SameFor<EMailAccount.loginName>();
    chained = chained.SameFor<EMailAccount.password>();
    chained = chained.SameFor<EMailAccount.authenticationType>();
    chained = chained.SameFor<EMailAccount.incomingHostProtocol>();
    chained = chained.SameFor<EMailAccount.incomingHostName>();
    chained = chained.SameFor<EMailAccount.incomingPort>();
    chained = chained.SameFor<EMailAccount.incomingConnectionEncryption>();
    chained = chained.SameFor<EMailAccount.outcomingAuthenticationDifferent>();
    chained = chained.SameFor<EMailAccount.outcomingAuthenticationRequest>();
    chained = chained.SameFor<EMailAccount.outcomingHostName>();
    chained = chained.SameFor<EMailAccount.outcomingLoginName>();
    chained = chained.SameFor<EMailAccount.outcomingPassword>();
    chained = chained.SameFor<EMailAccount.outgoingConnectionEncryption>();
    chained = chained.SameFor<EMailAccount.validateFrom>();
    chained = chained.SameFor<EMailAccount.imapRootFolder>();
    chained = chained.SameFor<EMailAccount.timeout>();
    chained.For<EMailAccount.address>((System.Action<PXUIFieldAttribute>) (_ => _.Enabled &= isNew));
    int num15;
    if (row.EmailAccountType == "S")
    {
      int? incomingHostProtocol2 = row.IncomingHostProtocol;
      int num16 = 1;
      num15 = incomingHostProtocol2.GetValueOrDefault() == num16 & incomingHostProtocol2.HasValue ? 1 : 0;
    }
    else
      num15 = 0;
    bool isVisible = num15 != 0;
    PXUIFieldAttribute.SetVisible<EMailAccount.imapRootFolder>(e.Cache, (object) row, isVisible);
    PXDefaultAttribute.SetPersistingCheck<EMailAccount.imapRootFolder>(e.Cache, (object) row, isVisible ? PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing);
    PXUIFieldAttribute.SetVisible<EMailAccount.fetchingBehavior>(e.Cache, (object) row, isVisible);
    PXDefaultAttribute.SetPersistingCheck<EMailAccount.fetchingBehavior>(e.Cache, (object) row, isVisible ? PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing);
    PXUIFieldAttribute.SetEnabled<EMailAccount.accountDisplayName>(e.Cache, (object) row, row.SenderDisplayNameSource == "A");
  }

  protected virtual void _(Events.RowDeleting<EMailAccount> e)
  {
    EMailAccount row = e.Row;
    if (row == null || row.EmailAccountType != "E")
      return;
    EMailSyncAccount emailSyncAccount = this.SyncAccount.SelectSingle();
    if (emailSyncAccount == null)
      return;
    this.SyncAccount.Delete(emailSyncAccount);
  }

  protected virtual void _(
    Events.FieldUpdated<EMailAccount, EMailAccount.userID> e)
  {
    EMailAccount row = e.Row;
    if (row == null)
      return;
    Users parent = PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<EMailAccount>.By<EMailAccount.userID>.FindParent((PXGraph) this, row);
    if (!row.UserID.HasValue || string.Compare(row.Address, parent?.Email) == 0)
      return;
    string strMessage = $"The email address of the selected user does not match the email address specified in the {"Email Address"} box.";
    PXUIFieldAttribute.SetWarning<EMailAccount.userID>(e.Cache, (object) row, PXMessages.Localize(strMessage));
  }

  protected virtual void _(
    Events.FieldUpdated<EMailAccount, EMailAccount.emailAccountType> e)
  {
    EMailAccount row = e.Row;
    if (row == null || !(row.EmailAccountType == "E"))
      return;
    e.Cache.SetValueExt<EMailAccount.userID>((object) e.Cache, (object) null);
  }

  protected virtual void _(Events.RowPersisting<EMailAccount> e)
  {
    EMailAccount row = e.Row;
    if (row == null)
      return;
    int? nullable;
    if (row.EmailAccountType == "S")
    {
      nullable = row.IncomingHostProtocol;
      int num = 1;
      if (nullable.GetValueOrDefault() == num & nullable.HasValue && string.IsNullOrEmpty(row.ImapRootFolder))
        PXUIFieldAttribute.SetError<EMailAccount.imapRootFolder>(e.Cache, (object) row, PXMessages.Localize("The field cannot be empty."));
    }
    bool? processUnassigned = row.ProcessUnassigned;
    bool flag = true;
    if (processUnassigned.GetValueOrDefault() == flag & processUnassigned.HasValue)
    {
      nullable = row.ResponseNotificationID;
      if (!nullable.HasValue)
      {
        string name = typeof (EMailAccount.responseNotificationID).Name;
        if (e.Cache.RaiseExceptionHandling(name, (object) e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) $"[{name}]"
        })))
          throw new PXRowPersistingException(name, (object) null, "'{0}' cannot be empty.", new object[1]
          {
            (object) name
          });
      }
    }
    if (row.EmailAccountType == "S" && string.IsNullOrEmpty(row.OutcomingHostName))
    {
      string name = typeof (EMailAccount.outcomingHostName).Name;
      e.Cancel = true;
      throw new PXRowPersistingException(name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) name
      });
    }
    if (string.IsNullOrEmpty(row.Description))
    {
      string name = typeof (EMailAccount.description).Name;
      e.Cancel = true;
      throw new PXRowPersistingException(name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) name
      });
    }
    if (string.IsNullOrEmpty(row.Address))
    {
      string name = typeof (EMailAccount.address).Name;
      e.Cancel = true;
      throw new PXRowPersistingException(name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) name
      });
    }
  }

  public override bool IsDirty
  {
    get
    {
      if (!PXLongOperation.Exists(this.UID) || PXLongOperation.GetCustomInfo(this.UID) == null || !(PXLongOperation.GetCustomInfo(this.UID).GetType() == typeof (object)))
        return base.IsDirty;
      foreach (KeyValuePair<System.Type, PXCache> cach in (Dictionary<System.Type, PXCache>) this.Caches)
      {
        if (this.Views.Caches.Contains(cach.Key) && cach.Value.IsDirty)
          return true;
      }
      return false;
    }
  }

  [Flags]
  private enum EmailingOperation
  {
    Receive = 1,
    Send = 2,
    SendAndReceive = Send | Receive, // 0x00000003
  }
}
