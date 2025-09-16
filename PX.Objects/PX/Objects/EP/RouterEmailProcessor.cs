// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.RouterEmailProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Mail;
using PX.Data;
using PX.Objects.CR;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

#nullable disable
namespace PX.Objects.EP;

public class RouterEmailProcessor : BaseRoutingProcessor
{
  protected override bool Process(BasicEmailProcessor.Package package)
  {
    PXGraph graph = package.Graph;
    EMailAccount account = package.Account;
    CRSMEmail message = package.Message;
    if (account != null && (account.ForbidRouting.GetValueOrDefault() || !account.RouteEmployeeEmails.GetValueOrDefault()))
      return false;
    bool? nullable1 = RouterEmailProcessor.IsFromInternalUser(graph, message);
    BaseRoutingProcessor.MailAddressList mailAddressList = new BaseRoutingProcessor.MailAddressList();
    if (nullable1.GetValueOrDefault())
    {
      mailAddressList.AddRange((IEnumerable<MailAddress>) this.GetExternalRecipient(graph, message));
    }
    else
    {
      bool? nullable2 = nullable1;
      bool flag = false;
      if (!(nullable2.GetValueOrDefault() == flag & nullable2.HasValue))
        return false;
      mailAddressList.AddRange((IEnumerable<MailAddress>) this.GetInternalRecipient(graph, message));
    }
    this.RemoveAddress(mailAddressList, message.MailFrom);
    this.RemoveAddress(mailAddressList, message.MailTo);
    this.RemoveAddress(mailAddressList, message.MailCc);
    this.RemoveAddress(mailAddressList, message.MailBcc);
    if (mailAddressList.Count == 0)
      return false;
    if (nullable1.GetValueOrDefault())
    {
      this.SendCopyMessageToOutside(graph, package.Account, message, (IEnumerable<MailAddress>) mailAddressList);
      this.MarkAsRoutingEmail(graph, message);
      this.MarkAsRead(graph, message);
    }
    else
      this.SendCopyMessageToInside(graph, package.Account, message, (IEnumerable<MailAddress>) mailAddressList);
    GraphHelper.EnsureCachePersistence(graph, ((object) message).GetType());
    return true;
  }

  private List<MailAddress> GetExternalRecipient(PXGraph graph, CRSMEmail message)
  {
    CRSMEmail previousExternalMessage = RouterEmailProcessor.GetPreviousExternalMessage(graph, message);
    List<MailAddress> col = new List<MailAddress>();
    if (previousExternalMessage != null)
    {
      col = this.GetExternalMailOutside(previousExternalMessage);
      if (previousExternalMessage.OwnerID.HasValue)
      {
        List<MailAddress> ownerAddress = this.GetOwnerAddress(graph, previousExternalMessage.OwnerID);
        if (ownerAddress != null)
          col.Add<MailAddress>((IEnumerable<MailAddress>) ownerAddress);
      }
    }
    return col;
  }

  private List<MailAddress> GetInternalRecipient(PXGraph graph, CRSMEmail message)
  {
    List<MailAddress> col = new List<MailAddress>();
    CRSMEmail parentMessage = RouterEmailProcessor.GetParentMessage(graph, message);
    if (message.OwnerID.HasValue)
    {
      List<MailAddress> ownerAddress = this.GetOwnerAddress(graph, message.OwnerID);
      if (ownerAddress != null)
        col.Add<MailAddress>((IEnumerable<MailAddress>) ownerAddress);
    }
    int? nullable;
    if (parentMessage != null)
    {
      int? ownerId = parentMessage.OwnerID;
      nullable = message.OwnerID;
      if (!(ownerId.GetValueOrDefault() == nullable.GetValueOrDefault() & ownerId.HasValue == nullable.HasValue))
      {
        List<MailAddress> ownerAddress = this.GetOwnerAddress(graph, parentMessage.OwnerID);
        if (ownerAddress != null)
          col.Add<MailAddress>((IEnumerable<MailAddress>) ownerAddress);
      }
    }
    if (col.Count == 0)
    {
      PXGraph graph1 = graph;
      Guid? refNoteId = message.RefNoteID;
      nullable = new int?();
      int? mainOwner = nullable;
      List<MailAddress> ownerAddressByNote = this.GetOwnerAddressByNote(graph1, refNoteId, mainOwner);
      if (ownerAddressByNote != null)
        col.Add<MailAddress>((IEnumerable<MailAddress>) ownerAddressByNote);
      List<MailAddress> addressByBaccountId = this.GetOwnerAddressByBAccountID(graph, message.BAccountID);
      if (addressByBaccountId != null)
        col.Add<MailAddress>((IEnumerable<MailAddress>) addressByBaccountId);
    }
    return col;
  }

