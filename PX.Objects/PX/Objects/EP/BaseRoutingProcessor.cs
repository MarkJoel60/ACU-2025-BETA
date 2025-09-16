// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.BaseRoutingProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Mail;
using PX.Data;
using PX.Data.EP;
using PX.Objects.CR;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Mail;

#nullable disable
namespace PX.Objects.EP;

public abstract class BaseRoutingProcessor : BasicEmailProcessor
{
  protected List<MailAddress> GenerateAddress(PX.Objects.CR.Contact employeeContact, Users user)
  {
    string str1 = (string) null;
    string str2 = (string) null;
    if (user != null && user.PKID.HasValue)
    {
      string str3 = user.FullName.With<string, string>((Func<string, string>) (_ => _.Trim()));
      if (!string.IsNullOrEmpty(str3))
        str1 = str3;
      string str4 = user.Email.With<string, string>((Func<string, string>) (_ => _.Trim()));
      if (!string.IsNullOrEmpty(str4))
        str2 = str4;
    }
    if (employeeContact != null && employeeContact.BAccountID.HasValue)
    {
      string str5 = employeeContact.DisplayName.With<string, string>((Func<string, string>) (_ => _.Trim()));
      if (!string.IsNullOrEmpty(str5))
        str1 = str5;
      string str6 = employeeContact.EMail.With<string, string>((Func<string, string>) (_ => _.Trim()));
      if (!string.IsNullOrEmpty(str6))
        str2 = str6;
    }
    return !string.IsNullOrEmpty(str2) ? EmailParser.ParseAddresses(PXDBEmailAttribute.FormatAddressesWithSingleDisplayName(str2, str1)) : (List<MailAddress>) null;
  }

  protected void FindOwner(
    PXGraph graph,
    IAssign source,
    out PX.Objects.CR.Contact contact,
    out Users user,
    out EPEmployee employee)
  {
    contact = (PX.Objects.CR.Contact) null;
    user = (Users) null;
    employee = (EPEmployee) null;
    if (source == null || !source.OwnerID.HasValue)
      return;
    this.FindOwner(graph, source.OwnerID, out contact, out user, out employee);
  }

  protected void FindOwner(
    PXGraph graph,
    int? ownerId,
    out PX.Objects.CR.Contact contact,
    out Users user,
    out EPEmployee employee)
  {
    contact = (PX.Objects.CR.Contact) null;
    user = (Users) null;
    employee = (EPEmployee) null;
    if (!ownerId.HasValue)
      return;
    PXSelectBase<Users, PXSelectJoin<Users, LeftJoin<EPEmployee, On<EPEmployee.userID, Equal<Users.pKID>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<EPEmployee.defContactID>>>>, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Clear(graph);
    PXResult<Users, EPEmployee, PX.Objects.CR.Contact> pxResult = (PXResult<Users, EPEmployee, PX.Objects.CR.Contact>) PXResultset<Users>.op_Implicit(PXSelectBase<Users, PXSelectJoin<Users, LeftJoin<EPEmployee, On<EPEmployee.userID, Equal<Users.pKID>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<EPEmployee.defContactID>>>>, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Select(graph, new object[1]
    {
      (object) ownerId
    }));
    contact = PXResult<Users, EPEmployee, PX.Objects.CR.Contact>.op_Implicit(pxResult);
    user = PXResult<Users, EPEmployee, PX.Objects.CR.Contact>.op_Implicit(pxResult);
    employee = PXResult<Users, EPEmployee, PX.Objects.CR.Contact>.op_Implicit(pxResult);
  }

