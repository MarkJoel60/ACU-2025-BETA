// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.SendEmailParams
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Mail;
using PX.Reports.Mail;
using PX.SM;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Reports;

public class SendEmailParams
{
  public SendEmailParams(GroupMessage message)
  {
    this.From = ((MailSender.MailMessageT) message).From;
    this.To = ((MailSender.MailMessageT) message).Addressee.To;
    this.Cc = ((MailSender.MailMessageT) message).Addressee.Cc;
    this.Bcc = ((MailSender.MailMessageT) message).Addressee.Bcc;
    this.Subject = ((MailSender.MailMessageT) message).Content.Subject;
    this.Body = ((MailSender.MailMessageT) message).Content.Body;
    this.Source = message.Relationship.ActivitySource;
    this.ParentSource = message.Relationship.ParentSource;
    this.TemplateID = message.TemplateID;
  }

  public IList<FileInfo> Attachments { get; } = (IList<FileInfo>) new List<FileInfo>();

  public string From { get; set; }

  public string To { get; set; }

  public string Cc { get; set; }

  public string Bcc { get; set; }

  public string Subject { get; set; }

  public string Body { get; set; }

  public object Source { get; set; }

  public object ParentSource { get; set; }

  public string TemplateID { get; set; }
}
