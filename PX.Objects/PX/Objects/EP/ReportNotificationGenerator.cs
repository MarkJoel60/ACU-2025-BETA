// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ReportNotificationGenerator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Common;
using PX.Common.Mail;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Reports;
using PX.Reports.Controls;
using PX.Reports.Data;
using PX.Reports.Mail;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

/// <summary>A report notification generator service.</summary>
public class ReportNotificationGenerator
{
  private readonly Report _report;
  private IDictionary<string, string> _parameters;
  private readonly IReportLoaderService _reportLoader;
  private readonly IReportDataBinder _reportDataBinder;

  [Obsolete("This constructor has been deprecated and will be removed in Acumatica ERP 2021R2. Please use dependency injection to inject a factory Func<string, ReportNotificationGenerator> and use it to create an instance of ReportNotificationGenerator instead")]
  public ReportNotificationGenerator(string reportId)
    : this(reportId, ServiceLocator.Current.GetInstance<IReportLoaderService>(), ServiceLocator.Current.GetInstance<IReportDataBinder>())
  {
  }

  /// <summary>
  /// Constructor for <see cref="T:PX.Objects.EP.ReportNotificationGenerator" /> service. The service is registered with Autofac.
  /// Please use dependency injection instead of direct calls to this constructor
  /// </summary>
  /// <param name="reportId">The report ID.</param>
  /// <param name="reportLoader">The report loader service.</param>
  /// <param name="reportDataBinder">The report data binder.</param>
  public ReportNotificationGenerator(
    string reportId,
    IReportLoaderService reportLoader,
    IReportDataBinder reportDataBinder)
  {
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(reportId, nameof (reportId), (string) null);
    this._reportLoader = reportLoader;
    this._reportDataBinder = reportDataBinder;
    this._report = this._reportLoader.LoadReport(reportId, (IPXResultset) null);
    if (this._report == null)
      throw new ArgumentException(PXMessages.LocalizeFormatNoPrefixNLA("Report '{0}' cannot be found", (object) reportId), nameof (reportId));
  }

  public int? MailAccountId { get; set; }

  public IDictionary<string, string> Parameters
  {
    get
    {
      return this._parameters ?? (this._parameters = (IDictionary<string, string>) new Dictionary<string, string>());
    }
    set => this._parameters = value;
  }

  public IEnumerable<NotificationRecipient> AdditionalRecipents { get; set; }

  public string Format { get; set; }

  public int? NotificationID { get; set; }

  public IEnumerable<CRSMEmail> Send()
  {
    this._reportLoader.InitDefaultReportParameters(this._report, this._parameters);
    ReportNode report = this._reportDataBinder.ProcessReportDataBinding(this._report);
    report.SendMailMode = true;
    return ReportNotificationGenerator.SendMessages(this.MailAccountId, report, this.Format, this.AdditionalRecipents, this.NotificationID);
  }

  [Obsolete("This method is obsolete and will be removed in future versions of Acumatica. Please use instance method Send instead")]
  public static IEnumerable<CRSMEmail> Send(
    string reportId,
    IDictionary<string, string> reportParams)
  {
    ReportNotificationGenerator notificationGenerator = ServiceLocator.Current.GetInstance<Func<string, ReportNotificationGenerator>>()(reportId);
    notificationGenerator.Parameters = reportParams;
    return notificationGenerator.Send();
  }

  private static IEnumerable<CRSMEmail> SendMessages(
    int? accountId,
    ReportNode report,
    string format,
    IEnumerable<NotificationRecipient> additionalRecipents,
    int? TemplateID)
  {
    List<CRSMEmail> crsmEmailList = new List<CRSMEmail>();
    Exception exception = (Exception) null;
    foreach (Message message in ReportNotificationGenerator.GetMessages(report))
    {
      try
      {
        crsmEmailList.AddRange(ReportNotificationGenerator.Send(accountId, message, format, additionalRecipents, TemplateID));
      }
      catch (Exception ex)
      {
        PXTrace.WriteError(ex);
        exception = ex;
      }
    }
    if (exception != null)
      throw exception;
    return (IEnumerable<CRSMEmail>) crsmEmailList;
  }

