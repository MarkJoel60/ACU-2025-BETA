// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ContactBAccountEmailProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common.Mail;
using PX.Data;
using PX.Objects.EP;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

#nullable disable
namespace PX.Objects.CR;

public class ContactBAccountEmailProcessor : BasicEmailProcessor
{
  protected override bool Process(BasicEmailProcessor.Package package)
  {
    EMailAccount account = package.Account;
    if (!account.IncomingProcessing.GetValueOrDefault() || !account.CreateActivity.GetValueOrDefault())
      return false;
    PXGraph graph = package.Graph;
    CRSMEmail message = package.Message;
    if (!string.IsNullOrEmpty(message.Exception) || message.RefNoteID.HasValue)
      return false;
    List<string> stringList = new List<string>();
    stringList.Add(package.Address);
    bool? isIncome = package.Message.IsIncome;
    bool flag = false;
    if (isIncome.GetValueOrDefault() == flag & isIncome.HasValue && package.Message.MailTo != null && package.Account.EmailAccountType == "E")
      stringList.InsertRange(0, EmailParser.ParseAddresses(message.MailTo).Select<MailAddress, string>((Func<MailAddress, string>) (m => m.Address)));
    foreach (string str in stringList)
    {
      PXSelectBase<Contact, PXSelect<Contact, Where<Contact.eMail, Contains<Required<Contact.eMail>>, And<Contact.contactType, Equal<ContactTypesAttribute.person>>>>.Config>.Clear(package.Graph);
      Contact contact = PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelect<Contact, Where<Contact.eMail, Contains<Required<Contact.eMail>>, And<Contact.contactType, Equal<ContactTypesAttribute.person>>>>.Config>.SelectWindowed(package.Graph, 0, 1, new object[1]
      {
        (object) str
      }));
      if (contact != null && contact.ContactID.HasValue)
      {
        GraphHelper.EnsureCachePersistence(graph, typeof (Contact));
        GraphHelper.EnsureCachePersistence(graph, typeof (BAccount));
        message.ContactID = contact.ContactID;
        PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>>>.Config>.Clear(package.Graph);
        BAccount baccount = PXResultset<BAccount>.op_Implicit(PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>>>.Config>.Select(package.Graph, new object[1]
        {
          (object) contact.BAccountID
        }));
        if (baccount != null)
          message.BAccountID = baccount.BAccountID;
        return true;
      }
    }
    return false;
  }
}
