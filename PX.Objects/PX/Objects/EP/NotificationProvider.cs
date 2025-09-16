// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.NotificationProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.EP;
using PX.Objects.CR;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

public class NotificationProvider : INotificationSender
{
  private IMailSendProvider _mailSendProvider;

  public NotificationProvider(IMailSendProvider mailSendProvider)
  {
    this._mailSendProvider = mailSendProvider;
  }

  public IEnumerable<IBqlTable> Notify(EmailNotificationParameters parameters)
  {
    NotificationGenerator notificationGenerator = this.InitializeGenerator(parameters);
    return notificationGenerator == null ? Enumerable.Empty<IBqlTable>() : (IEnumerable<IBqlTable>) notificationGenerator.Send();
  }

  public IEnumerable<IBqlTable> NotifyAndDeliver(EmailNotificationParameters parameters)
  {
    NotificationGenerator notificationGenerator = this.InitializeGenerator(parameters);
    if (notificationGenerator == null)
      return Enumerable.Empty<IBqlTable>();
    List<CRSMEmail> list = notificationGenerator.Send().ToList<CRSMEmail>();
    foreach (object message in notificationGenerator.CastToSMEmail((IEnumerable<CRSMEmail>) list))
      this._mailSendProvider.SendMessage(message);
    return (IEnumerable<IBqlTable>) list;
  }

  public NotificationGenerator InitializeGenerator(EmailNotificationParameters parameters)
  {
    if (parameters == null)
      return (NotificationGenerator) null;
    int? notificationId1 = parameters.NotificationID;
    NotificationGenerator notificationGenerator1;
    if (!notificationId1.HasValue)
    {
      notificationGenerator1 = new NotificationGenerator();
    }
    else
    {
      object row = parameters.Row;
      notificationId1 = parameters.NotificationID;
      int? notificationId2 = new int?(notificationId1.Value);
      notificationGenerator1 = (NotificationGenerator) TemplateNotificationGenerator.Create(row, notificationId2);
    }
    NotificationGenerator notificationGenerator2 = notificationGenerator1;
    notificationGenerator2.MailAccountId = parameters.EmailAccountID;
    notificationGenerator2.To = parameters.To;
    notificationGenerator2.Cc = parameters.Cc;
    notificationGenerator2.Bcc = parameters.Bcc;
    notificationGenerator2.Subject = parameters.Subject;
    notificationGenerator2.Body = parameters.Body;
    notificationGenerator2.BAccountID = parameters.BAccountID;
    notificationGenerator2.ContactID = parameters.ContactID;
    notificationGenerator2.RefNoteID = parameters.RefNoteID;
    notificationGenerator2.CreateAsDraft = parameters.CreateAsDraft;
    if (parameters.Attachments != null)
    {
      foreach (Tuple<string, byte[]> attachment in (IEnumerable<Tuple<string, byte[]>>) parameters.Attachments)
        notificationGenerator2.AddAttachment(attachment.Item1, attachment.Item2);
    }
    if (parameters.AttachmentLinks != null)
    {
      foreach (Guid attachmentLink in (IEnumerable<Guid>) parameters.AttachmentLinks)
        notificationGenerator2.AddAttachmentLink(attachmentLink);
    }
    return notificationGenerator2;
  }
}
