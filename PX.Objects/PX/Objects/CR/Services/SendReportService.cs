// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Services.SendReportService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.EP;
using PX.Data.Reports;
using PX.Data.Reports.Services;
using PX.Objects.EP;
using PX.Reports.Mail;
using PX.SM;
using PX.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Objects.CR.Services;

internal class SendReportService : ISendReportService
{
  public void SendEmail(GroupMessage message, IList<FileInfo> files)
  {
    SendEmailParams sendEmailParams = new SendEmailParams(message);
    EnumerableExtensions.AddRange<FileInfo>((ICollection<FileInfo>) sendEmailParams.Attachments, (IEnumerable<FileInfo>) files);
    this.SendEmail(sendEmailParams);
  }

  public void SendEmail(SendEmailParams sendEmailParams)
  {
    this.SendEmail(sendEmailParams, (object) null);
  }

  public void SendEmail(SendEmailParams sendEmailParams, object handler)
  {
    CREmailActivityMaint graph = PXGraph.CreateInstance<CREmailActivityMaint>();
    PXCache cache = ((PXSelectBase) graph.Message).Cache;
    PXCache pxCache1 = cache;
    bool isDirty = pxCache1.IsDirty;
    CRSMEmail crsmEmail = GraphHelper.NonDirtyInsert<CRSMEmail>(cache, (Action<CRSMEmail>) null);
    int? currentOwnerId = EmployeeMaint.GetCurrentOwnerID((PXGraph) graph);
    crsmEmail.MailTo = sendEmailParams.To;
    crsmEmail.MailCc = sendEmailParams.Cc;
    crsmEmail.MailBcc = sendEmailParams.Bcc;
    crsmEmail.Subject = sendEmailParams.Subject;
    crsmEmail.MPStatus = "DR";
    pxCache1.IsDirty = isDirty;
    cache.Current = (object) crsmEmail;
    System.Type sourceType = sendEmailParams.Source.With<object, System.Type>((Func<object, System.Type>) (s => s.GetType()));
    Guid? nullable1 = sourceType.With<System.Type, PXCache>((Func<System.Type, PXCache>) (type => ((PXGraph) graph).Caches[type])).With<PXCache, Guid?>((Func<PXCache, Guid?>) (c => PXNoteAttribute.GetNoteID(c, sendEmailParams.Source, EntityHelper.GetNoteField(sourceType))));
    crsmEmail.RefNoteID = nullable1;
    if (crsmEmail.RefNoteID.HasValue)
      crsmEmail.RefNoteIDType = sourceType.FullName;
    System.Type o = sendEmailParams.ParentSource.With<object, System.Type>((Func<object, System.Type>) (s => s.GetType()));
    PXCache pxCache2 = o.With<System.Type, PXCache>((Func<System.Type, PXCache>) (type => Activator.CreateInstance(BqlCommand.Compose(new System.Type[2]
    {
      typeof (PXCache<>),
      type
    }), (object) graph) as PXCache));
    EntityHelper entityHelper = new EntityHelper((PXGraph) graph);
    if (pxCache2 != null)
    {
      object entityRow = entityHelper.GetEntityRow(pxCache2.GetItemType(), entityHelper.GetEntityRowKeys(pxCache2.GetItemType(), sendEmailParams.ParentSource));
      string idField = EntityHelper.GetIDField(((PXGraph) graph).Caches[o]);
      int? nullable2 = (int?) ((PXGraph) graph).Caches[o].GetValue(entityRow, idField);
      crsmEmail.BAccountID = nullable2;
    }
    crsmEmail.Type = (string) null;
    crsmEmail.IsIncome = new bool?(false);
    crsmEmail.OwnerID = currentOwnerId;
    crsmEmail.Subject = crsmEmail.Subject;
    if (!string.IsNullOrEmpty(sendEmailParams.TemplateID))
    {
      this.FillEmailWithTemplate(sendEmailParams, graph, crsmEmail);
    }
    else
    {
      string str = sendEmailParams.Body;
      if (!SendReportService.IsHtml(str))
        str = Tools.ConvertSimpleTextToHtml(str);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("<html><body>");
      stringBuilder.Append(Tools.RemoveHeader(str));
      stringBuilder.Append("<br/>");
      stringBuilder.Append(Tools.RemoveHeader(crsmEmail.Body));
      stringBuilder.Append("</body></html>");
      crsmEmail.Body = stringBuilder.ToString();
    }
    crsmEmail.MailFrom = CREmailActivityMaint.FillMailFrom((PXGraph) graph, crsmEmail);
    crsmEmail.MailReply = CREmailActivityMaint.FillMailReply((PXGraph) graph, crsmEmail);
    crsmEmail.Body = PXRichTextConverter.NormalizeHtml(crsmEmail.Body);
    if (sendEmailParams.Attachments.Count > 0)
      SendReportService.AttachFiles(crsmEmail, cache, (IEnumerable<FileInfo>) sendEmailParams.Attachments);
    if (handler is Action<CRSMEmail> action)
      action(crsmEmail);
    ((PXGraph) graph).Caches[((object) crsmEmail).GetType()].RaiseRowSelected((object) crsmEmail);
    throw new PXPopupRedirectException((PXGraph) graph, ((object) graph).GetType().Name, true);
  }

