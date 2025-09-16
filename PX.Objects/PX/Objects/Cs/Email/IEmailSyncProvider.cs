// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.IEmailSyncProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.CR;
using PX.SM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CS.Email;

public interface IEmailSyncProvider : IDisposable
{
  bool AllowContactsSync { get; }

  bool AllowTasksSync { get; }

  bool AllowEventsSync { get; }

  bool AllowEmailsSync { get; }

  void ContactsSync(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes);

  void TasksSync(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes);

  void EventsSync(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes);

  void EmailsSync(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes);

  void SendMessage(PXSyncMailbox mailbox, IEnumerable<CRSMEmail> activities);

  IEnumerable<CRSMEmail> ReceiveMessage(PXSyncMailbox mailbox);
}
