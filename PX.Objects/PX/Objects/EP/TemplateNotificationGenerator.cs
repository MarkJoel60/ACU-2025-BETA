// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.TemplateNotificationGenerator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Common;
using PX.Common.Mail;
using PX.Data;
using PX.Data.EP;
using PX.Data.Wiki.Parser;
using PX.Objects.CR;
using PX.SM;
using PX.Web.UI;
using System;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.EP;

public class TemplateNotificationGenerator : NotificationGenerator
{
  private Guid? _refNoteId;
  private readonly object _entity;
  private readonly PXGraph _graph;

  private TemplateNotificationGenerator(PXGraph graph, object row, Notification t = null)
    : this(new PXGraph(), graph)
  {
    this._entity = row;
    this._refNoteId = new EntityHelper(this.Graph).GetEntityNoteID(this._entity);
    if (t == null)
      return;
    this.InitializeTemplate(t);
  }

  private TemplateNotificationGenerator(object row, string graphType = null)
    : this(new PXGraph(), TemplateNotificationGenerator.InitializeGraph(graphType))
  {
    if (row == null)
      return;
    EntityHelper entityHelper = new EntityHelper(this.Graph);
    System.Type type = row.GetType();
    if (!string.IsNullOrEmpty(this.Graph.PrimaryView))
    {
      System.Type itemType = this.Graph.Views[this.Graph.PrimaryView].Cache.GetItemType();
      if (type.IsSubclassOf(itemType))
        type = itemType;
    }
    row = entityHelper.GetEntityRow(type, entityHelper.GetEntityKey(row.GetType(), row));
    this.Graph.Caches[type].Current = row;
    this._entity = row;
    this._refNoteId = new EntityHelper(this.Graph).GetEntityNoteID(this._entity);
  }

  private TemplateNotificationGenerator(PXGraph baseGraph, PXGraph graph)
    : base(baseGraph, graph.Accessinfo)
  {
    this._graph = graph;
  }

  private TemplateNotificationGenerator(object row, Notification t)
    : this(row, ServiceLocator.Current.GetInstance<IPXPageIndexingService>().GetGraphTypeByScreenID(t.ScreenID))
  {
    this.InitializeTemplate(t);
  }

  public static TemplateNotificationGenerator Create(object row, int? notificationId)
  {
    Notification t = (Notification) null;
    if (notificationId.HasValue)
    {
      t = (Notification) PXGraph.CreateInstance<SMNotificationMaint>().PublishedNotification.Search<Notification.notificationID>((object) notificationId);
      if (t == null)
        throw new PXException("The email template cannot be found.");
    }
    return t != null ? new TemplateNotificationGenerator(row, t) : new TemplateNotificationGenerator(row);
  }

  public static TemplateNotificationGenerator Create(object row, Notification notification)
  {
    return new TemplateNotificationGenerator(row, notification);
  }

  public static TemplateNotificationGenerator Create(object row, string notificationCD = null)
  {
    Notification t = (Notification) null;
    if (notificationCD != null)
    {
      t = (Notification) PXGraph.CreateInstance<SMNotificationMaint>().PublishedNotification.Search<Notification.name>((object) notificationCD);
      if (t == null)
        throw new PXException("The {0} email template cannot be found.", new object[1]
        {
          (object) notificationCD
        });
    }
    return t != null ? new TemplateNotificationGenerator(row, t) : new TemplateNotificationGenerator(row);
  }

  public static TemplateNotificationGenerator Create(PXGraph graph, object row, int notificationId)
  {
    return new TemplateNotificationGenerator(graph, row, (Notification) PXGraph.CreateInstance<SMNotificationMaint>().PublishedNotification.Search<Notification.notificationID>((object) notificationId) ?? throw new PXException("The email template cannot be found."));
  }

  public static TemplateNotificationGenerator Create(
    PXGraph graph,
    object row,
    Notification notification)
  {
    return new TemplateNotificationGenerator(graph, row, notification);
  }

  public static TemplateNotificationGenerator Create(
    PXGraph graph,
    object row,
    string notificationCD = null)
  {
    Notification t = (Notification) null;
    if (notificationCD != null)
    {
      t = (Notification) PXGraph.CreateInstance<SMNotificationMaint>().PublishedNotification.Search<Notification.name>((object) notificationCD);
      if (t == null)
        throw new PXException("The {0} email template cannot be found.", new object[1]
        {
          (object) notificationCD
        });
    }
    return t != null ? new TemplateNotificationGenerator(graph, row, t) : new TemplateNotificationGenerator(graph, row);
  }

  public bool LinkToEntity { get; set; }