  private static IEnumerable<Message> GetMessages(ReportNode report)
  {
    Dictionary<GroupMessage, Message> dictionary = new Dictionary<GroupMessage, Message>();
    foreach (MailSettings mailSettings in report.Groups.Select<GroupNode, MailSettings>((Func<GroupNode, MailSettings>) (g => g.MailSettings)))
    {
      if (mailSettings != null && mailSettings.ShouldSerialize() && !dictionary.ContainsKey(MailSettings.op_Implicit(mailSettings)))
        dictionary.Add(MailSettings.op_Implicit(mailSettings), new Message(MailSettings.op_Implicit(mailSettings), report, MailSettings.op_Implicit(mailSettings)));
    }
    return (IEnumerable<Message>) dictionary.Values;
  }

  private static IEnumerable<CRSMEmail> Send(
    int? accountId,
    Message message,
    string format,
    IEnumerable<NotificationRecipient> additionalRecipients,
    int? TemplateID)
  {
    PXGraph graph = new PXGraph();
    object activitySource = ((GroupMessage) message).Relationship.ActivitySource;
    Guid? entityNoteId = ReportNotificationGenerator.GetEntityNoteID(graph, activitySource);
    int? nullable = ((GroupMessage) message).Relationship.ParentSource is BAccount parentSource ? parentSource.BAccountID : new int?();
    NotificationGenerator notificationGenerator = (NotificationGenerator) null;
    if (TemplateID.HasValue)
      notificationGenerator = (NotificationGenerator) TemplateNotificationGenerator.Create(activitySource, new int?(TemplateID.Value));
    else if (!string.IsNullOrEmpty(((GroupMessage) message).TemplateID))
      notificationGenerator = (NotificationGenerator) TemplateNotificationGenerator.Create(activitySource, ((GroupMessage) message).TemplateID);
    if (notificationGenerator == null)
      notificationGenerator = new NotificationGenerator(graph);
    notificationGenerator.Body = string.IsNullOrEmpty(notificationGenerator.Body) ? ((MailSender.MailMessageT) message).Content.Body : notificationGenerator.Body;
    notificationGenerator.Subject = string.IsNullOrEmpty(notificationGenerator.Subject) ? ((MailSender.MailMessageT) message).Content.Subject : notificationGenerator.Subject;
    notificationGenerator.MailAccountId = accountId;
    notificationGenerator.Reply = accountId.With<int?, EMailAccount>((Func<int?, EMailAccount>) (_ => (EMailAccount) PXSelectBase<EMailAccount, PXSelect<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Required<EMailAccount.emailAccountID>>>>.Config>.Select(graph, (object) _.Value))).With<EMailAccount, string>((Func<EMailAccount, string>) (_ => _.Address));
    notificationGenerator.To = ((MailSender.MailMessageT) message).Addressee.To;
    notificationGenerator.Cc = ((MailSender.MailMessageT) message).Addressee.Cc;
    notificationGenerator.Bcc = ((MailSender.MailMessageT) message).Addressee.Bcc;
    notificationGenerator.RefNoteID = entityNoteId;
    notificationGenerator.BAccountID = nullable;
    notificationGenerator.BodyFormat = PXNotificationFormatAttribute.ValidBodyFormat(format);
    List<NotificationRecipient> notificationRecipientList = new List<NotificationRecipient>();
    if (notificationGenerator.Watchers != null)
      notificationRecipientList.AddRange(notificationGenerator.Watchers);
    if (additionalRecipients != null)
      notificationRecipientList.AddRange(additionalRecipients);
    notificationGenerator.Watchers = (IEnumerable<NotificationRecipient>) notificationRecipientList;
    foreach (ReportStream attachment in message.Attachments)
      notificationGenerator.AddAttachment(attachment.Name, attachment.GetBytes());
    return notificationGenerator.Send();
  }

