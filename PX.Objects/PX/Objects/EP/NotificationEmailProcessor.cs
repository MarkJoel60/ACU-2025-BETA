// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.NotificationEmailProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using System.Collections.Generic;
using System.Net.Mail;

#nullable disable
namespace PX.Objects.EP;

public class NotificationEmailProcessor : BaseRoutingProcessor
{
  protected override bool Process(BasicEmailProcessor.Package package)
  {
    PXGraph graph = package.Graph;
    CRSMEmail message = package.Message;
    if (!message.IsIncome.GetValueOrDefault())
      return false;
    int num = message.ClassID.GetValueOrDefault() == -2 ? 1 : 0;
    BaseRoutingProcessor.MailAddressList mailAddressList = new BaseRoutingProcessor.MailAddressList();
    if (num != 0)
      mailAddressList.AddRange((IEnumerable<MailAddress>) this.GetFromInternal(graph, message));
    else
      mailAddressList.AddRange((IEnumerable<MailAddress>) this.GetFromExternal(graph, message));
    this.RemoveAddress(mailAddressList, message.MailFrom);
    this.RemoveAddress(mailAddressList, message.MailTo);
    this.RemoveAddress(mailAddressList, message.MailCc);
    this.RemoveAddress(mailAddressList, message.MailBcc);
    if (mailAddressList.Count == 0)
      return false;
    this.SendCopyMessageToInside(package.Graph, package.Account, message, (IEnumerable<MailAddress>) mailAddressList);
    GraphHelper.EnsureCachePersistence(graph, ((object) message).GetType());
    return true;
  }

  private BaseRoutingProcessor.MailAddressList GetFromInternal(PXGraph graph, CRSMEmail message)
  {
    CRSMEmail routingMessage = this.GetRoutingMessage(graph, message);
    if (routingMessage == null)
      return (BaseRoutingProcessor.MailAddressList) null;
    BaseRoutingProcessor.MailAddressList recipients = new BaseRoutingProcessor.MailAddressList();
    recipients.AddRange((IEnumerable<MailAddress>) this.GetOwnerAddressByNote(graph, routingMessage.RefNoteID, message.OwnerID));
    this.RemoveAddress(recipients, routingMessage.MailFrom);
    this.RemoveAddress(recipients, routingMessage.MailTo);
    this.RemoveAddress(recipients, routingMessage.MailCc);
    this.RemoveAddress(recipients, routingMessage.MailBcc);
    return recipients;
  }

  private BaseRoutingProcessor.MailAddressList GetFromExternal(PXGraph graph, CRSMEmail message)
  {
    BaseRoutingProcessor.MailAddressList recipients = new BaseRoutingProcessor.MailAddressList();
    recipients.AddRange((IEnumerable<MailAddress>) this.GetOwnerAddressByNote(graph, message.RefNoteID, message.OwnerID));
    CRSMEmail routingMessage = this.GetRoutingMessage(graph, message);
    if (routingMessage != null)
    {
      this.RemoveAddress(recipients, routingMessage.MailFrom);
      this.RemoveAddress(recipients, routingMessage.MailTo);
      this.RemoveAddress(recipients, routingMessage.MailCc);
      this.RemoveAddress(recipients, routingMessage.MailBcc);
    }
    return recipients;
  }

  private CRSMEmail GetRoutingMessage(PXGraph graph, CRSMEmail message)
  {
    return PXResultset<CRSMEmail>.op_Implicit(PXSelectBase<CRSMEmail, PXSelect<CRSMEmail, Where<CRSMEmail.responseToNoteID, Equal<Required<CRSMEmail.emailNoteID>>>>.Config>.SelectWindowed(graph, 0, 1, new object[1]
    {
      (object) message.EmailNoteID
    }));
  }
}
