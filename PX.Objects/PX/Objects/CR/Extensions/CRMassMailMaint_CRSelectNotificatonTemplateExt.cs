// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRMassMailMaint_CRSelectNotificatonTemplateExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.SM;
using PX.Web.UI;

#nullable disable
namespace PX.Objects.CR.Extensions;

public class CRMassMailMaint_CRSelectNotificatonTemplateExt : 
  CRSelectNotificatonTemplateExt<CRMassMailMaint, CRMassMail>
{
  protected virtual void _(Events.RowSelected<CRMassMail> e)
  {
    if (e.Row == null)
      return;
    ((PXAction) this.LoadEmailSource).SetEnabled(e.Row.Status != "S" & ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRMassMail>>) e).Cache.GetStatus((object) e.Row) != 2);
  }

  [PXDefault(1)]
  [PXMergeAttributes]
  protected virtual void _(
    Events.CacheAttached<NotificationFilter.insertTemplateText> e)
  {
  }

  public override void MapData(Notification notification)
  {
    int? insertTemplateText = ((PXSelectBase<NotificationFilter>) this.NotificationInfo).Current.InsertTemplateText;
    int num = 0;
    if (insertTemplateText.GetValueOrDefault() == num & insertTemplateText.HasValue)
    {
      ((PXSelectBase<CRMassMail>) this.Base.MassMails).Current.MailTo = notification.NTo;
      ((PXSelectBase<CRMassMail>) this.Base.MassMails).Current.MailCc = notification.NCc;
      ((PXSelectBase<CRMassMail>) this.Base.MassMails).Current.MailBcc = notification.NBcc;
      ((PXSelectBase<CRMassMail>) this.Base.MassMails).Current.MailContent = notification.Body;
      ((PXSelectBase<CRMassMail>) this.Base.MassMails).Current.MailSubject = notification.Subject;
    }
    else
    {
      CRMassMail current1 = ((PXSelectBase<CRMassMail>) this.Base.MassMails).Current;
      current1.MailTo = $"{current1.MailTo} {notification.NTo}";
      CRMassMail current2 = ((PXSelectBase<CRMassMail>) this.Base.MassMails).Current;
      current2.MailCc = $"{current2.MailCc} {notification.NCc}";
      CRMassMail current3 = ((PXSelectBase<CRMassMail>) this.Base.MassMails).Current;
      current3.MailBcc = $"{current3.MailBcc} {notification.NBcc}";
      CRMassMail current4 = ((PXSelectBase<CRMassMail>) this.Base.MassMails).Current;
      current4.MailSubject = $"{current4.MailSubject} {notification.Subject}";
      if (((PXSelectBase<NotificationFilter>) this.NotificationInfo).Current.InsertTemplateText.GetValueOrDefault() == 1)
        ((PXSelectBase<CRMassMail>) this.Base.MassMails).Current.MailContent = HtmlEntensions.MergeHtmls(((PXSelectBase<CRMassMail>) this.Base.MassMails).Current.MailContent, notification.Body);
      else
        ((PXSelectBase<CRMassMail>) this.Base.MassMails).Current.MailContent = HtmlEntensions.MergeHtmls(notification.Body, ((PXSelectBase<CRMassMail>) this.Base.MassMails).Current.MailContent);
    }
    ((PXSelectBase<CRMassMail>) this.Base.MassMails).Current.MailContent = PXRichTextConverter.NormalizeHtml(((PXSelectBase<CRMassMail>) this.Base.MassMails).Current.MailContent);
  }
}