  public virtual NotificationGenerator ParseNotification()
  {
    using (!string.IsNullOrEmpty(this._localeName) ? new PXCultureScope(new CultureInfo(this._localeName)) : (PXCultureScope) null)
      return new NotificationGenerator(this.Graph)
      {
        MailAccountId = this.MailAccountId,
        To = this.ParseMailAddressExpression(this.To),
        Cc = this.ParseMailAddressExpression(this.Cc),
        Bcc = this.ParseMailAddressExpression(this.Bcc),
        Subject = this.ParseExpression(this.Subject),
        Body = this.ParseExpression(this.Body),
        AttachmentsID = this.AttachmentsID
      };
  }

  protected override CRSMEmail CreateMessage()
  {
    using (!string.IsNullOrEmpty(this._localeName) ? new PXCultureScope(new CultureInfo(this._localeName)) : (PXCultureScope) null)
    {
      CRSMEmail message = this.CreateMessage(true);
      if (this.LinkToEntity && this._refNoteId.HasValue)
        message.RefNoteID = this._refNoteId;
      if (this._entity != null)
      {
        PX.Objects.CR.Contact entity = this._entity as PX.Objects.CR.Contact;
        message.ContactID = entity == null || !(entity.ContactType != "LD") ? message.ContactID : entity.ContactID;
        message.Body = PXRichTextConverter.NormalizeHtml(this.ParseExpression(this.Body));
      }
      message.MailTo = message.MailCc = message.MailBcc = (string) null;
      message.MailTo = NotificationGenerator.MergeAddressList(message, this.ParseMailAddressExpression(this.To), (string) null);
      message.MailCc = NotificationGenerator.MergeAddressList(message, this.ParseMailAddressExpression(this.Cc), (string) null);
      message.MailBcc = NotificationGenerator.MergeAddressList(message, this.ParseMailAddressExpression(this.Bcc), (string) null);
      message.MailReply = string.IsNullOrEmpty(this.Reply) ? message.MailFrom : this.ParseMailAddressExpression(this.Reply);
      message.Subject = this.ParseExpression(this.Subject);
      CREmailActivityMaint.TryCorrectMailDisplayNames(this.Graph, message);
      return message;
    }
  }

  private System.Type EntityType => this._entity.GetType();

  private PXGraph Graph => this._graph;

  private void InitializeTemplate(Notification t)
  {
    this.MailAccountId = t.NFrom ?? MailAccountManager.DefaultAnyMailAccountID;
    string str = this.MailAccountId.With<int?, EMailAccount>((Func<int?, EMailAccount>) (_ => (EMailAccount) PXSelectBase<EMailAccount, PXSelect<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Required<EMailAccount.emailAccountID>>>>.Config>.Select(this.Graph, (object) _.Value))).With<EMailAccount, string>((Func<EMailAccount, string>) (_ => _.Address));
    this.From = str;
    this.Reply = str;
    this.To = t.NTo;
    this.Cc = t.NCc;
    this.Bcc = t.NBcc;
    this.Body = t.Body;
    this.Subject = t.Subject;
    this.AttachmentsID = t.NoteID;
    this.ActivityType = t.Type;
    foreach (PXResult<NoteDoc> pxResult in PXSelectBase<NoteDoc, PXSelect<NoteDoc, Where<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>>>.Config>.Select(this.Graph, (object) t.NoteID))
      this.AddAttachmentLink(((NoteDoc) pxResult).FileID.Value);
    this._localeName = t.LocaleName;
    if (this.Graph.Views.ContainsKey("GeneralInfo"))
      return;
    this.Graph.Views.Add("GeneralInfo", new GeneralInfoSelect(this.Graph).View);
  }

  public string ParseMailAddressExpression(string field)
  {
    if (string.IsNullOrEmpty(field))
      return string.Empty;
    if (this._entity == null)
      return field;
    string[] strArray = field.Split(';', ',');
    StringBuilder stringBuilder = new StringBuilder();
    bool flag = true;
    for (int index = 0; index < strArray.Length; ++index)
    {
      string expression = this.ParseExpression(strArray[index]);
      try
      {
        foreach (MailAddress address in EmailParser.ParseAddresses(expression))
        {
          if (!flag)
            stringBuilder.Append(";");
          else
            flag = false;
          stringBuilder.Append(address.ToString());
        }
      }
      catch
      {
        string empty = string.Empty;
      }
    }
    return stringBuilder.ToString();
  }

  public string ParseExpression(string field)
  {
    return this._entity != null ? PXTemplateContentParser.Instance.Process(field, this.Graph, this.EntityType, (object[]) null) : field;
  }

  private static PXGraph InitializeGraph(string graphType)
  {
    System.Type type = (System.Type) null;
    if (graphType != null)
    {
      type = PXBuildManager.GetType(graphType, false);
      if (type == (System.Type) null)
        throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("{0} type cannot be found.", (object) graphType));
      if (!typeof (PXGraph).IsAssignableFrom(type))
        throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("{0} is not a graph subclass.", (object) graphType));
    }
    return (type != (System.Type) null ? PXGraph.CreateInstance(type) : (PXGraph) null) ?? new PXGraph();
  }
}
