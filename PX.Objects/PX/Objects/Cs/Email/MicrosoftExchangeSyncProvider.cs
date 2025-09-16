// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.MicrosoftExchangeSyncProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.Update.WebServices;
using PX.Objects.CR;
using PX.SM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CS.Email;

public class MicrosoftExchangeSyncProvider(EMailSyncServer server, EMailSyncPolicy policy) : 
  BaseExchangeSyncProvider(server, policy),
  IEmailSyncProvider,
  IDisposable
{
  protected PXExchangeServer _gate;

  public PXExchangeServer Gate
  {
    get
    {
      if (this._gate == null)
      {
        this._gate = PXExchangeServer.GetGate(this.Account, (PXExchangeEventDelegate) null);
        PXExchangeServer gate = this._gate;
        gate.Logger = (PXExchangeEventDelegate) Delegate.Combine((Delegate) gate.Logger, (Delegate) new PXExchangeEventDelegate((object) this, __methodptr(LogEvent)));
      }
      return this._gate;
    }
  }

  public override void ContactsSync(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes)
  {
    using (ExchangeBaseSyncCommand exchangeBaseSyncCommand = (ExchangeBaseSyncCommand) new ExchangeContactsSyncCommand(this))
      exchangeBaseSyncCommand.ProcessSync(policy, direction, mailboxes);
  }

  public override void TasksSync(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes)
  {
    using (ExchangeBaseSyncCommand exchangeBaseSyncCommand = (ExchangeBaseSyncCommand) new ExchangeTasksSyncCommand(this))
      exchangeBaseSyncCommand.ProcessSync(policy, direction, mailboxes);
  }

  public override void EventsSync(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes)
  {
    using (ExchangeBaseSyncCommand exchangeBaseSyncCommand = (ExchangeBaseSyncCommand) new ExchangeEventsSyncCommand(this))
      exchangeBaseSyncCommand.ProcessSync(policy, direction, mailboxes);
  }

  public override void EmailsSync(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes)
  {
    using (ExchangeBaseSyncCommand exchangeBaseSyncCommand = (ExchangeBaseSyncCommand) new ExchangeEmailsSyncCommand(this))
      exchangeBaseSyncCommand.ProcessSync(policy, direction, mailboxes);
  }

  public void SendMessage(PXSyncMailbox mailbox, IEnumerable<CRSMEmail> activities)
  {
    using (ExchangeEmailsSyncCommand emailsSyncCommand = new ExchangeEmailsSyncCommand(this))
      emailsSyncCommand.SendMessage(mailbox, activities);
  }

  public IEnumerable<CRSMEmail> ReceiveMessage(PXSyncMailbox mailbox)
  {
    using (new ExchangeEmailsSyncCommand(this))
      throw new NotImplementedException();
  }
}