  protected List<MailAddress> GetOwnerAddressByNote(PXGraph graph, Guid? noteId, int? mainOwner = null)
  {
    if (!noteId.HasValue)
      return (List<MailAddress>) null;
    object source = this.FindSource(graph, noteId.Value);
    if (source == null)
      return (List<MailAddress>) null;
    PX.Objects.CR.Contact contact;
    Users user;
    EPEmployee employee;
    this.FindOwner(graph, source as IAssign, out contact, out user, out employee);
    if (user == null)
      return (List<MailAddress>) null;
    if (mainOwner.HasValue)
    {
      int? nullable = mainOwner;
      int? contactId = contact.ContactID;
      if (nullable.GetValueOrDefault() == contactId.GetValueOrDefault() & nullable.HasValue == contactId.HasValue)
        return (List<MailAddress>) null;
    }
    return user?.State == "D" || employee?.VStatus == "I" || (employee != null ? (!employee.RouteEmails.GetValueOrDefault() ? 1 : 0) : 1) != 0 ? (List<MailAddress>) null : this.GenerateAddress(contact, user);
  }

  protected object FindSource(PXGraph graph, Guid noteId)
  {
    return new EntityHelper(graph).GetEntityRow(new Guid?(noteId));
  }

  protected void RemoveAddress(BaseRoutingProcessor.MailAddressList recipients, string addrStr)
  {
    if (string.IsNullOrEmpty(addrStr) || recipients.Count == 0)
      return;
    foreach (MailAddress address in EmailParser.ParseAddresses(addrStr))
      recipients.Remove(address);
  }

  protected void SendCopyMessageToInside(
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
    MailAddress mailAddress = (MailAddress) null;
    copy1.MailFrom = EmailParser.TryParse(message.MailFrom, ref mailAddress) ? new MailAddress(account.Address, mailAddress.DisplayName).ToString() : account.Address;
    copy1.MailTo = PXDBEmailAttribute.ToString(addresses);
    copy1.MailCc = (string) null;
    copy1.MailBcc = (string) null;
    copy1.MailReply = copy1.MailFrom;
    copy1.MPStatus = "PP";
    copy1.ClassID = new int?(-2);
    new AddInfoEmailProcessor().Process(new EmailProcessEventArgs(graph, account, copy1));
    copy1.RefNoteIDType = (string) null;
    copy1.ResponseToNoteID = message.EmailNoteID;
    copy1.RefNoteID = new Guid?();
    copy1.BAccountID = new int?();
    copy1.ContactID = new int?();
    copy1.Pop3UID = (string) null;
    copy1.ImapUID = new int?();
    Guid guid = Guid.NewGuid();
    copy1.ImcUID = new Guid?(guid);
    copy1.MessageId = $"{this.GetType().Name}_{guid.ToString().Replace("-", string.Empty)}";
    copy1.OwnerID = new int?();
    copy1.WorkgroupID = new int?();
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
      crsmEmail = (CRSMEmail) cach.CreateCopy(cach.Update((object) copy2));
    }
    crsmEmail.IsPrivate = message.IsPrivate;
    crsmEmail.WorkgroupID = message.WorkgroupID;
    Guid[] fileNotes = PXNoteAttribute.GetFileNotes(cach, (object) message);
    if (fileNotes == null)
      return;
    PXNoteAttribute.SetFileNotes(cach, (object) crsmEmail, fileNotes);
  }

  protected class MailAddressList : IEnumerable<MailAddress>, IEnumerable
  {
    private readonly HybridDictionary _items = new HybridDictionary();

    public void AddRange(IEnumerable<MailAddress> addresses)
    {
      if (addresses == null)
        return;
      foreach (MailAddress address in addresses)
        this.Add(address);
    }

    public void Add(MailAddress address)
    {
      string key = address.Address.With<string, string>((Func<string, string>) (_ => _.Trim())).With<string, string>((Func<string, string>) (_ => _.ToLower()));
      if (string.IsNullOrEmpty(key) || this._items.Contains((object) key))
        return;
      this._items.Add((object) key, (object) address);
    }

    public void Remove(MailAddress address)
    {
      string key = address.Address.With<string, string>((Func<string, string>) (_ => _.Trim())).With<string, string>((Func<string, string>) (_ => _.ToLower()));
      if (!this._items.Contains((object) key))
        return;
      this._items.Remove((object) key);
    }

    public IEnumerator<MailAddress> GetEnumerator()
    {
      foreach (DictionaryEntry dictionaryEntry in this._items)
        yield return (MailAddress) dictionaryEntry.Value;
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public int Count => this._items.Count;
  }
}
