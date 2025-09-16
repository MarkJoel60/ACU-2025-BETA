// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CREmailActivityMaint_CRSelectNotificatonTemplateExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.EP;
using PX.Data.Wiki.Parser;
using PX.SM;
using PX.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.Extensions;

public class CREmailActivityMaint_CRSelectNotificatonTemplateExt : 
  CRSelectNotificatonTemplateExt<CREmailActivityMaint, CRSMEmail>
{
  protected virtual void _(Events.RowSelected<CRSMEmail> e)
  {
    if (e.Row == null)
      return;
    ((PXAction) this.LoadEmailSource).SetEnabled(!e.Row.IsIncome.GetValueOrDefault() && e.Row.MPStatus == "DR" && e.Row.RefNoteID.HasValue);
  }

  public override void MapData(Notification notification)
  {
    object entityRow = new EntityHelper((PXGraph) this.Base).GetEntityRow(((PXSelectBase<CRSMEmail>) this.Base.Message).Current.RefNoteID, true);
    if (entityRow == null)
      return;
    System.Type type1 = entityRow.GetType();
    System.Type type2;
    PXPrimaryGraphAttribute.FindPrimaryGraph(((PXGraph) this.Base).Caches[type1], true, ref entityRow, ref type2);
    PXGraph instance = PXGraph.CreateInstance(type2);
    System.Type itemType = GraphHelper.GetPrimaryCache(instance).GetItemType();
    if (!type1.Equals(itemType))
      entityRow = new EntityHelper((PXGraph) this.Base).GetEntityRow(itemType, ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.RefNoteID);
    if (!((Dictionary<string, PXView>) instance.Views).ContainsKey("GeneralInfo"))
    {
      PXSelectBase pxSelectBase = (PXSelectBase) new GeneralInfoSelect(instance);
      instance.Views.Add("GeneralInfo", pxSelectBase.View);
    }
    object[] keys = this.GetKeys(entityRow, instance.Caches[itemType]);
    EntityHelper entityHelper = new EntityHelper(instance);
    instance.Caches[itemType].Current = entityHelper.GetEntityRow(itemType, keys);
    Notification copy = PXCache<Notification>.CreateCopy(notification);
    copy.Subject = PXTemplateContentParser.Instance.Process(notification.Subject, instance, itemType, (object[]) null);
    string str = "";
    if (instance.HasException())
    {
      ((PXSelectBase) this.Base.Message).Cache.RaiseExceptionHandling<CRSMEmail.subject>((object) ((PXSelectBase<CRSMEmail>) this.Base.Message).Current, (object) null, (Exception) new PXSetPropertyException("The value of at least one field in the selected email template is not recognized.", (PXErrorLevel) 2));
      str = PXUIFieldAttribute.GetWarning(instance.Views[instance.PrimaryView].Cache, instance.Views[instance.PrimaryView].Cache.Current, instance.Views[instance.PrimaryView].Cache.Keys[0]);
    }
    copy.Body = PXTemplateContentParser.Instance.Process(notification.Body, instance, itemType, (object[]) null);
    if (instance.HasException() && (string.IsNullOrEmpty(str) || str != PXUIFieldAttribute.GetWarning(instance.Views[instance.PrimaryView].Cache, (object) null, instance.Views[instance.PrimaryView].Cache.Keys[0])))
      ((PXSelectBase) this.Base.Message).Cache.RaiseExceptionHandling<CRSMEmail.body>((object) ((PXSelectBase<CRSMEmail>) this.Base.Message).Current, (object) null, (Exception) new PXSetPropertyException("The value of at least one field in the selected email template is not recognized.", (PXErrorLevel) 2));
    copy.NTo = PXTemplateContentParser.Instance.Process(notification.NTo, instance, itemType, (object[]) null);
    copy.NCc = PXTemplateContentParser.Instance.Process(notification.NCc, instance, itemType, (object[]) null);
    copy.NBcc = PXTemplateContentParser.Instance.Process(notification.NBcc, instance, itemType, (object[]) null);
    int? nullable1 = copy.NFrom;
    if (nullable1.HasValue && MailAccountManager.GetEmailAccountIfAllowed((PXGraph) this.Base, copy.NFrom) != null)
    {
      CRSMEmail current = ((PXSelectBase<CRSMEmail>) this.Base.Message).Current;
      nullable1 = copy.NFrom;
      int? nullable2 = new int?(nullable1.Value);
      current.MailAccountID = nullable2;
      ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.MailFrom = CREmailActivityMaint.FillMailFrom((PXGraph) this.Base, ((PXSelectBase<CRSMEmail>) this.Base.Message).Current, true);
      ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.MailReply = CREmailActivityMaint.FillMailReply((PXGraph) this.Base, ((PXSelectBase<CRSMEmail>) this.Base.Message).Current);
    }
    nullable1 = ((PXSelectBase<NotificationFilter>) this.NotificationInfo).Current.InsertTemplateText;
    int num = 0;
    if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
    {
      ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.MailTo = copy.NTo;
      ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.MailCc = copy.NCc;
      ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.MailBcc = copy.NBcc;
      ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.Body = copy.Body;
      ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.Subject = copy.Subject;
    }
    else
    {
      ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.MailTo = this.AppendAddresses<CRSMEmail.mailTo>(((PXSelectBase) this.Base.Message).Cache, ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.MailTo, copy.NTo);
      ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.MailCc = this.AppendAddresses<CRSMEmail.mailCc>(((PXSelectBase) this.Base.Message).Cache, ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.MailCc, copy.NCc);
      ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.MailBcc = this.AppendAddresses<CRSMEmail.mailBcc>(((PXSelectBase) this.Base.Message).Cache, ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.MailBcc, copy.NBcc);
      CRSMEmail current = ((PXSelectBase<CRSMEmail>) this.Base.Message).Current;
      current.Subject = $"{current.Subject} {copy.Subject}";
      nullable1 = ((PXSelectBase<NotificationFilter>) this.NotificationInfo).Current.InsertTemplateText;
      if (nullable1.GetValueOrDefault() == 1)
        ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.Body = HtmlEntensions.MergeHtmls(((PXSelectBase<CRSMEmail>) this.Base.Message).Current.Body, copy.Body);
      else
        ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.Body = HtmlEntensions.MergeHtmls(copy.Body, ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.Body);
    }
    ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.Body = MailAccountManager.AppendSignature(((PXSelectBase<CRSMEmail>) this.Base.Message).Current.Body, (PXGraph) this.Base, ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.ResponseToNoteID.HasValue && ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.Outgoing.GetValueOrDefault() ? (MailAccountManager.SignatureOptions) 2 : (MailAccountManager.SignatureOptions) 1);
    ((PXSelectBase<CRSMEmail>) this.Base.Message).Current.Body = PXRichTextConverter.NormalizeHtml(((PXSelectBase<CRSMEmail>) this.Base.Message).Current.Body);
    ((PXSelectBase) this.Base.Message).Cache.ForceExceptionHandling = true;
    ((PXSelectBase<CRSMEmail>) this.Base.Message).UpdateCurrent();
  }

  protected string AppendAddresses<TEmailField>(PXCache cache, string address1, string address2) where TEmailField : IBqlField
  {
    try
    {
      return PXDBEmailAttribute.AppendAddresses(address1, address2);
    }
    catch (Exception ex)
    {
      cache.RaiseExceptionHandling<TEmailField>(cache.Current, (object) address1, (Exception) new PXSetPropertyException(ex.InnerException, (PXErrorLevel) 2, "The value of at least one field in the selected email template is not recognized.", Array.Empty<object>()));
    }
    return address1;
  }

  protected virtual object[] GetKeys(object e, PXCache cache)
  {
    return cache.BqlKeys.Select<System.Type, object>((Func<System.Type, object>) (t => cache.GetValue(e, t.Name))).ToArray<object>();
  }
}