  private List<MailAddress> GetOwnerAddress(PXGraph graph, int? ownerId)
  {
    PX.Objects.CR.Contact contact;
    Users user;
    EPEmployee employee;
    this.FindOwner(graph, ownerId, out contact, out user, out employee);
    return this.PreventEmailRoutingFor(user, employee) ? (List<MailAddress>) null : this.GenerateAddress(contact, user);
  }

  private List<MailAddress> GetOwnerAddressByBAccountID(PXGraph graph, int? bAccID)
  {
    PXSelectBase<Users, PXSelectJoin<Users, LeftJoin<EPEmployee, On<EPEmployee.userID, Equal<Users.pKID>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<EPEmployee.defContactID>>>>, Where<PX.Objects.CR.Contact.bAccountID, Equal<Required<PX.Objects.CR.Contact.bAccountID>>>>.Config>.Clear(graph);
    PXResult<Users, EPEmployee, PX.Objects.CR.Contact> pxResult = (PXResult<Users, EPEmployee, PX.Objects.CR.Contact>) PXResultset<Users>.op_Implicit(PXSelectBase<Users, PXSelectJoin<Users, LeftJoin<EPEmployee, On<EPEmployee.userID, Equal<Users.pKID>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<EPEmployee.defContactID>>>>, Where<PX.Objects.CR.Contact.bAccountID, Equal<Required<PX.Objects.CR.Contact.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) bAccID
    }));
    if (pxResult == null)
      return (List<MailAddress>) null;
    PX.Objects.CR.Contact employeeContact = ((PXResult) pxResult).GetItem<PX.Objects.CR.Contact>();
    Users user = ((PXResult) pxResult).GetItem<Users>();
    EPEmployee employee = ((PXResult) pxResult).GetItem<EPEmployee>();
    return this.PreventEmailRoutingFor(user, employee) ? (List<MailAddress>) null : this.GenerateAddress(employeeContact, user);
  }

  private bool PreventEmailRoutingFor(Users user, EPEmployee employee)
  {
    return user?.State == "D" || employee?.VStatus == "I";
  }

  private List<MailAddress> GetExternalMailOutside(CRSMEmail message)
  {
    CRSMEmail crsmEmail = message;
    List<MailAddress> externalMailOutside = new List<MailAddress>();
    if (crsmEmail == null)
      return externalMailOutside;
    if (crsmEmail.IsIncome.GetValueOrDefault())
    {
      string str = crsmEmail.MailFrom.With<string, string>((Func<string, string>) (_ => _.Trim()));
      if (!string.IsNullOrEmpty(str))
        externalMailOutside = EmailParser.ParseAddresses(str);
    }
    else
      externalMailOutside = EmailParser.ParseAddresses(crsmEmail.MailTo);
    return externalMailOutside;
  }

  private static CRSMEmail GetParentMessage(PXGraph graph, CRSMEmail message)
  {
    return !message.Ticket.HasValue ? (CRSMEmail) null : RouterEmailProcessor.SelectActivity(graph, message.Ticket);
  }

  private static CRSMEmail GetPreviousExternalMessage(PXGraph graph, CRSMEmail message)
  {
    if (!message.Ticket.HasValue)
      return (CRSMEmail) null;
    CRSMEmail previousExternalMessage = RouterEmailProcessor.SelectActivity(graph, message.Ticket);
    while (previousExternalMessage != null && previousExternalMessage.ClassID.GetValueOrDefault() == -2)
      previousExternalMessage = RouterEmailProcessor.SelectParentEmail(graph, previousExternalMessage.ResponseToNoteID);
    return previousExternalMessage;
  }

  private static CRSMEmail SelectActivity(PXGraph graph, int? id)
  {
    PXSelectBase<CRSMEmail, PXSelect<CRSMEmail, Where<CRSMEmail.id, Equal<Required<CRSMEmail.id>>>>.Config>.Clear(graph);
    return PXResultset<CRSMEmail>.op_Implicit(PXSelectBase<CRSMEmail, PXSelect<CRSMEmail, Where<CRSMEmail.id, Equal<Required<CRSMEmail.id>>>>.Config>.Select(graph, new object[1]
    {
      (object) id
    }));
  }

  private static CRSMEmail SelectParentEmail(PXGraph graph, Guid? noteID)
  {
    PXSelectBase<CRSMEmail, PXSelect<CRSMEmail, Where<CRSMEmail.emailNoteID, Equal<Required<CRSMEmail.responseToNoteID>>>>.Config>.Clear(graph);
    return PXResultset<CRSMEmail>.op_Implicit(PXSelectBase<CRSMEmail, PXSelect<CRSMEmail, Where<CRSMEmail.emailNoteID, Equal<Required<CRSMEmail.responseToNoteID>>>>.Config>.Select(graph, new object[1]
    {
      (object) noteID
    }));
  }

  private void MarkAsRoutingEmail(PXGraph graph, CRSMEmail message)
  {
    PXCache cach = graph.Caches[((object) message).GetType()];
    message.ClassID = new int?(-2);
    message.RefNoteIDType = (string) null;
    message.RefNoteID = new Guid?();
    message.BAccountID = new int?();
    message.ContactID = new int?();
    CRSMEmail crsmEmail = message;
    cach.Update((object) crsmEmail);
  }

  private void MarkAsRead(PXGraph graph, CRSMEmail message)
  {
    EPView epView1 = PXResultset<EPView>.op_Implicit(PXSelectBase<EPView, PXSelect<EPView, Where<EPView.noteID, Equal<Required<CRSMEmail.noteID>>, And<EPView.contactID, Equal<Required<CRSMEmail.ownerID>>>>>.Config>.Select(graph, new object[2]
    {
      (object) message.NoteID,
      (object) graph.Accessinfo.ContactID
    }));
    EPView epView2;
    if (epView1 == null)
      epView2 = new EPView()
      {
        NoteID = message.NoteID,
        ContactID = graph.Accessinfo.ContactID
      };
    else
      epView2 = PXCache<EPView>.CreateCopy(epView1);
    if (epView2.Status.GetValueOrDefault() == 1)
      return;
    epView2.Status = new int?(1);
    graph.Caches[typeof (EPView)].Update((object) epView2);
  }

  internal static bool? IsFromInternalUser(PXGraph graph, CRSMEmail message)
  {
    string str = EmailParser.ParseAddresses(message.MailFrom).FirstOrDefault<MailAddress>().With<MailAddress, string>((Func<MailAddress, string>) (_ => _?.Address)).With<string, string>((Func<string, string>) (_ => _?.Trim()));
    PXSelectBase<Users, PXSelect<Users, Where2<Where<Users.guest, Equal<False>, Or<Users.guest, IsNull>>, And<Users.email, Equal<Required<Users.email>>>>>.Config>.Clear(graph);
    PXResultset<Users> pxResultset1 = PXSelectBase<Users, PXSelect<Users, Where2<Where<Users.guest, Equal<False>, Or<Users.guest, IsNull>>, And<Users.email, Equal<Required<Users.email>>>>>.Config>.Select(graph, new object[1]
    {
      (object) str
    });
    if ((pxResultset1.Count <= 0 ? 0 : (GraphHelper.RowCast<Users>((IEnumerable) pxResultset1).All<Users>((Func<Users, bool>) (_ => _.State == "D")) ? 1 : 0)) != 0)
      return new bool?();
    PXSelectBase<EPEmployee, PXSelectJoin<EPEmployee, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<EPEmployee.defContactID>>>, Where<EPEmployee.userID, IsNotNull, And<PX.Objects.CR.Contact.eMail, Equal<Required<PX.Objects.CR.Contact.eMail>>>>>.Config>.Clear(graph);
    PXResultset<EPEmployee> pxResultset2 = PXSelectBase<EPEmployee, PXSelectJoin<EPEmployee, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<EPEmployee.defContactID>>>, Where<EPEmployee.userID, IsNotNull, And<PX.Objects.CR.Contact.eMail, Equal<Required<PX.Objects.CR.Contact.eMail>>>>>.Config>.Select(graph, new object[1]
    {
      (object) str
    });
    if ((pxResultset2.Count <= 0 ? 0 : (GraphHelper.RowCast<EPEmployee>((IEnumerable) pxResultset2).All<EPEmployee>((Func<EPEmployee, bool>) (_ => _.VStatus == "I" || !_.RouteEmails.GetValueOrDefault())) ? 1 : 0)) != 0)
      return new bool?();
    return pxResultset1.Count > 0 || pxResultset2.Count > 0 ? new bool?(true) : new bool?(false);
  }

  internal static bool IsOwnerEqualUser(PXGraph graph, CRSMEmail message, int? owner)
  {
    string str = EmailParser.ParseAddresses(message.MailFrom).FirstOrDefault<MailAddress>().With<MailAddress, string>((Func<MailAddress, string>) (_ => _.Address)).With<string, string>((Func<string, string>) (_ => _.Trim()));
    PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.eMail, Equal<Required<PX.Objects.CR.Contact.eMail>>, And<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.employee>>>>.Config>.Clear(graph);
    PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.eMail, Equal<Required<PX.Objects.CR.Contact.eMail>>, And<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.employee>>>>.Config>.Select(graph, new object[1]
    {
      (object) str
    }));
    if (contact == null)
      return false;
    int? contactId = contact.ContactID;
    int? nullable = owner;
    return contactId.GetValueOrDefault() == nullable.GetValueOrDefault() & contactId.HasValue == nullable.HasValue;
  }

  private void SendCopyMessageToOutside(
    PXGraph graph,
    EMailAccount account,
    CRSMEmail message,
    IEnumerable<MailAddress> addresses)
  {
    PXCache cach = graph.Caches[((object) message).GetType()];
    CRSMEmail copy1 = (CRSMEmail) cach.CreateCopy((object) message);
    copy1.NoteID = new Guid?();
    copy1.EmailNoteID = new Guid?();
    copy1.IsIncome = new bool?(false);
    MailAddress mailAddress;
    copy1.MailFrom = EmailParser.TryParse(message.MailFrom, ref mailAddress) ? new MailAddress(account.Address, mailAddress.DisplayName).ToString() : account.Address;
    copy1.MailTo = PXDBEmailAttribute.ToString(addresses);
    copy1.MailCc = (string) null;
    copy1.MailBcc = (string) null;
    copy1.MPStatus = "PP";
    copy1.ClassID = new int?(4);
    Guid guid = Guid.NewGuid();
    copy1.ImcUID = new Guid?(guid);
    copy1.MessageId = $"{this.GetType().Name}_{guid.ToString().Replace("-", string.Empty)}";
    copy1.IsPrivate = message.IsPrivate;
    copy1.OwnerID = new int?();
    CRSMEmail copy2 = (CRSMEmail) cach.CreateCopy(cach.Insert((object) copy1));
    copy2.OwnerID = message.OwnerID;
    CRSMEmail crsmEmail;
    try
    {
      crsmEmail = (CRSMEmail) cach.Update((object) copy2);
    }
    catch (PXSetPropertyException ex)
    {
      copy2.OwnerID = new int?();
      crsmEmail = (CRSMEmail) cach.Update((object) copy2);
    }
    Guid[] fileNotes = PXNoteAttribute.GetFileNotes(cach, (object) message);
    if (fileNotes == null)
      return;
    PXNoteAttribute.SetFileNotes(cach, (object) crsmEmail, fileNotes);
  }
}