  private static Guid? GetEntityNoteID(PXGraph graph, object row)
  {
    EntityHelper entityHelper = new EntityHelper(graph);
    Guid? entityNoteId = new Guid?();
    if (row != null)
    {
      System.Type type = row.GetType();
      System.Type primaryGraphType = entityHelper.GetPrimaryGraphType(type, row, false);
      if (primaryGraphType != (System.Type) null)
        entityNoteId = PXNoteAttribute.GetNoteID((graph.GetType() != primaryGraphType ? PXGraph.CreateInstance(primaryGraphType) : graph).Caches[type], row, EntityHelper.GetNoteField(type));
    }
    return entityNoteId;
  }

  public static IEnumerable<GroupMessage> GetWatchers(
    GroupMessage source,
    string defaultFormat,
    RecipientList watchers)
  {
    if (watchers != null)
    {
      GroupMessage watcher1 = (GroupMessage) null;
      bool sourceAdded = false;
      if (defaultFormat != null)
      {
        watcher1 = new GroupMessage(((MailSender.MailMessageT) source).From, ((MailSender.MailMessageT) source).UID, ((MailSender.MailMessageT) source).Addressee, ((MailSender.MailMessageT) source).Content, source.TemplateID, ReportNotificationGenerator.ConvertFormat(defaultFormat), source.Relationship, source.Report);
        sourceAdded = true;
      }
      foreach (NotificationRecipient watcher2 in watchers)
      {
        if (string.Compare(watcher2.Email, ((MailSender.MailMessageT) source).Addressee.To, true) == 0)
        {
          watcher1 = new GroupMessage(((MailSender.MailMessageT) source).From, ((MailSender.MailMessageT) source).UID, ((MailSender.MailMessageT) source).Addressee, ((MailSender.MailMessageT) source).Content, source.TemplateID, ReportNotificationGenerator.ConvertFormat(watcher2.Format), source.Relationship, source.Report);
          sourceAdded = true;
          break;
        }
      }
      foreach (NotificationRecipient recipient in watchers)
      {
        string format = ReportNotificationGenerator.ConvertFormat(recipient.Format);
        if (watcher1 != null && watcher1.Format != format)
        {
          yield return watcher1;
          watcher1 = (GroupMessage) null;
        }
        if (watcher1 == null)
        {
          if (format == source.Format)
          {
            watcher1 = new GroupMessage(source);
            sourceAdded = true;
          }
          else
            watcher1 = new GroupMessage(((MailSender.MailMessageT) source).From, ((MailSender.MailMessageT) source).UID, MailSender.MessageAddressee.Empty, ((MailSender.MailMessageT) source).Content, source.TemplateID, format, source.Relationship, source.Report);
        }
        string addresses1 = ((MailSender.MailMessageT) watcher1).Addressee.To;
        string str1 = ((MailSender.MailMessageT) watcher1).Addressee.Cc;
        string str2 = ((MailSender.MailMessageT) watcher1).Addressee.Bcc;
        switch (recipient.AddTo)
        {
          case "C":
            str1 = PXDBEmailAttribute.AppendAddresses(str1, recipient.Email);
            break;
          case "B":
            str2 = PXDBEmailAttribute.AppendAddresses(str2, recipient.Email);
            break;
          default:
            addresses1 = PXDBEmailAttribute.AppendAddresses(addresses1, recipient.Email);
            break;
        }
        MailSender.MessageAddressee messageAddressee = new MailSender.MessageAddressee(addresses1 ?? recipient.Email, ((MailSender.MailMessageT) watcher1).Addressee.Reply, str1, str2);
        watcher1 = new GroupMessage(((MailSender.MailMessageT) watcher1).From, ((MailSender.MailMessageT) watcher1).UID, messageAddressee, ((MailSender.MailMessageT) watcher1).Content, watcher1.TemplateID, watcher1.Format, watcher1.Relationship, watcher1.Report);
        format = (string) null;
      }
      if (watcher1 != null)
        yield return watcher1;
      if (!sourceAdded)
        yield return source;
    }
    else
      yield return source;
  }

  public static string ConvertFormat(string notificationFormat)
  {
    switch (notificationFormat)
    {
      case "P":
        return "PDF";
      case "E":
        return "Excel";
      default:
        return "HTML";
    }
  }
}
