// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.DefaultEmailProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Mail;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.PM;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.EP;

public class DefaultEmailProcessor : BasicEmailProcessor
{
  protected override bool Process(BasicEmailProcessor.Package package)
  {
    EMailAccount account = package.Account;
    CRSMEmail message = package.Message;
    if (message.RefNoteID.HasValue || !message.Ticket.HasValue)
      return false;
    PXGraph graph = package.Graph;
    if (!message.StartDate.HasValue)
      message.StartDate = new DateTime?(PXTimeZoneInfo.Now);
    if (account.EmailAccountType == "S")
      message.OwnerID = this.GetKnownSender(graph, message);
    CRPMSMEmail parentOriginalEmail = DefaultEmailProcessor.GetParentOriginalEmail(graph, message.Ticket.Value);
    if (parentOriginalEmail == null)
      return false;
    message.ResponseToNoteID = parentOriginalEmail.EmailNoteID;
    message.ParentNoteID = parentOriginalEmail.ParentNoteID;
    message.RefNoteID = parentOriginalEmail.RefNoteID;
    message.BAccountID = parentOriginalEmail.BAccountID;
    message.ContactID = parentOriginalEmail.ContactID;
    message.DocumentNoteID = parentOriginalEmail.DocumentNoteID;
    this.CreateProjectTimeActivityForIncomingMessageIfNeeded(message, parentOriginalEmail);
    message.IsPrivate = parentOriginalEmail.IsPrivate;
    if (!message.OwnerID.HasValue)
    {
      if (account.EmailAccountType == "S")
      {
        try
        {
          message.WorkgroupID = parentOriginalEmail.WorkgroupID;
          graph.Caches[typeof (CRSMEmail)].SetValueExt<CRSMEmail.ownerID>((object) message, (object) parentOriginalEmail.OwnerID);
        }
        catch (PXSetPropertyException ex)
        {
          message.OwnerID = new int?();
        }
      }
    }
    return true;
  }

  private void CreateProjectTimeActivityForIncomingMessageIfNeeded(
    CRSMEmail incomingMessage,
    CRPMSMEmail parentMessage)
  {
    if (!parentMessage.ProjectID.HasValue)
      return;
    int? nullable1 = parentMessage.ProjectID;
    int id = NonProject.ID;
    if (nullable1.GetValueOrDefault() == id & nullable1.HasValue)
      return;
    CREmailActivityMaint instance1 = PXGraph.CreateInstance<CREmailActivityMaint>();
    PMTimeActivity instance2 = ((PXSelectBase) instance1.TAct).Cache.CreateInstance() as PMTimeActivity;
    instance2.RefNoteID = incomingMessage.NoteID;
    PMTimeActivity pmTimeActivity1 = ((PXSelectBase<PMTimeActivity>) instance1.TAct).Insert(instance2);
    ((PXSelectBase) instance1.TAct).Cache.SetValueExt<PMTimeActivity.trackTime>((object) pmTimeActivity1, (object) true);
    ((PXSelectBase<PMTimeActivity>) instance1.TAct).Update(pmTimeActivity1);
    pmTimeActivity1.Summary = incomingMessage.Subject;
    pmTimeActivity1.IsBillable = new bool?(false);
    ((PXSelectBase<PMTimeActivity>) instance1.TAct).Update(pmTimeActivity1);
    pmTimeActivity1.ProjectID = parentMessage.ProjectID;
    pmTimeActivity1.ProjectTaskID = parentMessage.ProjectTaskID;
    PMTimeActivity pmTimeActivity2 = pmTimeActivity1;
    nullable1 = parentMessage.CostCodeID;
    int? nullable2 = nullable1 ?? CostCodeAttribute.DefaultCostCode;
    pmTimeActivity2.CostCodeID = nullable2;
    ((PXSelectBase<PMTimeActivity>) instance1.TAct).Update(pmTimeActivity1);
    ((PXAction) instance1.Save).Press();
  }

  private int? GetKnownSender(PXGraph graph, CRSMEmail message)
  {
    string str = Mailbox.Parse(message.MailFrom).With<Mailbox, string>((Func<Mailbox, string>) (_ => _.Address)).With<string, string>((Func<string, string>) (_ => _.Trim()));
    PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where2<Where<PX.Objects.CR.Contact.eMail, Equal<Required<PX.Objects.CR.Contact.eMail>>, And<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.employee>>>, And<PX.Objects.CR.Contact.userID, IsNotNull>>>.Config>.Clear(graph);
    return PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where2<Where<PX.Objects.CR.Contact.eMail, Equal<Required<PX.Objects.CR.Contact.eMail>>, And<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.employee>>>, And<PX.Objects.CR.Contact.userID, IsNotNull>>>.Config>.Select(graph, new object[1]
    {
      (object) str
    }))?.ContactID;
  }

  /// <summary>
  /// Find email by <see cref="T:PX.Objects.CR.CRPMSMEmail.id" /> and return <see cref="T:PX.Objects.CR.CRPMSMEmail" /> in response to which this email was created.
  /// </summary>
  /// <param name="graph"></param>
  /// <param name="id"><see cref="T:PX.Objects.CR.CRPMSMEmail.id" /></param>
  /// <returns></returns>
  public static CRPMSMEmail GetParentOriginalEmail(PXGraph graph, int id)
  {
    PXSelectBase<CRPMSMEmail, PXSelectReadonly<CRPMSMEmail, Where<CRPMSMEmail.id, Equal<Required<CRPMSMEmail.id>>>>.Config>.Clear(graph);
    CRPMSMEmail parentOriginalEmail = PXResultset<CRPMSMEmail>.op_Implicit(PXSelectBase<CRPMSMEmail, PXSelectReadonly<CRPMSMEmail, Where<CRPMSMEmail.id, Equal<Required<CRPMSMEmail.id>>>>.Config>.Select(graph, new object[1]
    {
      (object) id
    }));
    while (parentOriginalEmail != null && parentOriginalEmail.ClassID.GetValueOrDefault() == -2)
    {
      if (!parentOriginalEmail.ResponseToNoteID.HasValue)
        parentOriginalEmail = (CRPMSMEmail) null;
      else
        parentOriginalEmail = PXResultset<CRPMSMEmail>.op_Implicit(PXSelectBase<CRPMSMEmail, PXSelectReadonly<CRPMSMEmail, Where<CRPMSMEmail.emailNoteID, Equal<Required<CRPMSMEmail.emailNoteID>>>>.Config>.Select(graph, new object[1]
        {
          (object) parentOriginalEmail.ResponseToNoteID
        }));
    }
    return parentOriginalEmail;
  }
}