  private void FillEmailWithTemplate(
    SendEmailParams sendEmailParams,
    CREmailActivityMaint graph,
    CRSMEmail newEmail)
  {
    NotificationGenerator notification = TemplateNotificationGenerator.Create(sendEmailParams.Source, sendEmailParams.TemplateID).ParseNotification();
    if (string.IsNullOrEmpty(newEmail.Body))
      newEmail.Body = notification.Body;
    else
      newEmail.Body = HtmlEntensions.MergeHtmls(notification.Body, newEmail.Body);
    if (string.IsNullOrEmpty(newEmail.Subject))
      newEmail.Subject = notification.Subject;
    if (string.IsNullOrEmpty(newEmail.MailTo))
      newEmail.MailTo = notification.To;
    if (string.IsNullOrEmpty(newEmail.MailCc))
      newEmail.MailCc = notification.Cc;
    if (string.IsNullOrEmpty(newEmail.MailBcc))
      newEmail.MailBcc = notification.Bcc;
    newEmail.MailAccountID = notification.MailAccountId ?? MailAccountManager.DefaultMailAccountID;
    newEmail.MessageId = $"<{Guid.NewGuid().ToString()}_acumatica{CREmailActivityMaint.GetMessageIDAppendix((PXGraph) graph, newEmail)}>";
    PXCache cache = ((PXSelectBase) graph.Message).Cache;
    Guid? nullable = notification.AttachmentsID;
    if (!nullable.HasValue)
      return;
    HashSet<Guid> source = new HashSet<Guid>();
    foreach (PXResult<NoteDoc> pxResult in PXSelectBase<NoteDoc, PXSelect<NoteDoc, Where<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>>>.Config>.Select((PXGraph) graph, new object[1]
    {
      (object) notification.AttachmentsID
    }))
    {
      NoteDoc noteDoc = PXResult<NoteDoc>.op_Implicit(pxResult);
      nullable = noteDoc.FileID;
      if (nullable.HasValue)
      {
        HashSet<Guid> guidSet = source;
        nullable = noteDoc.FileID;
        Guid guid = nullable.Value;
        guidSet.Add(guid);
      }
    }
    PXNoteAttribute.SetFileNotes(cache, (object) newEmail, source.ToArray<Guid>());
  }

  private static bool IsHtml(string text)
  {
    if (string.IsNullOrEmpty(text))
      return false;
    int num1 = text.IndexOf("<html", StringComparison.CurrentCultureIgnoreCase);
    int num2 = text.IndexOf("<body", StringComparison.CurrentCultureIgnoreCase);
    return num1 > -1 && num2 > -1 && num2 > num1;
  }

  internal static void AttachFiles(CRSMEmail newEmail, PXCache cache, IEnumerable<FileInfo> files)
  {
    UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
    HashSet<Guid> source = new HashSet<Guid>();
    instance.IgnoreFileRestrictions = true;
    foreach (FileInfo file in files)
    {
      string str = file.FullName.IndexOf('\\') > -1 ? string.Empty : "\\";
      file.FullName = $"[{newEmail.ImcUID}] {str}{file.FullName}";
      instance.SaveFile(file, (FileExistsAction) 1);
      Guid guid = file.UID.Value;
      source.Add(guid);
    }
    cache.SetValueExt((object) newEmail, "NoteFiles", (object) source.ToArray<Guid>());
  }
}
